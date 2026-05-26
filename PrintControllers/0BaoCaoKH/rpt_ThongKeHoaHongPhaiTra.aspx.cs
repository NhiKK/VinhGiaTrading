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

public partial class ReportWizard_RptKhachHang_rpt_ThongKeHoaHongPhaiTra : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string doitackinhdoanh_id = Request.QueryString["doitackinhdoanh_id"];

        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";
        string dtkd = "";
        if (doitackinhdoanh_id != "NULL")
        {
            dtkd = "and dh.md_nguoilienhe_id = '" + doitackinhdoanh_id + "'";
        }

        string sql = string.Format(@"
            declare @start datetime = convert(datetime,'{0}', 103)
            declare @end datetime = convert(datetime,'{1}', 103)

            declare @table table (
                khachhang nvarchar(100),
                nhhh nvarchar(200),
                soPO nvarchar(100),
                soINV nvarchar(100),
                tongtienINV decimal(18,2),
                TienHoaHong decimal(18,2),
                DaTra decimal(18,2),
                ConLai decimal(18,2),
                phantramhoahong decimal(18,2),
                ncu nvarchar(100),
                ghichu nvarchar(MAX),
                tien decimal(18,2)
            )

            insert into @table
            select 
                kh.ma_dtkd as khachhang,
                nlh.ten_dtkd as nhhh,
                dh.sochungtu as soPO,
                civ.so_inv as soINV,
                civ.tongtienINV,
                civ.tienhoahong as TienHoaHong,
                civ.DaTra,
                (civ.tienhoahong - civ.DaTra) as ConLai,
                civ.phantramhoahong,
                ncc.ma_dtkd as ncu,
                '' as ghichu,
                SUM(cdiv.thanhtien) as tien
            from (            
                select 
                    civ.c_packinginvoice_id,
                    civ.md_doitackinhdoanh_id as khid,
                    civ.so_inv,
                    civ.totalgross as tongtienINV,
                    convert(decimal(18,2),civ.totalgross * civ.phantramhoahong / 100) as tienhoahong,
                    isnull(civ.hoahongdatra, 0) as DaTra,
                    civ.phantramhoahong
                from c_packinginvoice civ
                where 
                    civ.ngay_motokhai between @start and @end
                    and civ.md_trangthai_id = 'HIEULUC'
            )civ
            inner join c_dongpklinv cdiv on civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
            inner join c_donhang dh on cdiv.c_donhang_id = dh.c_donhang_id and len(dh.md_nguoilienhe_id) > 0 and isnull(dh.hoahong, 0) > 0 {2}
            inner join md_doitackinhdoanh ncc on cdiv.nhacungungid = ncc.md_doitackinhdoanh_id
            inner join md_doitackinhdoanh kh on civ.khid = kh.md_doitackinhdoanh_id
            inner join md_doitackinhdoanh nlh on dh.md_nguoilienhe_id = nlh.md_doitackinhdoanh_id
            group by 
	            kh.ma_dtkd,
                nlh.ten_dtkd,
                dh.sochungtu,
                civ.so_inv,
                civ.tongtienINV,
                ncc.ma_dtkd,
                civ.tienhoahong,
                civ.DaTra,
                civ.phantramhoahong
            -----------------------------
                declare @tblPO table (
                soINV nvarchar(100),
                nhhh nvarchar(200),
                soPO nvarchar(100)
            )
            insert into @tblPO
            select distinct A.soINV, A.nhhh, A.soPO
            from @table A

            declare @tblPOGroup table (
                soINV nvarchar(100),
                nhhh nvarchar(MAX),
                soPO nvarchar(MAX)
            )
            insert into @tblPOGroup
            select distinct A.soINV, A.nhhh, 
            (
                SELECT isnull(T.soPO + ',' + char(10),'')
                FROM @tblPO T
                WHERE A.soINV = T.soINV and A.nhhh = T.nhhh
                FOR XML PATH(''), type
            ).value('.','varchar(max)') as soPO
            from @tblPO A
            --------------------------------------
            declare @tblNCC table (
                soINV nvarchar(100),
                nhhh nvarchar(200),
                ncu nvarchar(100)
            )
            insert into @tblNCC
            select distinct 
                A.soINV, 
                A.nhhh, 
                A.ncu
            from @table A

            declare @tblNCCGroup table (
                soINV nvarchar(100),
                nhhh nvarchar(200),
                ncu nvarchar(MAX)
            )
            insert into @tblNCCGroup
            select distinct A.soINV, A.nhhh,
            (
                SELECT isnull(T.ncu + ',' + char(10),'')
                FROM @tblNCC T
                WHERE A.soINV = T.soINV
                FOR XML PATH(''),type
            ).value('.','varchar(max)') as ncu
            from @tblNCC A
            --------------------------------------

            declare @tableDT table (
                khachhang nvarchar(100),
                nhhh nvarchar(200),
                soPO nvarchar(100),
                soINV nvarchar(100),
                tongtienINV decimal(18,2),
                ConLai decimal(18,2),
                phantramhoahong decimal(18,2),
                ncu nvarchar(100)
            )
            insert into @tableDT
            select
	            '' as khachhang, 
	            '' as nhhh, 
	            '' as soPO, 
	            A.soINV, 
	            sum(A.tien) as tongtienINV,
	            A.ConLai, 
	            A.phantramhoahong,
	            A.ncu
            from @table A
            group by A.soINV, A.ncu, A.ConLai, A.phantramhoahong

            select * 
            from (
	            select 
		            A.khachhang, A.nhhh, A.soPO, A.soINV, 
		            (case when A.countNCU < 2 then A.tongtienINV else -1 end) as tongtienINV, 
		            (case when A.countNCU < 2 then A.TienHoaHong else -1 end) as TienHoaHong,
		            (case when A.countNCU < 2 then A.DaTra else -1 end) as DaTra,
		            (case when A.countNCU < 2 then A.ConLai else -1 end) as ConLai, 
		            A.phantramhoahong, 
		            A.ncu, 
		            A.ghichu 
	            from 
	            (
		            select 
			            A.khachhang, A.nhhh, po.soPO, A.soINV, A.tongtienINV, 
			            A.TienHoaHong,
			            A.DaTra,
			            A.ConLai,
			            A.phantramhoahong, ncc.ncu, A.ghichu,
			            (LEN(ncc.ncu) - LEN(REPLACE(ncc.ncu, ',', ''))) as countNCU
		            from (
			            select distinct
				            A.khachhang, A.nhhh, A.soINV, A.tongtienINV, 
				            A.TienHoaHong, A.DaTra, A.ConLai, A.phantramhoahong, A.ghichu
			            from @table A
		            )A
		            inner join @tblNCCGroup ncc on A.soINV = ncc.soINV and A.nhhh = ncc.nhhh
		            inner join @tblPOGroup po on A.soINV = po.soINV and A.nhhh = po.nhhh
	            )A
	            union
	            select 
		            C.khachhang, C.nhhh, C.soPO, C.soINV, C.tongtienINV,
		            C.TienHoaHong,
		            (case when C.ConLai = 0 then C.TienHoaHong else 0 end) as DaTra,
		            (case when C.ConLai = 0 then 0 else C.TienHoaHong end) as ConLai,
		            C.phantramhoahong, C.ncu, '' as ghichu
	            from (
		            select B.*, 
		            convert(decimal(18,2),B.tongtienINV * B.phantramhoahong/100) as TienHoaHong
		            from
		            (
			            select A.* 
			            from @tableDT A
			            where (select COUNT(1) from @tableDT where A.soINV = soINV) > 1
		            )B
	            )C
            )D
            order by D.soINV, D.khachhang desc, D.ncu

        ", startdate, enddate, dtkd);

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
        int row = export.SetHeaderAnco(hssfsheet, "THỐNG KÊ HOA HỒNG PHẢI TRẢ", tungay, denngay, false);

        #region Header Column
        int widthDF = 4000;
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
            value = "khachhang",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tên Người Hưởng HH",
            value = "nhhh",
            witdh = 5000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số P/O",
            value = "soPO",
            witdh = 10350
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Số INV",
            value = "soINV",
            witdh = 5200
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tổng Tiền INV",
            value = "tongtienINV",
            witdh = 3700
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tiền Hoa Hồng",
            value = "TienHoaHong",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Đã Trả",
            value = "DaTra",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Còn Lại",
            value = "ConLai",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "% Hoa Hồng",
            value = "phantramhoahong",
            witdh = 2500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NCƯ",
            value = "ncu",
            witdh = 4500
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Ghi Chú",
            value = "",
            witdh = 3000
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
        int STT = 0;
        string groupINV = "";
        List<IntInt> lstMerge = new List<IntInt>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;
            string khachhang = dt.Rows[i]["khachhang"].ToString();
            string invoice = dt.Rows[i]["soINV"].ToString();

            if (groupINV != invoice)
            {
                string ncu = dt.Rows[i]["ncu"].ToString();
                int count = ncu.Where(c => c == ',').Count();
                if (count > 1)
                {
                    lstMerge.Add(new IntInt { rowstart = row + 1, rowend = row + count });
                }
                groupINV = invoice;
            }

            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "tongtienINV", "TienHoaHong", "DaTra", "ConLai" };
                string[] Remove2LastChar = new string[] { "ncu", "soPO" };

                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    string valDec = dt.Rows[i][lstHeader[j].value].ToString();
                    if (valDec != "-1.00")
                    {
                        cell.SetCellValue(double.Parse(valDec));
                    }
                    cell.CellStyle = export.borderright2dec;
                }
                else if (lstHeader[j].value == "phantramhoahong")
                {
                    cell.SetCellValue(dt.Rows[i][lstHeader[j].value].ToString() + "%");
                    cell.CellStyle = export.bordercenter;
                }
                else if (lstHeader[j].value == "rowNum")
                {
                    if (khachhang != "")
                    {
                        STT++;
                        cell.SetCellValue(STT);
                    }
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

                    if (Remove2LastChar.Contains(lstHeader[j].value))
                    {
                        if(str.Contains(",")) {
                            str = str.Remove(str.Length - 2);
                        }
                    }
                    cell.SetCellValue(str);
                    cell.CellStyle = export.border;
                }
                else
                {
                    cell.SetCellValue("");
                    cell.CellStyle = export.border;
                }
            }
            row_t.HeightInPoints = height;
            row++;
        }
        int end_sum = row;

        string spaceLine2 = " ", spaceLine1 = " ";
        for (int spaceI = 0; spaceI < 79; spaceI++)
        {
            spaceLine2 += " ";
        }

        spaceLine1 = spaceLine2;
        for (int spaceI = 0; spaceI < 10; spaceI++)
        {
            spaceLine1 += " ";
        }

        IRow row_total = s1.CreateRow(row);
		#region Tổng cộng
        row_total.HeightInPoints = 25;
        row_total.CreateCell(0).SetCellValue("Tổng cộng");
		row_total.GetCell(0).CellStyle = export.centerBold;
        CellRangeAddress cellRange04 = new CellRangeAddress(row, row, 0, 4);
        s1.AddMergedRegion(cellRange04);
		export.set_borderThin(cellRange04, "LRTB", hssfsheet);
		
		row_total.CreateCell(5).SetCellFormula(string.Format("SUM(F{0}:F{1})", start_sum, end_sum));
		row_total.GetCell(5).CellStyle = export.borderrightBold2dec;
		row_total.CreateCell(6).SetCellFormula(string.Format("SUM(G{0}:G{1})", start_sum, end_sum));
		row_total.GetCell(6).CellStyle = export.borderrightBold2dec;
		row_total.CreateCell(7).SetCellFormula(string.Format("SUM(H{0}:H{1})", start_sum, end_sum));
		row_total.GetCell(7).CellStyle = export.borderrightBold2dec;
		row_total.CreateCell(8).SetCellFormula(string.Format("SUM(I{0}:I{1})", start_sum, end_sum));
		row_total.GetCell(8).CellStyle = export.borderrightBold2dec;
		row_total.CreateCell(9).SetCellValue("");
		row_total.GetCell(9).CellStyle = export.border;
		row_total.CreateCell(10).SetCellValue("");
		row_total.GetCell(10).CellStyle = export.border;
		row_total.CreateCell(11).SetCellValue("");
		row_total.GetCell(11).CellStyle = export.border;
        row++;
		#endregion
		
		row_total = s1.CreateRow(row);
        row_total.HeightInPoints = 15;
        row_total.CreateCell(0).SetCellValue("");
        CellRangeAddress cellRange011 = new CellRangeAddress(row, row, 0, 11);
        s1.AddMergedRegion(cellRange011);
        row++;

        row_total = s1.CreateRow(row);
        row_total.HeightInPoints = 17;
        string rowHeadSign1 = "Người lập";
        string rowHeadSign2 = "Kế toán trưởng";
        string rowHeadSign3 = "Giám đốc";
        row_total.CreateCell(0).SetCellValue(rowHeadSign1 + spaceLine1 + rowHeadSign2 + spaceLine1 + rowHeadSign3);
        row_total.GetCell(0).CellStyle = export.centerBold;
        cellRange011 = new CellRangeAddress(row, row, 0, 11);
        s1.AddMergedRegion(cellRange011);
        row++;

        row_total = s1.CreateRow(row);
        row_total.HeightInPoints = 17;
        rowHeadSign1 = "(Ký và ghi rõ họ tên)";
        rowHeadSign2 = "(Ký và ghi rõ họ tên)";
        rowHeadSign3 = "(Ký và ghi rõ họ tên)";
        row_total.CreateCell(0).SetCellValue(rowHeadSign1 + spaceLine2 + rowHeadSign2 + spaceLine2 + rowHeadSign3);
        row_total.GetCell(0).CellStyle = export.center;
        cellRange011 = new CellRangeAddress(row, row, 0, 11);
        s1.AddMergedRegion(cellRange011);
        #endregion

        #region Merge
        foreach(var itemMer in lstMerge)
        {
            var cellRange = new CellRangeAddress(itemMer.rowstart, itemMer.rowend, 0, 3);
            s1.AddMergedRegion(cellRange);
        }
        #endregion

            hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("ThongKeHoaHongPhaiTra-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}

public class IntInt
{
    public int rowstart { get; set; }
    public int rowend { get; set; }
}