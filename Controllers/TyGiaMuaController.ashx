<%@ WebHandler Language="C#" Class="TyGiaMuaController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class TyGiaMuaController : IHttpHandler
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
        String sql = "select md_tygia_id, ten_tygia from md_tygia where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void edit(HttpContext context)
    {
        md_dongtien dt_USD = db.md_dongtiens.Where(s=>s.ma_iso == "USD").FirstOrDefault();
        md_dongtien dt_VND = db.md_dongtiens.Where(s=>s.ma_iso == "VND").FirstOrDefault();
        string trangthai = context.Request.Form["trangthai"];
        int nam = int.Parse(context.Request.Form["nam"]);
        int thang = int.Parse(context.Request.Form["ky"]);
        var ngayapdung = new DateTime(nam, thang, 1);
        var ngayketthuc = ngayapdung.AddMonths(1).AddDays(-1);
        
        if (trangthai == "HIEULUC")
        {
            foreach (var item in db.md_tygiamuas.Where(s => s.ngayapdung == ngayapdung & s.ngayketthuc == ngayketthuc))
            {
                item.trangthai = "SOANTHAO";
            }
        }

        md_tygiamua m = db.md_tygiamuas.FirstOrDefault(p => p.md_tygiamua_id == context.Request.Form["id"]);
        m.trangthai = trangthai;
        m.ten_tygia = "Kỳ "+ thang;
        m.tu_dongtien = dt_USD.md_dongtien_id;
        m.sang_dongtien = dt_VND.md_dongtien_id;
        m.ngayapdung = ngayapdung;
        m.ngayketthuc = ngayketthuc;
        m.nhan_voi = float.Parse(context.Request.Form["nhan_voi"]);

        m.mota = context.Request.Form["mota"];
        m.hoatdong = true;
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaycapnhat = DateTime.Now;
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        md_dongtien dt_USD = db.md_dongtiens.Where(s=>s.ma_iso == "USD").FirstOrDefault();
        md_dongtien dt_VND = db.md_dongtiens.Where(s=>s.ma_iso == "VND").FirstOrDefault();
        string trangthai = context.Request.Form["trangthai"];
        int nam = int.Parse(context.Request.Form["nam"]);
        int thang = int.Parse(context.Request.Form["ky"]);
        var ngayapdung = new DateTime(nam, thang, 1);
        var ngayketthuc = ngayapdung.AddMonths(1).AddDays(-1);

        if (trangthai == "HIEULUC")
        {
            foreach (var item in db.md_tygiamuas.Where(s => s.ngayapdung == ngayapdung & s.ngayketthuc == ngayketthuc))
            {
                item.trangthai = "SOANTHAO";
            }
        }

        md_tygiamua mnu = new md_tygiamua
        {
            md_tygiamua_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            trangthai = trangthai,
            ten_tygia = "Kỳ "+ thang,
            tu_dongtien = dt_USD.md_dongtien_id,
            sang_dongtien = dt_VND.md_dongtien_id,
            ngayapdung = ngayapdung,
            ngayketthuc = ngayketthuc,
            nhan_voi = float.Parse(context.Request.Form["nhan_voi"]),

            mota = context.Request.Form["mota"],
            hoatdong = true,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_tygiamuas.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        string id = context.Request.Form["id"];
        md_tygiamua tgm = db.md_tygiamuas.Where(s => s.md_tygiamua_id == id).FirstOrDefault();
        if(tgm != null)
        {
            if(tgm.trangthai == "SOANTHAO")
            {
                db.md_tygiamuas.DeleteOnSubmit(tgm);
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Không được xóa vì tỷ giá đã hiệu lực!");
            }
            db.SubmitChanges();
        }
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        string nam = context.Request.QueryString["nam"];
        if(string.IsNullOrEmpty(nam))
            nam = DateTime.Now.Year.ToString();
        String sqlCount = string.Format(@"
            SELECT COUNT(1) AS count 
            FROM md_tygiamua
            where 
            ngayapdung >= convert(datetime, '01/01/{0} 00:00:00', 103) and
            ngayketthuc <= convert(datetime, '31/12/{0} 23:59:59', 103)
        ", nam);
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

        string strsql = string.Format(@"select * from(
            select md_tygiamua_id, trangthai, ten_tygia,  'USD' as tu_dongtien, 'VND' as sang_dongtien,
            ngayapdung, ngayketthuc, CONVERT(VARCHAR(50), nhan_voi, 128) as nhan_voi, 
            CONVERT(VARCHAR(50), chia_cho, 128) as chia_cho,
            ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, mota, hoatdong,
            ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
            from md_tygiamua tg
            where 
                ngayapdung >= convert(datetime, '01/01/{2} 00:00:00', 103) and
                ngayketthuc <= convert(datetime, '31/12/{2} 23:59:59', 103)
        )P where RowNum > @start AND RowNum < @end", sidx, sord, nam);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_tygiamua_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_tygia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + int.Parse(DateTime.Parse(row["ngayapdung"].ToString()).ToString("MM")) + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tu_dongtien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sang_dongtien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngayapdung"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngayketthuc"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";

            xml += "<cell><![CDATA[" + row["nhan_voi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chia_cho"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
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
