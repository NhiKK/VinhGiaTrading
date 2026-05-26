<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-kehoach-xuathang.aspx.cs" Inherits="inc_kehoach_xuathang" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    // phân quyền theo nhóm
    String manv = UserUtils.getUser(Context);
    nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
    String strAccountSelect = "";
    System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
    strAccountSelect += "<select>";
    foreach (String item in lstAccount)
    {
        strAccountSelect += String.Format("<option value=\"{0}\">{0}</option>", item);
    }
    strAccountSelect += "</select>";
%>
<style type="text/css">
	td.nxh_xxn{
		font-weight:bold !important;
		color: red;
	}
	
	td.xnbb_dnow{
		font-weight:bold !important;
		color: red;
	}
	
	td.xnbbHT_dnow{
		font-weight:bold !important;
		color: blue;
	}
</style>
<script>
	function nxh_xxn(cellvalue, options, rowObject) {
		setTimeout(function(){
		var nxh = $('#grid_ds_kehoachxuathang').jqGrid('getCell', options.rowId, 'ngayxonghang');
		var xxn = $('#grid_ds_kehoachxuathang').jqGrid('getCell', options.rowId, 'ngayxonghang_xxn');
		var trangthai = $('#grid_ds_kehoachxuathang').jqGrid('getCell', options.rowId, 'md_trangthai_id');
		
		var arr_nxh = nxh.toString().split('/'), arr_xxn = xxn.toString().split('/');
		
		var date_nxh = new Date(arr_nxh[2] + '-' + arr_nxh[1] + '-' + arr_nxh[0]);

		var date_xxn = new Date(arr_xxn[2] + '-' + arr_xxn[1] + '-' + arr_xxn[0]);
		
		var date = (date_nxh - date_xxn)/1000/(3600*24);
		
		if (trangthai.indexOf("SOANTHAO") >= 0)
		{
			if( Math.round(date) > 3){
				if(cellvalue != '' & cellvalue != null) {
					$('#'+options.rowId).find('td:eq(' + (Number(options.pos) + 1) + ')').addClass("nxh_xxn");
				}
			}
		}
		
		
		}, 100);
		if(cellvalue == null)
			cellvalue = '';
		return cellvalue;
	}
	
	function xnbb_dnow(cellvalue, options, rowObject) {
		setTimeout(function() {
			var grid = $('#grid_ds_kehoachxuathang');
			var xnb = grid.jqGrid('getCell', options.rowId, 'ngayxacnhanbaobi');
			var sctDH = grid.jqGrid('getCell', options.rowId, 'c_donhang_id');
			var trangthai = grid.jqGrid('getCell', options.rowId, 'md_trangthai_id');
			var khongkhachkiem = grid.jqGrid('getCell', options.rowId, 'khongkhachkiem');
			khongkhachkiem = khongkhachkiem == null ? '' : khongkhachkiem.toString().toLowerCase();
			
			var arr_xnb = xnb.toString().split('/');
			
			var date_xnb = new Date(arr_xnb[2] + '-' + arr_xnb[1] + '-' + arr_xnb[0]);
			
			var date_n = new Date();
			
			var date = (date_xnb - date_n)/1000/(3600*24);

			if (trangthai.indexOf("SOANTHAO") >= 0)
			{
				var count = sctDH.toString().indexOf('_SR-');
				var rowCell = $('#'+options.rowId).find('td:eq(' + (Number(options.pos)) + ')');

				if(khongkhachkiem == 'true') {
					rowCell.removeClass('xnbb_dnow');
					rowCell.addClass("xnbbHT_dnow");
				}
				else if(Math.round(date) <= 7 & count <= -1) {
					if(cellvalue != '' & cellvalue != null) {
						rowCell.removeClass("xnbbHT_dnow");
						rowCell.addClass("xnbb_dnow");
					}
				}
			}
		}, 100);
		if(cellvalue == null)
			cellvalue = '';
		return cellvalue;
	}

    $(document).ready(function() {
        $("#datefrom").datepicker({
            defaultDate: "+1w",
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            onSelect: function(selectedDate) {
                $("#dateto").datepicker("option", "minDate", selectedDate);
            }
        });
        
        $("#dateto").datepicker({
            defaultDate: "+1w",
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            onSelect: function(selectedDate) {
                $("#datefrom").datepicker("option", "maxDate", selectedDate);
            }
        });

        $('.btn').button();

        $('#view').click(function() {
            var from = $('#datefrom').val();
            var to = $('#dateto').val();
			var type = $('#dateType').val();
            $('#grid_ds_kehoachxuathang').jqGrid('setGridParam', { url: 'Controllers/KeHoachXuatHangController.ashx?&dateFrom=' + from + '&dateTo=' + to + '&dateType=' + type, page: 1 });
            $('#grid_ds_kehoachxuathang').jqGrid().trigger('reloadGrid');
        });
    });
	
	
	function AuToKetThucKeHoach() {
        var msg = "";
        var id = "null";
		{
            msg = "Có phải bạn muốn tự động kết thúc tất cả kế hoạch có CBM <= 0?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-ketthuc-khxh\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-ketthuc-khxh').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/KeHoachXuatHangController.ashx?action=autostopkh&khxh=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-ketthuc-khxh').find('#dialog-caution').html(result);
                                $('#grid_ds_kehoachxuathang').jqGrid().trigger('reloadGrid');
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
        }
    }

    function KetThucKeHoach() {
        var msg = "";
        var id = $('#grid_ds_kehoachxuathang').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Hãy chọn 1 kế hoạch mà bạn muốn kết thúc.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-ketthuc-khxh\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-ketthuc-khxh').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function() {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn kết thúc kế hoạch này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-ketthuc-khxh\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-ketthuc-khxh').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/KeHoachXuatHangController.ashx?action=stopkh&khxh=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-ketthuc-khxh').find('#dialog-caution').html(result);
                                $('#grid_ds_kehoachxuathang').jqGrid().trigger('reloadGrid');
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
        }
    }
    

    function activeKHXH() {
        var msg = "";
        var id = $('#grid_ds_kehoachxuathang').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Bạn hãy chọn 1 đơn hàng mà bạn muốn hiệu lực.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-po\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-active-po').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function() {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Hiệu lực kế hoạch xuất hàng?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-kh\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-kh').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/KeHoachXuatHangController.ashx?action=activekh&khxh=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-active-kh').find('#dialog-caution').html(result);
                                $('#grid_ds_kehoachxuathang').jqGrid().trigger('reloadGrid');
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
        }
    }

    $('#print-kehoach').click(function() {
		var id = $('#grid_ds_kehoachxuathang').jqGrid('getGridParam', 'selrow');
        var from = $('#datefrom').val();
        var to = $('#dateto').val();
		var type = $('#dateType').val();
		
        var name = 'dialog-print-kehoach';
        var title = 'Trích xuất dữ liệu';
        var content = `<table style="width:100%">
                                <tr>
                                    <td>
										<input id="printRequired" type="radio" name="rdoPrintType" value="printRequired" checked="checked" /> 
										<label for="printRequired">In kế hoạch</label>
										<input id="printRequired2" type="radio" name="rdoPrintType" value="printRequired2" /> 
										<label for="printRequired2">In kế hoạch excel</label>
                                        <fieldset style="padding:10px">
                                        <input type="checkbox" id="chkOnlyUser" name="chkOnlyUser" />
                                        <label for="chkOnlyUser">Lọc theo người lập</label><br/>
                                        <span id="lstuser"><%= strAccountSelect %></span><br/><br/>
										<input type="checkbox" id="chkPartner" name="chkPartner" />
                                        <label for="chkPartner">Lọc theo nhà cung ứng</label>
										<br/><span id="lstpartner"></span>
										<br/><table style="margin: 10px 0 0 0px;"><tr><td><input type="checkbox" name="ST" id="ST" /></td>
                                        <td>Soạn thảo</td>
                                        <td><input type="checkbox" name="HL" id="HL" /></td>
                                        <td>Hiệu lực</td>
                                        <td><input type="checkbox" name="KT" id="KT" /></td>
                                        <td>Kết thúc</td></tr></table>
										</fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
										<input id="printProInfo" type="radio" name="rdoPrintType" value="printProInfo"/> <label for="printProInfo">In thông tin sản phẩm</label>
                                    </td>
                                </tr>
								<tr>
									<td>
										<input id="printYC" type="radio" name="rdoPrintType" value="printYC" /> <label for="printYC">In yêu cầu xuất hàng</label>
									</td>
								</tr>
                        </table>`;
        $.get("Controllers/PartnerController.ashx?action=getoption&isncc=1",
            function(result) {
                $('#lstpartner').append(result)
            });

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
            modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: [
                {
                    id: "btn-print-ok",
                    text: "In",
                    click: function() {
                        var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-kehoach').val();
                        var useUser = $('input[name=chkOnlyUser]:checked', '#dialog-print-kehoach').val();
                        var usePartner = $('input[name=chkPartner]:checked', '#dialog-print-kehoach').val();
						
						 //trang thai
                        var tt_ST = $('input[name=ST]:checked', '#dialog-print-kehoach').val();
                        var tt_HL = $('input[name=HL]:checked', '#dialog-print-kehoach').val();
                        var tt_KT = $('input[name=KT]:checked', '#dialog-print-kehoach').val();
						
                        var md_doitackinhdoanh_id = $('#lstpartner select').val();
                        var manv = $('#lstuser select').val();
                        var printURL = "PrintControllers/";
                        var windowSize = "width=700,height=700,scrollbars=yes";
                        if (typeof printType != 'undefined') {
                            if (printType == "printRequired" | printType == "printRequired2") {
                                if (from && to) {
									if(printType == "printRequired")
										printURL += "InKeHoachXuatHang/";
									else
										printURL += "InKeHoachXuatHang/KHXH_Excel.aspx";
									
                                    window.open(printURL + "?from=" + from + "&to=" + to
                                        + (typeof usePartner != 'undefined' ? ("&md_doitackinhdoanh_id=" + md_doitackinhdoanh_id) : '')
                                        + (typeof useUser != 'undefined' ? ("&manv=" + manv) : '')
                                        + "&dateType=" + type
										+ "&tt_ST=" + tt_ST
                                        + "&tt_HL=" + tt_HL
                                        + "&tt_KT=" + tt_KT
										, "In Kế Hoạch Xuất Hàng", windowSize);

                                    /*if (typeof usePartner != 'undefined') {
                                        window.open(printURL + "?from=" + from + "&to=" + to + "&md_doitackinhdoanh_id=" + md_doitackinhdoanh_id + "&dateType=" + type, "In Kế Hoạch Xuất Hàng", windowSize);
                                    } else {
                                        window.open(printURL + "?from=" + from + "&to=" + to + "&dateType=" + type, "In Kế Hoạch Xuất Hàng", windowSize);
                                    }*/
                                } else {
                                    alert('Bạn chưa chọn "Từ Ngày" hoặc "Đến Ngày"!');
                                }
                            }else if(printType == "printProInfo"){
								printURL += "InThongTinSanPhamKHXH/";
								if(id != null)
								{
									window.open(printURL + "?c_kehoachxuathang_id=" + id, "In Kế Hoạch Xuất Hàng", windowSize);
								}else{
									alert('Chưa chọn kế hoạch cần in.!');
								}
							}else if (printType == "printYC"){
								printURL += "InYeuCauXuatHang/";
								if (id != null) {
									window.open(printURL + "?c_kehoachxuathang_id=" + id, "In Phiếu Yêu Cầu Xuất", windowSize);
								} else {
									alert('Chưa chọn kế hoạch cần in.!');
								}
							}
                        } else {
                            alert('Hãy chọn cách in.!');
                        }
                    }
                }
                , {
                    id: "btn-print-close",
                    text: "Thoát",
                    click: function() {
                        $(this).dialog("destroy").remove();
                    }
                }
                ]
        });
    });

    function dupEditKeHoachXH(rowid) {
        $('#edit_grid_ds_kehoachxuathang').click();
    };
