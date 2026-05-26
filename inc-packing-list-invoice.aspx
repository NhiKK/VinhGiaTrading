<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-packing-list-invoice.aspx.cs" Inherits="inc_packing_list_invoice" %>

<%@ Register Src="jqGrid.ascx" TagName="jqGrid" TagPrefix="uc1" %>
<script>
    $(function () {
        $('#lay-center-pklivn').parent().layout({
            south: {
                size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function () {
                    var h = $("#lay-south-pklivn-detail").height();
                    // sett height
                    $("#grid_ds_chitietplivn").setGridHeight(h + 4 - 80);
                    $("#grid_ds_chitietphanboplivn").setGridHeight(h + 4 - 80);
                }
            },
            center: {
                onresize_end: function () {
                    var h = $("#lay-center-pklivn").height();
                    $("#grid_ds_plivn").setGridHeight(h - 90);
                }
            }
        });

        $('#lay-south-pklivn-detail').layout({
            center: {
                onresize_end: function () {
                    var w = $("#tab-pklivn-detail").width();
                    //set width
                    $("#grid_ds_chitietplivn").setGridWidth(w);
                    $("#grid_ds_chitietphanboplivn").setGridWidth(w);
                }
            },
            east: {
                size: 200
                , maxSize: "100%"
                , minSize: 0
            }
        });

        $('#tab-pklivn-detail').tabs();
    });

    function phanBoCocInvoice() {
        var id = $("#grid_ds_chitietphanboplivn").jqGrid('getGridParam', 'selrow');
        if (id != null) {
            $.ajax({
                url: "inc-phanbococinvoice.aspx?chitietphaithuid=" + id,
                type: "POST",
                data: {},
                error: function (rs) { },
                success: function (rs) {
                    var div = $("<div></div>");
                    div.appendTo($(document.body));
                    div.html(rs);
                    div.dialog({
                        title: "Phân bổ tiền cọc cho Invoice",
                        resizable: false,
                        modal: true,
                        width: 530,
                        close: function (event, ui) { $(this).dialog('destroy').remove(); }
                    });
                }
            });
        } else {
            alert("Chưa chọn dòng cần phân bổ!");
        }
    }

    function activePackingListInvoie() {
        var msg = "";
        var id = $('#grid_ds_plivn').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Bạn hãy chọn 1 Packing List - Invoice mà bạn muốn hiệu lực.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-packinglist\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-active-packinglist').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function () {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn hiệu lực Packing List - Invoice này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-packinglist\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-packinglist').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                    [
                        {
                            id: "btn-active-packinglist",
                            text: "Có",
                            click: function () {
                                $("#dialog-caution").ajaxStart(function () {
                                    $("#wait").css("display", "block");
                                    $("#btn-active-packinglist").button("disable");
                                    $("#btn-close").button("disable");
                                });

                                $("#dialog-caution").ajaxComplete(function () {
                                    $("#wait").css("display", "none");
                                    $("#btn-close span").text("Thoát");
                                    $("#btn-close").button("enable");
                                });

                                $.get("Controllers/PackingInvoiceController.ashx?action=activepacking&pakId=" + id, function (result) {
                                    //$('#dialog-caution').css({ 'color': 'green' });
                                    $('#dialog-active-packinglist').find('#dialog-caution').html(result);
                                    $('#grid_ds_plivn').jqGrid().trigger('reloadGrid');
                                });
                            }
                        },
                        {
                            id: "btn-close",
                            text: "Không",
                            click: function () {
                                $(this).dialog("destroy").remove();
                            }
                        }
                    ]
            });
        }
    }

    function funcPrintPKLI() {
        $.get('Controllers/PartnerController.ashx?action=getoption&isncc=1', function (result) {
            let pId = $('#grid_ds_plivn').jqGrid('getGridParam', 'selrow');
            var hangcua = $('#grid_ds_plivn').jqGrid('getCell', pId, 'hangcua');
            var loaihang = $('#grid_ds_plivn').jqGrid('getCell', pId, 'xuatkhau_uythac');
            var stl = "visibility: hidden";
            if (loaihang == "Nhận ủy thác")
                stl = "visibility: visible"

            let name = 'dialog-print-pkli';
            let title = 'Trích xuất dữ liệu';
            let content = `
                <table style="width:100%">
                <tr>
                    <td>
                        <input id="printPkliList" type="checkbox" name="rdoPrintType" srt=1 value="printPkliList" />
                        <label for="printPkliList">In Sale Contract</label>
                    </td>

                    <td>
                        <input id="printPackingList" type="checkbox" name="rdoPrintType" srt=3 value="printPackingList" checked="checked" />
                        <label for="printPackingList">In Packing List</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="printInvoice" type="checkbox" name="rdoPrintType" srt=2 value="printInvoice" />
                        <label for="printInvoice">In Invoice</label>
                    </td>

                    <td>
                        <input id="printHQ" type="checkbox" name="rdoPrintType" srt=4 value="printHQ" />
                        <label for="printHQ">In Tờ Khai Hải Quan</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="printKhaiHQ" type="checkbox" name="rdoPrintType" srt=5 value="printKhaiHQ" />
                        <label for="printKhaiHQ">In Danh Mục Khai Hải Quan (Mẫu 1)</label>
                    </td>
                    <td>
                        <input id="printPaymentRequest" type="checkbox" name="rdoPrintType" srt=8 value="printPaymentRequest" />
                        <label for="printPaymentRequest">In mẫu payment request</label>
                    </td>
                </tr>

                <tr>
                    <td>
                        <input id="printKhaiHQ02" type="checkbox" name="rdoPrintType" srt=6 value="printKhaiHQ02" />
                        <label for="printKhaiHQ02">In Danh Mục Khai Hải Quan (Mẫu 2)</label>
                    </td>
                    <td>
                        <input id="printBL" type="checkbox" name="rdoPrintType" srt=9 value="printBL" />
                        <label for="printBL">In Mẫu BL</label>
                        <div id="md_ncu_id" style="display:none">' + result + '</div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <input id="printKhaiHQ03" type="checkbox" name="rdoPrintType" srt=7 value="printKhaiHQ03" />
                        <label for="printKhaiHQ03">In Danh Mục Khai Hải Quan (HScode)</label>
                    </td>
                    <td>
                        <input id="printHopDongUyThac" type="checkbox" name="rdoPrintType" srt=11 value="printHopDongUyThac" style=${stl} />
                        <label for="printHopDongUyThac" style=${stl} >In Hợp Đồng Ủy Thác </label>
                    </td>
                </tr>

                </table>
            `;

            $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
            $('#' + name).dialog({
                width: 500
                , modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: [
                    {
                        id: "btn-print-ok",
                        text: "In",
                        click: function () {
                            let printTypes = $('input[name=rdoPrintType]:checked', '#dialog-print-pkli');
                            let windowSize = "width=700,height=700,scrollbars=yes";
                            if (!pId) {
                                alert('Bạn chưa chọn packing lsit - invoice cần in phiếu');
                                return;
                            }

                            let arrLink = [];
                            printTypes.sort(function(lhs, rhs){
                              return parseInt($(lhs).attr("srt"),10) - parseInt($(rhs).attr("srt"),10);
                           });

                            printTypes.each(function () {
                                let printURL = "";
                                let printType = $(this).val();
                                if (typeof printType != 'undefined') {
                                    if (printType == "printPackingList") {
                                        printURL += "PrintControllers/InPackingList/";
                                    }
                                    else if (printType == 'printInvoice') {
                                        printURL += "PrintControllers/InInvoice/";
                                    }
                                    else if (printType == 'printHQ') {
                                        printURL += "PrintControllers/InToKhaiHQ/";
                                    }
                                    else if (printType == 'printKhaiHQ') {
                                        printURL += "PrintControllers/InDanhMucKhaiHQ/";
                                    }
                                    else if (printType == 'printKhaiHQ02') {
                                        printURL += "PrintControllers/InDanhMucKhaiHQ02/";
                                    }
                                    else if (printType == 'printBL') {
                                        printURL += "PrintControllers/InBL/";
                                    }
                                    else if (printType == 'printPaymentRequest') {
                                        printURL += "SendMail/PaymentRequest.aspx";
                                    }
                                    else if (printType == 'printKhaiHQ03') {
                                        printURL += "PrintControllers/InDanhMucKhaiHQ03/";
                                    }
                                    else if (printType == 'printHopDongUyThac') {
                                        printURL += "PrintControllers/InHopDongUyThac/";
                                    }
                                    else {
                                        printURL += "PrintControllers/InSaleContact/";
                                    }
                                    arrLink.push(printURL);
                                }
                                else {
                                    alert('Hãy chọn cách in.!');
                                }
                            });

                            window.open(`PrintControllers/More/combine_print.aspx?arr=${arrLink.join(',')}&pkId=${pId}&hangcua=${hangcua}&dtId=${$('#md_ncu_id').find('select').val()}`, "In Phiếu", windowSize);
                        }
                    }
                    , {
                        id: "btn-print-close",
                        text: "Thoát",
                        click: function () {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
        });
    }

    function sendMailPKLINV() {
        var pklId = $('#grid_ds_plivn').jqGrid('getGridParam', 'selrow');
        if (pklId == null) {
            alert('Hãy chọn một Packing List - Invoice mà bạn muốn gửi email!');
        } else {
            window.open("SendMail/PaymentRequest.aspx?pklId=" + pklId, "Gửi email");
        }
    }

    function ReturnPKL() {
        if (vnnIsAdmin != 'True') {
            alert('Chỉ có quản trị viên được sử dụng chức năng này.');
            return;
        }

        var msg = "";
        var id = $('#grid_ds_plivn').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Hãy chọn 1 PKL mà bạn muốn trả PKL này về soạn thảo.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-trave-PKL\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-trave-PKL').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function () {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn trả PKL này về soạn thảo.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-trave-PKL\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-trave-PKL').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function () {
                            $("#dialog-caution").ajaxStart(function () {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function () {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/PackingInvoiceController.ashx?action=ReturnPKL&pakId=" + id, function (result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-trave-PKL').find('#dialog-caution').html(result);
                                $('#grid_ds_plivn').jqGrid().trigger('reloadGrid');
                            });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Không",
                        click: function () {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
        }
    }
</script>
<div class="ui-layout-center ui-widget-content" id="lay-center-pklivn">
    <uc1:jqGrid ID="grid_ds_plivn"
        Caption="Packing List - Invoice"
        SortName="pi.ngaytao"
        BtnPrint="funcPrintPKLI"
        FuncActive="activePackingListInvoie"
        FuncSendMail="sendMailPKLINV"
        FuncTaoPKLCungKH="function(){ add_tab('Tạo Packing List - Invoice Cùng Khách Hàng', 'inc-tao-chungtuxuathang-cungkh.aspx')}"
        FuncTaoPKLCungPO="function(){ add_tab('Tạo Packing List - Invoice Cùng PO', 'inc-tao-chungtuxuathang-cungpo.aspx')}"
        ReturnPKL="ReturnPKL"
        UrlFileAction="Controllers/PackingInvoiceController.ashx"
        ColNames="['c_packinginvoice_id', 'TT' , 'Gửi Request', 'Số Packing List', 'Số Invoice', 'Đơn Hàng', 'Người Tạo'
                     , 'Ngày Lập', 'Người Lập'
                     , 'Consignee', 'Notifparty', 'Alsonotifyparty', 'Mv'
                     , 'Ngày Vận Đơn', 'Nơi Đi', 'Nơi Đến', 'BLNo', 'Commodity', 'Discount', 'Handling Fee'
                     , 'Diễn Giải Cộng' , 'Giá Trị Cộng', 'Diễn Giải Trừ', 'Giá Trị Trừ'
                     , 'Diễn Giải Cộng PO', 'Giá Trị Cộng PO', 'Diễn Giải Trừ PO', 'Giá Trị Trừ PO', 'Additional packaging fees'
                     , 'Comoodity VN', 'Ngày Mở Tờ Khai', 'Ngày Phải TT'
                     , 'Total Dis' , 'Total Net', 'Total Gross'
					 , 'Tiền đặt cọc','Tiền đã trả','Tiền còn lại'
                     , 'Hoa Hồng', 'Giá Trị Hoa Hồng'
                     , 'Số tờ khai', 'Thông tin khủ trùng', 'Cân cont', 'Ghi chú', 'Nhà vận chuyển', 'CRD'
                     , 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật'
                     , 'Người Cập Nhật', 'Mô tả', 'Hoạt động', 'Tỷ Giá USD-VND', 'Hàng của', 'Loại hàng', 'Hợp đồng ủy thác']"
        RowNumbers="true"
        OndblClickRow="function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                
                width:900
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs); 
                }
                
            }); }"
        ColModel="[
            {
                fixed: true, name: 'c_packinginvoice_id'
                , index: 'c_packinginvoice_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_trangthai_id'
                 , index: 'md_trangthai_id'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , align: 'center'
                 , formatter: 'imagestatus'
            },
            {
                fixed: true, name: 'guirequest'
                , index: 'guirequest'
                , width: 100
                , editable: true
                , width: 100
                , edittype: 'checkbox'
                , editoptions: {value: 'True:False', defaultValue: 'False'}
                , align:'center'
                , search : true
                , stype : 'select'
                , unformat: disable_formatter
                , formatter: imgCheckBox
                , formoptions: { colpos: 3, rowpos: 11 }
                , searchoptions:{sopt:['bw'], value:':Tất cả;1:Có;0:Không' }
            },
            {
                 fixed: true, name: 'so_pkl'
                 , index: 'so_pkl'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 1 }
            },
            {
                 fixed: true, name: 'so_inv'
                 , index: 'so_inv'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 2 }
            },
            {
                 fixed: true, name: 'sodh'
                 , index: 'sodh.chungtu'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 2 }
            },
			{
                fixed: true, name: 'nguoitao1'
                , index: 'nguoitao'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editrules:{ edithidden : true }
				, editoptions: { readonly: 'readonly' }
				, formoptions: { colpos: 1, rowpos: 3 }
            },
            {
                 fixed: true, name: 'ngaylap'
                 , index: 'ngaylap'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 1, rowpos: 4 }
            },
            {
                 fixed: true, name: 'nguoilap'
                 , index: 'nguoilap'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 5 }
            },
            {
                 fixed: true, name: 'consignee'
                 , index: 'consignee'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 6 }
            },
            {
                 fixed: true, name: 'notifparty'
                 , index: 'notifparty'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 7 }
            },
            {
                 fixed: true, name: 'alsonotifyparty'
                 , index: 'alsonotifyparty'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 8 }
            },
            {
                 fixed: true, name: 'mv'
                 , index: 'mv'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 9 }
            },
            {
                 fixed: true, name: 'etd'
                 , index: 'etd'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 1, rowpos: 10 }
            },
            {
                 fixed: true, name: 'noidi'
                 , index: 'noidi'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PortController.ashx?action=getoption' }
                 , formoptions: { colpos: 1, rowpos: 11 }
            },
            {
                 fixed: true, name: 'noiden'
                 , index: 'noiden'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 12 }
            },
            {
                 fixed: true, name: 'blno'
                 , index: 'blno'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 1 }
            },
            {
                 fixed: true, name: 'commodity'
                 , index: 'commodity'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 2 }
            },
            {
                 fixed: true, name: 'discount'
                 , index: 'discount'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0', readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 3 }
            },
            {
                 fixed: true, name: 'handling_fee'
                 , index: 'handling_fee'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0' }
                 , formoptions: { colpos: 2, rowpos: 4 }
            },
            {
                 fixed: true, name: 'diengiai_cong'
                 , index: 'diengiai_cong'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 5 }
            },
            {
                 fixed: true, name: 'giatri_cong'
                 , index: 'giatri_cong'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0' }
                 , formoptions: { colpos: 2, rowpos: 6 }
            },
            {
                 fixed: true, name: 'diengiai_tru'
                 , index: 'diengiai_tru'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 7 }
            },
            {
                 fixed: true, name: 'giatri_tru'
                 , index: 'giatri_tru'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0' }
                 , formoptions: { colpos: 2, rowpos: 8 }
            },
            {
                 fixed: true, name: 'diengiaicong_po'
                 , index: 'diengiaicong_po'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 3, rowpos: 5 }
                 , editoptions:{ readonly: 'readonly' }
            },
            {
                 fixed: true, name: 'giatricong_po'
                 , index: 'giatricong_po'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0' }
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 3, rowpos: 6 }
            },
            {
                 fixed: true, name: 'diengiaitru_po'
                 , index: 'diengiaitru_po'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 3, rowpos: 7 }
            },
            {
                 fixed: true, name: 'giatritru_po'
                 , index: 'giatritru_po'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0' }
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos:3, rowpos: 8 }
            },
            {
                 fixed: true, name: 'cpdg_vuotchuan'
                 , hidden: true
                 , index: 'cpdg_vuotchuan'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 3, rowpos: 4 }
            },
            {
                 fixed: true, name: 'commodityvn'
                 , index: 'commodityvn'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 9 }
            },
            {
                 fixed: true, name: 'ngay_motokhai'
                 , index: 'ngay_motokhai'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 2, rowpos: 10 }
            },
            {
                 fixed: true, name: 'ngay_phaitt'
                 , index: 'ngay_phaitt'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 2, rowpos: 11 }
            },
            {
                 fixed: true, name: 'totaldis'
                 , index: 'totaldis'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 12 }
            },
            {
                 fixed: true, name: 'totalnet'
                 , index: 'totalnet'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 13 }
            },            
            {
                 fixed: true, name: 'totalgross'
                 , index: 'totalgross'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 14 }
            },
			{
                 fixed: true, name: 'tiendatcoc'
                 , index: 'tiendatcoc'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ defaultValue: '0' }
                 , formoptions: { colpos: 2, rowpos: 15 }
            },
			{
                 fixed: true, name: 'tiendatra'
                 , index: 'tiendatra'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 16 }
            },
			{
                 fixed: true, name: 'tienconlai'
                 , index: 'tienconlai'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 17 }
            },            
            {
                 fixed: true, name: 'phantramhoahong'
                 , index: 'phantramhoahong'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 3, rowpos: 9 }
            },            
            {
                 fixed: true, name: 'hoahongphaitra'
                 , index: 'hoahongphaitra'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 3, rowpos: 10 }
            },
            {
                fixed: true, name: 'sotokhai'
                , index: 'sotokhai'
                , editable: true
                , width: 100
                , edittype: 'text'
                , formoptions: { colpos: 1, rowpos: 13 }
            },
            {
                fixed: true, name: 'ttkhutrung'
                , index: 'ttkhutrung'
                , width: 100
                , editable:true
                , edittype: 'text'
                , formoptions: { colpos: 1, rowpos: 14 }
            },
            {
                fixed: true, name: 'cancont'
                , index: 'cancont'
                , width: 100
                , editable:true
                , edittype: 'text'
                , formoptions: { colpos: 1, rowpos: 15 }
            },
            {
                fixed: true, name: 'ghichu'
                , index: 'ghichu'
                , width: 100
                , editable:true
                , edittype: 'text'
                , formoptions: { colpos: 1, rowpos: 16 }
            },
            {
                fixed: true, name: 'nhavanchuyen'
                , index: 'nhavanchuyen'
                , width: 100
                , editable:true
                , edittype: 'text'
                , formoptions: { colpos: 1, rowpos: 17 }
            },
            {
                fixed: true, name: 'crd'
                , index: 'crd'
                , width: 100
                , editable:true
                , edittype: 'text'
                , formoptions: { colpos: 1, rowpos: 18 }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'mota'
                , index: 'mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , formoptions: { colpos: 2, rowpos: 18 }
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'tygiaUSD_VND', hidden: false
                , index: 'tygiaUSD_VND'
                , width: 100
                , editable:false
                , align: 'center'
            },
            {
                fixed: true, name: 'hangcua', hidden: false
                , index: 'hangcua'
                , width: 100
                , editable:true
                , align: 'center'
                , stype : 'select'
                , formoptions: { colpos: 3, rowpos: 12 }
                , edittype: 'select'
				, editoptions: { value:'VG:Vinh Gia;HOANGHUYHUNG:Hoàng Huy Hưng' }
				, searchoptions:{sopt:['eq'], value:':[ALL];VG:Vinh Gia;HOANGHUYHUNG:Hoàng Huy Hưng' }

            },
            {
                fixed: true, name: 'xuatkhau_uythac', hidden: false
                , index: 'xuatkhau_uythac'
                , width: 100
                , editable:true
                , align: 'center'
                , formoptions: { colpos: 3, rowpos: 13 }
                , edittype: 'select'
                , stype : 'select'
				, editoptions: { value:'Xuất khẩu:Xuất khẩu;Nhận ủy thác:Nhận ủy thác' }
				, searchoptions:{sopt:['eq'], value:':[ALL];Xuất khẩu:Xuất khẩu;Nhận ủy thác:Nhận ủy thác' }
            },
            {
                fixed: true, name: 'hopdonguythac'
                , index: 'hopdonguythac'
                , width: 100
                , editable:true
                , edittype: 'text'
        		, editrules: { edithidden:false }
                , formoptions: { colpos: 3, rowpos: 14 }
            }
            ]"
        GridComplete="var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
        FilterToolbar="true"
        Height="420"
        MultiSelect="false"
        ShowEdit="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
        ShowDel="<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
        EditFormOptions=" 
                width:900
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                    $('#hopdonguythac','#' + formid.attr('id')).prop('disabled', true);
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs); 
                }
            "
        ViewFormOptions="
                width:900
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs); 
                }
            "
        DelFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs); 
                }
            "
        OnSelectRow="
            function(ids) {
		        if(ids == null) {
			        ids=0;
				} 
                else {
					$.ajax({
                        url: 'Controllers/PackingInvoiceController.ashx?action=getinvoiceinformation&pakId='+ids,
                        beforeSend:function(){
                            $('#grid_ds_plivn').jqGrid('setCaption','đang tải...');
                        },
                        success: function(data){
                            $('#grid_ds_plivn').jqGrid('setCaption', data);	
                        }
                    }); 			
		        }

                $('#grid_ds_chitietplivn').jqGrid('getGridParam', 'postData').pklId = ids;
				$('#grid_ds_chitietplivn')[0].triggerToolbar();
                $('#grid_ds_chitietphanboplivn').jqGrid('getGridParam', 'postData').pklId = ids;
				$('#grid_ds_chitietphanboplivn')[0].triggerToolbar();
            }
        "
        runat="server" />
