<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TuyChinhGiuCho.aspx.cs" Inherits="Tool_TuyChinhGiuCho" %>
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

    #LuuChinhSuaTCGC {
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
            <span style="font-weight: bold; font-size: 160%;">Tùy chỉnh giữ chỗ</span>
        </legend>
         <table>
         <tr>
            <td class="tdRight tdTop">
                <label>Chọn mẫu: </label>
            </td>
             <td style="width:10px"></td>
            <td>
                <select id="mauTCGC" class="selectRptTCGC">
                    <%=loadOptions() %>
                </select>
            </td>
        </tr>
        
        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Số ngày trì hoãn: </label>
            </td>
            <td></td>
            <td>
                <input onKeyDown="return false" min="0" max="23" style="width:40px" id="songayTCGC" type="number"/>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

       <tr>
            <td style="text-align:center" colspan="3">
                <input type="button" value="Lưu chỉnh sửa" id="LuuChinhSuaTCGC" />
            </td>
        </tr>
    </table>
     </fieldset>
    <script type="text/javascript">
        let $mauTCGC = $('#mauTCGC');
        let $songayTCGC = $('#songayTCGC');
        
        $mauTCGC.change(function () {
            let optionSel = $mauTCGC[0].options[$mauTCGC[0].selectedIndex];

            let mau = optionSel.getAttribute('mau');
            let songay = optionSel.getAttribute('songay');

            $songayTCGC.val(songay);
        });

        $mauTCGC.change();

        document.getElementById('LuuChinhSuaTCGC').onclick = function () {
            if (!$mauTCGC.val()) {
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
                    "mau": $mauTCGC.val(),
                    "songay": $songayTCGC.val(),
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
                    $mauTCGC.html(data);
                    $mauTCGC.val(data.mauin).change();
                    alert('Lưu thành công');
                });

                // Define what happens in case of error
                XHR.addEventListener('error', function (event) {
                    alert('Oops! Something went wrong.');
                });

                // Set up our request
                XHR.open('POST', 'Tool/TuyChinhGiuCho.aspx');

                // Add the required HTTP header for form data POST requests
                XHR.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

                // Finally, send our data.
                XHR.send(urlEncodedData);
            }
        }
    </script>
</div>

<%} %>