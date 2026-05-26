<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phieuhanngach.aspx.cs" Inherits="inc_phieuhanngach" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function () {
        $('#lay-center-phieuhanngach').parent().layout({
            north: {
                size: "50%"
                , onresize_end: function () {
                    var o = $("#lay-north-phieuhanngach").height();
                    $("#grid_ds_phieuhanngach").setGridHeight(o - 90);

                }
            }
            , center: {
                onresize_end: function () {
                    var o = $("#lay-center-phieuhanngach").height();
                    $("#grid_ds_chitietphieuhanngach").setGridHeight(o - 90);
                }
            }
        });
    });

    function funcActiveHanNgach()
    {
        var id = jQuery("#grid_ds_phieuhanngach").jqGrid('getGridParam', 'selrow');
        if (id != null) {
            var sochungtu = $('#grid_ds_phieuhanngach').jqGrid('getCell', id, 'sochungtu');
            msg = "Có phải bạn muốn hiệu lực phiếu hạn ngạch '" + sochungtu + "'.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-active-hanngach\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-active-hanngach').dialog({
                modal: true
                , open: function (event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function () {
                            $("#dialog-caution").ajaxStart(function () {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function () {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/PhieuHanNgachController.ashx?action=ActiveHanNgach&id=" + id, function (result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-active-hanngach').find('#dialog-caution').html(result);
                                $('#grid_ds_phieuhanngach').jqGrid().trigger('reloadGrid');
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
        else {
            alert("Chưa chọn phiếu hạn ngạch cần hiệu lực!");
        }
    }
	
	function sendMailHanNgach() {
        var c_phieuhanngach_id = $('#grid_ds_phieuhanngach').jqGrid('getGridParam', 'selrow');
        if (c_phieuhanngach_id == null) {
            alert('Hãy chọn một phiếu hạn ngạch mà bạn muốn gửi email!');
        } else {

        $('body').append("<div title='Gửi Email' id='sendhanngach'>" + "<div id='content'>Đang kiểm tra...</div>" + "</div>");
        $('#sendhanngach').dialog({
                modal: true
			 , width:500 
             , open: function(event, ui) {
                 //hide close button.
                 $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
             }
             , buttons: [
                {
                    id: "btn-send",
                    text: "Gửi đi",
                    click: function () {
                        var sendType = $('input[name=rdTypeMail]:checked', '#sendhanngach').val();
                        if (typeof sendType != 'undefined') {

                            $('#sendhanngach #content').html("Đang gửi email...");
                            $("#btn-send").button("disable");
                            $("#btn-close").button("disable");

                            if (sendType == "kcs") {
                                $.get("Controllers/PhieuHanNgachController.ashx?action=sendmail&c_phieuhanngach_id=" + c_phieuhanngach_id + "&sendMailType=toPO", function (result) {
                                    $('#sendhanngach #content').html(result);
                                    $("#btn-close span").text("Thoát");
                                    $("#btn-close").button("enable");
                                });
                            }
                        } else {
                            alert("Hãy chọn người nhận!");
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

            $.get("Controllers/PhieuHanNgachController.ashx?action=startsendmail&c_phieuhanngach_id=" + c_phieuhanngach_id, function(result) {
                $('#sendhanngach').find('#content').html('<h2>Xác nhận trước khi gửi email!</h2><div>' + result + '</div>');
            });
        }
    }
</script>
<DIV class="ui-layout-north ui-widget-content" id="lay-north-phieuhanngach">    
     <uc1:jqGrid  ID="grid_ds_phieuhanngach" 
            Caption="Phiếu hạn ngạch"
            FilterToolbar = "true"
            SortName="phn.ngaytao"
            SortOrder="desc"
            FuncActive="funcActiveHanNgach"
			FuncSendMail="sendMailHanNgach"
            UrlFileAction="Controllers/PhieuHanNgachController.ashx"
            ColNames="['c_phieuhanngach_id', 'TT', 'Số chứng từ', 'Phiếu tăng', 'Ngày Lập','Ngày Trình','Ngày Duyệt', 'Lí Do', 'Số DSDH', 'Số PO', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            } ); }"
            ColModel = "[
            {
                fixed: true, name: 'c_phieuhanngach_id'
                , index: 'phn.c_phieuhanngach_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 name: 'md_trangthai_id'
                 , index: 'phn.md_trangthai_id'
                 , editable: false
                 , fixed:true , width: 100
                 , edittype: 'text'
                 , align:'center'
                 , formatter:'imagestatus'
                 , search : true
                 , stype : 'select'
                 , searchoptions:{sopt:['eq'], value:':[ALL];SOANTHAO:SOẠN THẢO;HIEULUC:HIỆU LỰC;KETTHUC:KẾT THÚC' }
            },
            {
                fixed: true, name: 'sochungtu'
                , index: 'phn.sochungtu'
                , width: 100
                , editable: true
                , edittype: 'text'
                , editoptions: { readonly:'readonly' }
            },
            {
                fixed: true
                , name: 'phieutang'
                , hidden: true
                , index: 'phn.phieutang'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ readonly:'readonly', value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
                , hidden: true
            },
            {
                 name: 'ngaylap'
                 , index: 'phn.ngaylap'
                 , editable: true
                 , fixed:true , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); if (currentTime) {  currentTime.setDate(currentTime.getDate() + 7) }; var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                 name: 'ngaytrinh'
                 , index: 'phn.ngaytrinh'
                 , editable: true
                 , fixed:true , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); if (currentTime) {  currentTime.setDate(currentTime.getDate() + 7) }; var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
            },
            {
                 name: 'ngayduyet'
                 , index: 'phn.ngayduyet'
                 , editable: true
                 , fixed:true , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); if (currentTime) {  currentTime.setDate(currentTime.getDate() + 7) }; var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
            },
             {
                fixed: true, name: 'lido'
                , index: 'phn.lido'
                , width: 100
                , editable: true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'sodathang'
                , index: 'phn.sodathang'
                , width: 100
                , editable: false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'so_po'
                , index: 'dh.sochungtu'
                , width: 100
                , editable: false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'phn.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'phn.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'phn.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'phn.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'phn.mota'
                , width: 100
                , editable:false
                , edittype: 'textarea'
                , hidden: true
            },
            {
                fixed: true
                , name: 'hoatdong'
                , hidden: true
                , index: 'phn.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            AddFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                afterSubmit: function(rs, postdata) { 
                    return showMsgDialog(rs); 
                }
            "
            EditFormOptions ="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
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
                beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter(); 
                    },
                    afterSubmit: function(rs, postdata) { 
                        return showMsgDialog(rs); 
                }
            "
            OnSelectRow = "
                function(ids) {
		            if(ids == null) {
			            ids=0; 
				    }
                    $('#grid_ds_chitietphieuhanngach').jqGrid('getGridParam', 'postData').c_phieuhanngach_id = ids;
				    $('#grid_ds_chitietphieuhanngach')[0].triggerToolbar();
                }
            "
		                
            Height = "420"
            ShowEdit = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            runat="server" />
    <script>
        $(function () {
            jQuery("#grid_ds_phieuhanngach").jqGrid('navGrid', "#grid_ds_phieuhanngach-pager").jqGrid('navButtonAdd', "#grid_ds_phieuhanngach-pager", { caption: "", buttonicon: "ui-icon ui-icon-arrowthick-1-s", onClickButton: function () { add_tab('Tạo phiếu hạn ngạch', 'inc-taohanngach.aspx') }, position: "last", title: "Giảm hạn ngạch", cursor: "pointer" });
        });
    </script>
</DIV>
<DIV class="ui-layout-center ui-widget-content" id="lay-center-phieuhanngach">
    <uc1:jqGrid  ID="grid_ds_chitietphieuhanngach"
            FilterToolbar = "true"
            Caption="Chi tiết hạn ngạch" 
            SortName="sothutu" 
            SortOrder="asc"
            UrlFileAction="Controllers/ChiTietPhieuHanNgachController.ashx" 
            ColNames="['c_chitietphieuhanngach_id', 'Số Thứ Tự', 'Mã SP', 'Mã SP Khách', 'SL Cần Làm', 'Mã SP Thay Đổi', 'SL Thay Đổi', 'Giá Nhập', 'Nhà Cung Cấp', 'Ghi Chú', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            } ); }"
            ColModel = "[
            {
                fixed: true, name: 'c_chitietphieuhanngach_id'
                , index: 'c_chitietphieuhanngach_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'sothutu'
                , index: 'sothutu'
                , width: 100
                , edittype: 'text'
                , editable: true
            },
            {
                fixed: true, name: 'ma_sanpham'
                , index: 'ma_sanpham'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editoptions:{ readonly:'readonly' }
            },
            {
                fixed: true, name: 'ma_sanpham_khach'
                , index: 'ma_sanpham_khach'
                , width: 100
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'sl_canlam'
                , index: 'sl_canlam'
                , width: 100
                , editable:true
                , edittype: 'text'
                , align:'right'
                , hidden: true
            },
            {
                fixed: true, name: 'ma_sanpham_thaydoi'
                , index: 'ma_sanpham_thaydoi'
                , width: 100
                , editable:true
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'sl_thaydoi'
                , index: 'sl_thaydoi'
                , width: 100
                , editable:true
                , edittype: 'text'
                , align:'right'
            },
            {
                fixed: true, name: 'gianhap'
                , index: 'gianhap'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editoptions:{ readonly:'readonly' }
            },
            {
                fixed: true, name: 'ma_dtkd'
                , index: 'ma_dtkd'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editoptions:{ readonly:'readonly' }
            },
            {
                fixed: true, name: 'ghichu'
                , index: 'ghichu'
                , width: 100
                , editable:true
                , edittype: 'text'
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            Height = "420"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,beforeShowForm: function (formid) {
                    var masterId = $('#grid_ds_khachhangdocquyen').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_doitackinhdoanh_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        alert('Hãy chọn một khách hàng mới có thể tạo chi tiết.!');
                    }else{
                        $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_khachhangdocquyen').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_doitackinhdoanh_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một khách hàng mới có thể tạo chi tiết.!'];
                    }else{
                        postdata.md_doitackinhdoanh_id = masterId;
                        return [true,'']; 
                    }
                }
                , afterSubmit: function(rs, postdata){
                   return showMsgDialog(rs);
                }
                "
            EditFormOptions ="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
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
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }   
            "
            runat="server" />
</DIV>
<script>
    createRightPanel('grid_ds_phieuhanngach');
    createRightPanel('grid_ds_chitietphieuhanngach');
</script>
