<%@ WebHandler Language="C#" Class="DonHangCoPhieuXuatController" %>

using System;
using System.Web;
using System.Linq;

public class DonHangCoPhieuXuatController : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
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

    LinqDBDataContext db = new LinqDBDataContext();
    
    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        
        bool isadmin = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(context))).isadmin.Value;

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
        
        
        // phân quyền theo nhóm
        String manv = UserUtils.getUser(context);
        String strAccount = "";
        System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
        foreach (String item in lstAccount)
        {
            strAccount += String.Format(", '{0}'", item);
        }
        strAccount = String.Format("'{0}'{1}", manv, strAccount);
        
        
        // String sqlCount = "SELECT COUNT(*) AS count FROM c_donhang dh WHERE 1=1 {0} " + filter;
        String sqlCount = @"select count(*) as count from(
				 SELECT dh.c_donhang_id, dh.sochungtu,
				 (select Count(*) from c_nhapxuat nx
				 left join c_dongnhapxuat dnx on nx.c_nhapxuat_id = dnx.c_nhapxuat_id
				 left join c_dongdonhang ddh on ddh.c_dongdonhang_id = dnx.c_dongdonhang_id 
				 where nx.md_loaichungtu_id = 'XK'  and nx.cr_invoice = 0 and ddh.c_donhang_id = dh.c_donhang_id) as xk 
				 FROM c_donhang dh WHERE 1=1 {0} " + filter + ")P where P.xk > 0";
        sqlCount = String.Format(sqlCount, isadmin == true ? "" : " AND dh.nguoitao IN(" + strAccount + ")");
        
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
            sidx = "dh.c_donhang_id";
        }

        /*string strsql = "select * from(" +
            " SELECT dh.c_donhang_id, dh.sochungtu " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_donhang dh WHERE 1=1 {0} " + filter +
            " )P where RowNum > @start AND RowNum < @end";*/
			
		string strsql = "select * from( " +
				" SELECT dh.c_donhang_id, dh.sochungtu, " +
				" (select Count(*) from c_nhapxuat nx " +
				" left join c_dongnhapxuat dnx on nx.c_nhapxuat_id = dnx.c_nhapxuat_id " +
				" left join c_dongdonhang ddh on ddh.c_dongdonhang_id = dnx.c_dongdonhang_id " +
				" where nx.md_loaichungtu_id = 'XK'  and nx.cr_invoice = 0 and ddh.c_donhang_id = dh.c_donhang_id) as xk " +
				" , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
				" FROM c_donhang dh WHERE 1=1 {0} " + filter +
				" )P where P.xk > 0 and RowNum > @start AND RowNum < @end";
        strsql = String.Format(strsql, isadmin == true ? "" : " AND dh.nguoitao IN(" + strAccount + ")");

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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}