<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PO.ascx.cs" Inherits="PO" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    var query = from i in db.c_donhangs where i.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id) && i.md_trangthai_id.Equals("HIEULUC") 
                || i.c_donhang_id.Equals(Value) 
				orderby i.ngaylap descending
				select new { value = i.c_donhang_id, name = i.sochungtu };
%>
<select <%=(Width>0)?"style=\"width:"+(Width+5)+"px\"":"" %> <%=((Disabled) ? "disabled=\"disabled\"" : "") %> name="<%=Name %>" id="<%=Name %>" <%=((OnChange != null) ? "onchange=\"" + OnChange + "\"" : "") %>>
    <%=(NullFirstItem ? "<option value='NULL'></option>" : "")%>
<%
    foreach (var i in query)
    {
        if (Value != null && Value.Equals(i.value))
        {
%>
    <option selected="selected" value="<%=i.value %>"><%=i.name%></option>
<%
        }
        else
        {
%>
    <option value="<%=i.value %>"><%=i.name%></option>
<%
        }
    }
%>
</select>