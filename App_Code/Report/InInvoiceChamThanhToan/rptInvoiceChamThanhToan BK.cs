// /*using System;
// using System.Drawing;
// using System.Collections;
// using System.ComponentModel;
// using DevExpress.XtraReports.UI;

// /// <summary>
// /// Summary description for rptInvoiceChamThanhToan
// /// </summary>
// public class rptInvoiceChamThanhToan : DevExpress.XtraReports.UI.XtraReport
// {
	// private DevExpress.XtraReports.UI.DetailBand Detail;
	// private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	// private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    // private XRTable xrTable2;
    // private XRTableRow xrTableRow2;
    // private XRTableCell xrTableCell8;
    // private XRTableCell xrTableCell9;
    // private XRTableCell xrTableCell10;
    // private XRTableCell xrTableCell11;
    // private XRTableCell xrTableCell12;
    // private XRTableCell xrTableCell13;
    // private XRTableCell xrTableCell14;
    // private ReportHeaderBand ReportHeader;
    // private XRLabel xrLabel1;
	// private XRLabel xrLabel2;
	// private XRLabel xrLabel3;
    // private PageHeaderBand PageHeader;
    // private XRTable xrTable1;
    // private XRTableRow xrTableRow1;
    // private XRTableCell xrTableCell1;
    // private XRTableCell xrTableCell2;
    // private XRTableCell xrTableCell3;
    // private XRTableCell xrTableCell4;
    // private XRTableCell xrTableCell5;
    // private XRTableCell xrTableCell6;
    // private XRTableCell xrTableCell7;
    // private ReportFooterBand ReportFooter;
    // private XRTable xrTable3;
    // private XRTableRow xrTableRow3;
    // private XRTableCell xrTableCell15;
    // private XRTableCell xrTableCell20;
    // private XRTableCell xrTableCell21;
    // private XRTableCell xrTableCell17;
    // private XRTableCell xrTableCell22;
    // private XRTableCell xrTableCell16;
    // private XRTableCell xrTableCell19;
    // private XRTableCell xrTableCell18;
	// /// <summary>
	// /// Required designer variable.
	// /// </summary>
	// private System.ComponentModel.IContainer components = null;

	// public rptInvoiceChamThanhToan()
	// {
		// InitializeComponent();
		// //
		// // TODO: Add constructor logic here
		// //
	// }
	
	// /// <summary> 
	// /// Clean up any resources being used.
	// /// </summary>
	// /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	// protected override void Dispose(bool disposing) {
		// if (disposing && (components != null)) {
			// components.Dispose();
		// }
		// base.Dispose(disposing);
	// }

	// #region Designer generated code

	// /// <summary>
	// /// Required method for Designer support - do not modify
	// /// the contents of this method with the code editor.
	// /// </summary>
	// private void InitializeComponent() {
            // string resourceFileName = "rptInvoiceChamThanhToan.resx";
            // DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            // DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            // this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            // this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            // this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            // this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            // this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            // this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            // this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
			 // this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
			  // this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            // this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            // this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            // this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            // this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            // this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            // this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            // this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            // this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            // ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            // ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            // ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            // ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // // 
            // // Detail
            // // 
            // this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable2});
            // this.Detail.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            // this.Detail.HeightF = 25F;
            // this.Detail.Name = "Detail";
            // this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // this.Detail.StylePriority.UseFont = false;
            // this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // // 
            // // xrTable2
            // // 
            // this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            // | DevExpress.XtraPrinting.BorderSide.Bottom)));
            // this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            // this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            // this.xrTable2.Name = "xrTable2";
            // this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            // this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow2});
            // this.xrTable2.SizeF = new System.Drawing.SizeF(714F, 25F);
            // this.xrTable2.StylePriority.UseBorders = false;
            // this.xrTable2.StylePriority.UseFont = false;
            // this.xrTable2.StylePriority.UsePadding = false;
            // this.xrTable2.StylePriority.UseTextAlignment = false;
            // this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // // 
            // // xrTableRow2
            // // 
            // this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell8,
            // this.xrTableCell9,
            // this.xrTableCell10,
            // this.xrTableCell17,
            // this.xrTableCell11,
            // this.xrTableCell12,
            // this.xrTableCell13,
            // this.xrTableCell14,
            // this.xrTableCell22});
            // this.xrTableRow2.Name = "xrTableRow2";
            // this.xrTableRow2.Weight = 1D;
            // // 
            // // xrTableCell8
            // // 
            // this.xrTableCell8.Name = "xrTableCell8";
            // this.xrTableCell8.StylePriority.UseTextAlignment = false;
            // xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            // xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            // this.xrTableCell8.Summary = xrSummary1;
            // this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // this.xrTableCell8.Weight = 0.39583332061767573D;
            // // 
            // // xrTableCell9
            // // 
            // this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaylap", "{0:dd/MM/yyyy}")});
            // this.xrTableCell9.Name = "xrTableCell9";
            // this.xrTableCell9.Text = "xrTableCell9";
            // this.xrTableCell9.Weight = 0.677082799916868D;
            // // 
            // // xrTableCell10
            // // 
            // this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_inv")});
            // this.xrTableCell10.Name = "xrTableCell10";
            // this.xrTableCell10.Text = "[so_inv]";
            // this.xrTableCell10.Weight = 0.82812461763983636D;
            // // 
            // // xrTableCell11
            // // 
            // this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgross", "{0:#,#.00}")});
            // this.xrTableCell11.Name = "xrTableCell11";
            // this.xrTableCell11.StylePriority.UseTextAlignment = false;
            // this.xrTableCell11.Text = "xrTableCell11";
            // this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // this.xrTableCell11.Weight = 0.71078213079076291D;
            // // 
            // // xrTableCell12
            // // 
            // this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngay_motokhai", "{0:dd/MM/yyyy}")});
            // this.xrTableCell12.Name = "xrTableCell12";
            // this.xrTableCell12.Text = "xrTableCell12";
            // this.xrTableCell12.Weight = 0.7784885030189469D;
            // // 
            // // xrTableCell13
            // // 
            // this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngayphaithanhtoan", "{0:dd/MM/yyyy}")});
            // this.xrTableCell13.Name = "xrTableCell13";
            // this.xrTableCell13.Text = "xrTableCell13";
            // this.xrTableCell13.Weight = 0.89966186274999382D;
            // // 
            // // xrTableCell14
            // // 
            // this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "songaychamtra")});
            // this.xrTableCell14.Name = "xrTableCell14";
            // this.xrTableCell14.StylePriority.UseTextAlignment = false;
            // this.xrTableCell14.Text = "[songaychamtra]";
            // this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // this.xrTableCell14.Weight = 1.0123302969812509D;
            // // 
            // // TopMargin
            // // 
            // this.TopMargin.HeightF = 50F;
            // this.TopMargin.Name = "TopMargin";
            // this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // // 
            // // BottomMargin
            // // 
            // this.BottomMargin.HeightF = 47F;
            // this.BottomMargin.Name = "BottomMargin";
            // this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // // 
            // // ReportHeader
            // // 
            // this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrLabel1,this.xrLabel2,this.xrLabel3});
            // this.ReportHeader.HeightF = 48F;
            // this.ReportHeader.Name = "ReportHeader";
            // // 
            // // xrLabel1
            // // 
            // this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            // this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            // this.xrLabel1.Name = "xrLabel1";
            // this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // this.xrLabel1.SizeF = new System.Drawing.SizeF(714F, 48F);
            // this.xrLabel1.StylePriority.UseFont = false;
            // this.xrLabel1.StylePriority.UseTextAlignment = false;
            // this.xrLabel1.Text = "DANH SÁCH INVOICE CHẬM THANH TOÁN";
            // this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			
			 // // 
            // // xrLabel3
            // // 
            // this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "Đến ngày {0}")});
            // this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            // this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(345.3125F, 47.99998F);
            // this.xrLabel3.Name = "xrLabel3";
            // this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // this.xrLabel3.SizeF = new System.Drawing.SizeF(202.6693F, 23F);
            // this.xrLabel3.StylePriority.UseFont = false;
            // this.xrLabel3.StylePriority.UseTextAlignment = false;
            // this.xrLabel3.Text = "xrLabel3";
            // this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // // 
            // // xrLabel2
            // // 
            // this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "Từ ngày {0}")});
            // this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            // this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(177.0834F, 47.99998F);
            // this.xrLabel2.Name = "xrLabel2";
            // this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // this.xrLabel2.SizeF = new System.Drawing.SizeF(168.2291F, 23F);
            // this.xrLabel2.StylePriority.UseFont = false;
            // this.xrLabel2.StylePriority.UseTextAlignment = false;
            // this.xrLabel2.Text = "xrLabel2";
            // this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // // 
            // // PageHeader
            // // 
            // this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable1});
            // this.PageHeader.HeightF = 45.83333F;
            // this.PageHeader.Name = "PageHeader";
            // // 
            // // xrTable1
            // // 
            // this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            // | DevExpress.XtraPrinting.BorderSide.Right) 
            // | DevExpress.XtraPrinting.BorderSide.Bottom)));
            // this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            // this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            // this.xrTable1.Name = "xrTable1";
            // this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow1});
            // this.xrTable1.SizeF = new System.Drawing.SizeF(714F, 45.83333F);
            // this.xrTable1.StylePriority.UseBorders = false;
            // this.xrTable1.StylePriority.UseFont = false;
            // this.xrTable1.StylePriority.UseTextAlignment = false;
            // this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // // 
            // // xrTableRow1
            // // 
            // this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell1,
            // this.xrTableCell2,
            // this.xrTableCell3,
            // this.xrTableCell16,
            // this.xrTableCell4,
            // this.xrTableCell5,
            // this.xrTableCell6,
            // this.xrTableCell7,
            // this.xrTableCell19});
            // this.xrTableRow1.Name = "xrTableRow1";
            // this.xrTableRow1.Weight = 1D;
            // // 
            // // xrTableCell1
            // // 
            // this.xrTableCell1.Name = "xrTableCell1";
            // this.xrTableCell1.Text = "STT";
            // this.xrTableCell1.Weight = 0.39583332061767573D;
            // // 
            // // xrTableCell2
            // // 
            // this.xrTableCell2.Name = "xrTableCell2";
            // this.xrTableCell2.Text = "NGÀY LẬP";
            // this.xrTableCell2.Weight = 0.677082799916868D;
            // // 
            // // xrTableCell3
            // // 
            // this.xrTableCell3.Name = "xrTableCell3";
            // this.xrTableCell3.Text = "INVOICE";
            // this.xrTableCell3.Weight = 0.82812461763983636D;
            // // 
            // // xrTableCell4
            // // 
            // this.xrTableCell4.Multiline = true;
            // this.xrTableCell4.Name = "xrTableCell4";
            // this.xrTableCell4.Text = "TRỊ GIÁ\r\nINVOICE";
            // this.xrTableCell4.Weight = 0.71078213079076291D;
            // // 
            // // xrTableCell5
            // // 
            // this.xrTableCell5.Multiline = true;
            // this.xrTableCell5.Name = "xrTableCell5";
            // this.xrTableCell5.Text = "NGÀY MỞ \r\nTỜ KHAI";
            // this.xrTableCell5.Weight = 0.7784885030189469D;
            // // 
            // // xrTableCell6
            // // 
            // this.xrTableCell6.Multiline = true;
            // this.xrTableCell6.Name = "xrTableCell6";
            // this.xrTableCell6.Text = "NGÀY PHẢI \r\nTHANH TOÁN";
            // this.xrTableCell6.Weight = 0.89966186274999382D;
            // // 
            // // xrTableCell7
            // // 
            // this.xrTableCell7.Multiline = true;
            // this.xrTableCell7.Name = "xrTableCell7";
            // this.xrTableCell7.Text = "SỐ NGÀY \r\nCHẬM TRẢ";
            // this.xrTableCell7.Weight = 1.0123302969812509D;
            // // 
            // // ReportFooter
            // // 
            // this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable3});
            // this.ReportFooter.HeightF = 25F;
            // this.ReportFooter.Name = "ReportFooter";
            // // 
            // // xrTable3
            // // 
            // this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            // | DevExpress.XtraPrinting.BorderSide.Right) 
            // | DevExpress.XtraPrinting.BorderSide.Bottom)));
            // this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            // this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            // this.xrTable3.Name = "xrTable3";
            // this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            // this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow3});
            // this.xrTable3.SizeF = new System.Drawing.SizeF(714F, 25F);
            // this.xrTable3.StylePriority.UseBorders = false;
            // this.xrTable3.StylePriority.UseFont = false;
            // this.xrTable3.StylePriority.UsePadding = false;
            // this.xrTable3.StylePriority.UseTextAlignment = false;
            // this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // // 
            // // xrTableRow3
            // // 
            // this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell15,
            // this.xrTableCell18,
            // this.xrTableCell20,
            // this.xrTableCell21});
            // this.xrTableRow3.Name = "xrTableRow3";
            // this.xrTableRow3.Weight = 1D;
            // // 
            // // xrTableCell15
            // // 
            // this.xrTableCell15.Name = "xrTableCell15";
            // this.xrTableCell15.StylePriority.UseTextAlignment = false;
            // this.xrTableCell15.Text = "TỔNG CỘNG";
            // this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // this.xrTableCell15.Weight = 1.9010406232259043D;
            // // 
            // // xrTableCell20
            // // 
            // this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgross", "{0:#,#.00}")});
            // this.xrTableCell20.Multiline = true;
            // this.xrTableCell20.Name = "xrTableCell20";
            // this.xrTableCell20.StylePriority.UseTextAlignment = false;
            // xrSummary2.FormatString = "{0:#,#.00}";
            // xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            // this.xrTableCell20.Summary = xrSummary2;
            // this.xrTableCell20.Text = "[totalgross]";
            // this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // this.xrTableCell20.Weight = 0.71078182260137213D;
            // // 
            // // xrTableCell21
            // // 
            // this.xrTableCell21.Multiline = true;
            // this.xrTableCell21.Name = "xrTableCell21";
            // this.xrTableCell21.Weight = 3.6819785239193603D;
            // // 
            // // xrTableCell16
            // // 
            // this.xrTableCell16.Name = "xrTableCell16";
            // this.xrTableCell16.Text = "ĐƠN HÀNG";
            // this.xrTableCell16.Weight = 0.846198689944234D;
            // // 
            // // xrTableCell17
            // // 
            // this.xrTableCell17.Name = "xrTableCell17";
            // this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "donhang")});
            // this.xrTableCell17.Text = "[donhang]";
            // this.xrTableCell17.Weight = 0.846198689944234D;
            // // 
            // // xrTableCell18
            // // 
            // this.xrTableCell18.Name = "xrTableCell18";
            // this.xrTableCell18.Weight = 0.84619872507758265D;
            // // 
            // // xrTableCell19
            // // 
            // this.xrTableCell19.Name = "xrTableCell19";
            // this.xrTableCell19.Text = "PAYMENT TERM";
            // this.xrTableCell19.Weight = 0.9914974731646502D;
            // // 
            // // xrTableCell22
            // // 
            // this.xrTableCell22.Name = "xrTableCell22";
            // this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "paymentterm")});
            // this.xrTableCell22.Text = "[paymentterm]";
            // this.xrTableCell22.Weight = 0.9914974731646502D;
            // // 
            // // rptInvoiceChamThanhToan
            // // 
            // this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            // this.Detail,
            // this.TopMargin,
            // this.BottomMargin,
            // this.ReportHeader,
            // this.PageHeader,
            // this.ReportFooter});
            // this.Margins = new System.Drawing.Printing.Margins(52, 61, 50, 47);
            // this.PageHeight = 1169;
            // this.PageWidth = 827;
            // this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            // this.Version = "12.2";
            // ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            // ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            // ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            // ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	// }

	// #endregion
