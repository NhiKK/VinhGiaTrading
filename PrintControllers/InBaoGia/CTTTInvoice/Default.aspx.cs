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

public partial class PrintControllers_CTTTInvoice_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
		string md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];
        rpt_chitietthanhtoaninvoice report = new rpt_chitietthanhtoaninvoice();
        String sql = this.CreateSql(startdate, enddate, md_doitackinhdoanh_id);
        this.viewReport(report, sql);
		//Response.Write(sql);
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

    public String CreateSql(string startdate, string enddate, string md_doitackinhdoanh_id)
    {
        LinqDBDataContext db = new LinqDBDataContext();
		if(md_doitackinhdoanh_id == "" | md_doitackinhdoanh_id == null)
			md_doitackinhdoanh_id = "null";
		else
			md_doitackinhdoanh_id = "N{'"+ md_doitackinhdoanh_id +"'}";
		string str = String.Format(@"exec [dbo].[rpt_chitietthanhtoaninvoice] N'{0}',N'{1}',{2}", startdate, enddate, md_doitackinhdoanh_id);
        return str;
    }
}


