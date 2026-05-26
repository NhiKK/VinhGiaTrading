using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using HSSFUtils;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.HSSF.Record.AutoFilter;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;
public partial class PrintControllers_NgungHoatDongDongGoi_Default : System.Web.UI.Page
{
    private class donggoiSP
    {
        public string ma_donggoi { get; set; }
        public int? hoatdong { get; set; }
    }
	private LinqDBDataContext db = new LinqDBDataContext();
	string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
	string filePath = "", filenameLC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpFileCollection files = Request.Files;
		if(files.Count > 0){
			filenameLC = files[0].FileName.Replace(".xlsx",".xls");
			filePath = Server.MapPath("../../VNN_Files/" + id + files[0].FileName);
			files[0].SaveAs(filePath);
			ReadExCel(Context);
		}
		else {
			Response.Write("Không tìm thấy tập tin.");
		}
    }
	
	public void ReadExCel(HttpContext context){
		//try {
			int j = filePath.LastIndexOf(".");
			if(filePath.Substring(j) != ".xls") {
				filePath = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(filePath);
			}
			Workbook wb = Workbook.Load(filePath);
			Worksheet ws = wb.Worksheets[0];
			this.NewFromCellCollection(context, ws.Cells);
		//}
		//catch (Exception ex){
			
			//Response.Write("Lỗi." + ex.Message);
		//}
	}
	
	
	public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
		int totalCount = cellCollection.Rows.Count;
		int rowErr = 0;
        var dgs = new List<donggoiSP>();
		for (int i = 1; i < totalCount; i++)
		{
			try
            {
                Row row = cellCollection.Rows[i];
				string ma_donggoi = row.GetCell(0).Value.ToString();
                int hoatdong = row.GetCell(1) == null ? 0 : int.Parse(row.GetCell(1).Value.ToString());
                dgs.Add(new donggoiSP() { ma_donggoi = ma_donggoi, hoatdong = hoatdong });
			}
			catch {
				rowErr++;
				if(rowErr <= 500)
					totalCount += 1;
			}
		}

        string msgFail = "", msgSuccess = "";
		foreach(var dg in dgs)
        {
            var dgsp = db.md_donggois.Where(s => s.ma_donggoi == dg.ma_donggoi).FirstOrDefault();
            if (dgsp == null)
                msgFail += string.Format("<div style='color:red'>Không tìm thấy mã đóng gói <b>{0}</b></div>", dg.ma_donggoi);
            else
            {
                dgsp.hoatdong = dg.hoatdong.GetValueOrDefault(0) == 1;
                msgSuccess += string.Format("<div style='color:blue'>Mã đóng gói <b>{0}</b> cập nhật hoạt động thành công</div>", dg.ma_donggoi);
            }
        }
        
        if(dgs.Count <= 0)
        {
            Response.Write("Không có dữ liệu.");
        }
        else
        {
            if (msgFail.Length > 0)
                Response.Write(msgFail);
            else
            {
                db.SubmitChanges();
                Response.Write(msgSuccess);
            }
        }
	}
}