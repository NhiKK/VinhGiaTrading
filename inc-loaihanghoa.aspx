<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-loaihanghoa.aspx.cs" Inherits="inc_loaihanghoa" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	function ChangeStatus() {
        var md_chungloai_id = $('#grid_ds_chungloai_hanghoa').jqGrid('getGridParam', 'selrow');
            $('body').append("<div title=\"Thay đổi trạng thái cho chủng loại hàng hóa\" id=\"dialog-changestatus\">" +
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
                        "<label for=\"toactive\">Chuyển tất cả chủng loại \"Soạn thảo\" sang \"Hiệu lực\"</label>" +
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
                        var pagram = $('#grid_ds_chungloai_hanghoa').jqGrid('getGridParam', 'postData');
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

                            $.get('Controllers/ChungLoaiController.ashx?action=updatestatus&md_chungloai_id=' + md_chungloai_id + '&trangthai=' + statusType, pagram, function(result) {
                                $("#dialog-changestatus").find("#caution").append(result);
                                $('#grid_ds_chungloai_hanghoa').jqGrid().trigger('reloadGrid');
                            });
                        } else {
                            alert('Chưa chọn trạng thái cho chủng Loại SP.!');
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
	
	function funcPrintNT() {
        var xkId = $('#grid_ds_chungloai_hanghoa').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-nk';
        var title = 'Trích xuất dữ liệu';
        var content = `<table style="width:100%">
							<tr>
                                <td>
                                </td>

                                <td>
                                    <input id="printExcel" type="radio" name="rdoPrintType" value="printExcel" checked />
                                    <label for="printExcel">Chiết xuất loại hàng hóa Excel</label>
                                </td>
                            </tr>
                      </table>`;

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
					var doitackinhdoanh_id = $("#txtDoiTacKH").val();
                    var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-nk').val();
                    var printURL = "PrintControllers/";
                    var windowSize = "width=700,height=700,scrollbars=yes";
                    if (typeof printType != 'undefined') {
							var postfilt = $('#grid_ds_chungloai_hanghoa').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InLoaiSanPham";
                            window.open(printURL + "?&filters=" + postfilt, "Chiết xuất loại hàng hóa", windowSize);
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

<uc1:jqGrid  ID="grid_ds_chungloai_hanghoa" 
            Caption="Chủng loại hàng hóa"
            FilterToolbar="true"
            SortName="cl.code_cl" 
            SortOrder="desc"
			BtnPrint="funcPrintNT"
            UrlFileAction="Controllers/ChungLoaiController.ashx" 
            ColNames="['md_chungloai', 'Trạng thái', 'Code Chủng Loại', 'Tiếng Việt', 'Tiếng Anh', 'Tiếng Việt Dài', 'Tiếng Anh Dài',  'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động', 'Thuộc nhóm']"
            RowNumbers="true"
			FuncChangeStatus="ChangeStatus"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, { 
                                            beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }
                                            , afterSubmit: function(rs, postdata){
                                                return showMsgDialog(rs);
                                            }
                                        } 
                            ); }"
            ColModel = "[
            {
                fixed: true, name: 'md_chungloai_id'
                , index: 'cl.md_chungloai_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
			{
                fixed: true, name: 'trangthai'
                , index: 'sp.trangthai'
                , width: 100
                , editable:false
                , formatter:'imagestatus'
                , align: 'center'
                , search: false
                , viewable: false
                , sortable: true
            },
            {
                fixed: true, name: 'code_cl'
                , index: 'cl.code_cl'
                , width: 110
                , edittype: 'text'
                , editable:true
                , align: 'center'
                , editrules: { required:true }
            },
            {
                 fixed: true, name: 'tv_ngan'
                 , index: 'cl.tv_ngan'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
                 , editrules: { required:true }
            },
            {
                fixed: true, name: 'ta_ngan'
                , index: 'cl.ta_ngan'
                , width: 250
                , edittype: 'text'
                , editable:true
                , editrules: { required:true }
            },
            {
                fixed: true, name: 'tv_dai'
                , index: 'cl.tv_dai'
                , width: 250
                , edittype: 'text'
                , editable:false
                , hidden: true
            },
            {
                fixed: true, name: 'ta_dai'
                , index: 'cl.ta_dai'
                , width: 250
                , edittype: 'text'
                , editable:false
                , hidden: true
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'cl.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'cl.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'cl.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'cl.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'cl.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'cl.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            },
			{
                fixed: true
				, name: 'nhomchungloai'
                , index: 'cl.nhomchungloai'
                , width: 100
                , editable:true
                , edittype: 'select'
				, editoptions: { dataUrl: 'Controllers/ChungLoaiController.ashx?action=nhomchungloai' }
				, stype:'select'
                , searchoptions:{ dataUrl: 'Controllers/ChungLoaiController.ashx?action=nhomchungloai' }
            }
            ]"
            Height = "100"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions ="beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }
                            , afterSubmit: function(rs, postdata){
                                return showMsgDialog(rs);
                            }
                            "
            EditFormOptions="beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }
                            , afterSubmit: function(rs, postdata){
                                return showMsgDialog(rs);
                            }
                            "
            DelFormOptions="beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }"
            ViewFormOptions="width: 500
                            ,beforeShowForm: function (formid) {
                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                            }"
            runat="server" />
			

<script>
	if(chietxuatLHH.toLowerCase() == 'false') {
		var parent = $('#grid_ds_chungloai_hanghoa-pager_left');
		parent.find('.ui-icon.ui-icon-print').parent().parent().remove();
	}

	createRightPanel('grid_ds_chungloai_hanghoa');
</script>