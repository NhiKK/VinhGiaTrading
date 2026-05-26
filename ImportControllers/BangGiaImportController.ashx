<%@ WebHandler Language="C#" Class="BangGiaImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class BangGiaImportController : IHttpHandler
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
                bool cellBGBan = row.GetCell(1).Value.Equals("Y") ? true : false;
                String cellDongTien = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                
                md_dongtien dongtien = ImportUtils.getDongTien(cellDongTien);
                md_banggia bg = ImportUtils.getBangGia(cellTenBG);

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
                
                // Kiểm tra dữ liệu quan hệ
                bool next = true;
                
                if (dongtien == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không có mã đồng tiền {0}</div>", cellDongTien);
                }
                
                // Nếu tất cả đều họp lệ
                if (next)
                {
                    if (bg == null)
                    {
                        BangGiaImport import = new BangGiaImport(cellTenBG, cellBGBan, dongtien.md_dongtien_id);
                        import.Import();
                        msg += String.Format("<div style=\"color:green\">Đã tạo một bảng giá mới!</div>");
                    }
                    else
                    {
                        BangGiaImport import = new BangGiaImport(cellTenBG, cellBGBan, dongtien.md_dongtien_id);
                        import.Update();
                        msg += String.Format("<div style=\"color:green\">Cập nhật bảng giá thành công!</div>");
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