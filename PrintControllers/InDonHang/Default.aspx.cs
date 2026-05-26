using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using NPOI.HSSF.UserModel;

using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using HSSFUtils;
using Newtonsoft.Json;

public partial class PrintControllers_InDonHang_Default : System.Web.UI.Page
{
    public LinqDBDataContext db = new LinqDBDataContext();
    private String fmt0 = "#,##0", fmt0i0 = "#,##0.0", fmt0i00 = "#,##0.00", fmt0i000 = "#,##0.000";
    decimal discountPO = 0;
    string type = "excel";
    string c_donhang_id = "";
    List<AvariablePrj.lstImage> lstimage = new List<AvariablePrj.lstImage>();
    List<AvariablePrj.lstText> lstText = new List<AvariablePrj.lstText>();
    bool poQR = false;
    string tbl = "c_donhang", tblChild = "c_dongdonhang";
    c_donhang dhClone;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            c_donhang_id = Request.QueryString["c_donhang_id"];
            type = Request.QueryString["type"];
            var dh = db.c_donhangs.Where(s => s.c_donhang_id == c_donhang_id).FirstOrDefault();
            if (dh == null)
            {
                tbl += "qr";
                tblChild += "qr";
                var dhqr = db.c_donhangqrs.Where(s => s.c_donhangqr_id == c_donhang_id).FirstOrDefault();
                dh = new c_donhang();
                dh.c_donhang_id = dhqr.c_donhangqr_id;
                dh.discount_hehang = dhqr.discount_hehang;
                dh.discount = dhqr.discount;
                dh.phanbodiscount = false;
                dh.discount_hehang_value = dhqr.discount_hehang_value;
                dh.cont20 = dhqr.cont20;
                dh.cont40 = dhqr.cont40;
                dh.cont40hc = dhqr.cont40hc;
                dh.cont45 = dhqr.cont45;
                dh.contle = dhqr.contle;
            }

            dhClone = dh.Clone();

            var helper = new VNN_Helper();
            helper.db = db;
            helper.dh = dhClone;

            discountPO = dh.discount_hehang.GetValueOrDefault(false) ? 0 : dh.discount.GetValueOrDefault(0);
            dh.discount = dh.phanbodiscount.GetValueOrDefault(false) == true ? 0 : dh.discount;
            dh.discount_hehang = dh.phanbodiscount.GetValueOrDefault(false) == true ? false : dh.discount_hehang;
            dh.discount_hehang_value = dh.phanbodiscount.GetValueOrDefault(false) == true ? "" : dh.discount_hehang_value;

            var pTang = (from p in db.c_phidonhangs where p.c_donhang_id.Equals(c_donhang_id) && p.phitang.Equals(true) select p.phi).Sum();
            var pGiam = (from p in db.c_phidonhangs where p.c_donhang_id.Equals(c_donhang_id) && p.phitang.Equals(false) select p.phi).Sum();

            string diengiaicong = "";
            string diengiaitru = "";

            foreach (c_phidonhang phi_1 in db.c_phidonhangs.Where(p => p.c_donhang_id.Equals(c_donhang_id) && p.phitang.Equals(true)))
            {
                // md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(s => s.md_doitackinhdoanh_id == phi_1.md_doitackinhdoanh_id);
                // diengiaicong += dtkd.ten_dtkd + ", ";
                diengiaicong += phi_1.mota + ", ";
            }
            if (diengiaicong.Length > 0)
            {
                diengiaicong = diengiaicong.Substring(0, diengiaicong.Length - 2);
            }
            foreach (c_phidonhang phi_2 in db.c_phidonhangs.Where(p => p.c_donhang_id.Equals(c_donhang_id) && p.phitang.Equals(false)))
            {
                // md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(s => s.md_doitackinhdoanh_id == phi_2.md_doitackinhdoanh_id);
                // diengiaitru += dtkd.ten_dtkd + ", ";
                diengiaitru += phi_2.mota + ", ";
            }
            if (diengiaitru.Length > 0)
            {
                diengiaitru = diengiaitru.Substring(0, diengiaitru.Length - 2);
            }

            decimal tang, giam;
            tang = pTang == null ? 0 : pTang.Value;
            giam = pGiam == null ? 0 : pGiam.Value;

            var ddh = (
                from c in db.c_dongdonhangs
                join b in db.md_sanphams on c.md_sanpham_id equals b.md_sanpham_id
                where c.c_donhang_id.Equals(dhClone.c_donhang_id)
                select new VNN_Helper.dongDonHangVNN { c = c, code_cl = b.ma_sanpham.Substring(0, 2) }
            ).ToList();

            #region Tính tổng tiền
            decimal total = helper.layTongGiaTriDonHang(ddh);

            total = total + tang - giam;

            total += ddh.Sum(s => s.c.chiphidonggoi.GetValueOrDefault(0) * s.c.soluong.GetValueOrDefault(0));
            #endregion Tính tổng tiền

            String totalWord = helper.doiTienThanhChu(true, total);

            String no_cont = "";

            if (dh.cont20 != null)
            {
                if (dh.cont20.Value.Equals(0))
                {
                    no_cont += "";
                }
                else
                {
                    no_cont += String.Format(" {0} x 20\" ", dh.cont20.Value);
                }
            }

            if (dh.cont40 != null)
            {
                if (dh.cont40.Value.Equals(0))
                {
                    no_cont += "";
                }
                else
                {
                    no_cont += String.Format("+ {0} x 40\" ", dh.cont40.Value);
                }
            }

            if (dh.cont40hc != null)
            {
                if (dh.cont40hc.Value.Equals(0))
                {
                    no_cont += "";
                }
                else
                {
                    no_cont += String.Format("+ {0} x 40HC ", dh.cont40hc.Value);
                }
            }

            if (dh.cont45 != null)
            {
                if (dh.cont45.Value.Equals(0))
                {
                    no_cont += "";
                }
                else
                {
                    no_cont += String.Format("+ {0} x 45\" ", dh.cont45.Value);
                }
            }

            if (dh.contle != null)
            {
                if (dh.contle.Value.Equals(0))
                {
                    no_cont += "";
                }
                else
                {
                    no_cont += String.Format("+ {0} x LCL ", dh.contle.Value);
                }
            }
            if (!no_cont.Equals(""))
            {
                no_cont = no_cont.Substring(1);
            }

