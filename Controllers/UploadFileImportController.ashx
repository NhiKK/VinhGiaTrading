<%@ WebHandler Language="C#" Class="UploadFileImportController" %>

using System;
using System.Web;
using System.IO;

public class UploadFileImportController : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        String action = context.Request.QueryString["action"];
        switch (action)
        {
            case "fileloader":
                this.FileLoader(context);
                break;
            case "upload":
                this.UploadFile(context);
                break;
            case"delete":
                this.DeleteFile(context);
                break;
            default:
                break;
        }
    }

    public void UploadFile(HttpContext context)
    {
        foreach (String item in context.Request.Files)
        {
            HttpPostedFile file = context.Request.Files[item] as HttpPostedFile;
            file.SaveAs(context.Server.MapPath("../FileUpload/" + file.FileName));
            context.Response.Write(file.FileName);
        }
    }

    public void DeleteFile(HttpContext context)
    {
        try
        {
            String fileName = context.Request.QueryString["filename"];
            File.Delete(context.Server.MapPath("../FileUpload/" + fileName));
            context.Response.Write("Đã xóa " + fileName);
        }
        catch (Exception ex)
        {
            context.Response.Write("Lỗi: " + ex.Message);
        }
        
    }

    public void FileLoader(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        String filePath = context.Server.MapPath("../FileUpload/");
        String[] files = Directory.GetFiles(filePath);
        context.Response.Write(this.parserXML(files));
    }

    public String parserXML(String[] files)
    {
        String ret = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        ret += "<root>";
        foreach (var item in files)
        {
            String name, url;
            name = item.Substring(item.LastIndexOf('\\') + 1);
            url = item;
            ret += "<file>";
            ret += String.Format("<filename>{0}</filename>", name);
            ret += String.Format("<fileurl>{0}</fileurl>", url);
            ret += "</file>";
        }
        ret += "</root>";
        return ret;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}