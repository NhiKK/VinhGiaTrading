<%@ WebHandler Language="C#" Class="QuyCachDongGoiController" %>

using System;
using System.Web;
using System.Linq;

public class QuyCachDongGoiController : IHttpHandler {
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        this.load(context);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String productId = context.Request.QueryString["productId"];

        String sqlCount = @"
            SELECT COUNT(1) AS count
            FROM md_sanpham sp, md_donggoisanpham dgsp, md_donggoi dg
            WHERE sp.md_sanpham_id = dgsp.md_sanpham_id
            AND dgsp.md_donggoi_id = dg.md_donggoi_id
            AND dgsp.md_sanpham_id = N'{0}'";

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
            sidx = "dgsp.md_donggoisanpham_id";
        }

        string strsql = string.Format(@"
        select * from(
            select 
                dgsp.md_donggoisanpham_id, 
                dg.ma_donggoi,
                dg.ten_donggoi, 
                sp.ma_sanpham, 
                dg.sl_inner, 
                (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner) as dvtinner, 
                dg.l1, 
                dg.w1, 
                dg.h1,
                dg.sl_outer, 
                (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer) as dvtouter, 
                dg.l2_mix, 
                dg.w2_mix, 
                dg.h2_mix,
                dg.v2, 
                dg.sl_cont_mix, 
                dg.soluonggoi_ctn_20,
                dg.soluonggoi_ctn,
                dg.soluonggoi_ctn_40hc,
                dg.nw1, 
                dg.gw1,
                dg.nw2,
                dg.gw2,
                dg.vtdg2,
                dg.cpdg_vuotchuan,
                dg.vd, 
                dg.vn, 
                dg.vl, 
                dg.ghichu_vachngan,
                dg.mix_chophepsudung, 
                dg.ngayxacnhan, 
                dg.mota as gcdg, 
                dg.doigia_donggoi,
                dgsp.macdinh, 
                dgsp.ngaytao, 
                dgsp.nguoitao, 
                dgsp.ngaycapnhat, 
                dgsp.nguoicapnhat, 
                dgsp.mota, 
                dgsp.hoatdong,
                ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
            FROM 
                md_sanpham sp, md_donggoisanpham dgsp, md_donggoi dg
            WHERE 
                sp.md_sanpham_id = dgsp.md_sanpham_id
                AND dgsp.md_donggoi_id = dg.md_donggoi_id
                AND dgsp.md_sanpham_id = N'{2}'
                AND dg.hoatdong = 1
        )P where RowNum > @start AND RowNum < @end",
        sidx,
        sord,
        productId == null ? "0" : productId
        );

        var dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            foreach (System.Data.DataColumn column in dt.Columns)
            {
                if (column.DataType == System.Type.GetType("System.DateTime"))
                {
                    try
                    {
                        xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", ((DateTime)row[column.ColumnName]).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", "");
                    }
                }
                else
                    xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", row[column.ColumnName]);
            }
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
