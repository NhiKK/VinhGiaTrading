<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-linkkiemtra.aspx.cs" Inherits="inc_linkkiemtra" %>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Register Src="jqGrid.ascx" TagName="jqGrid" TagPrefix="uc1" %>
<%@ Register Src="cbm_sanpham.ascx" TagName="cbm_sanpham" TagPrefix="uc2" %>
<script src="jQuery/Core/jquery-1.7.1.min.js" type="text/javascript"></script>

<style type="text/css">
    .table1padding tr td {
        padding: 3px;
    }

    .row-eq-height {
        display: -webkit-box;
        display: -webkit-flex;
        display: -ms-flexbox;
        display: flex;
    }
</style>

<%
//this.ma_hh.NullFirstItem = true;
//this.ma_hh.Name = "ma_hh";
%>

<div >
    <div style="background: #F4F0EC; width: 100%;" class="ui-layout-west" id="lay-west-linkkiemtra">
        <div id="line_filter">
            <table class="table1padding">
                <tr>
                    <td colspan ="2">Chọn Mã HH</td>
                    <td style="width: 100px">
                        <input id="ma_hh" name="ma_hh" type="text" />
                    </td>
                    <td>
                        <input type="button" id="linkkiemtra" value="Xem thông tin" onclick="linkkiemtra()" /></td>
                </tr>
            </table>
        </div>
    </div>

    <div style="background: #F4F0EC; width: 100%;" class="ui-layout-center ui-widget-content" id="lay-center-linkkiemtra">
    </div>
</div>

<script type="text/javascript">
    function linkkiemtra() {
        var ma_hh = $(`#ma_hh`).val();
         $("#lay-center-linkkiemtra").html('Đang tải...');
        $.ajax({
            url: "inc-view_linkkiemtra.aspx",
            type: "POST",
            data: {
                ma_hh: ma_hh
            },
            error: function (rs) {
                alert(rs);
            },
            success: function (rs) {
                $("#lay-center-linkkiemtra").html(rs);
            }
        });
    }
</script>
