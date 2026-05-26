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

public partial class PrintControllers_InToKhaiHQ_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        String c_pkivn_id = Request.QueryString["c_packinglist_id"];
        String select = String.Format(@"
            select 
                cpk.so_pkl, 
                cpk.ngaylap, 
                dt.ten_dtkd, 
                dt.diachi, 
                dt.tel, 
                dt.fax,
		        cpk.mv, 
                cb.ten_cangbien, 
                cpk.noiden, 
                cpk.blno, 
                cpk.commodity, 
                cpk.etd, 
                cpk.commodityvn,  
                diengiaicong_po, 
                giatricong_po, 
                diengiaitru_po, 
                giatritru_po,
                isnull(cpk.cpdg_vuotchuan, 0) as cpdg_vuotchuan
            from c_packinginvoice cpk, md_doitackinhdoanh dt, md_cangbien cb
            where cpk.noidi = cb.md_cangbien_id
	            and cpk.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
	            and cpk.c_packinginvoice_id ='" + c_pkivn_id + "'"); //c_pkivn_id

        DataTable dt = mdbc.GetData(select);

        if (dt.Rows.Count != 0)
        {
            HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
            String saveAsFileName = String.Format("ToKhaiHaiQuan-{0}.xls", DateTime.Now);
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

    public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        String c_pkivn_id = Request.QueryString["c_packinglist_id"];
        var pdkInvoice = db.c_packinginvoices.Single(inv => inv.c_packinginvoice_id.Equals(c_pkivn_id));

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("TKHQ");

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

        ICellStyle style22Bold = hssfworkbook.CreateCellStyle();
        style22Bold.SetFont(font22Bold);
        style22Bold.VerticalAlignment = VerticalAlignment.Top;
        style22Bold.WrapText = true;

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

        s1.SetColumnWidth(5, 4000);
        s1.SetColumnWidth(6, 0);
        s1.SetColumnWidth(7, 0);
        s1.SetColumnWidth(8, 0);
        s1.SetColumnWidth(9, 0);
        s1.SetColumnWidth(10, 0);
        s1.SetColumnWidth(11, 0);
        s1.SetColumnWidth(14, 0);


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



        s1.CreateRow(0).CreateCell(0).SetCellValue("HẢI QUAN VIỆT NAM");
        s1.GetRow(0).GetCell(0).CellStyle = style22Bold;
        s1.GetRow(0).HeightInPoints = 45;
        s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 8));

        s1.CreateRow(1).CreateCell(0).SetCellValue("DANH MỤC KÈM THEO TỜ KHAI HÀNG HOÁ XUẤT KHẨU");
        s1.GetRow(1).GetCell(0).CellStyle = style12;
        s1.GetRow(1).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 11));

        s1.CreateRow(2).CreateCell(0).SetCellValue("TKHQ SỐ:…………………/KV…………………….NGÀY……..THÁNG……….NĂM………….");
        s1.GetRow(2).GetCell(0).CellStyle = style12;
        s1.GetRow(2).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 8));

        s1.CreateRow(3).CreateCell(4).SetCellValue("No.:");
        s1.GetRow(3).GetCell(4).CellStyle = style12Bold;

        s1.GetRow(3).CreateCell(5).SetCellValue(dt.Rows[0]["so_pkl"].ToString());
        s1.GetRow(3).GetCell(5).CellStyle = style12Bold;
        s1.GetRow(3).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(3, 3, 5, 8));

        s1.CreateRow(4).CreateCell(4).SetCellValue("Date");
        s1.GetRow(4).GetCell(4).CellStyle = style12Bold;
        s1.GetRow(4).HeightInPoints = 22;

        s1.GetRow(4).CreateCell(5).SetCellValue(DateTime.Now.ToString("dd-MMM-yyyy"));
        s1.GetRow(4).GetCell(5).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(4, 4, 5, 8));

        s1.CreateRow(5).CreateCell(0).SetCellValue("Người mua:");
        s1.GetRow(5).GetCell(0).CellStyle = style12TopBold;

        string dc_kh = "";
        dc_kh = dt.Rows[0]["ten_dtkd"].ToString() + "\nAddress:" + dt.Rows[0]["diachi"].ToString() + "\nTel:" + dt.Rows[0]["tel"].ToString() + "\nFax:" + dt.Rows[0]["fax"].ToString();
        HSSFRichTextString rich = new HSSFRichTextString(dc_kh);
        s1.GetRow(5).CreateCell(2).SetCellValue(rich);
        s1.GetRow(5).GetCell(2).CellStyle = style12TopBold;
        s1.GetRow(5).HeightInPoints = 95;
        s1.AddMergedRegion(new CellRangeAddress(5, 5, 2, 8));

        s1.CreateRow(7).CreateCell(0).SetCellValue("Tên tàu:");
        s1.GetRow(7).GetCell(0).CellStyle = style12;

        s1.GetRow(7).CreateCell(2).SetCellValue(dt.Rows[0]["mv"].ToString());
        s1.GetRow(7).GetCell(2).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(7, 7, 2, 8));
        s1.GetRow(7).HeightInPoints = 22;

        s1.CreateRow(8).CreateCell(0).SetCellValue("Ngày tàu chạy:");
        s1.GetRow(8).GetCell(0).CellStyle = style12;

        s1.GetRow(8).CreateCell(2).SetCellValue(DateTime.Parse(dt.Rows[0]["etd"].ToString()).ToString("dd/MM/yyyy"));
        s1.GetRow(8).GetCell(2).CellStyle = style12;
        s1.GetRow(8).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(8, 8, 2, 8));


        s1.CreateRow(9).CreateCell(0).SetCellValue("Cảng đi:");
        s1.GetRow(9).GetCell(0).CellStyle = style12;

        s1.GetRow(9).CreateCell(2).SetCellValue(dt.Rows[0]["ten_cangbien"].ToString());
        s1.GetRow(9).GetCell(2).CellStyle = style12;
        s1.GetRow(9).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(9, 9, 2, 8));

        s1.CreateRow(10).CreateCell(0).SetCellValue("Cảng đến:");
        s1.GetRow(10).GetCell(0).CellStyle = style12;

        s1.GetRow(10).CreateCell(2).SetCellValue(dt.Rows[0]["noiden"].ToString());
        s1.GetRow(10).GetCell(2).CellStyle = style12;
        s1.GetRow(10).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(10, 10, 2, 8));

        s1.CreateRow(11).CreateCell(0).SetCellValue("Hàng hóa:");
        s1.GetRow(11).GetCell(0).CellStyle = style12;

        s1.GetRow(11).CreateCell(2).SetCellValue(dt.Rows[0]["commodityvn"].ToString());
        s1.GetRow(11).GetCell(2).CellStyle = style12;
        s1.GetRow(11).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(11, 11, 2, 8));

        //table
        s1.CreateRow(12).CreateCell(0).SetCellValue("Mã số");
        s1.GetRow(12).CreateCell(1).SetCellValue("Mã số khách");
        s1.GetRow(12).CreateCell(2).SetCellValue("Diễn giải");

        s1.GetRow(12).CreateCell(3).SetCellValue("Số lượng(cái/bộ)");
        s1.GetRow(12).CreateCell(4).SetCellValue("Đơn giá");
        s1.GetRow(12).CreateCell(5).SetCellValue("Thành tiền");
        s1.GetRow(12).CreateCell(6).SetCellValue("NW");
        s1.GetRow(12).CreateCell(7).SetCellValue("GW");
        s1.GetRow(12).CreateCell(8).SetCellValue("CBM");
        s1.GetRow(12).CreateCell(9).SetCellValue("Đóng gói");
        s1.GetRow(12).CreateCell(10).SetCellValue("");
        s1.GetRow(12).CreateCell(11).SetCellValue("Số kiện");

        s1.GetRow(12).GetCell(0).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(1).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(2).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(3).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(4).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(5).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(6).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(7).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(8).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(9).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(10).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(12).GetCell(11).CellStyle = styleCenterBorder12Bold;

        s1.AddMergedRegion(new CellRangeAddress(12, 12, 9, 10));


        string sql_group = string.Format(@"
            select distinct 
                dpk.c_donhang_id, 
                dh.sochungtu as sodh, 
                convert(decimal(18,2), po.discountPO) as discount, 
		        po.discountPO as discount1,
                po.hehang_value as discount_hehang_value,
		        cnx.container, 
                cnx.soseal, 
                cb.ten_cangbien, 
                cnx.c_nhapxuat_id,
                isnull(dh.cpdg_vuotchuan, 0) as cpdg_vuotchuan
            from c_dongpklinv dpk, 
                c_packinginvoice cpk, 
                c_donhang dh outer APPLY (select * from dbo.ftbl_getInfoPO(dh.phanbodiscount, dh.discount, null, dh.discount_hehang_value)) as po, 
                c_dongnhapxuat dnx, 
                c_nhapxuat cnx, 
                md_cangbien cb
            where dpk.c_packinginvoice_id = '{0}'
                and dpk.c_donhang_id = dh.c_donhang_id
		        and dpk.c_dongnhapxuat_id = dnx.c_dongnhapxuat_id
		        and dnx.c_nhapxuat_id = cnx.c_nhapxuat_id
                and dpk.c_packinginvoice_id = cpk.c_packinginvoice_id
		        and cpk.noidi = cb.md_cangbien_id"
        , c_pkivn_id);
        DataTable dt_g = mdbc.GetData(sql_group);

        string dh_ = @"select distinct c_donhang_id, sodh, container, soseal , discount, discount1, discount_hehang_value, c_nhapxuat_id, ten_cangbien, cpdg_vuotchuan from (" + sql_group + ") as tmp order by container asc, sodh asc";
        DataTable data_dh = mdbc.GetData(dh_);

        int g_line = 0;
        int r = 13;
        int countDonHang = data_dh.Rows.Count;
        int count = 1;
        double sumDiscount = 0;
        double sumFee = 0;
        List<int> lstQuantity = new List<int>();
        List<int> lstNoOfPack = new List<int>();
        List<int> lstTotalSalary = new List<int>();
        List<int> lstDiscount = new List<int>();
        List<int> lstNW = new List<int>();
        List<int> lstGW = new List<int>();
        List<int> lstCBM = new List<int>();
        List<int> lstPO = new List<int>();
        String funcDiscount = "";

        foreach (DataRow g_it in data_dh.Rows)
        {
            string discount_hehang_value = g_it["discount_hehang_value"].ToString();
            var nnl = string.IsNullOrEmpty(discount_hehang_value) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(discount_hehang_value);

            s1.CreateRow(r).CreateCell(0).SetCellValue("");
            s1.GetRow(r).CreateCell(0).SetCellValue("P/I No. " + g_it["sodh"].ToString());
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
            s1.GetRow(r).CreateCell(4).CellStyle = styleBorder12Bold;

            s1.GetRow(r).CreateCell(5).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(6).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(7).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(8).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(9).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(10).CellStyle = styleBorderCenter12;
            s1.GetRow(r).CreateCell(11).CellStyle = styleBorderCenter12;

            s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 2));
            s1.AddMergedRegion(new CellRangeAddress(r, r, 3, 5));

            string sql_de = string.Format(@"select ma_sanpham, ma_sanpham_khach, noofpack, soluong, gia, thanhtien, l1, w1, h1, l2, w2, h2,
								mota_tiengvietVG, ten_dvt, sl_outer, sl_outer_dg, dvt_outer, cbm, nkg, sl_inner, sl_outer, gkg
								--case when sl_inner > 1 then  round((((l1+w1)*(w1 + h1) / 5400 ) * sl_outer / sl_inner ) * noofpack + nkg, 2)
									--else round(( ((l2+w2)*(w2+h2)) / 5400) * noofpack + nkg, 2)
									--end as gkg
							from
							(select sp.ma_sanpham, cdd.ma_sanpham_khach, case when cdd.sl_outer = 0 then 0 else round(dpk.soluong/cdd.sl_outer, 1) end as noofpack ,
								dpk.soluong, dpk.gia, dpk.thanhtien, cdd.l2, cdd.w2, cdd.h2, cdd.l1, cdd.w1, cdd.h1,
								sp.mota_tiengvietVG, dvt.ten_dvt, mdg.sl_outer as sl_outer_dg,
								(select ten_dvt from md_donvitinh where md_donvitinh_id = mdg.dvt_outer) as dvt_outer,
								case when cdd.sl_outer = 0 then 0 else (round((cdd.l2 * cdd.h2 * cdd.w2/1000000 * dpk.soluong/cdd.sl_outer), 3)) end as cbm,
								(sp.trongluong*dpk.soluong) as nkg, cdd.sl_inner, cdd.sl_outer,
                                CAST(dpk.soluong as float) / CAST(mdg.sl_outer as float) * isnull(mdg.gw2, 0) as gkg
							from c_dongpklinv dpk left join md_sanpham sp on dpk.md_sanpham_id = sp.md_sanpham_id
								left join c_dongnhapxuat cdnx on dpk.c_dongnhapxuat_id = cdnx.c_dongnhapxuat_id
								left join c_dongdonhang cdd on cdnx.c_dongdonhang_id = cdd.c_dongdonhang_id
								left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_sanpham_id
								left join md_donggoi mdg on cdd.md_donggoi_id = mdg.md_donggoi_id
							where dpk.c_donhang_id = '{0}'
								and c_packinginvoice_id = '{1}'
								and cdnx.c_nhapxuat_id = '{2}'
								) as tmp order by ma_sanpham asc ", g_it["c_donhang_id"], c_pkivn_id, g_it["c_nhapxuat_id"]);

            DataTable dt_d = mdbc.GetData(sql_de);

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

                ir.CreateCell(2).SetCellValue(g_dt["mota_tiengvietVG"].ToString());
                ir.GetCell(2).CellStyle = styleBorder12;

                ir.CreateCell(3).SetCellValue(double.Parse(g_dt["soluong"].ToString()));
                ir.GetCell(3).CellStyle = styleNumber0Border12;

                ir.CreateCell(4).SetCellValue(double.Parse(g_dt["gia"].ToString()));
                ir.GetCell(4).CellStyle = styleNumber0i00Border12;

                ir.CreateCell(5).CellFormula = String.Format("D{0}*E{0}", r + 1);
                //ir.CreateCell(8).SetCellValue(double.Parse(g_dt["thanhtien"].ToString()));
                ir.GetCell(5).CellStyle = styleNumber0i00Border12;

                ir.CreateCell(6).SetCellValue(double.Parse(g_dt["nkg"].ToString()));
                ir.GetCell(6).CellStyle = styleNumber0i00Border12;

                ir.CreateCell(7).SetCellValue(double.Parse(g_dt["gkg"].ToString()));
                ir.GetCell(7).CellStyle = styleNumber0i00Border12;

                ir.CreateCell(8).SetCellValue(double.Parse(g_dt["cbm"].ToString()));
                ir.GetCell(8).CellStyle = styleNumber0i00Border12;

                ir.CreateCell(9).SetCellValue(double.Parse(g_dt["sl_outer"].ToString()));
                ir.GetCell(9).CellStyle = styleNumber0Border12;

                ir.CreateCell(10).SetCellValue(g_dt["dvt_outer"].ToString());
                ir.GetCell(10).CellStyle = styleBorder12;

                ir.CreateCell(11).SetCellValue(double.Parse(g_dt["noofpack"].ToString()));
                ir.GetCell(11).CellStyle = styleNumber0Border12;

                if ((chungloai != chungloaiTT & i < dt_d.Rows.Count - 1) | i == dt_d.Rows.Count - 1)
                {
                    if (nnl != null)
                    {
                        string gthh = nnl == null ? "0" : nnl.Where(s => s["hehang"].ToString() == chungloai).Select(s => s["giatri"].ToString()).FirstOrDefault();
                        var rowHeight = gthh == "0" ? 0 : 20;
                        r++;
                        var rowToTalHeHang = s1.CreateRow(r);
                        rowToTalHeHang.HeightInPoints = rowHeight;
                        rowToTalHeHang.CreateCell(2).SetCellValue(string.Format("Tổng cộng ({0}):", lstHH.Count + 1));
                        rowToTalHeHang.GetCell(2).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));
                        string fmlSum = string.Format(@"SUM(VNN{0}:VNN{1})", startRowHH + 1, r);
                        rowToTalHeHang.CreateCell(3).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "D"));
                        rowToTalHeHang.GetCell(3).CellStyle = styleNumber012Bold;
                        rowToTalHeHang.CreateCell(5).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "F"));
                        rowToTalHeHang.GetCell(5).CellStyle = styleNumber0i0012Bold;
                        rowToTalHeHang.CreateCell(6).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "G"));
                        rowToTalHeHang.GetCell(6).CellStyle = styleNumber0i0012Bold;
                        rowToTalHeHang.CreateCell(7).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "H"));
                        rowToTalHeHang.GetCell(7).CellStyle = styleNumber0i0012Bold;
                        rowToTalHeHang.CreateCell(8).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "I"));
                        rowToTalHeHang.GetCell(8).CellStyle = styleNumber0i0012Bold;
                        rowToTalHeHang.CreateCell(11).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "L"));
                        rowToTalHeHang.GetCell(11).CellStyle = styleNumber0i0012Bold;
                        r++;

                        var rowDisHeHang = s1.CreateRow(r);
                        rowDisHeHang.HeightInPoints = rowHeight;
                        rowDisHeHang.CreateCell(2).SetCellValue(string.Format(@"Giảm giá ({0}%):", gthh));
                        rowDisHeHang.GetCell(2).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));
                        rowDisHeHang.CreateCell(5).CellFormula = string.Format("ROUND(F{0}*{1}/100, 2)", r, gthh);
                        rowDisHeHang.GetCell(5).CellStyle = styleNumber0i0012Bold;
                        r++;

                        var rowTotalHeHang = s1.CreateRow(r);
                        rowTotalHeHang.HeightInPoints = rowHeight;
                        rowTotalHeHang.CreateCell(2).SetCellValue(string.Format("Tổng cộng ({0}):", lstHH.Count + 1));
                        rowTotalHeHang.GetCell(2).CellStyle = style12Bold;
                        s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 1));
                        rowTotalHeHang.CreateCell(3).CellFormula = string.Format("D{0}", r - 1);
                        rowTotalHeHang.GetCell(3).CellStyle = styleNumber012Bold;
                        rowTotalHeHang.CreateCell(5).CellFormula = string.Format("F{0} - F{1}", r - 1, r);
                        rowTotalHeHang.GetCell(5).CellStyle = styleNumber0i0012Bold;
                        rowTotalHeHang.CreateCell(6).CellFormula = string.Format("G{0}", r - 1);
                        rowTotalHeHang.GetCell(6).CellStyle = styleNumber0i0012Bold;
                        rowTotalHeHang.CreateCell(7).CellFormula = string.Format("H{0}", r - 1);
                        rowTotalHeHang.GetCell(7).CellStyle = styleNumber0i0012Bold;
                        rowTotalHeHang.CreateCell(8).CellFormula = string.Format("I{0}", r - 1);
                        rowTotalHeHang.GetCell(8).CellStyle = styleNumber0i0012Bold;
                        rowTotalHeHang.CreateCell(11).CellFormula = string.Format("L{0}", r - 1);
                        rowTotalHeHang.GetCell(11).CellStyle = styleNumber012Bold;

                        lstHH.Add(r);
                        startRowHH = r + 1;
                    }
                }
            }

            string sumQuantityPO = "", sumSubToTalPO = "", sumNoOfPackPO = "", sumNWPO = "", sumGWPO = "", sumCBMPO = "";
            if (nnl != null)
            {
                for (var iHH = 0; iHH < lstHH.Count; iHH++)
                {
                    var rowHH = lstHH[iHH];
                    sumNoOfPackPO += string.Format(@"L{0}+", rowHH + 1);
                    sumQuantityPO += string.Format(@"D{0}+", rowHH + 1);
                    sumSubToTalPO += string.Format(@"F{0}+", rowHH + 1);
                    sumNWPO += string.Format(@"G{0}+", rowHH + 1);
                    sumGWPO += string.Format(@"H{0}+", rowHH + 1);
                    sumCBMPO += string.Format(@"I{0}+", rowHH + 1);
                }
                sumNoOfPackPO += "0";
                sumQuantityPO += "0";
                sumSubToTalPO += "0";
                sumNWPO += "0";
                sumGWPO += "0";
                sumCBMPO += "0";
            }
            else
            {
                sumNoOfPackPO += string.Format(@"SUM(L{0}:L{1})", startRowHH + 1, r + 1);
                sumQuantityPO += string.Format(@"SUM(D{0}:D{1})", startRowHH + 1, r + 1);
                sumSubToTalPO += string.Format(@"SUM(F{0}:F{1})", startRowHH + 1, r + 1);
                sumNWPO += string.Format(@"SUM(G{0}:G{1})", startRowHH + 1, r + 1);
                sumGWPO += string.Format(@"SUM(H{0}:H{1})", startRowHH + 1, r + 1);
                sumCBMPO += string.Format(@"SUM(I{0}:I{1})", startRowHH + 1, r + 1);
            }

            r++;
            s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Tổng cộng{0}", countDonHang > 1 ? " (" + count.ToString() + "):" : ":"));
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

            double dsc1 = double.Parse(g_it["discount1"].ToString());
            if (dsc1 == 0)
                s1.GetRow(r).HeightInPoints = 0;

            //total NoOfPack
            s1.GetRow(r).CreateCell(11).CellFormula = sumNoOfPackPO;
            s1.GetRow(r).GetCell(11).CellStyle = styleNumber012Bold;
            lstNoOfPack.Add(r + 1);

            // total quantity
            s1.GetRow(r).CreateCell(3).CellFormula = sumQuantityPO;
            s1.GetRow(r).GetCell(3).CellStyle = styleNumber012Bold;
            lstQuantity.Add(r + 1);


            //total salary
            s1.GetRow(r).CreateCell(5).CellFormula = sumSubToTalPO;
            s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;
            lstTotalSalary.Add(r + 1);

            //total nw
            s1.GetRow(r).CreateCell(6).CellFormula = sumNWPO;
            s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;
            lstNW.Add(r + 1);

            //total gw
            s1.GetRow(r).CreateCell(7).CellFormula = sumGWPO;
            s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;
            lstGW.Add(r + 1);

            //total cbm
            s1.GetRow(r).CreateCell(8).CellFormula = sumCBMPO;
            s1.GetRow(r).GetCell(8).CellStyle = styleNumber0i0012Bold;
            lstCBM.Add(r + 1);
            
            // discount hidden
            s1.GetRow(r).CreateCell(14).SetCellValue(double.Parse(g_it["discount1"].ToString()));
            s1.GetRow(r).GetCell(14).CellStyle = styleNumber0i0012Bold;
            // total discount
            r++;

            string str_dsc1 = dsc1.ToString();

            s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Giảm giá ({0}%):", g_it["discount"].ToString()));
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
            funcDiscount = "ROUND(F{0}*O{0}/100,2)";

            s1.GetRow(r).CreateCell(5).CellFormula = String.Format(funcDiscount, r);
            s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;

            sumDiscount += double.Parse(g_it["discount"].ToString());

            if (double.Parse(g_it["discount"].ToString()) == 0) s1.GetRow(r).HeightInPoints = 0;

            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
            lstDiscount.Add(r + 1);
            //Phu thu
            r++;
            var phuthu = double.Parse(g_it["cpdg_vuotchuan"].ToString());
            s1.CreateRow(r).CreateCell(2).SetCellValue("Phụ thu");
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
            s1.GetRow(r).CreateCell(5).SetCellValue(phuthu);
            s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;
            if (phuthu == 0) s1.GetRow(r).HeightInPoints = 0;
            sumFee += phuthu;
            //Total
            r++;
            s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Tổng cộng{0}", countDonHang > 1 ? " (" + count.ToString() + "):" : ":"));
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

            // total NoOfPack
            s1.GetRow(r).CreateCell(11).CellFormula = String.Format("L{0}", r - 2);
            s1.GetRow(r).GetCell(11).CellStyle = styleNumber012Bold;

            // total quantity
            s1.GetRow(r).CreateCell(3).CellFormula = String.Format("D{0}", r - 2);
            s1.GetRow(r).GetCell(3).CellStyle = styleNumber012Bold;
            
            //total amount
            s1.GetRow(r).CreateCell(5).CellFormula = String.Format("F{0}-F{1}+F{2}", r - 2, r - 1, r);
            s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;

            s1.GetRow(r).CreateCell(6).CellFormula = String.Format("G{0}", r - 2);
            s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;

            s1.GetRow(r).CreateCell(7).CellFormula = String.Format("H{0}", r - 2);
            s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;

            s1.GetRow(r).CreateCell(8).CellFormula = String.Format("I{0}", r - 2);
            s1.GetRow(r).GetCell(8).CellStyle = styleNumber0i0012Bold;
            count++;
            r++;
            lstPO.Add(r);
        }


        s1.CreateRow(r).CreateCell(2).SetCellValue(pdkInvoice.diengiai_cong + ":");
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(5).SetCellValue((double)pdkInvoice.giatri_cong.GetValueOrDefault(0));
        s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;

        if (pdkInvoice.giatri_cong.GetValueOrDefault(0) == 0)
            s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue(pdkInvoice.diengiai_tru + ":");
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(5).SetCellValue((double)pdkInvoice.giatri_tru.GetValueOrDefault(0));
        s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatri_tru.GetValueOrDefault(0) == 0)
            s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue(pdkInvoice.diengiaicong_po + ":");
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(5).SetCellValue((double)pdkInvoice.giatricong_po.GetValueOrDefault(0));
        s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatricong_po.GetValueOrDefault(0) == 0)
            s1.GetRow(r).HeightInPoints = 0;

        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue(pdkInvoice.diengiaitru_po + ":");
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        s1.GetRow(r).CreateCell(5).SetCellValue(double.Parse(pdkInvoice.giatritru_po.Value.ToString()));
        s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;
        if (pdkInvoice.giatritru_po.Value == 0)
            s1.GetRow(r).HeightInPoints = 0;

        String strCount = "";
        for (int i = 1; i < count; i++)
        {
            strCount += " + " + i;
        }
        strCount = strCount.Substring(2);
        //Phụ thu
        r++;
        s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Phụ thu{0}", countDonHang > 1 ? " (" + strCount.ToString() + "):" : ":"));
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
        string fPhuThu = "";
        foreach (int item in lstPO)
        {
            fPhuThu += String.Format("+ F{0} ", item - 1);
        }
        fPhuThu = fPhuThu.Substring(1);
        
        s1.GetRow(r).CreateCell(5).SetCellFormula(fPhuThu);
        s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;

        //Tổng cộng
        r++;
        
        s1.CreateRow(r).CreateCell(2).SetCellValue(String.Format("Tổng cộng{0}", countDonHang > 1 ? " (" + strCount.ToString() + "):" : ":"));
        s1.GetRow(r).GetCell(2).CellStyle = style12Bold;

        // total quantity
        string fQuantity = "", fSalary = "", fDiscount = "", fNoOfPack = "", fGW = "", fCBM = "", fNW = "";

        foreach (int item in lstGW)
        {
            fGW += String.Format("+ H{0} ", item);
        }
        fGW = fGW.Substring(1);

        foreach (int item in lstNW)
        {
            fNW += String.Format("+ G{0} ", item);
        }
        fNW = fNW.Substring(1);

        foreach (int item in lstCBM)
        {
            fCBM += String.Format("+ I{0} ", item);
        }
        fCBM = fCBM.Substring(1);

        foreach (int item in lstNoOfPack)
        {
            fNoOfPack += String.Format("+ L{0} ", item);
        }
        fNoOfPack = fNoOfPack.Substring(1);

        foreach (int item in lstQuantity)
        {
            fQuantity += String.Format("+ D{0} ", item);
        }
        fQuantity = fQuantity.Substring(1);

        //total discount
        foreach (int item in lstDiscount)
        {
            fDiscount += String.Format("+ F{0} ", item);
        }
        fDiscount = fDiscount.Substring(1);


        //total salary
        foreach (int item in lstPO)
        {
            fSalary += String.Format("+ F{0} ", item);
        }
        fSalary = fSalary.Substring(1);

        s1.GetRow(r).CreateCell(3).SetCellFormula(fQuantity);
        s1.GetRow(r).GetCell(3).CellStyle = styleNumber012Bold;

        s1.GetRow(r).CreateCell(5).SetCellFormula(String.Format("{0} + F{2} - F{3} + F{4} - F{5}", fSalary, fDiscount, r - 4, r - 3, r - 2, r - 1));
        s1.GetRow(r).GetCell(5).CellStyle = styleNumber0i0012Bold;

        s1.GetRow(r).CreateCell(6).SetCellFormula(fNW);
        s1.GetRow(r).GetCell(6).CellStyle = styleNumber0i0012Bold;

        s1.GetRow(r).CreateCell(7).SetCellFormula(fGW);
        s1.GetRow(r).GetCell(7).CellStyle = styleNumber0i0012Bold;

        s1.GetRow(r).CreateCell(8).SetCellFormula(fCBM);
        s1.GetRow(r).GetCell(8).CellStyle = styleNumber0i0012Bold;

        s1.GetRow(r).CreateCell(11).SetCellFormula(fNoOfPack);
        s1.GetRow(r).GetCell(11).CellStyle = styleNumber012Bold;

        s1.GetRow(r).HeightInPoints = 0;
        s1.GetRow(r - 1).HeightInPoints = 0;
        if ((countDonHang > 1) || (sumDiscount > 0 && countDonHang > 1) || (pdkInvoice.giatri_cong > 0) || (pdkInvoice.giatri_tru > 0) || (pdkInvoice.giatricong_po > 0) || (pdkInvoice.giatritru_po > 0))
        {
            s1.GetRow(r).HeightInPoints = 32;
            if(sumFee > 0)
                s1.GetRow(r - 1).HeightInPoints = 32;
        }

        s1.CreateRow(r + 1).CreateCell(0).SetCellValue("Số lượng:");
        s1.GetRow(r + 1).GetCell(0).CellStyle = style12;

        s1.GetRow(r + 1).CreateCell(1).CellFormula = String.Format("D{0}", r + 1);
        s1.GetRow(r + 1).GetCell(1).CellStyle = styleNumber012;
        s1.GetRow(r + 1).CreateCell(2).SetCellValue("cái, bộ");
        s1.GetRow(r + 1).GetCell(2).CellStyle = style12;

        s1.CreateRow(r + 2).CreateCell(0).SetCellValue("Đóng gói:");
        s1.GetRow(r + 2).GetCell(0).CellStyle = style12;
        s1.GetRow(r + 2).CreateCell(1).CellFormula = String.Format("L{0}", r + 1);
        s1.GetRow(r + 2).GetCell(1).CellStyle = styleNumber0i0012;
        s1.GetRow(r + 2).CreateCell(2).SetCellValue("kiện");
        s1.GetRow(r + 2).GetCell(2).CellStyle = style12;

        // s1.CreateRow(r + 3).CreateCell(0).SetCellValue("Trọng lượng tịnh:");
        // s1.GetRow(r + 3).GetCell(0).CellStyle = style12;
        // s1.GetRow(r + 3).CreateCell(1).CellFormula = String.Format("G{0}", r + 1);
        // s1.GetRow(r + 3).GetCell(1).CellStyle = styleNumber0i0012;
        // s1.GetRow(r + 3).CreateCell(2).SetCellValue("kg");
        // s1.GetRow(r + 3).GetCell(2).CellStyle = style12;

        s1.CreateRow(r + 3).CreateCell(0).SetCellValue("Trọng lượng:");
        s1.GetRow(r + 3).GetCell(0).CellStyle = style12;
        s1.GetRow(r + 3).CreateCell(1).CellFormula = String.Format("H{0}", r + 1);
        s1.GetRow(r + 3).GetCell(1).CellStyle = styleNumber0i0012;
        s1.GetRow(r + 3).CreateCell(2).SetCellValue("kg");
        s1.GetRow(r + 3).GetCell(2).CellStyle = style12;

        s1.CreateRow(r + 4).CreateCell(0).SetCellValue("Khối lượng:");
        s1.GetRow(r + 4).GetCell(0).CellStyle = style12;
        s1.GetRow(r + 4).CreateCell(1).CellFormula = String.Format("I{0}", r + 1);
        s1.GetRow(r + 4).GetCell(1).CellStyle = styleNumber0i0012;
        s1.GetRow(r + 4).CreateCell(2).SetCellValue("cbm");
        s1.GetRow(r + 4).GetCell(2).CellStyle = style12;


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
            dic["name"] = "TKHQ";
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
