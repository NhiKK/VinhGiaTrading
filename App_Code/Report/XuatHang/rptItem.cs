using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptItem
/// </summary>
public class rptItem : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell lb_stt;
    private XRTableCell lb_ma_anco;
    private XRTableCell lb_makhach;
    private XRTableCell lb_mota;
    private XRTableCell lb_donvitinh;
    private XRTableCell lb_donggoi;
    private XRTableCell lb_sl_yeucau;
    private XRTableCell lb_sokien;
    private XRTableCell lb_ghichu;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell1;
    private XRTableRow xrTableRow1;
    private XRTable xrTable1;
    private PageHeaderBand PageHeader;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell6;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptItem()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
	}
	
	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
            string resourceFileName = "rptItem.resx";
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lb_stt = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_ma_anco = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_makhach = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_mota = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_donvitinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_donggoi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_sl_yeucau = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_sokien = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_ghichu = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(952.0417F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lb_stt,
            this.lb_ma_anco,
            this.lb_makhach,
            this.lb_mota,
            this.lb_donvitinh,
            this.lb_donggoi,
            this.xrTableCell12,
            this.xrTableCell14,
            this.lb_sl_yeucau,
            this.xrTableCell15,
            this.lb_sokien,
            this.lb_ghichu});
            this.xrTableRow2.Font = new System.Drawing.Font("Arial", 10F);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableRow2.Weight = 1D;
            // 
            // lb_stt
            // 
            this.lb_stt.Multiline = true;
            this.lb_stt.Name = "lb_stt";
            this.lb_stt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.lb_stt.StylePriority.UsePadding = false;
            this.lb_stt.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.lb_stt.Summary = xrSummary1;
            this.lb_stt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lb_stt.Weight = 0.47965836354628805D;
            // 
            // lb_ma_anco
            // 
            this.lb_ma_anco.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
            this.lb_ma_anco.Name = "lb_ma_anco";
            this.lb_ma_anco.Text = "[ma_sanpham]";
            this.lb_ma_anco.Weight = 1.0844433917402072D;
            // 
            // lb_makhach
            // 
            this.lb_makhach.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham_khach")});
            this.lb_makhach.Name = "lb_makhach";
            this.lb_makhach.Text = "[ma_sanpham_khach]";
            this.lb_makhach.Weight = 1.0218809547748129D;
            // 
            // lb_mota
            // 
            this.lb_mota.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tienganh")});
            this.lb_mota.Name = "lb_mota";
            this.lb_mota.Text = "[mota_tienganh]";
            this.lb_mota.Weight = 1.4515909040094353D;
            // 
            // lb_donvitinh
            // 
            this.lb_donvitinh.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_dvt")});
            this.lb_donvitinh.Name = "lb_donvitinh";
            this.lb_donvitinh.StylePriority.UseTextAlignment = false;
            this.lb_donvitinh.Text = "[ten_dvt]";
            this.lb_donvitinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lb_donvitinh.Weight = 0.47746854581660225D;
            // 
            // lb_donggoi
            // 
            this.lb_donggoi.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_donggoi")});
            this.lb_donggoi.Name = "lb_donggoi";
            this.lb_donggoi.StylePriority.UseTextAlignment = false;
            this.lb_donggoi.Text = "[ten_donggoi]";
            this.lb_donggoi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lb_donggoi.Weight = 0.92294965407494389D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_po")});
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "[sl_po]";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell12.Weight = 0.561865313375655D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_thucxuat")});
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "[sl_thucxuat]";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell14.Weight = 0.70323940611978819D;
            // 
            // lb_sl_yeucau
            // 
            this.lb_sl_yeucau.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_yeucau")});
            this.lb_sl_yeucau.Multiline = true;
            this.lb_sl_yeucau.Name = "lb_sl_yeucau";
            this.lb_sl_yeucau.StylePriority.UseTextAlignment = false;
            this.lb_sl_yeucau.Text = "[sl_yeucau]";
            this.lb_sl_yeucau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lb_sl_yeucau.Weight = 0.67195613483894D;
            // 
            // lb_sokien
            // 
            this.lb_sokien.Name = "lb_sokien";
            this.lb_sokien.StylePriority.UseTextAlignment = false;
            this.lb_sokien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lb_sokien.Weight = 0.60854216257596649D;
            // 
            // lb_ghichu
            // 
            this.lb_ghichu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ghichu")});
            this.lb_ghichu.Name = "lb_ghichu";
            this.lb_ghichu.Text = "[ghichu]";
            this.lb_ghichu.Weight = 0.88590604363995062D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "Ghi chú";
            this.xrTableCell8.Weight = 0.88499841769644749D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "Số kiện";
            this.xrTableCell7.Weight = 0.607918717468638D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "SL \r\nyêu cầu";
            this.xrTableCell5.Weight = 0.67126894164398809D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "Đóng gói";
            this.xrTableCell10.Weight = 0.92200400769733681D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "ĐVT";
            this.xrTableCell4.Weight = 0.47697845903692748D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Mô tả";
            this.xrTableCell3.Weight = 1.4501041269305897D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "Mã Khách";
            this.xrTableCell9.Weight = 1.0208343568151268D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Mã số VINHGIA";
            this.xrTableCell2.Weight = 1.0833323202465228D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.47916663064982579D;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell9,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell13,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "SL PO";
            this.xrTableCell11.Weight = 0.56128908995301652D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "SL \r\nđã xuất";
            this.xrTableCell13.Weight = 0.702519552557086D;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001430511F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(952.0416F, 46.875F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 46.875F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "SL\r\nthực xuất";
            this.xrTableCell6.Weight = 0.66000181014034731D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Weight = 0.66067992974817025D;
            // 
            // rptItem
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 216, 0, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
