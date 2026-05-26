<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-hanghoa-docquyen.aspx.cs" Inherits="inc_hanghoa_docquyen" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register Src="cdm_doitackinhdoanh_DQ.ascx" TagName="Cbm" TagPrefix="uc3" %>
<script>
    $(function() {
        $('#lay-center-hanghoadocquyen').parent().layout({
            north: {
                size: "50%"
                , onresize_end: function() {
                    var o = $("#lay-north-hanghoadocquyen").height();
                    $("#grid_ds_khachhangdocquyen").setGridHeight(o - 90);

                }
            }
            , center: {
                onresize_end: function() {
                    var o = $("#lay-center-hanghoadocquyen").height();
                    $("#grid_ds_hanghoadocquyen").setGridHeight(o - 90);
                }
            }
        });
    });
	
	<% 
    LinqDBDataContext db = new LinqDBDataContext();
    this.doitackinhdoanh.Name = "txtDoiTacKH";
    this.doitackinhdoanh.isncc = false;
    this.doitackinhdoanh.NullFirstItem = true;
    %>
	
	function funcPrintNT() {
        var xkId = $('#grid_ds_khachhangdocquyen').jqGrid('getGridParam', 'selrow');
        var name = 'dialog-print-nk';
        var title = 'Trích xuất dữ liệu';
        var content = `<table style="width:100%">
							<!--<tr>
                                <td>
                                </td>

                                <td>
                                    <input id="printRequired" type="radio" name="rdoPrintType" value="printRequired" />
                                    <label for="printRequired">Chiết xuất hàng hóa độc quyền - Chọn khách hàng</label>
									
                                </td>
                            </tr>-->
							
							<tr>
                                <td>
                                </td>
                                <td>
                                    <input id="printExcel" type="radio" name="rdoPrintType" value="printExcel" checked="checked" />
                                    <label for="printExcel">Chiết xuất hàng hóa độc quyền - Chọn khách hàng</label>
									<uc3:Cbm id="doitackinhdoanh" runat="server"/>
                                </td>
                            </tr>
                      </table>`;

        $('body').append('<div title="' + title + '" id="' + name + '">' + content + '</div>');
        $('#' + name).dialog({
            modal: true
            , open: function (event, ui) {
                //hide close button.
                $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
            }
            , buttons: [
            {
                id: "btn-print-ok",
                text: "In",
                click: function () {
					var doitackinhdoanh_id = $("#txtDoiTacKH").val();
                    var printType = $('input[name=rdoPrintType]:checked', '#dialog-print-nk').val();
                    var printURL = "PrintControllers/";
                    var windowSize = "width=700,height=700,scrollbars=yes";
                    if (typeof printType != 'undefined') {
                        if (printType == "printRequired") {
                            var postfilt = $('#grid_ds_khachhangdocquyen').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InCXHangHoaDocQuyen/";
                            window.open(printURL + "?md_hanghoadocquyen_id=" + xkId + "&doitackinhdoanh_id=" + doitackinhdoanh_id + "&filters=" + postfilt , "Chiết xuất hàng hóa độc quyền", windowSize);
                        }
						else {
							var postfilt = $('#grid_ds_khachhangdocquyen').jqGrid('getGridParam', 'postData').filters;
                            printURL += "InCXHangHoaDocQuyen/InHHDQ.aspx";
                            window.open(printURL + "?md_hanghoadocquyen_id=" + xkId + "&doitackinhdoanh_id=" + doitackinhdoanh_id + "&filters=" + postfilt, "Chiết xuất hàng hóa độc quyền", windowSize);
						}
                    } else {
                        alert('Hãy chọn cách in.!');
                    }
                }
            }
            , {
                id: "btn-print-close",
                text: "Thoát",
                click: function () {
                    $(this).dialog("destroy").remove();
                }
            }
            ]
        });
    }
