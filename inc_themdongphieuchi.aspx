<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc_themdongphieuchi.aspx.cs" Inherits="inc_themdongphieuchi" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register Src="Invoice.ascx" TagName="Cbm" TagPrefix="uc2" %>
<%@ Register Src="InvoiceHh.ascx" TagName="Cbm" TagPrefix="uc5" %>
<%@ Register Src="PO.ascx" TagName="Cbm" TagPrefix="uc3" %>
<%@ Register Src="PhieuNhap.ascx" TagName="Cbm" TagPrefix="uc4" %>

<%
    var db = new LinqDBDataContext();
    string c_thuchi_id = Context.Request.Form["c_thuchi_id"];
    var thuchi = db.c_thuchis.Single(c=>c.c_thuchi_id.Equals(c_thuchi_id));
    string md_doitackinhdoanh = thuchi.md_doitackinhdoanh_id;
    string loaiphieu2 = "", xuongId = md_doitackinhdoanh, xuongVal = thuchi.doitacValue;
    this.po.md_doitackinhdoanh_id = md_doitackinhdoanh;
    this.po.Name = "c_donhang_id";
    this.po.NullFirstItem = true;

    this.invoice.md_doitackinhdoanh_id = md_doitackinhdoanh;
    this.invoice.Name = "c_packinginvoice_id";
    this.invoice.NullFirstItem = true;

    this.inv.md_doitackinhdoanh_id = md_doitackinhdoanh;
    this.inv.Name = "inv";
    this.inv.NullFirstItem = true;

    this.phieunhap.md_doitackinhdoanh_id = md_doitackinhdoanh;
    this.phieunhap.Name = "c_nhapxuat_id";
    this.phieunhap.NullFirstItem = true;

    string action = Context.Request.Form["action"].ToString();
    string value_date = "";
    var obj = new c_chitietthuchi();
    if (action.Equals("edit"))
    {
        string line_id = Context.Request.Form["line_id"].ToString();
        obj = db.c_chitietthuchis.Single(ct => ct.c_chitietthuchi_id.Equals(line_id));
        this.po.Value = obj.c_donhang_id;
        this.invoice.Value = obj.c_packinginvoice_id;
        this.phieunhap.Value = obj.obj_code;
        this.loaithanhtoan.Value = obj.loaiphieu;
        loaiphieu2 = obj.loaiphieu2;
        xuongId = obj.md_doitackinhdoanh_id;
        //xuongVal = obj.dtkd_val_ThuChi;
        if (obj.ngaychi != null)
            value_date = obj.ngaychi.Value.ToString("dd/MM/yyyy");
    }
    else {
        obj.sotien = thuchi.tienconlai;
        obj.isdatcoc = false;
    }

    string kh_id = db.md_loaidtkds.Where(s => s.ma_loaidtkd == "KH").Select(s => s.md_loaidtkd_id).FirstOrDefault();
    string ncc_id = db.md_loaidtkds.Where(s => s.ma_loaidtkd == "NCC").Select(s => s.md_loaidtkd_id).FirstOrDefault();
    string ncuvt_id = db.md_loaidtkds.Where(s => s.ma_loaidtkd == "NCCVT").Select(s => s.md_loaidtkd_id).FirstOrDefault();
    string obj_loaidtkd_id = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == obj.md_doitackinhdoanh_id).Select(s => s.md_loaidtkd_id).FirstOrDefault();

 %>
 <script>
	$(function(){
		$('#inv').val('<%= obj.c_packinginvoice_id %>');
	});
 </script>
