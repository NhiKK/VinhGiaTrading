using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;

/// <summary>
/// Summary description for BaoGiaMauReport
/// </summary>
public class DanhSachDatHangReport
{
    LinqDBDataContext db = new LinqDBDataContext();
    private String c_danhsachdathang_id, logoPath, productImagePath, type;
    private List<AvariablePrj.lstImage> lstimage = new List<AvariablePrj.lstImage>();
    private List<AvariablePrj.lstText> lstText = new List<AvariablePrj.lstText>();

    public List<AvariablePrj.lstImage> Lstimage
    {
        get { return lstimage; }
        set { lstimage = value; }
    }

    public List<AvariablePrj.lstText> LstText
    {
        get { return lstText; }
        set { lstText = value; }
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

    public String TypePrint
    {
        get { return type; }
        set { type = value; }
    }

    public ExportExcel ConfigExcel { get; set; }

    public HSSFWorkbook CreateWBPI()
    {
        String select = String.Format(@"
                        select 
	                        dsdh.sochungtu
							, dh.sochungtu as so_po
							, dh.customer_order_no
							, dsdh.ngaylap
							, dsdh.hangiaohang_po
							, dtkd.ma_dtkd
							, dtkd.ten_dtkd
							, dtkd.tel
							, dtkd.fax
	                        , sp.ma_sanpham as maSo
							, ddsdh.ma_sanpham_khach as maKhachHang
                            , ddsdh.mota_tiengviet as moTa
                            , dvt.ma_edi as dvt
                            , ddsdh.sl_dathang as soLuong
							, ddsdh.gianhap as tongDonGiaMua
                            , isnull(ddsdh.giachuan, ddsdh.gianhap) as giaMua
                            , (ddsdh.gianhap - isnull(ddsdh.giachuan, ddsdh.gianhap)) as phi
							, dsdh.huongdanlamhang
							, dsdh.huongdanlamhangchung
							, ddsdh.huongdan_dathang
	                        , gh.ma_dtkd as diachigiaohang
							, dh.sl_cont
							, dh.loai_cont
							, dsdh.discount as discountdecimal
							, ddsdh.v2 as cbm
                            , (case ddsdh.sl_inner when 0 then ' ' else (cast(ddsdh.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner)) end) as dginner
                            , (cast(ddsdh.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer)) as dgouter
                            , (case ddsdh.sl_inner when 0 then 0 else ddsdh.sl_dathang / ddsdh.sl_inner end) as soKienInner
                            , (case ddsdh.sl_outer when 0 then 0 else ddsdh.sl_dathang / ddsdh.sl_outer end) as soKienOuter
                            , (select hoten from nhanvien where manv = dsdh.nguoitao) as nguoitao
							, sp.trongluong
							, sp.mota as ghichu
							, dsdh.mota as ghiChuDSDH
							, ddsdh.gianhap as gianhap
							, ddsdh.sl_dathang * ddsdh.gianhap as thanhtien
                            , isnull(dg.nw1, 0) as nw1
                            , isnull(dg.gw1, 0) as gw1
                            , isnull(dg.nw2, 0) as nw2
                            , isnull(dg.gw2, 0) as gw2
                            , isnull(dg.vtdg2, 0) as vtdg2
                        from 
	                        c_danhsachdathang dsdh
							left join md_doitackinhdoanh dtkd on dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
							left join md_doitackinhdoanh gh on dsdh.diachigiaohang = gh.md_doitackinhdoanh_id
	                        left join c_dongdsdh ddsdh on dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
							left join md_sanpham sp on ddsdh.md_sanpham_id = sp.md_sanpham_id
							left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
							left join c_donhang dh  on dsdh.c_donhang_id = dh.c_donhang_id
							left join md_donggoi dg on ddsdh.md_donggoi_id = dg.md_donggoi_id
                        where
	                        dsdh.c_danhsachdathang_id = N'{0}' order by sp.ma_sanpham asc"
                            , c_danhsachdathang_id == null ? "" : c_danhsachdathang_id
						);

        DataTable dt = mdbc.GetData(select);
		var tbl = dt;
        if (tbl.Rows.Count > 0)
        {
            tbl.Columns.Add("money", Type.GetType("System.String"));
			tbl.Columns.Add("mota_cong", Type.GetType("System.String"));
			tbl.Columns.Add("mota_tru", Type.GetType("System.String"));
			tbl.Columns.Add("discount", Type.GetType("System.Double"));
			tbl.Columns.Add("giatritang", Type.GetType("System.Double"));
			tbl.Columns.Add("giatrigiam", Type.GetType("System.Double"));
			tbl.Columns.Add("tongtiendatru", Type.GetType("System.Double"));
            //Header

            //Footer
            int lastRow = tbl.Rows.Count - 1;

			var phitang = (from t in db.c_phidathangs where t.isphicong.Equals(true) && t.c_danhsachdathang_id.Equals(c_danhsachdathang_id) select t.sotien).Sum();
			tbl.Rows[0]["giatritang"] = tbl.Rows[lastRow]["giatritang"] = phitang.GetValueOrDefault(0);
			
			var phigiam = (from t in db.c_phidathangs where t.isphicong.Equals(false) && t.c_danhsachdathang_id.Equals(c_danhsachdathang_id) select t.sotien).Sum();
			tbl.Rows[0]["giatrigiam"] = tbl.Rows[lastRow]["giatrigiam"] = phigiam.GetValueOrDefault(0);
            
			string diengiai_cong = String.Format(@"select t.mota from c_phidathang t where t.isphicong = 1 and t.c_danhsachdathang_id = '{0}'", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);
			DataTable cong = mdbc.GetData(diengiai_cong);
			var mota_cong = new List<string>();
			for(int i = 0; i < cong.Rows.Count ; i++)
			{
				mota_cong.Add(cong.Rows[i]["mota"].ToString());
			}
			
			string diengiai_tru = String.Format(@"select t.mota from c_phidathang t where t.isphicong = 0 and t.c_danhsachdathang_id = '{0}'", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);									
			DataTable tru = mdbc.GetData(diengiai_tru);
			var mota_tru = new List<string>();
			for(int i = 0; i < tru.Rows.Count; i++)
			{
				mota_tru.Add(tru.Rows[i]["mota"].ToString());
			}
			tbl.Rows[0]["mota_cong"] = tbl.Rows[lastRow]["mota_cong"] = string.Join(",", mota_cong);
			tbl.Rows[0]["mota_tru"] = tbl.Rows[lastRow]["mota_tru"] = string.Join(",", mota_tru);
			HSSFWorkbook hssfworkbook = this.CreateHSSFWorkbook(tbl, phitang, phigiam);
            String saveAsFileName = String.Format("DonDatHang-{0}.xls", DateTime.Now);
            return hssfworkbook;
        }
        else
        {
            throw new Exception("<h3>Đơn đặt hàng không có dữ liệu</h3>");
        }
    }

    public HSSFWorkbook CreateHSSFWorkbook(DataTable dt, Nullable<decimal> phitang, Nullable<decimal> phigiam)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
		HSSFSheet hssfsheet = (HSSFSheet)s1;
        ConfigExcel = new ExportExcel(hssfworkbook, 0, 15);
        ConfigExcel.NameTypePrint = "InPI";
        ConfigExcel.endHeader = type == "pdf" ? 16 : 9;
        ConfigExcel.isPDF = type == "pdf";
        var fontSizeRpt = double.Parse(ConfigExcel.layKichThuocChuChoRpt());
        var fontSizeRptShort = (short)fontSizeRpt;

        Excel_Format ex_fm = new Excel_Format(hssfworkbook); 
        ICellStyle cellBold = ex_fm.getcell(fontSizeRptShort, true, true, "", "L", "T");
        ICellStyle cellBold10 = ex_fm.getcell((short)(fontSizeRptShort - 2), true, true, "", "L", "T");
        ICellStyle cellBold10Top = ex_fm.getcell((short)(fontSizeRptShort - 2), true, true, "", "L", "B");
        //-- 
        ICellStyle cellHeader = ex_fm.getcell((short)(fontSizeRptShort + 6), true, true, "", "C", "T");
		//-- 
		ICellStyle cellHeader_n = ex_fm.getcell(fontSizeRptShort, false, true, "", "C", "T");
		//--
		ICellStyle celltext = ex_fm.getcell(fontSizeRptShort, false, true, "", "L", "T");
        ICellStyle celltext10 = ex_fm.getcell((short)(fontSizeRptShort - 2), false, true, "", "L", "T");
        ICellStyle celltext10Top = ex_fm.getcell((short)(fontSizeRptShort - 2), false, true, "", "L", "B");
        ICellStyle cellTextCenter = ex_fm.getcell(fontSizeRptShort, false, true, "", "C", "T");
        ICellStyle cellText10Center = ex_fm.getcell((short)(fontSizeRptShort - 2), false, true, "", "C", "T");
        //--
        ICellStyle rightBold = ex_fm.getcell(fontSizeRptShort, true, false, "", "R", "T");
		//--
		ICellStyle right = ex_fm.getcell(fontSizeRptShort, false, false, "", "R", "T");
		//--
		ICellStyle leftBold = ex_fm.getcell(fontSizeRptShort, true, false, "", "L", "T");
		//--
		ICellStyle left = ex_fm.getcell(fontSizeRptShort, false, false, "", "L", "T");
        //--
        ICellStyle border_number = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.0####");
        ICellStyle border_number_ = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.####");
        ICellStyle border_number0 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0");
		ICellStyle border_number1 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.0");
		ICellStyle border_number2 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.00");
		ICellStyle border_number3 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.000");
		ICellStyle border_number4 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.0000");

        var number = ex_fm.getcell2(fontSizeRptShort, false, true, "", "R", "T", "#,##0.0####");
        var number0 = ex_fm.getcell2(fontSizeRptShort, false, true, "", "R", "T", "#,##0");
        var number1 = ex_fm.getcell2(fontSizeRptShort, false, true, "", "R", "T", "#,##0.0");
        var number2 = ex_fm.getcell2(fontSizeRptShort, false, true, "", "R", "T", "#,##0.00");
        var number3 = ex_fm.getcell2(fontSizeRptShort, false, true, "", "R", "T", "#,##0.000");
        var numberBold = ex_fm.getcell2(fontSizeRptShort, true, true, "", "R", "T", "#,##0.0####");
        var numberBold0 = ex_fm.getcell2(fontSizeRptShort, true, true, "", "R", "T", "#,##0");
        var numberBold2 = ex_fm.getcell2(fontSizeRptShort, true, true, "", "R", "T", "#,##0.00");

        ICellStyle border = ex_fm.getcell(fontSizeRptShort, false, true, "LRBT", "L", "T");
		ICellStyle border_center = ex_fm.getcell(fontSizeRptShort, false, true, "LRBT", "C", "T");
		//--
		ICellStyle borderright = ex_fm.getcell(fontSizeRptShort, false, true, "LRBT", "L", "T");
		//--
		ICellStyle borderonlyleft = ex_fm.getcell(fontSizeRptShort, false, true, "L", "T", "T");
		//--
		ICellStyle borderWrap = ex_fm.getcell(fontSizeRptShort, true, true, "LRBT", "C", "T");
		//--
		ICellStyle signBold = ex_fm.getcell(fontSizeRptShort, true, true, "", "C", "C");
        //--
        var maDTKD = dt.Rows[0]["ma_dtkd"].ToString();

        ConfigExcel.cellHeader = cellHeader;
        ConfigExcel.cellHeader_n = cellHeader_n;
        var cellCBM = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", ConfigExcel.laySoThapPhanChoRpt(maDTKD, "cbm"));
        var cellSoLuong = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", ConfigExcel.laySoThapPhanChoRpt(maDTKD, "soluong"));
        var fmtDiscount = ConfigExcel.laySoThapPhanChoRpt("VINHGIA", "discount");
        var cellAmount = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", ConfigExcel.laySoThapPhanChoRpt(maDTKD, "amount"));
        var cellBoldCBM = ex_fm.getcell2(fontSizeRptShort, true, true, "", "R", "T", ConfigExcel.laySoThapPhanChoRpt(maDTKD, "cbm"));
        var cellBoldSoLuong = ex_fm.getcell2(fontSizeRptShort, true, true, "", "R", "T", ConfigExcel.laySoThapPhanChoRpt(maDTKD, "soluong"));
        var cellBoldAmount = ex_fm.getcell2(fontSizeRptShort, true, true, "", "R", "T", ConfigExcel.laySoThapPhanChoRpt(maDTKD, "amount"));
        //--
        HSSFUtils.CellStyleModel cellModel = new HSSFUtils.CellStyleModel(hssfworkbook);
        cellModel.BorderAll = true;
		//--
		//IDataFormat dataFormatCustom = hssfworkbook.CreateDataFormat();
		int heigh = 22;
		int row = 0;
        //set A1 - A4
        ConfigExcel.LogoRpt = new ExportExcel.Logoreport();
        ConfigExcel.LogoRpt.cell1 = type == "pdf" ? 0: 0;
        ConfigExcel.LogoRpt.cell2 = type == "pdf" ? 1 : 1;
        ConfigExcel.LogoRpt.dx1 = type == "pdf" ? 2930 : 2930;
        ConfigExcel.LogoRpt.dx2 = type == "pdf" ? 300 : 300;
        ConfigExcel.title = "ĐƠN ĐẶT HÀNG";
        row = ConfigExcel.createHeader(NameHeaderReport.ANCO, s1, row, heigh);

        s1.CreateRow(row).CreateCell(1).SetCellValue("Số chứng từ:");
        s1.GetRow(row).GetCell(1).CellStyle = celltext10;
        s1.GetRow(row).HeightInPoints = 20;
        s1.GetRow(row).CreateCell(2).SetCellValue(dt.Rows[0]["sochungtu"].ToString());
        s1.GetRow(row).GetCell(2).CellStyle = celltext10;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 3));
        lstText.Add(new AvariablePrj.lstText() {
            value = "",
            row = row,
            columnF = 2,
            columnT = 3,
            columnFPrev = 1,
            columnTPrev = 1,
            left = 88,
            top = (float)-0.5
        });
        
        s1.GetRow(row).CreateCell(4).SetCellValue("Theo P/O số:");
        s1.GetRow(row).GetCell(4).CellStyle = celltext10;
        s1.GetRow(row).CreateCell(5).SetCellValue(dt.Rows[0]["so_po"].ToString());
        s1.GetRow(row).GetCell(5).CellStyle = celltext10;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 9));
        lstText.Add(new AvariablePrj.lstText()
        {
            value = "",
            row = row,
            columnF = 5,
            columnT = 9,
            columnFPrev = 4,
            columnTPrev = 4,
            left = 108,
            top = (float)-0.5
        });
        row++;

        var ngayLapText = DateTime.Parse(dt.Rows[0]["ngaylap"].ToString()).ToString("dd/MM/yyyy");
        s1.CreateRow(row).CreateCell(1).SetCellValue("Ngày lập:");
        s1.GetRow(row).GetCell(1).CellStyle = celltext10;
        s1.GetRow(row).HeightInPoints = 20;
        s1.GetRow(row).CreateCell(2).SetCellValue(ngayLapText);
        s1.GetRow(row).GetCell(2).CellStyle = celltext10;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 3));
        lstText.Add(new AvariablePrj.lstText()
        {
            value = "",
            row = row,
            columnF = 2,
            columnT = 3,
            columnFPrev = 1,
            columnTPrev = 1,
            left = 105,
            top = (float)-0.5
        });

        s1.GetRow(row).CreateCell(4).SetCellValue("Customer Order No:");
        s1.GetRow(row).GetCell(4).CellStyle = celltext10;
        s1.GetRow(row).CreateCell(5).SetCellValue(dt.Rows[0]["customer_order_no"].ToString());
        s1.GetRow(row).GetCell(5).CellStyle = celltext10;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 9));
        lstText.Add(new AvariablePrj.lstText()
        {
            value = "",
            row = row,
            columnF = 5,
            columnT = 9,
            columnFPrev = 4,
            columnTPrev = 4,
            left = 78,
            top = (float)-0.5
        });
        row++;

        s1.CreateRow(row).CreateCell(1).SetCellValue("Công ty AnCơ kính đề nghị Quý Công ty/Đơn vị: " + dt.Rows[0]["ten_dtkd"].ToString());
        s1.GetRow(row).GetCell(1).CellStyle = celltext10;
        s1.GetRow(row).HeightInPoints = 20;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 9));
        row++;

        s1.CreateRow(row).CreateCell(1).SetCellValue("Điện thoại:");
        s1.GetRow(row).GetCell(1).CellStyle = celltext10;
        s1.GetRow(row).HeightInPoints = 20;
        s1.GetRow(row).CreateCell(2).SetCellValue(dt.Rows[0]["tel"].ToString());
        s1.GetRow(row).GetCell(2).CellStyle = celltext10;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 3));
        lstText.Add(new AvariablePrj.lstText()
        {
            value = "",
            row = row,
            columnF = 2,
            columnT = 3,
            columnFPrev = 1,
            columnTPrev = 1,
            left = 98,
            top = (float)-0.5
        });

        s1.GetRow(row).CreateCell(4).SetCellValue("Fax:");
        s1.GetRow(row).GetCell(4).CellStyle = celltext10;
        s1.GetRow(row).CreateCell(5).SetCellValue(dt.Rows[0]["fax"].ToString());
        s1.GetRow(row).GetCell(5).CellStyle = celltext10;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 9));
        lstText.Add(new AvariablePrj.lstText()
        {
            value = "",
            row = row,
            columnF = 5,
            columnT = 9,
            columnFPrev = 4,
            columnTPrev = 4,
            left = 147,
            top = (float)-0.2
        });
        row++;
        row++;
        s1.CreateRow(row).CreateCell(1).SetCellValue("Cung cấp cho công ty chúng tôi các mặt hàng với chi tiết:");
        s1.GetRow(row).GetCell(1).CellStyle = celltext10;
        s1.GetRow(row).HeightInPoints = 20;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 9));
        row++;
        // set A13 - All
        // -- Header
        var columnHeaders = new List<columnHeader>();
        int width = 5000;
        #region columnHeaders
        var cell = 0;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "STT",
            value = "stt",
            width = 1500
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Mã số",
            value = "maSo",
            width = 7000
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Hình ảnh",
            value = "",
            width = width
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Mã khách hàng",
            value = "maKhachHang",
            width = width
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Mô tả",
            value = "moTa",
            width = 8000
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "SL",
            value = "soLuong",
            width = 2500
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "ĐVT",
            value = "dvt",
            width = 2000
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Giá mua",
            value = "giaMua",
            width = guiNCC ? 0 : (int)(fontSizeRpt * 260),
            autoSize = guiNCC ? false : true
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Phí",
            value = "phi",
            width = guiNCC ? 0 : (int)(fontSizeRpt * 260),
            autoSize = guiNCC ? false : true
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Tổng đơn\ngiá mua",
            value = "tongDonGiaMua",
            width = guiNCC ? 0 : (int)(fontSizeRpt * 260),
            autoSize = guiNCC ? false : true
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Thành tiền",
            value = "thanhTien",
            width = guiNCC ? 0 : (int)(fontSizeRpt * 380)
            
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Đóng gói Inner",
            value = "dginner",
            width = 4000
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Đóng gói Outer",
            value = "dgouter",
            width = 4000
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Số kiện Inner",
            value = "soKienInner",
            width = 3400
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Số kiện Outer",
            value = "soKienOuter",
            width = 3400
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Cbm",
            value = "cbm",
            width = 3000
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Hướng dẫn làm hàng",
            value = "huongdan_dathang",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Trọng lượng",
            value = "trongluong",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Xác nhận kích thước",
            value = "ghichu",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Giá gốc",
            value = "giaMua",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "Phí",
            value = "phi",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "NW1",
            value = "nw1",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "GW1",
            value = "gw1",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "NW2",
            value = "nw2",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "GW2",
            value = "gw2",
            width = 0
        });
        cell++;
        columnHeaders.Add(new columnHeader()
        {
            columnAt = cell,
            text = "VTDG2",
            value = "vtdg2",
            width = 0
        });
        #endregion columnHeaders

        IRow rowColumnHeader = s1.CreateRow(row);
		rowColumnHeader.HeightInPoints = 47;
        var sumWidth2to9 = columnHeaders.Where(s=>s.columnAt >=2 & s.columnAt <= 9).Sum(s=>s.width);
		foreach(var colHeader in columnHeaders)
		{
			rowColumnHeader.CreateCell(colHeader.columnAt).SetCellValue(colHeader.text);
			rowColumnHeader.GetCell(colHeader.columnAt).CellStyle = borderWrap;
            s1.SetColumnWidth(colHeader.columnAt, colHeader.width);
		}
		row ++;
		//thu tu tong cong // dem so dong hang cua tung sochungtu
		int count_row = 1;
        double sumThanhTien = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //
			IRow row_t = s1.CreateRow(row);
            string moTa = dt.Rows[i]["moTa"].ToString();
            var heighMoTatMAX = ConfigExcel.MeasureTextHeight(moTa, 7000, "Arial", fontSizeRptShort) + 4;
            row_t.HeightInPoints = heighMoTatMAX < 70 ? 70 : heighMoTatMAX;

            row_t.CreateCell(2);
            try
            {
                var imgPath = ExcuteSignalRStatic.mapPathSignalR("~/" + ExcuteSignalRStatic.getImageProduct(dt.Rows[i]["maSo"].ToString()));
                lstimage.Add(new AvariablePrj.lstImage() {
                    row = row,
                    column = 3,
                    link = imgPath,
                    heightCell = row_t.HeightInPoints,
                    widthCell = 5000
                });
            }
            catch
            { }

            var soLuong = double.Parse(dt.Rows[i]["soluong"].ToString());
            var tongDonGiaMua = double.Parse(dt.Rows[i]["tongDonGiaMua"].ToString());
            var thanhtien = soLuong * tongDonGiaMua;
            sumThanhTien += thanhtien;

            foreach (var colHeader in columnHeaders)
			{
				var cel_row_t = row_t.CreateCell(colHeader.columnAt);
				if(colHeader.value == "stt")
                {
					cel_row_t.SetCellValue(count_row);
					cel_row_t.CellStyle = border_center; 
				}
                else if (new string[] { "dvt", "maSo" }.Contains(colHeader.value))
                {
                    cel_row_t.SetCellValue(dt.Rows[i][colHeader.value].ToString());
                    cel_row_t.CellStyle = border_center;
                }
                else if(colHeader.value == "thanhTien")
                {
                    if (guiNCC == false)
                        cel_row_t.CellFormula = String.Format("J{0}*F{0}",row + 1);

                    cel_row_t.CellStyle = cellAmount;
                }
				else if(new string[] { "trongluong", "nw1", "gw1", "nw2", "gw2", "vtdg2" }.Contains(colHeader.value))
                {
					cel_row_t.SetCellValue(double.Parse(dt.Rows[i][colHeader.value].ToString()));
					cel_row_t.CellStyle = border_number1;
				}
				else if(colHeader.value == "soLuong")
                {
					cel_row_t.SetCellValue(double.Parse(dt.Rows[i][colHeader.value].ToString()));
					cel_row_t.CellStyle = cellSoLuong;
				}
				else if (new string[] { "giaMua", "tongDonGiaMua", "phi" }.Contains(colHeader.value))
                {
                    var val = double.Parse(dt.Rows[i][colHeader.value].ToString());
                    if (guiNCC == false)
                        cel_row_t.SetCellValue(val);

                    cel_row_t.CellStyle = cellAmount;
                }
				else if( new string[] { "cbm", "soKienInner", "soKienOuter" }.Contains(colHeader.value))
                {
					cel_row_t.SetCellValue(double.Parse(dt.Rows[i][colHeader.value].ToString()));
					cel_row_t.CellStyle = cellCBM;
				}
				else if(colHeader.value == "")
                {
					cel_row_t.SetCellValue("");
					cel_row_t.CellStyle = border;
				}
				else
                {
					cel_row_t.SetCellValue(dt.Rows[i][colHeader.value].ToString());
					cel_row_t.CellStyle = border;
				}

                if (colHeader.autoSize.GetValueOrDefault(false))
                    s1.AutoSizeColumn(colHeader.columnAt);
            }
			row++;
			count_row++;
		}
        var discountP = double.Parse(dt.Rows[0]["discountdecimal"].ToString());
        var discount = Math.Round(sumThanhTien * discountP / 100, 2, MidpointRounding.AwayFromZero);

        //vi tri row dau tien
        int m = row - count_row + 2;
		int sub = row + 1;
		//start tong cong 1
        s1.CreateRow(row).CreateCell(1).SetCellValue("Tổng cộng:");
        s1.GetRow(row).HeightInPoints = 30;
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 4));

        s1.GetRow(row).CreateCell(5).SetCellFormula(String.Format("SUM(F{0}:F{1})", m, row));
        s1.GetRow(row).GetCell(5).CellStyle = cellBoldSoLuong;

        if (guiNCC == true)
            s1.GetRow(row).CreateCell(10).SetCellValue("");
        else
            s1.GetRow(row).CreateCell(10).SetCellFormula(String.Format("SUM(K{0}:K{1})", m, row));

        s1.GetRow(row).GetCell(10).CellStyle = cellBoldAmount;
        s1.GetRow(row).CreateCell(15).SetCellFormula(String.Format("SUM(P{0}:P{1})", m, row));
        s1.GetRow(row).GetCell(15).CellStyle = cellBoldCBM;

		row++;
		//start +
		int cong = row + 1;
        //s1.CreateRow(row).CreateCell(1).SetCellValue(dt.Rows[0]["diengiaitang"].ToString() + "(+):");
        var cellR01 = s1.CreateRow(row).CreateCell(1);
        cellR01.SetCellValue("(+):" + dt.Rows[0]["mota_cong"].ToString());
        cellR01.CellStyle = cellBold;
        var cellRange1to5v1 = new CellRangeAddress(row, row, 1, 9);
        s1.AddMergedRegion(cellRange1to5v1);

        var cellR10 = s1.GetRow(row).CreateCell(10);
        var phiTangVal = double.Parse(phitang == null ? "0" : phitang.Value.ToString());
        if (guiNCC == false)
        {
            cellR10.SetCellValue(phiTangVal);
            cellR10.CellStyle = cellBoldAmount;
        }

        if (phitang == null)
        {
            s1.GetRow(row).ZeroHeight = true;

            if (type == "pdf")
            {
                s1.GetRow(row).RemoveCell(cellR01);
                s1.GetRow(row).RemoveCell(cellR10);
            }
        }
        else
            s1.GetRow(row).HeightInPoints = 30;

        row++;
		//start -
		int tru = row + 1;
        //s1.CreateRow(row).CreateCell(1).SetCellValue(dt.Rows[0]["diengiaigiam"].ToString() + " (-):");
        cellR01 = s1.CreateRow(row).CreateCell(1);
        cellR01.SetCellValue("(-):" + dt.Rows[0]["mota_tru"].ToString());
        cellR01.CellStyle = cellBold;
        var cellRange1to5v2 = new CellRangeAddress(row, row, 1, 9);
        s1.AddMergedRegion(cellRange1to5v2);

        cellR10 = s1.GetRow(row).CreateCell(10);
        var phiGiamVal = double.Parse(phigiam == null ? "0" : phigiam.Value.ToString());
        if (guiNCC == false)
        {
            cellR10.SetCellValue(phiGiamVal);
            cellR10.CellStyle = cellBoldAmount;
        }

        if (phigiam == null)
        {
            //s1.GetRow(row).HeightInPoints = 0;
            s1.GetRow(row).ZeroHeight = true;
            if (type == "pdf")
            {
                s1.GetRow(row).RemoveCell(cellR01);
                s1.GetRow(row).RemoveCell(cellR10);
            }
        }
        else
            s1.GetRow(row).HeightInPoints = 30;

        row++;
		// start discount
		int dis = row + 1;
        var ds2so = discountP.ToString(fmtDiscount);
        s1.CreateRow(row).CreateCell(1).SetCellValue(String.Format("Discount ({0}%)", ds2so));
        s1.GetRow(row).HeightInPoints = 30;
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 4));

        cellR10 = s1.GetRow(row).CreateCell(10);
        if (guiNCC == false)
        {
            cellR10.SetCellValue(discount);
            cellR10.CellStyle = cellBoldAmount;
        }
		row++;
		// start tổng cộng đã trừ
		s1.CreateRow(row).CreateCell(1).SetCellValue("Tổng cộng đã trừ:");
        s1.GetRow(row).HeightInPoints = 30;
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 4));
        cellR10 = s1.GetRow(row).CreateCell(10);
        double tongCongDaTru = sumThanhTien + phiTangVal - phiGiamVal - discount;
        tongCongDaTru = tongCongDaTru < 0.000000001 ? 0 : tongCongDaTru;
        if (guiNCC == false)
        {
            cellR10.SetCellFormula(String.Format("K{0} + K{1} - K{2} - K{3}", sub, cong, tru, dis));
            cellR10.CellStyle = cellBoldAmount;
        }
		row++;
		// start viet bang chu
        s1.CreateRow(row).CreateCell(1).SetCellValue("Viết bằng chữ:");
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.GetRow(row).HeightInPoints = 30;
        if (guiNCC == false)
        {
            string textMoney = "";
            try
            {
                var tcdc = decimal.Round((decimal)tongCongDaTru, 2, MidpointRounding.AwayFromZero);
                textMoney = MoneyToWord.ConvertMoneyToText(tcdc.ToString()).Trim();
                if (textMoney.Contains("Dollars"))
                {
                    textMoney = "USD " + textMoney.Replace("Dollars", "");
                }
            }
            catch {
                textMoney = string.Format("{0}+{1}-{2}-{3}={4}", 
                    sumThanhTien.ToString(),
                    phiTangVal.ToString(),
                    phiGiamVal.ToString(),
                    discount.ToString(),
                    tongCongDaTru.ToString()
                    );
            }
            //if(textMoney.Length > 0) {
            //	textMoney = textMoney.Substring(0, 1).ToUpper() + textMoney.Substring(1);
            //}

            s1.GetRow(row).CreateCell(2).SetCellValue(textMoney);
            s1.GetRow(row).GetCell(2).CellStyle = celltext;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 10));
        }
		row++;
        // start ghi chu
        var ghiChuHDDH = dt.Rows[0]["ghiChuDSDH"].ToString();

        if (type == "pdf")
        {
            string[] arrDDHHs = ghiChuHDDH.Split(new string[] { "\n", "\r" }, StringSplitOptions.None);
            for (var iHDK = 0; iHDK < arrDDHHs.Length; iHDK++)
            {
                var rowLineHDK = s1.CreateRow(row);
                if (iHDK == 0)
                {
                    rowLineHDK.CreateCell(1).SetCellValue("Ghi chú:");
                    rowLineHDK.GetCell(1).CellStyle = cellBold;
                }

                s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 9));
                rowLineHDK.CreateCell(2).SetCellValue(arrDDHHs[iHDK]);
                rowLineHDK.GetCell(2).CellStyle = celltext;

                var heightMAX = ConfigExcel.MeasureTextHeight(arrDDHHs[iHDK], sumWidth2to9, "Arial", fontSizeRptShort);
                rowLineHDK.HeightInPoints = heightMAX < 14 ? 14 : heightMAX;
                row++;
            }
        }
        else
        {
            var rowLineHDK = s1.CreateRow(row);
            rowLineHDK.CreateCell(1).SetCellValue("Ghi chú:");
            rowLineHDK.GetCell(1).CellStyle = cellBold;

            rowLineHDK.CreateCell(2).SetCellValue(ghiChuHDDH);
            rowLineHDK.GetCell(2).CellStyle = celltext;

            var heightMAX = ConfigExcel.MeasureTextHeight(ghiChuHDDH, sumWidth2to9, "Arial", fontSizeRptShort - 2);
            rowLineHDK.HeightInPoints = heightMAX < 30 ? 30 : heightMAX;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 9));
            row++;
        }

        row++;
		// Ngay xong hang
        s1.CreateRow(row).CreateCell(1).SetCellValue("Ngày xong hàng:");
        s1.GetRow(row).HeightInPoints = 30;
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.GetRow(row).CreateCell(2).SetCellValue(DateTime.Parse(dt.Rows[0]["hangiaohang_po"].ToString()).ToString("dd/MM/yyyy"));
        s1.GetRow(row).GetCell(2).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 5));
		row++;
		// Dia diem xuat hang
        s1.CreateRow(row).CreateCell(1).SetCellValue("Địa điểm xuất hàng:");
        s1.GetRow(row).HeightInPoints = 30;
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.GetRow(row).CreateCell(2).SetCellValue(dt.Rows[0]["diachigiaohang"].ToString());
        s1.GetRow(row).GetCell(2).CellStyle = celltext;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 5));
		row++;
		//Huong dan lam hang
        s1.CreateRow(row).CreateCell(1).SetCellValue("Hướng dẫn làm hàng:");
        s1.GetRow(row).HeightInPoints = 30;
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 5));
        row++;
		// start dac diem hang hoa
        string huongdanlamhang = dt.Rows[0]["huongdanlamhang"].ToString();
        if (type == "pdf")
        {
            string[] arrDDHHs = huongdanlamhang.Split(new string[] { "\n", "\r" }, StringSplitOptions.None);
            for (var iHDK = 0; iHDK < arrDDHHs.Length; iHDK++)
            {
                var rowLineHDK = s1.CreateRow(row);
                if (iHDK == 0)
                {
                    rowLineHDK.CreateCell(1).SetCellValue("1. Đặc điểm hàng hóa:");
                    rowLineHDK.GetCell(1).CellStyle = cellBold10Top;
                }

                s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 9));
                rowLineHDK.CreateCell(2).SetCellValue(arrDDHHs[iHDK]);
                rowLineHDK.GetCell(2).CellStyle = celltext10Top;

                var heightMAX = ConfigExcel.MeasureTextHeight(arrDDHHs[iHDK], sumWidth2to9, "Arial", fontSizeRptShort - 2);
                rowLineHDK.HeightInPoints = heightMAX < 14 ? 14 : heightMAX;
                row++;
            }
            s1.CreateRow(row).HeightInPoints = 25;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 9));
            row++;
        }
        else
        {
            var rowLineHDK = s1.CreateRow(row);
            rowLineHDK.CreateCell(1).SetCellValue("1.Đặc điểm hàng hóa:");
            rowLineHDK.GetCell(1).CellStyle = cellBold10Top;

            rowLineHDK.CreateCell(2).SetCellValue(huongdanlamhang);
            rowLineHDK.GetCell(2).CellStyle = celltext10Top;

            var heightMAX = ConfigExcel.MeasureTextHeight(huongdanlamhang, sumWidth2to9, "Arial", fontSizeRptShort - 2);
            rowLineHDK.HeightInPoints = heightMAX < 14 ? 14 : heightMAX;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 9));
            row++;
            s1.CreateRow(row).HeightInPoints = 25;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 9));
            row++;
        }
        // start cac huong dan khac
        var huongdanlamhangchung = dt.Rows[0]["huongdanlamhangchung"].ToString();
        if (type == "pdf")
        {
            string[] arrHDKs = huongdanlamhangchung.Split(new string[] { "\n", "\r" }, StringSplitOptions.None);
            for (var iHDK = 0; iHDK < arrHDKs.Length; iHDK++)
            {
                var rowLineHDK = s1.CreateRow(row);
                if (iHDK == 0)
                {
                    rowLineHDK.CreateCell(1).SetCellValue("2.Các hướng dẫn khác:");
                    rowLineHDK.GetCell(1).CellStyle = cellBold10Top;
                }

                s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 9));
                rowLineHDK.CreateCell(2).SetCellValue(arrHDKs[iHDK]);
                rowLineHDK.GetCell(2).CellStyle = celltext10Top;

                var heightMAX = ConfigExcel.MeasureTextHeight(arrHDKs[iHDK], sumWidth2to9, "Arial", fontSizeRptShort - 2);
                rowLineHDK.HeightInPoints = heightMAX < 14 ? 14 : heightMAX;
                row++;
            }
        }
        else
        {
            var rowLineHDK = s1.CreateRow(row);
            rowLineHDK.CreateCell(1).SetCellValue("2. Các hướng dẫn khác:");
            rowLineHDK.GetCell(1).CellStyle = cellBold10Top;

            rowLineHDK.CreateCell(2).SetCellValue(huongdanlamhangchung);
            rowLineHDK.GetCell(2).CellStyle = celltext10Top;

            var heightMAX = ConfigExcel.MeasureTextHeight(huongdanlamhangchung, sumWidth2to9, "Arial", fontSizeRptShort - 2);
            rowLineHDK.HeightInPoints = heightMAX < 14 ? 14 : heightMAX;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 9));
            row++;
        }
        //--
        s1.CreateRow(row).CreateCell(1).SetCellValue("* Khi cơ sở nhận được đơn đặt hàng này vui lòng ký xác nhận và gửi lại.");
        s1.GetRow(row).HeightInPoints = 30;
        s1.GetRow(row).GetCell(1).CellStyle = cellBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 9));
		row++;
		//--
        s1.CreateRow(row).CreateCell(1).SetCellValue("Người lập phiếu");
        s1.GetRow(row).HeightInPoints = 20;
        s1.GetRow(row).GetCell(1).CellStyle = cellTextCenter;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 2));
		//
		s1.GetRow(row).CreateCell(7).SetCellValue("Nhà cung ứng");
        s1.GetRow(row).GetCell(7).CellStyle = cellTextCenter;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 7, 10));
		row++;
		//--
        s1.CreateRow(row).CreateCell(1).SetCellValue("(Ký và ghi rõ họ tên)");
        s1.GetRow(row).HeightInPoints = 20;
        s1.GetRow(row).GetCell(1).CellStyle = cellText10Center;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 2));
		//--
		s1.GetRow(row).CreateCell(7).SetCellValue("(Ký và ghi rõ họ tên)");
        s1.GetRow(row).GetCell(7).CellStyle = cellText10Center;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 7, 10));
		row = row + 3;
		//
        s1.CreateRow(row).CreateCell(1).SetCellValue(dt.Rows[0]["nguoitao"].ToString());
        s1.GetRow(row).HeightInPoints = 20;
        s1.GetRow(row).GetCell(1).CellStyle = cellTextCenter;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 2));
		row++;
        //s1.GetRow(row).CreateCell(4).SetCellValue("Trưởng phòng");
        //s1.AddMergedRegion(new CellRangeAddress(row, row, 4, 5));

        //s1.GetRow(row).CreateCell(4).SetCellValue("Ký và ghi rõ họ tên");
        //s1.AddMergedRegion(new CellRangeAddress(row, row, 4, 5));


        //s1.GetRow(row).CreateCell(8).SetCellValue(dt.Rows[0]["nguoitao"].ToString());
        //s1.GetRow(row).GetCell(8).CellStyle = cCenter;
        //s1.AddMergedRegion(new CellRangeAddress(row, row, 8, 9));


        ICellStyle cellUn = hssfworkbook.CreateCellStyle();
        IFont funderline = hssfworkbook.CreateFont();
        funderline.Underline = FontUnderlineType.Single;
        funderline.FontHeightInPoints = 12;
        cellUn.SetFont(funderline);
        cellUn.Alignment = HorizontalAlignment.Center;
        cellUn.VerticalAlignment = VerticalAlignment.Top;
		//s1.GetRow(2).GetCell(0).CellStyle = cellUn;
        s1.GetRow(3).GetCell(0).CellStyle = cellUn;

        s1 = ConfigExcel.PrintExcel((HSSFSheet)s1, row);
        s1.PrintSetup.Landscape = true;

        if (type == "pdf")
            s1.PrintSetup.PaperSize = (short)PaperSize.A4;
        return hssfworkbook;
    }

    public String C_baogia_id
    {
        get { return c_danhsachdathang_id; }
        set { c_danhsachdathang_id = value; }
    }

    public bool guiNCC { get; set; }

    public DanhSachDatHangReport(String c_danhsachdathang_id, String logoPath, String productImagePath)
    {
        this.c_danhsachdathang_id = c_danhsachdathang_id;
        this.logoPath = logoPath;
        this.productImagePath = productImagePath;
        this.guiNCC = false;
    }

    public IFont CreateFontSize(HSSFWorkbook hsswb, short size)
    {
        IFont font = hsswb.CreateFont();
        font.FontHeightInPoints = size;
        return font;
    }

}
