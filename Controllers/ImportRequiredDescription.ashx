<%@ WebHandler Language="C#" Class="ImportRequiredDescription" %>

using System;
using System.Web;

public class ImportRequiredDescription : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
            string databaseName = "ANCOWLINK.ancow2014";
            string strDelChungLoai = string.Format("delete {0}.[dbo].[md_chungloai]", databaseName);
            string strDelDeTai = string.Format("delete {0}.[dbo].[md_detai]", databaseName);
            string strDelMauSac = string.Format("delete {0}.[dbo].[md_mausac]", databaseName);

            string strInsertChungLoai = string.Format(@"insert into {0}.[dbo].md_chungloai select cl.md_chungloai_id, cl.code_cl, cl.ta_ngan from md_chungloai cl", databaseName);
            string strInsertDeTai = string.Format(@"insert into {0}.[dbo].md_detai select dt.md_detai_id, dt.md_chungloai_id, dt.code_cl, dt.code_dt, dt.ta_ngan from md_detai dt", databaseName);
            string strInsertMauSac = string.Format(@"insert into {0}.[dbo].md_mausac select ms.md_mausac_id, ms.md_detai_id, ms.md_chungloai_id, ms.code_mau, ms.code_cl, ms.code_dt, ms.ta_ngan from md_mausac ms", databaseName);

            mdbc.ExcuteNonQuery(strDelChungLoai);
            mdbc.ExcuteNonQuery(strDelDeTai);
            mdbc.ExcuteNonQuery(strDelMauSac);

            mdbc.ExcuteNonQuery(strInsertChungLoai);
            mdbc.ExcuteNonQuery(strInsertDeTai);
            mdbc.ExcuteNonQuery(strInsertMauSac);
            context.Response.Write("Cập nhật mô tả thành công!");
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