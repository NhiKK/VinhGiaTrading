<%@ WebHandler Language="C#" Class="NguoiLienHeController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class NguoiLienHeController : IHttpHandler
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
            case "getoptionNullFirst":
                this.getSelectOptionNullFirst(context);
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
        String sql = "select md_nguoilienhe_id, ten from md_nguoilienhe where hoatdong = 1 order by (ten) ";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void getSelectOptionNullFirst(HttpContext context)
    {
        String sql = "select distinct doitaclienquan, (select ma_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = doitaclienquan) as ten from md_nguoilienhe where hoatdong = 1  ";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToOptionNullFirst());
    }
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_nguoilienhe m = db.md_nguoilienhes.Single(p => p.md_nguoilienhe_id== context.Request.Form["id"]);
        //m.ten = context.Request.Form["ten"];
        //m.diachi = context.Request.Form["diachi"];
        //m.cmnd = context.Request.Form["cmnd"];
        //m.email = context.Request.Form["email"];
        //m.fax = context.Request.Form["fax"];
        //m.sdt = context.Request.Form["sdt"];
        //m.masothue = context.Request.Form["masothue"];
        m.muchoahong = decimal.Parse(context.Request.Form["muchoahong"]);
        m.doitaclienquan = context.Request.Form["doitaclienquan"];
        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaycapnhat = DateTime.Now;
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        try
        {
            String h = context.Request.Form["hoatdong"].ToLower();
            bool hd = false;
            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }

            md_nguoilienhe mnu = new md_nguoilienhe
            {
                md_nguoilienhe_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                //ten = context.Request.Form["ten"],
                //diachi = context.Request.Form["diachi"],
                //cmnd = context.Request.Form["cmnd"],
                //email = context.Request.Form["email"],
                //fax = context.Request.Form["fax"],
                //sdt = context.Request.Form["sdt"],
                //masothue = context.Request.Form["masothue"],
                md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"],
                muchoahong = decimal.Parse(context.Request.Form["muchoahong"]),
                doitaclienquan = context.Request.Form["doitaclienquan"],

                mota = context.Request.Form["mota"],
                hoatdong = hd,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };
            int check = (int)mdbc.ExecuteScalar("select count(*) from md_nguoilienhe where md_doitackinhdoanh_id ='" + context.Request.Form["md_doitackinhdoanh_id"] + "'");
            if (check == 0)
            {
                db.md_nguoilienhes.InsertOnSubmit(mnu);
                db.SubmitChanges();
            }
            else {
                jqGridHelper.Utils.writeResult(1, "Một đối tác chỉ có thể tồn tại 1 người liên hệ!");
            }
            
        }catch(Exception e){
            jqGridHelper.Utils.writeResult(0, e.Message);
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_nguoilienhe where md_nguoilienhe_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String md_doitackinhdoanh_id = context.Request.QueryString["md_doitackinhdoanh_id"];
        
        String sqlCount = "SELECT COUNT(*) AS count " + 
            @"FROM md_doitackinhdoanh dt
			left join md_nguoilienhe lh on dt.md_doitackinhdoanh_id = lh.md_doitackinhdoanh_id  
            AND lh.md_doitackinhdoanh_id = N'{0}'";

        //if (md_chungloai_id != null)
        //{
            sqlCount = String.Format(sqlCount, md_doitackinhdoanh_id);
        //}
        //else {
        //    sqlCount = String.Format(sqlCount, 0);
        //}
        
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
            sidx = "dt.md_detai_id";
        }

        string strsql = "select * from( " +
            " SELECT cl.md_nguoilienhe_id, dt.md_doitackinhdoanh_id, "+
            " (select ma_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = cl.doitaclienquan) as doitaclienquan, cl.muchoahong, cl.ten, cl.email, cl.diachi, cl.sdt " +
            " , cl.cmnd, dt.ma_dtkd, cl.masothue, cl.fax " +
            " , cl.ngaytao, cl.nguoitao, cl.ngaycapnhat, cl.nguoicapnhat, cl.mota, cl.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            @"FROM md_doitackinhdoanh dt
			left join md_nguoilienhe cl on dt.md_doitackinhdoanh_id = cl.md_doitackinhdoanh_id  
            WHERE cl.md_doitackinhdoanh_id = N'{0}')P where RowNum > @start AND RowNum < @end";
        
        //if (md_chungloai_id != null)
        //{
        //    strsql = String.Format(strsql, md_chungloai_id);
        //}
        //else
        //{
            strsql = String.Format(strsql, md_doitackinhdoanh_id);
        //}
        
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_nguoilienhe_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_doitackinhdoanh_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["doitaclienquan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["muchoahong"] + "]]></cell>";
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