// }*/

// /*
// using System;
// using System.Drawing;
// using System.Collections;
// using System.ComponentModel;
// using DevExpress.XtraReports.UI;

// /// <summary>
// /// Summary description for rptInvoiceChamThanhToan
// /// </summary>
// public class rptInvoiceChamThanhToan : DevExpress.XtraReports.UI.XtraReport
// {
	// private DevExpress.XtraReports.UI.DetailBand Detail;
	// private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	// private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    // private XRTable xrTable2;
    // private XRTableRow xrTableRow2;
    // private XRTableCell xrTableCell8;
    // private XRTableCell xrTableCell9;
    // private XRTableCell xrTableCell10;
    // private XRTableCell xrTableCell11;
    // private XRTableCell xrTableCell12;
    // private XRTableCell xrTableCell13;
    // private XRTableCell xrTableCell14;
    // private ReportHeaderBand ReportHeader;
    // private XRLabel xrLabel1;
    // private PageHeaderBand PageHeader;
    // private XRTable xrTable1;
    // private XRTableRow xrTableRow1;
    // private XRTableCell xrTableCell1;
    // private XRTableCell xrTableCell2;
    // private XRTableCell xrTableCell3;
    // private XRTableCell xrTableCell4;
    // private XRTableCell xrTableCell5;
    // private XRTableCell xrTableCell6;
    // private XRTableCell xrTableCell7;
    // private ReportFooterBand ReportFooter;
    // private XRTable xrTable3;
    // private XRTableRow xrTableRow3;
    // private XRTableCell xrTableCell15;
    // private XRTableCell xrTableCell20;
    // private XRTableCell xrTableCell21;
    // private XRTableCell xrTableCell17;
    // private XRTableCell xrTableCell22;
    // private XRTableCell xrTableCell16;
    // private XRTableCell xrTableCell19;
    // private XRTableCell xrTableCell18;
    // private XRLabel xrLabel5;
    // private XRLabel xrLabel4;
    // private XRLabel xrLabel3;
    // private XRLabel xrLabel2;
	// /// <summary>
	// /// Required designer variable.
	// /// </summary>
	// private System.ComponentModel.IContainer components = null;

	// public rptInvoiceChamThanhToan()
	// {
		// InitializeComponent();
		// //
		// // TODO: Add constructor logic here
		// //
	// }
	
	// /// <summary> 
	// /// Clean up any resources being used.
	// /// </summary>
	// /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	// protected override void Dispose(bool disposing) {
		// if (disposing && (components != null)) {
			// components.Dispose();
		// }
		// base.Dispose(disposing);
	// }

	// #region Designer generated code

	// /// <summary>
	// /// Required method for Designer support - do not modify
	// /// the contents of this method with the code editor.
	// /// </summary>
	// private void InitializeComponent() {
        // string resourceFileName = "rptInvoiceChamThanhToan.resx";
        // DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        // DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        // this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        // this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        // this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        // this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        // this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        // this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        // this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        // this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        // this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        // this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        // this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        // this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        // this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        // this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        // this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        // this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        // this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        // ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // // 
        // // Detail
        // // 
        // this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable2});
        // this.Detail.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        // this.Detail.HeightF = 25F;
        // this.Detail.Name = "Detail";
        // this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // this.Detail.StylePriority.UseFont = false;
        // this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // // 
        // // xrTable2
        // // 
        // this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    // | DevExpress.XtraPrinting.BorderSide.Bottom)));
        // this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        // this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        // this.xrTable2.Name = "xrTable2";
        // this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        // this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow2});
        // this.xrTable2.SizeF = new System.Drawing.SizeF(714F, 25F);
        // this.xrTable2.StylePriority.UseBorders = false;
        // this.xrTable2.StylePriority.UseFont = false;
        // this.xrTable2.StylePriority.UsePadding = false;
        // this.xrTable2.StylePriority.UseTextAlignment = false;
        // this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrTableRow2
        // // 
        // this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell8,
            // this.xrTableCell9,
            // this.xrTableCell10,
            // this.xrTableCell17,
            // this.xrTableCell11,
            // this.xrTableCell12,
            // this.xrTableCell13,
            // this.xrTableCell14,
            // this.xrTableCell22});
        // this.xrTableRow2.Name = "xrTableRow2";
        // this.xrTableRow2.Weight = 1D;
        // // 
        // // xrTableCell8
        // // 
        // this.xrTableCell8.Name = "xrTableCell8";
        // this.xrTableCell8.StylePriority.UseTextAlignment = false;
        // xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
        // xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        // this.xrTableCell8.Summary = xrSummary1;
        // this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell8.Weight = 0.39583332061767573D;
        // // 
        // // xrTableCell9
        // // 
        // this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaylap", "{0:dd/MM/yyyy}")});
        // this.xrTableCell9.Name = "xrTableCell9";
        // this.xrTableCell9.Text = "xrTableCell9";
        // this.xrTableCell9.Weight = 0.677082799916868D;
        // // 
        // // xrTableCell10
        // // 
        // this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_inv")});
        // this.xrTableCell10.Name = "xrTableCell10";
        // this.xrTableCell10.Text = "[so_inv]";
        // this.xrTableCell10.Weight = 0.82812461763983636D;
        // // 
        // // xrTableCell17
        // // 
        // this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "donhang")});
        // this.xrTableCell17.Name = "xrTableCell17";
        // this.xrTableCell17.Text = "[donhang]";
        // this.xrTableCell17.Weight = 0.846198689944234D;
        // // 
        // // xrTableCell11
        // // 
        // this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgross", "{0:#,#.00}")});
        // this.xrTableCell11.Name = "xrTableCell11";
        // this.xrTableCell11.StylePriority.UseTextAlignment = false;
        // this.xrTableCell11.Text = "xrTableCell11";
        // this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell11.Weight = 0.71078213079076291D;
        // // 
        // // xrTableCell12
        // // 
        // this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngay_motokhai", "{0:dd/MM/yyyy}")});
        // this.xrTableCell12.Name = "xrTableCell12";
        // this.xrTableCell12.Text = "xrTableCell12";
        // this.xrTableCell12.Weight = 0.7784885030189469D;
        // // 
        // // xrTableCell13
        // // 
        // this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngayphaithanhtoan", "{0:dd/MM/yyyy}")});
        // this.xrTableCell13.Name = "xrTableCell13";
        // this.xrTableCell13.Text = "xrTableCell13";
        // this.xrTableCell13.Weight = 0.89966186274999382D;
        // // 
        // // xrTableCell14
        // // 
        // this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "songaychamtra")});
        // this.xrTableCell14.Name = "xrTableCell14";
        // this.xrTableCell14.StylePriority.UseTextAlignment = false;
        // this.xrTableCell14.Text = "[songaychamtra]";
        // this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell14.Weight = 1.0123302969812509D;
        // // 
        // // xrTableCell22
        // // 
        // this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "paymentterm")});
        // this.xrTableCell22.Name = "xrTableCell22";
        // this.xrTableCell22.Text = "[paymentterm]";
        // this.xrTableCell22.Weight = 0.9914974731646502D;
        // // 
        // // TopMargin
        // // 
        // this.TopMargin.HeightF = 50F;
        // this.TopMargin.Name = "TopMargin";
        // this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // // 
        // // BottomMargin
        // // 
        // this.BottomMargin.HeightF = 47F;
        // this.BottomMargin.Name = "BottomMargin";
        // this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // // 
        // // ReportHeader
        // // 
        // this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrLabel5,
            // this.xrLabel4,
            // this.xrLabel3,
            // this.xrLabel2,
            // this.xrLabel1});
        // this.ReportHeader.HeightF = 81.33337F;
        // this.ReportHeader.Name = "ReportHeader";
        // // 
        // // xrLabel1
        // // 
        // this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
        // this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        // this.xrLabel1.Name = "xrLabel1";
        // this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel1.SizeF = new System.Drawing.SizeF(714F, 48F);
        // this.xrLabel1.StylePriority.UseFont = false;
        // this.xrLabel1.StylePriority.UseTextAlignment = false;
        // this.xrLabel1.Text = "DANH SÁCH INVOICE CHẬM THANH TOÁN";
        // this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // // 
        // // PageHeader
        // // 
        // this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable1});
        // this.PageHeader.HeightF = 45.83333F;
        // this.PageHeader.Name = "PageHeader";
        // // 
        // // xrTable1
        // // 
        // this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    // | DevExpress.XtraPrinting.BorderSide.Right)
                    // | DevExpress.XtraPrinting.BorderSide.Bottom)));
        // this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        // this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        // this.xrTable1.Name = "xrTable1";
        // this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow1});
        // this.xrTable1.SizeF = new System.Drawing.SizeF(714F, 45.83333F);
        // this.xrTable1.StylePriority.UseBorders = false;
        // this.xrTable1.StylePriority.UseFont = false;
        // this.xrTable1.StylePriority.UseTextAlignment = false;
        // this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // // 
        // // xrTableRow1
        // // 
        // this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell1,
            // this.xrTableCell2,
            // this.xrTableCell3,
            // this.xrTableCell16,
            // this.xrTableCell4,
            // this.xrTableCell5,
            // this.xrTableCell6,
            // this.xrTableCell7,
            // this.xrTableCell19});
        // this.xrTableRow1.Name = "xrTableRow1";
        // this.xrTableRow1.Weight = 1D;
        // // 
        // // xrTableCell1
        // // 
        // this.xrTableCell1.Name = "xrTableCell1";
        // this.xrTableCell1.Text = "STT";
        // this.xrTableCell1.Weight = 0.39583332061767573D;
        // // 
        // // xrTableCell2
        // // 
        // this.xrTableCell2.Name = "xrTableCell2";
        // this.xrTableCell2.Text = "NGÀY LẬP";
        // this.xrTableCell2.Weight = 0.677082799916868D;
        // // 
        // // xrTableCell3
        // // 
        // this.xrTableCell3.Name = "xrTableCell3";
        // this.xrTableCell3.Text = "INVOICE";
        // this.xrTableCell3.Weight = 0.82812461763983636D;
        // // 
        // // xrTableCell16
        // // 
        // this.xrTableCell16.Name = "xrTableCell16";
        // this.xrTableCell16.Text = "ĐƠN HÀNG";
        // this.xrTableCell16.Weight = 0.846198689944234D;
        // // 
        // // xrTableCell4
        // // 
        // this.xrTableCell4.Multiline = true;
        // this.xrTableCell4.Name = "xrTableCell4";
        // this.xrTableCell4.Text = "TRỊ GIÁ\r\nINVOICE";
        // this.xrTableCell4.Weight = 0.71078213079076291D;
        // // 
        // // xrTableCell5
        // // 
        // this.xrTableCell5.Multiline = true;
        // this.xrTableCell5.Name = "xrTableCell5";
        // this.xrTableCell5.Text = "NGÀY MỞ \r\nTỜ KHAI";
        // this.xrTableCell5.Weight = 0.7784885030189469D;
        // // 
        // // xrTableCell6
        // // 
        // this.xrTableCell6.Multiline = true;
        // this.xrTableCell6.Name = "xrTableCell6";
        // this.xrTableCell6.Text = "NGÀY PHẢI \r\nTHANH TOÁN";
        // this.xrTableCell6.Weight = 0.89966186274999382D;
        // // 
        // // xrTableCell7
        // // 
        // this.xrTableCell7.Multiline = true;
        // this.xrTableCell7.Name = "xrTableCell7";
        // this.xrTableCell7.Text = "SỐ NGÀY \r\nCHẬM TRẢ";
        // this.xrTableCell7.Weight = 1.0123302969812509D;
        // // 
        // // xrTableCell19
        // // 
        // this.xrTableCell19.Name = "xrTableCell19";
        // this.xrTableCell19.Text = "PAYMENT TERM";
        // this.xrTableCell19.Weight = 0.9914974731646502D;
        // // 
        // // ReportFooter
        // // 
        // this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable3});
        // this.ReportFooter.HeightF = 25F;
        // this.ReportFooter.Name = "ReportFooter";
        // // 
        // // xrTable3
        // // 
        // this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    // | DevExpress.XtraPrinting.BorderSide.Right)
                    // | DevExpress.XtraPrinting.BorderSide.Bottom)));
        // this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        // this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        // this.xrTable3.Name = "xrTable3";
        // this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        // this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow3});
        // this.xrTable3.SizeF = new System.Drawing.SizeF(714F, 25F);
        // this.xrTable3.StylePriority.UseBorders = false;
        // this.xrTable3.StylePriority.UseFont = false;
        // this.xrTable3.StylePriority.UsePadding = false;
        // this.xrTable3.StylePriority.UseTextAlignment = false;
        // this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // // 
        // // xrTableRow3
        // // 
        // this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell15,
            // this.xrTableCell18,
            // this.xrTableCell20,
            // this.xrTableCell21});
        // this.xrTableRow3.Name = "xrTableRow3";
        // this.xrTableRow3.Weight = 1D;
        // // 
        // // xrTableCell15
        // // 
        // this.xrTableCell15.Name = "xrTableCell15";
        // this.xrTableCell15.StylePriority.UseTextAlignment = false;
        // this.xrTableCell15.Text = "TỔNG CỘNG";
        // this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell15.Weight = 1.9010406232259043D;
        // // 
        // // xrTableCell18
        // // 
        // this.xrTableCell18.Name = "xrTableCell18";
        // this.xrTableCell18.Weight = 0.84619872507758265D;
        // // 
        // // xrTableCell20
        // // 
        // this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgross", "{0:#,#.00}")});
        // this.xrTableCell20.Multiline = true;
        // this.xrTableCell20.Name = "xrTableCell20";
        // this.xrTableCell20.StylePriority.UseTextAlignment = false;
        // xrSummary2.FormatString = "{0:#,#.00}";
        // xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        // this.xrTableCell20.Summary = xrSummary2;
        // this.xrTableCell20.Text = "[totalgross]";
        // this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell20.Weight = 0.71078182260137213D;
        // // 
        // // xrTableCell21
        // // 
        // this.xrTableCell21.Multiline = true;
        // this.xrTableCell21.Name = "xrTableCell21";
        // this.xrTableCell21.Weight = 3.6819785239193603D;
        // // 
        // // xrLabel2
        // // 
        // this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(142.7083F, 58.33337F);
        // this.xrLabel2.Name = "xrLabel2";
        // this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        // this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 23F);
        // this.xrLabel2.StylePriority.UseTextAlignment = false;
        // this.xrLabel2.Text = "Từ ngày";
        // this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrLabel3
        // // 
        // this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:MM/dd/yyyy}")});
        // this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        // this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(245.8333F, 58.33337F);
        // this.xrLabel3.Name = "xrLabel3";
        
        // this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel3.SizeF = new System.Drawing.SizeF(100F, 23F);
        // this.xrLabel3.StylePriority.UseTextAlignment = false;
		// xrSummary2.FormatString = "{0:MM/dd/yyyy}";
		// xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
		
        // this.xrLabel3.Text = "[tungay]";
        // this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrLabel4
        // // 
        // this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(359.0677F, 58.33337F);
        // this.xrLabel4.Name = "xrLabel4";
        // this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel4.SizeF = new System.Drawing.SizeF(100F, 23F);
        // this.xrLabel4.StylePriority.UseTextAlignment = false;
        // this.xrLabel4.Text = "Đến ngày";
        // this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrLabel5
        // // 
        // this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0}")});
        // this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        // this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(462.1927F, 58.33337F);
        // this.xrLabel5.Name = "xrLabel5";
        // this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 23F);
        // this.xrLabel5.StylePriority.UseTextAlignment = false;
        // this.xrLabel5.Text = "[denngay]";
        // this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // rptInvoiceChamThanhToan
        // // 
        // this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            // this.Detail,
            // this.TopMargin,
            // this.BottomMargin,
            // this.ReportHeader,
            // this.PageHeader,
            // this.ReportFooter});
        // this.Margins = new System.Drawing.Printing.Margins(52, 61, 50, 47);
        // this.PageHeight = 1169;
        // this.PageWidth = 827;
        // this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        // this.Version = "12.2";
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        // ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	// }

	// #endregion
