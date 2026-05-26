using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraReports.UI;


public partial class PrintControllers_InTongHopKhieuNai_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String tungay = Context.Request.QueryString["startdate"];
        String denngay = Context.Request.QueryString["enddate"];
        string md_doitackinhdoanh_id = Context.Request.QueryString["doitackinhdoanh_id"];

        md_doitackinhdoanh_id = md_doitackinhdoanh_id == null ? "" : md_doitackinhdoanh_id;

        rptTongHopKhieuNai report = new rptTongHopKhieuNai();
        this.viewReport(report, sql(DateTime.ParseExact(tungay, "MM/dd/yyyy", null), DateTime.ParseExact(denngay, "MM/dd/yyyy", null), md_doitackinhdoanh_id));
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

    public string sql(DateTime startdate, DateTime enddate, string md_doitackinhdoanh_id)
    {
        string str = string.Format(@"
            declare @tungay datetime = convert(datetime, '{0} 00:00:00', 103);
            declare @denngay datetime = convert(datetime, '{1} 23:59:59', 103);

            select 
                tc.ngaylapphieu, 
                tc.sophieu, 
                dtkd.ma_dtkd, 
                pkl.so_inv, 
                tc.lydo as noidung, 
                tc.tongcackhoan as trigia, 
                tc.mota as ghichu
            from c_chitietthuchi cttc join c_thuchi tc on tc.c_thuchi_id = cttc.c_thuchi_id 
	            join md_doitackinhdoanh dtkd on tc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	            left join c_packinginvoice pkl on cttc.c_packinginvoice_id = pkl.c_packinginvoice_id
            where  
	            tc.md_loaichungtu_id = 'd54ae3fc90661fab0f522dc7594cd222'
                and pkl.md_trangthai_id = 'HIEULUC'
				and cttc.loaiphieu = 'IN'
                and tc.ngaylapphieu >= @tungay
                and tc.ngaylapphieu <= @denngay
	            and cttc.c_packinginvoice_id = pkl.c_packinginvoice_id
                {2} 
            order by  tc.ngaylapphieu desc", 
            startdate.ToString("dd/MM/yyyy"), 
            enddate.ToString("dd/MM/yyyy"), 
            md_doitackinhdoanh_id == "" ? "" : "and dtkd.md_doitackinhdoanh_id = '" + md_doitackinhdoanh_id + "'");

        return str;
    }
}
