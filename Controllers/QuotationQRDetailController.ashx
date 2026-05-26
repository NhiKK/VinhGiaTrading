<%@ WebHandler Language="C#" Class="QuotationQRDetailController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class QuotationQRDetailController : IHttpHandler
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
        String quotationId = context.Request.QueryString["quotationId"];

        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        bool ghepbo = false;
        c_baogiaqr bg = db.c_baogiaqrs.FirstOrDefault(s=>s.c_baogiaqr_id == quotationId);
        if(bg != null) {
            ghepbo = bg.ghepbo.GetValueOrDefault(false);
        }
        String sqlCount = @"SELECT COUNT(1) AS count 
        FROM c_chitietbaogiaqr ctbg with (nolock) 
		left join c_baogiaqr bg with (nolock) on ctbg.c_baogiaqr_id = bg.c_baogiaqr_id 
		left join md_sanpham sp with (nolock) on ctbg.md_sanpham_id = sp.md_sanpham_id
		left join md_donggoi dg with (nolock) on ctbg.md_donggoi_id = dg.md_donggoi_id 
		left join md_hscode hs with (nolock) on hs.md_hscode_id = sp.md_hscode_id 
		WHERE 1=1 AND ctbg.c_baogiaqr_id = N'{0}' {1}";

        if (quotationId != null)
        {
            sqlCount = string.Format(sqlCount, quotationId, filter);
        }
        else
        {
            sqlCount = string.Format(sqlCount, 0, filter);
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

        string orderby = "";
        if (sidx.Equals("") || sidx == null)
        {
            sidx = "ctbg.c_chitietbaogiaqr_id";
        }

        if(ghepbo == true) {
            orderby = "ORDER BY substring(sp.ma_sanpham,0, 8) asc, substring(sp.ma_sanpham,13, 2) asc, substring(sp.ma_sanpham,10, 2) asc";
        }
        else {
            orderby = "ORDER BY " + sidx + " " + sord;
        }
        string strsql = @"select * from(
            select ctbg.c_chitietbaogiaqr_id, bg.c_baogiaqr_id, sp.trangthai, ctbg.trangthai as trangthai_ctbg, ctbg.sothutu, sp.md_sanpham_id, sp.ma_sanpham, hs.hscode, ctbg.ma_sanpham_khach,
            ctbg.giafob, ctbg.soluong, dg.ten_donggoi, ctbg.sl_inner,
            ctbg.l1, ctbg.w1, ctbg.h1, ctbg.sl_outer, ctbg.l2, ctbg.w2, ctbg.h2, sp.trongluong, ctbg.v2,  ctbg.sl_cont,
            ctbg.ghichu, ctbg.ngaytao, ctbg.nguoitao, ctbg.ngaycapnhat, ctbg.nguoicapnhat, ctbg.mota, ctbg.hoatdong, ctbg.docquyen,
            ROW_NUMBER() OVER ({1}) as RowNum 
            
			FROM c_chitietbaogiaqr ctbg with (nolock) 
			left join c_baogiaqr bg with (nolock) on ctbg.c_baogiaqr_id = bg.c_baogiaqr_id 
			left join md_sanpham sp with (nolock) on ctbg.md_sanpham_id = sp.md_sanpham_id
			left join md_donggoi dg with (nolock) on ctbg.md_donggoi_id = dg.md_donggoi_id 
			left join md_hscode hs with (nolock) on hs.md_hscode_id = sp.md_hscode_id 
            WHERE 1=1 AND ctbg.c_baogiaqr_id = N'{0}' {2}
            )P where RowNum > @start AND RowNum < @end";

        if (quotationId != null)
        {
            strsql = string.Format(strsql, quotationId, orderby, filter);
        }
        else
        {
            strsql = string.Format(strsql, 0, orderby, filter);
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
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[14] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[15] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[16] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[17] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[18] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[19] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[20] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[21] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[22] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[23] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[24].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[25] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[26].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[27] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[28] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[29] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[30] + "]]></cell>";
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
