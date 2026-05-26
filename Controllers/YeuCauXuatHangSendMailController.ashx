<%@ WebHandler Language="C#" Class="YeuCauXuatHangSendMailController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public class YeuCauXuatHangSendMailController : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string c_donhang_id, action;
        c_donhang_id = context.Request.QueryString["c_donhang_id"];
        action = context.Request.QueryString["action"];
        if (c_donhang_id != null)
        {
            switch (action)
            {
                case "setupMail":
                    this.SetupMail(context, c_donhang_id);
                    break;
                case "sendMail":
                    this.SendMailToKCS(context, c_donhang_id);
                    break;
                default:
                    context.Response.Write("Không xác định được hành động!");
                    break;
            }
        }
        else
        {
            context.Response.Write("Không tìm thấy đơn hàng!");
        }
    }

    LinqDBDataContext db = new LinqDBDataContext();
    
    public void SetupMail(HttpContext context, string c_donhang_id)
    {
        context.Response.ContentType = "text/plain";
        try
        {
            String manv = UserUtils.getUser(context);
            nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
            context.Response.Write(String.Format("<div>Gửi từ: {0}</div><div>Gửi đến: {1}</div><div>Cc: {2}</div>", nv.email, "quatity@ancometal.com", nv.email_cc));
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    public void SendMailToKCS(HttpContext context, string c_donhang_id)
    {
        context.Response.ContentType = "text/plain";
		try
		{
			c_donhang dh = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(c_donhang_id));
			String manv = UserUtils.getUser(context);
			nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
			md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));

			bool next = true;
			String msg = "";

			if (nv == null)
			{
				next = false;
				msg += "Không tìm thấy mã nhân viên!";
			}

			if (dh == null)
			{
				next = false;
				msg += "Không tìm thấy Danh sách đặt hàng!";
			}

			if (next)
			{
				// tạo file pdf
				rptXuatHang report = new rptXuatHang();
				string path = context.Server.MapPath("../FileSendMail/AnCoYeuCauXuatHang" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf");
				SqlDataAdapter da = new SqlDataAdapter(YeuCauXuatHangSql.getSql(c_donhang_id, ""), mdbc.GetConnection);
				DataSet ds = new DataSet();
				da.Fill(ds);
				report.DataSource = ds;
				report.DataAdapter = da;
				report.ExportToPdf(path);

				// Gửi email
				string title, content;
				title = string.Format("Yêu cầu xác nhận – Xuất hàng theo P/O số: " + dh.sochungtu);
				content = "Thân gửi: Phòng KCS !<br/>Anh/Chị vui lòng xác nhận yêu cầu xuất hàng theo P/O số " + dh.sochungtu + " trong file đính kèm.";

				GoogleMail mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value);
				mail.Send(nv.email, "quality@ancometal.com", nv.email_cc, "", title, content, path);

				context.Response.Write("Gửi mail thành công.!");
			}
			else
			{
				context.Response.Write(msg);
			}
		}catch(Exception ex)
		{
			context.Response.Write(ex);
		}
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}