<form id="f_thanhtoan">
    <div id="filter">
    <input type="hidden" name="c_thuchi_id" value="<%=c_thuchi_id %>"/>
    <input type="hidden" name="action" id="action" value="<%=action %>"/>
    <input type="hidden" name="id" id="id" value="<%=obj.c_chitietthuchi_id %>"/>
    <input type="hidden" name="dtkd" id="dtkd" value="<%=md_doitackinhdoanh %>"/>
    <input type="hidden" name="ids" id="ids"/>
    <input type="hidden" name="values" id="values"/>
    <table style="width: 100%;">
		<tr>
            <td>Loại</td>
            <td>
				<select <%= action.Equals("edit") ? "disabled" : "" %> id="loai" name="loai">
                <% if (obj.loai == "CK") { %>
					<option value="TM">Tiền mặt</option>				
					<option value="CK" selected>Chuyển khoản</option>
				<%} else { %>
					<option value="TM" selected>Tiền mặt</option>
					<option value="CK">Chuyển khoản</option>
				<%} %>			
                </select>
            </td>
        </tr>

        <tr>
            <td>TK Nợ</td>
            <td><input type="text" name="tkno" id="tkno" value="<%=obj.tk_no %>"/></td>
        </tr>
        <tr>
            <td>TK Có</td>
            <td><input type="text" name="tkco" id="tkco" value="<%=obj.tk_co %>"/></td>
        </tr>
		<tr>
			<td>Đối tác là NCCVT</td>
			<td>
				<input type="checkbox" name="dtlakh1" id="dtlakh1" value="False" />
				<script>
					$('#dtlakh1').change(function() {
						if($(this).prop('checked') == true) {
							$('#md_doitackinhdoanh_id2').children().each(function(){
								if($(this).attr('loaidt') == '<%= ncuvt_id %>'){
									$(this).show();
								}
								else if($(this).val() != ''){
									$(this).hide();
								}
							});
						}
						else {
							$('#md_doitackinhdoanh_id2').children().each(function(){
								if($(this).attr('loaidt') == '<%= ncc_id %>'){
									$(this).show();
								}
								else if($(this).val() != ''){
									$(this).hide();
								}
							});
						}
						$("#dtlakh").prop("checked", false);
						if ('<%= action %>' == "add")
							$('#md_doitackinhdoanh_id2').prop('selectedIndex',0);
					});
					$('#dtlakh1').change();
				</script>
			</td>
		</tr>
		<tr>
            <td>Đối tác là KH</td>
            <td>
				<input type="checkbox" name="dtlakh" id="dtlakh" value="False" />
				<script>
					$('#dtlakh').change(function() {
						if($(this).prop('checked') == true) {
							$('#md_doitackinhdoanh_id2').children().each(function(){
								if($(this).attr('loaidt') == '<%= kh_id %>'){
									$(this).show();
								}
								else if($(this).val() != ''){
									$(this).hide();
								}
							});
						}
						else {
							$('#md_doitackinhdoanh_id2').children().each(function(){
								if($(this).attr('loaidt') == '<%= ncc_id %>'){
									$(this).show();
								}
								else if($(this).val() != ''){
									$(this).hide();
								}
							});
						}
						$("#dtlakh1").prop("checked", false);
						if ('<%= action %>' == "add")
						{
							$('#md_doitackinhdoanh_id2').prop('selectedIndex',0);
						}
					});
					$('#dtlakh').change();
				</script>
			</td>
        </tr>
		<script>
			$(function(){
				if('<%= obj_loaidtkd_id %>' == '<%= ncuvt_id %>')
				{
					$('#dtlakh1').prop('checked', true);
					$('#dtlakh1').change();
				}
				else if('<%= obj_loaidtkd_id %>' == '<%= kh_id %>')
				{
					$("#dtlakh").prop("checked", true);
					$('#dtlakh').change();
				}
			});
		 </script>
		<tr>
            <td>Đối tác</td>
            <td>
				<select class="FormElement ui-widget-content ui-corner-all" name="md_doitackinhdoanh_id2" id="md_doitackinhdoanh_id2" value="" >
					<% 
					string str = "<option value=\"\"></option>";
					//foreach (md_doitackinhdoanh cn in db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id != null & s.md_doitackinhdoanh_id != "" & s.md_loaidtkd_id == "e39d870cb41bf429e4e033e37c8a4789").OrderBy(s=> s.ma_dtkd))
					foreach (md_doitackinhdoanh cn in db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id != null & s.md_doitackinhdoanh_id != "" & (s.md_loaidtkd_id == kh_id | s.md_loaidtkd_id == ncc_id | s.md_loaidtkd_id == ncuvt_id)
					).OrderBy(s=> s.ma_dtkd))
					{
						string selected = "";
						if (obj.md_doitackinhdoanh_id == cn.md_doitackinhdoanh_id) { selected = "selected"; }
						str += string.Format("<option loaidt=\"{3}\" {2} value=\"{0}\">{1}</option>", cn.md_doitackinhdoanh_id, cn.ma_dtkd, selected, cn.md_loaidtkd_id);
					}
					Response.Write(str);
					%>
					<!--<input type="text" name="md_doitackinhdoanh_id3" id="md_doitackinhdoanh_id3" value="<%=obj.md_doitackinhdoanh_id%>"/>-->
				</select>
			</td>
        </tr>
		
        <tr>
            <td>Số tiền</td>
            <td><input type="text" name="sotien" id="sotien" value="<%=obj.sotien %>"/></td>
        </tr>  
		<tr>
            <td>Số tiền cộng</td>
            <td><input type="text" name="sotiencong" id="sotiencong" value="<%=obj.sotiencong %>"/></td>
        </tr>
		<tr>
            <td>Thuế</td>
            <td>
                <select id="thue" name="thue">
					<% if(obj.thue == 0) {%>
						<option value="0" selected>VAT 00</option>
					<%} else {%>
						<option value="0">VAT 00</option>
					<%}%>
					
					<% if(obj.thue == 5) {%>
						<option value="5" selected>VAT 05</option>
					<%} else {%>
						<option value="5">VAT 05</option>
					<%}%>
					
					<% if(obj.thue == 10) {%>
						<option value="10" selected>VAT 10</option>
					<%} else {%>
						<option value="10">VAT 10</option>
					<%}%>  
					
					<% if(obj.sotiencong > 0) {%>
						<option value="0" selected>VAT Khác</option>
					<%} else {%>
						<option value="0">VAT Khác</option>
					<%}%>              
                </select>
            </td>
        </tr>
		<tr>
            <td>Bộ phận</td>
            <td>
				<select class="FormElement ui-widget-content ui-corner-all" name="md_bophan_id" id="md_bophan_id" value="" >
					<%
					str = "<option value=\"\"></option>";
					foreach (md_bophan cn in db.md_bophans.Where(s => s.md_bophan_id != null & s.md_bophan_id != "").OrderBy(s=> s.ten_bophan))
					{
						string selected = "";
						if (obj.md_bophan_id == cn.md_bophan_id) { selected = "selected"; }
						str += string.Format("<option {2} value=\"{0}\">{1}</option>", cn.md_bophan_id, cn.ten_bophan, selected);
					}
					Response.Write(str);
					%>
				</select>
			</td>
        </tr>
		<tr>
			<td>Ngày chi</td>
			<td><input id="ctd_ngaychi" name="ctd_ngaychi" value="<%=obj.ngaychi%>"/></td>
			<script>
				var now_vnn = new Date();
				var ngay_vnn = now_vnn.getDate(), thang_vnn = now_vnn.getMonth() + 1, nam_vnn = now_vnn.getFullYear();
				if (ngay_vnn < 10) {
					ngay_vnn = '0' + ngay_vnn;
				}
				if (thang_vnn < 10) {
					thang_vnn = '0' + thang_vnn;
				}
				var nht_vnn = ngay_vnn + "/" + thang_vnn + "/" + nam_vnn;
				if($('#ctd_ngaychi').val() != null & $('#ctd_ngaychi').val() != "")
					{$('#ctd_ngaychi').val('<%=value_date%>');}
				else
					{$('#ctd_ngaychi').val(nht_vnn);}
				
				$('#ctd_ngaychi').datepicker({
                    dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true,
                });
			</script>
		</tr>
		<tr>
            <td>Kiểu hồ sơ liên quan</td>
            <td>
                <select id="loaithanhtoan" name="loaithanhtoan" runat="server">
                    <option value="NO" selected="selected">Phiếu nhập</option>
                    <option value="PO">Hợp đồng</option>
                    <option value="IN">Packing list - Invoice</option>
					<%--<option value="CL">Chi tiền Clam</option>--%>
                    <option value="TU">Chi tạm ứng</option>
                    <option value="HH">Chi hoa hồng</option>
                    <option value="KH">Chi trả khác</option>
                    
                </select>
            </td>
        </tr>

        <tr>
            <td>Loại chi trả</td>
            <td>
                <select id="loaiphieu2" name="loaiphieu2" style="max-width: 200px;">
                    <option value=""></option>
                    <option ncc="ANCO1" value="chenhLechTyGiaDSBH">Chênh lệch tỷ giá (doanh số bán hàng)</option>
                    <option ncc="ANCO1,ANCO2" value="dsoVTXuatChoBPCtyKhac">D/số vật tư xuất cho các BP, các cty khác</option>
                    <option ncc="ANCO1,ANCO2" value="tienBHXHBHLDNghiViec">Tiền BHXH,BHLĐ, nghỉ việc</option>
                    <option ncc="ANCO2" value="dienNuocRacCtyHaca">Điện, nước, rác cty Haca</option>
                    <option ncc="ANCO2" value="nopTamUng">Nộp tạm ứng</option>
                    <option ncc="ANCO1,ANCO2" value="tienPheLieu">Tiền phế liệu</option>
                    <option ncc="ANCO1,ANCO2" value="VTBBPLTNK">Vật tư, bao bì, Pallet, Tem nhập kho</option>
                    <option ncc="ANCO1,ANCO2" value="chiPhiXuatKhau">Chi phí xuất khẩu</option>
                    <option ncc="ANCO1,ANCO2" value="chiPhiHCNS">Chi phí HCNS (điện, nước, rác, photo, bảo vệ, cước điện thoại, cơm trưa, xăng xe, VPP, tạp phẩm…)</option>
                    <option ncc="ANCO1,ANCO2" value="luongNV">Lương nhân viên</option>
                    <option ncc="ANCO1,ANCO2" value="luongCN">Lương công nhân</option>
                    <option ncc="ANCO1,ANCO2" value="phiCDBHXHNV">Phí CĐ và BHXH (23,5%) nhân viên</option>
                    <option ncc="ANCO1,ANCO2" value="phiCDBHXHCN">Phí CĐ và BHXH (23,5%) công nhân</option>
                    <option ncc="ANCO1,ANCO2" value="thuongLeTetNV">Thưởng lễ, tết nhân viên</option>
                    <option ncc="ANCO1,ANCO2" value="thuongLeTetCN">Thưởng tết công nhân</option>
                    <option ncc="ANCO2" value="tamUngLuongCN">Tạm ứng lương công nhân</option>
                    <option ncc="ANCO1,ANCO2" value="chiTienMatKhac">Chi tiền mặt khác (phí kiểm hàng tre, thiết bị máy vi tính…)</option>
                    <option ncc="ANCO2" value="trich20PheLieu">Trích 20% phế liệu</option>
                    <option ncc="ANCO1,ANCO2" value="KhieuNai">Khiếu nại</option>
                    <option ncc="ANCO1,ANCO2" value="cuocGuiMauKiemDinh">Cước gửi mẫu, kiểm định</option>
                </select>
            </td>
        </tr>
        
        <tr id="tr_pn" style="display:none">
            <td>Chọn Phiếu nhập</td>
            <td><uc4:Cbm id="phieunhap" runat="server"/></td>
        </tr>
        <tr id="tr_inv" style="display: none">
            <td>Chọn Packing list/Invoice</td>
            <td><uc2:Cbm id="inv" runat="server"/></td>
        </tr>
        <tr id="tr_invoice" style="display: none">
            <td>Chọn Packing list/Invoice</td>
            <td><uc5:Cbm id="invoice" runat="server"/></td>
        </tr>
        
        <tr id="tr_po" style="display: none">
            <td>Chọn P/O</td>
            <td><uc3:Cbm id="po" runat="server"/></td>
        </tr>
        
        <tr>
            <td>Là tiền đặt cọc</td>
            <td><input type="checkbox" id="istiencoc" name="istiencoc" <%=(obj.isdatcoc.Value) ? "checked=\"checked\"":""%>/></td>
        </tr>

        <tr>
            <td>Diễn giải</td>
            <td><textarea name="diengiai" id="diengiai" ><%=obj.diengiai %></textarea></td>
        </tr><tr>
            <td>Mô tả</td>
            <td><textarea name="mota" id="mota" ><%=obj.mota %></textarea></td>
        </tr>
        
    </table>
