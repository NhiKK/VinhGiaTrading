using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

public static class UserUtils
{
    public static String getUser(HttpContext context)
    {
        String ur = context.User.Identity.Name;
        return ur;
    }

    public static nhanvien getUser(LinqDBDataContext db, HttpContext context)
    {
        String ur = context.User.Identity.Name;
        return db.nhanviens.Where(s => s.manv == ur).FirstOrDefault();
    }

    //Mail KCS
    public static string mailKCS = "";
    //Mail Ke Toan NCU
    public static string mailKeToanNCU = "ketoanTEST@ancometal.com";

    public static decimal get_gianhap(HttpContext context, string ma_dtkd_select, decimal gia_fob, decimal discount, decimal discount_vinhgia, LinqDBDataContext db)
    {
        //Tinh % theo nha cung ung

        int getptr = 0;
        //lay % gia mua
        if (gia_fob == -1 & discount == -1 & discount_vinhgia == -1)
            getptr = 1;
        //lay ty gia mua
        else if (gia_fob == -2 & discount == -2 & discount_vinhgia == -2)
            getptr = 2;

        decimal gianhap = -1;
        //Neu doi tac la ANBINH, ANCO2, NAMAN
        if (new string[] { "ANBINH", "ANCO2", "NAMAN" }.Contains(ma_dtkd_select))
        {
            decimal tygia = 23000;
            decimal discountVNN = decimal.Parse("0.80");
            //Lay discount dac biet
            if (getptr == 1)
            {
                gianhap = discountVNN;
            }
            //Lay ty gia
            else if (getptr == 2)
            {
                gianhap = tygia;
            }
            //Lay gia mua từ NCC
            else
            {
                gianhap = -1;
            }
        }
        //Neu doi tac là ANCO1
        else if (new string[] { "ANCO1" }.Contains(ma_dtkd_select))
        {
            decimal tygia = 23000;
            var tyGiaMua = db.rpt_tonghopdoanhthuchiphis.Where(s => s.xuongValue == ma_dtkd_select & s.nam == DateTime.Now.Year).FirstOrDefault();
            if (tyGiaMua != null)
            {
                tygia = tyGiaMua.tyGiaMua.GetValueOrDefault(0) > 0 ? tyGiaMua.tyGiaMua.GetValueOrDefault(0) : tygia;
            }

            decimal discountVNN = decimal.Parse("0.80");
            //Lay discount dac biet
            if (getptr == 1)
            {
                gianhap = discountVNN;
            }
            //Lay ty gia
            else if (getptr == 2)
            {
                gianhap = tygia;
            }
            //Lay gia mua từ NCC
            else
            {
                //Công thức: giá mua = (FOB –Discount)*0.8*23.000 
                gianhap = (gia_fob - (gia_fob * discount) / 100) * discountVNN * tygia;
            }
        }
        //Neu doi tac la VINHGIA
        else if (Helper.nhaCungCaps.Contains(ma_dtkd_select))
        {
            decimal tygia = 1;
            //Lay discount dac biet
            if (getptr == 1)
            {
                gianhap = decimal.Parse("0.82");
            }
            //Lay ty gia
            else if (getptr == 2)
            {
                gianhap = tygia;
            }
            //Lay gia mua từ NCC
            else
            {
                //Công thức: giá mua = FOB
                gianhap = gia_fob;
            }
        }
        //Neu doi tac la TITAN
        else if (ma_dtkd_select == "TITAN")
        {
            decimal tygia = 22200;
            decimal discountVNN = decimal.Parse("0.95");
            //Lay discount dac biet
            if (getptr == 1)
            {
                gianhap = discountVNN;
            }
            //Lay ty gia
            else if (getptr == 2)
            {
                gianhap = tygia;
            }
            //Lay gia mua từ NCC
            else
            {
                //Công thức: giá mua = FOB
                gianhap = gia_fob - (gia_fob * discount) / 100 * discountVNN * tygia;
            }
        }
        return gianhap;
    }

