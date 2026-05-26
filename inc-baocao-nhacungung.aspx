<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-baocao-nhacungung.aspx.cs" Inherits="inc_baocao_nhacungung" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register Src="Invoice.ascx" TagName="Cbm" TagPrefix="uc2" %>
<%@ Register Src="cdm_doitackinhdoanh.ascx" TagName="Cbm" TagPrefix="uc3" %>
<% 
    LinqDBDataContext dbc = new LinqDBDataContext();
    String manv = UserUtils.getUser(Context);
    nhanvien nv = dbc.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
    String sql = String.Format("select * from md_baocao bc, md_phanquyenbaocao pqbc where bc.md_baocao_id = pqbc.md_baocao_id and bc.hoatdong = 1 AND pqbc.manv = @manv AND bc.md_module_id='BAOCAONCU' order by ten_baocao asc");
    String sqlAdmin = "select * from md_baocao where md_module_id='BAOCAONCU' and hoatdong = 1 order by ten_baocao asc ";
    System.Data.DataTable dt = new System.Data.DataTable();

    if (nv.isadmin.Value)
    {
        dt = mdbc.GetData(sqlAdmin);
    }
    else {
        dt = mdbc.GetData(sql, "@manv", manv);
    }
%>
<style type="text/css">
    a#nhapDuLieuChoBaoCao {
        position: relative;
        font-weight: bold;
        text-align: center;
        width: 100%;
        color: blue;
        text-decoration: underline;
        cursor: pointer;
        user-select: none;
    }

    td.td100 {
        width: 100px;
    }
