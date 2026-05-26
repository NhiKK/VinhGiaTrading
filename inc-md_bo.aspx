<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function() {
        $('#lay-center-md_bo').parent().layout({
            north: {
                size: "40%"
                , onresize_end: function() { var o = $("#lay-north-md_bo").height(); $("#grid_ds_md_bo").setGridHeight(o - 90);}
            },
            center:{
                onresize_end: function() { var o = $("#lay-center-md_bo").height(); $("#grid_ds_md_bo_chitiet").setGridHeight(o + 8 - 80);}
            }
        });
    });
</script>

<DIV class="ui-layout-north ui-widget-content" id="lay-north-md_bo">
    <uc1:jqGrid  ID="grid_ds_md_bo" 
            SortName="RowNum" 
			Caption="Bộ"
            SortOrder="asc"
            FilterToolbar="true"
            UrlFileAction="Controllers/Md_boController.ashx" 
            RowNumbers="true"
			OndblClickRow = "function(rowid) { 
				$('#edit_grid_ds_md_bo').click();
            }"
            ColNames="[
					'md_bo_id', 'Mã bộ', 'Thuộc bộ', 'Ngày Tạo'
                    , 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật'
                    , 'Mô tả', 'Hoạt động', 'Ghi chú'
			]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()-90);"
			OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_md_bo_chitiet').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_md_bo_chitiet').jqGrid('setGridParam',{url:'Controllers/Md_bo_chitietController.ashx?&cateId='+ids, page:1});
				                $('#grid_ds_md_bo_chitiet').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_md_bo_chitiet').jqGrid('setGridParam',{url:'Controllers/Md_bo_chitietController.ashx?&cateId='+ids, page:1});
			                $('#grid_ds_md_bo_chitiet').jqGrid().trigger('reloadGrid');			
		                } }"
            ColModel = "[
            {
                fixed: true, name: 'md_bo_id'
                , index: 'md_bo_id'
				, label: 'md_bo_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ma_bo'
                 , index: 'ma_bo'
				 , label: 'Mã bộ'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'ma_bo_cha'
                 , index: 'ma_bo_cha'
				 , label: 'Thuộc bộ'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'ngaytao'
				, label: 'Ngày tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'nguoitao'
				, label: 'Người tạo'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'ngaycapnhat'
				, label: 'Ngày cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'nguoicapnhat'
				, label: 'Người cập nhật'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'mota'
				, label: 'Mô tả'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: true
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'hoatdong'
				, label: 'Hoạt động'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            },
			{
                fixed: true, name: 'ghichu'
                , index: 'ghichu'
				, label: 'Ghi chú'
                , width: 100
                , editable:true
                , edittype: 'textarea'
                , hidden: true
            }
            ]"
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
                }
            "
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

<DIV class="ui-layout-center ui-widget-content" id="lay-center-md_bo">
<uc1:jqGrid  ID="grid_ds_md_bo_chitiet" 
            Caption="Chi tiết Bộ"
            SortName="md_bo_detail" 
			SortOrder="asc"
            UrlFileAction="Controllers/Md_bo_chitietController.ashx" 
            ColNames="['md_bo_chitiet_id', 'md_bo_id', 'Mã cái', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { 
				$('#edit_grid_ds_md_bo_chitiet').click();
            }"
            ColModel = "[
            {
                fixed: true, name: 'md_bo_chitiet_id'
                , index: 'bct.md_bo_chitiet_id'
                , width: 100
                , hidden: true 
                , editable:false
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_bo_id'
                 , index: 'bct.md_bo_id'
                 , editable: false
                 , width: 100
				 , hidden: true 
                 , editrules:{ required: true }
            },
            {
                 fixed: true, name: 'md_bo_detail'
                 , index: 'bct.md_bo_detail'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , editrules:{ required:true }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'bct.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
				, hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'bct.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
				, hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'bct.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
				, hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'bct.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
				, hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'bct.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'bct.hoatdong'
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
			ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
			, beforeSubmit:function(postdata, formid){
				var masterId = $('#grid_ds_md_bo').jqGrid('getGridParam', 'selrow');
				postdata.md_bo_id = masterId;
				return [true];
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
    createRightPanel('grid_ds_md_bo');
    createRightPanel('grid_ds_md_bo_chitiet');
</script>