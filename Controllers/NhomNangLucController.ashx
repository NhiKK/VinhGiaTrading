<%@ WebHandler Language="C#" Class="NhomNangLucController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class NhomNangLucController : IHttpHandler
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
        String sql = "select md_nhomnangluc_id, hehang + '(' + nhom + ')' as tennhom from md_nhomnangluc where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        string msg = "";
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_chungloai cl = db.md_chungloais.Single(p => p.md_chungloai_id.Equals(context.Request.Form["md_chungloai_id"]));
        string nhom = context.Request.Form["nhom"];
        var nl = db.md_nhomnanglucs.Where(p => 
            p.md_chungloai_id.Equals(context.Request.Form["md_chungloai_id"]) & 
            p.hehang.Equals(cl.code_cl) & 
            p.nhom == nhom &
            p.md_nhomnangluc_id != context.Request.Form["id"]
            );
        if(nl.Count() > 0)
        {
            msg = "Nhóm Năng Lực đã tồn tại!";
        }
        else
        {
            md_nhomnangluc m = db.md_nhomnanglucs.Single(p => p.md_nhomnangluc_id == context.Request.Form["id"]);
            m.hehang = cl.code_cl;
            m.nhom = context.Request.Form["nhom"];
            m.md_chungloai_id = context.Request.Form["md_chungloai_id"];
            m.mota_tiengviet = context.Request.Form["mota_tiengviet"];
            m.hscode = context.Request.Form["hscode"];
            m.thoigianlamhang = int.Parse(context.Request.Form["thoigianlamhang"]);

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaycapnhat = DateTime.Now;

            db.SubmitChanges();
            msg = "Cập nhật thành công.";
        }
        context.Response.Write(msg);
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        string msg = "";
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_chungloai cl = db.md_chungloais.Single(p => p.md_chungloai_id.Equals(context.Request.Form["md_chungloai_id"]));

        var nl = db.md_nhomnanglucs.Where(p => p.md_chungloai_id.Equals(context.Request.Form["md_chungloai_id"]) & p.hehang.Equals(cl.code_cl));
        if(nl.Count() > 0)
        {
            msg = "Nhóm Năng Lực đã tồn tại!";
        }
        else
        {
            md_nhomnangluc mnu = new md_nhomnangluc
            {
                md_nhomnangluc_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                hehang = cl.code_cl,
                nhom = context.Request.Form["nhom"],
                md_chungloai_id = context.Request.Form["md_chungloai_id"],
                mota_tiengviet = context.Request.Form["mota_tiengviet"],
                hscode = context.Request.Form["hscode"],
                thoigianlamhang = int.Parse(context.Request.Form["thoigianlamhang"]),
	
										
				  
										  
											  
						   
							  
	 

                mota = context.Request.Form["mota"],
                hoatdong = hd,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            db.md_nhomnanglucs.InsertOnSubmit(mnu);
            db.SubmitChanges();
            msg = "Thêm Nhóm Năng Lực thành công.";
        }
        context.Response.Write(msg);
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"], msg = "";
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");

        int count_sp_bk = db.ad_sanpham_backups.Where(s => s.md_nhomnangluc_id == context.Request.Form["id"]).Count();

        int count_ct = db.c_chitietnanglucs.Where(s => s.md_nhomnangluc_id == context.Request.Form["id"]).Count();

        int count_ls = db.c_lichsubooknanglucs.Where(s => s.md_nhomnangluc_id == context.Request.Form["id"]).Count();

        int countsp = db.md_sanphams.Where(s => s.md_nhomnangluc_id == context.Request.Form["id"]).Count();

        int total = count_sp_bk + count_ct + count_ls + countsp;

        if(total <= 0)
        {
            String sql = "delete md_nhomnangluc where md_nhomnangluc_id IN (" + id + ")";
            mdbc.ExcuteNonQuery(sql);
        }
        else
        {
            msg = "Lỗi: Không thể xóa.";
        }
        context.Response.Write(msg);
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

        String sqlCount = "SELECT COUNT(*) AS count FROM md_nhomnangluc nnl, md_chungloai cl " +
            " where nnl.md_chungloai_id = cl.md_chungloai_id " + filter;
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
            sidx = "nnl.md_nhomnangluc_id";
        }

        string strsql = "select * from(" +
            " select nnl.md_nhomnangluc_id, cl.code_cl, nnl.nhom " +
            " , nnl.mota_tiengviet, nnl.hscode, nnl.thoigianlamhang " +
            " , nnl.ngaytao, nnl.nguoitao " +
            " , nnl.ngaycapnhat, nnl.nguoicapnhat, nnl.mota, nnl.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " from md_nhomnangluc nnl , md_chungloai cl " +
            " where nnl.md_chungloai_id = cl.md_chungloai_id " + filter +
            ")P where RowNum > @start AND RowNum < @end";

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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[6].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[8].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
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
