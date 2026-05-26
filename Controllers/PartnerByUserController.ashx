<%@ WebHandler Language="C#" Class="PartnerByUserController" %>

using System;
using System.Web;
using System.Linq;

public class PartnerByUserController : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        this.load(context);
    }

    LinqDBDataContext db = new LinqDBDataContext();
    
    public void load(HttpContext context)
    {
        String productId = context.Request.QueryString["productId"];

        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        
        String manv = UserUtils.getUser(context);
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
        bool isadmin = nv.isadmin.Value;
        
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
            " JOIN md_quocgia qg ON dtkd.md_quocgia_id = qg.md_quocgia_id " +
            " JOIN md_khuvuc kv ON dtkd.md_khuvuc_id = kv.md_khuvuc_id " +
            " JOIN md_loaidtkd ldt ON dtkd.md_loaidtkd_id = ldt.md_loaidtkd_id" +
            " WHERE 1=1  AND isncc = 1 {0} " + filter;
       
        if (!isadmin)
        {
            sqlCount = String.Format(sqlCount, "AND dtkd.md_doitackinhdoanh_id IN( select qlnl.md_doitackinhdoanh_id from md_quanlynangluc qlnl where qlnl.manv = '" + manv + "' )");
        }
        else
        {
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
            " FROM md_doitackinhdoanh dtkd JOIN md_quocgia qg ON dtkd.md_quocgia_id = qg.md_quocgia_id " +
            " JOIN md_khuvuc kv ON dtkd.md_khuvuc_id = kv.md_khuvuc_id " +
            " JOIN md_loaidtkd ldt ON dtkd.md_loaidtkd_id = ldt.md_loaidtkd_id " +
            " WHERE 1=1 AND isncc = 1 {0} " + filter +
            " )P WHERE RowNum > @start AND RowNum < @end";

        if (!isadmin)
        {
            strsql = String.Format(strsql, "AND dtkd.md_doitackinhdoanh_id IN( select qlnl.md_doitackinhdoanh_id from md_quanlynangluc qlnl where qlnl.manv = '" + manv + "' )");
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}