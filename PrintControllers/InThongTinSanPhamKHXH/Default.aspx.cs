using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;


public partial class PrintControllers_InThongTinSanPhamKHXH_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        String c_kehoachxuathang_id = Request.QueryString["c_kehoachxuathang_id"];
        c_kehoachxuathang khxh = db.c_kehoachxuathangs.FirstOrDefault(p=>p.c_kehoachxuathang_id.Equals(c_kehoachxuathang_id));
        c_danhsachdathang dsdh = db.c_danhsachdathangs.FirstOrDefault(d=>d.sochungtu.Equals(khxh.c_danhsachdathang_id));
        String sql = "select * from dbo.f_ttsanpham_dsdh('" + dsdh.c_danhsachdathang_id + "')";
        rptThongTinSanPham report = new rptThongTinSanPham();
		report.Parameters["so_dsdh"].Value = dsdh.sochungtu;
        report.Parameters["so_donhang"].Value = dsdh.so_po;
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