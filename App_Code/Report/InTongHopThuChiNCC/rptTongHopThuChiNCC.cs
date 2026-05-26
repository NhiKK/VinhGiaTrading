using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptTongHopThuChi
/// </summary>
public class rptTongHopThuChiNCC : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell stt;
    private XRTableCell ma_dtkd;
    private XRTableCell tongthu;
    private XRTableCell tongchi;
    private ReportHeaderBand ReportHeader;
    private XRLabel denngay;
    private XRLabel xrLabel8;
    private XRLabel tungay;
    private XRLabel xrLabel6;
    private XRLabel today;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private ReportFooterBand ReportFooter;
    private XRLabel tongcongchi;
    private XRLabel tongcongthu;
    private XRLabel xrLabel7;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptTongHopThuChiNCC()
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
        string resourceFileName = "rptTongHopThuChiNCC.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.stt = new DevExpress.XtraReports.UI.XRTableCell();
        this.ma_dtkd = new DevExpress.XtraReports.UI.XRTableCell();
        this.tongthu = new DevExpress.XtraReports.UI.XRTableCell();
        this.tongchi = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.denngay = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.tungay = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.today = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.tongcongchi = new DevExpress.XtraReports.UI.XRLabel();
        this.tongcongthu = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
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
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(848.9582F, 25F);
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.stt,
            this.ma_dtkd,
            this.tongthu,
            this.tongchi});
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
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.stt.Summary = xrSummary1;
        this.stt.Text = "STT";
        this.stt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.stt.Weight = 0.23838629585274435D;
        // 
        // ma_dtkd
        // 
        this.ma_dtkd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
        this.ma_dtkd.Multiline = true;
        this.ma_dtkd.Name = "ma_dtkd";
        this.ma_dtkd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.ma_dtkd.StylePriority.UsePadding = false;
        this.ma_dtkd.StylePriority.UseTextAlignment = false;
        this.ma_dtkd.Text = "[ma_dtkd]";
        this.ma_dtkd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.ma_dtkd.Weight = 0.75916888345165612D;
        // 
        // tongthu
        // 
        this.tongthu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongthu", "{0:#,#.00}")});
        this.tongthu.Multiline = true;
        this.tongthu.Name = "tongthu";
        this.tongthu.NullValueText = "0";
        this.tongthu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.tongthu.StylePriority.UsePadding = false;
        this.tongthu.StylePriority.UseTextAlignment = false;
        this.tongthu.Text = "[tongthu]";
        this.tongthu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.tongthu.Weight = 0.99755464207562861D;
        // 
        // tongchi
        // 
        this.tongchi.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongchi", "{0:#,#.00}")});
        this.tongchi.Multiline = true;
        this.tongchi.Name = "tongchi";
        this.tongchi.NullValueText = "0";
        this.tongchi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.tongchi.StylePriority.UsePadding = false;
        this.tongchi.StylePriority.UseTextAlignment = false;
        this.tongchi.Text = "[tongchi]";
        this.tongchi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.tongchi.Weight = 0.99388730358647492D;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 1.041667F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 199F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.denngay,
            this.xrLabel8,
            this.tungay,
            this.xrLabel6,
            this.today,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
        this.ReportHeader.HeightF = 176.0417F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // denngay
        // 
        this.denngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MM/yyyy}")});
        this.denngay.LocationFloat = new DevExpress.Utils.PointFloat(632.2917F, 145.75F);
        this.denngay.Name = "denngay";
        this.denngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.denngay.SizeF = new System.Drawing.SizeF(108.3333F, 23F);
        this.denngay.StylePriority.UseTextAlignment = false;
        this.denngay.Text = "xrLabel6";
        this.denngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel8
        // 
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(492.7083F, 145.75F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(139.5834F, 23F);
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Đến ngày";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tungay
        // 
        this.tungay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MM/yyyy}")});
        this.tungay.LocationFloat = new DevExpress.Utils.PointFloat(250F, 145.75F);
        this.tungay.Name = "tungay";
        this.tungay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tungay.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.tungay.StylePriority.UseTextAlignment = false;
        this.tungay.Text = "xrLabel6";
        this.tungay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(150F, 145.75F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Từ ngày:";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // today
        // 
        this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
        this.today.LocationFloat = new DevExpress.Utils.PointFloat(632.2916F, 115.875F);
        this.today.Name = "today";
        this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.today.SizeF = new System.Drawing.SizeF(216.6667F, 22.99998F);
        this.today.StylePriority.UseTextAlignment = false;
        this.today.Text = "[today]";
        this.today.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(492.7083F, 115.875F);
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
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 86.62497F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(848.9584F, 19.79166F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "TỔNG HỢP THU CHI THEO NHÀ CUNG CẤP";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 52.16665F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(848.9583F, 23F);
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 29.16667F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(848.9583F, 23F);
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(848.9583F, 29.16667F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.PageHeader.HeightF = 25F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrTable1
        // 
        this.xrTable1.BackColor = System.Drawing.Color.BlueViolet;
        this.xrTable1.BorderWidth = 2;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(848.9583F, 25F);
        this.xrTable1.StylePriority.UseBackColor = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell4,
            this.xrTableCell2,
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
        this.xrTableCell1.Weight = 0.23897057925953585D;
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
        this.xrTableCell4.Text = "Mã Nhà Cung Cấp";
        this.xrTableCell4.Weight = 0.7610294207404642D;
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
        this.xrTableCell2.Text = "Tổng thu (VND)";
        this.xrTableCell2.Weight = 1D;
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
        this.xrTableCell3.Text = "Tổng chi (VND)";
        this.xrTableCell3.Weight = 0.9963232421875D;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tongcongchi,
            this.tongcongthu,
            this.xrLabel7});
        this.ReportFooter.HeightF = 25F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // tongcongchi
        // 
        this.tongcongchi.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongchi")});
        this.tongcongchi.LocationFloat = new DevExpress.Utils.PointFloat(566.6666F, 0F);
        this.tongcongchi.Name = "tongcongchi";
        this.tongcongchi.NullValueText = "0";
        this.tongcongchi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongchi.SizeF = new System.Drawing.SizeF(282.2916F, 23F);
        this.tongcongchi.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:#,#.00}";
        xrSummary2.IgnoreNullValues = true;
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.tongcongchi.Summary = xrSummary2;
        this.tongcongchi.Text = "tongcongchi";
        this.tongcongchi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tongcongthu
        // 
        this.tongcongthu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongthu")});
        this.tongcongthu.LocationFloat = new DevExpress.Utils.PointFloat(283.3333F, 0F);
        this.tongcongthu.Name = "tongcongthu";
        this.tongcongthu.NullValueText = "0";
        this.tongcongthu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tongcongthu.SizeF = new System.Drawing.SizeF(283.3333F, 23F);
        this.tongcongthu.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:#,#.00}";
        xrSummary3.IgnoreNullValues = true;
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.tongcongthu.Summary = xrSummary3;
        this.tongcongthu.Text = "tongcongthu";
        this.tongcongthu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(283.3333F, 23F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Tổng Cộng: ";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // rptTongHopThuChi
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 1, 199);
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
