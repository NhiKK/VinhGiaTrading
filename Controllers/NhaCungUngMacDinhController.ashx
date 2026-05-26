<%@ WebHandler Language="C#" Class="NhaCungUngMacDinhController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class NhaCungUngMacDinhController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getoptionselected":
                this.getOptionSelected(context);
                break;
            case"getoption":
                this.getOption(context);
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

    public void getOptionSelected(HttpContext context)
    {
        String md_sanpham_id = context.Request.QueryString["md_sanpham_id"];
        String c_dongdonhang_id = context.Request.QueryString["c_dongdonhang_id"];
        String nhacungungid = "";

        if (c_dongdonhang_id != "")
        {
            c_dongdonhang ddh = db.c_dongdonhangs.FirstOrDefault(p => p.c_dongdonhang_id.Equals(c_dongdonhang_id));
            nhacungungid = ddh.nhacungungid;
        }
        
        String sql = @"select dtkd.md_doitackinhdoanh_id, dtkd.ma_dtkd, ncu.macdinh
                        from md_doitackinhdoanh dtkd, md_nhacungungmacdinh ncu 
                        where dtkd.md_doitackinhdoanh_id = ncu.md_doitackinhdoanh_id 
                        and ncu.md_sanpham_id = '" + md_sanpham_id + @"' order by ncu.macdinh desc, dtkd.ma_dtkd asc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToSelected(nhacungungid));
    }

    public void getOption(HttpContext context)
    {
        String nhacungungid = context.Request.QueryString["nhacungungid"];
        String md_sanpham_id = context.Request.QueryString["md_sanpham_id"];
        String sql = @"select dtkd.md_doitackinhdoanh_id, dtkd.ma_dtkd 
                        from md_doitackinhdoanh dtkd, md_nhacungungmacdinh ncu 
                        where dtkd.md_doitackinhdoanh_id = ncu.md_doitackinhdoanh_id 
                        and ncu.md_sanpham_id = '" + md_sanpham_id + "'";
        SelectHtmlControl s = new SelectHtmlControl(sql);

        context.Response.Write(s.ToSelected(nhacungungid));
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        String md = context.Request.Form["macdinh"].ToLower();

        bool hd, macdinh;
        hd = macdinh = false;

        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        if (md.Equals("on") || md.Equals("true"))
        { macdinh = true; }

        String md_sanpham_id = context.Request.Form["md_sanpham_id"];
        String md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        String sel = "select COUNT(*) as count from md_nhacungungmacdinh where md_sanpham_id = @md_sanpham_id AND md_doitackinhdoanh_id = @md_doitackinhdoanh_id";
        int colCount = (int)mdbc.ExecuteScalar(sel, "@md_sanpham_id", md_sanpham_id, "@md_doitackinhdoanh_id", md_doitackinhdoanh_id);

        if (colCount != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã tồn tại nhà cung ứng.!");
        }
        else
        {
                        
            md_nhacungungmacdinh mnu = new md_nhacungungmacdinh
            {
                md_nhacungungmacdinh_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                md_sanpham_id = md_sanpham_id,
                md_doitackinhdoanh_id = md_doitackinhdoanh_id,
                macdinh = macdinh,

                mota = context.Request.Form["mota"],
                hoatdong = hd,
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context)
            };

            db.md_nhacungungmacdinhs.InsertOnSubmit(mnu);
            db.SubmitChanges();
            jqGridHelper.Utils.writeResult(1, "Đã tạo nhà cung ứng cho sản phẩm.!");
        }
    }
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        String md = context.Request.Form["macdinh"].ToLower();
        
        bool hd, macdinh;
        hd = macdinh = false;
        
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        if (md.Equals("on") || md.Equals("true"))
        { macdinh = true; }

        String id = context.Request.Form["id"];
        String md_sanpham_id =context.Request.Form["md_sanpham_id"];
        String md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        String sel = "select COUNT(*) as count from md_nhacungungmacdinh where md_sanpham_id = @md_sanpham_id AND md_doitackinhdoanh_id = @md_doitackinhdoanh_id AND md_nhacungungmacdinh_id != @md_nhacungungmacdinh_id";
        int colCount = (int)mdbc.ExecuteScalar(sel, "@md_sanpham_id", md_sanpham_id, "@md_doitackinhdoanh_id", md_doitackinhdoanh_id, "@md_nhacungungmacdinh_id", id);

        if (colCount != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã tồn tại nhà cung ứng.!");
        }
        else
        {
                        
            md_nhacungungmacdinh m = db.md_nhacungungmacdinhs.Single(p => p.md_nhacungungmacdinh_id.Equals(id));
            m.md_sanpham_id = md_sanpham_id;
            m.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
            m.macdinh = macdinh;

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;
            m.nguoicapnhat = UserUtils.getUser(context);
            db.SubmitChanges();
            jqGridHelper.Utils.writeResult(1, "Đã thay đổi thông tin nhà cung ứng.!");
        }
    }

    


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        var ncu = db.md_nhacungungmacdinhs.Single(cx=>cx.md_nhacungungmacdinh_id.Equals(id));
        int cout_cx = (from lcx in db.md_nhacungungmacdinhs
                      where lcx.md_sanpham_id.Equals(ncu.md_sanpham_id)
                      select new {lcx.md_sanpham_id}).Count();

        if (cout_cx >1)
        {
            String sql = "delete md_nhacungungmacdinh where md_nhacungungmacdinh_id = @md_nhacungungmacdinh_id";
            mdbc.ExcuteNonQuery(sql, "@md_nhacungungmacdinh_id", id);

            string select = "select top 1 md_nhacungungmacdinh_id from md_nhacungungmacdinh where md_sanpham_id='" + ncu.md_sanpham_id + "'";
            string mcx_id = (string)mdbc.ExecuteScalar(select);
            string update_cx = "update md_nhacungungmacdinh set macdinh=1 where md_nhacungungmacdinh_id ='" + mcx_id + "'";
            mdbc.ExcuteNonQuery(update_cx);
            jqGridHelper.Utils.writeResult(1, "Xóa nhà cung ứng thành công!");
        }
        if (cout_cx == 1)
        {
            jqGridHelper.Utils.writeResult(0, "Cần phải tồn tại ít nhất một nhà cung ứng mặc định cho sản phẩm!");
        }
        
    }

    public void load(HttpContext context)
    {
        String productId = context.Request.QueryString["productId"];
        
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
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM md_nhacungungmacdinh ncu, md_sanpham sp, md_doitackinhdoanh dtkd " +
            " WHERE ncu.md_sanpham_id = sp.md_sanpham_id " +
            " AND ncu.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id" +
            " AND ncu.md_sanpham_id = N'{0}' {1} ";

        if (productId != null)
        {
            sqlCount = String.Format(sqlCount, productId, filter);
        }
        else {
            sqlCount = String.Format(sqlCount, 0, filter);
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
            sidx = "ncu.md_nhacungungmacdinh_id";
        }

        String strsql = "select * from( " +
            " select ncu.md_nhacungungmacdinh_id, sp.md_sanpham_id, dtkd.ma_dtkd, ncu.macdinh " +
            " , ncu.ngaytao, ncu.nguoitao, ncu.ngaycapnhat, ncu.nguoicapnhat, ncu.mota, ncu.hoatdong " +
            " ,  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_nhacungungmacdinh ncu, md_sanpham sp, md_doitackinhdoanh dtkd " +
            " WHERE ncu.md_sanpham_id = sp.md_sanpham_id " +
            " AND ncu.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " AND ncu.md_sanpham_id = N'{0}' {1} " +
            " )P where RowNum > @start AND RowNum < @end";

        if (productId != null)
        {
            strsql = String.Format(strsql, productId, filter);
        }
        else
        {
            strsql = String.Format(strsql, 0, filter);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[4].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[6].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
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
