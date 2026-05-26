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

public partial class PrintControllers_InHHDQ_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String filter = Request.QueryString["filters"], md_dtkd = "";
        if (filter != null & filter != "undefined")
        {
            String _filters = Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
		else {
			filter = "";
		}
		
		if (Request.QueryString["doitackinhdoanh_id"] != "NULL")
        {
            String _md_dtkd = Request.QueryString["doitackinhdoanh_id"];
            md_dtkd = " and dtkd.md_doitackinhdoanh_id = '"+ _md_dtkd +"'";
        }

		/*String sql = String.Format(@"
        select distinct sp.ma_sanpham as [Mã Hàng Hóa], 
        [dbo].[f_getdtkd_theo_hhdq](hhdq.md_sanpham_id) as [Mã Khách Hàng]
        from md_hanghoadocquyen hhdq 
        left join md_sanpham sp on hhdq.md_sanpham_id = sp.md_sanpham_id
        left join md_doitackinhdoanh dtkd on hhdq.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
        where 1=1 {0} order by sp.ma_sanpham", filter);*/
		
		String sql = String.Format(@"
        select sp.ma_sanpham as [Mã Hàng Hóa], 
        dtkd.ma_dtkd as [Mã Khách Hàng],
		hhdq.mota as [Thị Trường Độc Quyền]
        from md_hanghoadocquyen hhdq 
        left join md_sanpham sp on hhdq.md_sanpham_id = sp.md_sanpham_id
        left join md_doitackinhdoanh dtkd on hhdq.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
        where 1=1 " + md_dtkd + " " + filter + @"
		group by dtkd.ma_dtkd, sp.ma_sanpham, hhdq.mota
		order by [Mã Khách Hàng], [Mã Hàng Hóa]" );

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
			
			s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 1));
            s1.CreateRow(0).CreateCell(0).SetCellValue("HÀNG HÓA ĐỘC QUYỀN");
			s1.GetRow(0).HeightInPoints = 26;
			//s1.GetRow(0).GetCell(0).VerticalAlignment = VerticalAlignment.Top;
			s1.GetRow(0).GetCell(0).CellStyle = cellBoldCenter16;
			s1.SetColumnWidth(0, 1500);
            IRow rowHeader = s1.CreateRow(1);
			rowHeader.HeightInPoints = 19;
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
                rowHeader.CreateCell(i).SetCellValue(col.ColumnName);
                rowHeader.GetCell(i).CellStyle = cellBoldCenter;
				if(i < 1)
					s1.SetColumnWidth(i, 4000);
				else
					s1.SetColumnWidth(i, 20000);
				i++;
            }

            int iRow = 2;
            foreach (DataRow row in dt.Rows)
            {    
                IRow rowDetails = s1.CreateRow(iRow);
				rowDetails.HeightInPoints = 17;
				rowDetails.CreateCell(0).SetCellValue(row["Mã Hàng Hóa"].ToString());
				rowDetails.GetCell(0).CellStyle = cellTop;
				rowDetails.CreateCell(1).SetCellValue(row["Mã Khách Hàng"].ToString());
				rowDetails.GetCell(1).CellStyle = cellTop;
				rowDetails.CreateCell(2).SetCellValue(row["Thị Trường Độc Quyền"].ToString());
				rowDetails.GetCell(2).CellStyle = cellTop;
				iRow++;
            }
            String saveAsFileName = "HangHoaDocQuyen.xls";
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