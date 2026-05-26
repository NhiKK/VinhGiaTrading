<%@ WebHandler Language="C#" Class="KhachHangCoPhieuXuatController" %>

using System;
using System.Web;

public class KhachHangCoPhieuXuatController : IHttpHandler {

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
                    default:
                        this.load(context);
                        break;
                }
                break;
        }
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
        
        String sqlCount = "SELECT COUNT(*) AS count FROM md_doitackinhdoanh dtkd Left join md_loaidtkd ldt on ldt.md_loaidtkd_id = dtkd.md_loaidtkd_id where dtkd.isncc = 0 and ldt.ma_loaidtkd != 'NHH' and dtkd.ma_dtkd != 'ESC' " + filter;
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
            sidx = "dtkd.ma_dtkd";
        }

        string strsql = "select * from(" +
            " SELECT dtkd.md_doitackinhdoanh_id, dtkd.ma_dtkd " +
			" , (select count(*) from c_nhapxuat where md_loaichungtu_id = 'XK'  and cr_invoice = 0 and md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id) as xk " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_doitackinhdoanh dtkd " +
            " Left join md_loaidtkd ldt on ldt.md_loaidtkd_id = dtkd.md_loaidtkd_id " +
			" where dtkd.isncc = 0 and ldt.ma_loaidtkd != 'NHH' and dtkd.ma_dtkd != 'ESC' " + filter +
            " )P where P.xk > 0 and RowNum > @start AND RowNum < @end";

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
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }


    #region IHttpHandler Members

    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }

    #endregion
}