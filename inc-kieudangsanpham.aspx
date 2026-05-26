<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-kieudangsanpham.aspx.cs" Inherits="inc_kieudangsanpham" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>

<script>
function ChangeStatus() {
        var md_kieudang_id = $('#grid_ds_kieudangsanpham').jqGrid('getGridParam', 'selrow');
            $('body').append("<div title=\"Thay đổi trạng thái cho kiểu dáng SP\" id=\"dialog-changestatus\">" +
            "<div style='display:none' id='wait'><img style='width:30px; height:30px' src='iconcollection/loading.gif'/></div>" +
            "<div id=\"caution\"></div>"+
            "<table>" +
                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"HIEULUC\" value=\"HIEULUC\" name=\"rdoStatus\" checked=\"checked\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"HIEULUC\">Chuyển trạng thái \"Hiệu lực\"</label>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"SOANTHAO\" value=\"SOANTHAO\" name=\"rdoStatus\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"SOANTHAO\">Chuyển trạng thái \"Soạn thảo\"</label>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td>" +
                        "<input type=\"radio\" id=\"toactive\" value=\"TOACTIVE\" name=\"rdoStatus\" />" +
                    "</td>" +
                    "<td>" +
                        "<label for=\"toactive\">Chuyển tất cả kiểu dáng \"Soạn thảo\" sang \"Hiệu lực\"</label>" +
                    "</td>" +
                "</tr>" +

            "</table>" +
        "</div>");

            $("#dialog-changestatus").dialog({
                width: 500
                , height:250
                , modal: true
                , open: function(event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
                , buttons: [
                {
                    id: "btn-change",
                    text: "Thay đổi",
                    click: function() {
                        var pagram = $('#grid_ds_kieudangsanpham').jqGrid('getGridParam', 'postData');
                        var statusType = $('input[name=rdoStatus]:checked', '#dialog-changestatus').val();
                        if (typeof statusType != 'undefined') {
                            $("#dialog-changestatus").ajaxStart(function() {
                                $("#wait").css("display", "block");
                                $("#btn-change").button("disable");
                                $("#btn-close").button("disable");
                            });

                            $("#dialog-changestatus").ajaxComplete(function() {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });

                            $.get('Controllers/KieuDangSanPhamController.ashx?action=updatestatus&md_kieudang_id=' + md_kieudang_id + '&trangthai=' + statusType, pagram, function(result) {
                                $("#dialog-changestatus").find("#caution").append(result);
                                $('#grid_ds_kieudangsanpham').jqGrid().trigger('reloadGrid');
                            });
                        } else {
                            alert('Chưa chọn trạng thái cho kiểu dáng sản phẩm.!');
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
    }
	
	function funcPrintKDSP() {
        var xkId = $('#grid_ds_kieudangsanpham').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-kdsp';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
							'<tr>' +
                                '<td>' +
                                '</td>' +

                                '<td>' +
                                    '<input id="printExcel" type="radio" name="rdoPrintType" value="printExcel" checked />' +
                                    '<label for="printExcel">Chiết xuất kiểu dáng Excel</label>' +
                                '</td>' +
                            '</tr>' +
                      '</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        name = '#' + name;
		$(name).dialog({
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
                    var printType = $('input[name=rdoPrintType]:checked', name).val();
                    var printURL = "PrintControllers/";
                    var windowSize = "width=700,height=700,scrollbars=yes";
                    if (typeof printType != 'undefined') {
                        if (printType == "printRequired") {
                            var postfilt = $('#grid_ds_kieudangsanpham').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InKDSP/";
                            window.open(printURL + "?md_kieudang_id=" + xkId + "&filters=" + postfilt, "Chiết xuất kiểu dáng", windowSize);
                        }
						else {
							var postfilt = $('#grid_ds_kieudangsanpham').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InKDSP/InKDSP.aspx";
                            window.open(printURL + "?md_kieudang_id=" + xkId + "&filters=" + postfilt, "Chiết xuất kiểu dáng", windowSize);
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

    <uc1:jqGrid  ID="grid_ds_kieudangsanpham" 
            SortName="md_kieudang_id" 
            UrlFileAction="Controllers/KieuDangSanPhamController.ashx" 
			BtnPrint="funcPrintKDSP"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 65);"
            ColNames="['md_kieudang_id', 'Trạng thái', 'Mã KD', 'Tên Tiếng Việt', 'Tên Tiếng Anh', 'Tên Tiếng Việt Dài', 'Tên Tiếng Anh Dài', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
            RowNumbers="true"
            FilterToolbar="true"
			FuncChangeStatus="ChangeStatus"
            OndblClickRow = "function(rowid) { /*$(this).jqGrid('editGridRow', rowid,{
                beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
            } );*/ }"
            ColModel = "[
            {
                fixed: true, name: 'md_kieudang_id'
                , index: 'md_kieudang_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
			{
                fixed: true, name: 'trangthai'
                , index: 'trangthai'
                , width: 100
                , editable:false
                , formatter:'imagestatus'
                , align: 'center'
                , search: false
                , viewable: false
                , sortable: true
            },
            {
                 fixed: true, name: 'ma_kieudang'
                 , index: 'ma_kieudang'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ten_tv'
                 , index: 'ten_tv'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ten_ta'
                 , index: 'ten_ta'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
            },
            {
				fixed: true, name: 'ten_tv_dai'
				, index: 'ten_tv_dai'
				, width: 250
				, edittype: 'text'
				, editable: false
				, hidden: true
            },
            {
                fixed: true, name: 'ten_ta_dai'
                , index: 'ten_ta_dai'
                , width: 250
                , edittype: 'text'
                , editable:false
				, hidden: true
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
                , hidden: false
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
	if(chietxuatKDSP.toLowerCase() == 'false') {
		var parent = $('#grid_ds_kieudangsanpham-pager_left');
		parent.find('.ui-icon.ui-icon-print').parent().parent().remove();
	}

	createRightPanel('grid_ds_kieudangsanpham');
</script>