<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-modong-ky.aspx.cs" Inherits="inc_modong_ky" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>

   <script lang="javascript">
        setTimeout(function () {
            $('#grid_ds_modongky-pager_left table tr').prepend(
                '<td class="ui-pg-button ui-corner-all" title="Tạo kỳ nhanh" id="taoky_grid_ds_ky">' +
                '<div class="ui-pg-div"><span onclick="Taomoichoky()" class="ui-icon ui-icon-circle-plus"></span></div><td>')
            ,5000
        });

        function Taomoichoky() {

            $.post("Controllers/MoDongKyController.ashx?action=TMCK", function (result) {
                alert(result);
            });
        }

        function dongMoKy() {
            var msg = "";
            var id = $('#grid_ds_modongky').jqGrid('getGridParam', 'selrow');

            if (id == null) {
                msg = "Chọn một kỳ để có thể thao tác!";
                // append dialog-taoky to body
                $('body').append('<div title="Thông Báo" id=\"dialog-active-po\"><h3>' + msg + '</h3></div>');

                // create new dialog
                $('#dialog-dongmoky').dialog({
                    modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons: {
                    'Thoát': function() {
                        $(this).dialog("destroy").remove();
                    }
                }
                });
            } else {
                msg = "Đóng/Mở kỳ kế toán";
                var p = '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span><span id="dialog-caution">' + msg + '</span></p>';
                var h3 = '<h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">' + p + '<div style="display:none" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>';
                var nhan_chonchedo = '<div style="padding: 6px 0px 1px 14px;"><input id="chk_kh" type="checkbox" value="Khách hàng" /><span style="position: absolute;margin: -1px 0 0 5px;">Khách hàng</span></div><div style="padding: 6px 0px 1px 14px;"><input id="chk_ncc" type="checkbox" value="Nhà cung cấp" /><span style="position: absolute;margin: -1px 0 0 5px;">Nhà cung cấp</span></div>';
                // append dialog-taoky to body
                $('body').append('<div title="Thông Báo" id=\"dialog-dongmoky\">' + h3 + nhan_chonchedo + '</div>');

                // create new dialog
                $('#dialog-dongmoky').dialog({
                    modal: true
                , open: function(event, ui) {
                    //hide close button.
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function() {
                                $("#wait").css("display", "block");
                                $("#btn-active").button("disable");
                                $("#btn-close").button("disable");


                                $.get("Controllers/MoDongKyController.ashx?action=OC&dongmoky_id=" + id + "&khachhang=" + document.getElementById('chk_kh').checked + "&nhacungcap=" + document.getElementById('chk_ncc').checked, function (result) {
                                //$('#dialog-caution').css({ 'color': 'green' });
                                $('#dialog-dongmoky').find('#dialog-caution').html(result);
                                $('#grid_ds_modongky').jqGrid().trigger('reloadGrid');
                                $("#wait").css("display", "none");
                                $("#btn-close span").text("Thoát");
                                $("#btn-close").button("enable");
                            });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Không",
                        click: function() {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
                });
            }
        }
    </script>

   <uc1:jqGrid  ID="grid_ds_modongky" 
            SortName="ky_hoatdong" 
            UrlFileAction="Controllers/MoDongKyController.ashx" 
            ColNames="['a_dongmoky_id', 'Năm Tài Chính', 'Kỳ Kế Toán', 'Kỳ Hoạt Động', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động','Hiệu Lực KH', 'Hiệu Lực NCC']"
            RowNumbers="true"
            FilterToolbar="true"
           FuncOpenClose="dongMoKy"
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
                fixed: true, name: 'a_dongmoky_id'
                , index: 'a_dongmoky_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'tennam'
                 , index: 'ntc.nam'
                 , editable: true
                 , width: 100
                 ,search: true
                 , edittype: 'select'
                 , editoptions: { 
                    dataUrl: 'Controllers/NamTaiChinhController.ashx?action=getoption' 
                    , dataInit: function(elem){
                        var thisval = $(elem).val();
                        $.ajax({
                           url: 'Controllers/KyTrongNamController.ashx?action=getoption&yearId=' + thisval
                           , type: 'GET'
                           , beforeSend: function() {
                                $('#tenky').html('&lt;option value=&quot;&quot;&gt;Loading cities...&lt;/option&gt;')
                                .attr('disabled', 'disabled');
                           }
                           , success: function(result) {
                               $('#tenky').removeAttr('disabled').html(result);
                           }
                       });
                    }
                    , dataEvents :[{
                        type: 'change',
                        fn:function(e){
                            var thisval = $(e.target).val();
                              $.ajax({
                                   url: 'Controllers/KyTrongNamController.ashx?action=getoption&yearId=' + thisval,
                                   type: 'GET',
                                   success: function(result) {
                                       $('#tenky').html(result);
                                   }
                               });
                        } // end func
                    }]
                 }
            },
            {
                fixed: true, name: 'tenky'
                , index: 'ktn.tenky'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { value: 'Chọn 1 kỳ' }
            },
            {
                fixed: true, name: 'ky_hoatdong'
                , index: 'ky_hoatdong'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: {value : 'MOKY:Mở Kỳ;DONGKY:Đóng Kỳ;DONGVINHVIEN:Đóng Kỳ Vĩnh Viễn'}
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
                , editoptions:{ value:'True:False' }
                , formatter: 'checkbox'
            },

            {
                fixed: true, name: 'hieuluc', hidden: false
                , index: 'daxuly'
                , width: 100
                , editable:false
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            },

             {
                fixed: true, name: 'hieuluc2', hidden: false
                , index: 'daxuly2'
                , width: 100
                , editable:false
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }

            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 65);"
            Height = "400"
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
			
<style type="text/css">
    #taoky_grid_ds_ky:hover
    {
        background-color:ActiveBorder
    }
    #taoky_grid_ds_ky:active
    {
        cursor:wait
    }
</style>

<script type="text/javascript">
    createRightPanel('grid_ds_modongky');
</script>