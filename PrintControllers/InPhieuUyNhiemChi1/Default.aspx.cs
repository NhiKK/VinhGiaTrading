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
        rptUyNhiemChi report = new rptUyNhiemChi();
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
		LinqDBDataContext db = new LinqDBDataContext();
		c_chitietthuchi cttc = db.c_chitietthuchis.Where(s=>s.c_chitietthuchi_id == id).Take(1).FirstOrDefault();
		string str = "select * from c_chitietthuchi where 1 = 2";
		string ma_iso = "VND", so_taikhoan = "", ten_dtkd = "", diachi = "", nganhang = "", bangso = "", bangchu = "", diengiai = "", masothue = "";
		if(cttc != null)
		{
			c_thuchi tc = db.c_thuchis.Where(s=>s.c_thuchi_id == cttc.c_thuchi_id).Take(1).FirstOrDefault();
			md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.Where(s=>s.md_doitackinhdoanh_id == cttc.md_doitackinhdoanh_id).Take(1).FirstOrDefault();
			md_dongtien dt = db.md_dongtiens.Where(s=>s.md_dongtien_id == tc.md_dongtien_id).Take(1).FirstOrDefault();
			
			if(dt != null) { ma_iso = dt.ma_iso; }
			if(dtkd != null) {
				so_taikhoan = dtkd.so_taikhoan; 
				ten_dtkd = dtkd.ten_dtkd;
				diachi = dtkd.diachi;
				nganhang = dtkd.nganhang;
				masothue = dtkd.masothue;
			}
			
			if(cttc.thue == null)
			{
				cttc.thue = 0;
			}
			double tien = (double)cttc.sotien.Value + (((double)cttc.sotien.Value * (double)cttc.thue) / 100);
			if(cttc.sotiencong != null){
				tien += (double)cttc.sotiencong.Value;
			}
			tien = (double)Math.Round(tien, 0);
			bangchu = VNN_ConvertMoney.convert(tien, ma_iso);
			bangso = VNN_ConvertMoney.sep_thous(VNN_ConvertMoney.autoRound((decimal)tien, 2).ToString(), ",", ".");
			
			str = String.Format(@" select
			N'007.100. 0321642' as sotk_no,
			N'CÔNG TY TNHH VINH GIA' as tentk_no,
			N'Khu công nghiệp Bắc Chu Lai, Xã Núi Thành, Thành phố Đà Nẵng, Việt Nam' as diachi_no,
			N'VCB-HCM' as nganhang_no,
			N'          /          / 20' + SUBSTRING(so_c, CHARINDEX('/',so_c) + 1, 2) as ngaychi,
			N'{0}' as dongtien, 
			N'{1}' as sotk_co, N'{2}' as tentk_co, N'{3}' as diachi_co, N'{4}' as nganhang_co,
			N'{5}' as tongtien, N'{6}' as tongtien_bangchu, N'{7}' as noidung
			from c_chitietthuchi
			where c_chitietthuchi_id = N'{8}'", 
			ma_iso, so_taikhoan, ten_dtkd, diachi, nganhang, bangso, bangchu, cttc.diengiai, id);
        }
		return str;
    }
}

