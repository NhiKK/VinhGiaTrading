<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-tygia.aspx.cs" Inherits="inc_tygia" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
    <uc1:jqGrid  ID="grid_ds_tygia" 
            SortName="ten_tygia" 
            UrlFileAction="Controllers/RateController.ashx" 
            ColNames="['mamenu', 'Tên Tỷ Giá', 'Từ Đồng Tiền', 'Sang Đồng Tiền', 'Hiệu Từ Ngày', 'Hiệu Lực Đến Ngày', 'Nhân Với', 'Chia Cho', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 45);"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{ beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }}); }"
            ColModel = "[
            {
                fixed: true, name: 'md_tygia_id'
                , index: 'md_tygia_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ten_tygia'
                 , index: 'ten_tygia'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'tu_dongtien'
                 , index: 'tu_dongtien'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'sang_dongtien'
                , index: 'sang_dongtien'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption' }
            },
            
            {
                fixed: true, name: 'hieuluc_tungay'
                , index: 'hieuluc_tungay'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                fixed: true, name: 'hieuluc_denngay'
                , index: 'hieuluc_denngay'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                fixed: true, name: 'nhan_voi'
                , index: 'nhan_voi'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editrules : { number: true }
            },
            {
                fixed: true, name: 'chia_cho'
                , index: 'chia_cho'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editrules : { number: true }
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
            AddFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            EditFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            ViewFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            DelFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            runat="server" />

<script type="text/javascript">
    createRightPanel('grid_ds_tygia');
</script>