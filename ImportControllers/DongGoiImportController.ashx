<%@ WebHandler Language="C#" Class="DongGoiImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class DongGoiImportController : IHttpHandler
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

    /*public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        //for (int i = 1; i < 5; i++)
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String cellTenDG = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                decimal cellSL_Inner = row.GetCell(1).Value == null ? 0 : decimal.Parse(row.GetCell(1).Value.ToString());
                String cellDvt_Inner = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                decimal cellL1 = row.GetCell(3).Value == null ? 0 : decimal.Parse(row.GetCell(3).Value.ToString());
                decimal cellW1 = row.GetCell(4).Value == null ? 0 : decimal.Parse(row.GetCell(4).Value.ToString());
                decimal cellH1 = row.GetCell(5).Value == null ? 0 : decimal.Parse(row.GetCell(5).Value.ToString());
                decimal cellSL_Outer = row.GetCell(6).Value == null ? 0 : decimal.Parse(row.GetCell(6).Value.ToString());
                String cellDvt_Outer = row.GetCell(7).Value == null ? String.Empty : row.GetCell(7).Value.ToString();
                decimal cellL2 = row.GetCell(8).Value == null ? 0 : decimal.Parse(row.GetCell(8).Value.ToString());
                decimal cellW2 = row.GetCell(9).Value == null ? 0 : decimal.Parse(row.GetCell(9).Value.ToString());
                decimal cellH2 = row.GetCell(10).Value == null ? 0 : decimal.Parse(row.GetCell(10).Value.ToString());
                decimal cellSL_Cont = row.GetCell(11).Value == null ? 0 : decimal.Parse(row.GetCell(11).Value.ToString());
                decimal cellVD = row.GetCell(13).Value == null ? 0 : decimal.Parse(row.GetCell(13).Value.ToString());
                decimal cellVN = row.GetCell(14).Value == null ? 0 : decimal.Parse(row.GetCell(14).Value.ToString());
                decimal cellVL = row.GetCell(15).Value == null ? 0 : decimal.Parse(row.GetCell(15).Value.ToString());
                String cellGhiChu_VachNgan = row.GetCell(16).Value == null ? String.Empty : row.GetCell(16).Value.ToString();
                Nullable<DateTime> ngayXacNhan = null;

                if (row.GetCell(17).Value != null)
                {
                    if (row.GetCell(17).Value.ToString() != "")
                    {
                        ngayXacNhan = row.GetCell(17).DateTimeValue;
                    }
                }
                
                
                md_donvitinh dvt_inner = ImportUtils.getDonViTinh(cellDvt_Inner);
                md_donvitinh dvt_outer = ImportUtils.getDonViTinh(cellDvt_Outer);
                
                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                bool next = true;

                if (dvt_inner == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy đơn vị tính inner {0} </div>", cellDvt_Inner);
                }

                if (dvt_outer == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy đơn vị tính outer {0} </div>", cellDvt_Outer);
                }

                if (next)
                {
                    md_donggoi dg = ImportUtils.getDongGoi(cellTenDG);
                    if (dg == null)
                    {
                        DongGoiImport import = new DongGoiImport(cellTenDG, cellSL_Outer.ToString() + " " + cellDvt_Outer, cellSL_Inner, dvt_inner.md_donvitinh_id, cellL1, cellW1, cellH1
                        , cellSL_Outer, dvt_outer.md_donvitinh_id, cellL2, cellW2, cellH2, cellSL_Cont, cellVD, cellVN, cellVL, cellGhiChu_VachNgan, ngayXacNhan);
                        import.Import();

                        msg += String.Format("<div style=\"color:green\">Đã tạo mới một đóng gói!</div>");
                    }
                    else
                    {
                        DongGoiImport import = new DongGoiImport(cellTenDG, cellSL_Outer.ToString() + " " + cellDvt_Outer, cellSL_Inner, dvt_inner.md_donvitinh_id, cellL1, cellW1, cellH1
                        , cellSL_Outer, dvt_outer.md_donvitinh_id, cellL2, cellW2, cellH2, cellSL_Cont, cellVD, cellVN, cellVL, cellGhiChu_VachNgan, ngayXacNhan);
                        import.Update();

                        msg += String.Format("<div style=\"color:green\">Cập nhật đóng gói thành công!</div>");
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
	*/

    public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        //for (int i = 1; i < 5; i++)
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                int cell = 0;
                String cellTenDG = row.GetCell(cell).Value == null ? String.Empty : row.GetCell(cell).Value.ToString();
                cell++;
                decimal cellSL_Inner = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                String cellDvt_Inner = row.GetCell(cell).Value == null ? String.Empty : row.GetCell(cell).Value.ToString();
                cell++;
                decimal cellL1 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellW1 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellH1 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellNW1 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellGW1 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellSL_Outer = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                String cellDvt_Outer = row.GetCell(cell).Value == null ? String.Empty : row.GetCell(cell).Value.ToString();
                cell++;
                decimal cellL2 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellW2 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellH2 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellNW2 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellVTDG2 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellGW2 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellCPDGVuotChuan = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellSL_Cont20 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellSL_Cont40 = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                cell++;
                decimal cellSL_Cont40HC = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                // decimal cellVD = row.GetCell(13).Value == null ? 0 : decimal.Parse(row.GetCell(13).Value.ToString());
                // decimal cellVN = row.GetCell(14).Value == null ? 0 : decimal.Parse(row.GetCell(14).Value.ToString());
                // decimal cellVL = row.GetCell(15).Value == null ? 0 : decimal.Parse(row.GetCell(15).Value.ToString());
                cell++;
                String ngayXacNhan = row.GetCell(cell).Value == null ? String.Empty : row.GetCell(cell).Value.ToString();
                cell++;
                String cellGhiChu = row.GetCell(cell).Value == null ? String.Empty : row.GetCell(cell).Value.ToString();
                //cell++;
                //String cellDGDG = row.GetCell(cell).Value == null ? String.Empty : row.GetCell(cell).Value.ToString();
                //cell++;
                //decimal cellTrongLuongDongGoi = row.GetCell(cell).Value == null ? 0 : decimal.Parse(row.GetCell(cell).Value.ToString());
                
                // if (row.GetCell(14).Value != null)
                // {
                // if (row.GetCell(14).Value.ToString() != "")
                // {
                // ngayXacNhan = row.GetCell(14).DateTimeValue;
                // }
                // }

                md_donvitinh dvt_inner = ImportUtils.getDonViTinh(cellDvt_Inner);
                md_donvitinh dvt_outer = ImportUtils.getDonViTinh(cellDvt_Outer);

                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);

                bool next = true, dgdg = false;

                if (dvt_inner == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy đơn vị tính inner {0} </div>", cellDvt_Inner);
                }

                if (dvt_outer == null)
                {
                    next = false;
                    msg += String.Format("<div style=\"color:red\">Không tìm thấy đơn vị tính outer {0} </div>", cellDvt_Outer);
                }

                if (next)
                {
                    md_donggoi dg = ImportUtils.getDongGoi(cellTenDG);
                    if (dg == null)
                    {
                        DongGoiImport import = new DongGoiImport();
                        import.Import(cellTenDG, cellSL_Outer.ToString() + " " + cellDvt_Outer, cellSL_Inner, dvt_inner.md_donvitinh_id, cellL1, cellW1, cellH1
                        , cellSL_Outer, dvt_outer.md_donvitinh_id, cellL2, cellW2, cellH2, cellSL_Cont40, cellSL_Cont20, cellSL_Cont40HC, ngayXacNhan, cellGhiChu, dgdg, 0, cellCPDGVuotChuan
                        , cellNW1, cellGW1, cellNW2, cellVTDG2, cellGW2);

                        msg += String.Format("<div style=\"color:green\">Đã tạo mới một đóng gói!</div>");
                    }
                    else
                    {
                        DongGoiImport import = new DongGoiImport();
                        import.Update(cellTenDG, cellSL_Outer.ToString() + " " + cellDvt_Outer, cellSL_Inner, dvt_inner.md_donvitinh_id, cellL1, cellW1, cellH1
                        , cellSL_Outer, dvt_outer.md_donvitinh_id, cellL2, cellW2, cellH2, cellSL_Cont40, cellSL_Cont20, cellSL_Cont40HC, ngayXacNhan, cellGhiChu, dgdg, 0, cellCPDGVuotChuan
                        , cellNW1, cellGW1, cellNW2, cellVTDG2, cellGW2);

                        msg += String.Format("<div style=\"color:green\">Cập nhật đóng gói thành công!</div>");
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