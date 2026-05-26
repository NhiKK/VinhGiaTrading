<%@ WebHandler Language="C#" Class="ProductController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using NetServ.Net.Json;
using System.IO;
using System.Collections.Generic;
using jqGridHelper;
using System.Data;


public class ProductController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "updatestatus":
                this.UpdateStatus(context);
                break;
            case "getSinglePicture":
                this.getSinglePicture(context);
                break;
            case "getpicture":
                this.getPicture(context);
                break;
            case "getcombogrid":
                this.getComboGrid(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "getdetail":
                this.getDetail(context);
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

    public void getDetail(HttpContext context)
    {
        string spid = context.Request.Form["sanpham"].ToString();
        LinqDBDataContext db = new LinqDBDataContext();
        var sp = db.md_sanphams.Single(s => s.md_sanpham_id.Equals(spid));
        var donggoisp = db.md_donggoisanphams.Single(dg => dg.md_sanpham_id.Equals(spid) && dg.macdinh.Value);
        var donggoi = db.md_donggois.Single(d => d.md_donggoi_id.Equals(donggoisp.md_donggoi_id));
        //string sql_cm = "select dbo.taoKichThuocSP('" + sp.md_sanpham_id + "')";
        //DataTable d1 = mdbc.GetData(sql_cm);
        //string sql_inch = "select dbo.taoKichThuocSPinch('" + sp.md_sanpham_id + "')";
        //DataTable d2 = mdbc.GetData(sql_inch);
        string sql_kt = "select dbo.taoKichThuocSPCatalog('"+sp.md_sanpham_id+"')";
        DataTable dkt = mdbc.GetData(sql_kt);
        string sql_vol = "select dbo.f_tinhCBMMaHang('" + sp.md_sanpham_id + "')";
        DataTable v = mdbc.GetData(sql_vol);

        //string size_cm = d1.Rows[0][0].ToString();//(string)mdbc.ExecuteScalarProcedure("select dbo.taoKichThuocSP('"+sp.md_sanpham_id+"')");
        //string size_inch = d2.Rows[0][0].ToString();//(string)mdbc.ExecuteScalarProcedure("select dbo.taoKichThuocSPinch('" + sp.md_sanpham_id + "')");
        string kt = dkt.Rows[0][0].ToString();

        decimal vol = 0;//decimal.Parse(v.Rows[0][0].ToString());
        vol = Math.Round((donggoi.l2_mix.Value * donggoi.w2_mix.Value * donggoi.h2_mix.Value)/1000000, 3);//Math.Round(vol * Convert.ToDecimal(3.31466672), 3);
        int index = donggoi.ten_donggoi.IndexOf("/");
        string quantity = donggoi.ten_donggoi.Substring(index + 1, donggoi.ten_donggoi.Length - (index + 1));
        string res = "";
        res += "|ITEM NO.: " + sp.ma_sanpham + "\n";
        res += "|Size: " + kt.Replace("|", " |        ") + "\n";
        res += "|Packing: " + donggoi.ten_donggoi + "\n";
        res += "|Volume: " + vol + " cbm \n";
        if (donggoi.ten_donggoi.Substring(index + 1, donggoi.ten_donggoi.Length - (index + 1)).Equals("pal"))
        {
            res += "|Pallet:" + donggoi.l2_mix + "x" + donggoi.w2_mix + "x" + donggoi.h2_mix + "cm \n";
        }

        res += "|Quantity/40': " + donggoi.soluonggoi_ctn + " " + quantity+ " \n";

        context.Response.Write(res);

    }

    public void UpdateStatus(HttpContext context)
    {
        String msg = "";
        try
        {
            String id = context.Request.QueryString["md_sanpham_id"];
            String trangthai = context.Request.QueryString["trangthai"].ToUpper();
            md_sanpham sp = db.md_sanphams.FirstOrDefault(p => p.md_sanpham_id.Equals(id));

            if (trangthai.Equals("TOSTOP"))
            {
                //// filter
                String filter = "";
                bool _search = bool.Parse(context.Request.QueryString["_search"]);
                if (_search)
                {
                    String _filters = context.Request.QueryString["filters"];
                    Filter f = Filter.CreateFilter(_filters);
                    filter = f.ToScript();
                }

                String sqlGetProductId = @"SELECT sp.md_sanpham_id
                            FROM 
	                            md_sanpham sp left join md_chungloai cl on sp.md_chungloai_id = cl.md_chungloai_id
	                            left join md_doitackinhdoanh ncc on sp.nhacungung = ncc.md_doitackinhdoanh_id
	                            left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id
	                            left join md_nhomnangluc nnl on sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
	                            left join md_cangbien cb on sp.md_cangbien_id = cb.md_cangbien_id
	                            left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
	                            left join md_kieudang kd on sp.md_kieudang_id = kd.md_kieudang_id
                                left join md_hscode hscode on sp.md_hscode_id = hscode.md_hscode_id
                            WHERE 
	                            1=1 " +
                   " {0} " +
                   " AND sp.hoatdong = 1 AND sp.trangthai not in ('NHD', 'HHT')";

                sqlGetProductId = String.Format(sqlGetProductId, filter);

                String sqlUpdateToStop = "update md_sanpham set trangthai = 'NHD', ngayngunghd = getdate() where md_sanpham_id IN(" + sqlGetProductId + ")";

                int s = mdbc.ExcuteNonQuery(sqlUpdateToStop);
                msg = "Đã chuyển {0} sản phẩm trên lưới sang ngưng hoạt động!";
                msg = String.Format(msg, s);
            }
            else
            {
                if (sp != null)
                {
                    bool next = true;
                    switch (trangthai)
                    {
                        case "DHD":
                            msg = UserUtils.checkHieuLucProduct(context, sp, db);
                            next = string.IsNullOrEmpty(msg) ? true : false;
                            msg = string.IsNullOrEmpty(msg) ? "Đã chuyển trạng thái sản phẩm thành Đang hoạt động!" : msg;
                            break;
                        case "NHD":
                            msg = "Đã chuyển trạng thái sản phẩm thành Ngưng hoạt động!";
                            break;
                        case "MOI":
                            msg = "Đã chuyển trạng thái sản phẩm thành Hàng mới!";
                            break;
                        case "SOANTHAO":
                            msg = "Đã chuyển trạng thái sản phẩm thành Soạn thảo!";
                            break;
                        case "TOACTIVE":
                            msg = "Đã chuyển tất cả sản phẩm soạn thảo sang hoạt động!";
                            break;
                        default:
                            msg = "Không xác định được trạng thái của sản phẩm!";
                            next = false;
                            break;
                    }

                    if (next)
                    {
                        if (trangthai.Equals("TOACTIVE"))
                        {
                            String update = "update md_sanpham set trangthai = 'DHD' where trangthai='SOANTHAO'";
                            mdbc.ExcuteNonQuery(update);
                        }
                        else
                        {
                            if(trangthai == "NHD")
                                sp.ngayngunghd = DateTime.Now;
                            sp.trangthai = trangthai;
                            db.SubmitChanges();
                        }
                    }
                }
                else
                {
                    msg = "Chưa chọn Mã hàng hoặc mã hàng không tồn tại!";
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        context.Response.Write(msg);
    }

    public void getSinglePicture(HttpContext context)
    {
        String ma_sanpham = context.Request.QueryString["ma_sanpham"];
        String selImage="";
        try
        {
            if (ma_sanpham.ElementAt(9).ToString().ToLower().Equals("f"))
            {
                selImage = String.Format("select url from md_anhsanpham where filter = N'{0}' ", ma_sanpham.Substring(0, 11));
            }
            else
            {
                selImage = String.Format("select url from md_anhsanpham where filter = N'{0}' ", ma_sanpham.Substring(0, 8));
            }
        }
        catch
        {
            selImage = String.Format("select url from md_anhsanpham where filter = N'{0}' ", ma_sanpham.Substring(0, 8));
        }
        String img = (String)mdbc.ExecuteScalar(selImage);
        if (img != null)
        {
            context.Response.Write("images/products/fullsize/" + img);
        }
        else {
            context.Response.Write("images/products/fullsize/default.jpg");
        }
    }

    public void getPicture(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        String md_sanpham_id = context.Request.QueryString["productId"];
        String ma_sanpham = "";

        String getMaSP = "select ma_sanpham from md_sanpham where md_sanpham_id = @md_sanpham_id  ";
        ma_sanpham = (String)mdbc.ExecuteScalar(getMaSP, "@md_sanpham_id", md_sanpham_id);

        String result = "";

        String getImage = "";

        try
        {
            if (ma_sanpham.ElementAt(9).ToString().ToLower().Equals("f"))
            {
                getImage = String.Format("select url from md_anhsanpham where filter LIKE N'{0}%' ", ma_sanpham.Substring(0, 11));
            }
            else
            {
                getImage = String.Format("select url from md_anhsanpham where filter LIKE N'{0}%' ", ma_sanpham.Substring(0, 8));
            }
        }
        catch
        {
            getImage = String.Format("select url from md_anhsanpham where filter LIKE N'{0}%' ", ma_sanpham.Substring(0, 8));
        }

        System.Data.SqlClient.SqlDataReader rd = mdbc.ExecuteReader(getImage);
        if (rd.HasRows)
        {
            result += "[";
            int i = 0;
            while (rd.Read())
            {
                String img = rd[0].ToString();
                if (i > 0)
                {
                    result += ",{";
                    result += String.Format("\"{0}\" : \"images/products/fullsize/{1}\", \"{2}\" : \"{3}\"", "href", img.Replace(" ", ""), "title", img.Substring(0,img.LastIndexOf(".")));
                    result += "}";
                }
                else
                {
                    result += "{";
                    result += String.Format("\"{0}\" : \"images/products/fullsize/{1}\", \"{2}\" : \"{3}\"", "href", img.Replace(" ", ""), "title", img.Substring(0,img.LastIndexOf(".")));
                    result += "}";
                }
                i++;
            }
            result += "]";
        }
        else {
            result += "[{\"href\":\"\", \"title\":\"\"}]";
        }
        rd.Close();
        context.Response.Write(result);
        //context.Response.Write("[{ \"href\" : \"http://fancyapps.com/fancybox/demo/1_b.jpg\",  \"title\" : \"1st title\"},{ \"href\" : \"http://fancyapps.com/fancybox/demo/2_b.jpg\",\"title\" : \"2nd title\"},{ \"href\" : \"http://fancyapps.com/fancybox/demo/3_b.jpg\", \"title\" : \"3rd title\"}]");
    }

    public void getComboGrid(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string soanthao = context.Request.QueryString["soanthao"];
        soanthao = "1";
        if (soanthao != "" & soanthao != null)
        {
            soanthao = "and trangthai != 'SOANTHAO'";
        }
        else
        {
            soanthao = "";
        }
        String searchTerm = context.Request.QueryString["searchTerm"];
        if (searchTerm == "")
        {
            searchTerm = "%";
        }
        else
        {
            searchTerm = String.Format("{0}%", searchTerm);
        }

        String sqlCount = String.Format("SELECT COUNT(*) AS count FROM md_sanpham WHERE ma_sanpham like '{0}'  AND trangthai != 'NHD' {1}", searchTerm, soanthao);

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
            sidx = "ma_sanpham";
        }

        String sql = "";
        if (total_page != 0)
        {
            sql = String.Format("select * from( SELECT *, ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum FROM md_sanpham WHERE ma_sanpham like '{0}' AND trangthai != 'NHD' {1} )P where RowNum > @start AND RowNum < @end", searchTerm, soanthao);
        }
        else
        {
            sql = String.Format("select * from( SELECT *, ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum FROM md_sanpham WHERE ma_sanpham like '{0}'  AND trangthai != 'NHD' {1} )P", searchTerm, soanthao);
        }

        var dt = mdbc.GetData(sql, "@start", start, "@end", end);
        var jsonNew = new Dictionary<string, object>();
        jsonNew["page"] = page;
        jsonNew["total"] = total_page;
        jsonNew["records"] = count;
        var items = new List<Dictionary<string, object>>();

        foreach (DataRow row in dt.Rows)
        {
            var item = new Dictionary<string, object>();
            item["md_sanpham_id"] = row["md_sanpham_id"];
            item["ma_sanpham"] = row["ma_sanpham"];
            item["mota_tiengviet"] = row["mota_tiengviet"];
            items.Add(item);
        }

        if (items.Count > 0)
            jsonNew["rows"] = items;
        else
            jsonNew["rows"] = null;

        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(jsonNew));
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select md_sanpham_id, mota_tiengviet from md_sanpham where hoatdong = 1 and trangthai != 'NHD' ";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }

    public void edit(HttpContext context)
    {
        try
        {
            String h;
            string nxn = context.Request.Form["ngayxacnhan"];
            string ten_danhmuc = context.Request.Form["ten_danhmuc"];
            h = context.Request.Form["hoatdong"].ToLower();

            bool hd;
            hd = false;

            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }

            decimal l_, w_, h_;
            l_ = decimal.Parse(context.Request.Form["l_cm"]);
            w_ = decimal.Parse(context.Request.Form["w_cm"]);
            h_ = decimal.Parse(context.Request.Form["h_cm"]);

            md_sanpham m = db.md_sanphams.Single(p => p.md_sanpham_id == context.Request.Form["id"]);
            m.ma_sanpham = context.Request.Form["ma_sanpham"];
            //m.ma_sanphamcu = context.Request.Form["ma_sanphamcu"];
            //m.mota_tiengviet = context.Request.Form["mota_tiengviet"];
            //m.mota_tienganh = context.Request.Form["mota_tienganh"];
            m.md_chucnang_id = context.Request.Form["md_chucnang_id"];
            m.md_kieudang_id = context.Request.Form["md_kieudang_id"];
            //m.chucnang_tiengviet = context.Request.Form["chucnang_tiengviet"];
            //m.chucnang_tienganh = context.Request.Form["chucnang_tienganh"];
            //m.ma_vach = context.Request.Form["ma_vach"];
            m.md_donvitinhsanpham_id = context.Request.Form["tendvt"];
            m.l_inch = l_ == 0 ? 0 : decimal.Multiply(l_, Convert.ToDecimal(0.393700787));
            m.w_inch = w_ == 0 ? 0 : decimal.Multiply(w_, Convert.ToDecimal(0.393700787));
            m.h_inch = h_ == 0 ? 0 : decimal.Multiply(h_, Convert.ToDecimal(0.393700787));
            m.l_cm = l_;
            m.w_cm = w_;
            m.h_cm = h_;
            if(!string.IsNullOrEmpty(nxn))
                m.ngayxacnhan = DateTime.ParseExact(nxn, "dd/MM/yyyy", null);
            else
                m.ngayxacnhan = null;

            m.md_tinhtranghanghoa_id = ten_danhmuc;
            m.hinhthucban = context.Request.Form["hinhthucban"];
            m.trongluong = decimal.Parse(context.Request.Form["trongluong"]);
            m.dientich = decimal.Parse(context.Request.Form["dientich"]);
            m.md_nhomnangluc_id = context.Request.Form["md_nhomnangluc_id"];
            //m.cmp_master = decimal.Parse(context.Request.Form["cmp_master"]);
            //m.cmp_park = decimal.Parse(context.Request.Form["cmp_park"]);
            //m.sanpham_docquyen = docquyen;
            m.md_chungloai_id = context.Request.Form["md_chungloai_id"];
            m.ghichu = context.Request.Form["ghichu"];
            m.nhacungung = context.Request.Form["tenncc"];
            m.mota = context.Request.Form["mota"];


            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;
            int check = (from sp in db.md_sanphams
                         where sp.ma_sanpham.Equals(context.Request.Form["ma_sanpham"]) && !sp.md_sanpham_id.Equals(context.Request.Form["id"])
                         select new { sp.ma_sanpham }).Count();
            if (check == 0)
            {
                db.SubmitChanges();
                jqGridHelper.Utils.writeResult(1, "Cập nhật thành công!");
            }
            else {
                jqGridHelper.Utils.writeResult(0, "Mã hàng đã tồn tại!");
            }
        }catch(Exception ex){
            jqGridHelper.Utils.writeResult(0, ex.Message);
        }
    }

    public void add(HttpContext context)
    {
        try
        {
            String h;
            h = context.Request.Form["hoatdong"].ToLower();
            string ten_danhmuc = context.Request.Form["ten_danhmuc"];

            bool hd;
            hd = false;

            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }

            decimal l_, w_, h_;
            l_ = decimal.Parse(context.Request.Form["l_cm"]);
            w_ = decimal.Parse(context.Request.Form["w_cm"]);
            h_ = decimal.Parse(context.Request.Form["h_cm"]);
            string nxn = context.Request.Form["ngayxacnhan"];
            md_sanpham mnu = new md_sanpham
            {
                md_sanpham_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                ma_sanpham = context.Request.Form["ma_sanpham"],
                //ma_sanphamcu = context.Request.Form["ma_sanphamcu"],
                //mota_tiengviet = context.Request.Form["mota_tiengviet"],
                //mota_tienganh = context.Request.Form["mota_tienganh"],

                //ma_vach = context.Request.Form["ma_vach"],
                md_donvitinhsanpham_id = context.Request.Form["tendvt"],
                l_inch = l_ == 0 ? 0 : decimal.Multiply(l_, Convert.ToDecimal(0.393700787)),
                w_inch = w_ == 0 ? 0 : decimal.Multiply(w_, Convert.ToDecimal(0.393700787)),
                h_inch = h_ == 0 ? 0 : decimal.Multiply(h_, Convert.ToDecimal(0.393700787)),
                l_cm = l_,
                w_cm = w_,
                h_cm = h_,
                trongluong = decimal.Parse(context.Request.Form["trongluong"]),
                dientich = decimal.Parse(context.Request.Form["dientich"]),
                md_nhomnangluc_id = context.Request.Form["md_nhomnangluc_id"],

                //sanpham_docquyen = docquyen,
                md_chungloai_id = context.Request.Form["md_chungloai_id"],
                md_cangbien_id = context.Request.Form["md_cangbien_id"],
                nhacungung = context.Request.Form["tenncc"],
                ghichu = context.Request.Form["ghichu"],
                trangthai = "SOANTHAO",
                vattu = false,
                ban_thanhpham = false,
                sanpham = true,
                md_tinhtranghanghoa_id = ten_danhmuc,
                hinhthucban = context.Request.Form["hinhthucban"],
                mota = context.Request.Form["mota"],
                hoatdong = hd,
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };
            if(!string.IsNullOrEmpty(nxn))
                mnu.ngayxacnhan = DateTime.ParseExact(nxn, "dd/MM/yyyy", null);
            int check = (from sp in db.md_sanphams
                         where sp.ma_sanpham.Equals(context.Request.Form["ma_sanpham"])
                         select new { sp.ma_sanpham}).Count();
            if (check == 0)
            {
                db.md_sanphams.InsertOnSubmit(mnu);
                db.SubmitChanges();
                jqGridHelper.Utils.writeResult(1, "Thêm mới thành công!");
            }
            else {
                jqGridHelper.Utils.writeResult(0, "Đã tồn tại mã hàng này!");
            }

        }catch(Exception ex){
            jqGridHelper.Utils.writeResult(0, ex.Message);
        }
    }

    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        md_sanpham sp = db.md_sanphams.FirstOrDefault(p => p.md_sanpham_id.Equals(id));
        if (sp == null)
        {
            if (sp.trangthai.Equals("SOANTHAO"))
            {
                db.md_sanphams.DeleteOnSubmit(sp);
                db.SubmitChanges();
                jqGridHelper.Utils.writeResult(1, "Đã xóa sản phẩm!");
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Xóa sản phẩm thất bại! Chỉ có thể xóa sản phẩm khi đang soạn thảo!");
            }
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không tìm thấy sản phẩm, có thể đã được xóa trước đó!");
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
            Filter f = Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }


        String sqlCount = string.Format(@"
            SELECT COUNT(1) AS count 
            FROM 
	            md_sanpham sp 
                left join md_chungloai cl on sp.md_chungloai_id = cl.md_chungloai_id
	            left join md_doitackinhdoanh ncc on sp.nhacungung = ncc.md_doitackinhdoanh_id
	            left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id
	            left join md_nhomnangluc nnl on sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
	            left join md_cangbien cb on sp.md_cangbien_id = cb.md_cangbien_id
	            left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
	            left join md_kieudang kd on sp.md_kieudang_id = kd.md_kieudang_id
                left join md_hscode hscode on sp.md_hscode_id = hscode.md_hscode_id
                left join md_danhmuchanghoa tt on sp.md_tinhtranghanghoa_id = tt.md_danhmuchanghoa_id
            WHERE 
	            1=1
                {0}
                AND sp.hoatdong = 1 AND sp.trangthai not in ('NHD', 'HHT', 'SOANTHAO')"
            , filter
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
            sidx = "sp.ma_sanpham";
        }

        string strsql = string.Format(@"
            select * from( 
                SELECT
	                sp.md_sanpham_id, sp.trangthai, sp.ma_sanpham
	                , kd.ten_tv as ten_kd
	                , kd.ma_kieudang as ma_kd
	                , cn.ten_tv as ten_cn
	                , cn.ma_chucnang as ma_cn
	                , dvt.ten_dvt as tendvt
	                , sp.mota_tiengviet,  sp.mota_tienganh
	                , sp.L_inch, sp.W_inch, sp.H_inch, sp.L_cm, sp.W_cm, sp.H_cm, sp.Trongluong, sp.dientich, sp.thetich
	                , (nnl.hehang + '('+ nnl.nhom +')') as nangluc
	                , cl.code_cl, sp.ghichu, ncc.ma_dtkd
	                , cb.ten_cangbien as cangbien, hscode.ma_hscode, hscode.hscode, sp.chucnangsp, sp.ngayxacnhan, tt.ten_danhmuc, sp.hinhthucban
	                , sp.ngaytao, sp.nguoitao, sp.ngaycapnhat, sp.nguoicapnhat, sp.mota, sp.hoatdong
	                , ROW_NUMBER() OVER (ORDER BY {0} {1} ) as RowNum 
                FROM 
	                md_sanpham sp 
                    left join md_chungloai cl on sp.md_chungloai_id = cl.md_chungloai_id
	                left join md_doitackinhdoanh ncc on sp.nhacungung = ncc.md_doitackinhdoanh_id
	                left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id
	                left join md_nhomnangluc nnl on sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
	                left join md_cangbien cb on sp.md_cangbien_id = cb.md_cangbien_id
	                left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
	                left join md_kieudang kd on sp.md_kieudang_id = kd.md_kieudang_id                     
                    left join md_hscode hscode on sp.md_hscode_id = hscode.md_hscode_id	    
                    left join md_danhmuchanghoa tt on sp.md_tinhtranghanghoa_id = tt.md_danhmuchanghoa_id
                WHERE 
	                1=1 {2}
                    AND sp.hoatdong = 1 
                    AND sp.trangthai not in ('NHD', 'HHT', 'SOANTHAO')
            )P WHERE RowNum > @start AND RowNum < @end"
            , sidx
            , sord
            , filter
        );

        var dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_sanpham_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_sanpham_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_sanpham"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_kd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_kd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_cn"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_cn"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota_tiengviet"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota_tienganh"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tendvt"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["L_inch"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["W_inch"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["H_inch"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["L_cm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["W_cm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["H_cm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trongluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["dientich"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["thetich"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nangluc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["code_cl"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cangbien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_hscode"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hscode"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chucnangsp"] + "]]></cell>";

            var nxn = row["ngayxacnhan"] + "";
            xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", string.IsNullOrEmpty(nxn) ? "" : DateTime.Parse(nxn).ToString("dd/MM/yyyy"));

            xml += "<cell><![CDATA[" + row["ten_danhmuc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hinhthucban"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghichu"] + "]]></cell>";

            var ngaytao = row["ngaytao"] + "";
            xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", string.IsNullOrEmpty(ngaytao) ? "" : DateTime.Parse(ngaytao).ToString("dd/MM/yyyy"));

            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";

            var ngaycapnhat = row["ngaycapnhat"] + "";
            xml += string.Format(@"<cell><![CDATA[{0}]]></cell>", string.IsNullOrEmpty(ngaycapnhat) ? "" : DateTime.Parse(ngaycapnhat).ToString("dd/MM/yyyy"));

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
