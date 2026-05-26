using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using HSSFUtils;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.HSSF.Record.AutoFilter;
public partial class PrintControllers_printST_PO_Default : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
			String select = String.Format(@"
				select a.md_trangthai_id, a.sochungtu, a.shipmenttime, a.ngaytao, b.ma_dtkd, c.hoten
				from c_donhang a
				left join md_doitackinhdoanh b on b.md_doitackinhdoanh_id = a.md_doitackinhdoanh_id
				left join nhanvien c on c.manv = a.nguoilap
				order by ngaytao asc
			");

			DataTable dt = mdbc.GetData(select);
			if (dt.Rows.Count != 0)
			{
				HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
				String saveAsFileName = String.Format("DonHangSoanTHao-{0}.xls", DateTime.Now);
				this.SaveFile(hssfworkbook, saveAsFileName);
				Response.Write(select);
			}
			else
			{
				Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
			}
        }
        catch (Exception ex)
        {
            Response.Write(String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex.Message));
        }
    }		

    public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
		HSSFSheet hssfsheet = (HSSFSheet)s1;
		
		Excel_Format ex_fm = new Excel_Format(hssfworkbook); 
        ICellStyle cellBold = ex_fm.getcell(12, true, true, "", "L", "T");
		//-- 
		ICellStyle cellHeader = ex_fm.getcell(18, true, true, "", "C", "T");
		//-- 
		ICellStyle cellHeader_n = ex_fm.getcell(12, false, true, "", "C", "T");
		//--
		ICellStyle celltext = ex_fm.getcell(12, false, true, "", "L", "T");
		//--
		ICellStyle rightBold = ex_fm.getcell(12, true, false, "", "R", "T");
		//--
		ICellStyle right = ex_fm.getcell(12, false, false, "", "R", "T");
		//--
		ICellStyle leftBold = ex_fm.getcell(12, true, false, "", "L", "T");
		//--
		ICellStyle left = ex_fm.getcell(12, false, false, "", "L", "T");
		//--
		ICellStyle border = ex_fm.getcell(12, false, true, "LRBT", "L", "T");
		//--
		ICellStyle borderright = ex_fm.getcell(12, false, true, "LRBT", "L", "T");
		//--
		ICellStyle borderonlyleft = ex_fm.getcell(12, false, true, "L", "T", "T");
		//--
		ICellStyle borderWrap = ex_fm.getcell(12, true, true, "LRBT", "C", "T");
		//--
		ICellStyle signBold = ex_fm.getcell(12, true, true, "", "C", "C");
		//--
		int heigh = 22;
		int row = 0;
		//set A1 - A3
		string[] a = { "Tổng hợp PO Soạn Thảo"};
		for(int i = 0; i < a.Count(); i++)
		{		
			s1.CreateRow(row).CreateCell(0).SetCellValue(a[i]);
			s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 5));
			s1.GetRow(row).HeightInPoints = heigh;
			s1.GetRow(row).GetCell(0).CellStyle = cellHeader;
			row++;
		}
		//--
		
		// set A13 - All 
		// -- Header
		int cell = 0;
		string[] d = {"Trạng Thái","Khách Hàng","Số Chứng Từ","Ngày Shipmenttime","Ngày Tạo","Người Lập"};
		IRow rowColumnHeader = s1.CreateRow(row);
		for(int i = 0; i < d.Count(); i++)
		{		
			int with = 6000;
			if(i == 2)
				with = 10000;
			rowColumnHeader.CreateCell(cell).SetCellValue(d[i]);
			rowColumnHeader.GetCell(i).CellStyle = borderWrap;
			s1.SetColumnWidth(cell,with);
			cell++;
		}
		row ++;
		// -- Details
		// create column
		string sochungtu = "", row_value = "";
		//thu tu tong cong // dem so dong hang cua tung sochungtu
		// create detail row
		for (int i = 0; i < dt.Rows.Count; i++)
        {
			string[] e_value = {"md_trangthai_id","ma_dtkd","sochungtu","shipmenttime","ngaytao","hoten"};
			IRow row_t = s1.CreateRow(row);
			//
			int cell_t = 0;
			for (int j = 0; j < e_value.Count(); j++)
			{
				if(j == 3| j == 4)
					row_t.CreateCell(cell_t).SetCellValue(DateTime.Parse(dt.Rows[i][e_value[j]].ToString()).ToString("dd/MM/yyyy"));
				else
					row_t.CreateCell(cell_t).SetCellValue(dt.Rows[i][e_value[j]].ToString());
				cell_t++;
			}
			
			try
			{
				for(int n = 0; n <= cell_t + 1; n++)
				{
					//  table style
					row_t.GetCell(n).CellStyle.WrapText = true;
					row_t.GetCell(n).CellStyle = border;
				}
			}
			catch{}
			row++;
        }
		//tru so dong du
        return hssfworkbook;
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