</script>
<DIV class="ui-layout-north ui-widget-content" id="lay-north-hanghoadocquyen">    
     <uc1:jqGrid  ID="grid_ds_khachhangdocquyen" 
            Caption="Khách Hàng"
            FilterToolbar = "true"
            SortName="ma_dtkd"
            SortOrder="asc"
            UrlFileAction="Controllers/PartnerCopyRightController.ashx"
            PostData = "'isncc' : '0'"
			BtnPrint="funcPrintNT"
            ColNames="['dtkd.md_doitackinhdoanh_id', 'Loại Đối Tác', 'Mã Đối Tác','Tên Đối Tác','Đại Diện','Chức Vụ','Điện Thoại','Fax','Email','Website','Địa Chỉ', 'Quốc Gia', 'Khu Vực', 'Bảng giá mặc định', 'Số Tài Khoản','Ngân Hàng','Mã Số Thuế','Tổng Công Nợ','Là Nhà Cung Cấp', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Mô tả', 'Hoạt động']"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            RowNumbers="true"
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
                 , index: 'tenloai'
                 , editable:false
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CategoryPartnerController.ashx?action=getoption' }
                 , hidden: true
                 , editrules:{ edithidden:true }
                 , search: true
            },
            {
                fixed: true, name: 'ma_dtkd'
                , index: 'dtkd.ma_dtkd'
                , width: 100
                , edittype: 'select'
                , editoptions: { dataUrl: 'Controllers/PartnerCopyRightController.ashx?action=getoption' }
                , editable: true
                , search: true
            },
            {
                fixed: true, name: 'ten_dtkd'
                , index: 'dtkd.ten_dtkd'
                , width: 200
                , edittype: 'text'
                , editable:false
                , search: true
            },
            {
                fixed: true, name: 'daidien'
                , index: 'dtkd.daidien'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            {
                fixed: true, name: 'chucvu'
                , index: 'dtkd.chucvu'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            {
                fixed: true, name: 'tel'
                , index: 'dtkd.tel'
                , width: 100
                , edittype: 'text'
                , editable:false
                , search: true
            },
            {
                fixed: true, name: 'fax'
                , index: 'dtkd.fax'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            {
                fixed: true, name: 'email'
                , index: 'dtkd.email'
                , width: 300
                , edittype: 'text'
                , editable:false
                , search: true
            },
            {
                fixed: true, name: 'url'
                , index: 'dtkd.url'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            
            {
                fixed: true, name: 'diachi'
                , index: 'dtkd.diachi'
                , width: 500
                , edittype: 'text'
                , editable:false
                , search: true
            },
            {
                 fixed: true, name: 'tenqg'
                 , index: 'tenqg'
                 , editable:false
                 , width: 100
                 , edittype: 'select'
                 , editoptions: { dataUrl: 'Controllers/CountryController.ashx?action=getoption' }
                 , hidden: true
                 , editrules:{ edithidden:true }
                 , search: true
            },
            {
                fixed: true, name: 'tenkv'
                , index: 'tenkv'
                , width: 100
                , edittype: 'select'
                , editable:false
                , editoptions: { dataUrl: 'Controllers/SaleRegionController.ashx?action=getoption' }
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            {
                fixed: true, name: 'banggia'
                , index: 'banggia'
                , width: 100
                , edittype: 'select'
                , editable:false
                , editoptions: { dataUrl: 'Controllers/PriceListController.ashx?action=getoption' }
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            {
                fixed: true, name: 'so_taikhoan'
                , index: 'dtkd.so_taikhoan'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            {
                fixed: true, name: 'nganhang'
                , index: 'dtkd.nganhang'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            {
                fixed: true, name: 'masothue'
                , index: 'dtkd.masothue'
                , width: 100
                , edittype: 'text'
                , editable:false
                , hidden: true
                , editrules:{ edithidden:true }
                , search: true
            },
            {
                fixed: true, name: 'tong_congno'
                , index: 'dtkd.tong_congno'
                , width: 100
                , edittype: 'text'
                , editable:false
                , editoptions:{ defaultValue: '0' }
                , hidden: true
                , search: true
            },
            {
                fixed: true, name: 'isncc'
                , index: 'dtkd.isncc'
                , width: 100
                , edittype: 'checkbox'
                , editable:false
                , formatter : 'checkbox'
                , align: 'center'
                , hidden: true
                , search: true
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
                , editable:false
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
            AddFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                afterSubmit: function(rs, postdata) { 
                    return showMsgDialog(rs); 
                }
            "
            EditFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                afterSubmit: function(rs, postdata) { 
                    return showMsgDialog(rs); 
                }
            "
            DelFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                },
                afterSubmit: function(rs, postdata) { 
                    return showMsgDialog(rs); 
                }
            "
            ViewFormOptions="
                beforeShowForm: function (formid) {
                    formid.closest('div.ui-jqdialog').dialogCenter(); 
                }
            "
            OnSelectRow = "function(ids) {
		                if(ids == null) {
			                ids=0;
			                if($('#grid_ds_hanghoadocquyen').jqGrid('getGridParam','records') &gt; 0 )
			                {
				                $('#grid_ds_hanghoadocquyen').jqGrid('setGridParam',{url:'Controllers/ProductByPartnerController.ashx?&partnerId='+ids,page:1});
				                $('#grid_ds_hanghoadocquyen').jqGrid().trigger('reloadGrid');
				            } 
				        } else {
			                $('#grid_ds_hanghoadocquyen').jqGrid('setGridParam',{url:'Controllers/ProductByPartnerController.ashx?&partnerId='+ids,page:1});
			                $('#grid_ds_hanghoadocquyen').jqGrid().trigger('reloadGrid');			
		                } }"
		                
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            runat="server" />
</DIV>
<DIV class="ui-layout-center ui-widget-content" id="lay-center-hanghoadocquyen">
    <uc1:jqGrid  ID="grid_ds_hanghoadocquyen"
            FilterToolbar = "true"
            Caption="Danh Sách Hàng Hóa Độc Quyền" 
            SortName="md_hanghoadocquyen_id" 
            UrlFileAction="Controllers/ProductByPartnerController.ashx" 
            ColNames="['md_hanghoadocquyen_id' ,'Tên Đối Tác', 'Mã SP', 'Mã SP', 'Ngày Tạo', 'Người Tạo', 'Ngày Cập Nhật', 'Người Cập Nhật', 'Thị Trường Độc Quyền', 'Hoạt động']"
            RowNumbers="true"
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
                fixed: true, name: 'md_hanghoadocquyen_id'
                , index: 'hhdq.md_hanghoadocquyen_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
                , key: true
            },
            {
                fixed: true, name: 'md_doitackinhdoanh_id'
                , index: 'dtkd.md_doitackinhdoanh_id'
                , width: 100
                , edittype: 'text'
                , editable: false
                , hidden: true
            },
            {
                fixed: true, name: 'sanpham_id'
                , index: 'sp.sanpham_id'
                , width: 100
                , hidden: true 
                , editable:true
                , edittype: 'text'
            },
            {
                fixed: true, name: 'tensp'
                , index: 'sp.ma_sanpham'
                , width: 100
                , edittype: 'text'
                , editable:true
                , editoptions: { 
                    dataInit : function (elem) { 
                        $(elem).combogrid({
                            searchIcon: true,
                            width: '480px',
                            url: 'Controllers/ProductController.ashx?action=getcombogrid',
                            colModel: [
                                { 'columnName': 'md_sanpham_id', 'width': '0', 'label': 'ID', hidden:true }
                              , { 'columnName': 'ma_sanpham', 'width': '50', 'label': 'Mã SP', 'align':'left'}
                              , { 'columnName': 'mota_tiengviet', 'width': '50', 'label': 'Mô Tả TV' , 'align':'left'}],
                              select: function(event, ui) {
                                    $(elem).val(ui.item.ma_sanpham);
                                    $('#sanpham_id').val(ui.item.md_sanpham_id);
                                    return false;
                              }
                        });
                    } 
                }
            },
            {
                fixed: true, name: 'ngaytao'
                , index: 'hhdq.ngaytao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoitao'
                , index: 'hhdq.nguoitao'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'ngaycapnhat'
                , index: 'hhdq.ngaycapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'nguoicapnhat'
                , index: 'hhdq.nguoicapnhat'
                , width: 100
                , editable:false
                , edittype: 'text'
            },
            {
                fixed: true, name: 'mota'
                , index: 'hhdq.mota'
                , width: 100
                , editable:true
                , edittype: 'textarea'
            },
            {
                fixed: true, name: 'hoatdong', hidden: true
                , index: 'hhdq.hoatdong'
                , width: 100
                , editable:true
                , edittype: 'checkbox'
                , align: 'center'
                , editoptions:{ value:'True:False', defaultValue: 'True' }
                , formatter: 'checkbox'
            }
            ]"
            GridComplete = "var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight($(o).height()- 90);"
            Height = "420"
            ShowAdd ="<%$ Code: DaoRules.GetRuleAdd(Context).ToString().ToLower() %>"
            ShowEdit ="<%$ Code: DaoRules.GetRuleEdit(Context).ToString().ToLower() %>"
            ShowDel = "<%$ Code: DaoRules.GetRuleDel(Context).ToString().ToLower() %>"
            AddFormOptions=" 
                errorTextFormat:function(data) { 
                        return 'Lỗi: ' + data.responseText 
                }
                ,beforeShowForm: function (formid) {
                    var masterId = $('#grid_ds_khachhangdocquyen').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_doitackinhdoanh_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        alert('Hãy chọn một khách hàng mới có thể tạo chi tiết.!');
                    }else{
                        $('#tr_' + forId + ' td.DataTD input#' + forId, formid).val(masterId);
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                }
                , beforeSubmit:function(postdata, formid){
                    var masterId = $('#grid_ds_khachhangdocquyen').jqGrid('getGridParam', 'selrow');
                    var forId = 'md_doitackinhdoanh_id';
                    if(masterId == null)
                    {
                        $(formid).parent().parent().parent().dialog('destroy').remove();
                        return [false,'Hãy chọn một khách hàng mới có thể tạo chi tiết.!'];
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

<script>	
	if(chietxuatHHDQ.toLowerCase() == 'false') {
		var parent = $('#grid_ds_khachhangdocquyen-pager_left');
		parent.find('.ui-icon.ui-icon-print').parent().parent().remove();
	}

	createRightPanel('grid_ds_khachhangdocquyen');
	createRightPanel('grid_ds_hanghoadocquyen');
</script>