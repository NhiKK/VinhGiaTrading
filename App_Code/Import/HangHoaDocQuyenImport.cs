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


public class HangHoaDocQuyenImport : ImportOriginal
{
    private String maKhachHang, maSanPham, ngayKetThuc, moTa, ghiChu;

    public String MaKhachHang
    {
        get { return maKhachHang; }
        set { maKhachHang = value; }
    }

    public String MaSanPham
    {
        get { return maSanPham; }
        set { maSanPham = value; }
    }
	
	public String MoTa
    {
        get { return moTa; }
        set { moTa = value; }
    }
	
	public String NgayKetThuc
    {
        get { return ngayKetThuc; }
        set { ngayKetThuc = value; }
    }
	
	public String GhiChu
    {
        get { return ghiChu; }
        set { ghiChu = value; }
    }

    public HangHoaDocQuyenImport(String md_sanpham_id, String md_doitackinhdoanh_id, String ngayKetThuc, String moTa, String ghiChu)
        : this(md_sanpham_id, md_doitackinhdoanh_id, ngayKetThuc, moTa, ghiChu, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {
    
    }

    public HangHoaDocQuyenImport(String md_sanpham_id, String md_doitackinhdoanh_id, String ngayKetThuc, String moTa, String ghiChu
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maSanPham = md_sanpham_id;
        this.maKhachHang = md_doitackinhdoanh_id;
		this.ngayKetThuc = ngayKetThuc;
		this.moTa = moTa;
		this.ghiChu = ghiChu;
		
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public HangHoaDocQuyenImport()
    {

    }

    public void Import()
    {
        String insert = "insert into md_hanghoadocquyen(md_hanghoadocquyen_id, md_doitackinhdoanh_id, md_sanpham_id, ngaybatdau, ngayketthuc, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, mota, hoatdong, ghichu) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " , @md_doitackinhdoanh_id, @md_sanpham_id, getdate(), convert(datetime, @ngayketthuc, 103), @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @mota, @hoatdong, @ghichu)";
        mdbc.ExcuteNonQuery(insert
            , "@md_doitackinhdoanh_id", maKhachHang
            , "@md_sanpham_id", maSanPham
			, "@ngayketthuc", ngayKetThuc
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
            , "@mota", moTa
            , "@hoatdong", hoatDong
			, "@ghiChu", ghiChu
			);

        md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(maKhachHang));
        if (dtkd != null)
        {
            dtkd.isdocquyen = true;
            db.SubmitChanges();
        }
    }

    LinqDBDataContext db = new LinqDBDataContext();
    public void Update()
    {
        md_hanghoadocquyen o = db.md_hanghoadocquyens.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(maKhachHang) && p.md_sanpham_id.Equals(maSanPham));
        if(string.IsNullOrEmpty(ngayKetThuc))
			o.ngayketthuc = null;
		else
			o.ngayketthuc = DateTime.ParseExact(ngayKetThuc, "dd/MM/yyyy", null);
		o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;
		o.mota = moTa;
        o.hoatdong = hoatDong;
		o.ghichu = ghiChu;
        db.SubmitChanges();

        md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(maKhachHang));
        if (dtkd != null)
        {
            dtkd.isdocquyen = true;
            db.SubmitChanges();
        }
    }
}
