<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-kichthuoc.aspx.cs" Inherits="inc_kichthuoc" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<uc1:jqGrid ID="grid_ds_kichthuoc"
    RowNumbers="true"
    OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
        beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
    } ); }"
    FilterToolbar = "true"
    SortName="kt.md_kichthuoc_id" 
    GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 65);"
    UrlFileAction="Controllers/KichThuocController.ashx" 
    ColNames="['md_kichthuoc_id', 'Tên Kích Thước', 'Tỉ Lệ', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
    ColModel = "[
    {
        fixed: true, name: 'md_kichthuoc_id'
        , index: 'kt.md_kichthuoc_id'
        , width: 100 
        , editable:true
        , edittype: 'text'
        , hidden : true
        , key: true
    },
    {
        fixed: true, name: 'ten_kichthuoc'
        , index: 'kt.ten_kichthuoc'
        , width: 100 
        , editable:true
        , editrules:{ required: true }
    },
    {
        fixed: true, name: 'tile'
        , align: 'right'
        , index: 'kt.tile'
        , width: 100 
        , editable:true
        , edittype: 'text'
        , custom:true
        , editrules:{ required: true, number:true }
        , editoptions: { defaultValue: '0' }
    },
    {
        fixed: true, name: 'ngaytao'
        , index: 'kt.ngaytao'
        , width: 100
        , editable:false
        , edittype: 'text'
        , hidden: true
    },
    {
        fixed: true, name: 'nguoitao'
        , index: 'kt.nguoitao'
        , width: 100
        , editable:false
        , edittype: 'text'
        , hidden: true
    },
    {
        fixed: true, name: 'ngaycapnhat'
        , index: 'kt.ngaycapnhat'
        , width: 100
        , editable:false
        , edittype: 'text'
        , hidden: true
    },
    {
        fixed: true, name: 'nguoicapnhat'
        , index: 'kt.nguoicapnhat'
        , width: 100
        , editable:false
        , edittype: 'text'
        , hidden: true
    },
    {
        fixed: true, name: 'mota'
        , index: 'kt.mota'
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
    ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
    ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
    ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
    AddFormOptions=" 
        beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
    "
    EditFormOptions ="
    beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
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
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
    "
    runat="server" />

<script type="text/javascript">
    createRightPanel('grid_ds_kichthuoc');
</script>