<%@ WebHandler Language="C#" Class="RateController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class RateController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getusdtovnd":
                this.getUsdToVnd(context);
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
        String sql = "select md_tygia_id, ten_tygia from md_tygia where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void getUsdToVnd(HttpContext context)
    {
        md_dongtien usd = db.md_dongtiens.FirstOrDefault(p => p.ma_iso.Equals("USD"));
        md_dongtien vnd = db.md_dongtiens.FirstOrDefault(p => p.ma_iso.Equals("VND"));
		
		string md_dongtien_id  = context.Request.QueryString["md_dongtien_id"];
		
		if(md_dongtien_id == "")
		{
			 md_dongtien_id= "385ec93024915838c98ef66e58b02e9b";
		}
		
        decimal quydoi = 0;
        if (usd == null || vnd == null)
        {
            quydoi = 0;
        }
        else {
            md_tygia tg = db.md_tygias.FirstOrDefault(
                    p => p.tu_dongtien_id.Equals(usd.md_dongtien_id)
                    && p.sang_dongtien_id.Equals(vnd.md_dongtien_id)
            );
            
            if (tg == null)
            {
                quydoi = 0;
            }
            else {
                if (md_dongtien_id == "964dd487f30e799f585cfab3ec5a178e") // VND
                {
                    quydoi = 1;
                }
				else if (md_dongtien_id == "385ec93024915838c98ef66e58b02e9b") // USD
                {
                     quydoi = tg.chia_cho == null ? 0 : tg.chia_cho.Value;
                }
                else
                {
                    quydoi = tg.nhan_voi == null ? 0 : tg.nhan_voi.Value;
                }
            }
        }

        context.Response.Write(quydoi);
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_tygia m = db.md_tygias.Single(p => p.md_tygia_id == context.Request.Form["id"]);
        m.ten_tygia = context.Request.Form["ten_tygia"];
        m.tu_dongtien_id = context.Request.Form["tu_dongtien"];
        m.sang_dongtien_id = context.Request.Form["sang_dongtien"];
        m.hieuluc_tungay = DateTime.ParseExact(context.Request.Form["hieuluc_tungay"],"dd/MM/yyyy", null);
        m.hieuluc_denngay = DateTime.ParseExact(context.Request.Form["hieuluc_denngay"], "dd/MM/yyyy", null);
        m.nhan_voi = decimal.Parse(context.Request.Form["nhan_voi"]);
        m.chia_cho = decimal.Parse(context.Request.Form["chia_cho"]);
        
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
        
        md_tygia mnu = new md_tygia
        {
            md_tygia_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            ten_tygia = context.Request.Form["ten_tygia"],
            tu_dongtien_id = context.Request.Form["tu_dongtien_id"],
            sang_dongtien_id = context.Request.Form["sang_dongtien_id"],
            hieuluc_tungay = DateTime.ParseExact(context.Request.Form["hieuluc_tungay"],"dd/MM/yyyy", null),
            hieuluc_denngay = DateTime.ParseExact(context.Request.Form["hieuluc_denngay"],"dd/MM/yyyy", null),
            nhan_voi = decimal.Parse(context.Request.Form["nhan_voi"]),
            chia_cho = decimal.Parse(context.Request.Form["chia_cho"]),
        
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_tygias.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update md_tygia set hoatdong = 0 where md_tygia_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count FROM md_tygia";
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
            sidx = "tg.ten_tygia";
        }

        string strsql = "select * from( " +
            " select md_tygia_id, ten_tygia,  (select ma_iso from md_dongtien dt where dt.md_dongtien_id = tg.tu_dongtien_id) as tu_dongtien, " +
            " (select ma_iso from md_dongtien dt where dt.md_dongtien_id = tg.sang_dongtien_id) as sang_dongtien, " +
            " hieuluc_tungay, hieuluc_denngay, nhan_voi, chia_cho, " +
            " ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, mota, hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " from md_tygia tg)P where RowNum > @start AND RowNum < @end";

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_tygia_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_tygia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tu_dongtien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sang_dongtien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["hieuluc_tungay"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["hieuluc_denngay"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nhan_voi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chia_cho"] + "]]></cell>";
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
