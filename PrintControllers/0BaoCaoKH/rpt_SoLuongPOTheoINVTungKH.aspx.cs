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

public partial class ReportWizard_RptKhachHang_rpt_SoLuongPOTheoINVTungKH : System.Web.UI.Page
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
            select 
            A.kh, 
            A.ten_quocgia, 
            A.nguoitaoPO, 
            A.nguoilapINV,
            SUM(A.soPOMau) as soPOMau,
            SUM(A.soPOCT) as soPOCT,
            SUM(A.soPOMau + A.soPOCT) as soPOINV
            from
            (
	            select distinct
		            khachhang.ma_dtkd as kh,
		            qgia.ten_quocgia,
		            (select hoten from nhanvien where dh.nguoilap = manv) as nguoitaoPO,
		            (select hoten from nhanvien where civ.nguoitao = manv) as nguoilapINV,
		            (case dh.donhang_mau when 1 then 1 else 0 end) as soPOMau,
		            (case dh.donhang_mau when 1 then 0 else 1 end) as soPOCT,
					dh.c_donhang_id
	            from (
		            select pkl.c_packinginvoice_id, pkl.md_doitackinhdoanh_id, pkl.nguoitao
		            from c_packinginvoice pkl with (nolock)
		            where 
					pkl.ngay_motokhai between @start and @end
					and pkl.md_trangthai_id = 'HIEULUC'
	            )civ
	            inner join c_dongpklinv cdiv on civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
	            inner join md_doitackinhdoanh khachhang on civ.md_doitackinhdoanh_id = khachhang.md_doitackinhdoanh_id
	            left join md_quocgia qgia on khachhang.md_quocgia_id = qgia.md_quocgia_id
	            inner join c_donhang dh on cdiv.c_donhang_id = dh.c_donhang_id
            )A
            group by 
	            A.kh, 
	            A.ten_quocgia, 
	            A.nguoitaoPO, 
	            A.nguoilapINV
            order by A.kh, A.nguoitaoPO, A.nguoilapINV
        ", startdate, enddate);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
        }
        else
        {
            Response.Write("<h3>Không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 7);
        int row = export.SetHeaderAnco(hssfsheet, "BÁO CÁO SỐ LƯỢNG PO THEO INVOICE TỪNG KHÁCH HÀNG", tungay, denngay, true);

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
            item = "MÃ KH",
            value = "kh",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "QUỐC GIA",
            value = "ten_quocgia",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NGƯỜI TẠO PO",
            value = "nguoitaoPO",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NGƯỜI LẬP CHỨNG TỪ",
            value = "nguoilapINV",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số PO" + Environment.NewLine + "mẫu",
            value = "soPOMau",
            witdh = 3600
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số PO"+ Environment.NewLine + "chính thức",
            value = "soPOCT",
            witdh = 3600
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số PO" + Environment.NewLine + "theo INV",
            value = "soPOINV",
            witdh = 3600
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
                string[] arrIntRight = new string[] { "soPOMau", "soPOCT", "soPOINV" };
                if (arrIntRight.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(int.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright;
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
                    cell.CellStyle = export.border;
                }
            }
            row_t.HeightInPoints = height;
            row++;
        }
        int end_sum = row;

        IRow row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("Tổng cộng");
        row_total.GetCell(0).CellStyle = export.borderrightBold;
        CellRangeAddress cellRange04 = new CellRangeAddress(row, row, 0, 4);
        s1.AddMergedRegion(cellRange04);
        export.set_borderThin(cellRange04, "LRTB", hssfsheet);

        row_total.CreateCell(5).SetCellFormula(string.Format("SUM(F{0}:F{1})", start_sum, end_sum));
        row_total.GetCell(5).CellStyle = export.borderrightBold;
        row_total.CreateCell(6).SetCellFormula(string.Format("SUM(G{0}:G{1})", start_sum, end_sum));
        row_total.GetCell(6).CellStyle = export.borderrightBold;
        row_total.CreateCell(7).SetCellFormula(string.Format("SUM(H{0}:H{1})", start_sum, end_sum));
        row_total.GetCell(7).CellStyle = export.borderrightBold;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("SoLuongPOTheoINVTungKH-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
