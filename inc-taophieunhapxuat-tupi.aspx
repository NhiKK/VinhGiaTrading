<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-taophieunhapxuat-tupi.aspx.cs" Inherits="inc_taophieunhapxuat_tupi" %>

<%@ Register Src="jqGrid.ascx" TagName="jqGrid" TagPrefix="uc1" %>
<script>
    $(document).ready(function () {
        $('#layout-center-taophieunhapxuat-pi').parent().layout({
            north: {
                size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function () {
                    var o = $("#layout-north-taophieunhapxuat-pi").height();
                    $("#grid_ds_dathang_taophieunhapxuat").setGridHeight(o - 90);
                }
            }
        });


        // Soan Thao
        $('#layout-center-taophieunhapxuat-pi').layout({
            west: {
                size: "48.5%"
                , onresize_end: function () {
                    var h = $("#layout-west-soanthao-taophieunhapxuat-pi").height();
                    var w = $("#layout-west-soanthao-taophieunhapxuat-pi").width();
                    $("#grid_ds_chitietdathang_taophieunhapxuat").setGridHeight(h - 70);
                    $("#grid_ds_chitietdathang_taophieunhapxuat").setGridWidth(w - 2);
                }
            }
            , east: {
                size: "48.5%"
                , onresize_end: function () {
                    var h = $("#layout-east-soanthao-taophieunhapxuat-pi").height();
                    var w = $("#layout-east-soanthao-taophieunhapxuat-pi").width();
                    $("#grid_ds_dachon_taophieunhapxuat").setGridHeight(h - 70);
                    $("#grid_ds_dachon_taophieunhapxuat").setGridWidth(w - 2);
                }
            }
        });




        $(".btn-active").button().css({ 'width': '100%', 'height': '25%' });

        $('#add-all-grid_ds_dachon_taophieunhapxuat').click(function () {
            var idExist = jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid('getDataIDs');
            var gridData = jQuery("#grid_ds_chitietdathang_taophieunhapxuat")
            var ids = gridData.jqGrid('getDataIDs');
            for (var i = 0, il = ids.length; i < il; i++) {
                var ret = gridData.jqGrid('getRowData', ids[i], true);
                var datarow = {
                    c_donhang_id: ret.c_donhang_id
                    , c_danhsachdathang_id: ret.c_danhsachdathang_id
                    , c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_dongdsdh_id: ret.c_dongdsdh_id
                    , md_sanpham_id: ret.md_sanpham_id
                    , tt: 1 + i
                    , ma_sanpham: ret.ma_sanpham
                    , ma_sanpham_khach: ret.ma_sanpham_khach
                    , sl_po: ret.sl_po
                    , sl_dathang: ret.sl_dathang
                    , sl_nhapthucte: ret.sl_conlai
                    , sl_conlai: ret.sl_conlai
                };
                $("#grid_ds_dachon_taophieunhapxuat").jqGrid('addRowData', ids[i], datarow);;
                gridData.jqGrid('delRowData', ids[i]);
            }
        });

        $('#add-grid_ds_dachon_taophieunhapxuat').click(function () {
            var idExist = jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid('getDataIDs');
            var id = jQuery("#grid_ds_chitietdathang_taophieunhapxuat").jqGrid('getGridParam', 'selrow');
            if (id) {
                var ret = jQuery("#grid_ds_chitietdathang_taophieunhapxuat").jqGrid('getRowData', id);
                var datarow = {
                    c_donhang_id: ret.c_donhang_id
                    , c_danhsachdathang_id: ret.c_danhsachdathang_id
                    , c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_dongdsdh_id: ret.c_dongdsdh_id
                    , md_sanpham_id: ret.md_sanpham_id
                    , tt: 1
                    , ma_sanpham: ret.ma_sanpham
                    , ma_sanpham_khach: ret.ma_sanpham_khach
                    , sl_po: ret.sl_po
                    , sl_dathang: ret.sl_dathang
                    , sl_nhapthucte: ret.sl_conlai
                    , sl_conlai: ret.sl_conlai
                };
                jQuery("#grid_ds_chitietdathang_taophieunhapxuat").jqGrid('delRowData', id);
                jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid('addRowData', id, datarow);
            } else { alert("Please select row"); }
        });

        $('#back-all-grid_ds_dachon_taophieunhapxuat').click(function () {
            var gridData = jQuery("#grid_ds_dachon_taophieunhapxuat")
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
                $("#grid_ds_chitietdathang_taophieunhapxuat").jqGrid('addRowData', ids[i], datarow);;
                gridData.jqGrid('delRowData', ids[i]);
            }
        });

        $('#back-grid_ds_dachon_taophieunhapxuat').click(function () {
            var gridData = jQuery("#grid_ds_dachon_taophieunhapxuat");
            var id = gridData.jqGrid('getGridParam', 'selrow');
            if (id) {
                var ret = jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid('getRowData', id);

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

                jQuery("#grid_ds_chitietdathang_taophieunhapxuat").jqGrid('addRowData', id, datarow);

                gridData.jqGrid('delRowData', id);
            } else { alert("Please select row"); }
        });
    });


    function funcCreatePI(postData) {
        var msg = "";
        var gridData = jQuery("#grid_ds_dachon_taophieunhapxuat");
        var ids = gridData.jqGrid('getDataIDs');

        if (ids.length == 0) {
            msg = "Hãy chọn danh sách sản phẩm mà bạn muốn tạo phiếu xuất.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-taophieunhapxuat-tupi\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-taophieunhapxuat-tupi').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function () {
                        $(this).dialog("destroy").remove();
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn phiếu xuất từ danh sách sản phẩm này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-taophieunhapxuat-tupi\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-taophieunhapxuat-tupi').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-dialog-taophieunhapxuat-tupi",
                        text: "Có",
                        click: function () {
                            $("#dialog-caution").ajaxStart(function () {
                                $("#wait").css("display", "block");
                                $("#btn-dialog-taophieunhapxuat-tupi").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function () {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            // $.get("Controllers/ChiaTachPOController.ashx?action=activepo&poId=" + id, function(result) {
                            //     //$('#dialog-caution').css({ 'color': 'green' });
                            //     $('#dialog-active-po').find('#dialog-caution').html(result);
                            //     $('#grid_ds_po').jqGrid().trigger('reloadGrid');
                            // });
                            //alert(postData);
                            $.ajax({
                                url: "Controllers/TaoPhieuNhapKho.ashx",
                                type: "POST",
                                datetype: "json",
                                data: { grid_form: postData, action: "TaoPhieuNhap" },
                                error: function (rs) {
                                    Popup("Error", 500, 400, rs.responseText);
                                },
                                success: function (rs) {
                                    $('#dialog-taophieunhapxuat-tupi').find('#dialog-caution').html(rs);
                                }

                            });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Không",
                        click: function () {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
        }
    }

</script>


<div class="ui-layout-north ui-widget-content" id="layout-north-taophieunhapxuat-pi">
    <uc1:jqGrid ID="grid_ds_dathang_taophieunhapxuat"
        Caption="Danh Sách Đặt Hàng"
        SortName="c_danhsachdathang_id"
        UrlFileAction="Controllers/DanhSachDatHangTaoPhieuXuatController.ashx"
        AfterRefresh="function(){ 
                var gridData = jQuery('#grid_ds_dachon_chiatachpo')
                var ids = gridData.jqGrid('getDataIDs');
                for (var i = 0, il = ids.length; i &lt; il; i++) {
                    gridData.jqGrid('delRowData', ids[i]);
                }
                jQuery('#grid_ds_chitietpo_chiatachpo').jqGrid().trigger('reloadGrid');
            }"
        ColNames="['c_danhsachdathang_id','Số Chứng Từ', 'Ngày Lập', 'Hạn Giao Hàng PO', 'Người Phụ Trách', 'Người Đặt Hàng', 'Đơn Hàng', 'Hướng Dẫn Làm Hàng', 'Trạng Thái', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
        RowNumbers="true"
        ColModel="[
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
        GridComplete="var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
        FilterToolbar="true"
        Height="420"
        OnSelectRow="function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_chitietdathang').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_chitietdathang_taophieunhapxuat').jqGrid('setGridParam',{url:'Controllers/DongDSDHTaoPhieuXuatController.ashx?&poId='+ids,page:1});
				                $('#grid_ds_chitietdathang_taophieunhapxuat').jqGrid().trigger('reloadGrid');
				                
				                var gridData = jQuery('#grid_ds_dachon_taophieunhapxuat')
                                var ids = gridData.jqGrid('getDataIDs');
                                for (var i = 0, il = ids.length; i &lt; il; i++) {
                                    gridData.jqGrid('delRowData', ids[i]);
                                }
				            } 
				        } else {
			                $('#grid_ds_chitietdathang_taophieunhapxuat').jqGrid('setGridParam',{url:'Controllers/DongDSDHTaoPhieuXuatController.ashx?&poId='+ids,page:1});
			                $('#grid_ds_chitietdathang_taophieunhapxuat').jqGrid().trigger('reloadGrid');
			                
			                    var gridData = jQuery('#grid_ds_dachon_taophieunhapxuat')
                                var ids = gridData.jqGrid('getDataIDs');
                                for (var i = 0, il = ids.length; i &lt; il; i++) {
                                    gridData.jqGrid('delRowData', ids[i]);
                                }			
		                } }"
        runat="server" />
</div>


<div class="ui-layout-center ui-widget-content" id="layout-center-taophieunhapxuat-pi">

    <div class="ui-layout-west ui-widget-content" id="layout-west-soanthao-taophieunhapxuat-pi">
        <uc1:jqGrid ID="grid_ds_chitietdathang_taophieunhapxuat"
            SortOrder="asc"
            SortName="sp.ma_sanpham"
            UrlFileAction="Controllers/DongDSDHTaoPhieuXuatController.ashx"
            ColNames="['c_donhang_id', 'Danh Sách Đặt Hàng', 'Dòng Đơn Hàng', 'Dòng Danh Sách Đặt Hàng', 'Mã SP', 'Mã SP', 'Mã SP Khách', 'Số Lượng PO', 'Số Lượng Đặt Hàng', 'Số Lượng Còn Lại']"
            RowNumbers="true"
            ColModel="[
                {
                    fixed: true, name: 'c_donhang_id'
                    , index: 'ddsdh.c_donhang_id'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                    , hidden:true
                },
                {
                    fixed: true, name: 'c_danhsachdathang_id'
                    , index: 'dsdh.c_danhsachdathang_id'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                    , hidden:true
                },
                {
                    fixed: true, name: 'c_dongdonhang_id'
                    , index: 'ddsdh.c_dongdonhang_id'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                    , hidden:true
                },
                {
                    fixed: true, name: 'c_dongdsdh_id'
                    , index: 'dsdh.c_dongdsdh_id'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                    , hidden:true
                },
                {
                    fixed: true, name: 'md_sanpham_id'
                    , index: 'ddsdh.md_sanpham_id'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                    , hidden:true
                },
                {
                    fixed: true, name: 'ma_sanpham'
                    , index: 'sp.ma_sanpham'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                },
                {
                    fixed: true, name: 'ma_sanpham_khach'
                    , index: 'ddsdh.ma_sanpham_khach'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
                },
                {
                    fixed: true, name: 'sl_po'
                    , index: 'ddsdh.sl_po'
                    , width: 100
                    , editable:true
                    , edittype: 'text'
                    , sortable: false
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
            Caption="Danh sách sản phẩm"
            GridComplete="var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 70);"
            Height="150"
            MultiSelect="false"
            runat="server" />
    </div>

    <div class="ui-layout-east ui-widget-content" id="layout-east-soanthao-taophieunhapxuat-pi">
        <script>
            $(document).ready(function () {
                var lastSel = -1;
                jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid({
                    datatype: "local",
                    height: 250,
                    colNames: ['c_donhang_id', 'c_danhsachdathang_id', 'c_dongdonhang_id', 'c_dongdsdh_id', 'md_sanpham_id', 'TT', 'Mã SP', 'Mã San Phẩm Khách', 'SL PO', 'SL Đặt Hàng', 'SL Còn Lại', 'SL Nhập Thực Tế'],
                    colModel: [
                               {
                                   fixed: true, name: 'c_donhang_id'
                                   , index: 'c_donhang_id'
                                   , width: 100
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                                   , hidden: true
                               },
                               {
                                   fixed: true, name: 'c_danhsachdathang_id'
                                   , index: 'c_danhsachdathang_id'
                                   , width: 100
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                                   , hidden: true
                               },
                               {
                                   fixed: true, name: 'c_dongdonhang_id'
                                   , index: 'c_dongdonhang_id'
                                   , width: 100
                                   , editable: true
                                   , edittype: 'text'
                                   , sortable: false
                                   , hidden: true
                               },
                               {
                                   fixed: true, name: 'c_dongdsdh_id'
                                   , index: 'c_dongdsdh_id'
                                   , width: 100
                                   , editable: true
                                   , edittype: 'text'
                                   , sortable: false
                                   , hidden: true
                               },
                               {
                                   fixed: true, name: 'md_sanpham_id'
                                   , index: 'md_sanpham_id'
                                   , width: 100
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                                   , hidden: true
                               },
                               {
                                   fixed: true, name: 'tt'
                                   , index: ''
                                   , width: 40
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                                   , hidden: false
                               },
                               {
                                   fixed: true, name: 'ma_sanpham'
                                   , index: 'sp.ma_sanpham'
                                   , width: 100
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                               },
                               {
                                   fixed: true, name: 'ma_sanpham_khach'
                                   , index: 'ma_sanpham_khach'
                                   , width: 100
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                               },
                               {
                                   fixed: true, name: 'sl_po'
                                   , index: 'sl_po'
                                   , width: 100
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                                   , align: 'right'
                                   , hidden: true
                               },
                               {
                                   fixed: true, name: 'sl_dathang'
                                   , index: 'sl_dathang'
                                   , width: 100
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                                   , align: 'right'
                               },
                               {
                                   fixed: true, name: 'sl_conlai'
                                   , index: 'sl_conlai'
                                   , width: 100
                                   , editable: false
                                   , edittype: 'text'
                                   , sortable: false
                                   , align: 'right'
                                },
                               {
                                   fixed: true, name: 'sl_nhapthucte'
                                   , index: 'sl_nhapthucte'
                                   , width: 100
                                   , editable: true
                                   , edittype: 'text'
                                   , sortable: false
                                   , align: 'right'
                               }
                    ],
                    autowidth: true,
                    multiselect: false,
                    caption: "Danh sách sản phẩm đã chọn",
                    height: 150,
                    gridComplete: function () { var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 70); },
                    showRefresh: false,
                    rowNumbers: true,
                    RowNum: 200,
                    viewrecords: true,
                    cellsubmit: 'clientArray',
                    onSelectRow: function (id) {
                        //if (id && id !== lastSel) {

                        jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid('restoreRow', lastSel);
                        jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid('editRow', id, true, null, null, 'clientArray');
                        lastSel = id;
                        //}
                    },
                    pager: $('#grid_ds_dachon_taophieunhapxuat-pager')
                });
                $('#grid_ds_dachon_taophieunhapxuat').jqGrid('navGrid', '#grid_ds_dachon_taophieunhapxuat-pager', { add: false, edit: false, del: false, search: false, refresh: false }, { height: 280, reloadAfterSubmit: false }, { height: 280, reloadAfterSubmit: false }, { reloadAfterSubmit: false }, {});

                $("#grid_ds_dachon_taophieunhapxuat").jqGrid('navButtonAdd', '#grid_ds_dachon_taophieunhapxuat-pager', {
                    caption: ""
                   , title: "Tạo phiếu nhập"
                   , buttonicon: "icon-pi"
                   , onClickButton: function () {
                       //var id = jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid('getGridParam', 'selrow');
                       //jQuery("#grid_ds_dachon_taophieunhapxuat").jqGrid('saveRow', id, true, 'clientArray', null, null);
                       var rowids = $("#grid_ds_dachon_taophieunhapxuat").jqGrid('getDataIDs');
                       for (var i in rowids) {
                           $("#grid_ds_dachon_taophieunhapxuat").jqGrid('saveRow', rowids[i], true, 'clientArray', null, null);
                       }
                       var gridData = jQuery(this).getRowData();
                       var postData = JSON.stringify(gridData);
                       //alert(postData);
                       funcCreatePI(postData);
                   }
                });

                $("#grid_ds_dachon_taophieunhapxuat").jqGrid('navButtonAdd', '#grid_ds_dachon_taophieunhapxuat-pager', {
                    caption: ""
                   , title: "SL nhập thực tế = SL còn lại"
                   , buttonicon: "ui-icon-star"
                    , onClickButton: function () {
                        let $gridSav = $(this);
                        let ids = $gridSav.jqGrid('getDataIDs');
                        if (ids.length > 0) {
                            for (let i in ids) {
                                let id = ids[i];
                                let sl_conlai = $gridSav.jqGrid('getCell', id, 'sl_conlai');
                                $gridSav.jqGrid("setCell", id, "sl_nhapthucte", sl_conlai);
                                $gridSav.jqGrid('saveRow', id, true, 'clientArray', null, null);
                            }

                            alert(`Đã cập nhật ${ids.length} dòng.`);
                        }
                   }
                });
            });
        </script>
        <table class="ui-layout-center" id="grid_ds_dachon_taophieunhapxuat"></table>
        <div id="grid_ds_dachon_taophieunhapxuat-pager"></div>
    </div>

    <div class="ui-layout-center ui-widget-content" id="layout-center-soanthao-taophieunhapxuat-pi" style="background: #E0CFC2">
        <button id="add-all-grid_ds_dachon_taophieunhapxuat" class="btn-active">>></button>
        <button id="add-grid_ds_dachon_taophieunhapxuat" class="btn-active">></button>
        <button id="back-all-grid_ds_dachon_taophieunhapxuat" class="btn-active"><<</button>
        <button id="back-grid_ds_dachon_taophieunhapxuat" class="btn-active"><</button>
    </div>
</div>

