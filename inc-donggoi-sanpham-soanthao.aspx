<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-donggoi-sanpham-soanthao.aspx.cs" Inherits="inc_donggoi_sanpham_soanthao" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	function ChangeStatus() {
        var md_donggoi_id = $('#grid_ds_donggoi_soanthao').jqGrid('getGridParam', 'selrow');
            $('body').append("<div title=\"Thay đổi trạng thái cho chủng loại hàng hóa\" id=\"dialog-changestatus\">" +
            "<div style='display:none' id='wait'><img style='width:30px; height:30px' src='iconcollection/loading.gif'/></div>" +
            "<div id=\"caution\"></div>"+
            "<table>" +
                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"HIEULUC\" value=\"HIEULUC\" name=\"rdoStatus\" checked=\"checked\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"HIEULUC\">Chuyển trạng thái \"Hiệu lực\"</label>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"SOANTHAO\" value=\"SOANTHAO\" name=\"rdoStatus\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"SOANTHAO\">Chuyển trạng thái \"Soạn thảo\"</label>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"toactive\" value=\"TOACTIVE\" name=\"rdoStatus\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"toactive\">Chuyển tất cả chủng loại \"Soạn thảo\" sang \"Hiệu lực\"</label>" +
                    "</td>" +
                "</tr>" +

            "</table>" +
        "</div>");

            $("#dialog-changestatus").dialog({
                width: 500
                , height:250
                , modal: true
                , open: function(event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
                , buttons: [
                {
                    id: "btn-change",
                    text: "Thay đổi",
                    click: function() {
                        var pagram = $('#grid_ds_donggoi_soanthao').jqGrid('getGridParam', 'postData');
                        var statusType = $('input[name=rdoStatus]:checked', '#dialog-changestatus').val();
                        if (typeof statusType != 'undefined') {
                            $("#dialog-changestatus").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-change").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-changestatus").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get('Controllers/PackingControllerSOANTHAO.ashx?action=updatestatus&md_donggoi_id=' + md_donggoi_id + '&trangthai=' + statusType, pagram, function(result) {
                                $("#dialog-changestatus").find("#caution").append(result);
                                $('#grid_ds_donggoi_soanthao').jqGrid().trigger('reloadGrid');
                            });
                        } else {
                            alert('Chưa chọn trạng thái cho đóng gói.!');
                        }
                    }
                }
                , {
                    id: "btn-close",
                    text: "Thoát",
                    click: function() {
                        $(this).dialog("destroy").remove();
                    }
                }
                ]
            });
    }
	
    $(function() {
        $('#lay-center-donggoisanpham-soanthao').parent().layout({
            north: {
                size: "50%"
                , onresize_end: function() {
                    var o = $("#lay-north-donggoisanpham-soanthao").height();
                    $("#grid_ds_donggoi_soanthao").setGridHeight(o - 90);
                }
            }
            , center: {
                onresize_end: function() {
                    var o = $("#lay-center-donggoisanpham-soanthao").height();
                    $("#grid_ds_donggoi_soanthaosanpham_soanthao").setGridHeight(o - 90);
                }
            }
        });
    });
	
	 function funcPrintNT() {
        var xkId = $('#grid_ds_donggoi_soanthao').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-nk';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
                            '<tr>' +
                                '<td>' +
                                '</td>' +

                                '<td>' +
                                    '<input id="printRequired" type="radio" name="rdoPrintType" value="printRequired" checked="checked" />' +
                                    '<label for="printRequired">Chiết xuất danh mục đóng gói</label>' +
                                '</td>' +
                            '</tr>' +
							'<tr>' +
                                '<td>' +
                                '</td>' +

                                '<td>' +
                                    '<input id="printExcel" type="radio" name="rdoPrintType" value="printExcel" />' +
                                    '<label for="printExcel">Chiết xuất Excel</label>' +
                                '</td>' +
                            '</tr>' +
                      '</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
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
                    var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-nk').val();
                    var printURL = "PrintControllers/";
                    var windowSize = "width=700,height=700,scrollbars=yes";
                    if (typeof printType != 'undefined') {
                        if (printType == "printRequired") {
                            var postfilt = $('#grid_ds_donggoi_soanthao').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InCXDanhMucDongGoi/";
                            window.open(printURL + "?md_donggoi_id=" + xkId + "&filters=" + postfilt, "Chiết xuất danh mục đóng gói", windowSize);
                        }
						else {
							var postfilt = $('#grid_ds_donggoi_soanthao').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InCXDanhMucDongGoi/InDMDG.aspx";
                            window.open(printURL + "?md_donggoi_id=" + xkId + "&filters=" + postfilt, "Chiết xuất danh mục đóng gói", windowSize);
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
</script>
<DIV class="ui-layout-north ui-widget-content" id="lay-north-donggoisanpham-soanthao">    
    <uc1:jqGrid  ID="grid_ds_donggoi_soanthao" 
                Caption="Cách Đóng Gói"
                SortName="md_donggoi_id" 
                FilterToolbar = "true"
                UrlFileAction="Controllers/PackingControllerSOANTHAO.ashx"
				BtnPrint="funcPrintNT"
				FuncChangeStatus="ChangeStatus"
                RowNumbers="true"
                OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{   
                    width:700,
                    errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                    , afterSubmit: function(rs, postdata){
                        return showMsgDialog(rs); 
                    }
                    , beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                    }
                
                }); }"
                ColModel="[
                {
                    fixed: true, name: 'md_donggoi_id'
                    , index: 'dg.md_donggoi_id'
                    , label: 'md_donggoi_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
				{
					fixed: true, name: 'trangthai'
					, index: 'trangthai'
                    , label: 'Trạng thái'
					, width: 100
					, editable:false
					, formatter:'imagestatus'
					, align: 'center'
					, search: false
					, viewable: false
					, sortable: true
					, hidden : true
				},
                {
                     fixed: true, name: 'ma_donggoi'
                     , index: 'dg.ma_donggoi'
                     , label: 'Mã Đóng Gói'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'ten_donggoi'
                     , index: 'dg.ten_donggoi'
                     , label: 'Tên Đóng Gói'
                     , editable: false
                     , width: 100
                     , edittype: 'text'
					 , hidden : true
                },
                {
                    fixed: true, name: 'sl_inner'
                    , index: 'dg.sl_inner'
                    , label: 'SL Inner'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ required:true, number:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'dvtinner'
                    , index: 'dvt.ten_dvt'
                    , label: 'ĐVT Inner'
                    , width: 50
                    , edittype: 'select'
                    , editable:true
                    , editoptions: { dataUrl: 'Controllers/UnitController.ashx?action=getoption' }
                    , editrules:{ required:true }
                    , sortable: false
                },
                {
                    fixed: true, name: 'l1'
                    , index: 'dg.l1'
                    , label: 'L1'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'w1'
                    , index: 'dg.w1'
                    , label: 'W1'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'h1'
                    , index: 'dg.h1'
                    , label: 'H1'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'sl_outer'
                    , index: 'dg.sl_outer'
                    , label: 'SL Outer'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'dvtouter'
                    , index: 'dvt.ten_dvt'
                    , label: 'ĐVT Outer'
                    , width: 50
                    , edittype: 'select'
                    , editable:true
                    , editoptions: { dataUrl: 'Controllers/UnitController.ashx?action=getoption' }
                    , editrules:{ required:true }
                    , sortable: false
                },
                {
                    fixed: true, name: 'l2_mix'
                    , index: 'dg.l2_mix'
                    , label: 'L2'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'w2_mix'
                    , index: 'dg.w2_mix'
                    , label: 'W2'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'h2_mix'
                    , index: 'dg.h2_mix'
                    , label: 'H2'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'v2'
                    , index: 'dg.v2'
                    , label: 'V2'
                    , width: 50
                    , edittype: 'text'
                    , editable: true
					, hidden : true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'sl_cont_mix'
                    , index: 'dg.sl_cont_mix'
                    , label: 'Số Lượng Cont Mix'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , hidden : true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'soluonggoi_ctn_20'
                    , index: 'dg.soluonggoi_ctn_20'
                    , label: 'SL Gói/cnt20'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
				{
                    fixed: true, name: 'soluonggoi_ctn'
                    , index: 'dg.soluonggoi_ctn'
                    , label: 'SL Gói/cnt40'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
				{
                    fixed: true, name: 'soluonggoi_ctn_40hc'
                    , index: 'dg.soluonggoi_ctn_40hc'
                    , label: 'SL Gói/cnt40 HC'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'nw1'
                    , index: 'dg.nw1'
                    , label: 'NW1'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'gw1'
                    , index: 'dg.gw1'
                    , label: 'GW1'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'nw2'
                    , index: 'dg.nw2'
                    , label: 'NW2'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'gw2'
                    , index: 'dg.gw2'
                    , label: 'GW2'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'vtdg2'
                    , index: 'dg.vtdg2'
                    , label: 'VTDG2'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'cpdg_vuotchuan'
                    , index: 'dg.cpdg_vuotchuan'
                    , label: 'CPĐG vượt chuẩn (USD)'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                    , formoptions: { colpos: 2, rowpos: 8 }
					, hidden : false
                },
                {
                    fixed: true, name: 'trongluongdonggoi'
                    , index: 'dg.trongluongdonggoi'
                    , label: 'Trọng lượng đóng gói'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { number:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                    , hidden: true
                    , hidedlg: true
                },
                {
                    fixed: true, name: 'vd'
                    , index: 'dg.vd'
                    , label: 'VD'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                    , formoptions: { colpos: 2, rowpos: 2 }
					, hidden : true
                },
                {
                    fixed: true, name: 'vn'
                    , index: 'dg.vn'
                    , label: 'VN'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                    , formoptions: { colpos: 2, rowpos: 3 }
					, hidden : true
                },
                {
                    fixed: true, name: 'vl'
                    , index: 'dg.vl'
                    , label: 'VL'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                    , formoptions: { colpos: 2, rowpos: 4 }
					, hidden : true
                },
                {
                    fixed: true, name: 'ghichu_vachngan'
                    , index: 'dg.ghichu_vachngan'
                    , label: 'Ghi Chú Vách Ngăn'
                    , width: 50
                    , edittype: 'textarea'
                    , editable:true
                    , formoptions: { colpos: 2, rowpos: 5 }
					, hidden : true
                },
                {
                    fixed: true, name: 'mix_chophepsudung'
                    , index: 'dg.mix_chophepsudung'
                    , label: 'Mix Cho Phép Sử Dụng'
                    , width: 50
                    , edittype: 'checkbox'
                    , editable:true
                    , hidden : true
                    , editrules : { edithidden:true }
                    , formoptions: { colpos: 2, rowpos: 6 }
                },
                {
					fixed: true, name: 'ngayxacnhan'
					, index: 'dg.ngayxacnhan'
                    , label: 'Ngày Xác Nhận'
					, editable: true
					, width: 50
					, edittype: 'text'
					, editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
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
                    fixed: true, name: 'mota'
                    , index: 'dg.mota'
                    , label: 'Ghi chú'
                    , width: 50
                    , editable:true
                    , edittype: 'textarea'
                    , editrules: { edithidden: true }
                    , formoptions: { colpos: 2, rowpos: 9 }
                    
                },
				{
                    fixed: true
					, name: 'doigia_donggoi'
                    , index: 'dg.doigia_donggoi'
                    , label: 'Đổi giá theo ĐG'
                    , width: 50
                    , edittype: 'checkbox'
                    , editable:true
                    , hidden : false
                    , editrules : { edithidden:true }
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                    , formoptions: { colpos: 2, rowpos: 7 }
                },
                {
                    fixed: true, name: 'ngaytao'
                    , index: 'dg.ngaytao'
                    , label: 'Ngày Tạo'
                    , width: 50
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'nguoitao'
                    , index: 'dg.nguoitao'
                    , label: 'Người Tạo'
                    , width: 50
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'ngaycapnhat'
                    , index: 'dg.ngaycapnhat'
                    , label: 'Ngày Cập Nhật'
                    , width: 50
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'nguoicapnhat'
                    , index: 'dg.nguoicapnhat'
                    , label: 'Người Cập Nhật'
                    , width: 50
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'hoatdong', hidden: true
                    , index: 'dg.hoatdong'
                    , label: 'Hoạt động'
                    , width: 50
                    , editable:true
                    , edittype: 'checkbox'
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                }
                ]"
                Height = "420"
                AddFormOptions=" 
                    width:990,
                    errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                    , afterSubmit: function(rs, postdata){
                        return showMsgDialog(rs); 
                    }
                    , beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                    }
                    "
                EditFormOptions ="
                    width:700,
                    errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                    , afterSubmit: function(rs, postdata){
                        return showMsgDialog(rs); 
                    }
                    , beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                    }
                    "
                ViewFormOptions="
                    width:700,
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
                }"
                GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
                OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_donggoi_soanthaosanpham_soanthao').jqGrid('getGridParam','records') &gt; 0 )
			                {
			                    var caption = $(this).jqGrid('getCell', ids, 'ten_donggoi');
			                    $('#grid_ds_donggoi_soanthaosanpham_soanthao').jqGrid('setCaption','Đóng gói sản phẩm: ' +  caption);
				                $('#grid_ds_donggoi_soanthaosanpham_soanthao').jqGrid('setGridParam',{url:'Controllers/PackingProductController.ashx?&packingId='+ids,page:1});
				                $('#grid_ds_donggoi_soanthaosanpham_soanthao').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
				            var caption = $(this).jqGrid('getCell', ids, 'ten_donggoi');
			                $('#grid_ds_donggoi_soanthaosanpham_soanthao').jqGrid('setCaption', 'Đóng gói sản phẩm theo cách : ' + caption);
			                $('#grid_ds_donggoi_soanthaosanpham_soanthao').jqGrid('setGridParam',{url:'Controllers/PackingProductController.ashx?&packingId='+ids,page:1});
			                $('#grid_ds_donggoi_soanthaosanpham_soanthao').jqGrid().trigger('reloadGrid');			
		                } }"
                ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
                ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
                ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
                runat="server" />
                
    
