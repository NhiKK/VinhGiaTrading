<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-import.aspx.cs" Inherits="inc_import" %>
<%@ Import Namespace="System.Linq" %>
<%
	LinqDBDataContext db = new LinqDBDataContext();
    String manv = UserUtils.getUser(Context);
	nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
	var imports = (from a in db.md_imports.Where(s=>s.hoatdong == true)
				  join b in db.md_phanquyenimports on a.md_import_id equals b.md_import_id into c
				  from c1 in c.DefaultIfEmpty()
				  where (c1.manv == manv || nv.isadmin == true)
				  select new { a.ma_import, a.ten_import }).Distinct();
				  
%>
<script>
    $(document).ready(function() {
        updateTree();

        $("#lay-center-import").parent().layout({
            north: {
                spacing_open: 0
            },
            west: {
                size: "30%"
            }
        });

        $('#frm_import').ajaxForm(function(result) {
            updateTree();
        });

        $('.btn').button();


        $('#btnImport').click(function() {
            var filename = $('#file_physical').html();
            var impfor = $('#selTable').val();

            msg = "Xác nhận lại trước khi import dữ liệu.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Import dữ liệu" id=\"dialog-import-file\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-import-file').dialog({
                width:600
                , height:500
                , modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-import",
                        text: "Có",
                        click: function() {

                        $("#wait").css("display", "block");
                        $("#btn-import").button("disable");
                        $("#btn-close").button("disable");
                        $('#dialog-import-file').find('#dialog-caution').html("Hệ thống đang xử lý import. Vui lòng không tắt trình duyệt cho đến khi import hoàn thành.!");


                            $.get("ImportControllers/ImportController.ashx?table=" + impfor + "&filename=" + filename, function(result) {
                                $('#dialog-import-file').find('#dialog-caution').html(result);
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                                updateTree();
                            })
                                .fail(function () {
                                    $('#dialog-import-file').find('#dialog-caution').html('Xảy ra lỗi không mong đợi, vui lòng thử lại.');
                                    $("#wait").css("display", "none");
                                    $("#btn-close span").text("Thoát");
                                    $("#btn-close").button("enable");
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
            
            
            
        });
    });

    function useFile(file) {
        var url = $(file).attr('title');
        var name = $(file).find('.filename').html();
        $('#file_select').html("Đang chọn tập tin: " + name);
        $('#file_physical').html(name);
    }

    function delFile(btnDel) {

        var name = $(btnDel).parent().find('.filename').html();

        if (name == null) {
            alert('Chưa chọn tập tin cần xóa.');
        } else {

            msg = "Có phải bạn muốn xóa tập tin này.?";
            var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
            var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';

            // append dialog-taoky to body
            $('body').append('<div title="Xóa tập tin" id=\"dialog-delete-file\">' + h3 + '</div>');

            // create new dialog
            $('#dialog-delete-file').dialog({
                modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-delete",
                        text: "Có",
                        click: function() {
                            $("#wait").css("display", "block");
                            $("#btn-delete").button("disable");
                            $("#btn-close").button("disable");

                            $.get("Controllers/UploadFileImportController.ashx?action=delete&filename=" + name, function(result) {
                                $('#dialog-delete-file').find('#dialog-caution').html(result);
                                updateTree();

                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
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

    function updateTree() {
        $("#file_select").html("");
        $("#file_physical").html("");
        $("#file_caution").html("");
        $.ajax({
            type: "GET",
            url: "Controllers/UploadFileImportController.ashx?action=fileloader",
            dataType: "xml",
            success: function(result) {
                $("#lay-west-import").html("");
                $(result).find("file").each(function() {
                    var name = $(this).find("filename").text();
                    var url = $(this).find("fileurl").text();
                    
                    $("#lay-west-import").append(
                        '<div onclick="useFile(this)" title="' + url + '" class="file">' +
                            '<div class="filename">' + name + '</div>' +
                            '<div  onclick="delFile(this)" title="Xóa file này" class="btn-file btn-del">Xóa</div>' +
                        '</div>'
                    );
                    
                });
            }
        });
    }

    function removeAllFileOnTheLeft() {
        let files = $('#lay-west-import > div.file');
        if (files.length > 0) {
            if(confirm(`Xóa ${files.length} tập tin đang hiển thị bên trái?`)) {
                files.each(function () {
                    let name = $(this).find(".filename").text();
                    $.get("Controllers/UploadFileImportController.ashx?action=delete&filename=" + name, function(result) {
                        updateTree();
                    })
                        .fail(function () {
                            alert(`Xóa tập tin "${name}" thất bại.`);
                        });
                });
            }
        }
        else {
            alert('Không có tập tin nào.');
        }
    }
</script>

<style>
    .btn-file
    {
        position:absolute;
        border:1px solid green;
        padding:5px;
        background:green;
        color:White;
    }
    
    .btn-del
    {
        top:3px;
        right:5px;	
    }
    
    .file
    {
    	position:relative;
    	text-align:left;
    	background:#eaeaea;
    	border:1px solid #ccc;
    	padding:10px 5px 10px 5px;
    	margin:2px;
    	width:95%;
    	cursor:pointer;
    	min-width:150px;
    }
</style>



<div class="ui-layout-north ui-widget-content" id="lay-north-import">
    <table class="ui-widget-header" style="width:100%; height:40px">
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width:50px"></td>
                        <td style="width:120px">Chọn Tập Tin Excel</td>
                        <td>
                            <form id="frm_import" action="Controllers/UploadFileImportController.ashx?action=upload" method="post" enctype="multipart/form-data">
                                <input type="file" name="data" id="data" />
                                <input type="submit" id="btnUpload" class="btn" value="Upload File" />
                            </form>
                        </td>
                        <td style="width:50px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>

<div class="ui-layout-west ui-widget-content" id="lay-west-import" style="overflow:auto !important; text-align:center">
    
</div>

<div class="ui-layout-center ui-widget-content" id="lay-center-import">
    <div style="padding:20px" id="file_caution"></div>
    <div style="padding:20px" id="file_select"></div>
    <div style="display:none" id="file_physical"></div>
    <div style="width:100%; margin:0px 0px 0px 20px">
        <table>
            <tr>
                <td colspan="3"><input style="padding: 4px; position: absolute; top: 10px;" onclick="removeAllFileOnTheLeft()" type="button" value="Xóa các tập tin bên trái" /></td>
            </tr>
            <tr>
                <td>Import cho</td>
                <td>
                    <select style="padding:5px; font-size:13px" id="selTable">
						<%if(imports.Count() > 0) { %>
							<%foreach(var item in imports.OrderBy(s=>s.ten_import)) {%>
								<option value="<%=item.ma_import%>"><%=item.ten_import%></option>
							<%}%>
						<%} else { %>
							<option value="notVal">Bạn không có quyền import</option>
						<%} %>
                    </select>
                </td>
                <td>
					<%if(imports.Count() > 0) { %>
						<button id="btnImport" class="btn">Thực hiện import dữ liệu</button>
					<%} %>

                </td>
            </tr>
        </table>
    </div>
</div>
