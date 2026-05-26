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

public partial class ReportWizard_RptKhachHang_rpt_DoanhSoHeHangNgayThang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        string sql = string.Format(@"
        declare @tungay datetime, @denngay datetime
        set @tungay = (select CONVERT(datetime, N'{0}', 103))
        set @denngay = (select CONVERT(datetime, N'{1}', 103))

		declare @table table (
			tieuchi int,
			code_cl varchar(3),
			t1 decimal(18,4),
			t2 decimal(18,4),
			t3 decimal(18,4),
			t4 decimal(18,4),
			t5 decimal(18,4),
			t6 decimal(18,4),
			t7 decimal(18,4),
			t8 decimal(18,4),
			t9 decimal(18,4),
			t10 decimal(18,4),
			t11 decimal(18,4),
			t12 decimal(18,4),
            t12nt decimal(18,4),
			tong decimal(18,4),
			tungay datetime,
			denngay datetime
		)

		insert into @table
		exec [dbo].[rpt_doanhsohehang_ngaythang] @tungay, @denngay, 0

		insert into @table
		exec [dbo].[rpt_doanhsohehang_ngaythang] @tungay, @denngay, 1

        insert into @table
		exec [dbo].[rpt_doanhsohehang_ngaythang] @tungay, @denngay, 2

		select 
		(case A.tieuchi when 0 then 'PO' when 1 then 'INV' else 'TC_CL' end) as tieuchi, 
		A.code_cl, 
		isnull(B.t1, 0) as t1, 
		isnull(B.t2, 0) as t2, 
		isnull(B.t3, 0) as t3, 
		isnull(B.t4, 0) as t4, 
		isnull(B.t5, 0) as t5, 
		isnull(B.t6, 0) as t6, 
		isnull(B.t7, 0) as t7, 
		isnull(B.t8, 0) as t8, 
		isnull(B.t9, 0) as t9, 
		isnull(B.t10, 0) as t10, 
		isnull(B.t11, 0) as t11, 
		isnull(B.t12, 0) as t12, 
        isnull(B.t12nt, 0) as t12nt, 
		isnull(B.tong, 0) as tong 
		from (
			select distinct 
			A.code_cl, 
			0 as tieuchi
			from @table A
			union 
			select distinct 
			A.code_cl, 
			1 as tieuchi
			from @table A
            union 
			select distinct 
			A.code_cl, 
			2 as tieuchi
			 from @table A
		) A 
		left join @table B on A.code_cl = B.code_cl and A.tieuchi = b.tieuchi
        where A.code_cl is not null
        ", startdate, enddate);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
        }
        else
        {
            Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay)
    {
        string title = "BÁO CÁO DOANH SỐ TỪNG HỆ HÀNG CẬP NHẬT HÀNG NGÀY - THÁNG";

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 14);
        int row = export.SetHeaderAnco(hssfsheet, title, tungay, denngay, false);
        string nt = tungay.Substring(6);
        int nam = int.Parse(nt) - 1;
        #region Header Column
        int widthDF = 4000;
        List<ItemValue> lstHeader = new List<ItemValue>();
        var item = new ItemValue
        {
            item = "Tiêu chí",
            value = "tieuchi",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Hệ hàng",
            value = "code_cl",
            witdh = 3000
        };
        lstHeader.Add(item);
        
        

        item = new ItemValue
        {
            item = "Jan",
            value = "t1",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Feb",
            value = "t2",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mar",
            value = "t3",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Apr",
            value = "t4",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "May",
            value = "t5",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Jun",
            value = "t6",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Jul",
            value = "t7",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Aug",
            value = "t8",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Sep",
            value = "t9",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Otc",
            value = "t10",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Nov",
            value = "t11",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Dec",
            value = "t12",
            witdh = widthDF
        };
        lstHeader.Add(item);
        
        item = new ItemValue
        {
            item = "" + nam,
            value = "t12nt",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tổng cộng",
            value = "tong",
            witdh = widthDF
        };
        lstHeader.Add(item);

        var alphabet = new PrintAnco2.InfoExcel();
        var ex_fm = new Excel_Format(hssfworkbook);

        IRow rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.HeightInPoints = 40;
        var borderWrap = ex_fm.getcell(12, true, true, "LRBT", "C", "C");
        for (int j = 0; j < lstHeader.Count; j++)
        {
            rowColumnHeader.CreateCell(j).SetCellValue(lstHeader[j].item);
            rowColumnHeader.GetCell(j).CellStyle = borderWrap;
            s1.SetColumnWidth(j, lstHeader[j].witdh);
        }
        row++;
        #endregion

        #region Set Table
        var bordercenterPO = ex_fm.getcell(12, false, true, "LRT", "C", "C");
        var bordercenterINV = ex_fm.getcell(12, false, true, "LR", "C", "C");
        var bordercenterTCCL = ex_fm.getcell(12, false, true, "LRB", "C", "C");
        var borderright2decPO = ex_fm.getcell2(12, false, true, "LRT", "R", "C", "#,##0.00");
        var borderright2decINV = ex_fm.getcell2(12, false, true, "LR", "R", "C", "#,##0.00");
        var borderright2decTCCL = ex_fm.getcell2(12, false, true, "LRB", "R", "C", "#,##0.00");
        var bordercenterBold = ex_fm.getcell(12, true, true, "LRBT", "C", "C");
        var borderrightBold2dec = ex_fm.getcell2(12, true, true, "LRBT", "R", "C", export.fmt0i00);
        List<int> lstsumPO = new List<int>();
        List<int> lstsumINV = new List<int>();
        int start_sum = row + 1;
        string code_cl = "";
        int STT = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;
            string tieuchiPOINV = dt.Rows[i]["tieuchi"].ToString();
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] numberCenter = new string[] { "tieuchi", "code_cl" };
                if (numberCenter.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(dt.Rows[i][lstHeader[j].value].ToString());
                    if (tieuchiPOINV == "PO")
                    {
                        cell.CellStyle = bordercenterPO;
                    }
                    else if(tieuchiPOINV == "INV")
                    {
                        cell.CellStyle = bordercenterINV;
                    }
                    if (tieuchiPOINV == "TC_CL")
                    {
                        cell.CellStyle = bordercenterTCCL;
                        if (lstHeader[j].value == "code_cl")
                            cell.SetCellValue("");
                    }
                }
                else
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    if (tieuchiPOINV == "PO")
                    {
                        cell.CellStyle = borderright2decPO;
                    }
                    else if (tieuchiPOINV == "INV")
                    {
                        cell.CellStyle = borderright2decINV;
                    }
                    else
                    {
                        string cnprev = "";
                        string cn = alphabet.Alphabet[j];
                        if(j > 0)
                            cnprev = alphabet.Alphabet[j - 1];

                        cell.CellStyle = borderright2decTCCL;
                        if (j > 2)
                        {
                            if (lstHeader[j].value == "tong")
                            {
                                cell.SetCellValue("");
                                cell.SetCellType(CellType.Blank);
                            }
                            else
                                cell.SetCellFormula(string.Format(@"{0}{1}-{0}{2}+{3}{4}", cn, row - 1, row, cnprev, row + 1));
                        }
                        else
                        {
                            decimal str3 = decimal.Parse(dt.Rows[i - 2]["t12nt"].ToString());
                            decimal str4 = decimal.Parse(dt.Rows[i - 1]["t12nt"].ToString());
                            cell.SetCellFormula(string.Format(@"{0}{1}-{0}{2}+{3}-{4}", cn, row - 1, row, str3, str4));
                        }
                    }
                }
            }
            row_t.HeightInPoints = height;
            row++;

            if (tieuchiPOINV == "PO")
            {
                lstsumPO.Add(row);
            }
            else if (tieuchiPOINV == "INV")
            {
                lstsumINV.Add(row);
            }
        }
        int end_sum = row;


        string SumStrTTPO = "";
        foreach (int num in lstsumPO)
        {
            SumStrTTPO += "{0}" + num + "+";
        }
        SumStrTTPO += "0";
        IRow row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("Tổng cộng PO");
        row_total.GetCell(0).CellStyle = bordercenterBold;
        export.MergedRegion(hssfsheet, row, 0, 1);
        CellRangeAddress cellRange01 = new CellRangeAddress(row, row, 0, 1);
        export.set_borderThin(cellRange01, "LRTB", hssfsheet);
        row_total.CreateCell(2).SetCellFormula(string.Format(SumStrTTPO, "C"));
        row_total.GetCell(2).CellStyle = borderrightBold2dec;
        row_total.CreateCell(3).SetCellFormula(string.Format(SumStrTTPO, "D"));
        row_total.GetCell(3).CellStyle = borderrightBold2dec;
        row_total.CreateCell(4).SetCellFormula(string.Format(SumStrTTPO, "E"));
        row_total.GetCell(4).CellStyle = borderrightBold2dec;
        row_total.CreateCell(5).SetCellFormula(string.Format(SumStrTTPO, "F"));
        row_total.GetCell(5).CellStyle = borderrightBold2dec;
        row_total.CreateCell(6).SetCellFormula(string.Format(SumStrTTPO, "G"));
        row_total.GetCell(6).CellStyle = borderrightBold2dec;
        row_total.CreateCell(7).SetCellFormula(string.Format(SumStrTTPO, "H"));
        row_total.GetCell(7).CellStyle = borderrightBold2dec;
        row_total.CreateCell(8).SetCellFormula(string.Format(SumStrTTPO, "I"));
        row_total.GetCell(8).CellStyle = borderrightBold2dec;
        row_total.CreateCell(9).SetCellFormula(string.Format(SumStrTTPO, "J"));
        row_total.GetCell(9).CellStyle = borderrightBold2dec;
        row_total.CreateCell(10).SetCellFormula(string.Format(SumStrTTPO, "K"));
        row_total.GetCell(10).CellStyle = borderrightBold2dec;
        row_total.CreateCell(11).SetCellFormula(string.Format(SumStrTTPO, "L"));
        row_total.GetCell(11).CellStyle = borderrightBold2dec;
        row_total.CreateCell(12).SetCellFormula(string.Format(SumStrTTPO, "M"));
        row_total.GetCell(12).CellStyle = borderrightBold2dec;
        row_total.CreateCell(13).SetCellFormula(string.Format(SumStrTTPO, "N"));
        row_total.GetCell(13).CellStyle = borderrightBold2dec;
        row_total.CreateCell(14).SetCellFormula(string.Format(SumStrTTPO, "O"));
        row_total.GetCell(14).CellStyle = borderrightBold2dec;
        row_total.CreateCell(15).SetCellFormula(string.Format(SumStrTTPO, "P"));
        row_total.GetCell(15).CellStyle = borderrightBold2dec;
        row_total.HeightInPoints = 30;
        row++;
        
        string SumStrTTINV = "";
        foreach (int num in lstsumINV)
        {
            SumStrTTINV += "{0}" + num + "+";
        }
        SumStrTTINV += "0";
        row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("Tổng cộng INV");
        row_total.GetCell(0).CellStyle = bordercenterBold;
        export.MergedRegion(hssfsheet, row, 0, 1);
        cellRange01 = new CellRangeAddress(row, row, 0, 1);
        export.set_borderThin(cellRange01, "LRTB", hssfsheet);
        row_total.CreateCell(2).SetCellFormula(string.Format(SumStrTTINV, "C"));
        row_total.GetCell(2).CellStyle = borderrightBold2dec;
        row_total.CreateCell(3).SetCellFormula(string.Format(SumStrTTINV, "D"));
        row_total.GetCell(3).CellStyle = borderrightBold2dec;
        row_total.CreateCell(4).SetCellFormula(string.Format(SumStrTTINV, "E"));
        row_total.GetCell(4).CellStyle = borderrightBold2dec;
        row_total.CreateCell(5).SetCellFormula(string.Format(SumStrTTINV, "F"));
        row_total.GetCell(5).CellStyle = borderrightBold2dec;
        row_total.CreateCell(6).SetCellFormula(string.Format(SumStrTTINV, "G"));
        row_total.GetCell(6).CellStyle = borderrightBold2dec;
        row_total.CreateCell(7).SetCellFormula(string.Format(SumStrTTINV, "H"));
        row_total.GetCell(7).CellStyle = borderrightBold2dec;
        row_total.CreateCell(8).SetCellFormula(string.Format(SumStrTTINV, "I"));
        row_total.GetCell(8).CellStyle = borderrightBold2dec;
        row_total.CreateCell(9).SetCellFormula(string.Format(SumStrTTINV, "J"));
        row_total.GetCell(9).CellStyle = borderrightBold2dec;
        row_total.CreateCell(10).SetCellFormula(string.Format(SumStrTTINV, "K"));
        row_total.GetCell(10).CellStyle = borderrightBold2dec;
        row_total.CreateCell(11).SetCellFormula(string.Format(SumStrTTINV, "L"));
        row_total.GetCell(11).CellStyle = borderrightBold2dec;
        row_total.CreateCell(12).SetCellFormula(string.Format(SumStrTTINV, "M"));
        row_total.GetCell(12).CellStyle = borderrightBold2dec;
        row_total.CreateCell(13).SetCellFormula(string.Format(SumStrTTINV, "N"));
        row_total.GetCell(13).CellStyle = borderrightBold2dec;
        row_total.CreateCell(14).SetCellFormula(string.Format(SumStrTTINV, "O"));
        row_total.GetCell(14).CellStyle = borderrightBold2dec;
        row_total.CreateCell(15).SetCellFormula(string.Format(SumStrTTINV, "P"));
        row_total.GetCell(15).CellStyle = borderrightBold2dec;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        hssfsheet.PrintSetup.Landscape = true;
        String saveAsFileName = String.Format("rpt_DoanhSoHeHangNgayThang-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
