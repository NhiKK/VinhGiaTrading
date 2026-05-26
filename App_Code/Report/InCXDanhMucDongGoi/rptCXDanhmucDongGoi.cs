using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptCXDanhmucDongGoi
/// </summary>
public class rptCXDanhmucDongGoi : DevExpress.XtraReports.UI.XtraReport
{
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel8;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel1;
    private XRLabel today;
    private XRLabel xrLabel4;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell19;
    private ReportFooterBand ReportFooter;
    private XRLabel sumslouter;
    private XRLabel sumslinner;
    private XRLabel xrLabel6;
    private DetailBand Detail;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell madonggoi;
    private XRTableCell slinner;
    private XRTableCell dvtinner;
    private XRTableCell l1;
    private XRTableCell w1;
    private XRTableCell h1;
    private XRTableCell slouter;
    private XRTableCell dvtouter;
    private XRTableCell l2;
    private XRTableCell w2;
    private XRTableCell h2;
    private XRTableCell v2;
    private XRTableCell vd;
    private XRTableCell vn;
    private XRTableCell ghichu;
    private XRTableCell ngayxacnhan;
    private XRTableCell soluonggoi_ctn2;
    private XRTableCell soluonggoi_ctn;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptCXDanhmucDongGoi()
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.madonggoi = new DevExpress.XtraReports.UI.XRTableCell();
            this.slinner = new DevExpress.XtraReports.UI.XRTableCell();
            this.dvtinner = new DevExpress.XtraReports.UI.XRTableCell();
            this.l1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.w1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.h1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.slouter = new DevExpress.XtraReports.UI.XRTableCell();
            this.dvtouter = new DevExpress.XtraReports.UI.XRTableCell();
            this.l2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.w2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.h2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.v2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.soluonggoi_ctn2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.vd = new DevExpress.XtraReports.UI.XRTableCell();
            this.vn = new DevExpress.XtraReports.UI.XRTableCell();
            this.ghichu = new DevExpress.XtraReports.UI.XRTableCell();
            this.ngayxacnhan = new DevExpress.XtraReports.UI.XRTableCell();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.today = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.soluonggoi_ctn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.sumslouter = new DevExpress.XtraReports.UI.XRLabel();
            this.sumslinner = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 32.29167F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.BlueViolet;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1167F, 32.29167F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.madonggoi,
            this.slinner,
            this.dvtinner,
            this.l1,
            this.w1,
            this.h1,
            this.slouter,
            this.dvtouter,
            this.l2,
            this.w2,
            this.h2,
            this.v2,
            this.soluonggoi_ctn2,
            this.vd,
            this.vn,
            this.ghichu,
            this.ngayxacnhan});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // madonggoi
            // 
            this.madonggoi.BackColor = System.Drawing.Color.Transparent;
            this.madonggoi.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.madonggoi.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.madonggoi.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "madonggoi")});
            this.madonggoi.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.madonggoi.ForeColor = System.Drawing.Color.Black;
            this.madonggoi.Name = "madonggoi";
            this.madonggoi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.madonggoi.StylePriority.UseBackColor = false;
            this.madonggoi.StylePriority.UseBorderColor = false;
            this.madonggoi.StylePriority.UseBorders = false;
            this.madonggoi.StylePriority.UseBorderWidth = false;
            this.madonggoi.StylePriority.UseFont = false;
            this.madonggoi.StylePriority.UseForeColor = false;
            this.madonggoi.StylePriority.UsePadding = false;
            this.madonggoi.StylePriority.UseTextAlignment = false;
            this.madonggoi.Text = "Mã đóng gói";
            this.madonggoi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.madonggoi.Weight = 0.32777844881804397D;
            // 
            // slinner
            // 
            this.slinner.BackColor = System.Drawing.Color.Transparent;
            this.slinner.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.slinner.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.slinner.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slinner")});
            this.slinner.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slinner.ForeColor = System.Drawing.Color.Black;
            this.slinner.Name = "slinner";
            this.slinner.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.slinner.StylePriority.UseBackColor = false;
            this.slinner.StylePriority.UseBorderColor = false;
            this.slinner.StylePriority.UseBorders = false;
            this.slinner.StylePriority.UseBorderWidth = false;
            this.slinner.StylePriority.UseFont = false;
            this.slinner.StylePriority.UseForeColor = false;
            this.slinner.StylePriority.UsePadding = false;
            this.slinner.StylePriority.UseTextAlignment = false;
            this.slinner.Text = "SLinner";
            this.slinner.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.slinner.Weight = 0.20639317225771781D;
            // 
            // dvtinner
            // 
            this.dvtinner.BackColor = System.Drawing.Color.Transparent;
            this.dvtinner.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.dvtinner.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.dvtinner.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvtinner")});
            this.dvtinner.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvtinner.ForeColor = System.Drawing.Color.Black;
            this.dvtinner.Name = "dvtinner";
            this.dvtinner.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.dvtinner.StylePriority.UseBackColor = false;
            this.dvtinner.StylePriority.UseBorderColor = false;
            this.dvtinner.StylePriority.UseBorders = false;
            this.dvtinner.StylePriority.UseBorderWidth = false;
            this.dvtinner.StylePriority.UseFont = false;
            this.dvtinner.StylePriority.UseForeColor = false;
            this.dvtinner.StylePriority.UsePadding = false;
            this.dvtinner.StylePriority.UseTextAlignment = false;
            this.dvtinner.Text = "DVTInner";
            this.dvtinner.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dvtinner.Weight = 0.23514529956097446D;
            // 
            // l1
            // 
            this.l1.BackColor = System.Drawing.Color.Transparent;
            this.l1.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.l1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.l1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l1")});
            this.l1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l1.ForeColor = System.Drawing.Color.Black;
            this.l1.Name = "l1";
            this.l1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.l1.StylePriority.UseBackColor = false;
            this.l1.StylePriority.UseBorderColor = false;
            this.l1.StylePriority.UseBorders = false;
            this.l1.StylePriority.UseBorderWidth = false;
            this.l1.StylePriority.UseFont = false;
            this.l1.StylePriority.UseForeColor = false;
            this.l1.StylePriority.UsePadding = false;
            this.l1.StylePriority.UseTextAlignment = false;
            this.l1.Text = "L1";
            this.l1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.l1.Weight = 0.16032640968614187D;
            // 
            // w1
            // 
            this.w1.BackColor = System.Drawing.Color.Transparent;
            this.w1.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.w1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.w1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w1")});
            this.w1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.w1.ForeColor = System.Drawing.Color.Black;
            this.w1.Name = "w1";
            this.w1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.w1.StylePriority.UseBackColor = false;
            this.w1.StylePriority.UseBorderColor = false;
            this.w1.StylePriority.UseBorders = false;
            this.w1.StylePriority.UseBorderWidth = false;
            this.w1.StylePriority.UseFont = false;
            this.w1.StylePriority.UseForeColor = false;
            this.w1.StylePriority.UsePadding = false;
            this.w1.StylePriority.UseTextAlignment = false;
            this.w1.Text = "W1";
            this.w1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.w1.Weight = 0.1603264073301551D;
            // 
            // h1
            // 
            this.h1.BackColor = System.Drawing.Color.Transparent;
            this.h1.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.h1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.h1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h1")});
            this.h1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.h1.ForeColor = System.Drawing.Color.Black;
            this.h1.Name = "h1";
            this.h1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.h1.StylePriority.UseBackColor = false;
            this.h1.StylePriority.UseBorderColor = false;
            this.h1.StylePriority.UseBorders = false;
            this.h1.StylePriority.UseBorderWidth = false;
            this.h1.StylePriority.UseFont = false;
            this.h1.StylePriority.UseForeColor = false;
            this.h1.StylePriority.UsePadding = false;
            this.h1.StylePriority.UseTextAlignment = false;
            this.h1.Text = "H1";
            this.h1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.h1.Weight = 0.16032640651311242D;
            // 
            // slouter
            // 
            this.slouter.BackColor = System.Drawing.Color.Transparent;
            this.slouter.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.slouter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.slouter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slouter")});
            this.slouter.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slouter.ForeColor = System.Drawing.Color.Black;
            this.slouter.Name = "slouter";
            this.slouter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.slouter.StylePriority.UseBackColor = false;
            this.slouter.StylePriority.UseBorderColor = false;
            this.slouter.StylePriority.UseBorders = false;
            this.slouter.StylePriority.UseBorderWidth = false;
            this.slouter.StylePriority.UseFont = false;
            this.slouter.StylePriority.UseForeColor = false;
            this.slouter.StylePriority.UsePadding = false;
            this.slouter.StylePriority.UseTextAlignment = false;
            this.slouter.Text = "SLOuter";
            this.slouter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.slouter.Weight = 0.21170744892028137D;
            // 
            // dvtouter
            // 
            this.dvtouter.BackColor = System.Drawing.Color.Transparent;
            this.dvtouter.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.dvtouter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.dvtouter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvtouter")});
            this.dvtouter.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvtouter.ForeColor = System.Drawing.Color.Black;
            this.dvtouter.Name = "dvtouter";
            this.dvtouter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.dvtouter.StylePriority.UseBackColor = false;
            this.dvtouter.StylePriority.UseBorderColor = false;
            this.dvtouter.StylePriority.UseBorders = false;
            this.dvtouter.StylePriority.UseBorderWidth = false;
            this.dvtouter.StylePriority.UseFont = false;
            this.dvtouter.StylePriority.UseForeColor = false;
            this.dvtouter.StylePriority.UsePadding = false;
            this.dvtouter.StylePriority.UseTextAlignment = false;
            this.dvtouter.Text = "DVTOuter";
            this.dvtouter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dvtouter.Weight = 0.24835756051927366D;
            // 
            // l2
            // 
            this.l2.BackColor = System.Drawing.Color.Transparent;
            this.l2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.l2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.l2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "l2")});
            this.l2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l2.ForeColor = System.Drawing.Color.Black;
            this.l2.Name = "l2";
            this.l2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.l2.StylePriority.UseBackColor = false;
            this.l2.StylePriority.UseBorderColor = false;
            this.l2.StylePriority.UseBorders = false;
            this.l2.StylePriority.UseBorderWidth = false;
            this.l2.StylePriority.UseFont = false;
            this.l2.StylePriority.UseForeColor = false;
            this.l2.StylePriority.UsePadding = false;
            this.l2.StylePriority.UseTextAlignment = false;
            this.l2.Text = "L2";
            this.l2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.l2.Weight = 0.16032641616819776D;
            // 
            // w2
            // 
            this.w2.BackColor = System.Drawing.Color.Transparent;
            this.w2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.w2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.w2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "w2")});
            this.w2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.w2.ForeColor = System.Drawing.Color.Black;
            this.w2.Name = "w2";
            this.w2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.w2.StylePriority.UseBackColor = false;
            this.w2.StylePriority.UseBorderColor = false;
            this.w2.StylePriority.UseBorders = false;
            this.w2.StylePriority.UseBorderWidth = false;
            this.w2.StylePriority.UseFont = false;
            this.w2.StylePriority.UseForeColor = false;
            this.w2.StylePriority.UsePadding = false;
            this.w2.StylePriority.UseTextAlignment = false;
            this.w2.Text = "W2";
            this.w2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.w2.Weight = 0.16032640937267867D;
            // 
            // h2
            // 
            this.h2.BackColor = System.Drawing.Color.Transparent;
            this.h2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.h2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.h2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "h2")});
            this.h2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.h2.ForeColor = System.Drawing.Color.Black;
            this.h2.Name = "h2";
            this.h2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.h2.StylePriority.UseBackColor = false;
            this.h2.StylePriority.UseBorderColor = false;
            this.h2.StylePriority.UseBorders = false;
            this.h2.StylePriority.UseBorderWidth = false;
            this.h2.StylePriority.UseFont = false;
            this.h2.StylePriority.UseForeColor = false;
            this.h2.StylePriority.UsePadding = false;
            this.h2.StylePriority.UseTextAlignment = false;
            this.h2.Text = "H2";
            this.h2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.h2.Weight = 0.16032640641808654D;
            // 
            // v2
            // 
            this.v2.BackColor = System.Drawing.Color.Transparent;
            this.v2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.v2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.v2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "v2")});
            this.v2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v2.ForeColor = System.Drawing.Color.Black;
            this.v2.Name = "v2";
            this.v2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.v2.StylePriority.UseBackColor = false;
            this.v2.StylePriority.UseBorderColor = false;
            this.v2.StylePriority.UseBorders = false;
            this.v2.StylePriority.UseBorderWidth = false;
            this.v2.StylePriority.UseFont = false;
            this.v2.StylePriority.UseForeColor = false;
            this.v2.StylePriority.UsePadding = false;
            this.v2.StylePriority.UseTextAlignment = false;
            this.v2.Text = "V2";
            this.v2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.v2.Weight = 0.20077907926102528D;
            // 
            // soluonggoi_ctn2
            // 
            this.soluonggoi_ctn2.BackColor = System.Drawing.Color.Transparent;
            this.soluonggoi_ctn2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.soluonggoi_ctn2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.soluonggoi_ctn2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soluonggoi_ctn")});
            this.soluonggoi_ctn2.Name = "soluonggoi_ctn2";
            this.soluonggoi_ctn2.StylePriority.UseBackColor = false;
            this.soluonggoi_ctn2.StylePriority.UseBorderColor = false;
            this.soluonggoi_ctn2.StylePriority.UseBorders = false;
            this.soluonggoi_ctn2.StylePriority.UseBorderWidth = false;
            this.soluonggoi_ctn2.StylePriority.UseTextAlignment = false;
            this.soluonggoi_ctn2.Text = "soluonggoi_ctn2";
            this.soluonggoi_ctn2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.soluonggoi_ctn2.Weight = 0.20077929671763717D;
            // 
            // vd
            // 
            this.vd.BackColor = System.Drawing.Color.Transparent;
            this.vd.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.vd.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.vd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vd")});
            this.vd.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vd.ForeColor = System.Drawing.Color.Black;
            this.vd.Name = "vd";
            this.vd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.vd.StylePriority.UseBackColor = false;
            this.vd.StylePriority.UseBorderColor = false;
            this.vd.StylePriority.UseBorders = false;
            this.vd.StylePriority.UseBorderWidth = false;
            this.vd.StylePriority.UseFont = false;
            this.vd.StylePriority.UseForeColor = false;
            this.vd.StylePriority.UsePadding = false;
            this.vd.StylePriority.UseTextAlignment = false;
            this.vd.Text = "VD";
            this.vd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.vd.Weight = 0.20077901161340256D;
            // 
            // vn
            // 
            this.vn.BackColor = System.Drawing.Color.Transparent;
            this.vn.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.vn.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.vn.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vn")});
            this.vn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vn.ForeColor = System.Drawing.Color.Black;
            this.vn.Name = "vn";
            this.vn.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.vn.StylePriority.UseBackColor = false;
            this.vn.StylePriority.UseBorderColor = false;
            this.vn.StylePriority.UseBorders = false;
            this.vn.StylePriority.UseBorderWidth = false;
            this.vn.StylePriority.UseFont = false;
            this.vn.StylePriority.UseForeColor = false;
            this.vn.StylePriority.UsePadding = false;
            this.vn.StylePriority.UseTextAlignment = false;
            this.vn.Text = "VN";
            this.vn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.vn.Weight = 0.20077940153921514D;
            // 
            // ghichu
            // 
            this.ghichu.BackColor = System.Drawing.Color.Transparent;
            this.ghichu.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.ghichu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ghichu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ghichu")});
            this.ghichu.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ghichu.ForeColor = System.Drawing.Color.Black;
            this.ghichu.Name = "ghichu";
            this.ghichu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.ghichu.StylePriority.UseBackColor = false;
            this.ghichu.StylePriority.UseBorderColor = false;
            this.ghichu.StylePriority.UseBorders = false;
            this.ghichu.StylePriority.UseBorderWidth = false;
            this.ghichu.StylePriority.UseFont = false;
            this.ghichu.StylePriority.UseForeColor = false;
            this.ghichu.StylePriority.UsePadding = false;
            this.ghichu.StylePriority.UseTextAlignment = false;
            this.ghichu.Text = "Ghi chú";
            this.ghichu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ghichu.Weight = 0.30924596273518862D;
            // 
            // ngayxacnhan
            // 
            this.ngayxacnhan.BackColor = System.Drawing.Color.Transparent;
            this.ngayxacnhan.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.ngayxacnhan.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ngayxacnhan.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngayxacnhan", "{0:dd/MM/yyyy}")});
            this.ngayxacnhan.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ngayxacnhan.ForeColor = System.Drawing.Color.Black;
            this.ngayxacnhan.Name = "ngayxacnhan";
            this.ngayxacnhan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.ngayxacnhan.StylePriority.UseBackColor = false;
            this.ngayxacnhan.StylePriority.UseBorderColor = false;
            this.ngayxacnhan.StylePriority.UseBorders = false;
            this.ngayxacnhan.StylePriority.UseBorderWidth = false;
            this.ngayxacnhan.StylePriority.UseFont = false;
            this.ngayxacnhan.StylePriority.UseForeColor = false;
            this.ngayxacnhan.StylePriority.UsePadding = false;
            this.ngayxacnhan.StylePriority.UseTextAlignment = false;
            this.ngayxacnhan.Text = "Ngày xác nhận";
            this.ngayxacnhan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ngayxacnhan.Weight = 0.31320671891222784D;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 79F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel1,
            this.today,
            this.xrLabel4});
            this.ReportHeader.HeightF = 143.125F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 88.54166F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(1167F, 19.79166F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "DANH MỤC ĐÓNG GÓI";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.99999F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1167F, 23F);
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 45.99997F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(1167F, 23F);
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1167F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // today
            // 
            this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
            this.today.LocationFloat = new DevExpress.Utils.PointFloat(859.8124F, 114.5833F);
            this.today.Name = "today";
            this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.today.SizeF = new System.Drawing.SizeF(191F, 23F);
            this.today.StylePriority.UseTextAlignment = false;
            this.today.Text = "today";
            this.today.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(741.0624F, 114.5833F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(118.75F, 23F);
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Núi Thành, ngày:";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 36.45833F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseBorders = false;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.BlueViolet;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1167F, 36.45833F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorderWidth = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell6,
            this.xrTableCell5,
            this.xrTableCell3,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell8,
            this.xrTableCell7,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell11,
            this.xrTableCell14,
            this.soluonggoi_ctn,
            this.xrTableCell16,
            this.xrTableCell15,
            this.xrTableCell17,
            this.xrTableCell19});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell4.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
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
            this.xrTableCell4.Text = "Mã đóng gói";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 0.32777844881804397D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell6.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell6.ForeColor = System.Drawing.Color.White;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell6.StylePriority.UseBackColor = false;
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UseForeColor = false;
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "SLinner";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 0.20639306352941189D;
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
            this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell5.StylePriority.UseBackColor = false;
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseForeColor = false;
            this.xrTableCell5.StylePriority.UsePadding = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "DVTInner";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell5.Weight = 0.23514540828928038D;
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
            this.xrTableCell3.Text = "L1";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 0.160326416481661D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell9.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell9.ForeColor = System.Drawing.Color.White;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell9.StylePriority.UseBackColor = false;
            this.xrTableCell9.StylePriority.UseBorderColor = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseForeColor = false;
            this.xrTableCell9.StylePriority.UsePadding = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "W1";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell9.Weight = 0.16032641412567422D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell10.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell10.ForeColor = System.Drawing.Color.White;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell10.StylePriority.UseBackColor = false;
            this.xrTableCell10.StylePriority.UseBorderColor = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseForeColor = false;
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "H1";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell10.Weight = 0.16032640651311245D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell8.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell8.ForeColor = System.Drawing.Color.White;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell8.StylePriority.UseBackColor = false;
            this.xrTableCell8.StylePriority.UseBorderColor = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseForeColor = false;
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "SLOuter";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell8.Weight = 0.21170738096509015D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell7.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell7.ForeColor = System.Drawing.Color.White;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell7.StylePriority.UseBackColor = false;
            this.xrTableCell7.StylePriority.UseBorderColor = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseForeColor = false;
            this.xrTableCell7.StylePriority.UsePadding = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "DVTOuter";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.24835756051927366D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell12.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell12.ForeColor = System.Drawing.Color.White;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell12.StylePriority.UseBackColor = false;
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseForeColor = false;
            this.xrTableCell12.StylePriority.UsePadding = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "L2";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell12.Weight = 0.16032641616819776D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell13.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell13.ForeColor = System.Drawing.Color.White;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell13.StylePriority.UseBackColor = false;
            this.xrTableCell13.StylePriority.UseBorderColor = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseForeColor = false;
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "W2";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell13.Weight = 0.16032640937267864D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell11.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell11.ForeColor = System.Drawing.Color.White;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell11.StylePriority.UseBackColor = false;
            this.xrTableCell11.StylePriority.UseBorderColor = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseForeColor = false;
            this.xrTableCell11.StylePriority.UsePadding = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "H2";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell11.Weight = 0.16032640641808654D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell14.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell14.ForeColor = System.Drawing.Color.White;
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell14.StylePriority.UseBackColor = false;
            this.xrTableCell14.StylePriority.UseBorderColor = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseForeColor = false;
            this.xrTableCell14.StylePriority.UsePadding = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "V2";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell14.Weight = 0.2007791879893312D;
            // 
            // soluonggoi_ctn
            // 
            this.soluonggoi_ctn.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.soluonggoi_ctn.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.soluonggoi_ctn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soluonggoi_ctn.ForeColor = System.Drawing.Color.White;
            this.soluonggoi_ctn.Name = "soluonggoi_ctn";
            this.soluonggoi_ctn.StylePriority.UseBackColor = false;
            this.soluonggoi_ctn.StylePriority.UseBorderColor = false;
            this.soluonggoi_ctn.StylePriority.UseFont = false;
            this.soluonggoi_ctn.StylePriority.UseForeColor = false;
            this.soluonggoi_ctn.StylePriority.UseTextAlignment = false;
            this.soluonggoi_ctn.Text = "SL Gói / cnt20";
            this.soluonggoi_ctn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.soluonggoi_ctn.Weight = 0.2007791879893312D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell16.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell16.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell16.ForeColor = System.Drawing.Color.White;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell16.StylePriority.UseBackColor = false;
            this.xrTableCell16.StylePriority.UseBorderColor = false;
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseForeColor = false;
            this.xrTableCell16.StylePriority.UsePadding = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "SL Gói / cnt40";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell16.Weight = 0.200779182671097D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell15.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell15.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell15.ForeColor = System.Drawing.Color.White;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell15.StylePriority.UseBackColor = false;
            this.xrTableCell15.StylePriority.UseBorderColor = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseForeColor = false;
            this.xrTableCell15.StylePriority.UsePadding = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "SL Gói / cnt40hc";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell15.Weight = 0.2007791860461563D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell17.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell17.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell17.ForeColor = System.Drawing.Color.White;
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell17.StylePriority.UseBackColor = false;
            this.xrTableCell17.StylePriority.UseBorderColor = false;
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UseForeColor = false;
            this.xrTableCell17.StylePriority.UsePadding = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = "Ghi chú";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell17.Weight = 0.30924601210045355D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTableCell19.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTableCell19.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell19.ForeColor = System.Drawing.Color.White;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrTableCell19.StylePriority.UseBackColor = false;
            this.xrTableCell19.StylePriority.UseBorderColor = false;
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UseForeColor = false;
            this.xrTableCell19.StylePriority.UsePadding = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.Text = "Ngày xác nhận";
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell19.Weight = 0.31320716548612659D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.sumslouter,
            this.sumslinner,
            this.xrLabel6});
            this.ReportFooter.HeightF = 23F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // sumslouter
            // 
            this.sumslouter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slouter")});
            this.sumslouter.LocationFloat = new DevExpress.Utils.PointFloat(403.4094F, 0F);
            this.sumslouter.Name = "sumslouter";
            this.sumslouter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.sumslouter.SizeF = new System.Drawing.SizeF(68.30765F, 23F);
            this.sumslouter.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.sumslouter.Summary = xrSummary1;
            this.sumslouter.Text = "sumslouter";
            this.sumslouter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // sumslinner
            // 
            this.sumslinner.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slinner")});
            this.sumslinner.LocationFloat = new DevExpress.Utils.PointFloat(105.7581F, 0F);
            this.sumslinner.Name = "sumslinner";
            this.sumslinner.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.sumslinner.SizeF = new System.Drawing.SizeF(66.59295F, 23F);
            this.sumslinner.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.sumslinner.Summary = xrSummary2;
            this.sumslinner.Text = "sumslinner";
            this.sumslinner.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(105.7581F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Tổng cộng:   ";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // rptCXDanhmucDongGoi
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 2, 79, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportPrintOptions.DetailCountOnEmptyDataSource = 12;
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
