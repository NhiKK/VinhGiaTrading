using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class ReportWizard_RptKhachHang_rpt_lietkehangbanchaypo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string strStartDate = Request.QueryString["startdate"];
		string strEndDate = Request.QueryString["enddate"];
		try {
			DateTime startdate = DateTime.ParseExact(strStartDate, "dd/MM/yyyy", null);
			DateTime enddate = DateTime.ParseExact(strEndDate, "dd/MM/yyyy", null);
			
			PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
			isreadonly.SetValue(this.Request.QueryString, false, null);
			this.Request.QueryString.Set("startdate", startdate.ToString("MM/dd/yyyy"));
			this.Request.QueryString.Set("enddate", enddate.ToString("MM/dd/yyyy"));
			isreadonly.SetValue(this.Request.QueryString, true, null);
		}
		catch {
		
		}
    }
}
