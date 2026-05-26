<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-kho.aspx.cs" Inherits="inc_kho" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function() {
        $('#lay-center-kho').parent().layout({
            north: {
                size: "40%"
                , onresize_end: function() { var o = $("#lay-north-kho").height(); $("#grid_ds_kho").setGridHeight(o + 8 - 80);}
            },
            center:{
                onresize_end: function() { var o = $("#lay-center-kho").height(); $("#grid_ds_hangtrongkho").setGridHeight(o + 8 - 80);}
            }
        });
    });
</script>
<DIV class="ui-layout-north ui-widget-content" id="lay-north-kho">
    <uc1:jqGrid  ID="grid_ds_kho" 
        SortName="ten_kho" 
        Caption="Kho"
        UrlFileAction="Controllers/WarehouseController.ashx" 
        ColNames="['md_kho_id', 'Mã Kho', 'Tên Kho', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
        RowNumbers="true"
        OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
        } ); }"
        ColModel = "[
        {
            fixed: true, name: 'md_kho_id'
            , index: 'md_kho_id'
            , width: 100
            , hidden: true 
            , editable:true
            , edittype: 'text'
            , key: true
        },
        {
            fixed: true, name: 'ma_kho'
            , index: 'ma_kho'
            , editable: true
            , width: 100
            , edittype: 'text'
            , editrules:{ required: true }
        },
        {
            fixed: true, name: 'ten_kho'
            , index: 'ten_kho'
            , width: 100
            , edittype: 'text'
            , editable:true
            , editrules:{ required: true }
        },
        {
            fixed: true, name: 'ngaytao'
            , index: 'ngaytao'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'nguoitao'
            , index: 'nguoitao'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'ngaycapnhat'
            , index: 'ngaycapnhat'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'nguoicapnhat'
            , index: 'nguoicapnhat'
            , width: 100
            , editable:false
            , edittype: 'text'
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
        GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 8 - 80);"
        OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_hangtrongkho').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_hangtrongkho').jqGrid('setGridParam',{url:'Controllers/ProductInWarehouseController.ashx?&cateId='+ids,page:1});
				                $('#grid_ds_hangtrongkho').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_hangtrongkho').jqGrid('setGridParam',{url:'Controllers/ProductInWarehouseController.ashx?&cateId='+ids,page:1});
			                $('#grid_ds_hangtrongkho').jqGrid().trigger('reloadGrid');			
		                } }"
        Height = "420"
        ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
        ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
        ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
        AddFormOptions=" 
                beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
        "
        EditFormOptions ="
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
            "
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
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
            "
        runat="server" />
</DIV> 
<DIV class="ui-layout-center ui-widget-content" id="lay-center-kho">
    <uc1:jqGrid  ID="grid_ds_hangtrongkho" 
            Caption="Hàng trong kho"
            SortName="htk.md_hangtrongkho_id" 
            UrlFileAction="Controllers/ProductInWarehouseController.ashx" 
            ColNames="['md_hangtrongkho_id', 'Kho', 'Sản Phẩm', 'Sản Phẩm', 'Số lượng', 'Số lượng trước kiểm tra', 'Đơn Vị Tính', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            } ); }"
            ColModel = "[
            {
                fixed: true, name: 'md_hangtrongkho_id'
                , index: 'htk.md_hangtrongkho_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'tenkho'
                 , index: 'kho.ten_kho'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/WarehouseController.ashx?action=getoption' }
                 , editrules:{ required: true }
            },
            {
                 fixed: true, name: 'sanpham_id'
                 , index: 'sp.md_sanpham_id'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , hidden:true
                 , editrules:{ required:true }
            },
            {
                fixed: true, name: 'tensp'
                , index: 'sp.ma_sanpham'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ required: true }
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
                                    $('#sanpham_id').val(ui.item.md_sanpham_id);
                                    return false;
                              }
                        });
                    } 
                }
            },
            {
                fixed: true, name: 'qty'
                , index: 'htk.qty'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ required: true, number: true  }
            },
            {
                fixed: true, name: 'qty_pre'
                , index: 'htk.qty_pre'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editrules:{ required: true, number: true  }
            },
            {
                fixed: true, name: 'tendvt'
                , index: 'dvt.ten_dvt'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/UnitController.ashx?action=getoption' }
                , editrules:{ required: true }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'htk.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'htk.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'htk.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'htk.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'mota'
                , index: 'htk.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'htk.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() + 8 - 80);"
            Height = "420"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
                "
            EditFormOptions ="
                beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
                "
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
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
                "
            runat="server" />
</DIV>


<script type="text/javascript">
    createRightPanel('grid_ds_kho');
    createRightPanel('grid_ds_hangtrongkho');
</script>
