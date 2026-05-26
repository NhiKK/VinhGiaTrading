using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String user = UserUtils.getUser(Context);
        if (user.Equals(""))
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            Response.Redirect("Default-Security.aspx");
        }
    }
}
