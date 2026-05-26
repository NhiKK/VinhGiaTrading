using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

    public partial class rpt_soducuoiky : DevExpress.XtraReports.UI.XtraReport
    {
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;
        private ReportHeaderBand ReportHeader;
        private XRLabel xrLabel3;
        private XRLabel xrLabel2;
        private XRLabel xrLabel4;
        private XRLabel today;
        private XRLabel xrLabel6;
        private XRLabel denngay;
        private XRLabel xrLabel9;
        private XRLabel xrLabel8;
        private XRLabel tungay;
        private PageHeaderBand PageHeader;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell3;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell6;
        private XRTableCell ten_dtkd;
        private XRTableCell sddk;
        private XRTableCell pstk;
        private XRTableCell sdck;
        private ReportFooterBand ReportFooter;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell11;
        private XRTableCell sum_sdck;
        private XRLabel xrLabel1;
    
        public rpt_soducuoiky()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ten_dtkd = new DevExpress.XtraReports.UI.XRTableCell();
            this.sddk = new DevExpress.XtraReports.UI.XRTableCell();
            this.pstk = new DevExpress.XtraReports.UI.XRTableCell();
            this.sdck = new DevExpress.XtraReports.UI.XRTableCell();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.denngay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.tungay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.today = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.sum_sdck = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 0F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.detailBand1.HeightF = 25F;
            this.detailBand1.Name = "detailBand1";
            // 
            // xrTable2
            // 
            this.xrTable2.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.BorderWidth = 2;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(650F, 25F);
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.ten_dtkd,
            this.sddk,
            this.pstk,
            this.sdck});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell6.Summary = xrSummary1;
            this.xrTableCell6.Text = "STT";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 1D;
            // 
            // ten_dtkd
            // 
            this.ten_dtkd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_dtkd")});
            this.ten_dtkd.Name = "ten_dtkd";
            this.ten_dtkd.StylePriority.UseTextAlignment = false;
            this.ten_dtkd.Text = "[ten_dtkd]";
            this.ten_dtkd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ten_dtkd.Weight = 1D;
            // 
            // sddk
            // 
            this.sddk.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sddk", "{0:#,#0.00}")});
            this.sddk.Name = "sddk";
            this.sddk.StylePriority.UseTextAlignment = false;
            this.sddk.Text = "[sddk]";
            this.sddk.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.sddk.Weight = 1D;
            // 
            // pstk
            // 
            this.pstk.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pstk", "{0:#,#0.00}")});
            this.pstk.Name = "pstk";
            this.pstk.StylePriority.UseTextAlignment = false;
            this.pstk.Text = "[pstk]";
            this.pstk.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.pstk.Weight = 1.4895834350585937D;
            // 
            // sdck
            // 
            this.sdck.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sdck", "{0:#,#0.00}")});
            this.sdck.Name = "sdck";
            this.sdck.StylePriority.UseTextAlignment = false;
            this.sdck.Text = "[sdck]";
            this.sdck.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.sdck.Weight = 2.0104165649414063D;
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.denngay,
            this.xrLabel9,
            this.xrLabel8,
            this.tungay,
            this.xrLabel6,
            this.today,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
            this.ReportHeader.HeightF = 148F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // denngay
            // 
            this.denngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MM/yyyy}")});
            this.denngay.LocationFloat = new DevExpress.Utils.PointFloat(448.9583F, 123F);
            this.denngay.Name = "denngay";
            this.denngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.denngay.SizeF = new System.Drawing.SizeF(201.0417F, 23F);
            this.denngay.Text = "[denngay]";
            // 
            // xrLabel9
            // 
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(327.0833F, 122.875F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(121.875F, 23.00001F);
            this.xrLabel9.Text = "đến ngày:";
            // 
            // xrLabel8
            // 
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(127.0833F, 122.875F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel8.Text = "từ ngày:";
            // 
            // tungay
            // 
            this.tungay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MM/yyyy}")});
            this.tungay.LocationFloat = new DevExpress.Utils.PointFloat(227.0833F, 123F);
            this.tungay.Name = "tungay";
            this.tungay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tungay.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.tungay.Text = "[tungay]";
            // 
            // xrLabel6
            // 
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(327.0833F, 100F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(121.875F, 23F);
            this.xrLabel6.Text = "Núi Thành, ngày:";
            // 
            // today
            // 
            this.today.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "today", "{0:dd/MM/yyyy}")});
            this.today.LocationFloat = new DevExpress.Utils.PointFloat(448.9583F, 100F);
            this.today.Name = "today";
            this.today.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.today.SizeF = new System.Drawing.SizeF(201.0417F, 23F);
            this.today.Text = "[today]";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 68.99995F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(650F, 31.00004F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "SỐ DƯ CUỐI KỲ";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 45.99997F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(650F, 23F);
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.99999F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(650F, 23F);
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(650F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.BackColor = System.Drawing.Color.SkyBlue;
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseBackColor = false;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.xrTable1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.BorderWidth = 2;
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.ForeColor = System.Drawing.Color.White;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(650F, 25F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseBorderWidth = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseForeColor = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Khách hàng";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Số dư đầu kỳ";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Phát sinh trong kỳ";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell5.Weight = 1.4895834350585937D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Số dư cuối kỳ";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 2.0104165649414063D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.ReportFooter.HeightF = 28.125F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(649.9999F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.sum_sdck});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Tổng cộng:";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell11.Weight = 2.072115790463096D;
            // 
            // sum_sdck
            // 
            this.sum_sdck.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sdck", "{0:#,#0.00}")});
            this.sum_sdck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sum_sdck.Name = "sum_sdck";
            this.sum_sdck.StylePriority.UseFont = false;
            this.sum_sdck.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:#,#.00}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.sum_sdck.Summary = xrSummary2;
            this.sum_sdck.Text = "Sum(sdck)";
            this.sum_sdck.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.sum_sdck.Weight = 0.92788420953690376D;
            // 
            // rpt_soducuoiky
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 0, 100);
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

    }