    public static Dictionary<string, object> get_giasanpham(HttpContext context, string c_baogia_id, string md_sanpham_id, LinqDBDataContext db)
    {
        var itemJS = new Dictionary<string, object>();
        itemJS["gia"] = "-1";
        itemJS["barcode"] = "";
        itemJS["phi"] = "";
        itemJS["ma_donggoi"] = "";
        itemJS["ma_khach"] = "";

        var baogia = db.c_baogias.Where(d => d.c_baogia_id.Equals(c_baogia_id)).
            Select(s => new { s.md_doitackinhdoanh_id, s.phienbangiacu, s.chkPBGCu, s.gia_db, ngaybaogia = s.ngaybaogia }).FirstOrDefault();
        if (baogia == null)
        {
            baogia = db.c_donhangs.Where(d => d.c_donhang_id.Equals(c_baogia_id)).
            Select(s => new { s.md_doitackinhdoanh_id, s.phienbangiacu, s.chkPBGCu, s.gia_db, ngaybaogia = s.ngaylap }).FirstOrDefault(); ;
        }
        var sp = db.md_sanphams.FirstOrDefault(s => s.md_sanpham_id.Equals(md_sanpham_id));
        if (baogia != null)
        {
            if (sp != null)
            {
                //Lấy theo phiên bảng giá cũ
                if (!String.IsNullOrEmpty(baogia.phienbangiacu) & baogia.chkPBGCu == true)
                {
                    md_phienbangia pbg = db.md_phienbangias.Where(s => s.md_phienbangia_id == baogia.phienbangiacu).FirstOrDefault();
                    md_giasanpham gsp = db.md_giasanphams.Where(s => s.md_phienbangia_id == pbg.md_phienbangia_id & s.md_sanpham_id == md_sanpham_id).FirstOrDefault();
                    decimal gia_sp = -1;
                    if (gsp != null)
                    {
                        gia_sp = gsp.gia.GetValueOrDefault(-1);
                        itemJS["ma_khach"] = gsp.mota;
                    }
                    itemJS["gia"] = gia_sp + "";
                    itemJS["barcode"] = "";
                    itemJS["ma_donggoi"] = "";
                }
                //Lấy theo bảng giá đặc biệt
                else if (baogia.gia_db == true)
                {
                    string md_doitackinhdoanh_id = baogia.md_doitackinhdoanh_id;
                    var items = mdbc.GetDataFromProcedure("getGiaMuaSp_Dacbiet", "@md_sanpham_id", md_sanpham_id, "@md_doitackinhdoanh_id", md_doitackinhdoanh_id, "@ngaytinhgia", baogia.ngaybaogia.Value.ToString("yyyy-MM-dd"));
                    itemJS["gia"] = items.Rows[0]["gia"].ToString();
                    itemJS["barcode"] = items.Rows[0]["barcode"].ToString();
                    itemJS["phi"] = items.Rows[0]["phi"].ToString();
                    itemJS["ma_donggoi"] = items.Rows[0]["ma_donggoi"].ToString();
                    itemJS["ma_khach"] = items.Rows[0]["ma_khach"].ToString();
                }
                //Lấy theo giá chuẩn
                else
                {
                    decimal gia_sp = (decimal)mdbc.ExecuteScalarProcedure("getGiaMuaSp", "@md_sanpham_id", md_sanpham_id, "@md_doitackinhdoanh_id", baogia.md_doitackinhdoanh_id, "@loai_bg", true, "@ngaytinhgia", baogia.ngaybaogia.Value.ToString("yyyy-MM-dd"));
                    itemJS["gia"] = gia_sp + "";
                    itemJS["barcode"] = "";
                    itemJS["phi"] = 0 + "";
                    itemJS["ma_donggoi"] = "";
                    itemJS["ma_khach"] = "";
                }
            }
        }

        return itemJS;
    }

    public static string get_sapxep_qodetail(HttpContext context, string c_baogia_id, string spstr, string default_od, LinqDBDataContext db)
    {
        string orderby = "";
        bool ghepbo = false;
        c_baogia bg = db.c_baogias.FirstOrDefault(s => s.c_baogia_id == c_baogia_id);
        if (bg != null)
        {
            if (bg.ghepbo == true)
            {
                ghepbo = true;
            }
        }

        if (ghepbo == true)
        {
            orderby = "order by substring(" + spstr + ",0, 8) asc, substring(" + spstr + ",13, 2) asc, substring(" + spstr + ",10, 2) asc";
        }
        else
        {
            orderby = default_od;
        }
        return orderby;
    }

