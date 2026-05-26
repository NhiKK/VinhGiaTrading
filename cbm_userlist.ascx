<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cbm_userlist.ascx.cs" Inherits="cbm_userlist" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    var query = from i in db.nhanviens 
                orderby i.manv
                select new { value = i.manv, name = i.manv};
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