using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QueryUtils
/// </summary>
public static class LinqUtils
{
    private static LinqDBDataContext db = new LinqDBDataContext();

    
    /// <summary>
    /// Lấy đóng gói mặc định của mã sản phẩm
    /// </summary>
    /// <param name="md_sanpham_id">Mã sản phẩm</param>
    /// <returns>Thông tin đóng gói của sản phẩm</returns>
    public static md_donggoi getDongGoi(String md_sanpham_id)
    {
        try
        {
            String md_donggoi_id = db.md_donggoisanphams.Single(p => p.md_sanpham_id.Equals(md_sanpham_id) && p.macdinh.Equals(true)).md_donggoi_id;
            md_donggoi donggoi = db.md_donggois.Single(p => p.md_donggoi_id.Equals(md_donggoi_id));
            return donggoi;
        }
        catch
        {
            return null;
        }
    }

    
    /// <summary>
    /// Lấy số thứ tự cao nhất dòng báo giá nếu không có thì trả về 0
    /// </summary>
    /// <param name="c_baogia_id">Mã báo giá</param>
    /// <returns>Số thứ tự cao nhất của sản phẩm thuộc báo giá</returns>
    public static int getMaxSttBaoGia(String c_baogia_id)
    {
        try
        {
            int stt = (from p in db.c_chitietbaogias where p.c_baogia_id.Equals(c_baogia_id) select p.sothutu).Max().Value;
            return stt;
        }
        catch
        {
            return 0;
        }
    }

    
    /// <summary>
    /// Lấy số thứ tự cao nhất dòng báo giá nếu không có thì trả về 0
    /// </summary>
    /// <param name="c_donhang_id">Mã đơn hàng</param>
    /// <returns>Số thứ tự cao nhất của sản phẩm thuộc đơn hàng</returns>
    public static int getMaxSttDonHang(String c_donhang_id)
    {
        try
        {
            int stt = (from p in db.c_dongdonhangs where p.c_donhang_id.Equals(c_donhang_id) select p.sothutu).Max().Value;
            return stt;
        }
        catch
        {
            return 0;
        }
    }

    public static List<String> GetUserListInGroup(String MaNhanVien)
    {
        String sel = String.Format(@" select distinct ctn.manv from md_chitietnhom ctn 
                                      where ctn.md_nhom_id IN (select md_nhom_id from md_chitietnhom 
                                                                where manv = @manv and nguoiquanly = 1)");
        System.Data.DataTable dt = mdbc.GetData(sel, "@manv", MaNhanVien);
        List<String> lst = new List<string>();
        foreach(System.Data.DataRow row in dt.Rows)
        {
            lst.Add(row[0].ToString());
        }
        return lst;
    }
}
