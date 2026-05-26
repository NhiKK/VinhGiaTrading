using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Objects
/// </summary>
public class Objects
{
    public Objects()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public partial class Json_product
    {
        public string ma_sanpham;
        public string gia_fob;
        public string image;
        public string soluong;
        public string detai_mausac;
    }

    public class BaoGiaMauReport_QRCode
    {
        private String c_baogia_id, logoPath, productImagePath, type;

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

        private LinqDBDataContext db = null;
        public HSSFWorkbook CreateWBQuotation()
        {
            string table = "c_baogia", tableDT = "c_chitietbaogia";
            if (type == "qr")
            {
                table = "c_baogiaqr";
                tableDT = "c_chitietbaogiaqr";
            }

            string orderby = UserUtils.get_sapxep_qodetail(null, c_baogia_id, "B.ma_sanpham", "order by B.ma_sanpham asc", db);

            String selectDetails = String.Format(@"
				SELECT *, CASE WHEN  B.sl_inner = '0 0' and B.master like N'%ctn%'
						THEN round(B.n_w_outer + ((B.l2 + B.w2) * (B.w2 + B.h2))/5400,0)

						WHEN B.sl_inner like N'%ctn%' and B.master like N'%ctn%'
						THEN round((B.g_w_inner * (B.sl_outer/ B.inner_)) +  (((B.l2 + B.w2) * (B.w2 + B.h2))/5400),0)

						WHEN B.sl_inner like N'%ctn%' and B.master like N'%pal%'
						THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 20,0)

						WHEN B.sl_inner like N'%ctn%' and B.master like N'%crt%'
						THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 5,0)

						WHEN B.sl_inner like N'%bun%' and B.master like N'%crt%'
						THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 5,0)

