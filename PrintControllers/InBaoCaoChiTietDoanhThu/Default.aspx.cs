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
public partial class PrintControllers_InBaoCaoChiTietDoanhThu_Default : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();
    private String fmt0 = "#,##0" , fmt0i0 = "#,##0.0" , fmt0i00 = "#,##0.00" , fmt0i000 = "#,##0.000";

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        {
			string ma_dtkd = "", md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"],where_dtkd = "", tundengay = "";
			if(md_doitackinhdoanh_id != null)
			{
				ma_dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == md_doitackinhdoanh_id).Select(s => s.ma_dtkd).FirstOrDefault();
			}

			if(ma_dtkd.Length > 0)
			{
				where_dtkd = " and j.ma_dtkd = '" + ma_dtkd +"'";
			}
			
			//lay tu ngay den ngay
			DateTime tungay = DateTime.ParseExact(Request.QueryString["startdate"], "dd/MM/yyyy", null);
			DateTime denngay = DateTime.ParseExact(Request.QueryString["enddate"], "dd/MM/yyyy", null);
			
			tundengay = "TỪ NGÀY " + tungay.ToString("dd/MM/yyyy") +" ĐẾN NGÀY " + denngay.ToString("dd/MM/yyyy");
			string sql = String.Format(@"
			select
				a.so_inv,
				convert(varchar, a.ngay_motokhai, 103) AS ngay_motokhai,
				b.ma_dtkd,
				c.ma_quocgia,
				g.sochungtu,
				e.ma_sanpham,
				e.mota_tiengviet,
				f.ten_dvt,
				k.sophieu as phieunhap, 
				CONVERT(varchar, k.ngay_phieu, 103) as ngay_phieu_nhap,
				isnull(l.slthuc_nhapxuat,0) as slthuc_nhap,
				isnull(l.dongia,0) as dongia_nhap,
				isnull(k.phicong,0) as phicong,
				isnull(k.phitru,0)as phitru,
				k.mota,
				z.sophieu as phieuxuat, 
				CONVERT(varchar, z.ngay_phieu, 103) as ngay_phieu_xuat,
				isnull(x.slthuc_nhapxuat,0) as slthuc_nhapxuat,
				isnull(x.dongia,0) as dongia_xuat,
				z.soseal,
				z.container,
				d.soluong,
				d.gia,
				a.discount,
				a.giatricong_po,
				a.giatritru_po,
				a.giatri_cong,
				a.giatritru_po,
				j.ma_dtkd AS kh,
				h.v2
			from c_packinginvoice a
			/* doi tac kinh doanh */
			left join md_doitackinhdoanh b on b.md_doitackinhdoanh_id = a.md_doitackinhdoanh_id
			left join md_quocgia c on c.md_quocgia_id = b.md_quocgia_id
			/* dong hang invoice */
			left join c_dongpklinv d on d.c_packinginvoice_id = a.c_packinginvoice_id
			/* lay dong hang */
			left join md_sanpham e on e.md_sanpham_id = d.md_sanpham_id
			left join md_donvitinhsanpham f on f.md_donvitinhsanpham_id = e.md_donvitinhsanpham_id
			/* lay don hang */
			left join c_donhang g on g.c_donhang_id = d.c_donhang_id
			left join c_dongdonhang h on h.c_donhang_id = g.c_donhang_id and h.md_sanpham_id = d.md_sanpham_id
			left join md_doitackinhdoanh j on j.md_doitackinhdoanh_id = h.nhacungungid
			/* lay phieu nhap */
			left join c_nhapxuat k on k.c_donhang_id = g.c_donhang_id and k.md_loaichungtu_id = 'NK'
			left join c_dongnhapxuat l on l.c_nhapxuat_id = k.c_nhapxuat_id and l.md_sanpham_id = d.md_sanpham_id
			/* lay phieu xuat */
			left join c_nhapxuat z on z.c_donhang_id = g.c_donhang_id and z.md_loaichungtu_id = 'XK'
			left join c_dongnhapxuat x on x.c_nhapxuat_id = z.c_nhapxuat_id and x.c_dongdonhang_id = h.c_dongdonhang_id and x.md_sanpham_id = d.md_sanpham_id
			where a.md_trangthai_id = 'HIEULUC' {0}
				and a.ngay_motokhai >= convert(date,'{1}',103) and a.ngay_motokhai <= convert(date,'{2}',103)
			group by 
				a.so_inv,
				a.ngay_motokhai,
				b.ma_dtkd,
				c.ma_quocgia,
				g.sochungtu,
				e.ma_sanpham,
				e.mota_tiengviet,
				f.ten_dvt,
				k.sophieu, 
				k.ngay_phieu,
				l.slthuc_nhapxuat,
				l.dongia,
				k.phicong,
				k.phitru,
				k.mota,
				z.sophieu, 
				z.ngay_phieu,
				x.slthuc_nhapxuat,
				x.dongia,
				z.soseal,
				z.container,
				d.soluong,
				d.gia,
				a.discount,
				a.giatricong_po,
				a.giatritru_po,
				a.giatri_cong,
				a.giatritru_po,
				j.ma_dtkd,
				h.v2
			order by a.ngay_motokhai asc
			", where_dtkd,tungay.ToString("dd/MM/yyyy"), denngay.ToString("dd/MM/yyyy"));
			
			DataTable dt = mdbc.GetData(sql);
			
			if (dt.Rows.Count != 0)
			{
				HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt,tundengay);
				String saveAsFileName = String.Format("InBaoCaoChiTietDoanhThu-{0}.xls", DateTime.Now);
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

    public HSSFWorkbook CreateWorkBookPO(DataTable dt, string tundengay)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
		HSSFSheet hssfsheet = (HSSFSheet)s1;
		
		Excel_Format ex_fm = new Excel_Format(hssfworkbook); 
        ICellStyle cellBold = ex_fm.getcell(11, true, true, "", "L", "T");
		//-- 
		ICellStyle cellHeader = ex_fm.getcell(11, false, true, "", "C", "T");
		//-- 
		ICellStyle cellHeader_n = ex_fm.getcell(11, true, true, "", "C", "T");
		//--
		ICellStyle border_n = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##0.0");
		//--
		ICellStyle border_n1 = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##0.00");
		//--
		ICellStyle border_n2 = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##");
		//--
		ICellStyle border = ex_fm.getcell(11, false, false, "LRBT", "L", "T");
		//--
		ICellStyle border_tt = ex_fm.getcell(11, false, false, "LRBT", "C", "T");
		//--
		ICellStyle borderWrap = ex_fm.getcell(11, true, true, "LRBT", "C", "T");
		//--
		int heigh = 22;
		int row = 1;
		//set B1
		string[] a = { "BÁO CÁO  CHI TIẾT DOANH THU" , tundengay};
		for(int n = 0; n < a.Count(); n++)
		{		
			s1.CreateRow(row).CreateCell(0).SetCellValue(a[n]);
			s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 33));
			s1.GetRow(row).GetCell(0).CellStyle = cellHeader;
			row++;
		}

		// set A5 - AH5
		// -- Header
		int cell_h1 = 0;
		IRow rowColumnHeader1 = s1.CreateRow(row);
		string[] b = {"STT","Invoice","Ngày MTK","Tên KH","Quốc gia","PO","MAHH","Mô tả","DVT"
					 ,"Phiếu nhập"," "," "," "," "," "," "," ","Phiếu xuất"," "," "," "," "
					 ,"Số cont","Số seal","Invoice"," "," ","DIS","P/O+","P/O-","INV+","INV-","NCU","CBM"};
		
		//rowColumnHeader.HeightInPoints = heigh;
		for(int n = 0; n < b.Count(); n++)
		{		
			int with = 0;
			
			if(b[n] == "STT" | b[n] == "DVT" )
				with = 1500;
			else if (b[n] == "Invoice")
				with = 4500;
			else if (b[n] == "Ngày MTK")
				with = 3500;
			else if (b[n] == "Quốc gia")
				with = 2500;
			else if (b[n] == "PO")
				with = 7500;
			else
				with = 4500;
			
			rowColumnHeader1.CreateCell(cell_h1).SetCellValue(b[n]);
			rowColumnHeader1.GetCell(cell_h1).CellStyle = borderWrap;
			s1.SetColumnWidth(cell_h1,with);
			cell_h1++;
		}
		row++;
		// -- Header
		int cell_h2 = 0;
		IRow rowColumnHeader2 = s1.CreateRow(row);
		string[] c = {" "," "," "," "," "," "," "," "," "
					 ,"Số phiếu","Ngày","Số lượng","Đơn giá","TT","Phí+","Phí-","Diễn giải","Số phiếu","Ngày","Số lượng","Đơn giá","TT"
					 ,"Số cont","Số seal","Số lượng","Đơn giá","TT"," "," "," "," "," "," "," "};
		//rowColumnHeader.HeightInPoints = heigh;
		for(int n = 0; n < c.Count(); n++)
		{		
			rowColumnHeader2.CreateCell(cell_h2).SetCellValue(c[n]);
			rowColumnHeader2.GetCell(cell_h2).CellStyle = borderWrap;
			if( n == 9 | n == 10 | n == 11 | n == 12 | n == 13 | n == 14 | n == 15 | n == 16 )
				s1.AddMergedRegion(new CellRangeAddress(row - 1 , row - 1, 9, 16));
			else if(n == 17 | n == 18 | n == 19 | n == 20 | n == 21 )
				s1.AddMergedRegion(new CellRangeAddress(row - 1 , row - 1, 17, 21));
			else if(n == 24 | n == 25 | n == 26)
				s1.AddMergedRegion(new CellRangeAddress(row - 1 , row - 1, 24, 26));
			else
				s1.AddMergedRegion(new CellRangeAddress(row-1, row, cell_h2, cell_h2));
			cell_h2++;
		}
		row++;
		
		for (int n = 0; n < dt.Rows.Count; n++)
		{
			
			IRow row_t = s1.CreateRow(row);
			//
			int cell_invoice = 0;
			string[] d_value = { " ",
								"so_inv",
								"ngay_motokhai",
								"ma_dtkd",
								"ma_quocgia",
								"sochungtu",
								"ma_sanpham",
								"mota_tiengviet",
								"ten_dvt",
								"phieunhap", 
								"ngay_phieu_nhap",
								"slthuc_nhap",
								"dongia_nhap",
								"thanhtien_nhap",
								"phicong",
								"phitru",
								"mota",
								"phieuxuat", 
								"ngay_phieu_xuat",
								"slthuc_nhapxuat",
								"dongia_xuat",
								"thanhtien_xuat",
								"soseal",
								"container",
								"soluong",
								"gia",
								"thanhtien_inv",
								"discount",
								"giatricong_po",
								"giatritru_po",
								"giatri_cong",
								"giatritru_po",
								"kh",
								"v2"
								};
			for (int i = 0; i < d_value.Count(); i++)
			{
				string val = d_value[i];
				if(i == 0)
					row_t.CreateCell(cell_invoice).SetCellValue(n+1);
				else if(val == "thanhtien_nhap") 
					row_t.CreateCell(cell_invoice).CellFormula = String.Format("L{0} * M{0}",row + 1);
				else if(val == "thanhtien_xuat") 
					row_t.CreateCell(cell_invoice).CellFormula = String.Format("T{0} * U{0}",row + 1);
				else if(val == "thanhtien_inv") 
					row_t.CreateCell(cell_invoice).CellFormula = String.Format("Y{0} * Z{0}",row + 1);
				else if(val == "slthuc_nhap" | val == "dongia_nhap" | val == "phicong" | val == "phitru" | val == "slthuc_nhapxuat" | val == "dongia_xuat" | val == "soluong" | val == "gia" | val == "discount" | val == "giatricong_po" | val == "giatritru_po" | val == "giatri_cong" | val == "giatritru_po" | val == "v2" )
					row_t.CreateCell(cell_invoice).SetCellValue(double.Parse(dt.Rows[n][val].ToString()));
				else
					row_t.CreateCell(cell_invoice).SetCellValue(dt.Rows[n][val].ToString());
				
				
				if(i == 0 | val == "ngay_motokhai" | val == "ma_quocgia" | val == "ten_dvt" | val == "ngay_phieu_nhap" | val == "ngay_phieu_xuat" )
					row_t.GetCell(cell_invoice).CellStyle = border_tt;
				else if( val == "dongia_nhap" | val == "phicong" | val == "phitru" |  val == "dongia_xuat"  | val == "gia" | val == "giatricong_po" | val == "giatritru_po" | val == "giatri_cong" | val == "giatritru_po" | val == "thanhtien_nhap" | val == "thanhtien_xuat" | val == "thanhtien_inv" | val == "v2" )
					row_t.GetCell(cell_invoice).CellStyle = border_n1;
				else if( val == "discount" )
					row_t.GetCell(cell_invoice).CellStyle = border_n;
				else if( val == "slthuc_nhap" | val == "slthuc_nhapxuat" | val == "soluong" )
					row_t.GetCell(cell_invoice).CellStyle = border_n2;
				else
					row_t.GetCell(cell_invoice).CellStyle = border;	
				cell_invoice++;
			}
			row++;
		}
		row++;
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
