<%@ Control Language="C#" AutoEventWireup="true" CodeFile="jqGrid.ascx.cs" Inherits="jqGrid" %>

<script>
    function lightboxFormat( cellvalue, options, rowObject ){
        return '<a rel="prettyPhoto" href="images/products/'+cellvalue+'"><img src="images/products/'+cellvalue+'"/></span></a>'
    }

    function disable_formatter(e, t, n) { let a = $(n).html(), i = $(a).prop("tagName"); return $(i, n).attr("title") }

    function imgCheckBox(cellvalue, options, rowObject) {
        if (cellvalue == 'True') {
            return `<img title="True" src="iconcollection/DGDGDQ.png">`;
        }
        else {
            return `<img title="False" src="iconcollection/SOANTHAO.png">`;
        }
    }
    
    function lightboxUnFormat( cellvalue, options, cell){
        return $('img', cell).attr('src');
    }
    
    function resizeSelectWidth($form)
    {
        var maxWidth = 0, newMaxWidth = 0, i,
        $selects = $form.find('tr.FormData > td.DataTD > select.FormElement'),
        cn = $selects.length;
        
        // calculate the max width of selects
        for (i = 0; i < cn; i += 1) {
            maxWidth = Math.max(maxWidth, $($selects[i]).width());
        }
        maxWidth += 2; // increase width to improve visibility
        
        // change the width of selects to the max width
        for (i = 0; i < cn; i += 1) {
            $($selects[i]).width(maxWidth);
            newMaxWidth = Math.max(maxWidth, $($selects[i]).width());
        }
    }
    
    <%= _validation %>
    $(function() {
         
        
        
        $("#<%=ID%>").jqGrid({
            <%= _caption == "" ? "" : "caption: '" + _caption + "',"%>
            url: '<%= _urlFileAction %>?action=0',
            editurl: '<%= _urlFileAction %>?action=1',
            <%= _postData == "" ? "" : "postData : {" + _postData + "}, " %>
            datatype: '<%= _dataType %>',
            autowidth: <%= _autoWidth %>,
            height: <%= _height %>,
            width: <%= _width %>, 
            <% if (!string.IsNullOrEmpty(_colNames))
    { %>
            colNames: <%= _colNames %>,
            <% } %>
            colModel: <%= _colModel %>,
            multiselect: <%= _multiSelect %>,
            //scroll: 1, // Virtual scrolling
            rownumbers: <%= _rowNumbers %>,
            scrollrows : true,
            rowNum: <%= _rowNum %>,
            rowList: <%= _rowList %>,
            viewrecords: true,
            altRows:true,
            ajaxSelectOptions: { cache: false },
            altclass:'ui-jqgrid-altrow', // custom in jqgrid css
            <%= _cellEdit == null ? "" : "cellEdit:" + _cellEdit + "," %> 
            //ondblClickRow: function(rowid) { $("#<%=ID%>").jqGrid('editGridRow', rowid, prmEdit) },
            sortname: "<%= _sortName %>",
            sortorder: "<%= _sortOrder %>",
            <%= _gridBeforeRequest == "" ? "" : "beforeRequest: function () { " + _gridBeforeRequest + " }," %> 
            <%= _gridComplete == "" ? "" : "gridComplete: function () { " + _gridComplete + " }," %> 
            <%= _loadError == "" ? "" : "loadError: function(xhr,st,err) { " + _loadError + " }," %>
            <%= _onSelectRow == "" ? "" : "onSelectRow: " + _onSelectRow + "," %>
            <%= _ondblClickRow == "" ? "" : "ondblClickRow: " + _ondblClickRow + "," %>

            pager: $('#<%=ID%>-pager')
        });
        //.gridResize()
        $("#<%=ID%>").jqGrid('navGrid', '#<%=ID%>-pager'
                          , { del: <%= _overrideDelete != ""? "false" : _showDel %>
                              , add: <%= _overrideAdd != ""? "false" : _showAdd %>
                              , edit: <%= _overrideEdit != ""? "false" : _showEdit %>
                              , search: <%= _showSearch %> 
                              , view : <%= _showView %> 
                              , refresh:<%= _showRefresh %> 
                              <%= _afterRefresh == ""? "" : ",afterRefresh :" + _afterRefresh %>
                              }

                            ,{  // edit
                                <%= _editFormOptions != "" ? _editFormOptions : "" %> 
                            }
                            ,{ // add
                                <%= _addFormOptions != "" ? _addFormOptions : "" %> 
                            }
                            ,{ // del
                                <%= _delFormOptions != "" ? _delFormOptions : "" %> 
                            }
                            ,{ // search
                            
                            }
                            ,{ // view
                                <%= _viewFormOptions != "" ? _viewFormOptions : "" %> 
                            }
                         );
        
        //        $("#<%=ID%>").jqGrid('navGrid','#<%=ID%>-toolbar',{edit:false,add:false,del:false});
        
        <% if (!_filterToolbar.Equals(""))
    { %>
        $("#<%=ID%>").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });
        <% }
    else { }%>
        
        <% if (!_overrideAdd.Equals(""))
    { %>
        /* _overrideAdd */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Thêm"
            , buttonicon: "ui-icon-plus"
            , position:"last"
            , onClickButton : <%= _overrideAdd %>
            });
        /* - _overrideAdd */
        <% }
    else { }%>
        
        <% if (!_overrideEdit.Equals(""))
    { %>
        /* _overrideEdit */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Thay đổi"
            , buttonicon: "ui-icon-pencil"
            , position:"last"
            , onClickButton : <%= _overrideEdit %>
            });
        /* - _overrideEdit */
        <% }
    else { }%>
        
        <% if (!_overrideDelete.Equals(""))
    { %>
        /* _overrideDelete */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Xóa"
            , buttonicon: "ui-icon-trash"
            , position:"last"
            , onClickButton : <%= _overrideDelete %>
            });
        /* - _overrideDelete */
        <% }
    else { }%>
		
        $("#<%=ID%>").jqGrid('navButtonAdd', '#<%=ID%>-pager',
		{ caption: "Columns",
		    title: "Reorder Columns",
		    onClickButton : function (){
		        $("#<%=ID%>").jqGrid('columnChooser',
				{
				    "done":function(){
				        var o = $(this).parent().parent().parent().parent().parent().parent(); 
				        $(this).setGridWidth($(o).width());
				    }
				});
		    }
		});
		
		
        
        <% if (!_btnPrint.Equals(""))
    { %>
        /* Print */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Trích Xuất Dữ Liệu"
            , buttonicon: "ui-icon-print"
            , onClickButton : <%= _btnPrint %>
            });
        /* - Print */
        <% }
    else { }%>
        
         <% if (!_funcChangeStatus.Equals(""))
    { %>
        /* gui mail */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Thay đổi trạng thái"
            , buttonicon: "ui-icon-transferthick-e-w"
            , onClickButton : <%= _funcChangeStatus %>
            });
        /* - gui mail */
        <% }
    else { }%>
        
        
        <% if (!_funcImportPicture.Equals(""))
    { %>
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
           , title:"Import Picture"
           , buttonicon: "icon-import-pic"
           , onClickButton : <%= _funcImportPicture %>
           });
        <% }
    else { }%>
        
        
        
        <% if (!_funcQoByProducts.Equals(""))
    { %>
        /* gui mail */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo quotation từ D/S sản phẩm"
            , buttonicon: "ui-icon-circle-plus"
            , onClickButton : <%= _funcQoByProducts %>
            });
        /* - gui mail */
        <% }
    else { }%>
        
        
        <% if (!_funcSendMail.Equals(""))
    { %>
        /* gui mail */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Gửi mail"
            , buttonicon: "ui-icon-mail-closed"
            , onClickButton : <%= _funcSendMail %>
            });
        /* - gui mail */
        <% }
    else { }%>
         
        
        <% if (!_funcActive.Equals(""))
    { %>
        /* Hieu luc Quotation */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Hiệu lực"
            , buttonicon: "icon-active-qo"
            , onClickButton : <%= _funcActive %>
            });
        /* - Hieu luc Quotation */
        <% }
    else { }%>
        
        <% if (!_funcOpenClose.Equals(""))
    { %>
        /* Dong Mo Ky */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Đóng mở kỳ"
            , buttonicon: "icon-active-qo"
            , onClickButton : <%= _funcOpenClose %>
            });
        /* - Dong Mo Ky */
        <% }
    else { }%>
		
		<% if (!_funcActiveNhapKho.Equals(""))
    { %>
        /* Hieu luc Nhap Kho */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title: "<%= _funcActiveNhapKho.Contains("XuatKho") ? "Hiệu lực xuất kho" : "Hiệu lực nhập kho" %>"
            , buttonicon: "icon-active-qo"
            , onClickButton : <%= _funcActiveNhapKho %>
            });
        /* - Hieu luc Nhap Kho */
        <% }
    else { }%>
        
		
		<% if (!_funcDeactiveNhapKho.Equals(""))
    { %>
        /* Soan thao Nhap Kho */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title: '<%= _funcDeactiveNhapKho.Contains("XuatKho") ? "Chuyển trạng thái \"Soạn thảo\" phiếu xuất kho" : "Chuyển trạng thái \"Soạn thảo\" phiếu nhập kho" %>'
            , buttonicon: "icon-deactive-qo"
            , onClickButton : <%= _funcDeactiveNhapKho %>
            });
        /* - Soan thao Nhap Kho */
        <% }
    else { }%>
        
		<% if (!_funcCopyDonHang.Equals(""))
    { %>
        /* Copy don hang */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Copy đơn hàng"
            , buttonicon: "icon-view-translate"
            , onClickButton : <%= _funcCopyDonHang %>
            });
        /* - Copy don hang */
        <% }
    else { }%>
		
        <% if (!_funcViewCapacity.Equals(""))
    { %>
        /* Xem nang luc */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Xem Năng Lực"
            , buttonicon: "icon-view-capacity"
            , onClickButton : <%= _funcViewCapacity %>
            });
        /* - Xem nang luc */
        <% }
    else { }%>
        
        <% if (!_funcPhanBoCocInv.Equals(""))
    { %>
        /* Xem nang luc */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Phân bổ cọc cho Invoice"
            , buttonicon: "icon-view-capacity"
            , onClickButton : <%= _funcPhanBoCocInv %>
            });
        /* - Xem nang luc */
        <% }
    else { }%>
        
        
        <% if (!_funcActiveQO.Equals(""))
    { %>
        /* Hieu luc Quotation */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Hiệu lực Quotation"
            , buttonicon: "icon-active-qo"
            , onClickButton : <%= _funcActiveQO %>
            });
        /* - Hieu luc Quotation */
        <% }
    else { }%>
        
        <% if (!_funcActivePI.Equals(""))
    { %>
        /* Hieu luc PI */
        if (doiNCU == 'True') {
            $("#<%=ID%>").jqGrid('navButtonAdd', '#<%=ID%>-pager', {
                caption: ""
                , title: "Hiệu lực DSĐH"
                , buttonicon: "icon-active-qo"
                , onClickButton: <%= _funcActivePI %>
            });
        }
        /* - Hieu luc PI */
        <% }
    else { }%>
        
        
        <% if (!_funcActivePhieuXuat.Equals(""))
    { %>
        /* Hieu luc Phieu Xuat */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Hiệu lực phiếu xuất"
            , buttonicon: "icon-active-qo"
            , onClickButton : <%= _funcActivePhieuXuat %>
            });
        /* - Hieu luc Phieu Xuat */
        <% }
    else { }%>
        
        
        <% if (!_funcActivePO.Equals(""))
    { %>
        /* Hieu luc PO */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Hiệu lực PO"
            , buttonicon: "icon-active-qo"
            , onClickButton : <%= _funcActivePO %>
            });
        /* - Hieu luc PO */
        <% }
    else { }%>
        
        
        <% if (!_funcPO.Equals(""))
    { %>
        /* Tao PO Tu Quotation */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo P/O Từ Quotation"
            , buttonicon: "icon-po"
            , onClickButton : <%= _funcPO %>
            });
        /* - Tao PO Tu Quotation */
        <% }
    else { }%>
        
        <% if (!_funcPI.Equals(""))
    { %>
        /* Tao PI Tu PO */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo ĐĐH"
            , buttonicon: "icon-pi"
            , onClickButton : <%= _funcPI %>
            });
        /* - Tao PI Tu PO */
        <% }
    else { }%>
        
        
        <% if (!_funcTaoTuanNangLuc.Equals(""))
    { %>
        /* Xem nang luc */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo Tuần Năng Lực"
            , buttonicon: "icon-view-capacity"
            , onClickButton : <%= _funcTaoTuanNangLuc %>
            });
        /* - Xem nang luc */
        <% }
    else { }%>
        
        
        <% if (!_funcNangLuc.Equals(""))
    { %>
        /* Tao Chi Tiet Nang Luc */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo Chi Tiết Năng Lực"
            , buttonicon: "icon-lightning"
            , onClickButton : <%= _funcNangLuc %>
            });
        /* - Tao Chi Tiet Nang Luc */
        <% }
    else { }%>
        
        
         
        <% if (!_funcTaoPhieuNhapXuat.Equals(""))
    { %>
        /* Tạo Phiếu Nhập Kho */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo Phiếu Nhập Kho"
            , buttonicon: "icon-mod-qo"
            , onClickButton : <%= _funcTaoPhieuNhapXuat %>
            });
        /* - Tạo Phiếu Nhập Kho */
        <% }
    else { }%>
         
         
        <% if (!_funcTaoPhieuXuatKho.Equals(""))
    { %>
        /* Tạo Phiếu Xuất Kho */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo Phiếu Xuất Kho"
            , buttonicon: "icon-create-xk"
            , onClickButton : <%= _funcTaoPhieuXuatKho %>
            });
        /* - Tạo Phiếu Xuất Kho */
        <% }
    else { }%>
         
         <% if (!_funcTaoPhieuXuatKhoN.Equals(""))
    { %>
        /* Tạo Phiếu Xuất Kho Nhiều Phiếu Nhập */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo Phiếu Xuất Kho Nhiều Phiếu Nhập"
            , buttonicon: "icon-create-xkn"
            , onClickButton : <%= _funcTaoPhieuXuatKhoN %>
            });
        /* - Tạo Phiếu Xuất Kho Nhiều Phiếu Nhập */
        <% }
    else { }%>
         
         
        <% if (!_funcChiaTachPOTaoPI.Equals(""))
    { %>
        /* Chia tách PO tạo PI */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo đơn đặt hàng"
            , buttonicon: "icon-mod-qo"
            , onClickButton : <%= _funcChiaTachPOTaoPI %>
            });
        /* - Chia tách PO tạo PI */
        <% }
    else { }%>
         
         
         <% if (!_funcTaoPhieuXuat.Equals(""))
    { %>
        /* Tạo phiếu xuất */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo phiếu xuất"
            , buttonicon: "icon-mod-qo"
            , onClickButton : <%= _funcTaoPhieuXuat %>
            });
        /* - Tạo phiếu xuất */
        <% }
    else { }%>
        
        
        
        
        
        <% if (!_funcTaoKy.Equals(""))
    { %>
        /* Tao Ky Cho Nam */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo Kỳ Cho Năm"
            , buttonicon: "icon-calender"
            , onClickButton : <%= _funcTaoKy %>
            });
        /* - Tao Ky Cho Nam */
        <% }
    else { }%>
         
        
        
        <% if (!_funcTaoPKLCungPO.Equals(""))
    { %>
        /* Tao Packing List Invoice Cùng PO */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tao Packing List Invoice Cùng PO"
            , buttonicon: "icon-pkl-po"
            , onClickButton : <%= _funcTaoPKLCungPO %>
            });
        /* - Tao Packing List Invoice Cùng Khách Hàng */
        <% }
    else { }%>   
        
        
         
        <% if (!_funcTaoPKLCungKH.Equals(""))
    { %>
        /* Tao Packing List Invoice Cùng PO */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Tạo Packing List Invoice Cùng Khách Hàng"
            , buttonicon: "icon-pkl-partner"
            , onClickButton : <%= _funcTaoPKLCungKH %>
            });
        /* - Tao Packing List Invoice Cùng Khách Hàng */
        <% }
    else { }%>  
         
         
        <% if (!_funcCopyPhienBan.Equals(""))
    { %>
        /* Copy phien ban gia */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Copy phiên bản giá"
            , buttonicon: "icon-copy"
            , onClickButton : <%= _funcCopyPhienBan %>
            });
        /* - Copy phien ban gia */
        <% }
    else { }%>  
         
         
        <% if (!_funcKetThuc.Equals(""))
    { %>
        /* Kết Thúc */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Kết Thúc"
            , buttonicon: "icon-stop"
            , onClickButton : <%= _funcKetThuc %>
            });
        /* - Kết Thúc */
        <% }
    else { }%>  
		 
		 <% if (!_autofuncKetThuc.Equals(""))
    { %>
        /* Kết Thúc */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Auto Kết Thúc"
            , buttonicon: "icon-stop"
            , onClickButton : <%= _autofuncKetThuc %>
            });
        /* - Kết Thúc */
        <% }
    else { }%>  
         
        <% if (!_funcXemNangLuc.Equals(""))
    { %>
        /* Xem Năng Lực */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Xem Năng Lực"
            , buttonicon: "ui-icon-note"
            , onClickButton : <%= _funcXemNangLuc %>
            });
        /* - Xem Năng Lực */
        <% }
    else { }%>
		 
		 <% if (!_GuiDonHang.Equals(""))
    { %>
        /* Gửi đơn đặt hàng */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Gửi đơn đặt hàng"
            , buttonicon: "icon-f2"
            , onClickButton : <%= _GuiDonHang %>
            });
        /* - Gửi đơn đặt hàng */
        <% }
    else { }%>
			 
			 <% if (!_InPhieuChi.Equals(""))
    { %>
        /* Gửi đơn đặt hàng */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"In phiếu chi"
            , buttonicon: "ui-icon ui-icon-print"
            , onClickButton : <%= _InPhieuChi %>
            });
        /* - Gửi đơn đặt hàng */
        <% }
    else { }%>

        <% if (!_funcReturnDonHang.Equals(""))
    { %>
        /* Tra don hang ve soan thao */
        if (vnnIsAdmin == 'True') {
            $("#<%=ID%>").jqGrid('navButtonAdd', '#<%=ID%>-pager', {
                caption: ""
                , title: "Trả về soạn thảo"
                , buttonicon: "icon-return"
                , onClickButton: <%= _funcReturnDonHang %>
            });
        }
        /* - Tra don hang ve soan thao */
        <% }
    else { }%>

         <% if (!_funcReturnPI.Equals(""))
    { %>
        /* Tra pi ve soan thao */
        if (vnnIsAdmin == 'True') {
            $("#<%=ID%>").jqGrid('navButtonAdd', '#<%=ID%>-pager', {
                caption: ""
                , title: "Trả về soạn thảo"
                , buttonicon: "icon-return"
                , onClickButton: <%= _funcReturnPI %>
            });
        }
        /* - Tra pi ve soan thao */
        <% }
    else { }%>

        <% if (!_funcConfirmPI.Equals(""))
    { %>
        /* Tra pi ve soan thao */
        //if (vnnIsAdmin == 'True') {
            $("#<%=ID%>").jqGrid('navButtonAdd', '#<%=ID%>-pager', {
                caption: ""
                , title: "Xác nhận PI"
                , buttonicon: "icon-active-qo"
                , onClickButton: <%= _funcConfirmPI %>
            });
        //}
        /* - Tra pi ve soan thao */
        <% }
    else { }%>

        <% if (!_funcviewCapacityRaBo.Equals(""))
    { %>
        /* Xem nang luc */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Xem Năng Lực RB"
            , buttonicon: "icon-view-capacity"
            , onClickButton : <%= _funcviewCapacityRaBo %>
            });
        /* - Xem nang luc */
        <% }
    else { }%>

            <% if (!_funcCoppyChiTietNangLuc.Equals(""))
    { %>
        /* Xem nang luc */
        $("#<%=ID%>").jqGrid('navButtonAdd','#<%=ID%>-pager',{
            caption:""
            , title:"Coppy chi tiết năng lực"
            , buttonicon: "icon-view-capacity"
            , onClickButton : <%= _funcCoppyChiTietNangLuc %>
            });
        /* - Xem nang luc */
        <% }
    else { }%>

        <% if (!_funcReturnPKL.Equals(""))
    { %>
        if (vnnIsAdmin == 'True') {
            $("#<%=ID%>").jqGrid('navButtonAdd', '#<%=ID%>-pager', {
                caption: ""
                , title: "Trả về soạn thảo"
                , buttonicon: "icon-return"
                , onClickButton: <%= _funcReturnPKL %>
            });
        }
        <% }
    else { }%>
    });
</script>

<table class="ui-layout-center" id="<%=ID%>"></table>
<div id="<%=ID%>-pager"></div>
<div id="<%=ID%>-toolbar"></div>
