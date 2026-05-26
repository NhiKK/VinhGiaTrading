<%@ WebHandler Language="C#" Class="PhienBanGiaImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class PhienBanGiaImportController : IHttpHandler
{

    public void ProcessRequest (HttpContext context) {
        String filename = context.Request.QueryString["filename"];
        String file = context.Server.MapPath("../FileUpload/" + filename);
        int j = file.LastIndexOf(".");
        if(file.Substring(j) != ".xls") {
            file = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(file);
        }
        Workbook wb = Workbook.Load(file);
        Worksheet ws = wb.Worksheets[0];
        this.NewFromCellCollection(context, ws.Cells);
    }

    public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String cellTenBG = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellTenPBG = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                DateTime cellNgayHieuLuc = row.GetCell(2).DateTimeValue;

                DateTime? cellNgayHetHan = null;
                try
                {
                    cellNgayHetHan = row.GetCell(3).DateTimeValue;
                }
                catch { }

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                md_banggia bg = ImportUtils.getBangGia(cellTenBG);

                if (bg == null)
                {
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy bảng giá {0}!</div>", cellTenBG);
                }
                else
                {
                    md_phienbangia pbg = ImportUtils.getPhienBanGia(cellTenPBG);
                    PhienBanGiaImport import = new PhienBanGiaImport(bg.md_banggia_id, cellTenPBG, cellNgayHieuLuc, cellNgayHetHan);
                    if (pbg == null)
                    {
                        import.Import();
                        msg += String.Format("<div style=\"color:green\">Đã tạo một phiên bản giá mới!</div>");
                    }
                    else
                    {
                        import.Update();
                        msg += String.Format("<div style=\"color:green\">Cập nhật phiên bản giá thành công!</div>");
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

    public bool IsReusable {
        get {
            return false;
        }
    }

}