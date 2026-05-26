<%@ WebHandler Language="C#" Class="TuanNangLucController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class TuanNangLucController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getoption":
                this.getSelectOption(context);
                break;
            case "createCapacity":
                this.createCapacity(context);
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

    public void createCapacity(HttpContext context)
    {
        String result = "";
        String weekId = context.Request.QueryString["weekId"];
        String selPartner = "select md_doitackinhdoanh_id from c_tuannangluc where c_tuannangluc_id = @c_tuannangluc_id";
        String partnerId = (String)mdbc.ExecuteScalar(selPartner,"@c_tuannangluc_id", weekId);
        
        
        String selWeekDetails = "select count(*) as count from c_chitietnangluc where c_tuannangluc_id = @c_tuannangluc_id";
        int count = (int)mdbc.ExecuteScalar(selWeekDetails, "@c_tuannangluc_id", weekId);
        //if (count > 0)
        //{
        //    result += "Tuần đã có chi tiết năng lực";
        //}
        //else {
           // mdbc.ExcuteNonProcedure("crtNangXuatTheoDauMa", "@ncc_id", partnerId, "@tuannl_id", weekId, "@user_id", "admin");
            //result += "Đã tạo chi tiết năng lực cho tuần mà bạn chọn";
        //}
		//result = "crtNangXuatTheoDauMa,@ncc_id," + partnerId + ",@tuannl_id," + weekId + ",@user_id,admin";
		
		try{
			string sql = String.Format(@"
					SELECT distinct sp.md_nhomnangluc_id as dauma
					FROM md_sanpham sp
					WHERE  '{0}' in 
					(
						select ncu_md.md_doitackinhdoanh_id from md_nhacungungmacdinh ncu_md
						where ncu_md.md_sanpham_id = sp.md_sanpham_id
					)",partnerId);
				
			System.Data.DataTable dt = mdbc.GetData(sql);
			
			if (dt.Rows.Count != 0)
			{
				for (int n = 0; n < dt.Rows.Count; n++)
				{
					string md_nhomnangluc_id = dt.Rows[n]["dauma"].ToString();
					string nhom = db.md_nhomnanglucs.Where(s => s.md_nhomnangluc_id == md_nhomnangluc_id).Select(s => s.nhom).FirstOrDefault();
					string loaisanpham = db.md_nhomnanglucs.Where(s => s.md_nhomnangluc_id == md_nhomnangluc_id).Select(s => s.mota_tiengviet).FirstOrDefault();
					string hehang = db.md_nhomnanglucs.Where(s => s.md_nhomnangluc_id == md_nhomnangluc_id).Select(s => s.hehang).FirstOrDefault();

					int check = db.c_chitietnanglucs.Where(p => p.c_tuannangluc_id == weekId  & p.md_nhomnangluc_id == md_nhomnangluc_id).Count();
					if(check <= 0)
					{
						c_chitietnangluc mnu = new c_chitietnangluc
						{
							c_chitietnangluc_id =  ImportUtils.getNEWID(),
							c_tuannangluc_id = weekId,
							md_doitackinhdoanh_id = partnerId,
							tenhehang = loaisanpham,
							dauma = hehang + "(" + nhom + ")",
							thoigiannhan_dh = 0,
							nangxuat_tb = 0,
							nangxuat_min = 0,
							nangxuat_max = 0,
							sl_dadat = 0,
							sl_conlai = 0,
							tinhtrang = "",
							hoatdong = true,
							nguoitao = UserUtils.getUser(context),
							nguoicapnhat = UserUtils.getUser(context),
							ngaytao = DateTime.Now,
							ngaycapnhat = DateTime.Now,
							md_nhomnangluc_id = md_nhomnangluc_id
						};

						db.c_chitietnanglucs.InsertOnSubmit(mnu);
						db.SubmitChanges();
					}
				}
				result = "Đã tạo chi tiết năng lực cho tuần mà bạn chọn";
			}
			context.Response.Write(result);
		}
		catch(Exception e){
            context.Response.Write(e.Message);
        }
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_tuannangluc_id, ten_tuan from c_tuannangluc where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_tuannangluc m = db.c_tuannanglucs.Single(p => p.c_tuannangluc_id == context.Request.Form["id"]);
        m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        m.ten_tuan = context.Request.Form["ten_tuan"];
        m.tuanthu = int.Parse(context.Request.Form["tuanthu"]);
        m.ngaybatdau = DateTime.ParseExact(context.Request.Form["ngaybatdau"], "dd/MM/yyyy", null);
        
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

        c_tuannangluc mnu = new c_tuannangluc
        {
            c_tuannangluc_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"],
            ten_tuan = context.Request.Form["ten_tuan"],
            tuanthu = int.Parse(context.Request.Form["tuanthu"]),
            ngaybatdau = DateTime.ParseExact(context.Request.Form["ngaybatdau"], "dd/MM/yyyy", null),
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.c_tuannanglucs.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "update c_tuannangluc set hoatdong = 0 where c_tuannangluc_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String partnerId = context.Request.QueryString["partnerId"];

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
        
        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_tuannangluc tnl, md_doitackinhdoanh dtkd " +
            " WHERE tnl.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id" +
            " AND tnl.md_doitackinhdoanh_id = N'{0}' {1}";

        if (partnerId != null)
        {
            sqlCount = String.Format(sqlCount, partnerId, filter);
        }
        else {
            sqlCount = String.Format(sqlCount, 0, filter);
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
            sidx = "tnl.c_tuannangluc_id";
        }

        string strsql = "select * from( " +
            " SELECT tnl.c_tuannangluc_id, dtkd.md_doitackinhdoanh_id, tnl.ten_tuan " +
            " , tnl.tuanthu, tnl.ngaybatdau, tnl.nam, tnl.ngaytao, tnl.nguoitao " +
            " , tnl.ngaycapnhat, tnl.nguoicapnhat, tnl.mota, tnl.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_tuannangluc tnl, md_doitackinhdoanh dtkd " +
            " WHERE tnl.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " + 
            " AND tnl.md_doitackinhdoanh_id = N'{0}' {1}" +
            " )P where RowNum > @start AND RowNum < @end";
        
        if (partnerId != null)
        {
            strsql = String.Format(strsql, partnerId, filter);
        }
        else
        {
            strsql = String.Format(strsql, 0, filter);
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
            xml += "<cell><![CDATA[" + DateTime.Parse(row[4].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[6].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[8].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
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
