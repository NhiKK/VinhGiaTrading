<%@ WebHandler Language="C#" Class="FancyBox" %>

using System;
using System.Web;

public class FancyBox : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("[{ \"href\" : \"http://fancyapps.com/fancybox/demo/1_b.jpg\",  \"title\" : \"1st title\"},{ \"href\" : \"http://fancyapps.com/fancybox/demo/2_b.jpg\",\"title\" : \"2nd title\"},{ \"href\" : \"http://fancyapps.com/fancybox/demo/3_b.jpg\", \"title\" : \"3rd title\"}]");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}