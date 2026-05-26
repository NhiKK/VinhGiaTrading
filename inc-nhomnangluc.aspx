<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-nhomnangluc.aspx.cs" Inherits="inc_nhomnangluc" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
	function funcPrintNhomNL() {
        var xkId = $('#grid_nhomnangluc').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-nk';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
							'<tr>' +
                                '<td>' +
                                '</td>' +

                                '<td>' +
                                    '<input id="printExcel" type="radio" name="rdoPrintType" value="printExcel" checked />' +
                                    '<label for="printExcel">Chiết xuất Nhóm NL Excel</label>' +
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
                            var postfilt = $('#grid_nhomnangluc').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InNhomNangLuc/";
                            window.open(printURL + "?md_hscode_id=" + xkId + "&filters=" + postfilt, "Chiết xuất Nhóm NL", windowSize);
                        }
						else {
							var postfilt = $('#grid_nhomnangluc').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InNhomNangLuc/InNhomNangLuc.aspx";
                            window.open(printURL + "?md_hscode_id=" + xkId + "&filters=" + postfilt, "Chiết xuất Nhóm NL", windowSize);
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
    <uc1:jqGrid  ID="grid_nhomnangluc" 
            SortName="nnl.md_nhomnangluc_id" 
            UrlFileAction="Controllers/NhomNangLucController.ashx" 
			BtnPrint="funcPrintNhomNL"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 65);"
            ColNames="['md_nhomnangluc_id','Hệ Hàng', 'Nhóm'
                    , 'Mô Tả TV','HS Code', 'Thời gian làm hàng', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
            RowNumbers="true"
            FilterToolbar="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            } ); }"
            ColModel = "[
            {
                fixed: true, name: 'md_nhomnangluc_id'
                , index: 'nnl.md_nhomnangluc_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_chungloai_id'
                 , index: 'cl.code_cl'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editrules: { required:true }
                 , editoptions: { dataUrl: 'Controllers/ChungLoaiController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'nhom'
                 , index: 'nnl.nhom'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules:{ required:true }
            },
            {
                 fixed: true, name: 'mota_tiengviet'
                 , index: 'nnl.mota_tiengviet'
                 , editable: true
                 , width: 250
                 , edittype: 'textarea'
            },
            {
                 fixed: true, name: 'hscode'
                 , index: 'nnl.hscode'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            }, 
            {
                 fixed: true, name: 'thoigianlamhang'
                 , index: 'nnl.thoigianlamhang'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions:{defaultValue: '0' }
                 , editrules: {number: true, required: true}
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
            
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
				afterSubmit: function(rs, postdata) { 
					alert(rs.responseText);
					$.jgrid.hideModal('#delmodgrid_nhomnangluc', {gbox:'#gbox_gridnhomnangluc'});
                    return false;
				},
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            "
            EditFormOptions ="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
				afterSubmit: function(rs, postdata) { 
					alert(rs.responseText);
					$.jgrid.hideModal('#delmodgrid_nhomnangluc', {gbox:'#gbox_gridnhomnangluc'});
                    return false;
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
				afterSubmit: function(rs, postdata) { 
					alert(rs.responseText);
					$.jgrid.hideModal('#delmodgrid_nhomnangluc', {gbox:'#gbox_gridnhomnangluc'});
                    return false;
				},
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
				}"
			Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            runat="server" />
			
<script type="text/javascript">
	if(chietxuatNNL.toLowerCase() == 'false') {
		var parent = $('#grid_nhomnangluc-pager_left');
		parent.find('.ui-icon.ui-icon-print').parent().parent().remove();
	}

	createRightPanel('grid_nhomnangluc');
</script>