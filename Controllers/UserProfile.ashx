<%@ WebHandler Language="C#" Class="UserProfile" %>

using System;
using System.Web;
using System.Linq;

public class UserProfile : IHttpHandler {

    LinqDBDataContext db = new LinqDBDataContext();
    
    public void ProcessRequest (HttpContext context) {
        String type = context.Request.QueryString["type"];
        switch (type)
        {
            case "editprofile":
                this.EditProfile(context);
                break;
            case "changepassword":
                this.ChangePassword(context);
                break;
            default:
                context.Response.Write("Không tìm thấy lệnh thích hợp");
                break;
        }
    }

    public void EditProfile(HttpContext context)
    {
        String txtMaNV = UserUtils.getUser(context);
        String txtHoTen = context.Request.Form["txtHoTen"];
        String txtEmail = context.Request.Form["txtEmail"];
        String txtDienThoai = context.Request.Form["txtDienThoai"];
        String txtDiaChi = context.Request.Form["txtDiaChi"];
        String msg = "";
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(txtMaNV));
        if (nv != null)
        {
            nv.hoten = txtHoTen;
            nv.email = txtEmail;
            nv.dienthoai = txtDienThoai;
            nv.diachi = txtDiaChi;
            db.SubmitChanges();
            msg = "Thay đổi thông tin thành công!";
        }
        else {
            msg = "Mã nhân viên " + txtMaNV + " không tồn tại!";
        }

        context.Response.Write(msg);
    }

    public void ChangePassword(HttpContext context)
    {
        String txtMaNV = UserUtils.getUser(context);
        String txtMatKhau = context.Request.Form["txtMatKhau"];
        String txtLapMatKhau = context.Request.Form["txtLapMatKhau"];
        String msg = "";
        bool next = true;
        
        if (txtMatKhau.Equals(""))
        {
            next = false;
            msg += "Mật khẩu không được để trống! ";
        }
        
        if (!txtMatKhau.Equals(txtLapMatKhau))
        {
            next = false;
            msg += "Hai mật khẩu không trùng nhau! ";
        }

        if (next)
        {
            nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(txtMaNV));
            nv.matkhau = Security.EncodeMd5Hash(txtMatKhau);
            db.SubmitChanges();
            msg += "Thay đổi mật khẩu thành công! ";
        }
        context.Response.Write(msg);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}