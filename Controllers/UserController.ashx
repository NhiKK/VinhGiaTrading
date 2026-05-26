<%@ WebHandler Language="C#" Class="UserController" %>

using System;
using System.Web;
using System.Linq;

public class UserController : IHttpHandler
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
        String sql = "select MANV, MANV as ten from NHANVIEN where hoatdong = 1 order by manv asc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        String ad = context.Request.Form["isadmin"].ToLower();
        bool hd = false;

        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        if (ad.Equals("on") || ad.Equals("true"))
        {
            ad = "true";
        }
        else
        {
            ad = "false";
        }
        bool remote = context.Request.Form["remote"].ToLower() == "true";
        bool canOTP = context.Request.Form["canOTP"].ToLower() == "true";
        bool suaGia = context.Request.Form["suaGia"].ToLower() == "true";
        bool hieuLucDH = context.Request.Form["hieuLucDH"].ToLower() == "true";
        bool themPhiSauHL = context.Request.Form["themPhiSauHL"].ToLower() == "true";
        bool doiNCU = context.Request.Form["doiNCU"].ToLower() == "true";
        bool suaPI = context.Request.Form["suaPI"].ToLower() == "true";
        bool email_xn = context.Request.Form["email_xn"].ToLower() == "true";

        String mk = context.Request.Form["matkhau"];

        nhanvien u = db.nhanviens.Single(p => p.manv == context.Request.Form["id"]);
        u.matkhau = mk != "" ? Security.EncodeMd5Hash(mk) : u.matkhau;
        u.mavt = context.Request.Form["tenvt"];
        u.mota = context.Request.Form["mota"];
        u.hoten = context.Request.Form["hoten"];
        u.email = context.Request.Form["email"];
        u.email_pass = context.Request.Form["email_pass"] == "" ? u.email_pass : context.Request.Form["email_pass"];
        u.smtp = context.Request.Form["smtp"];
        u.email_cc = context.Request.Form["email_cc"];
        u.dienthoai = context.Request.Form["dienthoai"];
        u.diachi = context.Request.Form["diachi"];
        u.isadmin = bool.Parse(ad);
        u.hoatdong = hd;
        u.remote = remote;
        u.canOTP = canOTP;
        u.suaGia = suaGia;
        u.hieuLucDH = hieuLucDH;
        u.themPhiSauHL = themPhiSauHL;
        u.doiNCU = doiNCU;
        u.suaPI = suaPI;
        u.email_xn = email_xn;

        u.ngaycapnhat = DateTime.Now;
        u.nguoicapnhat = UserUtils.getUser(context);

        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        String ad = context.Request.Form["isadmin"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        if (ad.Equals("on") || ad.Equals("true"))
        {
            ad = "true";
        }
        else
        {
            ad = "false";
        }

        bool remote = context.Request.Form["remote"].ToLower() == "true";
        bool canOTP = context.Request.Form["canOTP"].ToLower() == "true";
        bool suaGia = context.Request.Form["suaGia"].ToLower() == "true";
        bool hieuLucDH = context.Request.Form["hieuLucDH"].ToLower() == "true";
        bool themPhiSauHL = context.Request.Form["themPhiSauHL"].ToLower() == "true";
        bool doiNCU = context.Request.Form["doiNCU"].ToLower() == "true";
        bool suaPI = context.Request.Form["suaPI"].ToLower() == "true";
        bool email_xn = context.Request.Form["email_xn"].ToLower() == "true";

        String manv = context.Request.Form["manv"];

        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
        if (nv == null)
        {
            nhanvien user = new nhanvien
            {
                manv = context.Request.Form["manv"],
                matkhau = Security.EncodeMd5Hash(context.Request.Form["matkhau"]),
                mavt = context.Request.Form["tenvt"],
                hoten = context.Request.Form["hoten"],
                email = context.Request.Form["email"],
                email_pass = context.Request.Form["email_pass"],
                smtp = context.Request.Form["smtp"],
                email_cc = context.Request.Form["email_cc"],
                dienthoai = context.Request.Form["dienthoai"],
                diachi = context.Request.Form["diachi"],
                isadmin = bool.Parse(ad),
                remote = remote,
                canOTP = canOTP,
                suaGia = suaGia,
                hieuLucDH = hieuLucDH,
                themPhiSauHL = themPhiSauHL,
                doiNCU = doiNCU,
                suaPI = suaPI,
                email_xn = email_xn,
                mota = context.Request.Form["mota"],
                hoatdong = hd,

                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            db.nhanviens.InsertOnSubmit(user);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Tài khoản đã tồn tại.!");
        }

    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update nhanvien set hoatdong = 0 where manv IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
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

        String sqlCount = "SELECT COUNT(*) AS count from nhanvien nv left join vaitro vt on nv.mavt = vt.mavt where 1=1 " + filter;
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
            sidx = "nv.manv";
        }

        string strsql = string.Format(@"
			select * 
			from ( 
				select 
					nv.*, vt.tenvt as tenvt, smtp.smtpserver, ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum 
				from nhanvien nv 
					left join vaitro vt on nv.mavt = vt.mavt 
					left join md_smtp smtp on nv.smtp = smtp.md_smtp_id 
				where 
					1=1 {2}  
			)P 
			where RowNum > @start AND RowNum < @end
		",
        sidx,
        sord,
        filter
        );

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["manv"] + "]]></cell>";
            xml += "<cell><![CDATA[" + "" + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenvt"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoten"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["email"] + "]]></cell>";
            xml += "<cell><![CDATA[" + "" + "]]></cell>";
            xml += "<cell><![CDATA[" + row["smtpserver"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["email_cc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["dienthoai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["otp"] + "]]></cell>";

            xml += "<cell><![CDATA[" + row["diachi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isadmin"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["remote"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["canOTP"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["suaGia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hieuLucDH"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["themPhiSauHL"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["doiNCU"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["suaPI"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["email_xn"] + "]]></cell>";


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