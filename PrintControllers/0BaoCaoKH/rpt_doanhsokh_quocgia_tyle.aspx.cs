using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public partial class ReportWizard_RptKhachHang_rpt_doanhsokh_quocgia_tyle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];
        string loai = Request.QueryString["loai"];
        string whereDTKD = "";
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        if (doitackinhdoanh_id != null)
        {
            if (doitackinhdoanh_id != "NULL")
                whereDTKD = "and pkl.md_doitackinhdoanh_id='" + doitackinhdoanh_id + "'";
        }
        DateTime StartDateVal = DateTime.ParseExact(startdate, "dd/MM/yyyy HH:mm:ss", null);
        int year = StartDateVal.Year - 1;
        string startdatePrev = "01/01/" + year + " 00:00:00";
        string enddatePrev = "31/12/" + year + " 23:59:59";
        DateTime StartDatePrev = DateTime.ParseExact(startdatePrev, "dd/MM/yyyy HH:mm:ss", null);

        string sql = string.Format(@"
            declare @start datetime = convert(datetime,'{0}', 103)
            declare @end datetime = convert(datetime,'{1}', 103)
            declare @startPrev datetime = convert(datetime,'{2}', 103)
            declare @endPrev datetime = convert(datetime,'{3}', 103)
            declare @tblDSDC table (
	            khachhang nvarchar(100),
	            doanhso_dachon decimal(18,2),
	            quocgia nvarchar(100)
            )
            insert into @tblDSDC
            select dtkd.ma_dtkd as khachhang, A.doanhso_dachon, qgia.ten_quocgia as quocgia
            from(
	            select pkl.md_doitackinhdoanh_id,
	            SUM(isnull(pkl.totalgross,0)) as doanhso_dachon
	            from [dbo].[c_packinginvoice] pkl with (nolock)
	            where 
	            pkl.ngay_motokhai between @start and @end
	            and pkl.md_trangthai_id = 'HIEULUC'
	            {4}
                group by pkl.md_doitackinhdoanh_id
            )A
            inner join [dbo].[md_doitackinhdoanh] dtkd on A.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
            inner join [dbo].[md_quocgia] qgia on dtkd.md_quocgia_id = qgia.md_quocgia_id
            -----------------------------
            declare @tblDSNamTRuoc table (
	            khachhang nvarchar(100),
	            doanhso_namtruoc decimal(18,2)
            )
	        insert into @tblDSNamTRuoc
	        select dtkd.ma_dtkd as khachhang, A.doanhso_namtruoc from (
		        select pkl.md_doitackinhdoanh_id,
		        SUM(isnull(pkl.totalgross,0)) as doanhso_namtruoc
		        from [dbo].[c_packinginvoice] pkl with (nolock)
		        where 
		        pkl.ngay_motokhai between @startPrev and @endPrev
		        and pkl.md_trangthai_id = 'HIEULUC'
		        group by pkl.md_doitackinhdoanh_id
	        )A
	        inner join [dbo].[md_doitackinhdoanh] dtkd on A.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id

            select 
            dsdc.khachhang, dsdc.quocgia,
            isnull(dsnt.doanhso_namtruoc, 0) as doanhso_namtruoc, 
            isnull(dsdc.doanhso_dachon, 0) as doanhso_dachon
            from @tblDSDC dsdc
            left join @tblDSNamTRuoc dsnt on dsdc.khachhang = dsnt.khachhang
            order by dsdc.khachhang
        ", startdate, enddate, startdatePrev, enddatePrev, whereDTKD);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
        }
        else
        {
            Response.Write("<center><h1>Không có dữ liệu</h1></center>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 5);
        int row = export.SetHeaderAnco(hssfsheet, "BÁO CÁO DOANH SỐ TỪNG KHÁCH HÀNG", tungay, denngay, true);

        #region Header Column
        int widthDF = 5500;
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
            item = "Quốc gia",
            value = "quocgia",
            witdh = 11000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Doanh số" + Environment.NewLine + "(năm trước)",
            value = "doanhso_namtruoc",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Doanh số",
            value = "doanhso_dachon",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tỉ lệ tăng/giảm so với năm trước (%)",
            value = "tyletanggiam",
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
            row_t.HeightInPoints = 40;
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "doanhso_namtruoc", "doanhso_dachon" };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright2dec;
                }
                else if (lstHeader[j].value == "rowNum")
                {
                    cell.SetCellValue(i + 1);
                    cell.CellStyle = export.bordercenter;
                }
                else if (lstHeader[j].value == "tyletanggiam")
                {
                    cell.SetCellFormula(string.Format("IF(D{0}<>0,(E{0}-D{0})/D{0},0)", row + 1));

                    //cell.SetCellFormula(string.Format("(E{0}-D{0})/D{0}", row + 1));
                    cell.CellStyle = export.borderright2dec;
                }
                else
                {
                    cell.SetCellValue(dt.Rows[i][lstHeader[j].value].ToString());
                    cell.CellStyle = export.border;
                }
            }
            row++;
        }
        int end_sum = row;

        IRow row_total = s1.CreateRow(row);
        export.MergedRegion(hssfsheet, row, 0, 2);
        row_total.CreateCell(0).SetCellValue("Tổng cộng");
        row_total.GetCell(0).CellStyle = export.centerBold;

        row_total.CreateCell(4).SetCellFormula(string.Format("SUM(E{0}:E{1})", start_sum, end_sum));
        row_total.GetCell(4).CellStyle = export.rightBold2dec;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("DoanhSoTungKH-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
