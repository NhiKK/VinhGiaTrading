<%@ page language="C#" autoeventwireup="true" codefile="inc-tygiamua.aspx.cs" inherits="inc_tygiamua" %>

<script type="text/javascript">
    var nam_tygiamua = <%=DateTime.Now.Year %>;
    $(document).ready(function () {
        $('#lay-center-tygiamua').parent().layout({
            west: {
                size: "15%"
            }
        });
        $('#lay-west-tygiamua button').button().css({ 'width': '95%' });
    });

    function updateGrid_tygiamua(year) {
        nam_tygiamua = year;
        $('.title-tygiamua').html('Tỷ Giá Mua Năm ' + nam_tygiamua);
        $('#grid_ds_tygiamua').jqGrid('setGridParam', { url: 'Controllers/TyGiaMuaController.ashx?nam=' + nam_tygiamua, page: 1 });
        $('#grid_ds_tygiamua').jqGrid().trigger('reloadGrid');
    }

    function taoTyGia_tygiamua() {
        $('body').append(`
            <div title="Tạo tỷ giá năm ${nam_tygiamua}" id=\"dialog_tao_tygiamua\">
                <div style="display:none" id="wait">
                    <img style="width:30px; height:30px" src="iconcollection/loading.gif" />
                </div>
                <h3 style="padding: 10px" class="ui-state-highlight ui-corner-all">Bạn có chắn chắn muốn tạo tỷ giá cho năm ${nam_tygiamua}</h3>
            </div>`
        );
        let btn_close = null;
        // create new dialog
        $('#dialog_tao_tygiamua').dialog({
            modal: true
            , open: function (event, ui) {
                //hide close button.
                btn_close = $(this).parent().children().children('.ui-dialog-titlebar-close');
            }
            , buttons:
                [
                    {
                        id: "btn-active",
                        text: "Có",
                        click: function () {
                            $("#wait").css("display", "block");
                            $("#btn-active").button("disable");
                            $("#btn-close").button("disable");
                            btn_close.hide();
                            $.post("Controllers/TyGiaMuaController.ashx?action=tao_tygiamua"
                                , { nam: nam_tygiamua }
                                , function (result) {
                                    $('#dialog_tao_tygiamua').find('#dialog-caution').html(result);
                                    $('#grid_ds_po').jqGrid().trigger('reloadGrid');
                                    $("#wait").css("display", "none");
                                    $("#btn-close span").text("Thoát");
                                    $("#btn-close").button("enable");
                                    btn_close.show();
                                });
                        }
                    },
                    {
                        id: "btn-close",
                        text: "Không",
                        click: function () {
                            $(this).dialog("destroy").remove();
                        }
                    }
                ]
        });
    }
    function imageUnFormat_tygiamua(cellvalue, options, cell) {
        console.error('12324324');
        let src = $('img', cell).attr('src');
        if (src.lastIndexOf('SOANTHAO'))
            return 'SOANTHAO';
        else
            return 'HIEULUC';
    }
</script>

