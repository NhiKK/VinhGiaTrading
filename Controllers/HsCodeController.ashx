<%@ WebHandler Language="C#" Class="HsCodeController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class HsCodeController : IHttpHandler
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
        String sql = "select md_hscode_id, ma_hscode from md_hscode where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_hscode m = db.md_hscodes.Single(p => p.md_hscode_id == context.Request.Form["id"]);
        m.ma_hscode = context.Request.Form["ma_hscode"];
        m.hscode = context.Request.Form["hscode"];
        m.hehang = context.Request.Form["hehang"];
		m.tenhang_tv = context.Request.Form["tenhang_tv"];
		m.tenhang_ta = context.Request.Form["tenhang_ta"];
		m.thanhphan = context.Request.Form["thanhphan"];
		m.nhacungung = context.Request.Form["nhacungung"];
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
        
        md_hscode mnu = new md_hscode
        {
            md_hscode_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            ma_hscode = context.Request.Form["ma_hscode"],
            hscode = context.Request.Form["hscode"],
			hehang = context.Request.Form["hehang"],
			tenhang_tv = context.Request.Form["tenhang_tv"],
            tenhang_ta = context.Request.Form["tenhang_ta"],
			thanhphan = context.Request.Form["thanhphan"],
			nhacungung = context.Request.Form["nhacungung"],
		
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_hscodes.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_hscode where md_hscode_id IN (" + id + ")";
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

        String sqlCount = "SELECT COUNT(*) AS count FROM md_hscode where 1=1 {0}";
        sqlCount = string.Format(sqlCount, filter);
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
            sidx = "hehang asc, ma_hscode asc, hscode asc";
        }
		else {
			sidx = sidx + " " + sord;
		}

        string strsql = "select * from( " +
            " select md_hscode_id, ma_hscode, hscode, hehang, tenhang_tv, tenhang_ta, thanhphan, nhacungung, ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, mota, hoatdong" +
            ",  ROW_NUMBER() OVER (ORDER BY " + sidx + ") as RowNum "+
            " from md_hscode where 1=1 {0}" +
            ")P where RowNum > @start AND RowNum < @end";
        strsql = string.Format(strsql, filter);
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_hscode_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_hscode"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hscode"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hehang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenhang_tv"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenhang_ta"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["thanhphan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nhacungung"] + "]]></cell>";
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

    public bool IsReusable
    {
        get { return false; }
    }
}