</DIV>
<DIV class="ui-layout-center ui-widget-content" id="lay-center-donggoisanpham-soanthao">
    <uc1:jqGrid  ID="grid_ds_donggoi_soanthaosanpham_soanthao"
            Caption ="Đóng gói sản phẩm"
            SortName="dgsp.md_donggoisanpham_id" 
            FilterToolbar = "true"
            UrlFileAction="Controllers/PackingProductController.ashx" 
            ColNames="['md_donggoisanpham_id', 'Đóng Gói', 'Mã SP', 'Mã SP', 'Số Lượng', 'Mặc Định', 
			'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {   
            
                    errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                    , afterSubmit: function(rs, postdata){
                        return showMsgDialog(rs); 
                    }
                    , beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                    }
            
            }); }"
            ColModel = "[
            {
                fixed: true, name: 'md_donggoisanpham_id'
                , index: 'dgsp.md_donggoisanpham_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'md_donggoi_id'
                , index: 'dg.md_donggoi_id'
                , width: 100
                , editable:true
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'sanpham_id'
                , index: 'sp.md_sanpham_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , editrules:{ required:true }
            },
            {
                fixed: true, name: 'tensp'
                , index: 'sp.ma_sanpham'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ required:true }
                , editoptions: { 
                    dataInit : function (elem) { 
                        $(elem).combogrid({
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
                 fixed: true, name: 'soluong'
                 , index: 'dgsp.soluong'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ defaultValue: '1' }
                 , editrules:{ required:true, number:true }
                 , align : 'right'
            },
            {
                 fixed: true, name: 'macdinh'
                 , index: 'dgsp.macdinh'
                 , editable: true
                 , width: 100
                 , edittype: 'checkbox'
                 , align: 'center'
                 , editoptions:{ value:'True:False', defaultValue: 'True' }
                 , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'dgsp.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'dgsp.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'dgsp.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'dgsp.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'mota'
                , index: 'dgsp.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'dgsp.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            Height = "420"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            AddFormOptions=" 
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,beforeShowForm: function (formid) {
                    var masterId = $('#grid_ds_donggoi_soanthao').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_donggoi_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        alert('Hãy chọn một cách đóng gói mới có thể tạo chi tiết.!');
                    }else{
                        $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_donggoi_soanthao').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_donggoi_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một cách đóng gói mới có thể tạo chi tiết.!'];
                    }else{
                        postdata.md_donggoi_id = masterId;
                        return [true,'']; 
                    }
                }
                , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
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
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
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
            }"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            runat="server" />
</DIV>

<script>
	if(chietxuatDGST.toLowerCase() == 'false') {
		var parent = $('#grid_ds_donggoi_soanthao-pager_left');
		parent.find('.ui-icon.ui-icon-print').parent().parent().remove();
	}

	createRightPanel('grid_ds_donggoi_soanthao');
	createRightPanel('grid_ds_donggoi_soanthaosanpham_soanthao');
</script>