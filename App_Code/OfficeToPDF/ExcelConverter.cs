/**
 *  OfficeToPDF command line PDF conversion for Office 2007/2010/2013/2016
 *  Copyright (C) 2011-2018 Cognidox Ltd
 *  https://www.cognidox.com/opensource/
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 */

using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OfficeToPDF
{
    /// <summary>
    /// Handle conversion of Excel files
    /// </summary>
    /// 

    public class ExcelConverter : Converter
    {
        public List<AvariablePrj.lstImage> lstImage { get; set; }
        public List<AvariablePrj.lstText> lstText { get; set; }
        public List<AvariablePrj.lstTextReplace> lstTextReplace { get; set; }
        public List<AvariablePrj.lstFormula> lstFormula { get; set; }
        public List<int> lstAutoSizeColumn { get; set; }
        public int? endRow { get; set; }
        public int? endColumn { get; set; }
        public float? sizeAddPDF { get; set; }

        public ExcelConverter()
        {

        }

        // These are the properties we will extract from the first worksheet of any template document
        private static string[] templateProperties =
        {
            "BlackAndWhite", "BottomMargin", "CenterFooter", "CenterHeader",
            "CenterHorizontally", "CenterVertically", "DifferentFirstPageHeaderFooter",
            "Draft", "FirstPageNumber", "FitToPagesTall", "FitToPagesWide",
            "FooterMargin", "HeaderMargin", "LeftFooter", "LeftHeader",
            "LeftMargin", "OddAndEvenPagesHeaderFooter", "Order", "Orientation", "PaperSize", "PrintArea",
            "PrintGridlines", "PrintHeadings", "PrintNotes", "PrintTitleColumns", "PrintTitleRows",
            "RightFooter", "RightHeader", "RightMargin",
            "ScaleWithDocHeaderFooter", "TopMargin", "Zoom"
        };

        public void replaceTextRecursive(Microsoft.Office.Interop.Excel._Worksheet worksheet)
        {
            try
            {
                if (lstTextReplace.Count > 0)
                {
                    var textReplace = lstTextReplace.FirstOrDefault();
                    var oRange = worksheet.Cells.Find(What: textReplace.oldT, LookIn: XlFindLookIn.xlValues, SearchDirection: XlSearchDirection.xlNext, LookAt: XlLookAt.xlPart);
                    
                    if (oRange == null)
                    {
                        lstTextReplace.Remove(textReplace);
                    }
                    else
                    {
                        var text = oRange.Value2;
                        if (new string[] { "{#", "{!" }.Contains(textReplace.oldT.Substring(0, 2)))
                        {
                            oRange.Value2 = textReplace.newT;
                        }
                        else
                        {
                            try
                            {
                                int phStart = oRange.Value.ToString().LastIndexOf(textReplace.oldT);
                                int phEnd = textReplace.oldT.Length;

                                if (phStart > -1 & phEnd > -1)
                                {
                                    oRange.Characters[phStart + 1, phEnd].Insert(textReplace.newT);
                                }
                            }
                            catch
                            {
                                oRange.Value2 = textReplace.newT;
                            }
                        }
                    }

                    replaceTextRecursive(worksheet);
                }
            }
            catch {

            }
        }

        public string setSizePDF(String inputFile)
        {
            string msg = "";
            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbooks workbooks = null;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            try
            {
                object oMissing = System.Reflection.Missing.Value;
                app = new Microsoft.Office.Interop.Excel.Application();
                app.DisplayAlerts = false;
                app.AskToUpdateLinks = false;
                app.AlertBeforeOverwriting = false;
                app.EnableLargeOperationAlert = false;
                app.Interactive = false;
                app.FeatureInstall = Microsoft.Office.Core.MsoFeatureInstall.msoFeatureInstallNone;
                app.WindowState = XlWindowState.xlMinimized;
                app.Visible = false;
                workbooks = app.Workbooks;

                workbook = workbooks.Open(inputFile, oMissing, false, oMissing, oMissing, oMissing, true, oMissing, oMissing, oMissing, oMissing, oMissing, false, oMissing, oMissing);

                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

                for (var i = 1; i <= endRow + 1; i++)
                {
                    for (var j = 1; j <= endColumn + 1; j++)
                    {
                        var excelCellrange = (Range)worksheet.Cells[i, j];
                        var text = excelCellrange.Text.ToString();
                        var lengthText = text.Length;

                        for (var k = 0; k < lengthText; k++)
                        {
                            string size = excelCellrange.Characters[k, 1].Font.Size.ToString();
                            if (!string.IsNullOrEmpty(size))
                                excelCellrange.Characters[k, 1].Font.Size = float.Parse(size) + sizeAddPDF;
                        }
                    }
                }

                workbook.SaveAs(inputFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);
            }
            catch (COMException ce)
            {
                msg = "COMException: " + ce;
            }
            catch (Exception e)
            {
                msg = "Exception: " + e;
            }
            finally
            {
                if (workbook != null)
                {
                    ReleaseCOMObject(worksheet);
                    ReleaseCOMObject(app.ActiveWindow);
                    ReleaseCOMObject(workbook.Windows);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    // Excel sometimes needs a bit of a delay before we close in order to
                    // let things get cleaned up
                    workbook.Saved = true;
                    CloseExcelWorkbook(workbook);
                }

                if (workbooks != null)
                {
                    workbooks.Close();
                }

                if (app != null)
                {
                    app.Quit();
                }

                // Clean all the COM leftovers
                ReleaseCOMObject(workbook);
                ReleaseCOMObject(workbooks);
                ReleaseCOMObject(app);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return msg;
        }

        public string ConvertInterop(String inputFile, String outputFile, Hashtable options)
        {
            if (lstImage == null)
                lstImage = new List<AvariablePrj.lstImage>();

            if (lstText == null)
                lstText = new List<AvariablePrj.lstText>();

            if (lstTextReplace == null)
                lstTextReplace = new List<AvariablePrj.lstTextReplace>();

            if (lstFormula == null)
                lstFormula = new List<AvariablePrj.lstFormula>();

            if (lstAutoSizeColumn == null)
                lstAutoSizeColumn = new List<int>();

            var mimeTypeOutput = outputFile.Substring(outputFile.LastIndexOf(".") + 1);
            var msg = "";
            var source = new CancellationTokenSource();
            var token = source.Token;

            var ps = System.Management.Automation.PowerShell.Create();

            var pathScript = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/SetDefaultPrinter.ps1");

            var settings = new System.Drawing.Printing.PrinterSettings();

            if (settings.PrinterName.ToLower() != "Microsoft XPS Document Writer".ToLower())
            {
                var process = new Process();
                var startInfo = new ProcessStartInfo("powershell.exe", "-File " + pathScript + " -PrinterName \"Microsoft XPS Document Writer\"");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.UseShellExecute = false;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }

            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbooks workbooks = null;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;

            var task = new System.Threading.Tasks.Task(() =>
            {
                options = PDFBookmark.createOptions();
                Boolean running = false;    
                System.Object activeSheet = null;
                Microsoft.Office.Interop.Excel.Window activeWindow = null;
                Microsoft.Office.Interop.Excel.Windows wbWin = null;
                Hashtable templatePageSetup = new Hashtable();

                String tmpFile = null;
                object oMissing = System.Reflection.Missing.Value;
                Boolean nowrite = false;
                try
                {
                    // Excel can be very slow to start up, so try to get the COM
                    // object a few times
                    int tries = 10;
                    app = new Microsoft.Office.Interop.Excel.Application();
                    while (tries > 0)
                    {
                        try
                        {
                            // Try to set a property on the object
                            app.ScreenUpdating = false;
                        }
                        catch (COMException)
                        {
                            // Decrement the number of tries and have a bit of a snooze
                            tries--;
                            Thread.Sleep(500);
                            continue;
                        }
                        // Looks ok, so bail out of the loop
                        break;
                    }
                    if (tries == 0)
                    {
                        ReleaseCOMObject(app);
                        msg = Enum.GetName(typeof(ExitCode), ExitCode.ApplicationError);
                        throw new ArgumentException(msg);
                    }

                    //app.Visible = false;
                    app.DisplayAlerts = false;
                    app.AskToUpdateLinks = false;
                    app.AlertBeforeOverwriting = false;
                    app.EnableLargeOperationAlert = false;
                    //app.DisplayDocumentActionTaskPane = false;
                    app.Interactive = false;
                    //app.ScreenUpdating = false;
                    //app.CommandBars.ExecuteMso("HideRibbon");
                    app.FeatureInstall = Microsoft.Office.Core.MsoFeatureInstall.msoFeatureInstallNone;

                    var onlyActiveSheet = false;
                    Boolean activeSheetOnMaxRows = false;
                    Boolean includeProps = !false;
                    Boolean skipRecalculation = false;
                    Boolean showHeadings = false;
                    Boolean showFormulas = false;
                    Boolean isHidden = false;
                    Boolean screenQuality = false;
                    Boolean updateLinks = !false;
                    var passWord = "";
                    var writepassword = "";
                    var fallback_printer = "";
                    var printerO = "";
                    int maxRows = 0;
                    int worksheetNum = 0;
                    int sheetForConversionIdx = 0;
                    activeWindow = app.ActiveWindow;
                    Sheets allSheets = null;
                    XlFileFormat fmt = XlFileFormat.xlOpenXMLWorkbook;
                    XlFixedFormatQuality quality = XlFixedFormatQuality.xlQualityStandard;
                    if (isHidden)
                    {
                        // Try and at least minimise it
                        app.WindowState = XlWindowState.xlMinimized;
                        app.Visible = false;
                    }

                    String readPassword = "";
                    if (!String.IsNullOrEmpty(passWord))
                    {
                        readPassword = passWord;
                    }
                    Object oReadPass = (Object)readPassword;

                    String writePassword = "";
                    if (!String.IsNullOrEmpty(writepassword))
                    {
                        writePassword = writepassword;
                    }
                    Object oWritePass = (Object)writePassword;

                    // Check for password protection and no password
                    if (Converter.IsPasswordProtected(inputFile) && String.IsNullOrEmpty(readPassword))
                    {
                        Console.WriteLine("Unable to open password protected file");
                        msg = Enum.GetName(typeof(ExitCode), ExitCode.PasswordFailure);
                        throw new ArgumentException(msg);
                    }

                    app.EnableEvents = false;
                    workbooks = app.Workbooks;
                    // If we have no write password and we're attempting to open for writing, we might be
                    // caught out by an unexpected write password
                    if (writePassword == "" && !nowrite)
                    {
                        oWritePass = (Object)"FAKEPASSWORD";
                        try
                        {
                            workbook = workbooks.Open(inputFile, updateLinks, nowrite, oMissing, oReadPass, oWritePass, true, oMissing, oMissing, oMissing, oMissing, oMissing, false, oMissing, oMissing);
                        }
                        catch (System.Runtime.InteropServices.COMException)
                        {
                            // Attempt to open it in read-only mode
                            workbook = workbooks.Open(inputFile, updateLinks, true, oMissing, oReadPass, oWritePass, true, oMissing, oMissing, oMissing, oMissing, oMissing, false, oMissing, oMissing);
                        }
                    }
                    else
                    {
                        workbook = workbooks.Open(inputFile, updateLinks, nowrite, oMissing, oReadPass, oWritePass, true, oMissing, oMissing, oMissing, oMissing, oMissing, false, oMissing, oMissing);
                    }
                    
                    // Add in a delay to let Excel sort itself out
                    AddCOMDelay(options);

                    // Unable to open workbook
                    if (workbook == null)
                    {
                        msg = Enum.GetName(typeof(ExitCode), ExitCode.FileOpenFailure);
                        throw new ArgumentException(msg);
                    }

                    if (app.EnableEvents)
                    {
                        workbook.RunAutoMacros(XlRunAutoMacro.xlAutoOpen);
                    }

                    // Get any template options
                    SetPageOptionsFromTemplate(app, workbooks, options, ref templatePageSetup);

                    // Get the sheets
                    allSheets = workbook.Sheets;
                    var worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
                    worksheet.PageSetup.CenterHorizontally = true;
                    //worksheet.PageSetup.FitToPagesTall = 1;
                    //worksheet.PageSetup.FitToPagesWide = 1;

                    if (mimeTypeOutput == "pdf")
                    {
                        //var excelCellrange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[endRow, endColumn]];
                        //excelCellrange.Font.Size = 13.5;
                    }

                    replaceTextRecursive(worksheet);

                    foreach (var formula in lstFormula)
                    {
                        try
                        {
                            var RangeFm = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[formula.row, formula.col];
                            if (!string.IsNullOrEmpty(formula.formula))
                                RangeFm.Formula = string.Format("=" + formula.formula);
                            else
                            {
                                RangeFm.Formula = string.Format(RangeFm.Formula.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            string msg1 = ex.Message;
                        }
                    }

                    foreach (var image in lstImage)
                    {
						try 
						{
							var oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[image.row + 1, image.column + 1];
							var pictureR = System.Drawing.Image.FromFile(image.link);
							float width = (float)((double)oRange.Width);
							float height = (float)((double)oRange.RowHeight);

							for (var iRan = image.column + 1; iRan <= image.columnLast; iRan++)
							{
								var oRangeBS = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[image.row + 1, iRan + 1];
								width += (float)((double)oRangeBS.Width);
							}

							for (var iRan = image.row + 1; iRan <= image.rowLast; iRan++)
							{
								var oRangeBS = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[iRan + 1, image.column + 1];
								height += (float)((double)oRangeBS.RowHeight);
							}

							float widthR = pictureR.Width;
							float heightR = pictureR.Height;
							float percent = height / heightR;

							var widthImg = (int)(widthR * percent);
							var heightImg = (int)(heightR * percent - 3.5);
							widthImg = widthImg > width ? (int)(width - 1) : widthImg;
							var rateColumnAndPicture = (width - widthImg) / 2;
							rateColumnAndPicture = rateColumnAndPicture <= 0 ? 0 : rateColumnAndPicture;

							var rateRowAndPicture = (height - heightImg) / 2;
							rateRowAndPicture = rateRowAndPicture <= 0 ? 2 : rateRowAndPicture;

							var Left = (float)((double)oRange.Left + rateColumnAndPicture);
							var Top = (float)((double)oRange.Top + rateRowAndPicture);

                            if (image.keepSize.GetValueOrDefault(false))
                            {
                                Left = Left - rateColumnAndPicture;
                                if (image.leftIMG != null)
                                    Left = Left + image.leftIMG.GetValueOrDefault(0);
                                Top = Top - rateRowAndPicture;
                                if (image.topIMG != null)
                                    Top = Top + image.topIMG.GetValueOrDefault(0);
                                if(image.scale != null)
                                {
                                    widthR = widthR * image.scale.GetValueOrDefault(0) / 100;
                                    heightR = heightR * image.scale.GetValueOrDefault(0) / 100;
                                }
                                var pic1 = worksheet.Shapes.AddPicture(image.link, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, widthR, heightR);
                            }
                            else
                            {
                                var pic2 = worksheet.Shapes.AddPicture(image.link, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, widthImg, heightImg);
                            }
							pictureR.Dispose();
						}
						catch {}
                    }

                    float widthBackUp = -1;
                    foreach (var textInLine in lstText)
                    {
                        float leftTextPrev = -1, topTextPrev = 0, widthTextPrev = -1, heightTextPrev = 0;
                        if (textInLine.columnFPrev != null & textInLine.columnTPrev != null)
                        {
                            string textRangePrev = "";
                            var hasBoldPrev = false;
                            double fontSizePrev = 12;
                            string fontNamePrev = "Arial";
                            for (var iCl = textInLine.columnFPrev; iCl <= textInLine.columnTPrev; iCl++)
                            {
                                var oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[textInLine.row + 1, iCl + 1];
                                widthTextPrev += (float)((double)oRange.Width);

                                if (iCl == textInLine.columnFPrev)
                                {
                                    leftTextPrev = (float)((double)oRange.Left);
                                    topTextPrev = (float)((double)oRange.Top - textInLine.top);
                                    heightTextPrev = (float)((double)oRange.Height);
                                    textRangePrev = (string)oRange.Value2;
                                    oRange.Value2 = "";
                                    hasBoldPrev = (bool)oRange.Font.Bold;
                                    fontSizePrev = (double)oRange.Font.Size;
                                    fontNamePrev = (string)oRange.Font.Name;
                                }
                            }

                            var textBoxPrev = worksheet.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, leftTextPrev, topTextPrev, widthTextPrev, heightTextPrev);
                            textBoxPrev.TextFrame.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            textBoxPrev.TextFrame.MarginLeft = 3;
                            textBoxPrev.TextFrame.MarginRight = 2;
                            if (string.IsNullOrEmpty(textRangePrev))
                                textBoxPrev.Width = widthBackUp;
                            else
                                textBoxPrev.TextFrame.AutoSize = true;
                            textBoxPrev.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                            textBoxPrev.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                            var stringTextBoxPrev = textBoxPrev.TextFrame.Characters(Type.Missing, Type.Missing);
                            stringTextBoxPrev.Text = textRangePrev;
                            stringTextBoxPrev.Font.Name = fontNamePrev;
                            stringTextBoxPrev.Font.Bold = hasBoldPrev;
                            stringTextBoxPrev.Font.Size = fontSizePrev;

                            if (textBoxPrev.Width < textInLine.minWidth)
                                textBoxPrev.Width = (float)textInLine.minWidth;

                            leftTextPrev = textBoxPrev.Left;
                            widthTextPrev = textBoxPrev.Width;
                            widthBackUp = widthTextPrev;
                        }

                        float leftText = 0, topText = 0, widthText = 0, heightText = 0;
                        string textRange = "";
                        var hasBold = false;
                        double fontSize = 12;
                        string fontName = "Arial";
                        for (var iCl = textInLine.columnF; iCl <= textInLine.columnT; iCl++)
                        {
                            var oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[textInLine.row + 1, iCl + 1];
                            widthText += (float)((double)oRange.Width);

                            if (iCl == textInLine.columnF)
                            {
                                leftText = (float)((double)oRange.Left - textInLine.left);
                                topText = (float)((double)oRange.Top - textInLine.top);
                                heightText = (float)((double)oRange.RowHeight);
                                textRange = (string)oRange.Text;
                                oRange.Value2 = "";
                                hasBold = (bool)oRange.Font.Bold;
                                fontSize = (double)oRange.Font.Size;
                                fontName = (string)oRange.Font.Name;
                            }
                        }

                        leftText = leftTextPrev <= -1 ? leftText : leftTextPrev + widthTextPrev;
                        var textBox = worksheet.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, leftText, topText, widthText, heightText);
                        textBox.TextFrame.MarginLeft = 2;
                        textBox.TextFrame.MarginRight = 2;
                        textBox.TextFrame.MarginTop = 0;
                        textBox.TextFrame.MarginBottom = 0;
                        textBox.Height = heightText;
                        textBox.TextFrame.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        textBox.TextFrame2.WordWrap = Microsoft.Office.Core.MsoTriState.msoCTrue;
                        textBox.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        textBox.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        var stringTextBox = textBox.TextFrame.Characters(Type.Missing, Type.Missing);
                        stringTextBox.Text = textRange;
                        stringTextBox.Font.Name = fontName;
                        stringTextBox.Font.Bold = hasBold;
                        stringTextBox.Font.Size = fontSize;
                    }

                    

                    //foreach (var textReplace in lstTextReplace)
                    //{
                    //    try
                    //    {
                    //        var oRange = worksheet.Cells.Find(What: textReplace.oldT, LookIn: XlFindLookIn.xlValues, SearchDirection: XlSearchDirection.xlNext, LookAt: XlLookAt.xlPart);

                    //        if (textReplace.oldT.Substring(0, 2) == "{#")
                    //        {
                    //            oRange.Value2 = textReplace.newT;
                    //        }
                    //        else
                    //        {
                    //            int phStart = oRange.Value.ToString().LastIndexOf(textReplace.oldT);
                    //            int phEnd = textReplace.oldT.Length;

                    //            if (phStart > -1 & phEnd > -1)
                    //            {
                    //                oRange.Characters[phStart + 1, phEnd].Insert(textReplace.newT);
                    //            }
                    //        }
                    //    }
                    //    catch { }
                    //}

                    foreach (var col in lstAutoSizeColumn)
                    {
                        try
                        {
                            var RangeFm = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, col + 1];
                            RangeFm.EntireColumn.AutoFit();
                        }
                        catch (Exception ex)
                        {
                            string msg1 = ex.Message;
                        }
                    }

                    // Try and avoid xls files raising a dialog
                    var temporaryStorageDir = Path.GetTempFileName();
                    File.Delete(temporaryStorageDir);
                    Directory.CreateDirectory(temporaryStorageDir);
                    // We will save as xlsb (binary format) since this doesn't raise some errors when processing
                    tmpFile = Path.Combine(temporaryStorageDir, Path.GetFileNameWithoutExtension(inputFile) + ".xlsb");
                    fmt = XlFileFormat.xlExcel12;

                    // Set up the print quality
                    if (screenQuality)
                    {
                        quality = XlFixedFormatQuality.xlQualityMinimum;
                    }

                    // If a worksheet has been specified, try and use just the one
                    if (worksheetNum > 0)
                    {
                        // Force us just to use the active sheet
                        onlyActiveSheet = true;
                        try
                        {
                            if (worksheetNum > allSheets.Count)
                            {
                                // Sheet count is too big
                                msg = Enum.GetName(typeof(ExitCode), ExitCode.WorksheetNotFound);
                                throw new ArgumentException(msg);
                            }
                            if (allSheets[worksheetNum] is _Worksheet)
                            {
                                ((_Worksheet)allSheets[worksheetNum]).Activate();
                                sheetForConversionIdx = ((_Worksheet)allSheets[worksheetNum]).Index;
                            }
                            else if (allSheets[worksheetNum] is _Chart)
                            {
                                ((_Chart)allSheets[worksheetNum]).Activate();
                                sheetForConversionIdx = ((_Chart)allSheets[worksheetNum]).Index;
                            }

                        }
                        catch (Exception)
                        {
                            msg = Enum.GetName(typeof(ExitCode), ExitCode.WorksheetNotFound);
                            throw new ArgumentException(msg);
                        }
                    }

                    if (showFormulas)
                    {
                        // Determine whether to show formulas
                        try
                        {
                            activeWindow.DisplayFormulas = true;
                        }
                        catch (Exception) { }
                    }

                    // Keep the windows hidden
                    if (isHidden)
                    {
                        wbWin = workbook.Windows;
                        if (null != wbWin)
                        {
                            if (wbWin.Count > 0)
                            {
                                //wbWin[1].Visible = false;
                            }
                        }
                        if (null != activeWindow)
                        {
                            activeWindow.Visible = false;
                        }
                    }

                    // Keep track of the active sheet
                    int activeSheetIdx = 1;
                    if (workbook.ActiveSheet != null)
                    {
                        activeSheet = workbook.ActiveSheet;
                        if (activeSheet is _Worksheet)
                        {
                            activeSheetIdx = ((Worksheet)activeSheet).Index;
                        }
                        else if (activeSheet is _Chart)
                        {
                            activeSheetIdx = ((Microsoft.Office.Interop.Excel.Chart)activeSheet).Index;
                        }
                    }

                    // Large excel files may simply not print reliably - if the excel_max_rows
                    // configuration option is set, then we must close up and forget about 
                    // converting the file. However, if a print area is set in one of the worksheets
                    // in the document, then assume the author knew what they were doing and
                    // use the print area.

                    // We may need to loop through all the worksheets in the document
                    // depending on the options given. If there are maximum row restrictions
                    // or formulas are being shown, then we need to loop through all the
                    // worksheets
                    if (maxRows > 0 || showFormulas || showHeadings)
                    {
                        var row_count_check_ok = true;
                        var found_rows = 0;
                        var found_worksheet = "";
                        bool[] rowCountOK = new bool[allSheets.Count + 1];

                        // Loop through all the sheets (worksheets and charts)
                        for (int wsIdx = 1; wsIdx <= allSheets.Count; wsIdx++)
                        {
                            var ws = allSheets.Item[wsIdx];
                            rowCountOK[wsIdx] = true;

                            // Skip anything that is not the active sheet
                            if (onlyActiveSheet)
                            {
                                // Have to be careful to treat _Worksheet and _Chart items differently
                                try
                                {
                                    // Get the index of the active sheet
                                    if (wsIdx != activeSheetIdx)
                                    {
                                        // If we are not the active sheet, then skip to the next
                                        ReleaseCOMObject(ws);
                                        continue;
                                    }
                                }
                                catch (Exception)
                                {
                                    if (ws != null)
                                    {
                                        ReleaseCOMObject(ws);
                                    }
                                    continue;
                                }
                                sheetForConversionIdx = wsIdx;
                            }

                            if (showHeadings && ws is _Worksheet)
                            {
                                PageSetup pageSetup = null;
                                try
                                {
                                    pageSetup = ((Worksheet)ws).PageSetup;
                                    pageSetup.PrintHeadings = true;

                                }
                                catch (Exception) { }
                                finally
                                {
                                    ReleaseCOMObject(pageSetup);
                                }
                            }

                            // If showing formulas, make things auto-fit
                            if (showFormulas && ws is _Worksheet)
                            {
                                Range cols = null;
                                try
                                {
                                    ((_Worksheet)ws).Activate();
                                    activeWindow.DisplayFormulas = true;
                                    cols = ((Worksheet)ws).Columns;
                                    cols.AutoFit();
                                }
                                catch (Exception) { }
                                finally
                                {
                                    ReleaseCOMObject(cols);
                                }
                            }

                            // If there is a maximum row count, make sure we check each worksheet
                            if (maxRows > 0 && ws is _Worksheet)
                            {
                                // Check for a print area
                                var pageSetup = ((Worksheet)ws).PageSetup;
                                var printArea = pageSetup.PrintArea;
                                ReleaseCOMObject(pageSetup);
                                if (string.IsNullOrEmpty(printArea))
                                {
                                    // There is no print area, check that the row count is <= to the
                                    // excel_max_rows value. Note that we can't just take the range last
                                    // row, as this may return a huge value, rather find the last non-blank
                                    // row.
                                    var row_count = 0;
                                    var range = ((Worksheet)ws).UsedRange;
                                    if (range != null)
                                    {
                                        var rows = range.Rows;
                                        if (rows != null && rows.Count > maxRows)
                                        {
                                            var cells = range.Cells;
                                            if (cells != null)
                                            {
                                                var cellSearch = cells.Find("*", oMissing, oMissing, oMissing, oMissing, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious, false, oMissing, oMissing);
                                                // Make sure we actually get some results, since the worksheet may be totally blank
                                                if (cellSearch != null)
                                                {
                                                    row_count = cellSearch.Row;
                                                    found_worksheet = ((Worksheet)ws).Name;
                                                }
                                                ReleaseCOMObject(cellSearch);
                                            }
                                            ReleaseCOMObject(cells);
                                        }
                                        ReleaseCOMObject(rows);
                                    }
                                    ReleaseCOMObject(range);

                                    if (row_count > maxRows)
                                    {
                                        // Too many rows on this worksheet - mark the workbook as unprintable
                                        row_count_check_ok = false;
                                        rowCountOK[wsIdx] = false;
                                        found_rows = row_count;
                                        Converter.ReleaseCOMObject(ws);
                                        if (activeSheetOnMaxRows)
                                        {
                                            // Keep checking
                                            continue;
                                        }
                                        break;
                                    }
                                }
                            } // End of row check
                            Converter.ReleaseCOMObject(ws);
                        }

                        // Make sure we are not converting a document with too many rows
                        if (row_count_check_ok == false)
                        {
                            // We may want to try and convert the active sheet if it has not been included in the
                            // sheets with too many rows
                            bool bailOut = true;
                            if (activeSheetOnMaxRows && !onlyActiveSheet)
                            {
                                if (rowCountOK[activeSheetIdx])
                                {
                                    bailOut = false;
                                    sheetForConversionIdx = activeSheetIdx;
                                    onlyActiveSheet = true;
                                }
                            }
                            if (bailOut)
                            {
                                throw new Exception(String.Format("Too many rows to process ({0}) on worksheet {1}", found_rows, found_worksheet));
                            }
                        }
                    }

                    // Allow for re-calculation to be skipped
                    if (skipRecalculation)
                    {
                        app.Calculation = XlCalculation.xlCalculationManual;
                        app.CalculateBeforeSave = false;
                    }
                    else
                        app.Calculation = XlCalculation.xlCalculationAutomatic;

                    if (mimeTypeOutput == "pdf")
                    {
                        workbook.SaveAs(tmpFile, fmt, Type.Missing, Type.Missing, Type.Missing, false, XlSaveAsAccessMode.xlNoChange, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing);

                        if (onlyActiveSheet)
                        {
                            // Set up a delegate function for times we want to print
                            PrintDocument printFunc = delegate (string destination, string printer)
                            {
                                ((Worksheet)activeSheet).PrintOut(ActivePrinter: printer, PrintToFile: true, PrToFileName: destination);
                            };

                            if (sheetForConversionIdx > 0)
                            {
                                activeSheet = allSheets.Item[sheetForConversionIdx];
                            }
                            if (activeSheet is _Worksheet)
                            {
                                var wps = ((_Worksheet)activeSheet).PageSetup;
                                SetPageSetupProperties(templatePageSetup, wps);
                                if (String.IsNullOrEmpty(printerO))
                                {
                                    try
                                    {
                                        ((Worksheet)activeSheet).ExportAsFixedFormat(XlFixedFormatType.xlTypePDF,
                                        outputFile, quality, includeProps, false, Type.Missing, Type.Missing, false, Type.Missing);
                                    }
                                    catch (Exception)
                                    {
                                        if (!String.IsNullOrEmpty(fallback_printer))
                                        {
                                            PrintToGhostscript(fallback_printer, outputFile, printFunc);
                                        }
                                        else
                                        {
                                            throw;
                                        }
                                    }
                                }
                                else
                                {
                                    PrintToGhostscript(printerO, outputFile, printFunc);
                                }
                                ReleaseCOMObject(wps);
                            }
                            else if (activeSheet is _Chart)
                            {
                                var wps = ((_Chart)activeSheet).PageSetup;
                                SetPageSetupProperties(templatePageSetup, wps);
                                ((Microsoft.Office.Interop.Excel.Chart)activeSheet).ExportAsFixedFormat(XlFixedFormatType.xlTypePDF,
                                    outputFile, quality, includeProps, false, Type.Missing, Type.Missing, false, Type.Missing);
                                ReleaseCOMObject(wps);
                            }
                            else
                            {
                                msg = Enum.GetName(typeof(ExitCode), ExitCode.UnknownError);
                                throw new ArgumentException(msg);
                            }
                            AddCOMDelay(options);
                        }
                        else
                        {
                            //app.DisplayDocumentActionTaskPane = false;
                            //app.EnableEvents = false;
                            PrintDocument printFunc = delegate (string destination, string printer)
                            {
                                workbook.PrintOutEx(ActivePrinter: printer, PrintToFile: true, PrToFileName: destination);
                            };
                            if (HasTemplateOption(options))
                            {
                                // Set up the template page setup options on all the worksheets
                                // in the workbook
                                var worksheets = workbook.Worksheets;
                                for (int wsIdx = 1; wsIdx <= worksheets.Count; wsIdx++)
                                {
                                    var ws = worksheets[wsIdx];
                                    var wps = (ws is _Worksheet) ? ((_Worksheet)ws).PageSetup : ((_Chart)ws).PageSetup;
                                    SetPageSetupProperties(templatePageSetup, wps);
                                    ReleaseCOMObject(wps);
                                    ReleaseCOMObject(ws);
                                }
                                ReleaseCOMObject(worksheets);
                            }
                            if (String.IsNullOrEmpty(printerO))
                            {
                                try
                                {
                                    workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF,
                                    outputFile, quality, includeProps, false, Type.Missing, Type.Missing, false, Type.Missing);
                                }
                                catch (Exception)
                                {
                                    if (!String.IsNullOrEmpty(fallback_printer))
                                    {
                                        PrintToGhostscript(fallback_printer, outputFile, printFunc);
                                    }
                                    else
                                    {
                                        throw;
                                    }
                                }
                            }
                            //else
                            //{
                            //    PrintToGhostscript((string)options["printer"], outputFile, printFunc);
                            //}
                        }

                        ReleaseCOMObject(allSheets);
                        ReleaseCOMObject(fmt);
                        ReleaseCOMObject(quality);
                    }
                    else
                    {
                        //app.Calculation = XlCalculation.xlCalculationAutomatic;
                        //workbook.Application.Calculation = XlCalculation.xlCalculationAutomatic;
                        workbook.SaveAs(outputFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);
                    }
                }
                catch (COMException ce)
                {
                    msg = "COMException: " + ce;
                }
                catch (Exception e)
                {
                    msg = "Exception: " + e;
                }
                finally
                {
                    if (workbook != null)
                    {
                        ReleaseCOMObject(activeSheet);
                        ReleaseCOMObject(activeWindow);
                        ReleaseCOMObject(wbWin);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        // Excel sometimes needs a bit of a delay before we close in order to
                        // let things get cleaned up
                        workbook.Saved = true;
                        CloseExcelWorkbook(workbook);
                    }

                    if (!running)
                    {
                        if (workbooks != null)
                        {
                            workbooks.Close();
                        }

                        if (app != null)
                        {
                            ((Microsoft.Office.Interop.Excel._Application)app).Quit();
                        }
                    }

                    // Clean all the COM leftovers
                    ReleaseCOMObject(workbook);
                    ReleaseCOMObject(workbooks);
                    ReleaseCOMObject(app);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    if (tmpFile != null && File.Exists(tmpFile))
                    {
                        System.IO.File.Delete(tmpFile);
                        // Remove the temporary path to the temp file
                        try
                        {
                            Directory.Delete(Path.GetDirectoryName(tmpFile));
                        }
                        catch { }
                    }

                    while (!File.Exists(outputFile))
                    {
                        Thread.Sleep(1000);
                    }
                }
            }, token);

            var timeFist = DateTime.Now;
            var timeWait = 120000;
            int seconds = timeWait / 1000;
            task.Start();
            task.Wait(timeWait);

            try
            {
                task.Dispose();
            }
            catch { }

            if ((DateTime.Now - timeFist).TotalSeconds >= seconds)
            {
                msg = string.Format(@"Timeout, limit to print is {0} seconds.", seconds);
                try
                {
                    if (workbooks != null)
                    {
                        workbooks.Close();
                    }
                }
                catch { }

                try
                {
                    if (app != null)
                    {
                        ((Microsoft.Office.Interop.Excel._Application)app).Quit();
                    }
                }
                catch { }

                ReleaseCOMObject(workbook);
                ReleaseCOMObject(workbooks);
                ReleaseCOMObject(app);
            }

            return msg;
        }

        // Main conversion routine
        public static string Convert(String inputFile, String outputFile, Hashtable options, List<AvariablePrj.lstImage> lstImage, List<AvariablePrj.lstText> lstText)
        {
            var msg = "";
            var source = new CancellationTokenSource();
            var token = source.Token;

            var settings = new System.Drawing.Printing.PrinterSettings();

            if (settings.PrinterName.ToLower() != "Microsoft XPS Document Writer".ToLower())
            {
                //var pathScript = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/SetDefaultPrinter.ps1");
                //var process = new Process();
                //var startInfo = new ProcessStartInfo("powershell.exe", "-File " + pathScript + " -PrinterName \"Microsoft XPS Document Writer\"");
                //startInfo.WindowStyle = ProcessWindowStyle.Normal;
                //startInfo.UseShellExecute = false;
                //process.StartInfo = startInfo;
                //process.Start();
                //process.WaitForExit();

                var process = new Process();
                var startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.UseShellExecute = false;
                startInfo.FileName = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/AutoSetDefaultPrinter.bat");
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }

            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbooks workbooks = null;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            System.Object activeSheet = null;
            Microsoft.Office.Interop.Excel.Window activeWindow = null;
            Microsoft.Office.Interop.Excel.Windows wbWin = null;

            var task = new System.Threading.Tasks.Task(() =>
            {
                options = PDFBookmark.createOptions();
                Boolean running = false;
               
                Hashtable templatePageSetup = new Hashtable();

                String tmpFile = null;
                object oMissing = System.Reflection.Missing.Value;
                Boolean nowrite = true;
                try
                {
                    // Excel can be very slow to start up, so try to get the COM
                    // object a few times
                    int tries = 10;
                    app = new Microsoft.Office.Interop.Excel.Application();

                    while (tries > 0)
                    {
                        try
                        {
                            // Try to set a property on the object
                            app.ScreenUpdating = false;
                        }
                        catch (COMException)
                        {
                            // Decrement the number of tries and have a bit of a snooze
                            tries--;
                            Thread.Sleep(500);
                            continue;
                        }
                        // Looks ok, so bail out of the loop
                        break;
                    }
                    if (tries == 0)
                    {
                        ReleaseCOMObject(app);
                        msg = Enum.GetName(typeof(ExitCode), ExitCode.ApplicationError);
                        throw new ArgumentException(msg);
                    }

                    //app.Visible = false;
                    app.DisplayAlerts = false;
                    app.AskToUpdateLinks = false;
                    app.AlertBeforeOverwriting = false;
                    app.EnableLargeOperationAlert = false;
                    //app.DisplayDocumentActionTaskPane = false;
                    app.Interactive = false;
                    //app.ScreenUpdating = false;
                    //app.CommandBars.ExecuteMso("HideRibbon");
                    app.FeatureInstall = Microsoft.Office.Core.MsoFeatureInstall.msoFeatureInstallNone;

                    var onlyActiveSheet = false;
                    Boolean activeSheetOnMaxRows = false;
                    Boolean includeProps = !false;
                    Boolean skipRecalculation = false;
                    Boolean showHeadings = false;
                    Boolean showFormulas = false;
                    Boolean isHidden = false;
                    Boolean screenQuality = false;
                    Boolean updateLinks = !false;
                    var passWord = "";
                    var writepassword = "";
                    var fallback_printer = "";
                    var printerO = "";
                    int maxRows = 0;
                    int worksheetNum = 0;
                    int sheetForConversionIdx = 0;
                    activeWindow = app.ActiveWindow;
                    Sheets allSheets = null;
                    XlFileFormat fmt = XlFileFormat.xlOpenXMLWorkbook;
                    XlFixedFormatQuality quality = XlFixedFormatQuality.xlQualityStandard;
                    if (isHidden)
                    {
                        // Try and at least minimise it
                        app.WindowState = XlWindowState.xlMinimized;
                        app.Visible = false;
                    }

                    String readPassword = "";
                    if (!String.IsNullOrEmpty(passWord))
                    {
                        readPassword = passWord;
                    }
                    Object oReadPass = (Object)readPassword;

                    String writePassword = "";
                    if (!String.IsNullOrEmpty(writepassword))
                    {
                        writePassword = writepassword;
                    }
                    Object oWritePass = (Object)writePassword;

                    // Check for password protection and no password
                    if (Converter.IsPasswordProtected(inputFile) && String.IsNullOrEmpty(readPassword))
                    {
                        Console.WriteLine("Unable to open password protected file");
                        msg = Enum.GetName(typeof(ExitCode), ExitCode.PasswordFailure);
                        throw new ArgumentException(msg);
                    }

                    app.EnableEvents = false;
                    workbooks = app.Workbooks;
                    // If we have no write password and we're attempting to open for writing, we might be
                    // caught out by an unexpected write password
                    if (writePassword == "" && !nowrite)
                    {
                        oWritePass = (Object)"FAKEPASSWORD";
                        try
                        {
                            workbook = workbooks.Open(inputFile, updateLinks, nowrite, oMissing, oReadPass, oWritePass, true, oMissing, oMissing, oMissing, oMissing, oMissing, false, oMissing, oMissing);
                        }
                        catch (System.Runtime.InteropServices.COMException)
                        {
                            // Attempt to open it in read-only mode
                            workbook = workbooks.Open(inputFile, updateLinks, true, oMissing, oReadPass, oWritePass, true, oMissing, oMissing, oMissing, oMissing, oMissing, false, oMissing, oMissing);
                        }
                    }
                    else
                    {
                        workbook = workbooks.Open(inputFile, updateLinks, nowrite, oMissing, oReadPass, oWritePass, true, oMissing, oMissing, oMissing, oMissing, oMissing, false, oMissing, oMissing);
                    }
                    // Add in a delay to let Excel sort itself out
                    AddCOMDelay(options);

                    // Unable to open workbook
                    if (workbook == null)
                    {
                        msg = Enum.GetName(typeof(ExitCode), ExitCode.FileOpenFailure);
                        throw new ArgumentException(msg);
                    }

                    if (app.EnableEvents)
                    {
                        workbook.RunAutoMacros(XlRunAutoMacro.xlAutoOpen);
                    }

                    // Get any template options
                    SetPageOptionsFromTemplate(app, workbooks, options, ref templatePageSetup);

                    // Get the sheets
                    allSheets = workbook.Sheets;
                    var worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
                    worksheet.PageSetup.CenterHorizontally = true;
                    worksheet.PageSetup.FitToPagesWide = 1;
                    foreach (var image in lstImage)
                    {
						try 
						{
							var oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[image.row + 1, image.column];
							var pictureR = System.Drawing.Image.FromFile(image.link);
							float width = (float)((double)oRange.Width);
							float height = (float)((double)oRange.Height);
							float widthR = pictureR.Width;
							float heightR = pictureR.Height;
							float percent = height / heightR;

							var widthImg = (int)(widthR * percent);
							var heightImg = (int)(heightR * percent - 3.5);
							widthImg = widthImg > width ? (int)(width - 1) : widthImg;
							var rateColumnAndPicture = (width - widthImg) / 2;
							rateColumnAndPicture = rateColumnAndPicture <= 0 ? 0 : rateColumnAndPicture;

							var rateRowAndPicture = (height - heightImg) / 2;
							rateRowAndPicture = rateRowAndPicture <= 0 ? 2 : rateRowAndPicture;

							var Left = (float)((double)oRange.Left + rateColumnAndPicture);
							var Top = (float)((double)oRange.Top + rateRowAndPicture);

							var pic = worksheet.Shapes.AddPicture(image.link, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, widthImg, heightImg);
							
							pictureR.Dispose();
						}
						catch { }
                    }

                    float widthBackUp = -1;
                    foreach (var textInLine in lstText)
                    {
                        float leftTextPrev = -1, topTextPrev = 0, widthTextPrev = -1, heightTextPrev = 0;
                        if (textInLine.columnFPrev != null & textInLine.columnTPrev != null)
                        {
                            string textRangePrev = "";
                            var hasBoldPrev = false;
                            double fontSizePrev = 12;
                            string fontNamePrev = "Arial";
                            for (var iCl = textInLine.columnFPrev; iCl <= textInLine.columnTPrev; iCl++)
                            {
                                var oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[textInLine.row + 1, iCl + 1];
                                widthTextPrev += (float)((double)oRange.Width);

                                if (iCl == textInLine.columnFPrev)
                                {
                                    leftTextPrev = (float)((double)oRange.Left);
                                    topTextPrev = (float)((double)oRange.Top - textInLine.top);
                                    heightTextPrev = (float)((double)oRange.Height);
                                    textRangePrev = (string)oRange.Value2;
                                    oRange.Value2 = "";
                                    hasBoldPrev = (bool)oRange.Font.Bold;
                                    fontSizePrev = (double)oRange.Font.Size;
                                    fontNamePrev = (string)oRange.Font.Name;
                                }
                            }

                            var textBoxPrev = worksheet.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, leftTextPrev, topTextPrev, widthTextPrev, heightTextPrev);
                            textBoxPrev.TextFrame.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            textBoxPrev.TextFrame.MarginLeft = 3;
                            textBoxPrev.TextFrame.MarginRight = 2;
                            if (string.IsNullOrEmpty(textRangePrev))
                                textBoxPrev.Width = widthBackUp;
                            else
                                textBoxPrev.TextFrame.AutoSize = true;
                            textBoxPrev.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                            textBoxPrev.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                            var stringTextBoxPrev = textBoxPrev.TextFrame.Characters(Type.Missing, Type.Missing);
                            stringTextBoxPrev.Text = textRangePrev;
                            stringTextBoxPrev.Font.Name = fontNamePrev;
                            stringTextBoxPrev.Font.Bold = hasBoldPrev;
                            stringTextBoxPrev.Font.Size = fontSizePrev;

                            if (textBoxPrev.Width < textInLine.minWidth)
                                textBoxPrev.Width = (float)textInLine.minWidth;

                            leftTextPrev = textBoxPrev.Left;
                            widthTextPrev = textBoxPrev.Width;
                            widthBackUp = widthTextPrev;

                            
                        }

                        float leftText = 0, topText = 0, widthText = 0, heightText = 0;
                        string textRange = "";
                        var hasBold = false;
                        double fontSize = 12;
                        string fontName = "Arial";
                        for (var iCl = textInLine.columnF; iCl <= textInLine.columnT; iCl++)
                        {
                            var oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[textInLine.row + 1, iCl + 1];
                            widthText += (float)((double)oRange.Width);

                            if (iCl == textInLine.columnF)
                            {
                                leftText = (float)((double)oRange.Left - textInLine.left);
                                topText = (float)((double)oRange.Top - textInLine.top);
                                heightText = (float)((double)oRange.RowHeight);
                                textRange = (string)oRange.Text;
                                oRange.Value2 = "";
                                hasBold = (bool)oRange.Font.Bold;
                                fontSize = (double)oRange.Font.Size;
                                fontName = (string)oRange.Font.Name;
                            }
                        }

                        leftText = leftTextPrev <= -1 ? leftText : leftTextPrev + widthTextPrev;
                        var textBox = worksheet.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, leftText, topText, widthText, heightText);
                        textBox.TextFrame.MarginLeft = 2;
                        textBox.TextFrame.MarginRight = 2;
                        textBox.TextFrame.MarginTop = 0;
                        textBox.TextFrame.MarginBottom = 0;
                        textBox.Height = heightText;
                        textBox.TextFrame.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        textBox.TextFrame2.WordWrap = Microsoft.Office.Core.MsoTriState.msoCTrue;
                        textBox.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        textBox.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        var stringTextBox = textBox.TextFrame.Characters(Type.Missing, Type.Missing);
                        stringTextBox.Text = textRange;
                        stringTextBox.Font.Name = fontName;
                        stringTextBox.Font.Bold = hasBold;
                        stringTextBox.Font.Size = fontSize;
                    }

                    // Try and avoid xls files raising a dialog
                    var temporaryStorageDir = Path.GetTempFileName();
                    File.Delete(temporaryStorageDir);
                    Directory.CreateDirectory(temporaryStorageDir);
                    // We will save as xlsb (binary format) since this doesn't raise some errors when processing
                    tmpFile = Path.Combine(temporaryStorageDir, Path.GetFileNameWithoutExtension(inputFile) + ".xlsb");
                    fmt = XlFileFormat.xlExcel12;

                    // Set up the print quality
                    if (screenQuality)
                    {
                        quality = XlFixedFormatQuality.xlQualityMinimum;
                    }

                    // If a worksheet has been specified, try and use just the one
                    if (worksheetNum > 0)
                    {
                        // Force us just to use the active sheet
                        onlyActiveSheet = true;
                        try
                        {
                            if (worksheetNum > allSheets.Count)
                            {
                                // Sheet count is too big
                                msg = Enum.GetName(typeof(ExitCode), ExitCode.WorksheetNotFound);
                                throw new ArgumentException(msg);
                            }
                            if (allSheets[worksheetNum] is _Worksheet)
                            {
                                ((_Worksheet)allSheets[worksheetNum]).Activate();
                                sheetForConversionIdx = ((_Worksheet)allSheets[worksheetNum]).Index;
                            }
                            else if (allSheets[worksheetNum] is _Chart)
                            {
                                ((_Chart)allSheets[worksheetNum]).Activate();
                                sheetForConversionIdx = ((_Chart)allSheets[worksheetNum]).Index;
                            }

                        }
                        catch (Exception)
                        {
                            msg = Enum.GetName(typeof(ExitCode), ExitCode.WorksheetNotFound);
                            throw new ArgumentException(msg);
                        }
                    }

                    if (showFormulas)
                    {
                        // Determine whether to show formulas
                        try
                        {
                            activeWindow.DisplayFormulas = true;
                        }
                        catch (Exception) { }
                    }

                    // Keep the windows hidden
                    if (isHidden)
                    {
                        wbWin = workbook.Windows;
                        if (null != wbWin)
                        {
                            if (wbWin.Count > 0)
                            {
                                //wbWin[1].Visible = false;
                            }
                        }
                        if (null != activeWindow)
                        {
                            activeWindow.Visible = false;
                        }
                    }

                    // Keep track of the active sheet
                    int activeSheetIdx = 1;
                    if (workbook.ActiveSheet != null)
                    {
                        activeSheet = workbook.ActiveSheet;
                        if (activeSheet is _Worksheet)
                        {
                            activeSheetIdx = ((Worksheet)activeSheet).Index;
                        }
                        else if (activeSheet is _Chart)
                        {
                            activeSheetIdx = ((Microsoft.Office.Interop.Excel.Chart)activeSheet).Index;
                        }
                    }

                    // Large excel files may simply not print reliably - if the excel_max_rows
                    // configuration option is set, then we must close up and forget about 
                    // converting the file. However, if a print area is set in one of the worksheets
                    // in the document, then assume the author knew what they were doing and
                    // use the print area.

                    // We may need to loop through all the worksheets in the document
                    // depending on the options given. If there are maximum row restrictions
                    // or formulas are being shown, then we need to loop through all the
                    // worksheets
                    if (maxRows > 0 || showFormulas || showHeadings)
                    {
                        var row_count_check_ok = true;
                        var found_rows = 0;
                        var found_worksheet = "";
                        bool[] rowCountOK = new bool[allSheets.Count + 1];

                        // Loop through all the sheets (worksheets and charts)
                        for (int wsIdx = 1; wsIdx <= allSheets.Count; wsIdx++)
                        {
                            var ws = allSheets.Item[wsIdx];
                            rowCountOK[wsIdx] = true;

                            // Skip anything that is not the active sheet
                            if (onlyActiveSheet)
                            {
                                // Have to be careful to treat _Worksheet and _Chart items differently
                                try
                                {
                                    // Get the index of the active sheet
                                    if (wsIdx != activeSheetIdx)
                                    {
                                        // If we are not the active sheet, then skip to the next
                                        ReleaseCOMObject(ws);
                                        continue;
                                    }
                                }
                                catch (Exception)
                                {
                                    if (ws != null)
                                    {
                                        ReleaseCOMObject(ws);
                                    }
                                    continue;
                                }
                                sheetForConversionIdx = wsIdx;
                            }

                            if (showHeadings && ws is _Worksheet)
                            {
                                PageSetup pageSetup = null;
                                try
                                {
                                    pageSetup = ((Worksheet)ws).PageSetup;
                                    pageSetup.PrintHeadings = true;

                                }
                                catch (Exception) { }
                                finally
                                {
                                    ReleaseCOMObject(pageSetup);
                                }
                            }

                            // If showing formulas, make things auto-fit
                            if (showFormulas && ws is _Worksheet)
                            {
                                Range cols = null;
                                try
                                {
                                    ((_Worksheet)ws).Activate();
                                    activeWindow.DisplayFormulas = true;
                                    cols = ((Worksheet)ws).Columns;
                                    cols.AutoFit();
                                }
                                catch (Exception) { }
                                finally
                                {
                                    ReleaseCOMObject(cols);
                                }
                            }

                            // If there is a maximum row count, make sure we check each worksheet
                            if (maxRows > 0 && ws is _Worksheet)
                            {
                                // Check for a print area
                                var pageSetup = ((Worksheet)ws).PageSetup;
                                var printArea = pageSetup.PrintArea;
                                ReleaseCOMObject(pageSetup);
                                if (string.IsNullOrEmpty(printArea))
                                {
                                    // There is no print area, check that the row count is <= to the
                                    // excel_max_rows value. Note that we can't just take the range last
                                    // row, as this may return a huge value, rather find the last non-blank
                                    // row.
                                    var row_count = 0;
                                    var range = ((Worksheet)ws).UsedRange;
                                    if (range != null)
                                    {
                                        var rows = range.Rows;
                                        if (rows != null && rows.Count > maxRows)
                                        {
                                            var cells = range.Cells;
                                            if (cells != null)
                                            {
                                                var cellSearch = cells.Find("*", oMissing, oMissing, oMissing, oMissing, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious, false, oMissing, oMissing);
                                                // Make sure we actually get some results, since the worksheet may be totally blank
                                                if (cellSearch != null)
                                                {
                                                    row_count = cellSearch.Row;
                                                    found_worksheet = ((Worksheet)ws).Name;
                                                }
                                                ReleaseCOMObject(cellSearch);
                                            }
                                            ReleaseCOMObject(cells);
                                        }
                                        ReleaseCOMObject(rows);
                                    }
                                    ReleaseCOMObject(range);

                                    if (row_count > maxRows)
                                    {
                                        // Too many rows on this worksheet - mark the workbook as unprintable
                                        row_count_check_ok = false;
                                        rowCountOK[wsIdx] = false;
                                        found_rows = row_count;
                                        Converter.ReleaseCOMObject(ws);
                                        if (activeSheetOnMaxRows)
                                        {
                                            // Keep checking
                                            continue;
                                        }
                                        break;
                                    }
                                }
                            } // End of row check
                            Converter.ReleaseCOMObject(ws);
                        }

                        // Make sure we are not converting a document with too many rows
                        if (row_count_check_ok == false)
                        {
                            // We may want to try and convert the active sheet if it has not been included in the
                            // sheets with too many rows
                            bool bailOut = true;
                            if (activeSheetOnMaxRows && !onlyActiveSheet)
                            {
                                if (rowCountOK[activeSheetIdx])
                                {
                                    bailOut = false;
                                    sheetForConversionIdx = activeSheetIdx;
                                    onlyActiveSheet = true;
                                }
                            }
                            if (bailOut)
                            {
                                throw new Exception(String.Format("Too many rows to process ({0}) on worksheet {1}", found_rows, found_worksheet));
                            }
                        }
                    }

                    // Allow for re-calculation to be skipped
                    if (skipRecalculation)
                    {
                        app.Calculation = XlCalculation.xlCalculationManual;
                        app.CalculateBeforeSave = false;
                    }
                    else
                        app.Calculation = XlCalculation.xlCalculationAutomatic;

                    workbook.SaveAs(tmpFile, fmt, Type.Missing, Type.Missing, Type.Missing, false, XlSaveAsAccessMode.xlNoChange, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing);

                    if (onlyActiveSheet)
                    {
                        // Set up a delegate function for times we want to print
                        PrintDocument printFunc = delegate (string destination, string printer)
                        {
                            ((Worksheet)activeSheet).PrintOut(ActivePrinter: printer, PrintToFile: true, PrToFileName: destination);
                        };

                        if (sheetForConversionIdx > 0)
                        {
                            activeSheet = allSheets.Item[sheetForConversionIdx];
                        }
                        if (activeSheet is _Worksheet)
                        {
                            var wps = ((_Worksheet)activeSheet).PageSetup;
                            SetPageSetupProperties(templatePageSetup, wps);
                            if (String.IsNullOrEmpty(printerO))
                            {
                                try
                                {
                                    ((Worksheet)activeSheet).ExportAsFixedFormat(XlFixedFormatType.xlTypePDF,
                                    outputFile, quality, includeProps, false, Type.Missing, Type.Missing, false, Type.Missing);
                                }
                                catch (Exception)
                                {
                                    if (!String.IsNullOrEmpty(fallback_printer))
                                    {
                                        PrintToGhostscript(fallback_printer, outputFile, printFunc);
                                    }
                                    else
                                    {
                                        throw;
                                    }
                                }
                            }
                            else
                            {
                                PrintToGhostscript(printerO, outputFile, printFunc);
                            }
                            ReleaseCOMObject(wps);
                        }
                        else if (activeSheet is _Chart)
                        {
                            var wps = ((_Chart)activeSheet).PageSetup;
                            SetPageSetupProperties(templatePageSetup, wps);
                            ((Microsoft.Office.Interop.Excel.Chart)activeSheet).ExportAsFixedFormat(XlFixedFormatType.xlTypePDF,
                                outputFile, quality, includeProps, false, Type.Missing, Type.Missing, false, Type.Missing);
                            ReleaseCOMObject(wps);
                        }
                        else
                        {
                            msg = Enum.GetName(typeof(ExitCode), ExitCode.UnknownError);
                            throw new ArgumentException(msg);
                        }
                        AddCOMDelay(options);
                    }
                    else
                    {
                        //app.DisplayDocumentActionTaskPane = false;
                        //app.EnableEvents = false;
                        PrintDocument printFunc = delegate (string destination, string printer)
                        {
                            workbook.PrintOutEx(ActivePrinter: printer, PrintToFile: true, PrToFileName: destination);
                        };
                        if (HasTemplateOption(options))
                        {
                            // Set up the template page setup options on all the worksheets
                            // in the workbook
                            var worksheets = workbook.Worksheets;
                            for (int wsIdx = 1; wsIdx <= worksheets.Count; wsIdx++)
                            {
                                var ws = worksheets[wsIdx];
                                var wps = (ws is _Worksheet) ? ((_Worksheet)ws).PageSetup : ((_Chart)ws).PageSetup;
                                SetPageSetupProperties(templatePageSetup, wps);
                                ReleaseCOMObject(wps);
                                ReleaseCOMObject(ws);
                            }
                            ReleaseCOMObject(worksheets);
                        }
                        if (String.IsNullOrEmpty(printerO))
                        {
                            try
                            {
                                workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF,
                                outputFile, quality, includeProps, false, Type.Missing, Type.Missing, false, Type.Missing);
                            }
                            catch (Exception)
                            {
                                if (!String.IsNullOrEmpty(fallback_printer))
                                {
                                    PrintToGhostscript(fallback_printer, outputFile, printFunc);
                                }
                                else
                                {
                                    throw;
                                }
                            }
                        }
                        //else
                        //{
                        //    PrintToGhostscript((string)options["printer"], outputFile, printFunc);
                        //}
                    }

                    ReleaseCOMObject(allSheets);
                    ReleaseCOMObject(fmt);
                    ReleaseCOMObject(quality);
                }
                catch (COMException ce)
                {
                    msg = "COMException: " + ce;
                }
                catch (Exception e)
                {
                    msg = "Exception: " + e;
                }
                finally
                {
                    if (workbook != null)
                    {
                        ReleaseCOMObject(activeSheet);
                        ReleaseCOMObject(activeWindow);
                        ReleaseCOMObject(wbWin);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        // Excel sometimes needs a bit of a delay before we close in order to
                        // let things get cleaned up
                        workbook.Saved = true;
                        CloseExcelWorkbook(workbook);
                    }

                    //if (!running)
                    {
                        if (workbooks != null)
                        {
                            workbooks.Close();
                        }

                        if (app != null)
                        {
                            ((Microsoft.Office.Interop.Excel._Application)app).Quit();
                        }
                    }

                    // Clean all the COM leftovers
                    ReleaseCOMObject(workbook);
                    ReleaseCOMObject(workbooks);
                    ReleaseCOMObject(app);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    if (tmpFile != null && File.Exists(tmpFile))
                    {
                        System.IO.File.Delete(tmpFile);
                        // Remove the temporary path to the temp file
                        try
                        {
                            Directory.Delete(Path.GetDirectoryName(tmpFile));
                        }
                        catch { }
                    }

                    while (!File.Exists(outputFile))
                    {
                        Thread.Sleep(1000);
                    }
                }
            }, token);

            var timeFist = DateTime.Now;
            var timeWait = 120000;
            int seconds = timeWait / 1000;
            task.Start();
            task.Wait(timeWait);

            try
            {
                task.Dispose();
            }
            catch { }

            if ((DateTime.Now - timeFist).TotalSeconds >= seconds)
            {
                msg = string.Format(@"Timeout, limit to print is {0} seconds.", seconds);
                try
                {
                    if (workbooks != null)
                    {
                        workbooks.Close();
                    }
                }
                catch { }

                try
                {
                    if (app != null)
                    {
                        ((Microsoft.Office.Interop.Excel._Application)app).Quit();
                    }
                }
                catch { }

                ReleaseCOMObject(workbook);
                ReleaseCOMObject(workbooks);
                ReleaseCOMObject(app);
            }

            return msg;
        }

        // Return true if there is a valid template option
        protected static bool HasTemplateOption(Hashtable options)
        {
            if (String.IsNullOrEmpty((string)options["template"]) ||
                !File.Exists((string)options["template"]) ||
                !System.Text.RegularExpressions.Regex.IsMatch((string)options["template"], @"^.*\.xl[st][mx]?$"))
            {
                return false;
            }
            return true;
        }

        // Read the first worksheet from a template document and extract and store
        // the page settings for later use
        protected static void SetPageOptionsFromTemplate(Application app, Workbooks workbooks, Hashtable options, ref Hashtable templatePageSetup)
        {
            if (!HasTemplateOption(options))
            {
                return;
            }

            try
            {
                var template = workbooks.Open((string)options["template"]);
                AddCOMDelay(options);
                if (template != null)
                {
                    // Run macros from template if the /excel_template_macros option is given
                    if ((bool)options["excel_template_macros"])
                    {
                        var eventsEnabled = app.EnableEvents;
                        app.EnableEvents = true;
                        template.RunAutoMacros(XlRunAutoMacro.xlAutoOpen);
                        app.EnableEvents = eventsEnabled;
                    }

                    var templateSheets = template.Worksheets;
                    if (templateSheets != null)
                    {
                        // Copy the page setup details from the first sheet or chart in the template
                        if (templateSheets.Count > 0)
                        {
                            PageSetup tps = null;
                            var firstItem = templateSheets[1];
                            if (firstItem is _Worksheet)
                            {
                                tps = ((_Worksheet)firstItem).PageSetup;
                            }
                            else if (firstItem is _Chart)
                            {
                                tps = ((_Chart)firstItem).PageSetup;
                            }
                            var tpsType = tps.GetType();
                            for (int i = 0; i < templateProperties.Length; i++)
                            {
                                var prop = tpsType.InvokeMember(templateProperties[i], System.Reflection.BindingFlags.GetProperty, null, tps, null);
                                if (prop != null)
                                {
                                    templatePageSetup[templateProperties[i]] = prop;
                                }
                            }
                            //Converter.ReleaseCOMObject(firstItem);
                        }
                        //ReleaseCOMObject(templateSheets);
                    }
                    CloseExcelWorkbook(template);
                }
                //ReleaseCOMObject(template);
            }
            finally
            {
            }
        }

        // Add in the required millisecond delay
        private static void AddCOMDelay(Hashtable options)
        {
            if ((int)options["excel_delay"] > 0)
            {
                Thread.Sleep((int)options["excel_delay"]);
            }
        }

        // Load stored worksheet properties into the page setup
        protected static void SetPageSetupProperties(Hashtable tps, PageSetup wps)
        {
            if (tps == null || tps.Count == 0)
            {
                return;
            }

            var wpsType = wps.GetType();
            for (int i = 0; i < templateProperties.Length; i++)
            {
                object[] value = { tps[templateProperties[i]] };
                try
                {
                    wpsType.InvokeMember(templateProperties[i], System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.SetProperty, Type.DefaultBinder, wps, value);
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to set property {0}", templateProperties[i]);
                }
            }
        }

        private static bool CloseExcelWorkbook(Workbook workbook)
        {
            int tries = 20;
            while (tries-- > 0)
            {
                try
                {
                    workbook.Close();
                    return true;
                }
                catch (COMException)
                {
                    Thread.Sleep(500);
                }
            }
            return false;
        }
    }
}