/// <summary>
/// Summary description for XtraReport1
/// </summary>
public class rptUyNhiemChi : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow3;
    private XRTable xrTable3;
    private XRTableCell nganhang_no;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow7;
    private XRTableCell diachi_no;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell10;
    private XRTableRow xrTableRow6;
    private XRTableCell tentk_no;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell8;
    private XRTableRow xrTableRow5;
    private XRTableCell sotk_no;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell5;
    private XRTableRow xrTableRow4;
    private XRTable xrTable4;
    private XRLabel xrLabel12;
    private XRTableCell xrTableCell14;
    private XRTableRow xrTableRow8;
    private XRTable xrTable5;
    private XRTableCell dongtien;
    private XRTableCell tongtien;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell15;
    private XRTableRow xrTableRow9;
    private XRTable xrTable6;
    private XRLabel xrLabel11;
    private XRLabel xrLabel9;
    private XRLabel noidung;
    private XRTableCell nganhang_co;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell27;
    private XRTableRow xrTableRow16;
    private XRTableCell diachi_co;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell25;
    private XRTableRow xrTableRow14;
    private XRTableCell tentk_co;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell23;
    private XRTableRow xrTableRow13;
    private XRTableCell sotk_co;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell20;
    private XRTableRow xrTableRow12;
    private XRTable xrTable8;
    private XRTableCell tongtien_bangchu;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow10;
    private XRTable xrTable9;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell13;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell9;
    private XRTableRow xrTableRow11;
    private XRTable xrTable10;
    private XRLabel xrLabel5;
    private XRTableCell xrTableCell1;
    private XRTableRow xrTableRow2;
    private XRTable xrTable2;
    private XRLabel xrLabel13;
    private XRLabel xrLabel14;
    private XRLabel xrLabel15;
    private XRLabel xrLabel16;
    private XRLabel xrLabel17;
    private XRLabel masothue;
    private XRTableCell xrTableCell28;
    private XRTableRow xrTableRow15;
    private XRTable xrTable7;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRTableCell xrTableCell2;
    private XRTableRow xrTableRow1;
    private XRTable xrTable1;
    private XRTable xrTable11;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell32;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel10;
    private XRTable xrTable12;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell33;
    private XRLabel xrLabel18;
    private XRTable xrTable13;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTable xrTable14;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTable xrTable15;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell49;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell52;
    private XRLabel xrLabel19;
    private XRLabel xrLabel20;
    private XRLabel xrLabel21;
    private XRTable xrTable16;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTable xrTable17;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell57;
    private XRLabel xrLabel22;
    private XRTable xrTable18;
    private XRTableRow xrTableRow29;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell60;
    private XRTableRow xrTableRow30;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell63;
    private XRTableRow xrTableRow31;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableRow xrTableRow32;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell68;
    private XRTableCell xrTableCell69;
    private XRTable xrTable19;
    private XRTableRow xrTableRow33;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRTable xrTable20;
    private XRTableRow xrTableRow34;
    private XRTableCell xrTableCell73;
    private XRLabel xrLabel23;
    private XRLabel xrLabel24;
    private XRLabel xrLabel25;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private XRLabel xrLabel28;
    private XRLabel ngaychi;
    private XRLabel xrLabel8;
    private XRLabel xrLabel29;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptUyNhiemChi()
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
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable20 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow34 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable18 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow31 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow32 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable19 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow33 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ngaychi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.masothue = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tongtien_bangchu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.sotk_co = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tentk_co = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.diachi_co = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.nganhang_co = new DevExpress.XtraReports.UI.XRTableCell();
            this.noidung = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tongtien = new DevExpress.XtraReports.UI.XRTableCell();
            this.dongtien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.sotk_no = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tentk_no = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.diachi_no = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.nganhang_no = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            this.Detail.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Detail.HeightF = 1003.75F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseFont = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseFont = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable20,
            this.xrTable12,
            this.xrTable11,
            this.xrTable1,
            this.xrTable7,
            this.xrTable2});
            this.ReportHeader.HeightF = 1164.491F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable20
            // 
            this.xrTable20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable20.LocationFloat = new DevExpress.Utils.PointFloat(6.5F, 988.4069F);
            this.xrTable20.Name = "xrTable20";
            this.xrTable20.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow34});
            this.xrTable20.SizeF = new System.Drawing.SizeF(813.5399F, 133.542F);
            this.xrTable20.StylePriority.UseBorders = false;
            // 
            // xrTableRow34
            // 
            this.xrTableRow34.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell73});
            this.xrTableRow34.Name = "xrTableRow34";
            this.xrTableRow34.Weight = 0.86504823056097013D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell73.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel23,
            this.xrLabel24,
            this.xrLabel25,
            this.xrLabel26,
            this.xrLabel27,
            this.xrLabel28});
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.StylePriority.UseBorders = false;
            this.xrTableCell73.Weight = 3D;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "masothue")});
            this.xrLabel23.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(233.1752F, 0F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(386.1754F, 23F);
            this.xrLabel23.StylePriority.UseBorders = false;
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UsePadding = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel24.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(644.0808F, 38.62509F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(136F, 23F);
            this.xrLabel24.StylePriority.UseBorders = false;
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UsePadding = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "Giám đốc";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel25.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(507.9976F, 38.62511F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(136F, 23F);
            this.xrLabel25.StylePriority.UseBorders = false;
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UsePadding = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "Kiểm soát";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel26.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(371.9976F, 38.62512F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(136F, 23F);
            this.xrLabel26.StylePriority.UseBorders = false;
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UsePadding = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = "Thanh toán viên";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel27
            // 
            this.xrLabel27.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel27.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(173.1752F, 0F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(60F, 23F);
            this.xrLabel27.StylePriority.UseBorders = false;
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UsePadding = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.Text = "MÃ VAT:";
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel28.Font = new System.Drawing.Font("Arial", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(2F, 0F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(171.1752F, 23F);
            this.xrLabel28.StylePriority.UseBorders = false;
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UsePadding = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "DÀNH CHO NGÂN HÀNG";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable12
            // 
            this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(6.5F, 727.9902F);
            this.xrTable12.Name = "xrTable12";
            this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19});
            this.xrTable12.SizeF = new System.Drawing.SizeF(813.5424F, 260.4167F);
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 0.93632966116573035D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell33.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18,
            this.xrTable13,
            this.xrTable14,
            this.xrTable15,
            this.xrLabel19,
            this.xrLabel20,
            this.xrLabel21,
            this.xrTable16,
            this.xrTable17,
            this.xrLabel22,
            this.xrTable18,
            this.xrTable19});
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseBorders = false;
            this.xrTableCell33.Weight = 3D;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel18.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(472.1531F, 102F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 6, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(5F, 23.00002F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UsePadding = false;
            this.xrLabel18.Text = ":";
            // 
            // xrTable13
            // 
            this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(722.1256F, 25.00002F);
            this.xrTable13.Name = "xrTable13";
            this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow20,
            this.xrTableRow21});
            this.xrTable13.SizeF = new System.Drawing.SizeF(81.41675F, 50F);
            // 
            // xrTableRow20
            // 
            this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell35});
            this.xrTableRow20.Name = "xrTableRow20";
            this.xrTableRow20.Weight = 1D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell34.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell34.StylePriority.UseBorders = false;
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.StylePriority.UsePadding = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "Phí trong";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell34.Weight = 2.4310408579033957D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorderColor = false;
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.Weight = 0.60776024010090712D;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell36,
            this.xrTableCell37});
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 1D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell36.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.StylePriority.UsePadding = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.Text = "Phí ngoài";
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell36.Weight = 2.4310408579033957D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBorderColor = false;
            this.xrTableCell37.Weight = 0.60776024010090712D;
            // 
            // xrTable14
            // 
            this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(387.6948F, 50.0001F);
            this.xrTable14.Name = "xrTable14";
            this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow22});
            this.xrTable14.SizeF = new System.Drawing.SizeF(334.4308F, 51.99998F);
            // 
            // xrTableRow22
            // 
            this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40});
            this.xrTableRow22.Name = "xrTableRow22";
            this.xrTableRow22.Weight = 1D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell38.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 6, 3, 100F);
            this.xrTableCell38.StylePriority.UseBorderColor = false;
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UsePadding = false;
            this.xrTableCell38.Text = "BẰNG CHỮ";
            this.xrTableCell38.Weight = 0.74454867715533546D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell39.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell39.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 6, 0, 100F);
            this.xrTableCell39.StylePriority.UseBorderColor = false;
            this.xrTableCell39.StylePriority.UseBorders = false;
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.StylePriority.UsePadding = false;
            this.xrTableCell39.Text = ":";
            this.xrTableCell39.Weight = 0.057934625537183024D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell40.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtien_bangchu")});
            this.xrTableCell40.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 6, 3, 100F);
            this.xrTableCell40.StylePriority.UseBorderColor = false;
            this.xrTableCell40.StylePriority.UseBorders = false;
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.StylePriority.UsePadding = false;
            this.xrTableCell40.Text = "tongtien_bangchu";
            this.xrTableCell40.Weight = 2.1975166973074813D;
            // 
            // xrTable15
            // 
            this.xrTable15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable15.Font = new System.Drawing.Font("Arial", 7.25F);
            this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(1.000023F, 149.9999F);
            this.xrTable15.Name = "xrTable15";
            this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow24,
            this.xrTableRow25,
            this.xrTableRow26});
            this.xrTable15.SizeF = new System.Drawing.SizeF(373.8354F, 100F);
            this.xrTable15.StylePriority.UseBorders = false;
            this.xrTable15.StylePriority.UseFont = false;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell43});
            this.xrTableRow23.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.StylePriority.UseFont = false;
            this.xrTableRow23.Weight = 1D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell41.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell41.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell41.StylePriority.UseBorderColor = false;
            this.xrTableCell41.StylePriority.UseBorders = false;
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.StylePriority.UsePadding = false;
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.Text = "SỐ TK";
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell41.Weight = 0.46310939324040046D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell42.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell42.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseBorderColor = false;
            this.xrTableCell42.StylePriority.UseBorders = false;
            this.xrTableCell42.StylePriority.UseFont = false;
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.Text = ":";
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell42.Weight = 0.040270380803934795D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell43.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sotk_co")});
            this.xrTableCell43.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell43.StylePriority.UseBorderColor = false;
            this.xrTableCell43.StylePriority.UseBorders = false;
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.StylePriority.UsePadding = false;
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.Text = "xrTableCell6";
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell43.Weight = 2.1912844164794048D;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell44,
            this.xrTableCell45,
            this.xrTableCell46});
            this.xrTableRow24.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.StylePriority.UseFont = false;
            this.xrTableRow24.Weight = 1D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell44.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell44.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell44.StylePriority.UseBorderColor = false;
            this.xrTableCell44.StylePriority.UseBorders = false;
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.StylePriority.UsePadding = false;
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.Text = "TÊN TK";
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell44.Weight = 0.46310939298254417D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell45.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell45.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseBorderColor = false;
            this.xrTableCell45.StylePriority.UseBorders = false;
            this.xrTableCell45.StylePriority.UseFont = false;
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.Text = ":";
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell45.Weight = 0.040270380932863011D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell46.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tentk_co")});
            this.xrTableCell46.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell46.StylePriority.UseBorderColor = false;
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.StylePriority.UseFont = false;
            this.xrTableCell46.StylePriority.UsePadding = false;
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.Text = "xrTableCell8";
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell46.Weight = 2.1912844166083327D;
            // 
            // xrTableRow25
            // 
            this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell49});
            this.xrTableRow25.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow25.Name = "xrTableRow25";
            this.xrTableRow25.StylePriority.UseFont = false;
            this.xrTableRow25.Weight = 1D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell47.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell47.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell47.StylePriority.UseBorderColor = false;
            this.xrTableCell47.StylePriority.UseBorders = false;
            this.xrTableCell47.StylePriority.UseFont = false;
            this.xrTableCell47.StylePriority.UsePadding = false;
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.Text = "ĐỊA CHỈ";
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell47.Weight = 0.463109392724688D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell48.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell48.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseBorderColor = false;
            this.xrTableCell48.StylePriority.UseBorders = false;
            this.xrTableCell48.StylePriority.UseFont = false;
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.Text = ":";
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell48.Weight = 0.04027038106179099D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell49.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachi_co")});
            this.xrTableCell49.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell49.StylePriority.UseBorderColor = false;
            this.xrTableCell49.StylePriority.UseBorders = false;
            this.xrTableCell49.StylePriority.UseFont = false;
            this.xrTableCell49.StylePriority.UsePadding = false;
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.Text = "xrTableCell10";
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell49.Weight = 2.1912844167372607D;
            // 
            // xrTableRow26
            // 
            this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52});
            this.xrTableRow26.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow26.Name = "xrTableRow26";
            this.xrTableRow26.StylePriority.UseFont = false;
            this.xrTableRow26.Weight = 1D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell50.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell50.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell50.StylePriority.UseBorderColor = false;
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.StylePriority.UsePadding = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.Text = "TẠI NH";
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell50.Weight = 0.463109392724688D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell51.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell51.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseBorderColor = false;
            this.xrTableCell51.StylePriority.UseBorders = false;
            this.xrTableCell51.StylePriority.UseFont = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.Text = ":";
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell51.Weight = 0.040270381061790976D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell52.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nganhang_co")});
            this.xrTableCell52.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell52.StylePriority.UseBorderColor = false;
            this.xrTableCell52.StylePriority.UseBorders = false;
            this.xrTableCell52.StylePriority.UseFont = false;
            this.xrTableCell52.StylePriority.UsePadding = false;
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.Text = "xrTableCell12";
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell52.Weight = 2.1912844167372616D;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "noidung")});
            this.xrLabel19.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(477.1531F, 102.0001F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 6, 3, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(326.3895F, 47.99997F);
            this.xrLabel19.StylePriority.UseBorders = false;
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UsePadding = false;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel20.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(387.6948F, 102.0001F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 3, 6, 3, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(84.45837F, 22.99998F);
            this.xrLabel20.StylePriority.UseBorders = false;
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UsePadding = false;
            this.xrLabel20.Text = "NỘI DUNG";
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel21.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(387.6948F, 164.4999F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(174.805F, 23.00003F);
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UsePadding = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "KẾ TOÁN KÝ";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable16
            // 
            this.xrTable16.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(387.6948F, 25.00006F);
            this.xrTable16.Name = "xrTable16";
            this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
            this.xrTable16.SizeF = new System.Drawing.SizeF(334.4307F, 25F);
            this.xrTable16.StylePriority.UseFont = false;
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56});
            this.xrTableRow27.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.StylePriority.UseFont = false;
            this.xrTableRow27.Weight = 1D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell53.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell53.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell53.StylePriority.UseBorderColor = false;
            this.xrTableCell53.StylePriority.UseBorders = false;
            this.xrTableCell53.StylePriority.UseFont = false;
            this.xrTableCell53.StylePriority.UsePadding = false;
            this.xrTableCell53.StylePriority.UseTextAlignment = false;
            this.xrTableCell53.Text = "BẰNG SỐ ";
            this.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell53.Weight = 0.95680524460029548D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell54.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseBorderColor = false;
            this.xrTableCell54.StylePriority.UseBorders = false;
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.Text = ":";
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell54.Weight = 0.074450354332903937D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell55.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtien")});
            this.xrTableCell55.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell55.StylePriority.UseBorderColor = false;
            this.xrTableCell55.StylePriority.UseBorders = false;
            this.xrTableCell55.StylePriority.UseFont = false;
            this.xrTableCell55.StylePriority.UsePadding = false;
            this.xrTableCell55.StylePriority.UseTextAlignment = false;
            this.xrTableCell55.Text = "xrTableCell6";
            this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell55.Weight = 2.3724171091099757D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell56.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dongtien")});
            this.xrTableCell56.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 3, 3, 100F);
            this.xrTableCell56.StylePriority.UseBorderColor = false;
            this.xrTableCell56.StylePriority.UseBorders = false;
            this.xrTableCell56.StylePriority.UseFont = false;
            this.xrTableCell56.StylePriority.UsePadding = false;
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            this.xrTableCell56.Text = "VND";
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell56.Weight = 0.45157054884362469D;
            // 
            // xrTable17
            // 
            this.xrTable17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable17.Font = new System.Drawing.Font("Arial", 7.25F);
            this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(0.9999912F, 124.9999F);
            this.xrTable17.Name = "xrTable17";
            this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
            this.xrTable17.SizeF = new System.Drawing.SizeF(373.8355F, 25F);
            this.xrTable17.StylePriority.UseBorders = false;
            this.xrTable17.StylePriority.UseFont = false;
            // 
            // xrTableRow28
            // 
            this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell57});
            this.xrTableRow28.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow28.Name = "xrTableRow28";
            this.xrTableRow28.StylePriority.UseFont = false;
            this.xrTableRow28.Weight = 1D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell57.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell57.StylePriority.UseBorders = false;
            this.xrTableCell57.StylePriority.UseFont = false;
            this.xrTableCell57.StylePriority.UsePadding = false;
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            this.xrTableCell57.Text = "& GHI CÓ TÀI KHOẢN";
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell57.Weight = 3.0630476061391745D;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel22.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(584.5581F, 164.4999F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(218.9844F, 23.00003F);
            this.xrLabel22.StylePriority.UseBorders = false;
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UsePadding = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "CHỦ TÀI KHOẢN KÝ & ĐÓNG DẤU";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable18
            // 
            this.xrTable18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable18.Font = new System.Drawing.Font("Arial", 7.25F);
            this.xrTable18.LocationFloat = new DevExpress.Utils.PointFloat(0.9999912F, 25.00002F);
            this.xrTable18.Name = "xrTable18";
            this.xrTable18.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow29,
            this.xrTableRow30,
            this.xrTableRow31,
            this.xrTableRow32});
            this.xrTable18.SizeF = new System.Drawing.SizeF(373.8355F, 100F);
            this.xrTable18.StylePriority.UseBorders = false;
            this.xrTable18.StylePriority.UseFont = false;
            // 
            // xrTableRow29
            // 
            this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell60});
            this.xrTableRow29.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow29.Name = "xrTableRow29";
            this.xrTableRow29.StylePriority.UseFont = false;
            this.xrTableRow29.Weight = 1D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell58.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell58.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell58.StylePriority.UseBorderColor = false;
            this.xrTableCell58.StylePriority.UseBorders = false;
            this.xrTableCell58.StylePriority.UseFont = false;
            this.xrTableCell58.StylePriority.UsePadding = false;
            this.xrTableCell58.StylePriority.UseTextAlignment = false;
            this.xrTableCell58.Text = "SỐ TK";
            this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell58.Weight = 0.46310939324040029D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell59.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell59.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.StylePriority.UseBorderColor = false;
            this.xrTableCell59.StylePriority.UseBorders = false;
            this.xrTableCell59.StylePriority.UseFont = false;
            this.xrTableCell59.StylePriority.UseTextAlignment = false;
            this.xrTableCell59.Text = ":";
            this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell59.Weight = 0.040270380803934927D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell60.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTableCell60.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sotk_no")});
            this.xrTableCell60.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell60.StylePriority.UseBorderColor = false;
            this.xrTableCell60.StylePriority.UseBorderDashStyle = false;
            this.xrTableCell60.StylePriority.UseBorders = false;
            this.xrTableCell60.StylePriority.UseFont = false;
            this.xrTableCell60.StylePriority.UsePadding = false;
            this.xrTableCell60.StylePriority.UseTextAlignment = false;
            this.xrTableCell60.Text = "[sotk_no]";
            this.xrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell60.Weight = 2.1912846278963509D;
            // 
            // xrTableRow30
            // 
            this.xrTableRow30.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell63});
            this.xrTableRow30.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow30.Name = "xrTableRow30";
            this.xrTableRow30.StylePriority.UseFont = false;
            this.xrTableRow30.Weight = 1D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell61.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell61.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell61.StylePriority.UseBorderColor = false;
            this.xrTableCell61.StylePriority.UseBorders = false;
            this.xrTableCell61.StylePriority.UseFont = false;
            this.xrTableCell61.StylePriority.UsePadding = false;
            this.xrTableCell61.StylePriority.UseTextAlignment = false;
            this.xrTableCell61.Text = "TÊN TK";
            this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell61.Weight = 0.46310939298254439D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell62.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell62.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.StylePriority.UseBorderColor = false;
            this.xrTableCell62.StylePriority.UseBorders = false;
            this.xrTableCell62.StylePriority.UseFont = false;
            this.xrTableCell62.StylePriority.UseTextAlignment = false;
            this.xrTableCell62.Text = ":";
            this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell62.Weight = 0.040270380932863D;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tentk_no")});
            this.xrTableCell63.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell63.StylePriority.UseBorderColor = false;
            this.xrTableCell63.StylePriority.UseBorders = false;
            this.xrTableCell63.StylePriority.UseFont = false;
            this.xrTableCell63.StylePriority.UsePadding = false;
            this.xrTableCell63.StylePriority.UseTextAlignment = false;
            this.xrTableCell63.Text = "xrTableCell8";
            this.xrTableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell63.Weight = 2.1912846280252785D;
            // 
            // xrTableRow31
            // 
            this.xrTableRow31.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell64,
            this.xrTableCell65,
            this.xrTableCell66});
            this.xrTableRow31.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow31.Name = "xrTableRow31";
            this.xrTableRow31.StylePriority.UseFont = false;
            this.xrTableRow31.Weight = 1D;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell64.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell64.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell64.StylePriority.UseBorderColor = false;
            this.xrTableCell64.StylePriority.UseBorders = false;
            this.xrTableCell64.StylePriority.UseFont = false;
            this.xrTableCell64.StylePriority.UsePadding = false;
            this.xrTableCell64.StylePriority.UseTextAlignment = false;
            this.xrTableCell64.Text = "ĐỊA CHỈ";
            this.xrTableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell64.Weight = 0.463109392724688D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell65.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell65.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.StylePriority.UseBorderColor = false;
            this.xrTableCell65.StylePriority.UseBorders = false;
            this.xrTableCell65.StylePriority.UseFont = false;
            this.xrTableCell65.StylePriority.UseTextAlignment = false;
            this.xrTableCell65.Text = ":";
            this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell65.Weight = 0.040270381061790983D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachi_no")});
            this.xrTableCell66.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell66.StylePriority.UseBorderColor = false;
            this.xrTableCell66.StylePriority.UseBorders = false;
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.StylePriority.UsePadding = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.Text = "xrTableCell10";
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell66.Weight = 2.1912846281542064D;
            // 
            // xrTableRow32
            // 
            this.xrTableRow32.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell67,
            this.xrTableCell68,
            this.xrTableCell69});
            this.xrTableRow32.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow32.Name = "xrTableRow32";
            this.xrTableRow32.StylePriority.UseFont = false;
            this.xrTableRow32.Weight = 1D;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell67.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell67.Name = "xrTableCell67";
            this.xrTableCell67.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell67.StylePriority.UseBorderColor = false;
            this.xrTableCell67.StylePriority.UseBorders = false;
            this.xrTableCell67.StylePriority.UseFont = false;
            this.xrTableCell67.StylePriority.UsePadding = false;
            this.xrTableCell67.StylePriority.UseTextAlignment = false;
            this.xrTableCell67.Text = "TẠI NH";
            this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell67.Weight = 0.463109392724688D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell68.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.StylePriority.UseBorderColor = false;
            this.xrTableCell68.StylePriority.UseBorders = false;
            this.xrTableCell68.StylePriority.UseFont = false;
            this.xrTableCell68.StylePriority.UseTextAlignment = false;
            this.xrTableCell68.Text = ":";
            this.xrTableCell68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell68.Weight = 0.040270381061790893D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell69.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nganhang_no")});
            this.xrTableCell69.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell69.StylePriority.UseBorderColor = false;
            this.xrTableCell69.StylePriority.UseBorders = false;
            this.xrTableCell69.StylePriority.UseFont = false;
            this.xrTableCell69.StylePriority.UsePadding = false;
            this.xrTableCell69.StylePriority.UseTextAlignment = false;
            this.xrTableCell69.Text = "xrTableCell12";
            this.xrTableCell69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell69.Weight = 2.1912846281542069D;
            // 
            // xrTable19
            // 
            this.xrTable19.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable19.Font = new System.Drawing.Font("Arial", 7.25F);
            this.xrTable19.LocationFloat = new DevExpress.Utils.PointFloat(2.002398F, 6.103516E-05F);
            this.xrTable19.Name = "xrTable19";
            this.xrTable19.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow33});
            this.xrTable19.SizeF = new System.Drawing.SizeF(801.5401F, 24.99999F);
            this.xrTable19.StylePriority.UseBorders = false;
            this.xrTable19.StylePriority.UseFont = false;
            // 
            // xrTableRow33
            // 
            this.xrTableRow33.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell70,
            this.xrTableCell71,
            this.xrTableCell72});
            this.xrTableRow33.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow33.Name = "xrTableRow33";
            this.xrTableRow33.StylePriority.UseFont = false;
            this.xrTableRow33.Weight = 1D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell70.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.Padding = new DevExpress.XtraPrinting.PaddingInfo(4, 3, 3, 3, 100F);
            this.xrTableCell70.StylePriority.UseBorders = false;
            this.xrTableCell70.StylePriority.UseFont = false;
            this.xrTableCell70.StylePriority.UsePadding = false;
            this.xrTableCell70.StylePriority.UseTextAlignment = false;
            this.xrTableCell70.Text = "ĐỀ NGHỊ GHI NỢ TÀI KHOẢN";
            this.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell70.Weight = 2.3325922396323819D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell71.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.StylePriority.UseBorders = false;
            this.xrTableCell71.StylePriority.UseFont = false;
            this.xrTableCell71.Weight = 0.080453370035986821D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell72.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell72.StylePriority.UseBorders = false;
            this.xrTableCell72.StylePriority.UseFont = false;
            this.xrTableCell72.StylePriority.UsePadding = false;
            this.xrTableCell72.StylePriority.UseTextAlignment = false;
            this.xrTableCell72.Text = "SỐ TIỀN";
            this.xrTableCell72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell72.Weight = 2.6017086863574663D;
            // 
            // xrTable11
            // 
            this.xrTable11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(6.5F, 637.3652F);
            this.xrTable11.Name = "xrTable11";
            this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18});
            this.xrTable11.SizeF = new System.Drawing.SizeF(813.5425F, 90.62503F);
            this.xrTable11.StylePriority.UseBorders = false;
            this.xrTable11.StylePriority.UseFont = false;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell32});
            this.xrTableRow18.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.StylePriority.UseFont = false;
            this.xrTableRow18.Weight = 0.550632970070648D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel29,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel10});
            this.xrTableCell32.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.Weight = 2.9845632321210402D;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(6.542397F, 1.999998F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(259.9576F, 44.875F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "VIETCOMBANK";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Italic);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(379F, 1.999998F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(161.4584F, 44.875F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "- PAYMENT ORDER";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(266.5F, 1.999998F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(112.5F, 44.875F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "ỦY NHIỆM CHI";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(6.72876F, 27F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(813.5425F, 90.62503F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
            this.xrTableRow1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.Weight = 0.550632970070648D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ngaychi,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
            this.xrTableCell2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 2.9845632321210402D;
            // 
            // ngaychi
            // 
            this.ngaychi.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.ngaychi.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaychi", "{0:dd/MM/yyyy}")});
            this.ngaychi.Font = new System.Drawing.Font("Arial", 9.25F);
            this.ngaychi.LocationFloat = new DevExpress.Utils.PointFloat(379F, 57.62502F);
            this.ngaychi.Name = "ngaychi";
            this.ngaychi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ngaychi.SizeF = new System.Drawing.SizeF(161.4584F, 23F);
            this.ngaychi.StylePriority.UseBorders = false;
            this.ngaychi.StylePriority.UseFont = false;
            this.ngaychi.StylePriority.UseTextAlignment = false;
            this.ngaychi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(6.542397F, 1.999998F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(259.9576F, 44.875F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "VIETCOMBANK";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Italic);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(379F, 1.999998F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(161.4584F, 44.875F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "- PAYMENT ORDER";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(267.4989F, 57.62502F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(111.5011F, 23F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Ngày: ";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(266.5F, 1.999998F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(112.5F, 44.875F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "ỦY NHIỆM CHI";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable7
            // 
            this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(6.731144F, 380.125F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
            this.xrTable7.SizeF = new System.Drawing.SizeF(813.5399F, 132.5002F);
            this.xrTable7.StylePriority.UseBorders = false;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell28.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.masothue,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseBorders = false;
            this.xrTableCell28.Weight = 3D;
            // 
            // masothue
            // 
            this.masothue.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.masothue.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "masothue")});
            this.masothue.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.masothue.LocationFloat = new DevExpress.Utils.PointFloat(233.1752F, 0F);
            this.masothue.Name = "masothue";
            this.masothue.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.masothue.SizeF = new System.Drawing.SizeF(386.1754F, 23F);
            this.masothue.StylePriority.UseBorders = false;
            this.masothue.StylePriority.UseFont = false;
            this.masothue.StylePriority.UsePadding = false;
            this.masothue.StylePriority.UseTextAlignment = false;
            this.masothue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel17.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(644.0808F, 38.62509F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(136F, 23F);
            this.xrLabel17.StylePriority.UseBorders = false;
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UsePadding = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "Giám đốc";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel16.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(507.9976F, 38.62511F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(136F, 23F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UsePadding = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "Kiểm soát";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel15.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(371.9976F, 38.62512F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(136F, 23F);
            this.xrLabel15.StylePriority.UseBorders = false;
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UsePadding = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Thanh toán viên";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel14.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(173.1752F, 0F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(60F, 23F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UsePadding = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "MÃ VAT:";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel13.Font = new System.Drawing.Font("Arial", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(2F, 0F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(171.1752F, 23F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UsePadding = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "DÀNH CHO NGÂN HÀNG";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(6.72876F, 117.625F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(813.5424F, 262.5F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.94382011499297758D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrTable10,
            this.xrTable9,
            this.xrTable8,
            this.noidung,
            this.xrLabel9,
            this.xrLabel11,
            this.xrTable6,
            this.xrTable5,
            this.xrLabel12,
            this.xrTable4,
            this.xrTable3});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.Weight = 3D;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(472.1531F, 102F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 6, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(5F, 23.00002F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.Text = ":";
            // 
            // xrTable10
            // 
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(722.1256F, 25.00002F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11,
            this.xrTableRow17});
            this.xrTable10.SizeF = new System.Drawing.SizeF(81.41675F, 50F);
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell11});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell9.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UsePadding = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Phí trong";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell9.Weight = 2.4310408579033957D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorderColor = false;
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.Weight = 0.60776024010090712D;
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell16});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell13.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "Phí ngoài";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell13.Weight = 2.4310408579033957D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorderColor = false;
            this.xrTableCell16.Weight = 0.60776024010090712D;
            // 
            // xrTable9
            // 
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(387.6948F, 50.0001F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrTable9.SizeF = new System.Drawing.SizeF(334.4308F, 51.99998F);
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.xrTableCell31,
            this.tongtien_bangchu});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 6, 3, 100F);
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.Text = "BẰNG CHỮ";
            this.xrTableCell6.Weight = 0.74454867715533546D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell31.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell31.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 6, 0, 100F);
            this.xrTableCell31.StylePriority.UseBorderColor = false;
            this.xrTableCell31.StylePriority.UseBorders = false;
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UsePadding = false;
            this.xrTableCell31.Text = ":";
            this.xrTableCell31.Weight = 0.057934625537183024D;
            // 
            // tongtien_bangchu
            // 
            this.tongtien_bangchu.BorderColor = System.Drawing.Color.LightGray;
            this.tongtien_bangchu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tongtien_bangchu.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtien_bangchu")});
            this.tongtien_bangchu.Font = new System.Drawing.Font("Arial", 9.25F);
            this.tongtien_bangchu.Name = "tongtien_bangchu";
            this.tongtien_bangchu.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 6, 3, 100F);
            this.tongtien_bangchu.StylePriority.UseBorderColor = false;
            this.tongtien_bangchu.StylePriority.UseBorders = false;
            this.tongtien_bangchu.StylePriority.UseFont = false;
            this.tongtien_bangchu.StylePriority.UsePadding = false;
            this.tongtien_bangchu.Text = "tongtien_bangchu";
            this.tongtien_bangchu.Weight = 2.1975166973074813D;
            // 
            // xrTable8
            // 
            this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable8.Font = new System.Drawing.Font("Arial", 7.25F);
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(1.000023F, 149.9999F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow16});
            this.xrTable8.SizeF = new System.Drawing.SizeF(373.8354F, 100F);
            this.xrTable8.StylePriority.UseBorders = false;
            this.xrTable8.StylePriority.UseFont = false;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell22,
            this.sotk_co});
            this.xrTableRow12.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.StylePriority.UseFont = false;
            this.xrTableRow12.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell20.StylePriority.UseBorderColor = false;
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.StylePriority.UsePadding = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.Text = "SỐ TK";
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell20.Weight = 0.46310939324040046D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell22.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorderColor = false;
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.Text = ":";
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell22.Weight = 0.040270380803934795D;
            // 
            // sotk_co
            // 
            this.sotk_co.BorderColor = System.Drawing.Color.LightGray;
            this.sotk_co.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.sotk_co.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sotk_co")});
            this.sotk_co.Font = new System.Drawing.Font("Arial", 9.25F);
            this.sotk_co.Name = "sotk_co";
            this.sotk_co.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.sotk_co.StylePriority.UseBorderColor = false;
            this.sotk_co.StylePriority.UseBorders = false;
            this.sotk_co.StylePriority.UseFont = false;
            this.sotk_co.StylePriority.UsePadding = false;
            this.sotk_co.StylePriority.UseTextAlignment = false;
            this.sotk_co.Text = "xrTableCell6";
            this.sotk_co.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.sotk_co.Weight = 2.1912844164794048D;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.tentk_co});
            this.xrTableRow13.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.StylePriority.UseFont = false;
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell23.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell23.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell23.StylePriority.UseBorderColor = false;
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.StylePriority.UsePadding = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "TÊN TK";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell23.Weight = 0.46310939298254417D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell24.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseBorderColor = false;
            this.xrTableCell24.StylePriority.UseBorders = false;
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.Text = ":";
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell24.Weight = 0.040270380932863011D;
            // 
            // tentk_co
            // 
            this.tentk_co.BorderColor = System.Drawing.Color.LightGray;
            this.tentk_co.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tentk_co.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tentk_co")});
            this.tentk_co.Font = new System.Drawing.Font("Arial", 9.25F);
            this.tentk_co.Name = "tentk_co";
            this.tentk_co.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tentk_co.StylePriority.UseBorderColor = false;
            this.tentk_co.StylePriority.UseBorders = false;
            this.tentk_co.StylePriority.UseFont = false;
            this.tentk_co.StylePriority.UsePadding = false;
            this.tentk_co.StylePriority.UseTextAlignment = false;
            this.tentk_co.Text = "xrTableCell8";
            this.tentk_co.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tentk_co.Weight = 2.1912844166083327D;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell26,
            this.diachi_co});
            this.xrTableRow14.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.StylePriority.UseFont = false;
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell25.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell25.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell25.StylePriority.UseBorderColor = false;
            this.xrTableCell25.StylePriority.UseBorders = false;
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.StylePriority.UsePadding = false;
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "ĐỊA CHỈ";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell25.Weight = 0.463109392724688D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell26.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell26.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseBorderColor = false;
            this.xrTableCell26.StylePriority.UseBorders = false;
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.Text = ":";
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell26.Weight = 0.04027038106179099D;
            // 
            // diachi_co
            // 
            this.diachi_co.BorderColor = System.Drawing.Color.LightGray;
            this.diachi_co.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.diachi_co.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachi_co")});
            this.diachi_co.Font = new System.Drawing.Font("Arial", 9.25F);
            this.diachi_co.Name = "diachi_co";
            this.diachi_co.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.diachi_co.StylePriority.UseBorderColor = false;
            this.diachi_co.StylePriority.UseBorders = false;
            this.diachi_co.StylePriority.UseFont = false;
            this.diachi_co.StylePriority.UsePadding = false;
            this.diachi_co.StylePriority.UseTextAlignment = false;
            this.diachi_co.Text = "xrTableCell10";
            this.diachi_co.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.diachi_co.Weight = 2.1912844167372607D;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell29,
            this.nganhang_co});
            this.xrTableRow16.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.StylePriority.UseFont = false;
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell27.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell27.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell27.StylePriority.UseBorderColor = false;
            this.xrTableCell27.StylePriority.UseBorders = false;
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.StylePriority.UsePadding = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.Text = "TẠI NH";
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell27.Weight = 0.463109392724688D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell29.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell29.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseBorderColor = false;
            this.xrTableCell29.StylePriority.UseBorders = false;
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = ":";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell29.Weight = 0.040270381061790976D;
            // 
            // nganhang_co
            // 
            this.nganhang_co.BorderColor = System.Drawing.Color.LightGray;
            this.nganhang_co.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.nganhang_co.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nganhang_co")});
            this.nganhang_co.Font = new System.Drawing.Font("Arial", 9.25F);
            this.nganhang_co.Name = "nganhang_co";
            this.nganhang_co.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.nganhang_co.StylePriority.UseBorderColor = false;
            this.nganhang_co.StylePriority.UseBorders = false;
            this.nganhang_co.StylePriority.UseFont = false;
            this.nganhang_co.StylePriority.UsePadding = false;
            this.nganhang_co.StylePriority.UseTextAlignment = false;
            this.nganhang_co.Text = "xrTableCell12";
            this.nganhang_co.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.nganhang_co.Weight = 2.1912844167372616D;
            // 
            // noidung
            // 
            this.noidung.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.noidung.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "noidung")});
            this.noidung.Font = new System.Drawing.Font("Arial", 9.25F);
            this.noidung.LocationFloat = new DevExpress.Utils.PointFloat(477.1531F, 102.0001F);
            this.noidung.Name = "noidung";
            this.noidung.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 6, 3, 100F);
            this.noidung.SizeF = new System.Drawing.SizeF(326.3895F, 47.99997F);
            this.noidung.StylePriority.UseBorders = false;
            this.noidung.StylePriority.UseFont = false;
            this.noidung.StylePriority.UsePadding = false;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(387.6948F, 102.0001F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 3, 6, 3, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(84.45837F, 22.99998F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UsePadding = false;
            this.xrLabel9.Text = "NỘI DUNG";
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel11.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(387.6948F, 164.4999F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(174.805F, 23.00003F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UsePadding = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "KẾ TOÁN KÝ";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable6
            // 
            this.xrTable6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(387.6948F, 25.00006F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable6.SizeF = new System.Drawing.SizeF(334.4307F, 25F);
            this.xrTable6.StylePriority.UseFont = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell30,
            this.tongtien,
            this.dongtien});
            this.xrTableRow9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.StylePriority.UseFont = false;
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell15.StylePriority.UseBorderColor = false;
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UsePadding = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "BẰNG SỐ ";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell15.Weight = 0.95680524460029548D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseBorderColor = false;
            this.xrTableCell30.StylePriority.UseBorders = false;
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = ":";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell30.Weight = 0.074450354332903937D;
            // 
            // tongtien
            // 
            this.tongtien.BorderColor = System.Drawing.Color.LightGray;
            this.tongtien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tongtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tongtien")});
            this.tongtien.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.tongtien.Name = "tongtien";
            this.tongtien.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tongtien.StylePriority.UseBorderColor = false;
            this.tongtien.StylePriority.UseBorders = false;
            this.tongtien.StylePriority.UseFont = false;
            this.tongtien.StylePriority.UsePadding = false;
            this.tongtien.StylePriority.UseTextAlignment = false;
            this.tongtien.Text = "xrTableCell6";
            this.tongtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tongtien.Weight = 2.3724171091099757D;
            // 
            // dongtien
            // 
            this.dongtien.BorderColor = System.Drawing.Color.LightGray;
            this.dongtien.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.dongtien.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dongtien")});
            this.dongtien.Font = new System.Drawing.Font("Arial", 9.25F);
            this.dongtien.Name = "dongtien";
            this.dongtien.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 3, 3, 100F);
            this.dongtien.StylePriority.UseBorderColor = false;
            this.dongtien.StylePriority.UseBorders = false;
            this.dongtien.StylePriority.UseFont = false;
            this.dongtien.StylePriority.UsePadding = false;
            this.dongtien.StylePriority.UseTextAlignment = false;
            this.dongtien.Text = "VND";
            this.dongtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dongtien.Weight = 0.45157054884362469D;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable5.Font = new System.Drawing.Font("Arial", 7.25F);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0.9999912F, 124.9999F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(373.8355F, 25F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14});
            this.xrTableRow8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.StylePriority.UseFont = false;
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell14.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UsePadding = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "& GHI CÓ TÀI KHOẢN";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell14.Weight = 3.0630476061391745D;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel12.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(584.5581F, 164.4999F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(218.9844F, 23.00003F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UsePadding = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "CHỦ TÀI KHOẢN KÝ & ĐÓNG DẤU";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Font = new System.Drawing.Font("Arial", 7.25F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.9999912F, 25.00002F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow7});
            this.xrTable4.SizeF = new System.Drawing.SizeF(373.8355F, 100F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell17,
            this.sotk_no});
            this.xrTableRow4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UsePadding = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "SỐ TK";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell5.Weight = 0.46310939324040029D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell17.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorderColor = false;
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = ":";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell17.Weight = 0.040270380803934927D;
            // 
            // sotk_no
            // 
            this.sotk_no.BorderColor = System.Drawing.Color.LightGray;
            this.sotk_no.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.sotk_no.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.sotk_no.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sotk_no")});
            this.sotk_no.Font = new System.Drawing.Font("Arial", 9.25F);
            this.sotk_no.Name = "sotk_no";
            this.sotk_no.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.sotk_no.StylePriority.UseBorderColor = false;
            this.sotk_no.StylePriority.UseBorderDashStyle = false;
            this.sotk_no.StylePriority.UseBorders = false;
            this.sotk_no.StylePriority.UseFont = false;
            this.sotk_no.StylePriority.UsePadding = false;
            this.sotk_no.StylePriority.UseTextAlignment = false;
            this.sotk_no.Text = "[sotk_no]";
            this.sotk_no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.sotk_no.Weight = 2.1912846278963509D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell18,
            this.tentk_no});
            this.xrTableRow5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.StylePriority.UseFont = false;
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell8.StylePriority.UseBorderColor = false;
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "TÊN TK";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell8.Weight = 0.46310939298254439D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell18.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell18.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorderColor = false;
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = ":";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell18.Weight = 0.040270380932863D;
            // 
            // tentk_no
            // 
            this.tentk_no.BorderColor = System.Drawing.Color.LightGray;
            this.tentk_no.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tentk_no.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tentk_no")});
            this.tentk_no.Font = new System.Drawing.Font("Arial", 9.25F);
            this.tentk_no.Name = "tentk_no";
            this.tentk_no.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.tentk_no.StylePriority.UseBorderColor = false;
            this.tentk_no.StylePriority.UseBorders = false;
            this.tentk_no.StylePriority.UseFont = false;
            this.tentk_no.StylePriority.UsePadding = false;
            this.tentk_no.StylePriority.UseTextAlignment = false;
            this.tentk_no.Text = "xrTableCell8";
            this.tentk_no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tentk_no.Weight = 2.1912846280252785D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell19,
            this.diachi_no});
            this.xrTableRow6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.StylePriority.UseFont = false;
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell10.StylePriority.UseBorderColor = false;
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "ĐỊA CHỈ";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell10.Weight = 0.463109392724688D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell19.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseBorderColor = false;
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.Text = ":";
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell19.Weight = 0.040270381061790983D;
            // 
            // diachi_no
            // 
            this.diachi_no.BorderColor = System.Drawing.Color.LightGray;
            this.diachi_no.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.diachi_no.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "diachi_no")});
            this.diachi_no.Font = new System.Drawing.Font("Arial", 9.25F);
            this.diachi_no.Name = "diachi_no";
            this.diachi_no.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.diachi_no.StylePriority.UseBorderColor = false;
            this.diachi_no.StylePriority.UseBorders = false;
            this.diachi_no.StylePriority.UseFont = false;
            this.diachi_no.StylePriority.UsePadding = false;
            this.diachi_no.StylePriority.UseTextAlignment = false;
            this.diachi_no.Text = "xrTableCell10";
            this.diachi_no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.diachi_no.Weight = 2.1912846281542064D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell21,
            this.nganhang_no});
            this.xrTableRow7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.StylePriority.UseFont = false;
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UsePadding = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "TẠI NH";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell12.Weight = 0.463109392724688D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.BorderColor = System.Drawing.Color.LightGray;
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBorderColor = false;
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = ":";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell21.Weight = 0.040270381061790893D;
            // 
            // nganhang_no
            // 
            this.nganhang_no.BorderColor = System.Drawing.Color.LightGray;
            this.nganhang_no.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.nganhang_no.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "nganhang_no")});
            this.nganhang_no.Font = new System.Drawing.Font("Arial", 9.25F);
            this.nganhang_no.Name = "nganhang_no";
            this.nganhang_no.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.nganhang_no.StylePriority.UseBorderColor = false;
            this.nganhang_no.StylePriority.UseBorders = false;
            this.nganhang_no.StylePriority.UseFont = false;
            this.nganhang_no.StylePriority.UsePadding = false;
            this.nganhang_no.StylePriority.UseTextAlignment = false;
            this.nganhang_no.Text = "xrTableCell12";
            this.nganhang_no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.nganhang_no.Weight = 2.1912846281542069D;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable3.Font = new System.Drawing.Font("Arial", 7.25F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(2.002398F, 6.103516E-05F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(801.5401F, 24.99999F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell7,
            this.xrTableCell4});
            this.xrTableRow3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell3.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(4, 3, 3, 3, 100F);
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "ĐỀ NGHỊ GHI NỢ TÀI KHOẢN";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 2.3325922396323819D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Weight = 0.080453370035986821D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell4.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "SỐ TIỀN";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 2.6017086863574663D;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(270.2915F, 46.875F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(111.5011F, 23F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Ngày: ";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ngaychi", "{0:dd/MM/yyyy}")});
            this.xrLabel29.Font = new System.Drawing.Font("Arial", 9.25F);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(381.7926F, 46.875F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(161.4584F, 23F);
            this.xrLabel29.StylePriority.UseBorders = false;
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rptUyNhiemChi
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion
}