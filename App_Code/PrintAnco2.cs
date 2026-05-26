using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using System.Drawing;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public class PrintAnco2
{
    public static InfoPrint GetInfoPrint()
    {
        var context = HttpContext.Current;
        return new InfoPrint
        {
            logo = "",
            dd = DateTime.Now.ToString("dd"),
            MM = DateTime.Now.ToString("MM"),
            yyyy = DateTime.Now.ToString("yyyy"),
        };
    }

    public HSSFWorkbook hssfworkbook { get; set; }
    public int? columnLast { get; set; }
    public bool? isPDF { get; set; }
    public static double hesoColumn = 0.02967849293563;
    //public static double hesoColumn = 0.029;
    public PrintAnco2() { }
    public static string kiemTraDuLieuCoTonTaiKhiIn(HttpContext context, string sql, DevExpress.XtraReports.Web.ReportViewer viewer, bool showMsg)
    {
        var infoPrint = PrintAnco2.GetInfoPrint();
        string msg = "";
        var da = new SqlDataAdapter(sql, mdbc.GetConnection);
		da.SelectCommand.CommandTimeout = 60;
        var ds = new DataSet();
        da.Fill(ds);
        var tbl = ds.Tables[0];
        if (tbl.Rows.Count > 0)
        {
            tbl.Columns.Add("logo", Type.GetType("System.String"));
            tbl.Columns.Add("dd", Type.GetType("System.String"));
            tbl.Columns.Add("MM", Type.GetType("System.String"));
            tbl.Columns.Add("yyyy", Type.GetType("System.String"));

            for (var i = 0; i < tbl.Rows.Count; i++)
            {
                tbl.Rows[i]["logo"] = infoPrint.logo;
                tbl.Rows[i]["dd"] = infoPrint.dd;
                tbl.Rows[i]["MM"] = infoPrint.MM;
                tbl.Rows[i]["yyyy"] = infoPrint.yyyy;
            }

            viewer.Report.DataSource = ds;
            viewer.Report.DataAdapter = da;
            foreach(DataColumn column in tbl.Columns)
            {
                viewer.Report.DisplayName = viewer.Report.DisplayName.Replace("{" + column.ColumnName + "}", tbl.Rows[0][column.ColumnName].ToString());
            }
        }
        else
        {
            viewer.Report.DisplayName = System.Text.RegularExpressions.Regex.Replace(viewer.Report.DisplayName, @"{[^']*}", "");
            //viewer.SettingsLoadingPanel.Enabled = false;
            viewer.Visible = false;
            msg = "Không có dữ liệu";
        }

        if (msg.Length > 0)
            msg = string.Format(@"<center><h1>{0}</h1></center>", msg);

        if (showMsg & msg.Length > 0)
            context.Response.Write(msg);

        return msg;
    }

    public static string exportDataWithType(Task task, string sql, string type, string nameTemp, string rptDisplayName, DevExpress.XtraReports.Web.ReportViewer viewer, bool showMsg, bool compineF = false)
    {
        if (viewer.Report == null)
        {
            viewer.Report = new DevExpress.XtraReports.UI.XtraReport();
            viewer.Report.DisplayName = rptDisplayName;
            viewer.Report.DisplayName = System.Text.RegularExpressions.Regex.Replace(viewer.Report.DisplayName, @"{[^']*}", "");
            viewer.Visible = false;
        }

        var nameTempRoot = nameTemp;
        var context = HttpContext.Current;
        string msg = "";
        var export = new OfficeToPDF.ExportData();
        export.compineF = compineF;

        type = string.IsNullOrEmpty(type) ? "0" : type;
        if (type == "0")
        {
            string file = ExcuteSignalRStatic.mapPathSignalR("~/" + GetStore(false, nameTemp));
            if (File.Exists(file))
            {
                viewer.Report = DevExpress.XtraReports.UI.XtraReport.FromFile(file, true);
                viewer.Report.DisplayName = rptDisplayName;
                string m = kiemTraDuLieuCoTonTaiKhiIn(context, sql, viewer, true);
                if (m.Length <= 0)
                {
                    task.Start();
                    task.Wait();
                }
            }
            else
                msg = "false";
        }
        else if (type == "1")
        {
            string file = ExcuteSignalRStatic.mapPathSignalR("~/" + GetStore(false, nameTemp));
            if (File.Exists(file))
            {
                viewer.Report = DevExpress.XtraReports.UI.XtraReport.FromFile(file, true);
                viewer.Report.DisplayName = rptDisplayName;
                string m = kiemTraDuLieuCoTonTaiKhiIn(context, sql, viewer, true);
                if (m.Length <= 0)
                {
                    task.Start();
                    task.Wait();
                    Helper.viewFile(viewer);
                }
            }
            else
                msg = "false";
        }
        else if (type == "2")
        {
            nameTemp = nameTemp.Replace(".repx", ".xls");
            string file = ExcuteSignalRStatic.mapPathSignalR("~/" + GetStore(true, nameTemp));
            if (File.Exists(file))
            {
                string m = kiemTraDuLieuCoTonTaiKhiIn(context, sql, viewer, true);
                if (m.Length <= 0)
                {
                    task.Start();
                    task.Wait();
                    export.viewer = viewer;
                    export.DisplayName = rptDisplayName;
                    export.fileNameTemp = nameTemp;
                    export.sqlRun = sql;
                    export.dt = ((DataSet)viewer.Report.DataSource).Tables[0];
                    export.exec("excel");
                    rptDisplayName = export.DisplayName;
                }
            }
            else
                msg = "false";
        }
        else if (type == "3")
        {
            nameTemp = nameTemp.Replace(".repx", ".xls");
            string file = ExcuteSignalRStatic.mapPathSignalR("~/" + GetStore(true, nameTemp));
            if (File.Exists(file))
            {
                string m = kiemTraDuLieuCoTonTaiKhiIn(context, sql, viewer, true);
                if (m.Length <= 0)
                {
                    task.Start();
                    task.Wait();
                    export.viewer = viewer;
                    export.DisplayName = rptDisplayName;
                    export.fileNameTemp = nameTemp;
                    export.sqlRun = sql;
                    export.dt = ((DataSet)viewer.Report.DataSource).Tables[0];
                    export.exec("pdf");
                    rptDisplayName = export.DisplayName;
                }
            }
            else
                msg = "false";
        }

        if (viewer.Report == null)
        {
            viewer.Report = new DevExpress.XtraReports.UI.XtraReport();

            viewer.Report.DisplayName = rptDisplayName;
            viewer.Report.DisplayName = System.Text.RegularExpressions.Regex.Replace(viewer.Report.DisplayName, @"{[^']*}", "");
            viewer.Visible = false;
        }
        viewer.Report.DisplayName = viewer.Report.DisplayName.Trim();

        if (msg.Length > 0)
        {
            msg = string.Format(@"Cách in này không được sử dụng cho báo cáo hoặc mẫu in ""{0}"", hãy chọn một cách in khác", viewer.Report.DisplayName);
        }

        if (showMsg & msg.Length> 0)
            HttpContext.Current.Response.Write("<center><h1>"+ msg + "</h1></center>");

        return msg;
    }

    public SizeF DetectSizeText(string a, NPOI.SS.UserModel.IFont font)
    {
        var fontSylte = FontStyle.Regular;
        if (font.Boldweight > 400)
            fontSylte = FontStyle.Bold;
        else if (font.IsItalic)
            fontSylte = FontStyle.Italic;
        else if (font.IsStrikeout)
            fontSylte = FontStyle.Strikeout;
        else if (font.Underline != FontUnderlineType.None)
            fontSylte = FontStyle.Underline;

        var moreVal = isPDF.GetValueOrDefault(false) ? 0 : 0.5; 
        var fontS = new Font(font.FontName, (float)(font.FontHeightInPoints + moreVal), fontSylte);
        var fakeImage = new Bitmap(1, 1); //As we cannot use CreateGraphics() in a class library, so the fake image is used to load the Graphics.
        var graphics = Graphics.FromImage(fakeImage);
        var size = graphics.MeasureString(a, fontS);
        return size;
    }

    public float GetHeight(float height, int widthCol, string str, NPOI.SS.UserModel.IFont font)
    {
        var strArr = str.Split(new string[] { "\n", "\r" }, StringSplitOptions.None);
        float heightFinally = 0;
        foreach (var item in strArr)
        {
            double width = DetectSizeText(item, font).Width;
            double widthDiv = widthCol * hesoColumn;
            double aDec = width / (double)widthDiv;
            double aInt = int.Parse(aDec.ToString()[0].ToString());
            if (aDec - aInt > 0)
            {
                aInt = aInt + 1;
            }
            heightFinally += (float)(height * aInt);
        }
        return heightFinally;
    }

    public float GetHeight(float height, double widthCol, string str, NPOI.SS.UserModel.IFont font)
    {
        height = (float)16.8;
        var strArr = str.Split(new string[] { "\n", "\r" }, StringSplitOptions.None);
        float heightFinally = 0;
        foreach (var item in strArr)
        {
            double width = DetectSizeText(item, font).Width;
            double aDec = width / widthCol;
            double aInt = int.Parse(aDec.ToString()[0].ToString());
            if (aDec - aInt > 0)
            {
                aInt = aInt + 1;
            }
            heightFinally += (float)(height * aInt);
        }
        return heightFinally;
    }

    public float MeasureTextHeight(string text, int width, NPOI.SS.UserModel.IFont font)
    {
        double width2 = width * 0.003756;
        width2 = width2 * 7.5;
        if (string.IsNullOrEmpty(text)) return (float)0.0;
        var bitmap = new Bitmap(1, 1);
        var graphics = Graphics.FromImage(bitmap);

        var pixelWidth = Convert.ToInt32(width * 7.5);  //7.5 pixels per excel column width
        var drawingFont = new Font(font.FontName, (float)font.FontHeight);
        var size = graphics.MeasureString(text, drawingFont, (int)width2);

        //72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
        return (float)Math.Min(Convert.ToDouble(size.Height) * 72 / 96, 409) + 5;
    }

    public NPOI.HSSF.UserModel.HSSFSheet PrintExcel(NPOI.HSSF.UserModel.HSSFSheet s1, string type)
    {
        //s1.PrintSetup.PaperSize = (short)NPOI.SS.UserModel.PaperSize.A4;
        //s1.FitToPage = true;
        s1.PrintSetup.FitWidth = 1;
        s1.PrintSetup.FitHeight = 0;
        s1.SetMargin(NPOI.SS.UserModel.MarginType.TopMargin, 0);
        s1.SetMargin(NPOI.SS.UserModel.MarginType.BottomMargin, 0);
        s1.SetMargin(NPOI.SS.UserModel.MarginType.LeftMargin, 0.23);
        s1.SetMargin(NPOI.SS.UserModel.MarginType.RightMargin, 0.23);
        s1.SetMargin(NPOI.SS.UserModel.MarginType.HeaderMargin, 0);
        s1.SetMargin(NPOI.SS.UserModel.MarginType.FooterMargin, 0);

        hssfworkbook.SetPrintArea(
            hssfworkbook.GetSheetIndex(s1.SheetName), //sheet index
            0, //start column
            this.columnLast.GetValueOrDefault(0),
            0, //start row
            s1.LastRowNum
        );

        return s1;
    }

    public static string GetDecimal(string sothapphan, int type)
    {
        switch (sothapphan)
        {
            case "0":
                sothapphan = "#,#0";
                break;
            case "1":
                sothapphan = "#,#0.0";
                break;
            case "2":
                sothapphan = "#,#0.00";
                break;
            case "3":
                sothapphan = "#,#0.000";
                break;
            case "4":
                sothapphan = "#,#0.0000";
                break;
            case "5":
                sothapphan = "#,#0.####";
                break;
        }

        if (type == 1)
        {
            sothapphan = string.Format("{{0:{0}}}", sothapphan);
        }
        return sothapphan;
    }

    public static string Replace0ToHyphen(string sothapphan) 
    {
        sothapphan = sothapphan.Substring(1, sothapphan.Length - 2);
        //return "{0:#,#.####;-0;-}";
        return string.Format("{{{0};-0;-}}", sothapphan);
    }

    public static string Replace0ToHyphen2(string sothapphan)
    {
        return string.Format("{0};-0;-", sothapphan);
    }

    public static string GetStore(bool? excel, string fileName)
    {
        fileName = excel.GetValueOrDefault(false) ? fileName.Replace("[", "(").Replace("]", ")") : fileName;
        string msg = (excel.GetValueOrDefault(false) ? "ReportsStorageExcel/" : "ReportsStorage/");
        msg = string.IsNullOrEmpty(fileName) ? msg : (msg + fileName);

        return msg;
    }

    public static string GetStoreBackUp(bool? excel, string fileName)
    {
        string msg = "";
        if(excel.GetValueOrDefault(false))
            msg = GetStore(excel, fileName).Replace("/ReportsStorageExcel/", "/ReportsStorageExcelBackup/");
        else
            msg = GetStore(excel, fileName).Replace("/ReportsStorage/", "/ReportsStorageBackup/");

        return msg;
    }

    public class InfoExcel
    {
        public int? detail { get; set; }
        public int? startSumReport { get; set; }
        public int? startSumGroup { get; set; }
        public int? rptFooter { get; set; }
        public RptGroupHeader rptGroupHeader { get; set; }
        public int? rptGroupFooter { get; set; }
        public bool? sizePDF { get; set; }
        public string[] Alphabet = new string[] {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ"
        };
    }

    public class RptGroupHeader
    {
        public int? start { get; set; }
        public int? end { get; set; }
        public List<string> arrAva { get; set; } 
        public string key { get; set; }
        public int moreVal { get; set; }
        public List<IRowICellCopy> rows { get; set; }
    }

    public class IRowICellCopy
    {
        public IRow row { get; set; }
        public List<CellRangeAddress> mergeCells { get; set; }
        public List<ICell> cells { get; set; }
        public float? defaultHeight { get; set; }
    }
}
public class InfoPrint
{
    public string logo { get; set; }
    public string dd { get; set; }
    public string MM { get; set; }
    public string yyyy { get; set; }
}
