<%@ WebHandler Language="C#" Class="CangXuatHangImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class CangXuatHangImportController : IHttpHandler
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
        //for (int i = 1; i < 10; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String cellMaHH = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellMaCB = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
                bool next = true;
                
                md_sanpham sp = ImportUtils.getSanPham(cellMaHH);
                md_cangbien cb = ImportUtils.getCangBien(cellMaCB); 
                
                if (sp == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy mã hàng hóa {0} </div>", cellMaHH);
                }

                if (cb == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy mã cảng biển {0} </div>", cellMaCB);
                }
                

                if (next)
                {
                    // Import Cang xuat hang
                    md_cangxuathang cxh = ImportUtils.getCangXuatHang(cellMaHH, cellMaCB);
                    CangXuatHangImport import = new CangXuatHangImport(ImportUtils.getSanPham(cellMaHH).md_sanpham_id, ImportUtils.getCangBien(cellMaCB).md_cangbien_id);
                    if (cxh == null)
                    {
                        import.Import();
                        msg += String.Format("<div style=\"color:green\">Đã tạo một cảng xuất hàng mới!</div>");
                    }
                    else {
                        import.Update();
                        msg += String.Format("<div style=\"color:green\">Cập nhật cảng xuất hàng thành công!</div>");
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