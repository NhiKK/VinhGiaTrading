<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-namtaichinh.aspx.cs" Inherits="inc_namtaichinh" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
        $('#lay-center-namtaichinh').parent().layout({
            north: {
                size: "30%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                    var o = $("#layout-north-namtaichinh").height();
                    $("#grid_ds_namtaichinh").setGridHeight(o + 4 - 77);
                }
            },
            center: {
                onresize_end: function() {
                    var o = $("#lay-center-namtaichinh").height();
                    $("#grid_ds_kytrongnam").setGridHeight(o + 4 - 77);
                }
            }
        })
    });

    function funcTaoKy() {
        var id = $('#grid_ds_namtaichinh').jqGrid('getGridParam', 'selrow');
        var msg = "";
        
        
        // remove dialog-taoky old
        $('#dialog-taoky').remove();

        if (id == null) {
            msg = "Bạn hãy chọn 1 năm tài chính mà bạn muốn tạo kỳ.!";
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-taoky\"><h3>' + msg + '</h3></div>');

            // create new dialog
            $('#dialog-taoky').dialog({
                modal: true
                , buttons: {
                    'Thoát': function() {
                        $(this).dialog("close");
                    }
                }
            });
        } else {
            msg = "Có phải bạn muốn tạo các kỳ trong năm tài chính này?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';
           
            // append dialog-taoky to body
            $('body').append('<div title="Thông Báo" id=\"dialog-taoky\">' + h3 +'</div>');

            // create new dialog
            $('#dialog-taoky').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-get",
                        text: "Có",
                        click: function() {
                            $("#dialog-caution").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-get").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-caution").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get("Controllers/KyTrongNamController.ashx?action=autocreate&yearId=" + id, function(result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-caution').html(result);
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
</script>

<DIV class="ui-layout-north ui-widget-content" id="layout-north-namtaichinh">
    <uc1:jqGrid  ID="grid_ds_namtaichinh" 
            Caption="Năm tài chính"
            SortName="ten_namtaichinh" 
            UrlFileAction="Controllers/NamTaiChinhController.ashx" 
            ColNames="['a_namtaichinh_id', 'Tên Năm Tài Chính', 'Năm', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            FuncTaoKy = "funcTaoKy"
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
                fixed: true, name: 'a_namtaichinh_id'
                , index: 'a_namtaichinh_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ten_namtaichinh'
                 , index: 'ten_namtaichinh'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'nam'
                 , index: 'nam'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required:true, number: true, minValue: 1980, maxValue: 2500 }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()+ 4 - 77);" 
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_kytrongnam').jqGrid('getGridParam','records') &gt; 0 )
			                {
			                    var caption = $(this).jqGrid('getCell', ids, 'nam');
			                    $('#grid_ds_kytrongnam').jqGrid('setCaption', 'Kỳ trong năm: ' + caption);
			                
				                $('#grid_ds_kytrongnam').jqGrid('setGridParam',{url:'Controllers/KyTrongNamController.ashx?&yearId='+ids,page:1});
				                $('#grid_ds_kytrongnam').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
				            var caption = $(this).jqGrid('getCell', ids, 'nam');
			                $('#grid_ds_kytrongnam').jqGrid('setCaption', 'Kỳ trong năm: ' + caption);
			                
			                $('#grid_ds_kytrongnam').jqGrid('setGridParam',{url:'Controllers/KyTrongNamController.ashx?&yearId='+ids,page:1});
			                $('#grid_ds_kytrongnam').jqGrid().trigger('reloadGrid');			
		                } }"
		    AddFormOptions=" 
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
            AfterRefresh = "function(){ 
                jQuery('#grid_ds_kytrongnam').jqGrid().trigger('reloadGrid');
            }"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            runat="server" />
</DIV>
<DIV class="ui-layout-center ui-widget-content" id="lay-center-namtaichinh" style="padding:0;"> 
    <uc1:jqGrid  ID="grid_ds_kytrongnam" 
            Caption="Kỳ trong năm"
            SortName="ktn.soky" 
            SortOrder = "asc"
            UrlFileAction="Controllers/KyTrongNamController.ashx" 
            ColNames="['a_kytrongnam_id', 'Năm Tài Chính', 'Số Kỳ', 'Tên Kỳ', 'Ngày Bắt Đầu', 'Ngày Kết Thúc', 'Loại Kỳ', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
            ColModel = "[
            {
                fixed: true, name: 'a_kytrongnam_id'
                , index: 'ktn.a_kytrongnam_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'a_namtaichinh_id'
                 , index: 'namtc.a_namtaichinh_id'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , hidden:true
            },
            {
                fixed: true, name: 'soky'
                , index: 'ktn.soky'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules: { number: true }
            },
            {
                fixed: true, name: 'tenky'
                , index: 'ktn.tenky'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'ngaybatdau'
                , index: 'ktn.ngaybatdau'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                fixed: true, name: 'ngayketthuc'
                , index: 'ktn.ngayketthuc'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                fixed: true, name: 'loaiky'
                , index: 'ktn.loaiky'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { value: 'CHUAN:Kỳ chuẩn;DIEUCHINH:Kỳ điều chỉnh' }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ktn.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'ktn.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ktn.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'ktn.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true 
            },
            {
                fixed: true, name: 'mota'
                , index: 'ktn.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'ktn.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()+ 4 - 77);"
            Height = "420" 
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                    var masterId = $('#grid_ds_namtaichinh').jqGrid('getGridParam', 'selrow');
                    var forId = 'a_namtaichinh_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        alert('Hãy chọn một năm tài chính mới có thể tạo chi tiết.!');
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

<script type="text/javascript">
    createRightPanel('grid_ds_namtaichinh');
    createRightPanel('grid_ds_kytrongnam');
</script>