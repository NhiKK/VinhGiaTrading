<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-doitacnghiepvu.aspx.cs" Inherits="inc_doitacnghiepvu" %>
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
                    $("#grid_ds_doitackinhdoanhnv").setGridHeight(o - 90);
                }
                },
                center: {
                    onresize_end: function() {
                        var o = $("#lay-center-dtkd").height();
                        $("#grid_ds_nguoilienhenv").setGridHeight(o - 70);
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
							window.open(printURL + "?c_donhang_id=" + poId + "&where_ex=and ldt.ma_loaidtkd = 'KH'", "In Chiết Xuất Thông Tin Đối Tác Kinh Doanh", windowSize);
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
	
	function chosen_qgkvdtnv(id, action) {
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

			if(action == 'edit') {
				quocgia.prop('disabled', true);
				$('#diachi','#' + id).prop('disabled', true);
			}
			else {
				quocgia.prop('disabled', false);
				$('#diachi','#' + id).prop('disabled', false);
				quocgia.change();
			}
		}
		else {
			setTimeout(function(){ chosen_qgkvdtnv(id, action); }, 10);
		}
	}
	
	function loading() {
		$('#DelTbl_grid_ds_doitackinhdoanhnv').prepend('<div style="display:block" id="wait"><img style="width:30px; height:30px" src="iconcollection/loading.gif"/></div></h3>');
	}
	
	function end_loading() {
		 $("#wait").css("display", "none");
	}
	//BtnPrint="funcPrint"
    </script>
    
    <DIV class="ui-layout-north ui-widget-content" id="layout-north-dtkd">
    <uc1:jqGrid  ID="grid_ds_doitackinhdoanhnv" 
            SortName="ma_dtkd" 
            UrlFileAction="Controllers/PartnerNghiepVuController.ashx" 
            Caption="Đối tác kinh doanh"
            FilterToolbar="true"
			
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height() - 90);"
            ColNames="['dtkd.md_doitackinhdoanh_id', 'Loại Đối Tác', 'Mã Đối Tác','Tên Đối Tác','Đại Diện','Chức Vụ','Điện Thoại','Fax','Email','Website','Địa Chỉ', 'Quốc Gia', 'Khu Vực', 'Bảng giá mặc định', 'Số Tài Khoản','Ngân Hàng','Mã Số Thuế','Tổng Công Nợ','Là Nhà Cung Cấp', 'Thuộc cảng biển', 'Nguồn đối tác', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            RowNumbers="true"
            OndblClickRow = "
				function(rowid) { 
					// $(this).jqGrid('editGridRow', rowid, {
                        // width:400,
                        // afterSubmit: function(rs, postdata){
                            // return showMsgDialog(rs);
                        // }
                        // , beforeShowForm: function (formid) {
                            // formid.closest('div.ui-jqdialog').dialogCenter(); 
                        // }
                        // , errorTextFormat:function(data) { 
                            // return 'Lỗi: ' + data.responseText 
                        // }
                    // });
					$('#edit_grid_ds_doitackinhdoanhnv').click();
				}
			"
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_nguoilienhenv').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_nguoilienhenv').jqGrid('setGridParam',{url:'Controllers/NguoiLienHeController.ashx?&md_doitackinhdoanh_id='+ids,page:1});
				                $('#grid_ds_nguoilienhenv').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_nguoilienhenv').jqGrid('setGridParam',{url:'Controllers/NguoiLienHeController.ashx?&md_doitackinhdoanh_id='+ids,page:1});
			                $('#grid_ds_nguoilienhenv').jqGrid().trigger('reloadGrid');			
		                } }"
            ColModel = "[
            {
                fixed: true, name: 'md_doitackinhdoanh_id'
                , index: 'dtkd.md_doitackinhdoanh_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                 fixed: true, name: 'tenloai'
                 , index: 'ldt.ten_loaidtkd'
				 , formoptions:{label:'Loại Đối Tác ' + ip_fd()}
                 , editable: true
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PartnerNghiepVuController.ashx?action=getop_loaidtkd' }
                 , editrules:{ edithidden:true, required: true }
            },
            {
                fixed: true, name: 'ma_dtkd'
                , index: 'dtkd.ma_dtkd'
                , width: 100
				, formoptions:{ label:'Mã Đối Tác ' + ip_fd() }
				, editoptions: { maxlength: 20 }
                , edittype: 'text'
                , editable:true
				, editrules:{ edithidden:false, required: true }
            },
            {
                fixed: true, name: 'ten_dtkd'
                , index: 'dtkd.ten_dtkd'
                , width: 100
				, formoptions:{label:'Tên Đối Tác ' + ip_fd()}
                , edittype: 'text'
                , editable:true
				, editrules:{ edithidden:false, required: true }
            },
            {
                fixed: true, name: 'daidien'
                , index: 'dtkd.daidien'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'chucvu'
                , index: 'dtkd.chucvu'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'tel'
                , index: 'dtkd.tel'
                , width: 100
				, formoptions:{label:'Điện Thoại ' + ip_fd()}
                , edittype: 'text'
                , editable:true
				, editrules:{ edithidden:false, required: true }
            },
            {
                fixed: true, name: 'fax'
                , index: 'dtkd.fax'
                , width: 100
                , formoptions:{label:'Fax ' + ip_fd()}
				, edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true, required: true }
            },
            {
                fixed: true, name: 'email'
                , index: 'dtkd.email'
                , width: 100
                , formoptions:{label:'Email ' + ip_fd()}
				, edittype: 'text'
                , editable:true
				, editrules:{ edithidden:false, required: true }
            },
            {
                fixed: true, name: 'url'
                , index: 'dtkd.url'
                , width: 100
                , edittype: 'text'
                , editable:true
                , hidden: true
                , editrules:{ edithidden:true }
            },
            
            {
                fixed: true, name: 'diachi'
                , index: 'dtkd.diachi'
                , width: 100
                , formoptions:{label:'Địa chỉ ' + ip_fd()}
				, edittype: 'text'
                , editable:true
				, editrules:{ edithidden:false, required: true }
            },
            {
                 fixed: true, name: 'tenqg'
                 , index: 'tenqg'
                 , editable: true
                 , width: 100
				 , formoptions:{label:'Quốc gia ' + ip_fd()}
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CountryController.ashx?action=getoption' }
                 , hidden: true
                 , editrules:{ edithidden:true, required: true }
            },
            {
                fixed: true, name: 'tenkv'
                , index: 'tenkv'
                , width: 100
                , edittype: 'select'
                , editable:true
                , editoptions: { dataUrl: 'Controllers/SaleRegionController.ashx?action=getoption' }
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'banggia'
                , index: 'banggia'
                , width: 100
                , edittype: 'select'
                , editable:false
                , editoptions: { dataUrl: 'Controllers/PriceListController.ashx?action=getoptionpricesale' }
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'so_taikhoan'
                , index: 'dtkd.so_taikhoan'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'nganhang'
                , index: 'dtkd.nganhang'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'masothue'
                , index: 'dtkd.masothue'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
            },
            {
                fixed: true, name: 'tong_congno'
                , index: 'dtkd.tong_congno'
                , width: 100
                , edittype: 'text'
                , editable:false
                , editoptions:{ defaultValue: '0' }
                , editrules:{ required: true, number: true }
            },
            {
                fixed: true, name: 'isncc'
                , index: 'dtkd.isncc'
                , width: 100
                , edittype: 'checkbox'
                , editable:false
                , formatter : 'checkbox'
                , align: 'center'
				, editoptions:{ value:'True:False', defaultValue: 'False' }
            },
            {
                 fixed: true, name: 'md_cangbien_id'
                 , index: 'cangbien'
                 , editable: false
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/PortController.ashx?action=getoption' }
                 , hidden: false
                 , editrules:{ edithidden:true }
            },
			{
                 fixed: true, name: 'md_nguondtkd_id'
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
                , index: 'dtkd.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'dtkd.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'dtkd.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'dtkd.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
                , hidden: true
            },
            {
                fixed: true, name: 'mota'
                , index: 'dtkd.mota'
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
                width:400,
                beforeShowForm: function (formid) {
					chosen_qgkvdtnv(formid.attr('id'));
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                },
				beforeSubmit:function(postdata, formid){
                    var chk_ma_dtkd = chk_spec($('#ma_dtkd', '#' + formid.attr('id')).val(), 'Mã đối tác');
					if(chk_ma_dtkd != '') {
						return [false, chk_ma_dtkd];
					}
					else {
						return [true];
					}
                },
                afterSubmit: function(rs, postdata){
                    return showMsgDialog(rs);
                }
            "
            EditFormOptions ="
                width:400,
                beforeShowForm: function (formid) {
					chosen_qgkvdtnv(formid.attr('id'), 'edit');
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                errorTextFormat:function(data) { 
                    return 'Lỗi: ' + data.responseText 
                },
				beforeSubmit:function(postdata, formid){
                    var chk_ma_dtkd = chk_spec($('#ma_dtkd', '#' + formid.attr('id')).val(), 'Mã đối tác');
					if(chk_ma_dtkd != '') {
						return [false, chk_ma_dtkd];
					}
					else {
						return [true];
					}
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
                },
				beforeSubmit: function(rs, postdata) {
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

    <uc1:jqGrid  ID="grid_ds_nguoilienhenv" 
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
                        var masterId = $('#grid_ds_doitackinhdoanhnv').jqGrid('getGridParam', 'selrow');
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
                    var masterId = $('#grid_ds_doitackinhdoanhnv').jqGrid('getGridParam', 'selrow');
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
    createRightPanel('grid_ds_doitackinhdoanhnv');
    createRightPanel('grid_ds_nguoilienhenv');
</script>