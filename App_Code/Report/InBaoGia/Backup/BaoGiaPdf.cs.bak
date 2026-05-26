using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;

/// <summary>
/// Summary description for BaoGiaPdf
/// </summary>
public class BaoGiaPdf
{
    private String c_baogia_id, logoPath, productImagePath;
    LinqDBDataContext db = new LinqDBDataContext();

    public void CreatePdfQuotation(String fileName)
    {
        try
        {
            c_baogia bg = db.c_baogias.FirstOrDefault(p => p.c_baogia_id.Equals(c_baogia_id));
            String selectDetails = String.Format(@"select 
	            dtkd.ten_dtkd, dtkd.diachi, bg.ngaybaogia
	            , sp.ma_sanpham, SUBSTRING(sp.ma_sanpham,0,9) as pic, sp.ma_sanpham, ctbg.ma_sanpham_khach
	            , (CASE 
					WHEN kt.ten_kichthuoc = 'inch' then dbo.f_taomotainchQoPo(sp.md_sanpham_id) 
					ELSE ctbg.mota_tienganh
				END) as mota_tienganh
				, (select dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id)) as trongluong
                ,  tl.ten_trongluong
                , (CAST(dg.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner)) as sl_inner
	            , (CAST(dg.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer)) as master
                , dg.ten_donggoi, ctbg.l1, ctbg.w1, ctbg.h1, ctbg.l2, ctbg.w2, ctbg.h2, ctbg.v2, round(ctbg.v2 * 35.31466672, 2)  as cbf
	            , dg.soluonggoi_ctn as nopack, ctbg.giafob
	            , sp.l_cm, sp.w_cm, sp.h_cm
	            , bg.shipmenttime, cb.ten_cangbien
	            , bg.ngayhethan, kt.ten_kichthuoc, bg.moq_item_color, hscode.hscode
            from 
	            c_baogia bg
				left join c_chitietbaogia ctbg ON bg.c_baogia_id = ctbg.c_baogia_id
	            left join md_doitackinhdoanh dtkd ON bg.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
				left join md_sanpham sp ON ctbg.md_sanpham_id = sp.md_sanpham_id
				left join md_hscode hscode ON sp.md_hscode_id = hscode.md_hscode_id
	            left join md_donggoi dg ON ctbg.md_donggoi_id = dg.md_donggoi_id
	            left join md_cangbien cb ON sp.md_cangbien_id = cb.md_cangbien_id
                left join md_kichthuoc kt ON bg.md_kichthuoc_id = kt.md_kichthuoc_id
                left join md_trongluong tl ON bg.md_trongluong_id = tl.md_trongluong_id
			where bg.c_baogia_id = N'{0}'  order by sp.ma_sanpham asc ", c_baogia_id == null ? "" : c_baogia_id);


            String selectRefereces = String.Format(@"SELECT 
	                                                  (select ta_dai from md_chungloai cl where cl.code_cl = SUBSTRING(filter, 1, 2)) as ta_dai, SUBSTRING(filter, 1, 2) as chungloai, SUBSTRING(filter, 4, 2) as codemau, mau , url
                                                   FROM 
	                                                   c_baogia bg, md_color_reference color
                                                   WHERE
	                                                   bg.c_baogia_id = color.c_baogia_id
                                                       AND color.hoatdong = 1
                                                       AND bg.c_baogia_id = N'{0}'", c_baogia_id == null ? "" : c_baogia_id);

            String selectDistinct = String.Format(@"SELECT distinct chungloai
                                                    FROM(
                                                        SELECT 
	                                                        (select ta_dai from md_chungloai cl where cl.code_cl = SUBSTRING(filter, 1, 2)) as ta_dai, SUBSTRING(filter, 1, 2) as chungloai, SUBSTRING(filter, 4, 2) as codemau, mau , url
                                                        FROM 
	                                                        c_baogia bg, md_color_reference color
                                                        WHERE
	                                                        bg.c_baogia_id = color.c_baogia_id
                                                            AND bg.c_baogia_id = N'{0}'
                                                    ) as tmp", c_baogia_id == null ? "" : c_baogia_id);

            DataTable dtDetails = mdbc.GetData(selectDetails);
            DataTable dtDistinct = mdbc.GetData(selectDistinct);
            DataTable dtReferences = mdbc.GetData(selectRefereces);
            if (dtDetails.Rows.Count != 0)
            {
                this.CreatePDF(fileName, dtDetails, dtDistinct, dtReferences, bg.isform.Value);
            }
            else
            {
                throw new Exception("Báo giá không có dữ liệu!");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CreatePDF(String fileName, DataTable dtDetails, DataTable dtDistinct, DataTable dtReferences, bool isForm)
    {
        string tenkichthuoc = dtDetails.Rows[0]["ten_kichthuoc"].ToString();
        var doc1 = new Document(PageSize.A4_LANDSCAPE.Rotate(), 5F, 5F, 5F, 5F);
        PdfWriter.GetInstance(doc1, new FileStream(fileName, FileMode.Create));
        doc1.Open();
        try
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times_8 = new Font(bfTimes, 8, Font.NORMAL, BaseColor.BLACK);
            Font times_10 = new Font(bfTimes, 10, Font.BOLD, BaseColor.BLACK);
            Font times_13 = new Font(bfTimes, 13, Font.BOLD, BaseColor.BLACK);
            Font times_15 = new Font(bfTimes, 15, Font.BOLD, BaseColor.BLACK);

            Paragraph pCompany = new Paragraph("ANCO COMPANY LIMITED", times_13);
            pCompany.Alignment = 1;

            Paragraph pAddress = new Paragraph("3B/2, 1B Hamlet, An Phu Ward, Thuan An Dist., Binh Duong Province, Vietnam", times_8);
            pAddress.Alignment = 1;

            Paragraph pInfo = new Paragraph("Tel.: (84-274) 3740 973   Fax:  (84-274) 3740 621  Email: anco@ancopottery.com", times_8);
            pInfo.Alignment = 1;

            Paragraph pTitle = new Paragraph("QUOTATION", times_15);
            pTitle.Alignment = 1;

            PdfPTable tabHeader = new PdfPTable(2);
            float[] widths_header = new float[] { 0.2f, 2f };
            tabHeader.SetWidths(widths_header);

            PdfPCell cellInformation = new PdfPCell();
            cellInformation.Border = 0;
            cellInformation.AddElement(pCompany);
            cellInformation.AddElement(pAddress);
            cellInformation.AddElement(pInfo);
            cellInformation.AddElement(pTitle);

            if (isForm)
            {
                Paragraph pPortLoading = new Paragraph(dtDetails.Rows[0]["ten_cangbien"].ToString(), times_10);
                pPortLoading.Alignment = 1;
                cellInformation.AddElement(pPortLoading);
            }

            PdfPCell cellImg = new PdfPCell();
            cellImg.Border = 0;
            cellImg.AddElement(Image.GetInstance(logoPath));

            tabHeader.AddCell(cellImg);
            tabHeader.AddCell(cellInformation);
            doc1.Add(tabHeader);

            // # table header

            PdfPTable tabCustomer = new PdfPTable(4);
            float[] widths_customer = new float[] { 0.7f, 2f, 0.5f, 0.5f };
            tabCustomer.SetWidths(widths_customer);



            tabCustomer.AddCell(new Paragraph("CUSTOMER NAME:", times_8));
            tabCustomer.AddCell(new Paragraph("18 KARAT INT. PRODUCT SOURCING", times_8));
            tabCustomer.AddCell(new Paragraph(""));
            tabCustomer.AddCell(new Paragraph(""));

            tabCustomer.AddCell(new Paragraph("ADDRESS:", times_8));
            tabCustomer.AddCell(new Paragraph("5292-272nd street, Langley, BC CA V4W1S3, Canada", times_8));
            tabCustomer.AddCell(new Paragraph("DATE:", times_8));
            tabCustomer.AddCell(new Paragraph("14/12/2012", times_8));

            for (int i = 0; i < 2; i++)
            {
                foreach (PdfPCell item in tabCustomer.GetRow(i).GetCells())
                {
                    item.Border = 0;
                }
            }
            doc1.Add(tabCustomer);
            // # table customer

            PdfPTable tabDetails;

            if (isForm && tenkichthuoc.Equals("cm"))
            {
                tabDetails = new PdfPTable(12);
            }
            else if (!isForm && tenkichthuoc.Equals("cm"))
            {
                tabDetails = new PdfPTable(13);
            }
            else if (isForm && tenkichthuoc.Equals("inch"))
            {
                tabDetails = new PdfPTable(13);
            }
            else
            {
                tabDetails = new PdfPTable(14);
            }



            float[] widths_details;
            
            if (isForm && tenkichthuoc.Equals("cm"))
	        {
		        widths_details = new float[] { 1f, 1f, 0.7f, 1.5f, 0.4f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 0.5f };
            }
            else if (!isForm && tenkichthuoc.Equals("cm"))
            {
                widths_details = new float[] { 1f, 1f, 0.7f, 1.5f, 0.4f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
            }
            else if (isForm && tenkichthuoc.Equals("inch"))
            {
                widths_details = new float[] { 1f, 1f, 0.7f, 1.5f, 0.4f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
            }
            else
            {
                widths_details = new float[] { 1f, 1f, 0.7f, 1.5f, 0.4f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
            }

            tabDetails.SetWidths(widths_details);

            PdfPTable tabPak = new PdfPTable(2);

            // add tab packing header
            PdfPCell cHPak = new PdfPCell(new Phrase("Packing", times_8));
            cHPak.Colspan = 2;
            tabPak.AddCell(cHPak);

            tabPak.AddCell(new Phrase("Inner", times_8));
            tabPak.AddCell(new Phrase("Master", times_8));

            //create cell packing
            PdfPCell cellPak = new PdfPCell(tabPak);
            cellPak.Padding = 0f;

            PdfPTable tabInner = new PdfPTable(3);
            PdfPCell cHInner = new PdfPCell(new Phrase("Inner Packing size ("+dtDetails.Rows[0]["ten_kichthuoc"]+")", times_8));
            cHInner.Colspan = 3;
            tabInner.AddCell(cHInner);
            tabInner.AddCell(new Phrase("L", times_8));
            tabInner.AddCell(new Phrase("W", times_8));
            tabInner.AddCell(new Phrase("H", times_8));


            // create cell packing master
            PdfPCell cellInner = new PdfPCell(tabInner);
            cellInner.Padding = 0f;


            PdfPTable tabMaster = new PdfPTable(3);
            PdfPCell cHMaster = new PdfPCell(new Phrase("Master Packing size (" + dtDetails.Rows[0]["ten_kichthuoc"] + ")", times_8));
            cHMaster.Colspan = 3;
            tabMaster.AddCell(cHMaster);
            tabMaster.AddCell(new Phrase("L", times_8));
            tabMaster.AddCell(new Phrase("W", times_8));
            tabMaster.AddCell(new Phrase("H", times_8));

            // create cell packing master
            PdfPCell cellMaster = new PdfPCell(tabMaster);
            cellMaster.Padding = 0f;


            tabDetails.AddCell(new Phrase("Item No", times_8));
            tabDetails.AddCell(new Phrase("Picture - Only for shape ref. ", times_8));
            tabDetails.AddCell(new Phrase("Customer's Code", times_8));
            tabDetails.AddCell(new Phrase("Description", times_8));
            tabDetails.AddCell(new Paragraph("Weight(" + dtDetails.Rows[0]["ten_trongluong"] + ")", times_8));
            tabDetails.AddCell(cellPak);
            tabDetails.AddCell(cellInner);
            tabDetails.AddCell(cellMaster);
            tabDetails.AddCell(new Paragraph("PACK CBM", times_8));
            tabDetails.AddCell(new Paragraph("No. of packs/40'", times_8));
            tabDetails.AddCell(new Paragraph("FOB PRICE (USD)", times_8));

            if (!isForm)
            {
                tabDetails.AddCell(new Paragraph("Port Loading", times_8));
            }

            if (tenkichthuoc.Equals("inch"))
            {
                tabDetails.AddCell(new Paragraph("CBF", times_8));
            }

			tabDetails.AddCell(new Paragraph("HS Code", times_8));

            foreach (DataRow r in dtDetails.Rows)
            {
                tabDetails.AddCell(new Phrase(r["ma_sanpham"].ToString(), times_8));

                try
                {
                    PdfPCell cellDImg = new PdfPCell();
                    cellDImg.Border = 1;
                    cellDImg.AddElement(Image.GetInstance(productImagePath + r["pic"].ToString() + ".jpg"));
                    tabDetails.AddCell(cellDImg);
                }
                catch
                {
                }

                tabDetails.AddCell(new Phrase(r["ma_sanpham_khach"].ToString(), times_8));
                tabDetails.AddCell(new Phrase(r["mota_tienganh"].ToString(), times_8));
                tabDetails.AddCell(new Phrase(r["trongluong"].ToString(), times_8));

                //Packing
                PdfPTable tabDPak = new PdfPTable(2);
                tabDPak.AddCell(new Phrase(r["sl_inner"].ToString(), times_8));
                tabDPak.AddCell(new Phrase(r["master"].ToString(), times_8));

                PdfPCell cellDPak = new PdfPCell(tabDPak);
                cellDPak.Padding = 0f;
                tabDetails.AddCell(cellDPak);

                //Inner
                PdfPTable tabDInner = new PdfPTable(3);
                tabDInner.AddCell(new Phrase(r["l1"].ToString(), times_8));
                tabDInner.AddCell(new Phrase(r["w1"].ToString(), times_8));
                tabDInner.AddCell(new Phrase(r["h1"].ToString(), times_8));
                PdfPCell cellDInner = new PdfPCell(tabDInner);
                cellDInner.Padding = 0f;
                tabDetails.AddCell(cellDInner);

                //Master
                PdfPTable tabDMaster = new PdfPTable(3);
                tabDMaster.AddCell(new Phrase(r["l2"].ToString(), times_8));
                tabDMaster.AddCell(new Phrase(r["w2"].ToString(), times_8));
                tabDMaster.AddCell(new Phrase(r["h2"].ToString(), times_8));
                PdfPCell cellDMater = new PdfPCell(tabDMaster);
                cellDMater.Padding = 0f;
                tabDetails.AddCell(cellDMater);



                tabDetails.AddCell(new Phrase(r["v2"].ToString(), times_8));
                tabDetails.AddCell(new Phrase(r["nopack"].ToString(), times_8));
                tabDetails.AddCell(new Phrase(r["giafob"].ToString(), times_8));

                // khong phai don hang mau
                if (!isForm)
                {
                    tabDetails.AddCell(new Phrase(r["ten_cangbien"].ToString(), times_8));
                }

                if (tenkichthuoc.Equals("inch"))
                {
                    tabDetails.AddCell(new Phrase(r["ten_kichthuoc"].ToString(), times_8));
                }
				
				tabDetails.AddCell(new Phrase(r["hscode"].ToString(), times_8));
            }

            doc1.Add(tabDetails);

            PdfPTable tabPColor = new PdfPTable(1);
            PdfPCell cellColor = new PdfPCell(new Phrase("Color References:", times_8));
            cellColor.Border = 0;
            cellColor.PaddingTop = 3f;
            cellColor.PaddingBottom = 3f;
            tabPColor.AddCell(cellColor);
            doc1.Add(tabPColor);

            foreach (DataRow iDis in dtDistinct.Rows)
            {
                List<DataRow> items = new List<DataRow>();
                foreach (DataRow row in dtReferences.Rows)
                {
                    if (row["chungloai"].ToString().Equals(iDis["chungloai"].ToString()))
                    {
                        items.Add(row);
                    }
                }
                PdfPTable tabWrapRef = new PdfPTable(3);
                float[] width_Wrap = new float[] { 0.2f, 0.5f, 1f };
                tabWrapRef.SetWidths(width_Wrap);

                tabWrapRef.AddCell(new Phrase("", times_8));
                String tenTiengAnh = "";
                foreach (DataRow item in items)
                {
                    if (item["chungloai"].ToString().Equals(iDis["chungloai"].ToString()))
                    {
                        tenTiengAnh = item["ta_dai"].ToString();
                    }
                }
                tabWrapRef.AddCell(new Phrase(tenTiengAnh, times_8));

                PdfPTable tabRef = new PdfPTable(4);
                tabRef.DefaultCell.AddElement(new Phrase("", times_8));
                foreach (DataRow item in items)
                {
                    PdfPTable tabDColor = new PdfPTable(1);
                    tabDColor.AddCell(new Phrase("Color " + item["codemau"].ToString(), times_8));
                    tabDColor.AddCell(new Phrase(item["mau"].ToString(), times_8));

                    try
                    {
                        PdfPCell cellDColorImg = new PdfPCell();
                        cellDColorImg.Border = 1;
                        cellDColorImg.AddElement(Image.GetInstance(productImagePath + item["url"].ToString()));
                        tabDColor.AddCell(cellDColorImg);
                    }
                    catch
                    {

                    }
                    PdfPCell cellDColor = new PdfPCell(tabDColor);
                    cellDColor.Padding = 0f;

                    foreach (PdfPRow r in tabDColor.Rows)
                    {
                        foreach (PdfPCell c in r.GetCells())
                        {
                            c.Border = 0;
                        }
                    }

                    tabRef.AddCell(cellDColor);
                }

                int i = 0;
                while (true)
                {
                    if (Math.Ceiling((items.Count + i) % 4.0) != 0)
                    {
                        tabRef.AddCell(new Phrase("", times_8));
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                PdfPCell cellWrapRef = new PdfPCell(tabRef);
                cellWrapRef.Padding = 0f;
                tabWrapRef.AddCell(cellWrapRef);



                foreach (PdfPRow r in tabRef.Rows)
                {
                    foreach (PdfPCell c in r.GetCells())
                    {
                        c.Border = 0;
                    }
                }

                foreach (PdfPRow r in tabWrapRef.Rows)
                {
                    foreach (PdfPCell c in r.GetCells())
                    {
                        c.Border = 0;
                    }
                }

                doc1.Add(tabWrapRef);
            }

            PdfPTable tabFooter = new PdfPTable(3);
            float[] widths_footer = new float[] { 0.2f, 0.2f, 1f };
            tabFooter.SetWidths(widths_footer);
            tabFooter.AddCell(new Phrase("Remark:", times_8));
            PdfPCell cellRemark = new PdfPCell((new Phrase("All above FOB prices is requested for FCL container - Packing as our standard offered above - Labelling in black & white color – including Bill of lading & seal fee and THC charge from the shipping line/forwarder.", times_8)));
            cellRemark.Colspan = 2;
            tabFooter.AddCell(cellRemark);


            tabFooter.AddCell(new Phrase("", times_8));
            PdfPCell cellOther = new PdfPCell((new Phrase("Other costs if occurring, will be charged to customers.", times_8)));
            cellOther.Colspan = 2;
            tabFooter.AddCell(cellOther);

            tabFooter.AddCell(new Phrase("Number of items for:", times_8));
            tabFooter.AddCell(new Phrase("20' cont.", times_8));
            tabFooter.AddCell(new Phrase("10 different items (max.)", times_8));

            tabFooter.AddCell(new Phrase("", times_8));
            tabFooter.AddCell(new Phrase("40' cont.", times_8));
            tabFooter.AddCell(new Phrase("20 different items (max.)", times_8));

            tabFooter.AddCell(new Phrase("MOQ/item/color:", times_8));
            PdfPCell cellItemColor = new PdfPCell((new Phrase(dtDetails.Rows[0]["moq_item_color"].ToString(), times_8)));
            cellItemColor.Colspan = 2;
            tabFooter.AddCell(cellItemColor);

            tabFooter.AddCell(new Phrase("Shipmenttime:", times_8));
            PdfPCell cellShippmenttime = new PdfPCell((new Phrase("75 days after receipt of original L/C or 30% deposit", times_8)));
            cellShippmenttime.Colspan = 2;
            tabFooter.AddCell(cellShippmenttime);

            tabFooter.AddCell(new Phrase("Port of loading:", times_8));
            PdfPCell cellPortLoading = new PdfPCell((new Phrase(dtDetails.Rows[0]["ten_cangbien"].ToString(), times_8)));
            cellPortLoading.Colspan = 2;
            tabFooter.AddCell(cellPortLoading);

            tabFooter.AddCell(new Phrase("Payment term:", times_8));
            PdfPCell cellPayment = new PdfPCell((new Phrase("TTR 30% deposit, 70% after shipment, or L/C at sight ( value over USD 20,000)", times_8)));
            cellPayment.Colspan = 2;
            tabFooter.AddCell(cellPayment);

            tabFooter.AddCell(new Phrase("Bank Details:", times_8));
            PdfPCell cellBank01 = new PdfPCell((new Phrase("1. In favor ANCO COMPANY LTD. Account no. : 007.137.0321634 At bank: Joint Stock Commercial Bank For Foreign Trade of Vietnam (VIETCOMBANK),  29 Ben Chuong Duong St., Dist. 1, Hochiminh City, Vietnam. Tel: (84-28) 38297245, Fax: (84-28) 38297228, Swift code: BFTVVNVX007.", times_8)));
            cellBank01.Colspan = 2;
            tabFooter.AddCell(cellBank01);

            tabFooter.AddCell(new Phrase("", times_8));
            PdfPCell cellBank02 = new PdfPCell((new Phrase("2. In favor of ANCO COMPANY LTD. Account no. : 001-395698-101 (USD). At bank: HSBC   Hochiminh City Branch , Metropolitan Building, 235 Dong Khoi St., Dist. 1, Tel.: (84-028) 35203051, Fax: (84-028) 38230530, Swift code: HSBCVNVXHochiminh City, Vietnam. ", times_8)));
            cellBank02.Colspan = 2;
            tabFooter.AddCell(cellBank02);

            tabFooter.AddCell(new Phrase("Validity  until:", times_8));
            PdfPCell cellVali = new PdfPCell((new Phrase(DateTime.Parse(dtDetails.Rows[0]["ngayhethan"].ToString()).ToString("dd/MMM/yyyy"), times_8)));
            cellVali.Colspan = 2;
            tabFooter.AddCell(cellVali);

            foreach (PdfPRow r in tabFooter.Rows)
            {
                foreach (PdfPCell c in r.GetCells())
                {
                    try
                    {
                        c.Border = 0;
                    }
                    catch
                    {
                    }

                }
            }
            doc1.Add(tabFooter);
            doc1.Close();
        }
        catch (Exception e)
        {
            doc1.Close();
            throw e;
        }
    }

    public String LogoPath
    {
        get { return logoPath; }
        set { logoPath = value; }
    }

    public String ProductImagePath
    {
        get { return productImagePath; }
        set { productImagePath = value; }
    }

     public String C_baogia_id
    {
        get { return c_baogia_id; }
        set { c_baogia_id = value; }
    }

     public BaoGiaPdf(String c_baogia_id, String logoPath, String productImagePath)
     {
         this.c_baogia_id = c_baogia_id;
         this.logoPath = logoPath;
         this.productImagePath = productImagePath;
     }

     public BaoGiaPdf()
     {
         //
         // TODO: Add constructor logic here
         //
     }
}
