<%@ WebHandler Language="C#" Class="TaoPackingListController_bak" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Data;

public class TaoPackingListController_bak : IHttpHandler {
    LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
		String action, oper;
        action = context.Request.QueryString["action"];
        switch (action)
        {
			case "add":
                this.add(context);
                break;
			case "load":
                this.load(context);
                break;
        }
	}
	
	public void load(HttpContext context)
	{
		String arrayPhieuXuat = context.Request.Form["p[arrayPhieuXuat]"].ToString();
		String phieuxuatkho = "'" + arrayPhieuXuat.Replace(",", "','").Replace(" ", "") + "'";
		//--
		String getAllLine = String.Format("SELECT (select c_donhang_id from c_dongdonhang where c_dongdonhang_id = dnx.c_dongdonhang_id) as c_donhang_id, nx.md_doitackinhdoanh_id, dnx.* " +
            " FROM c_nhapxuat nx, c_dongnhapxuat dnx " +
            " WHERE nx.c_nhapxuat_id = dnx.c_nhapxuat_id AND dnx.c_nhapxuat_id IN({0}) ", phieuxuatkho);
		string sql_getdh = "select distinct c_donhang_id from (" + getAllLine + ") as tmp";
        DataTable dt_dh = mdbc.GetData(sql_getdh);
		//--
		decimal phi_tang = 0;
        decimal phi_giam = 0;
        //--
		decimal check_giatricong_po = (decimal)mdbc.ExecuteScalar("select coalesce(sum(giatricong_po), 0) from c_packinginvoice where md_trangthai_id = 'HIEULUC' and c_packinginvoice_id in (select distinct a.c_packinginvoice_id from c_dongpklinv a	left join c_dongnhapxuat b on b.c_dongnhapxuat_id = a.c_dongnhapxuat_id where  b.c_nhapxuat_id IN (" + phieuxuatkho + "))");
		decimal check_giatritru_po = (decimal)mdbc.ExecuteScalar("select coalesce(sum(giatritru_po), 0) from c_packinginvoice where md_trangthai_id = 'HIEULUC' and c_packinginvoice_id in (select distinct a.c_packinginvoice_id from c_dongpklinv a	left join c_dongnhapxuat b on b.c_dongnhapxuat_id = a.c_dongnhapxuat_id where b.c_nhapxuat_id IN (" + phieuxuatkho + "))");
		
		foreach (DataRow rdh in dt_dh.Rows)
        {
			phi_tang += ((decimal)mdbc.ExecuteScalar("select coalesce(sum(phi), 0) from c_phidonhang where phitang = 1 and c_donhang_id ='" + rdh["c_donhang_id"] + "'"));
			phi_giam += ((decimal)mdbc.ExecuteScalar("select coalesce(sum(phi), 0) from c_phidonhang where phitang = 0 and c_donhang_id ='" + rdh["c_donhang_id"] + "'"));
        }
		phi_tang = phi_tang - check_giatricong_po;
		phi_giam = phi_giam - check_giatritru_po;
		if(arrayPhieuXuat == "" | arrayPhieuXuat == " " | arrayPhieuXuat == null)
		{
			phi_tang = -1;
			phi_giam = -1;
		}
		context.Response.Write(phi_tang + "," + phi_giam);
	}
	
	public void add(HttpContext context)
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
        decimal totaldiscount = 0;
        decimal totalnet = 0;
        decimal totalgross = 0;
		//--
		decimal txtGiaTriCongPO = decimal.Parse(context.Request.Form["p[txtGiaTriCongPO]"]);
		decimal txtTongGiaTriCongPO = decimal.Parse(context.Request.Form["p[txtTongGiaTriCongPO]"]);
		//--
		decimal txtGiaTriTruPO = decimal.Parse(context.Request.Form["p[txtGiaTriTruPO]"]);
		decimal txtTongGiaTriTruPO = decimal.Parse(context.Request.Form["p[txtTongGiaTriTruPO]"]);

        List<c_dongpklinv> lst = new List<c_dongpklinv>();
        String getAllLine = String.Format("SELECT (select c_donhang_id from c_dongdonhang where c_dongdonhang_id = dnx.c_dongdonhang_id) as c_donhang_id, nx.md_doitackinhdoanh_id, dnx.* " +
            " FROM c_nhapxuat nx, c_dongnhapxuat dnx " +
            " WHERE nx.c_nhapxuat_id = dnx.c_nhapxuat_id AND dnx.c_nhapxuat_id IN({0}) ", phieuxuatkho);
		//--
        int sothutu = 10;
        DataTable dt = mdbc.GetData(getAllLine);
        String c_dongdonhang_id = dt.Rows[0]["c_dongdonhang_id"].ToString();
        String c_donhang_id = db.c_dongdonhangs.FirstOrDefault(ddh => ddh.c_dongdonhang_id.Equals(c_dongdonhang_id)).c_donhang_id;
        String md_cangbien_id = db.c_donhangs.FirstOrDefault(dh => dh.c_donhang_id.Equals(c_donhang_id)).md_cangbien_id;
		//--
        c_packinginvoice p = new c_packinginvoice();
        p.c_packinginvoice_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddhhmmss"));
        p.so_pkl = txtSoPackingList;
        p.so_inv = txtSoInvoice;
        p.ngaylap = DateTime.ParseExact(txtNgayLap, "dd/MM/yyyy", null);
        p.etd = DateTime.ParseExact(txtNgayVanDon, "dd/MM/yyyy", null);
        p.noiden = selNoiDen;
        p.mv = txtSoTau;
		//--
        p.diengiai_cong = txtDienGiaiCongThem;
        p.diengiai_tru = txtDienGiaiTruLai;
        p.giatri_cong = decimal.Parse(txtGiaTriCongThem);
        p.giatri_tru = decimal.Parse(txtGiaTriTruLai);
        p.md_trangthai_id = "SOANTHAO";
        p.thanhtoanxong = false;
        p.discount = db.c_donhangs.FirstOrDefault(dh=>dh.c_donhang_id.Equals(c_donhang_id)).discount;
        p.totaldis = totalnet * p.discount / 100;
        p.ngay_phaitt = DateTime.Now;
		//--
        p.nguoitao = UserUtils.getUser(context);
        p.nguoicapnhat = UserUtils.getUser(context);
        p.ngaytao = DateTime.Now;
        p.ngaycapnhat = DateTime.Now;
        p.hoatdong = true;
        p.noidi = md_cangbien_id;
        
        foreach (DataRow item in dt.Rows)
        {

            String c_dongnhapxuat_id = item["c_dongnhapxuat_id"].ToString();
            c_dongpklinv line = new c_dongpklinv();
            c_dongnhapxuat dongnx = db.c_dongnhapxuats.Single(dnx => dnx.c_dongnhapxuat_id.Equals(c_dongnhapxuat_id));
            var dongdonhang = db.c_dongdonhangs.Single(ddh => ddh.c_dongdonhang_id.Equals(item["c_dongdonhang_id"]));
            var donhang = db.c_donhangs.Single(dh => dh.c_donhang_id.Equals(dongdonhang.c_donhang_id));
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

            totaldiscount += (line.thanhtien.Value * donhang.discount.Value) / 100;
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
        string diendai_phitang = "";
        string diendai_phigiam = "";
        int check_phidh = 0;
        string sql_getdh = "select distinct c_donhang_id from (" + getAllLine + ") as tmp";
        DataTable dt_dh = mdbc.GetData(sql_getdh);
		string donhang_id="";
		string consignee = "";
        foreach (DataRow rdh in dt_dh.Rows)
        {
            check_phidh = (int)mdbc.ExecuteScalar("select count(*) as count from c_dongpklinv where c_donhang_id ='" + rdh["c_donhang_id"].ToString() + "'");
            if (check_phidh == 0)
            {
                phi_tang += ((decimal)mdbc.ExecuteScalar("select coalesce(sum(phi), 0) from c_phidonhang where phitang = 1 and c_donhang_id ='" + rdh["c_donhang_id"] + "'"));
                phi_giam += ((decimal)mdbc.ExecuteScalar("select coalesce(sum(phi), 0) from c_phidonhang where phitang = 0 and c_donhang_id ='" + rdh["c_donhang_id"] + "'"));
                diendai_phitang += ((string)mdbc.ExecuteScalar("select top(1) mota from c_phidonhang where c_donhang_id ='" + rdh["c_donhang_id"] + "' and phitang = 1"));
                diendai_phigiam += ((string)mdbc.ExecuteScalar("select top(1) mota from c_phidonhang where c_donhang_id ='" + rdh["c_donhang_id"] + "' and phitang = 0"));
            }
			donhang_id = rdh["c_donhang_id"].ToString();
			string getConsignee = string.Format("select isnull('|| ' + mota, '') from c_donhang where c_donhang_id ='{0}'", rdh["c_donhang_id"]);
			consignee += (string)mdbc.ExecuteScalar(getConsignee);
        }
		try { consignee = consignee.Substring(2, consignee.Length - 2); } catch { }
		
		p.consignee = consignee;
        //p.giatricong_po = phi_tang;
        p.giatricong_po = txtGiaTriCongPO;
        //p.giatritru_po = phi_giam;
        p.giatritru_po = txtGiaTriTruPO;
        p.diengiaicong_po = diendai_phitang;
        p.diengiaitru_po = diendai_phigiam;

        p.totaldis = Math.Round(totaldiscount, 2);
        p.totalnet = Math.Round(totalnet, 2);
        p.totalgross = p.totalnet - p.totaldis + (p.giatri_cong + p.giatricong_po) - (p.giatri_tru + p.giatritru_po);
				
	//tinh hoa hong phai tra khi tao invoice
        string sql_hhdh = "select coalesce(hoahong, 0) from c_donhang where c_donhang_id = '" +donhang_id+ "'";
        decimal phantramhh = (decimal)mdbc.ExecuteScalar(sql_hhdh);
        p.hoahongphaitra = Math.Round((p.totalgross.Value * phantramhh) / 100, 2);
        p.phantramhoahong = phantramhh;
        p.hoahongdatra = 0;
		
        try
        {
            int check = (from inv in db.c_packinginvoices
                         where inv.so_inv.Equals(txtSoInvoice)
                         select new { inv.so_inv }).Count();
					
			if(txtTongGiaTriCongPO < 0 || txtTongGiaTriTruPO < 0)
			{
				context.Response.Write("Chưa tính lại phí cộng trừ PO!");
			}
			else if(txtGiaTriCongPO < 0 || txtGiaTriTruPO < 0)
			{
				context.Response.Write("Phí cộng trừ PO phải lớn hơn hoặc bằng 0!");
			}
            else if(txtGiaTriCongPO > txtTongGiaTriCongPO)
			{
				context.Response.Write("'Giá Trị cộng PO' không được lớn hơn 'Tổng Giá Trị cộng PO'!");
			}
			else if(txtGiaTriTruPO > txtTongGiaTriTruPO)
			{
				context.Response.Write("'Giá Trị trừ PO' không được lớn hơn 'Tổng Giá Trị trừ PO'!");
			}
			else if (check == 0)
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
                context.Response.Write("Số Packing List/Invoice đã tồn tại trong chương trình.!");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }
    
    

 
    public bool IsReusable {
        get {
            return false;
        }
    }

}