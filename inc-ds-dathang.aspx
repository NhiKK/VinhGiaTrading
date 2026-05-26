<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-ds-dathang.aspx.cs" Inherits="inc_ds_dathang" %>

<%@ Register Src="jqGrid.ascx" TagName="jqGrid" TagPrefix="uc1" %>

<script>
    $(function () {
        $('#lay-center-dathang').parent().layout({
            south: {
                size: "50%"
            }
            , center: {
                onresize: function () {
                    let h = $("#lay-center-dathang").height();
                    $("#grid_ds_dathang").setGridHeight(h - 90);
                }
            }
        });

        // Layout nho duoi
        $('#lay-south-dathang').layout({
            center: {
                onresize: function () {
                    let o = $("#tabs-dathang-details");
                    $("#grid_ds_chitietdathang").setGridWidth(o.width());
                    $("#grid_phidathang").setGridWidth(o.width());

                    $("#grid_ds_chitietdathang").setGridHeight(o.height() - 97);
                    $("#grid_phidathang").setGridHeight(o.height() - 97);
                }
            }
        });

        $('#tabs-dathang-details').tabs();
    });


    function funcActivePI() {
        let msg = "";
        let id = $('#grid_ds_dathang').jqGrid('getGridParam', 'selarrrow');

        if (id == null) {
            msg = "Bạn hãy chọn 1 danh sách đặt hàng mà bạn muốn hiệu lực.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-pi\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-active-pi').dialog({
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
            var sochungtu = $('#grid_ds_dathang').jqGrid('getCell', id, 'sochungtu');
            msg = "Có phải bạn muốn hiệu lực danh sách đặt hàng.?";
            //msg = "Có phải bạn muốn hiệu lực danh sách đặt hàng '" + sochungtu + "'.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-pi\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-pi').dialog({
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

                                $.get("Controllers/DanhSachDatHangController.ashx?action=activepi&piId=" + id, function (result) {
                                    //$('#dialog-caution').css({ 'color': 'green' });
                                    $('#dialog-active-pi').find('#dialog-caution').html(result);
                                    $('#grid_ds_dathang').jqGrid().trigger('reloadGrid');
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

    //Xac Nhan PI
    function funcConfirmPI() {
        let msg = "";
        let id = $('#grid_ds_dathang').jqGrid('getGridParam', 'selarrrow');

        if (id == null) {
            msg = "Bạn hãy chọn 1 danh sách đặt hàng mà bạn muốn xác nhận.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-pi\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-active-pi').dialog({
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
            var sochungtu = $('#grid_ds_dathang').jqGrid('getCell', id, 'sochungtu');
            msg = "Có phải bạn muốn xác nhận danh sách đặt hàng.?";
            //msg = "Có phải bạn muốn hiệu lực danh sách đặt hàng '" + sochungtu + "'.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-pi\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-pi').dialog({
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

                                $.get("Controllers/DanhSachDatHangController.ashx?action=confirmpi&piId=" + id, function (result) {
                                    //$('#dialog-caution').css({ 'color': 'green' });
                                    $('#dialog-active-pi').find('#dialog-caution').html(result);
                                    $('#grid_ds_dathang').jqGrid().trigger('reloadGrid');
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
    //

    function printPI() {
        var piId = $('#grid_ds_dathang').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-pi';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
            '<tr>' +
            '<td>' +
            '<input id="printRequired" type="radio" name="rdoPrintType" value="printRequired" checked="checked" />' +
            '<label for="printRequired">Chiết xuất đơn đặt hàng</label>' +
            '</td>' +

            '<td>' +
            '<input id="printPoTem" type="radio" name="rdoPrintType" value="printPoTem" />' +
            '<label for="printPoTem">In chi tiết tem bao bì</label>' +
            '</td>' +
            '</tr>' +

            '<tr>' +
            '<td>' +
            '<input id="printST_DS" type="radio" name="rdoPrintType" value="printST_DS" />' +
            '<label for="printST_DS">Chiết xuất tất cả ds đặt hàng</label>' +
            '</td>' +


            '<td>' +
            '<input id="printPoBaoBi" type="radio" name="rdoPrintType" value="printPoBaoBi" />' +
            '<label for="printPoBaoBi">In bao bì</label>' +
            '</td>' +
            '</tr>' +

            '<tr>' +
            '<td>' +
            '<input id="printDSDH" type="radio" name="rdoPrintType" value="printDSDH" />' +
            '<label for="printDSDH">In đơn đặt hàng</label>' +
            '</td>' +

            '<td>' +
            '<input id="printPoXml" type="radio" name="rdoPrintType" value="printPoXml" />' +
            '<label for="printPoXml">In đơn hàng xml</label>' +
            '</td>' +


            '</tr>' +
            '</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
            width: 500,
            modal: true
            , open: function (event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
            , buttons: [
                {
                    id: "btn-print-ok",
                    text: "In",
                    click: function () {
                        var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-pi').val();
                        var printURL = "PrintControllers/";
                        var windowSize = "width=700,height=700,scrollbars=yes";
                        if (typeof printType != 'undefined') {
                            if (printType == "printRequired") {
                                if (piId != null) {
                                    printURL += "InDonDatHang/";
                                    let params_ = '';
                                    params_ += "&DSDHG=" + xemgiaDSDH;
                                    params_ += "&type=excel";
                                    window.open(printURL + "?c_danhsachdathang_id=" + piId + params_, "Chiết xuất Đơn Đặt Hàng", windowSize);
                                } else {
                                    alert('Bạn chưa chọn đơn đặt hàng cần in!');
                                }
                            } else if (printType == 'printPoTem') {
                                if (piId != null) {
                                    printURL += "InDonHangTemDSDH/";
                                    window.open(printURL + "?c_danhsachdathang_id=" + piId, "In Đơn Hàng Tem", windowSize);
                                } else {
                                    alert('Bạn chưa chọn danh sách đặt hàng cần in.!');
                                }
                            } else if (printType == 'printPoBaoBi') {
                                if (piId != null) {
                                    printURL += "InXacNhanBaoBiDSDH/";
                                    window.open(printURL + "?c_danhsachdathang_id=" + piId, "In Bao Bì", windowSize);
                                } else {
                                    alert('Bạn chưa chọn danh sách đặt hàng cần in.!');
                                }
                            } else if (printType == 'printDSDH') {
                                if (piId != null) {
                                    printURL += "InDonDatHang/";
                                    let params_ = '';
                                    params_ += "&DSDHG=" + xemgiaDSDH;
                                    params_ += "&type=pdf";

                                    windowSize = "top=0, left=0, width=" + window.outerWidth + ", height=" + window.outerHeight;
                                    window.open(printURL + "?c_danhsachdathang_id=" + piId + params_, "In Đơn Đặt Hàng", windowSize);
                                } else {
                                    alert('Bạn chưa chọn danh sách đặt hàng cần in.!');
                                }
                            } else if (printType == "printPoXml") {
                                if (piId != null) {
                                    printURL += "InDonHangXml/";
                                    window.open(printURL + "?c_danhsachdathang_id=" + piId, "In Đơn Hàng Xml", windowSize);
                                } else {
                                    alert('Bạn chưa chọn đơn đặt hàng cần chiết xuất xml!');
                                }
                            } else if (printType == "printST_DS") {
                                printURL += "InDSDH_Soanthao/";
                                window.open(printURL, "Chiết xuất tất cả các DSDH đang \"Soạn Thảo\"", windowSize);
                            }
                        } else {
                            alert('Hãy chọn cách in.!');
                        }
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
    }

    $(function () {
        $('#newKcs, #newNcu').change(function () {
            if ($(this).is(':checked')) {
                $('#Kcs').prop('checked', false);
                $('#Ncu').prop('checked', false);
            }
        });
    });

    function sendMailPI() {
        var piId = $('#grid_ds_dathang').jqGrid('getGridParam', 'selarrrow');
        if (piId == null) {
            alert('Hãy chọn một báo giá mà bạn muốn gửi email!');
        } else {

            $('body').append("<div title='Gửi Email' id='sendpi'>" + "<div id='content'>Đang kiểm tra...</div>" + "</div>");
            $('#sendpi').dialog({
                modal: true
                , width: 500
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: [
                    {
                        id: "btn-send",
                        text: "Gửi đi",
                        click: function () {
                            var sendType = $('input[name=rdType]:checked', '#sendpi').val();
                            var sendnewKcs = $('input[name=newKcs]:checked', '#sendpi').val();
                            var sendKcs = $('input[name=Kcs]:checked', '#sendpi').val();
                            var sendnewNcu = $('input[name=newNcu]:checked', '#sendpi').val();
                            var sendNcu = $('input[name=Ncu]:checked', '#sendpi').val();
                            var sendnewKTNCU = $('input[name=newKTNCU]:checked', '#sendpi').val();
                            var sendKTNCU = $('input[name=KTNCU]:checked', '#sendpi').val();
                            var sendMailType = "";
                            if (typeof sendType != 'undefined') {
                                if (sendnewKcs == null & sendKcs == null
                                    & sendnewNcu == null & sendNcu == null
                                    & sendnewKTNCU == null & sendKTNCU == null) {
                                    alert("Hãy chọn Người Gửi!");

                                }
                                else {
                                    $('#sendpi #content').html("Đang gửi email...");
                                    $("#btn-send").button("disable");
                                    $("#btn-close").button("disable");

                                    if (sendType == "toAll") {
                                        if (sendnewKcs == "newKcs") {
                                            sendMailType += sendnewKcs + ",";
                                        }

                                        if (sendnewNcu == "newNcu") {
                                            sendMailType += sendnewNcu + ",";
                                        }

                                        if (sendnewKTNCU == "newKTNCU") {
                                            sendMailType += sendnewKTNCU + ",";
                                        }
                                    }
                                    else {
                                        if (sendKcs == "Kcs") {
                                            sendMailType += sendKcs + ",";
                                        }

                                        if (sendNcu == "Ncu") {
                                            sendMailType += sendNcu + ",";
                                        }

                                        if (sendKTNCU == "KTNCU") {
                                            sendMailType += sendKTNCU + ",";
                                        }
                                    }

                                    $.get("Controllers/DanhSachDatHangController.ashx?action=sendmail&piId=" + piId + "&sendType=" + sendType + "&sendMailType=" + sendMailType, function (result) {
                                        $('#sendpi #content').html(result);
                                        $("#btn-close span").text("Thoát");
                                        $("#btn-close").button("enable");
                                        $('#grid_ds_dathang').jqGrid().trigger('reloadGrid');
                                    });
                                }

                            } else {
                                alert("Hãy chọn kiểu DSDH!");
                            }
                        }
                    }
                    , {
                        id: "btn-close",
                        text: "Thoát",
                        click: function () {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });

            $.get("Controllers/DanhSachDatHangController.ashx?action=startsendmail&piId=" + piId, function (result) {
                $('#sendpi').find('#content').html('<h2>Xác nhận trước khi gửi email!</h2><div>' + result + '</div>');
                hide_sendmail();
            });
        }
    }

    function LamMoiTrangThaiDDH() {
        $('body').append(`
			<div title='Làm mới trạng thái đơn mua hàng' id='lammoitrangthai'>
				<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>
				<div id='content'>Bạn muốn làm mới trạng thái đơn mua hàng?</div>
			</div>
		`);
        $('#lammoitrangthai').dialog({
            modal: true
            , width: 500
            , open: function (event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
            , buttons: [
                {
                    id: "btn-dongy",
                    text: "Đồng ý",
                    click: function () {
                        $('#wait').show();
                        $.get('Controllers/DanhSachDatHangController.ashx?action=danhsachdathang', function () {
                            $('#grid_ds_dathang').jqGrid().trigger('reloadGrid');
                            $('#wait').hide();
                            $('#lammoitrangthai').dialog("destroy").remove();
                        });
                    }
                }
                , {
                    id: "btn-close",
                    text: "Thoát",
                    click: function () {
                        $(this).dialog("destroy").remove();
                    }
                }
            ]
        });
    }

    function ReturnPI() {

        if (vnnIsAdmin != 'True') {
            alert('Chỉ có quản trị viên được sử dụng chức năng này.');
            return;
        }

        var msg = "";
        var id = $('#grid_ds_dathang').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Hãy chọn 1 đơn hàng mà bạn muốn trả đơn hàng này về soạn thảo.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-trave-pi\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-trave-pi').dialog({
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
            msg = "Có phải bạn muốn trả đơn hàng này về soạn thảo.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-trave-pi\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-trave-pi').dialog({
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

                                $.get("Controllers/DanhSachDatHangController.ashx?action=ReturnPI&piId=" + id, function (result) {
                                    //$('#dialog-caution').css({ 'color': 'green' });
                                    $('#dialog-trave-pi').find('#dialog-caution').html(result);
                                    $('#grid_ds_dathang').jqGrid().trigger('reloadGrid');
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

<script>
    function dupEditDanhSachDatHang(rowid) {
        <% if (DaoRules.GetRuleEdit(Context))
    { %>
        $(this).jqGrid('editGridRow', rowid, {
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter();
            }
            , afterSubmit: function (rs, postdata) {
                return showMsgDialog(rs);
            }
        }
        );
        <%} %>
    };
</script>

<script>
    function guidonhang() {
        var piId = $('#grid_ds_dathang').jqGrid('getGridParam', 'selarrrow');
        if (piId == null) {
            alert('Hãy chọn một báo giá mà bạn muốn gửi!');
        } else {

            $('body').append(`
				<div title='Gửi đơn hàng' id='guidonhang'>
					<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>
					<div class="dlg_content">Thực hiện chức năng này?</div>
				</div>`);
            $('#guidonhang').dialog({
                modal: true
                , width: 500
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: [
                    {
                        id: "btn-send",
                        text: "Gửi đi",
                        click: function () {
                            $('#btn-ok').button('option', 'disabled', true);
                            $('#wait').show();
                            $.post('Controllers/DanhSachDatHangController.ashx?action=guidonhang&piId=' + piId,
                                { id: piId.toString() }, function (result) {
                                    $('.dlg_content').html(result);
                                    $('#btn-ok').button('option', 'disabled', false);
                                    $('#wait').hide();
                                    $('#grid_ds_dathang')[0].triggerToolbar();
                                });
                        }
                    }
                    , {
                        id: "btn-close",
                        text: "Thoát",
                        click: function () {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
        }
    }

    /*ColNames="['c_danhsachdathang_id','TT', 'Trạng thái', 'Số chứng từ', 'Số Đơn Hàng', 'Customer Order No' 
                , 'Ngày xong hàng', 'NCU', 'Nơi Giao Hàng'
                , 'Discount(%)', 'Ngày Lập', 'Người Phụ Trách', 'Người Đặt Hàng',   'Đặt điểm hàng hóa', 'Các hướng dẩn khác'
                , 'Đã Gửi Hướng Dẫn Làm Hàng', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"*/
</script>

<div class="ui-layout-center ui-widget-content" id="lay-center-dathang">
    <uc1:jqGrid ID="grid_ds_dathang"
        Caption="Danh Sách Đặt Hàng"
        SortName="dsdh.ngaylap"
        FuncActivePI="funcActivePI"
        FuncConfirmPI="funcConfirmPI"
        FuncSendMail="sendMailPI"
        BtnPrint="printPI"
        UrlFileAction="Controllers/DanhSachDatHangController.ashx"
        RowNumbers="true"
        OndblClickRow="dupEditDanhSachDatHang"
        FuncChiaTachPOTaoPI="function(){ add_tab('Tạo đơn đặt hàng', 'inc-chiatachpo-taopi.aspx')}"
        GuiDonHang="guidonhang"
        ReturnPI="ReturnPI"
        MultiSelect="true"
        ColModel="[
            {
                fixed: true, name: 'c_danhsachdathang_id'
                , label: 'c_danhsachdathang_id'
                , index: 'dsdh.c_danhsachdathang_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
                , formoptions: { colpos: 1, rowpos: 1 }
            },
            {
                 fixed: true, name: 'md_trangthai_id'
                 , label: 'TT'
                 , index: 'dsdh.md_trangthai_id'
                 , editable: false
                 , fixed:true , width: 100
                 , edittype: 'text'
                 , align:'center'
                 , formatter:'imagestatus'
                 , search : true
                 , stype : 'select'
				 , formoptions: { colpos: 1, rowpos: 2 }
                 , searchoptions:{sopt:['eq'], value:':[ALL];SOANTHAO:SOẠN THẢO;HIEULUC:HIỆU LỰC;KETTHUC:KẾT THÚC' }
            },
			{
			     fixed: true, name: 'trangthai'
                 , label: 'Trạng thái'
                 , index: 'dsdh.trangthai'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , align:'center'
                 , formatter: 'trangthai'
				 , search : true
                 , stype : 'select'
				 , formoptions: { colpos: 1, rowpos: 2 }
                 , searchoptions:{sopt:['eq'], value:':[ALL];CHUAGUI:Chưa gửi;XACNHAN:Chờ gửi;TUCHOI:Từ chối;DAGUI:Đã gửi;DANHAN:Đã xem;DAHIEULUC:Đã nhận' }
            },
            {
                 fixed: true, name: 'sochungtu'
                 , label: 'Số chứng từ'
                 , index: 'dsdh.sochungtu'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'c_donhang_id'
                 , label: 'Số Đơn Hàng'
                 , index: 'dh.sochungtu'
                 , editable: false
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/DonHangController.ashx?action=getoption' }
                 , formoptions: { colpos: 1, rowpos: 8 }
            },
            {
                 fixed: true, name: 'customer_order_no'
                 , label: 'Customer Order No'
                 , index: 'dh.customer_order_no'
                 , editable: false
                 , width: 120
            },
            {
                 fixed: true, name: 'hangiaohang_po'
                 , label: 'Ngày xong hàng'
                 , align: 'center'
                 , index: 'dsdh.hangiaohang_po'
                 , editable: true
                 , width: 90
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 1, rowpos: 7 }
            },
            {
                 fixed: true, name: 'md_doitackinhdoanh_id'
                 , label: 'NCU'
                 , index: 'dtkd.ma_dtkd'
                 , editable: false
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=1' }
                 , formoptions: { colpos: 1, rowpos: 5 }
            },
			
			{
                 fixed: true, name: 'diachigiaohang'
                 , label: 'Nơi giao hàng'
                 , index: 'dsdh.diachigiaohang'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
				 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=1' }
                 , formoptions: { colpos: 1, rowpos: 16 }
            },
			
            {
                fixed: true, name: 'discount'
                , label: 'Discount(%)'
                , index: 'dsdh.discount'
                , width: 100
                , editable:true
                , edittype: 'text'
                , align: 'center'
                , editoptions:{ defaultValue: '0' }
                , editrules: { required: true, number:true, minValue: 0 }
                , formoptions: { colpos: 1, rowpos: 8 }
            },
			{
                 fixed: true, name: 'ngaylap'
                 , label: 'Ngày Lập'
                 , align: 'center'
                 , index: 'dsdh.ngaylap'
                 , editable: true
                 , width: 90
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 1, rowpos: 6 }
            },
			{
                fixed: true, name: 'nguoi_phutrach'
                , label: 'Người phụ trách'
                , index: 'dsdh.nguoi_phutrach'
                , width: 100
                , editable:false
                , edittype: 'text'
                , formoptions: { colpos: 1, rowpos: 9 }
            },
            {
                 fixed: true, name: 'nguoi_dathang'
                 , label: 'Người đặt hàng'
                 , index: 'dsdh.nguoi_dathang'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 4 }
            },
            {
                fixed: true, name: 'huongdanlamhang'
                , label: 'Đặc điểm hàng hóa'
                , index: 'dsdh.huongdanlamhang'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , formoptions: { colpos: 1, rowpos: 3 }
                , hidden: true
                , editrules:{ edithidden: true }
            },
			{
                fixed: true, name: 'huongdanlamhangchung'
                , label: 'Các hướng dẩn khác'
                , index: 'dsdh.huongdanlamhangchung'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , formoptions: { colpos: 1, rowpos: 4 }
                , hidden: true
                , editrules:{ edithidden: true }
            },
            {
                fixed: true, name: 'isgui_hdlh'
                , label: 'Đã gửi HDLH'
                , index: 'dsdh.isgui_hdlh'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
                , formoptions: { colpos: 1, rowpos: 10 }
				, hidden: true
				, editrules: { edithidden:true }
            },
            {
                fixed: true, name: 'ngaytao'
                , label: 'Ngày tạo'
                , index: 'dsdh.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
                , formoptions: { colpos: 1, rowpos: 11 }
            },
            {
                fixed: true, name: 'nguoitao'
                , label: 'Người tạo'
                , index: 'dsdh.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
                , formoptions: { colpos: 1, rowpos: 12 }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , label: 'Ngày cập nhật'
                , index: 'dsdh.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
                , formoptions: { colpos: 1, rowpos: 13 }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , label: 'Người cập nhật'
                , index: 'dsdh.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
                , formoptions: { colpos: 1, rowpos: 14 }
            },
            {
                fixed: true, name: 'mota'
                , label: 'Ghi chú'
                , index: 'dsdh.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , formoptions: { colpos: 1, rowpos: 15 }
				, hidden: true
				, editrules:{ edithidden: true }
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , label: 'Hoạt động'
                , index: 'dsdh.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
                , formoptions: { colpos: 1, rowpos: 17 }
            }
            ]"
        GridComplete="var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
        FilterToolbar="true"
        Height="420"
        ShowEdit="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
        ShowDel="<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
        EditFormOptions="
                width:350 ,
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
            "
        ViewFormOptions="
                width:350 ,
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , afterSubmit: function(rs, postdata) {
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
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
            "
        OnSelectRow="function(ids) {
				var chk_box = $('#jqg_grid_ds_dathang_' + ids);
				setTimeout(function() {
					if(!chk_box.prop('checked')) { ids = null; }
					if(ids == null) 
                    {
						ids = 0;
						$('#grid_ds_dathang').jqGrid('setCaption','Danh Sách Đặt Hàng');
					} 
                    else 
                    {					
						$.ajax({
							url: 'Controllers/DanhSachDatHangController.ashx?action=getpiinformation&poId='+ids,
							beforeSend:function() {
								$('#grid_ds_dathang').jqGrid('setCaption','đang tải...');
							},
							success: function(data) {
								 $('#grid_ds_dathang').jqGrid('setCaption', data);
							}
						});			
					}
                    $('#grid_ds_chitietdathang').jqGrid('getGridParam', 'postData').poId = ids;
                    $('#grid_phidathang').jqGrid('getGridParam', 'postData').poId = ids;
                    $('#grid_ds_chitietdathang')[0].triggerToolbar();
                    $('#grid_phidathang')[0].triggerToolbar();
				},10);
			}"
        runat="server" />
</div>
<div class="ui-layout-south ui-widget-content" id="lay-south-dathang" style="padding: 0;">
    <div class="ui-layout-center" id="tabs-dathang-details">
        <ul>
            <li><a href="#dathang-details">Các dòng đặt hàng</a></li>
            <li><a href="#phidathang-details">Các khoản phí</a></li>
        </ul>
        <div id="dathang-details">
            <uc1:jqGrid ID="grid_ds_chitietdathang"
                SortName="sp.ma_sanpham"
                SortOrder="ASC"
                UrlFileAction="Controllers/DongDanhSachDatHangController.ashx"
                RowNumbers="true"
                OndblClickRow="function(rowid) { $(this).jqGrid(
                                    'editGridRow', rowid, { 
                                            errorTextFormat:function(data) { 
                                                return 'Lỗi: ' + data.responseText 
                                            }
                                            , afterSubmit: function(rs, postdata){
                                                return showMsgDialog(rs); 
                                            }
                                            , beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }
                                    }) 
                            }"
                ColModel="[
            {
                fixed: true, name: 'c_dongdsdh_id'
                , label: 'c_dongdsdh_id'
                , index: 'ddsdh.c_dongdsdh_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'c_danhsachdathang_id'
                , label: 'Danh sách đặt hàng'
                , index: 'dsdh.c_danhsachdathang_id'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
            },
            {
                fixed: true, name: 'trangthai'
                , label: 'TT'
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
                fixed: true, name: 'sothutu'
                , label: 'STT'
                , index: 'ddsdh.sothutu'
                , width: 50
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sanpham_id'
                , label: 'Mã SP'
                , index: 'sp.md_sanpham_id'
                , width: 100
                , hidden: true 
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ma_sanpham'
                , label: 'Mã SP'
                , index: 'sp.ma_sanpham'
                , width: 100
                , edittype: 'text'
                , editable:false
                , editoptions: { 
                    dataInit : function (elem) { 
                        $(elem).combogrid({
                            searchIcon: true,
                            width: '480px',
                            url: 'Controllers/ProductController.ashx?action=getcombogrid',
                            colModel: [
                                { 'columnName': 'md_sanpham_id', 'width': '0', 'label': 'ID', hidden:true }
                              , { 'columnName': 'ma_sanpham', 'width': '50', 'label': 'Mã SP', 'align':'left'}
                              , { 'columnName': 'mota_tiengviet', 'width': '50', 'label': 'Mô Tả TV' , 'align':'left'}],
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
                fixed: true, name: 'mota_tiengviet'
                , label: 'Mô Tả TV'
                , index: 'ddsdh.mota_tiengviet'
                , width: 200
                , edittype: 'text'
                , editable:false
				, hidden: true
            },
            {
                fixed: true, name: 'md_doitackinhdoanh_id'
                , label: 'NCU'
                , index: 'dtkd.md_doitackinhdoanh_id'
                , width: 250
                , edittype: 'select'
                , editable:false
                , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=1' }
            },
            {
                fixed: true, name: 'huongdan_dathang'
                , label: 'Hướng dẫn đặt hàng'
                , index: 'ddsdh.huongdan_dathang'
                , width: 100
                , edittype: 'textarea'
                , editable:true
                , hidden:true
                , editrules: { edithidden: true }
            },
            {
                fixed: true, name: 'han_giaohang'
                , label: 'Hạn giao hàng'
                , index: 'ddsdh.han_giaohang'
                , width: 100
                , edittype: 'text'
                , editable: true
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				, hidden: true
				, editrules:{ edithidden: true }
            },
            {
                fixed: true, name: 'tem_dan'
                , label: 'Tem dán'
                , index: 'ddsdh.tem_dan'
                , width: 100
                , edittype: 'text'
                , editable:true
				, hidden:true
                , editrules: { edithidden: true }
            },
            {
                fixed: true, name: 'sl_dathang'
                , label: 'SL đặt hàng'
                , index: 'ddsdh.sl_dathang'
                , width: 100
                , edittype: 'text'
                , editable:false
                , editrules: { required:true, number: true, minValue:0 }
                , align:'right'
            },
            {
                fixed: true, name: 'giachuan'
                , label: 'Giá mua'
                , index: 'ddsdh.giachuan'
                , width: 100
                , edittype: 'text'
                , editable: xemgiaDSDH == 'True'
				, hidden: xemgiaDSDH == 'False'
                , editrules: { required:true, number: true, minValue:1 }
                , editoptions: {disabled: true}
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'phi'
                , label: 'Phí'
                , index: 'ddsdh.gianhap - ddsdh.giachuan'
                , width: 100
                , edittype: 'text'
                , editable: xemgiaDSDH == 'True'
				, hidden: xemgiaDSDH == 'False'
                , editrules: { required:true, number: true, minValue:1 }
                , editoptions: {disabled: true}
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
			{
                fixed: true, name: 'gianhap'
                , label: 'Giá nhập'
                , index: 'ddsdh.gianhap'
                , width: 100
                , edittype: 'text'
                , editable: xemgiaDSDH == 'True'
				, hidden: xemgiaDSDH == 'False'
                , editrules: { required:true, number: true, minValue:1 }
                , editoptions: {disabled: true}
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'sl_dagiao'
                , label: 'SL đã giao'
                , index: 'ddsdh.sl_dagiao'
                , width: 100
                , edittype: 'text'
                , editable:false
                , editrules: { required:true, number: true, minValue:0 }
                , align:'right'
                , formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
            },
            {
                fixed: true, name: 'sl_conlai'
                , label: 'SL còn lại'
                , index: 'ddsdh.sl_conlai'
                , width: 100
                , edittype: 'text'
                , editable:false
                , editrules: { required:true, number: true, minValue:0 }
                , align:'center'
                , formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
            },
            {
                fixed: true, name: 'ngaytao'
                , label: 'Ngày tạo'
                , index: 'ddsdh.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , label: 'Người tạo'
                , index: 'ddsdh.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , label: 'Ngày cập nhật'
                , index: 'ddsdh.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , label: 'Người cập nhật'
                , index: 'ddsdh.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'mota'
                , label: 'Mô tả'
                , index: 'ddsdh.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
				, hidden: true
				, editrules:{ edithidden: true }
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , label: 'Hoạt động'
                , index: 'ddsdh.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
                GridComplete="let o = $(this).parent().parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 97);$(this).setGridWidth($(o).width())"
                FilterToolbar="true"
                Height="150"
                MultiSelect="false"
                ShowEdit="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
                EditFormOptions="
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                "
                ViewFormOptions=" width:500
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }"
                runat="server" />
        </div>
        <div id="phidathang-details">
            <uc1:jqGrid ID="grid_phidathang"
                SortName="ngaytao"
                SortOrder="ASC"
                UrlFileAction="Controllers/PhiDathangController.ashx"
                RowNumbers="true"
                OndblClickRow="function(rowid) { $(this).jqGrid('editGridRow', rowid,
                                                    {
                                                        afterSubmit: function(rs, postdata){
                                                            return showMsgDialog(rs);
                                                        }
                                                        , beforeShowForm: function (formid) {
                                                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                                                        }
                                                        , errorTextFormat:function(data) { 
                                                            return 'Lỗi: ' + data.responseText 
                                                        }
                                                        
                                                    }
                                                ); }"
                ColModel="[
            {
                name: 'c_phidathang_id'
                , label: 'c_phidathang_id'
                , index: 'c_phidathang_id'
                , fixed:true , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                name: 'c_danhsachdathang_id'
                , label: 'Đơn đặt hàng'
                , index: 'dh.c_danhsachdathang_id'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
            },
            {
                 name: 'sotien'
                 , label: 'Số tiền'
                 , index: 'sotien'
                 , editable: true
                 , fixed:true , width: 100
                 , edittype: 'text'
                 , editoptions:{ defaultValue: '0' }
                 , editrules: { required:true, number:true, minValue: 0 }
                 , align: 'right'
            },
            {
                 name: 'isphicong'
                 , label: 'Là phí tăng' 
                 , index: 'isphicong'
                 , editable: true
                 , fixed:true , width: 100
                 , edittype: 'checkbox'
                 , align: 'center'
                 , editoptions:{ value:'True:False', defaultValue: 'True' }
                 , formatter: 'checkbox'
            },
            {
                name: 'ngaytao'
                , label: 'Ngày tạo'
                , index: 'ngaytao'
                , fixed:true , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: false 
                , editrules:{ edithidden : true }
            },
            {
                name: 'nguoitao'
                , label: 'Người tạo'
                , index: 'nguoitao'
                , fixed:true , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: false 
                , editrules:{ edithidden : true }
            },
            {
                name: 'ngaycapnhat'
                , label: 'Ngày cập nhật'
                , index: 'ngaycapnhat'
                , fixed:true , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: false 
                , editrules:{ edithidden : true }
            },
            {
                name: 'nguoicapnhat'
                , label: 'Người cập nhật'
                , index: 'nguoicapnhat'
                , fixed:true , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: false 
                , editrules:{ edithidden : true }
            },
            {
                name: 'mota'
                , label: 'Mô tả'
                , index: 'mota'
                , fixed:true , width: 200
                , editable:true
                , edittype: 'textarea'
            },
            {
                name: 'hoatdong', hidden: false
                , label: 'Hoạt động'
                , index: 'hoatdong'
                , fixed:true, width: 70
                , editable: false
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
                GridComplete="let o = $(this).parent().parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 97);$(this).setGridWidth($(o).width())"
                FilterToolbar="true"
                Height="150"
                MultiSelect="false"
                ShowAdd="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
                ShowEdit="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
                ShowDel="<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
                AddFormOptions=" 
                
                beforeShowForm: function (formid) {
                        var masterId = $('#grid_ds_dathang').jqGrid('getGridParam', 'selrow');
                        var forId = 'c_danhsachdathang_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một đơn hàng mới có thể tạo chi tiết.!');
                        }else{
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_dathang').jqGrid('getGridParam', 'selrow');
                    var forId = 'c_danhsachdathang_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một đơn hàng mới có thể tạo chi tiết.!!'];
                    }else{
                        postdata.c_danhsachdathang_id = masterId;
                        return [true,'']; 
                    }
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
                }"
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
                afterSubmit: function(rs, postdata) { 
                    return showMsgDialog(rs); 
            }"
                runat="server" />
        </div>
    </div>
</div>

<script>
    var hide_sendmail = function () {
        $('.t_plus', '#sendpi').hide();
        $('#new', '#sendpi').on("click", function () {
            $('.t_plus', '#sendpi').hide();
            $('.t_new', '#sendpi').show();
            $('.new_toAll', '#sendpi').prop('checked', false);
        });

        $('#plus', '#sendpi').on("click", function () {
            $('.t_plus', '#sendpi').show();
            $('.t_new', '#sendpi').hide();
            $('.new_toAllPlus', '#sendpi').prop('checked', false);
        });
    }

    $("#grid_ds_dathang").jqGrid('navGrid', "#grid_ds_dathang-pager").jqGrid('navButtonAdd', "#grid_ds_dathang-pager",
        {
            caption: "",
            buttonicon: "ui-icon-refresh",
            onClickButton: function () {
                LamMoiTrangThaiDDH();
            },
            title: "Làm mới trạng thái ĐĐH",
            cursor: "pointer"
        });
    //Doi nha cung cap
    if (doiNCU == 'True' || vnnIsAdmin == 'True') {
        $("#grid_ds_dathang").jqGrid('navGrid', "#grid_ds_dathang-pager").jqGrid('navButtonAdd', "#grid_ds_dathang-pager",
        {
            caption: "",
            buttonicon: "ui-icon-refresh",
            onClickButton: function () {
                var qoId = $('#grid_ds_dathang').jqGrid('getGridParam', 'selrow');
                if (qoId == null) {
                    alert('Hãy chọn một DSĐH');
                } else {
                    let name = 'dialog-kiemtratonkho-po';
                    $('body').append(`
                        <div title="Thay đổi nhà cung ứng" id="${name}">
                            <table>
                                <tr style="height: 25px">
                                    <td>
                                        <label for="rdoDoiNCCGiuChoPO" style="user-select:none; cursor:pointer;">Đổi Nhà cung ứng</label>
                                        <select id="selNCCGiuChoPO">
                                            <option value="VINHGIA">VINHGIA</option>
                                            <option value="HOANGHUYHUNG">HOANGHUYHUNG</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>`
                    );

                    //let $rdoGCPO = $('input[name="rdoGiuChoPO"]');
                    let $selNCCGCPO = $('#selNCCGiuChoPO');
                    $('#' + name).dialog({
                        modal: true,
                        open: function (event, ui) {
                            //hide close button.
                            //$rdoGCPO.change(function () {
                            //    let idsel = $rdoGCPO.filter(":checked").attr('id');
                            //    $selNCCGCPO.prop('disabled', idsel != 'rdoDoiNCCGiuChoPO');
                            //});
                            //$(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                        },
                        buttons: [
                            {
                                id: "btn-kiemtratonpo-ok",
                                text: "Đồng ý",
                                click: function () {
                                    //let huyGiuCho = $rdoGCPO.filter(":checked").attr('id') == 'rdoHuyGiuChoPO' ? 1 : 0;
                                    //let doiNCC = $rdoGCPO.filter(":checked").attr('id') == 'rdoDoiNCCGiuChoPO' ? 1 : 0;
                                    let nccDaChon = $selNCCGCPO.val();
                                    //$("#btn-kiemtratonpo-ok").button("disable");
                                    //$("#btn-kiemtratonpo-close").button("disable");
                                    $('#' + name).prepend('<div id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>');
                                    $.post('Controllers/DongDanhSachDatHangController.ashx?action=ESC_doincc',
                                        { id: qoId, nccDaChon: nccDaChon }, function (res) {
                                            res = JSON.parse(res);
                                            if (res.ok)
                                                res.msg = `<div style="color:blue">${res.msg}</div>`;
                                            else
                                                res.msg = `<div style="color:red">${res.msg}</div>`;
                                            $('#' + name).html(res.msg);
                                            $("#btn-kiemtratonpo-close").button("enable");
                                            $('#grid_ds_dathang')[0].triggerToolbar();
                                            $('#grid_ds_chitietdathang')[0].triggerToolbar();
                                        }).fail(function () {
                                            $('#' + name).html(`Hãy thử lại.`);
                                            $("#btn-kiemtratonpo-close").button("enable");
                                        });
                                }
                            },
                            {
                                id: "btn-kiemtratonpo-close",
                                text: "Thoát",
                                click: function () {
                                    $(this).dialog("destroy").remove();
                                }
                            }
                        ]
                    });
                }
            },
            title: "Đổi nhà cung ứng",
            cursor: "pointer"
        });
    }
    
    //Doi nha cung cap
    $('#grid_phidathang-pager_left').removeAttr('style');
    createRightPanel('grid_ds_dathang');
    createRightPanel('grid_ds_chitietdathang');
    createRightPanel('grid_phidathang');
</script>
