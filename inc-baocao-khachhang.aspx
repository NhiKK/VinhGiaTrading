<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-baocao-khachhang.aspx.cs" Inherits="inc_baocao_khachhang" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register Src="Invoice.ascx" TagName="Cbm" TagPrefix="uc2" %>
<%@ Register Src="cbm_dongmoky.ascx" TagName="Cbm" TagPrefix="uc6" %>
<%@ Register Src="cdm_doitackinhdoanh.ascx" TagName="Cbm" TagPrefix="uc3" %>
<%@ Register Src="cbm_hehang.ascx" TagName="Cbm" TagPrefix="uc4" %>
<%@ Register Src="cbm_userlist.ascx" TagName="Cbm" TagPrefix="uc5" %>
<%@ Register Src="cdm_BCKH_khachhangList.ascx" TagName="Cbm" TagPrefix="uc7" %>
<%@ Register Src="cdm_BCKH_nguoiHuongHoaHongList.ascx" TagName="Cbm" TagPrefix="uc8" %>
<% 
    LinqDBDataContext dbc = new LinqDBDataContext();
    String manv = UserUtils.getUser(Context);
    nhanvien nv = dbc.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
    String sql = String.Format("select * from md_baocao bc, md_phanquyenbaocao pqbc where bc.md_baocao_id = pqbc.md_baocao_id and bc.hoatdong = 1 AND pqbc.manv = @manv AND bc.md_module_id='BAOCAOKH' order by bc.ten_baocao");
    String sqlAdmin = "select * from md_baocao where md_module_id='BAOCAOKH' and hoatdong = 1 order by ten_baocao";
    System.Data.DataTable dt = new System.Data.DataTable();

    if (nv.isadmin.Value)
    {
        dt = mdbc.GetData(sqlAdmin);
    }
    else
    {
        dt = mdbc.GetData(sql, "@manv", manv);
    }
