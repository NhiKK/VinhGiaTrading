<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CongNoPhaiThu.aspx.cs" Inherits="ReportWizard_RptKhachHang_CongNoPhaiThu" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    string tungay = Context.Request.Form["tungay"].ToString();
    string denngay = Context.Request.Form["denngay"].ToString();
    string md_doitackinhdoanh_id = Context.Request.Form["ncu"].ToString();

    String sel = String.Format(@"
        declare @tungay datetime = convert(datetime, '{0} 00:00:00', 103);
        declare @denngay datetime = convert(datetime, '{1} 23:59:59', 103);

        select dt.ma_dtkd, coalesce(SUM(civ.totalgross), 0) as tonginvoice, 
            coalesce(sum(civ.tiendatcoc + civ.tiendatra), 0) as dathanhtoan, 
            coalesce(SUM(tienconlai), 0) as tienconlai
        from c_packinginvoice civ, md_doitackinhdoanh dt
        where civ.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
            and civ.ngaylap >= @tungay
            and civ.ngaylap <= @denngay
            {2}                   
            and (civ.thanhtoanxong <> 1 or civ.thanhtoanxong is null)
        group by dt.ma_dtkd
    ", 
    tungay, 
    denngay, 
    md_doitackinhdoanh_id.Equals("NULL") ? "" : "and civ.md_doitackinhdoanh_id ='"+md_doitackinhdoanh_id+"'");
    DataTable dt = mdbc.GetData(sel);
%>
<style>
    table 
    {
    	border-collapse:collapse;
    }
    	
    table tr th
    {
    	padding:5px;
    	border: 1px solid black;
    }
</style>
<div style="background:white">
    <table style="width:100%; text-align:center">
        <tr>
            <td>
                <!--<img src="images/VINHGIA_logo_print.png" width="80" height="50"/> -->
            </td>
            <td>
                <b style="font-size:15px">VINH GIA COMPANY LIMITED</b><br />
                Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam <br />
                Tel: (84-235) 3567393   Fax: (84-235) 3567494
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <b style="font-size:18px; text-transform:uppercase">Bảng Tổng Hợp Tình Hình Công Nợ Phải Thu</b><br />
            </td>
        </tr>
    </table>
	<table style="width:100%">
		<tr>
			<td></td>
			<td></td>
			<td>Núi Thành, Ngày</td>
			<td></td>
		</tr>
		<tr>
			<td>Từ ngày</td>
			<td></td>
			<td>Đến Ngày</td>
			<td></td>
		</tr>
	</table>
	
	<table class="content" style="width:100%">
		<tr>
			<td></td>
			<td></td>
			<td></td>
			<td>ĐVT: USD</td>
			<td></td>
		</tr>
		<tr>
			<th>STT</th>
			<th>Khách hàng</th>
			<th>Tổng giá trị tiền hàng đã xuất</th>
			<th>Tổng giá trị đã thanh toán</th>
			<th>Còn lại phải thu</th>
		</tr>
		<%foreach (DataRow item in dt.Rows)
        {%>
        <tr>
			<td><%=item[0] %></td>
			<td><%=item[1] %></td>
			<td><%=item[2] %></td>
			<td><%=item[3] %></td>
			<td><%=item[4] %></td>
		</tr>
        <%}%>
		
	</table>
	<div><b>Ghi chú:</b></div>
	<div>Số liệu có thể có sai số nhỏ do tất cả đều chuyển đổi từ các loại tiền khác về USD</div>
	<table style="width:100%">
		<tr>
			<td>Giám đốc</td>
			<td>Kế toán trưởng</td>
			<td>Thủ quỹ</td>
			<td>Người lập</td>
		</tr>
		<tr>
			<td><i>Ký và ghi rõ họ tên</i></td>
			<td><i>Ký và ghi rõ họ tên</i></td>
			<td><i>Ký và ghi rõ họ tên</i></td>
			<td><i>Ký và ghi rõ họ tên</i></td>
		</tr>
	</table>
</div>