</div>

<div id="grid_pb" style="display:none;">
    <table class="ui-layout-center" id="ds_phieunhap"></table>
    <div id="ds_phieunhap-pager"></div>
</div>

    <script type="text/javascript">
        $(document).ready(function () {
            
            let lastSel = -1;
            jQuery("#ds_phieunhap").jqGrid({
                url:"Controllers/ChiTietPhaiChiController.ashx?action=loadPN&dtkd="+$("#dtkd").val(),
                datatype: "xml",
                height: 100,
                colNames: ['c_nhapxuat_id', 'Số chứng từ', 'Số P/O'
                    , 'Thành tiền', 'Còn lại phải trả'],
                colModel: [
                                {
                                    fixed: true, name: 'c_nhapxuat_id'
                                    , index: 'c_nhapxuat_id'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: true
                                    , hidden: true
                                    , key: true
                                },
                                {
                                    fixed: true, name: 'sochungtu'
                                    , index: 'cnx.sophieu'
                                    , width: 100
                                    , edittype: 'select'
                                    , editable: false
                                    , sortable: true
                                    , search: true
                                },
                                {
                                    fixed: true, name: 'sodonhang'
                                    , index: 'cdh.sochungtu'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: false
                                    , sortable: true
                                    , search: true
                                },
                                {
                                    fixed: true, name: 'thanhtien'
                                    , index: 'thanhtien'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                    , search: false
                                },
                                {
                                    fixed: true, name: 'conlaiphaitra'
                                    , index: 'conlaiphaitra'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: true
                                    , sortable: false
                                    , search: false
                                }
                                ],
                autowidth: false,
                multiselect: true,
                caption: "Danh sách phiếu nhập chưa thanh toán",
                height: 150,
                width: 500,
                gridComplete: function () { var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 70); },
                showRefresh: false,
                rowNumbers: true,
                cellsubmit: 'clientArray',
                onSelectRow: function (id) {
                    if (id && id !== lastSel) {
                        jQuery("#ds_phieunhap").jqGrid('restoreRow', lastSel);
                        jQuery("#ds_phieunhap").jqGrid('editRow', id, true, null, null, 'clientArray');
                        lastSel = id;
                    }
                },
//                ondblClickRow: function (rowid) {
//                    $(this).jqGrid('editGridRow', rowid,
//                                                {
//                width: 500
//                , afterSubmit: function(rs, postdata){
//                    return showMsgDialog(rs);
//                }
//                , beforeShowForm: function (formid) {
//                    formid.closest('div.ui-jqdialog').dialogCenter(); 
//                }
//                , errorTextFormat:function(data) { 
//                    return 'Lỗi: ' + data.responseText 
//                }
//                });
//                },
                editurl: 'clientArray',
                pager: $('#ds_phieunhap-pager')
            });
            $('#ds_phieunhap').jqGrid('navGrid', '#ds_phieunhap-pager', { add: false, edit: false, del: false, search: false, refresh: false }, { height: 280, reloadAfterSubmit: false }, { height: 280, reloadAfterSubmit: false }, { reloadAfterSubmit: false }, {});
            $("#ds_phieunhap").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });
        });
        </script>


