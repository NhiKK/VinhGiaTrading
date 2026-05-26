using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraReports.UI;

public partial class PrintControllers_InYeuCauXuatHang_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String c_donhang_id = Context.Request.QueryString["c_donhang_id"];
        String c_kehoachxuathang_id = Context.Request.QueryString["c_kehoachxuathang_id"];

        rptXuatHang report = new rptXuatHang();
        String sql = YeuCauXuatHangSql.getSql(c_donhang_id,c_kehoachxuathang_id);
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
}
