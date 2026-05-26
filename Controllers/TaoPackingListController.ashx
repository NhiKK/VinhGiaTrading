<%@ WebHandler Language="C#" Class="TaoPackingListController" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Data;

public class TaoPackingListController : IHttpHandler
{
    LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            String txtSoPackingList = context.Request.Form["p[txtSoPackingList]"];
            String arrayPhieuXuat = context.Request.Form["p[arrayPhieuXuat]"].ToString();
            String txtSoInvoice = context.Request.Form["p[txtSoPackingList]"];
            String txtNgayLap = context.Request.Form["p[txtNgayLap]"];
            //String selNoiDi = context.Request.Form["p[selNoiDi]"];
            String selNoiDen = context.Request.Form["p[txtNoiDen]"];
            String txtDienGiaiCongThem = context.Request.Form["p[txtDienGiaiCongThem]"];
            String txtGiaTriCongThem = context.Request.Form["p[txtGiaTriCongThem]"];
            String txtDienGiaiTruLai = context.Request.Form["p[txtDienGiaiTruLai]"];
            String txtGiaTriTruLai = context.Request.Form["p[txtGiaTriTruLai]"];
            String txtNgayVanDon = context.Request.Form["p[txtNgayVanDon]"];
            String phieuxuatkho = "'" + arrayPhieuXuat.Replace(",", "','").Replace(" ", "") + "'";
            String txtSoTau = context.Request.Form["p[txtSoTau]"];
            String selHangCua = context.Request.Form["p[selHangCua]"];
            String selLoaiHang = context.Request.Form["p[selLoaiHang]"];
            String selHopDongUyThac = context.Request.Form["p[selHopDongUyThac]"];
            decimal totaldiscount = 0;
            decimal totalnet = 0;

            var lst = new List<c_dongpklinv>();
            String getAllLine = String.Format("SELECT (select c_donhang_id from c_dongdonhang where c_dongdonhang_id = dnx.c_dongdonhang_id) as c_donhang_id, nx.md_doitackinhdoanh_id, dnx.* " +
                " FROM c_nhapxuat nx, c_dongnhapxuat dnx " +
                " WHERE nx.c_nhapxuat_id = dnx.c_nhapxuat_id AND dnx.c_nhapxuat_id IN({0}) ", phieuxuatkho);

            int sothutu = 10;
            DataTable dt = mdbc.GetData(getAllLine);
            String c_dongdonhang_id = dt.Rows[0]["c_dongdonhang_id"].ToString();
            String c_donhang_id = db.c_dongdonhangs.FirstOrDefault(ddh => ddh.c_dongdonhang_id.Equals(c_dongdonhang_id)).c_donhang_id;
            String md_cangbien_id = db.c_donhangs.FirstOrDefault(dh => dh.c_donhang_id.Equals(c_donhang_id)).md_cangbien_id;

            c_packinginvoice p = new c_packinginvoice();
            p.c_packinginvoice_id = Helper.getNewId();
            //---PKL_INV
            txtSoInvoice = jqGridHelper.Utils.taoChungTuINV(db, selHangCua);
            p.so_pkl = txtSoInvoice;
            p.so_inv = txtSoInvoice;
            p.hangcua = selHangCua;
            p.xuatkhau_uythac = selLoaiHang;
            //---
            p.ngaylap = DateTime.ParseExact(txtNgayLap, "dd/MM/yyyy", null);
            p.etd = DateTime.ParseExact(txtNgayVanDon, "dd/MM/yyyy", null);
            p.noiden = selNoiDen;
            p.mv = txtSoTau;

            p.diengiai_cong = txtDienGiaiCongThem;
            p.diengiai_tru = txtDienGiaiTruLai;
            p.giatri_cong = decimal.Parse(txtGiaTriCongThem);
            p.giatri_tru = decimal.Parse(txtGiaTriTruLai);
            p.md_trangthai_id = "SOANTHAO";
            p.thanhtoanxong = false;
            p.discount = UserUtils.getDiscountgPO(db.c_donhangs.FirstOrDefault(dh => dh.c_donhang_id.Equals(c_donhang_id)), null);
            p.totaldis = totalnet * p.discount / 100;
            p.ngay_phaitt = DateTime.Now;

            p.nguoitao = UserUtils.getUser(context);
            p.nguoicapnhat = UserUtils.getUser(context);
            p.ngaytao = DateTime.Now;
            p.ngaycapnhat = DateTime.Now;
            p.hoatdong = true;
            p.noidi = md_cangbien_id;

