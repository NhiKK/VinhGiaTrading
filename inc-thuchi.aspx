<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-thuchi.aspx.cs" Inherits="inc_thuchi" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%
	LinqDBDataContext db = new LinqDBDataContext();
	string value = "<option md_loaidtkd_id='{3}' isncc='{2}' value='{0}'>{1}</option>", kq = "";
	foreach(md_doitackinhdoanh dtkd in db.md_doitackinhdoanhs.Where(s=>s.hoatdong == true).OrderBy(s => s.ma_dtkd))
	{
		kq += string.Format(value,dtkd.md_doitackinhdoanh_id, dtkd.ma_dtkd, dtkd.isncc.Value, dtkd.md_loaidtkd_id);
	}
%>
<script>
	var value_dtkd = "<%=kq%>";
    $(document).ready(function() {
        $('#layout-center-phaithu').parent().layout({
            south: {
                size: "50%"
                , onresize_end: function() {
                    var h = $("#layout-south-phaithu").height();
                    $("#grid_ds_chitietphaithu").setGridHeight(h - 95);
                    $("#grid_ds_chitietcackhoan").setGridHeight(h - 95);
                }
            }
            , center: {
                onresize_end: function() {
                var h = $("#layout-center-phaithu").height();
                    $("#grid_ds_phaithu").setGridHeight(h - 90);
                }
            }
        });

        $('#tabs-phaithu-details').tabs();
    });

    function SetTyGiaPhaiThu() {
        $.get('Controllers/RateController.ashx?action=getusdtovnd', function(result) {
            $('#tygia', "#FrmGrid_grid_ds_phaithu").val(result);
        });
    } 
	
    function updateQuyDoiPhaiThu(a) {
		if(a == null) a = 'edit';
		$('#loai').off('change');
		$('#loai').change(function() {
			load_loai_dtkd(a);
		});
		load_loai_dtkd(a);
        quydoi();
        $('#sotien', "#FrmGrid_grid_ds_phaithu").bind('change', function() {
            quydoi();
        });
        
        $('#tygia').bind('change', function() {
            quydoi();
        });
    }


    function quydoi() {
        var sotien = $("#sotien", "#FrmGrid_grid_ds_phaithu").val();
        var tygia = $("#tygia", "#FrmGrid_grid_ds_phaithu").val();
        var tienVnd = sotien * tygia;
        $('#quydoi_vnd', "#FrmGrid_grid_ds_phaithu").val(tienVnd);
    }

    function addDetailPhaiThu(action) {
        var par_id = $("#grid_ds_phaithu").jqGrid('getGridParam', 'selrow');
        if (par_id != null) {
            var detail_id = $("#grid_ds_chitietphaithu").jqGrid('getGridParam', 'selrow');
            var action_ = "add";
            if (detail_id != null) {
                action_ = "edit";
            }
            $.ajax({
                url: "inc_themdongthanhtoan.aspx",
                type: "POST",
                data: { c_thuchi_id: par_id, action: action, line_id: detail_id },
                error: function(rs) {
                    //alert(rs.responseText);
                },
                success: function(rs) {
                    var div = $("<div></div>");
                    div.appendTo($(document.body));
                    div.html(rs);
                    div.dialog({
                        title: "Chi tiết các khoản",
                        resizeable: false,
                        modal: true,
                        width: '350px',
                        height: 350,
                        close: function(event, ui) { $(this).dialog('destroy').remove(); },
                        buttons: [
					                        { text: "Lưu", click: function() { luuThanhToan($(this), $("#grid_ds_chitietphaithu")); ; } },
					                        { text: "Đóng", click: function() { $(this).dialog("destroy").remove(); } }
				                        ]
                    });
                }
            });
        } else {
            alert('Chưa chọn 1 phiếu thu!');
        }
    }
        
        function addDetailKhoanPhi(action) {
        var par_id = $("#grid_ds_phaithu").jqGrid('getGridParam', 'selrow');
        if (par_id != null) {
            var detail_id = $("#grid_ds_chitietcackhoan").jqGrid('getGridParam', 'selrow');
            var action_ = "add";
            if (detail_id != null) {
                action_ = "edit";
            }
            $.ajax({
                url: "inc_themdongchiphi.aspx",
                type: "POST",
                data: { c_thuchi_id: par_id, action: action, line_id: detail_id },
                error: function(rs) {
                    alert(rs.responseText);
                },
                success: function(rs) {
                    var div = $("<div></div>");
                    div.appendTo($(document.body));
                    div.html(rs);
                    div.dialog({
                        title: "Chi tiết các khoản",
                        resizeable: false,
                        modal: true,
                        width: '350px',
                        height: 250,
                        close: function(event, ui) { $(this).dialog('destroy').remove(); },
                        buttons: [
					                        { text: "Lưu", click: function() { luuDongPhi($(this), $("#grid_ds_chitietcackhoan")) ; } },
					                        { text: "Đóng", click: function() { $(this).dialog("destroy").remove(); } }
				                        ]
                    });
                }
            });
        } else {
            alert('Chưa chọn 1 phiếu thu!');
        }

    }

    function funcActiveThu() {
        var id = $("#grid_ds_phaithu").jqGrid('getGridParam', 'selrow');
        if (id != null) {


            var msg = "Có phải bạn muốn hiệu lực phiếu thu này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-phieuthu\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-phieuthu').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active-phieuthu",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-active-phieuthu").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/ThuChiController.ashx?action=activephieuthu&id=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-active-phieuthu').find('#dialog-caution').html(result);
                                $('#grid_ds_phaithu').jqGrid().trigger('reloadGrid');
                            });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Không",
                        click: function() {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
            
            
        } else {
            alert('Chưa chọn phiếu thu muốn hiệu lực.!');
        }
    }


    function phanBoCocInvoice() {
        var id = $("#grid_ds_chitietphaithu").jqGrid('getGridParam', 'selrow');
        if (id != null) {
            $.ajax({
                url: "inc-phanbococinvoice.aspx?chitietphaithuid=" + id,
                type: "POST",
                data: {},
                error: function(rs) { },
                success: function(rs) {
                    var div = $("<div></div>");
                    div.appendTo($(document.body));
                    div.html(rs);
                    div.dialog({
                        title: "Phân bổ tiền cọc cho Invoice",
                        resizable: false,
                        modal: true,
                        width: 530, 
                        close: function(event, ui) { $(this).dialog('destroy').remove(); }
                    });
                }
            });
        } else {
            alert("Chưa chọn dòng cần phân bổ!");
        }
    }
	
    function load_loai_dtkd(a) {
        var NCUVT = 'e39d870cb41bf429e4e033e37c8a4789';
        var NCC = '57062f40f8c5267d77d7cd56973841f4';
        var KH = 'f215ff793cded83e40f37f56bf44d1cb';

		if($('#loai').val() == 'NCU') {
		    var ncc_i = 0;
			$('#md_doitackinhdoanh_id option').each(function(){
				if($(this).attr('isncc') == 'True') {
					$(this).show();
					if(ncc_i == 0) { 
						ncc_i = 1; 
						if(a == 'add')
							$('#md_doitackinhdoanh_id').val($(this).val()); 
					}
				}
				else {
					$(this).hide();
				}
			});
		}
		else {
			var kh_i = 0;
			$('#md_doitackinhdoanh_id option').each(function(){
				if($(this).attr('isncc') == 'True') {
					$(this).hide();
				}
				else if ($(this).attr('isncc') == 'False' & $(this).attr('md_loaidtkd_id') == 'e39d870cb41bf429e4e033e37c8a4789')
				{
				    $(this).hide();
				}
				else {
					$(this).show();
					if(kh_i == 0) { 
						kh_i = 1; 
						if(a == 'add')
							$('#md_doitackinhdoanh_id').val($(this).val()); 
					}
				}
			});
		}
	}
