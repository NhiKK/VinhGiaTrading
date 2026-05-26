<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-mailtemplate.aspx.cs" Inherits="inc_mailtemplate" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
    
    <uc1:jqGrid  ID="grid_ds_mailtemplate" 
            SortName="ten_template" 
            UrlFileAction="Controllers/MailTemplateController.ashx" 
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 45);"
            ColNames="['md_mailtemplate_id','Tên Template', 'Subject', 'Content', 'Sử Dụng Cho', 'Mặc Định', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                width:500
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            } ); }"
            ColModel = "[
            {
                fixed: true, name: 'md_mailtemplate_id'
                , index: 'md_mailtemplate_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ten_template'
                 , index: 'ten_template '
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'subject_mail'
                 , index: 'subject_mail'
                 , editable: true
                 , width: 100
                 , edittype: 'textarea'
            },
            {
                 fixed: true, name: 'content_mail'
                 , index: 'content_mail'
                 , editable: true
                 , width: 100
                 , edittype: 'textarea'
                 , hidden: true
                 , editrules:{ edithidden: true }
            },
            {
                fixed: true, name: 'use_for'
                , index: 'use_for'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { value:'QO:Quotation;PO:Purchase Order;PI:Đơn Đặt Hàng;PIPLUS:Đơn Đặt Hàng Bổ Sung;PMTREQ:Payment Request;KETOAN:Kế Toán;QRCode:QRCode' }
            },
            {
                 fixed: true, name: 'default_mail'
                 , index: 'default_mail'
                 , editable: true
                 , width: 100
                 , edittype: 'checkbox'
                 , align: 'center'
                 , editoptions:{ value:'True:False', defaultValue: 'True' }
                 , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: true
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
                width: 500
                , beforeShowForm: function (formid) {
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
                width:500
                , beforeShowForm: function (formid) {
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
    createRightPanel('grid_ds_mailtemplate');
</script>