    public static string[] get_DQ_DG_ALL(HttpContext context, string md_donggoi_id, string md_doitackinhdoanh_id, string md_sanpham_id, LinqDBDataContext db)
    {
        string[] arr_dqdg = new string[2];
        md_donggoi donggoi = db.md_donggois.FirstOrDefault(p => p.md_donggoi_id.Equals(md_donggoi_id));
        //kiem tra hang hoa doc quyen
        int count_dq = (from hdq in db.md_hanghoadocquyens
                        where !hdq.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(md_sanpham_id)
                        select new { hdq.md_hanghoadocquyen_id }).Count();
        int count_dqdt = (from hdq in db.md_hanghoadocquyens
                          where hdq.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(md_sanpham_id)
                          select new { hdq.md_hanghoadocquyen_id }).Count();

        if (count_dq > 0 && count_dqdt == 0 && donggoi.doigia_donggoi == true)
        {
            arr_dqdg[0] = "DGDGDQ";
            arr_dqdg[1] = "DGDG & DQ";
        }
        else if (count_dq > 0 && count_dqdt == 0)
        {
            arr_dqdg[0] = "DQ";
            arr_dqdg[1] = "DGDG & DQ";
        }
        else if (donggoi.doigia_donggoi == true)
        {
            arr_dqdg[0] = "DGDG";
            arr_dqdg[1] = "";
        }
        else
        {
            arr_dqdg[0] = "BT";
            arr_dqdg[1] = "";
        }
        return arr_dqdg;
    }

    public static string checkHieuLucProduct(HttpContext context, md_sanpham sp, LinqDBDataContext db)
    {
        string msg = "";
        int countDG = (from a in db.md_sanphams
                       join b in db.md_donggoisanphams on a.md_sanpham_id equals b.md_sanpham_id
                       join c in db.md_donggois on b.md_donggoi_id equals c.md_donggoi_id
                       where a.md_sanpham_id == sp.md_sanpham_id
                       select new { a.md_sanpham_id }).Count();

        int countGia = (from a in db.md_banggias
                        join b in db.md_phienbangias on a.md_banggia_id equals b.md_banggia_id
                        join c in db.md_giasanphams on b.md_phienbangia_id equals c.md_phienbangia_id
                        where c.md_sanpham_id == sp.md_sanpham_id
                        select new { c.md_sanpham_id }).Count();


        var path = ExcuteSignalRStatic.getImageProduct(sp.ma_sanpham);
        var HinhAnh = path != Public.imgNotFound;

        if (HinhAnh)
            HinhAnh = System.IO.File.Exists(ExcuteSignalRStatic.mapPathSignalR(string.Format("~/{0}", path)));

        if (countDG <= 0)
            msg += "Thiếu đóng gói,";
        if (countGia <= 0)
            msg += "Thiếu giá,";
        if (HinhAnh == false)
            msg += string.Format(@"Thiếu hình ảnh {0},", path);

        if (!string.IsNullOrEmpty(msg))
            msg = msg.Substring(0, msg.Length - 1);
        return msg;
    }

    public static void SetFormValue(string key, string value)
    {
        var collection = System.Web.HttpContext.Current.Request.Form;

        // Get the "IsReadOnly" protected instance property.
        var propInfo = collection.GetType().GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

        // Mark the collection as NOT "IsReadOnly"
        propInfo.SetValue(collection, false, new object[] { });

        // Change the value of the key.
        collection[key] = value;

        // Mark the collection back as "IsReadOnly"     
        propInfo.SetValue(collection, true, new object[] { });
    }

