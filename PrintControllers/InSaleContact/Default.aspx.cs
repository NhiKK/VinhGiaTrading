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
using System.Globalization;

public partial class PrintControllers_InSaleContact_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        String c_pkivn_id = Request.QueryString["c_packinglist_id"];
        String select = String.Format(@"select cpk.so_pkl, cpk.etd, dt.ten_dtkd, dt.diachi, dt.tel, dt.fax,
		                                            cpk.mv, cb.ten_cangbien, cpk.noiden, cpk.blno, cpk.commodity, DATEADD(DAY, 20, cpk.ngaylap) as ngaylap
                                            from c_packinginvoice cpk, md_doitackinhdoanh dt, md_cangbien cb
                                            where cpk.noidi = cb.md_cangbien_id
	                                            and cpk.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
	                                            and cpk.c_packinginvoice_id ='" + c_pkivn_id + "'"); //c_pkivn_id

        String selNganHang = String.Format(@"select 
                                                    ngh.thongtin 
                                                from 
                                                    md_nganhang ngh, c_donhang dh, c_dongpklinv dpkl
                                                where
                                                    ngh.md_nganhang_id = dh.md_nganhang_id
                                                    AND dpkl.c_donhang_id = dh.c_donhang_id
                                                    AND dpkl.c_packinginvoice_id = '{0}'", c_pkivn_id);
        String nganhang = (String)mdbc.ExecuteScalar(selNganHang);

        String selPaymentterm = String.Format(@"select 
                                                    pmt.ten_paymentterm
                                                from 
                                                    md_paymentterm pmt, c_donhang dh, c_dongpklinv dpkl
                                                where
                                                    pmt.md_paymentterm_id = dh.md_paymentterm_id
                                                    AND dpkl.c_donhang_id = dh.c_donhang_id
                                                    AND dpkl.c_packinginvoice_id = '{0}'", c_pkivn_id);
        String paymentterm = (String)mdbc.ExecuteScalar(selPaymentterm);
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


        String selNgayLapDH = String.Format(@"select top(1) dh.ngaylap
                                            from 
                                                c_donhang dh, c_dongpklinv dpkl
                                            where dh.c_donhang_id = dpkl.c_donhang_id
                                                AND dpkl.c_packinginvoice_id = N'{0}' order by dh.ngaylap asc", c_pkivn_id);

        DateTime ngayLapDH = (DateTime)mdbc.ExecuteScalar(selNgayLapDH);
        string noidungNCC = "";
        DataTable dt = mdbc.GetData(select);

        if (dt.Rows.Count != 0)
        {
            HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt, chungTuDH, paymentterm, nganhang, ngayLapDH, noidungNCC);
            String saveAsFileName = String.Format("SaleContact-{0}.xls", DateTime.Now);
            this.SaveFile(hssfworkbook, saveAsFileName);
        }
        else
        {
            Response.Write("<h3>PackingList/Invoice không có dữ liệu</h3>");
        }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex.Message));
        //}
    }

    public HSSFWorkbook CreateWorkBookPO(DataTable dt, String chungTuDH, String paymentterm, String nganhang, DateTime ngayLapDH, String noidungNCC)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        String c_pkivn_id = Request.QueryString["c_packinglist_id"];
        var pdkInvoice = db.c_packinginvoices.Single(inv => inv.c_packinginvoice_id.Equals(c_pkivn_id));
        string nccHeader = "VINH GIA", nccDetail = "VINH GIA";
        if (pdkInvoice.hangcua == "VG")
            noidungNCC = "VINH GIA COMPANY LIMITED \nNorthern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam\nTel: (84-235) 3567393\nFax: (84-235) 3567494";
        else
        {
            nccHeader = "Hoàng Huy Hưng";
            nccDetail = "HHH";
            noidungNCC = "HOANG HUY HUNG COMPANY LIMITED \nTam Hiep 2 Urban residential area,  Nui Thanh Commune, Danang City, Viet Nam\nTel: (84-235) 3567998";
        }

        var lstPO = new List<int>();

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("SaleContact");

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



        s1.SetColumnWidth(0, 2000);
        s1.SetColumnWidth(1, 5000);
        s1.SetColumnWidth(2, 3500);
        s1.SetColumnWidth(3, 6500);
        s1.SetColumnWidth(5, 3000);
        s1.SetColumnWidth(7, 3500);
        s1.SetColumnWidth(11, 0);


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

        s1.CreateRow(0).CreateCell(0).SetCellValue("SALE CONTRACT");
        s1.GetRow(0).GetCell(0).CellStyle = styleCenter22Bold;
        s1.GetRow(0).HeightInPoints = 40;
        s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));

        s1.CreateRow(1).CreateCell(4).SetCellValue("No.:");
        s1.GetRow(1).GetCell(4).CellStyle = style12Bold;
        s1.GetRow(1).HeightInPoints = 22;

        s1.GetRow(1).CreateCell(5).SetCellValue(chungTuDH);
        s1.GetRow(1).GetCell(5).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(1, 1, 5, 7));

        s1.CreateRow(2).CreateCell(4).SetCellValue("Date:");
        s1.GetRow(2).GetCell(4).CellStyle = style12Bold;
        s1.GetRow(2).HeightInPoints = 21;

        s1.GetRow(2).CreateCell(5).SetCellValue(ngayLapDH.ToString("dd-MMM-yyyy"));
        s1.GetRow(2).GetCell(5).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(2, 2, 5, 7));

        s1.CreateRow(3).CreateCell(0).SetCellValue("The Seller:");
        s1.GetRow(3).GetCell(0).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 1));
        s1.GetRow(3).HeightInPoints = 85;

        HSSFRichTextString richSeller = new HSSFRichTextString(noidungNCC);
        s1.GetRow(3).CreateCell(2).SetCellValue(richSeller);
        s1.GetRow(3).GetCell(2).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(3, 3, 2, 7));


        s1.CreateRow(5).CreateCell(0).SetCellValue("The Buyer:");
        s1.GetRow(5).GetCell(0).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(5, 5, 0, 1));


        string dc_kh = "";
        dc_kh = dt.Rows[0]["ten_dtkd"].ToString() + "\n" + dt.Rows[0]["diachi"].ToString() + "\nTel:" + dt.Rows[0]["tel"].ToString() + "\nFax:" + dt.Rows[0]["fax"].ToString();
        HSSFRichTextString rich = new HSSFRichTextString(dc_kh);
        s1.GetRow(5).CreateCell(2).SetCellValue(rich);
        s1.GetRow(5).GetCell(2).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(5, 5, 2, 7));

        s1.GetRow(5).HeightInPoints = 95;

        s1.CreateRow(6).CreateCell(0).SetCellValue("The Payer:");
        s1.GetRow(6).GetCell(0).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(6, 6, 0, 1));
        s1.GetRow(6).HeightInPoints = 21;

        s1.CreateRow(7).CreateCell(0).SetCellValue(nccHeader + " Company agrees to sale and the Buyer agrees to buy the goods under terms and conditions as follows:");
        s1.GetRow(7).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(7, 7, 0, 7));
        s1.GetRow(7).HeightInPoints = 33;

        s1.CreateRow(8).CreateCell(0).SetCellValue("Customer's order no.:");
        s1.GetRow(8).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(8, 8, 0, 7));
        s1.GetRow(8).HeightInPoints = 21;

        s1.CreateRow(9).CreateCell(0).SetCellValue("Item nos., description, packing, quantity, unit price and amount:");
        s1.GetRow(9).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(9, 9, 0, 7));
        s1.GetRow(9).HeightInPoints = 21;

        //Table
        s1.CreateRow(10).CreateCell(0).SetCellValue("No");
        s1.GetRow(10).GetCell(0).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(10).CreateCell(1).SetCellValue(nccDetail + " Item No.");
        s1.GetRow(10).GetCell(1).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(10).CreateCell(2).SetCellValue("Customer Item No.");
        s1.GetRow(10).GetCell(2).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(10).CreateCell(3).SetCellValue("Description of goods");
        s1.GetRow(10).GetCell(3).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(10).CreateCell(4).SetCellValue("Quantity pcs,sets"); // 2 column
        s1.GetRow(10).GetCell(4).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(10).CreateCell(5).SetCellValue("");
        s1.GetRow(10).GetCell(5).CellStyle = styleCenterBorder12Bold;

        s1.AddMergedRegion(new CellRangeAddress(10, 10, 4, 5));

        s1.GetRow(10).CreateCell(6).SetCellValue("FOB price (USD)");
        s1.GetRow(10).GetCell(6).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(10).CreateCell(7).SetCellValue("Amount (USD)");
        s1.GetRow(10).GetCell(7).CellStyle = styleCenterBorder12Bold;

        string sql_group = string.Format(@"
            select distinct 
                dpk.c_donhang_id
                , dh.sochungtu as sodh
                , po.discountPO as discount
                , po.hehang_value as discount_hehang_value
                , isnull(dh.cpdg_vuotchuan, 0) as cpdg_vuotchuan
                , cnx.container
                , cnx.soseal
                , cb.ten_cangbien
                , cnx.c_nhapxuat_id
            from c_dongpklinv dpk
                , c_packinginvoice cpk
                , c_donhang dh outer APPLY (select * from dbo.ftbl_getInfoPO(dh.phanbodiscount, dh.discount, null, dh.discount_hehang_value)) as po
                , c_dongnhapxuat dnx
                , c_nhapxuat cnx
                , md_cangbien cb
            where dpk.c_packinginvoice_id = '{0}'
                and dpk.c_donhang_id = dh.c_donhang_id
		        and dpk.c_dongnhapxuat_id = dnx.c_dongnhapxuat_id
		        and dnx.c_nhapxuat_id = cnx.c_nhapxuat_id
                and dpk.c_packinginvoice_id = cpk.c_packinginvoice_id
		        and cpk.noidi = cb.md_cangbien_id"
        , c_pkivn_id);

        DataTable dt_g = mdbc.GetData(sql_group);

        string dh_ = string.Format(@"
            select distinct 
                c_donhang_id
                , sodh
                , discount_hehang_value
                , cpdg_vuotchuan
                , container
                , soseal
                , ten_cangbien
                , convert(decimal(18,2), discount) as discount
                , discount as discount1
                , c_nhapxuat_id 
            from 
                ({0}) as tmp 
            order by 
                container asc, sodh asc"
        , sql_group);

        DataTable data_dh = mdbc.GetData(dh_);

        int r = 11, g_line = 0;
        double sumDiscount = 0;
        double sumFee = 0;
        int countDonHang = data_dh.Rows.Count;
        int count = 1;
        List<int> lstQuantity = new List<int>();
        List<int> lstTotalSalary = new List<int>();
        List<int> lstDiscount = new List<int>();
        String funcDiscount = "";
        int rDiscount = 0, rPackingFee = 0;
        foreach (DataRow g_it in data_dh.Rows)
        {
            string discount_hehang_value = g_it["discount_hehang_value"].ToString();
            var nnl = string.IsNullOrEmpty(discount_hehang_value) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(discount_hehang_value);
            s1.CreateRow(r).CreateCell(0).SetCellValue("P/I No.:" + g_it["sodh"].ToString());
            if (g_line == 0)
            {
                s1.GetRow(r).CreateCell(4).SetCellValue("FOB " + g_it["ten_cangbien"].ToString());
            }
            else
            {
                s1.GetRow(r).CreateCell(4).SetCellValue("");
            }
            s1.GetRow(r).GetCell(0).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(1).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(2).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(3).CellStyle = styleBorder12;
            s1.GetRow(r).GetCell(4).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(5).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(6).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(7).CellStyle = styleBorder12;
            s1.GetRow(r).HeightInPoints = 22;

            s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 3));
            s1.AddMergedRegion(new CellRangeAddress(r, r, 4, 7));

            g_line++;

            string sql_de = string.Format(@"select sp.ma_sanpham, cdd.ma_sanpham_khach ,dpk.soluong, dpk.gia, dpk.thanhtien,
								dpk.mota_tienganh, dvt.ten_dvt
							from 
								c_dongpklinv dpk left join md_sanpham sp on dpk.md_sanpham_id = sp.md_sanpham_id
								left join c_dongnhapxuat cdnx on dpk.c_dongnhapxuat_id = cdnx.c_dongnhapxuat_id
								left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
								left join c_dongdonhang cdd on cdd.c_dongdonhang_id = cdnx.c_dongdonhang_id
							where 
								dpk.c_donhang_id = '{0}'
								and c_packinginvoice_id = '{1}'
								and cdnx.c_nhapxuat_id = '{2}'
							order by sp.ma_sanpham asc", g_it["c_donhang_id"], c_pkivn_id, g_it["c_nhapxuat_id"]);
            DataTable dt_d = mdbc.GetData(sql_de);
            int total_row_d = dt_d.Rows.Count;
            int rowNum = 1;

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
                ir.CreateCell(0).SetCellValue(rowNum);
                ir.GetCell(0).CellStyle = styleBorder12;

                ir.CreateCell(1).SetCellValue(g_dt["ma_sanpham"].ToString());
                ir.GetCell(1).CellStyle = styleBorder12;

                ir.CreateCell(2).SetCellValue(g_dt["ma_sanpham_khach"].ToString());
                ir.GetCell(2).CellStyle = styleBorder12;

                ir.CreateCell(3).SetCellValue(g_dt["mota_tienganh"].ToString());
                ir.GetCell(3).CellStyle = styleBorder12;

                ir.CreateCell(4).SetCellValue(double.Parse(g_dt["soluong"].ToString()));
                ir.GetCell(4).CellStyle = styleNumber0Border12;

                ir.CreateCell(5).SetCellValue(g_dt["ten_dvt"].ToString() + "s");
                ir.GetCell(5).CellStyle = styleBorder12;

                ir.CreateCell(6).SetCellValue(double.Parse(g_dt["gia"].ToString()));
                ir.GetCell(6).CellStyle = styleNumber0i00Border12;

                ir.CreateCell(7).CellFormula = String.Format("G{0}*E{0}", r + 1);
                //ir.CreateCell(7).SetCellValue(double.Parse(g_dt["thanhtien"].ToString()));
                ir.GetCell(7).CellStyle = styleNumber0i00Border12;

                //ir.CreateCell(11).SetCellValue(double.Parse(g_it["discount"].ToString()));
                rowNum++;

                if ((chungloai != chungloaiTT & i < dt_d.Rows.Count - 1) | i == dt_d.Rows.Count - 1)
                {
                    if (nnl != null)
                    {
                        string gthh = nnl == null ? "0" : nnl.Where(s => s["hehang"].ToString() == chungloai).Select(s => s["giatri"].ToString()).FirstOrDefault();
                        var rowHeight = gthh == "0" ? 0 : 20;
                        r++;
                        var rowToTalHeHang = s1.CreateRow(r);
                        rowToTalHeHang.HeightInPoints = rowHeight;
                        rowToTalHeHang.CreateCell(3).SetCellValue(string.Format("Sub Total ({0}):", lstHH.Count + 1));
                        rowToTalHeHang.GetCell(3).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 2));
                        string fmlSum = string.Format(@"SUM(VNN{0}:VNN{1})", startRowHH + 1, r);
                        rowToTalHeHang.CreateCell(4).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "E"));
                        rowToTalHeHang.GetCell(4).CellStyle = styleNumber012Bold;
                        rowToTalHeHang.CreateCell(5).SetCellValue("pcs/sets");
                        rowToTalHeHang.GetCell(5).CellStyle = style12Bold;
                        rowToTalHeHang.CreateCell(7).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "H"));
                        rowToTalHeHang.GetCell(7).CellStyle = styleNumber0i0012Bold;
                        r++;

                        var rowDisHeHang = s1.CreateRow(r);
                        rowDisHeHang.HeightInPoints = rowHeight;
                        rowDisHeHang.CreateCell(3).SetCellValue(string.Format(@"Discount ({0}%):", gthh));
                        rowDisHeHang.GetCell(3).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 2));
                        rowDisHeHang.CreateCell(7).CellFormula = string.Format("ROUND(H{0}*{1}/100, 2)", r, gthh);
                        rowDisHeHang.GetCell(7).CellStyle = styleNumber0i0012Bold;
                        r++;

                        var rowTotalHeHang = s1.CreateRow(r);
                        rowTotalHeHang.HeightInPoints = rowHeight;
                        rowTotalHeHang.CreateCell(3).SetCellValue(string.Format("Total ({0}):", lstHH.Count + 1));
                        rowTotalHeHang.GetCell(3).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 2));
                        rowTotalHeHang.CreateCell(4).CellFormula = string.Format("E{0}", r - 1);
                        rowTotalHeHang.GetCell(4).CellStyle = styleNumber012Bold;
                        rowTotalHeHang.CreateCell(7).CellFormula = string.Format("H{0} - H{1}", r - 1, r);
                        rowTotalHeHang.GetCell(7).CellStyle = styleNumber0i0012Bold;

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
                    sumQuantityPO += string.Format(@"E{0}+", rowHH + 1);
                    sumSubToTalPO += string.Format(@"H{0}+", rowHH + 1);
                }
                sumQuantityPO += "0";
                sumSubToTalPO += "0";
            }
            else
            {
                sumQuantityPO += string.Format(@"SUM(E{0}:E{1})", startRowHH + 1, r + 1);
                sumSubToTalPO += string.Format(@"SUM(H{0}:H{1})", startRowHH + 1, r + 1);
            }
            r++;
            s1.CreateRow(r).CreateCell(3).SetCellValue(String.Format("Sub Total:", countDonHang > 1 ? " (" + count.ToString() + ")" : ""));
            s1.GetRow(r).GetCell(3).CellStyle = style12Bold;

            // total quantity
            s1.GetRow(r).CreateCell(4).CellFormula = sumQuantityPO;
            s1.GetRow(r).GetCell(4).CellStyle = styleNumber012Bold;
            lstQuantity.Add(r + 1);

            s1.GetRow(r).CreateCell(5).SetCellValue("pcs/sets");
            s1.GetRow(r).GetCell(5).CellStyle = style12Bold;

            //total salary
            s1.GetRow(r).CreateCell(7).CellFormula = sumSubToTalPO;
            s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;
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
            s1.CreateRow(r).CreateCell(3).SetCellValue(String.Format("Discount ({0}%):", discountPO));
            s1.GetRow(r).GetCell(3).CellStyle = style12Bold;

            funcDiscount = "ROUND(H{0}*L{0}/100,2)";

            rDiscount = r;
            s1.GetRow(r).CreateCell(7).CellFormula = String.Format(funcDiscount, r);
            s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;

            //s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
            lstDiscount.Add(r + 1);

            sumDiscount += double.Parse(discountPO);

            if (double.Parse(discountPO) == 0) s1.GetRow(r).HeightInPoints = 0;
            //Extra Packing Fee
            r++;
            var packingfeePO = double.Parse(g_it["cpdg_vuotchuan"].ToString());
            s1.CreateRow(r).CreateCell(3).SetCellValue(String.Format("Extra packing fees:"));
            s1.GetRow(r).GetCell(3).CellStyle = style12Bold;
            rPackingFee = r;
            s1.GetRow(r).CreateCell(7).SetCellValue(packingfeePO);
            s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;
            if (packingfeePO == 0) s1.GetRow(r).HeightInPoints = 0;
            sumFee += packingfeePO;
            //Total
            r++;
            s1.CreateRow(r).CreateCell(3).SetCellValue(String.Format("Total{0}:", countDonHang > 1 ? " (" + count.ToString() + ")" : ""));
            s1.GetRow(r).GetCell(3).CellStyle = style12Bold;

            s1.GetRow(r).CreateCell(7).CellFormula = String.Format("H{0}- H{1} + H{2}", r - 2, r - 1, r);
            s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;

            // total quantity
            s1.GetRow(r).CreateCell(4).CellFormula = String.Format("E{0}", r - 2);
            s1.GetRow(r).GetCell(4).CellStyle = styleNumber012Bold;

            r++;
            count++;
            lstPO.Add(r);
        }

        s1.CreateRow(r).CreateCell(3).SetCellValue("(+)" + pdkInvoice.diengiai_cong + ":");
        s1.GetRow(r).GetCell(3).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(7).SetCellValue(double.Parse(pdkInvoice.giatri_cong.Value.ToString()));
        s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;

        if (pdkInvoice.giatri_cong.Value == 0) s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(3).SetCellValue("(-)" + pdkInvoice.diengiai_tru + ":");
        s1.GetRow(r).GetCell(3).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(7).SetCellValue(double.Parse(pdkInvoice.giatri_tru.Value.ToString()));
        s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatri_tru.Value == 0) s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(3).SetCellValue(pdkInvoice.diengiaicong_po + ":");
        s1.GetRow(r).GetCell(3).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(7).SetCellValue(double.Parse(pdkInvoice.giatricong_po.Value.ToString()));
        s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatricong_po.Value == 0) s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(3).SetCellValue(pdkInvoice.diengiaitru_po + ":");
        s1.GetRow(r).GetCell(3).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(7).SetCellValue(double.Parse(pdkInvoice.giatritru_po.Value.ToString()));
        s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatritru_po.Value == 0) s1.GetRow(r).HeightInPoints = 0;


        String strCount = "";
        for (int i = 1; i < count; i++)
        {
            strCount += " + " + i;
        }
        strCount = strCount.Substring(2);
        //Packing Fee
        r++;
        s1.CreateRow(r).CreateCell(3).SetCellValue(String.Format("Extra packing fees{0}", countDonHang > 1 ? " (" + strCount + "):" : ":"));
        s1.GetRow(r).GetCell(3).CellStyle = style12Bold;
        string sumToTalPakingFee = "";
        for (var iPO = 0; iPO < lstPO.Count; iPO++)
        {
            var row = lstPO[iPO];
            sumToTalPakingFee += string.Format(@"H{0}+", row - 1);
        }
        sumToTalPakingFee += "0";
        s1.GetRow(r).CreateCell(7).CellFormula = string.Format("{0}", sumToTalPakingFee);
        s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;
        s1.GetRow(r).HeightInPoints = 0;
        if (pdkInvoice.cpdg_vuotchuan.GetValueOrDefault(0) == 0) s1.GetRow(r).HeightInPoints = 0;
        //Total
        r++;
        s1.CreateRow(r).CreateCell(3).SetCellValue(String.Format("Total {0}:", countDonHang > 1 ? " (" + strCount + ")" : ""));
        s1.GetRow(r).GetCell(3).CellStyle = style12Bold;

        // total quantity
        string fQuantity = "", fSalary = "", fDiscount = "";

        foreach (int item in lstQuantity)
        {
            fQuantity += String.Format("+ E{0} ", item);
        }
        fQuantity = fQuantity.Substring(1);

        //total discount
        foreach (int item in lstDiscount)
        {
            fDiscount += String.Format("+ H{0} ", item);
        }
        fDiscount = fDiscount.Substring(1);


        //total salary
        foreach (int item in lstTotalSalary)
        {
            fSalary += String.Format("+ H{0} ", item);
        }
        fSalary = fSalary.Substring(1);

        s1.GetRow(r).CreateCell(4).SetCellFormula(fQuantity);
        s1.GetRow(r).GetCell(4).CellStyle = styleNumber012Bold;

        //s1.GetRow(r).CreateCell(6).SetCellFormula(String.Format("({0}) - ({1}) + G{2} - G{3} + G{4} - G{5}", fSalary, fDiscount, r - 3, r - 2, r - 1, r));
        string sumToTalPO = "";
        for (var iPO = 0; iPO < lstPO.Count; iPO++)
        {
            var row = lstPO[iPO];
            sumToTalPO += string.Format(@"H{0}+", row);
        }
        sumToTalPO += "0";
        s1.GetRow(r).CreateCell(7).CellFormula = string.Format("{0} + H{1} - H{2} + H{3} - H{4}", sumToTalPO, r - 4, r - 3, r - 2, r - 1);
        s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;



        HSSFFormulaEvaluator formula = new HSSFFormulaEvaluator(hssfworkbook);
        CellValue cValue = formula.Evaluate(s1.GetRow(r).GetCell(7));
        double doctien = cValue.NumberValue;

        s1.GetRow(rDiscount).GetCell(7).SetCellFormula(string.Format(funcDiscount, rDiscount));

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

        int j = m.LastIndexOf("and");

        if (m.Contains("Cents") | m.Contains("Cent"))
        {
            m = m.Replace("Cents", "").Replace("Cent", "");
            m = m.Insert(j + 4, "cents ");
        }
        else
        {
            m = m.Replace("Cents", "").Replace("Cent", "");
        }

        s1.CreateRow(r).CreateCell(0).SetCellValue("Say: USD " + m);

        s1.GetRow(r).GetCell(0).CellStyle = styleRight12Bold;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 7));
        s1.GetRow(r).HeightInPoints = 22;

        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("(10% more or less in quantity and amount are acceptable).");
        s1.GetRow(r).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 7));
        s1.GetRow(r).HeightInPoints = 22;

        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("Port of loading:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));

        s1.GetRow(r).CreateCell(2).SetCellValue(dt.Rows[0]["ten_cangbien"].ToString());
        s1.GetRow(r).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 2, 7));
        s1.GetRow(r).HeightInPoints = 22;

        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("Port of discharge:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));

        s1.GetRow(r).CreateCell(2).SetCellValue(dt.Rows[0]["noiden"].ToString());
        s1.GetRow(r).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 2, 7));
        s1.GetRow(r).HeightInPoints = 22;

        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("Shipment Time:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));

        s1.GetRow(r).CreateCell(2).SetCellValue(DateTime.Parse(dt.Rows[0]["ngaylap"].ToString()).ToString("dd-MMM-yyyy"));
        s1.GetRow(r).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 2, 7));
        s1.GetRow(r).HeightInPoints = 22;

        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("Payment term:");
        s1.GetRow(r).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));

        s1.GetRow(r).CreateCell(2).SetCellValue("By " + paymentterm);
        s1.GetRow(r).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 2, 7));
        s1.GetRow(r).HeightInPoints = 22;

        //-----//
        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("Bank Details:");
        s1.GetRow(r).GetCell(0).CellStyle = style12Top;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));

        string nd_dong1Bank = "", nd_dong2Bank = "", nd_dong3Bank = "", nd_dong4Bank = "", nd_dong5Bank = "", nd_dong6Bank = "";

        if (pdkInvoice.hangcua == "VG")
        {
            nd_dong1Bank = "In favor of: VINH GIA COMPANY LIMITED";
            nd_dong2Bank = "Account No.: 046.137.3860862";
            nd_dong3Bank = "At bank: JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIET NAM - TAN BINH DUONG BRANCH,";
            nd_dong4Bank = "NO 16, LE TRONG TAN STREET, BINH DUONG 2 QUARTER, DI AN WARD, HO CHI MINH CITY, VIET NAM.";
            nd_dong5Bank = "Tel: (84 - 274) 3792 158 - Fax: (84 - 274) 3793 970,";
            nd_dong6Bank = "Swift code: BFTVVNVX";
        }
        else
        {
            nd_dong1Bank = "In favor of: HOANG HUY HUNG COMPANY LIMITED";
            nd_dong2Bank = "Account No.:1021045695";
            nd_dong3Bank = "At bank: JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIETNAM";
            nd_dong4Bank = "35 TRAN HUNG DAO STREET, BAN THACH WARD, DA NANG CITY, VIET NAM";
            nd_dong5Bank = "";
            nd_dong6Bank = "SWIFT CODE: BFTVVNVX065";
        }

        var dong1Bank = nd_dong1Bank;
        var dong2Bank = nd_dong2Bank;
        var dong3Bank = nd_dong3Bank;
        var dong4Bank = nd_dong4Bank;
        var dong5Bank = nd_dong5Bank;
        var dong6Bank = nd_dong6Bank;

        var rtxtBank = new HSSFRichTextString(
            dong1Bank + "\n" +
            dong2Bank + "\n" +
            dong3Bank + "\n" +
            dong4Bank + "\n" +
            dong5Bank + "\n" +
            dong6Bank
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
        rtxtBank.ApplyFont(startBank, startBank + dong2Bank.Length, bold12Font);
        //--3
        startBank += dong2Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong3Bank.Length, bold12Font);
        //--4
        startBank += dong3Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong4Bank.Length, normal12Font);
        //--5
        startBank += dong4Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong5Bank.Length, normal12Font);
        //--6
        startBank += dong5Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong6Bank.Length, bold12Font);

        s1.GetRow(r).CreateCell(2).SetCellValue(rtxtBank);
        s1.GetRow(r).GetCell(2).CellStyle.WrapText = true;
        s1.GetRow(r).GetCell(2).CellStyle.VerticalAlignment = VerticalAlignment.Bottom;
        s1.GetRow(r).HeightInPoints = 118;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 2, 7));
        //#----//

        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue("This agreement comes into effect from the signing date.");
        s1.GetRow(r).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 7));
        s1.GetRow(r).HeightInPoints = 22;


        r++;
        s1.CreateRow(r).CreateCell(0).SetCellValue(@"THE BUYER");
        s1.GetRow(r).GetCell(0).CellStyle = styleCenter12Bold;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 2));

        s1.GetRow(r).CreateCell(5).SetCellValue(@"THE SELLER");
        s1.GetRow(r).GetCell(5).CellStyle = styleCenter12Bold;
        s1.AddMergedRegion(new CellRangeAddress(r, r, 5, 7));

        s1.GetRow(r).HeightInPoints = 22;

        r++;
        var link = "";
        if (pdkInvoice.hangcua == "VG")
            link = ExcuteSignalRStatic.mapPathSignalR("~/images/more/ckcdct.jpg");
        else
            link = ExcuteSignalRStatic.mapPathSignalR("~/images/more/hhh.jpg");

        if (File.Exists(link))
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(link);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            IDrawing patriarch = s1.CreateDrawingPatriarch();
            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 5, r + 1, 5, r + 1);
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
            dic["name"] = "SaleContact";
            Response.Clear();
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            Response.End();
        }
    }

    public IFont CreateFontSize(HSSFWorkbook hsswb, short size)
    {
        IFont font = hsswb.CreateFont();
        font.FontHeightInPoints = size;
        return font;
    }
}
