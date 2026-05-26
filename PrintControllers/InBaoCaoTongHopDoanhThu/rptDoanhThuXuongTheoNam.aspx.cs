using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintControllers_InBaoCaoTongHopDoanhThu_rptDoanhThuXuongTheoNam : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);

        var maXuong = context.Request.QueryString["maXuong"];
        string nameTemp = "(KT) Báo cáo tổng hợp doanh thu theo năm xưởng.xls";
        string nameRpt = "Báo cáo tổng hợp doanh thu theo năm của xưởng";
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
        string nam = context.Request.QueryString["nam"];
        string xuong = context.Request.QueryString["xuong"];
        var maXuong = context.Request.QueryString["maXuong"];
        var tenXuong = maXuong == "ANCO1" ? "AN CƠ 1" : (maXuong == "ANCO2" ? "AN CƠ 2" : maXuong);

        string sql = string.Format(@"	 
            declare @tungay datetime = convert(datetime, N'01/01/{0} 00:00:00', 103)
            declare @denngay datetime = convert(datetime, N'31/12/{0} 23:59:59', 103)
            declare @nam int = {0};
            declare @xuongId nvarchar(32) = N'{1}'
            declare @xuongName nvarchar(100) = N'{2}'
            declare @tygia decimal(18, 0) = 23000;           

            declare @tblTriGiaVND table (tien decimal(18,2), thang int)

            insert into @tblTriGiaVND
            select nx.grandtotal as tien, MONTH(nx.ngay_giaonhan) as thang
            from c_nhapxuat (nolock) nx 
            where 
	            ngay_giaonhan between @tungay and @denngay
	            and nx.md_doitackinhdoanh_id = @xuongId
                and nx.md_trangthai_id = 'HIEULUC'
            
            declare @tblTriGiaUSD table (tien decimal(18,2), thang int)
            insert into @tblTriGiaUSD
            select
                isnull(civ.totalgross, 0),
                MONTH(civ.ngay_motokhai)
            from c_packinginvoice (nolock) civ
            where 
                civ.ngay_motokhai between @tungay and @denngay
                and civ.md_trangthai_id = 'HIEULUC'
                and (select count(1) from c_dongpklinv (nolock) cdiv where cdiv.c_packinginvoice_id = civ.c_packinginvoice_id and cdiv.nhacungungid = @xuongId) > 0

            select 
                @nam as nam,
                @xuongName as xuong,
	            (select sum(tien) from @tblTriGiaVND where thang = 1) as triGiaVND1,
	            (select sum(tien) from @tblTriGiaVND where thang = 2) as triGiaVND2,
	            (select sum(tien) from @tblTriGiaVND where thang = 3) as triGiaVND3,
	            (select sum(tien) from @tblTriGiaVND where thang = 4) as triGiaVND4,
	            (select sum(tien) from @tblTriGiaVND where thang = 5) as triGiaVND5,
	            (select sum(tien) from @tblTriGiaVND where thang = 6) as triGiaVND6,
	            (select sum(tien) from @tblTriGiaVND where thang = 7) as triGiaVND7,
	            (select sum(tien) from @tblTriGiaVND where thang = 8) as triGiaVND8,
	            (select sum(tien) from @tblTriGiaVND where thang = 9) as triGiaVND9,
	            (select sum(tien) from @tblTriGiaVND where thang = 10) as triGiaVND10,
	            (select sum(tien) from @tblTriGiaVND where thang = 11) as triGiaVND11,
	            (select sum(tien) from @tblTriGiaVND where thang = 12) as triGiaVND12,
                
                (select sum(tien) from @tblTriGiaUSD where thang = 1) as triGiaUSD1,
	            (select sum(tien) from @tblTriGiaUSD where thang = 2) as triGiaUSD2,
	            (select sum(tien) from @tblTriGiaUSD where thang = 3) as triGiaUSD3,
	            (select sum(tien) from @tblTriGiaUSD where thang = 4) as triGiaUSD4,
	            (select sum(tien) from @tblTriGiaUSD where thang = 5) as triGiaUSD5,
	            (select sum(tien) from @tblTriGiaUSD where thang = 6) as triGiaUSD6,
	            (select sum(tien) from @tblTriGiaUSD where thang = 7) as triGiaUSD7,
	            (select sum(tien) from @tblTriGiaUSD where thang = 8) as triGiaUSD8,
	            (select sum(tien) from @tblTriGiaUSD where thang = 9) as triGiaUSD9,
	            (select sum(tien) from @tblTriGiaUSD where thang = 10) as triGiaUSD10,
	            (select sum(tien) from @tblTriGiaUSD where thang = 11) as triGiaUSD11,
	            (select sum(tien) from @tblTriGiaUSD where thang = 12) as triGiaUSD12,

                (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 1 and YEAR(ngayapdung) = @nam) as tyGia1,
	            (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 2 and YEAR(ngayapdung) = @nam) as tyGia2,
                (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 3 and YEAR(ngayapdung) = @nam) as tyGia3,
                (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 4 and YEAR(ngayapdung) = @nam) as tyGia4,
	            (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 5 and YEAR(ngayapdung) = @nam) as tyGia5,
                (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 6 and YEAR(ngayapdung) = @nam) as tyGia6,
                (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 7 and YEAR(ngayapdung) = @nam) as tyGia7,
	            (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 8 and YEAR(ngayapdung) = @nam) as tyGia8,
                (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 9 and YEAR(ngayapdung) = @nam) as tyGia9,
                (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 10 and YEAR(ngayapdung) = @nam) as tyGia10,
	            (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 11 and YEAR(ngayapdung) = @nam) as tyGia11,
                (select nhan_voi from md_tygiamua where MONTH(ngayapdung) = 12 and YEAR(ngayapdung) = @nam) as tyGia12
		"
        , nam
        , xuong
        , tenXuong
        );

        //throw new ArgumentNullException(sql);
        return sql;
    }
}