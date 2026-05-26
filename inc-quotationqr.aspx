<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-quotationqr.aspx.cs" Inherits="inc_quotationqr" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>

<script>
    $(document).ready(function () {

        $('#lay-center-quotationqr').parent().layout({
            south: {
                size: "50%"
            }
            , center: {
                onresize: function () {
                    var h = $("#lay-center-quotationqr").height();
                    $("#grid_ds_quotationqr").setGridHeight(h - 90);
                }
            }
        });

        // Layout nho duoi
        $('#layout-south-quotationqr').layout({
            center: {
                onresize: function () {
                    var o = $("#tabs-quotationqr-details")
                    $("#grid_ds_chitietquotationqr").setGridWidth(o.width());
                    $("#grid_ds_color_referencesqr").setGridWidth(o.width());

                    $("#grid_ds_chitietquotationqr").setGridHeight(o.height() - 97);
                    $("#grid_ds_color_referencesqr").setGridHeight(o.height() - 97);
                }
            }
        });

        $('#tabs-quotationqr-details').tabs();
    });

    function funcPrint() {
        var qoId = $('#grid_ds_quotationqr').jqGrid('getGridParam', 'selrow');
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
        $('#' + name).dialog(
            {
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: [
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
</script>
<DIV class="ui-layout-center ui-widget-content" id="lay-center-quotationqr" style="padding:0;">
    <uc1:jqGrid  ID="grid_ds_quotationqr"
            Caption="Báo Giá QR" 
            SortName="baogia.ngaytao" 
            SortOrder="desc"
            UrlFileAction="Controllers/QuotationQRController.ashx" 
            ColNames="['c_baogia_id', 'TT', 'Số Báo Giá', 'Khách Hàng'
                    , 'Ngày Báo Giá', 'Ngày Hết Hạn', 'Cảng Biển', 'Shipmenttime' , 'MOQ/Item Color'
                    , 'Tr.Lượng' , 'CBM', 'Total Quotation'
                    , 'Đồng Tiền', 'Kích Thước', 'QO mẫu', 'Ngày Tạo'
                    , 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật'
                    , 'Ghi chú', 'Hoạt động', 'Giá đặc biệt'
                    , 'Giá FOB phiên bản cũ', 'Phiên bản giá cũ', 'Đã ghép bộ']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { 
								/*$(this).jqGrid('editGridRow', rowid, { 
									errorTextFormat:function(data) { 
										return 'Lỗi: ' + data.responseText 
									}
									, afterSubmit: function(rs, postdata){
										return showMsgDialog(rs); 
									}
									, beforeShowForm: function (formid) {
                                        var frmID = '#' + formid.attr('id');
										$('#gia_db', frmID).prop('disabled', true);
										$('#isform', frmID).prop('disabled', true);
										disable_cangbien(frmID);
										formid.closest('div.ui-jqdialog').dialogCenter(); 
									}
                                })*/
								$('#edit_grid_ds_quotationqr').click();
                            }"

            BtnPrint="funcPrint"
            ColModel = "[
            {
                fixed: true, name: 'c_baogia_id'
                , index: 'c_baogia_id'
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
                                            $('#grid_ds_quotationqr')[0].triggerToolbar();
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
                                            $('#grid_ds_quotationqr')[0].triggerToolbar();
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
                 , editoptions: { dataUrl: 'Controllers/QuotationQRController.ashx?action=getSelectOption_cb' }
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            FilterToolbar="true"
            Height = "170"
            MultiSelect = "false" 
            ShowAdd ="false"
            ShowEdit ="false"
            ShowDel = "true"
            AddFormOptions=" 
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter();
                    var frmID = '#' + formid.attr('id');
					$('#gia_db', frmID).prop('disabled', false);
					$('#isform', frmID).prop('disabled', false);
                    $('#chkPBGCu', frmID).prop('disabled', false);
                    ShowPBGCu_grid_ds_quotationqr(frmID);
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
                    ShowPBGCu_grid_ds_quotationqr(frmID);
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

                    $('#grid_ds_chitietquotationqr').jqGrid('getGridParam', 'postData').quotationId = ids;
					$('#grid_ds_chitietquotationqr')[0].triggerToolbar();
                }
            "
            runat="server" />
</DIV> 

<DIV class="ui-layout-south ui-widget-content" id="layout-south-quotationqr">
    <div class="ui-layout-center" id="tabs-quotationqr-details">
        <ul>
            <li><a href="#qo-details-1">Danh Sách Sản Phẩm</a></li>
        </ul>
        
        <div id="qo-details-1">
                <uc1:jqGrid  ID="grid_ds_chitietquotationqr" 
                SortName="sp.ma_sanpham" 
                SortOrder="asc"
                UrlFileAction="Controllers/QuotationQRDetailController.ashx" 
                ColNames="['c_chitietbaogia_id', 'Báo Giá', 'TT', 'Trạng Thái', 'STT', 'Mã SP', 'Mã SP', 'HS Code', 'Mã SP Khách',  
				'Giá FOB', 'Số Lượng',  'Đóng Gói', 'Đóng Gói Inner', 'L1', 'W1', 'H1', 'Đóng Gói Outer', 'L2', 'W2', 'H2','Weight', 'V2', 'SL Cont',  'Ghi Chú', 
				'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt Động', 'Độc quyền']"
                FilterToolbar="true"
                RowNumbers="true"
                OndblClickRow = "
				function(rowid) { 
					// $(this).jqGrid('editGridRow', rowid,
					// {
						// afterShowForm:populatePackingQO
						// , afterclickPgButtons:populatePackingQO
						// , errorTextFormat:function(data) { 
							// return 'Lỗi: ' + data.responseText 
						// }
						// , afterSubmit: function(rs, postdata){
							// return showMsgDialog(rs);
						// }
						// , beforeShowForm: function (formid) {
							// $('#ma_sanpham', formid).mask('**-*****-**-**');
							// $('#tr_sothutu', formid).show();
							// $('#tr_md_donggoi_id', formid).show();
							
							// formid.closest('div.ui-jqdialog').dialogCenter();
						// }
						// , afterComplete: function()
						// {
							// var selMasterGrid = $('#grid_ds_quotationqr');
							// var id = $(selMasterGrid).jqGrid('getGridParam', 'selrow');
							// selMasterGrid.trigger('reloadGrid');
							// setTimeout(function(){
									// $(selMasterGrid).jqGrid('setSelection', id);
							// }, 500);
						// }
																		 
					// }); 
					$('#edit_grid_ds_chitietquotationqr').click();
				}"
                ColModel = "[
                {
                    fixed: true, name: 'c_chitietbaogia_id'
                    , index: 'ctbg.c_chitietbaogia_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                    fixed: true, name: 'c_baogia_id'
                    , index: 'bg.c_baogia_id'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , hidden: true
                },
                {
                    fixed: true, name: 'trangthai'
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
					 fixed: true, name: 'trangthai_ctbg'
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
                    fixed: true, name: 'sothutu'
                    , align: 'right'
                    , index: 'ctbg.sothutu'
                    , width: 50
                    , edittype: 'text'
                    , editable: true
                    , editrules: { required:true, number:true, minValue:0 }
                    , editoptions:{ defaultValue:'0' }
                },
                {
                    fixed: true, name: 'sanpham_id'
                    , index: 'sp.md_sanpham_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , editrules: { required:true }
                },
                {
                    fixed: true, name: 'ma_sanpham'
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
										var c_baogia_id = $('#grid_ds_quotationqr').jqGrid('getGridParam', 'selrow');
                                        updatePackingQO_CallBack(ui.item.md_sanpham_id);
                                        updateGiaSanPham_CallBack(c_baogia_id, ui.item.md_sanpham_id) 
                                        return false;
                                  }
                            });
                        } 
                    }
                },
				{
                    fixed: true, name: 'ma_hscode'
                    , index: 'hs.ma_hscode'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
					, hidden: false
					, editrulea:{ edithidden:true }
                },
                {
                    fixed: true, name: 'ma_sanpham_khach'
                    , index: 'ctbg.ma_sanpham_khach'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
					, hidden: true
					, editrulea:{ edithidden:true }
                },
                {
                    fixed: true, name: 'giafob'
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
                    fixed: true, name: 'soluong'
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
                    fixed: true, name: 'md_donggoi_id'
                    , index: 'dg.ten_donggoi'
                    , width: 100
                    , edittype: 'select'
                    , editable: true
                    , editoptions: { 'value':'Chọn một đóng gói'}
					, hidden:true
					, editrules:{ edithidden:true }
                },
                {
                    fixed: true, name: 'sl_inner'
                    , index: 'ctbg.sl_inner'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'l1'
                    , index: 'ctbg.l1'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'w1'
                    , index: 'ctbg.w1'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'h1'
                    , index: 'ctbg.h1'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'sl_outer'
                    , index: 'ctbg.sl_outer'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'l2'
                    , index: 'ctbg.l2'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'w2'
                    , index: 'ctbg.w2'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
                {
                    fixed: true, name: 'h2'
                    , index: 'ctbg.h2'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
					, hidden: true
                },
				{
                    fixed: true, name: 'trongluong'
                    , index: 'sp.trongluong'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'v2'
                    , index: 'ctbg.v2'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
				
                {
                    fixed: true, name: 'sl_cont'
                    , index: 'ctbg.sl_cont'
                    , width: 100
                    , edittype: 'text'
                    , editable:false
                    , align:'right'
                },
                {
                    fixed: true, name: 'ghichu'
                    , index: 'ctbg.ghichu'
                    , width: 100
                    , edittype: 'textarea'
                    , editable:true
                },
                {
                    fixed: true, name: 'ngaytao'
                    , index: 'ctbg.ngaytao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'nguoitao'
                    , index: 'ctbg.nguoitao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'ngaycapnhat'
                    , index: 'ctbg.ngaycapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'nguoicapnhat'
                    , index: 'ctbg.nguoicapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true 
                    , editrules:{ edithidden : true }
                },
                {
                    fixed: true, name: 'mota'
                    , index: 'ctbg.mota'
                    , width: 100
                    , editable:true
                    , edittype: 'textarea'
                    , hidden: true
                },
                {
                    fixed: true, name: 'hoatdong', hidden: true
                    , index: 'ctbg.hoatdong'
                    , width: 100
                    , editable:true
                    , edittype: 'checkbox'
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                },
				{
					name: 'docquyen'
					, index: 'docquyen'
					, fixed:true 
					, width: 100
					, editable:true
					, edittype: 'textarea'
					, hidden: true
					, editrules: { edithidden:true }
				}
                ]"
                GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 97);"
                Height = "150"
                OnSelectRow = "
                    function(ids) {
		                if(ids == null) {
			                ids=0;
				        }
                        let ma_sanpham = $(this).jqGrid('getCell', ids, 'ma_sanpham');
                                
                        $('#product-view-quotationqr').attr('src', '');
                        $('#product-view-quotationqr').attr('src', 'Controllers/API_System.ashx?oper=loadImageProduct&msp=' + ma_sanpham + '&ver=' + uuidv4());
                    }
                "
                AddFormOptions="
                    errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                    }
                    , beforeShowForm: function (formid) {
                        var masterId = $('#grid_ds_quotationqr').jqGrid('getGridParam', 'selrow');
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
                    , beforeSubmit:function(postdata, formid){
                        var masterId = $('#grid_ds_quotationqr').jqGrid('getGridParam', 'selrow');
                        var forId = 'c_baogia_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            return [false,'Hãy chọn một quotation mới có thể tạo chi tiết.!'];
                        }else{
                            postdata.c_baogia_id = masterId;
                            var check_thaydoi = $('#md_donggoi_id > option:selected').attr('other');
							var check_docquyen = $('#docquyen','#FrmGrid_grid_ds_chitietquotationqr').val();
							if(check_thaydoi.toUpperCase() == 'TRUE')
							{
								if(confirm('Lưu ý: Kiểu đóng gói bạn chọn có thể thay đổi giá !') == true) {
									if(check_docquyen != '') {
										if(confirm(check_docquyen) == true) {
											return [true];
										}
										else {
											return [false];
										}
									}
									else {
										return [true];
									}
								}
								else {
									return [false];
								}
							}
							else{
								if(check_docquyen != '') {
									if(confirm(check_docquyen) == true) {
										return [true];
									}
									else {
										return [false];
									}
								}
								else {
									return [true];
								}
							}
                        }
                    }
                    , afterSubmit: function(rs, postdata){
                       return showMsgDialog(rs);
                    }
                    , afterComplete: function()
                    {
                        var selMasterGrid = $('#grid_ds_quotationqr');
                        var id = $(selMasterGrid).jqGrid('getGridParam', 'selrow');
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
                        var masterId = $('#grid_ds_quotationqr').jqGrid('getGridParam', 'selrow');
                        var forId = 'c_baogia_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            return [false,'Hãy chọn một quotation mới có thể tạo chi tiết.!'];
                        }else{
                            postdata.c_baogia_id = masterId;
							var check_thaydoi = $('#md_donggoi_id > option:selected').attr('other');
							var check_docquyen = $('#docquyen','#FrmGrid_grid_ds_chitietquotationqr').val();
							if(check_thaydoi.toUpperCase() == 'TRUE')
							{
								if(confirm('Lưu ý: Kiểu đóng gói bạn chọn có thể thay đổi giá !') == true) {
									if(check_docquyen != '') {
										if(confirm(check_docquyen) == true) {
											return [true];
										}
										else {
											return [false];
										}
									}
									else {
										return [true];
									}
								}
								else {
									return [false];
								}
							}
							else{
								if(check_docquyen != '') {
									if(confirm(check_docquyen) == true) {
										return [true];
									}
									else {
										return [false];
									}
								}
								else {
									return [true];
								}
							}
                        }
                    }
                    , afterComplete: function()
                    {
                        var selMasterGrid = $('#grid_ds_quotationqr');
                        var id = $(selMasterGrid).jqGrid('getGridParam', 'selrow');
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
                        var selMasterGrid = $('#grid_ds_quotationqr');
                        var id = $(selMasterGrid).jqGrid('getGridParam', 'selrow');
                        selMasterGrid.trigger('reloadGrid');
                        setTimeout(function(){
                                $(selMasterGrid).jqGrid('setSelection', id);
                        }, 500);
                    }
	            "
                MultiSelect = "false" 
                ShowAdd ="false"
                ShowEdit ="false"
                ShowDel = "false"
                runat="server" />
        </div>
        
    </div>
    <div class="ui-layout-east" style="text-align:center; background:#f4f0ec">
        <table style="width:100%; height:100%">
            <tr>
                <td>
                    <img id="product-view-quotationqr" class="loadingIMG" style="max-width:100%; margin: 0px auto; min-width: 40px;" src="Controllers/API_System.ashx?oper=loadImageProduct&msp=default" />
                </td>
            </tr>
        </table>
    </div> 

    <script type="text/javascript">
        var funcChuyenQRThanhQO = function () {
            var qoId = $('#grid_ds_quotationqr').jqGrid('getGridParam', 'selrow');
					if (qoId === null) {
						alert('Hãy chọn một báo giá');
					} else {
						var name = 'dialog-ghepbo-qo';
						var title = 'Chuyển thành QO';
						var content = `
							<table style="width:100%">
								<tr>
									<td  colspan=2>
										<label style="font-weight: bold;font-size: 13px;">Bạn có chắc chắn muốn chuyển dòng này thành QO?</label>
										<br>
										<label style="font-weight: bold;font-size: 13px; line-height: 2">Lưu ý:</label>
										<br>
										<label style="font-style:italic; color: blue; line-height: 2">
											Khi đã thực hiện thao tác sẽ không thể hoàn tác.
										</label>
									</td>
								</tr>
							</table>`;

						$('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
						$('#' + name).dialog({
							modal: true, 
							open: function(event, ui) {
								//hide close button.
								$(this).parent().children().children('.ui-dialog-titlebar-close').hide();
							}, 
							buttons: [
							{
								id: "btn-chuyenthanhqo-ok",
								text: "Đồng ý",
                                    click: function () {
                                        $("#btn-chuyenthanhqo-ok").button("disable");
                                        $("#btn-chuyenthanhqo-close").button("disable");
                                        $('#' + name).prepend('<div id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>');
                                        $.post('Controllers/QuotationQRController.ashx?action=chuyenThanhQO&id='+qoId,
                                            { "id": qoId }, function (res) {
                                                res = JSON.parse(res);

                                                if (res.status === 0) {
                                                    $('#' + name).html(`<div style="color:red">${res.mess}</div>`);
                                                }
                                                else {
                                                    $('#' + name).html(`<div style="color:blue">QO: "${res.mess}" đã được tạo thành công.</div>`);
                                                    $('#grid_ds_quotationqr').jqGrid().trigger('reloadGrid');
                                                    $('#grid_ds_chitietquotationqr').jqGrid().trigger('reloadGrid');
                                                }
                                                $("#btn-chuyenthanhqo-close").button("enable");
                                        });
                                }
							},
							{
								id: "btn-chuyenthanhqo-close",
								text: "Thoát",
								click: function() {
									$(this).dialog("destroy").remove();
								}
							}
							]
						});
					}
        };

        $("#grid_ds_quotationqr").jqGrid('navGrid', "#grid_ds_quotationqr-pager").jqGrid('navButtonAdd', "#grid_ds_quotationqr-pager",
        {
            caption: "",
            buttonicon: "ui-icon-battery-3",
            onClickButton: funcChuyenQRThanhQO,
            title: "Chuyển thành QO",
            cursor: "pointer"
        });

        createRightPanel('grid_ds_quotationqr');
        createRightPanel('grid_ds_chitietquotationqr');
    </script>
</DIV>

   