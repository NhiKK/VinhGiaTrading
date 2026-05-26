<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-quytacmota.aspx.cs" Inherits="inc_quytacmota" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    $(function() {
        $('#lay-center-quytacmota').parent().layout({
            south: {
                size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                    var o = $("#lay-south-quytacmota").height();
                    $("#grid_ds_mausac").setGridHeight(o - 90);

                }
            }
            , north: {
                size: "25%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                    var o = $("#lay-north-quytacmota").height();
                    $("#grid_ds_chungloai").setGridHeight(o - 90);
                }
            }
            , center: {
                onresize_end: function() {
                    var o = $("#lay-center-quytacmota").height();
                    $("#grid_ds_detai").setGridHeight(o - 90);
                }
            }
        });
    });
</script>
 <%--dlclick chungloai OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            }); }"--%>
<DIV class="ui-layout-north ui-widget-content" id="lay-north-quytacmota">
    <uc1:jqGrid  ID="grid_ds_chungloai" 
            Caption="Chủng loại"
            FilterToolbar="true"
            SortName="cl.code_cl" 
            SortOrder="asc"
            UrlFileAction="Controllers/ChungLoaiController.ashx" 
            ColNames="['md_chungloai', 'Code Chủng Loại', 'Tiếng Việt Ngắn', 'Tiếng Anh Ngắn', 'Tiếng Việt Dài', 'Tiếng Anh Dài',  'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
           
            ColModel = "[
            {
                fixed: true, name: 'md_chungloai_id'
                , index: 'cl.md_chungloai_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'code_cl'
                , index: 'cl.code_cl'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                 fixed: true, name: 'tv_ngan'
                 , index: 'cl.tv_ngan'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
            },
            {
                fixed: true, name: 'ta_ngan'
                , index: 'cl.ta_ngan'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'tv_dai'
                , index: 'cl.tv_dai'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'ta_dai'
                , index: 'cl.ta_dai'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'cl.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'cl.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'cl.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'cl.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'mota'
                , index: 'cl.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'cl.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            Height = "100"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_detai').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_detai').jqGrid('setGridParam',{url:'Controllers/DeTaiController.ashx?&ChungLoaiId='+ids,page:1});
				                $('#grid_ds_detai').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_detai').jqGrid('setGridParam',{url:'Controllers/DeTaiController.ashx?&ChungLoaiId='+ids,page:1});
			                $('#grid_ds_detai').jqGrid().trigger('reloadGrid');			
		                } }"
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
            EditFormOptions="
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
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            "
            runat="server" />
</DIV>

 <%-- OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
            , afterSubmit: function(rs, postdata) {
              return showMsgDialog(rs);
            }
            , errorTextFormat:function(data) { 
                return 'Lỗi: ' + data.responseText 
            }
        } ); }" --%>
<DIV class="ui-layout-south ui-widget-content" id="lay-south-quytacmota" style="overflow:hidden">
 <uc1:jqGrid  ID="grid_ds_mausac" 
        Caption="Màu Sắc"
        FilterToolbar="true"
        SortName="ms.code_mau" 
        SortOrder="desc"
        UrlFileAction="Controllers/MauSacController.ashx" 
        ColNames="['md_mausac_id', 'Code Chủng Loại', 'Code Đề Tài', 'Code Màu', 'Tiếng Việt Ngắn', 'Tiếng Anh Ngắn', 'Tiếng Việt Dài', 'Tiếng Anh Dài', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
        RowNumbers="true"
       
        ColModel = "[
        {
            fixed: true, name: 'md_mausac_id'
            , index: 'ms.md_mausac_id'
            , width: 100
            , hidden: true 
            , editable:true
            , edittype: 'text'
            , key: true
        },
        {
             fixed: true, name: 'md_chungloai_id'
             , index: 'cl.md_chungloai_id'
             , editable: true
             , width: 100
             , edittype: 'text'
             , hidden:true
        },
        {
            fixed: true, name: 'md_detai_id'
            , index: 'dt.md_detai_id'
            , width: 100
            , edittype: 'text'
            , editable:true
            , hidden:true
        },
        {
            fixed: true, name: 'code_mau'
            , index: 'ms.code_mau'
            , width: 100
            , editable:true
            , edittype: 'text'
        },
        {
            fixed: true, name: 'tv_ngan'
            , index: 'ms.tv_ngan'
            , width: 100
            , editable:true
            , edittype: 'text'
        },
        {
            fixed: true, name: 'ta_ngan'
            , index: 'ms.ta_ngan'
            , width: 100
            , editable:true
            , edittype: 'text'
        },
        {
            fixed: true, name: 'tv_dai'
            , index: 'ms.tv_dai'
            , width: 100
            , editable:true
            , edittype: 'text'
			, hidden: true
        },
        {
            fixed: true, name: 'ta_dai'
            , index: 'ms.ta_dai'
            , width: 100
            , editable:true
            , edittype: 'text'
			, hidden: true
        },
        {
            fixed: true, name: 'ngaytao'
            , index: 'ms.ngaytao'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'nguoitao'
            , index: 'ms.nguoitao'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'ngaycapnhat'
            , index: 'ms.ngaycapnhat'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'nguoicapnhat'
            , index: 'ms.nguoicapnhat'
            , width: 100
            , editable:false
            , edittype: 'text'
        },
        {
            fixed: true, name: 'mota'
            , index: 'ms.mota'
            , width: 100
            , editable:true
            , edittype: 'textarea'
        },
        {
            fixed: true, name: 'hoatdong', hidden: true
            , index: 'ms.hoatdong'
            , width: 100
            , editable:true
            , edittype: 'checkbox'
            , align: 'center'
            , editoptions:{ value:'True:False', defaultValue: 'True' }
            , formatter: 'checkbox'
        }
        ]"
        Height = "100"
        GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
        ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
        ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
        ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
        AddFormOptions=" 
            beforeShowForm: function (formid) {
                    var masterId = $('#grid_ds_chungloai').jqGrid('getGridParam', 'selrow');
                    var masterId_detai = $('#grid_ds_detai').jqGrid('getGridParam', 'selrow');
                    
                    var forId = 'md_chungloai_id';
                    var forId_detai = 'md_detai_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        alert('Hãy chọn một chủng loại mới có thể tạo chi tiết.!');
                        
                    }else if(masterId_detai == null){
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        alert('Hãy chọn một đề tài mới có thể tạo chi tiết.!');
                    }else{
                        $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                        $('#tr_' + forId_detai + ' td.DataTD input#' + forId_detai, formid).val(masterId_detai);
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
            }
            , beforeSubmit:function(postdata, formid){
                var masterId = $('#grid_ds_chungloai').jqGrid('getGridParam', 'selrow');
                var masterId_detai = $('#grid_ds_detai').jqGrid('getGridParam', 'selrow');
                
                var forId = 'md_chungloai_id';
                var forId_detai = 'md_detai_id';
                if(masterId == null)
                {
                    $(formid).parent().parent().parent().dialog('destroy').remove();
                    alert('Hãy chọn một chủng loại mới có thể tạo chi tiết.!');
                    
                }else if(masterId_detai == null){
                    $(formid).parent().parent().parent().dialog('destroy').remove();
                    alert('Hãy chọn một đề tài mới có thể tạo chi tiết.!');
                }else{
                    postdata.md_chungloai_id = masterId;
                    postdata.md_detai_id = masterId_detai;
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
    
 <%-- OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid, {
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
                , afterSubmit: function(rs, postdata) {
                  return showMsgDialog(rs);
                }
                , errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
            } ); }" --%>
