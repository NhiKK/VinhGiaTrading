using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

public class SelectHtmlControl
{
    private String _sql;

    public String Sql
    {
        get { return _sql; }
        set { _sql = value; }
    }

    public SelectHtmlControl()
    { }

    public SelectHtmlControl(String sql)
    {
        _sql = sql;
    }
    public string ToOptionList()
    {
        DataTable dt = mdbc.GetData(_sql);
        String str = "";
        if (dt.Rows.Count != 0)
        {

            foreach (DataRow item in dt.Rows)
            {
                str += string.Format("<option value=\"{0}\">{1}</option>", item[0], item[1]);
            }
        }
        else
        {
            str = "null";
        }
        return str;
    }

    public string ToOptionSearch()
    {
        DataTable dt = mdbc.GetData(_sql);
        String str = "";
        if (dt.Rows.Count != 0)
        {
            str += "<select>";
            str += "<option value=\"[ALL]\">[ALL]</option>";
            foreach (DataRow item in dt.Rows)
            {
                str += string.Format("<option value=\"{0}\">{1}</option>", item[0], item[1]);
            }
            str += "</select>";
        }
        else
        {
            str = "null";
        }
        return str;
    }

    public string ToOptionNullFirst()
    {
        DataTable dt = mdbc.GetData(_sql);
        String str = "";
        if (dt.Rows.Count != 0)
        {
            str += "<select>";
            str += "<option value=\"\"></option>";
            foreach (DataRow item in dt.Rows)
            {
                str += string.Format("<option value=\"{0}\">{1}</option>", item[0], item[1]);
            }
            str += "</select>";
        }
        else
        {
            str = "null";
        }
        return str;
    }


    public override string ToString()
    {
        DataTable dt = mdbc.GetData(_sql);
        String str = "<select>";
        foreach (DataRow item in dt.Rows)
        {
            str += string.Format("<option value=\"{0}\">{1}</option>", item[0], item[1]);
        }
        str += "</select>";
        return str;
    }

    public string ToSelected(String id)
    {
        DataTable dt = mdbc.GetData(_sql);
        String str = "<select>";
        foreach (DataRow item in dt.Rows)
        {
			string other_str = "";
			
			try {
				other_str = item[2].ToString();
			}
			catch { }
			
            if (item[0].ToString().Equals(id))
            {
                str += string.Format("<option other=\"{2}\" selected=\"selected\" value=\"{0}\">{1}</option>", item[0], item[1], other_str);
            }
            else
            {
                str += string.Format("<option other=\"{2}\" value=\"{0}\">{1}</option>", item[0], item[1], other_str);
            }
        }
        str += "</select>";
        return str;
    }
}
