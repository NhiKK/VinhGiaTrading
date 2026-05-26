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

public partial class Tool_XoaChungLoaiNHD : System.Web.UI.Page
{
    private LinqDBDataContext db = new LinqDBDataContext();
    string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    string filePath = "", filenameLC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        string typepost = Request.Form["typepost"];
        string inputCL = Request.Form["chungloaiKTX"];
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
                            arrID += "'" + row.GetCell(0).Value.ToString() + "',";
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
        else if (!string.IsNullOrEmpty(inputCL))
        {
            arrID = string.Format("'{0}',", inputCL);
        }

        if (arrID != "")
        {
            arrID = arrID.Remove(arrID.Length - 1);
            arrID = "and cl.code_cl in (" + arrID + ")";
        }

        string sql = string.Format(@" 
           SELECT 
            cl.md_chungloai_id, 
            cl.code_cl,
            cl.tv_ngan, 
            cl.ta_ngan,
            nnl.nhom,
            cl.mota,
			cl.trangthai
            FROM md_chungloai cl
            left join md_nhomnangluc nnl on nnl.md_chungloai_id = cl.md_chungloai_id
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
					string idCL = dt.Rows[i]["md_chungloai_id"].ToString();
					md_chungloai cl = db.md_chungloais.Where(s => s.md_chungloai_id == idCL).FirstOrDefault();
					if (cl == null)
					{
						msg += string.Format("<div style='color: red'>Không tìm thấy chủng loại.</div>");
					}
					else
					{
						try
						{
							string msgDT = "";

							#region Hủy chủng loại
							if (typepost == "Huy")
							{
								try
								{
									foreach (md_mausac ms in db.md_mausacs.Where(s => s.md_chungloai_id == cl.md_chungloai_id & s.trangthai != "NHD"))
									{
										msgDT += string.Format("<div style='color: red'>Chủng loại {0} chưa ngưng hoạt động màu {1}.</div>", cl.code_cl, ms.code_mau);
									}

									foreach (md_detai dtai in db.md_detais.Where(s => s.md_chungloai_id == cl.md_chungloai_id & s.trangthai != "NHD"))
									{
										msgDT += string.Format("<div style='color: red'>Chủng loại {0} chưa ngưng hoạt động đề tài {1}.</div>", cl.code_cl, dtai.code_dt);
									}

									if (msgDT == "")
									{
										foreach (md_sanpham sp in db.md_sanphams.Where(s =>
											s.ma_sanpham.Substring(0, 2) == cl.code_cl))
										{
											sp.trangthai = "NHD";
											sp.ngayngunghd = DateTime.Now;
										}
										db.SubmitChanges();
										msgDT += string.Format("<div style='color: blue'>Chủng loại {0} đã ngưng hoạt động các sản phẩm liên quan.</div>", cl.code_cl);
									}
								}
								catch (Exception ex1)
								{
									msgDT += string.Format("<div style='color: red'>Chủng loại {0} ngưng hoạt động các sản phẩm liên quan bị lỗi {1}.</div>", cl.code_cl, ex1.Message);
								}


								if (!msgDT.Contains("'color: red'"))
								{
									cl.trangthai = "NHD";
									db.SubmitChanges();
									msgDT += string.Format("<div style='color: blue'>Chủng loại {0} đã chuyển sang ngưng hoạt động.</div>", cl.code_cl);
								}

								msg += string.Format("<div style='border: 1px solid #CCC; padding: 5px; margin-bottom: 5px'>{0}</div>", msgDT);
							}
							#endregion

							#region Xóa chủng loại
							else if (typepost == "Xoa")
							{
								foreach (md_mausac ms in db.md_mausacs.Where(s => s.md_chungloai_id == cl.md_chungloai_id))
								{
									msgDT += string.Format("<div style='color: red'>Chủng loại {0} còn màu {1}.</div>", cl.code_cl, ms.code_mau);
								}

								foreach (md_detai dtai in db.md_detais.Where(s => s.md_chungloai_id == cl.md_chungloai_id))
								{
									msgDT += string.Format("<div style='color: red'>Chủng loại {0} còn đề tài {1}.</div>", cl.code_cl, dtai.code_dt);
								}

								int count = db.md_sanphams.Where(s =>
									s.ma_sanpham.Substring(0, 2) == cl.code_cl).Count();

								if(count > 0)
								{
									msgDT += string.Format("<div style='color: red'>Chủng loại {0} còn sản phẩm.</div>", cl.code_cl);
									msgDT += string.Format(@"<input type=""button"" onclick=""exportSPTT('{0}','','')"" value='Chiết xuất sản phẩm'/>", cl.code_cl);
								}

								if (!msgDT.Contains("'color: red'"))
								{
									foreach(md_nhomnangluc nnl in db.md_nhomnanglucs.Where(s=>s.md_chungloai_id == cl.md_chungloai_id)) {
										db.md_nhomnanglucs.DeleteOnSubmit(nnl);
										msgDT += string.Format("<div style='color: blue'>Chủng loại {0} đã xóa NNL {1}.</div>", cl.code_cl, nnl.nhom);
									}
									db.md_chungloais.DeleteOnSubmit(cl);
									db.SubmitChanges();
									msgDT += string.Format("<div style='color: blue'>Chủng loại {0} đã được xóa.</div>", cl.code_cl);
								}

								msg += string.Format("<div style='border: 1px solid #CCC; padding: 5px; margin-bottom: 5px'>{0}</div>", msgDT);
							}
							#endregion
						}
						catch (Exception ex)
						{
							msg += string.Format("<div style='color: red'>Chủng loại {0} bị lỗi: {1}.</div>", cl.code_cl, ex.Message);
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
        int row = export.SetHeaderAnco(hssfsheet, "DANH SÁCH CHỦNG LOẠI NGƯNG HOẠT ĐỘNG", tungay, denngay, false);

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
            item = "Nhóm NL",
            value = "nhom",
            witdh = 3500
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
        String saveAsFileName = String.Format("DSChungLoaiNHD-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
