<%@ WebHandler Language="C#" Class="ChiPhanBoInvController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

public class ChiPhanBoInvController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();
    string md_doitackinhdoanh_id = "";
    string cttc_id = "";


    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        md_doitackinhdoanh_id = context.Request.QueryString["dtkd"];
        cttc_id = context.Request.QueryString["cttc"];
        switch (action)
        {
            case "getPackingInvoice":
                this.getPackingInvoice(context);
                break;
            case "loadInvoice":
                this.loadInvoice(context);
                break;
            default:
                switch (oper)
                {
                    case "del":
                        this.del(context);
                        break;
                    case "add":
                        this.add(context);
                        break;
                    case "edit":
                        this.edit(context);
                        break;
                    default:
                        this.loadInvoice(context);
                        break;
                }
                break;
        }
    }

    public void getPackingInvoice(HttpContext context)
    {
        string c_chitietthuchi_id = context.Request.QueryString["c_chitietthuchi_id"];
        c_chitietthuchi cttc = db.c_chitietthuchis.FirstOrDefault(p => p.c_chitietthuchi_id.Equals(c_chitietthuchi_id));

        string sql = string.Format("select c_packinginvoice_id, so_pkl from c_packinginvoice where c_packinginvoice_id IN( select c_packinginvoice_id from c_dongpklinv where c_donhang_id = '{0}' )", cttc.c_donhang_id);

        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    /*public void edit(HttpContext context)
    {
        try
        {
            string c_packinginvoice_id = context.Request.Form["id"];
            decimal tiendatcoc = decimal.Parse(context.Request.Form["tiendatcoc"]);
            decimal totalgross = decimal.Parse(context.Request.Form["totalgross"]);
            string sqlcheck = @"select count(*) from c_dongphanboinv where c_chitietthuchi_id = '" + cttc_id + "' and c_packinginvoice_id = '" + c_packinginvoice_id + "'";
            int count = (int)mdbc.ExecuteScalar(sqlcheck);
            c_chitietthuchi ct = db.c_chitietthuchis.FirstOrDefault(p => p.c_chitietthuchi_id.Equals(cttc_id));

            string strSumTienCoc = @"select sum(sotienphanbo)
                            from c_dongphanboinv
                            where c_packinginvoice_id='" + c_packinginvoice_id + "'";
            decimal decSumTienCoc = (decimal)mdbc.ExecuteScalar(strSumTienCoc);

            if (ct.sotien.Value >= decSumTienCoc)
            {
                if (count > 0)
                {
                    var obj = db.c_dongphanboinvs.Single(c => c.c_packinginvoice_id.Equals(c_packinginvoice_id) && c.c_chitietthuchi_id.Equals(cttc_id));
                    obj.sotienphanbo = tiendatcoc;
                    db.SubmitChanges();
                    mdbc.ExcuteNonQuery(@"update c_packinginvoice set ngaycapnhat = getdate() where c_packinginvoice_id = '" + c_packinginvoice_id + "'");
                }
                else
                {
                    c_dongphanboinv newobj = new c_dongphanboinv();
                    newobj.c_dongphanboinv_id = ImportUtils.getNEWID();
                    newobj.c_chitietthuchi_id = cttc_id;
                    newobj.c_donhang_id = null;
                    newobj.c_packinginvoice_id = c_packinginvoice_id;
                    newobj.sotienphanbo = tiendatcoc;
                    newobj.totalgross = totalgross;

                    newobj.ngaytao = DateTime.Now;
                    newobj.nguoitao = UserUtils.getUser(context);
                    newobj.ngaycapnhap = DateTime.Now;
                    newobj.nguoicapnhap = UserUtils.getUser(context);

                    db.c_dongphanboinvs.InsertOnSubmit(newobj);
                    db.SubmitChanges();

                    mdbc.ExcuteNonQuery(@"update c_packinginvoice set ngaycapnhat = getdate() where c_packinginvoice_id = '" + c_packinginvoice_id + "'");
                }
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Tổng tiền đã cọc phải nhỏ hơn hoặc bằng: " + ct.sotien + " " + decSumTienCoc);
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }

    }*/

    public void edit(HttpContext context)
    {
        try
        {
            string c_dongphanboinv_id = context.Request.Form["id"];
            string c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"];
            decimal tiendatcoc = decimal.Parse(context.Request.Form["tiendatcoc"]);
            decimal totalgross = decimal.Parse(context.Request.Form["totalgross"]);
            String c_chitietthuchi_id = context.Request.QueryString["cttc"];

            c_chitietthuchi ct = db.c_chitietthuchis.FirstOrDefault(p => p.c_chitietthuchi_id.Equals(cttc_id));

            decimal decSumTienCoc = 0;
            var dpbs = from dpbi in db.c_dongphanboinvs
                       where dpbi.c_packinginvoice_id.Equals(c_packinginvoice_id)
                       && dpbi.c_dongphanboinv_id != c_dongphanboinv_id
                       select dpbi;

            if (dpbs.Count() > 0)
            {
                decSumTienCoc = dpbs.Sum(p => p.sotienphanbo).Value;
            }

            string sqlKhoanPhi = string.Format(@"select isnull(SUM(sotien), 0) as khoanphi from c_chiphilienquan 
                        where c_donhang_id IN(
	                        select c_donhang_id from c_chitietthuchi 
	                        where c_chitietthuchi_id = '{0}'
                        )", c_chitietthuchi_id);

            decimal sumKhoanPhi = (decimal)mdbc.ExecuteScalar(sqlKhoanPhi);

            if ((decSumTienCoc + tiendatcoc) <= (ct.sotien.Value + sumKhoanPhi))
            {
                c_dongphanboinv obj = db.c_dongphanboinvs.FirstOrDefault(p => p.c_dongphanboinv_id.Equals(c_dongphanboinv_id));
                obj.sotienphanbo = tiendatcoc;
				db.SubmitChanges();
				
				c_packinginvoice pklu = db.c_packinginvoices.FirstOrDefault(p=>p.c_packinginvoice_id.Equals(c_packinginvoice_id));
				pklu.ngaycapnhat = DateTime.Now;
				pklu.mota = pklu.mota;
				db.SubmitChanges();
                //mdbc.ExcuteNonQuery(@"update c_packinginvoice set ngaycapnhat = getdate() where c_packinginvoice_id = '" + c_packinginvoice_id + "'");
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Tổng tiền đã cọc phải nhỏ hơn hoặc bằng: " + (ct.sotien.Value + sumKhoanPhi));
            }
        }
        catch (Exception ex)
        {
            jqGridHelper.Utils.writeResult(0, ex.Message);
        }
    }

    public void add(HttpContext context)
    {
        String id = context.Request.Form["id"];

        String c_chitietthuchi_id = context.Request.QueryString["cttc"];
        String c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"];
        decimal tiendatcoc = decimal.Parse(context.Request.Form["tiendatcoc"]);

        c_packinginvoice pkl = db.c_packinginvoices.FirstOrDefault(p => p.c_packinginvoice_id.Equals(c_packinginvoice_id));

        c_chitietthuchi ct = db.c_chitietthuchis.FirstOrDefault(p => p.c_chitietthuchi_id.Equals(cttc_id));

        decimal decSumTienPhanBo = 0;
        var dpbs = from dpbi in db.c_dongphanboinvs
                   where dpbi.c_chitietthuchi_id.Equals(c_chitietthuchi_id)
                   select dpbi;

        if (dpbs.Count() > 0)
        {
            decSumTienPhanBo = dpbs.Sum(p => p.sotienphanbo).Value;
        }

        string sqlKhoanPhi = string.Format(@"select isnull(SUM(sotien), 0) as khoanphi from c_chiphilienquan 
                        where c_donhang_id IN(
	                        select c_donhang_id from c_chitietthuchi 
	                        where c_chitietthuchi_id = '{0}'
                        )", c_chitietthuchi_id);
        
        decimal sumKhoanPhi = (decimal)mdbc.ExecuteScalar(sqlKhoanPhi);

        if ((decSumTienPhanBo + tiendatcoc) <= (ct.sotien.Value + sumKhoanPhi))
        {
            c_dongphanboinv dpb = new c_dongphanboinv();
            dpb.c_dongphanboinv_id = ImportUtils.getNEWID();
            dpb.c_chitietthuchi_id = c_chitietthuchi_id;
            dpb.c_packinginvoice_id = c_packinginvoice_id;
            dpb.totalgross = pkl.totalgross;
            dpb.sotienphanbo = tiendatcoc;

            dpb.ngayphanbo = DateTime.Now;
            dpb.nguoitao = UserUtils.getUser(context);
            dpb.ngaytao = DateTime.Now;
            dpb.nguoicapnhap = UserUtils.getUser(context);
            dpb.ngaycapnhap = DateTime.Now;
			
			db.c_dongphanboinvs.InsertOnSubmit(dpb);
            db.SubmitChanges();
			
			c_packinginvoice pklu = db.c_packinginvoices.FirstOrDefault(p=>p.c_packinginvoice_id.Equals(c_packinginvoice_id));
			pklu.ngaycapnhat = DateTime.Now;
			pklu.mota = pklu.mota;
			db.SubmitChanges();
            //mdbc.ExcuteNonQuery(@"update c_packinginvoice set ngaycapnhat = getdate() where c_packinginvoice_id = '" + c_packinginvoice_id + "'");
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Tổng tiền đã cọc phải nhỏ hơn hoặc bằng: " + (ct.sotien.Value + sumKhoanPhi));
        }
    }

    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        c_dongphanboinv dpb = db.c_dongphanboinvs.FirstOrDefault(p => p.c_dongphanboinv_id.Equals(id));
		db.c_dongphanboinvs.DeleteOnSubmit(dpb);
		db.SubmitChanges();
		
		c_packinginvoice pklu = db.c_packinginvoices.FirstOrDefault(p=>p.c_packinginvoice_id.Equals(dpb.c_packinginvoice_id));
		pklu.ngaycapnhat = DateTime.Now;
		pklu.mota = pklu.mota;
		db.SubmitChanges();
        //mdbc.ExcuteNonQuery(@"update c_packinginvoice set ngaycapnhat = getdate() where c_packinginvoice_id = '" + dpb.c_packinginvoice_id + "'");
    }

    public void loadInvoice(HttpContext context)
    {

        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        c_chitietthuchi ct = db.c_chitietthuchis.Single(c => c.c_chitietthuchi_id.Equals(cttc_id));
        String sqlCount = @"select count(*) from c_dongphanboinv
                            where c_packinginvoice_id in (select distinct c_packinginvoice_id from c_dongpklinv where c_donhang_id = '" + ct.c_donhang_id + "')";
        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        bool search = bool.Parse(context.Request.QueryString["_search"]);
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
        string filter = "";
        if (search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        string strsql = @"select dpb.c_dongphanboinv_id, so_pkl, pkl.totalgross, sotienphanbo, tiendatra  
                            from c_dongphanboinv dpb, c_packinginvoice pkl
                            where 
                                dpb.c_packinginvoice_id =  pkl.c_packinginvoice_id
                                and dpb.c_packinginvoice_id in (select distinct c_packinginvoice_id from c_dongpklinv where c_donhang_id = '" + ct.c_donhang_id + @"')
                            ";

        strsql = strsql.Replace("{dk}", filter);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_dongphanboinv_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_pkl"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalgross"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sotienphanbo"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tiendatra"] + "]]></cell>";
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
