<%@ WebHandler Language="C#" Class="DonViTinhDongGoiImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class DonViTinhDongGoiImportController : IHttpHandler
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
                String cellMa = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellTen = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                md_donvitinh dvt = ImportUtils.getDonViTinh(cellTen);
                if (dvt == null)
                {
                    DonViTinhDongGoiImport import = new DonViTinhDongGoiImport(cellMa, cellTen);
                    import.Import();
                    msg += String.Format("<div style=\"color:green\">Đã tạo một đơn vị tính đóng gói mới!</div>");
                }
                else
                {
                    DonViTinhDongGoiImport import = new DonViTinhDongGoiImport(cellMa, cellTen);
                    import.Update();
                    msg += String.Format("<div style=\"color:green\">Cập nhật đơn vị tính đóng gói thành công!</div>");
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