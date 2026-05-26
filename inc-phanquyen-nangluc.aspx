<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phanquyen-nangluc.aspx.cs" Inherits="inc_phanquyen_nangluc" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
    <uc1:jqGrid  ID="grid_ds_phanquyen_nangluc" 
            SortName="mavt" 
            UrlFileAction="Controllers/PhanQuyenNangLucController.ashx" 
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 45);"
            ColNames="['md_quanlynangluc_id', 'Nhân viên', 'Đối tác', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
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
            }); }"
            ColModel = "[
            {
                fixed: true, name: 'md_quanlynangluc_id'
                , index: 'md_quanlynangluc_id'
                , width: 250
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'manv'
                , index: 'manv'
                , width: 250
                , editable:true
                , edittype: 'select'
                , editoptions: { dataUrl: 'Controllers/UserController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'md_doitackinhdoanh_id'
                , index: 'dtkd.ma_dtkd'
                , width: 250
                , editable:true
                , edittype: 'select'
                , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=1' }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , hidden : true
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , hidden : true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , hidden : true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , hidden : true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'mota'
                , index: 'mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden:true
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
                }
            "
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
    createRightPanel('grid_ds_phanquyen_nangluc');
</script>