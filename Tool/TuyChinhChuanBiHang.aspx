<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TuyChinhChuanBiHang.aspx.cs" Inherits="Tool_TuyChinhChuanBiHang" %>
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

    .frmMauIn input, .frmMauIn textarea {
        width: 100%;
        padding-left: 3px;
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

    #LuuChinhSuaTCCBH {
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
            <span style="font-weight: bold; font-size: 160%;">Tùy chỉnh số ngày chuẩn bị hàng</span>
        </legend>
         <table>
         <tr>
            <td class="tdRight tdTop">
                <label>Chọn mẫu: </label>
            </td>
             <td style="width:10px"></td>
            <td>
                <select id="mauTCCBH" class="selectRptTCCBH">
                    <%=loadOptions() %>
                </select>
            </td>
        </tr>
        
        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Số ngày tối thiểu: </label>
            </td>
            <td></td>
            <td>
                <input onKeyDown="return false" min="0" max="100" style="width:40px" id="songayTCCBH" type="number"/>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

       <tr>
            <td style="text-align:center" colspan="3">
                <input type="button" value="Lưu chỉnh sửa" id="LuuChinhSuaTCCBH" />
            </td>
        </tr>
    </table>
     </fieldset>
    <script type="text/javascript">
        let $mauTCCBH = $('#mauTCCBH');
        let $songayTCCBH = $('#songayTCCBH');
        
        $mauTCCBH.change(function () {
            let optionSel = $mauTCCBH[0].options[$mauTCCBH[0].selectedIndex];

            let mau = optionSel.getAttribute('mau');
            let songay = optionSel.getAttribute('songay');

            $songayTCCBH.val(songay);
        });

        $mauTCCBH.change();

        document.getElementById('LuuChinhSuaTCCBH').onclick = function () {
            if (!$mauTCCBH.val()) {
                alert('Phải chọn mẫu in');
                return;
            }

            if (confirm('Thực hiện lưu chỉnh sửa của bạn???'))
            {
                const XHR = new XMLHttpRequest();

                let urlEncodedData = "",
                    urlEncodedDataPairs = [],
                    name;
                
                let data =
                {
                    "mau": $mauTCCBH.val(),
                    "songay": $songayTCCBH.val(),
                    "send": "true"
                };

                for (name in data) {
                    urlEncodedDataPairs.push(encodeURIComponent(name) + '=' + encodeURIComponent(data[name]));
                }

                // Combine the pairs into a single string and replace all %-encoded spaces to 
                // the '+' character; matches the behaviour of browser form submissions.
                urlEncodedData = urlEncodedDataPairs.join('&').replace(/%20/g, '+');

                // Define what happens on successful data submission
                XHR.addEventListener('load', function (event) {
                    let data = event.currentTarget.response;
                    $mauTCCBH.html(data);
                    $mauTCCBH.val(data.mauin).change();
                    alert('Lưu thành công');
                });

                // Define what happens in case of error
                XHR.addEventListener('error', function (event) {
                    alert('Oops! Something went wrong.');
                });

                // Set up our request
                XHR.open('POST', 'Tool/TuyChinhChuanBiHang.aspx');

                // Add the required HTTP header for form data POST requests
                XHR.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

                // Finally, send our data.
                XHR.send(urlEncodedData);
            }
        }
    </script>
</div>

<%} %>