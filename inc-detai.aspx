<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-detai.aspx.cs" Inherits="inc_detai" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<style type="text/css">
    .btnVNN 
    {
        color: blue;
        text-decoration: underline;
        cursor: pointer;
    }
</style>
<script>
	function ChangeStatus() {
        var md_detai_id = $('#grid_ds_detaimain').jqGrid('getGridParam', 'selrow');
            $('body').append(
	        `<div title="Thay đổi trạng thái cho đề tài" id="dialog-changestatus">
		        <div style='display:none' id='wait'><img style='width:30px; height:30px' src='iconcollection/loading.gif'/></div>
		        <div id="caution"></div>
		        <table>
			        <tr>
				        <td>
					        <input type="radio" id="HIEULUC" value="HIEULUC" name="rdoStatus" checked="checked" />
				        </td>
				        <td>
					        <label for="HIEULUC">Chuyển trạng thái "Hiệu lực"</label>
				        </td>
			        </tr>

			        <tr>
				        <td>
					        <input type="radio" id="SOANTHAO" value="SOANTHAO" name="rdoStatus" />
				        </td>
				        <td>
					        <label for="SOANTHAO">Chuyển trạng thái "Soạn thảo"</label>
				        </td>
			        </tr>
			
			        <tr>
				        <td>
					        <input type="radio" id="NHD" value="NHD" name="rdoStatus" />
				        </td>
				        <td>
					        <label for="NHD">Chuyển trạng thái "Ngưng hoạt động"</label>
				        </td>
			        </tr>

			        <tr>
                        <td>
                            <input type="radio" id="toactive" value="TOACTIVE" name="rdoStatus" />
                        </td>
                        <td>
                            <label for="toactive">Chuyển tất cả đề tài đang lọc từ "Soạn thảo" sang "Hiệu lực"</label>
                        </td>
                    </tr>
					
					<tr>
                        <td>
                            <input type="radio" id="toactiveNHD" value="TOACTIVENHD" name="rdoStatus" />
                        </td>
                        <td>
                            <label for="toactiveNHD">Chuyển tất cả đề tài đang lọc từ "Ngưng hoạt động" sang "Hiệu lực"</label>
                        </td>
                    </tr>
		        </table>
	        </div>`);

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
                        var pagram = $('#grid_ds_detaimain').jqGrid('getGridParam', 'postData');
                        var statusType = $('input[name=rdoStatus]:checked', '#dialog-changestatus').val();
                        if (typeof statusType != 'undefined') {
							$("#wait").css("display", "block");
							$("#btn-change").button("disable");
							$("#btn-close").button("disable");
                            $.get('Controllers/DeTaiMainController.ashx?action=updatestatus&md_detai_id=' + md_detai_id + '&trangthai=' + statusType, pagram, function(result) {
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
								$("#dialog-changestatus").find("#caution").append(result);
                                $('#grid_ds_detaimain').jqGrid().trigger('reloadGrid');
                            });
                        } else {
                            alert('Chưa chọn trạng thái cho đề tài.!');
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
	
	function funcPrintDeTai() {
        var xkId = $('#grid_ds_detaimain').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-kdsp';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
							'<tr>' +
                                '<td>' +
                                '</td>' +

                                '<td>' +
                                    '<input id="printExcel" type="radio" name="rdoPrintType" value="printExcel" checked />' +
                                    '<label for="printExcel">Chiết xuất đề tài Excel</label>' +
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
                            var postfilt = $('#grid_ds_detaimain').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InDeTai/";
                            window.open(printURL + "?md_detai_id=" + xkId + "&filters=" + postfilt, "Chiết xuất đề tài", windowSize);
                        }
						else {
							var postfilt = $('#grid_ds_detaimain').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InDeTai/InDeTai.aspx";
                            window.open(printURL + "?md_detai_id=" + xkId + "&filters=" + postfilt, "Chiết xuất đề tài", windowSize);
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

<script>
    $(document).ready(function () {
        $('#layout-center-grid_ds_detaimain').parent().layout({
            center: {
                size: "70%"
                , onresize_end: function () {
                    var h = $("#layout-center-grid_ds_detaimain").height();
                    var w = $("#layout-center-grid_ds_detaimain").width();
                    $("#grid_ds_detaimain").setGridHeight(h - 65);
                    $("#grid_ds_detaimain").setGridWidth(w - 2);
                }
            }
            , east: {
                size: "30%"
                , onresize_end: function () {

                }
            }
        });
    });
</script>

<div class="ui-layout-center ui-widget-content" id="layout-center-grid_ds_detaimain">
    <uc1:jqGrid  ID="grid_ds_detaimain" 
            SortName="" 
            UrlFileAction="Controllers/DeTaiMainController.ashx" 
			BtnPrint="funcPrintDeTai"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 65);"
            ColNames="['md_detai_id', 'Trạng thái', 'Chủng loại', 'Code Đề tài', 'Tên Tiếng Việt', 'Tên Tiếng Anh', 'Tên Tiếng Việt Dài', 'Tên Tiếng Anh Dài', 'Hình Thức Bán', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
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
            OnSelectRow = "function(ids) {
		            if(ids != null) {
                        let rowSel = $('#grid_ds_detaimain').jqGrid('getRowData', ids);
                        let img = rowSel['md_chungloai_id'] + '-' + rowSel['code_dt'];

                        $('#img-grid_ds_detaimain').attr('src', '');
                        $('#img-grid_ds_detaimain').attr('src', 'Controllers/API_System.ashx?oper=loadImageDetai&msp=' + img + '&ver=' + uuidv4());
                    }
                }"
            ColModel = "[
            {
                fixed: true, name: 'md_detai_id'
                , index: 'md_detai_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
			{
				fixed: true, name: 'trangthai'
				, index: 'dt.trangthai'
				, width: 100
				, editable:false
				, formatter:'imagestatus'
				, align: 'center'
				, search : true
				, stype : 'select'
				, searchoptions:{ value:':[ALL];SOANTHAO:SOẠN THẢO;HIEULUC:HIỆU LỰC;NHD:NGƯNG HOẠT ĐỘNG'}
				, viewable: false
				, sortable: true
			},
            {
				fixed: true, name: 'md_chungloai_id'
				, index: 'cl.code_cl'
				, editable: true
				, width: 100
				, edittype: 'select'
				, editoptions: { dataUrl: 'Controllers/ChungLoaiController.ashx?action=getoption' }
            },
			{
                 fixed: true, name: 'code_dt'
                 , index: 'code_dt'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'tv_ngan'
                 , index: 'dt.tv_ngan'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ta_ngan'
                 , index: 'dt.ta_ngan'
                 , editable: true
                 , width: 250
                 , edittype: 'text'
            },
            {
				fixed: true, name: 'tv_dai'
				, index: 'dt.tv_dai'
				, width: 250
				, edittype: 'text'
				, editable: false
				, hidden: true
            },
            {
                fixed: true, name: 'ta_dai'
                , index: 'dt.ta_dai'
                , width: 250
                , edittype: 'text'
                , editable:false
				, hidden: true
            },
			{
                fixed: true, name: 'hinhthucban'
                , index: 'dt.hinhthucban'
                , width: 120
                , edittype: 'select'
				, editoptions: { value: selectHinhThucBan }
                , editable:true
				, hidden: false
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'dt.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'dt.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'dt.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'dt.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'dt.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: false
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'dt.hoatdong'
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
</div>

<div class="ui-layout-east ui-widget-content" id="layout-east-grid_ds_detaimain">
    <table style="width:100%; height:100%">
        <tr>
            <td style="text-align: center;">
                <img alt="Không có hình ảnh" id="img-grid_ds_detaimain" class="loadingIMG" style="max-width:100%; margin: 0px auto; min-width: 40px;" src="Controllers/API_System.ashx?oper=loadImageDetai&msp=default" />
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
    function UploadHinhAnhgrid_ds_detaimain() {
        var data_js = new FormData();
        var div = $(
        `<form  enctype="multipart/form-data" id="frm_FileHAMauSac">
            <div class="mess"></div>
            <input type="button" onclick="$(this).next().click();" value="Chọn hình ảnh" />
            <input type="file" style="display:none" multiple accept=".jpg" />
        </form>`);
            div.dialog({
                title: "Upload hình ảnh theo đề tài",
                resizeable: false,
                modal: true,
                width: 400,
                height: 300,
                open: function(event, ui) {
                   var fileInput = $('#frm_FileHAMauSac input[type="file"]');
                   var mess = div.find('.mess');
                   fileInput.change(function(){
                      var files = $(this)[0].files;
                      var messFile = '';
                      for(var i=0; i < files.length; i++) {
                        var file = files[i];
                        var spanRemove = $('span.btnVNN[fileN="'+ file.name +'"]');
                        if(spanRemove.attr('fileN') != null) {
                            spanRemove.click();
                        }

                        messFile += `
                            <tr class="rowFile" style="height: 25px;">
                                <td>${file.name} (${file.size} Byte)</td>
                                <td><span class="btnVNN" fileI="${i}" fileN="${file.name}">X</span></td>
                            </tr>`;

                        data_js.append('hinhanh' + i, file);
                      }

                      var table = $('#frm_FileHAMauSac table');
                      if(table.prop('tagName') == null) { 
                          messFile = `<table style="width:100%">
                            <tr style="height: 25px;">
                                <td style="font-weight:bold">Tên</td>
                                <td style="font-weight:bold"><a class="btnVNN">Xóa</a></td>
                            </tr>
                            ${messFile}
                            </table>`;
                          mess.html(messFile);
                      }
                      else {
                        table.append(messFile);
                      }

                      $('span.btnVNN').off('click');
                      $('span.btnVNN').click(function(){
                        var index = $(this).attr('fileI');
                        $(this).parent().parent().remove();
                        data_js.delete('hinhanh' + index);
                        if($('tr.rowFile').length <= 0) {
                            $('#frm_FileHAMauSac table').remove();
                        }
                      });

                      $('a.btnVNN').off('click');
                      $('a.btnVNN').click(function(){
                        var index = $(this).attr('fileI');
                        $(this).parent().parent().remove();
                        $('span.btnVNN').click();
                      });

                      $(this).val('');
                   });
                },
                close: function (event, ui) {
                    $(this).dialog('destroy').remove(); 
                },
                buttons: [
				{
				    text: "Upload",
				    id: "uploadHAMauSac",
				    click: function () {
                        var mess = div.find('.mess');
                        mess.html('<img style="width:30px; height:30px" src="iconcollection/loading.gif"/>');
                        $('#uploadHAMauSac').prop('disabled', true);
                        $.ajax({
                            url : 'Controllers/DeTaiMainController.ashx?action=HinhAnh',
                            type : 'POST',
                            data : data_js,
                            processData: false,  
                            contentType: false,
                            success : function(data) {
                                mess.html(data);
                                data_js = new FormData();
                                $('#uploadHAMauSac').prop('disabled', false);
                            }
                        });
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

    $("#grid_ds_detaimain").jqGrid('navButtonAdd', '#grid_ds_detaimain-pager', {
        caption: "Upload hình ảnh"
				, title: "Upload hình ảnh màu sắc"
				, onClickButton: function () {
				    UploadHinhAnhgrid_ds_detaimain();
				}
    });

    createRightPanel('grid_ds_detaimain');
</script>