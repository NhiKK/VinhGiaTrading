using System;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
/// <summary>
/// Summary description for Helper
/// </summary>
public static class Helper
{
    public static string SOANTHAO = "SOANTHAO";
    public static string HIEULUC = "HIEULUC";
    public static string CHUAGUI = "CHUAGUI";
    public static string[] nhaCungCaps = new string[] { "VINHGIA", "HOANGHUYHUNG" };
    public static void removeFileWithPath(string path)
    {
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);
    }

    public static void removeDirectoryWithPath(string path)
    {
        if (System.IO.Directory.Exists(path))
        {
            var filesLost = new System.IO.DirectoryInfo(path).GetFiles("*");
            foreach (var file in filesLost)
                System.IO.File.Delete(file.FullName);
            System.IO.Directory.Delete(path);
        }
    }

    public static string getNewId()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }

    public static string EncodeHTML_VNN(string a)
    {
        if (a == null)
        {
            a = "";
        }
        a = a.Replace("&", "%26");
        a = a.Replace("+", "%2B");
        a = a.Replace("&", "%26");
        a = a.Replace("+", "%2B");
        return a;
    }

    public static System.Collections.Generic.Dictionary<string, string> pathReport(string name)
    {
        name = name.Replace("/", "-");
        var obj = new System.Collections.Generic.Dictionary<string, string>();
        obj["name"] = name;
        obj["link"] = HttpContext.Current.Server.MapPath("~/DEV_REPORT/pdfs/" + name);
        obj["linkView"] = EncodeHTML_VNN("DEV_REPORT/pdfs/" + name);
        return obj;
    }

    public static System.Collections.Generic.Dictionary<string, string> pathImport(string name)
    {
        name = name.Replace("/", "-");
        var obj = new System.Collections.Generic.Dictionary<string, string>();
        obj["name"] = name;
        obj["link"] = HttpContext.Current.Server.MapPath("~/file_import/" + name);
        obj["linkView"] = "~/file_import/" + name;
        return obj;
    }

    public static string getConnectStrings(string ten_connectstring)
    {
        return System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ten_connectstring].ConnectionString;
    }

    public static string getFilter(HttpContext context)
    {
        string filter = "";
        jqGridHelper.Filter f = new jqGridHelper.Filter();
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            if (_filters != null & _filters != "")
            {
                f = jqGridHelper.Filter.CreateFilter(_filters);
                filter = f.ToScript();
            }
        }
        return filter;
    }

    public static int Week_(DateTime? nullable)
    {
        if (nullable.HasValue)
        {
            GregorianCalendar gCalendar = new GregorianCalendar();
            int WeekNumber = gCalendar.GetWeekOfYear(nullable.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return WeekNumber;
        }
        else
            return 0;
    }

    public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
        DateTime firstThursday = jan1.AddDays(daysOffset);
        var cal = CultureInfo.CurrentCulture.Calendar;
        int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        var weekNum = weekOfYear;
        if (firstWeek == 1)
        {
            weekNum -= 1;
        }
        var result = firstThursday.AddDays(weekNum * 7);
        return result.AddDays(-3);
    }

    public static void downloadFiles(HttpContext context, string name, string link, int type)
    {
        string contentType = type == 0 ? "application/pdf" : "application/vnd.ms-excel";
        context.Response.Buffer = true;
        context.Response.Clear();
        context.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);
        context.Response.AddHeader("content-length", new System.IO.FileInfo(link).Length.ToString());
        context.Response.ContentType = contentType;
        context.Response.Charset = "";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.WriteFile(link);
        context.Response.End();
    }

    public static void viewFile(DevExpress.XtraReports.Web.ReportViewer viewer)
    {
        string linkExcel = viewer.Attributes["linkExcel"];
        string linkViewExcel = viewer.Attributes["linkViewExcel"];
        string nameExcel = viewer.Attributes["nameExcel"];
        string data = viewer.Attributes["files"];
        string url = "";

        string link = "", linkView = "", name = "";
        if (string.IsNullOrWhiteSpace(linkExcel))
        {
            var pathRp = pathReport(viewer.Report.DisplayName + ".pdf");
            viewer.Report.ExportToPdf(pathRp["link"]);
            linkView = pathRp["linkView"];
            link = pathRp["link"];
            name = pathRp["name"];
        }
        else
        {
            linkView = linkViewExcel;
            link = linkExcel;
            name = nameExcel;
        }

        if (!string.IsNullOrWhiteSpace(data))
        {

        }
        else
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            var pathRDR = "ViewPDFPublic/index.aspx?urlPDF=" + linkView + "&remove=true&zoom=page-width&zoomprint=0.999&id=" + Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(appPath) | appPath == "/")
                url = "/" + pathRDR;
            else
                url = appPath + "/" + pathRDR;
        }

        HttpContext.Current.Response.Redirect(url);
    }
}