<%@ WebHandler Language="C#" Class="API_System" %>

using System;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

public class API_System : IHttpHandler {
    public JavaScriptSerializer js = new JavaScriptSerializer();
    private LinqDBDataContext db = new LinqDBDataContext();
    public string emailcc = "anco@ancopottery.com,info@ancopottery.com";
    //public string emailcc = "ngocnhan@esc.vn";
    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.QueryString["oper"];
        switch (oper)
        {
            case "loadImageProduct":
                this.loadImageProduct(context);
                break;
            case "loadImageDetai":
                this.loadImageDetai(context);
                break;
            case "loadImageMauSac":
                this.loadImageMauSac(context);
                break;
            case "loadPDF":
                this.loadPDF(context);
                break;
            case "downloadFile":
                this.downloadFile(context);
                break;
            case "autoSendMail":
                this.autoSendMail(context);
                break;
        }
    }

    public void autoSendMail(HttpContext context)
    {
        string msg = "";
        try
        {
            //var nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals("admin@email"));
            
            var mails = new List<Dictionary<string, string>>();
            var config = new ExportExcel();
            var items = config.getJsonInfoCHMTD();
            var itemsCP = items.Clone();

            foreach (var item in items)
            {
                var smtp = db.md_smtps.FirstOrDefault(p => p.smtpserver == item["smtp"]);

                var mail = new GoogleMail(item["email"], item["pass"], smtp.smtpserver, smtp.port.Value, smtp.use_ssl.GetValueOrDefault(false));

                var vaitro = item["vaitro_nhanmail"];

                var laplai = item["laplai"];

                var nguoinhans = string.Join(",", db.nhanviens.Where(s => s.mavt == vaitro).Select(s => s.email).ToList());

                string nbd = "", nkt = "", nsk = item["ngaysk"], nhl = item["ngayhl"], giohl = item["giohl"];

                DateTime? nskDT = null, nhlDT = null;
                try
                {
                    nskDT = DateTime.ParseExact(nsk, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch {

                }

                nhlDT = DateTime.ParseExact(nhl, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var checkSend = false;

                if (nhlDT == null)
                {
                    msg = "Ngày hiệu lực lỗi";
                }
                else
                {
                    int giohlInt = 0;
                    try { giohlInt = int.Parse(giohl); } catch { }

                    if (DateTime.Now >= nhlDT.Value.AddHours(giohlInt))
                    {
                        if (nskDT == null)
                        {
                            checkSend = true;
                        }
                        else
                        {
                            var nowSub = (DateTime.Now.Date - nskDT.Value).TotalDays;
                            if (laplai == "MD")
                            {
                                if (nowSub >= 1 & DateTime.Now.Hour >= giohlInt)
                                {
                                    checkSend = true;
                                }
                            }
                            else if (laplai == "MW")
                            {
                                if (nowSub >= 7 & DateTime.Now.Hour >= giohlInt)
                                {
                                    checkSend = true;
                                }
                            }
                            else if (laplai == "MM")
                            {
                                nowSub = (DateTime.Now.Date.Year - nskDT.Value.Year) * 12 + (DateTime.Now.Date.Month - nskDT.Value.Month);
                                if (nowSub >= 1 & DateTime.Now.Hour >= giohlInt)
                                {
                                    checkSend = true;
                                }
                            }
                        }

                        if (checkSend)
                        {
                            nskDT = DateTime.Now.Date;

                            nbd = nskDT.Value.ToString("dd/MM/yyyy");

                            if (laplai == "MD")
                                nkt = nskDT.Value.AddDays(1).ToString("dd/MM/yyyy");
                            else if (laplai == "MW")
                                nkt = nskDT.Value.AddDays(7).ToString("dd/MM/yyyy");
                            else if (laplai == "MM")
                                nkt = nskDT.Value.AddMonths(1).ToString("dd/MM/yyyy");

                            var tieude = item["tieude"].Replace("[startDate]", nbd).Replace("[endDate]", nkt);

                            var noidung = item["noidung"].Replace("[startDate]", nbd).Replace("[endDate]", nkt);
                            noidung = noidung.Replace("\n", "<br>");

                            var taptin = string.Format(item["taptin"], nbd, nkt);
                            string linkConnect = string.Format(@"{0}&sendmail=true", taptin);
                            var data = new Dictionary<string, object>();
                            var msgDT = Extension.GetModule(linkConnect, data);
                            var link = "";
                            try
                            {
                                var dicFile = JsonConvert.DeserializeObject<Dictionary<string,string>>(msgDT);
                                link = dicFile["link"];
                                item["ngaysk"] = nskDT.Value.ToString("dd/MM/yyyy");
                            }
                            catch (Exception ex)
                            {
                                msg = msgDT + "\n" + ex.ToString();
                                noidung = msgDT;
                                item["ngaysk"] = nskDT.Value.ToString("dd/MM/yyyy");
                            }

                            mail.Send(
                                item["email"]
                                , nguoinhans
                                , ""
                                , ""
                                , tieude
                                , noidung
                                , link
                            );
                        }
                        else
                        {
                            msg = "Chưa tới thời hạn";
                        }
                    }
                    else
                    {
                        msg = string.Format(@"Chưa tới ngày ""{0}"", lúc 9 giờ sáng.", nhl);
                    }
                }
            }

            if (msg.Length <= 0)
            {
                if (JsonConvert.SerializeObject(items) != JsonConvert.SerializeObject(itemsCP))
                {
                    config.writeJsonInfoCHMTD(JsonConvert.SerializeObject(items, Formatting.Indented));
                    msg = "Gửi mail thành công";
                }
                else
                {
                    msg = "Gửi mail thất bại";
                }
            }
        }
        catch(Exception ex) {
            msg = ex.ToString();
        }

        context.Response.Write(msg);
    }

    public void downloadFile(HttpContext context)
    {
        string name = context.Request.QueryString["name"];
        string type = context.Request.QueryString["type"];
        string path = context.Request.QueryString["path"];
        var remove = context.Request.QueryString["remove"] == "true";
        path = ExcuteSignalRStatic.mapPathSignalR("~/" + path);
        if (File.Exists(path))
        {
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);
            context.Response.Charset = "";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.WriteFile(path);
            context.Response.Flush();
            if(remove)
                File.Delete(path);
            context.Response.End();
        }
        else
        {
            context.Response.Write(string.Format("Không tìm thấy đường dẫn<br>{0}", path));
        }
    }

    public void loadPDF(HttpContext context)
    {
        var urlPDF = context.Request.QueryString["urlPDF"];
        var removePDF = context.Request.QueryString["remove"];
        urlPDF = urlPDF.Substring(0, urlPDF.LastIndexOf("?") <= 0 ? urlPDF.Length : urlPDF.LastIndexOf("?"));
        var url = ExcuteSignalRStatic.mapPathSignalR("~/" + urlPDF);
        var lastIndex = url.LastIndexOf(".");
        var mimeType = url.Substring(lastIndex + 1).ToLower();
        lastIndex = url.LastIndexOf("/");
        var fileName = url.Substring(lastIndex + 1).ToLower();
        if (File.Exists(url))
        {
            using (var fileStream = new FileStream(url, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    context.Response.ContentType = mimeType == "pdf" ? "application/pdf" : "image/jpeg";
                    context.Response.BinaryWrite(memoryStream.ToArray());
                }
            }

            if (File.Exists(url) & removePDF == "true")
            {
                File.Delete(url);
            }
        }
        else
        {
            context.Response.Write(url);
        }
    }

    public void loadImageMauSac(HttpContext context)
    {
        var msp = context.Request.QueryString["msp"];
        var url = context.Server.MapPath("../" + Public.imgColor + msp + ".jpg");
        var lastIndex = url.LastIndexOf(".");
        var mimeType = url.Substring(lastIndex + 1).ToLower();

        if (!File.Exists(url))
            url = context.Server.MapPath("../" + Public.imgNotFound);

        using (var fileStream = new FileStream(url, FileMode.Open))
        {
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                context.Response.ContentType = mimeType == "jpg" ? "image/jpeg" : (mimeType == "png" ? "image/png" : "image/gif");
                context.Response.BinaryWrite(memoryStream.ToArray());
            }
        }
    }

    public void loadImageDetai(HttpContext context)
    {
        var msp = context.Request.QueryString["msp"];
        var url = context.Server.MapPath("../" + Public.imgPattern + msp + ".jpg");
        var lastIndex = url.LastIndexOf(".");
        var mimeType = url.Substring(lastIndex + 1).ToLower();

        if (!File.Exists(url))
            url = context.Server.MapPath("../" + Public.imgNotFound);

        using (var fileStream = new FileStream(url, FileMode.Open))
        {
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                context.Response.ContentType = mimeType == "jpg" ? "image/jpeg" : (mimeType == "png" ? "image/png" : "image/gif");
                context.Response.BinaryWrite(memoryStream.ToArray());
            }
        }
    }

    public void loadImageProduct(HttpContext context)
    {
        var msp = context.Request.QueryString["msp"];
        var path = ExcuteSignalRStatic.getImageProduct(msp);
        var url = ExcuteSignalRStatic.mapPathSignalR("~/" + path);
        var lastIndex = url.LastIndexOf(".");
        var mimeType = url.Substring(lastIndex + 1).ToLower();

        if (!File.Exists(url))
            url = context.Server.MapPath("../" + Public.imgNotFound);

        using (var fileStream = new FileStream(url, FileMode.Open))
        {
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                context.Response.ContentType = mimeType == "jpg" ? "image/jpeg" : (mimeType == "png" ? "image/png" : "image/gif");
                context.Response.BinaryWrite(memoryStream.ToArray());
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
}