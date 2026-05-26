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

public partial class Tool_KetThucQO : System.Web.UI.Page
{
    private LinqDBDataContext db = new LinqDBDataContext();
    string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    string filePath = "", filenameLC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string type = Request.QueryString["type"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        HttpFileCollection files = Request.Files;
        string arrID = "";
        if (files.Count > 0)
        {
            filenameLC = files[0].FileName.Replace(".xlsx", ".xls");
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
            catch { }
        }

        if (arrID != "")
        {
            arrID = arrID.Remove(arrID.Length - 1);
            arrID = "and baogia.sobaogia in (" + arrID + ")";
        }

        string sql = string.Format(@" 
            select baogia.c_baogia_id, baogia.md_trangthai_id, baogia.sobaogia, dtkd.ma_dtkd, 
            baogia.ngaybaogia, baogia.ngayhethan, cb.ten_cangbien, baogia.shipmenttime, baogia.nguoitao, baogia.mota as ghichu 
            FROM c_baogia baogia with (nolock) 
			LEFT JOIN md_doitackinhdoanh dtkd with (nolock) on baogia.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
			LEFT JOIN md_cangbien cb with (nolock) on baogia.md_cangbien_id = cb.md_cangbien_id
            WHERE 1=1 
            and baogia.ngayhethan between convert(datetime, '{0}', 103) and convert(datetime, '{1}', 103) 
            and baogia.md_trangthai_id != 'KETTHUC' 
            {2}
            order by baogia.sobaogia asc
        ", startdate, enddate, arrID);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            if (type == "1")
            {
                this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
            }
            else if (type == "2")
            {
                string msg = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idBaoGia = dt.Rows[i]["c_baogia_id"].ToString();
                    c_baogia bg = db.c_baogias.Where(s => s.c_baogia_id == idBaoGia).FirstOrDefault();
                    try
                    {
                        bg.md_trangthai_id = "KETTHUC";
                        db.SubmitChanges();
                        msg += string.Format("<div style='color: blue'>{0} đã được kết thúc.</div>", bg.sobaogia);
                    }
                    catch (Exception ex)
                    {
                        if (bg != null)
                            msg += string.Format("<div style='color: red'>{0} bị lỗi: {1}.</div>", bg.sobaogia, ex.Message);
                        else
                            msg += string.Format("<div style='color: red'>{0}.</div>", ex.Message);
                    }
                }
                Response.Write(msg);
            }
        }
        else
        {
            Response.Write("<h3>Không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 7);
        int row = export.SetHeaderAnco(hssfsheet, "DANH SÁCH QO CẦN KẾT THÚC", tungay, denngay, false);

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
            item = "Số báo giá",
            value = "sobaogia",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Khách hàng",
            value = "ma_dtkd",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Ngày báo giá",
            value = "ngaybaogia",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Ngày hết hạn",
            value = "ngayhethan",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tên cảng biển",
            value = "ten_cangbien",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Người tạo",
            value = "nguoitao",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Ghi chú",
            value = "ghichu",
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
                string[] arrDate = new string[] { "ngaybaogia", "ngayhethan" };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright2dec;
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
        String saveAsFileName = String.Format("DSQOCanKetThuc-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
