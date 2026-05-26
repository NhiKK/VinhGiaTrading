<%@ WebHandler Language="C#" Class="Md_bo_chitietController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class Md_bo_chitietController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getoption":
                this.getSelectOption(context);
                break;
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

    public void getSelectOption(HttpContext context)
    {
        String sql = "select md_hangtrongkho_id, qty from md_hangtrongkho  where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_bo_chitiet mnu = db.md_bo_chitiets.FirstOrDefault(s=>s.md_bo_chitiet_id == id);
        mnu.md_bo_detail = context.Request.Form["md_bo_detail"];   
		mnu.mota = context.Request.Form["mota"];
		mnu.hoatdong = true;
		mnu.ngaytao = DateTime.Now;
		mnu.ngaycapnhat = DateTime.Now;
		mnu.nguoitao = UserUtils.getUser(context);
		mnu.nguoicapnhat = UserUtils.getUser(context);
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        md_bo_chitiet mnu = new md_bo_chitiet();
        mnu.md_bo_chitiet_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss"));
        mnu.md_bo_id = context.Request.Form["md_bo_id"];
        mnu.md_bo_detail = context.Request.Form["md_bo_detail"];
            
		mnu.mota = context.Request.Form["mota"];
		mnu.hoatdong = true;
		mnu.ngaytao = DateTime.Now;
		mnu.ngaycapnhat = DateTime.Now;
		mnu.nguoitao = UserUtils.getUser(context);
		mnu.nguoicapnhat = UserUtils.getUser(context);

        db.md_bo_chitiets.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_bo_chitiet mnu = db.md_bo_chitiets.FirstOrDefault(s=>s.md_bo_chitiet_id == id);
		if(mnu != null) {
			db.md_bo_chitiets.DeleteOnSubmit(mnu);
			db.SubmitChanges();
		}
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String cateId = context.Request.QueryString["cateId"];

        String sqlCount = @"SELECT COUNT(*) AS count FROM md_bo bc, md_bo_chitiet bct
            WHERE bc.md_bo_id = bct.md_bo_id
            AND bct.md_bo_id  = N'{0}'";
        
        if (cateId != null)
        {
            sqlCount = string.Format(sqlCount, cateId);
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
            sidx = "bct.md_bo_chitiet_id";
        }

        string strsql = @"select * from( 
            select bct.md_bo_chitiet_id, bct.md_bo_id, bct.md_bo_detail, bct.ngaytao, bct.nguoitao, bct.ngaycapnhat,
            bct.nguoicapnhat, bct.mota, bct.hoatdong, ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + @") as RowNum
            FROM md_bo bc, md_bo_chitiet bct
            WHERE bc.md_bo_id = bct.md_bo_id
            AND bct.md_bo_id  = N'{0}'
            )P WHERE RowNum > @start AND RowNum < @end";

        if (cateId != null)
        {
            strsql = string.Format(strsql, cateId);
        }
        else
        {
            strsql = string.Format(strsql, 0);
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

    public bool IsReusable
    {
        get { return false; }
    }
}
