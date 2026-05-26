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

public partial class PrintControllers_InDanhMucKhaiHQ03_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String c_pkivn_id = Request.QueryString["c_packinglist_id"];
        String select = String.Format(@"select distinct cpk.so_pkl, cpk.etd, dt.ten_dtkd, dt.diachi, dt.tel, dt.fax,
		                                            cpk.mv, cb.ten_cangbien, cpk.noiden, cpk.blno, cpk.commodity
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
            String saveAsFileName = String.Format("DanhMucKhaiHaiQuan(HScode)-{0}.xls", DateTime.Now);
            this.SaveFile(hssfworkbook, saveAsFileName);
        }
        else
        {
            Response.Write("<h3>PackingList/Invoice không có dữ liệu</h3>");
        }
    }

    public HSSFWorkbook CreateWorkBookPO(DataTable dt, String chungTuDonHang)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        String c_pkivn_id = Request.QueryString["c_packinglist_id"];
        var pdkInvoice = db.c_packinginvoices.Single(inv => inv.c_packinginvoice_id.Equals(c_pkivn_id));

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("DMHQ(3)");

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
        styleNumber0Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0Border12.SetFont(font12);
        styleNumber0Border12.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0Border12.Alignment = HorizontalAlignment.Right;
        styleNumber0Border12.WrapText = true;
        styleNumber0Border12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleNumber0Border12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;


        ICellStyle styleNumber0i0Border12 = hssfworkbook.CreateCellStyle();
        styleNumber0i0Border12.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
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


        //0
        ICellStyle styleNumber012 = hssfworkbook.CreateCellStyle();
        styleNumber012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber012.SetFont(font12);
        styleNumber012.VerticalAlignment = VerticalAlignment.Top;
        styleNumber012.Alignment = HorizontalAlignment.Right;
        styleNumber012.WrapText = true;

        //1
        ICellStyle styleNumber0i012 = hssfworkbook.CreateCellStyle();
        styleNumber0i012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i012.SetFont(font12);
        styleNumber0i012.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i012.Alignment = HorizontalAlignment.Right;
        styleNumber0i012.WrapText = true;

        //2
        ICellStyle styleNumber0i0012 = hssfworkbook.CreateCellStyle();
        styleNumber0i0012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i0012.SetFont(font12);
        styleNumber0i0012.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i0012.Alignment = HorizontalAlignment.Right;
        styleNumber0i0012.WrapText = true;
        //3
        ICellStyle styleNumber0i00012 = hssfworkbook.CreateCellStyle();
        styleNumber0i00012.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.000");
        styleNumber0i00012.SetFont(font12);
        styleNumber0i00012.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i00012.Alignment = HorizontalAlignment.Right;
        styleNumber0i00012.WrapText = true;

        //4
        ICellStyle styleNumber012Bold = hssfworkbook.CreateCellStyle();
        styleNumber012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber012Bold.SetFont(font12Bold);
        styleNumber012Bold.VerticalAlignment = VerticalAlignment.Top;
        styleNumber012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber012Bold.WrapText = true;

        //5
        ICellStyle styleNumber0i012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i012Bold.SetFont(font12Bold);
        styleNumber0i012Bold.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i012Bold.WrapText = true;

        //6
        ICellStyle styleNumber0i0012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i0012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.00");
        styleNumber0i0012Bold.SetFont(font12Bold);
        styleNumber0i0012Bold.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i0012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i0012Bold.WrapText = true;
        //7
        ICellStyle styleNumber0i00012Bold = hssfworkbook.CreateCellStyle();
        styleNumber0i00012Bold.DataFormat = NPOIUtils.GetDataFormat(hssfworkbook, "#,#0.000");
        styleNumber0i00012Bold.SetFont(font12Bold);
        styleNumber0i00012Bold.VerticalAlignment = VerticalAlignment.Top;
        styleNumber0i00012Bold.Alignment = HorizontalAlignment.Right;
        styleNumber0i00012Bold.WrapText = true;

        s1.SetColumnWidth(0, 5000);
        s1.SetColumnWidth(1, 8500);
        s1.SetColumnWidth(2, 3500);
        s1.SetColumnWidth(3, 5000);
        s1.SetColumnWidth(4, 3500);
        s1.SetColumnWidth(5, 3500);
        s1.SetColumnWidth(6, 3500);
        s1.SetColumnWidth(7, 3500);





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


        s1.CreateRow(0).HeightInPoints = 0;
        s1.CreateRow(1).HeightInPoints = 0;
        s1.CreateRow(2).HeightInPoints = 0;


        s1.CreateRow(3).CreateCell(0).SetCellValue("DANH MỤC KHAI HẢI QUAN THEO HSCODE");
        s1.GetRow(3).GetCell(0).CellStyle = styleCenter18Bold;
        s1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 7));
        s1.GetRow(3).HeightInPoints = 30;

        s1.CreateRow(4).HeightInPoints = 0;
        s1.CreateRow(5).HeightInPoints = 0;
        s1.CreateRow(6).HeightInPoints = 0;
        s1.CreateRow(7).HeightInPoints = 0;
        s1.CreateRow(8).HeightInPoints = 0;
        s1.CreateRow(9).HeightInPoints = 0;
        s1.CreateRow(10).HeightInPoints = 0;
        s1.CreateRow(11).HeightInPoints = 0;
        s1.CreateRow(12).HeightInPoints = 0;

        //Table
        s1.CreateRow(13).CreateCell(0).SetCellValue("Mã HScode 8 số");
        s1.GetRow(13).GetCell(0).CellStyle = styleCenterBorder12Bold;


        //s1.GetRow(13).CreateCell(1).SetCellValue("Diễn giải");
        //s1.GetRow(13).GetCell(1).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(1).SetCellValue("Hệ hàng");
        s1.GetRow(13).GetCell(1).CellStyle = styleCenterBorder12Bold;

        //s1.GetRow(13).CreateCell(3).SetCellValue("Xuất sứ");
        //s1.GetRow(13).GetCell(3).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(2).SetCellValue("Số lượng");
        s1.GetRow(13).GetCell(2).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(3).SetCellValue("Trọng lượng");
        s1.GetRow(13).GetCell(3).CellStyle = styleCenterBorder12Bold;

        //s1.GetRow(13).CreateCell(6).SetCellValue("Đơn giá");
        //s1.GetRow(13).GetCell(6).CellStyle = styleCenterBorder12Bold;

        s1.GetRow(13).CreateCell(4).SetCellValue("Thành tiền");
        s1.GetRow(13).GetCell(4).CellStyle = styleCenterBorder12Bold;

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
        int r = 14;
        List<int> lstNoOfPack = new List<int>();
        List<int> lstQuantity = new List<int>();
        List<int> lstNW = new List<int>();
        List<int> lstGW = new List<int>();
        List<int> lstCBM = new List<int>();

        foreach (DataRow g_it in data_dh.Rows)
        {


            //r++;
            s1.CreateRow(r).CreateCell(0).SetCellValue("P/I No.: " + g_it["sodh"].ToString());

            s1.GetRow(r).GetCell(0).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(1).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(2).CellStyle = styleBorder12Bold;
            s1.GetRow(r).CreateCell(3).CellStyle = styleBorder12;
            s1.GetRow(r).CreateCell(4).CellStyle = styleBorder12;
            //  s1.GetRow(r).CreateCell(5).CellStyle = styleBorder12;
            //  s1.GetRow(r).CreateCell(6).CellStyle = styleBorder12;
            // s1.GetRow(r).CreateCell(7).CellStyle = styleBorder12;
            s1.AddMergedRegion(new CellRangeAddress(r, r, 0, 4));
            s1.GetRow(r).HeightInPoints = 22;

            string sql_de = @"select  hscode,ma_sanpham,SUM(soluong) as soluong,SUM(gia) as gia,SUM(thanhtien) as thanhtien,
 SUM(trongluong) as trongluong from ( 
select hscode,SUBSTRING(ma_sanpham,0,3) as ma_sanpham, soluong, gia, thanhtien, 'VIETNAM' as xuatxu,
                                mota_tiengviet, ten_dvt , (trongluong * soluong) as trongluong
                            from
                            (select sp.ma_sanpham, cdnx.mota_tiengviet, hscode.hscode,
	                            dpk.soluong, dpk.gia, dpk.thanhtien,
	                            dpk.mota_tienganh, dvt.ten_dvt, sp.trongluong
                            from c_dongpklinv dpk
                                left join 
                                    c_dongnhapxuat cdnx on dpk.c_dongnhapxuat_id = cdnx.c_dongnhapxuat_id
                                left join 
                                    c_dongdonhang cdd on cdnx.c_dongdonhang_id = cdd.c_dongdonhang_id
                                left join 
                                    md_sanpham sp on cdd.md_sanpham_id = sp.md_sanpham_id
                                left join 
                                    md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
	                            left join 
                                    md_hscode hscode on sp.md_hscode_id = hscode.md_hscode_id

                            where dpk.c_donhang_id =  '" + g_it["c_donhang_id"] + "'" +
                                @"and c_packinginvoice_id = '" + c_pkivn_id + "' " +
                               @"and cdnx.c_nhapxuat_id =  '" + g_it["c_nhapxuat_id"] + "'" +
                                @") as tmp ) as nhan
                                group by hscode,ma_sanpham
                                order by hscode asc";

            DataTable dt_d = mdbc.GetData(sql_de);
            int total_row_d = dt_d.Rows.Count;
            //create detail group
            foreach (DataRow g_dt in dt_d.Rows)
            {
                r++;
                IRow ir = s1.CreateRow(r);
                ir.CreateCell(0).SetCellValue(g_dt["hscode"].ToString());
                //ir.CreateCell(1).SetCellValue(g_dt["ma_sanpham_khach"].ToString());
                // ir.CreateCell(1).SetCellValue(g_dt["mota_tiengviet"].ToString());
                ir.CreateCell(1).SetCellValue(g_dt["ma_sanpham"].ToString());
                // ir.CreateCell(3).SetCellValue(g_dt["xuatxu"].ToString());

                //ir.CreateCell(3).SetCellValue(g_dt["ten_dvt"].ToString());
                ir.CreateCell(2).SetCellValue(double.Parse(g_dt["soluong"].ToString()));
                //ir.CreateCell(6).SetCellValue(double.Parse(g_dt["gia"].ToString()));
                ir.CreateCell(3).SetCellValue(double.Parse(g_dt["trongluong"].ToString()));
                ir.CreateCell(4).SetCellValue(double.Parse(g_dt["thanhtien"].ToString()));
                
                //ir.CreateCell(15).SetCellValue(double.Parse(g_dt["soluong"].ToString()));

                ir.GetCell(0).CellStyle = styleBorder12;
                ir.GetCell(1).CellStyle = styleBorder12;
                ir.GetCell(2).CellStyle = styleBorder12;
                ir.GetCell(3).CellStyle = styleBorder12;
                ir.GetCell(4).CellStyle = styleNumber0Border12;
               // ir.GetCell(5).CellStyle = styleBorder12;
               // ir.GetCell(6).CellStyle = styleNumber0i00Border12;
                //ir.GetCell(7).CellStyle = styleNumber0i00Border12;
            }
            r++;
            int count = 0;
            s1.CreateRow(r).CreateCell(2).SetCellValue("");
            s1.GetRow(r).GetCell(2).CellStyle = style12Bold;
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
            dic["name"] = "DMHQ (HSC)";
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