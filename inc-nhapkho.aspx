<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-nhapkho.aspx.cs" Inherits="inc_nhapkho" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
        $('#lay-center-nhapkho').parent().layout({
            north: {
                size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                var o = $("#layout-north-nhapkho").height();
                $("#grid_ds_nhapkho").setGridHeight(o - 90);
                }
            },
            center: {
                onresize_end: function() {
                var o = $("#lay-center-nhapkho").height();
                $("#grid_ds_chitietnhapkho").setGridHeight(o - 90);
                }
            }
        })
    });

    function activeNhapKho() {
        var id = $('#grid_ds_nhapkho').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            var msg = "Bạn hãy chọn 1 phiếu nhập để hiệu lực";
            var div = '<div title="Thông báo" id="dialog-activenhapkho"><h3>' + msg + '</h3></div>';
            $('body').append(div);
            $('#dialog-activenhapkho').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function() {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn hiệu lực phiếu nhập này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-activenhapkho\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-activenhapkho').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-activenhapkho",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-activenhapkho").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/NhapKhoController.ashx?action=activenk&nkId=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-activenhapkho').find('#dialog-caution').html(result);
                                $('#grid_ds_nhapkho').jqGrid().trigger('reloadGrid');
                            });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Không",
                        click: function() {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
        }
    }
	
	function deactiveNhapKho() {
        var id = $('#grid_ds_nhapkho').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            var msg = "Bạn hãy chọn 1 phiếu nhập để soạn thảo";
            var div = '<div title="Thông báo" id="dialog-activenhapkho"><h3>' + msg + '</h3></div>';
            $('body').append(div);
            $('#dialog-activenhapkho').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function() {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn soạn thảo phiếu nhập này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-activenhapkho\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-activenhapkho').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-activenhapkho",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-activenhapkho").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/NhapKhoController.ashx?action=deactivenk&nkId=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-activenhapkho').find('#dialog-caution').html(result);
                                $('#grid_ds_nhapkho').jqGrid().trigger('reloadGrid');
                            });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Không",
                        click: function() {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
        }
    }

    function createPhieuXuatKho() {
        var id = $('#grid_ds_nhapkho').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            var msg = "Bạn hãy chọn 1 phiếu nhập để tạo phiếu xuất";
            var div = '<div title="Thông báo" id="dialog-taophieuxuatkho"><h3>' + msg + '</h3></div>';
            $('body').append(div);
            $('#dialog-taophieuxuatkho').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function() {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn tạo phiếu xuất từ phiếu nhập này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-taophieuxuatkho\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-taophieuxuatkho').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-taophieuxuatkho",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-taophieuxuatkho").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/NhapKhoController.ashx?action=createxk&nkId=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-taophieuxuatkho').find('#dialog-caution').html(result);
                                $('#grid_ds_nhapkho').jqGrid().trigger('reloadGrid');
                            });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Không",
                        click: function() {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
        }
    }

    function funcPrintNK() {
        var xkId = $('#grid_ds_nhapkho').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-nk';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
                            '<tr>' +
                                '<td>' +
                                '</td>' +

                                '<td>' +
                                    '<input id="printRequired" type="radio" name="rdoPrintType" value="printRequired" checked="checked" />' +
                                    '<label for="printRequired">In phiếu nhập kho</label>' +
                                '</td>' +
                            '</tr>' +
                      '</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
            modal: true
            , open: function(event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
            , buttons: [
            {
                id: "btn-print-ok",
                text: "In",
                click: function() {
                    var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-nk').val();
                    var printURL = "PrintControllers/";
                    var windowSize = "width=700,height=700,scrollbars=yes";
                    if (typeof printType != 'undefined') {
                        if (printType == "printRequired") {
                            if (xkId != null) {
                                printURL += "InPhieuNhapHang/";
                                window.open(printURL + "?c_nhapxuat_id=" + xkId, "In Phiếu Nhập Kho", windowSize);
                            } else {
                                alert('Bạn chưa chọn phiếu nhập kho');
                            }
                        } else {
                        printURL += "InDSPhieuNhapHang/";
                            window.open(printURL, "In Danh Sách Phiếu Nhập Kho", windowSize);
                        }
                    } else {
                        alert('Hãy chọn cách in.!');
                    }
                }
            }
            , {
                id: "btn-print-close",
                text: "Thoát",
                click: function() {
                    $(this).dialog("destroy").remove();
                }
            }
            ]
        });
    }
    
