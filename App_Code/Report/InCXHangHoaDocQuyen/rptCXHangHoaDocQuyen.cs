using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptCXHangHoaDocQuyen
/// </summary>
public class rptCXHangHoaDocQuyen : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel today;
    private XRLabel xrLabel3;
    private XRLabel xrLabel8;
    private XRLabel xrLabel2;
    private XRLabel xrLabel4;
    private XRLabel xrLabel1;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell2;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell stt;
    private XRTableCell mahh;
    private XRTableCell makh;
    private ReportFooterBand ReportFooter;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptCXHangHoaDocQuyen()
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
        string resourceFileName = "rptCXHangHoaDocQuyen.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.stt = new DevExpress.XtraReports.UI.XRTableCell();
        this.mahh = new DevExpress.XtraReports.UI.XRTableCell();
        this.makh = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.today = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
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
        this.xrTable2.SizeF = new System.Drawing.SizeF(829.9373F, 25F);
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.stt,
            this.mahh,
            this.makh});
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
        // mahh
        // 
        this.mahh.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mahh")});
        this.mahh.Multiline = true;
        this.mahh.Name = "mahh";
        this.mahh.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.mahh.StylePriority.UsePadding = false;
        this.mahh.StylePriority.UseTextAlignment = false;
        this.mahh.Text = "[mahh]";
        this.mahh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.mahh.Weight = 0.44743288991903091D;
        // 
        // makh
        // 
        this.makh.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "makh")});
        this.makh.Multiline = true;
        this.makh.Name = "makh";
        this.makh.NullValueText = "0";
        this.makh.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.makh.StylePriority.UsePadding = false;
        this.makh.StylePriority.UseTextAlignment = false;
        this.makh.Text = "[makh]";
        this.makh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.makh.Weight = 2.2751588800370306D;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 17F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 2.083333F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.today,
            this.xrLabel3,
            this.xrLabel8,
            this.xrLabel2,
            this.xrLabel4,
            this.xrLabel1});
        this.ReportHeader.HeightF = 152.5F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // today
        // 
        this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
        this.today.LocationFloat = new DevExpress.Utils.PointFloat(638.9373F, 119.5F);
        this.today.Name = "today";
        this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.today.SizeF = new System.Drawing.SizeF(191F, 23F);
        this.today.StylePriority.UseTextAlignment = false;
        this.today.Text = "today";
        this.today.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 45.99997F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(829.9373F, 23F);
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 88.54166F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(829.9373F, 19.79166F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "HÀNG HÓA ĐỘC QUYỀN";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.99999F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(829.9373F, 23F);
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(520.1874F, 119.5F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(118.75F, 23F);
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Núi Thành, ngày:";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(829.9373F, 23F);
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
        this.xrTable1.SizeF = new System.Drawing.SizeF(829.9373F, 25F);
        this.xrTable1.StylePriority.UseBackColor = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell4,
            this.xrTableCell2});
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
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell4.StylePriority.UseBackColor = false;
        this.xrTableCell4.StylePriority.UseBorderColor = false;
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseBorderWidth = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UseForeColor = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Mã hàng hóa";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell4.Weight = 0.45503505339112349D;
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
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell2.StylePriority.UseBackColor = false;
        this.xrTableCell2.StylePriority.UseBorderColor = false;
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseBorderWidth = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UseForeColor = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Mã khách hàng";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell2.Weight = 2.2629029419261095D;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Name = "ReportFooter";
        // 
        // rptCXHangHoaDocQuyen
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
        this.Margins = new System.Drawing.Printing.Margins(7, 10, 17, 2);
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

    private void xrTableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }
}
