using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Collections;
using System.ComponentModel;

public partial class PrintControllers_InBaoCaoAncotrading_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
		string md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];
        rpBaoCaoBP report = new rpBaoCaoBP();
        String sql = this.CreateSql(startdate, enddate, md_doitackinhdoanh_id);
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

    // InPhieuChiTienMat
    public String CreateSql(string startdate, string enddate, string md_doitackinhdoanh_id)
    {
        LinqDBDataContext db = new LinqDBDataContext();
		
		string where_ex = "";
		if(md_doitackinhdoanh_id != null & md_doitackinhdoanh_id != "") {
			where_ex = " and cttc.md_doitackinhdoanh_id = '" + md_doitackinhdoanh_id + "'";
		}
		string str = String.Format(@"exec [dbo].[rpt_BaoCaoBP] N'{0}',N'{1}',N'{2}'", startdate, enddate, md_doitackinhdoanh_id);
        return str;
    }
}

/// <summary>
/// Summary description for rpBaoCaoBP
/// </summary>
public class rpBaoCaoBP : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell sochungtu;
    private XRTableCell ngaychi;
    private XRTableCell ten;
    private XRTableCell diengiai;
    private XRTableCell bophan;
    private XRTableCell thu;
    private XRTableCell chi;
    private XRTableCell ton;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell7;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private GroupHeaderBand GroupHeader1;
    private XRLabel tk;
    private XRLabel tungay;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel denngay;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rpBaoCaoBP()
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
            string resourceFileName = "rpBaoCaoBP.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.sochungtu = new DevExpress.XtraReports.UI.XRTableCell();
            this.ngaychi = new DevExpress.XtraReports.UI.XRTableCell();
            this.ten = new DevExpress.XtraReports.UI.XRTableCell();
            this.diengiai = new DevExpress.XtraReports.UI.XRTableCell();
            this.bophan = new DevExpress.XtraReports.UI.XRTableCell();
            this.thu = new DevExpress.XtraReports.UI.XRTableCell();
            this.chi = new DevExpress.XtraReports.UI.XRTableCell();
            this.ton = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.tungay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.denngay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tk = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(777F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.sochungtu,
            this.ngaychi,
            this.ten,
            this.diengiai,
            this.bophan,
            this.thu,
            this.chi,
            this.ton});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // sochungtu
            // 
            this.sochungtu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sochungtu")});
            this.sochungtu.Name = "sochungtu";
            this.sochungtu.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.sochungtu.StylePriority.UsePadding = false;
            this.sochungtu.StylePriority.UseTextAlignment = false;
            this.sochungtu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.sochungtu.Weight = 0.19682702318581838D;
            // 
            // ngaychi
            // 
            this.ngaychi.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaychi", "{0:dd/MM/yyyy}")});
            this.ngaychi.Name = "ngaychi";
            this.ngaychi.Weight = 0.28305473474922327D;
            // 
            // ten
            // 
            this.ten.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten")});
            this.ten.Name = "ten";
            this.ten.StylePriority.UseTextAlignment = false;
            this.ten.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ten.Weight = 0.39548345146031905D;
            // 
            // diengiai
            // 
            this.diengiai.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diengiai")});
            this.diengiai.Name = "diengiai";
            this.diengiai.StylePriority.UseTextAlignment = false;
            this.diengiai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.diengiai.Weight = 0.55764128342558539D;
            // 
            // bophan
            // 
            this.bophan.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "bophan")});
            this.bophan.Name = "bophan";
            this.bophan.StylePriority.UseTextAlignment = false;
            this.bophan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.bophan.Weight = 0.38651914854307456D;
            // 
            // thu
            // 
            this.thu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thu", "{0:#,#0.00}")});
            this.thu.Name = "thu";
            this.thu.StylePriority.UseTextAlignment = false;
            this.thu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.thu.Weight = 0.37427064166566115D;
            // 
            // chi
            // 
            this.chi.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "chi", "{0:#,#0.00}")});
            this.chi.Name = "chi";
            this.chi.StylePriority.UseTextAlignment = false;
            this.chi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.chi.Weight = 0.38573417516288588D;
            // 
            // ton
            // 
            this.ton.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ton.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ton", "{0:#,#0.00}")});
            this.ton.Name = "ton";
            this.ton.StylePriority.UseBorders = false;
            this.ton.StylePriority.UseTextAlignment = false;
            this.ton.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.ton.Weight = 0.42046954180743212D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseTextAlignment = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 299F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel4,
            this.tungay,
            this.xrLabel1,
            this.denngay});
            this.ReportHeader.HeightF = 46F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(408.8068F, 23F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(62.45041F, 23F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "đến ngày:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(307.2917F, 23F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Từ ngày:";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // tungay
            // 
            this.tungay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MM/yyyy}")});
            this.tungay.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.tungay.LocationFloat = new DevExpress.Utils.PointFloat(307.2917F, 23F);
            this.tungay.Multiline = true;
            this.tungay.Name = "tungay";
            this.tungay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tungay.SizeF = new System.Drawing.SizeF(101.5152F, 23F);
            this.tungay.StylePriority.UseFont = false;
            this.tungay.StylePriority.UseTextAlignment = false;
            this.tungay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(777.0001F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "BÁO CÁO BỘ PHẬN ANCOTRADING";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // denngay
            // 
            this.denngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MM/yyyy}")});
            this.denngay.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.denngay.LocationFloat = new DevExpress.Utils.PointFloat(471.2571F, 23F);
            this.denngay.Multiline = true;
            this.denngay.Name = "denngay";
            this.denngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.denngay.SizeF = new System.Drawing.SizeF(305.7429F, 23F);
            this.denngay.StylePriority.UseFont = false;
            this.denngay.StylePriority.UseTextAlignment = false;
            this.denngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 57.99999F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(777F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "Số";
            this.xrTableCell15.Weight = 0.19682689062877054D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "Ngày";
            this.xrTableCell8.Weight = 0.28305477893490583D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Weight = 0.39548336308895382D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Weight = 0.55764146016831584D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Weight = 0.38651914854307456D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Weight = 0.37427064166566115D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Weight = 0.38573417516288588D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.Weight = 0.42046954180743212D;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33.00001F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(777.0001F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell6,
            this.xrTableCell5,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell7});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "Chứng từ";
            this.xrTableCell4.Weight = 0.4798818168492851D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Tên";
            this.xrTableCell6.Weight = 0.39548339254607545D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "Diễn giải";
            this.xrTableCell5.Weight = 0.55764128342558561D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Bộ phận";
            this.xrTableCell1.Weight = 0.3865191485430744D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Thu";
            this.xrTableCell2.Weight = 0.37427040600868716D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Chi";
            this.xrTableCell3.Weight = 0.38573441081985987D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.Text = "Tồn";
            this.xrTableCell7.Weight = 0.42046977746440617D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001986821F, 10.00001F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(66.58344F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Tài khoản: ";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tk,
            this.xrLabel2,
            this.xrTable1,
            this.xrTable2});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("tk", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 82.99999F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // tk
            // 
            this.tk.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tk")});
            this.tk.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.tk.LocationFloat = new DevExpress.Utils.PointFloat(66.58363F, 10.00001F);
            this.tk.Multiline = true;
            this.tk.Name = "tk";
            this.tk.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tk.SizeF = new System.Drawing.SizeF(710.4165F, 23F);
            this.tk.StylePriority.UseFont = false;
            this.tk.StylePriority.UseTextAlignment = false;
            this.tk.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rpBaoCaoBP
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1});
            this.Margins = new System.Drawing.Printing.Margins(23, 27, 0, 299);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

}

    #endregion
}
