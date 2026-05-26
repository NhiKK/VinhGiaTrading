using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;

public partial class ReportWizard_RptKhachHang_taoCatalogue8 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);
        ReportViewer1.LocalReport.EnableExternalImages = true;
        //Warning[] warnings;
        //string mimeType;
        //string encoding;
        //string extension;
        //string deviceInfo;
        //string[] streamids;
        //deviceInfo =
        //  "<DeviceInfo>" +
        //  "  <OutputFormat>PDF</OutputFormat>" +
        //  "  <PageSize>A4</PageSize>" +
        //  "  <PageWidth>8.5in</PageWidth>" +
        //  "  <PageHeight>11in</PageHeight>" +
        //  "  <MarginTop>0.25in</MarginTop>" +
        //  "  <MarginLeft>0.25in</MarginLeft>" +
        //  "  <MarginRight>0.25in</MarginRight>" +
        //  "  <MarginBottom>0.25in</MarginBottom>" +
        //  "</DeviceInfo>";
        
        //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
        //FileStream fs = new FileStream("C:\\Sample.PDF", FileMode.Create);
        //fs.Write(bytes, 0, bytes.Length);
        //fs.Close();
    }
}
