<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-donggoi.aspx.cs" Inherits="inc_donggoi" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
    <uc1:jqGrid  ID="grid_ds_donggoi" 
            SortName="md_donggoi_id" 
            UrlFileAction="Controllers/PackingController.ashx" 
            ColNames="['md_donggoi_id', 'Tên Đóng Gói'
                    , 'Số Lượng Inner', 'Đơn Vị Tính Inner','L1'
                    , 'W1','H1','Số Lượng Outer'
                    , 'Đơn Vị Tính Outer','L2 Mix','W2 Mix'
                    , 'H2 Mix','V2','Số Lượng Cont Mix'
                    , 'Số Lượng Gói/cnt40','Tr.Lượng','Mix Cho Phép Sử Dụng'
                    , 'Ngày Tạo'
                    , 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật'
                    , 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
            ColModel = "[
            {
                fixed: true, name: 'md_donggoi_id'
                , index: 'md_donggoi_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ten_donggoi'
                 , index: 'ten_donggoi'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                fixed: true, name: 'sl_inner'
                , index: 'sl_inner'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'dvtinner'
                , index: 'dvtinner'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/UnitController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'l1'
                , index: 'l1'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'w1'
                , index: 'w1'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'h1'
                , index: 'h1'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'sl_outer'
                , index: 'sl_outer'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'dvtouter'
                , index: 'dvtouter'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/UnitController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'l2_mix'
                , index: 'l2_mix'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'w2_mix'
                , index: 'w2_mix'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'h2_mix'
                , index: 'h2_mix'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'v2'
                , index: 'v2'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'sl_cont_mix'
                , index: 'sl_cont_mix'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'soluonggoi_ctn'
                , index: 'soluonggoi_ctn'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'trongluong'
                , index: 'trongluong'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ number:true }
            },
            {
                fixed: true, name: 'mix_chophepsudung'
                , index: 'mix_chophepsudung'
                , width: 100
                , edittype: 'checkbox'
                , editable:true
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden : true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden : true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden : true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden : true
            },
            {
                fixed: true, name: 'mota'
                , index: 'mota'
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
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
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
            runat="server" />
			

