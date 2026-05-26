<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-tao-chungtuxuathang-cungkh.aspx.cs" Inherits="inc_tao_chungtuxuathang_cungkh" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function() {
        // level 1
        $('#layout-center-chungtuxuathang-cungkh').parent().layout({
            south: {
                size: "50%"
            }
        });

        // level 2
        $('#layout-south-chungtuxuathang-cungkh').layout({
            west: {
                size: "30%"
                , onresize_end: function() {
                    var o = $("#layout-west-phieuxuat-cungkh");
                    var h = o.height();
                    var w = o.width();
                    $("#grid_ds_donhangcungphieuxuat").setGridHeight(h - 90);
                    $("#grid_ds_donhangcungphieuxuat").setGridWidth(w);
                }
            }
        });

        // level 3
        $('#layout-center-phieuxuat-cungkh').layout({
            south: {
                size: "50%"
                , onresize_end: function() {
                    var o = $("#layout-south-soanthao-cungkh");
                    var h = o.height();
                    var w = o.width();
                    $("#grid_ds_chitietxuatkho_cungkh").setGridHeight(h - 73);
                    $("#grid_ds_chitietxuatkho_cungkh").setGridWidth(w);
                }
            }
            , center: {
                onresize_end: function() {
                    var o = $("#layout-center-soanthao-cungkh");
                    var h = o.height();
                    var w = o.width();
                    $("#grid_ds_xuatkho_cungkh").setGridHeight(h - 73);
                    $("#grid_ds_xuatkho_cungkh").setGridWidth(w);
                }
            }
        });

        $('.submit button').button();

        $('#txtKHNgayLap').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $('#txtKHNgayLap').datepicker('setDate', new Date());

        $('#txtKHNgayVanDon').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $('#txtKHNgayVanDon').datepicker('setDate', new Date());
    });
</script>

