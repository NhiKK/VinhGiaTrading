jQuery.fn.dialogCenter = function() {
    this.css("position", "absolute");
    this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px");
    this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");
    return this;
};

//trangthai
jQuery.extend($.fn.fmatter, {
    trangthai: function(cellvalue, options, rowdata) {
        if (cellvalue == 'DAGUI') {
            return '<span title = "DAGUI">Đã Gửi</span>';
        } else if (cellvalue == 'DANHAN') {
            return '<span title = "DANHAN">Đã Xem</span>';
        } else if (cellvalue == 'DAHIEULUC') {
            return '<span title = "DAHIEULUC">Đã Nhận</span>';
        } else if (cellvalue == 'TUCHOI') {
            return '<span title = "TUCHOI">Từ chối</span>';
        } else if (cellvalue == 'XACNHAN') {
            return '<span title = "XACNHAN">Chờ gửi</span>';
        }
		else {
            return '<span title = "CHUAGUI">Chưa Gửi</span>';
        }
    }
});
// -- trangthai

//loai
jQuery.extend($.fn.fmatter, {
    loai: function(cellvalue, options, rowdata) {
        if (cellvalue == 'NCU') {
            return '<span title= "NCU">Thu Nhà Cung Ứng</span>';
        } else if (cellvalue == 'KH') {
            return '<span title = "KH">Thu Khách Hàng</span>';
        }  else
		{
			return '<span title = ""></span>';
		}
	}
});
// -- loai

// image
jQuery.extend($.fn.fmatter, {
    image: function(cellvalue, options, rowdata) {
        return '<img src="' + cellvalue + '" />';
    }
});

jQuery.extend($.fn.fmatter.image, {
    unformat: function(cellvalue, options, cell) {
        return $('img', cell).attr('src');
    }
});
// -- image

// image status
jQuery.extend($.fn.fmatter, {
    imagestatus: function(cellvalue, options, rowdata) {
		return '<img src="iconcollection/' + cellvalue + '.png" />';
    }
});

jQuery.extend($.fn.fmatter.imagestatus, {
    unformat: function(cellvalue, options, cell) {
        return $('img', cell).attr('src');
    }
});
// -- image status

// status
jQuery.extend($.fn.fmatter, {
    status: function(cellvalue, options, cell) {
    return '<div title="' + cellvalue + '" class="' + cellvalue.toLowerCase() + '"> </div>';
    }
});

jQuery.extend($.fn.fmatter.status, {
    unformat: function(cellvalue, options, cell) {
    return $('div', cell).attr('title');
    }
});
// -- status