<%@ WebHandler Language="C#" Class="NhapXuatCalTotalController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Collections;

public class NhapXuatCalTotalController : IHttpHandler {

    LinqDBDataContext db = new LinqDBDataContext();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string c_nhapxuat_id = context.Request.QueryString["c_nhapxuat_id"];
        decimal totalQuantity = 0, totalSalary = 0;
        
        c_nhapxuat nx = db.c_nhapxuats.FirstOrDefault(p => p.c_nhapxuat_id.Equals(c_nhapxuat_id));
        
        if (nx != null)
        {
            var dnx = from c in db.c_dongnhapxuats where c.c_nhapxuat_id.Equals(nx.c_nhapxuat_id) select c;
            totalQuantity = dnx.Sum(p => p.slthuc_nhapxuat).Value;
            totalSalary = dnx.Sum(p => p.slthuc_nhapxuat * p.dongia).Value;
        }

        context.Response.Write(String.Format("Tổng số lượng: {0}, tổng tiền: {1} ", totalQuantity, totalSalary));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}