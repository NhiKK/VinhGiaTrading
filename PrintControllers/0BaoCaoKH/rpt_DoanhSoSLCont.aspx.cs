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

public partial class ReportWizard_RptKhachHang_rpt_DoanhSoSLCont : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        startdate = startdate + " 00:00:00";
        enddate = enddate + " 23:59:59";

        string sql = string.Format(@"
			declare @startdate datetime = convert(datetime, '{0}', 103)
            declare @enddate datetime = convert(datetime, '{1}', 103)
            declare @table table (
				thang int,
				ngay_motokhai datetime,
				khachhang nvarchar(100),
				so_inv nvarchar(100),
				nguoilap nvarchar(100),
				ncu nvarchar(100),
				po nvarchar(100),
				nvlapPO nvarchar(100),
				cbm decimal(18,3),
				discount decimal(18,2),
				soluong int,
				trigia decimal(18,2),
				c_nhapxuat_id varchar(32),
				md_quocgia_id varchar(32),
				md_sanpham_id varchar(32)
			)

			insert into @table
			select civ.thang,
			civ.ngay_motokhai,
			kh.ma_dtkd as khachhang,
			civ.so_inv,
			civ.nguoilap,
			ncu.ma_dtkd as ncu, 
			dh.sochungtu as po,
			dh.nguoilap as nvlapPO,
			cdiv.v2 as cbm,
			dh.discount,
			cdiv.soluong,
			civ.trigia,
			cdiv.c_nhapxuat_id,
			kh.md_quocgia_id,
			cdiv.md_sanpham_id
			from (
				select 
					civ.c_packinginvoice_id,
					datepart(m, civ.ngay_motokhai) as thang,
					civ.ngay_motokhai,
					civ.md_doitackinhdoanh_id as khachhangid,
					civ.so_inv,  
					civ.totalgross as trigia,
					civ.nguoitao as nguoilap
				from c_packinginvoice civ
				where 
					civ.md_trangthai_id <> 'SOANTHAO'
					and civ.ngay_motokhai >= @startdate
					and civ.ngay_motokhai <= @enddate
			)civ
			inner join c_dongpklinv cdiv on civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
			inner join c_donhang dh on cdiv.c_donhang_id = dh.c_donhang_id
			inner join md_doitackinhdoanh kh on civ.khachhangid = kh.md_doitackinhdoanh_id
			inner join md_doitackinhdoanh ncu on cdiv.nhacungungid = ncu.md_doitackinhdoanh_id
			--------------------------------------
			declare @tblNCC table (
				so_inv nvarchar(100),
				ncu nvarchar(100)
			)
			insert into @tblNCC
			select distinct A.so_inv, A.ncu
			from @table A
			declare @tblNCCGroup table (
				so_inv nvarchar(100),
				ncu nvarchar(MAX)
			)
			insert into @tblNCCGroup
			select distinct A.so_inv, 
			(
			  SELECT isnull(T.ncu + ',' + char(10),'')
			  FROM @tblNCC T
			  WHERE A.so_inv = T.so_inv
			  FOR XML PATH(''),type
			).value('.','varchar(max)') as ncu
			from @tblNCC A
			--------------------------------------
			declare @tblPO table (
				so_inv nvarchar(100),
				po nvarchar(100),
				nvlapPO nvarchar(100),
				discount decimal(18,2)
			)
			insert into @tblPO
			select distinct A.so_inv, A.po, A.nvlapPO, A.discount
			from @table A
			declare @tblPOGroup table (
				so_inv nvarchar(100),
				po nvarchar(MAX),
				nvlapPO nvarchar(MAX),
				discount nvarchar(1000)
			)
			insert into @tblPOGroup
			select distinct A.so_inv, 
			(
				SELECT isnull(T.po + ',' + char(10),'')
				FROM @tblPO T
				WHERE A.so_inv = T.so_inv
				FOR XML PATH(''), type
			).value('.','varchar(max)') as po,
			(
				SELECT isnull(T.nvlapPO + ',' + char(10),'')
				FROM @tblPO T
				WHERE A.so_inv = T.so_inv
				FOR XML PATH(''), type
			).value('.','varchar(max)') as nvlapPO,
			(
				SELECT isnull(convert(varchar(10), T.discount) + char(10),'')
				FROM @tblPO T
				WHERE A.so_inv = T.so_inv
				FOR XML PATH(''), type
			).value('.','varchar(max)') as discount
			from @tblPO A
			--------------------------------------
			declare @tblHeHang table (
				so_inv nvarchar(100),
				hehang varchar(3)
			)
			insert into @tblHeHang
			select distinct A.so_inv, SUBSTRING(sp.ma_sanpham,0,3) as hehang 
			from @table A
			inner join md_sanpham sp on A.md_sanpham_id = sp.md_sanpham_id
			declare @tblHeHangGroup table (
				so_inv nvarchar(100),
				hehang nvarchar(MAX)
			)
			insert into @tblHeHangGroup
			select distinct A.so_inv, 
			(
			  SELECT isnull(T.hehang + ',','')
			  FROM @tblHeHang T
			  WHERE A.so_inv = T.so_inv
			  FOR XML PATH(''), type
			).value('.','varchar(max)') as hehang
			from @tblHeHang A
			--------------------------------------
			declare @tbl2 table (
				thang int,
				ngay_motokhai datetime,
				khachhang nvarchar(150),
				ten_quocgia nvarchar(150),
				so_inv nvarchar(150),
				nguoilap nvarchar(150),
				cbm decimal(18,3),
				soluong int,
				trigia decimal(18,2),
				c_nhapxuat_id varchar(32),
				cont20 int,
				cont40 int,
				cont45 int,
				cont40hc int,
				contle int
			)

			insert into @tbl2
			select A.thang, A.ngay_motokhai, A.khachhang, qg.ten_quocgia, A.so_inv,
			A.nguoilap, A.cbm, A.soluong, A.trigia, A.c_nhapxuat_id,
			case when cnx.loaicont='CONT20' then cnx.socontainer else 0 end as cont20,
			case when cnx.loaicont='CONT40' then cnx.socontainer else 0 end as cont40,
			case when cnx.loaicont='CONT45' then cnx.socontainer else 0 end as cont45,
			case when cnx.loaicont='40HC' then cnx.socontainer else 0 end as cont40hc,
			case when cnx.loaicont='LE' then cnx.socontainer else 0 end as contle
			from @table A
			left join md_quocgia qg on qg.md_quocgia_id = A.md_quocgia_id
			left join c_nhapxuat cnx on cnx.c_nhapxuat_id = A.c_nhapxuat_id

			select A.*
			, ncc.ncu
			, po.po, po.nvlapPO, po.discount
			, hehang.hehang
			, B.cont20, B.cont40, B.cont40hc, B.cont45, B.contle
			from (
				select  
					A.thang, A.ngay_motokhai, A.khachhang, A.ten_quocgia, A.so_inv,
					A.nguoilap, SUM(A.cbm) as tongCBM, SUM(A.soluong) as tongSL, A.trigia
				from @tbl2 A
				group by  A.thang, A.ngay_motokhai, A.so_inv, A.nguoilap, A.trigia, A.ten_quocgia, A.khachhang
			)A
			inner join @tblNCCGroup ncc on A.so_inv = ncc.so_inv
			inner join @tblPOGroup po on A.so_inv = po.so_inv
			inner join @tblHeHangGroup hehang on A.so_inv = hehang.so_inv
			inner join (
				select 
					A.so_inv,
					SUM(A.cont20) as cont20,
					SUM(A.cont40) as cont40,
					SUM(A.cont40hc) as cont40hc,
					SUM(A.cont45) as cont45,
					SUM(A.contle) as contle
				from(
					select distinct A.so_inv, A.c_nhapxuat_id, A.cont20, A.cont40, A.cont40hc, A.cont45, A.contle from @tbl2 A
				)A
				group by A.so_inv
			)B on A.so_inv = B.so_inv
			order by A.ngay_motokhai, A.so_inv
        ", startdate, enddate);

        DataTable dt = mdbc.GetData(sql);
        if (dt.Rows.Count != 0)
        {
            this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
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
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 18);
        int row = export.SetHeaderAnco(hssfsheet, "BÁO CÁO DOANH SỐ - SỐ LƯỢNG CONT", tungay, denngay, false);

        #region Header Column
        int widthDF = 5000;
        List<ItemValue> lstHeader = new List<ItemValue>();
        var item = new ItemValue
        {
            item = "THÁNG",
            value = "thang",
            witdh = 2400
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NGÀY MTK",
            value = "ngay_motokhai",
            witdh = 3600
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
            item = "QUỐC GIA",
            value = "ten_quocgia",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "SỐ INVOICE",
            value = "so_inv",
            witdh = 8000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NGƯỜI LẬP",
            value = "nguoilap",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NHÀ CUNG ỨNG",
            value = "ncu",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "PO",
            value = "po",
            witdh = 10250
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "NV lập PO",
            value = "nvlapPO",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Hệ hàng (INV)",
            value = "hehang",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tổng (CBM) (INV)",
            value = "tongCBM",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Discount (P/O)",
            value = "discount",
            witdh = 3000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "Tổng SL (INV)",
            value = "tongSL",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TRỊ GIÁ",
            value = "trigia",
            witdh = 4000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "CONT 20",
            value = "cont20",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "CONT 40",
            value = "cont40",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "CONT 40HC",
            value = "cont40hc",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "CONT 45",
            value = "cont45",
            witdh = 2000
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "LẺ",
            value = "contle",
            witdh = 2000
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
                string[] arrDecimal = new string[] { "trigia" };
                string[] arrDate = new string[] { "ngay_motokhai" };
                string[] arrIntRight = new string[] { "cont20", "cont40", "cont45", "cont40hc", "contle", "tongSL" };
                string[] RemoveLastChar = new string[] { "hehang", "discount" };
				string[] Remove2LastChar = new string[] { "ncu", "po", "nvlapPO" };
                if (arrDecimal.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright2dec;
                }
                else if (arrIntRight.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(int.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright;
                }
                else if (lstHeader[j].value == "tongCBM")
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright3dec;
                }
                else if (lstHeader[j].value == "thang")
                {
                    cell.SetCellValue(int.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
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

                    if (RemoveLastChar.Contains(lstHeader[j].value))
                    {
                        str = str.Remove(str.Length - 1);
                    }
					else if (Remove2LastChar.Contains(lstHeader[j].value))
                    {
                        str = str.Remove(str.Length - 2);
                    } 
                    cell.SetCellValue(str);

                    if (lstHeader[j].value == "discount")
                        cell.CellStyle = export.bordercenter;
                    else
                        cell.CellStyle = export.border;
                }
            }
            row_t.HeightInPoints = height;
            row++;
        }
        int end_sum = row;

        IRow row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("Tổng cộng");
        row_total.GetCell(0).CellStyle = export.rightBold;
        export.MergedRegion(hssfsheet, row, 0, 9);
        CellRangeAddress cellRange09 = new CellRangeAddress(row, row, 0, 9);
        export.set_borderThin(cellRange09, "LRTB", hssfsheet);

        row_total.CreateCell(10).SetCellValue("");
        row_total.GetCell(10).CellStyle = export.border;
        row_total.CreateCell(11).SetCellValue("");
        row_total.GetCell(11).CellStyle = export.border;
        row_total.CreateCell(12).SetCellValue("");
        row_total.GetCell(12).CellStyle = export.border;
        row_total.CreateCell(13).SetCellFormula(string.Format("SUM(N{0}:N{1})", start_sum, end_sum));
        row_total.GetCell(13).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(14).SetCellFormula(string.Format("SUM(O{0}:O{1})", start_sum, end_sum));
        row_total.GetCell(14).CellStyle = export.borderrightBold;
        row_total.CreateCell(15).SetCellFormula(string.Format("SUM(P{0}:P{1})", start_sum, end_sum));
        row_total.GetCell(15).CellStyle = export.borderrightBold;
        row_total.CreateCell(16).SetCellFormula(string.Format("SUM(Q{0}:Q{1})", start_sum, end_sum));
        row_total.GetCell(16).CellStyle = export.borderrightBold;
        row_total.CreateCell(17).SetCellFormula(string.Format("SUM(R{0}:R{1})", start_sum, end_sum));
        row_total.GetCell(17).CellStyle = export.borderrightBold;
        row_total.CreateCell(18).SetCellFormula(string.Format("SUM(S{0}:S{1})", start_sum, end_sum));
        row_total.GetCell(18).CellStyle = export.borderrightBold;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        hssfsheet.PrintSetup.Landscape = true;
        String saveAsFileName = String.Format("DoanhSoSLCont-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
