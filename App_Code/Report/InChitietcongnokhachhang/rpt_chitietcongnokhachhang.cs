using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rpt_chitietcongnokhachhang
/// </summary>
public class rpt_chitietcongnokhachhang : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel9;
    private XRLabel denngay;
    private XRLabel tungay;
    private XRLabel xrLabel8;
    private XRLabel today;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell13;
    private XRTableCell sochungtu;
    private XRTableCell ngaychungtu;
    private XRTableCell noidung;
    private XRTableCell trigiainv;
    private XRTableCell thanhtoan;
    private XRTableCell ghichu;
    private XRLabel xrLabel11;
    private XRLabel ma_dtkd;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private GroupHeaderBand GroupHeader1;
    private GroupFooterBand GroupFooter1;
    private XRTableCell xrTableCell7;
    private ReportFooterBand ReportFooter;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell8;
    private XRTableCell sum_trigiainv;
    private XRTableCell sum_thanhtoan;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell20;
    private XRTableCell nocuoiky;
    private XRTableCell cocuoiky;
    private XRTableCell xrTableCell23;
    private XRTable xrTable3;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell27;
    private XRTableCell tt_tatca;
    private XRTable xrTable5;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell14;
    private XRTableCell nodauky;
    private XRTableCell codauky;
    private XRTableCell xrTableCell24;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell11;
    private XRTableCell tt_no;
    private XRTableCell tt_co;
    private XRTableCell tt_all;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell34;
    private XRLabel xrLabel6;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel10;
    private XRLabel xrLabel7;
    private XRTableCell xrTableCell9;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rpt_chitietcongnokhachhang()
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
            string resourceFileName = "XtraReport2.resx";
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.sochungtu = new DevExpress.XtraReports.UI.XRTableCell();
            this.ngaychungtu = new DevExpress.XtraReports.UI.XRTableCell();
            this.noidung = new DevExpress.XtraReports.UI.XRTableCell();
            this.trigiainv = new DevExpress.XtraReports.UI.XRTableCell();
            this.thanhtoan = new DevExpress.XtraReports.UI.XRTableCell();
            this.ghichu = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.denngay = new DevExpress.XtraReports.UI.XRLabel();
            this.tungay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.today = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.ma_dtkd = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.nodauky = new DevExpress.XtraReports.UI.XRTableCell();
            this.codauky = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.sum_trigiainv = new DevExpress.XtraReports.UI.XRTableCell();
            this.sum_thanhtoan = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.nocuoiky = new DevExpress.XtraReports.UI.XRTableCell();
            this.cocuoiky = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tt_no = new DevExpress.XtraReports.UI.XRTableCell();
            this.tt_co = new DevExpress.XtraReports.UI.XRTableCell();
            this.tt_all = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tt_tatca = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 24F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.White;
            this.xrTable2.BorderColor = System.Drawing.Color.Black;
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable2.ForeColor = System.Drawing.Color.Black;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(827.0001F, 24F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseForeColor = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.sochungtu,
            this.ngaychungtu,
            this.noidung,
            this.trigiainv,
            this.thanhtoan,
            this.ghichu});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorders = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell13.Summary = xrSummary1;
            this.xrTableCell13.Weight = 0.42921721320490069D;
            // 
            // sochungtu
            // 
            this.sochungtu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.sochungtu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sochungtu")});
            this.sochungtu.Name = "sochungtu";
            this.sochungtu.StylePriority.UseBorders = false;
            this.sochungtu.StylePriority.UseTextAlignment = false;
            this.sochungtu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.sochungtu.Weight = 0.965738951632733D;
            // 
            // ngaychungtu
            // 
            this.ngaychungtu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.ngaychungtu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaychungtu", "{0:dd/MM/yyyy}")});
            this.ngaychungtu.Name = "ngaychungtu";
            this.ngaychungtu.StylePriority.UseBorders = false;
            this.ngaychungtu.StylePriority.UseTextAlignment = false;
            this.ngaychungtu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.ngaychungtu.Weight = 0.858434351473521D;
            // 
            // noidung
            // 
            this.noidung.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.noidung.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "noidung")});
            this.noidung.Name = "noidung";
            this.noidung.StylePriority.UseBorders = false;
            this.noidung.StylePriority.UseTextAlignment = false;
            this.noidung.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.noidung.Weight = 1.6095645721555494D;
            // 
            // trigiainv
            // 
            this.trigiainv.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.trigiainv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "trigiainv", "{0:#,##0.00}")});
            this.trigiainv.Name = "trigiainv";
            this.trigiainv.StylePriority.UseBorders = false;
            this.trigiainv.StylePriority.UseTextAlignment = false;
            this.trigiainv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.trigiainv.Weight = 1.1624633684438794D;
            // 
            // thanhtoan
            // 
            this.thanhtoan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.thanhtoan.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thanhtoan", "{0:#,##0.00}")});
            this.thanhtoan.Multiline = true;
            this.thanhtoan.Name = "thanhtoan";
            this.thanhtoan.StylePriority.UseBorders = false;
            this.thanhtoan.StylePriority.UseTextAlignment = false;
            this.thanhtoan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.thanhtoan.Weight = 1.1624634366661255D;
            // 
            // ghichu
            // 
            this.ghichu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.ghichu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ghichu")});
            this.ghichu.Multiline = true;
            this.ghichu.Name = "ghichu";
            this.ghichu.StylePriority.UseBorders = false;
            this.ghichu.StylePriority.UseTextAlignment = false;
            this.ghichu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.ghichu.Weight = 1.2071748706509786D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.denngay,
            this.tungay,
            this.xrLabel8,
            this.today,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
            this.ReportHeader.HeightF = 156.8788F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(505.2974F, 133.8788F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(70.50458F, 23F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UsePadding = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Đến ngày:";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // denngay
            // 
            this.denngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MM/yyyy}")});
            this.denngay.Font = new System.Drawing.Font("Arial", 10F);
            this.denngay.LocationFloat = new DevExpress.Utils.PointFloat(575.8021F, 133.8788F);
            this.denngay.Name = "denngay";
            this.denngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.denngay.SizeF = new System.Drawing.SizeF(125.0001F, 23F);
            this.denngay.StylePriority.UseFont = false;
            this.denngay.StylePriority.UsePadding = false;
            this.denngay.StylePriority.UseTextAlignment = false;
            this.denngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // tungay
            // 
            this.tungay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MM/yyyy}")});
            this.tungay.Font = new System.Drawing.Font("Arial", 10F);
            this.tungay.LocationFloat = new DevExpress.Utils.PointFloat(212.5443F, 133.8788F);
            this.tungay.Name = "tungay";
            this.tungay.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tungay.SizeF = new System.Drawing.SizeF(125.0001F, 23F);
            this.tungay.StylePriority.UseFont = false;
            this.tungay.StylePriority.UsePadding = false;
            this.tungay.StylePriority.UseTextAlignment = false;
            this.tungay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(149.1978F, 133.8788F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(63.34647F, 23F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UsePadding = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Từ ngày:";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // today
            // 
            this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
            this.today.Font = new System.Drawing.Font("Arial", 10F);
            this.today.LocationFloat = new DevExpress.Utils.PointFloat(575.8021F, 110.8788F);
            this.today.Name = "today";
            this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.today.SizeF = new System.Drawing.SizeF(125.0001F, 23F);
            this.today.StylePriority.UseFont = false;
            this.today.StylePriority.UsePadding = false;
            this.today.StylePriority.UseTextAlignment = false;
            this.today.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(450.8019F, 110.8788F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(125.0001F, 23F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Núi Thành, ngày:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(151.7322F, 75.00001F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(482.2918F, 35.87879F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "CHI TIẾT CÔNG NỢ KHÁCH HÀNG";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(151.7322F, 50F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(482.2918F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(151.7322F, 25F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(482.2917F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(151.7322F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(482.2918F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(113.021F, 23F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UsePadding = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Mã khách hàng :";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ma_dtkd
            // 
            this.ma_dtkd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
            this.ma_dtkd.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.ma_dtkd.LocationFloat = new DevExpress.Utils.PointFloat(113.021F, 0F);
            this.ma_dtkd.Name = "ma_dtkd";
            this.ma_dtkd.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ma_dtkd.SizeF = new System.Drawing.SizeF(324.3229F, 23F);
            this.ma_dtkd.StylePriority.UseFont = false;
            this.ma_dtkd.StylePriority.UsePadding = false;
            this.ma_dtkd.StylePriority.UseTextAlignment = false;
            this.ma_dtkd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.LightGray;
            this.xrTable1.BorderColor = System.Drawing.Color.Black;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTable1.ForeColor = System.Drawing.Color.Black;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.99999F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(827.0001F, 48F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseForeColor = false;
            this.xrTable1.StylePriority.UsePadding = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell7,
            this.xrTableCell6});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.44818531873813461D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 3, 3, 100F);
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.Text = "Số chứng từ";
            this.xrTableCell2.Weight = 1.0084169430363508D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.Text = "Ngày chứng từ";
            this.xrTableCell3.Weight = 0.89637062080957308D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.Text = "Nội dung";
            this.xrTableCell4.Weight = 1.6806949151478006D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.Text = "Giá trị invoice đã xuất";
            this.xrTableCell5.Weight = 1.2138352257373177D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.Text = "Trị giá thanh toán";
            this.xrTableCell7.Weight = 1.2138352257373175D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.Text = "Ghi chú";
            this.xrTableCell6.Weight = 1.2605216166897841D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5,
            this.xrTable1,
            this.xrLabel11,
            this.ma_dtkd});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("ma_dtkd", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 94.99998F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable5
            // 
            this.xrTable5.BackColor = System.Drawing.Color.White;
            this.xrTable5.BorderColor = System.Drawing.Color.Black;
            this.xrTable5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable5.ForeColor = System.Drawing.Color.Black;
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 70.99998F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable5.SizeF = new System.Drawing.SizeF(827.0001F, 24F);
            this.xrTable5.StylePriority.UseBackColor = false;
            this.xrTable5.StylePriority.UseBorderColor = false;
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseForeColor = false;
            this.xrTable5.StylePriority.UsePadding = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.nodauky,
            this.codauky,
            this.xrTableCell24});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell14.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBackColor = false;
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "Số dư đầu kỳ";
            this.xrTableCell14.Weight = 3.862955367032685D;
            // 
            // nodauky
            // 
            this.nodauky.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.nodauky.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nodauky", "{0:#,##0.00}")});
            this.nodauky.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.nodauky.Name = "nodauky";
            this.nodauky.StylePriority.UseBorders = false;
            this.nodauky.StylePriority.UseFont = false;
            this.nodauky.StylePriority.UseTextAlignment = false;
            this.nodauky.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.nodauky.Weight = 1.1624633627668815D;
            // 
            // codauky
            // 
            this.codauky.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.codauky.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "codauky", "{0:#,##0.00}")});
            this.codauky.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.codauky.Name = "codauky";
            this.codauky.StylePriority.UseBorders = false;
            this.codauky.StylePriority.UseFont = false;
            this.codauky.StylePriority.UseTextAlignment = false;
            this.codauky.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.codauky.Weight = 1.1624634366661253D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseBorders = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell24.Weight = 1.2071745977619954D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.GroupFooter1.HeightF = 96F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable4
            // 
            this.xrTable4.BackColor = System.Drawing.Color.White;
            this.xrTable4.BorderColor = System.Drawing.Color.Black;
            this.xrTable4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable4.ForeColor = System.Drawing.Color.Black;
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow6,
            this.xrTableRow3,
            this.xrTableRow8});
            this.xrTable4.SizeF = new System.Drawing.SizeF(827.0001F, 96F);
            this.xrTable4.StylePriority.UseBackColor = false;
            this.xrTable4.StylePriority.UseBorderColor = false;
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseForeColor = false;
            this.xrTable4.StylePriority.UsePadding = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.sum_trigiainv,
            this.sum_thanhtoan,
            this.xrTableCell12});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BackColor = System.Drawing.Color.LightGray;
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBackColor = false;
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "Cộng phát sinh trong kỳ";
            this.xrTableCell8.Weight = 3.862955367032685D;
            // 
            // sum_trigiainv
            // 
            this.sum_trigiainv.BackColor = System.Drawing.Color.LightGray;
            this.sum_trigiainv.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.sum_trigiainv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sum_trigiainv", "{0:#,##0.00}")});
            this.sum_trigiainv.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.sum_trigiainv.Name = "sum_trigiainv";
            this.sum_trigiainv.StylePriority.UseBackColor = false;
            this.sum_trigiainv.StylePriority.UseBorders = false;
            this.sum_trigiainv.StylePriority.UseFont = false;
            this.sum_trigiainv.StylePriority.UseTextAlignment = false;
            this.sum_trigiainv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.sum_trigiainv.Weight = 1.1624633627668815D;
            // 
            // sum_thanhtoan
            // 
            this.sum_thanhtoan.BackColor = System.Drawing.Color.LightGray;
            this.sum_thanhtoan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.sum_thanhtoan.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sum_thanhtoan", "{0:#,##0.00}")});
            this.sum_thanhtoan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.sum_thanhtoan.Name = "sum_thanhtoan";
            this.sum_thanhtoan.StylePriority.UseBackColor = false;
            this.sum_thanhtoan.StylePriority.UseBorders = false;
            this.sum_thanhtoan.StylePriority.UseFont = false;
            this.sum_thanhtoan.StylePriority.UseTextAlignment = false;
            this.sum_thanhtoan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.sum_thanhtoan.Weight = 1.1624634366661253D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BackColor = System.Drawing.Color.LightGray;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBackColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell12.Weight = 1.2071745977619954D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.nocuoiky,
            this.cocuoiky,
            this.xrTableCell23});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.BackColor = System.Drawing.Color.LightGray;
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell20.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBackColor = false;
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Text = "Cuối kỳ";
            this.xrTableCell20.Weight = 3.8629550884667041D;
            // 
            // nocuoiky
            // 
            this.nocuoiky.BackColor = System.Drawing.Color.LightGray;
            this.nocuoiky.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.nocuoiky.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tt_no", "{0:#,##0.00}")});
            this.nocuoiky.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.nocuoiky.Name = "nocuoiky";
            this.nocuoiky.StylePriority.UseBackColor = false;
            this.nocuoiky.StylePriority.UseBorders = false;
            this.nocuoiky.StylePriority.UseFont = false;
            this.nocuoiky.StylePriority.UseTextAlignment = false;
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            this.nocuoiky.Summary = xrSummary2;
            this.nocuoiky.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.nocuoiky.Weight = 1.1624633684438794D;
            // 
            // cocuoiky
            // 
            this.cocuoiky.BackColor = System.Drawing.Color.LightGray;
            this.cocuoiky.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.cocuoiky.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tt_co", "{0:#,##0.00}")});
            this.cocuoiky.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cocuoiky.Multiline = true;
            this.cocuoiky.Name = "cocuoiky";
            this.cocuoiky.StylePriority.UseBackColor = false;
            this.cocuoiky.StylePriority.UseBorders = false;
            this.cocuoiky.StylePriority.UseFont = false;
            this.cocuoiky.StylePriority.UseTextAlignment = false;
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.cocuoiky.Summary = xrSummary3;
            this.cocuoiky.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.cocuoiky.Weight = 1.1624634366661255D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.BackColor = System.Drawing.Color.LightGray;
            this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell23.Multiline = true;
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBackColor = false;
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell23.Weight = 1.2071748706509786D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.tt_no,
            this.tt_co,
            this.tt_all});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = "Tổng cộng";
            this.xrTableCell11.Weight = 3.8629550884667041D;
            // 
            // tt_no
            // 
            this.tt_no.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tt_no.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tt_no", "{0:#,##0.00}")});
            this.tt_no.Name = "tt_no";
            this.tt_no.StylePriority.UseBorders = false;
            this.tt_no.StylePriority.UseTextAlignment = false;
            this.tt_no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.tt_no.Weight = 1.1624633684438794D;
            // 
            // tt_co
            // 
            this.tt_co.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tt_co.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tt_co", "{0:#,##0.00}")});
            this.tt_co.Name = "tt_co";
            this.tt_co.StylePriority.UseBorders = false;
            this.tt_co.StylePriority.UseTextAlignment = false;
            this.tt_co.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.tt_co.Weight = 1.1624634366661255D;
            // 
            // tt_all
            // 
            this.tt_all.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tt_all.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tt_all", "{0:#,##0.00}")});
            this.tt_all.Name = "tt_all";
            this.tt_all.StylePriority.UseBorders = false;
            this.tt_all.StylePriority.UseTextAlignment = false;
            this.tt_all.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.tt_all.Weight = 1.2071748706509786D;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell28,
            this.xrTableCell34});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell25.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseBorders = false;
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.Weight = 3.8629550884667041D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseBorders = false;
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell26.Weight = 1.1624633684438794D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseBorders = false;
            this.xrTableCell28.StylePriority.UseTextAlignment = false;
            this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell28.Weight = 1.1624634366661255D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBorders = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell34.Weight = 1.2071748706509786D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel10,
            this.xrLabel7,
            this.xrLabel6,
            this.xrTable3});
            this.ReportFooter.HeightF = 84.00002F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel16
            // 
            this.xrLabel16.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(620.2501F, 61.00002F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(206.75F, 23F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UsePadding = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "Ký và ghi rõ họ tên";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(413.5F, 61.00002F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(206.75F, 23F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UsePadding = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Ký và ghi rõ họ tên";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(206.75F, 61.00002F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(206.75F, 23F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UsePadding = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Ký và ghi rõ họ tên";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 61.00002F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(206.75F, 23F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UsePadding = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Ký và ghi rõ họ tên";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(620.2501F, 38F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(206.75F, 23F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UsePadding = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Người lập";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(413.5F, 38F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(206.75F, 23F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UsePadding = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "Thủ quỹ";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(206.7501F, 38F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(206.75F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Kế toán trưởng";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(6.357828E-05F, 38F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(206.75F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Giám đốc";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable3
            // 
            this.xrTable3.BackColor = System.Drawing.Color.White;
            this.xrTable3.BorderColor = System.Drawing.Color.Black;
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable3.ForeColor = System.Drawing.Color.Black;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(827.0001F, 24F);
            this.xrTable3.StylePriority.UseBackColor = false;
            this.xrTable3.StylePriority.UseBorderColor = false;
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseForeColor = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell9,
            this.tt_tatca});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell27.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseBorders = false;
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.Text = "Tổng cộng";
            this.xrTableCell27.Weight = 4.033667817626827D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell9.Weight = 2.427670712803502D;
            // 
            // tt_tatca
            // 
            this.tt_tatca.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tt_tatca.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tt_all", "{0:#,##0.00}")});
            this.tt_tatca.Name = "tt_tatca";
            this.tt_tatca.StylePriority.UseBorders = false;
            this.tt_tatca.StylePriority.UseTextAlignment = false;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.tt_tatca.Summary = xrSummary4;
            this.tt_tatca.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.tt_tatca.Weight = 1.2605219597970305D;
            // 
            // rpt_chitietcongnokhachhang
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.GroupFooter1,
            this.ReportFooter});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
