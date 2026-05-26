<%@ WebHandler Language="C#" Class="RoleModController" %>

using System;
using System.Web;
using System.Linq;

public class RoleModController : IHttpHandler
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
        String h, xe, th, s, xo, gi, chx;
        h = context.Request.Form["hoatdong"].ToLower();
        xe = context.Request.Form["xem"].ToLower();
        th = context.Request.Form["them"].ToLower();
        s = context.Request.Form["sua"].ToLower();
        xo = context.Request.Form["xoa"].ToLower();
        gi = context.Request.Form["gia"].ToLower();
		chx = context.Request.Form["chietxuat"].ToLower();
        bool hd, xem, them, sua, xoa, truyxuat, gia, chietxuat;
        hd = xem = them = sua = xoa = truyxuat = gia = chietxuat = false;

        if (h.Equals("on") || h.Equals("True") || h.Equals("true"))
        {
            hd = true;
        }
        if (xe.Equals("on") || xe.Equals("True") || xe.Equals("true"))
        {
            xem = true;
        }
        if (th.Equals("on") || th.Equals("True") || th.Equals("true"))
        {
            them = true;
        }
        if (s.Equals("on") || s.Equals("True") || s.Equals("true"))
        {
            sua = true;
        }
        if (xo.Equals("on") || xo.Equals("True") || xo.Equals("true"))
        {
            xoa = true;
        }
		if (gi.Equals("on") || gi.Equals("True") || gi.Equals("true"))
        {
            gia = true;
        }
		if (chx.Equals("on") || chx.Equals("True") || chx.Equals("true"))
        {
            chietxuat = true;
        }

        if (xem || them || sua || xoa || gia || chietxuat)
        {
            truyxuat = true;
        }

        phanquyen u = db.phanquyens.Single(p => p.mapq == context.Request.Form["id"]);
        u.mavt = context.Request.Form["tenvt"];
        u.mamenu = context.Request.Form["tenmenu"];
        u.hoatdong = hd;
        u.xem = xem;
        u.them = them;
        u.sua = sua;
        u.xoa = xoa;
        u.truyxuat = truyxuat;
        u.gia = gia;
		u.chietxuat = chietxuat;
        u.mota = context.Request.Form["mota"];
        u.nguoicapnhat = UserUtils.getUser(context);
        u.ngaycapnhat = DateTime.Now;
        db.SubmitChanges();
        
        
    }

    public void add(HttpContext context)
    {
        String h, xe, th, s, xo, gi, chx;
        h = context.Request.Form["hoatdong"].ToLower();
        xe = context.Request.Form["xem"].ToLower();
        th = context.Request.Form["them"].ToLower();
        s = context.Request.Form["sua"].ToLower();
        xo = context.Request.Form["xoa"].ToLower();
        gi = context.Request.Form["gia"].ToLower();
		chx = context.Request.Form["chietxuat"].ToLower();
        bool hd, xem, them, sua, xoa, truyxuat, gia, chietxuat;
        hd = xem = them = sua = xoa = truyxuat = gia = chietxuat = false;

        if (h.Equals("on") || h.Equals("True") || h.Equals("true"))
        {
            hd = true;
        }
        if (xe.Equals("on") || xe.Equals("True") || xe.Equals("true"))
        {
            xem = true;
        }
        if (th.Equals("on") || th.Equals("True") || th.Equals("true"))
        {
            them = true;
        }
        if (s.Equals("on") || s.Equals("True") || s.Equals("true"))
        {
            sua = true;
        }
        if (xo.Equals("on") || xo.Equals("True") || xo.Equals("true"))
        {
            xoa = true;
        }
		if (gi.Equals("on") || gi.Equals("True") || gi.Equals("true"))
        {
            gia = true;
        }
		if (chx.Equals("on") || chx.Equals("True") || chx.Equals("true"))
        {
            chietxuat = true;
        }
		
        if (xem || them || sua || xoa || gia || chietxuat)
        {
            truyxuat = true;
        }


        phanquyen p = new phanquyen();
            p.mapq = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmsstt"));
            p.mavt = context.Request.Form["tenvt"];
            p.mamenu = context.Request.Form["tenmenu"];
            p.hoatdong = hd;
            p.xem = xem;
            p.them = them;
            p.sua = sua;
            p.xoa = xoa;
			p.chietxuat = chietxuat;
            p.truyxuat = truyxuat;
            p.gia = gia;
            p.mota = context.Request.Form["mota"];
            p.hoatdong = hd;
            p.nguoitao = UserUtils.getUser(context);
            p.nguoicapnhat = UserUtils.getUser(context);
            p.ngaytao = DateTime.Now;
            p.ngaycapnhat = DateTime.Now;
        
        db.phanquyens.InsertOnSubmit(p);
        db.SubmitChanges();
        
        
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete phanquyen where mapq IN (" + id + ")";
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

        String sqlCount = "SELECT COUNT(*) AS count from phanquyen pq JOIN vaitro vt ON pq.mavt = vt.mavt JOIN menu mnu ON pq.mamenu = mnu.mamenu where 1=1 " + filter;
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

        string strsql = "SELECT * from( select pq.mapq, vt.tenvt, mnu.tenmenu, " +
            " pq.xem, pq.them, pq.sua, pq.xoa, pq.gia, pq.chietxuat, " + 
            " pq.ngaytao, pq.nguoitao, pq.ngaycapnhat, pq.nguoicapnhat, pq.mota, pq.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " + 
            " FROM phanquyen pq JOIN vaitro vt ON pq.mavt = vt.mavt JOIN menu mnu ON pq.mamenu = mnu.mamenu where 1=1 " + filter + " )P " + 
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
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
			xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[9].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[11].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[14] + "]]></cell>";
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