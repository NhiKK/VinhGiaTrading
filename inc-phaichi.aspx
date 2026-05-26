<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phaichi.aspx.cs" Inherits="inc_phaichi" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
function Chitudong(action) {

        var now = new Date();
        var ngay = now.getDate(), thang = now.getMonth() + 1, nam = now.getFullYear();
        if (ngay < 10) {
            ngay = '0' + ngay;
        }
        if (thang < 10) {
            thang = '0' + thang;
        }
        var nht = ngay + "/" + thang + "/" + nam;
        $('body').append('<div id="dlgKT" title="Chi tự động">' +
        '<table id="vu-css">' +
        '<tr>' +

        '<td>' +
            '<label>Đối tác</label>' +
        '</td>' +

        '<td id="ctd_doitac">' +
        '</td>' +

        '</tr>' +
        //tu ngay
        '<tr>' +

        '<td>' +
            '<label>Từ ngày</label>' +
        '</td>' +

        '<td>' +
            '&nbsp &nbsp<input type="text" id="ctd_tungay" value="' + nht + '" />' +
        '</td>' +

        '</tr>' +
        //denngay
        '<tr>' +

        '<td>' +
            '<label>Đến ngày</label>' +
        '</td>' +

        '<td>' +
            '&nbsp &nbsp<input type="text" id="ctd_denngay" value="' + nht + '" />' +
        '</td>' +

        '</tr>' +
        //Ngày lập phiếu
        '<tr>' +

        '<td>' +
            '<label>Ngày lập phiếu</label>' +
        '</td>' +

        '<td>' +
            '&nbsp &nbsp<input type="text" id="ctd_ngaylapphieu" value="' + nht + '" />' +
        '</td>' +

        '</tr>' +
        //Người giao nộp
        '<tr>' +

        '<td>' +
            '<label>Người giao nộp</label>' +
        '</td>' +

        '<td>' +
            '&nbsp &nbsp<input type="text" id="ctd_nguoigiaonop" />' +
        '</td>' +

        '</tr>' +
        //Ngày giao nộp
        '<tr>' +

        '<td>' +
            '<label>Ngày giao nộp</label>' +
        '</td>' +

        ' <td>' +
            '&nbsp &nbsp<input id="ctd_ngaygiaonop" value="' + nht + '" />' +
        '</td>' +

        '</tr>' +



        //Đồng tiền
        '<tr>' +

        '<td>' +
            '<label>Đồng tiền</label>' +
        '</td>' +

        '<td id="ctd_dongtien">' +
        '</td>' +
        '</tr>' +

        //Tỷ giá
        '<tr>' +

        '<td>' +
            '<label>Tỷ giá</label>' +
        '</td>' +

        '<td id="ctd_tygia">' +
        '</td>' +
        '</tr>' +


        //Quy đổi VND
        '<tr>' +

        '<td>' +
            '<label>Quy đổi VND</label>' +
        '</td>' +

        '<td>' +
            '&nbsp &nbsp<input type="text" id="ctd_quydoivnd" value="0"/>' +
        '</td>' +
        '</tr>' +


        //Mô tả
        '<tr>' +

        '<td>' +
            '<label>Mô tả</label>' +
        '</td>' +

        '<td>' +
            '&nbsp &nbsp<textarea id="ctd_mota" name="mota" cols="20" rows="2" role="textbox" multiline="true"></textarea>' +
        '</td>' +
        '</tr>' +

        //Hoạt động
        '<tr style="display:none">' +

        '<td>' +
            '<label>Hoạt động</label>' +
        '</td>' +

        '<td>' +
            '&nbsp &nbsp<input type="checkbox" id="ctd_hoatdong" Checked/>' +
        '</td>' +
        '</tr>' +
        '</tr>' +
        '</table>' +
        '</div>');

        $('#dlgKT').dialog({
            width: 400,
            modal: true,
            open: function (event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                $.get("Controllers/PhaiChiController.ashx?action=getdoitac", function (result) {

                    $('#ctd_doitac').append(result);
                });

       
                    $.get("Controllers/PhaiChiController.ashx?action=gettygia", function (result) {
                        $('#ctd_tygia').append(result);
                    });

                    $.get("Controllers/PhaiChiController.ashx?action=getdongtien", function (result) {
                        $('#ctd_dongtien').append(result);
                    });


                $('#ctd_ngaygiaonop').datepicker({

                    dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true,
                });
                $('#ctd_ngaylapphieu').datepicker({

                    dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true,
                });

                $('#ctd_tungay').datepicker({

                    dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true,
                });

                $('#ctd_denngay').datepicker({

                    dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true,
                });

            },
            buttons: [
                    {
                        id: "btn-ok",
                        text: "Thực hiện chuyển đổi",
                        click: function () {
                            $("#btn-ok").button("option", "disabled", true);
                            $.post('Controllers/PhaiChiController.ashx?action=chitudong',
                                {
                                    hoatdong: "True", ngaylapphieu: $('#ctd_ngaylapphieu').val(),
                                    ngay_giaonop: $('#ctd_ngaygiaonop').val(), md_doitackinhdoanh_id: $('#ctd_doitac select').val(),
                                    tygia: $('#ctd_tygia input').val(), quydoi_vnd: $('#ctd_quydoivnd').val(), md_dongtien_id: $('#ctd_dongtien select').val()
                                    , tungay: $('#ctd_tungay').val(), denngay: $('#ctd_denngay').val()
                                }
                                , function (result) {
                                    $('#dlgKT').append(result);
                                    //jQuery("#grid_ds_phaichi").jqGrid('setGridParam', { url: "Controllers/ChiTietPhaiChiController.ashx?action=0", page: 1 });
                                    jQuery("#grid_ds_phaichi").jqGrid().trigger('reloadGrid');
                                    $("#btn-ok").button("option", "disabled", false);
                                });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Thoát",
                        click: function () {

                            $(this).dialog("destroy").remove();

                        }
                    }
            ]
        });

    }
    
    $(document).ready(function () {
		//setTimeout(function () {
        //    $('#grid_ds_phaichi-pager_left table tr').prepend(
        //        '<td class="ui-pg-button ui-corner-all" title="Chi tự động" id="chitudong_grid_ds_phaichi">' +
        //        '<div class="ui-pg-div" onclick="Chitudong(\'add\')"><span class="ui-icon ui-icon-circle-plus"></span></div><td>')
        //    , 5000
        //});
		
        $('#layout-center-phaichi').parent().layout({
            south: {
                size: "50%"
                , onresize_end: function() {
                    var h = $("#layout-south-phaichi").height();
                    $("#grid_ds_chitietphaichi").setGridHeight(h - 95);
                    $("#JqGrid1").setGridHeight(h - 95);
                }
            }
            , center: {
                onresize_end: function() {
                    var h = $("#layout-center-phaichi").height();
                    $("#grid_ds_phaichi").setGridHeight(h - 90);
                }
            }
        });
		
		
        $('#tabs-phaichi-details').tabs();
		
		$('.ui-layout-resizer').click();
    });

    function SetTyGiaPhaiChi(md_dongtien_id) {
        $.get('Controllers/RateController.ashx?action=getusdtovnd&md_dongtien_id=' + md_dongtien_id, function (result) {
            $('#tygia', "#FrmGrid_grid_ds_phaichi").val(result);
        });
    }


    function updateQuyDoiPhaiChi() {
            quydoiPhaiChi('');
        $('#sotien', "#FrmGrid_grid_ds_phaichi").bind('change', function() {
            quydoiPhaiChi();
        });

        $('#tygia', "#FrmGrid_grid_ds_phaichi").bind('change', function() {
            quydoiPhaiChi('');
        });
    }


    function quydoiPhaiChi() {
        var sotien = $("#sotien", "#FrmGrid_grid_ds_phaichi").val();
        var tygia = $("#tygia", "#FrmGrid_grid_ds_phaichi").val();
        var tienVnd = sotien * tygia;
        $('#quydoi_vnd', "#FrmGrid_grid_ds_phaichi").val(tienVnd);
    }

    function addDetailPhaiChi(action) {
        var par_id = $("#grid_ds_phaichi").jqGrid('getGridParam', 'selrow');
        if (par_id != null) {
            var detail_id = $("#grid_ds_chitietphaichi").jqGrid('getGridParam', 'selrow');
            var action_ = "add";
            if (detail_id != null) {
                action_ = "edit";
            }
            $.ajax({
                url: "inc_themdongphieuchi.aspx",
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
                        width: '550px',
                        height: 480,
                        close: function(event, ui) { $(this).dialog('destroy').remove(); },
                        buttons: [
					                        { text: "Lưu", click: function() { luuThanhToan_pc($(this), $("#grid_ds_chitietphaichi")); } },
					                        { text: "Đóng", click: function() { $(this).dialog("destroy").remove(); } }
				                        ]
                    });
                }
            });
        } else {
            alert('Chưa chọn 1 phiếu thu!');
        }

    }

    function funcActiveChi() {
        var id = $("#grid_ds_phaichi").jqGrid('getGridParam', 'selrow');
        if (id != null) {

            var msg = "Có phải bạn muốn hiệu lực phiếu chi này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-phieuchi\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-phieuchi').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active-phieuchi",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-active-phieuchi").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/PhaiChiController.ashx?action=activephieuchi&id=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-active-phieuchi').find('#dialog-caution').html(result);
                                $('#grid_ds_phaichi').jqGrid().trigger('reloadGrid');
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
            alert('Chưa chọn phiếu chi muốn hiệu lực.!');
        }
    }
	
	function inphieuchi(){
        var ids = $('#grid_ds_chitietphaichi').jqGrid('getGridParam', 'selrow');
        var cell = $('#grid_ds_chitietphaichi').getRowData(ids);
        var str_tkco = cell['tk_co'].substring(0,3);
        var printURL = "PrintControllers/";
        var windowSize = "width=900,height=700,scrollbars=yes";
        if( str_tkco == '112') {
            printURL += 'InPhieuUyNhiemChi/';
            window.open(printURL + "?id=" + ids, "In phiếu ủy nhiệm chi", windowSize);
        }
        else {
            printURL += 'InPhieuChiTienMat/';
            window.open(printURL + "?id=" + ids, "In phiếu chi tiền mặt", windowSize);
        }
    }
	
	    function inphieuchi1() {
        var ids = $('#grid_ds_phaichi').jqGrid('getGridParam', 'selrow');
        var cell = $('#grid_ds_phaichi').getRowData(ids);
        var printURL = "PrintControllers/InPhieuUyNhiemChi1/";
        var windowSize = "width=700,height=700,scrollbars=yes";
        window.open(printURL + "?id=" + ids, "In phiếu ủy nhiệm chi", windowSize);
		}
