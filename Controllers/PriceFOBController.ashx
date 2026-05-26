<%@ WebHandler Language="C#" Class="PriceFOBController" %>

using System;
using System.Web;
using System.Linq;

public class PriceFOBController : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        this.load(context);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String productId = context.Request.QueryString["productId"];

        String sqlCount = @"
			SELECT 
				COUNT(1) AS count
            FROM 
				md_sanpham sp, md_banggia bg, md_phienbangia pbg, md_giasanpham gsp, md_dongtien dt
            WHERE 
				gsp.md_phienbangia_id = pbg.md_phienbangia_id
				AND pbg.md_trangthai_id = 'HIEULUC'
				AND pbg.md_banggia_id  = bg.md_banggia_id
				AND bg.md_dongtien_id = dt.md_dongtien_id
				AND gsp.hoatdong = 1
				AND bg.banggiaban = 1
				AND sp.md_sanpham_id = N'{0}'
				AND gsp.md_sanpham_id = N'{0}'
		";

        if (productId != null)
        {
            sqlCount = string.Format(sqlCount, productId);
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
            sidx = "gia";
        }

        string strsql = @"
			select * 
			from(
				select 
					gsp.md_giasanpham_id, pbg.ten_phienbangia, 
					sp.ma_sanpham, gsp.gia, dt.ma_iso, pbg.ngay_hieuluc, 
					gsp.ngaytao, gsp.nguoitao, gsp.ngaycapnhat, 
					gsp.nguoicapnhat, gsp.mota, gsp.hoatdong, 
					ROW_NUMBER() OVER (ORDER BY {1} {2}) as RowNum
				FROM 
					md_sanpham sp, md_banggia bg, md_phienbangia pbg, md_giasanpham gsp, md_dongtien dt
				WHERE 
					gsp.md_phienbangia_id = pbg.md_phienbangia_id
					AND pbg.md_trangthai_id = 'HIEULUC'
					AND pbg.md_banggia_id = bg.md_banggia_id
					AND pbg.md_banggia_id = bg.md_banggia_id
					AND bg.md_dongtien_id = dt.md_dongtien_id
					AND gsp.hoatdong = 1
					AND bg.banggiaban = 1
					AND sp.md_sanpham_id = N'{0}'
					AND gsp.md_sanpham_id = N'{0}'
			)P
            WHERE RowNum > @start AND RowNum < @end
		";

        if (productId != null)
        {
            strsql = string.Format(strsql, productId, sidx, sord);
        }
        else
        {
            strsql = string.Format(strsql, 0, sidx, sord);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[5].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}