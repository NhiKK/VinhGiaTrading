<%@ WebHandler Language="C#" Class="MauSacImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class MauSacImportController : IHttpHandler
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
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String cellMaCL = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                String cellMaDT = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                String cellMaMau = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                String cellTVN = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                //String cellTVD = row.GetCell(4).Value == null ? String.Empty : row.GetCell(4).Value.ToString();
                String cellTAN = row.GetCell(4).Value == null ? String.Empty : row.GetCell(4).Value.ToString();
                //String cellTAD = row.GetCell(6).Value == null ? String.Empty : row.GetCell(6).Value.ToString();
                String cellHTB = row.GetCell(5).Value == null ? String.Empty : row.GetCell(5).Value.ToString();
                String cellNM = row.GetCell(6).Value == null ? String.Empty : row.GetCell(6).Value.ToString();
                String cellGC = row.GetCell(7).Value == null ? String.Empty : row.GetCell(7).Value.ToString();
                md_detai dt = ImportUtils.getDeTai(cellMaDT, cellMaCL);
                md_chungloai cl = ImportUtils.getChungLoai(cellMaCL);
                md_nhommau nm = ImportUtils.getNhomMau(cellNM);

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
                bool next = true;

                if (dt == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy mã đề tài {0} </div>", cellMaDT);
                }

                if (cl == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy mã chủng loại {0} </div>", cellMaCL);
                }

                if (next)
                {
                    md_mausac ms = ImportUtils.getMauSac(cellMaMau, cellMaCL, cellMaDT);
                    if (ms == null)
                    {
                        MauSacImport import = new MauSacImport(cl.md_chungloai_id, dt.md_detai_id, cellMaMau, cellMaDT, cellMaCL, cellTVN, cellTAN, cellHTB, "", nm.md_nhommau_id, cellGC);
                        import.Import();

                        msg += String.Format(@"<div style=""color:green"">Đã tạo màu sắc sản phẩm mới!</div>");
                    }
                    //else if (ms.trangthai != "SOANTHAO")
                    //{
                    //    msg += String.Format(@"<div style=""color:red"">Dòng {0}-{1}-{2} có trạng thái là ""{3}""</div>", ms.code_cl, ms.code_dt, ms.code_mau, ms.trangthai);
                    //}
                    else
                    {
                        MauSacImport import = new MauSacImport(cl.md_chungloai_id, dt.md_detai_id, cellMaMau, cellMaDT, cellMaCL, cellTVN, cellTAN, cellHTB, "", nm.md_nhommau_id, cellGC);
                        import.Update();

                        msg += String.Format("<div style=\"color:green\">Cập nhật màu sắc sản phẩm thành công!</div>");
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}