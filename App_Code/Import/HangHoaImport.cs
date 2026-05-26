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


public class HangHoaImport : ImportOriginal
{
    private decimal l, w, h, trongLuong, dienTich, theTich;
    private String maHangHoa, maChungLoai, maKieuDang, maChucNang, maDonViTinh, maNhomNangLuc, 
        maCangBien, maNhaCungUng, maHSCode, trangThai, ghiChu, chucNangSP, ngayXacNhan, tinhTrangHH, hinhThucBan;

	public String GhiChu
    {
        get { return ghiChu; }
        set { ghiChu = value; }
    }
	
    public String TrangThai
    {
        get { return trangThai; }
        set { trangThai = value; }
    }

    public String MaHSCode
    {
        get { return maHSCode; }
        set { maHSCode = value; }
    }

    public String MaChungLoai
    {
        get { return maChungLoai; }
        set { maChungLoai = value; }
    }

    public String MaNhaCungUng
    {
        get { return maNhaCungUng; }
        set { maNhaCungUng = value; }
    }

    public String MaHangHoa
    {
        get { return maHangHoa; }
        set { maHangHoa = value; }
    }

    public String MaCangBien
    {
        get { return maCangBien; }
        set { maCangBien = value; }
    }

    public String MaNhomNangLuc
    {
        get { return maNhomNangLuc; }
        set { maNhomNangLuc = value; }
    }

    public String MaDonViTinh
    {
        get { return maDonViTinh; }
        set { maDonViTinh = value; }
    }

    public String MaChucNang
    {
        get { return maChucNang; }
        set { maChucNang = value; }
    }

    public String MaKieuDang
    {
        get { return maKieuDang; }
        set { maKieuDang = value; }
    }
    


    public decimal TrongLuong
    {
        get { return trongLuong; }
        set { trongLuong = value; }
    }

    public decimal L
    {
        get { return l; }
        set { l = value; }
    }

    public decimal H
    {
        get { return h; }
        set { h = value; }
    }

    public decimal W
    {
        get { return w; }
        set { w = value; }
    }

    public decimal DienTich
    {
        get { return dienTich; }
        set { dienTich = value; }
    }

    public string ChucNangSP
    {
        get { return chucNangSP; }
        set { chucNangSP = value; }
    }
	
	public string NgayXacNhan
    {
        get { return ngayXacNhan; }
        set { ngayXacNhan = value; }
    }
	
	public string TinhTrangHH
    {
        get { return tinhTrangHH; }
        set { tinhTrangHH = value; }
    }
	
	public string HinhThucBan
    {
        get { return hinhThucBan; }
        set { hinhThucBan = value; }
    }

    public HangHoaImport()
    { }

