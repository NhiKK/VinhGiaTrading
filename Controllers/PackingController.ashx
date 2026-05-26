<%@ WebHandler Language="C#" Class="PackingController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;

public class PackingController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();
    public class DongGoi__ID_Ten_DoiGia_ChiPhi
    {
        public string donggoiId { get; set; }
        public string maDongGoi { get; set; }
        public string tenDongGoi { get; set; }
        public string slinner { get; set; }
        public string dvtinner { get; set; }
        public bool? doiGiaDG { get; set; }
        public decimal? cpdg_VuotChuan { get; set; }
        public string slouter { get; set; }
        public string dvtouter { get; set; }
        public string ghichudonggoi { get; set; }
    }

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
        var itemJS = new Dictionary<string, object>();
        var bgdb = false;
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

        string docquyen = "", dtkd_id = "", danhmucHH = "", value_danhmucHH = "";
        if (c_baogia_id != null)
        {
            var bg = db.c_baogias.FirstOrDefault(p => p.c_baogia_id.Equals(c_baogia_id));
            dtkd_id = bg.md_doitackinhdoanh_id;
            bgdb = bg.gia_db.GetValueOrDefault(false);
            itemJS = UserUtils.get_giasanpham(context, c_baogia_id, md_sanpham_id, db);
        }
        else if (c_donhang_id != null)
        {
            var dhang = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(c_donhang_id));
            dtkd_id = dhang.md_doitackinhdoanh_id;
            bgdb = dhang.gia_db.GetValueOrDefault(false);
            itemJS = UserUtils.get_giasanpham(context, c_donhang_id, md_sanpham_id, db);
        }

        docquyen = check_hanghoadocquyen(md_sanpham_id, dtkd_id);
        danhmucHH = check_danhmuchanghoa(md_sanpham_id);
        value_danhmucHH = value_danhmuchanghoa(md_sanpham_id);

        var maDongGoiDB = itemJS["ma_donggoi"].ToString();

        var dgmds = (
                    from a in db.md_donggois
                    join b in db.md_donggoisanphams on a.md_donggoi_id equals b.md_donggoi_id
                    where a.hoatdong == true & (b.md_sanpham_id == md_sanpham_id | a.ma_donggoi == maDongGoiDB)
                    orderby (a.ma_donggoi == maDongGoiDB ? 1 : 0) descending, b.macdinh descending
                    select new DongGoi__ID_Ten_DoiGia_ChiPhi()
                    {
                        donggoiId = a.md_donggoi_id,
                        maDongGoi = a.ma_donggoi,
                        tenDongGoi = a.ten_donggoi,
                        doiGiaDG = a.doigia_donggoi,
                        cpdg_VuotChuan = a.cpdg_vuotchuan,
                        slinner = a.sl_inner.ToString(),
                        dvtinner = db.md_donvitinhs.Where(t => t.md_donvitinh_id == a.dvt_inner).Select(t => t.ten_dvt).FirstOrDefault(),
                        slouter = a.sl_outer.ToString(),
                        dvtouter = db.md_donvitinhs.Where(t => t.md_donvitinh_id == a.dvt_outer).Select(t => t.ten_dvt).FirstOrDefault(),
                        ghichudonggoi = a.mota

                    }
                    ).ToList();

        string selectDG = "", selectGCDG = "";

        var itemdh = (
                    from a in db.md_donggois
                    where a.md_donggoi_id == md_donggoi_id
                    select new DongGoi__ID_Ten_DoiGia_ChiPhi()
                    {
                        donggoiId = a.md_donggoi_id,
                        maDongGoi = a.ma_donggoi,
                        tenDongGoi = a.ten_donggoi,
                        doiGiaDG = a.doigia_donggoi,
                        cpdg_VuotChuan = a.cpdg_vuotchuan,
                        slinner = a.sl_inner.ToString(),
                        dvtinner = db.md_donvitinhs.Where(t => t.md_donvitinh_id == a.dvt_inner).Select(t => t.ten_dvt).FirstOrDefault(),
                        slouter = a.sl_outer.ToString(),
                        dvtouter = db.md_donvitinhs.Where(t => t.md_donvitinh_id == a.dvt_outer).Select(t => t.ten_dvt).FirstOrDefault(),
                        ghichudonggoi = a.mota

                    }
                    ).FirstOrDefault();

        if (itemdh != null)
        {
            selectDG += "<select>";

            selectDG += string.Format(@"<option value=""{0}"" other=""{2}"" cpdgvc=""{3}"" ghichudonggoi=""{4}"">{1}</option>",
                itemdh.donggoiId,
                itemdh.maDongGoi + " (" + itemdh.slinner + " " + itemdh.dvtinner + " // " + itemdh.slouter + " " + itemdh.dvtouter + ")",
                itemdh.doiGiaDG.GetValueOrDefault(false),
                itemdh.cpdg_VuotChuan.GetValueOrDefault(0),
                itemdh.ghichudonggoi
                );

            selectDG += "</select>";

            dgmds = dgmds.Where(s => s.donggoiId != itemdh.donggoiId).ToList();
        }

        if (dgmds.Count > 0)
        {
            selectDG += "<select>";
            foreach (var item in dgmds)
            {
                selectDG += string.Format(@"<option value=""{0}"" other=""{2}"" cpdgvc=""{3}"" ghichudonggoi=""{4}"">{1}</option>",
                    item.donggoiId,
                    item.maDongGoi + " (" + item.slinner + " " + item.dvtinner + " // " + item.slouter + " " + item.dvtouter + ")",
                    item.doiGiaDG.GetValueOrDefault(false),
                    item.cpdg_VuotChuan.GetValueOrDefault(0),
                    item.ghichudonggoi
                    );
            }
            selectDG += "</select>";
        }

        context.Response.Write(selectDG + "(##)" + docquyen + "(##)" + itemJS["gia"].ToString() + "(##)" + itemJS["phi"].ToString() + "(##)" + danhmucHH + "(##)" + selectGCDG + "(##)" + value_danhmucHH);
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

            db.md_donggois.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update md_donggoi set hoatdong = 0 where md_donggoi_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
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
            " FROM md_donggoi dg where dg.trangthai = 'HIEULUC' and 1=1 " + filter;

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
                dg.trangthai = 'HIEULUC' and 1=1 {2} and dg.hoatdong = 1
            )P 
        where RowNum > @start AND RowNum < @end"
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

    public string check_danhmuchanghoa(string md_sanpham_id)
    {
        string msg = "";
        var hanghoa = db.md_sanphams.Where(s => s.md_sanpham_id == md_sanpham_id).FirstOrDefault();
        if (hanghoa != null)
        {
            var dmhh = db.md_danhmuchanghoas.Where(s => s.md_danhmuchanghoa_id == hanghoa.md_tinhtranghanghoa_id).FirstOrDefault();
            if (dmhh != null)
            {
                if (dmhh.ma_danhmuc == "DM007")
                    msg = string.Format(@"Mã hàng đang nằm trong danh mục ""{0}"", hãy xác nhận trước khi mở bán!", dmhh.ten_danhmuc);
                //msg = dmhh.ten;
            }
        }
        return msg;
    }

    public string value_danhmuchanghoa(string md_sanpham_id)
    {
        string msg = "";
        var hanghoa = db.md_sanphams.Where(s => s.md_sanpham_id == md_sanpham_id).FirstOrDefault();
        if (hanghoa != null)
        {
            var dmhh = db.md_danhmuchanghoas.Where(s => s.md_danhmuchanghoa_id == hanghoa.md_tinhtranghanghoa_id).FirstOrDefault();
            if (dmhh != null)
            {
                msg = string.Format(@"{0},", dmhh.ten_danhmuc);
            }
        }
        return msg;
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
