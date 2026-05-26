using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;

public partial class PrintControllers_InKeHoachXuatHang_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dateFrom = DateTime.ParseExact(Request.QueryString["from"], "dd/MM/yyyy", null);
        DateTime dateTo = DateTime.ParseExact(Request.QueryString["to"], "dd/MM/yyyy", null);
        String md_doitackinhdoanh_id = Request.QueryString["md_doitackinhdoanh_id"];
        string chkManv = Request.QueryString["manv"];
		string dateType = Request.QueryString["dateType"];
        // phân quyền theo nhóm
        String manv = UserUtils.getUser(Context);
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
		
		//trang thai
        string tt_ST = Request.QueryString["tt_ST"];
        string tt_HL = Request.QueryString["tt_HL"];
        string tt_KT = Request.QueryString["tt_KT"];
		
        String dsNhanVien = "";

        String strAccount = "";
        if (chkManv != null) // có chọn user thì chỉ lọc theo user đã chọn
        {
            strAccount = chkManv;
            dsNhanVien = String.Format(" AND khxh.nguoitao IN('{0}') ", strAccount);
        }
        else // ngược lại thì phân quyền theo nhóm
        {
            
            System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
            foreach (String item in lstAccount)
            {
                strAccount += String.Format(", '{0}'", item);
            }
            strAccount = String.Format("'{0}'{1}", manv, strAccount);
            dsNhanVien = nv.isadmin.Value ? "" : String.Format(" AND khxh.nguoitao IN({0}) ", strAccount);
        }

        

        rptLicXuatHang report = new rptLicXuatHang();
        String sql = this.CreateSql(dateFrom, dateTo, dsNhanVien, dateType, tt_ST, tt_HL, tt_KT);
        
        if (md_doitackinhdoanh_id != null)
        {
            sql += String.Format(" AND khxh.md_doitackinhdoanh_id = '{0}'", md_doitackinhdoanh_id);
        }
		
		sql += string.Format(" order by {0} asc ", dateType =="0"? "khxh.ngaygiaohang" : "khxh.ngayxonghang");

        
        this.viewReport(report, sql);
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

    public String CreateSql(DateTime dateFrom, DateTime dateTo, String dsNhanVien, string dateType, string tt_ST, string tt_HL, string tt_KT)
    {
		string sql_them = "", sql_them2 = "";
        int dem = 0;
        if (tt_ST == "on")
        {
            sql_them2 += " and khxh.md_trangthai_id = 'SOANTHAO'";
            dem++;
        }

        if (tt_HL == "on")
        {
            sql_them2 += " and khxh.md_trangthai_id = 'HIEULUC'";
            dem++;
        }
        
        if (tt_KT == "on")
        {
            sql_them2 += " and khxh.md_trangthai_id = 'KETTHUC'";
            dem++;
        }

        if (dem == 0)
        {
            sql_them2 = "";
        }
        else if (dem == 1)
        {
            sql_them += sql_them2;
        }
        else
        {
            string chuoi = sql_them2.Substring(4).Replace("and", "or");
            sql_them2 = " and (" + chuoi + ")";
            sql_them += sql_them2;
        }
		
        String str = String.Format(@"select khxh.so_po, khxh.chungloaihang, khxh.cbm, khxh.cbm_conlai, khxh.shipmenttime, khxh.c_danhsachdathang_id
	                    , khxh.ngaygiaohang, khxh.ngayxonghang, khxh.cont20, khxh.cont40, khxh.cont40hc
	                    , khxh.ngayxuathang as ngaymotokhai, khxh.ghichu, khxh.ketquakiemhang, khxh.ngaykiemhang
                        , dtkd.ma_dtkd as nhacungung, cast('{0}' as datetime) as tungay 
                        , cast('{1}' as datetime) as denngay
                        , (case when khxh.khongtem = 0 and khxh.ngayxacnhantem is not null then (cast(datepart(d, khxh.ngayxacnhantem) as nvarchar) +'/'+ cast(datepart(m, khxh.ngayxacnhantem) as nvarchar)+'/'+ cast(datepart(yy, khxh.ngayxacnhantem) as nvarchar))
			                    when khxh.khongtem = 0 and khxh.ngayxacnhantem is null then ' '
			                    when khxh.khongtem = 1 then 'null'
			                    else ' '
			                    end )as ngayxacnhantem

	                    , (case when khxh.khongbaobi = 0 and khxh.ngayxacnhanbaobi is not null then (cast(datepart(d, khxh.ngayxacnhanbaobi) as nvarchar) +'/'+ cast(datepart(m, khxh.ngayxacnhanbaobi) as nvarchar)+'/'+ cast(datepart(yy, khxh.ngayxacnhanbaobi) as nvarchar))
			                    when khxh.khongbaobi = 0 and khxh.ngayxacnhanbaobi is null then ' '
			                    when khxh.khongbaobi = 1 then 'null'
			                    else ' '
			                    end )as ngayxacnhanbaobi
                        , (case when khxh.khongkhachkiem = 0 and khxh.ngaykhachkiem is not null then (cast(datepart(d, khxh.ngaykhachkiem) as nvarchar) +'/'+ cast(datepart(m, khxh.ngaykhachkiem) as nvarchar)+'/'+ cast(datepart(yy, khxh.ngaykhachkiem) as nvarchar))
			                    when khxh.khongkhachkiem = 0 and khxh.ngaykhachkiem is null then ' '
			                    when khxh.khongkhachkiem = 1 then 'null'
			                    else ' '
			                    end )as ngaykhachkiem
						, 	(case when DATEDIFF(DAY, khxh.shipmenttime, DATEADD(DAY, 6, khxh.ngayxonghang)) > 0 then DATEDIFF(DAY, khxh.shipmenttime, DATEADD(DAY, 6, khxh.ngayxonghang)) else null end) as tre
                        , (select ma_dtkd from md_doitackinhdoanh dtkd, c_donhang dh where dtkd.md_doitackinhdoanh_id = dh.md_doitackinhdoanh_id AND dh.c_donhang_id = khxh.c_donhang_id) as ma_dtkd
                        , isnull(khxh.ngaygiaohang, khxh.shipmenttime) as hangiaohang
                        , (CAST(DATEPART(MONTH,khxh.ngaygiaohang) as nvarchar) + '/' + CAST(DATEPART(YEAR, khxh.ngaygiaohang) as nvarchar)) as thang
						, (select hoten from nhanvien where manv = (select distinct nguoitao from c_danhsachdathang where sochungtu = khxh.c_danhsachdathang_id AND md_trangthai_id = 'HIEULUC')) as nguoilappi
                    from 
                        c_kehoachxuathang khxh
						left join md_doitackinhdoanh dtkd on khxh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
						left join c_donhang dh on khxh.c_donhang_id = dh.c_donhang_id
                    where 
                        1=1
						AND dh.md_trangthai_id <> 'KETTHUC'
                        AND khxh.{3} >= '{0}' 
                        AND khxh.{3} <='{1}' {2} {4}", dateFrom, dateTo, dsNhanVien, dateType =="0"? "ngaygiaohang" : "ngayxonghang", sql_them);
        return str;
    }
}


