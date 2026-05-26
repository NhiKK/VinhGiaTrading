<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phannhommau.aspx.cs" Inherits="inc_phannhommau" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	
</script>

<uc1:jqGrid  ID="grid_phannhommau" 
            Caption="Phân nhóm màu"
            FilterToolbar="true"
            SortName="nm.ma_nhommau" 
            SortOrder="asc"
            UrlFileAction="Controllers/PhanNhomMauController.ashx" 
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $('#edit_grid_phannhommau').click(); }"
            ColModel = "[
            {
                fixed: true, name: 'md_nhommau_id'
                , index: 'nm.md_nhommau_id'
                , label: 'Nhóm màu Id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ma_nhommau'
                , index: 'nm.ma_nhommau'
                , label: 'Mã'
                , width: 110
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'ten_nhommau'
                , index: 'nm.ten_nhommau'
                , label: 'Tên'
                , width: 160
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'nm.ngaytao'
                , label: 'Ngày tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nm.nguoitao'
                , label: 'Người tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'nm.ngaycapnhat'
                , label: 'Ngày cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nm.nguoicapnhat'
                , label: 'Người cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'nm.mota'
                , label: 'Mô tả'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'nm.hoatdong'
                , label: 'Hoạt động'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
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
			

<script>
	createRightPanel('grid_phannhommau');
</script>