            string select = string.Format(@"
             SELECT 
                 B.*
                 , CASE
				     WHEN B.sl_inner = 0
				     THEN B.n_w_inner + B.t_thung_outer

				     WHEN B.dvt_inner like N'%ctn%' and B.dvt_outer like N'%ctn%'
				     THEN B.n_w_master + B.t_thung_outer

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
				 ELSE 0 END as g_w_master
			 FROM
			 (
                 SELECT 
                     A.*
                     , A.giafob * A.soluong as amount
                    
                     , (select dbo.get_commodity(N'{0}') )  + ' products' as commodity
                     , '{1}' as money
                     , '{4}' as diengiaicong
                     , '{5}' as diengiaitru
                     , isnull('{6}',0) as phicong
                     , isnull('{7}',0) as phitru
                     , N'{8}' as no_cont
                     , (case when isnull(A.phanbodiscount, 0) = 1 then A.discountHHVal else null end) as discountHH
                     , CASE
					     WHEN A.dvt_inner like N'%bun%'
					     THEN A.n_w_inner + 0.5
					     WHEN A.dvt_inner like N'%crt%'  
					     THEN A.n_w_inner + 3
					     WHEN A.dvt_inner like N'%ctn%'  
					     THEN A.n_w_inner + t_thung_inner
					 ELSE 0 END as g_w_inner
                    
				 FROM (
                     select 
						 dh.{9}_id
						 , dh.sochungtu
                         , dh.phanbodiscount
                         , dh.customer_order_no
						 , dtkd.ten_dtkd
                         , dtkd.diachi
                         , dtkd.tel
                         , dtkd.fax
						 , dh.payer
                         , dh.ngaylap 
						 , sp.ma_sanpham
						 , convert(nvarchar,isnull(ddh.sl_inner, 0)) + ' ' + dvt_inner.ten_dvt as ten_donggoi_inner
						 , convert(nvarchar,isnull(ddh.sl_outer, 0)) + ' ' + dvt_outer.ten_dvt as ten_donggoi_outer
						 , ddh.ma_sanpham_khach
                         , ddh.mota_tienganh
						 , ddh.sl_outer
                         , ddh.sl_inner
						 , (case ddh.sl_outer when 0 then ' ' else (cast(ddh.sl_outer as nvarchar) + ' '+ (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer)) end) as ten_donggoi
						 , ddh.soluong
                         , (case when isnull(dh.discount, 0) > 0 then isnull(dh.discount, 0) else isnull(ddh.discount, 0) end) as discountHHVal
                         , dvt.ten_dvt as dvt_sp
                         , [dbo].[admin_layGiaFOB] (dh.phanbodiscount, dh.discount, dh.dongtienISO, ddh.giafob, ddh.phi, ddh.discount) as giafob
                         , isnull('{2}', 0) as dis
						 , cast('{2}' as decimal(18,6)) as dis1
                         , N'{3}' as discount_hehang_value

                         , isnull(ddh.giachuan, ddh.giafob) as giachuan
                         , isnull(ddh.phi, 0) as phi
						 , sp.l_cm, sp.w_cm, sp.h_cm, sp.l_inch, sp.w_inch, sp.h_inch
                         , convert(varchar, dh.shipmenttime, 106) as shipmenttime
                         , cb.ten_cangbien
                         , pmt.ten_paymentterm
                         , dh.ghichu
                         , ('In favor of VINH GIA COMPANY LIMITED Account No. : ' + ngh.thongtin) as mota
						 , ddh.v2 as cbm
                         , (sp.trongluong * ddh.soluong) as trongluong
                         , (case ddh.sl_outer when 0 then 0 else (ddh.soluong / ddh.sl_outer) end ) as ofpack
						 , (case dg.sl_inner when 0 then ' ' else (cast(dg.sl_inner as nvarchar) + ' '+ (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner)) end) as sl_inner_
						 , dh.portdischarge
                         , CASE WHEN dh.hoahong is null THEN 0 ELSE dh.hoahong END as hoahong
						 , (case when md_nguoilienhe_id is null then '' else (select ma_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = dh.md_nguoilienhe_id) end) as nguoinhan
						 , ddh.l1, ddh.w1, ddh.h1 , ddh.l2, ddh.w2, ddh.h2, hs.hscode as ma_hscode
						 , dvt_outer.ten_dvt as dvt_outer
						 , dvt_inner.ten_dvt as dvt_inner
						 , (ddh.l2 + ddh.w2) * (ddh.w2 * ddh.h2) / 5400 as t_thung_outer
						 , (ddh.l1 + ddh.w1) * (ddh.w1 * ddh.h1) / 5400 as t_thung_inner
						 , case when dh.md_trongluong_id is not null then dg.sl_outer * sp.trongluong else 0 end as n_w_master
						 , case when dh.md_trongluong_id is not null then dg.sl_inner * sp.trongluong else 0 end as n_w_inner
						 , kt.ten_kichthuoc
                         , tl.ten_trongluong
                         , ddh.barcode
                         , isnull(ddh.chiphidonggoi, 0) as cpdg_vuotchuan
                         , dmhh.ten_danhmuc
                         , hhdq.mota as ttdq
                         , dg.mota as ghichudonggoi
                         , dg.vtdg2
                         , dg.nw2
                         , dg.gw2
					 from
						 {9} dh
						 left join {10} ddh on dh.{9}_id = ddh.{9}_id
						 left join md_doitackinhdoanh dtkd on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
						 left join md_sanpham sp on ddh.md_sanpham_id = sp.md_sanpham_id
                         left join md_danhmuchanghoa dmhh ON dmhh.md_danhmuchanghoa_id = sp.md_tinhtranghanghoa_id
						 left join md_donggoi dg on ddh.md_donggoi_id = dg.md_donggoi_id
						 left join md_donvitinh dvt_outer on dvt_outer.md_donvitinh_id = dg.dvt_outer
						 left join md_donvitinh dvt_inner on dvt_inner.md_donvitinh_id = dg.dvt_inner				
						 left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
						 left join md_cangbien cb on dh.md_cangbien_id = cb.md_cangbien_id
						 left join md_paymentterm pmt on dh.md_paymentterm_id = pmt.md_paymentterm_id
						 left join md_nganhang ngh on dh.md_nganhang_id = ngh.md_nganhang_id
						 left join md_kichthuoc kt on dh.md_kichthuoc_id = kt.md_kichthuoc_id
						 left join md_trongluong tl on dh.md_trongluong_id = tl.md_trongluong_id
						 left join md_hscode hs on sp.md_hscode_id = hs.md_hscode_id
                         left join md_hanghoadocquyen hhdq on hhdq.md_sanpham_id = sp.md_sanpham_id and hhdq.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
                     where 1=1
				 ) A
             ) B
			 where B.{9}_id = N'{0}' 
             order by B.ma_sanpham asc"
                , c_donhang_id == null ? "" : c_donhang_id
                , totalWord
                , dh.discount.GetValueOrDefault(0)
                , dh.discount_hehang_value
                , diengiaicong.Replace("'", "''")
                , diengiaitru.Replace("'", "''")
                , tang
                , giam
                , no_cont
                , tbl
                , tblChild
            );