            var hopdonguythac = "";
            if (selLoaiHang == "Nhận ủy thác")
                hopdonguythac = jqGridHelper.Utils.taoChungTuPLHHH(db, selHangCua);

            p.hopdonguythac = hopdonguythac;

            double discountHH = 0;
            var lstHH = new List<Dictionary<string, object>>();
            foreach (DataRow item in dt.Rows)
            {

                String c_dongnhapxuat_id = item["c_dongnhapxuat_id"].ToString();
                c_dongpklinv line = new c_dongpklinv();
                c_dongnhapxuat dongnx = db.c_dongnhapxuats.Single(dnx => dnx.c_dongnhapxuat_id.Equals(c_dongnhapxuat_id));
                var nx = db.c_nhapxuats.Single(dnx => dnx.c_nhapxuat_id.Equals(dongnx.c_nhapxuat_id));
                var dongdonhang = db.c_dongdonhangs.Single(ddh => ddh.c_dongdonhang_id.Equals(item["c_dongdonhang_id"]));
                var donhang = db.c_donhangs.Single(dh => dh.c_donhang_id.Equals(dongdonhang.c_donhang_id));
                string c_danhsachdathang_id = db.c_dongdsdhs.Where(s => s.c_dongdsdh_id == dongnx.c_dongdsdh_id).Select(s => s.c_danhsachdathang_id).FirstOrDefault();

                line.c_dongpklinv_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddhhmmss") + item["c_dongnhapxuat_id"].ToString());
                line.c_packinginvoice_id = p.c_packinginvoice_id;
                line.c_donhang_id = dongdonhang.c_donhang_id;
                line.c_dongnhapxuat_id = item["c_dongnhapxuat_id"].ToString();
                line.md_doitackinhdoanh_id = donhang.md_doitackinhdoanh_id;
                p.md_doitackinhdoanh_id = donhang.md_doitackinhdoanh_id;
                line.md_sanpham_id = item["md_sanpham_id"].ToString();
                line.mota_tienganh = dongdonhang.mota_tienganh;
                //line.mota_tiengviet = dongdonhang.mota_tiengviet;
                line.ma_sanpham_khach = dongdonhang.ma_sanpham_khach;
                line.soluong = decimal.Parse(item["slthuc_nhapxuat"].ToString());
                line.gia = dongnx.dongia;

                line.thanhtien = dongnx.slthuc_nhapxuat * dongnx.dongia;

                //discountHH += (double)line.thanhtien.GetValueOrDefault(0) * discountHH_DH / 100;

                var discountPO = UserUtils.getDiscountgPO(donhang, null, 0);
                var discountHH_DH = (double)UserUtils.getDiscountgPO(donhang, dongdonhang, 1);

                lstHH.Add(new Dictionary<string, object>() {
                    { "code_cl", dongdonhang.code_cl },
                    { "po", dongdonhang.c_donhang_id },
                    { "discount_hehang", donhang.discount_hehang.GetValueOrDefault(false) },
                    { "thanhtien", line.thanhtien  },
                    { "discountHH", discountHH_DH },
                    { "discountPO", discountPO },
                    { "containerSeal", nx.container + "/" + nx.soseal }
                });

                line.nhacungungid = dongdonhang.nhacungungid;
                line.v2 = dongdonhang.v2;
                line.soluong_ddh = dongdonhang.soluong;
                line.c_nhapxuat_id = dongnx.c_nhapxuat_id;
                line.c_danhsachdathang_id = c_danhsachdathang_id;


                //totaldiscount += Math.Round((line.thanhtien.Value * discountPO) / 100, 2);
                totalnet += line.thanhtien.Value;

                line.line = sothutu;
                line.nguoitao = UserUtils.getUser(context);
                line.nguoicapnhat = UserUtils.getUser(context);
                line.ngaytao = DateTime.Now;
                line.ngaycapnhat = DateTime.Now;
                line.hoatdong = true;
                lst.Add(line);
                sothutu += 10;
            }

