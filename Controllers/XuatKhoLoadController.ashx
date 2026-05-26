<%@ WebHandler Language="C#" Class="XuatKhoLoadController" %>

using System;
using System.Web;
using System.Linq;

public class XuatKhoLoadController : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        this.load(context);
    }
    
    LinqDBDataContext db = new LinqDBDataContext();
    
    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";


        bool isadmin = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(context))).isadmin.Value;
        
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
            " FROM c_nhapxuat nx, c_donhang dh, md_doitackinhdoanh dtkd " +
            " , md_kho kho " +
            " WHERE nx.c_donhang_id = dh.c_donhang_id " +
            " {0} "+
            " AND nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " AND nx.md_kho_id = kho.md_kho_id AND nx.md_loaichungtu_id = N'XK' ";
        sqlCount = String.Format(sqlCount, isadmin == true ? "" : " AND nx.nguoitao IN(" + strAccount + ") ");
        
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
            " select nx.c_nhapxuat_id, dh.sochungtu, nx.sophieu, nx.sophieunx " +
            "   , nx.ngay_giaonhan, dtkd.ten_dtkd, nx.nguoigiao, nx.nguoinhan " +
            "   , nx.sophieukhach, nx.ngay_phieu, kho.ten_kho " +
            "   , nx.soseal, nx.socontainer, nx.loaicont, nx.md_trangthai_id " +
            "   , nx.md_loaichungtu_id, nx.ngaytao, nx.nguoitao, nx.ngaycapnhat, nx.nguoicapnhat " +
            "   , nx.mota, nx.hoatdong " +
            "   , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_nhapxuat nx, c_donhang dh, md_doitackinhdoanh dtkd, md_kho kho " +
            " WHERE nx.c_donhang_id = dh.c_donhang_id " +
            " {0}"+
            "   AND nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            "   AND nx.md_kho_id = kho.md_kho_id AND nx.md_loaichungtu_id = N'XK' " +
            " )P where RowNum > @start AND RowNum < @end";

        strsql = String.Format(strsql, isadmin == true ? "" : "AND nx.nguoitao IN(" + strAccount + ") ");
        
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