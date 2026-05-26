using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;

public partial class TestReadExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();

        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        //set A2
        s1.CreateRow(1).CreateCell(0).SetCellValue(-5);
        //set B2
        s1.GetRow(1).CreateCell(1).SetCellValue(1111);
        //set C2
        s1.GetRow(1).CreateCell(2).SetCellValue(7.623);
        //set A3
        s1.CreateRow(2).CreateCell(0).SetCellValue(2.2);

        //set A4=A2+A3
        s1.CreateRow(3).CreateCell(0).CellFormula = "A2+A3";
        //set D2=SUM(A2:C2);
        s1.GetRow(1).CreateCell(3).CellFormula = "SUM(A2:C2)";
        //set A5=cos(5)+sin(10)
        s1.CreateRow(4).CreateCell(0).CellFormula = "cos(5)+sin(10)";


        //create another sheet
        ISheet s2 = hssfworkbook.CreateSheet("Sheet2");
        //set cross-sheet reference
        s2.CreateRow(0).CreateCell(0).CellFormula = "Sheet1!A2+Sheet1!A3";


        String saveAsFileName = "text.xls";
        MemoryStream exportData = new MemoryStream();
        hssfworkbook.Write(exportData);

        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
        Response.Clear();
        Response.BinaryWrite(exportData.GetBuffer());
        Response.End();
    }
}
