<%@ WebHandler Language="C#" Class="QuotationDetailController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class QuotationDetailController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getgiasanpham":
                this.getGiaSanPham(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "check_quycachdonggoi":
                this.check_quycachdonggoi(context);
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

    public void check_quycachdonggoi(HttpContext context)
    {
        string msg = "";
        string sanpham_id = context.Request.QueryString["md_sanpham_id"];
        md_donggoi donggoi = db.md_donggois.FirstOrDefault(s => s.md_donggoi_id == context.Request.QueryString["md_donggoi_id"]);

        //kiem tra quy cach dong goi
        var md_sanpham_id = db.md_sanphams.Where(s => s.ma_sanpham == sanpham_id).Select(s => s.md_sanpham_id).FirstOrDefault();
        var count_qcdg = db.md_donggoisanphams.Where(s => s.md_sanpham_id == md_sanpham_id & s.md_donggoi_id == donggoi.md_donggoi_id).FirstOrDefault();
        if (count_qcdg.md_donggoi_id != null)
        {
            if (count_qcdg.macdinh == true)
                msg = donggoi.mota;
            context.Response.Write(msg);

        }
    }

    public void getGiaSanPham(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        string c_baogia_id = context.Request.QueryString["c_baogia_id"];
        string md_sanpham_id = context.Request.QueryString["md_sanpham_id"];
        var itemJS = UserUtils.get_giasanpham(context, c_baogia_id, md_sanpham_id, db);
        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(itemJS));
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_chitietbaogia_id, c_chitietbaogia_id from c_chitietbaogia where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        string msg = "";
        int check = 0;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String baogia_id = context.Request.Form["c_baogia_id"];
        c_baogia baogia = db.c_baogias.Single(p => p.c_baogia_id.Equals(baogia_id));
        String md_donggoi_id = context.Request.Form["md_donggoi_id"];

        md_sanpham sp = db.md_sanphams.Single(p => p.md_sanpham_id.Equals(context.Request.Form["sanpham_id"]));
        string docquyen = context.Request.Form["docquyen"];
        if (md_donggoi_id == null)
        {
            jqGridHelper.Utils.writeResult(0, "Bạn chưa chọn đóng gói");
        }
        else
        {
            md_donggoi donggoi = db.md_donggois.Single(p => p.md_donggoi_id.Equals(md_donggoi_id));
            if (donggoi == null)
            {
                jqGridHelper.Utils.writeResult(0, "Đóng gói không tồn tại!");
            }
            else
            {
                if (baogia.md_trangthai_id.Equals("HIEULUC"))
                {
                    jqGridHelper.Utils.writeResult(0, "Báo giá đã được hiệu lực. Không thể thay đổi thông tin sản phẩm!");
                }
                else if (baogia.md_trangthai_id.Equals("KETTHUC"))
                {
                    jqGridHelper.Utils.writeResult(0, "Báo giá đã được kết thúc. Không thể thay đổi thông tin sản phẩm!");
                }
                else
                {
                    c_chitietbaogia m = db.c_chitietbaogias.Single(p => p.c_chitietbaogia_id == context.Request.Form["id"]);
                    m.c_baogia_id = baogia_id;
                    //m.md_sanpham_id = context.Request.Form["sanpham_id"];
                    m.ma_sanpham_khach = context.Request.Form["ma_sanpham_khach"];
                    m.sothutu = int.Parse(context.Request.Form["sothutu"]);
                    m.giafob = decimal.Parse(context.Request.Form["giafob"]);
                    m.soluong = decimal.Parse(context.Request.Form["soluong"]);
                    m.ghichu = context.Request.Form["ghichu"];
                    m.trongluong = sp.trongluong;

                    // Copy Dong Goi
                    m.md_donggoi_id = donggoi.md_donggoi_id;
                    m.sl_inner = donggoi.sl_inner;
                    m.l1 = donggoi.l1;
                    m.w1 = donggoi.w1;
                    m.h1 = donggoi.h1;
                    m.sl_outer = donggoi.sl_outer;
                    m.l2 = donggoi.l2_mix;
                    m.w2 = donggoi.w2_mix;
                    m.h2 = donggoi.h2_mix;
                    m.v2 = donggoi.v2;
                    m.sl_cont = donggoi.soluonggoi_ctn;
                    m.vd = donggoi.vd;
                    m.vn = donggoi.vn;
                    m.vl = donggoi.vl;
                    m.ghichu_vachngan = donggoi.ghichu_vachngan;

                    m.mota = context.Request.Form["mota"];
                    m.hoatdong = hd;
                    m.ngaycapnhat = DateTime.Now;
                    m.nguoicapnhat = UserUtils.getUser(context);
                    m.docquyen = docquyen;
                    m.ghichudonggoingoai = donggoi.mota;

                    //kiem tra hang hoa doc quyen
                    int count_dq = (from hdq in db.md_hanghoadocquyens
                                    where !hdq.md_doitackinhdoanh_id.Equals(baogia.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(m.md_sanpham_id)
                                    select new { hdq.md_hanghoadocquyen_id }).Count();

                    int count_dqdt = (from hdq in db.md_hanghoadocquyens
                                      where hdq.md_doitackinhdoanh_id.Equals(baogia.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(m.md_sanpham_id)
                                      select new { hdq.md_hanghoadocquyen_id }).Count();

                    if (count_dq > 0 && count_dqdt == 0 && donggoi.doigia_donggoi == true)
                    {
                        m.trangthai = "DGDGDQ";
                    }
                    else if (count_dq > 0 && count_dqdt == 0)
                    {
                        m.trangthai = "DQ";
                    }
                    else if (donggoi.doigia_donggoi == true)
                    {
                        m.trangthai = "DGDG";
                    }
                    else
                    {
                        m.trangthai = "BT";
                    }

                    int count_ = (from dbg in db.c_chitietbaogias
                                  where dbg.md_sanpham_id.Equals(m.md_sanpham_id) && dbg.c_baogia_id.Equals(m.c_baogia_id) && !dbg.c_chitietbaogia_id.Equals(m.c_chitietbaogia_id)
                                  select new { dbg.c_chitietbaogia_id }).Count();
                    if (count_ > 0)
                    {
                        msg = "Mã hàng này đã tồn tại trong báo giá đang chọn!";
                        check++;
                    }
                    if (check == 0)
                    {
                        db.SubmitChanges();
                        jqGridHelper.Utils.writeResult(1, "Cập nhật thành công!");
                    }
                    else
                    {
                        jqGridHelper.Utils.writeResult(0, msg);
                    }
                }
            }
        }
    }

    public void add(HttpContext context)
    {

        string msg = "";
        int check = 0;
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String baogia_id, sanpham_id, ma_sp_khach, ma_sanpham;
        baogia_id = context.Request.Form["c_baogia_id"];
        sanpham_id = context.Request.Form["sanpham_id"];
        ma_sanpham = context.Request.Form["ma_sanpham"];
        ma_sp_khach = context.Request.Form["ma_sanpham_khach"];

        string md_donggoi_id = context.Request.Form["md_donggoi_id"];
        string docquyen = context.Request.Form["docquyen"];

        c_baogia baogia = db.c_baogias.Single(p => p.c_baogia_id.Equals(baogia_id));
        if (baogia.md_trangthai_id.Equals("HIEULUC"))
        {
            jqGridHelper.Utils.writeResult(0, "Báo giá đã được hiệu lực. Không thể thêm sản phẩm!");
        }
        else
        {
            // check product in quotation
            var count = db.c_chitietbaogias.Where(s => s.c_baogia_id == baogia_id & s.md_sanpham_id == sanpham_id).Count();
            if (count > 0)
            {
                msg = "Mã sản phẩm <b>'" + ma_sanpham + "'</b> đã có trong Quotation";
                check++;
            }
            else
            {
                // get product information
                md_sanpham pro = db.md_sanphams.Single(p => p.md_sanpham_id.Equals(sanpham_id));
                //-- get product information

                // select product price
                decimal price = decimal.Parse(context.Request.Form["giafob"]);
                //decimal price = (decimal)mdbc.ExecuteScalarProcedure("getGiaMuaSp", "@md_sanpham_id", sanpham_id, "@md_doitackinhdoanh_id", db.c_baogias.FirstOrDefault(p => p.c_baogia_id.Equals(baogia_id)).md_doitackinhdoanh_id, "@loai_bg", true, "@ngaytinhgia", baogia.ngaybaogia);
                // -- select product price

                if (price < 0)
                {
                    jqGridHelper.Utils.writeResult(0, "Mã hàng không có giá mặc định!");
                }
                else
                {
                    //  select packing
                    String dvt_inner = "", dvt_outer = "", ghichu_vachngan = "", trangthai = "", ghichudonggoingoai = "";
                    System.Nullable<decimal> sl_inner = 0, soluonggoi_ctn = 0, sl_outer = 0, l1 = 0, w1 = 0, h1 = 0, l2_mix = 0, w2_mix = 0, h2_mix = 0, v2 = 0, vd = 0, vn = 0, vl = 0;
                    // String getPacking = @"SELECT
                    // dg.md_donggoi_id, dg.sl_inner
                    // , dg.dvt_inner
                    // , dg.l1, dg.w1, dg.h1
                    // , dg.sl_outer
                    // , dg.dvt_outer
                    // , dg.l2_mix, dg.w2_mix, dg.h2_mix, dg.soluonggoi_ctn
                    // , dg.v2 , dg.vd, dg.vn, dg.vl, dg.ghichu_vachngan, dg.doigia_donggoi
                    // FROM
                    // md_donggoi dg, md_donggoisanpham dgsp
                    // WHERE
                    // dg.md_donggoi_id = dgsp.md_donggoi_id
                    // AND dgsp.macdinh = 1
                    // AND dgsp.md_sanpham_id = @md_sanpham_id";

                    // System.Data.SqlClient.SqlDataReader rd = mdbc.ExecuteReader(getPacking, "@md_sanpham_id", sanpham_id);
                    // if (rd.HasRows) {
                    // md_donggoi_id = rd["md_donggoi_id"].ToString();
                    // dvt_inner = rd["dvt_inner"].ToString();
                    // dvt_outer = rd["dvt_outer"].ToString();
                    // ghichu_vachngan = rd["ghichu_vachngan"].ToString();

                    // sl_inner = decimal.Parse(rd["sl_inner"].ToString());
                    // soluonggoi_ctn = decimal.Parse(rd["soluonggoi_ctn"].ToString());
                    // sl_outer = decimal.Parse(rd["sl_outer"].ToString());
                    // l1 = decimal.Parse(rd["l1"].ToString());
                    // w1 = decimal.Parse(rd["w1"].ToString());
                    // h1 = decimal.Parse(rd["h1"].ToString());
                    // l2_mix = decimal.Parse(rd["l2_mix"].ToString());
                    // w2_mix = decimal.Parse(rd["w2_mix"].ToString());
                    // h2_mix = decimal.Parse(rd["h2_mix"].ToString());
                    // v2 = decimal.Parse(rd["v2"].ToString());
                    // vd = decimal.Parse(rd["vd"].ToString());
                    // vn = decimal.Parse(rd["vn"].ToString());
                    // vl = decimal.Parse(rd["vl"].ToString());
                    // }
                    // else {
                    md_donggoi dg_sel = db.md_donggois.FirstOrDefault(s => s.md_donggoi_id == md_donggoi_id);
                    if (dg_sel != null)
                    {
                        dvt_inner = dg_sel.dvt_inner;
                        dvt_outer = dg_sel.dvt_outer;
                        ghichu_vachngan = dg_sel.ghichu_vachngan;

                        sl_inner = dg_sel.sl_inner;
                        soluonggoi_ctn = dg_sel.soluonggoi_ctn;
                        sl_outer = dg_sel.sl_outer;
                        l1 = dg_sel.l1;
                        w1 = dg_sel.w1;
                        h1 = dg_sel.h1;
                        l2_mix = dg_sel.l2_mix;
                        w2_mix = dg_sel.w2_mix;
                        h2_mix = dg_sel.h2_mix;
                        v2 = dg_sel.v2;
                        vd = dg_sel.vd;
                        vn = dg_sel.vn;
                        vl = dg_sel.vl;
                        ghichudonggoingoai = dg_sel.mota;
                    }
                    //}
                    //getOrderNo
                    String getOrder = "select max(sothutu) as count from c_chitietbaogia where c_baogia_id = @c_baogia_id";
                    object omax = mdbc.ExecuteScalar(getOrder, "@c_baogia_id", baogia_id);
                    int maxOrder = (omax.ToString().Equals("")) ? 0 : (int)omax;

                    c_chitietbaogia mnu = new c_chitietbaogia
                    {

                        c_chitietbaogia_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                        c_baogia_id = context.Request.Form["c_baogia_id"],
                        md_sanpham_id = context.Request.Form["sanpham_id"],
                        ma_sanpham_khach = context.Request.Form["ma_sanpham_khach"],
                        md_cangbien_id = pro.md_cangbien_id,
                        mota_tienganh = pro.mota_tienganh,
                        mota_tiengviet = pro.mota_tiengviet,
                        ghichu_vachngan = ghichu_vachngan,
                        sothutu = maxOrder + 10,
                        giafob = price,
                        soluong = decimal.Parse(context.Request.Form["soluong"]),
                        md_donggoi_id = md_donggoi_id,
                        sl_inner = sl_inner,
                        l1 = l1,
                        w1 = w1,
                        h1 = h1,
                        sl_outer = sl_outer,
                        l2 = l2_mix,
                        w2 = w2_mix,
                        h2 = h2_mix,
                        v2 = v2,
                        vd = vd,
                        vn = vn,
                        vl = vl,
                        sl_cont = soluonggoi_ctn,
                        ghichu = context.Request.Form["ghichu"],
                        docquyen = docquyen,
                        mota = context.Request.Form["mota"],
                        hoatdong = hd,
                        ngaytao = DateTime.Now,
                        ngaycapnhat = DateTime.Now,
                        nguoitao = UserUtils.getUser(context),
                        nguoicapnhat = UserUtils.getUser(context),
                        trangthai = trangthai,
                        ghichudonggoingoai = ghichudonggoingoai,
                    };

                    //kiem tra ma hang da ton tai chua
                    int count_r = (from dbg in db.c_chitietbaogias
                                   where dbg.md_sanpham_id.Equals(mnu.md_sanpham_id) && dbg.c_baogia_id.Equals(mnu.c_baogia_id)
                                   select new { dbg.c_chitietbaogia_id }).Count();
                    if (count_r == 0)
                    {
                        //db.c_chitietbaogias.InsertOnSubmit(mnu);
                        //db.SubmitChanges();
                    }
                    else
                    {
                        msg = "Đã tồn tại mã hàng trong báo giá!";
                        check++;
                    }
                    //kiem tra hang hoa doc quyen
                    // int count_dq = (from hdq in db.md_hanghoadocquyens
                    // where !hdq.md_doitackinhdoanh_id.Equals(baogia.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(mnu.md_sanpham_id)
                    // select new { hdq.md_hanghoadocquyen_id }).Count();
                    // int count_dqdt = (from hdq in db.md_hanghoadocquyens
                    // where hdq.md_doitackinhdoanh_id.Equals(baogia.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(mnu.md_sanpham_id)
                    // select new { hdq.md_hanghoadocquyen_id }).Count();
                    // if (count_dq > 0 && count_dqdt == 0)
                    // {
                    // msg = "Mã hàng này đang được bán độc quyền cho 1 khách hàng khác!";
                    // check++;
                    // }

                    md_donggoi donggoi = db.md_donggois.Single(p => p.md_donggoi_id.Equals(md_donggoi_id));
                    //kiem tra hang hoa doc quyen
                    int count_dq = (from hdq in db.md_hanghoadocquyens
                                    where !hdq.md_doitackinhdoanh_id.Equals(baogia.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(mnu.md_sanpham_id)
                                    select new { hdq.md_hanghoadocquyen_id }).Count();
                    int count_dqdt = (from hdq in db.md_hanghoadocquyens
                                      where hdq.md_doitackinhdoanh_id.Equals(baogia.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(mnu.md_sanpham_id)
                                      select new { hdq.md_hanghoadocquyen_id }).Count();

                    if (count_dq > 0 && count_dqdt == 0 && donggoi.doigia_donggoi == true)
                    {
                        mnu.trangthai = "DGDGDQ";
                        mnu.mota += "DGDG & DQ";
                    }
                    else if (count_dq > 0 && count_dqdt == 0)
                    {
                        mnu.trangthai = "DQ";
                        mnu.mota += "DGDG & DQ";
                    }
                    else if (donggoi.doigia_donggoi == true)
                    {
                        mnu.trangthai = "DGDG";
                    }
                    else
                    {
                        mnu.trangthai = "BT";
                    }

                    int count_cbien = (from hdq in db.md_cangxuathangs
                                       where hdq.md_sanpham_id.Equals(pro.md_sanpham_id) && hdq.md_cangbien_id.Equals(baogia.md_cangbien_id)
                                       select new { hdq.md_cangxuathang_id }).Count();

                    /*if (!pro.md_cangbien_id.Equals(baogia.md_cangbien_id) && baogia.isform.Value.Equals(false))
					{
						msg = "Mã hàng không thuộc cảng biển được chọn trên QO!";
						check++;
					}*/

                    if (count_cbien <= 0 && baogia.isform.Value.Equals(false))
                    {
                        msg = "Mã hàng không thuộc cảng biển được chọn trên QO!";
                        check++;
                    }

                    if (check == 0)
                    {
                        db.c_chitietbaogias.InsertOnSubmit(mnu);
                        db.SubmitChanges();
                        jqGridHelper.Utils.writeResult(1, "Thêm mới thành công 2!");
                    }
                }
            }
        }

        if (check > 0)
            jqGridHelper.Utils.writeResult(0, msg);
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        String selCount = "select count(*) as count from c_baogia bg, c_chitietbaogia ctbg " +
            " where bg.c_baogia_id = ctbg.c_baogia_id " +
            " AND bg.md_trangthai_id = 'HIEULUC' " +
            " AND ctbg.c_chitietbaogia_id = @c_chitietbaogia_id";
        int count = (int)mdbc.ExecuteScalar(selCount, "@c_chitietbaogia_id", id);
        if (count != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Bạn không thể xóa dòng hàng khi quotation đã hiệu lực.!");
        }
        else
        {
            String sql = "delete c_chitietbaogia where c_chitietbaogia_id = @c_chitietbaogia_id";
            mdbc.ExcuteNonQuery(sql, "@c_chitietbaogia_id", id);
            jqGridHelper.Utils.writeResult(1, "Đã xóa một dòng hàng.!");
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

        String quotationId = context.Request.QueryString["quotationId"];
        bool ghepbo = false;
        c_baogia bg = db.c_baogias.FirstOrDefault(s => s.c_baogia_id == quotationId);
        if (bg != null)
        {
            if (bg.ghepbo == true)
            {
                ghepbo = true;
            }
        }
        String sqlCount = @"SELECT COUNT(1) AS count 
        FROM c_chitietbaogia ctbg with (nolock) 
		left join c_baogia bg with (nolock) on ctbg.c_baogia_id = bg.c_baogia_id 
		left join md_sanpham sp with (nolock) on ctbg.md_sanpham_id = sp.md_sanpham_id
		left join md_donggoi dg with (nolock) on ctbg.md_donggoi_id = dg.md_donggoi_id 
		left join md_hscode hs with (nolock) on hs.md_hscode_id = sp.md_hscode_id 
        left join md_danhmuchanghoa tt with (nolock) on sp.md_tinhtranghanghoa_id = tt.md_danhmuchanghoa_id
		WHERE 1=1 AND ctbg.c_baogia_id = N'{0}' {1}";

        if (quotationId != null)
        {
            sqlCount = string.Format(sqlCount, quotationId, filter);
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

        string orderby = "";
        if (sidx.Equals("") || sidx == null)
        {
            sidx = "ctbg.c_chitietbaogia_id";
        }

        if (ghepbo == true)
        {
            orderby = "ORDER BY substring(sp.ma_sanpham,0, 8) asc, substring(sp.ma_sanpham,13, 2) asc, substring(sp.ma_sanpham,10, 2) asc";
        }
        else
        {
            orderby = "ORDER BY " + sidx + " " + sord;
        }
        string strsql = @"select * from(
            select ctbg.c_chitietbaogia_id, bg.c_baogia_id, sp.trangthai, ctbg.trangthai as trangthai_ctbg, ctbg.sothutu, sp.md_sanpham_id, sp.ma_sanpham, hs.hscode, ctbg.ma_sanpham_khach,
            ctbg.giafob, dg.cpdg_vuotchuan, ctbg.soluong, dg.ten_donggoi, ctbg.sl_inner,
            ctbg.l1, ctbg.w1, ctbg.h1, ctbg.sl_outer, ctbg.l2, ctbg.w2, ctbg.h2, sp.trongluong, sp.dientich, ctbg.v2,  ctbg.sl_cont,
            ctbg.ghichu, ctbg.ngaytao, ctbg.nguoitao, ctbg.ngaycapnhat, ctbg.nguoicapnhat, ctbg.mota, ctbg.hoatdong, ctbg.docquyen, ctbg.ghichudonggoingoai, tt.ten_danhmuc,
            ROW_NUMBER() OVER ({1}) as RowNum 
            
			FROM c_chitietbaogia ctbg with (nolock) 
			left join c_baogia bg with (nolock) on ctbg.c_baogia_id = bg.c_baogia_id 
			left join md_sanpham sp with (nolock) on ctbg.md_sanpham_id = sp.md_sanpham_id
			left join md_donggoi dg with (nolock) on ctbg.md_donggoi_id = dg.md_donggoi_id 
			left join md_hscode hs with (nolock) on hs.md_hscode_id = sp.md_hscode_id 
            left join md_danhmuchanghoa tt with (nolock) on sp.md_tinhtranghanghoa_id = tt.md_danhmuchanghoa_id
            WHERE 1=1 AND ctbg.c_baogia_id = N'{0}' {2}
            )P where RowNum > @start AND RowNum < @end";

        if (quotationId != null)
        {
            strsql = string.Format(strsql, quotationId, orderby, filter);
        }
        else
        {
            strsql = string.Format(strsql, 0, orderby, filter);
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
            foreach (System.Data.DataColumn column in dt.Columns)
            {
                if (column.DataType == System.Type.GetType("System.DateTime"))
                    xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", ((DateTime)row[column.ColumnName]).ToString("dd/MM/yyyy"));
                else
                    xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", row[column.ColumnName]);
            }
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
