<%@ WebHandler Language="C#" Class="ChiaTachPOController" %>

using System;
using System.Web;
using System.Linq;
using Newtonsoft.Json.Linq;
using NetServ.Net.Json;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ChiaTachPOController : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"].ToString();
        if (action.Equals("SaveDatHang"))
        {
            saveDatHang(context);
        }
    }

    /*public void saveDatHang(HttpContext context)
    {
        try
        {
            String result = "";
            LinqDBDataContext db = new LinqDBDataContext();
            string jsondata = context.Request.Form["grid_form"].ToString();
            IList<JToken> tokenList = JToken.Parse(jsondata).ToList();

            List<dongDatHang> dsSanPham = new List<dongDatHang>();

            foreach (JToken token in tokenList)
            {
                dsSanPham.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dongDatHang>(token.ToString()));
            }

            // Có danh sách sản phẩm mới thực hiện tạo DSDH
            if (dsSanPham.Count > 0)
            {
                // distinct danh sách khách hàng của các sản phẩm 
                var dsPartner = dsSanPham.Select(p => p.md_doitackinhdoanh_id).Distinct().ToList();
                int countPartner = dsPartner.Count();
                c_donhang donhang = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(dsSanPham[0].c_donhang_id));

                if (donhang != null)
                {
                    if (donhang.ismakepi.Equals(true))
                    {
                        result += String.Format("Đơn hàng {0} đã tạo PI từ trước đó.!", donhang.sochungtu);
                    }
                    else if (donhang.md_trangthai_id.Equals("SOANTHAO"))
                    {
                        result += String.Format("Bạn phải chuyển trạng thái PO {0} thành hiệu lực mới tạo được PI.!", donhang.sochungtu);
                    }
                    else if (donhang.md_trangthai_id.Equals("KETTHUC"))
                    {
                        result += String.Format("Không thể tạo đơn đặt hàng vì đơn hàng đã kết thúc.!", donhang.sochungtu);
                    }
                    else if (donhang.md_trangthai_id.Equals("HIEULUC"))
                    {
                        if (countPartner > 1)
                        {
                            result += String.Format("Không thể tạo đơn đặt hàng vì đơn hàng có nhiều hơn một nhà cung ứng.!", donhang.sochungtu);
                        }
                        else
                        {
                            String selSCT = @"SELECT (sct.tiento + '/' + CAST(sct.so_duocgan as nvarchar(32)) + '/' + hauto) as sochungtu 
                          FROM md_sochungtu sct, md_loaichungtu lct 
                          WHERE sct.md_loaichungtu_id = lct.md_loaichungtu_id AND lct.kieu_doituong = N'PI'";

                            String sochungtu = (String)mdbc.ExecuteScalar(selSCT);
                            //mdbc.ExcuteNonProcedure("PoToPi", "@c_donhang_id", poId, "@sochungtu", sochungtu);

                            // lấy nha cung ung
                            String md_doitackinhdoanh_id = (from p in db.md_doitackinhdoanhs 
                                                            where p.ma_dtkd.Equals(dsPartner[0].ToString()) 
                                                            select p.md_doitackinhdoanh_id).FirstOrDefault();

                            String c_dondathang_id = ImportUtils.getNEWID();

                            c_danhsachdathang dondathang = new c_danhsachdathang();
                            dondathang.c_danhsachdathang_id = c_dondathang_id;
                            dondathang.sochungtu = sochungtu;
                            dondathang.so_po = donhang.sochungtu;
                            dondathang.c_donhang_id = donhang.c_donhang_id;
                            dondathang.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                            dondathang.ngaylap = DateTime.Now;
                            dondathang.hangiaohang_po = donhang.shipmenttime;
                            dondathang.nguoi_phutrach = "";
                            dondathang.nguoi_dathang = "";
                            dondathang.md_trangthai_id = "SOANTHAO";
                            dondathang.ngaytao = DateTime.Now;
                            dondathang.ngaycapnhat = DateTime.Now;
                            dondathang.nguoitao = UserUtils.getUser(context);
                            dondathang.nguoicapnhat = UserUtils.getUser(context);
                            dondathang.hoatdong = true;
                            dondathang.isgui_hdlh = false;


                            // Lay cac dong don hang
                            System.Collections.Generic.List<c_dongdsdh> lstDongDonDatHang = new System.Collections.Generic.List<c_dongdsdh>();

                            foreach (var item in dsSanPham)
                            {
                                md_sanpham sp = db.md_sanphams.FirstOrDefault(p => p.md_sanpham_id.Equals(item.sanpham_id));
                                c_dongdonhang ddonhang = db.c_dongdonhangs.FirstOrDefault(p => p.c_dongdonhang_id.Equals(item.c_dongdonhang_id));

                                //String nhacungung = (from p in db.md_sanphams where p.md_sanpham_id.Equals(item.sanpham_id) select p.nhacungung).FirstOrDefault();
                                String nhacungung = ddonhang.nhacungungid;
                                decimal gianhap = (decimal)mdbc.ExecuteScalarProcedure("getGiaMuaSp", "@md_sanpham_id", item.sanpham_id, "@md_doitackinhdoanh_id", nhacungung, "@loai_bg", 0, "@ngaytinhgia", DateTime.Now);

                                c_dongdsdh ddsdh = new c_dongdsdh();
                                ddsdh.c_dongdsdh_id = ImportUtils.getNEWID();
                                ddsdh.sothutu = ddonhang.sothutu;
                                ddsdh.c_danhsachdathang_id = dondathang.c_danhsachdathang_id;
                                ddsdh.c_dongdonhang_id = item.c_dongdonhang_id;
                                ddsdh.md_sanpham_id = item.sanpham_id;
                                ddsdh.mota_tiengviet = sp.mota_tiengviet;
                                ddsdh.mota_tienganh = sp.mota_tienganh;
                                ddsdh.md_doitackinhdoanh_id = ddonhang.nhacungungid;
                                ddsdh.huongdan_dathang = "";
                                ddsdh.han_giaohang = DateTime.Now;
                                ddsdh.tem_dan = "Tem dáng";
                                ddsdh.sl_dathang = item.soluong_dadat;
                                ddsdh.sl_dagiao = 0;
                                ddsdh.sl_conlai = item.soluong_dadat;
                                ddsdh.gianhap = gianhap;
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
                                ddsdh.v2 =  ddonhang.sl_outer.Value == 0 ? 0 : Math.Round((((ddonhang.l2.Value * ddonhang.h2.Value * ddonhang.w2.Value) * item.soluong_dadat / ddonhang.sl_outer.Value) / 1000000), 3);
                                ddsdh.sl_cont = ddonhang.sl_cont;
                                ddsdh.vd = ddonhang.vd;
                                ddsdh.vn = ddonhang.vn;
                                ddsdh.vl = ddonhang.vl;
                                ddsdh.ghichu_vachngan = ddonhang.ghichu_vachngan;
                                ddsdh.ngaytao = DateTime.Now;
                                ddsdh.ngaycapnhat = DateTime.Now;
                                ddsdh.nguoitao = UserUtils.getUser(context);
                                ddsdh.nguoicapnhat = UserUtils.getUser(context);
                                ddsdh.hoatdong = true;

                                lstDongDonDatHang.Add(ddsdh);
                            }

                            donhang.ismakepi = false;
                            db.c_danhsachdathangs.InsertOnSubmit(dondathang);
                            db.c_dongdsdhs.InsertAllOnSubmit(lstDongDonDatHang);
                            db.SubmitChanges();
                            result += String.Format("Đã tạo đơn đặt hàng thành công với số chứng từ {0}.!", sochungtu);
                        }
                    }
                }
                jqGridHelper.Utils.writeResult(1, result);
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Không thể tạo danh sách đặt hàng không có sản phẩm!");
            }
        }
        catch (Exception e)
        {
            jqGridHelper.Utils.writeResult(0, e.Message);
        } 
    }*/

    public void saveDatHang(HttpContext context)
    {
        try
        {
            String result = "";
            LinqDBDataContext db = new LinqDBDataContext();
            string jsondata = context.Request.Form["grid_form"].ToString();
            IList<JToken> tokenList = JToken.Parse(jsondata).ToList();

            List<dongDatHang> dsSanPham = new List<dongDatHang>();

            foreach (JToken token in tokenList)
            {
                var dongDatHang = JsonConvert.DeserializeObject<dongDatHang>(token.ToString());

                if (dongDatHang.soluong_conlai >= dongDatHang.soluong_dadat)
                {
                    dsSanPham.Add(dongDatHang);
                }
                else
                {
                    result += String.Format("{0}: SL đặt tiếp lớn hơn SL còn lại ({1} > {2})breakLine", 
                        dongDatHang.ma_sanpham, 
                        dongDatHang.soluong_dadat.DropTrailingZeros(),
                        dongDatHang.soluong_conlai.DropTrailingZeros());
                }
            }

            // Có danh sách sản phẩm mới thực hiện tạo DSDH
            if (result.Length <= 0)
            {
                if (dsSanPham.Count > 0)
                {
                    // distinct danh sách khách hàng của các sản phẩm 
                    var dsPartner = dsSanPham.Select(p => p.md_doitackinhdoanh_id).Distinct().ToList();
                    int countPartner = dsPartner.Count;
                    var donhang = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(dsSanPham[0].c_donhang_id));

                    if (donhang != null)
                    {
                        if (donhang.ismakepi.Equals(true))
                        {
                            result += String.Format("Đơn hàng {0} đã tạo PI từ trước đó.!", donhang.sochungtu);
                        }
                        else if (donhang.md_trangthai_id.Equals("SOANTHAO"))
                        {
                            result += String.Format("Bạn phải chuyển trạng thái PO {0} thành hiệu lực mới tạo được PI.!", donhang.sochungtu);
                        }
                        else if (donhang.md_trangthai_id.Equals("KETTHUC"))
                        {
                            result += String.Format("Không thể tạo đơn đặt hàng vì đơn hàng đã kết thúc.!", donhang.sochungtu);
                        }
                        else if (donhang.md_trangthai_id.Equals("HIEULUC"))
                        {
                            if (countPartner > 1)
                            {
                                result += String.Format("Không thể tạo đơn đặt hàng vì đơn hàng có nhiều hơn một nhà cung ứng.!", donhang.sochungtu);
                            }
                            else
                            {
                                result += UserUtils.createPI(context, donhang, dsSanPham, db);
                            }
                        }
                    }
                    jqGridHelper.Utils.writeResult(1, result);
                }
                else
                {
                    jqGridHelper.Utils.writeResult(0, "Không thể tạo danh sách đặt hàng không có sản phẩm!");
                }
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, result);
            }
        }
        catch (Exception e)
        {
            jqGridHelper.Utils.writeResult(0, e.Message);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}

public class dsncc_ptr {
    public string id;
    public string ma_dtkd;
    public string tien;
}
