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

public partial class PrintControllers_InTemBaoBi_Default : System.Web.UI.Page
{
	LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        String id = Context.Request.QueryString["c_danhsachdathang_id"];
        rptInCTTemBB report = new rptInCTTemBB();
		string	sql = String.Format(@" select
				dsdh.sochungtu as dcht, dh.sochungtu as so, dtkd.ten_dtkd as doitac_kh, dtk.ten_dtkd as doitac
				, sp.ma_sanpham as ma_sanpham
				, '' as dungcho
				, ddsdh.ma_sanpham_khach as ma_khach
				, SUBSTRING(ddsdh.mota_tienganh,0,charindex(' - ',ddsdh.mota_tienganh))as mota_tienganh
				, SUBSTRING(ddsdh.mota_tienganh,charindex(' - ',ddsdh.mota_tienganh) + 3,charindex('Size',ddsdh.mota_tienganh) - 5 - charindex(' - ',ddsdh.mota_tienganh))as mausac
				, SUBSTRING(ddsdh.mota_tienganh,charindex('- Size',ddsdh.mota_tienganh)+7,len(ddsdh.mota_tienganh)+1)as kichthuoc
				, sp.ma_vach as ma_vach
				, ddsdh.sl_dathang as soluong
				, ddsdh.gianhap
				, (ddsdh.sl_dathang * ddsdh.gianhap) as thanhtien
				, dsdh.huongdanlamhang as huongdan
				, (case ddsdh.sl_inner when 0 then ' ' else (cast(dg.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner)) end) as dginner
				, (cast(ddsdh.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer)) as dgmaster
				, sp.mota as ghichu
				from c_danhsachdathang dsdh
				LEFT JOIN c_donhang dh ON dsdh.c_donhang_id = dh.c_donhang_id
				LEFT JOIN md_doitackinhdoanh dtk ON dh.md_doitackinhdoanh_id = dtk.md_doitackinhdoanh_id
				LEFT JOIN md_doitackinhdoanh dtkd ON dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
				LEFT JOIN c_dongdsdh ddsdh ON dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
				LEFT JOIN md_donggoi dg ON ddsdh.md_donggoi_id = dg.md_donggoi_id
				LEFT JOIN md_sanpham sp ON ddsdh.md_sanpham_id = sp.md_sanpham_id
				LEFT JOIN md_donvitinhsanpham dvt ON sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
				WHERE dsdh.c_danhsachdathang_id = N'{0}' order by sp.ma_sanpham asc", id);
				
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
}

/// <summary>
/// Summary description for rptInCTTemBB
/// </summary>
public class rptInCTTemBB : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel7;
    private XRLabel so;
    private XRLabel dcht;
    private XRLabel xrLabel1;
    private XRLabel xrLabel16;
    private XRLabel xrLabel9;
    private XRLabel xrLabel30;
    private XRLabel thoigian;
    private XRLabel diadiem;
    private XRLabel xrLabel34;
    private XRLabel xrLabel38;
    private XRLabel xrLabel45;
    private XRLabel xrLabel15;
    private XRLabel xrLabel13;
    private XRRichText huongdan;
    private XRLabel xrLabel22;
    private XRLabel xrLabel17;
    private XRLabel xrLabel2;
    private XRLabel xrLabel18;
    private XRLabel xrLabel19;
    private XRLabel doitac;
    private XRLabel xrLabel20;
    private XRLabel xrLabel32;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell13;
    private XRTableCell ma_sanpham;
    private XRTableCell dungcho;
    private XRTableCell ma_khach;
    private XRTableCell mota_tienganh;
    private XRTableCell mausac;
    private XRTableCell kichthuoc;
    private XRTableCell ma_vach;
    private XRTableCell dginner;
    private XRTableCell dgmaster;
    private XRTableCell soluong;
    private XRTableCell ghichu;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableCell tong_cong;
    private XRTableCell xrTableCell36;
    private XRLabel xrLabel3;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptInCTTemBB()
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
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ma_sanpham = new DevExpress.XtraReports.UI.XRTableCell();
            this.dungcho = new DevExpress.XtraReports.UI.XRTableCell();
            this.ma_khach = new DevExpress.XtraReports.UI.XRTableCell();
            this.mota_tienganh = new DevExpress.XtraReports.UI.XRTableCell();
            this.mausac = new DevExpress.XtraReports.UI.XRTableCell();
            this.kichthuoc = new DevExpress.XtraReports.UI.XRTableCell();
            this.ma_vach = new DevExpress.XtraReports.UI.XRTableCell();
            this.dginner = new DevExpress.XtraReports.UI.XRTableCell();
            this.dgmaster = new DevExpress.XtraReports.UI.XRTableCell();
            this.soluong = new DevExpress.XtraReports.UI.XRTableCell();
            this.ghichu = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.doitac = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.so = new DevExpress.XtraReports.UI.XRLabel();
            this.dcht = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tong_cong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.thoigian = new DevExpress.XtraReports.UI.XRLabel();
            this.diadiem = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.huongdan = new DevExpress.XtraReports.UI.XRRichText();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huongdan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.Detail.StylePriority.UsePadding = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1165F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.ma_sanpham,
            this.dungcho,
            this.ma_khach,
            this.mota_tienganh,
            this.mausac,
            this.kichthuoc,
            this.ma_vach,
            this.dginner,
            this.dgmaster,
            this.soluong,
            this.ghichu});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableRow2.StylePriority.UsePadding = false;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell13.Summary = xrSummary1;
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell13.Weight = 0.39724848594831963D;
            // 
            // ma_sanpham
            // 
            this.ma_sanpham.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
            this.ma_sanpham.Multiline = true;
            this.ma_sanpham.Name = "ma_sanpham";
            this.ma_sanpham.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ma_sanpham.StylePriority.UsePadding = false;
            this.ma_sanpham.Weight = 1.1289764938353351D;
            // 
            // dungcho
            // 
            this.dungcho.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dungcho")});
            this.dungcho.Name = "dungcho";
            this.dungcho.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.dungcho.StylePriority.UsePadding = false;
            this.dungcho.Weight = 0.49441098853831933D;
            // 
            // ma_khach
            // 
            this.ma_khach.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_khach")});
            this.ma_khach.Name = "ma_khach";
            this.ma_khach.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ma_khach.StylePriority.UsePadding = false;
            this.ma_khach.Weight = 1.0103656923004336D;
            // 
            // mota_tienganh
            // 
            this.mota_tienganh.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tienganh")});
            this.mota_tienganh.Multiline = true;
            this.mota_tienganh.Name = "mota_tienganh";
            this.mota_tienganh.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.mota_tienganh.StylePriority.UsePadding = false;
            this.mota_tienganh.Weight = 2.2685106088426128D;
            // 
            // mausac
            // 
            this.mausac.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mausac")});
            this.mausac.Name = "mausac";
            this.mausac.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.mausac.StylePriority.UsePadding = false;
            this.mausac.Weight = 0.90980416547266008D;
            // 
            // kichthuoc
            // 
            this.kichthuoc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "kichthuoc")});
            this.kichthuoc.Name = "kichthuoc";
            this.kichthuoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.kichthuoc.StylePriority.UsePadding = false;
            this.kichthuoc.Weight = 1.4605061693071844D;
            // 
            // ma_vach
            // 
            this.ma_vach.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_vach")});
            this.ma_vach.Name = "ma_vach";
            this.ma_vach.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ma_vach.StylePriority.UsePadding = false;
            this.ma_vach.Weight = 1.0985927884187881D;
            // 
            // dginner
            // 
            this.dginner.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dginner")});
            this.dginner.Multiline = true;
            this.dginner.Name = "dginner";
            this.dginner.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.dginner.StylePriority.UsePadding = false;
            this.dginner.Weight = 0.59973648602851659D;
            // 
            // dgmaster
            // 
            this.dgmaster.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dgmaster")});
            this.dgmaster.Name = "dgmaster";
            this.dgmaster.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.dgmaster.StylePriority.UsePadding = false;
            this.dgmaster.Weight = 0.67971218374817366D;
            // 
            // soluong
            // 
            this.soluong.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soluong", "{0:#.00}")});
            this.soluong.Name = "soluong";
            this.soluong.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.soluong.StylePriority.UsePadding = false;
            this.soluong.StylePriority.UseTextAlignment = false;
            this.soluong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.soluong.Weight = 0.79363938896251029D;
            // 
            // ghichu
            // 
            this.ghichu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.ghichu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ghichu")});
            this.ghichu.Name = "ghichu";
            this.ghichu.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ghichu.StylePriority.UseBorders = false;
            this.ghichu.StylePriority.UsePadding = false;
            this.ghichu.Weight = 1.1791326022342017D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.TopMargin.StylePriority.UsePadding = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.BottomMargin.StylePriority.UsePadding = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20,
            this.xrLabel32,
            this.xrLabel19,
            this.doitac,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel2,
            this.xrLabel9,
            this.xrLabel7,
            this.so,
            this.dcht,
            this.xrLabel1,
            this.xrLabel16});
            this.ReportHeader.HeightF = 187.2014F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ReportHeader.StylePriority.UsePadding = false;
            // 
            // xrLabel20
            // 
            this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "doitac_kh")});
            this.xrLabel20.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(79.66679F, 141.2014F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(214.0877F, 22.99998F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UsePadding = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(0F, 141.2014F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(79.66681F, 22.99998F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UsePadding = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            this.xrLabel32.Text = "Kính gởi:";
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(871.2457F, 164.2014F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(79.66681F, 22.99998F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UsePadding = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "Tên KH:";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // doitac
            // 
            this.doitac.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "doitac")});
            this.doitac.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.doitac.LocationFloat = new DevExpress.Utils.PointFloat(950.9125F, 164.2014F);
            this.doitac.Name = "doitac";
            this.doitac.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.doitac.SizeF = new System.Drawing.SizeF(214.0877F, 22.99998F);
            this.doitac.StylePriority.UseFont = false;
            this.doitac.StylePriority.UsePadding = false;
            this.doitac.StylePriority.UseTextAlignment = false;
            this.doitac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(871.2457F, 141.2014F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(79.66681F, 22.99997F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UsePadding = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "Theo số P/I:";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 53.10652F);
            this.xrLabel17.Multiline = true;
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(1165F, 26.55326F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UsePadding = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "MST: 3700318266- Tel:  0650.740.973 - Fax: 0650.740.621";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 26.55326F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1165F, 26.55326F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Khu phố 1B - An Phú - Thuận An - Binh Dương";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 164.2014F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(702.5F, 22.99997F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UsePadding = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Vui lòng xem hướng dẫn chi tiết in tem và bao bì như sau:";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.ForeColor = System.Drawing.Color.Black;
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 79.65979F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(1165F, 38.54167F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseForeColor = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "CHI TIẾT IN TEM VÀ BAO BÌ";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // so
            // 
            this.so.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "so")});
            this.so.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.so.LocationFloat = new DevExpress.Utils.PointFloat(79.6668F, 118.2015F);
            this.so.Name = "so";
            this.so.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.so.SizeF = new System.Drawing.SizeF(214.0877F, 22.99998F);
            this.so.StylePriority.UseFont = false;
            this.so.StylePriority.UsePadding = false;
            this.so.StylePriority.UseTextAlignment = false;
            this.so.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // dcht
            // 
            this.dcht.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dcht")});
            this.dcht.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.dcht.LocationFloat = new DevExpress.Utils.PointFloat(950.9125F, 141.2014F);
            this.dcht.Name = "dcht";
            this.dcht.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.dcht.SizeF = new System.Drawing.SizeF(214.0873F, 22.99998F);
            this.dcht.StylePriority.UseFont = false;
            this.dcht.StylePriority.UsePadding = false;
            this.dcht.StylePriority.UseTextAlignment = false;
            this.dcht.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.9999804F, 118.2015F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(78.66682F, 22.99998F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Số chứng từ:";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(1165F, 26.55326F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UsePadding = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "CÔNG TY TNHH VINH GIA";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrTable5,
            this.xrTable4,
            this.xrTable3,
            this.xrLabel30,
            this.thoigian,
            this.diadiem,
            this.xrLabel34,
            this.xrLabel38,
            this.xrLabel45,
            this.xrLabel15,
            this.xrLabel13,
            this.huongdan,
            this.xrLabel22});
            this.ReportFooter.HeightF = 357.2917F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.ReportFooter.StylePriority.UsePadding = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1.000161F, 245.2222F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(1164F, 22.99997F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "*Lưu ý: Khi cơ sở cung ứng nhận được đơn đặt hàng vui lòng ký xác nhận và fax lại" +
    ".";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1165F, 25F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UsePadding = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell57});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableRow5.StylePriority.UsePadding = false;
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Multiline = true;
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell47.StylePriority.UsePadding = false;
            this.xrTableCell47.Text = "Thành tiền";
            this.xrTableCell47.Weight = 1.5262249937983752D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell48.StylePriority.UsePadding = false;
            this.xrTableCell48.Weight = 0.49441145594116931D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell49.StylePriority.UsePadding = false;
            this.xrTableCell49.Weight = 1.0103656923004336D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Multiline = true;
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell50.StylePriority.UsePadding = false;
            this.xrTableCell50.Weight = 2.2685106088426128D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell51.StylePriority.UsePadding = false;
            this.xrTableCell51.Weight = 0.90980416547266008D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell52.StylePriority.UsePadding = false;
            this.xrTableCell52.Weight = 1.4605061693071844D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell53.StylePriority.UsePadding = false;
            this.xrTableCell53.Weight = 1.0985927884187881D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Multiline = true;
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell54.StylePriority.UsePadding = false;
            this.xrTableCell54.Weight = 0.59973648602851659D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell55.StylePriority.UsePadding = false;
            this.xrTableCell55.Weight = 0.67971218374817366D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell56.StylePriority.UsePadding = false;
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell56.Weight = 0.79363938896251029D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell57.StylePriority.UseBorders = false;
            this.xrTableCell57.StylePriority.UsePadding = false;
            this.xrTableCell57.Weight = 1.1791326022342017D;
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTable4.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1165F, 25F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UsePadding = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell45,
            this.xrTableCell46});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableRow4.StylePriority.UsePadding = false;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Multiline = true;
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell25.StylePriority.UsePadding = false;
            this.xrTableCell25.Text = "Đơn giá";
            this.xrTableCell25.Weight = 1.5262246789135756D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell37.StylePriority.UsePadding = false;
            this.xrTableCell37.Weight = 0.49441177082596882D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell38.StylePriority.UsePadding = false;
            this.xrTableCell38.Weight = 1.0103656923004336D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Multiline = true;
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell39.StylePriority.UsePadding = false;
            this.xrTableCell39.Weight = 2.2685106088426128D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell40.StylePriority.UsePadding = false;
            this.xrTableCell40.Weight = 0.90980416547266008D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell41.StylePriority.UsePadding = false;
            this.xrTableCell41.Weight = 1.4605061693071844D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell42.StylePriority.UsePadding = false;
            this.xrTableCell42.Weight = 1.0985927884187881D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Multiline = true;
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell43.StylePriority.UsePadding = false;
            this.xrTableCell43.Weight = 0.59973648602851659D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell44.StylePriority.UsePadding = false;
            this.xrTableCell44.Weight = 0.67971218374817366D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell45.StylePriority.UsePadding = false;
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell45.Weight = 0.79363938896251029D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.StylePriority.UsePadding = false;
            this.xrTableCell46.Weight = 1.1791326022342017D;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1165F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34,
            this.tong_cong,
            this.xrTableCell36});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableRow3.StylePriority.UsePadding = false;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Multiline = true;
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell26.StylePriority.UsePadding = false;
            this.xrTableCell26.Text = "Tổng cộng";
            this.xrTableCell26.Weight = 1.5262245214711758D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell27.StylePriority.UsePadding = false;
            this.xrTableCell27.Weight = 0.49441192826836855D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell28.StylePriority.UsePadding = false;
            this.xrTableCell28.Weight = 1.0103656923004336D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Multiline = true;
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell29.StylePriority.UsePadding = false;
            this.xrTableCell29.Weight = 2.2685106088426128D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell30.StylePriority.UsePadding = false;
            this.xrTableCell30.Weight = 0.90980416547266008D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell31.StylePriority.UsePadding = false;
            this.xrTableCell31.Weight = 1.4605061693071844D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell32.StylePriority.UsePadding = false;
            this.xrTableCell32.Weight = 1.0985927884187881D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Multiline = true;
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell33.StylePriority.UsePadding = false;
            this.xrTableCell33.Weight = 0.59973648602851659D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell34.StylePriority.UsePadding = false;
            this.xrTableCell34.Weight = 0.67971218374817366D;
            // 
            // tong_cong
            // 
            this.tong_cong.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "soluong")});
            this.tong_cong.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.tong_cong.Name = "tong_cong";
            this.tong_cong.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tong_cong.StylePriority.UseFont = false;
            this.tong_cong.StylePriority.UsePadding = false;
            this.tong_cong.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:#.00}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.tong_cong.Summary = xrSummary2;
            this.tong_cong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tong_cong.Weight = 0.79363938896251029D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.StylePriority.UsePadding = false;
            this.xrTableCell36.Weight = 1.1791326022342017D;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0F, 222.2222F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(147.9167F, 22.99997F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UsePadding = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "*Thời gian giao hàng :";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // thoigian
            // 
            this.thoigian.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thoigian", "{0:dd/MM/yyyy}")});
            this.thoigian.Font = new System.Drawing.Font("Arial", 9F);
            this.thoigian.LocationFloat = new DevExpress.Utils.PointFloat(147.9167F, 222.2223F);
            this.thoigian.Name = "thoigian";
            this.thoigian.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.thoigian.SizeF = new System.Drawing.SizeF(264.3622F, 22.99997F);
            this.thoigian.StylePriority.UseFont = false;
            this.thoigian.StylePriority.UsePadding = false;
            this.thoigian.StylePriority.UseTextAlignment = false;
            this.thoigian.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // diadiem
            // 
            this.diadiem.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diadiem")});
            this.diadiem.Font = new System.Drawing.Font("Arial", 9F);
            this.diadiem.LocationFloat = new DevExpress.Utils.PointFloat(647.6111F, 222.2222F);
            this.diadiem.Name = "diadiem";
            this.diadiem.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.diadiem.SizeF = new System.Drawing.SizeF(421.5564F, 22.99997F);
            this.diadiem.StylePriority.UseFont = false;
            this.diadiem.StylePriority.UsePadding = false;
            this.diadiem.StylePriority.UseTextAlignment = false;
            this.diadiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(513.611F, 222.2222F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(134.0001F, 22.99997F);
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.StylePriority.UsePadding = false;
            this.xrLabel34.StylePriority.UseTextAlignment = false;
            this.xrLabel34.Text = "*Địa điểm giao hàng:";
            this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(1.000158F, 75F);
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(146.9164F, 67.79166F);
            this.xrLabel38.StylePriority.UseFont = false;
            this.xrLabel38.StylePriority.UsePadding = false;
            this.xrLabel38.StylePriority.UseTextAlignment = false;
            this.xrLabel38.Text = "Hướng dẫn làm hàng:";
            this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel45
            // 
            this.xrLabel45.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(743.3335F, 268.2222F);
            this.xrLabel45.Multiline = true;
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel45.SizeF = new System.Drawing.SizeF(164.5964F, 18.26996F);
            this.xrLabel45.StylePriority.UseFont = false;
            this.xrLabel45.StylePriority.UsePadding = false;
            this.xrLabel45.StylePriority.UseTextAlignment = false;
            this.xrLabel45.Text = "Nhà cung ứng";
            this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(38.49999F, 286.4921F);
            this.xrLabel15.Multiline = true;
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(157.3333F, 18.26999F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UsePadding = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "(Ký, họ tên)";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(38.49999F, 268.2222F);
            this.xrLabel13.Multiline = true;
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(157.3333F, 18.26996F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UsePadding = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Người lập";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // huongdan
            // 
            this.huongdan.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "huongdan")});
            this.huongdan.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.huongdan.LocationFloat = new DevExpress.Utils.PointFloat(147.9166F, 74.99999F);
            this.huongdan.Name = "huongdan";
            this.huongdan.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.huongdan.SizeF = new System.Drawing.SizeF(693.7503F, 136.1111F);
            this.huongdan.StylePriority.UsePadding = false;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(743.3335F, 286.4921F);
            this.xrLabel22.Multiline = true;
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(164.5964F, 18.26999F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UsePadding = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "(Ký, họ tên)";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 69.44444F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.PageHeader.StylePriority.UsePadding = false;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1165F, 69.44444F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableRow1.StylePriority.UsePadding = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.39724848594831963D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.Text = "MS Anco";
            this.xrTableCell2.Weight = 1.1289757066233677D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.Text = "Dùng cho";
            this.xrTableCell3.Weight = 0.49441177575028633D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.Text = "MS khách";
            this.xrTableCell4.Weight = 1.0103653774156469D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell5.StylePriority.UsePadding = false;
            this.xrTableCell5.Text = "Mô tả hàng";
            this.xrTableCell5.Weight = 2.2685106088426132D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.Text = "Màu sắc hàng";
            this.xrTableCell6.Weight = 0.9098035357030867D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell7.StylePriority.UsePadding = false;
            this.xrTableCell7.Text = "Kích thước hàng";
            this.xrTableCell7.Weight = 1.4605068777979555D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.Text = "Barcode";
            this.xrTableCell8.Weight = 1.0985927096975903D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell9.StylePriority.UsePadding = false;
            this.xrTableCell9.Text = "Đóng gói inner";
            this.xrTableCell9.Weight = 0.59973648602851737D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.Text = "Đóng gói master";
            this.xrTableCell10.Weight = 0.67971218374817322D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell11.StylePriority.UsePadding = false;
            this.xrTableCell11.Text = "Số lượng hàng";
            this.xrTableCell11.Weight = 0.79363938896251018D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UsePadding = false;
            this.xrTableCell12.Text = "Ghi chú";
            this.xrTableCell12.Weight = 1.1791329171189886D;
            // 
            // rptInCTTemBB
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter,
            this.PageHeader});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(1, 3, 0, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huongdan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
