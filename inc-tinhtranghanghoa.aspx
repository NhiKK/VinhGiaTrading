<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-tinhtranghanghoa.aspx.cs" Inherits="inc_tinhtranghanghoa" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	
</script>

<uc1:jqGrid  ID="grid_tinhtrang_hanghoa" 
            Caption="Tình trạng hàng hóa"
            FilterToolbar="true"
            SortName="tt.ma_tinhtrang" 
            SortOrder="asc"
            UrlFileAction="Controllers/TinhTrangHangHoaController.ashx" 
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $('#edit_grid_tinhtrang_hanghoa').click(); }"
            ColModel = "[
            {
                fixed: true, name: 'md_tinhtranghanghoa_id'
                , index: 'tt.md_tinhtranghanghoa_id'
                , label: 'Tình trạng hàng hóa Id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ma_tinhtrang'
                , index: 'tt.ma_tinhtrang'
                , label: 'Mã'
                , width: 110
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'ten_tinhtrang'
                , index: 'tt.ten_tinhtrang'
                , label: 'Tên'
                , width: 160
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'ngayhethan'
                , index: 'tt.ngayhethan'
                , label: 'Ngày hết hạn'
                , width:80
                , editable: true
                , edittype: 'text'
                , align: 'center'
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', altField: '#shipmentdate', altFormat: 'dd/mm/yy'  }); } }
            },
            {
                fixed: true, name: 'tinhtrang_hethan'
                , index: 'tt.tinhtrang_hethan'
                , label: 'Tình trạng sau khi hết hạn'
                , width: 200
                , editable: true
                , align: 'center'
                , edittype: 'select'
                , editoptions: { dataUrl: 'Controllers/TinhTrangHangHoaController.ashx?action=getoption' }
                , formoptions: { label: 'Tình trạng sau&lt;br&gt;khi hết hạn' }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'tt.ngaytao'
                , label: 'Ngày tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'tt.nguoitao'
                , label: 'Người tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'tt.ngaycapnhat'
                , label: 'Ngày cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'tt.nguoicapnhat'
                , label: 'Người cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'tt.mota'
                , label: 'Mô tả'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'tt.hoatdong'
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
	createRightPanel('grid_tinhtrang_hanghoa');
</script>