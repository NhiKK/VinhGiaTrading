<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-hanghoasoanthao.aspx.cs" Inherits="inc_hanghoasoanthao" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>

<style type="text/css">
    #ctt_grid_ds_sanphamsoanthao:hover
    {
        background-color:ActiveBorder
    }
    #ctt_grid_ds_sanphamsoanthao:active
    {
        cursor:wait
    }
</style>

<script>
    $(function () {
        setTimeout(function () {
            $('#grid_ds_sanphamsoanthao-pager_left table tr').append(
                '<td class="ui-pg-button ui-corner-all" title="Chuyển trạng thái nhanh" id="ctt_grid_ds_sanphamsoanthao">' +
                '<div class="ui-pg-div"><span onclick="ChangeStatusALL()" class="ui-icon ui-icon-circle-plus"></span></div><td>')
            , 5000
        });

        $('#lay-center-sanphamsoanthao').parent().layout({
            south: {
                size: "40%"
                    , minSize: "0%"
                    , maxSize: "100%"
                    , onresize_end: function() {
                        var h = $("#lay-south-sanphamsoanthao").height();

                        // sett height
                        $("#grid_ds_quycachdonggoi_soanthao").setGridHeight(h + 4 - 80);
                        $("#grid_ds_giafob_soanthao").setGridHeight(h + 4 - 80);
                        $("#grid_ds_khachhangdocquyen_soanthao").setGridHeight(h + 4 - 80);
                        $("#grid_ds_cangxuathang_soanthao").setGridHeight(h + 4 - 80);
                    }
            }
            , center: {
                onresize_end: function() {
                    var h = $("#lay-center-sanphamsoanthao").height();
                    var w = $("#lay-center-sanphamsoanthao").width();
                    $("#grid_ds_sanphamsoanthao").setGridHeight(h + 4 - 70);
                }
            }
        });
        $('#lay-south-sanphamsoanthao').layout({
            center: {
                onresize_end: function() {
                var w = $("#tab-product-detailssoanthao").width();
                    //set width
                $("#grid_ds_quycachdonggoi_soanthao").setGridWidth(w);
                $("#grid_ds_giafob_soanthao").setGridWidth(w);
                $("#grid_ds_khachhangdocquyen_soanthao").setGridWidth(w);
                $("#grid_ds_cangxuathang_soanthao").setGridWidth(w);
                }
            },
            east: {
                size: 200
                , maxSize: "100%"
                , minSize: 0
            }
        });
        $('#tab-product-detailssoanthao').tabs();
    });

    function ChangeStatusALL() {
        $('body').append("<div title=\"Thay đổi trạng thái cho sản phẩm\" id=\"dialog-changestatus\">" +
            "<div style='display:none' id='wait'><img style='width:30px; height:30px' src='iconcollection/loading.gif'/></div>" +
            "<div id=\"caution\"></div>" +
            "<table>" +
                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"dhdHHST\" value=\"DHD\" name=\"rdoStatus\" checked=\"checked\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"dhdHHST\">Là sản phẩm đang hoạt động!</label>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"nhdHHST\" value=\"NHD\" name=\"rdoStatus\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"nhdHHST\">Là sản phẩm ngưng hoạt động!</label>" +
                    "</td>" +
                "</tr>" +
            "</table>" +
        "</div>");

        $("#dialog-changestatus").dialog({
            modal: true
			, maxHeight: window.innerHeight - 150
            , open: function (event, ui) {
                //hide close button.
                //$(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            },
			close: function () {
                $(this).dialog('destroy').remove();
            },
			buttons: [
			{
				id: "btn-change",
				text: "Thay đổi",
				click: function () {
					var statusType = $('input[name=rdoStatus]:checked', '#dialog-changestatus').val();
					if (typeof statusType != 'undefined') {
						$("#dialog-changestatus").ajaxStart(function () {
							$("#wait").css("display", "block");
							$("#btn-change").button("disable");
							$("#btn-close").button("disable");
						});

						$("#dialog-changestatus").ajaxComplete(function () {
							$("#wait").css("display", "none");
							$("#btn-close span").text("Thoát");
							$("#btn-close").button("enable");
						});

						$.post('Controllers/ProductSOANTHAOController.ashx?action=updatestatusALL&trangthai=' + statusType, 
						{ filters : $('#grid_ds_sanphamsoanthao').getGridParam("postData").filters, trangthai_ht: 'SOANTHAO' },
						function (result) {
							$("#dialog-changestatus").find("#caution").append(result);
							$('#grid_ds_sanphamsoanthao').jqGrid().trigger('reloadGrid');
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
        var md_sanpham_id = $('#grid_ds_sanphamsoanthao').jqGrid('getGridParam', 'selrow');
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
				, maxHeight: window.innerHeight - 150
				, open: function(event, ui) {
					//hide close button.
					//$(this).parent().children().children('.ui-dialog-titlebar-close').hide();
				},
				close: function () {
					$(this).dialog('destroy').remove();
				},
                buttons: [
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

                            $.get('Controllers/ProductController.ashx?action=updatestatus&md_sanpham_id=' + md_sanpham_id + '&trangthai=' + statusType, function (result) {
                                $("#dialog-changestatus").find("#caution").append(result);
                                $('#grid_ds_sanphamsoanthao').jqGrid().trigger('reloadGrid');
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
        funcPrintSPPublic('grid_ds_sanphamsoanthao','SOANTHAO');
    }
	
 </script>

<DIV class="ui-layout-center ui-widget-content" id="lay-center-sanphamsoanthao">
    <uc1:jqGrid  ID="grid_ds_sanphamsoanthao" 
            SortName="ma_sanpham"
            SortOrder ="asc"
            UrlFileAction="Controllers/ProductSOANTHAOController.ashx" 
            BtnPrint="funcPrintSP"
            RowNumbers="true"
            FuncChangeStatus="ChangeStatus"
            FuncImportPicture = "function(){ add_tab('Upload Hình Ảnh', 'inc-upload-image.aspx') }"
            ColModel = "[
            {
                fixed: true, name: 'md_sanpham_id'
                , label: 'md_sanpham_id'
                , index: 'sp.md_sanpham_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'trangthai'
                , label: 'TT'
                , index: 'sp.trangthai'
                , width: 100
                , editable:false
                , formatter:'imagestatus'
                , align: 'center'
                , search: false
                , viewable: false
                , sortable: true
				, hidden: true 
            },
            {
                fixed: true, name: 'picture'
                , label: 'H/A'
                , index: 'sp.picture'
                , width: 50
                , editable:false
                , formatter:imageFormat
                , align: 'center'
                , search: false
                , viewable: false
                , sortable: false
				, hidden: true 
            },
            {
                 fixed: true, name: 'ma_sanpham'
                 , label: 'Mã SP'
                 , index: 'sp.ma_sanpham'
                 , width: 100
                 , edittype: 'text'
                 , editable:true
                 , editrules:{ required:true }
            },
            {
                fixed: true, name: 'md_kieudang_id'
                , label: 'Tên KD'
                , index: 'kd.ten_tv'
                , width: 50
                , edittype: 'select'
                , editable:true
                , editrules:{ required:true, edithidden:true }
                , editoptions: { dataUrl: 'Controllers/KieuDangSanPhamController.ashx?action=getoption' }
            },
			{
                fixed: true, name: 'ma_kieudang'
                , label: 'Mã KD'
                , index: 'kd.ma_kieudang'
                , width: 50
                , edittype: 'text'
                , editable:false
                , editrules:{ required:true }
            },
            {
                fixed: true, name: 'md_chucnang_id'
                , label: 'Tên CN'
                , index: 'cn.ten_tv'
                , width: 50
                , edittype: 'select'
                , editable:true
                , editrules : { required:true, edithidden:true }
                , editoptions: { dataUrl: 'Controllers/ChucNangSanPhamController.ashx?action=getoption' }
            },
			{
                fixed: true, name: 'ma_chucnang'
                , label: 'Mã CN'
                , index: 'cn.ma_chucnang'
                , width: 50
                , edittype: 'text'
                , editable:false
                , editrules:{ required:true }
            },
            {
                fixed: true, name: 'mota_tiengviet'
                , label: 'Mô tả TV'
                , index: 'sp.mota_tiengviet'
                , width: 100
                , edittype: 'textarea'
                , editable:false
            },
            {
                fixed: true, name: 'mota_tienganh'
                , label: 'Mô tả TA'
                , index: 'sp.mota_tienganh'
                , width: 100
                , edittype: 'textarea'
                , editable:false
            },
            {
                fixed: true, name: 'tendvt'
                , label: 'ĐVT'
                , index: 'dvt.ten_dvt'
                , width: 50
                , align: 'center'
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/UnitProductController.ashx?action=getoption' }
                , stype:'select'
                , searchoptions:{ sopt:['eq'], dataUrl: 'Controllers/UnitProductController.ashx?action=getsearchoption' }
				, hidden: true 
            },
            {
                fixed: true, name: 'l_inch'
                , label: 'L (inch)'
                , index: 'sp.l_inch'
                , width: 50
                , edittype: 'text'
                , editable:false
                , hidden:true
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'w_inch'
                , label: 'W (inch)'
                , index: 'sp.w_inch'
                , width: 50
                , edittype: 'text'
                , editable:false
                , hidden:true
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'h_inch'
                , label: 'H (inch)'
                , index: 'sp.h_inch'
                , width: 50
                , edittype: 'text'
                , editable:false
                , hidden:true
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'l_cm'
                , label: 'L (cm)'
                , index: 'sp.l_cm'
                , width: 50
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'w_cm'
                , label: 'W (cm)'
                , index: 'sp.w_cm'
                , width: 50
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'h_cm'
                , label: 'H (cm)'
                , index: 'sp.h_cm'
                , width: 50
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'trongluong'
                , label: 'Trọng lượng'
                , index: 'sp.trongluong'
                , width: 50
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { required:true, number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'dientich'
                , label: 'Diện tích'
                , index: 'sp.dientich'
                , width: 50
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { required:true, number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'thetich'
                , label: 'Thể tích'
                , index: 'sp.thetich'
                , width: 50
                , align: 'right'
                , edittype: 'text'
                , editable:true
                , editrules : { required:true, number:true }
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'md_nhomnangluc_id'
                , label: 'Nhóm NL'
                , index: 'nnl.hehang'
                , width: 50
                , edittype: 'select'
                , editable:true
                , editrules : { required:true }
                , editoptions: { dataUrl: 'Controllers/NhomNangLucController.ashx?action=getoption' }
                , sortable: false
            },
            {
                fixed: true, name: 'md_chungloai_id'
                , label: 'Loại SP'
                , index: 'cl.code_cl'
                , width: 50
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/ChungLoaiController.ashx?action=getoption' }
                , hidden: true 
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'tenncc'
                , label: 'NCU'
                , index: 'ncc.ma_dtkd'
                , width: 50
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=1' }
                , editrules:{ edithidden:true }
                , hidden: true 
            },
            {
                fixed: true, name: 'md_cangbien_id'
                , label: 'Cảng biển'
                , index: 'cb.ten_cangbien'
                , width: 50
                , edittype: 'select'
                , editable:false
                , editoptions: { dataUrl: 'Controllers/PortController.ashx?action=getoption' }
                , hidden: true 
            },
            {
                fixed: true, name: 'md_hscode_id'
                , label: 'HSCode'
                , index: 'cb.ma_hscode'
                , width: 50
                , edittype: 'select'
                , editable: true
                , editoptions: { dataUrl: 'Controllers/HsCodeController.ashx?action=getoption' }
                , hidden: true 
            },
			{
                fixed: true, name: 'hscode_val'
                , label: 'HSCode (số)'
                , index: 'cb.ma_hscode'
                , width: 100
                , editable: false
                , hidden: false 
            },
            {
                fixed: true, name: 'chucnangsp'
                , label: 'Nhóm CN'
                , index: 'sp.chucnangsp'
                , width: 100
                , editable: false
                , hidden: false 
            },
			{
                fixed: true, name: 'ngayxacnhan'
                , label: 'Ngày xác nhận'
                , index: 'sp.ngayxacnhan'
                , width: 100
                , editable: true
				, edittype: 'text'
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                , hidden: false 
            },
            {
				fixed: true, name: 'ten_danhmuc'
                , label: 'TT hàng hóa'
				, index: 'sp.md_tinhtranghanghoa_id'
				, width: 100
				, edittype: 'select'
				, editoptions: { dataUrl: 'Controllers/DanhMucHangHoaController.ashx?action=getoption' }
				, editable: true
                , stype:'select'
                , searchoptions:{ dataUrl: 'Controllers/DanhMucHangHoaController.ashx?action=getoption' }
				, hidden: false
			},
			{
				fixed: true, name: 'hinhthucban'
                , label: 'Hình thức bán'
				, index: 'sp.hinhthucban'
				, width: 100
				, edittype: 'select'
				, editoptions: { value: selectHinhThucBan }
				, editable:true
				, hidden: false
			},
            {
                fixed: true, name: 'ghichu'
                , label: 'Ghi chú'
                , index: 'sp.ghichu'
                , width: 100
                , edittype: 'textarea'
                , editable: false
                , hidden: true 
                , editrules : { edithidden:true }
            },
            {
                fixed: true, name: 'ngaytao'
                , label: 'Ngày tạo'
                , index: 'sp.ngaytao'
                , width: 50
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'nguoitao'
                , label: 'Người tạo'
                , index: 'sp.nguoitao'
                , width: 50
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , label: 'Ngày cập nhật'
                , index: 'sp.ngaycapnhat'
                , width: 50
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , label: 'Người cập nhật'
                , index: 'sp.nguoicapnhat'
                , width: 50
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'mota'
                , label: 'Mô tả'
                , index: 'sp.mota'
                , width: 50
                , editable:true
                , edittype: 'textarea'
                , editrules:{ edithidden:true }
                , hidden: false
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , label: 'Hoạt động'
                , index: 'sp.hoatdong'
                , width: 50
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
			                    
			                    if($('#grid_ds_quycachdonggoi_soanthao').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_quycachdonggoi_soanthao').jqGrid('setGridParam',{url:'Controllers/QuyCachDongGoiController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_quycachdonggoi_soanthao').jqGrid().trigger('reloadGrid');
				                }
				                
				                if($('#grid_ds_giafob_soanthao').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_giafob_soanthao').jqGrid('setGridParam',{url:'Controllers/PriceFOBController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_giafob_soanthao').jqGrid().trigger('reloadGrid');
				                }
				                
				                if($('#grid_ds_gianoi_soanthao').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_gianoi_soanthao').jqGrid('setGridParam',{url:'Controllers/PriceSaleController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_gianoi_soanthao').jqGrid().trigger('reloadGrid');
				                }
				                
				                if($('#grid_ds_khachhangdocquyen_soanthao').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_khachhangdocquyen_soanthao').jqGrid('setGridParam',{url:'Controllers/QuyDinhVoiKhachHangController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_khachhangdocquyen_soanthao').jqGrid().trigger('reloadGrid');
				                } 
				                
				                if($('#grid_ds_cangxuathang_soanthao').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_cangxuathang_soanthao').jqGrid('setGridParam',{url:'Controllers/CangXuatHangController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_cangxuathang_soanthao').jqGrid().trigger('reloadGrid');
				                } 
				                
				                if($('#grid_ds_bientau_soanthao').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_bientau_soanthao').jqGrid('setGridParam',{url:'Controllers/BienTauController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_bientau_soanthao').jqGrid().trigger('reloadGrid');
				                } 
				                
				                if($('#grid_ds_nhacungungmacdinh_soanthao').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_nhacungungmacdinh_soanthao').jqGrid('setGridParam',{url:'Controllers/NhaCungUngMacDinhController.ashx?&productId='+ids,page:1});
				                    $('#grid_ds_nhacungungmacdinh_soanthao').jqGrid().trigger('reloadGrid');
				                }
				            } 
				            else 
				            {
			                    $('#grid_ds_quycachdonggoi_soanthao').jqGrid('setGridParam',{url:'Controllers/QuyCachDongGoiController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_quycachdonggoi_soanthao').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_giafob_soanthao').jqGrid('setGridParam',{url:'Controllers/PriceFOBController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_giafob_soanthao').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_gianoi_soanthao').jqGrid('setGridParam',{url:'Controllers/PriceSaleController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_gianoi_soanthao').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_khachhangdocquyen_soanthao').jqGrid('setGridParam',{url:'Controllers/QuyDinhVoiKhachHangController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_khachhangdocquyen_soanthao').jqGrid().trigger('reloadGrid');		
			                    
			                    $('#grid_ds_cangxuathang_soanthao').jqGrid('setGridParam',{url:'Controllers/CangXuatHangController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_cangxuathang_soanthao').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_bientau_soanthao').jqGrid('setGridParam',{url:'Controllers/BienTauController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_bientau_soanthao').jqGrid().trigger('reloadGrid');
			                    
			                    $('#grid_ds_nhacungungmacdinh_soanthao').jqGrid('setGridParam',{url:'Controllers/NhaCungUngMacDinhController.ashx?&productId='+ids,page:1});
			                    $('#grid_ds_nhacungungmacdinh_soanthao').jqGrid().trigger('reloadGrid');
			                    
			                    let ma_sanpham = $(this).jqGrid('getCell', ids, 'ma_sanpham');
			                    $('#img-productSoanThao').attr('src', '');
                                $('#img-productSoanThao').attr('src', 'Controllers/API_System.ashx?oper=loadImageProduct&msp=' + ma_sanpham + '&ver=' + uuidv4());
		                    } 

                            $('#grid_hanghoasoanthao_danhmuchanghoa').jqGrid('getGridParam', 'postData').id = ids;
                            $('#grid_hanghoasoanthao_danhmuchanghoa').jqGrid().trigger('reloadGrid');
		                }"
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"         
            runat="server" />
</DIV>
<DIV class="ui-layout-south" id="lay-south-sanphamsoanthao"> 
    <div class="ui-layout-center" id="tab-product-detailssoanthao">
        <ul>
            <li><a href="#nav-tabs-1">Quy Cách Đ.G</a></li>
			<li><a href="#nav-tabs-2">Giá FOB</a></li>
			<li><a href="#nav-tabs-3">Giá Nội</a></li>
			<li><a href="#nav-tabs-4">Quy Ðịnh Với K.H</a></li>
			<li><a href="#nav-tabs-5">Cảng Biển</a></li>
			<li><a href="#nav-tabs-6">Biến Tấu</a></li>
			<li><a href="#nav-tabs-7">NCU</a></li>
            <li style="display:none"><a href="#nav-tabs-8">Danh mục H.H</a></li>
		</ul>  
		    <div id="nav-tabs-1">
		        <uc1:jqGrid  ID="grid_ds_quycachdonggoi_soanthao" 
                SortName="md_donggoisanpham_id" 
                UrlFileAction="Controllers/QuyCachDongGoiController.ashx"
                RowNumbers="true"
                OndblClickRow = "function(rowid) { $(this).jqGrid('viewGridRow', rowid); }"
                ColModel = "[
                {
                    fixed: true, name: 'md_donggoisanpham_id'
                    , label: 'md_donggoisanpham_id'
                    , index: 'md_donggoisanpham_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                     fixed: true, name: 'ma_donggoi'
                     , label: 'Mã Ðóng Gói'
                     , index: 'ma_donggoi'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'ten_donggoi'
                     , label: 'Tên Ðóng Gói'
                     , index: 'ten_donggoi'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'ma_sanpham'
                     , label: 'Mã SP'
                     , index: 'ma_sanpham'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , hidden: false
                },
                {
                     fixed: true, name: 'sl_inner'
                     , label: 'SL Inner'
                     , index: 'sl_inner'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'dvtinner'
                     , label: 'ÐVT Inner'
                     , index: 'dg.dvt_inner'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'center'
                },
                {
                     fixed: true, name: 'l1'
                     , label: 'L1'
                     , index: 'l1'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'w1'
                     , label: 'W1'
                     , index: 'w1'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'h1'
                     , label: 'H1'
                     , index: 'h1'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'sl_outer'
                     , label: 'SL Outer'
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
                     , label: 'ÐVT Outer'
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
                     , label: 'L2'
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
                     , label: 'W2'
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
                     , label: 'H2'
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
                     , label: 'V2'
                     , index: 'v2'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , align:'right'
                },
                 {
                    fixed: true, name: 'sl_cont_mix'
                    , index: 'dg.sl_cont_mix'
                    , label: 'Số Lượng Cont Mix'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , hidden : true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
                {
                    fixed: true, name: 'soluonggoi_ctn_20'
                    , index: 'dg.soluonggoi_ctn_20'
                    , label: 'SL Gói/cnt20'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
				{
                    fixed: true, name: 'soluonggoi_ctn'
                    , index: 'dg.soluonggoi_ctn'
                    , label: 'SL Gói/cnt40'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
				{
                    fixed: true, name: 'soluonggoi_ctn_40hc'
                    , index: 'dg.soluonggoi_ctn_40hc'
                    , label: 'SL Gói/cnt40 HC'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules : { edithidden:true, number:true, required:true }
                    , editoptions:{ defaultValue: 0 }
                    , align : 'right'
                },
                {
                     fixed: true, name: 'nw1'
                     , label: 'NW1'
                     , index: 'nw1'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'gw1'
                     , label: 'GW1'
                     , index: 'gw1'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'nw2'
                     , label: 'NW2'
                     , index: 'nw2'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , align:'right'
                },
                {
                     fixed: true, name: 'gw2'
                     , label: 'GW2'
                     , index: 'gw2'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , align:'right'
                },
                {
                     fixed: true, name: 'vtdg2'
                     , label: 'VTDG2'
                     , index: 'vtdg2'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editrules:{ edithidden: true }
                     , align:'right'
                },
                {
                    fixed: true, name: 'cpdg_vuotchuan'
                    , index: 'dg.cpdg_vuotchuan'
                    , label: 'CPĐG vượt chuẩn (USD)'
                    , width: 50
                    , edittype: 'text'
                    , editable:true
                    , editrules:{ number:true, required:true }
                    , editoptions:{ defaultValue: '0' }
                    , align : 'right'
                    , formoptions: { colpos: 2, rowpos: 8 }
					, hidden : false
                },
                {
                     fixed: true, name: 'vd'
                     , label: 'VD'
                     , index: 'vd'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'vn'
                     , label: 'VN'
                     , index: 'vn'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'vl'
                     , label: 'VL'
                     , index: 'vl'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , align:'right'
                },
                {
                     fixed: true, name: 'ghichu_vachngan'
                     , label: 'Ghi Chú Vách Ngăn'
                     , index: 'ghichu_vachngan'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , formoptions:{colpos:2,rowpos:16}
                },
                {
                    fixed: true, name: 'mix_chophepsudung'
                    , index: 'dg.mix_chophepsudung'
                    , label: 'Mix Cho Phép Sử Dụng'
                    , width: 50
                    , edittype: 'checkbox'
                    , editable:true
                    , hidden : true
                    , editrules : { edithidden:true }
                    , formoptions: { colpos: 2, rowpos: 6 }
                },
                {
					fixed: true, name: 'ngayxacnhan'
					, index: 'dg.ngayxacnhan'
                    , label: 'Ngày Xác Nhận'
					, editable: true
					, width: 50
					, edittype: 'text'
					, editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
					, search: true
					, stype: 'text'
					, searchoptions: { 
						sopt: ['bw', 'ne']
						, dataInit: function (elem) { 
							$(elem).datepicker({ 
								changeYear: true
								, changeMonth: true
								, dateFormat:'dd/mm/yy'
								, onSelect: function(dateText, inst) {
								   if (this.id.substr(0, 3) === 'gs_') {
										// call triggerToolbar only in case of searching toolbar
										setTimeout(function () {
											// Kiểm tra ngày nếu thuộc 'dd/MM/yyyy' mới cho lộc
											var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
											if(regex.test(dateText)){
												$('#grid_ds_quotation')[0].triggerToolbar();
											}
										}, 100);
									}
								}
							});
						} 
					}
				},
				{
                    fixed: true, name: 'gcdg'
                    , index: 'dg.mota'
                    , label: 'Ghi chú ĐG'
                    , width: 50
                    , editable:true
                    , edittype: 'textarea'
                    , editrules: { edithidden: true }
                    , formoptions: { colpos: 2, rowpos: 9 }
                    
                },
				{
                    fixed: true
					, name: 'doigia_donggoi'
                    , index: 'dg.doigia_donggoi'
                    , label: 'Đổi giá theo ĐG'
                    , width: 50
                    , edittype: 'checkbox'
                    , editable:true
                    , hidden : false
                    , editrules : { edithidden:true }
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                    , formoptions: { colpos: 2, rowpos: 7 }
                },
                {
                     fixed: true, name: 'macdinh'
                     , label: 'Mặc Ðịnh'
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
                    , label: 'Ngày Tạo'
                    , index: 'ngaytao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'nguoitao'
                    , label: 'Người Tạo'
                    , index: 'nguoitao'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'ngaycapnhat'
                    , label: 'Ngày Cập Nhật'
                    , index: 'ngaycapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'nguoicapnhat'
                    , label: 'Người Cập Nhật'
                    , index: 'nguoicapnhat'
                    , width: 100
                    , editable:false
                    , edittype: 'text'
                    , hidden : true
                },
                {
                    fixed: true, name: 'mota'
                    , label: 'Mô tả'
                    , index: 'mota'
                    , width: 100
                    , editable:true
                    , edittype: 'textarea'
                    , hidden : true
                },
                {
                    fixed: true, name: 'hoatdong', hidden: true
                    , label: 'Hoạt động'
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
		        <uc1:jqGrid  ID="grid_ds_giafob_soanthao"
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
		        <uc1:jqGrid  ID="grid_ds_gianoi_soanthao"
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
		        <uc1:jqGrid  ID="grid_ds_khachhangdocquyen_soanthao"
                    SortName="hhdq.md_hanghoadocquyen_id" 
                    UrlFileAction="Controllers/QuyDinhVoiKhachHangController.ashx" 
                    ColNames="['hhdp.md_hanghoadocquyen_id' ,'Tên Đối Tác', 'Thị Trường Độc Quyền', 'Ngày bắt đầu', 'Ngày kết thúc', 'Mã SP', 'Mã SP', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Hoạt động', 'Ghi chú']"
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
                        fixed: true, name: 'mota'
                        , index: 'hhdq.mota'
                        , width: 100
                        , editable:true
                        , edittype: 'textarea'
                    },
					{
						fixed: true, name: 'ngaybatdau'
						, index: 'sp.ngaybatdau'
						, width: 100
						, editable: true
						, edittype: 'text'
						, align: 'center'
						, editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
						, hidden: false 
					},
					{
						fixed: true, name: 'ngayketthuc'
						, index: 'sp.ngayketthuc'
						, width: 100
						, editable: true
						, edittype: 'text'
						, align: 'center'
						, editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
						, hidden: false 
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
						fixed: true, name: 'hoatdong', hidden: true
						, index: 'hhdq.hoatdong'
						, width: 100
						, editable:true
						, edittype: 'checkbox'
						, align: 'center'
						, editoptions:{ value:'True:False', defaultValue: 'True' }
						, formatter: 'checkbox'
					},
					{
                        fixed: true, name: 'ghichu'
                        , index: 'hhdq.ghichu'
                        , width: 100
                        , editable:true
                        , edittype: 'textarea'
                    }
                    ]"
                    GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                    ViewFormOptions="width:900
                                    , beforeShowForm: function (formid) {
                                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                                    } "
                    Height = "420"
                    runat="server" />
                    GridComplete = "var o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                    ViewFormOptions="width:900
                                    , beforeShowForm: function (formid) {
                                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                                    } "
                    Height = "420"
                    runat="server" />
            		   
		    </div>
		    <div id="nav-tabs-5">
		        <uc1:jqGrid  ID="grid_ds_cangxuathang_soanthao"
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
                            var masterId = $('#grid_ds_sanphamsoanthao').jqGrid('getGridParam', 'selrow');
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
                            var masterId = $('#grid_ds_sanphamsoanthao').jqGrid('getGridParam', 'selrow');
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
                        beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        },
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    DelFormOptions="
                        beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        },
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
		        <uc1:jqGrid  ID="grid_ds_bientau_soanthao"
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
		        <uc1:jqGrid  ID="grid_ds_nhacungungmacdinh_soanthao"
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
                            var masterId = $('#grid_ds_sanphamsoanthao').jqGrid('getGridParam', 'selrow');
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
                            var masterId = $('#grid_ds_sanphamsoanthao').jqGrid('getGridParam', 'selrow');
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
                        beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        },
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    DelFormOptions="
                        beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        },
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    ViewFormOptions="
                        width:900
                        , beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                        } 
                    "
                    runat="server" />
		    </div>

            <div id="nav-tabs-8">
		        <uc1:jqGrid  ID="grid_hanghoasoanthao_danhmuchanghoa"
                    SortName="dm.ma_danhmuc" 
                    SortOrder="asc"
                    UrlFileAction="Controllers/HangHoa_DanhMucHangHoa_Controller.ashx" 
                    RowNumbers="true"
                    OndblClickRow="function(){ $('#edit_grid_hanghoasoanthao_danhmuchanghoa').click(); }"
                    ColModel = "[
                    {
                        fixed: true, name: 'md_sanpham_dmhh_id'
                        , index: 'dmhh.md_sanpham_dmhh_id'
                        , label: 'ID'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                        , key: true
                    },
                    {
                        fixed: true, name: 'md_sanpham_id'
                        , index: 'dmhh.md_sanpham_id'
                        , label: 'SP ID'
                        , width: 100
                        , hidden: true 
                        , editable:true
                        , edittype: 'text'
                    },
                    {
                        fixed: true, name: 'ma_danhmuc'
                        , index: 'dm.ma_danhmuc'
                        , label: 'Mã DMHH'
                        , width: 110
                        , edittype: 'text'
                        , editable: false
                        , align: 'center'
                        , editoptions: { readonly: 'readonly' }
                    },
                    {
                        fixed: true, name: 'ten_danhmuc'
                        , index: 'dm.ten_danhmuc'
                        , label: 'Tên DMHH'
                        , width: 110
                        , edittype: 'select'
                        , editable:true
                        , align: 'center'
                        , editrules : { required:true, edithidden:true }
                        , editoptions: { dataUrl: 'Controllers/DanhMucHangHoaController.ashx?action=getoption' }
                    },
                    {
                        fixed: true, name: 'ngaytao'
                        , index: 'dmhh.ngaytao'
                        , label: 'Ngày tạo'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoitao'
                        , index: 'dmhh.nguoitao'
                        , label: 'Người tạo'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'ngaycapnhat'
                        , index: 'dmhh.ngaycapnhat'
                        , label: 'Ngày cập nhật'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'nguoicapnhat'
                        , index: 'dmhh.nguoicapnhat'
                        , label: 'Người cập nhật'
                        , width: 100
                        , editable:false
                        , edittype: 'text'
                        , hidden: true
                    },
                    {
                        fixed: true, name: 'mota'
                        , index: 'dmhh.mota'
                        , label: 'Mô tả'
                        , width: 100
                        , editable:true
                        , edittype: 'textarea'
                    },
                    {
                        fixed: true, name: 'hoatdong', hidden: true
                        , index: 'dmhh.hoatdong'
                        , label: 'Hoạt động'
                        , width: 100
                        , editable:true
                        , edittype: 'checkbox'
                        , align: 'center'
                        , editoptions:{ value:'True:False', defaultValue: 'True' }
                        , formatter: 'checkbox'
                    }
                    ]"
                    GridComplete = "let o = $(this).parent().parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 4 - 80);  $(this).setGridWidth($(o).width()); "
                    ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
                    ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
                    ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
                    Height = "420"
                    AddFormOptions="
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                        , beforeShowForm: function (formid) {
                            let masterId = $('#grid_ds_sanphamsoanthao').jqGrid('getGridParam', 'selrow');
                            if(masterId == null)
                            {
                                $(formid).parent().parent().parent().dialog('destroy').remove();
                                alert('Hãy chọn một sản phẩm mới có thể tạo chi tiết.!');
                            }
                        }
                        , beforeSubmit:function(postdata, formid){
                            let masterId = $('#grid_ds_sanphamsoanthao').jqGrid('getGridParam', 'selrow');
                            if(masterId == null)
                            {
                                $(formid).parent().parent().parent().dialog('destroy').remove();
                                return [false,'Hãy chọn một sản phẩm mới có thể tạo chi tiết.!'];
                            }
                            else
                            {
                                postdata.md_sanpham_id = masterId;
                                return [true,'']; 
                            }
                        }"
                    EditFormOptions="
                        beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        },
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    DelFormOptions="
                        beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        },
                        afterSubmit: function(rs, postdata){
                           return showMsgDialog(rs);
                        }
                    "
                    ViewFormOptions="
                        width:900
                        , beforeShowForm: function (formid) {
                            formid.closest('div.ui-jqdialog').dialogCenter(); 
                        } 
                    "
                    runat="server" />
		    </div>
    </div>
    <div class="ui-layout-east" style="text-align:center; background:#f4f0ec">
        <table style="width:100%; height:100%">
            <tr>
                <td>
                    <img id="img-productSoanThao" class="loadingIMG" style="max-width:100%; margin: 0px auto; min-width: 40px;" src="Controllers/API_System.ashx?oper=loadImageProduct&msp=default" />
                </td>
            </tr>
        </table>
    </div>
</DIV>

<script>
	if(xemgiaHHST.toLowerCase() == 'false') {
		var parent = $('#tab-product-detailssoanthao');
		parent.find('a[href="#nav-tabs-2"]').parent().remove();
		parent.find('a[href="#nav-tabs-3"]').parent().remove();
		parent.find('#nav-tabs-2').remove();
		parent.find('#nav-tabs-3').remove();
	}
	
	if(chietxuatHHST.toLowerCase() == 'false') {
		var parent = $('#grid_ds_sanphamsoanthao-pager_left');
		parent.find('.ui-icon.ui-icon-print').parent().parent().remove();
	}
	
	createRightPanel('grid_ds_sanphamsoanthao');
	createRightPanel('grid_ds_quycachdonggoi_soanthao');
	createRightPanel('grid_ds_giafob_soanthao');
	createRightPanel('grid_ds_gianoi_soanthao');
	createRightPanel('grid_ds_khachhangdocquyen_soanthao');
	createRightPanel('grid_ds_cangxuathang_soanthao');
	createRightPanel('grid_ds_bientau_soanthao');
    createRightPanel('grid_ds_nhacungungmacdinh_soanthao');
    createRightPanel('grid_hanghoasoanthao_danhmuchanghoa');
	$('#tab-product-detailssoanthao').tabs();
</script>
