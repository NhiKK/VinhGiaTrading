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

public partial class PrintControllers_InPhieuNhapHang_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String id = Context.Request.QueryString["id"];
        rptInDonHang report = new rptInDonHang();
        String sql = this.CreateSql(id);
        this.viewReport(report, sql);
		//Response.Write(sql);
    }

    public void viewReport(XtraReport report, String SqlQuery)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }

    public String CreateSql(String id) 
    {
		HttpContext context = HttpContext.Current;
		LinqDBDataContext db = new LinqDBDataContext();

		string doitackinhdoanh_id = context.Request.QueryString["doitackinhdoanh_id"];
		string tu = context.Request.QueryString["startdate"];
		string den = context.Request.QueryString["enddate"];
		
		string dtkd = "";
		if(doitackinhdoanh_id != "NULL")
		{
			dtkd = "and cdh.md_nguoilienhe_id = '" + doitackinhdoanh_id +"'";
		}
		
		string str = String.Format(@"
            declare @tungay datetime = convert(datetime, '{0} 00:00:00', 103);
            declare @denngay datetime = convert(datetime, '{1} 23:59:59', 103);

			select
				convert(varchar(10),getdate(),103) as ngaylap, tmp.so_pkl as inv, tmp.sochungtu as po, tmp.dtkd as ma_kh,
				round(tmp.hoahongphaitra,2) as hoahong,
				tmp.hoahong as ghichu,
				N'Từ ngày: ' + '{0}' as tungay, N'Đến ngày' + '{1}' as denngay,
				tmp.total as tongtien,
				isnull(tmp.hoahongdatra,0) as datra,
				isnull(tmp.conlaiphaitra,0) as conlai,
				tmp.tendtkd as ten_kh
			from(
				select distinct (select dbo.LayChungTuDonHang(civ.c_packinginvoice_id) ) as sochungtu, civ.so_pkl, 
					(select ma_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = cdh.md_nguoilienhe_id) as dtkd,
					(civ.totalgross) as total,
					civ.phantramhoahong as hoahong,
					(civ.totalgross * civ.phantramhoahong / 100) as hoahongphaitra,
					civ.hoahongdatra, (round(civ.totalgross * civ.phantramhoahong / 100, 2) - civ.hoahongdatra) as conlaiphaitra,
					(select ten_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = cdh.md_nguoilienhe_id) as tendtkd
				from c_packinginvoice civ, c_dongpklinv cdi, c_donhang cdh
				where	
					civ.c_packinginvoice_id = cdi.c_packinginvoice_id
					and cdi.c_donhang_id = cdh.c_donhang_id
					{2}
					and coalesce(cdh.hoahong, 0) > 0
					and civ.ngay_motokhai >= @tungay and civ.ngay_motokhai <= @denngay
					and civ.md_trangthai_id = 'HIEULUC' ) as tmp ", 
			tu, den, dtkd );
		return str;
    }
}

