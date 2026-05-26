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


public class CangXuatHangImport : ImportOriginal
{
    private String maSanPham, maCangBien;

    public String MaSanPham
    {
        get { return maSanPham; }
        set { maSanPham = value; }
    }

    public String MaCangBien
    {
        get { return maCangBien; }
        set { maCangBien = value; }
    }

    public CangXuatHangImport()
    {
    
    }

    public CangXuatHangImport(String md_sanpham_id, String md_cangbien_id)
        : this(md_sanpham_id, md_cangbien_id, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public CangXuatHangImport(String md_sanpham_id, String md_cangbien_id
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maCangBien = md_cangbien_id;
        this.maSanPham = md_sanpham_id;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String insert = "insert into md_cangxuathang(md_cangxuathang_id, md_sanpham_id, md_cangbien_id, macdinh, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, hoatdong) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " , @md_sanpham_id, @md_cangbien_id, @macdinh, @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @hoatdong)";
        mdbc.ExcuteNonQuery(insert
            , "@md_sanpham_id", maSanPham 
            , "@md_cangbien_id", maCangBien
            , "@macdinh", true
            , "@ngaytao", ngayTao.ToString("yyyy-MM-dd HH:mm:ss")
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat.ToString("yyyy-MM-dd HH:mm:ss")
            , "@nguoicapnhat", nguoiCapNhat
            , "@hoatdong", hoatDong);
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_cangxuathang bc = db.md_cangxuathangs.FirstOrDefault(p => p.md_cangbien_id.Equals(maCangBien) && p.md_sanpham_id.Equals(maSanPham));
        bc.ngaycapnhat = ngayCapNhat;
        bc.nguoicapnhat = nguoiCapNhat;
        bc.hoatdong = hoatDong;
        db.SubmitChanges();
    }
}
