<%@ WebHandler Language="C#" Class="DongPackingListInvoiceController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class DongPackingListInvoiceController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getoption":
                this.getSelectOption(context);
                break;
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

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_dongpklinv_id, mota_tienganh from c_dongpklinv where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_dongpklinv m = db.c_dongpklinvs.Single(p => p.c_dongpklinv_id == context.Request.Form["id"]);
        m.c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"];
        m.c_dongnhapxuat_id = context.Request.Form["c_dongnhapxuat_id"];
        m.md_sanpham_id = context.Request.Form["sanpham_id"];
        m.mota_tienganh = context.Request.Form["mota_tienganh"];
        m.soluong = decimal.Parse(context.Request.Form["soluong"]);
        m.gia = decimal.Parse(context.Request.Form["gia"]);
        m.thanhtien = decimal.Parse(context.Request.Form["soluong"]) * decimal.Parse(context.Request.Form["gia"]);
        m.line = int.Parse(context.Request.Form["line"]);

        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaycapnhat = DateTime.Now;
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        
        c_dongpklinv mnu = new c_dongpklinv
        {
            c_dongpklinv_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"],
            c_dongnhapxuat_id = context.Request.Form["c_dongnhapxuat_id"],
            md_sanpham_id = context.Request.Form["sanpham_id"],
            mota_tienganh = context.Request.Form["mota_tienganh"],
            soluong = decimal.Parse(context.Request.Form["soluong"]),
            gia = decimal.Parse(context.Request.Form["gia"]),
            thanhtien = decimal.Parse(context.Request.Form["soluong"]) * decimal.Parse(context.Request.Form["gia"]),
            line = int.Parse(context.Request.Form["line"]),
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.c_dongpklinvs.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update c_dongpklinv set hoatdong = 0 where c_dongpklinv_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
		//// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
		
        String pklId = context.Request.QueryString["pklId"];
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_dongpklinv dpk, c_packinginvoice pk, md_sanpham sp " +
            " WHERE dpk.c_packinginvoice_id = pk.c_packinginvoice_id " +
            " AND dpk.md_sanpham_id = sp.md_sanpham_id " +
            " AND dpk.c_packinginvoice_id = N'{0}' " + filter;

        if (pklId != null)
        {
            sqlCount = string.Format(sqlCount, pklId);
        }
        else
        {
            sqlCount = string.Format(sqlCount, 0);
        }
        
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
            sidx = "dpk.c_dongpklinv_id";
        }

        string strsql = "select * from( " +
            " select dpk.c_dongpklinv_id, pk.c_packinginvoice_id, " +
            " sp.trangthai, dpk.line, sp.md_sanpham_id, " +
            " sp.ma_sanpham, dpk.mota_tienganh, dpk.ma_sanpham_khach, " +
	        " dpk.soluong, dpk.gia, " +
	        " dpk.thanhtien, dpk.ngaytao, dpk.nguoitao, " +
	        " dpk.ngaycapnhat, dpk.nguoicapnhat, " +
            " dpk.mota, dpk.hoatdong, hs.hscode, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_dongpklinv dpk, c_packinginvoice pk, md_sanpham sp, md_hscode hs " +
            " WHERE dpk.c_packinginvoice_id = pk.c_packinginvoice_id " +
	        " AND dpk.md_sanpham_id = sp.md_sanpham_id " +
	        " AND hs.md_hscode_id = sp.md_hscode_id " +
            " AND dpk.c_packinginvoice_id = N'{0}' " + filter +
            " )P WHERE RowNum > @start AND RowNum < @end";

        if (pklId != null)
        {
            strsql = string.Format(strsql, pklId);
        }
        else
        {
            strsql = string.Format(strsql, 0);
        }
        
        
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row[0] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[1] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[2] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[3] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[11].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[13].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[14] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[15] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[16] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[17] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
