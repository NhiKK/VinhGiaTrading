<%@ WebHandler Language="C#" Class="PhanNhomMauImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class PhanNhomMauImportController : IHttpHandler
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
        //for (int i = 1; i < 5; i++)
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String cellMaNM = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellTenNM = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String cellMoTa = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
                bool next = true;

                if (cellMaNM == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Dòng {0} chưa điền mã</div>", i + 1);
                }
                else if (cellTenNM == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Dòng {0} chưa điền tên</div>", i + 1);
                }

                if (next)
                {
                    md_nhommau nm = ImportUtils.getNhomMau(cellMaNM);
                    if (nm == null)
                    {
                        PhanNhomMauImport import = new PhanNhomMauImport();
                        import.Import(cellMaNM, cellTenNM, cellMoTa);

                        msg += String.Format("<div style=\"color:green\">Đã tạo mới một nhóm màu!</div>");
                    }
                    else
                    {
                        PhanNhomMauImport import = new PhanNhomMauImport();
                        import.Update(cellMaNM, cellTenNM, cellMoTa);

                        msg += String.Format("<div style=\"color:green\">Cập nhật nhóm màu thành công!</div>");
                    }
                }
                else
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