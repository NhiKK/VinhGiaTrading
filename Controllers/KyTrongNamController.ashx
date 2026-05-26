<%@ WebHandler Language="C#" Class="KyTrongNamController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class KyTrongNamController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "autocreate":
                this.createList(context);
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

    public void createList(HttpContext context)
    {
        String yearId = context.Request.QueryString["yearId"];
        String sqlCheck = "SELECT COUNT(*) AS count FROM a_kytrongnam WHERE a_namtaichinh_id = @yearId";
        int count = (int)mdbc.ExecuteScalar(sqlCheck, "@yearId", yearId);

        int year = 0;
        String getYear = "Select nam from a_namtaichinh where a_namtaichinh_id = @yearId";
        System.Data.SqlClient.SqlDataReader rd = mdbc.ExecuteReader(getYear, "@yearId", yearId);
        if (rd.HasRows)
        {
            rd.Read();
            year = int.Parse(rd[0].ToString());
        }

        if (count > 0)
        {
            // da co ky cho nam
            context.Response.Write("Năm tài chính đã có kỳ.!");
        }
        else
        {
            // chua co ky
            for (int i = 1; i <= 12; i++)
            {
                String ngaybatdau = String.Format("0{0}/0{1}/{2}", 1, i, year);
                String ngayketthuc = String.Format("{0}/0{1}/{2}", DateTime.DaysInMonth(year, i), i, year);
                if(i > 9)
                {
                    ngaybatdau = String.Format("0{0}/{1}/{2}", 1, i, year);
                    ngayketthuc = String.Format("{0}/{1}/{2}", DateTime.DaysInMonth(year, i), i, year);
                }
                
                a_kytrongnam ky = new a_kytrongnam();
                ky.a_kytrongnam_id = Security.EncodeMd5Hash(yearId + i + DateTime.Now.ToString("yyyyMMddddhhmmss"));
                ky.a_namtaichinh_id = yearId;
                ky.soky = i;
                ky.tenky = "Kỳ " + i;
                ky.loaiky = "CHUAN";

                ky.ngaybatdau = DateTime.ParseExact(ngaybatdau, "dd/MM/yyyy", null);
                ky.ngayketthuc = DateTime.ParseExact(ngayketthuc, "dd/MM/yyyy", null);
                ky.ngaytao = DateTime.Now;
                ky.ngaycapnhat = DateTime.Now;
                ky.hoatdong = true;
                
                db.a_kytrongnams.InsertOnSubmit(ky);
                db.SubmitChanges();
            }
            context.Response.Write("Các kỳ đã được tạo cho năm tài chính mà bạn chọn.!");
        }
    }

    public void getSelectOption(HttpContext context)
    {
        String yearId = context.Request.QueryString["yearId"];
        String sql = "select a_kytrongnam_id, tenky from a_kytrongnam where a_namtaichinh_id = N'" + yearId + "' AND hoatdong = 1 order by soky asc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToOptionList());
    }


    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        a_kytrongnam m = db.a_kytrongnams.Single(p => p.a_kytrongnam_id == context.Request.Form["id"]);
        m.a_namtaichinh_id = context.Request.Form["a_namtaichinh_id"];
        m.soky = int.Parse(context.Request.Form["soky"]);
        m.tenky = context.Request.Form["tenky"];
        m.ngaybatdau = DateTime.ParseExact(context.Request.Form["ngaybatdau"], "dd/MM/yyyy", null);
        m.ngayketthuc = DateTime.ParseExact(context.Request.Form["ngayketthuc"], "dd/MM/yyyy", null);
        m.loaiky = context.Request.Form["loaiky"];
        
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

        a_kytrongnam mnu = new a_kytrongnam
        {
            a_kytrongnam_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            a_namtaichinh_id = context.Request.Form["a_namtaichinh_id"],
            soky = int.Parse(context.Request.Form["soky"]),
            tenky = context.Request.Form["tenky"],
            ngaybatdau = DateTime.ParseExact(context.Request.Form["ngaybatdau"], "dd/MM/yyyy", null),
            ngayketthuc = DateTime.ParseExact(context.Request.Form["ngayketthuc"], "dd/MM/yyyy", null),
            loaiky = context.Request.Form["loaiky"],
        
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.a_kytrongnams.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update a_kytrongnam set hoatdong = 0 where a_kytrongnam_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String yearId = context.Request.QueryString["yearId"];
        
        String sqlCount = "SELECT COUNT(*) AS count FROM a_kytrongnam ktn, a_namtaichinh namtc WHERE ktn.a_namtaichinh_id = namtc.a_namtaichinh_id AND ktn.a_namtaichinh_id = N'{0}'";
        
        if (yearId != null)
        {
            sqlCount = string.Format(sqlCount, yearId);
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
            sidx = "ktn.soky";
        }

        string strsql = "select * from( " +
            " select ktn.a_kytrongnam_id, namtc.a_namtaichinh_id as a_namtaichinh_id " +
            " , ktn.soky, ktn.tenky, ktn.ngaybatdau, ktn.ngayketthuc " +
            " , ktn.loaiky, ktn.ngaytao, ktn.nguoitao, ktn.ngaycapnhat " +
            " , ktn.nguoicapnhat, ktn.mota, ktn.hoatdong " +
            " ,  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM a_kytrongnam ktn, a_namtaichinh namtc " +
            " WHERE ktn.a_namtaichinh_id = namtc.a_namtaichinh_id " +
            " AND ktn.a_namtaichinh_id = N'{0}' " +
            " )P where RowNum > @start AND RowNum < @end";

        if (yearId != null)
        {
            strsql = string.Format(strsql, yearId);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[4].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[5].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[7].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[9].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
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
