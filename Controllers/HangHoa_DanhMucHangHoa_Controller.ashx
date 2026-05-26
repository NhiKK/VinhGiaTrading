<%@ WebHandler Language="C#" Class="HangHoa_DanhMucHangHoa_Controller" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class HangHoa_DanhMucHangHoa_Controller : IHttpHandler
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
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String id = context.Request.Form["id"];
        String ten_danhmuc = context.Request.Form["ten_danhmuc"].Trim();

        var m = db.md_sanpham_dmhhs.Single(p => p.md_sanpham_dmhh_id.Equals(id));

        var lst = db.md_sanpham_dmhhs.Where(s =>
            s.md_danhmuchanghoa_id == id
            & s.md_sanpham_id == m.md_sanpham_id
            & s.md_sanpham_dmhh_id != m.md_sanpham_dmhh_id
            );

        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Sản phẩm đã có danh mục này!");
        }
        else
        {
            var dmhh = db.md_danhmuchanghoas.Where(s => s.md_danhmuchanghoa_id == ten_danhmuc).FirstOrDefault();
            var sp = db.md_sanphams.Where(s => s.md_sanpham_id == m.md_sanpham_id).FirstOrDefault();
            if (dmhh == null)
                jqGridHelper.Utils.writeResult(0, "Danh mục hàng hóa không tồn tại!");
            else if (sp == null)
                jqGridHelper.Utils.writeResult(0, "Sản phẩm không tồn tại!");
            else
            {
                m.md_danhmuchanghoa_id = dmhh.md_danhmuchanghoa_id;
                m.ma_danhmuc = dmhh.ma_danhmuc;
                m.ten_danhmuc = dmhh.ten_danhmuc;
                m.mota = context.Request.Form["mota"];
                m.nguoicapnhat = UserUtils.getUser(context);
                m.ngaycapnhat = DateTime.Now;
                db.SubmitChanges();
            }
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        string ten_danhmuc = context.Request.Form["ten_danhmuc"].Trim();
        string md_sanpham_id = context.Request.Form["md_sanpham_id"].Trim();

        var lst = db.md_sanpham_dmhhs.Where(s =>
            s.md_danhmuchanghoa_id == ten_danhmuc
            & s.md_sanpham_id == md_sanpham_id
            );

        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Sản phẩm đã có danh mục này!");
        }
        else
        {
            var dmhh = db.md_danhmuchanghoas.Where(s => s.md_danhmuchanghoa_id == ten_danhmuc).FirstOrDefault();
            var sp = db.md_sanphams.Where(s => s.md_sanpham_id == md_sanpham_id).FirstOrDefault();
            if (dmhh == null)
                jqGridHelper.Utils.writeResult(0, "Danh mục hàng hóa không tồn tại!");
            else if (sp == null)
                jqGridHelper.Utils.writeResult(0, "Sản phẩm không tồn tại!");
            else
            {
                var m = new md_sanpham_dmhh();
                m.md_sanpham_dmhh_id = Helper.getNewId();
                m.md_danhmuchanghoa_id = dmhh.md_danhmuchanghoa_id;
                m.md_sanpham_id = sp.md_sanpham_id;
                m.ma_danhmuc = dmhh.ma_danhmuc;
                m.ten_danhmuc = dmhh.ten_danhmuc;
                m.mota = context.Request.Form["mota"];
                m.hoatdong = true;
                m.nguoitao = UserUtils.getUser(context);
                m.nguoicapnhat = UserUtils.getUser(context);
                m.ngaytao = DateTime.Now;
                m.ngaycapnhat = DateTime.Now;
                db.md_sanpham_dmhhs.InsertOnSubmit(m);
                db.SubmitChanges();
            }
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_sanpham_dmhh where md_sanpham_dmhh_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String id = context.Request.Params["id"];

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }


        String sqlCount = @"
            SELECT 
                COUNT(1) AS count 
            FROM 
                md_sanpham_dmhh dmhh 
                LEFT JOIN md_danhmuchanghoa dm on dmhh.md_danhmuchanghoa_id = dm.md_danhmuchanghoa_id
            where 
                1=1 
                and dmhh.md_sanpham_id = N'{0}' 
                {1} 
        ";
        sqlCount = string.Format(sqlCount, id, filter);

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
                    dmhh.md_sanpham_dmhh_id, 
                    dmhh.md_sanpham_id, 
                    dm.ma_danhmuc, 
                    dm.ten_danhmuc, 
                    dm.ngaytao, 
                    dm.nguoitao, 
                    dm.ngaycapnhat, 
                    dm.nguoicapnhat, 
                    dm.mota, 
                    dm.hoatdong, 
                    ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
                FROM md_sanpham_dmhh dmhh 
                    LEFT JOIN md_danhmuchanghoa dm on dmhh.md_danhmuchanghoa_id = dm.md_danhmuchanghoa_id
                WHERE 
                    1=1
                    and dmhh.md_sanpham_id = N'{2}'
                    {3}
            )P 
            WHERE 
                RowNum > @start AND RowNum < @end
        ";

        strsql = string.Format(strsql, sidx, sord, id, filter);

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
