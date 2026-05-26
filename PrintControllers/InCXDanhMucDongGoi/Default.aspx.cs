using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;


public partial class PrintControllers_InCXDanhMucDongGoi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        rptCXDanhmucDongGoi report = new rptCXDanhmucDongGoi();
        String filter = "";
        if (Request.QueryString["filters"] != null & Request.QueryString["filters"] != "undefined")
        {
            String _filters = Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
        String sql = this.CreateSql(filter);
        this.viewReport(report, sql);
    }

    public void viewReport(XtraReport report, String SqlQuery)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        da.SelectCommand.CommandTimeout = 10000;
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }

    public String CreateSql(string filter)
    {

        String str = String.Format(@"
		select
        dg.ma_donggoi as madonggoi, dg.ten_donggoi as tendonggoi, dg.sl_inner as slinner, (select dvt.ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner) as dvtinner, dg.l1,dg.w1,dg.h1,
        dg.sl_outer as slouter, (select dvt.ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer) as dvtouter, 
        dg.l2_mix as l2, dg.w2_mix as w2, dg.h2_mix as h2,dg.v2,dg.mota as ghichu,dg.ngayxacnhan, dg.mota, getdate() as today,
        dg.soluonggoi_ctn_20 as soluonggoi_ctn, dg.soluonggoi_ctn as vd, dg.soluonggoi_ctn_40hc as vn
		from md_donggoi dg where 1=1 " + filter);
        return str;
    }
}