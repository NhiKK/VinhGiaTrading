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

public partial class PrintControllers_0BaoCaoKH_rpt_InBaoCaoTheoDoiXuatHang : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);

        string nameTemp = "(CT) Báo cáo theo dõi xuất hàng.xls";
        string nameRpt = "Báo cáo theo dõi xuất hàng";
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
        var tbl = ((DataSet)ReportViewer1.Report.DataSource).Tables[0];
        var soINV = "";

        foreach (DataRow row in tbl.Rows)
        {
            var soINV2 = row["soINV"].ToString();
            if (soINV != soINV2)
            {
                soINV = soINV2;
            }
            else
            {
                row["tongtien"] = DBNull.Value;
            }
        }
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
            declare @ngayDTcuaNam datetime = DATEADD(yy, DATEDIFF(yy, 0, @tungay), 0);

            declare @tongTruocDo decimal(18,2) = isnull((
	            select sum(isnull(totalgross, 0))
	            from c_packinginvoice (nolock)
	            where ngay_motokhai >= @ngayDTcuaNam and ngay_motokhai < @tungay and md_trangthai_id = 'HIEULUC'
            ), 0)

            declare @tblINV table (
	            idINV nvarchar(32), soINV nvarchar(32), soPO nvarchar(MAX), 
	            ngayMTK datetime, ngaytaoinvoice datetime, nguoilap nvarchar(32), 
	            soTKHQ nvarchar(32), tongtien decimal(18,2), khutrung nvarchar(100), 
	            nhavanchuyen nvarchar(100), canCont nvarchar(50), ghiChu nvarchar(150)
            )

            insert @tblINV
            select 
	            c_packinginvoice_id, 
	            so_inv, 
	            '' as soPO,
	            ngay_motokhai, 
	            ngaylap, 
	            nguoitao, 
	            sotokhai,
	            totalgross, 
	            ttkhutrung, 
	            nhavanchuyen, 
	            cancont, 
	            ghichu
            from c_packinginvoice (nolock)
            where 
	            ngay_motokhai between @tungay and @denngay
	            and md_trangthai_id = 'HIEULUC'

            declare @tblINV_DT table (idINV nvarchar(32), soINV nvarchar(32), ngayMTK datetime, idINVDT nvarchar(32), idDNX nvarchar(32), soPO nvarchar(MAX),
						                ngaytaoinvoice datetime, nguoilap nvarchar(32), nvchungtu nvarchar(32), soTKHQ nvarchar(32), tongtien decimal(18,2),
						                khutrung nvarchar(100), nhavanchuyen nvarchar(100), canCont nvarchar(50), ghiChu nvarchar(150))
            insert @tblINV_DT
            select distinct inv.idINV, inv.soINV, inv.ngayMTK, dinv.c_dongpklinv_id, dinv.c_dongnhapxuat_id, dh.sochungtu,
	                inv.ngaytaoinvoice, nv.hoten, nvct.hoten, inv.soTKHQ, ISNULL(inv.tongtien, 0), inv.khutrung,
	                inv.nhavanchuyen, inv.canCont, inv.ghiChu
            from c_dongpklinv (nolock) dinv 
            inner join c_donhang (nolock) dh on dinv.c_donhang_id = dh.c_donhang_id
            inner join @tblINV inv on dinv.c_packinginvoice_id = inv.idINV
            left join nhanvien nv on nv.manv = dh.nguoilap
            left join nhanvien nvct on nvct.manv = inv.nguoilap

            select 
	            @tungayStr as tungay,
	            @denngayStr as denngay,
                @tongTruocDo as tongTruocDo,
	            A.ngaytaoinvoice,
	            A.nguoilap,
	            A.nvchungtu,
	            A.soPO,
	            A.soINV, 
	            A.soTKHQ,
	            sum(A.c45) as c45,
	            sum(A.c40) as c40,
	            sum(A.c40hc) as c40hc,
	            sum(A.c20) as c20,
	            sum(A.cle) as cle,
	            A.tongtien,
	            A.soCont,
	            A.soSeal,
	            A.khutrung,
	            A.nhavanchuyen,
	            A.canCont,
	            A.ghiChu
            from (
	            select distinct
		            FORMAT(inv.ngayMTK, 'dd/MM/yy') as ngaytaoinvoice,
		            STUFF(
			            (
				            select ', ' + A.nguoilap AS [text()]
				            from (
					            select distinct inv2.nguoilap
					            from @tblINV_DT inv2 
					            where inv2.soINV = inv.soINV
				            )A
				            FOR XML PATH ('')
			            ), 
			            1, 2, ''
		            ) as nguoilap,
		            inv.nvchungtu,
		            STUFF(
			            (
				            select ', ' + A.soPO AS [text()]
				            from (
					            select distinct inv2.soPO
					            from @tblINV_DT inv2 
					            where inv2.soINV = inv.soINV
				            )A
				            FOR XML PATH ('')
			            ), 
			            1, 2, ''
		            ) as soPO,
		            inv.soINV,
		            inv.soTKHQ,
		            (case when cnx.loaicont like N'%45' then cnx.socontainer else 0 end) as c45,
		            (case when cnx.loaicont like N'%40' then cnx.socontainer else 0 end) as c40,
		            (case when cnx.loaicont like N'40%' then cnx.socontainer else 0 end) as c40hc,
		            (case when cnx.loaicont like N'%20' then cnx.socontainer else 0 end) as c20,
		            (case when cnx.loaicont like N'%LE' then cnx.socontainer else 0 end) as cle,
		            inv.tongtien,
		            cnx.container as soCont,
		            cnx.soseal as soSeal,
		            inv.khutrung,
		            inv.nhavanchuyen,
		            inv.canCont,
		            inv.ghiChu
	            from 
		            @tblINV_DT inv,
		            c_dongnhapxuat (nolock) dnx, 
		            c_nhapxuat (nolock) cnx
	            where 
		            inv.idDNX = dnx.c_dongnhapxuat_id
		            and dnx.c_nhapxuat_id = cnx.c_nhapxuat_id
					and isnull(inv.soINV,'') != ''
            ) A
	            group by
		            A.ngaytaoinvoice,
		            A.nguoilap,
		            A.nvchungtu,
		            A.soPO,
		            A.soINV, 
		            A.soTKHQ,
		            A.tongtien,
		            A.soCont,
		            A.soSeal,
		            A.khutrung,
		            A.nhavanchuyen,
		            A.canCont,
		            A.ghiChu
            order by
                A.soINV
		"
        , dateF.ToString("dd/MM/yyyy")
        , dateT.ToString("dd/MM/yyyy")
        );

        //throw new ArgumentNullException(sql);
        return sql;
    }
}
