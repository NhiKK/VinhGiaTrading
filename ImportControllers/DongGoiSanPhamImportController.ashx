<%@ WebHandler Language="C#" Class="DongGoiSanPhamImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class DongGoiSanPhamImportController : IHttpHandler
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
                String cellMaDG = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellMaHH = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String cellMacDinh = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                bool macdinh = cellMacDinh == "1" ? true : false;
                
                md_sanpham sp = ImportUtils.getSanPham(cellMaHH);
                md_donggoi dg = ImportUtils.getDongGoi(cellMaDG);
                
                
                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                bool next = true;

                if (sp == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy mã sản phẩm {0}!</div>", cellMaHH);
                }

                if (dg == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy mã đóng gói {0}!</div>", cellMaDG);
                }
                
                if (next)
                {
                    md_donggoisanpham dgsp = ImportUtils.getDongGoiSanPham(cellMaDG, cellMaHH);
                    DonGoiSanPhamImport import = new DonGoiSanPhamImport(ImportUtils.getDongGoi(cellMaDG).md_donggoi_id, ImportUtils.getSanPham(cellMaHH).md_sanpham_id, macdinh);
                    if (dgsp == null)
                    {
                        import.Import();
                        msg += String.Format("<div style=\"color:green\">Đã tạo một đóng gói cho sản phẩm!</div>");
                    }
                    else {
                        import.Update();
                        msg += String.Format("<div style=\"color:green\">Cập nhật đóng gói sản phẩm thành công!</div>");
                    }

                    if (macdinh)
                    {
                        String update = "update md_donggoisanpham set macdinh = 0 where md_sanpham_id = @md_sanpham_id and md_donggoi_id != @md_donggoi_id";
                        mdbc.ExcuteNonQuery(update, "@md_sanpham_id", sp.md_sanpham_id, "@md_donggoi_id", dg.md_donggoi_id);
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