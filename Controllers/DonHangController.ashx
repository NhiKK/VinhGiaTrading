<%@ WebHandler Language="C#" Class="DonHangController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
public class DonHangController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();
    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.Params["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "huypo":
                this.HuyDonHang(context);
                break;
            case "getpoinformation":
                this.getPoInformation(context);
                break;
            case "stoppo":
                this.stopPO(context);
                break;
            case "sendmail":
                this.sendMail(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "createpi":
                this.createPI(context);
                break;
            case "activepo":
                this.activePO(context);
                break;
            case "viewcapacity":
                this.viewCapacity(context);
                break;
            case "getdocument":
                this.getDocument(context);
                break;
            case "get_dsncc":
                this.get_dsncc(context);
                break;
            case "CopyDonHang":
                this.CopyDonHang(context);
                break;
            case "ChuyenDonHang":
                this.ChuyenDonHang(context);
                break;
            case "ReturnDonHang":
                this.ReturnDonHang(context);
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
    public void ReturnDonHang(HttpContext context)
    {
        try
        {
            String result = "", resultSuccess = "";
            String[] arrId = context.Request.QueryString["poId"].Split(',');

            foreach (var poId in arrId)
            {
                c_donhang donhang = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(poId));
                var checkDSDH = db.c_danhsachdathangs.Where(s => s.c_donhang_id == donhang.c_donhang_id).Count();

                if (donhang != null)
                {
                    String trangthai = donhang.md_trangthai_id;
                    switch (trangthai)
                    {
                        case "SOANTHAO":
                            result += "Hiện tại trạng thái đơn hàng đã là Soạn Thảo!<br/>";
                            break;
                        case "HIEULUC":
                            if (checkDSDH > 0)
                            {
                                result += donhang.sochungtu + " tồn tại DSDH!<br/>";
                                break;
                            }
                            else
                            {
                                donhang.md_trangthai_id = "SOANTHAO";
                                resultSuccess += donhang.sochungtu + " thành công trả về Soạn Thảo!<br/>";
                                string manv = UserUtils.getUser(context);
                                UserUtils.WriteLog(db, context, manv, DateTime.Now, "PO", donhang.sochungtu);
                            }
                            break;
                        default:
                            break;

                    }

                }
            }

            if (result.Length <= 0)
            {
                mdbc.ExcuteNonQuery("ALTER TABLE c_donhang DISABLE TRIGGER updateAmount");
                try
                {
                    db.SubmitChanges();
                    result = resultSuccess;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
                mdbc.ExcuteNonQuery("ALTER TABLE c_donhang ENABLE TRIGGER updateAmount");
            }

            context.Response.Write(result);
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.ToString());
        }
    }

    public static T Clone<T>(T source)
    {
        var dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
        using (var ms = new System.IO.MemoryStream())
        {
            dcs.WriteObject(ms, source);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            return (T)dcs.ReadObject(ms);
        }
    }

    public void ChuyenDonHang(HttpContext context)
    {
        string msg = "";
        string user = context.Request.Form["user"];
        try
        {
            user = user.Trim();
            var nv = db.nhanviens.Where(s => s.manv == user).FirstOrDefault();
            if (nv == null)
            {
                msg = string.Format("Không tìm thấy tài khoản có mã là {0}.", user);
                goto EndEventHandler;
            }

            string[] ids = context.Request.Params["id"].Split(',');
            foreach (var id in ids)
            {
                //Don hang
                var dh = db.c_donhangs.Where(p => p.c_donhang_id.Equals(id)).FirstOrDefault();
                if (dh != null)
                {
                    if (dh.nguoitao != nv.manv)
                    {
                        dh.nguoitao = nv.manv;
                        dh.nguoicapnhat = nv.manv;
                        dh.nguoilap = nv.manv;
                        //Dong don hang
                        var ddhs = db.c_dongdonhangs.Where(s => s.c_donhang_id == dh.c_donhang_id).ToList();
                        foreach (var ddh in ddhs)
                        {
                            ddh.nguoitao = nv.manv;
                            ddh.nguoicapnhat = nv.manv;
                        }
                        //Phi don hang
                        var pdhs = db.c_phidonhangs.Where(s => s.c_donhang_id == dh.c_donhang_id).ToList();
                        foreach (var pdh in pdhs)
                        {
                            pdh.nguoitao = nv.manv;
                            pdh.nguoicapnhat = nv.manv;
                        }
                    }
                    //Danh Sach Dat Hang
                    var dsdhs = db.c_danhsachdathangs.Where(s => s.c_donhang_id == dh.c_donhang_id).ToList();
                    foreach (var dsdh in dsdhs)
                    {
                        dsdh.nguoi_dathang = nv.manv;
                        dsdh.nguoi_phutrach = nv.manv;
                    }
                }
            }

            db.SubmitChanges();
        }
        catch (Exception e)
        {
            context.Response.Write(e.Message);
        }

    EndEventHandler:;
        context.Response.Write(msg);
    }

    public void CopyDonHang(HttpContext context)
    {
        try
        {
            string msg = "";
            string id = context.Request.QueryString["poId"];
            string kh = context.Request.QueryString["kh"];

            c_donhang dh = db.c_donhangs.Where(p => p.c_donhang_id.Equals(id)).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(kh) | kh == "[ALL]")
                kh = dh.md_doitackinhdoanh_id;
            string sqlCount = @"select count(b.trangthai) as count from c_dongdonhang a left join md_sanpham b on a.md_sanpham_id = b.md_sanpham_id where a.c_donhang_id  = '" + id + @"' and b.trangthai != 'DHD'";
            string sochungtu = "";
            int count = (int)mdbc.ExecuteScalar(sqlCount);
            if (dh == null)
            {
                msg = "<div style='color:red'>Lỗi: Không tìm thấy PO đã chọn.</div>";
            }
            else if (count > 0)
            {
                msg = "<div style='color:red'>Lỗi: PO có chứa mã hàng không hoạt động.</div>";
            }
            else
            {
                string id_new = ImportUtils.getNEWID();
                c_donhang dh_new = Clone(dh);
                dh_new.c_donhang_id = id_new;
                dh_new.md_doitackinhdoanh_id = kh;
                sochungtu = jqGridHelper.Utils.taoChungTuPO(dh_new.md_doitackinhdoanh_id, dh_new.donhang_mau == true ? 1 : 0, DateTime.Now.Year.ToString());
                dh_new.sochungtu = sochungtu;
                dh_new.md_trangthai_id = "SOANTHAO";
                dh_new.ismakepi = false;
                dh_new.ngaylap = DateTime.Now;
                dh_new.nguoilap = UserUtils.getUser(context);
                //--
                dh_new.ngaytao = DateTime.Now;
                dh_new.nguoitao = UserUtils.getUser(context);
                dh_new.ngaycapnhat = DateTime.Now;
                dh_new.nguoicapnhat = UserUtils.getUser(context);

                foreach (c_dongdonhang ddh in db.c_dongdonhangs.Where(s => s.c_donhang_id == dh.c_donhang_id))
                {
                    c_dongdonhang ddh_new = Clone(ddh);
                    ddh_new.c_dongdonhang_id = ImportUtils.getNEWID();
                    ddh_new.c_donhang_id = id_new;
                    ddh_new.soluong_dathang = 0;
                    ddh_new.soluong_conlai = ddh_new.soluong;
                    ddh_new.soluong_daxuat = 0;
                    //--
                    ddh_new.ngaytao = DateTime.Now;
                    ddh_new.nguoitao = UserUtils.getUser(context);
                    ddh_new.ngaycapnhat = DateTime.Now;
                    ddh_new.nguoicapnhat = UserUtils.getUser(context);
                    db.c_dongdonhangs.InsertOnSubmit(ddh_new);
                }

                foreach (c_phidonhang pdh in db.c_phidonhangs.Where(s => s.c_donhang_id == dh.c_donhang_id & s.hoatdong == true))
                {
                    c_phidonhang pdh_new = Clone(pdh);
                    pdh_new.c_phidonhang_id = ImportUtils.getNEWID();
                    pdh_new.c_donhang_id = id_new;
                    //--
                    pdh_new.ngaytao = DateTime.Now;
                    pdh_new.nguoitao = UserUtils.getUser(context);
                    pdh_new.ngaycapnhat = DateTime.Now;
                    pdh_new.nguoicapnhat = UserUtils.getUser(context);
                    db.c_phidonhangs.InsertOnSubmit(pdh_new);
                }

                db.c_donhangs.InsertOnSubmit(dh_new);
                db.SubmitChanges();
                msg = "<div style='color:blue'>Tạo mới PO \"" + sochungtu + "\" thành công.</div>";
            }
            context.Response.Write(msg);
        }
        catch (Exception e)
        {
            context.Response.Write(e.ToString());
        }
    }

    public void HuyDonHang(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        try
        {
            string c_donhang_id = context.Request.QueryString["poId"];
            c_donhang dh = db.c_donhangs.SingleOrDefault(p => p.c_donhang_id.Equals(c_donhang_id));
            if (dh != null)
            {

                if (dh.md_trangthai_id.Equals("HIEULUC"))
                {
                    var ddhs = from ddh in db.c_dongdonhangs where ddh.c_donhang_id.Equals(dh.c_donhang_id) select ddh.c_dongdonhang_id;

                    var ddsdhs = from ddsdh in db.c_dongdsdhs
                                 join dsdh in db.c_danhsachdathangs
                                     on ddsdh.c_danhsachdathang_id equals dsdh.c_danhsachdathang_id
                                 where
                                    ddhs.Contains(ddsdh.c_dongdonhang_id)
                                    && ddsdh.sl_dagiao < ddsdh.sl_dathang
                                    && dsdh.md_trangthai_id.Equals("HIEULUC")
                                 select ddsdh;

                    // Xoa danh sach dat hang soan thao
                    List<string> lstSoanThao = (from ddsdh in db.c_dongdsdhs
                                                join dsdh in db.c_danhsachdathangs
                                                    on ddsdh.c_danhsachdathang_id equals dsdh.c_danhsachdathang_id
                                                where
                                                   ddhs.Contains(ddsdh.c_dongdonhang_id)
                                                   && dsdh.md_trangthai_id.Equals("SOANTHAO")
                                                select ddsdh.c_danhsachdathang_id).ToList();


                    foreach (var c_danhsachdathang_id in lstSoanThao)
                    {
                        c_danhsachdathang dsdh = db.c_danhsachdathangs.SingleOrDefault(p => p.c_danhsachdathang_id.Equals(c_danhsachdathang_id));
                        if (dsdh != null)
                        {
                            var d_dsdhs = from ddsdh in db.c_dongdsdhs where ddsdh.c_danhsachdathang_id.Equals(dsdh.c_danhsachdathang_id) select ddsdh;
                            db.c_dongdsdhs.DeleteAllOnSubmit(d_dsdhs);
                            db.c_danhsachdathangs.DeleteOnSubmit(dsdh);
                        }
                    }

                    // # Xoa danh sach dat hang soan thao


                    // Set so luong huy cho cac danh sach dat hang hieu luc
                    List<string> lstDanhSachDatHang = new List<string>();
                    foreach (var ddsdh in ddsdhs)
                    {
                        ddsdh.sl_huy = ddsdh.sl_dathang - ddsdh.sl_dagiao;
                        lstDanhSachDatHang.Add(ddsdh.c_danhsachdathang_id);
                    }
                    // # Set so luong huy cho cac danh sach dat hang hieu luc

                    // Cap nhat trang thai cho danh sach dat hang
                    foreach (string c_danhsachdathang_id in lstDanhSachDatHang)
                    {
                        c_danhsachdathang dsdh = db.c_danhsachdathangs.SingleOrDefault(p => p.c_danhsachdathang_id.Equals(c_danhsachdathang_id));
                        if (dsdh != null)
                        {
                            dsdh.md_trangthai_id = "HUY";
                        }
                    }
                    // # Cap nhat trang thai cho danh sach dat hang

                    // Xóa các kế hoạch xuất hàng đang soạn thảo và kết thúc các kế hoạch đang hiệu lực
                    List<c_kehoachxuathang> lstKHXHXoa = new List<c_kehoachxuathang>();
                    foreach (string c_danhsachdathang_id in lstDanhSachDatHang)
                    {
                        c_danhsachdathang dsdh = db.c_danhsachdathangs.SingleOrDefault(p => p.c_danhsachdathang_id.Equals(c_danhsachdathang_id));
                        if (dsdh != null)
                        {
                            c_kehoachxuathang khxh = db.c_kehoachxuathangs.Single(p => p.c_danhsachdathang_id.Equals(dsdh.sochungtu));
                            if (khxh.md_trangthai_id.Equals("SOANTHAO"))
                            {
                                lstKHXHXoa.Add(khxh);
                            }
                            else if (khxh.md_trangthai_id.Equals("HIEULUC"))
                            {
                                khxh.md_trangthai_id = "KETTHUC";
                            }
                        }
                    }
                    // # Xóa các kế hoạch xuất hàng đang soạn thảo và kết thúc các kế hoạch đang hiệu lực

                    db.c_kehoachxuathangs.DeleteAllOnSubmit(lstKHXHXoa);
                    dh.md_trangthai_id = "HUY";
                    db.SubmitChanges();
                    context.Response.Write("Hủy đơn hàng thành công!");
                }
                else
                {
                    context.Response.Write("Chỉ có thể đơn hàng hiệu lực!");
                }
            }
            else
            {
                context.Response.Write("Không tìm thấy đơn hàng!");
            }
        }
        catch (Exception e)
        {
            context.Response.Write(e.Message);
        }
    }

    public void getPoInformation(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        string c_donhang_id = context.Request.QueryString["poId"];
        string msg = "";
        var helper = new VNN_Helper();
        try
        {
            decimal totalQuantity = 0, totalCbm = 0, totalCPDG = 0;
            decimal? phicong = 0, phitru = 0;
            c_donhang dh = db.c_donhangs.FirstOrDefault(d => d.c_donhang_id.Equals(c_donhang_id));
            if (dh != null)
            {
                helper.db = db;
                helper.dh = dh;
                var phiDH = from phi in db.c_phidonhangs where phi.c_donhang_id.Equals(dh.c_donhang_id) & phi.hoatdong == true select phi;
                phicong = phiDH.Where(p => p.phitang.Equals(true)).Select(p => p.phi).Sum();
                phitru = phiDH.Where(p => p.phitang.Equals(false)).Select(p => p.phi).Sum();

                var ddh = (
                        from c in db.c_dongdonhangs
                        join b in db.md_sanphams on c.md_sanpham_id equals b.md_sanpham_id
                        where c.c_donhang_id.Equals(dh.c_donhang_id)
                        select new VNN_Helper.dongDonHangVNN { c = c, code_cl = b.ma_sanpham.Substring(0, 2) }
                    ).ToList();

                totalQuantity = ddh.Sum(p => p.c.soluong).Value;

                totalCPDG = ddh.Sum(p => p.c.soluong.GetValueOrDefault(0) * p.c.chiphidonggoi.GetValueOrDefault(0));

                var isUSD = helper.laUSDHayVND();

                decimal total = helper.layTongGiaTriDonHang(ddh);

                string fmtAmount = string.Format("{0:#,##0.00}", total);
                if (!isUSD)
                {
                    total = Math.Ceiling(total);
                }

                phicong = phicong == null ? (decimal)0.00 : phicong.Value;
                phitru = phitru == null ? (decimal)0.00 : phitru.Value;
                total = total + phicong.Value - phitru.Value;
                total = total + totalCPDG;

                if (!isUSD)
                    fmtAmount = string.Format("{0:#,##0}", total);
                else
                    fmtAmount = string.Format("{0:#,##0.00}", total);


                totalCbm = ddh.Sum(s => s.c.v2.GetValueOrDefault(0));
                msg = string.Format("Đơn hàng:{0}, CBM:{1:#,##0.000}, Amount:{2}, Quantity:{3:#,##0}", dh.sochungtu, totalCbm, fmtAmount, totalQuantity);
            }
            else
            {
                msg = "Đơn hàng này đã bị xóa, hãy thử làm mới dữ liệu.";
            }
        }
        catch (Exception e)
        {
            msg = e.Message;
        }
        context.Response.Write(msg);
    }

    public void getDocument(HttpContext context)
    {
        try
        {
            string doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
            string dh_mau = context.Request.Form["isdonhangmau"].ToLower();
            string ngaydonhang = context.Request.Form["ngaydonhang"];
            DateTime ngaydh = DateTime.ParseExact(ngaydonhang, "dd/MM/yyyy", null);
            int isdh_mau;
            isdh_mau = 0;

            if (dh_mau.Equals("on") || dh_mau.Equals("true"))
            { isdh_mau = 1; }

            string sct = jqGridHelper.Utils.taoChungTuPO(doitackinhdoanh_id, isdh_mau, ngaydh.Year.ToString());
            context.Response.Write("<root><value>" + sct + "</value></root>");
        }
        catch (Exception e)
        {
            context.Response.Write("<root><value>" + e.Message + "</value></root>");
        }
    }

    public void stopPO(HttpContext context)
    {
        String msg = "";
        try
        {
            String c_donhang_id = context.Request.QueryString["poId"];
            c_donhang dh = db.c_donhangs.Single(p => p.c_donhang_id.Equals(c_donhang_id));

            if (dh != null)
            {
                switch (dh.md_trangthai_id)
                {
                    case "SOANTHAO":
                        msg = String.Format("Trạng thái đơn hàng {0} là Hiệu Lực mới có thể kết thúc.!", dh.sochungtu);
                        break;
                    case "KETTHUC":
                        msg = String.Format("Hiện tại trạng thái đơn hàng {0} đã là kết thúc.!", dh.sochungtu);
                        break;
                    case "HIEULUC":
                        dh.md_trangthai_id = "KETTHUC";
                        db.SubmitChanges();
                        msg = String.Format("Đã kết thúc đơn hàng {0} thành công.!", dh.sochungtu);
                        break;
                    default:
                        msg = String.Format("Không xác định được trạng thái của đơn hàng {0}.!", dh.sochungtu);
                        break;
                }
            }
            else
            {
                msg = "Đơn hàng không tồn tại.!";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        context.Response.Write(msg);
    }

    public void sendMail(HttpContext context)
    {
        md_mailtemplate tmp = db.md_mailtemplates.Single(p => p.use_for.Equals("PO") && p.default_mail.Equals(true));
        if (tmp != null)
        {
            String poId = context.Request.QueryString["poId"];
            c_donhang donhang = db.c_donhangs.Single(p => p.c_donhang_id.Equals(poId));
            md_doitackinhdoanh doitac = db.md_doitackinhdoanhs.Single(p => p.md_doitackinhdoanh_id.Equals(donhang.md_doitackinhdoanh_id));
            String str = String.Format("mailto:{0}?subject={1}&body={2}", doitac.email, tmp.subject_mail, tmp.content_mail.Replace("[SOPO]", donhang.sochungtu));
            context.Response.Write(str);
        }
    }

    public void viewCapacity(HttpContext context)
    {
        String poId = context.Request.QueryString["poId"];
        var donhang = db.c_donhangs.FirstOrDefault(dh => dh.c_donhang_id.Equals(poId));
        String result = "";
        if (donhang.isdonhangtmp.Equals(true))
        {
            result = "<div style=\"padding:15px; border:1px solid red; background:#ffadad;\"><h3>Không được xem năng lực đơn hàng tem.! " + "</h3></div>";
        }
        else
        {

            string ngaylap = donhang.ngaylap.Value.ToString("MM/dd/yyyy");
            String selCapacity = "select count(*) as count from dbo.f_tinhNangLucCungUng('" + poId + "') where result_nhandh =0 or result=0";
            int check = (int)mdbc.ExecuteScalar(selCapacity);
            String partnerId = db.c_donhangs.Single(dh => dh.c_donhang_id.Equals(poId)).md_doitackinhdoanh_id;
            System.Data.DataTable dt = mdbc.GetDataFromProcedure("tinhNangLucCungUng", "@donhang_id", poId);


            DateTime maxDate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);
            int colCount = dt.Columns.Count;
            int rowCount = dt.Rows.Count;
            if (rowCount > 0)
            {
                result += "<div style=\"height:450px; overflow:auto\"><table class=\"view-capacity\">";
                result += String.Format("<tr style=\"background:green; color:white\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>"
                        , "Đầu Mã", "Tên Nhóm Năng Lực", "Đối Tác", "Tổng CBM Cần Đặt", "CBM Đặt Được", "Thời Gian Dự Kiến", "Tuần Thứ", "Thời Gian Yêu Cầu", "Đánh Giá Năng Lực Cung Ứng");
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    result += "<tr>";
                    result += String.Format("<td>{0}</td>", row[0]);
                    result += String.Format("<td>{0}</td>", row[1]);
                    result += String.Format("<td>{0}</td>", row["ma_dtkd"]);
                    result += String.Format("<td>{0}</td>", row[2]);
                    result += String.Format("<td>{0}</td>", row[3]);
                    result += String.Format("<td>{0}</td>", DateTime.Parse(row[4].ToString()).ToString("dd/MM/yyyy"));
                    result += String.Format("<td>{0}</td>", row[5]);
                    result += String.Format("<td>{0}</td>", DateTime.Parse(row[6].ToString()).ToString("dd/MM/yyyy"));
                    //result += String.Format("<td>{0}</td>", row[7]);
                    //result += String.Format("<td>{0}</td>", row[8]);
                    //result += String.Format("<td><img src=\"iconcollection/{0}\"/></td>", row[9].ToString() == "True" ? "tick-icon.png" : "del.png");
                    result += String.Format("<td><img src=\"iconcollection/{0}\"/></td>", row[10].ToString() == "True" ? "tick-icon.png" : "del.png");
                    //result += String.Format("<td><img src=\"iconcollection/{0}\"/></td>", bool.Parse(row[10].ToString()) == true && bool.Parse(row[9].ToString()) == true ? "tick-icon.png" : "del.png");
                    result += "</tr>";

                    if (bool.Parse(row[10].ToString()))
                    {
                        DateTime ngaydukien = DateTime.Parse(row[4].ToString());
                        if (maxDate < ngaydukien)
                        {
                            maxDate = ngaydukien;
                        }
                    }
                }
                result += "</table></div>";


                //kiem tran xem co dat dc het sl  can dat ko
                //string sql_tongcandat = @"select sum(tmp.tong_cbmcandat) from (select distinct dauma, tong_cbmcandat from f_tinhNangLucCungUng('" + poId + "')) as tmp";

                //string sql_tongcandat = @"select sum(tmp.tong_cbmcandat) from (select distinct dauma, tong_cbmcandat from f_tinhNangLucCungUng('" + poId + "')) as tmp";

                if (check > 0)
                {
                    //tinh thoi gian du kien 
                    string sql_dk = @"select case when (tgdk < tgyc and tgnhandh > tgantoan_dh) then tgyc
										when (tgdk < tgyc and tgnhandh < tgantoan_dh) then ngay_dh + tgnhandh
										when (tgdk > tgyc and tgnhandh > tgantoan_dh) then tgdk
										else tgdk
										end
								from(
									 select MAX(thoigiandukien) tgdk, thoigianyeucau tgyc, max(tgnhan_dh) tgnhandh, max(tgantoan_dh) tgantoan_dh, cast('" + ngaylap + "' as datetime) as ngay_dh" +
                                         " from f_tinhNangLucCungUng('" + poId + "')" +
                                         " group by thoigianyeucau) as tmp";
                    DateTime ngaydk = (DateTime)mdbc.ExecuteScalar(sql_dk);
                    result = "<div style=\"padding:15px; border:1px solid red; background:#ffadad;\"><h3>Năng lực nhà cung ứng không đáp ứng được đơn hàng này.!<br/>Ngày dự kiến: " + ngaydk.ToString("dd/MM/yyyy") + "</h3></div>" + result;
                }
                else
                {
                    string sql_tongcandat = string.Format(@"SELECT sum(v2)  from c_dongdonhang ddh where ddh.c_donhang_id ='{0}'", poId);
                    string sql_slCandat = @"select isnull(SUM(cbm_candat), 0) from f_tinhNangLucCungUng('" + poId + "')";
                    string tong = mdbc.ExecuteScalar(sql_tongcandat).ToString();
                    decimal slTongCanDat = decimal.Parse(tong);
                    decimal slCanDat = decimal.Parse(mdbc.ExecuteScalar(sql_slCandat).ToString());

                    if (slCanDat < slTongCanDat)
                    {
                        result = "<div style=\"padding:15px; border:1px solid red; background:#ffadad;\"><h3>Năng lực nhà cung ứng không đáp ứng được đơn hàng này.!</h3>Năng lực Cung Ứng: " + slCanDat + ", CBM Đơn Hàng: " + slTongCanDat + "</div>" + result;
                    }
                    else
                    {
                        if (check == 0 && slCanDat >= slTongCanDat)
                        {
                            result = "<div style=\"padding:15px; border:1px sold green; background:#b9fdbf;\"><h3>Năng lực nhà cung ứng đáp ứng được đơn hàng này.!</h3>Năng lực Cung Ứng: " + slCanDat + ", CBM Đơn Hàng: " + slTongCanDat + "</div>" + result;
                        }
                        else
                        {
                            string sql_chk_lai_tongcandat = @"select isnull(sum(tmp.tong_cbmcandat), 0) from (select distinct dauma, tong_cbmcandat from f_tinhNangLucCungUng('" + poId + "')) as tmp";
                            decimal sl_chk_lai_TongCanDat = decimal.Parse(mdbc.ExecuteScalar(sql_chk_lai_tongcandat).ToString());

                            if (slCanDat >= sl_chk_lai_TongCanDat)
                            {
                                result = "<div style=\"padding:15px; border:1px solid green; background:#b9fdbf;\"><h3>Năng lực nhà cung ứng đáp ứng được đơn hàng này.!</h3>" + (slCanDat >= slTongCanDat) + "</div>" + result;
                            }
                            else
                            {
                                result = "<div style=\"padding:15px; border:1px solid red; background:#ffadad;\"><h3>Không đủ dữ liệu để tính năng lực.! </h3></div>" + result;
                            }
                        }
                    }
                }
            }
            else
            {
                result += "<div style=\"padding:15px; border:1px solid red; background:#b9fdbf;\">Không đủ dữ liệu để tính năng lực.!</div>";
            }
        }
        context.Response.Write(result);
    }

    public void createPI(HttpContext context)
    {
        String result = "";
        String poId = context.Request.QueryString["poId"];

        c_donhang donhang = db.c_donhangs.Single(p => p.c_donhang_id.Equals(poId));

        // String selCountPartner = String.Format(@"SELECT 
        // count(distinct sp.nhacungung) as Count 
        // FROM 
        // c_dongdonhang ddh
        // left join md_sanpham sp on ddh.md_sanpham_id = sp.md_sanpham_id 
        // WHERE 
        // c_donhang_id = '{0}'", poId);
        String selCountPartner = String.Format(@"SELECT 
									count(distinct ddh.nhacungungid) as Count 
									FROM 
									c_dongdonhang ddh
									WHERE
									c_donhang_id = '{0}'", poId);
        int countPartner = (int)mdbc.ExecuteScalar(selCountPartner);

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
                    String getTongSoLuongConLai = "select count(*) from c_dongdonhang where soluong_conlai < 1 AND c_donhang_id = @c_donhang_id";
                    int sum = (int)mdbc.ExecuteScalar(getTongSoLuongConLai, "@c_donhang_id", donhang.c_donhang_id);
                    if (sum == 0)
                    {
                        var dsSanPham = new List<dongDatHang>();
                        dsSanPham = db.c_dongdonhangs.Where(s => s.c_donhang_id == donhang.c_donhang_id)
                                .Select(s => new dongDatHang
                                {
                                    c_dongdonhang_id = s.c_dongdonhang_id,
                                    c_donhang_id = s.c_donhang_id,
                                    sanpham_id = s.md_sanpham_id,
                                    ma_sanpham = "",
                                    ma_sanpham_khach = s.ma_sanpham_khach,
                                    md_doitackinhdoanh_id = s.nhacungungid,
                                    soluong = s.soluong.GetValueOrDefault(0),
                                    soluong_dadat = s.soluong.GetValueOrDefault(0),
                                    soluong_conlai = s.soluong.GetValueOrDefault(0)
                                }).ToList();

                        result += UserUtils.createPI(context, donhang, dsSanPham, db);
                    }
                    else
                    {
                        result += string.Format("Tạo đơn đặt hàng thất bại! vì đơn hàng có mã hàng hết số lượng còn lại!");
                    }
                }
            }
        }
        context.Response.Write(result);
    }

    public string activePO_createPI(HttpContext context, c_donhang donhang)
    {
        String result = "";

        String selCountPartner = String.Format(@"SELECT 
									count(distinct ddh.nhacungungid) as Count 
									FROM 
									c_dongdonhang ddh
									WHERE
									c_donhang_id = '{0}'", donhang.c_donhang_id);
        int countPartner = (int)mdbc.ExecuteScalar(selCountPartner);

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
                    String getTongSoLuongConLai = "select count(*) from c_dongdonhang where soluong_conlai < 1 AND c_donhang_id = @c_donhang_id";
                    int sum = (int)mdbc.ExecuteScalar(getTongSoLuongConLai, "@c_donhang_id", donhang.c_donhang_id);
                    if (sum == 0)
                    {
                        var dsSanPham = new List<dongDatHang>();
                        dsSanPham = db.c_dongdonhangs.Where(s => s.c_donhang_id == donhang.c_donhang_id)
                                .Select(s => new dongDatHang
                                {
                                    c_dongdonhang_id = s.c_dongdonhang_id,
                                    c_donhang_id = s.c_donhang_id,
                                    sanpham_id = s.md_sanpham_id,
                                    ma_sanpham = "",
                                    ma_sanpham_khach = s.ma_sanpham_khach,
                                    md_doitackinhdoanh_id = s.nhacungungid,
                                    soluong = s.soluong.GetValueOrDefault(0),
                                    soluong_dadat = s.soluong.GetValueOrDefault(0),
                                    soluong_conlai = s.soluong.GetValueOrDefault(0)
                                }).ToList();

                        result += UserUtils.createPI(context, donhang, dsSanPham, db);
                    }
                    else
                    {
                        result += string.Format("Tạo đơn đặt hàng thất bại! vì đơn hàng có mã hàng hết số lượng còn lại!");
                    }
                }
            }
        }
        return result;
    }

    public void activePO(HttpContext context)
    {
        try
        {
            String result = "";
            String[] arrId = context.Request.QueryString["poId"].Split(',');

            foreach (var poId in arrId)
            {
                c_donhang donhang = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(poId));
                if (donhang != null)
                {
                    var donghangKoCoNCC = db.c_dongdonhangs.Where(s => s.c_donhang_id == donhang.c_donhang_id).Where(s => s.nhacungungid == null | s.nhacungungid == "").ToList();
                    if (donghangKoCoNCC.Count > 0)
                    {
                        result += string.Format("Đơn hàng <b>{0}</b> có mã hàng chưa được chỉ định <b>nhà cung cấp</b><br/>", donhang.sochungtu);
                        break;
                    }

                    var checkNCU = db.c_dongdonhangs.Where(s => s.c_donhang_id == donhang.c_donhang_id).Select(s => s.nhacungungid).Distinct().Count();

                    String trangthai = donhang.md_trangthai_id;
                    switch (trangthai)
                    {
                        case "HIEULUC":
                            result += "Hiện tại trạng thái đơn hàng đã là hiệu lực!<br/>";
                            break;
                        case "KETTHUC":
                            result += "Hiện tại trạng thái đơn hàng đã là kết thúc!<br/>";
                            break;
                        case "SOANTHAO":
                            if (donhang.cont20 <= 0 & donhang.cont40 <= 0 & donhang.cont40hc <= 0 & donhang.cont45 <= 0 & donhang.contle <= 0)
                            {
                                result += "Hiệu lực đơn hàng thất bại! Phải có ít nhất 1 cont lớn hơn 0. <br/>";
                            }
                            else if (donhang.ngaylap >= donhang.shipmenttime || donhang.ngaylap >= donhang.shipmentdate)
                            {
                                result += "Ngày lập PO không thể lớn hơn shipmenttime hoặc shipmentdate! <br/>";
                            }
                            else if (donhang.shipmentdate <= donhang.shipmenttime)
                            {
                                result += "Ngày Shipmentdate phải lớn hơn ngày Shipmenttime! <br/>";
                            }
                            else if (donhang.isdonhangtmp.Equals(true) | donhang.donhang_mau.Equals(true))
                            {
                                donhang.md_trangthai_id = "HIEULUC";
                                db.SubmitChanges();
                                result += "Hiệu lực đơn hàng thành công.! <br/>";

                                if (checkNCU == 1)
                                {
                                    result += activePO_createPI(context, donhang);
                                }
                            }
                            else
                            {
                                var user = UserUtils.getUser(db, context);
                                if (user.hieuLucDH.GetValueOrDefault(false))
                                {
                                    donhang.md_trangthai_id = "HIEULUC";
                                    db.SubmitChanges();
                                    result += "Hiệu lực đơn hàng thành công.! <br/>";

                                    if (checkNCU == 1)
                                    {
                                        result += activePO_createPI(context, donhang);
                                    }
                                }
                                else
                                {
                                    String partnerId = db.c_donhangs.Single(s => s.c_donhang_id.Equals(poId)).md_doitackinhdoanh_id;
                                    System.Data.DataTable dt = mdbc.GetDataFromProcedure("tinhNangLucCungUng", "@donhang_id", poId);
                                    int count = dt.Rows.Count;

                                    //string sql_TongCanDat = @"select sum(tmp.tong_cbmcandat) from (select distinct dauma, tong_cbmcandat from f_tinhNangLucCungUng('" + poId + "')) as tmp";
                                    string sql_TongCanDat = string.Format(@"select sum(v2)  from c_dongdonhang ddh where ddh.c_donhang_id ='{0}'", poId);
                                    string sql_slCandat = @"select isnull(SUM(cbm_candat), 0) from f_tinhNangLucCungUng('" + poId + "')";
                                    string tong = mdbc.ExecuteScalar(sql_TongCanDat).ToString();
                                    string tongCanDat = mdbc.ExecuteScalar(sql_slCandat).ToString();
                                    decimal slTongCanDat = string.IsNullOrWhiteSpace(tong) ? 0 : decimal.Parse(tong);
                                    decimal slCanDat = string.IsNullOrWhiteSpace(tongCanDat) ? 0 : decimal.Parse(tongCanDat);
                                    if (count != 0 && slCanDat >= slTongCanDat)
                                    {
                                        String selDetails = "select COUNT(1) as count from c_dongdonhang where c_donhang_id = @c_donhang_id";
                                        int countNL = (int)mdbc.ExecuteScalar(selDetails, "@c_donhang_id", poId);
                                        if (countNL > 0)
                                        {

                                            string sql_check = @"select COUNT(1) from [dbo].[f_tinhNangLucCungUng]('" + poId + "')" + " where result_nhandh = 0 or result = 0";
                                            int check = (int)mdbc.ExecuteScalar(sql_check);
                                            if (check == 0)
                                            {
                                                donhang.md_trangthai_id = "HIEULUC";
                                                db.SubmitChanges();
                                                mdbc.ExecuteScalarProcedure("p_bookNangLucCungUng", "@donhang_id", poId);
                                                result += "Hiệu lực đơn hàng thành công.! <br/>";
                                                if (checkNCU == 1)
                                                {
                                                    result += activePO_createPI(context, donhang);
                                                }
                                            }
                                            else
                                            {
                                                result += "đơn hàng không đủ năng lực ncc.! <br/>";
                                            }
                                        }
                                        else
                                        {
                                            result += "Đơn hàng chưa có dòng hàng.! <br/>";
                                        }
                                    }
                                    else
                                    {
                                        string sql_chk_lai_tongcandat = @"select isnull(sum(tmp.tong_cbmcandat), 0) from (select distinct dauma, tong_cbmcandat from f_tinhNangLucCungUng('" + poId + "')) as tmp";
                                        decimal sl_chk_lai_TongCanDat = decimal.Parse(mdbc.ExecuteScalar(sql_chk_lai_tongcandat).ToString());
                                        if (count != 0 && slCanDat >= slTongCanDat)
                                        {
                                            String selDetails = "select COUNT(1) as count from c_dongdonhang where c_donhang_id = @c_donhang_id";
                                            int countNL = (int)mdbc.ExecuteScalar(selDetails, "@c_donhang_id", poId);
                                            if (countNL > 0)
                                            {
                                                string sql_check = @"select COUNT(1) from [dbo].[f_tinhNangLucCungUng]('" + poId + "')" + " where result_nhandh = 0 or result = 0";
                                                int check = (int)mdbc.ExecuteScalar(sql_check);
                                                if (check == 0)
                                                {
                                                    donhang.md_trangthai_id = "HIEULUC";
                                                    db.SubmitChanges();
                                                    mdbc.ExecuteScalarProcedure("p_bookNangLucCungUng", "@donhang_id", poId);
                                                    result += "Hiệu lực đơn hàng thành công.! <br/>";

                                                    if (checkNCU == 1)
                                                    {
                                                        result += activePO_createPI(context, donhang);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                result += "Đơn hàng chưa có dòng hàng.! <br/>";
                                            }
                                        }
                                        else
                                        {
                                            result += "Năng lực của nhà cung ứng không đáp ứng đủ cho đơn hàng này.! <br/>";
                                            result += "<table><tr><td><input type='checkbox'/></td><td>Yêu cầu Administrator Hiệu Lực</td></tr></table>";
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            context.Response.Write(result);
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.ToString());
        }
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_donhang_id, sochungtu from c_donhang where hoatdong = 1";
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
            string msg = "";
            int check = 0;
            int donhangmau_ = 0;
            String dh_mau = context.Request.Form["donhang_mau"].ToLower();
            String dagui_mail = context.Request.Form["dagui_mail"].ToLower();
            String donhang_tmp = context.Request.Form["isdonhangtmp"].ToLower();
            String gia_db = context.Request.Form["gia_db"].ToLower();
            string chkPBGCu_str = context.Request.Form["chkPBGCu"];
            string phienbangiacu = context.Request.Form["phienbangiacu"];

            string phanbodiscountStr = (context.Request.Form["phanbodiscount"] + "").ToLower();
            var phanbodiscount = phanbodiscountStr.Equals("on") || phanbodiscountStr.Equals("true");

            bool isdh_mau, isdonhang_tmp, isgiadb, chkPBGCu = false;
            isdh_mau = isdonhang_tmp = isgiadb = false;
            decimal mnu_cont20 = 0;
            try { mnu_cont20 = decimal.Parse(context.Request.Form["cont20"]); } catch { }
            decimal mnu_cont40 = 0;
            try { mnu_cont40 = decimal.Parse(context.Request.Form["cont40"]); } catch { }
            decimal mnu_cont40hc = 0;
            try { mnu_cont40hc = decimal.Parse(context.Request.Form["cont40hc"]); } catch { }
            decimal mnu_cont45 = 0;
            try { mnu_cont45 = decimal.Parse(context.Request.Form["cont45"]); } catch { }
            decimal mnu_contle = 0;
            try { mnu_contle = decimal.Parse(context.Request.Form["contle"]); } catch { }


            if (chkPBGCu_str != null)
            {
                chkPBGCu_str = chkPBGCu_str.ToLower();
                if (chkPBGCu_str.Equals("on") || chkPBGCu_str.Equals("true"))
                { chkPBGCu = true; }
            }

            if (donhang_tmp.Equals("on") || donhang_tmp.Equals("true"))
            { isdonhang_tmp = true; donhangmau_ = 2; }

            if (dh_mau.Equals("on") || dh_mau.Equals("true"))
            { isdh_mau = true; donhangmau_ = 1; }

            if (gia_db.Equals("on") || gia_db.Equals("true"))
            { isgiadb = true; }

            if (dagui_mail.Equals("on") || dagui_mail.Equals("true"))
            {
                dagui_mail = "true";
            }
            else
            {
                dagui_mail = "false";
            }

            int countsct = (from dh in db.c_donhangs where dh.sochungtu.Equals(context.Request.Form["sochungtu"]) select new { dh.sochungtu }).Count();
            if (countsct > 0)
            {
                jqGridHelper.Utils.writeResult(0, "Số chứng từ đã tồn tại.");
            }
            else if (isdonhang_tmp && isdh_mau)
            {
                jqGridHelper.Utils.writeResult(0, "Tạo đơn hàng thất bại! chỉ được chọn hoặc đơn hàng tmp hoặt đơn hàng mẫu.");
            }
            else if (mnu_cont20 <= 0 & mnu_cont40 <= 0 & mnu_cont40hc <= 0 & mnu_cont45 <= 0 & mnu_contle <= 0)
            {
                jqGridHelper.Utils.writeResult(0, "Tạo đơn hàng thất bại! Phải có ít nhất 1 cont lớn hơn 0.");
            }
            else
            {
                string md_cangbien_id = context.Request.Form["md_cangbien_id"];
                md_cangbien cb = db.md_cangbiens.FirstOrDefault(p => p.md_cangbien_id.Equals(md_cangbien_id));
                md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(context.Request.Form["md_doitackinhdoanh_id"]));
                if (cb != null)
                {
                    c_donhang mnu = new c_donhang();
                    mnu.c_donhang_id = ImportUtils.getNEWID();

                    mnu.md_trangthai_id = "SOANTHAO";
                    mnu.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
                    mnu.c_baogia_id = context.Request.Form["c_baogia_id"];
                    mnu.ngaylap = DateTime.ParseExact(context.Request.Form["ngaylap"], "dd/MM/yyyy", null);
                    mnu.nguoilap = UserUtils.getUser(context);
                    mnu.md_cangbien_id = context.Request.Form["md_cangbien_id"];
                    mnu.discount = decimal.Parse(context.Request.Form["discount"]);
                    mnu.shipmentdate = DateTime.ParseExact(context.Request.Form["shipmentdate"], "dd/MM/yyyy", null);
                    mnu.shipmenttime = DateTime.ParseExact(context.Request.Form["shipmenttime"], "dd/MM/yyyy", null);
                    mnu.md_paymentterm_id = context.Request.Form["md_paymentterm_id"];
                    mnu.md_nganhang_id = context.Request.Form["md_nganhang_id"];
                    mnu.md_trongluong_id = context.Request.Form["md_trongluong_id"];
                    mnu.md_dongtien_id = context.Request.Form["md_dongtien_id"];
                    mnu.md_kichthuoc_id = context.Request.Form["md_kichthuoc_id"];
                    mnu.cont20 = decimal.Parse(context.Request.Form["cont20"]);
                    mnu.cont40 = decimal.Parse(context.Request.Form["cont40"]);
                    mnu.cont40hc = decimal.Parse(context.Request.Form["cont40hc"]);
                    mnu.cont45 = decimal.Parse(context.Request.Form["cont45"]);
                    mnu.contle = decimal.Parse(context.Request.Form["contle"]);
                    mnu.payer = context.Request.Form["payer"];
                    mnu.portdischarge = context.Request.Form["portdischarge"];
                    mnu.donhang_mau = isdh_mau;
                    mnu.isdonhangtmp = isdonhang_tmp;
                    mnu.dagui_mail = bool.Parse(dagui_mail);
                    mnu.hoahong = decimal.Parse(context.Request.Form["hoahong"]);
                    mnu.md_nguoilienhe_id = context.Request.Form["md_nguoilienhe_id"];
                    mnu.ismakepi = false;
                    mnu.customer_order_no = context.Request.Form["customer_order_no"];
                    mnu.gia_db = isgiadb;
                    mnu.chkPBGCu = chkPBGCu;
                    mnu.phienbangiacu = phienbangiacu;
                    mnu.ghichu = context.Request.Form["ghichu"];

                    if (!context.Request.Form["ngaydieuchinh"].Equals(""))
                    {
                        mnu.ngaydieuchinh = DateTime.ParseExact(context.Request.Form["ngaydieuchinh"], "dd/MM/yyyy", null);
                    }

                    mnu.md_banggia_id = context.Request.Form["md_banggia_id"];
                    mnu.sochungtu = jqGridHelper.Utils.taoChungTuPO(context.Request.Form["md_doitackinhdoanh_id"], donhangmau_, mnu.ngaylap.Value.Year.ToString());

                    mnu.phanbodiscount = phanbodiscount;
                    mnu.mota = context.Request.Form["mota"];
                    mnu.hoatdong = true;
                    mnu.ngaytao = DateTime.Now;
                    mnu.ngaycapnhat = DateTime.Now;
                    mnu.nguoitao = UserUtils.getUser(context);
                    mnu.nguoicapnhat = UserUtils.getUser(context);

                    md_doitackinhdoanh doitac = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(mnu.md_doitackinhdoanh_id));
                    md_banggia bga = db.md_banggias.Where(s => s.ten_banggia.Contains(doitac.ma_dtkd + "-FOB")).FirstOrDefault();
                    if (bga == null & isgiadb == true)
                    {
                        msg = "Khách hàng " + doitac.ma_dtkd + " không có bảng giá đặc biệt";
                        check++;
                    }
                    else if (mnu.ngaylap >= mnu.shipmentdate || mnu.ngaylap >= mnu.shipmenttime)
                    {
                        check++;
                        msg = "Ngày lập PO không được phép lớn hơn shipmenttime hoặc shipmentdate!";
                    }
                    else if (mnu.shipmentdate <= mnu.shipmenttime)
                    {
                        check++;
                        msg = "Ngày Shipmentdate phải lớn hơn Shipmenttime!";
                    }
                    else if (dtkd.ma_dtkd == "")
                    {
                        msg = "Khách Hàng không được để trống!";
                        check++;
                    }
                    else if (chkPBGCu == true & String.IsNullOrEmpty(phienbangiacu))
                    {
                        msg = "\"Phiên bảng giá cũ\" không thể bỏ trống.";
                        check++;
                    }

                    if (check == 0)
                    {
                        db.c_donhangs.InsertOnSubmit(mnu);
                        db.SubmitChanges();
                        jqGridHelper.Utils.writeResult(1, "Thêm mới thành công!");
                    }
                    else
                    {
                        jqGridHelper.Utils.writeResult(0, msg);
                    }
                }
                else
                {
                    jqGridHelper.Utils.writeResult(0, "Hãy chọn một cảng biển!");
                }
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
            string msg = "";
            int check = 0;
            String h = context.Request.Form["hoatdong"].ToLower();
            String dh_mau = context.Request.Form["donhang_mau"].ToLower();
            String dagui_mail = context.Request.Form["dagui_mail"].ToLower();
            String donhang_tmp = context.Request.Form["isdonhangtmp"].ToLower();
            string phanbodiscountStr = (context.Request.Form["phanbodiscount"] + "").ToLower();
            string sochungtu = context.Request.Form["sochungtu"];
            var phanbodiscount = phanbodiscountStr.Equals("on") || phanbodiscountStr.Equals("true");

            decimal mnu_cont20 = 0;
            try { mnu_cont20 = decimal.Parse(context.Request.Form["cont20"]); } catch { }
            decimal mnu_cont40 = 0;
            try { mnu_cont40 = decimal.Parse(context.Request.Form["cont40"]); } catch { }
            decimal mnu_cont40hc = 0;
            try { mnu_cont40hc = decimal.Parse(context.Request.Form["cont40hc"]); } catch { }
            decimal mnu_cont45 = 0;
            try { mnu_cont45 = decimal.Parse(context.Request.Form["cont45"]); } catch { }
            decimal mnu_contle = 0;
            try { mnu_contle = decimal.Parse(context.Request.Form["contle"]); } catch { }

            bool isdh_mau, isdonhang_tmp;
            isdh_mau = isdonhang_tmp = false;

            if (dh_mau.Equals("on") || dh_mau.Equals("true"))
            { isdh_mau = true; }


            if (donhang_tmp.Equals("on") || donhang_tmp.Equals("true"))
            { isdonhang_tmp = true; }


            if (dagui_mail.Equals("on") || dagui_mail.Equals("true"))
            {
                dagui_mail = "true";
            }
            else
            {
                dagui_mail = "false";
            }

            string md_cangbien_id = context.Request.Form["md_cangbien_id"];
            md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(context.Request.Form["md_doitackinhdoanh_id"]));
            md_cangbien cb = db.md_cangbiens.FirstOrDefault(p => p.md_cangbien_id.Equals(md_cangbien_id));
            c_donhang m = db.c_donhangs.Single(p => p.c_donhang_id == context.Request.Form["id"]);
            if (m.md_trangthai_id.Equals("HIEULUC"))
            {
                m.portdischarge = context.Request.Form["portdischarge"];
                m.mota = context.Request.Form["mota"];
                m.ghichu = context.Request.Form["ghichu"];
                db.SubmitChanges();
            }
            else if (mnu_cont20 <= 0 & mnu_cont40 <= 0 & mnu_cont40hc <= 0 & mnu_cont45 <= 0 & mnu_contle <= 0)
            {
                jqGridHelper.Utils.writeResult(0, "Tạo đơn hàng thất bại! Phải có ít nhất 1 cont lớn hơn 0.");
            }
            else if (sochungtu.Length <= 4 | sochungtu.LastIndexOf('/') <= -1)
            {
                jqGridHelper.Utils.writeResult(0, "Số chứng từ không đúng định dạng.");
            }
            else
            {
                var fistX1 = sochungtu.Split('/')[0];
                var fistX2 = m.sochungtu.Split('/')[0];
                var sctX1 = sochungtu.Substring(fistX1.Length);
                var sctX2 = m.sochungtu.Substring(fistX2.Length);
                if (sctX1 != sctX2)
                {
                    jqGridHelper.Utils.writeResult(0, string.Format(@"Số chứng từ phải có dạng như sau: {0}{1}", fistX1, sctX2));
                }
                else if (db.c_donhangs.Where(s => s.sochungtu == sochungtu & s.c_donhang_id != m.c_donhang_id).Take(1).Count() > 0)
                {
                    jqGridHelper.Utils.writeResult(0, string.Format(@"Số chứng từ ""{0}"" đã tồn tại", sochungtu));
                }
                else
                {
                    if (cb != null)
                    {
                        m.discount_hehang = context.Request.Form["discount_hehang"] == "True";
                        m.discount_hehang_value = m.discount_hehang.GetValueOrDefault(false) ? context.Request.Form["discount_hehang_value"] : "";
                        m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
                        m.sochungtu = sochungtu;
                        m.ngaylap = DateTime.ParseExact(context.Request.Form["ngaylap"], "dd/MM/yyyy", null);
                        m.nguoilap = UserUtils.getUser(context);
                        m.md_cangbien_id = context.Request.Form["md_cangbien_id"];
                        m.discount = decimal.Parse(context.Request.Form["discount"]);
                        m.shipmentdate = DateTime.ParseExact(context.Request.Form["shipmentdate"], "dd/MM/yyyy", null);
                        m.shipmenttime = DateTime.ParseExact(context.Request.Form["shipmenttime"], "dd/MM/yyyy", null);
                        m.md_paymentterm_id = context.Request.Form["md_paymentterm_id"];
                        m.md_nganhang_id = context.Request.Form["md_nganhang_id"];
                        m.md_trongluong_id = context.Request.Form["md_trongluong_id"];
                        m.md_dongtien_id = context.Request.Form["md_dongtien_id"];
                        m.md_kichthuoc_id = context.Request.Form["md_kichthuoc_id"];
                        m.payer = context.Request.Form["payer"];
                        m.portdischarge = context.Request.Form["portdischarge"];
                        m.sl_cont = 0;
                        m.cont20 = decimal.Parse(context.Request.Form["cont20"]);
                        m.cont40 = decimal.Parse(context.Request.Form["cont40"]);
                        m.cont40hc = decimal.Parse(context.Request.Form["cont40hc"]);
                        m.cont45 = decimal.Parse(context.Request.Form["cont45"]);
                        m.contle = decimal.Parse(context.Request.Form["contle"]);
                        m.customer_order_no = context.Request.Form["customer_order_no"];

                        m.md_banggia_id = context.Request.Form["md_banggia_id"];
                        m.donhang_mau = isdh_mau;
                        m.dagui_mail = bool.Parse(dagui_mail);
                        m.isdonhangtmp = isdonhang_tmp;
                        m.hoahong = decimal.Parse(context.Request.Form["hoahong"]);
                        m.md_nguoilienhe_id = context.Request.Form["md_nguoilienhe_id"];
                        m.ghichu = context.Request.Form["ghichu"];

                        if (!context.Request.Form["ngaydieuchinh"].Equals(""))
                        {
                            m.ngaydieuchinh = DateTime.ParseExact(context.Request.Form["ngaydieuchinh"], "dd/MM/yyyy", null);
                        }

                        int countsct = (from dh in db.c_donhangs where dh.sochungtu.Equals(context.Request.Form["sochungtu"]) && !dh.c_donhang_id.Equals(m.c_donhang_id) select new { dh.sochungtu }).Count();

                        m.mota = context.Request.Form["mota"];
                        m.phanbodiscount = phanbodiscount;
                        m.hoatdong = true;
                        m.ngaycapnhat = DateTime.Now;
                        m.nguoicapnhat = UserUtils.getUser(context);

                        if (countsct > 0)
                        {
                            msg = "Số chứng từ đã tồn tại";
                            check++;
                        }

                        if (m.ngaylap >= m.shipmenttime || m.ngaylap >= m.shipmentdate)
                        {
                            msg = "Ngày lập PO không thể lớn hơn shipmenttime hoặc shipmentdate!";
                            check++;
                        }

                        if (m.shipmentdate <= m.shipmenttime)
                        {
                            msg = "Ngày Shipmentdate phải lớn hơn ngày Shipmenttime!";
                            check++;
                        }

                        if (dtkd.ma_dtkd == "")
                        {
                            msg = "Khách Hàng không được để trống!";
                            check++;
                        }

                        if (m.md_trangthai_id.Equals("KETTHUC"))
                        {
                            msg = "Không thể sửa PO đã kết thúc!";
                            check++;
                        }

                        var sctLength = sochungtu.Length;
                        if (!sochungtu.Substring(sctLength - 6).Contains("-VG-"))
                        {
                            if (!sochungtu.Substring(sctLength - 9).Contains("-VG_SR-"))
                            {
                                if (!sochungtu.Substring(sctLength - 9).Contains("-VG_TI-"))
                                {
                                    msg = @"Số chứng từ phải có kí hiệu ""-VG-"", ""-VG_SR-"" hoặc ""-VG_TI-"" ở các kí tự cuối";
                                    check++;
                                }
                            }
                        }

                        if (check == 0)
                        {
                            db.SubmitChanges();

                            if (m.md_trangthai_id == "SOANTHAO")
                            {
                                if (m.discount_hehang.GetValueOrDefault(false))
                                {

                                    foreach (var cdh in db.c_dongdonhangs.Where(s => s.c_donhang_id == m.c_donhang_id).ToList())
                                    {
                                        var t = Public.DiscountHehangValue(m.discount_hehang_value, cdh, db);
                                        cdh.giafob = t.giafob;
                                        cdh.discount = t.discount;
                                    }
                                }
                                else
                                {
                                    foreach (var cdh in db.c_dongdonhangs.Where(s => s.c_donhang_id == m.c_donhang_id).ToList())
                                    {
                                        cdh.giafob = cdh.giachuan == null ? cdh.giafob : cdh.giachuan;
                                        cdh.discount = 0;
                                    }
                                }
                            }

                            db.SubmitChanges();
                            jqGridHelper.Utils.writeResult(1, "Cập nhật thành công!");
                        }
                        else
                        {
                            jqGridHelper.Utils.writeResult(0, msg);
                        }
                    }
                    else
                    {
                        jqGridHelper.Utils.writeResult(0, "Hãy chọn một cảng biển!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            jqGridHelper.Utils.writeResult(0, ex + "");
        }
    }

    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        c_donhang donhang = db.c_donhangs.FirstOrDefault(i => i.c_donhang_id.Equals(id));
        string msg = "";
        // id = id.Insert(0, "'");
        // id = id.Insert(id.Length, "'");
        // id = id.Replace(",", "','");
        try
        {
            if (donhang != null)
            {
                bool duocXoa = Public.LaNguoiTao(context, donhang.nguoitao, db);
                if (
                    donhang.md_trangthai_id.Equals("HIEULUC") |
                    donhang.md_trangthai_id.Equals("HUY") |
                    donhang.md_trangthai_id.Equals("KETTHUC")
                )
                {
                    msg = "Chỉ có thể xóa đơn hàng ở trạng thái \"Soạn Thảo\"!";
                    jqGridHelper.Utils.writeResult(0, msg);
                }
                else if (duocXoa == false)
                {
                    jqGridHelper.Utils.writeResult(0, string.Format(@"Chỉ tài khoản {0} hoặc admin mới có thể xóa.!", donhang.nguoitao));
                }
                else
                {
                    // String sql = "delete c_donhang where c_donhang_id IN (" + id + ")";
                    // String sqlDetails = "delete c_dongdonhang where c_donhang_id IN (" + id + ")";
                    // mdbc.ExcuteNonQuery(sqlDetails);
                    // mdbc.ExcuteNonQuery(sql);

                    db.c_donhangs.DeleteOnSubmit(donhang);
                    db.SubmitChanges();

                    foreach (c_dongdonhang ddh in db.c_dongdonhangs.Where(s => s.c_donhang_id == id))
                    {
                        db.c_dongdonhangs.DeleteOnSubmit(ddh);
                    }
                    db.SubmitChanges();
                    jqGridHelper.Utils.writeResult(1, "Xóa thành công!");
                }
            }
            else
            {
                jqGridHelper.Utils.writeResult(1, "Không tìm tháy đơn hàng, vui lòng làm mới dữ liệu!");
            }
        }
        catch (Exception ex)
        {
            jqGridHelper.Utils.writeResult(0, "Fail!" + ex.Message);
        }
    }
    #endregion

    #region load
    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        bool isadmin = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(context))).isadmin.Value;

        String md_trangthai_id = context.Request.QueryString["status"];
        md_trangthai_id = md_trangthai_id == null ? "" : md_trangthai_id.ToUpper();

        String fStatus = "";

        if (md_trangthai_id.Equals("ALL"))
        {
            fStatus = "";
        }
        else
        {
            fStatus = String.Format("AND dh.md_trangthai_id = N'{0}' ", md_trangthai_id);
        }

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


        String sqlCount = @"SELECT COUNT(1) AS count 
            FROM c_donhang dh with (nolock)
			left join md_doitackinhdoanh dtkd with (nolock) on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			left join md_cangbien cbien with (nolock) on dh.md_cangbien_id = cbien.md_cangbien_id
            left join md_paymentterm pmt with (nolock) on dh.md_paymentterm_id = pmt.md_paymentterm_id
			left join md_trongluong tl with (nolock) on dh.md_trongluong_id = tl.md_trongluong_id
			left join md_kichthuoc kt with (nolock) on dh.md_kichthuoc_id = kt.md_kichthuoc_id
			left join md_dongtien dtien with (nolock) on dh.md_dongtien_id = dtien.md_dongtien_id
			left join md_nganhang ngh with (nolock) on dh.md_nganhang_id = ngh.md_nganhang_id
            WHERE 1 = 1 {0} " + fStatus + " {1}";

        sqlCount = String.Format(sqlCount, isadmin == true ? "" : "AND dh.nguoitao IN(" + strAccount + ")", filter);

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
            sidx = "dh.c_donhang_id";
        }

        string strsql = @"select * from( 
            select dh.c_donhang_id, dh.md_trangthai_id, dh.donhang_mau, dh.sochungtu, dh.customer_order_no, 
            dtkd.ma_dtkd, dh.ngaylap, dh.nguoilap, 
            cbien.ten_cangbien, dh.discount, dh.shipmenttime, 
            dh.shipmentdate, pmt.ten_paymentterm, ngh.ma_nganhang,  tl.ten_trongluong, 
            dtien.ma_iso as tendongtien, kt.ten_kichthuoc, 
            dh.payer, dh.portdischarge, 
			(
				dh.amount - (dh.amount * dh.discount)/100 + 
				isnull((select SUM(phi) from c_phidonhang where phitang = 1 and c_donhang_id = dh.c_donhang_id and isnull(hoatdong, 0) = 1), 0) -
				isnull((select SUM(phi) from c_phidonhang where phitang = 0 and c_donhang_id = dh.c_donhang_id and isnull(hoatdong, 0) = 1), 0)
			) as amount,
            dh.cpdg_vuotchuan as cpdg_vuotchuan2,
            dh.totalcbm, dh.totalcbf, dh.dagui_mail, 
            dh.cont20, dh.cont40, dh.cont40hc, dh.contle, dh.cont45, 
            dh.ismakepi, dh.isdonhangtmp, 
            (case when md_nguoilienhe_id is null then '' else (select ma_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = dh.md_nguoilienhe_id) end) as nguoilienhe, 
            dh.hoahong, dh.ngaydieuchinh, 
            dh.ngaytao, dh.nguoitao, dh.ngaycapnhat, dh.nguoicapnhat, dh.mota, dh.hoatdong, 
            dh.gia_db,
            dh.chkPBGCu,
            dh.phienbangiacu,
            dh.ghichu,
            dh.phanbodiscount,
            dh.discount_hehang,
            dh.discount_hehang_value,
            ROW_NUMBER() OVER (ORDER BY {2} {3}) as RowNum 
            FROM c_donhang dh with (nolock)
			left join md_doitackinhdoanh dtkd with (nolock) on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			left join md_cangbien cbien with (nolock) on dh.md_cangbien_id = cbien.md_cangbien_id
            left join md_paymentterm pmt with (nolock) on dh.md_paymentterm_id = pmt.md_paymentterm_id
			left join md_trongluong tl with (nolock) on dh.md_trongluong_id = tl.md_trongluong_id
			left join md_kichthuoc kt with (nolock) on dh.md_kichthuoc_id = kt.md_kichthuoc_id
			left join md_dongtien dtien with (nolock) on dh.md_dongtien_id = dtien.md_dongtien_id
			left join md_nganhang ngh with (nolock) on dh.md_nganhang_id = ngh.md_nganhang_id
            WHERE 1 = 1 {0} {4} {1} )P where RowNum > @start AND RowNum < @end";

        strsql = String.Format(strsql,
            isadmin == true ? "" : "AND dh.nguoitao IN(" + strAccount + ")",
            filter, sidx, sord, fStatus);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_donhang_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["customer_order_no"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_cangbien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["shipmenttime"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalcbm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["amount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cpdg_vuotchuan2"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["discount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_paymentterm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_nganhang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoahong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoilienhe"] + "]]></cell>";

            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaylap"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoilap"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["shipmentdate"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + ((!row["ngaydieuchinh"].ToString().Equals("")) ? DateTime.Parse(row["ngaydieuchinh"].ToString()).ToString("dd/MM/yyyy") : "") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["donhang_mau"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isdonhangtmp"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_trongluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tendongtien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_kichthuoc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["payer"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["portdischarge"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalcbf"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cont20"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cont40"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cont40hc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cont45"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["contle"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["dagui_mail"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ismakepi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["gia_db"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chkPBGCu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phienbangiacu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghichu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phanbodiscount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["discount_hehang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["discount_hehang_value"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }
    #endregion

    public void get_dsncc(HttpContext context)
    {
        string json = "[";
        string c_donhang_id = context.Request.QueryString["c_donhang_id"];
        var dtkds = from a in db.c_dongdonhangs
                    join b in db.md_doitackinhdoanhs on a.nhacungungid equals b.md_doitackinhdoanh_id
                    where a.c_donhang_id == c_donhang_id
                    select new { b.md_doitackinhdoanh_id, b.ma_dtkd };

        foreach (var row in dtkds.Distinct().OrderBy(s => s.ma_dtkd))
        {
            json += string.Format(@"{{""id"":""{0}"", ""ma_dtkd"":""{1}"", ""tien"":""{2}""}},", row.md_doitackinhdoanh_id, row.ma_dtkd, 0);
        }
        if (json != "[")
        {
            json = json.Substring(0, json.Length - 1);
        }
        json += "]";

        context.Response.Write(json);
    }

    #endregion

    public bool IsReusable
    {
        get { return false; }
    }
}
