<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-kiemke-kho.aspx.cs" Inherits="inc_kiemke_kho" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
        $('#lay-center-kiemkekho').parent().layout({
            north: {
                size: "30%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                    var o = $("#layout-north-kiemkekho").height();
                    $("#grid_ds_kiemkekho").setGridHeight(o + 4 - 77);
                }
            },
            center: {
                onresize_end: function() {
                    var o = $("#lay-center-kiemkekho").height();
                    $("#grid_ds_dongkiemke").setGridHeight(o + 4 - 77);
                }
            }
        })
    });
</script>
<DIV class="ui-layout-north ui-widget-content" id="layout-north-kiemkekho">
    <uc1:jqGrid  ID="grid_ds_kiemkekho" 
            Caption="Kiểm Kê Kho"
            SortName="c_kiemkekho_id" 
            UrlFileAction="Controllers/KiemKeKhoController.ashx" 
            ColNames="['c_kiemkekho_id', 'Tên Kiểm Kê', 'Tên Kho', 'Ngày Kiểm Kê', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
            ColModel = "[
            {
                fixed: true, name: 'c_kiemkekho_id'
                , index: 'c_kiemkekho_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ten_kiemke'
                 , index: 'ten_kiemke'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required:true}
            },
            {
                 fixed: true, name: 'md_kho_id'
                 , index: 'kho.md_kho_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editrules: { required:true}
                 , editoptions: { dataUrl: 'Controllers/WarehouseController.ashx?action=getoption' }
            },
            {
                 fixed: true, name: 'ngaykiemke'
                 , index: 'ngaykiemke'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
                 , editrules: { required:true}
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: true
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()+ 4 - 77);"
            Height = "420"
            ShowAdd ="true"
            ShowEdit ="true"
            ShowDel = "true"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            EditFormOptions ="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            DelFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            runat="server" />
</DIV>

<DIV class="ui-layout-center ui-widget-content" id="lay-center-kiemkekho" style="padding:0;"> 
<uc1:jqGrid  ID="grid_ds_dongkiemke" 
            Caption="Đóng Kiểm Kê"
            SortName="c_dongkiemke_id" 
            UrlFileAction="Controllers/DongKiemKeController.ashx" 
            ColNames="['c_dongkiemke_id', 'Tên Kiểm Kê', 'Mã SP', 'Mã SP', 'Đơn Vị Tính', 'Line', 'Số Lượng Đếm Được', 'Số Lượng Sổ Sách', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
            ColModel = "[
            {
                fixed: true, name: 'c_dongkiemke_id'
                , index: 'c_dongkiemke_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'c_kiemkekho_id'
                 , index: 'kkk.c_kiemkekho_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editrules: { required:true}
                 , editoptions: { dataUrl: 'Controllers/KiemKeKhoController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'md_sanpham_id'
                 , index: 'sp.md_sanpham_id'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required:true}
                 , hidden:true
            },
            {
                 fixed: true, name: 'ma_sanpham'
                 , index: 'sp.ma_sanpham'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules: { required:true}
                 , editoptions: { 
                    dataInit : function (elem) { 
                        $(elem).combogrid({
                            searchIcon: true,
                            width: '480px',
                            url: 'Controllers/ProductController.ashx?action=getcombogrid',
                            colModel: [
                                { 'columnName': 'md_sanpham_id', 'width': '0', 'label': 'ID', hidden:true }
                              , { 'columnName': 'ma_sanpham', 'width': '50', 'label': 'Mã SP', 'align':'left'}
                              , { 'columnName': 'mota_tiengviet', 'width': '50', 'label': 'Mô Tả TV' , 'align':'left'}],
                              select: function(event, ui) {
                                    $(elem).val(ui.item.ma_sanpham);
                                    $('#md_sanpham_id').val(ui.item.md_sanpham_id);
                                    return false;
                              }
                        });
                    } 
                }
            },
            {
                 fixed: true, name: 'md_donvitinh_id'
                 , index: 'dvt.md_donvitinh_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editrules: { required:true}
                 , editoptions: { dataUrl: 'Controllers/UnitController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'line'
                , index: 'line'
                , width: 100
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'sl_demduoc'
                , index: 'sl_demduoc'
                , width: 100
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'sl_sosach'
                , index: 'sl_sosach'
                , width: 100
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: true
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
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()+ 4 - 77);"
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            EditFormOptions ="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            DelFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            runat="server" />
</DIV>