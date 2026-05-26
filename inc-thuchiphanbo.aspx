<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-thuchiphanbo.aspx.cs" Inherits="inc_thuchiphanbo" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function() {
        $('#layout-center-phaithuphanbo').parent().layout({
            south: {
                size: "50%"
                , onresize_end: function() {
                    var h = $("#layout-south-phaithuphanbo").height();
                    $("#grid_ds_chitietphaithuphanbo").setGridHeight(h - 95);
                }
            }
            , center: {
                onresize_end: function() {
                var h = $("#layout-center-phaithuphanbo").height();
                $("#grid_ds_phaithuphanbo").setGridHeight(h - 90);
                }
            }
        });

        $('#tabs-phaithuphanbo-details').tabs();
    });


        
       function phanBoCocInvoice() {
        var id = $("#grid_ds_chitietphaithuphanbo").jqGrid('getGridParam', 'selrow');
        if (id != null) {
            $.ajax({
                url: "inc-phanbococinvoice.aspx?chitietphaithuid=" + id,
                type: "POST",
                data: {},
                error: function(rs) { },
                success: function(rs) {
                    var div = $("<div></div>");
                    div.appendTo($(document.body));
                    div.html(rs);
                    div.dialog({
                        title: "Phân bổ tiền cọc cho Invoice",
                        resizable: false,
                        modal: true,
                        width: 530, 
                        close: function(event, ui) { $(this).dialog('destroy').remove(); }
                    });
                }
            });
        } else {
            alert("Chưa chọn dòng cần phân bổ!");
        }
    }
</script>
<div class="ui-layout-south ui-widget-content" id="layout-south-phaithuphanbo">
    <div class="ui-layout-center" id="tabs-phaithuphanbo-details">
        <ul>
            <li><a href="#phaithuphanbo-details-1">Chi tiết các khoản</a></li>
        </ul>
        <div id="phaithuphanbo-details-1">
                <uc1:jqGrid  ID="grid_ds_chitietphaithuphanbo" 
            FilterToolbar="true"
            SortName="cttc.c_chitietthuchi_id" 
            UrlFileAction="Controllers/ChiTietThuChiPhanBoController.ashx" 
            ColNames="['c_chitietthuchi_id', 'Thu Chi', 'TK Nợ', 'TK Có', 'Số Tiền', 'Số Tiền Bao Gồm Phí', 'Quy Đổi', 'Diễn Giải',  'P/O', 'Packing list/Invoice', 'Là tiền cọc', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            FuncPhanBoCocInv="phanBoCocInvoice"
            ColModel = "[
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 95);  $(this).setGridWidth($(o).width()); "
            Height = "150"
            MultiSelect = "false" 
            ShowAdd ="false"
            ShowEdit ="false"
            ShowDel = "false"
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
            EditFormOptions ="
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

