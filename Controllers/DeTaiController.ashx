<%@ WebHandler Language="C#" Class="DeTaiController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class DeTaiController : IHttpHandler
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
            String id = context.Request.QueryString["md_detai_id"];
            String trangthai = context.Request.QueryString["trangthai"];
            md_detai sp = db.md_detais.FirstOrDefault(p => p.md_detai_id.Equals(id));

            if (sp != null | trangthai == "TOACTIVE")
			{
				bool next = true;
				switch (trangthai)
				{
					case "HIEULUC":
						msg = "Đã chuyển trạng thái đề tài thành \"Hiệu lực\"!";
						break;
					case "SOANTHAO":
						msg = "Đã chuyển trạng thái đề tài thành Soạn thảo!";
						break;
					case "TOACTIVE":
						msg = "Đã chuyển tất cả đề tài \"soạn thảo\" sang \"Hiệu lực\"!";
						break;
					default:
						msg = "Không xác định được trạng thái của đề tài!";
						next = false;
						break;
				}

				if (next)
				{
					if (trangthai.Equals("TOACTIVE"))
					{
						String update = "update md_detai set trangthai = 'HIEULUC' where trangthai='SOANTHAO'";
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
				msg = "Chưa chọn đề tài hoặc đề tài không tồn tại!";
			}
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        context.Response.Write(msg);
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select md_detai_id, (code_cl + '-' + code_dt) from md_detai where hoatdong = 1 order by (code_cl + '-' + code_dt) desc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String parChungLoai_Id = context.Request.Form["md_chungloai_id"];
        String code_cl = db.md_chungloais.Single(cl => cl.md_chungloai_id.Equals(parChungLoai_Id)).code_cl;

        md_detai m = db.md_detais.Single(p => p.md_detai_id == context.Request.Form["id"]);
        m.md_chungloai_id = parChungLoai_Id;
        m.code_cl = code_cl;
        m.code_dt = context.Request.Form["code_dt"];
        m.tv_ngan = context.Request.Form["tv_ngan"];
        m.ta_ngan = context.Request.Form["ta_ngan"];
        m.tv_dai = context.Request.Form["tv_dai"];
        m.ta_dai = context.Request.Form["ta_dai"];
        
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaycapnhat = DateTime.Now;
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String parChungLoai_Id = context.Request.Form["md_chungloai_id"];
        String code_cl = db.md_chungloais.Single(cl => cl.md_chungloai_id.Equals(parChungLoai_Id)).code_cl;

        md_detai mnu = new md_detai
        {
            md_detai_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            md_chungloai_id = parChungLoai_Id,
            code_cl = code_cl,
            code_dt = context.Request.Form["code_dt"],
            tv_ngan = context.Request.Form["tv_ngan"],
            ta_ngan = context.Request.Form["ta_ngan"],
            tv_dai = context.Request.Form["tv_dai"],
            ta_dai = context.Request.Form["ta_dai"],
            trangthai = "SOANTHAO",
			
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_detais.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_detai where md_detai_id IN (" + id + ")";
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
        
        String md_chungloai_id = context.Request.QueryString["ChungLoaiId"];
        
        String sqlCount = "SELECT COUNT(*) AS count " + 
            " FROM md_detai dt, md_chungloai cl " +
            " WHERE dt.md_chungloai_id = cl.md_chungloai_id " +
            " AND dt.md_chungloai_id = N'{0}' " + filter;

        if (md_chungloai_id != null)
        {
            sqlCount = String.Format(sqlCount, md_chungloai_id);
        }
        else {
            sqlCount = String.Format(sqlCount, 0);
        }
        
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
            sidx = "dt.md_detai_id";
        }

        string strsql = "select * from( " +
            " SELECT dt.md_detai_id, cl.md_chungloai_id, dt.code_dt, dt.tv_ngan, dt.ta_ngan " +
            " , dt.tv_dai, dt.ta_dai " +
            " , dt.ngaytao, dt.nguoitao, dt.ngaycapnhat, dt.nguoicapnhat, dt.mota, dt.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM md_detai dt, md_chungloai cl " +
            " WHERE dt.md_chungloai_id = cl.md_chungloai_id " +
            " AND dt.md_chungloai_id = N'{0}' " + filter +
            " )P where RowNum > @start AND RowNum < @end";
        
        if (md_chungloai_id != null)
        {
            strsql = String.Format(strsql, md_chungloai_id);
        }
        else
        {
            strsql = String.Format(strsql, 0);
        }
        
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
