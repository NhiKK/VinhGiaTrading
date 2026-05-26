<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-banggia.aspx.cs" Inherits="inc_banggia" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
        $('#lay-center-bangia').tabs();
        $('#lay-center-bangia').parent().layout({
            north: {
                size: "20%"
                , minSize: "20%"
                , maxSize: "20%"
                , onresize_end: function() { var o = $("#lay-north-bangia").height(); $("#grid_ds_banggia").setGridHeight(o - 75); }
            }
            , center: {
                onresize_end: function() {
                    var o = $("#lay-center-bangia").height();
                    $("#grid_ds_banggia").setGridHeight(o - 80);
                    $("#JqGrid1").setGridHeight(o - 80);
                    $("#JqGrid2").setGridHeight(o - 80);
                }
            }
            , souht: {
                size: "50%"
                , minSize: "50%"
                , maxSize: "50%"
            }
        });
    });
</script>
<DIV class="ui-layout-north ui-widget-content" id="lay-north-banggia">
    <uc1:jqGrid  ID="grid_ds_banggia" 
            SortName="ten_banggia" 
            UrlFileAction="Controllers/PriceListController.ashx" 
            ColNames="['md_banggia_id', 'Tên Bảng Giá', 'Đồng Tiền', 'Bảng Giá Bán',  'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
            ColModel = "[
            {
                fixed: true, name: 'md_banggia_id'
                , index: 'md_banggia_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ten_banggia'
                , index: 'ten_banggia'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                 fixed: true, name: 'tendt'
                 , index: 'tendt'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'banggiaban'
                , index: 'banggiaban'
                , width: 100
                , edittype: 'checkbox'
                , editable:true
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
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
            Height = "220"
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

<DIV class="ui-layout-center ui-widget-content" id="lay-center-banggia">

</DIV>

<DIV class="ui-layout-south ui-widget-content" id="lay-south-banggia">

</DIV>
    