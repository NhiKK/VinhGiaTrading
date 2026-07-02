<%@ WebHandler Language="C#" Class="DanhSachDatHangController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class DanhSachDatHangController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();
    public String path = HttpContext.Current.Server.MapPath("~/FileSendMail/");
    public String logoPath = HttpContext.Current.Server.MapPath("~/images/VINHGIA_logo_print.png");
    public String productImagePath = HttpContext.Current.Server.MapPath("~/images/products/fullsize/");

    public string[] arrNCC = Helper.nhaCungCaps;
    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getpiinformation":
                this.getPiInformation(context);
                break;
            case "startsendmail":
                this.startSendMail(context);
                break;
            case "sendmail":
                this.sendMail(context);
                break;

            case "activepi":
                this.activePI(context);
                break;
            case "confirmpi":
                this.confirmPI(context);
                break;

            case "getoption":
                this.getSelectOption(context);
                break;

            case "guidonhang":
                this.guidonhang(context);
                break;
            case "danhsachdathang":
                this.danhsachdathang(context);
                break;
            case "ReturnPI":
                this.ReturnPI(context);
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

    public void confirmPI(HttpContext context)
    {
        String msg = "";
        try
        {
            String[] arrId = context.Request.QueryString["piId"].Split(',');

            foreach (var piId in arrId)
            {
                var dsdh = db.c_danhsachdathangs.FirstOrDefault(p => p.c_danhsachdathang_id.Equals(piId));
                if (dsdh.md_trangthai_id.Equals("SOANTHAO"))
                {
                    if (!String.IsNullOrWhiteSpace(dsdh.trangthai))
                        msg += "Lỗi:Dòng \"" + dsdh.sochungtu + "\" không ở trạng thái CHƯA GỬI.<br>";
                    else
                    {
                        dsdh.trangthai = "XACNHAN";
                        dsdh.ngaycapnhat = DateTime.Now;
                        dsdh.nguoicapnhat = UserUtils.getUser(context);
                        //db.SubmitChanges();
                        //msg = String.Format("Đã xác nhận DSĐH {0} thành công.!", dsdh.sochungtu);
                        // send mail
                        String manv = UserUtils.getUser(context);
                        var nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
                        //email nhan
                        var nv_xn = db.nhanviens.Where(p => p.email_xn == true).ToList();
                        //emailCC
                        var nv_cc = db.nhanviens.Where(p => p.manv == dsdh.nguoi_phutrach).ToList();
                        if (nv_xn == null)
                        {
                            msg = string.Format(@"<div style='color:red'>Chưa có thông tin người xác nhận.</div>");
                        }
                        else
                        {
                            md_smtp smtp = null;
                            if (string.IsNullOrWhiteSpace(nv.smtp))
                                smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
                            else
                                smtp = db.md_smtps.FirstOrDefault(p => p.md_smtp_id == nv.smtp);

                            var mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value, smtp.use_ssl.GetValueOrDefault(false));
                            var tmp = db.md_mailtemplates.FirstOrDefault(p => p.use_for.Equals("PI") && p.ten_template == "PI_XN");
                            var lstMail = new List<Dictionary<string, object>>();

                            string noidung = tmp.content_mail
                                .Replace("%0D", "<br/>")
                                .Replace("[NGAYLAP]", dsdh.ngaylap.Value.ToString("dd/MM/yyyy"));

                            noidung = noidung.Replace("[CHUNGTUDATHANG]", dsdh.sochungtu);
                            noidung = noidung.Replace("[SOPO]", dsdh.so_po);
                            //noidung = noidung.Replace("[NHACUNGUNG]", nv_xn.hoten);
                            noidung = noidung.Replace("[NGAYGIAOHANG]", dsdh.hangiaohang_po.Value.ToString("dd/MM/yyyy"));
                            //so dong hang
                            var dongdsdh = db.c_dongdsdhs.Where(p => p.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).Count();
                            noidung = noidung.Replace("[SOLUONGMAHANG]", dongdsdh.ToString());
                            noidung = noidung.Replace("[GIATRIDONHANG]", "đã ẩn");
                            noidung = Public.DecodeHTML(noidung);
                            string tieude = tmp.subject_mail;
                            tieude = tieude.Replace("[CHUNGTUDATHANG]", dsdh.sochungtu);
                            tieude = tieude.Replace("[SOPO]", dsdh.so_po);
                            tieude = tieude.Replace("[NGAYLAP]", dsdh.ngaylap == null ? "" : dsdh.ngaylap.Value.ToString("dd/MM/yyyy"));


                            //ds email nhan
                            var emails = string.Join(",", nv_xn.Where(s => !string.IsNullOrEmpty(s.email)).Select(s => s.email));
                            var emailccs = string.Join(",", nv_xn.Where(s => !string.IsNullOrEmpty(s.email_cc)).Select(s => s.email_cc));

                            var emailCC = string.Join(",", nv_cc.Where(s => !string.IsNullOrEmpty(s.email)).Select(s => s.email));
                            var mailObj = new Dictionary<string, object>();
                            mailObj["tieude"] = tieude;
                            mailObj["noidung"] = noidung;
                            mailObj["ma_dtkd"] = "";
                            mailObj["email"] = emails;
                            mailObj["emailkt"] = emailCC;
                            mailObj["dsdh"] = dsdh;
                            lstMail.Add(mailObj);

                            try
                            {
                                confirmPI_SendMail(context, lstMail, nv, mail, "");
                                db.SubmitChanges();
                                msg = String.Format("Đã xác nhận DSĐH {0} thành công.!", dsdh.sochungtu);
                            }
                            catch (Exception ex)
                            {
                                msg += string.Format(@"<div style='color:red'>Gửi mail thất bại: {0}.</div>", ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    msg += String.Format("<br>Hiện trạng thái danh sách đặt hàng {0} đã là hiệu lực.!", dsdh.sochungtu);
                }
            }

        }
        catch (Exception ex)
        {
            context.Response.Write(ex.ToString());
        }
        context.Response.Write(msg);
    }

    private void confirmPI_SendMail(HttpContext context, List<Dictionary<string, object>> lstMail, nhanvien nv, GoogleMail mail, string madtkd)
    {
        foreach (var mailObj in lstMail)
        {
            string emailCC = nv.email_cc + "," + UserUtils.mailKCS;

            if (string.IsNullOrEmpty(nv.email_cc) & string.IsNullOrEmpty(UserUtils.mailKCS))
            {
                emailCC = "";
            }
            else if (string.IsNullOrEmpty(nv.email_cc))
            {
                emailCC = UserUtils.mailKCS;
            }
            else if (string.IsNullOrEmpty(UserUtils.mailKCS))
            {
                emailCC = nv.email_cc;
            }

            var dsdh = mailObj["dsdh"] as c_danhsachdathang;
            //string fileTTSP = taoFileThongTinSanPham(context, nv.manv, dsdh);
            var fileDDHsKhongGia = taoFileDonDatHang(context, nv.manv, mailObj["ma_dtkd"].ToString(), dsdh, false);
            //string fileXNBB = taoFileXacNhanBaoBi(context, nv.manv, dsdh);
            string emailkt = mailObj["email"] + "";
            string emailcc = mailObj["emailkt"] + "";
            if (!string.IsNullOrEmpty(emailkt))
            {
                mail.Send(
                    nv.email
                    , emailkt
                    , emailcc
                    , ""
                    , mailObj["tieude"].ToString()
                    , mailObj["noidung"].ToString()
                    , fileDDHsKhongGia[0]
                );
            }
        }
    }

    public void ReturnPI(HttpContext context)
    {
        try
        {
            String result = "", resultSuccess = "";
            String[] arrId = context.Request.QueryString["piId"].Split(',');

            foreach (var piId in arrId)
            {
                var dsdh = db.c_danhsachdathangs.FirstOrDefault(p => p.c_danhsachdathang_id.Equals(piId));
                var donhang = db.c_donhangs.FirstOrDefault(dh => dh.c_donhang_id.Equals(dsdh.c_donhang_id));
                var khxh = db.c_kehoachxuathangs.Where(s => s.c_danhsachdathang_id == dsdh.sochungtu).FirstOrDefault();

                var dongDSDHs = db.c_dongdsdhs.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).ToList();
                var idsDDSDHs = dongDSDHs.Select(s => s.c_dongdsdh_id).ToList();
                var tonTaiNhapKho = db.c_dongnhapxuats.Where(s => idsDDSDHs.Contains(s.c_dongdsdh_id)).Take(1).Count() > 0;
                if (donhang != null)
                {
                    String trangthai = dsdh.md_trangthai_id;
                    switch (trangthai)
                    {
                        case "SOANTHAO":
                            result += "Hiện tại trạng thái đơn hàng đã là Soạn Thảo!<br/>";
                            break;
                        case "HIEULUC":
                            if (tonTaiNhapKho)
                            {
                                result += dsdh.sochungtu + " đã tạo phiếu nhập kho!<br/>";
                                break;
                            }
                            else
                            {
                                if (khxh != null)
                                    db.c_kehoachxuathangs.DeleteOnSubmit(khxh);

                                foreach (var ddsdh in dongDSDHs)
                                {
                                    var dongDH = db.c_dongdonhangs.Where(s => s.c_dongdonhang_id == ddsdh.c_dongdonhang_id).FirstOrDefault();
                                    if (dongDH != null)
                                    {
                                        dongDH.soluong_dathang = dongDH.soluong_dathang.GetValueOrDefault(0) - ddsdh.sl_dathang.GetValueOrDefault(0);
                                        dongDH.soluong_conlai = dongDH.soluong_conlai.GetValueOrDefault(0) + ddsdh.sl_dathang.GetValueOrDefault(0);
                                    }
                                }

                                dsdh.trangthai = "DANHAN";
                                dsdh.md_trangthai_id = "SOANTHAO";
                                resultSuccess += dsdh.sochungtu + " thành công trả về Soạn Thảo!<br/>";
                                string manv = UserUtils.getUser(context);
                                UserUtils.WriteLog(db, context, manv, DateTime.Now, "DSDH", donhang.sochungtu);
                            }
                            break;
                        default:
                            break;

                    }
                }
            }

            if (result.Length <= 0)
            {
                mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateGoiHDLH");
                mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateSLDaDatTrenPO");
                mdbc.ExcuteNonQuery("ALTER TABLE c_dongdonhang DISABLE TRIGGER updateCBMCBFPO");
                try
                {
                    db.SubmitChanges();
                    result = resultSuccess;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
                mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang ENABLE TRIGGER updateGoiHDLH");
                mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang ENABLE TRIGGER updateSLDaDatTrenPO");
                mdbc.ExcuteNonQuery("ALTER TABLE c_dongdonhang ENABLE TRIGGER updateCBMCBFPO");
            }

            context.Response.Write(result);

        }
        catch (Exception ex)
        {
            context.Response.Write(ex.ToString());
        }
    }

    public void danhsachdathang(HttpContext context)
    {
        var lst = new List<string>();
        lst.Add("");
        lst.Add(null);
        lst.Add("CHUAGUI");
        lst.Add("TUCHOI");
        lst.Add("DAGUI");
        lst.Add("DANHAN");

        string mdtkdId0 = db.md_doitackinhdoanhs.Where(s => s.ma_dtkd == arrNCC[0]).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
        var dsdhs0 = db.c_danhsachdathangs.Where(s => lst.Contains(s.trangthai) & s.md_trangthai_id != Helper.HIEULUC & s.md_doitackinhdoanh_id == mdtkdId0);
        var item0 = new Dictionary<string, object>();
        item0["result"] = dsdhs0.Select(s => new { s.c_danhsachdathang_id }).ToList();
        string msgVINHGIA = Extension.GetModule(
            Extension.API_ALL(arrNCC[0]) + "?oper=getListDSDH",
            item0
        );
        if (!msgVINHGIA.Contains("false##"))
        {
            var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(msgVINHGIA);
            foreach (var a in dsdhs0)
            {
                string trangthai = data.Where(s => s["c_danhsachdathang_id"] + "" == a.c_danhsachdathang_id).Select(s => s["trangthai"] + "").FirstOrDefault();
                if (!string.IsNullOrEmpty(trangthai))
                {
                    if (trangthai == "DANHAN")
                        a.trangthai = "DANHAN";
                    else if (trangthai == "DAGUI")
                        a.trangthai = "DAGUI";
                    else
                        a.trangthai = "DAHIEULUC";
                }
                else
                {
                    if (a.trangthai != "CHUAGUI" & !string.IsNullOrEmpty(a.trangthai))
                        a.trangthai = "TUCHOI";
                    else
                        a.trangthai = "CHUAGUI";
                }
            }

            mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateSLDaDatTrenPO");
            //mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang DISABLE TRIGGER TrigUpdateSoPI");
            mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateGoiHDLH");
            db.SubmitChanges();
            mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang ENABLE  TRIGGER updateSLDaDatTrenPO");
            //mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang ENABLE  TRIGGER TrigUpdateSoPI");
            mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang ENABLE  TRIGGER updateGoiHDLH");
        }

        string mdtkdId1 = db.md_doitackinhdoanhs.Where(s => s.ma_dtkd == arrNCC[1]).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
        var dsdhs1 = db.c_danhsachdathangs.Where(s => lst.Contains(s.trangthai) & s.md_trangthai_id != Helper.HIEULUC & s.md_doitackinhdoanh_id == mdtkdId1);
        var item1 = new Dictionary<string, object>();
        item1["result"] = dsdhs1.Select(s => new { s.c_danhsachdathang_id }).ToList();
        string msgHHH = Extension.GetModule(
        Extension.API_ALL(arrNCC[1]) + "?oper=getListDSDH",
        item1
    );
        if (!msgHHH.Contains("false##"))
        {
            var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(msgHHH);
            foreach (var a in dsdhs1)
            {
                string trangthai = data.Where(s => s["c_danhsachdathang_id"] + "" == a.c_danhsachdathang_id).Select(s => s["trangthai"] + "").FirstOrDefault();
                if (!string.IsNullOrEmpty(trangthai))
                {
                    if (trangthai == "DANHAN")
                        a.trangthai = "DANHAN";
                    else if (trangthai == "DAGUI")
                        a.trangthai = "DAGUI";
                    else
                        a.trangthai = "DAHIEULUC";
                }
                else
                {
                    if (a.trangthai != "CHUAGUI" & !string.IsNullOrEmpty(a.trangthai))
                        a.trangthai = "TUCHOI";
                    else
                        a.trangthai = "CHUAGUI";
                }
            }

            mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateSLDaDatTrenPO");
            //mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang DISABLE TRIGGER TrigUpdateSoPI");
            mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateGoiHDLH");
            db.SubmitChanges();
            mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang ENABLE  TRIGGER updateSLDaDatTrenPO");
            //mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang ENABLE  TRIGGER TrigUpdateSoPI");
            mdbc.ExcuteNonQuery("ALTER TABLE c_danhsachdathang ENABLE  TRIGGER updateGoiHDLH");
        }

        context.Response.Write(msgVINHGIA + msgHHH);
    }

    private void guidonhangSendMail(HttpContext context, List<Dictionary<string, object>> lstMail, nhanvien nv, GoogleMail mail, string madtkd)
    {
        foreach (var mailObj in lstMail.Where(s => s["ma_dtkd"].ToString() == madtkd))
        {
            string emailCC = nv.email_cc + "," + UserUtils.mailKCS;

            if (string.IsNullOrEmpty(nv.email_cc) & string.IsNullOrEmpty(UserUtils.mailKCS))
            {
                emailCC = "";
            }
            else if (string.IsNullOrEmpty(nv.email_cc))
            {
                emailCC = UserUtils.mailKCS;
            }
            else if (string.IsNullOrEmpty(UserUtils.mailKCS))
            {
                emailCC = nv.email_cc;
            }

            var dsdh = mailObj["dsdh"] as c_danhsachdathang;
            string fileTTSP = taoFileThongTinSanPham(context, nv.manv, dsdh);
            var fileDDHsKhongGia = taoFileDonDatHang(context, nv.manv, mailObj["ma_dtkd"].ToString(), dsdh, false);
            string fileXNBB = taoFileXacNhanBaoBi(context, nv.manv, dsdh);
            string emailkt = mailObj["email"] + "";

            if (!string.IsNullOrEmpty(emailkt))
            {
                mail.Send(
                    nv.email
                    , emailkt
                    , emailCC
                    , ""
                    , mailObj["tieude"].ToString()
                    , mailObj["noidung"].ToString()
                    , fileTTSP + ";" + fileDDHsKhongGia[0] + ";" + fileXNBB + ";" + fileDDHsKhongGia[1]
                );
            }
        }
    }

    public string EncodeHTML_DSDH(string a)
    {
        if (a == null)
        {
            a = "";
        }
        a = a.Replace("&", "%26");
        a = a.Replace("+", "%2B");
        a = a.Replace("&", "%26");
        a = a.Replace("+", "%2B");
        return a;
    }

    public void guidonhang(HttpContext context)
    {
        var db = new LinqDBDataContext();
        String manv = UserUtils.getUser(context);
        var nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
        md_smtp smtp = null;
        if (string.IsNullOrWhiteSpace(nv.smtp))
            smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
        else
            smtp = db.md_smtps.FirstOrDefault(p => p.md_smtp_id == nv.smtp);

        var mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value, smtp.use_ssl.GetValueOrDefault(false));
        var tmp = db.md_mailtemplates.FirstOrDefault(p => p.use_for.Equals("PI") && p.ten_template == "PI_VG");
        string msg = "", vnn_msg = "";
        string id = context.Request.Form["id"];
        string[] vnn = id.Split(',');
        string id_all = "";
        var lstVINHGIA = new List<Dictionary<string, object>>();
        var lstHHH = new List<Dictionary<string, object>>();
        var lstMail = new List<Dictionary<string, object>>();
        var lstDSDH = db.c_danhsachdathangs.Where(s => vnn.Contains(s.c_danhsachdathang_id));
        //
        foreach (var dsdh in lstDSDH)
        {
            var dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == dsdh.md_doitackinhdoanh_id).FirstOrDefault();

            if (dsdh.md_trangthai_id != "SOANTHAO")
                msg += "Lỗi:Dòng \"" + dsdh.sochungtu + "\" đã hiệu lực.<br>";
            else if (dsdh.trangthai == "DAGUI")
                msg += "Lỗi:Dòng \"" + dsdh.sochungtu + "\" đã được gửi trước đó.<br>";
            else if (dtkd.ma_dtkd != "ANCO1" & dtkd.ma_dtkd != "ANCO1_HACA-HK")
            {
                if (arrNCC.Contains(dtkd.ma_dtkd))
                {
                    dsdh.trangthai = "DAGUI";
                    dsdh.nguoi_dathang = UserUtils.getUser(context);
                    var dsdh2 = new c_danhsachdathang();
                    dsdh.CopyPropertiesTo(dsdh2);
                    dsdh2.sochungtu = EncodeHTML_DSDH(dsdh2.sochungtu);
                    dsdh2.so_po = EncodeHTML_DSDH(dsdh2.so_po);
                    dsdh2.mota = string.IsNullOrEmpty(dsdh2.mota) ? "" : EncodeHTML_DSDH(dsdh2.mota);
                    if (string.IsNullOrEmpty(dsdh2.huongdanlamhang))
                        dsdh2.huongdanlamhang = "";
                    dsdh2.huongdanlamhang = EncodeHTML_DSDH(dsdh2.huongdanlamhang);
                    if (string.IsNullOrEmpty(dsdh2.huongdanlamhangchung))
                        dsdh2.huongdanlamhangchung = "";
                    dsdh2.huongdanlamhangchung = EncodeHTML_DSDH(dsdh2.huongdanlamhangchung);

                    if (string.IsNullOrEmpty(dsdh2.diachigiaohang))
                        dsdh2.diachigiaohang = "";
                    dsdh2.diachigiaohang = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == dsdh2.diachigiaohang).Select(s => s.ma_dtkd).FirstOrDefault();
                    vnn_msg += string.Format(@"<div style='color:blue'>Gửi đơn hàng ""{0}"" thành công.</div>", dsdh.sochungtu);
                    var item = new Dictionary<string, object>();
                    item["c_danhsachdathang"] = dsdh2;

                    var dongdsdh = db.c_dongdsdhs.
                            Where(s => s.c_danhsachdathang_id == dsdh2.c_danhsachdathang_id).
                            Select(x => new
                            {
                                x.w2,
                                x.w1,
                                x.vn,
                                x.vl,
                                x.vd,
                                x.v2,
                                tem_dan = EncodeHTML_DSDH(x.tem_dan),
                                x.sothutu,
                                x.sl_inner,
                                dvt_inner = db.md_donvitinhs.FirstOrDefault(t => t.md_donvitinh_id == db.md_donggois.FirstOrDefault(tt => tt.md_donggoi_id == x.md_donggoi_id).dvt_inner).ten_dvt,
                                x.sl_outer,
                                dvt_outer = db.md_donvitinhs.FirstOrDefault(t => t.md_donvitinh_id == db.md_donggois.FirstOrDefault(tt => tt.md_donggoi_id == x.md_donggoi_id).dvt_outer).ten_dvt,
                                x.sl_huy,
                                x.sl_dathang,
                                x.sl_dagiao,
                                x.sl_cont,
                                x.sl_conlai,
                                mota = EncodeHTML_DSDH(x.mota),
                                md_sanpham_id = db.md_sanphams.Where(t => t.md_sanpham_id == x.md_sanpham_id).Select(t => t.ma_sanpham).FirstOrDefault(),
                                x.md_donggoi_id,
                                x.md_doitackinhdoanh_id,
                                ma_sanpham_khach = EncodeHTML_DSDH(x.ma_sanpham_khach),
                                x.l2,
                                x.l1,
                                huongdan_dathang = EncodeHTML_DSDH(x.huongdan_dathang),
                                x.h2,
                                x.h1,
                                x.gianhap,
                                x.giachuan,
                                ghichu_vachngan = EncodeHTML_DSDH(x.ghichu_vachngan),
                                x.c_dongdsdh_id,
                                x.c_dongdonhang_id,
                                x.c_danhsachdathang_id
                            })
                            .ToList();

                    var phidsdh = db.c_phidathangs.
                        Where(s => s.c_danhsachdathang_id == dsdh2.c_danhsachdathang_id & s.hoatdong == true).
                        Select(x => new
                        {
                            x.sotien,
                            x.isphicong,
                            mota = EncodeHTML_DSDH(x.mota),
                            x.c_phidathang_id,
                            x.c_danhsachdathang_id
                        })
                        .ToList();

                    item["c_dongdsdh"] = dongdsdh;
                    item["c_phidathang"] = phidsdh;

                    if (dtkd.ma_dtkd == arrNCC[0])
                    {
                        lstVINHGIA.Add(item);
                    }
                    else
                    {
                        lstHHH.Add(item);
                    }
                    if (tmp != null)
                    {
                        decimal? phicong = 0, phitru = 0, discount = 0;
                        decimal cong = 0, tru = 0;
                        phicong = phidsdh.Where(p => p.isphicong.Equals(true)).Select(p => p.sotien).Sum();
                        phitru = phidsdh.Where(p => p.isphicong.Equals(false)).Select(p => p.sotien).Sum();

                        cong = phicong == null ? 0 : phicong.Value;
                        tru = phitru == null ? 0 : phitru.Value;

                        decimal? giatridonhang = 0;
                        if (dsdh.total != null & dsdh.discount != 100)
                        {
                            if (dsdh.discount > 0)
                            {
                                discount = (dsdh.total.Value * dsdh.discount / 100);
                            }
                            giatridonhang = dsdh.total.Value - discount + cong - tru;
                        }

                        string noidung = tmp.content_mail
                            .Replace("%0D", "<br/>")
                            .Replace("[NGAYLAP]", dsdh.ngaylap.Value.ToString("dd/MM/yyyy"));

                        noidung = noidung.Replace("[CHUNGTUDATHANG]", dsdh.sochungtu);
                        noidung = noidung.Replace("[SOPO]", dsdh.so_po);
                        noidung = noidung.Replace("[NHACUNGUNG]", dtkd.ten_dtkd);
                        noidung = noidung.Replace("[NGAYGIAOHANG]", dsdh.hangiaohang_po.Value.ToString("dd/MM/yyyy"));
                        noidung = noidung.Replace("[SOLUONGMAHANG]", dongdsdh.Count().ToString());
                        noidung = noidung.Replace("[GIATRIDONHANG]", "đã ẩn");
                        noidung = Public.DecodeHTML(noidung);
                        string tieude = tmp.subject_mail;
                        tieude = tieude.Replace("[CHUNGTUDATHANG]", dsdh.sochungtu);
                        tieude = tieude.Replace("[SOPO]", dsdh.so_po);
                        tieude = tieude.Replace("[NGAYLAP]", dsdh.ngaylap == null ? "" : dsdh.ngaylap.Value.ToString("dd/MM/yyyy"));

                        var mailObj = new Dictionary<string, object>();
                        mailObj["tieude"] = tieude;
                        mailObj["noidung"] = noidung;
                        mailObj["ma_dtkd"] = dtkd.ma_dtkd;
                        mailObj["email"] = dtkd.email;
                        mailObj["emailkt"] = dtkd.emailkt;
                        mailObj["dsdh"] = dsdh;
                        lstMail.Add(mailObj);
                    }
                }
                else
                {
                    dsdh.trangthai = "DAHIEULUC";
                }
            }
            else if (msg.Length <= 0)
            {
                id_all += "'" + dsdh.c_danhsachdathang_id + "',";
            }
        }

        if (lstVINHGIA.Count > 0)
        {
            var item = new Dictionary<string, object>();
            item["result"] = lstVINHGIA;
            string linkEMan = Extension.API_ALL(arrNCC[0]) + "?oper=NhanDonHang";
            try
            {
                msg += Extension.GetModule(
                    linkEMan,
                    item
                );
            }
            catch (Exception ex)
            {
                string link = linkEMan;
                var url = context.Request.Url;
                if (!link.Contains("http"))
                {
                    var vtp = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
                    if (string.IsNullOrWhiteSpace(vtp))
                        link = url.GetLeftPart(UriPartial.Authority) + "/" + link;
                    else
                        link = url.GetLeftPart(UriPartial.Authority) + vtp + "/" + link;
                }
                msg += string.Format("<div style='display:none'>{0}</div>", ex + "," + link);
                msg += "errVinhGia:" + ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg += vnn_msg;
                db.SubmitChanges();
                try
                {
                    guidonhangSendMail(context, lstMail, nv, mail, arrNCC[0]);
                }
                catch (Exception ex)
                {
                    msg += string.Format(@"<div style='color:red'>Gửi mail thất bại: {0}.</div>", ex.Message);
                }
            }
        }

        if (lstHHH.Count > 0)
        {
            var item = new Dictionary<string, object>();
            item["result"] = lstHHH;
            try
            {
                msg += Extension.GetModule(
                    Extension.API_ALL(arrNCC[1]) + "?oper=NhanDonHang",
                    item
                );
            }
            catch (Exception ex)
            {
                msg += string.Format("<div style='display:none'>{0}</div>", ex + "");
                msg += "errHHH: " + ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg += vnn_msg;
                db.SubmitChanges();
                try
                {
                    guidonhangSendMail(context, lstMail, nv, mail, arrNCC[1]);
                }
                catch (Exception ex)
                {
                    msg += string.Format(@"<div style='color:red'>Gửi mail thất bại: {0}.</div>", ex.Message);
                }
            }
        }

        if (lstVINHGIA.Count <= 0 & lstHHH.Count <= 0)
        {
            msg += vnn_msg;
            db.SubmitChanges();
        }

        context.Response.Write(msg);
    }

    public string getSqlThongTinSP(string c_danhsachdathang_id)
    {
        String sql = "select * from dbo.f_ttsanpham_dsdh('" + c_danhsachdathang_id + "')";
        return sql;
    }

    public string getSqlDsdhPdf(string c_danhsachdathang_id, bool guiNCC)
    {
        string gianhap = "null", thanhtien = "null";
        if (guiNCC == false)
        {
            gianhap = "ddsdh.gianhap";
            thanhtien = "(ddsdh.sl_dathang * ddsdh.gianhap)";
        }

        string select = String.Format(@"
                        select 
	                        dsdh.sochungtu
							, dh.sochungtu as so_po
							, dh.customer_order_no
							, dsdh.ngaylap
							, dsdh.hangiaohang_po
							, dtkd.ma_dtkd
							, dtkd.ten_dtkd
							, dtkd.tel
							, dtkd.fax
	                        , sp.ma_sanpham
							, (case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) + '.jpg' else SUBSTRING(sp.ma_sanpham,0,12) + '.jpg' end) as pic
							, (case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) else SUBSTRING(sp.ma_sanpham,0,12) end) as pic
							, ddsdh.ma_sanpham_khach
							, ddsdh.mota_tiengviet
							, dvt.ma_edi
                            , ddsdh.sl_dathang
							, dsdh.huongdanlamhang
							, dsdh.huongdanlamhangchung
							, ddsdh.huongdan_dathang
	                        , gh.ma_dtkd as diachigiaohang
							, dh.sl_cont
							, dh.loai_cont
							, dsdh.discount as discountdecimal
							, ddsdh.v2
                            , (case ddsdh.sl_inner when 0 then ' ' else (cast(ddsdh.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner)) end) as dginner
                            , (cast(ddsdh.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer)) as dgouter
                            , (select hoten from nhanvien where manv = dsdh.nguoitao) as nguoitao
							, sp.trongluong
							, sp.mota as ghichu
							, dsdh.mota
							, {1} as gianhap
							, {2} as thanhtien
                        from 
	                        c_danhsachdathang dsdh
							left join md_doitackinhdoanh dtkd on dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
							left join md_doitackinhdoanh gh on dsdh.diachigiaohang = gh.md_doitackinhdoanh_id
	                        left join c_dongdsdh ddsdh on dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
							left join md_sanpham sp on ddsdh.md_sanpham_id = sp.md_sanpham_id
							left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
							left join c_donhang dh  on dsdh.c_donhang_id = dh.c_donhang_id
							left join md_donggoi dg on ddsdh.md_donggoi_id = dg.md_donggoi_id
                        where
	                        dsdh.c_danhsachdathang_id = N'{0}' order by sp.ma_sanpham asc",
                            c_danhsachdathang_id == null ? "" : c_danhsachdathang_id,
                            gianhap,
                            thanhtien
                        );
        return select;
    }

    public void getPiInformation(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        string c_danhsachdathang_id = context.Request.QueryString["poId"];
        string msg = "";
        try
        {
            decimal totalQuantity = 0, totalSalary = 0, totalCbm = 0;
            decimal? phicong = 0, phitru = 0, cbm = 0, quantity = 0;
            c_danhsachdathang dsdh = db.c_danhsachdathangs.FirstOrDefault(d => d.c_danhsachdathang_id.Equals(c_danhsachdathang_id));
            if (dsdh != null)
            {
                var ddsdh = from c in db.c_dongdsdhs where c.c_danhsachdathang_id.Equals(dsdh.c_danhsachdathang_id) select c;
                var phiDH = from phi in db.c_phidathangs where phi.c_danhsachdathang_id.Equals(dsdh.c_danhsachdathang_id) & phi.hoatdong == true select phi;

                quantity = ddsdh.Sum(p => p.sl_dathang);
                cbm = ddsdh.Sum(p => p.v2);
                phicong = phiDH.Where(p => p.isphicong.Equals(true)).Select(p => p.sotien).Sum();
                phitru = phiDH.Where(p => p.isphicong.Equals(false)).Select(p => p.sotien).Sum();
                totalSalary = ddsdh.Sum(p => p.sl_dathang * p.gianhap).Value;
                totalSalary = (totalSalary - (totalSalary * (dsdh.discount == null ? 0 : dsdh.discount.Value) / 100)) + (phicong == null ? 0 : phicong.Value) - (phitru == null ? 0 : phitru.Value);
                totalCbm = cbm == null ? 0 : cbm.Value;
                totalQuantity = quantity == null ? 0 : quantity.Value;

                msg = string.Format("Đơn đặt hàng:{0}, CBM:{1:#,##0.000}, Amount:{2:#,##0.00}, Quantity:{3:#,##0}", dsdh.sochungtu, totalCbm, totalSalary, totalQuantity);
            }
            else
                msg = "Đơn đặt hàng này đã bị xóa, hãy làm mới dữ liệu";
        }
        catch (Exception e)
        {
            msg = e.Message;
        }
        context.Response.Write(msg);
    }

    public void startSendMail(HttpContext context)
    {
        try
        {
            string piId = context.Request.QueryString["piId"];
            string manv = UserUtils.getUser(context);
            var nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
            //Lấy báo giá
            var dsdh = db.c_danhsachdathangs.FirstOrDefault(p => p.c_danhsachdathang_id.Equals(piId));
            // Lấy thông tin đối tác
            var doitac = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(dsdh.md_doitackinhdoanh_id));

            string str = @"
                <table style='width:100%'> 
			    <tr>
				    <td colspan='3'>
					    <input id='new' checked='checked' value='toAll' name='rdType' type='radio'/>
					    <label for='new'>Danh Sách Đặt Hàng mới:</label>
					    <!-- -->
					    <table class='t_new' style='width:100%; padding-left: 50px; border: 1px solid;'>
						    <tr class='hidden'> 
							    <td colspan='2'>
								    <input id='newKcs' value='newKcs' name='newKcs' type='checkbox' class='new_toAll' >
								    <label for='newKcs'>Gửi cho KCS</label>
							    </td>
						    </tr>
						    <tr class='hidden'>
							    <td>Gửi từ:</td>
							    <td>{0}</td>
						    </tr>
						    <tr class='hidden'>
							    <td>Gửi cho:</td>
							    <td>{3}</td>
						    </tr>
						    <tr class='hidden'>
							    <td>Cc:</td>
							    <td>{2}</td>
						    </tr>
						    <tr> 
							    <td colspan='2'>
								    <input id='newNcu' value='newNcu' name='newNcu' type='checkbox' class='new_toAll' >
								    <label for='newNcu'>Gửi cho Nhà cung ứng</label>
							    </td> 
						    </tr> 
						    <tr>
							    <td>Gửi từ:</td>
							    <td>{0}</td>
						    </tr>
						    <tr>
							    <td>Gửi cho:</td>
							    <td>{1}{4}</td>
						    </tr>
						    <tr>
							    <td>Cc:</td>
							    <td>{2}</td>
						    </tr>
                            <tr style='display:none'> 
							    <td colspan='2'>
								    <input id='newKTNCU' value='newKTNCU' name='newKTNCU' type='checkbox' class='new_toAll' >
								    <label for='newKTNCU'>Gửi cho Nhà cung ứng (không có giá FOB)</label>
							    </td> 
						    </tr> 
						    <tr style='display:none'>
							    <td>Gửi từ:</td>
							    <td>{0}</td>
						    </tr>
						    <tr style='display:none'>
							    <td>Gửi cho:</td>
							    <td>{4}</td>
						    </tr>
						    <tr style='display:none'>
							    <td>Cc:</td>
							    <td>{2}</td>
						    </tr>
					    </table>
				    </td>						
			    </tr>
			    <tr>
				    <td colspan='3'>
					    <input id='plus' value='toAllPlus' name='rdType' type='radio'/>
					    <label for='plus'>Bổ Sung Danh Sách Đặt Hàng:</label>
					    <table class='t_plus' style='width:100%; padding-left: 50px; border: 1px solid;'>
						    <tr class='hidden'> 
							    <td colspan='2'>
								    <input id='Kcs' value='Kcs' name='Kcs' type='checkbox' class='new_toAllPlus'>
								    <label for='Kcs'>Gửi cho KCS</label>
							    </td>
						    </tr>
						    <tr class='hidden'>
							    <td>Gửi từ:</td>
							    <td>{0}</td>
						    </tr>
						    <tr class='hidden'>
							    <td>Gửi cho:</td>
							    <td>{3}{4}</td>
						    </tr>
						    <tr class='hidden'>
							    <td>Cc:</td>
							    <td>{2}</td>
						    </tr>
						    <tr> 
							    <td colspan='2'>
								    <input id='Ncu' value='Ncu' name='Ncu' type='checkbox' class='new_toAllPlus'>
								    <label for='Ncu'>Gửi cho Nhà cung ứng</label>
							    </td> 
						    </tr> 
						    <tr>
							    <td>Gửi từ:</td>
							    <td>{0}</td>
						    </tr>
						    <tr>
							    <td>Gửi cho:</td>
							    <td>{1}{4}</td>
						    </tr>
						    <tr>
							    <td>Cc:</td>
							    <td>{2}</td>
						    </tr>
                            <tr style='display:none'> 
							    <td colspan='2'>
								    <input id='KTNCU' value='KTNCU' name='KTNCU' type='checkbox' class='new_toAllPlus'>
								    <label for='KTNCU'>Gửi cho Nhà cung ứng (không có giá FOB)</label>
							    </td> 
						    </tr> 
						    <tr style='display:none'>
							    <td>Gửi từ:</td>
							    <td>{0}</td>
						    </tr>
						    <tr style='display:none'>
							    <td>Gửi cho:</td>
							    <td>{4}</td>
						    </tr>
						    <tr style='display:none'>
							    <td>Cc:</td>
							    <td>{2}</td>
						    </tr>
					    </table>
				    </td>						
			    </tr>
			    </table>";

            context.Response.Write(string.Format(str, nv.email, doitac.email, nv.email_cc, UserUtils.mailKCS, string.IsNullOrEmpty(doitac.email) ? "" : "," + doitac.emailkt));
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    public void sendMail(HttpContext context)
    {
        string sendType = context.Request.QueryString["sendType"];
        //--
        string sendMailType = context.Request.QueryString["sendMailType"];
        //--
        string[] sendMail = sendMailType.Remove(sendMailType.Length - 1).Split(',');
        int count_mal = sendMail.Length;

        string manv = UserUtils.getUser(context);

        //--Tao hinh anh dinh kem

        //--
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
        //-- Lấy thông tin tmp
        md_mailtemplate tmp = db.md_mailtemplates.FirstOrDefault(p => p.use_for.Equals(sendType.Equals("toAllPlus") ? "PIPLUS" : "PI") && p.default_mail.Equals(true));

        // Lấy thông tin smtp
        md_smtp smtp = null;
        if (string.IsNullOrWhiteSpace(nv.smtp))
            smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
        else
            smtp = db.md_smtps.FirstOrDefault(p => p.md_smtp_id == nv.smtp);
        //-- c_danhsachdathang_id
        String piId = context.Request.QueryString["piId"];
        //--
        c_danhsachdathang dsdh = db.c_danhsachdathangs.FirstOrDefault(p => p.c_danhsachdathang_id.Equals(piId));
        //--
        md_doitackinhdoanh doitac = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(dsdh.md_doitackinhdoanh_id));
        //--
        int countProduct = (from p in db.c_dongdsdhs where p.c_danhsachdathang_id.Equals(piId) select p.sl_dathang).Count();

        bool next = true;
        var lstGNCC = new List<string> { "newNcu", "Ncu", "newKTNCU", "KTNCU" };

        string msg = "";
        //-- Canh bao loi
        if (nv == null)
        {
            next = false;
            msg += "Không tìm thấy mã nhân viên!";
        }

        if (tmp == null)
        {
            next = false;
            msg += "Không tìm thấy Mail Template!";
        }

        if (dsdh == null)
        {
            next = false;
            msg += "Không tìm thấy Danh sách đặt hàng!";
        }

        if (doitac == null)
        {
            next = false;
            msg += "Không tìm thấy đối tác!";
        }

        String fileNameExcel = manv + "Thong-Tin-San-Pham-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + ".xls";
        //-- Tao Excel
        try
        {
            rptThongTinSanPham report = new rptThongTinSanPham();
            report.Parameters["so_dsdh"].Value = dsdh.sochungtu;
            report.Parameters["so_donhang"].Value = dsdh.so_po;
            SqlDataAdapter da = new SqlDataAdapter(getSqlThongTinSP(piId), mdbc.GetConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            report.DataSource = ds;
            report.DataAdapter = da;
            report.ExportToXls(path + fileNameExcel);
        }
        catch (Exception ex)
        {
            next = false;
            context.Response.Write("Không tạo được file \"" + fileNameExcel + "\", " + ex.Message);
        }

        if (next)
        {
            try
            {
                decimal? phicong = 0, phitru = 0, discount = 0;
                decimal cong = 0, tru = 0;
                var phiDH = from phi in db.c_phidathangs where phi.c_danhsachdathang_id.Equals(dsdh.c_danhsachdathang_id) & phi.hoatdong == true select phi;

                phicong = phiDH.Where(p => p.isphicong.Equals(true)).Select(p => p.sotien).Sum();
                phitru = phiDH.Where(p => p.isphicong.Equals(false)).Select(p => p.sotien).Sum();

                cong = phicong == null ? 0 : phicong.Value;
                tru = phitru == null ? 0 : phitru.Value;

                decimal? giatridonhang = 0;
                if (dsdh.total != null & dsdh.discount != 100)
                {
                    if (dsdh.discount > 0)
                    {
                        discount = (dsdh.total.Value * dsdh.discount / 100);
                    }
                    giatridonhang = dsdh.total.Value - discount + cong - tru;
                }

                tmp.subject_mail = tmp.subject_mail.Replace("[CHUNGTUDATHANG]", dsdh.sochungtu);
                tmp.subject_mail = tmp.subject_mail.Replace("[SOPO]", dsdh.so_po);
                tmp.subject_mail = tmp.subject_mail.Replace("[NGAYLAP]", dsdh.ngaylap == null ? "" : dsdh.ngaylap.Value.ToString("dd/MM/yyyy"));

                tmp.content_mail = tmp.content_mail.Replace("[NHACUNGUNG]", doitac.ten_dtkd);
                tmp.content_mail = tmp.content_mail.Replace("[CHUNGTUDATHANG]", dsdh.sochungtu);
                tmp.content_mail = tmp.content_mail.Replace("[NGAYGIAOHANG]", dsdh.hangiaohang_po.Value.ToString("dd/MM/yyyy"));
                tmp.content_mail = tmp.content_mail.Replace("[SOLUONGMAHANG]", countProduct.ToString());
                tmp.content_mail = tmp.content_mail.Replace("[SOPO]", dsdh.so_po);
                GoogleMail mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value, smtp.use_ssl.GetValueOrDefault(false));
                string resp = "";
                var fileDDHs = new List<string>();
                var fileDDHsKhongGia = new List<string>();
                for (int i = 0; i <= count_mal - 1; i++)
                {
                    fileDDHs = taoFileDonDatHang(context, nv.manv, doitac.ma_dtkd, dsdh, false);
                    fileDDHsKhongGia = taoFileDonDatHang(context, nv.manv, doitac.ma_dtkd, dsdh, true);
                    if (fileDDHs[0].LastIndexOf(path) <= -1)
                    {
                        next = false;
                        context.Response.Write(fileDDHs[0]);
                    }
                    if (fileDDHs[1].LastIndexOf(path) <= -1)
                    {
                        next = false;
                        context.Response.Write(fileDDHs[1]);
                    }
                    if (fileDDHsKhongGia[0].LastIndexOf(path) <= -1)
                    {
                        next = false;
                        context.Response.Write(fileDDHsKhongGia[0]);
                    }
                    if (fileDDHsKhongGia[1].LastIndexOf(path) <= -1)
                    {
                        next = false;
                        context.Response.Write(fileDDHsKhongGia[1]);
                    }

                    if (next == true)
                    {
                        string noidung = tmp.content_mail
                            .Replace("%0D", "<br/>")
                            .Replace("[NGAYLAP]", dsdh.ngaylap.Value.ToString("dd/MM/yyyy"));
                        string noidungKhongGia = noidung;

                        noidung = noidung.Replace("[GIATRIDONHANG]", giatridonhang.ToString());
                        noidungKhongGia = noidungKhongGia.Replace("[GIATRIDONHANG]", "đã ẩn");

                        if (sendMail[i] == "newKcs" || sendMail[i] == "Kcs")
                        {
                            if (sendType == "toAllPlus")
                            {
                                mail.Send(nv.email, UserUtils.mailKCS, nv.email_cc, "", tmp.subject_mail, noidung, fileDDHs[0] + ";" + fileDDHs[1]);
                            }
                            else
                            {
                                mail.Send(nv.email, UserUtils.mailKCS, nv.email_cc, "", tmp.subject_mail, noidung, fileDDHs[0] + ";" + fileDDHs[1] + ";" + path + fileNameExcel);
                            }
                            resp += String.Format("Đã gửi mail thành công đến:<br/>{0}<br/>{1}<br/>", nv.email_cc, UserUtils.mailKCS);
                        }

                        if (lstGNCC.Contains(sendMail[i]))
                        {
                            string pathInBaoBi = "";
                            try
                            {
                                string sql = String.Format(@"select 
											pi_.c_danhsachdathang_id, po.sochungtu , pi_.ngaylap, dt.ten_dtkd, pi_.c_danhsachdathang_id
										from 
											c_donhang po
											left join c_danhsachdathang pi_ on po.c_donhang_id = pi_.c_donhang_id
											left join md_doitackinhdoanh dt on pi_.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
										where 
											pi_.c_danhsachdathang_id = '{0}'", piId == null ? "" : piId);
                                // tạo file excel
                                rptXacNhanBaoBi report = new rptXacNhanBaoBi();
                                pathInBaoBi = context.Server.MapPath("../FileSendMail/AnCoInBaoBi" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
                                SqlDataAdapter da = new SqlDataAdapter(sql, mdbc.GetConnection);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                report.DataSource = ds;
                                report.DataAdapter = da;
                                report.ExportToXls(pathInBaoBi);
                            }
                            catch (Exception ex)
                            {
                                next = false;
                                context.Response.Write("Không tạo được file xác nhận bao bì PDF, " + ex.Message);
                            }

                            if (next)
                            {
                                string ccSend = "", toSend = "";
                                ccSend = nv.email_cc;
                                toSend = doitac.email + (string.IsNullOrEmpty(doitac.emailkt) ? "" : "," + doitac.emailkt);
                                if (sendType == "toAllPlus")
                                {
                                    if (sendMail[i] == "newNcu" | sendMail[i] == "Ncu")
                                    {
                                        if (!string.IsNullOrEmpty(doitac.email))
                                            mail.Send(nv.email, doitac.email, nv.email_cc, "", tmp.subject_mail, noidung, fileDDHs[0] + ";" + fileDDHs[1] + ";" + pathInBaoBi);
                                        if (!string.IsNullOrEmpty(doitac.emailkt))
                                            mail.Send(nv.email, doitac.emailkt, "", "", tmp.subject_mail, noidungKhongGia, fileDDHsKhongGia[0] + ";" + fileDDHsKhongGia[1] + ";" + pathInBaoBi);
                                    }
                                }
                                else
                                {
                                    if (sendMail[i] == "newNcu" | sendMail[i] == "Ncu")
                                    {
                                        if (!string.IsNullOrEmpty(doitac.email))
                                            mail.Send(nv.email, doitac.email, nv.email_cc, "", tmp.subject_mail, noidung, fileDDHs[0] + ";" + fileDDHs[1] + ";" + pathInBaoBi + ";" + path + fileNameExcel);
                                        if (!string.IsNullOrEmpty(doitac.emailkt))
                                            mail.Send(nv.email, doitac.emailkt, "", "", tmp.subject_mail, noidungKhongGia, fileDDHsKhongGia[0] + ";" + fileDDHsKhongGia[1] + ";" + pathInBaoBi + ";" + path + fileNameExcel);
                                    }
                                }
                                resp += string.Format("<br/>Đã gửi mail thành công đến:<br/>{0}<br/>{1}<br/>", ccSend, toSend);
                            }
                        }
                    }
                }
                if (next)
                {
                    string dk_trangthai = "";
                    c_danhsachdathang dsdh_nht = db.c_danhsachdathangs.Where(s => s.c_danhsachdathang_id == piId).FirstOrDefault();
                    md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == dsdh_nht.md_doitackinhdoanh_id).FirstOrDefault();
                    if (!arrNCC.Contains(dtkd.ma_dtkd))
                        dk_trangthai = ", trangthai = N'DAGUI'";
                    String update = "update c_danhsachdathang set isgui_hdlh = 1" + dk_trangthai + " where c_danhsachdathang_id = @c_danhsachdathang_id";
                    mdbc.ExcuteNonQuery(update, "@c_danhsachdathang_id", piId);
                }
                context.Response.Write(resp);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }
    }

    public void activePI(HttpContext context)
    {
        String msg = "";
        try
        {
            String[] arrId = context.Request.QueryString["piId"].Split(',');

            foreach (var piId in arrId)
            {
                c_danhsachdathang dsdh = db.c_danhsachdathangs.FirstOrDefault(p => p.c_danhsachdathang_id.Equals(piId));

                c_donhang donhang = db.c_donhangs.FirstOrDefault(dh => dh.c_donhang_id.Equals(dsdh.c_donhang_id));

                if (dsdh.md_trangthai_id.Equals("SOANTHAO"))
                {
                    string ma_dtkd = db.md_doitackinhdoanhs.
                            Where(s => s.md_doitackinhdoanh_id == dsdh.md_doitackinhdoanh_id).
                            Select(s => s.ma_dtkd).FirstOrDefault();
                    // Dem cac dong don hang co sl_conlai < 1 trong cac dong danh sach dat hang co sl_conlai > sl_conlai trong dong don hang
                    String getCountSLConLai = String.Format("select COUNT(*) as count from c_dongdonhang ddh where c_dongdonhang_id in (select c_dongdonhang_id from c_dongdsdh ddsdh where ddsdh.c_danhsachdathang_id = '{0}' and ddsdh.sl_conlai > ddh.soluong_conlai) and soluong_conlai < 1 ", piId);
                    int CountSLConLai = (int)mdbc.ExecuteScalar(getCountSLConLai);
                    if (CountSLConLai > 0)
                    {
                        msg += String.Format("<br>Hiệu lực thất bại! Trong danh sách đặt hàng {0}, có một hoặc một vài sản phẩm đã hết số lượng còn lại .!", dsdh.sochungtu);
                    }
                    else if (dsdh.trangthai != "DAHIEULUC" & arrNCC.Contains(ma_dtkd))
                    {
                        msg += String.Format(@"<br>Hiệu lực thất bại! {0} phải trong trạng thái ""Đã nhận""", dsdh.sochungtu);
                    }
                    else
                    {
                        c_kehoachxuathang kehoach = new c_kehoachxuathang();
                        //kehoach.c_kehoachxuathang_id = Security.EncodeMd5Hash(DateTime.Now.ToString());
                        kehoach.c_kehoachxuathang_id = ImportUtils.getNEWID();
                        kehoach.c_donhang_id = dsdh.c_donhang_id;
                        kehoach.chungloaihang = "";
                        kehoach.c_danhsachdathang_id = dsdh.sochungtu;
                        kehoach.md_doitackinhdoanh_id = dsdh.md_doitackinhdoanh_id;
                        kehoach.so_po = dsdh.so_po;
                        kehoach.shipmenttime = donhang.shipmenttime;
                        kehoach.ngaygiaohang = dsdh.hangiaohang_po;
                        kehoach.ngayxonghang = dsdh.hangiaohang_po;
                        kehoach.ngayxacnhanbaobi = dsdh.hangiaohang_po.Value.AddDays(-35);
                        kehoach.cont20 = 0;
                        kehoach.cont40 = 0;
                        kehoach.cont40hc = 0;
                        kehoach.md_trangthai_id = "SOANTHAO";
                        kehoach.ngaytao = DateTime.Now;
                        kehoach.ngaycapnhat = DateTime.Now;
                        kehoach.nguoitao = UserUtils.getUser(context);
                        kehoach.nguoicapnhat = UserUtils.getUser(context);
                        kehoach.hoatdong = true;

                        string sql_cbm = @"select SUM(tmp.cbm) 
										from (
											select case when cdg.sl_outer = 0 then 0
													else round(((cdg.l2 * cdg.h2 * cdg.w2) * cdh.sl_dathang/cdg.sl_outer)/1000000, 3) end as cbm
												from c_dongdsdh cdh, c_dongdonhang cdg
												where 
													cdh.c_dongdonhang_id = cdg.c_dongdonhang_id
													and c_danhsachdathang_id = '" + piId + "') as tmp";

                        decimal cbm = (decimal)mdbc.ExecuteScalar(sql_cbm);
                        kehoach.cbm = Math.Round(cbm, 3);
                        kehoach.cbm_conlai = Math.Round(cbm, 3); ;

                        string sql = @"select code_cl
                        from md_chungloai where md_chungloai_id
                        in(
	                        select distinct ms.md_chungloai_id
	                        from c_dongdsdh cnx, md_sanpham ms 
	                        where ms.md_sanpham_id = cnx.md_sanpham_id and 
		                        c_danhsachdathang_id in ('" + piId + "')" +
                                ")order by code_cl";
                        string cole_cl = " ";
                        DataTable com = mdbc.GetData(sql);
                        foreach (DataRow item in com.Rows)
                        {
                            cole_cl += item[0].ToString() + ", ";
                        }
                        kehoach.chungloaihang = cole_cl.Length > 2 ? cole_cl.Substring(0, cole_cl.Length - 2) : "";
                        db.c_kehoachxuathangs.InsertOnSubmit(kehoach);

                        dsdh.md_trangthai_id = "HIEULUC";
                        db.SubmitChanges();
                        //mdbc.ExcuteNonProcedure("p_hieuLucDsdh", "@dsdh_id", piId);

                        // Cập nhật  ismakepi cho đơn hàng nếu tất cả sản phẩm của đơn hàng đều nhỏ hơn 1
                        String getTongSlConLai = "select sum(soluong_conlai) from c_dongdonhang ddh where ddh.c_donhang_id = @c_donhang_id";
                        int c = int.Parse(mdbc.ExecuteScalar(getTongSlConLai, "@c_donhang_id", dsdh.c_donhang_id).ToString());
                        if (c < 1)
                        {
                            //c_donhang donhang = db.c_donhangs.FirstOrDefault(dh => dh.c_donhang_id.Equals(dsdh.c_donhang_id));
                            donhang.ismakepi = true;
                            db.SubmitChanges();
                            //mdbc.ExcuteNonProcedure("p_tinhCBMconlaicuaKHXH", "@invoiceid", piId);
                        }

                        msg += String.Format("<br>Hiệu lực danh sách đặt hàng {0} thành công.!", dsdh.sochungtu);
                    }
                }
                else
                {
                    msg += String.Format("<br>Hiện trạng thái danh sách đặt hàng {0} đã là hiệu lực.!", dsdh.sochungtu);
                }
            }
        }
        catch (SqlException ex)
        {
            msg = "Lỗi xảy ra khi hiệu lực PI" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_danhsachdathang_id, sochungtu from c_danhsachdathang where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        string huongdanlamhang = context.Request.Form["huongdanlamhang"];
        string huongdanlamhangchung = context.Request.Form["huongdanlamhangchung"];
        string msg = "";
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String isgui_hdlh = context.Request.Form["isgui_hdlh"].ToLower();
        if (isgui_hdlh.Equals("true"))
        {
            isgui_hdlh = "true";
        }
        else
        {
            isgui_hdlh = "false";
        }

        var user = UserUtils.getUser(db, context);
        c_danhsachdathang m = db.c_danhsachdathangs.Single(p => p.c_danhsachdathang_id == context.Request.Form["id"]);
        if (m.md_trangthai_id.Equals("SOANTHAO"))
        {
            if (string.IsNullOrWhiteSpace(m.trangthai) || new string[] { "CHUAGUI", "TUCHOI" }.Contains(m.trangthai) || (m.trangthai == "XACNHAN" & user.suaPI.GetValueOrDefault()))
            {
                m.ngaylap = DateTime.ParseExact(context.Request.Form["ngaylap"], "dd/MM/yyyy", null);
                m.hangiaohang_po = DateTime.ParseExact(context.Request.Form["hangiaohang_po"], "dd/MM/yyyy", null);
                m.nguoi_phutrach = UserUtils.getUser(context);
                //m.nguoi_dathang = UserUtils.getUser(context);

                m.huongdanlamhang = context.Request.Form["huongdanlamhang"];
                m.huongdanlamhangchung = context.Request.Form["huongdanlamhangchung"];

                m.diachigiaohang = context.Request.Form["diachigiaohang"];
                m.isgui_hdlh = bool.Parse(isgui_hdlh);
                m.discount = decimal.Parse(context.Request.Form["discount"]);

                m.mota = context.Request.Form["mota"];
                m.hoatdong = hd;
                m.nguoicapnhat = UserUtils.getUser(context);
                m.ngaycapnhat = DateTime.Now;
                db.SubmitChanges();
            }
            else if (user.suaPI.GetValueOrDefault())
            {
                m.huongdanlamhang = context.Request.Form["huongdanlamhang"];
                m.huongdanlamhangchung = context.Request.Form["huongdanlamhangchung"];
                db.SubmitChanges();
            }
            else
            {
                msg += String.Format("<br>Danh sách đặt hàng {0} không thể chỉnh sửa.!", m.sochungtu);
                jqGridHelper.Utils.writeResult(0, msg);
            }
        }
        else
        {
            if (!user.suaPI.GetValueOrDefault())
            {
                msg += String.Format("<br>Danh sách đặt hàng {0} đã Hiệu Lực không thể chỉnh sửa.!", m.sochungtu);
                jqGridHelper.Utils.writeResult(0, msg);
            }
            else
            {
                m.isgui_hdlh = bool.Parse(isgui_hdlh);
                m.huongdanlamhang = context.Request.Form["huongdanlamhang"];
                m.huongdanlamhangchung = context.Request.Form["huongdanlamhangchung"];
                db.SubmitChanges();
                //jqGridHelper.Utils.writeResult(0,"Không thể chỉnh sửa khi đặt hàng đã hiệu lực.!");
            }

        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_danhsachdathang mnu = new c_danhsachdathang
        {
            c_danhsachdathang_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            //sochungtu = context.Request.Form["sochungtu"],
            ngaylap = DateTime.ParseExact(context.Request.Form["ngaylap"], "dd/MM/yyyy", null),
            hangiaohang_po = DateTime.ParseExact(context.Request.Form["hangiaohang_po"], "dd/MM/yyyy", null),
            nguoi_phutrach = UserUtils.getUser(context),
            nguoi_dathang = UserUtils.getUser(context),
            diachigiaohang = context.Request.Form["diachigiaohang"],
            discount = decimal.Parse(context.Request.Form["discount"]),
            //c_donhang_id = context.Request.Form["c_donhang_id"],
            //so_po = context.Request.Form["so_po"],
            md_trangthai_id = "SOANTHAO",

            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.c_danhsachdathangs.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        var ds = db.c_danhsachdathangs.Single(p => p.c_danhsachdathang_id.Equals(id));
        bool duocXoa = Public.LaNguoiTao(context, ds.nguoitao, db);
        if (ds.md_trangthai_id.Equals("HIEULUC"))
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa khi đặt hàng đã hiệu lực.!");
        }
        else if (new string[] { "DAGUI", "DANHAN", "DAHIEULUC" }.Contains(ds.trangthai))
        {
            jqGridHelper.Utils.writeResult(0, "Đã gửi NCC, không thể xóa.!");
        }
        else if (duocXoa == false)
        {
            jqGridHelper.Utils.writeResult(0, string.Format(@"Chỉ tài khoản {0} hoặc admin mới có thể xóa.!", ds.nguoitao));
        }
        else
        {
            var ct = from p in db.c_dongdsdhs
                     where p.c_danhsachdathang_id == ds.c_danhsachdathang_id
                     select p;

            var pds = from p in db.c_phidathangs
                      where p.c_danhsachdathang_id == ds.c_danhsachdathang_id & p.hoatdong == true
                      select p;
            db.c_dongdsdhs.DeleteAllOnSubmit(ct);
            db.c_phidathangs.DeleteAllOnSubmit(pds);
            db.c_danhsachdathangs.DeleteOnSubmit(ds);
            db.SubmitChanges();
        }
    }

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

        // phân quyền theo nhóm
        String manv = UserUtils.getUser(context);
        String strAccount = "";
        System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
        foreach (String item in lstAccount)
        {
            strAccount += String.Format(", '{0}'", item);
        }
        strAccount = String.Format("'{0}'{1}", manv, strAccount);

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_danhsachdathang dsdh, c_donhang dh, md_doitackinhdoanh dtkd " +
            " WHERE dsdh.c_donhang_id = dh.c_donhang_id" +
            " {0} " +
            " {1} " +
            " AND dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id ";

        sqlCount = string.Format(sqlCount, isadmin == true ? "" : "AND dsdh.nguoitao IN(" + strAccount + ")", filter);

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
            sidx = "dsdh.c_danhsachdathang_id";
        }

        string strsql = "select *, (case when dcgh_1 is null then dcgh_2 else dcgh_1 end) as madtkd from( " +
                " select dsdh.c_danhsachdathang_id, dsdh.md_trangthai_id,dsdh.trangthai, dsdh.sochungtu sodsdh,dsdh.so_po as sodh, dh.customer_order_no, dsdh.ngaylap, " +
                " dsdh.hangiaohang_po, dtkd.ma_dtkd, " +
                " dsdh.nguoi_phutrach, " +
                " dsdh.nguoi_dathang, " +
                " (select dtkd_a.ma_dtkd from md_doitackinhdoanh dtkd_a where dtkd_a.md_doitackinhdoanh_id = dsdh.diachigiaohang) as dcgh_1, " +
                " dsdh.diachigiaohang as dcgh_2, " +
                " dsdh.huongdanlamhang,dsdh.huongdanlamhangchung, dsdh.discount, dsdh.isgui_hdlh, dsdh.ngaytao, " +
                " dsdh.nguoitao, dsdh.ngaycapnhat, dsdh.nguoicapnhat, dsdh.mota, dsdh.hoatdong, " +
                " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
                " FROM c_danhsachdathang dsdh, c_donhang dh, md_doitackinhdoanh dtkd " +
                " WHERE dsdh.c_donhang_id = dh.c_donhang_id " +
                " AND dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
                " {0} " +
                " {1} " +
                ")P WHERE RowNum > @start AND RowNum < @end";

        strsql = String.Format(strsql, isadmin == true ? "" : "AND dsdh.nguoitao IN(" + strAccount + ")", filter);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_danhsachdathang_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sodsdh"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sodh"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["customer_order_no"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["hangiaohang_po"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["madtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["discount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaylap"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";


            xml += "<cell><![CDATA[" + row["nguoi_phutrach"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoi_dathang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["huongdanlamhang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["huongdanlamhangchung"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isgui_hdlh"] + "]]></cell>";
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

    public string taoFileThongTinSanPham(HttpContext context, string manv, c_danhsachdathang dsdh)
    {
        String fileNameExcel = manv + "Thong-Tin-San-Pham-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + ".xls";
        //-- Tao Excel
        try
        {
            rptThongTinSanPham report = new rptThongTinSanPham();
            report.Parameters["so_dsdh"].Value = dsdh.sochungtu;
            report.Parameters["so_donhang"].Value = dsdh.so_po;
            SqlDataAdapter da = new SqlDataAdapter(getSqlThongTinSP(dsdh.c_danhsachdathang_id), mdbc.GetConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            report.DataSource = ds;
            report.DataAdapter = da;
            fileNameExcel = path + fileNameExcel;
            report.ExportToXls(fileNameExcel);
        }
        catch (Exception ex)
        {
            fileNameExcel = "";
            context.Response.Write(string.Format(@"<div>Không tạo được file Excel Thông tin sản phẩm</div><div style='margin-left:5px;'>Chi tiết: {0}</div>", ex.Message));
        }

        return fileNameExcel;
    }

    public List<string> taoFileDonDatHang(HttpContext context, string manv, string ma_dtkd, c_danhsachdathang dsdh, bool guiNCC)
    {
        //-Tao file dinh kem
        String fileName = manv + "Don-Dat-Hang-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + guiNCC.ToString();
        String fileNameXls = fileName + ".xls";
        String fileNamePdf = fileName + ".pdf";
        //-- Tao excel
        NPOI.HSSF.UserModel.HSSFWorkbook wb = null;

        try
        {
            var rptPI = new DanhSachDatHangReport(dsdh.c_danhsachdathang_id, logoPath, productImagePath);
            rptPI.guiNCC = guiNCC;
            wb = rptPI.CreateWBPI();
            wb = rptPI.ConfigExcel.setImageForExcel(wb, rptPI.Lstimage);
            fileNameXls = path + fileNameXls;
            var xfile = new FileStream(fileNameXls, FileMode.Create, FileAccess.ReadWrite);
            wb.Write(xfile);
            xfile.Close();
            xfile.Dispose();
        }
        catch (Exception ex)
        {
            fileNameXls = "";
            context.Response.Write(string.Format(@"<div>Không tạo được file Excel Đơn Đặt Hàng</div><div style='margin-left:5px;'>Chi tiết: {0}</div>", ex.Message));
        }

        //-- Tao PDF
        try
        {
            var rptPI = new DanhSachDatHangReport(dsdh.c_danhsachdathang_id, logoPath, productImagePath);
            rptPI.guiNCC = guiNCC;
            rptPI.TypePrint = "pdf";
            wb = rptPI.CreateWBPI();
            //wb = rptPI.ConfigExcel.setImageForExcel(wb, rptPI.Lstimage);

            var url = path + Guid.NewGuid().ToString() + ".xls";
            var xfile = new FileStream(url, FileMode.Create, FileAccess.ReadWrite);
            wb.Write(xfile);
            xfile.Close();
            xfile.Dispose();

            fileNamePdf = path + fileNamePdf;
            var result = OfficeToPDF.ExcelConverter.Convert(url, fileNamePdf, null, rptPI.Lstimage, rptPI.LstText);
            File.Delete(url);
            //rptDanhSachDatHang report = new rptDanhSachDatHang();
            //SqlDataAdapter da = new SqlDataAdapter(getSqlDsdhPdf(dsdh.c_danhsachdathang_id, guiNCC), mdbc.GetConnection);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //var tbl = ds.Tables[0];
            //if (tbl.Rows.Count > 0)
            //{
            //    string c_danhsachdathang_id = dsdh.c_danhsachdathang_id;
            //    tbl.Columns.Add("money", Type.GetType("System.String"));
            //    tbl.Columns.Add("mota_cong", Type.GetType("System.String"));
            //    tbl.Columns.Add("mota_tru", Type.GetType("System.String"));
            //    tbl.Columns.Add("discount", Type.GetType("System.Double"));
            //    tbl.Columns.Add("giatritang", Type.GetType("System.Double"));
            //    tbl.Columns.Add("giatrigiam", Type.GetType("System.Double"));
            //    tbl.Columns.Add("tongtiendatru", Type.GetType("System.Double"));
            //    //Header

            //    //Footer
            //    int lastRow = tbl.Rows.Count - 1;

            //    var strSumThanhTien = tbl.Compute("Sum(thanhtien)", string.Empty).ToString();
            //    double? total = null;
            //    if (!string.IsNullOrEmpty(strSumThanhTien))
            //        total = double.Parse(strSumThanhTien);

            //    var strDiscount = tbl.Rows[0]["discountdecimal"].ToString();
            //    double? discount_ = null;
            //    if (!string.IsNullOrEmpty(strDiscount))
            //        discount_ = double.Parse(strDiscount);

            //    var totalDiscount = total.GetValueOrDefault(0) * discount_.GetValueOrDefault(0) / 100;
            //    tbl.Rows[lastRow]["discount"] = totalDiscount;

            //    var phitang = (from t in db.c_phidathangs where t.isphicong.Equals(true) && t.c_danhsachdathang_id.Equals(c_danhsachdathang_id) select t.sotien).Sum();
            //    tbl.Rows[lastRow]["giatritang"] = phitang.GetValueOrDefault(0);

            //    var phigiam = (from t in db.c_phidathangs where t.isphicong.Equals(false) && t.c_danhsachdathang_id.Equals(c_danhsachdathang_id) select t.sotien).Sum();
            //    tbl.Rows[lastRow]["giatrigiam"] = phigiam.GetValueOrDefault(0);

            //    var varTang = db.c_phidathangs.FirstOrDefault(tg => tg.c_danhsachdathang_id.Equals(c_danhsachdathang_id) && tg.isphicong.Equals(true));
            //    var varGiam = db.c_phidathangs.FirstOrDefault(gi => gi.c_danhsachdathang_id.Equals(c_danhsachdathang_id) && gi.isphicong.Equals(false));

            //    string dgTang, dgGiam;
            //    dgTang = varTang == null ? "" : varTang.mota;
            //    dgGiam = varGiam == null ? "" : varGiam.mota;

            //    double? totalUSD = null;
            //    string textMoney = "";
            //    if (guiNCC == false)
            //    {
            //        totalUSD = total - (double)totalDiscount + (double)phitang.GetValueOrDefault(0) - (double)phigiam.GetValueOrDefault(0);
            //        if (totalUSD != null)
            //        {
            //            if (new string[] { "VINHGIA" }.Contains(ma_dtkd))
            //            {
            //                string m = MoneyToWord.ConvertMoneyToText(totalUSD.ToString()).Replace("Dollars", "");
            //                int j = m.LastIndexOf("and");

            //                if (m.Contains("Cents") | m.Contains("Cent"))
            //                {
            //                    m = m.Replace("Cents", "").Replace("Cent", "");
            //                    m = m.Insert(m.Length, "cents");
            //                }
            //                else
            //                {
            //                    m = m.Replace("Cents", "").Replace("Cent", "");
            //                }
            //                textMoney = m;
            //            }
            //            else
            //            {
            //                textMoney = MoneyToWord.ConvertMoneyToTextVND((decimal)totalUSD.GetValueOrDefault(0));
            //            }
            //            tbl.Rows[lastRow]["money"] = textMoney;
            //            tbl.Rows[lastRow]["tongtiendatru"] = totalUSD;
            //        }
            //    }

            //    string diengiai_cong = String.Format(@"select t.mota from c_phidathang t where t.isphicong = 1 and t.c_danhsachdathang_id = '{0}'", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);
            //    DataTable cong_ = mdbc.GetData(diengiai_cong);
            //    var mota_cong = new List<string>();
            //    for (int i_ = 0; i_ < cong_.Rows.Count; i_++)
            //    {
            //        mota_cong.Add(cong_.Rows[i_]["mota"].ToString());
            //    }

            //    string diengiai_tru = String.Format(@"select t.mota from c_phidathang t where t.isphicong = 0 and t.c_danhsachdathang_id = '{0}'", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);
            //    DataTable tru_ = mdbc.GetData(diengiai_tru);
            //    var mota_tru = new List<string>();
            //    for (int i_ = 0; i_ < tru_.Rows.Count; i_++)
            //    {
            //        mota_tru.Add(tru_.Rows[i_]["mota"].ToString());
            //    }
            //    tbl.Rows[lastRow]["mota_cong"] = string.Join(",", mota_cong);
            //    tbl.Rows[lastRow]["mota_tru"] = string.Join(",", mota_tru);
            //}
            //report.DataSource = ds;
            //report.DataAdapter = da;
            //fileNamePdf = path + fileNamePdf;
            //report.ExportToPdf(fileNamePdf);
        }
        catch (Exception ex)
        {
            fileNamePdf = "";
            context.Response.Write(string.Format(@"<div>Không tạo được file PDF Thông tin sản phẩm</div><div style='margin-left:5px;'>Chi tiết: {0}</div>", ex.Message));
        }

        var arrLst = new List<string>();
        arrLst.Add(fileNameXls);
        arrLst.Add(fileNamePdf);
        return arrLst;
    }

    public string taoFileXacNhanBaoBi(HttpContext context, string manv, c_danhsachdathang dsdh)
    {
        string pathInBaoBi = path + "AnCoInBaoBi" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
        try
        {
            string sql = String.Format(@"select 
						pi_.c_danhsachdathang_id, po.sochungtu , pi_.ngaylap, dt.ten_dtkd, pi_.c_danhsachdathang_id
					from 
						c_donhang po
						left join c_danhsachdathang pi_ on po.c_donhang_id = pi_.c_donhang_id
						left join md_doitackinhdoanh dt on pi_.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
					where 
						pi_.c_danhsachdathang_id = '{0}'", dsdh.c_danhsachdathang_id == null ? "" : dsdh.c_danhsachdathang_id);
            // tạo file excel
            rptXacNhanBaoBi report = new rptXacNhanBaoBi();
            SqlDataAdapter da = new SqlDataAdapter(sql, mdbc.GetConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            report.DataSource = ds;
            report.DataAdapter = da;
            report.ExportToXls(pathInBaoBi);
        }
        catch (Exception ex)
        {
            pathInBaoBi = "";
            context.Response.Write(string.Format(@"<div>Không tạo được file Excel Xác Nhận Bao Bì</div><div style='margin-left:5px;'>Chi tiết: {0}</div>", ex.Message));
        }

        return pathInBaoBi;
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
