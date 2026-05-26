using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;


public partial class PrintControllers_InThongTinSanPhamReport_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        String md_sanpham_id = Request.QueryString["md_sanpham_id"];
        string md_tinhtranghanghoa_id = Request.QueryString["md_tinhtranghanghoa_id"];
        String typedg = Request.QueryString["typedg"];
        String trangthai = Request.QueryString["trangthai"];
        if (trangthai != null & trangthai != "")
        {
            trangthai = " and sp.trangthai = '"+ trangthai +"'";
        }
        else
        {
            trangthai = " and sp.trangthai = 'DHD'";
        }

        if (md_tinhtranghanghoa_id != null & md_tinhtranghanghoa_id != "")
        {
            md_tinhtranghanghoa_id = " and sp.md_tinhtranghanghoa_id = '" + md_tinhtranghanghoa_id + "'";
        }
        else
        {
            md_tinhtranghanghoa_id = " and 1 = 1";
        }

        if (typedg == "true") {
			typedg = "and dgsp.macdinh <> 1";
		}
		else {
			typedg = "and dgsp.macdinh = 1";
		}
        String sql =string.Format(@"select 
						(case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) + '.jpg' else SUBSTRING(sp.ma_sanpham,0,12) + '.jpg' end) as pic,
	                    sp.ma_sanpham, sp.mota_tiengviet, sp.mota_tienganh, 
	                    (select dvtsp.ten_dvt from md_donvitinhsanpham dvtsp where dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id) as dvt,
	                    l_cm as l, w_cm as w, h_cm as h, sp.trongluong, sp.dientich, sp.ghichu, sp.mota,
                        (select hehang + '(' + nhom + ')' from md_nhomnangluc nnl where nnl.md_nhomnangluc_id = sp.md_nhomnangluc_id) as chungloai,
                        dg.ten_donggoi,
	                    (
		                    select top 1 gsp.gia 
		                    from md_banggia bg, md_phienbangia pbg, md_giasanpham gsp
		                    where bg.md_banggia_id = pbg.md_banggia_id
		                    AND pbg.md_phienbangia_id = gsp.md_phienbangia_id
		                    AND sp.md_sanpham_id = gsp.md_sanpham_id
		                    AND bg.banggiaban = 1
		                    AND pbg.md_trangthai_id = 'HIEULUC'
		                    AND substring(bg.ten_banggia,0,5) = 'FOB-'
		                    order by pbg.ngay_hieuluc desc
	                    ) as giafob,
	                    (
		                    select top 1 gsp.gia 
		                    from md_banggia bg, md_phienbangia pbg, md_giasanpham gsp
		                    where bg.md_banggia_id = pbg.md_banggia_id
		                    AND pbg.md_phienbangia_id = gsp.md_phienbangia_id
		                    AND sp.md_sanpham_id = gsp.md_sanpham_id
		                    AND bg.banggiaban = 0
		                    AND pbg.md_trangthai_id = 'HIEULUC'
		                    order by pbg.ngay_hieuluc desc
	                    ) as giamua, 
	                    dg.sl_inner, (select dvt.ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner) as dvt_inner,
	                    dg.sl_outer, (select dvt.ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer) as dvt_outer,
	                    dg.l1, dg.w1, dg.h1, dg.l2_mix as l2, dg.w2_mix as w2, dg.h2_mix as h2,
	                    dtkd.ma_dtkd as nhacungung, cb.ten_cangbien as cangbien,isnull(dg.soluonggoi_ctn_20,0) as con20, isnull(dg.soluonggoi_ctn,0) as con40, isnull(dg.soluonggoi_ctn_40hc,0) as con40hc
                    from 
	                    md_sanpham sp
						left join md_doitackinhdoanh dtkd on sp.nhacungung = dtkd.md_doitackinhdoanh_id
						left join md_donggoisanpham dgsp on dgsp.md_sanpham_id = sp.md_sanpham_id
						left join md_donggoi dg on dg.md_donggoi_id = dgsp.md_donggoi_id
						left join md_cangbien cb on sp.md_cangbien_id = cb.md_cangbien_id
                    where sp.ma_sanpham like N'{0}' {1} {2} {3}
                    order by ma_sanpham asc", md_sanpham_id, trangthai, typedg, md_tinhtranghanghoa_id);
        rptLocThongTinSanPham report = new rptLocThongTinSanPham();
        this.viewReport(report, sql);
		//Response.Write(typedg);
    }

    public void viewReport(XtraReport report, String SqlQuery)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        da.SelectCommand.CommandTimeout = 50000;
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }
}