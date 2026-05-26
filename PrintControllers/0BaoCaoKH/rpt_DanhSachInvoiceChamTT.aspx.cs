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
using System.IO;

public partial class ReportWizard_RptKhachHang_rpt_DanhSachInvoiceChamTT : System.Web.UI.Page
{
    public bool sendmail = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        sendmail = Request.QueryString["sendmail"] == "true";
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        string sql = string.Format(@"
            declare @startdate datetime = convert(datetime, '{0}', 103)
            declare @enddate datetime = convert(datetime, '{1}', 103)

			select 
	            C.ngaylap, 
	            C.so_inv,
	            C.donhang,
	            C.totalgross,
	            C.tiendatcoc,
	            C.tiendatra,
	            C.tienconlai,
	            C.ngayvandon,
	            C.ngayphaitra,
	            C.paymentterm,
	            C.songaychamtra
            from (
	            select distinct
		            A.ngaylap,
		            A.so_inv, 
		            dbo.LayChungTuDonHang(A.c_packinginvoice_id) as donhang, 
		            A.totalgross, 
		            A.tiendatcoc,
		            A.tiendatra,
		            A.tienconlai,
		            A.ngayvandon,
		            DATEADD(day, B.songayphaithanhtoan, A.ngayvandon) as ngayphaitra,
		            (DATEDIFF(day, getdate(), A.ngayvandon + B.songayphaithanhtoan)) as songaychamtra,
		            B.ten_paymentterm as paymentterm
	            from (
		            select 
		            pkl.c_packinginvoice_id, 
		            pkl.ngaylap, 
		            pkl.so_inv, 
		            pkl.totalgross, 
		            pkl.tiendatcoc, 
		            pkl.tiendatra,
		            pkl.tienconlai, 
		            pkl.etd as ngayvandon
		            FROM c_packinginvoice pkl WITH (NOLOCK)
		            where pkl.etd between @startdate and @enddate and pkl.md_trangthai_id = 'HIEULUC' and pkl.tienconlai > 1
                    union
		            select 
		            pkl.c_packinginvoice_id, 
		            pkl.ngaylap, 
		            pkl.so_inv, 
		            pkl.totalgross, 
		            pkl.tiendatcoc, 
		            pkl.tiendatra,
		            pkl.tienconlai, 
		            pkl.etd as ngayvandon
		            FROM c_packinginvoice pkl WITH (NOLOCK)
		            where pkl.etd < @startdate and pkl.md_trangthai_id = 'HIEULUC' and pkl.tienconlai > 1
	            )A
	            left join (
		            select dpkl.c_packinginvoice_id, pmt.ten_paymentterm, pmt.mota, pmt.songayphaithanhtoan  
		            from c_dongpklinv dpkl with (nolock) 
		            left join c_donhang dh with (nolock) on dpkl.c_donhang_id = dh.c_donhang_id 
		            left join md_paymentterm pmt with (nolock) on dh.md_paymentterm_id = pmt.md_paymentterm_id 
	            )B on A.c_packinginvoice_id = B.c_packinginvoice_id
            )C
            where C.songaychamtra <= 0
            order by C.ngaylap desc, C.so_inv asc
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
        int row = export.SetHeaderAnco(hssfsheet, "DANH SÁCH INVOICE CHẬM THANH TOÁN", tungay, denngay, false);

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
            item = "NGÀY LẬP",
            value = "ngaylap",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "INVOICE",
            value = "so_inv",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "ĐƠN HÀNG",
            value = "donhang",
            witdh = 6000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TRỊ GIÁ" + Environment.NewLine + "INVOICE",
            value = "totalgross",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TIỀN CỌC",
            value = "tiendatcoc",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TIỀN ĐÃ TRẢ",
            value = "tiendatra",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TIỀN CÒN LẠI",
            value = "tienconlai",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NGÀY VẬN ĐƠN",
            value = "ngayvandon",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NGÀY PHẢI" + Environment.NewLine + "THANH TOÁN",
            value = "ngayphaitra",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "SỐ NGÀY" + Environment.NewLine + "CHẬM TRẢ",
            value = "songaychamtra",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "PAYMENT TERM",
            value = "paymentterm",
            witdh = 6000
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
                string[] arrDecimal = new string[] { "totalgross", "tiendatcoc", "tiendatra", "tienconlai" };
                string[] arrDate = new string[] { "ngayvandon", "ngayphaitra", "ngaylap" };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright2dec;
                }
                else if (lstHeader[j].value == "songaychamtra")
                {
                    cell.SetCellValue(int.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright;
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

        IRow row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("Tổng cộng");
        row_total.GetCell(0).CellStyle = export.borderrightBold;
        export.MergedRegion(hssfsheet, row, 0, 2);
        CellRangeAddress cellRange02 = new CellRangeAddress(row, row, 0, 2);
        export.set_borderThin(cellRange02, "LRTB", hssfsheet);

        row_total.CreateCell(3).SetCellValue("");
        row_total.GetCell(3).CellStyle = export.border;

        row_total.CreateCell(4).SetCellFormula(string.Format("SUM(E{0}:E{1})", start_sum, end_sum));
        row_total.GetCell(4).CellStyle = export.borderrightBold2dec;

        row_total.CreateCell(5).SetCellValue("");
        row_total.GetCell(5).CellStyle = export.border;

        export.MergedRegion(hssfsheet, row, 5, 11);
        CellRangeAddress cellRange59 = new CellRangeAddress(row, row, 5, 11);
        export.set_borderThin(cellRange59, "LRTB", hssfsheet);
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("DanhSachINVChamTT-{0}.xls", DateTime.Now.ToString("ddMMyyyyHH.mm.ss.fff"));

        if(sendmail)
        {
            try
            {
                var link = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/SendMail/{0}", saveAsFileName));
                using (FileStream file = new FileStream(link, FileMode.Create, FileAccess.ReadWrite))
                {
                    hssfworkbook.Write(file);
                }
                var dic = new Dictionary<string, string>();
                dic["link"] = link;
                dic["name"] = saveAsFileName;
                Response.Clear();
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dic));
                Response.Flush();
                Response.SuppressContent = true;
                ApplicationInstance.CompleteRequest();
            }
            catch(Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        else
            export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
