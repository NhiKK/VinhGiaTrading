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


public class MauSacImport : ImportOriginal
{
    private String code_mau, code_dt, code_cl, md_detai_id, md_chungloai_id, tv_ngan, ta_ngan, hinhthucban, hemau, md_nhommau_id, ghichu;

    public String Md_detai_id
    {
        get { return md_detai_id; }
        set { md_detai_id = value; }
    }

    public String Md_chungloai_id
    {
        get { return md_chungloai_id; }
        set { md_chungloai_id = value; }
    }

    public String Code_mau
    {
        get { return code_mau; }
        set { code_mau = value; }
    }

    public String Code_dt
    {
        get { return code_dt; }
        set { code_dt = value; }
    }

    public String Code_cl
    {
        get { return code_cl; }
        set { code_cl = value; }
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

    public String Hinhthucban
    {
        get { return hinhthucban; }
        set { hinhthucban = value; }
    }

    public String Hemau
    {
        get { return hemau; }
        set { hemau = value; }
    }

    public String Nhommau
    {
        get { return md_nhommau_id; }
        set { md_nhommau_id = value; }
    }


    public String Ghichu
    {
        get { return ghichu; }
        set { ghichu = value; }
    }

    public MauSacImport()
    {

    }

    public MauSacImport(String md_chungloai_id, String md_detai_id, String maMau, String code_dt, String code_cl, String tiengVietNgan, String tiengAnhNgan, String Hinhthucban, String Hemau, String Nhommau, String ghiChu)
        : this(md_chungloai_id, md_detai_id, maMau, code_dt, code_cl, tiengVietNgan, tiengAnhNgan, Hinhthucban, Hemau, Nhommau, ghiChu, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public MauSacImport(String md_chungloai_id, String md_detai_id, String maMau, String code_dt, String code_cl, String tiengVietNgan, String tiengAnhNgan, String Hinhthucban, String Hemau, String Nhommau, String ghiChu
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        if (Hinhthucban == "0")
            hinhthucban = "Bán đại trà";
        else if (Hinhthucban == "1")
            hinhthucban = "Độc quyền";
        else if (Hinhthucban == "2")
            hinhthucban = "Quản lý ngoài";
        else if (Hinhthucban == "3")
            hinhthucban = "Bán thử nghiệm";
        else
            hinhthucban = "";

        this.code_mau = maMau;
        this.md_detai_id = md_detai_id;
        this.md_chungloai_id = md_chungloai_id;
        this.code_dt = code_dt;
        this.code_cl = code_cl;
        this.tv_ngan = tiengVietNgan;
        this.ta_ngan = tiengAnhNgan;
        this.hinhthucban = hinhthucban;
        this.Hemau = Hemau;
        this.Nhommau = Nhommau;
        //this.tv_dai = tiengVietDai;
        //this.ta_dai = tiengAnhDai;
        this.ghichu = ghiChu;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String insert = "insert into md_mausac(md_mausac_id, md_detai_id, md_chungloai_id, code_mau, code_dt, code_cl, tv_ngan, ta_ngan, tv_dai, ta_dai, hinhthucban, hemau, md_nhommau_id, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, hoatdong, trangthai, mota) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " , @md_detai_id, @md_chungloai_id, @code_mau, @code_dt, @code_cl, @tv_ngan, @ta_ngan, @tv_dai, @ta_dai, @hinhthucban, @hemau, @md_nhommau_id, @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @hoatdong, @trangthai, @mota)";
        mdbc.ExcuteNonQuery(insert
            , "@md_detai_id", md_detai_id
            , "@md_chungloai_id", md_chungloai_id
            , "@code_mau", code_mau
            , "@code_dt", code_dt
            , "@code_cl", code_cl
            , "@tv_ngan", tv_ngan
            , "@ta_ngan", ta_ngan
            , "@tv_dai", ""
            , "@ta_dai", ""
            , "@hinhthucban", hinhthucban
            , "@hemau", hemau
            , "@md_nhommau_id", md_nhommau_id
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
            , "@hoatdong", hoatDong
            , "@trangthai", "SOANTHAO"
            , "@mota", ghichu);
    }

    LinqDBDataContext db = new LinqDBDataContext();
    public void Update()
    {
        md_mausac o = db.md_mausacs.FirstOrDefault(p => p.code_mau.Equals(code_mau) && p.code_cl.Equals(code_cl) && p.code_dt.Equals(code_dt));
        o.code_dt = code_dt;
        o.code_cl = code_cl;
        o.tv_ngan = tv_ngan;
        //o.tv_dai = tv_dai;
        o.ta_ngan = ta_ngan;
        //o.ta_dai = ta_dai;
        o.hinhthucban = hinhthucban;
        o.hemau = hemau;
        o.md_nhommau_id = md_nhommau_id;
        o.mota = ghichu;
        o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;

        db.SubmitChanges();
    }
}
