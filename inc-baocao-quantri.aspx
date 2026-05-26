<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-baocao-quantri.aspx.cs" Inherits="inc_baocao_quantri" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<% 
    LinqDBDataContext dbc = new LinqDBDataContext();
    String manv = UserUtils.getUser(Context);
    nhanvien nv = dbc.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
    String sql = String.Format("select * from md_baocao bc, md_phanquyenbaocao pqbc where bc.md_baocao_id = pqbc.md_baocao_id and bc.hoatdong = 1 AND pqbc.manv = @manv AND bc.md_module_id='BAOCAOQT'");
    String sqlAdmin = "select * from md_baocao where md_module_id='BAOCAOQT' and hoatdong = 1";
    System.Data.DataTable dt = new System.Data.DataTable();

    if (nv.isadmin.Value)
    {
        dt = mdbc.GetData(sqlAdmin);
    }
    else {
        dt = mdbc.GetData(sql, "@manv", manv);
    }
%>
<script>
    $(document).ready(function() {
        $('#lay-center-baocaoquantri').parent().layout({
            west: {
                size: "20%"
            }
        });
        $('#lay-west-baocaoquantri button').button().css({ 'width': '95%' });

       
    });

    function submitQuanTri() {
        var rptId = eval($("#rptQTId").val());
        switch (rptId) {
            case 1:
                var namtc = $("#nam").val();
                var startdate = "01/01/" + namtc;
                var enddate = "12/31/" + namtc;
                window.open("PrintControllers/InBaoCaoQuanTri/?startdate=" + startdate + "&enddate=" + enddate);
                break;
            default:
                alert('Không tìm thấy hàm!');
                break;
        }
    }

    function actionQuanTri(title, url, startdate, enddate, doitackinhdoanh_id) {
        var rptURL = url + "?startdate=" + startdate + "&enddate=" + enddate;
        if(doitackinhdoanh_id != "NULL") {
            rptURL += "&doitackinhdoanh_id=" + doitackinhdoanh_id;
        }

        window.open(rptURL);
    }

    function createFilterQT(type) {
        $("#filterQT").css({ "display": "" });
        $("#rptQTId").val(type);
        switch (type)
        {
            case 1:
                $('#lblReportNameQT').html("Báo cáo tổng hợp quản trị");
                break;
            default:
                $('#lblReportNameQT').html("Không xác định report");
                break;
        }

    }

 </script>

<div style="background:#F4F0EC" class="ui-layout-center ui-widget-content" id="lay-center-baocaoquantri">
    <div id="filterQT" style="display:none;">
    <input type="hidden" name="rptQTId" id="rptQTId"/>
        <table style="width:100%">
            <tr>
                <td colspan="2"><h3 style="font-size:18px; padding:5px" id="lblReportNameQT"></h3></td>
            </tr>
            <tr>
                <td >Năm</td>
                <td>
                   <select name="nam" id="nam">
                        <% for (int i = 2013; i < DateTime.Now.Year + 10; i++)
                           {%>
                              <option value="<%=i %>" <%= DateTime.Now.Year == i ? " selected=\"selected\"" :""%> ><%=i %> </option> 
                           <%} %>
                   </select>
                </td>
            </tr>
          
            <tr><td></td></tr>
            <tr>
                <td></td>
                <td><input type = "button" name="xemketquaqt" id="xemketquaqt" value="Xem kết quả" onclick="submitQuanTri()"/></td>
            </tr>
        </table>
    </div>
</div>

<div style="background:#F4F0EC;" class="ui-layout-west" id="lay-west-baocaoquantri">
    <div id="actonlist" class="ui-widget-content" style="height:100%; overflow:auto; text-align:center">
        <!--<button onclick="createFilterQT(1)" style="text-align:left;">Báo cáo tổng hợp</button>-->
        <% foreach (System.Data.DataRow item in dt.Rows)
           {%>
                <button style="text-align:left;" onclick="<%= item["hanhdong"] %>"><%= item["ten_baocao"] %></button>
        <% } %>
    </div>
</div>