<div style="background: #F4F0EC" class="ui-layout-center ui-widget-content" id="lay-center-tygiamua">
    <div style="padding: 7px; text-align: center; background-color: #FFF">
        <b style="font-size: 16px;" class="title-tygiamua">Tỷ Giá Mua Năm <%=DateTime.Now.Year %></b>
    </div>
    <%@ register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
    <uc1:jqgrid id="grid_ds_tygiamua"
        sortname="ngayapdung"
        sortorder="desc"
        urlfileaction="Controllers/TyGiaMuaController.ashx"
        colnames="['md_tygia_id', 'Trạng Thái', 'Tên Tỷ Giá', 'Kỳ', 'Từ Đồng Tiền', 'Sang Đồng Tiền', 'Ngày Áp Dụng', 'Ngày Kết Thúc', 'Nhân Với', 'Chia Cho', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả']"
        rownumbers="true"
        gridcomplete="var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 82);"
        ondblclickrow="function(rowid) { $(this).jqGrid('editGridRow', rowid,{ beforeShowForm: function (formid) {
                                                formid.closest('div.ui-jqdialog').dialogCenter(); 
                                            }}); }"
        colmodel="[
            {
                fixed: true, name: 'md_tygiamua_id'
                , index: 'md_tygiamua_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'trangthai'
                 , index: 'trangthai'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { defaultValue:'HIEULUC', value: 'SOANTHAO:Soạn Thảo;HIEULUC:Hiệu Lực' }
                 , formatter: 'imagestatus'
                 , align: 'center'
            },
            {
                 fixed: true, name: 'ten_tygia'
                 , hidden: false
                 , index: 'ten_tygia'
                 , editable: false
                 , width: 100
                 , edittype: 'text'
            },
            {
                fixed: true, name: 'ky'
                , hidden: true
                , width: 100
                , editable:true
                , edittype: 'select'
                , editrules: { edithidden:true }
                , editoptions: { value: '1:Kỳ 1;2:Kỳ 2;3:Kỳ 3;4:Kỳ 4;5:Kỳ 5;6:Kỳ 6;7:Kỳ 7;8:Kỳ 8;9:Kỳ 9;10:Kỳ 10;11:Kỳ 11;12:Kỳ 12' }
            },
            {
                 fixed: true, name: 'tu_dongtien'
                 , index: 'tu_dongtien'
                 , editable: true
                 , width: 100
                 , editoptions: { defaultValue: 'USD', disabled: true }
            },
            {
                fixed: true, name: 'sang_dongtien'
                , index: 'sang_dongtien'
                , width: 100
                , editable:true
                , editoptions: { defaultValue: 'VND', disabled: true }
            },
            
            {
                fixed: true, name: 'ngayapdung'
                , index: 'ngayapdung'
                , width: 100
                , editable:false
                , edittype: 'text'
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                fixed: true, name: 'ngayketthuc'
                , index: 'ngayketthuc'
                , width: 100
                , editable:false
                , edittype: 'text'
                , editoptions: { dataInit : function (elem) { $(elem).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' }); }, defaultValue: function(){ var currentTime = new Date(); var month = parseInt(currentTime.getMonth() + 1);     month = month &lt;= 9 ? '0' + month : month; var day = currentTime.getDate(); day = day &lt;= 9 ? '0'+ day : day; var year = currentTime.getFullYear(); return day + '/' + month + '/' + year; } }
            },
            {
                fixed: true, name: 'nhan_voi'
                , index: 'nhan_voi'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editrules : { number: true }
                , editoptions: { defaultValue: '23000' }
            },
            {
                fixed: true, name: 'chia_cho', hidden: true
                , index: 'chia_cho'
                , width: 100
                , editable:false
                , edittype: 'text'
                , editrules : { number: true }
                , editoptions: { defaultValue: '0.000043' }
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
            }
            ]"
        height="420"
        addformoptions="
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter();
            },
            beforeSubmit: function(postdata, formid){
				postdata.nam = nam_tygiamua;
                return [true,'']; 
			}
        "
        editformoptions=" 
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter();
                let row_id = $('#grid_ds_tygiamua').jqGrid('getGridParam', 'selrow');
                let trangthai = $('#grid_ds_tygiamua').jqGrid('getCell',row_id,'trangthai');
                if(trangthai.includes('HIEULUC'))
                    $('#trangthai').val('HIEULUC');
                else
                    $('#trangthai').val('SOANTHAO');
            },
            beforeSubmit: function(postdata, formid){
				postdata.nam = nam_tygiamua;
                return [true,'']; 
			}
        "
        viewformoptions="
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }
        "
        delformoptions="
            beforeShowForm: function (formid) {
                formid.closest('div.ui-jqdialog').dialogCenter(); 
            }, 
            afterSubmit: function(rs, postdata){
                return showMsgDialog(rs);
            }
        "
        showadd="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
        showedit="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
        showdel="<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
        runat="server" />
</div>
<div style="background: #F4F0EC;" class="ui-layout-west" id="lay-west-tygiamua">
    <div class="ui-widget-content" style="height: 100%; overflow: auto; text-align: center">
        <% for (var i = DateTime.Now.Year; i >= 2018; i--) { %>
        <button style="text-align: left;" onclick="updateGrid_tygiamua(<%=i %>)">Năm <%=i %></button>
        <% } %>
    </div>
</div>

<script type="text/javascript">
    createRightPanel('grid_ds_tygiamua');
</script>
