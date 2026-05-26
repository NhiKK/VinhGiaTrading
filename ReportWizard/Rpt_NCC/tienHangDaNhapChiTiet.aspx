<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tienHangDaNhapChiTiet.aspx.cs" Inherits="ReportWizard_Rpt_NCC_tienHangDaNhapChiTiet" %>
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


    string sql = @"select cnx.sophieu, cnx.ngay_giaonhan, sp.ma_sanpham, sp.mota_tiengviet,
	                cdnx.slthuc_nhapxuat, cdnx.dongia, (cdnx.slthuc_nhapxuat * cdnx.dongia) as thanhtien,
                    cnx.md_doitackinhdoanh_id	                
                from c_nhapxuat cnx, c_dongnhapxuat cdnx, md_sanpham sp
                where cnx.c_nhapxuat_id = cdnx.c_nhapxuat_id
	                and cnx.md_trangthai_id = 'HIEULUC'
	                and cnx.md_loaichungtu_id = 'NK'
	                and cdnx.md_sanpham_id = sp.md_sanpham_id";
    sql += " and cnx.ngay_giaonhan >='" + tungay + "' ";
    sql += " and cnx.ngay_giaonhan <='" + denngay + "' ";
    sql += md_doitackinhdoanh_id.Equals("NULL") ? "" : "and cnx.md_doitackinhdoanh_id ='"+md_doitackinhdoanh_id+"'";
    //sql += " order by cnx.sophieu";
    string sql_gr = @"select distinct tmp.md_doitackinhdoanh_id from("+sql+") as tmp";

    DataTable d_detail = mdbc.GetData(sql);
    DataTable d_gr = mdbc.GetData(sql_gr);
    double total = 0;
    double total_tt = 0;
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
        <tr><td colspan=2 align="center" style="width:100%"><span style="font-size: 18px; font-weight:bold">GIÁ TRỊ TIỀN HÀNG ĐÃ NHẬP CHI TIẾT</span></td></tr>
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
            <td align="center" style="width:60px; ">Ngày nhận hàng</td>
            <td align="center" style="width:80px; ">Mã số VINHGIA</td>
            <td align="center" style="width:140px; ">Mô tả</td>
            <td align="center" style="width:50px; ">Số lượng đã giao</td>
            <td align="center" style="width:50px; ">Đơn giá</td>
            <td align="center" style="width:60px; ">Thành tiền</td>
            </tr>
            <%
            int stt = 1;
            double sum_g = 0;
            double sum_gtt = 0;
            foreach (DataRow id in d_detail.Rows)
            {
                sum_g += double.Parse(id["slthuc_nhapxuat"].ToString());
                sum_gtt += double.Parse(id["thanhtien"].ToString());
                %>
            <tr style="width: 100%;">
            <td align="center" style="width:20px; "><%=stt %></td>
            <td align="center" style="width:80px; "><%=id["sophieu"]%></td>
            <td align="center" style="width:60px; "><%=(DateTime.Parse(id["ngay_giaonhan"].ToString())).ToString("dd/MM/yyyy")%></td>
            <td align="center" style="width:80px; "><%=id["ma_sanpham"]%></td>
            <td align="left" style="width:140px; "><%=id["mota_tiengviet"]%></td>
            <td align="right" style="width:50px; "><%=String.Format("{0:#,0}", double.Parse(id["slthuc_nhapxuat"].ToString()))%></td>
            <td align="right" style="width:50px; "><%=String.Format("{0:#,0}", double.Parse(id["dongia"].ToString()))%></td>
            <td align="right" style="width:60px; "><%=String.Format("{0:#,0}", double.Parse(id["thanhtien"].ToString()))%></td>
            </tr>
            <%
                stt++;
            }
            total += sum_g;
            total_tt += sum_gtt;
            %>
            
            <tr style="width: 100%;">
            <td align="right" colspan=5 style="width:140px; ">Cộng</td>
            <td align="right" style="width:50px; "><%=String.Format("{0:#,0}", sum_g)%></td>
            <td align="right" style="width:50px; "></td>
            <td align="right" style="width:60px; "><%=String.Format("{0:#,0}", sum_gtt)%></td>
            </tr>
        </table>
        
        <%}%>
        <table>
        <tr style="width: 100%;">
                <td align="right" style="width:630px; "><b>Tổng cộng</b></td>
                <td align="right" style="width:85px; "><%=String.Format("{0:#,0}", total)%></td>
                <td align="right" style="width:80px; "></td>
                <td align="right" style="width:100px; "><%=String.Format("{0:#,0}", total_tt)%></td>
            </tr>
        </table>    
</div>