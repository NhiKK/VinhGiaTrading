using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;



public partial class UpdateLinq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void updateLinq_Click(object sender, EventArgs e)
    {
        ADmin_UpdateLinq.Exec_UpdateLinq(Context);
    }
}
