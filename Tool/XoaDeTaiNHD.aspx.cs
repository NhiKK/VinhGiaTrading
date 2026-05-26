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

public partial class Tool_XoaDeTaiNHD : System.Web.UI.Page
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
                            arrID += "'" + row.GetCell(0).Value.ToString() + row.GetCell(1).Value.ToString() + "',";
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
        else if (!string.IsNullOrEmpty(inputCL) & !string.IsNullOrEmpty(inputDtai))
        {
            arrID = string.Format("'{0}{1}',", inputCL, inputDtai);
        }

        if (arrID != "")
        {
            arrID = arrID.Remove(arrID.Length - 1);
            arrID = "and (dt.code_cl + dt.code_dt) in (" + arrID + ")";
        }

        string sql = string.Format(@" 
           SELECT 
            dt.md_detai_id, 
            dt.trangthai, 
            dt.code_cl, 
            dt.code_dt, 
            dt.tv_ngan, 
            dt.ta_ngan,
			dt.trangthai,
            dt.mota
            FROM md_detai dt
            WHERE 1=1
            {0} 
        ", arrID);

		if (type == "1")
		{
			sql = sql.Replace("1=1","1=1 and trangthai = 'NHD'");
			DataTable dt = mdbc.GetData(sql);
			if (dt.Rows.Count != 0)
			{
				this.CreateWorkBookPO(dt, "", "");
			}
			else {
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
					string idDT = dt.Rows[i]["md_detai_id"].ToString();
					md_detai dtai = db.md_detais.Where(s => s.md_detai_id == idDT).FirstOrDefault();
					if (dtai == null)
					{
						msg += string.Format("<div style='color: red'>Không tìm thấy đề tài.</div>");
					}
					else
					{
						try
						{
							string msgDT = "";

							#region Hủy đề tài
							if (typepost == "Huy")
							{
								try
								{
									foreach (md_mausac ms in db.md_mausacs.Where(s => s.md_detai_id == dtai.md_detai_id & s.trangthai != "NHD"))
									{
										msgDT += string.Format("<div style='color: red'>{0}-{1} (CL-DT) chưa ngưng hoạt động màu {2}.</div>", dtai.code_cl, dtai.code_dt, ms.code_mau);
									}

									if (msgDT == "")
									{
										foreach (md_sanpham sp in db.md_sanphams.Where(s =>
											s.ma_sanpham.Substring(0, 2) == dtai.code_cl &
											s.ma_sanpham.Substring(6, 2) == dtai.code_dt))
										{
											sp.trangthai = "NHD";
											sp.ngayngunghd = DateTime.Now;
										}
										db.SubmitChanges();
										msgDT += string.Format("<div style='color: blue'>{0}-{1} (CL-DT) đã ngưng hoạt động các sản phẩm liên quan.</div>", dtai.code_cl, dtai.code_dt);
									}
								}
								catch (Exception ex1)
								{
									msgDT += string.Format("<div style='color: red'>{0}-{1} (CL-DT) ngưng hoạt động các sản phẩm liên quan bị lỗi {2}.</div>", dtai.code_cl, dtai.code_dt, ex1.Message);
								}


								if (!msgDT.Contains("'color: red'"))
								{
									dtai.trangthai = "NHD";
									db.SubmitChanges();
									msgDT += string.Format("<div style='color: blue'>{0}-{1} (CL-DT) đã chuyển sang ngưng hoạt động.</div>", dtai.code_cl, dtai.code_dt);
								}

								msg += string.Format("<div style='border: 1px solid #CCC; padding: 5px; margin-bottom: 5px'>{0}</div>", msgDT);
							}
							#endregion

							#region Xóa đề tài
							else if (typepost == "Xoa")
							{
								foreach (md_mausac ms in db.md_mausacs.Where(s => s.md_detai_id == dtai.md_detai_id))
								{
									msgDT += string.Format("<div style='color: red'>{0}-{1} (CL-DT) còn màu {2}.</div>", dtai.code_cl, dtai.code_dt, ms.code_mau);
								}

								int count = db.md_sanphams.Where(s =>
									s.ma_sanpham.Substring(0, 2) == dtai.code_cl &
									s.ma_sanpham.Substring(6, 2) == dtai.code_dt).Count();

								if (count > 0)
								{
									msgDT += string.Format("<div style='color: red'>{0}-{1} (CL-DT) còn sản phẩm.</div>", dtai.code_cl, dtai.code_dt);
									msgDT += string.Format(@"<input type=""button"" onclick=""exportSPTT('{0}','{1}','')"" value='Chiết xuất sản phẩm'/>", dtai.code_cl, dtai.code_dt);
								}

								if (!msgDT.Contains("'color: red'"))
								{
									db.md_detais.DeleteOnSubmit(dtai);
									db.SubmitChanges();
									msgDT += string.Format("<div style='color: blue'>{0}-{1} (CL-DT) đã được xóa.</div>", dtai.code_cl, dtai.code_dt);
								}

								msg += string.Format("<div style='border: 1px solid #CCC; padding: 5px; margin-bottom: 5px'>{0}</div>", msgDT);
							}
							#endregion
						}
						catch (Exception ex)
						{
							msg += string.Format("<div style='color: red'>{0}-{1} (CL-DT) bị lỗi: {2}.</div>", dtai.code_cl, dtai.code_dt, ex.Message);
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
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 5);
        int row = export.SetHeaderAnco(hssfsheet, "DANH SÁCH ĐỀ TÀI NGƯNG HOẠT ĐỘNG", tungay, denngay, false);

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
        String saveAsFileName = String.Format("DSDeTaiNHD-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
