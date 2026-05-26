using System;
using System.Collections.Generic;
using System.Web;
using NetServ.Net.Json;
using System.IO;
using System.Globalization;

namespace jqGridHelper
{
    public class RuleFilter
    {
        String field, oper, data;

        public String Field
        {
            get { return field; }
        }

        public String Oper
        {
            get { return oper; }
        }

        public String Data
        {
            get { return data; }
            set { data = value; }
        }

        public RuleFilter()
        {

        }

        public static RuleFilter CreateRuleFilter(IJsonType jsType)
        {
            JsonObject json = (JsonObject)jsType;
            RuleFilter rule = new RuleFilter();
            foreach (KeyValuePair<string, IJsonType> type in json)
            {
                String key = type.Key;
                String value = type.Value.ToString();
                switch (key)
                {
                    case "op":
                        rule.oper = value;
                        break;
                    case "data":
                        rule.data = value;
                        break;
                    case "field":
                    default:
                        rule.field = value;
                        break;
                }
            }
            return rule;
        }
    }

    public class Filter
    {
        String groupOp;
        List<RuleFilter> rules;
        IJsonType groups;

        public String ToScript()
        {
            String str = "";
            foreach (RuleFilter item in rules)
            {
                if (item.Data.ToUpper().Contains("[ALL]"))
                {
                    str += String.Format("{0} {1} LIKE '%' ", groupOp, item.Field);
                }
                else
                {
                    //DateTime date;
                    //bool isDate = DateTime.TryParseExact(item.Data, new String[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.CurrentCulture, DateTimeStyles.None, out date);

                    //if (isDate)
                    //{
                    //    item.Data = date.ToString();
                    //}
					if (item.Field == "sodh.chungtu")
                    {
                        str += String.Format("{0} CONVERT(nvarchar(max), {1}, 103) LIKE N'{2}%' ", groupOp, item.Field, item.Data);
                        break;
                    }
                    switch (item.Oper)
                    {
                        case "eq":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) = N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "ne":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) != N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "lt":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) < N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "le":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) =< N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "gt":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) > N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "ge":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) >= N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "bw":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) LIKE N'{2}%' ", groupOp, item.Field, item.Data);
                            break;
                        case "bn":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) LIKE N'%{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "in":
                            str += String.Format("{0} {1} IN (N'{2}') ", groupOp, item.Field, item.Data.Replace(",", "','"));
                            break;
                        case "ni":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) NOT IN N'%{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "ew":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) LIKE N'_{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "en":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) LIKE N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "cn":
                            str += String.Format("{0} CONVERT(nvarchar(32), {1}, 103) LIKE N'%{2}%' ", groupOp, item.Field, item.Data);
                            break;
                        case "nc":
                            str += String.Format("{0} {1} NOT LIKE N'%{2}%' ", groupOp, item.Field, item.Data);
                            break;
                        default:
                            str += "";
                            break;
                    }
                }
            }
            return str;
        }

        public static List<RuleFilter> CreateRules(IJsonType jsRules)
        {
            JsonArray arr = (JsonArray)jsRules;
            List<RuleFilter> lst = new List<RuleFilter>();
            foreach (IJsonType i in arr)
            {
                RuleFilter r = RuleFilter.CreateRuleFilter(i);
                lst.Add(r);
            }
            return lst;
        }
        
        public static Filter CreateFilter(String _filters)
        {
            JsonParser parser = new JsonParser(new StringReader(_filters), true);
            JsonObject json = parser.ParseObject();
            Filter f = new Filter();

            foreach (KeyValuePair<string, IJsonType> pair in json)
            {
                String key = pair.Key;
                IJsonType value = pair.Value;
                switch (key)
                {
                    case "groupOp":
                        f.groupOp = pair.Value.ToString();
                        break;
                    case "rules":
                        f.rules = Filter.CreateRules(value);
                        break;
                    case "groups":
                        f.groups = pair.Value;
                        break;
                    default:
                        break;
                }
            }
            return f;
        }
    }
}