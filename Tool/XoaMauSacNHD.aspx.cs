using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Data.Linq;
using System.Linq;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public partial class Tool_XoaMauSacNHD : System.Web.UI.Page
{
    private LinqDBDataContext db = new LinqDBDataContext();
    string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    string filePath = "", filenameLC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        string typepost = Request.Form["typepost"];
        string inputCL = Request.Form["chungloaiKTX"];
        string inputDtai = Request.Form["detaiKTX"];
        string inputMS = Request.Form["mausacKTX"];
        HttpFileCollection files = Request.Files;
        string arrID = "";
        if (files.Count > 0)
        {
            filenameLC = files[0].FileName.Replace(".xlsx", ".xls");
            if (filenameLC != "")
            {
                filePath = Server.MapPath("../VNN_Files/" + id + files[0].FileName);
                files[0].SaveAs(filePath);
                try
                {
                    int j = filePath.LastIndexOf(".");
                    if (filePath.Substring(j) != ".xls")
                    {
                        filePath = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(filePath);
                    }
                    Workbook wb = Workbook.Load(filePath);
                    Worksheet ws = wb.Worksheets[0];
                    var cellCollection = ws.Cells;
                    int totalCount = cellCollection.Rows.Count;
                    for (int i = 0; i < totalCount; i++)
                    {
                        try
                        {
                            Row row = cellCollection.Rows[i];
                            arrID += "'" + row.GetCell(0).Value.ToString() + row.GetCell(1).Value.ToString() + row.GetCell(2).Value.ToString() + "',";
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    arrID = "'Error',";
                }
            }
        }
        else if (!string.IsNullOrEmpty(inputCL) & !string.IsNullOrEmpty(inputDtai) & !string.IsNullOrEmpty(inputMS))
        {
            arrID = string.Format("'{0}{1}{2}',",inputCL, inputDtai, inputMS);
        }

        if (arrID != "")
        {
            arrID = arrID.Remove(arrID.Length - 1);
            arrID = "and (ms.code_cl + ms.code_dt + ms.code_mau) in (" + arrID + ")";
        }

        string sql = string.Format(@" 
           select 
            ms.md_mausac_id, 
            ms.code_mau, 
            ms.code_cl,
            ms.code_dt,
            ms.tv_ngan, 
            ms.ta_ngan,
			ms.trangthai,
            ms.mota
            FROM md_mausac ms
            WHERE 1=1
            {0}
            order by ms.code_cl, ms.code_dt, ms.code_mau
        ", arrID);

        
		if (type == "1")
		{
			sql = sql.Replace("1=1","1=1 and trangthai = 'NHD'");
			DataTable dt = mdbc.GetData(sql);
			if (dt.Rows.Count != 0)
			{
				this.CreateWorkBookPO(dt, "", "");
			}
			else
			{
				Response.Write("<h3>Không có dữ liệu</h3>");
			}
		}
		else if (type == "3")
		{
			DataTable dt = mdbc.GetData(sql);
			if (dt.Rows.Count != 0)
			{
				string msg = "";
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string idMS = dt.Rows[i]["md_mausac_id"].ToString();
					md_mausac ms = db.md_mausacs.Where(s => s.md_mausac_id == idMS).FirstOrDefault();
					if (ms == null)
					{
						msg += string.Format("<div style='color: red'>Không tìm thấy màu sắc.</div>");
					}
					else
					{
						try
						{
							string msgDT = "";

							#region Hủy màu sắc
							if (typepost == "Huy")
							{
								try
								{
									foreach (md_sanpham sp in db.md_sanphams.Where(s =>
										s.ma_sanpham.Substring(0, 2) == ms.code_cl &
										s.ma_sanpham.Substring(6, 2) == ms.code_dt &
										s.ma_sanpham.Substring(12, 2) == ms.code_mau))
									{
										sp.trangthai = "NHD";
										sp.ngayngunghd = DateTime.Now;
									}
									db.SubmitChanges();
									msgDT += string.Format("<div style='color: blue'>{0}-{1}-{2} (CL-DT-MS) đã ngưng hoạt động các sản phẩm liên quan.</div>", ms.code_cl, ms.code_dt, ms.code_mau);
								}
								catch (Exception ex1)
								{
									msgDT += string.Format("<div style='color: red'>{0}-{1}-{2} (CL-DT-MS) ngưng hoạt động các sản phẩm liên quan bị lỗi {3}.</div>", ms.code_cl, ms.code_dt, ms.code_mau, ex1.Message);
								}


								if (!msgDT.Contains("'color: red'"))
								{
									ms.trangthai = "NHD";
									db.SubmitChanges();
									msgDT += string.Format("<div style='color: blue'>{0}-{1}-{2} (CL-DT-MS) đã chuyển sang ngưng hoạt động.</div>", ms.code_cl, ms.code_dt, ms.code_mau);
								}

								msg += string.Format("<div style='border: 1px solid #CCC; padding: 5px; margin-bottom: 5px'>{0}</div>", msgDT);
							}
							#endregion

							#region Xóa màu sắc
							else if (typepost == "Xoa")
							{
								int count = db.md_sanphams.Where(s =>
									s.ma_sanpham.Substring(0, 2) == ms.code_cl &
									s.ma_sanpham.Substring(6, 2) == ms.code_dt &
									s.ma_sanpham.Substring(12, 2) == ms.code_mau).Count();
								
								if(count > 0)
								{
									msgDT += string.Format("<div style='color: red'>{0}-{1}-{2} (CL-DT-MS) còn sản phẩm.</div>", ms.code_cl, ms.code_dt, ms.code_mau);
									msgDT += string.Format(@"<input type=""button"" onclick=""exportSPTT('{0}','{1}','{2}')"" value='Chiết xuất sản phẩm'/>", ms.code_cl, ms.code_dt, ms.code_mau);
								}

								if (!msgDT.Contains("'color: red'"))
								{
									db.md_mausacs.DeleteOnSubmit(ms);
									db.SubmitChanges();
									msgDT += string.Format("<div style='color: blue'>{0}-{1}-{2} (CL-DT-MS) đã được xóa.</div>", ms.code_cl, ms.code_dt, ms.code_mau);
								}

								msg += string.Format("<div style='border: 1px solid #CCC; padding: 5px; margin-bottom: 5px'>{0}</div>", msgDT);
							}
							#endregion
						}
						catch (Exception ex)
						{
							msg += string.Format("<div style='color: red'>{0}-{1}-{2} (CL-DT-MS) bị lỗi: {3}.</div>", ms.code_cl, ms.code_dt, ms.code_mau, ex.Message);
						}
					}
				}
				Response.Write(msg);
			}
			else
			{
				Response.Write("<h3>Không có dữ liệu</h3>");
			}
		}

    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 6);
        int row = export.SetHeaderAnco(hssfsheet, "DANH SÁCH MÀU SẮC NGƯNG HOẠT ĐỘNG", tungay, denngay, false);

        #region Header Column
        int widthDF = 5000;
        List<ItemValue> lstHeader = new List<ItemValue>();
        var item = new ItemValue
        {
            item = "STT",
            value = "rowNum",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Chủng loại",
            value = "code_cl",
            witdh = 3500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Đề tài",
            value = "code_dt",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Màu sắc",
            value = "code_mau",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TV Ngắn",
            value = "tv_ngan",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TA Ngắn",
            value = "ta_ngan",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Ghi chú",
            value = "mota",
            witdh = widthDF
        };
        lstHeader.Add(item);

        IRow rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.HeightInPoints = 40;
        for (int j = 0; j < lstHeader.Count; j++)
        {
            rowColumnHeader.CreateCell(j).SetCellValue(lstHeader[j].item);
            rowColumnHeader.GetCell(j).CellStyle = export.borderWrap;
            s1.SetColumnWidth(j, lstHeader[j].witdh);
        }
        row++;
        #endregion

        #region Set Table
        int start_sum = row + 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { };
                string[] arrDate = new string[] { };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    try
                    {
                        cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                        cell.CellStyle = export.borderright;
                    }
                    catch { }
                }
                else if (lstHeader[j].value == "rowNum")
                {
                    cell.SetCellValue(i + 1);
                    cell.CellStyle = export.bordercenter;
                }
                else if (arrDate.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(DateTime.Parse(dt.Rows[i][lstHeader[j].value].ToString()).ToString("dd/MM/yyyy"));
                    cell.CellStyle = export.bordercenter;
                }
                else
                {
                    string str = dt.Rows[i][lstHeader[j].value].ToString();
                    float heightMAX = export.MeasureTextHeight(str, lstHeader[j].witdh);
                    if (heightMAX > height)
                    {
                        height = heightMAX;
                    }
                    cell.SetCellValue(str);
                    cell.CellStyle = export.border;
                }
            }
            row_t.HeightInPoints = height;
            row++;
        }
        int end_sum = row;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("DSMauSacNHD-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
