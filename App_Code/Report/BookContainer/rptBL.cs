using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptBL
/// </summary
/// 

public class rptBL : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel38;
    private XRLabel xrLabel5;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private XRLabel xrLabel39;
    private XRLabel xrLabel6;
    private XRLabel xrLabel44;
    private XRLabel xrLabel45;
    private XRLabel xrLabel41;
    private XRLabel xrLabel42;
    private XRLabel xrLabel40;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel43;
    private XRLabel xrLabel26;
    private XRLabel xrLabel25;
    private XRLabel xrLabel24;
    private XRLabel xrLabel23;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel xrLabel27;
    private XRLabel xrLabel16;
    private XRLabel xrLabel17;
    private XRLabel xrLabel18;
    private XRLabel xrLabel20;
    private XRLabel xrLabel21;
    private XRLabel xrLabel51;
    private XRLabel xrLabel19;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel50;
    private XRLabel xrLabel46;
    private XRLabel xrLabel47;
    private XRLabel xrLabel48;
    private XRLabel xrLabel49;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRLabel xrLabel33;
    private XRLabel xrLabel34;
    private XRLabel xrLabel31;
    private XRLabel xrLabel32;
    private XRLabel xrLabel29;
    private XRLabel xrLabel30;
    private XRLabel xrLabel22;
    private XRLabel xrLabel28;
    private XRLabel xrLabel52;
    private XRLabel xrLabel55;
    private XRLabel xrLabel59;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel37;
    private XRLabel xrLabel53;
    private XRLabel xrLabel58;
    private XRLabel xrLabel60;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptBL()
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
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel61 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel62 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel62,
            this.xrLabel57,
            this.xrLabel61,
            this.xrLabel15,
            this.xrLabel54,
            this.xrLabel56,
            this.xrLabel53,
            this.xrLabel58,
            this.xrLabel60,
            this.xrLabel50,
            this.xrLabel46,
            this.xrLabel47,
            this.xrLabel49,
            this.xrLabel35,
            this.xrLabel36,
            this.xrLabel33,
            this.xrLabel34,
            this.xrLabel31,
            this.xrLabel32,
            this.xrLabel29,
            this.xrLabel30,
            this.xrLabel48,
            this.xrLabel22,
            this.xrLabel28});
        this.Detail.HeightF = 169.5417F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 7.708295F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "HS code";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel54
        // 
        this.xrLabel54.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 7.708295F);
        this.xrLabel54.Name = "xrLabel54";
        this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel54.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel54.StylePriority.UseFont = false;
        this.xrLabel54.StylePriority.UseTextAlignment = false;
        this.xrLabel54.Text = ":";
        this.xrLabel54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel56
        // 
        this.xrLabel56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "hscode")});
        this.xrLabel56.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(199.7085F, 7.708295F);
        this.xrLabel56.Name = "xrLabel56";
        this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel56.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
        this.xrLabel56.StylePriority.UseFont = false;
        this.xrLabel56.StylePriority.UseTextAlignment = false;
        this.xrLabel56.Text = "[hscode]";
        this.xrLabel56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel53
        // 
        this.xrLabel53.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(401F, 96.54185F);
        this.xrLabel53.Name = "xrLabel53";
        this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel53.SizeF = new System.Drawing.SizeF(61.45831F, 22.20834F);
        this.xrLabel53.StylePriority.UseFont = false;
        this.xrLabel53.StylePriority.UseTextAlignment = false;
        this.xrLabel53.Text = "KGS";
        this.xrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel58
        // 
        this.xrLabel58.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(401F, 52.12498F);
        this.xrLabel58.Name = "xrLabel58";
        this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel58.SizeF = new System.Drawing.SizeF(61.45831F, 22.20834F);
        this.xrLabel58.StylePriority.UseFont = false;
        this.xrLabel58.StylePriority.UseTextAlignment = false;
        this.xrLabel58.Text = "pcs/sets";
        this.xrLabel58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel60
        // 
        this.xrLabel60.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel60.LocationFloat = new DevExpress.Utils.PointFloat(401.0001F, 74.33351F);
        this.xrLabel60.Name = "xrLabel60";
        this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel60.SizeF = new System.Drawing.SizeF(61.45828F, 22.20831F);
        this.xrLabel60.StylePriority.UseFont = false;
        this.xrLabel60.StylePriority.UseTextAlignment = false;
        this.xrLabel60.Text = "pkgs";
        this.xrLabel60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel50
        // 
        this.xrLabel50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "measurement")});
        this.xrLabel50.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 118.7502F);
        this.xrLabel50.Name = "xrLabel50";
        this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel50.SizeF = new System.Drawing.SizeF(527.0417F, 22.20834F);
        this.xrLabel50.StylePriority.UseFont = false;
        this.xrLabel50.StylePriority.UseTextAlignment = false;
        this.xrLabel50.Text = "[measurement]";
        this.xrLabel50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel46
        // 
        this.xrLabel46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "grweight")});
        this.xrLabel46.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 96.5416F);
        this.xrLabel46.Name = "xrLabel46";
        this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel46.SizeF = new System.Drawing.SizeF(202.0417F, 22.20834F);
        this.xrLabel46.StylePriority.UseFont = false;
        this.xrLabel46.StylePriority.UseTextAlignment = false;
        this.xrLabel46.Text = "[grweight]";
        this.xrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel47
        // 
        this.xrLabel47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "quantity")});
        this.xrLabel47.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(199.7085F, 52.12498F);
        this.xrLabel47.Name = "xrLabel47";
        this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel47.SizeF = new System.Drawing.SizeF(201.2915F, 22.20834F);
        this.xrLabel47.StylePriority.UseFont = false;
        this.xrLabel47.StylePriority.UseTextAlignment = false;
        this.xrLabel47.Text = "[quantity]";
        this.xrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel49
        // 
        this.xrLabel49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "numofpack")});
        this.xrLabel49.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 74.33331F);
        this.xrLabel49.Name = "xrLabel49";
        this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel49.SizeF = new System.Drawing.SizeF(202.0417F, 22.20834F);
        this.xrLabel49.StylePriority.UseFont = false;
        this.xrLabel49.StylePriority.UseTextAlignment = false;
        this.xrLabel49.Text = "[numofpack]";
        this.xrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel35
        // 
        this.xrLabel35.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 118.75F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = ":";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 118.75F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = "Measurement";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel33
        // 
        this.xrLabel33.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 96.54166F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = "Gross weight";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel34
        // 
        this.xrLabel34.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 96.54166F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = ":";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel31
        // 
        this.xrLabel31.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 74.33331F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "Number of pkgs";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel32
        // 
        this.xrLabel32.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 74.33331F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        this.xrLabel32.Text = ":";
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel29
        // 
        this.xrLabel29.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 52.12498F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = "Quantity";
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 52.12498F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.Text = ":";
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel48
        // 
        this.xrLabel48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "container")});
        this.xrLabel48.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(199.7085F, 29.91663F);
        this.xrLabel48.Name = "xrLabel48";
        this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel48.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
        this.xrLabel48.StylePriority.UseFont = false;
        this.xrLabel48.StylePriority.UseTextAlignment = false;
        this.xrLabel48.Text = "[container]";
        this.xrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel22
        // 
        this.xrLabel22.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 29.91663F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.Text = ":";
        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel28
        // 
        this.xrLabel28.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 29.91663F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "Container/Seal No.";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 294.0004F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(163.9168F, 22.20834F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "M/V";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel17
        // 
        this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "noidi")});
        this.xrLabel17.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 294.0004F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(526.6666F, 22.20834F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "[mv]";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel18
        // 
        this.xrLabel18.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 294.0004F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = ":";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0.3751198F, 227.375F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(163.5416F, 22.20834F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Consignee";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel44
        // 
        this.xrLabel44.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 338.4171F);
        this.xrLabel44.Name = "xrLabel44";
        this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel44.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel44.StylePriority.UseFont = false;
        this.xrLabel44.StylePriority.UseTextAlignment = false;
        this.xrLabel44.Text = ":";
        this.xrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel45
        // 
        this.xrLabel45.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(163.9169F, 360.6255F);
        this.xrLabel45.Name = "xrLabel45";
        this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel45.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel45.StylePriority.UseFont = false;
        this.xrLabel45.StylePriority.UseTextAlignment = false;
        this.xrLabel45.Text = ":";
        this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel41
        // 
        this.xrLabel41.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 271.7921F);
        this.xrLabel41.Name = "xrLabel41";
        this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel41.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel41.StylePriority.UseFont = false;
        this.xrLabel41.StylePriority.UseTextAlignment = false;
        this.xrLabel41.Text = ":";
        this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel42
        // 
        this.xrLabel42.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 249.5835F);
        this.xrLabel42.Name = "xrLabel42";
        this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel42.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel42.StylePriority.UseFont = false;
        this.xrLabel42.StylePriority.UseTextAlignment = false;
        this.xrLabel42.Text = ":";
        this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel40
        // 
        this.xrLabel40.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 227.375F);
        this.xrLabel40.Name = "xrLabel40";
        this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel40.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel40.StylePriority.UseFont = false;
        this.xrLabel40.StylePriority.UseTextAlignment = false;
        this.xrLabel40.Text = ":";
        this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel14
        // 
        this.xrLabel14.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0.3751198F, 271.7921F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(163.5416F, 22.20834F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "Also Notify";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel13
        // 
        this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "alsonotify")});
        this.xrLabel13.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 271.7919F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "[alsonotify]";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel43
        // 
        this.xrLabel43.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 316.2087F);
        this.xrLabel43.Name = "xrLabel43";
        this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel43.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel43.StylePriority.UseFont = false;
        this.xrLabel43.StylePriority.UseTextAlignment = false;
        this.xrLabel43.Text = ":";
        this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel26
        // 
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "to_")});
        this.xrLabel26.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 338.4171F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "[to_]";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel25
        // 
        this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "from_")});
        this.xrLabel25.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 316.2087F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.Text = "[from_]";
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel24
        // 
        this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "notify")});
        this.xrLabel24.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 249.5834F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "[notify]";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel23
        // 
        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "consignee")});
        this.xrLabel23.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 227.375F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        this.xrLabel23.Text = "[consignee]";
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0.7503192F, 360.6255F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(163.1665F, 22.20834F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "Commodity";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 338.4171F);
        this.xrLabel11.Multiline = true;
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(163.1665F, 22.20834F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "To";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 316.2087F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(163.1665F, 22.20834F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "From";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0.3751198F, 249.5835F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(163.5416F, 22.20834F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "Notify";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel27
        // 
        this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "commodity")});
        this.xrLabel27.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 360.6252F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(527.0417F, 22.20834F);
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "[commodity]";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0.3751437F, 205.1667F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(163.5416F, 22.20834F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Shipper";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(199.5001F, 205.1667F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(603.4999F, 22.20836F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel39
        // 
        this.xrLabel39.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 205.1667F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel39.StylePriority.UseFont = false;
        this.xrLabel39.StylePriority.UseTextAlignment = false;
        this.xrLabel39.Text = ":";
        this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0.5418141F, 171.5F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(802.4582F, 22.20834F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Please issue ORIGINAL B/L as follows:";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0.5418141F, 149.2917F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(802.4582F, 22.20834F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "SHIPPING INSTRUCTION";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0.3751198F, 100F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 22.20834F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "To:";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.5417888F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(802.4582F, 23.00002F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Arial", 12F);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0.5417888F, 23.00003F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(802.4582F, 22.20834F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City" +
", Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel38.Font = new System.Drawing.Font("Arial", 12F);
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(0.5418141F, 45.20836F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(802.4582F, 22.20834F);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 48F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 48F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel20
        // 
        this.xrLabel20.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0.3752311F, 0F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(163.5416F, 22.20831F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = "Freight";
        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel21
        // 
        this.xrLabel21.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(199.3336F, 0F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(188.5416F, 22.20831F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.Text = " COLLECT";
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel51
        // 
        this.xrLabel51.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 0F);
        this.xrLabel51.Name = "xrLabel51";
        this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel51.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel51.StylePriority.UseFont = false;
        this.xrLabel51.StylePriority.UseTextAlignment = false;
        this.xrLabel51.Text = ":";
        this.xrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel19
        // 
        this.xrLabel19.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 22.2084F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(163.5416F, 22.20831F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "Detail of red invoice:";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37,
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel38,
            this.xrLabel5,
            this.xrLabel3,
            this.xrLabel4,
            this.xrLabel39,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel27,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel23,
            this.xrLabel24,
            this.xrLabel25,
            this.xrLabel26,
            this.xrLabel43,
            this.xrLabel13,
            this.xrLabel14,
            this.xrLabel40,
            this.xrLabel42,
            this.xrLabel41,
            this.xrLabel45,
            this.xrLabel44,
            this.xrLabel6,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16});
        this.ReportHeader.HeightF = 412.2499F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel37
        // 
        this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dsdonhang", "AS PER ORDER NO. {0}")});
        this.xrLabel37.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(198.5832F, 382.8336F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "xrLabel37";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel52,
            this.xrLabel55,
            this.xrLabel59,
            this.xrLabel20,
            this.xrLabel21,
            this.xrLabel51,
            this.xrLabel19});
        this.ReportFooter.HeightF = 151.9167F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrLabel52
        // 
        this.xrLabel52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tencty")});
        this.xrLabel52.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(199.3335F, 46.41673F);
        this.xrLabel52.Name = "xrLabel52";
        this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel52.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
        this.xrLabel52.StylePriority.UseFont = false;
        this.xrLabel52.StylePriority.UseTextAlignment = false;
        this.xrLabel52.Text = "[tencty]";
        this.xrLabel52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel55
        // 
        this.xrLabel55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachi")});
        this.xrLabel55.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(199.3335F, 68.62507F);
        this.xrLabel55.Name = "xrLabel55";
        this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel55.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
        this.xrLabel55.StylePriority.UseFont = false;
        this.xrLabel55.StylePriority.UseTextAlignment = false;
        this.xrLabel55.Text = "[diachi]";
        this.xrLabel55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel59
        // 
        this.xrLabel59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "masothue")});
        this.xrLabel59.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(199.3335F, 90.83341F);
        this.xrLabel59.Name = "xrLabel59";
        this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel59.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
        this.xrLabel59.StylePriority.UseFont = false;
        this.xrLabel59.StylePriority.UseTextAlignment = false;
        this.xrLabel59.Text = "[masothue]";
        this.xrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.HeightF = 22.20834F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel57
        // 
        this.xrLabel57.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(0.7503195F, 140.9583F);
        this.xrLabel57.Name = "xrLabel57";
        this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel57.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
        this.xrLabel57.StylePriority.UseFont = false;
        this.xrLabel57.StylePriority.UseTextAlignment = false;
        this.xrLabel57.Text = "CRD";
        this.xrLabel57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel61
        // 
        this.xrLabel61.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel61.LocationFloat = new DevExpress.Utils.PointFloat(164.667F, 140.9583F);
        this.xrLabel61.Name = "xrLabel61";
        this.xrLabel61.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel61.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
        this.xrLabel61.StylePriority.UseFont = false;
        this.xrLabel61.StylePriority.UseTextAlignment = false;
        this.xrLabel61.Text = ":";
        this.xrLabel61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel62
        // 
        this.xrLabel62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "crd")});
        this.xrLabel62.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash;
        this.xrLabel62.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel62.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel62.LocationFloat = new DevExpress.Utils.PointFloat(198.5832F, 140.9585F);
        this.xrLabel62.Name = "xrLabel62";
        this.xrLabel62.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel62.SizeF = new System.Drawing.SizeF(527.4169F, 22.20834F);
        this.xrLabel62.StylePriority.UseBorderDashStyle = false;
        this.xrLabel62.StylePriority.UseBorders = false;
        this.xrLabel62.StylePriority.UseFont = false;
        this.xrLabel62.StylePriority.UseTextAlignment = false;
        this.xrLabel62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // rptBL
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter,
            this.GroupHeader1});
        this.Margins = new System.Drawing.Printing.Margins(9, 15, 48, 48);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "21.2";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private XRLabel xrLabel15;
    private XRLabel xrLabel54;
    private XRLabel xrLabel56;
    private XRLabel xrLabel57;
    private XRLabel xrLabel61;
    private XRLabel xrLabel62;
}

