using System;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_More_rpt_xemnanglucconlaiNCU : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        DateTime datenow = DateTime.Now;
        String md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];

        rpt_xemnanglucconlaiNCU report = new rpt_xemnanglucconlaiNCU();
        String sql = this.CreateSql(Request.QueryString["startdate"], Request.QueryString["enddate"], datenow, md_doitackinhdoanh_id);
        this.viewReport(report, sql);
    }

    public void viewReport(XtraReport report, String SqlQuery)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        da.SelectCommand.CommandTimeout = 50000;
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }

    public String CreateSql(string dateFrom, string dateTo, DateTime datenow, string dtkd)
    {
        if(!string.IsNullOrEmpty(dtkd))
        {
            dtkd = string.Format("'{0}'", dtkd);
        }
        else
        {
            dtkd = "null";
        }

        String str = String.Format(@"
          declare @startdate datetime = convert(datetime, '{0}', 103)
          declare @enddate datetime = convert(datetime, '{1}', 103)

          exec [dbo].[rpt_xemnanglucsanxuatconlai] @startdate, @enddate, {2}
        ", dateFrom, dateTo, dtkd);
        return str;
    }

    #region Report
    public partial class rpt_xemnanglucconlaiNCU : DevExpress.XtraReports.UI.XtraReport
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
        public rpt_xemnanglucconlaiNCU()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.nhomhang = new DevExpress.XtraReports.UI.XRTableCell();
            this.mota_tiengviet = new DevExpress.XtraReports.UI.XRTableCell();
            this.nangxuat_tb = new DevExpress.XtraReports.UI.XRTableCell();
            this.sl_dadat = new DevExpress.XtraReports.UI.XRTableCell();
            this.sl_conlai = new DevExpress.XtraReports.UI.XRTableCell();
            this.thoigianlamhang = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.tutuan = new DevExpress.XtraReports.UI.XRLabel();
            this.dentuan = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.today = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.tuanthu = new DevExpress.XtraReports.UI.XRLabel();
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
            this.xrTable1.SizeF = new System.Drawing.SizeF(849.9996F, 25F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.nhomhang,
            this.mota_tiengviet,
            this.nangxuat_tb,
            this.sl_dadat,
            this.sl_conlai,
            this.thoigianlamhang});
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
            this.xrTableCell1.Weight = 0.53583299589646494D;
            // 
            // nhomhang
            // 
            this.nhomhang.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nhomhang")});
            this.nhomhang.Name = "nhomhang";
            this.nhomhang.StylePriority.UseTextAlignment = false;
            this.nhomhang.Text = "Số phiếu";
            this.nhomhang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.nhomhang.Weight = 0.78708399746064894D;
            // 
            // mota_tiengviet
            // 
            this.mota_tiengviet.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tiengviet")});
            this.mota_tiengviet.Name = "mota_tiengviet";
            this.mota_tiengviet.StylePriority.UseTextAlignment = false;
            this.mota_tiengviet.Text = "Số PO";
            this.mota_tiengviet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.mota_tiengviet.Weight = 3.1970830306722378D;
            // 
            // nangxuat_tb
            // 
            this.nangxuat_tb.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nangxuat_tb")});
            this.nangxuat_tb.Name = "nangxuat_tb";
            this.nangxuat_tb.StylePriority.UseTextAlignment = false;
            this.nangxuat_tb.Text = "nangxuat_tb";
            this.nangxuat_tb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.nangxuat_tb.Weight = 0.92583651769735464D;
            // 
            // sl_dadat
            // 
            this.sl_dadat.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_dadat", "{0:#,#0.000}")});
            this.sl_dadat.Name = "sl_dadat";
            this.sl_dadat.StylePriority.UseTextAlignment = false;
            this.sl_dadat.Text = "Phí cộng";
            this.sl_dadat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.sl_dadat.Weight = 0.824164458252421D;
            // 
            // sl_conlai
            // 
            this.sl_conlai.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl_conlai", "{0:#,#0.000}")});
            this.sl_conlai.Name = "sl_conlai";
            this.sl_conlai.StylePriority.UseTextAlignment = false;
            this.sl_conlai.Text = "Phí trừ";
            this.sl_conlai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.sl_conlai.Weight = 0.86374743103844431D;
            // 
            // thoigianlamhang
            // 
            this.thoigianlamhang.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thoigianlamhang")});
            this.thoigianlamhang.Name = "thoigianlamhang";
            this.thoigianlamhang.StylePriority.UseTextAlignment = false;
            this.thoigianlamhang.Text = "Mô tả";
            this.thoigianlamhang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.thoigianlamhang.Weight = 1.3662477666714059D;
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
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrLabel7,
            this.xrLabel6,
            this.tutuan,
            this.dentuan,
            this.xrLabel9,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel3,
            this.today,
            this.xrLabel4});
            this.ReportHeader.HeightF = 275.0001F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.AliceBlue;
            this.xrTable2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 223.3334F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(849.9996F, 51.66667F);
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
            this.xrTableCell11});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "STT";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.53583298137094049D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Nhóm hàng";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.78708404892181993D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Mô tả";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 3.1970827951775664D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Năng suất";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.92583343988708888D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "SL đã đặt";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 0.8241668746576416D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "SL còn lại";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.86374878481711437D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Số ngày nhận hàng";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 1.366246962612176D;
            // 
            // xrLabel7
            // 
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(200F, 168.4584F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Từ tuần";
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
            // tutuan
            // 
            this.tutuan.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tutuan")});
            this.tutuan.LocationFloat = new DevExpress.Utils.PointFloat(300F, 168.4584F);
            this.tutuan.Name = "tutuan";
            this.tutuan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tutuan.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.tutuan.StylePriority.UseTextAlignment = false;
            this.tutuan.Text = "[tutuan]";
            this.tutuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // dentuan
            // 
            this.dentuan.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dentuan")});
            this.dentuan.LocationFloat = new DevExpress.Utils.PointFloat(560.4167F, 168.6667F);
            this.dentuan.Name = "dentuan";
            this.dentuan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dentuan.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.dentuan.StylePriority.UseTextAlignment = false;
            this.dentuan.Text = "[dentuan]";
            this.dentuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(460.4167F, 168.6667F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Đến tuần";
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
            this.xrLabel4.Text = "XEM NĂNG LỰC SẢN XUẤT CÒN LẠI";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11,
            this.tuanthu});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("tuanthu", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 35.49998F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(53.58331F, 35.49998F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Tuần:";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // tuanthu
            // 
            this.tuanthu.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.tuanthu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tuanthu")});
            this.tuanthu.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tuanthu.LocationFloat = new DevExpress.Utils.PointFloat(53.5833F, 0F);
            this.tuanthu.Name = "tuanthu";
            this.tuanthu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tuanthu.SizeF = new System.Drawing.SizeF(796.4163F, 35.49998F);
            this.tuanthu.StylePriority.UseBorders = false;
            this.tuanthu.StylePriority.UseFont = false;
            this.tuanthu.StylePriority.UseTextAlignment = false;
            this.tuanthu.Text = "[tuanthu]";
            this.tuanthu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rpt_xemnanglucconlaiNCU
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 23, 0);
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel tutuan;
        private DevExpress.XtraReports.UI.XRLabel dentuan;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel today;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell nhomhang;
        private XRTableCell mota_tiengviet;
        private XRTableCell sl_dadat;
        private XRTableCell sl_conlai;
        private XRTableCell thoigianlamhang;
        private XRLabel xrLabel11;
        private XRLabel tuanthu;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell11;
        private XRTableCell nangxuat_tb;
    }
    #endregion
}