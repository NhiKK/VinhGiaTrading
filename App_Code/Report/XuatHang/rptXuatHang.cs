using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for XtraReport1
/// </summary>
public class rptXuatHang : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel1;
    private XRLabel xrLabel12;
    private XRLabel lb_sochungtu;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel7;
    private XRLabel bl_ngay;
    private XRLabel xrLabel4;
    private XRLabel xrLabel11;
    private XRLabel xrLabel9;
    private XRLabel xrLabel16;
    private XRLabel xrLabel13;
    private XRSubreport xr_subreport;
    private rptItem rptItem1;
    private XRLabel lb_dh_so2;
    private XRLabel xrLabel24;
    private XRLabel xrLabel22;
    private XRLabel lb_nhacungung2;
    private XRLabel lblhdlamhang;
    private XRLabel xrLabel28;
    private XRLabel xrLabel19;
    private XRLabel xrLabel8;
    private XRLabel xrLabel32;
    private XRLabel xrLabel29;
    private XRLabel xrLabel30;
    private XRLabel xrLabel18;
    private XRLabel xrLabel6;
    private XRLabel xrLabel10;
    private XRLabel xrLabel5;
    private XRLabel xrLabel14;
    private XRLabel xrLabel17;
    private XRLabel xrLabel15;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell2;
    private XRTableCell huongdankhac;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell dacdiemhanghoa;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptXuatHang()
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
            string resourceFileName = "rptXuatHang.resx";
            System.Resources.ResourceManager resources = global::Resources.rptXuatHang.ResourceManager;
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.huongdankhac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblhdlamhang = new DevExpress.XtraReports.UI.XRLabel();
            this.xr_subreport = new DevExpress.XtraReports.UI.XRSubreport();
			this.rptItem1 = new rptItem();
            this.lb_dh_so2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_nhacungung2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.bl_ngay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lb_sochungtu = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.dacdiemhanghoa = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			((System.ComponentModel.ISupportInitialize)(this.rptItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrLabel17,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel10,
            this.xrLabel5,
            this.lblhdlamhang,
            this.xr_subreport,
            this.lb_dh_so2,
            this.xrLabel24,
            this.lb_nhacungung2,
            this.xrLabel22,
            this.xrLabel28,
            this.xrLabel19,
            this.xrLabel8,
            this.xrLabel32,
            this.xrLabel29,
            this.xrLabel30,
            this.xrLabel18,
            this.xrLabel6,
            this.xrLabel1,
            this.xrLabel11,
            this.xrLabel4,
            this.bl_ngay,
            this.xrLabel7,
            this.xrPictureBox1,
            this.lb_sochungtu,
            this.xrLabel12,
            this.xrLabel9,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel13,
            this.xrLabel16,
            this.xrTable1});
            this.Detail.HeightF = 567.1247F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.BorderColor = System.Drawing.Color.Transparent;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(3.125731F, 403.7502F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(975.9993F, 28.83289F);
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.huongdankhac});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.CanGrow = true;
            this.xrTableCell2.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "2. Các hướng dẫn khác:";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell2.Weight = 1.6041667175292969D;
            // 
            // huongdankhac
            // 
            this.huongdankhac.CanGrow = true;
            this.huongdankhac.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "huongdankhac")});
            this.huongdankhac.Font = new System.Drawing.Font("Arial", 9.75F);
            this.huongdankhac.Multiline = true;
            this.huongdankhac.Name = "huongdankhac";
            this.huongdankhac.StylePriority.UseFont = false;
            this.huongdankhac.StylePriority.UseTextAlignment = false;
            this.huongdankhac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.huongdankhac.Weight = 8.1558271789550769D;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(429.125F, 439.5831F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(163.5415F, 23F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "Thủ kho";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(429.125F, 462.5833F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(163.5415F, 22.99997F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Ký và ghi rõ họ tên";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel14
            // 
            this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nguoitao")});
            this.xrLabel14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(3.125151F, 537.7914F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(223.9164F, 25F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "[nguoitao]";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "noigiaohang")});
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(133.334F, 260.3752F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(845.7911F, 23.00002F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "[noigiaohang]";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(3.125153F, 260.3751F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(130.2083F, 23F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Nơi giao hàng:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblhdlamhang
            // 
            this.lblhdlamhang.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblhdlamhang.LocationFloat = new DevExpress.Utils.PointFloat(3.125156F, 340.7502F);
            this.lblhdlamhang.Multiline = true;
            this.lblhdlamhang.Name = "lblhdlamhang";
            this.lblhdlamhang.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblhdlamhang.SizeF = new System.Drawing.SizeF(975.9999F, 25F);
            this.lblhdlamhang.StylePriority.UseFont = false;
            this.lblhdlamhang.StylePriority.UseTextAlignment = false;
            this.lblhdlamhang.Text = "Hướng dẫn làm hàng:";
            this.lblhdlamhang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xr_subreport
            // 
            this.xr_subreport.LocationFloat = new DevExpress.Utils.PointFloat(3.125156F, 283.3752F);
            this.xr_subreport.Name = "xr_subreport";
            this.xr_subreport.ReportSource = this.rptItem1;
            this.xr_subreport.SizeF = new System.Drawing.SizeF(975.9999F, 57.37496F);
            this.xr_subreport.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xr_subreport_BeforePrint);
            // 
            // lb_dh_so2
            // 
            this.lb_dh_so2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ct_dsdh")});
            this.lb_dh_so2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_dh_so2.LocationFloat = new DevExpress.Utils.PointFloat(133.3335F, 237.3751F);
            this.lb_dh_so2.Name = "lb_dh_so2";
            this.lb_dh_so2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_dh_so2.SizeF = new System.Drawing.SizeF(845.7911F, 23.00002F);
            this.lb_dh_so2.StylePriority.UseFont = false;
            this.lb_dh_so2.StylePriority.UseTextAlignment = false;
            this.lb_dh_so2.Text = "[ct_dsdh]";
            this.lb_dh_so2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(3.125159F, 237.3751F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(130.2083F, 23F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "Xuất theo DSHD số:";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_nhacungung2
            // 
            this.lb_nhacungung2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_dtkd")});
            this.lb_nhacungung2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lb_nhacungung2.LocationFloat = new DevExpress.Utils.PointFloat(103.1257F, 214.3751F);
            this.lb_nhacungung2.Name = "lb_nhacungung2";
            this.lb_nhacungung2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_nhacungung2.SizeF = new System.Drawing.SizeF(875.9994F, 23.00002F);
            this.lb_nhacungung2.StylePriority.UseFont = false;
            this.lb_nhacungung2.StylePriority.UseTextAlignment = false;
            this.lb_nhacungung2.Text = "[ten_dtkd]";
            this.lb_nhacungung2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(3.125159F, 214.3751F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "Nhà cung ứng:";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(3.125159F, 439.5833F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(223.9164F, 23.00003F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "Người lập phiếu";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(773.1665F, 439.5833F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(205.9585F, 23.00003F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "Nhà cung ứng";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(227.0416F, 462.5833F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(202.0834F, 22.99997F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Ký và ghi rõ họ tên";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(592.6664F, 439.5833F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(180.5001F, 23F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            this.xrLabel32.Text = "KCS";
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(3.125159F, 462.5833F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(223.9164F, 22.99997F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.Text = "Ký và ghi rõ họ tên";
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(227.0416F, 439.5833F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(202.0834F, 23F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "Người lên conts";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(773.1665F, 462.5833F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(205.9585F, 22.99997F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "Ký và ghi rõ họ tên";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(592.6664F, 462.5833F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(180.5001F, 22.99997F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Ký và ghi rõ họ tên";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 16F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(202.0834F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(605.1665F, 54.25002F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(4.166667F, 189.3751F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(399.9999F, 25F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Số container:";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(202.0834F, 76.45836F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(605.1665F, 22.20834F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Tel.:  (Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // bl_ngay
            // 
            this.bl_ngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaylap", "{0:dd/MM/yyyy}")});
            this.bl_ngay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.bl_ngay.LocationFloat = new DevExpress.Utils.PointFloat(675.2497F, 139.375F);
            this.bl_ngay.Name = "bl_ngay";
            this.bl_ngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.bl_ngay.SizeF = new System.Drawing.SizeF(91.70862F, 25F);
            this.bl_ngay.StylePriority.UseFont = false;
            this.bl_ngay.StylePriority.UseTextAlignment = false;
            this.bl_ngay.Text = "bl_ngay";
            this.bl_ngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(554.4165F, 139.375F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(120.8333F, 25F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Núi Thành, ngày";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(133.3335F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(68.74988F, 56.91668F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // lb_sochungtu
            // 
            this.lb_sochungtu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ct_dh")});
            this.lb_sochungtu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lb_sochungtu.LocationFloat = new DevExpress.Utils.PointFloat(66.66666F, 164.3751F);
            this.lb_sochungtu.Name = "lb_sochungtu";
            this.lb_sochungtu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_sochungtu.SizeF = new System.Drawing.SizeF(337.4999F, 25F);
            this.lb_sochungtu.StylePriority.UseFont = false;
            this.lb_sochungtu.StylePriority.UseTextAlignment = false;
            this.lb_sochungtu.Text = "[ct_dh]";
            this.lb_sochungtu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(404.1666F, 189.3751F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(151.0417F, 25F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Số Seal:";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(4.166683F, 164.3751F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(62.5F, 25F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Số PO:";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(202.0834F, 98.6667F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(605.1665F, 40.70834F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "PHIẾU YÊU CẦU VÀ XÁC NHẬN XUẤT HÀNG";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(202.0832F, 54.25002F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(605.1664F, 22.20834F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(675.2498F, 189.3751F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(91.70856F, 25F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Container:";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(404.1666F, 164.3751F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(151.0418F, 25F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "Ngày xuất:";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.BorderColor = System.Drawing.Color.Transparent;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(3.125151F, 371.7502F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(975.9994F, 25F);
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.dacdiemhanghoa});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanGrow = true;
            this.xrTableCell1.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "1. Đặc điểm hàng hóa:";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell1.Weight = 1.6041667175292969D;
            // 
            // dacdiemhanghoa
            // 
            this.dacdiemhanghoa.CanGrow = true;
            this.dacdiemhanghoa.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dacdiemhanghoa")});
            this.dacdiemhanghoa.Font = new System.Drawing.Font("Arial", 9.75F);
            this.dacdiemhanghoa.Multiline = true;
            this.dacdiemhanghoa.Name = "dacdiemhanghoa";
            this.dacdiemhanghoa.StylePriority.UseFont = false;
            this.dacdiemhanghoa.StylePriority.UseTextAlignment = false;
            this.dacdiemhanghoa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.dacdiemhanghoa.Weight = 8.1558271789550769D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 3F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 33.91698F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rptXuatHang
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(51, 94, 3, 34);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "17.1";
			 ((System.ComponentModel.ISupportInitialize)(this.rptItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion


    private void xr_subreport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        String c_danhsachdathang_id = (String)GetCurrentColumnValue("c_danhsachdathang_id");
        String sql = String.Format(@"select 
                            sp.ma_sanpham, ddsdh.mota_tienganh, dongdh.ma_sanpham_khach
                            , dvt.ten_dvt, dg.ten_donggoi, ddsdh.sl_dathang, dongdh.soluong as sl_po, dongdh.soluong_daxuat as sl_thucxuat, (dongdh.soluong - dongdh.soluong_daxuat) as sl_yeucau
                        from 
                            c_dongdsdh ddsdh, md_doitackinhdoanh dtkd, md_sanpham sp, md_donvitinhsanpham dvt
                            , md_donggoi dg, c_dongdonhang dongdh
                        where 
                            ddsdh.md_sanpham_id = sp.md_sanpham_id
                            AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
                            AND ddsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
                            AND dongdh.c_dongdonhang_id = ddsdh.c_dongdonhang_id
                            AND dongdh.md_donggoi_id = dg.md_donggoi_id
                            AND ddsdh.c_danhsachdathang_id = N'{0}' order by sp.ma_sanpham asc ", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);
        SqlDataAdapter da = new SqlDataAdapter(sql, mdbc.GetConnection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        rptItem1.DataAdapter = da;
        rptItem1.DataSource = ds;
    }
}
