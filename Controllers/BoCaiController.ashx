<%@ WebHandler Language="C#" Class="BoCaiController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class BoCaiController : IHttpHandler
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
        String sql = "select md_bocai_id, code_bc from md_bocai where hoatdong = 1 order by code_bc desc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void getSearchOption(HttpContext context)
    {
        String sql = "select code_bc as id, code_bc from md_bocai where hoatdong = 1  order by code_bc asc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToOptionSearch());
    }
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_bocai exist = db.md_bocais.FirstOrDefault(
        p => p.code_bc.Equals(context.Request.Form["code_bc"]) 
            && p.md_bocai_id.Equals(context.Request.Form["id"])
        );
        
        if (exist != null)
        {
            jqGridHelper.Utils.writeResult(0, "Mã bộ cái đã tồn tại!");
        }
        else
        {
            md_bocai m = db.md_bocais.Single(p => p.md_bocai_id == context.Request.Form["id"]);
            m.code_bc = context.Request.Form["code_bc"];
            m.tv_ngan = context.Request.Form["tv_ngan"];
            m.ta_ngan = context.Request.Form["ta_ngan"];
            m.tv_dai = context.Request.Form["tv_dai"];
            m.ta_dai = context.Request.Form["ta_dai"];

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;
            m.nguoicapnhat = UserUtils.getUser(context);

            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_bocai exist = db.md_bocais.FirstOrDefault(p => p.code_bc.Equals(context.Request.Form["code_bc"]));
        if (exist != null)
        {
            jqGridHelper.Utils.writeResult(0, "Mã bộ cái đã tồn tại!");
        }
        else
        {
            md_bocai mnu = new md_bocai
            {
                md_bocai_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                code_bc = context.Request.Form["code_bc"],
                tv_ngan = context.Request.Form["tv_ngan"],
                ta_ngan = context.Request.Form["ta_ngan"],
                tv_dai = context.Request.Form["tv_dai"],
                ta_dai = context.Request.Form["ta_dai"],

                mota = context.Request.Form["mota"],
                hoatdong = hd,
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context)
            };

            db.md_bocais.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_bocai where md_bocai_id IN (" + id + ")";
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

        String sqlCount = "SELECT COUNT(*) AS count FROM md_bocai WHERE 1=1 {0}";
        sqlCount = String.Format(sqlCount, filter);
        
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
            sidx = "bc.md_bocai_id";
        }

        string strsql = "select * from( " +
            " select bc.* " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_bocai bc WHERE 1=1 {0} " +
            " )P where RowNum > @start AND RowNum < @end";
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
            xml += "<cell><![CDATA[" + row[0] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[1] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[2] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[3] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[6].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[8].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
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
