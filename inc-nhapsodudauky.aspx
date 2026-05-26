<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-nhapsodudauky.aspx.cs" Inherits="inc_nhapsodudauky" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register Src="cdm_doitackinhdoanh.ascx" TagName="Cbm" TagPrefix="uc3" %>
<%@ Register Src="cdm_doitackinhdoanhAll.ascx" TagName="Cbm" TagPrefix="uc2" %>
<%@ Register Src="cbm_kyketoan.ascx" TagName="Cbm" TagPrefix="uc6" %>
<%@ Register Src="cbm_dongmoky.ascx" TagName="Cbm" TagPrefix="uc7" %>
<link rel="stylesheet" type="text/css" href="jQuery/MutilSelect/jquery.multiselect.css" />
<script type="text/javascript" src="jQuery/MutilSelect/jquery.multiselect.js"></script>
<script type="text/javascript" src="jQuery/jquery.number.js"></script>
<%

    LinqDBDataContext db = new LinqDBDataContext();

    this.dtkd.Name = "md_doitackinhdoanh_id";
    this.dtkd.NullFirstItem = false;

    this.kyketoan.Name = "a_kytrongnam_id";
    this.kyketoan.NullFirstItem = false;
      
 %>

<div id="nhapsodudauky" style="overflow:auto;">
       <div style="margin: 30px 0px 10px 50px;"><span style="font-size:16px; font-weight:bold;">Nhập số dư đầu kỳ công nợ khách hàng</span></div>
       
       <div id="message" style="margin: 30px 0px 10px 50px;"></div>
       <table style="text-align:left; font-size:13px !important; margin:30px 0px 0px 100px">
            <tr>
                <td>Chọn khách hàng/NCC</td>
                <td><uc2:Cbm id="dtkd" runat="server"/></td>
            </tr>
            <tr>
                <td>Chọn kỳ kế toán</td>
                <td><uc7:Cbm id="kyketoan" runat="server"/></td>
            </tr>
            <tr>
                <td>Số dư</td>
                <td><input type="text" name="sodu" id="sodu"/></td>
                
            </tr>
            <tr>
                <td>Số dư nợ(Y)/Có(N)</td>
                <td><input type="checkbox" name="isnoco" id="isnoco" checked="checked"/></td>
            </tr>
            <tr>
                <td></td>
                <td><button name ="submit" id="submit"> Cập nhật</button></td>
            </tr>
       </table>
</div>
<script language="javascript">
    $(document).ready(function() {
		$("#sodu").number(true, 2);
        $("#submit").click(function() {
            var dtkd = $("#md_doitackinhdoanh_id").val();
            var kyketoan = $("#a_kytrongnam_id").val();
            var sodu = $("#sodu").val();
            var isnoco = $("#isnoco").attr("checked") ? "1" : "0";
            $.ajax({
                url: "Controllers/nhapcongnodauky.ashx?action=NhapDauKy",
                type: "POST",
                datatype: "xml",
                data: { md_doitackinhdoanh_id: dtkd, a_kytrongnam_id: kyketoan, sodu: sodu, isnoco: isnoco },
                error: function(rs) { },
                success: function(rs) {
                    var type = eval($(rs).find("type").text());
                    var message = $(rs).find("message").text();
                    if (type = 1) {
                        $("#message").html("<span style=\"font-size:14px; font-weight:bold; color:green;\">" + message + "</span>");
                    } else {
                        $("#message").html("<span style=\"font-size:14px; font-weight:bold; color:red;\">" + message + "</span>");
                    }

                    setTimeout(function() {
                        var divmessage = $("#message");
                        if (divmessage.html() == null) return false;
                        if (divmessage.html().length > 0) {
                            divmessage.empty();
                            return false;
                        }
                    }, 5000);
                }
            });
        });
    }); 
</script>