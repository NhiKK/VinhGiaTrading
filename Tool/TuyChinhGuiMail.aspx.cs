using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
public partial class Tool_TuyChinhGuiMail : System.Web.UI.Page
{
    public bool send = false;
    public ExportExcel config = new ExportExcel();

    public string loadOptions(List<Dictionary<string, string>> jsons = null)
    {
        if(jsons == null)
            jsons = config.getJsonInfoCHMTD();

        var selectRpt = jsons.Select(s =>
                    string.Format("<option mauin='{0}' laplai='{1}' ngayhl='{2}' tieude='{3}' noidung='{4}' vaitro='{5}' ngaysk='{6}' giohl='{7}' value='{0}'>{0}</option>",
                        s["mauin"].ToString(),
                        s["laplai"].ToString(),
                        s["ngayhl"].ToString(),
                        s["tieude"].ToString().Replace("'", "\\'"),
                        s["noidung"].ToString().Replace("'", "\\'"),
                        s["vaitro_nhanmail"].ToString(),
                        s["ngaysk"].ToString(),
                        s["giohl"].ToString()
                    )
                ).ToList();

       return string.Join("", selectRpt);
    }

    public string loadVaitros()
    {
        var db = new LinqDBDataContext();
        
        var selectRpt = db.vaitros.Select(s =>
                    string.Format("<option value='{0}'>{1}</option>",
                        s.mavt,
                        s.tenvt
                    )
                ).ToList();

        return string.Join("", selectRpt);
    }

    protected void Page_Load(object sender, EventArgs e)
    {   
        send = Request.Form["send"] == "true";
        var mauin = Request.Form["mauin"];
        var laplai = Request.Form["laplai"];
        var ngayhl = Request.Form["ngayhl"];
        var giohl = Request.Form["giohl"];
        var ngaysk = Request.Form["ngaysk"];
        var tieude = Request.Form["tieude"];
        var noidung = Request.Form["noidung"];
        var vaitro = Request.Form["vaitro"];
        string msg = "";
        if (send)
        {
            try
            {
                var jsons = config.getJsonInfoCHMTD();
                var json = jsons.Where(s => s["mauin"].ToString() == mauin).FirstOrDefault();
                if (json != null)
                {
                    json["laplai"] = laplai;
                    json["ngayhl"] = ngayhl;
                    json["ngaysk"] = ngaysk;
                    json["giohl"] = giohl;
                    json["tieude"] = tieude;
                    json["noidung"] = noidung;
                    json["vaitro_nhanmail"] = vaitro;
                    config.writeJsonInfoCHMTD(JsonConvert.SerializeObject(jsons, Formatting.Indented));
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