<!-- Level 1  South -->
<div class="ui-layout-south ui-widget-content" id="layout-south-chungtuxuathang-cungkh">
    <!-- Level 2 West -->
    <div class="ui-layout-west ui-widget-content" id="layout-west-phieuxuat-cungkh">
        <uc1:jqGrid  ID="grid_ds_donhangcungphieuxuat" 
            Caption="Danh sách khách hàng đã có phiếu xuất kho"
            SortName="dtkd.ma_dtkd" 
            SortOrder="asc"
            FilterToolbar="true"
            UrlFileAction="Controllers/KhachHangCoPhieuXuatController.ashx" 
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            ColNames="['md_doitackinhdoanh_id', 'Mã Khách Hàng']"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'md_doitackinhdoanh_id'
                , index: 'dtkd.md_doitackinhdoanh_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ma_dtkd'
                 , index: 'dtkd.ma_dtkd'
                 , editable: true
                 , width: 200
                 , edittype: 'text'
            }
            ]"
            OnSelectRow = "function(ids) {
		                    if(ids == null) {
			                    ids=0;
			                    if($('#grid_ds_xuatkho_cungkh').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_xuatkho_cungkh').jqGrid('setGridParam',{url:'Controllers/XuatKhoByPartnerController.ashx?&partnerId='+ids,page:1});
				                    $('#grid_ds_xuatkho_cungkh').jqGrid().trigger('reloadGrid');
				                } 
				            } else {
			                    $('#grid_ds_xuatkho_cungkh').jqGrid('setGridParam',{url:'Controllers/XuatKhoByPartnerController.ashx?&partnerId='+ids,page:1});
			                    $('#grid_ds_xuatkho_cungkh').jqGrid().trigger('reloadGrid');			
		                    } }"
            Height = "420"
            runat="server" />
    </div>
    <!-- Level 2 West -->
    
    <!-- Level 2 Center -->
    <div class="ui-layout-center ui-widget-content" id="layout-center-phieuxuat-cungkh">
        
        <!-- Level 3  South -->
        <div class="ui-layout-south ui-widget-content" id="layout-south-soanthao-cungkh">
            <uc1:jqGrid  ID="grid_ds_chitietxuatkho_cungkh" 
			Caption = "Chi Tiết Xuất Kho"
            SortName="dnx.c_dongnhapxuat_id" 
            UrlFileAction="Controllers/DongNhapXuatController.ashx" 
            ColNames="['c_dongnhapxuat_id', 'Nhập Xuất', 'Chi Tiết Đơn Hàng', 'STT', 'Mã SP', 'Mã SP', 'Mô Tả TV', 'Đơn Vị Tính', 'Số Lượng Phải Nhập/Xuất', 'Số Lượng Thực Nhập/Xuất', 'Đơn Giá', 'Số Kiện Thực Tế', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'c_dongnhapxuat_id'
                , index: 'c_dongnhapxuat_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'c_nhapxuat_id'
                 , index: 'c_nhapxuat_id'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , hidden : true
            },
            {
                fixed: true, name: 'c_dongdonhang_id'
                , index: 'c_dongdonhang_id'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
            },
            {
                fixed: true, name: 'line'
                , index: 'line'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sanpham_id'
                , index: 'sanpham_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'tensp'
                , index: 'tensp'
                , width: 100
                , edittype: 'text'
                , editable:true
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
                fixed: true, name: 'mota_tiengviet'
                , index: 'mota_tiengviet'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'md_donvitinh_id'
                , index: 'md_donvitinh_id'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/UnitController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'slphai_nhapxuat'
                , index: 'slphai_nhapxuat'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'slthuc_nhapxuat'
                , index: 'slthuc_nhapxuat'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'dongia'
                , index: 'dongia'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sokien_thucte'
                , index: 'sokien_thucte'
                , width: 100
                , edittype: 'text'
                , editable:true
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()+ 4 - 77);"
            Height = "150"
            MultiSelect = "false" 
            runat="server" />
        </div>
        <!-- # Level 3  South -->
        
        <!-- Level 3  Center -->
        <div class="ui-layout-center ui-widget-content" id="layout-center-soanthao-cungkh">
                <uc1:jqGrid  ID="grid_ds_xuatkho_cungkh"
                Caption="Xuất Kho" 
                SortName="c_nhapxuat_id" 
                UrlFileAction="Controllers/XuatKhoByPartnerController.ashx"
                ColNames="['c_nhapxuat_id', 'Số Phiếu', 'P/O Tham Chiếu', 'Ngày Giao Nhận', 'Đối Tác', 'Người Giao', 'Người Nhập', 'Số Phiếu Khách', 'Ngày Phiếu', 'Kho', 'Số Seal', 'Số Container', 'Loại Cont', 'Trạng Thái', 'Loại Chứng Từ', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
                RowNumbers="true"
                ColModel = "[
                {
                    fixed: true, name: 'c_nhapxuat_id'
                    , index: 'c_nhapxuat_id'
                    , width: 100
                    , hidden: true 
                    , editable:true
                    , edittype: 'text'
                    , key: true
                },
                {
                     fixed: true, name: 'sophieu'
                     , index: 'sophieu'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'sophieunx'
                     , index: 'sophieunx'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'ngay_giaonhan'
                     , index: 'ngay_giaonhan'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                },
                {
                     fixed: true, name: 'md_doitackinhdoanh_id'
                     , index: 'md_doitackinhdoanh_id'
                     , editable: true
                     , width: 100
                     , edittype: 'select'
                     , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption' }
                },
                {
                     fixed: true, name: 'nguoigiao'
                     , index: 'nguoigiao'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'nguoinhan'
                     , index: 'nguoinhan'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'sophieukhach'
                     , index: 'sophieukhach'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'ngay_phieu'
                     , index: 'ngay_phieu'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                },
                {
                     fixed: true, name: 'nhapxuat_kho_id'
                     , index: 'nhapxuat_kho_id'
                     , editable: true
                     , width: 100
                     , edittype: 'select'
                     , editoptions: { dataUrl: 'Controllers/WarehouseController.ashx?action=getoption' }
                     , hidden:true
                },
                {
                     fixed: true, name: 'soseal'
                     , index: 'soseal'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'socontainer'
                     , index: 'socontainer'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'loaicont'
                     , index: 'loaicont'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                },
                {
                     fixed: true, name: 'md_trangthai_id'
                     , index: 'nx.md_trangthai_id'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , hidden:true
                },
                {
                     fixed: true, name: 'md_loaichungtu_id'
                     , index: 'md_loaichungtu_id'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , hidden:true
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
                    , align: 'center'
                    , editoptions:{ value:'True:False', defaultValue: 'True' }
                    , formatter: 'checkbox'
                }
                ]"
                GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()+ 4 - 77);"
                Height = "170"
                MultiSelect = "true" 
                OnSelectRow = "function(ids) {
		                    if(ids == null) {
			                    ids=0;
			                    if($('#grid_ds_chitietxuatkho_cungkh').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_chitietxuatkho_cungkh').jqGrid('setGridParam',{url:'Controllers/DongNhapXuatController.ashx?&whId='+ids,page:1});
				                    $('#grid_ds_chitietxuatkho_cungkh').jqGrid().trigger('reloadGrid');
				                } 
				            } else {
			                    $('#grid_ds_chitietxuatkho_cungkh').jqGrid('setGridParam',{url:'Controllers/DongNhapXuatController.ashx?&whId='+ids,page:1});
			                    $('#grid_ds_chitietxuatkho_cungkh').jqGrid().trigger('reloadGrid');			
		                    } }"
                runat="server" />
        </div>
        <!-- # Level 3  Center -->
        
    </div>
    <!-- # Level 2 Center -->
    
