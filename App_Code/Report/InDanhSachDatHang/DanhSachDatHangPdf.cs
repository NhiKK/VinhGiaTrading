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
public class DanhSachDatHangPdf
{
    private String c_danhsachdathang_id, logoPath, productImagePath;
    LinqDBDataContext db = new LinqDBDataContext();

    public void CreatePdfPI(String fileName, String font)
    {
        try
        {
            String selTotal = String.Format(@"select SUM(ddsdh.sl_dathang * ddsdh.gianhap) as tongtien 
                                    FROM c_danhsachdathang dsdh, c_dongdsdh ddsdh 
                                    WHERE dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
                                    AND dsdh.c_danhsachdathang_id = '{0}'", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id);

            Nullable<decimal> discount = db.c_danhsachdathangs.FirstOrDefault(d => d.c_danhsachdathang_id.Equals(c_danhsachdathang_id)).discount;
            decimal total = (decimal)mdbc.ExecuteScalar(selTotal);

            Nullable<decimal> totalDiscount = total * (discount == null ? 0 : discount.Value) / 100;
            Nullable<decimal> phitang = (from t in db.c_phidathangs where t.isphicong.Equals(true) && t.c_danhsachdathang_id.Equals(c_danhsachdathang_id) select t.sotien).Sum();
            Nullable<decimal> phigiam = (from t in db.c_phidathangs where t.isphicong.Equals(false) && t.c_danhsachdathang_id.Equals(c_danhsachdathang_id) select t.sotien).Sum();
            Nullable<decimal> totalUSD = total - totalDiscount + (phitang == null ? 0 : phitang.Value) - (phigiam == null ? 0 : phigiam.Value);

            var varTang = db.c_phidathangs.FirstOrDefault(tg => tg.c_danhsachdathang_id.Equals(c_danhsachdathang_id) && tg.isphicong.Equals(true));
            var varGiam = db.c_phidathangs.FirstOrDefault(gi => gi.c_danhsachdathang_id.Equals(c_danhsachdathang_id) && gi.isphicong.Equals(false));

            String dgTang, dgGiam;
            dgTang = varTang == null ? "" : varTang.mota;
            dgGiam = varGiam == null ? "" : varGiam.mota;

            


            String select = String.Format(@"
                        select 
	                        dsdh.sochungtu, dh.sochungtu as so_po, dh.customer_order_no, dsdh.ngaylap, dsdh.hangiaohang_po, dtkd.ten_dtkd, dtkd.tel, dtkd.fax
	                        , sp.ma_sanpham, ddsdh.ma_sanpham_khach, ddsdh.mota_tiengviet, dvt.ma_edi
                            , ddsdh.sl_dathang, ddsdh.gianhap, dsdh.huongdanlamhang, ddsdh.huongdan_dathang
	                        , dsdh.diachigiaohang, dh.sl_cont, dh.loai_cont, dsdh.discount as discountdecimal, {2} as discount,  N'{1}' as money 
                            , N'{3}' as diengiaitang, N'{4}' as diengiaigiam
                            , (case ddsdh.sl_inner when 0 then ' ' else (cast(ddsdh.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner)) end) as dginner
                            , (cast(ddsdh.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer)) as dgouter
                            , (select hoten from nhanvien where manv = dsdh.nguoitao) as nguoitao
                            ,(case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) + '.jpg' else SUBSTRING(sp.ma_sanpham,0,12) + '.jpg' end) as pic
							, ddsdh.v2
                            from 
	                        c_danhsachdathang dsdh, md_doitackinhdoanh dtkd, md_sanpham sp
	                        , c_dongdsdh ddsdh, md_donvitinhsanpham dvt, c_donhang dh, md_donggoi dg
                        where
	                        dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	                        AND dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
	                        AND ddsdh.md_sanpham_id = sp.md_sanpham_id
                            AND dsdh.c_donhang_id = dh.c_donhang_id
	                        AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
                            AND ddsdh.md_donggoi_id = dg.md_donggoi_id
                            AND dsdh.c_danhsachdathang_id = N'{0}' order by sp.ma_sanpham asc", c_danhsachdathang_id == null ? "" : c_danhsachdathang_id, MoneyToWord.ConvertMoneyToTextVND(totalUSD == null ? 0 : totalUSD.Value), totalDiscount == null ? 0 : totalDiscount.Value, dgTang.Replace("'","''"), dgGiam.Replace("'","''"));

            DataTable dt = mdbc.GetData(select);

            if (dt.Rows.Count != 0)
            {
                this.CreatePDF(fileName, dt, phitang, phitang, font);
            }
            else
            {
                throw new Exception("<h3>Đơn hàng không có dữ liệu</h3>");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void CreatePDF(String fileName, DataTable dtDetails, Nullable<decimal> phitang, Nullable<decimal> phigiam, String font)
    {
        var doc1 = new Document(PageSize.A4_LANDSCAPE.Rotate(), 5F, 5F, 5F, 5F);
        PdfWriter.GetInstance(doc1, new FileStream(fileName, FileMode.Create));
        doc1.Open();
        try
        {
            BaseFont bfTimes = BaseFont.CreateFont(font, BaseFont.IDENTITY_H, true);
            Font times_8 = new Font(bfTimes, 8, Font.NORMAL, BaseColor.BLACK);
            Font times_10 = new Font(bfTimes, 10, Font.BOLD, BaseColor.BLACK);
            Font times_13 = new Font(bfTimes, 13, Font.BOLD, BaseColor.BLACK);
            Font times_15 = new Font(bfTimes, 15, Font.BOLD, BaseColor.BLACK);

            Paragraph pCompany = new Paragraph("VINH GIA COMPANY LIMITED", times_13);
            pCompany.Alignment = 1;

            Paragraph pAddress = new Paragraph("Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam", times_8);
            pAddress.Alignment = 1;

            Paragraph pInfo = new Paragraph("Tel: (84-235) 3567393   Fax: (84-235) 3567494", times_8);
            pInfo.Alignment = 1;

            Paragraph pTitle = new Paragraph("ĐƠN ĐẶT HÀNG", times_15);
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

            PdfPCell cellImg = new PdfPCell();
            cellImg.Border = 0;
            cellImg.AddElement(Image.GetInstance(logoPath));

            tabHeader.AddCell(cellImg);
            tabHeader.AddCell(cellInformation);
            doc1.Add(tabHeader);
            // # table header

            PdfPTable tabCustomer = new PdfPTable(5);
            float[] widths_customer = new float[] { 0.5f, 0.5f, 1, 0.5f, 0.5f };
            tabCustomer.SetWidths(widths_customer);

            tabCustomer.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(" ", times_8)));


            tabCustomer.AddCell(new PdfPCell(new Phrase("Số chứng từ:", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["sochungtu"].ToString(), times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase("Ngày lập:", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(DateTime.Parse(dtDetails.Rows[0]["ngaylap"].ToString()).ToString("dd/MM/yyyy"), times_8)));

            tabCustomer.AddCell(new PdfPCell(new Phrase("Theo P/O số:", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["so_po"].ToString(), times_8)));

            tabCustomer.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase("Customer Order No", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["customer_order_no"].ToString(), times_8)));
																				   
																																				   

            PdfPCell cellSendTo = new PdfPCell(new Phrase("Công ty AnCơ kính đề nghị Quý Công ty/Đơn vị: " + dtDetails.Rows[0]["ten_dtkd"].ToString(), times_8));
            cellSendTo.Colspan = 5;
            tabCustomer.AddCell(cellSendTo);

            tabCustomer.AddCell(new PdfPCell(new Phrase("Điện thoại:", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["tel"].ToString(), times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase("Fax:", times_8)));
            tabCustomer.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["fax"].ToString(), times_8)));

            PdfPCell cellProvideDetails = new PdfPCell(new Phrase("Cung cấp cho công ty chúng tôi các mặt hàng với chi tiết:", times_8));
            cellProvideDetails.Colspan = 5;
            tabCustomer.AddCell(cellProvideDetails);

            foreach (PdfPRow r in tabCustomer.Rows)
            {
                foreach (PdfPCell c in r.GetCells())
                {
                    try { c.Border = 0; }
                    catch { }
                }
            }
            doc1.Add(tabCustomer);

            PdfPTable tabDetails = new PdfPTable(12);
            float[] widths_details = new float[] { 0.2f, 0.5f, 0.5f, 0.3f, 1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.3f, 0.5f, 0.5f };
            tabDetails.SetWidths(widths_details);

            int tongsoluong = 0;
            decimal tongtien = 0;
            decimal tongcbm = 0;

            tabDetails.AddCell(new PdfPCell(new Phrase("STT", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Mã số", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Hình ảnh", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Mã sản phẩm khách", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Mô tả", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("ĐVT", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Cbm", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("SL", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Giá", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Thành tiền", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Đóng gói inner", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Đóng gói outer", times_8)));

            foreach (PdfPCell c in tabDetails.GetRow(0).GetCells())
            {
                c.HorizontalAlignment = 1;
                c.VerticalAlignment = 1;
            }

            int rowNum = 1;
            foreach (DataRow item in dtDetails.Rows)
            {
                PdfPCell cRowNum = new PdfPCell(new Phrase(rowNum.ToString(), times_8));
                cRowNum.HorizontalAlignment = 2;
                cRowNum.VerticalAlignment = 1;
                tabDetails.AddCell(cRowNum);

                PdfPCell cMaSanPham = new PdfPCell(new Phrase(item["ma_sanpham"].ToString(), times_8));
                cMaSanPham.HorizontalAlignment = 1;
                cMaSanPham.VerticalAlignment = 1;
                tabDetails.AddCell(cMaSanPham);

                try
                {
                    PdfPCell cellDImg = new PdfPCell();
                    cellDImg.Border = 1;
                    cellDImg.AddElement(Image.GetInstance(productImagePath + item["pic"].ToString() + ".jpg"));
                    tabDetails.AddCell(cellDImg);
                }
                catch
                {
					tabDetails.AddCell("");
                }

                PdfPCell cMaSanPhamKhach = new PdfPCell(new Phrase(item["ma_sanpham_khach"].ToString(), times_8));
                cMaSanPhamKhach.HorizontalAlignment = 1;
                cMaSanPhamKhach.VerticalAlignment = 1;
                tabDetails.AddCell(cMaSanPhamKhach);

                PdfPCell cMoTa = new PdfPCell(new Phrase(item["mota_tiengviet"].ToString(), times_8));
                cMoTa.HorizontalAlignment = 0;
                cMoTa.VerticalAlignment = 1;
                tabDetails.AddCell(cMoTa);

                PdfPCell cDVT = new PdfPCell(new Phrase(item["ma_edi"].ToString(), times_8));
                cDVT.HorizontalAlignment = 1;
                cDVT.VerticalAlignment = 1;
                tabDetails.AddCell(cDVT);

                PdfPCell cCBM = new PdfPCell(new Phrase(decimal.Parse(item["v2"].ToString()).ToString("#,##0.000"), times_8));
                cCBM.HorizontalAlignment = 2;
                cCBM.VerticalAlignment = 1;
                tabDetails.AddCell(cCBM);

                PdfPCell cSL = new PdfPCell(new Phrase(decimal.Parse(item["sl_dathang"].ToString()).ToString("#,##0"), times_8));
                cSL.HorizontalAlignment = 2;
                cSL.VerticalAlignment = 1;
                tabDetails.AddCell(cSL);

                PdfPCell cGia = new PdfPCell(new Phrase(decimal.Parse(item["gianhap"].ToString()).ToString("#,##0"), times_8));
                cGia.HorizontalAlignment = 2;
                cGia.VerticalAlignment = 1;
                tabDetails.AddCell(cGia);

                decimal thanhtien = decimal.Parse(item["sl_dathang"].ToString()) * decimal.Parse(item["gianhap"].ToString());

                PdfPCell cThanhTien = new PdfPCell(new Phrase(thanhtien.ToString("#,##0"), times_8));
                cThanhTien.HorizontalAlignment = 2;
                cThanhTien.VerticalAlignment = 1;
                tabDetails.AddCell(cGia);

                PdfPCell cInner = new PdfPCell(new Phrase(item["dginner"].ToString(), times_8));
                cInner.HorizontalAlignment = 1;
                cInner.VerticalAlignment = 1;
                tabDetails.AddCell(cInner);

                PdfPCell cOuter = new PdfPCell(new Phrase(item["dgouter"].ToString(), times_8));
                cOuter.HorizontalAlignment = 1;
                cOuter.VerticalAlignment = 1;
                tabDetails.AddCell(cOuter);

                tongsoluong += int.Parse(item["sl_dathang"].ToString());
                tongcbm += decimal.Parse(item["v2"].ToString());
                tongtien += thanhtien;
                rowNum++;
            }

            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("Tổng cộng:", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));

            PdfPCell cTongCbm = new PdfPCell(new Phrase(tongcbm.ToString("#,##0.000"), times_8));//
            cTongCbm.HorizontalAlignment = 2;



            tabDetails.AddCell(cTongCbm);
            PdfPCell cTongSoLuong = new PdfPCell(new Phrase(tongsoluong.ToString("#,##0"), times_8));//
            cTongSoLuong.HorizontalAlignment = 2;
            tabDetails.AddCell(new PdfPCell(cTongSoLuong));

            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            PdfPCell cTongTien = new PdfPCell(new Phrase(tongtien.ToString("#,##0"), times_8));//
            cTongTien.HorizontalAlignment = 2;

            tabDetails.AddCell(new PdfPCell(cTongTien));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            //

            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("(+):", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            PdfPCell cPhiTang = new PdfPCell(new Phrase(phitang == null ? "0" : phitang.Value.ToString("#,##0"), times_8));//
            cPhiTang.HorizontalAlignment = 2;

            tabDetails.AddCell(new PdfPCell(cPhiTang));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            //

            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("(-):", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            PdfPCell cPhiGiam = new PdfPCell(new Phrase(phigiam == null ? "0" : phigiam.Value.ToString("#,##0"), times_8));//
            cPhiGiam.HorizontalAlignment = 2;

            tabDetails.AddCell(new PdfPCell(cPhiGiam));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));

            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase(String.Format("Discount({0}%):", dtDetails.Rows[0]["discountdecimal"].ToString()), times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));

            PdfPCell cDisCount = new PdfPCell(new Phrase(dtDetails.Rows[0]["discount"].ToString(), times_8));
            cDisCount.HorizontalAlignment = 2;
            tabDetails.AddCell(new PdfPCell(cDisCount));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));

            decimal discount = decimal.Parse(dtDetails.Rows[0]["discount"].ToString());
            decimal tongcongdatru = tongtien + (phitang == null ? 0 : phitang.Value) - (phigiam == null ? 0 : phigiam.Value) - discount;
            PdfPCell cTongTienDaTru = new PdfPCell(new Phrase(tongcongdatru.ToString("#,##0"), times_8));
            cTongTienDaTru.HorizontalAlignment = 2;

            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("Tổng cộng đã trừ:", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("")));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(cTongTienDaTru));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));
            tabDetails.AddCell(new PdfPCell(new Phrase("", times_8)));

            int count = dtDetails.Rows.Count + 1;

            for (int i = count; i < tabDetails.Rows.Count; i++)
            {
                foreach (PdfPCell c in tabDetails.GetRow(i).GetCells())
                {
                    c.Border = 0;
                }
            }
            doc1.Add(tabDetails);

            PdfPTable tabSummary = new PdfPTable(2);
            float[] widths_summary = new float[] { 0.2f, 1f };
            tabSummary.SetWidths(widths_summary);

            tabSummary.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabSummary.AddCell(new PdfPCell(new Phrase(" ", times_8)));

            tabSummary.AddCell(new PdfPCell(new Phrase("Viết bằng chữ:", times_8)));
            tabSummary.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["money"].ToString(), times_8)));

            tabSummary.AddCell(new PdfPCell(new Phrase("Hạn giao hàng:", times_8)));
            tabSummary.AddCell(new PdfPCell(new Phrase(DateTime.Parse(dtDetails.Rows[0]["hangiaohang_po"].ToString()).ToString("dd/MM/yyyy"), times_8)));

            tabSummary.AddCell(new PdfPCell(new Phrase("Địa điểm xuất hàng:", times_8)));
            tabSummary.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["diachigiaohang"].ToString(), times_8)));

            tabSummary.AddCell(new PdfPCell(new Phrase("Hướng dẫn làm hàng:", times_8)));
            tabSummary.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["huongdanlamhang"].ToString(), times_8)));

            tabSummary.AddCell(new PdfPCell(new Phrase("Chú ý:", times_8)));
            tabSummary.AddCell(new PdfPCell(new Phrase("* Khi cơ sở nhận được đơn đặt hàng này vui lòng ký xác nhận và gửi lại.", times_8)));


            foreach (PdfPRow r in tabSummary.Rows)
            {
                foreach (PdfPCell c in r.GetCells())
                {
                    c.Border = 0;
                }
            }
            doc1.Add(tabSummary);

            PdfPTable tabFooter = new PdfPTable(3);
            tabFooter.AddCell(new PdfPCell(new Phrase("Người lập phiếu", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase("Nhà cung ứng", times_8)));

            tabFooter.AddCell(new PdfPCell(new Phrase("Ký và ghi rõ họ tên", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase("Ký và ghi rõ họ tên", times_8)));
            
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));

            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));

            tabFooter.AddCell(new PdfPCell(new Phrase(dtDetails.Rows[0]["nguoitao"].ToString(), times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabFooter.AddCell(new PdfPCell(new Phrase(" ", times_8)));
            tabFooter.GetRow(2).SetExtraHeight(0, 1f);
            tabFooter.GetRow(3).SetExtraHeight(0, 1f);


            foreach (PdfPRow r in tabFooter.Rows)
            {
                foreach (PdfPCell c in r.GetCells())
                {
                    c.Border = 0;
                    c.HorizontalAlignment = 1;
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
        get { return c_danhsachdathang_id; }
        set { c_danhsachdathang_id = value; }
    }

     public DanhSachDatHangPdf(String c_danhsachdathang_id, String logoPath, String productImagePath)
     {
         this.c_danhsachdathang_id = c_danhsachdathang_id;
         this.logoPath = logoPath;
         this.productImagePath = productImagePath;
     }

     public DanhSachDatHangPdf()
     {
         //
         // TODO: Add constructor logic here
         //
     }
}
