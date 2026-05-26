<%@ WebHandler Language="C#" Class="BoPhanController" %>

using System;
using System.Web;
using System.Linq;

public class BoPhanController : IHttpHandler {
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
        String md_bophan_id = context.Request.Form["id"];
        String ten_bophan = context.Request.Form["ten_bophan"];
        String mota = context.Request.Form["mota"];
        String ma_bophan = context.Request.Form["ma_bophan"];

        md_bophan bp = db.md_bophans.FirstOrDefault(s=>s.md_bophan_id == md_bophan_id);
        bp.md_bophan_id = md_bophan_id;
        bp.ten_bophan = ten_bophan;
        bp.ma_bophan = ma_bophan;

        bp.nguoitao = UserUtils.getUser(context);
        bp.ngaytao = DateTime.Now;
        bp.ngaycapnhat = DateTime.Now;
        bp.nguoicapnhat = UserUtils.getUser(context);
        bp.hoatdong = true;
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String md_bophan_id = ImportUtils.getNEWID();
        String ten_bophan = context.Request.Form["ten_bophan"];
        String mota = context.Request.Form["mota"];
		String ma_bophan = context.Request.Form["ma_bophan"];

        md_bophan bp = new md_bophan();
        bp.md_bophan_id = md_bophan_id;
        bp.ten_bophan = ten_bophan;
        bp.ma_bophan = ma_bophan;

        bp.nguoitao = UserUtils.getUser(context);
        bp.ngaytao = DateTime.Now;
        bp.ngaycapnhat = DateTime.Now;
        bp.nguoicapnhat = UserUtils.getUser(context);
        bp.hoatdong = true;
        
        db.md_bophans.InsertOnSubmit(bp);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_bophan bp = db.md_bophans.FirstOrDefault(p => p.md_bophan_id.Equals(id));
		db.md_bophans.DeleteOnSubmit(bp);
		db.SubmitChanges();
		//jqGridHelper.Utils.writeResult(0, "Xóa thành công!");
        
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count from md_bophan ";
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
            sidx = "ma_bophan";
        }

        string strsql = "select * from( select *, ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum from md_bophan)P where RowNum > @start AND RowNum < @end";

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_bophan_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_bophan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_bophan"] + "]]></cell>";

            
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

    public bool IsReusable
    {
        get { return false; }
    }

}