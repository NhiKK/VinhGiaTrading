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


public class BoCaiImport : ImportOriginal
{
    private String maBoCai, tv_ngan, ta_ngan, tv_dai, ta_dai;

    public String MaBoCai
    {
        get { return maBoCai; }
        set { maBoCai = value; }
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

    public String Tv_dai
    {
        get { return tv_dai; }
        set { tv_dai = value; }
    }

    public String Ta_dai
    {
        get { return ta_dai; }
        set { ta_dai = value; }
    }

    public BoCaiImport(String maBoCai, String tiengVietNgan, String tiengAnhNgan, String tiengVietDai, String tiengAnhDai)
        :this(maBoCai, tiengVietNgan, tiengAnhNgan, tiengVietDai, tiengAnhDai, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {
    
    }

    public BoCaiImport(String maBoCai, String tiengVietNgan, String tiengAnhNgan, String tiengVietDai, String tiengAnhDai
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maBoCai = maBoCai;
        this.tv_ngan = tiengVietNgan;
        this.ta_ngan = tiengAnhNgan;
        this.tv_dai = tiengVietDai;
        this.ta_dai = tiengAnhDai;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String insert = "insert into md_bocai(md_bocai_id, code_bc, tv_ngan, ta_ngan, tv_dai, ta_dai, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, hoatdong) values( " +
            " (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32)) " +
            " , @code_bc, @tv_ngan, @ta_ngan, @tv_dai, @ta_dai, @ngaytao, @nguoitao, @ngaycapnhat, @nguoicapnhat, @hoatdong)";
        mdbc.ExcuteNonQuery(insert
            , "@code_bc", MaBoCai
            , "@tv_ngan", tv_ngan
            , "@ta_ngan", ta_ngan
            , "@tv_dai", tv_dai
            , "@ta_dai", ta_dai
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
            , "@hoatdong", hoatDong);
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_bocai bc = db.md_bocais.FirstOrDefault(p => p.code_bc.Equals(maBoCai));
        bc.tv_ngan = tv_ngan;
        bc.tv_dai = tv_dai;
        bc.ta_ngan = ta_ngan;
        bc.ta_dai = bc.ta_dai;
        bc.ngaycapnhat = ngayCapNhat;
        bc.nguoicapnhat = nguoiCapNhat;
        bc.hoatdong = hoatDong;
        db.SubmitChanges();
    }
}
