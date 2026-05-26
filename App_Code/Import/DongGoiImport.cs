/*using System;
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


public class DongGoiImport : ImportOriginal
{
    private decimal sl_inner, l1, w1, h1, l2_mix, w2_mix
        , h2_mix, vd, vn, vl, sl_outer, soluonggoi_ctn;
    private String ma_donggoi, tenDongGoi, dvt_inner, dvt_outer, ghichu_vachngan, tenTrongLuong;

    private Nullable<DateTime> ngayXacNhan;

    public Nullable<DateTime> NgayXacNhan
    {
        get { return ngayXacNhan; }
        set { ngayXacNhan = value; }
    }

    public String TenDongGoi
    {
        get { return tenDongGoi; }
        set { tenDongGoi = value; }
    }

    public String TenTrongLuong
    {
        get { return tenTrongLuong; }
        set { tenTrongLuong = value; }
    }

    public String Ma_donggoi
    {
        get { return ma_donggoi; }
        set { ma_donggoi = value; }
    }

    public String Dvt_inner
    {
        get { return dvt_inner; }
        set { dvt_inner = value; }
    }

    public String Dvt_outer
    {
        get { return dvt_outer; }
        set { dvt_outer = value; }
    }

    public String Ghichu_vachngan
    {
        get { return ghichu_vachngan; }
        set { ghichu_vachngan = value; }
    }

    public decimal Sl_inner
    {
        get { return sl_inner; }
        set { sl_inner = value; }
    }

    public decimal L1
    {
        get { return l1; }
        set { l1 = value; }
    }

    public decimal W1
    {
        get { return w1; }
        set { w1 = value; }
    }

    public decimal H1
    {
        get { return h1; }
        set { h1 = value; }
    }

    public decimal L2_mix
    {
        get { return l2_mix; }
        set { l2_mix = value; }
    }

    public decimal W2_mix
    {
        get { return w2_mix; }
        set { w2_mix = value; }
    }

    public decimal H2_mix
    {
        get { return h2_mix; }
        set { h2_mix = value; }
    }

    public decimal Vd
    {
        get { return vd; }
        set { vd = value; }
    }

    public decimal Vn
    {
        get { return vn; }
        set { vn = value; }
    }

    public decimal Vl
    {
        get { return vl; }
        set { vl = value; }
    }

    public decimal Sl_outer
    {
        get { return sl_outer; }
        set { sl_outer = value; }
    }

    public decimal Soluonggoi_ctn
    {
        get { return soluonggoi_ctn; }
        set { soluonggoi_ctn = value; }
    }


    public DongGoiImport()
    {

    }

    public DongGoiImport(String maDongGoi, String tenDongGoi, decimal slInner, String md_inner_id, decimal l1, decimal w1, decimal h1
        , decimal slOuter, String md_outer_id, decimal l2, decimal w2, decimal h2
        , decimal slCont, decimal vd, decimal vn, decimal vl, String ghichu_vachngan, Nullable<DateTime> ngayXacNhan)
        : this(maDongGoi, tenDongGoi, slInner, md_inner_id, l1, w1, h1, slOuter, md_outer_id, l2, w2, h2, slCont, vd, vn, vl, ghichu_vachngan, DateTime.Now, "admin", DateTime.Now, "admin", true, ngayXacNhan)
    {


    }

    public DongGoiImport(String maDongGoi, String tenDongGoi, decimal slInner, String md_inner_id, decimal l1, decimal w1, decimal h1
        , decimal slOuter, String md_outer_id, decimal l2, decimal w2, decimal h2
        , decimal slCont, decimal vd, decimal vn, decimal vl, String ghichu_vachngan
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong, Nullable<DateTime> ngayXacNhan)
    {
        this.ma_donggoi = maDongGoi;
        this.tenDongGoi = tenDongGoi;
        this.sl_inner = slInner;
        this.dvt_inner = md_inner_id;
        this.l1 = l1;
        this.w1 = w1;
        this.h1 = h1;
        this.sl_outer = slOuter;
        this.dvt_outer = md_outer_id;
        this.l2_mix = l2;
        this.w2_mix = w2;
        this.h2_mix = h2;
        this.soluonggoi_ctn = slCont;
        this.vd = vd;
        this.vn = vn;
        this.vl = vl;
        this.ghichu_vachngan = ghichu_vachngan;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
        this.ngayXacNhan = ngayXacNhan;
    }

    public void Import()
    {
        md_donggoi o = new md_donggoi();
        o.md_donggoi_id = ImportUtils.getNEWID();
        o.ma_donggoi = ma_donggoi;
        o.ten_donggoi = tenDongGoi;
        o.sl_inner = sl_inner;
        o.dvt_inner = dvt_inner;
        o.l1 = l1;
        o.w1 = w1;
        o.h1 = h1;
        o.sl_outer = sl_outer;
        o.dvt_outer = dvt_outer;
        o.l2_mix = l2_mix;
        o.w2_mix = w2_mix;
        o.h2_mix = h2_mix;
        o.v2 = l2_mix * w2_mix * h2_mix / 1000000;
        o.soluonggoi_ctn = soluonggoi_ctn;
        o.vd = vd;
        o.vn = vn;
        o.vl = vl;
        o.ghichu_vachngan = ghichu_vachngan;
        o.ngaytao = ngayTao;
        o.nguoitao = nguoiTao;
        o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;
        o.hoatdong = hoatDong;
        o.ngayxacnhan = ngayXacNhan;

        db.md_donggois.InsertOnSubmit(o);
        db.SubmitChanges();
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_donggoi o = db.md_donggois.FirstOrDefault(p => p.ma_donggoi.Equals(ma_donggoi));
        o.ten_donggoi = tenDongGoi;
        o.sl_inner = sl_inner;
        o.dvt_inner = dvt_inner;
        o.l1 = l1;
        o.w1 = w1;
        o.h1 = h1;
        o.sl_outer = sl_outer;
        o.dvt_outer = dvt_outer;
        o.l2_mix = l2_mix;
        o.w2_mix = w2_mix;
        o.h2_mix = h2_mix;
        o.v2 = l2_mix * w2_mix * h2_mix / 1000000;
        o.soluonggoi_ctn = soluonggoi_ctn;
        o.vd = vd;
        o.vn = vn;
        o.vl = vl;
        o.ghichu_vachngan = ghichu_vachngan;
        o.ngaytao = ngayTao;
        o.nguoitao = nguoiTao;
        o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;
        o.hoatdong = hoatDong;
        o.ngayxacnhan = ngayXacNhan;

        db.SubmitChanges();
    }
}*/

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


