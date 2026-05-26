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

public partial class PrintControllers_0BaoCaoKH_rpt_InBaoCaoDonHangDaNhan : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);

        string nameTemp = "(CT) Báo cáo đơn hàng đã nhận.xls";
        string nameRpt = "Báo cáo đơn hàng đã nhận";
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

        string trangthai = Request.QueryString["trangthai"];
        if (String.IsNullOrEmpty(trangthai))
        {
            trangthai = "null";
        }
        else
        {
            trangthai = string.Format("'{0}'", trangthai);
        }

        string dtkd_id = Request.QueryString["dtkd_id"];
        if (String.IsNullOrEmpty(dtkd_id))
        {
            dtkd_id = "null";
        }
        else
        {
            dtkd_id = string.Format("'{0}'", dtkd_id);
        }

        string sql = string.Format(@"	 
            declare @tungay datetime, @denngay datetime 
	 
            declare @table table (
            makh nvarchar(100),
            po nvarchar(100),
            ngay_dsdh nvarchar(100),
            shipmenttime nvarchar(100),
            mahh nvarchar(100),
            sl int,
            giachuan decimal(18,2),
            gianhap decimal(18,2),
            nguoilap nvarchar(100),
            hdlh nvarchar(Max),
            hdlhkhac nvarchar(Max),
            tungay datetime,
            denngay datetime,
            ma_sanpham_khach nvarchar(Max),
            motaDG nvarchar(Max),
            cpdgvc decimal(18,2),
            mota_tienganh nvarchar(Max)

        )

        set @tungay = (select convert(datetime,'{0}',103))
		set @denngay = (select convert(datetime,'{1}', 103))

        insert into @table
        exec [dbo].[rpt_donhangdanhan] @tungay, @denngay, {2}, {3}

        select
            A.makh,
            A.po,
            A.ngay_dsdh,
            A.shipmenttime,
            A.mahh,
            A.sl,
            A.giachuan,
            A.gianhap,
            A.nguoilap,
            A.hdlh,
            A.hdlhkhac,
	        '{0}' as tungay,
	        '{1}' as denngay,
            A.ma_sanpham_khach,
            A.motaDG,
            A.cpdgvc,
            A.mota_tienganh
        from @table A
        order by A.makh, A.po, A.mahh
                
		"
        , dateF.ToString("dd/MM/yyyy")
        , dateT.ToString("dd/MM/yyyy")
        , trangthai
        , dtkd_id
        );

        //throw new ArgumentNullException(sql);
        return sql;
    }
}
