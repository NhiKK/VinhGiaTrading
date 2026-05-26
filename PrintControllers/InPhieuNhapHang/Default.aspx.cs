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
        String c_nhapxuat_id = Context.Request.QueryString["c_nhapxuat_id"];
        rptNhapHang report = new rptNhapHang();
        String sql = this.CreateSql(c_nhapxuat_id);
        this.viewReport(report, sql);
    }

    public void viewReport(XtraReport report, String SqlQuery)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        var tbl = ds.Tables[0];
        if (tbl.Rows.Count > 0)
        {
            tbl.Columns.Add("tien", Type.GetType("System.String"));
			
            //Footer
            int lastRow = tbl.Rows.Count - 1;
			string textMoney = "";
			double? totalUSD = null;
			string strTotalUSD = tbl.Rows[0]["totalgrand"].ToString();
			if(!string.IsNullOrEmpty(strTotalUSD))
				totalUSD = double.Parse(strTotalUSD);
			var ma_dtkd = tbl.Rows[0]["ma_dtkd"].ToString();
			if(totalUSD != null) {
				if(new string[]{ "VINHGIA" }.Contains(ma_dtkd)) {
					string m = MoneyToWord.ConvertMoneyToText(totalUSD.ToString()).Replace("Dollars", "");			
					int j =  m.LastIndexOf("and");
				
					if (m.Contains("Cents") | m.Contains("Cent"))
					{
						m = m.Replace("Cents", "").Replace("Cent", "");
						m = m.Insert(m.Length, "cents");
					}
					else {
						m = m.Replace("Cents", "").Replace("Cent", "");
					}
					textMoney = m;
				}
				else {
					textMoney = MoneyToWord.ConvertMoneyToTextVND((decimal)totalUSD.GetValueOrDefault(0));
				}
				tbl.Rows[lastRow]["tien"] = textMoney;
			}
			
            report.DataSource = ds;
            report.DataAdapter = da;
            ReportViewer1.Report = report;
        }
        else
        {
            Response.Write("<h1>Không có dữ liệu</h1>");
        }
    }

    public String CreateSql(String c_nhapxuat_id)
    {
        // String selTotal = String.Format(@"select SUM( dnx.dongia * dnx.slthuc_nhapxuat) as tongtien 
                            // from c_nhapxuat nx, c_dongnhapxuat dnx 
                            // where nx.c_nhapxuat_id = dnx.c_nhapxuat_id
                            // AND nx.c_nhapxuat_id = N'{0}'", c_nhapxuat_id == null ? "" : c_nhapxuat_id);
        // decimal total = (decimal)mdbc.ExecuteScalar(selTotal);

        String str = String.Format(@"
            SELECT 
	            nx.sophieu, dtkd.ma_dtkd, nx.ngay_giaonhan as ngay, dh.sochungtu as so_po, kho.ten_kho as giao_kho
                , (select sochungtu from c_danhsachdathang dsdh where dsdh.c_danhsachdathang_id = (select c_danhsachdathang_id from c_dongdsdh ddsdh where ddsdh.c_dongdsdh_id = dnx.c_dongdsdh_id)) as chungtu_dathang
                , sp.ma_sanpham, sp.mota_tiengviet, dvt.ma_edi as dvt, nx.phicong, nx.phitru, nx.discount, nx.totaldiscount, nx.grandtotal as totalgrand
	            , dnx.slthuc_nhapxuat as sl, dnx.dongia as dongia, (dnx.slthuc_nhapxuat * dnx.dongia) as thanhtien
                , (select hoten from nhanvien where manv = nx.nguoitao) as hoten
            FROM 
	            c_nhapxuat nx , c_dongnhapxuat dnx, md_doitackinhdoanh dtkd
                , c_donhang dh, md_sanpham sp, md_donvitinhsanpham dvt, md_kho kho
            WHERE
	            nx.c_nhapxuat_id = dnx.c_nhapxuat_id
	            AND nx.c_donhang_id = dh.c_donhang_id
                AND nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	            AND dnx.md_sanpham_id = sp.md_sanpham_id
	            AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
                AND nx.md_kho_id = kho.md_kho_id 
                AND nx.c_nhapxuat_id = N'{0}' order by sp.ma_sanpham asc"
			, c_nhapxuat_id == null ? "" : c_nhapxuat_id);
        return str;
    }
}

