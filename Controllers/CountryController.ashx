<%@ WebHandler Language="C#" Class="CountryController" %>

using System;
using System.Web;
using System.Data;
using System.Data.Linq;
using System.Linq;

public class CountryController : IHttpHandler
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
        String sql = "select md_quocgia_id, ten_quocgia, md_khuvuc_id from md_quocgia where hoatdong = 1 order by ten_quocgia asc";
        //SelectHtmlControl s = new SelectHtmlControl(sql);
		DataTable dt = mdbc.GetData(sql);
        String str = "<select>";
        foreach (DataRow item in dt.Rows)
        {
            str += string.Format("<option kv=\"{2}\" value=\"{0}\">{1}</option>", item[0], item[1], item[2]);
        }
        str += "</select>";
        context.Response.Write(str);
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_quocgia m = db.md_quocgias.Single(p => p.md_quocgia_id == context.Request.Form["id"]);
        m.ma_quocgia = context.Request.Form["ma_quocgia"];
        m.ten_quocgia = context.Request.Form["ten_quocgia"];
        m.md_khuvuc_id = context.Request.Form["md_khuvuc_id"];
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

        md_quocgia mnu = new md_quocgia
        {
            md_quocgia_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            ma_quocgia = context.Request.Form["ma_quocgia"],
            ten_quocgia = context.Request.Form["ten_quocgia"],
            md_khuvuc_id = context.Request.Form["md_khuvuc_id"],
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_quocgias.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update md_quocgia set hoatdong = 0 where md_quocgia_id IN (" + id + ")";
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
        
        String sqlCount = "SELECT COUNT(*) AS count FROM md_quocgia where 1=1 {0} ";
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
            sidx = "qg.ma_quocgia";
        }

        string strsql = "select * from( " +
            " select qg.md_quocgia_id, qg.ma_quocgia, qg.ten_quocgia, kv.ten_khuvuc, qg.ngaytao, qg.nguoitao, qg.ngaycapnhat, qg.nguoicapnhat, qg.mota, qg.hoatdong " +
            ",  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " from md_quocgia qg left join md_khuvuc kv on qg.md_khuvuc_id = kv.md_khuvuc_id where 1=1 {0} " +
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
            xml += "<cell><![CDATA[" + row["md_quocgia_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_quocgia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_quocgia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_khuvuc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ngaytao"] + "]]></cell>";
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