</script>



    <table class="ui-widget-header" style="width:100%; height:40px">
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width:50px"></td>
                        <td style="width:80px">Từ Ngày</td>
                        <td><input id ="datefrom" /></td>
                        <td style="width:50px"></td>
                        <td style="width:80px">Đến Ngày</td>
                        <td><input id ="dateto" /></td>
                        <td style="width:50px"></td>
						<td>
							<select id="dateType" style="padding: 2px; margin-right: 20px;">
								<option value="0">Lọc theo shipmenttime (KH)</option>
								<option value="1">Lọc theo ngày xong hàng</option>
							</select>
						</td>
						
                        <td><button id="view" class="btn">Xem Kế Hoạch</button></td>
                        <td><button id="print-kehoach" class="btn">In</button></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <uc1:jqGrid  ID="grid_ds_kehoachxuathang" 
            SortName="c_kehoachxuathang_id" 
            UrlFileAction="Controllers/KeHoachXuatHangController.ashx" 
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 105);
                var gridData = jQuery('#grid_ds_kehoachxuathang');
                var ids = gridData.jqGrid('getDataIDs');
                for (var i = 0, il = ids.length; i &lt; il; i++) {
                    var ret = gridData.jqGrid('getRowData', ids[i], true);
                }
                createRightPanel('grid_ds_kehoachxuathang'); 
            "
            FuncActive="activeKHXH"
            FuncKetThuc="KetThucKeHoach"
            FuncAutoKetThuc="AuToKetThucKeHoach"
            ColNames="['c_kehoachxuathang_id','TT','Khách Hàng', 'Đơn Hàng'
                    , 'Danh Sách Đặt Hàng', 'Nhà Cung Ứng', 'CL Hàng', 'CBM', 'CBM Còn Lại', 'Shipment Time(P/O)'
                    , 'Shipment Time(KH)', 'Ngày Xong Hàng', 'Ngày Xong Hàng(D/C)', 'Tem,Bao Bì Trễ Nhất', 'KO Tem'
                    , 'Ngày Xác Nhận Tem', 'KO Bao Bì', 'Ngày Xác Nhận Bao Bì'
					, 'Ngày Kiểm Hàng', 'Khách Không Kiểm Hàng', 'Ngày Khách Kiểm Hàng', 'Kết quả kiểm hàng', 'Ngày Vận Đơn(ETD)'
					, 'Nhân Viên Chứng Từ', 'Người Lập DSDH', 'Trễ(NV)', 'Trễ(NCU)'
					, 'Ngày Mở Tờ Khai'
                    , 'Tháng', 'Năm',  'Ghi Chú'
                    , 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật'
                    , 'Người Cập Nhật', 'Mô Tả', 'Hoạt Động']"
            RowNumbers="true"
            OndblClickRow = "dupEditKeHoachXH"
            ColModel = "[
            {
                fixed: true, name: 'c_kehoachxuathang_id'
                , index: 'c_kehoachxuathang_id'
                , width: 100
                , editable:true
                , edittype: 'text'
                , key: true
                , hidden:true
				, formoptions: { colpos: 1, rowpos: 1}
            },
            {
                 fixed: true, name: 'md_trangthai_id'
                 , index: 'kh.md_trangthai_id'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { readonly: 'readonly' }
                 , formatter: 'imagestatus'
                 , align:'center'
                 , search : true
                 , stype : 'select'
                 , searchoptions:{sopt:['in'], value:'SOANTHAO,HIEULUC:SOẠN THẢO & HIỆU LỰC;:[ALL];SOANTHAO:SOẠN THẢO;HIEULUC:HIỆU LỰC;KETTHUC:KẾT THÚC' }
				 , formoptions: { colpos: 1, rowpos: 2}
            },
			{
                fixed: true, name: 'khachhang'
                , index: 'khachhang.ma_dtkd'
                , width: 100
                , editable: false
                , edittype: 'text'
                , editoptions: { readonly: 'readonly' }
				, formoptions: { colpos: 1, rowpos: 3}
            },
            {
                fixed: true, name: 'c_donhang_id'
                , index: 'dh.sochungtu'
                , width: 100
                , editable: false
                , edittype: 'text'
				, formoptions: { colpos: 1, rowpos: 4}
            },
			{
                 fixed: true, name: 'c_danhsachdathang_id'
                 , index: 'kh.c_danhsachdathang_id'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { readonly: 'readonly' }
				 , formoptions: { colpos: 1, rowpos: 5 }
            },
            {
                fixed: true, name: 'md_doitackinhdoanh_id'
                , index: 'dtkd.ma_dtkd'
                , width: 70
                , editable: true
                , edittype: 'text'
                , editoptions: { readonly: 'readonly' }
				, formoptions: { colpos: 1, rowpos: 6 }
            },
            {
                 fixed: true, name: 'chungloaihang'
                 , index: 'kh.chungloaihang'
                 , editable: true
                 , width: 50
                 , edittype: 'textarea'
				, formoptions: { colpos: 1, rowpos: 7}				 
            },
            {
                 fixed: true, name: 'cbm'
                 , index: 'kh.cbm'
                 , editable: true
                 , width: 50
                 , edittype: 'text'   
                 , editoptions: { readonly: 'readonly' }              
                 , align:'right'
				 , formoptions: { colpos: 1, rowpos: 8}
            },
            {
                 fixed: true, name: 'cbm_conlai'
                 , index: 'kh.cbm'
                 , editable: true
                 , width: 50
                 , edittype: 'text'   
                 , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 3, prefix: ''}   
                 , align:'right'
				 , formoptions: { colpos: 1, rowpos: 9}
            },
            {
                 fixed: true, name: 'shipmenttime'
                 , index: 'kh.shipmenttime'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { readonly: 'readonly' }
				 , formoptions: { colpos: 1, rowpos: 10}
            },
            {
                 fixed: true, name: 'ngaygiaohang'
                 , index: 'kh.ngaygiaohang'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				 , hidden: true
				 , formoptions: { colpos: 1, rowpos: 11}
            },
			{
                 fixed: true, name: 'ngayxonghang_xxn'
                 , index: 'kh.ngayxonghang_xxn'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
				 , editoptions: { readonly: 'readonly' }
				 , formatter: nxh_xxn
				 , formoptions: { colpos: 1, rowpos: 12}
            },
            {
                 fixed: true, name: 'ngayxonghang'
                 , index: 'kh.ngayxonghang'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				 , formoptions: { colpos: 1, rowpos: 13}
            },
			{
                 fixed: true, name: 'ngayxacnhanbaobi'
                 , index: 'kh.ngayxacnhanbaobi'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				 , formoptions: { colpos: 1, rowpos: 18}
				 , formatter: xnbb_dnow
            },
            {
                fixed: true, name: 'khongtem'
                , index: 'kh.khongtem'
                , width: 50
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
				, formoptions: { colpos: 1, rowpos: 14}
            },
            {
                 fixed: true, name: 'ngayxacnhantem'
                 , index: 'kh.ngayxacnhantem'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				 , formoptions: { colpos: 1, rowpos: 15}
            },
            {
                fixed: true, name: 'khongbaobi'
                , index: 'kh.khongbaobi'
                , width: 50
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
				, formoptions: { colpos: 1, rowpos: 16}
            },           
			{
                 fixed: true, name: 'ngaytrenhat'
                 , index: 'P.ngaytrenhat'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
				 , align:'right'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				 , sortable: true
				 , formoptions: { colpos: 1, rowpos: 17}
            },
            {
                 fixed: true, name: 'ngaykiemhang'
                 , index: 'kh.ngaykiemhang'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				 , hidden: true
				 , formoptions: { colpos: 1, rowpos: 19}
            },
            {
                fixed: true, name: 'khongkhachkiem'
                , index: 'kh.khongkhachkiem'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
				, hidden: true
				, formoptions: { colpos: 1, rowpos: 20}
            },
            {
                 fixed: true, name: 'ngaykhachkiem'
                 , index: 'kh.ngaykhachkiem'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				 , hidden: true
				 , formoptions: { colpos: 1, rowpos: 21}
            },
			{
                 fixed: true, name: 'ketquakiemhang'
                 , index: 'kh.ketquakiemhang'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
				 , hidden: true
				 , formoptions: { colpos: 1, rowpos: 22}
            },
			{
                 fixed: true, name: 'etd'
                 , index: 'P.etd'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { readonly: 'readonly' }
				 , formoptions: { colpos: 1, rowpos: 24}
            },
			{
                 fixed: true, name: 'nhanvienct'
                 , index: 'kh.nhanvienct'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
				 , formoptions: { colpos: 1, rowpos: 23}
            },
			{
                 fixed: true, name: 'nguoilap'
                 , index: 'nv.hoten'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
				 , formoptions: { colpos: 1, rowpos: 25}
            },
			{
                 fixed: true, name: 'tre_nv'
                 , index: 'P.tre_nv'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
				 , formoptions: { colpos: 1, rowpos: 26}
            },
			{
                 fixed: true, name: 'tre'
                 , index: 'P.tre'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
				 , formoptions: { colpos: 1, rowpos: 27}
            },
            {
                 fixed: true, name: 'ngayxuathang'
                 , index: 'kh.ngayxuathang'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
				 , hidden: true
				 , formoptions: { colpos: 1, rowpos: 28}
            },
            {
                 fixed: true, name: 'thang'
                 , index: 'kh.thang'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , hidden: true
				 , formoptions: { colpos: 1, rowpos: 29}
            },
            {
                 fixed: true, name: 'nam'
                 , index: 'kh.nam'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , hidden: true
				 , formoptions: { colpos: 1, rowpos: 29}
            },
            {
                 fixed: true, name: 'ghichu'
                 , index: 'kh.ghichu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
				 , formoptions: { colpos: 1, rowpos: 30}
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'kh.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
				, formoptions: { colpos: 1, rowpos: 31}
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'kh.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
				, hidden: true
				, formoptions: { colpos: 1, rowpos: 32}
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'kh.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
				, formoptions: { colpos: 1, rowpos: 33}
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'kh.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
				, formoptions: { colpos: 1, rowpos: 34}
            },
            {
                fixed: true, name: 'mota'
                , index: 'kh.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: true
				, formoptions: { colpos: 1, rowpos: 35}
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'kh.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
				, formoptions: { colpos: 1, rowpos: 36}
            }
            ]"
            FilterToolbar="true"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ViewFormOptions = "width:500
                        , beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                        }"
            EditFormOptions = "width:500
                        , beforeShowForm: function (formid) {
							$('#ngayxacnhanbaobi', formid).change(function(){
								$('#khongkhachkiem', formid).prop('checked', true);
							});
                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                        }
                        , afterSubmit: function(rs, postdata) {
                          return showMsgDialog(rs);
                        }"
            Height = "420"
            runat="server" />

