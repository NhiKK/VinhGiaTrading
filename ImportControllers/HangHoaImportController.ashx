<%@ WebHandler Language="C#" Class="HangHoaImportController" %>

using System;
using System.Web;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;

public class HangHoaImportController : IHttpHandler
{

    public void ProcessRequest (HttpContext context) {
        String filename = context.Request.QueryString["filename"];
        String file = context.Server.MapPath("../FileUpload/" + filename);
        int j = file.LastIndexOf(".");
        if(file.Substring(j) != ".xls") {
            file = ConvertXLSXToXLS.ConvertWorkbookXSSFToHSSF(file);
        }
        Workbook wb = Workbook.Load(file);
        Worksheet ws = wb.Worksheets[0];
        this.NewFromCellCollection(context, ws.Cells);
    }

    public void NewFromCellCollection(HttpContext context, CellCollection cellCollection)
    {
        for (int i = 1; i < cellCollection.Rows.Count; i++)
        //for (int i = 1; i < 10; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String msg = "";
                msg += String.Format("<div>--- Import Dòng {0} ---</div>", i + 1);
                bool next = true;
                String cellMaHH = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                if(cellMaHH != null & cellMaHH != "")
                {
                    cellMaHH = cellMaHH.Replace(" ","");
                    if(cellMaHH.Length != 14) {
                        next = false;
                        msg += "<div style=\"color:red\">Mã hàng chỉ nên có đúng 14 ký tự.</div>";
                    }
                }
                else {
                    next = false;
                    msg += "<div style=\"color:red\">Mã hàng không thể rỗng.</div>";
                }

                if(next == true) {
                    String cellMaKD = row.GetCell(1).Value == null ? String.Empty : row.GetCell(1).Value.ToString();
                    String cellMaCN = row.GetCell(2).Value == null ? String.Empty : row.GetCell(2).Value.ToString();
                    String cellMaDVT = row.GetCell(3).Value == null ? String.Empty : row.GetCell(3).Value.ToString();
                    decimal cellL = row.GetCell(4).Value == null ? 0 : decimal.Parse(row.GetCell(4).Value.ToString());
                    decimal cellW = row.GetCell(5).Value == null ? 0 : decimal.Parse(row.GetCell(5).Value.ToString());
                    decimal cellH = row.GetCell(6).Value == null ? 0 : decimal.Parse(row.GetCell(6).Value.ToString());
                    decimal cellTrongLuong = row.GetCell(7).Value == null ? 0 : decimal.Parse(row.GetCell(7).Value.ToString());
                    decimal cellDienTich = row.GetCell(8).Value == null ? 0 : decimal.Parse(row.GetCell(8).Value.ToString());
                    decimal cellTheTich = row.GetCell(9).Value == null ? 0 : decimal.Parse(row.GetCell(9).Value.ToString());
                    String cellMaNhomNangLuc = row.GetCell(10).Value == null ? String.Empty : row.GetCell(10).Value.ToString();
                    String cellMaCangBien = row.GetCell(11).Value == null ? String.Empty : row.GetCell(11).Value.ToString();
                    //String cellMaDongGoi = row.GetCell(11).Value == null ? String.Empty : row.GetCell(11).Value.ToString();
                    String cellNhaCungUng = row.GetCell(12).Value == null ? String.Empty : row.GetCell(12).Value.ToString();
                    String cellHSCode = row.GetCell(13).Value == null ? String.Empty : row.GetCell(13).Value.ToString();
                    String cellTrangThai = row.GetCell(14).Value == null ? String.Empty : row.GetCell(14).Value.ToString();
                    String cellChucNangSP = row.GetCell(15).Value == null ? String.Empty : row.GetCell(15).Value.ToString();
                    String cellNgayXacNhan = row.GetCell(16).Value == null ? String.Empty : row.GetCell(16).Value.ToString();
                    String cellTinhTrang = row.GetCell(17).Value == null ? String.Empty : row.GetCell(17).Value.ToString();
                    String cellHinhThucBan = row.GetCell(18).Value == null ? String.Empty : row.GetCell(18).Value.ToString();
                    String cellGhiChu = row.GetCell(19).Value == null ? String.Empty : row.GetCell(19).Value.ToString();
                    md_kieudang kd = ImportUtils.getKieuDang(cellMaKD);
                    md_chucnang cn = ImportUtils.getChucNang(cellMaCN);
                    md_donvitinhsanpham dvt = ImportUtils.getDonViTinhSanPham(cellMaDVT);
                    md_nhomnangluc nnl = ImportUtils.getNhomNangLuc(cellMaHH.Substring(0, 2), cellMaNhomNangLuc);
                    md_cangbien cb = ImportUtils.getCangBien(cellMaCangBien);
                    //md_donggoi dg = ImportUtils.getDongGoi(cellMaDongGoi);
                    md_doitackinhdoanh dtkd = ImportUtils.getDoiTac(cellNhaCungUng);

                    md_hscode hscode = ImportUtils.getHSCode(cellHSCode);

                    var tinhTrangHH = ImportUtils.getDanhMucHH(cellTinhTrang);

                    string chungloai = cellMaHH.Substring(0, 2);
                    string detai = cellMaHH.Substring(6, 2);
                    string mausac = cellMaHH.Substring(12, 2);
                    md_chungloai cl = ImportUtils.getChungLoai(chungloai);
                    md_nhomchungloai ncl = ImportUtils.getNhomChungLoai(cellChucNangSP);
                    md_mausac dtai_ms = ImportUtils.getMauSac(mausac, chungloai, detai);




                    switch (cellTrangThai)
                    {
                        case "SOANTHAO":
                            next = true;
                            break;
                        default:
                            next = false;
                            msg += String.Format("<div style=\"color:red\">Trạng thái không đúng. {0} </div>", cellTrangThai);
                            break;
                    }



                    if (hscode == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy mã hscode {0} </div>", cellHSCode);
                    }

                    if (kd == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy mã kiểu dáng {0} </div>", cellMaKD);
                    }

                    if (cn == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy mã chức năng {0} </div>", cellMaCN);
                    }

                    if (dvt == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy mã đơn vi tín sản phẩm {0} </div>", cellMaDVT);
                    }

                    if (nnl == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy mã nhóm năng lực {0} </div>", cellMaHH.Substring(0, 2) + " " + cellMaNhomNangLuc);
                    }

                    if (cb == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy mã cảng biển {0} </div>", cellMaCangBien);
                    }

                    //if (dg == null)
                    //{
                    //    next = false;
                    //    msg += String.Format("<div style=\"color:red\">Không tìm thấy mã đóngg gói {0} </div>", cellMaDongGoi);
                    //}

                    if (dtkd == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy mã nhà cung ứng {0} </div>", cellNhaCungUng);
                    }

                    if (cl == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy mã loại hàng hóa {0} </div>", cellNhaCungUng);
                    }

                    if (dtai_ms == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy chủng loại \"{2}\" đề tài \"{0}\" màu sắc \"{1}\"</div>", detai, mausac, chungloai);
                    }

                    if(ncl == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy chức năng sản phẩm \"{0}\"</div>", cellChucNangSP);
                    }

