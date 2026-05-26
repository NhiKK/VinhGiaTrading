<%@ WebHandler Language="C#" Class="DongDanhSachDatHangController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;
using Newtonsoft.Json;

public class DongDanhSachDatHangController : IHttpHandler
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
            case "ESC_doincc":
                this.ESC_doincc(context);
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

    public void ESC_doincc(HttpContext context)
    {
        var id = context.Request.Form["id"];
        //var huyGiuCho = context.Request.Form["huyGiuCho"] == "1";
        //var doiNCC = context.Request.Form["doiNCC"] == "1";
        var nccDaChon = context.Request.Form["nccDaChon"];
        var res = new Public.ESC_kiemtraton_result();
        var dsdh = db.c_danhsachdathangs.Where(s => s.c_danhsachdathang_id == id).FirstOrDefault();
        if (dsdh == null)
        {
            res.ok = false;
            res.msg = "DSĐH không tồn tại, hãy làm mới lưới dữ liệu.";
            goto EndEventHandler;
        }
        if (dsdh.md_trangthai_id != Helper.SOANTHAO)
        {
            res.ok = false;
            res.msg = "DSĐH phải ở trạng thái SOẠN THẢO.";
            goto EndEventHandler;
        }
        else
        {
            if (new string[] { "DAGUI", "DANHAN", "DAHIEULUC" }.Contains(dsdh.trangthai) & !string.IsNullOrWhiteSpace(dsdh.trangthai))
            {
                res.ok = false;
                res.msg = "DSĐH phải ở trạng thái CHƯA GỬI hoặc CHỜ GỬI.";
                goto EndEventHandler;
            }
        }

        var dhs = db.c_dongdsdhs.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).ToList();
        if (string.IsNullOrWhiteSpace(nccDaChon))
        {
            foreach (var dh in dhs)
            {
                var dtkdId = db.md_nhacungungmacdinhs
                        .Where(s => s.md_sanpham_id == dh.md_sanpham_id & s.macdinh == true)
                        .OrderByDescending(s => s.ngaytao)
                        .Select(s => s.md_doitackinhdoanh_id)
                        .FirstOrDefault();
                dh.md_doitackinhdoanh_id = dtkdId;
            }
            db.SubmitChanges();
        }
        else
        {
            var dtkdId = db.md_doitackinhdoanhs.Where(s => s.ma_dtkd == nccDaChon).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(dtkdId))
            {
                res.ok = false;
                res.msg = string.Format(@"Không tìm thấy NCC {0} trong dữ liệu gốc.", nccDaChon);
                goto EndEventHandler;
            }
            else
            {
                foreach (var dh in dhs)
                {
                    dh.md_doitackinhdoanh_id = dtkdId;
                }
                dsdh.md_doitackinhdoanh_id = dtkdId;
                dsdh.diachigiaohang = dtkdId;
                db.SubmitChanges();
            }
        }

        //res = Public.ESC_kiemtraton(db, id, huyGiuCho, doiNCC, nccDaChon, bg, dhs);
        res.msg = "<div style='color:blue'>Đổi Nhà cung cấp thành công.</div>" + res.msg;
        EndEventHandler:;
        context.Response.Write(JsonConvert.SerializeObject(res));
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_dongdsdh_id, c_dongdsdh_id from c_dongdsdh where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(context)));
        c_dongdsdh m = db.c_dongdsdhs.Single(p => p.c_dongdsdh_id == context.Request.Form["id"]);

        c_danhsachdathang ds = db.c_danhsachdathangs.Single(p => p.c_danhsachdathang_id.Equals(m.c_danhsachdathang_id));
        if (ds.md_trangthai_id.Equals("HIEULUC"))
        {
            jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi đặt hàng đã hiệu lực.!");
        }
        else
        {

            m.sothutu = int.Parse(context.Request.Form["sothutu"]);
            m.huongdan_dathang = context.Request.Form["huongdan_dathang"];
            if (nv.isadmin.Value)
            {
                m.gianhap = decimal.Parse(context.Request.Form["gianhap"]);
            }
            m.tem_dan = context.Request.Form["tem_dan"];
            m.sothutu = int.Parse(context.Request.Form["sothutu"]);
            m.han_giaohang = DateTime.ParseExact(context.Request.Form["han_giaohang"], "dd/MM/yyyy", null);

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

        String c_danhsachdathang_id = context.Request.Form["c_danhsachdathang_id"];
        c_danhsachdathang ds = db.c_danhsachdathangs.Single(p => p.c_danhsachdathang_id.Equals(c_danhsachdathang_id));

        if (!ds.md_trangthai_id.Equals("SOANTHAO"))
        {
            c_dongdsdh m = new c_dongdsdh();

            m.c_danhsachdathang_id = c_danhsachdathang_id;
            m.c_dongdonhang_id = context.Request.Form["c_dongdonhang_id"];
            m.md_sanpham_id = context.Request.Form["sanpham_id"];
            m.mota_tiengviet = context.Request.Form["mota_tiengviet"];
            m.mota_tienganh = context.Request.Form["mota_tienganh"];

            m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
            m.huongdan_dathang = context.Request.Form["huongdan_dathang"];
            m.han_giaohang = DateTime.ParseExact(context.Request.Form["han_giaohang"], "dd/MM/yyyy", null);
            m.sl_dathang = decimal.Parse(context.Request.Form["sl_dathang"]);
            m.sl_dagiao = decimal.Parse(context.Request.Form["sl_dagiao"]);
            m.sl_conlai = decimal.Parse(context.Request.Form["sl_conlai"]);
            m.gianhap = decimal.Parse(context.Request.Form["gianhap"]);
            m.tem_dan = context.Request.Form["tem_dan"];
            m.sothutu = int.Parse(context.Request.Form["sothutu"]);

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaytao = DateTime.Now;
            m.ngaycapnhat = DateTime.Now;

            db.c_dongdsdhs.InsertOnSubmit(m);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi đặt hàng đã hiệu lực.!");
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        c_dongdsdh line = db.c_dongdsdhs.Single(p => p.c_dongdsdh_id.Equals(id));
        c_danhsachdathang master = db.c_danhsachdathangs.Single(p => p.c_danhsachdathang_id.Equals(line.c_danhsachdathang_id));
        if (!master.md_trangthai_id.Equals("SOANTHAO"))
        {
            db.c_dongdsdhs.DeleteOnSubmit(line);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa dòng khi đặt hàng đã hiệu lực.!");
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

        String poId = context.Request.QueryString["poId"];

        String sqlCount = string.Format(@"
            SELECT 
                COUNT(1) AS count
            FROM 
                c_dongdsdh ddsdh
                , c_danhsachdathang dsdh
                , md_sanpham sp
                , md_doitackinhdoanh dtkd
            WHERE 
                ddsdh.c_danhsachdathang_id = dsdh.c_danhsachdathang_id
                AND ddsdh.md_sanpham_id = sp.md_sanpham_id
                AND ddsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
                AND ddsdh.c_danhsachdathang_id = N'{{0}}'
                {0}
        ", filter);

        if (poId != null)
        {
            sqlCount = string.Format(sqlCount, poId);
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
            sidx = "c_dongdsdh_id";
        }

        string strsql = string.Format(@"
            select * from(
               select
                   ddsdh.c_dongdsdh_id
                   , dsdh.c_danhsachdathang_id
                   , sp.trangthai
                   , ddsdh.sothutu as ddsdhline 
                   , sp.md_sanpham_id
                   , sp.ma_sanpham
                   , ddsdh.mota_tiengviet 
                   , dtkd.ma_dtkd
                   , huongdan_dathang 
                   , han_giaohang
                   , tem_dan
                   , ddsdh.sl_dathang
                   , ddsdh.giachuan
                   , ddsdh.gianhap - isnull(ddsdh.giachuan, ddsdh.gianhap) as phi
                   , ddsdh.gianhap
                   , ddsdh.sl_dagiao
                   , ddsdh.sl_conlai 
                   , ddsdh.ngaytao, ddsdh.nguoitao, ddsdh.ngaycapnhat, ddsdh.nguoicapnhat, ddsdh.mota, ddsdh.hoatdong 
                   , ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
               FROM
                    c_dongdsdh ddsdh
                    , c_danhsachdathang dsdh
                    , md_sanpham sp 
                    , md_doitackinhdoanh dtkd 
               WHERE 
                    ddsdh.c_danhsachdathang_id = dsdh.c_danhsachdathang_id 
                    AND ddsdh.md_sanpham_id = sp.md_sanpham_id 
                    AND ddsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
                    AND ddsdh.c_danhsachdathang_id = N'{2}' {3}
            )P  WHERE RowNum > @start AND RowNum < @end"
            , sidx
            , sord
            , poId != null ? poId : "0"
            , filter
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
            xml += "<cell><![CDATA[" + row["c_dongdsdh_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["c_danhsachdathang_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ddsdhline"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_sanpham_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota_tiengviet"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["huongdan_dathang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["han_giaohang"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tem_dan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_dathang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["giachuan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["gianhap"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_dagiao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_conlai"] + "]]></cell>";
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
