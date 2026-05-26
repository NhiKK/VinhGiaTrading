<%@ WebHandler Language="C#" Class="DanhMucHangHoaController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class DanhMucHangHoaController : IHttpHandler
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
                this.getoption(context);
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

    public void getoption(HttpContext context)
    {
        String sql = "select md_danhmuchanghoa_id, ten_danhmuc from md_danhmuchanghoa where hoatdong = 1 order by ma_danhmuc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToOptionNullFirst());
    }

    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String id = context.Request.Form["id"];
        String ma_danhmuc = context.Request.Form["ma_danhmuc"].Trim();
        String ten_danhmuc = context.Request.Form["ten_danhmuc"].Trim();

        var lst = (from o in db.md_danhmuchanghoas
                   where (o.ma_danhmuc.Equals(ma_danhmuc) | o.ten_danhmuc.Equals(ten_danhmuc)) & !o.md_danhmuchanghoa_id.Equals(id)
                   select new { o.md_danhmuchanghoa_id });

        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã có danh mục này!");
        }
        else
        {
            var m = db.md_danhmuchanghoas.Single(p => p.md_danhmuchanghoa_id.Equals(id));
            m.ma_danhmuc = ma_danhmuc;
            m.ten_danhmuc = ten_danhmuc;
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

        String ma_danhmuc = context.Request.Form["ma_danhmuc"].Trim();
        String ten_danhmuc = context.Request.Form["ten_danhmuc"].Trim();

        var lst = (from o in db.md_danhmuchanghoas
                   where (o.ma_danhmuc.Equals(ma_danhmuc) | o.ten_danhmuc.Equals(ten_danhmuc))
                   select new { o.md_danhmuchanghoa_id });

        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã có danh mục này!");
        }
        else
        {
            var m = new md_danhmuchanghoa();
            m.md_danhmuchanghoa_id = Helper.getNewId();
            m.ma_danhmuc = ma_danhmuc;
            m.ten_danhmuc = ten_danhmuc;
            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.nguoitao = UserUtils.getUser(context);
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaytao = DateTime.Now;
            m.ngaycapnhat = DateTime.Now;
            db.md_danhmuchanghoas.InsertOnSubmit(m);
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_danhmuchanghoa where md_danhmuchanghoa_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
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

        String sqlCount = "SELECT COUNT(1) AS count FROM md_danhmuchanghoa dm where 1=1 {0} ";
        sqlCount = string.Format(sqlCount, filter);

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
            sidx = "dm.ma_danhmuc";
        }

        string strsql = @"
            SELECT 
                P.* 
            FROM (
                SELECT 
                    dm.md_danhmuchanghoa_id, 
                    dm.ma_danhmuc, 
                    dm.ten_danhmuc, 
                    dm.ngaytao, 
                    dm.nguoitao, 
                    dm.ngaycapnhat, 
                    dm.nguoicapnhat, 
                    dm.mota, 
                    dm.hoatdong, 
                    ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
                FROM 
                    md_danhmuchanghoa dm 
                WHERE 
                    1=1 {2}
            )P 
            WHERE 
                RowNum > @start AND RowNum < @end
        ";

        strsql = string.Format(strsql, sidx, sord, filter);

        var dt = mdbc.GetData(strsql, "@start", start, "@end", end);
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
