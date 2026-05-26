<%@ WebHandler Language="C#" Class="CangXuatHangController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class CangXuatHangController : IHttpHandler
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
        String sql = "select md_cangxuathang_id, md_cangxuathang_id from md_cangxuathang where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
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
        String md_cangbien_id = context.Request.Form["md_cangbien_id"];
        String sel = "select COUNT(*) as count from md_cangxuathang where md_sanpham_id = @md_sanpham_id AND md_cangbien_id = @md_cangbien_id AND md_cangxuathang_id != @md_cangxuathang_id";
        int colCount = (int)mdbc.ExecuteScalar(sel, "@md_sanpham_id", md_sanpham_id, "@md_cangbien_id", md_cangbien_id, "@md_cangxuathang_id", id);

        if (colCount != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã tồn tại cảng biển.!");
        }
        else
        {
            md_cangxuathang m = db.md_cangxuathangs.Single(p => p.md_cangxuathang_id.Equals(id));
            m.md_sanpham_id = md_sanpham_id;
            m.md_cangbien_id = md_cangbien_id;
            m.macdinh = macdinh;

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;
            m.nguoicapnhat = UserUtils.getUser(context);
            db.SubmitChanges();
            jqGridHelper.Utils.writeResult(1, "Đã thay đổi thông tin cảng biển.!");
        }
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
        
        String md_sanpham_id =context.Request.Form["md_sanpham_id"];
        String md_cangbien_id = context.Request.Form["md_cangbien_id"];
        String sel = "select COUNT(*) as count from md_cangxuathang where md_sanpham_id = @md_sanpham_id AND md_cangbien_id = @md_cangbien_id";
        int colCount = (int)mdbc.ExecuteScalar(sel, "@md_sanpham_id", md_sanpham_id, "@md_cangbien_id", md_cangbien_id);
        
        if (colCount != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã tồn tại cảng biển.!");
        }
        else
        {
            md_cangxuathang mnu = new md_cangxuathang
            {
                md_cangxuathang_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                md_sanpham_id = md_sanpham_id,
                md_cangbien_id = md_cangbien_id,
                macdinh = macdinh,

                mota = context.Request.Form["mota"],
                hoatdong = hd,
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context)
            };

            db.md_cangxuathangs.InsertOnSubmit(mnu);
            db.SubmitChanges();
            jqGridHelper.Utils.writeResult(1, "Đã tạo cảng biển cho sản phẩm.!");
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        var cxhang = db.md_cangxuathangs.Single(cx=>cx.md_cangxuathang_id.Equals(id));
        int cout_cx = (from lcx in db.md_cangxuathangs
                      where lcx.md_sanpham_id.Equals(cxhang.md_sanpham_id)
                      select new {lcx.md_sanpham_id}).Count();
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");

        if (cout_cx >1)
        {
            String sql = "delete md_cangxuathang where md_cangxuathang_id IN (" + id + ")";
            mdbc.ExcuteNonQuery(sql);
            jqGridHelper.Utils.writeResult(1, "Xóa dòng thành công!");

            string select = "select top 1 md_cangxuathang_id from md_cangxuathang where md_sanpham_id='" + cxhang.md_sanpham_id + "'";
            string mcx_id = (string)mdbc.ExecuteScalar(select);
            string update_cx = "update md_cangxuathang set macdinh=1 where md_cangxuathang_id ='"+mcx_id+"'";
            mdbc.ExcuteNonQuery(update_cx);
        }
        if (cout_cx == 1)
        {
            jqGridHelper.Utils.writeResult(0, "Cần phải tồn tại ít nhất một cảng mặc định cho sản phẩm!");
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
            " FROM md_cangxuathang cxh, md_sanpham sp, md_cangbien cb " +
            " WHERE cxh.md_sanpham_id = sp.md_sanpham_id " +
            " AND cxh.md_cangbien_id = cb.md_cangbien_id" +
            " AND cxh.md_sanpham_id = N'{0}' {1} ";

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
            sidx = "cxh.md_cangxuathang_id";
        }

        string strsql = "select * from( " +
            " select cxh.md_cangxuathang_id, sp.md_sanpham_id, cb.ten_cangbien, cxh.macdinh " +
            " , cxh.ngaytao, cxh.nguoitao, cxh.ngaycapnhat, cxh.nguoicapnhat, cxh.mota, cxh.hoatdong " +
            " ,  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_cangxuathang cxh, md_sanpham sp, md_cangbien cb " +
            " WHERE cxh.md_sanpham_id = sp.md_sanpham_id " +
            " AND cxh.md_cangbien_id = cb.md_cangbien_id " +
            " AND cxh.md_sanpham_id = N'{0}' {1} " +
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
