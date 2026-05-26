using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InDoanhSoTheoNCC_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        DateTime dateFrom = DateTime.ParseExact(Request.QueryString["startdate"], "dd/MM/yyyy", null);
        DateTime dateTo = DateTime.ParseExact(Request.QueryString["enddate"], "dd/MM/yyyy", null);
        DateTime datenow = DateTime.Now;
        String md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];

        rptDoanhSoTheoNCC report = new rptDoanhSoTheoNCC();
        String sql = this.CreateSql(dateFrom, dateTo, datenow, md_doitackinhdoanh_id);
        this.viewReport(report, sql);
    }

    public void viewReport(XtraReport report, String SqlQuery)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        da.SelectCommand.CommandTimeout = 50000;
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }

    public String CreateSql(DateTime dateFrom, DateTime dateTo, DateTime datenow, string dtkd)
    {
        string wheresql = "";
		string demncc = "nhan.demncc";
        if (dtkd != "" & dtkd != null)
        {
            wheresql = "and dtkd2.md_doitackinhdoanh_id = '" + dtkd + "'";
			demncc = "1";
        }
        String str = String.Format(@"
		
			select nhan.ma_dtkd, nhan.soinv, nhan.soPO, nhan.trigia/{4} as trigia, 
--(case when nhan.trigia = 0 then 0 else nhan.tongtienpx end) as tongtienpx
nhan.tongtienpx
,

--(case when nhan.trigia = 0 then 0 else (nhan.tongtienncc_khac - nhan.tongtienpx) end) as tongtienncc_khac
(nhan.tongtienncc_khac - nhan.tongtienpx) as tongtienncc_khac
,
--(case when nhan.trigia = 0 then 0 else nhan.phicong_inv end) as phicong_inv,
 nhan.phicong_inv,
--(case when nhan.trigia = 0 then 0 else nhan.phitru_inv end) as phitru_inv,
nhan.phitru_inv ,
--(case when nhan.trigia = 0 then 0 else nhan.phicong_po end) as phicong_po, 
nhan.phicong_po,
--(case when nhan.trigia = 0 then 0 else nhan.phitru_po end) as phitru_po, 
nhan.phitru_po,

nhan.cont20, nhan.cont40,
nhan.cont40hc, 
(case when nhan.trigia = 0 then 0 else nhan.discount end) as discount,
nhan.makh,nhan.ngaymtk,
nhan.thang, nhan.tungay, nhan.denngay, nhan.today, 
--(case when nhan.trigia = 0 then 0 else nhan.totaldiscount end) as totaldiscount
nhan.totaldiscount
from
(
select distinct dtkd.ma_dtkd as makh , inv.so_inv as soinv, dbo.f_getPOfrominv(inv.c_packinginvoice_id) as soPO,
            inv.totalgross as trigia, convert(float,[dbo].[f_gettienDoanhSoTheoNCC](dpkl.c_packinginvoice_id, dtkd2.md_doitackinhdoanh_id,null, inv.discount)) as tongtienpx, 
	        convert(float,[dbo].[f_gettienDoanhSoTheoNCC](dpkl.c_packinginvoice_id,null,null, inv.discount)) as tongtienncc_khac, 
            [dbo].[f_gettienDoanhSoTheoNCC](dpkl.c_packinginvoice_id, dtkd2.md_doitackinhdoanh_id, 'CONT20',inv.discount) as cont20, 
            [dbo].[f_gettienDoanhSoTheoNCC](dpkl.c_packinginvoice_id, dtkd2.md_doitackinhdoanh_id, 'CONT40',inv.discount) as cont40, 
            [dbo].[f_gettienDoanhSoTheoNCC](dpkl.c_packinginvoice_id, dtkd2.md_doitackinhdoanh_id, '40HC',inv.discount) as cont40hc, 
            inv.giatri_cong  as phicong_inv,
			inv.giatri_tru as phitru_inv,
			(select [dbo].[f_getSLNCCtheoINV](dpkl.c_packinginvoice_id)) as demncc,
			inv.discount,
			inv.totaldis as totaldiscount,
            dtkd2.ma_dtkd as ma_dtkd,
            convert(date,'{0}',103) as tungay,
            convert(date,'{1}',103) as denngay,
            convert(date,'{2}',103) as today,
            inv.giatricong_po as phicong_po,
			inv.giatritru_po as phitru_po,
            MONTH(convert(date,inv.ngay_motokhai,103))as thang,
            convert(date,inv.ngay_motokhai,103)as ngaymtk
            from c_packinginvoice inv
            inner join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = inv.md_doitackinhdoanh_id
            inner join c_dongpklinv dpkl on dpkl.c_packinginvoice_id = inv.c_packinginvoice_id
            inner join c_dongnhapxuat dnx on dnx.c_dongnhapxuat_id = dpkl.c_dongnhapxuat_id
            inner join c_dongnhapxuat dnx2 on dnx2.c_dongnhapxuat_id = dnx.dongnhapxuat_ref
            inner join c_nhapxuat nx on nx.c_nhapxuat_id = dnx2.c_nhapxuat_id
            inner join md_doitackinhdoanh dtkd2 on nx.md_doitackinhdoanh_id = dtkd2.md_doitackinhdoanh_id
            inner join (select top 1 tg.chia_cho from md_tygia tg 
            where tg.hieuluc_tungay = (select Max(hieuluc_tungay) from md_tygia) 
            )N on 1=1 
            where 1=1 {3}
            and inv.ngay_motokhai >= convert(date,'{0}',103) and inv.ngay_motokhai <= convert(date,'{1}',103)
            group by dtkd.ma_dtkd, inv.so_inv, inv.totalgross , dtkd2.ma_dtkd, inv.c_packinginvoice_id,
             nx.md_doitackinhdoanh_id,
            dtkd.md_doitackinhdoanh_id, dtkd2.md_doitackinhdoanh_id,
            nx.c_nhapxuat_id, dnx2.c_nhapxuat_id, inv.ngay_motokhai,
            inv.giatri_cong, inv.giatri_tru, N.chia_cho, inv.c_packinginvoice_id, dpkl.c_packinginvoice_id,
            nx.loaicont, inv.giatricong_po, inv.giatritru_po, inv.discount, inv.totaldis
)as nhan

order by ngaymtk asc                
", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), datenow.ToString("dd/MM/yyyy"), wheresql, demncc);
        return str;
    }
}