</style>
<script>
    $(document).ready(function() {
        $('#lay-center-baocaonhacungung').parent().layout({
            west: {
                size: "20%"
            }
        });
        $('#lay-west-baocaonhacungung button').button().css({ 'width': '95%' });


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
        var frmID = '#frmBCNCU';
        $("#tungayBCNCU", frmID).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $("#tungayBCNCU", frmID).datepicker('setDate', startDate);

        $("#denngayBCNCU", frmID).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $("#denngayBCNCU", frmID).datepicker('setDate', endDate);
    });



    function getQueryString() {
        let frmID = '#frmBCNCU';
        let tungayBCNCU = $("#tungayBCNCU", frmID).val();
        let denngayBCNCU = $("#denngayBCNCU", frmID).val();
        let ncu = $("#md_doitackinhdoanh_id", frmID).val();
        let cdxem = $("#chedoxem", frmID).val();
        let queryString = "?startdate=" + tungayBCNCU + "&enddate=" + denngayBCNCU;
        queryString = queryString + "&cdxem=" + cdxem;
        if (ncu != "NULL") {
            queryString = queryString + "&doitackinhdoanh_id=" + ncu;
        }
        return queryString;
    }

    function createFilter(stt) {
        let frmID = '#frmBCNCU';
        let ncuDisplay = $("#md_doitackinhdoanh_id", frmID).parent().parent();
        let namDisplay = $('#namBCNCU', frmID).parent().parent();
        let thangDisplay = $('#thangBCNCU', frmID).parent().parent();
        let tungayDisplay = $('#tungayBCNCU', frmID).parent().parent();
        let denngayDisplay = $('#denngayBCNCU', frmID).parent().parent();
        let loaiBaoCaoDisplay = $('#loaiBaoCao', frmID).parent().parent();

        let cheDoXemBThoacKT = $('.cheDoXemBThoacKT');

        let ncuDisplayFilter = function (filter) {
            $('#md_doitackinhdoanh_id option', frmID).each(function () {
                if (filter != null) {
                    if (filter.lastIndexOf($(this).text()) <= -1)
                        $(this).hide();
                    else
                        $(this).show();
                }
                else
                    $(this).show();
            });
        }

        namDisplay.hide();
        thangDisplay.hide();
        loaiBaoCaoDisplay.hide();

        cheDoXemBThoacKT.hide();
        tungayDisplay.show();
        denngayDisplay.show();
        ncuDisplay.show();
        ncuDisplayFilter(null);
        $("#filter").css({ "display": "" });
        $("#report_id").val(stt);
        switch (stt) {
            case 1000:
                ncuDisplay.hide();
                $('#lblReportNameNCU').html("Báo cáo doanh số nhà cung ứng");
                break;
            case 1001:
                ncuDisplay.hide();
                $('#lblReportNameNCU').html("Báo cáo phí cộng trừ theo nhà cung ứng");
                break;
            case 1002:
                loaiBaoCaoDisplay.show();
                namDisplay.show();
                tungayDisplay.hide();
                denngayDisplay.hide();
                ncuDisplayFilter(['ANCO1', 'ANCO2']);
                $('#lblReportNameNCU').html("Báo cáo tổng hợp doanh thu - chi phí của xưởng");
                break;
			case 100:
                $('#lblReportNameNCU').html("Tổng hợp tiền hàng đã xuất chi tiết");
                break;
            case 1:
                $('#lblReportNameNCU').html("Tổng tiền hàng đã nhập");
                break;
            case 2:
                cheDoXemBThoacKT.show();
                $('#lblReportNameNCU').html("Tổng hợp tiền hàng đã nhập chi tiết");
                break;
            case 3:
                $('#lblReportNameNCU').html("Tổng hợp giá trị tiền hàng đang đặc NCƯ");
                break;
            case 4:
                $('#lblReportNameNCU').html("Công nợ nhà cung ứng");
                break;
			case 5:
                $('#lblReportNameNCU').html("Tổng hợp thu chi theo NCC");
                break;
			case 6:
                $('#lblReportNameNCU').html("Hạch toán theo Invoice và NCC");
                break;
			case 7:
                $('#lblReportNameNCU').html("Doanh số theo NCC");
                break;
            case 8:
                $('#lblReportNameNCU').html("Báo cáo bộ phận ANCOTRADING");
                break;
			case 9:
                $('#lblReportNameNCU').html("Báo cáo tổng hợp thu/chi");
                break;
			case 10:
                $('#lblReportNameNCU').html("Báo cáo tổng hợp doanh thu");
                break;
			case 11:
                $('#lblReportNameNCU').html("Báo cáo chi tiết doanh thu");
                break;
			default:
                $('#lblReportNameNCU').html("Không xác định report");
                break;
        }

        let loaiBC = $('#loaiBaoCao', frmID);
        loaiBC.off('change');
        loaiBC.change(function () {
            if ($(this).val() == 'DTCPtheoThang')
                thangDisplay.show();
            else
                thangDisplay.hide();
        });
        loaiBC.prop('selectedIndex', 0).change();
    }

    function submitRpt() {
        var reportid = eval($("#report_id").val());
        var qstring = getQueryString();
        
        if (reportid == 1) {
            //window.open("ReportWizard/Rpt_NCC/rpt_tienhangdanhap.aspx" + qstring);
            window.open("PrintControllers/InTienHangChiTiet/rpt_tienhangdanhap.aspx" + qstring);
        } 
		else if (reportid == 2) {
            window.open("PrintControllers/InTienHangChiTiet/"+ qstring);
        }
		else if (reportid == 3) {
            window.open("ReportWizard/Rpt_NCC/rpt_giatritienhangdangdat.aspx" + qstring);
        }
		else if (reportid == 4) {
            window.open("/anco/PrintControllers/InCongNoNCC/"+ qstring);
        } 
		else if (reportid == 5) {
            window.open("/anco/PrintControllers/InTongHopThuChiNCC/"+ qstring);
        } 
		else if (reportid == 6) {
            window.open("PrintControllers/InHachToanTheoInvoice/" + qstring);
        } 
		else if (reportid == 7) {
            window.open("PrintControllers/InDoanhSoTheoNCC/" + qstring);
        }
		else if (reportid == 8) {
            window.open("PrintControllers/InBaoCaoAncotrading/" + qstring);
        }
		else if (reportid == 9) {
            window.open("PrintControllers/InBaoCaoThuChi/" + qstring);
        }
		else if (reportid == 10) {
            window.open("PrintControllers/InBaoCaoTongHopDoanhThu/" + qstring);
        }
		else if (reportid == 11) {
            window.open("PrintControllers/InBaoCaoChiTietDoanhThu/" + qstring);
        }
		else if (reportid == 100) {
            window.open("PrintControllers/InTienHangChiTietXuat/" + qstring);
        }
        else if (reportid == 1000) {
            window.open("PrintControllers/0BaoCaoNCU/rpt_DoanhSoNCU.aspx" + qstring);
        }
        else if (reportid == 1001) {
            window.open("PrintControllers/0BaoCaoNCU/rpt_PhiCongPhiTruNCU.aspx" + qstring);
        }
        else if (reportid == 1002) {
            qstring = "";
            qstring += "?nam=" + $('#namBCNCU', frmBCNCU).val();
            qstring += "&thang=" + $('#thangBCNCU', frmBCNCU).val();
            qstring += "&xuong=" + $('#md_doitackinhdoanh_id', frmBCNCU).val();
            qstring += "&maXuong=" + $('#md_doitackinhdoanh_id option:selected', frmBCNCU).text();

            let lbc = $('#loaiBaoCao', frmBCNCU).val();
            let url = '';
            if (lbc == 'DTCPtheoNam')
                url = "PrintControllers/InBaoCaoTongHopDoanhThu/rptDoanhThuChiPhiXuongTheoNam.aspx" + qstring;
            else if (lbc == 'DTCPtheoThang')
                url = "PrintControllers/InBaoCaoTongHopDoanhThu/rptDoanhThuChiPhiXuongTheoThang.aspx" + qstring;
            else if (lbc == 'DTtheoNam')
                url = "PrintControllers/InBaoCaoTongHopDoanhThu/rptDoanhThuXuongTheoNam.aspx" + qstring;
            else if (lbc == 'DTtheoThang')
                url = "PrintControllers/InBaoCaoTongHopDoanhThu/rptDoanhThuXuongTheoThang.aspx" + qstring;
            window.open(url);
        }
    }

    function nhapLieuBaoCaoNCC()
    {
        var reportid = eval($("#report_id").val());
        if (reportid == 1002) {
            add_tab('Nhập liệu tổng hợp D.Thu-C.Phí', 'inc-nhaplieu-bcThopDTCP.aspx');
        }
        else {
            alert('Báo cáo này không cần nhập liệu');
        }
    }
 