    public HangHoaImport(String maHangHoa, String md_chungloai_id, String md_kieudang_id, String md_chucnang_id, 
        String md_donvitinhsanpham_id, decimal l, decimal w, decimal h, decimal trongLuong
        , decimal dienTich
        , decimal theTich
        , String md_nhomnangluc_id, String md_cangbien_id, String md_doitackinhdoanh_id, String md_hscode_id, 
        String trangthai, String chucnangsp, String ngayXacNhan, String tinhTrangHH, String hinhThucBan, String ghichu)
        : this(maHangHoa, md_chungloai_id, md_kieudang_id, md_chucnang_id, md_donvitinhsanpham_id, l, w, h, trongLuong
              , dienTich
              , theTich
              , md_nhomnangluc_id, md_cangbien_id, md_doitackinhdoanh_id, md_hscode_id, trangthai, 
             chucnangsp, ngayXacNhan, tinhTrangHH, hinhThucBan, ghichu, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public HangHoaImport(String maHangHoa, String md_chungloai_id, String md_kieudang_id, String md_chucnang_id, String md_donvitinhsanpham_id, 
        decimal l, decimal w, decimal h, decimal trongLuong
        , decimal dienTich
        , decimal theTich
        , String md_nhomnangluc_id
        , String md_cangbien_id, 
        String md_doitackinhdoanh_id, String md_hscode_id, String trangthai, String chucnangsp, String ngayXacNhan, String tinhTrangHH, String hinhThucBan, String ghichu
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maHangHoa = maHangHoa;
        this.maChungLoai = md_chungloai_id;
        this.maKieuDang = md_kieudang_id;
        this.maChucNang = md_chucnang_id;
        this.maDonViTinh = md_donvitinhsanpham_id;
        this.l = l;
        this.w = w;
        this.h = h;
        this.trongLuong = trongLuong;
        this.dienTich = dienTich;
        this.theTich = theTich;
        this.maNhomNangLuc = md_nhomnangluc_id;
        this.maCangBien = md_cangbien_id;
        this.maNhaCungUng = md_doitackinhdoanh_id;
        this.maHSCode = md_hscode_id;
        this.trangThai = trangthai;
        this.chucNangSP = chucnangsp;
		this.ngayXacNhan = ngayXacNhan;
		this.tinhTrangHH = tinhTrangHH;
		if(hinhThucBan == "0")
			this.hinhThucBan = "Bán đại trà";
		else if(hinhThucBan == "1")
			this.hinhThucBan = "Độc quyền";
		else if(hinhThucBan == "2")
			this.hinhThucBan = "Quản lý ngoài";
		else if(hinhThucBan == "3")
			this.hinhThucBan = "Bán thử nghiệm";
		else
			this.hinhThucBan = "";
        this.ghiChu = ghichu;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        md_sanpham sp = new md_sanpham();
        sp.md_sanpham_id = ImportUtils.getNEWID();
        sp.ma_sanpham = maHangHoa;
        sp.md_kieudang_id = maKieuDang;
        sp.md_chucnang_id = maChucNang;
        sp.md_chungloai_id = maChungLoai;
        sp.md_donvitinhsanpham_id = maDonViTinh;
        sp.l_cm = l;
        sp.h_cm = h;
        sp.w_cm = w;
        sp.l_inch = l / (decimal)2.54;
        sp.w_inch = w / (decimal)2.54;
        sp.h_inch = h / (decimal)2.54;

        sp.trongluong = trongLuong;
        sp.dientich = dienTich;
        sp.thetich = theTich;
        sp.md_nhomnangluc_id = maNhomNangLuc;
        sp.nhacungung = maNhaCungUng;
        sp.md_cangbien_id = maCangBien;
        sp.md_hscode_id = maHSCode;
        sp.trangthai = "SOANTHAO";
        sp.chucnangsp = chucNangSP;
        if(string.IsNullOrEmpty(ngayXacNhan))
			sp.ngayxacnhan = null;
		else
			sp.ngayxacnhan = DateTime.ParseExact(ngayXacNhan, "dd/MM/yyyy", null);
		sp.md_tinhtranghanghoa_id = tinhTrangHH;
		sp.hinhthucban = hinhThucBan;
		sp.mota = ghiChu;
        sp.ngaytao = ngayTao;
        sp.nguoitao = nguoiTao;
        sp.ngaycapnhat = ngayCapNhat;
        sp.nguoicapnhat = nguoiCapNhat;
        sp.hoatdong = hoatDong;

        db.md_sanphams.InsertOnSubmit(sp);
        db.SubmitChanges();
    }

    LinqDBDataContext db = new LinqDBDataContext();


    public void Update()
    {
        md_sanpham o = db.md_sanphams.FirstOrDefault(p => p.ma_sanpham.Equals(maHangHoa));
        o.md_kieudang_id = maKieuDang;
        o.md_chucnang_id = maChucNang;
        o.md_donvitinhsanpham_id = maDonViTinh;
        o.l_cm = l;
        o.w_cm = w;
        o.h_cm = h;
        o.l_inch = l / (decimal)2.54;
        o.w_inch = w / (decimal)2.54;
        o.h_inch = h / (decimal)2.54;
        o.trongluong = trongLuong;
        o.dientich = dienTich;
        o.thetich = theTich;
        o.md_nhomnangluc_id = maNhomNangLuc;
        o.md_chungloai_id =maChungLoai;
        o.nhacungung = maNhaCungUng;
        o.md_cangbien_id = maCangBien;
        o.md_hscode_id = maHSCode;
        o.trangthai = trangThai;
        o.chucnangsp = chucNangSP;
		if(string.IsNullOrEmpty(ngayXacNhan))
			o.ngayxacnhan = null;
		else
			o.ngayxacnhan = DateTime.ParseExact(ngayXacNhan, "dd/MM/yyyy", null);
		o.md_tinhtranghanghoa_id = tinhTrangHH;
		o.hinhthucban = hinhThucBan;
		o.mota = ghiChu;
        o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;
        o.hoatdong = hoatDong;

        db.SubmitChanges();
    }
}
