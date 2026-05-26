<%@ WebHandler Language="C#" Class="PackingController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class PackingController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "updatestatus":
                this.UpdateStatus(context);
                break;
            case "getoptionselected":
                this.getOptionSelected(context);
                break;
            case "getbyproduct":
                this.getSelectOptionByProduct(context);
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

    public void UpdateStatus(HttpContext context)
    {
        String msg = "";
        try
        {
            String id = context.Request.QueryString["md_donggoi_id"];
            String trangthai = context.Request.QueryString["trangthai"];
            md_donggoi sp = db.md_donggois.FirstOrDefault(p => p.md_donggoi_id.Equals(id));

            if (sp != null | trangthai == "TOACTIVE")
            {
                bool next = true;
                switch (trangthai)
                {
                    case "HIEULUC":
                        msg = "Đã chuyển trạng thái đóng gói thành \"Hiệu lực\"!";
                        break;
                    case "SOANTHAO":
                        msg = "Đã chuyển trạng thái đóng gói thành Soạn thảo!";
                        break;
                    case "TOACTIVE":
                        msg = "Đã chuyển tất cả đóng gói \"soạn thảo\" sang \"Hiệu lực\"!";
                        break;
                    default:
                        msg = "Không xác định được trạng thái của đóng gói!";
                        next = false;
                        break;
                }

                if (next)
                {
                    if (trangthai.Equals("TOACTIVE"))
                    {
                        String update = "update md_donggoi set trangthai = 'HIEULUC' where trangthai='SOANTHAO'";
                        mdbc.ExcuteNonQuery(update);
                    }
                    else
                    {
                        sp.trangthai = trangthai;
                        db.SubmitChanges();
                    }
                }
            }
            else
            {
                msg = "Chưa chọn đóng gói hoặc đóng gói không tồn tại!";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        context.Response.Write(msg);
    }

    public void getOptionSelected(HttpContext context)
    {
        String md_sanpham_id = context.Request.QueryString["md_sanpham_id"];
        String c_dongdonhang_id = context.Request.QueryString["c_dongdonhang_id"];
        String c_chitietbaogia_id = context.Request.QueryString["c_chitietbaogia_id"];
        String c_baogia_id = context.Request.QueryString["c_baogia_id"];
        String c_donhang_id = context.Request.QueryString["c_donhang_id"];
        String md_donggoi_id = ""; // selected

        if (c_dongdonhang_id != null)
        {
            if (!c_dongdonhang_id.Equals(""))
            {
                c_dongdonhang ddh = db.c_dongdonhangs.FirstOrDefault(p => p.c_dongdonhang_id.Equals(c_dongdonhang_id));
                md_donggoi_id = ddh.md_donggoi_id;
            }
            else
            {
                md_donggoi_id = "";
            }
        }

        if (c_chitietbaogia_id != null)
        {
            c_chitietbaogia ctbg = db.c_chitietbaogias.FirstOrDefault(p => p.c_chitietbaogia_id.Equals(c_chitietbaogia_id));
            try
            {
                md_donggoi_id = ctbg.md_donggoi_id;
            }
            catch
            {

            }
        }

        string docquyen = "", dtkd_id = "";
        if (c_baogia_id != null)
        {
            dtkd_id = db.c_baogias.FirstOrDefault(p => p.c_baogia_id.Equals(c_baogia_id)).md_doitackinhdoanh_id;
            docquyen = check_hanghoadocquyen(md_sanpham_id, dtkd_id);
        }
        else if (c_donhang_id != null)
        {
            dtkd_id = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(c_donhang_id)).md_doitackinhdoanh_id;
            docquyen = check_hanghoadocquyen(md_sanpham_id, dtkd_id);
        }

        String sql = String.Format("select dg.md_donggoi_id, dg.ten_donggoi, dg.doigia_donggoi " +
                " FROM md_donggoi dg, md_donggoisanpham dgsp " +
                " WHERE dg.hoatdong = 1 " +
                " AND dg.md_donggoi_id = dgsp.md_donggoi_id " +
                " AND dgsp.md_sanpham_id = '{0}' order by dgsp.macdinh desc ", md_sanpham_id);

        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToSelected(md_donggoi_id) + "(##)" + docquyen);
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select md_donggoi_id, ten_donggoi from md_donggoi where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void getSelectOptionByProduct(HttpContext context)
    {
        String md_sanpham_id = context.Request.QueryString["md_sanpham_id"];
        String sql = String.Format("select dg.md_donggoi_id, ten_donggoi " +
                " FROM md_donggoi dg, md_donggoisanpham dgsp " +
                " WHERE dg.hoatdong = 1 " +
                " AND dg.md_donggoi_id = dgsp.md_donggoi_id " +
                " AND dgsp.md_sanpham_id = '{0}'", md_sanpham_id);
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToOptionList());
    }

    public void edit(HttpContext context)
    {
        String ma_donggoi = context.Request.Form["ma_donggoi"];
        String md_donggoi_id = context.Request.Form["md_donggoi_id"];

        String selPacking = "select COUNT(*) as count from md_donggoi where ma_donggoi = @ma_donggoi AND md_donggoi_id != @md_donggoi_id";
        int colCount = (int)mdbc.ExecuteScalar(selPacking, "@ma_donggoi", ma_donggoi, "@md_donggoi_id", md_donggoi_id);
        if (colCount != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Mã đóng gói đã tồn tại");
        }
        else
        {
            String h, mi, dgdg;
            h = context.Request.Form["hoatdong"].ToLower();
            mi = context.Request.Form["mix_chophepsudung"].ToLower();
            dgdg = context.Request.Form["doigia_donggoi"].ToLower();
            bool hd, mix, dgdg_chk;
            hd = mix = dgdg_chk = false;

            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }

            if (mi.Equals("on") || mi.Equals("true"))
            { mix = true; }

            if (dgdg.Equals("on") || dgdg.Equals("true"))
            { dgdg_chk = true; }

            String sl_outer = context.Request.Form["sl_outer"];
            String getDvt = "Select ten_dvt from md_donvitinh where md_donvitinh_id = @md_donvitinh_id";
            String dvtouter = (String)mdbc.ExecuteScalar(getDvt, "@md_donvitinh_id", context.Request.Form["dvtouter"]);


            md_donggoi m = db.md_donggois.Single(p => p.md_donggoi_id == context.Request.Form["id"]);
            m.ten_donggoi = sl_outer + dvtouter;
            m.ma_donggoi = ma_donggoi;
            m.sl_inner = decimal.Parse(context.Request.Form["sl_inner"]);
            m.dvt_inner = context.Request.Form["dvtinner"];
            m.l1 = decimal.Parse(context.Request.Form["l1"]);
            m.w1 = decimal.Parse(context.Request.Form["w1"]);
            m.h1 = decimal.Parse(context.Request.Form["h1"]);
            m.sl_outer = decimal.Parse(context.Request.Form["sl_outer"]);
            m.dvt_outer = context.Request.Form["dvtouter"];
            m.l2_mix = decimal.Parse(context.Request.Form["l2_mix"]);
            m.w2_mix = decimal.Parse(context.Request.Form["w2_mix"]);
            m.h2_mix = decimal.Parse(context.Request.Form["h2_mix"]);
            m.v2 = decimal.Parse(context.Request.Form["v2"]);
            m.sl_cont_mix = decimal.Parse(context.Request.Form["sl_cont_mix"]);
            m.soluonggoi_ctn = decimal.Parse(context.Request.Form["soluonggoi_ctn"]);
            //m.trongluongdonggoi = decimal.Parse(context.Request.Form["trongluongdonggoi"]);
            //m.md_trongluong_id = context.Request.Form["md_trongluong_id"];
            m.vd = decimal.Parse(context.Request.Form["vd"]);
            m.vn = decimal.Parse(context.Request.Form["vn"]);
            m.vl = decimal.Parse(context.Request.Form["vl"]);
            m.nw1 = decimal.Parse(context.Request.Form["nw1"]);
            m.gw1 = decimal.Parse(context.Request.Form["gw1"]);
            m.nw2 = decimal.Parse(context.Request.Form["nw2"]);
            m.gw2 = decimal.Parse(context.Request.Form["gw2"]);
            m.vtdg2 = decimal.Parse(context.Request.Form["vtdg2"]);
            m.ghichu_vachngan = context.Request.Form["ghichu_vachngan"];
            m.mix_chophepsudung = mix;

            if (context.Request.Form["ngayxacnhan"] != "")
            {
                m.ngayxacnhan = DateTime.ParseExact(context.Request.Form["ngayxacnhan"], "dd/MM/yyyy", null);
            }
            else
            {
                m.ngayxacnhan = null;
            }
            m.doigia_donggoi = dgdg_chk;
            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaycapnhat = DateTime.Now;

            var cpdgvc = context.Request.Form["cpdg_vuotchuan"];
            if (!string.IsNullOrWhiteSpace(cpdgvc))
                m.cpdg_vuotchuan = decimal.Parse(cpdgvc);
            else
                m.cpdg_vuotchuan = null;

            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
        String h, mi, dgdg;
        h = context.Request.Form["hoatdong"].ToLower();
        mi = context.Request.Form["mix_chophepsudung"].ToLower();
        dgdg = context.Request.Form["doigia_donggoi"].ToLower();
        bool hd, mix, dgdg_chk;
        hd = mix = dgdg_chk = false;

        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        if (mi.Equals("on") || mi.Equals("true"))
        { mix = true; }

        if (dgdg.Equals("on") || dgdg.Equals("true"))
        { dgdg_chk = true; }

        String ma_donggoi = context.Request.Form["ma_donggoi"];
        String selPacking = "select COUNT(*) as count from md_donggoi where ma_donggoi = @ma_donggoi";
        int colCount = (int)mdbc.ExecuteScalar(selPacking, "@ma_donggoi", ma_donggoi);
        if (colCount != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Mã đóng gói đã tồn tại");
        }
        else
        {
            String sl_outer = context.Request.Form["sl_outer"];
            String getDvt = "Select ten_dvt from md_donvitinh where md_donvitinh_id = @md_donvitinh_id";
            String dvtouter = (String)mdbc.ExecuteScalar(getDvt, "@md_donvitinh_id", context.Request.Form["dvtouter"]);

            DateTime? nxn = new DateTime();
            try
            {
                nxn = DateTime.ParseExact(context.Request.Form["ngayxacnhan"], "dd/MM/yyyy", null);
            }
            catch
            {
                nxn = null;
            }


            md_donggoi mnu = new md_donggoi
            {
                md_donggoi_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                ma_donggoi = ma_donggoi,
                ten_donggoi = sl_outer + dvtouter,
                sl_inner = decimal.Parse(context.Request.Form["sl_inner"]),
                dvt_inner = context.Request.Form["dvtinner"],
                l1 = decimal.Parse(context.Request.Form["l1"]),
                w1 = decimal.Parse(context.Request.Form["w1"]),
                h1 = decimal.Parse(context.Request.Form["h1"]),
                sl_outer = decimal.Parse(context.Request.Form["sl_outer"]),
                dvt_outer = context.Request.Form["dvtouter"],
                l2_mix = decimal.Parse(context.Request.Form["l2_mix"]),
                w2_mix = decimal.Parse(context.Request.Form["w2_mix"]),
                h2_mix = decimal.Parse(context.Request.Form["h2_mix"]),
                v2 = decimal.Parse(context.Request.Form["v2"]),
                sl_cont_mix = decimal.Parse(context.Request.Form["sl_cont_mix"]),
                soluonggoi_ctn = decimal.Parse(context.Request.Form["soluonggoi_ctn"]),
                //trongluongdonggoi = decimal.Parse(context.Request.Form["trongluongdonggoi"]),
                //md_trongluong_id = context.Request.Form["md_trongluong_id"],
                vd = decimal.Parse(context.Request.Form["vd"]),
                vn = decimal.Parse(context.Request.Form["vn"]),
                vl = decimal.Parse(context.Request.Form["vl"]),
                nw1 = decimal.Parse(context.Request.Form["nw1"]),
                gw1 = decimal.Parse(context.Request.Form["gw1"]),
                nw2 = decimal.Parse(context.Request.Form["nw2"]),
                gw2 = decimal.Parse(context.Request.Form["gw2"]),
                vtdg2 = decimal.Parse(context.Request.Form["vtdg2"]),
                ghichu_vachngan = context.Request.Form["ghichu_vachngan"],
                trangthai = "SOANTHAO",
                mix_chophepsudung = mix,

                ngayxacnhan = nxn,
                doigia_donggoi = dgdg_chk,
                mota = context.Request.Form["mota"],
                hoatdong = hd,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            var cpdgvc = context.Request.Form["cpdg_vuotchuan"];
            if (!string.IsNullOrWhiteSpace(cpdgvc))
                mnu.cpdg_vuotchuan = decimal.Parse(cpdgvc);
            else
                mnu.cpdg_vuotchuan = null;

            db.md_donggois.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        var dg = db.md_donggois.Where(s => s.md_donggoi_id == id).FirstOrDefault();
        var dgsp = db.md_donggoisanphams.Where(s => s.md_donggoi_id == id).ToList();
        db.md_donggois.DeleteOnSubmit(dg);
        db.md_donggoisanphams.DeleteAllOnSubmit(dgsp);
        db.SubmitChanges();

        //id = id.Insert(0, "'");
        //id = id.Insert(id.Length, "'");
        //id = id.Replace(",", "','");
        //String sql = "update md_donggoi set hoatdong = 0 where md_donggoi_id IN (" + id + ")";
        //mdbc.ExcuteNonQuery(sql);
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

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM md_donggoi dg where dg.trangthai = 'SOANTHAO' and 1=1 " + filter;

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
            sidx = "ten_donggoi";
        }

        string strsql = string.Format(@"
        select * from(
            select 
                md_donggoi_id, 
                dg.trangthai, 
                ma_donggoi, 
                ten_donggoi, 
                sl_inner, 
                (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner) as dvtinner, 
                l1, 
                w1, 
                h1, 
                sl_outer, 
                (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer) as dvtouter, 
                l2_mix, 
                w2_mix, 
                h2_mix, 
                v2, 
                sl_cont_mix, 
                soluonggoi_ctn_20,
                soluonggoi_ctn,
                soluonggoi_ctn_40hc,
                nw1, 
                gw1,
                nw2,
                gw2,
                vtdg2,
                dg.cpdg_vuotchuan, 
                trongluongdonggoi, 
                vd, 
                vn, 
                vl, 
                ghichu_vachngan, 
                mix_chophepsudung, 
                dg.ngayxacnhan, 
                dg.mota, 
                dg.doigia_donggoi,
                dg.ngaytao, 
                dg.nguoitao, 
                dg.ngaycapnhat, 
                dg.nguoicapnhat, 
                dg.hoatdong,
                ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
            FROM 
                md_donggoi dg 
            where 
                dg.trangthai = 'SOANTHAO' and 1=1 {2} and dg.hoatdong = 1
            )P 
        where RowNum > @start AND RowNum < @end
        "
        , sidx
        , sord
        , filter);


        var dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            foreach (System.Data.DataColumn column in dt.Columns)
            {
                if (column.DataType == System.Type.GetType("System.DateTime"))
                {
                    try
                    {
                        xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", ((DateTime)row[column.ColumnName]).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", "");
                    }
                }
                else
                    xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", row[column.ColumnName]);
            }
            xml += "</row>";
        }
        xml += "</rows>";

        context.Response.Write(xml);
    }

    public string check_hanghoadocquyen(string md_sanpham_id, string md_doitackinhdoanh_id)
    {
        string msg = "";
        md_hanghoadocquyen hhdq = db.md_hanghoadocquyens.FirstOrDefault(s => s.md_sanpham_id == md_sanpham_id);
        if (hhdq != null)
        {
            md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(s => s.md_doitackinhdoanh_id == hhdq.md_doitackinhdoanh_id);
            if (dtkd != null)
            {
                if (dtkd.md_doitackinhdoanh_id != md_doitackinhdoanh_id)
                {
                    md_quocgia quocgia = db.md_quocgias.FirstOrDefault(s => s.md_quocgia_id == dtkd.md_quocgia_id);
                    string ten_qg = "";
                    if (quocgia != null)
                    {
                        ten_qg = quocgia.ten_quocgia;
                    }
                    //msg = "Mã hàng độc quyền cho khách "+ dtkd.ma_dtkd +" ở thị trường "+ ten_qg +". Bạn có muốn mở bán cho khách hàng của bạn không ? YES/NO";
                    msg = "Mã hàng độc quyền cho khách " + dtkd.ma_dtkd + " ở thị trường " + hhdq.mota + ".\n Bạn có muốn mở bán cho khách hàng của bạn không ? YES/NO";
                }
            }
        }
        return msg;
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
