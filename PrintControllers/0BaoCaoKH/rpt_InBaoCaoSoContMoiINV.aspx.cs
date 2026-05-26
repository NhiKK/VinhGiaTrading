using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;

public partial class PrintControllers_0BaoCaoKH_rpt_InBaoCaoSoContMoiINV : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);

        string nameTemp = "(CT) Báo cáo số cont tương ứng mỗi INV.xls";
        string nameRpt = "Báo cáo số cont tương ứng mỗi INV";
        string sql = CreateSql(context);

        inPDF = "2";
        var task = new System.Threading.Tasks.Task(() =>
        {
            viewReport(sql);
        });

        PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, true);
    }

    public void viewReport(String SqlQuery)
    {

    }

    public string CreateSql(HttpContext context)
    {
        string startdate = context.Request.QueryString["startdate"];
        string enddate = context.Request.QueryString["enddate"];

        var dateF = DateTime.ParseExact(startdate, "dd/MM/yyyy", null);
        var dateT = DateTime.ParseExact(enddate, "dd/MM/yyyy", null);
        string sql = string.Format(@"	 
            declare @tungayStr nvarchar(MAX) = N'{0}';
            declare @denngayStr nvarchar(MAX) = N'{1}';

            declare @tungay datetime = convert(datetime, @tungayStr + N' 00:00:00', 103)
            declare @denngay datetime = convert(datetime, @denngayStr + N' 23:59:59', 103)            

            declare @tblINV table (idINV nvarchar(32), soINV nvarchar(32), ngayMTK datetime)
            insert @tblINV
            select c_packinginvoice_id, so_inv, ngay_motokhai 
            from c_packinginvoice (nolock)
            where ngay_motokhai between @tungay and @denngay and md_trangthai_id = 'HIEULUC'

            declare @tblINV_DT table (idINV nvarchar(32), soINV nvarchar(32), ngayMTK datetime, idINVDT nvarchar(32), idDNX nvarchar(32), soPI nvarchar(32))
            insert @tblINV_DT
            select inv.idINV, inv.soINV, inv.ngayMTK, dinv.c_dongpklinv_id, dinv.c_dongnhapxuat_id, dh.sochungtu
            from c_dongpklinv (nolock) dinv 
            left join c_donhang (nolock) dh on dinv.c_donhang_id = dh.c_donhang_id
            inner join @tblINV inv on dinv.c_packinginvoice_id = inv.idINV

            select distinct 
                @tungayStr as tungay,
                @denngayStr as denngay,
	            inv.soPI,
                inv.soINV, 
                cnx.container + '/' + cnx.soseal + ' ' + isnull(cnx.loaicont, 0) as soCont
            from 
	            @tblINV_DT inv,
                c_dongnhapxuat (nolock) dnx, 
                c_nhapxuat (nolock) cnx
            where 
	            inv.idDNX = dnx.c_dongnhapxuat_id
	            and dnx.c_nhapxuat_id = cnx.c_nhapxuat_id
            order by
                inv.soPI, inv.soINV
		"
        , dateF.ToString("dd/MM/yyyy")
        , dateT.ToString("dd/MM/yyyy")
        );

        //throw new ArgumentNullException(sql);
        return sql;
    }
}
