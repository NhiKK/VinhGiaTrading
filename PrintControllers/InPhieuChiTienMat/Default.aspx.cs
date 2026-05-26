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
        XtraReport2 report = new XtraReport2();
        String sql = this.CreateSql(id);
        this.viewReport(report, sql);
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

    // InPhieuChiTienMat
    public String CreateSql(String id)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        c_chitietthuchi cttc = db.c_chitietthuchis.Where(s => s.c_chitietthuchi_id == id).Take(1).FirstOrDefault();
        string str = "select * from c_chitietthuchi where 1 = 2";
        string ma_iso = "VND", daidien = "", ten_dtkd = "", diachi = "", nganhang = "", bangso = "", bangchu = "", bangso_quydoi = "", diengiai = "", masothue = "";
        if (cttc != null)
        {
            c_thuchi tc = db.c_thuchis.Where(s => s.c_thuchi_id == cttc.c_thuchi_id).Take(1).FirstOrDefault();
            md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == cttc.md_doitackinhdoanh_id).Take(1).FirstOrDefault();
            md_dongtien dt = db.md_dongtiens.Where(s => s.md_dongtien_id == tc.md_dongtien_id).Take(1).FirstOrDefault();

            if (dt != null) { ma_iso = dt.ma_iso; }
            if (dtkd != null)
            {
                daidien = dtkd.daidien;
                diachi = dtkd.diachi;
            }
			double tien = (double)cttc.sotien.Value;
			if(cttc.sotiencong != null){
				tien += (double)cttc.sotiencong.Value;
			}
            bangchu = VNN_ConvertMoney.convert(tien, ma_iso);
            bangso = VNN_ConvertMoney.sep_thous(VNN_ConvertMoney.autoRound((decimal)tien, 2).ToString(), ",", ".");
            bangso_quydoi = VNN_ConvertMoney.sep_thous(VNN_ConvertMoney.autoRound(cttc.quydoi.Value, 2).ToString(), ",", ".");
            
			str = String.Format(@" select
			N'{0}' as dongtien, 
			N'{1}' as ten, N'{2}' as diachi2, N'{3}' as sotien,
			N'{4}' as bangchu,
			cttc.ngaychi as n_ngay,cttc.ngaychi as n_thang,cttc.ngaychi as n_nam, 
			cttc.diengiai as lydo, cttc.tk_no as tk_no, cttc.tk_co as tk_co, N'{8}' as quydoi_vn,
			N'' as sophieu,
			N'{6}' as sochungtu, N'{7}' as tygia
			from c_chitietthuchi cttc
			inner join c_thuchi tc on tc.c_thuchi_id = cttc.c_thuchi_id
			where cttc.c_chitietthuchi_id = N'{9}'",
            "đồng", daidien, diachi, bangso, bangchu, tc.sophieu, tc.sochungtu, tc.tygia, bangso_quydoi, id);
        }
        return str;
        /*String str = String.Format(@"
		select dt.ma_iso as dongtien, 
		dtkd.daidien as ten, dtkd.diachi as diachi2,
		cttc.sotien as sotien, '' as bangchu, cttc.diengiai as lydo, cttc.tk_no as tk_no, cttc.tk_co as tk_co, cttc.quydoi as quydoi_vn,
        tc.sophieu as sophieu, tc.sochungtu as sochungtu, tc.tygia as tygia
		from c_chitietthuchi cttc
		left join c_thuchi tc on cttc.c_thuchi_id = tc.c_thuchi_id
		left join md_dongtien dt on tc.md_dongtien_id = dt.md_dongtien_id
		left join md_doitackinhdoanh dtkd on cttc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
		where cttc.c_chitietthuchi_id = '{0}' ", id);
        return str;*/
    }
}

