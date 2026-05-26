; (function($) {
    /**
    * jqGrid English Translation
    * Tony Tomov tony@trirand.com
    * http://trirand.com/blog/ 
    * Dual licensed under the MIT and GPL licenses:
    * http://www.opensource.org/licenses/mit-license.php
    * http://www.gnu.org/licenses/gpl.html
    **/
    $.jgrid = $.jgrid || {};
    $.extend($.jgrid, {
        defaults: {
            recordtext: "Xem {0} - {1} của {2}",
            emptyrecords: "Không có dữ liệu",
            loadtext: "Đang tải...",
            pgtext: "Trang {0} của {1}"
        },
        search: {
            caption: "Đang tìm...",
            Find: "Tìm kiếm",
            Reset: "Làm lại",
            odata: ['như', 'không như', 'ít hơn', 'ít hơn hoặc bằng', 'lớn hơn', 'lớn hơn hoặc bằng', 'bắt đầu với', 'không bắt đầu với', 'nằm trong', 'không nằm trong', 'kết thúc với', 'không kết thúc với', 'có chứ', 'không chứa'],
            groupOps: [{ op: "AND", text: "all" }, { op: "OR", text: "any"}],
            matchText: " phù hợp với",
            rulesText: " quy tắc"
        },
        edit: {
            addCaption: "Thêm",
            editCaption: "Thay đổi",
            bSubmit: "Lưu",
            bCancel: "Thoát",
            bClose: "Đóng",
            saveData: "Dữ liệu đã được thay đổi! Bạn có muốn lưu không?",
            bYes: "Đồng ý",
            bNo: "Không",
            bExit: "Hủy",
            msg: {
                required: "Không được để trống",
                number: "Vui lòng nhập đúng kiểu số",
                minValue: "giá trị phải lớn hơn hoặc bằng ",
                maxValue: "giá trị phải nhỏ hơn hoặc bằng ",
                email: "không phải định dạng email",
                integer: "Vui lòng nhập đúng kiểu số nguyên",
                date: "Vui lòng nhập đúng kiểu ngày dd/MM/yyyy",
                url: "Không phải là URL. Tiền tố cần thiết ('http://' or 'https://')",
                nodefined: " không được định nghĩa!",
                novalue: " trả về giá trị rỗng!",
                customarray: "Hàm tùy chỉnh nên trả về mảng!",
                customfcheck: "Hàm tùy chỉnh nên có trong trường kiểm tra!"

            }
        },
        view: {
            caption: "Xem",
            bClose: "Đóng"
        },
        del: {
            caption: "Xóa",
            msg: "Xóa các dòng đã chọn?",
            bSubmit: "Xóa",
            bCancel: "Hủy"
        },
        nav: {
            edittext: "",
            edittitle: "Chỉnh sửa dòng chọn",
            addtext: "",
            addtitle: "Thêm mới",
            deltext: "",
            deltitle: "Xóa dòng đã chọn",
            searchtext: "",
            searchtitle: "Tìm kiếm",
            refreshtext: "",
            refreshtitle: "Tải lại grid",
            alertcap: "Cảnh báo",
            alerttext: "Vui lòng chọn dòng",
            viewtext: "",
            viewtitle: "Xem dòng đã chọn"
        },
        col: {
            caption: "chọn cột",
            bSubmit: "Đồng ý",
            bCancel: "Hủy"
        },
        errors: {
            errcap: "Lỗi",
            nourl: "URL không được thiết lập",
            norecords: "Không có record để xử lý",
            model: "Số colNames khác số colModel!"
        },
        formatter: {
            integer: { thousandsSeparator: " ", defaultValue: '0' },
            number: { decimalSeparator: ".", thousandsSeparator: " ", decimalPlaces: 2, defaultValue: '0.00' },
            currency: { decimalSeparator: ".", thousandsSeparator: " ", decimalPlaces: 2, prefix: "", suffix: "", defaultValue: '0.00' },
            date: {
                dayNames: [
				"CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy",
				"Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"
			],
                monthNames: [
				"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12",
				"Tháng 01", "Tháng 02", "Tháng 03", "Tháng 04", "Tháng 04", "Tháng 06", "Tháng 07", "Tháng 08", "Tháng 09", "Tháng 10", "Tháng 11", "Tháng 12"
			],
                AmPm: ["am", "pm", "AM", "PM"],
                S: function(j) { return j < 11 || j > 13 ? ['st', 'nd', 'rd', 'th'][Math.min((j - 1) % 10, 3)] : 'th' },
                srcformat: 'Y-m-d',
                newformat: 'd/m/Y',
                masks: {
                    ISO8601Long: "Y-m-d H:i:s",
                    ISO8601Short: "Y-m-d",
                    ShortDate: "n/j/Y",
                    LongDate: "l, F d, Y",
                    FullDateTime: "l, F d, Y g:i:s A",
                    MonthDay: "F d",
                    ShortTime: "g:i A",
                    LongTime: "g:i:s A",
                    SortableDateTime: "Y-m-d\\TH:i:s",
                    UniversalSortableDateTime: "Y-m-d H:i:sO",
                    YearMonth: "F, Y"
                },
                reformatAfterEdit: false
            },
            baseLinkUrl: '',
            showAction: '',
            target: '',
            checkbox: { disabled: true },
            idName: 'id'
        }
    });
})(jQuery);