<%@ WebHandler Language="C#" Class="ChucNangSanPhamController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ChucNangSanPhamController : IHttpHandler
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
            String id = context.Request.QueryString["md_chucnang_id"];
            String trangthai = context.Request.QueryString["trangthai"];
            md_chucnang sp = db.md_chucnangs.FirstOrDefault(p => p.md_chucnang_id.Equals(id));

            if (sp != null | trangthai == "TOACTIVE")
			{
				bool next = true;
				switch (trangthai)
				{
					case "HIEULUC":
						msg = "Đã chuyển trạng thái chức năng sản phẩm thành \"Hiệu lực\"!";
						break;
					case "SOANTHAO":
						msg = "Đã chuyển trạng thái chức năng sản phẩm thành Soạn thảo!";
						break;
					case "TOACTIVE":
						msg = "Đã chuyển tất cả chức năng sản phẩm \"soạn thảo\" sang \"Hiệu lực\"!";
						break;
					default:
						msg = "Không xác định được trạng thái của chức năng sản phẩm!";
						next = false;
						break;
				}

				if (next)
				{
					if (trangthai.Equals("TOACTIVE"))
					{
						String update = "update md_chucnang set trangthai = 'HIEULUC' where trangthai='SOANTHAO'";
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
				msg = "Chưa chọn chức năng SP hoặc chức năng SP không tồn tại!";
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
        String sql = "select md_chucnang_id, ten_tv from md_chucnang where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        md_chucnang m = db.md_chucnangs.Single(p => p.md_chucnang_id == context.Request.Form["id"]);
        m.ma_chucnang = context.Request.Form["ma_chucnang"];
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
        
        md_chucnang mnu = new md_chucnang
        {
            md_chucnang_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            ma_chucnang = context.Request.Form["ma_chucnang"],
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

        db.md_chucnangs.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_chucnang where md_chucnang_id IN (" + id + ")";
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
        
        String sqlCount = "SELECT COUNT(*) AS count FROM md_chucnang where 1=1 {0}";
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
            sidx = "cn.md_chucnang_id";
        }

        string strsql = "select * from( " +
            " select cn.md_chucnang_id, cn.trangthai, cn.ma_chucnang, cn.ten_tv, cn.ten_ta, cn.ten_tv_dai, cn.ten_ta_dai, cn.ngaytao, cn.nguoitao, cn.ngaycapnhat, cn.nguoicapnhat, cn.mota, cn.hoatdong " +
            ",  ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum "+
            " from md_chucnang cn where 1=1 {0}" +
            ")P where RowNum > @start AND RowNum < @end";
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
