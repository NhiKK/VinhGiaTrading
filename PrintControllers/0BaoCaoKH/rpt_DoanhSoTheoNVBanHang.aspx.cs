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

public partial class ReportWizard_RptKhachHang_rpt_XuatHangTheoPO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        string sql = string.Format(@"
            declare @start datetime = convert(datetime,'{0}', 103)
            declare @end datetime = convert(datetime,'{1}', 103)
            SET ARITHABORT ON
            exec rpt_doanhsotheonhanvieninv @start, @end, null
            SET ARITHABORT OFF
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
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 15);
        int row = export.SetHeaderAnco(hssfsheet, "BÁO CÁO DOANH SỐ THEO NHÂN VIÊN BÁN HÀNG (THEO THÁNG - NĂM)", tungay, denngay, true);

        #region Header Column
        int widthDF = 5000;
        List<ItemValue> lstHeader = new List<ItemValue>();
        var item = new ItemValue
        {
            item = "STT",
            value = "",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Nhân viên",
            value = "",
            witdh = 4500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Hạn mục",
            value = "chitieu",
            witdh = 6500
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
            item = "Tổng cộng",
            value = "tongcong",
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
        string groupNV = "";
        int countNV = 0;
        List<int> lstsumDSTheoPO = new List<int>();
        List<int> lstsumSLPOHieuLuc = new List<int>();
        List<int> lstsumDSTheoINV = new List<int>();
        List<int> lstsumPOTheoINV = new List<int>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;

            string groupNVVal = dt.Rows[i]["manv"].ToString();
            if (groupNV != groupNVVal)
            {
                groupNV = groupNVVal;
                countNV++;
                var cellFirst = row_t.CreateCell(0);
                cellFirst.SetCellValue(countNV);
                cellFirst.CellStyle = export.bordercenter;
                cellFirst = row_t.CreateCell(1);
                cellFirst.SetCellValue(dt.Rows[i]["manv"].ToString());
                cellFirst.CellStyle = export.borderleft;
                CellRangeAddress cellRange115 = new CellRangeAddress(row, row, 1, 15);
                s1.AddMergedRegion(cellRange115);
                export.set_borderThin(cellRange115, "LRTB", hssfsheet);
                row_t.HeightInPoints = height;
                row++;
                row_t = s1.CreateRow(row);
            }

            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "t1", "t2", "t3", "t4", "t5", "t6", "t7", "t8", "t9", "t10", "t11", "t12", "tongcong" };
                if (arrDecimal.Contains(lstHeader[j].value))
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
                else
                {
                    cell.SetCellValue("");
                    cell.CellStyle = export.border;
                }
            }
            row_t.HeightInPoints = height;
            row++;

            string chitieu = dt.Rows[i]["chitieu"].ToString();
            if (chitieu == "Doanh số theo P/O")
            {
                lstsumDSTheoPO.Add(row);
            }
            else if (chitieu == "Số lượng P/O hiệu lực")
            {
                lstsumSLPOHieuLuc.Add(row);
            }
            else if (chitieu == "Doanh số theo INV")
            {
                lstsumDSTheoINV.Add(row);
            }
            else if (chitieu == "P/O theo INV")
            {
                lstsumPOTheoINV.Add(row);
            }
        }
        int end_sum = row;
        #endregion

        #region line
        IRow row_total = s1.CreateRow(row);
        var cellFirstTT = row_total.CreateCell(0);
        cellFirstTT.SetCellValue("");
        cellFirstTT.CellStyle = export.bordercenter;
        cellFirstTT = row_total.CreateCell(1);
        cellFirstTT.SetCellValue("Tổng cộng");
        cellFirstTT.CellStyle = export.borderleftBold;
        CellRangeAddress cellRange115TT = new CellRangeAddress(row, row, 1, 15);
        s1.AddMergedRegion(cellRange115TT);
        export.set_borderThin(cellRange115TT, "LRTB", hssfsheet);
        row_total.HeightInPoints = 25;
        row++;
        row_total = s1.CreateRow(row);
        #endregion

        #region Tổng cộng Doanh số theo P/O
        string SumStrDSTheoPO = "";
        foreach (int num in lstsumDSTheoPO)
        {
            SumStrDSTheoPO += "{0}" + num + "+";
        }
        SumStrDSTheoPO += "0";
        row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("");
        var cellRange01 = new CellRangeAddress(row, row, 0, 1);
        s1.AddMergedRegion(cellRange01);
        export.set_borderThin(cellRange01, "LRTB", hssfsheet);
        row_total.CreateCell(2).SetCellValue("Doanh số theo P/O");
        row_total.GetCell(2).CellStyle = export.borderleftBold;
        row_total.CreateCell(3).SetCellFormula(string.Format(SumStrDSTheoPO, "D"));
        row_total.GetCell(3).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(4).SetCellFormula(string.Format(SumStrDSTheoPO, "E"));
        row_total.GetCell(4).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(5).SetCellFormula(string.Format(SumStrDSTheoPO, "F"));
        row_total.GetCell(5).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(6).SetCellFormula(string.Format(SumStrDSTheoPO, "G"));
        row_total.GetCell(6).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(7).SetCellFormula(string.Format(SumStrDSTheoPO, "H"));
        row_total.GetCell(7).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(8).SetCellFormula(string.Format(SumStrDSTheoPO, "I"));
        row_total.GetCell(8).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(9).SetCellFormula(string.Format(SumStrDSTheoPO, "J"));
        row_total.GetCell(9).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(10).SetCellFormula(string.Format(SumStrDSTheoPO, "K"));
        row_total.GetCell(10).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(11).SetCellFormula(string.Format(SumStrDSTheoPO, "L"));
        row_total.GetCell(11).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(12).SetCellFormula(string.Format(SumStrDSTheoPO, "M"));
        row_total.GetCell(12).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(13).SetCellFormula(string.Format(SumStrDSTheoPO, "N"));
        row_total.GetCell(13).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(14).SetCellFormula(string.Format(SumStrDSTheoPO, "O"));
        row_total.GetCell(14).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(15).SetCellFormula(string.Format(SumStrDSTheoPO, "P"));
        row_total.GetCell(15).CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        row++;
        #endregion

        #region Tổng cộng Số lượng P/O hiệu lực
        string SumStrSLPOHieuLuc = "";
        foreach (int num in lstsumSLPOHieuLuc)
        {
            SumStrSLPOHieuLuc += "{0}" + num + "+";
        }
        SumStrSLPOHieuLuc += "0";
        row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("");
        cellRange01 = new CellRangeAddress(row, row, 0, 1);
        s1.AddMergedRegion(cellRange01);
        export.set_borderThin(cellRange01, "LRTB", hssfsheet);
        row_total.CreateCell(2).SetCellValue("Số lượng P/O hiệu lực");
        row_total.GetCell(2).CellStyle = export.borderleftBold;
        row_total.CreateCell(3).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "D"));
        row_total.GetCell(3).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(4).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "E"));
        row_total.GetCell(4).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(5).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "F"));
        row_total.GetCell(5).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(6).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "G"));
        row_total.GetCell(6).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(7).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "H"));
        row_total.GetCell(7).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(8).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "I"));
        row_total.GetCell(8).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(9).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "J"));
        row_total.GetCell(9).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(10).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "K"));
        row_total.GetCell(10).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(11).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "L"));
        row_total.GetCell(11).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(12).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "M"));
        row_total.GetCell(12).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(13).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "N"));
        row_total.GetCell(13).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(14).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "O"));
        row_total.GetCell(14).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(15).SetCellFormula(string.Format(SumStrSLPOHieuLuc, "P"));
        row_total.GetCell(15).CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        row++;
        #endregion

        #region Tổng cộng Doanh số theo INV
        string SumStrDSTheoINV = "";
        foreach (int num in lstsumDSTheoINV)
        {
            SumStrDSTheoINV += "{0}" + num + "+";
        }
        SumStrDSTheoINV += "0";
        row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("");
        cellRange01 = new CellRangeAddress(row, row, 0, 1);
        s1.AddMergedRegion(cellRange01);
        export.set_borderThin(cellRange01, "LRTB", hssfsheet);
        row_total.CreateCell(2).SetCellValue("Doanh số theo INV");
        row_total.GetCell(2).CellStyle = export.borderleftBold;
        row_total.CreateCell(3).SetCellFormula(string.Format(SumStrDSTheoINV, "D"));
        row_total.GetCell(3).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(4).SetCellFormula(string.Format(SumStrDSTheoINV, "E"));
        row_total.GetCell(4).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(5).SetCellFormula(string.Format(SumStrDSTheoINV, "F"));
        row_total.GetCell(5).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(6).SetCellFormula(string.Format(SumStrDSTheoINV, "G"));
        row_total.GetCell(6).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(7).SetCellFormula(string.Format(SumStrDSTheoINV, "H"));
        row_total.GetCell(7).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(8).SetCellFormula(string.Format(SumStrDSTheoINV, "I"));
        row_total.GetCell(8).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(9).SetCellFormula(string.Format(SumStrDSTheoINV, "J"));
        row_total.GetCell(9).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(10).SetCellFormula(string.Format(SumStrDSTheoINV, "K"));
        row_total.GetCell(10).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(11).SetCellFormula(string.Format(SumStrDSTheoINV, "L"));
        row_total.GetCell(11).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(12).SetCellFormula(string.Format(SumStrDSTheoINV, "M"));
        row_total.GetCell(12).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(13).SetCellFormula(string.Format(SumStrDSTheoINV, "N"));
        row_total.GetCell(13).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(14).SetCellFormula(string.Format(SumStrDSTheoINV, "O"));
        row_total.GetCell(14).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(15).SetCellFormula(string.Format(SumStrDSTheoINV, "P"));
        row_total.GetCell(15).CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        row++;
        #endregion

        #region Tổng cộng P/O theo INV
        string SumStrPOTheoINV = "";
        foreach (int num in lstsumPOTheoINV)
        {
            SumStrPOTheoINV += "{0}" + num + "+";
        }
        SumStrPOTheoINV += "0";
        row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("");
        cellRange01 = new CellRangeAddress(row, row, 0, 1);
        s1.AddMergedRegion(cellRange01);
        export.set_borderThin(cellRange01, "LRTB", hssfsheet);
        row_total.CreateCell(2).SetCellValue("P/O theo INV");
        row_total.GetCell(2).CellStyle = export.borderleftBold;
        row_total.CreateCell(3).SetCellFormula(string.Format(SumStrPOTheoINV, "D"));
        row_total.GetCell(3).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(4).SetCellFormula(string.Format(SumStrPOTheoINV, "E"));
        row_total.GetCell(4).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(5).SetCellFormula(string.Format(SumStrPOTheoINV, "F"));
        row_total.GetCell(5).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(6).SetCellFormula(string.Format(SumStrPOTheoINV, "G"));
        row_total.GetCell(6).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(7).SetCellFormula(string.Format(SumStrPOTheoINV, "H"));
        row_total.GetCell(7).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(8).SetCellFormula(string.Format(SumStrPOTheoINV, "I"));
        row_total.GetCell(8).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(9).SetCellFormula(string.Format(SumStrPOTheoINV, "J"));
        row_total.GetCell(9).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(10).SetCellFormula(string.Format(SumStrPOTheoINV, "K"));
        row_total.GetCell(10).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(11).SetCellFormula(string.Format(SumStrPOTheoINV, "L"));
        row_total.GetCell(11).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(12).SetCellFormula(string.Format(SumStrPOTheoINV, "M"));
        row_total.GetCell(12).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(13).SetCellFormula(string.Format(SumStrPOTheoINV, "N"));
        row_total.GetCell(13).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(14).SetCellFormula(string.Format(SumStrPOTheoINV, "O"));
        row_total.GetCell(14).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(15).SetCellFormula(string.Format(SumStrPOTheoINV, "P"));
        row_total.GetCell(15).CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        row++;
        #endregion
        
        hssfsheet = export.PrintExcel(hssfsheet, row);
        hssfsheet.PrintSetup.Landscape = true;
        String saveAsFileName = String.Format("DoanhSoTheoNV-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