                    if(tinhTrangHH == null)
                    {
                        next = false;
                        msg += String.Format("<div style=\"color:red\">Không tìm thấy tình trạng hàng hóa \"{0}\"</div>", cellTinhTrang);
                    }

                    if (next)
                    {
                        // Import san pham
                        md_sanpham sp = ImportUtils.getSanPham(cellMaHH);
                        HangHoaImport import = new HangHoaImport(
                            cellMaHH,
                            cl.md_chungloai_id,
                            kd.md_kieudang_id,
                            cn.md_chucnang_id,
                            dvt.md_donvitinhsanpham_id, cellL, cellW, cellH, cellTrongLuong, cellDienTich, cellTheTich, nnl.md_nhomnangluc_id,
                            cb.md_cangbien_id, dtkd.md_doitackinhdoanh_id, hscode.md_hscode_id,
                            cellTrangThai, cellChucNangSP, cellNgayXacNhan, tinhTrangHH.md_danhmuchanghoa_id, cellHinhThucBan, cellGhiChu);

                        if (sp == null)
                        {
                            import.Import();
                            msg += String.Format("<div style=\"color:green\">Đã tạo một sản phẩm mới!</div>");
                        }
                        else {
                            import.Update();
                            msg += String.Format("<div style=\"color:green\">Cập nhật sản phẩm thành công!</div>");
                        }

                        //// Import dong goi san pham
                        //md_donggoisanpham dgsp = ImportUtils.getDongGoiSanPham(cellMaDongGoi, cellMaHH);
                        //DonGoiSanPhamImport import2 = new DonGoiSanPhamImport(ImportUtils.getDongGoi(cellMaDongGoi).md_donggoi_id, ImportUtils.getSanPham(cellMaHH).md_sanpham_id);

                        //if (dgsp == null)
                        //{
                        //    import2.Import();
                        //    msg += String.Format("<div style=\"color:green\">Đã tạo một đóng gói cho sản phẩm!</div>");
                        //}
                        //else {
                        //    import2.Update();
                        //    msg += String.Format("<div style=\"color:green\">Cập nhật đóng gói sản phẩm thành công!</div>");
                        //}

                        // Import Cang xuat hang
                        md_cangxuathang cxh = ImportUtils.getCangXuatHang(cellMaHH, cellMaCangBien);
                        CangXuatHangImport import3 = new CangXuatHangImport(ImportUtils.getSanPham(cellMaHH).md_sanpham_id, ImportUtils.getCangBien(cellMaCangBien).md_cangbien_id);

                        if (cxh == null)
                        {
                            import3.Import();
                            msg += String.Format("<div style=\"color:green\">Đã tạo một cảng xuất hàng mới!</div>");
                        }
                        else
                        {
                            import3.Update();
                            msg += String.Format("<div style=\"color:green\">Cập nhật cảng xuất hàng thành công!</div>");
                        }
                    }
                }
                msg += String.Format("<div>--- Kết thúc dòng {0} ---</div>", i + 1);
                context.Response.Write(msg);

            }
            catch (Exception ex)
            {
                context.Response.Write(String.Format("<div style=\"color:red\">Dòng {0} xảy ra lỗi {1}</div>", i + 1, ex.Message));
            }
        }

        for (int i = 1; i < cellCollection.Rows.Count; i++)
        //for (int i = 1; i < 10; i++)
        {
            try
            {
                Row row = cellCollection.Rows[i];
                String cellMaHH = row.GetCell(0).Value == null ? String.Empty : row.GetCell(0).Value.ToString();
                decimal cellL = row.GetCell(4).Value == null ? 0 : decimal.Parse(row.GetCell(4).Value.ToString());
                md_sanpham sp = ImportUtils.getSanPham(cellMaHH);
                if (sp != null)
                {
                    String sql = "update md_sanpham set l_cm = " + cellL + " where md_sanpham_id = '" + sp.md_sanpham_id + "' ";
                    mdbc.ExcuteNonQuery(sql);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(String.Format("<div style=\"color:red\">Dòng {0} xảy ra lỗi {1}</div>", i + 1, ex.Message));
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}