</script>
<DIV class="ui-layout-north ui-widget-content" id="layout-north-nhapkho">
<uc1:jqGrid  ID="grid_ds_nhapkho"
            Caption="Nhập Kho" 
            SortName="nx.ngaytao"
            FuncActiveNhapKho = "activeNhapKho"
            FuncDeactiveNhapKho = "deactiveNhapKho"
            FuncTaoPhieuXuatKho="createPhieuXuatKho"
            BtnPrint="funcPrintNK"
            FuncTaoPhieuXuatKhoN="function(){ add_tab('Tạo phiếu xuất kho nhiều phiếu nhập', 'inc-taophieuxuat-nhieuphieunhat.aspx')}"
            FuncTaoPhieuNhapXuat="function(){ add_tab('Tạo phiếu nhập kho', 'inc-taophieunhapxuat-tupi.aspx')}"
            UrlFileAction="Controllers/NhapKhoController.ashx" 
            FilterToolbar="true"
            ColNames="['c_nhapxuat_id', 'TT', 'Đơn Hàng', 'Số Phiếu', 'Số Phiếu NX'
            , 'Ngày Giao Nhận', 'Đối Tác', 'Người Giao', 'Người Nhập', 'Số Phiếu Khách'
            , 'Ngày Phiếu', 'Kho', 'Số Seal', 'Số Container', 'Loại Cont', 'Phí cộng', 'Phí trừ'
            , 'Discount(%)', 'Tổng tiền'
            , 'Loại Chứng Từ', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật'
            , 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
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
                fixed: true, name: 'c_nhapxuat_id'
                , index: 'c_nhapxuat_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_trangthai_id'
                 , index: 'nx.md_trangthai_id'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , align: 'center'
                 , formatter:'imagestatus'
            },
            {
                 fixed: true, name: 'c_donhang_id'
                 , index: 'dh.sochungtu'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/DonHangController.ashx?action=getoption',  readonly:'readonly' }
                 
            },  
            {
                 fixed: true, name: 'sophieu'
                 , index: 'sophieu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'sophieunx'
                 , index: 'sophieunx'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { readonly:'readonly' }
            },
            {
                 fixed: true, name: 'ngay_giaonhan'
                 , index: 'ngay_giaonhan'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                 fixed: true, name: 'md_doitackinhdoanh_id'
                 , index: 'dtkd.ten_dtkd'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption', readonly:'readonly' }
            },
            {
                 fixed: true, name: 'nguoigiao'
                 , index: 'nguoigiao'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'nguoinhan'
                 , index: 'nguoinhan'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'sophieukhach'
                 , index: 'sophieukhach'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ngay_phieu'
                 , index: 'ngay_phieu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                 fixed: true, name: 'nhapxuat_kho_id'
                 , index: 'nhapxuat_kho_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/WarehouseController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'soseal'
                 , index: 'soseal'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'container'
                 , index: 'container'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'loaicont'
                 , index: 'loaicont'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { value:'CONT20:Cont 20;CONT40:Cont 40;CONT45:Cont 45;40HC:40HC;LE:Cont Lẻ' }
            },
            {
                 fixed: true, name: 'phicong'
                 , index: 'phicong'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'phitru'
                 , index: 'phitru'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'discount'
                 , index: 'discount'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { readonly:'readonly' }
            },
            {
                 fixed: true, name: 'grandtotal'
                 , index: 'grandtotal'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { readonly:'readonly' }
            },
            {
                 fixed: true, name: 'md_loaichungtu_id'
                 , index: 'md_loaichungtu_id'
                 , editable: false
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { value:'NK:Nhập Kho;XK:Xuất Kho' }
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()-90);"
            Height = "170"
            MultiSelect = "false" 
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
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
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_chitietnhapkho').jqGrid('getGridParam','records') &gt; 0 )
			                {
							
							$.ajax({
								url: 'Controllers/NhapXuatCalTotalController.ashx?c_nhapxuat_id='+ids,
								beforeSend:function(){
									$('#grid_ds_nhapkho').jqGrid('setCaption','Nhập kho: đang tải...');
								},
								success: function(data){
									 $('#grid_ds_nhapkho').jqGrid('setCaption','Nhập kho: ' +  data);
								}
							});
							
				                $('#grid_ds_chitietnhapkho').jqGrid('setGridParam',{url:'Controllers/DongNhapXuatController.ashx?&whId='+ids,page:1});
				                $('#grid_ds_chitietnhapkho').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
						
							$.ajax({
								url: 'Controllers/NhapXuatCalTotalController.ashx?c_nhapxuat_id='+ids,
								beforeSend:function(){
									$('#grid_ds_nhapkho').jqGrid('setCaption','Nhập kho: đang tải...');
								},
								success: function(data){
									 $('#grid_ds_nhapkho').jqGrid('setCaption','Nhập kho: ' +  data);
								}
							});
						
			                $('#grid_ds_chitietnhapkho').jqGrid('setGridParam',{url:'Controllers/DongNhapXuatController.ashx?&whId='+ids,page:1});
			                $('#grid_ds_chitietnhapkho').jqGrid().trigger('reloadGrid');			
		                } }"
            runat="server" />
