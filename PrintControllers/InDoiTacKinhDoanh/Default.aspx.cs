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

public partial class PrintControllers_InDoiTacKinhDoanh_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String filter = Request.QueryString["filters"];
        String where_ex = Request.QueryString["where_ex"];
        if (filter != null & filter != "undefined")
        {
            String _filters = Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
		else {
			filter = "";
		}
		
		if (where_ex == null | where_ex == "undefined")
        {
			where_ex = "";
		}
		
		String sql = String.Format(@"
			SELECT * FROM( 
            SELECT 
			ldt.ten_loaidtkd as [Loại Đối Tác]
			,dtkd.ma_dtkd as [Mã Đối Tác]
			,dtkd.ten_dtkd as [Tên Đối Tác]
			,dtkd.daidien as [Đại Diện]
			,dtkd.chucvu as [Chức Vụ]
			,dtkd.tel as [Điện thoại]
			,dtkd.fax as [Fax]
			,dtkd.email as [Email]
			,dtkd.url as [Website]
			,dtkd.diachi as [Địa Chỉ]
			,qg.ten_quocgia as [Quốc Gia]
			,kv.ten_khuvuc as [Khu Vực]
			,dtkd.so_taikhoan as [Số Tài Khoản]
			,dtkd.nganhang as [Ngân Hàng]
			,dtkd.masothue as [Mã Số Thuế]
			,dtkd.tong_congno as [Tổng Công Nợ]
			,dtkd.isncc as [Là Nhà Cung Cấp]
            ,(case when md_cangbien_id is not null then (select ten_cangbien from md_cangbien where md_cangbien_id = dtkd.md_cangbien_id)
                    else null end) as [Thuộc Cảng Biển], 
			ndt.ten_nguondtkd as [Nguồn Đối Tác]
            FROM md_doitackinhdoanh dtkd 
			LEFT JOIN md_quocgia qg ON dtkd.md_quocgia_id = qg.md_quocgia_id 
            LEFT JOIN md_khuvuc kv ON dtkd.md_khuvuc_id = kv.md_khuvuc_id 
            LEFT JOIN md_loaidtkd ldt ON dtkd.md_loaidtkd_id = ldt.md_loaidtkd_id  
			LEFT JOIN md_nguondtkd ndt ON dtkd.md_nguondtkd_id = ndt.md_nguondtkd_id 
            WHERE 1=1 {0} {1})P ", filter, where_ex);
			
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
            s1.CreateRow(0).CreateCell(0).SetCellValue("Danh Sách Đối Tác Kinh Doanh");
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
            String saveAsFileName = "DoiTacKinhDoanh.xls";
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