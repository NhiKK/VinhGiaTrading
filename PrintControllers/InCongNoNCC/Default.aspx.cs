using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintControllers_InCongNoNCC_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime tungay = DateTime.ParseExact(Request.QueryString["startdate"], "MM/dd/yyyy", null);
        DateTime denngay = DateTime.ParseExact(Request.QueryString["enddate"], "MM/dd/yyyy", null);
        string dtkd = Request.QueryString["doitackinhdoanh_id"];
        rptCongNoNCC report = new rptCongNoNCC();
        if (dtkd == null)
        {
            this.viewReport2(report, "rpt_congnophaitrancc2", tungay, denngay);
        }
        else
        {
            this.viewReport(report, "rpt_congnophaitrancc", tungay, denngay, dtkd);
        }
    }

    public void viewReport(XtraReport report, String SqlQuery, DateTime start, DateTime end, string dtkd)
    {
        SqlCommand cmd = mdbc.CreateCommand(SqlQuery, "@startdate", start, "@enddate", end, "@dtkd", dtkd);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }

    public void viewReport2(XtraReport report, String SqlQuery, DateTime start, DateTime end)
    {
        SqlCommand cmd = mdbc.CreateCommand(SqlQuery, "@startdate", start, "@enddate", end);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }
}