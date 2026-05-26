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

public partial class Tool_XuatDSHHNHD : System.Web.UI.Page
{
    private LinqDBDataContext db = new LinqDBDataContext();
    string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    string filePath = "", filenameLC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string chungloai = Request.QueryString["cl"];
        string detai = Request.QueryString["dt"];
        string mausac = Request.QueryString["ms"];
        string title = "";

        if (!string.IsNullOrEmpty(chungloai))
        {
            title += "CL:" + chungloai;
            chungloai = string.Format("AND SUBSTRING(sp.ma_sanpham, 1, 2) = '{0}'", chungloai);
        }

        if (!string.IsNullOrEmpty(detai))
        {
            title += " DT:" + detai;
            detai = string.Format("AND SUBSTRING(sp.ma_sanpham, 7, 2) = '{0}'", detai);
        }

        if (!string.IsNullOrEmpty(mausac))
        {
            title += " MS:" + mausac;
            mausac = string.Format("AND SUBSTRING(sp.ma_sanpham, 13, 2) = '{0}'", mausac);
        }

        string sql = string.Format(@" 
            select 
                sp.md_sanpham_id, 
                sp.ma_sanpham, 
                kd.ten_tv as ten_kd, 
                kd.ma_kieudang as ma_kd, 
                cn.ten_tv as ten_cn, 
                cn.ma_chucnang as ma_cn,
	            sp.mota_tiengviet,  
                sp.mota_tienganh, 
                sp.L_inch, 
                sp.W_inch, 
                sp.H_inch, 
                sp.L_cm, 
                sp.W_cm, 
                sp.H_cm, 
                sp.trongluong, 
                sp.dientich,
	            (nnl.hehang + '('+ nnl.nhom +')') as nangluc
            FROM 
	            md_sanpham sp 
	            left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id
	            left join md_nhomnangluc nnl on sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
	            left join md_kieudang kd on sp.md_kieudang_id = kd.md_kieudang_id
            WHERE 1=1
            {0}
            {1}
            {2}
            order by sp.ma_sanpham
        ", chungloai, detai, mausac);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, title);
        }
        else
        {
            Response.Write("<h3>Không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string title)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 12);
        int row = export.SetHeaderAnco(hssfsheet, "DANH SÁCH HÀNG HÓA THUỘC [" + title + "]", "", "", false);
        #region Header Column
        int widthDF = 5000;
        List<ItemValue> lstHeader = new List<ItemValue>();

        var item = new ItemValue
        {
            item = "Mã SP",
            value = "ma_sanpham",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tên KD",
            value = "ten_kd",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mã KD",
            value = "ma_kd",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tên CN",
            value = "ten_cn",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mã CN",
            value = "ma_cn",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mô tả TV",
            value = "mota_tiengviet",
            witdh = 7000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mô tả TA",
            value = "mota_tienganh",
            witdh = 7000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "L(cm)",
            value = "L_cm",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "W(cm)",
            value = "W_cm",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "H(cm)",
            value = "H_cm",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Trọng lượng",
            value = "trongluong",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Diện tích",
            value = "dientich",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Nhóm NL",
            value = "nangluc",
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
                string[] arrDecimal = new string[] { "totalcbm", "amount", "discount", "hoahong" };
                string[] arrDate = new string[] { "ngaylap", "shipmenttime" };
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
        String saveAsFileName = String.Format("DSHangHoaTheo{1}-{0}.xls", DateTime.Now, title);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
