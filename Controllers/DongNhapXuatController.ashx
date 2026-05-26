<%@ WebHandler Language="C#" Class="DongNhapXuatController" %>


using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class DongNhapXuatController : IHttpHandler
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
        String sql = "select c_dongnhapxuat_id, mota_tiengviet from c_dongnhapxuat where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_dongnhapxuat m = db.c_dongnhapxuats.Where(p => p.c_dongnhapxuat_id == context.Request.Form["id"]).FirstOrDefault();
        c_nhapxuat nx = db.c_nhapxuats.Where(p => p.c_nhapxuat_id.Equals(m.c_nhapxuat_id)).FirstOrDefault();
        if (nx.md_trangthai_id == "SOANTHAO")
        {
            //m.c_nhapxuat_id = context.Request.Form["c_nhapxuat_id"];
            //m.c_dongdonhang_id = context.Request.Form["c_dongdonhang_id"];
            m.md_sanpham_id = context.Request.Form["sanpham_id"];
            m.md_donvitinh_id = context.Request.Form["md_donvitinh_id"];
            m.slphai_nhapxuat = decimal.Parse(context.Request.Form["slphai_nhapxuat"]);
            m.slthuc_nhapxuat = decimal.Parse(context.Request.Form["slthuc_nhapxuat"]);
            m.dongia = decimal.Parse(context.Request.Form["dongia"]);
            m.sokien_thucte = decimal.Parse(context.Request.Form["sokien_thucte"]);

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;

            db.SubmitChanges();
        }
        else
        { 
            jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi phiếu Nhập/Xuất đã hiệu lực");
        }
        
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        
        c_dongnhapxuat mnu = new c_dongnhapxuat
        {
            c_dongnhapxuat_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            c_nhapxuat_id = context.Request.Form["c_nhapxuat_id"],
            c_dongdonhang_id = context.Request.Form["c_dongdonhang_id"],
            md_sanpham_id = context.Request.Form["sanpham_id"],
            md_donvitinh_id = context.Request.Form["tendvt"],
            slphai_nhapxuat = decimal.Parse(context.Request.Form["slphai_nhapxuat"]),
            slthuc_nhapxuat = decimal.Parse(context.Request.Form["slthuc_nhapxuat"]),
            dongia = decimal.Parse(context.Request.Form["dongia"]),
            sokien_thucte = decimal.Parse(context.Request.Form["sokien_thucte"]),
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.c_dongnhapxuats.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update c_dongnhapxuat set hoatdong = 0 where c_dongnhapxuat_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String whId = context.Request.QueryString["whId"];
        
        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_dongnhapxuat dnx " +
			" left join c_nhapxuat nx on dnx.c_nhapxuat_id = nx.c_nhapxuat_id " +
			" left join md_sanpham sp on dnx.md_sanpham_id = sp.md_sanpham_id" +
			" left join md_donvitinhsanpham dvt on dnx.md_donvitinh_id = dvt.md_donvitinhsanpham_id " +
            " WHERE dnx.c_nhapxuat_id = N'{0}' " + filter;

        if (whId != null)
        {
            sqlCount = string.Format(sqlCount, whId);
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
            sidx = "dnx.line";
        }

        string strsql = "select * from( " +
            " select dnx.c_dongnhapxuat_id, nx.sophieu, dnx.c_dongdonhang_id as donghang " +
            " , dnx.line, sp.md_sanpham_id, sp.ma_sanpham, dnx.mota_tiengviet, dvt.ten_dvt " +
            " , dnx.slphai_nhapxuat, dnx.slthuc_nhapxuat " +
            " , dnx.dongia, dnx.sokien_thucte " +
            " , dnx.ngaytao, dnx.nguoitao, dnx.ngaycapnhat, dnx.nguoicapnhat, dnx.mota, dnx.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_dongnhapxuat dnx " +
			" Left join c_nhapxuat nx on dnx.c_nhapxuat_id = nx.c_nhapxuat_id " +
			" Left join md_sanpham sp on dnx.md_sanpham_id = sp.md_sanpham_id " +
			" Left join md_donvitinhsanpham dvt on dnx.md_donvitinh_id = dvt.md_donvitinhsanpham_id" +
            " WHERE dnx.c_nhapxuat_id = N'{0}' " + filter +
            ")P WHERE RowNum > @start AND RowNum < @end";

        if (whId != null)
        {
            strsql = string.Format(strsql, whId);
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
            xml += "<cell><![CDATA[" + row[3] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[12].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[14].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[15] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[16] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[17] + "]]></cell>";
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