</div>

<div class="ui-layout-south" id="lay-south-pklivn-detail">
    <div class="ui-layout-center" id="tab-pklivn-detail">
        <ul>
            <li><a href="#nav-tabs-pklivn1">Chi Tiết Packing List - Invoice</a></li>
            <li><a href="#nav-tabs-pklivn2">Chi tiết các khoản</a></li>
        </ul>
        <div id="nav-tabs-pklivn1">
            <uc1:jqGrid ID="grid_ds_chitietplivn"
                SortName="ma_sanpham"
                SortOrder="asc"
                UrlFileAction="Controllers/DongPackingListInvoiceController.ashx"
                ColNames="['c_dongpklinv_id', 'Packing List Invoice', 'TT', 'STT', 'Mã Sản Phẩm', 'Mã Sản Phẩm', 'Mô Tả Tiếng Anh', 'Mã Sản Phẩm Khách', 'Số Lượng', 'Giá', 'Thành Tiền',  'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                RowNumbers="true"
                ColModel="[
                    {
                        fixed: true, name: 'c_dongpklinv_id'
                        , index: 'c_dongpklinv_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                        , key: true
                    },
                    {
                            fixed: true, name: 'c_packinginvoice_id'
                            , index: 'c_packinginvoice_id'
                            , editable: true
                            , width: 100
                            , edittype: 'text'
                            , hidden: true
                    },
                    {
                        fixed: true, name: 'trangthai'
                        , index: 'sp.trangthai'
                        , width: 100
                        , editable:false
                        , formatter:'imagestatus'
                        , align: 'center'
                        , search: false
                        , viewable: false
                        , sortable: false
                    },
                    {
                        fixed: true, name: 'line'
                        , index: 'line'
                        , width: 50
                        , edittype: 'text'
                        , editable:true
                        , align:'right'
                    },
                    {
                        fixed: true, name: 'sanpham_id'
                        , index: 'sanpham_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                    },
                    {
                        fixed: true, name: 'ma_sanpham'
                        , index: 'ma_sanpham'
                        , width: 100
                        , edittype: 'text'
                        , editable:true
                        , editoptions: { 
                            dataInit : function (elem) { 
                                $(elem).combogrid({
                                    searchIcon: true,
                                    width: '480px',
                                    url: 'Controllers/ProductController.ashx?action=getcombogrid',
                                    colModel: [
                                        { 'columnName': 'md_sanpham_id', 'width': '0', 'label': 'ID', hidden:true }
                                        , { 'columnName': 'ma_sanpham', 'width': '50', 'label': 'Mã Sản Phẩm', 'align':'left'}
                                        , { 'columnName': 'mota_tiengviet', 'width': '50', 'label': 'Mô Tả Tiếng Việt' , 'align':'left'}],
                                        select: function(event, ui) {
                                            $(elem).val(ui.item.ma_sanpham);
                                            $('#sanpham_id').val(ui.item.md_sanpham_id);
                                            return false;
                                        }
                                });
                            } 
                        }
                    },
                    {
                        fixed: true, name: 'mota_tienganh'
                        , index: 'mota_tienganh'
                        , width: 200
                        , edittype: 'text'
                        , editable:true
                    },
                    {
                        fixed: true, name: 'ma_sanpham_khach'
                        , index: 'dpk.ma_sanpham_khach'
                        , width: 100
                        , edittype: 'text'
                        , editable:true
                    },
                    {
                        fixed: true, name: 'soluong'
                        , index: 'soluong'
                        , width: 100
                        , edittype: 'text'
                        , editable:true
                        , editrules: { required:true, number:true, minValue:0 }
                        , editoptions:{ defaultValue:'0' }
                        , align:'right'
                        , formatter:'numberic'
                        , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
                    },
                    {
                        fixed: true, name: 'gia'
                        , index: 'gia'
                        , width: 100
                        , edittype: 'text'
                        , editable:true
                        , editrules: { required:true, number:true, minValue:0 }
                        , editoptions:{ defaultValue:'0' }
                        , align:'right'
                        , formatter:'currency'
                        , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
                    },
                    {
                        fixed: true, name: 'thanhtien'
                        , index: 'thanhtien'
                        , width: 100
                        , edittype: 'text'
                        , editable:false
                        , align:'right'
                        , formatter:'currency'
                        , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
                    },
                    {
                        fixed: true, name: 'ngaytao'
                        , index: 'ngaytao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true 
                        , editrules:{ edithidden : true }
                    },
                    {
                        fixed: true, name: 'nguoitao'
                        , index: 'nguoitao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true 
                        , editrules:{ edithidden : true }
                    },
                    {
                        fixed: true, name: 'ngaycapnhat'
                        , index: 'ngaycapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true 
                        , editrules:{ edithidden : true }
                    },
                    {
                        fixed: true, name: 'nguoicapnhat'
                        , index: 'nguoicapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true 
                        , editrules:{ edithidden : true }
                    },
                    {
                        fixed: true, name: 'mota'
                        , index: 'mota'
                        , width: 500
                        , editable:true
                        , edittype: 'textarea'
                    },
                    {
                        fixed: true, name: 'hoatdong', hidden: true
                        , index: 'hoatdong'
                        , width: 100
                        , editable:true
                        , edittype: 'checkbox'
                        , align: 'center'
                        , editoptions:{ value:'True:False', defaultValue: 'True' }
                        , formatter: 'checkbox'
                    }
                    ]"
                GridComplete="var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 95);  $(this).setGridWidth($(o).width()); "
                FilterToolbar="true"
                Height="420"
                MultiSelect="false"
                ViewFormOptions="
                    beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                    },
                    errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                "
                runat="server" />
        </div>

        <div id="nav-tabs-pklivn2">
            <uc1:jqGrid ID="grid_ds_chitietphanboplivn"
                FilterToolbar="true"
                SortName="cttc.c_chitietthuchi_id"
                UrlFileAction="Controllers/ChiTietThuChiPhanBoController.ashx"
                ColNames="['c_chitietthuchi_id', 'Thu Chi', 'TK Nợ', 'TK Có', 'Số Tiền', 'Số Tiền Bao Gồm Phí', 'Quy Đổi', 'Diễn Giải',  'P/O', 'Packing list/Invoice', 'Là tiền cọc', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                RowNumbers="true"
                FuncPhanBoCocInv="phanBoCocInvoice"
                ColModel="[
            {
                fixed: true, name: 'c_chitietthuchi_id'
                , index: 'cttc.c_chitietthuchi_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'c_thuchi_id'
                 , index: 'cttc.c_thuchi_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/ThuChiController.ashx?action=getoption' }
                 , editrules:{ edithidden : true }
                 , hidden:true
            },
            {
                fixed: true, name: 'tk_no'
                , index: 'cttc.tk_no'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'tk_co'
                , index: 'cttc.tk_co'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sotien'
                , index: 'cttc.sotien'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'tien'
                , index: 'cttc.tien'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'quydoi'
                , index: 'cttc.quydoi'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'diengiai'
                , index: 'cttc.diengiai'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'c_donhang_id'
                , index: 'cttc.c_donhang_id'
                , width: 100
                , edittype: 'text'
                
            },
            {
                fixed: true, name: 'c_packinginvoice_id'
                , index: 'cttc.c_packinginvoice_id'
                , width: 100
                , edittype: 'text'
                
            },
            {
                fixed: true, name: 'isdatcoc'
                , index: 'cttc.isdatcoc'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , formatter: 'checkbox'
                , align: 'center'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'cttc.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'cttc.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'cttc.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'cttc.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'mota'
                , index: 'cttc.mota'
                , width: 100
                , editable: true
                , edittype: 'textarea'
                , hidden: true
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'cttc.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , editoptions:{ defaultValue: 'True' }
                , editrules:{ edithidden : true }
            }
            ]"
                GridComplete="var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 95); $(this).setGridWidth($(o).width()); "
                Height="420"
                MultiSelect="false"
                ShowAdd="false"
                ShowEdit="false"
                ShowDel="false"
                AddFormOptions=" 
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
                EditFormOptions="
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
                ViewFormOptions="
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }"
                DelFormOptions="
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
                runat="server" />
        </div>
    </div>
