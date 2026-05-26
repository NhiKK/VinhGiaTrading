using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            GoogleMail mail = new GoogleMail("nhan@esc.vn", "yvCG7yaf", "mail.esc.vn", 25);
            mail.Send("nhan@esc.vn", "admin@ancopottery.com", "nhan@esc.vn", "", "Hello test mail", "Ha ha ha", Server.MapPath("FileSendMail/thanhbinhDon-Dat-Hang-433708-08-10-2013.pdf"));
            lbl.Text = "Thành công";
        //}catch(Exception ex)
        //{
        //    lbl.Text = ex.Message;
        //}
    }
}