// }
// */

// using System;
// using System.Drawing;
// using System.Collections;
// using System.ComponentModel;
// using DevExpress.XtraReports.UI;

// /// <summary>
// /// Summary description for rptInvoiceChamThanhToan
// /// </summary>
// public class rptInvoiceChamThanhToan : DevExpress.XtraReports.UI.XtraReport
// {
    // private DevExpress.XtraReports.UI.DetailBand Detail;
    // private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    // private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    // private XRTable xrTable2;
    // private XRTableRow xrTableRow2;
    // private XRTableCell xrTableCell8;
    // private XRTableCell xrTableCell9;
    // private XRTableCell xrTableCell10;
    // private XRTableCell xrTableCell11;
    // private XRTableCell xrTableCell12;
    // private XRTableCell xrTableCell13;
    // private XRTableCell xrTableCell14;
    // private ReportHeaderBand ReportHeader;
    // private XRLabel xrLabel1;
    // private PageHeaderBand PageHeader;
    // private XRTable xrTable1;
    // private XRTableRow xrTableRow1;
    // private XRTableCell xrTableCell1;
    // private XRTableCell xrTableCell2;
    // private XRTableCell xrTableCell3;
    // private XRTableCell xrTableCell4;
    // private XRTableCell xrTableCell5;
    // private XRTableCell xrTableCell6;
    // private XRTableCell xrTableCell7;
    // private ReportFooterBand ReportFooter;
    // private XRTable xrTable3;
    // private XRTableRow xrTableRow3;
    // private XRTableCell xrTableCell15;
    // private XRTableCell xrTableCell20;
    // private XRTableCell xrTableCell21;
    // private XRTableCell xrTableCell17;
    // private XRTableCell xrTableCell22;
    // private XRTableCell xrTableCell16;
    // private XRTableCell xrTableCell19;
    // private XRTableCell xrTableCell18;
    // private XRLabel xrLabel5;
    // private XRLabel xrLabel4;
    // private XRLabel xrLabel3;
    // private XRLabel xrLabel2;
    // /// <summary>
    // /// Required designer variable.
    // /// </summary>
    // private System.ComponentModel.IContainer components = null;

    // public rptInvoiceChamThanhToan()
    // {
        // InitializeComponent();
        // //
        // // TODO: Add constructor logic here
        // //
    // }

    // /// <summary> 
    // /// Clean up any resources being used.
    // /// </summary>
    // /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    // protected override void Dispose(bool disposing)
    // {
        // if (disposing && (components != null))
        // {
            // components.Dispose();
        // }
        // base.Dispose(disposing);
    // }

    // #region Designer generated code

    // /// <summary>
    // /// Required method for Designer support - do not modify
    // /// the contents of this method with the code editor.
    // /// </summary>
    // private void InitializeComponent()
    // {
        // string resourceFileName = "rptInvoiceChamThanhToan.resx";
        // DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        // DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        // DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        // DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        // this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        // this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        // this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        // this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        // this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        // this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        // this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        // this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        // this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        // this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        // this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        // this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        // this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        // this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        // this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        // this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        // this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        // this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        // this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        // ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // // 
        // // Detail
        // // 
        // this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable2});
        // this.Detail.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        // this.Detail.HeightF = 25F;
        // this.Detail.Name = "Detail";
        // this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // this.Detail.StylePriority.UseFont = false;
        // this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // // 
        // // xrTable2
        // // 
        // this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    // | DevExpress.XtraPrinting.BorderSide.Bottom)));
        // this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        // this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        // this.xrTable2.Name = "xrTable2";
        // this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        // this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow2});
        // this.xrTable2.SizeF = new System.Drawing.SizeF(714F, 25F);
        // this.xrTable2.StylePriority.UseBorders = false;
        // this.xrTable2.StylePriority.UseFont = false;
        // this.xrTable2.StylePriority.UsePadding = false;
        // this.xrTable2.StylePriority.UseTextAlignment = false;
        // this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrTableRow2
        // // 
        // this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell8,
            // this.xrTableCell9,
            // this.xrTableCell10,
            // this.xrTableCell17,
            // this.xrTableCell11,
            // this.xrTableCell12,
            // this.xrTableCell13,
            // this.xrTableCell14,
            // this.xrTableCell22});
        // this.xrTableRow2.Name = "xrTableRow2";
        // this.xrTableRow2.Weight = 1D;
        // // 
        // // xrTableCell8
        // // 
        // this.xrTableCell8.Name = "xrTableCell8";
        // this.xrTableCell8.StylePriority.UseTextAlignment = false;
        // xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
        // xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        // this.xrTableCell8.Summary = xrSummary1;
        // this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell8.Weight = 0.39583332061767573D;
        // // 
        // // xrTableCell9
        // // 
        // this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaylap", "{0:dd/MM/yyyy}")});
        // this.xrTableCell9.Name = "xrTableCell9";
        // this.xrTableCell9.Text = "xrTableCell9";
        // this.xrTableCell9.Weight = 0.677082799916868D;
        // // 
        // // xrTableCell10
        // // 
        // this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_inv")});
        // this.xrTableCell10.Name = "xrTableCell10";
        // this.xrTableCell10.Text = "[so_inv]";
        // this.xrTableCell10.Weight = 0.82812461763983636D;
        // // 
        // // xrTableCell17
        // // 
        // this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "donhang")});
        // this.xrTableCell17.Name = "xrTableCell17";
        // this.xrTableCell17.Text = "[donhang]";
        // this.xrTableCell17.Weight = 0.846198689944234D;
        // // 
        // // xrTableCell11
        // // 
        // this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgross", "{0:#,#.00}")});
        // this.xrTableCell11.Name = "xrTableCell11";
        // this.xrTableCell11.StylePriority.UseTextAlignment = false;
        // this.xrTableCell11.Text = "xrTableCell11";
        // this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell11.Weight = 0.71078213079076291D;
        // // 
        // // xrTableCell12
        // // 
        // this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngay_motokhai", "{0:dd/MM/yyyy}")});
        // this.xrTableCell12.Name = "xrTableCell12";
        // this.xrTableCell12.Text = "xrTableCell12";
        // this.xrTableCell12.Weight = 0.7784885030189469D;
        // // 
        // // xrTableCell13
        // // 
        // this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngayphaithanhtoan", "{0:dd/MM/yyyy}")});
        // this.xrTableCell13.Name = "xrTableCell13";
        // this.xrTableCell13.Text = "xrTableCell13";
        // this.xrTableCell13.Weight = 0.89966186274999382D;
        // // 
        // // xrTableCell14
        // // 
        // this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "songaychamtra")});
        // this.xrTableCell14.Name = "xrTableCell14";
        // this.xrTableCell14.StylePriority.UseTextAlignment = false;
        // this.xrTableCell14.Text = "[songaychamtra]";
        // this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell14.Weight = 1.0123302969812509D;
        // // 
        // // xrTableCell22
        // // 
        // this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "paymentterm")});
        // this.xrTableCell22.Name = "xrTableCell22";
        // this.xrTableCell22.Text = "[paymentterm]";
        // this.xrTableCell22.Weight = 0.9914974731646502D;
        // // 
        // // TopMargin
        // // 
        // this.TopMargin.HeightF = 50F;
        // this.TopMargin.Name = "TopMargin";
        // this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // // 
        // // BottomMargin
        // // 
        // this.BottomMargin.HeightF = 47F;
        // this.BottomMargin.Name = "BottomMargin";
        // this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // // 
        // // ReportHeader
        // // 
        // this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrLabel5,
            // this.xrLabel4,
            // this.xrLabel3,
            // this.xrLabel2,
            // this.xrLabel1});
        // this.ReportHeader.HeightF = 81.33337F;
        // this.ReportHeader.Name = "ReportHeader";
        // // 
        // // xrLabel5
        // // 
        // this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay","{0:MM/dd/yyyy}")});
        // this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        // this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(462.1927F, 58.33337F);
        // this.xrLabel5.Name = "xrLabel5";
        // this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 23F);
        // this.xrLabel5.StylePriority.UseTextAlignment = false;
       // // xrSummary2.FormatString = "{0:MM/dd/yyyy}";
       // // xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
       // // xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        // //this.xrLabel5.Summary = xrSummary2;
        // this.xrLabel5.Text = "[denngay]";
        // this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrLabel4
        // // 
        // this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(359.0677F, 58.33337F);
        // this.xrLabel4.Name = "xrLabel4";
        // this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel4.SizeF = new System.Drawing.SizeF(100F, 23F);
        // this.xrLabel4.StylePriority.UseTextAlignment = false;
        // this.xrLabel4.Text = "Đến ngày";
        // this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrLabel3
        // // 
        // this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay" ,"{0:MM/dd/yyyy}")});
        // this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        // this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(245.8333F, 58.33337F);
        // this.xrLabel3.Name = "xrLabel3";
        // this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel3.SizeF = new System.Drawing.SizeF(100F, 23F);
        // this.xrLabel3.StylePriority.UseTextAlignment = false;
       // //xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
       // // xrsummary2.func = Devexpress.xtrareports.ui.summaryfunc.dsum;
       // // this.xrLabel3.Summary = xrSummary3;
        // //xrSummary3.FormatString = "{0:MM/dd/yyyy}";
        
        
        // this.xrLabel3.Text = "[tungay]";
        // this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrLabel2
        // // 
        // this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(142.7083F, 58.33337F);
        // this.xrLabel2.Name = "xrLabel2";
        // this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 23F);
        // this.xrLabel2.StylePriority.UseTextAlignment = false;
        // this.xrLabel2.Text = "Từ ngày";
        // this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // // 
        // // xrLabel1
        // // 
        // this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
        // this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        // this.xrLabel1.Name = "xrLabel1";
        // this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // this.xrLabel1.SizeF = new System.Drawing.SizeF(714F, 48F);
        // this.xrLabel1.StylePriority.UseFont = false;
        // this.xrLabel1.StylePriority.UseTextAlignment = false;
        // this.xrLabel1.Text = "DANH SÁCH INVOICE CHẬM THANH TOÁN";
        // this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // // 
        // // PageHeader
        // // 
        // this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable1});
        // this.PageHeader.HeightF = 45.83333F;
        // this.PageHeader.Name = "PageHeader";
        // // 
        // // xrTable1
        // // 
        // this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    // | DevExpress.XtraPrinting.BorderSide.Right)
                    // | DevExpress.XtraPrinting.BorderSide.Bottom)));
        // this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        // this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        // this.xrTable1.Name = "xrTable1";
        // this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow1});
        // this.xrTable1.SizeF = new System.Drawing.SizeF(714F, 45.83333F);
        // this.xrTable1.StylePriority.UseBorders = false;
        // this.xrTable1.StylePriority.UseFont = false;
        // this.xrTable1.StylePriority.UseTextAlignment = false;
        // this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // // 
        // // xrTableRow1
        // // 
        // this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell1,
            // this.xrTableCell2,
            // this.xrTableCell3,
            // this.xrTableCell16,
            // this.xrTableCell4,
            // this.xrTableCell5,
            // this.xrTableCell6,
            // this.xrTableCell7,
            // this.xrTableCell19});
        // this.xrTableRow1.Name = "xrTableRow1";
        // this.xrTableRow1.Weight = 1D;
        // // 
        // // xrTableCell1
        // // 
        // this.xrTableCell1.Name = "xrTableCell1";
        // this.xrTableCell1.Text = "STT";
        // this.xrTableCell1.Weight = 0.39583332061767573D;
        // // 
        // // xrTableCell2
        // // 
        // this.xrTableCell2.Name = "xrTableCell2";
        // this.xrTableCell2.Text = "NGÀY LẬP";
        // this.xrTableCell2.Weight = 0.677082799916868D;
        // // 
        // // xrTableCell3
        // // 
        // this.xrTableCell3.Name = "xrTableCell3";
        // this.xrTableCell3.Text = "INVOICE";
        // this.xrTableCell3.Weight = 0.82812461763983636D;
        // // 
        // // xrTableCell16
        // // 
        // this.xrTableCell16.Name = "xrTableCell16";
        // this.xrTableCell16.Text = "ĐƠN HÀNG";
        // this.xrTableCell16.Weight = 0.846198689944234D;
        // // 
        // // xrTableCell4
        // // 
        // this.xrTableCell4.Multiline = true;
        // this.xrTableCell4.Name = "xrTableCell4";
        // this.xrTableCell4.Text = "TRỊ GIÁ\r\nINVOICE";
        // this.xrTableCell4.Weight = 0.71078213079076291D;
        // // 
        // // xrTableCell5
        // // 
        // this.xrTableCell5.Multiline = true;
        // this.xrTableCell5.Name = "xrTableCell5";
        // this.xrTableCell5.Text = "NGÀY MỞ \r\nTỜ KHAI";
        // this.xrTableCell5.Weight = 0.7784885030189469D;
        // // 
        // // xrTableCell6
        // // 
        // this.xrTableCell6.Multiline = true;
        // this.xrTableCell6.Name = "xrTableCell6";
        // this.xrTableCell6.Text = "NGÀY PHẢI \r\nTHANH TOÁN";
        // this.xrTableCell6.Weight = 0.89966186274999382D;
        // // 
        // // xrTableCell7
        // // 
        // this.xrTableCell7.Multiline = true;
        // this.xrTableCell7.Name = "xrTableCell7";
        // this.xrTableCell7.Text = "SỐ NGÀY \r\nCHẬM TRẢ";
        // this.xrTableCell7.Weight = 1.0123302969812509D;
        // // 
        // // xrTableCell19
        // // 
        // this.xrTableCell19.Name = "xrTableCell19";
        // this.xrTableCell19.Text = "PAYMENT TERM";
        // this.xrTableCell19.Weight = 0.9914974731646502D;
        // // 
        // // ReportFooter
        // // 
        // this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            // this.xrTable3});
        // this.ReportFooter.HeightF = 25F;
        // this.ReportFooter.Name = "ReportFooter";
        // // 
        // // xrTable3
        // // 
        // this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    // | DevExpress.XtraPrinting.BorderSide.Right)
                    // | DevExpress.XtraPrinting.BorderSide.Bottom)));
        // this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        // this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        // this.xrTable3.Name = "xrTable3";
        // this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        // this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            // this.xrTableRow3});
        // this.xrTable3.SizeF = new System.Drawing.SizeF(714F, 25F);
        // this.xrTable3.StylePriority.UseBorders = false;
        // this.xrTable3.StylePriority.UseFont = false;
        // this.xrTable3.StylePriority.UsePadding = false;
        // this.xrTable3.StylePriority.UseTextAlignment = false;
        // this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // // 
        // // xrTableRow3
        // // 
        // this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            // this.xrTableCell15,
            // this.xrTableCell18,
            // this.xrTableCell20,
            // this.xrTableCell21});
        // this.xrTableRow3.Name = "xrTableRow3";
        // this.xrTableRow3.Weight = 1D;
        // // 
        // // xrTableCell15
        // // 
        // this.xrTableCell15.Name = "xrTableCell15";
        // this.xrTableCell15.StylePriority.UseTextAlignment = false;
        // this.xrTableCell15.Text = "TỔNG CỘNG";
        // this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell15.Weight = 1.9010406232259043D;
        // // 
        // // xrTableCell18
        // // 
        // this.xrTableCell18.Name = "xrTableCell18";
        // this.xrTableCell18.Weight = 0.84619872507758265D;
        // // 
        // // xrTableCell20
        // // 
        // this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            // new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgross", "{0:#,#.00}")});
        // this.xrTableCell20.Multiline = true;
        // this.xrTableCell20.Name = "xrTableCell20";
        // this.xrTableCell20.StylePriority.UseTextAlignment = false;
        // xrSummary4.FormatString = "{0:#,#.00}";
        // xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        // this.xrTableCell20.Summary = xrSummary4;
        // this.xrTableCell20.Text = "[totalgross]";
        // this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // this.xrTableCell20.Weight = 0.71078182260137213D;
        // // 
        // // xrTableCell21
        // // 
        // this.xrTableCell21.Multiline = true;
        // this.xrTableCell21.Name = "xrTableCell21";
        // this.xrTableCell21.Weight = 3.6819785239193603D;
        // // 
        // // rptInvoiceChamThanhToan
        // // 
        // this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            // this.Detail,
            // this.TopMargin,
            // this.BottomMargin,
            // this.ReportHeader,
            // this.PageHeader,
            // this.ReportFooter});
        // this.Margins = new System.Drawing.Printing.Margins(52, 61, 50, 47);
        // this.PageHeight = 1169;
        // this.PageWidth = 827;
        // this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        // this.Version = "12.2";
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        // ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        // ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    // }

    // #endregion
// }


