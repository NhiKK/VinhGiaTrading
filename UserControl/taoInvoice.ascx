<%@ Control Language="C#" AutoEventWireup="true" CodeFile="taoInvoice.ascx.cs" Inherits="UserControl_taoInvoice" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    var query =
       from dtkd in db.md_doitackinhdoanhs
       join ldtkd in db.md_loaidtkds on dtkd.md_loaidtkd_id equals ldtkd.md_loaidtkd_id
       where ldtkd.ma_loaidtkd == "NCC" & dtkd.hoatdong == true
       select new { md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id, ma_dtkd = dtkd.ma_dtkd };
    var title = type == "KH" ? "cùng Khách Hàng" : "cùng PO";
    title = "Tạo Packing List - Invoice " + title;
%>
<style>
    .frm-packinglist {
        padding-top: 5px;
    }

        .frm-packinglist tr td {
            padding: 3px;
        }
</style>

<script>
    $(document).ready(function () {
        //let form = "#frmFrom<%=type%>";

        $('#txt<%=type%>NgayLap').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $('#txt<%=type%>NgayLap').datepicker('setDate', new Date());

        $('#txt<%=type%>NgayVanDon').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $('#txt<%=type%>NgayVanDon').datepicker('setDate', new Date());

        $('#txt<%=type%>HopDongUyThac').parent().parent().hide();

        
        <%--$('#sel<%=type %>LoaiHang').change(function () {
            if ($(form + " #sel<%=type %>LoaiHang").val() == "Nhận ủy thác")
                $('#txt<%=type%>HopDongUyThac').parent().parent().show();
            else
                $('#txt<%=type%>HopDongUyThac').parent().parent().hide();
        });--%>

        $('#btn_add_from_<%=type%>').click(function () {
            let idDlgTINV = "create_pkl_<%=type%>";
            let title = "<%=title%>";
            let form = "#frmFrom<%=type%>";

            if ($(form + " #txt<%=type %>SoTau").val() == "" || $(form + " #txt<%=type %>SoTau").val() == " ") {
                alert("Số Tàu(M/V) không được bỏ trống!");
            }
            else if ($(form + " #txt<%=type %>NoiDen").val() == "" || $(form + " #txt<%=type %>NoiDen").val() == " ") {
                alert("Nơi Đến(To) không được bỏ trống!");
            }
            else {
                $('body').append("<div title='" + title + "' id='" + idDlgTINV + "'>" +
                    "<h3 style='padding:10px' class='ui-state-highlight ui-corner-all'>" + "<div style='display:none' id='wait'><img style='width:30px; height:30px' src='iconcollection/loading.gif'/></div><div id='dialog_caution'>Có phải bạn muốn tạo Packing List - Invoice?</div></h3></div>");
                $(`#${idDlgTINV}`).dialog({
                    modal: true
					, open: function (event, ui) {
					    //hide close button.
					    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
					}
					, buttons:
					[
						{
						    id: `btn_ok${idDlgTINV}`,
						    text: "Có",
						    click: function () {
						        $("#wait").css("display", "block");
						        $(`#btn_ok${idDlgTINV}`).button("disable");
						        $(`#btn_close${idDlgTINV}`).button("disable");

						        let finallyDLG = function () {
						            $("#wait").css("display", "none");
						            $(`#btn_close${idDlgTINV} span`).text("Thoát");
						            $(`#btn_close${idDlgTINV}`).button("enable");
						        };

						        let data = new Object();

						        //data["txtSoPackingList"] = $(form + " #txt<%=type %>SoPackingList").val();
						        data["txtSoInvoice"] = $(form + " #txt<%=type %>SoInvoice").val();
						        data["txtNgayLap"] = $(form + " #txt<%=type %>NgayLap").val();
						        data["txtSoTau"] = $(form + " #txt<%=type %>SoTau").val();
						        //data["selNoiDi"] = $(form + " #selKHNoiDi").val();
						        data["txtNoiDen"] = $(form + " #txt<%=type %>NoiDen").val();
						        data["txtSoVanDon"] = $(form + " #txt<%=type %>SoVanDon").val();
						        data["txtNgayVanDon"] = $(form + " #txt<%=type %>NgayVanDon").val();
						        data["txtHangHoa"] = $(form + " #txt<%=type %>HangHoa").val();
						        data["txtHangHoaVn"] = $(form + " #txt<%=type %>HangHoaVn").val();
						        data["txtDienGiaiCongThem"] = $(form + " #txt<%=type %>DienGiaiCongThem").val();
						        data["txtGiaTriCongThem"] = $(form + " #txt<%=type %>GiaTriCongThem").val();
						        data["txtDienGiaiTruLai"] = $(form + " #txt<%=type %>DienGiaiTruLai").val();
						        data["txtGiaTriTruLai"] = $(form + " #txt<%=type %>GiaTriTruLai").val();
						        data["selHangCua"] = $(form + " #sel<%=type %>HangCua").val();
						        data["selLoaiHang"] = $(form + " #sel<%=type %>LoaiHang").val();

						        //data["selHopDongUyThac"] = $(form + " #sel<%=type %>HopDongUyThac").val();

						        data["arrayPhieuXuat"] = $('#grid_ds_xuatkho_<%=type %>').getGridParam('selarrrow').join(", ");
						        $.ajax({
						            url: "Controllers/TaoPackingListController.ashx",
						            type: "POST",
						            data: { p: data },
						            error: function (rs) {
						                finallyDLG();
						                Popup("Error", 450, 300, rs.responseText);
						            },
						            success: function (rs) {
						                finallyDLG();
						                $(`#${idDlgTINV}`).find('#dialog_caution').html(rs);
						            }
						        });

						    }
						},
						{
						    id: `btn_close${idDlgTINV}`,
						    text: "Không",
						    click: function () {
						        $(this).dialog("destroy").remove();
						    }
						}
					]
                });
                }
        });
    });
