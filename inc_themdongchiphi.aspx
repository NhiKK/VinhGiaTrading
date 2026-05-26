<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc_themdongchiphi.aspx.cs" Inherits="inc_themdongchiphi" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register Src="Invoice.ascx" TagName="Cbm" TagPrefix="uc2" %>
<%@ Register Src="PO.ascx" TagName="Cbm" TagPrefix="uc3" %>

<%
    LinqDBDataContext db = new LinqDBDataContext();
    string c_thuchi_id = Context.Request.Form["c_thuchi_id"];
    var thuchi = db.c_thuchis.Single(c=>c.c_thuchi_id.Equals(c_thuchi_id));
    string md_doitackinhdoanh = thuchi.md_doitackinhdoanh_id;

    this.po.md_doitackinhdoanh_id = md_doitackinhdoanh;
    this.po.Name = "c_donhang_id";
    this.po.NullFirstItem = true;

    this.invoice.md_doitackinhdoanh_id = md_doitackinhdoanh;
    this.invoice.Name = "c_packinginvoice_id";
    this.invoice.NullFirstItem = true;

    string action = Context.Request.Form["action"].ToString();
    c_chiphilienquan obj = new c_chiphilienquan();
    if (action.Equals("edit"))
    {
        string line_id = Context.Request.Form["line_id"].ToString();
        obj = db.c_chiphilienquans.Single(ct => ct.c_chiphilienquan_id.Equals(line_id));
        this.po.Value = obj.c_donhang_id;
        this.invoice.Value = obj.c_packinginvoice_id;
        if (!obj.c_donhang_id.Equals("NULL"))
        {
            this.loaithanhtoan.Value = "PO";
        }
        else if (!obj.c_packinginvoice_id.Equals("NULL"))
        {
            this.loaithanhtoan.Value = "IN";
        }
        else
        {
            this.loaithanhtoan.Value = "NO";
        }
    }
 %>
<form id="f_dongphi">
    <div id="filter_dongphi">
    <input type="hidden" name="c_thuchi_id" value="<%=c_thuchi_id %>"/>
    <input type="hidden" name="action" id="action" value="<%=action %>"/>
    <input type="hidden" name="id" id="id" value="<%=obj.c_chiphilienquan_id %>"/>
    <table>
        <tr>
            <td>TK Nợ</td>
            <td><input type="text" name="tkno" id="tkno" value="<%=obj.tk_no %>"/></td>
        </tr>
        <tr>
            <td>TK Có</td>
            <td><input type="text" name="tkco" id="tkco" value="<%=obj.tk_co %>"/></td>
        </tr>
        <tr>
            <td>Số tiền</td>
            <td><input type="text" name="sotien" id="sotien" value="<%=obj.sotien %>"/></td>
        </tr>
        <tr>
            <td>Kiểu hồ sơ liên quan</td>
            <td>
                <select id="loaithanhtoan" name="loaithanhtoan" runat="server">
                    <option value="NO"></option>
                    <option value="NH">Phí ngân hàng</option>
                    <option value="PO">Hợp đồng</option>
                    <option value="IN">Packing list - Invoice</option>
                </select>
            </td>
        </tr>
        <tr id="tr_invoice" style="display: none">
            <td>Chọn Packing list/Invoice</td>
            <td><uc2:Cbm id="invoice" runat="server"/></td>
        </tr>
        
        <tr id="tr_po" style="display: none">
            <td>Chọn P/O</td>
            <td><uc3:Cbm id="po" runat="server"/></td>
        </tr>
        
        <tr>
            <td>Diễn giải</td>
            <td><textarea name="diengiai" id="diengiai"><%=obj.diengiai %></textarea></td>
        </tr>
        
    </table>
</div>

</form>

<script language="javascript">
    $(function() {
        if ($("#loaithanhtoan").val() == "PO") {
            $("#tr_invoice").css({ "display": "none" });
            $("#c_packinginvoice_id").val("");
            $("#tr_po").css({ "display": "" });
            $("#istiencoc").attr("checked", true);
        } else if ($("#loaithanhtoan").val() == "IN") {
            $("#tr_po").css({ "display": "none" });
            $("#c_donhang_id").val("");
            $("#tr_invoice").css({ "display": "" });
        } else {
            $("#tr_po").css({ "display": "none" });
            $("#tr_invoice").css({ "display": "none" });
            $("#c_donhang_id").val(""); $("#c_packinginvoice_id").val("");
        }

        $("#loaithanhtoan").change(function() {
            var sel_op = $("#loaithanhtoan").val();
            if (sel_op == "PO") {
                $("#tr_invoice").css({ "display": "none" });
                $("#c_packinginvoice_id").val("");
                $("#tr_po").css({ "display": "" });

            } else if (sel_op == "IN") {
                $("#tr_po").css({ "display": "none" });
                $("#c_donhang_id").val("");
                $("#tr_invoice").css({ "display": "" });

            } else {
                $("#tr_po").css({ "display": "none" });
                $("#tr_invoice").css({ "display": "none" });
                $("#c_donhang_id").val(""); $("#c_packinginvoice_id").val("");
            }
        });
    });

    function luuDongPhi(dialog, grid) {
        var data = new Object();
        var action = $("#action").val();
        data = $("#f_dongphi").serialize();
        $.post("Controllers/ChiTietLienQuanController.ashx?action=" + action, data, function(result) {
//            if (result != null) {
//                alert(result);
//            } else {
                grid.jqGrid().trigger('reloadGrid');
                $(dialog).dialog('destroy').remove();
//            }
        });
//        $.ajax({
//            url: "Controllers/ChiTietThuChiController.ashx?action=add",
//            type: "POST",
//            data: data ,
//            error: function(rs) {
//                //alert(rs.responseText);
//            },
//            success: function(rs) {
//                //alert(rs);
//                $(dialog).dialog('destroy').remove();
//            }
//        });
    }
</script>