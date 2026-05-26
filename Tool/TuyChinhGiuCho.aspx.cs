using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
public partial class Tool_TuyChinhGiuCho : System.Web.UI.Page
{
    public bool send = false;
    public ExportExcel config = new ExportExcel();

    public string loadOptions(List<Dictionary<string, string>> jsons = null)
    {
        if(jsons == null)
            jsons = config.getJsonInfoCHGC();

        var selectRpt = jsons.Select(s =>
                    string.Format("<option mau='{0}' songay='{1}' value='{0}'>{0}</option>",
                        s["mau"].ToString(),
                        s["songay"].ToString()
                    )
                ).ToList();

       return string.Join("", selectRpt);
    }

    protected void Page_Load(object sender, EventArgs e)
    {   
        send = Request.Form["send"] == "true";
        var mau = Request.Form["mau"];
        var songay = Request.Form["songay"];
        string msg = "";
        if (send)
        {
            try
            {
                var jsons = config.getJsonInfoCHGC();
                var json = jsons.Where(s => s["mau"].ToString() == mau).FirstOrDefault();
                if (json != null)
                {
                    json["songay"] = songay;
                    config.writeJsonInfoCHGC(JsonConvert.SerializeObject(jsons, Formatting.Indented));
                }

                Response.Write(loadOptions(jsons));
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
}
