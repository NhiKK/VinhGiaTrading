<%@ WebHandler Language="C#" Class="DoiTacImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class DoiTacImportController : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
		try {
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
		catch (Exception ex)
		{
			context.Response.Write(String.Format("<div style=\"color:red\">Xảy ra lỗi {0}</div>", ex.Message));
		}
    }

    public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String cellMaDT = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellTenDT = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String cellDienThoai = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                String cellFax = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                String cellEmail = row.GetCell(4).Value == null ? String.Empty : row.GetCell(4).Value.ToString();
                String cellDiaChi = row.GetCell(5).Value == null ? String.Empty : row.GetCell(5).Value.ToString();
                String cellMaQuocGia = row.GetCell(6).Value == null ? String.Empty : row.GetCell(6).Value.ToString();
                String cellMaKhuVuc = row.GetCell(7).Value == null ? String.Empty : row.GetCell(7).Value.ToString();
				String soTK = row.GetCell(9).Value == null ? String.Empty : row.GetCell(9).Value.ToString();
				String tenNH = row.GetCell(10).Value == null ? String.Empty : row.GetCell(10).Value.ToString();
				String MaST = row.GetCell(11).Value == null ? String.Empty : row.GetCell(11).Value.ToString();
				String NguoiDD = row.GetCell(12).Value == null ? String.Empty : row.GetCell(12).Value.ToString();
				String Chucvu = row.GetCell(13).Value == null ? String.Empty : row.GetCell(13).Value.ToString();
                bool cellIsNCC = false;
				if(row.GetCell(8).Value == "1" | row.GetCell(8).Value == "2")
					cellIsNCC = true;
					
				string row8 = row.GetCell(8).Value.ToString();
				
                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                md_doitackinhdoanh dtkd = ImportUtils.getDoiTac(cellMaDT);
                //md_loaidtkd loai_dtkd = ImportUtils.getLoaiDoiTac(cellIsNCC);
				md_loaidtkd loai_dtkd = ImportUtils.getLoaiDoiTacVNN(row8);
                md_quocgia qg = ImportUtils.getQuocGia(cellMaQuocGia);
                md_khuvuc kv = ImportUtils.getKhuVuc(cellMaKhuVuc);

                bool next = true;

                if (loai_dtkd == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy chủng loại {0} </div>", cellIsNCC == true? "Nhà cung cấp" : "Khách hàng");
                }

                if (qg == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy quốc gia {0} </div>", cellMaQuocGia);
                }

                if (kv == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy khu vực {0} </div>", cellMaKhuVuc);
                }

                if (next)
                {
					DoiTacKinhDoanhImport import = new DoiTacKinhDoanhImport();
                    if (dtkd == null)
                    {
                        import.Import(loai_dtkd.md_loaidtkd_id, cellMaDT, cellTenDT, cellDienThoai, cellFax, cellEmail, cellDiaChi, cellIsNCC, qg.md_quocgia_id, kv.md_khuvuc_id, soTK, tenNH, MaST, NguoiDD, Chucvu);
                        msg += String.Format("<div style=\"color:green\">Đã tạo một đối tác kinh doanh mới!</div>");
                    }
                    else
                    {
                        import.Update(loai_dtkd.md_loaidtkd_id, cellMaDT, cellTenDT, cellDienThoai, cellFax, cellEmail, cellDiaChi, cellIsNCC, qg.md_quocgia_id, kv.md_khuvuc_id, soTK, tenNH, MaST, NguoiDD, Chucvu);
                        msg += String.Format("<div style=\"color:green\">Cập nhật đối tác thành công!</div>");
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