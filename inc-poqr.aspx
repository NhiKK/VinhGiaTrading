<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-poqr.aspx.cs" Inherits="inc_poqr" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>

<script>
    $(function() {
		$('#lay-center-poqr').parent().layout({
            south: {
                size: "50%"
            }
            , center: {
                onresize: function() {
                    var h = $("#lay-center-poqr").height();
                    $("#grid_ds_poqr").setGridHeight(h - 90);
                }
            }
        });
		
		// Layout nho duoi
        $('#layout-south-poqr').layout({
            center: {
                onresize: function() {
                    var o = $("#tabs-donhangqr-details")
                    $("#grid_ds_chitietpoqr").setGridWidth(o.width());

                    $("#grid_ds_chitietpoqr").setGridHeight(o.height() - 97);
                }
            }
        });

        $('#tabs-donhangqr-details').tabs();

       
    });

    function funcPrint() {
        var poId = $('#grid_ds_poqr').jqGrid('getGridParam', 'selarrrow');
        var name = 'dialog-print-po';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
						'<tr>' +
							'<td>' +
								'<input id="printPoDetails" type="radio" name="rdoPrintType" value="printPoDetails" checked="checked" />' +
								'<label for="printPoDetails">Chiết xuất đơn hàng</label>' +
							'</td>' +
							
							
							'<td>' +
								
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
                     text: "Xem",
                     click: function () {
                         var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-po').val();
                         var printURL = "PrintControllers/";
                         let windowSize = "top=0, left=0, width=" + window.outerWidth + ", height=" + window.outerHeight;
                         if (typeof printType != 'undefined') {
                             if (printType == 'printPoDetails') {
                                 if (poId != null) {
                                     printURL += "InDonHang/";
                                     window.open(printURL + "?c_donhang_id=" + poId + "&type=pdf", "Xem Đơn Hàng", windowSize);
                                 } else {
                                     alert('Bạn chưa chọn đơn hàng cần in.!');
                                 }
                             }
                         } else {
                             alert('Hãy chọn cách in.!');
                         }
                     }
                 },
                {
                    id: "btn-print-ok",
                    text: "In",
                    click: function() {
                        var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-po').val();
                        var printURL = "PrintControllers/";
                        var windowSize = "width=700,height=700,scrollbars=yes";
					    if (typeof printType != 'undefined') {
                            if (printType == 'printPoDetails') {
                                if (poId != null) {
                                    printURL += "InDonHang/";
                                    window.open(printURL + "?c_donhang_id=" + poId, "In Chiết Xuất Đơn Hàng", windowSize);
                                } else {
                                    alert('Bạn chưa chọn đơn hàng cần in.!');
                                }
                            }
                        } else {
                            alert('Hãy chọn cách in.!');
                        }
                    }
                },
                {
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
<DIV class="ui-layout-center ui-widget-content" id="lay-center-poqr" style="padding:0;">
<uc1:jqGrid  ID="grid_ds_poqr"
            BtnPrint="funcPrint"
            Caption="Đơn Hàng QR"
            SortName="dh.ngaytao" 
            SortOrder="desc"
            UrlFileAction="Controllers/DonHangQRController.ashx" 
            PostData = "'status' : 'all'"
			MultiSelect="true"
            ColNames="['c_donhangqr_id', '', 'Khách Hàng', 'Số Chứng Từ', 'Customer Order No', 'Cảng Biển'
                , 'Shipment Time', 'Total CBM', 'Amount', 'Discount(%)'
                , 'Paymentterm', 'Thông tin ngân hàng', 'Hoa hồng(%)', 'Người hưởng HH','Ngày Lập', 'Người Lập'
                , 'Shipment Date', 'Ngày điều chỉnh', 'ĐH Mẫu SR', 'ĐH Mẫu TI'
                , 'Tr.Lượng', 'Đồng Tiền', 'Kích Thước', 'Payer', 'PortDischarge'
                , 'Total CBF', 'Cont 20', 'Cont 40', 'Cont 40HC', 'Cont 45HC', 'Cont lẻ' 
				, 'Đã Gửi Mail', 'Make PI'
				, 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật'
                , 'Consingnee', 'Hoạt động', 'Giá đặc biệt'
                , 'Giá FOB phiên bản cũ', 'Phiên bản giá cũ'
                , 'Ghi chú']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { 
                $('#view_grid_ds_poqr').click();
			}"
            ColModel = "[
            {
                name: 'c_donhangqr_id'
                , index: 'c_donhangqr_id'
                , fixed:true , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
                , formoptions: { colpos: 1, rowpos: 1 }
            },
            {
                 name: 'md_trangthai_id'
                 , index: 'md_trangthai_id'
                 , editable: false
                 , fixed:true , width: 100
                 , edittype: 'text'
                 , align:'center'
                 , formatter:'imagestatus'
                 , search : true
                 , stype : 'select'
                 , searchoptions:{sopt:['eq'], value:':[ALL];SOANTHAO:SOẠN THẢO;HIEULUC:HIỆU LỰC;KETTHUC:KẾT THÚC' }
            },
			{
                 name: 'md_doitackinhdoanh_id'
                 , index: 'dtkd.md_doitackinhdoanh_id'
                 , editable: true
                 , fixed:true 
                 , width: 120
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=0' }
                 , formoptions: { label:'Khách Hàng ' + ip_fd(), colpos: 1, rowpos: 4 }
                 , search: true
                 , stype: 'select'
                 , searchoptions:{ sopt:['eq'], dataUrl: 'Controllers/PartnerController.ashx?action=getsearchoption&isncc=0' } 
            },
            {
                 name: 'sochungtu'
                 , index: 'sochungtu'
                 , editable: true
                 , fixed:true 
                 , width: 130
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 2 }
            },
            {
                 name: 'customer_order_no'
                 , index: 'customer_order_no'
                 , editable: true
                 , fixed:true 
                 , width: 50
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 3 }
            },
			{
                 name: 'md_cangbien_id'
                 , index: 'cbien.ten_cangbien'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/QuotationController.ashx?action=getSelectOption_cb' }
                 , formoptions: { label:'Cảng Biển ' + ip_fd(), colpos: 1, rowpos: 6 }
            },
			{
                 name: 'shipmenttime'
                 , index: 'shipmenttime'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', altField: '#shipmentdate', altFormat: 'dd/mm/yy', onSelect: function(){ var date = $(this).datepicker('getDate');  if (date) {  date.setDate(date.getDate() + 7);  $('#shipmentdate').datepicker('setDate', date);  }   }  }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
                 , formoptions: { label:'Shipment Time ' + ip_fd(), colpos: 1, rowpos: 8 }
                 , search: true
                 , stype: 'text'
                 , searchoptions: { 
                    sopt: ['eq', 'ne']
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
                                            $('#grid_ds_poqr')[0].triggerToolbar();
                                        }
                                    }, 100);
                                }
                            }
                        });
                    } 
                 }
            },
			{
                 name: 'totalcbm'
                 , index: 'totalcbm'
                 , editable: false
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , align: 'right'
            },
			{
                 name: 'amount'
                 , index: 'amount'
                 , editable: false
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , align: 'right'
				 , formatter:'number'
                 , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
			{
                 name: 'discount'
                 , index: 'discount'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 7 }
                 , editoptions:{ defaultValue: '0' }
                 , editrules: { required:true, number:true, minValue: 0 }
                 , align: 'right'
            },
			{
                 name: 'md_paymentterm_id'
                 , index: 'pmt.ten_paymentterm'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PaymentTermController.ashx?action=getoption' }
                 , formoptions: { label:'Paymentterm ' + ip_fd(), colpos: 1, rowpos: 11 }
            },
            {
                 name: 'md_nganhang_id'
                 , index: 'ngh.ma_nganhang'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/NganHangController.ashx?action=getoption' }
                 , formoptions: { colpos: 1, rowpos: 12 }
            },
			{
                 name: 'hoahong'
                 , index: 'hoahong'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 18 }
                 , editoptions:{ defaultValue: '0' }
                 , editrules: { required:true, number:true, minValue: 0 }
            },
            {
                 name: 'md_nguoilienhe_id'
                 , index: 'md_nguoilienhe_id'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/NguoiLienHeController.ashx?action=getoptionNullFirst' }
                 , formoptions: { colpos: 1, rowpos: 17 }
				 , hidden: true
				 , editrules: { edithidden:true }
            },
            {
                 name: 'ngaylap'
                 , index: 'ngaylap'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
                 , formoptions: { colpos: 1, rowpos: 5 }
                 , search: true
                 , stype: 'text'
                 , searchoptions: { 
                    sopt: ['eq', 'ne']
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
                                            $('#grid_ds_poqr')[0].triggerToolbar();
                                        }
                                    }, 50);
                                }
                            }
                        });
                    } 
                 }
            },
            {
                 name: 'nguoilap'
                 , index: 'dh.nguoilap'
                 , editable: false
                 , fixed:true , width: 50
                 , edittype: 'text'
            },
            {
                 name: 'shipmentdate'
                 , index: 'shipmentdate'
                 , editable: true
                 , hidden: true
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); if (currentTime) {  currentTime.setDate(currentTime.getDate() + 7) }; var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
                 , formoptions: { colpos: 1, rowpos: 9 }
                 , search: true
                 , stype: 'text'
                 , searchoptions: { 
                    sopt: ['eq', 'ne']
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
                                            $('#grid_ds_poqr')[0].triggerToolbar();
                                        }
                                    }, 100);
                                }
                            }
                        });
                    } 
                 }
            },
            {
                 name: 'ngaydieuchinh'
                 , index: 'ngaydieuchinh'
                 , editable: true
                 , hidden:true
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 10 }
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); if (currentTime) {  currentTime.setDate(currentTime.getDate() + 7) }; var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            
			{
                 name: 'donhang_mau'
                 , index: 'donhang_mau'
                 , editable: true
                 , fixed:true 
                 , width: 50
                 , edittype: 'checkbox'
                 , align:'center'
                 , formatter: 'checkbox'
				 , editoptions:{ value:'True:False', defaultValue: 'False' }
                 , formoptions: { colpos: 1, rowpos: 24 }
            },
            {
                 name: 'isdonhangtmp'   
                 , align: 'center'
                 , index: 'isdonhangtmp'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'checkbox'
                 , formatter: 'checkbox'
				 , hidden: true
                 , editoptions:{ value:'True:False', defaultValue: 'False' }
                 , formoptions: { colpos: 1, rowpos: 25 }
            },
			{
                 name: 'md_trongluong_id'
                 , index: 'tl.ten_trongluong'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/TrongLuongController.ashx?action=getoption' }
                 , formoptions: { colpos: 1, rowpos: 13 }
                 , hidden: true
                 , editrules:{ edithidden: true }
                 , align: 'right'
            },
            {
                 name: 'md_dongtien_id'
                 , index: 'dtien.ma_iso'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption' }
                 , formoptions: { colpos: 1, rowpos: 14 }
                 , hidden: true
                 , editrules:{ edithidden: true }
                 , align: 'center'
            },
            {
                 name: 'md_kichthuoc_id'
                 , index: 'kt.ten_kichthuoc'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/KichThuocController.ashx?action=getoption' }
                 , formoptions: { colpos: 1, rowpos: 15 }
                 , hidden: true
                 , editrules:{ edithidden: true }
                 , align: 'center'
            },
            {
                 name: 'payer'
                 , index: 'payer'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 16 }
				 , hidden: true
				 , editrules: { edithidden: true }
            },
            {
                 name: 'portdischarge'
                 , index: 'portdischarge'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 19 }
				 , hidden: true
				 , editrules: { edithidden: true }
            },
            {
                 name: 'totalcbf'
                 , index: 'totalcbf'
                 , editable: false
                 , fixed:true , width: 50
                 , edittype: 'text'
                 , align: 'right'
				 , hidden: true
            },
            {
                 fixed:true
                 , name: 'cont20'
                 , index: 'cont20'
                 , editable: true
                 , width: 50
                 , edittype: 'text'
                 , align: 'right'
                 , editrules:{ required:true, number:true }
                 , editoptions:{ defaultValue: '0' }
                 , formoptions: { label:'Cont 20 ' + ip_fd(),colpos: 2, rowpos: 3 }
				 , hidden: true
				 , editrules: { edithidden: true }
            },
            {
                 fixed:true
                 , name: 'cont40'
                 , index: 'cont40'
                 , editable: true
                 , width: 50
                 , edittype: 'text'
                 , align: 'right'
                 , editrules:{ required:true, number:true }
                 , editoptions:{ defaultValue: '0' }
                 , formoptions: { label:'Cont 40 ' + ip_fd(),colpos: 2, rowpos: 4 }
				 , hidden: true
				 , editrules: { edithidden: true }
            },
            {
                 fixed:true
                 , name: 'cont40hc'
                 , index: 'cont40hc'
                 , editable: true
                 , width: 50
                 , edittype: 'text'
                 , align: 'right'
                 , editrules:{ required:true, number:true }
                 , editoptions:{ defaultValue: '0' }
                 , formoptions: { label:'Cont 40hc ' + ip_fd(),colpos: 2, rowpos: 5 }
				 , hidden: true
				 , editrules: { edithidden: true }
            },
			{
                 fixed:true
                 , name: 'cont45'
                 , index: 'cont45'
                 , editable: true
                 , width: 50
                 , edittype: 'text'
                 , align: 'right'
                 , editrules:{ required:true, number:true }
                 , editoptions:{ defaultValue: '0' }
                 , formoptions: { label:'Cont 45 ' + ip_fd(),colpos: 2, rowpos: 6}
				 , hidden: true
				 , editrules: { edithidden: true }
            },
            {
                 fixed:true
                 , name: 'contle'
                 , index: 'contle'
                 , editable: true
                 , width: 50
                 , edittype: 'text'
                 , align: 'right'
                 , editrules:{ required:true, number:true }
                 , editoptions:{ defaultValue: '0' }
                 , formoptions: { label:'Cont lẻ ' + ip_fd(),colpos: 2, rowpos: 7 }
				 , hidden: true
				 , editrules: { edithidden: true }
            },
            {
                 name: 'dagui_mail'   
                 , index: 'dagui_mail'
                 , align: 'center'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'checkbox'
                 , formatter: 'checkbox'
                 , editoptions:{ value:'True:False', defaultValue: 'False' }
                 , formoptions: { colpos: 1, rowpos: 22 }
				 , hidden: true
				 , editrules: { edithidden: true }
            },
            {
                 name: 'ismakepi'   
                 , align: 'center'
                 , index: 'ismakepi'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'checkbox'
                 , formatter: 'checkbox'
                 , editoptions:{ value:'True:False', defaultValue: 'False', readonly: 'readonly' }
                 , formoptions: { colpos: 1, rowpos: 23 }
                 , hidden: true
            },
            {
                name: 'ngaytao'
                , index: 'ngaytao'
                , fixed:true , width: 50
                , editable:false
                , edittype: 'text'
                , hidden: true
                
            },
            {
                name: 'nguoitao'
                , index: 'nguoitao'
                , fixed:true , width: 50
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , fixed:true , width: 50
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , fixed:true , width: 50
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                name: 'mota'
                , index: 'mota'
                , fixed:true , width: 50
                , editable:true
                , edittype: 'text'
                , hidden: true
				, edirules:{ edithidden: true }
                , formoptions: { colpos: 2, rowpos: 8 }
            },
            {
                name: 'hoatdong'
                , hidden: true
                , index: 'hoatdong'
                , width: 30
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , formatter: 'checkbox'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formoptions: { colpos: 1, rowpos: 27 }
            },		
			{
                 name: 'gia_db'   
                 , align: 'center'
                 , index: 'gia_db'
                 , editable: true
                 , fixed:true , width: 50
                 , edittype: 'checkbox'
                 , formatter: 'checkbox'
                 , editoptions:{ value:'True:False', defaultValue: 'False' }
                 , formoptions: { colpos: 1, rowpos: 28 }
            },
            {
                fixed: true
                , name: 'chkPBGCu'
                , hidden: true
                , editrules:{
                    edithidden:true
                }
                , index: 'dh.chkPBGCu'
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
                , index: 'dh.phienbangiacu'
                , width: 100
                , editable:true
                , edittype: 'select'
                , align: 'center'
                , editoptions: {style:'width:98%', dataUrl: 'Controllers/SelectOption/PhienBanGiaCu.ashx?action=PhienBanGiaCu' }
                , formoptions:{label:'Phiên bản giá cũ ' + ip_fd()}
            },
			{
                fixed: true, name: 'ghichu'
                , index: 'ghichu'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: false 
                , editrules:{ edithidden : true }
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()-90);"
            FilterToolbar = "true"
            Height = "420"
            ShowAdd ="false"
            ShowEdit ="false"
            ShowDel = "true"
            OnSelectRow = "function(ids) {
				var chk_box = $('#jqg_grid_ds_poqr_' + ids);
				setTimeout(function() {
					if(!chk_box.prop('checked')) { ids = null; }
					if(ids == null) {
						ids=0;
					} else {
						$.ajax({
							url: 'Controllers/DonHangQRController.ashx?action=getpoinformation&poId='+ids,
							beforeSend:function(){
								$('#grid_ds_poqr').jqGrid('setCaption','đang tải...');
							},
							success: function(data){
								$('#grid_ds_poqr').jqGrid('setCaption', data);
							}
						});
					}
                    $('#grid_ds_chitietpoqr').jqGrid('getGridParam', 'postData').poId = ids;
					$('#grid_ds_chitietpoqr')[0].triggerToolbar();
				},10);
			}"
		    AddFormOptions=" 
		        width: 700
                , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter();
                    var frmID = '#' + formid.attr('id');
					$('#gia_db', frmID).prop('disabled', false);
					$('#donhang_mau', frmID).prop('disabled', false);
					$('#chkPBGCu', frmID).prop('disabled', false);
                    ShowPBGCu_grid_ds_poqr(frmID);
                    disableSelOption('#md_cangbien_id', frmID, false);
                    disableSelOption('#phienbangiacu', frmID, false);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                "
            EditFormOptions ="
		        width: 700
                , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter();
					var frmID = '#' + formid.attr('id');
                    $('#gia_db', frmID).prop('disabled', true);
					$('#donhang_mau', frmID).prop('disabled', true);
					$('#chkPBGCu', frmID).prop('disabled', true);
                    ShowPBGCu_grid_ds_poqr(frmID);
                    disableSelOption('#md_cangbien_id', frmID, true);
                    disableSelOption('#phienbangiacu', frmID, true);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                "
            ViewFormOptions="
                width: 700,
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
                afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
            }"
            runat="server" />
			
