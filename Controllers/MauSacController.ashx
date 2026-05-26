<%@ WebHandler Language="C#" Class="MauSacController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class MauSacController : IHttpHandler
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
        String sql = "select md_mausac_id, code_mau from md_mausac where hoatdong = 1 order by code_mau desc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String parChungLoai_Id = context.Request.Form["md_chungloai_id"];
        String code_cl = db.md_chungloais.Single(cl => cl.md_chungloai_id.Equals(parChungLoai_Id)).code_cl;

        String parDeTai_Id = context.Request.Form["md_detai_id"];
        String code_dt = db.md_detais.Single(dt => dt.md_detai_id.Equals(parDeTai_Id)).code_dt;
        

        md_mausac m = db.md_mausacs.Single(p => p.md_mausac_id == context.Request.Form["id"]);
        m.md_mausac_id = context.Request.Form["md_mausac_id"];
        m.md_chungloai_id = parChungLoai_Id;
        m.md_detai_id = parDeTai_Id;
        m.code_mau = context.Request.Form["code_mau"];
        m.code_cl = code_cl;
        m.code_dt = code_dt;
        m.tv_ngan = context.Request.Form["tv_ngan"];
        m.ta_ngan = context.Request.Form["ta_ngan"];
        m.tv_dai = context.Request.Form["tv_dai"];
        m.ta_dai = context.Request.Form["ta_dai"];
        
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

        String parChungLoai_Id = context.Request.Form["md_chungloai_id"];
        String code_cl = db.md_chungloais.Single(cl => cl.md_chungloai_id.Equals(parChungLoai_Id)).code_cl;

        String parDeTai_Id = context.Request.Form["md_detai_id"];
        String code_dt = db.md_detais.Single(dt=>dt.md_detai_id.Equals(parDeTai_Id)).code_dt;
        

        md_mausac mnu = new md_mausac
        {
            md_mausac_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            md_chungloai_id = parChungLoai_Id,
            md_detai_id = parDeTai_Id,
            code_mau = context.Request.Form["code_mau"],
            code_cl = code_cl,
            code_dt = code_dt,
            tv_ngan = context.Request.Form["tv_ngan"],
            ta_ngan = context.Request.Form["ta_ngan"],
            tv_dai = context.Request.Form["tv_dai"],
            ta_dai = context.Request.Form["ta_dai"],
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_mausacs.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_mausac where md_mausac_id IN (" + id + ")";
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
        
        String md_detai_id = context.Request.QueryString["DeTaiId"];
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM md_mausac ms, md_chungloai cl, md_detai dt " +
            " WHERE ms.md_chungloai_id = cl.md_chungloai_id " +
            " AND ms.md_detai_id = dt.md_detai_id " +
            " AND ms.md_detai_id = N'{0}' " + filter;

        if (md_detai_id != null)
        {
            sqlCount = String.Format(sqlCount, md_detai_id);
        }
        else {
            sqlCount = String.Format(sqlCount, 0);
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
            sidx = "ms.md_mausac_id";
        }

        string strsql = "select * from( " +
            " select ms.md_mausac_id, cl.md_chungloai_id, dt.md_detai_id, ms.code_mau " +
            " , ms.tv_ngan, ms.ta_ngan, ms.tv_dai, ms.ta_dai " +
            " , ms.ngaytao, ms.nguoitao, ms.ngaycapnhat, ms.nguoicapnhat, ms.mota, ms.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_mausac ms, md_chungloai cl, md_detai dt " +
            " WHERE ms.md_chungloai_id = cl.md_chungloai_id " +
            " AND ms.md_detai_id = dt.md_detai_id " +
            " AND ms.md_detai_id = N'{0}' " + filter +
            " )P where RowNum > @start AND RowNum < @end";

        if (md_detai_id != null)
        {
            strsql = String.Format(strsql, md_detai_id);
        }
        else
        {
            strsql = String.Format(strsql, 0);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[8].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[10].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
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
