<%@ WebHandler Language="C#" Class="XuatKhoByPartnerController" %>

using System;
using System.Web;

public class XuatKhoByPartnerController : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        this.load(context);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String partnerId = context.Request.QueryString["partnerId"];


        // phân quyền theo nhóm
        String manv = UserUtils.getUser(context);
        String strAccount = "";
        System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
        foreach (String item in lstAccount)
        {
            strAccount += String.Format(", '{0}'", item);
        }
        strAccount = String.Format("'{0}'{1}", manv, strAccount);
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_nhapxuat nx " +
			" left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE nx.md_loaichungtu_id = N'XK'"+
            " AND nx.md_trangthai_id='HIEULUC' AND (nx.cr_invoice <>1 or nx.cr_invoice is null) AND nx.md_doitackinhdoanh_id = N'{0}' ";

        if (partnerId != null)
        {
            sqlCount = String.Format(sqlCount, partnerId);
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

        string strsql = "select * from( " +
            " select nx.c_nhapxuat_id " +
            " , isnull((select dh.sochungtu from c_donhang dh where dh.c_donhang_id = nx.c_donhang_id),'***') as ct_dh " +
            " , nx.sophieu, nx.sophieunx " +
            "   , nx.ngay_giaonhan, dtkd.ten_dtkd, nx.nguoigiao, nx.nguoinhan " +
            "   , nx.sophieukhach, nx.ngay_phieu, kho.ten_kho " +
            "   , nx.soseal, nx.container, nx.loaicont, nx.md_trangthai_id " +
            "   , nx.md_loaichungtu_id, nx.ngaytao, nx.nguoitao, nx.ngaycapnhat, nx.nguoicapnhat " +
            "   , nx.mota, nx.hoatdong " +
            "   , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
			" FROM c_nhapxuat nx " +
			" left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE nx.md_loaichungtu_id = N'XK' AND nx.md_trangthai_id='HIEULUC' " +
            "   AND (nx.cr_invoice <>1 or nx.cr_invoice is null) AND nx.md_doitackinhdoanh_id = N'{0}' " +
            " )P where RowNum > @start AND RowNum < @end";

        if (partnerId != null)
        {
            strsql = String.Format(strsql, partnerId);
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