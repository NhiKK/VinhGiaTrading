using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.IO;
using NPOI.HSSF.UserModel;

public partial class PrintControllers_InBL_Default : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var context = HttpContext.Current;
            sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
            string nameTemp = "(CT) BL.xls";
            string nameRpt = "BL";

            string sql = CreateSql(context);

            inPDF = "2";
            var task = new System.Threading.Tasks.Task(() =>
            {
                viewReport(sql);
            });

            var compineF = Request.QueryString["compineF"];
            nameRpt = string.IsNullOrWhiteSpace(compineF) ? nameRpt + "-" + DateTime.Now.ToString("ddMMyy") : nameRpt;
            PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, string.IsNullOrWhiteSpace(compineF), !string.IsNullOrWhiteSpace(compineF));
        }
        catch (Exception ex)
        {
            Response.Write(ex + "");
        }
    }

    public void viewReport(String SqlQuery)
    {

    }

    public string CreateSql(HttpContext context)
    {
        String c_packinglist_id = context.Request.QueryString["pklId"];
        String md_doitackinhdoanh_id = Request.QueryString["md_doitackinhdoanh_id"];
        String hangcua = Request.QueryString["hangcua"];
        String noidungNCC1 = "", noidungNCC2 = "", noidungNCC3 = "", noidungNCC = "", noidungDetail = "";
        if (hangcua == "VG")
        {
            noidungNCC1 = "VINH GIA COMPANY LIMITED";
            noidungNCC2 = "Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam";
            noidungNCC3 = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            noidungNCC = "VINH GIA COMPANY LIMITED \nNorthern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam\nTel: (84-235) 3567393\nFax: (84-235) 3567494";
            noidungDetail = "Đơn vị bán hàng (Seller): CÔNG TY TNHH VINH GIA\nMã số thuế (Tax code):  4000462410\nĐịa chỉ (Address)::Khu công nghiệp Bắc Chu Lai, Xã Núi Thành, Thành phố Đà Nẵng";
        }
        else
        {
            noidungNCC1 = "HOANG HUY HUNG COMPANY LIMITED";
            noidungNCC2 = "Tam Hiep 2 Urban residential area, Nui Thanh Commune, Danang City, Viet Nam";
            noidungNCC3 = "Tel: (84-235) 3567393   Fax: (84-235) 3567494";
            noidungNCC = "HOANG HUY HUNG COMPANY LIMITED \nTam Hiep 2 Urban residential area,  Nui Thanh Commune, Danang City, Viet Nam\nTel: (84-235) 3567998";
            noidungDetail = "Đơn vị bán hàng (Seller): CÔNG TY TNHH HOÀNG HUY HƯNG\nMã số thuế(Tax code): 4001125746\nĐịa chỉ (Address): Khu dân cư Đô thị Tam Hiệp II, Xã Núi Thành, Thành phố Đà Nẵng, Việt Nam";
        }

        String selDH = @"select distinct dh.sochungtu from c_dongpklinv dpkl, c_donhang dh where dpkl.c_donhang_id = dh.c_donhang_id AND dpkl.c_packinginvoice_id = @c_packinginvoice_id";
        DataTable dtDonHang = mdbc.GetData(selDH, "@c_packinginvoice_id", c_packinglist_id);
        String dsDonHang = "";
        foreach (DataRow item in dtDonHang.Rows)
        {
            dsDonHang += String.Format(", {0}", item[0].ToString());
        }

        dsDonHang = dsDonHang.Substring(2);

        String select = string.Format(@"
            select top 1
                *, N'{0}' as dsdonhang, N'{0}' as dsdonhang2
                 , N'{4}' as noidungNCC1 , N'{5}' as noidungNCC2 , N'{6}' as noidungNCC3
                 , N'{7}' as noidungNCC
                 , N'{8}' as noidungDetail
            from 
                dbo.f_taobanginbl('{1}', '{2}', '{3}')",
            dsDonHang,
            c_packinglist_id,
            md_doitackinhdoanh_id,
            hangcua,
            noidungNCC1,
            noidungNCC2,
            noidungNCC3,
            noidungNCC,
            noidungDetail
            );

        return select;
    }
}