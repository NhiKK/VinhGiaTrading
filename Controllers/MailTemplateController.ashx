<%@ WebHandler Language="C#" Class="MailTemplateController" %>


using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class MailTemplateController : IHttpHandler
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
        String sql = "select md_mailtemplate_id, ten_template from md_mailtemplate where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String id = context.Request.Form["id"];
        String ten_template = context.Request.Form["ten_template"];
        String subject_mail = context.Request.Form["subject_mail"];
        String content_mail = context.Request.Form["content_mail"];
        String use_for = context.Request.Form["use_for"];
        String default_mail = context.Request.Form["default_mail"];
        String hoatdong = context.Request.Form["hoatdong"];
        
        // Đổi sang toán logic bool
        if (default_mail.ToLower().Equals("on") || default_mail.ToLower().Equals("true"))
        {
            default_mail = "true";
        }
        else {
            default_mail = "false";
        }

        // Đổi sang toán logic bool
        if (hoatdong.ToLower().Equals("on") || hoatdong.ToLower().Equals("true"))
        {
            hoatdong = "true";
        }
        else
        {
            hoatdong = "false";
        }
        
        md_mailtemplate m = db.md_mailtemplates.Single(p => p.md_mailtemplate_id.Equals(id));
        m.ten_template = ten_template;
        m.subject_mail = subject_mail;
        m.content_mail = content_mail;
        m.use_for = use_for;
        m.default_mail = bool.Parse(default_mail);
        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = bool.Parse(hoatdong);
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaycapnhat = DateTime.Now;
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {

        String id = context.Request.Form["id"];
        String ten_template = context.Request.Form["ten_template"];
        String subject_mail = context.Request.Form["subject_mail"];
        String content_mail = context.Request.Form["content_mail"];
        String use_for = context.Request.Form["use_for"];
        String default_mail = context.Request.Form["default_mail"];
        String hoatdong = context.Request.Form["hoatdong"];


        // Đổi sang toán logic bool
        if (default_mail.ToLower().Equals("on") || default_mail.ToLower().Equals("true"))
        {
            default_mail = "true";
        }
        else
        {
            default_mail = "false";
        }

        // Đổi sang toán logic bool
        if (hoatdong.ToLower().Equals("on") || hoatdong.ToLower().Equals("true"))
        {
            hoatdong = "true";
        }
        else
        {
            hoatdong = "false";
        }

        md_mailtemplate m = new md_mailtemplate();
        m.md_mailtemplate_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss"));
        m.ten_template = ten_template;
        m.subject_mail = subject_mail;
        m.content_mail = content_mail;
        m.use_for = use_for;
        m.default_mail = bool.Parse(default_mail);

        m.mota = context.Request.Form["mota"];
        m.hoatdong = bool.Parse(hoatdong);
        m.nguoitao = UserUtils.getUser(context);
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaytao = DateTime.Now;
        m.ngaycapnhat = DateTime.Now;
        

        db.md_mailtemplates.InsertOnSubmit(m);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_mailtemplate where md_mailtemplate_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String sqlCount = "SELECT COUNT(*) AS count FROM md_mailtemplate";
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
            sidx = "m.ten_template";
        }

        string strsql = "select * from( select m.md_mailtemplate_id, m.ten_template " +
            " , m.subject_mail, m.content_mail, m.use_for, m.default_mail " +
            " , ngaytao, nguoitao, ngaycapnhat, nguoicapnhat, mota, hoatdong " + 
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " from md_mailtemplate m " +
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
