<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserNameProfile.ascx.cs" Inherits="UserNameProfile" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<form id="Form1" runat="server">
<span><a onclick="add_tab('Quản lý thông tin', 'inc-quanlythongtin.aspx')" href="#"><% Response.Write("Quản lý thông tin của: " + Context.User.Identity.Name); %></a></span> | <span><asp:LinkButton ID="btnThoat" runat="server" onclick="btnThoat_Click">Log Out</asp:LinkButton></span>
</form>
<script>
    $(document).ready(function() {
        setInterval(function() {
            $.ajax("Controllers/GetSession.ashx", function(rs) {
            });
        }, 60000);
    });
</script>
