<%@ WebHandler Language="C#" Class="DeTaiImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class DeTaiImportController : IHttpHandler
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
                String cellMaCL = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellMaDT = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String cellTVN = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                //String cellTVD = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                String cellTAN = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                //String cellTAD = row.GetCell(5).Value == null ? String.Empty : row.GetCell(5).Value.ToString();
				String cellHTB = row.GetCell(4).Value == null ? String.Empty : row.GetCell(4).Value.ToString();
				String cellGC = row.GetCell(5).Value == null ? String.Empty : row.GetCell(5).Value.ToString();
                md_chungloai chungloai = ImportUtils.getChungLoai(cellMaCL);

                bool next = true;
                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
                
                if (chungloai == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy chủng loại {0} </div>", cellMaCL);
                }


                if (next)
                {
                    md_detai detai = ImportUtils.getDeTai(cellMaDT, cellMaCL);
                    if (detai == null)
                    {
                        DeTaiImport import = new DeTaiImport(cellMaDT, chungloai.md_chungloai_id, cellMaCL, cellTVN, cellTAN, cellHTB, cellGC);
                        import.Import();
                        msg += String.Format("<div style=\"color:green\">Đã tạo mới một đề tài!</div>");
                    }
					else if(detai.trangthai != "SOANTHAO")
					{
						msg += String.Format("<div style=\"color:red\">Dòng {0} đã Hiệu Lực</div>", i + 1);
					}
                    else
                    {
                        DeTaiImport import = new DeTaiImport(cellMaDT, chungloai.md_chungloai_id, cellMaCL, cellTVN, cellTAN, cellHTB, cellGC);
                        import.Update();
                        msg += String.Format("<div style=\"color:green\">Cập nhật đề tài thành công!</div>");
                    }

                    msg += String.Format("<div>--- Kết thúc dòng {0} ---</div>", i + 1);
                }
                
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