public class DongGoiImport : ImportOriginal
{
    public void Import(string maDongGoi, string tenDongGoi, decimal sl_inner, string dvt_inner, decimal l1, decimal w1, decimal h1, decimal sl_outer, string dvt_outer, decimal l2_mix, decimal w2_mix, decimal h2_mix,
	decimal slcont40, decimal slcont20, decimal slcont40hc, String nxn, string ghichu, bool dgdg, decimal trongluongdonggoi, decimal cellCPDGVuotChuan, decimal cellNW1, decimal cellGW1, decimal cellNW2,
    decimal cellVTDG2, decimal cellGW2)
    {
        md_donggoi o = new md_donggoi();
        o.md_donggoi_id = ImportUtils.getNEWID();
        o.ma_donggoi = maDongGoi;
        o.ten_donggoi = tenDongGoi;
        o.sl_inner = sl_inner;
        o.dvt_inner = dvt_inner;
        o.l1 = l1;
        o.w1 = w1;
        o.h1 = h1;
        o.sl_outer = sl_outer;
        o.dvt_outer = dvt_outer;
        o.l2_mix = l2_mix;
        o.w2_mix = w2_mix;
        o.h2_mix = h2_mix;
        o.v2 = l2_mix * w2_mix * h2_mix / 1000000;
        o.soluonggoi_ctn_20 = slcont20;
        o.soluonggoi_ctn = slcont40;
        o.soluonggoi_ctn_40hc = slcont40hc;
        o.nw1 = cellNW1;
        o.nw2 = cellNW2;
        o.gw1 = cellGW1;
        o.gw2 = cellGW2;
        o.vtdg2 = cellVTDG2;
        //o.vd = vd;
        //o.vn = vn;
        //o.vl = vl;
        //o.ghichu_vachngan = ghichu_vachngan;
        o.ngaytao = DateTime.Now;
        o.nguoitao = "admin";
        o.ngaycapnhat = DateTime.Now;
        o.nguoicapnhat = "admin";
        o.hoatdong = true;
		if(string.IsNullOrEmpty(nxn))
			o.ngayxacnhan = null;
		else
			o.ngayxacnhan = DateTime.ParseExact(nxn, "dd/MM/yyyy", null);
        //o.ngayxacnhan = nxn;
		o.mota = ghichu;
        o.trongluongdonggoi = trongluongdonggoi;
        o.doigia_donggoi = dgdg;
        o.cpdg_vuotchuan = cellCPDGVuotChuan;
        o.trangthai = "SOANTHAO";
        db.md_donggois.InsertOnSubmit(o);
        db.SubmitChanges();
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update(string maDongGoi, string tenDongGoi, decimal sl_inner, string dvt_inner, decimal l1, decimal w1, decimal h1, decimal sl_outer, string dvt_outer, decimal l2_mix, decimal w2_mix, decimal h2_mix,
	decimal slcont40, decimal slcont20, decimal slcont40hc, String nxn, string ghichu, bool dgdg, decimal trongluongdonggoi, decimal cellCPDGVuotChuan, decimal cellNW1, decimal cellGW1, decimal cellNW2,
    decimal cellVTDG2, decimal cellGW2)
    {
        md_donggoi o = db.md_donggois.FirstOrDefault(p => p.ma_donggoi.Equals(maDongGoi));
        if(o != null) {
			o.ten_donggoi = tenDongGoi;
			o.sl_inner = sl_inner;
			o.dvt_inner = dvt_inner;
			o.l1 = l1;
			o.w1 = w1;
			o.h1 = h1;
			o.sl_outer = sl_outer;
			o.dvt_outer = dvt_outer;
			o.l2_mix = l2_mix;
			o.w2_mix = w2_mix;
			o.h2_mix = h2_mix;
			o.v2 = l2_mix * w2_mix * h2_mix / 1000000;
			o.soluonggoi_ctn_20 = slcont20;
			o.soluonggoi_ctn = slcont40;
			o.soluonggoi_ctn_40hc = slcont40hc;
            o.nw1 = cellNW1;
            o.nw2 = cellNW2;
            o.gw1 = cellGW1;
            o.gw2 = cellGW2;
            o.vtdg2 = cellVTDG2;

            o.ngaycapnhat = DateTime.Now;
			o.nguoicapnhat = "admin";
			o.hoatdong = true;
			if(string.IsNullOrEmpty(nxn))
				o.ngayxacnhan = null;
			else
				o.ngayxacnhan = DateTime.ParseExact(nxn, "dd/MM/yyyy", null);
			//o.ngayxacnhan = nxn;
			o.mota = ghichu;
            o.trongluongdonggoi = trongluongdonggoi;
            o.doigia_donggoi = dgdg;
            o.cpdg_vuotchuan = cellCPDGVuotChuan;
			db.SubmitChanges();
		}
    }
}
