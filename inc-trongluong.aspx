<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-trongluong.aspx.cs" Inherits="inc_trongluong" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<uc1:jqGrid ID="grid_ds_trongluong"
    RowNumbers="true"
    FilterToolbar = "true"
    OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{ beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }}); }"
    GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 65);"
    SortName="tl.md_trongluong_id" 
    UrlFileAction="Controllers/TrongLuongController.ashx" 
    ColNames="['md_trongluong_id', 'Tên Tr.Lượng', 'Tỉ Lệ', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
    ColModel = "[
    {
        fixed: true, name: 'md_trongluong_id'
        , index: 'tl.md_trongluong_id'
        , width: 100 
        , editable:true
        , edittype: 'text'
        , hidden : true
        , key: true
    },
    {
        fixed: true, name: 'ten_trongluong'
        , index: 'tl.ten_trongluong'
        , width: 100 
        , editable:true
        , editrules:{ required: true }
    },
    {
        fixed: true, name: 'tile'
        , align: 'right'
        , index: 'tl.tile'
        , width: 100 
        , editable:true
        , edittype: 'text'
        , custom:true
        , editrules:{ required: true, number:true }
        , editoptions: { defaultValue: '0' }
    },
    {
        fixed: true, name: 'ngaytao'
        , index: 'tl.ngaytao'
        , width: 100
        , editable:false
        , edittype: 'text'
        , hidden: true
    },
    {
        fixed: true, name: 'nguoitao'
        , index: 'tl.nguoitao'
        , width: 100
        , editable:false
        , edittype: 'text'
        , hidden: true
    },
    {
        fixed: true, name: 'ngaycapnhat'
        , index: 'tl.ngaycapnhat'
        , width: 100
        , editable:false
        , edittype: 'text'
        , hidden: true
    },
    {
        fixed: true, name: 'nguoicapnhat'
        , index: 'tl.nguoicapnhat'
        , width: 100
        , editable:false
        , edittype: 'text'
        , hidden: true
    },
    {
        fixed: true, name: 'mota'
        , index: 'tl.mota'
        , width: 100
        , editable:true
        , edittype: 'textarea'
    },
    {
        fixed: true, name: 'hoatdong', hidden: true
        , index: 'tl.hoatdong'
        , width: 100
        , editable:true
        , edittype: 'checkbox'
        , align: 'center'
        , editoptions:{ value:'True:False', defaultValue: 'True' }
        , formatter: 'checkbox'
    }
    ]"
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

<script type="text/javascript">
    createRightPanel('grid_ds_trongluong');
</script>
