<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-giasanpham.aspx.cs" Inherits="inc_giasanpham" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
        $('#lay-center-banggia').parent().layout({
            south: {
                size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                    var o = $("#lay-south-banggia").height();
                    $("#grid_ds_giasanpham").setGridHeight(o - 90);
                }
            }
            , north: {
                size: "25%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                    var o = $("#lay-north-banggia").height();
                    $("#grid_ds_banggia").setGridHeight(o - 90);
                }
            }
            , center: {
                onresize_end: function() {
                    var o = $("#lay-center-banggia").height();
                    $("#grid_ds_phienbanggia").setGridHeight(o - 90);
                }
            }
        });
    });

    function copyPhienBanGia() {
        var whereid = $('#grid_ds_phienbanggia').jqGrid('getGridParam', 'selrow');
        if (whereid == null) {
            alert('Hãy chọn một phiên bản giá mà bạn muốn copy!');
        } else {
            $('body').append('<div title="Copy vào phiên bản giá" id="copy-version-price"><span id="content-dialog">Copy từ:<select id="price-list"></select></span></div>');
            $('#copy-version-price').dialog({
                modal: true
            , create: function(event, ui) {
                $.get('Controllers/PriceVersionController.ashx?action=getPriceList&filter=' + whereid, function(result) {
                    $('#price-list').append(result);
                });
            }
            , buttons:
            [
                {
                    id: "btn-copy",
                    text: "Thực hiện",
                    click: function() {
                    var id = $('#price-list').val();
                        $('#copy-version-price').ajaxStart(function() {
                            $("#btn-copy").button("disable");
                        });
                        
                        $.ajax({
                            url: "Controllers/PriceVersionController.ashx?action=copy",
                            type: "POST",
                            data: { price_version: id, id: whereid },
                            error: function(rs) {
                                Popup("Error", 500, 450, rs.responseText);
                            },
                            success: function(rs) {
                                var msg = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + rs + '</div>';
                                $('#copy-version-price #content-dialog').html(msg);
                            }
                        });
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
        }
    }

    function funcActiveVersion() {
        var msg = "";
        var id = $('#grid_ds_phienbanggia').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Bạn hãy chọn một phiên bản giá mà bạn muốn hiệu lực.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-version\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-active-version').dialog({
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
            msg = "Có phải bạn muốn hiệu lực phiên bản giá này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-version\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-version').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active-version",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-active-version").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/ProductPriceController.ashx?action=activeversion&vId=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-active-version').find('#dialog-caution').html(result);
                                $('#grid_ds_phienbanggia').jqGrid().trigger('reloadGrid');
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

    function printPriceList() {
        var id = $('#grid_ds_phienbanggia').jqGrid('getGridParam', 'selrow');
        if (id == null) {
            alert("Chưa chọn phiên bản giá!");
        } else {
            var windowSize = "width=700,height=700,scrollbars=yes";
            window.open("PrintControllers/InGiaSanPham/?md_phienbangia_id=" + id, "Chiết xuất giá sản phẩm", windowSize);
        }
    }
    
</script>
<DIV class="ui-layout-north ui-widget-content" id="lay-north-banggia">
    <uc1:jqGrid  ID="grid_ds_banggia" 
            Caption="Bảng giá"
            FilterToolbar = "true"
            SortName="bg.ten_banggia" 
            UrlFileAction="Controllers/PriceListController.ashx" 
            ColNames="['md_banggia_id', 'Tên Bảng Giá', 'Đồng Tiền', 'Bảng Giá Bán', 'Standar',  'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{  
                
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }                
                
            }); }"
            ColModel = "[
            {
                fixed: true, name: 'md_banggia_id'
                , index: 'bg.md_banggia_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ten_banggia'
                , index: 'bg.ten_banggia'
                , width: 250
                , edittype: 'text'
                , editable:true
                , editrules: { required:true }
            },
            {
                 fixed: true, name: 'tendt'
                 , index: 'dt.ma_iso'
                 , editable: true
                 , width: 80
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CurrencyController.ashx?action=getoption' }
                 , editrules: { required:true }
                 , align: 'center'
            },
            {
                fixed: true, name: 'banggiaban'
                , index: 'bg.banggiaban'
                , width: 100
                , edittype: 'checkbox'
                , editable:true
                , align: 'center'
                , formatter: 'checkbox'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                
            },
            {
                fixed: true, name: 'isstandar'
                , index: 'bg.isstandar'
                , width: 100
                , edittype: 'checkbox'
                , editable:true
                , align: 'center'
                , formatter: 'checkbox'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'bg.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , viewable: true
                , hidden:true
                , editrules:{edithidden:true}
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'bg.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'bg.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , viewable: true
                , hidden:true
                , editrules:{edithidden:true}
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'bg.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'mota'
                , index: 'bg.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong'
                , hidden: true
                , index: 'bg.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            Height = "100"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_phienbanggia').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_phienbanggia').jqGrid('setGridParam',{url:'Controllers/PriceVersionController.ashx?&cateId='+ids,page:1});
				                $('#grid_ds_phienbanggia').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_phienbanggia').jqGrid('setGridParam',{url:'Controllers/PriceVersionController.ashx?&cateId='+ids,page:1});
			                $('#grid_ds_phienbanggia').jqGrid().trigger('reloadGrid');			
		                } }"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                "
            EditFormOptions ="
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                "
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            DelFormOptions="
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                "
            runat="server" />
