<%@ WebHandler Language="C#" Class="TaoPhieuNhapKho" %>

using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using Newtonsoft.Json.Linq;
using NetServ.Net.Json;
using System.Collections.Generic;

public class TaoPhieuNhapKho : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"].ToString();
        if (action.Equals("TaoPhieuNhap"))
        {
            taoPhieuNhap(context);
        }
    }

    public void taoPhieuNhap(HttpContext context)
    {
        string msg = "";

        try
        {
            LinqDBDataContext db = new LinqDBDataContext();
            string jsondata = context.Request.Form["grid_form"].ToString();
            var dsdh = new c_danhsachdathang();
            string dsdh_id = "", diengiai = "";
            IList<JToken> tokenList = JToken.Parse(jsondata).ToList();

            List<phieuNhapKho> productList = new List<phieuNhapKho>();

            foreach (JToken token in tokenList)
            {
                productList.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<phieuNhapKho>(token.ToString()));
            }

            // Kiểm tra số lượng nhập thực tế
            bool next = true;
            foreach (var item in productList)
            {
                //Nếu lớn hơn số lượng còn lại sẽ không được tiếp tục
                if (item.sl_nhapthucte > item.sl_conlai & item.sl_nhapthucte != 0)
                {
                    msg = "Tạo phiếu nhập thất bại! Hãy kiểm tra lại số lượng nhập thực tế của sản phẩm lớn hơn số lượng còn lại.";
                    next = false;
                    break;
                }
            }

            List<string> c_danhsachdathang_ids = new List<string>();
            foreach (var item in productList)
            {
                c_danhsachdathang_ids.Add(item.c_danhsachdathang_id);
            }

            if (c_danhsachdathang_ids.Distinct().Count() > 1)
            {
                msg = "Tạo phiếu nhập thất bại! Không thể tạo phiếu nhập nhiều hơn một danh sách đặt hàng.";
                next = false;
            }

            // Nếu số lượng thực tế hợp lý
            if (next)
            {
                if (productList.Where(s => s.sl_nhapthucte > 0).Count() <= 0)
                    msg = "Không có dòng hàng nào được tạo";
                else
                {
                    String id = ImportUtils.getNEWID();
                    //tao 1 dong dat hang
                    c_nhapxuat obj = new c_nhapxuat();
                    obj.c_nhapxuat_id = id;
                    int stt = 10;

                    foreach (phieuNhapKho item in productList)
                    {
                        if (item.sl_nhapthucte > 0)
                        {
                            c_dongnhapxuat sobj = new c_dongnhapxuat();
                            obj.c_donhang_id = item.c_donhang_id;
                            sobj.c_dongnhapxuat_id = Security.EncodeMd5Hash(DateTime.Now.ToString() + item.c_dongdonhang_id);
                            sobj.c_nhapxuat_id = id;
                            sobj.c_dongdonhang_id = item.c_dongdonhang_id;
                            sobj.c_dongdsdh_id = item.c_dongdsdh_id;
                            dsdh_id = db.c_dongdsdhs.Single(s => s.c_dongdsdh_id.Equals(item.c_dongdsdh_id)).c_danhsachdathang_id;
                            //sobj. = db.c_donhangs.Single(o=>o.c_donhang_id.Equals(item.c_donhang_id)).sochungtu; 
                            sobj.md_sanpham_id = item.md_sanpham_id;

                            // lấy mô tả của sản phẩm
                            md_sanpham sp = db.md_sanphams.Single(p => p.md_sanpham_id.Equals(item.md_sanpham_id));
                            sobj.mota_tiengviet = sp.mota_tiengviet;

                            sobj.md_donvitinh_id = db.md_sanphams.Single(s => s.md_sanpham_id.Equals(item.md_sanpham_id)).md_donvitinhsanpham_id;
                            sobj.slphai_nhapxuat = item.sl_po;
                            sobj.slthuc_nhapxuat = item.sl_nhapthucte;
                            sobj.sl_dathang = item.sl_dathang;
                            sobj.dongia = db.c_dongdsdhs.Single(ddh => ddh.c_dongdsdh_id.Equals(item.c_dongdsdh_id)).gianhap;

                            sobj.hoatdong = true;
                            sobj.ngaytao = DateTime.Now;
                            sobj.ngaycapnhat = DateTime.Now;
                            sobj.nguoitao = UserUtils.getUser(context);
                            sobj.nguoicapnhat = UserUtils.getUser(context);
                            sobj.line = stt;
                            stt += 10;
                            db.c_dongnhapxuats.InsertOnSubmit(sobj);
                        }
                    }

                    string sqlCount = @"select count(*) from c_nhapxuat where md_loaichungtu_id = 'NK' and c_donhang_id = '" + obj.c_donhang_id +"'";
                    int count = (int)mdbc.ExecuteScalar(sqlCount);
                    if(count == 0)
                    {
                        foreach(c_phidathang pdh in db.c_phidathangs.Where(s=> s.c_danhsachdathang_id == dsdh_id & s.hoatdong == true))
                        {
                            string congtru = "";
                            if(pdh.isphicong == true)
                            {
                                congtru = "+";
                            }
                            else
                            {
                                congtru = "-";
                            }
                            diengiai += "Phí " + congtru + " " + pdh.sotien + " : " + pdh.mota + ";\n ";
                        }
                    }
                    else
                    {
                        diengiai = "";
                    }

                    md_sochungtu chungtu = db.md_sochungtus.Where(ct => ct.ten_sochungtu.Equals("Nhập kho")).FirstOrDefault();
                    var dh = db.c_donhangs.Single(so => so.c_donhang_id.Equals(obj.c_donhang_id));
                    obj.md_doitackinhdoanh_id = db.c_danhsachdathangs.Single(d => d.c_danhsachdathang_id.Equals(dsdh_id)).md_doitackinhdoanh_id;

                    obj.sophieu = chungtu.tiento + "/" + chungtu.so_duocgan + "/" + chungtu.hauto;
                    chungtu.so_duocgan = chungtu.so_duocgan + 1;
                    obj.socontainer = 1;
                    obj.loaicont = dh.loai_cont;

                    md_kho kho = db.md_khos.Where(ct => !ct.md_kho_id.Equals("")).FirstOrDefault();

                    obj.md_kho_id = kho.md_kho_id;
                    obj.ngay_phieu = DateTime.Now;
                    obj.ngay_giaonhan = DateTime.Now;
                    obj.cr_invoice = false;
                    obj.cr_phieuxuat = false;
                    obj.mota = diengiai;
                    obj.nguoitao = UserUtils.getUser(context);
                    obj.nguoicapnhat = UserUtils.getUser(context);

                    obj.ngaytao = DateTime.Now;
                    obj.ngaycapnhat = DateTime.Now;
                    obj.hoatdong = true;
                    obj.md_loaichungtu_id = "NK";
                    obj.md_trangthai_id = "SOANTHAO";

                    db.c_nhapxuats.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    String update = "update c_dongnhapxuat set hoatdong = 1 where c_nhapxuat_id = @id";
                    mdbc.ExcuteNonQuery(update, "@id", id);
                    context.Response.Write("Tạo phiếu xác nhận thành công!" + obj.sophieu);
                }
            }
            else {

            }
        }
        catch (Exception e)
        {
           msg = e.Message;
        }

        context.Response.Write(msg);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}