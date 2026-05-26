using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        {
            String c_baogia_id = Request.QueryString["c_baogia_id"];
            String logoPath = System.IO.Path.Combine(Server.MapPath("~/images/"), "VINHGIA_logo_print.png");
            String imgPath = Public.imgProducts;
			String userName = "";
			//--
			nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(Context)));
			if(nv != null)
			{
				userName = nv.manv;
			}
			//--
            c_baogia bg = db.c_baogias.FirstOrDefault(p => p.c_baogia_id.Equals(c_baogia_id));
            HSSFWorkbook wb = null;
			if(bg != null) 
			{
				if (bg.isform.Value)
				{
					BaoGiaMauReport_pdf rptBaoGia = new BaoGiaMauReport_pdf(c_baogia_id, logoPath, imgPath);
					wb = rptBaoGia.CreateWBQuotation();
					//this.SaveFile(wb, "AncoQuotation.xls");
					
				}
				else
				{
					BaoGiaReport_pdf rptBaoGia = new BaoGiaReport_pdf(c_baogia_id, logoPath, imgPath);
					wb = rptBaoGia.CreateWBQuotation();
					//this.SaveFile(wb, "AncoQuotation.xls");
				}
			}
			else 
			{
				var rptBaoGia = new BaoGiaMauQRReport(c_baogia_id, logoPath, imgPath);
				wb = rptBaoGia.CreateWBQuotation();
			}
			string filename = Server.MapPath("ReportPdf/AncoQuotation_"+ userName + ".xls");
			filename = filename.Replace("PrintControllers\\InBaoGia\\","");
			FileStream file = new FileStream(filename, FileMode.Create);  
			wb.Write(file);  
			file.Close();
			
			Workbook workbook = new Workbook();  
			workbook.LoadFromFile(filename);  
			filename = filename.Replace("AncoQuotation_"+userName+".xls","AncoQuotation_"+userName+".pdf");
			workbook.SaveToFile(filename,FileFormat.PDF);
			
			Response.Write("<iframe id-'iframe' src='../../ReportPdf/AncoQuotation_"+userName+".pdf' style='height: 99%; width: 100%;'></iframe>");
        }
        //catch (Exception ex)
        {
            //Response.Write(ex.Message);
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


