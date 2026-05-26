using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for YeuCauXuatHangSql
/// </summary>
public static class YeuCauXuatHangSql
{
    public static string getSql(string c_donhang_id, string c_kehoachxuathang_id)
    {
		string where_ = "";
		if(c_kehoachxuathang_id != null && c_kehoachxuathang_id != "")
		{
			where_ = "AND kh.c_kehoachxuathang_id = N'"+ c_kehoachxuathang_id +"'";
		}
		
		if(c_donhang_id != null && c_donhang_id != "")
		{
			where_ = "AND dh.c_donhang_id = N'"+ c_donhang_id +"' ";
		}
        String str = String.Format(@"
            select 
	            dh.c_donhang_id
	            , dsdh.c_danhsachdathang_id
	            , dh.sochungtu as ct_dh
	            , dsdh.sochungtu as ct_dsdh
	            , dtkd.ten_dtkd
	            , dh.ngaylap
                , dsdh.huongdanlamhang as dacdiemhanghoa
                , dsdh.huongdanlamhangchung as huongdankhac
                , gh.ten_dtkd as noigiaohang
                , (select hoten from nhanvien where manv = dsdh.nguoitao) as nguoitao
            from 
	            c_donhang dh
				left join c_danhsachdathang dsdh  on dh.c_donhang_id = dsdh.c_donhang_id
				left join md_doitackinhdoanh dtkd on dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
				left join c_kehoachxuathang	kh on kh.c_donhang_id = dh.c_donhang_id
				left join md_doitackinhdoanh gh on gh.md_doitackinhdoanh_id = dsdh.diachigiaohang
            where
                dsdh.md_trangthai_id = 'HIEULUC'
                {0}
                AND dh.hoatdong = 1
				AND kh.c_danhsachdathang_id = dsdh.sochungtu
				", where_);
        return str;
    }
}