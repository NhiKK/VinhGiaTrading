<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vnn-tool.aspx.cs" Inherits="vnn_tool" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<script>
    $(document).ready(function () {
        $('#lay-center-vnn_tool').parent().layout({
            west: {
                size: "20%"
            }
        });
        $('#lay-west-vnn_tool button').button().css({ 'width': '95%' });


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
        var frmID = '#frmvnn_tool';
        $("#tungayvnn_tool", frmID).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $("#tungayvnn_tool", frmID).datepicker('setDate', startDate);

        $("#denngayvnn_tool", frmID).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $("#denngayvnn_tool", frmID).datepicker('setDate', endDate);
    });



    function getQueryString() {
        var frmID = '#frmvnn_tool';
        var tungayvnn_tool = $("#tungayvnn_tool", frmID).val();
        var denngayvnn_tool = $("#denngayvnn_tool", frmID).val();
        var queryString = "?startdate=" + tungayvnn_tool + "&enddate=" + denngayvnn_tool;
        return queryString;
    }

    function createTool(a, stt) {
        var frmID = '#frmvnn_tool';
        var frmEl = $("#frmvnn_tool");
        var divRptEl = $("#reportviewer");
        var chuThich = frmEl.find('.chuthich');
        var thaotac2 = frmEl.find('.thaotac2');
        var startDate = $('#tungayvnn_tool');
        var endDate = $('#denngayvnn_tool');
        frmEl.attr('stt', stt);
        $('#lblReportNamevnn_tool').html($(a).text());
        frmEl.show();
        divRptEl.html('');
        var showEl = function() {
            startDate.parent().parent().show();
            endDate.parent().parent().show();
        }

        var hideEl = function() {
            startDate.parent().parent().hide();
            endDate.parent().parent().hide();
        }

        showEl();
        thaotac2.attr('onclick', 'submitRptTool(2)');

        switch (stt) {
            case 1:
                chuThich.html('Tìm kiếm theo ngày hết hạn QO');
                thaotac2.val('Kết thúc QO');
                break;
            case 2:
                chuThich.html('Tìm kiếm theo shipmentime PO');
                thaotac2.val('Kết thúc PO');
                break;
            case 3:
                chuThich.html('Tìm kiếm theo ngày ngưng hoạt động');
                thaotac2.val('Xóa mã hàng');
                break;
            case 4:
                chuThich.html('Tìm kiếm tất cả màu sắc ngưng hoạt động');
                thaotac2.val('Xóa/Hủy màu sắc');
                thaotac2.attr('onclick', 'submitRptTool(3)');
                hideEl();
                break;
            case 5:
                chuThich.html('Tìm kiếm tất cả đề tài ngưng hoạt động');;
                thaotac2.val('Xóa/Hủy đề tài');
                thaotac2.attr('onclick', 'submitRptTool(3)');
                hideEl();
                break;
            case 6:
                chuThich.html('Tìm kiếm tất cả chủng loại ngưng hoạt động');
                thaotac2.val('Xóa/Hủy chủng loại');
                thaotac2.attr('onclick', 'submitRptTool(3)');
                hideEl();
                break;
            case 7:
                frmEl.hide();
                $('#reportviewer').html('<div id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>');
                $('#reportviewer').load('Tool/TuyChinhMauIn.aspx', function () {

                });
                break;
            case 8:
                frmEl.hide();
                $('#reportviewer').html('<div id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>');
                $('#reportviewer').load('Tool/TuyChinhGuiMail.aspx', function () {

                });
                break;
            case 9:
                frmEl.hide();
                $('#reportviewer').html('<div id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>');
                $('#reportviewer').load('Tool/DoiGiaDonHangDaHieuLuc.aspx', function () {

                });
                break;
        }
    }

    function submitRptTool(type) {
        var stt = $("#frmvnn_tool").attr('stt');
        var startdate = $('#tungayvnn_tool').val();
        var enddate = $('#denngayvnn_tool').val();
        var title = "", url = "", xoabtn = "", huybtn = "", display="", textMH = "Mã hàng", textTheoMa = "Theo mã";
        switch (stt) {
            case '1':
                title = "Xem DS/Kết thúc QO";
                url = "Tool/KetThucQO.aspx";
				textMH = "Số báo giá";
				xoabtn = "Thực hiện";
				textTheoMa = "Theo Q/O";
                break;
            case '2':
                title = "Xem DS/Kết thúc PO";
                url = "Tool/KetThucPO.aspx";
				textMH = "Số đơn hàng";
				xoabtn = "Thực hiện";
				textTheoMa = "Theo P/O";
                break;
            case '3':
                title = "Xem DS/Xóa mã hàng";
                url = "Tool/XoaHangHoaNHD.aspx";
                xoabtn = "Xóa mã hàng";
                huybtn = "Hủy mã hàng";
                break;
            case '4':
                title = "Xem DS/Xóa màu sắc";
                url = "Tool/XoaMauSacNHD.aspx";
                xoabtn = "Xóa màu sắc";
                huybtn = "Hủy màu sắc";
                break;
            case '5':
                title = "Xem DS/Xóa đề tài";
                url = "Tool/XoaDeTaiNHD.aspx";
                xoabtn = "Xóa đề tài";
                huybtn = "Hủy đề tài";
                break;
            case '6':
                title = "Xem DS/Xóa chủng loại";
                url = "Tool/XoaChungLoaiNHD.aspx";
                xoabtn = "Xóa chủng loại";
                huybtn = "Hủy chủng loại";
                break;
        }
		
		if(huybtn == "") {
			display = "display:none;";
		}
		
        if (type == 1) {
            actionRptTool(title, url, startdate, enddate, type);
        }
        else if (type == 2) {
            var windowSize = "width=700,height=700,scrollbars=yes";
            $('body').append(`
            <div title="${title}" id="dialog-actionTool">
                <form 
                    enctype="multipart/form-data" 
                    id="frm_KetThucXoa" 
                    action='${url}?type=${type}&startdate=${startdate}&enddate=${enddate}' 
                    target="exportEX_popup" 
					method="post" onsubmit="window.open('about:blank','exportEX_popup','${windowSize}');"
				>
                    <input type="hidden" name="typepost" />
					<table style="width:100%">
                        <tr>
                            <td>
                                <fieldset style="padding:10px 10px">
                                <legend>
                                     <input id="printFile" type="radio" name="rdoPrintType" value="printFile" />
                                     <label for="printFile">Theo tập tin</label>
                                </legend>
					            <input type="file" id="fileExcel" name="fileExcel" accept=".xls,.xlsx"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="padding:10px 10px">
                                <legend>
                                     <input id="printInput" type="radio" name="rdoPrintType" value="printInput" />
                                     <label for="printInput">${textTheoMa}</label>
                                </legend>
					            <table style="width:100%">
                                    <tr>
                                        <td>${textMH}<br><input type="text" id="MaHangKTX" name="MaHangKTX" class="inputCLDTMS" style="width: 90%;"/></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
				</form>
            </div>`
            );

            // create new dialog
            $('#dialog-actionTool').dialog({
                modal: true, 
                open: function (event, ui) {
                    var fileExcel = $('#fileExcel',nameParent);
                    var inputCLDTMS = $('.inputCLDTMS', nameParent);
                    var rdoPrintType = $('input[name="rdoPrintType"]', nameParent);
					rdoPrintType.click(function(){
						var val = $(this).val();
						if(val == 'printFile') {
                            inputCLDTMS.val('');
							fileExcel.prop('disabled', false);
                            inputCLDTMS.prop('disabled', true);
						}
						else {
                            fileExcel.val('');
							fileExcel.prop('disabled', true);
                            inputCLDTMS.prop('disabled', false);
						}
					});

                    rdoPrintType.first().click();
                },
                buttons: [
                    {
						id: "btn-print-cancel",
						text: huybtn,
						style: display,
						click: function () {
                            $('#frm_KetThucXoa').find('input[name="typepost"]').val('Huy');
						    $('#frm_KetThucXoa').submit();
						}
					},
                    {
						id: "btn-print-ok",
						text: xoabtn,
						click: function () {
                            $('#frm_KetThucXoa').find('input[name="typepost"]').val('Xoa');
						    $('#frm_KetThucXoa').submit();
						}
					},
                    {
						id: "btn-print-close",
						text: "Thoát",
						click: function () {
							$(this).dialog("destroy").remove();
						}
					}
                ]
            });
        }
        else if (type == 3) {
            var windowSize = "width=700,height=700,scrollbars=yes";
            $('body').append(`
            <div title="${title}" id="dialog-actionTool">
                <form 
                    enctype="multipart/form-data" 
                    id="frm_KetThucXoa" 
                    action='${url}?type=${type}' 
                    target="exportEX_popup" 
					method="post" onsubmit="window.open('about:blank','exportEX_popup','${windowSize}');"
				>
                    <input type="hidden" name="typepost" />
                    <table style="width:100%">
                        <tr>
                            <td>
                                <fieldset style="padding:10px 10px">
                                <legend>
                                     <input id="printFile" type="radio" name="rdoPrintType" value="printFile" />
                                     <label for="printFile">Theo tập tin</label>
                                </legend>
					            <input type="file" id="fileExcel" name="fileExcel" accept=".xls,.xlsx"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="padding:10px 10px">
                                <legend>
                                     <input id="printInput" type="radio" name="rdoPrintType" value="printInput" />
                                     <label for="printInput">Theo mã</label>
                                </legend>
					            <table style="width:100%">
                                    <tr>
                                        <td>Chủng loại<br><input type="text" id="chungloaiKTX" name="chungloaiKTX" class="inputCLDTMS" style="width: 90%;"/></td>
                                        <td>Đề tài<br><input type="text" id="detaiKTX" name="detaiKTX" class="inputCLDTMS" style="width: 90%;"/></td>
                                        <td>Màu sắc<br><input type="text" id="mausacKTX" name="mausacKTX" class="inputCLDTMS" style="width: 90%;"/></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
				</form>
            </div>`
            );

            // create new dialog
            var nameParent = '#dialog-actionTool';
            $('#dialog-actionTool').dialog({
                modal: true, 
                open: function (event, ui) {
                    var fileExcel = $('#fileExcel',nameParent);
                    var inputCLDTMS = $('.inputCLDTMS', nameParent);
                    var rdoPrintType = $('input[name="rdoPrintType"]', nameParent);
					rdoPrintType.click(function(){
						var val = $(this).val();
						if(val == 'printFile') {
                            inputCLDTMS.val('');
							fileExcel.prop('disabled', false);
                            inputCLDTMS.prop('disabled', true);
						}
						else {
                            fileExcel.val('');
							fileExcel.prop('disabled', true);
                            inputCLDTMS.prop('disabled', false);
						}
					});

                    rdoPrintType.first().click();
                    if(stt == 5) {
                        $('#mausacKTX', nameParent).parent().remove();
                    }
                    else if(stt == 6) {
                        $('#mausacKTX', nameParent).parent().remove();
                        $('#detaiKTX', nameParent).parent().remove();
                    }
                }, 
                buttons: [
                    {
						id: "btn-print-huy",
						text: huybtn,
						click: function () {
                            $('#frm_KetThucXoa').find('input[name="typepost"]').val('Huy');
						    $('#frm_KetThucXoa').submit();
						}
					},
                     {
						id: "btn-print-xoa",
						text: xoabtn,
						click: function () {
                            $('#frm_KetThucXoa').find('input[name="typepost"]').val('Xoa');
						    $('#frm_KetThucXoa').submit();
						}
					},
                    {
						id: "btn-print-close",
						text: "Thoát",
						click: function () {
							$(this).dialog("destroy").remove();
						}
					}
                ]
            });
        }
    }
 
    function actionRptTool(title, url, startdate, enddate, type) {
        var rptURL = url + "?startdate=" + startdate + "&enddate=" + enddate;
        if (type != "NULL") {
            rptURL += "&type=" + type;
        }
        console.error(rptURL);
        window.open(rptURL);
    }