    public static string createPI(HttpContext context, c_donhang donhang, List<dongDatHang> dsSanPham, LinqDBDataContext db)
    {

        // String selSCT = @"SELECT (sct.tiento + '/' + CAST(sct.so_duocgan as nvarchar(32)) + '/' + hauto) as sochungtu 
        // FROM md_sochungtu sct, md_loaichungtu lct 
        // WHERE sct.md_loaichungtu_id = lct.md_loaichungtu_id AND lct.kieu_doituong = N'PI'";

        // String sochungtu = (String)mdbc.ExecuteScalar(selSCT);

        var sctObject = db.md_sochungtus
            .Join(db.md_loaichungtus, sct => sct.md_loaichungtu_id, lct => lct.md_loaichungtu_id, (sct, lct) => new { sct, lct })
            .Where(s => s.lct.kieu_doituong == "PI" & s.sct.tiento == "DSDH").FirstOrDefault();

        String sochungtu = sctObject == null ? "" : string.Format(@"{0}/{1}/{2}", sctObject.sct.tiento, sctObject.sct.so_duocgan, sctObject.sct.hauto);

        if (db.c_danhsachdathangs.Where(s => s.sochungtu == sochungtu).Take(1).Count() > 0)
        {
            return string.Format(@"Lỗi trùng số chứng từ {0}", sochungtu);
        }
        else
        {

            var dsPartner = dsSanPham.Select(p => p.md_doitackinhdoanh_id).Distinct().ToList();

            String md_doitackinhdoanh_id = (from p in db.md_doitackinhdoanhs
                                            where p.ma_dtkd.Equals(dsPartner[0].ToString())
                                            select p.md_doitackinhdoanh_id).FirstOrDefault();
            md_doitackinhdoanh_id = md_doitackinhdoanh_id == null ? dsPartner[0].ToString() : md_doitackinhdoanh_id;

            string maNCC = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id == md_doitackinhdoanh_id).ma_dtkd;
            var dsdh = db.c_danhsachdathangs.Where(s => s.c_donhang_id == donhang.c_donhang_id & s.md_doitackinhdoanh_id == md_doitackinhdoanh_id).Count();

            if (dsdh > 0)
            {
                return string.Format(@"Lỗi đã tồn tại số đơn hàng {0} trong danh sách đặt hàng", donhang.sochungtu);
            }
            else
            {
                String c_dondathang_id = ImportUtils.getNEWID();

                var dondathang = new c_danhsachdathang();
                if (Helper.nhaCungCaps.Contains(maNCC))
                {
                    //discount_vinhgia = donhang.discount.Value;
                    dondathang.discount = donhang.discount.GetValueOrDefault(0);
                }
                else if (new string[] { "ANCO2", "ANBINH" }.Contains(maNCC))
                {
                    if (donhang.donhang_mau.GetValueOrDefault(false) == true)
                    {
                        if (donhang.discount.GetValueOrDefault(0) == 100)
                            dondathang.discount = donhang.discount;
                    }
                }

                dondathang.c_danhsachdathang_id = c_dondathang_id;
                dondathang.sochungtu = sochungtu;
                dondathang.so_po = donhang.sochungtu;
                dondathang.c_donhang_id = donhang.c_donhang_id;
                dondathang.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                dondathang.ngaylap = DateTime.Now;
                dondathang.hangiaohang_po = donhang.shipmenttime.Value.AddDays(-7);
                dondathang.nguoi_phutrach = "";
                dondathang.nguoi_dathang = "";
                dondathang.diachigiaohang = md_doitackinhdoanh_id;
                dondathang.md_trangthai_id = "SOANTHAO";
                dondathang.ngaytao = DateTime.Now;
                dondathang.ngaycapnhat = DateTime.Now;
                dondathang.nguoitao = getUser(context);
                dondathang.nguoicapnhat = getUser(context);
                dondathang.hoatdong = true;
                dondathang.isgui_hdlh = false;
                string mota_diengiai = "";
                foreach (var pdh in db.c_phidonhangs.Where(s => s.c_donhang_id == donhang.c_donhang_id & s.hoatdong == true))
                {
                    var dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id == pdh.md_doitackinhdoanh_id);
                    if (dtkd != null)
                    {
                        if ((bool)pdh.phitang)
                            mota_diengiai += pdh.diengiai_kh + " " + pdh.phi + " USD " + dtkd.ma_dtkd + "\n";
                        else
                            mota_diengiai += pdh.diengiai_kh + " " + pdh.phi + " USD " + dtkd.ma_dtkd + "\n";
                    }
                }
                dondathang.mota = donhang.ghichu + "\n" + mota_diengiai;

                // Lay cac dong don hang
                var lstDongDonDatHang = new List<c_dongdsdh>();
                var lstDongDonHang = dsSanPham;

                string ma_dtkd_select = maNCC;
                foreach (var item in lstDongDonHang)
                {
                    var sp = db.md_sanphams.FirstOrDefault(p => p.md_sanpham_id.Equals(item.sanpham_id));
                    var ddonhang = db.c_dongdonhangs.FirstOrDefault(p => p.c_dongdonhang_id.Equals(item.c_dongdonhang_id));
                    string nhacungung = ddonhang.nhacungungid;
                    //decimal gianhap = (decimal)mdbc.ExecuteScalarProcedure("getGiaMuaSp", "@md_sanpham_id", sp.md_sanpham_id, "@md_doitackinhdoanh_id", nhacungung, "@loai_bg", 0, "@ngaytinhgia", DateTime.Now.ToString("yyyy-MM-dd"));
                    decimal gianhap = (decimal)mdbc.ExecuteScalarProcedure("getGiaStandar", "@md_sanpham_id", sp.md_sanpham_id, "@ngaytinhgia", DateTime.Now.ToString("yyyy-MM-dd"), "@giaban", 1);
                    //decimal giaMua = (decimal)mdbc.ExecuteScalarProcedure("getGiaMuaSp", "@md_sanpham_id", sp.md_sanpham_id, "@md_doitackinhdoanh_id", nhacungung, "@loai_bg", 0, "@ngaytinhgia", DateTime.Now.ToString("yyyy-MM-dd"));
                    decimal giaMua = (decimal)mdbc.ExecuteScalarProcedure("getGiaMuaSp_DB_SD", "@md_sanpham_id", sp.md_sanpham_id, "@md_doitackinhdoanh_id", donhang.md_doitackinhdoanh_id, "@ngaytinhgia", DateTime.Now.ToString("yyyy-MM-dd"));
                    decimal giachuan = gianhap;
                    //Neu doi tac la VINHGIA

                    //decimal gianhap2 = get_gianhap(context, ma_dtkd_select, ddonhang.giafob.Value, 0, 0, db);
                    decimal gianhap2 = get_gianhap(context, ma_dtkd_select, gianhap, 0, 0, db);
                    var giaNhap2BD = gianhap2;
                    decimal tygia = get_gianhap(context, ma_dtkd_select, -2, -2, -2, db);

                    var phiDiscount = ddonhang.phi.GetValueOrDefault(0) * 100 / (100 + (decimal)ddonhang.discount.GetValueOrDefault(0));

                    if (donhang.discount.GetValueOrDefault(0) > 0)
                    {
                        if (dondathang.discount.GetValueOrDefault(0) <= 0)
                            phiDiscount = ddonhang.phi.GetValueOrDefault(0) * 100 / (100 + donhang.discount.GetValueOrDefault(0));
                        else
                            phiDiscount = ddonhang.phi.GetValueOrDefault(0);
                    }

                    gianhap2 = gianhap2 - gianhap2 * getDiscountgPO(donhang, ddonhang) / 100;

                    if (maNCC == "ANCO1" & giachuan > -1)
                        giaNhap2BD = -1;

                    if (giaNhap2BD > -1)
                    {
                        if (Helper.nhaCungCaps.Contains(maNCC))
                        {
                            gianhap = ddonhang.discount.GetValueOrDefault(0) > 0 ? gianhap2 : giaNhap2BD;
                            giachuan = gianhap;
                            gianhap = gianhap + phiDiscount;
                        }
                        else
                        {
                            gianhap = gianhap2;
                            giachuan = jqGridHelper.Utils.Round5(gianhap, -2);
                            gianhap = jqGridHelper.Utils.Round5(gianhap + (phiDiscount * tygia), -2);
                        }
                    }
                    else
                    {
                        gianhap = jqGridHelper.Utils.Round5(gianhap + phiDiscount * tygia, -2);
                    }

                    var sldatVG = ddonhang.soluong - ddonhang.soluong_dathang.GetValueOrDefault(0) - ddonhang.soluongLTHHH;
                    var sldatHHH = ddonhang.soluong - ddonhang.soluong_dathang.GetValueOrDefault(0) - ddonhang.soluongLTVG;

                    if (maNCC == Helper.nhaCungCaps[0] & sldatVG < item.soluong_dadat)
                        return string.Format("Lỗi {0}: Số lượng tối đa có thể đặt VINHGIA là {1}", sp.ma_sanpham, sldatVG.GetValueOrDefault(0).DropTrailingZeros());

                    if (maNCC == Helper.nhaCungCaps[1] & sldatHHH < item.soluong_dadat)
                        return string.Format("Lỗi {0}: Số lượng tối đa có thể đặt HHH là {1}", sp.ma_sanpham, sldatHHH.GetValueOrDefault(0).DropTrailingZeros());

                    var ddsdh = new c_dongdsdh();
                    ddsdh.c_dongdsdh_id = ImportUtils.getNEWID();
                    ddsdh.sothutu = ddonhang.sothutu;
                    ddsdh.c_danhsachdathang_id = dondathang.c_danhsachdathang_id;
                    ddsdh.c_dongdonhang_id = item.c_dongdonhang_id;
                    ddsdh.md_sanpham_id = item.sanpham_id;
                    ddsdh.mota_tiengviet = sp.mota_tiengviet;
                    ddsdh.mota_tienganh = sp.mota_tienganh;
                    ddsdh.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                    ddsdh.huongdan_dathang = ma_dtkd_select;
                    ddsdh.han_giaohang = DateTime.Now;
                    ddsdh.tem_dan = "Tem dáng";
                    ddsdh.sl_dathang = item.soluong_dadat;
                    ddsdh.sl_dagiao = 0;
                    ddsdh.sl_conlai = item.soluong_dadat;
                    //ddsdh.gianhap = gianhap;
                    ddsdh.gianhap = giaMua == -1 ? ddonhang.giafob : giaMua;
                    ddsdh.ma_sanpham_khach = item.ma_sanpham_khach;
                    ddsdh.md_donggoi_id = ddonhang.md_donggoi_id;
                    ddsdh.sl_inner = ddonhang.sl_inner;
                    ddsdh.l1 = ddonhang.l1;
                    ddsdh.w1 = ddonhang.w1;
                    ddsdh.h1 = ddonhang.h1;
                    ddsdh.sl_outer = ddonhang.sl_outer;
                    ddsdh.l2 = ddonhang.l2;
                    ddsdh.w2 = ddonhang.w2;
                    ddsdh.h2 = ddonhang.h2;
                    ddsdh.v2 = ddonhang.sl_outer.Value == 0 ? 0 : Math.Round((((ddonhang.l2.Value * ddonhang.h2.Value * ddonhang.w2.Value) * item.soluong_dadat / ddonhang.sl_outer.Value) / 1000000), 3);
                    ddsdh.sl_cont = ddonhang.sl_cont;
                    ddsdh.vd = ddonhang.vd;
                    ddsdh.vn = ddonhang.vn;
                    ddsdh.vl = ddonhang.vl;
                    ddsdh.ghichu_vachngan = ddonhang.ghichu_vachngan;
                    ddsdh.ngaytao = DateTime.Now;
                    ddsdh.ngaycapnhat = DateTime.Now;
                    ddsdh.nguoitao = getUser(context);
                    ddsdh.nguoicapnhat = getUser(context);
                    ddsdh.hoatdong = true;
                    //ddsdh.giachuan = giachuan;
                    ddsdh.giachuan = giaMua == -1 ? ddonhang.giafob : giaMua;

                    lstDongDonDatHang.Add(ddsdh);
                }

                foreach (var pdonh in db.c_phidonhangs.Where(s => s.c_donhang_id == donhang.c_donhang_id & s.hoatdong == true))
                {
                    if (pdonh.tracho_ncc.GetValueOrDefault(false) == true)
                    {
                        #region Chuyen theo % CBM
                        if (pdonh.check_ptrCBM == true)
                        {
                            // tinh CBM
                            System.Data.DataTable dt = mdbc.GetDataFromProcedure("tinhNangLucCungUng_group", "@donhang_id", donhang.c_donhang_id);
                            int rowCount = dt.Rows.Count;
                            string nangluc = "";
                            if (rowCount > 0)
                            {
                                foreach (System.Data.DataRow row in dt.Rows)
                                {
                                    if (ma_dtkd_select == row["ma_dtkd"].ToString())
                                        nangluc += row["ma_dtkd"] + ":" + row["cbm"] + ",";
                                }
                            }

                            decimal cbm = 0;
                            string[] nangluc_split = nangluc.Split(',');
                            for (int i = 0; i < nangluc_split.Count(); i++)
                            {
                                string[] cbm_ncc = nangluc_split[i].Split(':');
                                try
                                {
                                    cbm = decimal.Parse(cbm_ncc[1]);
                                    decimal vnn = Math.Round((cbm * 100) / donhang.totalcbm.Value, 0);
                                    decimal vnn1 = Math.Round(pdonh.phi.Value * vnn / 100, 0);

                                    decimal get_giaPB = get_gianhap(context, cbm_ncc[0], -2, -2, -2, db);
                                    decimal tienVND = 0;
                                    if (get_giaPB > 1)
                                    {
                                        tienVND = jqGridHelper.Utils.Round5(vnn1 * get_giaPB, -2);
                                    }
                                    else
                                    {
                                        tienVND = vnn1;
                                    }

                                    if (tienVND > 0)
                                    {
                                        var pdath = new c_phidathang
                                        {
                                            c_phidathang_id = ImportUtils.getNEWID(),
                                            c_danhsachdathang_id = c_dondathang_id,
                                            sotien = tienVND,
                                            isphicong = pdonh.phitang,

                                            mota = pdonh.diengiai_kh + "\n",
                                            hoatdong = true,
                                            ngaytao = DateTime.Now,
                                            ngaycapnhat = DateTime.Now,
                                            nguoitao = getUser(context),
                                            nguoicapnhat = getUser(context)
                                        };
                                        db.c_phidathangs.InsertOnSubmit(pdath);
                                    }
                                }
                                catch { }
                            }
                        }
                        #endregion

                        #region Chuyen het NCC
                        else if (pdonh.check_chhet == true)
                        {
                            var d = JsonConvert.DeserializeObject<List<dsncc_ptr>>(pdonh.dsncc_check_chhet);
                            try
                            {
                                dondathang.mota = pdonh.mota + ": " + pdonh.phi.Value + " USD.";
                                foreach (var row in d)
                                {
                                    dondathang.mota += "\n" + row.ma_dtkd + ":" + row.tien.ToString() + " USD.";
                                    if (md_doitackinhdoanh_id == row.id.ToString())
                                    {
                                        decimal tien = decimal.Parse((row.tien.ToString()).ToString());
                                        decimal get_giaPB = UserUtils.get_gianhap(context, row.ma_dtkd, -2, -2, -2, db);
                                        decimal tienVND = 0;
                                        if (get_giaPB > 1)
                                        {
                                            tienVND = jqGridHelper.Utils.Round5(tien * get_giaPB, -2); ;
                                        }
                                        else
                                        {
                                            tienVND = tien;
                                        }
                                        if (tienVND > 0)
                                        {
                                            string mota_dsdh = "";
                                            if (pdonh.tracho_ncc == true)
                                            {
                                                mota_dsdh = pdonh.diengiai_kh;
                                            }
                                            else
                                            {
                                                mota_dsdh = pdonh.mota;
                                            }

                                            var pdath = new c_phidathang
                                            {
                                                c_phidathang_id = ImportUtils.getNEWID(),
                                                c_danhsachdathang_id = c_dondathang_id,
                                                sotien = tienVND,
                                                isphicong = pdonh.phitang,

                                                mota = mota_dsdh,
                                                hoatdong = true,
                                                ngaytao = DateTime.Now,
                                                ngaycapnhat = DateTime.Now,
                                                nguoitao = getUser(context),
                                                nguoicapnhat = getUser(context)
                                            };
                                            db.c_phidathangs.InsertOnSubmit(pdath);
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                        #endregion

                        #region Chuyen theo cong thuc gia mua NCC
                        else if (pdonh.check_ptr == true)
                        {
                            var d = JsonConvert.DeserializeObject<List<dsncc_ptr>>(pdonh.dsncc_check_ptr);
                            foreach (var row in d)
                            {
                                if (md_doitackinhdoanh_id == row.id.ToString())
                                {
                                    decimal tien = decimal.Parse((row.tien.ToString()).ToString());
                                    decimal get_giaPB = get_gianhap(context, row.ma_dtkd, tien, 0, 0, db);
                                    decimal tienVND = 0;
                                    if (get_giaPB > 0)
                                    {
                                        tienVND = get_giaPB;
                                    }
                                    else
                                    {
                                        tienVND = tien;
                                    }


                                    if (tienVND > 0)
                                    {
                                        string mota_dsdh = "";
                                        if (pdonh.tracho_ncc == true)
                                        {
                                            mota_dsdh = pdonh.diengiai_kh;
                                        }
                                        else
                                        {
                                            mota_dsdh = pdonh.mota;
                                        }

                                        c_phidathang pdath = new c_phidathang
                                        {
                                            c_phidathang_id = ImportUtils.getNEWID(),
                                            c_danhsachdathang_id = c_dondathang_id,
                                            sotien = tienVND,
                                            isphicong = pdonh.phitang,

                                            mota = mota_dsdh,
                                            hoatdong = true,
                                            ngaytao = DateTime.Now,
                                            ngaycapnhat = DateTime.Now,
                                            nguoitao = getUser(context),
                                            nguoicapnhat = getUser(context)
                                        };
                                        db.c_phidathangs.InsertOnSubmit(pdath);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Chuyen Binh Thuong
                        else
                        {
                            string mota_dsdh = "";
                            if (pdonh.tracho_ncc == true)
                            {
                                mota_dsdh = pdonh.diengiai_kh;
                            }
                            else
                            {
                                mota_dsdh = pdonh.mota;
                            }

                            var pdath = new c_phidathang
                            {
                                c_phidathang_id = ImportUtils.getNEWID(),
                                c_danhsachdathang_id = c_dondathang_id,
                                sotien = pdonh.phi,
                                isphicong = pdonh.phitang,

                                mota = mota_dsdh,
                                hoatdong = true,
                                ngaytao = DateTime.Now,
                                ngaycapnhat = DateTime.Now,
                                nguoitao = getUser(context),
                                nguoicapnhat = getUser(context)
                            };
                            db.c_phidathangs.InsertOnSubmit(pdath);
                        }
                        #endregion
                    }
                    else if (Helper.nhaCungCaps.Contains(ma_dtkd_select))
                    {
                        #region Phi khach hang chiu
                        var pdath = new c_phidathang
                        {
                            c_phidathang_id = ImportUtils.getNEWID(),
                            c_danhsachdathang_id = c_dondathang_id,
                            sotien = pdonh.phi,
                            isphicong = pdonh.phitang,

                            mota = pdonh.mota,
                            hoatdong = true,
                            ngaytao = DateTime.Now,
                            ngaycapnhat = DateTime.Now,
                            nguoitao = getUser(context),
                            nguoicapnhat = getUser(context)
                        };
                        db.c_phidathangs.InsertOnSubmit(pdath);
                        #endregion
                    }
                }

                donhang.ismakepi = false;
                if (dondathang.discount == null)
                    dondathang.discount = 0;

                //dondathang.mota = donhang.ghichu + "\n" + dondathang.mota;
                db.c_danhsachdathangs.InsertOnSubmit(dondathang);
                db.c_dongdsdhs.InsertAllOnSubmit(lstDongDonDatHang);

                sctObject.sct.so_duocgan = sctObject.sct.so_duocgan.GetValueOrDefault(0) + 1;

                db.SubmitChanges();


                return string.Format("Đã tạo đơn đặt hàng thành công với số chứng từ {0}.!", sochungtu);
            }
        }
    }

    public static decimal getGiaHopDongPO(c_donhang dh, c_dongdonhang ddh)
    {
        var giaFOBvaPhi = ddh.giafob.GetValueOrDefault(0) + ddh.phi.GetValueOrDefault(0);
        var discount = dh.discount.GetValueOrDefault(0) > 0 ? dh.discount.GetValueOrDefault(0) : (decimal)ddh.discount.GetValueOrDefault(0);
        var phanBoDiscountVaoGia = dh.phanbodiscount.GetValueOrDefault(false) ? giaFOBvaPhi * discount / 100 : 0;
        var giaHopDong = giaFOBvaPhi - phanBoDiscountVaoGia;
        return Math.Round(giaHopDong, 3);
    }

    public static decimal getGiaHopDongPO(c_donhang dh, c_dongdonhang ddh, int? round, bool? pbds)
    {
        pbds = pbds == null ? dh.phanbodiscount.GetValueOrDefault(false) : pbds.GetValueOrDefault(false);
        var giaFOBvaPhi = ddh.giafob.GetValueOrDefault(0) + ddh.phi.GetValueOrDefault(0);
        var discount = dh.discount.GetValueOrDefault(0) > 0 ? dh.discount.GetValueOrDefault(0) : (decimal)ddh.discount.GetValueOrDefault(0);
        var phanBoDiscountVaoGia = pbds.GetValueOrDefault(false) ? giaFOBvaPhi * discount / 100 : 0;
        var giaHopDong = giaFOBvaPhi - phanBoDiscountVaoGia;
        return round == null ? giaHopDong : Math.Round(giaHopDong, round.GetValueOrDefault(0), MidpointRounding.AwayFromZero);
    }

    public static decimal getDiscountgPO(c_donhang dh, c_dongdonhang ddh)
    {
        decimal discount = 0;
        if (ddh == null)
        {
            discount = dh.phanbodiscount.GetValueOrDefault(false) ? 0 : dh.discount.GetValueOrDefault(0);
        }
        else
        {
            discount = dh.phanbodiscount.GetValueOrDefault(false) ? 0 : (dh.discount.GetValueOrDefault(0) <= 0 ? (decimal)ddh.discount.GetValueOrDefault(0) : dh.discount.GetValueOrDefault(0));
        }
        return discount;
    }

    public static decimal getDiscountgPO(c_donhang dh, c_dongdonhang ddh, int type)
    {

        decimal discount = 0;
        if (type == 0)
        {
            discount = dh.phanbodiscount.GetValueOrDefault(false) ? 0 : dh.discount.GetValueOrDefault(0);
        }
        else if (type == 1)
        {
            discount = dh.phanbodiscount.GetValueOrDefault(false) ? 0 : (decimal)ddh.discount.GetValueOrDefault(0);
        }
        return discount;
    }

    private class dsncc_ptr
    {
        public string id;
        public string ma_dtkd;
        public string tien;
    }

    public static String WriteLog(LinqDBDataContext db, HttpContext context, string manv, DateTime ngay, string module, string sochungtu)
    {
        var log = new ad_log();
        log.ad_log_id = ImportUtils.getNEWID();
        log.manv = manv;
        log.ngay = ngay;
        log.module = module;
        log.sochungtu = sochungtu;
        //
        log.ngaytao = DateTime.Now;
        log.ngaycapnhat = DateTime.Now;
        log.nguoitao = UserUtils.getUser(context);
        log.nguoicapnhat = UserUtils.getUser(context);
        log.mota = "";
        db.ad_logs.InsertOnSubmit(log);
        db.SubmitChanges();
        return "";
    }
}
