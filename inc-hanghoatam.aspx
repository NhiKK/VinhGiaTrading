<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-hanghoatam.aspx.cs" Inherits="inc_hanghoatam" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
		 setTimeout(function () {
            $('#grid_ds_sanphamtam-pager_left table tr').append(
                '<td class="ui-pg-button ui-corner-all" title="Chuyển trạng thái nhanh" id="ctt_grid_ds_sanphamtam">' +
                '<div class="ui-pg-div"><span onclick="ChangeStatusALL_HHT()" class="ui-icon ui-icon-circle-plus"></span></div><td>')
            , 5000
        });

        $('#lay-center-sanphamtam').parent().layout({
            south: {
                size: "40%"
                    , minSize: "0%"
                    , maxSize: "100%"
                    , onresize_end: function() {
                        var h = $("#lay-south-sanphamtam").height();

                        // sett height
                        $("#grid_ds_quycachdonggoitam").setGridHeight(h + 4 - 80);
                        $("#grid_ds_giafobtam").setGridHeight(h + 4 - 80);
                        $("#grid_ds_khachhangdocquyen_sptam").setGridHeight(h + 4 - 80);
                        $("#grid_ds_cangxuathangtam").setGridHeight(h + 4 - 80);
                    }
            }
            , center: {
                onresize_end: function() {
                    var h = $("#lay-center-sanphamtam").height();
                    var w = $("#lay-center-sanphamtam").width();
                    $("#grid_ds_sanphamtam").setGridHeight(h + 4 - 70);
                }
            }
        });
        $('#lay-south-sanphamtam').layout({
            center: {
                onresize_end: function() {
                var w = $("#tab-product-detailstam").width();
                    //set width
                $("#grid_ds_quycachdonggoitam").setGridWidth(w);
                $("#grid_ds_giafobtam").setGridWidth(w);
                $("#grid_ds_khachhangdocquyen_sptam").setGridWidth(w);
                $("#grid_ds_cangxuathangtam").setGridWidth(w);
                }
            },
            east: {
                size: 200
                , maxSize: "100%"
                , minSize: 0
            }
        });
        $('#tab-product-detailstam').tabs();
    });
	
	function ChangeStatusALL_HHT() {
        $('body').append("<div title=\"Thay đổi trạng thái cho sản phẩm\" id=\"dialog-changestatus\">" +
            "<div style='display:none' id='wait'><img style='width:30px; height:30px' src='iconcollection/loading.gif'/></div>" +
            "<div id=\"caution\"></div>" +
            "<table>" +
                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"nhd\" value=\"NHD\" name=\"rdoStatus\" checked=\"checked\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"dhd\">Là sản phẩm ngưng hoạt động!</label>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"hhh\" value=\"HHH\" name=\"rdoStatus\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"hhh\">Hủy hàng hóa</label>" +
                    "</td>" +
                "</tr>" +
            "</table>" +
        "</div>");

        $("#dialog-changestatus").dialog({
            modal: true
            , open: function (event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
                , buttons: [
                {
                    id: "btn-change",
                    text: "Thay đổi",
                    click: function () {
                        var statusType = $('input[name=rdoStatus]:checked', '#dialog-changestatus').val();
                        if (typeof statusType != 'undefined') {
							$("#wait").css("display", "block");
                            $("#btn-change").button("disable");
                            $("#btn-close").button("disable");
                            $.post('Controllers/ProductPauseController.ashx?action=updatestatusALL_NHD&trangthai=' + statusType, 
							{ filters : $('#grid_ds_sanphamtam').getGridParam("postData").filters, trangthai_ht: 'HHT' },
							function (result) {
								$("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                                $("#dialog-changestatus").find("#caution").append(result);
                                $('#grid_ds_sanphamtam').jqGrid().trigger('reloadGrid');
                            });
                        } else {
                            alert('Chưa chọn trạng thái cho sản phẩm.!');
                        }
                    }
                }
                , {
                    id: "btn-close",
                    text: "Thoát",
                    click: function () {
                        $(this).dialog("destroy").remove();
                    }
                }
                ]
        });
    }
	
    function showPicture(obj) {
        var id = $(obj).attr('id');
        $.get("Controllers/ProductController.ashx?action=getpicture&productId=" + id, function(result) {
            $.fancybox.open(jQuery.parseJSON(result), {
                padding: 0
            });
        });
    }
    
    function imageFormat(cellvalue, options, rowObject) {
        return '<img id="' + cellvalue + '" onclick="showPicture(this)" style="cursor:pointer" src="iconcollection/pro_icon.png" />';
    }
    
    function imageUnFormat(cellvalue, options, cell) {
        return $('img', cell).attr('src');
    }

    function ChangeStatus() {
        var md_sanpham_id = $('#grid_ds_sanphamtam').jqGrid('getGridParam', 'selrow');
        if (md_sanpham_id != null) {
            $('body').append("<div title=\"Thay đổi trạng thái cho sản phẩm\" id=\"dialog-changestatus\">" +
            "<div style='display:none' id='wait'><img style='width:30px; height:30px' src='iconcollection/loading.gif'/></div>" +
            "<div id=\"caution\"></div>"+
            "<table>" +
                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"moi\" value=\"MOI\" name=\"rdoStatus\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"moi\">Là sản phẩm mới!</label>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"dhd\" value=\"DHD\" name=\"rdoStatus\" checked=\"checked\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"dhd\">Là sản phẩm đang hoạt động!</label>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"nhd\" value=\"NHD\" name=\"rdoStatus\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"nhd\">Là sản phẩm ngưng hoạt động!</label>" +
                    "</td>" +
                "</tr>" +

            "</table>" +
        "</div>");

            $("#dialog-changestatus").dialog({
                modal: true
            , open: function(event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
                , buttons: [
                {
                    id: "btn-change",
                    text: "Thay đổi",
                    click: function() {
                        var statusType = $('input[name=rdoStatus]:checked', '#dialog-changestatus').val();
                        if (typeof statusType != 'undefined') {
                            $("#dialog-changestatus").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-change").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-changestatus").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get('Controllers/ProductController.ashx?action=updatestatus&md_sanpham_id=' + md_sanpham_id + '&trangthai=' + statusType, function(result) {
                                $("#dialog-changestatus").find("#caution").append(result);
                                $('#grid_ds_sanphamtam').jqGrid().trigger('reloadGrid');
                            });
                        } else {
                            alert('Chưa chọn trạng thái cho sản phẩm.!');
                        }
                    }
                }
                , {
                    id: "btn-close",
                    text: "Thoát",
                    click: function() {
                        $(this).dialog("destroy").remove();
                    }
                }
                ]
            });
        } else {
            alert('Chưa chọn sản phẩm.!');
        }
    }
	
	function funcPrintSP() {
        var poId = $('#grid_ds_sanphamtam').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-sp';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
                                '<tr>' +
                                    '<td>' +
                                        '<input id="printImportWebsite" type="radio" name="rdoPrintType" value="printImportWebsite" checked="checked" />' +
                                        '<label for="printImportWebsite">Chiết xuất thông tin import website</label>' +
                                    '</td>' +
                                '</tr>' +
								'<tr>' +
									'<td>' +
										'<fieldset style="padding:10px 0px"><legend><input id="printThongTin" type="radio" name="rdoPrintType" value="printThongTin" />' +
										'<label for="printThongTin">Chiết xuất thông tin sản phẩm</label></legend>' +
										'<label for="valueLike">Giá trị lọc:</label> <input type="text" id="valueLike" /></fieldset>' +
									'</td>' +
								'</tr>' +
								'<tr>' +
									'<td>' +
										'<fieldset style="padding:10px 0px"><legend><input id="printThongTinMore" type="radio" name="rdoPrintType" value="printThongTinMore" />' +
										'<label for="printThongTinMore">Chiết xuất thông tin sản phẩm (xls)</label></legend>' +
										'<label for="valueLikeMore">Giá trị lọc:</label> <input type="text" id="valueLikeMore" /></fieldset>' +
									'</td>' +
								'</tr>' +
                          '</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
            modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: [
                {
                    id: "btn-print-ok",
                    text: "In",
                    click: function () {
                        var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-sp').val();
                        var printURL = "PrintControllers/";
                        var windowSize = "width=700,height=700,scrollbars=yes";
                        if (typeof printType != 'undefined') {
                            if (printType == "printImportWebsite") {
                                if (poId != null) {
                                    printURL += "InThongTinSanPham/";
                                    window.open(printURL + "?md_sanpham_id=" + poId + "&trangthai=HHT", "In Thông Tin Sản Phẩm", windowSize);
                                } else {
                                    alert('Bạn chưa chọn sản phẩm cần in.!');
                                }
                            } else if (printType == "printThongTin") {
                                var valueLike = $('#valueLike').val();
                                if (valueLike != null) {
                                    printURL += "InThongTinSanPhamReport/";
                                    window.open(printURL + "?md_sanpham_id=" + valueLike + "&trangthai=HHT", "In Thông Tin Sản Phẩm", windowSize);
                                } else {
                                    alert('Bạn chưa chọn sản phẩm cần in.!');
                                }
                            } else if (printType == "printThongTinMore") {
                                var valueLike = $('#valueLikeMore').val();
                                if (valueLike != null) {
                                    printURL += "InThongTinSanPhamMore/";
                                    window.open(printURL + "?md_sanpham_id=" + valueLike + "&trangthai=HHT", "In Thông Tin Sản Phẩm", windowSize);
                                } else {
                                    alert('Bạn chưa chọn sản phẩm cần in.!');
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
                    click: function () {
                        $(this).dialog("destroy").remove();
                    }
                }
                ]
        });
    }
</script>

<DIV class="ui-layout-center ui-widget-content" id="lay-center-sanphamtam">
    <uc1:jqGrid  ID="grid_ds_sanphamtam" 
            SortName="ma_sanpham"
            SortOrder ="asc"
            UrlFileAction="Controllers/ProductFormController.ashx" 
			BtnPrint="funcPrintSP"
            ColNames="['md_sanpham_id',     'TT',     'H/A',     'Mã SP', 'Tên KD'
                      , 'Tên CN', 'Mô Tả TV', 'Mô Tả TA'
                      , 'Đơn Vị Tính',      'L(inch)',      'W(inch)'
                      , 'H(inch)',          'L(cm)',        'W(cm)'
                      , 'H(cm)',            'Tr.Lượng',  'DT', 'Nhóm NL'
                      , 'Loại SP'     ,    'NCU','Cảng Biển'
                      , 'Liên kết'   , 'Ngày Tạo'
                      , 'Người Tạo',        'Ngày Cập Nhật',    'Người Cập Nhật'
                      , 'Ghi chú',            'Hoạt động']"
            RowNumbers="true"
            FuncChangeStatus="ChangeStatus"
            FuncImportPicture = "function(){ add_tab('Upload Hình Ảnh', 'inc-upload-image.aspx') }"
            ColModel = "[
            {
                fixed: true, name: 'md_sanpham_id'
                , index: 'md_sanpham_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'trangthai'
                , index: 'trangthai'
                , width: 100
                , editable:false
                , formatter:'imagestatus'
                , align: 'center'
                , search: false
                , viewable: false
                , sortable: false
            },
            {
                fixed: true, name: 'picture'
                , index: 'picture'
                , width: 50
                , editable:false
                , formatter:imageFormat
                , align: 'center'
                , search: false
                , viewable: false
                , sortable: false
            },
            {
                 fixed: true, name: 'ma_sanpham'
                 , index: 'ma_sanpham'
                 , width: 100
                 , edittype: 'text'
                 , editable:true
                 , editrules:{ required:true }
            },
            {
                fixed: true, name: 'md_kieudang_id'
                , index: 'kd.ten_tv'
                , width: 100
                , edittype: 'select'
                , editable:true
                , hidden: true
                , editrules:{ required:true, edithidden:true }
                , editoptions: { dataUrl: 'Controllers/KieuDangSanPhamController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'md_chucnang_id'
                , index: 'cn.ten_tv'
                , width: 100
                , edittype: 'select'
                , editable:true
                , hidden: true 
                , editrules : { required:true, edithidden:true }
                , editoptions: { dataUrl: 'Controllers/ChucNangSanPhamController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'mota_tiengviet'
                , index: 'mota_tiengviet'
                , width: 200
                , edittype: 'textarea'
                , editable:false
            },
            {
                fixed: true, name: 'mota_tienganh'
                , index: 'mota_tienganh'
                , width: 200
                , edittype: 'textarea'
                , editable:false
            },
            {
                fixed: true, name: 'tendvt'
                , index: 'dvt.ten_dvt'
                , width: 100
                , align: 'center'
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/UnitProductController.ashx?action=getoption' }
                , stype:'select'
                , searchoptions:{ sopt:['eq'], dataUrl: 'Controllers/UnitProductController.ashx?action=getsearchoption' }
            },
            {
                fixed: true, name: 'l_inch'
                , index: 'l_inch'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden:true
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'w_inch'
                , index: 'w_inch'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden:true
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'h_inch'
                , index: 'h_inch'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden:true
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'l_cm'
                , index: 'l_cm'
                , width: 100
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'w_cm'
                , index: 'w_cm'
                , width: 100
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'h_cm'
                , index: 'h_cm'
                , width: 100
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'trongluong'
                , index: 'trongluong'
                , width: 100
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { required:true, number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'dientich'
                , index: 'dientich'
                , width: 100
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { required:true, number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'md_nhomnangluc_id'
                , index: 'nnl.hehang'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editrules : { required:true }
                , editoptions: { dataUrl: 'Controllers/NhomNangLucController.ashx?action=getoption' }
                , sortable: false
            },
            {
                fixed: true, name: 'md_chungloai_id'
                , index: 'cl.code_cl'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/ChungLoaiController.ashx?action=getoption' }
                , hidden: true 
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'tenncc'
                , index: 'tenncc'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=1' }
                , editrules:{ edithidden:true }
                , hidden: true 
            },
            {
                fixed: true, name: 'md_cangbien_id'
                , index: 'cb.ten_cangbien'
                , width: 100
                , edittype: 'select'
                , editable:false
                , editoptions: { dataUrl: 'Controllers/PortController.ashx?action=getoption' }
                , hidden: true 
            },
            {
                fixed: true, name: 'ghichu'
                , index: 'ghichu'
                , width: 100
                , edittype: 'textarea'
                , editable:true
                , hidden: false 
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'mota'
                , index: 'mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , editrules:{ edithidden:true }
                , hidden: false 
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            FilterToolbar = "true"
            ViewFormOptions = "width:900
                        , beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                        }"
            EditFormOptions = "width:900
                        , beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                        }
                        , afterSubmit: function(rs, postdata) {
                          return showMsgDialog(rs);
                        }"
            AddFormOptions=" width:900
                            , beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }
                            , afterSubmit: function(rs, postdata) {
                              return showMsgDialog(rs);
                            }
                           "
			DelFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
            }"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 8 - 75);"
            OnSelectRow = "function(ids) {
		                    if(ids == null) {
			                    ids=0;
			                    
			                    if($('#grid_ds_quycachdonggoitam').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_quycachdonggoitam').jqGrid('setGridParam',{url:'Controllers/QuyCachDongGoiController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_quycachdonggoitam').jqGrid().trigger('reloadGrid');
				                }
				                
				                if($('#grid_ds_giafobtam').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_giafobtam').jqGrid('setGridParam',{url:'Controllers/PriceFOBController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_giafobtam').jqGrid().trigger('reloadGrid');
				                }
				                
				                if($('#grid_ds_gianoitam').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_gianoitam').jqGrid('setGridParam',{url:'Controllers/PriceSaleController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_gianoitam').jqGrid().trigger('reloadGrid');
				                }
				                
				                if($('#grid_ds_khachhangdocquyen_sptam').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_khachhangdocquyen_sptam').jqGrid('setGridParam',{url:'Controllers/QuyDinhVoiKhachHangController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_khachhangdocquyen_sptam').jqGrid().trigger('reloadGrid');
				                } 
				                
				                if($('#grid_ds_cangxuathangtam').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_cangxuathangtam').jqGrid('setGridParam',{url:'Controllers/CangXuatHangController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_cangxuathangtam').jqGrid().trigger('reloadGrid');
				                } 
				                
				                if($('#grid_ds_bientautam').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_bientautam').jqGrid('setGridParam',{url:'Controllers/BienTauController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_bientautam').jqGrid().trigger('reloadGrid');
				                } 
				                
				                if($('#grid_ds_nhacungungmacdinhtam').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_nhacungungmacdinhtam').jqGrid('setGridParam',{url:'Controllers/NhaCungUngMacDinhController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_nhacungungmacdinhtam').jqGrid().trigger('reloadGrid');
				                }
				            } 
				            else 
				            {
			                    $('#grid_ds_quycachdonggoitam').jqGrid('setGridParam',{url:'Controllers/QuyCachDongGoiController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_quycachdonggoitam').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_giafobtam').jqGrid('setGridParam',{url:'Controllers/PriceFOBController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_giafobtam').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_gianoitam').jqGrid('setGridParam',{url:'Controllers/PriceSaleController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_gianoitam').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_khachhangdocquyen_sptam').jqGrid('setGridParam',{url:'Controllers/QuyDinhVoiKhachHangController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_khachhangdocquyen_sptam').jqGrid().trigger('reloadGrid');		
			                    
			                    $('#grid_ds_cangxuathangtam').jqGrid('setGridParam',{url:'Controllers/CangXuatHangController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_cangxuathangtam').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_bientautam').jqGrid('setGridParam',{url:'Controllers/BienTauController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_bientautam').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_nhacungungmacdinhtam').jqGrid('setGridParam',{url:'Controllers/NhaCungUngMacDinhController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_nhacungungmacdinhtam').jqGrid().trigger('reloadGrid');
			                    
			                    var ma_sanpham = $(this).jqGrid('getCell', ids, 'ma_sanpham');
			                    $.get('Controllers/ProductController.ashx?action=getSinglePicture&ma_sanpham=' + ma_sanpham, function(data) {
                                      $('#img-product').attr('src', data);
                                });
		                    } 
		                }"
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"         
            runat="server" />
</DIV>
<DIV class="ui-layout-south" id="lay-south-sanphamtam"> 
    <div class="ui-layout-center" id="tab-product-detailstam">
        <ul>
			    <li><a href="#nav-tabs-1">Quy Cách Ðóng Gói</a></li>
			    <li><a href="#nav-tabs-2">Giá FOB</a></li>
			    <li><a href="#nav-tabs-3">Giá Nội</a></li>
			    <li><a href="#nav-tabs-4">Quy Ðịnh Với Khách Hàng</a></li>
			    <li><a href="#nav-tabs-5">Cảng Biển</a></li>
			    <li><a href="#nav-tabs-6">Biến Tấu</a></li>
			    <li><a href="#nav-tabs-7">NCU</a></li>
		</ul>  
		    <div id="nav-tabs-1">
		        <uc1:jqGrid  ID="grid_ds_quycachdonggoitam" 
                SortName="md_donggoisanpham_id" 
                UrlFileAction="Controllers/QuyCachDongGoiController.ashx" 
                ColNames="['md_donggoisanpham_id', 'Tên Ðóng Gói', 'Mã SP'
                        , 'SL Inner', 'ÐVT Inner', 'L1', 'W1', 'H1'
                        , 'SL Outer', 'ÐVT Outer', 'L2', 'W2', 'H2', 'V2'
                        , 'SL gói/ctn40', 'VD', 'VN', 'VL'
                        , 'Ghi Chú Vách Ngăn', 'Số Lượng', 'Mặc Ðịnh'
                        , 'Ngày Tạo' , 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật'
                        , 'Mô tả', 'Hoạt động']"
                RowNumbers="true"
                OndblClickRow = "function(rowid) { $(this).jqGrid('viewGridRow', rowid); }"
                ColModel = "[
                {
                    fixed: true, name: 'md_donggoisanpham_id'
                    , index: 'md_donggoisanpham_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                     fixed: true, name: 'ten_donggoi'
                     , index: 'ten_donggoi'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'ma_sanpham'
                     , index: 'ma_sanpham'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'sl_inner'
                     , index: 'sl_inner'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'dvtinner'
                     , index: 'dg.dvt_inner'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'center'
                },
                {
                     fixed: true, name: 'l1'
                     , index: 'l1'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'w1'
                     , index: 'w1'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'h1'
                     , index: 'h1'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'sl_outer'
                     , index: 'sl_outer'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , formoptions:{colpos:2,rowpos:4}
                     , align:'right'
                },
                {
                     fixed: true, name: 'dvtouter'
                     , index: 'dg.dvt_outer'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , formoptions:{colpos:2,rowpos:5}
                     , align:'center'
                },
                {
                     fixed: true, name: 'l2_mix'
                     , index: 'l2_mix'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , formoptions:{colpos:2,rowpos:6}
                     , align:'right'
                },
                {
                     fixed: true, name: 'w2_mix'
                     , index: 'w2_mix'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , formoptions:{colpos:2,rowpos:7}
                     , align:'right'
                },
                {
                     fixed: true, name: 'h2_mix'
                     , index: 'h2_mix'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , formoptions:{colpos:2,rowpos:8}
                     , align:'right'
                },
                {
                     fixed: true, name: 'v2'
                     , index: 'v2'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , align:'right'
                },
                {
                     fixed: true, name: 'soluonggoi_ctn'
                     , index: 'soluonggoi_ctn'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , align:'right'
                },
                {
                     fixed: true, name: 'vd'
                     , index: 'vd'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'vn'
                     , index: 'vn'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'vl'
                     , index: 'vl'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'ghichu_vachngan'
                     , index: 'ghichu_vachngan'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , formoptions:{colpos:2,rowpos:16}
                },
                {
                     fixed: true, name: 'soluong'
                     , index: 'soluong'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , hidden: true
                     , formoptions:{colpos:2,rowpos:17}
                     , align:'right'
                },
                {
                     fixed: true, name: 'macdinh'
                     , index: 'macdinh'
                     , align: 'center'
                     , editable: true
                     , width: 100
                     , editoptions:{ value:'True:False', defaultValue: 'True' }
                     , edittype: 'checkbox'
                     , formatter: 'checkbox'
                     , formoptions:{colpos:2,rowpos:18}
                },
                {
                    fixed: true, name: 'ngaytao'
                    , index: 'ngaytao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'nguoitao'
                    , index: 'nguoitao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'ngaycapnhat'
                    , index: 'ngaycapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'nguoicapnhat'
                    , index: 'nguoicapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'mota'
                    , index: 'mota'
                    , width: 100
                    , editable:true
                    , edittype: 'textarea'
                    , hidden : true
                },
                {
                    fixed: true, name: 'hoatdong', hidden: true
                    , index: 'hoatdong'
                    , width: 100
                    , editable:true
                    , edittype: 'checkbox'
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                }
                ]"
                Height = "420"
                ViewFormOptions="width:900
                                , beforeShowForm: function (formid) {
                                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                                } "
                GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);"
                runat="server" />
		    </div>
		    <div id="nav-tabs-2">
		        <uc1:jqGrid  ID="grid_ds_giafobtam"
                SortName="md_giasanpham_id" 
                UrlFileAction="Controllers/PriceFOBController.ashx" 
                ColNames="['md_giasanpham_id', 'Tên Phiên Bản','Tên Sản Phẩm', 'Giá'
                , 'Ðồng Tiền', 'Ngày Hiệu Lực', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                RowNumbers="true"
                ColModel = "[
                {
                    fixed: true, name: 'md_giasanpham_id'
                    , index: 'md_giasanpham_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                     fixed: true, name: 'ten_phienbangia'
                     , index: 'pbg.ten_phienbangia'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules: { required:true }
                },
                {
                    fixed: true, name: 'ma_sanpham'
                    , index: 'sp.ma_sanpham'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , editrules: { required:true }
                },
                {
                    fixed: true, name: 'gia'
                    , index: 'gia'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , align:'right'
                    , formatter:'currency'
                    , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
                },
                {
                    fixed: true, name: 'md_dongtien_id'
                    , index: 'dt.ma_iso'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , align:'center'
                },
                {
                    fixed: true, name: 'ngay_hieuluc'
                    , index: 'pbg.ngay_hieuluc'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                },
                {
                    fixed: true, name: 'ngaytao'
                    , index: 'gsp.ngaytao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true
                },
                {
                    fixed: true, name: 'nguoitao'
                    , index: 'gsp.nguoitao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true
                },
                {
                    fixed: true, name: 'ngaycapnhat'
                    , index: 'gsp.ngaycapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true
                },
                {
                    fixed: true, name: 'nguoicapnhat'
                    , index: 'gsp.nguoicapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true
                },
                {
                    fixed: true, name: 'mota'
                    , index: 'gsp.mota'
                    , width: 100
                    , editable:true
                    , edittype: 'textarea'
                    , hidden: true
                },
                {
                    fixed: true, name: 'hoatdong', hidden: true
                    , index: 'gsp.hoatdong'
                    , width: 100
                    , editable:true
                    , edittype: 'checkbox'
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                }
                ]"
                GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                ViewFormOptions="width:900
                                , beforeShowForm: function (formid) {
                                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                                } "
                runat="server" />
		    </div>
		    
		    
		    <div id="nav-tabs-3">
		        <uc1:jqGrid  ID="grid_ds_gianoitam"
                SortName="md_giasanpham_id" 
                UrlFileAction="Controllers/PriceFOBController.ashx" 
                ColNames="['md_giasanpham_id', 'Tên Phiên Bản','Tên Sản Phẩm', 'Giá'
                , 'Ðồng Tiền', 'Ngày Hiệu Lực', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                RowNumbers="true"
                ColModel = "[
                {
                    fixed: true, name: 'md_giasanpham_id'
                    , index: 'md_giasanpham_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                     fixed: true, name: 'ten_phienbangia'
                     , index: 'pbg.ten_phienbangia'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules: { required:true }
                },
                {
                    fixed: true, name: 'ma_sanpham'
                    , index: 'sp.ma_sanpham'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , editrules: { required:true }
                },
                {
                    fixed: true, name: 'gia'
                    , index: 'gia'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , align:'right'
                    , formatter:'currency'
                    , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
                },
                {
                    fixed: true, name: 'md_dongtien_id'
                    , index: 'dt.ma_iso'
                    , width: 100
                    , edittype: 'text'
                    , editable:true
                    , align:'center'
                },
                {
                    fixed: true, name: 'ngay_hieuluc'
                    , index: 'pbg.ngay_hieuluc'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                },
                {
                    fixed: true, name: 'ngaytao'
                    , index: 'gsp.ngaytao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true
                },
                {
                    fixed: true, name: 'nguoitao'
                    , index: 'gsp.nguoitao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true
                },
                {
                    fixed: true, name: 'ngaycapnhat'
                    , index: 'gsp.ngaycapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true
                },
                {
                    fixed: true, name: 'nguoicapnhat'
                    , index: 'gsp.nguoicapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden: true
                },
                {
                    fixed: true, name: 'mota'
                    , index: 'gsp.mota'
                    , width: 100
                    , editable:true
                    , edittype: 'textarea'
                    , hidden: true
                },
                {
                    fixed: true, name: 'hoatdong', hidden: true
                    , index: 'gsp.hoatdong'
                    , width: 100
                    , editable:true
                    , edittype: 'checkbox'
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                }
                ]"
                GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                ViewFormOptions="width:900
                                , beforeShowForm: function (formid) {
                                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                                } "
                runat="server" />
		    </div>
		    
		    
		    <div id="nav-tabs-4">
		        <uc1:jqGrid  ID="grid_ds_khachhangdocquyen_sptam"
                    SortName="hhdq.md_hanghoadocquyen_id" 
                    UrlFileAction="Controllers/QuyDinhVoiKhachHangController.ashx" 
                    ColNames="['hhdp.md_hanghoadocquyen_id' ,'Tên Đối Tác', 'Mã SP', 'Mã SP', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                    RowNumbers="true"
                    OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
                    
                    ColModel = "[
                    {
                        fixed: true, name: 'md_hanghoadocquyen_id'
                        , index: 'hhdq.md_hanghoadocquyen_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                        , key: true
                    },
                    {
                        fixed: true, name: 'md_doitackinhdoanh_id'
                        , index: 'dtkd.ma_dtkd'
                        , width: 100
                        , edittype: 'select'
                        , editable:true
                        , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=0' }
                    },
                    {
                        fixed: true, name: 'sanpham_id'
                        , index: 'sp.md_sanpham_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                    },
                    {
                        fixed: true, name: 'tensp'
                        , index: 'sp.ma_sanpham'
                        , width: 100
                        , edittype: 'text'
                        , editable: true
                        , hidden: true 
                        , editoptions: { 
                            dataInit : function (elem) { 
                                $(elem).combogrid({
                                    searchIcon: true,
                                    width: '480px',
                                    url: 'Controllers/ProductController.ashx?action=getcombogrid',
                                    colModel: [
                                        { 'columnName': 'md_sanpham_id', 'width': '0', 'label': 'ID', hidden:true }
                                      , { 'columnName': 'ma_sanpham', 'width': '50', 'label': 'Mã SP', 'align':'left'}
                                      , { 'columnName': 'mota_tiengviet', 'width': '50', 'label': 'Mô Tả TV' , 'align':'left'}],
                                      select: function(event, ui) {
                                            $(elem).val(ui.item.ma_sanpham);
                                            $('#sanpham_id').val(ui.item.md_sanpham_id);
                                            return false;
                                      }
                                });
                            } 
                        }
                    },
                    {
                        fixed: true, name: 'ngaytao'
                        , index: 'hhdq.ngaytao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoitao'
                        , index: 'hhdq.nguoitao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'ngaycapnhat'
                        , index: 'hhdq.ngaycapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoicapnhat'
                        , index: 'hhdq.nguoicapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'mota'
                        , index: 'hhdq.mota'
                        , width: 100
                        , editable:true
                        , edittype: 'textarea'
                    },
                    {
                    fixed: true, name: 'hoatdong', hidden: true
                    , index: 'hhdq.hoatdong'
                    , width: 100
                    , editable:true
                    , edittype: 'checkbox'
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                }
                    ]"
                    GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                    ViewFormOptions="width:900
                                    , beforeShowForm: function (formid) {
                                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                                    } "
                    Height = "420"
                    runat="server" />
            		   
		    </div>
		    <div id="nav-tabs-5">
		        <uc1:jqGrid  ID="grid_ds_cangxuathangtam"
                    SortName="cxh.md_cangxuathang_id" 
                    UrlFileAction="Controllers/CangXuatHangController.ashx" 
                    ColNames="['md_cangxuathang_id' , 'Mã SP', 'Tên Cảng Biển', 'Mặc Định', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                    RowNumbers="true"
                    ColModel = "[
                    {
                        fixed: true, name: 'md_cangxuathang_id'
                        , index: 'cxh.md_cangxuathang_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                        , key: true
                    },
                    {
                        fixed: true, name: 'md_sanpham_id'
                        , index: 'cxh.md_sanpham_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                    },
                    
                    {
                        fixed: true, name: 'md_cangbien_id'
                        , index: 'cxh.md_cangbien_id'
                        , width: 100
                        , edittype: 'select'
                        , editable:true
                        , editoptions: { dataUrl: 'Controllers/PortController.ashx?action=getoption' }
                    },
                    {
                        fixed: true, name: 'macdinh'
                        , index: 'cxh.macdinh'
                        , width: 100
                        , edittype: 'checkbox'
                        , editable:true
                        , editoptions:{ value:'True:False', defaultValue: 'True' }
                        , formatter: 'checkbox'
                        , align: 'center'
                    },
                    {
                        fixed: true, name: 'ngaytao'
                        , index: 'cxh.ngaytao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoitao'
                        , index: 'cxh.nguoitao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'ngaycapnhat'
                        , index: 'cxh.ngaycapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoicapnhat'
                        , index: 'cxh.nguoicapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'mota'
                        , index: 'cxh.mota'
                        , width: 100
                        , editable:true
                        , edittype: 'textarea'
                    },
                    {
                        fixed: true, name: 'hoatdong', hidden: true
                        , index: 'cxh.hoatdong'
                        , width: 100
                        , editable:true
                        , edittype: 'checkbox'
                        , align: 'center'
                        , editoptions:{ value:'True:False', defaultValue: 'True' }
                        , formatter: 'checkbox'
                    }
                    ]"
                    GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                    ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
                    ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
                    ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
                    Height = "420"
                    AddFormOptions="
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                        , beforeShowForm: function (formid) {
                            var masterId = $('#grid_ds_sanpham').jqGrid('getGridParam', 'selrow');
                            var forId = 'md_sanpham_id';
                            if(masterId == null)
                            {
                                $(formid).parent().parent().parent().dialog('destroy').remove();
                                alert('Hãy chọn một sản phẩm mới có thể tạo chi tiết.!');
                            }else{
                                $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                                formid.closest('div.ui-jqdialog').dialogCenter();
                            }
                        }
                        , beforeSubmit:function(postdata, formid){
                            var masterId = $('#grid_ds_sanpham').jqGrid('getGridParam', 'selrow');
                            var forId = 'md_sanpham_id';
                            if(masterId == null)
                            {
                                $(formid).parent().parent().parent().dialog('destroy').remove();
                                return [false,'Hãy chọn một sản phẩm mới có thể tạo chi tiết.!'];
                            }else{
                                postdata.md_sanpham_id = masterId;
                                return [true,'']; 
                            }
                        }"
                    EditFormOptions="
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    DelFormOptions="
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    ViewFormOptions="width:900
                                    , beforeShowForm: function (formid) {
                                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                                    } "
                    runat="server" />
		    </div>
		    
		    
		    
		    
		    
		    <div id="nav-tabs-6">
		        <uc1:jqGrid  ID="grid_ds_bientautam"
                    SortName="sp.ma_sanpham" 
                    UrlFileAction="Controllers/BienTauController.ashx" 
                    ColNames="['md_bientau_id' , 'Mã SP', 'Biến tấu', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                    RowNumbers="true"
                    OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
                    ColModel = "[
                    {
                        fixed: true, name: 'md_bientau_id'
                        , index: 'bt.md_bientau_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                        , key: true
                    },
                    {
                        fixed: true, name: 'ma_sanpham'
                        , index: 'sp.ma_sanpham'
                        , width: 100
                        , editable:true
                        , edittype: 'text'
                    },
                    
                    {
                        fixed: true, name: 'ma_sanpham_ref'
                        , index: 'bt.ma_sanpham_ref'
                        , width: 100
                        , edittype: 'text'
                        , editable:true
                    },
                    {
                        fixed: true, name: 'ngaytao'
                        , index: 'cxh.ngaytao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoitao'
                        , index: 'cxh.nguoitao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'ngaycapnhat'
                        , index: 'cxh.ngaycapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoicapnhat'
                        , index: 'cxh.nguoicapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'mota'
                        , index: 'cxh.mota'
                        , width: 100
                        , editable:true
                        , edittype: 'textarea'
                    },
                    {
                        fixed: true, name: 'hoatdong', hidden: true
                        , index: 'cxh.hoatdong'
                        , width: 100
                        , editable:true
                        , edittype: 'checkbox'
                        , align: 'center'
                        , editoptions:{ value:'True:False', defaultValue: 'True' }
                        , formatter: 'checkbox'
                    }
                    ]"
                    GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                    Height = "420"
                    ViewFormOptions="width:900
                                    , beforeShowForm: function (formid) {
                                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                                    } "
                    runat="server" />
		    </div>
		    
		    
		    <div id="nav-tabs-7">
		        <uc1:jqGrid  ID="grid_ds_nhacungungmacdinhtam"
                    SortName="ncu.md_nhacungungmacdinh_id" 
                    UrlFileAction="Controllers/NhaCungUngMacDinhController.ashx" 
                    ColNames="['md_nhacungungmacdinh_id' , 'Mã SP', 'NCU', 'Mặc Định', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                    RowNumbers="true"
                    ColModel = "[
                    {
                        fixed: true, name: 'md_nhacungungmacdinh_id'
                        , index: 'ncu.md_nhacungungmacdinh_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                        , key: true
                    },
                    {
                        fixed: true, name: 'md_sanpham_id'
                        , index: 'ncu.md_sanpham_id'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                    },
                    
                    {
                        fixed: true, name: 'md_doitackinhdoanh_id'
                        , index: 'ncu.md_doitackinhdoanh_id'
                        , width: 100
                        , edittype: 'select'
                        , editable:true
                        , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=1' }
                    },
                    {
                        fixed: true, name: 'macdinh'
                        , index: 'cxh.macdinh'
                        , width: 100
                        , edittype: 'checkbox'
                        , editable:true
                        , editoptions:{ value:'True:False', defaultValue: 'True' }
                        , formatter: 'checkbox'
                        , align: 'center'
                    },
                    {
                        fixed: true, name: 'ngaytao'
                        , index: 'cxh.ngaytao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoitao'
                        , index: 'cxh.nguoitao'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'ngaycapnhat'
                        , index: 'cxh.ngaycapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoicapnhat'
                        , index: 'cxh.nguoicapnhat'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'mota'
                        , index: 'cxh.mota'
                        , width: 100
                        , editable:true
                        , edittype: 'textarea'
                    },
                    {
                        fixed: true, name: 'hoatdong', hidden: true
                        , index: 'cxh.hoatdong'
                        , width: 100
                        , editable:true
                        , edittype: 'checkbox'
                        , align: 'center'
                        , editoptions:{ value:'True:False', defaultValue: 'True' }
                        , formatter: 'checkbox'
                    }
                    ]"
                    GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                    ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
                    ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
                    ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
                    Height = "420"
                    AddFormOptions="
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                        , beforeShowForm: function (formid) {
                            var masterId = $('#grid_ds_sanpham').jqGrid('getGridParam', 'selrow');
                            var forId = 'md_sanpham_id';
                            if(masterId == null)
                            {
                                $(formid).parent().parent().parent().dialog('destroy').remove();
                                alert('Hãy chọn một sản phẩm mới có thể tạo chi tiết.!');
                            }else{
                                $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                                formid.closest('div.ui-jqdialog').dialogCenter();
                            }
                        }
                        , beforeSubmit:function(postdata, formid){
                            var masterId = $('#grid_ds_sanpham').jqGrid('getGridParam', 'selrow');
                            var forId = 'md_sanpham_id';
                            if(masterId == null)
                            {
                                $(formid).parent().parent().parent().dialog('destroy').remove();
                                return [false,'Hãy chọn một sản phẩm mới có thể tạo chi tiết.!'];
                            }else{
                                postdata.md_sanpham_id = masterId;
                                return [true,'']; 
                            }
                        }"
                    EditFormOptions="
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    DelFormOptions="
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    ViewFormOptions="width:900
                                    , beforeShowForm: function (formid) {
                                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                                    } "
                    runat="server" />
		    </div>
    </div>
    <div class="ui-layout-east" style="text-align:center; background:#f4f0ec">
        <table style="width:100%; height:100%">
            <tr>
                <td>
                    <img id="img-product" style="width:200px; height:150px" src="images/products/fullsize/default.jpg" />
                </td>
            </tr>
        </table>
    </div>
</DIV>


