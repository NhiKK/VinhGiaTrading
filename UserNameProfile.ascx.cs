using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class UserNameProfile : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = "select * from nhanvien where manv = @manv";
        string manv = Context.User.Identity.Name;
        string username = "";
        System.Data.SqlClient.SqlDataReader rd = mdbc.ExecuteReader(sql, "@manv", manv);
        if (rd.HasRows)
        {
            rd.Read();
            username = rd["manv"].ToString();
        }
    }

    protected void btnThoat_Click(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx", true);
    }
}
