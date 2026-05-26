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

public partial class ReportWizard_RptKhachHang_rpt_DoanhSoTungHeHangTheoINVThucXuat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string loai = Request.QueryString["trans"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        if (String.IsNullOrEmpty(loai))
        {
            loai = "null";
        }
        else
        {
            loai = string.Format("'{0}'", loai);
        }

        string sql = string.Format(@"
            declare @tungay datetime, @denngay datetime
            declare @table table(
                code_cl nvarchar(100),
                sl int, 
                thanhtien decimal(18,2),
                nhacungungid varchar(32),
                cbm decimal(18,3),
                tungay datetime,
                denngay datetime,
                thang int,
                reporttype varchar(20)
            )
            set @tungay = (select CONVERT(datetime, N'{0}', 103))
            set @denngay = (select CONVERT(datetime, N'{1}', 103))
            
            insert into @table
            Exec [dbo].[rpt_doanhsotheohehang] @tungay, @denngay, {2}

            select A.code_cl, ncu.ma_dtkd as ncu, A.sl, A.cbm, A.thanhtien, A.thang 
            from @table A
            left join md_doitackinhdoanh ncu on A.nhacungungid = ncu.md_doitackinhdoanh_id
            order by A.thang
        ", startdate, enddate, loai);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"], Request.QueryString["trans"]);
        }
        else
        {
            Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay, string title)
    {
        if (title == "IN")
        {
            title = "DOANH SỐ CỘNG TỪNG HỆ HÀNG THEO INVOICE THỰC XUẤT";
        }
        else
        {
            title = "DOANH SỐ CỘNG TỪNG HỆ HÀNG THEO PO THỰC XUẤT";
        }
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 5);
        int row = export.SetHeaderAnco(hssfsheet, title, tungay, denngay, false);

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
            item = "Hệ hàng",
            value = "code_cl",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NCU",
            value = "ncu",
            witdh = 5500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tổng SL",
            value = "sl",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "CBM",
            value = "cbm",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Doanh số",
            value = "thanhtien",
            witdh = 6000
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
        int start_sumThang = start_sum;
        List<int> lstSumTT = new List<int>();
        List<string> lstSumHH = null;
        List<string> lstSumNCC = null;
        List<string> lstSumHHTC = new List<string>();
        List<string> lstSumNCCTC = new List<string>();
        string groupThang = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;

            #region group header Tháng
            string groupThangVal = dt.Rows[i]["thang"].ToString();
            if (groupThang != groupThangVal)
            {
                groupThang = groupThangVal;
                var cell = row_t.CreateCell(0);
                cell.SetCellValue("Tháng:");
                cell.CellStyle = export.celltext;
                //--
                cell = row_t.CreateCell(1);
                cell.SetCellValue(groupThang);
                cell.CellStyle = export.celltext;
                export.MergedRegion(hssfsheet, row, 1, 5);
                row++;
                row_t.HeightInPoints = height;
                row_t = s1.CreateRow(row);
                start_sumThang = row;

                lstSumHH = new List<string>();
                lstSumNCC = new List<string>();
            }
            #endregion

            #region detail table
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "thanhtien" };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright2dec;
                }
                else if (lstHeader[j].value == "sl")
                {
                    cell.SetCellValue(int.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright0dec;
                }
                else if (lstHeader[j].value == "cbm")
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright3dec;
                }
                else if (lstHeader[j].value == "rowNum")
                {
                    cell.SetCellValue(i+1);
                    cell.CellStyle = export.bordercenter;
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
                    cell.CellStyle = export.bordercenter;
                }
            }
			lstSumHH.Add(dt.Rows[i]["code_cl"].ToString());
            lstSumNCC.Add(dt.Rows[i]["ncu"].ToString());
            lstSumHHTC.Add(dt.Rows[i]["code_cl"].ToString());
            lstSumNCCTC.Add(dt.Rows[i]["ncu"].ToString());
            row_t.HeightInPoints = height;
            row++;
            #endregion

            #region group footer Tháng
            string groupThangValFooter = "";
            try { groupThangValFooter = dt.Rows[i + 1]["thang"].ToString(); }
            catch { }

            if (groupThang != groupThangValFooter)
            {
                row_t = s1.CreateRow(row);
                var cell = row_t.CreateCell(0);
                cell.SetCellValue("Cộng:");
                cell.CellStyle = export.centerBold;
                export.MergedRegion(hssfsheet, row, 0, 1);
                CellRangeAddress cellRange01 = new CellRangeAddress(row, row, 0, 1);
                export.set_borderThin(cellRange01, "LRTB", hssfsheet);
                //--
                cell = row_t.CreateCell(2);
                cell.SetCellValue("");
                cell.CellStyle = export.bordercenterBold;
                //--
                cell = row_t.CreateCell(3);
                cell.SetCellFormula(string.Format(@"SUM(D{0}:D{1})", start_sumThang, row));
                cell.CellStyle = export.borderrightBold0dec;
                //--
                cell = row_t.CreateCell(4);
                cell.SetCellValue("");
                cell.CellStyle = export.bordercenterBold;
                //--
                cell = row_t.CreateCell(5);
                cell.SetCellFormula(string.Format(@"SUM(F{0}:F{1})", start_sumThang, row));
                cell.CellStyle = export.borderrightBold2dec;
                row_t.HeightInPoints = 25;
                row++;
                lstSumTT.Add(row);

                int startSumHH = start_sumThang, endSumHH = row - 1;
                foreach (string hehang in lstSumHH.Distinct().OrderBy(s => s)) {
                    row_t = s1.CreateRow(row);
                    cell = row_t.CreateCell(0);
                    cell.SetCellValue(hehang);
                    cell.CellStyle = export.centerBold;
                    export.MergedRegion(hssfsheet, row, 0, 1);
                    cellRange01 = new CellRangeAddress(row, row, 0, 1);
                    export.set_borderThin(cellRange01, "LRTB", hssfsheet);
                    //--
                    cell = row_t.CreateCell(2);
                    cell.SetCellValue("");
                    cell.CellStyle = export.bordercenterBold;
                    //--
                    cell = row_t.CreateCell(3);
                    cell.SetCellFormula(string.Format(@"SUMIF(B{0}:B{1},A{2},D{0}:D{1})", startSumHH, endSumHH, row + 1));
                    cell.CellStyle = export.borderrightBold0dec;
                    //--
                    cell = row_t.CreateCell(4);
                    cell.SetCellValue("");
                    cell.CellStyle = export.bordercenterBold;
                    //--
                    cell = row_t.CreateCell(5);
                    cell.SetCellFormula(string.Format(@"SUMIF(B{0}:B{1},A{2},F{0}:F{1})", startSumHH, endSumHH, row + 1));
                    cell.CellStyle = export.borderrightBold2dec;
                    row_t.HeightInPoints = 25;
					row++;
                }

                foreach (string ncc in lstSumNCC.Distinct().OrderBy(s=>s))
                {
                    row_t = s1.CreateRow(row);
                    cell = row_t.CreateCell(0);
                    cell.SetCellValue(ncc);
                    cell.CellStyle = export.centerBold;
                    export.MergedRegion(hssfsheet, row, 0, 1);
                    cellRange01 = new CellRangeAddress(row, row, 0, 1);
                    export.set_borderThin(cellRange01, "LRTB", hssfsheet);
                    //--
                    cell = row_t.CreateCell(2);
                    cell.SetCellValue("");
                    cell.CellStyle = export.bordercenterBold;
                    //--
                    cell = row_t.CreateCell(3);
                    cell.SetCellFormula(string.Format(@"SUMIF(C{0}:C{1},A{2},D{0}:D{1})", startSumHH, endSumHH, row + 1));
                    cell.CellStyle = export.borderrightBold0dec;
                    //--
                    cell = row_t.CreateCell(4);
                    cell.SetCellValue("");
                    cell.CellStyle = export.bordercenterBold;
                    //--
                    cell = row_t.CreateCell(5);
                    cell.SetCellFormula(string.Format(@"SUMIF(C{0}:C{1},A{2},F{0}:F{1})", startSumHH, endSumHH, row + 1));
                    cell.CellStyle = export.borderrightBold2dec;
                    row_t.HeightInPoints = 25;
                    row++;
                }
            }
            #endregion
        }
        int end_sum = row;

        #region Tong Cong
        string SumStrTT = "";
        foreach (int num in lstSumTT)
        {
            SumStrTT += "{0}" + num + "+";
        }
        SumStrTT += "0";
        IRow row_total = s1.CreateRow(row);
        row++;
        row_total = s1.CreateRow(row);
        var cellTT = row_total.CreateCell(0);
        cellTT.SetCellValue("Tổng cộng:");
        cellTT.CellStyle = export.centerBold;
        export.MergedRegion(hssfsheet, row, 0, 1);
        CellRangeAddress cellRange01TT = new CellRangeAddress(row, row, 0, 1);
        export.set_borderThin(cellRange01TT, "LRTB", hssfsheet);

        cellTT = row_total.CreateCell(2);
        cellTT.SetCellValue("");
        cellTT.CellStyle = export.border;
        //--
        cellTT = row_total.CreateCell(3);
        cellTT.SetCellFormula(string.Format(SumStrTT, "D"));
        cellTT.CellStyle = export.borderrightBold0dec;
        //--
        cellTT = row_total.CreateCell(4);
        cellTT.SetCellValue("");
        cellTT.CellStyle = export.bordercenterBold;
        //--
        cellTT = row_total.CreateCell(5);
        cellTT.SetCellFormula(string.Format(SumStrTT, "F"));
        cellTT.CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;

        int startSumHHTC = start_sum, endSumHHTC = end_sum;

        row++;
        foreach (string hehang in lstSumHHTC.Distinct().OrderBy(s => s))
        {
            row_total = s1.CreateRow(row);
            cellTT = row_total.CreateCell(0);
            cellTT.SetCellValue(hehang);
            cellTT.CellStyle = export.centerBold;
            export.MergedRegion(hssfsheet, row, 0, 1);
            cellRange01TT = new CellRangeAddress(row, row, 0, 1);
            export.set_borderThin(cellRange01TT, "LRTB", hssfsheet);
            //--
            cellTT = row_total.CreateCell(2);
            cellTT.SetCellValue("");
            cellTT.CellStyle = export.bordercenterBold;
            //--
            cellTT = row_total.CreateCell(3);
            cellTT.SetCellFormula(string.Format(@"SUMIF(B{0}:B{1},A{2},D{0}:D{1})", startSumHHTC, endSumHHTC, row + 1));
            cellTT.CellStyle = export.borderrightBold0dec;
            //--
            cellTT = row_total.CreateCell(4);
            cellTT.SetCellValue("");
            cellTT.CellStyle = export.bordercenterBold;
            //--
            cellTT = row_total.CreateCell(5);
            cellTT.SetCellFormula(string.Format(@"SUMIF(B{0}:B{1},A{2},F{0}:F{1})", startSumHHTC, endSumHHTC, row + 1));
            cellTT.CellStyle = export.borderrightBold2dec;
            row_total.HeightInPoints = 25;
            row++;
        }

        foreach (string ncc in lstSumNCCTC.Distinct().OrderBy(s => s))
        {
            row_total = s1.CreateRow(row);
            cellTT = row_total.CreateCell(0);
            cellTT.SetCellValue(ncc);
            cellTT.CellStyle = export.centerBold;
            export.MergedRegion(hssfsheet, row, 0, 1);
            cellRange01TT = new CellRangeAddress(row, row, 0, 1);
            export.set_borderThin(cellRange01TT, "LRTB", hssfsheet);
            //--
            cellTT = row_total.CreateCell(2);
            cellTT.SetCellValue("");
            cellTT.CellStyle = export.bordercenterBold;
            //--
            cellTT = row_total.CreateCell(3);
            cellTT.SetCellFormula(string.Format(@"SUMIF(C{0}:C{1},A{2},D{0}:D{1})", startSumHHTC, endSumHHTC, row + 1));
            cellTT.CellStyle = export.borderrightBold0dec;
            //--
            cellTT = row_total.CreateCell(4);
            cellTT.SetCellValue("");
            cellTT.CellStyle = export.bordercenterBold;
            //--
            cellTT = row_total.CreateCell(5);
            cellTT.SetCellFormula(string.Format(@"SUMIF(C{0}:C{1},A{2},F{0}:F{1})", startSumHHTC, endSumHHTC, row + 1));
            cellTT.CellStyle = export.borderrightBold2dec;
            row_total.HeightInPoints = 25;
            row++;
        }
        #endregion

        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("DoanhSoTungHHThucXuat-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
