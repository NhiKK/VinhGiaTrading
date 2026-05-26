<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phienbangia.aspx.cs" Inherits="inc_phienbangia" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<uc1:jqGrid  ID="grid_ds_phienbanggia" 
        SortName="ten_phienbangia" 
        UrlFileAction="Controllers/PriceVersionController.ashx" 
        ColNames="['md_phienbangia_id', 'Tên Phiên Bản', 'Tên Bảng Giá', 'Ngày Hiệu Lực', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
        RowNumbers="true"
        OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
        ColModel = "[
        {
            fixed: true, name: 'md_phienbangia_id'
            , index: 'md_phienbangia_id'
            , width: 100
            , hidden: true 
            , editable:true
            , edittype: 'text'
            , key: true
        },
        {
             fixed: true, name: 'ten_phienbangia'
             , index: 'ten_phienbangia'
             , editable: true
             , width: 100
             , edittype: 'text'
        },
        {
            fixed: true, name: 'tenbg'
            , index: 'tenbg'
            , width: 100
            , edittype: 'select'
            , editable:true
            , editoptions: { dataUrl: 'Controllers/PriceListController.ashx?action=getoption' }
        },
        {
            fixed: true, name: 'ngay_hieuluc'
            , index: 'ngay_hieuluc'
            , width: 100
            , editable:true
            , edittype: 'text'
            , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
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
        ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
        ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
        ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
        runat="server" />