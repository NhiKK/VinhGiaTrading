using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptDoanhSoTheoCont
/// </summary>
public class rptDoanhSoTheoCont : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel6;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell11;
    private ReportFooterBand ReportFooter;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell28;
    private XRTableCell nv_lappo;
    private XRTableCell xrTableCell30;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptDoanhSoTheoCont()
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
        string resourceFileName = "rptDoanhSoTheoCont.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.nv_lappo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 28.125F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001192093F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1166F, 28.125F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell29,
            this.nv_lappo,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22});
        this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.xrTableRow2.StylePriority.UseFont = false;
        this.xrTableRow2.StylePriority.UsePadding = false;
        this.xrTableRow2.Weight = 0.599999967447914D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thang")});
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "[thang]";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell12.Weight = 0.63541675567626932D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngay_motokhai", "{0:dd/MM/yyyy}")});
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Weight = 0.90624996185302753D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "[ma_dtkd]";
        this.xrTableCell14.Weight = 1.0895829772949217D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "quocgia")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "[quocgia]";
        this.xrTableCell15.Weight = 1.2493760059281773D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_inv")});
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "[so_inv]";
        this.xrTableCell16.Weight = 1.0262798551601144D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nhanvien")});
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "[nhanvien]";
        this.xrTableCell17.Weight = 1.0759553114487674D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ncu")});
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "[ncu]";
        this.xrTableCell18.Weight = 1.1476923722395556D;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PO")});
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.Text = "[PO]";
        this.xrTableCell29.Weight = 1.1272863963121944D;
        // 
        // nv_lappo
        // 
        this.nv_lappo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nv_lappo")});
        this.nv_lappo.Name = "nv_lappo";
        this.nv_lappo.Text = "nv_lappo";
        this.nv_lappo.Weight = 1.2127262955556359D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgross", "{0:#,#0.00}")});
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell19.Weight = 0.92213496432270414D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont20", "{0:#,#0}")});
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell20.Weight = 0.43649503780525611D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40hc", "{0:#,#0}")});
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell21.Weight = 0.58526817958285127D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40", "{0:#,#0}")});
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell22.Weight = 0.48070242488056769D;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 49F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 50F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel6});
        this.ReportHeader.HeightF = 167.1669F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel9
        // 
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(698.9583F, 110.75F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(121.0418F, 23F);
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "Núi Thành, ngày:";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(820.0001F, 110.75F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(151.2501F, 23F);
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "[today]";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MM/yyyy}")});
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(661.6667F, 144.1667F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(151.2501F, 23F);
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MM/yyyy}")});
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(412.2916F, 144.1667F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(151.2501F, 23F);
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(314.1664F, 144.1669F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(98.12514F, 23F);
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Từ ngày:";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 74.20836F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1166F, 36.54166F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "BÁO CÁO DOANH SỐ - SỐ LƯỢNG CONT";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.20834F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1166F, 23F);
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 28.20832F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1166F, 23F);
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1166F, 28.20833F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel6
        // 
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(563.5417F, 144.1667F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(98.12514F, 23F);
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Đến ngày:";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.PageHeader.HeightF = 46.875F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1166F, 46.875F);
        this.xrTable1.StylePriority.UseBorders = false;
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
            this.xrTableCell7,
            this.xrTableCell6,
            this.xrTableCell5,
            this.xrTableCell28,
            this.xrTableCell30,
            this.xrTableCell10,
            this.xrTableCell9,
            this.xrTableCell8,
            this.xrTableCell11});
        this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.StylePriority.UseFont = false;
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "THÁNG";
        this.xrTableCell1.Weight = 0.62285572052001936D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "NGÀY MTK";
        this.xrTableCell2.Weight = 0.88833324432373051D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "MÃ KH";
        this.xrTableCell3.Weight = 1.0680418395996094D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "QUỐC GIA";
        this.xrTableCell4.Weight = 1.2246753692626951D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "SỐ INVOICE";
        this.xrTableCell7.Weight = 1.005989990234375D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "NGƯỜI LẬP";
        this.xrTableCell6.Weight = 1.0546846771240235D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "NHÀ CUNG ỨNG";
        this.xrTableCell5.Weight = 1.1250026416778562D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Text = "PO";
        this.xrTableCell28.Weight = 1.1049999856948851D;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.Text = "NV lập PO";
        this.xrTableCell30.Weight = 1.1887517404556269D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "TRỊ GIÁ";
        this.xrTableCell10.Weight = 0.90390439987182614D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "CONT 20";
        this.xrTableCell9.Weight = 0.42786560058593781D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "CONT 40HC";
        this.xrTableCell8.Weight = 0.57369754791259742D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "CONT 40";
        this.xrTableCell11.Weight = 0.4711972427368159D;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.ReportFooter.HeightF = 25F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001192093F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1166F, 25F);
        this.xrTable3.StylePriority.UseBorders = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27});
        this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.StylePriority.UseFont = false;
        this.xrTableRow3.StylePriority.UseTextAlignment = false;
        this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "Tổng cộng:";
        this.xrTableCell23.Weight = 2.602803413982012D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgross", "{0:#,#0.00}")});
        this.xrTableCell24.Name = "xrTableCell24";
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell24.Summary = xrSummary1;
        this.xrTableCell24.Weight = 0.25343074926095949D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont20", "{0:#,#0}")});
        this.xrTableCell25.Name = "xrTableCell25";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell25.Summary = xrSummary2;
        this.xrTableCell25.Weight = 0.11996245350045172D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40hc", "{0:#,#0}")});
        this.xrTableCell26.Name = "xrTableCell26";
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell26.Summary = xrSummary3;
        this.xrTableCell26.Weight = 0.16084973054013174D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40", "{0:#,#0}")});
        this.xrTableCell27.Name = "xrTableCell27";
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell27.Summary = xrSummary4;
        this.xrTableCell27.Weight = 0.13211184671469253D;
        // 
        // rptDoanhSoTheoCont
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
        this.Margins = new System.Drawing.Printing.Margins(3, 0, 49, 50);
        this.PageHeight = 827;
        this.PageWidth = 1169;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4Rotated;
        this.Version = "13.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
