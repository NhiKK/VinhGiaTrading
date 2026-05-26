using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for rptXacNhanBaoBi
/// </summary>
public class rptXacNhanBaoBi : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel7;
    private XRSubreport xrSubreport1;
    private rptXacNhanBaoBiItems rptXacNhanBaoBiItems1;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel10;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public rptXacNhanBaoBi()
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
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
        string resourceFileName = "rptXacNhanBaoBi.resx";
        System.Resources.ResourceManager resources = global::Resources.rptXacNhanBaoBi.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
        this.rptXacNhanBaoBiItems1 = new rptXacNhanBaoBiItems();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.rptXacNhanBaoBiItems1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrSubreport1,
            this.xrLabel7});
        this.Detail.HeightF = 198.3335F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 176.1251F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(1250.083F, 22.20834F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "Ghi chú: Nếu NW = 0 hoặc ngày xác nhận đóng gói trống, nhà cung ứng phải gửi xác " +
            "nhận và yêu cầu PTSP cập nhật.";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Font = new System.Drawing.Font("Arial", 9F);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 108.0417F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(199.0417F, 22.20834F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "(Ký, ghi rõ họ tên)";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 85.83336F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(199.0417F, 22.20834F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Người lập";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrSubreport1
        // 
        this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.20834F);
        this.xrSubreport1.Name = "xrSubreport1";
        this.xrSubreport1.ReportSource = this.rptXacNhanBaoBiItems1;
        this.xrSubreport1.SizeF = new System.Drawing.SizeF(1250.083F, 63.62502F);
        this.xrSubreport1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreport1_BeforePrint);
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten_dtkd", "Nhà cung ứng: {0}")});
        this.xrLabel7.Font = new System.Drawing.Font("Arial", 9F);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(1250.083F, 22.20834F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "xrLabel7";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 49F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 101.6666F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel1,
            this.xrPictureBox1,
            this.xrLabel2,
            this.xrLabel4,
            this.xrLabel3});
        this.ReportHeader.HeightF = 193.4584F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sochungtu", "Theo số PO: {0}")});
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 171.25F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(790.7083F, 22.20837F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaylap", "Ngày lập: {0:dd/MMM/yyyy}")});
        this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 149.0417F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(790.7083F, 22.20834F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "xrLabel5";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Arial", 16F);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(198.9582F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(790.7083F, 50F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "VINH GIA COMPANY LIMITED";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(124.9998F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(73.95822F, 54.25002F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(198.958F, 50F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(790.7084F, 22.20834F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 75F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(790.7083F, 22.20834F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Tel.:  (Tel: (84-235) 3567393   Fax: (84-235) 3567494";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(198.9583F, 100F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(790.7083F, 49.04169F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "XÁC NHẬN BAO BÌ VÀ ĐÓNG GÓI";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // rptXacNhanBaoBi
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
        this.Margins = new System.Drawing.Printing.Margins(49, 46, 49, 102);
        this.PageHeight = 1169;
        this.PageWidth = 1654;
        this.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
        this.Version = "11.2";
        ((System.ComponentModel.ISupportInitialize)(this.rptXacNhanBaoBiItems1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

    private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        String c_danhsachdathang_id = (String)GetCurrentColumnValue("c_danhsachdathang_id");
        String sql = String.Format(@"select *, case when sl_inner > 1 then  round((((l1+w1)*(w1 + h1) / 5400 ) * sl_outer / sl_inner ) * noofpack + NW, 2)
		                                    else round((((l2+w2)*(w2+h2)) / 5400) * noofpack + NW, 2)
		                                    end as gw
                                    from
                                    (select cdg.sothutu, sp.ma_sanpham, cdg.ma_sanpham_khach, ddh.sl_dathang, dg.ten_donggoi,
                                        (case when cdg.sl_outer = 0 then 0 else round(cdg.soluong/cdg.sl_outer, 1) end) as noofpack,
	                                    dg.sl_inner, (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner) as dvt_inner,
	                                    cdg.l1, cdg.w1, cdg.h1, dg.sl_outer,(select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer) as dvt_outer,
	                                    cdg.l2, cdg.w2, cdg.h2, cdg.vd, cdg.vn, cdg.vl, (case when cdg.sl_outer = 0 then 0 else round((ddh.sl_dathang/dg.sl_outer), 1) end) as slthung, 
                                        (case dg.sl_inner when 0 then 0 else round((ddh.sl_dathang/dg.sl_inner), 1) end) as slthunginner,
										dg.soluonggoi_ctn,
	                                    (sp.trongluong * ddh.sl_dathang) as NW, dsdh.huongdanlamhang, dg.ngayxacnhan, dg.mota
                                    from c_danhsachdathang dsdh, c_dongdsdh ddh, c_dongdonhang cdg, md_sanpham sp, md_donggoi dg
                                    where ddh.c_danhsachdathang_id = N'{0}'
                                            and dsdh.c_danhsachdathang_id = ddh.c_danhsachdathang_id
		                                    and ddh.c_dongdonhang_id = cdg.c_dongdonhang_id
		                                    and cdg.md_donggoi_id = dg.md_donggoi_id
		                                    and ddh.md_sanpham_id = sp.md_sanpham_id) as tmp order by sothutu asc", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);
        SqlDataAdapter da = new SqlDataAdapter(sql, mdbc.GetConnection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        rptXacNhanBaoBiItems1.DataAdapter = da;
        rptXacNhanBaoBiItems1.DataSource = ds;
    }
}
