<%@ WebHandler Language="C#" Class="ProductPriceController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ProductPriceController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();


    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "activeversion":
                this.activeVersion(context);
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

    public void activeVersion(HttpContext context)
    {
        String phienbangia_id = context.Request.QueryString["vId"];
        md_phienbangia pbg = db.md_phienbangias.Single(p => p.md_phienbangia_id.Equals(phienbangia_id));
        int count_gsp = db.md_giasanphams.Where(p => p.md_phienbangia_id.Equals(phienbangia_id)).Count();
        if (pbg.md_trangthai_id.Equals("HIEULUC"))
        {
            context.Response.Write("Hiện trạng thái của phiên bản giá đã là hiệu lực!");
        }
        else if (count_gsp <= 0)
        {
            context.Response.Write("Phiên bản giá chưa có dữ liệu!");
        }
        else
        {
            pbg.md_trangthai_id = "HIEULUC";
            db.SubmitChanges();
            context.Response.Write(String.Format("Đã hiệu lực phiên bản giá {0} thành công!", pbg.ten_phienbangia));
        }
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select md_giasanpham_id, gia from md_giasanpham where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        string msg = "";
        try
        {
            String h = context.Request.Form["hoatdong"].ToLower();
            bool hd = false;
            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }
            String id = context.Request.Form["id"];
            md_giasanpham gsp = db.md_giasanphams.FirstOrDefault(p => p.md_giasanpham_id.Equals(id));
            md_phienbangia pbg = db.md_phienbangias.FirstOrDefault(p => p.md_phienbangia_id.Equals(gsp.md_phienbangia_id));

            if (pbg.md_trangthai_id.Equals("HIEULUC"))
            {
                msg = "Không thể chỉnh sửa khi phiên bản giá đã hiệu lực!";
            }
            else if (pbg.md_trangthai_id.Equals("SOANTHAO"))
            {
                //gsp.md_phienbangia_id = context.Request.Form["md_phienbangia_id"];
                gsp.md_sanpham_id = context.Request.Form["sanpham_id"];
                gsp.gia = decimal.Parse(context.Request.Form["gia"]);
                gsp.phi = decimal.Parse(context.Request.Form["phi"]);
                gsp.mota = context.Request.Form["mota"];
                gsp.barcode = context.Request.Form["barcode"];

                gsp.hoatdong = hd;
                gsp.ngaycapnhat = DateTime.Now;
                gsp.nguoicapnhat = UserUtils.getUser(context);

                var ma_donggoi = context.Request.Form["ma_donggoi"].Trim();
                if (string.IsNullOrWhiteSpace(ma_donggoi))
                    gsp.ma_donggoi = "";
                else
                {
                    var dg = db.md_donggois.Where(s => s.ma_donggoi == ma_donggoi).FirstOrDefault();
                    if (dg == null)
                        msg = string.Format("Không tìm thấy đóng gói có mã {0}", ma_donggoi);
                    else
                        gsp.ma_donggoi = dg.ma_donggoi;
                }

                if (msg.Length <= 0)
                    db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
            jqGridHelper.Utils.writeResult(1, "Cập nhập thành công!");
        else
            jqGridHelper.Utils.writeResult(0, msg);

    }

    public void add(HttpContext context)
    {
        string msg = "";
        try
        {
            String h = context.Request.Form["hoatdong"].ToLower();
            bool hd = false;
            if (h.Equals("on") || h.Equals("true"))
            {
                hd = true;
            }

            String md_sanpham_id = context.Request.Form["sanpham_id"];
            String md_phienbangia_id = context.Request.Form["md_phienbangia_id"];

            String select = "select md_trangthai_id from md_phienbangia where md_phienbangia_id = @md_phienbangia_id";
            String isActive = (String)mdbc.ExecuteScalar(select, "@md_phienbangia_id", md_phienbangia_id);
            if (isActive.Equals("HIEULUC"))
            {
                msg = "Không thể thêm mới khi phiên bản giá đã hiệu lực";
            }
            else
            {
                String selCount = "select count(*) from md_giasanpham where md_sanpham_id = @md_sanpham_id AND md_phienbangia_id = @md_phienbangia_id";
                int count = (int)mdbc.ExecuteScalar(selCount, "@md_sanpham_id", md_sanpham_id, "@md_phienbangia_id", md_phienbangia_id);
                if (count != 0)
                {
                    msg = "Đã tồn tại mã hàng trong phiên bản!";
                }
                else
                {
                    md_giasanpham mnu = new md_giasanpham
                    {
                        md_giasanpham_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                        md_phienbangia_id = md_phienbangia_id,
                        md_sanpham_id = md_sanpham_id,
                        gia = decimal.Parse(context.Request.Form["gia"]),
                        phi = decimal.Parse(context.Request.Form["phi"]),
                        mota = context.Request.Form["mota"],
                        barcode = context.Request.Form["barcode"],
                        hoatdong = hd,
                        ngaytao = DateTime.Now,
                        ngaycapnhat = DateTime.Now,
                        nguoitao = UserUtils.getUser(context),
                        nguoicapnhat = UserUtils.getUser(context)
                    };

                    var ma_donggoi = context.Request.Form["ma_donggoi"].Trim();
                    if (string.IsNullOrWhiteSpace(ma_donggoi))
                        mnu.ma_donggoi = "";
                    else
                    {
                        var dg = db.md_donggois.Where(s => s.ma_donggoi == ma_donggoi).FirstOrDefault();
                        if (dg == null)
                            msg = string.Format("Không tìm thấy đóng gói có mã {0}", ma_donggoi);
                        else
                            mnu.ma_donggoi = dg.ma_donggoi;
                    }

                    if (msg.Length <= 0)
                    {
                        db.md_giasanphams.InsertOnSubmit(mnu);
                        db.SubmitChanges();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
            jqGridHelper.Utils.writeResult(1, "Đã thêm mới thành công!");
        else
            jqGridHelper.Utils.writeResult(0, msg);
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_giasanpham gsp = db.md_giasanphams.FirstOrDefault(p => p.md_giasanpham_id.Equals(id));
        md_phienbangia pbg = db.md_phienbangias.FirstOrDefault(p => p.md_phienbangia_id.Equals(gsp.md_phienbangia_id));
        if (pbg.md_trangthai_id.Equals("HIEULUC"))
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa giá, vì sản phẩm thuộc phiên bản giá đã hiện lực!");
        }
        else if (pbg.md_trangthai_id.Equals("SOANTHAO"))
        {
            db.md_giasanphams.DeleteOnSubmit(gsp);
            db.SubmitChanges();
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

        string sqlCount = @"
            SELECT 
                COUNT(1) AS count 
            FROM 
                md_giasanpham gsp, md_sanpham sp
            WHERE 
                gsp.md_sanpham_id = sp.md_sanpham_id
                AND gsp.md_phienbangia_id = N'{0}'
        ";

        if (cateId != null)
        {
            sqlCount = string.Format(sqlCount, cateId, filter);
        }
        else
        {
            sqlCount = string.Format(sqlCount, 0, filter);
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
            sidx = "gia";
        }

        string strsql = @"
            select * from(
                select 
                    gsp.md_giasanpham_id
                    , gsp.md_phienbangia_id
                    , sp.md_sanpham_id
                    , sp.ma_sanpham as tensp
                    , gsp.gia
                    , isnull(gsp.phi, 0) as phi
                    , (gsp.gia + isnull(gsp.phi, 0)) as tongGia
                    , gsp.ma_donggoi
                    , gsp.mota
                    , gsp.barcode
                    , gsp.ngaytao
                    , gsp.nguoitao
                    , gsp.ngaycapnhat
                    , gsp.nguoicapnhat
                    , gsp.hoatdong
                    , ROW_NUMBER() OVER (ORDER BY {1} {2}) as RowNum
                FROM md_giasanpham gsp, md_sanpham sp
                WHERE 
                    gsp.md_sanpham_id = sp.md_sanpham_id
                    AND gsp.md_phienbangia_id = N'{0}' {3}
            )P
            WHERE RowNum > @start AND RowNum < @end";

        if (cateId != null)
        {
            strsql = string.Format(strsql, cateId, sidx, sord, filter);
        }
        else
        {
            strsql = string.Format(strsql, 0, sidx, sord, filter);
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
            xml += "<cell><![CDATA[" + row["md_giasanpham_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_phienbangia_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_sanpham_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tensp"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["gia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tongGia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_donggoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["barcode"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
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
