<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-taohanngach.aspx.cs" Inherits="inc_taohanngach" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function () {
        $('#layout-center-taohanngach-pi').parent().layout({
            north: {
                size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function () {
                    var o = $("#layout-north-taohanngach-pi").height();
                    $("#grid_ds_dathang_taohanngach").setGridHeight(o - 90);
                }
            }
        });


        // Soan Thao
        $('#layout-center-taohanngach-pi').layout({
            west: {
                size: "48.5%"
                , onresize_end: function () {
                    var h = $("#layout-west-soanthao-taohanngach-pi").height();
                    var w = $("#layout-west-soanthao-taohanngach-pi").width();
                    $("#grid_ds_chitietdathang_taohanngach").setGridHeight(h - 90);
                    $("#grid_ds_chitietdathang_taohanngach").setGridWidth(w - 2);
                }
            }
            , east: {
                size: "48.5%"
                , onresize_end: function () {
                    var h = $("#layout-east-soanthao-taohanngach-pi").height();
                    var w = $("#layout-east-soanthao-taohanngach-pi").width();
                    $("#grid_ds_dachon_taohanngach").setGridHeight(h - 90);
                    $("#grid_ds_dachon_taohanngach").setGridWidth(w - 2);
                }
            }
        });




        $(".btn-active").button().css({ 'width': '100%', 'height': '25%' });

        $('#add-all-taohanngach').click(function () {
            var gridData = jQuery("#grid_ds_chitietdathang_taohanngach")
            var ids = gridData.jqGrid('getDataIDs');
            for (var i = 0, il = ids.length; i < il; i++) {
                var ret = gridData.jqGrid('getRowData', ids[i], true);
                var datarow = {
                    c_donhang_id: ret.c_donhang_id
                    , c_danhsachdathang_id: ret.c_danhsachdathang_id
                    , c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_dongdsdh_id: ret.c_dongdsdh_id
                    , md_sanpham_id: ret.md_sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , ma_sanpham_khach: ret.ma_sanpham_khach
                    , sl_canlam: 0
                    , sl_thaydoi: 0
                    , sl_po: ret.sl_po
                    , sl_dathang: ret.sl_dathang
                    , sl_nhapthucte: ret.sl_conlai
                    , sl_conlai: ret.sl_conlai
                };
                $("#grid_ds_dachon_taohanngach").jqGrid('addRowData', ids[i], datarow);;
                gridData.jqGrid('delRowData', ids[i]);
            }
        });

        $('#add-taohanngach').click(function () {
            var id = jQuery("#grid_ds_chitietdathang_taohanngach").jqGrid('getGridParam', 'selrow');
            if (id) {
                var ret = jQuery("#grid_ds_chitietdathang_taohanngach").jqGrid('getRowData', id);
                var datarow = {
                    c_donhang_id: ret.c_donhang_id
                    , c_danhsachdathang_id: ret.c_danhsachdathang_id
                    , c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_dongdsdh_id: ret.c_dongdsdh_id
                    , md_sanpham_id: ret.md_sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , ma_sanpham_khach: ret.ma_sanpham_khach
                    , sl_canlam: 0
                    , sl_thaydoi: 0
                    , sl_po: ret.sl_po
                    , sl_dathang: ret.sl_dathang
                    , sl_nhapthucte: ret.sl_conlai
                    , sl_conlai: ret.sl_conlai
                };
                jQuery("#grid_ds_chitietdathang_taohanngach").jqGrid('delRowData', id);
                jQuery("#grid_ds_dachon_taohanngach").jqGrid('addRowData', id, datarow);
            } else { alert("Please select row"); }
        });

        $('#back-all-taohanngach').click(function () {
            var gridData = jQuery("#grid_ds_dachon_taohanngach")
            var ids = gridData.jqGrid('getDataIDs');
            for (var i = 0, il = ids.length; i < il; i++) {
                var ret = gridData.jqGrid('getRowData', ids[i], true);
                var datarow = {
                    c_donhang_id: ret.c_donhang_id
                    , c_danhsachdathang_id: ret.c_danhsachdathang_id
                    , c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_dongdsdh_id: ret.c_dongdsdh_id
                    , md_sanpham_id: ret.md_sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , ma_sanpham_khach: ret.ma_sanpham_khach
                    , sl_po: ret.sl_po
                    , sl_dathang: ret.sl_dathang
                    , sl_nhapthucte: ret.sl_conlai
                    , sl_conlai: ret.sl_conlai
                };
                $("#grid_ds_chitietdathang_taohanngach").jqGrid('addRowData', ids[i], datarow);;
                gridData.jqGrid('delRowData', ids[i]);
            }
        });

        $('#back-taohanngach').click(function () {
            var id = jQuery("#grid_ds_dachon_taohanngach").jqGrid('getGridParam', 'selrow');
            if (id) {
                var ret = jQuery("#grid_ds_dachon_taohanngach").jqGrid('getRowData', id);

                var datarow = {
                    c_donhang_id: ret.c_donhang_id
                    , c_danhsachdathang_id: ret.c_danhsachdathang_id
                    , c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_dongdsdh_id: ret.c_dongdsdh_id
                    , md_sanpham_id: ret.md_sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , ma_sanpham_khach: ret.ma_sanpham_khach
                    , sl_po: ret.sl_po
                    , sl_dathang: ret.sl_dathang
                    , sl_nhapthucte: ret.sl_conlai
                    , sl_conlai: ret.sl_conlai
                };
                jQuery("#grid_ds_dachon_taohanngach").jqGrid('delRowData', id);
                jQuery("#grid_ds_chitietdathang_taohanngach").jqGrid('addRowData', id, datarow);
            } else { alert("Please select row"); }
        });
    });


    function funcShowDialogHanNgach()
    {
        var c_danhsachdathang_id = jQuery("#grid_ds_dathang_taohanngach").jqGrid('getGridParam', 'selrow');
        var sodsdh = $('#grid_ds_dathang_taohanngach').jqGrid('getCell', c_danhsachdathang_id, 'sochungtu');
        var table = '<table id="table_taohanngach">' +
            '<tr>' +
                    '<td>Danh Sách Đặt Hàng</td>' +
                    '<td><input type="hidden" value="' + c_danhsachdathang_id + '" id="txtDanhSachDatHang" /><label id="lblDanhSachDatHang"><b>' + sodsdh + '</b></label</td>' +
            '</tr>' +
            /*'<tr>' +
                    '<td>Phiếu tăng</td>' +
                    '<td><input type="checkbox" id="chkPhieuTang" /></td>' +
                '</tr>' +
            '<tr>' +*/
            '<tr>' +
                '<td>Lí do</td>' +
                '<td><input id="txtLiDo" /></td>' +
            '</tr>' +
                '<td>Ngày lập</td>' +
                '<td><input class="date" id="txtNgayLap" /></td>' +
            '</tr>' +
            '<tr>' +
                '<td>Ngày trình</td>' +
                '<td><input class="date" id="txtNgayTrinh" /></td>' +
            '</tr>' +
            '<tr>' +
                '<td>Ngày duyệt</td>' +
                '<td><input class="date" id="txtNgayDuyet" /></td>' +
            '</tr>' +
                
                
            '</table>';
        $('body').append('<div id="dialogCreateHanNgach" title="Tạo Hạn Ngạch"></div>');
        $('#dialogCreateHanNgach').append('<div id="dialog-caution"></div>');
        $('#dialogCreateHanNgach').append(table);

        $('#table_taohanngach .date').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
        $('#table_taohanngach .date').datepicker('setDate', new Date());

        $('#dialogCreateHanNgach').dialog({
            modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                },
            buttons: [
                {
                    id: 'btnThoat'
                    , text: 'Thoát'
                    , click: function () {
                        $(this).dialog("destroy").remove();
                    }
                },
                {
                    id: 'btnTaoHanNgach'
                    , text: 'Tạo Hạn Ngạch'
                    , click: function () {
                        $('#dialogCreateHanNgach').find('#dialog-caution').html('Đang tạo hạn ngạch!');
                        var gridData = jQuery('#grid_ds_dachon_taohanngach').getRowData();
                        var postData = JSON.stringify(gridData);

                        //var chkPhieuTang = $('#chkPhieuTang').is(":checked");
                        var chkPhieuTang = false;
                        var txtDanhSachDatHang = $('#txtDanhSachDatHang').val();
                        var txtNgayLap = $('#txtNgayLap').val();
                        var txtNgayTrinh = $('#txtNgayTrinh').val();
                        var txtNgayDuyet = $('#txtNgayDuyet').val();
                        var txtLiDo = $('#txtLiDo').val();
                        $.ajax({
                            url: "Controllers/PhieuHanNgachController.ashx?action=TaoHanNgach",
                            type: "POST",
                            datetype: "json",
                            data: {
                                grid_form: postData
                                , c_danhsachdathang_id: txtDanhSachDatHang
                                , ngaylap: txtNgayLap
                                , ngaytrinh: txtNgayTrinh
                                , ngayduyet: txtNgayDuyet
                                , lido: txtLiDo
                                , phieutang: chkPhieuTang
                            },
                            error: function (rs) {
                                Popup("Error", 500, 400, rs.responseText);
                            },
                            success: function (rs) {
                                $('#dialogCreateHanNgach').find('#dialog-caution').html(rs);
                            }

                        });
                    }
                }
            ]
        });
    }

