<%@ WebHandler Language="C#" Class="ImportController" %>

using System;
using System.Web;
using System.IO;

public class ImportController : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        String fileName = context.Request.QueryString["filename"];
        String tableName = context.Request.QueryString["table"];
        String filePhysical = context.Server.MapPath("../FileUpload/" + fileName);
        bool next = true;

        if (fileName.Equals(""))
        {
            next = false;
            context.Response.Write("Chưa chọn tập tin cần import.");
        }

        if (tableName.Equals(""))
        {
            next = false;
            context.Response.Write("Chưa chọn bảng cần import.");
        }


        if (!File.Exists(filePhysical))
        {
            next = false;
            context.Response.Write("Không tìm thấy tập tin yêu cầu! Có thể đã bị xóa trước đó.");
        }

        if (next)
        {
            switch (tableName)
            {
                case "quocgia":
                    context.Response.Redirect("QuocGiaImportController.ashx?filename=" + fileName);
                    break;
                case "chucnangsanpham":
                    context.Response.Redirect("ChucNangImportController.ashx?filename=" + fileName);
                    break;
                case "chungloaisanpham":
                    context.Response.Redirect("ChungLoaiImportController.ashx?filename=" + fileName);
                    break;
                case "kieudangsanpham":
                    context.Response.Redirect("KieuDangImportController.ashx?filename=" + fileName);
                    break;
                case "mausacsanpham":
                    context.Response.Redirect("MauSacImportController.ashx?filename=" + fileName);
                    break;
                case "bocai":
                    context.Response.Redirect("BoCaiImportController.ashx?filename=" + fileName);
                    break;
                case "detai":
                    context.Response.Redirect("DeTaiImportController.ashx?filename=" + fileName);
                    break;
                case "doitackinhdoanh":
                    context.Response.Redirect("DoiTacImportController.ashx?filename=" + fileName);
                    break;
                case "sanpham":
                    context.Response.Redirect("HangHoaImportController.ashx?filename=" + fileName);
                    break;
                case "donggoisanpham":
                    context.Response.Redirect("DongGoiSanPhamImportController.ashx?filename=" + fileName);
                    break;
                case "cangxuathang":
                    context.Response.Redirect("CangXuatHangImportController.ashx?filename=" + fileName);
                    break;
                case "sanphamdocquyen":
                    context.Response.Redirect("HangHoaDocQuyenImportController.ashx?filename=" + fileName);
                    break;
                case "donvitinhdonggoi":
                    context.Response.Redirect("DonViTinhDongGoiImportController.ashx?filename=" + fileName);
                    break;
                case "donggoi":
                    context.Response.Redirect("DongGoiImportController.ashx?filename=" + fileName);
                    break;
                case "banggia":
                    context.Response.Redirect("BangGiaImportController.ashx?filename=" + fileName);
                    break;
                case "phienbangia":
                    context.Response.Redirect("PhienBanGiaImportController.ashx?filename=" + fileName);
                    break;
                case "giasanpham":
                    context.Response.Redirect("GiaSanPhamImportController.ashx?filename=" + fileName);
                    break;
                case "nhomnangluc":
                    context.Response.Redirect("NhomNangLucImportController.ashx?filename=" + fileName);
                    break;
                case "bientau":
                    context.Response.Redirect("BienTauImportController.ashx?filename=" + fileName);
                    break;
                case "hscode":
                    context.Response.Redirect("HsCodeImportController.ashx?filename=" + fileName);
                    break;
                case "nhacungungmacdinh":
                    context.Response.Redirect("NhaCungUngMacDinhImportController.ashx?filename=" + fileName);
                    break;
                case "capnhatmotasanpham":
                    context.Response.Redirect("CapNhatMoTaSanPhamImportController.ashx?filename=" + fileName);
                    break;
                case "chitietthuchi":
                    context.Response.Redirect("ChiTietThuChiImportController.ashx?filename=" + fileName);
                    break;
                case "danhmuchanghoa":
                    context.Response.Redirect("HangHoa_DanhMucHangHoa_ImportController.ashx?filename=" + fileName);
                    break;
                case "phannhommau":
                    context.Response.Redirect("PhanNhomMauImportController.ashx?filename=" + fileName);
                    break;
                default:
                    context.Response.Write("Vui lòng chọn bảng cần import.");
                    break;

            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}