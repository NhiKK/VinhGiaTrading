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

public partial class ReportWizard_RptKhachHang_rpt_DoanhSoTheoCNSP : System.Web.UI.Page
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
                code_cl nvarchar(100),
                chucnangsp nvarchar(250),
                ta nvarchar(MAX),
                nhomcl nvarchar(100),
                doanhso_dachon decimal(18,2)
            )
            insert into @tblDSDC
            select cl.code_cl, sp.chucnangsp, cl.ta_ngan, cl.nhomchungloai,
            Sum(cdinv.thanhtien - cdinv.thanhtien*dh.discount/100) as doanhso_dachon
            from(
                select pkl.c_packinginvoice_id
                from [dbo].[c_packinginvoice] pkl with (nolock)
                where 
                pkl.ngay_motokhai between @start and @end
                and pkl.md_trangthai_id = 'HIEULUC'
            )A
            inner join [dbo].[c_dongpklinv] cdinv on A.c_packinginvoice_id = cdinv.c_packinginvoice_id
            inner join [dbo].[md_sanpham] sp on cdinv.md_sanpham_id = sp.md_sanpham_id
            inner join [dbo].[md_chungloai] cl on sp.md_chungloai_id = cl.md_chungloai_id
            inner join [dbo].[c_donhang] dh on cdinv.c_donhang_id = dh.c_donhang_id
            where sp.chucnangsp is not null
            group by cl.code_cl, sp.chucnangsp, cl.nhomchungloai, cl.ta_ngan
            -----------------------------
            declare @tblDSNamTRuoc table (
                code_cl nvarchar(100),
                chucnangsp nvarchar(250),
                doanhso_namtruoc decimal(18,2)
            )
            insert into @tblDSNamTRuoc
            select cl.code_cl, sp.chucnangsp,
            Sum(cdinv.thanhtien - cdinv.thanhtien*dh.discount/100) as doanhso_namtruoc
            from(
                select pkl.c_packinginvoice_id
                from [dbo].[c_packinginvoice] pkl with (nolock)
                where 
                pkl.ngay_motokhai between @startPrev and @endPrev
                and pkl.md_trangthai_id = 'HIEULUC'
            )A
            inner join [dbo].[c_dongpklinv] cdinv on A.c_packinginvoice_id = cdinv.c_packinginvoice_id
            inner join [dbo].[md_sanpham] sp on cdinv.md_sanpham_id = sp.md_sanpham_id
            inner join [dbo].[md_chungloai] cl on sp.md_chungloai_id = cl.md_chungloai_id
            inner join [dbo].[c_donhang] dh on cdinv.c_donhang_id = dh.c_donhang_id
            where sp.chucnangsp is not null
            group by cl.code_cl, sp.chucnangsp
            -----------------------------
            select 
                dsdc.ta + ' (' + dsdc.code_cl + ')' as code_cl, 
                dsdc.chucnangsp + ':' as code_ncl,
                isnull(dsnt.doanhso_namtruoc, 0) as doanhso_namtruoc, 
                isnull(dsdc.doanhso_dachon, 0) as doanhso_dachon
            from @tblDSDC dsdc
                left join @tblDSNamTRuoc dsnt on dsdc.code_cl = dsnt.code_cl
                left join [dbo].[md_nhomchungloai] ncl on dsdc.nhomcl = ncl.md_nhomchungloai_id
            order by ncl.sapxep, dsdc.code_cl
        ", startdate, enddate, startdatePrev, enddatePrev);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"], StartDateVal.Year, year);
        }
        else
        {
            Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay, int year, int yearPrev)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 2);
        int row = export.SetHeaderAnco(hssfsheet, "BÁO CÁO DOANH SỐ THEO CHỨC NĂNG SẢN PHẨM", tungay, denngay, true);

        #region Header Column
        int widthDF = 9000;
        List<ItemValue> lstHeader = new List<ItemValue>();
        var item = new ItemValue
        {
            item = "NHÀ CUNG ỨNG",
            value = "code_cl",
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
        cellColumnHeader.SetCellValue("Chức năng SP");
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

        for (int he = 0; he < lstHeader.Count; he++)
        {
            s1.SetColumnWidth(he, lstHeader[he].witdh);
        }

        row++;
        rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.HeightInPoints = 20;
        cellColumnHeader = rowColumnHeader.CreateCell(1);
        cellColumnHeader.CellStyle = export.bordercenterBold;
        cellColumnHeader.SetCellValue(yearPrev + " (USD)");
        cellColumnHeader = rowColumnHeader.CreateCell(2);
        cellColumnHeader.CellStyle = export.bordercenterBold;
        cellColumnHeader.SetCellValue(year + " (USD)");
        cellRangeR00 = new CellRangeAddress(row - 1, row, 0, 0);
        s1.AddMergedRegion(cellRangeR00);
        export.set_borderThin(cellRangeR00, "LRTB", hssfsheet);
        row++;
        #endregion

        #region Set Table
        string groupNCL = "";
        int STT = 1, STTGroup = 0;
        int start_sum = row + 1;


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;
            string code_ncl = dt.Rows[i]["code_ncl"].ToString();
            if (groupNCL != code_ncl)
            {
                groupNCL = code_ncl;
                STT = 1;
                STTGroup++;
                var cellFirst = row_t.CreateCell(0);
                cellFirst.SetCellValue(STTGroupFunc(STTGroup) + ". " + groupNCL);
                cellFirst.CellStyle = export.borderleftBold;

                double sumGroup1 = (double)dt.AsEnumerable().Where(x => x.Field<string>("code_ncl") == groupNCL)
                   .Sum(x => x.Field<decimal>("doanhso_namtruoc"));
                cellFirst = row_t.CreateCell(1);
                cellFirst.SetCellValue(sumGroup1);
                cellFirst.CellStyle = export.borderrightBold2dec;

                double sumGroup2 = (double)dt.AsEnumerable().Where(x => x.Field<string>("code_ncl") == groupNCL)
                   .Sum(x => x.Field<decimal>("doanhso_dachon"));
                cellFirst = row_t.CreateCell(2);
                cellFirst.SetCellValue(sumGroup2);
                cellFirst.CellStyle = export.borderrightBold2dec;
                row_t.HeightInPoints = height;
                row++;
                row_t = s1.CreateRow(row);
            }
            else
            {
                STT++;
            }

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
                    string str = STT + ". " + FirstLetterToUpper(dt.Rows[i][lstHeader[j].value].ToString());
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
        row_total.CreateCell(0).SetCellValue("   Tổng cộng:");
        row_total.GetCell(0).CellStyle = export.borderleftBold;

        double sumGroupTT1 = (double)dt.AsEnumerable().Sum(x => x.Field<decimal>("doanhso_namtruoc"));
        row_total.CreateCell(1).SetCellValue(sumGroupTT1);
        row_total.GetCell(1).CellStyle = export.borderrightBold2dec;

        double sumGroupTT2 = (double)dt.AsEnumerable().Sum(x => x.Field<decimal>("doanhso_dachon"));
        row_total.CreateCell(2).SetCellValue(sumGroupTT2);
        row_total.GetCell(2).CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("rpt_DoanhSoTheoCNSP-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }

    public string STTGroupFunc(int count)
    {
        string a = "";
        if (count == 1)
        {
            a = "A";
        }
        else if (count == 2)
        {
            a = "B";
        }
        else if (count == 3)
        {
            a = "C";
        }
        else if (count == 4)
        {
            a = "D";
        }
        else if (count == 5)
        {
            a = "E";
        }
        else if (count == 6)
        {
            a = "F";
        }
        else if (count == 7)
        {
            a = "G";
        }
        else if (count == 8)
        {
            a = "H";
        }
        else if (count == 9)
        {
            a = "I";
        }
        else if (count == 10)
        {
            a = "J";
        }
        else if (count == 11)
        {
            a = "K";
        }
        else if (count == 12)
        {
            a = "L";
        }
        else if (count == 13)
        {
            a = "M";
        }
        else if (count == 14)
        {
            a = "N";
        }
        return a;
    }

    public string FirstLetterToUpper(string str)
    {
        if (str == null)
            return null;

        if (str.Length > 1)
            return char.ToUpper(str[0]) + str.Substring(1);

        return str.ToUpper();
    }
}

