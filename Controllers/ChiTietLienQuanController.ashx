<%@ WebHandler Language="C#" Class="ChiTietLienQuanController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ChiTietLienQuanController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "add":
                this.add(context);
                break;
            case "edit":
                this.edit(context);
                break;
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
        String sql = "select c_chiphilienquan_id, c_chiphilienquan_id from c_chiphilienquan where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        try
        {
            c_chiphilienquan m = db.c_chiphilienquans.Single(p => p.c_chiphilienquan_id == context.Request.Form["id"]);
            var thuchi = db.c_thuchis.Single(c => c.c_thuchi_id.Equals(m.c_thuchi_id));
            if (thuchi.md_trangthai_id.Equals("SOANTHAO"))
            {
                m.tk_no = int.Parse(context.Request.Form["tkno"]);
                m.tk_co = int.Parse(context.Request.Form["tkco"]);
                m.sotien = decimal.Parse(context.Request.Form["sotien"]);
                m.quydoi = (thuchi.tygia.Value * decimal.Parse(context.Request.Form["sotien"]));
                m.diengiai = context.Request.Form["diengiai"];
                m.c_donhang_id = context.Request.Form["c_donhang_id"];
                m.c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"];
                m.loaichiphi = context.Request.Form["loaithanhtoan"];

                m.mota = context.Request.Form["mota"];
                m.hoatdong = true;
                m.ngaycapnhat = DateTime.Now;
                m.nguoicapnhat = UserUtils.getUser(context);

                db.SubmitChanges();
            }
            else {
                jqGridHelper.Utils.writeResult(0,"Không thể chỉnh sửa khi phiếu thu đã hiệu lực.");
            }
        }
        catch (Exception e)
        {
            context.Response.Write(e.Message);
        }
       
    }

    public void add(HttpContext context)
    {
        try
        {
            string c_thuchi_id = context.Request.Form["c_thuchi_id"];
            var thuchi = db.c_thuchis.Single(c => c.c_thuchi_id.Equals(c_thuchi_id));
            if (thuchi.md_trangthai_id.Equals("SOANTHAO"))
            {
                c_chiphilienquan mnu = new c_chiphilienquan
                {
                    c_chiphilienquan_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                    c_thuchi_id = c_thuchi_id,
                    tk_no = int.Parse(context.Request.Form["tkno"]),
                    tk_co = int.Parse(context.Request.Form["tkco"]),
                    sotien = decimal.Parse(context.Request.Form["sotien"]),
                    quydoi = (thuchi.tygia.Value * decimal.Parse(context.Request.Form["sotien"])),
                    diengiai = context.Request.Form["diengiai"],
                    c_donhang_id = context.Request.Form["c_donhang_id"],
                    c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"],
                    loaichiphi = context.Request.Form["loaithanhtoan"],
                    
                    mota = context.Request.Form["mota"],
                    hoatdong = true,
                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now,
                    nguoicapnhat = UserUtils.getUser(context),
                    nguoitao = UserUtils.getUser(context)
                };

                db.c_chiphilienquans.InsertOnSubmit(mnu);
                db.SubmitChanges();
            }
            else {
                jqGridHelper.Utils.writeResult(0, "Không thể thêm mới khi phiếu thu đã hiệu lực.");
            }
        }catch(Exception e){
            context.Response.Write(e.Message);
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        c_chiphilienquan ctlq = db.c_chiphilienquans.FirstOrDefault(p => p.c_chiphilienquan_id.Equals(id));
        c_thuchi tc = db.c_thuchis.FirstOrDefault(p => p.c_thuchi_id.Equals(ctlq.c_thuchi_id));
        if (tc.md_trangthai_id.Equals("SOANTHAO"))
        {
            db.c_chiphilienquans.DeleteOnSubmit(ctlq);
            db.SubmitChanges();
        }
        else {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa khi phiếu thu đã hiệu lực.");
        }
    }

    public void load(HttpContext context)
    {
        /*context.Response.ContentType = "text/xml";
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
        String tcId = context.Request.QueryString["tcId"];
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_chiphilienquan ctlq, c_thuchi tc" + 
            " WHERE ctlq.c_thuchi_id = tc.c_thuchi_id  "+
            " AND ctlq.c_thuchi_id = '{0}' ";
        sqlCount = string.Format(sqlCount, tcId);
        
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
            sidx = "cttc.c_chiphilienquan_id";
        }

        string strsql = "select * from( " +
            " select cttc.c_chiphilienquan_id, tc.sophieu, cttc.tk_no, cttc.tk_co, " +
            " cttc.sotien, cttc.quydoi, cttc.diengiai, cttc.obj_code, cttc.obj_id, " +
            " cttc.obj_num,  " +
            " (case when c_donhang_id is null then '' else (select sochungtu from c_donhang where c_donhang_id = cttc.c_donhang_id) end) as c_donhang_id, " +
            " (case when c_packinginvoice_id is null then '' else (select so_pkl from c_packinginvoice where c_packinginvoice_id = cttc.c_packinginvoice_id) end) as c_packinginvoice_id, " +
            " cttc.ngaytao, cttc.nguoitao, cttc.ngaycapnhat, " +
            " cttc.nguoicapnhat, cttc.mota, cttc.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_chiphilienquan cttc, c_thuchi tc " +
            " WHERE cttc.c_thuchi_id = tc.c_thuchi_id " +
            " AND cttc.c_thuchi_id = '{0}'" +
            " )P where RowNum > @start AND RowNum < @end ";
			
			
        if (tcId != null)
        {
            strsql = string.Format(strsql, tcId);
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
            xml += "<cell><![CDATA[" + row["c_chiphilienquan_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_no"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_co"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sotien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["quydoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["diengiai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["c_donhang_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["c_packinginvoice_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        
        context.Response.Write(xml);*/
		
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
        String tcId = context.Request.QueryString["tcId"];
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_chiphilienquan ctlq left join c_thuchi tc on ctlq.c_thuchi_id = tc.c_thuchi_id left join c_packinginvoice cpk on ctlq.c_packinginvoice_id = cpk.c_packinginvoice_id left join c_donhang dh on ctlq.c_donhang_id = dh.c_donhang_id" +
            " WHERE ctlq.c_thuchi_id = tc.c_thuchi_id  "+
            " AND ctlq.c_thuchi_id = '{0}' "+filter;
        sqlCount = string.Format(sqlCount, tcId);
        
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
            sidx = "ctlq.c_chiphilienquan_id";
        }

        string strsql = "select * from( " +
            " select ctlq.c_chiphilienquan_id, tc.sophieu, ctlq.tk_no, ctlq.tk_co, " +
            " ctlq.sotien, ctlq.quydoi, ctlq.diengiai, ctlq.obj_code, ctlq.obj_id, " +
            " ctlq.obj_num,  " +
           // " (case when c_donhang_id is null then '' else (select sochungtu from c_donhang where c_donhang_id = ctlq.c_donhang_id) end) as c_donhang_id, " +
           // " (case when c_packinginvoice_id is null then '' else (select so_pkl from c_packinginvoice where c_packinginvoice_id = ctlq.c_packinginvoice_id) end) as c_packinginvoice_id, " +
            " dh.sochungtu, " +
            " cpk.so_pkl, " +
            " ctlq.ngaytao, ctlq.nguoitao, ctlq.ngaycapnhat, " +
            " ctlq.nguoicapnhat, ctlq.mota, ctlq.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_chiphilienquan ctlq left join c_thuchi tc on ctlq.c_thuchi_id = tc.c_thuchi_id left join c_packinginvoice cpk on ctlq.c_packinginvoice_id = cpk.c_packinginvoice_id left join c_donhang dh on ctlq.c_donhang_id = dh.c_donhang_id" +
            " WHERE ctlq.c_thuchi_id = tc.c_thuchi_id " +
            " AND ctlq.c_thuchi_id = '{0}'" +filter+
            " )P where RowNum > @start AND RowNum < @end ";

        if (tcId != null)
        {
            strsql = string.Format(strsql, tcId);
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
            xml += "<cell><![CDATA[" + row["c_chiphilienquan_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_no"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_co"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sotien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["quydoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["diengiai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_pkl"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
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
