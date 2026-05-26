using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for InfomationDB
/// </summary>
public class InfomationDB
{
    private string connectionName = "anco_test_importConnectionString";
    private string linqName = "LinqDB";
    private string linqUrl = "App_Code/LinQ/";
    public string ConnectionName
    {
        get { return connectionName; }
        set { connectionName = value; }
    }

    public string LinqName
    {
        get { return linqName; }
        set { linqName = value; }
    }
    public string LinqUrl
    {
        get { return linqUrl; }
        set { linqUrl = value; }
    }


    public string DataSource { get; set; }
    public string InitialCatalog { get; set; }
    public string UserID { get; set; }
    public string Password { get; set; }
    public string ConnectionString { get; set; }
    public InfomationDB()
    {
        if (string.IsNullOrEmpty(DataSource) |
                string.IsNullOrEmpty(InitialCatalog) |
                string.IsNullOrEmpty(UserID) |
                string.IsNullOrEmpty(Password)
            )
        {
            string filepath = ExcuteSignalRStatic.mapPathSignalR("~/Web.config");
            var xmlStr = File.ReadAllText(filepath);
            var str = XElement.Parse(xmlStr);
            var result = str.Elements("connectionStrings");
            foreach (var a in result)
            {
                var result2 = a.Elements("add");
                foreach (var b in result2)
                {
                    var name = b.Attribute("name").Value;
                    var connectionString = b.Attribute("connectionString").Value;

                    if (name == this.connectionName)
                    {
                        ConnectionString = connectionString;
                        string[] lstStr = connectionString.Split(';');
                        foreach (string c in lstStr)
                        {
                            if (c.Contains("Data Source="))
                            {
                                DataSource = c.Replace("Data Source=", "");
                            }
                            else if (c.Contains("Initial Catalog="))
                            {
                                InitialCatalog = c.Replace("Initial Catalog=", "");
                            }
                            else if (c.Contains("User ID="))
                            {
                                UserID = c.Replace("User ID=", "");
                            }
                            else if (c.Contains("Password="))
                            {
                                Password = c.Replace("Password=", "");
                            }
                        }
                    }
                }
            }
        }


    }
}