            try
            {
                DataTable dt = mdbc.GetData(select);

                if (dt.Rows.Count != 0)
                {
                    HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
                    String saveAsFileName = String.Format("DonHang-{0}.xls", DateTime.Now);
                    this.SaveFile(hssfworkbook, saveAsFileName);
                }
                else
                {
                    Response.Write("<h3>Đơn hàng không có dữ liệu</h3>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex + "");
            }
        }
        catch (Exception ex)
        {
            Response.Write(String.Format("<h3>Chưa chọn PO cần chiết xuất</h3>"));

            //Response.Write(String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex.Message));
        }
    }

     public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        string discount_hehang_value = dt.Rows[0]["discount_hehang_value"] + "";
        string tenKT = dt.Rows[0]["ten_kichthuoc"] + "";
        string tenTL = dt.Rows[0]["ten_trongluong"] + "";
        var nnl = string.IsNullOrEmpty(discount_hehang_value) ? null : JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(discount_hehang_value);

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        HSSFSheet hssfsheet = (HSSFSheet)s1;

        var config = new ExportExcel(hssfworkbook, 0, type == "pdf" ? 12 : 16);
        config.NameTypePrint = "InPO";
        config.isPDF = type == "pdf";
        config.endHeader = 12;

        Excel_Format ex_fm = new Excel_Format(hssfworkbook);

        #region Style Cell
        var fontSizeRpt = double.Parse(config.layKichThuocChuChoRpt());
        var fontSizeRptShort = (short)fontSizeRpt;
        ICellStyle cellBold = ex_fm.getcell(fontSizeRptShort, true, true, "", "L", "T");
        ICellStyle cellBoldTop = ex_fm.getcell(fontSizeRptShort, true, true, "", "L", "B");
        ICellStyle cellBoldJustify = ex_fm.getcell(fontSizeRptShort, true, true, "", "L", "J");
        var cellBoldBG = ex_fm.getcell(fontSizeRptShort, true, true, "", "L", "T");
        cellBoldBG.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        cellBoldBG.FillPattern = FillPattern.SolidForeground;
        //-- 
        ICellStyle cellHeader = ex_fm.getcell((short)(fontSizeRptShort + 6), true, true, "", "C", "T");
        //-- 
        ICellStyle cellHeader_n = ex_fm.getcell(fontSizeRptShort, false, true, "", "C", "T");
        //--
        ICellStyle celltext = ex_fm.getcell(fontSizeRptShort, false, true, "", "L", "T");
        //--
        ICellStyle rightBold = ex_fm.getcell(fontSizeRptShort, true, false, "", "R", "T");
        var bottomLeftBold = ex_fm.getcell(fontSizeRptShort, true, false, "B", "L", "T");

        ICellStyle rightBold0 = ex_fm.getcell2(fontSizeRptShort, true, false, "", "R", "T", fmt0);
        ICellStyle rightBold3 = ex_fm.getcell2(fontSizeRptShort, true, false, "", "R", "T", fmt0i000);
        //--
        ICellStyle rightBold4 = ex_fm.getcell2(fontSizeRptShort, true, false, "", "R", "T", "#,##0.0000");
        //--
        ICellStyle right = ex_fm.getcell(fontSizeRptShort, false, false, "", "R", "T");
        //--
        ICellStyle leftBold = ex_fm.getcell(fontSizeRptShort, true, false, "", "L", "T");
        //--
        ICellStyle left = ex_fm.getcell(fontSizeRptShort, false, false, "", "L", "T");
        //--
        ICellStyle border_number0 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##");
        ICellStyle border_number1 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.0");
        ICellStyle border_number2 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.00");
        ICellStyle border_number3 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.000");
        ICellStyle border_number4 = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", "#,##0.0000");
        ICellStyle border = ex_fm.getcell(fontSizeRptShort, false, true, "LRBT", "L", "T");
        ICellStyle border_center = ex_fm.getcell(fontSizeRptShort, false, true, "LRBT", "C", "T");
        //--
        ICellStyle borderright = ex_fm.getcell(fontSizeRptShort, false, true, "LRBT", "L", "T");
        //--
        ICellStyle borderonlyleft = ex_fm.getcell(fontSizeRptShort, false, true, "L", "T", "T");
        //--
        ICellStyle borderWrap = ex_fm.getcell(fontSizeRptShort, true, true, "LRBT", "C", "T");
        //--
        ICellStyle signBold = ex_fm.getcell(fontSizeRptShort, true, true, "", "C", "C");

        config.cellHeader = cellHeader;
        config.cellHeader_n = cellHeader_n;
        var cellCBM = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", config.laySoThapPhanChoRpt("", "cbm"));
        var cellSoLuong = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", config.laySoThapPhanChoRpt("", "soluong"));
        var fmtDiscount = config.laySoThapPhanChoRpt("", "discount");
        var cellAmount = ex_fm.getcell2(fontSizeRptShort, false, true, "LRBT", "R", "T", config.laySoThapPhanChoRpt("", "amount"));
        var cellBoldCBM = ex_fm.getcell2(fontSizeRptShort, true, false, "", "R", "T", config.laySoThapPhanChoRpt("", "cbm"));
        var cellBoldSoLuong = ex_fm.getcell2(fontSizeRptShort, true, false, "", "R", "T", config.laySoThapPhanChoRpt("", "soluong"));
        var cellBoldAmount = ex_fm.getcell2(fontSizeRptShort, true, false, "", "R", "T", config.laySoThapPhanChoRpt("", "amount"));
        #endregion Style Cell

        int row = 0;
        //
        var columnHeaders = new List<columnHeader>();
        int width = 4500;
        int height = 22;

