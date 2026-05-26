using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InSoDuCuoiKy_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        DateTime dateFrom = DateTime.ParseExact(Request.QueryString["startdate"], "MM/dd/yyyy", null);
        DateTime dateTo = DateTime.ParseExact(Request.QueryString["enddate"], "MM/dd/yyyy", null);
        DateTime datenow = DateTime.Now;
        String md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];

        rpt_soducuoiky report = new rpt_soducuoiky();
        String sql = this.CreateSql(dateFrom, dateTo, datenow, md_doitackinhdoanh_id);
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

    public String CreateSql(DateTime dateFrom, DateTime dateTo, DateTime datenow, string dtkd)
    {
        string wheresql = "";
        //if (dtkd.Length > 0)
        //{
        //    wheresql = " and nhan.md_doitackinhdoanh_id = '" + dtkd + "'";
        //}
        String str = String.Format(@"
            select convert(date,'{0}',103) as tungay, convert(date,'{1}',103) as denngay, convert(date,'{2}',103) as today 
            , ten_dtkd, sddk, pstk, sdck
            from
            (
            select dtkd.ma_dtkd as ten_dtkd,dbo.f_getsodudauky(convert(date,'{0}',103),thcn.md_doitackinhdoanh_id) as sddk, 
            (SUM(notrongky - cotrongky)) as pstk, 
            dbo.f_getsoducuoiky(convert(date,'{1}',103),thcn.md_doitackinhdoanh_id) as sdck from a_tonghopcongno thcn

            inner join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = thcn.md_doitackinhdoanh_id 
            where
            thcn.a_kytrongnam_id in (
                select a_kytrongnam_id from
                a_kytrongnam ktn where
                ktn.ngaybatdau <= convert(date,'{1}',103) 
                and ktn.ngaybatdau >= dbo.[f_ngaythangnam](1,MONTH(convert(date,'{0}',103)),Year(convert(date,'{0}',103))
            ))
            group by dtkd.ma_dtkd, thcn.md_doitackinhdoanh_id) as nhan

        ", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), datenow.ToString("dd/MM/yyyy"));
        return str;
    }
}