using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InTienHangChiTiet_Default : System.Web.UI.Page
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

        if (string.IsNullOrEmpty(cdxem))
        {
            var report = new rpt_tienhangdanhapchitiet();
            this.viewReport(report, sql);
        }
        else
        {
            string file = Server.MapPath("../../ReportsStorage/[KT] Tổng hợp tiền hàng đã nhập chi tiết.repx");
            var report = XtraReport.FromFile(file, true);
            this.viewReport(report, sql);
        }
        //Response.Write(sql);
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
        string cdxem = HttpContext.Current.Request.QueryString["cdxem"];
        string str = "";
        string dtkdWhere = string.IsNullOrEmpty(dtkd) ? 
            "" : 
                string.Format("AND cnx.md_doitackinhdoanh_id = N'{0}'", dtkd);

        if (string.IsNullOrEmpty(cdxem))
        {
            if (dtkd == null | dtkd == "")
            {
                str = String.Format(@"
                    DECLARE @startdate datetime = convert(datetime, '{0}', 103) 
                    DECLARE @enddate datetime = convert(datetime, '{1}', 103) 
                    DECLARE @table TABLE ( 
	                    c_dongnhapxuat_id nvarchar(32),
	                    so_inv nvarchar(50)
                    )

                    INSERT INTO @table
                    SELECT dnx.dongnhapxuat_ref, A.so_inv
                    FROM c_dongnhapxuat dnx (nolock)
                    inner join (
	                    SELECT cdiv5.c_dongnhapxuat_id,
		                       civ5.so_inv
	                    FROM c_dongpklinv cdiv5 (nolock),
		                     c_packinginvoice civ5 (nolock)
	                    WHERE civ5.c_packinginvoice_id = cdiv5.c_packinginvoice_id
	                      AND civ5.md_trangthai_id = 'HIEULUC'
	                      AND civ5.ngay_motokhai >= @startdate
	                      AND civ5.ngay_motokhai <= @enddate
                    ) A on dnx.c_dongnhapxuat_id = A.c_dongnhapxuat_id

                      SELECT nhan.*,
                        (SELECT convert(decimal,sum((cdnx5.slthuc_nhapxuat * cdnx5.dongia) - (cdnx5.slthuc_nhapxuat * cdnx5.dongia)*cnx5.discount/100))
                         FROM c_dongnhapxuat cdnx5,
                              c_nhapxuat cnx5,
                              c_dongdonhang cdgh5,
                              c_donhang dh5,
                              md_sanpham sp5
                         WHERE cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
                           AND cdnx5.md_sanpham_id = sp5.md_sanpham_id
                           AND cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
                           AND cdgh5.c_donhang_id = dh5.c_donhang_id
                           AND cnx5.md_loaichungtu_id = 'NK'
                           AND cdnx5.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)
	                    ) AS tongtiencc ,
                        (SELECT sum(cdnx5.dongia)
                         FROM c_dongnhapxuat cdnx5,
                              c_nhapxuat cnx5,
                              c_dongdonhang cdgh5,
                              c_donhang dh5,
                              md_sanpham sp5
                         WHERE cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
                           AND cdnx5.md_sanpham_id = sp5.md_sanpham_id
                           AND cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
                           AND cdgh5.c_donhang_id = dh5.c_donhang_id
                           AND cnx5.md_loaichungtu_id = 'NK'
                           AND cdnx5.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)
	                    ) AS tongdongiacc,
                        (SELECT sum(cdnx5.slthuc_nhapxuat)
                         FROM c_dongnhapxuat cdnx5,
                              c_nhapxuat cnx5,
                              c_dongdonhang cdgh5,
                              c_donhang dh5,
                              md_sanpham sp5
                         WHERE cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
                           AND cdnx5.md_sanpham_id = sp5.md_sanpham_id
                           AND cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
                           AND cdgh5.c_donhang_id = dh5.c_donhang_id
                           AND cnx5.md_loaichungtu_id = 'NK'
                           AND cdnx5.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)
	                    ) AS tongsoluongcc
                      FROM
                        (
		                    SELECT cnx.sophieu,
                                cnx.ngay_giaonhan,
                                sp.ma_sanpham,
                                cdnx.mota_tiengviet,
                                cdnx.slthuc_nhapxuat,
                                cdnx.dongia,
                                convert(decimal,(cdnx.slthuc_nhapxuat * cdnx.dongia) - (cdnx.slthuc_nhapxuat * cdnx.dongia)*cnx.discount/100) AS thanhtien,
				   
                                dtkd.ma_dtkd,
                                dh.sochungtu AS sopo,
                                cnx.phicong AS phicongPO,
                                cnx.phitru AS phitruPO,
                                @startdate AS tungay,
                                @enddate AS denngay,
                                GETDATE() AS today,
                                cnx.discount ,

                           (SELECT convert(decimal,sum((cdnx2.slthuc_nhapxuat * cdnx2.dongia) - (cdnx2.slthuc_nhapxuat * cdnx2.dongia)*cnx2.discount/100))
                            FROM c_dongnhapxuat cdnx2,
                                 c_nhapxuat cnx2,
                                 c_dongdonhang cdgh2,
                                 c_donhang dh2,
                                 md_sanpham sp2
                            WHERE cnx2.c_nhapxuat_id = cdnx2.c_nhapxuat_id
                              AND cdnx2.md_sanpham_id = sp2.md_sanpham_id
                              AND cdgh2.c_dongdonhang_id = cdnx2.c_dongdonhang_id
                              AND cdgh2.c_donhang_id = dh2.c_donhang_id
                              AND cnx2.md_loaichungtu_id = 'NK'
                              AND cnx2.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
                              AND cdnx2.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)
		                    ) AS tongtien ,

                           (SELECT sum(cdnx3.dongia)
                            FROM c_dongnhapxuat cdnx3,
                                 c_nhapxuat cnx3,
                                 c_dongdonhang cdgh3,
                                 c_donhang dh3,
                                 md_sanpham sp3
                            WHERE cnx3.c_nhapxuat_id = cdnx3.c_nhapxuat_id
                              AND cdnx3.md_sanpham_id = sp3.md_sanpham_id
                              AND cdgh3.c_dongdonhang_id = cdnx3.c_dongdonhang_id
                              AND cdgh3.c_donhang_id = dh3.c_donhang_id
                              AND cnx3.md_loaichungtu_id = 'NK'
                              AND cnx3.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
                              AND cdnx3.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)) AS tongdongia,  
                           (SELECT sum(cdnx4.slthuc_nhapxuat)
                            FROM c_dongnhapxuat cdnx4,
                                 c_nhapxuat cnx4,
                                 c_dongdonhang cdgh4,
                                 c_donhang dh4,
                                 md_sanpham sp4
                            WHERE cnx4.c_nhapxuat_id = cdnx4.c_nhapxuat_id
                              AND cdnx4.md_sanpham_id = sp4.md_sanpham_id
                              AND cdgh4.c_dongdonhang_id = cdnx4.c_dongdonhang_id
                              AND cdgh4.c_donhang_id = dh4.c_donhang_id
                              AND cnx4.md_loaichungtu_id = 'NK'
                              AND cnx4.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
                              AND cdnx4.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)) AS tongsoluong,
		                      tb.so_inv
                         FROM c_dongnhapxuat cdnx,
                              c_nhapxuat cnx,
                              c_dongdonhang cdgh,
                              c_donhang dh,
                              md_sanpham sp,
                              md_doitackinhdoanh dtkd,
		                      @table tb
                         WHERE cnx.c_nhapxuat_id = cdnx.c_nhapxuat_id
                           AND cdnx.md_sanpham_id = sp.md_sanpham_id
                           AND cdgh.c_dongdonhang_id = cdnx.c_dongdonhang_id
                           AND cdgh.c_donhang_id = dh.c_donhang_id
                           AND cnx.md_loaichungtu_id = 'NK'
                           AND cnx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
                           AND cdnx.c_dongnhapxuat_id = tb.c_dongnhapxuat_id) AS nhan
                    ORDER BY nhan.ma_dtkd,
                             nhan.sophieu,
                             nhan.ma_sanpham
                ", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), datenow.ToString("dd/MM/yyyy"));
            }
            else
            {
                str = String.Format(@"            
                    DECLARE @startdate datetime = convert(datetime, '{0}', 103) 
                    DECLARE @enddate datetime = convert(datetime, '{1}', 103) 
                    DECLARE @table TABLE ( 
	                    c_dongnhapxuat_id nvarchar(32),
	                    so_inv nvarchar(50)
                    )

                    INSERT INTO @table
                    SELECT dnx.dongnhapxuat_ref, A.so_inv
                    FROM c_dongnhapxuat dnx (nolock)
                    inner join (
	                    SELECT cdiv5.c_dongnhapxuat_id,
		                       civ5.so_inv
	                    FROM c_dongpklinv cdiv5 (nolock),
		                     c_packinginvoice civ5 (nolock)
	                    WHERE civ5.c_packinginvoice_id = cdiv5.c_packinginvoice_id
	                      AND civ5.md_trangthai_id = 'HIEULUC'
	                      AND civ5.ngay_motokhai >= @startdate
	                      AND civ5.ngay_motokhai <= @enddate
                    ) A on dnx.c_dongnhapxuat_id = A.c_dongnhapxuat_id

                      SELECT nhan.*,
                        (SELECT convert(decimal,sum((cdnx5.slthuc_nhapxuat * cdnx5.dongia) - (cdnx5.slthuc_nhapxuat * cdnx5.dongia)*cnx5.discount/100))
                         FROM c_dongnhapxuat cdnx5,
                              c_nhapxuat cnx5,
                              c_dongdonhang cdgh5,
                              c_donhang dh5,
                              md_sanpham sp5
                         WHERE cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
                           AND cdnx5.md_sanpham_id = sp5.md_sanpham_id
                           AND cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
                           AND cdgh5.c_donhang_id = dh5.c_donhang_id
                           AND cnx5.md_loaichungtu_id = 'NK'				 
                           AND cdnx5.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)
	                    ) AS tongtiencc ,

                        (SELECT sum(cdnx5.dongia)
                         FROM c_dongnhapxuat cdnx5,
                              c_nhapxuat cnx5,
                              c_dongdonhang cdgh5,
                              c_donhang dh5,
                              md_sanpham sp5
                         WHERE cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
                           AND cdnx5.md_sanpham_id = sp5.md_sanpham_id
                           AND cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
                           AND cdgh5.c_donhang_id = dh5.c_donhang_id
                           AND cnx5.md_loaichungtu_id = 'NK'
										 
                           AND cdnx5.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)
	                    ) AS tongdongiacc,

                        (SELECT sum(cdnx5.slthuc_nhapxuat)
                         FROM c_dongnhapxuat cdnx5,
                              c_nhapxuat cnx5,
                              c_dongdonhang cdgh5,
                              c_donhang dh5,
                              md_sanpham sp5
                         WHERE cnx5.c_nhapxuat_id = cdnx5.c_nhapxuat_id
                           AND cdnx5.md_sanpham_id = sp5.md_sanpham_id
                           AND cdgh5.c_dongdonhang_id = cdnx5.c_dongdonhang_id
                           AND cdgh5.c_donhang_id = dh5.c_donhang_id
                           AND cnx5.md_loaichungtu_id = 'NK'					 
                           AND cdnx5.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)
	                    ) AS tongsoluongcc
  
                      FROM
                        (
		                    SELECT cnx.sophieu,
                                cnx.ngay_giaonhan,
                                sp.ma_sanpham,
                                cdnx.mota_tiengviet,
                                cdnx.slthuc_nhapxuat,
                                cdnx.dongia,
                                convert(decimal,(cdnx.slthuc_nhapxuat * cdnx.dongia) - (cdnx.slthuc_nhapxuat * cdnx.dongia)*cnx.discount/100) AS thanhtien,
				   
                                dtkd.ma_dtkd,
                                dh.sochungtu AS sopo,
                                cnx.phicong AS phicongPO,
                                cnx.phitru AS phitruPO,
                                @startdate AS tungay,
                                @enddate AS denngay,
                                GETDATE() AS today,
                                cnx.discount ,

                           (SELECT convert(decimal,sum((cdnx2.slthuc_nhapxuat * cdnx2.dongia) - (cdnx2.slthuc_nhapxuat * cdnx2.dongia)*cnx2.discount/100))
                            FROM c_dongnhapxuat cdnx2,
                                 c_nhapxuat cnx2,
                                 c_dongdonhang cdgh2,
                                 c_donhang dh2,
                                 md_sanpham sp2
                            WHERE cnx2.c_nhapxuat_id = cdnx2.c_nhapxuat_id
                              AND cdnx2.md_sanpham_id = sp2.md_sanpham_id
                              AND cdgh2.c_dongdonhang_id = cdnx2.c_dongdonhang_id
                              AND cdgh2.c_donhang_id = dh2.c_donhang_id
                              AND cnx2.md_loaichungtu_id = 'NK'
                              AND cnx2.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
                              AND cdnx2.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)
		                    ) AS tongtien ,

                           (SELECT sum(cdnx3.dongia)
                            FROM c_dongnhapxuat cdnx3,
                                 c_nhapxuat cnx3,
                                 c_dongdonhang cdgh3,
                                 c_donhang dh3,
                                 md_sanpham sp3
                            WHERE cnx3.c_nhapxuat_id = cdnx3.c_nhapxuat_id
                              AND cdnx3.md_sanpham_id = sp3.md_sanpham_id
                              AND cdgh3.c_dongdonhang_id = cdnx3.c_dongdonhang_id
                              AND cdgh3.c_donhang_id = dh3.c_donhang_id
                              AND cnx3.md_loaichungtu_id = 'NK'
                              AND cnx3.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
                              AND cdnx3.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)) AS tongdongia ,
                           (SELECT sum(cdnx4.slthuc_nhapxuat)
                            FROM c_dongnhapxuat cdnx4,
                                 c_nhapxuat cnx4,
                                 c_dongdonhang cdgh4,
                                 c_donhang dh4,
                                 md_sanpham sp4
                            WHERE cnx4.c_nhapxuat_id = cdnx4.c_nhapxuat_id
                              AND cdnx4.md_sanpham_id = sp4.md_sanpham_id
                              AND cdgh4.c_dongdonhang_id = cdnx4.c_dongdonhang_id
                              AND cdgh4.c_donhang_id = dh4.c_donhang_id
                              AND cnx4.md_loaichungtu_id = 'NK'
                              AND cnx4.md_doitackinhdoanh_id = cnx.md_doitackinhdoanh_id
                              AND cdnx4.c_dongnhapxuat_id IN (SELECT c_dongnhapxuat_id FROM @table)) AS tongsoluong,
		                      tb.so_inv
                         FROM c_dongnhapxuat cdnx,
                              c_nhapxuat cnx,
                              c_dongdonhang cdgh,
                              c_donhang dh,
                              md_sanpham sp,
                              md_doitackinhdoanh dtkd,
		                      @table tb
                         WHERE cnx.c_nhapxuat_id = cdnx.c_nhapxuat_id
                           AND cdnx.md_sanpham_id = sp.md_sanpham_id
                           AND cdgh.c_dongdonhang_id = cdnx.c_dongdonhang_id
                           AND cdgh.c_donhang_id = dh.c_donhang_id
                           AND cnx.md_loaichungtu_id = 'NK'
                           AND cnx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
                           AND cnx.md_doitackinhdoanh_id = N'{2}'
                           AND cdnx.c_dongnhapxuat_id = tb.c_dongnhapxuat_id) AS nhan
                    ORDER BY nhan.ma_dtkd,
                             nhan.sophieu,
                             nhan.ma_sanpham
                    ", dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), dtkd, datenow.ToString("dd/MM/yyyy"));
            }
        }
        else if(cdxem == "KT")
        {
            str = string.Format(@"
                DECLARE @startdate datetime = convert(datetime, '{0}', 103) 
                DECLARE @enddate datetime = convert(datetime, '{1}', 103)
                declare @tbl table (
	                sophieu nvarchar(MAX),
	                ngay_giaonhan datetime,
	                ma_sanpham nvarchar(MAX),
	                mota_tiengviet nvarchar(MAX),
	                slthuc_nhapxuat decimal(18,4),
	                dongia decimal(18,2),
	                thanhtien decimal(18,2),
	                ma_dtkd nvarchar(MAX),
	                sopo nvarchar(MAX),
	                phicongPO numeric(18,2),
	                phitruPO numeric(18,2),
	                tungay datetime,
	                denngay datetime,
	                today datetime,
	                discount numeric(18,2),
	                giafob numeric(18,2),
                    discountPO numeric(18, 6),
                    thanhtienfob decimal(18,2),
	                so_inv nvarchar(MAX)
                )

                insert into @tbl
                SELECT cnx.sophieu,
                        cnx.ngay_giaonhan,
                        sp.ma_sanpham,
                        cdnx.mota_tiengviet,
                        cdnx.slthuc_nhapxuat,
                        cdnx.dongia,
                        convert(decimal,(cdnx.slthuc_nhapxuat * cdnx.dongia) - (cdnx.slthuc_nhapxuat * cdnx.dongia)*cnx.discount/100) AS thanhtien,
                        dtkd.ma_dtkd,
                        dh.sochungtu AS sopo,
                        cnx.phicong AS phicongPO,
                        cnx.phitru AS phitruPO,
                        @startdate AS tungay,
                        @enddate AS denngay,
                        GETDATE() AS today,
                        cnx.discount,
                        cdgh.giafob,
                        dh.discount as discountPO,
                        convert(decimal(18,2),(cdnx.slthuc_nhapxuat * cdgh.giafob) - (cdnx.slthuc_nhapxuat * cdgh.giafob)*cnx.discount/100) AS thanhtienfob,
		                (
			                SELECT
		                        (select top 1 so_inv from c_packinginvoice where c_packinginvoice_id = cdiv5.c_packinginvoice_id)
	                        FROM c_dongpklinv cdiv5 (nolock)
			                where
				                cdiv5.c_dongnhapxuat_id in (
					                select dxk.c_dongnhapxuat_id 
					                from c_dongnhapxuat dxk
					                where dxk.dongnhapxuat_ref = cdnx.c_dongnhapxuat_id
				                )
		                ) as so_inv
                FROM c_dongnhapxuat (nolock) cdnx,
                        c_nhapxuat (nolock) cnx,
                        c_dongdonhang (nolock) cdgh,
                        c_donhang (nolock) dh,
                        md_sanpham (nolock) sp,
                        md_doitackinhdoanh dtkd
                WHERE cnx.c_nhapxuat_id = cdnx.c_nhapxuat_id
                    AND cdnx.md_sanpham_id = sp.md_sanpham_id
                    AND cdgh.c_dongdonhang_id = cdnx.c_dongdonhang_id
                    AND cdgh.c_donhang_id = dh.c_donhang_id
                    AND cnx.md_loaichungtu_id = 'NK'
                    AND cnx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
                    AND cnx.ngay_giaonhan BETWEEN @startdate AND @enddate
                    {2}

                declare @tblCC table (
	                tongtiencc decimal(18,2),
	                tongdongiacc decimal(18,2),
	                tongsoluongcc decimal(18,4),
                    tongfobcc decimal(18,2),
                    tongtienfobcc decimal(18,2)
                )
                insert into @tblCC
                select 
                    sum(isnull(thanhtien, 0)), 
                    sum(isnull(dongia, 0)), 
                    SUM(isnull(slthuc_nhapxuat,0)), 
                    SUM(isnull(giafob,0)),
                    SUM(isnull(thanhtienfob,0)) 
                from @tbl

                declare @tbltheoNCC table (
	                ma_dtkd nvarchar(32),
	                tongtien decimal(18,2),
	                tongdongia decimal(18,2),
	                tongsoluong decimal(18,4),
                    tongfob decimal(18,2),
                    tongtienfob decimal(18,2)
                )
                insert into @tbltheoNCC
                select 
                    ma_dtkd, 
                    sum(isnull(thanhtien, 0)), 
                    sum(isnull(dongia, 0)), 
                    SUM(isnull(slthuc_nhapxuat,0)), 
                    SUM(isnull(giafob,0)),
                    SUM(isnull(thanhtienfob,0))
                from @tbl 
                group by ma_dtkd

                select a.*, 
                    (select b.tongtien from @tbltheoNCC b where b.ma_dtkd = a.ma_dtkd) as tongtien,
                    (select b.tongdongia from @tbltheoNCC b where b.ma_dtkd = a.ma_dtkd) as tongdongia,
                    (select b.tongsoluong from @tbltheoNCC b where b.ma_dtkd = a.ma_dtkd) as tongsoluong,
                    (select b.tongfob from @tbltheoNCC b where b.ma_dtkd = a.ma_dtkd) as tongfob,
                    (select b.tongtienfob from @tbltheoNCC b where b.ma_dtkd = a.ma_dtkd) as tongtienfob,
                    (select top 1 b.tongtiencc from @tblCC b) as tongtiencc,
                    (select top 1 b.tongdongiacc from @tblCC b) as tongdongiacc,
                    (select top 1 b.tongsoluongcc from @tblCC b) as tongsoluongcc,
                    (select top 1 b.tongfobcc from @tblCC b) as tongfobcc,
                    (select top 1 b.tongtienfobcc from @tblCC b) as tongtienfobcc
                from @tbl a
                ORDER BY a.ma_dtkd,
                            a.sophieu,
                            a.ma_sanpham
            ",
            dateFrom.ToString("dd/MM/yyyy"), 
            dateTo.ToString("dd/MM/yyyy"),
            dtkdWhere, 
            datenow.ToString("dd/MM/yyyy"));
        }
        return str;
    }
}