</DIV>

<DIV class="ui-layout-south ui-widget-content" id="layout-south-poqr"> 
	<div class="ui-layout-center" id="tabs-donhangqr-details">
        <ul>
            <li><a href="#donhangqr-details">Các dòng P/O</a></li>
        </ul>
        <div id="donhangqr-details">
            <uc1:jqGrid  ID="grid_ds_chitietpoqr" 
            SortName="ddh.sothutu" 
            SortOrder="desc"
            UrlFileAction="Controllers/DongDonHangQRController.ashx" 
            ColNames="['c_dongdonhangqr_id', 'Đơn Hàng','TT','Trạng Thái', 'STT', 'Mã SP'
                    , 'Mã SP', 'Mã SP Khách'
                    ,  'Giá', 'Số Lượng', 'Đóng Gói', 'Đóng Gói Inner'
                    , 'L1', 'W1', 'H1', 'Đóng Gói Outer'
                    , 'L2', 'W2', 'H2', 'SL Cont', 'V2', 'Nhà cung ứng'
                    , 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật'
                    , 'Người Cập Nhật', 'Mô tả', 'Hoạt động', 'Độc quyền']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { 
								$('#view_grid_ds_chitietpoqr').click();
							}"
            ColModel = "[
            {
                name: 'c_dongdonhangqr_id'
                , index: 'c_dongdonhangqr_id'
                , fixed:true , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                name: 'c_donhangqr_id'
                , index: 'dh.c_donhangqr_id'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
            },
            {
                 fixed: true, name: 'trangthai'
                 , index: 'sp.trangthai'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , align:'center'
                 , formatter:'imagestatus'
                 , search : true
                 , stype : 'select'
                 , searchoptions:{ value:':[ALL];MOI:MỚI;DHD:ĐANG HOẠT ĐỘNG;NHD:NGƯNG HOẠT ĐỘNG'}
            },
			{
                 fixed: true, name: 'trangthai_ddh'
                 , index: 'ddh.trangthai'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , align:'center'
                 , formatter:'imagestatus'
                 , search : true
                 , stype : 'select'
                 , searchoptions:{ value:':[ALL];DGDG:Đổi giá theo đóng gói;BT:Giá bình thường;DQ:HH Độc quyền'}
            },
            {
                name: 'sothutu'
                , index: 'ddh.sothutu'
                , fixed:true , width: 50
                , edittype: 'text'
                , editable: true
                , editrules: { required:true, number: true }
                , editoptions:{ defaultValue: '0' }
                , align: 'right'
            },
            {
                name: 'sanpham_id'
                , index: 'sp.sanpham_id'
                , fixed:true , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
            },
            {
                name: 'ma_sanpham'
                , index: 'sp.ma_sanpham'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:true
                , editoptions: { 
                    dataInit : function (elem) { 
                        $(elem).combogrid({
                            searchIcon: true,
                            width: '480px',
                            url: 'Controllers/ProductController.ashx?action=getcombogrid&soanthao=1',
                            colModel: [
                                { 'columnName': 'md_sanpham_id', 'width': '0', 'label': 'ID', hidden:true }
                              , { 'columnName': 'ma_sanpham', 'width': '50', 'label': 'Mã SP', 'align':'left'}
                              , { 'columnName': 'mota_tiengviet', 'width': '50', 'label': 'Mô Tả TV' , 'align':'left'}],
                              select: function(event, ui) {
                                    $(elem).val(ui.item.ma_sanpham);
                                    $('#sanpham_id').val(ui.item.md_sanpham_id);
                                    var c_donhangqr_id = $('#grid_ds_poqr').jqGrid('getGridParam', 'selrow');
                                    
                                    updateGiaSanPhamPO_CallBack(c_donhangqr_id, ui.item.md_sanpham_id)                    
                                    updatePackingPO_CallBack(ui.item.md_sanpham_id);
                                    updateNhaCungUngPO_CallBack(ui.item.md_sanpham_id);
									
                                    return false;
                              }
                        });
                    } 
                }
            },
            {
                name: 'ma_sanpham_khach'
                , index: 'ddh.ma_sanpham_khach'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:true
            },
			{
				name: 'giafob'
				, index: 'ddh.giafob'
				, fixed:true , width: 100
				, edittype: 'text'
				, editable: true
				, editrules: { required:true, number: true }
				, editoptions:{ defaultValue: '0' }
				, align: 'right'
				, formatter:'currency'
				, formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
			},
            {
                name: 'soluong'
                , index: 'ddh.soluong'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:true
                , editoptions:{ defaultValue: '1' }
                , align: 'right'
                , formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
            },
            
            {
                name: 'md_donggoi_id'
                , index: 'dg.ten_donggoi'
                , fixed:true , width: 100
                , edittype: 'select'
                , editable: true
                , editoptions: { 'value' : 'Chọn đóng gói' }
				, hidden:true 
				, editrules: { edithidden:true }
            },
            {
                name: 'sl_inner'
                , index: 'ddh.sl_inner'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
            },
            {
                name: 'l1'
                , index: 'ddh.l1'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
				, hidden:true
            },
            {
                name: 'w1'
                , index: 'ddh.w1'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
				, hidden:true
            },
            {
                name: 'h1'
                , index: 'ddh.h1'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
				, hidden:true
            },
            {
                name: 'sl_outer'
                , index: 'ddh.sl_outer'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
            },
            {
                name: 'l2'
                , index: 'ddh.l2'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
				, hidden:true
            },
            {
                name: 'w2'
                , index: 'ddh.w2'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
				, hidden:true
            },
            {
                name: 'h2'
                , index: 'ddh.h2'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
				, hidden:true
            },
			{
                name: 'sl_cont'
                , index: 'ddh.sl_cont'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
                , formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
            },
            {
                name: 'v2'
                , index: 'ddh.v2'
                , fixed:true , width: 100
                , edittype: 'text'
                , editable:false
                , align: 'right'
            },
			{
                 name: 'nhacungungid'
                 , index: 'dtkd.ma_dtkd'
                 , editable: true
                 , fixed:true 
                 , width: 120
                 , edittype: 'select'
                 , editoptions: { 'value' : 'Chọn nhà cung ứng'}
            },
            
            {
                name: 'ngaytao'
                , index: 'ddh.ngaytao'
                , fixed:true , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                name: 'nguoitao'
                , index: 'ddh.nguoitao'
                , fixed:true , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                name: 'ngaycapnhat'
                , index: 'ddh.ngaycapnhat'
                , fixed:true , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                name: 'nguoicapnhat'
                , index: 'ddh.nguoicapnhat'
                , fixed:true , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                name: 'mota'
                , index: 'ddh.mota'
                , fixed:true , width: 100
                , editable:true
                , edittype: 'textarea'
				, hidden: true
				, editrules: { edithidden:true }
            },
            {
                name: 'hoatdong', hidden: true
                , index: 'ddh.hoatdong'
                , width: 30
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            },
			{
                name: 'docquyen'
                , index: 'ddh.docquyen'
                , fixed:true , width: 100
                , editable:true
                , edittype: 'textarea'
				, hidden: false
				, editrules: { edithidden:true }
            }
            ]"
            GridComplete = "let o = $('#tabs-donhangqr-details'); $(this).setGridHeight($(o).height() - 97);$(this).setGridWidth($(o).width())"
            FilterToolbar="true"
            Height = "150"
            MultiSelect = "false" 
            ShowAdd ="false"
            ShowEdit ="false"
            ShowDel = "false"
			OnSelectRow = "
				function(ids) {
					let ma_sanpham = $('#grid_ds_chitietpoqr').jqGrid('getCell', ids, 'ma_sanpham') + '';
                    $('#product-view-poqr').attr('src', '');
                    $('#product-view-poqr').attr('src', 'Controllers/API_System.ashx?oper=loadImageProduct&msp=' + ma_sanpham + '&ver=' + uuidv4());
				}
			"
            AddFormOptions=" 
                width: 450,
                beforeShowForm: function (formid) {
                        var masterId = $('#grid_ds_poqr').jqGrid('getGridParam', 'selrow');
                        var forId = 'c_donhangqr_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một đơn hàng mới có thể tạo chi tiết.!');
                        }else{
                            $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
							
							$('#ma_sanpham', formid).mask('99-*****-**-**');
                            $('#ma_sanpham', formid).prop('disabled', false);
                            $('#tr_giafob', formid).show();
                            $('#tr_sothutu', formid).hide();
                            $('#tr_nhacungungid', formid).show();
                            $('#tr_md_donggoi_id', formid).show();
                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                        }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_poqr').jqGrid('getGridParam', 'selrow');
                    var forId = 'c_donhangqr_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một đơn hàng mới có thể tạo chi tiết.!!'];
                    }else{
                        postdata.c_donhangqr_id = masterId;
						var check_thaydoi = $('#md_donggoi_id > option:selected').attr('other');
						var check_docquyen = $('#docquyen','#FrmGrid_grid_ds_chitietpoqr').val();
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
                "
            EditFormOptions ="
                width: 450,
                beforeShowForm: function (formid) {
                    $('#ma_sanpham', formid).mask('**-*****-**-**');
                    $('#ma_sanpham', formid).prop('disabled', true);
                    $('#tr_giafob', formid).show();
                    $('#tr_sothutu', formid).show();
                    $('#tr_md_donggoi_id', formid).show();
                    $('#tr_nhacungungid', formid).show();
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
				, beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_poqr').jqGrid('getGridParam', 'selrow');
                    var forId = 'c_donhangqr_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một đơn hàng có thể sửa chi tiết.!!'];
                    }else{
                        postdata.c_donhangqr_id = masterId;
						var check_thaydoi = $('#md_donggoi_id > option:selected').attr('other');
						var check_docquyen = $('#docquyen','#FrmGrid_grid_ds_chitietpoqr').val();
						if(check_thaydoi.toUpperCase() == 'TRUE')
						{
							if(confirm('Mã hàng đã thay đổi giá theo đóng gói') == true) {
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
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
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
                afterSubmit: function(rs, postdata) { 
                    return showMsgDialog(rs); 
            }"
            runat="server" />
        </div>
	</div>
	<div class="ui-layout-east" style="text-align:center; background:#f4f0ec">
		<table style="width:100%; height:100%">
			<tr>
				<td>
					<img id="product-view-poqr" class="loadingIMG" style="max-width:100%; margin: 0px auto; min-width: 40px;" src="Controllers/API_System.ashx?oper=loadImageProduct&msp=default" />
				</td>
			</tr>
		</table>
	</div> 
	<script>
		var hide_dtkd_philqpo = function(frm) {
			$('#tr_md_doitackinhdoanh_id', frm).hide();
			$('#tr_phitang_kh', frm).hide();
			$('#tr_diengiai_kh', frm).hide();
			$('#tbl_dsncc_check_ptr', frm).hide();
			$('#tbl_dsncc_check_chhet', frm).hide();
		}
		
		var hide_dtkd_philqpo1 = function(frm) {
			$('#tr_md_doitackinhdoanh_id', frm).hide();
			$('#tr_phitang_kh', frm).show();
			$('#tr_diengiai_kh', frm).show();
			$('#tbl_dsncc_check_ptr', frm).hide();
			$('#tbl_dsncc_check_chhet', frm).hide();
		}
		
		var show_dtkd_philqpo = function(frm) {
			$('#tr_phitang_kh', frm).show();
			$('#tr_diengiai_kh', frm).show();
			$('#tbl_dsncc_check_ptr', frm).hide();
		}
		
		var checkbox_khoanphilq = function() {
			var frm = '#FrmGrid_grid_khoanphi';
			$('#check_chhet', frm).off('change');
			$('#check_chhet', frm).change(function() {
				var chk_ptr = $('#check_ptr', frm);
				var chk_ptrCBM = $('#check_ptrCBM', frm);
				if($(this).prop('checked')) {
					chk_ptr.prop('checked', false);
					chk_ptrCBM.prop('checked', false);
					hide_dtkd_philqpo1(frm);
					$('#tbl_dsncc_check_chhet', frm).show();		
				}
				else if(chk_ptr.prop('checked') == false & chk_ptrCBM.prop('checked') == false) {
					hide_dtkd_philqpo(frm);
				}
			});
			
			$('#check_ptr', frm).off('change');
			$('#check_ptr', frm).change(function() {
				var chk_het = $('#check_chhet', frm);
				var chk_ptrCBM = $('#check_ptrCBM', frm);
				if($(this).prop('checked')) {
					chk_het.prop('checked', false);
					chk_ptrCBM.prop('checked', false);
					hide_dtkd_philqpo1(frm);
					$('#tbl_dsncc_check_ptr', frm).show();
				}
				else if(chk_het.prop('checked') == false & chk_ptrCBM.prop('checked') == false) {
					hide_dtkd_philqpo(frm);
				}
			});
			
			$('#check_ptrCBM', frm).off('change');
			$('#check_ptrCBM', frm).change(function() {
				var chk_ptr = $('#check_ptr', frm);
				var chk_het = $('#check_chhet', frm);
				if($(this).prop('checked')) {
					chk_ptr.prop('checked', false);
					chk_het.prop('checked', false);
					hide_dtkd_philqpo1(frm);
				}
				else if(chk_ptr.prop('checked') == false & chk_het.prop('checked') == false) {
					hide_dtkd_philqpo(frm);
				}
			});
			
			$('#tracho_ncc', frm).off('change');
			$('#tracho_ncc', frm).change(function() {
				var chk_ptr = $('#check_ptr', frm);
				var chk_het = $('#check_chhet', frm);
				var chk_ptrCBM = $('#check_ptrCBM', frm);
				if($(this).prop('checked')) {
					if(chk_het.prop('checked')) {
						hide_dtkd_philqpo1();
					}
					else if(chk_ptr.prop('checked') | chk_ptrCBM.prop('checked')) {
						hide_dtkd_philqpo1();
					}
					else {
						hide_dtkd_philqpo();
					}
					$('#tr_header_ncc', frm).show();
					$('#tr_check_chhet', frm).show();
					$('#tr_check_ptr', frm).show();
					$('#tr_check_ptrCBM', frm).show();
					$('.row_detail', frm).show();
					//$('#editmodgrid_khoanphi').css('width', '410px');
				}
				else {
					$('#tr_header_ncc', frm).hide();
					$('#tr_phitang_kh', frm).hide();
					$('#tr_diengiai_kh', frm).hide();
					$('#tr_check_chhet', frm).hide();
					$('#tr_check_ptr', frm).hide();
					$('#tr_check_ptrCBM', frm).hide();
					$('#tr_header_ncc', frm).hide();
					$('#check_chhet', frm).prop( 'checked', false );
					$('#check_ptrCBM', frm).prop( 'checked', false );
					$('#check_ptr', frm).prop( 'checked', false );
					$('.row_detail', frm).hide();
					
					//$('#editmodgrid_khoanphi').css('width', '300px');
				}
			});
			
			$('#phitang', frm).off('change');
			$('#phitang', frm).change(function() {
				var phitang_kh = $('#phitang_kh', frm);
				phitang_kh.prop('checked', $(this).prop('checked'));
			});
		};
		
		var header_phidongpo = function() {
			var frm = '#FrmGrid_grid_khoanphi';
			$('#tr_header_kh', frm).show();
			$('#tr_header_kh .CaptionTD', frm).attr('colspan', 2).css({ 'font-size':'20px', 'background-color':'#FFF' });
			$('#tr_header_kh .DataTD', frm).remove();
			$('#tr_header_ncc', frm).show();
			$('#tr_header_ncc .CaptionTD', frm).attr('colspan', 2).css({'font-size':'20px', 'border-top':'6px solid #383838', 'background-color':'#FFF'});
			$('#tr_header_ncc .DataTD', frm).remove();
			
			$('#tr_dsncc_check_ptr', frm).hide();
			$('#tr_dsncc_check_chhet', frm).hide();
			$('#tr_md_doitackinhdoanh_id', frm).hide();
			
			$('#check_ptrCBM', frm).change(); 
			$('#check_chhet', frm).change(); 
			$('#tracho_ncc', frm).change();
			
			show_selectoption_dsphi();
			show_congthuc_giamua();
			show_chuyenhetNCC();
			//'<tr rowpos="3" class="FormData"><td colspan=2></td>Khách hàng</tr>'.insertBefore($('#tr_phi'));
		};
		
		var show_selectoption_dsphi = function() {
			var frm = '#FrmGrid_grid_khoanphi';
			var myVar = setInterval(function() {
				var options = $('#md_doitackinhdoanh_id',frm).children('option');
				if(options.length > 0) {
					clearInterval(myVar);
					var c_donhangqr_id = $('#grid_ds_poqr').jqGrid('getGridParam', 'selrow');
					var allow = '';
					var txt_sel = $('#md_doitackinhdoanh_id',frm).children('option:selected').text();
					$.getJSON('Controllers/DonHangQRController.ashx?action=get_dsncc&c_donhangqr_id=' + c_donhangqr_id, function(result) {
						for(var i in result) {
							var row = result[i];
							allow += row['ma_dtkd'] + ',';
						}
						allow = allow.split(',');
						var ii = 0;
						options.each(function() {
							if(allow.indexOf($(this).text()) > -1) {
								$(this).show();
								if(allow.indexOf(txt_sel) <= -1) {
									if(ii == 0) { $(this).prop('selected', true); }
									ii++;
								}
							}
							else {
								$(this).hide();
							}
						});
					});
				}
			}, 1000);
		}
		var show_chuyenhetNCC = function() {
			show_congthuc_giamua('chncc');
		};
		
		var show_congthuc_giamua = function(chncc) {
			var frm = '#FrmGrid_grid_khoanphi';
			var ava_chk = 'check_ptr';
			if(chncc != null & chncc != '') {
				ava_chk = 'check_chhet';
			}
			var dsncc_check_ptr = $('#dsncc_'+ava_chk, frm);
			
			var c_donhangqr_id = $('#grid_ds_poqr').jqGrid('getGridParam', 'selrow');
			var tbl_edit_dsncc = JSON.parse('[{"id":"","ma_dtkd":"","tien":"0"}]');
			try {
				tbl_edit_dsncc = JSON.parse(dsncc_check_ptr.val());
			}
			catch(r) {
				//alert(r);
			}
			
			$('#tbl_dsncc_'+ava_chk, frm).remove();
			$.getJSON('Controllers/DonHangQRController.ashx?action=get_dsncc&c_donhangqr_id=' + c_donhangqr_id, function(result) {
				var tbl = '<tr id="tbl_dsncc_'+ ava_chk +'"><td style="padding-top: 6px;" colspan="2"><table>';
				for(var i in result) {
					var row = result[i];
					tbl += 
					`<tr class="row_detail">
						<td style="display:none">`+ row['id'] +`</td>
						<td style="color: blue">`+ row['ma_dtkd'] +`</td>
						<td style="width:20px"></td>
						<td><input type="text" value="`  + row['tien'] +`"/></td>
					</tr>`;
				}
				tbl += '</table></tr>';
				
				$(tbl).insertAfter(dsncc_check_ptr.parent().parent());
				var chk_ptr = $('#'+ava_chk, frm);
				if(chk_ptr.prop('checked') != true) {
					$('#tbl_dsncc_' + ava_chk, frm).hide();
				}
				
				for(var i in tbl_edit_dsncc) {
					var row = tbl_edit_dsncc[i];
					$('#tbl_dsncc_'+ ava_chk, frm).children('td').find('.row_detail').each(function() {
						var id = $(this).children('td:eq(0)').html();
						if(row['id'] == id) {
							$(this).children('td:eq(3)').find('input').val(row['tien']);
						}
					});
				}
			});
		}
		
		var get_dsncc_check_chhet = function() {
			return get_dsncc_check_ptr('chncc');
		};
		var get_dsncc_check_ptr = function(chncc) {
			var frm = '#FrmGrid_grid_khoanphi';
			var ava_chk = 'dsncc_check_ptr';
			if(chncc != null & chncc != '') {
				ava_chk = 'dsncc_check_chhet';
			}
	
			var dsncc_check_ptr = $('#'+ava_chk, frm);
			var check_ptr = $('#check_ptr', frm);
			var check_chhet = $('#check_chhet', frm);
			var phi = $('#phi', frm);
			var result = '', tongtien = 0, tongphi = 0;
			try { tongphi = Number(phi.val()); } catch(r) { }
			$('#tbl_'+ava_chk, frm).children('td').find('.row_detail').each(function() {
				var tien = $(this).children('td:eq(3)').find('input').val();
				result += "{";
				result += '"id":"' + $(this).children('td:eq(0)').html() + '",';
				result += '"ma_dtkd":"' + $(this).children('td:eq(1)').html() + '",';
				result += '"tien":"' + tien + '"';
				result += "},";
				
				tongtien += Number(tien);
				
			});
			if(result != '') {
				result = '[' + result.substring(0, result.length - 1) + ']';
			}
			
			var msg = '';
			if(check_ptr.prop('checked') | check_chhet.prop('checked')) {
				if(tongtien <= tongphi) {
					dsncc_check_ptr.val(result);
					msg = dsncc_check_ptr.val();
				}
				else {
					msg = 'error';
				}
			}
			else {
				dsncc_check_ptr.val('');
				msg = dsncc_check_ptr.val();
			}
			return msg;
		}
	</script>
	
	 <script type="text/javascript">
        let funcChuyenQRThanhPO = function () {
            let poId = $('#grid_ds_poqr').jqGrid('getGridParam', 'selrow');
					if (poId === null) {
						alert('Hãy chọn một đơn hàng');
					} else {
						let name = 'dialog-chuyenthanh-po';
						let title = 'Chuyển thành PO';
						let content = `
							<table style="width:100%">
								<tr>
									<td  colspan=2>
										<label style="font-weight: bold;font-size: 13px;">Bạn có chắc chắn muốn chuyển dòng này thành PO?</label>
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
                                        $.post('Controllers/DonHangQRController.ashx?action=chuyenThanhPO&id='+poId,
                                            { "id": poId }, function (res) {
                                                res = JSON.parse(res);

                                                if (res.status === 0) {
                                                    $('#' + name).html(`<div style="color:red">${res.mess}</div>`);
                                                }
                                                else {
                                                    $('#' + name).html(`<div style="color:blue">QO: "${res.mess}" đã được tạo thành công.</div>`);
                                                    $('#grid_ds_poqr').jqGrid().trigger('reloadGrid');
                                                    $('#grid_ds_chitietpoqr').jqGrid().trigger('reloadGrid');
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

        $("#grid_ds_poqr").jqGrid('navGrid', "#grid_ds_poqr-pager").jqGrid('navButtonAdd', "#grid_ds_poqr-pager",
            {
                caption: "",
                buttonicon: "ui-icon-battery-3",
                onClickButton: funcChuyenQRThanhPO,
                title: "Chuyển thành PO",
                cursor: "pointer"
            });

        createRightPanel('grid_ds_poqr');
        createRightPanel('grid_ds_chitietpoqr'); 
    </script>
</DIV>
    