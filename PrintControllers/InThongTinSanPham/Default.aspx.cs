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
using System.Globalization;

public partial class PrintControllers_InThongTinSanPham_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDBDataContext db = new LinqDBDataContext();
        string md_sanpham_id = Request.QueryString["md_sanpham_id"];
        string md_tinhtranghanghoa_id = Request.QueryString["md_tinhtranghanghoa_id"];
        string trangthai = Request.QueryString["trangthai"];
        if (trangthai != null & trangthai != "")
        {
            trangthai = " and sp.trangthai = '"+ trangthai +"'";
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
        md_sanpham sp = db.md_sanphams.FirstOrDefault(p => p.md_sanpham_id.Equals(md_sanpham_id));
        DataTable dt = mdbc.GetData(this.getSql(sp.ma_sanpham, trangthai + md_tinhtranghanghoa_id));
        if (dt.Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
            String saveAsFileName = "ThongTinSanPham.xls";
            this.SaveFile(hssfworkbook, saveAsFileName);
        }
        else {
            Response.Write("Không có dữ liệu" + md_sanpham_id);
        }
        
    }

    public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        IRow row = s1.CreateRow(0);
        row.CreateCell(0).SetCellValue(dt.Rows[0]["value"].ToString());
        row.CreateCell(1).SetCellValue(dt.Rows[0]["description"].ToString());
        row.CreateCell(2).SetCellValue(dt.Rows[0]["quantity"].ToString());
        row.CreateCell(3).SetCellValue(dt.Rows[0]["packing"].ToString());
        row.CreateCell(4).SetCellValue(dt.Rows[0]["l2_mix"].ToString());
        row.CreateCell(5).SetCellValue(dt.Rows[0]["w2_mix"].ToString());
        row.CreateCell(6).SetCellValue(dt.Rows[0]["h2_mix"].ToString());
        row.CreateCell(7).SetCellValue(dt.Rows[0]["cbm"].ToString());
        row.CreateCell(8).SetCellValue(dt.Rows[0]["slc_out"].ToString());
        row.CreateCell(9).SetCellValue(dt.Rows[0]["price"].ToString());
        row.CreateCell(10).SetCellValue(dt.Rows[0]["pallet"].ToString());
        row.CreateCell(11).SetCellValue(dt.Rows[0]["size"].ToString());
        return hssfworkbook;
    }

    public string getSql(string md_sanpham_id, string trangthai)
    {
        string sql = string.Format(@"select
	        sp.ma_sanpham as value, sp.mota_tienganh as [description],
	        dg.sl_outer as quantity, (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer) as packing,
	        dg.l2_mix, dg.w2_mix,dg.h2_mix, dg.v2 as cbm, dg.soluonggoi_ctn as slc_out,
	        (
		        select top 1 gsp.gia 
		        from md_banggia bg, md_phienbangia pbg, md_giasanpham gsp
		        where bg.md_banggia_id = pbg.md_banggia_id
		        AND pbg.md_phienbangia_id = gsp.md_phienbangia_id
		        AND sp.md_sanpham_id = gsp.md_sanpham_id
		        AND bg.banggiaban = 1
		        AND bg.isstandar = 1
		        AND pbg.md_trangthai_id = 'HIEULUC'
		        order by pbg.ngay_hieuluc desc
	        ) as price, 
	        (cast(dg.l2_mix as nvarchar) + ' x ' + cast(dg.w2_mix as nvarchar) + ' x '+ cast(dg.h2_mix as nvarchar)) as pallet,
	        (cast(sp.l_cm as nvarchar) + ' x ' + cast(sp.w_cm as nvarchar) + ' x ' + cast(sp.h_cm as nvarchar)) as size
        from
	        md_sanpham sp
			left join md_donggoisanpham dgsp on sp.md_sanpham_id = dgsp.md_sanpham_id
			left join md_donggoi dg on dgsp.md_donggoi_id = dgsp.md_donggoi_id
        where sp.ma_sanpham in('{0}') {1}
	        order by sp.ma_sanpham asc", md_sanpham_id, trangthai);
        return sql;
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