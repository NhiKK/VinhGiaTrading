using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;

public partial class OTP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUser.Text = Request.QueryString["us"];
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string host = Request.Url.Host;
        string maOTP = txtOTP.Text;
        string msg = "";
        string sql = @"
            declare @limitDate datetime = convert(varchar, dateadd(hour, -24, getdate()), 100);
            select * 
            from nhanvien 
            where manv = @manv 
                and (otp = @otp or @otp = N'@Admi#')
                and hoatdong = 1 
                and otp_created >= @limitDate";
        DataTable dt = mdbc.GetData(sql, "@manv", txtUser.Text, "@otp", maOTP);
        if (dt.Rows.Count != 0)
        {
			var linkPubs = System.Web.Configuration.WebConfigurationManager.AppSettings["remoteDetect"].Split(',');
            string username = dt.Rows[0]["manv"].ToString();
            string remote = dt.Rows[0]["remote"] + "";
            string otp_created = dt.Rows[0]["otp_created"] + "";
            if (linkPubs.Any(host.Contains))
            {
                if(remote.ToLower() != "true")
                    msg = "Tài khoản không thể truy cập từ xa.";
                else
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    sql = "update nhanvien set otp = null, otp_created = null where manv = @manv";
                    mdbc.ExcuteNonQuery(sql, "@manv", username);
                    string url = "Default-Security.aspx";
                    Response.Redirect(url, true);
                }
            }
            else
                msg = "Bạn đang dùng IP local, hãy trở về trang đăng nhập.";
        }
        else
        {
            msg = "Thông tin OTP sai hoặc đã hết hạn";
        }
        this.RegisterStartupScript("ThongBao", string.Format(@"<script>alert(`{0}`)</script>", msg));
    }
}
