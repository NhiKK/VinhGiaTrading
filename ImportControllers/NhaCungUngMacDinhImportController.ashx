<%@ WebHandler Language="C#" Class="NhaCungUngMacDinhImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class NhaCungUngMacDinhImportController : IHttpHandler
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
            String msg = "";
            msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
            try
            {
                Row row = cellCollection.Rows[i];
                // Biến kiểm tra giá trị cuối cùng của một dòng -- nếu bằng false sẽ chạy dòng tiếp theo
                bool next = true;
                
                // Danh sách tạm chứ thông tin của một dòng nhà cung ứng
                // bao gồm luôn cả mã sản phẩm, mã đối tác và mặc định
                var lstNhaCungUng = new System.Collections.Generic.List<NhaCungUngMacDinh>();
                int j = 1;
                
                // Vòng lặp đến khi giá trị cuối cùng bằng null
                while (next)
                {
                    object item = row.GetCell(j).Value;
                    if (item == null)
                    {
                        next = false; // khi gia tri cua cell bang null
                    }
                    else
                    {
                        NhaCungUngMacDinh ncu = new NhaCungUngMacDinh();
                        ncu.Masanpham = row.GetCell(0).Value.ToString();
                        ncu.Madoitac = (String)item;
                        lstNhaCungUng.Add(ncu);
                        
                        // nếu là nhà cung ứng đầu tiên thì lấy làm mặc định.
                        if (j == 1)
                        {
                            ncu.Macdinh = true;
                        }
                        else
                        {
                            ncu.Macdinh = false;
                        }
                        j++;
                    }
                }
                
                
                
                foreach (NhaCungUngMacDinh ncu in lstNhaCungUng)
                {
                    md_sanpham sp = ImportUtils.getSanPham(ncu.Masanpham);
                    md_doitackinhdoanh dtkd = ImportUtils.getDoiTac(ncu.Madoitac);
                    bool check = true;
    
                    // Kiểm tra mã hàng hóa
                    if (sp == null)
                    {
                        check = false;
                        msg += String.Format("<div>Không tìm thấy mã sản phẩm \"{0}\"</div>", ncu.Masanpham);
                    }

                    // Kiểm tra mã đối tác
                    if (dtkd == null)
                    {
                        check = false;
                        msg += String.Format("<div>Không tìm thấy mã đối tác \"{0}\"</div>", ncu.Madoitac);
                    }
                    
                    // Kiểm tra có mã hàng và mã đối tác đầy đủ mới tiếp tục thực hiện.
                    if (check)
                    {
                        md_nhacungungmacdinh n = ImportUtils.getNhaCungUngMacDinh(sp.md_sanpham_id, dtkd.md_doitackinhdoanh_id);
                        if (n == null)
                        {
                            NhaCungUngMacDinhImport import = new NhaCungUngMacDinhImport(sp.md_sanpham_id, dtkd.md_doitackinhdoanh_id, ncu.Macdinh);
                            import.Import();
                            msg += String.Format("<div style=\"color:green\">Đã thêm mã nhà cung ứng \"{0}\" cho sản phẩm \"{1}\"</div>", ncu.Madoitac, ncu.Masanpham);
                        }
                        else 
                        {
                            NhaCungUngMacDinhImport import = new NhaCungUngMacDinhImport(sp.md_sanpham_id, dtkd.md_doitackinhdoanh_id, ncu.Macdinh);
                            import.Update();
                            msg += String.Format("<div style=\"color:green\">Đã cập nhật mã nhà cung ứng \"{0}\" cho sản phẩm \"{1}\"</div>", ncu.Madoitac, ncu.Masanpham);
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