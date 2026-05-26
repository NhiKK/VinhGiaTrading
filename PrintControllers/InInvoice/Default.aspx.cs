using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;

public partial class PrintControllers_InInvoice_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String c_pkivn_id = Request.QueryString["c_packinglist_id"];
            String select = String.Format(@"select cpk.so_pkl, cpk.etd, dt.ten_dtkd, dt.diachi, dt.tel, dt.fax,
		                                            cpk.mv, cb.ten_cangbien, cpk.noiden, cpk.blno, cpk.commodity, diengiaicong_po, giatricong_po, diengiaitru_po, giatritru_po, cpk.ngaytao
                                            from c_packinginvoice cpk, md_doitackinhdoanh dt, md_cangbien cb
                                            where cpk.noidi = cb.md_cangbien_id
	                                            and cpk.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
	                                            and cpk.c_packinginvoice_id ='" + c_pkivn_id + "'"); //c_pkivn_id

            DataTable dt = mdbc.GetData(select);

            String selNganHang = String.Format(@"select 
                                        distinct thongtin 
                                   from 
                                        md_nganhang ngh, c_donhang dh, c_dongpklinv dpkl
                                   where
                                        ngh.md_nganhang_id = dh.md_nganhang_id
                                        AND dpkl.c_donhang_id = dh.c_donhang_id
                                        AND dpkl.c_packinginvoice_id = N'{0}'", c_pkivn_id);
            String nganhang = (String)mdbc.ExecuteScalar(selNganHang);

            String selPayment = String.Format(@"select 
                                        distinct ten_paymentterm 
                                   from 
                                        md_paymentterm pmt, c_donhang dh, c_dongpklinv dpkl
                                   where
                                        pmt.md_paymentterm_id = dh.md_paymentterm_id
                                        AND dpkl.c_donhang_id = dh.c_donhang_id
                                        AND dpkl.c_packinginvoice_id = N'{0}'", c_pkivn_id);

            String paymentterm = (String)mdbc.ExecuteScalar(selPayment);

            String selDonHang = String.Format(@"select 
                                        distinct dh.sochungtu 
                                   from 
                                        c_donhang dh, c_dongpklinv dpkl
                                   where
                                        dpkl.c_donhang_id = dh.c_donhang_id
                                        AND dpkl.c_packinginvoice_id = N'{0}'", c_pkivn_id);
            DataTable dtChungTuDonHang = mdbc.GetData(selDonHang);
            String chungTuDonHang = "";

            foreach (DataRow item in dtChungTuDonHang.Rows)
            {
                chungTuDonHang += String.Format(", {0}", item[0].ToString());
            }

            chungTuDonHang = chungTuDonHang.Substring(2);

            if (dt.Rows.Count != 0)
            {
                HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt, paymentterm, nganhang, chungTuDonHang);
                String saveAsFileName = String.Format("Invoice-{0}.xls", DateTime.Now);
                this.SaveFile(hssfworkbook, saveAsFileName);
            }
            else
            {
                Response.Write("<h3>PackingList/Invoice không có dữ liệu</h3>");
            }
        }
        catch (Exception ex)
        {
            Response.Write(String.Format("{0}", ex.Message));
        }
    }

    public HSSFWorkbook CreateWorkBookPO(DataTable dt, String paymentterm, String nganhang, String chungtudonhang)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        String c_pkivn_id = Request.QueryString["c_packinglist_id"];
        var pdkInvoice = db.c_packinginvoices.Single(inv => inv.c_packinginvoice_id.Equals(c_pkivn_id));
        String noidungNCC1 = "", noidungNCC2 = "", noidungNCC3 = "", noidungNCC4 = "", noidungNCC5 = "",
               noidungNCC6 = "", noidungNCC7 = "", noidungNCC8 = "", noidungNCC9 = "", link = "";
        if (pdkInvoice.hangcua == "VG")
        {
            noidungNCC1 = "VINH GIA COMPANY LIMITED";
            noidungNCC2 = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            noidungNCC3 = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            noidungNCC4 = "VINH GIA COMPANY LIMITED";
            noidungNCC5 = "046.137.3860862";

            noidungNCC6 = "JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIET NAM - TAN BINH DUONG BRANCH,";
            noidungNCC7 = "NO 16, LE TRONG TAN STREET, BINH DUONG 2 QUARTER, DI AN WARD, HO CHI MINH CITY, VIET NAM";
            noidungNCC8 = "Tel: (84 - 274) 3792 158 - Fax: (84 - 274) 3793 970,";
            noidungNCC9 = "Swift code: BFTVVNVX";

            link = ExcuteSignalRStatic.mapPathSignalR("~/images/more/ckcdct.jpg");
        }
        else
        {
            noidungNCC1 = "HOANG HUY HUNG COMPANY LIMITED";
            noidungNCC2 = "Tam Hiep 2 Urban residential area,  Nui Thanh Commune, Danang City, Viet Nam";
            noidungNCC3 = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            noidungNCC4 = "HOANG HUY HUNG COMPANY LIMITED";
            noidungNCC5 = "1021045695";

            noidungNCC6 = "JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIETNAM";
            noidungNCC7 = "35 TRAN HUNG DAO STREET, BAN THACH WARD, DA NANG CITY, VIET NAM";
            noidungNCC8 = "";
            noidungNCC9 = "Swift code: BFTVVNVX065";

            link = ExcuteSignalRStatic.mapPathSignalR("~/images/more/hhh.jpg");
        }

        var lstPO = new List<int>();
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("INV");

        IFont font12 = hssfworkbook.CreateFont();
        font12.FontHeightInPoints = 12;

        IFont font12Bold = hssfworkbook.CreateFont();
        font12Bold.FontHeightInPoints = 12;
        font12Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font16Bold = hssfworkbook.CreateFont();
        font16Bold.FontHeightInPoints = 16;
        font16Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font12BoldItalic = hssfworkbook.CreateFont();
        font12BoldItalic.FontHeightInPoints = 12;
        font12BoldItalic.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
        font12BoldItalic.IsItalic = true;

        IFont font22Bold = hssfworkbook.CreateFont();
        font22Bold.FontHeightInPoints = 22;
        font22Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font14Bold = hssfworkbook.CreateFont();
        font14Bold.FontHeightInPoints = 14;
        font14Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont font18Bold = hssfworkbook.CreateFont();
        font18Bold.FontHeightInPoints = 18;
        font18Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        // Cell Style 
        ICellStyle styleCenter18Bold = hssfworkbook.CreateCellStyle();
        styleCenter18Bold.SetFont(font18Bold);
        styleCenter18Bold.VerticalAlignment = VerticalAlignment.Top;
        styleCenter18Bold.Alignment = HorizontalAlignment.Center;
        styleCenter18Bold.WrapText = true;

        ICellStyle styleTop14Bold = hssfworkbook.CreateCellStyle();
        styleTop14Bold.SetFont(font14Bold);
        styleTop14Bold.VerticalAlignment = VerticalAlignment.Top;
        styleTop14Bold.WrapText = true;

        ICellStyle styleCenter22Bold = hssfworkbook.CreateCellStyle();
        styleCenter22Bold.SetFont(font22Bold);
        styleCenter22Bold.VerticalAlignment = VerticalAlignment.Top;
        styleCenter22Bold.Alignment = HorizontalAlignment.Center;
        styleCenter22Bold.WrapText = true;

        ICellStyle styleCenter12Bold = hssfworkbook.CreateCellStyle();
        styleCenter12Bold.SetFont(font12Bold);
        styleCenter12Bold.VerticalAlignment = VerticalAlignment.Top;
        styleCenter12Bold.Alignment = HorizontalAlignment.Center;
        styleCenter12Bold.WrapText = true;

        ICellStyle styleCenter16Bold = hssfworkbook.CreateCellStyle();
        styleCenter16Bold.SetFont(font16Bold);
        styleCenter16Bold.VerticalAlignment = VerticalAlignment.Top;
        styleCenter16Bold.Alignment = HorizontalAlignment.Center;
        styleCenter16Bold.WrapText = true;

        ICellStyle styleRight12Bold = hssfworkbook.CreateCellStyle();
        styleRight12Bold.SetFont(font12Bold);
        styleRight12Bold.VerticalAlignment = VerticalAlignment.Top;
        styleRight12Bold.Alignment = HorizontalAlignment.Right;
        styleRight12Bold.WrapText = true;

        ICellStyle styleCenterBorder12Bold = hssfworkbook.CreateCellStyle();
        styleCenterBorder12Bold.SetFont(font12Bold);
        styleCenterBorder12Bold.VerticalAlignment = VerticalAlignment.Top;
        styleCenterBorder12Bold.Alignment = HorizontalAlignment.Center;
        styleCenterBorder12Bold.WrapText = true;
        styleCenterBorder12Bold.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleCenterBorder12Bold.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleCenterBorder12Bold.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleCenterBorder12Bold.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle styleBorder12Bold = hssfworkbook.CreateCellStyle();
        styleBorder12Bold.SetFont(font12Bold);
        styleBorder12Bold.VerticalAlignment = VerticalAlignment.Top;
        styleBorder12Bold.WrapText = true;
        styleBorder12Bold.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12Bold.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12Bold.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12Bold.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle styleCenterBorderBottom12 = hssfworkbook.CreateCellStyle();
        styleCenterBorderBottom12.SetFont(font12);
        styleCenterBorderBottom12.VerticalAlignment = VerticalAlignment.Top;
        styleCenterBorderBottom12.Alignment = HorizontalAlignment.Center;
        styleCenterBorderBottom12.WrapText = true;
        styleCenterBorderBottom12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle style12BoldItalic = hssfworkbook.CreateCellStyle();
        style12BoldItalic.SetFont(font12BoldItalic);
        style12BoldItalic.VerticalAlignment = VerticalAlignment.Top;
        style12BoldItalic.WrapText = true;

        ICellStyle styleCenter12 = hssfworkbook.CreateCellStyle();
        styleCenter12.SetFont(font12);
        styleCenter12.VerticalAlignment = VerticalAlignment.Top;
        styleCenter12.Alignment = HorizontalAlignment.Center;
        styleCenter12.WrapText = true;

        ICellStyle style12 = hssfworkbook.CreateCellStyle();
        style12.SetFont(font12);
        style12.VerticalAlignment = VerticalAlignment.Top;
        style12.WrapText = true;

        ICellStyle style12Bold = hssfworkbook.CreateCellStyle();
        style12Bold.SetFont(font12Bold);
        style12Bold.VerticalAlignment = VerticalAlignment.Top;
        style12Bold.WrapText = true;

        ICellStyle style12TopBold = hssfworkbook.CreateCellStyle();
        style12TopBold.SetFont(font12Bold);
        style12TopBold.VerticalAlignment = VerticalAlignment.Bottom;
        style12TopBold.WrapText = true;

        ICellStyle style12Top = hssfworkbook.CreateCellStyle();
        style12Top.SetFont(font12);
        style12Top.VerticalAlignment = VerticalAlignment.Bottom;
        style12Top.WrapText = true;

        ICellStyle styleBorder12 = hssfworkbook.CreateCellStyle();
        styleBorder12.SetFont(font12);
        styleBorder12.VerticalAlignment = VerticalAlignment.Top;
        styleBorder12.WrapText = true;
        styleBorder12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorder12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle styleBorderCenter12 = hssfworkbook.CreateCellStyle();
        styleBorderCenter12.SetFont(font12);
        styleBorderCenter12.VerticalAlignment = VerticalAlignment.Top;
        styleBorderCenter12.Alignment = HorizontalAlignment.Center;
        styleBorderCenter12.WrapText = true;
        styleBorderCenter12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorderCenter12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorderCenter12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleBorderCenter12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle styleNumber0Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0");
        styleNumber0Border12.SetFont(font12);
        styleNumber0Border12.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0Border12.WrapText = true;
        styleNumber0Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;


        ICellStyle styleNumber0i0Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0i0Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.0");
        styleNumber0i0Border12.SetFont(font12);
        styleNumber0i0Border12.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i0Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0i0Border12.WrapText = true;
        styleNumber0i0Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i0Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i0Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i0Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;


        ICellStyle styleNumber0i00Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0i00Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i00Border12.SetFont(font12);
        styleNumber0i00Border12.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i00Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0i00Border12.WrapText = true;
        styleNumber0i00Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i00Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i00Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i00Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;


        ICellStyle styleNumber0i000Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0i000Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.000");
        styleNumber0i000Border12.SetFont(font12);
        styleNumber0i000Border12.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i000Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0i000Border12.WrapText = true;
        styleNumber0i000Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i000Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i000Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0i000Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;



        ICellStyle styleNumber012 = hssfworkbook.CreateCellStyle();
        styleNumber012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0");
        styleNumber012.SetFont(font12);
        styleNumber012.VerticalAlignment = VerticalAlignment.Top;
        styleNumber012.Alignment = HorizontalAlignment.Right;
        styleNumber012.WrapText = true;


        ICellStyle styleNumber0i012 = hssfworkbook.CreateCellStyle();
        styleNumber0i012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.0");
        styleNumber0i012.SetFont(font12);
        styleNumber0i012.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i012.Alignment = HorizontalAlignment.Right;
        styleNumber0i012.WrapText = true;


        ICellStyle styleNumber0i0012 = hssfworkbook.CreateCellStyle();
        styleNumber0i0012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i0012.SetFont(font12);
        styleNumber0i0012.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i0012.Alignment = HorizontalAlignment.Right;
        styleNumber0i0012.WrapText = true;

        ICellStyle styleNumber0i00012 = hssfworkbook.CreateCellStyle();
        styleNumber0i00012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.000");
        styleNumber0i00012.SetFont(font12);
        styleNumber0i00012.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i00012.Alignment = HorizontalAlignment.Right;
        styleNumber0i00012.WrapText = true;


        ICellStyle styleNumber012Bold = hssfworkbook.CreateCellStyle();
        styleNumber012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0");
        styleNumber012Bold.SetFont(font12Bold);
        styleNumber012Bold.VerticalAlignment = VerticalAlignment.Top;
        styleNumber012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber012Bold.WrapText = true;


        ICellStyle styleNumber0i012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.0");
        styleNumber0i012Bold.SetFont(font12Bold);
        styleNumber0i012Bold.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i012Bold.WrapText = true;


        ICellStyle styleNumber0i0012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i0012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i0012Bold.SetFont(font12Bold);
        styleNumber0i0012Bold.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i0012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i0012Bold.WrapText = true;

        ICellStyle styleNumber0i00012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i00012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.000");
        styleNumber0i00012Bold.SetFont(font12Bold);
        styleNumber0i00012Bold.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i00012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i00012Bold.WrapText = true;


        s1.SetMargin(MarginType.RightMargin, (double)0.5);
        s1.SetMargin(MarginType.TopMargin, (double)0.6);
        s1.SetMargin(MarginType.LeftMargin, (double)0.4);
        s1.SetMargin(MarginType.BottomMargin, (double)0.3);
        s1.FitToPage = true;

        IPrintSetup print = s1.PrintSetup;
        print.PaperSize = (short)PaperSize.A4;
        print.Scale = (short)80;
        print.FitWidth = (short)1;
        print.FitHeight = (short)0;

        s1.SetColumnWidth(0, 5500);
        s1.SetColumnWidth(1, 3500);
        s1.SetColumnWidth(2, 9500);
        s1.SetColumnWidth(3, 3000);
        s1.SetColumnWidth(4, 3000);
        s1.SetColumnWidth(5, 3800);
        s1.SetColumnWidth(6, 4000);
        s1.SetColumnWidth(8, 0);
        s1.SetColumnWidth(11, 0);


        s1.CreateRow(0).CreateCell(0).SetCellValue(noidungNCC1);
        s1.GetRow(0).GetCell(0).CellStyle = styleCenter18Bold;
        s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));
        s1.GetRow(0).HeightInPoints = 30;

        s1.CreateRow(1).CreateCell(0).SetCellValue(noidungNCC2);
        s1.GetRow(1).GetCell(0).CellStyle = styleCenter12;
        s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 6));
        s1.GetRow(1).HeightInPoints = 22;

        s1.CreateRow(2).CreateCell(0).SetCellValue(noidungNCC3);
        s1.GetRow(2).GetCell(0).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(1).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(2).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(3).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(4).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(5).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(6).CellStyle = styleCenterBorderBottom12;

        s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));
        s1.GetRow(2).HeightInPoints = 22;

        s1.CreateRow(3).CreateCell(0).SetCellValue("COMMERCIAL INVOICE");
        s1.GetRow(3).GetCell(0).CellStyle = styleCenter18Bold;
        s1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 6));
        s1.GetRow(3).HeightInPoints = 30;

        s1.CreateRow(4).CreateCell(3).SetCellValue("No.:");
        s1.GetRow(4).GetCell(3).CellStyle = style12Bold;

        s1.GetRow(4).CreateCell(4).SetCellValue(dt.Rows[0]["so_pkl"].ToString());
        s1.GetRow(4).GetCell(4).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(4, 4, 4, 6));
        s1.GetRow(4).HeightInPoints = 22;

        s1.CreateRow(5).CreateCell(3).SetCellValue("Date:");
        s1.GetRow(5).GetCell(3).CellStyle = style12Bold;

        s1.GetRow(5).CreateCell(4).SetCellValue(DateTime.Parse(dt.Rows[0]["ngaytao"].ToString()).ToString("dd-MMM-yyyy"));
        s1.GetRow(5).GetCell(4).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(5, 5, 4, 6));
        s1.GetRow(5).HeightInPoints = 22;


        s1.CreateRow(6).CreateCell(0).SetCellValue("For the account & risk of the buyer:");
        s1.GetRow(6).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(6, 6, 0, 6));
        s1.GetRow(6).HeightInPoints = 22;

        string dc_kh = "";
        dc_kh = dt.Rows[0]["ten_dtkd"].ToString() + " \nAddress:" + dt.Rows[0]["diachi"].ToString() + "\nTel:" + dt.Rows[0]["tel"].ToString() + "\nFax:" + dt.Rows[0]["fax"].ToString();
        HSSFRichTextString rich = new HSSFRichTextString(dc_kh);
        s1.CreateRow(7).CreateCell(2).SetCellValue(rich);

        s1.GetRow(7).GetCell(2).CellStyle = styleTop14Bold;
        s1.GetRow(7).HeightInPoints = 95;
        s1.AddMergedRegion(new CellRangeAddress(7, 7, 2, 6));

        s1.CreateRow(9).CreateCell(0).SetCellValue("M/V:");
        s1.GetRow(9).GetCell(0).CellStyle = style12;

        s1.GetRow(9).CreateCell(2).SetCellValue(dt.Rows[0]["mv"].ToString());
        s1.GetRow(9).GetCell(2).CellStyle = style12;
        s1.GetRow(9).HeightInPoints = 22;

        s1.AddMergedRegion(new CellRangeAddress(9, 9, 2, 6));

        s1.CreateRow(10).CreateCell(0).SetCellValue("From:");
        s1.GetRow(10).GetCell(0).CellStyle = style12;

        s1.GetRow(10).CreateCell(2).SetCellValue(dt.Rows[0]["ten_cangbien"].ToString());
        s1.GetRow(10).GetCell(2).CellStyle = style12;

        s1.GetRow(10).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(10, 10, 2, 6));

        s1.CreateRow(11).CreateCell(0).SetCellValue("To:");
        s1.GetRow(11).GetCell(0).CellStyle = style12;

        s1.GetRow(11).CreateCell(2).SetCellValue(dt.Rows[0]["noiden"].ToString());
        s1.GetRow(11).GetCell(2).CellStyle = style12;

        s1.GetRow(11).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(11, 11, 2, 6));

        s1.CreateRow(12).CreateCell(0).SetCellValue("B/L No.:");
        s1.GetRow(12).GetCell(0).CellStyle = style12;

        s1.GetRow(12).CreateCell(2).SetCellValue(dt.Rows[0]["blno"].ToString());
        s1.GetRow(12).GetCell(2).CellStyle = style12;

        s1.GetRow(12).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(12, 12, 2, 6));

        s1.CreateRow(13).CreateCell(0).SetCellValue("Commodity:");
        s1.GetRow(13).GetCell(0).CellStyle = style12;

        s1.GetRow(13).CreateCell(2).SetCellValue((dt.Rows[0]["commodity"].ToString() + " wares").ToUpper());
        s1.GetRow(13).GetCell(2).CellStyle = style12;

        s1.GetRow(13).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(13, 13, 2, 6));


        s1.CreateRow(14).CreateCell(2).SetCellValue(String.Format("AS PER PROFORMA INVOICE NO. : {0}", chungtudonhang));
        s1.GetRow(14).GetCell(2).CellStyle = style12;
        s1.GetRow(14).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(14, 14, 2, 6));

        // table
        s1.CreateRow(15).CreateCell(0).SetCellValue("Seller's Article");
        s1.GetRow(15).CreateCell(1).SetCellValue("Buyer's Article");
        s1.GetRow(15).CreateCell(2).SetCellValue("Description of goods");
        s1.GetRow(15).CreateCell(3).SetCellValue("Quantity (pcs,sets)");
        s1.GetRow(15).CreateCell(4).SetCellValue("");
        s1.GetRow(15).CreateCell(5).SetCellValue("Unit price (USD/pc,set)");
        s1.GetRow(15).CreateCell(6).SetCellValue("Amount (USD)");

        s1.GetRow(15).GetCell(0).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(1).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(2).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(3).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(4).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(5).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(6).CellStyle = styleCenterBorder12Bold;


        s1.AddMergedRegion(new CellRangeAddress(15, 15, 3, 4));

        /*string sql_group = @"select distinct dpk.c_donhang_id, (select sochungtu from c_donhang dh where dh.c_donhang_id = dpk.c_donhang_id) as sodh,  cdh.discount, cnx.container, cnx.soseal, cb.ten_cangbien, cnx.c_nhapxuat_id,
                           isnull(cdh.cont20, 0) as cont20, isnull(cdh.cont40,0) as cont40, isnull(cdh.cont40hc, 0) as cont40hc, isnull(cdh.contle, 0 ) as contle, isnull(cdh.cont45, 0) as cont45
                            from c_dongpklinv dpk, c_packinginvoice cpk, c_donhang cdh, c_dongnhapxuat dnx, c_nhapxuat cnx, md_cangbien cb
                            where dpk.c_packinginvoice_id = '" + c_pkivn_id + "'" + //c_pkivn_id
                                    @"and dpk.c_donhang_id = cdh.c_donhang_id
		                            and dpk.c_dongnhapxuat_id = dnx.c_dongnhapxuat_id
		                            and dnx.c_nhapxuat_id = cnx.c_nhapxuat_id
                                    and dpk.c_packinginvoice_id = cpk.c_packinginvoice_id
		                            and cpk.noidi = cb.md_cangbien_id";*/

        string sql_group = string.Format(@"
            select distinct 
                dpk.c_donhang_id, 
                dh.sochungtu as sodh,
                po.discountPO as discount,
                po.hehang_value as discount_hehang_value,
                isnull(dh.cpdg_vuotchuan, 0) as cpdg_vuotchuan,
                cnx.container, 
                cnx.soseal, 
                cb.ten_cangbien, 
                cnx.c_nhapxuat_id,
                isnull(cnx.loaicont, 0) as loaicont
            from 
                c_dongpklinv dpk, 
                c_packinginvoice cpk, 
                c_donhang dh outer APPLY (select * from dbo.ftbl_getInfoPO(dh.phanbodiscount, dh.discount, null, dh.discount_hehang_value)) as po, 
                c_dongnhapxuat dnx, 
                c_nhapxuat cnx, 
                md_cangbien cb
            where 
                dpk.c_packinginvoice_id = '{0}'
                and dpk.c_donhang_id = dh.c_donhang_id
		        and dpk.c_dongnhapxuat_id = dnx.c_dongnhapxuat_id
		        and dnx.c_nhapxuat_id = cnx.c_nhapxuat_id
                and dpk.c_packinginvoice_id = cpk.c_packinginvoice_id
		        and cpk.noidi = cb.md_cangbien_id"
        , c_pkivn_id);

        DataTable dt_g = mdbc.GetData(sql_group);

        string dh_ = string.Format(@"
            select distinct 
                c_donhang_id, 
                sodh, 
                discount_hehang_value,
                cpdg_vuotchuan,
                container, 
                soseal, 
                ten_cangbien, 
			    convert(decimal(18,2), discount) as discount, 
			    discount as discount1,
			    c_nhapxuat_id,  
                loaicont 
            from ({0}) as tmp 
            order by 
                container asc, 
                sodh asc 
            ", sql_group);
        DataTable data_dh = mdbc.GetData(dh_);
        int g_line = 0;
        int r = 16;

        int countDonHang = data_dh.Rows.Count;
        List<int> lstQuantity = new List<int>();
        List<int> lstTotalSalary = new List<int>();
        List<int> lstDiscount = new List<int>();
        double sumDiscount = 0;
        double sumFee = 0;
        int count = 1;
        string funcDiscount = "";
        int rDiscount = 0;
        foreach (DataRow g_it in data_dh.Rows)
        {
            string discount_hehang_value = g_it["discount_hehang_value"].ToString();
            var nnl = string.IsNullOrEmpty(discount_hehang_value) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(discount_hehang_value);


            /*decimal cont20, cont40, cont40hc, contle, cont45;
            cont20 = decimal.Parse(g_it["cont20"].ToString());
            cont40 = decimal.Parse(g_it["cont40"].ToString());
            cont40hc = decimal.Parse(g_it["cont40hc"].ToString());
            contle = decimal.Parse(g_it["contle"].ToString());
            cont45 = decimal.Parse(g_it["cont45"].ToString());*/
            string loaicont = g_it["loaicont"].ToString();

            /*s1.CreateRow(r).CreateCell(0).SetCellValue("Container/Seal No.: " + g_it["container"].ToString() + "/" + g_it["soseal"].ToString() + " "
                + (cont20 != 0 ? (", " + 1 + "x20'") : (""))
                + (cont40 != 0 ? (", " + 1 + "x40'") : (""))
                + (cont40hc != 0 ? (", " + 1 + "x40hc") : (""))
                + (contle != 0 ? (", " + 1 + "xLCL'") : (""))
                + (cont45 != 0 ? (", " + 1 + "x450'") : (""))
                );*/

            switch (loaicont)
            {
                case "CONT20":
                    loaicont = "1x20'";
                    break;
                case "CONT40":
                    loaicont = "1x40'";
                    break;
                case "40HC":
                    loaicont = "1x40hc'";
                    break;
                case "LE":
                    loaicont = "1xLCL'";
                    break;
                case "CONT45":
                    loaicont = "1x45'";
                    break;
                default:
                    loaicont = "";
                    break;
            }

            s1.CreateRow(r).CreateCell(0).SetCellValue("Container/Seal No.: " + g_it["container"].ToString() + "/" + g_it["soseal"].ToString() + " " + loaicont);


            if (g_line == 0)
            {
                s1.GetRow(r).CreateCell(3).SetCellValue("FOB " + g_it["ten_cangbien"].ToString());
            }
            else
            {
                s1.GetRow(r).CreateCell(3).SetCellValue("");
            }
            g_line++;

            s1.GetRow(r).GetCell(0).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(1).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(2).CellStyle = styleBorder12Bold;
            s1.GetRow(r).GetCell(3).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(4).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(5).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(6).CellStyle = styleBorderCenter12;
            s1.GetRow(r).HeightInPoints = 22;

            s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 2));
            s1.AddMergedRegion(new CellRangeAddress(r, r, 3, 6));

            r++;
            s1.CreateRow(r).CreateCell(0).SetCellValue("P/I No. " + g_it["sodh"].ToString());//

            s1.GetRow(r).GetCell(0).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(1).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(2).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(3).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(4).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(5).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(6).CellStyle = styleBorder12;
            s1.GetRow(r).HeightInPoints = 22;

            s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 2));
            s1.AddMergedRegion(new CellRangeAddress(r, r, 3, 6));

            string sql_de = string.Format(@"
                            select sp.ma_sanpham
                                , cdd.ma_sanpham_khach
                                , dpk.soluong
                                , dpk.gia
                                , dpk.thanhtien
							    , dpk.mota_tienganh
                                , dvt.ten_dvt
							from c_dongpklinv dpk left join md_sanpham sp on dpk.md_sanpham_id = sp.md_sanpham_id
								left join c_dongnhapxuat cdnx on dpk.c_dongnhapxuat_id = cdnx.c_dongnhapxuat_id
								left join c_dongdonhang cdd on cdd.c_dongdonhang_id = cdnx.c_dongdonhang_id
								left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
							where dpk.c_donhang_id = '{0}'
								and c_packinginvoice_id = '{1}'
								and cdnx.c_nhapxuat_id ='{2}'
								order by sp.ma_sanpham asc", g_it["c_donhang_id"], c_pkivn_id, g_it["c_nhapxuat_id"]);
            DataTable dt_d = mdbc.GetData(sql_de);

            //create detail group
            var startRowHH = -1;
            var lstHH = new List<int>();

            for (var i = 0; i < dt_d.Rows.Count; i++)
            {
                r++;
                DataRow g_dt = dt_d.Rows[i];
                var msp = g_dt["ma_sanpham"].ToString();
                var chungloai = msp.Substring(0, 2);
                var chungloaiTT = i == dt_d.Rows.Count - 1 ? chungloai : dt_d.Rows[i + 1]["ma_sanpham"].ToString().Substring(0, 2);
                if (startRowHH == -1)
                    startRowHH = r;

                IRow ir = s1.CreateRow(r);
                ir.CreateCell(0).SetCellValue(g_dt["ma_sanpham"].ToString());
                ir.GetCell(0).CellStyle = styleBorder12;

                ir.CreateCell(1).SetCellValue(g_dt["ma_sanpham_khach"].ToString());
                ir.GetCell(1).CellStyle = styleBorder12;

                ir.CreateCell(2).SetCellValue(g_dt["mota_tienganh"].ToString());
                ir.GetCell(2).CellStyle = styleBorder12;

                ir.CreateCell(3).SetCellValue(double.Parse(g_dt["soluong"].ToString()));
                ir.GetCell(3).CellStyle = styleNumber0Border12;

                ir.CreateCell(4).SetCellValue(g_dt["ten_dvt"].ToString() + "s");
                ir.GetCell(4).CellStyle = styleBorder12;

                ir.CreateCell(5).SetCellValue(double.Parse(g_dt["gia"].ToString()));
                ir.GetCell(5).CellStyle = styleNumber0i00Border12;

                ir.CreateCell(6).CellFormula = String.Format("D{0}*F{0}", r + 1);
                //ir.CreateCell(6).SetCellValue(double.Parse(g_dt["thanhtien"].ToString()));
                ir.GetCell(6).CellStyle = styleNumber0i00Border12;

                ir.CreateCell(8).SetCellValue(double.Parse(g_it["discount1"].ToString()));
                ir.GetCell(8).CellStyle = styleNumber0i0012;


                if ((chungloai != chungloaiTT & i < dt_d.Rows.Count - 1) | i == dt_d.Rows.Count - 1)
                {
                    if (nnl != null)
                    {
                        string gthh = nnl == null ? "0" : nnl.Where(s => s["hehang"].ToString() == chungloai).Select(s => s["giatri"].ToString()).FirstOrDefault();
                        var rowHeight = gthh == "0" ? 0 : 20;
                        r++;
                        var rowToTalHeHang = s1.CreateRow(r);
                        rowToTalHeHang.HeightInPoints = rowHeight;
                        rowToTalHeHang.CreateCell(2).SetCellValue(string.Format("Sub Total ({0}):", lstHH.Count + 1));
                        rowToTalHeHang.GetCell(2).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));
                        string fmlSum = string.Format(@"SUM(VNN{0}:VNN{1})", startRowHH + 1, r);
                        rowToTalHeHang.CreateCell(3).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "D"));
                        rowToTalHeHang.GetCell(3).CellStyle = styleNumber012Bold;
                        rowToTalHeHang.CreateCell(4).SetCellValue("pcs/sets");
                        rowToTalHeHang.GetCell(4).CellStyle = style12Bold;
                        rowToTalHeHang.CreateCell(6).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "G"));
                        rowToTalHeHang.GetCell(6).CellStyle = styleNumber0i0012Bold;
                        r++;

                        var rowDisHeHang = s1.CreateRow(r);
                        rowDisHeHang.HeightInPoints = rowHeight;
                        rowDisHeHang.CreateCell(2).SetCellValue(string.Format(@"Discount ({0}%):", gthh));
                        rowDisHeHang.GetCell(2).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));
                        rowDisHeHang.CreateCell(6).CellFormula = string.Format("ROUND(G{0}*{1}/100, 2)", r, gthh);
                        rowDisHeHang.GetCell(6).CellStyle = styleNumber0i0012Bold;
                        r++;

                        var rowTotalHeHang = s1.CreateRow(r);
                        rowTotalHeHang.HeightInPoints = rowHeight;
                        rowTotalHeHang.CreateCell(2).SetCellValue(string.Format("Total ({0}):", lstHH.Count + 1));
                        rowTotalHeHang.GetCell(2).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));
                        rowTotalHeHang.CreateCell(3).CellFormula = string.Format("D{0}", r - 1);
                        rowTotalHeHang.GetCell(3).CellStyle = styleNumber012Bold;
                        rowTotalHeHang.CreateCell(6).CellFormula = string.Format("G{0} - G{1}", r - 1, r);
                        rowTotalHeHang.GetCell(6).CellStyle = styleNumber0i0012Bold;

                        lstHH.Add(r);
                        startRowHH = r + 1;
                    }
                }
            }

            string sumQuantityPO = "", sumSubToTalPO = "";
            if (nnl != null)
            {
                for (var iHH = 0; iHH < lstHH.Count; iHH++)
                {
                    var rowHH = lstHH[iHH];
                    sumQuantityPO += string.Format(@"D{0}+", rowHH + 1);
                    sumSubToTalPO += string.Format(@"G{0}+", rowHH + 1);
                }
                sumQuantityPO += "0";
                sumSubToTalPO += "0";
            }
            else
            {
                sumQuantityPO += string.Format(@"SUM(D{0}:D{1})", startRowHH + 1, r + 1);
                sumSubToTalPO += string.Format(@"SUM(G{0}:G{1})", startRowHH + 1, r + 1);
            }
            r++;
            s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Sub Total:", countDonHang > 1 ? " (" + count.ToString() + ")" : ""));
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

            // total quantity
            s1.GetRow(r).CreateCell(3).CellFormula = sumQuantityPO;
            s1.GetRow(r).GetCell(3).CellStyle = styleNumber012Bold;
            lstQuantity.Add(r + 1);

            s1.GetRow(r).CreateCell(4).SetCellValue("pcs/sets");
            s1.GetRow(r).GetCell(4).CellStyle = style12Bold;

            //total salary
            s1.GetRow(r).CreateCell(6).CellFormula = sumSubToTalPO;
            s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;
            lstTotalSalary.Add(r + 1);

            // discount hidden
            s1.GetRow(r).CreateCell(11).SetCellValue(double.Parse(g_it["discount1"].ToString()));
            s1.GetRow(r).GetCell(11).CellStyle = styleNumber0i0012Bold;
            // total discount
            double dsc1 = double.Parse(g_it["discount1"].ToString());
            if (dsc1 == 0)
                s1.GetRow(r).HeightInPoints = 0;
            r++;

            string str_dsc1 = dsc1.ToString();

            var discountPO = g_it["discount"].ToString() + "";
            discountPO = discountPO.Replace(".00", "");
            s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Discount ({0}%):", discountPO));
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

            funcDiscount = "ROUND(G{0}*L{0}/100,2)";

            rDiscount = r;
            s1.GetRow(r).CreateCell(6).CellFormula = String.Format(funcDiscount, r);
            s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;

            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
            lstDiscount.Add(r + 1);

            sumDiscount += double.Parse(discountPO);

            if (double.Parse(discountPO) == 0) s1.GetRow(r).HeightInPoints = 0;

            //Packing Fee
            r++;
            var packingFee = double.Parse(g_it["cpdg_vuotchuan"].ToString());
            s1.CreateRow(r).CreateCell(2).SetCellValue(string.Format("Extra packing fees:"));
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
            s1.GetRow(r).CreateCell(6).SetCellValue(packingFee);
            s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;
            if (packingFee == 0) s1.GetRow(r).HeightInPoints = 0;
            sumFee += packingFee;
            //Total
            r++;
            s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Total{0}:", countDonHang > 1 ? " (" + count.ToString() + ")" : ""));
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

            s1.GetRow(r).CreateCell(6).CellFormula = String.Format("G{0}-G{1}+G{2}", r - 2, r - 1, r);
            s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;

            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

            // total quantity
            s1.GetRow(r).CreateCell(3).CellFormula = String.Format("D{0}", r - 2);
            s1.GetRow(r).GetCell(3).CellStyle = styleNumber012Bold;

            r++;
            count++;
            lstPO.Add(r);
        }

        s1.CreateRow(r).CreateCell(2).SetCellValue("(+)" + pdkInvoice.diengiai_cong + ":");
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(6).SetCellValue(double.Parse(pdkInvoice.giatri_cong.Value.ToString()));
        s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;

        if (pdkInvoice.giatri_cong.Value == 0) s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue("(-)" + pdkInvoice.diengiai_tru + ":");
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(6).SetCellValue(double.Parse(pdkInvoice.giatri_tru.Value.ToString()));
        s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatri_tru.Value == 0) s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue(pdkInvoice.diengiaicong_po + ":");
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(6).SetCellValue(double.Parse(pdkInvoice.giatricong_po.Value.ToString()));
        s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatricong_po.Value == 0) s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue(pdkInvoice.diengiaitru_po + ":");
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(6).SetCellValue(double.Parse(pdkInvoice.giatritru_po.Value.ToString()));
        s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatritru_po.Value == 0) s1.GetRow(r).HeightInPoints = 0;

        String strCount = "";
        for (int i = 1; i < count; i++)
        {
            strCount += " + " + i;
        }
        strCount = strCount.Substring(2);
        //Packing Fee
        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Extra packing fees{0}", countDonHang > 1 ? " (" + strCount + "):" : ":"));
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
        string sumToTalPackingFee = "";
        for (var iPO = 0; iPO < lstPO.Count; iPO++)
        {
            var row = lstPO[iPO];
            sumToTalPackingFee += string.Format(@"G{0}+", row - 1);
        }
        sumToTalPackingFee += "0";
        s1.GetRow(r).CreateCell(6).CellFormula = string.Format("{0}", sumToTalPackingFee);
        s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.cpdg_vuotchuan.GetValueOrDefault(0) == 0) s1.GetRow(r).HeightInPoints = 0;
        //Total
        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Total{0}", countDonHang > 1 ? " (" + strCount + "):" : ":"));
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        // total quantity
        string fQuantity = "", fSalary = "", fDiscount = "";

        foreach (int item in lstQuantity)
        {
            fQuantity += String.Format("+ D{0} ", item);
        }
        fQuantity = fQuantity.Substring(1);

        //total discount
        foreach (int item in lstDiscount)
        {
            fDiscount += String.Format("+ G{0} ", item);
        }
        fDiscount = fDiscount.Substring(1);


        //total salary
        foreach (int item in lstTotalSalary)
        {
            fSalary += String.Format("+ G{0} ", item);
        }
        fSalary = fSalary.Substring(1);

        s1.GetRow(r).CreateCell(3).SetCellFormula(fQuantity);
        s1.GetRow(r).GetCell(3).CellStyle = styleNumber012Bold;


        string sumToTalPO = "";
        for (var iPO = 0; iPO < lstPO.Count; iPO++)
        {
            var row = lstPO[iPO];
            sumToTalPO += string.Format(@"G{0}+", row);
        }
        sumToTalPO += "0";
        s1.GetRow(r).CreateCell(6).CellFormula = string.Format("{0} + G{1} - G{2} + G{3} - G{4}", sumToTalPO, r - 4, r - 3, r - 2, r - 1);
        s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;



        HSSFFormulaEvaluator formula = new HSSFFormulaEvaluator(hssfworkbook);
        CellValue cValue = formula.Evaluate(s1.GetRow(r).GetCell(6));
        double doctien = cValue.NumberValue;

        s1.GetRow(rDiscount).GetCell(6).SetCellFormula(string.Format(funcDiscount, rDiscount));

        s1.GetRow(r).HeightInPoints = 0;
        s1.GetRow(r - 1).HeightInPoints = 0;
        if ((countDonHang > 1) || (sumDiscount > 0 && countDonHang > 1) || (pdkInvoice.giatri_cong > 0) || (pdkInvoice.giatri_tru > 0) || (pdkInvoice.giatricong_po > 0) || (pdkInvoice.giatritru_po > 0))
        {
            s1.GetRow(r).HeightInPoints = 32;
            if (sumFee > 0)
                s1.GetRow(r - 1).HeightInPoints = 32;
        }


        //create end page

        r++;
        ReadMoney.NumberToEnglish convert = new ReadMoney.NumberToEnglish();
        double total__ = Convert.ToDouble(pdkInvoice.totalgross.Value + pdkInvoice.cpdg_vuotchuan.GetValueOrDefault(0));
        String m = MoneyToWord.ConvertMoneyToText(total__.ToString()).Replace("Dollars", "");

        s1.CreateRow(r).CreateCell(0).SetCellValue("Say: USD " + m);

        s1.GetRow(r).GetCell(0).CellStyle = styleRight12Bold;
        s1.GetRow(r).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 6));


        r++;

        s1.CreateRow(r).CreateCell(0).SetCellValue(@"Please arrange the payment to our account with banking details as follows:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;
        s1.GetRow(r).HeightInPoints = 22;

        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 6));


        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue(@"Beneficary's name:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;

        s1.GetRow(r).CreateCell(1).SetCellValue(noidungNCC4);
        s1.GetRow(r).GetCell(1).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 1, 6));

        s1.GetRow(r).HeightInPoints = 22;


        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue(@"Beneficary's address:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;

        s1.GetRow(r).CreateCell(1).SetCellValue(noidungNCC2);
        s1.GetRow(r).GetCell(1).CellStyle = style12;

        s1.GetRow(r).HeightInPoints = 44;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 1, 6));


        r++;

        s1.CreateRow(r).CreateCell(0).SetCellValue("Account no.:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;
        s1.GetRow(r).HeightInPoints = 22;

        s1.GetRow(r).CreateCell(1).SetCellValue(noidungNCC5);
        s1.GetRow(r).GetCell(1).CellStyle = style12Bold;
        s1.GetRow(r).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 1, 6));

        r++;

        s1.CreateRow(r).CreateCell(0).SetCellValue("At bank:");
        s1.GetRow(r).GetCell(0).CellStyle = style12Top;
        s1.GetRow(r).HeightInPoints = 30;

        var dong1Bank = noidungNCC6;
        var dong2Bank = noidungNCC7;
        var dong3Bank = noidungNCC8;
        var dong4Bank = noidungNCC9;

        var rtxtBank = new HSSFRichTextString(
            dong1Bank + "\n" +
            dong2Bank + "\n" +
            dong3Bank + "\n" +
            dong4Bank
        );

        IFont bold12Font = hssfworkbook.CreateFont();
        bold12Font.FontHeightInPoints = 12;
        bold12Font.FontName = "Arial";
        bold12Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        IFont normal12Font = hssfworkbook.CreateFont();
        normal12Font.FontHeightInPoints = 12;
        normal12Font.FontName = "Arial";
        normal12Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

        //--1
        var startBank = 0;
        rtxtBank.ApplyFont(startBank, startBank + dong1Bank.Length, bold12Font);
        //--2
        startBank = dong1Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong2Bank.Length, normal12Font);
        //--3
        startBank += dong2Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong3Bank.Length, normal12Font);
        //--4
        startBank += dong3Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong4Bank.Length, bold12Font);

        s1.GetRow(r).CreateCell(1).SetCellValue(rtxtBank);
        s1.GetRow(r).GetCell(1).CellStyle.WrapText = true;
        s1.GetRow(r).GetCell(1).CellStyle.VerticalAlignment = VerticalAlignment.Bottom;
        s1.GetRow(r).HeightInPoints = 70;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 1, 6));

        r++;
        s1.CreateRow(r).CreateCell(3).SetCellValue(noidungNCC1);
        s1.GetRow(r).GetCell(3).CellStyle = styleCenter16Bold;
        s1.GetRow(r).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 3, 6));

        r++;

        //var link = ExcuteSignalRStatic.mapPathSignalR("~/images/more/ckcdct.jpg");
        if (File.Exists(link))
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(link);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            IDrawing patriarch = s1.CreateDrawingPatriarch();
            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 4, r + 1, 4, r + 1);
            anchor.AnchorType = AnchorType.MoveDontResize;

            int index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.JPEG);
            IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
            signaturePicture.Resize();
        }

        return hssfworkbook;
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName)
    {
        var compineF = Request.QueryString["compineF"];
        if (string.IsNullOrWhiteSpace(compineF))
        {
            MemoryStream exportData = new MemoryStream();
            hsswb.Write(exportData);

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
            Response.Clear();
            Response.BinaryWrite(exportData.GetBuffer());
            Response.End();
        }
        else
        {
            var link = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/FileUpload/Compine/{0}.xls", Guid.NewGuid().ToString()));
            using (FileStream file = new FileStream(link, FileMode.Create, FileAccess.ReadWrite))
            {
                hsswb.Write(file);
                file.Dispose();
            }
            var dic = new Dictionary<string, string>();
            dic["link"] = link;
            dic["name"] = "INV";
            Response.Clear();
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dic));
        }
    }

    public IFont CreateFontSize(HSSFWorkbook hsswb, short size)
    {
        IFont font = hsswb.CreateFont();
        font.FontHeightInPoints = size;
        return font;
    }
}
