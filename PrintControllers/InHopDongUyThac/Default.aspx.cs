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

public partial class PrintControllers_InHopDongUyThac_Default : System.Web.UI.Page
{
    public string logo = "", sothapphan = "", inPDF = "";
    public InfoPrint infoPrint = PrintAnco2.GetInfoPrint();
    public HttpContext context = HttpContext.Current;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var context = HttpContext.Current;
            sothapphan = PrintAnco2.GetDecimal(context.Request.QueryString["stp"], 1);

            string nameTemp = "(CT) Hợp đồng ủy thác.xls";
            string nameRpt = "Hợp đồng ủy thác";
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
        catch (Exception ex)
        {
            Response.Write(ex + "");
        }
    }

    public void viewReport(String SqlQuery)
    {

    }

    public string CreateSql(HttpContext context)
    {
        String c_packinglist_id = context.Request.QueryString["pklId"];
        String hangcua = context.Request.QueryString["hangcua"];
        string benA = "VINHGIA";
        string logoA = "images/more/vg_hdut.jpg";
        string logoB = "images/more/hhh_hdut.jpg";
        if (hangcua == "VG")
        {
            hangcua = "VINHGIA";
            benA = "HOANGHUYHUNG";
            logoA = "images/more/hhh_hdut.jpg";
            logoB = "images/more/vg_hdut.jpg";
        }

        string sql = string.Format(@"	 
            select 
                CONVERT(decimal(18,2),(select 
			        sum(isnull(dpkl.soluong, 0))
			        --isnull(dpkl.thanhtien, 0) as thanhtien
		        from c_packinginvoice pkl
		        left join c_dongpklinv dpkl on dpkl.c_packinginvoice_id = pkl.c_packinginvoice_id
		        where pkl.c_packinginvoice_id = '{0}')) as sum_sl,

		  --      CONVERT(decimal(18,2),(select 
			 --       sum(isnull(dpkl.thanhtien, 0))
			 --       --isnull(dpkl.thanhtien, 0) as thanhtien
		  --      from c_packinginvoice pkl
		  --      left join c_dongpklinv dpkl on dpkl.c_packinginvoice_id = pkl.c_packinginvoice_id
		  --      where pkl.c_packinginvoice_id = '{0}')) as sum_thanhtien,

    --            CONVERT(decimal(18,2),(select 
			 --       sum(isnull(dpkl.thanhtien, 0))
			 --       --isnull(dpkl.thanhtien, 0) as thanhtien
		  --      from c_packinginvoice pkl
		  --      left join c_dongpklinv dpkl on dpkl.c_packinginvoice_id = pkl.c_packinginvoice_id
		  --      where pkl.c_packinginvoice_id = '{0}')) 
				--- (isnull(pkl.giatri_tru,0) + isnull(pkl.giatritru_po,0) + isnull(pkl.totaldis, 0))as sum_cuoi,

				isnull(pkl.totalnet,0) as sum_thanhtien,
				isnull(pkl.tienconlai,0) as sum_cuoi,
	            sp.mota_tiengvietVG as mota_tiengviet,
	            isnull(dpkl.soluong, 0) as sl,
	            isnull(dpkl.gia, 0) as dongia,
	            isnull(dpkl.thanhtien, 0) as thanhtien,
                --Ben A
                upper(dtkdA.ten_dtkd) as ctyA,
                dtkdA.masothue as mstA,
                dtkdA.diachi as diachiA,
                dtkdA.daidien as daidienA,
	            --Ben B
                upper(dtkd.ten_dtkd) + N' (Bên nhận ủy thác)' as tencty,
	            N'MST:' + dtkd.masothue as mst,
	            N'Địa chỉ:' + dtkd.diachi as diachi,
	            N'Do ông/bà:' + dtkd.daidien + N' - Làm đại diện' as daidien,
	            pkl.hopdonguythac as hopdonguythac,
	            (select top(1) FORMAT(dh.ngaylap, 'dd/MM/yyyy')
			            from 
				            c_donhang dh, c_dongpklinv dpkl
			            where dh.c_donhang_id = dpkl.c_donhang_id
				            AND dpkl.c_packinginvoice_id = pkl.c_packinginvoice_id) as ngaysalecontract,
	            FORMAT(DATEADD(day, 10, pkl.ngaylap),'dd/MM/yyyy') as ngay_motokhai,
                '{3}' as picture_logoA,
                '{4}' as picture_logoB,
	            isnull(pkl.giatri_tru,0) + isnull(pkl.giatritru_po,0) + isnull(pkl.totaldis, 0) as giamgia,
				isnull(pkl.giatri_cong,0) + isnull(pkl.giatricong_po,0) + isnull(pkl.cpdg_vuotchuan, 0) as cong
            from c_packinginvoice pkl
            left join c_dongpklinv dpkl on dpkl.c_packinginvoice_id = pkl.c_packinginvoice_id
            left join md_sanpham sp on sp.md_sanpham_id = dpkl.md_sanpham_id
            left join md_doitackinhdoanh dtkd on dtkd.ma_dtkd = '{1}'
            left join md_doitackinhdoanh dtkdA on dtkdA.ma_dtkd = '{2}'
            where pkl.c_packinginvoice_id = '{0}'
            order by sp.ma_sanpham               
		"
        , c_packinglist_id
        , hangcua
        , benA
        , logoA
        , logoB
        );
        return sql;
    }
}
