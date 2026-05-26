<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-tao-chungtuxuathang-cungkh.aspx.cs" Inherits="inc_tao_chungtuxuathang_cungkh" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register src="~/UserControl/taoInvoice.ascx" tagname="taoInvoice" tagprefix="uc2" %>
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
                    $("#grid_ds_xuatkho_KH").setGridHeight(h - 73);
                    $("#grid_ds_xuatkho_KH").setGridWidth(w);
                }
            }
        });

        $('.submit button').button();
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
			                    if($('#grid_ds_xuatkho_KH').jqGrid('getGridParam','records') &gt; 0 )
			                    {
				                    $('#grid_ds_xuatkho_KH').jqGrid('setGridParam',{url:'Controllers/XuatKhoByPartnerController.ashx?&partnerId='+ids,page:1});
				                    $('#grid_ds_xuatkho_KH').jqGrid().trigger('reloadGrid');
				                } 
				            } else {
			                    $('#grid_ds_xuatkho_KH').jqGrid('setGridParam',{url:'Controllers/XuatKhoByPartnerController.ashx?&partnerId='+ids,page:1});
			                    $('#grid_ds_xuatkho_KH').jqGrid().trigger('reloadGrid');			
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
            ColNames="['c_dongnhapxuat_id', 'Nhập Xuất', 'Chi Tiết Đơn Hàng', 'STT', 'Mã Sản Phẩm', 'Mã Sản Phẩm', 'Mô Tả Tiếng Việt', 'Đơn Vị Tính', 'Số Lượng Phải Nhập/Xuất', 'Số Lượng Thực Nhập/Xuất', 'Đơn Giá', 'Số Kiện Thực Tế', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
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
                              , { 'columnName': 'ma_sanpham', 'width': '50', 'label': 'Mã Sản Phẩm', 'align':'left'}
                              , { 'columnName': 'mota_tiengviet', 'width': '50', 'label': 'Mô Tả Tiếng Việt' , 'align':'left'}],
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
                <uc1:jqGrid  ID="grid_ds_xuatkho_KH"
                Caption="Xuất Kho" 
                SortName="c_nhapxuat_id" 
                UrlFileAction="Controllers/XuatKhoByPartnerController.ashx"
                ColNames="['c_nhapxuat_id', 'Số', 'Số Phiếu', 'P/O Tham Chiếu', 'Ngày Giao Nhận', 'Đối Tác', 'Người Giao', 'Người Nhập', 'Số Phiếu Khách', 'Ngày Phiếu', 'Kho', 'Số Seal', 'Số Container', 'Loại Cont', 'Trạng Thái', 'Loại Chứng Từ', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
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
                     fixed: true, name: 'ct_dh'
                     , index: 'ct_dh'
                     , editable: true
                     , width: 100
                     , edittype: 'text'
                     , hidden: true
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
    <uc2:taoInvoice ID="taoInvoice" type="KH" runat="server" />
</div>
<!-- Level 1 Center -->


    