</DIV>

<DIV class="ui-layout-center ui-widget-content" id="lay-center-nhapkho" style="padding:0;"> 
			<uc1:jqGrid  ID="grid_ds_chitietnhapkho" 
			Caption = "Chi Tiết Nhập Kho"
            SortName="sp.ma_sanpham"
            SortOrder="asc" 
            UrlFileAction="Controllers/DongNhapXuatController.ashx" 
            ColNames="['c_dongnhapxuat_id', 'Nhập Xuất', 'Chi Tiết Đơn Hàng','STT', 'Mã SP'
                    , 'Mã SP', 'Mô Tả TV', 'Đơn Vị Tính'
                    , 'SL Phải Nhập', 'SL Thực Nhập', 'Đơn Giá'
                    , 'Số Kiện Thực Tế', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật'
                    , 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            FilterToolbar="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{
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
                fixed: true, name: 'c_dongnhapxuat_id'
                , index: 'c_dongnhapxuat_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'c_nhapxuat_id'
                 , index: 'c_nhapxuat_id'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , hidden: true
            },
            {
                fixed: true, name: 'c_dongdonhang_id'
                , index: 'c_dongdonhang_id'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
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
                , index: 'sp.ma_sanpham'
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
                , index: 'mota_tiengviet'
                , width: 200
                , edittype: 'textarea'
                , editable:true
            },
            {
                fixed: true, name: 'md_donvitinh_id'
                , index: 'md_donvitinh_id'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/UnitProductController.ashx?action=getoption' }
                , align:'center'
            },
            {
                fixed: true, name: 'slphai_nhapxuat'
                , index: 'slphai_nhapxuat'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules: { required:true, number:true, minValue:'1' }
                , editoptions:{ defaultValue: '1' }
                , align:'right'
                , formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
            },
            {
                fixed: true, name: 'slthuc_nhapxuat'
                , index: 'slthuc_nhapxuat'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules: { required:true, number:true, minValue:'1' }
                , editoptions:{ defaultValue: '1' }
                , align:'right'
                , formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
            },
            {
                fixed: true, name: 'dongia'
                , index: 'dongia'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules: { required:true, number:true, minValue:'1' }
                , editoptions:{ defaultValue: '1' }
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 'auto', prefix: ''}
            },
            {
                fixed: true, name: 'sokien_thucte'
                , index: 'sokien_thucte'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules: { required:true, number:true, minValue:'0' }
                , editoptions:{ defaultValue: '0' }
                , align:'right'
                , formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            Height = "150"
            MultiSelect = "false" 
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
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
            runat="server" />
	</DIV> 

<script type="text/javascript">
    createRightPanel('grid_ds_nhapkho');
    createRightPanel('grid_ds_chitietnhapkho');
</script>