/// <summary>
/// Summary description for rptNhapHang
/// </summary>
public class rptNhapHang : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel13;
    private XRLabel xrLabel11;
    private XRLabel lb_so_bo;
    private XRLabel xrLabel9;
    private XRLabel bl_ngay;
    private XRLabel xrLabel7;
    private XRLabel xrLabel5;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel22;
    private XRLabel lb_giao_kho;
    private XRLabel xrLabel20;
    private XRLabel lb_kyten_coso;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel lb_ngaygiao;
    private XRLabel xrLabel15;
    private XRLabel lb_giaohang_so;
    private XRLabel xrLabel32;
    private XRLabel xrLabel30;
    private XRLabel xrLabel28;
    private XRLabel xrLabel27;
    private XRLabel lb_tien;
    private XRLabel xrLabel25;
    private XRLabel xrLabel24;
    private XRLabel lb_tongcong;
    private XRLabel lb_coso;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell lb_sott;
    private XRTableCell lb_maso;
    private XRTableCell lb_mota;
    private XRTableCell lb_dvt;
    private XRTableCell lb_sl;
    private XRTableCell lb_dongia;
    private XRTableCell lb_thanhtien;
    private XRTableCell lb_ghichu;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRLabel lb_sochungtu;
    private XRLabel lb_hoadon_so;
    private XRLabel xrLabel10;
    private XRLabel xrLabel12;
    private XRLabel xrLabel14;
    private XRLabel xrLabel21;
    private XRLabel xrLabel19;
    private XRLabel xrLabel16;
    private XRLabel xrLabel31;
    private XRLabel xrLabel26;
    private XRLabel xrLabel23;
    private XRLabel xrLabel33;
    private XRLabel xrLabel8;
    private XRLabel xrLabel6;
    private XRLabel xrLabel29;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptNhapHang()
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
        string resourceFileName = "rptNhapHang.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.lb_sott = new DevExpress.XtraReports.UI.XRTableCell();
        this.lb_maso = new DevExpress.XtraReports.UI.XRTableCell();
        this.lb_mota = new DevExpress.XtraReports.UI.XRTableCell();
        this.lb_dvt = new DevExpress.XtraReports.UI.XRTableCell();
        this.lb_sl = new DevExpress.XtraReports.UI.XRTableCell();
        this.lb_dongia = new DevExpress.XtraReports.UI.XRTableCell();
        this.lb_thanhtien = new DevExpress.XtraReports.UI.XRTableCell();
        this.lb_ghichu = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.lb_sochungtu = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_coso = new DevExpress.XtraReports.UI.XRLabel();
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
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_giao_kho = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_kyten_coso = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_ngaygiao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_giaohang_so = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_so_bo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_hoadon_so = new DevExpress.XtraReports.UI.XRLabel();
        this.bl_ngay = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_tien = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.lb_tongcong = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 28F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Font = new System.Drawing.Font("Arial", 10F);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(753.9999F, 27.50002F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lb_sott,
            this.lb_maso,
            this.lb_mota,
            this.lb_dvt,
            this.lb_sl,
            this.lb_dongia,
            this.lb_thanhtien,
            this.lb_ghichu});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // lb_sott
        // 
        this.lb_sott.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lb_sott.Name = "lb_sott";
        this.lb_sott.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.lb_sott.StylePriority.UseFont = false;
        this.lb_sott.StylePriority.UsePadding = false;
        this.lb_sott.StylePriority.UseTextAlignment = false;
        xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.lb_sott.Summary = xrSummary1;
        this.lb_sott.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.lb_sott.Weight = 0.3001079510644733D;
        // 
        // lb_maso
        // 
        this.lb_maso.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_sanpham")});
        this.lb_maso.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lb_maso.Name = "lb_maso";
        this.lb_maso.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_maso.StylePriority.UseFont = false;
        this.lb_maso.StylePriority.UsePadding = false;
        this.lb_maso.StylePriority.UseTextAlignment = false;
        this.lb_maso.Text = "[ma_sanpham]";
        this.lb_maso.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lb_maso.Weight = 0.8269791456272173D;
        // 
        // lb_mota
        // 
        this.lb_mota.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mota_tiengviet")});
        this.lb_mota.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lb_mota.Multiline = true;
        this.lb_mota.Name = "lb_mota";
        this.lb_mota.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.lb_mota.StylePriority.UseFont = false;
        this.lb_mota.StylePriority.UsePadding = false;
        this.lb_mota.StylePriority.UseTextAlignment = false;
        this.lb_mota.Text = "[mota_tiengviet]";
        this.lb_mota.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lb_mota.Weight = 1.5708964553893698D;
        // 
        // lb_dvt
        // 
        this.lb_dvt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dvt")});
        this.lb_dvt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lb_dvt.Name = "lb_dvt";
        this.lb_dvt.StylePriority.UseFont = false;
        this.lb_dvt.Text = "[dvt]";
        this.lb_dvt.Weight = 0.410118795399489D;
        // 
        // lb_sl
        // 
        this.lb_sl.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl", "{0:#,#0.##}")});
        this.lb_sl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lb_sl.Name = "lb_sl";
        this.lb_sl.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_sl.StylePriority.UseFont = false;
        this.lb_sl.StylePriority.UsePadding = false;
        this.lb_sl.StylePriority.UseTextAlignment = false;
        this.lb_sl.Text = "lb_sl";
        this.lb_sl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.lb_sl.Weight = 0.548161613881219D;
        // 
        // lb_dongia
        // 
        this.lb_dongia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dongia", "{0:#,#0.##}")});
        this.lb_dongia.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lb_dongia.Name = "lb_dongia";
        this.lb_dongia.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_dongia.StylePriority.UseFont = false;
        this.lb_dongia.StylePriority.UsePadding = false;
        this.lb_dongia.StylePriority.UseTextAlignment = false;
        this.lb_dongia.Text = "lb_dongia";
        this.lb_dongia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.lb_dongia.Weight = 0.7708522367591526D;
        // 
        // lb_thanhtien
        // 
        this.lb_thanhtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thanhtien", "{0:#,#0.##}")});
        this.lb_thanhtien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lb_thanhtien.Name = "lb_thanhtien";
        this.lb_thanhtien.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_thanhtien.StylePriority.UseFont = false;
        this.lb_thanhtien.StylePriority.UsePadding = false;
        this.lb_thanhtien.StylePriority.UseTextAlignment = false;
        this.lb_thanhtien.Text = "lb_thanhtien";
        this.lb_thanhtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.lb_thanhtien.Weight = 1.0579018938918956D;
        // 
        // lb_ghichu
        // 
        this.lb_ghichu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ghichu")});
        this.lb_ghichu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lb_ghichu.Name = "lb_ghichu";
        this.lb_ghichu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_ghichu.StylePriority.UseFont = false;
        this.lb_ghichu.StylePriority.UsePadding = false;
        this.lb_ghichu.StylePriority.UseTextAlignment = false;
        this.lb_ghichu.Text = "[ghichu]";
        this.lb_ghichu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lb_ghichu.Weight = 0.9730103972453652D;
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
            this.lb_sochungtu,
            this.lb_coso,
            this.xrTable1,
            this.xrLabel22,
            this.lb_giao_kho,
            this.xrLabel20,
            this.lb_kyten_coso,
            this.xrLabel18,
            this.xrLabel17,
            this.lb_ngaygiao,
            this.xrLabel15,
            this.lb_giaohang_so,
            this.xrLabel13,
            this.xrLabel11,
            this.lb_so_bo,
            this.xrLabel9,
            this.lb_hoadon_so,
            this.bl_ngay,
            this.xrLabel7,
            this.xrLabel5,
            this.xrLabel1,
            this.xrPictureBox1,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2});
        this.ReportHeader.HeightF = 352F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // lb_sochungtu
        // 
        this.lb_sochungtu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sophieu")});
        this.lb_sochungtu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_sochungtu.LocationFloat = new DevExpress.Utils.PointFloat(209.375F, 140.0418F);
        this.lb_sochungtu.Name = "lb_sochungtu";
        this.lb_sochungtu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_sochungtu.SizeF = new System.Drawing.SizeF(190.8901F, 23.00002F);
        this.lb_sochungtu.StylePriority.UseFont = false;
        this.lb_sochungtu.StylePriority.UseTextAlignment = false;
        this.lb_sochungtu.Text = "[sophieu]";
        this.lb_sochungtu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_coso
        // 
        this.lb_coso.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
        this.lb_coso.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_coso.LocationFloat = new DevExpress.Utils.PointFloat(579F, 209.0417F);
        this.lb_coso.Name = "lb_coso";
        this.lb_coso.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_coso.SizeF = new System.Drawing.SizeF(175F, 23.00003F);
        this.lb_coso.StylePriority.UseFont = false;
        this.lb_coso.StylePriority.UseTextAlignment = false;
        this.lb_coso.Text = "[ma_dtkd]";
        this.lb_coso.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 322.9999F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(754.0001F, 28.74988F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
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
            this.xrTableCell8});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.Text = "STT";
        this.xrTableCell1.Weight = 0.300107870556557D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.Text = "Mã số";
        this.xrTableCell2.Weight = 0.82697907872503784D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.Text = "Mô tả";
        this.xrTableCell3.Weight = 1.5708962271230602D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.Text = "ĐVT";
        this.xrTableCell4.Weight = 0.41011899955548181D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.Text = "SL";
        this.xrTableCell5.Weight = 0.54816152463856294D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.Text = "Đơn giá";
        this.xrTableCell6.Weight = 0.77085214726188489D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.Text = "Thành tiền";
        this.xrTableCell7.Weight = 1.0579016530992047D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.Text = "Ghi chú";
        this.xrTableCell8.Weight = 0.97301102597680944D;
        // 
        // xrLabel22
        // 
        this.xrLabel22.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(5.086263E-05F, 300F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(753.9999F, 23F);
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.Text = "Theo chi tiết sau đây:";
        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_giao_kho
        // 
        this.lb_giao_kho.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "giao_kho")});
        this.lb_giao_kho.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_giao_kho.LocationFloat = new DevExpress.Utils.PointFloat(254.1667F, 255.0418F);
        this.lb_giao_kho.Name = "lb_giao_kho";
        this.lb_giao_kho.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_giao_kho.SizeF = new System.Drawing.SizeF(499.8332F, 22.99994F);
        this.lb_giao_kho.StylePriority.UseFont = false;
        this.lb_giao_kho.StylePriority.UseTextAlignment = false;
        this.lb_giao_kho.Text = "[giao_kho]";
        this.lb_giao_kho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel20
        // 
        this.xrLabel20.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 255.0418F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(254.1667F, 23.00002F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = "Được giao và nhận hàng tại kho:";
        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_kyten_coso
        // 
        this.lb_kyten_coso.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ma_dtkd")});
        this.lb_kyten_coso.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_kyten_coso.LocationFloat = new DevExpress.Utils.PointFloat(454F, 232.0418F);
        this.lb_kyten_coso.Name = "lb_kyten_coso";
        this.lb_kyten_coso.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_kyten_coso.SizeF = new System.Drawing.SizeF(299.9999F, 23.00002F);
        this.lb_kyten_coso.StylePriority.UseFont = false;
        this.lb_kyten_coso.StylePriority.UseTextAlignment = false;
        this.lb_kyten_coso.Text = "[ma_dtkd]";
        this.lb_kyten_coso.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel18
        // 
        this.xrLabel18.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 232.0418F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(454F, 23.00002F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = "Chúng tôi, những người ký tên dưới đây xác nhận nhận hàng của cơ sở:";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel17
        // 
        this.xrLabel17.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(454F, 209.0417F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(124.9999F, 23.00003F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "Của cơ sở:";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_ngaygiao
        // 
        this.lb_ngaygiao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngay", "{0:M/d/yyyy}")});
        this.lb_ngaygiao.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_ngaygiao.LocationFloat = new DevExpress.Utils.PointFloat(579F, 186.0418F);
        this.lb_ngaygiao.Name = "lb_ngaygiao";
        this.lb_ngaygiao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_ngaygiao.SizeF = new System.Drawing.SizeF(174.9999F, 23.00003F);
        this.lb_ngaygiao.StylePriority.UseFont = false;
        this.lb_ngaygiao.StylePriority.UseTextAlignment = false;
        this.lb_ngaygiao.Text = "lb_ngaygiao";
        this.lb_ngaygiao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(454F, 186.0418F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(124.9999F, 23.00002F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "Ngày:";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_giaohang_so
        // 
        this.lb_giaohang_so.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "giaohang_so")});
        this.lb_giaohang_so.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_giaohang_so.LocationFloat = new DevExpress.Utils.PointFloat(209.375F, 187.0834F);
        this.lb_giaohang_so.Name = "lb_giaohang_so";
        this.lb_giaohang_so.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_giaohang_so.SizeF = new System.Drawing.SizeF(190.8901F, 22.99998F);
        this.lb_giaohang_so.StylePriority.UseFont = false;
        this.lb_giaohang_so.StylePriority.UseTextAlignment = false;
        this.lb_giaohang_so.Text = "[giaohang_so]";
        this.lb_giaohang_so.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel13
        // 
        this.xrLabel13.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 187.0834F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(209.375F, 22.99998F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "Căn cứ theo phiếu giao hàng số:";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(454F, 163.0418F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(124.9999F, 23.00003F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "Theo P/O số:";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_so_bo
        // 
        this.lb_so_bo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "so_po")});
        this.lb_so_bo.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_so_bo.LocationFloat = new DevExpress.Utils.PointFloat(579F, 163.0418F);
        this.lb_so_bo.Name = "lb_so_bo";
        this.lb_so_bo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_so_bo.SizeF = new System.Drawing.SizeF(175F, 23.00003F);
        this.lb_so_bo.StylePriority.UseFont = false;
        this.lb_so_bo.StylePriority.UseTextAlignment = false;
        this.lb_so_bo.Text = "[so_po]";
        this.lb_so_bo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 163.0418F);
        this.xrLabel9.Multiline = true;
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(209.375F, 24.04166F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "Nhập cho đơn đặt hàng số:";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_hoadon_so
        // 
        this.lb_hoadon_so.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "chungtu_dathang")});
        this.lb_hoadon_so.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_hoadon_so.LocationFloat = new DevExpress.Utils.PointFloat(209.375F, 163.0418F);
        this.lb_hoadon_so.Name = "lb_hoadon_so";
        this.lb_hoadon_so.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_hoadon_so.SizeF = new System.Drawing.SizeF(190.8901F, 24.04163F);
        this.lb_hoadon_so.StylePriority.UseFont = false;
        this.lb_hoadon_so.StylePriority.UseTextAlignment = false;
        this.lb_hoadon_so.Text = "[chungtu_dathang]";
        this.lb_hoadon_so.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // bl_ngay
        // 
        this.bl_ngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngay", "{0:M/d/yyyy}")});
        this.bl_ngay.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.bl_ngay.LocationFloat = new DevExpress.Utils.PointFloat(579F, 140.0418F);
        this.bl_ngay.Name = "bl_ngay";
        this.bl_ngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.bl_ngay.SizeF = new System.Drawing.SizeF(175F, 23.00002F);
        this.bl_ngay.StylePriority.UseFont = false;
        this.bl_ngay.StylePriority.UseTextAlignment = false;
        this.bl_ngay.Text = "bl_ngay";
        this.bl_ngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(454F, 140.0418F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(125F, 23.00002F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Núi Thành, ngày";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 139.7917F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(209.375F, 22.74998F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Số chứng từ:";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Arial", 16F);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(71.58348F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(683.4165F, 44.25001F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(71.58347F, 54.25002F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(71.58348F, 66.45836F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(683.4165F, 22.20834F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Tel.:  (Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(71.58348F, 88.66669F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(563.4164F, 51.12502F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "PHIẾU NHẬP HÀNG";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(71.58356F, 44.25F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(683.4164F, 22.20835F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel33,
            this.xrLabel31,
            this.xrLabel26,
            this.xrLabel23,
            this.xrLabel21,
            this.xrLabel19,
            this.xrLabel16,
            this.xrLabel14,
            this.xrLabel12,
            this.xrLabel10,
            this.xrLabel8,
            this.xrLabel6,
            this.xrLabel32,
            this.xrLabel30,
            this.xrLabel29,
            this.xrLabel28,
            this.xrLabel27,
            this.lb_tien,
            this.xrLabel25,
            this.xrLabel24,
            this.lb_tongcong});
        this.ReportFooter.HeightF = 320F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrLabel33
        // 
        this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "hoten")});
        this.xrLabel33.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(0F, 296.7917F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(251.23F, 23.00003F);
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = "[hoten]";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel31
        // 
        this.xrLabel31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalgrand", "{0:#,#0.##}")});
        this.xrLabel31.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(513.8177F, 91.99992F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(126.5796F, 22.99995F);
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "[totalgrand]";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel26
        // 
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totaldiscount", "{0:#,#0.##}")});
        this.xrLabel26.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(513.8177F, 68.99994F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(126.5795F, 22.99996F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "[totaldiscount]";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel23
        // 
        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phitru", "{0:#,#0.##}")});
        this.xrLabel23.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(513.8177F, 45.99996F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(126.5795F, 22.99996F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        this.xrLabel23.Text = "[phitru]";
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel21
        // 
        this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "discount", "Discount: ({0}%)")});
        this.xrLabel21.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 68.99999F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(299.5497F, 22.99995F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.Text = "xrLabel21";
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel19
        // 
        this.xrLabel19.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(5.086263E-05F, 45.99996F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(299.5496F, 23.00002F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "Phí trừ:";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.99993F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(299.5496F, 23.00002F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "Phí cộng:";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "phicong", "{0:#,#0.##}")});
        this.xrLabel14.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(513.8177F, 22.99993F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(126.5795F, 22.99996F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "[phicong]";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(5.086263E-05F, 0F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(299.5496F, 23.00002F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "Tổng cộng:";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sl")});
        this.xrLabel10.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(362.8831F, 0F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(64F, 23.00002F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:#,#0.##}";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel10.Summary = xrSummary2;
        this.xrLabel10.Text = "xrLabel10";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel32
        // 
        this.xrLabel32.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(502.46F, 180.625F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(251.5399F, 23.00002F);
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        this.xrLabel32.Text = "Nhà cung ứng";
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(251.54F, 180.6248F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(250.9201F, 23.00002F);
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.Text = "Trưởng phòng";
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel28
        // 
        this.xrLabel28.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 180.625F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(251.23F, 23.00002F);
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "Người lập phiếu";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel27
        // 
        this.xrLabel27.Font = new System.Drawing.Font("Arial", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(0F, 154.6666F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(150.7502F, 23.00002F);
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "Ghi chú:";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_tien
        // 
        this.lb_tien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tien")});
        this.lb_tien.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_tien.LocationFloat = new DevExpress.Utils.PointFloat(150.7502F, 131.6666F);
        this.lb_tien.Name = "lb_tien";
        this.lb_tien.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_tien.SizeF = new System.Drawing.SizeF(484.2498F, 23.00002F);
        this.lb_tien.StylePriority.UseFont = false;
        this.lb_tien.StylePriority.UseTextAlignment = false;
        this.lb_tien.Text = "0";
        this.lb_tien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel25
        // 
        this.xrLabel25.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(0F, 131.6666F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(150.7502F, 23.00002F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.Text = "Viết bằng chữ:";
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel24
        // 
        this.xrLabel24.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 91.99992F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(299.5497F, 23.00003F);
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "Tổng cộng đã trừ:";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lb_tongcong
        // 
        this.lb_tongcong.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "thanhtien", "{0:#,#0.##}")});
        this.lb_tongcong.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.lb_tongcong.LocationFloat = new DevExpress.Utils.PointFloat(513.8177F, 0F);
        this.lb_tongcong.Name = "lb_tongcong";
        this.lb_tongcong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lb_tongcong.SizeF = new System.Drawing.SizeF(126.5795F, 23.00002F);
        this.lb_tongcong.StylePriority.UseFont = false;
        this.lb_tongcong.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:#,#0.##}";
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.lb_tongcong.Summary = xrSummary3;
        this.lb_tongcong.Text = "[thanhtien]";
        this.lb_tongcong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel29
        // 
        this.xrLabel29.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0F, 203.625F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(251.23F, 23.00002F);
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = "Ký và ghi rõ họ tên";
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(251.23F, 203.6248F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(251.23F, 23.00002F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Ký và ghi rõ họ tên";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(502.46F, 203.6248F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(251.2299F, 23.00005F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Ký và ghi rõ họ tên";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // rptNhapHang
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
        this.Margins = new System.Drawing.Printing.Margins(36, 36, 0, 0);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}