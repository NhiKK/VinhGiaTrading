<%@ WebHandler Language="C#" Class="RoleController" %>

using System;
using System.Web;
using System.Linq;

public class RoleController : IHttpHandler
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
        String sql = "select mavt, tenvt from vaitro where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        vaitro u = db.vaitros.Single(p => p.mavt == context.Request.Form["id"]);
        u.tenvt = context.Request.Form["tenvt"];
        u.hoatdong = hd;
        u.mota = context.Request.Form["mota"];
        u.ngaycapnhat = DateTime.Now;
        u.nguoicapnhat = UserUtils.getUser(context);
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        vaitro obj = new vaitro
        {
            mavt = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmsstt")),
            tenvt = context.Request.Form["tenvt"],
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.vaitros.InsertOnSubmit(obj);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.mavt.Equals(id));
        phanquyen pq = db.phanquyens.FirstOrDefault(p => p.mavt.Equals(id));

        String msg = "";
        bool next = true;
        
        if (nv != null)
        {
            next = false;
            msg += "<div>Không thể xóa vì có nhân viên thuộc vai trò này!</div>";
        }

        if (pq != null)
        {
            next = false;
            msg += "<div>Không thể xóa vì vai trò này đã có phân quyền!</div>";
        }
        
        if (next)
        {
            vaitro vt = db.vaitros.FirstOrDefault(p => p.mavt.Equals(id));
            db.vaitros.DeleteOnSubmit(vt);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, msg);   
        }
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

        String sqlCount = "SELECT COUNT(*) AS count FROM vaitro where 1=1 " + filter;
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
            sidx = "vt.tenvt";
        }

        string strsql = "select * from( select vt.*,  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum from vaitro vt where 1=1 "+filter+" )P where RowNum > @start AND RowNum < @end";

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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[2].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[3] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[4].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
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