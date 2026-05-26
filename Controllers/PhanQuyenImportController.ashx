<%@ WebHandler Language="C#" Class="PhanQuyenImportController" %>

using System;
using System.Web;
using System.Linq;

public class PhanQuyenImportController : IHttpHandler
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
                    case "del":
                        this.del(context);
                        break;
                    case "edit":
                        this.edit(context);
                        break;
                    case "add":
                        this.add(context);
                        break;
                    default:
                        this.load(context);
                        break;
                }
                break;
        }
    }

    public void edit(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_phanquyenimport pqbc = db.md_phanquyenimports.FirstOrDefault(p => p.md_phanquyenimport_id.Equals(id));
        if (pqbc != null)
        {
            pqbc.manv = context.Request.Form["manv"];
            pqbc.ngaycapnhat = DateTime.Now;
            pqbc.nguoicapnhat = UserUtils.getUser(context);
            pqbc.mota = context.Request.Form["mota"];
            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
        md_phanquyenimport pqbc = new md_phanquyenimport();
        pqbc.md_import_id = context.Request.Form["md_import_id"];
        pqbc.md_phanquyenimport_id = ImportUtils.getNEWID();
        pqbc.manv = context.Request.Form["manv"];
        pqbc.ngaytao = DateTime.Now;
        pqbc.nguoitao = UserUtils.getUser(context);
        pqbc.ngaycapnhat = DateTime.Now;
        pqbc.nguoicapnhat = UserUtils.getUser(context);
        pqbc.mota = context.Request.Form["mota"];

        db.md_phanquyenimports.InsertOnSubmit(pqbc);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_phanquyenimport pqbc = db.md_phanquyenimports.FirstOrDefault(p => p.md_phanquyenimport_id.Equals(id));
        if (pqbc != null)
        {
            db.md_phanquyenimports.DeleteOnSubmit(pqbc);
            db.SubmitChanges();
        }
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String md_import_id = context.Request.QueryString["md_import_id"];

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        String sqlCount = "SELECT COUNT(*) AS count FROM md_import bc, md_phanquyenimport pqbc where bc.md_import_id = pqbc.md_import_id AND pqbc.md_import_id = @md_import_id " + filter;
        

        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        String sidx = context.Request.QueryString["sidx"];
        String sord = context.Request.QueryString["sord"];
        int total_page;
        int count = (int)mdbc.ExecuteScalar(sqlCount, "@md_import_id", md_import_id == null ? "0" : md_import_id);
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
            sidx = "pqbc.manv";
        }

       string strsql = "select * from( " +
            " select pqbc.md_phanquyenimport_id, bc.ten_import, pqbc.manv, pqbc.ngaytao, pqbc.nguoitao, " +
            " pqbc.ngaycapnhat, pqbc.nguoicapnhat, pqbc.mota, pqbc.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_import bc, md_phanquyenimport pqbc " +
            " WHERE bc.md_import_id = pqbc.md_import_id " +
            " AND bc.md_import_id = @md_import_id" +
            filter +
            " )P where RowNum > @start AND RowNum < @end";

        System.Data.DataTable dt = mdbc.GetData(strsql, "@md_import_id", md_import_id == null ? "0" : md_import_id, "@start", start, "@end", end);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[3].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[5].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
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