<%@ WebHandler Language="C#" Class="ChucNangImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class ChucNangImportController : IHttpHandler
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
                String cellMaCN = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellTVN = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String cellTAN = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                String cellGC = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                // String cellTVD = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                // String cellTAD = row.GetCell(4).Value == null ? String.Empty : row.GetCell(4).Value.ToString();
                
                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                md_chucnang chucnang = ImportUtils.getChucNang(cellMaCN);
                if (chucnang == null)
                {
                    ChucNangImport import = new ChucNangImport(cellMaCN, cellTVN, cellTAN, cellGC);
                    import.Import();
                    msg += String.Format("<div style=\"color:green\">Đã tạo một chức năng sản phẩm mới!</div>");
                }
				else if(chucnang != null)
				{
					msg += String.Format("<div style=\"color:red\">Dòng {0} đã tồn tại</div>", i + 1);
				}
                else 
                {
                    ChucNangImport import = new ChucNangImport(cellMaCN, cellTVN, cellTAN, cellGC);
                    import.Update();
                    msg += String.Format("<div style=\"color:green\">Cập nhật chức năng thành công!</div>");
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