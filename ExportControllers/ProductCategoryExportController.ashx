<%@ WebHandler Language="C#" Class="ProductCategoryExportController" %>

using System;
using System.Web;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data.SqlClient;
using System.Collections.Generic;
using iTextSharp.Helper;
using System.Data;
using iTextSharp.text.html.simpleparser;

public class ProductCategoryExportController : IHttpHandler
{ 
    public static String COMPANY = Customer.getCompany;
    public static String ADDRESS = Customer.getAddress;
    public static String CONTACT = Customer.getContact;
    public static String LOGO = String.Format("../images/{0}",Customer.getLogo) ;

    public void ProcessRequest(HttpContext context)
    {
        String oper = context.Request.QueryString["oper"];
        switch (oper)
        {
            case "pdf":
            default:
                this.exportToPDF(context);
                break;
        }
    }

    private void exportToPDF(HttpContext context)
    {
        // create font
        Font font = PdfHelper.getNewFont(context.Server.MapPath("../Fonts/TIMES.TTF"));
        font.Size = 8;

        Font fontInfo = PdfHelper.getNewFont(context.Server.MapPath("../Fonts/TIMES.TTF"));
        fontInfo.Size = 10;

        Font fontTitle = PdfHelper.getNewFont(context.Server.MapPath("../Fonts/TIMES.TTF"));
        fontTitle.Size = 13;

        MemoryStream ms = new MemoryStream();
        Document doc = new Document(PageSize.A4);
        PdfWriter writer = PdfWriter.GetInstance(doc, ms);

        doc.Open();

        // create info
        String info = String.Format("{0}\r\n{1}\r\n{2}\r\n", COMPANY, ADDRESS, CONTACT);
        Paragraph p = new Paragraph(info, fontInfo);
        p.IndentationLeft = 150f;
        p.SpacingAfter = 10f;


        // create logo
        Image img = Image.GetInstance(context.Server.MapPath(LOGO));
        img.SetAbsolutePosition(60, doc.PageSize.Height - 80);
        img.ScalePercent(25f, 20f);

        // create title
        Paragraph pTitle = new Paragraph("DANH SÁCH LOẠI SẢN PHẨM", fontTitle);
        pTitle.Alignment = 1;
        pTitle.SpacingAfter = 10;
        pTitle.SpacingBefore = 10;

        // create table
        string sql = "select ma_loaisanpham, ten_loaisanpham, mota from md_loaisanpham";
        SqlDataReader rd = mdbc.ExecuteReader(sql);
        int colCount = rd.FieldCount;

        int[] headerwidths = { 5, 30, 20 };
        PdfPTable tab = PdfHelper.getTable(rd, headerwidths, font
            ,"Mã Loại", "Tên Loại", "Mô Tả");

        //add
        PdfHelper.executeElements(doc, p, img, pTitle, tab);

        doc.Close();
        writer.Close();

        context.Response.ClearContent();
        context.Response.ClearHeaders();
        context.Response.ContentType = "application/pdf";
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=ProductCategory.pdf");
        context.Response.BinaryWrite(ms.ToArray());
        context.Response.End();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}