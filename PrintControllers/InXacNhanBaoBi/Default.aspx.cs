using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InXacNhanBaoBi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String md_donhang_id = Request.QueryString["c_donhang_id"];
        rptXacNhanBaoBi report = new rptXacNhanBaoBi();
        String sql = this.CreateSql(md_donhang_id);
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

    public String CreateSql(String md_donhang_id)
    {
        String str = String.Format(@"select 
                                        pi_.c_danhsachdathang_id, po.sochungtu , pi_.ngaylap, dt.ten_dtkd, pi_.c_danhsachdathang_id
                                    from 
                                        c_donhang po, c_danhsachdathang pi_, md_doitackinhdoanh dt
                                    where 
                                        po.c_donhang_id = pi_.c_donhang_id
	                                    and pi_.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
	                                    and po.c_donhang_id = '{0}'", md_donhang_id == null ? "" : md_donhang_id);
        return str;
    }
}
