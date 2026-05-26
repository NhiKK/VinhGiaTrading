using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default_Security : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        if(cookie != null)
        {
            if(cookie.Name == "VINHGIA2019_LOGIN")
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                FormsAuthentication.SignOut();
                Response.Redirect("");
            }
        }
    }
}
