<%@ WebHandler Language="C#" Class="taoListQuotation" %>

using System;
using System.Web;
using System.Linq;
using Newtonsoft.Json.Linq;
using NetServ.Net.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class taoListQuotation : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        /*string action = context.Request.Form["action"].ToString();
        
        if(action.Equals("createList")){
            createListQO(context);
        }*/
		
		//try
        {
            string listQO = "";
            string khachang = context.Request.Form["dg[khachhang]"].ToString();
            string shipmenttime = context.Request.Form["dg[tgat]"].ToString();
            string shipmentdate = context.Request.Form["dg[ngayhethan]"].ToString();
            string ngaybaogia = context.Request.Form["dg[ngaybaogia]"].ToString();
            string postdata = context.Request.Form["dg[datagrid]"].ToString();
            //string paymentterm = context.Request.Form["dg[paymentterm]"].ToString();
            string trongluong = context.Request.Form["dg[trongluong]"].ToString();

            string kichthuoc = context.Request.Form["dg[kichthuoc]"].ToString();
            string dongtien = context.Request.Form["dg[dongtien]"].ToString();

            IList<JToken> tokenList = JToken.Parse(postdata).ToList();
            LinqDBDataContext db = new LinqDBDataContext();
            List<taoQuotation> productList = new List<taoQuotation>();

            foreach (JToken token in tokenList)
            {
                productList.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<taoQuotation>(token.ToString()));
            }

            List<String> arr = new List<String>();
            foreach (taoQuotation item in productList)
            {
                string sql = "select md_cangbien_id from md_sanpham where md_sanpham_id ='" + item.md_sanpham_id + "'";
                string cb = (string)mdbc.ExecuteScalar(sql);
                if (!arr.Contains(cb))
                {
                    arr.Add(cb);
                }
            }
            List<c_chitietbaogia> ctbgs = new List<c_chitietbaogia>();
			// if(DateTime.ParseExact(ngaybaogia, "dd/MM/yyyy", null) > DateTime.ParseExact(shipmentdate, "dd/MM/yyyy", null))
			// {
				// context.Response.Write("Ngày hết hạn  phải trễ hơn ngày báo giá ít nhất 1 ngày.");
			// }
			// else
			{
				for (int i = 0; i < arr.Count; i++)
				{
					string id_baogia = ImportUtils.getNEWID();
					c_baogia mnu = new c_baogia();
					mnu.c_baogia_id = id_baogia;
					mnu.sobaogia = jqGridHelper.Utils.taoChungTuQO(khachang, 0, DateTime.ParseExact(ngaybaogia, "dd/MM/yyyy", null).Year.ToString());
					mnu.md_doitackinhdoanh_id = khachang;
					mnu.shipmenttime = int.Parse(shipmenttime);
					mnu.ngaybaogia = DateTime.ParseExact(ngaybaogia, "dd/MM/yyyy", null);
					mnu.ngayhethan = DateTime.ParseExact(shipmentdate, "dd/MM/yyyy", null);
					mnu.md_trongluong_id = trongluong;
					mnu.md_cangbien_id = arr[i].ToString();
					mnu.md_dongtien_id = dongtien;
					mnu.md_kichthuoc_id = kichthuoc;
					mnu.totalcbm = 0;
					mnu.totalcbf = 0;
					mnu.totalquo = 0;
					mnu.md_trangthai_id = "SOANTHAO";
					mnu.isform = false;
					mnu.mota = "";
					mnu.hoatdong = true;
					mnu.ngaytao = DateTime.Now;
					mnu.nguoitao = UserUtils.getUser(context);
					mnu.ngaycapnhat = DateTime.Now;
					mnu.nguoicapnhat = UserUtils.getUser(context);
					
					db.c_baogias.InsertOnSubmit(mnu);
					db.SubmitChanges();
					
					listQO += mnu.sobaogia + ", ";
					int sothutu = 10;
					foreach (taoQuotation item in productList)// duyet qua cac ma hang cung cang voi cang dang truy xuat o tren
					{
						md_sanpham pro = db.md_sanphams.Single(p => p.md_sanpham_id.Equals(item.md_sanpham_id));
						if (pro.md_cangbien_id.Equals(arr[i].ToString()))
						{
							// add ma hang vao qo dang tao
							
							decimal price = (decimal)mdbc.ExecuteScalarProcedure("getGiaMuaSp", "@md_sanpham_id", item.md_sanpham_id, "@md_doitackinhdoanh_id", khachang, "@loai_bg", true, "@ngaytinhgia", DateTime.ParseExact(ngaybaogia, "dd/MM/yyyy", null));

							//  select packing
							String getPacking = @"SELECT
									dg.md_donggoi_id, dg.sl_inner
									, dg.dvt_inner
									, dg.l1, dg.w1, dg.h1
									, dg.sl_outer
									, dg.dvt_outer
									, dg.l2_mix, dg.w2_mix, dg.h2_mix, dg.soluonggoi_ctn
									, dg.v2 , isnull(dg.vd,0) as vd, isnull(dg.vn,0) as vn, isnull(dg.vl,0) as vl, dg.ghichu_vachngan
								FROM
									md_donggoi dg JOIN md_donggoisanpham dgsp ON dg.md_donggoi_id = dgsp.md_donggoi_id
								WHERE
									dgsp.macdinh = 1
									AND dgsp.md_sanpham_id = @md_sanpham_id";

							DataTable dtPacking = mdbc.GetData(getPacking, "@md_sanpham_id", item.md_sanpham_id);
							if (dtPacking.Rows.Count > 0)
							{

								c_chitietbaogia mnu_ = new c_chitietbaogia();
								mnu_.c_chitietbaogia_id = ImportUtils.getNEWID();
								mnu_.trangthai = "BT";
								mnu_.c_baogia_id = id_baogia;
								mnu_.md_sanpham_id = item.md_sanpham_id;
								mnu_.ma_sanpham_khach = item.ma_sp_khach;
								mnu_.md_cangbien_id = pro.md_cangbien_id;
								mnu_.mota_tienganh = pro.mota_tienganh;
								mnu_.mota_tiengviet = pro.mota_tiengviet;
								mnu_.ghichu_vachngan = dtPacking.Rows[0]["ghichu_vachngan"].ToString();
								mnu_.trongluong = pro.trongluong;
								mnu_.sothutu = sothutu;
								mnu_.giafob = price;
								mnu_.soluong = item.soluong;
								mnu_.md_donggoi_id = dtPacking.Rows[0]["md_donggoi_id"].ToString();
								mnu_.sl_inner = decimal.Parse(dtPacking.Rows[0]["sl_inner"].ToString());
								mnu_.l1 = decimal.Parse(dtPacking.Rows[0]["l1"].ToString());
								mnu_.w1 = decimal.Parse(dtPacking.Rows[0]["w1"].ToString());
								mnu_.h1 = decimal.Parse(dtPacking.Rows[0]["h1"].ToString());
								mnu_.sl_outer = decimal.Parse(dtPacking.Rows[0]["sl_outer"].ToString());
								mnu_.l2 = decimal.Parse(dtPacking.Rows[0]["l2_mix"].ToString());
								mnu_.w2 = decimal.Parse(dtPacking.Rows[0]["w2_mix"].ToString());
								mnu_.h2 = decimal.Parse(dtPacking.Rows[0]["h2_mix"].ToString());
								mnu_.v2 = decimal.Parse(dtPacking.Rows[0]["v2"].ToString());
								mnu_.vd = decimal.Parse(dtPacking.Rows[0]["vd"].ToString());
								mnu_.vn = decimal.Parse(dtPacking.Rows[0]["vn"].ToString());
								mnu_.vl = decimal.Parse(dtPacking.Rows[0]["vl"].ToString());
								mnu_.sl_cont = decimal.Parse(dtPacking.Rows[0]["soluonggoi_ctn"].ToString());
								mnu_.ghichu = "";

								mnu_.mota = "";
								mnu_.hoatdong = true;
								mnu_.ngaytao = DateTime.Now;
								mnu_.ngaycapnhat = DateTime.Now;
								mnu_.nguoitao = UserUtils.getUser(context);
								mnu_.nguoicapnhat = UserUtils.getUser(context);
								ctbgs.Add(mnu_);
								sothutu +=10;
							}
						}

					}
					
				}
				
				db.c_chitietbaogias.InsertAllOnSubmit(ctbgs);       
				db.SubmitChanges();
				context.Response.Write("Đã tạo các QO: " + listQO);
			}
        }
		//catch(Exception ex){
          //  context.Response.Write(ex.Message);
        //}
        
    }

    /*public void createListQO(HttpContext context)
    {
        //try
        {
            string listQO = "";
            string khachang = context.Request.Form["dg[khachhang]"].ToString();
            string shipmenttime = context.Request.Form["dg[tgat]"].ToString();
            string shipmentdate = context.Request.Form["dg[ngayhethan]"].ToString();
            string ngaybaogia = context.Request.Form["dg[ngaybaogia]"].ToString();
            string postdata = context.Request.Form["dg[datagrid]"].ToString();
            //string paymentterm = context.Request.Form["dg[paymentterm]"].ToString();
            string trongluong = context.Request.Form["dg[trongluong]"].ToString();

            string kichthuoc = context.Request.Form["dg[kichthuoc]"].ToString();
            string dongtien = context.Request.Form["dg[dongtien]"].ToString();

            IList<JToken> tokenList = JToken.Parse(postdata).ToList();
            LinqDBDataContext db = new LinqDBDataContext();
            List<taoQuotation> productList = new List<taoQuotation>();

            foreach (JToken token in tokenList)
            {
                productList.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<taoQuotation>(token.ToString()));
            }

            List<String> arr = new List<String>();
            foreach (taoQuotation item in productList)
            {
                string sql = "select md_cangbien_id from md_sanpham where md_sanpham_id ='" + item.md_sanpham_id + "'";
                string cb = (string)mdbc.ExecuteScalar(sql);
                if (!arr.Contains(cb))
                {
                    arr.Add(cb);
                }
            }
            List<c_chitietbaogia> ctbgs = new List<c_chitietbaogia>();
            for (int i = 0; i < arr.Count; i++)
            {
                string id_baogia = ImportUtils.getNEWID();
                c_baogia mnu = new c_baogia
                {
                    c_baogia_id = id_baogia,
                    sobaogia = jqGridHelper.Utils.taoChungTuQO(khachang, 0, DateTime.ParseExact(ngaybaogia, "dd/MM/yyyy", null).Year.ToString()),
                    md_doitackinhdoanh_id = khachang,
                    //md_paymentterm_id = paymentterm,
                    shipmenttime = int.Parse(shipmenttime),
                    //md_banggia_id = context.Request.Form["md_banggia_id"],
                    ngaybaogia = DateTime.ParseExact(ngaybaogia, "dd/MM/yyyy", null),
                    ngayhethan = DateTime.ParseExact(shipmentdate, "dd/MM/yyyy", null),
                    md_trongluong_id = trongluong,
                    md_cangbien_id = arr[i].ToString(),
                    md_dongtien_id = dongtien,
                    md_kichthuoc_id = kichthuoc,
                    totalcbm = 0,
                    totalcbf = 0,
                    totalquo = 0,
                    md_trangthai_id = "SOANTHAO",
                    isform = false,
                    mota = "",
                    hoatdong = true,
                    ngaytao = DateTime.Now,
                    nguoitao = UserUtils.getUser(context),
                    ngaycapnhat = DateTime.Now,
                    nguoicapnhat = UserUtils.getUser(context)
                };
                
                db.c_baogias.InsertOnSubmit(mnu);
                db.SubmitChanges();
                
                listQO += mnu.sobaogia + ", ";
				int sothutu = 10;
                foreach (taoQuotation item in productList)// duyet qua cac ma hang cung cang voi cang dang truy xuat o tren
                {
                    md_sanpham pro = db.md_sanphams.Single(p => p.md_sanpham_id.Equals(item.md_sanpham_id));
                    if (pro.md_cangbien_id.Equals(arr[i].ToString()))
                    {
						// add ma hang vao qo dang tao
                        
                        decimal price = (decimal)mdbc.ExecuteScalarProcedure("getGiaMuaSp", "@md_sanpham_id", item.md_sanpham_id, "@md_doitackinhdoanh_id", khachang, "@loai_bg", true, "@ngaytinhgia", DateTime.ParseExact(ngaybaogia, "dd/MM/yyyy", null));

                        //  select packing
                        String getPacking = @"SELECT
	                            dg.md_donggoi_id, dg.sl_inner
	                            , dg.dvt_inner
	                            , dg.l1, dg.w1, dg.h1
	                            , dg.sl_outer
	                            , dg.dvt_outer
	                            , dg.l2_mix, dg.w2_mix, dg.h2_mix, dg.soluonggoi_ctn
	                            , dg.v2 , dg.vd, dg.vn, dg.vl, dg.ghichu_vachngan
                            FROM
	                            md_donggoi dg JOIN md_donggoisanpham dgsp ON dg.md_donggoi_id = dgsp.md_donggoi_id
                            WHERE
	                            dgsp.macdinh = 1
	                            AND dgsp.md_sanpham_id = @md_sanpham_id";

                        DataTable dtPacking = mdbc.GetData(getPacking, "@md_sanpham_id", item.md_sanpham_id);
                        if (dtPacking.Rows.Count > 0)
                        {

                            c_chitietbaogia mnu_ = new c_chitietbaogia
                            {
                                c_chitietbaogia_id = ImportUtils.getNEWID(),
                                c_baogia_id = id_baogia,
                                md_sanpham_id = item.md_sanpham_id,
                                ma_sanpham_khach = item.ma_sp_khach,
                                md_cangbien_id = pro.md_cangbien_id,
                                mota_tienganh = pro.mota_tienganh,
                                mota_tiengviet = pro.mota_tiengviet,
                                ghichu_vachngan = dtPacking.Rows[0]["ghichu_vachngan"].ToString(),
                                trongluong = pro.trongluong,
                                sothutu = sothutu,
                                giafob = price,
                                soluong = item.soluong,
                                md_donggoi_id = dtPacking.Rows[0]["md_donggoi_id"].ToString(),
                                sl_inner = decimal.Parse(dtPacking.Rows[0]["sl_inner"].ToString()),
                                l1 = decimal.Parse(dtPacking.Rows[0]["l1"].ToString()),
                                w1 = decimal.Parse(dtPacking.Rows[0]["w1"].ToString()),
                                h1 = decimal.Parse(dtPacking.Rows[0]["h1"].ToString()),
                                sl_outer = decimal.Parse(dtPacking.Rows[0]["sl_outer"].ToString()),
                                l2 = decimal.Parse(dtPacking.Rows[0]["l2_mix"].ToString()),
                                w2 = decimal.Parse(dtPacking.Rows[0]["w2_mix"].ToString()),
                                h2 = decimal.Parse(dtPacking.Rows[0]["h2_mix"].ToString()),
                                v2 = decimal.Parse(dtPacking.Rows[0]["v2"].ToString()),
                                vd = decimal.Parse(dtPacking.Rows[0]["vd"].ToString()),
                                vn = decimal.Parse(dtPacking.Rows[0]["vn"].ToString()),
                                vl = decimal.Parse(dtPacking.Rows[0]["vl"].ToString()),
                                sl_cont = decimal.Parse(dtPacking.Rows[0]["soluonggoi_ctn"].ToString()),
                                ghichu = "",

                                mota = "",
                                hoatdong = true,
                                ngaytao = DateTime.Now,
                                ngaycapnhat = DateTime.Now,
                                nguoitao = UserUtils.getUser(context),
                                nguoicapnhat = UserUtils.getUser(context)
                            };
                            ctbgs.Add(mnu_);
							sothutu +=10;
                        }
                    }

                }
				
            }
			
			db.c_chitietbaogias.InsertAllOnSubmit(ctbgs);       
			db.SubmitChanges();
            context.Response.Write("Đã tạo các QO: " + listQO);
        }
		//catch(Exception ex){
          //  context.Response.Write(ex.Message);
        //}
    }*/
	
    public bool IsReusable {
        get {
            return false;
        }
    }

}