</form>

<script type="text/javascript">
    $(function() {
	
        if ($("#loaithanhtoan").val() == "PO") {
            $("#tr_invoice").css({ "display": "none" });
            $("#tr_inv").css({ "display": "none" });
            $("#c_packinginvoice_id").val("");
            $("#tr_po").css({ "display": "" });
            $("#istiencoc").attr("checked", true);
            $("#grid_pb").css({ "display": "none" });
        } else if ($("#loaithanhtoan").val() == "IN") {
            $("#tr_po").css({ "display": "none" });
            $("#c_donhang_id").val("");
            $("#tr_invoice").css({ "display": "none" });
            $("#tr_inv").css({ "display": "" });
            $("#grid_pb").css({ "display": "none" });
        } else if ($("#loaithanhtoan").val() == "CL") {
            $("#tr_po").css({ "display": "none" });
            $("#c_donhang_id").val("");
            $("#tr_invoice").css({ "display": "none" });
            $("#tr_inv").css({ "display": "" });
            $("#grid_pb").css({ "display": "none" });
        } else if ($("#loaithanhtoan").val() == "HH") {
            $("#tr_po").css({ "display": "none" });
            $("#c_donhang_id").css({ "display": "none" });
            $("#tr_invoice").css({ "display": "" });
            $("#grid_pb").css({ "display": "none" });
            $("#istiencoc").attr("disabled", true);

        } else if ($("#loaithanhtoan").val() == "KH") {
            $("#tr_po").css({ "display": "none" });
            $("#c_donhang_id").css({ "display": "none" });
            $("#tr_invoice").css({ "display": "none" });
            $("#grid_pb").css({ "display": "none" });
            $("#istiencoc").attr("disabled", true);

        } else {
            $("#tr_po").css({ "display": "none" });
            $("#tr_invoice").css({ "display": "none" });
            $("#c_donhang_id").val(""); $("#c_packinginvoice_id").val("");
            $("#grid_pb").css({ "display": "" });
        }

        let loaithanhtoan = $('#loaithanhtoan', '#f_thanhtoan');
        loaithanhtoan.change(function () {
            let sel_op = $("#loaithanhtoan").val();
            let lp2Pr = $('#loaiphieu2', '#f_thanhtoan').parent().parent();
            $("#istiencoc").attr("disabled", false);
            $("#tr_inv").css({ "display": "none" });
            $("#tr_invoice").css({ "display": "none" });
            lp2Pr.hide();

            if (sel_op == "PO") {
                $("#tr_invoice").css({ "display": "none" });
                $("#c_donhang_id").css({ "display": "" });
                $("#c_packinginvoice_id").val("");
                $("#tr_po").css({ "display": "" });
                $("#grid_pb").css({ "display": "none" });

            } else if (sel_op == "IN") {
                $("#tr_po").css({ "display": "none" });
                $("#c_donhang_id").val("");
                $("#tr_inv").css({ "display": "" });
                $("#grid_pb").css({ "display": "none" });

            } else if (sel_op == "CL") {
                $("#tr_po").css({ "display": "none" });
                $("#c_donhang_id").val("");
                $("#tr_inv").css({ "display": "" });
                $("#grid_pb").css({ "display": "none" });

            } else if (sel_op == "TU") {
                $("#tr_po").css({ "display": "none" });
                $("#c_donhang_id").val("");
                $("#tr_invoice").css({ "display": "" });
                $("#grid_pb").css({ "display": "none" });

            } else if (sel_op == "HH") {
                $("#tr_po").css({ "display": "none" });
                $("#c_donhang_id").css({ "display": "none" });
                $("#tr_invoice").css({ "display": "" });
                $("#grid_pb").css({ "display": "none" });
                $("#istiencoc").attr("disabled", true);

            } else if (sel_op == "KH") {
                $("#tr_po").css({ "display": "none" });
                $("#c_donhang_id").css({ "display": "none" });
                $("#tr_invoice").css({ "display": "none" });
                $("#grid_pb").css({ "display": "none" });
                $("#istiencoc").attr("disabled", true);

                lp2Pr.show();

            } else {
                $("#tr_po").css({ "display": "none" });
                $("#tr_invoice").css({ "display": "none" });
                $("#c_donhang_id").val(""); $("#c_packinginvoice_id").val("");
                $("#grid_pb").css({ "display": "" });
            }
        });

        let sel = '<%=xuongVal%>';    
        $('#loaiphieu2 option', '#f_thanhtoan').each(function () {
            let attr = $(this).attr('ncc');
            if (typeof attr !== typeof undefined && attr !== false) {
                let arrAttr = attr.split(',');
                if (arrAttr.lastIndexOf(sel) > -1)
                    $(this).show();
                else
                    $(this).hide();
            }
            else {
                $(this).show();
            }
        });

        $('#md_doitackinhdoanh_id2', '#f_thanhtoan').val('<%=xuongId%>').change();
        $('#loaiphieu2', '#f_thanhtoan').val('<%=loaiphieu2%>');
        loaithanhtoan.change();
    });

    function luuThanhToan_pc(dialog, grid) {
            grid = $("#ds_phieunhap");
            var action = $("#action").val();
            var id = grid.jqGrid('getGridParam', 'selrow');
            grid.jqGrid('saveRow', id, true, 'clientArray', null, null);
            var ids = grid.jqGrid('getGridParam', 'selarrrow');
            if (ids.length > 0) {
                var names = [];
                for (var i = 0, il = ids.length; i < il; i++) {
                    var name = grid.jqGrid('getCell', ids[i], 'conlaiphaitra');
                    names.push(name);
                }
                $("#ids").val(JSON.stringify(ids).toString());
                $("#values").val(JSON.stringify(names).toString());

            }
            var data = $("#f_thanhtoan").serialize();
            // thuc hien luu dong chi tiet
            $.post("Controllers/ChiTietPhaiChiController.ashx?action=" + action, data, function (result) {
                var type = eval($(result).find('type').text());
                var message = $(result).find('message').text();
                if (type == 0) {
                    alert(message);
                } else {
                    $(dialog).dialog('destroy').remove();
                    $("#grid_ds_chitietphaichi").jqGrid().trigger('reloadGrid');
                }
            }); 
    }
</script> 