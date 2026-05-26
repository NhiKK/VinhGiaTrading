<%@ WebHandler Language="C#" Class="DocumentNoController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class DocumentNoController : IHttpHandler
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
        String sql = "select md_sochungtu_id, ten_sochungtu from md_sochungtu where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd;
        hd = false;
        
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_sochungtu m = db.md_sochungtus.Single(p => p.md_sochungtu_id == context.Request.Form["id"]);
        m.ten_sochungtu = context.Request.Form["ten_sochungtu"];
        m.md_loaichungtu_id = context.Request.Form["md_loaichungtu_id"];
        m.buocnhay = int.Parse(context.Request.Form["buocnhay"]);
        m.so_duocgan = decimal.Parse(context.Request.Form["so_duocgan"]);
        m.tiento = context.Request.Form["tiento"];
        m.hauto = context.Request.Form["hauto"];
        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.ngaycapnhat = DateTime.Now;
        m.nguoicapnhat = UserUtils.getUser(context);
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd;
        hd  = false;

        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        
        md_sochungtu mnu = new md_sochungtu
        {
            md_sochungtu_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            ten_sochungtu = context.Request.Form["ten_sochungtu"],
            md_loaichungtu_id = context.Request.Form["md_loaichungtu_id"],
            buocnhay = int.Parse(context.Request.Form["buocnhay"]),
            so_duocgan = decimal.Parse(context.Request.Form["so_duocgan"]),
            tiento = context.Request.Form["tiento"],
            hauto = context.Request.Form["hauto"],
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
            
        };

        db.md_sochungtus.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update md_sochungtu set hoatdong = 0 where md_sochungtu_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count FROM md_sochungtu";
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
            sidx = "sct.ten_sochungtu";
        }

        string strsql = "select * from( " +
            " select sct.md_sochungtu_id, lct.tenchungtu, sct.ten_sochungtu, sct.buocnhay, sct.so_duocgan, sct.tiento, sct.hauto, " + 
            " sct.ngaytao, sct.nguoitao, sct.ngaycapnhat, sct.nguoicapnhat, sct.mota, sct.hoatdong,  " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_sochungtu sct, md_loaichungtu lct " +
            " WHERE sct.md_loaichungtu_id = lct.md_loaichungtu_id " +
            " )P where RowNum > @start AND RowNum < @end";

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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[7].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[9].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
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
