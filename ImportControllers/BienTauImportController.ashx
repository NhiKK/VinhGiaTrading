<%@ WebHandler Language="C#" Class="BienTauImportController" %>

using System;
using System.Web;
using System.Linq;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class BienTauImportController : IHttpHandler
{
    LinqDBDataContext db = new LinqDBDataContext();
    
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
        //for (int i = 1; i < 3; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                var lstBienTau = new System.Collections.Generic.List<string>();
                
                bool next = true;
                int j = 1;
                
                // lay danh sach cau bien tau
                while(next == true)
                {
                    object item = row.GetCell(j).Value;
                    if (string.IsNullOrWhiteSpace((string)item))
                    {
                        next = false; // khi gia tri cua cell bang null
                    }
                    else {
                        lstBienTau.Add((String)item);
                        j++;
                    }
                }
                
                // lay danh sach cac ma hang
                String cellMaHangTho = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String select = String.Format("select md_sanpham_id from md_sanpham where ma_sanpham like N'{0}%'", cellMaHangTho);

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
                
                System.Data.DataTable dt = mdbc.GetData(select);
                foreach (System.Data.DataRow item in dt.Rows)
                {
					String md_sanpham_id = item[0].ToString();
                    
					foreach (String bt in lstBienTau)
                    {
                        md_bientau bientau = ImportUtils.getBienTau(md_sanpham_id, bt);
                        
                        BienTauImport import = new BienTauImport(md_sanpham_id, bt);

                        if (bientau == null)
                        {
                            import.Import();
                            String update = "update md_sanpham set updated = (case when updated = 1 then 0 else 1 end) where md_sanpham_id = @md_sanpham_id";
                            mdbc.ExcuteNonQuery(update, "@md_sanpham_id", md_sanpham_id);
                            msg += String.Format("<div style=\"color:green\">Đã tạo một biến tấu mới!</div>");
                        }
                        else
                        {
                            import.Update();
                            String update = "update md_sanpham set updated = (case when updated = 1 then 0 else 1 end) where md_sanpham_id = @md_sanpham_id";
                            mdbc.ExcuteNonQuery(update, "@md_sanpham_id", md_sanpham_id);
                            msg += String.Format("<div style=\"color:green\">Cập nhật biến tấu thành công!</div>");
                        }
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