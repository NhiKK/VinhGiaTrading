using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InTienHangChiTietXuat_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        DateTime dateFrom = DateTime.ParseExact(Request.QueryString["startdate"], "MM/dd/yyyy", null);
        DateTime dateTo = DateTime.ParseExact(Request.QueryString["enddate"], "MM/dd/yyyy", null);
        DateTime datenow = DateTime.Now;
        String md_doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];

        rpt_tienhangdaxuatchitiet report = new rpt_tienhangdaxuatchitiet();
        String sql = this.CreateSql(dateFrom, dateTo, datenow, md_doitackinhdoanh_id);
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

    public String CreateSql(DateTime dateFrom, DateTime dateTo, DateTime datenow, string dtkd)
    {
        string wheresql = "";
        //if (dtkd.Length > 0)
        //{
        //    wheresql = " and nhan.md_doitackinhdoanh_id = '" + dtkd + "'";
        //}
        String str="";
        if (dtkd == null | dtkd == "")
        {
            str = String.Format(@"
	select nhan.*,
	(select convert(decimal,sum((cdnx5.slthuc_nhapxuat * cdnx5.dongia) - (cdnx5.slthuc_nhapxuat * cdnx5.dongia)*isnull(cnx5.discount,0)/100)) 
	from c_dongnhapxuat cdnx5, c_nhapxuat cnx5, c_dongdonhang cdgh5, c_donhang dh5, md_sanpham sp5
	where cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
			and cdnx5.md_sanpham_id = sp5.md_sanpham_id
			and cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
			and cdgh5.c_donhang_id = dh5.c_donhang_id
			and cnx5.md_loaichungtu_id = 'XK'
			and cdnx5.c_dongnhapxuat_id in ( 			
				select cdiv5.c_dongnhapxuat_id
				from c_dongpklinv cdiv5, c_packinginvoice civ5
				where civ5.c_packinginvoice_id = cdiv5.c_packinginvoice_id
				and civ5.md_trangthai_id = 'HIEULUC'
				and civ5.ngay_motokhai >= convert(date,'{0}',103)
				and civ5.ngay_motokhai <= convert(date,'{1}',103)
			)
	) as tongtiencc
	,
	(select sum(cdnx5.dongia) 
	from c_dongnhapxuat cdnx5, c_nhapxuat cnx5, c_dongdonhang cdgh5, c_donhang dh5, md_sanpham sp5
	where cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
			and cdnx5.md_sanpham_id = sp5.md_sanpham_id
			and cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
			and cdgh5.c_donhang_id = dh5.c_donhang_id
			and cnx5.md_loaichungtu_id = 'XK'
			and cdnx5.c_dongnhapxuat_id in ( 			
				select cdiv5.c_dongnhapxuat_id
				from c_dongpklinv cdiv5, c_packinginvoice civ5
				where civ5.c_packinginvoice_id = cdiv5.c_packinginvoice_id
					and civ5.md_trangthai_id = 'HIEULUC'
					and civ5.ngay_motokhai >= convert(date,'{0}',103)
					and civ5.ngay_motokhai <= convert(date,'{1}',103)
			)
	) as tongdongiacc
	,
	(select sum(cdnx5.slthuc_nhapxuat) 
	from c_dongnhapxuat cdnx5, c_nhapxuat cnx5, c_dongdonhang cdgh5, c_donhang dh5, md_sanpham sp5
	where cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
			and cdnx5.md_sanpham_id = sp5.md_sanpham_id
			and cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
			and cdgh5.c_donhang_id = dh5.c_donhang_id
			and cnx5.md_loaichungtu_id = 'XK'
			and cdnx5.c_dongnhapxuat_id in (
				select cdiv5.c_dongnhapxuat_id
				from c_dongpklinv cdiv5, c_packinginvoice civ5
				where civ5.c_packinginvoice_id = cdiv5.c_packinginvoice_id
					and civ5.md_trangthai_id = 'HIEULUC'
					and civ5.ngay_motokhai >= convert(date,'{0}',103)
					and civ5.ngay_motokhai <= convert(date,'{1}',103)
				)
	) as tongsoluongcc
	from
	(
		select cnx.sophieu, cnx.ngay_giaonhan, sp.ma_sanpham, cdnx.mota_tiengviet,
		cdnx.slthuc_nhapxuat, cdnx.dongia,
		convert(decimal,(cdnx.slthuc_nhapxuat * cdnx.dongia) - (cdnx.slthuc_nhapxuat * cdnx.dongia)*isnull(cnx.discount,0)/100)
		as thanhtien, dtkd.ma_dtkd, dh.sochungtu as sopo,
		(select isnull(sum(isnull(phi,0)),0) phi from c_phidonhang pdh where pdh.phitang = 1 and pdh.c_donhang_id = dh.c_donhang_id) as phicongPO,
		(select isnull(sum(isnull(phi,0)),0) phi from c_phidonhang pdh where pdh.phitang = 0 and pdh.c_donhang_id = dh.c_donhang_id) as phitruPO,
		convert(date,'{0}',103) as tungay, convert(date,'{1}',103) as denngay,
		convert(date,'{2}',103) as today, cnx.discount,
		(select convert(decimal,sum((cdnx2.slthuc_nhapxuat * cdnx2.dongia) - (cdnx2.slthuc_nhapxuat * cdnx2.dongia)*isnull(cnx2.discount,0)/100)) 
		from c_dongnhapxuat cdnx2, c_nhapxuat cnx2, c_dongdonhang cdgh2, c_donhang dh2, md_sanpham sp2
		where cnx2.c_nhapxuat_id = cdnx2.c_nhapxuat_id
			and cdnx2.md_sanpham_id = sp2.md_sanpham_id
			and cdgh2.c_dongdonhang_id = cdnx2.c_dongdonhang_id
			and cdgh2.c_donhang_id = dh2.c_donhang_id
			and cnx2.md_loaichungtu_id = 'XK'
			and cnx2.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
			and cdnx2.c_dongnhapxuat_id in ( 			
				select cdiv2.c_dongnhapxuat_id
				from c_dongpklinv cdiv2, c_packinginvoice civ2
				where civ2.c_packinginvoice_id = cdiv2.c_packinginvoice_id
					and civ2.md_trangthai_id = 'HIEULUC'
					and civ2.ngay_motokhai >= convert(date,'{0}',103)
					and civ2.ngay_motokhai <= convert(date,'{1}',103)	
			)
		) as tongtien,
		(select sum(cdnx3.dongia) 
		from c_dongnhapxuat cdnx3, c_nhapxuat cnx3, c_dongdonhang cdgh3, c_donhang dh3, md_sanpham sp3
		where cnx3.c_nhapxuat_id = cdnx3.c_nhapxuat_id
			and cdnx3.md_sanpham_id = sp3.md_sanpham_id
			and cdgh3.c_dongdonhang_id = cdnx3.c_dongdonhang_id
			and cdgh3.c_donhang_id = dh3.c_donhang_id
			and cnx3.md_loaichungtu_id = 'XK'
			and cnx3.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
			and cdnx3.c_dongnhapxuat_id in ( 			
				select cdiv3.c_dongnhapxuat_id
				from c_dongpklinv cdiv3, c_packinginvoice civ3
				where civ3.c_packinginvoice_id = cdiv3.c_packinginvoice_id
					and civ3.md_trangthai_id = 'HIEULUC'
					and civ3.ngay_motokhai >= convert(date,'{0}',103)
					and civ3.ngay_motokhai <= convert(date,'{1}',103)
			)
		) as tongdongia,
		(select sum(cdnx4.slthuc_nhapxuat) 
		from c_dongnhapxuat cdnx4, c_nhapxuat cnx4, c_dongdonhang cdgh4, c_donhang dh4, md_sanpham sp4
		where cnx4.c_nhapxuat_id = cdnx4.c_nhapxuat_id
			and cdnx4.md_sanpham_id = sp4.md_sanpham_id
			and cdgh4.c_dongdonhang_id = cdnx4.c_dongdonhang_id
			and cdgh4.c_donhang_id = dh4.c_donhang_id
			and cnx4.md_loaichungtu_id = 'XK'
			and cnx4.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
			and cdnx4.c_dongnhapxuat_id in ( 			
				select cdiv4.c_dongnhapxuat_id
				from c_dongpklinv cdiv4, c_packinginvoice civ4
				where civ4.c_packinginvoice_id = cdiv4.c_packinginvoice_id
					and civ4.md_trangthai_id = 'HIEULUC'
					and civ4.ngay_motokhai >= convert(date,'{0}',103)
					and civ4.ngay_motokhai <= convert(date,'{1}',103)
			)
		) as tongsoluong,
		InV.so_inv
		from c_dongnhapxuat cdnx
		inner join c_nhapxuat cnx on cnx.c_nhapxuat_id = cdnx.c_nhapxuat_id
		inner join c_dongdonhang cdgh on cdgh.c_dongdonhang_id = cdnx.c_dongdonhang_id
		inner join c_donhang dh on cdgh.c_donhang_id = dh.c_donhang_id
		inner join md_doitackinhdoanh dtkd on cnx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
		inner join md_sanpham sp on cdnx.md_sanpham_id = sp.md_sanpham_id
		inner join (
			select cdiv.c_dongnhapxuat_id, civ.so_inv 
			from c_dongpklinv cdiv, c_packinginvoice civ
			where civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
			and civ.md_trangthai_id = 'HIEULUC'
			and civ.ngay_motokhai >= convert(date,'01/05/2014',103)
			and civ.ngay_motokhai <= convert(date,'31/05/2014',103)
		)InV on cdnx.c_dongnhapxuat_id = InV.c_dongnhapxuat_id
		where cnx.md_loaichungtu_id = 'XK'
) as nhan
order by nhan.ma_dtkd,nhan.sophieu, nhan.ma_sanpham
        
        ", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), datenow.ToString("dd/MM/yyyy"));
        }
        else
        {
            str = String.Format(@"
	select nhan.*,
	(select convert(decimal,sum((cdnx5.slthuc_nhapxuat * cdnx5.dongia) - (cdnx5.slthuc_nhapxuat * cdnx5.dongia)*isnull(cnx5.discount,0)/100)) 
	from c_dongnhapxuat cdnx5, c_nhapxuat cnx5, c_dongdonhang cdgh5, c_donhang dh5, md_sanpham sp5
	where cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
			and cdnx5.md_sanpham_id = sp5.md_sanpham_id
			and cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
			and cdgh5.c_donhang_id = dh5.c_donhang_id
			and cnx5.md_loaichungtu_id = 'XK'
			and cnx5.md_doitackinhdoanh_id = '{3}'
			and cdnx5.c_dongnhapxuat_id in ( 			
				select cdiv5.c_dongnhapxuat_id
				from c_dongpklinv cdiv5, c_packinginvoice civ5
				where civ5.c_packinginvoice_id = cdiv5.c_packinginvoice_id
				and civ5.md_trangthai_id = 'HIEULUC'
				and civ5.ngay_motokhai >= convert(date,'{0}',103)
				and civ5.ngay_motokhai <= convert(date,'{1}',103)
			)
	) as tongtiencc
	,
	(select sum(cdnx5.dongia) 
	from c_dongnhapxuat cdnx5, c_nhapxuat cnx5, c_dongdonhang cdgh5, c_donhang dh5, md_sanpham sp5
	where cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
			and cdnx5.md_sanpham_id = sp5.md_sanpham_id
			and cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
			and cdgh5.c_donhang_id = dh5.c_donhang_id
			and cnx5.md_loaichungtu_id = 'XK'
			and cnx5.md_doitackinhdoanh_id = '{3}'
			and cdnx5.c_dongnhapxuat_id in ( 			
				select cdiv5.c_dongnhapxuat_id
				from c_dongpklinv cdiv5, c_packinginvoice civ5
				where civ5.c_packinginvoice_id = cdiv5.c_packinginvoice_id
					and civ5.md_trangthai_id = 'HIEULUC'
					and civ5.ngay_motokhai >= convert(date,'{0}',103)
					and civ5.ngay_motokhai <= convert(date,'{1}',103)
			)
	) as tongdongiacc
	,
	(select sum(cdnx5.slthuc_nhapxuat) 
	from c_dongnhapxuat cdnx5, c_nhapxuat cnx5, c_dongdonhang cdgh5, c_donhang dh5, md_sanpham sp5
	where cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
			and cdnx5.md_sanpham_id = sp5.md_sanpham_id
			and cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
			and cdgh5.c_donhang_id = dh5.c_donhang_id
			and cnx5.md_loaichungtu_id = 'XK'
			and cnx5.md_doitackinhdoanh_id = '{3}'
			and cdnx5.c_dongnhapxuat_id in (
				select cdiv5.c_dongnhapxuat_id
				from c_dongpklinv cdiv5, c_packinginvoice civ5
				where civ5.c_packinginvoice_id = cdiv5.c_packinginvoice_id
					and civ5.md_trangthai_id = 'HIEULUC'
					and civ5.ngay_motokhai >= convert(date,'{0}',103)
					and civ5.ngay_motokhai <= convert(date,'{1}',103)
				)
	) as tongsoluongcc
	from
	(
		select cnx.sophieu, cnx.ngay_giaonhan, sp.ma_sanpham, cdnx.mota_tiengviet,
		cdnx.slthuc_nhapxuat, cdnx.dongia,
		convert(decimal,(cdnx.slthuc_nhapxuat * cdnx.dongia) - (cdnx.slthuc_nhapxuat * cdnx.dongia)*isnull(cnx.discount,0)/100)
		as thanhtien, dtkd.ma_dtkd, dh.sochungtu as sopo,
		(select isnull(sum(isnull(phi,0)),0) phi from c_phidonhang pdh where pdh.phitang = 1 and pdh.c_donhang_id = dh.c_donhang_id) as phicongPO,
		(select isnull(sum(isnull(phi,0)),0) phi from c_phidonhang pdh where pdh.phitang = 0 and pdh.c_donhang_id = dh.c_donhang_id) as phitruPO,
		convert(date,'{0}',103) as tungay, convert(date,'{1}',103) as denngay,
		convert(date,'{2}',103) as today, cnx.discount,
		(select convert(decimal,sum((cdnx2.slthuc_nhapxuat * cdnx2.dongia) - (cdnx2.slthuc_nhapxuat * cdnx2.dongia)*isnull(cnx2.discount,0)/100)) 
		from c_dongnhapxuat cdnx2, c_nhapxuat cnx2, c_dongdonhang cdgh2, c_donhang dh2, md_sanpham sp2
		where cnx2.c_nhapxuat_id = cdnx2.c_nhapxuat_id
			and cdnx2.md_sanpham_id = sp2.md_sanpham_id
			and cdgh2.c_dongdonhang_id = cdnx2.c_dongdonhang_id
			and cdgh2.c_donhang_id = dh2.c_donhang_id
			and cnx2.md_loaichungtu_id = 'XK'
			and cnx2.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
			and cdnx2.c_dongnhapxuat_id in ( 			
				select cdiv2.c_dongnhapxuat_id
				from c_dongpklinv cdiv2, c_packinginvoice civ2
				where civ2.c_packinginvoice_id = cdiv2.c_packinginvoice_id
					and civ2.md_trangthai_id = 'HIEULUC'
					and civ2.ngay_motokhai >= convert(date,'{0}',103)
					and civ2.ngay_motokhai <= convert(date,'{1}',103)	
			)
		) as tongtien,
		(select sum(cdnx3.dongia) 
		from c_dongnhapxuat cdnx3, c_nhapxuat cnx3, c_dongdonhang cdgh3, c_donhang dh3, md_sanpham sp3
		where cnx3.c_nhapxuat_id = cdnx3.c_nhapxuat_id
			and cdnx3.md_sanpham_id = sp3.md_sanpham_id
			and cdgh3.c_dongdonhang_id = cdnx3.c_dongdonhang_id
			and cdgh3.c_donhang_id = dh3.c_donhang_id
			and cnx3.md_loaichungtu_id = 'XK'
			and cnx3.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
			and cdnx3.c_dongnhapxuat_id in ( 			
				select cdiv3.c_dongnhapxuat_id
				from c_dongpklinv cdiv3, c_packinginvoice civ3
				where civ3.c_packinginvoice_id = cdiv3.c_packinginvoice_id
					and civ3.md_trangthai_id = 'HIEULUC'
					and civ3.ngay_motokhai >= convert(date,'{0}',103)
					and civ3.ngay_motokhai <= convert(date,'{1}',103)
			)
		) as tongdongia,
		(select sum(cdnx4.slthuc_nhapxuat) 
		from c_dongnhapxuat cdnx4, c_nhapxuat cnx4, c_dongdonhang cdgh4, c_donhang dh4, md_sanpham sp4
		where cnx4.c_nhapxuat_id = cdnx4.c_nhapxuat_id
			and cdnx4.md_sanpham_id = sp4.md_sanpham_id
			and cdgh4.c_dongdonhang_id = cdnx4.c_dongdonhang_id
			and cdgh4.c_donhang_id = dh4.c_donhang_id
			and cnx4.md_loaichungtu_id = 'XK'
			and cnx4.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
			and cdnx4.c_dongnhapxuat_id in ( 			
				select cdiv4.c_dongnhapxuat_id
				from c_dongpklinv cdiv4, c_packinginvoice civ4
				where civ4.c_packinginvoice_id = cdiv4.c_packinginvoice_id
					and civ4.md_trangthai_id = 'HIEULUC'
					and civ4.ngay_motokhai >= convert(date,'{0}',103)
					and civ4.ngay_motokhai <= convert(date,'{1}',103)
			)
		) as tongsoluong
		from c_dongnhapxuat cdnx
		inner join c_nhapxuat cnx on cnx.c_nhapxuat_id = cdnx.c_nhapxuat_id
		inner join c_dongdonhang cdgh on cdgh.c_dongdonhang_id = cdnx.c_dongdonhang_id
		inner join c_donhang dh on cdgh.c_donhang_id = dh.c_donhang_id
		inner join md_doitackinhdoanh dtkd on cnx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
		inner join md_sanpham sp on cdnx.md_sanpham_id = sp.md_sanpham_id
		inner join (
			select cdiv.c_dongnhapxuat_id, civ.so_inv 
			from c_dongpklinv cdiv, c_packinginvoice civ
			where civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
			and civ.md_trangthai_id = 'HIEULUC'
			and civ.ngay_motokhai >= convert(date,'01/05/2014',103)
			and civ.ngay_motokhai <= convert(date,'31/05/2014',103)
		)InV on cdnx.c_dongnhapxuat_id = InV.c_dongnhapxuat_id
		where cnx.md_loaichungtu_id = 'XK'
		and cnx.md_doitackinhdoanh_id = '{3}'
) as nhan
order by nhan.ma_dtkd,nhan.sophieu, nhan.ma_sanpham
        ", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), datenow.ToString("dd/MM/yyyy"), dtkd);
        }
            return str;
    }
}