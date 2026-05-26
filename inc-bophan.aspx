<%@ Page Language="C#" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<script>
    /*$(window).bind('resize', function () {
        $("#grid_ds_nhanvien").setGridHeight($(window).height() - 220);
    }).trigger('resize');*/
</script>

    <uc1:jqGrid ID="grid_ds_bophan" 
            SortName="ma_bophan" 
            UrlFileAction="Controllers/BoPhanController.ashx" 
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 45);"
            ColNames="['BP Id', 'Mã BP', 'Tên BP', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid,{ 
                        afterSubmit: function(rs, postdata) {
                            return showMsgDialog(rs);
                        },
                        beforeShowForm: function (formid) {
                                               formid.closest('div.ui-jqdialog').dialogCenter(); 
                        }}); }"
            ColModel = "[
            {
                fixed: true, name: 'md_bophan_id'
                , index: 'md_bophan_id'
                , width: 100 
                , hidden:true
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'ma_bophan'
                , index: 'ma_bophan'
                , width: 100 
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ten_bophan'
                , index: 'ten_bophan'
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
                fixed: true, name: 'hoatdong'
                , index: 'hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
                , hidden:true
            }
            ]"
            AddFormOptions=" 
            afterSubmit: function(rs, postdata) {
                            return showMsgDialog(rs);
                        },
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }"
            EditFormOptions=" 
            afterSubmit: function(rs, postdata) {
                return showMsgDialog(rs);
            },
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }"
            ViewFormOptions=" beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }"
            DelFormOptions="
            afterSubmit: function(rs, postdata) {
                            return showMsgDialog(rs);
                        },
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            runat="server" />


<script type="text/javascript">
    createRightPanel('grid_ds_bophan');
</script>

