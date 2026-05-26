<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-doitackinhdoanh.aspx.cs" Inherits="inc_doitackinhdoanh" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
    <script language="javascript">


        $(function() {
            $('#lay-center-dtkd').parent().layout({
                north: {
                    size: "50%"
                , minSize: "0%"
                , maxSize: "100%"
                , onresize_end: function() {
                    var o = $("#layout-north-dtkd").height();
                    $("#grid_ds_doitackinhdoanh").setGridHeight(o - 90);
                }
                },
                center: {
                    onresize_end: function() {
                        var o = $("#lay-center-dtkd").height();
                        $("#grid_ds_nguoilienhe").setGridHeight(o - 70);
                    }
                }
            });
        });
    
	function funcPrint() {
        //var poId = $('#grid_ds_po').jqGrid('getGridParam', 'selrow');
		var poId = "";
        var name = 'dialog-print-po';
        var title = 'Trích xuất dữ liệu';
        var content = '<table style="width:100%">' +
							'<tr>' +
                                '<td>' +
                                    '<input id="printdtkd" type="radio" name="rdoPrintType" value="printdtkd" checked="checked" />' +
                                    '<label for="printdtkd">Chiết xuất thông tin</label>' +
                                '</td>' +
                            '</tr>' +
                      '</table>';

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
            modal: true
            , open: function(event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
            , buttons: [
            {
                id: "btn-print-ok",
                text: "In",
                click: function() {
                    var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-po').val();
                    var printURL = "PrintControllers/";
                    var windowSize = "width=700,height=700,scrollbars=yes";
                    if (typeof printType != 'undefined') {
                        if (printType == 'printdtkd') {
							printURL += "InDoiTacKinhDoanh/";
							window.open(printURL + "?c_donhang_id=" + poId, "In Chiết Xuất Thông Tin Đối Tác Kinh Doanh", windowSize);
                        }
                    } else {
                        alert('Hãy chọn cách in.!');
                    }
                }
            }
            , {
                id: "btn-print-close",
                text: "Thoát",
                click: function() {
                    $(this).dialog("destroy").remove();
                }
            }
            ]
        });
    }
	
	function chosen_qgkv(id) {
		var quocgia = $('#tenqg','#' + id);
		var khuvuc = $('#tenkv','#' + id);
		if(quocgia.attr('id') != null & khuvuc.attr('id') != null) {
			quocgia.off('change');
			khuvuc.prop('disabled', true);
			quocgia.change(function() {
				var val_kv = $(this).children('option:selected').attr('kv');
				if(val_kv != '') {
					khuvuc.val(val_kv);
				}
			});
			quocgia.change();
		}
		else {
			setTimeout(function(){ chosen_qgkv(id); }, 10);
		}
	}
	
	function loading() {
		$('#DelTbl_grid_ds_doitackinhdoanh').prepend('<div style="display:block" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>');
	}
	
	function end_loading() {
		 $("#wait").css("display", "none");
	}
    </script>
    
    <DIV class="ui-layout-north ui-widget-content" id="layout-north-dtkd">
    <uc1:jqGrid  ID="grid_ds_doitackinhdoanh" 
            SortName="ma_dtkd" 
            UrlFileAction="Controllers/PartnerController.ashx" 
            Caption="Đối tác kinh doanh"
            FilterToolbar="true"
			BtnPrint="funcPrint"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            RowNumbers="true"
            OndblClickRow = "
				function(rowid) { 
					$('#edit_grid_ds_doitackinhdoanh').click();
				}
			"
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_nguoilienhe').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_nguoilienhe').jqGrid('setGridParam',{url:'Controllers/NguoiLienHeController.ashx?&md_doitackinhdoanh_id='+ids,page:1});
				                $('#grid_ds_nguoilienhe').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_nguoilienhe').jqGrid('setGridParam',{url:'Controllers/NguoiLienHeController.ashx?&md_doitackinhdoanh_id='+ids,page:1});
			                $('#grid_ds_nguoilienhe').jqGrid().trigger('reloadGrid');			
		                } }"
            ColModel = "[
            {
                fixed: true, name: 'md_doitackinhdoanh_id'
                , label: 'dtkd.md_doitackinhdoanh_id'
                , index: 'dtkd.md_doitackinhdoanh_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'tenloai'
                 , label: 'Loại Đối Tác'
                 , index: 'ldt.ten_loaidtkd'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CategoryPartnerController.ashx?action=getoption' }
                 , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'ma_dtkd'
                , label: 'Mã Đối Tác'
                , index: 'dtkd.ma_dtkd'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'ten_dtkd'
                , label: 'Tên Đối Tác'
                , index: 'dtkd.ten_dtkd'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'daidien'
                , label: 'Đại Diện'
                , index: 'dtkd.daidien'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'chucvu'
                , label: 'Chức Vụ'
                , index: 'dtkd.chucvu'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'tel'
                , label: 'Điện Thoại'
                , index: 'dtkd.tel'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'fax'
                , label: 'Fax'
                , index: 'dtkd.fax'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'email'
                , label: 'Email'
                , index: 'dtkd.email'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'emailkt'
                , label: 'Email (không giá FOB)'
                , index: 'dtkd.emailkt'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                fixed: true, name: 'url'
                , label: 'Website'
                , index: 'dtkd.url'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            
            {
                fixed: true, name: 'diachi'
                , label: 'Địa Chỉ'
                , index: 'dtkd.diachi'
                , width: 100
                , edittype: 'text'
                , editable:true
            },
            {
                 fixed: true, name: 'tenqg'
                 , label: 'Quốc Gia'
                 , index: 'qg.ten_quocgia'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CountryController.ashx?action=getoption' }
                 , hidden: true
                 , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'tenkv'
                , label: 'Khu Vực'
                , index: 'kv.ten_khuvuc'
                , width: 100
                , edittype: 'select'
				, editoptions: { dataUrl: 'Controllers/SaleRegionController.ashx?action=getoption' }
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'banggia'
                , label: 'Bảng giá mặc định'
                , index: 'banggia'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/PriceListController.ashx?action=getoptionpricesale' }
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'so_taikhoan'
                , label: 'Số Tài Khoản'
                , index: 'dtkd.so_taikhoan'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'nganhang'
                , label: 'Ngân Hàng'
                , index: 'dtkd.nganhang'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'masothue'
                , label: 'Mã Số Thuế'
                , index: 'dtkd.masothue'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'tong_congno'
                , label: 'Tổng Công Nợ'
                , index: 'dtkd.tong_congno'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editoptions:{ defaultValue: '0' }
                , editrules:{ required: true, number: true }
            },
            {
                fixed: true, name: 'isncc'
                , label: 'Là Nhà Cung Cấp'
                , index: 'dtkd.isncc'
                , width: 100
                , edittype: 'checkbox'
                , editable:true
                , formatter : 'checkbox'
                , align: 'center'
				, editoptions:{ value:'True:False', defaultValue: 'False' }
            },
            {
                 fixed: true, name: 'md_cangbien_id'
                 , label: 'Thuộc cảng biển'
                 , index: 'cangbien'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PortController.ashx?action=getoption' }
                 , hidden: false
                 , editrules:{ edithidden:true }
            },
			{
                 fixed: true, name: 'md_nguondtkd_id'
                 , label: 'Nguồn đối tác'
                 , index: 'ndt.ten_nguondtkd'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/NguonDoiTacController.ashx?action=getoption' }
                 , hidden: false
                 , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'ngaytao'
                , label: 'Ngày Tạo'
                , index: 'dtkd.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , label: 'Người Tạo'
                , index: 'dtkd.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , label: 'Ngày Cập Nhật'
                , index: 'dtkd.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , label: 'Người Cập Nhật'
                , index: 'dtkd.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , label: 'Mô tả'
                , index: 'dtkd.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , label: 'Hoạt động'
                , index: 'hoatdong'
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
                width:400,
                beforeShowForm: function (formid) {
                    chosen_qgkv(formid.attr('id'));
					formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
				errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }, 
				afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
            EditFormOptions ="
                width:400,
                beforeShowForm: function (formid) {
                    chosen_qgkv(formid.attr('id'));
					formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                },
				afterSubmit: function(rs, postdata){
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
            DelFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                }
                ,beforeSubmit: function(rs, postdata) {
                    loading();
					return [true];
                },
                afterSubmit: function(rs, postdata){
					end_loading();
                    return showMsgDialog(rs);
                }
            "
            runat="server" />
            </DIV>
            
<DIV class="ui-layout-center ui-widget-content" id="lay-center-dtkd" style="padding:0;">

    <uc1:jqGrid  ID="grid_ds_nguoilienhe" 
            SortName="cl.ten" 
            UrlFileAction="Controllers/NguoiLienHeController.ashx" 
            Caption="Người liên hệ"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 70);"
            ColNames="['lh.md_nguoilienhe_id', 'Đối tác kinh doanh', 'Đối tác liên quan', 'Mức hoa hồng','Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "function(rowid) { $(this).jqGrid('editGridRow', rowid); }"
            ColModel = "[
            {
                fixed: true, name: 'md_nguoilienhe_id'
                , index: 'cl.md_nguoilienhe_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'ma_dtkd'
                 , index: 'dt.md_doitackinhdoanh_id'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , hidden: true
                 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoption&isncc=0' }
            },
            {
                 fixed: true, name: 'doitaclienquan'
                 , index: 'doitaclienquan'
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PartnerController.ashx?action=getoptionhh' }
            },
            {
                fixed: true, name: 'muchoahong'
                , index: 'cl.muchoahong'
                , width: 100
                , editable:true
                , edittype: 'text'
                , editoptions:{ defaultValue: '0' }
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
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'cl.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'cl.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
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
                , index: 'hoatdong'
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
                        var masterId = $('#grid_ds_doitackinhdoanh').jqGrid('getGridParam', 'selrow');
                        var forId = 'md_doitackinhdoanh_id';
                        if(masterId == null)
                        {
                            $(formid).parent().parent().parent().dialog('destroy').remove();
                            alert('Hãy chọn một đơn hàng mới có thể tạo chi tiết.!');
                        }else{
                            formid.closest('div.ui-jqdialog').dialogCenter();
                        }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_doitackinhdoanh').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_doitackinhdoanh_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một đơn hàng mới có thể tạo chi tiết.!!'];
                    }else{
                        postdata.md_doitackinhdoanh_id = masterId;
                        return [true,'']; 
                    }
                }
                , afterSubmit: function(rs, postdata){
                   return showMsgDialog(rs);
                }
                "
            EditFormOptions ="
                afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                },
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

<script type="text/javascript">
    createRightPanel('grid_ds_doitackinhdoanh');
    createRightPanel('grid_ds_nguoilienhe');
</script>