</div>

<%-- VNN --%>
<script>
    function capnhatphicongphitru() {
        var msg = "";
        var id = $('#grid_ds_plivn').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            alert('Hãy chọn một Packing List - Invoice mà bạn muốn cập nhật!');
        } else {
            msg = "Cập nhật phí cộng, phí trừ.";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-capnhatinvoice\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-capnhatinvoice').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                    [
                        {
                            id: "btn-active",
                            text: "Có",
                            click: function () {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");


                                $.get("Controllers/PackingInvoiceController.ashx?action=updatephicongphitru&pakId=" + id, function (result) {
                                    //$('#dialog-caution').css({ 'color': 'green' });
                                    $('#dialog-capnhatinvoice').find('#dialog-caution').html(result.split(':')[1]);
                                    $('#grid_ds_plivn').jqGrid().trigger('reloadGrid');
                                    setTimeout(function () {
                                        $("#grid_ds_plivn").jqGrid('setSelection', result.split(':')[0]);
                                    }, 1000);
                                    $("#wait").css("display", "none");
                                    $("#btn-close span").text("Thoát");
                                    $("#btn-close").button("enable");
                                });
                            }
                        },
                        {
                            id: "btn-close",
                            text: "Không",
                            click: function () {
                                $(this).dialog("destroy").remove();
                            }
                        }
                    ]
            });
        }
    }

    setTimeout(function () {
        $('#grid_ds_plivn-pager_left table tr').append(
            '<td onclick="capnhatphicongphitru()" class="ui-pg-button ui-corner-all" title="Cập nhật phí cộng phí trừ" id="capnhat_grid_ds_plivn">' +
            '<div class="ui-pg-div"><span class="ui-icon ui-icon-gear"></span></div><td>')
            , 5000
    });

    createRightPanel('grid_ds_plivn');
    createRightPanel('grid_ds_chitietplivn');
    createRightPanel('grid_ds_chitietphanboplivn');
</script>

<style type="text/css">
    #capnhat_grid_ds_plivn:hover {
        border: 1px solid #f5ad66;
        background: #f5f0e5 url(images/ui-bg_glass_100_f5f0e5_1x400.png) 50% 50% repeat-x;
        font-weight: normal;
        color: #a46313;
        padding: 0;
    }

    #capnhat_grid_ds_plivn:active {
        cursor: wait;
    }
</style>

