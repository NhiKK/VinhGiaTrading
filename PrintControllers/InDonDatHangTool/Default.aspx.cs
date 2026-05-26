using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;

public partial class PrintControllers_InDonDatHangTool_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        string c_danhsachdathang_id = Request.QueryString["c_danhsachdathang_id"];
		string xemgiaDSDH = Request.QueryString["DSDHG"];
        rptDanhSachDatHang report = new rptDanhSachDatHang();
        this.viewReport(report, getSql(c_danhsachdathang_id, xemgiaDSDH), c_danhsachdathang_id, xemgiaDSDH);
    }

    public string getSql(string c_danhsachdathang_id, string xemgiaDSDH)
    {
		string gianhap = "null"
		, thanhtien = "null";
		if(xemgiaDSDH == "True") 
		{
			gianhap = "ddsdh.gianhap";
			thanhtien = "(ddsdh.sl_dathang * ddsdh.gianhap)";
		}
		
		string select = String.Format(@"
                        select 
	                        dsdh.sochungtu
							, dh.sochungtu as so_po
							, dh.customer_order_no
							, dsdh.ngaylap
							, dsdh.hangiaohang_po
							, dtkd.ma_dtkd
							, dtkd.ten_dtkd
							, dtkd.tel
							, dtkd.fax
	                        , sp.ma_sanpham
							, (case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) + '.jpg' else SUBSTRING(sp.ma_sanpham,0,12) + '.jpg' end) as pic
							, (case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) else SUBSTRING(sp.ma_sanpham,0,12) end) as pic
							, ddsdh.ma_sanpham_khach
							, ddsdh.mota_tiengviet
							, dvt.ma_edi
                            , ddsdh.sl_dathang
							, dsdh.huongdanlamhang
							, dsdh.huongdanlamhangchung
							, ddsdh.huongdan_dathang
	                        , gh.ma_dtkd as diachigiaohang
							, dh.sl_cont
							, dh.loai_cont
							, dsdh.discount as discountdecimal
							, ddsdh.v2
                            , (case ddsdh.sl_inner when 0 then ' ' else (cast(ddsdh.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner)) end) as dginner
                            , (cast(ddsdh.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer)) as dgouter
                            , (select hoten from nhanvien where manv = dsdh.nguoitao) as nguoitao
							, sp.trongluong
							, sp.mota as ghichu
							, dsdh.mota
							, {1} as gianhap
							, {2} as thanhtien
                        from 
	                        c_danhsachdathang dsdh
							left join md_doitackinhdoanh dtkd on dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
							left join md_doitackinhdoanh gh on dsdh.diachigiaohang = gh.md_doitackinhdoanh_id
	                        left join c_dongdsdh ddsdh on dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
							left join md_sanpham sp on ddsdh.md_sanpham_id = sp.md_sanpham_id
							left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
							left join c_donhang dh  on dsdh.c_donhang_id = dh.c_donhang_id
							left join md_donggoi dg on ddsdh.md_donggoi_id = dg.md_donggoi_id
                        where
	                        dsdh.c_danhsachdathang_id = N'{0}' order by sp.ma_sanpham asc", 
							c_danhsachdathang_id == null ? "" : c_danhsachdathang_id,
							gianhap,
							thanhtien
						);
        return select;
    }

    public void viewReport(XtraReport report, string SqlQuery, string c_danhsachdathang_id, string xemgiaDSDH)
    {
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, mdbc.GetConnection);
        DataSet ds = new DataSet();
        da.Fill(ds);
        report.DataSource = ds;
        report.DataAdapter = da;
		var tbl = ds.Tables[0];
        if (tbl.Rows.Count > 0)
        {
            tbl.Columns.Add("money", Type.GetType("System.String"));
			tbl.Columns.Add("mota_cong", Type.GetType("System.String"));
			tbl.Columns.Add("mota_tru", Type.GetType("System.String"));
			tbl.Columns.Add("discount", Type.GetType("System.Double"));
			tbl.Columns.Add("giatritang", Type.GetType("System.Double"));
			tbl.Columns.Add("giatrigiam", Type.GetType("System.Double"));
			tbl.Columns.Add("tongtiendatru", Type.GetType("System.Double"));
            //Header

            //Footer
            int lastRow = tbl.Rows.Count - 1;

            var strSumThanhTien = tbl.Compute("Sum(thanhtien)", string.Empty).ToString();
			double? total = null;
			if (!string.IsNullOrEmpty(strSumThanhTien))
                total = double.Parse(strSumThanhTien);
			
			var strDiscount = tbl.Rows[0]["discountdecimal"].ToString();
			double? discount = null;
			if (!string.IsNullOrEmpty(strDiscount))
                discount = double.Parse(strDiscount);
            
			var totalDiscount = total.GetValueOrDefault(0) * discount.GetValueOrDefault(0) / 100;
			tbl.Rows[lastRow]["discount"] = totalDiscount;
			
			var phitang = (from t in db.c_phidathangs where t.isphicong.Equals(true) && t.c_danhsachdathang_id.Equals(c_danhsachdathang_id) select t.sotien).Sum();
			tbl.Rows[lastRow]["giatritang"] = phitang.GetValueOrDefault(0);
			
			var phigiam = (from t in db.c_phidathangs where t.isphicong.Equals(false) && t.c_danhsachdathang_id.Equals(c_danhsachdathang_id) select t.sotien).Sum();
			tbl.Rows[lastRow]["giatrigiam"] = phigiam.GetValueOrDefault(0);
			
			var varTang = db.c_phidathangs.FirstOrDefault(tg => tg.c_danhsachdathang_id.Equals(c_danhsachdathang_id) && tg.isphicong.Equals(true));
			var varGiam = db.c_phidathangs.FirstOrDefault(gi => gi.c_danhsachdathang_id.Equals(c_danhsachdathang_id) && gi.isphicong.Equals(false));

			string dgTang, dgGiam;
			dgTang = varTang == null ? "" : varTang.mota;
			dgGiam = varGiam == null ? "" : varGiam.mota;
			
			double? totalUSD = null;
			string textMoney = "";
			if(xemgiaDSDH == "True") 
			{
				totalUSD = total - (double)totalDiscount + (double)phitang.GetValueOrDefault(0) - (double)phigiam.GetValueOrDefault(0);
				var ma_dtkd = tbl.Rows[0]["ma_dtkd"].ToString();
				if(totalUSD != null) {
					if(new string[]{ "VINHGIA" }.Contains(ma_dtkd)) {
						string m = MoneyToWord.ConvertMoneyToText(totalUSD.ToString()).Replace("Dollars", "");			
						int j =  m.LastIndexOf("and");
					
						if (m.Contains("Cents") | m.Contains("Cent"))
						{
							m = m.Replace("Cents", "").Replace("Cent", "");
							m = m.Insert(m.Length, "cents");
						}
						else {
							m = m.Replace("Cents", "").Replace("Cent", "");
						}
						textMoney = m;
					}
					else {
						textMoney = MoneyToWord.ConvertMoneyToTextVND((decimal)totalUSD.GetValueOrDefault(0));
					}
					tbl.Rows[lastRow]["money"] = textMoney;
					tbl.Rows[lastRow]["tongtiendatru"] = totalUSD;
				}
			}
			
			string diengiai_cong = String.Format(@"select t.mota from c_phidathang t where t.isphicong = 1 and t.c_danhsachdathang_id = '{0}'", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);
			DataTable cong = mdbc.GetData(diengiai_cong);
			var mota_cong = new List<string>();
			for(int i = 0; i < cong.Rows.Count ; i++)
			{
				mota_cong.Add(cong.Rows[i]["mota"].ToString());
			}
			
			string diengiai_tru = String.Format(@"select t.mota from c_phidathang t where t.isphicong = 0 and t.c_danhsachdathang_id = '{0}'", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);									
			DataTable tru = mdbc.GetData(diengiai_tru);
			var mota_tru = new List<string>();
			for(int i = 0; i < tru.Rows.Count; i++)
			{
				mota_tru.Add(tru.Rows[i]["mota"].ToString());
			}
			tbl.Rows[lastRow]["mota_cong"] = string.Join(",", mota_cong);
			tbl.Rows[lastRow]["mota_tru"] = string.Join(",", mota_tru);
			
            report.DataSource = ds;
            report.DataAdapter = da;
            ReportViewer1.Report = report;
        }
        else
        {
            Response.Write("<h1>Không có dữ liệu</h1>");
        }
    }
}