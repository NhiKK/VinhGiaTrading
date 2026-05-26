<%@ WebHandler Language="C#" Class="QuyDinhVoiKhachHangController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class QuyDinhVoiKhachHangController : IHttpHandler
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
        String sql = "select md_hanghoadocquyen_id, md_hanghoadocquyen_id from md_hanghoadocquyen hhdq JOIN md_sanpham sp ON hhdq.md_sanpham_id = sp.md_sanpham_id where hhdq.hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_hanghoadocquyen m = db.md_hanghoadocquyens.Single(p => p.md_hanghoadocquyen_id == context.Request.Form["id"]);
        m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        m.md_sanpham_id = context.Request.Form["sanpham_id"];
        
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

        md_hanghoadocquyen mnu = new md_hanghoadocquyen
        {
            md_hanghoadocquyen_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"],
            md_sanpham_id = context.Request.Form["sanpham_id"],
            mota = context.Request.Form["mota"],
            
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_hanghoadocquyens.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update md_hanghoadocquyen set hoatdong = 0 where md_hanghoadocquyen_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        String productId = context.Request.QueryString["productId"];
        
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM md_hanghoadocquyen hhdq, md_sanpham sp, md_doitackinhdoanh dtkd " +
            " WHERE hhdq.md_sanpham_id = sp.md_sanpham_id " +
            " AND hhdq.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " AND hhdq.md_sanpham_id = @md_sanpham_id";

        
        
        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        String sidx = context.Request.QueryString["sidx"];
        String sord = context.Request.QueryString["sord"];
        int total_page;
        int count = (int)mdbc.ExecuteScalar(sqlCount, "@md_sanpham_id", productId != null ? productId : "0");
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
            sidx = "md_hanghoadocquyen_id";
        }

        string strsql = "select * from( " +
            " SELECT hhdq.md_hanghoadocquyen_id, dtkd.ma_dtkd as tendtkd, hhdq.ngaybatdau, hhdq.ngayketthuc, hhdq.mota, sp.md_sanpham_id as sanpham_id, sp.ma_sanpham as tensp, " +
            " hhdq.ngaytao, hhdq.nguoitao, hhdq.ngaycapnhat, hhdq.nguoicapnhat, hhdq.hoatdong, hhdq.ghichu, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_hanghoadocquyen hhdq, md_sanpham sp, md_doitackinhdoanh dtkd " +
            " WHERE hhdq.md_sanpham_id = sp.md_sanpham_id " +
            " AND hhdq.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " AND hhdq.md_sanpham_id =  @md_sanpham_id" +
            ")P WHERE RowNum > @start AND RowNum < @end";

        if (productId != null)
        {
            strsql = String.Format(strsql, productId);
        }
        else
        {
            strsql = String.Format(strsql, 0);
        }

        System.Data.DataTable dt = mdbc.GetData(strsql, "@md_sanpham_id", productId != null ? productId : "0", "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_hanghoadocquyen_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tendtkd"] + "]]></cell>";
			xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
			
			if(!string.IsNullOrEmpty(row["ngaybatdau"] + ""))
				xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaybatdau"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
			else
				xml += "<cell><![CDATA[]]></cell>";
			
			if(!string.IsNullOrEmpty(row["ngayketthuc"] + ""))
				xml += "<cell><![CDATA[" + DateTime.Parse(row["ngayketthuc"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
			else
				xml += "<cell><![CDATA[]]></cell>";
			
            xml += "<cell><![CDATA[" + row["sanpham_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tensp"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghichu"] + "]]></cell>";
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
