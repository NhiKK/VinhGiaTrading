<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-quanly-congsuat.aspx.cs" Inherits="inc_quanly_congsuat" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
        $('#lay-center-nhacungung_qlcs').parent().layout({
            south: {
                size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                var o = $("#lay-south-nhacungung_qlcs").height();
                $("#grid_ds_chitietnangluc_qlcs").setGridHeight(o - 90);

                }
            }
            , north: {
                size: "25%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                var o = $("#lay-north-nhacungung_qlcs").height();
                $("#grid_ds_nhacungung_qlcs").setGridHeight(o - 70);
                }
            }
            , center: {
                onresize_end: function() {
                var o = $("#lay-center-nhacungung_qlcs").height();
                $("#grid_ds_tuannangluc_qlcs").setGridHeight(o - 90);
                }
            }
        });

        $("#tungay_qlcs").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'mm/dd/yy' });
        $("#tungay_qlcs").datepicker('setDate', new Date());


        $("#denngay_qlcs").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'mm/dd/yy' });
        $("#denngay_qlcs").datepicker('setDate', new Date());
    });


    function funcNangLuc() {
        var msg = "";
        var id = $('#grid_ds_tuannangluc_qlcs').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Bạn hãy chọn 1 tuần mà bạn muốn tạo năng lực.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-tao-nangluc_qlcs\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-tao-nangluc_qlcs').dialog({
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
            msg = "Có phải bạn muốn tạo năng lực cho tuần này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-tao-nangluc_qlcs\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-tao-nangluc_qlcs').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/TuanNangLucController.ashx?action=createCapacity&weekId=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                            $('#dialog-tao-nangluc_qlcs').find('#dialog-caution').html(result);
                            $('#grid_ds_chitietnangluc_qlcs').jqGrid().trigger('reloadGrid');
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

    function createTuanNLTheoNam(div) {
        var nam = $("#nam").val();
        var dtkd_id = $('#grid_ds_nhacungung_qlcs').jqGrid('getGridParam', 'selrow');
        $.ajax({
            url: "Controllers/PartnerController.ashx?action=createWeekCapacity",
            type: "POST",
            datatype: "xml",
            data: { dtkd: dtkd_id, nam: nam },
            error: function(rs) { },
            success: function(rs) {
                //                var type = eval($(rs).find("type").text());
                //                var message = $(rs).find("message").text();
                //                if(type == 1){
                $('#grid_ds_tuannangluc').jqGrid().trigger('reloadGrid');
                $(div).dialog("close");
                //                }else{
                //                    $("#alert_msg").html(message);
                //                }
            }
        });
    }

    function createTuanNL() {
        var div = $("#crt_tuannl");
        var dtkd_id = $('#grid_ds_nhacungung_qlcs').jqGrid('getGridParam', 'selrow');
        div.dialog({
            title: "Tạo các tuần năng lực cho năm được chọn",
            resizeable: false,
            modal: true,
            width: '250px',
            height: 150,
            close: function(event, ui) { $(this).dialog('close'); },
            buttons: [
					   { text: "Tạo", click: function() { createTuanNLTheoNam(div); } },
					   { text: "Đóng", click: function() { $(this).dialog("close"); } }
				      ]
        });
    }


    function funcTaoTuanNangLuc() {
        var msg = "";
        var id = $('#grid_ds_nhacungung_qlcs').jqGrid('getGridParam', 'selrow');

        if (id == null) {
            msg = "Bạn hãy chọn 1 nhà cung ứng mà bạn muốn tạo tuần năng lực.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-tao-tuan-nangluc_qlcs\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-tao-tuan-nangluc_qlcs').dialog({
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
            msg = "Có phải bạn muốn tạo tuần năng lực cho nhà cung ứng này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-tao-tuan-nangluc_qlcs\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-tao-tuan-nangluc_qlcs').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/PartnerController.ashx?action=createWeekCapacity&partnerId=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                            $('#dialog-tao-tuan-nangluc_qlcs').find('#dialog-caution').html(result);
                            $('#grid_ds_tuannangluc_qlcs').jqGrid().trigger('reloadGrid');
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

    function viewNangLucConLai() {

        var div = $("#filter_nl_qlcs");
        var dtkd_id = $('#grid_ds_nhacungung_qlcs').jqGrid('getGridParam', 'selrow');
        div.dialog({
            title: "Xem năng lực còn lại theo ngày",
            resizeable: false,
            modal: true,
            width: '300px',
            height: 250,
            close: function(event, ui) { $(this).dialog('close'); },
            buttons: [
					   { text: "Xem", click: function() { window.open("ReportWizard/Rpt_NCC/rpt_xemnanglucconlaiNCU.aspx?startdate=" + $("#tungay").val() + "&enddate=" + $("#denngay").val() + "&doitackinhdoanh_id=" + dtkd_id); $(this).dialog("close"); } },
					   { text: "Đóng", click: function() { $(this).dialog("close"); } }
				      ]
				});
				$("#tungay_qlcs").datepicker("hide");
    }
    
</script>
<div id="filter_nl_qlcs" style="display:none;">
    <table>
        <tr>
            <td>Từ ngày</td>
            <td><input type="text" name="tungay", id="tungay_qlcs"/></td>
        </tr>
        <tr>
            <td>Đến ngày</td>
            <td><input type="text" name="denngay", id="denngay_qlcs"/></td>
        </tr>
    </table>

</div>

<div id="crt_tuannl" style="display:none;">
    <table>
        <tr><td colspan="2"> <div id="alert_msg"></div> </td></tr>
        <tr>
            <td>Chọn năm</td>
            <td>
                <select id ="nam" name="nam">
                    <%
                        int year = DateTime.Now.Year;
                        for (int i = year; i <= year + 10; i ++ )
                        {%>
                    <option value="<%=i %>"><%=i %></option> 
                        <%} %>
                </select>
            </td>
        </tr>
    </table>

</div>

<DIV class="ui-layout-north ui-widget-content" id="lay-north-nhacungung_qlcs">
    <uc1:jqGrid  ID="grid_ds_nhacungung_qlcs" 
            Caption="NCU"
            SortName="ma_dtkd"
            SortOrder = "ASC"
            UrlFileAction="Controllers/PartnerByUserController.ashx"
            FuncTaoTuanNangLuc = "createTuanNL"
            FuncXemNangLuc="viewNangLucConLai"
            ColNames="['dtkd.md_doitackinhdoanh_id', 'Loại Đối Tác', 'Mã Đối Tác','Tên Đối Tác','Đại Diện','Chức Vụ','Điện Thoại','Fax','Email','Website','Địa Chỉ', 'Quốc Gia', 'Khu Vực', 'Bảng giá mặc định', 'Số Tài Khoản','Ngân Hàng','Mã Số Thuế','Tổng Công Nợ','Là Nhà Cung Cấp', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 70);"
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
                 fixed: true, name: 'tenloai'
                 , index: 'tenloai'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CategoryPartnerController.ashx?action=getoption' }
                 , hidden: true
                 , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'ma_dtkd'
                , index: 'dtkd.ma_dtkd'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'ten_dtkd'
                , index: 'dtkd.ten_dtkd'
                , width: 350
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'daidien'
                , index: 'dtkd.daidien'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'chucvu'
                , index: 'dtkd.chucvu'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'tel'
                , index: 'dtkd.tel'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'fax'
                , index: 'dtkd.fax'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'email'
                , index: 'dtkd.email'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'url'
                , index: 'dtkd.url'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            
            {
                fixed: true, name: 'diachi'
                , index: 'dtkd.diachi'
                , width: 200
                , edittype: 'text'
                , editable:true
            },
            {
                 fixed: true, name: 'tenqg'
                 , index: 'tenqg'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CountryController.ashx?action=getoption' }
                 , hidden: true
                 , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'tenkv'
                , index: 'tenkv'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/SaleRegionController.ashx?action=getoption' }
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'banggia'
                , index: 'banggia'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/PriceListController.ashx?action=getoption' }
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'so_taikhoan'
                , index: 'dtkd.so_taikhoan'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'nganhang'
                , index: 'dtkd.nganhang'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'masothue'
                , index: 'dtkd.masothue'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'tong_congno'
                , index: 'dtkd.tong_congno'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editoptions:{ defaultValue: '0' }
            },
            {
                fixed: true, name: 'isncc'
                , index: 'dtkd.isncc'
                , width: 100
                , edittype: 'checkbox'
                , editable:true
                , formatter: 'checkbox'
                , hidden : true
                , align: 'center'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'dtkd.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'dtkd.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'dtkd.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'dtkd.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'dtkd.mota'
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
            OnSelectRow = "
                function(ids) {
		            if(ids == null) {
			            ids=0;
				    }
                    
                    if($('#grid_ds_tuannangluc_qlcs').jqGrid('getGridParam','records') &gt; 0 )
			        {
			            let caption = $(this).jqGrid('getCell', ids, 'ma_dtkd');
			            $('#grid_ds_tuannangluc_qlcs').jqGrid('setCaption', 'Tuần năng lực của nhà cung ứng: ' + caption); 
				    }

                    $('#grid_ds_tuannangluc_qlcs').jqGrid('getGridParam', 'postData').partnerId = ids;
				    $('#grid_ds_tuannangluc_qlcs')[0].triggerToolbar();
                }
            "
            Height = "420"
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            runat="server" />
</DIV>

<DIV class="ui-layout-center ui-widget-content" id="lay-center-nhacungung_qlcs">
    <uc1:jqGrid  ID="grid_ds_tuannangluc_qlcs" 
            Caption="Tuần năng lực"
            SortName="tuanthu" 
            SortOrder = "ASC"
            FuncNangLuc = "funcNangLuc"
            UrlFileAction="Controllers/TuanNangLucController.ashx"
            ColNames="['c_tuannangluc_id','NCU', 'Tên Tuần', 'Tuần Thứ', 'Ngày Bắt Đầu', 'Năm', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            RowNumbers="true"
            FilterToolbar="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            
            }); }"
            ColModel = "[
            {
                fixed: true, name: 'c_tuannangluc_id'
                , index: 'c_tuannangluc_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_doitackinhdoanh_id'
                 , index: 'dtkd.md_doitackinhdoanh_id'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
                 , hidden:true
            },
            {
                 fixed: true, name: 'ten_tuan'
                 , index: 'ten_tuan'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'tuanthu'
                 , index: 'tuanthu'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required: true, number:true, minValue: 1, maxValue: 99 }
                 , editoptions: { defaultValue: '1' }
            },
            {
                 fixed: true, name: 'ngaybatdau'
                 , index: 'ngaybatdau'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                 fixed: true, name: 'nam'
                 , index: 'nam'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required:true, number:true, minValue:0, maxValue:9999 }
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
            OnSelectRow = "
                function(ids) {
		            if(ids == null) {
			            ids=0;
				    }
                    
                    $('#grid_ds_chitietnangluc_qlcs').jqGrid('getGridParam', 'postData').weekId = ids;
				    $('#grid_ds_chitietnangluc_qlcs')[0].triggerToolbar();
                }
            "
            Height = "420"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                    var masterId = $('#grid_ds_nhacungung_qlcs').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_doitackinhdoanh_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        alert('Hãy chọn một nhà cung ứng mới có thể tạo chi tiết.!');
                    }else{
                        $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            EditFormOptions ="
                beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter();
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
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
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            ShowAdd ="true"
            ShowEdit ="true"
            ShowDel = "true"
            runat="server" />
</DIV>


<DIV class="ui-layout-south ui-widget-content" id="lay-south-nhacungung_qlcs" style="overflow:hidden">
    <uc1:jqGrid  ID="grid_ds_chitietnangluc_qlcs" 
            Caption="Chi Tiết Năng Lực"
            SortName="ctnl.tenhehang" 
            SortOrder = "ASC"
            UrlFileAction="Controllers/ChiTietNangLucController.ashx"
            ColNames="['c_chitietnangluc_id', 'Nhóm NL', 'Tuần Năng Lực'
                    , 'NCU', 'Tên Hệ Hàng', 'Đầu Mã'
                    , 'Năng Suất'
                    , 'Số Lượng Đã Đặt', 'Số Lượng Còn Lại'
                    , 'Áp dụng cho các tuần sau'
                    ,  'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi Chú', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            RowNumbers="true"
            FilterToolbar="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            }); }"
            ColModel = "[
            {
                fixed: true, name: 'c_chitietnangluc_id'
                , index: 'c_chitietnangluc_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_nhomnangluc_id'
                 , index: 'nnl.md_nhomnangluc_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/NhomNangLucController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'c_tuannangluc_id'
                 , index: 'tnl.c_tuannangluc_id'
                 , width: 100
                 , edittype: 'text'
                 , editable: true
                 , hidden:true
            },
            {
                 fixed: true, name: 'md_doitackinhdoanh_id'
                 , index: 'dtkd.md_doitackinhdoanh_id'
                 , width: 100
                 , edittype: 'text'
                 , editable: true
                 , hidden:true
            },
            {
                 fixed: true, name: 'tenhehang'
                 , index: 'ctnl.tenhehang'
                 , width: 100
                 , editable: false
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'dauma'
                 , index: 'dauma'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'nangxuat_tb'
                 , index: 'ctnl.nangxuat_tb'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required:true, number:true, minValue:0}
                 , editoptions:{ defaultValue: '0' }
            },
            {
                 fixed: true, name: 'sl_dadat'
                 , index: 'ctnl.sl_dadat'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required:true, number:true, minValue:0 }
                 , editoptions:{ defaultValue: '0' }
            },
            {
                 fixed: true, name: 'sl_conlai'
                 , index: 'ctnl.sl_conlai'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },  
            {
                fixed: true, name: 'saveto'
                , index: 'saveto'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
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
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                     beforeShowForm: function (formid) {
                            var masterId = $('#grid_ds_nhacungung_qlcs').jqGrid('getGridParam', 'selrow');
                            var masterId_week = $('#grid_ds_tuannangluc_qlcs').jqGrid('getGridParam', 'selrow');
                            
                            var forId = 'md_doitackinhdoanh_id';
                            var forId_week = 'c_tuannangluc_id';
                            if(masterId == null)
                            {
                                $(formid).parent().parent().parent().dialog('destroy').remove();
                                alert('Hãy chọn một nhà cung ứng mới có thể tạo chi tiết.!');
                                
                            }else if(masterId_week == null){
                                $(formid).parent().parent().parent().dialog('destroy').remove();
                                alert('Hãy chọn một tuần năng lực mới có thể tạo chi tiết.!');
                            }else{
                                $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                                $('#tr_' + forId_week + ' td.DataTD input#' + forId_week, formid).val(masterId_week);
                                formid.closest('div.ui-jqdialog').dialogCenter();
                            }
                    }
                    , beforeSubmit:function(postdata, formid){
                        var masterId = $('#grid_ds_nhacungung_qlcs').jqGrid('getGridParam', 'selrow');
                        var masterId_week = $('#grid_ds_tuannangluc_qlcs').jqGrid('getGridParam', 'selrow');
                        
                        var forId = 'md_doitackinhdoanh_id';
                        var forId_week = 'c_tuannangluc_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một nhà cung ứng mới có thể tạo chi tiết.!');
                            
                        }else if(masterId_week == null){
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một tuần năng lực mới có thể tạo chi tiết.!');
                        }else{
                            postdata.md_doitackinhdoanh_id = masterId;
                            postdata.c_tuannangluc_id = masterId_week;
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
                },
                afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
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
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            runat="server" />
</DIV>

<script type="text/javascript">
    createRightPanel('grid_ds_nhacungung_qlcs');
    createRightPanel('grid_ds_tuannangluc_qlcs');
    createRightPanel('grid_ds_chitietnangluc_qlcs');
</script>