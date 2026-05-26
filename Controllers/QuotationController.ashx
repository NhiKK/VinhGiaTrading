<%@ WebHandler Language="C#" Class="QuotationController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public class QuotationController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "createcolor":
                this.createColor(context);
                break;
            case "startsendmail":
                this.startSendMail(context);
                break;
            case "sendmail":
                this.sendMail(context);
                break;
            case "createpo":
                this.createPO(context);
                break;
            case "activeqo":
                this.activeQO(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "getSelectOption_cb":
                this.getSelectOption_cb(context);
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

    #region action
    public void getSelectOption_cb(HttpContext context)
    {
        String sql = "select md_cangbien_id, ten_cangbien from md_cangbien where hoatdong = 1 and ma_cangbien != 'HPP' order by ten_cangbien asc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToOptionNullFirst());
    }

    public void createColor(HttpContext context)
    {
        //try
        {
            var qoId = context.Request.QueryString["qoId"];
            var bg = db.c_baogias.Where(p => p.c_baogia_id.Equals(qoId)).FirstOrDefault();
            if (bg != null)
            {
                var ctbgSpIds = db.c_chitietbaogias.Where(s => s.c_baogia_id == bg.c_baogia_id).Select(s => s.md_sanpham_id).ToList();
                var sps = db.md_sanphams.Where(s =>
                    ctbgSpIds.Contains(s.md_sanpham_id)
                ).Select(s => new { s.ma_sanpham }).ToList();

                db.md_color_references.DeleteAllOnSubmit(db.md_color_references.Where(s => s.c_baogia_id == bg.c_baogia_id));

                foreach (var sp in sps)
                {
                    string cl = sp.ma_sanpham.Substring(0, 2);
                    string dt = sp.ma_sanpham.Substring(6, 2);
                    string ms = sp.ma_sanpham.Substring(12, 2);

                    string filter = cl + "-" + dt + "-" + ms;
                    var mausacs = db.md_mausacs.Where(s =>
                        s.code_cl == cl & s.trangthai == "HIEULUC"
                    ).ToList();

                    foreach (var mausac in mausacs)
                    {
                        var filterMS = mausac.code_cl + "-" + mausac.code_dt + "-" + mausac.code_mau;
                        var url = filterMS + ".jpg";
                        var chk = db.md_color_references.Where(s => s.c_baogia_id == bg.c_baogia_id & s.filter == filterMS).FirstOrDefault();

                        var macdinh = filterMS == filter ? true : false;
                        if (chk == null)
                        {
                            chk = new md_color_reference
                            {
                                md_color_reference_id = Guid.NewGuid().ToString().Replace("-", ""),
                                c_baogia_id = bg.c_baogia_id,
                                filter = filterMS,
                                mau = mausac.ta_ngan,
                                hoatdong = macdinh,
                                mota = "",
                                ngaycapnhat = DateTime.Now,
                                ngaytao = DateTime.Now,
                                nguoicapnhat = "admin",
                                nguoitao = "admin",
                                url = url
                            };
                            db.md_color_references.InsertOnSubmit(chk);
                        }
                        else
                        {
                            chk.hoatdong = chk.hoatdong == true ? true : macdinh;
                            chk.ngaycapnhat = DateTime.Now;
                            chk.nguoicapnhat = "admin";
                            chk.url = url;
                        }
                    }
                    db.SubmitChanges();
                }

                //        string sql = string.Format("select ma_sanpham from md_sanpham sp left join c_chitietbaogia ctbg on ctbg.md_sanpham_id = sp.md_sanpham_id where sp.md_sanpham_id = ctbg.md_sanpham_id and ctbg.c_baogia_id = '{0}'", qoId);
                //        DataTable dtSP = mdbc.GetData(sql);
                //        foreach (DataRow sp in dtSP.Rows)
                //        {
                //            string color_rf = sp[0].ToString().Substring(0, 2);
                //            mm = color_rf;
                //            string sqlColor = string.Format(@"select distinct
                //	ta_ngan, url, filter, code_cl 
                //from (
                //	select ms.ta_ngan, tmp.url, tmp.filter, tmp.code_cl
                //	from(
                //		select url, substring(filter, 1, 5) as filter,  SUBSTRING(filter, 1, 2) as code_cl, SUBSTRING(filter, 4, 2) as code_mau
                //		from md_anhsanpham where url like '%.jp%'
                //	) as tmp, md_mausac ms
                //	where tmp.code_cl = ms.code_cl
                //	and tmp.code_mau = ms.code_mau
                //) as re
                //                        where re.code_cl = '{0}'", color_rf);
                //            DataTable dtColor = mdbc.GetData(sqlColor);
                //            foreach (DataRow c in dtColor.Rows)
                //            {
                //                string sqlFilter = string.Format("select count(filter) from md_color_reference where c_baogia_id = '{0}' and filter = '{1}'", qoId, c["filter"]);
                //                int filterCount = (int)mdbc.ExecuteScalar(sqlFilter);
                //                if (filterCount == 0)
                //                {
                //                    string filter = sp[0].ToString().Substring(0, 2) + "-" + sp[0].ToString().Substring(sp[0].ToString().LastIndexOf('-') + 1, 2);
                //                    string sqlInsert = string.Format(@"INSERT INTO [dbo].[md_color_reference]
                //                                   ([md_color_reference_id],[c_baogia_id],[mau],[url],[filter]
                //                                   ,[ngaytao],[nguoitao],[ngaycapnhat],[nguoicapnhat]
                //                                   ,[mota],[hoatdong])
                //                                VALUES
                //                                   (REPLACE(NEWID(),'-',''),'{0}', '{1}', '{2}', '{3}'
                //                                   ,getdate(),'admin',getdate(),'admin','',{4})", qoId, c["ta_ngan"], c["url"], c["filter"], 0);
                //                    mdbc.ExcuteNonQuery(sqlInsert);

                //                }
                //            }
                //            string selFilter = string.Format("select (substring(sp.ma_sanpham, 1, 2) + '-' + substring(sp.ma_sanpham, len(sp.ma_sanpham)-1, 2)) from md_sanpham sp, c_chitietbaogia ctbg where sp.md_sanpham_id = ctbg.md_sanpham_id and ctbg.c_baogia_id = '{0}'", qoId);
                //            string updateDetault = string.Format("update md_color_reference set hoatdong = 1 where c_baogia_id = '{0}' and filter IN(" + selFilter + ")", qoId);
                //            mdbc.ExcuteNonQuery(updateDetault);
                //        }
                context.Response.Write("Tạo Color References cho Quotation thành công!");
            }
        }
        //catch (Exception ex)
        {
            //context.Response.Write("Lỗi: " + ex.Message);
        }
    }

    public void startSendMail(HttpContext context)
    {
        String qoId = context.Request.QueryString["qoId"];
        String manv = UserUtils.getUser(context);
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
        //Lấy báo giá
        c_baogia baogia = db.c_baogias.FirstOrDefault(p => p.c_baogia_id.Equals(qoId));
        // Lấy thông tin đối tác
        md_doitackinhdoanh doitac = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(baogia.md_doitackinhdoanh_id));
        context.Response.Write(String.Format("<div>Gửi từ: {0}</div><div>Gửi đến: {1}</div><div>Cc: {2}</div>", nv.email, doitac.email, nv.email_cc));
    }

    public void sendMail(HttpContext context)
    {

        String manv = UserUtils.getUser(context);
        String fileName = manv + "BaoGia-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + ".xls";
        String fileNamePdf = manv + "BaoGia-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + ".pdf";
        String path = context.Server.MapPath("~/FileSendMail/");
        String logoPath = context.Server.MapPath("~/images/VINHGIA_logo_print.png");
        String productImagePath = context.Server.MapPath("~/images/products/fullsize/");
        MemoryStream ms = new MemoryStream();

        String qoId = context.Request.QueryString["qoId"];

        //Lấy thông tin nhân viên
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));

        // Lấy mail template c?a báo giá
        md_mailtemplate tmp = db.md_mailtemplates.Single(p => p.use_for.Equals("QO") && p.default_mail.Equals(true));

        //Lấy báo giá
        c_baogia baogia = db.c_baogias.FirstOrDefault(p => p.c_baogia_id.Equals(qoId));
        // Lấy thông tin đối tác
        md_doitackinhdoanh doitac = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(baogia.md_doitackinhdoanh_id));

        // Lấy thông tin smtp
        md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));

        bool next = true;
        String msg = "";
        if (nv == null)
        {
            next = false;
            msg += "<div>Không tìm thấy thông tin nhân viên!</div>";
        }

        if (tmp == null)
        {
            next = false;
            msg += "<div>Không có Template Email!</div>";
        }

        if (baogia == null)
        {
            next = false;
            msg += "<div>Không tìm thấy báo giá!</div>";
        }

        if (doitac == null)
        {
            next = false;
            msg += "<div>Không tìm thấy đối tác!</div>";
        }

        if (smtp == null)
        {
            next = false;
            msg += "<div>Không tìm thấy smtp!</div>";
        }

        try
        {
            if (baogia.isform.Equals(false))
            {
                BaoGiaReport rptQO = new BaoGiaReport(qoId, logoPath, productImagePath);
                NPOI.HSSF.UserModel.HSSFWorkbook wb = rptQO.CreateWBQuotation();
                wb.Write(ms);
                File.WriteAllBytes(path + fileName, ms.ToArray());
                ms.Close();
            }
            else
            {
                BaoGiaMauReport rptQO = new BaoGiaMauReport(qoId, logoPath, productImagePath);
                NPOI.HSSF.UserModel.HSSFWorkbook wb = rptQO.CreateWBQuotation();
                wb.Write(ms);
                File.WriteAllBytes(path + fileName, ms.ToArray());
                ms.Close();
            }
        }
        catch (Exception ex)
        {
            next = false;
            context.Response.Write(ex.Message);
        }

        try
        {
            BaoGiaPdf baogiaPdf = new BaoGiaPdf(qoId, logoPath, productImagePath);
            baogiaPdf.CreatePdfQuotation(path + fileNamePdf);
        }
        catch (Exception ex)
        {
            next = false;
            context.Response.Write(ex.Message);
        }

        if (next)
        {
            try
            {
                GoogleMail mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value, smtp.use_ssl.GetValueOrDefault(false));
                mail.Send(nv.email, doitac.email, nv.email_cc, "", tmp.subject_mail, tmp.content_mail.Replace("%0D", "<br/>"), path + fileName + ";" + path + fileNamePdf);
                //File.Delete(path + fileName);
                context.Response.Write(String.Format("Ðã gửi mail thành công đến:<br/>{0}<br/>{1}<br/>", nv.email_cc, doitac.email));
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }
    }

    public void createPO(HttpContext context)
    {
        string qoId = context.Request.QueryString["qoId"];
        string option = context.Request.QueryString["option"];
        string optionType = context.Request.QueryString["optionType"];
        //--
        c_baogia bg = db.c_baogias.Single(p => p.c_baogia_id.Equals(qoId));
        //--
        string sct = "", msg = "";
        bool isform = true;

        if (bg.md_trangthai_id.Equals("HIEULUC"))
        {
            //try
            {
                String md_paymentterm_id = db.md_paymentterms.Take(1).FirstOrDefault().md_paymentterm_id;
                int count_qotopo = (from c in db.c_donhangs where c.c_baogia_id.Equals(qoId) select new { c.sochungtu }).Count();

                //-- Dem cang bien
                string countQO = "select count(distinct md_cangbien_id) from c_chitietbaogia where c_baogia_id = @c_baogia_id";
                int count = (int)mdbc.ExecuteScalar(countQO, "@c_baogia_id", qoId);
                //--
                if (count_qotopo > 0)
                {
                    msg = "Lỗi:Không thể tạo vì P/O tươnng ứng đã được tạo truớc đó!";
                }
                else if (bg.isform == true & option == "0")
                {
                    msg = "Lỗi:Không thể tạo vì " + bg.sobaogia + " là Q/O mẫu!";
                }
                else if (option == "1" & optionType == "por")
                {
                    msg = "";
                }
                else if ((option == "1" || option == "0") & count > 1)
                {
                    msg = "Lỗi:Không thể tạo vì Q/O có 2 cảng biển!";
                }

                if (msg == "")
                {
                    //1. Q/O BT -> P/O BT , P/O SR
                    //2. Q/O SR -> P/O BT , P/O SR
                    //3. Q.O SR 2CB -> P/O SR hoac 2 P/O 2 CB
                    switch (option)
                    {
                        case "0":
                            count = 1;
                            break;
                        case "1":
                            count = 1;
                            break;
                        case "2":
                            if (optionType == "po")
                                count = 2;
                            else
                                count = 1;
                            break;
                        default:
                            count = 1;
                            break;
                    }

                    if (optionType == "po")
                        isform = false;
                    else
                        isform = true;
                    //-- Lay cang bien
                    string db_fr = "c_baogia";
                    if (count == 2)
                        db_fr = "c_chitietbaogia";
                    string strsql = "select distinct md_cangbien_id from " + db_fr + " where c_baogia_id = @c_baogia_id";
                    System.Data.DataTable dt = mdbc.GetData(strsql, "@c_baogia_id", qoId);

                    md_nganhang ng = db.md_nganhangs.Take(1).FirstOrDefault();
                    decimal muchoahong = 0;

                    md_nguoilienhe nguoilienhe = db.md_nguoilienhes.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(bg.md_doitackinhdoanh_id));
                    if (nguoilienhe != null)
                    {
                        muchoahong = nguoilienhe.muchoahong == null ? 0 : nguoilienhe.muchoahong.Value;
                    }

                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        string id = ImportUtils.getNEWID();
                        string sochungtu = jqGridHelper.Utils.taoChungTuPO(bg.md_doitackinhdoanh_id, isform == true ? 1 : 0, DateTime.Now.Year.ToString());
                        //--
                        c_donhang dh = new c_donhang();
                        dh.c_donhang_id = id;
                        dh.sochungtu = sochungtu;
                        dh.c_baogia_id = bg.c_baogia_id;

                        dh.donhang_mau = isform;

                        dh.md_doitackinhdoanh_id = bg.md_doitackinhdoanh_id;
                        dh.ngaylap = DateTime.Now;
                        dh.nguoilap = UserUtils.getUser(context);

                        dh.md_cangbien_id = row["md_cangbien_id"].ToString();

                        dh.discount = 0;
                        dh.shipmentdate = DateTime.Now;
                        dh.shipmenttime = DateTime.Now;
                        dh.md_paymentterm_id = md_paymentterm_id;
                        dh.md_trongluong_id = bg.md_trongluong_id;
                        dh.md_kichthuoc_id = bg.md_kichthuoc_id;
                        dh.payer = "";
                        dh.isdonhangtmp = false;

                        dh.cont20 = 0;
                        dh.cont40 = 0;
                        dh.cont40hc = 0;
                        dh.cont45 = 0;
                        dh.contle = 0;

                        if (nguoilienhe != null)
                        {
                            dh.md_nguoilienhe_id = nguoilienhe.doitaclienquan;
                        }

                        dh.hoahong = muchoahong;
                        dh.portdischarge = "";
                        dh.amount = bg.totalquo;
                        dh.totalcbm = bg.totalcbm;
                        dh.totalcbf = bg.totalcbf;
                        dh.ismakepi = false;
                        dh.md_trangthai_id = "SOANTHAO";
                        dh.md_dongtien_id = bg.md_dongtien_id;
                        dh.md_nganhang_id = ng.md_nganhang_id;
                        dh.ngaytao = DateTime.Now;
                        dh.nguoitao = UserUtils.getUser(context);
                        dh.ngaycapnhat = DateTime.Now;
                        dh.nguoicapnhat = UserUtils.getUser(context);
                        dh.hoatdong = true;
                        dh.ghichu = bg.mota;
                        dh.gia_db = bg.gia_db;
                        int sothutu = 10;

                        //--
                        sct += sochungtu + ",";
                        //Tạo dòng đơn hàng
                        var lstQOD = (from p in db.c_chitietbaogias
                                      where p.c_baogia_id.Equals(bg.c_baogia_id)
                                      select p);
                        //--2 cang bien
                        if (count == 2)
                            lstQOD = (from p in db.c_chitietbaogias
                                      where p.c_baogia_id.Equals(bg.c_baogia_id) && p.md_cangbien_id.Equals(row["md_cangbien_id"].ToString())
                                      select p);

                        System.Collections.Generic.List<c_dongdonhang> lstPOD = new System.Collections.Generic.List<c_dongdonhang>();

                        // Chạy qua từng dòng QO tạo dòng PO
                        foreach (var item in lstQOD)
                        {
                            var dgdh = db.md_donggois.Where(s => s.md_donggoi_id == item.md_donggoi_id).FirstOrDefault();
                            c_dongdonhang ddh = new c_dongdonhang();
                            ddh.c_dongdonhang_id = ImportUtils.getNEWID();
                            ddh.c_donhang_id = id;
                            ddh.md_sanpham_id = item.md_sanpham_id;
                            ddh.ma_sanpham_khach = item.ma_sanpham_khach;
                            ddh.mota_tiengviet = item.mota_tiengviet;
                            ddh.mota_tienganh = item.mota_tienganh;
                            ddh.md_donggoi_id = item.md_donggoi_id;

                            if (dgdh != null)
                            {
                                ddh.chiphidonggoi = dgdh.cpdg_vuotchuan;
                                ddh.ghichudonggoi = dgdh.mota;
                                ddh.ghichudonggoingoai = dgdh.mota;
                            }

                            ddh.giafob = item.giafob;
                            ddh.soluong = item.soluong;
                            // ddh.soluong_dathang = item.soluong;
                            ddh.soluong_dathang = 0;
                            ddh.soluong_conlai = item.soluong;
                            ddh.soluong_daxuat = 0;
                            ddh.sl_inner = item.sl_inner;
                            ddh.l1 = item.l1;
                            ddh.w1 = item.w1;
                            ddh.h1 = item.h1;
                            ddh.sl_outer = item.sl_outer;
                            ddh.l2 = item.l2;
                            ddh.w2 = item.w2;
                            ddh.h2 = item.h2;
                            ddh.v2 = item.v2;
                            ddh.sl_cont = item.sl_cont;
                            ddh.vd = item.vd;
                            ddh.vn = item.vn;
                            ddh.vl = item.vl;
                            ddh.ghichu_vachngan = item.ghichu_vachngan;
                            ddh.nhacungungid = db.md_sanphams.FirstOrDefault(sp => sp.md_sanpham_id.Equals(item.md_sanpham_id)).nhacungung;
                            ddh.ngaytao = DateTime.Now;
                            ddh.ngaycapnhat = DateTime.Now;
                            ddh.nguoitao = UserUtils.getUser(context);
                            ddh.nguoicapnhat = UserUtils.getUser(context);
                            ddh.hoatdong = true;
                            ddh.sothutu = sothutu;
                            ddh.trangthai = item.trangthai;
                            sothutu += 10;

                            //lstPOD.Add(ddh);
                            db.c_dongdonhangs.InsertOnSubmit(ddh);
                        }
                        db.c_donhangs.InsertOnSubmit(dh);
                        db.SubmitChanges();

                        string sql_update = "update c_dongdonhang set ngaycapnhat = getdate() where c_donhang_id = '" + id + "'";
                        mdbc.ExcuteNonQuery(sql_update);
                    }
                    //mdbc.ExcuteNonProcedure("QuotationToPO", "@c_baogia_id", qoId, "@sochungtu", sochungtu, "@donhangmau", bg.isform, "@nguoitao", UserUtils.getUser(context));



                    msg = String.Format("Ðã tạo PO thành công với số chứng từ {0}.!", sct = sct.Remove(sct.Length - 1));
                }
                context.Response.Write(msg);
            }
            //catch (Exception ex)
            //{
            //context.Response.Write(ex.Message);
            //}
        }
        else if (bg.md_trangthai_id.Equals("SOANTHAO"))
        {
            context.Response.Write("Quotation này chưa đuợc hiệu lực!");
        }
    }

    public void activeQO(HttpContext context)
    {
        String qoId = context.Request.QueryString["qoId"];
        String getQO = "SELECT sobaogia, md_doitackinhdoanh_id, md_paymentterm_id " +
            " , shipmenttime, md_banggia_id, ngaybaogia, ngayhethan, md_trongluong_id " +
            " , md_cangbien_id, totalcbm, totalcbf, totalquo, md_trangthai_id, mota " +
            " FROM c_baogia where c_baogia_id = @qoId";

        System.Data.SqlClient.SqlDataReader rd = mdbc.ExecuteReader(getQO, "@qoId", qoId);
        if (rd.HasRows)
        {
            rd.Read();
            String result = "";
            result += rd[0] == null ? "Số báo giá không được r?ng<br/>" : "";
            result += rd[1] == null ? "Chua chon khách hàng<br/>" : "";
            result += rd[2] == null ? "Chua chon Payment Term<br/>" : "";
            result += rd[3] == null ? "Chua nhap Shipment Date<br/>" : "";
            result += rd[4] == null ? "Chua chon Bảng giá<br/>" : "";
            result += rd[5] == null ? "Chua chon ngày báo giá<br/>" : "";
            result += rd[6] == null ? "Chua chon ngày hết hạn<br/>" : "";
            result += rd[7] == null ? "Chua nhập trọng luợng<br/>" : "";
            result += rd[8] == null ? "Chua chọn cảng biển<br/>" : "";
            result += rd[9] == null ? "Chua có CBM<br/>" : "";
            result += rd[10] == null ? "Chua có CBF<br/>" : "";
            result += rd[11] == null ? "Chua có Total Quotation<br/>" : "";

            String trangthai = rd[12].ToString().ToUpper();
            if (trangthai.Equals("SOANTHAO"))
            {
                String getQoDetails = "select count(*) from c_chitietbaogia where c_baogia_id = @c_baogia_id";
                int count = (int)mdbc.ExecuteScalar(getQoDetails, "@c_baogia_id", qoId);

                if (result.Equals("") && count != 0)
                {
                    String sqlStop = "select count(*) from md_sanpham sp, c_chitietbaogia ctbg where sp.md_sanpham_id = ctbg.md_sanpham_id AND ctbg.c_baogia_id =  @c_baogia_id AND sp.trangthai ='NHD'";
                    int stop = (int)mdbc.ExecuteScalar(sqlStop, "@c_baogia_id", qoId);
                    if (stop <= 0)
                    {
                        String updateActive = "Update c_baogia set md_trangthai_id = N'HIEULUC' where c_baogia_id = @c_baogia_id";
                        mdbc.ExcuteNonQuery(updateActive, "@c_baogia_id", qoId);
                        context.Response.Write("Hiêu lực quotation thành công.!");
                    }
                    else
                    {
                        result += "Không thể hiệu lực khi Quotation chứa sản phẩm dã ngung kinh doanh!<br/>";
                        context.Response.Write(result);
                    }
                }
                else
                {
                    result += "Quotation cha có sản phẩm <br/>";
                    context.Response.Write(result);
                }
            }
            else if (trangthai.Equals("HIEULUC"))
            {
                context.Response.Write("Hiện trạng thái của Quotation đã là hiệu lực.!<br/>");
            }
        }
        rd.Close();
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_baogia_id, sobaogia from c_baogia where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    #endregion

    #region oper

    #region AddEditDel
    public void add(HttpContext context)
    {
        try
        {
            String h = context.Request.Form["hoatdong"].ToLower();
            String gia = context.Request.Form["gia_db"];
            string isform = context.Request.Form["isform"].ToLower();
            string chkPBGCu_str = context.Request.Form["chkPBGCu"];
            string phienbangiacu = context.Request.Form["phienbangiacu"];

            bool hd = false, gia_db = false, chkPBGCu = false;
            int check = 0; // validate form on server
            int qo_mau = 0;
            string msg = "";

            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }

            if (chkPBGCu_str != null)
            {
                chkPBGCu_str = chkPBGCu_str.ToLower();
                if (chkPBGCu_str.Equals("on") || chkPBGCu_str.Equals("true"))
                { chkPBGCu = true; }
            }

            if (gia == "True")
            { gia_db = true; }

            if (isform.Equals("on") || isform.Equals("true"))
            { qo_mau = 1; }

            String sochungtu = jqGridHelper.Utils.taoChungTuQO(context.Request.Form["md_doitackinhdoanh_id"], qo_mau, DateTime.ParseExact(context.Request.Form["ngaybaogia"], "dd/MM/yyyy", null).Year.ToString());

            c_baogia mnu = new c_baogia
            {
                c_baogia_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                sobaogia = sochungtu,
                md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"],
                //md_paymentterm_id = context.Request.Form["md_paymentterm_id"],
                shipmenttime = int.Parse(context.Request.Form["shipmenttime"]),
                //md_banggia_id = context.Request.Form["md_banggia_id"],
                ngaybaogia = DateTime.ParseExact(context.Request.Form["ngaybaogia"], "dd/MM/yyyy", null),
                ngayhethan = DateTime.ParseExact(context.Request.Form["ngayhethan"], "dd/MM/yyyy", null),
                md_trongluong_id = context.Request.Form["md_trongluong_id"],
                md_cangbien_id = context.Request.Form["md_cangbien_id"],
                md_dongtien_id = context.Request.Form["md_dongtien_id"],
                md_kichthuoc_id = context.Request.Form["md_kichthuoc_id"],
                totalcbm = 0,
                totalcbf = 0,
                totalquo = 0,
                isform = ((qo_mau == 1) ? true : false),
                moq_item_color = context.Request.Form["moq_item_color"],
                md_trangthai_id = "SOANTHAO",
                chkPBGCu = chkPBGCu,
                phienbangiacu = phienbangiacu,

                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                mota = context.Request.Form["mota"],
                hoatdong = hd,
                gia_db = gia_db,
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            if (mnu.ngaybaogia >= mnu.ngayhethan)
            {
                msg = "Ngày báo giá không đuợc lớn hơn ngày hết hạn!";
                check++;
            }
            else if (mnu.md_doitackinhdoanh_id == "c010dd297f3ee5797e426176e8685b2c")
            {
                msg = "Khách Hàng: Không được bỏ trống!";
                check++;
            }
            else if (chkPBGCu == true & String.IsNullOrEmpty(phienbangiacu))
            {
                msg = "\"Phiên bảng giá cũ\" không thể bỏ trống.";
                check++;
            }

            md_doitackinhdoanh doitac = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(mnu.md_doitackinhdoanh_id));
            md_banggia bga = db.md_banggias.Where(s => s.ten_banggia.Contains(doitac.ma_dtkd + "-FOB")).FirstOrDefault();

            if (bga == null & gia == "True")
            {
                msg = "Khách hàng " + doitac.ma_dtkd + " không có bảng giá đặc biệt";
                check++;
            }

            if (check == 0)
            {
                db.c_baogias.InsertOnSubmit(mnu);
                db.SubmitChanges();
                jqGridHelper.Utils.writeResult(1, "Thêm mới thành công!");
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, msg);
            }
        }
        catch (Exception ex)
        {
            jqGridHelper.Utils.writeResult(0, ex.Message);
        }
    }

    public void edit(HttpContext context)
    {
        try
        {
            int check = 0;
            string msg = "";
            String h = context.Request.Form["hoatdong"].ToLower();
            String gia = context.Request.Form["gia_db"];
            bool hd = false, gia_db = false; ;
            if (h.Equals("on") || h.Equals("true"))
            { hd = true; }

            if (gia == "True")
            { gia_db = true; }

            String c_baogia_id = context.Request.Form["id"];

            String checkQO = "select md_trangthai_id from c_baogia where c_baogia_id = @c_baogia_id";
            String trangthai = (String)mdbc.ExecuteScalar(checkQO, "@c_baogia_id", c_baogia_id);
            if (trangthai.Equals("HIEULUC"))
            {
                jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi quotation đã hiệu lực!");
            }
            else
            {
                c_baogia m = db.c_baogias.Single(p => p.c_baogia_id == context.Request.Form["id"]);
                m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
                //m.md_paymentterm_id = context.Request.Form["md_paymentterm_id"];
                m.shipmenttime = int.Parse(context.Request.Form["shipmenttime"]);
                //m.md_banggia_id = context.Request.Form["md_banggia_id"];
                m.ngaybaogia = DateTime.ParseExact(context.Request.Form["ngaybaogia"], "dd/MM/yyyy", null);
                m.ngayhethan = DateTime.ParseExact(context.Request.Form["ngayhethan"], "dd/MM/yyyy", null);
                m.md_trongluong_id = context.Request.Form["md_trongluong_id"];
                m.md_cangbien_id = context.Request.Form["md_cangbien_id"];
                m.md_kichthuoc_id = context.Request.Form["md_kichthuoc_id"];
                m.moq_item_color = context.Request.Form["moq_item_color"];


                m.mota = context.Request.Form["mota"];
                m.hoatdong = hd;
                m.gia_db = gia_db;
                m.nguoicapnhat = UserUtils.getUser(context);
                m.ngaycapnhat = DateTime.Now;
                m.nguoicapnhat = UserUtils.getUser(context);

                if (m.ngaybaogia > m.ngayhethan)
                {
                    msg = "Ngày báo giá không được lớn hơn ngày hết hạn!";
                    check++;
                }
                if (check == 0)
                {
                    db.SubmitChanges();
                    jqGridHelper.Utils.writeResult(1, "Cập nhật thành công!");
                }
                else
                {
                    jqGridHelper.Utils.writeResult(0, msg);
                }

            }
        }
        catch (Exception ex)
        {
            jqGridHelper.Utils.writeResult(0, ex.Message);
        }
    }

    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        var qo = db.c_baogias.Single(q => q.c_baogia_id.Equals(id));
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        if (qo.md_trangthai_id.Equals("SOANTHAO"))
        {
            String sql = "delete c_baogia  where c_baogia_id IN (" + id + ") and md_trangthai_id <> 'HIEULUC'";
            mdbc.ExcuteNonQuery(sql);
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa báo giá đã hiệu lực!");
        }
    }
    #endregion

    #region load
    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        bool isadmin = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(context))).isadmin.Value;

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        // phân quy?n theo nhóm
        String manv = UserUtils.getUser(context);
        String strAccount = "";
        System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
        foreach (String item in lstAccount)
        {
            strAccount += String.Format(", '{0}'", item);
        }
        strAccount = String.Format("'{0}'{1}", manv, strAccount);

        String sqlCount = @"SELECT COUNT(1) AS count
            FROM c_baogia baogia with (nolock)
			LEFT JOIN md_doitackinhdoanh dtkd with (nolock) on baogia.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
			LEFT JOIN md_cangbien cb with (nolock) on baogia.md_cangbien_id = cb.md_cangbien_id 
            LEFT JOIN md_dongtien dt with (nolock) on baogia.md_dongtien_id = dt.md_dongtien_id 
            LEFT JOIN md_trongluong tl with (nolock) on baogia.md_trongluong_id = tl.md_trongluong_id
            LEFT JOIN md_kichthuoc kt with (nolock) on baogia.md_kichthuoc_id = kt.md_kichthuoc_id
            WHERE 1=1
            {0}
            {1}";
        sqlCount = String.Format(sqlCount, isadmin == true ? "" : "AND baogia.nguoitao IN(" + strAccount + ")", filter);

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
            sidx = "baogia.ngaytao";
            sord = "desc";
        }


        string strsql = @"select * from( 
            select baogia.c_baogia_id, baogia.md_trangthai_id, baogia.sobaogia, dtkd.ma_dtkd, 
            ngaybaogia, ngayhethan, cb.ten_cangbien, baogia.shipmenttime, baogia.moq_item_color,  tl.ten_trongluong, 
            baogia.totalcbm, baogia.totalcbf, baogia.totalquo, dt.ma_iso, kt.ten_kichthuoc, baogia.isform,  
            baogia.ngaytao, baogia.nguoitao, baogia.ngaycapnhat, baogia.nguoicapnhat, baogia.mota, baogia.hoatdong, 
            baogia.gia_db, baogia.ghepbo, baogia.chkPBGCu, baogia.phienbangiacu,
            ROW_NUMBER() OVER (ORDER BY {2} {3}) as RowNum 
            FROM c_baogia baogia with (nolock) 
			LEFT JOIN md_doitackinhdoanh dtkd with (nolock) on baogia.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
			LEFT JOIN md_cangbien cb with (nolock) on baogia.md_cangbien_id = cb.md_cangbien_id 
            LEFT JOIN md_dongtien dt with (nolock) on baogia.md_dongtien_id = dt.md_dongtien_id 
            LEFT JOIN md_trongluong tl with (nolock) on baogia.md_trongluong_id = tl.md_trongluong_id
            LEFT JOIN md_kichthuoc kt with (nolock) on baogia.md_kichthuoc_id = kt.md_kichthuoc_id 
            WHERE 1=1
            {0}
            {1}
        )P where RowNum > @start AND RowNum < @end";

        strsql = String.Format(strsql, isadmin == true ? "" : "AND baogia.nguoitao IN(" + strAccount + ")", filter, sidx, sord);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_baogia_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sobaogia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaybaogia"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngayhethan"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_cangbien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["shipmenttime"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["moq_item_color"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_trongluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalcbm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalquo"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_iso"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_kichthuoc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isform"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["gia_db"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chkPBGCu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phienbangiacu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghepbo"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }
    #endregion

    #endregion

    public bool IsReusable
    {
        get { return false; }
    }
}
