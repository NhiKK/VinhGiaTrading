<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-phanbococinvoice.aspx.cs" Inherits="inc_phanbococinvoice" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Register src="jqGrid.ascx" tagname="jqGrid" tagprefix="uc1" %>
<%@ Register Src="Invoice.ascx" TagName="Cbm" TagPrefix="uc2" %>
<%@ Register Src="InvoiceHh.ascx" TagName="Cbm" TagPrefix="uc5" %>
<%@ Register Src="PO.ascx" TagName="Cbm" TagPrefix="uc3" %>
<%@ Register Src="PhieuNhap.ascx" TagName="Cbm" TagPrefix="uc4" %>

<%
    LinqDBDataContext db = new LinqDBDataContext();
    
    string chitiethuchi = Context.Request.QueryString["chitietphaithuid"];
    var obj = db.c_chitietthuchis.Single(c=> c.c_chitietthuchi_id.Equals(chitiethuchi));

    string sqlKhoanPhi = string.Format(@"select isnull(SUM(sotien), 0) as khoanphi from c_chiphilienquan ctlq
                        where ctlq.c_thuchi_id = '{1}' and c_donhang_id IN(
	                        select c_donhang_id from c_chitietthuchi 
	                        where c_chitietthuchi_id = '{0}'
                        )", obj.c_chitietthuchi_id, obj.c_thuchi_id );

    decimal sumKhoanPhi = (decimal)mdbc.ExecuteScalar(sqlKhoanPhi);
    
    string sql = @" select coalesce(sum(sotienphanbo), 0)
                    from c_dongphanboinv 
                    where c_chitietthuchi_id = '"+chitiethuchi+"'";    
    decimal sumphanbo = (decimal)mdbc.ExecuteScalar(sql);

    decimal sotiencothephanbo = (obj.sotien.Value + sumKhoanPhi) - sumphanbo;

 %>
<form id="f_phanbo">
<input type="hidden" name="cttc" id="cttc" value = "<%=chitiethuchi %>"/>
<% if (obj.loaiphieu.Equals("PO"))
   { %> 
    <div id="mesage_pb" >
        <% if (sotiencothephanbo > 0)
           {%>
                <div style="border-color:#3399FF; border-style:solid; padding:3px; background-color:#99CCFF;">
                    <span style="font-size:13px; font-style:italic; font-weight:bold;">Số tiền còn lại có thể phân bổ: <%=sotiencothephanbo.ToString("#,##.##") %> trong tổng <%=  (obj.sotien.Value + sumKhoanPhi).ToString("#,##.##") %></span>
                </div>
                
        <%}
           else
           { %>
                 <div style="border-color:#009900; border-style:solid; padding:5px;background-color:#33FF66;" >
                    <span style="font-size:13px; font-style:italic; font-weight:bold;" >Số tiền đã được phân bổ hết trong <%= (obj.sotien.Value + sumKhoanPhi).ToString("#,##.##") %></span>
                </div>
        <%} %>
    </div> 
 
    <div id="grid_phanbo" >
        <table class="ui-layout-center" id="ds_phanbo"></table>
        <div id="ds_phanbo-pager"></div>
    </div>
<%}
   else
   { %>
     <div style="border-color:#FF6600; border-style:solid; padding:5px;background-color:#FFCC66;" >
        <span style="font-size:13px; font-style:italic; font-weight:bold;" >Không thể phân bổ số tiền không phải là tiền cọc!</span>
     </div>
<%} %>

    <script language="javascript">
        $(document).ready(function() {
            var lastSel = -1;
            var cttc_ = $("#cttc").val();

            jQuery("#ds_phanbo").jqGrid({
                url: "Controllers/ChiPhanBoInvController.ashx?action=loadInvoice&po=" + $("#dtkd").val(),
                datatype: "xml",
                height: 100,
                postData: {cttc:cttc_},
                colNames: ['c_packinginvoice_id', 'Số pkl/invoice', 'Tổng tiền invoice'
                    , 'Tiền đã cọc', 'Còn lại phải trả'],
                colModel: [
                                {
                                    fixed: true, name: 'c_dongphanboinv_id'
                                    , index: 'c_dongphanboinv_id'
                                    , width: 100
                                    , editable: false
                                    , edittype: 'text'
                                    , sortable: true
                                    , hidden: true
                                    , key: true
                                },
                                {
                                    fixed: true, name: 'c_packinginvoice_id'
                                    , index: 'c_packinginvoice_id'
                                    , width: 100
                                    , edittype: 'select'
                                    , editable: true
                                    , sortable: true
                                    , search: true
                                    , editoptions: { dataUrl: 'Controllers/ChiPhanBoInvController.ashx?action=getPackingInvoice&c_chitietthuchi_id=' + cttc_ }
                                },
                                {
                                    fixed: true, name: 'totalgross'
                                    , index: 'totalgross'
                                    , width: 100
                                    , editable: true
                                    , edittype: 'text'
                                    , sortable: false
                                    , search: false
                                    , editoptions: { defaultvalue: '0', readonly: 'readonly' }
                                    , formatter: 'currency'
                                    , editrules: {number:true}
                                },
                                {
                                    fixed: true, name: 'tiendatcoc'
                                    , index: 'tiendatcoc'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: true
                                    , sortable: false
                                    , search: false
                                    , editrules: { number: true }
                                    , formatter: 'currency'
                                    , editoptions: { defaultValue: '0' }
                                },
                                {
                                    fixed: true, name: 'tiendatra'
                                    , index: 'tiendatra'
                                    , width: 100
                                    , edittype: 'text'
                                    , editable: false
                                    , sortable: false
                                    , search: false
                                    , editrules: { number: true }
                                }
                                ],
                autowidth: false,
                multiselect: false,
                caption: "Danh sách invoice được phân bổ",
                height: 150,
                width: 500,
                gridComplete: function() { var o = $(this).parent().parent().parent().parent().parent(); $(this).setGridHeight(150); },
                showRefresh: false,
                rowNumbers: true,
                cellsubmit: 'clientArray',
                //                onSelectRow: function(id) {
                //                    if (id && id !== lastSel) {
                //                        jQuery("#ds_phanbo").jqGrid('restoreRow', lastSel);
                //                        jQuery("#ds_phanbo").jqGrid('editRow', id, true, null, null, 'clientArray');
                //                        lastSel = id;
                //                    }
                //                },
                ondblClickRow: function(rowid) {
                    $(this).jqGrid('editGridRow', rowid,
                    {
                        width: 500
                    , afterSubmit: function(rs, postdata) {
                        return showMsgDialog(rs);
                    }
                    , beforeShowForm: function(formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                    , errorTextFormat: function(data) {
                        return 'Lỗi: ' + data.responseText
                    }
                    });
                },
                editurl: 'Controllers/ChiPhanBoInvController.ashx?cttc='+cttc_,
                pager: $('#ds_phanbo-pager')
            });
            $('#ds_phanbo').jqGrid('navGrid', '#ds_phanbo-pager', { add: true, edit: true, del: true, search: false, refresh: true },
                {
                    afterSubmit: function (rs, postdata) {
						$('#grid_ds_phaithuphanbo')[0].triggerToolbar();
                        return showMsgDialog(rs);
                    }
                    , beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                    , errorTextFormat: function (data) {
                        return 'Lỗi: ' + data.responseText
                    }
                }
            , {
                    afterSubmit: function (rs, postdata) {
						$('#grid_ds_phaithuphanbo')[0].triggerToolbar();
                        return showMsgDialog(rs);
                    }
                    , beforeShowForm: function (formid) {
                        formid.closest('div.ui-jqdialog').dialogCenter();
                    }
                    , errorTextFormat: function (data) {
                        return 'Lỗi: ' + data.responseText
                    }
            }, { reloadAfterSubmit: true }, {});
            $("#ds_phanbo").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });
        });
        </script>


</form>

<script language="javascript">

</script>