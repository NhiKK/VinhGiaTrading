<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-view_xemnangluc.aspx.cs" Inherits="inc_view_xemnangluc" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Newtonsoft.Json.Linq" %>

<%  
    LinqDBDataContext db = new LinqDBDataContext();
    string hehang = Context.Request.Form["hehang"];
    double cbm = double.Parse(Context.Request.Form["cbm"]);
    string ngay = Context.Request.Form["ngay"];
    var hh = db.md_nhomnanglucs.Single(h=>h.md_nhomnangluc_id.Equals(hehang));
    string sql = @"select distinct md_doitackinhdoanh_id 
                    from c_chitietnangluc 
                    where md_nhomnangluc_id = '"+hehang+@"' 
	                    and c_tuannangluc_id in (select c_tuannangluc_id from c_tuannangluc where tuanthu = (select datepart(wk, '"+ngay+@"')))
                    ";
    DataTable dt_kd = mdbc.GetData(sql);
    
 %>
<div style="padding: 5px;">
    
    <% 
        if (dt_kd.Rows.Count > 0)
        {
            foreach (DataRow item in dt_kd.Rows)
            {
                var dtkd = db.md_doitackinhdoanhs.Single(d => d.md_doitackinhdoanh_id.Equals(item["md_doitackinhdoanh_id"].ToString()));       
    %>
         <div>
            <h2> Nhà cung cấp: <%=dtkd.ma_dtkd%></h2>
            <%
        string sql_ = "select * from f_xemNangLucCungUng('" + hehang + "', " + cbm + ", '" + dtkd.md_doitackinhdoanh_id + "', '" + ngay + "')";
        DataTable rows = mdbc.GetData(sql_);
        if (rows.Rows.Count.Equals(0))
        {%>
            <div style="background: #FF7474; border: 2px; padding: 7px; width: 520px;">Không đủ dữ kiện tính năng lực!</div> 
        <%}
        else
        {

            string sql_max = "select max(thoigiandukien) from (" + sql_ + ") as tmp";
            string sql_sum = "select sum(cbm_candat) from (" + sql_ + ") as tmp";
            DateTime ngaydukien = (DateTime)mdbc.ExecuteScalar(sql_max);
            decimal sum_cbm = (decimal)mdbc.ExecuteScalar(sql_sum);
            double sum = Convert.ToDouble(sum_cbm);
            DataTable dr = mdbc.GetData(sql_);
            if (sum == cbm)
            { 
           %>
            
            <div style="background: #80FF80; border: 2px; padding: 7px; width: 520px;">
                Hệ hàng: <b><%=hh.hehang + "(" + hh.nhom + ")"%> </b>có thể đáp ứng được vào ngày:<b><%=ngaydukien.ToString("dd/MM/yyyy")%></b> 
            </div>
            <table >
                <tr style="background: green; padding: 2px;" >
                    <td style="width: 140px; font-weight: bold; padding: 2px;" align=center>Nhóm NL</td>
                    <td style="width: 140px; font-weight: bold; padding: 2px;" align=center>Số cbm đặt được</td>
                    <td style="width: 130px; font-weight: bold; padding: 2px;" align=center>Tuần thứ</td>
                    <td style="width: 100px; font-weight: bold; padding: 2px;" align=center>Ngày dự kiến</td>
                </tr>
            
            <%
        foreach (DataRow r in dr.Rows)
        {%>
                   <tr>
                    <td style="width: 140px;" align=center><%=r["dauma"]%></td>
                    <td style="width: 140px;" align=right><%=r["cbm_candat"]%></td>
                    <td style="width: 130px;" align=right><%=r["tuanthu"]%></td>
                    <td style="width: 100px;" align=center><%=(DateTime.Parse(r["thoigiandukien"].ToString())).ToString("dd/MM/yyyy")%></td>
                    </tr> 
                <%} %>
             </table>   
            
            <% }
            else
            { %>
             <div style="background: #FF7474; border: 2px; padding: 7px; width: 520px;">Các tuần năng lực hiện có không thể đáp ứng đủ số lượng cont cần xem!</div>   
                <%}%>
            
         </div>  
       <%}
            }
        }
        else
        {  %>
        <div style="background: #FF7474; border: 2px; padding: 7px; width: 520px;">Không đủ dữ kiện tính năng lực!</div>   
        <%}%>
    
</div>