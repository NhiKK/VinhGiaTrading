<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-taophieuxuat-nhieuphieunhat.aspx.cs" Inherits="inc_taophieuxuat_nhieuphieunhat" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    function cr_out_from_ins() {
        var data = new Object();
        var ids = $('#grid_ds_nhapkho_taopx').getGridParam('selarrrow');
        if (ids != "") {
            data["arrayPhieuXuat"] = ids.join(", ");
			$('#grid_ds_nhapkho_taopx-pager_left .ui-icon.icon-create-xkn').hide();
            $.ajax({
                url: "Controllers/TaoPXuatTuNPNhapController.ashx",
                type: "POST",
                data: { "p": data, "p_s": ids.toString() },
                error: function(rs) {
					$('#grid_ds_nhapkho_taopx-pager_left .ui-icon.icon-create-xkn').show();
                    Popup("Error", 450, 300, rs.responseText);
                },
                success: function(rs) {
					$('#grid_ds_nhapkho_taopx-pager_left .ui-icon.icon-create-xkn').show();
                    alert(rs);
                }
            });
        } else {
            alert("Hãy chọn các phiếu nhập để tạo phiếu xuất!");
        }
    }
</script>
<uc1:jqGrid  ID="grid_ds_nhapkho_taopx"
        Caption="Phiếu Nhập Kho" 
        SortName="ngay_phieu" 
        UrlFileAction="Controllers/NhapKhoTaoPhieuXuatController.ashx" 
        FuncTaoPhieuXuatKhoN="cr_out_from_ins"
        FilterToolbar="true"
        ColNames="['c_nhapxuat_id', 'TT', 'Đơn Hàng', 'Số Phiếu', 'Số Phiếu NX'
        , 'Ngày Giao Nhận', 'Đối Tác', 'Người Giao', 'Người Nhập', 'Số Phiếu Khách'
        , 'Ngày Phiếu', 'Kho', 'Số Seal', 'Số Container', 'Loại Cont'
        , 'Loại Chứng Từ', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật'
        , 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
        RowNumbers="true"
        ColModel = "[
        {
            fixed: true, name: 'c_nhapxuat_id'
            , index: 'c_nhapxuat_id'
            , width: 100
            , hidden: true 
            , editable:true
            , edittype: 'text'
            , key: true
        },
        {
             fixed: true, name: 'md_trangthai_id'
             , index: 'nx.md_trangthai_id'
             , editable: false
             , width: 100
             , edittype: 'text'
             , formatter: 'status'
        },
        {
             fixed: true, name: 'c_donhang_id'
             , index: 'dh.sochungtu'
             , editable: true
             , width: 100
             , edittype: 'select'
             , editoptions: { dataUrl: 'Controllers/DonHangController.ashx?action=getoption',  readonly:'readonly' }
             
        },  
        {
             fixed: true, name: 'sophieu'
             , index: 'sophieu'
             , editable: true
             , width: 100
             , edittype: 'text'
        },
        {
             fixed: true, name: 'sophieunx'
             , index: 'sophieunx'
             , editable: true
             , width: 100
             , edittype: 'text'
        },
        {
             fixed: true, name: 'ngay_giaonhan'
             , index: 'ngay_giaonhan'
             , editable: true
             , width: 100
             , edittype: 'text'
             , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
        },
        {
             fixed: true, name: 'md_doitackinhdoanh_id'
             , index: 'dtkd.ma_dtkd'
             , editable: true
             , width: 100
             , edittype: 'select'
             , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption', readonly:'readonly' }
        },
        {
             fixed: true, name: 'nguoigiao'
             , index: 'nx.nguoigiao'
             , editable: true
             , width: 100
             , edittype: 'text'
        },
        {
             fixed: true, name: 'nguoinhan'
             , index: 'nx.nguoinhan'
             , editable: true
             , width: 100
             , edittype: 'text'
        },
        {
             fixed: true, name: 'sophieukhach'
             , index: 'nx.sophieukhach'
             , editable: true
             , width: 100
             , edittype: 'text'
        },
        {
             fixed: true, name: 'ngay_phieu'
             , index: 'nx.ngay_phieu'
             , editable: true
             , width: 100
             , edittype: 'text'
             , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
        },
        {
             fixed: true, name: 'nhapxuat_kho_id'
             , index: 'nx.nhapxuat_kho_id'
             , editable: true
             , width: 100
             , edittype: 'select'
             , editoptions: { dataUrl: 'Controllers/WarehouseController.ashx?action=getoption' }
        },
        {
             fixed: true, name: 'soseal'
             , index: 'nx.soseal'
             , editable: true
             , width: 100
             , edittype: 'text'
             , editrules:{ required:true }
        },
        {
             fixed: true, name: 'socontainer'
             , index: 'nx.socontainer'
             , editable: true
             , width: 100
             , edittype: 'text'
             , editrules:{ required:true }
        },
        {
             fixed: true, name: 'loaicont'
             , index: 'nx.loaicont'
             , editable: true
             , width: 100
             , edittype: 'select'
             , editoptions: { value:'CONT20:Cont 20;CONT40:Cont 40;40HC:40HC;LE:Cont Lẻ' }
        },
        {
             fixed: true, name: 'md_loaichungtu_id'
             , index: 'nx.md_loaichungtu_id'
             , editable: false
             , width: 100
             , edittype: 'select'
             , editoptions: { value:'NK:Nhập Kho;XK:Xuất Kho' }
        },
        {
            fixed: true, name: 'ngaytao'
            , index: 'ngaytao'
            , width: 100
            , editable:false
            , edittype: 'text'
            , hidden: true 
            , editrules:{ edithidden : true }
        },
        {
            fixed: true, name: 'nguoitao'
            , index: 'nguoitao'
            , width: 100
            , editable:false
            , edittype: 'text'
            , hidden: true 
            , editrules:{ edithidden : true }
        },
        {
            fixed: true, name: 'ngaycapnhat'
            , index: 'ngaycapnhat'
            , width: 100
            , editable:false
            , edittype: 'text'
            , hidden: true 
            , editrules:{ edithidden : true }
        },
        {
            fixed: true, name: 'nguoicapnhat'
            , index: 'nguoicapnhat'
            , width: 100
            , editable:false
            , edittype: 'text'
            , hidden: true 
            , editrules:{ edithidden : true }
        },
        {
            fixed: true, name: 'mota'
            , index: 'mota'
            , width: 100
            , editable:true
            , edittype: 'textarea'
        },
        {
            fixed: true, name: 'hoatdong', hidden: true
            , index: 'hoatdong'
            , width: 100
            , editable:true
            , edittype: 'checkbox'
            , align: 'center'
            , editoptions:{ value:'True:False', defaultValue: 'True' }
            , formatter: 'checkbox'
        }
        ]"
        GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
        Height = "170"
		RowNum="200"
        MultiSelect = "true" 
        ViewFormOptions="
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            },
            errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
        }"
        runat="server" />