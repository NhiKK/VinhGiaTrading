<%@ WebHandler Language="C#" Class="LamPhieuXacNhanXuat" %>

using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using Newtonsoft.Json.Linq;
using NetServ.Net.Json;
using System.Collections.Generic;

public class LamPhieuXacNhanXuat : IHttpHandler
{
    
   public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"].ToString();
        if (action.Equals("SaveXacNhanXuat"))
        {
            saveXacNhanXuat(context);
        }
    }

   public void saveXacNhanXuat(HttpContext context)
    {
        try
        {
            LinqDBDataContext db = new LinqDBDataContext();
            string jsondata = context.Request.Form["grid_form"].ToString();
            IList<JToken> tokenList = JToken.Parse(jsondata).ToList();

            List<xacNhanXuat> productList = new List<xacNhanXuat>();

            foreach (JToken token in tokenList)
            {
                productList.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<xacNhanXuat>(token.ToString()));
            }

            if (productList.Count > 0)
            {
                //tao 1 dong dat hang
                c_xacnhan_xuatkho obj = new c_xacnhan_xuatkho();
                obj.c_xacnhan_xuatkho_id = Security.EncodeMd5Hash(DateTime.Now.ToString());
                int stt = 10;
                foreach (xacNhanXuat item in productList)
                {
                    c_dongxacnhan_xuatkho sobj = new c_dongxacnhan_xuatkho();
                    obj.c_donhang_id = item.c_donhang_id;
                    sobj.c_dongxacnhan_xuatkho_id = Security.EncodeMd5Hash(DateTime.Now.ToString() + item.c_dongdonhang_id);
                    sobj.c_xacnhan_xuatkho_id = obj.c_xacnhan_xuatkho_id;
                    sobj.c_dongdonhang_id = item.c_dongdonhang_id;
                    //sobj. = db.c_donhangs.Single(o=>o.c_donhang_id.Equals(item.c_donhang_id)).sochungtu; 
                    sobj.md_sanpham_id = item.sanpham_id;
                    sobj.dvt_sanpham = db.md_sanphams.Single(s=>s.md_sanpham_id.Equals(item.sanpham_id)).md_donvitinhsanpham_id;
                    sobj.ma_sanpham_khach = "";
                    sobj.sl_po = item.soluong;
                    sobj.sl_thucxuat = 0;
                    sobj.sl_yeucauxuat = item.soluong_yeucauxuat;
                    sobj.md_donggoi_id = db.md_donggoisanphams.SingleOrDefault(dg=>dg.md_sanpham_id.Equals(item.sanpham_id) && dg.hoatdong.Equals(true)).md_donggoi_id;
                    
                    sobj.hoatdong = true;
                    sobj.ngaytao = DateTime.Now;
                    sobj.ngaycapnhat = DateTime.Now;
                    sobj.nguoitao = "admin";
                    sobj.nguoicapnhat = "admin";

                    db.c_dongxacnhan_xuatkhos.InsertOnSubmit(sobj);
                }
                var chungtu = db.md_sochungtus.Single(ct => ct.md_loaichungtu_id.Equals("cc32c35ee06ef3ac9c2c053c7647227c"));
                var dh = db.c_donhangs.Single(so=>so.c_donhang_id.Equals(obj.c_donhang_id));
                obj.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id;
                obj.so_po = dh.sochungtu;
                obj.sochungtu = chungtu.tiento + "/" + chungtu.so_duocgan + "/" + chungtu.hauto;
                chungtu.so_duocgan = chungtu.so_duocgan + 1;
                obj.md_kho_id = "571291beb315f41166c65640ac2fbc7f";
                obj.nguoilap = "";
                obj.nguoitao = "";
                obj.nguoicapnhat = "";
                obj.ngaylap = DateTime.Now;
                obj.ngaytao = DateTime.Now;
                obj.ngaycapnhat = DateTime.Now;
                obj.hoatdong = true;
                obj.md_trangthai_id = "SOANTHAO";

                db.c_xacnhan_xuatkhos.InsertOnSubmit(obj);
                db.SubmitChanges();               
                context.Response.Write("Tạo phiếu xác nhận thành công!" + obj.sochungtu);
            }
        }
        catch (Exception e)
        {
            context.Response.Write(e.Message);
        }


    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}