<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-nhomchucnang.aspx.cs" Inherits="inc_nhomchucnang" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>

<uc1:jqGrid  ID="grid_ds_nhomchucnang" 
            Caption="Nhóm chức năng hàng hóa"
            FilterToolbar="true"
            SortName="ncl.sapxep" 
            SortOrder="asc"
            UrlFileAction="Controllers/NhomChucNangController.ashx" 
            ColNames="['md_nhomchungloai', 'Nhóm chức năng', 'Sắp xếp']"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'md_nhomchungloai_id'
                , index: 'ncl.md_nhomchungloai_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'code_ncl'
                , index: 'ncl.code_ncl'
                , width: 210
                , edittype: 'text'
                , editable:true
                , editrules: { required:true }
            },
            {
                 fixed: true, name: 'sapxep'
                 , index: 'ncl.sapxep'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
		 , align: 'center'
                 , editrules: { required:true }
            }
            ]"
            Height = "100"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions ="beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }
                            , afterSubmit: function(rs, postdata){
                                return showMsgDialog(rs);
                            }
                            "
            EditFormOptions="beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }
                            , afterSubmit: function(rs, postdata){
                                return showMsgDialog(rs);
                            }
                            "
            DelFormOptions="beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }"
            ViewFormOptions="width: 500
                            ,beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }"
            runat="server" />

<script type="text/javascript">
    createRightPanel('grid_ds_nhomchucnang');
</script>