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

public partial class ReportWizard_RptKhachHang_rpt_DoanhThuTheoMH : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];
        string hehang_id = Request.QueryString["hehang_id"];
        string baocaotheo = Request.QueryString["baocaotheo"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";
        if (String.IsNullOrEmpty(doitackinhdoanh_id))
        {
            doitackinhdoanh_id = "null";
        }
        else
        {
            doitackinhdoanh_id = string.Format("'{0}'", doitackinhdoanh_id);
        }

        string where_hehangid = "";
        if (!String.IsNullOrEmpty(hehang_id))
        {
            LinqDBDataContext db = new LinqDBDataContext();
            md_chungloai cl = db.md_chungloais.Where(s => s.md_chungloai_id == hehang_id).FirstOrDefault();
            if (cl != null)
            {
                where_hehangid = string.Format(@"and A.code_cl = '{0}'", cl.code_cl);
            }
        }

        if(baocaotheo == "PO")
        {
            baocaotheo = "[dbo].[rpt_doanhthutheomahangPO]";
        }
        else
        {
            baocaotheo = "[dbo].[rpt_doanhthutheomahang]";
        }

        string sql = string.Format(@"
            declare @tungay datetime, @denngay datetime
            declare @table table(
                code_cl nvarchar(100),
                md_chucnang_id varchar(32), 
                ma_dtkd nvarchar(1000),
                md_quocgia_id varchar(32),
                ma_sanpham nvarchar(100),
                mota_tiengviet nvarchar(MAX),
                soluong int,
                gia decimal(18,2),
                nhacungungid varchar(32),
                thanhtien decimal(18,2),
                tungay datetime,
                denngay datetime
            )
            set @tungay = (select CONVERT(datetime, N'{0}', 103))
            set @denngay = (select CONVERT(datetime, N'{1}', 103))
            insert into @table
            Exec {4} @tungay, @denngay, {2}

            select A.ma_dtkd, A.code_cl, qg.ten_quocgia, A.ma_sanpham,  
            A.mota_tiengviet, cn.ten_tv, ncu.ma_dtkd as ncu, A.soluong, A.gia, A.thanhtien
            from @table A
            left join md_quocgia qg with (nolock) on A.md_quocgia_id = qg.md_quocgia_id
            left join md_doitackinhdoanh ncu with (nolock) on A.nhacungungid = ncu.md_doitackinhdoanh_id
            left join md_chucnang cn with (nolock) on cn.md_chucnang_id = A.md_chucnang_id
            where 1 = 1 {3}
            order by A.ma_dtkd, A.code_cl, A.ma_sanpham 
        ", startdate, enddate, doitackinhdoanh_id, where_hehangid, baocaotheo);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            if (doitackinhdoanh_id == "null")
            {
                this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"], baocaotheo);
            }
            else
            {
                this.CreateWorkBookPO2(dt, Request.QueryString["startdate"], Request.QueryString["enddate"], baocaotheo);
            }
        }
        else
        {
            Response.Write("<h3>Không có dữ liệu (404 data not found)</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay, string baocaotheo)
    {
        if (baocaotheo.Contains("PO"))
        {
            baocaotheo = "(THEO PO)";
        }
        else
        {
            baocaotheo = "(THEO INV)";
        }

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 9);

        int row = export.SetHeaderAnco(hssfsheet, "TỔNG HỢP DOANH THU THEO TỪNG MẶT HÀNG " + baocaotheo, tungay, denngay, true);

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
            item = "Mã KH",
            value = "ma_dtkd",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Quốc gia",
            value = "ten_quocgia",
            witdh = 4500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mã hàng",
            value = "ma_sanpham",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mô tả sản phẩm",
            value = "mota_tiengviet",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tên chức năng SP",
            value = "ten_tv",
            witdh = 3500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NCU",
            value = "ncu",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số lượng",
            value = "soluong",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Giá",
            value = "gia",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Thành tiền",
            value = "thanhtien",
            witdh = 4000
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
            #region detail table
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "soluong", "gia", "thanhtien" };
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
                else if (!String.IsNullOrEmpty(lstHeader[j].value))
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
            #endregion
        }
        int end_sum = row;

        IRow row_total = s1.CreateRow(row);
        var cellTT = row_total.CreateCell(0);
        cellTT.SetCellValue("Tổng Cộng:");
        cellTT.CellStyle = export.centerBold;
        export.MergedRegion(hssfsheet, row, 0, 4);
        CellRangeAddress cellRange04TT = new CellRangeAddress(row, row, 0, 4);
        export.set_borderThin(cellRange04TT, "LRTB", hssfsheet);
        //--
        cellTT = row_total.CreateCell(5);
        cellTT.SetCellValue("");
        cellTT.CellStyle = export.bordercenterBold;
        //--
        cellTT = row_total.CreateCell(6);
        cellTT.SetCellValue("");
        cellTT.CellStyle = export.bordercenterBold;
        //--
        cellTT = row_total.CreateCell(7);
        cellTT.SetCellFormula(string.Format("SUM(H{0}:H{1})", start_sum, end_sum));
        cellTT.CellStyle = export.borderrightBold2dec;
        //--
        cellTT = row_total.CreateCell(8);
        cellTT.SetCellValue("");
        cellTT.CellStyle = export.bordercenterBold;
        //--
        cellTT = row_total.CreateCell(9);
        cellTT.SetCellFormula(string.Format("SUM(J{0}:J{1})", start_sum, end_sum));
        cellTT.CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("DoanhThuTheoMH-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }

    public void CreateWorkBookPO2(DataTable dt, string tungay, string denngay, string baocaotheo)
    {
        if (baocaotheo.Contains("PO"))
        {
            baocaotheo = "(THEO PO)";
        }
        else
        {
            baocaotheo = "(THEO INV)";
        }

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 9);
        int row = export.SetHeaderAnco(hssfsheet, "TỔNG HỢP DOANH THU THEO TỪNG MẶT HÀNG " + baocaotheo, tungay, denngay, true);

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
            item = "Mã KH",
            value = "ma_dtkd",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Quốc gia",
            value = "ten_quocgia",
            witdh = 4500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mã hàng",
            value = "ma_sanpham",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Mô tả sản phẩm",
            value = "mota_tiengviet",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tên chức năng SP",
            value = "ten_tv",
            witdh = 3500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NCU",
            value = "ncu",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số lượng",
            value = "soluong",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Giá",
            value = "gia",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Thành tiền",
            value = "thanhtien",
            witdh = 4000
        };
        lstHeader.Add(item);
        #endregion

        #region Set Table
        int start_sum = row + 1;
        int start_sumHeHang = start_sum;
        int STT = 1;
        List<int> lstsumKH = new List<int>();
        List<int> lstSumTT = new List<int>();
        string groupKH = "";
        string groupHeHang = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;

            #region group header Mã KH
            string groupKHVal = dt.Rows[i]["ma_dtkd"].ToString();
            if (groupKH != groupKHVal)
            {
                lstsumKH.Clear();
                groupKH = groupKHVal;
                groupHeHang = "";
                var cell = row_t.CreateCell(0);
                cell.SetCellValue("Mã khách hàng:");
                cell.CellStyle = export.celltext;
                export.MergedRegion(hssfsheet, row, 0, 1);
                //--
                cell = row_t.CreateCell(2);
                cell.SetCellValue(groupKH);
                cell.CellStyle = export.celltext;
                export.MergedRegion(hssfsheet, row, 2, 9);
                row++;
                row_t.HeightInPoints = height;
                row_t = s1.CreateRow(row);
            }
            #endregion

            #region group header Hệ hàng
            string groupHeHangVal = dt.Rows[i]["code_cl"].ToString();
            if (groupHeHang != groupHeHangVal)
            {
                groupHeHang = groupHeHangVal;
                var cell = row_t.CreateCell(0);
                cell.SetCellValue("Loại hàng:");
                cell.CellStyle = export.celltext;
                export.MergedRegion(hssfsheet, row, 0, 1);
                //--
                cell = row_t.CreateCell(2);
                cell.SetCellValue(groupHeHang);
                cell.CellStyle = export.celltext;
                export.MergedRegion(hssfsheet, row, 2, 9);
                row++;

                IRow rowColumnHeader = s1.CreateRow(row);
                rowColumnHeader.HeightInPoints = 40;
                for (int j = 0; j < lstHeader.Count; j++)
                {
                    rowColumnHeader.CreateCell(j).SetCellValue(lstHeader[j].item);
                    rowColumnHeader.GetCell(j).CellStyle = export.borderWrap;
                    s1.SetColumnWidth(j, lstHeader[j].witdh);
                }
                row++;
                row_t.HeightInPoints = height;
                row_t = s1.CreateRow(row);
                start_sumHeHang = row + 1;
                STT = 1;
            }
            else
            {
                STT++;
            }
            #endregion

            #region detail table
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "soluong", "gia", "thanhtien" };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright2dec;
                }
                else if (lstHeader[j].value == "rowNum")
                {
                    cell.SetCellValue(STT);
                    cell.CellStyle = export.bordercenter;
                }
                else if (!String.IsNullOrEmpty(lstHeader[j].value))
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
            #endregion

            #region group footer Hệ hàng
            string groupHeHangValFooter = "";
            try { groupHeHangValFooter = dt.Rows[i + 1]["code_cl"].ToString(); }
            catch { }

            if (groupHeHang != groupHeHangValFooter)
            {
                row_t = s1.CreateRow(row);
                var cell = row_t.CreateCell(0);
                cell.SetCellValue("Cộng:");
                cell.CellStyle = export.centerBold;
                export.MergedRegion(hssfsheet, row, 0, 4);
                CellRangeAddress cellRange04 = new CellRangeAddress(row, row, 0, 4);
                export.set_borderThin(cellRange04, "LRTB", hssfsheet);
                //--
                cell = row_t.CreateCell(5);
                cell.SetCellValue("");
                cell.CellStyle = export.bordercenterBold;
                //--
                cell = row_t.CreateCell(6);
                cell.SetCellValue("");
                cell.CellStyle = export.bordercenterBold;
                //--
                cell = row_t.CreateCell(7);
                cell.SetCellFormula(string.Format(@"SUM(H{0}:H{1})", start_sumHeHang, row));
                cell.CellStyle = export.borderrightBold2dec;
                //--
                cell = row_t.CreateCell(8);
                cell.SetCellValue("");
                cell.CellStyle = export.bordercenterBold;
                //--
                cell = row_t.CreateCell(9);
                cell.SetCellFormula(string.Format(@"SUM(J{0}:J{1})", start_sumHeHang, row));
                cell.CellStyle = export.borderrightBold2dec;
                row_t.HeightInPoints = 25;
                row++;
                lstsumKH.Add(row);
            }
            #endregion

            #region group footer Khách Hàng
            string groupKHValFooter = "";
            try { groupKHValFooter = dt.Rows[i + 1]["ma_dtkd"].ToString(); }
            catch { }

            if (groupKH != groupKHValFooter)
            {
                string sumStr = "";
                foreach (int num in lstsumKH)
                {
                    sumStr += "{0}" + num + "+";
                }
                if (sumStr != "") { sumStr = sumStr.Remove(sumStr.Length - 1); }

                row_t = s1.CreateRow(row);
                var cell = row_t.CreateCell(0);
                cell.SetCellValue("Cộng (Theo KH):");
                cell.CellStyle = export.centerBold;
                export.MergedRegion(hssfsheet, row, 0, 4);
                CellRangeAddress cellRange04 = new CellRangeAddress(row, row, 0, 4);
                export.set_borderThin(cellRange04, "LRTB", hssfsheet);
                //--
                cell = row_t.CreateCell(5);
                cell.SetCellValue("");
                cell.CellStyle = export.bordercenterBold;
                //--
                cell = row_t.CreateCell(6);
                cell.SetCellValue("");
                cell.CellStyle = export.bordercenterBold;
                //--
                cell = row_t.CreateCell(7);
                cell.SetCellFormula(string.Format(sumStr, "H"));
                cell.CellStyle = export.borderrightBold2dec;
                //--
                cell = row_t.CreateCell(8);
                cell.SetCellValue("");
                cell.CellStyle = export.bordercenterBold;
                //--
                cell = row_t.CreateCell(9);
                cell.SetCellFormula(string.Format(sumStr, "J"));
                cell.CellStyle = export.borderrightBold2dec;
                row_t.HeightInPoints = 25;
                row++;
                lstSumTT.Add(row);
            }
            #endregion
        }
        #region Tong Cong
        string SumStrTT = "";
        foreach (int num in lstSumTT)
        {
            SumStrTT += "{0}" + num + "+";
        }
        SumStrTT += "0";

        IRow row_total = s1.CreateRow(row);
        var cellTT = row_total.CreateCell(0);
        cellTT.SetCellValue("Tổng Cộng:");
        cellTT.CellStyle = export.centerBold;
        export.MergedRegion(hssfsheet, row, 0, 4);
        CellRangeAddress cellRange04TT = new CellRangeAddress(row, row, 0, 4);
        export.set_borderThin(cellRange04TT, "LRTB", hssfsheet);
        //--
        cellTT = row_total.CreateCell(5);
        cellTT.SetCellValue("");
        cellTT.CellStyle = export.bordercenterBold;
        //--
        cellTT = row_total.CreateCell(6);
        cellTT.SetCellValue("");
        cellTT.CellStyle = export.bordercenterBold;
        //--
        cellTT = row_total.CreateCell(7);
        cellTT.SetCellFormula(string.Format(SumStrTT, "H"));
        cellTT.CellStyle = export.borderrightBold2dec;
        //--
        cellTT = row_total.CreateCell(8);
        cellTT.SetCellValue("");
        cellTT.CellStyle = export.bordercenterBold;
        //--
        cellTT = row_total.CreateCell(9);
        cellTT.SetCellFormula(string.Format(SumStrTT, "J"));
        cellTT.CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        #endregion

        #endregion
        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("DoanhThuTheoMH-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
