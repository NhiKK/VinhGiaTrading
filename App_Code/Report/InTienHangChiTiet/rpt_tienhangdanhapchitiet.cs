using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
public partial class rpt_tienhangdanhapchitiet : DevExpress.XtraReports.UI.XtraReport
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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
    /// 
    public rpt_tienhangdanhapchitiet()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
            string resourceFileName = "rpt_tienhangdanhapchitiet.resx";
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.sophieu = new DevExpress.XtraReports.UI.XRTableCell();
            this.ngay_giaonhan = new DevExpress.XtraReports.UI.XRTableCell();
            this.ma_sanpham = new DevExpress.XtraReports.UI.XRTableCell();
            this.sopo = new DevExpress.XtraReports.UI.XRTableCell();
            this.discount = new DevExpress.XtraReports.UI.XRTableCell();
            this.phicongPO = new DevExpress.XtraReports.UI.XRTableCell();
            this.phitruPO = new DevExpress.XtraReports.UI.XRTableCell();
            this.mota_tiengviet = new DevExpress.XtraReports.UI.XRTableCell();
            this.so_inv = new DevExpress.XtraReports.UI.XRTableCell();
            this.slthuc_nhapxuat = new DevExpress.XtraReports.UI.XRTableCell();
            this.dongia = new DevExpress.XtraReports.UI.XRTableCell();
            this.thanhtien = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.tongthanhtien_cc = new DevExpress.XtraReports.UI.XRLabel();
            this.tongdongia_cc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.tongsoluong_cc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.tungay = new DevExpress.XtraReports.UI.XRLabel();
            this.denngay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.today = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.tongdongia = new DevExpress.XtraReports.UI.XRLabel();
            this.tongthanhtien = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.tongsoluong = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.ma_dtkd = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.White;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(847.5732F, 25F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.sophieu,
            this.ngay_giaonhan,
            this.ma_sanpham,
            this.sopo,
            this.discount,
            this.phicongPO,
            this.phitruPO,
            this.mota_tiengviet,
            this.so_inv,
            this.slthuc_nhapxuat,
            this.dongia,
            this.thanhtien});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell1.Summary = xrSummary1;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.31916633605957023D;
            // 
            // sophieu
            // 
            this.sophieu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sophieu")});
            this.sophieu.Name = "sophieu";
            this.sophieu.StylePriority.UseTextAlignment = false;
            this.sophieu.Text = "Số phiếu";
            this.sophieu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.sophieu.Weight = 0.60041648864746056D;
            // 
            // ngay_giaonhan
            // 
            this.ngay_giaonhan.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngay_giaonhan", "{0:dd/MM/yyyy}")});
            this.ngay_giaonhan.Name = "ngay_giaonhan";
            this.ngay_giaonhan.StylePriority.UseTextAlignment = false;
            this.ngay_giaonhan.Text = "Ngày giao nhận";
            this.ngay_giaonhan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.ngay_giaonhan.Weight = 0.71124931335449226D;
            // 
            // ma_sanpham
            // 
            this.ma_sanpham.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
            this.ma_sanpham.Name = "ma_sanpham";
            this.ma_sanpham.StylePriority.UseTextAlignment = false;
            this.ma_sanpham.Text = "Mã Sản Phẩm";
            this.ma_sanpham.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.ma_sanpham.Weight = 0.66916778564453083D;
            // 
            // sopo
            // 
            this.sopo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sopo")});
            this.sopo.Name = "sopo";
            this.sopo.StylePriority.UseTextAlignment = false;
            this.sopo.Text = "Số PO";
            this.sopo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.sopo.Weight = 0.58281250000000018D;
            // 
            // discount
            // 
            this.discount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "discount")});
            this.discount.Name = "discount";
            this.discount.StylePriority.UseTextAlignment = false;
            this.discount.Text = "discount";
            this.discount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.discount.Weight = 0.5703125D;
            // 
            // phicongPO
            // 
            this.phicongPO.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phicongPO", "{0:#,#0.00}")});
            this.phicongPO.Name = "phicongPO";
            this.phicongPO.StylePriority.UseTextAlignment = false;
            this.phicongPO.Text = "Phí cộng";
            this.phicongPO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.phicongPO.Weight = 0.54687538692577575D;
            // 
            // phitruPO
            // 
            this.phitruPO.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phitruPO", "{0:#,#0.00}")});
            this.phitruPO.Name = "phitruPO";
            this.phitruPO.StylePriority.UseTextAlignment = false;
            this.phitruPO.Text = "Phí trừ";
            this.phitruPO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.phitruPO.Weight = 0.52000007146848015D;
            // 
            // mota_tiengviet
            // 
            this.mota_tiengviet.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tiengviet")});
            this.mota_tiengviet.Name = "mota_tiengviet";
            this.mota_tiengviet.StylePriority.UseTextAlignment = false;
            this.mota_tiengviet.Text = "Mô tả";
            this.mota_tiengviet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.mota_tiengviet.Weight = 0.92583452040915526D;
            // 
            // so_inv
            // 
            this.so_inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_inv")});
            this.so_inv.Name = "so_inv";
            this.so_inv.StylePriority.UseTextAlignment = false;
            this.so_inv.Text = "so_inv";
            this.so_inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.so_inv.Weight = 0.92583452040915526D;
            // 
            // slthuc_nhapxuat
            // 
            this.slthuc_nhapxuat.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "slthuc_nhapxuat", "{0:#,#}")});
            this.slthuc_nhapxuat.Name = "slthuc_nhapxuat";
            this.slthuc_nhapxuat.NullValueText = "0";
            this.slthuc_nhapxuat.StylePriority.UseTextAlignment = false;
            this.slthuc_nhapxuat.Text = "[slthuc_nhapxuat]";
            this.slthuc_nhapxuat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.slthuc_nhapxuat.Weight = 0.57926996872040981D;
            // 
            // dongia
            // 
            this.dongia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dongia", "{0:#,#}")});
            this.dongia.Name = "dongia";
            this.dongia.NullValueText = "0";
            this.dongia.StylePriority.UseTextAlignment = false;
            this.dongia.Text = "dongia";
            this.dongia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.dongia.Weight = 0.68739505730660888D;
            // 
            // thanhtien
            // 
            this.thanhtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thanhtien", "{0:#,#}")});
            this.thanhtien.Name = "thanhtien";
            this.thanhtien.NullValueText = "0";
            this.thanhtien.StylePriority.UseTextAlignment = false;
            this.thanhtien.Text = "thanhtien";
            this.thanhtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.thanhtien.Weight = 0.83739814142208191D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 23F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tongthanhtien_cc,
            this.tongdongia_cc,
            this.xrLabel8,
            this.tongsoluong_cc,
            this.xrLabel12});
            this.ReportFooter.HeightF = 24.04162F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // tongthanhtien_cc
            // 
            this.tongthanhtien_cc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tongthanhtien_cc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tongthanhtien_cc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtiencc", "{0:#,#}")});
            this.tongthanhtien_cc.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tongthanhtien_cc.LocationFloat = new DevExpress.Utils.PointFloat(763.8333F, 0F);
            this.tongthanhtien_cc.Name = "tongthanhtien_cc";
            this.tongthanhtien_cc.NullValueText = "0";
            this.tongthanhtien_cc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tongthanhtien_cc.SizeF = new System.Drawing.SizeF(83.73981F, 23F);
            this.tongthanhtien_cc.StylePriority.UseBackColor = false;
            this.tongthanhtien_cc.StylePriority.UseBorders = false;
            this.tongthanhtien_cc.StylePriority.UseFont = false;
            this.tongthanhtien_cc.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:#,#}";
            this.tongthanhtien_cc.Summary = xrSummary2;
            this.tongthanhtien_cc.Text = "tongthanhtien_cc";
            this.tongthanhtien_cc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // tongdongia_cc
            // 
            this.tongdongia_cc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tongdongia_cc.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.tongdongia_cc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongdongiacc", "{0:#,#}")});
            this.tongdongia_cc.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tongdongia_cc.LocationFloat = new DevExpress.Utils.PointFloat(695.0939F, 0F);
            this.tongdongia_cc.Name = "tongdongia_cc";
            this.tongdongia_cc.NullValueText = "0";
            this.tongdongia_cc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tongdongia_cc.SizeF = new System.Drawing.SizeF(68.7395F, 23F);
            this.tongdongia_cc.StylePriority.UseBackColor = false;
            this.tongdongia_cc.StylePriority.UseBorders = false;
            this.tongdongia_cc.StylePriority.UseFont = false;
            this.tongdongia_cc.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,#}";
            this.tongdongia_cc.Summary = xrSummary3;
            this.tongdongia_cc.Text = "tongdongia_cc";
            this.tongdongia_cc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(400F, 23F);
            this.xrLabel8.StylePriority.UseBackColor = false;
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseForeColor = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Tổng công cuối cùng:";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // tongsoluong_cc
            // 
            this.tongsoluong_cc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tongsoluong_cc.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.tongsoluong_cc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongsoluongcc", "{0:#,#}")});
            this.tongsoluong_cc.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tongsoluong_cc.LocationFloat = new DevExpress.Utils.PointFloat(637.1671F, 0F);
            this.tongsoluong_cc.Name = "tongsoluong_cc";
            this.tongsoluong_cc.NullValueText = "0";
            this.tongsoluong_cc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tongsoluong_cc.SizeF = new System.Drawing.SizeF(57.92688F, 23F);
            this.tongsoluong_cc.StylePriority.UseBackColor = false;
            this.tongsoluong_cc.StylePriority.UseBorders = false;
            this.tongsoluong_cc.StylePriority.UseFont = false;
            this.tongsoluong_cc.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:#,#}";
            this.tongsoluong_cc.Summary = xrSummary4;
            this.tongsoluong_cc.Text = "tongsoluong_cc";
            this.tongsoluong_cc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.xrLabel12.BorderColor = System.Drawing.Color.Black;
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(400F, 0F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(237.1669F, 23F);
            this.xrLabel12.StylePriority.UseBackColor = false;
            this.xrLabel12.StylePriority.UseBorderColor = false;
            this.xrLabel12.StylePriority.UseBorders = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabel6,
            this.tungay,
            this.denngay,
            this.xrLabel9,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel3,
            this.today,
            this.xrLabel4});
            this.ReportHeader.HeightF = 191.6667F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel7
            // 
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(200F, 168.4584F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Từ ngày";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(583.1666F, 134.8333F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(130.2083F, 23F);
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Núi Thành, ngày";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // tungay
            // 
            this.tungay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MM/yyyy}")});
            this.tungay.LocationFloat = new DevExpress.Utils.PointFloat(300F, 168.4584F);
            this.tungay.Name = "tungay";
            this.tungay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tungay.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.tungay.StylePriority.UseTextAlignment = false;
            this.tungay.Text = "[tungay]";
            this.tungay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // denngay
            // 
            this.denngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MM/yyyy}")});
            this.denngay.LocationFloat = new DevExpress.Utils.PointFloat(560.4167F, 168.6667F);
            this.denngay.Name = "denngay";
            this.denngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.denngay.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.denngay.StylePriority.UseTextAlignment = false;
            this.denngay.Text = "[denngay]";
            this.denngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(460.4167F, 168.6667F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Đến ngày";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 37.58334F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(850F, 23F);
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(850.0001F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 67.00001F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(850F, 23F);
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // today
            // 
            this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
            this.today.LocationFloat = new DevExpress.Utils.PointFloat(713.3749F, 134.8333F);
            this.today.Name = "today";
            this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.today.SizeF = new System.Drawing.SizeF(136.6247F, 23F);
            this.today.StylePriority.UseTextAlignment = false;
            this.today.Text = "[today]";
            this.today.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 111.8333F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(850F, 23.00001F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "GIÁ TRỊ TIỀN HÀNG ĐÃ NHẬP CHI TIẾT";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.tongdongia,
            this.tongthanhtien,
            this.xrLabel5,
            this.tongsoluong});
            this.GroupFooter1.HeightF = 23.95833F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrLabel10
            // 
            this.xrLabel10.BackColor = System.Drawing.Color.SkyBlue;
            this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(400F, 0F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(237.1669F, 23F);
            this.xrLabel10.StylePriority.UseBackColor = false;
            this.xrLabel10.StylePriority.UseBorders = false;
            // 
            // tongdongia
            // 
            this.tongdongia.BackColor = System.Drawing.Color.SkyBlue;
            this.tongdongia.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.tongdongia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongdongia", "{0:#,#}")});
            this.tongdongia.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tongdongia.LocationFloat = new DevExpress.Utils.PointFloat(695.0939F, 0F);
            this.tongdongia.Name = "tongdongia";
            this.tongdongia.NullValueText = "0";
            this.tongdongia.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tongdongia.SizeF = new System.Drawing.SizeF(68.7395F, 23F);
            this.tongdongia.StylePriority.UseBackColor = false;
            this.tongdongia.StylePriority.UseBorders = false;
            this.tongdongia.StylePriority.UseFont = false;
            this.tongdongia.StylePriority.UseTextAlignment = false;
            xrSummary5.FormatString = "{0:#,#}";
            this.tongdongia.Summary = xrSummary5;
            this.tongdongia.Text = "tongdongia";
            this.tongdongia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // tongthanhtien
            // 
            this.tongthanhtien.BackColor = System.Drawing.Color.SkyBlue;
            this.tongthanhtien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tongthanhtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtien", "{0:#,#}")});
            this.tongthanhtien.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tongthanhtien.LocationFloat = new DevExpress.Utils.PointFloat(763.8334F, 0F);
            this.tongthanhtien.Name = "tongthanhtien";
            this.tongthanhtien.NullValueText = "0";
            this.tongthanhtien.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tongthanhtien.SizeF = new System.Drawing.SizeF(83.73975F, 23F);
            this.tongthanhtien.StylePriority.UseBackColor = false;
            this.tongthanhtien.StylePriority.UseBorders = false;
            this.tongthanhtien.StylePriority.UseFont = false;
            this.tongthanhtien.StylePriority.UseTextAlignment = false;
            xrSummary6.FormatString = "{0:#,#}";
            this.tongthanhtien.Summary = xrSummary6;
            this.tongthanhtien.Text = "tongthanhtien";
            this.tongthanhtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.BackColor = System.Drawing.Color.SkyBlue;
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(400F, 23F);
            this.xrLabel5.StylePriority.UseBackColor = false;
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Tổng công:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // tongsoluong
            // 
            this.tongsoluong.BackColor = System.Drawing.Color.SkyBlue;
            this.tongsoluong.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.tongsoluong.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongsoluong", "{0:#,#}")});
            this.tongsoluong.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tongsoluong.LocationFloat = new DevExpress.Utils.PointFloat(637.1669F, 0F);
            this.tongsoluong.Name = "tongsoluong";
            this.tongsoluong.NullValueText = "0";
            this.tongsoluong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tongsoluong.SizeF = new System.Drawing.SizeF(57.92706F, 23F);
            this.tongsoluong.StylePriority.UseBackColor = false;
            this.tongsoluong.StylePriority.UseBorders = false;
            this.tongsoluong.StylePriority.UseFont = false;
            this.tongsoluong.StylePriority.UseTextAlignment = false;
            xrSummary7.FormatString = "{0:#,#}";
            this.tongsoluong.Summary = xrSummary7;
            this.tongsoluong.Text = "tongsoluong";
            this.tongsoluong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 14.58333F;
            this.PageHeader.Name = "PageHeader";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrLabel11,
            this.ma_dtkd});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("ma_dtkd", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 96.45834F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.AliceBlue;
            this.xrTable2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 44.79167F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(847.5731F, 51.66667F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell10,
            this.xrTableCell9,
            this.xrTableCell8,
            this.xrTableCell6,
            this.xrTableCell3,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell7,
            this.xrTableCell13,
            this.xrTableCell2,
            this.xrTableCell14,
            this.xrTableCell4});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "STT";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.31916633605957023D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Số phiếu";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.60041648864746056D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Ngày giao\r\nnhận";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.71124946594238292D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Mã SP";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.66916778564453061D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Số PO";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 0.58281250000000018D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Discount";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.5703125D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Phí cộng PO";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 0.54687499895564D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Phí trừ PO";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 0.52000000092366716D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BackColor = System.Drawing.Color.AliceBlue;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBackColor = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Mô tả";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.92583433223654787D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BackColor = System.Drawing.Color.AliceBlue;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBackColor = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "Số INV";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.92583433223654787D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Số lượng";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.57927007354900517D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "Đơn giá";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell14.Weight = 0.68739559192151312D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Thành tiền";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 0.837396809282659D;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21.79166F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(132.2917F, 23F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Nhà cung ứng:";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ma_dtkd
            // 
            this.ma_dtkd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
            this.ma_dtkd.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ma_dtkd.LocationFloat = new DevExpress.Utils.PointFloat(132.2917F, 21.79166F);
            this.ma_dtkd.Name = "ma_dtkd";
            this.ma_dtkd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ma_dtkd.SizeF = new System.Drawing.SizeF(717.7079F, 23F);
            this.ma_dtkd.StylePriority.UseFont = false;
            this.ma_dtkd.StylePriority.UseTextAlignment = false;
            this.ma_dtkd.Text = "[ma_dtkd]";
            this.ma_dtkd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rpt_tienhangdanhapchitiet
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter,
            this.ReportHeader,
            this.GroupFooter1,
            this.PageHeader,
            this.GroupHeader1});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 23, 100);
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
    private DevExpress.XtraReports.UI.XRLabel tongthanhtien_cc;
    private DevExpress.XtraReports.UI.XRLabel tongdongia_cc;
    private DevExpress.XtraReports.UI.XRLabel xrLabel8;
    private DevExpress.XtraReports.UI.XRLabel tongsoluong_cc;
    private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
    private DevExpress.XtraReports.UI.XRLabel xrLabel7;
    private DevExpress.XtraReports.UI.XRLabel xrLabel6;
    private DevExpress.XtraReports.UI.XRLabel tungay;
    private DevExpress.XtraReports.UI.XRLabel denngay;
    private DevExpress.XtraReports.UI.XRLabel xrLabel9;
    private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    private DevExpress.XtraReports.UI.XRLabel xrLabel1;
    private DevExpress.XtraReports.UI.XRLabel xrLabel3;
    private DevExpress.XtraReports.UI.XRLabel today;
    private DevExpress.XtraReports.UI.XRLabel xrLabel4;
    private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
    private DevExpress.XtraReports.UI.XRLabel tongdongia;
    private DevExpress.XtraReports.UI.XRLabel tongthanhtien;
    private DevExpress.XtraReports.UI.XRLabel xrLabel5;
    private PageHeaderBand PageHeader;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell sophieu;
    private XRTableCell ngay_giaonhan;
    private XRTableCell ma_sanpham;
    private XRTableCell sopo;
    private XRTableCell phicongPO;
    private XRTableCell phitruPO;
    private XRTableCell mota_tiengviet;
    private XRTableCell slthuc_nhapxuat;
    private XRTableCell dongia;
    private XRTableCell thanhtien;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell14;
    private XRLabel xrLabel11;
    private XRLabel ma_dtkd;
    private XRLabel xrLabel12;
    private XRLabel xrLabel10;
    private XRTableCell discount;
    private XRTableCell xrTableCell3;
    private XRTableCell so_inv;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell4;
    private DevExpress.XtraReports.UI.XRLabel tongsoluong;
}

