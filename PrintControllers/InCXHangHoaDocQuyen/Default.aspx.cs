using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;


public partial class PrintControllers_InCXHangHoaDocQuyen_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        rptCXHangHoaDocQuyen report = new rptCXHangHoaDocQuyen();
        String filter = "", md_dtkd = "";
        if (Request.QueryString["filters"] != null & Request.QueryString["filters"] != "undefined")
        {
            String _filters = Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
		else if (Request.QueryString["doitackinhdoanh_id"] != "NULL")
        {
            String _md_dtkd = Request.QueryString["doitackinhdoanh_id"];
            md_dtkd = " and dtkd.md_doitackinhdoanh_id = '"+ _md_dtkd +"'";
        }
        String sql = this.CreateSql(filter,md_dtkd);
        this.viewReport(report, sql);
		//Response.Write(sql);
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

    public String CreateSql(string filter, string md_dtkd)
    {

        String str = String.Format(@"
        select sp.ma_sanpham as ma_hh, 
        dtkd.ten_dtkd as ma_dtkd, 
        getdate() as today
        from md_hanghoadocquyen hhdq 
        left join md_sanpham sp on hhdq.md_sanpham_id = sp.md_sanpham_id
        left join md_doitackinhdoanh dtkd on hhdq.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
        where 1=1 " + md_dtkd + " " + filter + @"
		group by dtkd.ten_dtkd, sp.ma_sanpham
		order by ma_dtkd, ma_hh" );
        return str;
    }
}