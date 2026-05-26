using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using NPOI.SS.Util;
using NPOI.HSSF.Util;

public partial class PrintControllers_InDeTai_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String filter = Request.QueryString["filters"];
        if (filter != null & filter != "undefined")
        {
            String _filters = Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
		else {
			filter = "";
		}
		
		String sql = String.Format(@"
		select
			cl.code_cl as [Chủng loại], 
			dt.code_dt as [Đề tài], 
			dt.tv_ngan as [Tên Tiếng Việt], 
			dt.ta_ngan as [Tên Tiếng Anh],
			cl.code_cl + '-' + dt.code_dt + '.jpg' as [Hình ảnh],
			dt.hinhthucban as [Hình thức bán],
			dt.mota as [Ghi chú]
		from md_detai dt, md_chungloai cl
		where dt.md_chungloai_id = cl.md_chungloai_id {0}
		order by cl.code_cl, dt.code_dt
		", filter);

        DataTable dt = mdbc.GetData(sql);

        if (dt.Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
			IFont fontBold = hssfworkbook.CreateFont();
			fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
			
			IFont fontBoldBlue = hssfworkbook.CreateFont();
			fontBoldBlue.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
			fontBoldBlue.Color = HSSFColor.Blue.Index;
		
			ICellStyle cellCenter = hssfworkbook.CreateCellStyle();
			cellCenter.Alignment = HorizontalAlignment.Center;
			cellCenter.VerticalAlignment = VerticalAlignment.Top;
			cellCenter.WrapText = true;
			
			ICellStyle cellBoldCenter = hssfworkbook.CreateCellStyle();
			cellBoldCenter.Alignment = HorizontalAlignment.Center;
			cellBoldCenter.VerticalAlignment = VerticalAlignment.Top;
			cellBoldCenter.WrapText = true;
			cellBoldCenter.SetFont(fontBold);
			
			ICellStyle cellBoldCenter16 = hssfworkbook.CreateCellStyle();
			cellBoldCenter16.Alignment = HorizontalAlignment.Center;
			cellBoldCenter16.VerticalAlignment = VerticalAlignment.Top;
			cellBoldCenter16.SetFont(fontBoldBlue);
			
			ICellStyle cellTop = hssfworkbook.CreateCellStyle();
			cellTop.VerticalAlignment = VerticalAlignment.Top;
			
			s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, (dt.Columns.Count - 1 + 3)));
            s1.CreateRow(0).CreateCell(0).SetCellValue("Đề tài");
			s1.GetRow(0).HeightInPoints = 26;
			//s1.GetRow(0).GetCell(0).VerticalAlignment = VerticalAlignment.Top;
			s1.GetRow(0).GetCell(0).CellStyle = cellBoldCenter16;
            IRow rowHeader = s1.CreateRow(1);
			rowHeader.HeightInPoints = 19;
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
				if(i != 5) {
					s1.AddMergedRegion(new CellRangeAddress(1, 2, i, i));
					rowHeader.CreateCell(i).SetCellValue(col.ColumnName);
					rowHeader.GetCell(i).CellStyle = cellBoldCenter;
					if("Tên Tiếng Anh".Contains(col.ColumnName)) {
						s1.SetColumnWidth(i, 6000);
					}
					else if("Tên Tiếng Việt,Ghi chú".Contains(col.ColumnName)) {
						s1.SetColumnWidth(i, 16000);
					}
					else {
						s1.SetColumnWidth(i, 4000);
					}
					i++;
				}
				else {
					s1.AddMergedRegion(new CellRangeAddress(1, 1, i, i + 3));
					rowHeader.CreateCell(i).SetCellValue(col.ColumnName);
					rowHeader.GetCell(i).CellStyle = cellBoldCenter;
					
					IRow rowHeader2 = s1.CreateRow(2);
					rowHeader2.CreateCell(i).SetCellValue("Bán đại trà");
					rowHeader2.CreateCell(i+1).SetCellValue("Độc quyền");
					rowHeader2.CreateCell(i+2).SetCellValue("Quản lý ngoài");
					rowHeader2.CreateCell(i+3).SetCellValue("Bán thử nghiệm");
					for(int k = i; k <= i + 3; k++) {
						s1.SetColumnWidth(k, 2500);
						rowHeader2.GetCell(k).CellStyle = cellBoldCenter;
					}
					i += 4;
				}
            }

            int iRow = 3;
            foreach (DataRow row in dt.Rows)
            {   
				IRow rowDetails = s1.CreateRow(iRow);
				rowDetails.HeightInPoints = 17;
				int iCol = 0;
				foreach (DataColumn col in dt.Columns)
				{
					try
					{
						String imgPath = Server.MapPath("~/images/products/detai/" + row[4].ToString());
						System.Drawing.Image image = System.Drawing.Image.FromFile(imgPath);
						MemoryStream ms = new MemoryStream();
						image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

						IDrawing patriarch = s1.CreateDrawingPatriarch();
						HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 4, iRow, 5, iRow + 1);
						anchor.AnchorType = AnchorType.MoveDontResize;

						int index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.JPEG);
						IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
					}
					catch
					{ 
						
					}
					
					string[] arr_number = {
						"(##)"
					};
					string[] arr_center = {
						"(##)"
					};
					if(arr_number.Contains(col.ColumnName)) {
						double number = 0;
						try { number = double.Parse(row[col.ColumnName].ToString()); } catch { }
						rowDetails.CreateCell(iCol).SetCellValue(number);
						rowDetails.GetCell(iCol).SetCellType(CellType.Numeric);
					}
					else if(arr_center.Contains(col.ColumnName)){
						rowDetails.CreateCell(iCol).SetCellValue(row[col.ColumnName].ToString());
						rowDetails.GetCell(iCol).CellStyle = cellCenter;
					}
					else {
						if(iCol != 4 & iCol != 5) {
							rowDetails.CreateCell(iCol).SetCellValue(row[col.ColumnName].ToString());
							if(iCol == 0 | iCol == 1) {
								rowDetails.GetCell(iCol).CellStyle = cellCenter;
							}
							else {
								rowDetails.GetCell(iCol).CellStyle = cellTop;
							}
						}
						else {
							if(row[col.ColumnName].ToString() == "Bán đại trà") {
								rowDetails.CreateCell(iCol).SetCellValue("X");
								rowDetails.GetCell(iCol).CellStyle = cellCenter;
							}
							else if(row[col.ColumnName].ToString() == "Độc quyền") {
								rowDetails.CreateCell(iCol + 1).SetCellValue("X");
								rowDetails.GetCell(iCol + 1).CellStyle = cellCenter;
							}
							else if(row[col.ColumnName].ToString() == "Quản lý ngoài") {
								rowDetails.CreateCell(iCol+2).SetCellValue("X");
								rowDetails.GetCell(iCol+2).CellStyle = cellCenter;
							}
							else if(row[col.ColumnName].ToString() == "Bán thử nghiệm") {
								rowDetails.CreateCell(iCol+3).SetCellValue("X");
								rowDetails.GetCell(iCol+3).CellStyle = cellCenter;
							}
						}
					}
					if(iCol != 5)
						iCol++;
					else
						iCol+=4;
				}
				iRow++;
            }
            String saveAsFileName = "DeTai.xls";
            this.SaveFile(hssfworkbook, saveAsFileName);
        }
        else
        {
            Response.Write("Không có dữ liệu.");
        }
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName)
    {
        MemoryStream exportData = new MemoryStream();
        hsswb.Write(exportData);

        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
        Response.Clear();
        Response.BinaryWrite(exportData.GetBuffer());
        Response.End();
    }
}