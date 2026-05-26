<%@ WebHandler Language="C#" Class="GiaSanPhamImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class GiaSanPhamImportController : IHttpHandler
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
    }

    public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        //for (int i = 1; i < 100; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
                bool next = true;
                String cellMaHH = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                if (cellMaHH != null & cellMaHH != "")
                {
                    cellMaHH = cellMaHH.Replace(" ", "");
                    if (cellMaHH.Length != 14)
                    {
                        next = false;
                        msg += "<div style=\"color:red\">Mã hàng chỉ nên có đúng 14 ký tự.</div>";
                    }
                }
                else
                {
                    next = false;
                    msg += "<div style=\"color:red\">Mã hàng không thể rỗng.</div>";
                }

                if (next == true)
                {
                    String cellTenPB = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                    decimal cellGia = row.GetCell(2).Value == null ? 0 : decimal.Parse(row.GetCell(2).Value.ToString());

                    String cellMoTa = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                    String cellBarCode = row.GetCell(4).Value == null ? String.Empty : row.GetCell(4).Value.ToString();
                    decimal cellPhi = row.GetCell(5).Value == null ? 0 : decimal.Parse(row.GetCell(5).Value.ToString());
                    String cellDongGoi = row.GetCell(6).Value == null ? String.Empty : row.GetCell(6).Value.ToString();

                    md_phienbangia pbg = ImportUtils.getPhienBanGia(cellTenPB);
                    md_sanpham sp = ImportUtils.getSanPham(cellMaHH);
                    if(!string.IsNullOrWhiteSpace(cellDongGoi))
                    {
                        var dg = ImportUtils.getDongGoi(cellDongGoi);
                        if(dg == null)
                        {
                            next = false;
                            msg += String.Format("<div style=\"color:red\">Không tìm thấy đóng gói có mã {0} </div>", cellDongGoi);
                        }
                    }

                    if (pbg == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy phiên bản giá {0} </div>", cellTenPB);
                    }

                    if (sp == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy sản phẩm {0} </div>", cellMaHH);
                    }

                    if (next)
                    {
                        var gsp = ImportUtils.getGiaSanPham(cellTenPB, cellMaHH);
                        var import = new GiaSanPhamImport(sp.md_sanpham_id, pbg.md_phienbangia_id, cellGia, cellMoTa, cellBarCode, cellPhi, cellDongGoi);
                        if (gsp == null)
                        {
                            import.Import();
                            msg += String.Format("<div style=\"color:green\">Đã tạo một giá sản phẩm mới!</div>");
                        }
                        else
                        {
                            import.Update();
                            msg += String.Format("<div style=\"color:green\">Cập nhật giá sản phẩm thành công!</div>");
                        }
                    }
                }
                msg += String.Format("<div>--- Kết thúc dòng {0} ---</div>", i + 1);
                context.Response.Write(msg);
            }
            catch (Exception ex)
            {
                context.Response.Write(String.Format("<div style=\"color:red\">Dòng {0} xảy ra lỗi {1}</div>", i + 1, ex.Message));
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