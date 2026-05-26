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

public partial class ReportWizard_RptKhachHang_rpt_XuatHangTheoPO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        string sql = string.Format(@"
            declare @tungay datetime = convert(datetime,'{0}', 103)
            declare @denngay datetime = convert(datetime,'{1}', 103)
            declare @table table(
                ma_dtkd nvarchar(100),
                ncu nvarchar(1000),
                sochungtu nvarchar(100),
                shipmenttime datetime,
                giatripo decimal(18,2),
                discount decimal(18,2),
                phicong decimal(18,2),
                phitru decimal(18,2),
                daxuat decimal(18,2),
                tongcoc decimal(18,2),
                conlai decimal(18,2)
            )

            insert into @table
            select
                dtkd.ma_dtkd, 
                dtkd2.ma_dtkd as ncu, 
                A.sochungtu, 
                A.shipmenttime,
                A.amount as giatripo,
                (A.amount * A.discount / 100) as discount,
                isnull((select SUM(isnull(phi, 0)) from c_phidonhang pdh where pdh.c_donhang_id = A.c_donhang_id and phitang = 1), 0) as phicong, 
	            isnull((select SUM(isnull(phi, 0)) from c_phidonhang pdh where pdh.c_donhang_id = A.c_donhang_id and phitang = 0), 0) as phitru, 
	            SUM((ddh.giafob + isnull(ddh.phi,0)) * ddh.soluong_daxuat) as daxuat, 
	            SUM(isnull(cttc.sotien, 0))/COUNT(1) as tongcoc, 
	            0 as conlai
            from (
                select
                    dh.md_doitackinhdoanh_id, dh.c_donhang_id, dh.sochungtu, dh.amount, dh.totalamount, dh.shipmenttime, dh.discount
                from 
                    c_donhang dh 
                where 
                    dh.md_trangthai_id = 'HIEULUC'
                    and dh.shipmenttime between @tungay and @denngay
            )A 
            left join md_doitackinhdoanh dtkd on A.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
            left join c_dongdonhang ddh on ddh.c_donhang_id = A.c_donhang_id
            left join md_doitackinhdoanh dtkd2 on ddh.nhacungungid = dtkd2.md_doitackinhdoanh_id
            left join c_chitietthuchi cttc on A.c_donhang_id = cttc.c_donhang_id
            group by dtkd.ma_dtkd, A.sochungtu, A.shipmenttime, dtkd2.ma_dtkd, A.totalamount, A.amount, A.discount, A.c_donhang_id
            order by A.sochungtu, dtkd.ma_dtkd

            --------------------------------------
            declare @tblNCC table (
                sochungtu nvarchar(100),
                ncu nvarchar(100)
            )
            insert into @tblNCC
            select distinct 
                A.sochungtu,
                A.ncu
            from @table A

            declare @tblNCCGroup table (
                sochungtu nvarchar(100),
                ncu nvarchar(MAX)
            )
            insert into @tblNCCGroup
            select distinct A.sochungtu,
            (
                SELECT isnull(T.ncu + ',' + char(10),'')
                FROM @tblNCC T
                WHERE A.sochungtu = T.sochungtu
                FOR XML PATH(''),type
            ).value('.','varchar(max)') as ncu
            from @tblNCC A
            --------------------------------------

            select A.*, 
            ncc.ncu, 
              (A.giatripo - A.daxuat) as conlai
            --(A.giatripo + A.phicong - A.phitru - A.daxuat) as conlai
            from (
	            select A.ma_dtkd, A.sochungtu, A.shipmenttime,
	            A.giatripo, A.discount, A.phicong, A.phitru, SUM(A.daxuat) as daxuat, A.tongcoc
	            from @table A
	            group by A.ma_dtkd, A.sochungtu, A.shipmenttime, A.giatripo, A.discount, A.phicong, A.phitru, A.tongcoc
            ) A
            inner join @tblNCCGroup ncc on A.sochungtu = ncc.sochungtu
        ", startdate, enddate);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
        }
        else
        {
            Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
        }
    }

    public void CreateWorkBookPO(DataTable dt, string tungay, string denngay)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 11);
        int row = export.SetHeaderAnco(hssfsheet, "TÌNH HÌNH XUẤT HÀNG THEO P/O", tungay, denngay, false);

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
            item = "MÃ KH",
            value = "ma_dtkd",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "PO",
            value = "sochungtu",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Shipment time",
            value = "shipmenttime",
            witdh = 4500
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
            item = "TỔNG" + Environment.NewLine + "GIÁ TRỊ PO",
            value = "giatripo",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TỔNG" + Environment.NewLine + "DISCOUNT",
            value = "discount",
            witdh = 3500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TỔNG" + Environment.NewLine + "P+",
            value = "phicong",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TỔNG" + Environment.NewLine + "P-",
            value = "phitru",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TỔNG" + Environment.NewLine + "ĐÃ XUẤT",
            value = "daxuat",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TỔNG" + Environment.NewLine + "CỌC",
            value = "tongcoc",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "CÒN LẠI" + Environment.NewLine + "CHƯA XUẤT",
            value = "conlai",
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
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "giatripo", "discount", "daxuat", "tongcoc", "phicong", "phitru", "conlai" };
                string[] arrDate = new string[] { "shipmenttime" };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
					var strNB = dt.Rows[i][lstHeader[j].value].ToString();
					if(!string.IsNullOrEmpty(strNB)) {
						cell.SetCellValue(double.Parse(strNB));
					}
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
                else if (!String.IsNullOrEmpty(lstHeader[j].value))
                {
                    string str = dt.Rows[i][lstHeader[j].value].ToString();
                    float heightMAX = export.MeasureTextHeight(str, lstHeader[j].witdh);
                    if (heightMAX > height)
                    {
                        height = heightMAX;
                    }

                    if (lstHeader[j].value == "ncu")
                    {
						if(str.Length >= 2)
							str = str.Remove(str.Length - 2);
                    }
                    cell.SetCellValue(str);
                    cell.CellStyle = export.border;
                }
            }
            row_t.HeightInPoints = height;
            row++;
        }
        int end_sum = row;

        IRow row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("Tổng cộng");
        row_total.GetCell(0).CellStyle = export.borderrightBold;
        export.MergedRegion(hssfsheet, row, 0, 2);
        CellRangeAddress cellRange02 = new CellRangeAddress(row, row, 0, 2);
        export.set_borderThin(cellRange02, "LRTB", hssfsheet);

        row_total.CreateCell(3).SetCellValue("");
        row_total.GetCell(3).CellStyle = export.border;
        row_total.CreateCell(4).SetCellValue("");
        row_total.GetCell(4).CellStyle = export.border;
        row_total.CreateCell(5).SetCellFormula(string.Format("SUM(F{0}:F{1})", start_sum, end_sum));
        row_total.GetCell(5).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(6).SetCellFormula(string.Format("SUM(G{0}:G{1})", start_sum, end_sum));
        row_total.GetCell(6).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(7).SetCellValue("");
        row_total.GetCell(7).CellStyle = export.border;
        row_total.CreateCell(8).SetCellFormula(string.Format("SUM(I{0}:I{1})", start_sum, end_sum));
        row_total.GetCell(8).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(9).SetCellFormula(string.Format("SUM(J{0}:J{1})", start_sum, end_sum));
        row_total.GetCell(9).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(10).SetCellFormula(string.Format("SUM(K{0}:K{1})", start_sum, end_sum));
        row_total.GetCell(10).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(11).SetCellFormula(string.Format("SUM(L{0}:L{1})", start_sum, end_sum));
        row_total.GetCell(11).CellStyle = export.borderrightBold2dec;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("XuatHangTheoPO-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
