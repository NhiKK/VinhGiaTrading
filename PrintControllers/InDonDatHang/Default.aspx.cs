using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public partial class PrintControllers_InDonDatHang_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
    string type = "", c_danhsachdathang_id = "";
    List<AvariablePrj.lstImage> lstimage = new List<AvariablePrj.lstImage>();
    List<AvariablePrj.lstText> lstText = new List<AvariablePrj.lstText>();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            type = Request.QueryString["type"];
            c_danhsachdathang_id = Request.QueryString["c_danhsachdathang_id"];
            String logoPath = System.IO.Path.Combine(Server.MapPath("~/images/"), "VINHGIA_logo_print.png");
            String imgPath = Server.MapPath("~/images/products/fullsize/");
            DanhSachDatHangReport rptDSDH = new DanhSachDatHangReport(c_danhsachdathang_id, logoPath, imgPath);
			rptDSDH.guiNCC = Request.QueryString["DSDHG"] == "False";
            rptDSDH.TypePrint = type;
            lstimage = rptDSDH.Lstimage;
            lstText = rptDSDH.LstText;
            HSSFWorkbook wb = rptDSDH.CreateWBPI();

            this.SaveFile(wb, "Danh-Sach-Dat-Hang-" + DateTime.Now.ToString() + ".xls", rptDSDH.ConfigExcel);
        }
        catch (Exception ex)
        {
            Response.Write(ex + "");
        }
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName, ExportExcel config)
    {
        if (type != "pdf")
        {

            hsswb = config.setImageForExcel(hsswb, lstimage);
            MemoryStream exportData = new MemoryStream();
            hsswb.Write(exportData);
            hsswb.Clear();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
            Response.Clear();
            Response.BinaryWrite(exportData.GetBuffer());
            Response.End();
        }
        else 
        {
            var fileName = "DSDH_" + c_danhsachdathang_id + "_" + Guid.NewGuid().ToString();
            string url = Server.MapPath(string.Format(@"ReportPdf/{0}.xls", fileName));
            url = url.Replace("PrintControllers\\InDonDatHang\\", "");

            var xfile = new FileStream(url, FileMode.Create, System.IO.FileAccess.ReadWrite);
            hsswb.Write(xfile);
            xfile.Close();
            xfile.Dispose();

            if (type == "pdf")
            {
                var urlPDF = url.Replace(fileName + ".xls", fileName + ".pdf");

                var result = OfficeToPDF.ExcelConverter.Convert(url, urlPDF, null, lstimage, lstText);
                File.Delete(url);

                Response.Write(result);
                if(result.Length <= 0)
                    Response.Redirect(string.Format("../../ViewPDFPublic/index.aspx?urlpdf=/ReportPdf/{0}.pdf&zoomprint=0.999&zoom=page-width&remove=true", fileName));
            }
        }
    }
}
