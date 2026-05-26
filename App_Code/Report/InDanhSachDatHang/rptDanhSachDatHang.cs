using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptDanhSachDatHang
/// </summary>
public class rptDanhSachDatHang : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel14;
    private XRLabel xrLabel15;
    private XRLabel xrLabel12;
    private XRLabel xrLabel13;
    private XRLabel xrLabel11;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
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
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRLabel xrLabel23;
    private XRLabel xrLabel21;
    private XRLabel xrLabel18;
    private XRLabel xrLabel22;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private XRLabel xrLabel16;
    private XRLabel xrLabel17;
    private XRLabel xrLabel38;
    private XRLabel xrLabel36;
    private XRLabel xrLabel37;
    private XRLabel xrLabel34;
    private XRLabel xrLabel35;
    private XRLabel xrLabel33;
    private XRLabel xrLabel30;
    private XRLabel xrLabel28;
    private XRLabel xrLabel29;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private XRLabel xrLabel25;
    private XRLabel xrLabel24;
    private XRLabel xrLabel41;
    private XRLabel xrLabel42;
    private XRLabel xrLabel39;
    private XRLabel xrLabel40;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel32;
    private XRLabel xrLabel31;
    private XRRichText huongdanlamhangchung;
    private XRLabel xrLabel43;
    private XRRichText ghichu;
    private XRLabel customer_order_no;
    private XRLabel xrLabel45;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptDanhSachDatHang()
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
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
        this.ghichu = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.huongdanlamhangchung = new DevExpress.XtraReports.UI.XRRichText();
        this.huongdanlamhang = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.customer_order_no = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ghichu)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.huongdanlamhangchung)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.huongdanlamhang)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 55.20834F;
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
        this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(929F, 55.20834F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UsePadding = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrTableCell13.Summary = xrSummary1;
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell13.Weight = 0.40624999999999978D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "[ma_sanpham]";
        this.xrTableCell14.Weight = 0.73958335876464842D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pic")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "[pic]";
        this.xrTableCell15.Weight = 0.74875015258789079D;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "pic", "~/images/products/fullsize/{0}")});
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(79.5F, 54F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham_khach")});
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "[ma_sanpham_khach]";
        this.xrTableCell16.Weight = 0.76958351135253911D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tiengviet")});
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "[mota_tiengviet]";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell17.Weight = 1.1670855848524309D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_edi")});
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "[ma_edi]";
        this.xrTableCell18.Weight = 0.44393480004204644D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "v2", "{0:#,##0.000}")});
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "xrTableCell19";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell19.Weight = 0.48246379475534706D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_dathang", "{0:#,##0}")});
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell20.Weight = 0.49968498181032495D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "gianhap", "{0:#,#0.##}")});
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell21.Weight = 0.63333337502219633D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thanhtien", "{0:#,#0.##}")});
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell22.Weight = 1.1875302436950779D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dginner")});
        this.xrTableCell23.Multiline = true;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "[dginner]";
        this.xrTableCell23.Weight = 0.7993985246789117D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dgouter")});
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "[dgouter]";
        this.xrTableCell24.Weight = 0.71278377303413931D;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 21F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 27.29168F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(3.973643E-05F, 217.3333F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(928.9999F, 51.04167F);
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
            this.xrTableCell7,
            this.xrTableCell5,
            this.xrTableCell4,
            this.xrTableCell9,
            this.xrTableCell8,
            this.xrTableCell6,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "STT";
        this.xrTableCell1.Weight = 0.40624999999999978D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "Mã số";
        this.xrTableCell2.Weight = 0.73958335876464842D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Hình ảnh";
        this.xrTableCell3.Weight = 0.74875015258789079D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "Mã khách hàng";
        this.xrTableCell7.Weight = 0.76958351135253911D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "Mô tả";
        this.xrTableCell5.Weight = 1.1670855848524309D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "ĐVT";
        this.xrTableCell4.Weight = 0.44393480004204644D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "Cbm";
        this.xrTableCell9.Weight = 0.48246379475534706D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "SL";
        this.xrTableCell8.Weight = 0.49968499556876711D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "Giá";
        this.xrTableCell6.Weight = 0.63333280446800477D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "Thành tiền";
        this.xrTableCell10.Weight = 1.1875307241544264D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Multiline = true;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "Đóng gói\r\nInner";
        this.xrTableCell11.Weight = 0.79939921495076072D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "Đóng gói outer";
        this.xrTableCell12.Weight = 0.712783210165707D;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.customer_order_no,
            this.xrLabel45,
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel6,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel11,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel15,
            this.xrLabel14,
            this.xrTable1});
        this.ReportHeader.HeightF = 268.375F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(928.9999F, 32.375F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 32.37502F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(928.9999F, 23F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 55.37497F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(928.9999F, 23F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Arial", 17F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 78.37499F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(929F, 46.95834F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "ĐƠN ĐẶT HÀNG";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(43.93359F, 125.3333F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(100.0416F, 23F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Số chứng từ:";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sochungtu")});
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(143.9752F, 125.3333F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(145.875F, 23F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "[sochungtu]";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_po")});
        this.xrLabel8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(579.7499F, 125.3333F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(145.875F, 23F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "[so_po]";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(439.0833F, 125.3333F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(140.6666F, 22.99998F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Theo P/O số:";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(43.93384F, 148.3333F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(100.0416F, 23F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "Ngày lập:";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel9
        // 
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaylap", "{0:dd/MM/yyyy}")});
        this.xrLabel9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(143.9754F, 148.3333F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(145.875F, 23F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "xrLabel9";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_dtkd", "Công ty AnCơ kính đề nghị Quý Công ty/Đơn vị: {0}")});
        this.xrLabel11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(43.93362F, 171.3333F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(885.0662F, 23F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "xrLabel11";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel13
        // 
        this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dienthoai")});
        this.xrLabel13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(143.9752F, 194.3333F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(145.875F, 23F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "[dienthoai]";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(43.93359F, 194.3333F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(100.0416F, 23F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "Điện thoại:";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(379.6668F, 194.3333F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(100.0416F, 23F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "Fax:";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "fax")});
        this.xrLabel14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(479.7083F, 194.3333F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(145.875F, 23F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "[fax]";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel43,
            this.ghichu,
            this.xrLabel32,
            this.xrLabel31,
            this.huongdanlamhangchung,
            this.huongdanlamhang,
            this.xrLabel41,
            this.xrLabel42,
            this.xrLabel39,
            this.xrLabel40,
            this.xrLabel38,
            this.xrLabel36,
            this.xrLabel37,
            this.xrLabel34,
            this.xrLabel35,
            this.xrLabel33,
            this.xrLabel30,
            this.xrLabel28,
            this.xrLabel29,
            this.xrLabel26,
            this.xrLabel27,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel21,
            this.xrLabel18,
            this.xrLabel22,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel16,
            this.xrLabel17});
        this.ReportFooter.HeightF = 499.7083F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrLabel43
        // 
        this.xrLabel43.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(43.93384F, 138F);
        this.xrLabel43.Name = "xrLabel43";
        this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel43.SizeF = new System.Drawing.SizeF(160.9546F, 23F);
        this.xrLabel43.StylePriority.UseFont = false;
        this.xrLabel43.StylePriority.UseTextAlignment = false;
        this.xrLabel43.Text = "Ghi chú";
        this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // ghichu
        // 
        this.ghichu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota")});
        this.ghichu.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.ghichu.LocationFloat = new DevExpress.Utils.PointFloat(204.8884F, 138F);
        this.ghichu.Name = "ghichu";
        this.ghichu.SizeF = new System.Drawing.SizeF(560.5779F, 46.00005F);
        // 
        // xrLabel32
        // 
        this.xrLabel32.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(43.93355F, 297.0001F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(160.9546F, 23F);
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        this.xrLabel32.Text = "2. Các hướng dẫn khác:";
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel31
        // 
        this.xrLabel31.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(43.93355F, 253.0001F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(160.9546F, 23F);
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "1. Đặc điểm hàng hóa:";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // huongdanlamhangchung
        // 
        this.huongdanlamhangchung.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "huongdanlamhangchung")});
        this.huongdanlamhangchung.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.huongdanlamhangchung.LocationFloat = new DevExpress.Utils.PointFloat(204.8882F, 297.0001F);
        this.huongdanlamhangchung.Name = "huongdanlamhangchung";
        this.huongdanlamhangchung.SizeF = new System.Drawing.SizeF(560.5779F, 46.00006F);
        // 
        // huongdanlamhang
        // 
        this.huongdanlamhang.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "huongdanlamhang")});
        this.huongdanlamhang.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.huongdanlamhang.LocationFloat = new DevExpress.Utils.PointFloat(204.8882F, 251.0001F);
        this.huongdanlamhang.Name = "huongdanlamhang";
        this.huongdanlamhang.SizeF = new System.Drawing.SizeF(560.5779F, 46.00005F);
        // 
        // xrLabel41
        // 
        this.xrLabel41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tru", "(-) {0}")});
        this.xrLabel41.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(43.93365F, 68.99999F);
        this.xrLabel41.Name = "xrLabel41";
        this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel41.SizeF = new System.Drawing.SizeF(513.7834F, 23F);
        this.xrLabel41.StylePriority.UseFont = false;
        this.xrLabel41.StylePriority.UseTextAlignment = false;
        this.xrLabel41.Text = "(-)";
        this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel42
        // 
        this.xrLabel42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "giatrigiam", "{0:#,##0.####}")});
        this.xrLabel42.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(637.0419F, 68.99999F);
        this.xrLabel42.Name = "xrLabel42";
        this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel42.SizeF = new System.Drawing.SizeF(128.4243F, 23F);
        this.xrLabel42.StylePriority.UseFont = false;
        this.xrLabel42.StylePriority.UseTextAlignment = false;
        this.xrLabel42.Text = "xrLabel42";
        this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel39
        // 
        this.xrLabel39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_cong", "(+) {0}")});
        this.xrLabel39.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(43.93361F, 46.00003F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(513.7834F, 23F);
        this.xrLabel39.StylePriority.UseFont = false;
        this.xrLabel39.StylePriority.UseTextAlignment = false;
        this.xrLabel39.Text = "(+)";
        this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel40
        // 
        this.xrLabel40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "giatritang", "{0:#,##0.####}")});
        this.xrLabel40.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(637.0419F, 46F);
        this.xrLabel40.Name = "xrLabel40";
        this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel40.SizeF = new System.Drawing.SizeF(128.4243F, 23F);
        this.xrLabel40.StylePriority.UseFont = false;
        this.xrLabel40.StylePriority.UseTextAlignment = false;
        this.xrLabel40.Text = "xrLabel40";
        this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel38
        // 
        this.xrLabel38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nguoitao")});
        this.xrLabel38.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(43.93361F, 476.7083F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(245.9166F, 23F);
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "[nguoitao]";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(528.3672F, 368.0836F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(237.0989F, 23F);
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = "Nhà cung ứng";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel37
        // 
        this.xrLabel37.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(528.3674F, 391.0835F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(237.0989F, 23F);
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "Ký và ghi rõ họ tên";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel34
        // 
        this.xrLabel34.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(43.93361F, 391.0835F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(245.9166F, 23F);
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = "Ký và ghi rõ họ tên";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel35
        // 
        this.xrLabel35.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(43.93361F, 368.0836F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(245.9166F, 23F);
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "Người lập phiếu";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel33
        // 
        this.xrLabel33.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(43.93365F, 343.0002F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(721.5325F, 23F);
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = "* Khi cơ sở nhận được đơn đặt hàng này vui lòng ký xác nhận và gửi lại.";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(43.93361F, 230.0002F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(160.9546F, 23F);
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.Text = "Hướng dẫn làm hàng:";
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel28
        // 
        this.xrLabel28.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(43.93361F, 207.0002F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(160.9546F, 23F);
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "Địa điểm xuất hàng:";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel29
        // 
        this.xrLabel29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachigiaohang")});
        this.xrLabel29.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(204.8882F, 207.0002F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(560.578F, 22.99998F);
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = "[diachigiaohang]";
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel26
        // 
        this.xrLabel26.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(43.93361F, 184F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(160.9546F, 22.99999F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "Ngày xong hàng:";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel27
        // 
        this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "hangiaohang_po", "{0:dd/MM/yyyy}")});
        this.xrLabel27.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(204.8882F, 184F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(560.578F, 22.99998F);
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "xrLabel27";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel25
        // 
        this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "money")});
        this.xrLabel25.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(204.8882F, 115F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(560.578F, 22.99999F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.Text = "[money]";
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel24
        // 
        this.xrLabel24.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(43.93361F, 115F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(160.9546F, 22.99999F);
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "Viết bằng chữ:";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel23
        // 
        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thanhtien")});
        this.xrLabel23.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(637.0418F, 0F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(128.4244F, 23F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:#,#0.##}";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel23.Summary = xrSummary2;
        this.xrLabel23.Text = "xrLabel23";
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel21
        // 
        this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "discount", "{0:#,#0.##}")});
        this.xrLabel21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(637.0419F, 23.00002F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(128.4243F, 23F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.Text = "xrLabel21";
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel18
        // 
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_dathang")});
        this.xrLabel18.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(514.5123F, 0F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(54.03809F, 23F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:#,##0}";
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel18.Summary = xrSummary3;
        this.xrLabel18.Text = "[sl_dathang]";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel22
        // 
        this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtiendatru", "{0:#,#0.##}")});
        this.xrLabel22.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(637.042F, 92.00001F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(128.4242F, 23F);
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.Text = "xrLabel22";
        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel20
        // 
        this.xrLabel20.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(43.93359F, 92.00001F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(160.9546F, 23F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = "Tổng cộng đã trừ:";
        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel19
        // 
        this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "discountdecimal", "Discount ({0}%):")});
        this.xrLabel19.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(43.93358F, 23.00002F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(245.9166F, 23F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "xrLabel19";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(43.93358F, 0F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(160.9546F, 23F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "Tổng cộng:";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel17
        // 
        this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "v2", "{0:#,##0.000}")});
        this.xrLabel17.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(462.3367F, 0F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(52.1756F, 23F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        xrSummary4.FormatString = "{0:#,##0.000}";
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel17.Summary = xrSummary4;
        this.xrLabel17.Text = "xrLabel17";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // customer_order_no
        // 
        this.customer_order_no.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "customer_order_no")});
        this.customer_order_no.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.customer_order_no.LocationFloat = new DevExpress.Utils.PointFloat(579.7499F, 148.3333F);
        this.customer_order_no.Name = "customer_order_no";
        this.customer_order_no.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.customer_order_no.SizeF = new System.Drawing.SizeF(145.8749F, 23F);
        this.customer_order_no.StylePriority.UseFont = false;
        this.customer_order_no.StylePriority.UseTextAlignment = false;
        this.customer_order_no.Text = "[sochungtu]";
        this.customer_order_no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel45
        // 
        this.xrLabel45.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(439.0833F, 148.3333F);
        this.xrLabel45.Name = "xrLabel45";
        this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel45.SizeF = new System.Drawing.SizeF(140.6666F, 23F);
        this.xrLabel45.StylePriority.UseFont = false;
        this.xrLabel45.StylePriority.UseTextAlignment = false;
        this.xrLabel45.Text = "Customer Order No:";
        this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // rptDanhSachDatHang
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 21, 27);
        this.PageHeight = 1268;
        this.PageWidth = 929;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4Extra;
        this.Version = "17.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ghichu)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.huongdanlamhangchung)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.huongdanlamhang)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private XRRichText huongdanlamhang;


}
