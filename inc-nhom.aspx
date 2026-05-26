<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-nhom.aspx.cs" Inherits="inc_nhom" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function() {
        $('#nhom-center').parent().layout({
            south: {
                size: "50%"
                , onresize: function() {
                    var o = $('#nhom-south');
                    var h = o.height();
                    var w = o.width();
                    $('#grid_ds_chitietnhom').setGridHeight(h - 70);
                    $('#grid_ds_chitietnhom').setGridWidth(w);
                }
            }
            , center: {
                onresize: function() {
                    var o = $('#nhom-center');
                    var h = o.height();
                    var w = o.width();
                    $('#grid_ds_nhom').setGridHeight(h - 70);
                    $('#grid_ds_nhom').setGridWidth(w);
                }
            }
        });
    });
</script>

<div id="nhom-center" class="ui-layout-center">
        <uc1:jqGrid  ID="grid_ds_nhom" 
            Caption="Nhóm"
            SortName="tennhom"
            SortOrder = "ASC"
            UrlFileAction="Controllers/NhomController.ashx"
            ColNames="['md_nhom_id', 'Tên Nhóm', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 70);"
            OndblClickRow = "function(rowid) { $(this).jqGrid(
                                    'editGridRow', rowid, { 
                                            errorTextFormat:function(data) { 
                                                return 'Lỗi: ' + data.responseText 
                                            }
                                            , afterSubmit: function(rs, postdata){
                                                return showMsgDialog(rs); 
                                            }
                                            , beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }
                                    }) 
                            }"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'md_nhom_id'
                , index: 'md_nhom_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'tennhom'
                 , index: 'tennhom'
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
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_chitietnhom').jqGrid('getGridParam','records') &gt; 0 )
			                {
			                    var caption = $(this).jqGrid('getCell', ids, 'tennhom');
			                    $('#grid_ds_chitietnhom').jqGrid('setCaption', 'Chi tiết của nhóm: ' + caption);
			                        
				                $('#grid_ds_chitietnhom').jqGrid('setGridParam',{url:'Controllers/ChiTietNhomController.ashx?&grpId='+ids,page:1});
				                $('#grid_ds_chitietnhom').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
				            var caption = $(this).jqGrid('getCell', ids, 'tennhom');
			                $('#grid_ds_chitietnhom').jqGrid('setCaption', 'Chi tiết của nhóm: ' + caption);
			                
			                $('#grid_ds_chitietnhom').jqGrid('setGridParam',{url:'Controllers/ChiTietNhomController.ashx?&grpId='+ids,page:1});
			                $('#grid_ds_chitietnhom').jqGrid().trigger('reloadGrid');			
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


<div id="nhom-south" class="ui-layout-south">
    <uc1:jqGrid  ID="grid_ds_chitietnhom" 
            Caption="Chi Tiết Nhóm"
            SortName="ctn.manv"
            SortOrder = "ASC"
            UrlFileAction="Controllers/ChiTietNhomController.ashx"
            ColNames="['md_chitietnhom_id', 'md_nhom_id', 'Mã Nhân Viên', 'Là Người Quản Lý', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 70);"
            OndblClickRow = "function(rowid) { $(this).jqGrid(
                                    'editGridRow', rowid, { 
                                            errorTextFormat:function(data) { 
                                                return 'Lỗi: ' + data.responseText 
                                            }
                                            , afterSubmit: function(rs, postdata){
                                                return showMsgDialog(rs); 
                                            }
                                            , beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }
                                    }) 
                            }"
            RowNumbers="true"
            ColModel = "[
            {
                fixed: true, name: 'md_chitietnhom_id'
                , index: 'ctn.md_chitietnhom_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_nhom_id'
                 , index: 'ctn.md_nhom_id'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , hidden: true
            },
            {
                 fixed: true, name: 'manv'
                 , index: 'ctn.manv'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/UserController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'nguoiquanly'
                , index: 'ctn.nguoiquanly'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'False' }
                , formatter: 'checkbox'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ctn.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'ctn.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ctn.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'ctn.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'ctn.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'ctn.hoatdong'
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
                width: 450
                , errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                , beforeShowForm: function (formid) {
                    var masterId = $('#grid_ds_nhom').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_nhom_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        alert('Hãy chọn một nhóm mới có thể tạo chi tiết.!');
                    }else{
                        $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_nhom').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_nhom_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một nhóm mới có thể tạo chi tiết.!'];
                    }else{
                        postdata.md_nhom_id = masterId;
                        return [true,'']; 
                    }
                }
                , afterSubmit: function(rs, postdata){
                   return showMsgDialog(rs);
                }
            "
            EditFormOptions="
                width: 450
                , errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata){
                   return showMsgDialog(rs);
                }
            "
            DelFormOptions="
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                , beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata){
                   return showMsgDialog(rs);
                }
            "
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
            }"
            runat="server" />
</div>

<script type="text/javascript">
    createRightPanel('grid_ds_nhom');
    createRightPanel('grid_ds_chitietnhom');
</script>