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

public partial class ReportWizard_RptKhachHang_rpt_LietKeHangBanChay : System.Web.UI.Page
{
    #region cancel
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    string startdate = Request.QueryString["startdate"];
    //    string enddate = Request.QueryString["enddate"];
    //    startdate = startdate + " 00:00:00";
    //    enddate = enddate + " 23:59:59";
    //    string hehang_id = Request.QueryString["hehang_id"];
    //    if (String.IsNullOrEmpty(hehang_id))
    //    {
    //        hehang_id = "";
    //    }
    //    else
    //    {
    //        hehang_id = string.Format("and sp.md_chungloai_id = '{0}'", hehang_id);
    //    }

    //    string dtkd_id = Request.QueryString["dtkd_id"];
    //    if (String.IsNullOrEmpty(dtkd_id))
    //    {
    //        dtkd_id = "";
    //    }
    //    else
    //    {
    //        dtkd_id = string.Format("and dt.md_doitackinhdoanh_id = '{0}'", dtkd_id);
    //    }

    //    string thongketheo = Request.QueryString["thongketheo"];

    //    string sql = string.Format(@"
    //        declare @startdate datetime = convert(datetime, '{0}', 103);
    //        declare @enddate datetime = convert(datetime, '{1}', 103);
    //        declare @thongkeTheo nvarchar(4) = N'{2}';            

    //        if(@thongkeTheo = 'IN')
    //        begin
    //            select 
    //                (substring(sp.ma_sanpham, 1, 9) + substring(sp.ma_sanpham, 13, 22)) as makh,
				//	isnull(sum(isnull(cdiv.soluong, 0)), 0) as sl, 
    //                dt.ma_dtkd, 
    //                @startdate as tungay, 
    //                @enddate as denngay
			 //   from c_packinginvoice civ, c_dongpklinv cdiv, md_doitackinhdoanh dt, md_sanpham sp
			 //   where 
    //                civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
				//	and civ.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
				//	and cdiv.md_sanpham_id = sp.md_sanpham_id
				//	and civ.md_trangthai_id = 'HIEULUC'
				//	and civ.ngay_motokhai > = @startdate
				//	and civ.ngay_motokhai <= @enddate 
    //                {3}
    //                {4}
			 //   group by (substring(sp.ma_sanpham, 1, 9) + substring(sp.ma_sanpham, 13, 22)), dt.ma_dtkd
			 //   order by sum(cdiv.soluong) desc
    //        end
    //        else
    //        begin
    //            select 
    //                (substring(sp.ma_sanpham, 1, 9) + substring(sp.ma_sanpham, 13, 22)) as makh,
			 //       sum(cdiv.soluong) as sl, 
    //                dt.ma_dtkd, 
    //                @startdate as tungay, 
    //                @enddate as denngay
    //            from 
    //                c_donhang civ, c_dongdonhang cdiv, md_doitackinhdoanh dt, md_sanpham sp
    //            where 
    //                civ.c_donhang_id = cdiv.c_donhang_id
		  //          and civ.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
		  //          and cdiv.md_sanpham_id = sp.md_sanpham_id
		  //          and civ.md_trangthai_id = 'HIEULUC'
		  //          and civ.shipmenttime > = @startdate
		  //          and civ.shipmenttime <= @enddate 
    //                {3}
    //                {4}
    //            group by (substring(sp.ma_sanpham, 1, 9) + substring(sp.ma_sanpham, 13, 22)), dt.ma_dtkd
    //            order by sum(cdiv.soluong) desc
    //        end
    //    ", startdate, enddate, thongketheo, hehang_id, dtkd_id);

    //    DataTable dt = mdbc.GetData(sql);
    //    if (dt.Rows.Count != 0)
    //    {
    //        this.CreateWorkBookPO(dt, Request.QueryString["startdate"], Request.QueryString["enddate"]);
    //    }
    //    else
    //    {
    //        Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
    //    }
    //}

    //public void CreateWorkBookPO(DataTable dt, string tungay, string denngay)
    //{
    //    HSSFWorkbook hssfworkbook = new HSSFWorkbook();
    //    ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
    //    HSSFSheet hssfsheet = (HSSFSheet)s1;
    //    ExportExcel export = new ExportExcel(hssfworkbook, 0, 3);
    //    int row = export.SetHeaderAnco(hssfsheet, "LIỆT KÊ MẶT HÀNG BÁN CHẠY(THEO INVOICE)", tungay, denngay, true);

    //    #region Header Column
    //    int widthDF = 7000;
    //    List<ItemValue> lstHeader = new List<ItemValue>();
    //    var item = new ItemValue
    //    {
    //        item = "STT",
    //        value = "rowNum",
    //        witdh = 2000
    //    };
    //    lstHeader.Add(item);