</script>
<div class="ui-layout-south ui-widget-content" id="layout-south-phaithu">
    <div class="ui-layout-center" id="tabs-phaithu-details">
        <ul>
            <li><a href="#phaithu-details-1">Chi tiết các khoản</a></li>
            <li><a href="#phaithu-details-2">Các khoản phí liên quan</a></li>
        </ul>
      <%-- <div id="phaithu-details-1">
                <uc1:jqGrid  ID="grid_ds_chitietphaithu" 
            FilterToolbar="true"
            SortName="cttc.c_chitietthuchi_id" 
            UrlFileAction="Controllers/ChiTietThuChiController.ashx" 
            ColNames="['c_chitietthuchi_id', 'so_c', 'Thu Chi', 'TK Nợ', 'TK Có', 'Số Tiền', 'Quy Đổi', 'Diễn Giải',  'P/O', 'Packing list/Invoice', 'Là tiền cọc', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OverrideAdd = "function(){addDetailPhaiThu('add')}"
            OndblClickRow = "function(){addDetailPhaiThu('edit')}"
            FuncPhanBoCocInv="phanBoCocInvoice"
            ColModel = "[
            {
                fixed: true, name: 'c_chitietthuchi_id'
                , index: 'cttc.c_chitietthuchi_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'c_thuchi_id'
                 , index: 'cttc.c_thuchi_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/ThuChiController.ashx?action=getoption' }
                 , editrules:{ edithidden : true }
                 , hidden:true
            },
			{
                fixed: true, name: 'so_c'
                , index: 'cttc.so_c'
                , width: 100
                , hidden: false 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'tk_no'
                , index: 'cttc.tk_no'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'tk_co'
                , index: 'cttc.tk_co'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sotien'
                , index: 'cttc.sotien'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'quydoi'
                , index: 'cttc.quydoi'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'diengiai'
                , index: 'cttc.diengiai'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'c_donhang_id'
                , index: 'cttc.c_donhang_id'
                , width: 100
                , edittype: 'text'
                
            },
            {
                fixed: true, name: 'c_packinginvoice_id'
                , index: 'cttc.c_packinginvoice_id'
                , width: 100
                , edittype: 'text'
                
            },
            {
                fixed: true, name: 'isdatcoc'
                , index: 'cttc.isdatcoc'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , formatter: 'checkbox'
                , align: 'center'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'cttc.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'cttc.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'cttc.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'cttc.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'mota'
                , index: 'cttc.mota'
                , width: 100
                , editable: true
                , edittype: 'textarea'
                , hidden: true
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'cttc.hoatdong'
                , width: 100
                , editable:false
                , edittype: 'checkbox'
                , editoptions:{ defaultValue: 'True' }
                , editrules:{ edithidden : true }
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 95);  $(this).setGridWidth($(o).width()); "
            Height = "150"
            MultiSelect = "false" 
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
            EditFormOptions ="
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
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
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
            runat="server" />
        </div>--%>
		
		 <div id="phaithu-details-1">
                <uc1:jqGrid  ID="grid_ds_chitietphaithu" 
            FilterToolbar="true"
            SortName="cttc.c_chitietthuchi_id" 
            UrlFileAction="Controllers/ChiTietThuChiController.ashx" 
            ColNames="['c_chitietthuchi_id', 'Phiếu', 'Thu Chi', 'TK Nợ', 'TK Có', 'Số Tiền', 'Quy Đổi', 'Diễn Giải',  'P/O', 'Packing list/Invoice', 'Là tiền cọc', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OverrideAdd = "function(){addDetailPhaiThu('add')}"
            OndblClickRow = "function(){addDetailPhaiThu('edit')}"
            FuncPhanBoCocInv="phanBoCocInvoice"
            ColModel = "[
            {
                fixed: true, name: 'c_chitietthuchi_id'
                , index: 'cttc.c_chitietthuchi_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
			{
                fixed: true, name: 'so_c'
                , index: 'cttc.so_c'
                , width: 100
                , hidden: false 
                , editable:false
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'c_thuchi_id'
                 , index: 'cttc.c_thuchi_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/ThuChiController.ashx?action=getoption' }
                 , editrules:{ edithidden : true }
                 , hidden:true
            },
            {
                fixed: true, name: 'tk_no'
                , index: 'cttc.tk_no'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'tk_co'
                , index: 'cttc.tk_co'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sotien'
                , index: 'cttc.sotien'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'quydoi'
                , index: 'cttc.quydoi'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'diengiai'
                , index: 'cttc.diengiai'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'c_donhang_id'
                , index: 'dh.sochungtu'
                , width: 100
                , edittype: 'text'
                
            },
            {
                fixed: true, name: 'c_packinginvoice_id'
                , index: 'so_pkl'
                , width: 100
                , edittype: 'text'
                
            },
            {
                fixed: true, name: 'isdatcoc'
                , index: 'cttc.isdatcoc'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , formatter: 'checkbox'
                , align: 'center'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'cttc.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'cttc.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'cttc.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'cttc.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'mota'
                , index: 'cttc.mota'
                , width: 100
                , editable: true
                , edittype: 'textarea'
                , hidden: true
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'cttc.hoatdong'
                , width: 100
                , editable:false
                , edittype: 'checkbox'
                , editoptions:{ defaultValue: 'True' }
                , editrules:{ edithidden : true }
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 95);  $(this).setGridWidth($(o).width()); "
            Height = "150"
            MultiSelect = "false" 
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
            EditFormOptions ="
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
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
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
            runat="server" />
        </div>
        
        <div id="phaithu-details-2">
               <uc1:jqGrid  ID="grid_ds_chitietcackhoan" 
            SortName="ctlq.c_chiphilienquan_id" 
            UrlFileAction="Controllers/ChiTietLienQuanController.ashx" 
            ColNames="['c_chitietthuchi_id', 'Thu Chi', 'TK Nợ', 'TK Có', 'Số Tiền', 'Quy Đổi', 'Diễn Giải',  'P/O', 'Packing list/Invoice', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            FilterToolbar="true"
            OverrideAdd = "function(){addDetailKhoanPhi('add');}"
            OndblClickRow = "function(){addDetailKhoanPhi('edit');}"
            
            ColModel = "[
            {
                fixed: true, name: 'c_chiphilienquan_id'
                , index: 'ctlq.c_chiphilienquan_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'c_thuchi_id'
                 , index: 'ctlq.c_thuchi_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/ThuChiController.ashx?action=getoption' }
                 , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'tk_no'
                , index: 'ctlq.tk_no'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'tk_co'
                , index: 'ctlq.tk_co'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sotien'
                , index: 'ctlq.sotien'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'quydoi'
                , index: 'ctlq.quydoi'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'diengiai'
                , index: 'ctlq.diengiai'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'c_donhang_id'
                , index: 'dh.sochungtu'
                , width: 100
                , edittype: 'text'
                
            },
            {
                fixed: true, name: 'c_packinginvoice_id'
                , index: 'cpk.so_pkl'
                , width: 100
                , edittype: 'text'
                , search : true
                
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ctlq.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'ctlq.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ctlq.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'ctlq.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'mota'
                , index: 'ctlq.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden:true
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'ctlq.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , editoptions:{ defaultValue: 'True' }
                , editrules:{ edithidden : true }
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 95);  $(this).setGridWidth($(o).width()); "
            Height = "150"
            MultiSelect = "false" 
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
            EditFormOptions ="
            beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
            return 'Lỗi: ' + data.responseText 
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
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
            }
            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
            runat="server" />
        </div>
    </div>
