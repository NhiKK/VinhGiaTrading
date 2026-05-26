<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-baobi.aspx.cs" Inherits="inc_baobi" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
        $('#lay-center-baobi').parent().layout({
            north: {
                size: "50%"
                , onresize_end: function() {
                    var o = $("#lay-north-baobi").height();
                    $("#grid_ds_donmuabaobi").setGridHeight(o - 90);
                }
            }
            , center: {
                onresize_end: function() {
                    var o = $("#lay-center-baobi").height();
                    $("#grid_ds_dongbaobi").setGridHeight(o - 90);
                }
            }
        });
    });
</script>
<DIV class="ui-layout-north ui-widget-content" id="lay-north-baobi">    
    <uc1:jqGrid  ID="grid_ds_donmuabaobi" 
                Caption="Đơn Mua Bao Bì"
                SortName="c_donmuabaobi_id" 
                UrlFileAction="Controllers/OrderPackingController.ashx" 
                ColNames="['c_donmuabaobi_id', 'Đơn Mua', 'Ngày Lập', 'Người Lập', 'Đối Tác Kinh Doanh', 'Trạng Thái' ,'Ngày Tạo'
                        , 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật'
                        , 'Mô tả', 'Hoạt động']"
                RowNumbers="true"
                OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
                ColModel = "[
                {
                    fixed: true, name: 'c_donmuabaobi_id'
                    , index: 'c_donmuabaobi_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                     fixed: true, name: 'donmua'
                     , index: 'donmua'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules: { required:true}
                },
                {
                     fixed: true, name: 'ngaylap'
                     , index: 'ngaylap'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                },
                {
                     fixed: true, name: 'nguoilap'
                     , index: 'nguoilap'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules: { required:true}
                },
                {
                    fixed: true, name: 'md_doitackinhdoanh_id'
                    , index: 'dtkd.md_doitackinhdoanh_id'
                    , width: 100
                    , edittype: 'select'
                    , editable:true
                    , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption' }
                    , editrules : { required:true }
                },
                {
                    fixed: true, name: 'md_trangthai_id'
                    , index: 'tthai.md_trangthai_id'
                    , width: 100
                    , edittype: 'select'
                    , editable:true
                    , editoptions: { dataUrl: 'Controllers/StatusController.ashx?action=getoption' }
                    , editrules : { required:true }
                },
                {
                    fixed: true, name: 'ngaytao'
                    , index: 'ngaytao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'nguoitao'
                    , index: 'nguoitao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'ngaycapnhat'
                    , index: 'ngaycapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'nguoicapnhat'
                    , index: 'nguoicapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'mota'
                    , index: 'mota'
                    , width: 100
                    , editable:true
                    , edittype: 'textarea'
                    , hidden : true
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
                Height = "420"
                GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
                FilterToolbar="true"
                ShowAdd ="true"
                ShowEdit ="true"
                ShowDel = "true"
                AddFormOptions=" 
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            EditFormOptions ="
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
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
                runat="server" />
                
    
</DIV>
<DIV class="ui-layout-center ui-widget-content" id="lay-center-baobi">
    <uc1:jqGrid  ID="grid_ds_dongbaobi"
            Caption ="Đóng Bao Bì"
            SortName="c_dongbaobi_id" 
            UrlFileAction="Controllers/PackingCompleteController.ashx" 
            ColNames="['c_dongbaobi_id', 'Đơn Mua Bao Bì', 'Hình Thức Đóng Gói', 'Quy Cách', 'Số Lượng', 'Giá', 'Line', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
            ColModel = "[
            {
                fixed: true, name: 'c_dongbaobi_id'
                , index: 'c_dongbaobi_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'c_donmuabaobi_id'
                , index: 'dmbb.c_donmuabaobi_id'
                , width: 100
                , editable:true
                , edittype: 'select'
                , editoptions: { dataUrl: 'Controllers/OrderPackingController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'md_hinhthucdonggoi_id'
                , index: 'htdg.md_hinhthucdonggoi_id'
                , width: 100
                , editable:true
                , edittype: 'select'
                , editoptions: { dataUrl: 'Controllers/PackagingMethodController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'quycach'
                 , index: 'quycach'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'soluong'
                 , index: 'soluong'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'gia'
                 , index: 'gia'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'line'
                 , index: 'line'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
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
            Height = "420"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            FilterToolbar="true"
            ShowAdd ="true"
            ShowEdit ="true"
            ShowDel = "true"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            EditFormOptions ="
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
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            runat="server" />
</DIV>