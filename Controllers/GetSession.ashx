<%@ WebHandler Language="C#" Class="GetSession" %>

using System;
using System.Web;
using System.Web.Security;

public class GetSession : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        String username = UserUtils.getUser(context);
        FormsAuthentication.SetAuthCookie(username, false);
        context.Response.Write("");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}