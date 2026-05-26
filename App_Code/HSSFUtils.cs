using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace HSSFUtils
{
    public static class CellDataFormat
    {
        public static short GetDataFormat(HSSFWorkbook workbook, String formatData)
        {
            var formatId = HSSFDataFormat.GetBuiltinFormat(formatData);
            if (formatId == -1)
            {
                var newDataFormat = workbook.CreateDataFormat();
                return newDataFormat.GetFormat(formatData);
            }
            else
            {
                return formatId;
            }  
        }
    }

    public class CellStyleModel
    {
        private IFont font;
        private HSSFWorkbook workbook;
        private bool borderAll;
        private String formatData;
        private HorizontalAlignment _horizontalAlignment = HorizontalAlignment.Left;
        private VerticalAlignment _verticalAlignment = VerticalAlignment.Top;

        public VerticalAlignment VerticalAlignment
        {
            get { return _verticalAlignment; }
            set { _verticalAlignment = value; }
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return _horizontalAlignment; }
            set { _horizontalAlignment = value; }
        }

        public CellStyleModel(HSSFWorkbook workbook)
        {
            this.workbook = workbook;
        }

        public ICellStyle CreateCellStyle()
        {
            ICellStyle cs = workbook.CreateCellStyle();
            
            // font
            if (font != null)
            {
                cs.SetFont(font);
            }

            // border
            if (borderAll)
            {
                this.SetBorder(cs, BorderStyle.Thin);
            }

            // Format data
            if (formatData != null)
            {
                var formatId = HSSFDataFormat.GetBuiltinFormat(formatData);
                if (formatId == -1)
                {
                    var newDataFormat = workbook.CreateDataFormat();
                    cs.DataFormat = newDataFormat.GetFormat(formatData);
                }
                else
                {
                    cs.DataFormat = formatId;
                }    
            }

            cs.Alignment = _horizontalAlignment;
            cs.VerticalAlignment = _verticalAlignment;
            return cs;  
        }

        private void SetBorder(ICellStyle CellStyle, BorderStyle borderStyle)
        {
            CellStyle.BorderLeft
                = CellStyle.BorderRight
                = CellStyle.BorderTop
                = CellStyle.BorderBottom
                = borderStyle;
        }

        public IFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public bool BorderAll
        {
            get { return borderAll; }
            set { borderAll = value; }
        }

        public String FormatData
        {
            get { return formatData; }
            set { formatData = value; }
        }
    }
}
