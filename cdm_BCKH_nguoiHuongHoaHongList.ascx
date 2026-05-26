<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cdm_BCKH_nguoiHuongHoaHongList.ascx.cs" Inherits="cdm_BCKH_nguoiHuongHoaHongList"%>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%
    LinqDBDataContext db = new LinqDBDataContext();
    var query = from i in db.md_doitackinhdoanhs
				join b in db.md_loaidtkds on i.md_loaidtkd_id equals b.md_loaidtkd_id
				where b.ma_loaidtkd == "NHH" & i.hoatdong == true
                orderby i.ma_dtkd
                select new { value = i.md_doitackinhdoanh_id, name = i.ma_dtkd};
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