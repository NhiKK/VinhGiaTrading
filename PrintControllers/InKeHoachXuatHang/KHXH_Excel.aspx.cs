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

public partial class PrintControllers_InKeHoachXuatHang_KHXH_Excel : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dateFrom = DateTime.ParseExact(Request.QueryString["from"], "dd/MM/yyyy", null);
        DateTime dateTo = DateTime.ParseExact(Request.QueryString["to"], "dd/MM/yyyy", null);
        String md_doitackinhdoanh_id = Request.QueryString["md_doitackinhdoanh_id"];
        string chkManv = Request.QueryString["manv"];
		string dateType = Request.QueryString["dateType"];
        // phân quyền theo nhóm
        String manv = UserUtils.getUser(Context);
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
		
		//trang thai
        string tt_ST = Request.QueryString["tt_ST"];
        string tt_HL = Request.QueryString["tt_HL"];
        string tt_KT = Request.QueryString["tt_KT"];
		
        String dsNhanVien = "";

        String strAccount = "";
        if (chkManv != null) // có chọn user thì chỉ lọc theo user đã chọn
        {
            strAccount = chkManv;
            dsNhanVien = String.Format(" AND khxh.nguoitao IN('{0}') ", strAccount);
        }
        else // ngược lại thì phân quyền theo nhóm
        {
            
            System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
            foreach (String item in lstAccount)
            {
                strAccount += String.Format(", '{0}'", item);
            }
            strAccount = String.Format("'{0}'{1}", manv, strAccount);
            dsNhanVien = nv.isadmin.Value ? "" : String.Format(" AND khxh.nguoitao IN({0}) ", strAccount);
        }

        

       
        String sql = this.CreateSql(dateFrom, dateTo, dsNhanVien, dateType, tt_ST, tt_HL, tt_KT);
        
        if (md_doitackinhdoanh_id != null)
        {
            sql += String.Format(" AND A.md_doitackinhdoanh_id = '{0}'", md_doitackinhdoanh_id);
        }
		
		sql += string.Format(" order by {0} asc, A.so_po asc ", dateType =="0"? "A.ngaygiaohang" : "A.ngayxonghang");
		
		DataTable dt = mdbc.GetData(sql);
		Response.Write(dt.Rows.Count);
		if (dt.Rows.Count > 0)
		{
			HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt, md_doitackinhdoanh_id);
			String saveAsFileName = String.Format("PackingList-{0}.xls", DateTime.Now);
			this.SaveFile(hssfworkbook, saveAsFileName);
		}
		else
		{
			Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
		}
    }
	
    public String CreateSql(DateTime dateFrom, DateTime dateTo, String dsNhanVien, string dateType, string tt_ST, string tt_HL, string tt_KT)
    {
		string sql_them = "", sql_them2 = "";
        int dem = 0;
        if (tt_ST == "on")
        {
            sql_them2 += " and khxh.md_trangthai_id = 'SOANTHAO'";
            dem++;
        }

        if (tt_HL == "on")
        {
            sql_them2 += " and khxh.md_trangthai_id = 'HIEULUC'";
            dem++;
        }
        
        if (tt_KT == "on")
        {
            sql_them2 += " and khxh.md_trangthai_id = 'KETTHUC'";
            dem++;
        }

        if (dem == 0)
        {
            sql_them2 = "";
        }
        else if (dem == 1)
        {
            sql_them += sql_them2;
        }
        else
        {
            string chuoi = sql_them2.Substring(4).Replace("and", "or");
            sql_them2 = " and (" + chuoi + ")";
            sql_them += sql_them2;
        }
		
        String str = String.Format(@"
                declare @tungay datetime = convert(datetime, '{0} 00:00:00', 103);
                declare @denngay datetime = convert(datetime, '{1} 23:59:59', 103);
                
                declare @tblKHXH table (
	                so_po nvarchar(MAX),
	                chungloaihang nvarchar(MAX),
	                cbm decimal(18, 4),
	                cbm_conlai decimal(18, 4),
	                shipmenttime datetime,
	                c_danhsachdathang_id nvarchar(50),
	                ngaygiaohang datetime,
	                ngayxonghang datetime,
	                cont20 numeric,
	                cont40 numeric,
	                cont40hc numeric,
	                ngayxuathang datetime,
	                ghichu nvarchar(MAX),
	                ketquakiemhang nvarchar(MAX),
	                ngaykiemhang datetime,
	                ngayxacnhantem nvarchar(MAX),
	                ngayxacnhanbaobi nvarchar(MAX),
	                ngaykhachkiem nvarchar(MAX),
	                tre int,
	                hangiaohang datetime,
	                thang nvarchar(MAX),
	                ngayxonghang_xxn datetime,
	                ngaytrenhat datetime,
	                khongtem bit,
	                khongbaobi bit,
	                md_doitackinhdoanh_id nvarchar(32),
	                c_donhang_id nvarchar(32)
                )
                insert into @tblKHXH
                select 
	                khxh.so_po, khxh.chungloaihang, khxh.cbm, khxh.cbm_conlai, khxh.shipmenttime, khxh.c_danhsachdathang_id, 
	                khxh.ngaygiaohang, khxh.ngayxonghang, khxh.cont20, khxh.cont40, khxh.cont40hc, khxh.ngayxuathang,
	                khxh.ghichu, khxh.ketquakiemhang, khxh.ngaykiemhang, 
	                (
		                case when khxh.khongtem = 0 and khxh.ngayxacnhantem is not null then (cast(datepart(d, khxh.ngayxacnhantem) as nvarchar) +'/'+ cast(datepart(m, khxh.ngayxacnhantem) as nvarchar)+'/'+ cast(datepart(yy, khxh.ngayxacnhantem) as nvarchar))
		                when khxh.khongtem = 0 and khxh.ngayxacnhantem is null then ' '
		                when khxh.khongtem = 1 then 'null'
		                else ' '
		                end 
	                )as ngayxacnhantem,
	                (
		                case when khxh.khongbaobi = 0 and khxh.ngayxacnhanbaobi is not null then (cast(datepart(d, khxh.ngayxacnhanbaobi) as nvarchar) +'/'+ cast(datepart(m, khxh.ngayxacnhanbaobi) as nvarchar)+'/'+ cast(datepart(yy, khxh.ngayxacnhanbaobi) as nvarchar))
                        when khxh.khongbaobi = 0 and khxh.ngayxacnhanbaobi is null then ' '
                        when khxh.khongbaobi = 1 then 'null'
                        else ' '
		                end 
	                )as ngayxacnhanbaobi,
	                (
		                case when khxh.khongkhachkiem = 0 and khxh.ngaykhachkiem is not null then (cast(datepart(d, khxh.ngaykhachkiem) as nvarchar) +'/'+ cast(datepart(m, khxh.ngaykhachkiem) as nvarchar)+'/'+ cast(datepart(yy, khxh.ngaykhachkiem) as nvarchar))
                        when khxh.khongkhachkiem = 0 and khxh.ngaykhachkiem is null then ' '
                        when khxh.khongkhachkiem = 1 then 'null'
                        else ' '
		                end 
	                )as ngaykhachkiem,
	                (
		                case when DATEDIFF(DAY, khxh.shipmenttime, DATEADD(DAY, 6, khxh.ngayxonghang)) > 0 
		                then DATEDIFF(DAY, khxh.shipmenttime, DATEADD(DAY, 6, khxh.ngayxonghang)) 
		                else null 
		                end
	                ) as tre,
	                isnull(khxh.ngaygiaohang, khxh.shipmenttime) as hangiaohang,
	                CAST(DATEPART(MONTH,khxh.ngaygiaohang) as nvarchar) + '/' + CAST(DATEPART(YEAR, khxh.ngaygiaohang) as nvarchar),
	                khxh.shipmenttime - 7,
	                khxh.shipmenttime - 35,
	                khxh.khongtem, 
	                khxh.khongbaobi,
	                khxh.md_doitackinhdoanh_id,
	                khxh.c_donhang_id
                from c_kehoachxuathang khxh
                left join c_donhang dh on khxh.c_donhang_id = dh.c_donhang_id
                where 
	                khxh.{3} between @tungay and @denngay
	                AND dh.md_trangthai_id != 'KETTHUC'
                    {2} {4}

                declare @tblINV table (donhangId nvarchar(32), ngay_motokhai datetime)
                insert into @tblINV
                select dpkl.c_donhang_id, inv.ngay_motokhai 
                from c_packinginvoice inv
                inner join c_dongpklinv dpkl on dpkl.c_packinginvoice_id = inv.c_packinginvoice_id
                where 
	                dpkl.c_dongnhapxuat_id in
	                (
		                select c_dongnhapxuat_id 
		                from c_dongnhapxuat
		                where c_dongdonhang_id in (
			                select c_dongdonhang_id from c_dongdonhang where c_donhang_id in (
				                select khxh.c_donhang_id from @tblKHXH khxh
			                )
		                )
	                )
                
                select 
                    A.*, 
                    (DATEDIFF(day, A.shipmenttime, A.etd)) as tre_nv
                from (
	                select 
		                khxh.so_po, 
		                khxh.chungloaihang, 
		                khxh.cbm, 
		                khxh.cbm_conlai, 
		                khxh.shipmenttime, 
		                khxh.c_danhsachdathang_id, 
		                khxh.ngaygiaohang, 
		                khxh.ngayxonghang, 
		                khxh.cont20, khxh.cont40, khxh.cont40hc, 
		                khxh.ngayxuathang as ngaymotokhai, 
		                khxh.ghichu, 
		                khxh.ketquakiemhang, 
		                khxh.ngaykiemhang, 
		                dtkd.ma_dtkd as nhacungung, 
		                @tungay as tungay, 
		                @denngay as denngay,
		                khxh.ngayxacnhantem,
		                khxh.ngayxacnhanbaobi,
		                khxh.ngaykhachkiem,
		                khxh.tre,
		                khxh.hangiaohang,
		                khxh.thang,
		                khxh.ngayxonghang_xxn,
		                khxh.ngaytrenhat,
		                khxh.khongtem, 
		                khxh.khongbaobi,
		                (select ma_dtkd from md_doitackinhdoanh dtkd, c_donhang dh where dtkd.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id AND dh.c_donhang_id = khxh.c_donhang_id) as ma_dtkd,
		                (select hoten from nhanvien where manv = (select distinct nguoitao from c_danhsachdathang where sochungtu = khxh.c_danhsachdathang_id AND md_trangthai_id = 'HIEULUC')) as nguoilappi,
		                (select top 1 inv.ngay_motokhai from @tblINV inv where inv.donhangId = khxh.c_donhang_id order by inv.ngay_motokhai asc) as etd,
		                dtkd.md_doitackinhdoanh_id
	                from @tblKHXH khxh
		                left join md_doitackinhdoanh dtkd on khxh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id		     
                )A 
                where 1=1
			", 
            dateFrom.ToString("dd/MM/yyyy"), 
            dateTo.ToString("dd/MM/yyyy"), 
            dsNhanVien, 
            dateType =="0"? "ngaygiaohang" : "ngayxonghang", 
            sql_them
        );
        // select top 1 pkl.etd 
        // from c_dongpklinv dpkl, c_packinginvoice pkl 
        // where dpkl.c_packinginvoice_id = pkl.c_packinginvoice_id
        // and dpkl.c_donhang_id = dh.c_donhang_id
        // ) as etd
        //throw new ArgumentNullException(str);	
        return str;
    }
	
	public HSSFWorkbook CreateWorkBookPO(DataTable dt, string md_doitackinhdoanh_id)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
		HSSFSheet hssfsheet = (HSSFSheet)s1;
		hssfsheet.DisplayGridlines = false;
		Excel_Format ex_fm = new Excel_Format(hssfworkbook); 
        ICellStyle cellBold = ex_fm.getcell(9, true, true, "", "L", "T");
		//-- 
		ICellStyle cellHeader = ex_fm.getcell(18, true, true, "", "C", "T");
		//-- 
		ICellStyle cellHeader_n = ex_fm.getcell(9, true, true, "", "C", "T");
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
		ICellStyle borderright = ex_fm.getcell(9, false, true, "LRBT", "R", "T");
		//--
		ICellStyle borderleft = ex_fm.getcell(9, false, true, "LRBT", "T", "T");
		//--
		ICellStyle borderWrap = ex_fm.getcell(9, false, true, "LRBT", "C", "T");
		//--
		ICellStyle signBold = ex_fm.getcell(12, true, true, "", "C", "C");
		//--
		IDataFormat dataFormatCustom = hssfworkbook.CreateDataFormat();
		//--
		int heigh = 22;
		int row = 0;
		//set A1 - A3
		string[] a = { "LỊCH XUẤT HÀNG"," "," "};
		string[] a_value = {"","nhacungung",""};
		for(int i = 0; i < a.Count(); i++)
		{		
			s1.CreateRow(row).CreateCell(0).SetCellValue(a[i]);
			s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 23));
			s1.GetRow(row).HeightInPoints = heigh;
			
			if(a_value[i] != "") {
				if(md_doitackinhdoanh_id != "" & md_doitackinhdoanh_id != null) {
					s1.GetRow(row).CreateCell(0).SetCellValue("Nhà cung ứng: " + dt.Rows[0][a_value[i]].ToString());
				}
				else {
					s1.GetRow(row).CreateCell(0).SetCellValue("Nhà cung ứng: "); 
				}
			}
			if(i == 0 | i == 3)
			{
				s1.GetRow(row).HeightInPoints = 30;
				s1.GetRow(row).GetCell(0).CellStyle = cellHeader;
			}
			else
			{
				s1.GetRow(row).GetCell(0).CellStyle = cellHeader_n;
			}
			
			
			row++;
		}
		//--

		//set F4 - F5
		string[] b = { "TỪ NGÀY: ","ĐẾN NGÀY: "};
		string[] b_value = {"tungay","denngay"};
		for(int i = 0; i < b.Count(); i++)
		{
			if(i == 0) {
				s1.CreateRow(row).CreateCell(1).SetCellValue(b[i] + " " + DateTime.Parse(dt.Rows[0][b_value[i]].ToString()).ToString("dd/MM/yyyy"));
				//s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 2));
				//s1.AddMergedRegion(new CellRangeAddress(row, row, 3, 5));
				s1.GetRow(row).GetCell(1).CellStyle = cellBold;
			}
			else {
				s1.GetRow(row).CreateCell(3).SetCellValue(b[i] + " " +DateTime.Parse(dt.Rows[0][b_value[i]].ToString()).ToString("dd/MM/yyyy"));
				//s1.AddMergedRegion(new CellRangeAddress(row, row, 6, 8));
				//s1.AddMergedRegion(new CellRangeAddress(row, row, 9, 11));
				s1.GetRow(row).GetCell(3).CellStyle = cellBold;
			}			
		}
		row++;
		
		// set A13 - All
		// -- Header
		int cell = 0;
		string[] d = {"STT","KHÁCH HÀNG","P/O","ĐƠN ĐẶT HÀNG","NHÀ CUNG ỨNG","CHỦNG LOẠI HÀNG",
						"CBM","CBM CÒN LẠI","Shipment time (PO)","NGÀY XONG HÀNG (XXN)","NGÀY XONG HÀNG (D/C)",
						"NGÀY TRỂ NHẤT HƯỚNG DẪN TEM, BAO BÌ","KHÔNG TEM","NGÀY XN TEM","KHÔNG BAO BÌ","NGÀY XN BAO BÌ",
						"NGÀY KIỂM HÀNG","KHÁCH KIỂM HÀNG","KẾT QUẢ KIỂM HÀNG","NGÀY VẬN ĐƠN (ETD)","NHÂN VIÊN CHỨNG TỪ",
						"NGƯỜI LẬP","TRỄ(NV)","TRỄ (NCU)","GHI CHÚ"
					 };
		IRow rowColumnHeader = s1.CreateRow(row);
		rowColumnHeader.HeightInPoints = 47;
		for(int i = 0; i < d.Count(); i++)
		{		
			int width = 5000;
			rowColumnHeader.CreateCell(cell).SetCellValue(d[i]);
			rowColumnHeader.GetCell(i).CellStyle = borderWrap;
			//STT
			if(i == 0) {
				width = 1000;
			}
			//NHÀ CUNG ỨNG
			else if(i == 4) {
				width = 3000;
			}
			//ĐƠN ĐẶT HÀNG	
			else if(i == 5) {
				width = 2000;
			}
			else if(i == 6) {
				width = 2000;
			}
			else if(i == 7) {
				width = 2000;
			}
			else if(i == 8) {
				width = 3000;
			}
			else if(i == 9) {
				width = 3000;
			}
			//CHỦNG LOẠI HÀNG, CBM, CBM CÒN LẠI
			else if(i == 4 | i == 5 | i ==6) {
				width = 2000;
			}
			//Shipment time (PO)
			else if(i == 7 | i == 8 | i== 9 | i == 10) {
				width = 3800;
			}
			else if(i == 11 | i == 12 | i == 13 | i == 14) {
				width = 3000;
			}
			else if(i == 15 | i == 16 | i == 17) {
				width = 0;
			}
			else if(i == 22 | i == 23) {
				width = 2500;
			}
			else
			{
				width = 5000;
			}
			
			s1.SetColumnWidth(cell, width);
			cell++;
		}
		row ++;
		
		// -- Details
		// create column
		string sochungtu = "", row_value = "";
		//thu tu tong cong // dem so dong hang cua tung sochungtu
		int count_total = 1, count_row = 0, count_row_tt = 1;
		// tap hop so luong dong tong cong // tap hop cac vi tri cua tong cong
		string n_total = "", l_total = "", l_cbm = "";
		// create detail row
		string[] row_detail_value = {"","ma_dtkd","so_po","c_danhsachdathang_id","nhacungung","chungloaihang",
		"cbm","cbm_conlai","shipmenttime","ngayxonghang_xxn","ngayxonghang",
		"ngaytrenhat","khongtem","ngayxacnhantem","khongbaobi","ngayxacnhanbaobi",
		"ngaykiemhang","ngaykhachkiem","ketquakiemhang","etd","",
		"nguoilappi","tre_nv","tre","ghichu"
		};
		
		var stt = 1;
		for (int i = 0; i < dt.Rows.Count; i++)
        {
			try {
				sochungtu = dt.Rows[i-1]["thang"].ToString();
			}
			catch { }
			
			if(dt.Rows[i]["thang"].ToString() != sochungtu)
			{ 
				s1.CreateRow(row).CreateCell(0).SetCellValue("Tháng " + dt.Rows[i]["thang"].ToString());
				s1.GetRow(row).GetCell(0).CellStyle = cellBold;
				CellRangeAddress cellRange023 = new CellRangeAddress(row, row, 0, 24);
				s1.AddMergedRegion(cellRange023);
				s1.GetRow(row).HeightInPoints = 18;
				ex_fm.set_border(cellRange023, "LRTB", hssfsheet);
				//--
				sochungtu = dt.Rows[i]["thang"].ToString();
				row++;
			}
			
			if(dt.Rows[i]["thang"].ToString() == sochungtu)
			{
				var row_detail = s1.CreateRow(row);
				s1.GetRow(row).HeightInPoints = 22;
				for(int j = 0; j < row_detail_value.Length; j++) {
					var row_cel_detail = row_detail.CreateCell(j);
					if(j == 0) {
						row_cel_detail.SetCellType(CellType.Numeric);
						row_cel_detail.SetCellValue(stt);
						row_cel_detail.CellStyle = borderWrap;
					}
					else if(j == 1 | j == 2 | j == 3 | j == 4 | j == 5 | j == 20 | j == 21 | j == 24) {
						row_cel_detail.CellStyle = borderleft;
						try { row_cel_detail.SetCellValue(dt.Rows[i][row_detail_value[j]].ToString()); } catch { }
					}
					else if ( j == 6 | j == 7 | j == 22 | j == 23) {
						row_cel_detail.CellStyle = borderright;
						row_cel_detail.SetCellType(CellType.Numeric);
						double dbl_val = 0;
						try { dbl_val = double.Parse(dt.Rows[i][row_detail_value[j]].ToString()); } catch { }
						row_cel_detail.SetCellValue(dbl_val);
					}
					else if(j > 0){
						row_cel_detail.CellStyle = borderWrap;
						string date_cell = "";
						try { date_cell = dt.Rows[i][row_detail_value[j]].ToString(); } catch { }
						
						try { 
							if(date_cell != "" & date_cell != null) {
								date_cell = DateTime.Parse(date_cell).ToString("dd/MM/yyyy");
							}
						} catch { }
						row_cel_detail.SetCellValue(date_cell);
						//row_cel_detail.CellStyle.DataFormat = dataFormatCustom.GetFormat("dd/MM/yyyy");
					}
				}
				stt++;
			}
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


