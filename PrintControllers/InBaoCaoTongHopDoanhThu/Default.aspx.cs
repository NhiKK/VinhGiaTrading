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
public partial class PrintControllers_InBaoCaoTongHopDoanhThu_Default : System.Web.UI.Page
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
				ma_dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == md_doitackinhdoanh_id).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
			}

			if(ma_dtkd.Length > 0)
			{
				where_dtkd = " where ncu.md_doitackinhdoanh_id = '" + ma_dtkd +"'";
			}
			
			//lay tu ngay den ngay
			DateTime tungay = DateTime.ParseExact(Request.QueryString["startdate"], "dd/MM/yyyy", null);
			DateTime denngay = DateTime.ParseExact(Request.QueryString["enddate"], "dd/MM/yyyy", null);
			
			tundengay = "TỪ NGÀY " + tungay.ToString("dd/MM/yyyy") +" ĐẾN NGÀY " + denngay.ToString("dd/MM/yyyy");
								
			string sql = String.Format(@"
							select distinct 
								   pk_1.so_inv as so_inv,  
								   convert(varchar, pk_1.ngay_motokhai, 103) as ngay_motokhai,
								   dtkd.ma_dtkd,
								   qg.ma_quocgia,
								   dh_1.sochungtu,
								   dh_1.hoahong,
								   pm.ten_paymentterm,
								   A.code_cl,
								   A.tv_ngan as chungloai,
								   nx.sophieu as phieunhap,
								   convert(varchar, nx.ngay_phieu, 103) as ngay_nhap,
								   isnull((select sum(dnx.slthuc_nhapxuat) from c_dongnhapxuat dnx where dnx.c_nhapxuat_id = nx.c_nhapxuat_id ),0) as slthuc_nhap,
								   isnull(nx.grandtotal, 0) as tong_nhap,
								   isnull(nx.phicong, 0) as phicong,
								   isnull(nx.phitru, 0) as phitru,
								   nx.mota,
								   xk.sophieu as phieuxuat,
									convert(varchar, xk.ngay_phieu, 103) as ngay_xuat,
									isnull((select sum(dnx.slthuc_nhapxuat) from c_dongnhapxuat dnx where dnx.c_nhapxuat_id = xk.c_nhapxuat_id ),0) as slthuc_xuat,
									isnull(xk.grandtotal, 0) as tong_xuat,
									xk.socontainer,
									xk.soseal,
									isnull((select sum(dpk_1.soluong) from c_dongpklinv dpk_1 where dpk_1.c_packinginvoice_id = pk_1.c_packinginvoice_id ),0) as sl_inv,
									isnull(pk_1.totalgross, 0) as tong_inv,
									isnull(pk_1.totaldis, 0) as tong_dis,
									isnull(pk_1.giatricong_po, 0) as giatricong_po,
									isnull(pk_1.giatritru_po, 0) as giatritru_po,
									isnull(pk_1.giatri_cong, 0) as giatri_cong,
									isnull(pk_1.giatri_tru, 0) as giatri_tru,
									ncu.ma_dtkd as ncu,
									isnull(dh_1.totalcbm,0) as totalcbm,
									xk.loaicont,
									case
										when xk.loaicont = 'CONT20' then 1
										else 0
									end as con20,
									case
										when xk.loaicont = 'CONT40' then 1
										else 0
									end as con40,
									case
										when xk.loaicont = 'CONT45' then 1
										else 0
									end as con45,
									case
										when xk.loaicont = '40HC' then 1
										else 0
									end as con40hc,
									case
										when xk.loaicont = 'LE' then 1
										else 0
									end as conle
									 from (
								select distinct 
										cl.code_cl,
										cl.tv_ngan,
										dh.c_donhang_id,
										pk.c_packinginvoice_id,
										pk.md_doitackinhdoanh_id
							   from c_packinginvoice pk
								left join c_dongpklinv dpk on dpk.c_packinginvoice_id = pk.c_packinginvoice_id 
								left join c_donhang dh on dh.c_donhang_id = dpk.c_donhang_id
								left join md_sanpham sp on sp.md_sanpham_id = dpk.md_sanpham_id
								left join md_chungloai cl on cl.md_chungloai_id = sp.md_chungloai_id
							    where pk.md_trangthai_id = 'HIEULUC' 
										and pk.ngay_motokhai >= convert(date,'{1}',103)
										and pk.ngay_motokhai <= convert(date,'{2}',103)
							)A
							left join c_packinginvoice pk_1 on pk_1.c_packinginvoice_id = A.c_packinginvoice_id
							left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = A.md_doitackinhdoanh_id
							left join md_quocgia qg on qg.md_quocgia_id = dtkd.md_quocgia_id
							left join c_donhang dh_1 on dh_1.c_donhang_id = A.c_donhang_id
							left join c_dongdonhang ddh_1 on ddh_1.c_donhang_id = dh_1.c_donhang_id
							left join md_paymentterm pm on pm.md_paymentterm_id = dh_1.md_paymentterm_id
							left join c_nhapxuat nx on nx.c_donhang_id = A.c_donhang_id
							and nx.md_loaichungtu_id = 'NK'
							left join c_nhapxuat xk on xk.c_donhang_id = A.c_donhang_id
							and xk.md_loaichungtu_id = 'XK'
							left join md_doitackinhdoanh ncu on ncu.md_doitackinhdoanh_id = ddh_1.nhacungungid
							{0}
							order by dh_1.sochungtu asc, nx.sophieu asc, xk.sophieu asc
			", where_dtkd,tungay.ToString("dd/MM/yyyy"), denngay.ToString("dd/MM/yyyy"));
			
			DataTable dt = mdbc.GetData(sql);
			// Response.Write(sql);
			if (dt.Rows.Count != 0)
			{
				HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt,tundengay);
				String saveAsFileName = String.Format("InBaoCaoTongHopDoanhThu-{0}.xls", DateTime.Now);
				this.SaveFile(hssfworkbook, saveAsFileName);
			}
			else
			{
				Response.Write("<h3>Báo cáo không có dữ liệu</h3>");
			}
        }
        //catch (Exception ex)
        {
            //String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex.Message));
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
		ICellStyle celln = ex_fm.getcell2(11, false, false, "", "R", "c", "#,##0.00");
		//--
		ICellStyle cellnn_r = ex_fm.getcell(11, false, false, "", "R", "T");
		//--
		ICellStyle border_n = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##0.0");
		//--
		ICellStyle border_n1 = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##0.00");
		//--
		ICellStyle border_n2 = ex_fm.getcell2(11, false, false, "LRBT", "R", "T", "#,##");
		//--
		ICellStyle border = ex_fm.getcell(11, false, false, "LRBT", "L", "T");
		//--
		ICellStyle border_r = ex_fm.getcell(11, false, false, "LRBT", "R", "T");
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
		string[] b = {"STT","Invoice","Ngày MTK","Tên KH","Quốc gia","PO","% Hoa hồng phải trả","Hình thức Thanh Toán","Hệ hàng","Tên hệ hàng","Phiếu nhập"," "," "," "," "," "," ","Phiếu xuất"," "," "," "
					 ,"Số cont","Số seal","Invoice"," ","DIS(TT)","P/O+","P/O-","INV+","INV-","NCU","CBM","Số cont."," "," "," "," "};
		
		//rowColumnHeader.HeightInPoints = heigh;
		for(int n = 0; n < b.Count(); n++)
		{		
			int with = 0;
			
			if(b[n] == "STT" | b[n] == "DVT" | b[n] == "Hệ hàng" )
				with = 1500;
			else if (b[n] == "Invoice")
				with = 4500;
			else if (b[n] == "Ngày MTK")
				with = 3500;
			else if (b[n] == "Quốc gia")
				with = 2500;
			else if (b[n] == "PO" | b[n] ==  "Tên hệ hàng")
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
					
		string[] c = {" "," "," "," "," "," "," "," "," "," ","Số phiếu","Ngày","Tổng số lượng","TT","Phí+","Phí-","Diễn giải","Số phiếu","Ngày","Tổng số lượng","TT"
					 ," "," ","Tổng số lượng","TT"," "," "," "," "," "," "," ","20'","40'","40HC","45'","Lẻ"};
					 
		//rowColumnHeader.HeightInPoints = heigh;
		for(int n = 0; n < c.Count(); n++)
		{		
			rowColumnHeader2.CreateCell(cell_h2).SetCellValue(c[n]);
			rowColumnHeader2.GetCell(cell_h2).CellStyle = borderWrap;
			if( n == 10 | n == 11 | n == 12 | n == 13 | n == 14 | n == 15 | n == 16 )
				s1.AddMergedRegion(new CellRangeAddress(row - 1 , row - 1, 10, 16));
			else if(n == 17 | n == 18 | n == 19 | n == 20 )
				s1.AddMergedRegion(new CellRangeAddress(row - 1 , row - 1, 17, 20));
			else if( n == 23 | n == 24)
				s1.AddMergedRegion(new CellRangeAddress(row - 1 , row - 1, 23, 24));
			else if( n == 32 | n == 33 | n == 34 | n == 35 | n == 36)
				s1.AddMergedRegion(new CellRangeAddress(row - 1 , row - 1, 32, 36));
			else
				s1.AddMergedRegion(new CellRangeAddress(row-1, row, cell_h2, cell_h2));
			cell_h2++;
		}
		row++;
		int start_row = row;
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
							    "hoahong",
							    "ten_paymentterm",
							    "code_cl",
							    "chungloai",
							    "phieunhap",
							    "ngay_nhap",
							    "slthuc_nhap",
							    "tong_nhap",
							    "phicong",
							    "phitru",
							    "mota",
							    "phieuxuat",
							    "ngay_xuat",
							    "slthuc_xuat",
							    "tong_xuat",
							    "socontainer",
							    "soseal",
								"sl_inv",
							    "tong_inv",
							    "tong_dis",
							    "giatricong_po",
							    "giatritru_po",
							    "giatri_cong",
							    "giatri_tru",
							    "ncu",
							    "totalcbm",
								"con20",
								"con40",
								"con45",
								"con40hc",
								"conle"
								};

								
			for (int i = 0; i < d_value.Count(); i++)
			{
				string val = d_value[i];
				if(i == 0)
					row_t.CreateCell(cell_invoice).SetCellValue(n+1);
				else if(val == "hoahong" | val == "slthuc_nhap" | val == "tong_nhap" | val == "phicong" | val == "phitru" | val == "slthuc_xuat" | val == "tong_xuat" | val == "code_cl" | val == "sl_inv" | val == "tong_inv" | val == "tong_dis" | val == "giatricong_po" | val == "giatritru_po" | val == "giatri_cong" | val == "giatri_tru" | val == "totalcbm" | val == "con20" | val == "con40" | val == "con45" | val == "con40hc" | val == "conle" )
					row_t.CreateCell(cell_invoice).SetCellValue(double.Parse(dt.Rows[n][val].ToString()));
				else
					row_t.CreateCell(cell_invoice).SetCellValue(dt.Rows[n][val].ToString());
				
				
				if(i == 0 | val == "ngay_motokhai" | val == "ma_quocgia" | val == "code_cl" | val == "ngay_nhap" | val == "ngay_xuat" )
					row_t.GetCell(cell_invoice).CellStyle = border_tt;
				else if(val == "hoahong" | val == "slthuc_nhap" | val == "tong_nhap" | val == "phicong" | val == "phitru" | val == "slthuc_xuat" | val == "tong_xuat" | val == "sl_inv" | val == "tong_inv" | val == "tong_dis" | val == "giatricong_po" | val == "giatritru_po" | val == "giatri_cong" | val == "giatri_tru" | val == "totalcbm" )
					row_t.GetCell(cell_invoice).CellStyle = border_n1;
				else if( val == "discount" )
					row_t.GetCell(cell_invoice).CellStyle = border_n;
				else if( val == "con20" | val == "con40" | val == "con45" | val == "con40hc" | val == "conle" )
					row_t.GetCell(cell_invoice).CellStyle = border_r;
				else
					row_t.GetCell(cell_invoice).CellStyle = border;	
				cell_invoice++;
			}
			row++;
		}
		s1.CreateRow(row).CreateCell(1).SetCellValue("Tổng cộng: ");
		s1.GetRow(row).CreateCell(12).CellFormula = String.Format("SUM(M{0}:M{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(13).CellFormula = String.Format("SUM(N{0}:N{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(14).CellFormula = String.Format("SUM(O{0}:O{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(15).CellFormula = String.Format("SUM(P{0}:P{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(19).CellFormula = String.Format("SUM(T{0}:T{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(20).CellFormula = String.Format("SUM(U{0}:U{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(23).CellFormula = String.Format("SUM(X{0}:X{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(24).CellFormula = String.Format("SUM(Y{0}:Y{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(26).CellFormula = String.Format("SUM(AA{0}:AA{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(27).CellFormula = String.Format("SUM(AB{0}:AB{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(28).CellFormula = String.Format("SUM(AC{0}:AC{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(29).CellFormula = String.Format("SUM(AD{0}:AD{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(31).CellFormula = String.Format("SUM(AF{0}:AF{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(32).CellFormula = String.Format("SUM(AG{0}:AG{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(33).CellFormula = String.Format("SUM(AH{0}:AH{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(34).CellFormula = String.Format("SUM(AI{0}:AI{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(35).CellFormula = String.Format("SUM(AJ{0}:AJ{1})",start_row,row - 1);
		s1.GetRow(row).CreateCell(36).CellFormula = String.Format("SUM(AK{0}:AK{1})",start_row,row - 1);
		
		s1.GetRow(row).GetCell(1).CellStyle = cellBold;
		s1.GetRow(row).GetCell(12).CellStyle = celln;
		s1.GetRow(row).GetCell(13).CellStyle = celln;
		s1.GetRow(row).GetCell(14).CellStyle = celln;
		s1.GetRow(row).GetCell(15).CellStyle = celln;
		s1.GetRow(row).GetCell(19).CellStyle = celln;
		s1.GetRow(row).GetCell(20).CellStyle = celln;
		s1.GetRow(row).GetCell(23).CellStyle = celln;
		s1.GetRow(row).GetCell(24).CellStyle = celln;
		s1.GetRow(row).GetCell(26).CellStyle = celln;
		s1.GetRow(row).GetCell(27).CellStyle = celln;;
		s1.GetRow(row).GetCell(28).CellStyle = celln;;
		s1.GetRow(row).GetCell(29).CellStyle = celln;;
		s1.GetRow(row).GetCell(31).CellStyle = celln;;
		s1.GetRow(row).GetCell(32).CellStyle = cellnn_r;
		s1.GetRow(row).GetCell(33).CellStyle = cellnn_r;
		s1.GetRow(row).GetCell(34).CellStyle = cellnn_r;
		s1.GetRow(row).GetCell(35).CellStyle = cellnn_r;
		s1.GetRow(row).GetCell(36).CellStyle = cellnn_r;
		
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
