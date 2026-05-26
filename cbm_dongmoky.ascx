<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cbm_dongmoky.ascx.cs" Inherits="cbm_dongmoky" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    var query = from i in db.a_dongmokies join t in db.a_kytrongnams on i.a_kytrongnam_id equals t.a_kytrongnam_id
                    join n in db.a_namtaichinhs on i.a_namtaichinh_id equals n.a_namtaichinh_id
					where i.ky_hoatdong.Equals("MOKY") & (i.daxuly.Equals(1) | i.daxuly2.Equals(1))
                orderby i.ngaytao
                select new { value = i.a_dongmoky_id, name = (t.tenky + " - " + n.nam.Value)};
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