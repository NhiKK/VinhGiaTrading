using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;

public partial class PrintControllers_InDanhSachSanPhamDSDH_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        String c_danhsachdathang_id = Request.QueryString["c_danhsachdathang_id"];
        rptThongTinSanPham report = new rptThongTinSanPham();
        this.viewReport(report, getSql(c_danhsachdathang_id));
    }

    public string getSql(string c_danhsachdathang_id)
    {
        String select = "select * from dbo.f_ttsanpham_dsdh('" + c_danhsachdathang_id + "')";
        return select;
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