</script>

<div class="ui-layout-south ui-widget-content" id="layout-south-phaichi">
    <div class="ui-layout-center" id="tabs-phaichi-details">
        <ul>
            <li><a href="#phaichi-details-1">Chi tiết các khoản</a></li>
            <!--<li><a href="#phaichi-details-2">Các khoản phí liên quan</a></li>-->
        </ul>
        
		
		<div id="phaichi-details-1">
                <uc1:jqGrid  ID="grid_ds_chitietphaichi" 
            FilterToolbar="true"
            SortName="cttc.c_chitietthuchi_id" 
            UrlFileAction="Controllers/ChiTietPhaiChiController.ashx" 
            ColNames="['c_chitietthuchi_id', 'Phiếu', 'Thu Chi', 'TK Nợ', 'TK Có', 'Số Tiền', 'Số Tiền Cộng', 'Thuế', 'Mã Đối Tác', 'Quy Đổi'
                , 'Diễn Giải', 'Là tiền cọc', 'Phiếu nhập', 'Đơn Hàng', 'Invoice','Bộ Phận','Ngày Chi', 'Ngày Tạo', 'Người Tạo'
                , 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động'
                , 'cttc.md_doitackinhdoanh_id','Tên Đối Tác','Đại Diện' ,'Địa Chỉ', 'Số Tài Khoản','Ngân Hàng',]"
            RowNumbers="true"
            OverrideAdd = "function(){addDetailPhaiChi('add')}"
            OndblClickRow = "function(){addDetailPhaiChi('edit')}"
            OverrideEdit = "function(){addDetailPhaiChi('edit')}"
			InPhieuChi="inphieuchi"
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
                , width: 70
                , hidden: false 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'sophieu'
                 , index: 'cttc.sophieu'
                 , editable: true
                 , width: 60
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/ThuChiController.ashx?action=getoption' }
                 , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'tk_no'
                , index: 'cttc.tk_no'
                , width: 50
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'tk_co'
                , index: 'cttc.tk_co'
                , width: 50
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
                fixed: true, name: 'sotiencong'
                , index: 'cttc.sotiencong'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
				, hidden: true
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
			{
                fixed: true, name: 'thue'
                , index: 'cttc.thue'
                , width: 30
                , edittype: 'text'
                , editable:true
                , align:'right'
                //, formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
			{
                fixed: true, name: 'ma_dtkd'
                , index: 'dtkd.ma_dtkd'
                , width: 100
                , hidden: false 
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'quydoi'
                , index: 'cttc.quydoi'
                , width: 100
                , edittype: 'text'
                , editable:true
                , align:'right'
				, hidden:true
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true, name: 'diengiai'
                , index: 'cttc.diengiai'
                , width: 400
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'isdatcoc' 
                , index: 'cttc.isdatcoc'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'phieunhap'
                , index: 'nx.sophieu'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sodonhang'
                , index: 'dh.sochungtu'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/DonHangController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'c_packinginvoice_id'
                , index: 'cttc.c_packinginvoice_id'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/DonHangController.ashx?action=getoption' }
            },
			{
                fixed: true, name: 'md_bophan_id'
                , index: 'dh.md_bophan_id'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/BoPhanController.ashx?action=getoption' }
            },
			{
                fixed: true, name: 'ngaychi'
                , index: 'cttc.ngaychi'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: false 
                , editrules:{ edithidden : true }
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
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'cttc.hoatdong'
                , width: 100
                , editable:false
                , edittype: 'checkbox'
                , editoptions:{ defaultValue: 'True' }
                , editrules:{ edithidden : true }
            },
			            {
                fixed: true, name: 'md_doitackinhdoanh_id'
                , index: 'cttc.md_doitackinhdoanh_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ten_dtkd'
                , index: 'dtkd.ten_dtkd'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
                {
                fixed: true, name: 'daidien'
                , index: 'dtkd.daidien'
                , width: 100
                , hidden: true 
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'diachi'
                , index: 'dtkd.diachi'
                , width: 100
                , hidden: true 
                , hidden: true 
                , edittype: 'text'
                , editable:true
            },
                {
                fixed: true, name: 'so_taikhoan'
                , index: 'dtkd.so_taikhoan'
                , width: 100
                , hidden: true 
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'nganhang'
                , index: 'dtkd.nganhang'
                , width: 100
                , hidden: true 
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
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

<div class="ui-layout-center ui-widget-content" id="layout-center-phaichi">
    <uc1:jqGrid  ID="grid_ds_phaichi"
            Caption="Phải Chi" 
            SortName="tc.ngaytao" 
			FilterToolbar="true"
            UrlFileAction="Controllers/PhaiChiController.ashx" 
            ColNames="['c_thuchi_id', 'Trạng Thái', 'Số Phiếu', 'Mã đối tác', 'Ngày lập phiếu'
                ,  'Người Giao Nộp', 'Ngày Giao Nộp', 'Số Tiền'
                , 'Đồng Tiền', 'Tỷ Giá', 'Loại Phiếu'
                , 'Mô tả', 'Số Chứng Từ', 'Quy Đổi VND'
                , 'TK Quỹ'
                , 'Tổng chi tiết các khoản', 'Tổng gồm chi phí', 'Còn lại khi trừ các khoản'
                , 'Loại Chứng Từ'
                , 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            FuncActive="funcActiveChi"
			InPhieuChi="inphieuchi1"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, 
            {
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
	            errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
	            },
	            afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , afterShowForm:updateQuyDoiPhaiChi
            
            } ); }"
            ColModel = "[
            {
                fixed: true, name: 'c_thuchi_id'
                , index: 'tc.c_thuchi_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 name: 'md_trangthai_id'
                 , index: 'tc.md_trangthai_id'
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
                 , index: 'tc.sophieu'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'md_doitackinhdoanh_id'
                 , index: 'dtkd.ma_dtkd'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getSelectOptionAll' }
            },
            {
                 fixed: true, name: 'ngaylapphieu'
                 , index: 'tc.ngaylapphieu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            }, 
            {
                 fixed: true, name: 'nguoi_giaonop'
                 , index: 'tc.nguoi_giaonop'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            }, 
            {
                 fixed: true, name: 'ngay_giaonop'
                 , index: 'tc.ngay_giaonop'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },			
            {
                 fixed: true, name: 'sotien'
                 , index: 'tc.sotien'
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
                 , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption', 
                    dataEvents:[
                        {  
                            type:'change',
                            fn: function(e){
                                var v = $(e.target).val();
                                SetTyGiaPhaiChi(v);
                            }
                        }
                    ] }
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
                 , hidden: false
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
                 , hidden: false
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
                , editable:false
                , edittype: 'checkbox'
                , editoptions:{ defaultValue: 'True' }
                , editrules:{ edithidden : true }
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            Height = "170"
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
	            },
	            afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , afterShowForm: function(){ 
                    SetTyGiaPhaiChi('');
                    updateQuyDoiPhaiChi();
                }
	        "
	        EditFormOptions ="
	            beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
	            errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
	            },
	            afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
                , afterShowForm: updateQuyDoiPhaiChi
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
			                if($('#grid_ds_chitietphaichi').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_chitietphaichi').jqGrid('setGridParam',{url:'Controllers/ChiTietPhaiChiController.ashx?&tcId='+ids,page:1});
				                $('#grid_ds_chitietphaichi').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_chitietphaichi').jqGrid('setGridParam',{url:'Controllers/ChiTietPhaiChiController.ashx?&tcId='+ids,page:1});
			                $('#grid_ds_chitietphaichi').jqGrid().trigger('reloadGrid');			
		                } }"
            runat="server" />
</div>

<style type="text/css">
    #vu-css tr td select 
    {
        margin: 0px 0px 0px 9px;
    }
    

    #ctd_dongtien input
    {
        margin: 0px 0px 0px 9px;
    }

    #ctd_tygia input
    {
        margin: 0px 0px 0px 9px;
    }

    #vu-css tr td
    {
        padding:0px
    }
    #chitudong_grid_ds_phaichi:hover
    {
        padding:0px;
        border: 1px solid #f5ad66;
        background: #f5f0e5 url(images/ui-bg_glass_100_f5f0e5_1x400.png) 50% 50% repeat-x;
        font-weight: normal;
        color: #a46313;
    }
</style>

<script type="text/javascript">
    $('#grid_ds_phaichi').jqGrid('navButtonAdd', '#grid_ds_phaichi-pager', {
        caption: ""
            , title: "Chi tự động"
            , buttonicon: "ui-icon-circle-plus"
            , position: "first"
            , onClickButton: Chitudong
    });

    createRightPanel('grid_ds_phaichi');
    createRightPanel('grid_ds_chitietphaichi');
</script>

