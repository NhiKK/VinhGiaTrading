using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptXuatKho
/// </summary>
public class rptXuatKho : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel18;
    private XRLabel lb_sophieu;
    private XRLabel lb_taikho;
    private XRLabel xrLabel20;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel5;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel9;
    private XRLabel lb_so_con;
    private XRLabel xrLabel11;
    private XRLabel xrLabel7;
    private XRLabel bl_ngay;
    private XRLabel lb_donvi_xuat;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell lb_sott;
    private XRTableCell lb_maso;
    private XRTableCell lb_mota;
    private XRTableCell lb_dvt;
    private XRTableCell lb_sl;
    private XRTableCell lb_dongia;
    private XRTableCell lb_thanhtien;
    private XRTableCell lb_ghichu;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;
    private XRLabel xrLabel32;
    private XRLabel xrLabel8;
    private XRLabel xrLabel6;
    private XRLabel xrLabel28;
    private XRLabel xrLabel24;
    private XRLabel xrLabel25;
    private XRLabel lb_tien;
    private XRLabel xrLabel12;
    private XRLabel lb_seal;
    private XRLabel lb_sum_sl;
    private XRLabel xrLabel14;
    private XRLabel xrLabel10;
    private GroupHeaderBand GroupHeader1;
    private GroupFooterBand GroupFooter1;
    private XRLabel lb_tongcong;
    private XRLabel xrLabel13;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptXuatKho()
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
            string resourceFileName = "rptXuatKho.resx";
            System.Resources.ResourceManager resources = global::Resources.rptXuatKho.ResourceManager;
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lb_sott = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_maso = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_mota = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_dvt = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_sl = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_dongia = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_thanhtien = new DevExpress.XtraReports.UI.XRTableCell();
            this.lb_ghichu = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_seal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_sophieu = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_taikho = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_so_con = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.bl_ngay = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_donvi_xuat = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_sum_sl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_tien = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_tongcong = new DevExpress.XtraReports.UI.XRLabel();
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
            this.xrTable2.Font = new System.Drawing.Font("Arial", 10F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(764F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lb_sott,
            this.lb_maso,
            this.lb_mota,
            this.lb_dvt,
            this.lb_sl,
            this.lb_dongia,
            this.lb_thanhtien,
            this.lb_ghichu});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // lb_sott
            // 
            this.lb_sott.Name = "lb_sott";
            this.lb_sott.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.lb_sott.StylePriority.UsePadding = false;
            this.lb_sott.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.lb_sott.Summary = xrSummary1;
            this.lb_sott.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lb_sott.Weight = 0.5290913532767334D;
            // 
            // lb_maso
            // 
            this.lb_maso.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
            this.lb_maso.Name = "lb_maso";
            this.lb_maso.Text = "[ma_sanpham]";
            this.lb_maso.Weight = 0.948065742810561D;
            // 
            // lb_mota
            // 
            this.lb_mota.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tiengviet")});
            this.lb_mota.Name = "lb_mota";
            this.lb_mota.Text = "[mota_tiengviet]";
            this.lb_mota.Weight = 1.8717666983698529D;
            // 
            // lb_dvt
            // 
            this.lb_dvt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt")});
            this.lb_dvt.Name = "lb_dvt";
            this.lb_dvt.StylePriority.UseTextAlignment = false;
            this.lb_dvt.Text = "[ma_dvt]";
            this.lb_dvt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lb_dvt.Weight = 0.47925505960178594D;
            // 
            // lb_sl
            // 
            this.lb_sl.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soluong", "{0:#,#}")});
            this.lb_sl.Name = "lb_sl";
            this.lb_sl.StylePriority.UseTextAlignment = false;
            this.lb_sl.Text = "lb_sl";
            this.lb_sl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lb_sl.Weight = 0.46797904765074261D;
            // 
            // lb_dongia
            // 
            this.lb_dongia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dongia")});
            this.lb_dongia.Name = "lb_dongia";
            this.lb_dongia.StylePriority.UseTextAlignment = false;
            this.lb_dongia.Text = "[dongia]";
            this.lb_dongia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lb_dongia.Weight = 0.62295571530312988D;
            // 
            // lb_thanhtien
            // 
            this.lb_thanhtien.BackColor = System.Drawing.Color.Transparent;
            this.lb_thanhtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thanhtien", "{0:#,#.00}")});
            this.lb_thanhtien.Name = "lb_thanhtien";
            this.lb_thanhtien.StylePriority.UseBackColor = false;
            this.lb_thanhtien.StylePriority.UseTextAlignment = false;
            this.lb_thanhtien.Text = "lb_thanhtien";
            this.lb_thanhtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lb_thanhtien.Weight = 0.99665447227986792D;
            // 
            // lb_ghichu
            // 
            this.lb_ghichu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ghichu")});
            this.lb_ghichu.Name = "lb_ghichu";
            this.lb_ghichu.StylePriority.UseTextAlignment = false;
            this.lb_ghichu.Text = "[ghichu]";
            this.lb_ghichu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lb_ghichu.Weight = 0.943307267270077D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 25F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 48.00002F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(478.5286F, 185.9586F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(46.875F, 23F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Seal:";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_seal
            // 
            this.lb_seal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "seal")});
            this.lb_seal.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_seal.LocationFloat = new DevExpress.Utils.PointFloat(525.4036F, 185.9586F);
            this.lb_seal.Name = "lb_seal";
            this.lb_seal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_seal.SizeF = new System.Drawing.SizeF(238.5961F, 23.00006F);
            this.lb_seal.StylePriority.UseFont = false;
            this.lb_seal.StylePriority.UseTextAlignment = false;
            this.lb_seal.Text = "[seal]";
            this.lb_seal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 243.75F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(764F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.49434755156922555D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Mã số";
            this.xrTableCell2.Weight = 0.8858099868715219D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Mô tả";
            this.xrTableCell3.Weight = 1.7488550825018989D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "ĐVT";
            this.xrTableCell4.Weight = 0.44778370928522609D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "SL";
            this.xrTableCell5.Weight = 0.437249157028913D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Đơn giá";
            this.xrTableCell6.Weight = 0.58204853378276844D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "Thành tiền";
            this.xrTableCell7.Weight = 0.93120796128681838D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "Ghi chú";
            this.xrTableCell8.Weight = 0.88136395254836664D;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 187.0001F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "Xuất tại kho:";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_sophieu
            // 
            this.lb_sophieu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sophieu")});
            this.lb_sophieu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_sophieu.LocationFloat = new DevExpress.Utils.PointFloat(72.16637F, 139.9585F);
            this.lb_sophieu.Name = "lb_sophieu";
            this.lb_sophieu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_sophieu.SizeF = new System.Drawing.SizeF(175.073F, 23.00002F);
            this.lb_sophieu.StylePriority.UseFont = false;
            this.lb_sophieu.StylePriority.UseTextAlignment = false;
            this.lb_sophieu.Text = "[sophieu]";
            this.lb_sophieu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_taikho
            // 
            this.lb_taikho.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "taikho")});
            this.lb_taikho.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_taikho.LocationFloat = new DevExpress.Utils.PointFloat(99.99989F, 187.0001F);
            this.lb_taikho.Name = "lb_taikho";
            this.lb_taikho.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_taikho.SizeF = new System.Drawing.SizeF(147.2394F, 22.99998F);
            this.lb_taikho.StylePriority.UseFont = false;
            this.lb_taikho.StylePriority.UseTextAlignment = false;
            this.lb_taikho.Text = "[taikho]";
            this.lb_taikho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 220.75F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(763.9999F, 23.00002F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "Theo chi chiết sau:";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.083333F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(65.33342F, 54.25002F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 16F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(65.33331F, 2.083333F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(698.6665F, 44.41668F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 139.9585F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(72.16644F, 22.99998F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Số phiếu:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(65.33318F, 46.50002F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(698.6667F, 22.20832F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(65.33318F, 90.91669F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(698.6666F, 49.04168F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "PHIẾU XUẤT KHO";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(65.33331F, 68.70836F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(698.6665F, 22.20834F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Tel.:  (Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 162.9585F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(79.16666F, 24.04166F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Xuất cho:";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_so_con
            // 
            this.lb_so_con.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_con")});
            this.lb_so_con.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_so_con.LocationFloat = new DevExpress.Utils.PointFloat(578.5287F, 162.9586F);
            this.lb_so_con.Name = "lb_so_con";
            this.lb_so_con.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_so_con.SizeF = new System.Drawing.SizeF(185.4712F, 23.00002F);
            this.lb_so_con.StylePriority.UseFont = false;
            this.lb_so_con.StylePriority.UseTextAlignment = false;
            this.lb_so_con.Text = "[so_con]";
            this.lb_so_con.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(478.5286F, 162.9586F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Số container:";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(478.5286F, 139.9585F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(125F, 23.00002F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Núi Thành, ngày";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // bl_ngay
            // 
            this.bl_ngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngay", "{0:M/d/yyyy}")});
            this.bl_ngay.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.bl_ngay.LocationFloat = new DevExpress.Utils.PointFloat(603.5287F, 139.9585F);
            this.bl_ngay.Name = "bl_ngay";
            this.bl_ngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.bl_ngay.SizeF = new System.Drawing.SizeF(160.4712F, 23.00002F);
            this.bl_ngay.StylePriority.UseFont = false;
            this.bl_ngay.StylePriority.UseTextAlignment = false;
            this.bl_ngay.Text = "bl_ngay";
            this.bl_ngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_donvi_xuat
            // 
            this.lb_donvi_xuat.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sochungtu")});
            this.lb_donvi_xuat.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_donvi_xuat.LocationFloat = new DevExpress.Utils.PointFloat(79.16666F, 162.9585F);
            this.lb_donvi_xuat.Name = "lb_donvi_xuat";
            this.lb_donvi_xuat.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_donvi_xuat.SizeF = new System.Drawing.SizeF(168.0728F, 24.04166F);
            this.lb_donvi_xuat.StylePriority.UseFont = false;
            this.lb_donvi_xuat.StylePriority.UseTextAlignment = false;
            this.lb_donvi_xuat.Text = "[sochungtu]";
            this.lb_donvi_xuat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(572.1928F, 45.99997F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(191.8071F, 23.00002F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Kế toán trưởng";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(572.1926F, 68.99999F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(191.8072F, 23.00002F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "Ký và ghi rõ họ tên";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lb_sum_sl
            // 
            this.lb_sum_sl.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soluong")});
            this.lb_sum_sl.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_sum_sl.LocationFloat = new DevExpress.Utils.PointFloat(426.4027F, 0F);
            this.lb_sum_sl.Name = "lb_sum_sl";
            this.lb_sum_sl.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_sum_sl.SizeF = new System.Drawing.SizeF(52.12598F, 23.00002F);
            this.lb_sum_sl.StylePriority.UseFont = false;
            this.lb_sum_sl.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:#,#}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.lb_sum_sl.Summary = xrSummary2;
            this.lb_sum_sl.Text = "lb_sum_sl";
            this.lb_sum_sl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(193.7499F, 45.99997F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(182.2918F, 23F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "Thủ kho";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0F, 68.99999F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(193.75F, 23.00002F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.Text = "Ký và ghi rõ họ tên";
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(376.0418F, 45.99997F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(196.1509F, 23.00002F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            this.xrLabel32.Text = "Người nhận hàng";
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(193.7499F, 68.99999F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(182.2918F, 23.00002F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Ký và ghi rõ họ tên";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(376.0418F, 68.99999F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(196.1509F, 23.00002F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Ký và ghi rõ họ tên";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 45.99997F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(193.75F, 23.00001F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "Người lập phiếu";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(426.4028F, 22.99999F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "Tổng cộng:";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.99995F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(164.5336F, 23.00002F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "Bằng chữ:";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabel25.Visible = false;
            // 
            // lb_tien
            // 
            this.lb_tien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tien")});
            this.lb_tien.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_tien.LocationFloat = new DevExpress.Utils.PointFloat(164.5338F, 22.99995F);
            this.lb_tien.Name = "lb_tien";
            this.lb_tien.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_tien.SizeF = new System.Drawing.SizeF(599.4662F, 22.99999F);
            this.lb_tien.StylePriority.UseFont = false;
            this.lb_tien.StylePriority.UseTextAlignment = false;
            this.lb_tien.Text = "[tien]";
            this.lb_tien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lb_tien.Visible = false;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20,
            this.bl_ngay,
            this.xrLabel7,
            this.xrLabel11,
            this.lb_so_con,
            this.xrLabel9,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel5,
            this.xrLabel1,
            this.xrPictureBox1,
            this.lb_donvi_xuat,
            this.lb_taikho,
            this.lb_sophieu,
            this.xrLabel18,
            this.xrTable1,
            this.lb_seal,
            this.xrLabel12});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("sochungtu", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 268.75F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrLabel25,
            this.lb_tien,
            this.lb_tongcong,
            this.xrLabel24,
            this.xrLabel28,
            this.xrLabel6,
            this.xrLabel8,
            this.xrLabel32,
            this.xrLabel29,
            this.xrLabel30,
            this.lb_sum_sl,
            this.xrLabel10,
            this.xrLabel14});
            this.GroupFooter1.HeightF = 164.5833F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // xrLabel13
            // 
            this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "hoten")});
            this.xrLabel13.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 141.5833F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(193.75F, 22.99998F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "[hoten]";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lb_tongcong
            // 
            this.lb_tongcong.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thanhtien")});
            this.lb_tongcong.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_tongcong.LocationFloat = new DevExpress.Utils.PointFloat(547.9168F, 0F);
            this.lb_tongcong.Name = "lb_tongcong";
            this.lb_tongcong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_tongcong.SizeF = new System.Drawing.SizeF(111.0126F, 23.00002F);
            this.lb_tongcong.StylePriority.UseFont = false;
            this.lb_tongcong.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,#.00}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.lb_tongcong.Summary = xrSummary3;
            this.lb_tongcong.Text = "[thanhtien]";
            this.lb_tongcong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // rptXuatKho
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupFooter1});
            this.Margins = new System.Drawing.Printing.Margins(32, 31, 25, 48);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion




}
