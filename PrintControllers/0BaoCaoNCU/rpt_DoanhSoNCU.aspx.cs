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
        string sql = string.Format(@"
            declare @start datetime = convert(datetime,'{0}', 103)
            declare @end datetime = convert(datetime,'{1}', 103)
            declare @startPrev datetime = convert(datetime,'{2}', 103)
            declare @endPrev datetime = convert(datetime,'{3}', 103)
            declare @tblDSDC table (
                ncu nvarchar(100),
                doanhso_dachon decimal(18,2)
            )
            insert into @tblDSDC
            select dtkd.ma_dtkd as ncu, 
            Sum(cdinv.thanhtien - cdinv.thanhtien*dh.discount/100) as doanhso_dachon
            from(
                select pkl.c_packinginvoice_id
                from [dbo].[c_packinginvoice] pkl with (nolock)
                where 
                pkl.ngay_motokhai between @start and @end
                and pkl.md_trangthai_id = 'HIEULUC'
            )A
            inner join [dbo].[c_dongpklinv] cdinv on A.c_packinginvoice_id = cdinv.c_packinginvoice_id
            inner join [dbo].[md_doitackinhdoanh] dtkd on cdinv.nhacungungid = dtkd.md_doitackinhdoanh_id
            inner join [dbo].[c_donhang] dh on cdinv.c_donhang_id = dh.c_donhang_id
            group by dtkd.ma_dtkd
            -----------------------------
            declare @tblDSNamTRuoc table (
                ncu nvarchar(100),
                doanhso_namtruoc decimal(18,2)
            )
            insert into @tblDSNamTRuoc
            select dtkd.ma_dtkd as ncu, 
            Sum(cdinv.thanhtien - cdinv.thanhtien*dh.discount/100) as doanhso_namtruoc
            from(
                select pkl.c_packinginvoice_id
                from [dbo].[c_packinginvoice] pkl with (nolock)
                where 
                pkl.ngay_motokhai between @startPrev and @endPrev
                and pkl.md_trangthai_id = 'HIEULUC'
            )A
            inner join [dbo].[c_dongpklinv] cdinv on A.c_packinginvoice_id = cdinv.c_packinginvoice_id
            inner join [dbo].[md_doitackinhdoanh] dtkd on cdinv.nhacungungid = dtkd.md_doitackinhdoanh_id
            inner join [dbo].[c_donhang] dh on cdinv.c_donhang_id = dh.c_donhang_id
            group by dtkd.ma_dtkd
            -----------------------------
            select 
            dsdc.ncu,
            isnull(dsnt.doanhso_namtruoc, 0) as doanhso_namtruoc, 
            isnull(dsdc.doanhso_dachon, 0) as doanhso_dachon
            from @tblDSDC dsdc
            left join @tblDSNamTRuoc dsnt on dsdc.ncu = dsnt.ncu
            order by dsdc.ncu
        ", startdate, enddate, startdatePrev, enddatePrev);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"], StartDateVal.Year, year);
			//Response.Write(sql);
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
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 2);
        int row = export.SetHeaderAnco(hssfsheet, "BÁO CÁO DOANH SỐ NHÀ CUNG ỨNG", tungay, denngay, true);

        #region Header Column
        int widthDF = 9000;
        List<ItemValue> lstHeader = new List<ItemValue>();
        var item = new ItemValue
        {
            item = "NHÀ CUNG ỨNG",
            value = "ncu",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Doanh số năm trước",
            value = "doanhso_namtruoc",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Doanh số đã chọn",
            value = "doanhso_dachon",
            witdh = widthDF
        };
        lstHeader.Add(item);


        IRow rowColumnHeader = s1.CreateRow(row);
        var cellColumnHeader = rowColumnHeader.CreateCell(0);
        cellColumnHeader.SetCellValue("NHÀ CUNG ỨNG");
        cellColumnHeader.CellStyle = export.centerBold;
        rowColumnHeader.HeightInPoints = 40;

        cellColumnHeader = rowColumnHeader.CreateCell(1);
        HSSFRichTextString richString = new HSSFRichTextString("DOANH SỐ BÁN - DISCOUNT\n(chưa bao gồm phí trừ, phí cộng)");
        IFont font = hssfworkbook.CreateFont();
        font.FontHeightInPoints = 10;
        font.IsItalic = true;
        font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
        richString.ApplyFont(23, 56, font);
        cellColumnHeader.CellStyle = export.centerBold;
        cellColumnHeader.SetCellValue(richString);
        cellColumnHeader.CellStyle = export.bordercenterBold;
        CellRangeAddress cellRangeR00 = new CellRangeAddress(row, row, 1, 2);
        s1.AddMergedRegion(cellRangeR00);
        export.set_borderThin(cellRangeR00, "LRTB", hssfsheet);
        rowColumnHeader.HeightInPoints = 40;

        for(int he = 0; he < lstHeader.Count; he++) {
            s1.SetColumnWidth(he, lstHeader[he].witdh);
        }

        row++;
        rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.HeightInPoints = 20;
        cellColumnHeader = rowColumnHeader.CreateCell(1);
        cellColumnHeader.CellStyle = export.bordercenterBold;
        cellColumnHeader.SetCellValue(yearPrev);
        cellColumnHeader = rowColumnHeader.CreateCell(2);
        cellColumnHeader.CellStyle = export.bordercenterBold;
        cellColumnHeader.SetCellValue(year);
        cellRangeR00 = new CellRangeAddress(row - 1, row, 0, 0);
        s1.AddMergedRegion(cellRangeR00);
        export.set_borderThin(cellRangeR00, "LRTB", hssfsheet);
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
                string[] arrDecimal = new string[] { "doanhso_namtruoc", "doanhso_dachon" };
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
            }
            row_t.HeightInPoints = height;
            row++;
        }
        int end_sum = row;

        IRow row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("Tổng cộng:");
        row_total.GetCell(0).CellStyle = export.borderleftBold;

        row_total.CreateCell(1).SetCellFormula(string.Format("SUM(B{0}:B{1})", start_sum, end_sum));
        row_total.GetCell(1).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(2).SetCellFormula(string.Format("SUM(C{0}:C{1})", start_sum, end_sum));
        row_total.GetCell(2).CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("rpt_DoanhSoNCU-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
