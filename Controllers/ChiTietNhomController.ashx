<%@ WebHandler Language="C#" Class="ChiTietNhomController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ChiTietNhomController : IHttpHandler
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
        String sql = "select mamenu, tenmenu from menu where hoatdong = 1 order by tenmenu asc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String nguoiquanly = context.Request.Form["nguoiquanly"].ToLower();

        if (nguoiquanly.Equals("on") || nguoiquanly.Equals("true"))
        {
            nguoiquanly = "True";
        }
        else {
            nguoiquanly = "False";
        }
        
        String manv = context.Request.Form["manv"];
        String md_nhom_id = context.Request.Form["md_nhom_id"];
        String md_chitietnhom_id = context.Request.Form["md_chitietnhom_id"];

        md_chitietnhom ctn = db.md_chitietnhoms.FirstOrDefault(
                p => p.manv.Equals(manv) 
                && p.md_nhom_id.Equals(md_nhom_id) 
                && !p.md_chitietnhom_id.Equals(md_chitietnhom_id)
            );
        if (ctn != null)
        {
            jqGridHelper.Utils.writeResult(0, "Đã tồn tại nhân viên thuộc nhóm này!");
        }
        else {
            md_chitietnhom m = db.md_chitietnhoms.Single(p => p.md_chitietnhom_id == context.Request.Form["id"]);
            m.manv = context.Request.Form["manv"];
            m.nguoiquanly = bool.Parse(nguoiquanly);

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaycapnhat = DateTime.Now;

            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String nguoiquanly = context.Request.Form["nguoiquanly"].ToLower();
        
        if (nguoiquanly.Equals("on") || nguoiquanly.Equals("true"))
        {
            nguoiquanly = "True";
        }
        else
        {
            nguoiquanly = "False";
        }

        String manv = context.Request.Form["manv"];
        String md_nhom_id = context.Request.Form["md_nhom_id"];

        md_chitietnhom ctn = db.md_chitietnhoms.FirstOrDefault(p => p.manv.Equals(manv) && p.md_nhom_id.Equals(md_nhom_id));
        
        if (ctn != null)
        {
            jqGridHelper.Utils.writeResult(0, "Đã tồn tại nhân viên thuộc nhóm này!");
        }
        else {
            md_chitietnhom mnu = new md_chitietnhom
            {
                md_chitietnhom_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                md_nhom_id = md_nhom_id,
                manv = context.Request.Form["manv"],
                nguoiquanly = bool.Parse(nguoiquanly),

                mota = context.Request.Form["mota"],
                hoatdong = hd,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            db.md_chitietnhoms.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_chitietnhom where md_chitietnhom_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String grpId = context.Request.QueryString["grpId"];
        
        
        
        String sqlCount = String.Format("SELECT COUNT(*) AS count " +
                " FROM md_chitietnhom ctn " +
                " WHERE ctn.md_nhom_id = N'{0}' ", grpId);
        
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
            sidx = "ctn.manv";
        }

        string strsql = String.Format("select * from( " +
            " select ctn.*,  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " from md_chitietnhom ctn " +
            " where ctn.md_nhom_id = N'{0}' " +
            " )P where RowNum > @start AND RowNum < @end", grpId);

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
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[6].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
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
