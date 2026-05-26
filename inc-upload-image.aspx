<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-upload-image.aspx.cs" Inherits="inc_upload_image" %>
<script type="text/javascript">
    // Convert divs to queue widgets when the DOM is ready
    $(function() {
        $('#filelist').height($('#lay-center-upload').parent().height() - 70);
        $('.btn-action').button();
        $('#lay-center-upload').parent().layout({
            center: {
                onresize_end: function() { $('#filelist').height($('#lay-center-upload').parent().height() - 70); }
            }
        });
    });
</script>

<div class="ui-layout-center" id="lay-center-upload">
    <form method="post" action="Controllers/UploadFile.ashx" enctype="multipart/form-data">  
        <DIV class="ui-widget-header">            <div style="padding:10px">Upload Ảnh Sản Phẩm</div>        </DIV>        <div id="filelist" class="ui-layout-content" style="padding:0; background:#f0f0f0;"></div>        <DIV class="ui-widget-header">
            <div style="padding:3px">
                <span class="btn-action" id="pickfiles" href="#"><b>Chọn File</b></span>
                <span class="btn-action" id="resetall" href="#"><b>Bỏ Chọn Tất Cả</b></span>
                <span class="btn-action" id="uploadfiles" href="#"><b>Bắt Đầu Upload File</b></span>
            </div>
        </DIV>
    </form>
</div>

<script>
    $(function () {
        var uploader = new plupload.Uploader({
            runtimes: 'gears,html5,flash,silverlight,browserplus',
            browse_button: 'pickfiles',
            container: 'lay-center-upload',
            url: 'Controllers/UploadFile.ashx',
            flash_swf_url: 'jQuery/FileUpload/plupload.flash.swf',
            silverlight_xap_url: 'jQuery/FileUpload/FileUpload/plupload.silverlight.xap',
            filters: [
			{ title: "Image files", extensions: "jpg,gif,png" }
		]
        });

        uploader.bind('Init', function (up, params) {
            //$('#filelist').html("<div>Current runtime: " + params.runtime + "</div>");
        });

        $('#uploadfiles').click(function (e) {
            uploader.start();
            e.preventDefault();
        });

        $('#resetall').click(function (e) {
            $('#filelist *').remove();
            uploader.splice();
            uploader.refresh();
        });

        $(".delete_pending_file").live('click', function () {
            var file_id = $(this).attr('id');
            var file = uploader.getFile(file_id);

            if (file !== undefined) {
                uploader.removeFile(file);
                $('#file_' + file_id).remove();
            }
            return false;
        });

        uploader.init();

        uploader.bind('FilesAdded', function (up, files) {
            $.each(files, function (i, file) {
                $('#filelist').append(
                '<div style="padding:8px" class="ui-widget ui-state-default ui-corner-all ui-button-text-only" id="file_' + file.id + '">' + file.name +
                '<div id="' + file.id + '" class="delete_pending_file" style="cursor:pointer; float:right; width:80px; text-align:center">&times;</div>' +
                '<div class="progress-container" style="float:right; width:80px; text-align:center">0%</div>' +
                '<div style="float:right; width:80px; text-align:center">' + plupload.formatSize(file.size) + '</div>' +
                '<div style="clear:both"></div>' +
                '</div>'
                );
            });

            up.refresh(); // Reposition Flash/Silverlight
        });

        uploader.bind('UploadProgress', function (up, file) {
            $('#file_' + file.id + " .progress-container").html(file.percent + "%");
        });

        uploader.bind('Error', function (up, err) {
//            $('#filelist').append("<div>Error: " + err.code +
//			    ", Message: " + err.message +
//			    (err.file ? ", File: " + err.file.name : "") +
//			    "</div>"
//		  );
            alert(err.message);
            up.refresh(); // Reposition Flash/Silverlight
        });

        uploader.bind('FileUploaded', function (up, file) {
            $('#' + file.id + " b").html("100%");
        });
    });
</script>

