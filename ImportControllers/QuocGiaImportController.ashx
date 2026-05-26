<%@ WebHandler Language="C#" Class="QuocGiaImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class QuocGiaImportController : IHttpHandler {
    
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
				String maKhuvuc = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                String tenQuocGia = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String maQuocGia = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                md_quocgia qg = ImportUtils.getQuocGia(maQuocGia);
                md_khuvuc kv = ImportUtils.getKhuVuc(maKhuvuc);
                if(kv == null) {
					 msg += String.Format("<div style=\"color:red\">Không tìm thấy khu vực dòng " + (i+1) + "</div>");
				}
				else {
					if (qg == null)
					{
						QuocGiaImport import = new QuocGiaImport();
						import.Import(maQuocGia, tenQuocGia, kv.md_khuvuc_id);
						msg += String.Format("<div style=\"color:green\">Đã tạo một quốc gia mới!</div>");
					}
					else
					{
						QuocGiaImport import = new QuocGiaImport();
						import.Update(maQuocGia, tenQuocGia, kv.md_khuvuc_id);
						msg += String.Format("<div style=\"color:green\">Cập nhật quốc gia thành công!</div>");
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
}