</DIV>


<DIV class="ui-layout-south ui-widget-content" id="lay-south-banggia" style="overflow:hidden">
    <uc1:jqGrid  ID="grid_ds_giasanpham" 
            Caption="Giá sản phẩm"
            FilterToolbar = "true"
            SortName="gsp.md_giasanpham_id" 
            UrlFileAction="Controllers/ProductPriceController.ashx" 
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{ 
            
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                
            }); }"
            ColModel = "[
            {
                fixed: true
                , name: 'md_giasanpham_id'
                , index: 'gsp.md_giasanpham_id'
                , label: 'md_giasanpham_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true
                 , name: 'md_phienbangia_id'
                 , index: 'pbg.md_phienbangia_id'
                 , label: 'Tên Phiên Bản'
                 , width: 100
                 , edittype: 'text'
                 , editable: true
                 , hidden: true
            },
            {
                fixed: true
                , name: 'sanpham_id'
                , index: 'sp.md_sanpham_id'
                , label: 'Tên Sản Phẩm'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , editrules: { required:true }
            },
            {
                fixed: true
                , name: 'masp'
                , index: 'sp.ma_sanpham'
                , label: 'Tên Sản Phẩm'
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
                , editrules: { required:true }
            },
            {
                fixed: true
                , name: 'gia'
                , index: 'gsp.gia'
                , label: 'Giá'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules: { required:true, number:true, minValue:0 }
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true
                , name: 'phi'
                , index: 'gsp.phi'
                , label: 'Phí'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules: { required:true, number:true, minValue:0 }
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true
                , name: 'tongGia'
                , index: 'gsp.gia + gsp.phi'
                , label: 'Tổng Giá'
                , width: 100
                , edittype: 'text'
                , editable: false
                , editrules: { required:true, number:true, minValue:0 }
                , align:'right'
                , formatter:'currency'
                , formatoptions:{decimalSeparator:'.', thousandsSeparator: ',', decimalPlaces: 2, prefix: ''}
            },
            {
                fixed: true
                , name: 'ma_donggoi'
                , index: 'gsp.ma_donggoi'
                , label: 'Mã đóng gói'
                , width: 100
                , editable:true
                , edittype: 'text'
                , align:'center'
            },
            {
                fixed: true
                , name: 'mota'
                , index: 'gsp.mota'
                , label: 'Mã Số Khách'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true
                , name: 'barcode'
                , index: 'gsp.barcode'
                , label: 'Mã Barcode'
                , hidden: true
                , editrules:{ edithidden: true }
                , width: 100
                , editable:true
            },
            {
                fixed: true
                , name: 'ngaytao'
                , label: 'Ngày Tạo'
                , index: 'gsp.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true
                , name: 'nguoitao'
                , label: 'Người Tạo'
                , index: 'gsp.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true
                , name: 'ngaycapnhat'
                , index: 'gsp.ngaycapnhat'
                , label: 'Ngày Cập Nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true
                , name: 'nguoicapnhat'
                , index: 'gsp.nguoicapnhat'
                , label: 'Người Cập Nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true
                , name: 'hoatdong'
                , label: 'Hoạt động'
                , hidden: true
                , index: 'gsp.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            Height = "100"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                ,beforeShowForm: function (formid) {
                        var masterId = $('#grid_ds_phienbanggia').jqGrid('getGridParam', 'selrow');
                        var forId = 'md_phienbangia_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một phiên bản giá mới có thể tạo chi tiết.!');
                        }else{
                            $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        }
                    }
                , beforeSubmit:function(postdata, formid){
                        var masterId = $('#grid_ds_phienbanggia').jqGrid('getGridParam', 'selrow');
                        var forId = 'md_phienbangia_id';
                        
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            return [false,'Hãy chọn một phiên bản giá mới có thể tạo chi tiết.!'];
                        }else{
                            postdata.md_phienbangia_id = masterId;
                            return [true,'']; 
                        }
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                "
            EditFormOptions ="
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                "
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            DelFormOptions="
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                "
            runat="server" />
