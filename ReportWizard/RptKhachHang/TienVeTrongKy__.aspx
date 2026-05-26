<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TienVeTrongKy__.aspx.cs" Inherits="ReportWizard_Rpt_NCC_TienVeTrongKy__" %>
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
    string tn = (DateTime.ParseExact(tungay, "dd/MM/yyyy", null)).ToString("MM/dd/yyyy");
    string dn = (DateTime.ParseExact(denngay, "dd/MM/yyyy", null)).ToString("MM/dd/yyyy");


    string sql = @"select ct.ngaylapphieu, ct.sophieu, ct.lydo, ct.sotien, ct.tongcackhoan, ct.tongkhoanphi, ct.mota 
                    from c_thuchi ct
                    where 1=1 ";
    sql += " and ngaylapphieu  >='" + tungay + "' ";
    sql += " and ngaylapphieu  <='" + denngay + "' ";
    sql += md_doitackinhdoanh_id.Equals("NULL") ? "" : "and md_doitackinhdoanh_id ='"+md_doitackinhdoanh_id+"'";
    //sql += " order by cnx.sophieu";
    string sql_gr = @"select distinct tmp.md_doitackinhdoanh_id from("+sql+") as tmp";

    DataTable d_detail = mdbc.GetData(sql);
    DataTable d_gr = mdbc.GetData(sql_gr);
    double total = 0;
    %>
<style type="text/css">
	    .tableborder{
        border-collapse:collapse;
	    }
        .tableborder td, th
        {
        border:1px solid black;
        padding: 5px;
        }
     </style>
<div style="background:white; padding:5px;">
    <table style="width:100%">
        <tr><td colspan=2 align="center" style="width:100%"><span style="font-size: 18px; font-weight:bold">VINH GIA COMPANY LIMITED</span></td> </tr>
        <tr><td colspan=2 align="center" style="width:100%"><span style="font-size: 14px; ">Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam</span></td></tr>
        <tr><td colspan=2 align="center" style="width:100%"><span style="font-size: 14px; ">Tel.: (84-235) 3567393 Fax: (84-235) 3567494</span></td></tr>
        <tr><td colspan=2 style="width:100%"> </td></tr>
        <tr><td colspan=2 align="center" style="width:100%"><span style="font-size: 18px; font-weight:bold">GIÁ TRỊ TIỀN HÀNG ĐÃ NHẬP</span></td></tr>
        <tr><td colspan=2 style="width:100%"> </td></tr>
        <tr><td colspan=2 style="width:100%"> </td></tr>
        <tr><td colspan=2 style="width:100%"> </td></tr>
        <tr><td style="width:200px"></td><td><span style="font-size: 14px; ">Núi Thành, ngày:</span></td> </tr>
        <tr><td style="width:200px"><span style="font-size: 14px; ">Từ ngày: <%=tungay %></span></td><td><span style="font-size: 14px; ">Đến ngày: <%=denngay %></span></td></tr>
        
    </table>
    <%
        foreach (DataRow ig in d_gr.Rows)
        {
            
            var dtkd = db.md_doitackinhdoanhs.Single(d=>d.md_doitackinhdoanh_id.Equals(ig["md_doitackinhdoanh_id"]));   
            %>
       <table class="tableborder" style="width:100%; background:#EFEFEF;">
        <tr><td><span style="font-size: 14px; ">Tên cơ sở cung ứng:<%=dtkd.ma_dtkd %></span></td></tr>
       </table>     
        
        <table class="tableborder" style="width: 100%">
            <tr style="width: 100%;">
            <td align="center" style="width:20px; ">STT</td>
            <td align="center" style="width:80px; ">Số phiếu nhập kho</td>
            <td align="center" style="width:60px; ">Ngày lập</td>
            <td align="center" style="width:120px; ">Nhập tại kho</td>
            <td align="center" style="width:120px; ">Số phiếu khách</td>
            <td align="center" style="width:100px; ">Thành tiền</td>
            <td align="center" style="width:160px; ">Ghi chú</td>
            </tr>
            <%
            int stt = 1;
            double sum_g = 0;
            foreach (DataRow id in d_detail.Rows)
            {
                sum_g += double.Parse(id["thanhtien"].ToString());
                %>
            <tr style="width: 100%;">
            <td align="center" style="width:20px; "><%=stt %></td>
            <td align="center" style="width:80px; "><%=id["sophieu"] %></td>
            <td align="center" style="width:60px; "><%=(DateTime.Parse(id["ngay_giaonhan"].ToString())).ToString("dd/MM/yyyy")%></td>
            <td align="center" style="width:120px; "><%=id["ma_kho"]%></td>
            <td align="center" style="width:120px; "><%=id["sophieukhach"]%></td>
            <td align="right" style="width:100px; "><%=String.Format("{0:#,0}", double.Parse(id["thanhtien"].ToString()))%></td>
            <td align="center" style="width:160px; "></td>
            </tr> 
            <%
                stt++;
            }
            total += sum_g;
            %>
            
            <tr style="width: 100%;">
                <td colspan=5 align="right" style="width:100px; "><b>Cộng</b></td>
                <td align="right" style="width:100px; "><%=String.Format("{0:#,0}", sum_g)%></td>
                <td align="right" style="width:100px; "></td>
            </tr>
        </table>
        
        <%}%>
        <table>
        <tr style="width: 100%;">
                <td align="right" style="width:580px; "><b>Tổng cộng</b></td>
                <td align="right" style="width:125px; "><%=String.Format("{0:#,0}", total)%></td>
            </tr>
        </table>    
</div>