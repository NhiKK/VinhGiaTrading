<%@ WebHandler Language="C#" Class="TrongLuongController" %>
using System;
using System.Web;
using System.Linq;
using System.Data.SqlClient;

public class TrongLuongController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getsearchoption":
                this.getSearchOption(context);
                break;
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
        String sql = "select md_trongluong_id, ten_trongluong from md_trongluong where hoatdong = 1 order by ten_trongluong asc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void getSearchOption(HttpContext context)
    {
        String sql = "select ten_trongluong as id, ten_trongluong from md_trongluong where hoatdong = 1 order by ten_trongluong asc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToOptionSearch());
    }

    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String id = context.Request.Form["id"];
        String ten_trongluong = context.Request.Form["ten_trongluong"];
        String tile = context.Request.Form["tile"];
        String description = context.Request.Form["mota"];


        String getCate = "select ten_trongluong from md_trongluong where md_trongluong_id != @id AND ten_trongluong = @ten_trongluong";
        SqlDataReader rd = mdbc.ExecuteReader(getCate, "@id", id, "@ten_trongluong", ten_trongluong);
        if (rd.HasRows)
        {
            jqGridHelper.Utils.ShowMsg(String.Format("Trọng lượng {0} đã tồn tại trong hệ thống!", ten_trongluong));
        }
        else
        {
            md_trongluong obj = db.md_trongluongs.Single(p => p.md_trongluong_id == id);
            obj.ten_trongluong = ten_trongluong;
            obj.tile = decimal.Parse(tile);
            obj.mota = description;
            obj.hoatdong = hd;
            obj.nguoicapnhat = UserUtils.getUser(context);
            obj.ngaycapnhat = DateTime.Now;
            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String ten_trongluong = context.Request.Form["ten_trongluong"];
        String getCate = "select ten_trongluong from md_trongluong where ten_trongluong = @ten_trongluong";
        SqlDataReader rd = mdbc.ExecuteReader(getCate, "@ten_trongluong", ten_trongluong);
        if (rd.HasRows)
        {
            jqGridHelper.Utils.ShowMsg(String.Format("Trọng lượng {0} đã tồn tại trong hệ thống!", ten_trongluong));
        }
        else
        {
            md_trongluong obj = new md_trongluong
            {
                md_trongluong_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmsstt")),
                ten_trongluong = context.Request.Form["ten_trongluong"],
                tile = decimal.Parse(context.Request.Form["tile"]),
                mota = context.Request.Form["mota"],
                hoatdong = hd,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            db.md_trongluongs.InsertOnSubmit(obj);
            db.SubmitChanges();
        }
    
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update md_trongluong set hoatdong = 0 where md_trongluong_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count from md_trongluong";
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
            sidx = "tl.md_trongluong";
        }

        string strsql = "select * from( select tl.*, ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum from md_trongluong tl)P where RowNum > @start AND RowNum < @end";

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_trongluong_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_trongluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tile"] + "]]></cell>";
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