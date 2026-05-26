using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintControllers_InBaoCaoTongHopDoanhThu_rpt_KiemTraMaHangHoa : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;

    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        string nameTemp = "(NV) Kiểm tra mã hàng.xls";
        string nameRpt = "Kiểm tra mã hàng";
        string sql = CreateSql(context);

        inPDF = context.Request.QueryString["kieuin"];
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
        string tungay = context.Request.QueryString["startdate"];
        string denngay = context.Request.QueryString["enddate"];
        string khachhang = context.Request.QueryString["doitackinhdoanh_id"];
        string whereTNDN = "";
        string tungayDenNgay = "";
        if (!string.IsNullOrWhiteSpace(tungay) & !string.IsNullOrWhiteSpace(denngay))
        {
            whereTNDN = string.Format(" and inv.ngay_motokhai between convert(datetime, N'{0} 00:00:00', 103) and convert(datetime, N'{1} 23:59:59', 103)", tungay, denngay);
            tungayDenNgay = string.Format(@"Từ ngày {0} đến {1}", tungay, denngay);
        }
        else if(!string.IsNullOrWhiteSpace(tungay))
        {
            whereTNDN = string.Format(" and inv.ngay_motokhai >= convert(datetime, N'{0} 00:00:00', 103)", tungay);
            tungayDenNgay = string.Format(@"Từ ngày {0}", tungay);
        }
        else if (!string.IsNullOrWhiteSpace(denngay))
        {
            whereTNDN = string.Format(" and inv.ngay_motokhai <= convert(datetime, N'{0} 23:59:59', 103)", denngay);
            tungayDenNgay = string.Format(@"Đến ngày {0}", denngay);
        }

        if (!string.IsNullOrWhiteSpace(khachhang))
        {
            whereTNDN += string.Format(" and dh.md_doitackinhdoanh_id = N'{0}'", khachhang);
        }

        string sql = string.Format(@"
            select distinct 
	            sp.ma_sanpham,
	            dh.sochungtu as sopo,
	            ddh.soluong as sldh,
	            ddh.giafob + isnull(ddh.phi, 0) - isnull((
                    case 
                        when isnull(dh.phanbodiscount, 0) = 1
                        then (ddh.giafob + ddh.phi) * (case when isnull(dh.discount, 0) > 0 then isnull(dh.discount, 0) else isnull(ddh.discount, 0) end) / 100
                        else 0 
                    end
                ), 0) as giafob,
	            (case when inv.md_trangthai_id = 'SOANTHAO' then N'Soạn Thảo' else N'Hiệu lực' end) as trangthai,
	            inv.so_inv,
	            dinv.soluong as slinv,
	            dinv.gia as giainv,
	            inv.ngay_motokhai,
	            hhdq.mota as ttdq,
	            ddh.ngaytao,
                N'{1}' as tungaydenngay
            from c_dongdonhang ddh
	            left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id
	            left join md_sanpham sp on sp.md_sanpham_id = ddh.md_sanpham_id
	            left join c_dongpklinv dinv on dinv.c_donhang_id = ddh.c_donhang_id and dinv.md_sanpham_id = sp.md_sanpham_id
	            left join c_packinginvoice inv on inv.c_packinginvoice_id = dinv.c_packinginvoice_id
	            left join md_hanghoadocquyen hhdq on hhdq.md_sanpham_id = sp.md_sanpham_id and hhdq.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id
            where 
	            1=1
                {0}
            order by ddh.ngaytao desc
		"
        , whereTNDN
        , tungayDenNgay
        );

        //throw new ArgumentNullException(sql);
        return sql;
    }
}