<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phieunhapxuat.aspx.cs" Inherits="inc_phieunhapxuat" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function() {
        // layout outer
        $('#lay-center-phieunhapxuat').parent().layout({
            north: {
                size: "50%"
                , onresize_end: function() {
                    var o = $("#lay-north-phieunhapxuat").height();
                    $("#grid_ds_po_phieunhapxuat").setGridHeight(o - 90);
                }
            }
        });

        // layout inner
        $('#lay-center-phieunhapxuat').layout({
            west: {
                size: "48.5%"
                , onresize_end: function() {
                    var h = $("#lay-west-put").height();
                    var w = $("#lay-west-put").width();
                    $("#grid_ds_chitietpo_phieunhapxuat").setGridHeight(h - 70);
                    $("#grid_ds_chitietpo_phieunhapxuat").setGridWidth(w - 2);
                }
            }
            , east: {
                size: "48.5%"
                , onresize_end: function() {
                    var h = $("#lay-east-put").height();
                    var w = $("#lay-east-put").width();
                    $("#grid_ds_dachon_phieunhapxuat").setGridHeight(h - 70);
                    $("#grid_ds_dachon_phieunhapxuat").setGridWidth(w - 2);
                }
            }
        });

        $(".btn-active-put").button().css({ 'width': '100%', 'height': '25%' });

        $('#add-all').click(function() {
        var gridData = jQuery("#grid_ds_chitietpo_phieunhapxuat")
            var ids = gridData.jqGrid('getDataIDs');
            for (var i = 0, il = ids.length; i < il; i++) {
                var ret = gridData.jqGrid('getRowData', ids[i], true);
                var datarow = {
                    c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_donhang_id: ret.c_donhang_id
                    , sanpham_id: ret.sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , mota_tiengviet: ret.mota_tiengviet
                    , md_doitackinhdoanh_id: ret.md_doitackinhdoanh_id
                    , soluong: ret.soluong
                    , soluong_yeucauxuat: ret.soluong_conlai
                    , soluong_daxuat: ret.soluong_daxuat
                };
                $("#grid_ds_dachon_phieunhapxuat").jqGrid('addRowData', ids[i], datarow); ;
                gridData.jqGrid('delRowData', ids[i]);
            }
        });

        $('#add').click(function() {
        var id = jQuery("#grid_ds_chitietpo_phieunhapxuat").jqGrid('getGridParam', 'selrow');
            if (id) {
                var ret = jQuery("#grid_ds_chitietpo_phieunhapxuat").jqGrid('getRowData', id);
                var datarow = {
                    c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_donhang_id: ret.c_donhang_id
                    , sanpham_id: ret.sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , mota_tiengviet: ret.mota_tiengviet
                    , md_doitackinhdoanh_id: ret.md_doitackinhdoanh_id
                    , soluong: ret.soluong
                    , soluong_yeucauxuat: ret.soluong_conlai
                    , soluong_daxuat: ret.soluong_daxuat
                };
                jQuery("#grid_ds_chitietpo_phieunhapxuat").jqGrid('delRowData', id);
                jQuery("#grid_ds_dachon_phieunhapxuat").jqGrid('addRowData', id, datarow);
            } else { alert("Please select row"); }
        });

        $('#back-all').click(function() {
        var gridData = jQuery("#grid_ds_dachon_phieunhapxuat")
            var ids = gridData.jqGrid('getDataIDs');
            for (var i = 0, il = ids.length; i < il; i++) {
                var ret = gridData.jqGrid('getRowData', ids[i], true);
                var datarow = {
                    c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_donhang_id: ret.c_donhang_id
                    , sanpham_id: ret.sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , mota_tiengviet: ret.mota_tiengviet
                    , md_doitackinhdoanh_id: ret.md_doitackinhdoanh_id
                    , soluong: ret.soluong
                    , soluong_daxuat: ret.soluong_daxuat
                    , soluong_conlai: ret.soluong - ret.soluong_daxuat
                };
                $("#grid_ds_chitietpo_phieunhapxuat").jqGrid('addRowData', ids[i], datarow); ;
                gridData.jqGrid('delRowData', ids[i]);
            }
        });

        $('#back').click(function() {
        var id = jQuery("#grid_ds_dachon_phieunhapxuat").jqGrid('getGridParam', 'selrow');
            if (id) {
                var ret = jQuery("#grid_ds_dachon_phieunhapxuat").jqGrid('getRowData', id);

                var datarow = {
                    c_dongdonhang_id: ret.c_dongdonhang_id
                    , c_donhang_id: ret.c_donhang_id
                    , sanpham_id: ret.sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , mota_tiengviet: ret.mota_tiengviet
                    , md_doitackinhdoanh_id: ret.md_doitackinhdoanh_id
                    , soluong: ret.soluong
                    , soluong_daxuat: ret.soluong_daxuat
                    , soluong_conlai: ret.soluong - ret.soluong_daxuat
                };
                jQuery("#grid_ds_dachon_phieunhapxuat").jqGrid('delRowData', id);
                jQuery("#grid_ds_chitietpo_phieunhapxuat").jqGrid('addRowData', id, datarow);
            } else { alert("Please select row"); }
        });


    });


    function funcCreatePhieuXN(postData) {
        var msg = "";
        var gridData = jQuery("#grid_ds_dachon_phieunhapxuat")
        var ids = gridData.jqGrid('getDataIDs');

        if (ids.length == 0) {
            msg = "Hãy chọn danh sách sản phẩm mà bạn muốn tạo PI.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-create-pi\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-create-pi').dialog({
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
            msg = "Có phải bạn muốn PI từ danh sách sản phẩm này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-create-pi\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-create-pi').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-create-pi",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-create-pi").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });
                            alert(postData);
                            // $.get("Controllers/ChiaTachPOController.ashx?action=activepo&poId=" + id, function(result) {
                            //     //$('#dialog-caution').css({ 'color': 'green' });
                            //     $('#dialog-active-po').find('#dialog-caution').html(result);
                            //     $('#grid_ds_po').jqGrid().trigger('reloadGrid');
                            // });
                            //                            $.ajax({
                            //                                url: "Controllers/ChiaTachPOController.ashx",
                            //                                type: "POST",
                            //                                datetype: "json",
                            //                                data: { grid_form: postData, action: "SaveDatHang" },
                            //                                error: function(rs) {
                            //                                    Popup("Error", 500, 400, rs.responseText);
                            //                                },
                            //                                success: function(rs) {
                            //                                    $('#dialog-create-pi').find('#dialog-caution').html(rs);
                            //                                }

                            //                            });
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
    
    
</script>

<div class="ui-layout-center ui-widget-content" id="lay-center-phieunhapxuat">
   
    <div class="ui-layout-west ui-widget-content" id="lay-west-put">
            <uc1:jqGrid  ID="grid_ds_chitietpo_phieunhapxuat" 
            SortName="ddh.c_dongdonhang_id" 
            UrlFileAction="Controllers/DongDonHangPhieuXuatController.ashx" 
            ColNames="['c_dongdonhang_id', 'Đơn Hàng', 'Mã SP'
                    , 'Mã SP', 'Mô Tả TV', 'Số Lượng Đơn Hàng'
                    , 'Số Lượng Đã Xuất', 'Số Lượng Còn Lại']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
            ColModel = "[
            {
                fixed: true, name: 'c_dongdonhang_id'
                , index: 'c_dongdonhang_id'
                , width: 100
                , editable:true
                , edittype: 'text'
                , sortable: false
                , hidden: true
            },
            {
                fixed: true, name: 'c_donhang_id'
                , index: 'dh.c_donhang_id'
                , width: 100
                , edittype: 'text'
                , editable:true
                , sortable: false
                , hidden: true
            },
            {
                fixed: true, name: 'sanpham_id'
                , index: 'sp.sanpham_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , sortable: false
            },
            {
                fixed: true, name: 'ma_sanpham'
                , index: 'sp.ma_sanpham'
                , width: 100
                , edittype: 'text'
                , editable:true
                , sortable: false
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
                , index: 'sp.mota_tiengviet'
                , width: 100
                , edittype: 'text'
                , editable:true
                , sortable: false
            },
            {
                fixed: true, name: 'soluong'
                , index: 'ddh.soluong'
                , width: 100
                , edittype: 'text'
                , editable:true
                , sortable: false
            },
            {
                fixed: true, name: 'soluong_daxuat'
                , index: 'ddh.soluong_daxuat'
                , width: 100
                , edittype: 'text'
                , editable:true
                , sortable: false
            },
            {
                fixed: true, name: 'soluong_conlai'
                , index: 'ddh.soluong_conlai'
                , width: 100
                , edittype: 'text'
                , editable:true
                , sortable: false
            }
            ]"
            Caption = "Danh sách sản phẩm"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 70);"
            ShowRefresh = "false"
            Height = "150"
            MultiSelect = "false" 
            
            runat="server" />
    </div>
    
    <div class="ui-layout-center ui-widget-content" id="lay-center-put">
            <button id="add-all" class="btn-active-put">>></button>
            <button id="add" class="btn-active-put">></button>
            <button id="back-all" class="btn-active-put"><<</button>
            <button id="back" class="btn-active-put"><</button>
    </div>
    
    <div class="ui-layout-east ui-widget-content" id="lay-east-put">
            <script>
                $(document).ready(function() {
                    jQuery("#grid_ds_dachon_phieunhapxuat").jqGrid({
                        datatype: "local",
                        height: 250,
                        colNames: ['c_dongdonhang_id', 'Đơn Hàng', 'Mã SP'
                    , 'Mã SP', 'Mô Tả TV', 'Số Lượng Đơn Hàng'
                    , 'Số Lượng Yêu Cầu Xuất', 'Số Lượng Đã Xuất'],
                        colModel: [
                                {
                                    fixed: true, name: 'c_dongdonhang_id'
                                    , index: 'c_dongdonhang_id'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                    , hidden: true
                                },
                                {
                                    fixed: true, name: 'c_donhang_id'
                                    , index: 'dh.c_donhang_id'
                                    , width: 100
                                    , edittype: 'select'
                                    , editable: false
                                    , editoptions: { dataUrl: 'Controllers/DonHangController.ashx?action=getoption' }
                                    , sortable: false
                                    , hidden: true
                                },
                                {
                                    fixed: true, name: 'sanpham_id'
                                    , index: 'sp.sanpham_id'
                                    , width: 100
                                    , hidden: true
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                },
                                {
                                    fixed: true, name: 'ma_sanpham'
                                    , index: 'sp.ma_sanpham'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: false
                                    , sortable: false
                                    , editoptions: {
                                        dataInit: function(elem) {
                                            $(elem).combogrid({
                                                searchIcon: true,
                                                width: '480px',
                                                url: 'Controllers/ProductController.ashx?action=getcombogrid',
                                                colModel: [
                                                    { 'columnName': 'md_sanpham_id', 'width': '0', 'label': 'ID', hidden: true }
                                                  , { 'columnName': 'ma_sanpham', 'width': '50', 'label': 'Mã SP', 'align': 'left' }
                                                  , { 'columnName': 'mota_tiengviet', 'width': '50', 'label': 'Mô Tả TV', 'align': 'left'}],
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
                                    , index: 'sp.mota_tiengviet'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: false
                                    , sortable: false
                                },
                                {
                                    fixed: true, name: 'soluong'
                                    , index: 'ddh.soluong'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: false
                                    , sortable: false
                                },
                                {
                                    fixed: true, name: 'soluong_yeucauxuat'
                                    , index: 'ddh.soluong_yeucauxuat'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: true
                                    , sortable: false
                                    , editrules: { required: true, number: true }
                                },
                                {
                                    fixed: true, name: 'soluong_daxuat'
                                    , index: 'ddh.soluong_daxuat'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: false
                                    , sortable: false
                                    , hidden: true
                                }
                                ],
                        autowidth: true,
                        multiselect: false,
                        caption: "Danh sách sản phẩm đã chọn",
                        height: 150,
                        gridComplete: function() { var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 70); },
                        showRefresh: false,
                        rowNumbers: true,
                        cellEdit: true,
                        cellsubmit: 'clientArray',
                        pager: $('#grid_ds_dachon_phieunhapxuat-pager')
                    });
                    $('#grid_ds_dachon_phieunhapxuat').jqGrid('navGrid', '#grid_ds_dachon_phieunhapxuat-pager', { add: false, edit: false, del: false, search: false, refresh: false }, { height: 280, reloadAfterSubmit: false }, { height: 280, reloadAfterSubmit: false }, { reloadAfterSubmit: false }, {});

                    $("#grid_ds_dachon_phieunhapxuat").jqGrid('navButtonAdd', '#grid_ds_dachon_phieunhapxuat-pager', {
                        caption: ""
                    , title: "Tạo Phiếu Xác Nhận"
                    , buttonicon: "icon-pi"
                    , onClickButton: function() {
                        var gridData = jQuery(this).getRowData();
                        var postData = JSON.stringify(gridData);
                        funcCreatePhieuXN(postData);
                    }
                    });
                });
        </script>
        <table class="ui-layout-center" id="grid_ds_dachon_phieunhapxuat"></table>
        <div id="grid_ds_dachon_phieunhapxuat-pager"></div>
    </div>
    
</div>

<div class="ui-layout-north" id="lay-north-phieunhapxuat">
    <uc1:jqGrid  ID="grid_ds_po_phieunhapxuat" 
            Caption="Đơn Hàng"
            SortName="c_donhang_id" 
            UrlFileAction="Controllers/DonHangController.ashx" 
            AfterRefresh = "function(){ 
                var gridData = jQuery('#grid_ds_dachon_chiatachpo')
                var ids = gridData.jqGrid('getDataIDs');
                for (var i = 0, il = ids.length; i &lt; il; i++) {
                    gridData.jqGrid('delRowData', ids[i]);
                }
                jQuery('#grid_ds_chitietpo_chiatachpo').jqGrid().trigger('reloadGrid');
            }"
            PostData = "'status' : 'hieuluc'"
            ColNames="['c_donhang_id','Số Chứng Từ','Khách Hàng'
                , 'Ngày Lập', 'Người Lập', 'Cảng Biển'
                , 'Discount', 'Shipment Date'
                , 'Shipment Time', 'Paymentterm'
                , 'Tr.Lượng', 'Đồng Tiền', 'Kích Thước', 'Payer'
                , 'PortDischarge', 'Amount', 'Total CBM', 'Total CBF'
                , 'Make PI', 'Bảng Giá','Trạng Thái', 'Ngày Tạo'
                , 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật'
                , 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'c_donhang_id'
                , index: 'c_donhang_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'sochungtu'
                 , index: 'sochungtu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
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
                 fixed: true, name: 'ngaylap'
                 , index: 'ngaylap'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
            },
            {
                 fixed: true, name: 'nguoilap'
                 , index: 'nguoilap'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'md_cangbien_id'
                 , index: 'cbien.ten_cangbien'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PortController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'discount'
                 , index: 'discount'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'shipmentdate'
                 , index: 'shipmentdate'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
            },
            {
                 fixed: true, name: 'shipmenttime'
                 , index: 'shipmenttime'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
            },
            {
                 fixed: true, name: 'md_paymentterm_id'
                 , index: 'pmt.ten_paymentterm'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PaymentTermController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'md_trongluong_id'
                 , index: 'tl.ten_trongluong'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/TrongLuongController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'md_dongtien_id'
                 , index: 'dtien.ma_iso'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'md_kichthuoc_id'
                 , index: 'kt.ten_kichthuoc'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/KichThuocController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'payer'
                 , index: 'payer'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'portdischarge'
                 , index: 'portdischarge'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'amount'
                 , index: 'amount'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'totalcbm'
                 , index: 'totalcbm'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'totalcbf'
                 , index: 'totalcbf'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ismakepi'
                 , index: 'ismakepi'
                 , editable: true
                 , width: 100
                 , edittype: 'checkbox'
            },
            {
                 fixed: true, name: 'md_banggia_id'
                 , index: 'bgia.md_banggia_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PriceListController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'md_trangthai_id'
                 , index: 'md_trangthai_id'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
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
                , hidden: true
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()-90);"
            OnSelectRow = "function(ids) {
                        
                        
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_chitietpo_phieunhapxuat').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_chitietpo_phieunhapxuat').jqGrid('setGridParam',{url:'Controllers/DongDonHangPhieuXuatController.ashx?&poId='+ids,page:1});
				                $('#grid_ds_chitietpo_phieunhapxuat').jqGrid().trigger('reloadGrid');
				                
				            } 
				        } else {
			                $('#grid_ds_chitietpo_phieunhapxuat').jqGrid('setGridParam',{url:'Controllers/DongDonHangPhieuXuatController.ashx?&poId='+ids,page:1});
			                $('#grid_ds_chitietpo_phieunhapxuat').jqGrid().trigger('reloadGrid');	
			                
		                } }"
            FilterToolbar = "true"
            Height = "420"
            runat="server" />
</div>