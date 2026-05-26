<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-quanlythongtin.aspx.cs" Inherits="inc_quanlythongtin" %>
<%@ Import Namespace="System.Linq" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(Context)));
%>
<script>
    $(document).ready(function() {
        $('div.btn').button();

        $('#btnUpdateProfile').click(function() {
            var data = $('#frmProfile').serialize();
            $.post('Controllers/UserProfile.ashx?type=editprofile', data, function(result) {
                alert(result);
            });
        });

        $('#btnUpdatePassword').click(function() {
            var data = $('#frmProfile').serialize();
            $.post('Controllers/UserProfile.ashx?type=changepassword', data, function(result) {
                alert(result);
            });
        });
    });
</script>
<div style="width:90%; text-align:center; padding:20">
    <form id="frmProfile" method="post">
        <table style="text-align:left; font-size:13px !important; margin:100px 0px 0px 100px">
            <tr>
                <td>Mã nhân viên</td>
                <td>:</td>
                <td><input type="text" id="txtMaNV" name="txtMaNV" disabled="disabled" value="<%= nv.manv %>" /></td>
                <td style="width:80px"></td>
                <td>Mật khẩu</td>
                <td>:</td>
                <td><input type="password" id="txtMatKhau" name="txtMatKhau" /></td>
            </tr>
              
            <tr>
                <td>Họ tên</td>
                <td>:</td>
                <td><input type="text" id="txtHoTen" name="txtHoTen"  value="<%= nv.hoten %>" /></td>
                <td></td>
                <td>Nhập lại mật khẩu</td>
                <td>:</td>
                <td><input type="password" id="txtLapMatKhau" name="txtLapMatKhau" /></td>
            </tr>
            
            <tr>
                <td>Email</td>
                <td>:</td>
                <td><input type="text" id="txtEmail" name="txtEmail" value="<%= nv.email %>" /></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            
            <tr>
                <td>Điện thoại</td>
                <td>:</td>
                <td><input type="text" id="txtDienThoai" name="txtDienThoai" value="<%= nv.dienthoai %>" /></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            
            <tr>
                <td>Địa chỉ</td>
                <td>:</td>
                <td><textarea type="text" id="txtDiaChi" name="txtDiaChi"><%= nv.diachi %></textarea></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            
            <tr>
                <td> </td>
                <td> </td>
                <td> </td>
                <td> </td>
                <td> </td>
                <td> </td>
                <td> </td>
            </tr>
            
            <tr>
                <td></td>
                <td></td>
                <td><div class="btn" id="btnUpdateProfile">Thay đổi thông tin</div></td>
                <td></td>
                <td></td>
                <td></td>
                <td><div class="btn" id="btnUpdatePassword">Thay đổi mật khẩu</div></td>
            </tr>
            
        </table>
    </form>
</div>