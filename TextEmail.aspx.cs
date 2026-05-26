using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TextEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Type type = DateTime.Now.GetType();
        Response.Write(type.FullName);
    }
}
