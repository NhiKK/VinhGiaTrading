using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraReports.UI;

public partial class PrintControllers_InDanhSachInVoiceChamThanhToan_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String tungay = Context.Request.QueryString["startdate"];
        String denngay = Context.Request.QueryString["enddate"];
        string md_doitackinhdoanh_id = Context.Request.QueryString["md_doitackinhdoanh_id"];

        rptInvoiceChamThanhToan report = new rptInvoiceChamThanhToan();
        this.viewReport(report, sql(DateTime.ParseExact(tungay,"MM/dd/yyyy", null), DateTime.ParseExact(denngay, "MM/dd/yyyy", null)));
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

    public string sql(DateTime ngaysosanh, DateTime ngaysosanh2)
    {
        string str = string.Format(@"
            declare @tungay datetime = convert(datetime, '{0} 00:00:00', 103);
            declare @denngay datetime = convert(datetime, '{1} 23:59:59', 103);

            select pkl.tiendatcoc as tiencoc,
	            pkl.ngaylap, pkl.so_inv, pkl.totalgross, dbo.LayChungTuDonHang(pkl.c_packinginvoice_id) as donhang
			    , (
				    select top 1 pmt.ten_paymentterm from c_dongpklinv dpkl 
					    left join c_donhang dh on dpkl.c_donhang_id = dh.c_donhang_id 
					    left join md_paymentterm pmt on dh.md_paymentterm_id = pmt.md_paymentterm_id
					    where dpkl.c_packinginvoice_id = pkl.c_packinginvoice_id
			    ) as paymentterm
	            , pkl.ngay_motokhai, DATEADD(day, 14, pkl.ngay_motokhai) as ngayphaithanhtoan 
	            , DATEDIFF(day, getdate(), pkl.ngay_motokhai) + 14 as songaychamtra
			    , @tungay as tungay, @denngay as denngay
            from 
	            c_packinginvoice pkl
            where
                pkl.ngay_motokhai >= @tungay and  pkl.ngay_motokhai <= @denngay
                and pkl.md_trangthai_id = 'HIEULUC'
                and pkl.tienconlai > 20
	            and (DATEDIFF(day, getdate(), pkl.ngay_motokhai) + 14) < 0
            order by ngaylap desc, so_inv asc", 
            ngaysosanh.ToString("dd/MM/yyyy"), 
            ngaysosanh2.ToString("dd/MM/yyyy"));
        return str;
    }
}