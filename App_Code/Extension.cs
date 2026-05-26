using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using System.IO;
using System.Data;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Reflection;
using System.Net;
using System.Text;

/// <summary>
/// Summary description for Extension
/// </summary>
public static class Extension
{
    public static string fmtDate = "dd/MM/yyyy";
	public static string API_VINHGIA = System.Web.Configuration.WebConfigurationManager.AppSettings["API_VINHGIA"];
	public static string API_HOANGHUYHUNG = System.Web.Configuration.WebConfigurationManager.AppSettings["API_HOANGHUYHUNG"];

    public static string API_ALL(string ma_dtkd)
    {
        return System.Web.Configuration.WebConfigurationManager.AppSettings["API_" + ma_dtkd];
    }
    public static string removeAllSpaceOrTrimText(this string a, bool onlyTrim)
    {
        if (a == null)
            a = "";

        return onlyTrim ? a.Trim() : a.Replace(" ", "");
    }

    public static string ConvertXML(this DataTable dtMain, int page, int total, Int64 records)
    {
        DataSet ds = new DataSet();
        ds.DataSetName = "rows";
        dtMain.TableName = "row";
        List<int> columnDate = new List<int>();
        for (int i = 0; i < dtMain.Columns.Count; i++)
        {
            var type = dtMain.Columns[i].DataType;
            if (type == typeof(DateTime))
            {
                columnDate.Add(i);
            }
        }

        DataTable dt = dtMain.Clone();
        foreach (int typeDate in columnDate)
        {
            dt.Columns[typeDate].DataType = typeof(String);
        }

        foreach (DataRow row in dtMain.Rows)
        {
            dt.ImportRow(row);
        }

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            DataRow row = dt.Rows[j];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (j == 0)
                {
                    var type = dt.Columns[i].DataType;
                }
                dt.Columns[i].ReadOnly = false;

                if (string.IsNullOrEmpty(row[i].ToString()))
                    row[i] = string.Empty;
                else if (columnDate.Contains(i))
                {
                    DateTime? chkDate = row[i].ToString().getDate();
                    if (chkDate.HasValue)
                        row[i] = chkDate.Value.ToString(fmtDate);
                }

                if (j == dt.Rows.Count - 1)
                {
                    dt.Columns[i].ColumnName = "cellVNNVNNVNNVNNVNNVNN" + i;
                }
            }
        }
        ds.Tables.Add(dt);
        string myXmlString = ds.GetXml();
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(myXmlString);

        XmlNode nodeRecords = xml.CreateElement("records");
        nodeRecords.InnerText = records.ToString();
        xml.DocumentElement.PrependChild(nodeRecords);

        XmlNode nodeTotal = xml.CreateElement("total");
        nodeTotal.InnerText = total.ToString();
        xml.DocumentElement.PrependChild(nodeTotal);

        XmlNode nodePage = xml.CreateElement("page");
        nodePage.InnerText = page.ToString();
        xml.DocumentElement.PrependChild(nodePage);
        string xmlFinal = xml.OuterXml;
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            xmlFinal = xmlFinal.SafeReplace("cellVNNVNNVNNVNNVNNVNN" + i, "cell", true);
        }
        return xmlFinal;
    }

    public static DateTime? getDate(this string t)
    {
        DateTime tempDate;
        DateTime.TryParse(t, out tempDate);
        return tempDate;
    }

    public static string ConvertJson(this DataTable t, int page, int total, Int64 records)
    {
        var result = new
        {
            page = page,
            total = total,
            records = records,
            rows = t
        };
        return JsonConvert.SerializeObject(result);
    }

    public static string SafeReplace(this string input, string find, string replace, bool matchWholeWord)
    {
        string textToFind = matchWholeWord ? string.Format(@"\b{0}\b", find) : find;
        return Regex.Replace(input, textToFind, replace);
    }
	
	public static decimal DecRound(this decimal dtMain, int dec)
    {
        dtMain = Decimal.Round(dtMain, dec);
        return dtMain;
	}

    public static T Cast<T>(this Object myobj)
    {
        Type objectType = myobj.GetType();
        Type target = typeof(T);
        var x = Activator.CreateInstance(target, false);
        var z = from source in objectType.GetMembers().ToList()
                where source.MemberType == MemberTypes.Property
                select source;
        var d = from source in target.GetMembers().ToList()
                where source.MemberType == MemberTypes.Property
                select source;
        List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
           .ToList().Contains(memberInfo.Name)).ToList();
        PropertyInfo propertyInfo;
        object value;
        foreach (var memberInfo in members)
        {
            propertyInfo = typeof(T).GetProperty(memberInfo.Name);
            try
            {
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                propertyInfo.SetValue(x, value, null);
            }
            catch
            {

            }
        }
        return (T)x;
    }

	public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
    {
        var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
        var destProps = typeof(TU).GetProperties()
                .Where(x => x.CanWrite)
                .ToList();

        foreach (var sourceProp in sourceProps)
        {
            if (destProps.Any(x => x.Name == sourceProp.Name))
            {
                var p = destProps.First(x => x.Name == sourceProp.Name);
                if (p.CanWrite)
                { // check if the property can be set or no.
                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
                }
            }

        }
    }
	
	public static string DecodeHTML(string text)
    {
        try { text = text.Replace("0ψ0", "<").Replace("1Ψ1", ">"); }
        catch { }
        return text;
    }

    public static string EncodeHTML(string text)
    {
        try { text = text.Replace("<", "0ψ0").Replace(">", "1Ψ1"); }
        catch { }
        return text;
    }

    public static string DropTrailingZeros(this decimal value)
    {
        return value.ToString("#,#0.####");
    }

    public static T Clone<T>(this T source)
    {
        var dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
        using (var ms = new System.IO.MemoryStream())
        {
            dcs.WriteObject(ms, source);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            return (T)dcs.ReadObject(ms);
        }
    }

    public static void delayTask(this System.Threading.Tasks.Task task, int time)
    {
        System.Threading.Timer timer = null;
        timer = new System.Threading.Timer((call) => {
            if (timer != null)
            {
                task.Start();
                timer.Dispose();
            }
        }, null, time, time);

        task.Wait();
    }

    public static string GetModule(string link, Dictionary<string, object> jsonData)
    {
        var context = System.Web.HttpContext.Current;
        var url = context.Request.Url;
        if (!link.Contains("http"))
        {
            var vtp = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            if (string.IsNullOrWhiteSpace(vtp))
                link = url.GetLeftPart(UriPartial.Authority) + "/" + link;
            else
                link = url.GetLeftPart(UriPartial.Authority) + vtp + "/" + link;
        }
        var myUri = new Uri(link);
        string jsonString = JsonConvert.SerializeObject(jsonData);
        var request = (HttpWebRequest)WebRequest.Create(myUri);
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "POST";

        request.CookieContainer = new CookieContainer();
        var authCookie = context.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
        request.CookieContainer.Add(new System.Net.Cookie(authCookie.Name, authCookie.Value, authCookie.Path, url.Host));

        string postData = string.Format(@"data={0}", jsonString);
        Stream reqStream = request.GetRequestStream();
        byte[] postArray = Encoding.UTF8.GetBytes(postData);
        reqStream.Write(postArray, 0, postArray.Length);
        reqStream.Close();
        var sr = new StreamReader(request.GetResponse().GetResponseStream());
        string result = sr.ReadToEnd();
        return result;
    }

    public static int? ToNullableInt(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return int.Parse(s);
        else
            return null;
    }

    public static long? ToNullableLong(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return long.Parse(s);
        else
            return null;
    }

    public static decimal? ToNullableDecimal(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return decimal.Parse(s);
        else
            return null;
    }

    public static double? ToNullableDouble(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return double.Parse(s);
        else
            return null;
    }

    public static bool? ToNullableBool(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return s.ToLower() == "true";
        else
            return null;
    }
}