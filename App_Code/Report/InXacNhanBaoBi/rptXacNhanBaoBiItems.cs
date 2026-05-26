/*using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptXacNhanBaoBiItems
/// </summary>
public class rptXacNhanBaoBiItems : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell15;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTable xrTable3;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell17;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTable xrTable4;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell22;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTable xrTable5;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell53;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptXacNhanBaoBiItems()
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
            string resourceFileName = "rptXacNhanBaoBiItems.resx";
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.Detail.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.Detail.HeightF = 70.83334F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1268F, 70.83334F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UsePadding = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell30,
            this.xrTableCell34,
            this.xrTableCell31,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell36,
            this.xrTableCell41,
            this.xrTableCell40,
            this.xrTableCell39,
            this.xrTableCell35,
            this.xrTableCell44,
            this.xrTableCell45,
            this.xrTableCell43,
            this.xrTableCell46,
            this.xrTableCell48,
            this.xrTableCell47,
            this.xrTableCell42,
            this.xrTableCell54,
            this.xrTableCell32,
            this.xrTableCell50,
            this.xrTableCell52,
            this.xrTableCell51,
            this.xrTableCell49});
            this.xrTableRow8.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.StylePriority.UseFont = false;
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell33.Summary = xrSummary1;
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell33.Weight = 0.09463221645436809D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
            this.xrTableCell30.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseFont = false;
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "[ma_sanpham]";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell30.Weight = 0.26037219123416122D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham_khach")});
            this.xrTableCell34.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "[ma_sanpham_khach]";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell34.Weight = 0.25983736349844955D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_dathang", "{0:#,#}")});
            this.xrTableCell31.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "xrTableCell31";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell31.Weight = 0.15397788929674117D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_donggoi")});
            this.xrTableCell37.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.Text = "[ten_donggoi]";
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell37.Weight = 0.18391784589651616D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_inner")});
            this.xrTableCell38.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "[sl_inner]";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell38.Weight = 0.12082947860730826D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt_inner")});
            this.xrTableCell36.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.Text = "[dvt_inner]";
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell36.Weight = 0.15798759297288517D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l1")});
            this.xrTableCell41.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.Text = "[l1]";
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell41.Weight = 0.091870034628183647D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w1")});
            this.xrTableCell40.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            this.xrTableCell40.Text = "[w1]";
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell40.Weight = 0.0918696038839244D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h1")});
            this.xrTableCell39.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            this.xrTableCell39.Text = "[h1]";
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell39.Weight = 0.0918705420579739D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_outer")});
            this.xrTableCell35.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.Text = "[sl_outer]";
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell35.Weight = 0.097839557788023335D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt_outer")});
            this.xrTableCell44.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.Text = "[dvt_outer]";
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell44.Weight = 0.12991889222653327D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l2")});
            this.xrTableCell45.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseFont = false;
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.Text = "[l2]";
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell45.Weight = 0.086612529281472631D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w2")});
            this.xrTableCell43.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.Text = "[w2]";
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell43.Weight = 0.086612529281472644D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h2")});
            this.xrTableCell46.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseFont = false;
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.Text = "[h2]";
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell46.Weight = 0.086612647572983231D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vd")});
            this.xrTableCell48.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseFont = false;
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.Text = "[vd]";
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell48.Weight = 0.09641403617565196D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vn")});
            this.xrTableCell47.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseFont = false;
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.Text = "[vn]";
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell47.Weight = 0.096414192809928051D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vl")});
            this.xrTableCell42.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseFont = false;
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.Text = "[vl]";
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell42.Weight = 0.0964143498521059D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slthunginner", "{0:#,#0.0}")});
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.Text = "[slthunginner]";
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell54.Weight = 0.17370616074195777D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slthung", "{0:#,#0.0}")});
            this.xrTableCell32.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.Text = "xrTableCell32";
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell32.Weight = 0.17370616074195777D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nw", "{0:#,#.0}")});
            this.xrTableCell50.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.Text = "xrTableCell50";
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell50.Weight = 0.1469207585010904D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "gw", "{0:#,#.0}")});
            this.xrTableCell52.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseFont = false;
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell52.Weight = 0.16563291565998686D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngayxacnhan", "{0:dd/MM/yyyy}")});
            this.xrTableCell51.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseFont = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.Text = "[ngayxacnhan]";
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell51.Weight = 0.17236944301399643D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseFont = false;
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell49.Weight = 0.13772436978982189D;
            // 
            // TopMargin
            // 
            this.TopMargin.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseFont = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseFont = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.ReportHeader.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.ReportHeader.HeightF = 83.33334F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.StylePriority.UseFont = false;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1268F, 83.33334F);
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
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell7,
            this.xrTableCell16,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell21,
            this.xrTableCell53,
            this.xrTableCell27,
            this.xrTableCell26,
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell3});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.098772375924246592D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "MS.ANCO";
            this.xrTableCell2.Weight = 0.27176341329302095D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "MS.KHÁCH";
            this.xrTableCell4.Weight = 0.27120528221130369D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "SỐ\r\nLƯỢNG\r\nPO";
            this.xrTableCell5.Weight = 0.1607144117355348D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "ĐÓNG GÓI";
            this.xrTableCell6.Weight = 0.19196424995149877D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "SL\r\nINNER";
            this.xrTableCell8.Weight = 0.12611586153507223D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "ĐVT\r\nINNER";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.16489955761602954D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Weight = 0.28766776898077545D;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(107.396F, 83.33337F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15});
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell15.Multiline = true;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Text = "KT. ĐÓNG GÓI\r\nINNER(CM)";
            this.xrTableCell15.Weight = 3D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "DÀI";
            this.xrTableCell10.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = "RỘNG";
            this.xrTableCell11.Weight = 1D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "CAO";
            this.xrTableCell12.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell16.Multiline = true;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Text = "SL\r\nOUTER";
            this.xrTableCell16.Weight = 0.10212037627186094D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "ĐVT\r\nOUTER";
            this.xrTableCell13.Weight = 0.13560268261602945D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.Weight = 0.27120552467448367D;
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(101.2501F, 83.33337F);
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17});
            this.xrTableRow4.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell17.Multiline = true;
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.Text = "KT. ĐÓNG GÓI\r\nOUTER(CM)";
            this.xrTableCell17.Weight = 3D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20});
            this.xrTableRow5.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.StylePriority.UseFont = false;
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.Text = "DÀI";
            this.xrTableCell18.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.Text = "RỘNG";
            this.xrTableCell19.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Text = "CAO";
            this.xrTableCell20.Weight = 1D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.xrTableCell21.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.Text = "xrTableCell21";
            this.xrTableCell21.Weight = 0.30189710546817095D;
            // 
            // xrTable4
            // 
            this.xrTable4.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6,
            this.xrTableRow7});
            this.xrTable4.SizeF = new System.Drawing.SizeF(112.7082F, 83.33337F);
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22});
            this.xrTableRow6.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.StylePriority.UseFont = false;
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell22.Multiline = true;
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "VÁCH NGĂN";
            this.xrTableCell22.Weight = 3D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
            this.xrTableRow7.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.StylePriority.UseFont = false;
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.Text = "DÀI";
            this.xrTableCell23.Weight = 1D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.Text = "RỘNG";
            this.xrTableCell24.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.Text = "CAO";
            this.xrTableCell25.Weight = 1D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Multiline = true;
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Text = "SL\r\nĐÓNG GÓI INNER";
            this.xrTableCell53.Weight = 0.18130579871524655D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell27.Multiline = true;
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.Text = "SL\r\nĐÓNG GÓI OUTER";
            this.xrTableCell27.Weight = 0.18130579871524655D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.Text = "NW(KG)";
            this.xrTableCell26.Weight = 0.15334885852145291D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseFont = false;
            this.xrTableCell28.Text = "GW(KG)";
            this.xrTableCell28.Weight = 0.17287902882588763D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.Text = "NGÀY XÁC NHẬN ĐÓNG GÓI";
            this.xrTableCell29.Weight = 0.17991059532921222D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "GHI CHÚ";
            this.xrTableCell3.Weight = 0.1437498810434979D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1});
            this.ReportFooter.HeightF = 70.83334F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "huongdanlamhang", "{0:(###) ### - ####}")});
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(239.583F, 10.00001F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1028.417F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "xrLabel2";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 10.00001F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(239.5833F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Hướng dẫn làm hàng:";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rptXacNhanBaoBiItems
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 1752;
            this.PageWidth = 1268;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3Extra;
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptXacNhanBaoBiItems
/// </summary>
public class rptXacNhanBaoBiItems : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell15;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTable xrTable3;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell17;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTable xrTable4;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell22;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTable xrTable5;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell53;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell55;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptXacNhanBaoBiItems()
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
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            string resourceFileName = "rptXacNhanBaoBiItems.resx";
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.Detail.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.Detail.HeightF = 75.08333F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1268F, 70.83334F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UsePadding = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell30,
            this.xrTableCell34,
            this.xrTableCell31,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell36,
            this.xrTableCell41,
            this.xrTableCell40,
            this.xrTableCell39,
            this.xrTableCell35,
            this.xrTableCell44,
            this.xrTableCell45,
            this.xrTableCell43,
            this.xrTableCell46,
            this.xrTableCell48,
            this.xrTableCell47,
            this.xrTableCell42,
            this.xrTableCell54,
            this.xrTableCell32,
            this.xrTableCell56,
            this.xrTableCell50,
            this.xrTableCell52,
            this.xrTableCell51,
            this.xrTableCell49});
            this.xrTableRow8.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.StylePriority.UseFont = false;
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell33.Summary = xrSummary1;
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell33.Weight = 0.09463221645436809D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
            this.xrTableCell30.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseFont = false;
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "[ma_sanpham]";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell30.Weight = 0.26037219123416122D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham_khach")});
            this.xrTableCell34.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "[ma_sanpham_khach]";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell34.Weight = 0.25983736349844955D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_dathang", "{0:#,#}")});
            this.xrTableCell31.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "xrTableCell31";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell31.Weight = 0.15397788929674117D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_donggoi")});
            this.xrTableCell37.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.Text = "[ten_donggoi]";
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell37.Weight = 0.18391784589651616D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_inner")});
            this.xrTableCell38.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "[sl_inner]";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell38.Weight = 0.12082947860730826D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt_inner")});
            this.xrTableCell36.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.Text = "[dvt_inner]";
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell36.Weight = 0.15798759297288517D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l1")});
            this.xrTableCell41.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.Text = "[l1]";
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell41.Weight = 0.091870034628183647D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w1")});
            this.xrTableCell40.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            this.xrTableCell40.Text = "[w1]";
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell40.Weight = 0.0918696038839244D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h1")});
            this.xrTableCell39.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            this.xrTableCell39.Text = "[h1]";
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell39.Weight = 0.0918705420579739D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_outer")});
            this.xrTableCell35.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.Text = "[sl_outer]";
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell35.Weight = 0.097839557788023335D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt_outer")});
            this.xrTableCell44.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.Text = "[dvt_outer]";
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell44.Weight = 0.12991889222653327D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l2")});
            this.xrTableCell45.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseFont = false;
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.Text = "[l2]";
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell45.Weight = 0.086612529281472631D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w2")});
            this.xrTableCell43.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.Text = "[w2]";
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell43.Weight = 0.086612529281472644D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h2")});
            this.xrTableCell46.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseFont = false;
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.Text = "[h2]";
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell46.Weight = 0.086612647572983231D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vd")});
            this.xrTableCell48.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseFont = false;
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.Text = "[vd]";
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell48.Weight = 0.09641403617565196D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vn")});
            this.xrTableCell47.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseFont = false;
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.Text = "[vn]";
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell47.Weight = 0.096414192809928051D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vl")});
            this.xrTableCell42.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseFont = false;
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.Text = "[vl]";
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell42.Weight = 0.0964143498521059D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slthunginner", "{0:#,#0.0}")});
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.Text = "[slthunginner]";
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell54.Weight = 0.17370616074195777D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slthung", "{0:#,#0.0}")});
            this.xrTableCell32.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.Text = "xrTableCell32";
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell32.Weight = 0.11625834629003362D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soluonggoi_ctn")});
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.Text = "[soluonggoi_ctn]";
			this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell56.Weight = 0.11625865955858582D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nw", "{0:#,#.0}")});
            this.xrTableCell50.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.Text = "xrTableCell50";
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell50.Weight = 0.088109913394428738D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "gw", "{0:#,#.0}")});
            this.xrTableCell52.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseFont = false;
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell52.Weight = 0.16563291565998686D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngayxacnhan", "{0:dd/MM/yyyy}")});
            this.xrTableCell51.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseFont = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.Text = "[ngayxacnhan]";
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell51.Weight = 0.17236944301399643D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Font = new System.Drawing.Font("Times New Roman", 8F);
			this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota")});
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseFont = false;
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell49.Weight = 0.13772436978982189D;
            // 
            // TopMargin
            // 
            this.TopMargin.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseFont = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseFont = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.ReportHeader.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.ReportHeader.HeightF = 83.33334F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.StylePriority.UseFont = false;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1268F, 83.33334F);
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
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell7,
            this.xrTableCell16,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell21,
            this.xrTableCell53,
            this.xrTableCell27,
            this.xrTableCell55,
            this.xrTableCell26,
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell3});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.098772375924246592D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "MS.VINHGIA";
            this.xrTableCell2.Weight = 0.27176341329302095D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "MS.KHÁCH";
            this.xrTableCell4.Weight = 0.27120528221130369D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "SỐ\r\nLƯỢNG\r\nPO";
            this.xrTableCell5.Weight = 0.1607144117355348D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "ĐÓNG GÓI";
            this.xrTableCell6.Weight = 0.19196424995149877D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "SL\r\nINNER";
            this.xrTableCell8.Weight = 0.12611586153507223D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "ĐVT\r\nINNER";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.16489955761602954D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Weight = 0.28766776898077545D;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(107.396F, 83.33337F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15});
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell15.Multiline = true;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Text = "KT. ĐÓNG GÓI\r\nINNER(CM)";
            this.xrTableCell15.Weight = 3D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "DÀI";
            this.xrTableCell10.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = "RỘNG";
            this.xrTableCell11.Weight = 1D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "CAO";
            this.xrTableCell12.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell16.Multiline = true;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Text = "SL\r\nOUTER";
            this.xrTableCell16.Weight = 0.10212037627186094D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "ĐVT\r\nOUTER";
            this.xrTableCell13.Weight = 0.13560268261602945D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.Weight = 0.27120552467448367D;
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(101.2501F, 83.33337F);
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17});
            this.xrTableRow4.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell17.Multiline = true;
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.Text = "KT. ĐÓNG GÓI\r\nOUTER(CM)";
            this.xrTableCell17.Weight = 3D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20});
            this.xrTableRow5.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.StylePriority.UseFont = false;
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.Text = "DÀI";
            this.xrTableCell18.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.Text = "RỘNG";
            this.xrTableCell19.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Text = "CAO";
            this.xrTableCell20.Weight = 1D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.xrTableCell21.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.Text = "xrTableCell21";
            this.xrTableCell21.Weight = 0.30189710546817095D;
            // 
            // xrTable4
            // 
            this.xrTable4.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6,
            this.xrTableRow7});
            this.xrTable4.SizeF = new System.Drawing.SizeF(112.7082F, 83.33337F);
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22});
            this.xrTableRow6.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.StylePriority.UseFont = false;
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell22.Multiline = true;
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "VÁCH NGĂN";
            this.xrTableCell22.Weight = 3D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
            this.xrTableRow7.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.StylePriority.UseFont = false;
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.Text = "DÀI";
            this.xrTableCell23.Weight = 1D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.Text = "RỘNG";
            this.xrTableCell24.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.Text = "CAO";
            this.xrTableCell25.Weight = 1D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Multiline = true;
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Text = "SL\r\nĐÓNG GÓI INNER";
            this.xrTableCell53.Weight = 0.18130579871524655D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell27.Multiline = true;
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.Text = "SL\r\nĐÓNG GÓI OUTER";
            this.xrTableCell27.Weight = 0.12134480914766233D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Multiline = true;
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.Text = "Số lượng gói/Conts";
            this.xrTableCell55.Weight = 0.1213448091476623D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.Text = "NW(KG)";
            this.xrTableCell26.Weight = 0.09196503894137481D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseFont = false;
            this.xrTableCell28.Text = "GW(KG)";
            this.xrTableCell28.Weight = 0.17287902882588763D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.Text = "NGÀY XÁC NHẬN ĐÓNG GÓI";
            this.xrTableCell29.Weight = 0.17991059532921222D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "MÔ TẢ ĐÓNG GÓI";
            this.xrTableCell3.Weight = 0.1437498810434979D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1});
            this.ReportFooter.HeightF = 70.83334F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "huongdanlamhang", "{0:(###) ### - ####}")});
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(239.583F, 10.00001F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1028.417F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "xrLabel2";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 10.00001F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(239.5833F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Hướng dẫn làm hàng:";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rptXacNhanBaoBiItems
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 1752;
            this.PageWidth = 1268;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3Extra;
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