</script>

<% 
    LinqDBDataContext db = new LinqDBDataContext();
    this.doitackinhdoanh.Name = "md_doitackinhdoanh_id";
    this.doitackinhdoanh.isncc = true;
    this.doitackinhdoanh.NullFirstItem = true;
    %>

<div style="background:#F4F0EC" class="ui-layout-center ui-widget-content" id="lay-center-baocaonhacungung">
    <div id="filter" style="display:none;">
    <input type="hidden" name="report_id" id="report_id"/>
    <form id="frmBCNCU">
        <table style="padding: 5px 10px 5px 10px;">
            <tr>
                <td colspan="2"><h3 style="font-size:18px; padding:5px" id="lblReportNameNCU"></h3></td>
            </tr>
            <tr>
                <td class="td100">Loại báo cáo</td>
                <td>
                    <select id="loaiBaoCao">
                        <option rpId="1002" value="DTCPtheoNam">Doanh thu và chi phí theo năm</option>
                        <option rpId="1002" value="DTCPtheoThang">Doanh thu và chi phí theo tháng</option>
                        <option rpId="1002" value="DTtheoNam">Doanh Thu theo năm</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="td100">Từ ngày</td>
                <td><input type="text" name="tungay" id="tungayBCNCU"/></td>
            </tr>
            <tr>
                <td class="td100">Đến ngày</td>
                <td><input type="text" name="denngay" id="denngayBCNCU"/></td>
            </tr>
            <tr>
                <td class="td100">Chọn năm</td>
                <td>
                    <select name="nam" id="namBCNCU">
                        <% for (int i = DateTime.Now.Year + 10; i >= DateTime.Now.Year - 10; i--)
                           {%>
                              <option value="<%=i %>" <%= DateTime.Now.Year == i ? " selected=\"selected\"" :""%> ><%=i %> </option> 
                           <%} %>
                   </select>
                </td>
            </tr>
            <tr>
                <td class="td100">Chọn tháng</td>
                <td>
                    <select name="thang" id="thangBCNCU">
                        <% for (int i = 12; i >= 1; i--)
                           {%>
                              <option value="<%=i %>" <%= DateTime.Now.Month == i ? " selected=\"selected\"" :""%> ><%=i %> </option> 
                           <%} %>
                   </select>
                </td>
            </tr>
            <tr>
                <td class="td100">Chọn NCU</td>
                <td><uc3:Cbm id="doitackinhdoanh" runat="server"/></td>
            </tr>
            <tr class="cheDoXemBThoacKT">
                <td class="td100">Chọn chế độ xem</td>
                <td>
                    <select id="chedoxem">
                        <option value="">Bình thường</option>
                        <option value="KT">Kế Toán</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="height:10px"></div>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input style="padding:10px; font-weight: bold" type="button" name="xemketqua" id="xemketqua" value="Xem kết quả" onclick="submitRpt()"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="height:10px"></div>
                </td>
            </tr>
            <tr>
                <td class="td100"></td>
                <td>
                    <a onclick="nhapLieuBaoCaoNCC(this)" id="nhapDuLieuChoBaoCao">Nhập dữ liệu cho báo cáo</a>
                </td>
            </tr>
            <tr class="cheDoXemBThoacKT">
                <td colspan="2">
                    <div style="height:10px"></div>
                </td>
            </tr>
            <tr class="cheDoXemBThoacKT">
                <td colspan="2">
                    <b>Chế độ xem KT : </b><i>Thời gian dựa trên ngày nhập kho và xem được phiếu Soạn Thảo</i>
                </td>
            </tr>
        </table>
    </form>
</div>
<div id="reportviewer">

</div>
</div>

<div style="background:#F4F0EC;" class="ui-layout-west" id="lay-west-baocaonhacungung">
    <div class="ui-widget-content" style="height:100%; overflow:auto; text-align:center">
		<% foreach (System.Data.DataRow item in dt.Rows)
           {%>
                <button style="text-align:left;" onclick="<%= item["hanhdong"] %>"><%= item["ten_baocao"] %></button>
        <% } %>
        
    </div>
</div>

