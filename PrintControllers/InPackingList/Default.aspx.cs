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

public partial class PrintControllers_InPackingList_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String c_pkivn_id = Request.QueryString["c_packinglist_id"];

            String select = String.Format(@"select cpk.so_pkl, cpk.etd, dt.ten_dtkd, dt.diachi, dt.tel, dt.fax,
		                                            cpk.mv, cb.ten_cangbien, cpk.noiden, cpk.blno, cpk.commodity, cpk.ngaytao
                                            from c_packinginvoice cpk, md_doitackinhdoanh dt, md_cangbien cb
                                            where cpk.noidi = cb.md_cangbien_id
	                                            and cpk.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
	                                            and cpk.c_packinginvoice_id ='" + c_pkivn_id + "'"); //c_pkivn_id

            String selChungTuDH = String.Format(@"select 
                                                    distinct dh.sochungtu 
                                                from 
                                                    c_donhang dh, c_dongpklinv dpkl
                                                where dh.c_donhang_id = dpkl.c_donhang_id
                                                    AND dpkl.c_packinginvoice_id = N'{0}'", c_pkivn_id);
            DataTable dtChungTuDH = mdbc.GetData(selChungTuDH);
            String chungTuDH = "";

            foreach (DataRow item in dtChungTuDH.Rows)
            {
                chungTuDH += String.Format(", {0}", item[0]);
            }

            chungTuDH = chungTuDH.Substring(2);

            DataTable dt = mdbc.GetData(select);

            if (dt.Rows.Count != 0)
            {
                HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt, chungTuDH);
                String saveAsFileName = String.Format("PackingList-{0}.xls", DateTime.Now);
                this.SaveFile(hssfworkbook, saveAsFileName);
            }
            else
            {
                Response.Write("<h3>PackingList/Invoice không có dữ liệu</h3>");
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex + "");
        }
    }

    public HSSFWorkbook CreateWorkBookPO(DataTable dt, String chungTuDonHang)
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
            noidungNCC7 = "NO 16, LE TRONG TAN STREET, BINH DUONG 2 QUARTER, DI AN WARD, HO CHI MINH CITY, VIET NAM.";
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
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("PKL");

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
        style12TopBold.VerticalAlignment = VerticalAlignment.Top;
        style12TopBold.WrapText = true;

        ICellStyle style12Top = hssfworkbook.CreateCellStyle();
        style12Top.SetFont(font12);
        style12Top.VerticalAlignment = VerticalAlignment.Top;
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
		
		ICellStyle styleNumber0Border12i = hssfworkbook.CreateCellStyle();
        styleNumber0Border12i.SetFont(font12);
        styleNumber0Border12i.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0Border12i.Alignment = HorizontalAlignment.Right;
        styleNumber0Border12i.WrapText = true;
        styleNumber0Border12i.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12i.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12i.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12i.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;


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

        s1.SetColumnWidth(0, 5000);
        s1.SetColumnWidth(1, 3500);
        s1.SetColumnWidth(2, 6500);

        s1.SetColumnWidth(7, 3500);
        s1.SetColumnWidth(8, 0);
        s1.SetColumnWidth(9, 0);
        s1.SetColumnWidth(10, 0);
        s1.SetColumnWidth(11, 0);
        s1.SetColumnWidth(12, 0);
        s1.SetColumnWidth(13, 0);
        s1.SetColumnWidth(14, 0);
        s1.SetColumnWidth(15, 0);
        s1.SetColumnWidth(16, 0);
        s1.SetColumnWidth(17, 0);
        s1.SetColumnWidth(18, 0);


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


        s1.CreateRow(0).CreateCell(0).SetCellValue(noidungNCC1);
        s1.GetRow(0).GetCell(0).CellStyle = styleCenter18Bold;
        s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));
        s1.GetRow(0).HeightInPoints = 30;

        s1.CreateRow(1).CreateCell(0).SetCellValue(noidungNCC2);
        s1.GetRow(1).GetCell(0).CellStyle = styleCenter12;
        s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 7));
        s1.GetRow(1).HeightInPoints = 22;

        s1.CreateRow(2).CreateCell(0).SetCellValue(noidungNCC3);
        s1.GetRow(2).GetCell(0).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(1).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(2).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(3).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(4).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(5).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(6).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(7).CellStyle = styleCenterBorderBottom12;

        s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 7));
        s1.GetRow(2).HeightInPoints = 22;

        s1.CreateRow(3).CreateCell(0).SetCellValue("PACKING LIST");
        s1.GetRow(3).GetCell(0).CellStyle = styleCenter18Bold;
        s1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 7));
        s1.GetRow(3).HeightInPoints = 30;

        s1.CreateRow(4).CreateCell(4).SetCellValue("No.:");
        s1.GetRow(4).GetCell(4).CellStyle = style12Bold;

        s1.GetRow(4).CreateCell(5).SetCellValue(dt.Rows[0]["so_pkl"].ToString());
        s1.GetRow(4).GetCell(5).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(4, 4, 5, 7));
        s1.GetRow(4).HeightInPoints = 22;

        s1.CreateRow(5).CreateCell(4).SetCellValue("Date:");
        s1.GetRow(5).GetCell(4).CellStyle = style12Bold;

        s1.GetRow(5).CreateCell(5).SetCellValue(DateTime.Parse(dt.Rows[0]["ngaytao"].ToString()).ToString("dd-MMM-yyyy"));
        s1.GetRow(5).GetCell(5).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(5, 5, 5, 7));
        s1.GetRow(5).HeightInPoints = 22;

        s1.CreateRow(6).CreateCell(0).SetCellValue("The Buyer:");
        s1.GetRow(6).GetCell(0).CellStyle = style12TopBold;

        String dc_kh = dt.Rows[0]["ten_dtkd"].ToString() + " \nAddress:" + dt.Rows[0]["diachi"].ToString() + "\nTel:" + dt.Rows[0]["tel"].ToString() + "\nFax:" + dt.Rows[0]["fax"].ToString();
        HSSFRichTextString rich = new HSSFRichTextString(dc_kh);
        s1.GetRow(6).CreateCell(2).SetCellValue(rich);
        s1.GetRow(6).GetCell(2).CellStyle = styleTop14Bold;
        s1.AddMergedRegion(new CellRangeAddress(6, 6, 2, 7));

        s1.GetRow(6).HeightInPoints = 90;


        s1.CreateRow(7).CreateCell(0).SetCellValue("Mv:");
        s1.GetRow(7).GetCell(0).CellStyle = style12;

        s1.GetRow(7).CreateCell(2).SetCellValue(dt.Rows[0]["mv"].ToString());
        s1.GetRow(7).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(7, 7, 2, 7));

        s1.GetRow(7).HeightInPoints = 22;


        s1.CreateRow(8).CreateCell(0).SetCellValue("From:");
        s1.GetRow(8).GetCell(0).CellStyle = style12;

        s1.GetRow(8).CreateCell(2).SetCellValue(dt.Rows[0]["ten_cangbien"].ToString());
        s1.GetRow(8).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(8, 8, 2, 7));
        s1.GetRow(8).HeightInPoints = 22;

        s1.CreateRow(9).CreateCell(0).SetCellValue("To:");
        s1.GetRow(9).GetCell(0).CellStyle = style12;

        s1.GetRow(9).CreateCell(2).SetCellValue(dt.Rows[0]["noiden"].ToString());
        s1.GetRow(9).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(9, 9, 2, 7));
        s1.GetRow(9).HeightInPoints = 22;

        s1.CreateRow(10).CreateCell(0).SetCellValue("BL no:");
        s1.GetRow(10).GetCell(0).CellStyle = style12;

        s1.GetRow(10).CreateCell(2).SetCellValue(dt.Rows[0]["blno"].ToString());
        s1.GetRow(10).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(10, 10, 2, 7));
        s1.GetRow(10).HeightInPoints = 22;

        s1.CreateRow(11).CreateCell(0).SetCellValue("Commodity:");
        s1.GetRow(11).GetCell(0).CellStyle = style12;

        s1.GetRow(11).CreateCell(2).SetCellValue((dt.Rows[0]["commodity"].ToString() + " wares").ToUpper());
        s1.GetRow(11).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(11, 11, 2, 7));
        s1.GetRow(11).HeightInPoints = 22;

        s1.CreateRow(12).CreateCell(2).SetCellValue(String.Format("As per proforma invoice No. : {0}", chungTuDonHang).ToUpper());
        s1.GetRow(12).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(12, 12, 2, 7));
        s1.GetRow(12).HeightInPoints = 22;

        //Table
        s1.CreateRow(13).CreateCell(0).SetCellValue("Seller's Article");
        s1.GetRow(13).GetCell(0).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(1).SetCellValue("Buyer's Article");
        s1.GetRow(13).GetCell(1).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(2).SetCellValue("Description of goods");
        s1.GetRow(13).GetCell(2).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(3).SetCellValue("Packing");
        s1.GetRow(13).GetCell(3).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(4).CellStyle = styleCenterBorder12Bold;

        s1.AddMergedRegion(new CellRangeAddress(13, 13, 3, 4));

        s1.GetRow(13).CreateCell(5).SetCellValue("No. of pkgs");
        s1.GetRow(13).GetCell(5).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(6).SetCellValue("Quantity (pcs, sets)");
        s1.GetRow(13).GetCell(6).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(7).CellStyle = styleCenterBorder12Bold;

        s1.AddMergedRegion(new CellRangeAddress(13, 13, 6, 7));

        s1.GetRow(13).CreateCell(8).SetCellValue("NW(kgs)");
        s1.GetRow(13).GetCell(8).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(9).SetCellValue("GW(kgs)");
        s1.GetRow(13).GetCell(9).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(10).SetCellValue("CBM");
        s1.GetRow(13).GetCell(10).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(11).SetCellValue("L2");
        s1.GetRow(13).GetCell(11).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(12).SetCellValue("W2");
        s1.GetRow(13).GetCell(12).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(13).SetCellValue("H2");
        s1.GetRow(13).GetCell(13).CellStyle = styleCenterBorder12Bold;


        /*string sql_group = @"select distinct dpk.c_donhang_id, (select sochungtu from c_donhang dh where dh.c_donhang_id = dpk.c_donhang_id) as sodh,  cdh.discount, cnx.container, cnx.soseal, cb.ten_cangbien, cnx.c_nhapxuat_id,
                            isnull(cdh.cont20, 0) as cont20, isnull(cdh.cont40,0) as cont40, isnull(cdh.cont40hc, 0) as cont40hc, isnull(cdh.contle, 0 ) as contle, isnull(cdh.cont45, 0) as cont45
                            from c_dongpklinv dpk, c_packinginvoice cpk, c_donhang cdh, c_dongnhapxuat dnx, c_nhapxuat cnx, md_cangbien cb
                            where dpk.c_packinginvoice_id = '" + c_pkivn_id + "'" + //c_pkivn_id
                                    @"and dpk.c_donhang_id = cdh.c_donhang_id
		                            and dpk.c_dongnhapxuat_id = dnx.c_dongnhapxuat_id
		                            and dnx.c_nhapxuat_id = cnx.c_nhapxuat_id
                                    and dpk.c_packinginvoice_id = cpk.c_packinginvoice_id
		                            and cpk.noidi = cb.md_cangbien_id";*/
									
		string sql_group = @"select distinct dpk.c_donhang_id, (select sochungtu from c_donhang dh where dh.c_donhang_id = dpk.c_donhang_id) as sodh,  cdh.discount, cnx.container, cnx.soseal, cb.ten_cangbien, cnx.c_nhapxuat_id,
                            isnull(cnx.loaicont, 0) as loaicont
                            from c_dongpklinv dpk, c_packinginvoice cpk, c_donhang cdh, c_dongnhapxuat dnx, c_nhapxuat cnx, md_cangbien cb
                            where dpk.c_packinginvoice_id = '" + c_pkivn_id + "'" + //c_pkivn_id
                                    @"and dpk.c_donhang_id = cdh.c_donhang_id
		                            and dpk.c_dongnhapxuat_id = dnx.c_dongnhapxuat_id
		                            and dnx.c_nhapxuat_id = cnx.c_nhapxuat_id
                                    and dpk.c_packinginvoice_id = cpk.c_packinginvoice_id
		                            and cpk.noidi = cb.md_cangbien_id";
									
        DataTable dt_g = mdbc.GetData(sql_group);
        int total_row = dt_g.Rows.Count;

        string dh_ = @"select distinct c_donhang_id, sodh, container, soseal, ten_cangbien, discount, c_nhapxuat_id,  loaicont  from (" + sql_group + ") as tmp order by container asc, sodh asc";
        DataTable data_dh = mdbc.GetData(dh_);
        int count_group = data_dh.Rows.Count;
        int count = 1;
        int g_line = 0;
        int r = 14;
        List<int> lstNoOfPack = new List<int>();
        List<int> lstQuantity = new List<int>();
        List<int> lstNW = new List<int>();
        List<int> lstGW = new List<int>();
        List<int> lstCBM = new List<int>();

        foreach (DataRow g_it in data_dh.Rows)
        {
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
                s1.GetRow(r).CreateCell(3).SetCellValue("");
                //s1.GetRow(r).CreateCell(3).SetCellValue("FOB " + g_it["ten_cangbien"].ToString());
            }
            else
            {
                s1.GetRow(r).CreateCell(3).SetCellValue("");
            }
            g_line++;



            s1.GetRow(r).GetCell(0).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(1).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(2).CellStyle = styleBorder12Bold;

            s1.GetRow(r).GetCell(3).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(4).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(5).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(6).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(7).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(8).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(9).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(10).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(11).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(12).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(13).CellStyle = styleBorder12;

            s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 7));
            s1.GetRow(r).HeightInPoints = 22;

            r++;
            s1.CreateRow(r).CreateCell(0).SetCellValue("P/I No.: " + g_it["sodh"].ToString());

            s1.GetRow(r).GetCell(0).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(1).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(2).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(3).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(4).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(5).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(6).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(7).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(8).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(9).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(10).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(11).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(12).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(13).CellStyle = styleBorder12;
            s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 7));
            s1.GetRow(r).HeightInPoints = 22;

            string sql_de = string.Format(@"select ma_sanpham, ma_sanpham_khach, noofpack, soluong, gia, thanhtien, l1, w1, h1, l2, w2, h2,
									mota_tienganh, ten_dvt, sl_outer, sl_outer_dg, dvt_outer, cbm, nkg, sl_inner, sl_outer, gkg
									--case when sl_inner > 1 then  round((((l1+w1)*(w1 + h1) / 5400 ) * sl_outer / sl_inner ) * noofpack + nkg, 2)
										--else round(( ((l2+w2)*(w2+h2)) / 5400) * noofpack + nkg, 2)
										--end as gkg
									from
									(select sp.ma_sanpham, cdd.ma_sanpham_khach, case when cdd.sl_outer = 0 then 0 else round(dpk.soluong/cdd.sl_outer, 1) end as noofpack ,
										dpk.soluong, dpk.gia, dpk.thanhtien, cdd.l2, cdd.w2, cdd.h2, cdd.l1, cdd.w1, cdd.h1,
										dpk.mota_tienganh, dvt.ten_dvt, mdg.sl_outer as sl_outer_dg, 
										(select ten_dvt from md_donvitinh where md_donvitinh_id = mdg.dvt_outer) as dvt_outer, 
										case when cdd.sl_outer = 0 then 0 else (round((cdd.l2 * cdd.h2 * cdd.w2/1000000 * dpk.soluong/cdd.sl_outer), 3)) end as cbm,
										--(sp.trongluong*dpk.soluong) as nkg, 
										CAST(dpk.soluong as float) / CAST(mdg.sl_outer as float) * isnull(mdg.nw2, 0) as nkg,
	                                    CAST(dpk.soluong as float) / CAST(mdg.sl_outer as float) * isnull(mdg.gw2, 0) as gkg,
										cdd.sl_inner, cdd.sl_outer
								from c_dongpklinv dpk left join md_sanpham sp on dpk.md_sanpham_id = sp.md_sanpham_id
									left join c_dongnhapxuat cdnx on cdnx.c_dongnhapxuat_id = dpk.c_dongnhapxuat_id
									left join c_dongdonhang cdd on cdd.c_dongdonhang_id = cdnx.c_dongdonhang_id
									left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id 
									left join md_donggoi mdg on cdd.md_donggoi_id = mdg.md_donggoi_id
								where dpk.c_donhang_id =  '{0}'
									and c_packinginvoice_id =  '{1}'
									and cdnx.c_nhapxuat_id = '{2}') as tmp order by ma_sanpham asc", g_it["c_donhang_id"], c_pkivn_id, g_it["c_nhapxuat_id"]);

            DataTable dt_d = mdbc.GetData(sql_de);
            int total_row_d = dt_d.Rows.Count;
            //create detail group
            foreach (DataRow g_dt in dt_d.Rows)
            {
                r++;
                IRow ir = s1.CreateRow(r);
                ir.CreateCell(0).SetCellValue(g_dt["ma_sanpham"].ToString());
                ir.CreateCell(1).SetCellValue(g_dt["ma_sanpham_khach"].ToString());
                ir.CreateCell(2).SetCellValue(g_dt["mota_tienganh"].ToString());
                ir.CreateCell(3).SetCellValue(double.Parse(g_dt["sl_outer_dg"].ToString()));
                ir.CreateCell(4).SetCellValue(g_dt["dvt_outer"].ToString());
                //ir.CreateCell(5).SetCellValue(double.Parse(g_dt["noofpack"].ToString()));
				ir.CreateCell(5).CellFormula = String.Format("G{0}/D{0}",r+1);
                ir.CreateCell(6).SetCellValue(double.Parse(g_dt["soluong"].ToString()));
                ir.CreateCell(7).SetCellValue(g_dt["ten_dvt"].ToString() + "s");
                ir.CreateCell(8).SetCellValue(double.Parse(g_dt["nkg"].ToString()));
                ir.CreateCell(9).SetCellValue(double.Parse(g_dt["gkg"].ToString()));
                ir.CreateCell(10).SetCellValue(double.Parse(g_dt["cbm"].ToString()));
                ir.CreateCell(11).SetCellValue(double.Parse(g_dt["l2"].ToString()));
                ir.CreateCell(12).SetCellValue(double.Parse(g_dt["w2"].ToString()));
                ir.CreateCell(13).SetCellValue(double.Parse(g_dt["h2"].ToString()));

                //ir.CreateCell(14).SetCellValue(double.Parse(g_dt["noofpack"].ToString()));
				ir.CreateCell(14).CellFormula = String.Format("G{0}/D{0}",r);
                ir.CreateCell(15).SetCellValue(double.Parse(g_dt["soluong"].ToString()));
                ir.CreateCell(16).SetCellValue(double.Parse(g_dt["nkg"].ToString()));
                ir.CreateCell(17).SetCellValue(double.Parse(g_dt["gkg"].ToString()));
                ir.CreateCell(18).SetCellValue(double.Parse(g_dt["cbm"].ToString()));

                ir.GetCell(0).CellStyle = styleBorder12;
                ir.GetCell(1).CellStyle = styleBorder12;
                ir.GetCell(2).CellStyle = styleBorder12;
                ir.GetCell(3).CellStyle = styleNumber0Border12;
                ir.GetCell(4).CellStyle = styleBorder12;
                ir.GetCell(5).CellStyle = styleNumber0Border12i;
                ir.GetCell(6).CellStyle = styleNumber0Border12;
                ir.GetCell(7).CellStyle = styleBorder12;
                ir.GetCell(8).CellStyle = styleNumber0i00Border12;
                ir.GetCell(9).CellStyle = styleNumber0i00Border12;
                ir.GetCell(10).CellStyle = styleNumber0i00Border12;
                ir.GetCell(11).CellStyle = styleNumber0i00Border12;
                ir.GetCell(12).CellStyle = styleNumber0i00Border12;
                ir.GetCell(13).CellStyle = styleNumber0i00Border12;
            }
            r++;
            s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Total{0}:", count_group > 1 ? " (" + count.ToString() + ")" : ""));
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;


            // total no of pack
            s1.GetRow(r).CreateCell(5).CellFormula = String.Format("SUM(F{0}:F{1})", r - dt_d.Rows.Count + 1, r);
            s1.GetRow(r).GetCell(5).CellStyle = styleNumber012Bold;
            lstNoOfPack.Add(r + 1);

            // total quantity
            s1.GetRow(r).CreateCell(6).CellFormula = String.Format("SUM(G{0}:G{1})", r - dt_d.Rows.Count + 1, r);
            s1.GetRow(r).GetCell(6).CellStyle = styleNumber012Bold;
            lstQuantity.Add(r + 1);

            //total NW
            s1.GetRow(r).CreateCell(8).CellFormula = String.Format("SUM(I{0}:I{1})", r - dt_d.Rows.Count + 1, r);
            s1.GetRow(r).GetCell(8).CellStyle = styleNumber0i0012Bold;
            lstNW.Add(r + 1);

            // total GW
            s1.GetRow(r).CreateCell(9).CellFormula = String.Format("SUM(J{0}:J{1})", r - dt_d.Rows.Count + 1, r);
            s1.GetRow(r).GetCell(9).CellStyle = styleNumber0i0012Bold;
            lstGW.Add(r + 1);

            // total CBM
            s1.GetRow(r).CreateCell(10).CellFormula = String.Format("SUM(K{0}:K{1})", r - dt_d.Rows.Count + 1, r);
            s1.GetRow(r).GetCell(10).CellStyle = styleNumber0i0012Bold;
            lstCBM.Add(r + 1);
            s1.GetRow(r).HeightInPoints = 22;

            count++;
            r++;
        }
        string fNoOfPack = "", fQuantity = "", fNW = "", fGW = "", fCBM = "";

        foreach (int item in lstNoOfPack)
        {
            fNoOfPack += String.Format("+ F{0} ", item);
        }
        fNoOfPack = fNoOfPack.Substring(1);



        foreach (int item in lstQuantity)
        {
            fQuantity += String.Format("+ G{0} ", item);
        }
        fQuantity = fQuantity.Substring(1);



        foreach (int item in lstNW)
        {
            fNW += String.Format("+ I{0} ", item);
        }
        fNW = fNW.Substring(1);

        foreach (int item in lstGW)
        {
            fGW += String.Format("+ J{0} ", item);
        }
        fGW = fGW.Substring(1);

        foreach (int item in lstCBM)
        {
            fCBM += String.Format("+ K{0} ", item);
        }
        fCBM = fCBM.Substring(1);

        String strCount = "";
        for (int i = 1; i < count; i++)
        {
            strCount += " + " + i;
        }
        strCount = strCount.Substring(2);


        s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Total{0}:", count_group > 1 ? " (" + strCount + ")" : ""));
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(5).SetCellFormula(fNoOfPack);
        s1.GetRow(r).GetCell(5).CellStyle = styleNumber012Bold;

        s1.GetRow(r).CreateCell(6).SetCellFormula(fQuantity);
        s1.GetRow(r).GetCell(6).CellStyle = styleNumber012Bold;

        s1.GetRow(r).CreateCell(8).SetCellFormula(fNW);
        s1.GetRow(r).GetCell(8).CellStyle = styleNumber0i0012Bold;

        s1.GetRow(r).CreateCell(9).SetCellFormula(fGW);
        s1.GetRow(r).GetCell(9).CellStyle = styleNumber0i0012Bold;

        s1.GetRow(r).CreateCell(10).SetCellFormula(fCBM);
        s1.GetRow(r).GetCell(10).CellStyle = styleNumber0i0012Bold;
        s1.GetRow(r).HeightInPoints = 22;


        s1.GetRow(r).HeightInPoints = 0;

        if (count_group > 1)
        {
            s1.GetRow(r).HeightInPoints = 22;
        }

        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("Gross weight:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;

        s1.GetRow(r).CreateCell(1).SetCellFormula(String.Format("J{0}", r));
        s1.GetRow(r).GetCell(1).CellStyle = styleNumber0i0012;

        s1.GetRow(r).CreateCell(2).SetCellValue("kgs");
        s1.GetRow(r).GetCell(2).CellStyle = style12;

        s1.GetRow(r).HeightInPoints = 22;

        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("Measurement:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;

        s1.GetRow(r).CreateCell(1).SetCellFormula(String.Format("K{0}", r - 1));
        s1.GetRow(r).GetCell(1).CellStyle = styleNumber0i0012;

        s1.GetRow(r).CreateCell(2).SetCellValue("cbm");
        s1.GetRow(r).GetCell(2).CellStyle = style12;

        s1.GetRow(r).HeightInPoints = 22;

        r++;
        s1.CreateRow(r).CreateCell(3).SetCellValue(noidungNCC1);
        s1.AddMergedRegion(new CellRangeAddress(r, r, 3, 7));
        s1.GetRow(r).GetCell(3).CellStyle = styleCenter16Bold;
        s1.GetRow(r).HeightInPoints = 22;

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
            }
            var dic = new Dictionary<string, string>();
            dic["link"] = link;
            dic["name"] = "PKL";
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
