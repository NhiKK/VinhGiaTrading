using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using System.Data;

public partial class PrintControllers_InXuatKho_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String c_nhapxuat_id = Context.Request.QueryString["c_nhapxuat_id"];
        rptXuatKho report = new rptXuatKho();
        String sql = this.CreateSql(c_nhapxuat_id);
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

    public String CreateSql(String c_nhapxuat_id)
    {
        String selTotal = String.Format(@"select SUM( dnx.dongia * dnx.sl_dathang ) as tongtien 
                            from c_nhapxuat nx, c_dongnhapxuat dnx 
                            where nx.c_nhapxuat_id = dnx.c_nhapxuat_id
                            AND nx.c_nhapxuat_id = N'{0}'", c_nhapxuat_id == null ? "" : c_nhapxuat_id);
        decimal total = (decimal)mdbc.ExecuteScalar(selTotal);

        String str = String.Format(@"
            SELECT 
                nx.sophieu, nx.ngay_giaonhan as ngay, dh.sochungtu, nx.container as so_con, nx.soseal as seal, kho.ten_kho as taikho
                , sp.ma_sanpham, sp.mota_tiengviet, dvt.ma_edi as dvt
                , dnx.slthuc_nhapxuat as soluong, dnx.dongia as dongia, (dnx.slthuc_nhapxuat * dnx.dongia) as thanhtien
                , N'{0} USD' as tien, (select hoten from nhanvien where manv = nx.nguoitao) as hoten
            FROM 
                c_nhapxuat nx , c_dongnhapxuat dnx, md_kho kho
                , md_sanpham sp, md_donvitinhsanpham dvt, c_dongdonhang ddh, c_donhang dh
            WHERE
                nx.c_nhapxuat_id = dnx.c_nhapxuat_id
                AND dnx.md_sanpham_id = sp.md_sanpham_id
                AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
                AND nx.md_kho_id = kho.md_kho_id 
                AND dnx.c_dongdonhang_id = ddh.c_dongdonhang_id
                AND ddh.c_donhang_id = dh.c_donhang_id
                AND nx.c_nhapxuat_id = N'{1}' order by dnx.line asc", MoneyToWord.ConvertMoneyToTextVND(total), c_nhapxuat_id == null ? "" : c_nhapxuat_id);
        return str;
    }
}

