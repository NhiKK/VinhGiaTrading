<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cbm_nhomNangLuc.ascx.cs" Inherits="cbm_nhomNangLuc" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    var query = from i in db.md_nhomnanglucs orderby i.hehang, i.nhom select new { value = i.md_nhomnangluc_id, name = i.hehang , nhom = i.nhom, ten = i.mota_tiengviet };
%>
<select <%=(Width>0)?"style=\"width:"+(Width+5)+"px\"":"" %> <%=((Disabled) ? "disabled=\"disabled\"" : "") %> name="<%=Name%>" id="<%=Name%>" <%=((OnChange != null) ? "onchange=\"" + OnChange + "\"" : "") %>>
    <%=(NullFirstItem ? "<option value='NULL'></option>" : "")%>
<%
    foreach (var i in query)
    {
        if (Value != null && Value.Equals(i.value))
        {
%>
    <option selected="selected" value="<%=i.value %>"><%=i.name + "("+i.nhom+") - " + i.ten%></option>
<%
        }
        else
        {
%>
    <option value="<%=i.value %>"><%=i.name + "("+i.nhom+") - " + i.ten%></option>
<%
        }
    }
%>
</select>