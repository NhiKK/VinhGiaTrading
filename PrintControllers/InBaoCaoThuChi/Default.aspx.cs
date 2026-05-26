using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using HSSFUtils;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.HSSF.Record.AutoFilter;
public partial class PrintControllers_InBaoCaoThuChi_Default : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();
    private String fmt0 = "#,##0" , fmt0i0 = "#,##0.0" , fmt0i00 = "#,##0.00" , fmt0i000 = "#,##0.000";

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        {
			string ma_dtkd = "", md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"],where_dtkd = "";
			if(md_doitackinhdoanh_id != null)
			{
				ma_dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == md_doitackinhdoanh_id).Select(s => s.ma_dtkd).FirstOrDefault();
			}

			if(ma_dtkd.Length > 0)
			{
				where_dtkd = " and A.ma = '" + ma_dtkd +"'";
			}
			
			//lay tu ngay den ngay
			string startdate = Request.QueryString["startdate"];
			string enddate = Request.QueryString["enddate"];
			DateTime tungay_ = DateTime.ParseExact(startdate, "dd/MM/yyyy", null);
			DateTime denngay_ = DateTime.ParseExact(enddate, "dd/MM/yyyy", null);
			 
			int tungay = tungay_.Year;
			int denngay = denngay_.Year;
			int year = 0;
			string where_year = "";
			if(tungay - denngay == 0)
			{
				year = tungay;
				where_year = " and A.nam = '" + year + "'";
			}
			else
			{
				int count_year = denngay - tungay;
				year = tungay + count_year;
				where_year += " and A.nam >= " + tungay + " and A.nam <= " + year + " ";
			}
			
			string sql = String.Format(@"
			select * from (
				select distinct b.soky,
								CONVERT(varchar(MAX), c.nam) as nam ,
								t.ma_dtkd as ma,
								t.ngaytao as ngayt,
								t.codauky - t.nodauky as dauky,
								t.cotrongky as thu,
								t.notrongky as chi,
								t.cocuoiky - t.nocuoiky as cuoiky,
								t.a_kytrongnam_id,
								t.a_namtaichinh_id
				from a_tonghopcongno t
				left join a_kytrongnam b on b.a_kytrongnam_id = t.a_kytrongnam_id
				left join a_namtaichinh c on c.a_namtaichinh_id = t.a_namtaichinh_id
			) A
			inner join (
			select a.ma_dtkd as ma, max(a.ngaytao) as MaxDate, a.a_kytrongnam_id, a.a_namtaichinh_id
			from a_tonghopcongno a
			group by a.ma_dtkd, a.a_kytrongnam_id, a.a_namtaichinh_id
			) tm on tm.ma = A.ma 
				and tm.MaxDate = A.ngayt
				and tm.a_kytrongnam_id = A.a_kytrongnam_id 
				and tm.a_namtaichinh_id = A.a_namtaichinh_id
			where A.ma != '' {0} {1} order by A.ma asc, A.nam asc, A.soky asc
			", where_dtkd, where_year);
			
			string year_dtkd = String.Format(@"
			select * from (
				select distinct CONVERT(varchar(MAX), c.nam) as nam ,
								t.ma_dtkd as ma
				from a_tonghopcongno t
				left join a_kytrongnam b on b.a_kytrongnam_id = t.a_kytrongnam_id
				left join a_namtaichinh c on c.a_namtaichinh_id = t.a_namtaichinh_id
			) A
			where A.ma != '' {0} {1} order by A.ma asc, A.nam asc
			", where_dtkd, where_year);
			
			DataTable dt = mdbc.GetData(sql);
			DataTable dt_y = mdbc.GetData(year_dtkd);
			
			if (dt.Rows.Count != 0)
			{
				HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt, dt_y);
				String saveAsFileName = String.Format("InBaoCaoThuChi-{0}.xls", DateTime.Now);
				this.SaveFile(hssfworkbook, saveAsFileName);
			}
			else
			{
				Response.Write("<h3>Báo cáo không có dữ liệu</h3>");
			}
        }
        //catch (Exception ex)
        {
            //Response.Write(String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex.Message));
        }
    }		

    public HSSFWorkbook CreateWorkBookPO(DataTable dt, DataTable dt_y)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
		HSSFSheet hssfsheet = (HSSFSheet)s1;
		
		Excel_Format ex_fm = new Excel_Format(hssfworkbook); 
        ICellStyle cellBold = ex_fm.getcell(11, true, true, "", "L", "T");
		//-- 
		ICellStyle cellHeader = ex_fm.getcell(16, false, true, "", "C", "T");
		//-- 
		ICellStyle cellHeader_n = ex_fm.getcell(14, true, true, "", "C", "T");
		//--
		ICellStyle left = ex_fm.getcell(11, false, false, "", "L", "T");
		//--
		ICellStyle border = ex_fm.getcell(11, false, true, "LRBT", "L", "T");
		//--
		ICellStyle border_t = ex_fm.getcell(11, true, true, "LRBT", "L", "T");
		//--
		ICellStyle borderWrap = ex_fm.getcell(11, true, true, "LRBT", "C", "T");
		//--
		int heigh = 22;
		int row = 0;
		//set B1
		string[] a = { "BÁO CAO TỔNG HỢP THU/CHI"};
		for(int i = 0; i < a.Count(); i++)
		{		
			s1.CreateRow(row).CreateCell(1).SetCellValue(a[i]);
			s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 5));
			s1.GetRow(row).HeightInPoints = heigh;
			s1.GetRow(row).GetCell(1).CellStyle = cellHeader;
			row++;
		}
		//set B2 - F2
		string[] b = {" "," "};
		string[] b_value = {"ma","nam"};
		//-- Lay tung ma_dtkd va nam
		for(int i = 0; i < dt_y.Rows.Count; i++)
		{
			for(int n = 0; n < b.Count(); n++)
			{	
				if(n == 0)
					s1.CreateRow(row).CreateCell(1).SetCellValue(dt_y.Rows[i][b_value[n]].ToString());
				else
					s1.CreateRow(row).CreateCell(1).SetCellValue("Năm " + dt_y.Rows[i][b_value[n]].ToString());
				
				s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 5));
				s1.GetRow(row).HeightInPoints = heigh;
				s1.GetRow(row).GetCell(1).CellStyle = cellHeader_n;
				row++;
			}
			row++;
			// set B6 - F6
			// -- Header
			int cell_h = 1;
			string[] c = {" ","Đầu kỳ","Thu","Chi","Cuối kỳ"};
			IRow rowColumnHeader = s1.CreateRow(row);
			//rowColumnHeader.HeightInPoints = heigh;
			for(int n = 0; n < c.Count(); n++)
			{		
				int with = 5000;
				rowColumnHeader.CreateCell(cell_h).SetCellValue(c[n]);
				rowColumnHeader.GetCell(cell_h).CellStyle = borderWrap;
				s1.SetColumnWidth(cell_h,with);
				cell_h++;
			}
			row++;
			int end_row = row, start_row = row + 1 ;
			for (int n = 0; n < dt.Rows.Count; n++)
			{
				string[] d_value = {"soky","dauky","thu","chi","cuoiky"};
				IRow row_t = s1.CreateRow(row);
				//
				
				if(dt.Rows[n]["nam"].ToString() == dt_y.Rows[i]["nam"].ToString() & dt.Rows[n]["ma"].ToString() == dt_y.Rows[i]["ma"].ToString())
				{
					int cell_d = 1;
					for (int j = 0; j < d_value.Count(); j++)
					{
						if(j == 0)
						{
							row_t.CreateCell(cell_d).SetCellValue("Tháng " + dt.Rows[n][d_value[j]].ToString());
						}
						else
						{
							row_t.CreateCell(cell_d).SetCellValue(double.Parse(dt.Rows[n][d_value[j]].ToString()));
						}
						row_t.GetCell(cell_d).CellStyle = border;
						row_t.GetCell(cell_d).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
						cell_d++;
					}
					
					row++;
					end_row++;
					
				}	
			}
			string[] e = {"Tổng cộng","SUM(C{0},C{1})","SUM(D{0},D{1})","SUM(E{0},E{1})","SUM(F{0},F{1})"};
			int cell_t = 1;
			for(int n = 0; n < e.Count(); n++)
			{	
				if(n == 0)
					s1.CreateRow(row).CreateCell(cell_t).SetCellValue(e[n]);
				else
					s1.GetRow(row).CreateCell(cell_t).CellFormula = String.Format(e[n],start_row,end_row);
				s1.GetRow(row).GetCell(cell_t).CellStyle = border_t;
				s1.GetRow(row).GetCell(cell_t).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
				cell_t++;
			}
			row++;
		}
		row = row + 2;
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
