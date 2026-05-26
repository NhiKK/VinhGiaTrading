<%@ WebHandler Language="C#" Class="DongXacNhanXuatKhoController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class DongXacNhanXuatKhoController : IHttpHandler
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
        String sql = "select c_dongxacnhan_xuatkho_id, c_dongxacnhan_xuatkho_id from c_dongxacnhan_xuatkho where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_dongxacnhan_xuatkho m = db.c_dongxacnhan_xuatkhos.Single(p => p.c_dongxacnhan_xuatkho_id == context.Request.Form["id"]);
        m.c_dongdonhang_id = context.Request.Form["c_dongdonhang_id"];
        m.md_sanpham_id = context.Request.Form["md_sanpham_id"];
        m.sl_po = decimal.Parse(context.Request.Form["sl_po"]);
        m.sl_yeucauxuat = decimal.Parse(context.Request.Form["sl_yeucauxuat"]);
        m.sl_thucxuat = decimal.Parse(context.Request.Form["sl_thucxuat"]);
        m.ma_sanpham_khach = context.Request.Form["ma_sanpham_khach"];
        m.mota_tienganh = context.Request.Form["mota_tienganh"];
        m.dvt_sanpham = context.Request.Form["dvt_sanpham"];
        m.md_donggoi_id = context.Request.Form["md_donggoi_id"];
        m.so_bien = context.Request.Form["so_bien"];
        m.ghichu = context.Request.Form["ghichu"];
        
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
        
        c_dongxacnhan_xuatkho mnu = new c_dongxacnhan_xuatkho
        {
            c_dongxacnhan_xuatkho_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            c_dongdonhang_id = context.Request.Form["c_dongdonhang_id"],
            md_sanpham_id = context.Request.Form["md_sanpham_id"],
            sl_po = decimal.Parse(context.Request.Form["sl_po"]),
            sl_yeucauxuat = decimal.Parse(context.Request.Form["sl_yeucauxuat"]),
            sl_thucxuat = decimal.Parse(context.Request.Form["sl_thucxuat"]),
            ma_sanpham_khach = context.Request.Form["ma_sanpham_khach"],
            mota_tienganh = context.Request.Form["mota_tienganh"],
            dvt_sanpham = context.Request.Form["dvt_sanpham"],
            md_donggoi_id = context.Request.Form["md_donggoi_id"],
            so_bien = context.Request.Form["so_bien"],
            ghichu = context.Request.Form["ghichu"],
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.c_dongxacnhan_xuatkhos.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update c_dongxacnhan_xuatkho set hoatdong = 0 where c_dongxacnhan_xuatkho_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String pxkId = context.Request.QueryString["pxkId"];
        
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_dongxacnhan_xuatkho dxnxk, c_xacnhan_xuatkho xnxk, md_sanpham sp, md_donvitinh dvt " +
            " WHERE dxnxk.c_xacnhan_xuatkho_id = xnxk.c_xacnhan_xuatkho_id " +
            " AND dxnxk.md_sanpham_id = sp.md_sanpham_id " +
            " AND dxnxk.dvt_sanpham = dvt.md_donvitinh_id " +
            " AND dxnxk.c_xacnhan_xuatkho_id = N'{0}'";
        
        if (pxkId != null)
        {
            sqlCount = String.Format(sqlCount, pxkId);
        }
        else { sqlCount = String.Format(sqlCount, 0); }
        
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
            sidx = "dxnxk.c_dongxacnhan_xuatkho_id";
        }

        string strsql = "select * from( " +
            " select dxnxk.c_dongxacnhan_xuatkho_id, xnxk.sochungtu as ct_xk " +
            " , dxnxk.c_dongdonhang_id, sp.md_sanpham_id, sp.ma_sanpham, dxnxk.sl_po, dxnxk.sl_yeucauxuat " +
            " , dxnxk.sl_thucxuat, dxnxk.ma_sanpham_khach " +
            " , dxnxk.mota_tienganh, dvt.ten_dvt, dxnxk.md_donggoi_id, dxnxk.so_bien, dxnxk.ghichu " +
            " , dxnxk.ngaytao, dxnxk.nguoitao, dxnxk.ngaycapnhat, dxnxk.nguoicapnhat " +
            " , dxnxk.mota, dxnxk.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_dongxacnhan_xuatkho dxnxk, c_xacnhan_xuatkho xnxk, md_sanpham sp, md_donvitinh dvt " +
            " WHERE dxnxk.c_xacnhan_xuatkho_id = xnxk.c_xacnhan_xuatkho_id " +
            " AND dxnxk.md_sanpham_id = sp.md_sanpham_id " +
            " AND dxnxk.dvt_sanpham = dvt.md_donvitinh_id " +
            " AND dxnxk.c_xacnhan_xuatkho_id = N'{0}' " +
            " )P where RowNum > @start AND RowNum < @end";
        

        if (pxkId != null)
        {
            strsql = String.Format(strsql, pxkId);
        }
        else { strsql = String.Format(strsql, 0); }
        
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
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[14].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[15] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[16].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[17] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[18] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[19] + "]]></cell>";
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
