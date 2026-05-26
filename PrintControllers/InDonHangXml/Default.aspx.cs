using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PrintControllers_InDonHangXml_Default : System.Web.UI.Page
{
    LinqDBDataContext db = new LinqDBDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            String c_danhsachdathang_id = Request.QueryString["c_danhsachdathang_id"];
        //    c_donhang dh = (from donhang in db.c_donhangs
        //                    join dsdh in db.c_danhsachdathangs
        //                    on donhang.c_donhang_id equals dsdh.c_donhang_id
        //                    where dsdh.c_danhsachdathang_id.Equals(c_danhsachdathang_id)
        //                    select donhang).FirstOrDefault();
        //    if (dh == null)
        //    {
        //        Response.Write("Đơn hàng không tồn tại! Có thể đã được xóa trước đó!");
        //    }
        //    else
        //    {
                String sql = @"select 
                                    1 as c_donhang_id, dh.sochungtu, dh.ngaylap, 49 as nguoilap, 455 as diachi
                                    , cb.ma_cangbien, (select bg.sobaogia from c_baogia bg where c_baogia_id = dh.c_baogia_id) as soorder
                                    , dtkd.diachi, dh.discount, dh.shipmenttime
                                    , (pmt.ten_paymentterm + ' In favor of VINH GIA COMPANY LIMITED Account no. : ' + ngh.thongtin) as paymentterm
                                    , dh.shipmentdate, dh.portdischarge, (dh.sochungtu + ' / '+ dsdh.sochungtu) as payer, 1 as ten_kichthuoc, 1 as ma_iso, 7 as ten_trongluong
                                    , 49 as nguoitao1, dsdh.ngaytao as ngaytao1, 1 as md_trangthai_id
                                    -- Dong don hang
                                    , 1 as c_dongdonhang_id, sp.ma_sanpham, ddsdh.ma_sanpham_khach, ddsdh.sothutu, ddsdh.sl_dathang
                                    , ddsdh.gianhap 
                                    , ddsdh.mota
                                    , '1' as id_donggoi
                                    , '0' as id_mix
                                    , ddsdh.sl_inner, ddsdh.l1, ddsdh.w1, ddsdh.h1
                                    , ddsdh.sl_outer, ddsdh.l2, ddsdh.w2, ddsdh.h2, ddsdh.v2
                                    , ddsdh.sl_cont, '0' as loaibia, 49 as nguoitao, ddsdh.ngaytao
                                from 
                                    c_donhang dh, md_cangbien cb, md_paymentterm pmt
                                    , md_nganhang ngh, md_kichthuoc kt, md_dongtien dt, md_trongluong tl
                                    , md_sanpham sp, md_doitackinhdoanh dtkd
                                    , c_danhsachdathang dsdh, c_dongdsdh ddsdh
                                where
                                    dh.md_cangbien_id = cb.md_cangbien_id
                                    AND pmt.md_paymentterm_id = dh.md_paymentterm_id
                                    AND ngh.md_nganhang_id = dh.md_nganhang_id
                                    AND dh.md_kichthuoc_id = kt.md_kichthuoc_id
                                    AND dh.md_dongtien_id = dt.md_dongtien_id
                                    AND dh.md_trongluong_id = tl.md_trongluong_id
                                    AND ddsdh.md_sanpham_id = sp.md_sanpham_id
                                    AND dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
                                    AND dsdh.c_donhang_id = dh.c_donhang_id
                                    AND dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
                                    AND dsdh.c_danhsachdathang_id = @c_danhsachdathang_id";
                DataTable dt = mdbc.GetData(sql, "@c_danhsachdathang_id", c_danhsachdathang_id);

                String xml = "<?xml version=\"1.0\" standalone=\"yes\"?>\r\n";
                xml += "<PO>\r\n";
                xml += "\t<XemHD_Alls>\r\n";
                //---
                xml += String.Format("\t\t\t<IDHopDong>{0}</IDHopDong>\r\n", 1);
                xml += String.Format("\t\t\t<SoHD>{0}</SoHD>\r\n", dt.Rows[0]["sochungtu"].ToString());
                xml += String.Format("\t\t\t<NgayLap>{0}</NgayLap>\r\n", DateTime.Parse(dt.Rows[0]["ngaylap"].ToString()).ToString("MMM dd yyyy hh:mmtt"));
                xml += String.Format("\t\t\t<IDNguoiLap>{0}</IDNguoiLap>\r\n", dt.Rows[0]["nguoilap"].ToString());
                xml += String.Format("\t\t\t<NoiNhan>{0}</NoiNhan>\r\n", dt.Rows[0]["diachi"].ToString());
                xml += String.Format("\t\t\t<SoOrder>{0}</SoOrder>\r\n", dt.Rows[0]["soorder"].ToString());
                xml += String.Format("\t\t\t<PORT_LOADING>{0}</PORT_LOADING>\r\n", dt.Rows[0]["ma_cangbien"].ToString());
                xml += String.Format("\t\t\t<DISCOUNT>{0}</DISCOUNT>\r\n", dt.Rows[0]["discount"].ToString());
                xml += String.Format("\t\t\t<SHIPMENTDATE>{0}</SHIPMENTDATE>\r\n", DateTime.Parse(dt.Rows[0]["shipmentdate"].ToString()).ToString("MMM dd yyyy hh:mmtt"));
                xml += String.Format("\t\t\t<PAYMENTTERM>{0}</PAYMENTTERM>\r\n", dt.Rows[0]["paymentterm"].ToString());
                xml += String.Format("\t\t\t<HanGiaoHang>{0}</HanGiaoHang>\r\n", DateTime.Parse(dt.Rows[0]["shipmenttime"].ToString()).ToString("MMM dd yyyy hh:mmtt"));
                xml += String.Format("\t\t\t<PortDischarge xml:space=\"preserve\">{0}</PortDischarge>\r\n", dt.Rows[0]["portdischarge"].ToString());
                xml += String.Format("\t\t\t<Payer xml:space=\"preserve\">{0}</Payer>\r\n", dt.Rows[0]["payer"].ToString());
                xml += String.Format("\t\t\t<IdKichThuoc>{0}</IdKichThuoc>\r\n", dt.Rows[0]["ten_kichthuoc"].ToString());
                xml += String.Format("\t\t\t<IdTienTe>{0}</IdTienTe>\r\n", dt.Rows[0]["ma_iso"].ToString());
                xml += String.Format("\t\t\t<IdTrongLuong>{0}</IdTrongLuong>\r\n", dt.Rows[0]["ten_trongluong"].ToString());
                xml += String.Format("\t\t\t<nguoiTao>{0}</nguoiTao>\r\n", dt.Rows[0]["nguoitao1"].ToString());
                xml += String.Format("\t\t\t<NgayTao>{0}</NgayTao>\r\n", DateTime.Parse(dt.Rows[0]["ngaytao1"].ToString()).ToString("MMM dd yyyy hh:mmtt"));
                xml += String.Format("\t\t\t<TrangThai>{0}</TrangThai>\r\n", dt.Rows[0]["md_trangthai_id"].ToString());
                //---
                xml += "\t</XemHD_Alls>";

                foreach (DataRow r in dt.Rows)
                {
                    xml += "\t<XemHD_Alls_Items>\r\n";

                    xml += String.Format("\t\t\t<ID>{0}</ID>\r\n", 1);
                    xml += String.Format("\t\t\t<IDHopDong>{0}</IDHopDong>\r\n", 1);
                    xml += String.Format("\t\t\t<MaHH>{0}</MaHH>\r\n", r["ma_sanpham"].ToString());
                    xml += String.Format("\t\t\t<MaHH_Khach>{0}</MaHH_Khach>\r\n", r["ma_sanpham_khach"].ToString());
                    xml += String.Format("\t\t\t<SoTT>{0}</SoTT>\r\n", r["sothutu"].ToString());
                    xml += String.Format("\t\t\t<SoLuong>{0}</SoLuong>\r\n", r["sl_dathang"].ToString());
                    xml += String.Format("\t\t\t<Gia>{0}</Gia>\r\n", r["gianhap"].ToString());
                    xml += String.Format("\t\t\t<GhiChu>{0}</GhiChu>\r\n", r["mota"].ToString());
                    xml += String.Format("\t\t\t<IdDongGoi>{0}</IdDongGoi>\r\n", r["id_donggoi"].ToString());
                    xml += String.Format("\t\t\t<IdMix>{0}</IdMix>\r\n", r["id_mix"].ToString());
                    xml += String.Format("\t\t\t<SLInner>{0}</SLInner>\r\n", r["sl_inner"].ToString());
                    xml += String.Format("\t\t\t<L1>{0}</L1>\r\n", r["l1"].ToString() == "" ? "0" : r["l1"].ToString());
                    xml += String.Format("\t\t\t<W1>{0}</W1>\r\n", r["w1"].ToString() == "" ? "0" : r["w1"].ToString());
                    xml += String.Format("\t\t\t<H1>{0}</H1>\r\n", r["h1"].ToString() == "" ? "0" : r["h1"].ToString());
                    xml += String.Format("\t\t\t<SlOuter>{0}</SlOuter>\r\n", r["sl_outer"].ToString() == "" ? "0" : r["sl_outer"].ToString());
                    xml += String.Format("\t\t\t<L2>{0}</L2>\r\n", r["l2"].ToString() == "" ? "0" : r["l2"].ToString());
                    xml += String.Format("\t\t\t<H2>{0}</H2>\r\n", r["w2"].ToString() == "" ? "0" : r["w2"].ToString());
                    xml += String.Format("\t\t\t<W2>{0}</W2>\r\n", r["h2"].ToString() == "" ? "0" : r["h2"].ToString());
                    xml += String.Format("\t\t\t<V2>{0}</V2>\r\n", r["v2"].ToString() == "" ? "0" : r["v2"].ToString());
                    xml += String.Format("\t\t\t<SlCont>{0}</SlCont>\r\n", r["sl_cont"].ToString() == "" ? "0" : r["sl_cont"].ToString());
                    xml += String.Format("\t\t\t<LoaiBia>{0}</LoaiBia>\r\n", r["loaibia"].ToString());
                    xml += String.Format("\t\t\t<nguoitao>{0}</nguoitao>\r\n",r["nguoitao"].ToString());
                    xml += String.Format("\t\t\t<ngaytao>{0}</ngaytao>\r\n", DateTime.Parse(r["ngaytao"].ToString()).ToString("MMM dd yyyy hh:mmtt"));

                    xml += "\t</XemHD_Alls_Items>\r\n";
                }
                xml += "</PO>";

                xml = xml.Replace("&", "&amp;");

                Response.ContentType = "text/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Don-Hang-" + DateTime.Now.ToString("HH-mm-ss dd/MM/yyyy") + ".xml");
                Response.Write(xml);
                Response.End();
        //    }

        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
    }
}