        #region columnHeaders
        columnHeaders.Add(new columnHeader()
        {
            text = "No.",
            value = "no",
            columnAt = columnHeaders.Count,
            width = 2000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "VINH GIA Item No.",
            value = "ma_sanpham",
            columnAt = columnHeaders.Count,
            width = 6000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Picture - Only for shape ref.",
            value = "",
            columnAt = columnHeaders.Count,
            width = width
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Customer Item No.",
            value = "ma_sanpham_khach",
            columnAt = columnHeaders.Count,
            width = width
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Description",
            value = "mota_tienganh",
            columnAt = columnHeaders.Count,
            width = type == "pdf" ? 12000 : 10000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Packing inner/master",
            value = "ten_donggoi_inner",
            columnAt = columnHeaders.Count,
            width = 3000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "ten_donggoi_outer",
            columnAt = columnHeaders.Count,
            width = 3000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Nbr of Packs",
            value = "ofpack",
            columnAt = columnHeaders.Count,
            width = width
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Quantity (sets/pcs)",
            value = "soluong",
            columnAt = columnHeaders.Count,
            width = 3000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "dvt_sp",
            columnAt = columnHeaders.Count,
            width = 3000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "FOB price (USD)",
            value = "giafob",
            columnAt = columnHeaders.Count,
            width = width
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "CBM",
            value = "cbm",
            columnAt = columnHeaders.Count,
            width = width
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Amount (USD)",
            value = "amount",
            columnAt = columnHeaders.Count,
            width = width
        });

        columnHeaders.Add(new columnHeader()
        {
            text = string.Format(@"Master Packing size (LxWxH){0}", tenKT),
            value = "l2",
            columnAt = columnHeaders.Count,
            width = type == "pdf" ? 0 : 3000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "w2",
            columnAt = columnHeaders.Count,
            width = type == "pdf" ? 0 : 3000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "h2",
            columnAt = columnHeaders.Count,
            width = type == "pdf" ? 0 : 3000
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "HS CODE",
            value = "ma_hscode",
            columnAt = columnHeaders.Count,
            width = type == "pdf" ? 0 : width
        });

        columnHeaders.Add(new columnHeader()
        {
            text = string.Format(@"Weight({0}s)", tenTL),
            value = "trongluong",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = string.Format(@"N.W/master ({0}s)", tenTL),
            value = "nw2",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = string.Format(@"G.W/master ({0}s)", tenTL),
            value = "gw2",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = string.Format(@"Packing Material ({0}s)", tenTL),
            value = "vtdg2",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Master packing",
            value = "sl_outer",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Inner packing",
            value = "sl_inner",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Inner Packing size (LxWxH)",
            value = "l1",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "w1",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "h1",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Product size (LxWxH) cm",
            value = "l_cm",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "w_cm",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "h_cm",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Product size (LxWxH) inch",
            value = "l_inch",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "w_inch",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "",
            value = "h_inch",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Barcode",
            value = "barcode",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Giá gốc",
            value = "giachuan",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Phí",
            value = "phi",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Discount (%)",
            value = "discountHH",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "CPDG vượt chuẩn",
            value = "cpdg_vuotchuan",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "TT Hàng Hóa",
            value = "ten_danhmuc",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Độc quyền",
            value = "ttdq",
            columnAt = columnHeaders.Count,
            width = 0
        });

        columnHeaders.Add(new columnHeader()
        {
            text = "Ghi chú đóng gói",
            value = "ghichudonggoi",
            columnAt = columnHeaders.Count,
            width = 0
        });

        var width02to12 = columnHeaders.Where(s => s.columnAt >= 2 & s.columnAt <= 12).Select(s => s.width).Sum();

        #endregion columnHeaders
        //
        //set A1 - A4
        row = config.createHeader(NameHeaderReport.ANCO, s1, row, height);
        //--
        //set J4 - F8
        string[] b = { "Date:", "No.:" };
        string[] b_value = { "ngaylap", "sochungtu" };
        for (int i = 0; i < b.Count(); i++)
        {
            s1.CreateRow(row).CreateCell(9).SetCellValue(b[i]);
            s1.GetRow(row).GetCell(9).CellStyle = cellBold;
            s1.AddMergedRegion(new CellRangeAddress(row, row, 10, 12));

            if (i == 0)
                s1.GetRow(row).CreateCell(10).SetCellValue(DateTime.Parse(dt.Rows[0][b_value[i]].ToString()).ToString("dd/MMM/yyyy"));
            else if (b_value[i] != "")
                s1.GetRow(row).CreateCell(10).SetCellValue(dt.Rows[0][b_value[i]].ToString());

            s1.GetRow(row).GetCell(9).CellStyle = cellBold;
            s1.GetRow(row).GetCell(10).CellStyle = cellBold;
            s1.GetRow(row).HeightInPoints = height;
            row++;
        }
        //set A7 - A11
        string[] c = {
             "The Buyer:"
             , ""
             , ""
             , "The Payer:"
             , "VINH GIA Company agrees to sale and the Buyer agrees to buy the goods under terms and conditions as follows:"
             , "Customer's order no:"
             , "Commodity:"
             , "Item nos., description, packing, quantity, unit price and amount:"
         };
        string[] c_value = {
             "ten_dtkd"
             , "diachi"
             , ""
             , "payer"
             , ""
             , "customer_order_no"
             , "commodity"
             , ""
         };

