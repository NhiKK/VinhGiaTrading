<%@ WebHandler Language="C#" Class="XacNhanXuatKhoController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class XacNhanXuatKhoController : IHttpHandler
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
        String sql = "select c_xacnhan_xuatkho_id, sochungtu from c_xacnhan_xuatkho where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_xacnhan_xuatkho m = db.c_xacnhan_xuatkhos.Single(p => p.c_xacnhan_xuatkho_id == context.Request.Form["id"]);
        m.sochungtu = context.Request.Form["sochungtu"];
        m.ngaylap = DateTime.ParseExact(context.Request.Form["ngaylap"], "dd/MM/yyyy", null);
        m.nguoilap = context.Request.Form["nguoilap"];
        m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        m.md_kho_id = context.Request.Form["md_kho_id"];
        m.so_po = context.Request.Form["so_po"];
        m.c_donhang_id = context.Request.Form["c_donhang_id"];
        m.so_cont = decimal.Parse(context.Request.Form["so_cont"]);
        m.so_seal = decimal.Parse(context.Request.Form["so_seal"]);
        m.loai = context.Request.Form["loai"];
        m.ghichu = context.Request.Form["ghichu"];
        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.ngaycapnhat = DateTime.Now;
        m.nguoicapnhat = UserUtils.getUser(context);
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        
        c_xacnhan_xuatkho mnu = new c_xacnhan_xuatkho
        {
            c_xacnhan_xuatkho_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            sochungtu = context.Request.Form["sochungtu"],
            ngaylap = DateTime.ParseExact(context.Request.Form["ngaylap"], "dd/MM/yyyy", null),
            nguoilap = context.Request.Form["nguoilap"],
            md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"],
            md_kho_id = context.Request.Form["md_kho_id"],
            so_po = context.Request.Form["so_po"],
            c_donhang_id = context.Request.Form["c_donhang_id"],
            so_cont = decimal.Parse(context.Request.Form["so_cont"]),
            so_seal = decimal.Parse(context.Request.Form["so_seal"]),
            loai = context.Request.Form["loai"],
            ghichu = context.Request.Form["ghichu"],
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now,
            
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
        };

        db.c_xacnhan_xuatkhos.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update c_xacnhan_xuatkho set hoatdong = 0 where c_xacnhan_xuatkho_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_xacnhan_xuatkho xnxk, md_doitackinhdoanh dtkd, md_kho kho, c_donhang dh " +
            " WHERE  xnxk.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " AND xnxk.c_donhang_id = dh.c_donhang_id " +
            " AND xnxk.md_kho_id = kho.md_kho_id ";
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
            sidx = "xnxk.c_xacnhan_xuatkho_id";
        }

        string strsql = "select * from( " +
            " select xnxk.c_xacnhan_xuatkho_id, xnxk.sochungtu as ct_xk, xnxk.ngaylap, xnxk.nguoilap " +
            " , dtkd.ma_dtkd, kho.ten_kho, xnxk.so_po, dh.sochungtu as ct_dh, xnxk.so_cont, xnxk.so_seal " +
            " , xnxk.loai, xnxk.ghichu, xnxk.ngaytao, xnxk.nguoitao, xnxk.ngaycapnhat, xnxk.nguoicapnhat " +
            " , xnxk.mota, xnxk.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_xacnhan_xuatkho xnxk, md_doitackinhdoanh dtkd, md_kho kho, c_donhang dh " +
            " WHERE  xnxk.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " AND xnxk.md_kho_id = kho.md_kho_id " +
            " AND xnxk.c_donhang_id = dh.c_donhang_id " +
            " )P where RowNum > @start AND RowNum < @end";

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
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[12].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[14].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[15] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[16] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[17] + "]]></cell>";
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
