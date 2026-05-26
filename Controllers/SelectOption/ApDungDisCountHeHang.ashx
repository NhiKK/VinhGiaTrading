<%@ WebHandler Language="C#" Class="ApDungDisCountHeHang" %>

using System.Collections.Generic;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using System.Linq;

public class ApDungDisCountHeHang : IHttpHandler
{

    private LinqDBDataContext db = new LinqDBDataContext();
    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.QueryString["action"];
        switch (oper)
        {
            case "DiscountHH":
                this.load(context);
                break;
        }
    }

    public void load(HttpContext context)
    {
        string id = context.Request.QueryString["c_donhang_id"];
        var dis = db.c_donhangs.Where(s => s.c_donhang_id == id).Select(s => s.discount_hehang_value).FirstOrDefault();
        List<Dictionary<string, object>> nnl = null;
        if(!string.IsNullOrEmpty(dis))
            nnl = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(dis);

        var hh_ddh = string.Format(@"
            select cl.code_cl
            from c_dongdonhang ddh 
            left join md_sanpham sp on sp.md_sanpham_id = ddh.md_sanpham_id
            left join md_chungloai cl on cl.md_chungloai_id = sp.md_chungloai_id
            where ddh.c_donhang_id = '{0}'
            group by cl.code_cl
        ", id);

        var dt = mdbc.GetData(hh_ddh);
        var tbl = new List<Dictionary<string, object>>();
        foreach(DataRow row in dt.Rows)
        {
            string code_cl = row["code_cl"] + "";
            string gthh = nnl == null ? "0" : nnl.Where(s => s["hehang"].ToString() == code_cl).Select(s => s["giatri"].ToString()).FirstOrDefault();
            gthh = string.IsNullOrEmpty(gthh) ? "0" : gthh;

            tbl.Add(new Dictionary<string, object>()
            {
                { "hehang", code_cl },
                { "giatri",  gthh }
            });
        }

        context.Response.Write(JsonConvert.SerializeObject(tbl));
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
