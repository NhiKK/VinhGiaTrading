<%@ WebHandler Language="C#" Class="NamTaiChinhController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class NamTaiChinhController : IHttpHandler
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
        String sql = "select a_namtaichinh_id, ten_namtaichinh from a_namtaichinh where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        int nam = int.Parse(context.Request.Form["nam"]);
        String yearId =context.Request.Form["id"];
        String getYear = "select COUNT(nam) as count from a_namtaichinh where nam = @nam AND a_namtaichinh_id != @id";
        int count = (int)mdbc.ExecuteScalar(getYear, "@nam", nam, "@id", yearId);
        if (count > 0)
        {
            jqGridHelper.Utils.ShowMsg("Năm đã tồn tại.!");
        }
        else
        {
            a_namtaichinh m = db.a_namtaichinhs.Single(p => p.a_namtaichinh_id == yearId );
            m.ten_namtaichinh = context.Request.Form["ten_namtaichinh"];
            m.nam = int.Parse(context.Request.Form["nam"]);
            m.mota = context.Request.Form["mota"];
            
            m.hoatdong = hd;
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaycapnhat = DateTime.Now;

            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        
        int nam = int.Parse(context.Request.Form["nam"]);
        
        String getYear = "select COUNT(nam) as count from a_namtaichinh where nam = @nam";
        int count = (int)mdbc.ExecuteScalar(getYear, "@nam", nam);
        if (count > 0)
        {
            jqGridHelper.Utils.ShowMsg("Năm đã tồn tại.!");
        }
        else
        {
            a_namtaichinh mnu = new a_namtaichinh
            {
                a_namtaichinh_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                ten_namtaichinh = context.Request.Form["ten_namtaichinh"],
                nam = int.Parse(context.Request.Form["nam"]),
                mota = context.Request.Form["mota"],
                hoatdong = hd,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            db.a_namtaichinhs.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update a_namtaichinh set hoatdong = 0 where a_namtaichinh_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count FROM a_namtaichinh";
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
            sidx = "ntc.ten_namtaichinh";
        }

        string strsql = "select * from( select ntc.a_namtaichinh_id, ntc.ten_namtaichinh, ntc.nam, ntc.ngaytao, ntc.nguoitao, ntc.ngaycapnhat, ntc.nguoicapnhat, ntc.mota, ntc.hoatdong,  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum from a_namtaichinh ntc)P where RowNum > @start AND RowNum < @end";

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
