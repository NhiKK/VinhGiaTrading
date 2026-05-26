<%@ WebHandler Language="C#" Class="ProductInWarehouseController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ProductInWarehouseController : IHttpHandler
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
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_hangtrongkho m = db.md_hangtrongkhos.Single(p => p.md_hangtrongkho_id == context.Request.Form["id"]);
        m.md_kho_id = context.Request.Form["tenkho"];
        m.md_sanpham_id = context.Request.Form["sanpham_id"];
        m.qty = decimal.Parse(context.Request.Form["qty"]);
        m.qty_pre = decimal.Parse(context.Request.Form["qty_pre"]);
        m.md_donvitinh_id = context.Request.Form["tendvt"];
        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.ngaycapnhat = DateTime.Now;
        m.nguoicapnhat = UserUtils.getUser(context);
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_hangtrongkho mnu = new md_hangtrongkho
        {
            md_hangtrongkho_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            md_kho_id = context.Request.Form["tenkho"],
            md_sanpham_id = context.Request.Form["sanpham_id"],
            qty = decimal.Parse(context.Request.Form["qty"]),
            qty_pre = decimal.Parse(context.Request.Form["qty_pre"]),
            md_donvitinh_id = context.Request.Form["tendvt"],
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context)
        };

        db.md_hangtrongkhos.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update md_hangtrongkho set hoatdong = 0 where md_hangtrongkho_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String cateId = context.Request.QueryString["cateId"];

        String sqlCount = "SELECT COUNT(*) AS count FROM md_hangtrongkho htk, md_kho kho, " +
            " md_sanpham sp, md_donvitinhsanpham dvt " +
            " WHERE htk.md_kho_id = kho.md_kho_id " +
            " AND htk.md_sanpham_id = sp.md_sanpham_id " +
            " AND htk.md_donvitinh_id = dvt.md_donvitinhsanpham_id " +
            " AND htk.md_kho_id  = N'{0}'";
        
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
            sidx = "htk.md_hangtrongkho_id";
        }

        string strsql = "select * from( " +
            " select htk.md_hangtrongkho_id, kho.ten_kho as tenkho, sp.md_sanpham_id as sanpham_id , sp.mota_tiengviet tensp, " +
            " htk.qty, htk.qty_pre, dvt.ten_dvt as tendvt, htk.ngaytao, htk.nguoitao, htk.ngaycapnhat, " +
            " htk.nguoicapnhat, htk.mota, htk.hoatdong, ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_hangtrongkho htk, md_kho kho, md_sanpham sp, md_donvitinhsanpham dvt " +
            " WHERE htk.md_kho_id = kho.md_kho_id " +
            " AND htk.md_sanpham_id = sp.md_sanpham_id " +
            " AND htk.md_donvitinh_id = dvt.md_donvitinhsanpham_id " +
            " AND htk.md_kho_id  = N'{0}'" +
            ")P WHERE RowNum > @start AND RowNum < @end";

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
            xml += "<cell><![CDATA[" + row[3] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[7].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[9].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
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