</script>

<div style="background:#F4F0EC" class="ui-layout-center ui-widget-content" id="lay-center-vnn_tool">
    <form id="frmvnn_tool" style="display:none">
        <table>
            <tr>
                <td colspan="2"><h3 style="font-size:18px; padding:5px" id="lblReportNamevnn_tool"></h3></td>
            </tr>
            <tr>
                <td colspan=2 style="font-style:italic">
                    Chú thích:
                    <span class="chuthich">
                        Tìm kiếm
                    <span>
                </td>
            </tr>
            <tr style="height:5px">
                <td colspan=2></td>
            </tr>
            <tr>
                <td>Từ ngày</td>
                <td><input type="text" name="tungay" id="tungayvnn_tool"/></td>
            </tr>
            <tr>
                <td>Đến ngày</td>
                <td><input type="text" name="denngay" id="denngayvnn_tool"/></td>
            </tr>
            <tr style="height:10px">
                <td colspan=2></td>
            </tr>
            <tr>
                <td colspan=2 style="vertical-align: middle;text-align: center;">
                    <input class="thaotac1" type = "button" onclick="submitRptTool(1)" value="Xem danh sách"/>
                    <input type = "button" value="X" style="opacity:0"/>
                    <input class="thaotac2" type = "button" onclick="submitRptTool(2)" value="Kết thúc"/>
                </td>
            </tr>
        </table>
    </form>

    <div id="reportviewer">

    </div>
</div>

<div style="background:#F4F0EC;" class="ui-layout-west" id="lay-west-vnn_tool">
    <div class="ui-widget-content" style="height:100%; overflow:auto; text-align:center">
        <button style="text-align:left;" onclick="createTool(this, 1)">Kết thúc báo giá (QO)</button>
        <button style="text-align:left;" onclick="createTool(this, 2)">Kết thúc đơn hàng (PO)</button>
        <button style="text-align:left;" onclick="createTool(this, 3)">Xóa mã hàng đã NHĐ</button>
        <button style="text-align:left;" onclick="createTool(this, 4)">Xóa / Hủy màu sắc</button>
        <button style="text-align:left;" onclick="createTool(this, 5)">Xóa / Hủy đề tài</button>
        <button style="text-align:left;" onclick="createTool(this, 6)">Xóa / Hủy chủng loại</button>
        <button style="text-align:left;" onclick="createTool(this, 7)">Tùy chỉnh mẫu In</button>
        <button style="text-align:left;" onclick="createTool(this, 8)">Tùy chỉnh gửi mail</button>
        <button style="text-align:left;" onclick="createTool(this, 9)">Đổi giá đơn hàng đã HL</button>
    </div>
</div>

