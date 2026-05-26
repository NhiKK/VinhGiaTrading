<%@ WebHandler Language="C#" Class="ChungLoaiController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class ChungLoaiController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
			case "updatestatus":
                this.UpdateStatus(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "getcolorref":
                this.getAllColor(context);
                break;
			case "nhomchungloai":
				this.nhomchungloai(context);
				break;
            default:
                switch (oper)
                {
                    case "del":
                        this.del(context);
                        break;
                    case "edit":
                        this.edit(context);
                        break;
                    case "add":
                        this.add(context);
                        break;
                    default:
                        this.load(context);
                        break;
                }
                break;
        }
    }
	
	public void UpdateStatus(HttpContext context)
    {
        String msg = "";
        try
        {   
            String id = context.Request.QueryString["md_chungloai_id"];
            String trangthai = context.Request.QueryString["trangthai"];
            md_chungloai sp = db.md_chungloais.FirstOrDefault(p => p.md_chungloai_id.Equals(id));

            if (sp != null | trangthai == "TOACTIVE")
			{
				bool next = true;
				switch (trangthai)
				{
					case "HIEULUC":
						msg = "Đã chuyển trạng thái chủng loại sản phẩm thành \"Hiệu lực\"!";
						break;
					case "SOANTHAO":
						msg = "Đã chuyển trạng thái chủng loại sản phẩm thành Soạn thảo!";
						break;
					case "TOACTIVE":
						msg = "Đã chuyển tất cả chủng loại sản phẩm \"soạn thảo\" sang \"Hiệu lực\"!";
						break;
					default:
						msg = "Không xác định được trạng thái của chủng loại sản phẩm!";
						next = false;
						break;
				}

				if (next)
				{
					if (trangthai.Equals("TOACTIVE"))
					{
						String update = "update md_chungloai set trangthai = 'HIEULUC' where trangthai='SOANTHAO'";
						mdbc.ExcuteNonQuery(update);
					}
					else
					{	
						sp.trangthai = trangthai;
						db.SubmitChanges();
					}
				}
			}
			else
			{
				msg = "Chưa chọn Chủng loại SP hoặc Chủng loại SP không tồn tại!";
			}
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        context.Response.Write(msg);
    }

    public void getAllColor(HttpContext context) { 
        LinqDBDataContext db = new LinqDBDataContext();
        string hehangid = context.Request.Form["hehang"].ToString();
        var hh = db.md_chungloais.Single(h => h.md_chungloai_id.Equals(hehangid));
        string sql = @"select distinct code_mau, tv_ngan from md_mausac where code_cl = '"+hh.code_cl+"' order by code_mau";

        DataTable data = mdbc.GetData(sql);
        string res = "";
        foreach (DataRow item in data.Rows)
        {
            
            res += "<option value=\""+item[0].ToString()+"\">"+item[1].ToString()+"</option>";
        }
        context.Response.Write(res);
    }
    
    public void nhomchungloai(HttpContext context)
    {
        String sql = "select md_nhomchungloai_id, code_ncl from md_nhomchungloai where 1=1 order by sapxep asc";
        DataTable data = mdbc.GetData(sql);
        string res = "<select>";
		res += "<option value=\"\"></option>";
        foreach (DataRow item in data.Rows)
        {
            res += "<option value=\""+item[0].ToString()+"\">"+item[1].ToString()+"</option>";
        }
		res += "</select>";
        context.Response.Write(res);
    }
    
	public void getSelectOption(HttpContext context)
    {
        String sql = "select md_chungloai_id, code_cl from md_chungloai where hoatdong = 1 order by code_cl desc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
	
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        
        String chungloai_id = context.Request.Form["id"];
        String code_cl = context.Request.Form["code_cl"];
        
        
        var lst = (from o in db.md_chungloais
                  where o.code_cl.Equals(code_cl) && !o.md_chungloai_id.Equals(chungloai_id)
                  select new { o.md_chungloai_id });
        
        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã có code chủng loại này!");
        }
        else
        {
            md_chungloai m = db.md_chungloais.Single(p => p.md_chungloai_id.Equals(chungloai_id));
            m.code_cl = context.Request.Form["code_cl"];
            m.tv_ngan = context.Request.Form["tv_ngan"];
            m.ta_ngan = context.Request.Form["ta_ngan"];
			m.nhomchungloai = context.Request.Form["nhomchungloai"];
            //m.tv_dai = context.Request.Form["tv_dai"];
            //m.ta_dai = context.Request.Form["ta_dai"];

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.nguoicapnhat = UserUtils.getUser(context);
            m.ngaycapnhat = DateTime.Now;
            db.SubmitChanges();
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        
        String code_cl = context.Request.Form["code_cl"];
        var lst = from o in db.md_chungloais where o.code_cl.Equals(code_cl) select o;
        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã có code chủng loại này!");
        }
        else
        {
            md_chungloai mnu = new md_chungloai
            {
                md_chungloai_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                code_cl = context.Request.Form["code_cl"],
                tv_ngan = context.Request.Form["tv_ngan"],
                ta_ngan = context.Request.Form["ta_ngan"],
				nhomchungloai = context.Request.Form["nhomchungloai"],
                trangthai = "SOANTHAO",
                //tv_dai = context.Request.Form["tv_dai"],
                //ta_dai = context.Request.Form["ta_dai"],

                mota = context.Request.Form["mota"],
                hoatdong = hd,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context),
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };

            db.md_chungloais.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_chungloai where md_chungloai_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
        
        String sqlCount = "SELECT COUNT(*) AS count FROM md_chungloai cl where 1=1 {0} ";
        sqlCount = string.Format(sqlCount, filter);
        
        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        String sidx = context.Request.QueryString["sidx"];
        String sord = context.Request.QueryString["sord"];
        int total_page;
        int count = (int)mdbc.ExecuteScalar(sqlCount);
        int start, end;

        if (count > 0)
        {
            total_page = (int)Math.Ceiling(1.0 * count / limit);
        }
        else
        {
            total_page = 0;
        }

        if (page > total_page) page = total_page;
        start = limit * page - limit;
        end = (page * limit) + 1;

        if (sidx.Equals("") || sidx == null)
        {
            sidx = "cl.md_chungloai_id";
        }

        string strsql = "select * from( " +
            " select cl.md_chungloai_id, cl.trangthai, cl.code_cl, cl.tv_ngan, cl.ta_ngan, cl.tv_dai, cl.ta_dai, cl.ngaytao, cl.nguoitao, cl.ngaycapnhat, cl.nguoicapnhat, cl.mota, cl.hoatdong, ncl.code_ncl " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_chungloai cl left join md_nhomchungloai ncl on cl.nhomchungloai = ncl.md_nhomchungloai_id where 1=1 {0}" +
            " )P where RowNum > @start AND RowNum < @end";
        strsql = string.Format(strsql, filter);
        
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row[0] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[1] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[2] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[3] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[7].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[9].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[12] + "]]></cell>";
			xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
