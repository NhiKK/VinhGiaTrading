using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;


public partial class PrintControllers_InHachToanTheoInvoice_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        DateTime dateFrom = DateTime.ParseExact(Request.QueryString["startdate"], "MM/dd/yyyy", null);
        DateTime dateTo = DateTime.ParseExact(Request.QueryString["enddate"], "MM/dd/yyyy", null);
        DateTime datenow = DateTime.Now;
        String md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];

		rptHoachToanTheoInvoice report = new rptHoachToanTheoInvoice();
        String sql = this.CreateSql(dateFrom, dateTo, datenow, md_doitackinhdoanh_id);
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

    public String CreateSql(DateTime dateFrom, DateTime dateTo, DateTime datenow, string dtkd)
    {
        string wheresql = "";
        if (dtkd != "" & dtkd != null)
        {
            wheresql = "and dtkd.md_doitackinhdoanh_id = '" + dtkd + "'";
        }
        String str = String.Format(@"
        select distinct pkl.so_inv, dtkd.ma_dtkd, nx.sophieu as phieunhap,
		nx.grandtotal as tongtienphieunhap ,  
		isnull(cttc.sotien,0) as datra , cttc.diengiai, (nx.grandtotal - isnull(cttc.sotien,0)) as conlai,
		convert(date,'{0}',103) as tungay,
		convert(date,'{1}',103) as denngay,
		convert(date,'{2}',103) as today
		from c_dongpklinv dpkl
		left join c_packinginvoice pkl on dpkl.c_packinginvoice_id = pkl.c_packinginvoice_id 
		left join c_donhang dh on dpkl.c_donhang_id = dh.c_donhang_id 
		left join c_dongdonhang ddh on dh.c_donhang_id = ddh.c_donhang_id
		left join c_dongnhapxuat dnx on ddh.c_dongdonhang_id = dnx.c_dongdonhang_id
		left join c_nhapxuat nx on nx.c_nhapxuat_id = dnx.c_nhapxuat_id
		left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = nx.md_doitackinhdoanh_id
		left join c_chitietthuchi cttc on cttc.obj_code = nx.c_nhapxuat_id
		left join c_thuchi tc on cttc.c_thuchi_id = tc.c_thuchi_id
		where 
		dongnhapxuat_ref is null and dtkd.isncc = 1 and 
        nx.grandtotal > 0 and 
		pkl.ngay_motokhai >= convert(date,'{0}',103) and ngay_motokhai <= convert(date,'{1}',103) and
		pkl.md_trangthai_id = 'HIEULUC'
		{3}
		order by pkl.so_inv
        ", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), datenow.ToString("dd/MM/yyyy"), wheresql);
        return str;
    }
}