using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintControllers_InBaoCaoTongHopDoanhThu_rptDoanhThuChiPhiXuongTheoThang : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);

        var maXuong = context.Request.QueryString["maXuong"];
        string nameTemp = "(KT) Báo cáo tổng hợp doanh thu - chi phí theo tháng của xưởng " + maXuong + ".xls";
        string nameRpt = "Báo cáo tổng hợp doanh thu - chi phí theo tháng của xưởng " + maXuong;
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
        string thang = context.Request.QueryString["thang"] + "";
        string xuong = context.Request.QueryString["xuong"];

        thang = thang.Length <= 1 ? "0" + thang : thang;

        string sql = string.Format(@"	 
            declare @tungay datetime = convert(datetime, N'01/{0}/{1} 00:00:00', 103)
            declare @denngay datetime = DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@tungay)+1,0))
            declare @thang int = {0};
            declare @nam int = {1};
            declare @thang_nam nvarchar(32) = N'{0}/{1}'
            declare @xuongId nvarchar(32) = N'{2}'

            declare @tbl table (tien decimal(18,2))
            insert into @tbl
            select nx.grandtotal as tien
            from c_nhapxuat (nolock) nx 
            where 
	            ngay_giaonhan between @tungay and @denngay
	            and nx.md_doitackinhdoanh_id = @xuongId
                and nx.md_trangthai_id = 'HIEULUC'
            
            declare @tblINV table (id varchar(32), tienhoahong decimal(18,2), ngay_motokhai datetime)
            insert into @tblINV
            select 
                civ.c_packinginvoice_id, 
                isnull(civ.hoahongdatra, 0),
                civ.ngay_motokhai
            from c_packinginvoice civ
            where 
                civ.ngay_motokhai between @tungay and @denngay
                and civ.md_trangthai_id = 'HIEULUC'              

            declare @tblHoaHong table (tien decimal(18,2), thang int)
            insert into @tblHoaHong
            select civ.tienhoahong, MONTH(civ.ngay_motokhai) as thang
            from @tblINV civ
            where
            (
                select count(1)
                from c_dongpklinv (nolock) cdiv
                where 
                    cdiv.nhacungungid = @xuongId
                    and cdiv.c_packinginvoice_id = civ.id
            ) > 0
            
            declare @tblPhieuChi table (
                chenhLechTyGiaDSBH decimal(18,2),
                dsoVTXuatChoBPCtyKhac decimal(18,2), 
                tienBHXHBHLDNghiViec decimal(18,2),
                dienNuocRacCtyHaca decimal(18,2), 
                nopTamUng decimal(18,2),
                tienPheLieu decimal(18,2), 
                VTBBPLTNK decimal(18,2),
                chiPhiXuatKhau decimal(18,2), 
                chiPhiHCNS decimal(18,2),
                luongNV decimal(18,2), 
                luongCN decimal(18,2),
                phiCDBHXHNV decimal(18,2), 
                phiCDBHXHCN decimal(18,2),
                thuongLeTetNV decimal(18,2), 
                thuongLeTetCN decimal(18,2),
                tamUngLuongCN decimal(18,2), 
                chiTienMatKhac decimal(18,2),
                trich20PheLieu decimal(18,2), 
                khieuNai decimal(18,2),
                cuocGuiMauKiemDinh decimal(18,2),
                
                GCchenhLechTyGiaDSBH nvarchar(MAX), 
                GCdsoVTXuatChoBPCtyKhac nvarchar(MAX), 
                GCtienBHXHBHLDNghiViec nvarchar(MAX),
                GCdienNuocRacCtyHaca nvarchar(MAX), 
                GCnopTamUng nvarchar(MAX),
                GCtienPheLieu nvarchar(MAX), 
                GCVTBBPLTNK nvarchar(MAX),
                GCchiPhiXuatKhau nvarchar(MAX), 
                GCchiPhiHCNS nvarchar(MAX),
                GCluongNV nvarchar(MAX), 
                GCluongCN nvarchar(MAX),
                GCphiCDBHXHNV nvarchar(MAX), 
                GCphiCDBHXHCN nvarchar(MAX),
                GCthuongLeTetNV nvarchar(MAX), 
                GCthuongLeTetCN nvarchar(MAX),
                GCtamUngLuongCN nvarchar(MAX), 
                GCchiTienMatKhac nvarchar(MAX),
                GCtrich20PheLieu nvarchar(MAX), 
                GCkhieuNai nvarchar(MAX),
                GCcuocGuiMauKiemDinh nvarchar(MAX)
            )
            insert into @tblPhieuChi
            select
                (case when dtc.loaiphieu2 = 'chenhLechTyGiaDSBH' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'dsoVTXuatChoBPCtyKhac' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'tienBHXHBHLDNghiViec' then dtc.sotien else 0 end), 
                (case when dtc.loaiphieu2 = 'dienNuocRacCtyHaca' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'nopTamUng' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'tienPheLieu' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'VTBBPLTNK' then dtc.sotien else 0 end), 
                (case when dtc.loaiphieu2 = 'chiPhiXuatKhau' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'chiPhiHCNS' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'luongNV' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'luongCN' then dtc.sotien else 0 end), 
                (case when dtc.loaiphieu2 = 'phiCDBHXHNV' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'phiCDBHXHCN' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'thuongLeTetNV' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'thuongLeTetCN' then dtc.sotien else 0 end), 
                (case when dtc.loaiphieu2 = 'tamUngLuongCN' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'chiTienMatKhac' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'trich20PheLieu' then dtc.sotien else 0 end),
                (case when dtc.loaiphieu2 = 'KhieuNai' then dtc.sotien else 0 end), 
                (case when dtc.loaiphieu2 = 'cuocGuiMauKiemDinh' then dtc.sotien else 0 end),
                
                (case when dtc.loaiphieu2 = 'chenhLechTyGiaDSBH' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'dsoVTXuatChoBPCtyKhac' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'tienBHXHBHLDNghiViec' then dtc.diengiai else '' end), 
                (case when dtc.loaiphieu2 = 'dienNuocRacCtyHaca' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'nopTamUng' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'tienPheLieu' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'VTBBPLTNK' then dtc.diengiai else '' end), 
                (case when dtc.loaiphieu2 = 'chiPhiXuatKhau' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'chiPhiHCNS' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'luongNV' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'luongCN' then dtc.diengiai else '' end), 
                (case when dtc.loaiphieu2 = 'phiCDBHXHNV' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'phiCDBHXHCN' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'thuongLeTetNV' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'thuongLeTetCN' then dtc.diengiai else '' end), 
                (case when dtc.loaiphieu2 = 'tamUngLuongCN' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'chiTienMatKhac' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'trich20PheLieu' then dtc.diengiai else '' end),
                (case when dtc.loaiphieu2 = 'KhieuNai' then dtc.diengiai else '' end), 
                (case when dtc.loaiphieu2 = 'cuocGuiMauKiemDinh' then dtc.diengiai else '' end)
            from c_chitietthuchi dtc
				left join c_thuchi tc on tc.c_thuchi_id = dtc.c_thuchi_id
            where 
                tc.ngay_giaonop between @tungay and @denngay
                and dtc.dtkd_id_ThuChi = @xuongId
				--and tc.md_trangthai_id = 'HIEULUC'

            declare @tblNhapLieu table (
                thang int, 
                noDauNam decimal(18,2)
            )
            insert into @tblNhapLieu
            select 
                tc.thang,
                tc.noDauNam
            from rpt_tonghopdoanhthuchiphi tc
            where 
                tc.nam = @nam
                and tc.thang = 1
                and tc.xuongId = @xuongId

            select 
                @thang_nam as thang_nam,
                (select sum(noDauNam) from @tblNhapLieu where thang = 1) as noDauNam,
	            
                (select sum(tien) from @tbl) as a1,
                '' as gc_a1,
                
                (select sum(chenhLechTyGiaDSBH) from @tblPhieuChi where chenhLechTyGiaDSBH != 0) as a1BS1,
                substring((select ', ' + GCchenhLechTyGiaDSBH from @tblPhieuChi where chenhLechTyGiaDSBH != 0 FOR XML PATH ('')),2,100000) as gc_a1BS1,

                (select sum(dsoVTXuatChoBPCtyKhac) from @tblPhieuChi where dsoVTXuatChoBPCtyKhac > 0) as a2,
                substring((select ', ' + GCdsoVTXuatChoBPCtyKhac from @tblPhieuChi where dsoVTXuatChoBPCtyKhac > 0 FOR XML PATH ('')),2,100000) as gc_a2,
				
				(select sum(tienBHXHBHLDNghiViec) from @tblPhieuChi where tienBHXHBHLDNghiViec > 0) as a2BS1,
                substring((select ', ' + GCtienBHXHBHLDNghiViec from @tblPhieuChi where tienBHXHBHLDNghiViec > 0 FOR XML PATH ('')),2,100000) as gc_a2BS1,
				
                (select sum(tienPheLieu) from @tblPhieuChi where tienPheLieu > 0) as a3,
                substring((select ', ' + GCtienPheLieu from @tblPhieuChi where tienPheLieu > 0 FOR XML PATH ('')),2,100000) as gc_a3,

                (select sum(nopTamUng) from @tblPhieuChi where nopTamUng > 0) as a4,
                substring((select ', ' + GCnopTamUng from @tblPhieuChi where nopTamUng > 0 FOR XML PATH ('')),2,100000) as gc_a4,

                (select sum(VTBBPLTNK) from @tblPhieuChi where VTBBPLTNK > 0) as b1,
                substring((select ', ' + GCVTBBPLTNK from @tblPhieuChi where VTBBPLTNK > 0 FOR XML PATH ('')),2,100000) as gc_b1,

                (select sum(chiPhiXuatKhau) from @tblPhieuChi where chiPhiXuatKhau > 0) as b2,
                substring((select ', ' + GCchiPhiXuatKhau from @tblPhieuChi where chiPhiXuatKhau > 0 FOR XML PATH ('')),2,100000) as gc_b2,

                (select sum(chiPhiHCNS) from @tblPhieuChi where chiPhiHCNS > 0) as b3,
                substring((select ', ' + GCchiPhiHCNS from @tblPhieuChi where chiPhiHCNS > 0 FOR XML PATH ('')),2,100000) as gc_b3,

                (select sum(luongNV) from @tblPhieuChi where luongNV > 0) as b4,
                substring((select ', ' + GCluongNV from @tblPhieuChi where luongNV > 0 FOR XML PATH ('')),2,100000) as gc_b4,

                (select sum(luongCN) from @tblPhieuChi where luongCN > 0) as b5,
                substring((select ', ' + GCluongCN from @tblPhieuChi where luongCN > 0 FOR XML PATH ('')),2,100000) as gc_b5,

                (select sum(phiCDBHXHNV) from @tblPhieuChi where phiCDBHXHNV > 0) as b6,
                substring((select ', ' + GCphiCDBHXHNV from @tblPhieuChi where phiCDBHXHNV > 0 FOR XML PATH ('')),2,100000) as gc_b6,

                (select sum(phiCDBHXHCN) from @tblPhieuChi where phiCDBHXHCN > 0) as b7,
                substring((select ', ' + GCphiCDBHXHCN from @tblPhieuChi where phiCDBHXHCN > 0 FOR XML PATH ('')),2,100000) as gc_b7,

                (select sum(thuongLeTetNV) from @tblPhieuChi where thuongLeTetNV > 0) as b8,
                substring((select ', ' + GCthuongLeTetNV from @tblPhieuChi where thuongLeTetNV > 0 FOR XML PATH ('')),2,100000) as gc_b8,

                (select sum(thuongLeTetCN) from @tblPhieuChi where thuongLeTetCN > 0) as b9,
                substring((select ', ' + GCthuongLeTetCN from @tblPhieuChi where thuongLeTetCN > 0 FOR XML PATH ('')),2,100000) as gc_b9,

                (select sum(chiTienMatKhac) from @tblPhieuChi where chiTienMatKhac > 0) as b10,
                substring((select ', ' + GCchiTienMatKhac from @tblPhieuChi where chiTienMatKhac > 0 FOR XML PATH ('')),2,100000) as gc_b10,

                (select sum(tien) from @tblHoaHong) as b11,
                '' as gc_b11,

                (select sum(KhieuNai) from @tblPhieuChi where KhieuNai > 0) as b12,
                substring((select ', ' + GCKhieuNai from @tblPhieuChi where KhieuNai > 0 FOR XML PATH ('')),2,100000) as gc_b12,

                (select sum(cuocGuiMauKiemDinh) from @tblPhieuChi where cuocGuiMauKiemDinh > 0) as b13,
                substring((select ', ' + GCcuocGuiMauKiemDinh from @tblPhieuChi where cuocGuiMauKiemDinh > 0 FOR XML PATH ('')),2,100000) as gc_b13,

                (select sum(tamUngLuongCN) from @tblPhieuChi where tamUngLuongCN > 0) as b14,
                substring((select ', ' + GCtamUngLuongCN from @tblPhieuChi where tamUngLuongCN > 0 FOR XML PATH ('')),2,100000) as gc_b14
		"
        , thang
        , nam
        , xuong
        );

        //throw new ArgumentNullException(sql);
        return sql;
    }
}