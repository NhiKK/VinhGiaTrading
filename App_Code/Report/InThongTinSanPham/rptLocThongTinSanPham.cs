using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptLocThongTinSanPham
/// </summary>
public class rptLocThongTinSanPham : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel1;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell16;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell55;
    private XRPictureBox xrPictureBox1;
    private XRTableCell con20;
    private XRTableCell con40;
    private XRTableCell con40hc;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptLocThongTinSanPham()
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
            string resourceFileName = "XtraReport1.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.con20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.con40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.con40hc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 47.29169F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1654F, 47.29169F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell56,
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell54,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44,
            this.con20,
            this.con40,
            this.con40hc,
            this.xrTableCell45,
            this.xrTableCell46});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.Weight = 0.272177396025054D;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "pic", "~/images/products/fullsize/{0}")});
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(1.999999F, 2.000023F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(69.18559F, 43.29167F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "[ma_sanpham]";
            this.xrTableCell24.Weight = 0.30645798487175024D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tiengviet")});
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Text = "[mota_tiengviet]";
            this.xrTableCell25.Weight = 0.33971134359526095D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tienganh")});
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Text = "[mota_tienganh]";
            this.xrTableCell26.Weight = 0.33971130119730175D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt")});
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Text = "[dvt]";
            this.xrTableCell27.Weight = 0.161806638899537D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l")});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseTextAlignment = false;
            this.xrTableCell28.Text = "[l]";
            this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell28.Weight = 0.16180626812007354D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w")});
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = "[w]";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell29.Weight = 0.16180650511005629D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h")});
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "[h]";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell30.Weight = 0.16180674165919495D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "trongluong")});
            this.xrTableCell31.Multiline = true;
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "[trongluong]";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell31.Weight = 0.23129018601511053D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dientich")});
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.Text = "[dientich]";
            this.xrTableCell49.Weight = 0.23129029369552878D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "chungloai")});
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.Text = "[chungloai]";
            this.xrTableCell50.Weight = 0.27871895315459583D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota")});
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.Text = "[mota]";
            this.xrTableCell54.Weight = 0.2513489686976601D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "giafob")});
            this.xrTableCell32.Multiline = true;
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.Text = "[giafob]";
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell32.Weight = 0.204503034239023D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "giamua")});
            this.xrTableCell33.Multiline = true;
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            this.xrTableCell33.Text = "[giamua]";
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell33.Weight = 0.19126463586633657D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_donggoi")});
            this.xrTableCell34.Multiline = true;
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.Text = "[ten_donggoi]";
            this.xrTableCell34.Weight = 0.15778043216161125D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_inner")});
            this.xrTableCell35.Multiline = true;
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.Text = "[sl_inner]";
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell35.Weight = 0.1577809002985508D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt_inner")});
            this.xrTableCell36.Multiline = true;
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Text = "[dvt_inner]";
            this.xrTableCell36.Weight = 0.15778045775419419D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l1")});
            this.xrTableCell37.Multiline = true;
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.Text = "[sl_outer]";
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell37.Weight = 0.15778067741407142D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w1")});
            this.xrTableCell38.Multiline = true;
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Text = "[dvt_outer]";
            this.xrTableCell38.Weight = 0.15778064907351808D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h1")});
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            this.xrTableCell39.Text = "[l1]";
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell39.Weight = 0.15778068363866332D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_outer")});
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            this.xrTableCell40.Text = "[w1]";
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell40.Weight = 0.15778068249838934D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt_outer")});
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.Text = "[h1]";
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell41.Weight = 0.15778066657557954D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l2")});
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.Text = "[l2]";
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell42.Weight = 0.1577806815174303D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w2")});
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.Text = "[w2]";
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell43.Weight = 0.15778068032482984D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h2")});
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.Text = "[h2]";
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell44.Weight = 0.15778019379739117D;
            // 
            // con20
            // 
            this.con20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "con20")});
            this.con20.Name = "con20";
            this.con20.StylePriority.UseTextAlignment = false;
            this.con20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.con20.Weight = 0.15778066632758836D;
            // 
            // con40
            // 
            this.con40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "con40")});
            this.con40.Name = "con40";
            this.con40.StylePriority.UseTextAlignment = false;
            this.con40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.con40.Weight = 0.15778066632758836D;
            // 
            // con40hc
            // 
            this.con40hc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "con40hc")});
            this.con40hc.Name = "con40hc";
            this.con40hc.StylePriority.UseTextAlignment = false;
            this.con40hc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.con40hc.Weight = 0.15778066632758836D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nhacungung")});
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Text = "[nhacungung]";
            this.xrTableCell45.Weight = 0.38275749351863081D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cangbien")});
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.Text = "[cangbien]";
            this.xrTableCell46.Weight = 0.2658443832688227D;
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
            this.BottomMargin.HeightF = 53F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
            this.ReportHeader.HeightF = 48F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1654F, 48F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "THÔNG TIN SẢN PHẨM";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 53.93231F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1654F, 53.9323F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell55,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell6,
            this.xrTableCell5,
            this.xrTableCell8,
            this.xrTableCell7,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell52,
            this.xrTableCell10,
            this.xrTableCell12,
            this.xrTableCell11,
            this.xrTableCell9,
            this.xrTableCell15,
            this.xrTableCell14,
            this.xrTableCell13,
            this.xrTableCell20,
            this.xrTableCell19,
            this.xrTableCell21,
            this.xrTableCell23,
            this.xrTableCell22,
            this.xrTableCell18,
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell17,
            this.xrTableCell16});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseBackColor = false;
            this.xrTableRow1.StylePriority.UseForeColor = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.Text = "Hình ảnh";
            this.xrTableCell55.Weight = 0.27217741769469167D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Mã sản phẩm";
            this.xrTableCell1.Weight = 0.30645800909299908D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Tiếng Việt";
            this.xrTableCell2.Weight = 0.33971136197658336D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Tiếng Anh";
            this.xrTableCell3.Weight = 0.33971137879494323D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "ĐVT";
            this.xrTableCell4.Weight = 0.16180650478760347D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "L";
            this.xrTableCell6.Weight = 0.16180649142030915D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "W";
            this.xrTableCell5.Weight = 0.16180649126356603D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "H";
            this.xrTableCell8.Weight = 0.16180647678099192D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "Trọng\r\nLượng";
            this.xrTableCell7.Weight = 0.23129030667971817D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Multiline = true;
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.Text = "Diện\r\nTích";
            this.xrTableCell47.Weight = 0.23129032342214639D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Multiline = true;
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.Text = "Chủng\r\nloại";
            this.xrTableCell48.Weight = 0.2787192078211348D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Multiline = true;
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.Text = "Ghi chú";
            this.xrTableCell52.Weight = 0.25134899728986465D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Multiline = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "Giá\r\nFOB";
            this.xrTableCell10.Weight = 0.20450322921977551D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Multiline = true;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "Giá\r\nMua";
            this.xrTableCell12.Weight = 0.19126451671984462D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "Đóng\r\ngói";
            this.xrTableCell11.Weight = 0.15778062156223954D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "SL\r\nInner";
            this.xrTableCell9.Weight = 0.15778062940051035D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Multiline = true;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "ĐVT\r\nInner";
            this.xrTableCell15.Weight = 0.15778063145125348D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Multiline = true;
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Text = "L1";
            this.xrTableCell14.Weight = 0.15778062925740183D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "W1";
            this.xrTableCell13.Weight = 0.15778063005059528D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Text = "H1";
            this.xrTableCell20.Weight = 0.15778062641340129D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Multiline = true;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Text = "SL\r\nOuter";
            this.xrTableCell19.Weight = 0.15778068194136413D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Multiline = true;
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "ĐVT\r\nOuter";
            this.xrTableCell21.Weight = 0.15778066681758068D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Text = "L2";
            this.xrTableCell23.Weight = 0.15778066675047692D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "W2";
            this.xrTableCell22.Weight = 0.15778066595737189D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Text = "H2";
            this.xrTableCell18.Weight = 0.15778055093300164D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.Text = "SL Cont 20";
            this.xrTableCell57.Weight = 0.15778052139985938D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.Text = "SL Cont 40";
            this.xrTableCell58.Weight = 0.15778053616642895D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.Text = "SL Cont 40hc";
            this.xrTableCell59.Weight = 0.15778068383212687D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "Nhà Cung Ứng";
            this.xrTableCell17.Weight = 0.38275704645454889D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "Cảng Biển";
            this.xrTableCell16.Weight = 0.263051905931679D;
            // 
            // rptLocThongTinSanPham
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 49, 53);
            this.PageHeight = 1169;
            this.PageWidth = 1654;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}