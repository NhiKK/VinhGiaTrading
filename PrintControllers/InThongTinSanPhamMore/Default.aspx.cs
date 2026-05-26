using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class PrintControllers_InThongTinSanPhamMore_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string md_sanpham_id = Request.QueryString["md_sanpham_id"];
        String trangthai = Request.QueryString["trangthai"];
		String typedg = Request.QueryString["typedg"];
        string md_tinhtranghanghoa_id = Request.QueryString["md_tinhtranghanghoa_id"];
        if (trangthai != null & trangthai != "")
        {
            trangthai = " and sp.trangthai = '"+ trangthai +"'";
        }
        else
        {
            trangthai = " and sp.trangthai = 'DHD'";
        }
		
		if(typedg == "true") {
			typedg = "and dgsp.macdinh <> 1";
		}
		else {
			typedg = "and dgsp.macdinh = 1";
		}

        if (md_tinhtranghanghoa_id != null & md_tinhtranghanghoa_id != "")
        {
            md_tinhtranghanghoa_id = " and sp.md_tinhtranghanghoa_id = '" + md_tinhtranghanghoa_id + "'";
        }
        else
        {
            md_tinhtranghanghoa_id = " and 1 = 1";
        }

        String sql = string.Format(@"select
                    sp.ma_sanpham as [ma san pham], sp.mota_tiengviet as [ten tieng viet], sp.mota_tienganh as [ten tieng anh], kd.ten_tv as [ten kieu dang], cn.ten_tv as [ten chuc nang]
                    , kd.ma_kieudang as [ma kieu dang], cn.ma_chucnang as [ma chuc nang] 
                    , (select dvtsp.ten_dvt from md_donvitinhsanpham dvtsp where dvtsp.md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id) as [don vi tinh sp]
                    , l_cm as l, w_cm as w, h_cm as h
                    , sp.trongluong, sp.dientich, sp.thetich
                    , (select nhom from md_nhomnangluc nnl where nnl.md_nhomnangluc_id = sp.md_nhomnangluc_id) as [nhom nang luc]    
					, cb.ma_cangbien as [Cang Bien], dtkd.ma_dtkd as [NCC]
					, hscode.ma_hscode as [HS Code]
					, sp.chucnangsp
					, CONVERT(VARCHAR, sp.ngayxacnhan, 103) as ngayxacnhan
                    , dmhh.ten_danhmuc as [danh muc hang hoa]
					, sp.hinhthucban
					, sp.mota
					, sp.mota as ghichu
                    , isnull((
                        select top 1 gsp.gia 
                        from md_banggia bg, md_phienbangia pbg, md_giasanpham gsp
                        where bg.md_banggia_id = pbg.md_banggia_id
                        AND pbg.md_phienbangia_id = gsp.md_phienbangia_id
                        AND sp.md_sanpham_id = gsp.md_sanpham_id
                        AND bg.banggiaban = 1
						AND substring(bg.ten_banggia,0,5) = 'FOB-'
                        AND pbg.md_trangthai_id = 'HIEULUC'
                        order by pbg.ngay_hieuluc desc
                    ), 0) as [gia ban]
                    , isnull((
                        select top 1 gsp.gia 
                        from md_banggia bg, md_phienbangia pbg, md_giasanpham gsp
                        where bg.md_banggia_id = pbg.md_banggia_id
                        AND pbg.md_phienbangia_id = gsp.md_phienbangia_id
                        AND sp.md_sanpham_id = gsp.md_sanpham_id
                        AND bg.banggiaban = 0
                        AND pbg.md_trangthai_id = 'HIEULUC'
                        order by pbg.ngay_hieuluc desc
                    ), 0) as [gia mua]
                    , isnull(dg.sl_inner,0) as [SL Inner], isnull((select dvt.ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner),0) as [DVT Inner]
                    , isnull(dg.l1,0) as l1, isnull(dg.w1,0) as w1, isnull(dg.h1,0) as h1
                    , isnull(dg.sl_outer,0) as [SL Outer], isnull((select dvt.ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer),0) as [DVT Outer]
                    , isnull(dg.l2_mix,0) as l2, isnull(dg.w2_mix,0) as w2, isnull(dg.h2_mix,0) as h2
                    , isnull(dg.soluonggoi_ctn_20,0) as [SL Cont 20], isnull(dg.soluonggoi_ctn,0) as [SL Cont 40]
					, isnull(dg.soluonggoi_ctn_40hc,0) as [SL Cont 40 hc]
					, isnull(dg.vd,0) as vd
					, isnull(dg.vn,0) as vn
					, isnull(dg.vl,0) as vl
					, dg.ghichu_vachngan
                    , CONVERT(VARCHAR, dg.ngayxacnhan, 103) as ngayxacnhan_donggoi
					, sp.trangthai
					, dg.mota as [ghi chu dong goi]
                    , nm.ten_nhommau as [nhóm màu]
                    , isnull(dg.nw1, 0) as nw1
                    , isnull(dg.gw1, 0) as gw1
                    , isnull(dg.nw2, 0) as nw2
                    , isnull(dg.gw2, 0) as gw2
                    , isnull(dg.vtdg2, 0) as vtdg2
                    , isnull(dg.cpdg_vuotchuan, 0) as cpdg_vuotchuan
                from 
                    md_sanpham sp
					left join md_doitackinhdoanh dtkd on sp.nhacungung = dtkd.md_doitackinhdoanh_id
                    left join md_donggoisanpham dgsp on dgsp.md_sanpham_id = sp.md_sanpham_id
					left join md_donggoi dg on dg.md_donggoi_id = dgsp.md_donggoi_id
					left join md_cangbien cb on sp.md_cangbien_id = cb.md_cangbien_id
                    left join md_kieudang kd on sp.md_kieudang_id = kd.md_kieudang_id
					left join md_chucnang cn on sp.md_chucnang_id = cn.md_chucnang_id
					left join md_hscode hscode on sp.md_hscode_id = hscode.md_hscode_id
                    left join md_danhmuchanghoa dmhh on sp.md_tinhtranghanghoa_id = dmhh.md_danhmuchanghoa_id
                    left join md_mausac ms on ms.code_cl = substring(sp.ma_sanpham, 0, 3) and ms.code_dt = substring(sp.ma_sanpham, 7, 2) and ms.code_mau = substring(sp.ma_sanpham, 13, 2) and ms.trangthai = 'HIEULUC'
                    left join md_nhommau nm on nm.md_nhommau_id = ms.md_nhommau_id
                where sp.ma_sanpham like '{0}' {1} {2} {3}
                order by ma_sanpham asc", md_sanpham_id, trangthai, typedg, md_tinhtranghanghoa_id);

        DataTable dt = mdbc.GetData(sql);

        if (dt.Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
			
			Excel_Format ex_fm = new Excel_Format(hssfworkbook); 
			ICellStyle cellBold = ex_fm.cell_decimal_1(10, false, true, "", "R", "T");
			ICellStyle cellBold2 = ex_fm.cell_decimal_2(10, false, true, "", "R", "T");
			ICellStyle cellBold3 = ex_fm.cell_decimal_3(10, false, true, "", "R", "T");
			//-- 
			
            s1.CreateRow(0).CreateCell(0).SetCellValue("THÔNG TIN SẢN PHẨM");

            IRow rowHeader = s1.CreateRow(1);
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
                rowHeader.CreateCell(i).SetCellValue(col.ColumnName);
                i++;
            }

            int iRow = 2;
            foreach (DataRow row in dt.Rows)
            {
                IRow rowDetails = s1.CreateRow(iRow);

                int cell = 0;
                rowDetails.CreateCell(cell).SetCellValue(row["ma san pham"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ten tieng viet"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ten tieng anh"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ten kieu dang"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ten chuc nang"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ma kieu dang"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ma chuc nang"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["don vi tinh sp"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["l"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["w"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["h"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["trongluong"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold2;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["dientich"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold3;
                cell++;
                var rowDT13 = rowDetails.CreateCell(cell);
                if (!string.IsNullOrEmpty(row["thetich"].ToString()))
                    rowDT13.SetCellValue(double.Parse(row["thetich"].ToString()));
                rowDetails.GetCell(cell).CellStyle = cellBold3;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["nhom nang luc"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["Cang Bien"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["NCC"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["HS Code"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["chucnangsp"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ngayxacnhan"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["danh muc hang hoa"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["hinhthucban"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["mota"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ghichu"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["gia ban"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold2;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["gia mua"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold2;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["SL Inner"].ToString()));
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["DVT Inner"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["l1"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["w1"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["h1"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["SL Outer"].ToString()));
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["DVT Outer"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["l2"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["w2"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["h2"].ToString()));
				rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["SL Cont 20"].ToString()));
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["SL Cont 40"].ToString()));
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["SL Cont 40 hc"].ToString()));
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["vd"].ToString()));
				s1.SetColumnWidth(cell, 0);
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["vn"].ToString()));
				s1.SetColumnWidth(cell, 0);
                cell++;
                rowDetails.CreateCell(40).SetCellValue(double.Parse(row["vl"].ToString()));
				s1.SetColumnWidth(cell, 0);
                cell++;
                rowDetails.CreateCell(41).SetCellValue(row["ghichu_vachngan"].ToString());
				s1.SetColumnWidth(cell, 0);
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ngayxacnhan_donggoi"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["trangthai"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["ghi chu dong goi"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(row["nhóm màu"].ToString());
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["nw1"].ToString()));
                rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["gw1"].ToString()));
                rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["nw2"].ToString()));
                rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["gw2"].ToString()));
                rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["vtdg2"].ToString()));
                rowDetails.GetCell(cell).CellStyle = cellBold;
                cell++;
                rowDetails.CreateCell(cell).SetCellValue(double.Parse(row["cpdg_vuotchuan"].ToString()));
                rowDetails.GetCell(cell).CellStyle = cellBold;

                iRow++;
            }
            String saveAsFileName = "ThongTinSanPham.xls";
            this.SaveFile(hssfworkbook, saveAsFileName);
        }
        else
        {
            Response.Write("Không có dữ liệu" + md_sanpham_id);
        }
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