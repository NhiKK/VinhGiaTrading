<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-nguondoitac.aspx.cs" Inherits="inc_nguondoitac" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	function funcPrintNguonDT() {
        var xkId = $('#grid_ds_nguondoitac').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-ngdt';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
		'<tr>' +
			'<td>' +
			'</td>' +

			'<td>' +
				'<input id="printExcel" type="radio" name="rdoPrintType" value="printExcel" checked />' +
				'<label for="printExcel">Chiết xuất Nguồn Đối Tác Excel</label>' +
			'</td>' +
		'</tr>' +
		'</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
		name = '#' + name;
        $(name).dialog({
            modal: true, 
			open: function (event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
            , buttons: [
            {
                id: "btn-print-ok",
                text: "In",
                click: function () {
                    var printType = $('input[name=rdoPrintType]:checked', name).val();
                    var printURL = "PrintControllers/";
                    var windowSize = "width=700,height=700,scrollbars=yes";
                    if (typeof printType != 'undefined') {
                        if (printType == "printRequired") {
                            var postfilt = $('#grid_ds_nguondoitac').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InNguonDoiTac/";
                            window.open(printURL + "?md_nguondtkd_id=" + xkId + "&filters=" + postfilt, "Chiết xuất nguồn đối tác", windowSize);
                        }
						else {
							var postfilt = $('#grid_ds_nguondoitac').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InNguonDoiTac/InNguonDoiTac.aspx";
                            window.open(printURL + "?md_nguondtkd_id=" + xkId + "&filters=" + postfilt, "Chiết xuất nguồn đối tác", windowSize);
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
    <uc1:jqGrid  ID="grid_ds_nguondoitac" 
            SortName="ma_nguondtkd" 
            UrlFileAction="Controllers/NguonDoiTacController.ashx" 
            BtnPrint="funcPrintNguonDT"
			GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 65);"
            ColNames="['md_nguondtkd_id', 'Mã Nguồn', 'Tên Nguồn', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
            RowNumbers="true"
			FilterToolbar="true"
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
            }); }"
            ColModel = "[
            {
                fixed: true, name: 'md_nguondtkd_id'
                , index: 'md_nguondtkd_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ma_nguondtkd'
                 , index: 'ma_nguondtkd'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                fixed: true, name: 'ten_nguondtkd'
                , index: 'ten_nguondtkd'
                , width: 100
                , edittype: 'text'
                , editable:true
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

<script type="text/javascript">
    createRightPanel('grid_ds_nguondoitac');
</script>