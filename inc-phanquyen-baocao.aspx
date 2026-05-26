<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phanquyen-baocao.aspx.cs" Inherits="inc_phanquyen_baocao" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(document).ready(function () {
        $('#lay-center-phanquyen-baocao').parent().layout({
            center: {
                onresize_end: function () {
                    var o = $("#lay-center-phanquyen-baocao").height();
                    $("#grid_ds_baocao").setGridHeight(o - 90);
                }
            }
            , south: {
                size: "50%"
                , onresize_end: function () {
                    var o = $("#lay-south-phanquyen-baocao").height();
                    $("#grid_ds_phanquyen_baocao").setGridHeight(o - 90);
                }
            }
        });
    });
</script>

<div class="ui-layout-center ui-widget-content" id="lay-center-phanquyen-baocao">
    <uc1:jqGrid  ID="grid_ds_baocao" 
            Caption="Báo cáo"
            SortName="ten_baocao"
            SortOrder = "ASC"
            UrlFileAction="Controllers/BaoCaoController.ashx"
            ColNames="['md_baocao_id', 'Module Id', 'Tên Báo Cáo', 'Hành Động', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            FilterToolbar="true"
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
                fixed: true, name: 'md_baocao_id'
                , index: 'md_baocao_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'md_module_id'
                , index: 'md_module_id'
                , width: 100
                , editable:true
                , edittype: 'select'
                , editoptions:{ value:'BAOCAONCU:Báo Cáo Nhà Cung Ứng;BAOCAOKH:Báo Cáo Khách Hàng;BAOCAOQT:Báo Cáo Quản Trị' }
            },
            {
                 fixed: true, name: 'ten_baocao'
                 , index: 'ten_baocao'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                 fixed: true, name: 'hanhdong'
                 , index: 'hanhdong'
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
			                if($('#grid_ds_phanquyen_baocao').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_phanquyen_baocao').jqGrid('setGridParam',{url:'Controllers/PhanQuyenBaoCaoController.ashx?&md_baocao_id='+ids,page:1});
				                $('#grid_ds_phanquyen_baocao').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_phanquyen_baocao').jqGrid('setGridParam',{url:'Controllers/PhanQuyenBaoCaoController.ashx?&md_baocao_id='+ids,page:1});
			                $('#grid_ds_phanquyen_baocao').jqGrid().trigger('reloadGrid');			
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

<div class="ui-layout-south ui-widget-content" id="lay-south-phanquyen-baocao">
    <uc1:jqGrid  ID="grid_ds_phanquyen_baocao" 
            Caption="Phân quyền báo cáo"
            SortName="manv"
            SortOrder = "ASC"
            UrlFileAction="Controllers/PhanQuyenBaoCaoController.ashx"
            ColNames="['md_phanquyen_baocao_id', 'Báo cáo', 'Mã Nhân Viên', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            FilterToolbar="true"
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
                fixed: true
				, name: 'md_phanquyen_baocao_id'
                , index: 'pqbc.md_phanquyen_baocao_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'md_baocao_id'
                , index: 'bc.ten_baocao'
                , width: 100
                , editable:true
                , edittype: 'text'
                , hidden: true 
            },
            {
                 fixed: true, name: 'manv'
                 , index: 'pqbc.manv'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/UserController.ashx?action=getoption' }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'pqbc.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'pqbc.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'pqbc.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'pqbc.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'pqbc.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'pqbc.hoatdong'
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
                        var masterId = $('#grid_ds_baocao').jqGrid('getGridParam', 'selrow');
                        var forId = 'md_baocao_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một báo cáo mới có thể tạo chi tiết.!');
                        }else{
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_baocao').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_baocao_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một báo cáo mới có thể tạo chi tiết.!!'];
                    }else{
                        postdata.md_baocao_id = masterId;
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
    createRightPanel('grid_ds_baocao');
    createRightPanel('grid_ds_phanquyen_baocao');
</script>
