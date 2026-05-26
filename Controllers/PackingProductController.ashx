<%@ WebHandler Language="C#" Class="PackingProductController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class PackingProductController : IHttpHandler
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
        String sql = "select md_donggoisanpham_id, md_donggoisanpham_id from md_donggoisanpham where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        String ten_donggoi = context.Request.Form["md_donggoi_id"];
        String sanpham_id = context.Request.Form["sanpham_id"];
        decimal soluong = decimal.Parse(context.Request.Form["soluong"]);



        String h = context.Request.Form["hoatdong"].ToLower();
        String md = context.Request.Form["macdinh"].ToLower();
        bool hd, macdinh;
        hd = macdinh = false;

        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        if (md.Equals("on") || md.Equals("true"))
        { macdinh = true; }



        if (macdinh == true)
        {
            String update = "update md_donggoisanpham set macdinh = 0 where md_sanpham_id = @sanpham_id";
            mdbc.ExcuteNonQuery(update, "@sanpham_id", sanpham_id);
        }

        md_donggoisanpham m = db.md_donggoisanphams.Single(p => p.md_donggoisanpham_id.Equals(context.Request.Form["id"]));
        m.md_donggoi_id = ten_donggoi;
        m.md_sanpham_id = sanpham_id;
        m.soluong = soluong;
        m.macdinh = macdinh;

        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaycapnhat = DateTime.Now;

        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        try
        {
            String md_donggoi_id = context.Request.Form["md_donggoi_id"];
            String sanpham_id = context.Request.Form["sanpham_id"];
            String tensp = context.Request.Form["tensp"];
            decimal soluong = decimal.Parse(context.Request.Form["soluong"]);

            String h = context.Request.Form["hoatdong"].ToLower();
            bool hd;
            hd = false;

            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }

            String checkProduct = "select COUNT(*) as count FROM md_donggoisanpham WHERE md_donggoi_id = @md_donggoi_id AND md_sanpham_id = @md_sanpham_id";
            int count = (int)mdbc.ExecuteScalar(checkProduct, "@md_donggoi_id", md_donggoi_id, "@md_sanpham_id", sanpham_id);

            if (count > 0)
            {
                jqGridHelper.Utils.writeResult(0, "Mã sản phẩm " + tensp + " đã có trong đóng gói này");
            }
            else
            {
                String update = "update md_donggoisanpham set macdinh = 0 where md_sanpham_id = @md_sanpham_id";
                mdbc.ExcuteNonQuery(update, "@md_sanpham_id", sanpham_id);

                md_donggoisanpham mnu = new md_donggoisanpham
                {
                    md_donggoisanpham_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                    md_donggoi_id = md_donggoi_id,
                    md_sanpham_id = sanpham_id,
                    soluong = soluong,
                    macdinh = true,

                    mota = context.Request.Form["mota"],
                    hoatdong = hd,
                    nguoitao = UserUtils.getUser(context),
                    nguoicapnhat = UserUtils.getUser(context),
                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now
                };

                db.md_donggoisanphams.InsertOnSubmit(mnu);
                db.SubmitChanges();
                jqGridHelper.Utils.writeResult(1, "Thêm mới thành công!");
            }
        }catch(Exception ex){
            jqGridHelper.Utils.writeResult(0, ex.Message);
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_donggoisanpham where md_donggoisanpham_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String packingId = context.Request.QueryString["packingId"];

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }


        String sqlCount = "SELECT COUNT(*) AS count FROM md_donggoisanpham dgsp, md_donggoi dg, md_sanpham sp WHERE dgsp.md_donggoi_id = dg.md_donggoi_id AND dgsp.md_sanpham_id = sp.md_sanpham_id AND dgsp.md_donggoi_id = N'{0}' " + filter;

        if (packingId != null)
        {
            sqlCount = string.Format(sqlCount, packingId);
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
            sidx = "md_donggoisanpham_id";
        }

        string strsql = "select * from( " + 
            " select  dgsp.md_donggoisanpham_id, dg.md_donggoi_id, sp.md_sanpham_id, " +
            " sp.ma_sanpham, dgsp.soluong, dgsp.macdinh, dgsp.ngaytao, dgsp.nguoitao, " +
            " dgsp.ngaycapnhat, dgsp.nguoicapnhat, dgsp.mota, dgsp.hoatdong, " +
            "  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_donggoisanpham dgsp, md_donggoi dg, md_sanpham sp " +
            " WHERE dgsp.md_donggoi_id = dg.md_donggoi_id " +
            " AND dgsp.md_sanpham_id = sp.md_sanpham_id " + filter +
            " AND dgsp.md_donggoi_id = N'{0}')P where RowNum > @start AND RowNum < @end";

        if (packingId != null)
        {
            strsql = string.Format(strsql, packingId);
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
