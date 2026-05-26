<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-view_quotation.aspx.cs" Inherits="inc_view_quotation" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Newtonsoft.Json.Linq" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%
    string jsondata = Request.Form["datagrid"].ToString();
    IList<JToken> tokenList = JToken.Parse(jsondata).ToList();
    LinqDBDataContext db = new LinqDBDataContext ();
    List<taoQuotation> productList = new List<taoQuotation>();

    foreach (JToken token in tokenList)
    {
        productList.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<taoQuotation>(token.ToString()));
    }

    List<String> arr =new List<String>();
    foreach (taoQuotation item in productList)
    {
        string sql = "select md_cangbien_id from md_sanpham where md_sanpham_id ='" + item.md_sanpham_id + "'";
        string cb = (string)mdbc.ExecuteScalar(sql);
        if (!arr.Contains(cb))
        {
            arr.Add(cb);
        }
    }
 %>
<form id="createQO">
<div id="tao-quotation">
    <ul>
        <li><a href="#tab1">Nhập thông tin Quotation</a></li>
        <li><a href="#tab2">Xem chi tiết</a></li>
    </ul>
    <div>
        <div id="tab1">
            <table>
                <tr>
                    <td>Khách hàng<b style="color:red">(*)</b></td>
                    <td><span id="khachhang_v" ></span></td>
                </tr>
                
                <tr>
                    <td>Ngày báo giá</td>
                    <td><input name="ngaybaogia_v" id="ngaybaogia_v" value=""/></td>
                </tr>
                <tr>
                    <td>Shipmenttime</td>
                    <td><input name="shipmenttime" id="shipmenttime" value="75"/></td>
                </tr>
                <tr>
                    <td>Ngày hết hạn<b style="color:red">(*)</b></td>
                    <td><input name="ngayhethan_v" id="ngayhethan_v" value=""/></td>
                </tr>
                <tr>
                    <td>Tr.Lượng</td>
                    <td><span id="ptrongluong_v" ></span></td>
                </tr>
                <tr>
                    <td>Đồng tiền</td>
                    <td><span id="pdongtien_v" ></span></td>
                </tr>
                <tr>
                    <td>Kích thước</td>
                    <td><span id="pkichthuoc_v" ></span></td>
                </tr>
				<tr>
                    <td>Cảng biển</td>
                    <td class="DataTD">
                        <select id="cangbien" name="cangbien">
                            <option>Đang tải...</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>Giá đặc biệt</td>
                    <td><input type="checkbox" id="gia_db" name="gia_db"/></td>
                </tr>
                <tr style="display:none;">
                    <td>Giá FOB phiên bản cũ</td>
                    <td><input type="checkbox" id="chkPBGCu" name="chkPBGCu" /></td>
                </tr>
                <tr id="tr_phienbangiacu" style="display:none;">
                    <td>Phiên bản giá cũ <b style="color:red">(*)</b></td>
                    <td class="DataTD">
                        <select id="phienbangiacu" name="phienbangiacu">
                            <option>Đang tải...</option>
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        
        <div id="tab2">
            <%
            for (int i = 0; i < arr.Count; i++)
            {
                string id_cang = arr[i].ToString();
                string ten_cang = db.md_cangbiens.Single(c=>c.md_cangbien_id.Equals(id_cang)).ten_cangbien;
                %>
               <table style="width:100%">
                   <tr class="ui-widget-header">
                        <td style="padding:5px; font-weight:bold; font-size:12px"><%=ten_cang %></td>
                   </tr>
               </table>
               <table style="width:100%">
                <tr>
                    <td style="text-align:center"><h3>Mã SP</h3></td>
                    <td style="text-align:center"><h3>Mã SP khách</h3></td>
                    <td style="text-align:center"><h3>Số lượng</h3></td>
                </tr>
                
                <%
                foreach (taoQuotation item in productList)
                {
                    string sql_ = "select md_cangbien_id from md_sanpham where md_sanpham_id ='" + item.md_sanpham_id + "'";
                    string cb_ = (string)mdbc.ExecuteScalar(sql_);
                    if(cb_.Equals(id_cang)){
                    %>
                   <tr>
                        <td style="padding:5px"><%=item.ma_sanpham %></td>
                        <td style="padding:5px"><%=item.ma_sp_khach %></td>
                        <td style="padding:5px"><%=item.soluong %></td>
                   </tr>
                <%}}%>
                
            </table> 
            <div style="height:30px"></div>
            <%}%>

        </div>
    </div>