<DIV class="ui-layout-center ui-widget-content" id="lay-center-quytacmota">
 
   <uc1:jqGrid  ID="grid_ds_detai" 
            Caption="Đề tài"
            FilterToolbar = "true"
            SortName="dt.code_dt" 
            SortOrder="desc"
            UrlFileAction="Controllers/DeTaiController.ashx" 
            ColNames="['md_detai_id', 'Code Chủng Loại', 'Code Đề Tài', 'Tiếng Việt Ngắn', 'Tiếng Anh Ngắn', 'Tiếng Việt Dài', 'Tiếng Anh Dài', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Ghi chú', 'Hoạt động']"
            RowNumbers="true"
           
            ColModel = "[
            {
                fixed: true, name: 'md_detai_id'
                , index: 'dt.md_detai_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'md_chungloai_id'
                 , index: 'cl.md_chungloai_id'
                 , editable: true
                 , width: 100
                 , edittype: 'text'
                 , hidden:true
                 , editrules:{ required:true }
            },
            {
                fixed: true, name: 'code_dt'
                , index: 'dt.code_dt'
                , width: 100
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'tv_ngan'
                , index: 'dt.tv_ngan'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'ta_ngan'
                , index: 'dt.ta_ngan'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'tv_dai'
                , index: 'dt.tv_dai'
                , width: 100
                , edittype: 'text'
                , editable:true
				, hidden: true
            },
            {
                fixed: true, name: 'ta_dai'
                , index: 'dt.ta_dai'
                , width: 100
                , edittype: 'text'
                , editable:true
				, hidden: true
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'dt.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'dt.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'dt.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'dt.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'mota'
                , index: 'dt.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'dt.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            Height = "100"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_mausac').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_mausac').jqGrid('setGridParam',{url:'Controllers/MauSacController.ashx?&DeTaiId='+ids,page:1});
				                $('#grid_ds_mausac').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_mausac').jqGrid('setGridParam',{url:'Controllers/MauSacController.ashx?&DeTaiId='+ids,page:1});
			                $('#grid_ds_mausac').jqGrid().trigger('reloadGrid');			
		                } }"
            AddFormOptions=" 
                beforeShowForm: function (formid) {
                        var masterId = $('#grid_ds_chungloai').jqGrid('getGridParam', 'selrow');
                        var forId = 'md_chungloai_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một chủng loại mới có thể tạo chi tiết.!');
                        }else{
                            $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_chungloai').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_chungloai_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một chủng loại mới có thể tạo chi tiết.!!'];
                    }else{
                        postdata.md_chungloai_id = masterId;
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
                }
                , errorTextFormat:function(data) { 
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

<script type="text/javascript">
    createRightPanel('grid_ds_chungloai');
    createRightPanel('grid_ds_detai');
    createRightPanel('grid_ds_mausac');
</script>