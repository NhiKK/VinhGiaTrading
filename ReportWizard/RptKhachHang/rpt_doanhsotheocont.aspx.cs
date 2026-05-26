using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;


public partial class ReportWizard_RptKhachHang_rpt_doanhsotheocont : System.Web.UI.Page
{
     protected void Page_Load(object sender, EventArgs e)
    {
        String startdate = Request.QueryString["startdate"];
        String enddate = Request.QueryString["enddate"];
        rptDoanhSoTheoCont report = new rptDoanhSoTheoCont();
        this.viewReport(report, "rpt_doanhsotheocont", DateTime.ParseExact(startdate, "MM/dd/yyyy", null), DateTime.ParseExact(enddate, "MM/dd/yyyy", null));
    }

    public void viewReport(XtraReport report, String SqlQuery, DateTime startdate, DateTime enddate)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = 5000000;
        da.SelectCommand.Parameters.AddWithValue("@startdate", startdate);
        da.SelectCommand.Parameters.AddWithValue("@enddate", enddate);
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }

}
