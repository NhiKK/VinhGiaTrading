using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InTienHangChiTiet_rpt_tienhangdanhap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        DateTime dateFrom = DateTime.ParseExact(Request.QueryString["startdate"], "dd/MM/yyyy", null);
        DateTime dateTo = DateTime.ParseExact(Request.QueryString["enddate"], "dd/MM/yyyy", null);
        DateTime datenow = DateTime.Now;
        string md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];
        string cdxem = HttpContext.Current.Request.QueryString["cdxem"];
        String sql = this.CreateSql(dateFrom, dateTo, datenow, md_doitackinhdoanh_id);

        string file = Server.MapPath("../../ReportsStorage/[KT] Tổng hợp tiền hàng đã nhập.repx");
        var report = XtraReport.FromFile(file, true);
        this.viewReport(report, sql);
        //Response.Write(sql);
    }

     public void viewReport(XtraReport report, String SqlQuery)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        var tbl = ds.Tables[0];
        if (tbl.Rows.Count > 0)
        {
            tbl.Columns.Add("sumPhiCong", Type.GetType("System.Double"));
            tbl.Columns.Add("sumPhiTru", Type.GetType("System.Double"));
            tbl.Columns.Add("sumThanhTien", Type.GetType("System.Double"));
            tbl.Columns.Add("sumDatra", Type.GetType("System.Double"));
            tbl.Columns.Add("sumConLai", Type.GetType("System.Double"));
            tbl.Columns.Add("sumTotalGross", Type.GetType("System.Double"));
            tbl.Columns.Add("sumPhiCongNCC", Type.GetType("System.Double"));
            tbl.Columns.Add("sumPhiTruNCC", Type.GetType("System.Double"));
            tbl.Columns.Add("sumThanhTienNCC", Type.GetType("System.Double"));
            tbl.Columns.Add("sumDatraNCC", Type.GetType("System.Double"));
            tbl.Columns.Add("sumConLaiNCC", Type.GetType("System.Double"));
            tbl.Columns.Add("sumTotalGrossNCC", Type.GetType("System.Double"));
            
            var sumPhiCongVal = tbl.Compute("Sum(phicong)", string.Empty).ToString();
            var sumPhiTruVal = tbl.Compute("Sum(phitru)", string.Empty).ToString();
            var sumThanhTienVal = tbl.Compute("Sum(thanhtien)", string.Empty).ToString();
            var sumDatraVal = tbl.Compute("Sum(datra)", string.Empty).ToString();
            var sumConLaiVal = tbl.Compute("Sum(conlai)", string.Empty).ToString();
            var sumTotalGrossVal = tbl.Compute("Sum(totalgross)", string.Empty).ToString();

            var thanhtienPN = report.Report.FindControl("thanhtienPN", true);
            var phicong = report.Report.FindControl("phicong", true);
            var phitru = report.Report.FindControl("phitru", true);
            var thanhtien = report.Report.FindControl("thanhtien", true);
            var datra = report.Report.FindControl("datra", true);
            var conLai = report.Report.FindControl("conLai", true);
            var giatriINV = report.Report.FindControl("giatriINV", true);
            var sumPhiCong = report.Report.FindControl("sumPhiCong", true);
            var sumPhiTru = report.Report.FindControl("sumPhiTru", true);
            var sumThanhTien = report.Report.FindControl("sumThanhTien", true);
            var sumDatra = report.Report.FindControl("sumDatra", true);
            var sumConLai = report.Report.FindControl("sumConLai", true);
            var sumPhiCongNCC = report.Report.FindControl("sumPhiCongNCC", true);
            var sumPhiTruNCC = report.Report.FindControl("sumPhiTruNCC", true);
            var sumThanhTienNCC = report.Report.FindControl("sumThanhTienNCC", true);
            var sumDatraNCC = report.Report.FindControl("sumDatraNCC", true);
            var sumConLaiNCC = report.Report.FindControl("sumConLaiNCC", true);

            thanhtienPN.DataBindings[0].FormatString =
            phicong.DataBindings[0].FormatString =
            phitru.DataBindings[0].FormatString =
            thanhtien.DataBindings[0].FormatString =
            datra.DataBindings[0].FormatString =
            conLai.DataBindings[0].FormatString =
            giatriINV.DataBindings[0].FormatString =
            sumPhiCong.DataBindings[0].FormatString =
            sumPhiTru.DataBindings[0].FormatString =
            sumThanhTien.DataBindings[0].FormatString =
            sumDatra.DataBindings[0].FormatString =
            sumConLai.DataBindings[0].FormatString =
            sumPhiCongNCC.DataBindings[0].FormatString =
            sumPhiTruNCC.DataBindings[0].FormatString =
            sumThanhTienNCC.DataBindings[0].FormatString =
            sumDatraNCC.DataBindings[0].FormatString =
            sumConLaiNCC.DataBindings[0].FormatString = "{0:#,#0.##}";

            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                DataRow row = tbl.Rows[i];
                //Header
                if (string.IsNullOrEmpty(sumPhiCongVal))
                    row["sumPhiCong"] = DBNull.Value;
                else
                    row["sumPhiCong"] = double.Parse(sumPhiCongVal);

                if (string.IsNullOrEmpty(sumPhiTruVal))
                    row["sumPhiTru"] = DBNull.Value;
                else
                    row["sumPhiTru"] = double.Parse(sumPhiTruVal);

                if (string.IsNullOrEmpty(sumThanhTienVal))
                    row["sumThanhTien"] = DBNull.Value;
                else
                    row["sumThanhTien"] = double.Parse(sumThanhTienVal);

                if (string.IsNullOrEmpty(sumDatraVal))
                    row["sumDatra"] = DBNull.Value;
                else
                    row["sumDatra"] = double.Parse(sumDatraVal);

                if (string.IsNullOrEmpty(sumConLaiVal))
                    row["sumConLai"] = DBNull.Value;
                else
                    row["sumConLai"] = double.Parse(sumConLaiVal);

                string ncc = row["ma_dtkd"].ToString();
                string value = "";
                row["sumPhiCongNCC"] = double.Parse("0");
                value = tbl.Compute("Sum(phicong)", string.Format("[ma_dtkd] = '{0}'", ncc)).ToString();
                if (string.IsNullOrEmpty(value))
                    row["sumPhiCongNCC"] = double.Parse("0");
                else
                    row["sumPhiCongNCC"] = double.Parse(value);

                value = tbl.Compute("Sum(phitru)", string.Format("[ma_dtkd] = '{0}'", ncc)).ToString();
                if (string.IsNullOrEmpty(value))
                    row["sumPhiTruNCC"] = DBNull.Value;
                else
                    row["sumPhiTruNCC"] = decimal.Parse(value);

                value = tbl.Compute("Sum(thanhtien)", string.Format("[ma_dtkd] = '{0}'", ncc)).ToString();
                if (string.IsNullOrEmpty(value))
                    row["sumThanhTienNCC"] = DBNull.Value;
                else
                    row["sumThanhTienNCC"] = decimal.Parse(value);

                value = tbl.Compute("Sum(datra)", string.Format("[ma_dtkd] = '{0}'", ncc)).ToString();
                if (string.IsNullOrEmpty(value))
                    row["sumDaTraNCC"] = DBNull.Value;
                else
                    row["sumDaTraNCC"] = decimal.Parse(value);

                value = tbl.Compute("Sum(conlai)", string.Format("[ma_dtkd] = '{0}'", ncc)).ToString();
                if (string.IsNullOrEmpty(value))
                    row["sumConLaiNCC"] = DBNull.Value;
                else
                    row["sumConLaiNCC"] = decimal.Parse(value);
            }
            report.DataSource = ds;
            report.DataAdapter = da;
            ReportViewer1.Report = report;
        }
        else
        {
            Response.Write("<h1>Không có dữ liệu</h1>");
        }
    }

    public String CreateSql(DateTime dateFrom, DateTime dateTo, DateTime datenow, string dtkd)
    {
        string cdxem = HttpContext.Current.Request.QueryString["cdxem"];
        string str = "";

        dtkd = string.IsNullOrEmpty(dtkd) ? "null" : string.Format("N'{0}'", dtkd);
        str = String.Format(@"
            DECLARE @startdate datetime = convert(datetime, '{0}', 103) 
            DECLARE @enddate datetime = convert(datetime, '{1}', 103)
            declare @dtkd nvarchar(32) =  {2}
            exec rpt_giatritienhang @startdate, @enddate, @dtkd
        ", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), dtkd);
          
        return str;
    }
}