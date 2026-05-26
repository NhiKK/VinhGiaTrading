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
public partial class PrintControllers_Invoice_Default : System.Web.UI.Page
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
                    var pdgvc = db.c_donhangs.Where(s => s.c_donhang_id.Equals(c_id[i]) & s.cpdg_vuotchuan != null).ToList().Sum(s => s.cpdg_vuotchuan.GetValueOrDefault(0));
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
                    total = total + tang - giam + pdgvc;
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

                //-- Lay sochungtu
                string soct_th = "";
                string count_sct = String.Format(@"select distinct dh.sochungtu
				from c_dongdonhang ddh
				left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id
				where N'{0}' like  N'%'+dh.c_donhang_id+'%'
				order by dh.sochungtu asc
				", c_donhang_id == null ? "" : c_donhang_id);
                DataTable dt_ct = mdbc.GetData(count_sct);
                for (int i = 0; i < dt_ct.Rows.Count; i++)
                {
                    if (soct_th == "")
                        soct_th = "AS PER PROFORMA INVOICE NO.: " + dt_ct.Rows[i][0];
                    else
                        soct_th += ", " + dt_ct.Rows[i][0];
                }

                //-- Lay chungloai
                string cl_th = "";
                string count_cl = String.Format(@"select distinct cl.ta_ngan
				from c_dongdonhang ddh
				left join md_sanpham sp on sp.md_sanpham_id = ddh.md_sanpham_id
				left join md_chungloai cl on cl.md_chungloai_id = sp.md_chungloai_id
				left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id
				where N'{0}' like  N'%'+dh.c_donhang_id+'%'
				order by cl.ta_ngan asc
				", c_donhang_id == null ? "" : c_donhang_id);
                DataTable dt_cl = mdbc.GetData(count_cl);
                for (int i = 0; i < dt_cl.Rows.Count; i++)
                {
                    if (cl_th == "")
                        cl_th = "" + dt_cl.Rows[i][0];
                    else
                        cl_th += ", " + dt_cl.Rows[i][0];
                }

                String select = String.Format(@"
				SELECT B.*
				FROM (select
				GETDATE() as ngaylap
				, dh.c_donhang_id
				, dh.sochungtu
                , isnull(dh.cpdg_vuotchuan, 0) as cpdg_vuotchuan
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
                , (select 'VINH GIA COMPANY LIMITED Account No. : ' + thongtin from md_nganhang where dh.md_nganhang_id = md_nganhang_id) as thongtinnganhang
				, '{2}' as sochungtu_th
				, '{3}' as chungloai_th
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
				
				)B order by B.sochungtu asc, B.ma_sanpham asc
				", c_donhang_id == null ? "" : c_donhang_id, totalWord, soct_th, cl_th.ToUpper());

                DataTable dt = mdbc.GetData(select);
                if (dt.Rows.Count != 0)
                {
                    HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
                    String saveAsFileName = String.Format("Invoice-{0}.xls", DateTime.Now);
                    this.SaveFile(hssfworkbook, saveAsFileName);
                    Response.Write(select);
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

        IPrintSetup print = s1.PrintSetup;
        print.PaperSize = (short)PaperSize.A4;
        print.Scale = (short)80;
        print.FitWidth = (short)1;
        print.FitHeight = (short)0;

        s1.SetColumnWidth(0, 5500);
        s1.SetColumnWidth(1, 3500);
        s1.SetColumnWidth(2, 9500);
        s1.SetColumnWidth(3, 3000);
        s1.SetColumnWidth(4, 3000);
        s1.SetColumnWidth(5, 3800);
        s1.SetColumnWidth(6, 4000);
        s1.SetColumnWidth(8, 0);
        s1.SetColumnWidth(11, 0);

        Excel_Format ex_fm = new Excel_Format(hssfworkbook);
        ICellStyle cellBold = ex_fm.getcell(12, true, true, "", "L", "T");
        //-- 
        ICellStyle cellHeader = ex_fm.getcell(18, true, true, "", "C", "T");
        //-- 
        ICellStyle cellHeader_n = ex_fm.getcell(12, false, true, "", "C", "T");
        //--
        ICellStyle celltext = ex_fm.getcell(12, false, true, "", "L", "T");
        //--
        ICellStyle rightBold = ex_fm.getcell(12, true, false, "", "R", "T");
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
        //--
        ICellStyle borderonlyleft = ex_fm.getcell(12, false, true, "L", "T", "T");
        //--
        ICellStyle borderWrap = ex_fm.getcell(12, true, true, "LRBT", "C", "T");
        //--
        ICellStyle signBold = ex_fm.getcell(12, true, true, "", "C", "C");
        //--
        var number2Bold = ex_fm.getcell2(12, true, true, "", "R", "T", "#,##0.00");
        // Cell Style 
        var styleCenter18Bold = ex_fm.getcell2(18, true, true, "", "C", "T", "");
        var styleCenter12 = ex_fm.getcell2(12, false, true, "", "C", "T", "");
        var style12 = ex_fm.getcell2(12, false, true, "", "L", "T", "");
        var styleCenterBorderBottom12 = ex_fm.getcell2(12, false, true, "B", "C", "T", "");
        var style12Bold = ex_fm.getcell2(12, true, true, "", "L", "T", "");
        var styleTop14Bold = ex_fm.getcell2(14, true, true, "", "L", "T", "");
        var styleCenterBorder12Bold = ex_fm.getcell2(12, true, true, "LTRB", "C", "T", "");
        var style12Top = ex_fm.getcell2(12, false, true, "", "L", "T", "");
        var styleCenter16Bold = ex_fm.getcell2(16, true, true, "", "C", "T", "");
        var styleNumber0i0012Bold = ex_fm.getcell2(12, true, true, "", "R", "T", "#,#0.00");
        int row = 16;
        //Dong 1 - 3
        s1.CreateRow(0).CreateCell(0).SetCellValue("VINH GIA COMPANY LIMITED");
        s1.GetRow(0).GetCell(0).CellStyle = styleCenter18Bold;
        s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));
        s1.GetRow(0).HeightInPoints = 30;

        s1.CreateRow(1).CreateCell(0).SetCellValue("Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam");
        s1.GetRow(1).GetCell(0).CellStyle = styleCenter12;
        s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 6));
        s1.GetRow(1).HeightInPoints = 22;

        s1.CreateRow(2).CreateCell(0).SetCellValue("Tel: (84-235) 3567393   Fax: (84-235) 3567494");
        s1.GetRow(2).GetCell(0).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(1).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(2).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(3).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(4).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(5).CellStyle = styleCenterBorderBottom12;
        s1.GetRow(2).CreateCell(6).CellStyle = styleCenterBorderBottom12;
        s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));
        s1.GetRow(2).HeightInPoints = 22;

        s1.CreateRow(3).CreateCell(0).SetCellValue("COMMERCIAL INVOICE");
        s1.GetRow(3).GetCell(0).CellStyle = styleCenter18Bold;
        s1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 6));
        s1.GetRow(3).HeightInPoints = 30;
        //Dong 4 - 14
        s1.CreateRow(4).CreateCell(3).SetCellValue("No.:");
        s1.GetRow(4).GetCell(3).CellStyle = style12Bold;
        s1.GetRow(4).CreateCell(4).SetCellValue("");
        s1.GetRow(4).GetCell(4).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(4, 4, 4, 6));
        s1.GetRow(4).HeightInPoints = 22;

        s1.CreateRow(5).CreateCell(3).SetCellValue("Date:");
        s1.GetRow(5).GetCell(3).CellStyle = style12Bold;
        s1.GetRow(5).CreateCell(4).SetCellValue(DateTime.Parse(dt.Rows[0]["ngaylap"].ToString()).ToString("dd-MMM-yyyy"));
        s1.GetRow(5).GetCell(4).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(5, 5, 4, 6));
        s1.GetRow(5).HeightInPoints = 22;

        s1.CreateRow(6).CreateCell(0).SetCellValue("For the account & risk of the buyer:");
        s1.GetRow(6).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(6, 6, 0, 6));
        s1.GetRow(6).HeightInPoints = 22;

        string dc_kh = "";
        dc_kh = dt.Rows[0]["ten_dtkd"].ToString() + " \nAddress:" + dt.Rows[0]["diachi"].ToString() + "\nTel:" + dt.Rows[0]["tel"].ToString() + "\nFax:" + dt.Rows[0]["fax"].ToString();
        HSSFRichTextString rich = new HSSFRichTextString(dc_kh);
        s1.CreateRow(7).CreateCell(2).SetCellValue(rich);
        s1.GetRow(7).GetCell(2).CellStyle = styleTop14Bold;
        s1.GetRow(7).HeightInPoints = 95;
        s1.AddMergedRegion(new CellRangeAddress(7, 7, 2, 6));

        s1.CreateRow(9).CreateCell(0).SetCellValue("M/V:");
        s1.GetRow(9).GetCell(0).CellStyle = style12;
        s1.GetRow(9).CreateCell(2).SetCellValue("");
        s1.GetRow(9).GetCell(2).CellStyle = style12;
        s1.GetRow(9).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(9, 9, 2, 6));

        s1.CreateRow(10).CreateCell(0).SetCellValue("From:");
        s1.GetRow(10).GetCell(0).CellStyle = style12;
        s1.GetRow(10).CreateCell(2).SetCellValue(dt.Rows[0]["cang_x"].ToString());
        s1.GetRow(10).GetCell(2).CellStyle = style12;
        s1.GetRow(10).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(10, 10, 2, 6));

        s1.CreateRow(11).CreateCell(0).SetCellValue("To:");
        s1.GetRow(11).GetCell(0).CellStyle = style12;
        s1.GetRow(11).CreateCell(2).SetCellValue(dt.Rows[0]["cang_n"].ToString());
        s1.GetRow(11).GetCell(2).CellStyle = style12;
        s1.GetRow(11).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(11, 11, 2, 6));

        s1.CreateRow(12).CreateCell(0).SetCellValue("B/L No.:");
        s1.GetRow(12).GetCell(0).CellStyle = style12;
        s1.GetRow(12).CreateCell(2).SetCellValue("");
        s1.GetRow(12).GetCell(2).CellStyle = style12;
        s1.GetRow(12).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(12, 12, 2, 6));

        s1.CreateRow(13).CreateCell(0).SetCellValue("Commodity:");
        s1.GetRow(13).GetCell(0).CellStyle = style12;
        s1.GetRow(13).CreateCell(2).SetCellValue((dt.Rows[0]["chungloai_th"].ToString() + " wares").ToUpper());
        s1.GetRow(13).GetCell(2).CellStyle = style12;
        s1.GetRow(13).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(13, 13, 2, 6));

        s1.CreateRow(14).CreateCell(2).SetCellValue(dt.Rows[0]["sochungtu_th"].ToString());
        s1.GetRow(14).GetCell(2).CellStyle = style12;
        s1.GetRow(14).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(14, 14, 2, 6));

        //Dong 15 - Het
        s1.CreateRow(15).CreateCell(0).SetCellValue("Seller's Article");
        s1.GetRow(15).CreateCell(1).SetCellValue("Buyer's Article");
        s1.GetRow(15).CreateCell(2).SetCellValue("Description of goods");
        s1.GetRow(15).CreateCell(3).SetCellValue("Quantity (pcs,sets)");
        s1.GetRow(15).CreateCell(4).SetCellValue("");
        s1.GetRow(15).CreateCell(5).SetCellValue("Unit price (USD/pc,set)");
        s1.GetRow(15).CreateCell(6).SetCellValue("Amount (USD)");

        s1.GetRow(15).GetCell(0).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(1).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(2).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(3).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(4).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(5).CellStyle = styleCenterBorder12Bold;
        s1.GetRow(15).GetCell(6).CellStyle = styleCenterBorder12Bold;
        s1.AddMergedRegion(new CellRangeAddress(15, 15, 3, 4));
        // -- Details
        // create column
        string sochungtu = "";
        //thu tu tong cong // dem so dong hang cua tung sochungtu
        int count_row = 0, count_row_tt = 1;
        // tap hop so luong dong tong cong // tap hop cac vi tri cua tong cong
        string l_cbm = "";
        // create detail row
        int countDonHang = dt.AsEnumerable().Select(s => s.Field<string>("sochungtu")).Distinct().Count();
        decimal giatricongPO = 0;
        decimal giatritruPO = 0;
        var startRowHH = -1;
        var lstHH = new List<int>();
        var lstPO = new List<int>();
        double sumDiscount = 0;
        double sumFee = 0;
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
                startRowHH = row + 3;

            if (i == 0 | soPO != soPOTR) //ke dong sochungtu dau tien
            {
                s1.CreateRow(row).CreateCell(0).SetCellValue("Container/Seal No.: ");
                s1.GetRow(row).CreateCell(4).SetCellValue("FOB " + dt.Rows[i]["cang_x"].ToString());
                CellRangeAddress cellRange03 = new CellRangeAddress(row, row, 0, 3);
                CellRangeAddress cellRange46 = new CellRangeAddress(row, row, 4, 6);
                s1.AddMergedRegion(cellRange03);
                s1.AddMergedRegion(cellRange46);
                s1.GetRow(row).GetCell(0).CellStyle = leftBold;
                s1.GetRow(row).GetCell(4).CellStyle = borderWrap;
                s1.GetRow(row).CreateCell(7).CellStyle = borderonlyleft;
                s1.GetRow(row).HeightInPoints = 22;
                ex_fm.set_border(cellRange03, "LRTB", hssfsheet);
                ex_fm.set_border(cellRange46, "LRTB", hssfsheet);
                //--
                row++;
                //--
                s1.CreateRow(row).CreateCell(0).SetCellValue("P/I No. " + dt.Rows[i]["sochungtu"].ToString());
                cellRange03 = new CellRangeAddress(row, row, 0, 3);
                cellRange46 = new CellRangeAddress(row, row, 4, 6);
                s1.AddMergedRegion(cellRange03);
                s1.AddMergedRegion(cellRange46);
                s1.GetRow(row).GetCell(0).CellStyle = leftBold;
                s1.GetRow(row).CreateCell(7).CellStyle = borderonlyleft;
                s1.GetRow(row).HeightInPoints = 22;
                sochungtu = dt.Rows[i]["sochungtu"].ToString();
                ex_fm.set_border(cellRange03, "LRTB", hssfsheet);
                ex_fm.set_border(cellRange46, "LRTB", hssfsheet);
                row++;
            }
            else //ke dong sochungtu tiep theo
            {

            }

            string[] e_value = { "ma_sanpham", "", "mota_tienganh", "soluong", "ten_dvt", "giafob", "" };
            if (dt.Rows[i]["sochungtu"].ToString() != "9999")
            {
                IRow row_t = s1.CreateRow(row); row_t.HeightInPoints = 60;
                //
                int cell_t = 0;
                for (int j = 0; j < e_value.Count(); j++)
                {
                    if (j == 3 | j == 5)
                        row_t.CreateCell(cell_t).SetCellValue(double.Parse(dt.Rows[i][e_value[j]].ToString()));
                    else if (j == 6)
                        row_t.CreateCell(cell_t).CellFormula = String.Format("D{0} * F{0}", row + 1);
                    else if (e_value[j] == "")
                        row_t.CreateCell(cell_t).SetCellValue("");
                    else
                        row_t.CreateCell(cell_t).SetCellValue(dt.Rows[i][e_value[j]].ToString()); ;
                    cell_t++;
                }

                try
                {
                    for (int n = 0; n <= cell_t + 1; n++)
                    {
                        row_t.GetCell(n).CellStyle = border;
                    }
                }
                catch { }
                s1.GetRow(row).GetCell(3).CellStyle = borderright;
                s1.GetRow(row).GetCell(5).CellStyle = borderright;
                s1.GetRow(row).GetCell(6).CellStyle = borderright;
                s1.GetRow(row).GetCell(6).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");

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
                        rowToTalHeHang.CreateCell(2).SetCellValue(string.Format("Sub Total ({0}):", lstHH.Count + 1));
                        rowToTalHeHang.GetCell(2).CellStyle = leftBold;
                        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 1));
                        string fmlSum = string.Format(@"SUM(VNN{0}:VNN{1})", startRowHH, row);
                        rowToTalHeHang.CreateCell(3).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "D"));
                        rowToTalHeHang.GetCell(3).CellStyle = number2Bold;
                        rowToTalHeHang.CreateCell(4).SetCellValue("pcs/sets");
                        rowToTalHeHang.GetCell(4).CellStyle = leftBold;
                        rowToTalHeHang.CreateCell(6).CellFormula = string.Format("{0}", fmlSum.Replace("VNN", "G"));
                        rowToTalHeHang.GetCell(6).CellStyle = number2Bold;
                        row++;
                        count_row++;
                        count_row_tt++;

                        var rowDisHeHang = s1.CreateRow(row);
                        rowDisHeHang.HeightInPoints = rowHeight;
                        rowDisHeHang.CreateCell(2).SetCellValue(string.Format(@"Discount ({0}%):", gthh));
                        rowDisHeHang.GetCell(2).CellStyle = leftBold;
                        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 1));
                        rowDisHeHang.CreateCell(6).CellFormula = string.Format("ROUND(G{0}*{1}/100, 2)", row, gthh);
                        rowDisHeHang.GetCell(6).CellStyle = number2Bold;
                        row++;
                        count_row++;
                        count_row_tt++;

                        var rowTotalHeHang = s1.CreateRow(row);
                        rowTotalHeHang.HeightInPoints = rowHeight;
                        rowTotalHeHang.CreateCell(2).SetCellValue(string.Format("Total ({0}):", lstHH.Count + 1));
                        rowTotalHeHang.GetCell(2).CellStyle = leftBold;
                        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 1));
                        rowTotalHeHang.CreateCell(3).CellFormula = string.Format("D{0}", row - 1);
                        rowTotalHeHang.GetCell(3).CellStyle = number2Bold;
                        rowTotalHeHang.CreateCell(6).CellFormula = string.Format("G{0} - G{1}", row - 1, row);
                        rowTotalHeHang.GetCell(6).CellStyle = number2Bold;
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
                            sumQuantity += string.Format("D{0}+", iHH);
                            sumAmount += string.Format("G{0}+", iHH);
                        }
                        sumQuantity += "0";
                        sumAmount += "0";
                    }
                    else
                    {
                        sumQuantity += string.Format("SUM(D{0}:D{1})", startRowHH, row);
                        sumAmount += string.Format("SUM(G{0}:G{1})", startRowHH, row);
                    }
                    //vi tri row dau tien
                    int m = row - count_row + 1, sub_row = 0, discount_row = 0;
                    //start tong cong 1
                    s1.CreateRow(row).CreateCell(2).SetCellValue(string.Format("Sub Total:", lstPO.Count + 1));
                    s1.GetRow(row).CreateCell(3).CellFormula = string.Format("{0}", sumQuantity);
                    s1.GetRow(row).CreateCell(6).CellFormula = string.Format("{0}", sumAmount);
                    s1.GetRow(row).CreateCell(14).SetCellValue(discountPO);
                    s1.SetColumnWidth(14, 0);
                    // style
                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(3).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                    for (int nht = 6; nht <= 6; nht++)
                    {
                        s1.GetRow(row).GetCell(nht).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                    }

                    row++;
                    l_cbm += row + ",";
                    sub_row = row;
                    //end

                    //start giam gia
                    m = row - count_row + 1;
                    
                    s1.CreateRow(row).CreateCell(2).SetCellValue("Discount PO (" + discountPO + "%):");
                    s1.GetRow(row).CreateCell(6).CellFormula = String.Format("ROUND(G{0}*O{1}/100,2)", row, row);
                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(6).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                    sumDiscount += double.Parse(discountPO);
                    if (double.Parse(discountPO) == 0) s1.GetRow(row).ZeroHeight = true;
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
                        if(phidonhang.Count == 1)
                        {
                            phidonhang.Add(new c_phidonhang()
                            {
                                phitang = !loaiPhi,
                                phi = 0,
                            });
                        }

                        foreach (var pdh in phidonhang.OrderByDescending(s=>s.phitang))
                        {
                            //start phi cong va tru
                            m = row - count_row + 1;
                            string plus = "+";
                            int check_plus = 0;
                            if (pdh.phitang != true)
                            {
                                plus = "-";
                                check_plus = 1;
                                giatritruPO = giatritruPO + pdh.phi.GetValueOrDefault(0);
                            }
                            else
                            {
                                giatricongPO = giatricongPO + pdh.phi.GetValueOrDefault(0);
                            }

                            s1.CreateRow(row).CreateCell(2).SetCellValue("(" + plus + ")" + pdh.mota + ":");
                            s1.GetRow(row).CreateCell(6).SetCellValue((double)pdh.phi);
                            s1.GetRow(row).CreateCell(14).SetCellValue(check_plus);

                            s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                            s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                            s1.GetRow(row).GetCell(6).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                            s1.GetRow(row).HeightInPoints = 22;
                            s1.GetRow(row).ZeroHeight = pdh.phi.GetValueOrDefault(0) <= 0;
                            row++;
                            vitri_ct += plus + "H" + row;
                            //end    
                        }
                    }
                    else
                    {
                        //start phi cong
                        m = row - count_row + 1;
                        s1.CreateRow(row).CreateCell(2).SetCellValue("(+):");
                        s1.GetRow(row).CreateCell(6).SetCellValue(0);
                        s1.GetRow(row).CreateCell(14).SetCellValue(0);

                        s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                        s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                        s1.GetRow(row).GetCell(6).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                        s1.GetRow(row).HeightInPoints = 22;
                        s1.GetRow(row).ZeroHeight = true;
                        row++;
                        chuoi_ct += "+G" + row;
                        //end

                        //start phi tru
                        m = row - count_row + 1;
                        s1.CreateRow(row).CreateCell(2).SetCellValue("(-):");
                        s1.GetRow(row).CreateCell(6).SetCellValue(0);
                        s1.GetRow(row).CreateCell(14).SetCellValue(0);

                        s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                        s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                        s1.GetRow(row).GetCell(6).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                        s1.GetRow(row).HeightInPoints = 22;
                        s1.GetRow(row).ZeroHeight = true;
                        row++;
                        chuoi_ct += "-G" + row;
                        //end
                    }

                    //end

                    //Packing Fee
                    var packingFee = double.Parse(dt.Rows[i]["cpdg_vuotchuan"].ToString());
                    s1.CreateRow(row).CreateCell(2).SetCellValue(string.Format("Extra packing fees:"));
                    s1.GetRow(row).GetCell(2).CellStyle = style12Bold;
                    s1.GetRow(row).CreateCell(6).SetCellValue(packingFee);
                    s1.GetRow(row).GetCell(6).CellStyle = styleNumber0i0012Bold;
                    s1.GetRow(row).HeightInPoints = 22;
                    s1.GetRow(row).ZeroHeight = packingFee == 0;
                    sumFee += packingFee;
                    row++;
                    //start tong cong 2
                    m = row - count_row + 1;
                    string sumToTalPO = "";
                    for (var iHH = 0; iHH < lstHH.Count; iHH++)
                    {
                        var rowHH = lstHH[iHH];
                        sumToTalPO += string.Format(@"G{0}+", rowHH + 1);
                    }
                    sumToTalPO += "0";
                    s1.CreateRow(row).CreateCell(2).SetCellValue(string.Format("Total{0}:", countDonHang > 1 ? " (" + (lstPO.Count + 1) + ")" : ""));
                    s1.GetRow(row).CreateCell(3).CellFormula = string.Format("D{0}", sub_row);
                    s1.GetRow(row).CreateCell(6).CellFormula = string.Format("G{0}-G{1}+G{2}-G{3} + G{4}", sub_row, sub_row + 1, sub_row + 2, sub_row + 3, sub_row + 4);
                    // style
                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(3).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(6).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                    s1.GetRow(row).HeightInPoints = 22;
                    row++;
                    lstPO.Add(row);
                    lstHH.Clear();
                    startRowHH = -1;
                    //end
                }
            }
        }

        string totalTxt = "", sumQuantityAll = "", sumAmountAll = "";
        for (var i = 0; i < lstPO.Count; i++)
        {
            var rowPO = lstPO[i];
            totalTxt += string.Format("{0}+", i + 1);
            sumQuantityAll += string.Format(@"D{0}+", rowPO);
            sumAmountAll += string.Format(@"G{0}+", rowPO);
        }
        sumQuantityAll += "0";
        sumAmountAll += "0";
        totalTxt = totalTxt.Length > 0 ? totalTxt.Substring(0, totalTxt.Length - 1) : "";
        //Packing Fee
        s1.CreateRow(row).CreateCell(2).SetCellValue(String.Format("Extra packing fees{0}:", countDonHang > 1 ? " (" + totalTxt + ")" : ""));
        s1.GetRow(row).GetCell(2).CellStyle = style12Bold;
        string sumToTalPackingFee = "";
        for (var iPO = 0; iPO < lstPO.Count; iPO++)
        {
            var r = lstPO[iPO];
            sumToTalPackingFee += string.Format(@"G{0}+", r - 1);
        }
        sumToTalPackingFee += "0";
        s1.GetRow(row).CreateCell(6).CellFormula = string.Format("{0}", sumToTalPackingFee);
        s1.GetRow(row).GetCell(6).CellStyle = styleNumber0i0012Bold;
        row++;
        //Total
        s1.CreateRow(row).CreateCell(2).SetCellValue(string.Format(@"Total{0}:", countDonHang > 1 ? " (" + totalTxt + ")" : ""));
        s1.GetRow(row).GetCell(2).CellStyle = leftBold;
        s1.GetRow(row).CreateCell(3).CellFormula = string.Format(sumQuantityAll);
        s1.GetRow(row).GetCell(3).CellStyle = rightBold;
        s1.GetRow(row).CreateCell(6).CellFormula = string.Format(sumAmountAll);
        s1.GetRow(row).GetCell(6).CellStyle = rightBold;

        s1.GetRow(row).HeightInPoints = 22;
        s1.GetRow(row - 1).HeightInPoints = 22;
        s1.GetRow(row).ZeroHeight = true;
        s1.GetRow(row - 1).ZeroHeight = true;
        if ((countDonHang > 1) || (sumDiscount > 0 && countDonHang > 1) || giatricongPO > 0 || giatritruPO > 0)
        {
            s1.GetRow(row).ZeroHeight = false;
            if (sumFee > 0)
                s1.GetRow(row - 1).ZeroHeight = false;
        }
        row++;
        s1.CreateRow(row).CreateCell(0).SetCellValue("Say: USD " + dt.Rows[0]["money"].ToString().Replace("Dollars", ""));
        s1.GetRow(row).GetCell(0).CellStyle = rightBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 6));
        row++;
        ////
        s1.CreateRow(row).CreateCell(0).SetCellValue("Please arrange the payment to our account with banking details as follows:");
        s1.GetRow(row).GetCell(0).CellStyle = style12;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 6));
        s1.GetRow(row).HeightInPoints = 22;
        row++;

        s1.CreateRow(row).CreateCell(0).SetCellValue(@"Beneficary's name:");
        s1.GetRow(row).GetCell(0).CellStyle = style12;
        s1.GetRow(row).CreateCell(1).SetCellValue(@"VINH GIA COMPANY LIMITED");
        s1.GetRow(row).GetCell(1).CellStyle = style12Bold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 6));
        s1.GetRow(row).HeightInPoints = 22;
        row++;

        s1.CreateRow(row).CreateCell(0).SetCellValue(@"Beneficary's address:");
        s1.GetRow(row).GetCell(0).CellStyle = style12;
        s1.GetRow(row).CreateCell(1).SetCellValue(@"Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam");
        s1.GetRow(row).GetCell(1).CellStyle = style12;
        s1.GetRow(row).HeightInPoints = 44;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 6));
        row++;

        s1.CreateRow(row).CreateCell(0).SetCellValue("Account no.:");
        s1.GetRow(row).GetCell(0).CellStyle = style12;
        s1.GetRow(row).HeightInPoints = 22;
        s1.GetRow(row).CreateCell(1).SetCellValue("046.137.3860862");
        s1.GetRow(row).GetCell(1).CellStyle = style12Bold;
        s1.GetRow(row).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 6));
        row++;

        s1.CreateRow(row).CreateCell(0).SetCellValue("At bank:");
        s1.GetRow(row).GetCell(0).CellStyle = style12Top;
        s1.GetRow(row).HeightInPoints = 30;
        var dong1Bank = "JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIET NAM - TAN BINH DUONG BRANCH";
        var dong2Bank = "NO 16, LE TRONG TAN STREET, BINH DUONG 2 QUARTER, DI AN WARD, HO CHI MINH CITY, VIET NAM";
        var dong3Bank = "Tel: (84 - 274) 3792 158 - Fax: (84 - 274) 3793 970,";
        var dong4Bank = "Swift code: BFTVVNVX";
        var rtxtBank = new HSSFRichTextString(
            dong1Bank + "\n" +
            dong2Bank + "\n" +
            dong3Bank + "\n" +
            dong4Bank
        );
        IFont bold12Font = hssfworkbook.CreateFont();
        bold12Font.FontHeightInPoints = 12;
        bold12Font.FontName = "Arial";
        bold12Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
        IFont normal12Font = hssfworkbook.CreateFont();
        normal12Font.FontHeightInPoints = 12;
        normal12Font.FontName = "Arial";
        normal12Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
        var startBank = 0;
        rtxtBank.ApplyFont(startBank, startBank + dong1Bank.Length, bold12Font);
        //--2
        startBank = dong1Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong2Bank.Length, normal12Font);
        //--3
        startBank += dong2Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong3Bank.Length, normal12Font);
        //--4
        startBank += dong3Bank.Length + 1;
        rtxtBank.ApplyFont(startBank, startBank + dong4Bank.Length, bold12Font);
        s1.GetRow(row).CreateCell(1).SetCellValue(rtxtBank);
        s1.GetRow(row).GetCell(1).CellStyle.WrapText = true;
        s1.GetRow(row).GetCell(1).CellStyle.VerticalAlignment = VerticalAlignment.Bottom;
        s1.GetRow(row).HeightInPoints = 70;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 6));
        row++;

        s1.CreateRow(row).CreateCell(3).SetCellValue(@"VINH GIA COMPANY LIMITED");
        s1.GetRow(row).GetCell(3).CellStyle = styleCenter16Bold;
        s1.GetRow(row).HeightInPoints = 22;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 3, 6));
        row++;

        var link = ExcuteSignalRStatic.mapPathSignalR("~/images/more/ckcdct.jpg");
        if (File.Exists(link))
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(link);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            IDrawing patriarch = s1.CreateDrawingPatriarch();
            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 4, row + 1, 4, row + 1);
            anchor.AnchorType = AnchorType.MoveDontResize;

            int index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.JPEG);
            IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
            signaturePicture.Resize();
        }

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
