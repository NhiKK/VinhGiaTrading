<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-xemnangluc.aspx.cs" Inherits="inc_xemnangluc" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register Src="cdm_doitackinhdoanh.ascx" TagName="Cbm" TagPrefix="uc2" %>
<%@ Register Src="cbm_nhomNangLuc.ascx" TagName="cbm_nhomNangLuc" TagPrefix="uc3" %>
<script>
    $(document).ready(function() {
        $('#lay-center-xemnangluc').parent().layout({
            west: {
                size: "30%"
            }
        });
        $('#lay-west-xemnangluc button').button().css({ 'width': '95%' });

        $(".date input").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'mm/dd/yy' });
        $(".date input").datepicker('setDate', new Date());
        
    });

    function tinhTotalCbm() {
        var socont = eval($("#socont").val());
        var cbm_cont = eval($("#cbm_cont").val());
        $("#totalCbm").val(socont*cbm_cont);
    } 
   
</script>
<style type="text/css">
    .table1padding tr td
    {
       padding: 3px;
     }
</style>

<%
    this.hehang.NullFirstItem = true;
    this.hehang.Name = "hehang";
    

    //this.ncu.NullFirstItem = true;
    //this.ncu.isncc = true;

    string ngay = DateTime.Now.ToString("dd/MM/yyyy");
 %>
    
<div style="background:#F4F0EC" class="ui-layout-center ui-widget-content" id="lay-center-xemnangluc">
   
</div>

<div style="background:#F4F0EC;" class="ui-layout-west" id="lay-west-xemnangluc">
    <div id="line_filter">
        <table class="table1padding">
            <tr>
                <td>Chọn hệ hàng</td>
                <td colspan="3"><uc3:cbm_nhomNangLuc id="hehang" runat="server"/></td>
            </tr>
            <tr>
                <td>Số cont</td>
                <td><input type="text" name="socont" id="socont" style="width: 40px" maxlength="4" value="1" onkeyup="tinhTotalCbm();"/></td>
                <td>1 cont <=></td>
                <td><input type="text" name="cbm_cont" id="cbm_cont" style="width: 40px"  maxlength="2" value="54" onkeyup="tinhTotalCbm();"/>cbm</td>
            </tr>
            <tr>
                <td>Tổng Cbm</td>
                <td><input type="text" name="totalCbm" id="totalCbm" value="52"/></td>
            </tr>
            <%--<tr>
                <td>Nhà cung ứng</td>
                <td><uc2:Cbm id="ncu" runat="server"/></td>
            </tr>--%>
            <tr>
                <td>Ngày tính năng lực</td>
                <td><div class="date"><input type="text" name="ngaynangluc" id="ngaynangluc" value="<%=ngay %>"/></div></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="button" name="xemnangluc" value="Xem năng lực" onclick="xemNangLuc()"/></td>
            </tr>
        </table>
   </div>
</div>
<script language ="javascript" >
    function xemNangLuc() {
        var totalCbm = $("#totalCbm").val();
        var hehang = $("#hehang").val();
        var ngay = $("#ngaynangluc").val();
        $.ajax({
            url: "inc-view_xemnangluc.aspx",
            type: "POST",
            data: { cbm: totalCbm, hehang: hehang, ngay: ngay },
            error: function(rs) {
                alert(rs);
            },
            success: function(rs) {
                $("#lay-center-xemnangluc").html(rs);
            }
        });
    }
</script>