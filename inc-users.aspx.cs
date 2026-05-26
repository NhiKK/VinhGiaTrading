using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
public partial class inc_users : System.Web.UI.Page
{
	private LinqDBDataContext db = new LinqDBDataContext();
	public string result = "";
    protected void Page_Load(object sender, EventArgs e)
    {
		var smtps = db.md_smtps.Where(s=>s.hoatdong == true).OrderBy(s=>s.ngaytao).Select(s => new { s.md_smtp_id, s.smtpserver }).ToList();
		var smtpDic = new Dictionary<string, string>();
		smtpDic.Add("", "");
		foreach(var smtp in smtps) 
		{
			smtpDic.Add(smtp.md_smtp_id, smtp.smtpserver);
		}
		result = JsonConvert.SerializeObject(smtpDic);
    }
}
