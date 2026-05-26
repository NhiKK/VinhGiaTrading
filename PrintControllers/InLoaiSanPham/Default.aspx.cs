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
public partial class PrintControllers_Default : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();
    private String fmt0 = "#,##0" , fmt0i0 = "#,##0.0" , fmt0i00 = "#,##0.00" , fmt0i000 = "#,##0.000";

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        {
			String select = String.Format(@"select * from md_chungloai order by code_cl");

			DataTable dt = mdbc.GetData(select);
			if (dt.Rows.Count != 0)
			{
				HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
				String saveAsFileName = String.Format("ChungLoaiSP-{0}.xls", DateTime.Now);
				this.SaveFile(hssfworkbook, saveAsFileName);
				Response.Write(select);
			}
			else
			{
				Response.Write("<h3>Chủng loại không có dữ liệu</h3>");
			}
        }
        //catch (Exception ex)
        {
            //Response.Write(String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex.Message));
        }
    }		

    public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
		HSSFSheet hssfsheet = (HSSFSheet)s1;
		
		Excel_Format ex_fm = new Excel_Format(hssfworkbook); 
		//--
		ICellStyle border = ex_fm.getcell(12, false, true, "LRBT", "L", "T");
		//--
		ICellStyle borderright = ex_fm.getcell(12, false, true, "LRBT", "L", "T");
		//--
		//--
		ICellStyle borderWrap = ex_fm.getcell(12, true, true, "LRBT", "C", "T");
		int heigh = 10;
		int row = 0;
		
		// set A13 - All
		// -- Header
		int cell = 0;
		string[] d = {"Code chủng loại", "TV Ngắn", "TA Ngắn", "Mô tả"};
		IRow rowColumnHeader = s1.CreateRow(row);
		rowColumnHeader.HeightInPoints = 20;
		for(int i = 0; i < d.Count(); i++)
		{		
			int with = 7000;
			rowColumnHeader.CreateCell(cell).SetCellValue(d[i]);
			rowColumnHeader.GetCell(i).CellStyle = borderWrap;
			s1.SetColumnWidth(cell,with);
			cell++;
		}
		row ++;
		// -- Details
		// create detail row
		for (int i = 0; i < dt.Rows.Count; i++)
        {
			string[] e_value = {"code_cl","tv_ngan","ta_ngan","mota"};
			IRow row_t = s1.CreateRow(row); row_t.HeightInPoints = 20;
			//
			int cell_t = 0;
			for (int j = 0; j < e_value.Count(); j++)
			{
				row_t.CreateCell(cell_t).SetCellValue(dt.Rows[i][e_value[j]].ToString());;
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
			s1.GetRow(row).GetCell(3).CellStyle = borderright;
			row++;
        }
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