						WHEN B.sl_inner like N'%bun%' and B.master like N'%pal%'
						THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 20,0)

						WHEN B.sl_inner = '0 0' and B.master like N'%pal%'
						THEN round(B.n_w_outer + 25,0)

						WHEN B.sl_inner = '0 0' and B.master like N'%bun%'
						THEN round(B.n_w_outer + 0.5,0)
						
						WHEN B.sl_inner = '0 0' and B.master like N'%crt%'
						THEN round(B.n_w_outer + 5,0)

						ELSE 0 END as g_w_outer
				FROM(
				SELECT *,CASE WHEN  A.sl_inner like N'%ctn%'  
						THEN A.n_w_inner + ((A.l1+A.w1)*(A.w1+A.h1)/5400)
						WHEN A.sl_inner like N'%bun%'  
						THEN A.n_w_inner + 0.5
						WHEN A.sl_inner like N'%crt%'  
						THEN A.n_w_inner + 3
						ELSE 0 END as g_w_inner
				FROM(
				select 
	            dtkd.ten_dtkd, dtkd.diachi, dtkd.tel, dtkd.fax, bg.ngaybaogia
	            , sp.ma_sanpham as msp, (case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) else SUBSTRING(sp.ma_sanpham,0,12) end) as pic, sp.ma_sanpham, ctbg.ma_sanpham_khach
	            , (CASE 
						WHEN kt.ten_kichthuoc = 'inch' then dbo.f_taomotainchQoPo(sp.md_sanpham_id) 
						ELSE ctbg.mota_tienganh
					END) as mota_tienganh
				,  (select dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id)) as trongluong
                ,  tl.ten_trongluong, dg.sl_inner as inner_, dg.sl_outer
                , (CAST(dg.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner)) as sl_inner
	            , (CAST(dg.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer)) as master
                , dg.ten_donggoi, ctbg.l1, ctbg.w1, ctbg.h1, ctbg.l2, ctbg.w2, ctbg.h2, ctbg.v2, round(ctbg.v2 * 35.31466672, 2)  as cbf
	            , dg.soluonggoi_ctn as nopack, dg.soluonggoi_ctn_20 as nopack_20, dg.soluonggoi_ctn_40hc as nopack_40hc, ctbg.giafob, ctbg.soluong
	            , sp.l_cm, sp.w_cm, sp.h_cm, sp.l_inch, sp.w_inch, sp.h_inch
	            , bg.shipmenttime, bg.shipmenttime + 15 as shipmenttime2, cb.ten_cangbien
	            , bg.ngayhethan, kt.ten_kichthuoc, bg.moq_item_color, hscode.hscode
				, dg.sl_inner * (SELECT dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id))  as n_w_inner
				, dg.sl_outer * (SELECT dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id)) as n_w_outer
            from 
	            {2} bg
				left join {3} ctbg ON bg.{2}_id = ctbg.{2}_id
	            left join md_doitackinhdoanh dtkd ON bg.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
				left join md_sanpham sp ON ctbg.md_sanpham_id = sp.md_sanpham_id
				left join md_hscode hscode ON sp.md_hscode_id = hscode.md_hscode_id
	            left join md_donggoi dg ON ctbg.md_donggoi_id = dg.md_donggoi_id
	            left join md_cangbien cb ON sp.md_cangbien_id = cb.md_cangbien_id
                left join md_kichthuoc kt ON bg.md_kichthuoc_id = kt.md_kichthuoc_id
                left join md_trongluong tl ON bg.md_trongluong_id = tl.md_trongluong_id
            where bg.{2}_id = N'{0}')A)B {1}", (c_baogia_id == null ? "" : c_baogia_id), orderby, table, tableDT);


            String selectRefereces = String.Format(@"SELECT 
	                                                  (select SUBSTRING(filter, 1, 2) + '-' + ta_ngan as ta_ngan from md_chungloai cl where cl.code_cl = SUBSTRING(filter, 1, 2)) as ta_ngan, SUBSTRING(filter, 1, 2) as chungloai, SUBSTRING(filter, 4, 2) as codemau, mau , url
                                                   FROM 
	                                                   {1} bg
														inner join md_color_reference color on bg.{1}_id = color.c_baogia_id
                                                   WHERE
                                                       color.hoatdong = 1
                                                       AND bg.{1}_id = N'{0}'", (c_baogia_id == null ? "" : c_baogia_id), table);

            String selectDistinct = String.Format(@"SELECT distinct chungloai
                                                    FROM(
                                                        SELECT 
	                                                        (select ta_ngan from md_chungloai cl where cl.code_cl = SUBSTRING(filter, 1, 2)) as ta_ngan, SUBSTRING(filter, 1, 2) as chungloai, SUBSTRING(filter, 4, 2) as codemau, mau , url
                                                        FROM 
	                                                        {1} bg
															inner join md_color_reference color on bg.{1}_id = color.c_baogia_id
                                                        WHERE
															color.hoatdong = 1
                                                            AND bg.{1}_id = N'{0}'
                                                    ) as tmp", (c_baogia_id == null ? "" : c_baogia_id), table);

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
            }
        }

        private HSSFWorkbook CreateHSSFWorkbook(DataTable dtDetails, DataTable dtDistinct, DataTable dtReferences)
        {
            string tenkt = dtDetails.Rows[0]["ten_kichthuoc"].ToString().ToLower();
            string tentl = dtDetails.Rows[0]["ten_trongluong"].ToString().ToLower();
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
            System.Drawing.Image logo = System.Drawing.Image.FromFile(logoPath);
            MemoryStream mslogo = new MemoryStream();
            logo.Save(mslogo, System.Drawing.Imaging.ImageFormat.Jpeg);

            IDrawing patriarchlogo = s1.CreateDrawingPatriarch();
            HSSFClientAnchor anchorlogo = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 1, 3);
            anchorlogo.AnchorType = AnchorType.DontMoveAndResize;

            int indexlogo = hssfworkbook.AddPicture(mslogo.ToArray(), PictureType.JPEG);
            IPicture signaturePicturelogo = patriarchlogo.CreatePicture(anchorlogo, indexlogo);

            // set A1
            s1.CreateRow(0).CreateCell(0).SetCellValue("VINH GIA COMPANY LIMITED");

            // set A2
            s1.CreateRow(1).CreateCell(0).SetCellValue("Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam");

            // set A3
            s1.CreateRow(2).CreateCell(0).SetCellValue("Tel: (84-235) 3567393   Fax: (84-235) 3567494");

            // set A4
            s1.CreateRow(3).CreateCell(0).SetCellValue("QUOTATION");

            IFont fontBold = hssfworkbook.CreateFont();
            fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            ICellStyle cellBold = hssfworkbook.CreateCellStyle();
            cellBold.SetFont(fontBold);

            ICellStyle cellBoldCenter = hssfworkbook.CreateCellStyle();
            cellBoldCenter.Alignment = HorizontalAlignment.Center;
            cellBoldCenter.VerticalAlignment = VerticalAlignment.Top;
            cellBoldCenter.SetFont(fontBold);

            // Style of Company Name
            ICellStyle cellStyleCompany = hssfworkbook.CreateCellStyle();
            cellStyleCompany.Alignment = HorizontalAlignment.Center;
            cellStyleCompany.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 15));

            // Style of Company Information 
            ICellStyle cellStyleInfomation = hssfworkbook.CreateCellStyle();
            cellStyleInfomation.Alignment = HorizontalAlignment.Center;
            cellStyleInfomation.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 12));

            // Style of Title
            ICellStyle cellStyleTitle = hssfworkbook.CreateCellStyle();
            cellStyleTitle.Alignment = HorizontalAlignment.Center;
            cellStyleTitle.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 18));

            // Style of Company Information Underline
            ICellStyle cellStyleInfomationUnderline = hssfworkbook.CreateCellStyle();
            cellStyleInfomationUnderline.Alignment = HorizontalAlignment.Center;
            cellStyleInfomationUnderline.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 12, true));



            s1.GetRow(0).GetCell(0).CellStyle = cellStyleCompany;
            s1.GetRow(1).GetCell(0).CellStyle = cellStyleInfomation;
            s1.GetRow(2).GetCell(0).CellStyle = cellStyleInfomationUnderline;
            s1.GetRow(3).GetCell(0).CellStyle = cellStyleTitle;

            s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 9));
            s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 9));
            s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 9));
            s1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 9));

            s1.CreateRow(4).CreateCell(0).SetCellValue("CUSTOMER NAME:");
            s1.GetRow(4).GetCell(0).CellStyle = cellBold;

            s1.GetRow(4).CreateCell(1).SetCellValue(dtDetails.Rows[0]["ten_dtkd"].ToString());
            s1.GetRow(4).GetCell(1).CellStyle = cellBold;


            s1.CreateRow(5).CreateCell(0).SetCellValue("ADDRESS:");
            s1.GetRow(5).GetCell(0).CellStyle = cellBold;

            s1.GetRow(5).CreateCell(1).SetCellValue(dtDetails.Rows[0]["diachi"].ToString() + " Tel:" + dtDetails.Rows[0]["tel"].ToString() + " Fax:" + dtDetails.Rows[0]["fax"].ToString());
            s1.GetRow(5).GetCell(1).CellStyle = cellBold;

            s1.GetRow(4).CreateCell(8).SetCellValue("Date:");



            s1.GetRow(4).GetCell(8).CellStyle = cellBold;

            s1.GetRow(4).CreateCell(9).SetCellValue(DateTime.Parse(dtDetails.Rows[0]["ngaybaogia"].ToString()).ToString("dd/MMM/yyyy"));
            s1.GetRow(4).GetCell(9).CellStyle = cellBold;

            IRow headerColumn = s1.CreateRow(6);
            IRow headerColumn2 = s1.CreateRow(7);

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

            for (int i = 0; i <= 9; i++)
            {
                headerColumn.CreateCell(i).CellStyle = hStyle;
                headerColumn2.CreateCell(i).CellStyle = hStyle;
            }

            headerColumn.GetCell(0).SetCellValue("Item No");
            s1.AddMergedRegion(new CellRangeAddress(6, 7, 0, 0));

            headerColumn.GetCell(1).SetCellValue("Picture - Only for shape ref.");
            s1.AddMergedRegion(new CellRangeAddress(6, 7, 1, 1));

            headerColumn.GetCell(2).SetCellValue("Description");
            s1.AddMergedRegion(new CellRangeAddress(6, 7, 2, 2));

            headerColumn.GetCell(3).SetCellValue("Packing");
            s1.AddMergedRegion(new CellRangeAddress(6, 6, 3, 4));

            headerColumn2.GetCell(3).SetCellValue("Inner");
            headerColumn2.GetCell(4).SetCellValue("Master");

            headerColumn.GetCell(5).SetCellValue(String.Format("PACK {0}", tenkt.Equals("cm") ? "CBM" : "CBF"));
            s1.AddMergedRegion(new CellRangeAddress(6, 7, 5, 5));

            headerColumn.GetCell(6).SetCellValue("No. of master packages per 40'' container");
            s1.AddMergedRegion(new CellRangeAddress(6, 7, 6, 6));

            headerColumn.GetCell(7).SetCellValue("FOB PRICE (USD)");
            s1.AddMergedRegion(new CellRangeAddress(6, 7, 7, 7));

            headerColumn.GetCell(8).SetCellValue("Port of loading");
            s1.AddMergedRegion(new CellRangeAddress(6, 7, 8, 8));

            headerColumn.GetCell(9).SetCellValue("Quantity");
            s1.AddMergedRegion(new CellRangeAddress(6, 7, 9, 9));

            HSSFSheet hssfsheet = (HSSFSheet)s1;

            Excel_Format ex_fm = new Excel_Format(hssfworkbook);

            ICellStyle cellStyleCenter = ex_fm.getcell(10, false, true, "LRTB", "C", "T");
            ICellStyle cellStyleLeft = ex_fm.getcell(10, false, true, "LRTB", "L", "T");
            ICellStyle cellStyleNumber2 = ex_fm.cell_decimal_2(10, false, true, "LRTB", "C", "T");
            ICellStyle cellStyleNumber3 = ex_fm.cell_decimal_3(10, false, true, "LRTB", "C", "T");

            ICellStyle border = hssfworkbook.CreateCellStyle();
            border.BorderBottom = border.BorderLeft
                = border.BorderRight
                = border.BorderTop
                = NPOI.SS.UserModel.BorderStyle.Thin;
            border.WrapText = true;
            border.VerticalAlignment = VerticalAlignment.Top;

            s1.SetColumnWidth(0, 5000);
            s1.SetColumnWidth(1, 4000);
            s1.SetColumnWidth(2, 9000);
            s1.SetColumnWidth(3, 3000);
            s1.SetColumnWidth(4, 3000);
            s1.SetColumnWidth(5, 3000);
            s1.SetColumnWidth(6, 3000);
            s1.SetColumnWidth(7, 4000);
            s1.SetColumnWidth(8, 4000);
            s1.SetColumnWidth(9, 4000);

            ICellStyle cellNumBorder = hssfworkbook.CreateCellStyle();
            cellNumBorder.Alignment = HorizontalAlignment.Right;
            cellNumBorder.VerticalAlignment = VerticalAlignment.Top;
            cellNumBorder.DataFormat = HSSFUtils.CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
            cellNumBorder.BorderBottom = cellNumBorder.BorderRight = cellNumBorder.BorderLeft = cellNumBorder.BorderTop = BorderStyle.Thin;

            int detailsCount = dtDetails.Rows.Count;
            for (int i = 0; i < detailsCount; i++)
            {
                try
                {
                    String imgPath = System.IO.Path.Combine(productImagePath, dtDetails.Rows[i]["pic"].ToString() + ".jpg");
                    System.Drawing.Image image = System.Drawing.Image.FromFile(imgPath);
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    IDrawing patriarch = s1.CreateDrawingPatriarch();
                    HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 1, 8 + i, 2, 9 + i);
                    anchor.AnchorType = AnchorType.MoveDontResize;

                    int index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.JPEG);
                    IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
                }
                catch
                { }

                s1.CreateRow(8 + i).CreateCell(0).SetCellValue(dtDetails.Rows[i]["ma_sanpham"].ToString());
                s1.GetRow(8 + i).HeightInPoints = 50;
                s1.GetRow(8 + i).CreateCell(1).SetCellValue("");
                s1.GetRow(8 + i).CreateCell(2).SetCellValue(dtDetails.Rows[i]["mota_tienganh"].ToString());

                s1.GetRow(8 + i).CreateCell(3).SetCellValue(dtDetails.Rows[i]["sl_inner"].ToString());
                s1.GetRow(8 + i).CreateCell(4).SetCellValue(dtDetails.Rows[i]["master"].ToString());

                s1.GetRow(8 + i).CreateCell(5).SetCellValue(tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["v2"].ToString()) : double.Parse(dtDetails.Rows[i]["cbf"].ToString()));
                s1.GetRow(8 + i).GetCell(5).SetCellType(CellType.Numeric);

                s1.GetRow(8 + i).CreateCell(6).SetCellValue(double.Parse(dtDetails.Rows[i]["nopack"].ToString()));
                s1.GetRow(8 + i).GetCell(6).SetCellType(CellType.Numeric);

                s1.GetRow(8 + i).CreateCell(7).SetCellValue(double.Parse(dtDetails.Rows[i]["giafob"].ToString()));
                s1.GetRow(8 + i).GetCell(7).SetCellType(CellType.Numeric);

                s1.GetRow(8 + i).CreateCell(8).SetCellValue(dtDetails.Rows[i]["ten_cangbien"].ToString());

                s1.GetRow(8 + i).CreateCell(9).SetCellValue(int.Parse(dtDetails.Rows[i]["soluong"].ToString()));
                s1.GetRow(8 + i).GetCell(9).SetCellType(CellType.Numeric);
            }

            for (int i = 0; i < dtDetails.Rows.Count; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    if (j == 2)
                    {
                        s1.GetRow(8 + i).GetCell(j).CellStyle = cellStyleLeft;
                    }
                    else if (j == 5)
                    {
                        s1.GetRow(8 + i).GetCell(j).CellStyle = cellStyleNumber3;
                    }
                    else if (j == 7)
                    {
                        s1.GetRow(8 + i).GetCell(j).CellStyle = cellStyleNumber2;
                    }
                    else
                    {
                        s1.GetRow(8 + i).GetCell(j).CellStyle = cellStyleCenter;
                    }
                }
            }

            int totalLine = 0;
            s1.CreateRow(totalLine * 8 + 10 + detailsCount).CreateCell(0).SetCellValue("Minimum order:");
            s1.CreateRow(totalLine * 8 + 11 + detailsCount).CreateCell(0).SetCellValue("Shipment time:");
            s1.CreateRow(totalLine * 8 + 12 + detailsCount).CreateCell(0).SetCellValue("Payment terms:");
            s1.CreateRow(totalLine * 8 + 13 + detailsCount).CreateCell(0).SetCellValue("Validity  until:");

            ICellStyle wrap = hssfworkbook.CreateCellStyle();
            wrap.WrapText = true;

            s1.GetRow(totalLine * 8 + 10 + detailsCount).CreateCell(1).SetCellValue("1x40” container ( a full container)");
            s1.GetRow(totalLine * 8 + 11 + detailsCount).CreateCell(1).SetCellValue(String.Format("{0} - {1} days after receipt of original L/C or 30% deposit", dtDetails.Rows[0]["shipmenttime"].ToString(), dtDetails.Rows[0]["shipmenttime2"].ToString()));
            s1.GetRow(totalLine * 8 + 12 + detailsCount).CreateCell(1).SetCellValue("TTR 30% deposit, 70% after shipment, or L/C at sight ( value over USD 20,000)");
            s1.GetRow(totalLine * 8 + 13 + detailsCount).CreateCell(1).SetCellValue(DateTime.Parse(dtDetails.Rows[0]["ngayhethan"].ToString()).ToString("dd/MMM/yyyy"));

            return hssfworkbook;
        }

        public String C_baogia_id
        {
            get { return c_baogia_id; }
            set { c_baogia_id = value; }
        }

        public BaoGiaMauReport_QRCode(LinqDBDataContext dbP, String c_baogia_id, String logoPath, String productImagePath, params object[] args)
        {
            db = dbP;
            this.c_baogia_id = c_baogia_id;
            this.logoPath = logoPath;
            this.productImagePath = productImagePath;
            if (args.Length > 0)
                this.type = args[0].ToString();
            else
                this.type = "";
        }
    }

    public class Inquiry_QRCode
    {
        private String logoPath, productImagePath, type, customer, address, tel, fax;
        private List<Json_product> products;
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

        private LinqDBDataContext db = null;
        public HSSFWorkbook CreateWBQuotation()
        {
            var arr = products.Select(s => s.ma_sanpham).ToList();
            string strSps = string.Join("','", arr);

            string sql = string.Format(@"
                select 
                    A.*,
                    cast(A.sl_inner as nvarchar(32)) + ' ' + A.dvtinner as [inner],
                    cast(A.sl_outer as nvarchar(32)) + ' ' + A.dvtouter as [master]
                from (
                    select 
                        sp.ma_sanpham,
                        sp.mota_tienganh,
                        dg.sl_inner, 
                        (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner) as dvtinner,
                        dg.sl_outer, 
                        (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer) as dvtouter,
                        dg.soluonggoi_ctn as nopack,
                        cb.ten_cangbien,
                        substring(sp.ma_sanpham, 0, 9) as kd,
                        substring(sp.ma_sanpham, 9, 3) as dt,
                        substring(sp.ma_sanpham, 12, 3) as ms,
                        (case when SUBSTRING(sp.ma_sanpham, 10, 1) != 'F' then SUBSTRING(sp.ma_sanpham, 0, 9) else SUBSTRING(sp.ma_sanpham, 0, 12) end) as pic
                    from md_sanpham (nolock) sp
                        left join md_donggoisanpham (nolock) dgsp on dgsp.md_sanpham_id = sp.md_sanpham_id
                        left join md_donggoi (nolock) dg on dg.md_donggoi_id = dgsp.md_donggoi_id
                        left join md_cangxuathang (nolock) cxh on cxh.md_sanpham_id = sp.md_sanpham_id
                        left join md_cangbien (nolock) cb on cb.md_cangbien_id = cxh.md_cangbien_id
                    where 
                        1=1
                        and ma_sanpham in ('{0}')
                        and dgsp.macdinh = 1
                        and cxh.macdinh = 1
                )A
                order by A.kd, A.ms, A.dt
            ", strSps);
            DataTable dtDetails = mdbc.GetData(sql);
            if (dtDetails.Rows.Count != 0)
            {
                HSSFWorkbook hssfworkbook = this.CreateHSSFWorkbook(dtDetails);
                return hssfworkbook;
            }
            else
            {
                throw new Exception("Báo giá không có dữ liệu!");
            }
        }

        private HSSFWorkbook CreateHSSFWorkbook(DataTable dtDetails)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
            System.Drawing.Image logo = System.Drawing.Image.FromFile(logoPath);
            MemoryStream mslogo = new MemoryStream();
            logo.Save(mslogo, System.Drawing.Imaging.ImageFormat.Jpeg);

            IDrawing patriarchlogo = s1.CreateDrawingPatriarch();
            HSSFClientAnchor anchorlogo = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 1, 3);
            anchorlogo.AnchorType = AnchorType.DontMoveAndResize;

            int indexlogo = hssfworkbook.AddPicture(mslogo.ToArray(), PictureType.JPEG);
            IPicture signaturePicturelogo = patriarchlogo.CreatePicture(anchorlogo, indexlogo);

            s1.CreateRow(0).CreateCell(0).SetCellValue("VINH GIA COMPANY LIMITED");

            s1.CreateRow(1).CreateCell(0).SetCellValue("Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam");

            s1.CreateRow(2).CreateCell(0).SetCellValue("Tel: (84-235) 3567393   Fax: (84-235) 3567494");
            
            s1.CreateRow(3).CreateCell(0).SetCellValue("QUOTATION REQUEST");

            s1.CreateRow(4).CreateCell(0).SetCellValue("Please send us a quotation for the below-mentioned items through our following contact:");

            IFont fontBold = hssfworkbook.CreateFont();
            fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            ICellStyle cellBold = hssfworkbook.CreateCellStyle();
            cellBold.SetFont(fontBold);

            ICellStyle cellBoldCenter = hssfworkbook.CreateCellStyle();
            cellBoldCenter.Alignment = HorizontalAlignment.Center;
            cellBoldCenter.VerticalAlignment = VerticalAlignment.Top;
            cellBoldCenter.SetFont(fontBold);

            // Style of Company Name
            ICellStyle cellStyleCompany = hssfworkbook.CreateCellStyle();
            cellStyleCompany.Alignment = HorizontalAlignment.Center;
            cellStyleCompany.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 15));

            // Style of Company Information 
            ICellStyle cellStyleInfomation = hssfworkbook.CreateCellStyle();
            cellStyleInfomation.Alignment = HorizontalAlignment.Center;
            cellStyleInfomation.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 12));

            // Style of Title
            ICellStyle cellStyleTitle = hssfworkbook.CreateCellStyle();
            cellStyleTitle.Alignment = HorizontalAlignment.Center;
            cellStyleTitle.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 18));

            // Style of Company Information Underline
            ICellStyle cellStyleInfomationUnderline = hssfworkbook.CreateCellStyle();
            cellStyleInfomationUnderline.Alignment = HorizontalAlignment.Center;
            cellStyleInfomationUnderline.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 12, true));



            s1.GetRow(0).GetCell(0).CellStyle = cellStyleCompany;
            s1.GetRow(1).GetCell(0).CellStyle = cellStyleInfomation;
            s1.GetRow(2).GetCell(0).CellStyle = cellStyleInfomationUnderline;
            s1.GetRow(3).GetCell(0).CellStyle = cellStyleTitle;
            s1.GetRow(4).GetCell(0).CellStyle = cellStyleInfomation;

            s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));
            s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 6));
            s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));
            s1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 6));
            s1.AddMergedRegion(new CellRangeAddress(4, 4, 0, 6));

            var rowTT = 5;
            s1.CreateRow(rowTT).CreateCell(0).SetCellValue("CUSTOMER NAME:");
            s1.GetRow(rowTT).GetCell(0).CellStyle = cellBold;

            s1.GetRow(rowTT).CreateCell(1).SetCellValue(customer);
            s1.GetRow(rowTT).GetCell(1).CellStyle = cellBold;


            s1.CreateRow(rowTT + 1).CreateCell(0).SetCellValue("ADDRESS:");
            s1.GetRow(rowTT + 1).GetCell(0).CellStyle = cellBold;

            string val51 = "";
            if (!string.IsNullOrEmpty(address))
                val51 += address;

            if (!string.IsNullOrEmpty(tel))
            {
                if (val51.Length > 0)
                    val51 += ",";
                val51 += " Tel: " + tel;
            }

            if (!string.IsNullOrEmpty(fax))
            {
                if (val51.Length > 0)
                    val51 += ",";
                val51 += " Fax: " + tel;
            }

            s1.GetRow(rowTT + 1).CreateCell(1).SetCellValue(val51);
            s1.AddMergedRegion(new CellRangeAddress(5, 5, 1, 3));
            s1.GetRow(rowTT + 1).GetCell(1).CellStyle = cellBold;

            s1.GetRow(rowTT).CreateCell(5).SetCellValue("Date:");
            s1.GetRow(rowTT).CreateCell(6).SetCellValue(DateTime.Now.ToString("dd/MMM/yyyy"));
            s1.GetRow(rowTT).GetCell(6).CellStyle = cellBold;

            var rowTTH1 = rowTT + 3;
            var rowTTH2 = rowTT + 4;
            IRow headerColumn = s1.CreateRow(rowTTH1);
            IRow headerColumn2 = s1.CreateRow(rowTTH2);

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

            for (int i = 0; i <= 6; i++)
            {
                headerColumn.CreateCell(i).CellStyle = hStyle;
                headerColumn2.CreateCell(i).CellStyle = hStyle;
            }

            headerColumn.GetCell(0).SetCellValue("Item No");
            s1.AddMergedRegion(new CellRangeAddress(rowTTH1, rowTTH2, 0, 0));

            headerColumn.GetCell(1).SetCellValue("Picture - Only for shape ref.");
            s1.AddMergedRegion(new CellRangeAddress(rowTTH1, rowTTH2, 1, 1));

            headerColumn.GetCell(2).SetCellValue("Description");
            s1.AddMergedRegion(new CellRangeAddress(rowTTH1, rowTTH2, 2, 2));

            headerColumn.GetCell(3).SetCellValue("Packing");
            s1.AddMergedRegion(new CellRangeAddress(rowTTH1, rowTTH1, 3, 4));

            headerColumn2.GetCell(3).SetCellValue("Inner");
            headerColumn2.GetCell(4).SetCellValue("Master");  

            headerColumn.GetCell(5).SetCellValue("No. of master packages per 40'' container");
            s1.AddMergedRegion(new CellRangeAddress(rowTTH1, rowTTH2, 5, 5));

            headerColumn.GetCell(6).SetCellValue("Port of loading");
            s1.AddMergedRegion(new CellRangeAddress(rowTTH1, rowTTH2, 6, 6));

            HSSFSheet hssfsheet = (HSSFSheet)s1;

            Excel_Format ex_fm = new Excel_Format(hssfworkbook);

            ICellStyle cellStyleCenter = ex_fm.getcell(10, false, true, "LRTB", "C", "T");
            ICellStyle cellStyleLeft = ex_fm.getcell(10, false, true, "LRTB", "L", "T");
            ICellStyle cellStyleNumber2 = ex_fm.cell_decimal_2(10, false, true, "LRTB", "C", "T");
            ICellStyle cellStyleNumber3 = ex_fm.cell_decimal_3(10, false, true, "LRTB", "C", "T");

            ICellStyle border = hssfworkbook.CreateCellStyle();
            border.BorderBottom = border.BorderLeft
                = border.BorderRight
                = border.BorderTop
                = NPOI.SS.UserModel.BorderStyle.Thin;
            border.WrapText = true;
            border.VerticalAlignment = VerticalAlignment.Top;

            s1.SetColumnWidth(0, 5000);
            s1.SetColumnWidth(1, 4000);
            s1.SetColumnWidth(2, 9000);
            s1.SetColumnWidth(3, 3000);
            s1.SetColumnWidth(4, 3000);
            s1.SetColumnWidth(5, 3000);
            s1.SetColumnWidth(6, 3000);

            ICellStyle cellNumBorder = hssfworkbook.CreateCellStyle();
            cellNumBorder.Alignment = HorizontalAlignment.Right;
            cellNumBorder.VerticalAlignment = VerticalAlignment.Top;
            cellNumBorder.DataFormat = HSSFUtils.CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
            cellNumBorder.BorderBottom = cellNumBorder.BorderRight = cellNumBorder.BorderLeft = cellNumBorder.BorderTop = BorderStyle.Thin;

            int detailsCount = dtDetails.Rows.Count;
            for (int i = 0; i < detailsCount; i++)
            {
                var rowTTDT = rowTT + 5 + i;
                try
                {
                    String imgPath = Path.Combine(productImagePath, dtDetails.Rows[i]["pic"].ToString() + ".jpg");
                    System.Drawing.Image image = System.Drawing.Image.FromFile(imgPath);
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    IDrawing patriarch = s1.CreateDrawingPatriarch();
                    HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 1, rowTTDT, 2, rowTTDT + 1);
                    anchor.AnchorType = AnchorType.MoveDontResize;

                    int index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.JPEG);
                    IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
                }
                catch
                { }

                
                s1.CreateRow(rowTTDT).CreateCell(0).SetCellValue(dtDetails.Rows[i]["ma_sanpham"].ToString());
                s1.GetRow(rowTTDT).HeightInPoints = 50;
                s1.GetRow(rowTTDT).CreateCell(1).SetCellValue("");
                s1.GetRow(rowTTDT).CreateCell(2).SetCellValue(dtDetails.Rows[i]["mota_tienganh"].ToString());

                s1.GetRow(rowTTDT).CreateCell(3).SetCellValue(dtDetails.Rows[i]["inner"].ToString());
                s1.GetRow(rowTTDT).CreateCell(4).SetCellValue(dtDetails.Rows[i]["master"].ToString());

                s1.GetRow(rowTTDT).CreateCell(5).SetCellValue(double.Parse(dtDetails.Rows[i]["nopack"].ToString()));
                s1.GetRow(rowTTDT).GetCell(5).SetCellType(CellType.Numeric);

                s1.GetRow(rowTTDT).CreateCell(6).SetCellValue(dtDetails.Rows[i]["ten_cangbien"].ToString());
            }

            for (int i = 0; i < dtDetails.Rows.Count; i++)
            {
                var rowTTDT = rowTT + 5 + i;
                for (int j = 0; j <= 6; j++)
                {
                    if (j == 2)
                    {
                        s1.GetRow(rowTTDT).GetCell(j).CellStyle = cellStyleLeft;
                    }
                    else
                    {
                        s1.GetRow(rowTTDT).GetCell(j).CellStyle = cellStyleCenter;
                    }
                }
            }
            
            return hssfworkbook;
        }

        public Inquiry_QRCode(LinqDBDataContext dbP, List<Json_product> products, string logoPath, string productImagePath,
            string customer, string address, string tel, string fax)
        {
            db = dbP;
            this.logoPath = logoPath;
            this.productImagePath = productImagePath;
            this.products = products;
            this.customer = customer;
            this.address = address;
            this.tel = tel;
            this.fax = fax;
        }
    }
}