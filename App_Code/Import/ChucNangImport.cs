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


public class ChucNangImport : ImportOriginal
{
    private String maChucNang, tv_ngan, ta_ngan, ghichu;

    public String MaChucNang
    {
        get { return maChucNang; }
        set { maChucNang = value; }
    }

    public String Tv_ngan
    {
        get { return tv_ngan; }
        set { tv_ngan = value; }
    }

    public String Ta_ngan
    {
        get { return ta_ngan; }
        set { ta_ngan = value; }
    }

    public String Ghichu
    {
        get { return ghichu; }
        set { ghichu = value; }
    }

    public ChucNangImport()
    { }

    public ChucNangImport(String maChucNang, String tiengVietNgan, String tiengAnhNgan, String GhiChu)
        : this(maChucNang, tiengVietNgan, tiengAnhNgan, GhiChu, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {
    
    }

    public ChucNangImport(String maChucNang, String tiengVietNgan, String tiengAnhNgan, String GhiChu
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maChucNang = maChucNang;
        this.tv_ngan = tiengVietNgan;
        this.ta_ngan = tiengAnhNgan;
        this.ghichu = GhiChu;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String insert = "insert into md_chucnang(md_chucnang_id, ma_chucnang, ten_tv, ten_ta, ten_tv_dai, ten_ta_dai, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, mota, hoatdong, trangthai) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " , @ma_chucnang, @ten_tv, @ten_ta, @ten_tv_dai, @ten_ta_dai, @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @mota, @hoatdong, @trangthai)";
        mdbc.ExcuteNonQuery(insert
            , "@ma_chucnang", maChucNang
            , "@ten_tv", tv_ngan
            , "@ten_ta", ta_ngan
            , "@ten_tv_dai", ""
            , "@ten_ta_dai", ""
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
			, "@mota", ghichu
            , "@hoatdong", hoatDong
            , "@trangthai", "SOANTHAO");
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_chucnang o = db.md_chucnangs.FirstOrDefault(p => p.ma_chucnang.Equals(maChucNang));
        o.ten_tv = tv_ngan;
        o.ten_ta = ta_ngan;
		o.ten_tv_dai = "";
        o.ten_ta_dai = "";
        o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;
        o.hoatdong = hoatDong;
		o.mota = ghichu;
        db.SubmitChanges();
    }
}
