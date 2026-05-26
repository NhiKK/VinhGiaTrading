using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraReports.UI;


public partial class PrintControllers_InXuatHangTheoPO_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime tungay = DateTime.ParseExact(Request.QueryString["startdate"], "MM/dd/yyyy", null);
        DateTime denngay = DateTime.ParseExact(Request.QueryString["enddate"], "MM/dd/yyyy", null);

        rptXuatHangTheoPO report = new rptXuatHangTheoPO();
       String sql = string.Format(@"
            declare @tungay datetime = convert(datetime, '{0} 00:00:00', 103);
            declare @denngay datetime = convert(datetime, '{1} 23:59:59', 103);
            select distinct dtkd.ma_dtkd, dh.sochungtu, SUM(dh.totalamount)/COUNT(ddh.c_donhang_id) + isnull((select SUM(isnull(phi, 0)) from c_phidonhang pdh where pdh.c_donhang_id = dh.c_donhang_id and phitang = 1), 0) as giatripo, SUM(dh.discount) as discount
                , '{2}' as tungay, '{3}' as denngay
	            , SUM(ddh.giafob * ddh.soluong_daxuat) as daxuat
	            , SUM(cttc.sotien)/COUNT(ddh.c_donhang_id) as tongcoc
	            , isnull((select SUM(isnull(phi, 0)) from c_phidonhang pdh where pdh.c_donhang_id = dh.c_donhang_id and phitang = 1), 0) as phicong
	            , isnull((select SUM(isnull(phi, 0)) from c_phidonhang pdh where pdh.c_donhang_id = dh.c_donhang_id and phitang = 0), 0) as phitru
	            , SUM(dh.totalamount)/COUNT(ddh.c_donhang_id) - SUM(dh.discount) 
		            + isnull((select SUM(isnull(phi, 0)) from c_phidonhang pdh where pdh.c_donhang_id = dh.c_donhang_id and phitang = 1), 0)  -- cong phi tang
		            - isnull((select SUM(isnull(phi, 0)) from c_phidonhang pdh where pdh.c_donhang_id = dh.c_donhang_id and phitang = 0), 0) -- tru phi giam
		            - SUM(ddh.giafob * ddh.soluong_daxuat) -- tru so luong da xuat
		            as conlai
            from c_donhang dh 
	            left join md_doitackinhdoanh dtkd on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	            left join c_dongdonhang ddh on ddh.c_donhang_id = dh.c_donhang_id
	            left join c_chitietthuchi cttc on dh.c_donhang_id = cttc.c_donhang_id
            where 
	            dh.md_trangthai_id = 'HIEULUC'
	                
	            and dh.shipmenttime >= @tungay
	            and dh.shipmenttime <= @denngay
            group by dtkd.ma_dtkd, dh.c_donhang_id, dh.sochungtu
        ", tungay, denngay, tungay.ToString("dd/MM/yyyy"), denngay.ToString("dd/MM/yyyy"));
		
		//and cttc.isdatcoc = 1
	            // and cttc.loaiphieu = 'PO'   
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