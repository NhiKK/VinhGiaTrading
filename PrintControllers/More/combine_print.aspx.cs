using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Excel;

public partial class PrintControllers_More_combine_print : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
    public string output = "";
    public HSSFWorkbook product = new HSSFWorkbook();
    protected void Page_Load(object sender, EventArgs e)
    {
        output = ExcuteSignalRStatic.mapPathSignalR(string.Format("~/FileUpload/Compine/CP-{0}.xls", Guid.NewGuid().ToString()));

        string arr = Request.QueryString["arr"];
        string pkId = Request.QueryString["pkId"];
        string dtId = Request.QueryString["dtId"];
        string hangcua = Request.QueryString["hangcua"];
        var arrFD = arr.Split(',');
        var context = System.Web.HttpContext.Current;
        var url = context.Request.Url;
        var haveErr = false;

        var dics = new List<Dictionary<string, string>>();
        for (var i = 0; i < arrFD.Length; i++)
        {
            string fd = arrFD[i];
            string linkConnect = string.Format(@"{0}?c_packinglist_id={1}&pklId={1}&dtId={2}&hangcua={3}&compineF=true", fd, pkId, dtId, hangcua);
            var data = new Dictionary<string, object>();

            var msgDT = Extension.GetModule(linkConnect, data);
            try
            {    
                var dicFile = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,string>>(msgDT);
                var link = dicFile["link"];
                var name = dicFile["name"];
                dics.Add(dicFile);
            }
            catch(Exception ex)
            {
                Response.Write(msgDT);
                haveErr = true;
            }
        }

        if (!haveErr)
        {
            MergeWorkbooks(dics);

            if (File.Exists(output))
            {
                foreach (var dic in dics)
                {
                    File.Delete(dic["link"]);
                }

                using (FileStream file = new FileStream(output, FileMode.Open, FileAccess.Read))
                {
                    var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);
                    file.Dispose();
                    File.Delete(output);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", string.Format("Files-{0}.xls", DateTime.Now.ToString("ddMMyy"))));
                    Response.Clear();
                    Response.BinaryWrite(memoryStream.ToArray());
                    Response.End();
                    memoryStream.Dispose();
                }
            }
        }
    }

    private void MergeWorkbooks(List<Dictionary<string, string>> dics)
    {
        var app = new Application();
        string destinationFilePath = output;
        app.DisplayAlerts = false; // No prompt when overriding

        // Create a new workbook (index=1) and open source workbooks (index=2,3,...)
        Workbook destinationWb = app.Workbooks.Add();

        foreach (var dic in dics)
        {
            app.Workbooks.Add(dic["link"]);
        }

        // Copy all worksheets
        Worksheet after = (Worksheet)destinationWb.Worksheets[1];
        for (int wbIndex = app.Workbooks.Count; wbIndex >= 2; wbIndex--)
        {
            Workbook wb = app.Workbooks[wbIndex];
            for (int wsIndex = wb.Worksheets.Count; wsIndex >= 1; wsIndex--)
            {
                Worksheet ws = (Worksheet)wb.Worksheets[wsIndex];
                ws.Copy(After: after);
            }
        }

        // Close source documents before saving destination. Otherwise, save will fail
        for (int wbIndex = 2; wbIndex <= app.Workbooks.Count; wbIndex++)
        {
            Workbook wb = app.Workbooks[wbIndex];
            wb.Close();
        }

        // Delete default worksheet
        after.Delete();

        // Save new workbook
        destinationWb.SaveAs(destinationFilePath);
        destinationWb.Close();

        app.Quit();

        try
        {
            if (null != app && System.Runtime.InteropServices.Marshal.IsComObject(app))
            {
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        catch { }
        finally
        {
            app = null;
        }
    }
}