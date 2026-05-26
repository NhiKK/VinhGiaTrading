<%@ WebHandler Language="C#" Class="ThuChiController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class ThuChiController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "activephieuthu":
                this.ActivePhieuThu(context);
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

    public void ActivePhieuThu(HttpContext context)
    {
        String id = context.Request.QueryString["id"];
        c_thuchi thu = db.c_thuchis.FirstOrDefault(p => p.c_thuchi_id.Equals(id));
        decimal sum_detail = (decimal)mdbc.ExecuteScalar("select coalesce(sum(sotien), 0) from c_chitietthuchi where c_thuchi_id = '"+thu.c_thuchi_id+"'");
		if (thu != null)
        {
            if (thu.md_trangthai_id.Equals("SOANTHAO"))
            {
				
				
				/*
				// old
				if(thu.sotien.Value >= sum_detail){
					string sql = "select distinct c_packinginvoice_id from c_chitietthuchi where c_thuchi_id = '" + id + "'";
                    DataTable data = mdbc.GetData(sql);
                    foreach (DataRow item in data.Rows)
                    {
                        mdbc.ExcuteNonQuery("update c_packinginvoice set ngaycapnhat = getdate() where c_packinginvoice_id = '"+item[0]+"'");
                    }
             
					thu.md_trangthai_id = "HIEULUC";
					db.SubmitChanges();
					context.Response.Write("Hiệu lực phiếu thu thành công!");
				}else{
					context.Response.Write("Số tiền phân bổ ở các dòng không được lớn hơn số tiền trên phiếu thu.");
				}*/
				
				
				if(thu.sotien.Value >= sum_detail){
					thu.md_trangthai_id = "HIEULUC";
					db.SubmitChanges();
					
					string sql = "select distinct c_packinginvoice_id from c_chitietthuchi where c_thuchi_id = '" + id + "'";
					DataTable data = mdbc.GetData(sql);
					foreach (DataRow item in data.Rows)
					{
						c_packinginvoice pkl = db.c_packinginvoices.SingleOrDefault(p => p.c_packinginvoice_id.Equals(item[0].ToString()));
						pkl.ngaycapnhat = DateTime.Now;
						pkl.mota = pkl.mota;
						db.SubmitChanges();
						//mdbc.ExcuteNonQuery("update c_packinginvoice set ngaycapnhat = getdate() where c_packinginvoice_id = '"+item[0]+"'");
					}
					context.Response.Write("Hiệu lực phiếu thu thành công!");
				}else{
					context.Response.Write("Số tiền phân bổ ở các dòng không được lớn hơn số tiền trên phiếu thu.");
				}
				
				
				
            }
            else {
                context.Response.Write("Phiếu thu đã được hiệu lực trước đó!");
            }
        }
        else {
           context.Response.Write("Không tồn tại phiếu thu. Có thể đã được xóa trước đó!");
        }
    }

    public void getSelectOption(HttpContext context)
    {
        //String sql = "select c_thuchi_id, sophieu from c_thuchi where hoatdong = 1";
        //SelectHtmlControl s = new SelectHtmlControl(sql);
        //context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        //String h = context.Request.Form["hoatdong"].ToLower();
		string md_doitackinhdoanh_id= context.Request.Form["md_doitackinhdoanh_id"];
		string loai = context.Request.Form["loai"];
        bool hd = false;
        /*if (h.Equals("on") || h.Equals("true"))
        { hd = true; }*/
        //bool check = (bool)mdbc.ExecuteScalarProcedure("p_kiemtragiaodich", "@date", (DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null)).ToString("MM/dd/yyyy"));
        bool check = (bool)mdbc.ExecuteScalarProcedure("p_kiemtragiaodich", "@date", (DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null)).ToString("MM/dd/yyyy")
		,"@md_doitackinhdoanh_id", md_doitackinhdoanh_id);
		
		if (check.Equals(true))
        {
            c_thuchi mnu = db.c_thuchis.Single(p => p.c_thuchi_id == context.Request.Form["id"]);
            if (mnu.md_trangthai_id.Equals("SOANTHAO"))
            {
				mnu.loai = loai;
                mnu.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
                mnu.ngaylapphieu = DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null);
                mnu.nguoi_giaonop = context.Request.Form["nguoi_giaonop"];
                mnu.ngay_giaonop = DateTime.ParseExact(context.Request.Form["ngay_giaonop"], "dd/MM/yyyy", null);
                mnu.sotien = decimal.Parse(context.Request.Form["sotien"]);
                mnu.md_dongtien_id = context.Request.Form["md_dongtien_id"];
                mnu.tygia = decimal.Parse(context.Request.Form["tygia"]);
                mnu.lydo = context.Request.Form["lydo"];
                mnu.loaiphieu = context.Request.Form["loaiphieu"];
                mnu.sochungtu = context.Request.Form["sochungtu"];
                mnu.quydoi_vnd = decimal.Parse(context.Request.Form["quydoi_vnd"]);
                mnu.tk_quy = int.Parse(context.Request.Form["tk_quy"]);
                //mnu.tongcackhoan = decimal.Parse(context.Request.Form["tongcackhoan"]);
                //mnu.tongkhoanphi = decimal.Parse(context.Request.Form["tongkhoanphi"]);
                mnu.tienconlai = mnu.tongcackhoan - (mnu.sotien + mnu.tongkhoanphi);

                mnu.mota = context.Request.Form["mota"];
                mnu.hoatdong = true;
                mnu.ngaycapnhat = DateTime.Now;
                mnu.nguoicapnhat = UserUtils.getUser(context);

                db.SubmitChanges();
            }
            else {
                jqGridHelper.Utils.writeResult(0, "Không thể chi sửa khi phiếu thu đã hiệu lực!");
            }
        }
        else
        {
          jqGridHelper.Utils.writeResult(0, "Không thể thêm giao dịch vào kỳ kế toán đã đóng!");
        }
    }

    public void add(HttpContext context)
    {
        try
        {
            //String h = context.Request.Form["hoatdong"].ToLower();
			string md_doitackinhdoanh_id= context.Request.Form["md_doitackinhdoanh_id"];
			string loai = context.Request.Form["loai"];
            bool hd = false;
            /*if (h.Equals("on") || h.Equals("true"))
            { hd = true; }*/

            md_sochungtu sochungtu = db.md_sochungtus.Single(s => s.tiento.Equals("PT"));
            string sphieu = sochungtu.tiento + "/" + sochungtu.so_duocgan + "/" + sochungtu.hauto;
            bool check = (bool)mdbc.ExecuteScalarProcedure("p_kiemtragiaodich", "@date", (DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null)).ToString("MM/dd/yyyy"),"@md_doitackinhdoanh_id", md_doitackinhdoanh_id);
            if (check.Equals(true))
            {
                c_thuchi mnu = new c_thuchi();
                mnu.c_thuchi_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss"));
                mnu.sophieu = sphieu;
				mnu.loai = loai;
                mnu.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
                mnu.ngaylapphieu = DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null);
                mnu.nguoi_giaonop = context.Request.Form["nguoi_giaonop"];
                mnu.ngay_giaonop = DateTime.ParseExact(context.Request.Form["ngay_giaonop"], "dd/MM/yyyy", null);
                mnu.sotien = decimal.Parse(context.Request.Form["sotien"]);
                mnu.md_dongtien_id = context.Request.Form["md_dongtien_id"];
                mnu.tygia = decimal.Parse(context.Request.Form["tygia"]);
                mnu.lydo = context.Request.Form["lydo"];
                mnu.loaiphieu = context.Request.Form["loaiphieu"];
                mnu.sochungtu = context.Request.Form["sochungtu"];
                mnu.quydoi_vnd = decimal.Parse(context.Request.Form["quydoi_vnd"]);
                mnu.tk_quy = int.Parse(context.Request.Form["tk_quy"]);
                mnu.tongcackhoan = 0;
                mnu.tongkhoanphi = 0;
                mnu.tienconlai = mnu.tongcackhoan - mnu.sotien + mnu.tongkhoanphi;
                mnu.md_trangthai_id = context.Request.Form["SOANTHAO"];
                string md_loaichungtu_id = db.md_loaichungtus.Where(s => s.kieu_doituong == "PT").Select(s => s.md_loaichungtu_id).FirstOrDefault();
                mnu.md_loaichungtu_id = md_loaichungtu_id;
                mnu.ispaymentin = true;
                mnu.md_trangthai_id = "SOANTHAO";

                mnu.mota = context.Request.Form["mota"];
                mnu.hoatdong = true;
                mnu.nguoitao = UserUtils.getUser(context);
                mnu.nguoicapnhat = UserUtils.getUser(context);
                mnu.ngaytao = DateTime.Now;
                mnu.ngaycapnhat = DateTime.Now;

                db.c_thuchis.InsertOnSubmit(mnu);
                sochungtu.so_duocgan = sochungtu.so_duocgan + sochungtu.buocnhay;
                db.SubmitChanges();
                jqGridHelper.Utils.writeResult(1, "Thêm mới thành công");
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Không thể thêm giao dịch vào kỳ kế toán đã đóng!");
            }
                
        }
		catch(Exception e)
		{
            jqGridHelper.Utils.writeResult(0, e.Message);
        }
    }


    public void del(HttpContext context)
    {
        try
        {
            String id = context.Request.Form["id"];
            c_thuchi thu = db.c_thuchis.FirstOrDefault(p => p.c_thuchi_id.Equals(id));
            if (thu != null)
            {
                if (thu.md_trangthai_id.Equals("SOANTHAO"))
                {
                    String delThuChi = "delete c_thuchi where c_thuchi_id = @c_thuchi_id";
                    String delLQ = "delete c_chiphilienquan where c_thuchi_id = @c_thuchi_id";
                    String delCT = "delete c_chitietthuchi where c_thuchi_id = @c_thuchi_id";
                    mdbc.ExcuteNonQuery(delLQ, "@c_thuchi_id", id);
                    mdbc.ExcuteNonQuery(delCT, "@c_thuchi_id", id);
                    mdbc.ExcuteNonQuery(delThuChi, "@c_thuchi_id", id);                    
                }
                else
                {
                    jqGridHelper.Utils.writeResult(0, "Không thể xóa khi phiếu thu đã hiệu lực!");
                }
            }
            else {
                jqGridHelper.Utils.writeResult(0, "Không tồn tại phiếu thu. Có thể đã được xóa trước đó!");
            }
        }
        catch (Exception ex)
        {
            jqGridHelper.Utils.writeResult(0, ex.Message);
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
		
        String sqlCount = "SELECT COUNT(*) AS count FROM c_thuchi tc, md_doitackinhdoanh dtkd, md_dongtien dt, md_loaichungtu lct " +
            " WHERE tc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " AND tc.md_dongtien_id = dt.md_dongtien_id " +
            " AND tc.md_loaichungtu_id = lct.md_loaichungtu_id AND tc.ispaymentin = 1" + filter;
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
            sidx = "tc.c_thuchi_id";
        }
        
        string strsql = "select * from( " +
            " select tc.c_thuchi_id, tc.md_trangthai_id,tc.loai, tc.sophieu,  " +
            " dtkd.ma_dtkd, tc.ngaylapphieu, tc.nguoi_giaonop, " +
	        " tc.ngay_giaonop, tc.sotien, " +
	        " dt.ma_iso, tc.tygia, " +
	        " tc.lydo, tc.loaiphieu, " +
	        " tc.sochungtu, tc.quydoi_vnd, " +
	        " tc.tk_no, tc.tk_co, " +
	        " tc.tk_quy, tc.so_dadinhkhoan, " +
	        " tc.so_chuadinhkhoan, tc.so_dachiphi, tc.ispaymentin," +
            " lct.tenchungtu, tc.tongcackhoan, tc.tongkhoanphi, tc.tienconlai," +
            " tc.ngaytao, tc.nguoitao, tc.ngaycapnhat, tc.nguoicapnhat, tc.mota, tc.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_thuchi tc, md_doitackinhdoanh dtkd, md_dongtien dt, md_loaichungtu lct " +
            " WHERE tc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
	        " AND tc.md_dongtien_id = dt.md_dongtien_id " +
            " AND tc.md_loaichungtu_id = lct.md_loaichungtu_id " +
            " AND tc.ispaymentin = 1 " + filter +
            ")P WHERE RowNum > @start AND RowNum < @end";

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_thuchi_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["loai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaylapphieu"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoi_giaonop"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngay_giaonop"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sotien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_iso"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tygia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["loaiphieu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["lydo"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["quydoi_vnd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_quy"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tongcackhoan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tongkhoanphi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tienconlai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ispaymentin"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
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
