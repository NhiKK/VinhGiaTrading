<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inc-view_linkkiemtra.aspx.cs" Inherits="inc_view_linkkiemtra" %>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Newtonsoft.Json.Linq" %>

<style type="text/css">
    #customers {
        font-family: Arial, Helvetica, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }

        #customers td, #customers th {
            border: 1px solid #ddd;
            padding: 8px;
        }

        #customers tr:nth-child(even) {
            background-color: #FFFFFF;
        }

        #customers tr:hover {
            background-color: #ddd;
        }

        #customers th {
            padding-top: 5px;
            padding-bottom: 5px;
            text-align: left;
            background-color: #04AA6D;
            color: white;
        }
</style>

<%  
    LinqDBDataContext db = new LinqDBDataContext();
    string ma_hh = Context.Request.Form["ma_hh"];
    var hh_id = db.md_sanphams.Where(s => s.ma_sanpham == ma_hh).Select(s => s.md_sanpham_id).FirstOrDefault();
    string sql1 = @"select * 
                    from c_dongdonhang a
                    where a.md_sanpham_id = '" + hh_id + "'" +
                    "order by ngaytao desc";

    DataTable sql1_ = mdbc.GetData(sql1);

%>
<div style="padding: 5px;">

    <% 
        if (sql1_.Rows.Count > 0)
        {
    %>
    <div>
        <table id ="customers">
            <tr style="background: #D2B48C; padding: 2px;" >
                <td style="width: 180px; font-weight: bold; padding: 2px;" align="center">Mã HH</td>
                <td style="width: 250px; font-weight: bold; padding: 2px;" align="center">P/O</td>
                <td style="width: 100px; font-weight: bold; padding: 2px;" align="center">SL</td>
                <td style="width: 100px; font-weight: bold; padding: 2px;" align="center">Giá bán</td>
                <td style="width: 100px; font-weight: bold; padding: 2px;" align="center">Trạng Thái</td>
                <td style="width: 250px; font-weight: bold; padding: 2px;" align="center">Số INV</td>
                <td style="width: 100px; font-weight: bold; padding: 2px;" align="center">SL đã xuất</td>
                <td style="width: 100px; font-weight: bold; padding: 2px;" align="center">Giá xuất</td>
                <td style="width: 100px; font-weight: bold; padding: 2px;" align="center">Ngày mở tờ khai</td>
            </tr>

            <%

                foreach (DataRow r in sql1_.Rows)
                {
                    decimal giahopdong = 0;
                    var sp = db.md_sanphams.Where(s => s.md_sanpham_id == r["md_sanpham_id"]).FirstOrDefault();
                    var dh = db.c_donhangs.Where(s => s.c_donhang_id == r["c_donhang_id"]).FirstOrDefault();
                    var ddh = db.c_dongdonhangs.Where(s => s.c_dongdonhang_id == r["c_dongdonhang_id"]).FirstOrDefault();
                    if (dh.phanbodiscount == true)
                    {
                        giahopdong = (ddh.giafob.GetValueOrDefault(0) + ddh.phi.GetValueOrDefault(0)) -
                                     ((ddh.giafob.GetValueOrDefault(0) + ddh.phi.GetValueOrDefault(0)) * (dh.discount.GetValueOrDefault(0) / 100));
                    }
                    else
                    {
                        giahopdong = (ddh.giafob.GetValueOrDefault(0) + ddh.phi.GetValueOrDefault(0));
                    }

                    //---Chung tu xk
                    string trangthai = "", so_inv = "", ngay_motokhai = "";
                    decimal soluong = 0, gia = 0;
                    c_dongpklinv link_xk = db.c_dongpklinvs.Where(s => s.c_donhang_id == ddh.c_donhang_id & s.md_sanpham_id == ddh.md_sanpham_id).FirstOrDefault();
                    if (link_xk != null)
                    {
                        c_packinginvoice pk_xk = db.c_packinginvoices.Where(s => s.c_packinginvoice_id == link_xk.c_packinginvoice_id).FirstOrDefault();
                        if (pk_xk.md_trangthai_id == "SOANTHAO")
                            trangthai = "Soạn Thảo";
                        else
                        {
                            trangthai = "Hiệu lực";
                            so_inv = pk_xk.so_inv;
                            soluong = link_xk.soluong.GetValueOrDefault(0);
                            gia = link_xk.gia.GetValueOrDefault(0);
                            ngay_motokhai = pk_xk.ngay_motokhai.ToString().Substring(0, 10);
                        }
                    }
            %>
            <tr>
                <td style="width: 180px;" align="center"><%=sp.ma_sanpham%></td>
                <td style="width: 250px;" align="center"><%=dh.sochungtu%></td>
                <td style="width: 100px;" align="right"><%=ddh.soluong.GetValueOrDefault(0)%></td>
                <td style="width: 100px;" align="right"><%=giahopdong%></td>
                <td style="width: 140px;" align="center"><%=trangthai%></td>
                <td style="width: 250px;" align="center"><%=so_inv%></td>
                <td style="width: 140px;" align="right"><%=soluong%></td>
                <td style="width: 140px;" align="right"><%=gia%></td>
                <td style="width: 140px;" align="center"><%=ngay_motokhai%></td>
            </tr>
            <%} %>
        </table>

    </div>
    <%
        }
        else
        {  %>
    <div style="background: #FF7474; border: 2px; padding: 7px; width: 520px;">Không có dữ liệu của mã hh trong P/O và INV!</div>
    <%}%>
</div>
