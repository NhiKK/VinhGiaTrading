<%@ WebHandler Language="C#" Class="DongDonHangController" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Data;

public class DongDonHangController : IHttpHandler
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
            case "getgiasanpham":
                this.getGiaSanPham(context);
                break;
            case "getmotagia":
                this.getMoTaGia(context);
                break;
            case "check_quycachdonggoi":
                this.check_quycachdonggoi(context);
                break;
            case "doiphiDG0":
                this.doiphiDG0(context);
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

    public void doiphiDG0(HttpContext context)
    {
        string msg = "";
        string idpo = context.Request.Form["idpo"];
        var po = db.c_donhangs.Where(s => s.c_donhang_id == idpo).FirstOrDefault();
        if (po == null)
        {
            msg += String.Format(@"<div error=1 style=""color:red; padding: 3px;"">Đơn hàng bạn chọn không tồn tại</div>");
        }
        //else if (po.md_trangthai_id != "SOANTHAO")
        //{
        //    msg += String.Format(@"<div error=1 style=""color:red; padding: 3px;"">Đơn hàng {0} không ở trạng thái ""Soạn Thảo""</div>", po.sochungtu);
        //}
        else
        {
            var invIds = db.c_dongpklinvs.Where(s => s.c_donhang_id == po.c_donhang_id).Select(s=>s.c_packinginvoice_id).ToList();
            var inv = db.c_packinginvoices.Where(s => invIds.Contains(s.c_packinginvoice_id)).OrderBy(s => s.ngaytao).FirstOrDefault();
            if(inv != null)
            {
                if (inv.cpdg_vuotchuan.GetValueOrDefault(0) >= po.cpdg_vuotchuan.GetValueOrDefault(0))
                    inv.cpdg_vuotchuan = inv.cpdg_vuotchuan.GetValueOrDefault(0) - po.cpdg_vuotchuan.GetValueOrDefault(0);
            }

            foreach (var donghang in db.c_dongdonhangs.Where(s => s.c_donhang_id == po.c_donhang_id).ToList())
            {
                donghang.chiphidonggoi = 0;
            }

            db.SubmitChanges();

            msg += String.Format(@"<div style=""color:blue; padding: 3px;"">Cập nhật phí đóng gói thành công</div>");
        }

        context.Response.Write(msg);
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

    public void getMoTaGia(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        string c_donhang_id = context.Request.QueryString["c_donhang_id"];
        string md_sanpham_id = context.Request.QueryString["md_sanpham_id"];
        var donhang = db.c_donhangs.FirstOrDefault(d => d.c_donhang_id.Equals(c_donhang_id));
        var sp = db.md_sanphams.FirstOrDefault(s => s.md_sanpham_id.Equals(md_sanpham_id));
        if (donhang != null)
        {
            if (sp != null)
            {
                string gia_sp = "1";
                string dtkd_id = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(c_donhang_id)).md_doitackinhdoanh_id;
                if (donhang.gia_db == true)
                    gia_sp = (string)mdbc.ExecuteScalarProcedure("getMoTaGia_Dacbiet", "@md_sanpham_id", md_sanpham_id, "@md_doitackinhdoanh_id", dtkd_id, "@ngaytinhgia", donhang.ngaylap.Value.ToString("yyyy-MM-dd"));
                else if (donhang.isdonhangtmp == true)
                    gia_sp = (string)mdbc.ExecuteScalarProcedure("getMoTaGia_Mau", "@md_sanpham_id", md_sanpham_id, "@md_doitackinhdoanh_id", dtkd_id, "@loai_bg", true, "@ngaytinhgia", donhang.ngaylap.Value.ToString("yyyy-MM-dd"), "@mau", 1);
                else
                    gia_sp = (string)mdbc.ExecuteScalarProcedure("getMoTaGia", "@md_sanpham_id", md_sanpham_id, "@md_doitackinhdoanh_id", dtkd_id, "@loai_bg", true, "@ngaytinhgia", donhang.ngaylap.Value.ToString("yyyy-MM-dd"));

                context.Response.Write(gia_sp);
            }
            else
            {
                context.Response.Write("");
            }
        }
        else
        {
            context.Response.Write("");
        }
    }

    public void getGiaSanPham(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        string c_donhang_id = context.Request.QueryString["c_donhang_id"];
        string md_sanpham_id = context.Request.QueryString["md_sanpham_id"];
        var itemJS = UserUtils.get_giasanpham(context, c_donhang_id, md_sanpham_id, db);

        var entries = itemJS.Select(d =>
           string.Format("\"{0}\": [\"{1}\"]", d.Key, string.Join(",", d.Value).Replace("\"", "\\\"")));
        context.Response.Write("{" + string.Join(",", entries) + "}");
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_dongdonhang_id, line from c_dongdonhang where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        try
        {
            string msg = "";
            int check = 0;
            String h = context.Request.Form["hoatdong"].ToLower();
            bool hd = false;
            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }

            nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(context)));

            String md_sanpham_id = context.Request.Form["sanpham_id"];
            string docquyen = context.Request.Form["docquyen"];
            // Lấy đóng gói của sản phẩm
            String md_donggoi_id = context.Request.Form["md_donggoi_id"];
            String id_donggoi_outer = context.Request.Form["id_donggoi_outer"];
            var phiVal = decimal.Parse(context.Request.Form["phi"]);
            var gia_sp = decimal.Parse(context.Request.Form["giafob"]);

            if (md_donggoi_id == null)
            {
                jqGridHelper.Utils.writeResult(0, "Bạn chưa chọn đóng gói");
            }
            else if (phiVal < 0)
            {
                jqGridHelper.Utils.writeResult(0, "Phí phải lớn hơn hoặc bằng 0");
            }
            else if (gia_sp == 0)
            {
                jqGridHelper.Utils.writeResult(0, "Giá nằm trong phiên bản giá đã hết hạn.!");
            }
            else if (gia_sp == -1)
            {
                jqGridHelper.Utils.writeResult(0, "Không tìm thấy giá.!");
            }
            else
            {
                md_donggoi donggoi = db.md_donggois.Single(p => p.md_donggoi_id.Equals(md_donggoi_id));
                c_dongdonhang m = db.c_dongdonhangs.Single(p => p.c_dongdonhang_id == context.Request.Form["id"]);
                //md_donggoi donggoi_outer = db.md_donggois.Single(p => p.md_donggoi_id.Equals(id_donggoi_outer));

                var donhang = db.c_donhangs.Single(d => d.c_donhang_id.Equals(m.c_donhang_id));
                if (donhang.md_trangthai_id.Equals("SOANTHAO"))
                {
                    m.c_donhang_id = context.Request.Form["c_donhang_id"];
                    //m.md_sanpham_id = context.Request.Form["sanpham_id"];
                    m.ma_sanpham_khach = context.Request.Form["ma_sanpham_khach"];
                    m.nhacungungid = context.Request.Form["nhacungungid"];
                    m.sothutu = int.Parse(context.Request.Form["sothutu"]);
                    if (nv.isadmin.Value | nv.suaGia.GetValueOrDefault(false))
                    {
                        m.giafob = gia_sp;
                    }
                    m.phi = phiVal;
                    m.soluong = decimal.Parse(context.Request.Form["soluong"]);
                    m.soluong_conlai = decimal.Parse(context.Request.Form["soluong"]);



                    // Thành phần đóng gói
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
                    m.vd = donggoi.vd;
                    m.vn = donggoi.vn;
                    m.vl = donggoi.vl;
                    m.ghichu_vachngan = donggoi.ghichu_vachngan;
                    m.sl_cont = donggoi.soluonggoi_ctn;
                    m.chiphidonggoi = decimal.Parse(context.Request.Form["chiphidonggoi"]);
                    m.ghichu = context.Request.Form["ghichu"];
                    m.ghichudonggoi = context.Request.Form["ghichudonggoi"];
                    m.mota = context.Request.Form["mota"];
                    m.hoatdong = hd;
                    m.nguoicapnhat = UserUtils.getUser(context);
                    m.ngaycapnhat = DateTime.Now;
                    m.docquyen = docquyen;
                    m.ghichudonggoingoai = donggoi.mota;

                    //kiem tra hang hoa doc quyen
                    int count_dq = (from hdq in db.md_hanghoadocquyens
                                    where !hdq.md_doitackinhdoanh_id.Equals(donhang.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(m.md_sanpham_id)
                                    select new { hdq.md_hanghoadocquyen_id }).Count();

                    int count_dqdt = (from hdq in db.md_hanghoadocquyens
                                      where hdq.md_doitackinhdoanh_id.Equals(donhang.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(m.md_sanpham_id)
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


                    // if (count_dq > 0 && count_dqdt == 0)
                    // {
                    // msg = "Mã hàng này đang được bán độc quyền cho 1 khách hàng khác!";
                    // check++;
                    // }

                    int count_ = (from dbg in db.c_dongdonhangs
                                  where dbg.md_sanpham_id.Equals(m.md_sanpham_id) && dbg.c_donhang_id.Equals(m.c_donhang_id) && !dbg.c_dongdonhang_id.Equals(m.c_dongdonhang_id)
                                  select new { dbg.c_dongdonhang_id }).Count();

                    if (count_ > 0)
                    {
                        msg = "Mã hàng này đã tồn tại trong PO đang chọn!";
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
                else if (donhang.md_trangthai_id.Equals("HIEULUC"))
                {
                    jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi đơn hàng đã hiệu lực!");
                }
                else if (donhang.md_trangthai_id.Equals("KETTHUC"))
                {
                    jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi đơn hàng đã kết thúc!");
                }
            }
        }
        catch (Exception ex)
        {
            jqGridHelper.Utils.writeResult(0, ex.Message);
        }
    }

    public void add(HttpContext context)
    {
        // try
        //{
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String md_sanpham_id = context.Request.Form["sanpham_id"];
        String c_donhang_id = context.Request.Form["c_donhang_id"];
        string docquyen = context.Request.Form["docquyen"];
        // Lấy thông tin sản phẩm
        md_sanpham sanpham = db.md_sanphams.Single(p => p.md_sanpham_id.Equals(md_sanpham_id));

        // Lấy thông tin đơn hàng
        var donhang = db.c_donhangs.Single(d => d.c_donhang_id.Equals(c_donhang_id));

        var itemJS = UserUtils.get_giasanpham(context, c_donhang_id, md_sanpham_id, db);
        string barcode = "";
        decimal gia_sp = decimal.Parse(context.Request.Form["giafob"]); ;
        try
        {
            barcode = itemJS["barcode"].ToString();
        }
        catch { }

        // Lấy đóng gói của sản phẩm
        //md_donggoi donggoi = LinqUtils.getDongGoi(md_sanpham_id);
        md_donggoi donggoi = db.md_donggois.FirstOrDefault(s => s.md_donggoi_id == context.Request.Form["md_donggoi_id"]);
        //md_donggoi donggoi_outer = db.md_donggois.FirstOrDefault(s => s.md_donggoi_id == context.Request.Form["id_donggoi_outer"]);
        // Lấy số thứ tự cao nhất trong đơn hàng
        int maxLine = LinqUtils.getMaxSttDonHang(c_donhang_id);

        int count_ = (from ddh in db.c_dongdonhangs
                      where ddh.c_donhang_id.Equals(c_donhang_id) && ddh.md_sanpham_id.Equals(md_sanpham_id)
                      select new { ddh.c_dongdonhang_id }).Count();

        //kiem tra hang hoa doc quyen
        int count_dq = (from hdq in db.md_hanghoadocquyens
                        where !hdq.md_doitackinhdoanh_id.Equals(donhang.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(md_sanpham_id)
                        select new { hdq.md_hanghoadocquyen_id }).Count();
        int count_dqdt = (from hdq in db.md_hanghoadocquyens
                          where hdq.md_doitackinhdoanh_id.Equals(donhang.md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(md_sanpham_id)
                          select new { hdq.md_hanghoadocquyen_id }).Count();

        // kiem tra tinh hop le doi voi ma hang co 2 nha cung ung
        string ncu_line = sanpham.nhacungung;
        var nhacungung = db.md_doitackinhdoanhs.FirstOrDefault(nc => nc.md_doitackinhdoanh_id.Equals(sanpham.nhacungung));

        string ncc_cangbien = "", dh_cangbien = "";
        try { ncc_cangbien = nhacungung.md_cangbien_id; } catch { }
        try { dh_cangbien = donhang.md_cangbien_id; } catch { }
        if (ncc_cangbien != dh_cangbien)
        {
            int count_donghang = (from ddh in db.c_dongdonhangs
                                  where ddh.c_donhang_id.Equals(c_donhang_id)
                                  select new { ddh.c_dongdonhang_id }).Count();
            string sql_cb = @"select top 1 md_doitackinhdoanh_id
                                from md_nhacungungmacdinh ncmd
                                where md_sanpham_id = '" + sanpham.md_sanpham_id + @"'
	                                and md_doitackinhdoanh_id in (select md_doitackinhdoanh_id from md_doitackinhdoanh where md_cangbien_id = '" + donhang.md_cangbien_id + "')";
            if (count_donghang > 0)
            {
                sql_cb += " and md_doitackinhdoanh_id in (select distinct nhacungungid from c_dongdonhang where c_donhang_id = '" + donhang.c_donhang_id + "')";
            }

            DataTable dbcb = mdbc.GetData(sql_cb);
            if (dbcb.Rows.Count != 0)
            {
                ncu_line = dbcb.Rows[0][0].ToString();
            }
        }



        // Kiểm tra tính hợp lệ của thông tin
        bool next = true;

        if (nhacungung == null)
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Không có nhà cung ứng mặc định");
        }

        if (donhang.md_trangthai_id.Equals("HIEULUC"))
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Không thể thêm mới dòng hàng khi PO đã hiệu lực!");
        }

        if (donhang.md_trangthai_id.Equals("KETTHUC"))
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Không thể thêm mới dòng hàng khi PO đã kết thúc!");
        }

        if (sanpham == null)
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Không có thông tin của sản phẩm.!");
        }

        string phi = context.Request.Form["phi"];
        var phiVal = decimal.Parse(string.IsNullOrEmpty(phi) ? "0" : phi);

        if (donhang.isdonhangtmp != null)
        {
            if (donhang.isdonhangtmp.Value)
            {
                if (!sanpham.trangthai.Equals("HHT"))
                {
                    next = false;
                    jqGridHelper.Utils.writeResult(0, "Mã sản phẩm phải là tạm.!");
                }
            }
        }

        if (donggoi == null)
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Không có thông tin của đóng gói.!");
        }

        if (gia_sp == 0)
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Giá nằm trong phiên bản giá đã hết hạn.!");
        }
        else if (gia_sp == -1)
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Không tìm thấy giá.!");
        }

        //Nhan edit
        var check_cbien = db.md_cangxuathangs.Where(hdq => hdq.md_sanpham_id.Equals(md_sanpham_id) && hdq.md_cangbien_id.Equals(donhang.md_cangbien_id)).Count();

        //var check_cbien = db.md_sanphams.Where(s=>s.md_cangbien_id.Equals(donhang.md_cangbien_id) & s.md_sanpham_id == md_sanpham_id).Count();

        if (check_cbien <= 0 && donhang.donhang_mau.Value.Equals(false))
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Mã hàng này không thuộc cảng biển được chọn trên PO!");
        }

        if (count_ > 0)
        {
            next = false;
            jqGridHelper.Utils.writeResult(0, "Mã hàng đã tồn tại trong PO đang chọn!");
        }

        if (next)
        {
            c_dongdonhang mnu = new c_dongdonhang();

            mnu.c_dongdonhang_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss"));
            mnu.c_donhang_id = c_donhang_id;
            mnu.md_sanpham_id = md_sanpham_id;
            mnu.ma_sanpham_khach = context.Request.Form["ma_sanpham_khach"];
            mnu.sothutu = maxLine + 10;
            mnu.giafob = gia_sp;
            mnu.giachuan = null;
            mnu.discount = 0;
            mnu.editphi = (context.Request.Form["editphi"] + "").ToLower() == "true";

            if (donhang.discount_hehang.GetValueOrDefault(false) == true)
            {
                mnu = Public.DiscountHehangValue(donhang.discount_hehang_value, mnu, db);
            }

            mnu.phi = phiVal;
            mnu.soluong = decimal.Parse(context.Request.Form["soluong"]);
            mnu.soluong_conlai = decimal.Parse(context.Request.Form["soluong"]);
            mnu.chiphidonggoi = decimal.Parse(context.Request.Form["chiphidonggoi"]);
            mnu.soluong_daxuat = 0;
            mnu.soluong_dathang = 0;
            mnu.barcode = barcode;
            mnu.mota_tiengviet = sanpham.mota_tiengviet;
            mnu.mota_tienganh = sanpham.mota_tienganh;
            // mnu.nhacungungid = ncu_line;
            mnu.nhacungungid = context.Request.Form["nhacungungid"];

            // Thành phần đóng gói
            mnu.md_donggoi_id = donggoi.md_donggoi_id;
            mnu.sl_inner = donggoi.sl_inner;
            mnu.l1 = donggoi.l1;
            mnu.w1 = donggoi.w1;
            mnu.h1 = donggoi.h1;
            mnu.sl_outer = donggoi.sl_outer;
            mnu.l2 = donggoi.l2_mix;
            mnu.w2 = donggoi.w2_mix;
            mnu.h2 = donggoi.h2_mix;
            mnu.v2 = donggoi.v2;
            mnu.vd = donggoi.vd;
            mnu.vn = donggoi.vn;
            mnu.vl = donggoi.vl;
            mnu.ghichu_vachngan = donggoi.ghichu_vachngan;
            mnu.sl_cont = donggoi.soluonggoi_ctn;
            mnu.ghichu = context.Request.Form["ghichu"];
            mnu.ghichudonggoi = context.Request.Form["ghichudonggoi"];

            if (count_dq > 0 && count_dqdt == 0 && donggoi.doigia_donggoi == true)
            {
                mnu.trangthai = "DGDGDQ";
            }
            else if (count_dq > 0 && count_dqdt == 0)
            {
                mnu.trangthai = "DQ";
            }
            else if (donggoi.doigia_donggoi == true)
            {
                mnu.trangthai = "DGDG";
            }
            else
            {
                mnu.trangthai = "BT";
            }

            // Thông tin chung
            mnu.ghichudonggoingoai = donggoi.mota;
            mnu.mota = context.Request.Form["mota"];
            mnu.hoatdong = hd;
            mnu.nguoitao = UserUtils.getUser(context);
            mnu.nguoicapnhat = UserUtils.getUser(context);
            mnu.ngaytao = DateTime.Now;
            mnu.ngaycapnhat = DateTime.Now;
            mnu.docquyen = docquyen;
            db.c_dongdonhangs.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
        //}
        //  catch (Exception ex)
        // {
        //     jqGridHelper.Utils.writeResult(0, ex.Message);
        // }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        c_dongdonhang ddh = db.c_dongdonhangs.FirstOrDefault(p => p.c_dongdonhang_id.Equals(id));
        c_donhang dh = db.c_donhangs.FirstOrDefault(c => c.c_donhang_id.Equals(ddh.c_donhang_id));
        bool duocXoa = Public.LaNguoiTao(context, dh.nguoitao, db);

        if (dh.md_trangthai_id.Equals("HIEULUC"))
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa dòng trong PO đã hiệu lực!");
        }
        else if (duocXoa == false)
        {
            jqGridHelper.Utils.writeResult(0, string.Format(@"Chỉ tài khoản {0} hoặc admin mới có thể xóa.!", ddh.nguoitao));
        }
        else
        {
            db.c_dongdonhangs.DeleteOnSubmit(ddh);
            db.SubmitChanges();
            jqGridHelper.Utils.writeResult(1, "Xóa dòng thành công!");
        }
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        //// filter
        string filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            string _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        string poId = context.Request.QueryString["poId"];

        string sqlCount = string.Format(@"
            SELECT COUNT(1) AS count
            FROM
                c_dongdonhang ddh
                left join c_donhang dh on ddh.c_donhang_id = dh.c_donhang_id
                left join md_sanpham sp on ddh.md_sanpham_id = sp.md_sanpham_id
                left join md_kieudang kd on kd.md_kieudang_id = sp.md_kieudang_id
                left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id
                left join md_donggoi dg on ddh.md_donggoi_id = dg.md_donggoi_id
                left join md_doitackinhdoanh dtkd on ddh.nhacungungid = dtkd.md_doitackinhdoanh_id
                left join md_danhmuchanghoa tt on sp.md_tinhtranghanghoa_id = tt.md_danhmuchanghoa_id
            WHERE 
                dh.c_donhang_id = N'{0}' {1}",
            poId != null ? poId : "0",
            filter
        );

        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        string sidx = context.Request.QueryString["sidx"];
        string sord = context.Request.QueryString["sord"];
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
            sidx = "ddh.c_dongdonhang_id";
        }

        string strsql = @"
                select 
                    P.* 
                    , (P.giafob + P.phi) - 
                    (
                        case 
                            when isnull(P.phanbodiscount, 0) = 1
                            then (P.giafob + P.phi) * P.discount / 100
                            else 0 
                        end
                    )
                    as giahopdong
                from(
                    select
                        ddh.c_dongdonhang_id
                        , dh.c_donhang_id
                        , sp.trangthai
                        , ddh.sothutu
                        , sp.md_sanpham_id
                        , sp.ma_sanpham
                        , ddh.ma_sanpham_khach
                        , ddh.giafob
                        , isnull(ddh.phi, 0) as phi
                        , ddh.chiphidonggoi
                        , ddh.soluong * isnull(ddh.chiphidonggoi, 0) as cpdg_vuotchuan
                        , (case when isnull(dh.discount, 0) > 0 then isnull(dh.discount, 0) else isnull(ddh.discount, 0) end) as discount
                        , dh.phanbodiscount
                        , ddh.soluong
                        , dg.ten_donggoi
                        , ddh.id_donggoi_outer
                        , ddh.sl_inner
                        , ddh.l1, ddh.w1, ddh.h1,  ddh.sl_outer, ddh.l2, ddh.w2, ddh.h2
                        , ddh.sl_cont, ddh.v2, ddh.ghichudonggoi, dtkd.ma_dtkd, ddh.sl_outer2, dvto.ten_dvt, ddh.ngaytao, ddh.nguoitao
                        , ddh.ngaycapnhat,  ddh.nguoicapnhat, ddh.mota
                        , ddh.hoatdong ,ddh.trangthai as trangthai_ddh, ddh.docquyen, ddh.barcode, ddh.editphi, ddh.ghichudonggoingoai, tt.ten_danhmuc
                        , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            @" FROM
                c_dongdonhang ddh
                left join c_donhang dh on ddh.c_donhang_id = dh.c_donhang_id
                left join md_sanpham sp on ddh.md_sanpham_id = sp.md_sanpham_id
                left join md_kieudang kd on kd.md_kieudang_id = sp.md_kieudang_id
                left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id
                left join md_donggoi dg on ddh.md_donggoi_id = dg.md_donggoi_id
                left join md_donvitinh dvto on ddh.dvt_outer = dvto.md_donvitinh_id
                left join md_doitackinhdoanh dtkd on ddh.nhacungungid = dtkd.md_doitackinhdoanh_id
                left join md_danhmuchanghoa tt on sp.md_tinhtranghanghoa_id = tt.md_danhmuchanghoa_id
            WHERE
                dh.c_donhang_id = N'{0}' " + filter +
            @" )P
            WHERE RowNum > @start AND RowNum < @end";

        if (poId != null)
        {
            strsql = string.Format(strsql, poId);
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
            xml += "<cell><![CDATA[" + row["c_dongdonhang_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["c_donhang_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai_ddh"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_sanpham_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sothutu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham_khach"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["giafob"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["discount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["giahopdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chiphidonggoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cpdg_vuotchuan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phanbodiscount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["soluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_donggoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["id_donggoi_outer"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_inner"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["l1"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["w1"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["h1"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_outer"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["l2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["w2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["h2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_cont"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["v2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghichudonggoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_outer2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_dvt"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["docquyen"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["barcode"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["editphi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghichudonggoingoai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_danhmuc"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }

    // public string check_hanghoadocquyen(string md_sanpham_id, string md_doitackinhdoanh_id) {
    // string msg = "";
    // md_hanghoadocquyen hhdq = db.md_hanghoadocquyens.FirstOrDefault(s=>s.md_sanpham_id == md_sanpham_id);
    // if(hhdq != null) {
    // md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(s=>s.md_doitackinhdoanh_id == hhdq.md_doitackinhdoanh_id);
    // if(dtkd != null) {
    // if(dtkd.md_doitackinhdoanh_id != md_doitackinhdoanh_id) {
    // md_quocgia quocgia = db.md_quocgias.FirstOrDefault(s=>s.md_quocgia_id == dtkd.md_quocgia_id);
    // string ten_qg = "";
    // if(quocgia != null) {
    // ten_qg = quocgia.ten_quocgia;
    // }
    // msg = "Mã hàng độc quyền cho khách "+ dtkd.ma_dtkd +" ở thị trường "+ ten_qg +". Bạn có muốn mở bán cho khách hàng của bạn không ? YES/NO";
    // }
    // }
    // }
    // return msg;
    // }

    public bool IsReusable
    {
        get { return false; }
    }
}
