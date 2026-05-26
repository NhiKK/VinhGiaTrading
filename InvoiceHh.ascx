<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvoiceHh.ascx.cs" Inherits="InvoiceHh" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%@ Import Namespace="System.Data" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    //var query = from i in db.c_packinginvoices where i.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id) && (i.md_trangthai_id.Equals("HIEULUC")
    //                   || i.c_packinginvoice_id.Equals(Value))
    //            orderby i.ngaylap
    //            select new { value = i.c_packinginvoice_id, name = i.so_pkl };
    string sql = @"
                    select distinct civ.c_packinginvoice_id, civ.so_inv 
                    from c_packinginvoice civ, c_dongpklinv cdiv
                    where civ.c_packinginvoice_id = cdiv.c_packinginvoice_id
		                    and cdiv.c_donhang_id in (
								                    select c_donhang_id 
								                    from c_donhang
								                    where md_nguoilienhe_id = '"+md_doitackinhdoanh_id+@"')
		                    and (coalesce(civ.hoahongphaitra, 0) - coalesce(civ.hoahongdatra, 0)) > 0 ";
    DataTable data = mdbc.GetData(sql);
%>
<select <%=(Width>0)?"style=\"width:"+(Width+5)+"px\"":"" %> <%=((Disabled) ? "disabled=\"disabled\"" : "") %> name="<%=Name %>" id="<%=Name %>" <%=((OnChange != null) ? "onchange=\"" + OnChange + "\"" : "") %>>
    <%=(NullFirstItem ? "<option value='NULL'></option>" : "")%>
<%
    foreach (DataRow i in data.Rows)
    {
        if (Value != null && Value.Equals(i[0]))
        {
%>
    <option selected="selected" value="<%=i[0] %>"><%=i[1]%></option>
<%
        }
        else
        {
%>
    <option value="<%=i[0] %>"><%=i[1]%></option>
<%
        }
    }
%>
</select>