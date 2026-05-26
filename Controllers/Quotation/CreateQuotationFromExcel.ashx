<%@ WebHandler Language="C#" Class="CreateQuotationFromExcel" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExcelLibrary.SpreadSheet;

public class CreateQuotationFromExcel : IHttpHandler
{
    public class HangHoaExcel
    {
        public string ma_sanpham { get; set; }
        public string docquyen { get; set; }
    }

    private LinqDBDataContext db = new LinqDBDataContext();
    string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    string filePath = "", filenameLC = "";
    List<c_chitietbaogia> lstStr = new List<c_chitietbaogia>();
    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.QueryString["oper"];
        switch (oper)
        {
            default:
                this.exec(context);
                break;
        }
    }

    public string NewId()
    {
        return Guid.NewGuid().ToString().Replace("-", "").ToLower();
    }

    public void exec(HttpContext context)
    {
        string msg = "";
        try
        {
            string khachhang_v = context.Request.Form["khachhang_v"];
            string ngaybaogia_v = context.Request.Form["ngaybaogia_v"];
            string shipmenttime = context.Request.Form["shipmenttime"];
            string ngayhethan_v = context.Request.Form["ngayhethan_v"];
            string ptrongluong_v = context.Request.Form["ptrongluong_v"];
            string pdongtien_v = context.Request.Form["pdongtien_v"];
            string pkichthuoc_v = context.Request.Form["pkichthuoc_v"];
            string gia_db_str = context.Request.Form["gia_db"].removeAllSpaceOrTrimText(false).ToLower();
            string chkPBGCu_str = context.Request.Form["chkPBGCu"].removeAllSpaceOrTrimText(false).ToLower();
            string phienbangiacu = context.Request.Form["phienbangiacu"];
            string cangbien = context.Request.Form["cangbien"];
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                filenameLC = files[0].FileName;
                filePath = context.Server.MapPath("../../VNN_Files/" + id + files[0].FileName);
                files[0].SaveAs(filePath);
            }
            else
            {
                msg = "Không tìm thấy tập tin.";
            }

            bool gia_db = false, chkPBGCu = false;
            if (chkPBGCu_str.Equals("on") || chkPBGCu_str.Equals("true"))
            { chkPBGCu = true; }
            if (gia_db_str.Equals("on") || gia_db_str.Equals("true"))
            { gia_db = true; }


            if (msg.Length <= 0)
            {
                #region add c_baogia
                String sochungtu = jqGridHelper.Utils.taoChungTuQO(khachhang_v, 1, DateTime.ParseExact(ngaybaogia_v, "dd/MM/yyyy", null).Year.ToString());
                c_baogia mnu = new c_baogia
                {
                    c_baogia_id = id,
                    sobaogia = sochungtu,
                    md_doitackinhdoanh_id = khachhang_v,
                    shipmenttime = int.Parse(shipmenttime),
                    ngaybaogia = DateTime.ParseExact(ngaybaogia_v, "dd/MM/yyyy", null),
                    ngayhethan = DateTime.ParseExact(ngayhethan_v, "dd/MM/yyyy", null),
                    md_trongluong_id = ptrongluong_v,
                    md_cangbien_id = cangbien,
                    md_dongtien_id = pdongtien_v,
                    md_kichthuoc_id = pkichthuoc_v,
                    totalcbm = 0,
                    totalcbf = 0,
                    totalquo = 0,
                    isform = true,
                    moq_item_color = "",
                    md_trangthai_id = "SOANTHAO",
                    chkPBGCu = chkPBGCu,
                    phienbangiacu = phienbangiacu,

                    nguoitao = UserUtils.getUser(context),
                    nguoicapnhat = UserUtils.getUser(context),
                    mota = "",
                    hoatdong = true,
                    gia_db = gia_db,
                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now
                };

                if (mnu.ngaybaogia >= mnu.ngayhethan)
                {
                    msg = "Ngày báo giá không đuợc lớn hơn ngày hết hạn!";
                }
                else if (mnu.md_doitackinhdoanh_id == "c010dd297f3ee5797e426176e8685b2c")
                {
                    msg = "Khách Hàng: Không được bỏ trống!";
                }
                else if (chkPBGCu == true & String.IsNullOrEmpty(phienbangiacu))
                {
                    msg = "\"Phiên bảng giá cũ\" không thể bỏ trống.";
                }

                md_doitackinhdoanh doitac = db.md_doitackinhdoanhs.FirstOrDefault(p => p.md_doitackinhdoanh_id.Equals(mnu.md_doitackinhdoanh_id));
                md_banggia bga = db.md_banggias.Where(s => s.ten_banggia.Contains(doitac.ma_dtkd + "-FOB")).FirstOrDefault();

                if (bga == null & gia_db == true)
                {
                    msg = "Khách hàng " + doitac.ma_dtkd + " không có bảng giá đặc biệt";
                }

                if (msg.Length <= 0)
                {
                    db.c_baogias.InsertOnSubmit(mnu);
                    db.SubmitChanges();
                    var result = ReadExCel(context);
                    msg = result["msg"];
                    var ok = result["ok"] == "true";
                    if (ok)
                    {
                        msg += string.Format(@"<div style='color:blue'>Đã tạo thành công báo giá ""{0}""</div>", sochungtu);
                        db.SubmitChanges();
                    }
                    else
                    {
                        msg += string.Format(@"<div style='color:red'>Báo giá khởi tạo thất bại ""{0}""</div>", sochungtu);
                        db.c_baogias.DeleteOnSubmit(mnu);
                        db.SubmitChanges();
                    }
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        context.Response.Write(msg);
    }

    public Dictionary<string, string> ReadExCel(HttpContext context)
    {
        var result = new Dictionary<string, string>();
        string msg = "";
        bool ok = true;
        try
        {
            int j = filePath.LastIndexOf(".");
            if (filePath.Substring(j) != ".xls")
            {
                filePath = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(filePath);
            }
            Workbook wb = Workbook.Load(filePath);
            Worksheet ws = wb.Worksheets[0];
            msg = this.NewFromCellCollection(context, ws.Cells);
            if (msg.StartsWith("false#"))
            {
                ok = false;
                msg = msg.Substring(6);
            }
        }
        catch (Exception ex)
        {
            ok = false;
            msg = ex.Message;
        }
        result["msg"] = msg;
        result["ok"] = ok.ToString().ToLower();
        return result;
    }


    public string NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        int totalCount = cellCollection.Rows.Count;
        int rowErr = 0, rowAdd = 0;
        string msg = "";
        bool choPhepImport = true;

        var msps = new List<HangHoaExcel>();
        for (int i = 1; i < totalCount; i++)
        {
            Row row = cellCollection.Rows[i];

            if (row.GetCell(0) != null)
            {
                var item = new HangHoaExcel();
                item.ma_sanpham = row.GetCell(0).Value.ToString().Replace(" ", ""); ;
                item.docquyen = row.GetCell(1).Value.ToString();

                if (msps.Where(s => s.ma_sanpham == item.ma_sanpham).Count() <= 0)
                    msps.Add(item);
            }
        }

        string khachhang_id = context.Request.Form["khachhang_v"];
        var dem = 0;
        foreach (var msp in msps)
        {
            dem++;
            string ma_sanpham = msp.ma_sanpham, docquyen = "", kiemtraDQ = msp.docquyen, ma_khach = "", ma_donggoi = "";
            decimal soluong = 1, giafob = 0;
            try
            {
                md_sanpham sp = db.md_sanphams.FirstOrDefault(s => s.ma_sanpham == ma_sanpham);
                if (sp != null)
                {
                    if (!new string[] { "NHD", "HHT", "SOANTHAO" }.Contains(sp.trangthai))
                    {
                        try
                        {
                            var itemJS = UserUtils.get_giasanpham(context, id, sp.md_sanpham_id, db);
                            giafob = decimal.Parse(itemJS["gia"].ToString());
                            ma_khach = itemJS["ma_khach"].ToString();
                            ma_donggoi = itemJS["ma_donggoi"].ToString();
                        }
                        catch
                        {
                            giafob = -1;
                        }

                        if (giafob > -1)
                        {
                            docquyen = check_hanghoadocquyen(sp.md_sanpham_id, khachhang_id);
                            if (kiemtraDQ != "1" & docquyen != "")
                            {
                                var docquyenRutGon = docquyen.Split(new string[] { "\n" }, StringSplitOptions.None)[0];
                                docquyenRutGon = docquyenRutGon.Replace("Mã hàng độc quyền ", "");
                                choPhepImport = false;
                                msg += string.Format(@"<div style='color:red'>Sản phẩm ""{0}"" được bán độc quyền {1}.</div>", ma_sanpham, docquyenRutGon);
                            }
                            else
                            {
                                #region them dong don hang
                                var bg = db.c_baogias.Where(s => s.c_baogia_id == id).FirstOrDefault();
                                var donggoi = (from a in db.md_donggoisanphams
                                               join b in db.md_donggois on a.md_donggoi_id equals b.md_donggoi_id
                                               where a.md_sanpham_id == sp.md_sanpham_id & a.macdinh == true
                                               select new
                                               {
                                                   b.md_donggoi_id,
                                                   b.doigia_donggoi,
                                                   b.ghichu_vachngan,
                                                   b.sl_inner,
                                                   b.l1,
                                                   b.w1,
                                                   b.h1,
                                                   b.sl_outer,
                                                   b.l2_mix,
                                                   b.w2_mix,
                                                   b.h2_mix,
                                                   b.v2,
                                                   b.vd,
                                                   b.vn,
                                                   b.vl,
                                                   b.soluonggoi_ctn,
                                                   b.mota
                                               }).FirstOrDefault();
                                if (bg.gia_db == true)
                                    donggoi = (from a in db.md_donggoisanphams
                                               join b in db.md_donggois on a.md_donggoi_id equals b.md_donggoi_id
                                               where a.md_sanpham_id == sp.md_sanpham_id & b.ma_donggoi == ma_donggoi
                                               select new
                                               {
                                                   b.md_donggoi_id,
                                                   b.doigia_donggoi,
                                                   b.ghichu_vachngan,
                                                   b.sl_inner,
                                                   b.l1,
                                                   b.w1,
                                                   b.h1,
                                                   b.sl_outer,
                                                   b.l2_mix,
                                                   b.w2_mix,
                                                   b.h2_mix,
                                                   b.v2,
                                                   b.vd,
                                                   b.vn,
                                                   b.vl,
                                                   b.soluonggoi_ctn,
                                                   b.mota
                                               }).FirstOrDefault();

                                if (donggoi == null)
                                {
                                    choPhepImport = false;
                                    msg += string.Format(@"<div style='color:red'>Sản phẩm ""{0}"" không có đóng gói mặc định.</div>", ma_sanpham);
                                }
                                else
                                {
                                    //kiem tra hang hoa doc quyen
                                    int count_dq = (from hdq in db.md_hanghoadocquyens
                                                    where !hdq.md_doitackinhdoanh_id.Equals(khachhang_id) && hdq.md_sanpham_id.Equals(sp.md_sanpham_id)
                                                    select new { hdq.md_hanghoadocquyen_id }).Count();
                                    int count_dqdt = (from hdq in db.md_hanghoadocquyens
                                                      where hdq.md_doitackinhdoanh_id.Equals(khachhang_id) && hdq.md_sanpham_id.Equals(sp.md_sanpham_id)
                                                      select new { hdq.md_hanghoadocquyen_id }).Count();

                                    string trangthai = "BT", mota = "";
                                    if (count_dq > 0 && count_dqdt == 0 && donggoi.doigia_donggoi == true)
                                    {
                                        trangthai = "DGDGDQ";
                                        mota += "DGDG & DQ";
                                    }
                                    else if (count_dq > 0 && count_dqdt == 0)
                                    {
                                        trangthai = "DQ";
                                        mota += "DGDG & DQ";
                                    }
                                    else if (donggoi.doigia_donggoi == true)
                                    {
                                        trangthai = "DGDG";
                                    }
                                    else
                                    {
                                        trangthai = "BT";
                                    }

                                    c_chitietbaogia mnu = new c_chitietbaogia
                                    {
                                        c_chitietbaogia_id = NewId(),
                                        c_baogia_id = id,
                                        md_sanpham_id = sp.md_sanpham_id,
                                        ma_sanpham_khach = ma_khach,
                                        md_cangbien_id = sp.md_cangbien_id,
                                        mota_tienganh = sp.mota_tienganh,
                                        mota_tiengviet = sp.mota_tiengviet,
                                        ghichu_vachngan = donggoi.ghichu_vachngan,
                                        sothutu = dem * 10,
                                        giafob = giafob,
                                        soluong = soluong,
                                        md_donggoi_id = donggoi.md_donggoi_id,
                                        sl_inner = donggoi.sl_inner,
                                        l1 = donggoi.l1,
                                        w1 = donggoi.w1,
                                        h1 = donggoi.h1,
                                        sl_outer = donggoi.sl_outer,
                                        l2 = donggoi.l2_mix,
                                        w2 = donggoi.w2_mix,
                                        h2 = donggoi.h2_mix,
                                        v2 = donggoi.v2,
                                        vd = donggoi.vd,
                                        vn = donggoi.vn,
                                        vl = donggoi.vl,
                                        sl_cont = donggoi.soluonggoi_ctn,
                                        ghichu = "",
                                        docquyen = docquyen,
                                        mota = "",
                                        hoatdong = true,
                                        ngaytao = DateTime.Now,
                                        ngaycapnhat = DateTime.Now,
                                        nguoitao = UserUtils.getUser(context),
                                        nguoicapnhat = UserUtils.getUser(context),
                                        trangthai = trangthai,
                                        ghichudonggoingoai = donggoi.mota
                                    };

                                    db.c_chitietbaogias.InsertOnSubmit(mnu);
                                    rowAdd++;
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            choPhepImport = false;
                            msg += string.Format(@"<div style='color:red'>Sản phẩm ""{0}"" chưa có giá FOB</div>", ma_sanpham);
                        }
                    }
                    else
                    {
                        choPhepImport = false;
                        var textTT = sp.trangthai == "SOANTHAO" ? "Soạn Thảo" : (sp.trangthai == "NHD" ? "Ngưng hoạt động" : "Hàng Hóa Tạm");
                        msg += string.Format(@"<div style='color:red'>Sản phẩm ""{0}"" có trạng thái ""{1}"" không cho phép import</div>", ma_sanpham, textTT);
                    }
                }
                else
                {
                    choPhepImport = false;
                    msg += string.Format(@"<div style='color:red'>Không tìm thấy sản phẩm có mã ""{0}""</div>", ma_sanpham);
                }
            }
            catch (Exception exx)
            {
                choPhepImport = false;
                msg += string.Format(@"<div style='color:red'>{0}</div>", exx + "");
            }
        }

        if (rowAdd == 0)
            msg = "false#" + msg;
        else if (msg.Length > 0 & !choPhepImport)
            msg = "false#" + msg;

        return msg;
    }

    public string check_hanghoadocquyen(string md_sanpham_id, string md_doitackinhdoanh_id)
    {
        string msg = "";
        md_hanghoadocquyen hhdq = db.md_hanghoadocquyens.FirstOrDefault(s => s.md_sanpham_id == md_sanpham_id);
        if (hhdq != null)
        {
            md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(s => s.md_doitackinhdoanh_id == hhdq.md_doitackinhdoanh_id);
            if (dtkd != null)
            {
                if (dtkd.md_doitackinhdoanh_id != md_doitackinhdoanh_id)
                {
                    md_quocgia quocgia = db.md_quocgias.FirstOrDefault(s => s.md_quocgia_id == dtkd.md_quocgia_id);
                    string ten_qg = "";
                    if (quocgia != null)
                    {
                        ten_qg = quocgia.ten_quocgia;
                    }
                    //msg = "Mã hàng độc quyền cho khách "+ dtkd.ma_dtkd +" ở thị trường "+ ten_qg +". Bạn có muốn mở bán cho khách hàng của bạn không ? YES/NO";
                    msg = "Mã hàng độc quyền cho khách " + dtkd.ma_dtkd + " ở thị trường " + hhdq.mota + ".\n Bạn có muốn mở bán cho khách hàng của bạn không ? YES/NO";
                }
            }
        }
        return msg;
    }

    public bool IsReusable
    {
        get { return false; }
    }
}