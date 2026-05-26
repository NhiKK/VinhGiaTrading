<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cdm_doitackinhdoanh_ldt.ascx.cs" Inherits="cdm_doitackinhdoanh" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>

<%
    LinqDBDataContext db = new LinqDBDataContext();
    var query = from dtkd in db.md_doitackinhdoanhs
                join ldt in db.md_loaidtkds on dtkd.md_loaidtkd_id equals ldt.md_loaidtkd_id
                where ldt.ma_loaidtkd == "NCC"
                orderby dtkd.ma_dtkd
                select new { value = dtkd.md_doitackinhdoanh_id, name = dtkd.ma_dtkd};
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