            //tao comudity tren packing-list/invoice
            string com_tv = "";
            string com_ta = "";
            string sql = @"select tv_ngan, ta_ngan
                        from md_chungloai where md_chungloai_id
                        in(
	                        select distinct ms.md_chungloai_id
	                        from c_dongnhapxuat cnx, md_sanpham ms 
	                        where ms.md_sanpham_id = cnx.md_sanpham_id and 
		                        c_nhapxuat_id in (" + phieuxuatkho + ")" +
                            ")order by code_cl";
            DataTable com = mdbc.GetData(sql);
            foreach (DataRow item in com.Rows)
            {
                com_tv += item[0].ToString() + ", ";
                com_ta += item[1].ToString() + ", ";
            }

            p.commodityvn = com_tv.Length > 1 ? com_tv.Substring(0, com_tv.Length - 2) : "";
            p.commodity = com_ta.Length > 1 ? com_ta.Substring(0, com_ta.Length - 2) : "";

            // tinh cac khoan phi phat sinh tren cac don hang
            decimal phi_tang = 0;
            decimal phi_giam = 0;
            decimal tiendatcoc = 0;
            //
            decimal cocdatinh = 0;
            decimal sum_cocdatinh = 0;
            string diendai_phitang = "";
            string diendai_phigiam = "";
            int check_phidh = 0;
            decimal cpdg_vuotchuan = 0;
            string sql_getdh = "select distinct c_donhang_id from (" + getAllLine + ") as tmp";
            DataTable dt_dh = mdbc.GetData(sql_getdh);
            string donhang_id = "";
            string consignee = "";
            foreach (DataRow rdh in dt_dh.Rows)
            {
                string cdhID = rdh["c_donhang_id"].ToString();
                donhang_id = cdhID;
                check_phidh = (int)mdbc.ExecuteScalar("select count(1) as count from c_dongpklinv where c_donhang_id ='" + cdhID + "'");
                if (check_phidh == 0)
                {
                    phi_tang += ((decimal)mdbc.ExecuteScalar("select coalesce(sum(phi), 0) from c_phidonhang where phitang = 1 and isnull(hoatdong, 0) = 1 and c_donhang_id ='" + cdhID + "'"));
                    phi_giam += ((decimal)mdbc.ExecuteScalar("select coalesce(sum(phi), 0) from c_phidonhang where phitang = 0 and isnull(hoatdong, 0) = 1 and c_donhang_id ='" + cdhID + "'"));
                    diendai_phitang += ((string)mdbc.ExecuteScalar("select top(1) mota from c_phidonhang where phitang = 1 and isnull(hoatdong, 0) = 1 and c_donhang_id ='" + cdhID + "'"));
                    diendai_phigiam += ((string)mdbc.ExecuteScalar("select top(1) mota from c_phidonhang where phitang = 0 and isnull(hoatdong, 0) = 1 and c_donhang_id ='" + cdhID + "'"));
                    cpdg_vuotchuan += db.c_donhangs.Where(s => s.c_donhang_id == cdhID).Select(s => s.cpdg_vuotchuan).FirstOrDefault().GetValueOrDefault(0);
                }
                if (dt_dh.Rows.Count > 1)
                {
                    if (check_phidh == 0)
                    {
                        //phi_tang += ((decimal)mdbc.ExecuteScalar("select coalesce(sum(phi), 0) from c_phidonhang where phitang = 1 and isnull(hoatdong, 0) = 1 and c_donhang_id ='" + cdhID + "'"));
                        //phi_giam += ((decimal)mdbc.ExecuteScalar("select coalesce(sum(phi), 0) from c_phidonhang where phitang = 0 and isnull(hoatdong, 0) = 1 and c_donhang_id ='" + cdhID + "'"));
                        //diendai_phitang += ((string)mdbc.ExecuteScalar("select top(1) mota from c_phidonhang where phitang = 1 and isnull(hoatdong, 0) = 1 and c_donhang_id ='" + cdhID + "'"));
                        //diendai_phigiam += ((string)mdbc.ExecuteScalar("select top(1) mota from c_phidonhang where phitang = 0 and isnull(hoatdong, 0) = 1 and c_donhang_id ='" + cdhID + "'"));
                        //cpdg_vuotchuan += db.c_donhangs.Where(s => s.c_donhang_id == cdhID).Select(s => s.cpdg_vuotchuan).FirstOrDefault().GetValueOrDefault(0);
                        tiendatcoc += db.c_chitietthuchis.Where(s => s.c_donhang_id == cdhID & s.isdatcoc == true).ToList().Sum(s => s.sotien.GetValueOrDefault(0));
                    }
                }
                else
                {
                    //
                    tiendatcoc += db.c_chitietthuchis.Where(s => s.c_donhang_id == cdhID & s.isdatcoc == true).ToList().Sum(s => s.sotien.GetValueOrDefault(0));
                    cocdatinh = (decimal)mdbc.ExecuteScalar("select isnull(sum(P.tiendatcoc),0) from (select DISTINCT inv.c_packinginvoice_id,inv.tiendatcoc from c_dongpklinv inv_ct left join c_packinginvoice inv on inv.c_packinginvoice_id = inv_ct.c_packinginvoice_id where inv_ct.c_donhang_id ='" + cdhID + "'" + ") as P");
                    sum_cocdatinh += cocdatinh;
                }
                string getConsignee = string.Format("select isnull('|| ' + isnull(mota,''), '') from c_donhang where c_donhang_id ='{0}'", cdhID);
                consignee += (string)mdbc.ExecuteScalar(getConsignee);
            }
            p.consignee = consignee.Substring(2, consignee.Length - 2);
            p.giatricong_po = phi_tang;
            p.giatritru_po = phi_giam;
            p.diengiaicong_po = diendai_phitang;
            p.diengiaitru_po = diendai_phigiam;
            p.cpdg_vuotchuan = cpdg_vuotchuan;
            p.tiendatcoc = tiendatcoc - sum_cocdatinh;

            var lstHHStr = lstHH.Where(s => (bool)s["discount_hehang"]).Select(s => new
            {
                hh = s["code_cl"] + "",
                po = s["po"] + "",
                containerSeal = s["containerSeal"] + ""
            }).Distinct();

            var lstPOStr = lstHH.Where(s => !(bool)s["discount_hehang"]).Select(
                s => new
                {
                    po = s["po"] + "",
                    containerSeal = s["containerSeal"] + ""
                }
            ).Distinct();

            foreach (var hhPO in lstHHStr)
            {
                var rowHHs = lstHH.Where(s => (s["code_cl"] + "") == hhPO.hh & (s["po"] + "") == hhPO.po & (s["containerSeal"] + "") == hhPO.containerSeal);
                var tt = rowHHs.Sum(s => double.Parse(s["thanhtien"] + ""));
                var discount = rowHHs.Select(s => double.Parse(s["discountHH"] + "")).FirstOrDefault();
                totaldiscount = totaldiscount + (decimal)Math.Round(tt * discount / 100, 2);
            }

            foreach (var po in lstPOStr)
            {
                var rowHHs = lstHH.Where(s => (s["po"] + "") == po.po & (s["containerSeal"] + "") == po.containerSeal);
                var tt = rowHHs.Sum(s => double.Parse(s["thanhtien"] + ""));
                var discount = rowHHs.Select(s => double.Parse(s["discountPO"] + "")).FirstOrDefault();
                totaldiscount = totaldiscount + (decimal)Math.Round(tt * discount / 100, 2);
            }

            //totaldiscount = totaldiscount + (decimal)discountHH;
            p.totaldis = totaldiscount;
            p.totalnet = totalnet;
            p.totalgross = p.totalnet - p.totaldis + (p.giatri_cong + p.giatricong_po) - (p.giatri_tru + p.giatritru_po) + p.cpdg_vuotchuan;

            //tinh hoa hong phai tra khi tao invoice
            string sql_hhdh = "select coalesce(hoahong, 0) from c_donhang where c_donhang_id = '" + donhang_id + "'";
            decimal phantramhh = 0;
            try
            {
                phantramhh = (decimal)mdbc.ExecuteScalar(sql_hhdh);
            }
            catch { }

            p.hoahongphaitra = Math.Round((p.totalgross.Value * phantramhh) / 100, 2);
            p.phantramhoahong = phantramhh;
            p.hoahongdatra = 0;

            try
            {
                int check = (from inv in db.c_packinginvoices
                             where inv.so_inv.Equals(txtSoInvoice)
                             select new { inv.so_inv }).Count();
                if (check == 0)
                {
                    db.c_dongpklinvs.InsertAllOnSubmit(lst);
                    db.c_packinginvoices.InsertOnSubmit(p);

                    db.SubmitChanges();

                    var pk = db.c_packinginvoices.FirstOrDefault(pkl => pkl.c_packinginvoice_id.Equals(p.c_packinginvoice_id));
                    pk.hoatdong = true;
                    db.SubmitChanges();

                    context.Response.Write("Tạo " + txtSoInvoice + " thành công.!");
                }
                else
                {
                    context.Response.Write(string.Format(@"Số Packing List/Invoice {0} đã tồn tại trong chương trình.!", txtSoInvoice));
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex + "");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex + "");
        }
    }




    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}