<%@ WebHandler Language="C#" Class="TaoPXuatTuNPNhapController" %>

using System;
using System.Web;
using System.Linq;
using System.Data;

public class TaoPXuatTuNPNhapController : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //try
        {
            string ids = context.Request.Form["p_s"];
            string msg = Public.taoPhieuXK(context, ids);
            context.Response.Write(msg);
        }
        //catch (Exception ex)
        //{
        //context.Response.Write(ex.Message);
        //}

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}