/// <summary>
/// Summary description for XtraReport1
/// </summary>
/// <summary>
/// Summary description for XtraReport2
/// </summary>
public class XtraReport2 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private XRLabel quyenso1;
    private XRLabel ten1;
    private XRLabel no1;
    private XRLabel so1;
    private XRLabel quydoi1;
    private XRLabel tygia1;
    private XRLabel danhan1;
    private XRLabel kemtheo1;
    private XRLabel sotien1;
    private XRLabel lydo1;
    private XRLabel xrLabel24;
    private XRLabel diachi2;
    private XRLabel xrLabel25;
    private XRLabel donvi1;
    private XRLabel tygia;
    private XRLabel danhan;
    private XRLabel xrLabel33;
    private XRLabel xrLabel32;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;
    private XRLabel xrLabel28;
    private XRLabel xrLabel27;
    private XRLabel diachi1;
    private XRLabel quydoi;
    private XRLabel co1;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private XRLabel kemtheo;
    private XRLabel bangchu;
    private XRLabel sotien;
    private XRLabel lydo;
    private XRLabel diachix;
    private XRLabel ten;
    private XRLabel co;
    private XRLabel no;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel donvi;
    private XRLabel diachiew;
    private XRLabel xrLabel1;
    private XRLabel so;
    private XRLabel ngay;
    private XRLabel thang;
    private XRLabel nam;
    private XRLabel quyenso;
    private XRLabel xrLabel31;
    private XRLabel xrLabel26;
    private XRLabel xrLabel37;
    private XRLabel dongtien;
    private XRPictureBox xrPictureBox1;
    private XRLabel n_nam;
    private XRLabel n_thang;
    private XRLabel n_ngay;
    private XRLabel xrLabel75;
    private XRLabel xrLabel76;
    private XRLabel xrLabel74;
    private XRLabel xrLabel73;
    private XRLabel xrLabel72;
    private XRPictureBox xrPictureBox2;
    private XRLabel xrLabel71;
    private XRLabel xrLabel70;
    private XRLabel xrLabel69;
    private XRLabel xrLabel68;
    private XRLabel xrLabel67;
    private XRLabel xrLabel66;
    private XRLabel xrLabel65;
    private XRLabel xrLabel64;
    private XRLabel xrLabel63;
    private XRLabel xrLabel62;
    private XRLabel xrLabel61;
    private XRLabel xrLabel60;
    private XRLabel xrLabel59;
    private XRLabel xrLabel58;
    private XRLabel xrLabel57;
    private XRLabel xrLabel56;
    private XRLabel xrLabel55;
    private XRLabel xrLabel54;
    private XRLabel xrLabel53;
    private XRLabel xrLabel52;
    private XRLabel xrLabel51;
    private XRLabel xrLabel50;
    private XRLabel xrLabel49;
    private XRLabel xrLabel48;
    private XRLabel xrLabel45;
    private XRLabel xrLabel44;
    private XRLabel xrLabel42;
    private XRLabel xrLabel41;
    private XRLabel xrLabel40;
    private XRLabel xrLabel39;
    private XRLabel xrLabel38;
    private XRLabel xrLabel36;
    private XRLabel xrLabel35;
    private XRLabel xrLabel34;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel80;
    private XRLabel xrLabel81;
    private XRLabel xrLabel82;
    private XRLabel xrLabel77;
    private XRLabel xrLabel78;
    private XRLabel xrLabel79;
    private XRLabel xrLabel47;
    private XRLabel xrLabel46;
    private XRLabel xrLabel43;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRLabel xrLabel21;
    private XRLabel xrLabel85;
    private XRLabel xrLabel86;
    private XRLabel xrLabel84;
    private XRLabel xrLabel83;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReport2()
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel85 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel86 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel84 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel83 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel80 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel81 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel82 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel77 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel78 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel79 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel61 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel62 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel63 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel64 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel65 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel66 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel67 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel68 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel69 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel70 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel71 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel72 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel73 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel74 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel76 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel75 = new DevExpress.XtraReports.UI.XRLabel();
            this.n_nam = new DevExpress.XtraReports.UI.XRLabel();
            this.n_thang = new DevExpress.XtraReports.UI.XRLabel();
            this.n_ngay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.dongtien = new DevExpress.XtraReports.UI.XRLabel();
            this.quyenso1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.no1 = new DevExpress.XtraReports.UI.XRLabel();
            this.so1 = new DevExpress.XtraReports.UI.XRLabel();
            this.quydoi1 = new DevExpress.XtraReports.UI.XRLabel();
            this.tygia1 = new DevExpress.XtraReports.UI.XRLabel();
            this.danhan1 = new DevExpress.XtraReports.UI.XRLabel();
            this.kemtheo1 = new DevExpress.XtraReports.UI.XRLabel();
            this.sotien1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lydo1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.diachi2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.donvi1 = new DevExpress.XtraReports.UI.XRLabel();
            this.tygia = new DevExpress.XtraReports.UI.XRLabel();
            this.danhan = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.diachi1 = new DevExpress.XtraReports.UI.XRLabel();
            this.quydoi = new DevExpress.XtraReports.UI.XRLabel();
            this.co1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.kemtheo = new DevExpress.XtraReports.UI.XRLabel();
            this.bangchu = new DevExpress.XtraReports.UI.XRLabel();
            this.sotien = new DevExpress.XtraReports.UI.XRLabel();
            this.lydo = new DevExpress.XtraReports.UI.XRLabel();
            this.diachix = new DevExpress.XtraReports.UI.XRLabel();
            this.ten = new DevExpress.XtraReports.UI.XRLabel();
            this.co = new DevExpress.XtraReports.UI.XRLabel();
            this.no = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.donvi = new DevExpress.XtraReports.UI.XRLabel();
            this.diachiew = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.so = new DevExpress.XtraReports.UI.XRLabel();
            this.ngay = new DevExpress.XtraReports.UI.XRLabel();
            this.thang = new DevExpress.XtraReports.UI.XRLabel();
            this.nam = new DevExpress.XtraReports.UI.XRLabel();
            this.quyenso = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.BackColor = System.Drawing.Color.Transparent;
            this.Detail.BorderColor = System.Drawing.Color.Black;
            this.Detail.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.Detail.Expanded = false;
            this.Detail.ForeColor = System.Drawing.Color.Black;
            this.Detail.HeightF = 10.63709F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseBackColor = false;
            this.Detail.StylePriority.UseBorderColor = false;
            this.Detail.StylePriority.UseBorderDashStyle = false;
            this.Detail.StylePriority.UseForeColor = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel85,
            this.xrLabel86,
            this.xrLabel84,
            this.xrLabel83,
            this.xrLabel21,
            this.xrLabel22,
            this.xrLabel23,
            this.xrLabel80,
            this.xrLabel81,
            this.xrLabel82,
            this.xrLabel43,
            this.xrLabel46,
            this.xrLabel47,
            this.xrLabel77,
            this.xrLabel78,
            this.xrLabel79,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel8,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel13,
            this.xrLabel14,
            this.xrLabel15,
            this.xrLabel16,
            this.xrLabel17,
            this.xrLabel18,
            this.xrLabel34,
            this.xrLabel35,
            this.xrLabel36,
            this.xrLabel38,
            this.xrLabel39,
            this.xrLabel40,
            this.xrLabel41,
            this.xrLabel42,
            this.xrLabel44,
            this.xrLabel45,
            this.xrLabel48,
            this.xrLabel49,
            this.xrLabel50,
            this.xrLabel51,
            this.xrLabel52,
            this.xrLabel53,
            this.xrLabel54,
            this.xrLabel55,
            this.xrLabel56,
            this.xrLabel57,
            this.xrLabel58,
            this.xrLabel59,
            this.xrLabel60,
            this.xrLabel61,
            this.xrLabel62,
            this.xrLabel63,
            this.xrLabel64,
            this.xrLabel65,
            this.xrLabel66,
            this.xrLabel67,
            this.xrLabel68,
            this.xrLabel69,
            this.xrLabel70,
            this.xrLabel71,
            this.xrPictureBox2,
            this.xrLabel72,
            this.xrLabel73,
            this.xrLabel74,
            this.xrLabel76,
            this.xrLabel75,
            this.n_nam,
            this.n_thang,
            this.n_ngay,
            this.xrPictureBox1,
            this.dongtien,
            this.quyenso1,
            this.ten1,
            this.no1,
            this.so1,
            this.quydoi1,
            this.tygia1,
            this.danhan1,
            this.kemtheo1,
            this.sotien1,
            this.lydo1,
            this.xrLabel24,
            this.diachi2,
            this.xrLabel25,
            this.donvi1,
            this.tygia,
            this.danhan,
            this.xrLabel33,
            this.xrLabel32,
            this.xrLabel30,
            this.xrLabel29,
            this.xrLabel28,
            this.xrLabel27,
            this.diachi1,
            this.quydoi,
            this.co1,
            this.xrLabel20,
            this.xrLabel19,
            this.kemtheo,
            this.bangchu,
            this.sotien,
            this.lydo,
            this.diachix,
            this.ten,
            this.co,
            this.no,
            this.xrLabel10,
            this.xrLabel9,
            this.donvi,
            this.diachiew,
            this.xrLabel1,
            this.so,
            this.ngay,
            this.thang,
            this.nam,
            this.quyenso,
            this.xrLabel31,
            this.xrLabel26,
            this.xrLabel37});
            this.TopMargin.Font = new System.Drawing.Font("Arial", 10.75F);
            this.TopMargin.HeightF = 1157.764F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseFont = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel85
            // 
            this.xrLabel85.AutoWidth = true;
            this.xrLabel85.CanGrow = false;
            this.xrLabel85.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel85.ForeColor = System.Drawing.Color.Black;
            this.xrLabel85.LocationFloat = new DevExpress.Utils.PointFloat(9.749947F, 367.4734F);
            this.xrLabel85.Name = "xrLabel85";
            this.xrLabel85.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel85.SizeF = new System.Drawing.SizeF(139.3058F, 16.99994F);
            this.xrLabel85.StylePriority.UseFont = false;
            this.xrLabel85.StylePriority.UseForeColor = false;
            this.xrLabel85.StylePriority.UseTextAlignment = false;
            this.xrLabel85.Text = "Bùi Thị Hồng Phương";
            this.xrLabel85.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel86
            // 
            this.xrLabel86.AutoWidth = true;
            this.xrLabel86.CanGrow = false;
            this.xrLabel86.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel86.ForeColor = System.Drawing.Color.Black;
            this.xrLabel86.LocationFloat = new DevExpress.Utils.PointFloat(479.4583F, 367.4734F);
            this.xrLabel86.Name = "xrLabel86";
            this.xrLabel86.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel86.SizeF = new System.Drawing.SizeF(151.3058F, 16.99994F);
            this.xrLabel86.StylePriority.UseFont = false;
            this.xrLabel86.StylePriority.UseForeColor = false;
            this.xrLabel86.StylePriority.UseTextAlignment = false;
            this.xrLabel86.Text = "Nguyễn Thị Thanh Hiển";
            this.xrLabel86.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel84
            // 
            this.xrLabel84.AutoWidth = true;
            this.xrLabel84.CanGrow = false;
            this.xrLabel84.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel84.ForeColor = System.Drawing.Color.Black;
            this.xrLabel84.LocationFloat = new DevExpress.Utils.PointFloat(479.4583F, 1010.538F);
            this.xrLabel84.Name = "xrLabel84";
            this.xrLabel84.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel84.SizeF = new System.Drawing.SizeF(151.3058F, 16.99994F);
            this.xrLabel84.StylePriority.UseFont = false;
            this.xrLabel84.StylePriority.UseForeColor = false;
            this.xrLabel84.StylePriority.UseTextAlignment = false;
            this.xrLabel84.Text = "Nguyễn Thị Thanh Hiển";
            this.xrLabel84.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel83
            // 
            this.xrLabel83.AutoWidth = true;
            this.xrLabel83.CanGrow = false;
            this.xrLabel83.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel83.ForeColor = System.Drawing.Color.Black;
            this.xrLabel83.LocationFloat = new DevExpress.Utils.PointFloat(9.749939F, 1010.538F);
            this.xrLabel83.Name = "xrLabel83";
            this.xrLabel83.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel83.SizeF = new System.Drawing.SizeF(139.3058F, 16.99994F);
            this.xrLabel83.StylePriority.UseFont = false;
            this.xrLabel83.StylePriority.UseForeColor = false;
            this.xrLabel83.StylePriority.UseTextAlignment = false;
            this.xrLabel83.Text = "Bùi Thị Hồng Phương";
            this.xrLabel83.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel21.ForeColor = System.Drawing.Color.Black;
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(736.2366F, 215.4394F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(31.79169F, 19F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseForeColor = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "năm ";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel22.ForeColor = System.Drawing.Color.Black;
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(661.0656F, 215.4394F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(43.45831F, 19F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseForeColor = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "Tháng ";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel23.ForeColor = System.Drawing.Color.Black;
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(588.4873F, 215.4394F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(39.51276F, 19F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UseForeColor = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.Text = "Ngày ";
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel80
            // 
            this.xrLabel80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_ngay", "{0:dd}")});
            this.xrLabel80.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel80.ForeColor = System.Drawing.Color.Black;
            this.xrLabel80.LocationFloat = new DevExpress.Utils.PointFloat(628.0001F, 215.4393F);
            this.xrLabel80.Name = "xrLabel80";
            this.xrLabel80.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel80.SizeF = new System.Drawing.SizeF(33.06543F, 19.00006F);
            this.xrLabel80.StylePriority.UseFont = false;
            this.xrLabel80.StylePriority.UseForeColor = false;
            this.xrLabel80.StylePriority.UseTextAlignment = false;
            this.xrLabel80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel81
            // 
            this.xrLabel81.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_thang", "{0:MM}")});
            this.xrLabel81.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel81.ForeColor = System.Drawing.Color.Black;
            this.xrLabel81.LocationFloat = new DevExpress.Utils.PointFloat(705.2218F, 215.4394F);
            this.xrLabel81.Name = "xrLabel81";
            this.xrLabel81.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel81.SizeF = new System.Drawing.SizeF(29.77832F, 19F);
            this.xrLabel81.StylePriority.UseFont = false;
            this.xrLabel81.StylePriority.UseForeColor = false;
            this.xrLabel81.StylePriority.UseTextAlignment = false;
            this.xrLabel81.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel82
            // 
            this.xrLabel82.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_nam", "{0:yyyy}")});
            this.xrLabel82.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel82.ForeColor = System.Drawing.Color.Black;
            this.xrLabel82.LocationFloat = new DevExpress.Utils.PointFloat(768.0282F, 215.4394F);
            this.xrLabel82.Name = "xrLabel82";
            this.xrLabel82.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel82.SizeF = new System.Drawing.SizeF(57.9718F, 19.00001F);
            this.xrLabel82.StylePriority.UseFont = false;
            this.xrLabel82.StylePriority.UseForeColor = false;
            this.xrLabel82.StylePriority.UseTextAlignment = false;
            this.xrLabel82.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel43
            // 
            this.xrLabel43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_nam", "{0:yyyy}")});
            this.xrLabel43.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel43.ForeColor = System.Drawing.Color.Black;
            this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(768.0282F, 873.8334F);
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel43.SizeF = new System.Drawing.SizeF(57.9718F, 19.00001F);
            this.xrLabel43.StylePriority.UseFont = false;
            this.xrLabel43.StylePriority.UseForeColor = false;
            this.xrLabel43.StylePriority.UseTextAlignment = false;
            this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel46
            // 
            this.xrLabel46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_thang", "{0:MM}")});
            this.xrLabel46.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel46.ForeColor = System.Drawing.Color.Black;
            this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(705.2218F, 873.8334F);
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel46.SizeF = new System.Drawing.SizeF(29.77832F, 19F);
            this.xrLabel46.StylePriority.UseFont = false;
            this.xrLabel46.StylePriority.UseForeColor = false;
            this.xrLabel46.StylePriority.UseTextAlignment = false;
            this.xrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel47
            // 
            this.xrLabel47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_ngay", "{0:dd}")});
            this.xrLabel47.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel47.ForeColor = System.Drawing.Color.Black;
            this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(627.9999F, 873.8333F);
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel47.SizeF = new System.Drawing.SizeF(33.06543F, 19.00006F);
            this.xrLabel47.StylePriority.UseFont = false;
            this.xrLabel47.StylePriority.UseForeColor = false;
            this.xrLabel47.StylePriority.UseTextAlignment = false;
            this.xrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel77
            // 
            this.xrLabel77.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel77.ForeColor = System.Drawing.Color.Black;
            this.xrLabel77.LocationFloat = new DevExpress.Utils.PointFloat(588.4872F, 873.8334F);
            this.xrLabel77.Name = "xrLabel77";
            this.xrLabel77.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel77.SizeF = new System.Drawing.SizeF(39.51276F, 19F);
            this.xrLabel77.StylePriority.UseFont = false;
            this.xrLabel77.StylePriority.UseForeColor = false;
            this.xrLabel77.StylePriority.UseTextAlignment = false;
            this.xrLabel77.Text = "Ngày ";
            this.xrLabel77.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel78
            // 
            this.xrLabel78.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel78.ForeColor = System.Drawing.Color.Black;
            this.xrLabel78.LocationFloat = new DevExpress.Utils.PointFloat(661.0655F, 873.8334F);
            this.xrLabel78.Name = "xrLabel78";
            this.xrLabel78.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel78.SizeF = new System.Drawing.SizeF(43.45831F, 19F);
            this.xrLabel78.StylePriority.UseFont = false;
            this.xrLabel78.StylePriority.UseForeColor = false;
            this.xrLabel78.StylePriority.UseTextAlignment = false;
            this.xrLabel78.Text = "Tháng ";
            this.xrLabel78.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel79
            // 
            this.xrLabel79.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel79.ForeColor = System.Drawing.Color.Black;
            this.xrLabel79.LocationFloat = new DevExpress.Utils.PointFloat(736.2365F, 873.8334F);
            this.xrLabel79.Name = "xrLabel79";
            this.xrLabel79.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel79.SizeF = new System.Drawing.SizeF(31.79169F, 19F);
            this.xrLabel79.StylePriority.UseFont = false;
            this.xrLabel79.StylePriority.UseForeColor = false;
            this.xrLabel79.StylePriority.UseTextAlignment = false;
            this.xrLabel79.Text = "năm ";
            this.xrLabel79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel2.ForeColor = System.Drawing.Color.Black;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(65.55004F, 1102.23F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(228F, 20F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "(Liên gửi ra ngoài phải đóng dấu)";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.ForeColor = System.Drawing.Color.Black;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(344.9153F, 901.6962F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(101.5F, 17F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Thủ quỹ";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.AutoWidth = true;
            this.xrLabel4.CanGrow = false;
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel4.ForeColor = System.Drawing.Color.Black;
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(344.9153F, 918.6962F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(101.5F, 17F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseForeColor = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "(Ký, họ tên)";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel5.ForeColor = System.Drawing.Color.Black;
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(366.9281F, 718.7569F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(67.49997F, 17F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseForeColor = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Quyển số:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel6.ForeColor = System.Drawing.Color.Black;
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(479.1365F, 693.7568F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(31.79163F, 18.99982F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseForeColor = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "năm ";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel7.ForeColor = System.Drawing.Color.Black;
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(400.9655F, 693.7568F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(43.45837F, 18.99994F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseForeColor = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Tháng ";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel8.ForeColor = System.Drawing.Color.Black;
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(331.3872F, 693.7568F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(39.51276F, 19F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseForeColor = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Ngày ";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel11.ForeColor = System.Drawing.Color.Black;
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(471.1851F, 673.0068F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(28.74307F, 17F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseForeColor = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Số: ";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.ForeColor = System.Drawing.Color.Black;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(333.4281F, 660.5068F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(137.2986F, 31F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseForeColor = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "PHIẾU CHI";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel13.ForeColor = System.Drawing.Color.Black;
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(4.428158F, 718.7569F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(58.80127F, 17F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseForeColor = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Địa chỉ : ";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel14.ForeColor = System.Drawing.Color.Black;
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(4.428158F, 701.7568F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(56.80127F, 17F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseForeColor = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Đơn vị : ";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.ForeColor = System.Drawing.Color.Black;
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(646.9281F, 660.5068F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(140.5F, 17F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseForeColor = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Mẫu số 02 - TT";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Font = new System.Drawing.Font("Arial", 8F);
            this.xrLabel16.ForeColor = System.Drawing.Color.Black;
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(597.4281F, 677.7568F);
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(218.5002F, 32F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseForeColor = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "( Ban hành theo QĐ số: 15/2006/QĐ-BTC\r\nNgày 20/03/2006 của bộ trưởng BTC )";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.ForeColor = System.Drawing.Color.Black;
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(669.1844F, 710.7568F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(38F, 17F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseForeColor = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "Nợ : ";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel18.ForeColor = System.Drawing.Color.Black;
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(669.1844F, 728.7568F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(38F, 17F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseForeColor = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "Có : ";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel34.ForeColor = System.Drawing.Color.Black;
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(65.55009F, 758.1962F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(151.7909F, 20F);
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.StylePriority.UseForeColor = false;
            this.xrLabel34.StylePriority.UseTextAlignment = false;
            this.xrLabel34.Text = "Họ tên người nhận tiền:";
            this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel35.ForeColor = System.Drawing.Color.Black;
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(65.55007F, 778.1962F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(55.45177F, 20F);
            this.xrLabel35.StylePriority.UseFont = false;
            this.xrLabel35.StylePriority.UseForeColor = false;
            this.xrLabel35.StylePriority.UseTextAlignment = false;
            this.xrLabel35.Text = "Địa chỉ:";
            this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel36.ForeColor = System.Drawing.Color.Black;
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(65.55006F, 798.1962F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(67.5268F, 20F);
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.StylePriority.UseForeColor = false;
            this.xrLabel36.StylePriority.UseTextAlignment = false;
            this.xrLabel36.Text = "Lý do chi: ";
            this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel38.ForeColor = System.Drawing.Color.Black;
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(65.55006F, 818.1962F);
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(55.45179F, 20F);
            this.xrLabel38.StylePriority.UseFont = false;
            this.xrLabel38.StylePriority.UseForeColor = false;
            this.xrLabel38.StylePriority.UseTextAlignment = false;
            this.xrLabel38.Text = "Số tiền: ";
            this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel39
            // 
            this.xrLabel39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "bangchu")});
            this.xrLabel39.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel39.ForeColor = System.Drawing.Color.Black;
            this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(164.9874F, 838.1962F);
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel39.SizeF = new System.Drawing.SizeF(550.5161F, 20F);
            this.xrLabel39.StylePriority.UseFont = false;
            this.xrLabel39.StylePriority.UseForeColor = false;
            this.xrLabel39.StylePriority.UseTextAlignment = false;
            this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel40
            // 
            this.xrLabel40.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel40.ForeColor = System.Drawing.Color.Black;
            this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(65.55005F, 858.1962F);
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel40.SizeF = new System.Drawing.SizeF(76.52682F, 20F);
            this.xrLabel40.StylePriority.UseFont = false;
            this.xrLabel40.StylePriority.UseForeColor = false;
            this.xrLabel40.StylePriority.UseTextAlignment = false;
            this.xrLabel40.Text = "Kèm theo:";
            this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel41
            // 
            this.xrLabel41.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel41.ForeColor = System.Drawing.Color.Black;
            this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(362.2038F, 818.1962F);
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel41.SizeF = new System.Drawing.SizeF(352.978F, 20F);
            this.xrLabel41.StylePriority.UseFont = false;
            this.xrLabel41.StylePriority.UseForeColor = false;
            this.xrLabel41.StylePriority.UseTextAlignment = false;
            this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel42
            // 
            this.xrLabel42.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel42.ForeColor = System.Drawing.Color.Black;
            this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(309.0769F, 858.1962F);
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel42.SizeF = new System.Drawing.SizeF(89.99994F, 20F);
            this.xrLabel42.StylePriority.UseFont = false;
            this.xrLabel42.StylePriority.UseForeColor = false;
            this.xrLabel42.StylePriority.UseTextAlignment = false;
            this.xrLabel42.Text = "chứng từ gốc";
            this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel44
            // 
            this.xrLabel44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tk_co")});
            this.xrLabel44.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel44.ForeColor = System.Drawing.Color.Black;
            this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(707.1845F, 728.7568F);
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel44.SizeF = new System.Drawing.SizeF(80.24359F, 17F);
            this.xrLabel44.StylePriority.UseFont = false;
            this.xrLabel44.StylePriority.UseForeColor = false;
            this.xrLabel44.StylePriority.UseTextAlignment = false;
            this.xrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel45
            // 
            this.xrLabel45.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel45.ForeColor = System.Drawing.Color.Black;
            this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(65.55009F, 1082.23F);
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel45.SizeF = new System.Drawing.SizeF(119.4621F, 20F);
            this.xrLabel45.StylePriority.UseFont = false;
            this.xrLabel45.StylePriority.UseForeColor = false;
            this.xrLabel45.StylePriority.UseTextAlignment = false;
            this.xrLabel45.Text = "+ Số tiền quy đổi :";
            this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel48
            // 
            this.xrLabel48.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(63.22997F, 718.7568F);
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel48.SizeF = new System.Drawing.SizeF(296.67F, 17F);
            this.xrLabel48.StylePriority.UseFont = false;
            this.xrLabel48.StylePriority.UseTextAlignment = false;
            this.xrLabel48.Text = "Khu công nghiệp Bắc Chu Lai, Xã Núi Thành, Thành phố Đà Nẵng, Việt Nam";
            this.xrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel49
            // 
            this.xrLabel49.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel49.ForeColor = System.Drawing.Color.Black;
            this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(496.9281F, 904.6962F);
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel49.SizeF = new System.Drawing.SizeF(115.5F, 17F);
            this.xrLabel49.StylePriority.UseFont = false;
            this.xrLabel49.StylePriority.UseForeColor = false;
            this.xrLabel49.StylePriority.UseTextAlignment = false;
            this.xrLabel49.Text = "Kế toán trưởng";
            this.xrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel50
            // 
            this.xrLabel50.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel50.ForeColor = System.Drawing.Color.Black;
            this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(652.9281F, 904.6962F);
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel50.SizeF = new System.Drawing.SizeF(153.0001F, 17F);
            this.xrLabel50.StylePriority.UseFont = false;
            this.xrLabel50.StylePriority.UseForeColor = false;
            this.xrLabel50.StylePriority.UseTextAlignment = false;
            this.xrLabel50.Text = "Giám đốc";
            this.xrLabel50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel51
            // 
            this.xrLabel51.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel51.ForeColor = System.Drawing.Color.Black;
            this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(15.7711F, 918.6962F);
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel51.SizeF = new System.Drawing.SizeF(126.3058F, 17F);
            this.xrLabel51.StylePriority.UseFont = false;
            this.xrLabel51.StylePriority.UseForeColor = false;
            this.xrLabel51.StylePriority.UseTextAlignment = false;
            this.xrLabel51.Text = "(Ký, họ tên)";
            this.xrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel52
            // 
            this.xrLabel52.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel52.ForeColor = System.Drawing.Color.Black;
            this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(178.6582F, 918.6962F);
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel52.SizeF = new System.Drawing.SizeF(123.9326F, 17F);
            this.xrLabel52.StylePriority.UseFont = false;
            this.xrLabel52.StylePriority.UseForeColor = false;
            this.xrLabel52.StylePriority.UseTextAlignment = false;
            this.xrLabel52.Text = "(Ký, họ tên)";
            this.xrLabel52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel53
            // 
            this.xrLabel53.AutoWidth = true;
            this.xrLabel53.CanGrow = false;
            this.xrLabel53.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel53.ForeColor = System.Drawing.Color.Black;
            this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(496.9281F, 921.6962F);
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel53.SizeF = new System.Drawing.SizeF(115.4999F, 17F);
            this.xrLabel53.StylePriority.UseFont = false;
            this.xrLabel53.StylePriority.UseForeColor = false;
            this.xrLabel53.StylePriority.UseTextAlignment = false;
            this.xrLabel53.Text = "(Ký, họ tên)";
            this.xrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel54
            // 
            this.xrLabel54.AutoWidth = true;
            this.xrLabel54.CanGrow = false;
            this.xrLabel54.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel54.ForeColor = System.Drawing.Color.Black;
            this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(652.9281F, 921.6962F);
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel54.SizeF = new System.Drawing.SizeF(153.0001F, 17F);
            this.xrLabel54.StylePriority.UseFont = false;
            this.xrLabel54.StylePriority.UseForeColor = false;
            this.xrLabel54.StylePriority.UseTextAlignment = false;
            this.xrLabel54.Text = "(Ký, họ tên, đóng dấu)";
            this.xrLabel54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel55
            // 
            this.xrLabel55.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel55.ForeColor = System.Drawing.Color.Black;
            this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(65.55007F, 1042.23F);
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel55.SizeF = new System.Drawing.SizeF(231.4518F, 20F);
            this.xrLabel55.StylePriority.UseFont = false;
            this.xrLabel55.StylePriority.UseForeColor = false;
            this.xrLabel55.StylePriority.UseTextAlignment = false;
            this.xrLabel55.Text = " Đã nhận đủ số tiền (viết bằng chữ) :";
            this.xrLabel55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel56
            // 
            this.xrLabel56.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel56.ForeColor = System.Drawing.Color.Black;
            this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(65.55007F, 1062.23F);
            this.xrLabel56.Name = "xrLabel56";
            this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel56.SizeF = new System.Drawing.SizeF(238.0408F, 20F);
            this.xrLabel56.StylePriority.UseFont = false;
            this.xrLabel56.StylePriority.UseForeColor = false;
            this.xrLabel56.StylePriority.UseTextAlignment = false;
            this.xrLabel56.Text = "+ Tỷ giá ngoại tệ (vàng, bạc, đá quý) : ";
            this.xrLabel56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel57
            // 
            this.xrLabel57.Font = new System.Drawing.Font("Arial", 9F);
            this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(61.22942F, 701.7568F);
            this.xrLabel57.Name = "xrLabel57";
            this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel57.SizeF = new System.Drawing.SizeF(259.1578F, 17F);
            this.xrLabel57.StylePriority.UseFont = false;
            this.xrLabel57.StylePriority.UseTextAlignment = false;
            this.xrLabel57.Text = "CÔNG TY TNHH VINH GIA";
            this.xrLabel57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel58
            // 
            this.xrLabel58.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel58.ForeColor = System.Drawing.Color.Black;
            this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(178.6582F, 901.6962F);
            this.xrLabel58.Name = "xrLabel58";
            this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel58.SizeF = new System.Drawing.SizeF(123.9326F, 17F);
            this.xrLabel58.StylePriority.UseFont = false;
            this.xrLabel58.StylePriority.UseForeColor = false;
            this.xrLabel58.StylePriority.UseTextAlignment = false;
            this.xrLabel58.Text = "Người nhận tiền";
            this.xrLabel58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel59
            // 
            this.xrLabel59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachi2")});
            this.xrLabel59.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(121.0019F, 778.1962F);
            this.xrLabel59.Name = "xrLabel59";
            this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel59.SizeF = new System.Drawing.SizeF(594.5017F, 20F);
            this.xrLabel59.StylePriority.UseFont = false;
            this.xrLabel59.StylePriority.UseTextAlignment = false;
            this.xrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel60
            // 
            this.xrLabel60.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel60.ForeColor = System.Drawing.Color.Black;
            this.xrLabel60.LocationFloat = new DevExpress.Utils.PointFloat(15.7711F, 901.6962F);
            this.xrLabel60.Name = "xrLabel60";
            this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel60.SizeF = new System.Drawing.SizeF(126.3058F, 17F);
            this.xrLabel60.StylePriority.UseFont = false;
            this.xrLabel60.StylePriority.UseForeColor = false;
            this.xrLabel60.StylePriority.UseTextAlignment = false;
            this.xrLabel60.Text = "Người lập phiếu";
            this.xrLabel60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel61
            // 
            this.xrLabel61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lydo")});
            this.xrLabel61.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel61.LocationFloat = new DevExpress.Utils.PointFloat(133.0769F, 798.1962F);
            this.xrLabel61.Name = "xrLabel61";
            this.xrLabel61.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel61.SizeF = new System.Drawing.SizeF(582.4266F, 20F);
            this.xrLabel61.StylePriority.UseFont = false;
            this.xrLabel61.StylePriority.UseTextAlignment = false;
            this.xrLabel61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel62
            // 
            this.xrLabel62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sotien")});
            this.xrLabel62.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel62.LocationFloat = new DevExpress.Utils.PointFloat(121.0019F, 818.1962F);
            this.xrLabel62.Name = "xrLabel62";
            this.xrLabel62.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel62.SizeF = new System.Drawing.SizeF(174F, 20F);
            this.xrLabel62.StylePriority.UseFont = false;
            this.xrLabel62.StylePriority.UseTextAlignment = false;
            this.xrLabel62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel63
            // 
            this.xrLabel63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sochungtu")});
            this.xrLabel63.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel63.LocationFloat = new DevExpress.Utils.PointFloat(142.0769F, 858.1962F);
            this.xrLabel63.Name = "xrLabel63";
            this.xrLabel63.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel63.SizeF = new System.Drawing.SizeF(167F, 20F);
            this.xrLabel63.StylePriority.UseFont = false;
            this.xrLabel63.StylePriority.UseTextAlignment = false;
            this.xrLabel63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel64
            // 
            this.xrLabel64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "danhan1")});
            this.xrLabel64.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel64.LocationFloat = new DevExpress.Utils.PointFloat(297.0019F, 1042.23F);
            this.xrLabel64.Name = "xrLabel64";
            this.xrLabel64.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel64.SizeF = new System.Drawing.SizeF(430.4793F, 20F);
            this.xrLabel64.StylePriority.UseFont = false;
            this.xrLabel64.StylePriority.UseTextAlignment = false;
            this.xrLabel64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel65
            // 
            this.xrLabel65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tygia")});
            this.xrLabel65.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel65.LocationFloat = new DevExpress.Utils.PointFloat(303.5909F, 1062.23F);
            this.xrLabel65.Name = "xrLabel65";
            this.xrLabel65.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel65.SizeF = new System.Drawing.SizeF(423.8903F, 20F);
            this.xrLabel65.StylePriority.UseFont = false;
            this.xrLabel65.StylePriority.UseTextAlignment = false;
            this.xrLabel65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel66
            // 
            this.xrLabel66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "quydoi_vn")});
            this.xrLabel66.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel66.LocationFloat = new DevExpress.Utils.PointFloat(185.0122F, 1082.23F);
            this.xrLabel66.Name = "xrLabel66";
            this.xrLabel66.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel66.SizeF = new System.Drawing.SizeF(542.469F, 20F);
            this.xrLabel66.StylePriority.UseFont = false;
            this.xrLabel66.StylePriority.UseTextAlignment = false;
            this.xrLabel66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel67
            // 
            this.xrLabel67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sophieu")});
            this.xrLabel67.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel67.LocationFloat = new DevExpress.Utils.PointFloat(499.9282F, 673.0068F);
            this.xrLabel67.Name = "xrLabel67";
            this.xrLabel67.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel67.SizeF = new System.Drawing.SizeF(90.49997F, 17F);
            this.xrLabel67.StylePriority.UseFont = false;
            this.xrLabel67.StylePriority.UseTextAlignment = false;
            this.xrLabel67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel68
            // 
            this.xrLabel68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tk_no")});
            this.xrLabel68.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel68.ForeColor = System.Drawing.Color.Black;
            this.xrLabel68.LocationFloat = new DevExpress.Utils.PointFloat(707.1844F, 710.7568F);
            this.xrLabel68.Name = "xrLabel68";
            this.xrLabel68.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel68.SizeF = new System.Drawing.SizeF(80.24359F, 17F);
            this.xrLabel68.StylePriority.UseFont = false;
            this.xrLabel68.StylePriority.UseForeColor = false;
            this.xrLabel68.StylePriority.UseTextAlignment = false;
            this.xrLabel68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel69
            // 
            this.xrLabel69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten")});
            this.xrLabel69.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel69.LocationFloat = new DevExpress.Utils.PointFloat(217.341F, 758.1962F);
            this.xrLabel69.Name = "xrLabel69";
            this.xrLabel69.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel69.SizeF = new System.Drawing.SizeF(498.1624F, 19.99999F);
            this.xrLabel69.StylePriority.UseFont = false;
            this.xrLabel69.StylePriority.UseTextAlignment = false;
            this.xrLabel69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel70
            // 
            this.xrLabel70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "quyenso1")});
            this.xrLabel70.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel70.LocationFloat = new DevExpress.Utils.PointFloat(434.4281F, 718.7568F);
            this.xrLabel70.Name = "xrLabel70";
            this.xrLabel70.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel70.SizeF = new System.Drawing.SizeF(102F, 17F);
            this.xrLabel70.StylePriority.UseFont = false;
            this.xrLabel70.StylePriority.UseTextAlignment = false;
            this.xrLabel70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel71
            // 
            this.xrLabel71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dongtien")});
            this.xrLabel71.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel71.LocationFloat = new DevExpress.Utils.PointFloat(295.0018F, 818.1962F);
            this.xrLabel71.Name = "xrLabel71";
            this.xrLabel71.SizeF = new System.Drawing.SizeF(66.20456F, 20F);
            this.xrLabel71.StylePriority.UseTextAlignment = false;
            this.xrLabel71.Text = "đồng";
            this.xrLabel71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.ImageUrl = "~\\images\\anco_logo_25px.png";
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(9.428148F, 653.7568F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(111.5737F, 48.00001F);
            // 
            // xrLabel72
            // 
            this.xrLabel72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_ngay", "{0:dd}")});
            this.xrLabel72.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel72.ForeColor = System.Drawing.Color.Black;
            this.xrLabel72.LocationFloat = new DevExpress.Utils.PointFloat(370.9001F, 693.7568F);
            this.xrLabel72.Name = "xrLabel72";
            this.xrLabel72.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel72.SizeF = new System.Drawing.SizeF(30.06549F, 19F);
            this.xrLabel72.StylePriority.UseFont = false;
            this.xrLabel72.StylePriority.UseForeColor = false;
            this.xrLabel72.StylePriority.UseTextAlignment = false;
            this.xrLabel72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel73
            // 
            this.xrLabel73.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_thang", "{0:MM}")});
            this.xrLabel73.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel73.ForeColor = System.Drawing.Color.Black;
            this.xrLabel73.LocationFloat = new DevExpress.Utils.PointFloat(448.1217F, 693.7568F);
            this.xrLabel73.Name = "xrLabel73";
            this.xrLabel73.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel73.SizeF = new System.Drawing.SizeF(29.77832F, 18.99988F);
            this.xrLabel73.StylePriority.UseFont = false;
            this.xrLabel73.StylePriority.UseForeColor = false;
            this.xrLabel73.StylePriority.UseTextAlignment = false;
            this.xrLabel73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel74
            // 
            this.xrLabel74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_nam", "{0:yyyy}")});
            this.xrLabel74.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel74.ForeColor = System.Drawing.Color.Black;
            this.xrLabel74.LocationFloat = new DevExpress.Utils.PointFloat(510.9282F, 693.7568F);
            this.xrLabel74.Name = "xrLabel74";
            this.xrLabel74.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel74.SizeF = new System.Drawing.SizeF(57.9718F, 19.00001F);
            this.xrLabel74.StylePriority.UseFont = false;
            this.xrLabel74.StylePriority.UseForeColor = false;
            this.xrLabel74.StylePriority.UseTextAlignment = false;
            this.xrLabel74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel76
            // 
            this.xrLabel76.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel76.ForeColor = System.Drawing.Color.Black;
            this.xrLabel76.LocationFloat = new DevExpress.Utils.PointFloat(65.55006F, 838.1962F);
            this.xrLabel76.Name = "xrLabel76";
            this.xrLabel76.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel76.SizeF = new System.Drawing.SizeF(99.33808F, 20F);
            this.xrLabel76.StylePriority.UseFont = false;
            this.xrLabel76.StylePriority.UseForeColor = false;
            this.xrLabel76.StylePriority.UseTextAlignment = false;
            this.xrLabel76.Text = "Viết bằng chữ:";
            this.xrLabel76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel75
            // 
            this.xrLabel75.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel75.ForeColor = System.Drawing.Color.Black;
            this.xrLabel75.LocationFloat = new DevExpress.Utils.PointFloat(65.87185F, 186.4394F);
            this.xrLabel75.Name = "xrLabel75";
            this.xrLabel75.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel75.SizeF = new System.Drawing.SizeF(99.33808F, 20F);
            this.xrLabel75.StylePriority.UseFont = false;
            this.xrLabel75.StylePriority.UseForeColor = false;
            this.xrLabel75.StylePriority.UseTextAlignment = false;
            this.xrLabel75.Text = "Viết bằng chữ:";
            this.xrLabel75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // n_nam
            // 
            this.n_nam.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_nam", "{0:yyyy}")});
            this.n_nam.Font = new System.Drawing.Font("Arial", 10F);
            this.n_nam.ForeColor = System.Drawing.Color.Black;
            this.n_nam.LocationFloat = new DevExpress.Utils.PointFloat(511.25F, 42.00001F);
            this.n_nam.Name = "n_nam";
            this.n_nam.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.n_nam.SizeF = new System.Drawing.SizeF(57.9718F, 19.00001F);
            this.n_nam.StylePriority.UseFont = false;
            this.n_nam.StylePriority.UseForeColor = false;
            this.n_nam.StylePriority.UseTextAlignment = false;
            this.n_nam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // n_thang
            // 
            this.n_thang.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_thang", "{0:MM}")});
            this.n_thang.Font = new System.Drawing.Font("Arial", 10F);
            this.n_thang.ForeColor = System.Drawing.Color.Black;
            this.n_thang.LocationFloat = new DevExpress.Utils.PointFloat(448.4435F, 42.00002F);
            this.n_thang.Name = "n_thang";
            this.n_thang.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.n_thang.SizeF = new System.Drawing.SizeF(29.77832F, 19F);
            this.n_thang.StylePriority.UseFont = false;
            this.n_thang.StylePriority.UseForeColor = false;
            this.n_thang.StylePriority.UseTextAlignment = false;
            this.n_thang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // n_ngay
            // 
            this.n_ngay.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "n_ngay", "{0:dd}")});
            this.n_ngay.Font = new System.Drawing.Font("Arial", 10F);
            this.n_ngay.ForeColor = System.Drawing.Color.Black;
            this.n_ngay.LocationFloat = new DevExpress.Utils.PointFloat(371.2218F, 42.00001F);
            this.n_ngay.Name = "n_ngay";
            this.n_ngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.n_ngay.SizeF = new System.Drawing.SizeF(30.06549F, 19F);
            this.n_ngay.StylePriority.UseFont = false;
            this.n_ngay.StylePriority.UseForeColor = false;
            this.n_ngay.StylePriority.UseTextAlignment = false;
            this.n_ngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "~\\images\\anco_logo_25px.png";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(9.749939F, 2.000008F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(111.5737F, 48.00001F);
            // 
            // dongtien
            // 
            this.dongtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dongtien")});
            this.dongtien.Font = new System.Drawing.Font("Arial", 10F);
            this.dongtien.LocationFloat = new DevExpress.Utils.PointFloat(295.3236F, 166.4394F);
            this.dongtien.Name = "dongtien";
            this.dongtien.SizeF = new System.Drawing.SizeF(66.20456F, 20F);
            this.dongtien.StylePriority.UseTextAlignment = false;
            this.dongtien.Text = "đồng";
            this.dongtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // quyenso1
            // 
            this.quyenso1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "quyenso1")});
            this.quyenso1.Font = new System.Drawing.Font("Arial", 10F);
            this.quyenso1.LocationFloat = new DevExpress.Utils.PointFloat(434.7499F, 67.00001F);
            this.quyenso1.Name = "quyenso1";
            this.quyenso1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.quyenso1.SizeF = new System.Drawing.SizeF(102F, 17F);
            this.quyenso1.StylePriority.UseFont = false;
            this.quyenso1.StylePriority.UseTextAlignment = false;
            this.quyenso1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ten1
            // 
            this.ten1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ten")});
            this.ten1.Font = new System.Drawing.Font("Arial", 10F);
            this.ten1.LocationFloat = new DevExpress.Utils.PointFloat(217.6628F, 106.4394F);
            this.ten1.Name = "ten1";
            this.ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ten1.SizeF = new System.Drawing.SizeF(498.1624F, 19.99999F);
            this.ten1.StylePriority.UseFont = false;
            this.ten1.StylePriority.UseTextAlignment = false;
            this.ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // no1
            // 
            this.no1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tk_no")});
            this.no1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.no1.ForeColor = System.Drawing.Color.Black;
            this.no1.LocationFloat = new DevExpress.Utils.PointFloat(707.5062F, 59.00002F);
            this.no1.Name = "no1";
            this.no1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.no1.SizeF = new System.Drawing.SizeF(80.24359F, 17F);
            this.no1.StylePriority.UseFont = false;
            this.no1.StylePriority.UseForeColor = false;
            this.no1.StylePriority.UseTextAlignment = false;
            this.no1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // so1
            // 
            this.so1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sophieu")});
            this.so1.Font = new System.Drawing.Font("Arial", 10F);
            this.so1.LocationFloat = new DevExpress.Utils.PointFloat(500.25F, 21.25001F);
            this.so1.Name = "so1";
            this.so1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.so1.SizeF = new System.Drawing.SizeF(90.49997F, 17F);
            this.so1.StylePriority.UseFont = false;
            this.so1.StylePriority.UseTextAlignment = false;
            this.so1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // quydoi1
            // 
            this.quydoi1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "quydoi_vn")});
            this.quydoi1.Font = new System.Drawing.Font("Arial", 10F);
            this.quydoi1.LocationFloat = new DevExpress.Utils.PointFloat(185.334F, 440.4734F);
            this.quydoi1.Name = "quydoi1";
            this.quydoi1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.quydoi1.SizeF = new System.Drawing.SizeF(542.469F, 20F);
            this.quydoi1.StylePriority.UseFont = false;
            this.quydoi1.StylePriority.UseTextAlignment = false;
            this.quydoi1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // tygia1
            // 
            this.tygia1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tygia")});
            this.tygia1.Font = new System.Drawing.Font("Arial", 10F);
            this.tygia1.LocationFloat = new DevExpress.Utils.PointFloat(303.9127F, 420.4734F);
            this.tygia1.Name = "tygia1";
            this.tygia1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tygia1.SizeF = new System.Drawing.SizeF(423.8903F, 20F);
            this.tygia1.StylePriority.UseFont = false;
            this.tygia1.StylePriority.UseTextAlignment = false;
            this.tygia1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // danhan1
            // 
            this.danhan1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "danhan1")});
            this.danhan1.Font = new System.Drawing.Font("Arial", 10F);
            this.danhan1.LocationFloat = new DevExpress.Utils.PointFloat(297.3236F, 400.4734F);
            this.danhan1.Name = "danhan1";
            this.danhan1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.danhan1.SizeF = new System.Drawing.SizeF(430.4793F, 20F);
            this.danhan1.StylePriority.UseFont = false;
            this.danhan1.StylePriority.UseTextAlignment = false;
            this.danhan1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // kemtheo1
            // 
            this.kemtheo1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sochungtu")});
            this.kemtheo1.Font = new System.Drawing.Font("Arial", 10F);
            this.kemtheo1.LocationFloat = new DevExpress.Utils.PointFloat(142.3987F, 206.4394F);
            this.kemtheo1.Name = "kemtheo1";
            this.kemtheo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.kemtheo1.SizeF = new System.Drawing.SizeF(167F, 20F);
            this.kemtheo1.StylePriority.UseFont = false;
            this.kemtheo1.StylePriority.UseTextAlignment = false;
            this.kemtheo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // sotien1
            // 
            this.sotien1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sotien")});
            this.sotien1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.sotien1.LocationFloat = new DevExpress.Utils.PointFloat(121.3236F, 166.4394F);
            this.sotien1.Name = "sotien1";
            this.sotien1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.sotien1.SizeF = new System.Drawing.SizeF(174F, 20F);
            this.sotien1.StylePriority.UseFont = false;
            this.sotien1.StylePriority.UseTextAlignment = false;
            this.sotien1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lydo1
            // 
            this.lydo1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lydo")});
            this.lydo1.Font = new System.Drawing.Font("Arial", 10F);
            this.lydo1.LocationFloat = new DevExpress.Utils.PointFloat(133.3987F, 146.4394F);
            this.lydo1.Name = "lydo1";
            this.lydo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lydo1.SizeF = new System.Drawing.SizeF(582.4266F, 20F);
            this.lydo1.StylePriority.UseFont = false;
            this.lydo1.StylePriority.UseTextAlignment = false;
            this.lydo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel24.ForeColor = System.Drawing.Color.Black;
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(16.09289F, 247.9394F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(126.3058F, 17F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseForeColor = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "Người lập phiếu";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // diachi2
            // 
            this.diachi2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachi2")});
            this.diachi2.Font = new System.Drawing.Font("Arial", 10F);
            this.diachi2.LocationFloat = new DevExpress.Utils.PointFloat(121.3236F, 126.4394F);
            this.diachi2.Name = "diachi2";
            this.diachi2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.diachi2.SizeF = new System.Drawing.SizeF(594.5017F, 20F);
            this.diachi2.StylePriority.UseFont = false;
            this.diachi2.StylePriority.UseTextAlignment = false;
            this.diachi2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel25.ForeColor = System.Drawing.Color.Black;
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(178.98F, 247.9394F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(123.9326F, 17F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseForeColor = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "Người nhận tiền";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // donvi1
            // 
            this.donvi1.Font = new System.Drawing.Font("Arial", 9F);
            this.donvi1.LocationFloat = new DevExpress.Utils.PointFloat(61.55121F, 50.00002F);
            this.donvi1.Name = "donvi1";
            this.donvi1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.donvi1.SizeF = new System.Drawing.SizeF(259.1578F, 17F);
            this.donvi1.StylePriority.UseFont = false;
            this.donvi1.StylePriority.UseTextAlignment = false;
            this.donvi1.Text = "CÔNG TY TNHH VINH GIA";
            this.donvi1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // tygia
            // 
            this.tygia.Font = new System.Drawing.Font("Arial", 10F);
            this.tygia.ForeColor = System.Drawing.Color.Black;
            this.tygia.LocationFloat = new DevExpress.Utils.PointFloat(65.87186F, 420.4734F);
            this.tygia.Name = "tygia";
            this.tygia.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tygia.SizeF = new System.Drawing.SizeF(238.0408F, 20F);
            this.tygia.StylePriority.UseFont = false;
            this.tygia.StylePriority.UseForeColor = false;
            this.tygia.StylePriority.UseTextAlignment = false;
            this.tygia.Text = "+ Tỷ giá ngoại tệ (vàng, bạc, đá quý) : ";
            this.tygia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // danhan
            // 
            this.danhan.Font = new System.Drawing.Font("Arial", 10F);
            this.danhan.ForeColor = System.Drawing.Color.Black;
            this.danhan.LocationFloat = new DevExpress.Utils.PointFloat(65.87186F, 400.4734F);
            this.danhan.Name = "danhan";
            this.danhan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.danhan.SizeF = new System.Drawing.SizeF(231.4518F, 20F);
            this.danhan.StylePriority.UseFont = false;
            this.danhan.StylePriority.UseForeColor = false;
            this.danhan.StylePriority.UseTextAlignment = false;
            this.danhan.Text = " Đã nhận đủ số tiền (viết bằng chữ) :";
            this.danhan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel33
            // 
            this.xrLabel33.AutoWidth = true;
            this.xrLabel33.CanGrow = false;
            this.xrLabel33.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel33.ForeColor = System.Drawing.Color.Black;
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(653.2499F, 267.9394F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(153.0001F, 17F);
            this.xrLabel33.StylePriority.UseFont = false;
            this.xrLabel33.StylePriority.UseForeColor = false;
            this.xrLabel33.StylePriority.UseTextAlignment = false;
            this.xrLabel33.Text = "(Ký, họ tên, đóng dấu)";
            this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel32
            // 
            this.xrLabel32.AutoWidth = true;
            this.xrLabel32.CanGrow = false;
            this.xrLabel32.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel32.ForeColor = System.Drawing.Color.Black;
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(497.2499F, 267.9394F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(115.4999F, 17F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UseForeColor = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            this.xrLabel32.Text = "(Ký, họ tên)";
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel30.ForeColor = System.Drawing.Color.Black;
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(178.98F, 264.9394F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(123.9326F, 17F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseForeColor = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "(Ký, họ tên)";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel29.ForeColor = System.Drawing.Color.Black;
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(16.09288F, 264.9394F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(126.3058F, 17F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseForeColor = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.Text = "(Ký, họ tên)";
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel28.ForeColor = System.Drawing.Color.Black;
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(653.2499F, 250.9394F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(153.0001F, 17F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseForeColor = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "Giám đốc";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel27
            // 
            this.xrLabel27.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel27.ForeColor = System.Drawing.Color.Black;
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(497.2499F, 250.9394F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(115.5F, 17F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UseForeColor = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.Text = "Kế toán trưởng";
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // diachi1
            // 
            this.diachi1.Font = new System.Drawing.Font("Arial", 9F);
            this.diachi1.LocationFloat = new DevExpress.Utils.PointFloat(63.55177F, 67.00001F);
            this.diachi1.Name = "diachi1";
            this.diachi1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.diachi1.SizeF = new System.Drawing.SizeF(296.67F, 17F);
            this.diachi1.StylePriority.UseFont = false;
            this.diachi1.StylePriority.UseTextAlignment = false;
            this.diachi1.Text = "3B/2 Kp.1B,P.An Phú, TX.Thuận An, BD";
            this.diachi1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // quydoi
            // 
            this.quydoi.Font = new System.Drawing.Font("Arial", 10F);
            this.quydoi.ForeColor = System.Drawing.Color.Black;
            this.quydoi.LocationFloat = new DevExpress.Utils.PointFloat(65.87187F, 440.4734F);
            this.quydoi.Name = "quydoi";
            this.quydoi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.quydoi.SizeF = new System.Drawing.SizeF(119.4621F, 20F);
            this.quydoi.StylePriority.UseFont = false;
            this.quydoi.StylePriority.UseForeColor = false;
            this.quydoi.StylePriority.UseTextAlignment = false;
            this.quydoi.Text = "+ Số tiền quy đổi :";
            this.quydoi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // co1
            // 
            this.co1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tk_co")});
            this.co1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.co1.ForeColor = System.Drawing.Color.Black;
            this.co1.LocationFloat = new DevExpress.Utils.PointFloat(707.5063F, 77.00003F);
            this.co1.Name = "co1";
            this.co1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.co1.SizeF = new System.Drawing.SizeF(80.24359F, 17F);
            this.co1.StylePriority.UseFont = false;
            this.co1.StylePriority.UseForeColor = false;
            this.co1.StylePriority.UseTextAlignment = false;
            this.co1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel20.ForeColor = System.Drawing.Color.Black;
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(309.3987F, 206.4394F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(89.99994F, 20F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseForeColor = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "chứng từ gốc";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel19.ForeColor = System.Drawing.Color.Black;
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(362.5256F, 166.4394F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(352.978F, 20F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseForeColor = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // kemtheo
            // 
            this.kemtheo.Font = new System.Drawing.Font("Arial", 10F);
            this.kemtheo.ForeColor = System.Drawing.Color.Black;
            this.kemtheo.LocationFloat = new DevExpress.Utils.PointFloat(65.87184F, 206.4394F);
            this.kemtheo.Name = "kemtheo";
            this.kemtheo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.kemtheo.SizeF = new System.Drawing.SizeF(76.52682F, 20F);
            this.kemtheo.StylePriority.UseFont = false;
            this.kemtheo.StylePriority.UseForeColor = false;
            this.kemtheo.StylePriority.UseTextAlignment = false;
            this.kemtheo.Text = "Kèm theo:";
            this.kemtheo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // bangchu
            // 
            this.bangchu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "bangchu")});
            this.bangchu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.bangchu.ForeColor = System.Drawing.Color.Black;
            this.bangchu.LocationFloat = new DevExpress.Utils.PointFloat(165.3091F, 186.4394F);
            this.bangchu.Name = "bangchu";
            this.bangchu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.bangchu.SizeF = new System.Drawing.SizeF(550.5161F, 20F);
            this.bangchu.StylePriority.UseFont = false;
            this.bangchu.StylePriority.UseForeColor = false;
            this.bangchu.StylePriority.UseTextAlignment = false;
            this.bangchu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // sotien
            // 
            this.sotien.Font = new System.Drawing.Font("Arial", 10F);
            this.sotien.ForeColor = System.Drawing.Color.Black;
            this.sotien.LocationFloat = new DevExpress.Utils.PointFloat(65.87185F, 166.4394F);
            this.sotien.Name = "sotien";
            this.sotien.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.sotien.SizeF = new System.Drawing.SizeF(55.45179F, 20F);
            this.sotien.StylePriority.UseFont = false;
            this.sotien.StylePriority.UseForeColor = false;
            this.sotien.StylePriority.UseTextAlignment = false;
            this.sotien.Text = "Số tiền: ";
            this.sotien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lydo
            // 
            this.lydo.Font = new System.Drawing.Font("Arial", 10F);
            this.lydo.ForeColor = System.Drawing.Color.Black;
            this.lydo.LocationFloat = new DevExpress.Utils.PointFloat(65.87185F, 146.4394F);
            this.lydo.Name = "lydo";
            this.lydo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lydo.SizeF = new System.Drawing.SizeF(67.5268F, 20F);
            this.lydo.StylePriority.UseFont = false;
            this.lydo.StylePriority.UseForeColor = false;
            this.lydo.StylePriority.UseTextAlignment = false;
            this.lydo.Text = "Lý do chi: ";
            this.lydo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // diachix
            // 
            this.diachix.Font = new System.Drawing.Font("Arial", 10F);
            this.diachix.ForeColor = System.Drawing.Color.Black;
            this.diachix.LocationFloat = new DevExpress.Utils.PointFloat(65.87186F, 126.4394F);
            this.diachix.Name = "diachix";
            this.diachix.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.diachix.SizeF = new System.Drawing.SizeF(55.45177F, 20F);
            this.diachix.StylePriority.UseFont = false;
            this.diachix.StylePriority.UseForeColor = false;
            this.diachix.StylePriority.UseTextAlignment = false;
            this.diachix.Text = "Địa chỉ:";
            this.diachix.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ten
            // 
            this.ten.Font = new System.Drawing.Font("Arial", 10F);
            this.ten.ForeColor = System.Drawing.Color.Black;
            this.ten.LocationFloat = new DevExpress.Utils.PointFloat(65.87187F, 106.4394F);
            this.ten.Name = "ten";
            this.ten.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ten.SizeF = new System.Drawing.SizeF(151.7909F, 20F);
            this.ten.StylePriority.UseFont = false;
            this.ten.StylePriority.UseForeColor = false;
            this.ten.StylePriority.UseTextAlignment = false;
            this.ten.Text = "Họ tên người nhận tiền:";
            this.ten.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // co
            // 
            this.co.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.co.ForeColor = System.Drawing.Color.Black;
            this.co.LocationFloat = new DevExpress.Utils.PointFloat(669.5062F, 77.00003F);
            this.co.Name = "co";
            this.co.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.co.SizeF = new System.Drawing.SizeF(38F, 17F);
            this.co.StylePriority.UseFont = false;
            this.co.StylePriority.UseForeColor = false;
            this.co.StylePriority.UseTextAlignment = false;
            this.co.Text = "Có : ";
            this.co.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // no
            // 
            this.no.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.no.ForeColor = System.Drawing.Color.Black;
            this.no.LocationFloat = new DevExpress.Utils.PointFloat(669.5062F, 59.00002F);
            this.no.Name = "no";
            this.no.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.no.SizeF = new System.Drawing.SizeF(38F, 17F);
            this.no.StylePriority.UseFont = false;
            this.no.StylePriority.UseForeColor = false;
            this.no.StylePriority.UseTextAlignment = false;
            this.no.Text = "Nợ : ";
            this.no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 8F);
            this.xrLabel10.ForeColor = System.Drawing.Color.Black;
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(597.7499F, 26.00001F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(218.5002F, 32F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseForeColor = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "( Ban hành theo QĐ số: 15/2006/QĐ-BTC\r\nNgày 20/03/2006 của bộ trưởng BTC )";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.ForeColor = System.Drawing.Color.Black;
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(647.2499F, 8.750004F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(140.5F, 17F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseForeColor = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Mẫu số 02 - TT";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // donvi
            // 
            this.donvi.Font = new System.Drawing.Font("Arial", 10F);
            this.donvi.ForeColor = System.Drawing.Color.Black;
            this.donvi.LocationFloat = new DevExpress.Utils.PointFloat(4.749939F, 50.00002F);
            this.donvi.Name = "donvi";
            this.donvi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.donvi.SizeF = new System.Drawing.SizeF(56.80127F, 17F);
            this.donvi.StylePriority.UseFont = false;
            this.donvi.StylePriority.UseForeColor = false;
            this.donvi.StylePriority.UseTextAlignment = false;
            this.donvi.Text = "Đơn vị : ";
            this.donvi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // diachiew
            // 
            this.diachiew.Font = new System.Drawing.Font("Arial", 10F);
            this.diachiew.ForeColor = System.Drawing.Color.Black;
            this.diachiew.LocationFloat = new DevExpress.Utils.PointFloat(4.74994F, 67.00003F);
            this.diachiew.Name = "diachiew";
            this.diachiew.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.diachiew.SizeF = new System.Drawing.SizeF(58.80127F, 17F);
            this.diachiew.StylePriority.UseFont = false;
            this.diachiew.StylePriority.UseForeColor = false;
            this.diachiew.StylePriority.UseTextAlignment = false;
            this.diachiew.Text = "Địa chỉ : ";
            this.diachiew.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.ForeColor = System.Drawing.Color.Black;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(333.7499F, 8.75001F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(137.2986F, 31F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "PHIẾU CHI";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // so
            // 
            this.so.Font = new System.Drawing.Font("Arial", 10F);
            this.so.ForeColor = System.Drawing.Color.Black;
            this.so.LocationFloat = new DevExpress.Utils.PointFloat(471.5069F, 21.25001F);
            this.so.Name = "so";
            this.so.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.so.SizeF = new System.Drawing.SizeF(28.74307F, 17F);
            this.so.StylePriority.UseFont = false;
            this.so.StylePriority.UseForeColor = false;
            this.so.StylePriority.UseTextAlignment = false;
            this.so.Text = "Số: ";
            this.so.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ngay
            // 
            this.ngay.Font = new System.Drawing.Font("Arial", 10F);
            this.ngay.ForeColor = System.Drawing.Color.Black;
            this.ngay.LocationFloat = new DevExpress.Utils.PointFloat(331.709F, 42.00001F);
            this.ngay.Name = "ngay";
            this.ngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ngay.SizeF = new System.Drawing.SizeF(39.51276F, 19F);
            this.ngay.StylePriority.UseFont = false;
            this.ngay.StylePriority.UseForeColor = false;
            this.ngay.StylePriority.UseTextAlignment = false;
            this.ngay.Text = "Ngày ";
            this.ngay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // thang
            // 
            this.thang.Font = new System.Drawing.Font("Arial", 10F);
            this.thang.ForeColor = System.Drawing.Color.Black;
            this.thang.LocationFloat = new DevExpress.Utils.PointFloat(401.2873F, 42.00002F);
            this.thang.Name = "thang";
            this.thang.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.thang.SizeF = new System.Drawing.SizeF(43.45834F, 19F);
            this.thang.StylePriority.UseFont = false;
            this.thang.StylePriority.UseForeColor = false;
            this.thang.StylePriority.UseTextAlignment = false;
            this.thang.Text = "Tháng ";
            this.thang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // nam
            // 
            this.nam.Font = new System.Drawing.Font("Arial", 10F);
            this.nam.ForeColor = System.Drawing.Color.Black;
            this.nam.LocationFloat = new DevExpress.Utils.PointFloat(479.4583F, 42.00002F);
            this.nam.Name = "nam";
            this.nam.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.nam.SizeF = new System.Drawing.SizeF(31.79166F, 19F);
            this.nam.StylePriority.UseFont = false;
            this.nam.StylePriority.UseForeColor = false;
            this.nam.StylePriority.UseTextAlignment = false;
            this.nam.Text = "năm ";
            this.nam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // quyenso
            // 
            this.quyenso.Font = new System.Drawing.Font("Arial", 10F);
            this.quyenso.ForeColor = System.Drawing.Color.Black;
            this.quyenso.LocationFloat = new DevExpress.Utils.PointFloat(367.2499F, 67.00003F);
            this.quyenso.Name = "quyenso";
            this.quyenso.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.quyenso.SizeF = new System.Drawing.SizeF(67.49997F, 17F);
            this.quyenso.StylePriority.UseFont = false;
            this.quyenso.StylePriority.UseForeColor = false;
            this.quyenso.StylePriority.UseTextAlignment = false;
            this.quyenso.Text = "Quyển số:";
            this.quyenso.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel31
            // 
            this.xrLabel31.AutoWidth = true;
            this.xrLabel31.CanGrow = false;
            this.xrLabel31.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel31.ForeColor = System.Drawing.Color.Black;
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(345.2371F, 264.9394F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(101.5F, 17F);
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UseForeColor = false;
            this.xrLabel31.StylePriority.UseTextAlignment = false;
            this.xrLabel31.Text = "(Ký, họ tên)";
            this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel26.ForeColor = System.Drawing.Color.Black;
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(345.2371F, 247.9394F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(101.5F, 17F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseForeColor = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = "Thủ quỹ";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel37
            // 
            this.xrLabel37.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel37.ForeColor = System.Drawing.Color.Black;
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(67.32365F, 460.4734F);
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(228F, 20F);
            this.xrLabel37.StylePriority.UseFont = false;
            this.xrLabel37.StylePriority.UseForeColor = false;
            this.xrLabel37.StylePriority.UseTextAlignment = false;
            this.xrLabel37.Text = "(Liên gửi ra ngoài phải đóng dấu)";
            this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // XtraReport2
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(0, 1, 1158, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.SnapToGrid = false;
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion
}