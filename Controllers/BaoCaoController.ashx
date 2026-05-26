<%@ WebHandler Language="C#" Class="BaoCaoController" %>

using System;
using System.Web;
using System.Linq;

public class BaoCaoController : IHttpHandler {

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
		string h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        String id = context.Request.Form["id"];
        md_baocao bc = db.md_baocaos.FirstOrDefault(p => p.md_baocao_id.Equals(id));
        if (bc != null)
        {
            bc.md_module_id = context.Request.Form["md_module_id"];
            bc.ten_baocao = context.Request.Form["ten_baocao"];
            bc.hanhdong = context.Request.Form["hanhdong"];
            bc.ngaycapnhat = DateTime.Now;
            bc.nguoicapnhat = UserUtils.getUser(context);
            bc.mota = context.Request.Form["mota"];
			bc.hoatdong = hd;
            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
		string h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
	
        md_baocao bc = new md_baocao();
        bc.md_baocao_id = ImportUtils.getNEWID();
        bc.md_module_id = context.Request.Form["md_module_id"];
        bc.ten_baocao = context.Request.Form["ten_baocao"];
        bc.hanhdong = context.Request.Form["hanhdong"];
        bc.ngaytao = DateTime.Now;
        bc.nguoitao = UserUtils.getUser(context);
        bc.ngaycapnhat = DateTime.Now;
        bc.nguoicapnhat = UserUtils.getUser(context);
        bc.mota = context.Request.Form["mota"];
        bc.hoatdong = hd;
        db.md_baocaos.InsertOnSubmit(bc);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_baocao bc = db.md_baocaos.FirstOrDefault(p => p.md_baocao_id.Equals(id));
        if (bc!=null)
        {
            var pqs = from p in db.md_phanquyenbaocaos where p.md_baocao_id.Equals(bc.md_baocao_id) select p;
            db.md_phanquyenbaocaos.DeleteAllOnSubmit(pqs);
            db.md_baocaos.DeleteOnSubmit(bc);
            db.SubmitChanges();
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

        String sqlCount = "SELECT COUNT(*) AS count FROM md_baocao bc where 1=1 " + filter;

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
            sidx = "bc.ten_baocao";
        }

        string strsql = "select * from( " +
            " select * " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_baocao bc where 1=1 " + filter +
            " )P where RowNum > @start AND RowNum < @end";
        
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_baocao_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_module_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_baocao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hanhdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
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