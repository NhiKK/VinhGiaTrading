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
public partial class PrintControllers_InThongTinSanPhamQRCode_Default : System.Web.UI.Page
{
    private LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        string md_sanpham_id = Request.QueryString["md_sanpham_id"];
        String trangthai = Request.QueryString["trangthai"];
        string md_tinhtranghanghoa_id = Request.QueryString["md_tinhtranghanghoa_id"];
        if (trangthai != null & trangthai != "")
        {
            trangthai = " and sp.trangthai = '" + trangthai + "'";
        }
        else
        {
            trangthai = " and sp.trangthai = 'DHD'";
        }

        if (md_tinhtranghanghoa_id != null & md_tinhtranghanghoa_id != "")
        {
            md_tinhtranghanghoa_id = " and sp.md_tinhtranghanghoa_id = '" + md_tinhtranghanghoa_id + "'";
        }
        else
        {
            md_tinhtranghanghoa_id = " and 1 = 1";
        }

        String sql = string.Format(@"
		select
			sp.ma_sanpham as [Mã Số Anco], '' as [Kích thước hàng ShowRoom], '' as [Kích thước hàng], 
			'' as [Inner], '' as [Master], '' as [Pallet size], '' as [Q'ty/cont], '' as [Biến tấu]
		from 
			md_sanpham sp
		where sp.ma_sanpham like '{0}' {1} {2}
		ORDER BY substring(sp.ma_sanpham,0, 8) asc, substring(sp.ma_sanpham,13, 2) asc, substring(sp.ma_sanpham,10, 2) desc
		", md_sanpham_id, trangthai, md_tinhtranghanghoa_id);
        DataTable dt = mdbc.GetData(sql);

        if (dt.Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
            HSSFSheet hssfsheet = (HSSFSheet)s1;

            Excel_Format ex_fm = new Excel_Format(hssfworkbook);
            ICellStyle cellHeader = ex_fm.getcell(11, true, true, "", "C", "T");
            ICellStyle cellHeader1 = ex_fm.getcell(10, true, true, "", "C", "T");
            ICellStyle cellcenter = ex_fm.getcell(9, false, true, "", "C", "T");

            s1.CreateRow(0).CreateCell(0).SetCellValue("THÔNG TIN SẢN PHẨM QRCODE");
            CellRangeAddress cellRange01 = new CellRangeAddress(0, 0, 0, 7);
            s1.AddMergedRegion(cellRange01);
            s1.GetRow(0).HeightInPoints = 30;
            s1.GetRow(0).GetCell(0).CellStyle = cellHeader;
            ex_fm.set_border2(cellRange01, "LRTB", hssfsheet);
            IRow rowHeader = s1.CreateRow(1);
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
                rowHeader.CreateCell(i).SetCellValue(col.ColumnName);
                s1.SetColumnWidth(i, 6000);
                s1.GetRow(1).GetCell(i).CellStyle = cellHeader1;
                i++;

            }
            s1.GetRow(1).HeightInPoints = 30;

            int iRow = 2;
            foreach (DataRow row in dt.Rows)
            {
                IRow rowDetails = s1.CreateRow(iRow);
                string ma_sanpham = row["Mã Số Anco"].ToString();
                rowDetails.CreateCell(0).SetCellValue(ma_sanpham);
                rowDetails.CreateCell(1).SetCellValue(" ");
                rowDetails.CreateCell(2).SetCellValue(" ");
                rowDetails.CreateCell(3).SetCellValue(" ");
                rowDetails.CreateCell(4).SetCellValue(" ");
                rowDetails.CreateCell(5).SetCellValue(" ");
                rowDetails.CreateCell(6).SetCellValue(" ");
                rowDetails.CreateCell(7).SetCellValue(" ");
                string bc = ma_sanpham.Substring(9, 2);

                var dgsp = (from a in db.md_sanphams
                            join b in db.md_donggoisanphams on a.md_sanpham_id equals b.md_sanpham_id
                            join c in db.md_donggois on b.md_donggoi_id equals c.md_donggoi_id
                            where a.ma_sanpham == ma_sanpham & b.macdinh == true
                            select new
                            {
                                c.sl_inner,
                                dvtInner = db.md_donvitinhs.Where(s => s.md_donvitinh_id == c.dvt_inner).Select(s => s.ten_dvt).FirstOrDefault(),
                                c.sl_outer,
                                dvtOuter = db.md_donvitinhs.Where(s => s.md_donvitinh_id == c.dvt_outer).Select(s => s.ten_dvt).FirstOrDefault(),
                                c.l2_mix,
                                c.w2_mix,
                                c.h2_mix,
                                qutyPerCont = c.soluonggoi_ctn_20 + " pal/20\"; " + c.soluonggoi_ctn + " pal/40\""
                            }).ToList().Select(s => new {
                                inner = s.sl_inner + " " + s.dvtInner,
                                outer = s.sl_outer + " " + s.dvtOuter,
                                palletSize =
                                "L" + s.l2_mix.GetValueOrDefault(0).ToString("#,#0.#") +
                                "- W" + s.w2_mix.GetValueOrDefault(0).ToString("#,#0.#") +
                                "- H" + s.h2_mix.GetValueOrDefault(0).ToString("#,#0.#") + " cm",
                                s.qutyPerCont
                            }).FirstOrDefault();

                if (bc.Substring(0, 1) != "0" & bc != "S1" & bc.Substring(0, 1) != "F")
                {
                    rowDetails.GetCell(3).SetCellValue(dgsp == null ? "" : dgsp.inner);
                    rowDetails.GetCell(4).SetCellValue(dgsp == null ? "" : dgsp.outer);
                    rowDetails.GetCell(5).SetCellValue(dgsp == null ? "" : dgsp.palletSize);
                    rowDetails.GetCell(6).SetCellValue(dgsp == null ? "" : dgsp.qutyPerCont);
                    rowDetails.GetCell(7).SetCellValue(bc);

                    //var bodetail = db.md_bos.Where()
                    string start = ma_sanpham.Substring(0, 9) + "S";
                    string end = ma_sanpham.Substring(11, 3);
                    string boS = db.md_sanphams.Where(s => 
                        s.ma_sanpham.StartsWith(start)
                        & s.ma_sanpham.EndsWith(end)).Select(s => s.ma_sanpham).FirstOrDefault();
                    Response.Write(boS);
                    boS = !string.IsNullOrEmpty(boS) ? boS.Substring(9, 2) : "";
                    var boSele = db.md_bos.Where(s => s.ma_bo == bc & s.ma_bo_cha == boS).FirstOrDefault();

                    if (boSele != null)
                    {
                        var lstVal = new List<string>();
                        foreach (var bodt in db.md_bo_chitiets.Where(s=>s.md_bo_id == boSele.md_bo_id).OrderBy(s=>s.md_bo_detail))
                        {
                            string ma_spdetail = ma_sanpham.Replace(bc, bodt.md_bo_detail);
                            var sp = db.md_sanphams.FirstOrDefault(s => s.ma_sanpham == ma_spdetail);
                            if (sp != null)
                            {
                                bool hinhTron = isRound(sp.md_kieudang_id, db);
                                if (hinhTron == true)
                                {
                                    lstVal.Add("D" + sp.l_cm.GetValueOrDefault(0).ToString("#,#0.#") + "- H" + sp.h_cm.GetValueOrDefault(0).ToString("#,#0.#") + " cm");
                                }
                                else
                                {
                                    lstVal.Add("L" + sp.l_cm.GetValueOrDefault(0).ToString("#,#0.#") + "- W" +
                                    sp.w_cm.GetValueOrDefault(0).ToString("#,#0.#") + "- H" + sp.h_cm.GetValueOrDefault(0).ToString("#,#0.#") + " cm");
                                }
                            }
                        }
                        rowDetails.GetCell(2).SetCellValue(string.Join("\n", lstVal));
                    }
                }
                else
                {
                    md_sanpham sp = db.md_sanphams.FirstOrDefault(s => s.ma_sanpham == ma_sanpham);
                    if (sp != null)
                    {
                        bool hinhTron = isRound(sp.md_kieudang_id, db);
                        rowDetails.GetCell(1).SetCellValue(sp.l_cm.GetValueOrDefault(0).ToString("#,#0.0") + "x" +
                        sp.w_cm.GetValueOrDefault(0).ToString("#,#0.0") + "x" + sp.h_cm.GetValueOrDefault(0).ToString("#,#0.0") + "H cm");

                        if (hinhTron == true)
                        {
                            rowDetails.GetCell(2).SetCellValue("D" + sp.l_cm.GetValueOrDefault(0).ToString("#,#0.#") + "- H" + sp.h_cm.GetValueOrDefault(0).ToString("#,#0.#") + " cm");
                        }
                        else
                        {
                            rowDetails.GetCell(2).SetCellValue("L" + sp.l_cm.GetValueOrDefault(0).ToString("#,#0.#") + "- W" +
                            sp.w_cm.GetValueOrDefault(0).ToString("#,#0.#") + "- H" + sp.h_cm.GetValueOrDefault(0).ToString("#,#0.#") + " cm");
                        }
                        rowDetails.GetCell(3).SetCellValue(dgsp == null ? "" : dgsp.inner);
                        rowDetails.GetCell(4).SetCellValue(dgsp == null ? "" : dgsp.outer);
                        rowDetails.GetCell(5).SetCellValue(dgsp == null ? "" : dgsp.palletSize);
                        rowDetails.GetCell(6).SetCellValue(dgsp == null ? "" : dgsp.qutyPerCont);
                        rowDetails.GetCell(7).SetCellValue(bc);
                    }
                }

                for (int cou = 0; cou < dt.Columns.Count; cou++)
                {
                    rowDetails.GetCell(cou).CellStyle = cellcenter;
                }
                iRow++;
            }
            String saveAsFileName = "ThongTinSanPhamQRCode.xls";
            this.SaveFile(hssfworkbook, saveAsFileName);
        }
        else
        {
            Response.Write("Không có dữ liệu" + md_sanpham_id);
        }
    }

    public bool isRound(string kd, LinqDBDataContext db)
    {
        var kieudang = db.md_kieudangs.Where(s => s.md_kieudang_id == kd & s.ma_kieudang == "ROU").FirstOrDefault();
        if (kieudang == null)
            return false;
        else
            return true;
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