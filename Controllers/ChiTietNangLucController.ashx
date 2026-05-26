<%@ WebHandler Language="C#" Class="ChiTietNangLucController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ChiTietNangLucController : IHttpHandler
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

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_chitietnangluc_id, c_chitietnangluc_id from c_chitietnangluc where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        String s = context.Request.Form["saveto"].ToLower();
        bool hd = false;
        bool st = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
        if (s.Equals("on") || s.Equals("true"))
        { st = true; }

        String md_nhomnangluc_id = context.Request.Form["md_nhomnangluc_id"];
        md_nhomnangluc nnl = db.md_nhomnanglucs.Single(p => p.md_nhomnangluc_id.Equals(md_nhomnangluc_id));
        String dauma = nnl.hehang + "(" + nnl.nhom + ")";

        decimal nangsuat = decimal.Parse(context.Request.Form["nangxuat_tb"]);
        decimal sl_dadat = decimal.Parse(context.Request.Form["sl_dadat"]);
        var m = db.c_chitietnanglucs.Single(p => p.c_chitietnangluc_id == context.Request.Form["id"]);

        var chkExist = db.c_chitietnanglucs.Where(t => 
            t.c_tuannangluc_id == m.c_tuannangluc_id 
            & t.dauma == dauma 
            & t.c_chitietnangluc_id != m.c_chitietnangluc_id).FirstOrDefault();
        if (chkExist != null)
        {
            jqGridHelper.Utils.writeResult(0, string.Format("Đầu mã {0} đã tồn tại.", dauma));
        }
        else
        {
            m.md_nhomnangluc_id = context.Request.Form["md_nhomnangluc_id"];
            //m.c_tuannangluc_id = context.Request.Form["c_tuannangluc_id"];
            //m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
            m.tenhehang = db.md_chungloais.FirstOrDefault(p => p.code_cl.Equals(nnl.hehang) && p.hoatdong.Equals(true)).tv_ngan; // ten he hang = loai san pham cua nhom nang luc
            m.dauma = dauma; // ghep ten he hang cua nhom nang luc
                             //m.thoigiannhan_dh = int.Parse(context.Request.Form["thoigiannhan_dh"]);

            m.nangxuat_tb = nangsuat;
            m.sl_dadat = sl_dadat;
            m.sl_conlai = nangsuat - sl_dadat;
            //m.tinhtrang = context.Request.Form["tinhtrang"];


            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;
            m.nguoicapnhat = UserUtils.getUser(context);
            db.SubmitChanges();
            if (st.Equals(true))
            {
                string sql_uAll = @"update c_chitietnangluc
					set nangxuat_tb = " + nangsuat + @", sl_conlai = " + nangsuat + @"
					where c_chitietnangluc_id in(
						select c_chitietnangluc_id 
						from c_chitietnangluc                      
						where md_nhomnangluc_id = '" + md_nhomnangluc_id + "'" +
                                " and md_doitackinhdoanh_id = '" + m.md_doitackinhdoanh_id + "'" +
                                @" and c_tuannangluc_id in 
								(select c_tuannangluc_id from c_tuannangluc 
								where ngaybatdau > (select ngaybatdau from c_tuannangluc where c_tuannangluc_id = '" + m.c_tuannangluc_id + @"'))
							and sl_dadat = 0
					)";
                mdbc.ExcuteNonQuery(sql_uAll);
            }
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        // lay thong tin nhom nang luc
        String md_nhomnangluc_id = context.Request.Form["md_nhomnangluc_id"];
        md_nhomnangluc nnl = db.md_nhomnanglucs.Single(p => p.md_nhomnangluc_id.Equals(md_nhomnangluc_id));
        String dauma = nnl.hehang + "(" + nnl.nhom + ")";

        decimal nangsuat = decimal.Parse(context.Request.Form["nangxuat_tb"]);
        decimal sl_dadat = decimal.Parse(context.Request.Form["sl_dadat"]);

        // insert new //        
        String c_tuannangluc_id = context.Request.Form["c_tuannangluc_id"];
        String md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];

        var chkExist = db.c_chitietnanglucs.Where(s => s.c_tuannangluc_id == c_tuannangluc_id & s.dauma == dauma).FirstOrDefault();
        if (chkExist != null)
        {
            jqGridHelper.Utils.writeResult(0, string.Format("Đầu mã {0} đã tồn tại.", dauma));
        }
        else
        {
            var mnu = new c_chitietnangluc
            {
                c_chitietnangluc_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                md_nhomnangluc_id = context.Request.Form["md_nhomnangluc_id"],
                c_tuannangluc_id = context.Request.Form["c_tuannangluc_id"],
                md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"],
                tenhehang = db.md_chungloais.FirstOrDefault(p => p.code_cl.Equals(nnl.hehang) && p.hoatdong.Equals(true)).tv_ngan, // ten he hang = loai san pham cua nhom nang luc
                dauma = dauma, // ghep ten he hang cua nhom nang luc
                               //thoigiannhan_dh = int.Parse(context.Request.Form["thoigiannhan_dh"]),

                nangxuat_tb = nangsuat,
                sl_dadat = sl_dadat,
                sl_conlai = nangsuat - sl_dadat,
                //tinhtrang = context.Request.Form["tinhtrang"],

                ngaytao = DateTime.Now,
                mota = context.Request.Form["mota"],
                hoatdong = hd,
                ngaycapnhat = DateTime.Now,
                nguoicapnhat = UserUtils.getUser(context),
                nguoitao = UserUtils.getUser(context)
            };

            db.c_chitietnanglucs.InsertOnSubmit(mnu);
            db.SubmitChanges();
        }
    }

    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete c_chitietnangluc where c_chitietnangluc_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String weekId = context.Request.QueryString["weekId"];

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        String sqlCount = @"SELECT COUNT(*) AS count 
        FROM c_chitietnangluc ctnl
		left join md_nhomnangluc nnl on ctnl.md_nhomnangluc_id = nnl.md_nhomnangluc_id
		left join c_tuannangluc tnl on ctnl.c_tuannangluc_id = tnl.c_tuannangluc_id
		left join md_doitackinhdoanh dtkd on ctnl.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
        WHERE 
        1=1
        AND ctnl.c_tuannangluc_id = N'{0}' {1}";

        if (weekId != null)
        {
            sqlCount = String.Format(sqlCount, weekId, filter);
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
            sidx = "ctnl.c_chitietnangluc_id";
        }

        string strsql = @"select * from( 
            SELECT ctnl.c_chitietnangluc_id 
            , (nnl.hehang + '(' + nnl.nhom + ')') as nhomnl 
            , tnl.c_tuannangluc_id, dtkd.md_doitackinhdoanh_id, ctnl.tenhehang, ctnl.dauma 
            , ctnl.nangxuat_tb, ctnl.sl_dadat, ctnl.sl_conlai, 0 as saveto 
            , ctnl.ngaytao, ctnl.nguoitao, ctnl.ngaycapnhat, ctnl.nguoicapnhat, ctnl.mota 
            , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + @") as RowNum 
            FROM c_chitietnangluc ctnl
			left join md_nhomnangluc nnl on ctnl.md_nhomnangluc_id = nnl.md_nhomnangluc_id
			left join c_tuannangluc tnl on ctnl.c_tuannangluc_id = tnl.c_tuannangluc_id
			left join md_doitackinhdoanh dtkd on ctnl.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
            WHERE 1=1 
            AND ctnl.c_tuannangluc_id = N'{0}' {1}
            )P where RowNum > @start AND RowNum < @end";

        if (weekId != null)
        {
            strsql = String.Format(strsql, weekId, filter);
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
            xml += "<cell><![CDATA[" + row[4] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[5] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[6] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[7] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[10].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row[12].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row[13] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[14] + "]]></cell>";
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
