<%@ WebHandler Language="C#" Class="ColorReferencesController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ColorReferencesController : IHttpHandler
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
        String sql = "select md_color_reference_id, md_color_reference_id from md_color_referencs";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_color_reference m = db.md_color_references.Single(p => p.md_color_reference_id == context.Request.Form["id"]);
        //m.c_baogia_id = context.Request.Form["c_baogia_id"];
        m.mau = context.Request.Form["mau"];
        m.url = context.Request.Form["url"];
        m.filter = context.Request.Form["filter"];
        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.ngaycapnhat = DateTime.Now;
        m.nguoicapnhat = UserUtils.getUser(context);
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        
        md_color_reference mnu = new md_color_reference
        {
            md_color_reference_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            c_baogia_id = context.Request.Form["c_baogia_id"],
            mau = context.Request.Form["mau"],
            url = context.Request.Form["url"],
            filter = context.Request.Form["filter"],
            
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_color_references.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update md_color_reference set hoatdong = 0 where md_color_reference_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        String quotationId = context.Request.QueryString["quotationId"];
        
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
        
        String sqlCount = @"SELECT COUNT(1) AS count 
            FROM md_color_reference color with (nolock)
			Left join c_baogia bg with (nolock) on color.c_baogia_id = bg.c_baogia_id
            WHERE color.c_baogia_id = N'{0}' {1} ";

        if (quotationId != null)
        {
            sqlCount = String.Format(sqlCount, quotationId, filter);
        }
        else {
            sqlCount = String.Format(sqlCount, 0, filter);
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
            sidx = "color.md_color_reference_id";
        }

        string strsql = @"select * from( 
            select color.md_color_reference_id, bg.c_baogia_id, color.mau, color.url, color.filter 
             , color.ngaytao, color.nguoitao, color.ngaycapnhat 
             , color.nguoicapnhat, color.mota, color.hoatdong 
             , ROW_NUMBER() OVER (ORDER BY {2} {3}) as RowNum 
             FROM md_color_reference color with (nolock) 
			 Left join c_baogia bg with (nolock) on bg.c_baogia_id = color.c_baogia_id 
             WHERE color.c_baogia_id = N'{0}' {1} 
             )P where RowNum > @start AND RowNum < @end ";
        
        if (quotationId != null)
        {
            strsql = String.Format(strsql, quotationId, filter, sidx, sord);
        }
        else
        {
            strsql = String.Format(strsql, 0, filter, sidx, sord);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[5].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[7].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
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
