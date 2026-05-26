<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-xacnhanxuatkho.aspx.cs" Inherits="inc_xacnhanxuatkho" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
    $('#lay-center-xacnhanxuatkho').parent().layout({
            north: {
                size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                var o = $("#layout-north-xacnhanxuatkho").height();
                $("#grid_ds_xacnhanxuatkho").setGridHeight(o - 90);
                }
            },
            center: {
                onresize_end: function() {
                var o = $("#lay-center-xacnhanxuatkho").height();
                $("#grid_ds_chitietxacnhanxuatkho").setGridHeight(o - 90);
                }
            }
        })
    });
    
</script>
<DIV class="ui-layout-north ui-widget-content" id="layout-north-xacnhanxuatkho">
<uc1:jqGrid  ID="grid_ds_xacnhanxuatkho" 
            Caption="Phiếu Xuất Kho"
            SortName="c_xacnhan_xuatkho_id" 
            UrlFileAction="Controllers/XacNhanXuatKhoController.ashx" 
            ColNames="['c_xacnhan_xuatkho_id', 'Số Chứng Từ', 'Ngày Lập'
                    , 'Người Lập', 'Đối Tác Kinh Doanh', 'Kho', 'Số PO', 'Đơn Hàng'
                    , 'Số Cont', 'Số Seal', 'Loại', 'Ghi Chú'
                    , 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{ beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }}); }"
            FuncTaoPhieuXuat="function(){ add_tab('Tạo Phiếu Xuất', 'inc-taoxacnhan-xuatkho.aspx')}"
            FuncActivePhieuXuat = "function(){alert('aaa')}"
            ColModel = "[
            {
                fixed: true, name: 'c_xacnhan_xuatkho_id'
                , index: 'xnxk.c_xacnhan_xuatkho_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'sochungtu'
                 , index: 'xnxk.sochungtu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ngaylap'
                 , index: 'xnxk.ngaylap'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                fixed: true, name: 'nguoilap'
                , index: 'xnxk.nguoilap'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                 fixed: true, name: 'md_doitackinhdoanh_id'
                 , index: 'dtkd.ma_dtkd'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'md_kho_id'
                 , index: 'kho.ten_kho'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/WareHouseController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'so_po'
                 , index: 'xnxk.so_po'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'c_donhang_id'
                 , index: 'dh.sochungtu'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/DonHangController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'so_cont'
                 , index: 'xnxk.so_cont'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'so_seal'
                 , index: 'xnxk.so_seal'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'loai'
                 , index: 'xnxk.loai'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ghichu'
                 , index: 'xnxk.ghichu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'dsdh.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'dsdh.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'dsdh.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'dsdh.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'dsdh.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: true
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'dsdh.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            FilterToolbar = "true"
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions="beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            EditFormOptions="beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            ViewFormOptions="beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            DelFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_chitietxacnhanxuatkho').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_chitietxacnhanxuatkho').jqGrid('setGridParam',{url:'Controllers/DongXacNhanXuatKhoController.ashx?&pxkId='+ids,page:1});
				                $('#grid_ds_chitietxacnhanxuatkho').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_chitietxacnhanxuatkho').jqGrid('setGridParam',{url:'Controllers/DongXacNhanXuatKhoController.ashx?&pxkId='+ids,page:1});
			                $('#grid_ds_chitietxacnhanxuatkho').jqGrid().trigger('reloadGrid');			
		                } }"
            runat="server" />
</DIV>

<DIV class="ui-layout-center ui-widget-content" id="lay-center-xacnhanxuatkho" style="padding:0;"> 
			<uc1:jqGrid  ID="grid_ds_chitietxacnhanxuatkho" 
            SortName="dxnxk.c_dongxacnhan_xuatkho_id" 
            UrlFileAction="Controllers/DongXacNhanXuatKhoController.ashx" 
            ColNames="['c_dongxacnhan_xuatkho_id', 'Phiếu Xác Nhận', 'Dòng Đơn Hàng', 'Mã SP', 'Mã SP'
                    , 'Số Lượng PO', 'Số Lượng Yêu Cầu Xuất', 'Số Lượng Thực Xuất', 'Mã SP Khách'
                    , 'Mô Tả TA', 'Đơn Vị Tính Sản Phẩm', 'Đóng Gói', 'Số Biên', 'Ghi Chú'
                    , 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{ beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }}); }"
            ColModel = "[
            {
                fixed: true, name: 'c_dongxacnhan_xuatkho_id'
                , index: 'dxnxk.c_dongxacnhan_xuatkho_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'c_xacnhan_xuatkho_id'
                , index: 'xnxk.c_xacnhan_xuatkho_id'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/XacNhanXuatKhoController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'c_dongdonhang_id'
                , index: 'dxnxk.c_dongdonhang_id'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
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
                fixed: true, name: 'ma_sanpham'
                , index: 'sp.ma_sanpham'
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
                fixed: true, name: 'sl_po'
                , index: 'dxnxk.sl_po'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sl_yeucauxuat'
                , index: 'dxnxk.sl_yeucauxuat'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'sl_thucxuat'
                , index: 'dxnxk.sl_thucxuat'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'ma_sanpham_khach'
                , index: 'dxnxk.ma_sanpham_khach'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'mota_tienganh'
                , index: 'dxnxk.mota_tienganh'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'dvt_sanpham'
                , index: 'dvt.ten_dvt'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'md_donggoi_id'
                , index: 'dxnxk.md_donggoi_id'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
            },
            {
                fixed: true, name: 'so_bien'
                , index: 'dxnxk.so_bien'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'ghichu'
                , index: 'dxnxk.ghichu'
                , width: 100
                , edittype: 'textarea'
                , editable:true
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'dxnxk.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'dxnxk.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'dxnxk.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'dxnxk.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
                , editrules:{ edithidden : true }
            },
            {
                fixed: true, name: 'mota'
                , index: 'dxnxk.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: true 
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'dxnxk.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            Caption = "Chi Tiết Phiếu Xuất Kho"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()-90);"
            FilterToolbar = "true"
            Height = "150"
            MultiSelect = "false" 
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            EditFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            DelFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            ViewFormOptions=" beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }"
            runat="server" />
	</DIV>
