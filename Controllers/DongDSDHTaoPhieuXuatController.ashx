<%@ WebHandler Language="C#" Class="DongDSDHTaoPhieuXuatController" %>

using System;
using System.Web;

public class DongDSDHTaoPhieuXuatController : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        this.load(context);
    }

    public void load(HttpContext context)
    {
        
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String poId = context.Request.QueryString["poId"];

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_dongdsdh ddsdh, c_danhsachdathang dsdh, c_dongdonhang ddh, " +
            " md_sanpham sp, md_doitackinhdoanh dtkd " +
            " WHERE ddsdh.c_danhsachdathang_id = dsdh.c_danhsachdathang_id " +
            " AND ddsdh.c_dongdonhang_id = ddh.c_dongdonhang_id " +
            " AND ddsdh.md_sanpham_id = sp.md_sanpham_id " +
            " AND ddsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " AND ddsdh.c_danhsachdathang_id = N'{0}'";

        if (poId != null)
        {
            sqlCount = string.Format(sqlCount, poId);
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
            sidx = "c_dongdsdh_id";
        }

        string strsql = "select * from( " +
               " select " +
               " dsdh.c_donhang_id, dsdh.c_danhsachdathang_id, ddsdh.c_dongdonhang_id, ddsdh.c_dongdsdh_id, ddsdh.md_sanpham_id, sp.ma_sanpham " +
               " , ddsdh.ma_sanpham_khach, ddsdh.sl_dathang as sl_po, ddsdh.sl_dathang, ddsdh.sl_conlai " + // chu y: chua co cot so luong po
               "     , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
               " FROM " +
               "     c_dongdsdh ddsdh, c_danhsachdathang dsdh " +
               "     , c_dongdonhang ddh, md_sanpham sp " +
               "     , md_doitackinhdoanh dtkd " +
               " WHERE " +
               "     ddsdh.c_danhsachdathang_id = dsdh.c_danhsachdathang_id " +
               "     AND ddsdh.c_dongdonhang_id = ddh.c_dongdonhang_id " +
               "     AND ddsdh.md_sanpham_id = sp.md_sanpham_id " +
               "     AND ddsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
               "     AND ddsdh.c_danhsachdathang_id = N'{0}'" +
               " )P  WHERE RowNum > @start AND RowNum < @end";

        if (poId != null)
        {
            strsql = string.Format(strsql, poId);
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