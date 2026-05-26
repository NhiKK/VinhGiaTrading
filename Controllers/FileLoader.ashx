<%@ WebHandler Language="C#" Class="FileLoader" %>

using System;
using System.Web;
using System.IO;

public class FileLoader : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
    }

    
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}