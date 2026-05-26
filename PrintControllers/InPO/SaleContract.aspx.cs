using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using HSSFUtils;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.HSSF.Record.AutoFilter;
public partial class PrintControllers_SaleContract_Default : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();
    private String fmt0 = "#,##0", fmt0i0 = "#,##0.0", fmt0i00 = "#,##0.00", fmt0i000 = "#,##0.000";

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        {
            String c_donhang_id = Request.QueryString["c_donhang_id"];
            //-kiem tra co bao nhieu Khach Hang
            string check_dtkd = String.Format(@"select distinct dh.md_doitackinhdoanh_id
			from c_dongdonhang ddh
			left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id
			where N'{0}' like  N'%'+dh.c_donhang_id+'%'
			order by dh.md_doitackinhdoanh_id asc
			", c_donhang_id == null ? "" : c_donhang_id);
            //--
            DataTable dt_ck = mdbc.GetData(check_dtkd);
            if (dt_ck.Rows.Count > 1)
            {
                Response.Write("<h3>Đơn hàng không cùng Khách hàng</h3>");
            }
            else
            {
                decimal total_all = 0, total = 0;
                string[] c_id = c_donhang_id.Split(',');
                for (int i = 0; i < c_id.Length; i++)
                {
                    var pTang = (from p in db.c_phidonhangs where p.c_donhang_id.Equals(c_id[i]) && p.phitang.Equals(true) select p.phi).Sum();
                    var pGiam = (from p in db.c_phidonhangs where p.c_donhang_id.Equals(c_id[i]) && p.phitang.Equals(false) select p.phi).Sum();

                    decimal tang, giam;
                    tang = pTang == null ? 0 : pTang.Value;
                    giam = pGiam == null ? 0 : pGiam.Value;

                    double totalDB = (double)mdbc.ExecuteScalar(@"
                        declare @discount numeric(18, 6) = isnull((select discount from c_donhang where c_donhang_id = @c_donhang_id), 0)
                        select a.total -
                        cast(a.total * @discount / 100 as decimal(18, 2)) 
                        from (
	                        select sum(amount) as total
	                        from (
		                        select a.hehang, a.amount - round(a.amount * a.discount / 100, 2) as amount
		                        from (
			                        select a.hehang, a.discount, sum(a.amount) as amount
			                        from (
				                        select
					                        dh.soluong * (isnull(dh.giachuan, dh.giafob)) as amount,
					                        isnull(dh.discount, 0) as discount,
					                        isnull(dh.phi, 0) as phi,
					                        SUBSTRING(sp.ma_sanpham,0,3) as hehang
				                        from 
					                        c_dongdonhang dh
					                        left join md_sanpham sp on sp.md_sanpham_id = dh.md_sanpham_id
				                        where 
					                        c_donhang_id = @c_donhang_id
			                        )a
			                        group by hehang, discount
		                        )a
	                        )a
                        )a
                    "
                    , "@c_donhang_id"
                    , c_id[i]);

                    total = (decimal)totalDB;
                    total = total + tang - giam;
                    total_all += total;
                }
                
                String totalWord = MoneyToWord.ConvertMoneyToText(total_all.ToString()).Replace("Dollars", "");

                int j = totalWord.LastIndexOf("and");

                if (totalWord.Contains("Cents") | totalWord.Contains("Cent"))
                {
                    totalWord = totalWord.Replace("Cents", "").Replace("Cent", "");
                    totalWord = totalWord.Insert(totalWord.Length, "cents");
                }
                else
                {
                    totalWord = totalWord.Replace("Cents", "").Replace("Cent", "");
                }

                String select = String.Format(@"SELECT *, CASE
					WHEN B.sl_inner = 0
					THEN B.n_w_inner + B.t_thung_outer

					WHEN B.dvt_inner like N'%ctn%' and B.dvt_outer like N'%ctn%'
					THEN B.n_w_outer + B.t_thung_outer

					WHEN B.dvt_inner like N'%ctn%' and B.dvt_outer like N'%pal%'
					THEN B.g_w_inner + 20

					WHEN B.dvt_inner like N'%ctn%' and B.dvt_outer like N'%crt%'
					THEN B.g_w_inner + 5

					WHEN B.dvt_inner like N'%bun%' and B.dvt_outer like N'%crt%'
					THEN B.g_w_inner + 5

					WHEN B.dvt_inner like N'%bun%' and B.dvt_outer like N'%pal%'
					THEN B.g_w_inner + 20

					WHEN B.sl_inner = 0 and B.dvt_outer like N'%pal%'
					THEN B.g_w_inner + 25

					ELSE 0 END as g_w_outer
					FROM
					(SELECT *,
					CASE
					WHEN A.dvt_inner like N'%bun%'
					THEN A.n_w_inner + 0.5
					WHEN A.dvt_inner like N'%crt%'  
					THEN A.n_w_inner + 3
					WHEN A.dvt_inner like N'%ctn%'  
					THEN A.n_w_inner + t_thung_inner
					ELSE 0 END as g_w_inner
					FROM (select
					GETDATE() as ngaylap
					, dh.c_donhang_id
					, dh.sochungtu
					, dtkd.ten_dtkd
					, dtkd.diachi
					, dtkd.tel
					, dtkd.fax 
					, dh.portdischarge as cang_n
					, cx.ten_cangbien as cang_x
					, dh.shipmenttime
					, pmt.ten_paymentterm
					, sp.ma_sanpham
					, sp.mota_tiengviet
					, sp.mota_tienganh
					, ddh.soluong
					, isnull(ddh.giachuan, ddh.giafob) as giafob
                    , ddh.discount as disHH
					, dh.totalcbm
                    , dh.discount_hehang_value
					, convert(decimal(18,2) ,dh.discount) as discount
					, trl.ten_trongluong
					, dg.sl_outer
					, dg.sl_inner
					, dvt_outer.ten_dvt as dvt_outer
					, dvt_inner.ten_dvt as dvt_inner
					, dvt.ten_dvt
					, ddh.l1, ddh.w1, ddh.h1
					, ddh.l2, ddh.w2, ddh.h2, ddh.v2
					, case when dh.md_trongluong_id is not null then dg.sl_outer * (SELECT dbo.f_convertKgToPounds(sp.trongluong, dh.md_trongluong_id)) else 0 end as n_w_outer
					, case when dh.md_trongluong_id is not null then dg.sl_inner * (SELECT dbo.f_convertKgToPounds(sp.trongluong, dh.md_trongluong_id)) else 0 end as n_w_inner
					, (ddh.l2 + ddh.w2) * (ddh.w2 * ddh.h2) / 5400 as t_thung_outer
					, (ddh.l1 + ddh.w1) * (ddh.w1 * ddh.h1) / 5400 as t_thung_inner
					, '{1}' as money
                    , (select 'In favor of VINH GIA COMPANY LIMITED Account No. : ' + thongtin from md_nganhang where dh.md_nganhang_id = md_nganhang_id) as thongtinnganhang
					from c_dongdonhang ddh
					left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id
					left join md_sanpham sp on sp.md_sanpham_id = ddh.md_sanpham_id
					left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id
					left join md_paymentterm pmt on dh.md_paymentterm_id = pmt.md_paymentterm_id
					left join md_cangbien cx on cx.md_cangbien_id = dh.md_cangbien_id
					left join md_donggoi dg on dg.md_donggoi_id = ddh.md_donggoi_id
					left join md_donvitinh dvt_outer on dvt_outer.md_donvitinh_id = dg.dvt_outer
					left join md_donvitinh dvt_inner on dvt_inner.md_donvitinh_id = dg.dvt_inner
					left join md_donvitinhsanpham dvt on dvt.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id
					left join md_trongluong trl on trl.md_trongluong_id = dh.md_trongluong_id
					where N'{0}' like  N'%'+dh.c_donhang_id+'%'
					
					)A)B order by B.sochungtu asc, B.ma_sanpham asc

				", c_donhang_id == null ? "" : c_donhang_id, totalWord);

                DataTable dt = mdbc.GetData(select);

                if (dt.Rows.Count != 0)
                {
                    HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
                    String saveAsFileName = String.Format("SaleContract-{0}.xls", DateTime.Now);
                    this.SaveFile(hssfworkbook, saveAsFileName);
                }
                else
                {
                    Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
                }
            }
        }
        //catch (Exception ex)
        {
            //Response.Write(String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex.Message));
        }
    }

    public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");

        HSSFSheet hssfsheet = (HSSFSheet)s1;
        Excel_Format ex_fm = new Excel_Format(hssfworkbook);
        ICellStyle cellBold = ex_fm.getcell(12, true, true, "", "L", "T");
        //-- 
        ICellStyle cellHeader = ex_fm.getcell(22, true, true, "", "C", "T");
        //--
        ICellStyle celltext = ex_fm.getcell(12, false, true, "", "L", "T");
        //--
        ICellStyle rightBold = ex_fm.getcell(12, true, false, "", "R", "T");
        ICellStyle rightBoldSoLuong = ex_fm.getcell2(12, true, false, "", "R", "T", fmt0);
        //--
        ICellStyle right = ex_fm.getcell(12, false, false, "", "R", "T");
        //--
        ICellStyle leftBold = ex_fm.getcell(12, true, false, "", "L", "T");
        //--
        ICellStyle left = ex_fm.getcell(12, false, false, "", "L", "T");
        //--
        ICellStyle border = ex_fm.getcell(12, false, true, "LRBT", "L", "T");
        //--
        ICellStyle borderright = ex_fm.getcell(12, false, true, "LRBT", "R", "T");
        ICellStyle borderrightSoLuong = ex_fm.getcell2(12, false, true, "LRBT", "R", "T", fmt0);
        //--
        ICellStyle borderonlyleft = ex_fm.getcell(12, false, true, "L", "T", "T");
        //--
        ICellStyle borderWrap = ex_fm.getcell(12, true, true, "LRBT", "C", "T");
        ICellStyle borderWrapSTT = ex_fm.getcell(12, false, true, "LRBT", "C", "T");
        //--
        ICellStyle signBold = ex_fm.getcell(12, true, true, "", "C", "C");
        var number = ex_fm.getcell2(12, false, true, "LRBT", "R", "T", "#,##0");
        var numberBold = ex_fm.getcell2(12, true, true, "", "R", "T", "#,##0");

        var number2 = ex_fm.getcell2(12, false, true, "LRBT", "R", "T", "#,##0.00");
        var number2Bold = ex_fm.getcell2(12, true, true, "", "R", "T", "#,##0.00");

        //--
        int heigh = 22;
        int row = 0;
        //set A1 - A3
        string[] a = { "SALE CONTRACT" };
        for (int i = 0; i < a.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(0).SetCellValue(a[i]);
            s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 7));
            s1.GetRow(row).HeightInPoints = heigh;
            if (i <= 0)
            {
                s1.GetRow(row).HeightInPoints = 45;
                s1.GetRow(row).GetCell(0).CellStyle = cellHeader;
            }
            else
            {
                s1.GetRow(row).GetCell(0).CellStyle = celltext;
            }
            row++;
        }
        //--

        //set F4 - F5
        string[] b = { "No.:", "Date:" };
        string[] b_value = { "sochungtu", "ngaylap" };
        for (int i = 0; i < b.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(4).SetCellValue(b[i]);
            s1.GetRow(row).GetCell(4).CellStyle = cellBold;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 7));
            if (i == 1)
                s1.GetRow(row).CreateCell(5).SetCellValue(DateTime.Parse(dt.Rows[0][b_value[i]].ToString()).ToString("dd/MM/yyyy"));
            else if (b_value[i] != "")
                s1.GetRow(row).CreateCell(5).SetCellValue(dt.Rows[0][b_value[i]].ToString());
            else
                s1.GetRow(row).CreateCell(5).SetCellValue("");
            //s1.GetRow(row).GetCell(5).CellStyle = cellBold;
            s1.GetRow(row).GetCell(5).CellStyle = cellBold;
            s1.GetRow(row).HeightInPoints = heigh;
            row++;
        }

        //set A6 - A12
        string[] c = { "The Seller:", "The Buyer:", "The Payer:", "VINH GIA Company agrees to sale and the Buyer agrees to buy the goods under terms and conditions as follows:", "Customer's order no.:", "Item nos., description, packing, quantity, unit price and amount:" };
        string[] c_value = { "", "" };
        for (int i = 0; i < c.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(0).SetCellValue(c[i]);
            string rich_text = " {0} \n {1} \n Tel:{2} \n Fax{3}";
            if (i == 0)
                rich_text = "VINH GIA COMPANY LIMITED \n Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam\nTel: (84-235) 3567393\nFax: (84-235) 3567494";
            HSSFRichTextString rich = new HSSFRichTextString(String.Format(rich_text, dt.Rows[0]["ten_dtkd"].ToString(), dt.Rows[0]["diachi"].ToString(), dt.Rows[0]["tel"].ToString(), dt.Rows[0]["fax"].ToString()));
            if (i <= 2)
            {
                heigh = 95;
                if (i == 2)
                    heigh = 22;
                else
                {
                    s1.GetRow(row).CreateCell(2).SetCellValue(rich);
                    s1.GetRow(row).GetCell(2).CellStyle = cellBold;
                }
                s1.GetRow(row).GetCell(0).CellStyle = cellBold;

                s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 7));
                s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 1));

            }
            else
            {
                s1.GetRow(row).GetCell(0).CellStyle = celltext;
                s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 7));
                heigh = 22;
            }

            s1.GetRow(row).HeightInPoints = heigh;
            row++;
        }

        // set A13 - All
        // -- Header
        int cell = 0;
        string[] d = { "No", "VINH GIA Item No.:", "Customer Item No.", "Description of goods", "Quantity pcs,sets", "", "FOB price (USD)", "Amount (USD)" };
        IRow rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.HeightInPoints = 47;
        for (int i = 0; i < d.Count(); i++)
        {
            int with = 5500;
            rowColumnHeader.CreateCell(cell).SetCellValue(d[i]);
            rowColumnHeader.GetCell(i).CellStyle = borderWrap;
            if (new int[] { 4, 5 }.Contains(i))
                with = 3000;
            else if (i == 3)
                with = 10000;
            else if (i == 0)
            {
                with = 2000;
            }
            else if (i == 2)
            {
                with = 5600;
            }

            s1.SetColumnWidth(cell, with);
            cell++;
        }
        s1.AddMergedRegion(new CellRangeAddress(row, row, 4, 5));
        row++;
        // -- Details
        // create column
        string sochungtu = "";
        //thu tu tong cong // dem so dong hang cua tung sochungtu
        int count_row = 0, count_row_tt = 1;
        // tap hop so luong dong tong cong // tap hop cac vi tri cua tong cong
        string l_cbm = "";
        // create detail row

        var startRowHH = -1;
        var lstHH = new List<int>();
        var lstPO = new List<int>();
        string discount_hehang_value = "-1";
        List<Dictionary<string, object>> nnl = null;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (discount_hehang_value == "-1")
            {
                discount_hehang_value = dt.Rows[i]["discount_hehang_value"].ToString();
                nnl = string.IsNullOrEmpty(discount_hehang_value) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(discount_hehang_value);
            }

            var msp = dt.Rows[i]["ma_sanpham"].ToString();
            var chungloai = msp.Substring(0, 2);
            var chungloaiTT = i == dt.Rows.Count - 1 ? chungloai : dt.Rows[i + 1]["ma_sanpham"].ToString().Substring(0, 2);

            var soPO = dt.Rows[i]["sochungtu"].ToString();
            var soPOTT = i == dt.Rows.Count - 1 ? soPO : dt.Rows[i + 1]["sochungtu"].ToString();
            var soPOTR = i == 0 ? soPO : dt.Rows[i - 1]["sochungtu"].ToString();

            if (startRowHH == -1)
                startRowHH = row + 2;

            if (i == 0 | soPO != soPOTR) //ke dong sochungtu dau tien
            {
                s1.CreateRow(row).CreateCell(0).SetCellValue("P/I No. " + dt.Rows[i]["sochungtu"].ToString());
                s1.GetRow(row).CreateCell(4).SetCellValue("FOB " + dt.Rows[i]["cang_x"].ToString());
                CellRangeAddress cellRange03 = new CellRangeAddress(row, row, 0, 3);
                CellRangeAddress cellRange47 = new CellRangeAddress(row, row, 4, 7);
                s1.AddMergedRegion(cellRange03);
                s1.AddMergedRegion(cellRange47);
                s1.GetRow(row).GetCell(0).CellStyle = leftBold;
                s1.GetRow(row).GetCell(4).CellStyle = borderWrap;
                s1.GetRow(row).CreateCell(7).CellStyle = borderonlyleft;
                s1.GetRow(row).HeightInPoints = 22;
                ex_fm.set_border(cellRange03, "LRTB", hssfsheet);
                ex_fm.set_border(cellRange47, "LRTB", hssfsheet);
                //--
                row++;
                //--
                //s1.CreateRow(row).CreateCell(0).SetCellValue("P/I No. " + dt.Rows[i]["sochungtu"].ToString());
                //cellRange03 = new CellRangeAddress(row, row, 0, 3);
                //cellRange47 = new CellRangeAddress(row, row, 4, 7);
                //s1.AddMergedRegion(cellRange03);
                //s1.AddMergedRegion(cellRange47);
                //s1.GetRow(row).GetCell(0).CellStyle = leftBold;
                //s1.GetRow(row).CreateCell(7).CellStyle = borderonlyleft;
                //s1.GetRow(row).HeightInPoints = 22;
                sochungtu = dt.Rows[i]["sochungtu"].ToString();
                //ex_fm.set_border(cellRange03, "LRTB", hssfsheet);
                //ex_fm.set_border(cellRange47, "LRTB", hssfsheet);
                //row++;
            }
            else //ke dong sochungtu tiep theo
            {

            }

            string[] e_value = { "num", "ma_sanpham", "", "mota_tienganh", "soluong", "ten_dvt", "giafob", "" };
            if (dt.Rows[i]["sochungtu"].ToString() != "9999")
            {
                IRow row_t = s1.CreateRow(row); row_t.HeightInPoints = 60;
                //
                int cell_t = 0;
                for (int j = 0; j < e_value.Length; j++)
                {
                    if (j == 4 | j == 6)
                        row_t.CreateCell(cell_t).SetCellValue(double.Parse(dt.Rows[i][e_value[j]].ToString()));
                    else if (j == 7)
                        row_t.CreateCell(cell_t).CellFormula = String.Format("E{0} * G{0}", row + 1);
                    else if (e_value[j] == "")
                        row_t.CreateCell(cell_t).SetCellValue("");
                    else if (e_value[j] == "num")
                        row_t.CreateCell(cell_t).SetCellValue(i + 1);
                    else
                        row_t.CreateCell(cell_t).SetCellValue(dt.Rows[i][e_value[j]].ToString());
                    
                    row_t.GetCell(cell_t).CellStyle = border;
                    cell_t++;
                }

                s1.GetRow(row).GetCell(0).CellStyle = borderWrapSTT;
                s1.GetRow(row).GetCell(4).CellStyle = borderright;
                s1.GetRow(row).GetCell(6).CellStyle = borderright;
                s1.GetRow(row).GetCell(7).CellStyle = number2;

                row++;
                count_row++;
                count_row_tt++;

                if (chungloai != chungloaiTT | i == dt.Rows.Count - 1 | soPO != soPOTT)
                {
                    if (nnl != null)
                    {
                        string gthh = dt.Rows[i]["disHH"] + "";
                        gthh = string.IsNullOrEmpty(gthh) ? "0" : gthh;
                        var rowHeight = gthh == "0" ? 0 : 20;
                        var rowToTalHeHang = s1.CreateRow(row);
                        rowToTalHeHang.HeightInPoints = rowHeight;
                        rowToTalHeHang.CreateCell(3).SetCellValue(string.Format("Sub Total ({0}):", lstHH.Count + 1));
                        rowToTalHeHang.GetCell(3).CellStyle = leftBold;
                        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 1));
                        string fmlSum = string.Format(@"SUM(VNN{0}:VNN{1})", startRowHH, row);
                        rowToTalHeHang.CreateCell(4).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "E"));
                        rowToTalHeHang.GetCell(4).CellStyle = numberBold;
                        rowToTalHeHang.CreateCell(5).SetCellValue("pcs/sets");
                        rowToTalHeHang.GetCell(5).CellStyle = leftBold;
                        rowToTalHeHang.CreateCell(7).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "H"));
                        rowToTalHeHang.GetCell(7).CellStyle = number2Bold;
                        row++;
                        count_row++;
                        count_row_tt++;

                        var rowDisHeHang = s1.CreateRow(row);
                        rowDisHeHang.HeightInPoints = rowHeight;
                        rowDisHeHang.CreateCell(3).SetCellValue(string.Format(@"Discount ({0}%):", gthh));
                        rowDisHeHang.GetCell(3).CellStyle = leftBold;
                        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 1));
                        rowDisHeHang.CreateCell(7).CellFormula = string.Format("ROUND(H{0}*{1}/100, 2)", row, gthh);
                        rowDisHeHang.GetCell(7).CellStyle = number2Bold;
                        row++;
                        count_row++;
                        count_row_tt++;

                        var rowTotalHeHang = s1.CreateRow(row);
                        rowTotalHeHang.HeightInPoints = rowHeight;
                        rowTotalHeHang.CreateCell(3).SetCellValue(string.Format("Total ({0}):", lstHH.Count + 1));
                        rowTotalHeHang.GetCell(3).CellStyle = leftBold;
                        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 1));
                        rowTotalHeHang.CreateCell(4).CellFormula = string.Format("E{0}", row - 1);
                        rowTotalHeHang.GetCell(4).CellStyle = numberBold;
                        rowTotalHeHang.CreateCell(7).CellFormula = string.Format("H{0} - H{1}", row - 1, row);
                        rowTotalHeHang.GetCell(7).CellStyle = number2Bold;
                        row++;
                        count_row++;
                        count_row_tt++;

                        startRowHH = row + 1;
                        lstHH.Add(row);
                    }
                }

                if (soPO != soPOTT | i == dt.Rows.Count - 1)
                {
                    discount_hehang_value = "-1";
                    var discountPO = dt.Rows[i]["discount"] + "";
                    discountPO = discountPO.Replace(".00", "");

                    string sumQuantity = "", sumAmount = "";
                    if (nnl != null)
                    {
                        foreach (var iHH in lstHH)
                        {
                            sumQuantity += string.Format("E{0}+", iHH);
                            sumAmount += string.Format("H{0}+", iHH);
                        }
                        sumQuantity += "0";
                        sumAmount += "0";
                    }
                    else
                    {
                        sumQuantity += string.Format(@"SUM(E{0}:E{1})", startRowHH, row);
                        sumAmount += string.Format(@"SUM(H{0}:H{1})", startRowHH , row);
                    }
                    
                    //vi tri row dau tien
                    int m = row - count_row + 1, sub_row = 0, discount_row = 0;
                    //start tong cong 1
                    s1.CreateRow(row).CreateCell(3).SetCellValue(string.Format("Sub Total:", lstPO.Count + 1));
                    s1.GetRow(row).CreateCell(4).CellFormula = string.Format("{0}", sumQuantity);
                    s1.GetRow(row).CreateCell(7).CellFormula = string.Format("{0}", sumAmount);
                    s1.GetRow(row).CreateCell(14).SetCellValue(discountPO);
                    s1.SetColumnWidth(14, 0);
                    // style
                    s1.GetRow(row).GetCell(3).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(4).CellStyle = numberBold;
                    s1.GetRow(row).GetCell(7).CellStyle = number2Bold;

                    row++;
                    l_cbm += row + ",";
                    sub_row = row;
                    //end

                    //start giam gia
                    m = row - count_row + 1;

                    s1.CreateRow(row).CreateCell(3).SetCellValue("Discount PO (" + discountPO + "%):");
                    s1.GetRow(row).CreateCell(7).CellFormula = String.Format("ROUND(H{0}*O{1}/100,2)", row, row);
                    s1.GetRow(row).GetCell(3).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(7).CellStyle = number2Bold;

                    if (discountPO != "0" & !string.IsNullOrEmpty(discountPO))
                    {
                        s1.GetRow(row).HeightInPoints = 22;
                    }
                    else
                    {
                        s1.GetRow(row).HeightInPoints = 0;
                    }
                    row++;
                    discount_row = row;
                    //end

                    //start phi cong tru
                    string chuoi_ct = "0";
                    var phidonhang = db.c_phidonhangs.Where(s => s.c_donhang_id == dt.Rows[i]["c_donhang_id"].ToString()).ToList();
                    if (phidonhang.Count > 0)
                    {
                        string vitri_ct = "-0";
                        var loaiPhi = phidonhang.FirstOrDefault().phitang;
                        if (phidonhang.Count == 1)
                        {
                            phidonhang.Add(new c_phidonhang()
                            {
                                phitang = !loaiPhi,
                                phi = 0,
                            });
                        }

                        foreach (var pdh in phidonhang.OrderByDescending(s => s.phitang))
                        {
                            //start phi cong va tru
                            m = row - count_row + 1;
                            string plus = "+";
                            int check_plus = 0;
                            if (pdh.phitang != true)
                            {
                                plus = "-";
                                check_plus = 1;
                            }

                            s1.CreateRow(row).CreateCell(3).SetCellValue("(" + plus + ")" + pdh.mota + ":");
                            s1.GetRow(row).CreateCell(7).SetCellValue((double)pdh.phi);
                            s1.GetRow(row).CreateCell(14).SetCellValue(check_plus);

                            s1.GetRow(row).GetCell(3).CellStyle = leftBold;
                            s1.GetRow(row).GetCell(7).CellStyle = number2Bold;
                            s1.GetRow(row).HeightInPoints = pdh.phi > 0 ? 22 : 0;
                            row++;
                            vitri_ct += plus + "H" + row;
                            //end    
                        }
                    }
                    else
                    {
                        //start phi cong
                        m = row - count_row + 1;
                        s1.CreateRow(row).CreateCell(3).SetCellValue("(+):");
                        s1.GetRow(row).CreateCell(7).SetCellValue(0);
                        s1.GetRow(row).CreateCell(14).SetCellValue(0);

                        s1.GetRow(row).GetCell(3).CellStyle = leftBold;
                        s1.GetRow(row).GetCell(7).CellStyle = number2Bold;
                        s1.GetRow(row).HeightInPoints = 0;
                        row++;
                        chuoi_ct += "+G" + row;
                        //end

                        //start phi tru
                        m = row - count_row + 1;
                        s1.CreateRow(row).CreateCell(3).SetCellValue("(-):");
                        s1.GetRow(row).CreateCell(7).SetCellValue(0);
                        s1.GetRow(row).CreateCell(14).SetCellValue(0);

                        s1.GetRow(row).GetCell(3).CellStyle = leftBold;
                        s1.GetRow(row).GetCell(7).CellStyle = number2Bold;
                        s1.GetRow(row).HeightInPoints = 0;
                        row++;
                        chuoi_ct += "-G" + row;
                        //end
                    }

                    //end

                    //start tong cong 2

                    m = row - count_row + 1;
                    string sumToTalPO = "";
                    for (var iHH = 0; iHH < lstHH.Count; iHH++)
                    {
                        var rowHH = lstHH[iHH];
                        sumToTalPO += string.Format(@"G{0}+", rowHH + 1);
                    }
                    sumToTalPO += "0";
                    s1.CreateRow(row).CreateCell(3).SetCellValue(string.Format("Total ({0}):", lstPO.Count + 1));
                    s1.GetRow(row).CreateCell(4).CellFormula = string.Format("E{0}", sub_row);
                    s1.GetRow(row).CreateCell(7).CellFormula = string.Format("H{0}-H{1}+H{2}-H{3}", sub_row, sub_row + 1, sub_row + 2, sub_row + 3);
                    // style
                    s1.GetRow(row).GetCell(3).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(4).CellStyle = numberBold;
                    s1.GetRow(row).GetCell(7).CellStyle = number2Bold;
                    s1.GetRow(row).HeightInPoints = 22;
                    row++;
                    lstPO.Add(row);
                    lstHH.Clear();
                    startRowHH = -1;
                    //end
                }
            }
        }
        //tru so dong du
        string totalTxt = "", sumQuantityAll = "", sumAmountAll = "";
        for (var i = 0; i < lstPO.Count; i++)
        {
            var rowPO = lstPO[i];
            totalTxt += string.Format("({0})+", i + 1);
            sumQuantityAll += string.Format(@"E{0}+", rowPO);
            sumAmountAll += string.Format(@"H{0}+", rowPO);
        }
        sumQuantityAll += "0";
        sumAmountAll += "0";
        totalTxt = totalTxt.Length > 0 ? totalTxt.Substring(0, totalTxt.Length - 1) : "";

        s1.CreateRow(row).CreateCell(3).SetCellValue(string.Format(@"Total {0}:", totalTxt));
        s1.GetRow(row).GetCell(3).CellStyle = leftBold;
        s1.GetRow(row).CreateCell(4).CellFormula = string.Format(sumQuantityAll);
        s1.GetRow(row).GetCell(4).CellStyle = numberBold;
        s1.GetRow(row).CreateCell(7).CellFormula = string.Format(sumAmountAll);
        s1.GetRow(row).GetCell(7).CellStyle = number2Bold;

        s1.GetRow(row).HeightInPoints = 22;
        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Say:USD " + dt.Rows[0]["money"].ToString().Replace("Dollars", ""));
        s1.GetRow(row).GetCell(0).CellStyle = rightBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 7));
        row++;
        //
        s1.CreateRow(row).CreateCell(0).SetCellValue("(10% more or less in quantity and amount are acceptable).");
        s1.GetRow(row).GetCell(0).CellStyle = right;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 7));
        row++;
        //end

        //set A6 - A12
        string[] f = { "Port of loading:", "Port of discharge:", "Shipment Time:", "Payment term:" };
        string[] f_value = { "cang_x", "cang_n", "shipmenttime", "ten_paymentterm" };
        for (int i = 0; i < f.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(0).SetCellValue(f[i]);
            heigh = 22;
            if (i == 3)
            {
                heigh = 95;
                string thongtinNH = dt.Rows[0]["ten_paymentterm"].ToString() + Environment.NewLine + dt.Rows[0]["thongtinnganhang"].ToString();
                s1.GetRow(row).CreateCell(2).SetCellValue(thongtinNH);
            }
            else
            {
                s1.GetRow(row).CreateCell(2).SetCellValue(dt.Rows[0][f_value[i]].ToString());
            }
            s1.GetRow(row).GetCell(0).CellStyle = celltext;
            s1.GetRow(row).GetCell(2).CellStyle = celltext;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 1));
            s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 7));
            s1.GetRow(row).HeightInPoints = heigh;
            row++;
        }
        //
        s1.CreateRow(row).CreateCell(0).SetCellValue("This agreement comes into effect from the signing date.");
        s1.GetRow(row).GetCell(0).CellStyle = left;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 7));
        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("THE BUYER");
        s1.GetRow(row).CreateCell(5).SetCellValue("THE SELLER");
        s1.GetRow(row).GetCell(0).CellStyle = signBold;
        s1.GetRow(row).GetCell(5).CellStyle = signBold;
        s1.GetRow(row).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 3));
        s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 7));
        row++;


        #region Format Print Excel

        s1.PrintSetup.PaperSize = (short)PaperSize.A4;
        s1.FitToPage = true;
        s1.PrintSetup.FitWidth = 1;
        s1.PrintSetup.FitHeight = 0;
        s1.SetMargin(MarginType.TopMargin, 0.75);
        s1.SetMargin(MarginType.BottomMargin, 0.75);
        s1.SetMargin(MarginType.LeftMargin, 0.23);
        s1.SetMargin(MarginType.RightMargin, 0.23);
        s1.SetMargin(MarginType.HeaderMargin, 0.31);
        s1.SetMargin(MarginType.FooterMargin, 0.31);
        hssfworkbook.SetPrintArea(
            hssfworkbook.GetSheetIndex(s1.SheetName), //sheet index
            0, //start column
            7, //end column
            0, //start row
            row + 1
        );
        #endregion
        return hssfworkbook;
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName)
    {
        MemoryStream exportData = new MemoryStream();
        hsswb.Write(exportData);

        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
        Response.Clear();
        Response.BinaryWrite(exportData.GetBuffer());
        Response.End();
    }
}
