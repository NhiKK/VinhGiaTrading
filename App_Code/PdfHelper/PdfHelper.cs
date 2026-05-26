using System;
using System.Collections.Generic;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace iTextSharp
{
    namespace Helper
    {
        public class PdfHelper
        {
            public static bool executeElements(Document doc, params IElement[] ielement)
            {
                for (int i = 0; i < ielement.Length; i++)
                {
                    doc.Add(ielement[i]);
                }
                return true;
            }

            public static Font getNewFont(String fontPath)
            {
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, true);
                Font font = new Font(bf);
                return font;
            }



            public static PdfPTable getTable(SqlDataReader DataSource, int[] headerWidths, Font font, params object[] columnNames)
            {
                SqlDataReader rd = DataSource;
                int colCount = rd.FieldCount;
                PdfPTable tab = new PdfPTable(colCount);
                tab.SetWidths(headerWidths);
                for (int i = 0; i < columnNames.Length; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(columnNames[i].ToString(), font));
                    cell.BackgroundColor = new BaseColor(220,220,220);
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    tab.AddCell(cell);
                }

                while (rd.Read())
                {
                    for (int i = 0; i < colCount; i++)
                    {
                        PdfPCell cell = new PdfPCell();
                        cell.AddElement(new Phrase(rd[i].ToString(), font));
                        tab.AddCell(cell);
                    }
                }
                return tab;
            }
        }
    }
}