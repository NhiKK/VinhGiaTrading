<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoanhThuTheoMatHang.aspx.cs" Inherits="ReportWizard_RptKhachHang_DoanhThuTheoMatHang" %>
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


    string sql = @"select dt.ma_dtkd, cl.code_cl, sp.ma_sanpham, sp.mota_tiengviet, sum(cdi.soluong) as soluong, cdi.gia, 
		                sum(cdi.thanhtien) as thanhtien, dt.md_doitackinhdoanh_id, cl.md_chungloai_id
                from c_packinginvoice civ, c_dongpklinv cdi, c_nhapxuat cnx, c_dongnhapxuat cdnx,
	                md_sanpham sp, md_chungloai cl, md_doitackinhdoanh dt
                where civ.c_packinginvoice_id = cdi.c_packinginvoice_id	
	                and cdi.c_dongnhapxuat_id = cdnx.c_dongnhapxuat_id
	                and cdnx.c_nhapxuat_id = cnx.c_nhapxuat_id
	                and cdi.md_sanpham_id = sp.md_sanpham_id
	                and sp.md_chungloai_id = cl.md_chungloai_id
	                and civ.md_doitackinhdoanh_id = dt.md_doitackinhdoanh_id
                    and civ.md_trangthai_id = 'HIEULUC'";
    sql += " and civ.ngaylap >='" + tungay + "' ";
    sql += " and civ.ngaylap <='" + denngay + "' ";
    sql += md_doitackinhdoanh_id.Equals("NULL") ? "" : "and civ.md_doitackinhdoanh_id ='" + md_doitackinhdoanh_id + "'";
    //sql += " order by cnx.sophieu";
    sql += " group by dt.ma_dtkd, cl.code_cl, sp.ma_sanpham, sp.mota_tiengviet, cdi.gia, dt.md_doitackinhdoanh_id, cl.md_chungloai_id";
    //sql += " order by dt.ma_dtkd, cl.code_cl";
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
        <tr><td colspan=2 align="center" style="width:100%"><span style="font-size: 18px; font-weight:bold">TỔNG HỢP DOANH THU THEO TỪNG MẶT HÀNG </span></td></tr>
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
       <div style="width:100%; height:20px;"></div>
       <table class="tableborder" style="width:100%; background:#EFEFEF;">
        <tr><td><span style="font-size: 14px; "><%=dtkd.ma_dtkd %></span></td></tr>
       </table>     
            <%
                string sql_mh = "select distinct tmp.md_chungloai_id from ("+sql+") as tmp where tmp.md_doitackinhdoanh_id = '"+dtkd.md_doitackinhdoanh_id+"' ";
                DataTable d_mh = mdbc.GetData(sql_mh);
                int stt = 1;
                foreach (DataRow item in d_mh.Rows)
                {
                    var chungloai = db.md_chungloais.Single(c=>c.md_chungloai_id.Equals(item["md_chungloai_id"].ToString()));
                    string sql_detail = "select * from ("+sql+") as tmp where tmp.md_chungloai_id = '"+chungloai.md_chungloai_id+"' and tmp.md_doitackinhdoanh_id ='"+dtkd.md_doitackinhdoanh_id+"'";
                    DataTable detail = mdbc.GetData(sql_detail);
                %> 
                  <table class="tableborder" style="width:100%; background:#EFEFFF;">
                    <tr><td><span style="font-size: 14px; ">Loại Hàng:<%=chungloai.code_cl%></span></td></tr>
                   </table>  
                    <table class="tableborder" style="width: 100%">
                    <%if (stt == 1)
                      { %>
                    <tr style="width: 100%;">
                    <td align="center" style="width:20px; ">STT</td>
                    <td align="center" style="width:80px; ">Mã hàng</td>
                    <td align="center" style="width:250px; ">Mô tả</td>
                    <td align="center" style="width:60px; ">Số lượng</td>
                    <td align="center" style="width:60px; ">Đơn giá</td>
                    <td align="center" style="width:60px; ">Thành tiền</td>
                    </tr>
                    <%} %>
                <%
                   
                    foreach (DataRow d in detail.Rows)
                    {%>
                        <tr style="width: 100%;">
                        <td align="center" style="width:20px; "><%=stt%></td>
                        <td align="center" style="width:80px; "><%=d["ma_sanpham"]%></td>
                        <td align="left" style="width:250px; "><%=d["mota_tiengviet"]%></td>
                        <td align="right" style="width:60px; "><%=d["soluong"]%></td>
                        <td align="right" style="width:60px; "><%=d["gia"]%></td>
                        <td align="right" style="width:60px; "><%=d["thanhtien"] %></td>
                        </tr> 
                    <%
                        stt++;
                    }
                }
                 %>
            
        </table>
        
        <%}%>
       
</div>