</div>
<!-- Level 1 South -->

<!-- Level 1 Center -->
<div class="ui-layout-center ui-widget-content" id="layout-center-chungtuxuathang-cungkh" style="text-align:center; background:#F4F0EC; overflow:auto !important">
<div class="ui-widget-header" style="padding:8px">Thông tin packing list - invoice</div>
<style>
    .frm-packinglist{ padding-top:5px}
    .frm-packinglist tr td
    {
        padding:3px;	
    }
</style>
<script>

    $(document).ready(function() {
		//-- Tạo Packing List
        $('#btn_add_from_kh').click(function() {
            var id = "create_pkl_partner";
            var title = "Tạo Packing List - Invoice Cùng Khách Hàng";

            $('body').append("<div title='" + title + "' id='" + id + "'>" +
                    "<h3 style='padding:10px' class='ui-state-highlight ui-corner-all'>" + "<div style='display:none' id='wait'><img style='width:30px; height:30px' src='iconcollection/loading.gif'/></div><div id='dialog_caution'>Có phải bạn muốn tạo Packing List - Invoice?</div></h3></div>");
            $("#create_pkl_partner").dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn_partner",
                        text: "Có",
                        click: function() {
                        $("#create_pkl_partner").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn_partner").button("disable");
                                $("#btn_close").button("disable");
                            });

                            $("#create_pkl_partner").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn_close span").text("Thoát");
                                $("#btn_close").button("enable");
                            });

                            var data = new Object();
                            var form = "#frmFromKH";
                            data["txtSoPackingList"] = $(form + " #txtKHSoPackingList").val();
                            data["txtSoInvoice"] = $(form + " #txtKHSoInvoice").val();
                            data["txtNgayLap"] = $(form + " #txtKHNgayLap").val();
                            data["txtSoTau"] = $(form + " #txtKHSoTau").val();
                            //data["selNoiDi"] = $(form + " #selKHNoiDi").val();
                            data["txtNoiDen"] = $(form + " #txtKHNoiDen").val();
                            data["txtSoVanDon"] = $(form + " #txtKHSoVanDon").val();
                            data["txtNgayVanDon"] = $(form + " #txtKHNgayVanDon").val();
                            data["txtHangHoa"] = $(form + " #txtKHHangHoa").val();
                            data["txtHangHoaVn"] = $(form + " #txtKHHangHoaVn").val();
                            data["txtDienGiaiCongThem"] = $(form + " #txtKHDienGiaiCongThem").val();
                            data["txtGiaTriCongThem"] = $(form + " #txtKHGiaTriCongThem").val();
                            data["txtDienGiaiTruLai"] = $(form + " #txtKHDienGiaiTruLai").val();
                            data["txtGiaTriTruLai"] = $(form + " #txtKHGiaTriTruLai").val();
                            //
							data["txtTongGiaTriTruPO"] = $(form + " #txtKHTongGiaTriTruPO").val();
                            data["txtGiaTriTruPO"] = $(form + " #txtKHGiaTriTruPO").val();
							//
							data["txtTongGiaTriCongPO"] = $(form + " #txtKHTongGiaTriCongPO").val();
                            data["txtGiaTriCongPO"] = $(form + " #txtKHGiaTriCongPO").val();
                            data["arrayPhieuXuat"] = $('#grid_ds_xuatkho_cungkh').getGridParam('selarrrow').join(", ");
                            $.ajax({
                                url: "Controllers/TaoPackingListController.ashx?action=add",
                                type: "POST",
                                data: { p: data },
                                error: function(rs) {
                                    Popup("Error", 450, 300, rs.responseText);
                                },
                                success: function(rs) {
                                    $('#create_pkl_partner').find('#dialog_caution').html(rs);
									$('#txtKHTongGiaTriCongPO').val(-1);
									$('#txtKHTongGiaTriTruPO').val(-1);
									$('#txtKHGiaTriCongPO').val(0);
									$('#txtKHGiaTriTruPO').val(0);
                                }
                            });
                            
                        }
                    },
                    {
                        id: "btn_close",
                        text: "Không",
                        click: function() {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
            

        });
		//-- Tinh lai phi cong tru PO
		$('#btn_load_from_kh').click(function() {
			var data = new Object();
			var form = "#frmFromKH";
			
			data["arrayPhieuXuat"] = $('#grid_ds_xuatkho_cungkh').getGridParam('selarrrow').join(", ");
			$.ajax({
				url: "Controllers/TaoPackingListController.ashx?action=load",
				type: "POST",
				data: { p: data },
				error: function(rs) {
					Popup("Error", 450, 300, rs.responseText);
				},
				success: function(rs) {
					rs = rs.split(',');
					$('#txtKHTongGiaTriCongPO').val(rs[0]);
					$('#txtKHTongGiaTriTruPO').val(rs[1]);
				}
			});
		});
	});