/// <summary>
/// Summary description for rptInDonHang
/// </summary>
public class rptInDonHang : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel8;
    private XRLabel xrLabel5;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell po;
    private XRTableCell ghichu;
    private XRTableCell xrTableCell4;
    private XRTableCell ma_kh;
    private XRTableCell ten_kh;
    private XRTableCell inv;
    private XRTableCell hoahong;
    private XRTableCell conlai;
    private XRTableCell tongtien;
    private XRTableCell datra;
    private XRLabel xrLabel46;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRLabel denngay;
    private XRLabel tungay;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell sum_tongtien;
    private XRTableCell sum_hoahong;
    private XRTableCell sum_datra;
    private XRTableCell sum_conlai;
    private XRTableCell xrTableCell32;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel xrLabel11;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptInDonHang()
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ma_kh = new DevExpress.XtraReports.UI.XRTableCell();
            this.ten_kh = new DevExpress.XtraReports.UI.XRTableCell();
            this.po = new DevExpress.XtraReports.UI.XRTableCell();
            this.inv = new DevExpress.XtraReports.UI.XRTableCell();
            this.tongtien = new DevExpress.XtraReports.UI.XRTableCell();
            this.hoahong = new DevExpress.XtraReports.UI.XRTableCell();
            this.datra = new DevExpress.XtraReports.UI.XRTableCell();
            this.conlai = new DevExpress.XtraReports.UI.XRTableCell();
            this.ghichu = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.denngay = new DevExpress.XtraReports.UI.XRLabel();
            this.tungay = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.sum_tongtien = new DevExpress.XtraReports.UI.XRTableCell();
            this.sum_hoahong = new DevExpress.XtraReports.UI.XRTableCell();
            this.sum_datra = new DevExpress.XtraReports.UI.XRTableCell();
            this.sum_conlai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 42.08337F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.BorderColor = System.Drawing.Color.Silver;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(827.0001F, 42.08337F);
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.ma_kh,
            this.ten_kh,
            this.po,
            this.inv,
            this.tongtien,
            this.hoahong,
            this.datra,
            this.conlai,
            this.ghichu});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell4.Summary = xrSummary1;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 0.11301915520190828D;
            // 
            // ma_kh
            // 
            this.ma_kh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ma_kh.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_kh")});
            this.ma_kh.Name = "ma_kh";
            this.ma_kh.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ma_kh.StylePriority.UseBorders = false;
            this.ma_kh.StylePriority.UsePadding = false;
            this.ma_kh.StylePriority.UseTextAlignment = false;
            this.ma_kh.Text = "[ma_kh]";
            this.ma_kh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ma_kh.Weight = 0.27786826251941721D;
            // 
            // ten_kh
            // 
            this.ten_kh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ten_kh.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_kh")});
            this.ten_kh.Multiline = true;
            this.ten_kh.Name = "ten_kh";
            this.ten_kh.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ten_kh.StylePriority.UseBorders = false;
            this.ten_kh.StylePriority.UsePadding = false;
            this.ten_kh.StylePriority.UseTextAlignment = false;
            this.ten_kh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ten_kh.Weight = 0.49199550072969123D;
            // 
            // po
            // 
            this.po.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.po.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "po")});
            this.po.Name = "po";
            this.po.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.po.StylePriority.UseBorders = false;
            this.po.StylePriority.UsePadding = false;
            this.po.StylePriority.UseTextAlignment = false;
            this.po.Text = "[po]";
            this.po.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.po.Weight = 0.33808336833943481D;
            // 
            // inv
            // 
            this.inv.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.inv.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "inv")});
            this.inv.Name = "inv";
            this.inv.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.inv.StylePriority.UseBorders = false;
            this.inv.StylePriority.UsePadding = false;
            this.inv.StylePriority.UseTextAlignment = false;
            this.inv.Text = "[inv]";
            this.inv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.inv.Weight = 0.29901133090096293D;
            // 
            // tongtien
            // 
            this.tongtien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tongtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtien", "{0:f}")});
            this.tongtien.Name = "tongtien";
            this.tongtien.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tongtien.StylePriority.UseBorders = false;
            this.tongtien.StylePriority.UsePadding = false;
            this.tongtien.StylePriority.UseTextAlignment = false;
            this.tongtien.Text = "tongtien";
            this.tongtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tongtien.Weight = 0.24616151758338012D;
            // 
            // hoahong
            // 
            this.hoahong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.hoahong.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "hoahong", "{0:f}")});
            this.hoahong.Name = "hoahong";
            this.hoahong.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.hoahong.StylePriority.UseBorders = false;
            this.hoahong.StylePriority.UsePadding = false;
            this.hoahong.StylePriority.UseTextAlignment = false;
            this.hoahong.Text = "hoahong";
            this.hoahong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hoahong.Weight = 0.24616150379407303D;
            // 
            // datra
            // 
            this.datra.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.datra.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "datra", "{0:f}")});
            this.datra.Name = "datra";
            this.datra.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.datra.StylePriority.UseBorders = false;
            this.datra.StylePriority.UsePadding = false;
            this.datra.StylePriority.UseTextAlignment = false;
            this.datra.Text = "datra";
            this.datra.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.datra.Weight = 0.24616170119677355D;
            // 
            // conlai
            // 
            this.conlai.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.conlai.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "conlai", "{0:f}")});
            this.conlai.Name = "conlai";
            this.conlai.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.conlai.StylePriority.UseBorders = false;
            this.conlai.StylePriority.UsePadding = false;
            this.conlai.StylePriority.UseTextAlignment = false;
            this.conlai.Text = "[conlai]";
            this.conlai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.conlai.Weight = 0.24616109512112505D;
            // 
            // ghichu
            // 
            this.ghichu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ghichu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ghichu", "{0:#.0}" + "%")});
            this.ghichu.Name = "ghichu";
            this.ghichu.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ghichu.StylePriority.UseBorders = false;
            this.ghichu.StylePriority.UsePadding = false;
            this.ghichu.StylePriority.UseTextAlignment = false;
            this.ghichu.Text = "ghichu";
            this.ghichu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ghichu.Weight = 0.24854537525129777D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 12F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 26F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 98.6667F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(827.0001F, 40.70834F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "THỐNG KÊ HOA HỒNG PHẢI TRẢ";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Underline);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 76.45836F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(827.0001F, 22.20834F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Tel.:  (Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 54.25002F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(827.0001F, 22.20834F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(827.0001F, 54.25002F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaylap", "{0:dd/MMM/yyyy}")});
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(688.8959F, 139.375F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(138.1025F, 23F);
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "xrLabel8";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(575.9178F, 139.375F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(112.9781F, 23F);
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Núi Thành, ngày:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel46
            // 
            this.xrLabel46.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(0F, 39.04171F);
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel46.SizeF = new System.Drawing.SizeF(275.6667F, 26.47226F);
            this.xrLabel46.StylePriority.UseFont = false;
            this.xrLabel46.StylePriority.UseTextAlignment = false;
            this.xrLabel46.Text = "Người lập";
            this.xrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.denngay,
            this.tungay,
            this.xrLabel1,
            this.xrLabel5,
            this.xrLabel8,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel4});
            this.ReportHeader.HeightF = 198.5257F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // denngay
            // 
            this.denngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "denngay", "{0:dd/MMM/yyyy}")});
            this.denngay.LocationFloat = new DevExpress.Utils.PointFloat(505.5241F, 162.375F);
            this.denngay.Name = "denngay";
            this.denngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.denngay.SizeF = new System.Drawing.SizeF(233.5509F, 23F);
            this.denngay.StylePriority.UseTextAlignment = false;
            this.denngay.Text = "[denngay]";
            this.denngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // tungay
            // 
            this.tungay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tungay", "{0:dd/MMM/yyyy}")});
            this.tungay.LocationFloat = new DevExpress.Utils.PointFloat(269.8336F, 162.375F);
            this.tungay.Name = "tungay";
            this.tungay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tungay.SizeF = new System.Drawing.SizeF(233.5509F, 23F);
            this.tungay.StylePriority.UseTextAlignment = false;
            this.tungay.Text = "[tungay]";
            this.tungay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel9,
            this.xrTable3,
            this.xrLabel46});
            this.ReportFooter.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.ReportFooter.HeightF = 248.6806F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.StylePriority.UseBorderColor = false;
            this.ReportFooter.StylePriority.UseFont = false;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(551.3334F, 65.51395F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(275.6667F, 26.47226F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "(Ký và ghi rõ họ tên)";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(275.6667F, 65.51395F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(275.6667F, 26.47226F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "(Ký và ghi rõ họ tên)";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(3.708733E-05F, 65.51395F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(275.6667F, 26.47226F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "(Ký và ghi rõ họ tên)";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(551.3334F, 39.04171F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(275.6667F, 26.47226F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "Giám đốc";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(275.6667F, 39.04171F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(275.6667F, 26.47226F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Kế toán trưởng";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable3
            // 
            this.xrTable3.BackColor = System.Drawing.Color.Silver;
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(827.0001F, 26.0417F);
            this.xrTable3.StylePriority.UseBackColor = false;
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell22,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27,
            this.sum_tongtien,
            this.sum_hoahong,
            this.sum_datra,
            this.sum_conlai,
            this.xrTableCell32});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Weight = 0.1087881875933234D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Multiline = true;
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.StylePriority.UsePadding = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.Text = "Tổng cộng:";
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell22.Weight = 0.26746541157611192D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Multiline = true;
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell25.Weight = 0.47357622106640618D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Multiline = true;
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell26.Weight = 0.3254264418763097D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell27.Weight = 0.28781708008585571D;
            // 
            // sum_tongtien
            // 
            this.sum_tongtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtien")});
            this.sum_tongtien.Multiline = true;
            this.sum_tongtien.Name = "sum_tongtien";
            this.sum_tongtien.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.sum_tongtien.StylePriority.UsePadding = false;
            this.sum_tongtien.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.sum_tongtien.Summary = xrSummary2;
			xrSummary2.FormatString = "{0:f}";
            this.sum_tongtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.sum_tongtien.Weight = 0.23694563432471522D;
            // 
            // sum_hoahong
            // 
            this.sum_hoahong.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "hoahong")});
            this.sum_hoahong.Name = "sum_hoahong";
            this.sum_hoahong.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.sum_hoahong.StylePriority.UsePadding = false;
            this.sum_hoahong.StylePriority.UseTextAlignment = false;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.sum_hoahong.Summary = xrSummary3;
			xrSummary3.FormatString = "{0:f}";
            this.sum_hoahong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.sum_hoahong.Weight = 0.23694602093002942D;
            // 
            // sum_datra
            // 
            this.sum_datra.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "datra")});
            this.sum_datra.Name = "sum_datra";
            this.sum_datra.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.sum_datra.StylePriority.UsePadding = false;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.sum_datra.Summary = xrSummary4;
			xrSummary4.FormatString = "{0:f}";
            this.sum_datra.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.sum_datra.Weight = 0.23694601531831627D;
            // 
            // sum_conlai
            // 
            this.sum_conlai.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "conlai")});
            this.sum_conlai.Name = "sum_conlai";
            this.sum_conlai.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.sum_conlai.StylePriority.UsePadding = false;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.sum_conlai.Summary = xrSummary5;
			xrSummary5.FormatString = "{0:f}";
            this.sum_conlai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.sum_conlai.Weight = 0.23694542602808463D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Multiline = true;
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell32.Weight = 0.23924556254736079D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.HeightF = 42.08337F;
            this.GroupHeader1.KeepTogether = true;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTable2.BorderColor = System.Drawing.Color.Silver;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.ForeColor = System.Drawing.Color.White;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(3.532127E-05F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(827.0001F, 42.08337F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseForeColor = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell16,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell23,
            this.xrTableCell24});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.Text = "STT";
            this.xrTableCell14.Weight = 0.1087881875933234D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.Multiline = true;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.Text = "Mã khách hàng";
            this.xrTableCell15.Weight = 0.26746541157611192D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.Text = "Tên người hưởng HH";
            this.xrTableCell17.Weight = 0.47357622106640623D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Multiline = true;
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.Text = "Số P/O";
            this.xrTableCell18.Weight = 0.32542641742809664D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.Text = "Số Invoice";
            this.xrTableCell16.Weight = 0.28781708008585566D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell19.Multiline = true;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.Text = "Tổng tiền Invoice";
            this.xrTableCell19.Weight = 0.2369458299104199D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.Text = "Tiền hoa hồng";
            this.xrTableCell20.Weight = 0.23694582534432468D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.Text = "Đã trả";
            this.xrTableCell21.Weight = 0.23694581973261158D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.Text = "Còn lại";
            this.xrTableCell23.Weight = 0.23694581719949404D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Multiline = true;
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "Ghi chú";
            this.xrTableCell24.Weight = 0.23924539140986917D;
            // 
            // rptInDonHang
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter,
            this.ReportHeader,
            this.GroupHeader1});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 12, 26);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion
}