        for (int i = 0; i < c.Length; i++)
        {
            var rich_text = "";
            rich_text = i == 0 ? "{0}" : (i == 1 ? "Address: {1}" : (i == 2 ? "Tel: {2}                          Fax: {3}" : "{4}"));
            rich_text = string.Format(rich_text, dt.Rows[0]["ten_dtkd"], dt.Rows[0]["diachi"], dt.Rows[0]["tel"], dt.Rows[0]["fax"], c_value[i] == "" ? "" : dt.Rows[0][c_value[i]]);

            IRow rowCreate = null;
            if (!config.isPDF.GetValueOrDefault(false))
            {
                if (!new int[] { 1, 2 }.Contains(i))
                {
                    rowCreate = s1.CreateRow(row);
                    rowCreate.CreateCell(1).SetCellValue(c[i]);
                    rowCreate.GetCell(1).SetCellValue(c[i]);
                    rowCreate.GetCell(1).CellStyle = i > 0 ? celltext : cellBold;
                    rowCreate.CreateCell(2).SetCellValue(rich_text);
                    height = 22;
                }
                else
                {
                    row = row - 1;
                    rowCreate = s1.GetRow(row);
                    rowCreate.GetCell(2).SetCellValue(rowCreate.GetCell(2).StringCellValue + "\n" + rich_text);
                    height = 50;
                }
            }
            else
            {
                rowCreate = s1.CreateRow(row);
                rowCreate.CreateCell(1).SetCellValue(c[i]);
                rowCreate.GetCell(1).CellStyle = i > 0 ? celltext : cellBold;
                rowCreate.CreateCell(2).SetCellValue(rich_text);
                height = 22;
            }

            rowCreate.GetCell(2).CellStyle = i > 2 ? celltext : cellBold;

            if (new int[] { 0, 1, 2, 3, 5, 6 }.Contains(i))
            {
                var int012 = new int[] { 0, 1, 2 }.Contains(i);
                var int3 = new int[] { 3 }.Contains(i);
                var int5 = new int[] { 5 }.Contains(i);
                var int6 = new int[] { 6 }.Contains(i);

                lstText.Add(new AvariablePrj.lstText()
                {
                    columnFPrev = 1,
                    columnTPrev = 1,
                    columnF = 2,
                    columnT = 12,
                    left = int012 ? 60 : (int3 ? 60 : (int5 ? 15 : (int6 ? 60 : 0))),
                    top = (float)-0.5,
                    row = row,
                    bold = int012 ? true : false
                });
                s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 12));
            }
            else
            {
                s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 12));
                height = 22;
                rowCreate.GetCell(1).CellStyle = celltext;
            }

            rowCreate.HeightInPoints = height;
            row++;
        }

        // set A13 - All
        // -- Header
        IRow rowColumnHeader = s1.CreateRow(row);
        rowColumnHeader.Height = -1;
        foreach (var columnHeader in columnHeaders.OrderBy(s => s.columnAt))
        {
            rowColumnHeader.CreateCell(columnHeader.columnAt).SetCellValue(columnHeader.text);
            rowColumnHeader.GetCell(columnHeader.columnAt).CellStyle = borderWrap;
            s1.SetColumnWidth(columnHeader.columnAt, columnHeader.width);
        }
        s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 6));
        s1.AddMergedRegion(new CellRangeAddress(row, row, 8, 9));
        s1.AddMergedRegion(new CellRangeAddress(row, row, 13, 15));
        s1.AddMergedRegion(new CellRangeAddress(row, row, 23, 25));
        s1.AddMergedRegion(new CellRangeAddress(row, row, 26, 28));
        s1.AddMergedRegion(new CellRangeAddress(row, row, 29, 31));

        row++;
        // -- Details
        int count_row = 1;
        //
        double discount = 0, discount1 = 0, startRowHH = -1;
        var lstHH = new List<int>();
        var lstAmountHH = new List<int>();
        // create detail row
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var msp = dt.Rows[i]["ma_sanpham"].ToString();
            var mota = dt.Rows[i]["mota_tienganh"] + "";
            var masterSize = new List<string>();
            masterSize.Add(dt.Rows[i]["l2"] + "");
            masterSize.Add(dt.Rows[i]["w2"] + "");
            masterSize.Add(dt.Rows[i]["h2"] + "");

            var cmInch = Public.convertCmToInch(mota, masterSize, tenKT);
            dt.Rows[i]["mota_tienganh"] = cmInch["mota"];
            masterSize = cmInch["masterSize"] as List<string>;
            dt.Rows[i]["l2"] = masterSize[0];
            dt.Rows[i]["w2"] = masterSize[1];
            dt.Rows[i]["h2"] = masterSize[2];

            var kgPound = Public.convertKgToPound(
                dt.Rows[i]["trongluong"] + ""
                , dt.Rows[i]["n_w_master"] + ""
                , dt.Rows[i]["g_w_master"] + ""
                , tenTL
                );
            dt.Rows[i]["trongluong"] = kgPound["weight"];
            dt.Rows[i]["n_w_master"] = kgPound["NWmaster"];
            dt.Rows[i]["g_w_master"] = kgPound["GWmaster"];

            var chungloai = msp.Substring(0, 2);
            var chungloaiTT = i == dt.Rows.Count - 1 ? chungloai : dt.Rows[i + 1]["ma_sanpham"].ToString().Substring(0, 2);

            if (startRowHH == -1)
                startRowHH = row;

            var valDis = dt.Rows[i]["dis"].ToString();
            discount = double.Parse(string.IsNullOrWhiteSpace(valDis) ? "0" : valDis);

            var valDis1 = dt.Rows[i]["dis1"].ToString();
            discount1 = double.Parse(string.IsNullOrWhiteSpace(valDis1) ? "0" : valDis1);

            IRow row_t = s1.CreateRow(row);
            float heightMAX = config.MeasureTextHeight(mota, 10000, "Arial", fontSizeRptShort);
            row_t.HeightInPoints = heightMAX < 60 ? 60 : heightMAX;

            //
            try
            {
                var imgPath = ExcuteSignalRStatic.mapPathSignalR("~/" + ExcuteSignalRStatic.getImageProduct(msp));

                lstimage.Add(new AvariablePrj.lstImage
                {
                    row = row,
                    column = 3,
                    link = imgPath
                });
            }
            catch { }
            //

            foreach (var columnHeader in columnHeaders.OrderBy(s => s.columnAt))
            {
                var cel_row_t = row_t.CreateCell(columnHeader.columnAt);
                if (new string[] { "no" }.Contains(columnHeader.value))
                {
                    cel_row_t.SetCellValue(count_row);
                    cel_row_t.CellStyle = border_center;
                }
                else if (new string[] { "ofpack" }.Contains(columnHeader.value))
                {
                    cel_row_t.CellFormula = String.Format("I{0}/V{0}", row + 1);
                    cel_row_t.CellStyle = border_number2;
                }
                else if (new string[] { "cbm" }.Contains(columnHeader.value))
                {
                    cel_row_t.CellFormula = String.Format("((N{0}*O{0}*P{0})*H{0})/1000000", row + 1);
                    cel_row_t.CellStyle = cellCBM;
                }
                else if (new string[] { "amount" }.Contains(columnHeader.value))
                {
                    cel_row_t.CellFormula = String.Format("K{0}*I{0}", row + 1);
                    cel_row_t.CellStyle = cellAmount;
                }
                else if (new string[] { "soluong" }.Contains(columnHeader.value))
                {
                    var val = dt.Rows[i][columnHeader.value].ToString();
                    cel_row_t.SetCellValue(double.Parse(string.IsNullOrWhiteSpace(val) ? "0" : val));
                    cel_row_t.CellStyle = cellSoLuong;
                }
                else if (new string[] { "trongluong" }.Contains(columnHeader.value))
                {
                    var val = dt.Rows[i][columnHeader.value].ToString();
                    cel_row_t.SetCellFormula(string.Format("H{0}*IF(ISNUMBER(T{0}), T{0}, 0)", row_t.RowNum + 1));
                    cel_row_t.CellStyle = cellAmount;
                }
                else if (new string[] { "giafob", "giachuan", "phi", "cpdg_vuotchuan" }.Contains(columnHeader.value))
                {
                    var val = dt.Rows[i][columnHeader.value].ToString();
                    cel_row_t.SetCellValue(Math.Round(double.Parse(string.IsNullOrWhiteSpace(val) ? "0" : val), 2));
                    cel_row_t.CellStyle = cellAmount;
                }
                else if (new string[] { "l2", "w2", "h2" }.Contains(columnHeader.value))
                {
                    var val = dt.Rows[i][columnHeader.value].ToString();
                    cel_row_t.SetCellValue(double.Parse(string.IsNullOrWhiteSpace(val) ? "0" : val));
                    cel_row_t.CellStyle = border_number2;
                }
                else if (new string[] { "ma_hscode", "ma_sanpham" }.Contains(columnHeader.value))
                {
                    cel_row_t.SetCellValue(dt.Rows[i][columnHeader.value].ToString());
                    cel_row_t.CellStyle = border_center;
                }
                else if (new string[] { "discountHH" }.Contains(columnHeader.value))
                {
                    cel_row_t.SetCellValue(dt.Rows[i][columnHeader.value].ToString());
                    cel_row_t.CellStyle = border_number4;
                }
                // else if( e_value[j] == "ten_donggoi_inner") {
                // cel_row_t.SetCellValue(dt.Rows[i][e_value[j]].ToString().Replace(" ","\n"));
                // cel_row_t.CellStyle = border_center;
                // }
                else if (columnHeader.value == "")
                {
                    cel_row_t.SetCellValue("");
                    cel_row_t.CellStyle = border;
                }
                else
                {
                    cel_row_t.SetCellValue(dt.Rows[i][columnHeader.value].ToString());
                    cel_row_t.CellStyle = border;
                }
                //  table style


                // else if(j == 8)
                // s1.GetRow(row).GetCell(j).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##");
                // else
                // s1.GetRow(row).GetCell(j).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
            }
            row++;
            count_row++;

            if (nnl != null)
            {
                if (chungloai != chungloaiTT | i == dt.Rows.Count - 1)
                {
                    string gthh = nnl == null ? "0" : nnl.Where(s => s["hehang"].ToString() == chungloai).Select(s => s["giatri"].ToString()).FirstOrDefault();
                    gthh = string.IsNullOrWhiteSpace(gthh) ? "0" : gthh;
                    var rowToTalHeHang = s1.CreateRow(row);
                    rowToTalHeHang.HeightInPoints = height;
                    rowToTalHeHang.CreateCell(4).SetCellValue(string.Format("Sub Total ({0}):", lstHH.Count + 1));
                    var range1_3 = new CellRangeAddress(row, row, 1, 3);
                    var range4_6 = new CellRangeAddress(row, row, 4, 6);
                    s1.AddMergedRegion(range1_3);
                    s1.AddMergedRegion(range4_6);
                    rowToTalHeHang.GetCell(4).CellStyle = leftBold;

                    string fmlSum = string.Format(@"SUM(I{0}:I{1})", startRowHH + 1, row);
                    rowToTalHeHang.CreateCell(8).CellFormula = string.Format("{0}", fmlSum, row + 1);
                    rowToTalHeHang.GetCell(8).CellStyle = cellBoldSoLuong;

                    fmlSum = string.Format(@"SUM(L{0}:L{1})", startRowHH + 1, row);
                    rowToTalHeHang.CreateCell(11).CellFormula = string.Format("{0}", fmlSum, row + 1);
                    rowToTalHeHang.GetCell(11).CellStyle = cellBoldCBM;

                    fmlSum = string.Format(@"SUM(M{0}:M{1})", startRowHH + 1, row);
                    rowToTalHeHang.CreateCell(12).CellFormula = string.Format("{0}", fmlSum, row + 1);
                    rowToTalHeHang.GetCell(12).CellStyle = cellBoldAmount;
                    row++;

                    var rowDisHeHang = s1.CreateRow(row);
                    rowDisHeHang.HeightInPoints = height;
                    rowDisHeHang.CreateCell(4).SetCellValue(string.Format(@"Discount ({0}%):", double.Parse(gthh).ToString(fmtDiscount)));
                    rowDisHeHang.GetCell(4).CellStyle = leftBold;
                    s1.AddMergedRegion(new CellRangeAddress(row, row, 4, 10));
                    rowDisHeHang.CreateCell(12).CellFormula = string.Format("ROUND(M{0}*{1}/100, 2)", row, gthh);
                    rowDisHeHang.GetCell(12).CellStyle = cellBoldAmount;
                    row++;

                    var rowTotalHeHang = s1.CreateRow(row);
                    rowTotalHeHang.HeightInPoints = height;
                    rowTotalHeHang.CreateCell(4).SetCellValue(string.Format("Total ({0}):", lstHH.Count + 1));
                    rowTotalHeHang.GetCell(4).CellStyle = leftBold;
                    s1.AddMergedRegion(new CellRangeAddress(row, row, 4, 10));
                    rowTotalHeHang.CreateCell(12).CellFormula = string.Format("M{0} - M{1}", row - 1, row);
                    rowTotalHeHang.GetCell(12).CellStyle = cellBoldAmount;

                    lstAmountHH.Add(row);

                    row++;
                    count_row++;
                    startRowHH = row;
                    lstHH.Add(row);
                }
            }
        }

        //vi tri row dau tien
        int m = row - count_row;
        int sub = row + 1;

        string sumQuantify = "",
            sumCBM = "",
            sumAmount = "",
            sumTotalAmount = "";
        if (nnl != null)
        {
            s1.CreateRow(row).CreateCell(4).SetCellValue("Sub Total:");
            s1.GetRow(row).HeightInPoints = height;
            foreach (var i in lstAmountHH)
            {
                sumQuantify += string.Format("I{0}+", i - 1);
                sumCBM += string.Format("L{0}+", i - 1);
                sumAmount += string.Format("M{0}+", i - 1);
                sumTotalAmount += string.Format("M{0}+", i + 1);
            }
            sumQuantify += "0";
            sumCBM += "0";
            sumAmount += "0";
            sumTotalAmount += "0";

            s1.GetRow(row).CreateCell(7).CellFormula = string.Format("SUM(H{0}:H{1})", m, row - 3);
            s1.GetRow(row).CreateCell(8).CellFormula = string.Format("{0}", sumQuantify);
            s1.GetRow(row).CreateCell(11).CellFormula = string.Format("{0}", sumCBM);
            s1.GetRow(row).CreateCell(12).CellFormula = string.Format("{0}", sumAmount);
        }
        else
        {
            s1.CreateRow(row).CreateCell(4).SetCellValue("Sub Total:");
            s1.GetRow(row).HeightInPoints = height;
            s1.GetRow(row).CreateCell(7).CellFormula = String.Format("SUM(H{0}:H{1})", m, row);
            s1.GetRow(row).CreateCell(8).CellFormula = String.Format("SUM(I{0}:I{1})", m, row);
            s1.GetRow(row).CreateCell(11).CellFormula = String.Format("SUM(L{0}:L{1})", m, row);
            s1.GetRow(row).CreateCell(12).CellFormula = String.Format("SUM(M{0}:M{1})", m, row);
        }

        //s1.GetRow(13).GetCell(0).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.0");

        //tru so dong du
        //start tong cong total

        //start tong cong 1

        s1.GetRow(row).CreateCell(20).SetCellValue(discount);
        // style
        s1.GetRow(row).GetCell(4).CellStyle = leftBold;
        s1.GetRow(row).GetCell(7).CellStyle = rightBold;
        s1.GetRow(row).GetCell(8).CellStyle = cellBoldSoLuong;
        s1.GetRow(row).GetCell(11).CellStyle = cellBoldCBM;
        s1.GetRow(row).GetCell(12).CellStyle = cellBoldAmount;


        // for(int nht = 7; nht <= 7; nht++)
        // {
        // s1.GetRow(row).GetCell(nht).CellStyle = rightBold;
        // s1.GetRow(row).GetCell(nht).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
        // }						
        row++;
        //end
        //start giam gia

        int dis = row + 1;
        if (nnl == null)
        {
            s1.CreateRow(row).CreateCell(4).SetCellValue("Discount (" + discount1.ToString(fmtDiscount) + "%):");

            string str_dsc1 = discount.ToString();
            string funcDiscount = "ROUND(M{0}*U{0}/100,2)";

            s1.GetRow(row).CreateCell(12).CellFormula = String.Format(funcDiscount, row, row);
            s1.GetRow(row).GetCell(4).CellStyle = leftBold;
            s1.GetRow(row).GetCell(12).CellStyle = cellBoldAmount;
            if (discount1 <= 0)
            {
                s1.GetRow(row).HeightInPoints = 0;
                s1.GetRow(row).ZeroHeight = true;
                if (type == "pdf")
                {
                    s1.GetRow(row).RemoveCell(s1.GetRow(row).GetCell(4));
                    s1.GetRow(row).RemoveCell(s1.GetRow(row).GetCell(12));
                }
            }
            else
                s1.GetRow(row).HeightInPoints = height;
            row++;
        }

        //end
        var rowTotalCPDG = row + 1;
        s1.CreateRow(row).CreateCell(4).SetCellValue("Additional packaging fees");
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).CreateCell(12).CellFormula = String.Format("SUMPRODUCT(I{0}:I{1}, AK{0}:AK{1})", 1, row);
        // style
        s1.GetRow(row).GetCell(4).CellStyle = leftBold;
        s1.GetRow(row).GetCell(12).CellStyle = cellBoldAmount;
        row++;
        int cong = row + 1;
        s1.CreateRow(row).CreateCell(4).SetCellValue("(+) " + dt.Rows[0]["diengiaicong"].ToString());
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).CreateCell(12).CellFormula = String.Format("Value(\"{0}\")", dt.Rows[0]["phicong"].ToString());
        // style
        s1.GetRow(row).GetCell(4).CellStyle = leftBold;
        s1.GetRow(row).GetCell(12).CellStyle = cellBoldAmount;
        row++;
        //
        int tru = row + 1;
        s1.CreateRow(row).CreateCell(4).SetCellValue("(-) " + dt.Rows[0]["diengiaitru"].ToString());
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).CreateCell(12).CellFormula = String.Format("Value(\"{0}\")", dt.Rows[0]["phitru"].ToString());
        // style
        s1.GetRow(row).GetCell(4).CellStyle = leftBold;
        s1.GetRow(row).GetCell(12).CellStyle = cellBoldAmount;
        row++;
        //
        string totalTxt = "";
        for (var i = 0; i < lstAmountHH.Count; i++)
            totalTxt += string.Format("({0})+", i + 1);
        totalTxt = totalTxt.Length > 0 ? totalTxt.Substring(0, totalTxt.Length - 1) : "";
        string txtTotal = nnl == null ? "Total:" : string.Format("Total {0}:", totalTxt);
        s1.CreateRow(row).CreateCell(4).SetCellValue(txtTotal);
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).CreateCell(12).CellFormula = String.Format("{0} - {1} + M{2} - M{3} + M{4}"
            , nnl == null ? "M" + sub : sumTotalAmount
            , nnl == null ? "M" + dis : "0"
            , cong
            , tru
            , rowTotalCPDG
            );
        s1.GetRow(row).GetCell(4).CellStyle = leftBold;
        s1.GetRow(row).GetCell(12).CellStyle = rightBold;
        s1.GetRow(row).GetCell(12).CellStyle.DataFormat = CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
        row++;
        //
        var hoaHongVal = double.Parse(dt.Rows[0]["hoahong"].ToString());
        var hoaHongRow = string.Format(@"Hoa hồng: {0}%          Người nhận: {1}", hoaHongVal.ToString("#,#0.00"), dt.Rows[0]["nguoinhan"].ToString());
        s1.CreateRow(row).CreateCell(0).SetCellValue(hoaHongRow);
        if (hoaHongVal > 0)
            s1.GetRow(row).HeightInPoints = height;
        else
            s1.GetRow(row).ZeroHeight = true;

        s1.GetRow(row).GetCell(0).CellStyle = rightBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 12));
        row++;
        //
        s1.CreateRow(row).CreateCell(0).SetCellValue("Say: " + dt.Rows[0]["money"].ToString());
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).GetCell(0).CellStyle = rightBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 12));
        row++;
        //
        s1.CreateRow(row).CreateCell(0).SetCellValue("(10% more or less in quantity and amount are acceptable).");
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).GetCell(0).CellStyle = rightBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 12));
        row++;
        //set A6 - A12
        string[] f = { "Note:", "No. of Container(s):", "Port of loading:", "Port of discharge:", "Shipment Time:", "Payment term:", "Bank Details:" };
        string[] f_value = { "ghichu", "no_cont", "ten_cangbien", "portdischarge", "shipmenttime", "ten_paymentterm", "mota" };
        for (int i = 0; i < f.Count(); i++)
        {
            var text = dt.Rows[0][f_value[i]].ToString().Trim();
            s1.CreateRow(row).CreateCell(1).SetCellValue(f[i]);
            s1.GetRow(row).CreateCell(2).SetCellValue(text);
            height = 22;
            if (f_value[i] == "mota")
            {
                var dong0Text = "In favor of VINH GIA COMPANY LIMITED";
                var dong1Text = "Account No.: 046.137.3860862";
                var dong2Text = "At bank: JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE OF VIET NAM - TAN BINH DUONG BRANCH";
                var dong3Text = "         NO 16, LE TRONG TAN STREET, BINH DUONG 2 QUARTER, DI AN WARD, HO CHI MINH CITY, VIET NAM";
                var dong4Text = "         Tel: (84-274) 3792 158- Fax: (84-274) 3793 970";
                var dong5Text = "         Swift code: BFTVVNVX";

                text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", dong0Text, dong1Text, dong2Text, dong3Text, dong4Text, dong5Text);
                height = 114;
                s1.GetRow(row).GetCell(2).SetCellValue(text);
                s1.GetRow(row).GetCell(1).CellStyle = cellBold;
                s1.GetRow(row).GetCell(2).CellStyle = cellBold;
                s1.GetRow(row).GetCell(2).CellStyle.VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (f_value[i] == "no_cont")
            {
                s1.GetRow(row).GetCell(1).CellStyle = cellBoldBG;
                s1.GetRow(row).GetCell(2).CellStyle = cellBoldBG;
                lstText.Add(new AvariablePrj.lstText()
                {
                    columnF = 2,
                    columnT = 12,
                    columnFPrev = 1,
                    columnTPrev = 1,
                    row = row,
                    minWidth = 140
                });
            }
            else
            {
                s1.GetRow(row).GetCell(1).CellStyle = cellBold;
                s1.GetRow(row).GetCell(2).CellStyle = cellBold;
            }

            s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 1));
            s1.AddMergedRegion(new CellRangeAddress(row, row, 2, 12));
            s1.GetRow(row).HeightInPoints = height;


            row++;
        }
        height = 22;
        //
        s1.CreateRow(row).CreateCell(1).SetCellValue("This agreement comes into effect from the signing date.:");
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).GetCell(1).CellStyle = left;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 1, 12));
        row++;
        row++;
        //
        s1.CreateRow(row).CreateCell(0).SetCellValue("CONFIRMED BY THE BUYER");
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).GetCell(0).CellStyle = signBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 0, 4));
        //
        s1.GetRow(row).CreateCell(5).SetCellValue("VINH GIA COMPANY LIMITED");
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).GetCell(5).CellStyle = signBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 12));
        row++;
        //
        s1.CreateRow(row).CreateCell(5).SetCellValue("(Signed)");
        s1.GetRow(row).HeightInPoints = height;
        s1.GetRow(row).GetCell(5).CellStyle = signBold;
        s1.AddMergedRegion(new CellRangeAddress(row, row, 5, 12));
        row++;

        ICellStyle cellUn = hssfworkbook.CreateCellStyle();
        IFont funderline = hssfworkbook.CreateFont();
        funderline.Underline = FontUnderlineType.Single;
        funderline.FontHeightInPoints = fontSizeRptShort;
        cellUn.SetFont(funderline);
        cellUn.Alignment = HorizontalAlignment.Center;
        cellUn.VerticalAlignment = VerticalAlignment.Top;
        //s1.GetRow(2).GetCell(0).CellStyle = cellUn;
        s1.GetRow(3).GetCell(0).CellStyle = cellUn;

        s1 = config.PrintExcel((HSSFSheet)s1, row);

        if (type == "pdf")
        {
            s1.PrintSetup.Landscape = false;
            s1.PrintSetup.PaperSize = (short)PaperSize.A4;
        }
        else
            s1.PrintSetup.Landscape = true;

        return hssfworkbook;
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName)
    {
        //xuất Excel không áp dụng Spire.Xls
        //if (type != "pdf")
        //{
        //    foreach (var item in lstimage)
        //    {
        //        var image = System.Drawing.Image.FromFile(item.link);
        //        MemoryStream ms = new MemoryStream();
        //        //pull the memory stream from the image (I need this for the byte array later)
        //        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        image.Dispose();
        //        //the drawing patriarch will hold the anchor and the master information
        //        IDrawing patriarch = hsswb.GetSheetAt(0).CreateDrawingPatriarch();
        //        //store the coordinates of which cell and where in the cell the image goes
        //        int intx1 = item.column - 1;
        //        int inty1 = item.row;
        //        int intx2 = item.column;
        //        int inty2 = 1 + item.row;

        //        HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, intx1, inty1, intx2, inty2);
        //        //types are 0, 2, and 3. 0 resizes within the cell, 2 doesn’t
        //        anchor.AnchorType = 0;
        //        //add the byte array and encode it for the excel file
        //        int index = hsswb.AddPicture(ms.ToArray(), PictureType.JPEG);
        //        IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
        //    }

        //    MemoryStream exportData = new MemoryStream();
        //    hsswb.Write(exportData);
        //    hsswb.Clear();

        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
        //    Response.Clear();
        //    Response.BinaryWrite(exportData.GetBuffer());
        //    Response.End();
        //}
        ////xuất Excel có áp dụng Spire.Xls
        //else
        {
            var fileName = "PO_" + c_donhang_id + "_" + Guid.NewGuid().ToString();
            string url = Server.MapPath(string.Format(@"ReportPdf/{0}.xls", fileName));
            url = url.Replace("PrintControllers\\InDonHang\\", "");

            var xfile = new FileStream(url, FileMode.Create, System.IO.FileAccess.ReadWrite);
            hsswb.Write(xfile);
            xfile.Close();
            xfile.Dispose();

            if (type == "pdf")
            {
                var urlPDF = url.Replace(fileName + ".xls", fileName + ".pdf");
                var result = OfficeToPDF.ExcelConverter.Convert(url, urlPDF, null, lstimage, lstText);
                File.Delete(url);
                Response.Write(result);
                if (result.Length <= 0)
                    Response.Redirect(string.Format("../../ViewPDFPublic/index.aspx?urlpdf=/ReportPdf/{0}.pdf&zoomprint=0.999&zoom=page-width&remove=true", fileName));
            }
            else
            {
                var urlXls = url.Replace(fileName + ".xls", fileName + "1.xls");
                var excel = new OfficeToPDF.ExcelConverter();
                foreach (var img in lstimage)
                    img.column -= 1;
                excel.lstImage = lstimage;
                //excel.lstText = lstText;
                var result = excel.ConvertInterop(url, urlXls, null);
                File.Delete(url);
                Response.Write(result);
                if (result.Length <= 0)
                {
                    var data = "";
                    data += "oper=downloadFile";
                    data += string.Format("&name={0}", saveAsFileName);
                    data += string.Format("&path=ReportPdf/{0}1.xls", fileName);
                    data += string.Format("&remove=true");
                    Response.Redirect(string.Format("../../Controllers/API_System.ashx?{0}", data));
                }
            }
        }
    }
}
