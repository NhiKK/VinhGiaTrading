using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;

public partial class PrintControllers_InDonDatHang_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String c_danhsachdathang_id = Request.QueryString["c_danhsachdathang_id"];
            String logoPath = System.IO.Path.Combine(Server.MapPath("~/images/"), "VINHGIA_logo_print.png");
            String imgPath = Server.MapPath("~/images/products/fullsize/");
            DanhSachDatHangReport rptDSDH = new DanhSachDatHangReport(c_danhsachdathang_id, logoPath, imgPath);
            HSSFWorkbook wb = rptDSDH.CreateWBPI();
            this.SaveFile(wb, "Danh-Sach-Dat-Hang-" + DateTime.Now.ToString() + ".xls");
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
