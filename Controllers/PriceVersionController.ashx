<%@ WebHandler Language="C#" Class="PriceVersionController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class PriceVersionController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getPriceList":
                this.getPriceList(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "copy":
                this.copyPriceListVersion(context);
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

    public void getPriceList(HttpContext context)
    {
        String id = context.Request.QueryString["filter"];
        String sql = "select md_phienbangia_id, ten_phienbangia from md_phienbangia where hoatdong = 1 AND md_phienbangia_id != @md_phienbangia_id";
        System.Data.SqlClient.SqlDataReader rd = mdbc.ExecuteReader(sql, "@md_phienbangia_id", id);
        String wr = "";
        if (rd.HasRows)
        {
            while (rd.Read())
            {
                wr += String.Format("<option value=\"{0}\">{1}</option>", rd[0], rd[1]);
            }
        }
        rd.Close();
        context.Response.Write(wr);
    }

    public void copyPriceListVersion(HttpContext context)
    {
        try
        {
            string p_id = context.Request.Form["price_version"].ToString();
            string id = context.Request.Form["id"].ToString();
            mdbc.ExcuteNonProcedure("copyPhienBanGia", "@phienban_cp", p_id, "@phienban", id);
            context.Response.Write("Copy phiên bản giá thành công!");
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select md_phienbangia_id, ten_phienbangia from md_phienbangia where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        string msg = "";
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        string ten_phienbangia = context.Request.Form["ten_phienbangia"];
        string md_banggia_id = context.Request.Form["md_banggia_id"];
        string ngay_hieuluc = context.Request.Form["ngay_hieuluc"];
        string ngay_hethan = context.Request.Form["ngay_hethan"];

        md_phienbangia m = db.md_phienbangias.FirstOrDefault(p => p.md_phienbangia_id == context.Request.Form["id"]);
        if (m.md_trangthai_id.Equals("HIEULUC"))
        {
            if (m.ten_phienbangia != ten_phienbangia)
                msg = "Đã hiệu lực, Không thể sửa tên PBG";
            else if (m.ngay_hieuluc.Value.ToString("dd/MM/yyyy") != ngay_hieuluc)
                msg = "Đã hiệu lực, Không thể sửa ngày hiệu lực";
            else if (m.ngay_hethan == null)
            {
                if(!string.IsNullOrWhiteSpace(ngay_hethan))
                    msg = "Đã hiệu lực, Không thể sửa ngày hết hạn";
            }
            else if (m.ngay_hethan.Value.ToString("dd/MM/yyyy") != ngay_hethan)
                msg = "Đã hiệu lực, Không thể sửa ngày hết hạn";

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;
            m.nguoicapnhat = UserUtils.getUser(context);
            db.SubmitChanges();
        }
        else if (m.md_trangthai_id.Equals("SOANTHAO"))
        {
            m.ten_phienbangia = ten_phienbangia;
            m.md_banggia_id = md_banggia_id;
            m.ngay_hieuluc = DateTime.ParseExact(ngay_hieuluc, "dd/MM/yyyy", null);
            m.mota = context.Request.Form["mota"];

            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;
            m.nguoicapnhat = UserUtils.getUser(context);

            if (string.IsNullOrWhiteSpace(ngay_hethan))
                m.ngay_hethan = null;
            else
                m.ngay_hethan = DateTime.ParseExact(ngay_hethan, "dd/MM/yyyy", null);

            db.SubmitChanges();
        }

        if (msg.Length > 0)
            jqGridHelper.Utils.writeResult(0, msg);
    }

    public void add(HttpContext context)
    {
        string h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        string ngay_hethan = context.Request.Form["ngay_hethan"];

        md_phienbangia mnu = new md_phienbangia
        {
            md_phienbangia_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            ten_phienbangia = context.Request.Form["ten_phienbangia"],
            md_banggia_id = context.Request.Form["md_banggia_id"],
            ngay_hieuluc = DateTime.ParseExact(context.Request.Form["ngay_hieuluc"], "dd/MM/yyyy", null),
            md_trangthai_id = "SOANTHAO",
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        if (string.IsNullOrWhiteSpace(ngay_hethan))
            mnu.ngay_hethan = null;
        else
            mnu.ngay_hethan = DateTime.ParseExact(ngay_hethan, "dd/MM/yyyy", null);

        db.md_phienbangias.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];

        md_phienbangia pbg = db.md_phienbangias.FirstOrDefault(p => p.md_phienbangia_id.Equals(id));
        if (pbg.md_trangthai_id.Equals("HIEULUC"))
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa khi phiên bản giá đã hiệu lực");
        }
        else if (pbg.md_trangthai_id.Equals("SOANTHAO"))
        {
            String delDetails = "delete md_giasanpham where md_phienbangia_id = @md_phienbangia_id";
            String delMaster = String.Format("delete md_phienbangia where md_phienbangia_id = @md_phienbangia_id");

            mdbc.ExcuteNonQuery(delDetails, "@md_phienbangia_id", id);
            mdbc.ExcuteNonQuery(delMaster, "@md_phienbangia_id", id);
        }
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


        String cateId = context.Request.QueryString["cateId"];
        String sqlCount = "SELECT COUNT(*) AS count FROM md_phienbangia pbg JOIN md_banggia bg ON pbg.md_banggia_id = bg.md_banggia_id WHERE pbg.md_banggia_id = N'{0}' " + filter;

        if (cateId != null)
        {
            sqlCount = string.Format(sqlCount, cateId);
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
            sidx = "ten_phienbangia";
        }

        string strsql = "select * from( " +
            " select pbg.md_phienbangia_id, pbg.md_trangthai_id, pbg.ten_phienbangia, bg.md_banggia_id, pbg.ngay_hieuluc," +
            " pbg.ngaytao, pbg.nguoitao, pbg.ngaycapnhat, pbg.nguoicapnhat, pbg.mota, pbg.hoatdong, pbg.ngay_hethan " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_phienbangia pbg JOIN md_banggia bg ON pbg.md_banggia_id = bg.md_banggia_id " +
            " WHERE pbg.md_banggia_id = N'{0}' " + filter +
            " )P where RowNum > @start AND RowNum < @end";

        if (cateId != null)
        {
            strsql = string.Format(strsql, cateId);
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
            xml += "<cell><![CDATA[" + (!row[4].ToString().Equals("") ? DateTime.Parse(row[4].ToString()).ToString("dd/MM/yyyy") : "") + "]]></cell>";
            xml += "<cell><![CDATA[" + (!row[11].ToString().Equals("") ? DateTime.Parse(row[11].ToString()).ToString("dd/MM/yyyy") : "") + "]]></cell>";
            xml += "<cell><![CDATA[" + (!row[5].ToString().Equals("") ? DateTime.Parse(row[5].ToString()).ToString("dd/MM/yyyy") : "") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[7].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
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
