<%@ WebHandler Language="C#" Class="ImportAllProduct" %>

using System;
using System.Web;

public class ImportAllProduct : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
            string databaseName = "ANCOWLINK.ancow2014";
            string strDel = string.Format("delete {0}.[dbo].[md_sanpham]", databaseName);
            string strInsert = string.Format(@"insert into {0}.dbo.md_sanpham
                                select 
	                                REPLACE(NEWID(), '-', '')
	                                , sp.ma_sanpham
	                                , sp.mota_tienganh
	                                , l2_mix
	                                , w2_mix
	                                , h2_mix
	                                , v2
	                                , sl_outer
	                                , soluonggoi_ctn
	                                , (select md_cangbien.md_cangbien_id from {0}.dbo.md_cangbien where macangbien = cb.ma_cangbien)
	                                , (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer) 
                                from 
	                                md_sanpham sp
	                                , md_donggoi dg
	                                , md_donggoisanpham dgsp
	                                , md_cangbien cb
                                where
	                                sp.md_sanpham_id = dgsp.md_sanpham_id
	                                AND dgsp.md_donggoi_id = dg.md_donggoi_id
	                                AND sp.md_cangbien_id = cb.md_cangbien_id
	                                AND dgsp.macdinh = 1
	                                AND sp.trangthai='DHD'
	                                AND sp.md_sanpham_id NOT IN( select distinct md_sanpham_id from md_hanghoadocquyen )", databaseName);

            mdbc.ExcuteNonQuery(strDel);
            mdbc.ExcuteNonQuery(strInsert);

            context.Response.Write("Import thành công!");
        }catch(Exception ex)
        {
            context.Response.Write("Lỗi: " + ex.Message);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}