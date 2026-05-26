<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TuyChinhGuiMail.aspx.cs" Inherits="Tool_TuyChinhGuiMail" %>
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
            <span style="font-weight: bold; font-size: 160%;">Tùy chỉnh gửi mail tự động</span>
        </legend>
         <table>
         <tr>
            <td class="tdRight tdTop">
                <label>Chọn mẫu in: </label>
            </td>
             <td></td>
            <td>
                <select id="mauInGulMail" class="selectRptTCGM">
                    <%=loadOptions() %>
                </select>
            </td>
        </tr>
        
        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Lặp lại: </label>
            </td>
            <td></td>
            <td>
                <select id="lapLaiGulMail" class="selectRptTCGM">
                    <option value="MD">mỗi ngày</option>
                    <option value="MW">mỗi tuần</option>
                    <option value="MM">mỗi tháng</option>
                </select>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Ngày hiệu lực: </label>
            </td>
            <td><div class="spaceTr"></div></td>
            <td>
                <input style="width:90px" id="ngayHLGulMail" type="text"/>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Giờ hiệu lực: </label>
            </td>
            <td><div class="spaceTr"></div></td>
            <td>
                <input onKeyDown="return false" min="0" max="23" style="width:40px" id="gioHLGulMail" type="number"/>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Ngày đã gửi: </label>
            </td>
            <td><div class="spaceTr"></div></td>
            <td>
                <input style="width:90px" id="ngaySKGulMail" type="text"/>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Vai trò nhận: </label>
            </td>
            <td><div class="spaceTr"></div></td>
            <td>
                <select id="vaiTroGuiMail" class="selectRptTCGM">
                    <%=loadVaitros() %>
                </select>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Tiêu đề: </label>
            </td>
            <td></td>
            <td>
                <input id="tieuDeGuiMail" type="text"/>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td class="tdRight tdTop">
                <label>Nội dung: </label>
            </td>
            <td></td>
            <td>
                <textarea id="noiDungGuiMail" rows="5"></textarea>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

       <tr>
            <td style="text-align:center" colspan="3">
                <input type="button" value="Lưu chỉnh sửa" id="LuuChinhSua" />
            </td>
        </tr>
    </table>
     </fieldset>
    <script type="text/javascript">
        let $mauinGM = $('#mauInGulMail');
        let $laplaiGM = $('#lapLaiGulMail');
        let $ngayhlGM = $('#ngayHLGulMail');
        let $giohlGM = $('#gioHLGulMail');
        let $tieudeGM = $('#tieuDeGuiMail');
        let $noidungGM = $('#noiDungGuiMail');
        let $vaitroGM = $('#vaiTroGuiMail');
        let $ngaySKGM = $('#ngaySKGulMail');

        $ngayhlGM.datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });

        $ngaySKGM.datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });

        $mauinGM.change(function () {
            let optionSel = $mauinGM[0].options[$mauinGM[0].selectedIndex];

            let mauin = optionSel.getAttribute('mauin');
            let laplai = optionSel.getAttribute('laplai');
            let ngayhl = optionSel.getAttribute('ngayhl');
            let giohl = optionSel.getAttribute('giohl');
            let tieude = optionSel.getAttribute('tieude');
            let noidung = optionSel.getAttribute('noidung');
            let vaitro = optionSel.getAttribute('vaitro');
            let ngaysk = optionSel.getAttribute('ngaysk');

            $laplaiGM.val(laplai);
            $ngayhlGM.val(ngayhl);
            $tieudeGM.val(tieude);
            $noidungGM.val(noidung);
            $vaitroGM.val(vaitro);
            $ngaySKGM.val(ngaysk);
            $giohlGM.val(giohl);
        });

        $mauinGM.change();

        document.getElementById('LuuChinhSua').onclick = function () {
            if (!$mauinGM.val()) {
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
                    "mauin": $mauinGM.val(),
                    "laplai": $laplaiGM.val(),
                    "ngayhl": $ngayhlGM.val(),
                    "tieude": $tieudeGM.val(),
                    "noidung": $noidungGM.val(),
                    "vaitro": $vaitroGM.val(),
                    "ngaysk": $ngaySKGM.val(),
                    "giohl": $giohlGM.val(),
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
                    $mauinGM.html(data);
                    $mauinGM.val(data.mauin).change();
                    alert('Lưu thành công');
                });

                // Define what happens in case of error
                XHR.addEventListener('error', function (event) {
                    alert('Oops! Something went wrong.');
                });

                // Set up our request
                XHR.open('POST', 'Tool/TuyChinhGuiMail.aspx');

                // Add the required HTTP header for form data POST requests
                XHR.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

                // Finally, send our data.
                XHR.send(urlEncodedData);
            }
        }
    </script>
</div>

<%} %>