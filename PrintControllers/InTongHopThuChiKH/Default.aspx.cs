using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InTongHopThuChiKH_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        DateTime dateFrom = DateTime.ParseExact(Request.QueryString["startdate"], "MM/dd/yyyy", null);
        DateTime dateTo = DateTime.ParseExact(Request.QueryString["enddate"], "MM/dd/yyyy", null);
        DateTime datenow = DateTime.Now;
        String md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];

        rptTongHopThuChi report = new rptTongHopThuChi();
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
        if (dtkd != "" & dtkd != null)
        {
            wheresql = "and dtkd2.md_doitackinhdoanh_id = '" + dtkd + "'";
        }
        String str = String.Format(@"
            select dtkd2.md_doitackinhdoanh_id, dtkd2.ma_dtkd,
            (select SUM(tongtientt.sotien) from
				 (select distinct tc.c_thuchi_id, 
                 (case tc.md_dongtien_id when '964dd487f30e799f585cfab3ec5a178e' then tc.sotien/N.chia_cho
                 else
                 tc.sotien end
                 )as sotien
                        from c_thuchi tc
                        left join c_chitietthuchi cttc on cttc.c_thuchi_id = tc.c_thuchi_id
                        left join c_packinginvoice pkl on cttc.c_packinginvoice_id = pkl.c_packinginvoice_id
						left join (select top 1 tg.chia_cho from md_tygia tg 
                        where tg.hieuluc_tungay = (select Max(hieuluc_tungay) from md_tygia) 
                        )N on 1=1 
                        where tc.md_loaichungtu_id = 'af267ebc35a04973698def488f8c3124'
                        and tc.md_doitackinhdoanh_id = dtkd2.md_doitackinhdoanh_id
                        and pkl.ngay_motokhai >=  convert(date,'{0}',103) 
                        and pkl.ngay_motokhai <=  convert(date,'{1}',103)
                        and tc.md_trangthai_id =  'HIEULUC'
				 ) as tongtientt
             ) as tongthu ,

            (select SUM(tongtientc.sotien) from
                (select distinct tc.c_thuchi_id, 
                 (case tc.md_dongtien_id when '964dd487f30e799f585cfab3ec5a178e' then tc.sotien/N.chia_cho
                 else
                 tc.sotien end
                 )as sotien
                        from c_thuchi tc
                        left join c_chitietthuchi cttc on cttc.c_thuchi_id = tc.c_thuchi_id
                        left join c_packinginvoice pkl on cttc.c_packinginvoice_id = pkl.c_packinginvoice_id
						left join (select top 1 tg.chia_cho from md_tygia tg 
                        where tg.hieuluc_tungay = (select Max(hieuluc_tungay) from md_tygia) 
                        )N on 1=1 
                        where tc.md_loaichungtu_id = 'd54ae3fc90661fab0f522dc7594cd222'
                        and tc.md_doitackinhdoanh_id = dtkd2.md_doitackinhdoanh_id
                        and pkl.ngay_motokhai >=  convert(date,'{0}',103) 
                        and pkl.ngay_motokhai <=  convert(date,'{1}',103)
                        and tc.md_trangthai_id =  'HIEULUC'
				 ) as tongtientc
             ) as tongchi ,
            convert(date,'{2}',103)  as today,
            convert(date,'{0}',103)  as tungay,
            convert(date,'{1}',103)  as denngay
            from md_doitackinhdoanh dtkd2 
            where dtkd2.isncc = 0 {3}
        ", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), datenow.ToString("dd/MM/yyyy"), wheresql);
        return str;
    }
}
