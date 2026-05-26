using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ExcelLibrary.SpreadSheet;


public class DoiTacKinhDoanhImport : ImportOriginal
{
    private bool isNCC;
    private String maDoiTac, tenDoiTac, dienThoai, email, diaChi, maLoai, maQuocGia, maKhuVuc, fax, soTK, tenNH, maST, nguoidd, chucvu;


    public String Fax
    {
        get { return fax; }
        set { fax = value; }
    }

    public String MaLoai
    {
        get { return maLoai; }
        set { maLoai = value; }
    }

    public String MaQuocGia
    {
        get { return maQuocGia; }
        set { maQuocGia = value; }
    }

    public String MaKhuVuc
    {
        get { return maKhuVuc; }
        set { maKhuVuc = value; }
    }

    public String MaDoiTac
    {
        get { return maDoiTac; }
        set { maDoiTac = value; }
    }

    public String TenDoiTac
    {
        get { return tenDoiTac; }
        set { tenDoiTac = value; }
    }

    public String DienThoai
    {
        get { return dienThoai; }
        set { dienThoai = value; }
    }

    public String Email
    {
        get { return email; }
        set { email = value; }
    }

    public String DiaChi
    {
        get { return diaChi; }
        set { diaChi = value; }
    }
    

    public bool IsNCC
    {
        get { return isNCC; }
        set { isNCC = value; }
    }
     
	public String SoTK
    {
        get { return soTK; }
        set { soTK = value; }
    }
	
	public String TenNH
    {
        get { return tenNH; }
        set { tenNH = value; }
    }
	
	public String MaST
    {
        get { return maST; }
        set { maST = value; }
    }
	 
	public String NguoiDD
    {
        get { return nguoidd; }
        set { nguoidd = value; }
    }
	
	public String Chucvu
    {
        get { return chucvu; }
        set { chucvu = value; }
    }
	
    public DoiTacKinhDoanhImport()
    {
    
    }

    // public DoiTacKinhDoanhImport(String md_loaidtkd_id, String maDoiTac, String tenDoiTac, String dienThoai, String fax, String email, String diaChi, bool isncc, String md_quocgia_id, String md_khuvuc_id
	// , String soTK, String tenNH, String maST, String NguoiDD, String Chucvu)
        // : this(md_loaidtkd_id, maDoiTac, tenDoiTac, dienThoai, fax, email, diaChi, isncc, md_quocgia_id, md_khuvuc_id, soTK, tenNH, maST, NguoiDD, Chucvu, DateTime.Now, "admin", DateTime.Now, "admin", true)
    // {

    // }

    // public DoiTacKinhDoanhImport()
    // {
        // this.maLoai = md_loaidtkd_id;
        // this.maDoiTac = maDoiTac;
        // this.tenDoiTac = tenDoiTac;
        // this.dienThoai = dienThoai;
        // this.fax = fax;
        // this.email = email;
        // this.diaChi = diaChi;
        // this.isNCC = isncc;
        // this.maKhuVuc = md_khuvuc_id;
        // this.maQuocGia = md_quocgia_id;
		// this.soTK = soTK; 
		// this.tenNH = tenNH;
		// this.maST = maST;
		// this.nguoidd = NguoiDD;
		// this.chucvu = Chucvu;
        // this.ngayTao = ngayTao;
        // this.nguoiTao = nguoiTao;
        // this.ngayCapNhat = ngayCapNhat;
        // this.nguoiCapNhat = nguoiCapNhat;
        // this.hoatDong = hoatDong;
    // }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Import(String maLoai, String maDoiTac, String tenDoiTac, String dienThoai, String fax, String email, String diaChi, bool isNCC, String maQuocGia, String maKhuVuc
        , String soTK, String tenNH, String maST, String NguoiDD, String Chucvu)
    {
        md_doitackinhdoanh dtkd = new md_doitackinhdoanh();
        dtkd.md_doitackinhdoanh_id = ImportUtils.getNEWID();
        dtkd.md_loaidtkd_id = maLoai;
        dtkd.ma_dtkd = maDoiTac;
        dtkd.ten_dtkd = tenDoiTac;
        dtkd.tel = dienThoai;
        dtkd.fax = fax;
        dtkd.email = email;
        dtkd.diachi = diaChi;
        dtkd.isncc = isNCC;
        dtkd.md_khuvuc_id = maKhuVuc;
        dtkd.md_quocgia_id = maQuocGia;
		dtkd.so_taikhoan = soTK; 
		dtkd.nganhang = tenNH;
		dtkd.masothue = maST;
		dtkd.daidien = NguoiDD;
		dtkd.chucvu = Chucvu;
		
        dtkd.isdocquyen = false;
        dtkd.ngaytao = DateTime.Now;
        dtkd.nguoitao = "admin";
        dtkd.ngaycapnhat = DateTime.Now;
        dtkd.nguoicapnhat = "admin";
        dtkd.hoatdong = true;

        db.md_doitackinhdoanhs.InsertOnSubmit(dtkd);
        db.SubmitChanges();
    }

    public void Update(String maLoai, String maDoiTac, String tenDoiTac, String dienThoai, String fax, String email, String diaChi, bool isNCC, String maQuocGia, String maKhuVuc
        , String soTK, String tenNH, String maST, String NguoiDD, String Chucvu)
    {
        md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.ma_dtkd.Equals(maDoiTac));
        dtkd.md_loaidtkd_id = maLoai;
        dtkd.ten_dtkd = tenDoiTac;
        dtkd.tel = dienThoai;
        dtkd.fax = fax;
        dtkd.email = email;
        dtkd.diachi = diaChi;
        dtkd.isncc = isNCC;
        dtkd.md_khuvuc_id = maKhuVuc;
        dtkd.md_quocgia_id = maQuocGia;
		dtkd.so_taikhoan = soTK; 
		dtkd.nganhang = tenNH;
		dtkd.masothue = maST;
		dtkd.daidien = NguoiDD;
		dtkd.chucvu = Chucvu;
        dtkd.ngaycapnhat = DateTime.Now;
        dtkd.nguoicapnhat = "admin";
        dtkd.hoatdong = true;
		db.SubmitChanges();
    }
}
