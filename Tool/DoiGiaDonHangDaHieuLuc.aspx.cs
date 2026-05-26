using System;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;
using System.Web;
using System.Linq;

public partial class Tool_DoiGiaDonHangDaHieuLuc : System.Web.UI.Page
{
    public bool send = false;
    public LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        string msg = "";
        send = Request.Form["send"] == "true";
        if (send)
        {
            try
            {
                String file = "";
                foreach (String item in Request.Files)
                {
                    var fileF = Request.Files[item] as HttpPostedFile;
                    var link = ExcuteSignalRStatic.mapPathSignalR("~/Logs/DoiGiaDonHangHL/" + fileF.FileName);
                    fileF.SaveAs(link);
                    int j = link.LastIndexOf(".");
                    if (link.Substring(j) != ".xls")
                    {
                        file = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(link);
                        System.IO.File.Delete(link);
                    }
                    else
                    {
                        file = link;
                    }
                }

                Workbook wb = Workbook.Load(file);
                Worksheet ws = wb.Worksheets[0];
                msg = this.NewFromCellCollection(ws.Cells);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        Response.Write(msg);
    }

    public string NewFromCellCollection(CellCollection cellCollection)
    {
        string msg = "";
        var isPO = Request.Form["type"] == "PO";
        var isDSDH = Request.Form["type"] == "DSDH";

        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String cellDonHang = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString().Trim();
                String cellMaHang = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString().Trim();
                String cellGia = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString().Trim();

                if (isPO)
                {
                    var dh = db.c_donhangs.Where(s => s.sochungtu == cellDonHang).FirstOrDefault();
                    if (dh == null)
                        throw new Exception(string.Format(@"Không tìm thấy đơn hàng ""{0}""", cellDonHang));

                    var sp = db.md_sanphams.Where(s => s.ma_sanpham == cellMaHang).FirstOrDefault();
                    if (sp == null)
                        throw new Exception(string.Format(@"Không tìm thấy sản phẩm ""{0}""", cellMaHang));

                    if (string.IsNullOrWhiteSpace(cellGia))
                        throw new Exception(string.Format(@"Giá sản phẩm ""{0}"" không thể bỏ trống", cellMaHang));

                    var ddh = db.c_dongdonhangs.Where(s => s.c_donhang_id == dh.c_donhang_id & s.md_sanpham_id == sp.md_sanpham_id).FirstOrDefault();
                    if (ddh != null)
                    {
                        ddh.giafob = decimal.Parse(cellGia);
                        msg = String.Format("<div style=\"color:blue; padding: 3px;\">Dòng {0}: ĐH={1}, MSP={2}, GSP={3} cập nhật thành công</div>", i, dh.sochungtu, sp.ma_sanpham, ddh.giafob.GetValueOrDefault(0).DropTrailingZeros());
                    }
                }
                else if(isDSDH)
                {
                    var dsdh = db.c_danhsachdathangs.Where(s => s.sochungtu == cellDonHang).FirstOrDefault();
                    if (dsdh == null)
                        throw new Exception(string.Format(@"Không tìm thấy đơn đặt hàng ""{0}""", cellDonHang));

                    var sp = db.md_sanphams.Where(s => s.ma_sanpham == cellMaHang).FirstOrDefault();
                    if (sp == null)
                        throw new Exception(string.Format(@"Không tìm thấy sản phẩm ""{0}""", cellMaHang));

                    if (string.IsNullOrWhiteSpace(cellGia))
                        throw new Exception(string.Format(@"Giá sản phẩm ""{0}"" không thể bỏ trống", cellMaHang));

                    var ddsdh = db.c_dongdsdhs.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id & s.md_sanpham_id == sp.md_sanpham_id).FirstOrDefault();
                    if (ddsdh != null)
                    {
                        var phi = ddsdh.gianhap - ddsdh.giachuan;
                        ddsdh.giachuan = decimal.Parse(cellGia);
                        ddsdh.gianhap = ddsdh.giachuan + phi;
                        msg = String.Format("<div style=\"color:blue; padding: 3px;\">Dòng {0}: ĐĐH={1}, MSP={2}, GSP={3} cập nhật thành công</div>", i, dsdh.sochungtu, sp.ma_sanpham, ddsdh.giachuan.GetValueOrDefault(0).DropTrailingZeros());
                    }
                }
            }
            catch (Exception ex)
            {
                msg = String.Format("<div style=\"color:red; padding: 3px;\">Dòng {0} xảy ra lỗi {1}</div>", i + 1, ex.Message);
            }
        }

        if(msg.LastIndexOf("color:blue") > -1)
        {
            db.SubmitChanges();
        }

        return msg;
    }
}
