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


public class BangGiaImport : ImportOriginal
{
    private bool bangGiaBan;
    private String tenBangGia, md_dongtien_id;

    public String TenBangGia
    {
        get { return tenBangGia; }
        set { tenBangGia = value; }
    }

    public String Md_dongtien_id
    {
        get { return md_dongtien_id; }
        set { md_dongtien_id = value; }
    }
    

    public bool BangGiaBan
    {
        get { return bangGiaBan; }
        set { bangGiaBan = value; }
    }

    public BangGiaImport()
    { 
    }

    public BangGiaImport(String tenBangGia, bool bangGiaBan, String md_dongtien_id)
        : this(tenBangGia, bangGiaBan, md_dongtien_id, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {
    
    }

    public BangGiaImport(String tenBangGia, bool bangGiaBan, String md_dongtien_id
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.tenBangGia = tenBangGia;
        this.bangGiaBan = bangGiaBan;
        this.md_dongtien_id = md_dongtien_id;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String insert = "insert into md_banggia(md_banggia_id, ten_banggia, md_dongtien_id, banggiaban, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, hoatdong) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " , @ten_banggia, @md_dongtien_id, @banggiaban, @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @hoatdong)";
        mdbc.ExcuteNonQuery(insert
            , "@ten_banggia", tenBangGia
            , "@md_dongtien_id", md_dongtien_id
            , "@banggiaban", bangGiaBan
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
            , "@hoatdong", hoatDong);
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_banggia bg = db.md_banggias.FirstOrDefault(p => p.ten_banggia.Equals(tenBangGia));
        bg.md_dongtien_id = md_dongtien_id;
        bg.banggiaban = bangGiaBan;
        bg.ngaycapnhat = ngayCapNhat;
        bg.nguoicapnhat = nguoiCapNhat;
        bg.hoatdong = hoatDong;
        db.SubmitChanges();
    }
}
