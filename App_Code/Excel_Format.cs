using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using HSSFUtils;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.HSSF.Record.AutoFilter;

/// <summary>
/// Tien Ich Chuyen Tien So Thanh Tien Chu
/// </summary>
public class Excel_Format
{
	HSSFWorkbook hssfworkbook = null;
	public Excel_Format(HSSFWorkbook hssf)
    {
		hssfworkbook = hssf;
	}
	
	public IFont getfont(short size, bool bold)
    {
        IFont font = hssfworkbook.CreateFont();
		if(bold == true) {
			font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
		}
		font.FontHeightInPoints = (short)size;
		
		return font;
    }
	
	public ICellStyle getcell(short size, bool bold, bool wraptext, string border, string HorAlign, string VerAlign)  
	{
		ICellStyle cell = hssfworkbook.CreateCellStyle();
        cell.SetFont(getfont(size, bold));
        cell.WrapText = wraptext;
		//set border
		if(border.Contains("L")) {
			cell.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("R")) {
			cell.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("T")) {
			cell.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("B")) {
			cell.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		
		if(HorAlign == "L") {
			cell.Alignment = HorizontalAlignment.Left;
		}
		else if(HorAlign == "R") {
			cell.Alignment = HorizontalAlignment.Right;
		}
		else if(HorAlign == "C") {
			cell.Alignment = HorizontalAlignment.Center;
		}
		
		if(VerAlign == "B") {
            cell.VerticalAlignment = 0;
        }
		else if(VerAlign == "T") {
			cell.VerticalAlignment = VerticalAlignment.Top;
		}
		else if(VerAlign == "C") {
			cell.VerticalAlignment = VerticalAlignment.Center;
		}
        else if (VerAlign == "J")
        {
            cell.VerticalAlignment = VerticalAlignment.Justify;
        }
        return cell;
	}
	
	public ICellStyle getcell2(short size, bool bold, bool wraptext, string border, string HorAlign, string VerAlign, string dataFormat) 
	{
		ICellStyle cell = hssfworkbook.CreateCellStyle();
        cell.SetFont(getfont(size, bold));
        cell.WrapText = wraptext;
		//set border
		if(border.Contains("L")) {
			cell.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("R")) {
			cell.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("T")) {
			cell.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("B")) {
			cell.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		
		if(HorAlign == "L") {
			cell.Alignment = HorizontalAlignment.Left;
		}
		else if(HorAlign == "R") {
			cell.Alignment = HorizontalAlignment.Right;
		}
		else if(HorAlign == "C") {
			cell.Alignment = HorizontalAlignment.Center;
		}
		
		if(VerAlign == "B") {
			cell.VerticalAlignment = VerticalAlignment.Bottom;
		}
		else if(VerAlign == "T") {
			cell.VerticalAlignment = VerticalAlignment.Top;
		}
		else if(VerAlign == "C") {
			cell.VerticalAlignment = VerticalAlignment.Center;
		}
		cell.DataFormat = hssfworkbook.CreateDataFormat().GetFormat(dataFormat);
		return cell;
	}
	
	public void set_border(CellRangeAddress cellRange, string border, HSSFSheet hssfsheet) {
		if(border.Contains("L")) {
			HSSFRegionUtil.SetBorderLeft(NPOI.SS.UserModel.BorderStyle.Thin, cellRange, hssfsheet, hssfworkbook);
		}
		if(border.Contains("R")) {
			HSSFRegionUtil.SetBorderRight(NPOI.SS.UserModel.BorderStyle.Thin, cellRange, hssfsheet, hssfworkbook);
		}
		if(border.Contains("T")) {
			HSSFRegionUtil.SetBorderTop(NPOI.SS.UserModel.BorderStyle.Thin, cellRange, hssfsheet, hssfworkbook);
		}
		if(border.Contains("B")) {
			HSSFRegionUtil.SetBorderBottom(NPOI.SS.UserModel.BorderStyle.Thin, cellRange, hssfsheet, hssfworkbook);
		}
	}
	
	public void set_border2(CellRangeAddress cellRange, string border, HSSFSheet hssfsheet) {
		if(border.Contains("L")) {
			HSSFRegionUtil.SetBorderLeft(NPOI.SS.UserModel.BorderStyle.Thick, cellRange, hssfsheet, hssfworkbook);
		}
		if(border.Contains("R")) {
			HSSFRegionUtil.SetBorderRight(NPOI.SS.UserModel.BorderStyle.Thick, cellRange, hssfsheet, hssfworkbook);
		}
		if(border.Contains("T")) {
			HSSFRegionUtil.SetBorderTop(NPOI.SS.UserModel.BorderStyle.Thick, cellRange, hssfsheet, hssfworkbook);
		}
		if(border.Contains("B")) {
			HSSFRegionUtil.SetBorderBottom(NPOI.SS.UserModel.BorderStyle.Thick, cellRange, hssfsheet, hssfworkbook);
		}
	}
	
	public ICellStyle cell_decimal_1(short size, bool bold, bool wraptext, string border, string HorAlign, string VerAlign)
	{
		ICellStyle cell = hssfworkbook.CreateCellStyle();
        cell.SetFont(getfont(size, bold));
        cell.WrapText = wraptext;
		//set border
		if(border.Contains("L")) {
			cell.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("R")) {
			cell.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("T")) {
			cell.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("B")) {
			cell.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		
		if(HorAlign == "L") {
			cell.Alignment = HorizontalAlignment.Left;
		}
		else if(HorAlign == "R") {
			cell.Alignment = HorizontalAlignment.Right;
		}
		else if(HorAlign == "C") {
			cell.Alignment = HorizontalAlignment.Center;
		}
		
		if(VerAlign == "B") {
			cell.VerticalAlignment = VerticalAlignment.Bottom;
		}
		else if(VerAlign == "T") {
			cell.VerticalAlignment = VerticalAlignment.Top;
		}
		else if(VerAlign == "C") {
			cell.VerticalAlignment = VerticalAlignment.Center;
		}
		cell.DataFormat = hssfworkbook.CreateDataFormat().GetFormat("#0.0");
		return cell;
	}
	
	public ICellStyle cell_decimal_2(short size, bool bold, bool wraptext, string border, string HorAlign, string VerAlign)
	{
		ICellStyle cell = hssfworkbook.CreateCellStyle();
        cell.SetFont(getfont(size, bold));
        cell.WrapText = wraptext;
		//set border
		if(border.Contains("L")) {
			cell.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("R")) {
			cell.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("T")) {
			cell.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("B")) {
			cell.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		
		if(HorAlign == "L") {
			cell.Alignment = HorizontalAlignment.Left;
		}
		else if(HorAlign == "R") {
			cell.Alignment = HorizontalAlignment.Right;
		}
		else if(HorAlign == "C") {
			cell.Alignment = HorizontalAlignment.Center;
		}
		
		if(VerAlign == "B") {
			cell.VerticalAlignment = VerticalAlignment.Bottom;
		}
		else if(VerAlign == "T") {
			cell.VerticalAlignment = VerticalAlignment.Top;
		}
		else if(VerAlign == "C") {
			cell.VerticalAlignment = VerticalAlignment.Center;
		}
		cell.DataFormat = hssfworkbook.CreateDataFormat().GetFormat("#0.00");
		return cell;
	}
	
	public ICellStyle cell_decimal_3(short size, bool bold, bool wraptext, string border, string HorAlign, string VerAlign)
	{
		ICellStyle cell = hssfworkbook.CreateCellStyle();
        cell.SetFont(getfont(size, bold));
        cell.WrapText = wraptext;
		//set border
		if(border.Contains("L")) {
			cell.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("R")) {
			cell.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("T")) {
			cell.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		if(border.Contains("B")) {
			cell.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
		}
		
		if(HorAlign == "L") {
			cell.Alignment = HorizontalAlignment.Left;
		}
		else if(HorAlign == "R") {
			cell.Alignment = HorizontalAlignment.Right;
		}
		else if(HorAlign == "C") {
			cell.Alignment = HorizontalAlignment.Center;
		}
		
		if(VerAlign == "B") {
			cell.VerticalAlignment = VerticalAlignment.Bottom;
		}
		else if(VerAlign == "T") {
			cell.VerticalAlignment = VerticalAlignment.Top;
		}
		else if(VerAlign == "C") {
			cell.VerticalAlignment = VerticalAlignment.Center;
		}
		cell.DataFormat = hssfworkbook.CreateDataFormat().GetFormat("#0.000");
		return cell;
	}
}
