<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-hesohanghoa.aspx.cs" Inherits="inc_hesohanghoa" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	
</script>

<uc1:jqGrid  ID="grid_heso_hanghoa" 
            Caption="Danh mục hàng hóa"
            FilterToolbar="true"
            SortName="hs.ma_heso" 
            SortOrder="asc"
            UrlFileAction="Controllers/HeSoHangHoaController.ashx" 
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $('#edit_grid_heso_hanghoa').click(); }"
            ColModel = "[
            {
                fixed: true, name: 'md_heso_id'
                , index: 'hs.md_heso_id'
                , label: 'Hệ số Id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ma_heso'
                , index: 'hs.ma_heso'
                , label: 'Mã'
                , width: 110
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'giatri'
                , index: 'hs.giatri'
                , label: 'Giá trị'
                , width: 110
                , editable: true
                , fixed:true
                , edittype: 'text'
                , align: 'right'
				, formatter:'number'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'hs.ngaytao'
                , label: 'Ngày tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'hs.nguoitao'
                , label: 'Người tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'hs.ngaycapnhat'
                , label: 'Ngày cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'hs.nguoicapnhat'
                , label: 'Người cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'hs.mota'
                , label: 'Mô tả'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'hs.hoatdong'
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
	createRightPanel('grid_heso_hanghoa');
</script>