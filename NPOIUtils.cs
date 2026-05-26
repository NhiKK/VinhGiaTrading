using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

public static class NPOIUtils
{
    public static ICellStyle CellNumberStyleNoMod(HSSFWorkbook hssfworkbook, IFont font)
    {
        return NPOIUtils.CellNumberStyle(hssfworkbook, font, false);
    }

    public static ICellStyle CellNumberBorderStyleNoMod(HSSFWorkbook hssfworkbook, IFont font, bool border)
    {
        ICellStyle cellStyleBoderNumber8 = hssfworkbook.CreateCellStyle();
        cellStyleBoderNumber8.Alignment = HorizontalAlignment.Right;
        cellStyleBoderNumber8.VerticalAlignment = VerticalAlignment.Center;
        cellStyleBoderNumber8.SetFont(font);

        if (border)
        {
            cellStyleBoderNumber8.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleBoderNumber8.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleBoderNumber8.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleBoderNumber8.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        }

        var formatId = HSSFDataFormat.GetBuiltinFormat("#,##");
        if (formatId == -1)
        {
            var newDataFormat = hssfworkbook.CreateDataFormat();
            cellStyleBoderNumber8.DataFormat = newDataFormat.GetFormat("#,##");
        }
        else
        {
            cellStyleBoderNumber8.DataFormat = formatId;
        }

        return cellStyleBoderNumber8;
    }

    public static short GetDataFormat(HSSFWorkbook hssfworkbook, string format)
    {
        try
        {
            short formatId = HSSFDataFormat.GetBuiltinFormat(format);
            if (formatId == -1)
            {
                IDataFormat newFmt = hssfworkbook.CreateDataFormat();
                return newFmt.GetFormat(format);
            }
            throw new Exception("Không tìm thấy kiểu định dạng.");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static ICellStyle CellNumberStyle(HSSFWorkbook hssfworkbook, IFont font, bool border)
    {
        ICellStyle cellStyleBoderNumber8 = hssfworkbook.CreateCellStyle();
        cellStyleBoderNumber8.Alignment = HorizontalAlignment.Right;
        cellStyleBoderNumber8.VerticalAlignment = VerticalAlignment.Center;
        cellStyleBoderNumber8.SetFont(font);

        if (border)
        {
            cellStyleBoderNumber8.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleBoderNumber8.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleBoderNumber8.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyleBoderNumber8.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        }

        var formatId = HSSFDataFormat.GetBuiltinFormat("#,##0.00");
        if (formatId == -1)
        {
            var newDataFormat = hssfworkbook.CreateDataFormat();
            cellStyleBoderNumber8.DataFormat = newDataFormat.GetFormat("#,##0.00");
        }
        else
        {
            cellStyleBoderNumber8.DataFormat = formatId;
        }

        return cellStyleBoderNumber8;
    }

    public static ICellStyle CellNumberStyle(HSSFWorkbook hssfworkbook, IFont font)
    {
        return NPOIUtils.CellNumberStyle(hssfworkbook, font, false);
    }

    /// <summary>
    /// Tạo Font Size
    /// </summary>
    /// <param name="hsswb">Tạo font cho HSSFWorkbook</param>
    /// <param name="size">Độ to của font muốn tạo</param>
    /// <returns>IFont với độ to size</returns>
    public static IFont CreateFontSize(HSSFWorkbook hsswb, short size)
    {
        return NPOIUtils.CreateFontSize(hsswb, size, false);
    }

    /// <summary>
    /// Tạo Font Size
    /// </summary>
    /// <param name="hsswb">Tạo font cho HSSFWorkbook</param>
    /// <param name="size">Độ to của font muốn tạo</param>
    /// <param name="IsUnderline">Có gạch chân khi IsUnderline = true</param>
    /// <returns>IFONT có độ to size và kiểu gạch chân IsUnderline</returns>
    public static IFont CreateFontSize(HSSFWorkbook hsswb, short size, bool IsUnderline)
    {
        IFont font = hsswb.CreateFont();
        font.FontHeightInPoints = size;
        if (IsUnderline)
        {
            font.Underline = 1;    
        }
        return font;
    }
}
