<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cbm_sanpham.ascx.cs" Inherits="cbm_sanpham" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    var query = from i in db.md_sanphams orderby i.ma_sanpham select new { value = i.ma_sanpham, name = i.ma_sanpham };
%>
<select <%=(Width>0)?"style=\"width:"+(Width+10)+"px\"":"" %> <%=((Disabled) ? "disabled=\"disabled\"" : "") %> name="<%=Name%>" id="<%=Name%>" <%=((OnChange != null) ? "onchange=\"" + OnChange + "\"" : "") %>>
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