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


public class CangBienImport : ImportOriginal
{
    private String maCangBien, tenCangBien;

    public String TenCangBien
    {
        get { return tenCangBien; }
        set { tenCangBien = value; }
    }

    public String MaCangBien
    {
        get { return maCangBien; }
        set { maCangBien = value; }
    }

    public CangBienImport()
    {

    }

    public CangBienImport(String maCangBien, String tenCangBien)
        : this(maCangBien, tenCangBien, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public CangBienImport(String maCangBien, String tenCangBien, DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maCangBien = maCangBien;
        this.tenCangBien = tenCangBien;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String insert = "insert into md_cangbien(md_cangbien_id, ma_cangbien, ten_cangbien, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, hoatdong) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " ,@ma_cangbien, @ten_cangbien, @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @hoatdong)";
        mdbc.ExcuteNonQuery(insert, "@ma_cangbien", maCangBien, "@ten_cangbien", tenCangBien
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
            , "@hoatdong", hoatDong);
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_cangbien bc = db.md_cangbiens.FirstOrDefault(p => p.ma_cangbien.Equals(maCangBien));
        bc.ten_cangbien = tenCangBien;
        bc.ngaycapnhat = ngayCapNhat;
        bc.nguoicapnhat = nguoiCapNhat;
        bc.hoatdong = hoatDong;
        db.SubmitChanges();
    }
}
