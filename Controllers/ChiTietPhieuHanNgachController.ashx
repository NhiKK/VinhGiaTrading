<%@ WebHandler Language="C#" Class="ChiTietPhieuHanNgachController" %>

using System;
using System.Web;
using System.Linq;
using System.Data.Linq;

public class ChiTietPhieuHanNgachController : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            default:
                switch (oper)
                {
                    case "del":
                        this.del(context);
                        break;
                    case "edit":
                        this.edit(context);
                        break;
                    case "add":
                        this.add(context);
                        break;
                    default:
                        this.load(context);
                        break;
                }
                break;
        }
    }

    public void load(HttpContext context) {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        string c_phieuhanngach_id = context.Request.QueryString["c_phieuhanngach_id"];


        String sqlCount = "SELECT COUNT(*) AS count FROM c_chitietphieuhanngach ctphn, md_sanpham sp, md_doitackinhdoanh dtkd where ctphn.md_sanpham_id = sp.md_sanpham_id and ctphn.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id and ctphn.c_phieuhanngach_id = '" + c_phieuhanngach_id + "'";
        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        String sidx = context.Request.QueryString["sidx"];
        String sord = context.Request.QueryString["sord"];
        int total_page;
        int count = (int)mdbc.ExecuteScalar(sqlCount);
        int start, end;

        if (count > 0)
        {
            total_page = (int)Math.Ceiling(1.0 * count / limit);
        }
        else
        {
            total_page = 0;
        }

        if (page > total_page) page = total_page;
        start = limit * page - limit;
        end = (page * limit) + 1;

        if (sidx.Equals("") || sidx == null)
        {
            sidx = "phn.sochungtu";
        }

        string strsql = "select * from( " +
            " select ctphn.c_chitietphieuhanngach_id, ctphn.sothutu, sp.ma_sanpham, ctphn.ma_sanpham_khach " +
            " , ctphn.sl_canlam, ( select ma_sanpham from md_sanpham where md_sanpham_id = ctphn.ma_sanpham_thaydoi) as ma_sanpham_thaydoi, ctphn.sl_thaydoi, ctphn.gianhap, dtkd.ma_dtkd, ctphn.ghichu, " +
            " ctphn.ngaytao, ctphn.nguoitao, ctphn.ngaycapnhat, ctphn.nguoicapnhat, ctphn.mota, ctphn.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_chitietphieuhanngach ctphn, md_sanpham sp, md_doitackinhdoanh dtkd where ctphn.md_sanpham_id = sp.md_sanpham_id and ctphn.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id and ctphn.c_phieuhanngach_id = '" + c_phieuhanngach_id + "'" +
            ")P WHERE RowNum > @start AND RowNum < @end";

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_chitietphieuhanngach_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sothutu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham_khach"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_canlam"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham_thaydoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_thaydoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["gianhap"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghichu"] + "]]></cell>";
            
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }
    
    public void add(HttpContext context) { }
    
    public void edit(HttpContext context) {
        string id = context.Request.Form["id"];
        
        int sothutu = int.Parse(context.Request.Form["sothutu"]);
        string ma_sanpham_khach = context.Request.Form["ma_sanpham_khach"];
        int sl_thaydoi = int.Parse(context.Request.Form["sl_thaydoi"]);
        string ghichu = context.Request.Form["ghichu"];
        string mota = context.Request.Form["mota"];
        
        c_chitietphieuhanngach cthn = db.c_chitietphieuhanngaches.FirstOrDefault(p => p.c_chitietphieuhanngach_id.Equals(id));
        c_phieuhanngach hn = db.c_phieuhanngaches.FirstOrDefault(p => p.c_phieuhanngach_id.Equals(cthn.c_phieuhanngach_id));
        if (hn.md_trangthai_id.Equals("SOANTHAO"))
        {
            if (cthn != null)
            {
                cthn.sothutu = sothutu;
                cthn.ma_sanpham_khach = ma_sanpham_khach;
                cthn.sl_thaydoi = sl_thaydoi;
                cthn.ghichu = ghichu;
                cthn.mota = mota;

                db.SubmitChanges();
                
            }
        }
    }
    
    public void del(HttpContext context) {
        string id = context.Request.Form["id"];
        c_chitietphieuhanngach cthn = db.c_chitietphieuhanngaches.FirstOrDefault(p => p.c_chitietphieuhanngach_id.Equals(id));
        c_phieuhanngach hn = db.c_phieuhanngaches.FirstOrDefault(p => p.c_phieuhanngach_id.Equals(cthn.c_phieuhanngach_id));
        if (hn.md_trangthai_id.Equals("SOANTHAO"))
        {
            if (cthn != null)
            {
                db.c_chitietphieuhanngaches.DeleteOnSubmit(cthn);
                db.SubmitChanges();
            }
        }
    }

    LinqDBDataContext db = new LinqDBDataContext();
    public bool IsReusable {
        get {
            return false;
        }
    }

}