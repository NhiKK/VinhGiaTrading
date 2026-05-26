using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Collections;
using System.ComponentModel;

public partial class PrintControllers_CTTTInvoice_Default : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string startdate = Request.QueryString["startdate"];
        //string enddate = Request.QueryString["enddate"];
        //string md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];
        //rpt_chitietthanhtoaninvoice report = new rpt_chitietthanhtoaninvoice();
        //      String sql = this.CreateSql(startdate, enddate, md_doitackinhdoanh_id);
        //      this.viewReport(report, sql);

        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);

        string nameTemp = "(CT) Chi tiết thanh toán theo invoice.xls";
        string nameRpt = "Chi tiết thanh toán theo invoice";
        string sql = CreateSql(context);

        inPDF = "2";
        var task = new System.Threading.Tasks.Task(() =>
        {
            viewReport(sql);
        });

        PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, true);
    }

    //public void viewReport(XtraReport report, String SqlQuery)
    //{
    //    SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
    //    da.SelectCommand.CommandTimeout = 50000;
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    report.DataSource = ds;
    //    report.DataAdapter = da;
    //    ReportViewer1.Report = report;
    //}

    public void viewReport(String SqlQuery)
    {

    }

    public String CreateSql(HttpContext context)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];

        if (md_doitackinhdoanh_id == "" | md_doitackinhdoanh_id == null)
            md_doitackinhdoanh_id = "null";
        else
            md_doitackinhdoanh_id = "N'" + md_doitackinhdoanh_id + "'";
        string str = String.Format(@"

                declare @table table (
					tungay datetime,
                    denngay datetime,
					ma_dtkd nvarchar(100),
					ngay_motokhai datetime,
					sopo nvarchar(MAX),
                    so_inv nvarchar(MAX),
                    tongtienpo decimal(18,2),
					phiinvoice decimal(18,2),
                    totalgross decimal(18,2),
                    chitrakhac decimal(18,2),
                    tiendatra decimal(18,2),
					tiendatcoc decimal(18,2),
					sotien decimal(18,2),
                    tienve decimal(18,2),
                    conlai decimal(18,2),
                    today datetime,
                    tiencoc decimal(18,2),
					phicoc decimal(18,2),
					tienthanhtoan decimal(18,2),
                    phithanhtoan decimal(18,2)
                    
                )

                declare @tungay datetime = convert(datetime, N'{0}', 103);
                declare @denngay datetime = convert(datetime, N'{1}', 103);

                insert into @table
                exec [dbo].[rpt_chitietthanhtoaninvoice] @tungay, @denngay, {2}

                select
                    format(A.tungay, 'dd/MM/yyyy') as tungay,format(A.denngay, 'dd/MM/yyyy') as denngay,A.ma_dtkd,
	                format(A.ngay_motokhai, 'dd/MM/yyyy') as ngay_motokhai, A.sopo, A.so_inv,
	                isnull(A.tongtienpo, 0) as tongtienpo,
	                isnull(A.phiinvoice, 0) as phiinvoice,
	                isnull(A.totalgross, 0) as totalgross,
	                isnull(A.chitrakhac, 0) as chitrakhac,
	                isnull(A.tiendatra, 0) as tiendatra,
	                isnull(A.tiendatcoc, 0) as tiendatcoc,
	                isnull(A.sotien, 0) as sotien, 
	                isnull(A.tienve, 0) as tienve, 
	                isnull((A.totalgross - A.tiendatra - A.tiendatcoc - A.chitrakhac),0) as conlai, 
	                format(getDate(), 'dd/MM/yyyy') as today,
                    isnull(A.tiencoc, 0) as tiencoc,
	                isnull(A.phicoc, 0) as phicoc,
	                isnull(A.tienthanhtoan, 0) as tienthanhtoan, 
	                isnull(A.phithanhtoan, 0) as phithanhtoan
  
                from @table A
                order by A.ngay_motokhai
            "
            , startdate
            , enddate
            , md_doitackinhdoanh_id
        );
        return str;
    }
}


