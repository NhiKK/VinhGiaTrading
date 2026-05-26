using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InDonHangTool_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
	decimal discountPO = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        String c_donhang_id = Request.QueryString["c_donhang_id"].Substring(0,32);
        var dh = db.c_donhangs.Where(p => p.c_donhang_id.Equals(c_donhang_id)).FirstOrDefault();
        discountPO = dh.discount_hehang.GetValueOrDefault(false) ? 0 : dh.discount.GetValueOrDefault(0);
        dh.discount = dh.phanbodiscount.GetValueOrDefault(false) == true ? 0 : dh.discount;
		dh.discount_hehang = dh.phanbodiscount.GetValueOrDefault(false) == true ? false : dh.discount_hehang;
        dh.discount_hehang_value = dh.phanbodiscount.GetValueOrDefault(false) == true ? "" : dh.discount_hehang_value;

        String sql = "";
        if (dh.discount.GetValueOrDefault(0) > 0)
        {
            rptInDonHang report = new rptInDonHang();
            sql = this.CreateSql(dh);
            this.viewReport(report, sql);
        }
        else {
            rptInDonHangNoDisCount report = new rptInDonHangNoDisCount();
            sql = this.CreateSql(dh);
			this.viewReport(report, sql);
        }
		//Response.Write(sql);
    }

    public void viewReport(XtraReport report, String SqlQuery)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
        ReportViewer1.Report = report;
    }

    public String CreateSql(c_donhang dh)
    {
        var pTang = (from p in db.c_phidonhangs where p.c_donhang_id.Equals(dh.c_donhang_id) && p.phitang.Equals(true) select p.phi).Sum();
        var pGiam = (from p in db.c_phidonhangs where p.c_donhang_id.Equals(dh.c_donhang_id) && p.phitang.Equals(false) select p.phi).Sum();

        string diengiaicong = "";
        string diengiaitru = "";
			
		foreach(c_phidonhang phi_1 in db.c_phidonhangs.Where(p => p.c_donhang_id.Equals(dh.c_donhang_id) && p.phitang.Equals(true)))
		{
			// md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(s => s.md_doitackinhdoanh_id == phi_1.md_doitackinhdoanh_id);
			// diengiaicong += dtkd.ten_dtkd + ", ";
			diengiaicong += phi_1.mota + ", ";
		}
		if(diengiaicong.Length > 0) {
			diengiaicong = diengiaicong.Substring(0, diengiaicong.Length - 2);
		}
		foreach(c_phidonhang phi_2 in db.c_phidonhangs.Where(p => p.c_donhang_id.Equals(dh.c_donhang_id) && p.phitang.Equals(false)))
		{
			// md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(s => s.md_doitackinhdoanh_id == phi_2.md_doitackinhdoanh_id);
			// diengiaitru += dtkd.ten_dtkd + ", ";
			diengiaitru += phi_2.mota + ", ";
		}
		if(diengiaitru.Length > 0) {
			diengiaitru = diengiaitru.Substring(0, diengiaitru.Length - 2);
		}
        decimal tang, giam;
        tang = pTang == null ? 0 : pTang.Value;
        giam = pGiam == null ? 0 : pGiam.Value;

        decimal total = decimal.Parse(
            mdbc.ExecuteScalar2(@"
                select 
                    cast(A.amount - (A.amount * @discount / 100) as decimal(18, 2))
                from (
                    select 
                        sum(soluong * ((giafob + isnull(phi, 0)) - (giafob + isnull(phi, 0)) * isnull(discount, 0) / 100)) as amount 
                    from 
                        c_dongdonhang 
                    where 
                        c_donhang_id = @c_donhang_id
                )A
                "
                , "@c_donhang_id"
				, "nvarchar(32)"
                , dh.c_donhang_id
                , "@discount"
				, "decimal(18,6)"
                , discountPO
				, "@phanbodiscount"
				, "bit"
				, dh.phanbodiscount.GetValueOrDefault(false)
            ) + ""
        );

        total = total + tang - giam;
			
		String totalWord = MoneyToWord.ConvertMoneyToText(total.ToString()).Replace("Dollars", "");
			
		int j =  totalWord.LastIndexOf("and");
		
		if (totalWord.Contains("Cents") | totalWord.Contains("Cent"))
		{
			totalWord = totalWord.Replace("Cents", "").Replace("Cent", "");
			totalWord = totalWord.Insert(totalWord.Length, "cents");
		}
		else {
			totalWord = totalWord.Replace("Cents", "").Replace("Cent", "");
		}

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

        String str = String.Format(@"
        SELECT *, 
            CASE
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
			ELSE 0 END as g_w_master,
            
            convert(decimal(18,2),(B.total * B.dis / 100)) as discount,
			
            B.total * B.hoahong / 100 as total_hoahong
		FROM
		(
            SELECT 
                A.*
                , convert(decimal(18,2), N'{9}') as total
                , A.giafob * A.soluong as amount
                
                , (select dbo.get_commodity(N'{0}') )  + ' products' as commodity
                , '{1}' as money
                , '{4}' as diengiaicong
                , '{5}' as diengiaitru
                , isnull('{6}',0) as phicong
                , isnull('{7}',0) as phitru
                , N'{8}' as no_cont

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
					dh.c_donhang_id
					, dh.sochungtu, dh.customer_order_no
					, dtkd.ten_dtkd, dtkd.diachi, dtkd.tel, dtkd.fax
					, dh.payer, dh.ngaylap 
					, sp.ma_sanpham
					, convert(nvarchar,isnull(ddh.sl_inner, 0)) + ' ' + dvt_inner.ten_dvt as ten_donggoi_inner
					, convert(nvarchar,isnull(ddh.sl_outer, 0)) + ' ' + dvt_outer.ten_dvt as ten_donggoi_outer
					, ddh.ma_sanpham_khach, ddh.mota_tienganh
					, ddh.sl_outer , ddh.sl_inner
					, (case ddh.sl_outer when 0 then ' ' else (cast(ddh.sl_outer as nvarchar) + ' '+ (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer)) end) as ten_donggoi
					, ddh.soluong
                    , dvt.ten_dvt as dvt_sp
                    , (
                        case 
                            when isnull(dh.phanbodiscount, 0) = 0
                            then ddh.giafob + isnull(ddh.phi, 0)
                            else (ddh.giafob + isnull(ddh.phi, 0)) - (ddh.giafob + isnull(ddh.phi, 0)) * 
                                (
                                    case when dh.discount > 0 then dh.discount else ddh.discount end
                                ) / 100
                        end
                    ) as giafob
                    , cast('{2}' as decimal(18,2)) as dis
                    , N'{3}' as discount_hehang_value
					, sp.l_cm, sp.w_cm, sp.h_cm, sp.l_inch, sp.w_inch, sp.h_inch , dh.shipmenttime, cb.ten_cangbien, pmt.ten_paymentterm, (' In favor of VINH GIA COMPANY LIMITED Account No. : ' + replace(ngh.thongtin,'''','''''')) as mota
					, ddh.v2 as cbm, (sp.trongluong * ddh.soluong) as trongluong
                    ,(case ddh.sl_outer when 0 then 0 else (ddh.soluong / ddh.sl_outer) end ) as ofpack
					, (case dg.sl_inner when 0 then ' ' else (cast(dg.sl_inner as nvarchar) + ' '+ (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner)) end) as sl_inner_
					, dh.portdischarge
					, dh.amount as amountTT
					, isnull(dh.hoahong, 0) as hoahong
					, (case when md_nguoilienhe_id is null then '' else (select ma_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = dh.md_nguoilienhe_id) end) as nguoinhan
					, ddh.l1, ddh.w1, ddh.h1 , ddh.l2, ddh.w2, ddh.h2, hs.ma_hscode
					, dvt_outer.ten_dvt as dvt_outer
					, dvt_inner.ten_dvt as dvt_inner
					, (ddh.l2 + ddh.w2) * (ddh.w2 * ddh.h2) / 5400 as t_thung_outer
					, (ddh.l1 + ddh.w1) * (ddh.w1 * ddh.h1) / 5400 as t_thung_inner
					, case when dh.md_trongluong_id is not null then dg.sl_outer * (SELECT dbo.f_convertKgToPounds(sp.trongluong, dh.md_trongluong_id)) else 0 end as n_w_master
					, case when dh.md_trongluong_id is not null then dg.sl_inner * (SELECT dbo.f_convertKgToPounds(sp.trongluong, dh.md_trongluong_id)) else 0 end as n_w_inner
					, kt.ten_kichthuoc, dh.ghichu as ghichu,  (case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) + '.jpg' else SUBSTRING(sp.ma_sanpham,0,12) + '.jpg' end) as url
				from
					c_donhang dh
					left join c_dongdonhang ddh on dh.c_donhang_id = ddh.c_donhang_id
					left join md_doitackinhdoanh dtkd on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
					left join md_sanpham sp on ddh.md_sanpham_id = sp.md_sanpham_id
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
			)A
        )B
		where B.c_donhang_id = N'{0}' order by B.ma_sanpham asc",
        dh.c_donhang_id, 
		totalWord,
        dh.discount.GetValueOrDefault(0),
        dh.discount_hehang_value,
        diengiaicong.Replace("'","''"), 
        diengiaitru.Replace("'","''"), 
        tang, 
        giam, 
        no_cont,
        total
        );
        return str;
    }
}


