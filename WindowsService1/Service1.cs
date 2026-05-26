using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public string linkEman = "", linkHost = "";
        public int sophutNV = 15;

        public Service1()
        {
            string pathRoot = AppDomain.CurrentDomain.BaseDirectory.Replace(@"WindowsService1\bin\Debug\", "");
            pathRoot += $"\\Web.config";
            var xmlStr = File.ReadAllText(pathRoot);
            var str = XElement.Parse(xmlStr);
            var result = str.Elements("appSettings");
            string link = "";
            foreach (var a in result)
            {
                var result2 = a.Elements("add");
                foreach (var b in result2)
                {
                    var name = b.Attribute("key").Value;
                    if (name == "urlEMan")
                        linkHost = b.Attribute("value").Value;
                    if (name == "urlNhacViec")
                        link = b.Attribute("value").Value;
                    if (name == "soPhutNhacViec")
                    {
                        try
                        {
                            sophutNV = int.Parse(b.Attribute("value").Value);
                        }
                        catch { }
                    }
                }
            }

            linkEman = linkHost + "/" + link;

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000 * sophutNV; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {

        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
        }

        public string GetCookie()
        {
            string formData = "";
            formData += string.Format(@"oper=ESC_getCookie");
            var myUri = new Uri(linkHost + "/Controllers/API_Android.ashx");
            string postData = formData;
            var request = (HttpWebRequest)WebRequest.Create(myUri);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            Stream reqStream = request.GetRequestStream();
            byte[] postArray = Encoding.UTF8.GetBytes(postData);
            reqStream.Write(postArray, 0, postArray.Length);
            reqStream.Close();
            var sr = new StreamReader(request.GetResponse().GetResponseStream());
            string result = sr.ReadToEnd();
            return result;
        }

        public void WriteToFile(string Message)
        {
            try
            {
                var myUri = new Uri(linkEman);
                var request = (HttpWebRequest)WebRequest.Create(myUri);
                request.Method = "GET";
                var cookie = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(GetCookie());
                request.CookieContainer = new CookieContainer();
                var authCk = new System.Net.Cookie();
                authCk.Name = cookie["Name"];
                authCk.Value = cookie["Value"];
                authCk.Domain = myUri.Host;
                request.CookieContainer.Add(authCk);
                var sr = new StreamReader(request.GetResponse().GetResponseStream());
                string result = sr.ReadToEnd();
                Message = result;
            }
            catch (Exception ex)
            {
                Message = linkEman + "\n" + ex.ToString();
            }

            var checkMess = Message.Replace(" ", "");
            if (!string.IsNullOrWhiteSpace(checkMess))
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    // Create a file to write to.   
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
        }
    }
}
