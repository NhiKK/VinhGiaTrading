<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phanquyen-import.aspx.cs" Inherits="inc_phanquyen_import" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function () {
        $('#lay-center-phanquyen-import').parent().layout({
            center: {
                onresize_end: function () {
                    var o = $("#lay-center-phanquyen-import").height();
                    $("#grid_ds_import").setGridHeight(o - 90);
                }
            }
            , south: {
                size: "50%"
                , onresize_end: function () {
                    var o = $("#lay-south-phanquyen-import").height();
                    $("#grid_ds_phanquyen_import").setGridHeight(o - 90);
                }
            }
        });
    });
</script>

<div class="ui-layout-center ui-widget-content" id="lay-center-phanquyen-import">
    <uc1:jqGrid  ID="grid_ds_import" 
            Caption="Tên import"
            SortName="ten_import"
            SortOrder = "ASC"
            UrlFileAction="Controllers/ImportController.ashx"
            ColNames="['md_import_id', 'Mã Import', 'Tên Import', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            FilterToolbar="true"
            OndblClickRow = "function(rowid) {
								
							}"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'md_import_id'
                , index: 'md_import_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
			{
                 fixed: true, name: 'ma_import'
                 , index: 'ma_import'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ten_import'
                 , index: 'ten_import'
                 , editable: true
                 , width: 100
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
            },
            {
                fixed: true, name: 'hoatdong', hidden: false
                , index: 'hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_phanquyen_import').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_phanquyen_import').jqGrid('setGridParam',{url:'Controllers/PhanQuyenImportController.ashx?&md_import_id='+ids,page:1});
				                $('#grid_ds_phanquyen_import').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_phanquyen_import').jqGrid('setGridParam',{url:'Controllers/PhanQuyenImportController.ashx?&md_import_id='+ids,page:1});
			                $('#grid_ds_phanquyen_import').jqGrid().trigger('reloadGrid');			
		                } }"
            Height = "420"
            ShowAdd="true"
            ShowEdit="true"
            ShowDel="true"
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
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
            EditFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
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
</div>

<div class="ui-layout-south ui-widget-content" id="lay-south-phanquyen-import">
    <uc1:jqGrid  ID="grid_ds_phanquyen_import" 
            Caption="Phân quyền import"
            SortName="manv"
            SortOrder = "ASC"
            UrlFileAction="Controllers/PhanQuyenImportController.ashx"
            ColNames="['md_phanquyenimport_id', 'Import', 'Mã Nhân Viên', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            FilterToolbar="true"
            OndblClickRow = "function(rowid) { 
                            
							}"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true
				, name: 'md_phanquyenimport_id'
                , index: 'pqip.md_phanquyenimport_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'md_import_id'
                , index: 'md_import_id'
                , width: 100
                , editable:true
                , edittype: 'text'
                , hidden: true 
            },
            {
                 fixed: true, name: 'manv'
                 , index: 'pqip.manv'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/UserController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'pqip.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'pqip.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'pqip.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'pqip.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'pqip.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'pqip.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                        var masterId = $('#grid_ds_import').jqGrid('getGridParam', 'selrow');
                        var forId = 'md_import_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một import mới có thể tạo chi tiết.!');
                        }else{
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_import').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_import_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một import mới có thể tạo chi tiết.!!'];
                    }else{
                        postdata.md_import_id = masterId;
                        return [true,'']; 
                    }
                }
                , afterSubmit: function(rs, postdata){
                   return showMsgDialog(rs);
                }
                "
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
                afterSubmit: function(rs, postdata) { 
                    return showMsgDialog(rs); 
            }"
            runat="server" />
</div>  

<script type="text/javascript">
    createRightPanel('grid_ds_import');
    createRightPanel('grid_ds_phanquyen_import');
</script>

