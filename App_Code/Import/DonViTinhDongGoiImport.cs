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


public class DonViTinhDongGoiImport : ImportOriginal
{
    private String maDVT, tenDVT;

    public String MaDVT
    {
        get { return maDVT; }
        set { maDVT = value; }
    }

    public String TenDVT
    {
        get { return tenDVT; }
        set { tenDVT = value; }
    }


    public DonViTinhDongGoiImport()
    {
    
    }

    public DonViTinhDongGoiImport(String maDVT, String tenDVT)
        : this(maDVT, tenDVT, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public DonViTinhDongGoiImport(String maDVT, String tenDVT, DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maDVT = maDVT;
        this.tenDVT = tenDVT;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String insert = "insert into md_donvitinh(md_donvitinh_id, ma_edi, ten_dvt, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, hoatdong) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " , @ma_edi, @ten_dvt, @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @hoatdong)";
        mdbc.ExcuteNonQuery(insert
            , "@ma_edi", maDVT
            , "@ten_dvt", tenDVT
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
            , "@hoatdong", hoatDong);
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_donvitinh o = db.md_donvitinhs.FirstOrDefault(p => p.ma_edi.Equals(maDVT));
        o.ten_dvt = tenDVT;
        o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;
        db.SubmitChanges();
    }
}
