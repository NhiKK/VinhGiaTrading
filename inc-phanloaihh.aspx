<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phanloaihh.aspx.cs" Inherits="inc_phanloaihh" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<uc1:jqGrid  ID="grid_ds_phanloai_hanghoa" 
            Caption="Phân loại hàng hóa theo chủng loại hàng"
            FilterToolbar="true"
            SortName="cl.code_cl" 
            SortOrder="desc"
            UrlFileAction="Controllers/PhanLoaiHHController.ashx" 
            ColNames="['md_phanloaihh', 'Code phân loại', 'Code Chủng Loại', 'Tên loại hàng', 'Mô tả', 'Cảng biển xuất hàng', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, { 
                                            beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }
                                            , afterSubmit: function(rs, postdata){
                                                return showMsgDialog(rs);
                                            }
                                        } 
                            ); }"
            ColModel = "[
            {
                fixed: true, name: 'md_phanloaihh_id'
                , index: 'pl.md_phanloaihh_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'code_pl'
                , index: 'pl.code_pl'
                , width: 110
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'md_chungloai_id'
                , index: 'cl.code_cl'
                , width: 110
                , edittype: 'select'
                , editoptions: { dataUrl: 'Controllers/ChungLoaiController.ashx?action=getoption' }
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                 fixed: true, name: 'ta_ngan'
                 , index: 'pl.ta_ngan'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
                 , editrules: { required:true }
            },
            {
                fixed: true, name: 'ta_dai'
                , index: 'pl.ta_dai'
                , width: 250
                , edittype: 'textarea'
                , editable:true
                , editrules: { required:true }
            },
             {
                 fixed: true, name: 'cangbiensx'
                 , index: 'pl.cangbiensx'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
                 , editrules: { required:true }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'cl.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'cl.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'cl.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'cl.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'cl.hoatdong'
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


<script type="text/javascript">
    createRightPanel('grid_ds_phanloai_hanghoa');
</script>