</div>

<div class="ui-layout-center ui-widget-content" id="layout-center-phaithu">
    <uc1:jqGrid  ID="grid_ds_phaithu"
            Caption="Phải Thu" 
            SortName="tc.ngaytao"
            UrlFileAction="Controllers/ThuChiController.ashx"
            FuncActive="funcActiveThu"
            FilterToolbar="true"
            ColNames="['c_thuchi_id', 'TT', 'Số Phiếu', 'Loại','Mã khách hàng', 'Ngày lập phiếu'
                ,  'Người Giao Nộp', 'Ngày Giao Nộp', 'Số Tiền'
                , 'Đồng Tiền', 'Tỷ Giá', 'Loại Phiếu'
                , 'Mô tả', 'Số Chứng Từ', 'Quy Đổi VND'
                , 'TK Quỹ'
                , 'Tổng chi tiết các khoản', 'Tổng gồm chi phí', 'Còn lại khi trừ các khoản'
                , 'Loại Chứng Từ'
                , 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, 
            {
                width:350,
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
	            errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
	            },
	            afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , afterShowForm: updateQuyDoiPhaiThu
            
            }
            ); }"
            ColModel = "[
            {
                fixed: true, name: 'c_thuchi_id'
                , index: 'c_thuchi_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 name: 'md_trangthai_id'
                 , index: 'md_trangthai_id'
                 , editable: false
                 , fixed:true , width: 100
                 , edittype: 'text'
                 , align:'center'
                 , formatter:'imagestatus'
                 , search : true
                 , stype : 'select'
                 , searchoptions:{sopt:['eq'], value:':[ALL];SOANTHAO:SOẠN THẢO;HIEULUC:HIỆU LỰC' }
            },
            {
                 fixed: true, name: 'sophieu'
                 , index: 'sophieu'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },
			{
                 fixed: true, name: 'loai'
                 , index: 'loai'
                 , editable: true
                 , width: 100
				 , formatter:'loai'
                 , edittype: 'select'
				 , editoptions: { value:'NCU:Thu NCU;KH:Thu Khách Hàng' }
				 , searchoptions:{sopt:['eq'], value:':[ALL];NCU:Thu NCU;KH:Thu Khách Hàng' }
            },
            {
                 fixed: true, name: 'md_doitackinhdoanh_id'
                 , index: 'dtkd.ma_dtkd'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { value: 'loading:Đang tải' }
            },
            {
                 fixed: true, name: 'ngaylapphieu'
                 , index: 'ngaylapphieu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            }, 
            {
                 fixed: true, name: 'nguoi_giaonop'
                 , index: 'nguoi_giaonop'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            }, 
            {
                 fixed: true, name: 'ngay_giaonop'
                 , index: 'ngay_giaonop'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            }, 
            {
                 fixed: true, name: 'sotien'
                 , index: 'sotien'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: {number:true, required:true, defaultValue: '0'}
                 , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            }, 
            {
                 fixed: true, name: 'md_dongtien_id'
                 , index: 'dt.ma_iso'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption' }
            }, 
            {
                 fixed: true, name: 'tygia'
                 , index: 'tygia'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: {number:true, required:true, defaultValue: '0'}
                 , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            }, 
            {
                 fixed: true, name: 'loaiphieu'
                 , index: 'loaiphieu'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            }, 
            {
                 fixed: true, name: 'lydo'
                 , index: 'lydo'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            }, 
            {
                 fixed: true, name: 'sochungtu'
                 , index: 'sochungtu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            }, 
            {
                 fixed: true, name: 'quydoi_vnd'
                 , index: 'quydoi_vnd'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: {number:true, required:true, defaultValue: '0'}
                 , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            }, 
            {
                 fixed: true, name: 'tk_quy'
                 , index: 'tk_quy'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: {number:true, required:true}
                 , editoptions: {defaultValue: '0'}
                 , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 0, prefix: ''}
            }, 
            {
                 fixed: true, name: 'tongcackhoan'
                 , index: 'tongcackhoan'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: {number:true, required:true}
                 , editoptions: {defaultValue: '0', readonly: 'readonly'}
                 , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            }, 
            {
                 fixed: true, name: 'tongkhoanphi'
                 , index: 'tongkhoanphi'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: {number:true, required:true}
                 , editoptions: {defaultValue: '0', readonly: 'readonly'}
                 , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            }, 
            {
                 fixed: true, name: 'tienconlai'
                 , index: 'tienconlai'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: {number:true, required:true}
                 , editoptions: {defaultValue: '0', readonly: 'readonly'}
                 , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
                 
            }, 
            {
                 fixed: true, name: 'md_loaichungtu_id'
                 , index: 'lct.tenchungtu'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/DocumentCategoryController.ashx?action=getoption' }
                 , hidden: true
            }, 
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
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
                , editoptions:{ defaultValue: 'True' }
                , editrules:{ edithidden : true }
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90); if($('#md_doitackinhdoanh_id').attr('id') == null) { $('#grid_ds_phaithu-pager_left .ui-icon-plus').click(); $.jgrid.hideModal('#editmodgrid_ds_phaithu', { gbox: '#gbox_grid_ds_phaithu'}); }"
            Height = "170"
            MultiSelect = "false" 
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                width:350,
                beforeShowForm: function (formid) {
					if($('#md_doitackinhdoanh_id').val() == 'loading')
						$('#md_doitackinhdoanh_id').html(value_dtkd);
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
	            },
	            afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , afterShowForm: function(){
                    SetTyGiaPhaiThu();
                    updateQuyDoiPhaiThu('add');
                } 
	        "
	        EditFormOptions ="
	            width:350,
	            beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }, 
				errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
	            },
	            afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , afterShowForm: updateQuyDoiPhaiThu
            "
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
	            }
	            , afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
	        "
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_chitietphaithu').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_chitietphaithu').jqGrid('setGridParam',{url:'Controllers/ChiTietThuChiController.ashx?&tcId='+ids,page:1});
				                $('#grid_ds_chitietphaithu').jqGrid().trigger('reloadGrid');
				                $('#grid_ds_chitietcackhoan').jqGrid('setGridParam',{url:'Controllers/ChiTietLienQuanController.ashx?&tcId='+ids,page:1});
				                $('#grid_ds_chitietcackhoan').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_chitietphaithu').jqGrid('setGridParam',{url:'Controllers/ChiTietThuChiController.ashx?&tcId='+ids,page:1});
			                $('#grid_ds_chitietphaithu').jqGrid().trigger('reloadGrid');	
			                $('#grid_ds_chitietcackhoan').jqGrid('setGridParam',{url:'Controllers/ChiTietLienQuanController.ashx?&tcId='+ids,page:1});
				            $('#grid_ds_chitietcackhoan').jqGrid().trigger('reloadGrid');	
		                } }"
            runat="server" />
</div>

<script type="text/javascript">
    createRightPanel('grid_ds_phaithu');
    createRightPanel('grid_ds_chitietphaithu');
</script>