    //    item = new ItemValue
    //    {
    //        item = "MÃ KH",
    //        value = "makh",
    //        witdh = widthDF
    //    };
    //    lstHeader.Add(item);

    //    item = new ItemValue
    //    {
    //        item = "SL",
    //        value = "sl",
    //        witdh = widthDF
    //    };
    //    lstHeader.Add(item);

    //    item = new ItemValue
    //    {
    //        item = "KHÁCH HÀNG",
    //        value = "ma_dtkd",
    //        witdh = 10000
    //    };
    //    lstHeader.Add(item);

    //    IRow rowColumnHeader = s1.CreateRow(row);
    //    rowColumnHeader.HeightInPoints = 40;
    //    for (int j = 0; j < lstHeader.Count; j++)
    //    {
    //        rowColumnHeader.CreateCell(j).SetCellValue(lstHeader[j].item);
    //        rowColumnHeader.GetCell(j).CellStyle = export.borderWrap;
    //        s1.SetColumnWidth(j, lstHeader[j].witdh);
    //    }
    //    row++;
    //    #endregion

    //    #region Set Table
    //    int start_sum = row + 1;
    //    string groupData = "";
    //    int countRG = 1;
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        IRow row_t = s1.CreateRow(row);
    //        float height = 25;
    //        if (groupData != dt.Rows[i]["makh"].ToString())
    //        {
    //            row_t.HeightInPoints = height;
    //            for (int j = 0; j < lstHeader.Count; j++)
    //            {
    //                var cell = row_t.CreateCell(j);
    //                if (lstHeader[j].value == "rowNum")
    //                {
    //                    cell.SetCellValue(countRG);
    //                    cell.CellStyle = export.bordercenterBold;
    //                }
    //                else if (lstHeader[j].value == "sl")
    //                {
    //                    var sumSL = dt.AsEnumerable().Where(s => s.Field<string>("makh") == groupData).Sum(x => x.Field<decimal>("sl"));
    //                    cell.SetCellValue(int.Parse(sumSL.ToString()));
    //                    cell.CellStyle = export.borderrightBold;
    //                }
    //                else if (lstHeader[j].value == "makh")
    //                {
    //                    groupData = dt.Rows[i][lstHeader[j].value].ToString();
    //                    cell.SetCellValue(groupData);
    //                    cell.CellStyle = export.borderleftBold;
    //                }
    //                else
    //                {
    //                    cell.SetCellValue("");
    //                    cell.CellStyle = export.bordercenter;
    //                }
    //            }
    //            countRG++;
    //            row++;
    //            row_t = s1.CreateRow(row);
    //        }
            
    //        for (int j = 0; j < lstHeader.Count; j++)
    //        {
    //            var cell = row_t.CreateCell(j);
    //            if (lstHeader[j].value == "sl")
    //            {
    //                cell.SetCellValue(int.Parse(dt.Rows[i][lstHeader[j].value].ToString()));
    //                cell.CellStyle = export.borderright;
    //            }
    //            else if (lstHeader[j].value == "makh")
    //            {
    //                string str = dt.Rows[i][lstHeader[j].value].ToString();
    //                float heightMAX = export.MeasureTextHeight(str, lstHeader[j].witdh);
    //                if (heightMAX > height)
    //                {
    //                    height = heightMAX;
    //                }
    //                cell.SetCellValue(str);
    //                cell.CellStyle = export.border;
    //            }
    //            else
    //            {
    //                cell.SetCellValue("");
    //                cell.CellStyle = export.bordercenter;
    //            }
    //        }
    //        row_t.HeightInPoints = height;
    //        row++;
    //    }
    //    int end_sum = row;
    //    #endregion

    //    hssfsheet = export.PrintExcel(hssfsheet, row);
    //    String saveAsFileName = String.Format("MatHangBanChayTheoINV-{0}.xls", DateTime.Now);
    //    export.SaveFile(hssfworkbook, saveAsFileName, Context);
    //}

    #endregion cancel

