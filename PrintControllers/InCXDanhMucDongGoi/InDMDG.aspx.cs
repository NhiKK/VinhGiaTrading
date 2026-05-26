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

public partial class PrintControllers_InDMDG_Default : System.Web.UI.Page
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
			dg.ma_donggoi as [Mã đóng gói], 
            dg.sl_inner as [SLInner], 
			(select dvt.ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner) as [DVTInner], 
			dg.l1 as [L1],
            dg.w1 as [W1],
            dg.h1 as [H1],
            dg.sl_outer as [SLOuter], 
			(select dvt.ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer) as [DVTOuter], 
			dg.l2_mix as [L2], 
            dg.w2_mix as [W2], 
            dg.h2_mix as [H2],
            dg.v2 as [V2],
			dg.soluonggoi_ctn_20 as [SL Gói/cnt20], 
            dg.soluonggoi_ctn as [SL Gói/cnt40], 
            dg.soluonggoi_ctn_40hc as [SL Gói/cnt40hc],
            dg.nw1 as [NW1],
            dg.gw1 as [GW1],
            dg.nw2 as [NW2],
            dg.gw2 as [GW2],
            dg.vtdg2 as [VTDG2],
            isnull(dg.cpdg_vuotchuan, 0) as [CPDG vượt chuẩn],
			CONVERT(char(10), dg.ngayxacnhan,103) as [Ngày xác nhận],
            dg.mota as [Ghi chú]
		from 
            md_donggoi dg 
        where 
            1=1 {0}", 
        filter);

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
            s1.CreateRow(0).CreateCell(0).SetCellValue("DANH MỤC ĐÓNG GÓI");
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
				s1.SetColumnWidth(i, 2000);
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
						"SLInner","SLOuter","L1","W1","H1","L2","W2","H2","V2","SL Gói/cnt20","SL Gói/cnt40","SL Gói/cnt40hc",
                        "NW1", "GW1", "Trọng lượng ĐG", "NW2", "GW2", "VTDG2", "CPDG vượt chuẩn"
                    };

					if(arr_number.Contains(col.ColumnName)) {
						double number = 0;
						try { number = double.Parse(row[col.ColumnName].ToString()); } catch { }
						rowDetails.CreateCell(iCol).SetCellValue(number);
						rowDetails.GetCell(iCol).SetCellType(CellType.Numeric);
					}
					else {
						rowDetails.CreateCell(iCol).SetCellValue(row[col.ColumnName].ToString());
						rowDetails.GetCell(iCol).CellStyle = cellTop;
					}
					
					iCol++;
				}
				iRow++;
            }

            for (var ii = 0; ii < dt.Columns.Count; ii++)
            {
                s1.AutoSizeColumn(ii);
            }

            String saveAsFileName = "DanhMucDongGoi.xls";
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