<div class="ui-layout-center ui-widget-content" id="layout-center-phaithuphanbo">
    <uc1:jqGrid  ID="grid_ds_phaithuphanbo"
            Caption="Packing List - Invoice" 
            SortName="pi.ngaytao" 
            UrlFileAction="Controllers/PackingInvoiceController.ashx" 
            ColNames="['c_packinginvoice_id', 'TT' , 'Số Packing List', 'Số Invoice', 'Đơn Hàng'
                     , 'Người Lập', 'Ngày Lập', 'Người Lập'
                     , 'Consignee', 'Notifparty', 'Alsonotifyparty', 'Mv'
                     , 'Ngày Vận Đơn', 'Nơi Đi', 'Nơi Đến', 'BLNo', 'Commodity', 'Discount', 'Handling Fee'
                     , 'Diễn Giải Cộng' , 'Giá Trị Cộng', 'Diễn Giải Trừ', 'Giá Trị Trừ'
                     , 'Diễn Giải Cộng PO', 'Giá Trị Cộng PO', 'Diễn Giải Trừ PO', 'Giá Trị Trừ PO'
                     , 'Comoodity VN', 'Ngày Mở Tờ Khai', 'Ngày Phải TT'
                     , 'Total Dis' , 'Total Net', 'Total Gross'
					 , 'Tiền đặt cọc','Tiền đã trả','Tiền còn lại'
                     , 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật'
                     , 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'c_packinginvoice_id'
                , index: 'c_packinginvoice_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
				, formoptions: { colpos: 1, rowpos: 1 }
            },
            {
                 fixed: true, name: 'md_trangthai_id'
                 , index: 'md_trangthai_id'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , align: 'center'
                 , formatter: 'imagestatus'
				 , stype : 'select'
                 , searchoptions:{sopt:['in'], value:'SOANTHAO,HIEULUC:SOẠN THẢO & HIỆU LỰC;:[ALL];SOANTHAO:SOẠN THẢO;HIEULUC:HIỆU LỰC;KETTHUC:KẾT THÚC' }
				 , formoptions: { colpos: 2, rowpos: 1 }
            },
            {
                 fixed: true, name: 'so_pkl'
                 , index: 'so_pkl'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 2 }
            },
            {
                 fixed: true, name: 'so_inv'
                 , index: 'so_inv'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 3 }
            },
            {
                 fixed: true, name: 'sodh'
                 , index: 'dbo.LayChungTuDonHang(pi.c_packinginvoice_id)'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 4 }
            },
            {
                 fixed: true, name: 'nguoitao1'
                 , index: 'nguoitao1'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 5 }
            },
			{
                 fixed: true, name: 'ngaylap'
                 , index: 'ngaylap'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 1, rowpos: 6 }
            }, 
			{
                 fixed: true, name: 'nguoilap'
                 , index: 'nguoilap'
				 , hidden: true
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 1 }
            },
            {
                 fixed: true, name: 'consignee'
                 , index: 'consignee'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 7 }
            },
            {
                 fixed: true, name: 'notifparty'
                 , index: 'notifparty'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 8 }
            },
            {
                 fixed: true, name: 'alsonotifyparty'
                 , index: 'alsonotifyparty'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 9 }
            },
            {
                 fixed: true, name: 'mv'
                 , index: 'mv'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 10 }
            },
            {
                 fixed: true, name: 'etd'
                 , index: 'etd'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 1, rowpos: 11 }
            },
            {
                 fixed: true, name: 'noidi'
                 , index: 'noidi'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PortController.ashx?action=getoption' }
                 , formoptions: { colpos: 1, rowpos: 12 }
            },
            {
                 fixed: true, name: 'noiden'
                 , index: 'noiden'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 1, rowpos: 13 }
            },
            {
                 fixed: true, name: 'blno'
                 , index: 'blno'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 2 }
            },
            {
                 fixed: true, name: 'commodity'
                 , index: 'commodity'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 3 }
            },
            {
                 fixed: true, name: 'discount'
                 , index: 'discount'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0', readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 4 }
            },
            {
                 fixed: true, name: 'handling_fee'
                 , index: 'handling_fee'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0' }
                 , formoptions: { colpos: 2, rowpos: 5 }
            },
            {
                 fixed: true, name: 'diengiai_cong'
                 , index: 'diengiai_cong'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 6 }
            },
            {
                 fixed: true, name: 'giatri_cong'
                 , index: 'giatri_cong'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0' }
                 , formoptions: { colpos: 2, rowpos: 7 }
            },
            {
                 fixed: true, name: 'diengiai_tru'
                 , index: 'diengiai_tru'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 8 }
            },
            {
                 fixed: true, name: 'giatri_tru'
                 , index: 'giatri_tru'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { number:true, minValue:0 }
                 , editoptions:{ defaultValue:'0' }
                 , formoptions: { colpos: 2, rowpos: 9 }
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
                 , editoptions:{ defaultValue:'0', readonly: 'readonly' }
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
                 , editoptions:{ defaultValue:'0', readonly: 'readonly' }
                 , formoptions: { colpos:3, rowpos: 8 }
            },
            {
                 fixed: true, name: 'commodityvn'
                 , index: 'commodityvn'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , formoptions: { colpos: 2, rowpos: 10 }
            },
            {
                 fixed: true, name: 'ngay_motokhai'
                 , index: 'ngay_motokhai'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 2, rowpos: 11 }
            },
            {
                 fixed: true, name: 'ngay_phaitt'
                 , index: 'ngay_phaitt'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , formoptions: { colpos: 2, rowpos: 12 }
            },
            {
                 fixed: true, name: 'totaldis'
                 , index: 'totaldis'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 13 }
            },
            {
                 fixed: true, name: 'totalnet'
                 , index: 'totalnet'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 14 }
            },            
            {
                 fixed: true, name: 'totalgross'
                 , index: 'totalgross'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 15 }
            },
			{
                 fixed: true, name: 'tiendatcoc'
                 , index: 'tiendatcoc'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 16 }
            },
			{
                 fixed: true, name: 'tiendatra'
                 , index: 'tiendatra'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 17 }
            },
			{
                 fixed: true, name: 'tienconlai'
                 , index: 'tienconlai'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{ readonly: 'readonly' }
                 , formoptions: { colpos: 2, rowpos: 18 }
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
                , formoptions: { colpos: 2, rowpos: 16 }
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
            FilterToolbar="true"
            Height = "170"
            MultiSelect = "false" 
            ShowEdit ="false"
            ShowDel = "false"
            EditFormOptions =" 
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
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_chitietphaithuphanbo').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_chitietphaithuphanbo').jqGrid('setGridParam',{url:'Controllers/ChiTietThuChiPhanBoController.ashx?&tcId='+ids,page:1});
				                $('#grid_ds_chitietphaithuphanbo').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_chitietphaithuphanbo').jqGrid('setGridParam',{url:'Controllers/ChiTietThuChiPhanBoController.ashx?&tcId='+ids,page:1});
			                $('#grid_ds_chitietphaithuphanbo').jqGrid().trigger('reloadGrid');	
			               
		                } }"
            runat="server" />
</div>

<script type="text/javascript">
    createRightPanel('grid_ds_phaithuphanbo');
    createRightPanel('grid_ds_chitietphaithuphanbo');
</script>
