<%@ WebHandler Language="C#" Class="PhanQuyenNangLucController" %>

using System;
using System.Web;
using System.Linq;

public class PhanQuyenNangLucController : IHttpHandler
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
        md_quanlynangluc u = db.md_quanlynanglucs.FirstOrDefault(p => p.md_quanlynangluc_id.Equals(context.Request.Form["id"]));
        u.manv = context.Request.Form["manv"];
        u.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        u.hoatdong = true;
        u.mota = context.Request.Form["mota"];
        u.nguoicapnhat = UserUtils.getUser(context);
        u.ngaycapnhat = DateTime.Now;
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        md_quanlynangluc u = new md_quanlynangluc();
        u.md_quanlynangluc_id = ImportUtils.getNEWID();
        u.manv = context.Request.Form["manv"];
        u.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        u.hoatdong = true;
        u.mota = context.Request.Form["mota"];
        u.nguoicapnhat = UserUtils.getUser(context);
        u.ngaycapnhat = DateTime.Now;
        u.nguoitao = UserUtils.getUser(context);
        u.ngaytao = DateTime.Now;
        
        db.md_quanlynanglucs.InsertOnSubmit(u);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_quanlynangluc where md_quanlynangluc_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count FROM md_quanlynangluc qlnl, nhanvien nv, md_doitackinhdoanh dtkd " +
            " WHERE qlnl.manv = nv.manv AND qlnl.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id ";
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
            sidx = "qlnl.manv";
        }

        string strsql = "SELECT * from(" +
            " select qlnl.md_quanlynangluc_id, qlnl.manv, dtkd.ma_dtkd, " +
            " qlnl.ngaytao, qlnl.nguoitao, qlnl.ngaycapnhat, qlnl.nguoicapnhat, qlnl.mota, qlnl.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " + 
            " FROM md_quanlynangluc qlnl, nhanvien nv, md_doitackinhdoanh dtkd " +
            " WHERE qlnl.manv = nv.manv AND qlnl.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " )P WHERE RowNum > @start AND RowNum < @end";

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