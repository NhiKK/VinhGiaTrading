<%@ WebHandler Language="C#" Class="BienTauController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class BienTauController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
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
    
    public void edit(HttpContext context)
    {
        
    }

    public void add(HttpContext context)
    {
        
    }


    public void del(HttpContext context)
    {
        
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String md_sanpham_id = context.Request.QueryString["productId"];
        
        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        String sqlCount = "SELECT COUNT(*) AS count FROM md_bientau bt, md_sanpham sp WHERE bt.md_sanpham_id = sp.md_sanpham_id AND bt.md_sanpham_id = N'{0}' {1}";
        sqlCount = String.Format(sqlCount, md_sanpham_id == "" ? "0" : md_sanpham_id, filter);
        
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
            sidx = "bt.md_bientau_id";
        }

        string strsql = "select * from( " +
            " select bt.md_bientau_id, sp.ma_sanpham, bt.ma_sanpham_ref, bt.ngaytao, bt.nguoitao, bt.ngaycapnhat, bt.nguoicapnhat, bt.mota, bt.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_bientau bt, md_sanpham sp WHERE bt.md_sanpham_id = sp.md_sanpham_id AND bt.md_sanpham_id = N'{0}' {1} " +
            " )P where RowNum > @start AND RowNum < @end";
        strsql = string.Format(strsql, md_sanpham_id == "" ? "0" : md_sanpham_id, filter);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[3].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[5].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
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
