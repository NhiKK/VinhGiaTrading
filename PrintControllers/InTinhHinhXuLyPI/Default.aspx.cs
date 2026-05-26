using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using System.Data;

public partial class PrintControllers_InTinhHinhXuLyPI_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String c_donhang_id = Context.Request.QueryString["c_donhang_id"];
        rptTinhHinhXuLyPI report = new rptTinhHinhXuLyPI();
        String sql = this.CreateSql(c_donhang_id);
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

   public String CreateSql(String c_donhang_id)
    {
        String str = String.Format(@"select 
	                        dh.sochungtu, dh.ngaylap, dh.shipmenttime, (select hoten from nhanvien where manv = dh.nguoilap) as nguoiphutrach,
	                        sp.ma_sanpham, ddh.mota_tienganh, ddh.soluong, ddh.soluong_daxuat, (ddh.soluong - ddh.soluong_daxuat) as soluong_conlai, round((((ddh.soluong - ddh.soluong_daxuat) * v2) / ddh.soluong), 3) as cbm_conlai
                        from 
	                        c_donhang dh, c_dongdonhang ddh, md_sanpham sp
                        where
	                        dh.c_donhang_id = ddh.c_donhang_id
	                        AND ddh.md_sanpham_id = sp.md_sanpham_id
                            AND dh.c_donhang_id = '{0}'", c_donhang_id);
        return str;
    }
}

