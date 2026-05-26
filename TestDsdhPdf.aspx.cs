using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestDsdhPdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String piId = "fb4e05f5143e3ae6d6be102cbc0af3ed";// Request.QueryString["c_danhsachdathang_id"];
            //String manv = UserUtils.getUser(Context);
            String fileName = "DanhSachDatHang-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + ".pdf";
            String path = Server.MapPath("~/FileSendMail/");
            String logoPath = Server.MapPath("~/images/VINHGIA_logo_print.png");
            String productImagePath = Server.MapPath("~/images/products/fullsize/");

            DanhSachDatHangPdf rptPI = new DanhSachDatHangPdf(piId, logoPath, productImagePath);
            rptPI.CreatePdfPI(path + fileName, Server.MapPath("~/Fonts/TIMES.TTF"));
            Response.Write("Thành công!");
    }


}
