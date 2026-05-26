<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-danhmuchanghoa.aspx.cs" Inherits="inc_danhmuchanghoa" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	
</script>

<uc1:jqGrid  ID="grid_danhmuc_hanghoa" 
            Caption="Danh mục hàng hóa"
            FilterToolbar="true"
            SortName="dm.ma_danhmuc" 
            SortOrder="asc"
            UrlFileAction="Controllers/DanhMucHangHoaController.ashx" 
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $('#edit_grid_danhmuc_hanghoa').click(); }"
            ColModel = "[
            {
                fixed: true, name: 'md_danhmuchanghoa_id'
                , index: 'dm.md_danhmuchanghoa_id'
                , label: 'Danh mục hàng hóa Id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ma_danhmuc'
                , index: 'dm.ma_danhmuc'
                , label: 'Mã'
                , width: 110
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'ten_danhmuc'
                , index: 'dm.ten_danhmuc'
                , label: 'Tên'
                , width: 110
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'cl.ngaytao'
                , label: 'Ngày tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'dm.nguoitao'
                , label: 'Người tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'dm.ngaycapnhat'
                , label: 'Ngày cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'dm.nguoicapnhat'
                , label: 'Người cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'dm.mota'
                , label: 'Mô tả'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'dm.hoatdong'
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
	createRightPanel('grid_danhmuc_hanghoa');
</script>