</script>
    <div style="width:990px; margin:auto">
        <table id="frmFromKH" class="frm-packinglist" style="width:100%; text-align:left">
                <tr>
                    <td>Số Packing List/Invoice</td>
                    <td>:</td>
                    <td><input id="txtKHSoPackingList" name="txtKHtxtSoPackingList" type="text" /></td>
                    <%--<td>Số Invoice</td>
                    <td>:</td>--%>
                    <td><input id="txtKHSoInvoice" name="txtKHtxtSoInvoice" type="hidden" /></td>
                </tr>
                
                <tr>
                    <td>Ngày Lập</td>
                    <td>:</td>
                    <td>
                        <div class="datetime">
                            <input id="txtKHNgayLap" name="txtKHNgayLap" type="text"/>
                        </div>
                    </td>
                    <td>Số Tàu(M/V)</td>
                    <td>:</td>
                    <td><input id="txtKHSoTau" name="txtKHSoTau" type="text" /></td>
                </tr>
                
                <tr>
                    <td><%--Nơi Đi(From)--%></td>
                    <td></td>
                    <td>
                        <%--<% 
                            String selFrom = "select md_cangbien_id, ten_cangbien from md_cangbien order by ten_cangbien asc";
                            System.Data.SqlClient.SqlDataReader rdFrom = mdbc.ExecuteReader(selFrom);
                        %>
                        <select id="selKHNoiDi" name="selKHNoiDi">
                           <% if (rdFrom.HasRows)
                              {
                                  while (rdFrom.Read())
                                  {
                           %>     
                                <option value="<%= rdFrom[0] %>"><%= rdFrom[1]%></option>
                           <%     }
                              }
                              rdFrom.Close();
                           %>
                        </select>--%>
                    </td>
                    <td>Nơi Đến(To)</td>
                    <td>:</td>
                    <td>
                         <input id="txtKHNoiDen" name="txtKHNoiDen" />
                    </td>
                </tr>
                
                <tr>
                    <td>Số Vận Đơn(BL. No)</td>
                    <td>:</td>
                    <td><input id="txtKHSoVanDon" name="txtKHSoVanDon" type="text" /></td>
                    <td>Ngày Vận Đơn</td>
                    <td>:</td>
                    <td>
                        <div class="datetime">
                            <input id="txtKHNgayVanDon" name="txtKHNgayVanDon" type="text" />
                        </div>
                    </td>
                </tr>
                
                <%--<tr>
                    <td>Hàng Hóa(Commondity)</td>
                    <td>:</td>
                    <td><input id="txtHangHoa" name="txtHangHoa" type="text" /></td>
                    <td>Hàng Hóa VN</td>
                    <td>:</td>
                    <td><input id="txtHangHoaVn" name="txtHangHoaVn" type="text" /></td>
                </tr>--%>
                
                <tr>
                    <td>Diễn Giải Cộng Thêm</td>
                    <td>:</td>
                    <td><input id="txtKHDienGiaiCongThem" name="txtKHDienGiaiCongThem" type="text" /></td>
                    <td>Giá Trị Cộng Thêm</td>
                    <td>:</td>
                    <td><input id="txtKHGiaTriCongThem" name="txtKHGiaTriCongThem" type="text" value="0" /></td>
                </tr>
                
                <tr>
                    <td>Diễn Giải Trừ Lại</td>
                    <td>:</td>
                    <td><input id="txtKHDienGiaiTruLai" name="txtKHDienGiaiTruLai" type="text" /></td>
                    <td>Giá Trị Trừ Lại</td>
                    <td>:</td>
                    <td><input id="txtKHGiaTriTruLai" name="txtKHGiaTriTruLai" type="text" value="0" /></td>
                </tr>
				
				<tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>Tổng giá trị cộng PO</td>
                    <td>:</td>
                    <td><input id="txtKHTongGiaTriCongPO" name="txtKHTongGiaTriCongPO" type="text" value="-1" readonly /></td>
                    <td></td>
                    <td></td>
                </tr>
				<tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>Giá Trị cộng PO</td>
                    <td>:</td>
                    <td><input id="txtKHGiaTriCongPO" name="txtKHGiaTriCongPO" type="text" value="0" /></td>
                    <td></td>
                    <td></td>
                </tr>

				<tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>Tổng giá trị trừ PO</td>
                    <td>:</td>
                    <td><input id="txtKHTongGiaTriTruPO" name="txtKHTongGiaTriTruPO" type="text" value="-1" readonly /></td>
                    <td></td>
                    <td></td>
                </tr>
				<tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>Giá Trị trừ PO</td>
                    <td>:</td>
                    <td><input id="txtKHGiaTriTruPO" name="txtKHGiaTriTruPO" type="text" value="0" /></td>
                    <td></td>
                    <td></td>
                </tr>
				
				<tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td rowspan="5" class="submit">
						<button id="btn_load_from_kh">Tính lại phí cộng trừ PO</button>
                        <button id="btn_add_from_kh">Tạo Packing List - Invoice</button>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
    </div>
</div>
<!-- Level 1 Center -->


    