//public class rptBL : DevExpress.XtraReports.UI.XtraReport
//{
//    private DevExpress.XtraReports.UI.DetailBand Detail;
//    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
//    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
//    private XRLabel xrLabel1;
//    private XRLabel xrLabel2;
//    private XRLabel xrLabel38;
//    private XRLabel xrLabel5;
//    private XRLabel xrLabel3;
//    private XRLabel xrLabel4;
//    private XRLabel xrLabel7;
//    private XRLabel xrLabel8;
//    private XRLabel xrLabel39;
//    private XRLabel xrLabel6;
//    private XRLabel xrLabel44;
//    private XRLabel xrLabel45;
//    private XRLabel xrLabel41;
//    private XRLabel xrLabel42;
//    private XRLabel xrLabel40;
//    private XRLabel xrLabel14;
//    private XRLabel xrLabel13;
//    private XRLabel xrLabel43;
//    private XRLabel xrLabel26;
//    private XRLabel xrLabel25;
//    private XRLabel xrLabel24;
//    private XRLabel xrLabel23;
//    private XRLabel xrLabel12;
//    private XRLabel xrLabel11;
//    private XRLabel xrLabel10;
//    private XRLabel xrLabel9;
//    private XRLabel xrLabel27;
//    private XRLabel xrLabel15;
//    private XRLabel xrLabel56;
//    private XRLabel xrLabel57;
//    private XRLabel xrLabel16;
//    private XRLabel xrLabel17;
//    private XRLabel xrLabel18;
//    private XRLabel xrLabel20;
//    private XRLabel xrLabel21;
//    private XRLabel xrLabel51;
//    private XRLabel xrLabel19;
//    private ReportHeaderBand ReportHeader;
//    private ReportFooterBand ReportFooter;
//    private XRLabel xrLabel50;
//    private XRLabel xrLabel46;
//    private XRLabel xrLabel47;
//    private XRLabel xrLabel48;
//    private XRLabel xrLabel49;
//    private XRLabel xrLabel35;
//    private XRLabel xrLabel36;
//    private XRLabel xrLabel33;
//    private XRLabel xrLabel34;
//    private XRLabel xrLabel31;
//    private XRLabel xrLabel32;
//    private XRLabel xrLabel29;
//    private XRLabel xrLabel30;
//    private XRLabel xrLabel22;
//    private XRLabel xrLabel28;
//    private XRLabel xrLabel52;
//    private XRLabel xrLabel55;
//    private XRLabel xrLabel59;
//    private GroupHeaderBand GroupHeader1;
//    private XRLabel xrLabel37;
//    private XRLabel xrLabel53;
//    private XRLabel xrLabel58;
//    private XRLabel xrLabel60;
//    /// <summary>
//    /// Required designer variable.
//    /// </summary>
//    private System.ComponentModel.IContainer components = null;

