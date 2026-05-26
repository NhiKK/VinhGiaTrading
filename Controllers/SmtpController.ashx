<%@ WebHandler Language="C#" Class="SmtpController" %>

using System;
using System.Web;
using System.Linq;

public class SmtpController : IHttpHandler {
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
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
    
    public void edit(HttpContext context)
    {
        String md_smtp_id = context.Request.Form["md_smtp_id"];
        String ten = context.Request.Form["ten"];
        String smtpserver = context.Request.Form["smtpserver"];
        String port = context.Request.Form["port"];
        String macdinh = context.Request.Form["macdinh"].ToLower();
        String use_ssl = context.Request.Form["use_ssl"].ToLower();
        
        if (macdinh.Equals("on") || macdinh.Equals("true"))
        {
            macdinh = "true";
            String update = "update md_smtp set macdinh = 0";
            mdbc.ExcuteNonQuery(update);
        }
        else
        {
            macdinh = "false";
        }

        if (use_ssl.Equals("on") || use_ssl.Equals("true"))
        {
            use_ssl = "true";
        }
        else
        {
            use_ssl = "false";
        }

        md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.md_smtp_id.Equals(md_smtp_id));
        smtp.ten = ten;
        smtp.smtpserver = smtpserver;
        smtp.port = port == "" ? 25 : int.Parse(port);
        smtp.macdinh = bool.Parse(macdinh);
        smtp.use_ssl = bool.Parse(use_ssl);
        
        smtp.ngaycapnhat = DateTime.Now;
        smtp.nguoicapnhat = UserUtils.getUser(context);
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String md_smtp_id = ImportUtils.getNEWID();
        String ten = context.Request.Form["ten"];
        String smtpserver = context.Request.Form["smtpserver"];
        String port = context.Request.Form["port"];
        String macdinh = context.Request.Form["macdinh"].ToLower();
        String use_ssl = context.Request.Form["use_ssl"].ToLower();

        if (macdinh.Equals("on") || macdinh.Equals("true"))
        {
            macdinh = "true";
            String update = "update md_smtp set macdinh = 0";
            mdbc.ExcuteNonQuery(update);
        }
        else
        {
            macdinh = "false";
        }

        if (use_ssl.Equals("on") || use_ssl.Equals("true"))
        {
            use_ssl = "true";
        }
        else
        {
            use_ssl = "false";
        }

        md_smtp smtp = new md_smtp();
        smtp.md_smtp_id = md_smtp_id;
        smtp.ten = ten;
        smtp.smtpserver = smtpserver;
        smtp.port = port == "" ? 25 : int.Parse(port);
        smtp.macdinh = bool.Parse(macdinh);
        smtp.use_ssl = bool.Parse(use_ssl);

        smtp.nguoitao = UserUtils.getUser(context);
        smtp.ngaytao = DateTime.Now;
        smtp.ngaycapnhat = DateTime.Now;
        smtp.nguoicapnhat = UserUtils.getUser(context);
        smtp.hoatdong = true;
        
        db.md_smtps.InsertOnSubmit(smtp);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.md_smtp_id.Equals(id));
        if (smtp.macdinh.Equals(true))
        {
            db.md_smtps.DeleteOnSubmit(smtp);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xuất SMTP mặc định!");
        }
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count from md_smtp ";
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
            sidx = "ten";
        }

        string strsql = "select * from( select *, ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum from md_smtp)P where RowNum > @start AND RowNum < @end";

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_smtp_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["smtpserver"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["port"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["macdinh"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["use_ssl"] + "]]></cell>";
            
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