    public LinqDBDataContext db = new LinqDBDataContext();
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);
        string nameTemp = "(AD) Liệt kê mặt hàng bán chạy.xls";
        string nameRpt = "Liệt kê hàng bán chạy";
        string sql = CreateSql(context);

        inPDF = "2";
        var task = new System.Threading.Tasks.Task(() =>
        {
            viewReport(sql);
        });

        var compineF = Request.QueryString["compineF"];
        nameRpt = string.IsNullOrWhiteSpace(compineF) ? nameRpt + "-" + DateTime.Now.ToString("ddMMyy") : nameRpt;
        PrintAnco2.exportDataWithType(task, sql, inPDF, nameTemp, nameRpt, ReportViewer1, string.IsNullOrWhiteSpace(compineF), !string.IsNullOrWhiteSpace(compineF));
    }

    public void viewReport(String SqlQuery)
    {
        var tbl = ((DataSet)ReportViewer1.Report.DataSource).Tables[0];
        var maKH = "";
        var num = 1;
        foreach (DataRow row in tbl.Rows)
        {
            if (row["makh"].ToString() != maKH)
            {
                row["num"] = num;
                num++;
                maKH = row["makh"].ToString();
            }
        }
    }

    public string CreateSql(HttpContext context)
    {
        string startdate = Request.QueryString["startdate"];
        string enddate = Request.QueryString["enddate"];
        string hehang_id = Request.QueryString["hehang_id"];
        if (String.IsNullOrEmpty(hehang_id))
        {
            hehang_id = "";
        }
        else
        {
            hehang_id = string.Format("and sp.md_chungloai_id = '{0}'", hehang_id);
        }

        string dtkd_id = Request.QueryString["doitackinhdoanh_id"];
        if (String.IsNullOrEmpty(dtkd_id))
        {
            dtkd_id = "";
        }
        else
        {
            dtkd_id = string.Format("and dt.md_doitackinhdoanh_id = '{0}'", dtkd_id);
        }

        string thongketheo = Request.QueryString["thongketheo"];
        thongketheo = string.IsNullOrEmpty(thongketheo) | thongketheo == "IN" ? "INV" : "PO";

        string sql = string.Format(@"
            declare @tungay nvarchar(100) = '{0}';
            declare @denngay nvarchar(100) = '{1}';
            declare @startdate datetime = convert(datetime, @tungay + ' 00:00:00', 103);
            declare @enddate datetime = convert(datetime, @denngay + ' 23:59:59', 103);
            declare @thongkeTheo nvarchar(4) = N'{2}';            
            declare @tbl table (makh nvarchar(50), sl decimal(18, 0), ma_dtkd nvarchar(200))

            if(@thongkeTheo = 'INV')
            begin
                insert into @tbl
                select A.*
                from (
                    select 
                        (substring(sp.ma_sanpham, 1, 9) + substring(sp.ma_sanpham, 13, 22)) as makh,
        	            isnull(sum(isnull(cdiv.soluong, 0)), 0) as sl, 
                        dt.ma_dtkd
                    from 
                        c_packinginvoice civ, c_dongpklinv cdiv, md_doitackinhdoanh dt, md_sanpham sp
                    where 
                        civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
        	            and civ.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
        	            and cdiv.md_sanpham_id = sp.md_sanpham_id
        	            and civ.md_trangthai_id = 'HIEULUC'
        	            and civ.ngay_motokhai > = @startdate
        	            and civ.ngay_motokhai <= @enddate 
                        {3}
                        {4}
                    group by 
                        (substring(sp.ma_sanpham, 1, 9) + substring(sp.ma_sanpham, 13, 22)), dt.ma_dtkd
                )A
            end
            else
            begin
                insert into @tbl
                select A.*
                from (
                    select 
                        (substring(sp.ma_sanpham, 1, 9) + substring(sp.ma_sanpham, 13, 22)) as makh,
        	            isnull(sum(isnull(cdiv.soluong, 0)), 0) as sl, 
                        dt.ma_dtkd
                    from 
                        c_donhang civ, c_dongdonhang cdiv, md_doitackinhdoanh dt, md_sanpham sp
                    where 
                        civ.c_donhang_id = cdiv.c_donhang_id
        	            and civ.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
        	            and cdiv.md_sanpham_id = sp.md_sanpham_id
        	            and civ.md_trangthai_id = 'HIEULUC'
        	            and civ.shipmenttime > = @startdate
                        and civ.shipmenttime <= @enddate 
                        {3}
                        {4}
                    group by 
                        (substring(sp.ma_sanpham, 1, 9) + substring(sp.ma_sanpham, 13, 22)), dt.ma_dtkd
                )A
            end
            
            select 
                A.*, @tungay as tungay, @denngay as denngay, @thongkeTheo as tkt, 0 as num
            from (
                select tbl.*, (select sum(tbl2.sl) from @tbl tbl2 where tbl2.makh = tbl.makh) as tongsl
                from @tbl tbl
            )A
            order by A.tongsl desc, A.makh, A.sl desc
        ", startdate, enddate, thongketheo, hehang_id, dtkd_id);
        return sql;
    }
}
