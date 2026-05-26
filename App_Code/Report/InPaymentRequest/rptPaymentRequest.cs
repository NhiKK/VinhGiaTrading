using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptPaymentRequest
/// </summary>
public class rptPaymentRequest : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel25;
    private XRLabel xrLabel24;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel26;
    private XRLabel xrLabel17;
    private XRRichText xrRichText8;
    private XRRichText xrRichText7;
    private XRRichText xrRichText6;
    private XRRichText xrRichText5;
    private XRRichText xrRichText4;
    private XRRichText xrRichText3;
    private XRRichText xrRichText2;
    private XRRichText xrRichText1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptPaymentRequest()
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptPaymentRequest));
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText3 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText4 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText5 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText6 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText7 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText8 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Dpi = 100F;
        this.Detail.HeightF = 0F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 100F;
        this.TopMargin.HeightF = 50F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 100F;
        this.BottomMargin.HeightF = 51.00001F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17,
            this.xrRichText8,
            this.xrRichText7,
            this.xrRichText6,
            this.xrRichText5,
            this.xrRichText4,
            this.xrRichText3,
            this.xrRichText2,
            this.xrRichText1,
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
        this.ReportHeader.Dpi = 100F;
        this.ReportHeader.HeightF = 855.8334F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel26
        // 
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtieninvoice", "Amount: {0} USD")});
        this.xrLabel26.Dpi = 100F;
        this.xrLabel26.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(2.384186E-05F, 301.0001F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(251.25F, 23F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "[tongtieninvoice]";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel25
        // 
        this.xrLabel25.Dpi = 100F;
        this.xrLabel25.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(0F, 809.8334F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(763.8333F, 23F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.Text = "Thank you & Best Regards";
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel24
        // 
        this.xrLabel24.Dpi = 100F;
        this.xrLabel24.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 763.7084F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(763.8334F, 23F);
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "PLEASE SEND US THE CONFIRMATION WHEN YOU RECEIVED THIS PAYMENT REQUEST";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Dpi = 100F;
        this.xrLabel16.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 370.0001F);
        this.xrLabel16.Multiline = true;
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(763.8334F, 54.25F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "Please find in attached files: copies of Invoice, Packing list & B/L for your inf" +
"ormation.\r\nWe will send you the C/O later.\r\nPlease arrange the payment to our ac" +
"count with banking details as follows:";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "giatriconlai", "Balance to be paid: {0} USD")});
        this.xrLabel15.Dpi = 100F;
        this.xrLabel15.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0.1666768F, 347.0001F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(763.8333F, 23F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "[giatriconlai]";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soinvoice", " as per Invoice no.: {0}")});
        this.xrLabel14.Dpi = 100F;
        this.xrLabel14.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(251.4167F, 301.0001F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(512.5833F, 23.00003F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "[soinvoice]";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel13
        // 
        this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "datcoc", "Deposit: {0:#,#.00} USD")});
        this.xrLabel13.Dpi = 100F;
        this.xrLabel13.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0.1666768F, 324.0001F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(763.8333F, 23F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "[datcoc]";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel12
        // 
        this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slinvoice", "Quantity: {0} SETS")});
        this.xrLabel12.Dpi = 100F;
        this.xrLabel12.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 278.0001F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(763.8334F, 23.00003F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "[slinvoice]";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sobil", "B/L: {0}")});
        this.xrLabel11.Dpi = 100F;
        this.xrLabel11.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 255F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(763.8334F, 23F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "[sobil]";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 100F;
        this.xrLabel10.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 232.0001F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(763.8334F, 23.00003F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "ETA:";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel9
        // 
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "shipmenttime", "ETD: {0}")});
        this.xrLabel9.Dpi = 100F;
        this.xrLabel9.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 209.0001F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(763.8334F, 23.00003F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "[shipmenttime]";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "noiden", "To: {0}")});
        this.xrLabel8.Dpi = 100F;
        this.xrLabel8.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 186.0001F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(763.8334F, 23.00002F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "[noiden]";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "noidi", "From: {0}")});
        this.xrLabel7.Dpi = 100F;
        this.xrLabel7.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 163F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(763.8334F, 23.00002F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "[noidi]";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tentau", "Name of Vessel: {0}")});
        this.xrLabel6.Dpi = 100F;
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 140F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(763.8334F, 23.00002F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "[tentau]";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "chungloai", "Commodity: {0}")});
        this.xrLabel5.Dpi = 100F;
        this.xrLabel5.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 117F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(763.8334F, 23.00002F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "[chungloai]";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Dpi = 100F;
        this.xrLabel4.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 93.99999F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(763.8334F, 23F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Please be advised that we shipped out for the above P/I with the following detail" +
"s:";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 100F;
        this.xrLabel3.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0.1667023F, 70.99996F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(763.8334F, 22.99999F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Dear Sirs,";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel2
        // 
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sopo", "PI#: {0}")});
        this.xrLabel2.Dpi = 100F;
        this.xrLabel2.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(2.384186E-05F, 47.99995F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(199.1667F, 23.00001F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "[sopo]";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 100F;
        this.xrLabel1.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(199.1667F, 23F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "PAYMENT REQUEST";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrRichText1
        // 
        this.xrRichText1.Dpi = 100F;
        this.xrRichText1.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0.1666768F, 439.4167F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(763.8333F, 23F);
        this.xrRichText1.StylePriority.UseFont = false;
        this.xrRichText1.StylePriority.UsePadding = false;
        // 
        // xrRichText2
        // 
        this.xrRichText2.Dpi = 100F;
        this.xrRichText2.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(0.3335063F, 462.4167F);
        this.xrRichText2.Name = "xrRichText2";
        this.xrRichText2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
        this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
        this.xrRichText2.SizeF = new System.Drawing.SizeF(763.6665F, 22.99997F);
        this.xrRichText2.StylePriority.UseFont = false;
        this.xrRichText2.StylePriority.UsePadding = false;
        // 
        // xrRichText3
        // 
        this.xrRichText3.Dpi = 100F;
        this.xrRichText3.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrRichText3.LocationFloat = new DevExpress.Utils.PointFloat(0.3335571F, 485.4167F);
        this.xrRichText3.Name = "xrRichText3";
        this.xrRichText3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
        this.xrRichText3.SerializableRtfString = resources.GetString("xrRichText3.SerializableRtfString");
        this.xrRichText3.SizeF = new System.Drawing.SizeF(763.6665F, 22.99997F);
        this.xrRichText3.StylePriority.UseFont = false;
        this.xrRichText3.StylePriority.UsePadding = false;
        // 
        // xrRichText4
        // 
        this.xrRichText4.Dpi = 100F;
        this.xrRichText4.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrRichText4.LocationFloat = new DevExpress.Utils.PointFloat(1.333415F, 508.4167F);
        this.xrRichText4.Name = "xrRichText4";
        this.xrRichText4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
        this.xrRichText4.SerializableRtfString = resources.GetString("xrRichText4.SerializableRtfString");
        this.xrRichText4.SizeF = new System.Drawing.SizeF(762.6665F, 64.25006F);
        this.xrRichText4.StylePriority.UseFont = false;
        this.xrRichText4.StylePriority.UsePadding = false;
        // 
        // xrRichText5
        // 
        this.xrRichText5.Dpi = 100F;
        this.xrRichText5.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrRichText5.LocationFloat = new DevExpress.Utils.PointFloat(0.8335876F, 588.7084F);
        this.xrRichText5.Name = "xrRichText5";
        this.xrRichText5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
        this.xrRichText5.SerializableRtfString = resources.GetString("xrRichText5.SerializableRtfString");
        this.xrRichText5.SizeF = new System.Drawing.SizeF(763.1664F, 36.33331F);
        this.xrRichText5.StylePriority.UseFont = false;
        this.xrRichText5.StylePriority.UsePadding = false;
        // 
        // xrRichText6
        // 
        this.xrRichText6.Dpi = 100F;
        this.xrRichText6.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrRichText6.LocationFloat = new DevExpress.Utils.PointFloat(1.666921F, 625.0417F);
        this.xrRichText6.Name = "xrRichText6";
        this.xrRichText6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
        this.xrRichText6.SerializableRtfString = resources.GetString("xrRichText6.SerializableRtfString");
        this.xrRichText6.SizeF = new System.Drawing.SizeF(762.333F, 53.83331F);
        this.xrRichText6.StylePriority.UseFont = false;
        this.xrRichText6.StylePriority.UsePadding = false;
        // 
        // xrRichText7
        // 
        this.xrRichText7.Dpi = 100F;
        this.xrRichText7.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrRichText7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 698.3333F);
        this.xrRichText7.Name = "xrRichText7";
        this.xrRichText7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
        this.xrRichText7.SerializableRtfString = resources.GetString("xrRichText7.SerializableRtfString");
        this.xrRichText7.SizeF = new System.Drawing.SizeF(763.6665F, 22.99997F);
        this.xrRichText7.StylePriority.UseFont = false;
        this.xrRichText7.StylePriority.UsePadding = false;
        // 
        // xrRichText8
        // 
        this.xrRichText8.Dpi = 100F;
        this.xrRichText8.Font = new System.Drawing.Font("Arial", 9.25F);
        this.xrRichText8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 721.3333F);
        this.xrRichText8.Name = "xrRichText8";
        this.xrRichText8.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
        this.xrRichText8.SerializableRtfString = resources.GetString("xrRichText8.SerializableRtfString");
        this.xrRichText8.SizeF = new System.Drawing.SizeF(763.6665F, 22.99997F);
        this.xrRichText8.StylePriority.UseFont = false;
        this.xrRichText8.StylePriority.UsePadding = false;
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 100F;
        this.xrLabel17.Font = new System.Drawing.Font("Arial", 9.75F);
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 832.8334F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(763.8333F, 23F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // rptPaymentRequest
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
        this.Margins = new System.Drawing.Printing.Margins(29, 34, 50, 51);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "16.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
