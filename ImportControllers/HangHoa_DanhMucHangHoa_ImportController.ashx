<%@ WebHandler Language="C#" Class="HangHoa_DanhMucHangHoa_ImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Linq;

public class HangHoa_DanhMucHangHoa_ImportController : IHttpHandler
{
    public class SANPHAM_DMHH
    {
        public string ma_sanpham { get; set; }
        public string ma_danhmuc { get; set; }
    }
    public string FileLink = "";
    public LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest (HttpContext context) {
        String filename = context.Request.QueryString["filename"];
        FileLink = context.Server.MapPath("../FileUpload/" + filename);
        int j = FileLink.LastIndexOf(".");
        if(FileLink.Substring(j) != ".xls") {
            FileLink = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(FileLink);
        }
        Workbook wb = Workbook.Load(FileLink);
        Worksheet ws = wb.Worksheets[0];
        this.NewFromCellCollection(context, ws.Cells);
    }

    public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        String msg = "<br>";
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        {
            string msgDT = "";
            msgDT += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
            try
            {
                Row row = cellCollection.Rows[i];
                // Biến kiểm tra giá trị cuối cùng của một dòng -- nếu bằng false sẽ chạy dòng tiếp theo
                bool next = true;

                // Danh sách tạm chứ thông tin của một dòng nhà cung ứng
                // bao gồm luôn cả mã sản phẩm, mã đối tác và mặc định
                var lstNhaCungUng = new System.Collections.Generic.List<SANPHAM_DMHH>();
                int j = 1;

                // Vòng lặp đến khi giá trị cuối cùng bằng null
                while (next)
                {
                    object item = row.GetCell(j).Value;
                    if (item == null)
                    {
                        next = false; // khi gia tri cua cell bang null
                    }
                    else
                    {
                        var ncu = new SANPHAM_DMHH();
                        ncu.ma_sanpham = row.GetCell(0).Value.ToString();
                        ncu.ma_danhmuc = (String)item;
                        ncu.ma_danhmuc = ncu.ma_danhmuc.Trim();
                        lstNhaCungUng.Add(ncu);
                        j++;
                    }
                }



                foreach (var ncu in lstNhaCungUng)
                {
                    var sp = ImportUtils.getSanPham(ncu.ma_sanpham);
                    var dmhh = db.md_danhmuchanghoas.Where(s => s.ma_danhmuc == ncu.ma_danhmuc | s.ten_danhmuc == ncu.ma_danhmuc).FirstOrDefault();
                    bool check = true;

                    // Kiểm tra mã hàng hóa
                    if (sp == null)
                    {
                        check = false;
                        msgDT += String.Format("<div>Không tìm thấy mã sản phẩm \"{0}\"</div>", ncu.ma_sanpham);
                    }

                    // Kiểm tra mã đối tác
                    if (dmhh == null)
                    {
                        check = false;
                        msgDT += String.Format("<div>Không tìm thấy danh mục hàng hóa \"{0}\"</div>", ncu.ma_danhmuc);
                    }

                    // Kiểm tra có mã hàng và mã đối tác đầy đủ mới tiếp tục thực hiện.
                    if (check)
                    {
                        var n = db.md_sanpham_dmhhs.Where(s => s.md_sanpham_id == sp.md_sanpham_id & s.md_danhmuchanghoa_id == dmhh.md_danhmuchanghoa_id).FirstOrDefault();
                        if (n == null)
                        {
                            n = new md_sanpham_dmhh();
                            n.md_sanpham_dmhh_id = Helper.getNewId();
                            n.md_sanpham_id = sp.md_sanpham_id;
                            n.md_danhmuchanghoa_id = dmhh.md_danhmuchanghoa_id;
                            n.ma_danhmuc = dmhh.ma_danhmuc;
                            n.ten_danhmuc = dmhh.ten_danhmuc;
                            n.ngaytao = DateTime.Now;
                            n.ngaycapnhat = DateTime.Now;
                            n.nguoitao = UserUtils.getUser(context);
                            n.nguoicapnhat = UserUtils.getUser(context);
                            n.mota = "";
                            n.hoatdong = true;
                            db.md_sanpham_dmhhs.InsertOnSubmit(n);
                            db.SubmitChanges();
                            msgDT += String.Format("<div style=\"color:green\">Đã thêm danh mục hàng hóa \"{0}\" cho sản phẩm \"{1}\"</div>", ncu.ma_danhmuc, ncu.ma_sanpham);
                        }
                        else
                        {
                            n.md_danhmuchanghoa_id = dmhh.md_danhmuchanghoa_id;
                            n.ma_danhmuc = dmhh.ma_danhmuc;
                            n.ten_danhmuc = dmhh.ten_danhmuc;
                            n.ngaycapnhat = DateTime.Now;
                            n.nguoicapnhat = UserUtils.getUser(context);
                            n.mota = "";
                            n.hoatdong = true;
                            db.SubmitChanges();
                            msgDT += String.Format("<div style=\"color:green\">Đã cập nhật danh mục hàng hóa \"{0}\" cho sản phẩm \"{1}\"</div>", ncu.ma_danhmuc, ncu.ma_sanpham);
                        }
                    }
                }

                msgDT += String.Format("<div>--- Kết thúc dòng {0} ---</div>", i + 1);
            }
            catch (Exception ex)
            {
                msgDT += String.Format("<div style=\"color:red\">Dòng {0} xảy ra lỗi {1}</div>", i + 1, ex.Message);
            }

            msg += "<br>" + msgDT;
        }

        context.Response.Write(msg);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}