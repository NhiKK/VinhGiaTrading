<%@ WebHandler Language="C#" Class="XuatKhoByPOController" %>

using System;
using System.Web;

public class XuatKhoByPOController : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        this.load(context);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String poId = context.Request.QueryString["poId"];
        
        /*String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_nhapxuat nx " +
			" Left join c_dongnhapxuat dnx on dnx.c_nhapxuat_id = nx.c_nhapxuat_id " +
			" Left join c_dongdonhang ddh on ddh.c_dongdonhang_id = dnx.c_dongdonhang_id " +
			" Left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id  " +
			" Left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " Left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE nx.md_loaichungtu_id = N'XK' "+
            " AND nx.md_trangthai_id='HIEULUC' AND (nx.cr_invoice is null or nx.cr_invoice <> 1) AND dh.c_donhang_id = N'{0}' ";*/
			
		string sqlCount = "SELECT Count(*) as count from ( select distinct P.* from( " +
            " select nx.c_nhapxuat_id, dh.sochungtu, nx.sophieu, nx.sophieunx " +
            "   , nx.ngay_giaonhan, dtkd.ten_dtkd, nx.nguoigiao, nx.nguoinhan " +
            "   , nx.sophieukhach, nx.ngay_phieu, kho.ten_kho " +
            "   , nx.soseal, nx.socontainer, nx.loaicont, nx.md_trangthai_id " +
            "   , nx.md_loaichungtu_id, nx.ngaytao, nx.nguoitao, nx.ngaycapnhat, nx.nguoicapnhat " +
            "   , nx.mota, nx.hoatdong " +
						" FROM c_nhapxuat nx " +
			" Left join c_dongnhapxuat dnx on dnx.c_nhapxuat_id = nx.c_nhapxuat_id " +
			" Left join c_dongdonhang ddh on ddh.c_dongdonhang_id = dnx.c_dongdonhang_id " +
			" Left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id  " +
			" Left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " Left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE nx.md_loaichungtu_id = N'XK' AND nx.md_trangthai_id='HIEULUC' " +
            "   AND (nx.cr_invoice is null or nx.cr_invoice <> 1) AND dh.c_donhang_id = N'{0}' " +
            " )P )A ";
			
        if (poId != null)
        {
            sqlCount = String.Format(sqlCount, poId);
        }
        else {
            sqlCount = String.Format(sqlCount, 0);
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
            sidx = "nx.c_nhapxuat_id";
        }
		else
		{
			sidx = "nx." + sidx;
		}

        string strsql = "SELECT B.* from ( SELECT A.*, (ROW_NUMBER() OVER (ORDER BY A.c_nhapxuat_id DESC)) AS RowNum from ( select distinct P.* from( " +
            " select nx.c_nhapxuat_id, dh.sochungtu, nx.sophieu, nx.sophieunx " +
            "   , nx.ngay_giaonhan, dtkd.ten_dtkd, nx.nguoigiao, nx.nguoinhan " +
            "   , nx.sophieukhach, nx.ngay_phieu, kho.ten_kho " +
            "   , nx.soseal, nx.socontainer, nx.loaicont, nx.md_trangthai_id " +
            "   , nx.md_loaichungtu_id, nx.ngaytao, nx.nguoitao, nx.ngaycapnhat, nx.nguoicapnhat " +
            "   , nx.mota, nx.hoatdong " +
						" FROM c_nhapxuat nx " +
			" Left join c_dongnhapxuat dnx on dnx.c_nhapxuat_id = nx.c_nhapxuat_id " +
			" Left join c_dongdonhang ddh on ddh.c_dongdonhang_id = dnx.c_dongdonhang_id " +
			" Left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id  " +
			" Left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " Left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE nx.md_loaichungtu_id = N'XK' AND nx.md_trangthai_id='HIEULUC' " +
            "   AND (nx.cr_invoice is null or nx.cr_invoice <> 1) AND dh.c_donhang_id = N'{0}' " +
            " )P )A )B where B.RowNum > @start AND B.RowNum < @end";

        if (poId != null)
        {
            strsql = String.Format(strsql, poId);
        }
        else
        {
            strsql = String.Format(strsql, 0);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[4].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[9].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[14] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[15] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[16].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[17] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[18].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[19] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[20] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[21] + "]]></cell>";
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