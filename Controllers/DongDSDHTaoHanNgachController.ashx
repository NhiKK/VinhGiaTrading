<%@ WebHandler Language="C#" Class="DongDSDHTaoHanNgachController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class DongDSDHTaoHanNgachController : IHttpHandler {

    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        this.load(context);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        // filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
		
        String c_danhsachdathang_id = context.Request.QueryString["c_danhsachdathang_id"];

        String sqlCount = "SELECT COUNT(*) AS count from c_dongdsdh ddsdh, md_sanpham sp where ddsdh.md_sanpham_id = sp.md_sanpham_id and ddsdh.c_danhsachdathang_id ='{0}'  " + filter;

        if (c_danhsachdathang_id != null)
        {
            sqlCount = string.Format(sqlCount, c_danhsachdathang_id);
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
            sidx = "ddh.c_dongdonhang_id";
        }

        string strsql = "select * from( " +
            " select " +
            " ddsdh.c_dongdsdh_id, sp.md_sanpham_id, sp.ma_sanpham, ddsdh.ma_sanpham_khach, sl_dathang, sl_conlai " +
            "   , ROW_NUMBER() OVER (ORDER BY "+ sidx +" "+ sord +") as RowNum " +
            "FROM  " +
            "   c_dongdsdh ddsdh, md_sanpham sp " +
            "WHERE  " +
            "   ddsdh.md_sanpham_id = sp.md_sanpham_id " +
            "   and ddsdh.c_danhsachdathang_id = '{0}' " +
			filter +
            ")P " +
            " WHERE RowNum > @start AND RowNum < @end";

        if (c_danhsachdathang_id != null)
        {
            strsql = string.Format(strsql, c_danhsachdathang_id);
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
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}