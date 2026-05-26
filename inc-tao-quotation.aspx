<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-tao-quotation.aspx.cs" Inherits="inc_tao_quotation" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<style type="text/css">
td#grid_ds_dachon_taoqo-pager_left .ui-pg-div {
    color: #0000ef;
    font-weight: bold;
    padding-right: 3px;
    padding-top: 4px;
	user-select: none;
}
</style>
<script>
    $(document).ready(function() {
        $(".btn-active").button().css({ 'width': '100%', 'height': '25%' });
        $('#layout-center-taoqo').parent().layout({
            west: {
                size: "48,5%"
                , onresize_end: function() {
                    var h = $("#layout-west-taoqo").height();
                    var w = $("#layout-west-taoqo").width();
                    $("#grid_ds_sanpham_taoqo").setGridHeight(h - 90);
                    $("#grid_ds_sanpham_taoqo").setGridWidth(w - 2);
                }
            }
            , east: {
                size: "48,5%"
                , onresize_end: function() {
                    var h = $("#layout-east-taoqo").height();
                    var w = $("#layout-east-taoqo").width();
                    $("#grid_ds_dachon_taoqo").setGridHeight(h - 70);
                    $("#grid_ds_dachon_taoqo").setGridWidth(w - 2);
                }
            }
        });
    });

    $('#add-all-create-qo').click(function() {
        var idExist = jQuery("#grid_ds_dachon_taoqo").jqGrid('getDataIDs');
        var gridData = jQuery("#grid_ds_sanpham_taoqo");
        var ids = gridData.jqGrid('getGridParam', 'selarrrow');

        for (var i = 0, il = ids.length; i < il; i++) {
            var ret = gridData.jqGrid('getRowData', ids[i], true);

            // Kiem tra trong danh sach chon da ton tai ma san pham id chua?
            var next = true;
            if (idExist.length == 0) {
                next = true;
            } else {
                for (var j = 0, jl = idExist.length; j < jl; j++) {
                    if (ret.md_sanpham_id == idExist[j]) {
                        next = false;
                        break;
                    }
                }
            }

            // neu next = true thi tiep tuc add san pham vao
            if (next == true) {
                var datarow = {
                    md_sanpham_id: ret.md_sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , soluong: '1'
					, giafob: ret.giafob
					, ten_donggoi:ret.ten_donggoi
					, v2: ret.v2
					, noofpack: ret.noofpack
                };

                $("#grid_ds_dachon_taoqo").jqGrid('addRowData', ids[i], datarow);
            }
        }
    });


    $('#add-create-qo').click(function() {
        var idExist = jQuery("#grid_ds_dachon_taoqo").jqGrid('getDataIDs');

        var id = jQuery("#grid_ds_sanpham_taoqo").jqGrid('getGridParam', 'selrow');
        if (id) {

            var ret = jQuery("#grid_ds_sanpham_taoqo").jqGrid('getRowData', id);

            // Kiem tra trong danh sach chon da ton tai ma san pham id chua?
            var next = true;
            if (idExist.length == 0) {
                next = true;
            } else {
                for (var i = 0, il = idExist.length; i < il; i++) {
                    if (ret.md_sanpham_id == idExist[i]) {
                        next = false;
                        break;
                    }
                }
            }

            // neu next = true thi tiep tuc add san pham vao
            if (next == true) {
                var datarow = {
                    md_sanpham_id: ret.md_sanpham_id
                    , ma_sanpham: ret.ma_sanpham
                    , soluong: '1'
					, giafob: ret.giafob
					, ten_donggoi:ret.ten_donggoi
					, v2: ret.v2
					, noofpack: ret.noofpack
                };
                jQuery("#grid_ds_dachon_taoqo").jqGrid('addRowData', id, datarow);
            }
        } else { alert("Please select row"); }
    });

        $('#back-all-create-qo').click(function() {
            var gridData = jQuery("#grid_ds_dachon_taoqo")
            var ids = gridData.jqGrid('getDataIDs');
            for (var i = 0, il = ids.length; i < il; i++) {
                gridData.jqGrid('delRowData', ids[i]);
            }
        });

        $('#back-create-qo').click(function() {
            var id = jQuery("#grid_ds_dachon_taoqo").jqGrid('getGridParam', 'selrow');
            if (id) {
                jQuery("#grid_ds_dachon_taoqo").jqGrid('delRowData', id);
            } else { alert("Please select row"); }
        });

        function funcCreateQuotation(postData) {
            var msg = "";
            var gridData = jQuery("#grid_ds_dachon_taoqo")
            var ids = gridData.jqGrid('getDataIDs');

            if (ids.length == 0) {
                msg = "Hãy chọn danh sách sản phẩm mà bạn muốn tạo báo giá.!";
                // append dialog-taoky to body
                $('body').append('<div title="Thông Báo" id=\"dialog-taoquotation\"><h3>' + msg + '</h3></div>');

                // create new dialog
                $('#dialog-taoquotation').dialog({
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
                msg = "Có phải bạn muốn quotation từ danh sách sản phẩm này.?";
                var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
                var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

                // append dialog-taoky to body
                $('body').append('<div title="Thông Báo" id=\"dialog-taoquotation\">' + h3 + '</div>');

                // create new dialog
                $('#dialog-taoquotation').dialog({
                    modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-dialog-taoquotation",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-dialog-taoquotation").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            // $.get("Controllers/ChiaTachPOController.ashx?action=activepo&poId=" + id, function(result) {
                            //     //$('#dialog-caution').css({ 'color': 'green' });
                            //     $('#dialog-active-po').find('#dialog-caution').html(result);
                            //     $('#grid_ds_po').jqGrid().trigger('reloadGrid');
                            // });
                            $.ajax({
                                url: "inc-view_quotation.aspx",
                                type: "POST",
                                datetype: "json",
                                data: { datagrid: postData, action: "TaoPhieuNhap" },
                                error: function(rs) {
                                    Popup("Error", 500, 400, rs.responseText);
                                },
                                success: function(rs) {
                                    var div = $("<div></div>");
                                    div.appendTo($(document.body));
                                    div.html(rs);
                                    div.dialog({
                                        title: "Phân chia mã hàng theo cảng",
                                        resizeable: false,
                                        modal: true,
                                        width: '900px',
                                        height: 550,
                                        close: function(event, ui) { $(this).dialog('destroy').remove(); },
                                        buttons: [
					                        { text: "Tạo QO", id: "create_pos", click: function() { 
												//$('#create_pos').button("disable"); 
												//$('#create_pos').button("option", "label", 'Đang tạo Quotation 
												//<img style="width:25px; height:20px" src="iconcollection/loading.gif"/>');
												createListQO(postData, $("#create_pos")); } },
					                        { text: "Đóng", click: function() { $(this).dialog("destroy").remove(); } }
				                        ]
                                    });
                                }

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

        function funcCreateQuotationExcel() {
            var div = $('<div><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>');
            div.dialog({
                title: "Phân chia mã hàng theo cảng",
                resizeable: false,
                modal: true,
                width: '900px',
                height: 550,
                open: function(event, ui) {
                    $.ajax({
                        url: "inc-view_quotation.aspx",
                        type: "POST",
                        datetype: "json",
                        data: { datagrid: null, action: "TaoPhieuNhap" },
                        error: function (rs) {
                            Popup("Error", 500, 400, rs.responseText);
                        },
                        success: function (rs) {
                            div.html(rs);
                            $('#tao-quotation table tbody').append(`
                            <tr>
							    <td colspan=2>
								    <fieldset style="padding:10px">
                                        <legend>
									        <label style="font-weight:bold"> Nhập excel (xls) để tạo dòng hàng </label>
                                        </legend>
									    <form enctype="multipart/form-data" id="frm_InOutQRCode" action="PrintControllers/InThongTinSanPhamQRCode/ExportQRCode.aspx" target="exportEX_popup" method="post" onsubmit="window.open('about:blank','exportEX_popup','width=700,height=700,scrollbars=yes');">
										    <input type="file" id="fileExcel" name="fileExcel" accept=".xls,.xlsx">
									    </form>
								    </fieldset>
							    </td>
						    </tr>
                            `);
                        }
                    });
                },
                close: function (event, ui) {
                    $(this).dialog('destroy').remove(); 
                },
                buttons: [
				{
				    text: "Tạo QO",
				    id: "create_pos",
				    click: function () {
                        var frmID = '#createQO';
                        var data_js = new FormData();
                        var nbg = $("#ngaybaogia_v", frmID).val();
                        var nhh = $("#ngayhethan_v", frmID).val();
                        var check = chkCreateListQO(nbg, nhh);

                        var file = $('#frm_InOutQRCode input[type="file"]')[0].files;
                        if(file.length > 0) {
                            file = file[0];
                        }
                        else {
                            if(check == 0) {
                                alert('Chưa chọn tập tin.');
                                check = 1;
                            }
                        }

                        if(check == 0) {
                            data_js.append('khachhang_v', $('#khachhang_v select', frmID).val());
                            data_js.append('ngaybaogia_v', $('#ngaybaogia_v', frmID).val());
                            data_js.append('shipmenttime', $('#shipmenttime', frmID).val());
                            data_js.append('ngayhethan_v', $('#ngayhethan_v', frmID).val());
                            data_js.append('ptrongluong_v', $('#ptrongluong_v select', frmID).val());
                            data_js.append('pdongtien_v', $('#pdongtien_v select', frmID).val());
                            data_js.append('pkichthuoc_v', $('#pkichthuoc_v select', frmID).val());
                            data_js.append('gia_db', $('#gia_db', frmID).prop('checked'));
                            data_js.append('chkPBGCu', $('#chkPBGCu', frmID).prop('checked'));
                            data_js.append('phienbangiacu', $('#phienbangiacu', frmID).val());
                            data_js.append('cangbien', $('#cangbien', frmID).val());
                            data_js.append('file', file);
                            div.prepend('<div id="waitLoad"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>');
				            $("#create_pos").attr("disabled", true);
                            $.ajax({
                                url: 'Controllers/Quotation/CreateQuotationFromExcel.ashx',
                                type: 'POST',
                                data: data_js,
                                processData: false,
                                contentType: false,
                                success: function (result) {
                                    div.html(`<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all"><p><span id="dialog-caution">${result}</span></p></h3>`);
                                },
                                error: function (jqXHR, textStatus, errorThrown) {
                                    $('#waitLoad').remove();
                                    $("#create_pos").removeAttr("disabled");
                                    alert('error Server');
                                }
                            });
                        }
				    }
				},
				{
				    text: "Đóng",
				    click: function () {
				        $(this).dialog("destroy").remove();
				    }
				}
			    ]
            });
        }

        function showPictureQO(obj) {
            var id = $(obj).attr('id');
            $.get("Controllers/ProductController.ashx?action=getpicture&productId=" + id, function(result) {
                $.fancybox.open(jQuery.parseJSON(result), {
                    padding: 0
                });
            });
        }

        function imageFormatQO(cellvalue, options, rowObject) {
            return '<img id="' + cellvalue + '" onclick="showPictureQO(this)" style="cursor:pointer" src="iconcollection/pro_icon.png" />';
        }

        function imageUnFormatQO(cellvalue, options, cell) {
            return $('img', cell).attr('src');
        }
</script>

<div class="ui-layout-west ui-widget-content" id="layout-west-taoqo">
            <uc1:jqGrid  ID="grid_ds_sanpham_taoqo" 
            SortName="sp.ma_sanpham"
            SortOrder ="asc"
            UrlFileAction="Controllers/ProductCreateQOController.ashx" 
            ColNames="['md_sanpham_id', '', 'Mã SP', 'Mô Tả TV', 'Mô Tả TA', 'Giá FOB', 'Đóng Gói', 'CBM', 'No of Pack']"
            RowNumbers="true"
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
                fixed: true, name: 'picture'
                , index: 'picture'
                , width: 100
                , editable:false
                , formatter: imageFormatQO
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
                fixed: true, name: 'mota_tiengviet'
                , index: 'mota_tiengviet'
                , width: 100
                , edittype: 'textarea'
                , editable:false
            },
            {
                fixed: true, name: 'mota_tienganh'
                , index: 'mota_tienganh'
                , width: 100
                , edittype: 'textarea'
                , editable:false
            },
			{
                fixed: true, name: 'giafob'
                , index: 'giafob'
                , width: 50
                , edittype: 'textarea'
                , editable: false
                , align: 'right'
                , search: false
                , sortable: false
            },
			{
                fixed: true, name: 'ten_donggoi'
                , index: 'ten_donggoi'
                , width: 50
                , edittype: 'textarea'
                , editable: false
                , search: false
                , sortable: false
            },
			{
                fixed: true, name: 'v2'
                , index: 'v2'
                , width: 50
                , edittype: 'textarea'
                , editable: false
                , align: 'right'
                , search: false
                , sortable: false
            },
			{
                fixed: true, name: 'noofpack'
                , index: 'noofpack'
                , width: 50
                , edittype: 'textarea'
                , editable: false
                , align: 'right'
                , search: false
                , sortable: false
            }
			
            ]"
            Caption="Danh sách sản phẩm"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); 
                $(this).setGridHeight($(o).height() - 90); 
                $(this).setGridWidth($(o).width() - 2) "
            FilterToolbar = "true"
            ViewFormOptions = "
                        afterShowForm : function(formid){
                                formid.closest('div.ui-jqdialog').dialogCenter();    
                        }"
            Height = "420"
            MultiSelect="true"
            runat="server" />
</div>

<div class="ui-layout-east ui-widget-content" id="layout-east-taoqo">
    <script>
        $(document).ready(function () {
            var lastSel = -1;
            jQuery("#grid_ds_dachon_taoqo").jqGrid({
                datatype: "local",
                height: 250,
                colNames: ['md_sanpham_id', 'Mã SP', 'Mã SP Khách', 'Số Lượng', 'Giá FOB', 'Đóng Gói', 'CBM', 'No of Pack'],
                colModel: [
                                {
                                    fixed: true, name: 'md_sanpham_id'
                                    , index: 'md_sanpham_id'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                    , hidden: true
                                }
                                , {
                                    fixed: true, name: 'ma_sanpham'
                                    , index: 'ma_sanpham'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: false
                                }
                                , {
                                    fixed: true, name: 'ma_sp_khach'
                                    , index: 'ma_sp_khach'
                                    , width: 100
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                }
                                , {
                                    fixed: true, name: 'soluong'
                                    , index: 'soluong'
                                    , width: 100
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                    , editrules: { requied: true, number: true }
                                }
								, {
								    fixed: true, name: 'giafob'
                                    , index: 'giafob'
                                    , width: 50
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                    , editrules: { requied: true, number: true }
								}
								, {
								    fixed: true, name: 'ten_donggoi'
                                    , index: 'ten_donggoi'
                                    , width: 50
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                    , editrules: { requied: true, number: true }
								}
								, {
								    fixed: true, name: 'v2'
                                    , index: 'v2'
                                    , width: 50
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                    , editrules: { requied: true, number: true }
								}
								, {
								    fixed: true, name: 'noofpack'
                                    , index: 'noofpack'
                                    , width: 50
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                    , editrules: { requied: true, number: true }
								}

                                ],
                autowidth: true,
                multiselect: false,
                caption: "Danh sách sản phẩm đã chọn",
                height: 150,
                gridComplete: function () { var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 70); },
                showRefresh: false,
                rowNumbers: true,
                cellsubmit: 'clientArray',
                onSelectRow: function (id) {
                    // if (id && id !== lastSel) {
                    // jQuery("#grid_ds_dachon_taoqo").jqGrid('restoreRow', lastSel);
                    // jQuery("#grid_ds_dachon_taoqo").jqGrid('editRow', id, true, null, null, 'clientArray');
                    // lastSel = id;
                    // }
                },
                pager: $('#grid_ds_dachon_taoqo-pager')
            });
            $('#grid_ds_dachon_taoqo').jqGrid('navGrid', '#grid_ds_dachon_taoqo-pager', { add: false, edit: false, del: false, search: false, refresh: false }, { height: 280, reloadAfterSubmit: false }, { height: 280, reloadAfterSubmit: false }, { reloadAfterSubmit: false }, {});

            $("#grid_ds_dachon_taoqo").jqGrid('navButtonAdd', '#grid_ds_dachon_taoqo-pager', {
                caption: ""
				, title: "Tạo Quotation"
				, buttonicon: "icon-pi"
				, onClickButton: function () {
				    //var id = jQuery("#grid_ds_dachon_taoqo").jqGrid('getGridParam', 'selrow');
				    //jQuery("#grid_ds_dachon_taoqo").jqGrid('saveRow', id, true, 'clientArray', null, null);
				    // var rowids = $("#grid_ds_dachon_taoqo").jqGrid('getDataIDs');
				    // for(var i in rowids) {
				    // $("#grid_ds_dachon_taoqo").jqGrid('saveRow', rowids[i], true, 'clientArray', null, null);
				    // }
				    var gridData = jQuery(this).getRowData();
				    var postData = JSON.stringify(gridData);
				    funcCreateQuotation(postData);
				}
            });

            $("#grid_ds_dachon_taoqo").jqGrid('navButtonAdd', '#grid_ds_dachon_taoqo-pager', {
                caption: "Tạo QO từ Excel"
				, title: "Tạo Quotation từ Excel"
				, onClickButton: function () {
				    funcCreateQuotationExcel();
				}
            });
        });
        </script>
        <table class="ui-layout-center" id="grid_ds_dachon_taoqo"></table>
        <div id="grid_ds_dachon_taoqo-pager"></div>
</div>

<div class="ui-layout-center ui-widget-content" id="layout-center-taoqo">
    <button id="add-all-create-qo" class="btn-active">>></button>
    <button id="add-create-qo" class="btn-active">></button>
    <button id="back-all-create-qo" class="btn-active"><<</button>
    <button id="back-create-qo" class="btn-active"><</button>
</div>

