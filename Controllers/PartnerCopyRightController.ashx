<%@ WebHandler Language="C#" Class="PartnerCopyRightController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class PartnerCopyRightController : IHttpHandler
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
        String sql = String.Format("select md_doitackinhdoanh_id, ma_dtkd from md_doitackinhdoanh where isncc = {0} AND (isdocquyen = 0 or isdocquyen is null) AND hoatdong = 1 order by ma_dtkd asc", 0);
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void add(HttpContext context)
    {
        String md_doitackinhdoanh_id = context.Request.Form["ma_dtkd"];
        md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id));
        if (dtkd == null)
        {
            jqGridHelper.Utils.writeResult(0, "Không tìm thấy mã đối tác! " + md_doitackinhdoanh_id);
        }
        else
        {
            dtkd.isdocquyen = true;
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String md_doitackinhdoanh_id = context.Request.Form["id"];
        md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id));
        dtkd.isdocquyen = false;
        db.SubmitChanges();
    }

    public void load(HttpContext context)
    {
        String isncc = context.Request.QueryString["isncc"];
        String productId = context.Request.QueryString["productId"];
        
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
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM md_doitackinhdoanh dtkd " +
            " left JOIN md_quocgia qg ON dtkd.md_quocgia_id = qg.md_quocgia_id " +
            " left JOIN md_khuvuc kv ON dtkd.md_khuvuc_id = kv.md_khuvuc_id " +
            " left JOIN md_loaidtkd ldt ON dtkd.md_loaidtkd_id = ldt.md_loaidtkd_id " +
            " WHERE dtkd.isdocquyen = 1 {0} " + filter;

        if (isncc != null)
        {
            sqlCount = String.Format(sqlCount, " AND isncc = " + isncc);
        }
        else {
            sqlCount = String.Format(sqlCount, "");
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
            sidx = "dtkd.ma_dtkd";
        }

        string strsql = "SELECT * FROM( " +
            " SELECT dtkd.*, qg.ten_quocgia as tenqg, kv.ten_khuvuc as tenkv, ldt.ten_loaidtkd as tenloai, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            @" FROM md_doitackinhdoanh dtkd 
			left JOIN md_quocgia qg ON dtkd.md_quocgia_id = qg.md_quocgia_id 
            left JOIN md_khuvuc kv ON dtkd.md_khuvuc_id = kv.md_khuvuc_id 
            left JOIN md_loaidtkd ldt ON dtkd.md_loaidtkd_id = ldt.md_loaidtkd_id 
            WHERE  dtkd.isdocquyen = 1 {0} " + filter + " )P WHERE RowNum > @start AND RowNum < @end";
        
        if (isncc != null)
        {
            strsql = String.Format(strsql, " AND isncc = " + isncc);
        }
        else
        {
            strsql = String.Format(strsql, "");
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
            xml += "<cell><![CDATA[" + row["md_doitackinhdoanh_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenloai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["daidien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chucvu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tel"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["fax"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["email"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["url"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["diachi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenqg"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tenkv"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_banggia_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_taikhoan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nganhang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["masothue"] + "]]></cell>";
            xml += "<cell><![CDATA[" + CultureInfoUtil.currencyUSD(row["tong_congno"]) + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isncc"] + "]]></cell>";
            
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
