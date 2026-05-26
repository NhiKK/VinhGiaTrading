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
public partial class PrintControllers_IToKhaiHQ_Default : System.Web.UI.Page
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
					, cn.ten_cangbien as cang_n
					, cx.ten_cangbien as cang_x
					, sp.ma_sanpham
					, sp.mota_tiengviet
					, ddh.soluong
					, ddh.giafob
					, dh.totalcbm
                    , dh.discount_hehang_value
					, convert(decimal(18,2),dh.discount) as discount
					, dh.discount as discount1
					, trl.ten_trongluong
					, dg.sl_outer
					, dg.sl_inner
					, dvt_outer.ten_dvt as dvt_outer
					, dvt_inner.ten_dvt as dvt_inner
					, ddh.l1, ddh.w1, ddh.h1
					, ddh.l2, ddh.w2, ddh.h2, ddh.v2
					, case when dh.md_trongluong_id is not null then dg.sl_outer * (SELECT dbo.f_convertKgToPounds(sp.trongluong, dh.md_trongluong_id)) else 0 end as n_w_outer
					, case when dh.md_trongluong_id is not null then dg.sl_inner * (SELECT dbo.f_convertKgToPounds(sp.trongluong, dh.md_trongluong_id)) else 0 end as n_w_inner
					, (ddh.l2 + ddh.w2) * (ddh.w2 * ddh.h2) / 5400 as t_thung_outer
					, (ddh.l1 + ddh.w1) * (ddh.w1 * ddh.h1) / 5400 as t_thung_inner
					from c_dongdonhang ddh
					left join c_donhang dh on dh.c_donhang_id = ddh.c_donhang_id
					left join md_sanpham sp on sp.md_sanpham_id = ddh.md_sanpham_id
					left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id
					left join md_cangbien cn on cn.md_cangbien_id = dtkd.md_cangbien_id
					left join md_cangbien cx on cx.md_cangbien_id = dh.md_cangbien_id
					left join md_donggoi dg on dg.md_donggoi_id = ddh.md_donggoi_id
					left join md_donvitinh dvt_outer on dvt_outer.md_donvitinh_id = dg.dvt_outer
					left join md_donvitinh dvt_inner on dvt_inner.md_donvitinh_id = dg.dvt_inner
					left join md_trongluong trl on trl.md_trongluong_id = dh.md_trongluong_id
					where N'{0}' like  N'%'+dh.c_donhang_id+'%'
					union
					select null as ngaylap
					, null as c_donhang_id
					, '9999' as sochungtu
					, null as ten_dtkd
					, null as diachi
					, null as tel
					, null as fax 
					, null as cang_n
					, null as cang_x
					, null as ma_sanpham
					, null as mota_tiengviet
					, null as soluong
					, null as giafob
					, null as totalcbm
					, null as discount
					, null as discount1
					, null as ten_trongluong
					, null as sl_outer
					, null as sl_inner
					, null as dvt_outer
					, null as dvt_inner
					, null as l1, null as w1, null as h1
					, null as l2, null as w2, null as h2, null as v2
					, null as n_w_outer
					, null as n_w_inner
					, null as t_thung_outer
					, null as t_thung_inner
					)A)B order by B.sochungtu asc, B.ma_sanpham asc

				", c_donhang_id == null ? "" : c_donhang_id);

                DataTable dt = mdbc.GetData(select);

                if (dt.Rows.Count != 0)
                {
                    HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
                    String saveAsFileName = String.Format("ToKhaiHaiQuan-{0}.xls", DateTime.Now);
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
        int heigh = 22;
        int row = 0;
        //set A1 - A3
        string[] a = { "HẢI QUAN VIỆT NAM", "DANH MỤC KÈM THEO TỜ KHAI HÀNG HOÁ XUẤT KHẨU", "TKHQ SỐ:………………………/KV………………………….NGÀY……..THÁNG……….NĂM…………" };
        for (int i = 0; i < a.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(0).SetCellValue(a[i]);
            s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 8));
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
        string[] b_value = { "", "ngaylap" };
        for (int i = 0; i < b.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(5).SetCellValue(b[i]);
            s1.GetRow(row).GetCell(5).CellStyle = cellBold;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 6, 8));
            if (i == 1)
                s1.GetRow(row).CreateCell(6).SetCellValue(DateTime.Parse(dt.Rows[0][b_value[i]].ToString()).ToString("dd/MM/yyyy"));
            else if (b_value[i] != "")
                s1.GetRow(row).CreateCell(6).SetCellValue(dt.Rows[0][b_value[i]].ToString());
            else
                s1.GetRow(row).CreateCell(6).SetCellValue("");
            s1.GetRow(row).GetCell(6).CellStyle = cellBold;
            s1.GetRow(row).HeightInPoints = heigh;
            row++;
        }

        //set A6 - A12
        string[] c = { "Người mua:", "Tên tàu:", "Ngày tàu chạy:", "Cảng đi:", "Cảng đến:", "Hàng hóa:" };
        string[] c_value = { "", "", "", "cang_x", "cang_n", "" };
        for (int i = 0; i < c.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(0).SetCellValue(c[i]);
            s1.GetRow(row).GetCell(0).CellStyle = cellBold;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 8));
            if (i == 0)
            {
                HSSFRichTextString rich = new HSSFRichTextString(String.Format("{0} \n Address:{1} \n Tel:{2} \n Fax{3}", dt.Rows[0]["ten_dtkd"].ToString(), dt.Rows[0]["diachi"].ToString(), dt.Rows[0]["tel"].ToString(), dt.Rows[0]["fax"].ToString()));
                s1.GetRow(row).CreateCell(2).SetCellValue(rich);
                s1.GetRow(row).GetCell(2).CellStyle = cellBold;
                s1.GetRow(row).HeightInPoints = 95;
                row++;
            }
            else
            {
                if (c_value[i] != "")
                {
                    s1.GetRow(row).CreateCell(2).SetCellValue(dt.Rows[0][c_value[i]].ToString());
                    s1.GetRow(row).GetCell(2).CellStyle = celltext;
                }
                s1.GetRow(row).HeightInPoints = heigh;

            }
            row++;
        }

        // set A13 - All
        // -- Header
        int cell = 0;
        string[] d = { "Mã số", "Mã số khách:", "Diễn giải", "Đóng gói", "", "Số kiện", "Số lượng (cái/bộ)", "Đơn giá", "Thành tiền", "NW", "GW", "CBM" };
        IRow rowColumnHeader = s1.CreateRow(12);
        rowColumnHeader.HeightInPoints = 47;
        for (int i = 0; i < d.Count(); i++)
        {
            int with = 5000;
            rowColumnHeader.CreateCell(cell).SetCellValue(d[i]);
            rowColumnHeader.GetCell(i).CellStyle = borderWrap;
            if (9 <= i & i <= 11)
                with = 0;
            else if (i == 2)
                with = 11000;
            s1.SetColumnWidth(cell, with);
            cell++;
        }
        s1.AddMergedRegion(new CellRangeAddress(row, row, 3, 4));
        row++;
        // -- Details
        // create column
        string sochungtu = "", row_value = "";
        //thu tu tong cong // dem so dong hang cua tung sochungtu
        int count_total = 1, count_row = 0;
        // tap hop so luong dong tong cong // tap hop cac vi tri cua tong cong
        string n_total = "", l_total = "", l_cbm = "";
        // create detail row
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == 0) //ke dong sochungtu dau tien
            {
                if (dt.Rows[i]["sochungtu"].ToString() != sochungtu)
                {

                    s1.CreateRow(row).CreateCell(0).SetCellValue("P/I No. " + dt.Rows[i]["sochungtu"].ToString());

                    s1.GetRow(row).CreateCell(5).SetCellValue("FOB " + dt.Rows[i]["cang_x"].ToString());
                    // for(int i_bder=0;i_bder<=0;i_bder++){
                    // s1.GetRow(row).GetCell(i_bder).CellStyle = leftBold;
                    // }
                    // for(int i_bder=5;i_bder<=5;i_bder++){
                    // s1.GetRow(row).GetCell(i_bder).CellStyle = borderWrap;
                    // }
                    s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 4));
                    s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 8));
                    s1.GetRow(row).GetCell(0).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(5).CellStyle = borderWrap;
                    s1.GetRow(row).CreateCell(12).CellStyle = borderonlyleft;
                    s1.GetRow(row).HeightInPoints = 22;
                    sochungtu = dt.Rows[i]["sochungtu"].ToString();
                    row++;
                }
            }
            else //ke dong sochungtu tiep theo
            {
                if (dt.Rows[i]["sochungtu"].ToString() != dt.Rows[i - 1]["sochungtu"].ToString())
                {
                    //tap hop row tong cong 
                    n_total += count_total + " + ";
                    //vi tri row dau tien
                    int m = row - count_row + 1, sub_row = 0, discount_row = 0;
                    //start tong cong 1
                    s1.CreateRow(row).CreateCell(2).SetCellValue("Tổng cộng(" + count_total + "):");
                    s1.GetRow(row).CreateCell(5).CellFormula = String.Format("SUM(F{0}:F{1})", m, row);
                    s1.GetRow(row).CreateCell(6).CellFormula = String.Format("SUM(G{0}:G{1})", m, row);
                    s1.GetRow(row).CreateCell(8).CellFormula = String.Format("SUM(I{0}:I{1})", m, row);
                    s1.GetRow(row).CreateCell(9).CellFormula = String.Format("SUM(J{0}:I{1})", m, row);
                    s1.GetRow(row).CreateCell(10).CellFormula = String.Format("SUM(K{0}:I{1})", m, row);
                    s1.GetRow(row).CreateCell(11).CellFormula = String.Format("SUM(L{0}:L{1})", m, row);
                    s1.GetRow(row).CreateCell(14).SetCellValue(dt.Rows[i - 1]["discount1"].ToString());
                    s1.SetColumnWidth(14, 0);
                    // style
                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(5).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                    for (int nht = 8; nht <= 11; nht++)
                    {
                        s1.GetRow(row).GetCell(nht).CellStyle = rightBold;
                        s1.GetRow(row).GetCell(nht).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                    }
					
					double dsc1 = double.Parse(dt.Rows[i - 1]["discount1"].ToString());
                    if (dt.Rows[i - 1]["discount"].ToString() != "0")
                    {
                        s1.GetRow(row).HeightInPoints = 22;
                    }
                    else
                    {
                        s1.GetRow(row).HeightInPoints = 0;
                    }
                    row++;
                    l_cbm += row + ",";
                    sub_row = row;
                    //end

                    //start giam gia
                    m = row - count_row + 1;
                    s1.CreateRow(row).CreateCell(2).SetCellValue("Giảm giá(" + dt.Rows[i - 1]["discount"].ToString() + "%):");
					
					string str_dsc1 = dsc1.ToString();
					string funcDiscount = "";
					if(str_dsc1.Contains(".")) {
						string dot1 = str_dsc1.Split('.')[1];
						if(dot1.Length > 2) {
							funcDiscount = "I{0}*O{1}/100";
						}
						else {
							funcDiscount = "ROUND(I{0}*O{1}/100,2)";
						}
					}
					else {
						funcDiscount = "ROUND(I{0}*O{1}/100,2)";
					}
                    s1.GetRow(row).CreateCell(8).CellFormula = String.Format(funcDiscount, row, row);
                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(8).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(8).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                    if (dt.Rows[i - 1]["discount"].ToString() != "0")
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
                    string c_donhang_id = "", chuoi_ct = "0";
                    var phidonhang = db.c_phidonhangs.Where(s => s.c_donhang_id == dt.Rows[i - 1]["c_donhang_id"].ToString());
                    if (phidonhang.Count() > 0)
                    {
                        string vitri_ct = "-0";
                        foreach (c_phidonhang pdh in phidonhang)
                        {
                            int check_exit = 0;
                            if (c_donhang_id == dt.Rows[i - 1]["c_donhang_id"].ToString())
                            {
                                check_exit = 1;
                            }
                            else
                            {
                                c_donhang_id = dt.Rows[i - 1]["c_donhang_id"].ToString();
                                check_exit = 0;
                            }

                            if (check_exit == 0)
                            {
                                String select_congtru = String.Format(@"
										select  sum(A.cong) as sl_cong, sum(A.tru) as sl_tru from (
											select dh.phi, dh.mota
											, case when dh.phitang = 1 then 1 else 0 end as cong
											, case when dh.phitang = 0 then 1 else 0 end as tru
											from c_phidonhang dh where  N'{0}' like N'%' + dh.c_donhang_id + '%'
											group by dh.phi, dh.mota, dh.phitang
											)A
										", c_donhang_id);
                                DataTable dt_ct = mdbc.GetData(select_congtru);

                                if (dt_ct.Rows[0]["sl_cong"].ToString() == "0")
                                {
                                    //start phi cong
                                    m = row - count_row + 1;
                                    s1.CreateRow(row).CreateCell(2).SetCellValue("(+):");
                                    s1.GetRow(row).CreateCell(8).SetCellValue(0);
                                    s1.GetRow(row).CreateCell(14).SetCellValue(0);

                                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                                    s1.GetRow(row).GetCell(8).CellStyle = rightBold;
                                    s1.GetRow(row).GetCell(8).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                                    s1.GetRow(row).HeightInPoints = 0;
                                    row++;
                                    vitri_ct += "+H" + row;
                                    //end
                                }

                                if (dt_ct.Rows[0]["sl_cong"].ToString() != "0" | dt_ct.Rows[0]["sl_tru"].ToString() != "0")
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

                                    s1.CreateRow(row).CreateCell(2).SetCellValue("(" + plus + ")" + pdh.mota + ":");
                                    s1.GetRow(row).CreateCell(8).SetCellValue((double)pdh.phi);
                                    s1.GetRow(row).CreateCell(14).SetCellValue(check_plus);

                                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                                    s1.GetRow(row).GetCell(8).CellStyle = rightBold;
                                    s1.GetRow(row).GetCell(8).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                                    s1.GetRow(row).HeightInPoints = 22;
                                    row++;
                                    vitri_ct += plus + "H" + row;
                                    //end
                                }

                                if (dt_ct.Rows[0]["sl_tru"].ToString() == "0")
                                {
                                    //start phi tru
                                    m = row - count_row + 1;
                                    s1.CreateRow(row).CreateCell(2).SetCellValue("(-):");
                                    s1.GetRow(row).CreateCell(8).SetCellValue(0);
                                    s1.GetRow(row).CreateCell(14).SetCellValue(1);

                                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                                    s1.GetRow(row).GetCell(8).CellStyle = rightBold;
                                    s1.GetRow(row).GetCell(8).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                                    s1.GetRow(row).HeightInPoints = 0;
                                    row++;
                                    vitri_ct += "-H" + row;
                                    //end
                                }
                                chuoi_ct = vitri_ct;
                            }
                        }
                    }
                    else
                    {
                        //start phi cong
                        m = row - count_row + 1;
                        s1.CreateRow(row).CreateCell(2).SetCellValue("(+):");
                        s1.GetRow(row).CreateCell(8).SetCellValue(0);
                        s1.GetRow(row).CreateCell(14).SetCellValue(0);

                        s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                        s1.GetRow(row).GetCell(8).CellStyle = rightBold;
                        s1.GetRow(row).GetCell(8).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                        s1.GetRow(row).HeightInPoints = 0;
                        row++;
                        chuoi_ct += "+I" + row;
                        //end

                        //start phi tru
                        m = row - count_row + 1;
                        s1.CreateRow(row).CreateCell(2).SetCellValue("(-):");
                        s1.GetRow(row).CreateCell(8).SetCellValue(0);
                        s1.GetRow(row).CreateCell(14).SetCellValue(0);

                        s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                        s1.GetRow(row).GetCell(8).CellStyle = rightBold;
                        s1.GetRow(row).GetCell(8).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                        s1.GetRow(row).HeightInPoints = 0;
                        row++;
                        chuoi_ct += "-I" + row;
                        //end
                    }
                    //end

                    //start tong cong 2
                    m = row - count_row + 1;
                    s1.CreateRow(row).CreateCell(2).SetCellValue("Tổng cộng(" + count_total + "):");
                    s1.GetRow(row).CreateCell(5).CellFormula = String.Format("F{0}", sub_row);
                    s1.GetRow(row).CreateCell(6).CellFormula = String.Format("G{0}", sub_row);
                    s1.GetRow(row).CreateCell(8).CellFormula = String.Format("I{0} - I{1} + {2}", sub_row, discount_row, chuoi_ct);
                    // style
                    s1.GetRow(row).GetCell(2).CellStyle = leftBold;
                    s1.GetRow(row).GetCell(5).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(8).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(8).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                    s1.GetRow(row).HeightInPoints = 22;
                    row++;
                    l_total += row + ",";
                    //end

                    // ke dong sochungtu
                    if (dt.Rows[i]["sochungtu"].ToString() != "9999")
                    {
                        s1.CreateRow(row).CreateCell(0).SetCellValue("P/I No. " + dt.Rows[i]["sochungtu"].ToString());
                        s1.GetRow(row).CreateCell(5).SetCellValue("FOB " + dt.Rows[i]["cang_x"].ToString());

                        s1.GetRow(row).GetCell(0).CellStyle = leftBold;
                        s1.GetRow(row).GetCell(5).CellStyle = borderWrap;
                        CellRangeAddress cellRange04 = new CellRangeAddress(row, row, 0, 4);
                        CellRangeAddress cellRange58 = new CellRangeAddress(row, row, 5, 8);
                        s1.AddMergedRegion(cellRange04);
                        s1.AddMergedRegion(cellRange58);
                        //border merge
                        HSSFRegionUtil.SetBorderBottom(NPOI.SS.UserModel.BorderStyle.Thin, cellRange04, hssfsheet, hssfworkbook);
                        HSSFRegionUtil.SetBorderTop(NPOI.SS.UserModel.BorderStyle.Thin, cellRange04, hssfsheet, hssfworkbook);
                        HSSFRegionUtil.SetBorderRight(NPOI.SS.UserModel.BorderStyle.Thin, cellRange04, hssfsheet, hssfworkbook);
                        HSSFRegionUtil.SetBorderLeft(NPOI.SS.UserModel.BorderStyle.Thin, cellRange04, hssfsheet, hssfworkbook);

                        HSSFRegionUtil.SetBorderBottom(NPOI.SS.UserModel.BorderStyle.Thin, cellRange58, hssfsheet, hssfworkbook);
                        HSSFRegionUtil.SetBorderTop(NPOI.SS.UserModel.BorderStyle.Thin, cellRange58, hssfsheet, hssfworkbook);
                        HSSFRegionUtil.SetBorderRight(NPOI.SS.UserModel.BorderStyle.Thin, cellRange58, hssfsheet, hssfworkbook);
                        HSSFRegionUtil.SetBorderLeft(NPOI.SS.UserModel.BorderStyle.Thin, cellRange58, hssfsheet, hssfworkbook);

                        s1.GetRow(row).CreateCell(12).CellStyle = borderonlyleft;
                        s1.GetRow(row).HeightInPoints = 22;

                    }
                    count_total++;
                    row++;
                    count_row = 0;
                    sochungtu = dt.Rows[i]["sochungtu"].ToString();
                }
            }
            string[] e_value = { "ma_sanpham", "", "mota_tiengviet", "sl_outer", "dvt_outer", "", "soluong", "giafob", "", "n_w_outer", "g_w_outer", "totalcbm" };
            if (dt.Rows[i]["sochungtu"].ToString() != "9999")
            {
                IRow row_t = s1.CreateRow(row); row_t.HeightInPoints = 60;
                //
                int cell_t = 0;
                for (int j = 0; j < e_value.Count(); j++)
                {
                    if (j == 3 | j == 6 | j == 7 | j == 9 | j == 10 | j == 11)
                    {
                        row_t.CreateCell(cell_t).SetCellValue(double.Parse(dt.Rows[i][e_value[j]].ToString()));
                        row_t.GetCell(cell_t).CellStyle = borderright;
                    }
                    //else if (j == 6 | j == 7 | j == 9 | j == 10 | j == 11)
                    //{
                    //    row_t.CreateCell(cell_t).SetCellValue(double.Parse(dt.Rows[i][e_value[j]].ToString()));
                    //    row_t.GetCell(cell_t).CellStyle = border;
                    //}
                    else if (j == 5)
                    {
                        //row_t.CreateCell(cell_t).CellFormula = String.Format("ROUNDUP(G{0} / D{0},0)", row + 1);
                        row_t.CreateCell(cell_t).CellFormula = String.Format("G{0} / D{0}", row + 1);
                        row_t.GetCell(cell_t).CellStyle = borderright;
                    }
                    else if (j == 8)
                    {
                        row_t.CreateCell(cell_t).CellFormula = String.Format("H{0} * G{0}", row + 1);
                        row_t.GetCell(cell_t).CellStyle = borderright;
                    }
                    else if (e_value[j] == "")
                    {
                        row_t.CreateCell(cell_t).SetCellValue("");
                        row_t.GetCell(cell_t).CellStyle = border;
                    }
                    else
                    {
                        row_t.CreateCell(cell_t).SetCellValue(dt.Rows[i][e_value[j]].ToString());
                        row_t.GetCell(cell_t).CellStyle = border;
                    }
                    cell_t++;
                }

                try
                {
                    //for (int n = 0; n <= cell_t + 1; n++)
                    //{
                    //  table style

                    //}

                    // s1.GetRow(row).GetCell(5).CellStyle = rightBold;
                    // s1.GetRow(row).GetCell(6).CellStyle = rightBold;
                    // s1.GetRow(row).GetCell(8).CellStyle = rightBold;
                    s1.GetRow(row).GetCell(8).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
                }
                catch { }
                row++;
                count_row++;
            }
        }
        //tru so dong du
        row--;
        // tap hop 
        string numbel_total = n_total.Substring(0, n_total.Length - 3), get_locate_sokien = "", get_locate_soluong = "", get_locate_thanhtien = "", get_locate_cbm = "", get_locate_nw = "", get_locate_gw = "";
        string[] locate_total = l_total.Split(',');
        string[] locate_cbm = l_cbm.Split(',');
        for (int i = 0; i < locate_total.Count() - 1; i++)
        {
            get_locate_sokien += "F" + locate_total[i] + "+";
            get_locate_soluong += "G" + locate_total[i] + "+";
            get_locate_thanhtien += "I" + locate_total[i] + "+";
            get_locate_cbm += "L" + locate_cbm[i] + "+";
            get_locate_nw += "K" + locate_cbm[i] + "+";
            get_locate_gw += "J" + locate_cbm[i] + "+";
        }
        //
        string new_get_locate_thanhtien = get_locate_thanhtien.Substring(0, get_locate_thanhtien.Length - 1);
        string new_get_locate_soluong = get_locate_soluong.Substring(0, get_locate_soluong.Length - 1);
        string new_get_locate_sokien = get_locate_sokien.Substring(0, get_locate_sokien.Length - 1);
        //
        string new_get_locate_cbm = get_locate_cbm.Substring(0, get_locate_cbm.Length - 1);
        string new_get_locate_nw = get_locate_nw.Substring(0, get_locate_nw.Length - 1);
        string new_get_locate_gw = get_locate_gw.Substring(0, get_locate_gw.Length - 1);
        //start tong cong total
        s1.CreateRow(row).CreateCell(2).SetCellValue("Tổng cộng(" + numbel_total + "):");
        s1.GetRow(row).CreateCell(5).CellFormula = String.Format(new_get_locate_sokien);
        s1.GetRow(row).CreateCell(6).CellFormula = String.Format(new_get_locate_soluong);
        s1.GetRow(row).CreateCell(8).CellFormula = String.Format(new_get_locate_thanhtien);
        s1.GetRow(row).CreateCell(9).CellFormula = String.Format(new_get_locate_nw);
        s1.GetRow(row).CreateCell(10).CellFormula = String.Format(new_get_locate_gw);
        s1.GetRow(row).CreateCell(11).CellFormula = String.Format(new_get_locate_cbm);
        // style
        s1.GetRow(row).GetCell(2).CellStyle = leftBold;
        s1.GetRow(row).GetCell(5).CellStyle = rightBold;
        s1.GetRow(row).GetCell(6).CellStyle = rightBold;
        for (int i = 8; i <= 11; i++)
        {
            s1.GetRow(row).GetCell(i).CellStyle = rightBold;
            s1.GetRow(row).GetCell(i).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
        }
        s1.GetRow(row).HeightInPoints = 22;
        row++;
        //end
        //set final
        int row_g_total = row;
        string[] f = { "Số lượng:", "Đóng gói:", "Trọng lượng tịnh:", "Trọng lượng:", "Khối lượng:" };
        string[] f_value = { "G", "F", "J", "K", "L" };
        string[] f_3 = { "cái, bộ", "kiện", "", "", "cbm" };
        for (int i = 0; i < f.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(0).SetCellValue(f[i]);
            s1.GetRow(row).CreateCell(1).CellFormula = String.Format(f_value[i] + row_g_total);
            s1.GetRow(row).CreateCell(2).SetCellValue(f_3[i]);
            if (f_3[i] == "")
            {
                s1.GetRow(row).CreateCell(2).SetCellValue(dt.Rows[0]["ten_trongluong"].ToString());
            }
            try
            {
                s1.GetRow(row).GetCell(0).CellStyle = celltext;
                s1.GetRow(row).GetCell(1).CellStyle = right;
                s1.GetRow(row).GetCell(2).CellStyle = celltext;
            }
            catch { }
            row++;
        }

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
            8, //end column
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
