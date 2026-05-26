<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoiGiaDonHangDaHieuLuc.aspx.cs" Inherits="Tool_DoiGiaDonHangDaHieuLuc" %>
<% if(!send) { %>
<style type="text/css">
    .frmMauIn table {
        width: 100%;
        max-width: 450px;
        border-collapse: collapse;
        margin: 0px auto;
    }

    .frmMauIn {
        margin-top: 15px;
        margin-left: 15px;
    }

    .frmMauIn h1 {
        margin-bottom: 10px;
        font-size: 160%;
    }

    .frmMauIn label {
        font-weight: bold;
    }

    .frmMauIn input[type="file"], .frmMauIn textarea {
        width: 100%;
        padding-left: 3px;
    }

    .frmMauIn input[type="button"] {
        padding: 5px 10px 5px 10px;
    }

    .frmMauIn fieldset {
        padding: 15px;
        max-width: 500px;
        margin: 30px auto;
        background-color: #FFF;
    }

    .frmMauIn .spaceTr {
        width: 5px;
        height: 15px;
    }

    #ktcExcel, #ktcPDF {
        width: 50px;
    }

    #LuuChinhSua {
        max-width: 120px;
    }

    .tdRight {
        text-align: right;
    }

    .tdTop {
        vertical-align: top;
        top: 2px;
        position: relative;
    }
</style>
<div class="frmMauIn">
    
    <fieldset>
        <legend style="text-align:center">
            <span style="font-weight: bold; font-size: 160%;">Đổi giá đơn hàng đã hiệu lực</span>
        </legend>
         
        <table>
             <tr>
                <td class="tdRight">
                    <label>Excel:&nbsp;</label>
                </td>
                 <td></td>
                <td>
                    <input id="fileDGDHHL" type="file" accept=".xls,.xlsx" style="border: 1px solid #CCC; padding: 5px;" />
                </td>
            </tr>
        
            <tr><td><div class="spaceTr"></div></td></tr>

             <tr>
                <td class="tdRight">
                    <label>Loại:&nbsp;</label>
                </td>
                 <td></td>
                <td>
                    <select id="typeDGDHHL">
                        <option>PO</option>
                        <option>DSDH</option>
                    </select>
                </td>
            </tr>

            <tr><td><div class="spaceTr"></div></td></tr>

            <tr>
                <td class="tdRight">
                    <label>Mẫu:&nbsp;</label>
                </td>
                 <td></td>
                <td>
                    <a target="_blank" href="Mẫu import/mẫu đổi giá (PO hoặc DSDH).xlsx" style="color: blue">
                        Tải mẫu excel ở đây    
                    </a>
                </td>
            </tr>

            <tr><td><div class="spaceTr"></div></td></tr>

           <tr>
                <td style="text-align:center" colspan="3">
                    <input type="button" value="Thực hiện đổi giá" id="LuuChinhSuaDGDHHL" />
                </td>
            </tr>
        </table>
    </fieldset>

    <script type="text/javascript">
        let $fileDGDHHL = $('#fileDGDHHL');
        
        document.getElementById('LuuChinhSuaDGDHHL').onclick = function () {
            if (!$fileDGDHHL.val()) {
                alert('Phải 1 chọn tập tin excel');
                return;
            }

            let textTitle = $('#typeDGDHHL').val() == 'PO' ? 'đơn hàng' : 'đơn đặt hàng';

            $('body').append(`
                <div title="Đổi giá ${textTitle} đã hiệu lực" id="dialog-doigiaDHDHL">
                    <p style="padding: 3px; background-color: #f8fbc6;">
                        <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em; top: -3px; position: relative;"></span>
                        <span class="dialog-caution">Bạn có chắc muốn đổi giá ${textTitle}?</span>
                    </p>
                    <div style="display:none" id="wait-doigiaDHDHL"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div>
                    <div class="dialog-result"></div>
                </div>
            `);

            // create new dialog
            let $dialogDoigiaDHDHL = $('#dialog-doigiaDHDHL');
            let $dialogCaution = $dialogDoigiaDHDHL.find('.dialog-caution');
            let $dialogResult = $dialogDoigiaDHDHL.find('.dialog-result');
            let $waitDoigiaDHDHL = $("#wait-doigiaDHDHL");
            let $btnOkDoigiaDHDHL, $btnCloseDoigiaDHDHL;
            $dialogDoigiaDHDHL.dialog({
                width:600
                , height:500
                , modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                    $btnOkDoigiaDHDHL = $("#btn-ok-doigiaDHDHL");
                    $btnCloseDoigiaDHDHL = $("#btn-close-doigiaDHDHL");
                }
                , buttons:
                [
                    {
                        id: "btn-ok-doigiaDHDHL",
                        text: "Có, Tôi chắc chắn",
                        click: function () {
                            $dialogResult.html('');
                            $waitDoigiaDHDHL.css("display", "block");
                            $btnOkDoigiaDHDHL.button("disable");
                            $btnCloseDoigiaDHDHL.button("disable");
                            $dialogCaution.html("Hệ thống đang xử lý. Vui lòng không tắt trình duyệt cho đến khi hoàn thành.!");

                            let formData = new FormData();
                            formData.append("file", $fileDGDHHL[0].files[0]);
                            formData.append("type", $('#typeDGDHHL').val());
                            formData.append("send", "true");

                            $.ajax({
                                url: 'Tool/DoiGiaDonHangDaHieuLuc.aspx',
                                type: 'POST',
                                data: formData,
                                success: function (result) {
                                    $dialogResult.html(result);
                                    $waitDoigiaDHDHL.css("display", "none");
                                    $btnCloseDoigiaDHDHL.find('span').text("Thoát");
                                    $btnCloseDoigiaDHDHL.button("enable");
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    $dialogCaution.html('Xảy ra lỗi không mong đợi, vui lòng thử lại.');
                                    $waitDoigiaDHDHL.css("display", "none");
                                    $btnCloseDoigiaDHDHL.find('span').text("Thoát");
                                    $btnCloseDoigiaDHDHL.button("enable");
                                },
                                cache: false,
                                contentType: false,
                                processData: false
                            });
                        }
                    },
                    {
                        id: "btn-close-doigiaDHDHL",
                        text: "Không",
                        click: function() {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
            });
        }
    </script>
</div>

<%} %>