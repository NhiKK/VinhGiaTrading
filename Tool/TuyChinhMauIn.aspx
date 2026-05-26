<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TuyChinhMauIn.aspx.cs" Inherits="Tool_TuyChinhMauIn" %>
<% if(!send & !sendFile) { %>
<style type="text/css">
    .frmMauIn table {
        max-width: 500px;
        border-collapse: collapse;
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

    .frmMauIn input, .frmMauIn select {
        width: 99%;
    }
    .frmMauIn fieldset {
        padding: 15px;
        width: 100%;
    }

    .frmMauIn .spaceTr {
        width: 20px;
        height: 15px;
    }

    #ktcExcel, #ktcPDF {
        width: 50px;
    }

    #LuuChinhSua, #LuuHinhAnh, #selectRpt {
        max-width: 120px;
    }

    #selectRpt2 {
        display:none;
    }
</style>
<div class="frmMauIn">
    <h1>Tùy chỉnh mẫu in đơn hàng, DSDH</h1>

    <table>
         <tr>
            <td>
                <label>Chọn mẫu in: </label>
                <%=optionsMauIn %>
                <select id="selectRpt2"></select>
            </td>
        </tr>
        
        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td>
                <label>Kích thước chữ Excel: </label>
                <input id="ktcExcel" type="number" />
            </td>
            <td><div class="spaceTr"></div></td>
            <td>
                <label>Kích thước chữ PDF: </label>
                <input id="ktcPDF" type="number" />
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td colspan="3">
                <fieldset>
                    <legend><b>Định dạng số thập phân</b></legend>
                    <div class="selectNCC">
                        <label>Chọn NCC</label><br>
                        <select id="selNCCStp"></select>
                    </div>
                    <div class="selectNCC" style="height:15px;"></div>
                    <div style="display:table">
                        <div style="display:table-row">
                            <div style="display:table-cell">
                                <label>Số lượng</label><br>
                                <input id="fmtSoLuong" type="text" />
                            </div>
                            <div style="display:table-cell;width:20px"></div>
                            <div style="display:table-cell">
                                <label>CBM</label><br>
                                <input id="fmtCBM" type="text" />
                            </div>
                            <div style="display:table-cell;width:20px"></div>
                            <div style="display:table-cell">
                                <label>Discount</label><br>
                                <input id="fmtDiscount" type="text" />
                            </div>
                            <div style="display:table-cell;width:20px"></div>
                            <div style="display:table-cell">
                                <label>Đơn giá, Thành Tiền</label><br>
                                <input id="fmtAmount" type="text" />
                            </div>
                        </div>
                    </div>
                </fieldset>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

       <tr>
            <td style="text-align:center" colspan="3">
                <input type="button" value="Lưu chỉnh sửa" id="LuuChinhSua" />
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td colspan="3">
                <fieldset>
                    <legend><b>Hình con dấu và chữ ký</b></legend>
                    <input type="file" id="luuHinhAnhFile" style="display:none" accept=".jpg,.png" onchange="let thisSav = this; $(thisSav).val() == '' ? null : (tuyChinhMauIn.readDataWhenChangeFile(this, function(rs) { !rs.ok ? alert(rs.mess) : ($(thisSav).next().attr('src', rs.data), $(thisSav).parent().prev().val(JSON.stringify(rs))) }));"/>
                    <img src="images/more/ckcdct.jpg?v=<%=Guid.NewGuid().ToString()%>" onerror="this.src='images/more/notfound.jpg'" onclick="$(this).prev().click();" width="180" height="190" style="padding: 15px; cursor:pointer;" />
                </fieldset>
            </td>
        </tr>

        <tr><td><div class="spaceTr"></div></td></tr>

        <tr>
            <td style="text-align:center" colspan="3">
                <input type="button" value="Lưu hình ảnh" id="LuuHinhAnh" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var tuyChinhMauIn = {
            readDataWhenChangeFile: function (input, callback) {
                let imgJs = {
                    ok: false,
                    data: '',
                    name: '',
                    mess: '',
                    maxSize: 0,
                    size: 0
                };

                let fileSel, accept;
                if (input.files && input.files[0]) {
                    fileSel = input.files[0];
                    accept = input.getAttribute('accept');
                    maxSize = input.getAttribute('maxSize');
                }
                else {
                    fileSel = input;
                    accept = fileSel.accept;
                    maxSize = fileSel.size;
                }

                maxSize = maxSize ? Number(maxSize) : 999999999999;

                if (fileSel.size > maxSize) {
                    alert(`Tập tin quá lớn (tối đa ${formatBytes(maxSize, 2)})`);
                    return;
                }

                let reader = new FileReader();

                reader.onload = function (e) {
                    let indexMimeType = fileSel.name.lastIndexOf(".");
                    let mimeType = fileSel.name.substring(indexMimeType).toLowerCase();
                    if (accept.split(',').lastIndexOf(mimeType) <= -1) {
                        imgJs.ok = false;
                        imgJs.mess = 'Chỉ chấp nhận ảnh có các định dạng sau: ' + accept.replace(/,/g, ', ');
                    }
                    else {
                        imgJs.ok = true;
                        imgJs.data = e.target.result;
                        imgJs.name = fileSel.name;
                        imgJs.size = fileSel.size;
                        imgJs.maxSize = maxSize;
                    }
                    callback(imgJs);
                }

                reader.readAsDataURL(fileSel);
            }
        }

        let selectRpt = document.getElementById('selectRpt');
        selectRpt.onchange = function (e) {
            let optionSel = selectRpt.options[selectRpt.selectedIndex];
            let jsonKTC = JSON.parse(optionSel.getAttribute('ktc'))[0];
            let selectNCC = document.getElementsByClassName('selectNCC');
            document.getElementById('ktcExcel').value = jsonKTC.excel;
            document.getElementById('ktcPDF').value = jsonKTC.pdf;

            let jsonSTP = JSON.parse(optionSel.getAttribute('stps'))[0];
            let jsonDTKDs = jsonSTP.dtkd;
            let selNCCStp = document.getElementById('selNCCStp');
            selNCCStp.innerHTML = '';
            if (jsonDTKDs) {
                Array.prototype.forEach.call(selectNCC, function (item) { item.style.display = 'block'; });
                let node = document.createElement("option");
                selNCCStp.appendChild(node);
                for (let i = 0; i < jsonDTKDs.length; i++)
                {
                    let row = jsonDTKDs[i];
                    let node = document.createElement("option");
                    node.value = row.name;
                    node.text = row.name;
                    node.setAttribute("cbm", row.cbm ? row.cbm : jsonSTP.cbm);
                    node.setAttribute("soluong", row.soluong ? row.soluong : jsonSTP.soluong);
                    node.setAttribute("discount", row.discount ? row.discount : jsonSTP.discount);
                    node.setAttribute("amount", row.amount ? row.amount : jsonSTP.amount);
                    selNCCStp.appendChild(node);
                }

                selNCCStp.onchange = function () {
                    let optionSel = selNCCStp.options[selNCCStp.selectedIndex];
                    document.getElementById('fmtSoLuong').value = optionSel.getAttribute("soluong");
                    document.getElementById('fmtCBM').value = optionSel.getAttribute("cbm");
                    document.getElementById('fmtDiscount').value = optionSel.getAttribute("discount");
                    document.getElementById('fmtAmount').value = optionSel.getAttribute("amount");
                };

                selNCCStp.onchange();
            }
            else {
                Array.prototype.forEach.call(selectNCC, function (item) { item.style.display = 'none'; });
                document.getElementById('fmtSoLuong').value = jsonSTP.soluong;
                document.getElementById('fmtCBM').value = jsonSTP.cbm;
                document.getElementById('fmtDiscount').value = jsonSTP.discount;
                document.getElementById('fmtAmount').value = jsonSTP.amount;
            }
        };

        document.getElementById('LuuChinhSua').onclick = function () {
            if (!document.getElementById('selectRpt').value) {
                alert('Phải chọn mẫu in');
                return;
            }

            if (confirm('Thực hiện lưu chỉnh sửa của bạn???'))
            {
                const XHR = new XMLHttpRequest();

                let urlEncodedData = "",
                    urlEncodedDataPairs = [],
                    name;

                // Turn the data object into an array of URL-encoded key/value pairs.
                let dtkd = document.getElementById('selNCCStp').value;
                let stps = {
                    "soluong": document.getElementById('fmtSoLuong').value,
                    "cbm": document.getElementById('fmtCBM').value,
                    "discount": document.getElementById('fmtDiscount').value,
                    "amount": document.getElementById('fmtAmount').value
                };

                let data =
                {
                    "fontSize": JSON.stringify([
                        {
                            "excel": document.getElementById('ktcExcel').value,
                            "pdf": document.getElementById('ktcPDF').value
                        }
                    ]),
                    "name": document.getElementById("selectRpt").value,
                    "dtkd": dtkd,
                    "soThapPhan": JSON.stringify(stps),
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
                    //console.log(event.currentTarget.response);
                    let selectRpt = document.getElementById('selectRpt');
                    let selectRpt2 = document.getElementById('selectRpt2');
                    selectRpt2.innerHTML = event.currentTarget.response;

                    let options = selectRpt.options;
                    for (let i = 0; i < options.length; i++)
                    {
                        let option = options[i];
                        let option2 = selectRpt2.options[i];

                        option.setAttribute("ktc", option2.getAttribute("ktc"));
                        option.setAttribute("stps", option2.getAttribute("stps"));
                    }

                    let valSel = document.getElementById('selNCCStp').value;
                    selectRpt.onchange();
                    document.getElementById('selNCCStp').value = valSel;
                    document.getElementById('selNCCStp').onchange();

                    alert('Lưu thành công');
                });

                // Define what happens in case of error
                XHR.addEventListener('error', function (event) {
                    alert('Oops! Something went wrong.');
                });

                // Set up our request
                XHR.open('POST', 'Tool/TuyChinhMauIn.aspx');

                // Add the required HTTP header for form data POST requests
                XHR.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

                // Finally, send our data.
                XHR.send(urlEncodedData);
            }
        }


        document.getElementById('LuuHinhAnh').onclick = function () {
            let btnSav = $(this);
            if (!document.getElementById('luuHinhAnhFile').value) {
                alert('Phải chọn 1 hình ảnh');
                return;
            }

            if (confirm('Thực hiện lưu hình ảnh của bạn???'))
            {
                let dataF = $('#luuHinhAnhFile').next()[0].src;
               
                if (dataF.startsWith('data:image/jpeg;base64')) {
                    btnSav.prop('disabled', true);
                    $.post('Tool/TuyChinhMauIn.aspx', {
                        file: $('#luuHinhAnhFile').next()[0].src,
                        sendFile: "true"
                    }, function (result) {
                        if (result != '')
                            alert(result);
                        else {
                            $('#luuHinhAnhFile').next()[0].src = 'images/more/ckcdct.jpg?v=' + uuidv4();
                        }
                        btnSav.prop('disabled', false);
                        document.getElementById('luuHinhAnhFile').value = '';
                    })
                        .fail(function () {
                            btnSav.prop('disabled', false);
                        });
                }
                else {
                    alert('Hãy chọn 1 ảnh');
                }
            }
        }
    </script>
</div>

<%} %>