<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-hscode.aspx.cs" Inherits="inc_hscode" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	function funcPrintHSCODE() {
        var xkId = $('#grid_ds_hscode').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-nk';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
							'<tr>' +
                                '<td>' +
                                '</td>' +

                                '<td>' +
                                    '<input id="printExcel" type="radio" name="rdoPrintType" value="printExcel" checked/>' +
                                    '<label for="printExcel">Chiết xuất HSCODE Excel</label>' +
                                '</td>' +
                            '</tr>' +
                      '</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
            modal: true
            , open: function (event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
            , buttons: [
            {
                id: "btn-print-ok",
                text: "In",
                click: function () {
                    var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-nk').val();
                    var printURL = "PrintControllers/";
                    var windowSize = "width=700,height=700,scrollbars=yes";
                    if (typeof printType != 'undefined') {
                        if (printType == "printRequired") {
                            var postfilt = $('#grid_ds_hscode').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InHSCODE/";
                            window.open(printURL + "?md_hscode_id=" + xkId + "&filters=" + postfilt, "Chiết xuất hàng hóa độc quyền", windowSize);
                        }
						else {
							var postfilt = $('#grid_ds_hscode').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InHSCODE/InHSCODE.aspx";
                            window.open(printURL + "?md_hscode_id=" + xkId + "&filters=" + postfilt, "Chiết xuất hàng hóa độc quyền", windowSize);
						}
                    } else {
                        alert('Hãy chọn cách in.!');
                    }
                }
            }
            , {
                id: "btn-print-close",
                text: "Thoát",
                click: function () {
                    $(this).dialog("destroy").remove();
                }
            }
            ]
        });
    }
</script>
    <uc1:jqGrid  ID="grid_ds_hscode" 
            SortName="" 
            UrlFileAction="Controllers/HsCodeController.ashx" 
			BtnPrint="funcPrintHSCODE"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 65);"
            ColNames="['md_hscode_id', 'Mã HSCode', 'HSCode', 'Hệ Hàng', 'Diễn giải', 'Diễn giải TA', 'THÀNH PHẦN % NVL CẤU THÀNH', 'NCƯ', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
            RowNumbers="true"
            FilterToolbar="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{
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
                fixed: true, name: 'md_hscode_id'
                , index: 'md_hscode_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ma_hscode'
                 , index: 'ma_hscode'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules:{ required:true }
            },
            {
                 fixed: true, name: 'hscode'
                 , index: 'hscode'
                 , editable: true
                 , width: 120
                 , edittype: 'text'
                 , editrules:{ required:true }
            },
			{
                 fixed: true, name: 'hehang'
                 , index: 'hehang'
                 , editable: true
                 , width: 120
                 , edittype: 'text'
            },
			{
                 fixed: true, name: 'tenhang_tv'
                 , index: 'tenhang_tv'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
            },
			{
                 fixed: true, name: 'tenhang_ta'
                 , index: 'tenhang_ta'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
            },
			{
                 fixed: true, name: 'thanhphan'
                 , index: 'thanhphan'
                 , editable: true
                 , width: 220
                 , edittype: 'textarea'
            },
			{
                 fixed: true, name: 'nhacungung'
                 , index: 'nhacungung'
                 , editable: true
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
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
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
			
			
			
			
<script>
	if(chietxuatHSCode.toLowerCase() == 'false') {
		var parent = $('#grid_ds_hscode-pager_left');
		parent.find('.ui-icon.ui-icon-print').parent().parent().remove();
	}

	createRightPanel('grid_ds_hscode');
</script>