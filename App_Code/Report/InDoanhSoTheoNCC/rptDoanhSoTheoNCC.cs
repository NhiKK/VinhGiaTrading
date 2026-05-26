using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptDoanhSoTheoNCC
/// </summary>
public class rptDoanhSoTheoNCC : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell thang;
    private XRTableCell ngaymtk;
    private XRTableCell makh;
    private XRTableCell soinv;
    private XRTableCell soPO;
    private XRTableCell trigia;
    private XRTableCell tongtienpx;
    private XRTableCell tongtienncc_khac;
    private XRTableCell phicong_inv;
    private XRTableCell phitru_inv;
    private XRTableCell cont20;
    private XRTableCell cont40hc;
    private XRTableCell cont40;
    private XRLabel denngay;
    private XRLabel xrLabel8;
    private XRLabel tungay;
    private XRLabel xrLabel6;
    private XRLabel today;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
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
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRLabel ma_dtkd;
    private XRLabel xrLabel7;
    private ReportFooterBand ReportFooter;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell4;
    private XRTableCell sum_trigia;
    private XRTableCell sum_tongtienpx;
    private XRTableCell sum_tongtienncc_khac;
    private XRTableCell sum_phicong_inv;
    private XRTableCell sum_phitru_inv;
    private XRTableCell sum_cont20;
    private XRTableCell sum_cont40hc;
    private XRTableCell sum_cont40;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell1;
    private XRTableCell sumtc_trigia;
    private XRTableCell sumtc_tongtienpx;
    private XRTableCell sumtc_tongtienncc_khac;
    private XRTableCell sumtc_phicong_inv;
    private XRTableCell sumtc_phitru_inv;
    private XRTableCell sumtc_cont20;
    private XRTableCell sumtc_cont40hc;
    private XRTableCell sumtc_cont40;
    private XRTableCell phicong_po;
    private XRTableCell phitru_po;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell sumtc_phicong_po;
    private XRTableCell sumtc_phitru_po;
    private XRTableCell sum_phicong_po;
    private XRTableCell sum_phitru_po;
    private XRTableCell discount;
    private XRTableCell totaldiscount;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell sumtc_discount;
    private XRTableCell sumtc_totaldiscount;
    private XRTableCell sum_discount;
    private XRTableCell sum_totaldiscount;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptDoanhSoTheoNCC()
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
        string resourceFileName = "rptDoanhSoTheoNCC.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary12 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary13 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary14 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary15 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary16 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary17 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary18 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary19 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary20 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary21 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary22 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary23 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary24 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary25 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary26 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary27 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.thang = new DevExpress.XtraReports.UI.XRTableCell();
        this.ngaymtk = new DevExpress.XtraReports.UI.XRTableCell();
        this.makh = new DevExpress.XtraReports.UI.XRTableCell();
        this.soinv = new DevExpress.XtraReports.UI.XRTableCell();
        this.soPO = new DevExpress.XtraReports.UI.XRTableCell();
        this.trigia = new DevExpress.XtraReports.UI.XRTableCell();
        this.discount = new DevExpress.XtraReports.UI.XRTableCell();
        this.totaldiscount = new DevExpress.XtraReports.UI.XRTableCell();
        this.tongtienpx = new DevExpress.XtraReports.UI.XRTableCell();
        this.tongtienncc_khac = new DevExpress.XtraReports.UI.XRTableCell();
        this.phicong_inv = new DevExpress.XtraReports.UI.XRTableCell();
        this.phitru_inv = new DevExpress.XtraReports.UI.XRTableCell();
        this.phicong_po = new DevExpress.XtraReports.UI.XRTableCell();
        this.phitru_po = new DevExpress.XtraReports.UI.XRTableCell();
        this.cont20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cont40hc = new DevExpress.XtraReports.UI.XRTableCell();
        this.cont40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
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
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.ma_dtkd = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_trigia = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_discount = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_totaldiscount = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_tongtienpx = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_tongtienncc_khac = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_phicong_inv = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_phitru_inv = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_phicong_po = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_phitru_po = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_cont20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_cont40hc = new DevExpress.XtraReports.UI.XRTableCell();
        this.sumtc_cont40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_trigia = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_discount = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_totaldiscount = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_tongtienpx = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_tongtienncc_khac = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_phicong_inv = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_phitru_inv = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_phicong_po = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_phitru_po = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_cont20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_cont40hc = new DevExpress.XtraReports.UI.XRTableCell();
        this.sum_cont40 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.HeightF = 26.04167F;
        this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1558.542F, 25F);
        this.xrTable1.StylePriority.UseBorderColor = false;
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.thang,
            this.ngaymtk,
            this.makh,
            this.soinv,
            this.soPO,
            this.trigia,
            this.discount,
            this.totaldiscount,
            this.tongtienpx,
            this.tongtienncc_khac,
            this.phicong_inv,
            this.phitru_inv,
            this.phicong_po,
            this.phitru_po,
            this.cont20,
            this.cont40hc,
            this.cont40});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // thang
        // 
        this.thang.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thang")});
        this.thang.Name = "thang";
        this.thang.StylePriority.UseTextAlignment = false;
        this.thang.Text = "Tháng";
        this.thang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.thang.Weight = 0.13146548367567562D;
        // 
        // ngaymtk
        // 
        this.ngaymtk.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaymtk", "{0:dd/MM/yyyy}")});
        this.ngaymtk.Name = "ngaymtk";
        this.ngaymtk.StylePriority.UseTextAlignment = false;
        this.ngaymtk.Text = "Ngày MTK";
        this.ngaymtk.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.ngaymtk.Weight = 0.18803885224032149D;
        // 
        // makh
        // 
        this.makh.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "makh")});
        this.makh.Name = "makh";
        this.makh.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.makh.StylePriority.UsePadding = false;
        this.makh.Text = "Mã KH";
        this.makh.Weight = 0.31950455664453814D;
        // 
        // soinv
        // 
        this.soinv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soinv")});
        this.soinv.Name = "soinv";
        this.soinv.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.soinv.StylePriority.UsePadding = false;
        this.soinv.Text = "Số Invoice";
        this.soinv.Weight = 0.28987063652839251D;
        // 
        // soPO
        // 
        this.soPO.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soPO")});
        this.soPO.Name = "soPO";
        this.soPO.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.soPO.StylePriority.UsePadding = false;
        this.soPO.Text = "PO";
        this.soPO.Weight = 0.32058191911785949D;
        // 
        // trigia
        // 
        this.trigia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "trigia", "{0:#,#.00}")});
        this.trigia.Name = "trigia";
        this.trigia.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.trigia.StylePriority.UsePadding = false;
        this.trigia.StylePriority.UseTextAlignment = false;
        this.trigia.Text = "trigia";
        this.trigia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.trigia.Weight = 0.25862100291620388D;
        // 
        // discount
        // 
        this.discount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "discount", "{0:#,#}")});
        this.discount.Name = "discount";
        this.discount.StylePriority.UseTextAlignment = false;
        this.discount.Text = "discount";
        this.discount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.discount.Weight = 0.18103416584051035D;
        // 
        // totaldiscount
        // 
        this.totaldiscount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totaldiscount", "{0:#,#.00}")});
        this.totaldiscount.Name = "totaldiscount";
        this.totaldiscount.StylePriority.UseTextAlignment = false;
        this.totaldiscount.Text = "totaldiscount";
        this.totaldiscount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.totaldiscount.Weight = 0.25862068843614611D;
        // 
        // tongtienpx
        // 
        this.tongtienpx.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienpx", "{0:#,#.00}")});
        this.tongtienpx.Name = "tongtienpx";
        this.tongtienpx.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.tongtienpx.StylePriority.UsePadding = false;
        this.tongtienpx.StylePriority.UseTextAlignment = false;
        this.tongtienpx.Text = "tongtienpx";
        this.tongtienpx.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.tongtienpx.Weight = 0.31034474599660467D;
        // 
        // tongtienncc_khac
        // 
        this.tongtienncc_khac.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienncc_khac", "{0:#,#.00}")});
        this.tongtienncc_khac.Name = "tongtienncc_khac";
        this.tongtienncc_khac.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.tongtienncc_khac.StylePriority.UsePadding = false;
        this.tongtienncc_khac.StylePriority.UseTextAlignment = false;
        this.tongtienncc_khac.Text = "tongtienncc_khac";
        this.tongtienncc_khac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.tongtienncc_khac.Weight = 0.3243533484292625D;
        // 
        // phicong_inv
        // 
        this.phicong_inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phicong_inv", "{0:#,#.00}")});
        this.phicong_inv.Name = "phicong_inv";
        this.phicong_inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.phicong_inv.StylePriority.UsePadding = false;
        this.phicong_inv.StylePriority.UseTextAlignment = false;
        xrSummary1.IgnoreNullValues = true;
        this.phicong_inv.Summary = xrSummary1;
        this.phicong_inv.Text = "phicong_inv";
        this.phicong_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.phicong_inv.Weight = 0.20689703006375D;
        // 
        // phitru_inv
        // 
        this.phitru_inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phitru_inv", "{0:#,#.00}")});
        this.phitru_inv.Name = "phitru_inv";
        this.phitru_inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.phitru_inv.StylePriority.UsePadding = false;
        this.phitru_inv.StylePriority.UseTextAlignment = false;
        xrSummary2.IgnoreNullValues = true;
        this.phitru_inv.Summary = xrSummary2;
        this.phitru_inv.Text = "phitru_inv";
        this.phitru_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.phitru_inv.Weight = 0.20689560673891255D;
        // 
        // phicong_po
        // 
        this.phicong_po.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phicong_po", "{0:#,#.00}")});
        this.phicong_po.Name = "phicong_po";
        this.phicong_po.Text = "phicong_po";
        this.phicong_po.Weight = 0.20689686847923527D;
        // 
        // phitru_po
        // 
        this.phitru_po.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phitru_po", "{0:#,#.00}")});
        this.phitru_po.Name = "phitru_po";
        this.phitru_po.Text = "phitru_po";
        this.phitru_po.Weight = 0.20689686847923544D;
        // 
        // cont20
        // 
        this.cont20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont20", "{0:#,#}")});
        this.cont20.Name = "cont20";
        this.cont20.NullValueText = "0";
        this.cont20.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.cont20.StylePriority.UsePadding = false;
        this.cont20.StylePriority.UseTextAlignment = false;
        this.cont20.Text = "CONT 20";
        this.cont20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cont20.Weight = 0.20689624365105314D;
        // 
        // cont40hc
        // 
        this.cont40hc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40hc", "{0:#,#}")});
        this.cont40hc.Name = "cont40hc";
        this.cont40hc.NullValueText = "0";
        this.cont40hc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.cont40hc.StylePriority.UsePadding = false;
        this.cont40hc.StylePriority.UseTextAlignment = false;
        xrSummary3.IgnoreNullValues = true;
        this.cont40hc.Summary = xrSummary3;
        this.cont40hc.Text = "cont40hc";
        this.cont40hc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cont40hc.Weight = 0.20689655094450907D;
        // 
        // cont40
        // 
        this.cont40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40", "{0:#,#}")});
        this.cont40.Name = "cont40";
        this.cont40.NullValueText = "0";
        this.cont40.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.cont40.StylePriority.UsePadding = false;
        this.cont40.StylePriority.UseTextAlignment = false;
        this.cont40.Text = "CONT 40";
        this.cont40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cont40.Weight = 0.20689684918039752D;
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.HeightF = 79F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.HeightF = 5.208333F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
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
        this.ReportHeader.HeightF = 163.625F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // denngay
        // 
        this.denngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MM/yyyy}")});
        this.denngay.LocationFloat = new DevExpress.Utils.PointFloat(938.9587F, 140.625F);
        this.denngay.Name = "denngay";
        this.denngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.denngay.SizeF = new System.Drawing.SizeF(139.5833F, 23F);
        this.denngay.StylePriority.UseTextAlignment = false;
        this.denngay.Text = "Núi Thành, ngày:";
        this.denngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel8
        // 
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(849.3754F, 140.625F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(89.58331F, 23F);
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "đến ngày:";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // tungay
        // 
        this.tungay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MM/yyyy}")});
        this.tungay.LocationFloat = new DevExpress.Utils.PointFloat(583.5421F, 140.625F);
        this.tungay.Name = "tungay";
        this.tungay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.tungay.SizeF = new System.Drawing.SizeF(139.5833F, 23F);
        this.tungay.StylePriority.UseTextAlignment = false;
        this.tungay.Text = "Núi Thành, ngày:";
        this.tungay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(513.7505F, 140.625F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(69.79163F, 23F);
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "từ ngày:";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // today
        // 
        this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
        this.today.LocationFloat = new DevExpress.Utils.PointFloat(938.9587F, 104.7917F);
        this.today.Name = "today";
        this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.today.SizeF = new System.Drawing.SizeF(139.5833F, 23F);
        this.today.StylePriority.UseTextAlignment = false;
        this.today.Text = "Núi Thành, ngày:";
        this.today.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(799.3754F, 104.7917F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(139.5833F, 23F);
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Núi Thành, ngày:";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 65.87502F);
        this.xrLabel4.Multiline = true;
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1558.542F, 21.95834F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UsePadding = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "BÁO CÁO DOANH SỐ - THEO NHÀ CUNG ỨNG";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 43.9167F);
        this.xrLabel3.Multiline = true;
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1558.542F, 21.95834F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UsePadding = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21.95835F);
        this.xrLabel2.Multiline = true;
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1558.542F, 21.95834F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UsePadding = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Multiline = true;
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1558.542F, 21.95834F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UsePadding = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ma_dtkd,
            this.xrLabel7,
            this.xrTable2});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("ma_dtkd", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 64.58334F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // ma_dtkd
        // 
        this.ma_dtkd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
        this.ma_dtkd.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ma_dtkd.LocationFloat = new DevExpress.Utils.PointFloat(64.58334F, 9.29168F);
        this.ma_dtkd.Name = "ma_dtkd";
        this.ma_dtkd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.ma_dtkd.SizeF = new System.Drawing.SizeF(1493.958F, 23F);
        this.ma_dtkd.StylePriority.UseFont = false;
        this.ma_dtkd.StylePriority.UseTextAlignment = false;
        this.ma_dtkd.Text = "Mã NCC:";
        this.ma_dtkd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 9.291668F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(64.58334F, 23F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Mã NCC:";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.BackColor = System.Drawing.Color.DarkSlateBlue;
        this.xrTable2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.ForeColor = System.Drawing.Color.White;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 32.29167F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1558.542F, 32.29166F);
        this.xrTable2.StylePriority.UseBackColor = false;
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseForeColor = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "Tháng";
        this.xrTableCell14.Weight = 0.13146548367567562D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "Ngày MTK";
        this.xrTableCell15.Weight = 0.18803885224032149D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "Mã KH";
        this.xrTableCell16.Weight = 0.31950432965935149D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "Số Invoice";
        this.xrTableCell17.Weight = 0.28987054766152148D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "PO";
        this.xrTableCell18.Weight = 0.32058177105746621D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "Trị giá";
        this.xrTableCell19.Weight = 0.25862061930511882D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "Discount";
        this.xrTableCell5.Weight = 0.18103443289991555D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "Total discount";
        this.xrTableCell6.Weight = 0.2586206177709498D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Text = "Tổng tiền PX";
        this.xrTableCell20.Weight = 0.31034473231903714D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "Tổng tiền NCC khác";
        this.xrTableCell21.Weight = 0.32435340829907605D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "P+(INV)";
        this.xrTableCell22.Weight = 0.206896493650643D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "P-(INV)";
        this.xrTableCell23.Weight = 0.20689649097168364D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "P+(PO)";
        this.xrTableCell2.Weight = 0.20689648624475066D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "P-(PO)";
        this.xrTableCell3.Weight = 0.20689649611034144D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "CONT 20";
        this.xrTableCell24.Weight = 0.2068964935797506D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Text = "CONT 40HC";
        this.xrTableCell25.Weight = 0.20689649721914785D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Text = "CONT 40";
        this.xrTableCell26.Weight = 0.20689652044157281D;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.ReportFooter.HeightF = 53.125F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrTable4
        // 
        this.xrTable4.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(1558.542F, 53.125F);
        this.xrTable4.StylePriority.UseBorderColor = false;
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        this.xrTable4.StylePriority.UseTextAlignment = false;
        this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.sumtc_trigia,
            this.sumtc_discount,
            this.sumtc_totaldiscount,
            this.sumtc_tongtienpx,
            this.sumtc_tongtienncc_khac,
            this.sumtc_phicong_inv,
            this.sumtc_phitru_inv,
            this.sumtc_phicong_po,
            this.sumtc_phitru_po,
            this.sumtc_cont20,
            this.sumtc_cont40hc,
            this.sumtc_cont40});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Multiline = true;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 4, 0, 0, 100F);
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Tổng cộng cuối cùng:";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell1.Weight = 1.2494611554509403D;
        // 
        // sumtc_trigia
        // 
        this.sumtc_trigia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "trigia")});
        this.sumtc_trigia.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sumtc_trigia.Multiline = true;
        this.sumtc_trigia.Name = "sumtc_trigia";
        this.sumtc_trigia.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sumtc_trigia.StylePriority.UseFont = false;
        this.sumtc_trigia.StylePriority.UsePadding = false;
        this.sumtc_trigia.StylePriority.UseTextAlignment = false;
        xrSummary4.FormatString = "{0:#,#.00}";
        xrSummary4.IgnoreNullValues = true;
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_trigia.Summary = xrSummary4;
        this.sumtc_trigia.Text = "sumtc_trigia";
        this.sumtc_trigia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_trigia.Weight = 0.25862128476932278D;
        // 
        // sumtc_discount
        // 
        this.sumtc_discount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sumtc_discount")});
        this.sumtc_discount.Name = "sumtc_discount";
        this.sumtc_discount.StylePriority.UseTextAlignment = false;
        xrSummary5.FormatString = "{0:#,#}";
        xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_discount.Summary = xrSummary5;
        this.sumtc_discount.Text = "sumtc_discount";
        this.sumtc_discount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_discount.Weight = 0.18103413811744765D;
        // 
        // sumtc_totaldiscount
        // 
        this.sumtc_totaldiscount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totaldiscount")});
        this.sumtc_totaldiscount.Name = "sumtc_totaldiscount";
        this.sumtc_totaldiscount.StylePriority.UseTextAlignment = false;
        xrSummary6.FormatString = "{0:#,#.00}";
        xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_totaldiscount.Summary = xrSummary6;
        this.sumtc_totaldiscount.Text = "sumtc_totaldiscount";
        this.sumtc_totaldiscount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_totaldiscount.Weight = 0.25862068102243529D;
        // 
        // sumtc_tongtienpx
        // 
        this.sumtc_tongtienpx.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienpx")});
        this.sumtc_tongtienpx.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sumtc_tongtienpx.Multiline = true;
        this.sumtc_tongtienpx.Name = "sumtc_tongtienpx";
        this.sumtc_tongtienpx.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sumtc_tongtienpx.StylePriority.UseFont = false;
        this.sumtc_tongtienpx.StylePriority.UsePadding = false;
        this.sumtc_tongtienpx.StylePriority.UseTextAlignment = false;
        xrSummary7.FormatString = "{0:#,#.00}";
        xrSummary7.IgnoreNullValues = true;
        xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_tongtienpx.Summary = xrSummary7;
        this.sumtc_tongtienpx.Text = "sumtc_tongtienpx";
        this.sumtc_tongtienpx.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_tongtienpx.Weight = 0.3103446212737157D;
        // 
        // sumtc_tongtienncc_khac
        // 
        this.sumtc_tongtienncc_khac.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienncc_khac")});
        this.sumtc_tongtienncc_khac.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sumtc_tongtienncc_khac.Multiline = true;
        this.sumtc_tongtienncc_khac.Name = "sumtc_tongtienncc_khac";
        this.sumtc_tongtienncc_khac.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sumtc_tongtienncc_khac.StylePriority.UseFont = false;
        this.sumtc_tongtienncc_khac.StylePriority.UsePadding = false;
        this.sumtc_tongtienncc_khac.StylePriority.UseTextAlignment = false;
        xrSummary8.FormatString = "{0:#,#.00}";
        xrSummary8.IgnoreNullValues = true;
        xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_tongtienncc_khac.Summary = xrSummary8;
        this.sumtc_tongtienncc_khac.Text = "sumtc_tongtienncc_khac";
        this.sumtc_tongtienncc_khac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_tongtienncc_khac.Weight = 0.3243534688183794D;
        // 
        // sumtc_phicong_inv
        // 
        this.sumtc_phicong_inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phicong_inv")});
        this.sumtc_phicong_inv.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sumtc_phicong_inv.Multiline = true;
        this.sumtc_phicong_inv.Name = "sumtc_phicong_inv";
        this.sumtc_phicong_inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sumtc_phicong_inv.StylePriority.UseFont = false;
        this.sumtc_phicong_inv.StylePriority.UsePadding = false;
        this.sumtc_phicong_inv.StylePriority.UseTextAlignment = false;
        xrSummary9.FormatString = "{0:#,#.00}";
        xrSummary9.IgnoreNullValues = true;
        xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_phicong_inv.Summary = xrSummary9;
        this.sumtc_phicong_inv.Text = "sumtc_phicong_inv";
        this.sumtc_phicong_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_phicong_inv.Weight = 0.20689651582910223D;
        // 
        // sumtc_phitru_inv
        // 
        this.sumtc_phitru_inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phitru_inv")});
        this.sumtc_phitru_inv.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sumtc_phitru_inv.Multiline = true;
        this.sumtc_phitru_inv.Name = "sumtc_phitru_inv";
        this.sumtc_phitru_inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sumtc_phitru_inv.StylePriority.UseFont = false;
        this.sumtc_phitru_inv.StylePriority.UsePadding = false;
        this.sumtc_phitru_inv.StylePriority.UseTextAlignment = false;
        xrSummary10.FormatString = "{0:#,#.00}";
        xrSummary10.IgnoreNullValues = true;
        xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_phitru_inv.Summary = xrSummary10;
        this.sumtc_phitru_inv.Text = "sumtc_phitru_inv";
        this.sumtc_phitru_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_phitru_inv.Weight = 0.20689682884919725D;
        // 
        // sumtc_phicong_po
        // 
        this.sumtc_phicong_po.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phicong_po")});
        this.sumtc_phicong_po.Name = "sumtc_phicong_po";
        xrSummary11.FormatString = "{0:#,#.00}";
        xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_phicong_po.Summary = xrSummary11;
        this.sumtc_phicong_po.Text = "sumtc_phicong_po";
        this.sumtc_phicong_po.Weight = 0.20689591893476322D;
        // 
        // sumtc_phitru_po
        // 
        this.sumtc_phitru_po.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phitru_po")});
        this.sumtc_phitru_po.Name = "sumtc_phitru_po";
        xrSummary12.FormatString = "{0:#,#.00}";
        xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_phitru_po.Summary = xrSummary12;
        this.sumtc_phitru_po.Text = "sumtc_phitru_po";
        this.sumtc_phitru_po.Weight = 0.20689749743003416D;
        // 
        // sumtc_cont20
        // 
        this.sumtc_cont20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont20")});
        this.sumtc_cont20.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sumtc_cont20.Multiline = true;
        this.sumtc_cont20.Name = "sumtc_cont20";
        this.sumtc_cont20.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sumtc_cont20.StylePriority.UseFont = false;
        this.sumtc_cont20.StylePriority.UsePadding = false;
        this.sumtc_cont20.StylePriority.UseTextAlignment = false;
        xrSummary13.FormatString = "{0:#,#}";
        xrSummary13.IgnoreNullValues = true;
        xrSummary13.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_cont20.Summary = xrSummary13;
        this.sumtc_cont20.Text = "sumtc_cont20";
        this.sumtc_cont20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_cont20.Weight = 0.20689588300888473D;
        // 
        // sumtc_cont40hc
        // 
        this.sumtc_cont40hc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40hc")});
        this.sumtc_cont40hc.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sumtc_cont40hc.Multiline = true;
        this.sumtc_cont40hc.Name = "sumtc_cont40hc";
        this.sumtc_cont40hc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sumtc_cont40hc.StylePriority.UseFont = false;
        this.sumtc_cont40hc.StylePriority.UsePadding = false;
        this.sumtc_cont40hc.StylePriority.UseTextAlignment = false;
        xrSummary14.FormatString = "{0:#,#}";
        xrSummary14.IgnoreNullValues = true;
        xrSummary14.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_cont40hc.Summary = xrSummary14;
        this.sumtc_cont40hc.Text = "sumtc_cont40hc";
        this.sumtc_cont40hc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_cont40hc.Weight = 0.20689651330483094D;
        // 
        // sumtc_cont40
        // 
        this.sumtc_cont40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40")});
        this.sumtc_cont40.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sumtc_cont40.Multiline = true;
        this.sumtc_cont40.Name = "sumtc_cont40";
        this.sumtc_cont40.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sumtc_cont40.StylePriority.UseFont = false;
        this.sumtc_cont40.StylePriority.UsePadding = false;
        this.sumtc_cont40.StylePriority.UseTextAlignment = false;
        xrSummary15.FormatString = "{0:#,#}";
        xrSummary15.IgnoreNullValues = true;
        xrSummary15.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.sumtc_cont40.Summary = xrSummary15;
        this.sumtc_cont40.Text = "sumtc_cont40";
        this.sumtc_cont40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sumtc_cont40.Weight = 0.20689652913479284D;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.GroupFooter1.HeightF = 41.66667F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTable3
        // 
        this.xrTable3.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1558.542F, 41.66667F);
        this.xrTable3.StylePriority.UseBorderColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.sum_trigia,
            this.sum_discount,
            this.sum_totaldiscount,
            this.sum_tongtienpx,
            this.sum_tongtienncc_khac,
            this.sum_phicong_inv,
            this.sum_phitru_inv,
            this.sum_phicong_po,
            this.sum_phitru_po,
            this.sum_cont20,
            this.sum_cont40hc,
            this.sum_cont40});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Multiline = true;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 4, 0, 0, 100F);
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Tổng cộng theo NCC:";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell4.Weight = 1.2494613527628491D;
        // 
        // sum_trigia
        // 
        this.sum_trigia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "trigia")});
        this.sum_trigia.Multiline = true;
        this.sum_trigia.Name = "sum_trigia";
        this.sum_trigia.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sum_trigia.StylePriority.UsePadding = false;
        this.sum_trigia.StylePriority.UseTextAlignment = false;
        xrSummary16.FormatString = "{0:#,#.00}";
        xrSummary16.IgnoreNullValues = true;
        xrSummary16.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_trigia.Summary = xrSummary16;
        this.sum_trigia.Text = "sum_trigia";
        this.sum_trigia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_trigia.Weight = 0.25862098880145945D;
        // 
        // sum_discount
        // 
        this.sum_discount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sum_discount")});
        this.sum_discount.Name = "sum_discount";
        this.sum_discount.StylePriority.UseTextAlignment = false;
        xrSummary17.FormatString = "{0:#,#}";
        xrSummary17.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_discount.Summary = xrSummary17;
        this.sum_discount.Text = "sum_discount";
        this.sum_discount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_discount.Weight = 0.18103415784863855D;
        // 
        // sum_totaldiscount
        // 
        this.sum_totaldiscount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totaldiscount")});
        this.sum_totaldiscount.Name = "sum_totaldiscount";
        this.sum_totaldiscount.StylePriority.UseTextAlignment = false;
        xrSummary18.FormatString = "{0:#,#.00}";
        xrSummary18.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_totaldiscount.Summary = xrSummary18;
        this.sum_totaldiscount.Text = "sum_totaldiscount";
        this.sum_totaldiscount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_totaldiscount.Weight = 0.25862052317290818D;
        // 
        // sum_tongtienpx
        // 
        this.sum_tongtienpx.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienpx")});
        this.sum_tongtienpx.Multiline = true;
        this.sum_tongtienpx.Name = "sum_tongtienpx";
        this.sum_tongtienpx.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sum_tongtienpx.StylePriority.UsePadding = false;
        this.sum_tongtienpx.StylePriority.UseTextAlignment = false;
        xrSummary19.FormatString = "{0:#,#.00}";
        xrSummary19.IgnoreNullValues = true;
        xrSummary19.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_tongtienpx.Summary = xrSummary19;
        this.sum_tongtienpx.Text = "sum_tongtienpx";
        this.sum_tongtienpx.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_tongtienpx.Weight = 0.31034477912324304D;
        // 
        // sum_tongtienncc_khac
        // 
        this.sum_tongtienncc_khac.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtienncc_khac")});
        this.sum_tongtienncc_khac.Multiline = true;
        this.sum_tongtienncc_khac.Name = "sum_tongtienncc_khac";
        this.sum_tongtienncc_khac.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sum_tongtienncc_khac.StylePriority.UsePadding = false;
        this.sum_tongtienncc_khac.StylePriority.UseTextAlignment = false;
        xrSummary20.FormatString = "{0:#,#.00}";
        xrSummary20.IgnoreNullValues = true;
        xrSummary20.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_tongtienncc_khac.Summary = xrSummary20;
        this.sum_tongtienncc_khac.Text = "sum_tongtienncc_khac";
        this.sum_tongtienncc_khac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_tongtienncc_khac.Weight = 0.32435335043126373D;
        // 
        // sum_phicong_inv
        // 
        this.sum_phicong_inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phicong_inv")});
        this.sum_phicong_inv.Multiline = true;
        this.sum_phicong_inv.Name = "sum_phicong_inv";
        this.sum_phicong_inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sum_phicong_inv.StylePriority.UsePadding = false;
        this.sum_phicong_inv.StylePriority.UseTextAlignment = false;
        xrSummary21.FormatString = "{0:#,#.00}";
        xrSummary21.IgnoreNullValues = true;
        xrSummary21.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_phicong_inv.Summary = xrSummary21;
        this.sum_phicong_inv.Text = "sum_phicong_inv";
        this.sum_phicong_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_phicong_inv.Weight = 0.20689698937768375D;
        // 
        // sum_phitru_inv
        // 
        this.sum_phitru_inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phitru_inv")});
        this.sum_phitru_inv.Multiline = true;
        this.sum_phitru_inv.Name = "sum_phitru_inv";
        this.sum_phitru_inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sum_phitru_inv.StylePriority.UsePadding = false;
        this.sum_phitru_inv.StylePriority.UseTextAlignment = false;
        xrSummary22.FormatString = "{0:#,#.00}";
        xrSummary22.IgnoreNullValues = true;
        xrSummary22.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_phitru_inv.Summary = xrSummary22;
        this.sum_phitru_inv.Text = "sum_phitru_inv";
        this.sum_phitru_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_phitru_inv.Weight = 0.206896513150143D;
        // 
        // sum_phicong_po
        // 
        this.sum_phicong_po.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phicong_po")});
        this.sum_phicong_po.Name = "sum_phicong_po";
        xrSummary23.FormatString = "{0:#,#.00}";
        xrSummary23.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_phicong_po.Summary = xrSummary23;
        this.sum_phicong_po.Text = "sum_phicong_po";
        this.sum_phicong_po.Weight = 0.20689591893476322D;
        // 
        // sum_phitru_po
        // 
        this.sum_phitru_po.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phitru_po")});
        this.sum_phitru_po.Name = "sum_phitru_po";
        xrSummary24.FormatString = "{0:#,#.00}";
        xrSummary24.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_phitru_po.Summary = xrSummary24;
        this.sum_phitru_po.Text = "sum_phitru_po";
        this.sum_phitru_po.Weight = 0.20689718173098004D;
        // 
        // sum_cont20
        // 
        this.sum_cont20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont20")});
        this.sum_cont20.Multiline = true;
        this.sum_cont20.Name = "sum_cont20";
        this.sum_cont20.NullValueText = "0";
        this.sum_cont20.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sum_cont20.StylePriority.UsePadding = false;
        this.sum_cont20.StylePriority.UseTextAlignment = false;
        xrSummary25.FormatString = "{0:#,#}";
        xrSummary25.IgnoreNullValues = true;
        xrSummary25.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_cont20.Summary = xrSummary25;
        this.sum_cont20.Text = "sum_cont20";
        this.sum_cont20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_cont20.Weight = 0.20689621843909983D;
        // 
        // sum_cont40hc
        // 
        this.sum_cont40hc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40hc")});
        this.sum_cont40hc.Multiline = true;
        this.sum_cont40hc.Name = "sum_cont40hc";
        this.sum_cont40hc.NullValueText = "0";
        this.sum_cont40hc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sum_cont40hc.StylePriority.UsePadding = false;
        this.sum_cont40hc.StylePriority.UseTextAlignment = false;
        xrSummary26.FormatString = "{0:#,#}";
        xrSummary26.IgnoreNullValues = true;
        xrSummary26.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_cont40hc.Summary = xrSummary26;
        this.sum_cont40hc.Text = "sum_cont40hc";
        this.sum_cont40hc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_cont40hc.Weight = 0.20689653303602174D;
        // 
        // sum_cont40
        // 
        this.sum_cont40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "cont40")});
        this.sum_cont40.Multiline = true;
        this.sum_cont40.Name = "sum_cont40";
        this.sum_cont40.NullValueText = "0";
        this.sum_cont40.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.sum_cont40.StylePriority.UsePadding = false;
        this.sum_cont40.StylePriority.UseTextAlignment = false;
        xrSummary27.FormatString = "{0:#,#}";
        xrSummary27.IgnoreNullValues = true;
        xrSummary27.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.sum_cont40.Summary = xrSummary27;
        this.sum_cont40.Text = "sum_cont40";
        this.sum_cont40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.sum_cont40.Weight = 0.2068965291347929D;
        // 
        // rptDoanhSoTheoNCC
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.ReportHeader,
            this.GroupHeader1,
            this.ReportFooter,
            this.GroupFooter1});
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(3, 6, 79, 5);
        this.PageHeight = 1169;
        this.PageWidth = 1654;
        this.PaperKind = System.Drawing.Printing.PaperKind.A3;
        this.ReportPrintOptions.DetailCountOnEmptyDataSource = 12;
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
