using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Data;

/// <summary>
/// Summary description for BaoGiaReport
/// </summary>
public class BaoGiaReport
{
    private String c_baogia_id, logoPath, productImagePath, typeReport;
    private bool? laBaoGiaMau;
    private bool? laBaoGiaMauQR;
    private List<AvariablePrj.lstImage> lstimage = new List<AvariablePrj.lstImage>();

    public List<AvariablePrj.lstImage> Lstimage
    {
        get { return lstimage; }
        set { lstimage = value; }
    }
    public String LogoPath
    {
        get { return logoPath; }
        set { logoPath = value; }
    }

    public String ProductImagePath
    {
        get { return productImagePath; }
        set { productImagePath = value; }
    }

    public String TypeReport
    {
        get { return typeReport; }
        set { typeReport = value; }
    }

    public bool? LaBaoGiaMau
    {
        get { return laBaoGiaMau; }
        set { laBaoGiaMau = value; }
    }

    public bool? LaBaoGiaMauQR
    {
        get { return laBaoGiaMauQR; }
        set { laBaoGiaMauQR = value; }
    }


    private LinqDBDataContext db = new LinqDBDataContext();
    public HSSFWorkbook CreateWBQuotation()
    {
        //try
        //{
        string tbl = LaBaoGiaMauQR.GetValueOrDefault(false) ? "c_baogiaqr" : "c_baogia";
        string tblChild1 = LaBaoGiaMauQR.GetValueOrDefault(false) ? "c_chitietbaogiaqr" : "c_chitietbaogia"; 
        string orderby = UserUtils.get_sapxep_qodetail(null, c_baogia_id, "B.ma_sanpham", "order by B.ma_sanpham asc", db);

        String selectDetails = String.Format(@"
			SELECT *
                				--, 
                --CASE 
                    --WHEN B.sl_inner = '0 0' and B.master like N'%ctn%'
				    --THEN round(B.n_w_outer + ((B.l2 + B.w2) * (B.w2 + B.h2))/5400,2)
                
                    --WHEN B.sl_inner = '0 0' and B.master like N'%pal%'
				    --THEN round(B.n_w_outer + 25,2)

				    --WHEN B.sl_inner = '0 0' and B.master like N'%bun%'
				    --THEN round(B.n_w_outer + 0.5,2)
						
				    --WHEN B.sl_inner = '0 0' and B.master like N'%crt%'
				    --THEN round(B.n_w_outer + 5,2)
                
				    --WHEN B.sl_inner like N'%ctn%' and B.master like N'%ctn%' and B.inner_ > 0
				    --THEN round((B.g_w_inner * (B.sl_outer/ B.inner_)) +  (((B.l2 + B.w2) * (B.w2 + B.h2))/5400),2)

				    --WHEN B.sl_inner like N'%ctn%' and B.master like N'%pal%' and B.inner_ > 0
				    --THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 20,2)

				    --WHEN B.sl_inner like N'%ctn%' and B.master like N'%crt%' and B.inner_ > 0
				    --THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 5,2)

