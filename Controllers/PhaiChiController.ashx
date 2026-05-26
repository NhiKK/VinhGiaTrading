<%@ WebHandler Language="C#" Class="PhaiChiController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class PhaiChiController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "activephieuchi":
                this.ActivePhieuChi(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "chitudong":
                this.ChiTuDong(context);
                break;
            case "getdoitac":
                this.Getdoitac(context);
                break;
            case "getdongtien":
                this.Getdongtien(context);
                break;
            case "gettygia":
                this.Gettygia(context);
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

    //nhan-chitudong
    public void Getdoitac(HttpContext context)
    {
        string select = "<select>";
        foreach (md_doitackinhdoanh dtkd in db.md_doitackinhdoanhs.ToList().OrderBy(p => p.ma_dtkd))
        {
            select += "<option value=\"" + dtkd.md_doitackinhdoanh_id + "\">" + dtkd.ma_dtkd + "</option>";
        }
        select += "</select>";

        context.Response.Write(select);
    }
    public void Getdongtien(HttpContext context)
    {
        string select = "<select>";
        foreach (md_dongtien dt in db.md_dongtiens.ToList())
        {
            if (dt.ma_iso == "VND")
            {
                select += "<option selected value=\"" + dt.md_dongtien_id + "\">" + dt.ma_iso + "</option>";
            }
            else
            {
                select += "<option value=\"" + dt.md_dongtien_id + "\">" + dt.ma_iso + "</option>";
            }
        }
        select += "</select>";

        context.Response.Write(select);
    }
    public void Gettygia(HttpContext context)
    {
        string select = "<input";
        md_tygia tg = db.md_tygias.ToList().OrderByDescending(p => p.hieuluc_tungay).FirstOrDefault();

        select += " type='text' value= '" + tg.nhan_voi + "'";

        select += "/>";

        context.Response.Write(select);
    }

    public decimal isnull(decimal t)
    {
        if (t == null)
            return 0;
        else
            return t;

    }

    public void ChiTuDong(HttpContext context)
    {
        try
        {
            //String h = context.Request.Form["hoatdong"].ToLower();
            bool hd = false;
            /*if (h.Equals("on") || h.Equals("true"))
            { hd = true; }*/

            md_sochungtu sochungtu = db.md_sochungtus.Single(s => s.tiento.Equals("PC"));
            string dtkd = context.Request.Form["md_doitackinhdoanh_id"];
            string ma_dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == dtkd).Select(s => s.ma_dtkd).FirstOrDefault();
            string sphieu = sochungtu.tiento + "/" + sochungtu.so_duocgan + "/" + sochungtu.hauto;
            bool check = (bool)mdbc.ExecuteScalarProcedure("p_kiemtragiaodich", "@date", (DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null)).ToString("MM/dd/yyyy"), "@md_doitackinhdoanh_id", dtkd);
            DateTime nbd = DateTime.ParseExact(context.Request.Form["tungay"], "dd/MM/yyyy", null);
            DateTime nkt = DateTime.ParseExact(context.Request.Form["denngay"], "dd/MM/yyyy", null);
            if(ma_dtkd == "")
            {
                jqGridHelper.Utils.writeResult(0, "Lỗi:Chưa chọn Đối tác");
            }
            else if (check.Equals(true))
            {
                //dtkd = context.Request.Form["md_doitackinhdoanh_id"];
                var chitietcackhoan = from c in db.c_nhapxuats
                                      join p in db.c_donhangs on c.c_donhang_id equals p.c_donhang_id
                                      join h1 in db.c_dongpklinvs on p.c_donhang_id equals h1.c_donhang_id
                                      join h2 in db.c_packinginvoices on h1.c_packinginvoice_id equals h2.c_packinginvoice_id
                                      where c.md_loaichungtu_id == "NK"
                                      & c.md_trangthai_id == "HIEULUC" & c.md_doitackinhdoanh_id == dtkd
                                      & c.grandtotal > 0
                                      select new
                                      {
                                          ngay_motokhai = h2.ngay_motokhai,
                                          c_nhapxuat_id = c.c_nhapxuat_id,
                                          c_donhang_id = c.c_donhang_id,
                                          sochungtu = c.sophieu,
                                          sodonhang = p.sochungtu,
                                          thanhtien = c.grandtotal,
                                          datra =
                                          (from k in db.c_chitietthuchis.ToList()
                                           where k.obj_code == c.c_nhapxuat_id
                                           select new { sotien = k.sotien }).Sum(t => t.sotien.Value)
                                      };

                var chitietcackhoan2 = from c in chitietcackhoan.Distinct()
                                       where (c.thanhtien - c.datra > 0 | c.datra == null) & (c.ngay_motokhai >= nbd & c.ngay_motokhai <= nkt)
                                       select new
                                       {
                                           c_nhapxuat_id = c.c_nhapxuat_id,
                                           c_donhang_id = c.c_donhang_id,
                                           sochungtu = c.sochungtu,
                                           sodonhang = c.sodonhang,
                                           thanhtien = c.thanhtien,
                                           datra = c.datra.ToString(),
                                           //              conlaiphaitra = c.thanhtien - isnull(c.datra)
                                       };


                var sotien = mdbc.ExecuteScalar("select [dbo].[getTIENCHITUDONG]('" + dtkd + "','"+ nbd.ToString("MM/dd/yyyy") +"','"+ nkt.ToString("MM/dd/yyyy") +"')");


                c_thuchi mnu = new c_thuchi();
                string c_thuchi_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss"));
                mnu.c_thuchi_id = c_thuchi_id;
                mnu.sophieu = sphieu;
                mnu.md_doitackinhdoanh_id = dtkd;
                mnu.ngaylapphieu = DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null);
                mnu.nguoi_giaonop = context.Request.Form["nguoi_giaonop"];
                mnu.ngay_giaonop = DateTime.ParseExact(context.Request.Form["ngay_giaonop"], "dd/MM/yyyy", null);
                mnu.sotien = decimal.Parse(sotien.ToString());
                mnu.md_dongtien_id = context.Request.Form["md_dongtien_id"];
                mnu.tygia = decimal.Parse(context.Request.Form["tygia"]);
                // mnu.lydo = context.Request.Form["lydo"];
                mnu.loaiphieu = context.Request.Form["loaiphieu"];
                mnu.sochungtu = context.Request.Form["sochungtu"];

                string ma_iso = db.md_dongtiens.Where(s => s.md_dongtien_id == context.Request.Form["md_dongtien_id"]).Select(s => s.ma_iso).FirstOrDefault();
                if (ma_iso == "VND")
                {
                    mnu.quydoi_vnd = mnu.sotien;
                }
                else
                {
                    mnu.quydoi_vnd = decimal.Parse(context.Request.Form["quydoi_vnd"]);
                }

                // mnu.tk_quy = int.Parse(context.Request.Form["tk_quy"]);
                mnu.tongcackhoan = 0;
                mnu.tongkhoanphi = 0;
                mnu.tienconlai = 0;
                // mnu.md_trangthai_id = context.Request.Form["SOANTHAO"];'
                string md_loaichungtu_id = db.md_loaichungtus.Where(s => s.kieu_doituong == "PT").Select(s => s.md_loaichungtu_id).FirstOrDefault();
                mnu.md_loaichungtu_id = md_loaichungtu_id;
                mnu.ispaymentin = false;
                mnu.md_trangthai_id = "SOANTHAO";

                // mnu.mota = context.Request.Form["mota"];
                mnu.hoatdong = true;
                mnu.nguoitao = UserUtils.getUser(context);
                mnu.nguoicapnhat = UserUtils.getUser(context);
                mnu.ngaytao = DateTime.Now;
                mnu.ngaycapnhat = DateTime.Now;

                db.c_thuchis.InsertOnSubmit(mnu);
                sochungtu.so_duocgan = sochungtu.so_duocgan + sochungtu.buocnhay;
                db.SubmitChanges();
                int i = 0;
                decimal tiendatra=0;
                foreach (var t in chitietcackhoan2)
                {

                    if (t.datra == null)
                    {
                        tiendatra = 0;
                    }
                    else
                    {
                        tiendatra = t.datra.FirstOrDefault();
                    }
                    c_chitietthuchi mnuct = new c_chitietthuchi();
                    mnuct.c_chitietthuchi_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss") + i);
                    mnuct.c_thuchi_id = c_thuchi_id ;
                    mnuct.tk_no = 0;
                    mnuct.tk_co = 0;
                    mnuct.sotien = t.thanhtien - tiendatra;
                    mnuct.quydoi = 0;
                    mnuct.c_donhang_id = t.c_donhang_id;
                    mnuct.c_packinginvoice_id = null;
                    mnuct.obj_code = t.c_nhapxuat_id;
                    mnuct.isdatcoc = false;

                    mnuct.mota = "";
                    mnuct.hoatdong = true;
                    mnuct.ngaytao = DateTime.Now;
                    mnuct.ngaycapnhat = DateTime.Now;
                    mnuct.nguoitao = UserUtils.getUser(context);
                    mnuct.nguoicapnhat = UserUtils.getUser(context);

                    db.c_chitietthuchis.InsertOnSubmit(mnuct);
                    if (i < chitietcackhoan2.Count())
                    {
                        i++;
                    }
                }
                db.SubmitChanges();

            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Không thể thêm giao dịch vào kỳ kế toán đã đóng!");
            }
        }
        catch (Exception e)
        {
            if(e.Message.Contains("Input string was not in a correct format"))
            {
                jqGridHelper.Utils.writeResult(0, "không còn phiếu nhập để có thể chi tự động");
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, e.Message);
            }
        }
    }
    //end nhan-chitudong
    public void ActivePhieuChi(HttpContext context)
    {
        String id = context.Request.QueryString["id"];
        c_thuchi thuchi = db.c_thuchis.FirstOrDefault(p => p.c_thuchi_id.Equals(id));
        decimal sum_detail = (decimal)mdbc.ExecuteScalar("select coalesce(sum(sotien), 0) from c_chitietthuchi where c_thuchi_id = '"+thuchi.c_thuchi_id+"'");
        if (thuchi != null)
        {
            if (thuchi.md_trangthai_id.Equals("SOANTHAO"))
            {
                if (thuchi.sotien.Value >= sum_detail)
                {
                    thuchi.md_trangthai_id = "HIEULUC";
                    db.SubmitChanges();
                    string sql = "select distinct c_packinginvoice_id from c_chitietthuchi where c_thuchi_id = '" + id + "'";
                    DataTable data = mdbc.GetData(sql);
                    foreach (DataRow item in data.Rows)
                    {
                        mdbc.ExcuteNonQuery("update c_packinginvoice set ngaycapnhat = getdate() where c_packinginvoice_id = N'"+item[0].ToString() +"'");
                    }
                    context.Response.Write("Hiệu lực phiếu chi thành công.");
                }
                else
                {
                    context.Response.Write("Số tiền phân bổ ở các dòng không được lớn hơn số tiền trên phiếu chi.");
                }

            }else
            {
                context.Response.Write("Phiếu chi đang ở trạng thái hiệu lực.");
            }
        }else {
            context.Response.Write("Phiếu chi không tồn tại. Có thể đã được xóa trước đó.");
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
        bool hd = false;
        string md_doitackinhdoanh_id= context.Request.Form["md_doitackinhdoanh_id"];
        /*if (h.Equals("on") || h.Equals("true"))
        { hd = true; }*/
        bool check = (bool)mdbc.ExecuteScalarProcedure("p_kiemtragiaodich", "@date", (DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null)).ToString("MM/dd/yyyy")
        ,"@md_doitackinhdoanh_id", md_doitackinhdoanh_id);
        if (check.Equals(true))
        {
            c_thuchi mnu = db.c_thuchis.Single(p => p.c_thuchi_id == context.Request.Form["id"]);
            if (mnu.md_trangthai_id.Equals("SOANTHAO"))
            {
				var dtkdId = context.Request.Form["md_doitackinhdoanh_id"];
				var dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == dtkdId).FirstOrDefault();
                var idVal = new string[] { "", "" };
                if(dtkd != null)
                {
                    idVal[0] = dtkd.md_doitackinhdoanh_id;
                    idVal[1] = dtkd.ma_dtkd;
                }
				
                mnu.md_doitackinhdoanh_id = idVal[0];
				mnu.doitacValue = idVal[1];
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
                jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi phiếu chi đang hiệu lực!");
            }
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể thêm giao dịch vào kỳ kế toán đã đóng!");
        }
    }

    public void add(HttpContext context)
    {
        //try
        {
            bool hd = false;
            string md_doitackinhdoanh_id= context.Request.Form["md_doitackinhdoanh_id"];
            md_sochungtu sochungtu = db.md_sochungtus.Single(s => s.tiento.Equals("PC"));
            string sphieu = sochungtu.tiento + "/" + sochungtu.so_duocgan + "/" + sochungtu.hauto;
            bool check = (bool)mdbc.ExecuteScalarProcedure("p_kiemtragiaodich", "@date", (DateTime.ParseExact(context.Request.Form["ngaylapphieu"], "dd/MM/yyyy", null)).ToString("MM/dd/yyyy")
            ,"@md_doitackinhdoanh_id", md_doitackinhdoanh_id);

            if (check.Equals(true))
            {
                string dtkdId = context.Request.Form["md_doitackinhdoanh_id"];
                var dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == dtkdId).FirstOrDefault();
                var idVal = new string[] { "", "" };
                if(dtkd != null)
                {
                    idVal[0] = dtkd.md_doitackinhdoanh_id;
                    idVal[1] = dtkd.ma_dtkd;
                }

                var mnu = new c_thuchi();
                mnu.c_thuchi_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss"));
                mnu.sophieu = sphieu;
                mnu.md_doitackinhdoanh_id = idVal[0];
                mnu.doitacValue = idVal[1];
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
                mnu.tienconlai = 0;
                mnu.md_trangthai_id = context.Request.Form["SOANTHAO"];
                string md_loaichungtu_id = db.md_loaichungtus.Where(s => s.kieu_doituong == "PT").Select(s => s.md_loaichungtu_id).FirstOrDefault();
                mnu.md_loaichungtu_id = md_loaichungtu_id;
                mnu.ispaymentin = false;
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
            else {
                jqGridHelper.Utils.writeResult(0, "Không thể thêm giao dịch vào kỳ kế toán đã đóng!");
            }
        }
        //catch(Exception e)
        {
            //jqGridHelper.Utils.writeResult(0, e.Message);
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        c_thuchi thuchi = db.c_thuchis.FirstOrDefault(p => p.c_thuchi_id.Equals(id));
        if (thuchi != null)
        {
            if (thuchi.md_trangthai_id.Equals("SOANTHAO"))
            {
                String delThuChi = "delete c_thuchi where c_thuchi_id = @c_thuchi_id";
                String delCT = "delete c_chitietthuchi where c_thuchi_id = @c_thuchi_id";
                mdbc.ExcuteNonQuery(delCT, "@c_thuchi_id", id);
                mdbc.ExcuteNonQuery(delThuChi, "@c_thuchi_id", id);
            }
            else {
                jqGridHelper.Utils.writeResult(0, "Không thể xóa phiếu chi đang hiệu lực.!");
            }
        }
        else {
            jqGridHelper.Utils.writeResult(0, "Phiếu chi không tồn tại. Có thể đã được xóa trước đó.!");
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

        String sqlCount = string.Format(@"
			SELECT COUNT(1) AS count 
			FROM c_thuchi tc
			left join md_doitackinhdoanh dtkd on tc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			left join md_dongtien dt on tc.md_dongtien_id = dt.md_dongtien_id
			left join md_loaichungtu lct on tc.md_loaichungtu_id = lct.md_loaichungtu_id
            WHERE 1=1 AND tc.ispaymentin = 0 {0}", 
			filter
		);
		
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

        string strsql = string.Format(@"
			select P.* 
			from(
				select tc.c_thuchi_id, tc.md_trangthai_id, tc.sophieu,
				dtkd.ma_dtkd, tc.ngaylapphieu, tc.nguoi_giaonop,
				tc.ngay_giaonop, tc.sotien,
				dt.ma_iso, tc.tygia,
				tc.lydo, tc.loaiphieu,
				tc.sochungtu, tc.quydoi_vnd,
				tc.tk_no, tc.tk_co,
				tc.tk_quy, tc.so_dadinhkhoan,
				tc.so_chuadinhkhoan, tc.so_dachiphi, tc.ispaymentin,
				lct.tenchungtu, tc.tongcackhoan, tc.tongkhoanphi, tc.tienconlai,
				tc.ngaytao, tc.nguoitao, tc.ngaycapnhat, tc.nguoicapnhat, tc.mota, tc.hoatdong,
				ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum
				FROM c_thuchi tc
				left join md_doitackinhdoanh dtkd on tc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
				left join md_dongtien dt on tc.md_dongtien_id = dt.md_dongtien_id
				left join md_loaichungtu lct on tc.md_loaichungtu_id = lct.md_loaichungtu_id
				WHERE
				1=1
				AND tc.ispaymentin = 0 {2}
            )P 
			WHERE RowNum > @start AND RowNum < @end", 
			sidx, sord, filter);

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
