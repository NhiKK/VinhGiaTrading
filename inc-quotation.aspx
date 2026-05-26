<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-quotation.aspx.cs" Inherits="inc_quotation" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<style type="text/css">
    .ui-icon.icon-ghepbo-qo {
        background: url(iconcollection/ghepbo-qo.png) no-repeat !important;
    }

    #FrmGrid_grid_ds_quotation #md_doitackinhdoanh_id {
        width: 98%;
    }

    .sp_bocai_qo td {
        font-weight: bold !important;
        color: blue;
    }
</style>
<script>
    function sp_bocai_qo(cellvalue, options, rowObject) {
        setTimeout(function () {
            let c_baogia_id = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
            let ghepbo = $('#grid_ds_quotation').jqGrid('getCell', c_baogia_id, 'ghepbo');
            if (ghepbo) {
                ghepbo = ghepbo.toLowerCase();
                if (ghepbo == 'true') {
                    $('#' + options.rowId).removeClass("sp_bocai_qo");
                    if (cellvalue != '' & cellvalue != null) {
                        var bientau = cellvalue.substring(9, 11);
                        if (bientau[0] != '0' & bientau[0] != '1' & bientau[0] != '2')
                            $('#' + options.rowId).addClass("sp_bocai_qo");
                    }
                }
            }
        }, 100);
        if (cellvalue == null)
            cellvalue = '';
        return cellvalue;
    }

    function updateGiaSanPham_CallBack(c_baogia_id, md_sanpham_id) {
        var frm = "#FrmGrid_grid_ds_chitietquotation";
        var giafob = $('#giafob', frm);
        giafob.val('Đang tải...');
        $.ajax({
            url: "Controllers/QuotationDetailController.ashx?action=getgiasanpham&c_baogia_id=" + c_baogia_id + "&md_sanpham_id=" + md_sanpham_id,
            type: "GET",
            success: function (data) {
                let json = JSON.parse(data);
                try {
                    giafob.val(json.gia);
                    if (json.ma_donggoi) {
                        $(`#md_donggoi_id option[mdg="${json.ma_donggoi}"]`).prop('selected', true);
                    }
                }
                catch (r) {
                    giafob.val(-1);
                    console.error(r);
                }
            }
        });
    }

    $(document).ready(function () {

        $('#lay-center-quotation').parent().layout({
            south: {
                size: "50%"
            }
            , center: {
                onresize: function () {
                    var h = $("#lay-center-quotation").height();
                    $("#grid_ds_quotation").setGridHeight(h - 90);
                }
            }
        });

        // Layout nho duoi
        $('#layout-south-quotation').layout({
            center: {
                onresize: function () {
                    let o = $("#tabs-quotation-details");
                    $("#grid_ds_chitietquotation").setGridWidth(o.width());
                    $("#grid_ds_color_references").setGridWidth(o.width());

                    $("#grid_ds_chitietquotation").setGridHeight(o.height() - 97);
                    $("#grid_ds_color_references").setGridHeight(o.height() - 97);
                }
            }
        });

        $('#tabs-quotation-details').tabs();
    });

    function TaoColorReferences() {
        var id = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');

        if (id != null) {
            var sochungtu = $('#grid_ds_quotation').jqGrid('getCell', id, 'sobaogia');
            $('body').append('<div title="Tạo Color References Cho ' + sochungtu + '" id="frmCreateColor"><div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div><div class="caption">Lưu ý: Để tạo Color References thì Quotation phải có các dòng sản phẩm.</div></div>');
            $('#frmCreateColor').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                        [
                            {
                                id: "btnCreateColor",
                                text: "Có",
                                click: function () {
                                    $("#frmCreateColor .caption").ajaxStart(function () {
                                        $("#frmCreateColor #wait").css("display", "block");
                                        $("#btnCreateColor").button("disable");
                                        $("#btnClose").button("disable");
                                    });

                                    $("#frmCreateColor .caption").ajaxComplete(function () {
                                        $("#frmCreateColor #wait").css("display", "none");
                                        $("#btnClose span").text("Thoát");
                                        $("#btnClose").button("enable");
                                    });

                                    $.get("Controllers/QuotationController.ashx?action=createcolor&qoId=" + id, function (result) {
                                        $('#frmCreateColor .caption').html(result);
                                        $('#grid_ds_color_references').jqGrid().trigger('reloadGrid');
                                    });
                                }
                            },
                            {
                                id: "btnClose",
                                text: "Không",
                                click: function () {
                                    $(this).dialog("destroy").remove();
                                }
                            }
                        ]
            });
        } else {
            alert('Hãy chọn một báo giá');
        }
    }

    function funcPO() {
        var msg = "";
        var id = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
        if (id == null) {
            msg = "Bạn hãy chọn 1 quotation mà bạn muốn tạo P/O.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-create-po\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-create-po').dialog({
                modal: true,
                open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                },
                buttons: {
                    'Thoát': function () {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn tạo P/O từ quotation này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            var name = 'dialog-create-po';
            var title = 'Thông Báo';
            var content = h3 + '<table style="width:100%">' +
								'<tr style="height: 25px;">' +
									'<td>' +
									'</td>' +

									'<td>' +
										'<input class="printRequired" id="printRequired" type="radio" name="rdoPrintType" value="printRequired" checked="checked" />' +
										'<label class="printRequired" for="printRequired">Tạo P/O Từ Quotation</label>' +
										'<table class="t_po" style="width:100%; padding-left: 50px; border: 1px solid;">' +
											'<tr style="height: 25px;">' +
												'<td colspan="2">' +
													'<input id="po" value="po" name="po_chs" type="radio" checked="checked" >' +
													'<label for="po">Thành P/O</label>' +
												'</td>' +
											'</tr>' +
											'<tr style="height: 25px;">' +
												'<td colspan="2">' +
													'<input id="por" value="por" name="po_chs" type="radio" >' +
													'<label for="por">Thành P/O Mẫu</label>' +
												'</td>' +
											'</tr>' +
										'</table>' +
									'</td>' +
								'</tr>' +
								'<tr style="height: 25px;">' +
									'<td>' +
									'</td>' +

									'<td>' +
										'<input class="print1CB" id="print1CB" type="radio" name="rdoPrintType" value="print1CB" />' +
										'<label class="print1CB" for="print1CB">Tạo P/O Từ Quotation Mẫu</label>' +
										'<table class="t_po1" style="width:100%; padding-left: 50px; border: 1px solid;">' +
											'<tr style="height: 25px;">' +
												'<td colspan="2">' +
													'<input id="po1" value="po" name="po_chs1" type="radio" checked="checked" >' +
													'<label for="po1">Thành P/O</label>' +
												'</td>' +
											'</tr>' +
											'<tr style="height: 25px;">' +
												'<td colspan="2">' +
													'<input id="por1" value="por" name="po_chs1" type="radio" >' +
													'<label for="por1">Thành P/O Mẫu</label>' +
												'</td>' +
											'</tr>' +
										'</table>' +
									'</td>' +
								'</tr>' +
								'<tr style="height: 25px;">' +
									'<td>' +
									'</td>' +

									'<td>' +
										'<input class="print2CB" id="print2CB" type="radio" name="rdoPrintType" value="print2CB" />' +
										'<label class="print2CB" for="print2CB">Tạo P/O Từ Quotation Mẫu Có 2 Cảng Biển</label>' +
										'<table class="t_po2" style="width:100%; padding-left: 50px; border: 1px solid;">' +
											'<tr style="height: 25px;">' +
												'<td colspan="2">' +
													'<input id="po2" value="po" name="po_chs2" type="radio" checked="checked" >' +
													'<label for="po2">Thành P/O</label>' +
												'</td>' +
											'</tr>' +
											'<tr style="height: 25px;">' +
												'<td colspan="2">' +
													'<input id="por2" value="por" name="po_chs2" type="radio" >' +
													'<label for="por2">Thành P/O Mẫu</label>' +
												'</td>' +
											'</tr>' +
										'</table>' +
									'</td>' +
								'</tr>' +
						  '</table>';

            //1. Q/O BT -> P/O BT , P/O SR
            //2. Q/O SR -> P/O BT , P/O SR
            //3. Q.O SR 2CB -> P/O SR hoac 2 P/O 2 CB
            $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');

            $('#' + name).dialog({
                modal: true,
                open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                },
                buttons: [
				{
				    id: "btn-create-po",
				    text: "Có",
				    click: function () {

				        //1. Q/O BT -> P/O BT , P/O SR
				        //2. Q/O SR -> P/O BT , P/O SR
				        //3. Q.O SR 2CB -> P/O SR hoac 2 P/O 2 CB

				        var printType = $('input[name=rdoPrintType]:checked', '#' + name).val();
				        var optionType = "";

				        var option = 0, check = 0;
				        if (typeof printType != 'undefined') {
				            if (printType == "printRequired") {
				                option = 0;
				                optionType = $('input[name=po_chs]:checked', '#' + name).val();
				            }
				            else if (printType == "print1CB") {
				                option = 1;
				                optionType = $('input[name=po_chs1]:checked', '#' + name).val();
				            }
				            else if (printType == "print2CB") {
				                option = 2;
				                optionType = $('input[name=po_chs2]:checked', '#' + name).val();
				            }

				            $("#dialog-caution").ajaxStart(function () {
				                $("#wait").css("display", "block");
				                $("#btn-close").button("disable");
				            });

				            $("#dialog-caution").ajaxComplete(function () {
				                $("#wait").css("display", "none");
				                $("#btn-close span").text("Thoát");
				                $("#btn-close").button("enable");
				            });

				            $.get("Controllers/QuotationController.ashx?action=createpo&qoId=" + id + "&option=" + option + "&optionType=" + optionType, function (result) {
				                //$('#dialog-caution').css({ 'color': 'green' });
				                $('#dialog-create-po').find('#dialog-caution').html(result);

				                if (result.indexOf("Lỗi:") != -1)
				                    $("#btn-create-po").button("enable");
				                else
				                    $("#btn-create-po").button("disable");
				            });

				        } else {
				            alert('Hãy chọn cách tạo P/O.!');
				        }
				    }
				},
				{
				    id: "btn-print-close",
				    text: "Không",
				    click: function () {
				        $(this).dialog("destroy").remove();
				    }
				}
                ]
            });
        }
        hide_createpo();
    }

    var hide_createpo = function () {
        var myGrid = $('#grid_ds_quotation'), selRowId = myGrid.jqGrid('getGridParam', 'selrow'), celValue = myGrid.jqGrid('getCell', selRowId, 'isform');

        $('#printRequired', '#dialog-create-po').on("click", function () {
            $('.t_po', '#dialog-create-po').show();
            $('.t_po1', '#dialog-create-po').hide();
            $('.t_po2', '#dialog-create-po').hide();
        });

        $('#print1CB', '#dialog-create-po').on("click", function () {
            $('.t_po', '#dialog-create-po').hide();
            $('.t_po1', '#dialog-create-po').show();
            $('.t_po2', '#dialog-create-po').hide();
        });

        $('#print2CB', '#dialog-create-po').on("click", function () {
            $('.t_po', '#dialog-create-po').hide();
            $('.t_po1', '#dialog-create-po').hide();
            $('.t_po2', '#dialog-create-po').show();
        });

        if (celValue == "True") {
            $('#print1CB', '#dialog-create-po').click();
            $('.printRequired', '#dialog-create-po').hide();
        }
        else {
            $('#printRequired', '#dialog-create-po').click();
            $('.print1CB', '#dialog-create-po').hide();
            $('.print2CB', '#dialog-create-po').hide();
        }
    }

    function funcActiveQO() {
        var msg = "";
        var id = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Bạn hãy chọn 1 quotation mà bạn muốn hiệu lực.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-quotation\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-active-quotation').dialog({
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
            msg = "Có phải bạn muốn hiệu lực quotation này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-quotation\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-quotation').dialog({
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

						        $.get("Controllers/QuotationController.ashx?action=activeqo&qoId=" + id, function (result) {
						            //$('#dialog-caution').css({ 'color': 'green' });
						            $('#dialog-active-quotation').find('#dialog-caution').html(result);
						            $('#grid_ds_quotation').jqGrid().trigger('reloadGrid');
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

    function funcPrint() {
        var qoId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-qo';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
									'<tr>' +
										'<td>' +
											'<input id="printRequired" type="radio" name="rdoPrintType" value="printRequired" checked="checked" />' +
											'<label for="printRequired">In báo giá hiện tại</label>' +
										'</td>' +

										'<td>' +
										'</td>' +

									'</tr>' +
							  '</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
            modal: true,
            open: function (event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            },
            buttons: [
				{
				    id: "btn-print-ok",
				    text: "Xem",
				    click: function () {
				        var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-qo').val();
				        var printURL = "PrintControllers/";
				        let windowSize = "top=0, left=0, width=" + window.outerWidth + ", height=" + window.outerHeight;
				        if (typeof printType != 'undefined') {
				            if (qoId != null) {
				                printURL += "InBaoGia/";
				                window.open(printURL + "?c_baogia_id=" + qoId + "&type=pdf", "Xem Báo Giá", windowSize);
				            } else {
				                alert('Bạn chưa chọn báo giá cần xem báo giá');
				            }
				        } else {
				            alert('Hãy chọn cách in.!');
				        }
				    }
				},
				{
				    id: "btn-print-ok",
				    text: "In",
				    click: function () {
				        var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-qo').val();
				        var printURL = "PrintControllers/";
				        var windowSize = "width=700px,height=700px,scrollbars=yes";
				        if (typeof printType != 'undefined') {
				            if (printType == "printRequired") {
				                if (qoId != null) {
				                    printURL += "InBaoGia/";
				                    window.open(printURL + "?c_baogia_id=" + qoId, "In Báo Giá", windowSize);
				                } else {
				                    alert('Bạn chưa chọn báo giá cần in báo giá');
				                }
				            } else {
				                printURL += "InDSBaoGia/";
				                window.open(printURL, "In Danh Sách Báo Giá", windowSize);
				            }
				        } else {
				            alert('Hãy chọn cách in.!');
				        }
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

    function sendMailQuotation() {
        var qoId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
        if (qoId == null) {
            alert('Hãy chọn một báo giá mà bạn muốn gửi email!');
        } else {

            $('body').append("<div title='Gửi Email' id='sendquotation'>" + "<div id='content'>Đang kiểm tra...</div>" + "</div>");
            $('#sendquotation').dialog({
                modal: true
				 , open: function (event, ui) {
				     //hide close button.
				     $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
				 }
				 , buttons: [
					{
					    id: "btn-send",
					    text: "Gửi đi",
					    click: function () {
					        $('#sendquotation #content').html("Đang gửi email...");
					        $("#btn-send").button("disable");
					        $("#btn-close").button("disable");

					        $.get("Controllers/QuotationController.ashx?action=sendmail&qoId=" + qoId, function (result) {
					            $('#sendquotation #content').html(result);
					            $("#btn-close span").text("Thoát");
					            $("#btn-close").button("enable");
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

            $.get("Controllers/QuotationController.ashx?action=startsendmail&qoId=" + qoId, function (result) {
                $('#sendquotation').find('#content').html('<h2>Xác nhận trước khi gửi email!</h2><div>' + result + '</div>');
            });
        }
    }

    function updatePackingQO_CallBack(md_sanpham_id) {
        let frm = '#FrmGrid_grid_ds_chitietquotation';
        let c_chitietbaogia_id = $("#c_chitietbaogia_id", frm).val();
        let c_baogia_id = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
        $("#md_donggoi_id", frm).html("<option value=''>Đang tải đóng gói...</option>").attr("disabled", "disabled");
        $("#giafob", frm).html('Đang tải giá...');
        $.ajax({
            url: "Controllers/PackingController.ashx?action=getoptionselected&c_chitietbaogia_id=" + c_chitietbaogia_id + "&md_sanpham_id=" + md_sanpham_id +
            "&c_baogia_id=" + c_baogia_id,
            type: "GET",
            success: function (packingHtml) {
                var arr_pkl = packingHtml.split('(##)');
                if (packingHtml != 'null') {
                    $("#md_donggoi_id", frm).removeAttr("disabled").html(arr_pkl[0]);
                    $("#docquyen", frm).val(arr_pkl[1]);
                    $("#giafob", frm).val(arr_pkl[2]);
                    $("#docquyen", frm).attr('dmhh', arr_pkl[4]);
                }
                else {
                    $("#md_donggoi_id", frm).html("<option value=''>Không tìm thấy đóng gói!</option>");
                }
            }
        });
    }

    // This function gets called whenever an edit dialog is opened
    function populatePackingQO() {
        // first of all update the city based on the country
        updatePackingQO_CallBack($("#sanpham_id").val());
        var id_sel = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
        celValue = $('#grid_ds_quotation').jqGrid('getCell', id_sel, 'gia_db');
        var giafob = $('#giafob', '#FrmGrid_grid_ds_chitietquotation');
        var docquyen = $('#tr_docquyen', '#FrmGrid_grid_ds_chitietquotation');
        if (celValue == 'True') {
            giafob.prop('disabled', true);
        }
        else {
            giafob.prop('disabled', false);
        }
        docquyen.hide();
    }

    function disable_cangbien(frmID) {
        var cangbien = $('#md_cangbien_id', frmID);
        if (cangbien.attr('id') != null) {
            cangbien.prop('disabled', true);
        }
        else {
            setTimeout(function () { disable_cangbien(frmID); }, 10);
        }
    }

    function ShowPBGCu_grid_ds_quotation(frmID, action) {
        var ShowPBGCu = function () {
            var chkPBGCu = $('#chkPBGCu', frmID);
            var tr_phienbangiacu = $('#tr_phienbangiacu', frmID);
            if (chkPBGCu.prop('checked') == true) {
                tr_phienbangiacu.show();
            }
            else {
                tr_phienbangiacu.hide();
            }
        }

        var chkPBGCu_ = $('#chkPBGCu', frmID);
        chkPBGCu_.off('change');
        chkPBGCu_.change(function () {
            ShowPBGCu();
        });
        ShowPBGCu();
    }

    function check_quycachdonggoi(md_sanpham_id, md_donggoi_id) {
        var frm = "#FrmGrid_grid_ds_chitietquotation";
        $.ajax({
            url: "Controllers/QuotationDetailController.ashx?action=check_quycachdonggoi&md_sanpham_id=" + md_sanpham_id + "&md_donggoi_id=" + md_donggoi_id,
            type: "GET",
            success: function (data) {
                if (data.length > 0) {
                    alert(data);
                }
            }
        });
    }
</script>
<DIV class="ui-layout-center ui-widget-content" id="lay-center-quotation" style="padding:0;">
    <uc1:jqGrid  ID="grid_ds_quotation"
            Caption="Báo Giá" 
            SortName="baogia.ngaytao" 
            SortOrder="desc"
            UrlFileAction="Controllers/QuotationController.ashx" 
            ColNames="['c_baogiaqr_id', 'TT', 'Số Báo Giá', 'Khách Hàng'
                    , 'Ngày Báo Giá', 'Ngày Hết Hạn', 'Cảng Biển', 'Shipmenttime' , 'MOQ/Item Color'
                    , 'Tr.Lượng' , 'CBM', 'Total Quotation'
                    , 'Đồng Tiền', 'Kích Thước', 'QO mẫu', 'Ngày Tạo'
                    , 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật'
                    , 'Ghi chú', 'Hoạt động', 'Giá đặc biệt'
                    , 'Giá FOB phiên bản cũ', 'Phiên bản giá cũ', 'Đã ghép bộ']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { 
								$('#edit_grid_ds_quotation').click();
                            }"
            FuncActiveQO = "funcActiveQO"
            FuncPO = "funcPO"
            BtnPrint="funcPrint"
            FuncSendMail="sendMailQuotation"
            FuncQoByProducts ="function(){ add_tab('Tạo Quotation Từ D/S Sản Phẩm', 'inc-tao-quotation.aspx')}"
            ColModel = "[
            {
                fixed: true, name: 'c_baogiaqr_id'
                , index: 'c_baogiaqr_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_trangthai_id'
                 , index: 'baogia.md_trangthai_id'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , align:'center'
                 , formatter:'imagestatus'
                 , search : true
                 , stype : 'select'
                 , searchoptions:{ value:':[ALL];SOANTHAO:SOẠN THẢO;HIEULUC:HIỆU LỰC'}
            },
            {
                 fixed: true, name: 'sobaogia'
                 , index: 'baogia.sobaogia'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
				 , editrules: { required:true }
            },
            {
                 fixed: true, name: 'md_doitackinhdoanh_id'
                 , index: 'dtkd.md_doitackinhdoanh_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=0' }
                 , search : true
                 , stype : 'select'
                 , searchoptions: { sopt:['eq'], dataUrl: 'Controllers/PartnerController.ashx?action=getsearchoption&isncc=0' }
				 , formoptions:{label:'Khách Hàng ' + ip_fd()}
            },
            
            {
                 fixed: true, name: 'ngaybaogia'
                 , index: 'baogia.ngaybaogia'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required: true }
                 , search: true
                 , stype: 'text'
                 , searchoptions: { 
                    sopt: ['bw', 'ne']
                    , dataInit: function (elem) { 
                        $(elem).datepicker({ 
                            changeYear: true
                            , changeMonth: true
                            , dateFormat:'dd/mm/yy'
                            , onSelect: function(dateText, inst) {
                               if (this.id.substr(0, 3) === 'gs_') {
                                    // call triggerToolbar only in case of searching toolbar
                                    setTimeout(function () {
                                        // Kiểm tra ngày nếu thuộc 'dd/MM/yyyy' mới cho lộc
                                        var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
                                        if(regex.test(dateText)){
                                            $('#grid_ds_quotation')[0].triggerToolbar();
                                        }
                                    }, 100);
                                }
                            }
                        });
                    } 
                 }
            },
            {
                 fixed: true, name: 'ngayhethan'
                 , index: 'baogia.ngayhethan'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required: true }
                 , search: true
                 , stype: 'text'
                 , searchoptions: { 
                    sopt: ['bw', 'ne']
                    , dataInit: function (elem) { 
                        $(elem).datepicker({ 
                            changeYear: true
                            , changeMonth: true
                            , dateFormat:'dd/mm/yy'
                            , onSelect: function(dateText, inst) {
                               if (this.id.substr(0, 3) === 'gs_') {
                                    // call triggerToolbar only in case of searching toolbar
                                    setTimeout(function () {
                                        // Kiểm tra ngày nếu thuộc 'dd/MM/yyyy' mới cho lộc
                                        var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
                                        if(regex.test(dateText)){
                                            $('#grid_ds_quotation')[0].triggerToolbar();
                                        }
                                    }, 100);
                                }
                            }
                        });
                    } 
                 }
				 , formoptions:{label:'Ngày hết hạn ' + ip_fd()}
            },
			{
                 fixed: true, name: 'md_cangbien_id'
                 , index: 'cb.ten_cangbien'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/QuotationController.ashx?action=getSelectOption_cb' }
				 , editrules: { required:true }
				 , formoptions:{label:'Cảng Biển ' + ip_fd()} 
            },
			{
                 fixed: true, name: 'shipmenttime'
                 , index: 'baogia.shipmenttime'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required: true, number:true, minValue:1, edithidden:true }
                 , editoptions:{ defaultValue: '75' }
				 , hidden: true 
            },
            {
                 fixed: true, name: 'moq_item_color'
                 , index: 'bg.moq_item_color'
                 , editable: true
                 , width: 100
                 , edittype: 'textarea'
				 , hidden: true 
				 , editrules: {edithidden:true} 
            },
			
            
            {
                 fixed: true, name: 'md_trongluong_id'
                 , index: 'tl.ten_trongluong'
                 , align: 'right'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/TrongLuongController.ashx?action=getoption' }
                 , search:true
                 , stype:'select'
                 , searchoptions:{ sopt:['eq'], dataUrl: 'Controllers/TrongLuongController.ashx?action=getsearchoption' }
                 , hidden: true 
                 , editrules:{ edithidden : true }
            },
            {
                 fixed: true, name: 'totalcbm'
                 , index: 'baogia.totalcbm'
                 , align: 'right'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                , formatter:'number'
				, hidden: true 
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 3, prefix: ''}
            },
            {
                 fixed: true, name: 'totalquo'
                 , index: 'baogia.totalquo'
                 , align: 'right'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
				 , hidden: true 
                , formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                 fixed: true, name: 'md_dongtien_id'
                 , index: 'dt.ma_iso'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption' }
                 , hidden: true 
                 , editrules:{ edithidden : true }
            },{
                 fixed: true, name: 'md_kichthuoc_id'
                 , index: 'kt.ten_kichthuoc'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/KichThuocController.ashx?action=getoption' }
                 , hidden: true 
                 , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'isform'
                , index: 'baogia.isform'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'baogia.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'baogia.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                /*, hidden: true 
                , editrules:{ edithidden : true }*/
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'baogia.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'baogia.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'mota'
                , index: 'baogia.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: false 
                , editrules:{ edithidden : true }
            },
            
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'baogia.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            },
			{
                fixed: true, name: 'gia_db', hidden: false
                , index: 'baogia.gia_db'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
            {
                fixed: true
                , name: 'chkPBGCu'
                , hidden: true
                , editrules:{
                    edithidden:true
                }
                , index: 'baogia.chkPBGCu'
                , width: 100
                , editable: showPBGCforEveryOne
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
			{
                fixed: true
                , name: 'phienbangiacu'
                , hidden: true
                , editrules:{
                    edithidden:true
                }
                , index: 'baogia.phienbangiacu'
                , width: 100
                , editable:true
                , edittype: 'select'
                , align: 'center'
                , editoptions: {style:'width:98%', dataUrl: 'Controllers/SelectOption/PhienBanGiaCu.ashx?action=PhienBanGiaCu' }
                , formoptions:{label:'Phiên bản giá cũ ' + ip_fd()}
            },
			{
                fixed: true, name: 'ghepbo', hidden: true
                , index: 'baogia.ghepbo'
                , width: 100
                , editable:false
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            }
            ]"
            GridComplete = "let o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            FilterToolbar="true"
            Height = "170"
            MultiSelect = "false" 
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                },
				beforeSubmit: function(rs, postdata){
					let laQOMau = $('#isform', postdata.selector).prop('checked');
					if(laQOMau) {
						if(confirm('Bạn muốn tạo báo giá mẫu?')) {
							return [true];
						}
						else {
							return false;
						}
					}
					else {
						if(confirm('Bạn muốn tạo báo giá bình thường?')) {
							return [true];
						}
						else {
							return false;
						}
					}
                },
				afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter();
                    var frmID = '#' + formid.attr('id');
					$('#gia_db', frmID).prop('disabled', false);
					$('#isform', frmID).prop('disabled', false);
                    $('#chkPBGCu', frmID).prop('disabled', false);
                    ShowPBGCu_grid_ds_quotation(frmID);
                    disableSelOption('#md_cangbien_id', frmID, false);
                    disableSelOption('#phienbangiacu', frmID, false);
                }
                "
            EditFormOptions ="
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter();
                    var frmID = '#' + formid.attr('id');
					$('#gia_db', frmID).prop('disabled', true);
					$('#isform', frmID).prop('disabled', true);
                    $('#chkPBGCu', frmID).prop('disabled', true);
                    ShowPBGCu_grid_ds_quotation(frmID);
                    disableSelOption('#md_cangbien_id', frmID, true);
                    disableSelOption('#phienbangiacu', frmID, true);
                }
                "
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            DelFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
            }"
            OnSelectRow = "
                function(ids) {
		            if(ids == null) {
			            ids=0;
				    }

                    $('#grid_ds_chitietquotation').jqGrid('getGridParam', 'postData').quotationId = ids;
                    $('#grid_ds_color_references').jqGrid('getGridParam', 'postData').quotationId = ids;
					$('#grid_ds_chitietquotation')[0].triggerToolbar();
                    $('#grid_ds_color_references')[0].triggerToolbar();
                }
            "
            runat="server" />
</DIV> 

<DIV class="ui-layout-south ui-widget-content" id="layout-south-quotation">
    <div class="ui-layout-center" id="tabs-quotation-details">
        <ul>
            <li><a href="#qo-details-1">Danh Sách Sản Phẩm</a></li>
            <li><a href="#qo-details-2">Color References</a></li>
        </ul>
        
        
        
        <div id="qo-details-1">
                <uc1:jqGrid  ID="grid_ds_chitietquotation" 
                SortName="sp.ma_sanpham" 
                SortOrder="asc"
                UrlFileAction="Controllers/QuotationDetailController.ashx" 
                ColNames=""
                RowNumbers="true"
				FilterToolbar="true"
                OndblClickRow = "
				function(rowid) { 
					$('#edit_grid_ds_chitietquotation').click();
				}"
                ColModel = "[
                {
                    fixed: true, name: 'c_chitietbaogia_id', label: 'c_chitietbaogia_id'
                    , index: 'ctbg.c_chitietbaogia_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                    fixed: true, name: 'c_baogia_id', label: 'Báo Giá'
                    , index: 'bg.c_baogia_id'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , hidden: true
                },
                {
                    fixed: true, name: 'trangthai', label: 'TT'
                    , index: 'sp.trangthai'
                    , width: 50
                    , editable:false
                    , formatter:'imagestatus'
                    , align: 'center'
                    , search: false
                    , viewable: false
                    , sortable: false
                },
				{
					 fixed: true, name: 'trangthai_ctbg', label: 'Trạng Thái'
					 , index: 'ctbg.trangthai'
					 , editable: false
					 , width: 50
					 , edittype: 'text'
					 , align:'center'
					 , formatter:'imagestatus'
					 , search : true
					 , stype : 'select'
					 , searchoptions:{ value:':[ALL];DGDG:Đổi giá theo đóng gói;BT:Giá bình thường'}
				},
                {
                    fixed: true, name: 'sothutu', label: 'STT'
                    , align: 'right'
                    , index: 'ctbg.sothutu'
                    , width: 50
                    , edittype: 'text'
                    , editable: true
                    , editrules: { required:true, number:true, minValue:0 }
                    , editoptions:{ defaultValue:'0' }
                },
                {
                    fixed: true, name: 'sanpham_id', label: 'Mã SP'
                    , index: 'sp.md_sanpham_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , editrules: { required:true }
                },
                {
                    fixed: true, name: 'ma_sanpham', label: 'Mã SP'
                    , index: 'sp.ma_sanpham'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , editrules: { required:true }
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
										var c_baogia_id = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
                                        updatePackingQO_CallBack(ui.item.md_sanpham_id);
                                        //updateGiaSanPham_CallBack(c_baogia_id, ui.item.md_sanpham_id) 
                                        return false;
                                  }
                            });
                        } 
                    }
					, formatter: sp_bocai_qo
                },
				{
                    fixed: true, name: 'ma_hscode', label: 'HS Code'
                    , index: 'hs.ma_hscode'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
					, hidden: false
					, editrulea:{ edithidden:true }
                },
                {
                    fixed: true, name: 'ma_sanpham_khach', label: 'Mã SP Khách'
                    , index: 'ctbg.ma_sanpham_khach'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
					, hidden: true
					, editrulea:{ edithidden:true }
                },
                {
                    fixed: true, name: 'giafob', label: 'Giá FOB'
                    , index: 'ctbg.giafob'
                    , width: 100
                    , edittype: 'text'
                    , editable: true
                    , editrules: { required:true, number:true }
                    , editoptions: { defaultValue: '0' }
                    , align:'right'
                    , formatter:'currency'
                    , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
                },
                {
                    fixed: true, name: 'cpdg_vuotchuan', label: 'Chi phí ĐG'
                    , index: 'dg.cpdg_vuotchuan'
                    , width: 100
                    , edittype: 'text'
                    , editable: false
                    , editrules: { required:true, number:true }
                    , editoptions: { defaultValue: '0' }
                    , align:'right'
                    , formatter:'currency'
                    , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
                },
                {
                    fixed: true, name: 'soluong', label: 'Số Lượng'
                    , index: 'ctbg.soluong'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , editrules: { required:true, number:true, minValue:1 }
                    , editoptions: { defaultValue: '1' }
                    , align:'right'
                    , formatter:'number'
                    , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
					, hidden: true
					, editrulea:{ edithidden:true }
                },
                {
                    name: 'md_donggoi_id'
				    , label: 'Đóng Gói'
                    , index: 'dg.ten_donggoi'
                    , fixed:true , width: 100
                    , edittype: 'select'
                    , editable: true
                    , editoptions: { 'value' : 'Chọn đóng gói' }
				    , hidden:true 
				    , editrules: { edithidden:true }
                },
                {
                    fixed: true, name: 'sl_inner', label: 'Đóng Gói Inner'
                    , index: 'ctbg.sl_inner'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'l1', label: 'L1'
                    , index: 'ctbg.l1'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'w1', label: 'W1'
                    , index: 'ctbg.w1'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'h1', label: 'H1'
                    , index: 'ctbg.h1'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'sl_outer', label: 'Đóng Gói Outer'
                    , index: 'ctbg.sl_outer'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'l2', label: 'L2'
                    , index: 'ctbg.l2'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'w2', label: 'W2'
                    , index: 'ctbg.w2'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'h2', label: 'H2'
                    , index: 'ctbg.h2'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
				{
                    fixed: true, name: 'trongluong', label: 'Weight'
                    , index: 'sp.trongluong'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'dientich', label: 'Diện Tích'
                    , index: 'sp.trongluong'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'v2', label: 'V2'
                    , index: 'ctbg.v2'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
				
                {
                    fixed: true, name: 'sl_cont', label: 'SL Cont'
                    , index: 'ctbg.sl_cont'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'ghichu', label: 'Ghi Chú'
                    , index: 'ctbg.ghichu'
                    , width: 100
                    , edittype: 'textarea'
                    , editable:true
                },
                {
                    fixed: true, name: 'ngaytao', label: 'Ngày Tạo'
                    , index: 'ctbg.ngaytao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'nguoitao', label: 'Người Tạo'
                    , index: 'ctbg.nguoitao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'ngaycapnhat', label: 'Ngày Cập Nhật'
                    , index: 'ctbg.ngaycapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'nguoicapnhat', label: 'Người Cập Nhật'
                    , index: 'ctbg.nguoicapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'mota', label: 'Mô tả'
                    , index: 'ctbg.mota'
                    , width: 100
                    , editable:true
                    , edittype: 'textarea'
                    , hidden: true
                },
                {
                    fixed: true, name: 'hoatdong', label: 'Hoạt Động', hidden: true
                    , index: 'ctbg.hoatdong'
                    , width: 100
                    , editable:true
                    , edittype: 'checkbox'
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                },
				{
					name: 'docquyen', label: 'Độc quyền'
					, index: 'docquyen'
					, fixed:true 
					, width: 100
					, editable:true
					, edittype: 'textarea'
					, hidden: false
					, editrules: { edithidden:true }
				},
                {
                    fixed: true, name: 'ghichudonggoingoai', label: 'Đ.Gói Ngoài Mặc Định'
                    , index: 'ctbg.ghichudonggoingoai'
                    , width: 120
                    , edittype: 'textarea'
                    , editable:false
                    , hidden: false
					, editrules: { edithidden:true }
                },
                {
				    fixed: true, name: 'ten_danhmuc'
                    , label: 'TT hàng hóa'
				    , index: 'sp.md_tinhtranghanghoa_id'
				    , width: 100
				    , edittype: 'select'
				    , editoptions: { dataUrl: 'Controllers/DanhMucHangHoaController.ashx?action=getoption' }
				    , editable: false
                    , stype:'select'
                    , searchoptions:{ dataUrl: 'Controllers/DanhMucHangHoaController.ashx?action=getoption' }
				    , hidden: false
			    }
                ]"
                GridComplete = "let o = $('#tabs-quotation-details'); $(this).setGridHeight($(o).height() - 97);$(this).setGridWidth($(o).width())"
                Height = "150"
                OnSelectRow = "
					function(ids) {
						let ma_sanpham = $('#grid_ds_chitietquotation').jqGrid('getCell', ids, 'ma_sanpham') + '';
                        $('#product-view-quotation').attr('src', '');
                        $('#product-view-quotation').attr('src', 'Controllers/API_System.ashx?oper=loadImageProduct&msp=' + ma_sanpham + '&ver=' + uuidv4());
					}
				"
                AddFormOptions="
                    errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                    , beforeShowForm: function (formid) {
                        var masterId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
                        var forId = 'c_baogia_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một quotation mới có thể tạo chi tiết.!');
                        }else{
                            $('#ma_sanpham', formid).mask('**-*****-**-**');
                            $('#ma_sanpham', formid).prop('disabled', false);
                            $('#tr_sothutu', formid).hide();
                            $('#tr_md_donggoi_id', formid).show();
                            $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        }
                    }
					, afterShowForm:populatePackingQO
                    , afterclickPgButtons: populatePackingQO
                    , beforeSubmit: function(postdata, formid) {
                        let masterId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
                        let forId = 'c_baogia_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            return [false,'Hãy chọn một quotation mới có thể tạo chi tiết.!'];
                        }
                        else
                        {
                            postdata.c_baogia_id = masterId;
                            let check_thaydoi = $('#md_donggoi_id','#FrmGrid_grid_ds_chitietquotation').find(':selected').attr('other');
							let check_docquyen = $('#docquyen','#FrmGrid_grid_ds_chitietquotation').val();
                            let check_dmhh = $('#docquyen','#FrmGrid_grid_ds_chitietquotation').attr('dmhh');
                            //check_quycachdonggoi(postdata.ma_sanpham, postdata.md_donggoi_id);
							
                            let checkDanhMucHHFunc = function() {
	                            if(check_dmhh != '') {
		                            if(confirm(check_dmhh)) {
			                            return true;
		                            }
		                            else {
			                            return false;
		                            }
	                            }
	                            else {
		                            return true;
	                            }
                            };

                            let checkDocQuyenFunc = function() {
	                            if(check_docquyen != '') {
		                            if(confirm(check_docquyen)) {
			                            return checkDanhMucHHFunc();
		                            }
		                            else {
			                            return false;
		                            }
	                            }
	                            else {
		                            return checkDanhMucHHFunc();
	                            }
                            };

                            if(check_thaydoi.toUpperCase() == 'TRUE')
                            {
	                            if(confirm('Lưu ý: Kiểu đóng gói bạn chọn có thể thay đổi giá !')) {
		                            return [checkDocQuyenFunc()];
	                            }
	                            else {
		                            return [false];
	                            }
                            }
                            else
                            {
	                            return [checkDocQuyenFunc()];
                            }
                        }
                    }
                    , afterSubmit: function(rs, postdata){
                       return showMsgDialog(rs);
                    }
                    , afterComplete: function()
                    {
                        let selMasterGrid = $('#grid_ds_quotation');
                        let id = $(selMasterGrid).jqGrid('getGridParam', 'selrow');
                        selMasterGrid.trigger('reloadGrid');
                        setTimeout(function(){
                                $(selMasterGrid).jqGrid('setSelection', id);
                        }, 500);
                    }
                "
	            EditFormOptions ="
	                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                    , afterSubmit: function(rs, postdata){
                        return showMsgDialog(rs);
                    }
                    , beforeShowForm: function (formid) {
                        $('#ma_sanpham', formid).mask('**-*****-**-**');
                        $('#ma_sanpham', formid).prop('disabled', true);
                        $('#tr_sothutu', formid).show();
                        $('#tr_md_donggoi_id', formid).show();
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
					, beforeSubmit:function(postdata, formid){
                        let masterId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
                        let forId = 'c_baogia_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            return [false,'Hãy chọn một quotation mới có thể tạo chi tiết.!'];
                        }
                        else
                        {
                            postdata.c_baogia_id = masterId;
							let check_thaydoi = $('#md_donggoi_id','#FrmGrid_grid_ds_chitietquotation').find(':selected').attr('other');
							let check_docquyen = $('#docquyen','#FrmGrid_grid_ds_chitietquotation').val();
                            let check_dmhh = $('#docquyen','#FrmGrid_grid_ds_chitietquotation').attr('dmhh');
                            //check_quycachdonggoi(postdata.ma_sanpham, postdata.md_donggoi_id);
							
                            let checkDanhMucHHFunc = function() {
	                            if(check_dmhh != '') {
		                            if(confirm(check_dmhh)) {
			                            return true;
		                            }
		                            else {
			                            return false;
		                            }
	                            }
	                            else {
		                            return true;
	                            }
                            };

                            let checkDocQuyenFunc = function() {
	                            if(check_docquyen != '') {
		                            if(confirm(check_docquyen)) {
			                            return checkDanhMucHHFunc();
		                            }
		                            else {
			                            return false;
		                            }
	                            }
	                            else {
		                            return checkDanhMucHHFunc();
	                            }
                            };

                            if(check_thaydoi.toUpperCase() == 'TRUE')
                            {
	                            if(confirm('Lưu ý: Kiểu đóng gói bạn chọn có thể thay đổi giá !')) {
		                            return [checkDocQuyenFunc()];
	                            }
	                            else {
		                            return [false];
	                            }
                            }
                            else
                            {
	                            return [checkDocQuyenFunc()];
                            }
                        }
                    }
                    , afterShowForm:populatePackingQO
                    , afterclickPgButtons: populatePackingQO
                    , afterComplete: function()
                    {
                        let selMasterGrid = $('#grid_ds_quotation');
                        let id = $(selMasterGrid).jqGrid('getGridParam', 'selrow');
                        selMasterGrid.trigger('reloadGrid');
                        setTimeout(function(){
                                $(selMasterGrid).jqGrid('setSelection', id);
                        }, 500);
                    }
	            "
	            DelFormOptions="
	                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                    , afterSubmit: function(rs, postdata){
                        return showMsgDialog(rs);
                    }
                    , beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                    , afterComplete: function()
                    {
                        var selMasterGrid = $('#grid_ds_quotation');
                        var id = $(selMasterGrid).jqGrid('getGridParam', 'selrow');
                        selMasterGrid.trigger('reloadGrid');
                        setTimeout(function(){
                                $(selMasterGrid).jqGrid('setSelection', id);
                        }, 500);
                    }
	            "
                MultiSelect = "false" 
                ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
                ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
                ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
                runat="server" />
        </div>
        
        
        
        <div id="qo-details-2">
                <uc1:jqGrid  ID="grid_ds_color_references" 
                SortName="md_color_reference_id" 
                UrlFileAction="Controllers/ColorReferencesController.ashx" 
                ColNames="['md_color_reference_id', 'Số Báo Giá', 'Màu', 'Url', 'Filter', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô Tả', 'Mặc Định']"
                FilterToolbar="true"
                RowNumbers="true"
                OndblClickRow = "
                    function(rowid) { 
                        $(this).jqGrid('editGridRow', rowid,
                        {
                            errorTextFormat:function(data) { 
                                return 'Lỗi: ' + data.responseText 
                            }
                            , afterSubmit: function(rs, postdata){
                                return showMsgDialog(rs);
                            }
                            , beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter();
                            }
                        }); 
                    }
                "
                ColModel = "[
                {
                    fixed: true, name: 'md_color_reference_id'
                    , index: 'color.md_color_reference_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                    fixed: true, name: 'c_baogia_id'
                    , index: 'bg.sobaogia'
                    , width: 100
                    , edittype: 'text'
                    , editable: true
                    , hidden: true
                },
                {
                    fixed: true, name: 'mau'
                    , index: 'color.mau'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                },
                {
                    fixed: true, name: 'url'
                    , index: 'color.url'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                },
                {
                    fixed: true, name: 'filter'
                    , index: 'color.filter'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                },
                {
                    fixed: true, name: 'ngaytao'
                    , index: 'color.ngaytao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'nguoitao'
                    , index: 'color.nguoitao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'ngaycapnhat'
                    , index: 'color.ngaycapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'nguoicapnhat'
                    , index: 'color.nguoicapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'mota'
                    , index: 'color.mota'
                    , width: 100
                    , editable:true
                    , edittype: 'textarea'
                },
                {
                    fixed: true, name: 'hoatdong'
                    , index: 'color.hoatdong'
                    , width: 100
                    , editable:true
                    , edittype: 'checkbox'
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                }
                ]"
                GridComplete = "let o = $('#tabs-quotation-details'); $(this).setGridHeight($(o).height() - 97); $(this).setGridWidth($(o).width())"
                Height = "150"
                ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
                ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
                ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
                OnSelectRow = "
                    function(ids) {
		                if(ids == null) {
			                ids=0;
				        }
                         let ma_sanpham = $(this).jqGrid('getCell', ids, 'ma_sanpham');
                        $('#img-product').attr('src', '');
                        $('#img-product').attr('src', 'Controllers/API_System.ashx?oper=loadImageProduct&msp=' + ma_sanpham + '&ver=' + uuidv4());
                     }
                "
                AddFormOptions="
                afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , beforeShowForm: function (formid) {
                     var masterId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
                        var forId = 'c_baogia_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một quotation mới có thể tạo chi tiết.!');
                        }else{
                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                        }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
                    var forId = 'c_baogia_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một quotation mới có thể tạo chi tiết.!'];
                    }else{
                        postdata.c_baogia_id = masterId;
                        return [true,'']; 
                    }
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }"
	            EditFormOptions ="
	            afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }"
                MultiSelect = "false"
                runat="server" />
        </div>
        
    </div> 
    <div class="ui-layout-east" style="text-align:center; background:#f4f0ec">
        <table style="width:100%; height:100%">
            <tr>
                <td>
                    <img id="product-view-quotation" class="loadingIMG" style="max-width:100%; margin: 0px auto; min-width: 40px;" src="Controllers/API_System.ashx?oper=loadImageProduct&msp=default" />
                </td>
            </tr>
        </table>
    </div> 
	<script type="text/javascript">
	    $("#grid_ds_quotation").jqGrid('navGrid', "#grid_ds_quotation-pager").jqGrid('navButtonAdd', "#grid_ds_quotation-pager",
		{
		    caption: "",
		    buttonicon: "ui-icon-battery-3",
		    onClickButton: TaoColorReferences,
		    title: "Tạo Color References",
		    cursor: "pointer"
		});

	    $("#grid_ds_quotation").jqGrid('navGrid', "#grid_ds_quotation-pager").jqGrid('navButtonAdd', "#grid_ds_quotation-pager",
		{
		    caption: "",
		    buttonicon: "icon-ghepbo-qo",
		    onClickButton: function () {
		        var qoId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
		        if (qoId == null) {
		            alert('Hãy chọn một báo giá');
		        } else {
		            var qoId = $('#grid_ds_quotation').jqGrid('getGridParam', 'selrow');
		            var name = 'dialog-ghepbo-qo';
		            var title = 'Ghép bộ';
		            var content = `
						<table style="width:100%">
							<tr>
								<td  colspan=2>
									<label style="font-weight: bold;font-size: 13px;">Bạn có chắc chắn muốn ghép bộ?</label>
									<br>
									<label style="font-weight: bold;font-size: 13px; line-height: 2">Lưu ý:</label>
									<br>
									<label style="font-style:italic; color: blue; line-height: 2">
										1. Chức năng này chỉ sử dụng được 1 lần.
										<br>
										2. Nếu đã chắc chắn, hãy chọn 1 trong 2 tiêu chí bên dưới và nhấn "Đồng ý".
									</label>
								</td>
							</tr>
							<tr>
								<td style="height: 3px" colspan=2>
								</td>
							</tr>
							<tr>
								<td style="width:20px">
									<input id="ghepbo-qo-chk1" type="radio" name="ghepbo-qo-chk1" value="0" />
								</td>
								<td>
									Ghép bộ không xóa dữ liệu
								</td>
							</tr>
							<tr>
								<td style="height: 5px" colspan=2>
								</td>
							</tr>
							<tr>
								<td  style="width:20px">
									<input id="ghepbo-qo-chk2" type="radio" name="ghepbo-qo-chk1" value="1" />
								</td>
								<td>
									Ghép bộ xóa dữ liệu
								</td>
							</tr>
						</table>`;

		            $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
		            $('#' + name).dialog({
		                modal: true,
		                open: function (event, ui) {
		                    //hide close button.
		                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
		                },
		                buttons: [
						{
						    id: "btn-ghepbo-ok",
						    text: "Đồng ý",
						    click: function () {
						        var printType = $('input[name=ghepbo-qo-chk1]:checked', '#' + name).val();
						        if (printType == null) {
						            alert('Hãy chọn tiêu chí để ghép bộ.');
						        } else {
						            $("#btn-ghepbo-ok").button("disable");
						            $("#btn-ghepbo-close").button("disable");
						            $('#' + name).prepend('<div id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>');
						            $.post('Controllers/API_Android.ashx?oper=ESC_ghepbo',
									{ type: printType, c_baogia_id: qoId }, function (res) {
									    res = res.split('#');
									    if (res[0] == 'true') {
									        $("#grid_ds_quotation").jqGrid('setCell', qoId, 'ghepbo', 'True');
									        $('#' + name).html(res[1]);
									        $('#grid_ds_chitietquotation').jqGrid().trigger('reloadGrid');
									    }
									    else {
									        $('#' + name).html('<div style="color:red">' + res[1] + '</div>');
									    }
									    $("#btn-ghepbo-close").button("enable");
									});
						        }
						    }
						},
						{
						    id: "btn-ghepbo-close",
						    text: "Thoát",
						    click: function () {
						        $(this).dialog("destroy").remove();
						    }
						}
		                ]
		            });
		        }
		    },
		    title: "Ghép bộ",
		    cursor: "pointer"
		});

	    createRightPanel('grid_ds_quotation');
	    createRightPanel('grid_ds_chitietquotation');
	    createRightPanel('grid_ds_color_references');
    </script>
</DIV>

<!--{
	fixed: true, name: 'gia_db', hidden: false
	, index: 'baogia.gia_db'
	, width: 100
	, editable:true
	, edittype: 'select'
	, align: 'center'
	, stype:'select'
	, searchoptions:{sopt:['eq'] ,value:{'':'[ALL]','0':'giá mặc định','1':'giá đặc biệt'}}
	, editoptions:{value:{'0':'giá mặc định','1':'giá đặc biệt'}}
	, formatter: 'checkbox'
}-->

   