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

public partial class PrintControllers_0BaoCaoKH_rpt_InBaoCaoDonHangDaXuat : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);

        string nameTemp = "(CT) Báo cáo đơn hàng đã xuất.xls";
        string nameRpt = "Báo cáo đơn hàng đã xuất";
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

        string trangthaixuathang = Request.QueryString["trangthaixuathang"];
        if (String.IsNullOrEmpty(trangthaixuathang))
        {
            trangthaixuathang = "null";
        }
        else
        {
            trangthaixuathang = string.Format("'{0}'", trangthaixuathang);
        }

        string dtkd_id = Request.QueryString["doitackinhdoanh_id"];
        if (String.IsNullOrEmpty(dtkd_id))
        {
            dtkd_id = "null";
        }
        else
        {
            dtkd_id = string.Format("'{0}'", dtkd_id);
        }

        string tieude = "BÁO CÁO ĐƠN HÀNG ĐÃ XUẤT";
        if (trangthaixuathang == "'XUATHET'")
            tieude = "BÁO CÁO ĐƠN HÀNG ĐÃ XUẤT HẾT";
        else if (trangthaixuathang == "'CHUAXUATHET'")
            tieude = "BÁO CÁO ĐƠN HÀNG CHƯA XUẤT HẾT";

        string sql = string.Format(@"	 
        declare @tungay datetime, @denngay datetime
        declare @table table (
            makh nvarchar(100),
            soPO nvarchar(100),
            mahh nvarchar(100),
            sl decimal(18,2),
            gia decimal(18,2),
            sl_daxuat decimal(18,2),
			sl_conlai decimal(18,2),
            ngay_motokhai nvarchar(100),
			soINV nvarchar(100),
            nguoilap nvarchar(Max),
            tungay datetime,
            denngay datetime,
            noiden nvarchar(Max)
        )

        set @tungay = (select convert(datetime,'{0}',103))
		set @denngay = (select convert(datetime,'{1}', 103))

        insert into @table
        exec [dbo].[rpt_donhangdaxuat] @tungay, @denngay, {2}, {3}

        select distinct
            A.makh,
            A.soPO,
            A.mahh,
            A.sl,
            A.gia,
            A.sl_daxuat,
			A.sl_conlai,
            A.ngay_motokhai,
			A.soINV,
            A.nguoilap,
	        N'{0}' as tungay,
	        N'{1}' as denngay,
            N'{4}' as tieude,
            A.noiden
        from @table A
                
		"
        , dateF.ToString("dd/MM/yyyy")
        , dateT.ToString("dd/MM/yyyy")
        , trangthaixuathang
        , dtkd_id
        , tieude
        );

        //throw new ArgumentNullException(sql);
        return sql;
    }
}