</script>
<div style="width: 990px; margin: auto">
    <table id="frmFrom<%=type %>" class="frm-packinglist" style="width: 100%; text-align: left">
        <tr>
            <td>
                <input id="txt<%=type %>SoPackingList" name="txt<%=type %>SoPackingList" type="hidden" required /></td>
            <td>
                <input id="txt<%=type %>SoInvoice" name="txt<%=type %>SoInvoice" type="hidden" /></td>
        </tr>

        <tr>
            <td>Ngày Lập</td>
            <td>:</td>
            <td>
                <div class="datetime">
                    <input id="txt<%=type %>NgayLap" name="txt<%=type %>NgayLap" type="text" />
                </div>
            </td>
            <td>Số Tàu(M/V)<b style="color: red">(*)</b></td>
            <td>:</td>
            <td>
                <input id="txt<%=type %>SoTau" name="txt<%=type %>SoTau" type="text" required /></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>Nơi Đến(To)<b style="color: red">(*)</b></td>
            <td>:</td>
            <td>
                <input id="txt<%=type %>NoiDen" name="txt<%=type %>NoiDen" required />
            </td>
        </tr>
        <tr>
            <td>Số Vận Đơn(BL. No)</td>
            <td>:</td>
            <td>
                <input id="txt<%=type %>SoVanDon" name="txt<%=type %>SoVanDon" type="text" /></td>
            <td>Ngày Vận Đơn</td>
            <td>:</td>
            <td>
                <div class="datetime">
                    <input id="txt<%=type %>NgayVanDon" name="txt<%=type %>NgayVanDon" type="text" />
                </div>
            </td>
        </tr>
        <tr>
            <td>Diễn Giải Cộng Thêm</td>
            <td>:</td>
            <td>
                <input id="txt<%=type %>DienGiaiCongThem" name="txt<%=type %>DienGiaiCongThem" type="text" /></td>
            <td>Giá Trị Cộng Thêm</td>
            <td>:</td>
            <td>
                <input id="txt<%=type %>GiaTriCongThem" name="txt<%=type %>GiaTriCongThem" type="text" value="0" /></td>
        </tr>

        <tr>
            <td>Diễn Giải Trừ Lại</td>
            <td>:</td>
            <td>
                <input id="txt<%=type %>DienGiaiTruLai" name="txt<%=type %>DienGiaiTruLai" type="text" /></td>
            <td>Giá Trị Trừ Lại</td>
            <td>:</td>
            <td>
                <input id="txt<%=type %>GiaTriTruLai" name="txt<%=type %>GiaTriTruLai" type="text" value="0" /></td>
        </tr>

        <tr>
            <td>Hàng của</td>
            <td>:</td>
            <td>
                <select id="sel<%=type %>HangCua" name="sel<%=type %>HangCua">
                    <option value="VG">Vinh Gia</option>
                    <option value="HOANGHUYHUNG">Hoàng Huy Hưng</option>
                </select>
            </td>
            <td>Loại</td>
            <td>:</td>
            <td>
                <select id="sel<%=type %>LoaiHang" name="sel<%=type %>LoaiHang">
                    <option value="Xuất khẩu">Xuất khẩu</option>
                    <option value="Nhận ủy thác">Nhận ủy thác</option>
                </select>
            </td>
        </tr>

        <%--<tr>
            <td></td>
            <td></td>
            <td></td>
            <td>Hợp đồng ủy thác</td>
            <td>:</td>
            <td>
                <input id="txt<%=type %>HopDongUyThac" name="txt<%=type %>HopDongUyThac" type="text" /></td>
        </tr>--%>

        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td rowspan="5" class="submit">
                <button id="btn_add_from_<%=type %>">Tạo Packing List - Invoice</button>
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
</div>
