<%@ WebHandler Language="C#" Class="DanhSachDatHangTaoHanNgachController" %>

using System;
using System.Web;
using System.Linq;
using System.Data.Linq;

public class DanhSachDatHangTaoHanNgachController : IHttpHandler {

    LinqDBDataContext db = new LinqDBDataContext();
    
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


        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_danhsachdathang dsdh, c_donhang dh " +
            " WHERE dsdh.c_donhang_id = dh.c_donhang_id {0} AND dsdh.md_trangthai_id = 'HIEULUC' " + filter;

        sqlCount = String.Format(sqlCount, isadmin == true ? "" : "AND dsdh.nguoitao IN(" + strAccount + ") ");

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
            sidx = "dsdh.c_danhsachdathang_id";
        }

        string strsql = "select * from( " +
                " select dsdh.c_danhsachdathang_id, dsdh.sochungtu sodsdh, dsdh.ngaylap, " +
                " dsdh.hangiaohang_po, dsdh.nguoi_phutrach, dsdh.nguoi_dathang, " +
                " dh.sochungtu as sodh, dsdh.so_po, dsdh.md_trangthai_id, dsdh.huongdanlamhang, dsdh.ngaytao, " +
                " dsdh.nguoitao, dsdh.ngaycapnhat, dsdh.nguoicapnhat, dsdh.mota, dsdh.hoatdong, " +
                " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
                " FROM c_danhsachdathang dsdh, c_donhang dh " +
                " WHERE dsdh.c_donhang_id = dh.c_donhang_id {0} AND dsdh.md_trangthai_id = 'HIEULUC' " + filter +
                ")P WHERE RowNum > @start AND RowNum < @end";

        strsql = String.Format(strsql, isadmin == true ? "" : "AND dsdh.nguoitao IN(" + strAccount + ") ");

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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[2].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[3].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[10].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[12].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[14] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[15] + "]]></cell>";
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