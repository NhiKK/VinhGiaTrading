using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptBaoCaoQuanTri
/// </summary>
public class rptBaoCaoQuanTri : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRLabel xrLabel3;
    private XRLabel xrLabel1;
    private XRLabel xrLabel4;
    private XRLabel xrLabel2;
    private XRLabel xrLabel5;
    private XRLabel today;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell14;
    private XRTableCell tieuchi;
    private XRTableCell tongcong;
    private XRTableCell t1;
    private XRTableCell t2;
    private XRTableCell t3;
    private XRTableCell t4;
    private XRTableCell t5;
    private XRTableCell t6;
    private XRTableCell t7;
    private XRTableCell t8;
    private XRTableCell t9;
    private XRTableCell t10;
    private XRTableCell t11;
    private XRTableCell t12;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell groupname;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell27;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptBaoCaoQuanTri()
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
        string resourceFileName = "rptBaoCaoQuanTri.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.tieuchi = new DevExpress.XtraReports.UI.XRTableCell();
        this.tongcong = new DevExpress.XtraReports.UI.XRTableCell();
        this.t1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.t12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.today = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.groupname = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 53.125F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.BackColor = System.Drawing.Color.Transparent;
        this.xrTable2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.ForeColor = System.Drawing.Color.Black;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1647.375F, 53.125F);
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
            this.tieuchi,
            this.tongcong,
            this.t1,
            this.t2,
            this.t3,
            this.t4,
            this.t5,
            this.t6,
            this.t7,
            this.t8,
            this.t9,
            this.t10,
            this.t11,
            this.t12});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.CanShrink = true;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Weight = 0.362068964836859D;
        // 
        // tieuchi
        // 
        this.tieuchi.CanShrink = true;
        this.tieuchi.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tieuchi")});
        this.tieuchi.Name = "tieuchi";
        this.tieuchi.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.tieuchi.StylePriority.UsePadding = false;
        this.tieuchi.StylePriority.UseTextAlignment = false;
        this.tieuchi.Text = "Ngày MTK";
        this.tieuchi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.tieuchi.Weight = 0.27252154509036858D;
        // 
        // tongcong
        // 
        this.tongcong.CanShrink = true;
        this.tongcong.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongcong", "{0:#,#.00}")});
        this.tongcong.Name = "tongcong";
        this.tongcong.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.tongcong.StylePriority.UsePadding = false;
        this.tongcong.StylePriority.UseTextAlignment = false;
        this.tongcong.Text = "PO";
        this.tongcong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.tongcong.Weight = 0.336206898536202D;
        // 
        // t1
        // 
        this.t1.CanShrink = true;
        this.t1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t1", "{0:#,#.00}")});
        this.t1.Name = "t1";
        this.t1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t1.StylePriority.UsePadding = false;
        this.t1.StylePriority.UseTextAlignment = false;
        this.t1.Text = "Trị giá";
        this.t1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t1.Weight = 0.27413792615837762D;
        // 
        // t2
        // 
        this.t2.CanShrink = true;
        this.t2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t2", "{0:#,#.00}")});
        this.t2.Name = "t2";
        this.t2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t2.StylePriority.UsePadding = false;
        this.t2.StylePriority.UseTextAlignment = false;
        this.t2.Text = "Tổng tiền PX";
        this.t2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t2.Weight = 0.27413793557041666D;
        // 
        // t3
        // 
        this.t3.CanShrink = true;
        this.t3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t3", "{0:#,#.00}")});
        this.t3.Name = "t3";
        this.t3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t3.StylePriority.UsePadding = false;
        this.t3.StylePriority.UseTextAlignment = false;
        this.t3.Text = "Tổng tiền NCC khác";
        this.t3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t3.Weight = 0.274137929220146D;
        // 
        // t4
        // 
        this.t4.CanShrink = true;
        this.t4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t4", "{0:#,#.00}")});
        this.t4.Name = "t4";
        this.t4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t4.StylePriority.UsePadding = false;
        this.t4.StylePriority.UseTextAlignment = false;
        this.t4.Text = "P+(INV)";
        this.t4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t4.Weight = 0.27413792595987257D;
        // 
        // t5
        // 
        this.t5.CanShrink = true;
        this.t5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t5", "{0:#,#.00}")});
        this.t5.Name = "t5";
        this.t5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t5.StylePriority.UsePadding = false;
        this.t5.StylePriority.UseTextAlignment = false;
        this.t5.Text = "P-(INV)";
        this.t5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t5.Weight = 0.27413793525853619D;
        // 
        // t6
        // 
        this.t6.CanShrink = true;
        this.t6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t6", "{0:#,#.00}")});
        this.t6.Name = "t6";
        this.t6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t6.StylePriority.UsePadding = false;
        this.t6.StylePriority.UseTextAlignment = false;
        this.t6.Text = "CONT 20";
        this.t6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t6.Weight = 0.27413793866048192D;
        // 
        // t7
        // 
        this.t7.CanShrink = true;
        this.t7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t7", "{0:#,#.00}")});
        this.t7.Name = "t7";
        this.t7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t7.StylePriority.UsePadding = false;
        this.t7.StylePriority.UseTextAlignment = false;
        this.t7.Text = "CONT 40HC";
        this.t7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t7.Weight = 0.27413793025485383D;
        // 
        // t8
        // 
        this.t8.CanShrink = true;
        this.t8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t8", "{0:#,#.00}")});
        this.t8.Name = "t8";
        this.t8.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t8.StylePriority.UsePadding = false;
        this.t8.StylePriority.UseTextAlignment = false;
        this.t8.Text = "CONT 40";
        this.t8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t8.Weight = 0.27413792191307063D;
        // 
        // t9
        // 
        this.t9.CanShrink = true;
        this.t9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t9", "{0:#,#.00}")});
        this.t9.Name = "t9";
        this.t9.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t9.StylePriority.UsePadding = false;
        this.t9.StylePriority.UseTextAlignment = false;
        this.t9.Text = "t9";
        this.t9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t9.Weight = 0.27413792647377666D;
        // 
        // t10
        // 
        this.t10.CanShrink = true;
        this.t10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t10", "{0:#,#.00}")});
        this.t10.Name = "t10";
        this.t10.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t10.StylePriority.UsePadding = false;
        this.t10.StylePriority.UseTextAlignment = false;
        this.t10.Text = "t10";
        this.t10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t10.Weight = 0.27413792875412962D;
        // 
        // t11
        // 
        this.t11.CanShrink = true;
        this.t11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t11", "{0:#,#.00}")});
        this.t11.Name = "t11";
        this.t11.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t11.StylePriority.UsePadding = false;
        this.t11.StylePriority.UseTextAlignment = false;
        this.t11.Text = "t11";
        this.t11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t11.Weight = 0.27413792989430635D;
        // 
        // t12
        // 
        this.t12.CanShrink = true;
        this.t12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "t12", "{0:#,#.00}")});
        this.t12.Name = "t12";
        this.t12.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
        this.t12.StylePriority.UsePadding = false;
        this.t12.StylePriority.UseTextAlignment = false;
        this.t12.Text = "t12";
        this.t12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.t12.Weight = 0.27413794962549903D;
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.today,
            this.xrLabel3,
            this.xrLabel1,
            this.xrLabel4,
            this.xrLabel2});
        this.topMarginBand1.HeightF = 147.1251F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // xrLabel5
        // 
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(928.4376F, 114.1251F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(139.5833F, 23F);
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Núi Thành, ngày:";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // today
        // 
        this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
        this.today.LocationFloat = new DevExpress.Utils.PointFloat(1068.021F, 114.1251F);
        this.today.Name = "today";
        this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.today.SizeF = new System.Drawing.SizeF(139.5833F, 23F);
        this.today.StylePriority.UseTextAlignment = false;
        this.today.Text = "Núi Thành, ngày:";
        this.today.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 53.91671F);
        this.xrLabel3.Multiline = true;
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1650F, 21.95834F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UsePadding = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
        this.xrLabel1.Multiline = true;
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1650F, 21.95834F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UsePadding = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.87503F);
        this.xrLabel4.Multiline = true;
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1650F, 21.95834F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UsePadding = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "BÁO CÁO QUẢN TRỊ";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 31.95836F);
        this.xrLabel2.Multiline = true;
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1650F, 21.95834F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UsePadding = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.HeightF = 0F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("groupname", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 43.75F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrTable1
        // 
        this.xrTable1.BackColor = System.Drawing.Color.DarkSlateBlue;
        this.xrTable1.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.BorderWidth = 1;
        this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.ForeColor = System.Drawing.Color.White;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(3.433228E-05F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1647.375F, 43.75F);
        this.xrTable1.StylePriority.UseBackColor = false;
        this.xrTable1.StylePriority.UseBorderColor = false;
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseForeColor = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.groupname,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell1,
            this.xrTableCell28,
            this.xrTableCell27});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // groupname
        // 
        this.groupname.BackColor = System.Drawing.Color.SlateGray;
        this.groupname.CanShrink = true;
        this.groupname.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "groupname")});
        this.groupname.Name = "groupname";
        this.groupname.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.groupname.StylePriority.UseBackColor = false;
        this.groupname.StylePriority.UsePadding = false;
        this.groupname.StylePriority.UseTextAlignment = false;
        this.groupname.Text = "Tên group";
        this.groupname.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.groupname.Weight = 0.36206892466501983D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.CanShrink = true;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "Tiêu chí";
        this.xrTableCell2.Weight = 0.27252155047747573D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.CanShrink = true;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "Tổng cộng 12 tháng";
        this.xrTableCell4.Weight = 0.3362068643786924D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.CanShrink = true;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "Tháng 1";
        this.xrTableCell5.Weight = 0.27413791012962607D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.CanShrink = true;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "Tháng 2";
        this.xrTableCell6.Weight = 0.2741379089956425D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.CanShrink = true;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "Tháng 3";
        this.xrTableCell7.Weight = 0.27413790854208525D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.CanShrink = true;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "Tháng 4";
        this.xrTableCell8.Weight = 0.27413790219181622D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.CanShrink = true;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "Tháng 5";
        this.xrTableCell9.Weight = 0.27413790879713806D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.CanShrink = true;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "Tháng 6";
        this.xrTableCell10.Weight = 0.27413790823020545D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.CanShrink = true;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "Tháng 7";
        this.xrTableCell11.Weight = 0.27413791163215151D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.CanShrink = true;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "Tháng 8";
        this.xrTableCell12.Weight = 0.274137903226523D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.CanShrink = true;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "Tháng 9";
        this.xrTableCell13.Weight = 0.27413791461593107D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.CanShrink = true;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Tháng 10";
        this.xrTableCell1.Weight = 0.27413791159139528D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.CanShrink = true;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Text = "Tháng 11";
        this.xrTableCell28.Weight = 0.27413791159139544D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.CanShrink = true;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.Text = "Tháng 12";
        this.xrTableCell27.Weight = 0.27413793890782434D;
        // 
        // rptBaoCaoQuanTri
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.GroupHeader1});
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(4, 0, 147, 0);
        this.PageHeight = 1169;
        this.PageWidth = 1654;
        this.PaperKind = System.Drawing.Printing.PaperKind.A3;
        this.ReportPrintOptions.DetailCountOnEmptyDataSource = 12;
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
