using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class inc_nhaplieu_bcThopDTCP : System.Web.UI.Page
{
    public string jsonYear = "", jsonMonth = "", jsonXuong = "";
    public LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        var nowYear = DateTime.Now.Year;
        var list = new List<string>();
        list.Add(":");
        for (var i = nowYear + 2; i >= 2020; i--)
        {
            list.Add(string.Format(@"{0}:{1}", i, i));
        }
        jsonYear = string.Join(";", list);

        list = new List<string>();
        list.Add(":");
        for (var i = 1; i <= 1; i++)
        {
            list.Add(string.Format(@"{0}:{1}", i, i));
        }
        jsonMonth = string.Join(";", list);
        
        var arr = new string[] { "ANCO1", "ANCO2" };
        list = new List<string>();
        list.Add(":");
        foreach (var xuong in db.md_doitackinhdoanhs.Where(s=> arr.Contains(s.ma_dtkd)).OrderBy(s=>s.ma_dtkd))
        {
            list.Add(string.Format(@"{0}:{1}", xuong.md_doitackinhdoanh_id, xuong.ma_dtkd));
        }
        jsonXuong = string.Join(";", list);
    }
}
