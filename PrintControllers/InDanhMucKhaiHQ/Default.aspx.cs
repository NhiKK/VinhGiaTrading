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
using System.Linq;

public partial class PrintControllers_InDanhMucKhaiHQ_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String c_pkivn_id = Request.QueryString["c_packinglist_id"];
			String select = String.Format(@"select sp.ma_sanpham, hsc.hscode ,sp.mota_tiengvietVG , 'VIETNAM' as xuatxu, cdp.soluong, 
												dvt.mota, cdp.gia, cdp.thanhtien
											from c_dongpklinv cdp left join md_sanpham sp on cdp.md_sanpham_id = sp.md_sanpham_id
												left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
												left join md_nhomnangluc nnl on sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
												left join md_hscode hsc on sp.md_hscode_id = hsc.md_hscode_id
											where 
												cdp.c_packinginvoice_id = '{0}'", c_pkivn_id);
												
												
            /*String select = String.Format(@"select sp.ma_sanpham, hsc.hscode ,sp.mota_tiengviet , 'VIETNAM' as xuatxu, cdp.soluong, 
	                                            dvt.mota, cdp.gia, cdp.thanhtien
                                            from c_dongpklinv cdp, md_sanpham sp, md_donvitinhsanpham dvt, md_nhomnangluc nnl, md_hscode hsc
                                            where 
                                                cdp.c_packinginvoice_id = '{0}'
	                                            and cdp.md_sanpham_id = sp.md_sanpham_id
	                                            and sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
                                                and sp.md_hscode_id = hsc.md_hscode_id
	                                            and sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id", c_pkivn_id);*/

            DataTable dt = mdbc.GetData(select);

            if (dt.Rows.Count != 0)
            {
                HSSFWorkbook hssfworkbook = this.CreateWorkBookPO(dt);
                String saveAsFileName = String.Format("KhaiHaiQuan-{0}.xls", DateTime.Now);
                this.SaveFile(hssfworkbook, saveAsFileName);
            }
            else
            {
                Response.Write("<h3>PackingList/Invoice không có dữ liệu</h3>");
            }
        }
        catch (Exception ex)
        {
            Response.Write(String.Format("<h3>Quá trình chiếc xuất xảy ra lỗi {0}</h3>", ex + ""));
        }
    }

    public HSSFWorkbook CreateWorkBookPO(DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();

        //style boder and center font size 8
        var font10Bold = hssfworkbook.CreateFont();
        font10Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        var font10BoldItalic = hssfworkbook.CreateFont();
        font10BoldItalic.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
        font10BoldItalic.IsItalic = true;
        var font10Italic = hssfworkbook.CreateFont();
        font10Italic.IsItalic = true;

        var font8Bold = hssfworkbook.CreateFont();
        font8Bold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
        font8Bold.FontHeightInPoints = 12;

        ICellStyle cellStyleBoder10TL = hssfworkbook.CreateCellStyle();
        cellStyleBoder10TL.Alignment = HorizontalAlignment.Left;
        cellStyleBoder10TL.VerticalAlignment = VerticalAlignment.Top;
        cellStyleBoder10TL.SetFont(font10Bold);

        ICellStyle cellStyleBoder10T = hssfworkbook.CreateCellStyle();
        cellStyleBoder10T.Alignment = HorizontalAlignment.Left;
        cellStyleBoder10T.VerticalAlignment = VerticalAlignment.Top;
        
        ICellStyle cellStyleBoder8 = hssfworkbook.CreateCellStyle();
        cellStyleBoder8.Alignment = HorizontalAlignment.Center;
        cellStyleBoder8.VerticalAlignment = VerticalAlignment.Top;
        cellStyleBoder8.WrapText = true;
        cellStyleBoder8.SetFont(CreateFontSize(hssfworkbook, 12));
        cellStyleBoder8.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle cellStyleBoder8R = hssfworkbook.CreateCellStyle();
        cellStyleBoder8R.Alignment = HorizontalAlignment.Right;
        cellStyleBoder8R.SetFont(CreateFontSize(hssfworkbook, 12));
        cellStyleBoder8R.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8R.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8R.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8R.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle cellStyleBoder8L = hssfworkbook.CreateCellStyle();
        cellStyleBoder8L.Alignment = HorizontalAlignment.Left;
        cellStyleBoder8L.VerticalAlignment = VerticalAlignment.Top;
        cellStyleBoder8L.WrapText = true;
        cellStyleBoder8L.SetFont(CreateFontSize(hssfworkbook, 12));
        cellStyleBoder8L.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8L.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8L.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8L.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle cellStyleBoder8C = hssfworkbook.CreateCellStyle();
        cellStyleBoder8C.Alignment = HorizontalAlignment.Center;
        cellStyleBoder8C.WrapText = true;
        cellStyleBoder8C.SetFont(CreateFontSize(hssfworkbook, 12));
        cellStyleBoder8C.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8C.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8C.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBoder8C.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

        ICellStyle cellStyleTR = hssfworkbook.CreateCellStyle();
        cellStyleTR.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleTR.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleTR.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleTR.Alignment = HorizontalAlignment.Center;
        cellStyleTR.SetFont(font8Bold);
        ICellStyle cellStyleBR = hssfworkbook.CreateCellStyle();
        cellStyleBR.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBR.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBR.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBR.Alignment = HorizontalAlignment.Center;
        cellStyleBR.SetFont(font8Bold);

        ICellStyle cellStyleLC = hssfworkbook.CreateCellStyle();
        cellStyleLC.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleLC.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleLC.Alignment = HorizontalAlignment.Center;

        ICellStyle cellStyleBL = hssfworkbook.CreateCellStyle();
        cellStyleBL.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBL.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyleBR.SetFont(font8Bold);
        ICellStyle cellStyleR = hssfworkbook.CreateCellStyle();
        cellStyleBoder8.Alignment = HorizontalAlignment.Right;
        ICellStyle cellStyleC = hssfworkbook.CreateCellStyle();
        cellStyleC.Alignment = HorizontalAlignment.Center;
        cellStyleC.SetFont(font10Bold);

        ICellStyle cellStyleBoderNumber8 = NPOIUtils.CellNumberStyle(hssfworkbook, CreateFontSize(hssfworkbook, 12), true);
        ICellStyle cellStyleNumber8 = NPOIUtils.CellNumberStyle(hssfworkbook, CreateFontSize(hssfworkbook, 12), false);

        LinqDBDataContext db = new LinqDBDataContext();
        String c_pkivn_id = Request.QueryString["c_packinglist_id"];
        
        ISheet s1 = hssfworkbook.CreateSheet("DMHQ(1)");

        s1.SetMargin(MarginType.RightMargin, (double)0.5);
        s1.SetMargin(MarginType.TopMargin, (double)0.6);
        s1.SetMargin(MarginType.LeftMargin, (double)0.4);
        s1.SetMargin(MarginType.BottomMargin, (double)0.3);
        s1.FitToPage = true;

        IPrintSetup print = s1.PrintSetup;
        print.PaperSize = (short)PaperSize.A4;
        print.Scale = (short)80;
        print.FitWidth = (short)1;
        print.FitHeight = (short)0;

        // set A2
        s1.CreateRow(1).CreateCell(0).SetCellValue("DANH MỤC HẢI QUAN");
        s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 7));

        IRow rowColumnHeader = s1.CreateRow(4);
        // create column of table details
        rowColumnHeader.CreateCell(0).SetCellValue("Mã");
        rowColumnHeader.GetCell(0).CellStyle = cellStyleBoder8C;
        rowColumnHeader.CreateCell(1).SetCellValue("Diễn giải");
        rowColumnHeader.GetCell(1).CellStyle = cellStyleBoder8C;
        rowColumnHeader.CreateCell(2).SetCellValue("Mã HS 8 số");
        rowColumnHeader.GetCell(2).CellStyle = cellStyleBoder8C;
        rowColumnHeader.CreateCell(3).SetCellValue("Xuất xứ");
        rowColumnHeader.GetCell(3).CellStyle = cellStyleBoder8C;
        rowColumnHeader.CreateCell(4).SetCellValue("Số lượng");
        rowColumnHeader.GetCell(4).CellStyle = cellStyleBoder8C;
        rowColumnHeader.CreateCell(5).SetCellValue("Đơn vị tính");
        rowColumnHeader.GetCell(5).CellStyle = cellStyleBoder8C;
        rowColumnHeader.CreateCell(6).SetCellValue("Đơn giá");
        rowColumnHeader.GetCell(6).CellStyle = cellStyleBoder8C;
        rowColumnHeader.CreateCell(7).SetCellValue("Thành tiền");
        rowColumnHeader.GetCell(7).CellStyle = cellStyleBoder8C;

		string sql_de = String.Format(@"select sp.ma_sanpham, hsc.hscode as hscode
											, sp.mota_tiengvietVG , 'VIETNAM' as xuatxu, cdp.soluong
											, dvt.mota as dvt, cdp.gia, cdp.thanhtien
										from c_dongpklinv cdp left join md_sanpham sp on cdp.md_sanpham_id = sp.md_sanpham_id
											left join c_dongnhapxuat cdnx on cdp.c_dongnhapxuat_id = cdnx.c_dongnhapxuat_id
											left join md_donvitinhsanpham dvt on sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
											left join md_nhomnangluc nnl on sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
											left join md_hscode hsc on sp.md_hscode_id = hsc.md_hscode_id
										where 
											cdp.c_packinginvoice_id = '{0}'
										order by ma_sanpham asc", c_pkivn_id);
		
        /*string sql_de = @"select sp.ma_sanpham, hsc.hscode as hscode,sp.mota_tiengviet , 'VIETNAM' as xuatxu, cdp.soluong, 
	                        dvt.mota as dvt, cdp.gia, cdp.thanhtien
                        from c_dongpklinv cdp, md_sanpham sp, md_donvitinhsanpham dvt, md_nhomnangluc nnl, md_hscode hsc
                        where cdp.c_packinginvoice_id = '" + c_pkivn_id + "'" +//c_pkivn_id
	                        @"and cdp.md_sanpham_id = sp.md_sanpham_id
	                        and sp.md_nhomnangluc_id = nnl.md_nhomnangluc_id
                            and sp.md_hscode_id = hsc.md_hscode_id
	                        and sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id order by ma_sanpham asc";*/
            DataTable dt_d = mdbc.GetData(sql_de);
            int total_row_d = dt_d.Rows.Count;
            //create detail group
            int r=4;
            foreach (DataRow g_dt in dt_d.Rows)
            {
                r++;

                IRow ir = s1.CreateRow(r); ir.HeightInPoints = 45;
                ir.CreateCell(0).SetCellValue(g_dt["ma_sanpham"].ToString());
                ir.GetCell(0).CellStyle = cellStyleBoder8L;
                ir.CreateCell(1).SetCellValue(g_dt["mota_tiengvietVG"].ToString() + " hàng mới 100%");
                ir.GetCell(1).CellStyle = cellStyleBoder8L;
                ir.CreateCell(2).SetCellValue(g_dt["hscode"].ToString());
                ir.GetCell(2).CellStyle = cellStyleBoder8L;
                ir.CreateCell(3).SetCellValue(g_dt["xuatxu"].ToString());
                ir.GetCell(3).CellStyle = cellStyleBoder8L;
                ir.CreateCell(4).SetCellValue(double.Parse(g_dt["soluong"].ToString()));
                ir.GetCell(4).CellStyle = cellStyleBoderNumber8;
                ir.CreateCell(5).SetCellValue(g_dt["dvt"].ToString());
                ir.GetCell(5).CellStyle = cellStyleBoderNumber8;
                ir.CreateCell(6).SetCellValue(double.Parse(g_dt["gia"].ToString()));
                ir.GetCell(6).CellStyle = cellStyleBoderNumber8;
                ir.CreateCell(7).SetCellValue(double.Parse(g_dt["thanhtien"].ToString()));
                ir.GetCell(7).CellStyle = cellStyleBoderNumber8;
                

            }
            
        // Style of Company Name
        ICellStyle cellStyleCompany = hssfworkbook.CreateCellStyle();
        cellStyleCompany.Alignment = HorizontalAlignment.Center;
        cellStyleCompany.SetFont(CreateFontSize(hssfworkbook, 22));

        // Style of Company Information 
        ICellStyle cellStyleInfomation = hssfworkbook.CreateCellStyle();
        cellStyleInfomation.Alignment = HorizontalAlignment.Center;
        cellStyleInfomation.SetFont(CreateFontSize(hssfworkbook, 12));

        // Style of Company Information 
        ICellStyle cellStyleTitle = hssfworkbook.CreateCellStyle();
        cellStyleTitle.Alignment = HorizontalAlignment.Center;
        cellStyleTitle.SetFont(CreateFontSize(hssfworkbook, 22));

        s1.SetColumnWidth(0, 5000); 
        s1.SetColumnWidth(1, 10000);
        s1.SetColumnWidth(2, 5000);
        s1.SetColumnWidth(3, 5000);
        s1.SetColumnWidth(4, 4500);
        s1.SetColumnWidth(5, 4500);
        s1.SetColumnWidth(6, 4500);
        s1.SetColumnWidth(7, 4500); 
        s1.GetRow(1).GetCell(0).CellStyle = cellStyleCompany;

        return hssfworkbook;
    }

    public void SaveFile(HSSFWorkbook hsswb, String saveAsFileName)
    {
        var compineF = Request.QueryString["compineF"];
        if (string.IsNullOrWhiteSpace(compineF))
        {
            MemoryStream exportData = new MemoryStream();
            hsswb.Write(exportData);

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
            Response.Clear();
            Response.BinaryWrite(exportData.GetBuffer());
            Response.End();
        }
        else
        {
            var link = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/FileUpload/Compine/{0}.xls", Guid.NewGuid().ToString()));
            using (FileStream file = new FileStream(link, FileMode.Create, FileAccess.ReadWrite))
            {
                hsswb.Write(file);
            }
            var dic = new Dictionary<string, string>();
            dic["link"] = link;
            dic["name"] = "DMHQ (1)";
            Response.Clear();
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dic));
        }
    }

    public IFont CreateFontSize(HSSFWorkbook hsswb, short size)
    {
        IFont font = hsswb.CreateFont();
        font.FontHeightInPoints = size;
        return font;
    }
}