</DIV>
    

<DIV class="ui-layout-center ui-widget-content" id="lay-center-banggia">
    <uc1:jqGrid  ID="grid_ds_phienbanggia" 
        Caption="Phiên bản giá"
        FilterToolbar="true"
        SortName="pbg.ten_phienbangia" 
        UrlFileAction="Controllers/PriceVersionController.ashx" 
        ColNames="['md_phienbangia_id', 'TT', 'Tên Phiên Bản', 'Tên Bảng Giá', 'Ngày Hiệu Lực', 'Ngày hết hạn', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
        RowNumbers="true"
        BtnPrint="printPriceList"
        FuncCopyPhienBan="copyPhienBanGia"
        FuncActive ="funcActiveVersion"
        OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {    
        
            errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
        
        }); }"
        ColModel = "[
        {
            fixed: true, name: 'md_phienbangia_id'
            , index: 'pbg.md_phienbangia_id'
            , width: 100
            , hidden: true 
            , editable:true
            , edittype: 'text'
            , key: true
        },
        {
             fixed: true, name: 'md_trangthai_id'
             , index: 'pbg.md_trangthai_id'
             , editable: false
             , width: 50
             , edittype: 'text'
             , sortable: false
             , align: 'center'
             , formatter: 'imagestatus'
        },
        {
             fixed: true, name: 'ten_phienbangia'
             , index: 'pbg.ten_phienbangia'
             , editable: true
             , width: 250
             , edittype: 'text'
             , editrules: { required:true }
        },
        {
            fixed: true, name: 'md_banggia_id'
            , index: 'bg.md_banggia_id'
            , width: 100
            , edittype: 'text'
            , editrules: { required:true }
            , editable: true
            , hidden: true
        },
        {
            fixed: true, name: 'ngay_hieuluc'
            , index: 'pbg.ngay_hieuluc'
            , width: 100
            , editable:true
            , edittype: 'text'
            , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            , editrules: { required:true}
        },
        {
            fixed: true, name: 'ngay_hethan'
            , index: 'pbg.ngay_hethan'
            , width: 100
            , editable:true
            , edittype: 'text'
            , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            , editrules: { required:false}
        },
        {
            fixed: true, name: 'ngaytao'
            , index: 'pbg.ngaytao'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'nguoitao'
            , index: 'pbg.nguoitao'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'ngaycapnhat'
            , index: 'pbg.ngaycapnhat'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'nguoicapnhat'
            , index: 'pbg.nguoicapnhat'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'mota'
            , index: 'pbg.mota'
            , width: 100
            , editable:true
            , edittype: 'textarea'
        },
        {
                fixed: true, name: 'hoatdong', hidden: false
                , index: 'pbg.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
        ]"
        Height = "100"
        GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
        OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_giasanpham').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_giasanpham').jqGrid('setGridParam',{url:'Controllers/ProductPriceController.ashx?&cateId='+ids,page:1});
				                $('#grid_ds_giasanpham').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_giasanpham').jqGrid('setGridParam',{url:'Controllers/ProductPriceController.ashx?&cateId='+ids,page:1});
			                $('#grid_ds_giasanpham').jqGrid().trigger('reloadGrid');			
		                } }"
        ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
        ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
        ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
        AddFormOptions=" 
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                , beforeShowForm: function (formid) {
                        var masterId = $('#grid_ds_banggia').jqGrid('getGridParam', 'selrow');
                        var forId = 'md_banggia_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một bảng giá mới có thể tạo chi tiết.!');
                        }else{
                            $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        }
                    }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_banggia').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_banggia_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một quotation mới có thể tạo chi tiết.!'];
                    }else{
                        postdata.md_bangia_id = masterId;
                        return [true,'']; 
                    }
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
            "
            EditFormOptions ="
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                "
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            DelFormOptions="
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,afterSubmit: function(rs, postdata) {
                    return showMsgDialog(rs); 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                "
        runat="server" />
</DIV>

<script type="text/javascript">
    createRightPanel('grid_ds_banggia');
    createRightPanel('grid_ds_phienbanggia');
    createRightPanel('grid_ds_giasanpham');
</script>