				    --WHEN B.sl_inner like N'%bun%' and B.master like N'%crt%' and B.inner_ > 0
				    --THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 5,2)

				    --WHEN B.sl_inner like N'%bun%' and B.master like N'%pal%' and B.inner_ > 0
				    --THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 20,2)

				--ELSE 0 END as g_w_outer
			FROM (
				SELECT *,
                    CASE WHEN  A.sl_inner like N'%ctn%'  
					THEN round(A.n_w_inner + ((A.l1+A.w1)*(A.w1+A.h1)/5400),2)
					WHEN A.sl_inner like N'%bun%'  
					THEN round(A.n_w_inner + 0.5,2)
					WHEN A.sl_inner like N'%crt%'  
					THEN round(A.n_w_inner + 3,2)
					ELSE 0 END as g_w_inner
				FROM (
				    select 
	                    dtkd.ten_dtkd, dtkd.diachi, dtkd.tel, dtkd.fax, bg.ngaybaogia
	                    , sp.ma_sanpham as msp
                        , (case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) else SUBSTRING(sp.ma_sanpham,0,12) end) as pic
                        , sp.ma_sanpham
                        , ctbg.ma_sanpham_khach
	                    , (CASE 
						        WHEN kt.ten_kichthuoc = 'inch' then dbo.f_taomotainchQoPo(sp.md_sanpham_id) 
						        ELSE ctbg.mota_tienganh
					        END) as mota_tienganh
				        , (select dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id)) as trongluong
                        , sp.dientich
                        , tl.ten_trongluong, dg.sl_inner as inner_, dg.sl_outer
                        , (CAST(dg.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner)) as sl_inner
	                    , (CAST(dg.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer)) as master
                        , dg.ten_donggoi, ctbg.l1, ctbg.w1, ctbg.h1, ctbg.l2, ctbg.w2, ctbg.h2, ctbg.v2, round(ctbg.v2 * 35.31466672, 2)  as cbf
	                    , isnull(dg.soluonggoi_ctn,0) as nopack, isnull(dg.soluonggoi_ctn_20,0) as nopack_20, isnull(dg.soluonggoi_ctn_40hc,0) as nopack_40hc, ctbg.giafob
	                    , sp.l_cm, sp.w_cm, sp.h_cm, sp.l_inch, sp.w_inch, sp.h_inch
	                    , bg.shipmenttime, bg.shipmenttime + 15 as shipmenttime2, cb.ten_cangbien
	                    , bg.ngayhethan, kt.ten_kichthuoc, bg.moq_item_color, hscode.hscode
				        , dg.sl_inner * (SELECT dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id))  as n_w_inner
				        , dg.sl_outer * (SELECT dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id)) as n_w_outer
                        , hscode.thanhphan
						, ctbg.docquyen
                        , isnull(dg.cpdg_vuotchuan, 0) as cpdg_vuotchuan
                        , dmhh.ten_danhmuc
                        , isnull(dg.nw2, 0) as nw2
                        , isnull(dg.gw2, 0) as gw2
                        , isnull(dg.vtdg2, 0) as vtdg2

                        , isnull(dg.gw2, 0) as g_w_outer
                    from 
	                    {2} bg
				        left join {3} ctbg ON bg.{2}_id = ctbg.{2}_id
	                    left join md_doitackinhdoanh dtkd ON bg.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
				        left join md_sanpham sp ON ctbg.md_sanpham_id = sp.md_sanpham_id
                        left join md_danhmuchanghoa dmhh ON dmhh.md_danhmuchanghoa_id = sp.md_tinhtranghanghoa_id
				        left join md_hscode hscode ON sp.md_hscode_id = hscode.md_hscode_id
	                    left join md_donggoi dg ON ctbg.md_donggoi_id = dg.md_donggoi_id
	                    left join md_cangbien cb ON sp.md_cangbien_id = cb.md_cangbien_id
                        left join md_kichthuoc kt ON bg.md_kichthuoc_id = kt.md_kichthuoc_id
                        left join md_trongluong tl ON bg.md_trongluong_id = tl.md_trongluong_id
                    where bg.{2}_id = N'{0}'
                )A
            )B {1}
            "
            , c_baogia_id == null ? "" : c_baogia_id
            , orderby
            , tbl
            , tblChild1
        );


        String selectRefereces = String.Format(@"
            SELECT 
	            (select SUBSTRING(filter, 1, 2) + '-' + ta_ngan as ta_ngan from md_chungloai cl where cl.code_cl = SUBSTRING(filter, 1, 2)) as ta_ngan, 
                SUBSTRING(filter, 1, 2) as chungloai, 
                SUBSTRING(filter, 4, len(filter)) as codemau, 
                mau, 
                url
            FROM 
	            c_baogia bg
				inner join md_color_reference color on bg.c_baogia_id = color.c_baogia_id
            WHERE
                color.hoatdong = 1
                AND bg.c_baogia_id = N'{0}'
            order by url
        ", c_baogia_id == null ? "" : c_baogia_id);

        String selectDistinct = String.Format(@"
            SELECT distinct 
                chungloai, ta_ngan
            FROM (
                SELECT 
	                (select ta_ngan from md_chungloai cl where cl.code_cl = SUBSTRING(filter, 1, 2)) as ta_ngan, SUBSTRING(filter, 1, 2) as chungloai, SUBSTRING(filter, 4, 2) as codemau, mau , url
                FROM 
	                c_baogia bg
					inner join md_color_reference color on bg.c_baogia_id = color.c_baogia_id
                WHERE
					color.hoatdong = 1
                    AND bg.c_baogia_id = N'{0}'
            ) as tmp", c_baogia_id == null ? "" : c_baogia_id);

        DataTable dtDetails = mdbc.GetData(selectDetails);
        DataTable dtDistinct = mdbc.GetData(selectDistinct);
        DataTable dtReferences = mdbc.GetData(selectRefereces);
        if (dtDetails.Rows.Count != 0)
        {
            HSSFWorkbook hssfworkbook = this.CreateHSSFWorkbook(dtDetails, dtDistinct, dtReferences);
            return hssfworkbook;
        }
        else
        {
            throw new Exception("Báo giá không có dữ liệu!");
            //throw new Exception(selectDetails);
        }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    private HSSFWorkbook CreateHSSFWorkbook(DataTable dtDetails, DataTable dtDistinct, DataTable dtReferences)
    {
        
        String tenkt = dtDetails.Rows[0]["ten_kichthuoc"].ToString().ToLower();
        String tentl = dtDetails.Rows[0]["ten_trongluong"].ToString().ToLower();
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");

        var config = new ExportExcel(hssfworkbook, 0, 23);
        var ex_fm = new Excel_Format(hssfworkbook);
        var celltext = ex_fm.getcell(10, false, true, "", "L", "T");
        var cellTextCenterBoderAll = ex_fm.getcell(10, false, true, "LRTB", "C", "T");

        config.title = "QUOTATION";

        ICellStyle cellStyleRight = hssfworkbook.CreateCellStyle();
        cellStyleRight.Alignment = HorizontalAlignment.Right;
        cellStyleRight.VerticalAlignment = VerticalAlignment.Top;
        cellStyleRight.BorderBottom = cellStyleRight.BorderLeft
            = cellStyleRight.BorderRight
            = cellStyleRight.BorderTop
            = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleRight.WrapText = true;


        IFont fontBold = hssfworkbook.CreateFont();
        fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        ICellStyle cellBold = hssfworkbook.CreateCellStyle();
        cellBold.SetFont(fontBold);

        ICellStyle cellBoldCenter = hssfworkbook.CreateCellStyle();
        cellBoldCenter.Alignment = HorizontalAlignment.Center;
        cellBoldCenter.VerticalAlignment = VerticalAlignment.Top;
        cellBoldCenter.SetFont(fontBold);

        int row = 0;
        config.endHeader = 20;
        config.LogoRpt = new ExportExcel.Logoreport() {
            cell1 = 0,
            cell2 = 0,
            dx1 = 450,
            dx2 = 1000
        };
        row = config.createHeader(NameHeaderReport.ANCO, s1, row, config.endHeader);

        ICellStyle cellUn = hssfworkbook.CreateCellStyle();
        IFont funderline = hssfworkbook.CreateFont();
        funderline.Underline = FontUnderlineType.Single;
        funderline.FontHeightInPoints = 12;
        cellUn.SetFont(funderline);
        cellUn.Alignment = HorizontalAlignment.Center;
        cellUn.VerticalAlignment = VerticalAlignment.Top;
        s1.GetRow(3).GetCell(0).CellStyle = cellUn;

        // set A4
        s1.CreateRow(row).CreateCell(0).SetCellValue("FOB TAM HIEP or DA NANG PORT");
        ICellStyle cel = hssfworkbook.CreateCellStyle();
        cel.Alignment = HorizontalAlignment.Center;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, config.endHeader.GetValueOrDefault(0)));

        IFont f = hssfworkbook.CreateFont();
        f.FontHeightInPoints = 13;
        cel.SetFont(f);
        s1.GetRow(row).GetCell(0).CellStyle = cel;

        row++;
        s1.CreateRow(row).CreateCell(1).SetCellValue("CUSTOMER NAME:");
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 2));

        s1.GetRow(row).CreateCell(3).SetCellValue(dtDetails.Rows[0]["ten_dtkd"].ToString());
        s1.GetRow(row).GetCell(3).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 3, 11));

        row++;
        s1.CreateRow(row).CreateCell(1).SetCellValue("ADDRESS:");
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 2));

        s1.GetRow(row).CreateCell(3).SetCellValue(dtDetails.Rows[0]["diachi"].ToString() + " Tel:" + dtDetails.Rows[0]["tel"].ToString() + " Fax:" + dtDetails.Rows[0]["fax"].ToString());
        s1.GetRow(row).GetCell(3).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 3, 11));

        s1.GetRow(row).CreateCell(12).SetCellValue("Date:");
        s1.GetRow(row).GetCell(12).CellStyle = cellBold;

        s1.GetRow(row).CreateCell(13).SetCellValue(DateTime.Parse(dtDetails.Rows[0]["ngaybaogia"].ToString()).ToString("dd/MMM/yyyy"));
        s1.GetRow(row).GetCell(13).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 13, 15));

        row++;
        IRow headerColumn = s1.CreateRow(row);
        row++;
        IRow headerColumn2 = s1.CreateRow(row);

        headerColumn.HeightInPoints = 40;
        headerColumn2.HeightInPoints = 40;
        ICellStyle hStyle = hssfworkbook.CreateCellStyle();
        hStyle.WrapText = true;
        hStyle.SetFont(fontBold);
        hStyle.Alignment = HorizontalAlignment.Center;
        hStyle.VerticalAlignment = VerticalAlignment.Top;
        hStyle.BorderBottom = hStyle.BorderLeft
            = hStyle.BorderRight
            = hStyle.BorderTop
            = NPOI.SS.UserModel.BorderStyle.Thin;
        //--
        var totalColumn = 36;
        for (int i = 0; i <= totalColumn; i++)
        {
            headerColumn.CreateCell(i).CellStyle = hStyle;
            headerColumn2.CreateCell(i).CellStyle = hStyle;
        }
        //--
        headerColumn.GetCell(0).SetCellValue("Item No");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 0, 0));

        headerColumn.GetCell(1).SetCellValue("Picture - Only for shape ref.");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 1, 1));

        headerColumn.GetCell(2).SetCellValue("Customer's Code");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 2, 2));

        headerColumn.GetCell(3).SetCellValue("Description");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 3, 3));

        headerColumn.GetCell(4).SetCellValue("Weight (" + tentl + ")");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 4, 4));

        headerColumn.GetCell(5).SetCellValue("Packing");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row - 1, 5, 6));

        headerColumn2.GetCell(5).SetCellValue("Inner");
        headerColumn2.GetCell(6).SetCellValue("Master");

        headerColumn.GetCell(7).SetCellValue(String.Format("Inner Packing size ({0})", tenkt.Equals("cm") ? "cm" : "inch"));
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row - 1, 7, 9));
        headerColumn2.GetCell(7).SetCellValue("L");
        headerColumn2.GetCell(8).SetCellValue("W");
        headerColumn2.GetCell(9).SetCellValue("H");


        headerColumn.GetCell(10).SetCellValue(String.Format("Master Packing size ({0})", tenkt.Equals("cm") ? "cm" : "inch"));
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row - 1, 10, 12));

        headerColumn2.GetCell(10).SetCellValue("L");
        headerColumn2.GetCell(11).SetCellValue("W");
        headerColumn2.GetCell(12).SetCellValue("H");


        headerColumn.GetCell(13).SetCellValue(String.Format("PACK {0}", tenkt.Equals("cm") ? "CBM" : "CBF"));
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 13, 13));

        headerColumn.GetCell(14).SetCellValue("No. of master packages");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row - 1, 14, 16));

        headerColumn2.GetCell(14).SetCellValue("per 20'");
        headerColumn2.GetCell(15).SetCellValue("per 40'");
        headerColumn2.GetCell(16).SetCellValue("per 40'HC");

        headerColumn.GetCell(17).SetCellValue("N.W/inner (" + tentl + ")");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 17, 17));

        headerColumn.GetCell(18).SetCellValue("G.W/inner (" + tentl + ")");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 18, 18));

        headerColumn.GetCell(19).SetCellValue("N.W/master (" + tentl + ")");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 19, 19));

        headerColumn.GetCell(20).SetCellValue("G.W/master (" + tentl + ")");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 20, 20));

        headerColumn.GetCell(21).SetCellValue("FOB PRICE (USD)");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 21, 21));

        headerColumn.GetCell(22).SetCellValue("port of loading");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 22, 22));

        headerColumn.GetCell(23).SetCellValue("HS Code");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 23, 23));

        headerColumn.GetCell(24).SetCellValue(String.Format("Size Product ({0})", tenkt.Equals("cm") ? "cm" : "inch"));
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 24, 26));

        headerColumn.GetCell(27).SetCellValue("M2");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 27, 27));

        headerColumn2.GetCell(24).SetCellValue("L");
        headerColumn2.GetCell(25).SetCellValue("W");
        headerColumn2.GetCell(26).SetCellValue("H");

        headerColumn.GetCell(28).SetCellValue("Packing");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row - 1, 28, 29));

        headerColumn2.GetCell(28).SetCellValue("Inner");
        headerColumn2.GetCell(29).SetCellValue("Master");

        headerColumn.GetCell(30).SetCellValue("Material");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 30, 30));
		
		headerColumn.GetCell(31).SetCellValue("Độc Quyền");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 31, 31));

        headerColumn.GetCell(32).SetCellValue("CPDG vượt chuẩn");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 32, 32));

        headerColumn.GetCell(33).SetCellValue("TT Hàng hóa");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 33, 33));

        headerColumn.GetCell(34).SetCellValue("NW2");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 34, 34));

        headerColumn.GetCell(35).SetCellValue("GW2");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 35, 35));

        headerColumn.GetCell(36).SetCellValue("VTDG2");
        s1.AddMergedRegion(new CellRangeAddress(row - 1, row, 36, 36));

        ICellStyle border = hssfworkbook.CreateCellStyle();
        border.BorderBottom = border.BorderLeft
            = border.BorderRight
            = border.BorderTop
            = NPOI.SS.UserModel.BorderStyle.Thin;
        border.WrapText = true;
        border.VerticalAlignment = VerticalAlignment.Top;

        s1.SetColumnWidth(0, 5000);
        s1.SetColumnWidth(1, 4000);
        s1.SetColumnWidth(3, 9000);
        s1.SetColumnWidth(4, 3000);

        s1.SetColumnWidth(17, 3000);
        s1.SetColumnWidth(18, 3000);
        s1.SetColumnWidth(19, 3000);
        s1.SetColumnWidth(20, 3000);
        s1.SetColumnWidth(21, 4000);

        s1.SetColumnWidth(24, 0);
        s1.SetColumnWidth(25, 0);
        s1.SetColumnWidth(26, 0);
        s1.SetColumnWidth(27, 0);
        s1.SetColumnWidth(28, 0);
        s1.SetColumnWidth(29, 0);
        s1.SetColumnWidth(30, 0);
        s1.SetColumnWidth(31, 0);
        s1.SetColumnWidth(32, 0);
        s1.SetColumnWidth(33, 0);
        s1.SetColumnWidth(34, 0);
        s1.SetColumnWidth(35, 0);
        s1.SetColumnWidth(36, 0);
        //SO
        ICellStyle cellNumBorder = hssfworkbook.CreateCellStyle();
        cellNumBorder.Alignment = HorizontalAlignment.Right;
        cellNumBorder.VerticalAlignment = VerticalAlignment.Top;
        cellNumBorder.DataFormat = HSSFUtils.CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
        cellNumBorder.BorderBottom = cellNumBorder.BorderRight = cellNumBorder.BorderLeft = cellNumBorder.BorderTop = BorderStyle.Thin;

        ICellStyle cellNumBorderCBM = hssfworkbook.CreateCellStyle();
        cellNumBorderCBM.Alignment = HorizontalAlignment.Right;
        cellNumBorderCBM.VerticalAlignment = VerticalAlignment.Top;
        cellNumBorderCBM.DataFormat = HSSFUtils.CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.000");
        cellNumBorderCBM.BorderBottom = cellNumBorderCBM.BorderRight = cellNumBorderCBM.BorderLeft = cellNumBorderCBM.BorderTop = BorderStyle.Thin;

        ICellStyle cellNumBorderKT = hssfworkbook.CreateCellStyle();
        cellNumBorderKT.Alignment = HorizontalAlignment.Right;
        cellNumBorderKT.VerticalAlignment = VerticalAlignment.Top;
        cellNumBorderKT.DataFormat = HSSFUtils.CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.0");
        cellNumBorderKT.BorderBottom = cellNumBorderKT.BorderRight = cellNumBorderKT.BorderLeft = cellNumBorderKT.BorderTop = BorderStyle.Thin;
        //SO
        
        int detailsCount = dtDetails.Rows.Count;
        var rowDt = row;
        for (int i = 0; i < detailsCount; i++)
        {
            rowDt++;
            var imgPath = ExcuteSignalRStatic.mapPathSignalR("~/" + ExcuteSignalRStatic.getImageProduct(dtDetails.Rows[i]["msp"].ToString()));
            //var image = System.Drawing.Image.FromFile(imgPath);
            lstimage.Add(new AvariablePrj.lstImage()
            {
                row = rowDt,
                column = 2,
                link = imgPath
            });

            //try
            //{
            //    String imgPath = System.IO.Path.Combine(productImagePath, dtDetails.Rows[i]["pic"].ToString() + ".jpg");
            //    System.Drawing.Image image = System.Drawing.Image.FromFile(imgPath);
            //    MemoryStream ms = new MemoryStream();
            //    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            //    IDrawing patriarch = s1.CreateDrawingPatriarch();
            //    HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 1, rowDt, 2, rowDt + 1);
            //    anchor.AnchorType = 2;

            //    int index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.JPEG);
            //    IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
            //}
            //catch
            //{ }
            
            s1.CreateRow(rowDt).CreateCell(0).SetCellValue(dtDetails.Rows[i]["msp"].ToString());
            s1.GetRow(rowDt).HeightInPoints = 50;
            s1.GetRow(rowDt).CreateCell(1).SetCellValue("");
            s1.GetRow(rowDt).CreateCell(2).SetCellValue(dtDetails.Rows[i]["ma_sanpham_khach"].ToString());
            s1.GetRow(rowDt).CreateCell(3).SetCellValue(dtDetails.Rows[i]["mota_tienganh"].ToString());
            s1.GetRow(rowDt).CreateCell(4).SetCellValue(double.Parse(dtDetails.Rows[i]["trongluong"].ToString()));
            // s1.GetRow(9 + i).CreateCell(4).SetCellType(CellType.FORMULA);
            // s1.GetRow(9 + i).GetCell(4).SetCellFormula("1 + H" + (10 + i));
            s1.GetRow(rowDt).GetCell(4).CellStyle = cellStyleRight;

            s1.GetRow(rowDt).CreateCell(5).SetCellValue(dtDetails.Rows[i]["sl_inner"].ToString());

            s1.GetRow(rowDt).CreateCell(6).SetCellValue(dtDetails.Rows[i]["master"].ToString());

            s1.GetRow(rowDt).CreateCell(7).SetCellValue(double.Parse((tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["l1"].ToString()) : double.Parse(dtDetails.Rows[i]["l1"].ToString()) / (double)2.54).ToString("#.0")));
            s1.GetRow(rowDt).GetCell(7).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(8).SetCellValue(double.Parse((tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["w1"].ToString()) : double.Parse(dtDetails.Rows[i]["w1"].ToString()) / (double)2.54).ToString("#.0")));
            s1.GetRow(rowDt).GetCell(8).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(9).SetCellValue(double.Parse((tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["h1"].ToString()) : double.Parse(dtDetails.Rows[i]["h1"].ToString()) / (double)2.54).ToString("#.0")));
            s1.GetRow(rowDt).GetCell(9).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(10).SetCellValue(double.Parse((tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["l2"].ToString()) : double.Parse(dtDetails.Rows[i]["l2"].ToString()) / (double)2.54).ToString("#.0")));
            s1.GetRow(rowDt).GetCell(10).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(11).SetCellValue(double.Parse((tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["w2"].ToString()) : double.Parse(dtDetails.Rows[i]["w2"].ToString()) / (double)2.54).ToString("#.0")));
            s1.GetRow(rowDt).GetCell(11).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(12).SetCellValue(double.Parse((tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["h2"].ToString()) : double.Parse(dtDetails.Rows[i]["h2"].ToString()) / (double)2.54).ToString("#.0")));
            s1.GetRow(rowDt).GetCell(12).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(13).SetCellValue(tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["v2"].ToString()) : double.Parse(dtDetails.Rows[i]["cbf"].ToString()));
            s1.GetRow(rowDt).GetCell(13).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(14).SetCellValue(double.Parse(dtDetails.Rows[i]["nopack_20"].ToString()));
            s1.GetRow(rowDt).GetCell(14).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(15).SetCellValue(double.Parse(dtDetails.Rows[i]["nopack"].ToString()));
            s1.GetRow(rowDt).GetCell(15).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(16).SetCellValue(double.Parse(dtDetails.Rows[i]["nopack_40hc"].ToString()));
            s1.GetRow(rowDt).GetCell(16).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(17).SetCellValue(double.Parse(dtDetails.Rows[i]["n_w_inner"].ToString()));
            s1.GetRow(rowDt).GetCell(17).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(18).SetCellValue(double.Parse(dtDetails.Rows[i]["g_w_inner"].ToString()));
            s1.GetRow(rowDt).GetCell(18).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(19).SetCellValue(double.Parse(dtDetails.Rows[i]["n_w_outer"].ToString()));
            s1.GetRow(rowDt).GetCell(19).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(20).SetCellValue(double.Parse(dtDetails.Rows[i]["g_w_outer"].ToString()));
            s1.GetRow(rowDt).GetCell(20).SetCellType(CellType.Numeric);

            s1.GetRow(rowDt).CreateCell(21).SetCellValue(double.Parse(dtDetails.Rows[i]["giafob"].ToString()));
            s1.GetRow(rowDt).GetCell(21).SetCellType(CellType.Numeric);
            s1.GetRow(rowDt).GetCell(21).CellStyle = cellNumBorder;

            s1.GetRow(rowDt).CreateCell(22).SetCellValue(dtDetails.Rows[i]["ten_cangbien"].ToString());
            s1.GetRow(rowDt).CreateCell(23).SetCellValue(double.Parse(dtDetails.Rows[i]["hscode"].ToString()));

            s1.GetRow(rowDt).CreateCell(24).SetCellValue(tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["l_cm"].ToString()) : double.Parse(dtDetails.Rows[i]["l_inch"].ToString()));
            s1.GetRow(rowDt).CreateCell(25).SetCellValue(tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["w_cm"].ToString()) : double.Parse(dtDetails.Rows[i]["w_inch"].ToString()));
            s1.GetRow(rowDt).CreateCell(26).SetCellValue(tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["h_cm"].ToString()) : double.Parse(dtDetails.Rows[i]["h_inch"].ToString()));
            s1.GetRow(rowDt).CreateCell(27).SetCellValue(double.Parse(dtDetails.Rows[i]["dientich"].ToString()));
            s1.GetRow(rowDt).CreateCell(28).SetCellValue(double.Parse(dtDetails.Rows[i]["inner_"].ToString()));
            s1.GetRow(rowDt).CreateCell(29).SetCellValue(double.Parse(dtDetails.Rows[i]["sl_outer"].ToString()));
            s1.GetRow(rowDt).CreateCell(30).SetCellValue(dtDetails.Rows[i]["thanhphan"].ToString());
            s1.GetRow(rowDt).CreateCell(31).SetCellValue(dtDetails.Rows[i]["docquyen"].ToString());

            s1.GetRow(rowDt).CreateCell(32).SetCellValue(double.Parse(dtDetails.Rows[i]["cpdg_vuotchuan"].ToString()));
            s1.GetRow(rowDt).GetCell(32).SetCellType(CellType.Numeric);
            s1.GetRow(rowDt).GetCell(32).CellStyle = cellNumBorder;

            s1.GetRow(rowDt).CreateCell(33).SetCellValue(dtDetails.Rows[i]["ten_danhmuc"].ToString());

            s1.GetRow(rowDt).CreateCell(34).SetCellValue(double.Parse(dtDetails.Rows[i]["nw2"].ToString()));
            s1.GetRow(rowDt).GetCell(34).SetCellType(CellType.Numeric);
            s1.GetRow(rowDt).GetCell(34).CellStyle = cellNumBorder;

            s1.GetRow(rowDt).CreateCell(35).SetCellValue(double.Parse(dtDetails.Rows[i]["giafob"].ToString()));
            s1.GetRow(rowDt).GetCell(35).SetCellType(CellType.Numeric);
            s1.GetRow(rowDt).GetCell(35).CellStyle = cellNumBorder;

            s1.GetRow(rowDt).CreateCell(36).SetCellValue(double.Parse(dtDetails.Rows[i]["vtdg2"].ToString()));
            s1.GetRow(rowDt).GetCell(36).SetCellType(CellType.Numeric);
            s1.GetRow(rowDt).GetCell(36).CellStyle = cellNumBorder;
        }

        rowDt = row;
        for (int i = 0; i < dtDetails.Rows.Count; i++)
        {
            rowDt++;
            for (int j = 0; j <= totalColumn; j++)
            {
                if(j == 0)
                    s1.GetRow(rowDt).GetCell(j).CellStyle = cellTextCenterBoderAll;
                else if (new int[] { 4, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 27}.Contains(j))
                {
                    s1.GetRow(rowDt).GetCell(j).CellStyle = cellStyleRight;
                }
                else
                {
                    s1.GetRow(rowDt).GetCell(j).CellStyle = border;
                    for (int nht = 7; nht <= 12; nht++)
                    {
                        s1.GetRow(rowDt).GetCell(nht).CellStyle = cellNumBorderKT;
                    }
                    s1.GetRow(rowDt).GetCell(13).CellStyle = cellNumBorderCBM;
                    s1.GetRow(rowDt).GetCell(18).CellStyle = cellNumBorder;
                    s1.GetRow(rowDt).GetCell(20).CellStyle = cellNumBorder;
                    s1.GetRow(rowDt).GetCell(21).CellStyle = cellNumBorder;
                    //--Size Product
                    s1.GetRow(rowDt).GetCell(24).CellStyle = cellNumBorderKT;
                    s1.GetRow(rowDt).GetCell(25).CellStyle = cellNumBorderKT;
                    s1.GetRow(rowDt).GetCell(26).CellStyle = cellNumBorderKT;
                }
            }
        }

        row = rowDt + 1;

        s1.CreateRow(row).CreateCell(0).SetCellValue("Color References:");
        int lineNum = 2;
        for (int i = 0; i < dtDistinct.Rows.Count; i++)
        {
            var rowChungLoai = dtDistinct.Rows[i];
            var items = dtReferences.AsEnumerable().Where(s => s.Field<string>("chungloai") == rowChungLoai["chungloai"].ToString()).ToList();

            row++;
            s1.CreateRow(row);
            s1.GetRow(row).CreateCell(1).SetCellValue(rowChungLoai["chungloai"].ToString() + " - " + rowChungLoai["ta_ngan"].ToString());
            s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 3));
            row++;
            s1.CreateRow(row);

            var rowMS = row;
            for (int j = 0; j < items.Count; j++)
            {
                if (j + 1 % 4 == 0 | j == 0)
                {
                    for (var iR = 0; iR < lineNum; iR++)
                    {
                        row++;
                        s1.CreateRow(row);
                        if (iR == lineNum - 2)
                            s1.GetRow(row).HeightInPoints = 70;
                    }
                }

                s1.GetRow(rowMS-1).CreateCell(5 + j * 3).SetCellValue("Color " + items[j]["codemau"].ToString());
                s1.GetRow(rowMS).CreateCell(5 + j * 3).SetCellValue(items[j]["mau"].ToString());
                try
                {
                    var imgPath = ExcuteSignalRStatic.getImageColor(items[j]["url"].ToString());
                    if (imgPath.StartsWith("Error"))
                    {
                        s1.GetRow(rowMS + 1).CreateCell(5 + j * 3).SetCellValue(imgPath);
                    }
                    else
                    {
                        imgPath = ExcuteSignalRStatic.mapPathSignalR("~/" + imgPath);

                        var image = System.Drawing.Image.FromFile(imgPath);
                        var ms = new MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                        var patriarch = s1.CreateDrawingPatriarch();
                        var anchor = new HSSFClientAnchor(0, 0, 0, 0
                            , 5 + j * 3
                            , rowMS + 1
                            , 7 + j * 3
                            , rowMS + 2
                        );
                        anchor.AnchorType = AnchorType.MoveDontResize;

                        int index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.JPEG);
                        IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
                    }
                }
                catch
                { }
            }
        }

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Remark:");
        s1.GetRow(row).HeightInPoints = 30;
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue("All offered FOB prices are requested for FCL container - including packing as our above-mentioned  standard; Labeling in black & white color ( if required); Bill of lading & seal fee, THC, ENS charges from the shipping line/forwarder and normal container fumigation & ventilation fees with Methyl Bromide (if any).");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("");
        s1.GetRow(row).CreateCell(1).SetCellValue(
           @"The fee of Fumigation and Ventilation under AFAS standard for all containers imported to Australia (if required) will be charged to customers: 250 USD/20"" container; 350 USD/40"", 40”HC, 45” container."
        );
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("");
        s1.GetRow(row).CreateCell(1).SetCellValue("Other arising costs (if any) will be charged to customers.");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("");
        s1.GetRow(row).CreateCell(1).SetCellValue("All information of weight, loadability are estimated only.");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Minimum order:");
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue("1x40” container ( a full container)");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("");
        s1.GetRow(row).CreateCell(1).SetCellValue("Order of 1x20\" can be accepted, but the extra charge of 400 USD will be added into the proforma invoice / commercial invoice");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Number of items for:");
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue("20' cont.");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.GetRow(row).CreateCell(2).SetCellValue("12 different items (max.) which is offered under the same delivery terms/port of loading.");
        s1.GetRow(row).GetCell(2).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 15));
        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("");
        s1.GetRow(row).CreateCell(1).SetCellValue("40' cont.");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.GetRow(row).CreateCell(2).SetCellValue("24 different items (max.) which is offered under the same delivery terms/port of loading.");
        s1.GetRow(row).GetCell(2).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("");
        s1.GetRow(row).CreateCell(1).SetCellValue("The extra charge of 5%-10% will be added into the prices if the number of items in a container is over the above-mentioned requirement.");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("MOQ/item/color:");
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue(dtDetails.Rows[0]["moq_item_color"].ToString());
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Shipment time:");
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue(String.Format("Normally, {0} - {1} days after receipt of original L/C or 30% deposit", dtDetails.Rows[0]["shipmenttime"].ToString(), dtDetails.Rows[0]["shipmenttime2"].ToString()));
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("");
        s1.GetRow(row).CreateCell(1).SetCellValue(String.Format("In peak season, the shipment time can be longer and is quoted under rules of FIRST IN FIRST OUT ( FIFO) "));
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Port of loading:");
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue(String.Format("{0}", "TAM HIEP or DA NANG PORT"));
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Payment terms:");
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue("TTR 30% deposit, 70% after shipment, or L/C at sight ( value over USD 20,000)");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Bank Details:");
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue("In favor of VINH GIA COMPANY LIMITED");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(1).SetCellValue("Account No. : 046.137.3860862");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(1).SetCellValue("At bank: JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIET NAM - TAN BINH DUONG BRANCH");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(1).SetCellValue("NO 16, LE TRONG TAN STREET, BINH DUONG 2 QUARTER, DI AN WARD, HO CHI MINH CITY, VIET NAM");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(1).SetCellValue("Tel: (84-274) 3792 158- Fax: (84-274) 3793 970,");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        //row++;
        //s1.CreateRow(row).CreateCell(1).SetCellValue("Fax: (84-028) 38297228,");
        //s1.GetRow(row).GetCell(1).CellStyle = celltext;
        //s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(1).SetCellValue("Swift code: BFTVVNVX.");
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Validity  until:");
        s1.GetRow(row).GetCell(0).CellStyle = celltext;
        s1.GetRow(row).CreateCell(1).SetCellValue(DateTime.Parse(dtDetails.Rows[0]["ngayhethan"].ToString()).ToString("dd/MMM/yyyy"));
        s1.GetRow(row).GetCell(1).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 15));

        s1 = config.PrintExcel((HSSFSheet)s1, row);
        s1.PrintSetup.Landscape = true;

        if (typeReport == "pdf")
            s1.PrintSetup.PaperSize = (short)PaperSize.A4;

        return hssfworkbook;
    }

    public String C_baogia_id
    {
        get { return c_baogia_id; }
        set { c_baogia_id = value; }
    }

    public BaoGiaReport(String c_baogia_id, String logoPath, String productImagePath)
    {
        this.c_baogia_id = c_baogia_id;
        this.logoPath = logoPath;
        this.productImagePath = productImagePath;
    }
}
