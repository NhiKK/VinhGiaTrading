<%@ WebHandler Language="C#" Class="DongDonHangChiaTachController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class DongDonHangChiaTachController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

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
            " FROM c_dongdonhang ddh, c_donhang dh, md_sanpham sp " +
            " WHERE ddh.c_donhang_id = dh.c_donhang_id " +
            " AND ddh.md_sanpham_id = sp.md_sanpham_id " +
            " AND dh.c_donhang_id = N'{0}' AND ddh.soluong_conlai > 0 ";

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
            sidx = "ddh.c_dongdonhang_id";
        }

        string strsql = "select * from( " +
            " select "+
            "    ddh.c_dongdonhang_id, ddh.c_donhang_id, sp.md_sanpham_id " +
            "    , sp.ma_sanpham, ddh.ma_sanpham_khach, (select ma_dtkd from md_doitackinhdoanh dtkd where dtkd.md_doitackinhdoanh_id = ddh.nhacungungid) md_dtkd_id " +
            "    , isnull(ddh.soluong, 1) sl, isnull(ddh.soluong_dathang, 0) as sl_dh, isnull(ddh.soluong_conlai, 0) as sl_cl " +
            "    , ROW_NUMBER() OVER (ORDER BY (select ma_dtkd from md_doitackinhdoanh dtkd where dtkd.md_doitackinhdoanh_id = sp.nhacungung) asc ) as RowNum " +
            @" FROM c_dongdonhang ddh
			left join c_donhang dh on ddh.c_donhang_id = dh.c_donhang_id 
			left join md_sanpham sp on ddh.md_sanpham_id = sp.md_sanpham_id 
            WHERE 1=1
				AND dh.c_donhang_id = N'{0}'  
				AND ddh.soluong_conlai > 0 
            )P
            WHERE RowNum > @start AND RowNum < @end";
        
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
