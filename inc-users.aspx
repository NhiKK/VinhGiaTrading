<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-users.aspx.cs" Inherits="inc_users" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    /*$(window).bind('resize', function () {
        $("#grid_ds_nhanvien").setGridHeight($(window).height() - 220);
    }).trigger('resize');*/
	var jsonUserSMTP = <%=result%>;
</script>
	
    <uc1:jqGrid ID="grid_ds_nhanvien" 
            SortName="manv" 
            UrlFileAction="Controllers/UserController.ashx" 
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 65);"
            RowNumbers="true"
            FilterToolbar="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{ 
                        afterSubmit: function(rs, postdata) {
                            return showMsgDialog(rs);
                        },
                        beforeShowForm: function (formid) {
                                               formid.closest('div.ui-jqdialog').dialogCenter(); 
                        }}); }"
            ColModel = "[
            {
                fixed: true, name: 'manv'
                , label: 'Mã Nhân Viên'
                , index: 'manv'
                , width: 100 
                , editable:true
                , edittype: 'text'
                , key: true
                , editrules: { readyonly: 'readyonly' }
            },
            {
                fixed: true, name: 'matkhau'
                , label: 'Mật Khẩu'
                , index: 'matkhau'
                , width: 100 
                , editable:true
                , edittype: 'password'
                , hidden: true
                , editrules: { edithidden:true }
            },
            {
                fixed: true, name: 'tenvt'
                , label: 'Vai Trò'
                , index: 'tenvt'
                , width: 100 
                , editable:true
                , edittype: 'select'
                , editoptions: { dataUrl: 'Controllers/RoleController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'hoten'
                , label: 'Họ Tên'
                , index: 'hoten'
                , width: 100 
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'email'
                , label: 'Email'
                , index: 'email'
                , width: 100 
                , editable:true
                , edittype: 'text'
                , editrules:{ required:true, email:true }
            },
            {
                fixed: true, name: 'email_pass'
                , label: 'Email Password'
                , index: 'email_pass'
                , width: 100 
                , editable: true
                , edittype: 'password'
                , hidden: true
                , editrules:{ edithidden:true }
            },
			{
                fixed: true, name: 'smtp'
                , label: 'SMTP'
                , index: 'smtp'
                , width: 100 
                , editable:true
                , edittype: 'select'
                , editoptions:{ value: jsonUserSMTP }
            },
            {
                fixed: true, name: 'email_cc'
                , label: 'Email CC'
                , index: 'email_cc'
                , width: 100 
                , editable:true
                , edittype: 'text'
                , editrules:{ required:true, email:true }
            },
            {
                fixed: true, name: 'dienthoai'
                , label: 'Điện Thoại'
                , index: 'dienthoai'
                , width: 100 
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'otp'
                , label: 'OTP'
                , index: 'otp'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: false
            },
            {
                fixed: true, name: 'diachi'
                , label: 'Địa Chỉ'
                , index: 'diachi'
                , width: 100 
                , editable: true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'isadmin'
                , label: 'Admin'
                , index: 'isadmin'
                , width: 100
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
			{
                fixed: true, name: 'remote'
                , label: 'Remote'
                , index: 'remote'
                , width: 80
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'canOTP'
                , label: 'Cần OTP'
                , index: 'canOTP'
                , width: 80
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'suaGia'
                , label: 'Sửa giá PO'
                , index: 'suaGia'
                , width: 80
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'hieuLucDH'
                , label: 'Hiệu lực PO'
                , index: 'hieuLucDH'
                , width: 80
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'themPhiSauHL'
                , label: 'Thêm phí (sau HL)'
                , index: 'themPhiSauHL'
                , width: 80
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'doiNCU'
                , label: 'Đổi NCU (PI)'
                , index: 'doiNCU'
                , width: 80
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'suaPI'
                , label: 'Chỉnh sửa (PI)'
                , index: 'suaPI'
                , width: 80
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'email_xn'
                , label: 'Nhận email (PI)'
                , index: 'email_xn'
                , width: 80
                , editable: true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions: { value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'ngaytao'
                , label: 'Ngày Tạo'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , label: 'Người Tạo'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , label: 'Ngày Cập Nhật'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , label: 'Người Cập Nhật'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , label: 'Mô tả'
                , index: 'mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong'
                , label: 'Hoạt động'
                , index: 'hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            AddFormOptions=" 
            afterSubmit: function(rs, postdata) {
                            return showMsgDialog(rs);
                        },
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }"
            EditFormOptions=" 
            afterSubmit: function(rs, postdata) {
                return showMsgDialog(rs);
            },
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }"
            ViewFormOptions=" beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }"
            DelFormOptions="
            afterSubmit: function(rs, postdata) {
                            return showMsgDialog(rs);
                        },
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            runat="server" />

<script type="text/javascript">
    createRightPanel('grid_ds_nhanvien');
</script>
