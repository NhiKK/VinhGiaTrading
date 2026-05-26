using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptHoachToanTheoInvoice
/// </summary>
public class rptHoachToanTheoInvoice : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel denngay;
    private XRLabel today;
    private XRLabel xrLabel8;
    private XRLabel xrLabel6;
    private XRLabel tungay;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel3;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell stt;
    private XRTableCell phieunhap;
    private XRTableCell datra;
    private XRTableCell conlai;
    private ReportFooterBand ReportFooter;
    private XRLabel tongcongdatra;
    private XRLabel xrLabel7;
    private XRLabel tongcongtienPN;
    private GroupHeaderBand GroupHeader1;
    private XRLabel ma_dtkd;
    private XRLabel xrLabel9;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell tongtienphieunhap;
    private XRLabel so_inv;
    private XRLabel xrLabel10;
    private XRTableCell xrTableCell5;
    private GroupHeaderBand GroupHeader2;
    private GroupFooterBand GroupFooter1;
    private XRLabel tongcongphaitra;
    private XRLabel tongcongdatra3;
    private XRLabel tongcongphaitra3;
    private XRLabel xrLabel11;
    private XRLabel tongcongtienPN3;
    private GroupFooterBand GroupFooter2;
    private XRLabel xrLabel13;
    private XRLabel tongcongphaitra2;
    private XRLabel tongcongdatra2;
    private XRLabel tongcongtienPN2;
    private XRLabel so_inv2;
    private XRLabel ma_dtkd2;
    private XRLabel stt_so_inv;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptHoachToanTheoInvoice()
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
        string resourceFileName = "rptHoachToanTheoInvoice.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.stt = new DevExpress.XtraReports.UI.XRTableCell();
        this.phieunhap = new DevExpress.XtraReports.UI.XRTableCell();
        this.tongtienphieunhap = new DevExpress.XtraReports.UI.XRTableCell();
        this.datra = new DevExpress.XtraReports.UI.XRTableCell();
        this.conlai = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.denngay = new DevExpress.XtraReports.UI.XRLabel();
        this.today = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.tungay = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.tongcongdatra3 = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongphaitra3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongtienPN3 = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongtienPN = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongdatra = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.so_inv = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.ma_dtkd = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.so_inv2 = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongphaitra = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongphaitra2 = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongdatra2 = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongtienPN2 = new DevExpress.XtraReports.UI.XRLabel();
        this.ma_dtkd2 = new DevExpress.XtraReports.UI.XRLabel();
        this.stt_so_inv = new DevExpress.XtraReports.UI.XRLabel();
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
        this.xrTable2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 2;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(53.12519F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(784.8749F, 25F);
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.stt,
            this.phieunhap,
            this.tongtienphieunhap,
            this.datra,
            this.conlai});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // stt
        // 
        this.stt.Multiline = true;
        this.stt.Name = "stt";
        this.stt.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.stt.StylePriority.UsePadding = false;
        this.stt.StylePriority.UseTextAlignment = false;
        xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.stt.Summary = xrSummary1;
        this.stt.Text = "STT";
        this.stt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.stt.Weight = 0.289731295881499D;
        // 
        // phieunhap
        // 
        this.phieunhap.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phieunhap")});
        this.phieunhap.Multiline = true;
        this.phieunhap.Name = "phieunhap";
        this.phieunhap.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.phieunhap.StylePriority.UsePadding = false;
        this.phieunhap.StylePriority.UseTextAlignment = false;
        this.phieunhap.Text = "[ma_dtkd]";
        this.phieunhap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.phieunhap.Weight = 0.52078221753706078D;
        // 
        // tongtienphieunhap
        // 
        this.tongtienphieunhap.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienphieunhap", "{0:#,#.00}")});
        this.tongtienphieunhap.Name = "tongtienphieunhap";
        this.tongtienphieunhap.StylePriority.UseTextAlignment = false;
        this.tongtienphieunhap.Text = "[tongtienphieunhap]";
        this.tongtienphieunhap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.tongtienphieunhap.Weight = 0.73378902870596063D;
        // 
        // datra
        // 
        this.datra.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "datra", "{0:#,#.00}")});
        this.datra.Multiline = true;
        this.datra.Name = "datra";
        this.datra.NullValueText = "0";
        this.datra.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.datra.StylePriority.UsePadding = false;
        this.datra.StylePriority.UseTextAlignment = false;
        this.datra.Text = "[tongthu]";
        this.datra.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.datra.Weight = 0.62515883168644082D;
        // 
        // conlai
        // 
        this.conlai.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "conlai", "{0:#,#.00}")});
        this.conlai.Multiline = true;
        this.conlai.Name = "conlai";
        this.conlai.NullValueText = "0";
        this.conlai.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.conlai.StylePriority.UsePadding = false;
        this.conlai.StylePriority.UseTextAlignment = false;
        this.conlai.Text = "[tongchi]";
        this.conlai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.conlai.Weight = 0.59391253033747282D;
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
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.denngay,
            this.today,
            this.xrLabel8,
            this.xrLabel6,
            this.tungay,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel3,
            this.xrLabel5,
            this.xrLabel4});
        this.ReportHeader.HeightF = 177.0833F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // denngay
        // 
        this.denngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MM/yyyy}")});
        this.denngay.LocationFloat = new DevExpress.Utils.PointFloat(631.3334F, 146.7917F);
        this.denngay.Name = "denngay";
        this.denngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.denngay.SizeF = new System.Drawing.SizeF(108.3333F, 23F);
        this.denngay.StylePriority.UseTextAlignment = false;
        this.denngay.Text = "xrLabel6";
        this.denngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // today
        // 
        this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
        this.today.LocationFloat = new DevExpress.Utils.PointFloat(631.3334F, 116.9167F);
        this.today.Name = "today";
        this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.today.SizeF = new System.Drawing.SizeF(206.6667F, 22.99998F);
        this.today.StylePriority.UseTextAlignment = false;
        this.today.Text = "[today]";
        this.today.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel8
        // 
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(491.7501F, 146.7917F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(139.5834F, 23F);
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Đến ngày";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(70.31247F, 146.7917F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Từ ngày:";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tungay
        // 
        this.tungay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MM/yyyy}")});
        this.tungay.LocationFloat = new DevExpress.Utils.PointFloat(170.3125F, 146.7917F);
        this.tungay.Name = "tungay";
        this.tungay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tungay.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.tungay.StylePriority.UseTextAlignment = false;
        this.tungay.Text = "xrLabel6";
        this.tungay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel2
        // 
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 29.16667F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(828.0001F, 23F);
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(828F, 29.16667F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 52.16665F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(828F, 23F);
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel5
        // 
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(491.7501F, 116.9167F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(139.5834F, 22.99998F);
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Núi Thành, ngày:";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 86.62497F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(828F, 19.79166F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "HẠCH TOÁN THEO INVOICE VÀ NHÀ CUNG CẤP";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tongcongdatra3,
            this.tongcongphaitra3,
            this.xrLabel11,
            this.tongcongtienPN3});
        this.ReportFooter.HeightF = 37.5F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // tongcongdatra3
        // 
        this.tongcongdatra3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "datra")});
        this.tongcongdatra3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.tongcongdatra3.LocationFloat = new DevExpress.Utils.PointFloat(490.7912F, 0F);
        this.tongcongdatra3.Name = "tongcongdatra3";
        this.tongcongdatra3.NullValueText = "0";
        this.tongcongdatra3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongdatra3.SizeF = new System.Drawing.SizeF(177.5626F, 23F);
        this.tongcongdatra3.StylePriority.UseFont = false;
        this.tongcongdatra3.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:#,#.00}";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.tongcongdatra3.Summary = xrSummary2;
        this.tongcongdatra3.Text = "tongcongdatra3";
        this.tongcongdatra3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tongcongphaitra3
        // 
        this.tongcongphaitra3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "conlai")});
        this.tongcongphaitra3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.tongcongphaitra3.LocationFloat = new DevExpress.Utils.PointFloat(668.3535F, 0F);
        this.tongcongphaitra3.Name = "tongcongphaitra3";
        this.tongcongphaitra3.NullValueText = "0";
        this.tongcongphaitra3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongphaitra3.SizeF = new System.Drawing.SizeF(169.6466F, 23F);
        this.tongcongphaitra3.StylePriority.UseFont = false;
        this.tongcongphaitra3.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:#,#.00}";
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.tongcongphaitra3.Summary = xrSummary3;
        this.tongcongphaitra3.Text = "tongcongphaitra3";
        this.tongcongphaitra3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(272.3745F, 23F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "Tổng Cộng cuối cùng: ";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // tongcongtienPN3
        // 
        this.tongcongtienPN3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienphieunhap")});
        this.tongcongtienPN3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.tongcongtienPN3.LocationFloat = new DevExpress.Utils.PointFloat(282.3747F, 0F);
        this.tongcongtienPN3.Name = "tongcongtienPN3";
        this.tongcongtienPN3.NullValueText = "0";
        this.tongcongtienPN3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongtienPN3.SizeF = new System.Drawing.SizeF(208.4165F, 23F);
        this.tongcongtienPN3.StylePriority.UseFont = false;
        this.tongcongtienPN3.StylePriority.UseTextAlignment = false;
        xrSummary4.FormatString = "{0:#,#.00}";
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.tongcongtienPN3.Summary = xrSummary4;
        this.tongcongtienPN3.Text = "tongcongtienPN3";
        this.tongcongtienPN3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tongcongtienPN
        // 
        this.tongcongtienPN.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienphieunhap")});
        this.tongcongtienPN.LocationFloat = new DevExpress.Utils.PointFloat(283.3335F, 0F);
        this.tongcongtienPN.Name = "tongcongtienPN";
        this.tongcongtienPN.NullValueText = "0";
        this.tongcongtienPN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongtienPN.SizeF = new System.Drawing.SizeF(208.4165F, 43.83329F);
        this.tongcongtienPN.StylePriority.UseTextAlignment = false;
        xrSummary5.FormatString = "{0:#,#.00}";
        xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.tongcongtienPN.Summary = xrSummary5;
        this.tongcongtienPN.Text = "tongcongtienPN";
        this.tongcongtienPN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(53.12519F, 0F);
        this.xrLabel7.Multiline = true;
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(82.29173F, 43.83329F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Tổng cộng theo INV: ";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tongcongdatra
        // 
        this.tongcongdatra.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "datra")});
        this.tongcongdatra.LocationFloat = new DevExpress.Utils.PointFloat(491.75F, 0F);
        this.tongcongdatra.Name = "tongcongdatra";
        this.tongcongdatra.NullValueText = "0";
        this.tongcongdatra.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongdatra.SizeF = new System.Drawing.SizeF(177.5626F, 43.83329F);
        this.tongcongdatra.StylePriority.UseTextAlignment = false;
        xrSummary6.FormatString = "{0:#,#.00}";
        xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.tongcongdatra.Summary = xrSummary6;
        this.tongcongdatra.Text = "tongcongdatra";
        this.tongcongdatra.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.stt_so_inv,
            this.so_inv,
            this.xrLabel10,
            this.xrTable1});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("so_inv", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 62.91666F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.StylePriority.UseTextAlignment = false;
        this.GroupHeader1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // so_inv
        // 
        this.so_inv.BackColor = System.Drawing.Color.SlateBlue;
        this.so_inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_inv")});
        this.so_inv.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.so_inv.ForeColor = System.Drawing.Color.White;
        this.so_inv.LocationFloat = new DevExpress.Utils.PointFloat(135.417F, 0F);
        this.so_inv.Name = "so_inv";
        this.so_inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.so_inv.SizeF = new System.Drawing.SizeF(702.583F, 23F);
        this.so_inv.StylePriority.UseBackColor = false;
        this.so_inv.StylePriority.UseFont = false;
        this.so_inv.StylePriority.UseForeColor = false;
        this.so_inv.StylePriority.UseTextAlignment = false;
        this.so_inv.Text = "so_inv";
        this.so_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.BackColor = System.Drawing.Color.SlateBlue;
        this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.ForeColor = System.Drawing.Color.White;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(53.12519F, 0F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(82.2918F, 23F);
        this.xrLabel10.StylePriority.UseBackColor = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseForeColor = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "Số invoice:";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.BackColor = System.Drawing.Color.BlueViolet;
        this.xrTable1.BorderWidth = 2;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(53.12518F, 37.91666F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(784.875F, 25F);
        this.xrTable1.StylePriority.UseBackColor = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell4,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.DarkSlateBlue;
        this.xrTableCell1.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell1.ForeColor = System.Drawing.Color.White;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseBorderColor = false;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UseForeColor = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "STT";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell1.Weight = 0.29044140601083346D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.BackColor = System.Drawing.Color.DarkSlateBlue;
        this.xrTableCell4.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.BorderWidth = 2;
        this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell4.ForeColor = System.Drawing.Color.White;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.xrTableCell4.StylePriority.UseBackColor = false;
        this.xrTableCell4.StylePriority.UseBorderColor = false;
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseBorderWidth = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UseForeColor = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Phiếu Nhập";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell4.Weight = 0.52205857143727663D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.BackColor = System.Drawing.Color.DarkSlateBlue;
        this.xrTableCell2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.BorderWidth = 2;
        this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell2.ForeColor = System.Drawing.Color.White;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.xrTableCell2.StylePriority.UseBackColor = false;
        this.xrTableCell2.StylePriority.UseBorderColor = false;
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseBorderWidth = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UseForeColor = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Tổng tiền PN";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell2.Weight = 0.73558785483851208D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.BackColor = System.Drawing.Color.DarkSlateBlue;
        this.xrTableCell5.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell5.ForeColor = System.Drawing.Color.White;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseBackColor = false;
        this.xrTableCell5.StylePriority.UseBorderColor = false;
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UseForeColor = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "Đã chi trả";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell5.Weight = 0.62669100131071476D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BackColor = System.Drawing.Color.DarkSlateBlue;
        this.xrTableCell3.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.ForeColor = System.Drawing.Color.White;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.xrTableCell3.StylePriority.UseBackColor = false;
        this.xrTableCell3.StylePriority.UseBorderColor = false;
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UseForeColor = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "Còn lại phải trả";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell3.Weight = 0.5953674283410566D;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(170.3125F, 23F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "Nhà cung cấp:";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // ma_dtkd
        // 
        this.ma_dtkd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
        this.ma_dtkd.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ma_dtkd.LocationFloat = new DevExpress.Utils.PointFloat(170.3125F, 0F);
        this.ma_dtkd.Name = "ma_dtkd";
        this.ma_dtkd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.ma_dtkd.SizeF = new System.Drawing.SizeF(667.6876F, 23F);
        this.ma_dtkd.StylePriority.UseFont = false;
        this.ma_dtkd.StylePriority.UseTextAlignment = false;
        this.ma_dtkd.Text = "ma_dtkd";
        this.ma_dtkd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ma_dtkd,
            this.xrLabel9});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("ma_dtkd", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 36.45833F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.so_inv2,
            this.tongcongphaitra,
            this.xrLabel7,
            this.tongcongtienPN,
            this.tongcongdatra});
        this.GroupFooter1.HeightF = 43.83329F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // so_inv2
        // 
        this.so_inv2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_inv")});
        this.so_inv2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.so_inv2.LocationFloat = new DevExpress.Utils.PointFloat(135.4169F, 0F);
        this.so_inv2.Multiline = true;
        this.so_inv2.Name = "so_inv2";
        this.so_inv2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.so_inv2.SizeF = new System.Drawing.SizeF(147.9166F, 43.83329F);
        this.so_inv2.StylePriority.UseFont = false;
        this.so_inv2.StylePriority.UseTextAlignment = false;
        this.so_inv2.Text = "so_inv2";
        this.so_inv2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tongcongphaitra
        // 
        this.tongcongphaitra.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "conlai")});
        this.tongcongphaitra.LocationFloat = new DevExpress.Utils.PointFloat(669.3125F, 0F);
        this.tongcongphaitra.Name = "tongcongphaitra";
        this.tongcongphaitra.NullValueText = "0";
        this.tongcongphaitra.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongphaitra.SizeF = new System.Drawing.SizeF(168.6875F, 43.83329F);
        this.tongcongphaitra.StylePriority.UseTextAlignment = false;
        xrSummary8.FormatString = "{0:#,#.00}";
        xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.tongcongphaitra.Summary = xrSummary8;
        this.tongcongphaitra.Text = "tongcongphaitra";
        this.tongcongphaitra.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupFooter2
        // 
        this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.tongcongphaitra2,
            this.tongcongdatra2,
            this.tongcongtienPN2,
            this.ma_dtkd2});
        this.GroupFooter2.HeightF = 42.08334F;
        this.GroupFooter2.Level = 1;
        this.GroupFooter2.Name = "GroupFooter2";
        // 
        // xrLabel13
        // 
        this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(53.12519F, 0F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(82.29173F, 42.08333F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "Tổng cộng      theo NCC: ";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tongcongphaitra2
        // 
        this.tongcongphaitra2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "conlai")});
        this.tongcongphaitra2.LocationFloat = new DevExpress.Utils.PointFloat(668.3538F, 0F);
        this.tongcongphaitra2.Name = "tongcongphaitra2";
        this.tongcongphaitra2.NullValueText = "0";
        this.tongcongphaitra2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongphaitra2.SizeF = new System.Drawing.SizeF(169.6462F, 42.08334F);
        this.tongcongphaitra2.StylePriority.UseTextAlignment = false;
        xrSummary9.FormatString = "{0:#,#.00}";
        xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.tongcongphaitra2.Summary = xrSummary9;
        this.tongcongphaitra2.Text = "tongcongphaitra2";
        this.tongcongphaitra2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tongcongdatra2
        // 
        this.tongcongdatra2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "datra")});
        this.tongcongdatra2.LocationFloat = new DevExpress.Utils.PointFloat(490.7914F, 0F);
        this.tongcongdatra2.Name = "tongcongdatra2";
        this.tongcongdatra2.NullValueText = "0";
        this.tongcongdatra2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongdatra2.SizeF = new System.Drawing.SizeF(177.5626F, 42.08334F);
        this.tongcongdatra2.StylePriority.UseTextAlignment = false;
        xrSummary10.FormatString = "{0:#,#.00}";
        xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.tongcongdatra2.Summary = xrSummary10;
        this.tongcongdatra2.Text = "tongcongdatra2";
        this.tongcongdatra2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tongcongtienPN2
        // 
        this.tongcongtienPN2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienphieunhap")});
        this.tongcongtienPN2.LocationFloat = new DevExpress.Utils.PointFloat(282.3749F, 0F);
        this.tongcongtienPN2.Name = "tongcongtienPN2";
        this.tongcongtienPN2.NullValueText = "0";
        this.tongcongtienPN2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongtienPN2.SizeF = new System.Drawing.SizeF(208.4165F, 42.08334F);
        this.tongcongtienPN2.StylePriority.UseTextAlignment = false;
        xrSummary11.FormatString = "{0:#,#.00}";
        xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.tongcongtienPN2.Summary = xrSummary11;
        this.tongcongtienPN2.Text = "tongcongtienPN2";
        this.tongcongtienPN2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // ma_dtkd2
        // 
        this.ma_dtkd2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
        this.ma_dtkd2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ma_dtkd2.LocationFloat = new DevExpress.Utils.PointFloat(135.4169F, 0F);
        this.ma_dtkd2.Name = "ma_dtkd2";
        this.ma_dtkd2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.ma_dtkd2.SizeF = new System.Drawing.SizeF(147.9167F, 42.08334F);
        this.ma_dtkd2.StylePriority.UseFont = false;
        this.ma_dtkd2.StylePriority.UseTextAlignment = false;
        this.ma_dtkd2.Text = "so_inv2";
        this.ma_dtkd2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // stt_so_inv
        // 
        this.stt_so_inv.BackColor = System.Drawing.Color.SlateBlue;
        this.stt_so_inv.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.stt_so_inv.ForeColor = System.Drawing.Color.White;
        this.stt_so_inv.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.stt_so_inv.Name = "stt_so_inv";
        this.stt_so_inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.stt_so_inv.SizeF = new System.Drawing.SizeF(53.12516F, 23F);
        this.stt_so_inv.StylePriority.UseBackColor = false;
        this.stt_so_inv.StylePriority.UseFont = false;
        this.stt_so_inv.StylePriority.UseForeColor = false;
        this.stt_so_inv.StylePriority.UseTextAlignment = false;
        xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
        xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.stt_so_inv.Summary = xrSummary7;
        this.stt_so_inv.Text = "stt_so_inv";
        this.stt_so_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // rptHoachToanTheoInvoice
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupFooter1,
            this.GroupFooter2});
        this.Margins = new System.Drawing.Printing.Margins(0, 6, 0, 0);
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
