<%@ WebHandler Language="C#" Class="HsCodeImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class HsCodeImportController : IHttpHandler
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
                String cellMaSanPham = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellTiengViet = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String cellTiengAnh = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                md_sanpham sp = ImportUtils.getSanPham(cellMaSanPham);
                if (sp == null)
                {
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy mã sản phẩm \"{0}\"!</div>", cellMaSanPham);
                }
                else {
                    UpdateMoTaSanPhamImport import = new UpdateMoTaSanPhamImport(sp.md_sanpham_id, cellTiengViet, cellTiengAnh);
                    import.Update();

                    msg += String.Format("<div style=\"color:green\">Cập nhật mô tả sản phẩm thành công!</div>");
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