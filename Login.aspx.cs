using System;
using System.Web.Security;
using System.Data;
using System.Net;
using System.Collections.Generic;
using System.Linq;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string host = Request.Url.Host;
        string manv = txtUserName.Text;
        string msg = "";
        string matkhau = Security.EncodeMd5Hash(txtPassword.Text);
        string sql = "select * from nhanvien where manv = @manv and matkhau = @matkhau and hoatdong = 1";
        DataTable dt = mdbc.GetData(sql, "@manv", manv, "@matkhau", matkhau);
        if (dt.Rows.Count != 0)
        {
			var linkPubs = System.Web.Configuration.WebConfigurationManager.AppSettings["remoteDetect"].Split(',');
			
            string username = dt.Rows[0]["manv"].ToString();
            var remote = (dt.Rows[0]["remote"] + "").ToLower() == "true";
            var canOTP = (dt.Rows[0]["canOTP"] + "").ToLower() == "true";
            var email = (dt.Rows[0]["email"] + "");
            var email_pass = (dt.Rows[0]["email_pass"] + "");
            var otp = (dt.Rows[0]["otp"] as string);
            var otp_created = (dt.Rows[0]["otp_created"] as DateTime?);
            var hoten = (dt.Rows[0]["hoten"] + "");

            if (linkPubs.Any(host.Contains))
            {
                if(!remote)
                    msg = "Tài khoản không thể truy cập từ xa.";
                else
                {
                    if (canOTP)
                    {
                        if(!string.IsNullOrWhiteSpace(otp) & otp_created != null)
                        {
                            var limitOTPTime = otp_created.Value.AddMinutes(2);
                            if (DateTime.Now <= limitOTPTime)
                            {
                                var totalsecond = Math.Ceiling((limitOTPTime - DateTime.Now).TotalSeconds);
                                msg = string.Format("Mã OTP chỉ được cấp 2 phút 1 lần, bạn hãy chờ thêm {0} giây nữa.", totalsecond);
                            }
                        }

                        if (msg.Length <= 0)
                        {
                            char[] charArr = "0123456789".ToCharArray();
                            var lstRD = new List<string>();
                            Random objran = new Random();
                            int noofcharacters = 6;
                            for (int i = 0; i < noofcharacters; i++)
                            {
                                //It will not allow Repetation of Characters
                                int pos = objran.Next(1, charArr.Length);
                                if (!lstRD.Contains(charArr.GetValue(pos).ToString()))
                                    lstRD.Add(charArr.GetValue(pos).ToString());
                                else
                                    i--;
                            }

                            string strrandom = string.Join(" ", lstRD);
                            string strOpt = strrandom.Replace(" ", "");
                            sql = "update nhanvien set otp = @otp, otp_created = getdate() where manv = @manv";
                            mdbc.ExcuteNonQuery(sql, "@manv", username, "@otp", strOpt);

                            string ErrorMessage = "";
                            try
                            {
                                if (string.IsNullOrWhiteSpace(email))
                                    ErrorMessage = "Không tìm thấy email của bạn";
                                else
                                {
                                    string sqlUserSendMail = "select * from nhanvien where manv = @manv and hoatdong = 1";
                                    DataTable dtUserSendMail = mdbc.GetData(sqlUserSendMail, "@manv", "admin@email");
                                    if (dtUserSendMail.Rows.Count <= 0)
                                        ErrorMessage = "Không tìm thấy tài khoản email mặc định (admin@email)";
                                    else
                                    {
                                        var emailUserSendMail = (dtUserSendMail.Rows[0]["email"] + "");
                                        var email_passUserSendMail = (dtUserSendMail.Rows[0]["email_pass"] + "");
                                        if (string.IsNullOrWhiteSpace(emailUserSendMail))
                                            ErrorMessage = "Không tìm thấy email của tài khoản mặc định.";
                                        else if (string.IsNullOrWhiteSpace(email))
                                            ErrorMessage = "tài khoản mặc định chưa được thiết lập mật khẩu email.";
                                        else
                                        {
                                            string sqlSMTP = "select top 1 * from md_smtp where ten = N'Vinh Gia'";
                                            DataTable dtSMTP = mdbc.GetData(sqlSMTP);
                                            if (dtSMTP.Rows.Count > 0)
                                            {
                                                var smtpserver = dtSMTP.Rows[0]["smtpserver"] + "";
                                                var port = int.Parse((dtSMTP.Rows[0]["port"] + "").ToLower());
                                                var mail = new GoogleMail(emailUserSendMail, email_passUserSendMail, smtpserver, port);
                                                string ngayCapOTP = DateTime.Now.ToString("dd/MM/yyyy");
                                                string gioCapOTP = DateTime.Now.ToString("HH:mm");

                                                string noidung = "";
                                                noidung += string.Format(@"Xin chào ""{0}"", mật danh ""{1}""<br/><br/>", hoten, username);
                                                noidung += string.Format(@"Mật khẩu (OTP) để đăng nhập hệ thống của bạn là <b>{0}</b>.<br/><br/>", strOpt);
                                                noidung += string.Format(@"Mật khẩu (OTP) này được cấp vào ngày {0} lúc {1}.<br/><br/>", ngayCapOTP, gioCapOTP);
                                                noidung += string.Format(@"Lưu ý, nếu không phải là bạn, xin đừng cung cấp mã này cho bất cứ ai.<br/><br/>");
                                                noidung += string.Format(@"VINH GIA COMPANY LIMITED.");

                                                string tieude = "";
                                                tieude += string.Format("OTP đăng nhập VINHGIA TRADING được cấp vào ngày {0} lúc {1}", ngayCapOTP, gioCapOTP);

                                                mail.Send(emailUserSendMail, email, "", "", tieude, noidung, "");
                                            }
                                            else
                                            {
                                                ErrorMessage = "Không tìm thấy thiết lập SMTP cho email.";
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage = ex.Message;
                            }

                            if (string.IsNullOrEmpty(ErrorMessage))
                                Response.Redirect("OTP.aspx?us=" + username, true);
                            else
                                msg = ErrorMessage;
                        }
                    }
                }
            }

            if (msg.Length <= 0)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                //string url = (Request.QueryString["ReturnUrl"] != null) ? Request.QueryString["ReturnUrl"].ToString() : "Default-Security.aspx";
                string url = "Default-Security.aspx";
                Response.Redirect(url, true);
            }
        }
        else
        {
            msg = "Thông tin đăng nhập không đúng";
        }
        this.RegisterStartupScript("ThongBao", string.Format(@"<script>alert(`{0}`)</script>", msg));
    }
}
