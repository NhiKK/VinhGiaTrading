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

public partial class ReportWizard_RptKhachHang_rpt_DoanhSoSLContCBMTheoNCU : System.Web.UI.Page
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

        declare @tbl table (
			khachhang nvarchar(150),
			so_dsdh nvarchar(150),
			so_po nvarchar(150),
			ncu nvarchar(150),
			so_inv nvarchar(150),
			trigia decimal(18,2),
			trgiaNCC decimal(18,2),
			c_nhapxuat_id varchar(32),
			cont20 nvarchar(150),
			cont40 nvarchar(150),
			cont45 nvarchar(150),
			cont40hc nvarchar(150),
			contle nvarchar(150),
			ghichuCBM decimal(18,3) 
		)

		insert into @tbl
		select
			kh.ma_dtkd as khachhang,
			dsdh.sochungtu as so_dsdh,
			dsdh.so_po,
			ncu.ma_dtkd as ncu,
			civ.so_inv,
			civ.trigia,
			cdiv.thanhtien as trgiaNCC,
			cnx.c_nhapxuat_id,
			case when cnx.loaicont='CONT20' then cnx.container else '0' end as cont20,
			case when cnx.loaicont='CONT40' then cnx.container else '0' end as cont40,
			case when cnx.loaicont='CONT45' then cnx.container else '0' end as cont45,
			case when cnx.loaicont='40HC' then cnx.container else '0' end as cont40hc,
			case when cnx.loaicont='LE' then cnx.container else '0' end as contle,
			((cdiv.v2/cdiv.soluong_ddh) * cdiv.soluong) as ghichuCBM
		from (
			select 
				civ.c_packinginvoice_id,
				civ.md_doitackinhdoanh_id as khachhangid,
				civ.so_inv,  
				civ.totalgross as trigia
			from c_packinginvoice civ
			where 
				civ.md_trangthai_id <> 'SOANTHAO'
				and civ.ngay_motokhai between @startdate and @enddate
		)civ
		inner join c_dongpklinv cdiv on civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
		inner join c_danhsachdathang dsdh on cdiv.c_danhsachdathang_id = dsdh.c_danhsachdathang_id
		inner join md_doitackinhdoanh kh on civ.khachhangid = kh.md_doitackinhdoanh_id
		inner join md_doitackinhdoanh ncu on cdiv.nhacungungid = ncu.md_doitackinhdoanh_id
		inner join c_nhapxuat cnx on cnx.c_nhapxuat_id = cdiv.c_nhapxuat_id


		select 
			A.khachhang, A.so_dsdh, A.so_po, A.ncu,
			A.so_inv, A.trigia, A.trgiaNCC,
			B.cont20, B.cont40, B.cont40hc, B.cont45, B.contle, A.ghichuCBM
		from (
			select 
				A.khachhang, A.so_dsdh, A.so_po, A.ncu,
				A.so_inv, A.trigia, sum(A.trgiaNCC) as trgiaNCC, SUM(A.ghichuCBM) as ghichuCBM
			from @tbl A
			group by A.so_inv, A.khachhang, A.so_dsdh, a.so_po, A.ncu, A.trigia
		)A 
		inner join (
			select 
				A.so_inv,
				sum(A.cont20) as cont20,
				sum(A.cont40) as cont40,
				sum(A.cont40hc) as cont40hc,
				sum(A.cont45) as cont45,
				sum(A.contle) as contle
			from(
				select distinct A.so_inv, A.c_nhapxuat_id,
				(case when isnull(A.cont20,'0') = '0' then 0 else 1 end) as cont20, 
				(case when isnull(A.cont40,'0') = '0' then 0 else 1 end) as cont40, 
				(case when isnull(A.cont40hc,'0') = '0' then 0 else 1 end) as cont40hc,
				(case when isnull(A.cont45,'0') = '0' then 0 else 1 end) as cont45, 
				(case when isnull(A.contle,'0') = '0' then 0 else 1 end) as contle
				from @tbl A
			)A
			group by A.so_inv
		)B on A.so_inv = B.so_inv
		order by A.so_inv, A.so_po, A.ncu, A.so_dsdh
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
        ExportExcel export = new ExportExcel(hssfworkbook, 0, 13);
        int row = export.SetHeaderAnco(hssfsheet, "BÁO CÁO DOANH SỐ - SỐ LƯỢNG CONTAINER THEO NHÀ CUNG ỨNG", tungay, denngay, true);

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
            value = "khachhang",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "DSDH",
            value = "so_dsdh",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "PO",
            value = "so_po",
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
            item = "SỐ INV",
            value = "so_inv",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TRỊ GIÁ INV",
            value = "trigia",
            witdh = widthDF
        };
        lstHeader.Add(item);

        item = new ItemValue
        {
            item = "TRỊ GIÁ INV THEO NCU",
            value = "trgiaNCC",
            witdh = widthDF
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

        item = new ItemValue
        {
            item = "Ghi chú CBM",
            value = "ghichuCBM",
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
        string so_inv = "", so_po = "", ncu = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row_t = s1.CreateRow(row);
            float height = 25;
            string so_invval = dt.Rows[i]["so_inv"].ToString();
            string so_poval = dt.Rows[i]["so_po"].ToString();
            string ncuval = dt.Rows[i]["ncu"].ToString();
            for (int j = 0; j < lstHeader.Count; j++)
            {
                var cell = row_t.CreateCell(j);
                string[] arrDecimal = new string[] { "trigia", "trgiaNCC" };
                string[] arrIntRight = new string[] { "cont20", "cont40", "cont45", "cont40hc", "contle" };
                string[] arrDecimal3 = new string[] { "ghichuCBM" };
                if (lstHeader[j].value == "rowNum")
                {
                    cell.SetCellValue(i + 1);
                    cell.CellStyle = export.bordercenter;
                }
                else if (arrDecimal.Contains(lstHeader[j].value))
                {
                    cell.SetCellValue(double.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
                    cell.CellStyle = export.borderright2dec;
                }
                else if (arrDecimal3.Contains(lstHeader[j].value))
                {
                    double cbm = double.Parse(dt.Rows[i][lstHeader[j].value].ToString());
                    if (so_invval == so_inv & so_poval == so_po)
                    {
                        if (ncuval != ncu)
                        {
                            double cbmprv = double.Parse(dt.Rows[i - 1][lstHeader[j].value].ToString());
                            s1.GetRow(row - 1).GetCell(j).SetCellValue(cbmprv);
                            cell.SetCellValue(cbm);
                        }
                    }
                    cell.CellStyle = export.borderright3dec;
                }
                else if (arrIntRight.Contains(lstHeader[j].value))
                {
                    int socont = int.Parse(dt.Rows[i][lstHeader[j].value].ToString());
                    if (so_invval == so_inv)
                    {
                        socont = 0;
                    }
					
					if (so_invval == so_inv & so_poval == so_po)
                    {
                        if (ncuval != ncu)
                        {
							s1.GetRow(row - 1).GetCell(j).SetCellValue(0);
						}
					}
                    cell.SetCellValue(socont);
                    cell.CellStyle = export.borderright;
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

            so_inv = so_invval;
            so_po = so_poval;
            ncu = ncuval;
        }
        int end_sum = row;

        IRow row_total = s1.CreateRow(row);
        row_total.CreateCell(0).SetCellValue("Tổng cộng");
        row_total.GetCell(0).CellStyle = export.borderrightBold;
        export.MergedRegion(hssfsheet, row, 0, 5);
        CellRangeAddress cellRange06 = new CellRangeAddress(row, row, 0, 5);
        export.set_borderThin(cellRange06, "LRTB", hssfsheet);

        row_total.CreateCell(6).SetCellValue("");
        row_total.GetCell(6).CellStyle = export.border;
        row_total.CreateCell(7).SetCellFormula(string.Format("SUM(H{0}:H{1})", start_sum, end_sum));
        row_total.GetCell(7).CellStyle = export.borderrightBold2dec;
        row_total.CreateCell(8).SetCellFormula(string.Format("SUM(I{0}:I{1})", start_sum, end_sum));
        row_total.GetCell(8).CellStyle = export.borderrightBold;
        row_total.CreateCell(9).SetCellFormula(string.Format("SUM(J{0}:J{1})", start_sum, end_sum));
        row_total.GetCell(9).CellStyle = export.borderrightBold;
        row_total.CreateCell(10).SetCellFormula(string.Format("SUM(K{0}:K{1})", start_sum, end_sum));
        row_total.GetCell(10).CellStyle = export.borderrightBold;
        row_total.CreateCell(11).SetCellFormula(string.Format("SUM(L{0}:L{1})", start_sum, end_sum));
        row_total.GetCell(11).CellStyle = export.borderrightBold;
        row_total.CreateCell(12).SetCellFormula(string.Format("SUM(M{0}:M{1})", start_sum, end_sum));
        row_total.GetCell(12).CellStyle = export.borderrightBold;
        row_total.CreateCell(13).SetCellValue("");
        row_total.GetCell(13).CellStyle = export.borderrightBold;
        row_total.HeightInPoints = 30;
        #endregion

        hssfsheet = export.PrintExcel(hssfsheet, row);
        String saveAsFileName = String.Format("DoanhSoSLContCBMTheoNCU-{0}.xls", DateTime.Now);
        export.SaveFile(hssfworkbook, saveAsFileName, Context);
    }
}
