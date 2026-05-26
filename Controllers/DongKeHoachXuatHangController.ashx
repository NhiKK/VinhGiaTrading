<%@ WebHandler Language="C#" Class="DongKeHoachXuatHangController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class DongKeHoachXuatHangController : IHttpHandler
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
        String sql = "select c_dongkhxh_id, c_dongkhxh_id from c_dongkhxh where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_dongkhxh m = db.c_dongkhxhs.Single(p => p.c_dongkhxh_id == context.Request.Form["id"]);
        m.c_kehoachxuathang_id = context.Request.Form["c_kehoachxuathang_id"];
        m.c_donhang_id = context.Request.Form["c_donhang_id"];
        m.c_dongdonhang_id = context.Request.Form["c_dongdonhang_id"];
        m.soluong = decimal.Parse(context.Request.Form["soluong"]);
        m.ngay_xuathang = DateTime.ParseExact(context.Request.Form["ngay_xuathang"], "dd/MM/yyyy", null);
        m.line = int.Parse(context.Request.Form["line"]);
        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaycapnhat = DateTime.Now;
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_dongkhxh mnu = new c_dongkhxh
        {
            c_dongkhxh_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            c_kehoachxuathang_id = context.Request.Form["c_kehoachxuathang_id"],
            c_donhang_id = context.Request.Form["c_donhang_id"],
            c_dongdonhang_id = context.Request.Form["c_dongdonhang_id"],
            soluong = decimal.Parse(context.Request.Form["soluong"]),
            ngay_xuathang = DateTime.ParseExact(context.Request.Form["ngay_xuathang"], "dd/MM/yyyy", null),
            line = int.Parse(context.Request.Form["line"]),
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.c_dongkhxhs.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update c_dongkhxh set hoatdong = 0 where c_dongkhxh_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_dongkhxh dkhxh, c_kehoachxuathang khxh, c_donhang dh, c_dongdonhang ddh " +
            " WHERE dkhxh.c_kehoachxuathang_id = khxh.c_kehoachxuathang_id " +
            " AND dkhxh.c_donhang_id = dh.c_donhang_id " +
            " AND dkhxh.c_dongdonhang_id = ddh.c_dongdonhang_id";
        
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
            sidx = "dkhxh.c_dongkhxh_id";
        }

        string strsql = "select * from( " +
            " select dkhxh.c_dongkhxh_id, khxh.ten_kehoach, dh.sochungtu, " +
            " ddh.c_dongdonhang_id, dkhxh.soluong, dkhxh.ngay_xuathang, " +
            " dkhxh.line, dkhxh.ngaytao, dkhxh.nguoitao, dkhxh.ngaycapnhat, " +
            " dkhxh.nguoicapnhat, dkhxh.mota, dkhxh.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_dongkhxh dkhxh, c_kehoachxuathang khxh, c_donhang dh, c_dongdonhang ddh " +
            " WHERE dkhxh.c_kehoachxuathang_id = khxh.c_kehoachxuathang_id " +
            " AND dkhxh.c_donhang_id = dh.c_donhang_id " +
            " AND dkhxh.c_dongdonhang_id = ddh.c_dongdonhang_id)P " +
            " WHERE RowNum > @start AND RowNum < @end";

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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[5].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
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
