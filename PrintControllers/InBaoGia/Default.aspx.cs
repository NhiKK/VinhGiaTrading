using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraReports.UI;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;

using System;
using Spire.Xls;
using System.Drawing;
using System.Drawing.Imaging;


public partial class PrintControllers_InBaoGia_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
    public string type = "", c_baogia_id = "";
    public List<AvariablePrj.lstImage> lstImage = new List<AvariablePrj.lstImage>();
    protected void Page_Load(object sender, EventArgs e)
    {
        type = Request.QueryString["type"];
        //try
        {
            c_baogia_id = Request.QueryString["c_baogia_id"];
            String logoPath = System.IO.Path.Combine(Server.MapPath("~/images/"), "VINHGIA_logo_print.png");
            String imgPath = Public.imgProducts;

            var bg = db.c_baogias.FirstOrDefault(p => p.c_baogia_id.Equals(c_baogia_id));
            HSSFWorkbook wb = null;

			var rptBaoGia = new BaoGiaReport(c_baogia_id, logoPath, imgPath);
            rptBaoGia.TypeReport = type;
            rptBaoGia.LaBaoGiaMau = bg == null ? false : bg.isform.GetValueOrDefault(false);
            rptBaoGia.LaBaoGiaMauQR = bg == null ? true : false;
            wb = rptBaoGia.CreateWBQuotation();
            lstImage = rptBaoGia.Lstimage;
            this.SaveFile(wb, bg == null ? "VinhgiaQuotationQR_R.xls" : (bg.isform.GetValueOrDefault(false) ? "VinhgiaQuotation_R.xls" : "VinhgiaQuotation.xls"));
        }
        //catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName)
    {

        var fileName = "DSDH_" + c_baogia_id + "_" + Guid.NewGuid().ToString();
        string url = Server.MapPath(string.Format(@"ReportPdf/{0}.xls", fileName));
        url = url.Replace("PrintControllers\\InBaoGia\\", "");

        var xfile = new FileStream(url, FileMode.Create, System.IO.FileAccess.ReadWrite);
        hsswb.Write(xfile);
        xfile.Close();
        xfile.Dispose();

        var excv = new OfficeToPDF.ExcelConverter();

        if (type == "pdf")
        {
            var urlPDF = url.Replace(fileName + ".xls", fileName + ".pdf");

            //var result = OfficeToPDF.ExcelConverter.Convert(url, urlPDF, null, lstImage, new List<AvariablePrj.lstText>());

            excv.lstImage = lstImage;
            excv.lstText = null;
            var result = excv.ConvertInterop(url, urlPDF, null);
            try
            {
                File.Delete(url);
            }
            catch { }

            Response.Write(result);
            if (result.Length <= 0)
                Response.Redirect(string.Format("../../ViewPDFPublic/index.aspx?urlpdf=/ReportPdf/{0}.pdf&zoomprint=0.999&zoom=page-width&remove=true", fileName));
        }
        else
        {
            var urlXls = url.Replace(fileName + ".xls", fileName + "1.xls");
            foreach (var img in lstImage)
                img.column -= 1;
            excv.lstImage = lstImage;
            excv.lstText = null;
            var result = excv.ConvertInterop(url, urlXls, null);
            try
            {
                File.Delete(url);
            }
            catch { }
            Response.Write(result);
            if (result.Length <= 0)
            {
                var data = "";
                data += "oper=downloadFile";
                data += string.Format("&name={0}", saveAsFileName);
                data += string.Format("&path=ReportPdf/{0}1.xls", fileName);
                data += string.Format("&remove=true");
                Response.Redirect(string.Format("../../Controllers/API_System.ashx?{0}", data));
            }
        }
    }
}




