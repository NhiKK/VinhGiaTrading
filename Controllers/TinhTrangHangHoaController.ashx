<%@ WebHandler Language="C#" Class="TinhTrangHangHoaController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class TinhTrangHangHoaController : IHttpHandler
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
        String sql = "select md_tinhtranghanghoa_id, ten_tinhtrang from md_tinhtranghanghoa where hoatdong = 1 order by ma_tinhtrang";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToOptionNullFirst());
    }

    public void edit(HttpContext context)
    {
        String ngayhethan = context.Request.Form["ngayhethan"];
        String tinhtrang_hethan = context.Request.Form["tinhtrang_hethan"];
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String id = context.Request.Form["id"];
        String ma_tinhtrang = context.Request.Form["ma_tinhtrang"].Trim();
        String ten_tinhtrang = context.Request.Form["ten_tinhtrang"].Trim();

        var lst = (from o in db.md_tinhtranghanghoas
                   where (o.ma_tinhtrang.Equals(ma_tinhtrang) | o.ten_tinhtrang.Equals(ten_tinhtrang)) & !o.md_tinhtranghanghoa_id.Equals(id)
                   select new { o.md_tinhtranghanghoa_id });

        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã có danh mục này!");
        }
        else
        {
            var m = db.md_tinhtranghanghoas.Single(p => p.md_tinhtranghanghoa_id.Equals(id));
            m.ma_tinhtrang = ma_tinhtrang;
            m.ten_tinhtrang = ten_tinhtrang;
            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;

            if (string.IsNullOrWhiteSpace(ngayhethan))
                m.ngayhethan = null;
            else
                m.ngayhethan = DateTime.ParseExact(ngayhethan, "dd/MM/yyyy", null);

            m.tinhtrang_hethan = tinhtrang_hethan;
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaycapnhat = DateTime.Now;
            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        String ngayhethan = context.Request.Form["ngayhethan"];
        String tinhtrang_hethan = context.Request.Form["tinhtrang_hethan"];
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String ma_tinhtrang = context.Request.Form["ma_tinhtrang"].Trim();
        String ten_tinhtrang = context.Request.Form["ten_tinhtrang"].Trim();

        var lst = (from o in db.md_tinhtranghanghoas
                   where (o.ma_tinhtrang.Equals(ma_tinhtrang) | o.ten_tinhtrang.Equals(ten_tinhtrang))
                   select new { o.md_tinhtranghanghoa_id });

        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã có danh mục này!");
        }
        else
        {
            var m = new md_tinhtranghanghoa();
            m.md_tinhtranghanghoa_id = Helper.getNewId();
            m.ma_tinhtrang = ma_tinhtrang;
            m.ten_tinhtrang = ten_tinhtrang;
            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;

            if (string.IsNullOrWhiteSpace(ngayhethan))
                m.ngayhethan = null;
            else
                m.ngayhethan = DateTime.ParseExact(ngayhethan, "dd/MM/yyyy", null);

            m.tinhtrang_hethan = tinhtrang_hethan;
            m.nguoitao = UserUtils.getUser(context);
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaytao = DateTime.Now;
            m.ngaycapnhat = DateTime.Now;
            db.md_tinhtranghanghoas.InsertOnSubmit(m);
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_tinhtranghanghoa where md_tinhtranghanghoa_id IN (" + id + ")";
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

        String sqlCount = "SELECT COUNT(1) AS count FROM md_tinhtranghanghoa tt where 1=1 {0} ";
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
            sidx = "tt.ma_tinhtrang";
        }

        string strsql = @"
            SELECT 
                P.* 
            FROM (
                SELECT 
                    tt.md_tinhtranghanghoa_id, 
                    tt.ma_tinhtrang, 
                    tt.ten_tinhtrang, 
                    tt.ngayhethan,
                    tts.ten_tinhtrang as tinhtrang_hethan,
                    tt.ngaytao, 
                    tt.nguoitao, 
                    tt.ngaycapnhat, 
                    tt.nguoicapnhat, 
                    tt.mota, 
                    tt.hoatdong, 
                    ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
                FROM 
                    md_tinhtranghanghoa tt 
                    left join md_tinhtranghanghoa tts on tt.tinhtrang_hethan = tts.md_tinhtranghanghoa_id
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
            foreach (System.Data.DataColumn column in dt.Columns)
            {
                if (column.DataType == System.Type.GetType("System.DateTime"))
                {
                    try
                    {
                        xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", ((DateTime)row[column.ColumnName]).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", "");
                    }
                }
                else
                    xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", row[column.ColumnName]);
            }
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
