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

public partial class PrintControllers_InNguonDoiTac_Default : System.Web.UI.Page
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
			ndt.ma_nguondtkd as [Mã Nguồn], ndt.ten_nguondtkd as [Tên Nguồn], ndt.mota as [Ghi chú]
		from md_nguondtkd ndt
		WHERE 1=1 {0}", filter);

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
			
			ICellStyle cellBoldCenter = hssfworkbook.CreateCellStyle();
			cellBoldCenter.Alignment = HorizontalAlignment.Center;
			cellBoldCenter.VerticalAlignment = VerticalAlignment.Top;
			cellBoldCenter.SetFont(fontBold);
			
			ICellStyle cellBoldCenter16 = hssfworkbook.CreateCellStyle();
			cellBoldCenter16.Alignment = HorizontalAlignment.Center;
			cellBoldCenter16.VerticalAlignment = VerticalAlignment.Top;
			cellBoldCenter16.SetFont(fontBoldBlue);
			
			ICellStyle cellTop = hssfworkbook.CreateCellStyle();
			cellTop.VerticalAlignment = VerticalAlignment.Top;
			
			s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, (dt.Columns.Count - 1)));
            s1.CreateRow(0).CreateCell(0).SetCellValue("Nguồn đối tác");
			s1.GetRow(0).HeightInPoints = 26;
			//s1.GetRow(0).GetCell(0).VerticalAlignment = VerticalAlignment.Top;
			s1.GetRow(0).GetCell(0).CellStyle = cellBoldCenter16;
            IRow rowHeader = s1.CreateRow(1);
			rowHeader.HeightInPoints = 19;
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
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

            int iRow = 2;
            foreach (DataRow row in dt.Rows)
            {   
				IRow rowDetails = s1.CreateRow(iRow);
				rowDetails.HeightInPoints = 17;
				int iCol = 0;
				foreach (DataColumn col in dt.Columns)
				{
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
						rowDetails.CreateCell(iCol).SetCellValue(row[col.ColumnName].ToString());
						rowDetails.GetCell(iCol).CellStyle = cellTop;
					}
					iCol++;
				}
				iRow++;
            }
            String saveAsFileName = "NguonDoiTac.xls";
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