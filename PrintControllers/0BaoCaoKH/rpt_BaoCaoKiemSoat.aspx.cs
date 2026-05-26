using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintControllers_InBaoCaoTongHopDoanhThu_rpt_BaoCaoKiemSoat : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "", nameRpt = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;

    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        string nameTemp = "(AD) Báo cáo kiểm soát.xls";
        nameRpt = "Báo cáo kiểm soát";
        string sql = CreateSql(context);

        if (sql.StartsWith("false#"))
        {
            Response.Write(sql.Substring(6));
        }
        else
        {
            inPDF = context.Request.QueryString["kieuin"];
            var task = new System.Threading.Tasks.Task(() =>
            {
                viewReport(sql);
            });

            PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, true);
        }
    }

    public void viewReport(String SqlQuery)
    {

    }

    public string CreateSql(HttpContext context)
    {
        string tungay = context.Request.QueryString["startdate"];
        string denngay = context.Request.QueryString["enddate"];
        string khachhang = context.Request.QueryString["doitackinhdoanh_id"];
        string whereTNDN = "";
        string sql = "";

        if (string.IsNullOrWhiteSpace(tungay) | string.IsNullOrWhiteSpace(denngay))
        {
            sql = "false#<center><h1>Khoảng thời gian đang chọn không thể xác định</h1></center>";
        }
        else
        {

            if (!string.IsNullOrWhiteSpace(khachhang))
            {
                whereTNDN += string.Format(" and dh.md_doitackinhdoanh_id = N'{0}'", khachhang);
            }

            sql = string.Format(@"
                declare @tungay datetime = convert(datetime, N'{0} 00:00:00', 103)
                declare @denngay datetime = convert(datetime, N'{1} 23:59:59', 103)

                
                select 
                    N'{0}' as tungay,
                    N'{1}' as denngay, 
	                dh.sochungtu as soPO, 
                    dh.shipmenttime,
	                dh.amount as giatriPO, 
	                inv.so_inv as inv, 
                    inv.ngay_motokhai,
                    inv.tiendatra,
	                --inv.totalgross as giatriINV,

                    (select SUM(dinv.thanhtien) 
		                from c_dongpklinv dinv 
		                where dinv.c_packinginvoice_id = inv.c_packinginvoice_id 
			                and dinv.c_donhang_id = dh.c_donhang_id
		            ) as giatriINV,

	                (
		                select 
			                sum(cttc.sotien)
		                from c_thuchi tc
			                inner join c_chitietthuchi cttc on tc.c_thuchi_id = cttc.c_thuchi_id  
		                where 
			                tc.sophieu like 'PT%' 
			                and tc.md_trangthai_id = 'HIEULUC'
			                and tc.ngay_giaonop between @tungay and @denngay
			                and cttc.isdatcoc = 1
			                and cttc.c_donhang_id = dh.c_donhang_id
	                ) as tiencoc
                from c_donhang dh
	                left join c_packinginvoice inv on dh.c_donhang_id in (
		                select dinv.c_donhang_id 
		                from c_dongpklinv dinv 
		                where dinv.c_packinginvoice_id = inv.c_packinginvoice_id
	                )
                where
	                1=1
	                and dh.shipmenttime between @tungay and @denngay
	                and dh.md_trangthai_id = 'HIEULUC'
	                and isnull(inv.tienconlai, 1) > 0
                    {2}
                order by
                    --dh.shipmenttime, dh.sochungtu, 
                    inv.ngay_motokhai,
                    inv.so_inv
		    "
            , tungay
            , denngay
            , whereTNDN
            );
        }
        return sql;
    }
}