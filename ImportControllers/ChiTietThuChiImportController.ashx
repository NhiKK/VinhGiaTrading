<%@ WebHandler Language="C#" Class="ChiTietThuChiImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Linq;

public class ChiTietThuChiImportController : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        String filename = context.Request.QueryString["filename"];
        String file = context.Server.MapPath("../FileUpload/" + filename);
        int j = file.LastIndexOf(".");
        if (file.Substring(j) != ".xls")
        {
            file = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(file);
        }
        Workbook wb = Workbook.Load(file);
        Worksheet ws = wb.Worksheets[0];
        this.NewFromCellCollection(context, ws.Cells);
        if (System.IO.File.Exists(file))
            System.IO.File.Delete(file);
    }

    public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        var db = new LinqDBDataContext();
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                string phieuThu = row.GetCell(0).Value + "";
                string tkNo = row.GetCell(1).Value + "";
                string tkCo = row.GetCell(2).Value + "";
                string soTien = row.GetCell(3).Value + "";
                string kieuHoSo = row.GetCell(4).Value + "";
                bool laTienCoc = (row.GetCell(5).Value + "").ToLower() == "1";
                string dienGiai = row.GetCell(6).Value + "";
                string packingINV = row.GetCell(7).Value + "";
                string PO = row.GetCell(8).Value + "";
                bool laPhi = (row.GetCell(9).Value + "").ToLower() == "1";

                var pT = db.c_thuchis.Where(s => s.sophieu == phieuThu).FirstOrDefault();

                string msg = "";
                msg += string.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                // Kiểm tra dữ liệu quan hệ
                bool next = true;

                if (pT == null)
                {
                    next = false;
                    msg += string.Format("<div style=\"color:red\">Không có số phiếu {0}</div>", phieuThu);
                }
                else
                {
                    var donhang = db.c_donhangs.Where(s =>
                            s.md_doitackinhdoanh_id == pT.md_doitackinhdoanh_id
                            & s.sochungtu == PO & s.md_trangthai_id == "HIEULUC"
                        ).FirstOrDefault();

                    var invoice = db.c_packinginvoices.Where(s =>
                            s.md_doitackinhdoanh_id == pT.md_doitackinhdoanh_id
                            & s.so_inv == packingINV
                            & s.md_trangthai_id == "HIEULUC"
                            & s.tienconlai > 0
                        ).FirstOrDefault();

                    if (!string.IsNullOrEmpty((PO)))
                    {
                        if (donhang == null)
                        {
                            next = false;
                            msg += string.Format(@"<div style=""color:red"">Không có số phiếu PO {0}</div>", PO);
                        }
                        else
                        {
                            if(kieuHoSo == "PO")
                                PO = donhang.c_donhang_id;
                            else
                                msg += string.Format(@"<div style=""color:red"">Kiểu hồ sơ phải là ""PO""</div>");
                        }
                    }

                    if (!string.IsNullOrEmpty((packingINV)))
                    {
                        if (invoice == null)
                        {
                            next = false;
                            msg += string.Format(@"<div style=""color:red"">Không có số phiếu INV {0}</div>", packingINV);
                        }
                        else
                        {
                            if(kieuHoSo == "IN")
                                packingINV = invoice.c_packinginvoice_id;
                            else
                                msg += string.Format(@"<div style=""color:red"">Kiểu hồ sơ phải là ""IN""</div>");
                        }
                    }
                }

                // Nếu tất cả đều họp lệ
                if (next)
                {
                    if (laPhi == true)
                    {
                        var ctlq = new c_chiphilienquan();
                        ctlq.c_chiphilienquan_id = Public.NewId();
                        ctlq.c_thuchi_id = pT.c_thuchi_id;
                        ctlq.tk_no = int.Parse(tkNo);
                        ctlq.tk_co = int.Parse(tkCo);
                        ctlq.sotien = decimal.Parse(soTien);
                        ctlq.quydoi = ctlq.sotien * pT.tygia.GetValueOrDefault(0);
                        ctlq.diengiai = dienGiai;
                        ctlq.c_donhang_id = PO;
                        ctlq.c_packinginvoice_id = packingINV;
                        ctlq.loaichiphi = kieuHoSo;
                        ctlq.hoatdong = true;
                        ctlq.ngaytao = DateTime.Now;
                        ctlq.ngaycapnhat = DateTime.Now;
                        ctlq.nguoicapnhat = UserUtils.getUser(context);
                        ctlq.nguoitao = UserUtils.getUser(context);
                        db.c_chiphilienquans.InsertOnSubmit(ctlq);
                        db.SubmitChanges();
                        msg += string.Format(@"<div style=""color:green"">Đã tạo chi tiết liên quan cho phiếu {0}!</div>", pT.sophieu);
                    }
                    else
                    {
                        var cttc = new c_chitietthuchi();
                        cttc.c_chitietthuchi_id = Public.NewId();
                        cttc.c_thuchi_id = pT.c_thuchi_id;
                        cttc.so_c = Public.getPhieuC(db);
                        cttc.tk_no = int.Parse(tkNo);
                        cttc.tk_co = int.Parse(tkCo);
                        cttc.sotien = decimal.Parse(soTien);
                        cttc.quydoi = cttc.sotien * pT.tygia.GetValueOrDefault(0);
                        cttc.diengiai = dienGiai;
                        cttc.c_donhang_id = PO;
                        cttc.c_packinginvoice_id = packingINV;
                        cttc.isdatcoc = laTienCoc;
                        cttc.loaiphieu = kieuHoSo;
                        cttc.hoatdong = true;
                        cttc.ngaytao = DateTime.Now;
                        cttc.ngaycapnhat = DateTime.Now;
                        cttc.nguoitao = UserUtils.getUser(context);
                        cttc.nguoicapnhat = UserUtils.getUser(context);
                        db.c_chitietthuchis.InsertOnSubmit(cttc);
                        db.SubmitChanges();
                        msg += string.Format(@"<div style=""color:green"">Đã tạo chi tiết thu chi cho phiếu {0}!</div>", pT.sophieu);
                    }

                }

                msg += String.Format("<div>--- Kết thúc dòng {0} ---</div>", i + 1);
                context.Response.Write(msg);
            }
            catch (Exception ex)
            {
                context.Response.Write(String.Format("<div style=\"color:red\">Dòng {0} xảy ra lỗi {1}</div>", i + 1, ex));
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}