</div>
</form>
<script language="javascript">
    $(function () {
        var f = $('#tao-quotation');
        f.tabs();

        $("#ngaybaogia_v", '#createQO').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $("#ngaybaogia_v", '#createQO').datepicker('setDate', new Date());

        $("#ngayhethan_v", '#createQO').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $("#ngayhethan_v", '#createQO').datepicker('setDate', new Date());


        $("#tab1 table tr td").css({ "padding": "3px" });

        $.get("Controllers/PartnerController.ashx?action=getoption&isncc=0", function (result) {
            $('#khachhang_v').append(result);
        });

        $.get("Controllers/trongluongController.ashx?action=getoption", function (result) {
            $('#ptrongluong_v').append(result);
        });
        $.get("Controllers/CurrencyController.ashx?action=getoption", function (result) {
            $('#pdongtien_v').html(result);
        });
        $.get("Controllers/kichthuocController.ashx?action=getoption", function (result) {
            $('#pkichthuoc_v').html(result);
        });

        $.get("Controllers/SelectOption/PhienBanGiaCu.ashx?action=PhienBanGiaCu", function (result) {
            result = result.replace('</select>', '');
            result = result.replace('<select>', '');
            $("#phienbangiacu", "#createQO").html(result);
        });
		
		$.get("Controllers/QuotationController.ashx?action=getSelectOption_cb", function (result) {
            result = result.replace('</select>', '');
            result = result.replace('<select>', '');
            $("#cangbien", "#createQO").html(result);
            $("#cangbien", "#createQO").prop('selectedIndex', 1);
        });

        ShowPBGCu_createQO('#createQO');
    });

    function ShowPBGCu_createQO(frmID, action) {
        var ShowPBGCu = function () {
            var chkPBGCu = $('#chkPBGCu', frmID);
            var tr_phienbangiacu = $('#tr_phienbangiacu', frmID);
            if (chkPBGCu.prop('checked') == true) {
                tr_phienbangiacu.show();
            }
            else {
                tr_phienbangiacu.hide();
            }
        }

        var chkPBGCu_ = $('#chkPBGCu', frmID);
        chkPBGCu_.off('change');
        chkPBGCu_.change(function () {
            ShowPBGCu();
        });
        ShowPBGCu();
        if (showPBGCforEveryOne) {
            chkPBGCu_.parent().parent().show();
        }
    }

    function createListQO(postdata, button) {
        var nbg = $("#ngaybaogia_v", "#createQO").val();
        var nhh = $("#ngayhethan_v", "#createQO").val();
        var check = chkCreateListQO(nbg, nhh);
        if (check == 0) {
            var post = new Object();
            post["khachhang"] = $("#khachhang_v select", "#createQO").val();
            post["cangbien"] = $("#cangbien", "#createQO").val();
            post["ngaybaogia"] = nbg;
            post["ngayhethan"] = nhh;
            post["trongluong"] = $("#ptrongluong_v select", "#createQO").val();
            post["dongtien"] = $("#pdongtien_v select", "#createQO").val();
            post["kichthuoc"] = $("#pkichthuoc_v select", "#createQO").val();
            post["datagrid"] = postdata;
            post["tgat"] = $("#shipmenttime", "#createQO").val();
            $.ajax({
                url: "Controllers/taoListQuotation.ashx",
                type: "POST",
                data: { dg: post, action: "createList" },
                error: function (rs) {
                    //alert(rs.responseText);
                },
                success: function (rs) {
                    $("#create_pos").attr("disabled", true);
                    button.button("option", "label", "Tạo Quotation thành công!");
                    alert(rs);
                }
            });
        }
    }

    function chkCreateListQO(nbg, nhh) {
        var arr_nbg = nbg.split('/'), arr_nhh = nhh.split('/');
        var check = 0;

        try {
            var date_nbg = new Date(arr_nbg[2] + '-' + arr_nbg[1] + '-' + arr_nbg[0]);

            var date_nhh = new Date(arr_nhh[2] + '-' + arr_nhh[1] + '-' + arr_nhh[0]);
            if (date_nbg == 'Invalid Date') {
                alert('Ngày báo giá không đúng định dạng dd/MM/yyyy.');
                check++;
            }
            else if (date_nhh == 'Invalid Date') {
                alert('Ngày hết hạn không đúng định dạng dd/MM/yyyy.');
                check++;
            }
            else if ($("#khachhang_v select", "#createQO").val() == "c010dd297f3ee5797e426176e8685b2c") {
                alert('Khách hàng không được bỏ trống!');
                check++;
            }
			else if ($("#cangbien", "#createQO").val() == "") {
                alert('Cảng biển không được bỏ trống!');
                check++;
            }
            else if (date_nbg >= date_nhh) {
                alert('Ngày báo giá không đuợc lớn hơn ngày hết hạn!');
                check++;
            }
        }
        catch (r) {
            check++;
            alert('Ngày không đúng định dạng.');
        }
        return check;
    }
</script>