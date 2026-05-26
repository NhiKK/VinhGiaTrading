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
                String cellMaHSCode = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString().Trim();
                String cellHsCode = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String cellHeHang = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString().Trim();
                String cellDienGiai = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                String cellDienGiaiTA = row.GetCell(4).Value == null ? String.Empty : row.GetCell(4).Value.ToString();
                String cellThanhPhan = row.GetCell(5).Value == null ? String.Empty : row.GetCell(5).Value.ToString();
                String cellNCU = row.GetCell(6).Value == null ? String.Empty : row.GetCell(6).Value.ToString();
                String cellGhiChu = row.GetCell(7).Value == null ? String.Empty : row.GetCell(7).Value.ToString();

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                //md_hscode hscode = ImportUtils.getHSCode(cellMaHSCode, cellHeHang);
                string select = string.Format(@"select * from md_hscode where ma_hscode=N'{0}' and hehang=N'{1}'", cellMaHSCode, cellHeHang);
                var a = mdbc.GetData(select);
                if (a.Rows.Count <= 0)
                {
                    HsCodeImport import = new HsCodeImport(cellMaHSCode, cellHsCode, cellHeHang, cellDienGiai, cellDienGiaiTA, cellThanhPhan, cellNCU, cellGhiChu);
                    import.Import();

                    msg += String.Format("<div style=\"color:green\">Đã tạo một HSCode mới!</div>");
                }
                else {
                    HsCodeImport import = new HsCodeImport(cellMaHSCode, cellHsCode, cellHeHang, cellDienGiai, cellDienGiaiTA, cellThanhPhan, cellNCU, cellGhiChu);
                    import.Update();

                    msg += String.Format("<div style=\"color:green\">Cập nhật HSCode thành công!</div>");
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