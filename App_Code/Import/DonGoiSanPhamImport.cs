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


public class DonGoiSanPhamImport : ImportOriginal
{
    private String maDongGoi, maSanPham;
    private bool macDinh;

    public bool MacDinh
    {
        get { return macDinh; }
        set { macDinh = value; }
    }

    public String MaDongGoi
    {
        get { return maDongGoi; }
        set { maDongGoi = value; }
    }

    public String MaSanPham
    {
        get { return maSanPham; }
        set { maSanPham = value; }
    }

    public DonGoiSanPhamImport()
    {
    
    }

    public DonGoiSanPhamImport(String md_donggoi_id, String md_sanpham_id, bool macDinh)
        : this(md_donggoi_id, md_sanpham_id, macDinh, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public DonGoiSanPhamImport(String md_donggoi_id, String md_sanpham_id, bool macDinh
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maDongGoi = md_donggoi_id;
        this.maSanPham = md_sanpham_id;
        this.macDinh = macDinh;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String insert = "insert into md_donggoisanpham(md_donggoisanpham_id, md_donggoi_id, md_sanpham_id, soluong, macdinh, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, hoatdong) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " , @md_donggoi_id, @md_sanpham_id, @soluong, @macdinh, @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @hoatdong)";
        mdbc.ExcuteNonQuery(insert
            , "@md_donggoi_id", maDongGoi
            , "@md_sanpham_id", maSanPham
            , "@soluong", 1
            , "@macdinh", macDinh
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
            , "@hoatdong", hoatDong);
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_donggoisanpham o = db.md_donggoisanphams.FirstOrDefault(p => p.md_donggoi_id.Equals(maDongGoi) && p.md_sanpham_id.Equals(maSanPham));
        o.soluong = 1;
        o.macdinh = macDinh;
        o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;
        o.hoatdong = hoatDong;
        db.SubmitChanges();
    }
}
