using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class cdm_BCKH_khachhangList : System.Web.UI.UserControl
{
    public string Name = null;
    public string Value = null;
    public string md_doitackinhdoanh_id = null;
    public bool isncc = false;
    public bool Disabled = false;
    public string OnChange = null;
    public bool NullFirstItem = false;
    public int Width = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }
}
