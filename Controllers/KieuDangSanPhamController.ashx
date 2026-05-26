<%@ WebHandler Language="C#" Class="KieuDangSanPhamController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class KieuDangSanPhamController : IHttpHandler
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
            String id = context.Request.QueryString["md_kieudang_id"];
            String trangthai = context.Request.QueryString["trangthai"];
            md_kieudang sp = db.md_kieudangs.FirstOrDefault(p => p.md_kieudang_id.Equals(id));

            if (sp != null | trangthai == "TOACTIVE")
			{
				bool next = true;
				switch (trangthai)
				{
					case "HIEULUC":
						msg = "Đã chuyển trạng thái kiểu dáng sản phẩm thành \"Hiệu lực\"!";
						break;
					case "SOANTHAO":
						msg = "Đã chuyển trạng thái kiểu dáng sản phẩm thành Soạn thảo!";
						break;
					case "TOACTIVE":
						msg = "Đã chuyển tất cả kiểu dáng sản phẩm \"soạn thảo\" sang \"Hiệu lực\"!";
						break;
					default:
						msg = "Không xác định được trạng thái của kiểu dáng sản phẩm!";
						next = false;
						break;
				}

				if (next)
				{
					if (trangthai.Equals("TOACTIVE"))
					{
						String update = "update md_kieudang set trangthai = 'HIEULUC' where trangthai='SOANTHAO'";
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
				msg = "Chưa chọn kiểu dáng SP hoặc kiểu dáng SP không tồn tại!";
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
        String sql = "select md_kieudang_id, ten_tv from md_kieudang where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_kieudang m = db.md_kieudangs.Single(p => p.md_kieudang_id == context.Request.Form["id"]);
        m.ma_kieudang = context.Request.Form["ma_kieudang"];
        m.ten_tv = context.Request.Form["ten_tv"];
        m.ten_ta = context.Request.Form["ten_ta"];
        m.ten_tv_dai = context.Request.Form["ten_tv_dai"];
        m.ten_ta_dai = context.Request.Form["ten_ta_dai"];
        
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

        md_kieudang mnu = new md_kieudang
        {
            md_kieudang_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            ma_kieudang = context.Request.Form["ma_kieudang"],
            ten_tv = context.Request.Form["ten_tv"],
            ten_ta = context.Request.Form["ten_ta"],
            ten_tv_dai = context.Request.Form["ten_tv_dai"],
            ten_ta_dai = context.Request.Form["ten_ta_dai"],
			trangthai = "SOANTHAO",
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_kieudangs.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_kieudang where md_kieudang_id IN (" + id + ")";
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
        

        String sqlCount = "SELECT COUNT(*) AS count FROM md_kieudang WHERE 1=1 " + filter;
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
            sidx = "kd.md_kieudang_id";
        }

        string strsql = "select * from( " +
            " select kd.md_kieudang_id, kd.trangthai, kd.ma_kieudang, kd.ten_tv, kd.ten_ta, kd.ten_tv_dai, kd.ten_ta_dai, kd.ngaytao, kd.nguoitao, kd.ngaycapnhat, kd.nguoicapnhat, kd.mota, kd.hoatdong " +
            ",  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum from md_kieudang kd " +
            " WHERE 1=1 " + filter +
            ")P where RowNum > @start AND RowNum < @end";

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
