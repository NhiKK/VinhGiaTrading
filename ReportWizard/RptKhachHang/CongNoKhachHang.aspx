<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CongNoKhachHang.aspx.cs" Inherits="ReportWizard_RptKhachHang_CongNoKhachHang" %>
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


    string sql = string.Format(@"
        declare @tungay datetime = convert(datetime, '{0} 00:00:00', 103);
        declare @denngay datetime = convert(datetime, '{1} 23:59:59', 103);
        select * 
        from
        (
	        select civ.ngaylap as ngay, civ.so_inv as sophieu, 'Invoice' as noidung, 
		        civ.totalgross as giatriinvoice, 0 as trigiathanhtoan, civ.md_doitackinhdoanh_id
	        from c_packinginvoice civ
	        where civ.md_trangthai_id = 'HIEULUC'
		        and civ.ngaylap >= @tungay
		        and civ.ngaylap <= @denngay {dk2}
	        union
	        select ct.ngaylapphieu as ngay, ct.sophieu as sophieu, 'Phieu thu' as noidung, 
		        0 as giatriinvoice, ct.sotien as trigiathanhtoan, ct.md_doitackinhdoanh_id
	        from c_thuchi ct
	        where ct.ngaylapphieu > = @tungay
		        and ct.ngaylapphieu <= @denngay {dk1}
        ) as tmp
        where tmp.md_doitackinhdoanh_id is not null
    ", tungay, denngay);

    if(!md_doitackinhdoanh_id.Equals("NULL")){
        sql = sql.Replace("{dk2}", " and civ.md_doitackinhdoanh_id ='" + md_doitackinhdoanh_id + "'");
        sql = sql.Replace("{dk1}", " and ct.md_doitackinhdoanh_id ='" + md_doitackinhdoanh_id + "'");
    }else{
        sql = sql.Replace("{dk2}", " ");
        sql = sql.Replace("{dk1}", " ");
    }
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
        <tr><td colspan=2 align="center" style="width:100%"><span style="font-size: 18px; font-weight:bold">CHI TIẾT CÔNG NỢ KHÁCH HÀNG</span></td></tr>
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
        <tr><td><span style="font-size: 14px; "><%=dtkd.ma_dtkd %></span></td></tr>
       </table>     
        
        <table class="tableborder" style="width: 100%">
            <tr style="width: 100%;">
            <td align="center" style="width:20px; ">STT</td>
            <td align="center" style="width:80px; ">Số chứng từ</td>
            <td align="center" style="width:60px; ">Ngày chứng từ</td>
            <td align="center" style="width:120px; ">Nội dung</td>
            <td align="center" style="width:120px; ">Trị giá invoice</td>
            <td align="center" style="width:100px; ">Trị giá thanh toán</td>
            <td align="center" style="width:160px; ">Ghi chú</td>
            </tr>
            <%
            int stt = 1;
            double sum_g = 0;
            double sum_gt = 0;
            string sql_detail = "select * from ("+sql+") as tmp where tmp.md_doitackinhdoanh_id='"+dtkd.md_doitackinhdoanh_id+"'";
            DataTable dd = mdbc.GetData(sql_detail);
            foreach (DataRow id in dd.Rows)
            {
                sum_g += double.Parse(id["giatriinvoice"].ToString());
                sum_gt += double.Parse(id["trigiathanhtoan"].ToString());
                %>
            <tr style="width: 100%;">
            <td align="center" style="width:20px; "><%=stt %></td>
            <td align="center" style="width:80px; "><%=id["sophieu"] %></td>
            <td align="center" style="width:60px; "><%=(DateTime.Parse(id["ngay"].ToString())).ToString("dd/MM/yyyy")%></td>
            <td align="center" style="width:120px; "><%=id["noidung"]%></td>
            <td align="right" style="width:120px; "><%=String.Format("{0:#,0}", double.Parse(id["giatriinvoice"].ToString()))%></td>
            <td align="right" style="width:100px; "><%=String.Format("{0:#,0}", double.Parse(id["trigiathanhtoan"].ToString()))%></td>
            <td align="center" style="width:160px; "></td>
            </tr> 
            <%
                stt++;
            }
            total += sum_g;
            %>
            
            <tr style="width: 100%;">
                <td colspan=4 align="right" style="width:100px; "><b>Cộng</b></td>
                <td align="right" style="width:120px; "><%=String.Format("{0:#,0}", sum_g)%></td>
                <td align="right" style="width:100px; "><%=String.Format("{0:#,0}", sum_gt)%></td>
                <td align="center" style="width:160px; "></td>
            </tr>
        </table>
        
        <%}%>
        <%--<table>
        <tr style="width: 100%;">
                <td align="right" style="width:580px; "><b>Tổng cộng</b></td>
                <td align="right" style="width:125px; "><%=String.Format("{0:#,0}", total)%></td>
            </tr>
        </table>    --%>
</div>