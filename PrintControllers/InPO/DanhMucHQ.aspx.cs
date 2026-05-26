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

public partial class PrintControllers_DanhMucHQ_Default : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();
    private String fmt0 = "#,##0", fmt0i0 = "#,##0.0", fmt0i00 = "#,##0.00", fmt0i000 = "#,##0.000";

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        {
            String c_donhang_id = Request.QueryString["c_donhang_id"];

            String select = String.Format(@"
			select 
			dh.sochungtu
			, sp.ma_sanpham
			, sp.mota_tiengvietVG
			, hs.hscode
			, ddh.soluong
			, (case dvtsp.ma_edi when N'set' then N'bộ' when N'pc' then N'cái' else dvtsp.ma_edi end) as ma_edi
			, ddh.soluong_daxuat
			, ddh.giafob
			from c_dongdonhang ddh
			left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id
			left join md_sanpham sp on sp.md_sanpham_id = ddh.md_sanpham_id
			left join md_hscode hs on hs.md_hscode_id = sp.md_hscode_id
			left join md_donvitinhsanpham dvtsp on dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
			where N'{0}' like  N'%'+dh.c_donhang_id+'%'
			order by dh.sochungtu asc, sp.ma_sanpham asc
			", c_donhang_id == null ? "" : c_donhang_id);

            DataTable dt = mdbc.GetData(select);

            if (dt.Rows.Count != 0)
            {
                HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
                String saveAsFileName = String.Format("DanhMucKhaiHaiQuan-{0}.xls", DateTime.Now);
                this.SaveFile(hssfworkbook, saveAsFileName);
            }
            else
            {
                Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
            }
        }
        //catch (Exception ex)
        {
            //Response.Write(String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex.Message));
        }
    }

    public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");

        //Font size
        IFont fontBold = hssfworkbook.CreateFont();
        fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
        fontBold.FontHeightInPoints = 12;

        IFont font12 = hssfworkbook.CreateFont();
        font12.FontHeightInPoints = 12;

        IFont font22Bold = hssfworkbook.CreateFont();
        font22Bold.FontHeightInPoints = 22;
        font22Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        //Cell style
        //- header
        ICellStyle cellBold = hssfworkbook.CreateCellStyle();
        cellBold.SetFont(fontBold);
        cellBold.WrapText = true;
        cellBold.Alignment = HorizontalAlignment.Left;
        cellBold.VerticalAlignment = VerticalAlignment.Top;
        //-- 
        ICellStyle cellHeader = hssfworkbook.CreateCellStyle();
        cellHeader.SetFont(font22Bold);
        cellHeader.WrapText = true;
        cellHeader.Alignment = HorizontalAlignment.Center;
        cellHeader.VerticalAlignment = VerticalAlignment.Top;
        //--
        ICellStyle celltext = hssfworkbook.CreateCellStyle();
        celltext.SetFont(font12);
        celltext.WrapText = true;
        celltext.Alignment = HorizontalAlignment.Left;
        celltext.VerticalAlignment = VerticalAlignment.Top;

        // - table style
        HSSFUtils.CellStyleModel cellStyleModel = new HSSFUtils.CellStyleModel(hssfworkbook);
        cellStyleModel.BorderAll = true;
        // 
        ICellStyle right = cellStyleModel.CreateCellStyle();
        right.VerticalAlignment = VerticalAlignment.Top;
        right.Alignment = HorizontalAlignment.Right;

        ICellStyle left = cellStyleModel.CreateCellStyle();
        left.VerticalAlignment = VerticalAlignment.Top;
        left.Alignment = HorizontalAlignment.Left;

        //-- table
        ICellStyle border = hssfworkbook.CreateCellStyle();
        border.BorderBottom = border.BorderLeft
            = border.BorderRight
            = border.BorderTop
            = NPOI.SS.UserModel.BorderStyle.Thin;
        border.WrapText = true;
        border.VerticalAlignment = VerticalAlignment.Top;
        border.Alignment = HorizontalAlignment.Left;

        //--- Header
        ICellStyle borderWrap = hssfworkbook.CreateCellStyle();
        borderWrap.SetFont(fontBold);
        borderWrap.BorderBottom = borderWrap.BorderLeft
            = borderWrap.BorderRight
            = borderWrap.BorderTop
            = NPOI.SS.UserModel.BorderStyle.Thin;
        borderWrap.WrapText = true;
        borderWrap.VerticalAlignment = VerticalAlignment.Top;
        borderWrap.Alignment = HorizontalAlignment.Center;

        //--- Chung tu
        ICellStyle borderCT = hssfworkbook.CreateCellStyle();
        borderCT.SetFont(fontBold);
        borderCT.BorderBottom = borderCT.BorderLeft
            = borderCT.BorderRight
            = borderCT.BorderTop
            = NPOI.SS.UserModel.BorderStyle.Thin;
        borderCT.WrapText = true;
        borderCT.VerticalAlignment = VerticalAlignment.Top;
        borderCT.Alignment = HorizontalAlignment.Left;
        borderCT.SetFont(font12);
        //
        int heigh = 22;
        int row = 0;
        //set A1 - A3
        string[] a = { "DANH MỤC KHAI HẢI QUAN" };
        for (int i = 0; i < a.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(0).SetCellValue(a[i]);
            s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 7));
            s1.GetRow(row).HeightInPoints = heigh;
            if (i <= 0)
            {
                s1.GetRow(row).HeightInPoints = 45;
                s1.GetRow(row).GetCell(0).CellStyle = cellHeader;
            }
            else
            {
                s1.GetRow(row).GetCell(0).CellStyle = celltext;
            }
            row++;
        }
        //

        // set A13 - All
        // -- Header
        int cell = 0;
        string[] d = { "Mã", "Diễn giải", "Mã HS 8 số", "xuất xứ", "Số lượng", "Đơn vị tính", "Đơn giá", "Thành tiền" };
        IRow rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.HeightInPoints = 47;
        for (int i = 0; i < d.Count(); i++)
        {
            int with = 4500;
            rowColumnHeader.CreateCell(cell).SetCellValue(d[i]);
            rowColumnHeader.GetCell(i).CellStyle = borderWrap;
            if (i == 1)
                with = 12450;
            s1.SetColumnWidth(cell, with);
            cell++;
        }
        // -- Details
        string sochungtu = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["sochungtu"].ToString() != sochungtu)
            {
                row++;
                s1.CreateRow(row).CreateCell(0).SetCellValue("P/I No. " + dt.Rows[i]["sochungtu"].ToString());
                s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 7));
                s1.GetRow(row).GetCell(0).CellStyle = borderCT;
                s1.GetRow(row).HeightInPoints = 22;
                sochungtu = dt.Rows[i]["sochungtu"].ToString();
            }
            row++;
            string[] e_value = { "ma_sanpham", "mota_tiengvietVG", "hscode", "", "soluong", "ma_edi", "giafob", "" };
            IRow row_t = s1.CreateRow(row);
            row_t.HeightInPoints = 60;

            int cell_t = 0;
            for (int j = 0; j < e_value.Count(); j++)
            {
                if (e_value[j] != "")
                    row_t.CreateCell(cell_t).SetCellValue(dt.Rows[i][e_value[j]].ToString());
                else if (j == 3)
                    row_t.CreateCell(cell_t).SetCellValue("VIETNAM");
                else if (j == 7)
                    row_t.CreateCell(cell_t).CellFormula = String.Format("E{0} * G{0}", row + 1);
                else
                    row_t.CreateCell(cell_t).SetCellValue("");

                s1.GetRow(row).GetCell(cell_t).CellStyle = celltext;

                cell_t++;

                try
                {
                    for (int n = 0; n <= cell_t; n++)
                    {
                        //  table style
                        row_t.GetCell(n).CellStyle.WrapText = true;
                        row_t.GetCell(n).CellStyle = border;
                    }
                }
                catch { }
            }

            row_t.GetCell(4).CellStyle = right;
            row_t.GetCell(4).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");

            row_t.GetCell(6).CellStyle = right;
            row_t.GetCell(6).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");

            row_t.GetCell(7).CellStyle = right;
            row_t.GetCell(7).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
        }

        #region Format Print Excel

        s1.PrintSetup.PaperSize = (short)PaperSize.A4;
        s1.FitToPage = true;
        s1.PrintSetup.FitWidth = 1;
        s1.PrintSetup.FitHeight = 0;
        s1.SetMargin(MarginType.TopMargin, 0.75);
        s1.SetMargin(MarginType.BottomMargin, 0.75);
        s1.SetMargin(MarginType.LeftMargin, 0.23);
        s1.SetMargin(MarginType.RightMargin, 0.23);
        s1.SetMargin(MarginType.HeaderMargin, 0.31);
        s1.SetMargin(MarginType.FooterMargin, 0.31);
        hssfworkbook.SetPrintArea(
            hssfworkbook.GetSheetIndex(s1.SheetName), //sheet index
            0, //start column
            7, //end column
            0, //start row
            row + 1
        );
        #endregion
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

    public IFont CreateFontSize(HSSFWorkbook hsswb, short size)
    {
        IFont font = hsswb.CreateFont();
        font.FontHeightInPoints = size;
        return font;
    }

    public ICellStyle GetStyleNumber(HSSFWorkbook workbook, String format)
    {
        ICellStyle s = workbook.CreateCellStyle();
        s.Alignment = HorizontalAlignment.Right;
        s.VerticalAlignment = VerticalAlignment.Top;
        s.BorderLeft = s.BorderRight = s.BorderBottom = s.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        s.DataFormat = CellDataFormat.GetDataFormat(workbook, format);
        return s;
    }
}