//    public rptBL()
//    {
//        InitializeComponent();
//        //
//        // TODO: Add constructor logic here
//        //
//    }

//    /// <summary> 
//    /// Clean up any resources being used.
//    /// </summary>
//    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//    protected override void Dispose(bool disposing)
//    {
//        if (disposing && (components != null))
//        {
//            components.Dispose();
//        }
//        base.Dispose(disposing);
//    }

//    #region Designer generated code

//    /// <summary>
//    /// Required method for Designer support - do not modify
//    /// the contents of this method with the code editor.
//    /// </summary>
//    private void InitializeComponent()
//    {
//        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
//        this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
//        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
//        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
//        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
//        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
//        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
//        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
//        this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
//        this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
//        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
//        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
//        // 
//        // Detail
//        // 
//        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
//            this.xrLabel53,
//            this.xrLabel58,
//            this.xrLabel60,
//            this.xrLabel50,
//            this.xrLabel46,
//            this.xrLabel47,
//            this.xrLabel49,
//            this.xrLabel35,
//            this.xrLabel36,
//            this.xrLabel33,
//            this.xrLabel34,
//            this.xrLabel31,
//            this.xrLabel32,
//            this.xrLabel29,
//            this.xrLabel30,
//            this.xrLabel48,
//            this.xrLabel22,
//            this.xrLabel28});
//        this.Detail.Dpi = 100F;
//        this.Detail.HeightF = 128.75F;
//        this.Detail.Name = "Detail";
//        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
//        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
//        // 
//        // xrLabel53
//        // 
//        this.xrLabel53.Dpi = 100F;
//        this.xrLabel53.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(401F, 74.33351F);
//        this.xrLabel53.Name = "xrLabel53";
//        this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel53.SizeF = new System.Drawing.SizeF(61.45831F, 22.20834F);
//        this.xrLabel53.StylePriority.UseFont = false;
//        this.xrLabel53.StylePriority.UseTextAlignment = false;
//        this.xrLabel53.Text = "KGS";
//        this.xrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel58
//        // 
//        this.xrLabel58.Dpi = 100F;
//        this.xrLabel58.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(401F, 29.91664F);
//        this.xrLabel58.Name = "xrLabel58";
//        this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel58.SizeF = new System.Drawing.SizeF(61.45831F, 22.20834F);
//        this.xrLabel58.StylePriority.UseFont = false;
//        this.xrLabel58.StylePriority.UseTextAlignment = false;
//        this.xrLabel58.Text = "pcs/sets";
//        this.xrLabel58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel60
//        // 
//        this.xrLabel60.Dpi = 100F;
//        this.xrLabel60.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel60.LocationFloat = new DevExpress.Utils.PointFloat(401.0001F, 52.12517F);
//        this.xrLabel60.Name = "xrLabel60";
//        this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel60.SizeF = new System.Drawing.SizeF(61.45828F, 22.20831F);
//        this.xrLabel60.StylePriority.UseFont = false;
//        this.xrLabel60.StylePriority.UseTextAlignment = false;
//        this.xrLabel60.Text = "pkgs";
//        this.xrLabel60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel50
//        // 
//        this.xrLabel50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "measurement")});
//        this.xrLabel50.Dpi = 100F;
//        this.xrLabel50.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 96.54185F);
//        this.xrLabel50.Name = "xrLabel50";
//        this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel50.SizeF = new System.Drawing.SizeF(527.0417F, 22.20834F);
//        this.xrLabel50.StylePriority.UseFont = false;
//        this.xrLabel50.StylePriority.UseTextAlignment = false;
//        this.xrLabel50.Text = "[measurement]";
//        this.xrLabel50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel46
//        // 
//        this.xrLabel46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "grweight")});
//        this.xrLabel46.Dpi = 100F;
//        this.xrLabel46.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 74.33325F);
//        this.xrLabel46.Name = "xrLabel46";
//        this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel46.SizeF = new System.Drawing.SizeF(202.0417F, 22.20834F);
//        this.xrLabel46.StylePriority.UseFont = false;
//        this.xrLabel46.StylePriority.UseTextAlignment = false;
//        this.xrLabel46.Text = "[grweight]";
//        this.xrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel47
//        // 
//        this.xrLabel47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "quantity")});
//        this.xrLabel47.Dpi = 100F;
//        this.xrLabel47.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(199.7085F, 29.91664F);
//        this.xrLabel47.Name = "xrLabel47";
//        this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel47.SizeF = new System.Drawing.SizeF(201.2915F, 22.20834F);
//        this.xrLabel47.StylePriority.UseFont = false;
//        this.xrLabel47.StylePriority.UseTextAlignment = false;
//        this.xrLabel47.Text = "[quantity]";
//        this.xrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel49
//        // 
//        this.xrLabel49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "numofpack")});
//        this.xrLabel49.Dpi = 100F;
//        this.xrLabel49.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 52.12498F);
//        this.xrLabel49.Name = "xrLabel49";
//        this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel49.SizeF = new System.Drawing.SizeF(202.0417F, 22.20834F);
//        this.xrLabel49.StylePriority.UseFont = false;
//        this.xrLabel49.StylePriority.UseTextAlignment = false;
//        this.xrLabel49.Text = "[numofpack]";
//        this.xrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel35
//        // 
//        this.xrLabel35.Dpi = 100F;
//        this.xrLabel35.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 96.54166F);
//        this.xrLabel35.Name = "xrLabel35";
//        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel35.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel35.StylePriority.UseFont = false;
//        this.xrLabel35.StylePriority.UseTextAlignment = false;
//        this.xrLabel35.Text = ":";
//        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel36
//        // 
//        this.xrLabel36.Dpi = 100F;
//        this.xrLabel36.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 96.54166F);
//        this.xrLabel36.Name = "xrLabel36";
//        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel36.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
//        this.xrLabel36.StylePriority.UseFont = false;
//        this.xrLabel36.StylePriority.UseTextAlignment = false;
//        this.xrLabel36.Text = "Measurement";
//        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel33
//        // 
//        this.xrLabel33.Dpi = 100F;
//        this.xrLabel33.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 74.33332F);
//        this.xrLabel33.Name = "xrLabel33";
//        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel33.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
//        this.xrLabel33.StylePriority.UseFont = false;
//        this.xrLabel33.StylePriority.UseTextAlignment = false;
//        this.xrLabel33.Text = "Gross weight";
//        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel34
//        // 
//        this.xrLabel34.Dpi = 100F;
//        this.xrLabel34.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 74.33332F);
//        this.xrLabel34.Name = "xrLabel34";
//        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel34.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel34.StylePriority.UseFont = false;
//        this.xrLabel34.StylePriority.UseTextAlignment = false;
//        this.xrLabel34.Text = ":";
//        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel31
//        // 
//        this.xrLabel31.Dpi = 100F;
//        this.xrLabel31.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 52.12498F);
//        this.xrLabel31.Name = "xrLabel31";
//        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel31.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
//        this.xrLabel31.StylePriority.UseFont = false;
//        this.xrLabel31.StylePriority.UseTextAlignment = false;
//        this.xrLabel31.Text = "Number of pkgs";
//        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel32
//        // 
//        this.xrLabel32.Dpi = 100F;
//        this.xrLabel32.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 52.12498F);
//        this.xrLabel32.Name = "xrLabel32";
//        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel32.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel32.StylePriority.UseFont = false;
//        this.xrLabel32.StylePriority.UseTextAlignment = false;
//        this.xrLabel32.Text = ":";
//        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel29
//        // 
//        this.xrLabel29.Dpi = 100F;
//        this.xrLabel29.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 29.91664F);
//        this.xrLabel29.Name = "xrLabel29";
//        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel29.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
//        this.xrLabel29.StylePriority.UseFont = false;
//        this.xrLabel29.StylePriority.UseTextAlignment = false;
//        this.xrLabel29.Text = "Quantity";
//        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel30
//        // 
//        this.xrLabel30.Dpi = 100F;
//        this.xrLabel30.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 29.91664F);
//        this.xrLabel30.Name = "xrLabel30";
//        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel30.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel30.StylePriority.UseFont = false;
//        this.xrLabel30.StylePriority.UseTextAlignment = false;
//        this.xrLabel30.Text = ":";
//        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel48
//        // 
//        this.xrLabel48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "container")});
//        this.xrLabel48.Dpi = 100F;
//        this.xrLabel48.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(199.7085F, 7.708295F);
//        this.xrLabel48.Name = "xrLabel48";
//        this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel48.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
//        this.xrLabel48.StylePriority.UseFont = false;
//        this.xrLabel48.StylePriority.UseTextAlignment = false;
//        this.xrLabel48.Text = "[container]";
//        this.xrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel22
//        // 
//        this.xrLabel22.Dpi = 100F;
//        this.xrLabel22.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(164.6669F, 7.708295F);
//        this.xrLabel22.Name = "xrLabel22";
//        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel22.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel22.StylePriority.UseFont = false;
//        this.xrLabel22.StylePriority.UseTextAlignment = false;
//        this.xrLabel22.Text = ":";
//        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel28
//        // 
//        this.xrLabel28.Dpi = 100F;
//        this.xrLabel28.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 7.708295F);
//        this.xrLabel28.Name = "xrLabel28";
//        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel28.SizeF = new System.Drawing.SizeF(163.9167F, 22.20834F);
//        this.xrLabel28.StylePriority.UseFont = false;
//        this.xrLabel28.StylePriority.UseTextAlignment = false;
//        this.xrLabel28.Text = "Container/Seal No.";
//        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel16
//        // 
//        this.xrLabel16.Dpi = 100F;
//        this.xrLabel16.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 358.8334F);
//        this.xrLabel16.Name = "xrLabel16";
//        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel16.SizeF = new System.Drawing.SizeF(163.9168F, 22.20834F);
//        this.xrLabel16.StylePriority.UseFont = false;
//        this.xrLabel16.StylePriority.UseTextAlignment = false;
//        this.xrLabel16.Text = "M/V";
//        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel17
//        // 
//        this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "noidi")});
//        this.xrLabel17.Dpi = 100F;
//        this.xrLabel17.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 358.8334F);
//        this.xrLabel17.Name = "xrLabel17";
//        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel17.SizeF = new System.Drawing.SizeF(526.6666F, 22.20834F);
//        this.xrLabel17.StylePriority.UseFont = false;
//        this.xrLabel17.StylePriority.UseTextAlignment = false;
//        this.xrLabel17.Text = "[mv]";
//        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel18
//        // 
//        this.xrLabel18.Dpi = 100F;
//        this.xrLabel18.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 358.8334F);
//        this.xrLabel18.Name = "xrLabel18";
//        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel18.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel18.StylePriority.UseFont = false;
//        this.xrLabel18.StylePriority.UseTextAlignment = false;
//        this.xrLabel18.Text = ":";
//        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel15
//        // 
//        this.xrLabel15.Dpi = 100F;
//        this.xrLabel15.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(199.5001F, 227.375F);
//        this.xrLabel15.Multiline = true;
//        this.xrLabel15.Name = "xrLabel15";
//        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel15.SizeF = new System.Drawing.SizeF(603.4999F, 23.25003F);
//        this.xrLabel15.StylePriority.UseFont = false;
//        this.xrLabel15.StylePriority.UseTextAlignment = false;
//        this.xrLabel15.Text = "Northern Chu Lai Industrial Zone, Tam Hiep Commune,";
//        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel56
//        // 
//        this.xrLabel56.Dpi = 100F;
//        this.xrLabel56.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(199.5001F, 273.8751F);
//        this.xrLabel56.Multiline = true;
//        this.xrLabel56.Name = "xrLabel56";
//        this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel56.SizeF = new System.Drawing.SizeF(526.2915F, 14.08337F);
//        this.xrLabel56.StylePriority.UseFont = false;
//        this.xrLabel56.StylePriority.UseTextAlignment = false;
//        this.xrLabel56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel57
//        // 
//        this.xrLabel57.Dpi = 100F;
//        this.xrLabel57.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(199.5001F, 250.625F);
//        this.xrLabel57.Multiline = true;
//        this.xrLabel57.Name = "xrLabel57";
//        this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel57.SizeF = new System.Drawing.SizeF(603.4999F, 23.25006F);
//        this.xrLabel57.StylePriority.UseFont = false;
//        this.xrLabel57.StylePriority.UseTextAlignment = false;
//        this.xrLabel57.Text = "Nui Thanh District, Quang Nam Province, Viet Nam";
//        this.xrLabel57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel6
//        // 
//        this.xrLabel6.Dpi = 100F;
//        this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0.3751198F, 292.2081F);
//        this.xrLabel6.Name = "xrLabel6";
//        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel6.SizeF = new System.Drawing.SizeF(163.5416F, 22.20834F);
//        this.xrLabel6.StylePriority.UseFont = false;
//        this.xrLabel6.StylePriority.UseTextAlignment = false;
//        this.xrLabel6.Text = "Consignee";
//        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel44
//        // 
//        this.xrLabel44.Dpi = 100F;
//        this.xrLabel44.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 403.2501F);
//        this.xrLabel44.Name = "xrLabel44";
//        this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel44.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel44.StylePriority.UseFont = false;
//        this.xrLabel44.StylePriority.UseTextAlignment = false;
//        this.xrLabel44.Text = ":";
//        this.xrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel45
//        // 
//        this.xrLabel45.Dpi = 100F;
//        this.xrLabel45.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(163.9169F, 425.4582F);
//        this.xrLabel45.Name = "xrLabel45";
//        this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel45.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel45.StylePriority.UseFont = false;
//        this.xrLabel45.StylePriority.UseTextAlignment = false;
//        this.xrLabel45.Text = ":";
//        this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel41
//        // 
//        this.xrLabel41.Dpi = 100F;
//        this.xrLabel41.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 336.6251F);
//        this.xrLabel41.Name = "xrLabel41";
//        this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel41.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel41.StylePriority.UseFont = false;
//        this.xrLabel41.StylePriority.UseTextAlignment = false;
//        this.xrLabel41.Text = ":";
//        this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel42
//        // 
//        this.xrLabel42.Dpi = 100F;
//        this.xrLabel42.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 314.4166F);
//        this.xrLabel42.Name = "xrLabel42";
//        this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel42.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel42.StylePriority.UseFont = false;
//        this.xrLabel42.StylePriority.UseTextAlignment = false;
//        this.xrLabel42.Text = ":";
//        this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel40
//        // 
//        this.xrLabel40.Dpi = 100F;
//        this.xrLabel40.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 292.2081F);
//        this.xrLabel40.Name = "xrLabel40";
//        this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel40.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel40.StylePriority.UseFont = false;
//        this.xrLabel40.StylePriority.UseTextAlignment = false;
//        this.xrLabel40.Text = ":";
//        this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel14
//        // 
//        this.xrLabel14.Dpi = 100F;
//        this.xrLabel14.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0.3751198F, 336.6251F);
//        this.xrLabel14.Name = "xrLabel14";
//        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel14.SizeF = new System.Drawing.SizeF(163.5416F, 22.20834F);
//        this.xrLabel14.StylePriority.UseFont = false;
//        this.xrLabel14.StylePriority.UseTextAlignment = false;
//        this.xrLabel14.Text = "Also Notify";
//        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel13
//        // 
//        this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "alsonotify")});
//        this.xrLabel13.Dpi = 100F;
//        this.xrLabel13.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 336.625F);
//        this.xrLabel13.Name = "xrLabel13";
//        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel13.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
//        this.xrLabel13.StylePriority.UseFont = false;
//        this.xrLabel13.StylePriority.UseTextAlignment = false;
//        this.xrLabel13.Text = "[alsonotify]";
//        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel43
//        // 
//        this.xrLabel43.Dpi = 100F;
//        this.xrLabel43.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 381.0418F);
//        this.xrLabel43.Name = "xrLabel43";
//        this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel43.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel43.StylePriority.UseFont = false;
//        this.xrLabel43.StylePriority.UseTextAlignment = false;
//        this.xrLabel43.Text = ":";
//        this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel26
//        // 
//        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "to_")});
//        this.xrLabel26.Dpi = 100F;
//        this.xrLabel26.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 403.2501F);
//        this.xrLabel26.Name = "xrLabel26";
//        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel26.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
//        this.xrLabel26.StylePriority.UseFont = false;
//        this.xrLabel26.StylePriority.UseTextAlignment = false;
//        this.xrLabel26.Text = "[to_]";
//        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel25
//        // 
//        this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "from_")});
//        this.xrLabel25.Dpi = 100F;
//        this.xrLabel25.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 381.0418F);
//        this.xrLabel25.Name = "xrLabel25";
//        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel25.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
//        this.xrLabel25.StylePriority.UseFont = false;
//        this.xrLabel25.StylePriority.UseTextAlignment = false;
//        this.xrLabel25.Text = "[from_]";
//        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel24
//        // 
//        this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "notify")});
//        this.xrLabel24.Dpi = 100F;
//        this.xrLabel24.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 314.4164F);
//        this.xrLabel24.Name = "xrLabel24";
//        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel24.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
//        this.xrLabel24.StylePriority.UseFont = false;
//        this.xrLabel24.StylePriority.UseTextAlignment = false;
//        this.xrLabel24.Text = "[notify]";
//        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel23
//        // 
//        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "consignee")});
//        this.xrLabel23.Dpi = 100F;
//        this.xrLabel23.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(199.3334F, 292.2081F);
//        this.xrLabel23.Name = "xrLabel23";
//        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel23.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
//        this.xrLabel23.StylePriority.UseFont = false;
//        this.xrLabel23.StylePriority.UseTextAlignment = false;
//        this.xrLabel23.Text = "[consignee]";
//        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel12
//        // 
//        this.xrLabel12.Dpi = 100F;
//        this.xrLabel12.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0.7503192F, 425.4582F);
//        this.xrLabel12.Name = "xrLabel12";
//        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel12.SizeF = new System.Drawing.SizeF(163.1665F, 22.20834F);
//        this.xrLabel12.StylePriority.UseFont = false;
//        this.xrLabel12.StylePriority.UseTextAlignment = false;
//        this.xrLabel12.Text = "Commodity";
//        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel11
//        // 
//        this.xrLabel11.Dpi = 100F;
//        this.xrLabel11.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 403.2501F);
//        this.xrLabel11.Multiline = true;
//        this.xrLabel11.Name = "xrLabel11";
//        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel11.SizeF = new System.Drawing.SizeF(163.1665F, 22.20834F);
//        this.xrLabel11.StylePriority.UseFont = false;
//        this.xrLabel11.StylePriority.UseTextAlignment = false;
//        this.xrLabel11.Text = "To";
//        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel10
//        // 
//        this.xrLabel10.Dpi = 100F;
//        this.xrLabel10.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0.7502317F, 381.0418F);
//        this.xrLabel10.Name = "xrLabel10";
//        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel10.SizeF = new System.Drawing.SizeF(163.1665F, 22.20834F);
//        this.xrLabel10.StylePriority.UseFont = false;
//        this.xrLabel10.StylePriority.UseTextAlignment = false;
//        this.xrLabel10.Text = "From";
//        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel9
//        // 
//        this.xrLabel9.Dpi = 100F;
//        this.xrLabel9.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0.3751198F, 314.4166F);
//        this.xrLabel9.Name = "xrLabel9";
//        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel9.SizeF = new System.Drawing.SizeF(163.5416F, 22.20834F);
//        this.xrLabel9.StylePriority.UseFont = false;
//        this.xrLabel9.StylePriority.UseTextAlignment = false;
//        this.xrLabel9.Text = "Notify";
//        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel27
//        // 
//        this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "commodity")});
//        this.xrLabel27.Dpi = 100F;
//        this.xrLabel27.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 425.4583F);
//        this.xrLabel27.Name = "xrLabel27";
//        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel27.SizeF = new System.Drawing.SizeF(527.0417F, 22.20834F);
//        this.xrLabel27.StylePriority.UseFont = false;
//        this.xrLabel27.StylePriority.UseTextAlignment = false;
//        this.xrLabel27.Text = "[commodity]";
//        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel7
//        // 
//        this.xrLabel7.Dpi = 100F;
//        this.xrLabel7.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0.3751437F, 205.1667F);
//        this.xrLabel7.Name = "xrLabel7";
//        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel7.SizeF = new System.Drawing.SizeF(163.5416F, 22.20834F);
//        this.xrLabel7.StylePriority.UseFont = false;
//        this.xrLabel7.StylePriority.UseTextAlignment = false;
//        this.xrLabel7.Text = "Shipper";
//        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel8
//        // 
//        this.xrLabel8.Dpi = 100F;
//        this.xrLabel8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
//        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(199.5001F, 205.1667F);
//        this.xrLabel8.Name = "xrLabel8";
//        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel8.SizeF = new System.Drawing.SizeF(603.4999F, 22.20836F);
//        this.xrLabel8.StylePriority.UseFont = false;
//        this.xrLabel8.StylePriority.UseTextAlignment = false;
//        this.xrLabel8.Text = "VINH GIA COMPANY LIMITED";
//        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel39
//        // 
//        this.xrLabel39.Dpi = 100F;
//        this.xrLabel39.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 205.1667F);
//        this.xrLabel39.Name = "xrLabel39";
//        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel39.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel39.StylePriority.UseFont = false;
//        this.xrLabel39.StylePriority.UseTextAlignment = false;
//        this.xrLabel39.Text = ":";
//        this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel4
//        // 
//        this.xrLabel4.Dpi = 100F;
//        this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0.5418141F, 171.5F);
//        this.xrLabel4.Name = "xrLabel4";
//        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel4.SizeF = new System.Drawing.SizeF(802.4582F, 22.20834F);
//        this.xrLabel4.StylePriority.UseFont = false;
//        this.xrLabel4.StylePriority.UseTextAlignment = false;
//        this.xrLabel4.Text = "Please issue ORIGINAL B/L as follows:";
//        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel3
//        // 
//        this.xrLabel3.Dpi = 100F;
//        this.xrLabel3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
//        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0.5418141F, 149.2917F);
//        this.xrLabel3.Name = "xrLabel3";
//        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel3.SizeF = new System.Drawing.SizeF(802.4582F, 22.20834F);
//        this.xrLabel3.StylePriority.UseFont = false;
//        this.xrLabel3.StylePriority.UseTextAlignment = false;
//        this.xrLabel3.Text = "SHIPPING INSTRUCTION";
//        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel5
//        // 
//        this.xrLabel5.Dpi = 100F;
//        this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0.3751198F, 100F);
//        this.xrLabel5.Name = "xrLabel5";
//        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 22.20834F);
//        this.xrLabel5.StylePriority.UseFont = false;
//        this.xrLabel5.StylePriority.UseTextAlignment = false;
//        this.xrLabel5.Text = "To:";
//        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel1
//        // 
//        this.xrLabel1.Dpi = 100F;
//        this.xrLabel1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
//        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.5417888F, 0F);
//        this.xrLabel1.Name = "xrLabel1";
//        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel1.SizeF = new System.Drawing.SizeF(802.4582F, 23.00002F);
//        this.xrLabel1.StylePriority.UseFont = false;
//        this.xrLabel1.StylePriority.UseTextAlignment = false;
//        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
//        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel2
//        // 
//        this.xrLabel2.Dpi = 100F;
//        this.xrLabel2.Font = new System.Drawing.Font("Arial", 12F);
//        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0.5417888F, 23.00003F);
//        this.xrLabel2.Name = "xrLabel2";
//        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel2.SizeF = new System.Drawing.SizeF(802.4582F, 22.20834F);
//        this.xrLabel2.StylePriority.UseFont = false;
//        this.xrLabel2.StylePriority.UseTextAlignment = false;
//        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Tam Hiep Commune, Nui Thanh District, Quang Nam" +
//" Province, Viet Nam";
//        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel38
//        // 
//        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
//        this.xrLabel38.Dpi = 100F;
//        this.xrLabel38.Font = new System.Drawing.Font("Arial", 12F);
//        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(0.5418141F, 45.20836F);
//        this.xrLabel38.Name = "xrLabel38";
//        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel38.SizeF = new System.Drawing.SizeF(802.4582F, 22.20834F);
//        this.xrLabel38.StylePriority.UseBorders = false;
//        this.xrLabel38.StylePriority.UseFont = false;
//        this.xrLabel38.StylePriority.UseTextAlignment = false;
//        this.xrLabel38.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
//        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // TopMargin
//        // 
//        this.TopMargin.Dpi = 100F;
//        this.TopMargin.HeightF = 48F;
//        this.TopMargin.Name = "TopMargin";
//        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
//        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
//        // 
//        // BottomMargin
//        // 
//        this.BottomMargin.Dpi = 100F;
//        this.BottomMargin.HeightF = 48F;
//        this.BottomMargin.Name = "BottomMargin";
//        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
//        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
//        // 
//        // xrLabel20
//        // 
//        this.xrLabel20.Dpi = 100F;
//        this.xrLabel20.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0.3752311F, 0F);
//        this.xrLabel20.Name = "xrLabel20";
//        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel20.SizeF = new System.Drawing.SizeF(163.5416F, 22.20831F);
//        this.xrLabel20.StylePriority.UseFont = false;
//        this.xrLabel20.StylePriority.UseTextAlignment = false;
//        this.xrLabel20.Text = "Freight";
//        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel21
//        // 
//        this.xrLabel21.Dpi = 100F;
//        this.xrLabel21.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
//        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(199.3336F, 0F);
//        this.xrLabel21.Name = "xrLabel21";
//        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel21.SizeF = new System.Drawing.SizeF(188.5416F, 22.20831F);
//        this.xrLabel21.StylePriority.UseFont = false;
//        this.xrLabel21.StylePriority.UseTextAlignment = false;
//        this.xrLabel21.Text = " COLLECT";
//        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel51
//        // 
//        this.xrLabel51.Dpi = 100F;
//        this.xrLabel51.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(163.9168F, 0F);
//        this.xrLabel51.Name = "xrLabel51";
//        this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel51.SizeF = new System.Drawing.SizeF(16.66668F, 22.20834F);
//        this.xrLabel51.StylePriority.UseFont = false;
//        this.xrLabel51.StylePriority.UseTextAlignment = false;
//        this.xrLabel51.Text = ":";
//        this.xrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
//        // 
//        // xrLabel19
//        // 
//        this.xrLabel19.Dpi = 100F;
//        this.xrLabel19.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 22.2084F);
//        this.xrLabel19.Name = "xrLabel19";
//        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel19.SizeF = new System.Drawing.SizeF(163.5416F, 22.20831F);
//        this.xrLabel19.StylePriority.UseFont = false;
//        this.xrLabel19.StylePriority.UseTextAlignment = false;
//        this.xrLabel19.Text = "Detail of red invoice:";
//        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // ReportHeader
//        // 
//        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
//            this.xrLabel37,
//            this.xrLabel1,
//            this.xrLabel2,
//            this.xrLabel38,
//            this.xrLabel5,
//            this.xrLabel3,
//            this.xrLabel4,
//            this.xrLabel39,
//            this.xrLabel8,
//            this.xrLabel7,
//            this.xrLabel27,
//            this.xrLabel9,
//            this.xrLabel10,
//            this.xrLabel11,
//            this.xrLabel12,
//            this.xrLabel23,
//            this.xrLabel24,
//            this.xrLabel25,
//            this.xrLabel26,
//            this.xrLabel43,
//            this.xrLabel13,
//            this.xrLabel14,
//            this.xrLabel40,
//            this.xrLabel42,
//            this.xrLabel41,
//            this.xrLabel45,
//            this.xrLabel44,
//            this.xrLabel6,
//            this.xrLabel57,
//            this.xrLabel56,
//            this.xrLabel15,
//            this.xrLabel18,
//            this.xrLabel17,
//            this.xrLabel16});
//        this.ReportHeader.Dpi = 100F;
//        this.ReportHeader.HeightF = 469.875F;
//        this.ReportHeader.Name = "ReportHeader";
//        // 
//        // xrLabel37
//        // 
//        this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dsdonhang", "AS PER ORDER NO. {0}")});
//        this.xrLabel37.Dpi = 100F;
//        this.xrLabel37.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(198.5832F, 447.6667F);
//        this.xrLabel37.Name = "xrLabel37";
//        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel37.SizeF = new System.Drawing.SizeF(526.2915F, 22.20834F);
//        this.xrLabel37.StylePriority.UseFont = false;
//        this.xrLabel37.StylePriority.UseTextAlignment = false;
//        this.xrLabel37.Text = "xrLabel37";
//        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // ReportFooter
//        // 
//        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
//            this.xrLabel52,
//            this.xrLabel55,
//            this.xrLabel59,
//            this.xrLabel20,
//            this.xrLabel21,
//            this.xrLabel51,
//            this.xrLabel19});
//        this.ReportFooter.Dpi = 100F;
//        this.ReportFooter.HeightF = 151.9167F;
//        this.ReportFooter.Name = "ReportFooter";
//        // 
//        // xrLabel52
//        // 
//        this.xrLabel52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tencty")});
//        this.xrLabel52.Dpi = 100F;
//        this.xrLabel52.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
//        this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(199.3335F, 46.41673F);
//        this.xrLabel52.Name = "xrLabel52";
//        this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel52.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
//        this.xrLabel52.StylePriority.UseFont = false;
//        this.xrLabel52.StylePriority.UseTextAlignment = false;
//        this.xrLabel52.Text = "[tencty]";
//        this.xrLabel52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel55
//        // 
//        this.xrLabel55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachi")});
//        this.xrLabel55.Dpi = 100F;
//        this.xrLabel55.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(199.3335F, 68.62507F);
//        this.xrLabel55.Name = "xrLabel55";
//        this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel55.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
//        this.xrLabel55.StylePriority.UseFont = false;
//        this.xrLabel55.StylePriority.UseTextAlignment = false;
//        this.xrLabel55.Text = "[diachi]";
//        this.xrLabel55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // xrLabel59
//        // 
//        this.xrLabel59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
//            new DevExpress.XtraReports.UI.XRBinding("Text", null, "masothue")});
//        this.xrLabel59.Dpi = 100F;
//        this.xrLabel59.Font = new System.Drawing.Font("Arial", 10F);
//        this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(199.3335F, 90.83341F);
//        this.xrLabel59.Name = "xrLabel59";
//        this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//        this.xrLabel59.SizeF = new System.Drawing.SizeF(526.2914F, 22.20834F);
//        this.xrLabel59.StylePriority.UseFont = false;
//        this.xrLabel59.StylePriority.UseTextAlignment = false;
//        this.xrLabel59.Text = "[masothue]";
//        this.xrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
//        // 
//        // GroupHeader1
//        // 
//        this.GroupHeader1.Dpi = 100F;
//        this.GroupHeader1.HeightF = 22.20834F;
//        this.GroupHeader1.Name = "GroupHeader1";
//        // 
//        // rptBL
//        // 
//        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
//            this.Detail,
//            this.TopMargin,
//            this.BottomMargin,
//            this.ReportHeader,
//            this.ReportFooter,
//            this.GroupHeader1});
//        this.Margins = new System.Drawing.Printing.Margins(9, 15, 48, 48);
//        this.PageHeight = 1169;
//        this.PageWidth = 827;
//        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
//        this.Version = "16.1";
//        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

//    }

//    #endregion
//}
