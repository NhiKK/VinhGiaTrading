<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cdm_doitackinhdoanh.ascx.cs" Inherits="cdm_doitackinhdoanh" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
				
	var query = (from a in db.md_hanghoadocquyens
	join i in db.md_doitackinhdoanhs on a.md_doitackinhdoanh_id equals i.md_doitackinhdoanh_id
	where i.isncc.Value.Equals(isncc)
	orderby i.ma_dtkd
	select new { value = i.md_doitackinhdoanh_id, name = i.ma_dtkd}).Distinct().OrderBy(n => n.name);
%>
<select style='width:100%' <%=((Disabled) ? "disabled=\"disabled\"" : "") %> name="<%=Name %>" id="<%=Name %>" <%=((OnChange != null) ? "onchange=\"" + OnChange + "\"" : "") %>>
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