<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-importwebsite.aspx.cs" Inherits="inc_importwebsite" %>
<script>
    $(document).ready(function () {
        $("#lay-center-import").parent().layout({
            north: {
                spacing_open: 0
            },
            west: {
                size: "30%"
            }
        });
        $('.btn').button();

        $('#btnImportWeb').click(function () {
            var type = $('input[name=rdImpType]:checked', '.frm-impweb').val();
            var link = "";
            switch (type) {
                case "allproduct":
                    link = 'ImportAllProduct.ashx';
                    $('.lblCaption').html('Đang thực hiện Cập nhật lại tất cả sản phẩm.');
                    break;
                case "repdesc":
                    link = 'ImportRequiredDescription.ashx';
                    $('.lblCaption').html('Cập nhật lại quy tắc mô tả.');
                    break;
                default:
                    $('.lblCaption').html('Không xác định.');
            }

            
            $(this).attr('disabled', 'disabled');

            $.ajax({
                url: 'Controllers/' + link,
                type: "GET",
                success: function (rs) {
                    $(this).removeAttr('disabled');
                    $('.lblCaption').html(rs);
                }
            });
        });
    });


</script>

<style>
    .frm-impweb p
    {
        width:300px;
        font-size:15px;
        padding-bottom:5px;
    }
</style>


<div class="ui-layout-center ui-widget-content" id="lay-center-import">
    <div style="width:100%; margin:0px 0px 0px 20px">
        <br />
        <h1 style="font-size:28px; text-align:center">Import dữ liệu cho website ANCO</h1>
        <br />
        <div class="frm-impweb">
            <p>Hãy chọn chức năng:</p>
            <p><input type="radio" name="rdImpType" value="allproduct" id="rdAllProduct" checked="checked" /><label for="rdAllProduct">Cập nhật lại tất cả sản phẩm</label></p>
            <p><input type="radio" name="rdImpType" value="repdesc" id="rdRepDesc" /><label for="rdRepDesc">Cập nhật lại quy tắc mô tả</label></p>
            <div>
                <br />
                <p class="lblCaption"></p>
                <button id="btnImportWeb" class="btn">Thực hiện import</button>
            </div>
        </div>
    </div>
</div>
