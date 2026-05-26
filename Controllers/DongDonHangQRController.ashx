<%@ WebHandler Language="C#" Class="DongdonhangqrQRController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class DongdonhangqrQRController : IHttpHandler
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

        String poId = context.Request.QueryString["poId"];
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_dongdonhangqr ddh, c_donhangqr dh, md_sanpham sp, md_donggoi dg, md_doitackinhdoanh dtkd " +
            " WHERE ddh.c_donhangqr_id = dh.c_donhangqr_id " +
            " AND ddh.md_sanpham_id = sp.md_sanpham_id " +
            " AND ddh.md_donggoi_id = dg.md_donggoi_id " +
            "    AND ddh.nhacungungid = dtkd.md_doitackinhdoanh_id " +
            " AND dh.c_donhangqr_id = N'{0}' " + filter;

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
            sidx = "ddh.c_dongdonhangqr_id";
        }

        String strsql = "select * from( " +
            " select " +
            "    ddh.c_dongdonhangqr_id, dh.c_donhangqr_id, sp.trangthai, ddh.sothutu, sp.md_sanpham_id " +
            "    , sp.ma_sanpham, ddh.ma_sanpham_khach " +
            "    , ddh.giafob, ddh.soluong " +
            "    , dg.ten_donggoi, ddh.sl_inner " +
            "    , ddh.l1, ddh.w1, ddh.h1,  ddh.sl_outer, ddh.l2, ddh.w2, ddh.h2 " +
            "    , ddh.sl_cont, ddh.v2, dtkd.ma_dtkd, ddh.ngaytao, ddh.nguoitao " +
            "    , ddh.ngaycapnhat,  ddh.nguoicapnhat, ddh.mota, ddh.hoatdong ,ddh.trangthai as trangthai_ddh, ddh.docquyen" +
            "    , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            "FROM  " +
            "   c_dongdonhangqr ddh " +
			" 	left join c_donhangqr dh on ddh.c_donhangqr_id = dh.c_donhangqr_id " +
			" 	left join md_sanpham sp on ddh.md_sanpham_id = sp.md_sanpham_id " +
			" 	left join md_kieudang kd on kd.md_kieudang_id = sp.md_kieudang_id " +
			" 	left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id " +
			" 	left join md_donggoi dg on ddh.md_donggoi_id = dg.md_donggoi_id " +
			" 	left join md_doitackinhdoanh dtkd on ddh.nhacungungid = dtkd.md_doitackinhdoanh_id " +
            "WHERE  " +
            "   dh.c_donhangqr_id = N'{0}' " + filter +
            ")P " +
            " WHERE RowNum > @start AND RowNum < @end";
        
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
            xml += "<cell><![CDATA[" + row["c_dongdonhangqr_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["c_donhangqr_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai_ddh"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sothutu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_sanpham_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham_khach"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["giafob"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["soluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_donggoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_inner"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["l1"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["w1"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["h1"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_outer"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["l2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["w2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["h2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_cont"] + "]]></cell>";
			xml += "<cell><![CDATA[" + row["v2"] + "]]></cell>";
			xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
			xml += "<cell><![CDATA[" + row["docquyen"] + "]]></cell>";
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
