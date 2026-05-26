using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;
using System.Data;

public partial class PrintControllers_InBaoGiaMau_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String c_baogia_id = Request.QueryString["c_baogia_id"];
            String logoPath = System.IO.Path.Combine(Server.MapPath("~/images/"), "VINHGIA_logo_print.png");
            String imgPath = Server.MapPath("~/images/products/fullsize/");
            BaoGiaMauReport rptBaoGia = new BaoGiaMauReport(c_baogia_id, logoPath, imgPath);
            HSSFWorkbook wb = rptBaoGia.CreateWBQuotation();
            this.SaveFile(wb, "Bao-Gia-Mau-" + DateTime.Now.ToString() + ".xls");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName)
    {
        MemoryStream exportData = new MemoryStream();
        hsswb.Write(exportData);

        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
        Response.Clear();
        Response.BinaryWrite(exportData.GetBuffer());
        Response.End();
    }
}