</script>


<DIV class="ui-layout-north ui-widget-content" id="layout-north-taohanngach-pi">
    <uc1:jqGrid  ID="grid_ds_dathang_taohanngach" 
            Caption="Danh Sách Đặt Hàng"
            SortName="c_danhsachdathang_id" 
            UrlFileAction="Controllers/DanhSachDatHangTaoHanNgachController.ashx" 
                        ColNames="['c_danhsachdathang_id','Số Chứng Từ', 'Ngày Lập', 'Hạn Giao Hàng PO', 'Người Phụ Trách', 'Người Đặt Hàng', 'Đơn Hàng', 'Hướng Dẫn Làm Hàng', 'Trạng Thái', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'c_danhsachdathang_id'
                , index: 'dsdh.c_danhsachdathang_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'sochungtu'
                 , index: 'dsdh.sochungtu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ngaylap'
                 , index: 'dsdh.ngaylap'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                 fixed: true, name: 'hangiaohang_po'
                 , index: 'dsdh.hangiaohang_po'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                fixed: true, name: 'nguoi_phutrach'
                , index: 'dsdh.nguoi_phutrach'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
			
            {
                 fixed: true, name: 'nguoi_dathang'
                 , index: 'dsdh.nguoi_dathang'
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
                 fixed: true, name: 'md_trangthai_id'
                 , index: 'dsdh.md_trangthai_id'
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
                fixed: true, name: 'huongdanhlamhang'
                , index: 'dsdh.huongdanhlamhang'
                , width: 100
                , editable:true
                , edittype: 'textarea'
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
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_chitietdathang_taohanngach').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_chitietdathang_taohanngach').jqGrid('setGridParam',{url:'Controllers/DongDSDHTaoHanNgachController.ashx?c_danhsachdathang_id='+ids,page:1});
				                $('#grid_ds_chitietdathang_taohanngach').jqGrid().trigger('reloadGrid');
				                
				                var gridData = jQuery('#grid_ds_dachon_taohanngach')
                                var ids = gridData.jqGrid('getDataIDs');
                                for (var i = 0, il = ids.length; i &lt; il; i++) {
                                    gridData.jqGrid('delRowData', ids[i]);
                                }
				            } 
				        } else {
			                $('#grid_ds_chitietdathang_taohanngach').jqGrid('setGridParam',{url:'Controllers/DongDSDHTaoHanNgachController.ashx?c_danhsachdathang_id='+ids,page:1});
			                $('#grid_ds_chitietdathang_taohanngach').jqGrid().trigger('reloadGrid');
			                
			                    var gridData = jQuery('#grid_ds_dachon_taohanngach')
                                var ids = gridData.jqGrid('getDataIDs');
                                for (var i = 0, il = ids.length; i &lt; il; i++) {
                                    gridData.jqGrid('delRowData', ids[i]);
                                }			
		                } }"
            runat="server" />
