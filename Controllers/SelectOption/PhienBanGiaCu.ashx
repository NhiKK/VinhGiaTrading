<%@ WebHandler Language="C#" Class="PhienBanGiaCu" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Data.Linq;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

public class PhienBanGiaCu : IHttpHandler 
{
    
    private LinqDBDataContext db = new LinqDBDataContext();
	public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.QueryString["action"];
        switch (oper)
        {
            case "PhienBanGiaCu":
                this.load(context);
                break;
        }
    }

    public void load(HttpContext context)
    {
		string select = @"<select>{0}</select>";
        string option = "<option value={0}>{1}</option>";
        string option_ = "";
        option_ += string.Format(option, "", "");
        foreach(md_phienbangia pbg in db.md_phienbangias.Where(s=>s.hoatdong == true).OrderBy(s=>s.ten_phienbangia)) {
            option_ += string.Format(option, pbg.md_phienbangia_id, pbg.ten_phienbangia);
        }
        
        select = string.Format(select, option_);
        context.Response.Write(select);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
