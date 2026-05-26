using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

public partial class PrintControllers_InGiaSanPham_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String md_phienbangia_id = Request.QueryString["md_phienbangia_id"];
        String sql = "select pbg.ten_phienbangia, sp.ma_sanpham, gsp.gia, gsp.phi, gsp.mota, gsp.ma_donggoi from md_sanpham sp, md_giasanpham gsp, md_phienbangia pbg where sp.md_sanpham_id = gsp.md_sanpham_id AND pbg.md_phienbangia_id = gsp.md_phienbangia_id AND pbg.md_phienbangia_id = @md_phienbangia_id order by sp.ma_sanpham asc";
        DataTable dt = mdbc.GetData(sql, "@md_phienbangia_id", md_phienbangia_id);
        
        if (dt.Rows.Count != 0)
        {
            HSSFWorkbook hssfworkbook = this.CreateWorkBook(dt);
            String saveAsFileName = String.Format(dt.Rows[0]["ten_phienbangia"].ToString() +"-{0}.xls", DateTime.Now);
            this.SaveFile(hssfworkbook, saveAsFileName);
        }
        else
        {
            Response.Write("<h3>Phiên bản giá " + dt.Rows[0]["ten_phienbangia"].ToString() + " không có dữ liệu</h3>");
        }
    }

    public HSSFWorkbook CreateWorkBook(DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
		
		Excel_Format ex_fm = new Excel_Format(hssfworkbook); 
		ICellStyle cellBold2 = ex_fm.cell_decimal_2(10, false, true, "", "R", "T");
		
        IRow row1 = s1.CreateRow(0);

        row1.CreateCell(0).SetCellValue("Mã sản phẩm");
        row1.CreateCell(1).SetCellValue("Giá");
		row1.CreateCell(2).SetCellValue("Phí");
		row1.CreateCell(3).SetCellValue("Tổng Giá");
        row1.CreateCell(4).SetCellValue("Mã Khách Hàng");
        row1.CreateCell(5).SetCellValue("Mã Đóng Gói");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
			var row = i + 1;
            s1.CreateRow(row).CreateCell(0).SetCellValue(dt.Rows[i]["ma_sanpham"].ToString());
			
			var gia = double.Parse(dt.Rows[i]["gia"].ToString());
            s1.GetRow(row).CreateCell(1).SetCellValue(gia);
			s1.GetRow(row).GetCell(1).CellStyle = cellBold2;

            var colPhi = dt.Rows[i]["phi"] + "";
            if (string.IsNullOrWhiteSpace(colPhi))
                colPhi = "0";

            var phi = double.Parse(colPhi);
			s1.GetRow(row).CreateCell(2).SetCellValue(phi);
			s1.GetRow(row).GetCell(2).CellStyle = cellBold2;
			
			var tonggia = gia + phi;
			s1.GetRow(row).CreateCell(3).SetCellValue(tonggia);
			s1.GetRow(row).GetCell(3).CellStyle = cellBold2;

            s1.GetRow(row).CreateCell(4).SetCellValue(dt.Rows[i]["mota"].ToString());
            s1.GetRow(row).CreateCell(5).SetCellValue(dt.Rows[i]["ma_donggoi"].ToString());
        }

        return hssfworkbook;
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