</DIV>


<DIV class="ui-layout-center ui-widget-content" id="layout-center-taohanngach-pi">
    
    <DIV class="ui-layout-west ui-widget-content" id="layout-west-soanthao-taohanngach-pi">
                <uc1:jqGrid  ID="grid_ds_chitietdathang_taohanngach" 
            SortOrder="asc"
            SortName="sp.ma_sanpham"  
            UrlFileAction="Controllers/DongDSDHTaoHanNgachController.ashx" 
            ColNames="['c_dongdsdh_id', 'Mã SP', 'Mã SP', 'Mã SP Khách', 'Số Lượng Đặt Hàng', 'Số Lượng Còn Lại']"
            RowNumbers="true"
            ColModel = "[
                {
                    fixed: true, name: 'c_dongdsdh_id'
                    , index: 'ddsdh.c_dongdsdh_id'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                    , hidden: true
                },
                {
                    fixed: true, name: 'md_sanpham_id'
                    , index: 'sp.md_sanpham_id'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                    , hidden: true
                },
                {
                    fixed: true, name: 'ma_sanpham'
                    , index: 'sp.ma_sanpham'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                },
                {
                    fixed: true, name: 'ma_sanpham_khach'
                    , index: 'ddsdh.ma_sanpham_khach'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                    , hidden:true
                },
                {
                    fixed: true, name: 'sl_dathang'
                    , index: 'ddsdh.sl_dathang'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                },
                {
                    fixed: true, name: 'sl_conlai'
                    , index: 'ddsdh.sl_conlai'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                }
            ]"
			RowNum="200"
            Caption = "Danh sách sản phẩm"
			FilterToolbar = "true"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            Height = "150"
            MultiSelect = "false" 
            runat="server" />
    </DIV>
    
    <DIV class="ui-layout-east ui-widget-content" id="layout-east-soanthao-taohanngach-pi">
         <script>
             $(document).ready(function () {
                 var lastSel = -1;
                 jQuery("#grid_ds_dachon_taohanngach").jqGrid({
                     datatype: "local",
                     height: 250,
                     colModel: [
                                {
                                    fixed: true
                                    , label : 'Dòng Danh Sách Đặt Hàng'
                                    , name: 'c_dongdsdh_id'
                                    , index: 'c_dongdsdh_id'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                    , hidden: true
                                },
                                {
                                    fixed: true
                                    , label: 'Mã SP'
                                    , name: 'md_sanpham_id'
                                    , index: 'md_sanpham_id'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                    , hidden: true
                                },
                                {
                                    fixed: true
                                    , label: 'Mã SP'
                                    , name: 'ma_sanpham'
                                    , index: 'ma_sanpham'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                },
                                {
                                    fixed: true
                                    , label: 'Mã SP Khách'
                                    , name: 'ma_sanpham_khach'
                                    , index: 'ma_sanpham_khach'
                                    , width: 100
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                },
                                {
                                    fixed: true
                                    , label: 'Số Lượng Cần Làm'
                                    , name: 'sl_canlam'
                                    , index: 'sl_canlam'
                                    , width: 100
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                    , editrules: { required: true, integer: true }
                                    , align: 'right'
                                    , hidden: true
                                },
                                {
                                    fixed: true
                                    , label: 'Số Lượng Đổi HN'
                                    , name: 'sl_thaydoi'
                                    , index: 'sl_thaydoi'
                                    , width: 100
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                    , editrules: { required: true, integer: true }
                                    , align: 'right'
                                },
                                {
                                    fixed: true
                                    , label: 'Ghi Chú'
                                    , name: 'ghichu'
                                    , index: 'ghichu'
                                    , width: 100
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                },
                                {
                                    fixed: true
                                    , label: 'SL Đặt Hàng'
                                    , name: 'sl_dathang'
                                    , index: 'sl_dathang'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                    , align: 'right'
                                },
                                {
                                    fixed: true
                                    , label: 'SL Còn Lại'
                                    , name: 'sl_conlai'
                                    , index: 'sl_conlai'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                    , align: 'right'
                                }
                                
                     ],
                     autowidth: true,
                     multiselect: false,
                     caption: "Danh sách sản phẩm đã chọn",
                     height: 150,
                     gridComplete: function () { var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90); },
                     showRefresh: false,
					 
                     rowNumbers: true,
                     cellsubmit: 'clientArray',
                     onSelectRow: function (id) {
                         //if (id && id !== lastSel) {
                             jQuery("#grid_ds_dachon_taohanngach").jqGrid('restoreRow', lastSel);
                             jQuery("#grid_ds_dachon_taohanngach").jqGrid('editRow', id, true, null, null, 'clientArray');
                             lastSel = id;
                         //}
                     },
                     pager: $('#grid_ds_dachon_taohanngach-pager')
                 });
				 
				 jQuery("#grid_ds_dachon_taohanngach").jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
				 
                 $('#grid_ds_dachon_taohanngach').jqGrid('navGrid', '#grid_ds_dachon_taohanngach-pager', { add: false, edit: false, del: false, search: false, refresh: false }, { height: 280, reloadAfterSubmit: false }, { height: 280, reloadAfterSubmit: false }, { reloadAfterSubmit: false }, {});

                 $("#grid_ds_dachon_taohanngach").jqGrid('navButtonAdd', '#grid_ds_dachon_taohanngach-pager', {
                     caption: ""
                    , title: "Tạo hạn ngạch"
                    , buttonicon: "icon-pi"
                    , onClickButton: function () {
						var ids = $("#grid_ds_dachon_taohanngach").jqGrid('getDataIDs');
						for(var i in ids) {
							$("#grid_ds_dachon_taohanngach").jqGrid('saveRow', ids[i], true, 'clientArray', null, null);
						}
                        funcShowDialogHanNgach();
                    }
                 });
             });
        </script>
        <table class="ui-layout-center" id="grid_ds_dachon_taohanngach"></table>
        <div id="grid_ds_dachon_taohanngach-pager"></div>
    </DIV>
    
    <DIV class="ui-layout-center ui-widget-content" id="layout-center-soanthao-taohanngach-pi" style="background: #E0CFC2">
        <button id="add-all-taohanngach" class="btn-active">>></button>
        <button id="add-taohanngach" class="btn-active">></button>
        <button id="back-all-taohanngach" class="btn-active"><<</button>
        <button id="back-taohanngach" class="btn-active"><</button>
    </DIV>
</DIV>


