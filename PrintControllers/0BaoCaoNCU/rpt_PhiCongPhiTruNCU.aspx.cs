using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;

public partial class ReportWizard_RptKhachHang_rpt_DoanhSoNCU : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";
        DateTime StartDateVal = DateTime.ParseExact(startdate, "dd/MM/yyyy HH:mm:ss", null);
        int year = StartDateVal.Year - 1;
        string startdatePrev = "01/01/" + year + " 00:00:00";
        string enddatePrev = "31/12/" + year + " 23:59:59";
        string anco2018 = "ancotrading2018", anco = anco2018;
        if (year < 2018)
        {
            anco = "ancotrading";
        }
        string sql = string.Format(@"
            declare @start datetime = convert(datetime,'{0}', 103)
            declare @end datetime = convert(datetime,'{1}', 103)
            declare @table table (
				khachhang nvarchar(100),
				dsdh nvarchar(100),
				so_inv nvarchar(100),
				so_po nvarchar(100),
				ncc nvarchar(100),
				trigiaINV decimal(18,2),
				trigiaNCC decimal(18,2),
				totaldisINV decimal(18,2),
				tongtienPX decimal(18,2),
				phicongINV decimal(18,2),
				diengiaicongINV nvarchar(MAX),
				phitruINV decimal(18,2),
				diengiaitruINV nvarchar(MAX),
				phicongPO decimal(18,2),
				diengiaicongPO nvarchar(MAX),
				phitruPO decimal(18,2),
				diengiaitruPO nvarchar(MAX)
			)

			insert into @table
			select 
				kh.ma_dtkd as khachhang, 
				dsdh.sochungtu as dsdh,
				A.so_inv,
				dsdh.so_po,
				ncc.ma_dtkd as ncc,
				A.totalgross as trigiaINV,
				SUM(cdiv.thanhtien - cdiv.thanhtien*dh.discount/100) as trigiaNCC,
				A.totaldis as totaldisINV,
				SUM(dnx.slthuc_nhapxuat * dnx.dongia) as tongtienPX,
				A.giatri_cong as phicongINV,
				A.diengiai_cong as diengiaicongINV,
				A.giatri_tru as phitruINV,
				A.diengiai_tru as diengiaitruINV,
				A.giatricong_po as phicongPO,
				A.diengiaicong_po as diengiaicongPO,
				A.giatritru_po as phitruPO,
				A.diengiaitru_po as diengiaitruPO
			from(
				select 
				pkl.c_packinginvoice_id, 
				pkl.md_doitackinhdoanh_id as khachhangid, 
				pkl.so_inv, 
				pkl.totalgross, 
				pkl.totaldis, 
				pkl.giatri_cong,
				pkl.diengiai_cong,
				pkl.giatri_tru,
				pkl.diengiai_tru,
				pkl.giatricong_po,
				pkl.diengiaicong_po,
				pkl.giatritru_po,
				pkl.diengiaitru_po
				from c_packinginvoice pkl with (nolock)
				where 
				pkl.ngay_motokhai between @start and @end
				and pkl.md_trangthai_id = 'HIEULUC'
			)A
			inner join c_dongpklinv cdiv on A.c_packinginvoice_id = cdiv.c_packinginvoice_id
			inner join md_doitackinhdoanh kh on A.khachhangid = kh.md_doitackinhdoanh_id
			inner join md_doitackinhdoanh ncc on cdiv.nhacungungid = ncc.md_doitackinhdoanh_id
			inner join c_danhsachdathang dsdh on cdiv.c_danhsachdathang_id = dsdh.c_danhsachdathang_id
			inner join c_donhang dh on cdiv.c_donhang_id = dh.c_donhang_id
			inner join c_dongnhapxuat dnx on cdiv.c_dongnhapxuat_id = dnx.c_dongnhapxuat_id
			group by 
				kh.ma_dtkd, 
				dsdh.sochungtu,
				A.so_inv,
				dsdh.so_po,
				ncc.ma_dtkd,
				A.totalgross,
				A.totaldis,
				A.giatri_cong,
				A.diengiai_cong,
				A.giatri_tru,
				A.diengiai_tru,
				A.giatricong_po,
				A.diengiaicong_po,
				A.giatritru_po,
				A.diengiaitru_po
			order by A.so_inv, dsdh.so_po, ncc.ma_dtkd
			---------------------------
			select
				A.khachhang, 
				A.dsdh,
				A.so_inv,
				A.so_po,
				A.ncc,
				A.trigiaINV,
				A.trigiaNCC,
				A.totaldisINV,
				B.tongtienPX,
				C.trigiaNCCtotal - A.trigiaNCC as trigiaNCCKhac, 
				A.phicongINV,
				A.diengiaicongINV,
				A.phitruINV,
				A.diengiaitruINV,
				A.phicongPO,
				A.diengiaicongPO,
				A.phitruPO,
				A.diengiaitruPO
			from @table A
			inner join 
			(select A.so_inv, SUM(A.tongtienPX) as tongtienPX
			from @table A
			group by A.so_inv) B on A.so_inv = B.so_inv
			inner join 
			(select A.so_inv, A.so_po, SUM(A.trigiaNCC) as trigiaNCCtotal
			from @table A
			group by A.so_inv, A.so_po) C on A.so_inv = C.so_inv and A.so_po = C.so_po
        ", startdate, enddate, startdatePrev, enddatePrev, anco2018, anco);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"], StartDateVal.Year, year);
        }
        else
        {
            Response.Write("<h3>Không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay, int year, int yearPrev)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 18);
        int row = export.SetHeaderAnco(hssfsheet, "BÁO CÁO PHÍ CÔNG TRỪ THEO NHÀ CUNG ỨNG", tungay, denngay, true);

        #region Header Column
        int widthDF = 5000;
        List<ItemValue> lstHeader = new List<ItemValue>();
        var item = new ItemValue
        {
            item = "STT",
            value = "rowNum",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mã KH",
            value = "khachhang",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "DSĐH",
            value = "dsdh",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số Invoice",
            value = "so_inv",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "PO",
            value = "so_po",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NCU",
            value = "ncc",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Trị giá tổng INVOICE",
            value = "trigiaINV",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Trị giá Invoice theo NCU",
            value = "trigiaNCC",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Total discount",
            value = "totaldisINV",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tổng tiền PX",
            value = "tongtienPX",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tổng tiền NCC khác",
            value = "trigiaNCCKhac",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "P+(INV)",
            value = "phicongINV",
            witdh = widthDF
        };
        lstHeader.Add(item);

		item = new ItemValue
        {
            item = "Diễn giải INV+",
            value = "diengiaicongINV",
            witdh = widthDF
        };
        lstHeader.Add(item);
		
        item = new ItemValue
        {
            item = "P-(INV)",
            value = "phitruINV",
            witdh = widthDF
        };
        lstHeader.Add(item);
		
		item = new ItemValue
        {
            item = "Diễn giải INV-",
            value = "diengiaitruINV",
            witdh = widthDF
        };
        lstHeader.Add(item);
		
        item = new ItemValue
        {
            item = "P+(PO)",
            value = "phicongPO",
            witdh = widthDF
        };
        lstHeader.Add(item);
		
		item = new ItemValue
        {
            item = "Diễn giải PO+",
            value = "diengiaicongPO",
            witdh = widthDF
        };
        lstHeader.Add(item);
		
        item = new ItemValue
        {
            item = "P-(PO)",
            value = "phitruPO",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Diễn giải PO-",
            value = "diengiaitruPO",
            witdh = widthDF
        };
        lstHeader.Add(item);

        IRow rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.HeightInPoints = 40;
        for (int j = 0; j < lstHeader.Count; j++)
        {
            rowColumnHeader.CreateCell(j).SetCellValue(lstHeader[j].item);
            rowColumnHeader.GetCell(j).CellStyle = export.borderWrap;
            s1.SetColumnWidth(j, lstHeader[j].witdh);
        }
        row++;
        #endregion

        #region Set Table
        int start_sum = row + 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { 
                    "trigiaINV", "trigiaNCC", "totaldisINV", "tongtienPX", 
                    "trigiaNCCKhac", "phicongINV", "phitruINV", "phicongPO", "phitruPO"
                };

                if (lstHeader[j].value == "rowNum")
                {
                    cell.SetCellValue(i + 1);
                    cell.CellStyle = export.bordercenter;
                }
                else if (arrDecimal.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright2dec;
                }
                else if (!String.IsNullOrEmpty(lstHeader[j].value))
                {
                    string str = dt.Rows[i][lstHeader[j].value].ToString();
                    float heightMAX = export.MeasureTextHeight(str, lstHeader[j].witdh);
                    if (heightMAX > height)
                    {
                        height = heightMAX;
                    }
                    cell.SetCellValue(str);
                    cell.CellStyle = export.border;
                }
            }
            row_t.HeightInPoints = height;
            row++;
        }
        int end_sum = row;

        //IRow row_total = s1.CreateRow(row);
        //row_total.CreateCell(0).SetCellValue("Tổng cộng:");
        //row_total.GetCell(0).CellStyle = export.borderleftBold;

        //row_total.CreateCell(1).SetCellFormula(string.Format("SUM(B{0}:B{1})", start_sum, end_sum));
        //row_total.GetCell(1).CellStyle = export.borderrightBold2dec;
        //row_total.CreateCell(2).SetCellFormula(string.Format("SUM(C{0}:C{1})", start_sum, end_sum));
        //row_total.GetCell(2).CellStyle = export.borderrightBold2dec;
        //row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        hssfsheet.PrintSetup.Landscape = true;
        String saveAsFileName = String.Format("PhiCongPhiTruNCU-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