%>
<script>
    $(document).ready(function () {
        $('#lay-center-baocaokhachhang').parent().layout({
            west: {
                size: "20%"
            }
        });
        $('#lay-west-baocaokhachhang button').button().css({ 'width': '95%' });

        var startDate, endDate;
        startDate = new Date();
        endDate = new Date();

        // ngày đầu tháng
        startDate.setDate(1);

        // ngày đầu tháng
        endDate.setDate(1);

        // cộng thêm một tháng
        endDate.setMonth(endDate.getMonth() + 1);

        // trừ đi một ngày
        endDate.setDate(endDate.getDate() - 1);

        $(".date input").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $(".date input#txtTuNgayKH").datepicker('setDate', startDate);
        $(".date input#txtDenNgayKH").datepicker('setDate', endDate);
    });

    function submitKhachHang() {
        let frmID = '#frmBCKHHANG';
        let rptId = eval($("#rptKhachHangId", frmID).val());
        let startdate = $("#txtTuNgayKH", frmID).val();
        let enddate = $("#txtDenNgayKH", frmID).val();
        let doitackinhdoanh_id = $("#txtDoiTacKH", frmID).val();
        let hehang_id = $("#txtHeHang", frmID).val();
        let loaiphieu = $("#loaiphieu", frmID).val();
        let thuchi = $("#thuchi", frmID).val();
        let nguoibanhang = $("#txtNguoiBanHang", frmID).val();
        let thongketheo = $("#thongketheo", frmID).val();
        let trangthai = $("#trangthai", frmID).val();
        let trangthaixuathang = $("#trangthaixuathang", frmID).val();
        let kieuin = $("#kieuinBCKH", frmID).val();

        switch (rptId) {
            case 1000:
                actionKhachHang("Doanh số theo chức năng SP"
                    , "PrintControllers/0BaoCaoKH/rpt_DoanhSoTheoCNSP.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 1001:
                actionKhachHang("Báo cáo doanh số - số lượng container theo NCU"
                    , "PrintControllers/0BaoCaoKH/rpt_DoanhSoSLContCBMTheoNCU.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 1002:
                actionKhachHang("Báo cáo số lượng PO theo INV từng KH"
                    , "PrintControllers/0BaoCaoKH/rpt_SoLuongPOTheoINVTungKH.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 1003:
                actionKhachHang("Báo cáo số cont tương ứng mỗi INV"
                    , "PrintControllers/0BaoCaoKH/rpt_InBaoCaoSoContMoiINV.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 1006:
                actionKhachHang("Báo cáo theo dõi xuất hàng"
                    , "PrintControllers/0BaoCaoKH/rpt_InBaoCaoTheoDoiXuatHang.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 1007:
                actionKhachHang("Kiểm tra mã hàng hóa"
                    , "PrintControllers/0BaoCaoKH/rpt_KiemTraMaHangHoa.aspx"
                    , startdate, enddate, doitackinhdoanh_id, null, null, kieuin);
                break;
            case 1008:
                actionKhachHang("Báo cáo kiểm soát"
                    , "PrintControllers/0BaoCaoKH/rpt_BaoCaoKiemSoat.aspx"
                    , startdate, enddate, doitackinhdoanh_id, null, null, kieuin);
                break;

            case 100:
                actionKhachHang("Tổng hợp khiếu nại"
                    , "PrintControllers/InTongHopKhieuNai/"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 101:
                actionKhachHang("Danh sách invoice chậm thanh toán"
                    , "PrintControllers/0BaoCaoKH/rpt_DanhSachInvoiceChamTT.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 102:
                actionKhachHang("Xem số dư cuối kỳ"
                    , "PrintControllers/InSoDuCuoiKy/"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 103:
                actionKhachHang("Tổng hợp thu chi theo KH"
                    , "PrintControllers/InTongHopThuChiKH/"
                    , startdate, enddate, doitackinhdoanh_id);
                break;

            case 1:
                actionKhachHang("Tổng hợp doanh thu theo từng mặt hàng"
                    , "PrintControllers/0BaoCaoKH/rpt_DoanhThuTheoMH.aspx"
                    , startdate, enddate, doitackinhdoanh_id, hehang_id, thongketheo);
                break;
            case 2:
                actionKhachHang("Tổng hợp tình hình công nợ phải thu"
                    , "ReportWizard/RptKhachHang/rptTinhHinhCongNoPhaiThu.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 3:
                // actionKhachHang("Chi tiết công nợ khách hàng"
                // , "ReportWizard/RptKhachHang/rpt_chitietcongnokhachhang.aspx"
                // , startdate, enddate, doitackinhdoanh_id);
                actionKhachHang("Chi tiết công nợ khách hàng",
					"PrintControllers/InChitietCongNoKhachHang/", startdate, enddate, doitackinhdoanh_id);
                break;
            case 4:
                actionKhachHang("Tổng hợp tiền về trong kỳ"
                    , "ReportWizard/RptKhachHang/rptTongHopTienVe.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 5:
                // actionKhachHang("Chi tiết thanh toán theo invoice"
                // , "ReportWizard/RptKhachHang/rpt_chitietthanhtoaninvoice.aspx"
                // , startdate, enddate, doitackinhdoanh_id);
                actionKhachHang("Chi tiết thanh toán theo invoice",
				"PrintControllers/CTTTInvoice/", startdate, enddate, doitackinhdoanh_id);
                break;
            case 6:
                // actionKhachHang("Thống kê hoa hồng phải trả"
                // , "ReportWizard/RptKhachHang/rpt_thongkehoahongphaitra.aspx"
                // , startdate, enddate, doitackinhdoanh_id);
                // break;
                doitackinhdoanh_id = $("#tr_nguoihuonghoahong select", frmID).val();
                window.open("PrintControllers/0BaoCaoKH/rpt_ThongKeHoaHongPhaiTra.aspx?startdate=" + startdate + "&enddate=" + enddate + "&doitackinhdoanh_id=" + doitackinhdoanh_id);
                break;
            case 7:
                actionKhachHang("Báo cáo doanh số - Số lượng cont"
                    , "PrintControllers/0BaoCaoKH/rpt_DoanhSoSLCont.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 8:
                window.open("PrintControllers/0BaoCaoKH/rpt_DoanhSoTungHeHangTheoINVThucXuat.aspx?startdate=" + startdate + "&enddate=" + enddate + "&trans=" + thongketheo);
                break;
            case 9:
                doitackinhdoanh_id = doitackinhdoanh_id + "&loai=" + thongketheo;
                // actionKhachHang("Báo cáo doanh số từng khách hàng / Từng quốc gia (tỉ lệ %)"
                // , "ReportWizard/RptKhachHang/rpt_doanhsokh_quocgia_tyle.aspx"
                // , startdate, enddate, doitackinhdoanh_id);
                actionKhachHang("Báo cáo doanh số từng khách hàng / Từng quốc gia (tỉ lệ %)"
                    , "PrintControllers/0BaoCaoKH/rpt_doanhsokh_quocgia_tyle.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 10:
                let hh = "";
                hh = (hehang_id == "NULL") ? "" : "&hehang_id=" + hehang_id;
                hh = (doitackinhdoanh_id == "NULL") ? hh : hh + "&doitackinhdoanh_id=" + doitackinhdoanh_id;
                hh = (thongketheo == "NULL") ? hh : hh + "&thongketheo=" + thongketheo;
                window.open("PrintControllers/0BaoCaoKH/rpt_LietKeHangBanChay.aspx?startdate=" + startdate + "&enddate=" + enddate + hh);
                //if (thongketheo == 'IN') {
                //    window.open("PrintControllers/0BaoCaoKH/rpt_LietKeHangBanChay.aspx?startdate=" + startdate + "&enddate=" + enddate + hh);
                //} else {
                //    window.open("ReportWizard/RptKhachHang/rpt_lietkehangbanchaypo.aspx?startdate=" + startdate + "&enddate=" + enddate + hh);
                //}
                break;
            case 11:
                doitackinhdoanh_id = doitackinhdoanh_id + "&loai=" + thongketheo;
                actionKhachHang("Báo cáo doanh số từng hệ hàng cập nhật theo ngày - tháng"
                    , "PrintControllers/0BaoCaoKH/rpt_DoanhSoHeHangNgayThang.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 12:
                actionKhachHang("Báo cáo doanh số theo nhân viên chứng từ"
                    , "PrintControllers/0BaoCaoKH/rpt_DoanhSoTheoNVChungTu.aspx"
                    , startdate, enddate, doitackinhdoanh_id);
                break;
            case 13:
                var join = "";
                join = (loaiphieu == "NULL") ? "" : "&loaiphieu=" + loaiphieu;
                join = (doitackinhdoanh_id == "NULL") ? join : join + "&doitackinhdoanh_id=" + doitackinhdoanh_id;
                window.open("ReportWizard/RptKhachHang/rpt_lietkephieuthu.aspx?startdate=" + startdate + "&enddate=" + enddate + join);
                break;
            case 14:
                var join2 = "";
                join2 = (thuchi == "NULL") ? "" : "&ispaymentin=" + thuchi;
                join2 = (doitackinhdoanh_id == "NULL") ? join2 : join2 + "&doitackinhdoanh_id=" + doitackinhdoanh_id;
                window.open("ReportWizard/RptKhachHang/rpt_lietkethuchiTH.aspx?startdate=" + startdate + "&enddate=" + enddate + join2);
                break;
            case 15:
                var join3 = "";
                join3 = (nguoibanhang == "NULL") ? "" : "&nvkinhdoanh=" + nguoibanhang;
                join3 = (doitackinhdoanh_id == "NULL") ? join3 : join3 + "&doitackinhdoanh_id=" + doitackinhdoanh_id;
                window.open("ReportWizard/RptKhachHang/rpt_loinhuanchiphi.aspx?startdate=" + startdate + "&enddate=" + enddate + join3);
                break;
            case 16:
                var join3 = "";
                var manc = "<%=UserUtils.getUser(Context) %>";
                join3 = "&manv=" + manc;
                window.open("PrintControllers/0BaoCaoKH/rpt_DoanhSoTheoNVBanHang.aspx?startdate=" + startdate + "&enddate=" + enddate + join3);
                break;
            case 17:
                var join3 = "";
                join3 = "?kytrongnam=" + $("#kydangmo_id", frmID).val();
                join3 = (doitackinhdoanh_id == "NULL") ? join3 : join3 + "&doitackinhdoanh=" + doitackinhdoanh_id;
                join3 = join3 + "&isncu=" + $("#loaidt", frmID).val();
                window.open("ReportWizard/RptKhachHang/rpt_xemsodudauky.aspx?startdate=" + startdate + "&enddate=" + enddate + join3);
                break;
            case 18:
                window.open("PrintControllers/0BaoCaoKH/rpt_XuatHangTheoPO.aspx?startdate=" + startdate + "&enddate=" + enddate);
                break;
            case 19:
                var join3 = "";
                join3 = (trangthai == "NULL") ? "" : "&trangthai=" + trangthai;
                join3 = (doitackinhdoanh_id == "NULL") ? join3 : join3 + "&doitackinhdoanh_id=" + doitackinhdoanh_id;
                window.open("PrintControllers/0BaoCaoKH/rpt_InBaoCaoDonHangDaNhan.aspx?startdate=" + startdate + "&enddate=" + enddate + join3);
                break;
            case 20:
                var join3 = "";
                join3 = (trangthaixuathang == "NULL") ? "" : "&trangthaixuathang=" + trangthaixuathang;
                join3 = (doitackinhdoanh_id == "NULL") ? join3 : join3 + "&doitackinhdoanh_id=" + doitackinhdoanh_id;
                window.open("PrintControllers/0BaoCaoKH/rpt_InBaoCaoDonHangDaXuat.aspx?startdate=" + startdate + "&enddate=" + enddate + join3);
                break;
            default:
                alert('Không tìm thấy hàm!');
                break;
        }
    }

    function actionKhachHang(title, url, startdate, enddate, doitackinhdoanh_id, hehang_id, baocaotheo, kieuin) {
        var rptURL = url + "?startdate=" + startdate + "&enddate=" + enddate;
        if (doitackinhdoanh_id != "NULL") {
            rptURL += "&doitackinhdoanh_id=" + doitackinhdoanh_id;
        }

        if (hehang_id != "NULL" & hehang_id != null) {
            rptURL += "&hehang_id=" + hehang_id;
        }

        if (baocaotheo != "NULL" & hehang_id != null) {
            rptURL += "&baocaotheo=" + baocaotheo;
        }

        if (kieuin != "NULL" & kieuin != null) {
            rptURL += "&kieuin=" + kieuin;
        }

        window.open(rptURL);
    }

    function createFilterKH(type) {
        var frmID = '#frmBCKHHANG';
        $("#filterKH", frmID).css({ "display": "" });
        $("#tr_hehang", frmID).css({ "display": "none" });
        $("#tr_loaiphieu", frmID).css({ "display": "none" });
        $("#rptKhachHangId", frmID).val(type);
        $("#tr_thuchi", frmID).css({ "display": "none" });
        $("#tr_kieuinBCKH", frmID).css({ "display": "none" });
        $("#tr_nguoibanhang", frmID).css({ "display": "none" });
        $("#tungay", frmID).css({ "display": "" });
        $("#denngay", frmID).css({ "display": "" });
        $("#tr_kydangmo", frmID).css({ "display": "none" });
        $("#tr_loaikh", frmID).css({ "display": "none" });
        $("#tr_thongketheo", frmID).css({ "display": "none" });
        $("#tr_nguoihuonghoahong", frmID).css({ "display": "none" });
        $("#tr_dtkd", frmID).css({ "display": "none" });
        $("#tr_trangthai", frmID).hide();
        $("#tr_trangthaixuathang", frmID).hide();
        switch (type) {
            case 1000:
                $('#lblReportNameKH').html("Báo cáo doanh số theo chức năng SP");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 1001:
                $('#lblReportNameKH').html("Báo cáo doanh số - số lượng container theo NCU");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 1002:
                $('#lblReportNameKH').html("Báo cáo số lượng PO theo INV từng KH");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 1003:
                $('#lblReportNameKH').html("Báo cáo số cont tương ứng mỗi INV");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 1006:
                $('#lblReportNameKH').html("Báo cáo theo dõi xuất hàng");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 1007:
                $('#lblReportNameKH').html("Kiểm tra mã hàng hóa");
                $("#tr_dtkd", frmID).css({ "display": "" });
                $("#tr_kieuinBCKH", frmID).css({ "display": "" });
                break;
            case 1008:
                $('#lblReportNameKH').html("Báo cáo kiểm soát");
                $("#tr_dtkd", frmID).css({ "display": "" });
                $("#tr_kieuinBCKH", frmID).css({ "display": "" });
                break;

            case 100:
                $('#lblReportNameKH').html("Tổng hợp khiếu nại");
                $("#tr_dtkd", frmID).css({ "display": "" });
                break;
            case 101:
                $('#lblReportNameKH').html("Danh sách invoice chậm thanh toán");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 102:
                $('#lblReportNameKH').html("Xem Số dư cuối kỳ");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 103:
                $('#lblReportNameKH').html("Tổng hợp thu chi theo KH");
                $("#tr_dtkd", frmID).css({ "display": "" });
                break;

            case 1:
                $('#lblReportNameKH').html("Tổng hợp doanh thu theo từng mặt hàng");
                $("#tr_dtkd", frmID).css({ "display": "" });
                $("#tr_hehang", frmID).css({ "display": "" });
                $("#tr_thongketheo").css({ "display": "" });
                break;
            case 2:
                $('#lblReportNameKH').html("Tổng hợp tình hình công nợ phải thu");
                $("#tr_dtkd", frmID).css({ "display": "" });
                break;
            case 3:
                $('#lblReportNameKH').html("Chi tiết công nợ khách hàng");
                $("#tr_dtkd", frmID).css({ "display": "" });
                break;
            case 4:
                $('#lblReportNameKH').html("Tổng hợp tiền về trong kỳ");
                $("#tr_dtkd", frmID).css({ "display": "" });
                break;
            case 5:
                $('#lblReportNameKH').html("Chi tiết thanh toán theo invoice");
                $("#tr_dtkd").css({ "display": "" });
                break;
            case 6:
                $('#lblReportNameKH').html("Thống kê hoa hồng phải trả");
                $("#tr_nguoihuonghoahong", frmID).css({ "display": "" });
                break;
            case 7:
                $('#lblReportNameKH').html("Báo cáo doanh số - Số lượng cont");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 8:
                $("#tr_thongketheo").css({ "display": "" });
                $('#lblReportNameKH').html("Báo cáo doanh số từng hệ hàng theo Invoice thực xuất");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 9:
                $('#lblReportNameKH').html("Báo cáo doanh số từng khách hàng");
                $("#tr_dtkd", frmID).css({ "display": "" });
                break;
            case 10:
                $('#lblReportNameKH').html("Báo cáo liệt kê hàng bán chạy");
                $("#tr_thongketheo", frmID).css({ "display": "" });
                $("#tr_dtkd", frmID).css({ "display": "" });
                $("#tr_hehang", frmID).css({ "display": "" });
                break;
            case 11:
                $('#lblReportNameKH').html("Báo cáo doanh số từng hệ hàng cập nhật theo ngày - tháng");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 12:
                $('#lblReportNameKH').html("Báo cáo doanh số theo nhân viên chứng từ");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 16:
                $('#lblReportNameKH').html("Báo cáo doanh số theo nhân viên bán hàng");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;

            case 17:
                $('#lblReportNameKH').html("Xem số dư đầu kỳ");
                $("#tr_dtkd", frmID).css({ "display": "" });
                $("#tr_kydangmo", frmID).css({ "display": "" });
                $("#tr_loaikh", frmID).css({ "display": "" });
                $("#tungay", frmID).css({ "display": "none" });
                $("#denngay", frmID).css({ "display": "none" });
                break;

            case 18:
                $('#lblReportNameKH').html("Xuất hàng theo P/O");
                $("#tr_dtkd", frmID).css({ "display": "none" });
                break;
            case 19:
                $('#lblReportNameKH').html("Báo cáo đơn hàng đã nhận");
                $("#tr_trangthai", frmID).css({ "display": "" });
                $("#tr_dtkd", frmID).css({ "display": "" });
                break;
            case 20:
                $('#lblReportNameKH').html("Báo cáo đơn hàng đã xuất");
                $("#tr_trangthaixuathang", frmID).css({ "display": "" });
                $("#tr_dtkd", frmID).css({ "display": "" });
                break;

            default:
                $('#lblReportNameKH').html("Không xác định report");
                break;
        }

    }

    function createFilterHH(type) {
        var frmID = '#frmBCKHHANG';
        $("#filterKH", frmID).css({ "display": "" });
        $("#tr_dtkd", frmID).css({ "display": "none" });
        $("#tr_hehang", frmID).css({ "display": "" });
        $("#rptKhachHangId", frmID).val(type);
        $("#tr_thuchi", frmID).css({ "display": "none" });
        $("#tr_nguoibanhang", frmID).css({ "display": "none" });
        $("#tr_kydangmo", frmID).css({ "display": "none" });
        $("#tungay", frmID).css({ "display": "" });
        $("#denngay", frmID).css({ "display": "" });
        $("#tr_loaikh", frmID).css({ "display": "none" });
        $("#tr_loaiphieu", frmID).css({ "display": "none" });
        $("#tr_nguoihuonghoahong", frmID).css({ "display": "none" });
        switch (type) {
            case 10:
                $("#tr_thongketheo", frmID).css({ "display": "" });
                $("#tr_dtkd", frmID).css({ "display": "" });
                $('#lblReportNameKH').html("Báo cáo liệt kê hàng bán chạy");
                break;
            default:
                $('#lblReportNameKH').html("Không xác định report");
                break;
        }
    }

    function createFilterPT(type) {
        var frmID = '#frmBCKHHANG';
        $("#filterKH", frmID).css({ "display": "" });
        $("#tr_hehang", frmID).css({ "display": "none" });
        $("#tr_dtkd", frmID).css({ "display": "" });
        $("#tungay", frmID).css({ "display": "" });
        $("#denngay", frmID).css({ "display": "" });
        $("#rptKhachHangId", frmID).val(type);
        $("#tr_nguoibanhang", frmID).css({ "display": "none" });
        $("#tr_kydangmo", frmID).css({ "display": "none" });
        $("#tr_loaikh", frmID).css({ "display": "none" });
        $("#tr_thongketheo", frmID).css({ "display": "none" });
        switch (type) {
            case 13:
                $("#tr_loaiphieu", frmID).css({ "display": "" });
                $("#tr_thuchi", frmID).css({ "display": "none" });
                $('#lblReportNameKH').html("Liệt kê chi tiết các khoản phiếu thu");
                break;
            case 14:
                $("#tr_loaiphieu", frmID).css({ "display": "none" });
                $("#tr_thuchi", frmID).css({ "display": "" });
                $('#lblReportNameKH').html("Liệt kê tổng hợp phiếu thu/chi");
                break;
            case 15:
                $("#tr_nguoibanhang", frmID).css({ "display": "" });
                $("#tr_loaiphieu", frmID).css({ "display": "none" });
                $("#tr_thuchi", frmID).css({ "display": "none" });
                $('#lblReportNameKH').html("Xem lợi nhuận chi phí");
                break;
            default:
                $('#lblReportNameKH').html("Không xác định report");
                break;
        }
    }
</script>
<% 
    LinqDBDataContext db = new LinqDBDataContext();
    this.doitackinhdoanh.Name = "txtDoiTacKH";
    this.doitackinhdoanh.isncc = false;
    this.doitackinhdoanh.NullFirstItem = true;
    this.hehang.Name = "txtHeHang";
    this.hehang.NullFirstItem = true;
    this.nguoibanhang.Name = "txtNguoiBanHang";
    this.nguoibanhang.NullFirstItem = true;

    this.kydangmo.Name = "kydangmo_id";
    this.kydangmo.NullFirstItem = false;


    %>
    
<div style="background:#F4F0EC" class="ui-layout-center ui-widget-content" id="lay-center-baocaokhachhang">
    <form id="frmBCKHHANG">
        <div id="filterKH" style="display:none; padding:5px 5px 15px 15px">
            <input type="hidden" name="rptKhachHangId" id="rptKhachHangId"/>
            <table style="width:100%">
				<tr>
					<th style="width:160px"></th>
					<th></th>
				</tr>
                <tr>
                    <td colspan="2"><h3 style="font-size:18px; padding:5px" id="lblReportNameKH"></h3></td>
                </tr>
                <tr id="tungay">
                    <td >Từ ngày</td>
                    <td>
                        <div class="date">
                            <input type="text" name="txtTuNgayKH" id="txtTuNgayKH"/>
                        </div>
                    </td>
                </tr>
                <tr id="denngay">
                    <td>Đến ngày</td>
                    <td>
                        <div class="date">
                            <input type="text" name="txtDenNgayKH" id="txtDenNgayKH"/>
                        </div>
                    </td>
                </tr>
                <tr id="tr_kydangmo">
                    <td>Chọn người bán hàng</td>
                    <td><uc6:Cbm id="kydangmo" runat="server"/></td>
                </tr>
                    <tr id="tr_loaikh">
                    <td>Chọn loại đối tác</td>
                    <td><select id="loaidt" name="loaidt">
                            <option value="true">NCU</option>
                            <option value="false" selected="selected">Khách hàng</option>
                        </select></td>
                </tr>
                <tr id="tr_dtkd">
                    <td>Chọn khách hàng</td>
                    <td><uc7:Cbm id="doitackinhdoanh" runat="server"/></td>
                </tr>
				<tr id="tr_nguoihuonghoahong">
                    <td>Chọn người hưởng hoa hồng</td>
                    <td><uc8:Cbm id="nguoihuonghoahong" runat="server"/></td>
                </tr>
                <tr id="tr_hehang" >
                    <td>Chọn hệ hàng</td>
                    <td><uc4:Cbm id="hehang" runat="server"/></td>
                </tr>
                <tr id="tr_loaiphieu" >
                    <td>Chọn loại phiếu</td>
                    <td><select name="loaiphieu", id="loaiphieu">
                        <option value="NULL"></option>
                        <option value="CL">Thu tiền Clam</option>
                        <option value="TU">Thu lại tạm ứng</option>
                        <option value="PO">Hợp đồng</option>
                        <option value="IN">Packing list - Invoice</option>
                        </select></td>
                </tr>
                <tr id="tr_kieuinBCKH" >
                    <td>Loại in</td>
                    <td>
                        <select name="kieuinBCKH", id="kieuinBCKH">
						    <option value="2">Excel</option>
						    <option value="3" selected="selected">PDF</option>
                        </select>
                    </td>
                </tr>
                <tr id="tr_thongketheo" >
                    <td>Thống kê theo</td>
                    <td><select name="thongketheo", id="thongketheo">
						    <option value="PO">Đơn hàng (PO)</option>
						    <option value="IN" selected="selected">Packing list - Invoice</option>
                        </select></td>
                </tr>
                <tr id="tr_thuchi" >
                    <td>Chọn loại phiếu</td>
                    <td><select name="thuchi", id="thuchi">
                        <option value="NULL"></option>
                        <option value="true">Phiếu thu</option>
                        <option value="flase">Phiếu chi</option>
                        </select></td>
                </tr>
                <tr id="tr_nguoibanhang">
                    <td>Chọn người bán hàng</td>
                    <td><uc5:Cbm id="nguoibanhang" runat="server"/></td>
                </tr>
                <tr id="tr_trangthai">
                    <td>Trạng thái</td>
                    <td><select id="trangthai" name="trangthai">
                            <option value="NULL" selected="selected"></option>
                            <option value="SOANTHAO">Soạn thảo</option>
                            <option value="HIEULUC">Hiệu lực</option>
                            <option value="KETTHUC">Kết thúc</option>
                        </select>
                    </td>
                </tr>

                <tr id="tr_trangthaixuathang">
                    <td>Trạng thái</td>
                    <td><select id="trangthaixuathang" name="trangthaixuathang">
                            <option value="NULL" selected="selected"></option>
                            <option value="XUATHET">Xuất hết</option>
                            <option value="XUATCHUAHET">Chưa xuất hết</option>
                        </select>
                    </td>
                </tr>
            
                <tr><td></td></tr>
                <tr>
                    <td></td>
                    <td><input type = "button" name="xemketqua" id="xemketqua" value="Xem kết quả" onclick="submitKhachHang()"/></td>
                </tr>
            </table>
        </div>
    </form>
</div>

<div style="background:#F4F0EC;" class="ui-layout-west" id="lay-west-baocaokhachhang">
    <div id="actonlist" class="ui-widget-content" style="height:100%; overflow:auto; text-align:center">
		<% foreach (System.Data.DataRow item in dt.Rows)
            {%>
                <button style="text-align:left;" onclick="<%= item["hanhdong"] %>"><%= item["ten_baocao"] %></button>
        <% } %>
        
    </div>
</div>

<!-- <button onclick="createFilterKH(1)" style="text-align:left;">Tổng hợp doanh thu theo từng mặt hàng</button>
        <button onclick="createFilterKH(2)" style="text-align:left;">Tổng hợp tình hình công nợ phải thu</button>
        <button onclick="createFilterKH(3)" style="text-align:left;">Chi tiết công nợ khách hàng</button>
        <button onclick="createFilterKH(4)" style="text-align:left;">Tổng hợp tiền về trong kỳ</button>
        <button onclick="createFilterKH(5)" style="text-align:left;">Chi tiết thanh toán theo invoice</button>
        <button onclick="createFilterKH(6)" style="text-align:left;">Thống kê hoa hồng phải trả</button>
        <button onclick="createFilterKH(7)" style="text-align:left;">Báo cáo doanh số - Số lượng cont</button>
        <button onclick="createFilterKH(8)" style="text-align:left;">Báo cáo doanh số từng hệ hàng theo Invoice thực xuất</button>
        <button onclick="createFilterKH(9)" style="text-align:left;">Báo cáo doanh số từng khách hàng</button>
        <button onclick="createFilterHH(10)" style="text-align:left;">Báo cáo liệt kê hàng bán chạy</button>
        <button onclick="createFilterKH(11)" style="text-align:left;">Báo cáo doanh số từng hệ hàng cập nhật theo ngày - tháng(theo P/O)</button>
        <button onclick="createFilterKH(12)" style="text-align:left;">Báo cáo doanh số bán hàng theo nhân viên chứng từ</button>
        <button onclick="createFilterKH(16)" style="text-align:left;">Báo cáo doanh số bán hàng từng nhân viên và Invoice thực xuất</button>
        <button onclick="createFilterPT(13)" style="text-align:left;">Liệt kê chi tiết các khoản phiếu thu</button>
        <button onclick="createFilterPT(14)" style="text-align:left;">Liệt kê tổng hợp thu chi</button>
        <button onclick="createFilterPT(15)" style="text-align:left;">Lợi nhuận chi phí</button>
        <button onclick="createFilterKH(17)" style="text-align:left;">Xem số dư đầu kỳ</button> -->