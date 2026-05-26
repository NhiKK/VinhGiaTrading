<%@ WebHandler Language="C#" Class="MoDongKyController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class MoDongKyController : IHttpHandler
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
            case "OC":
                this.dongMoKy(context);
                break;
            case "TMCK":
                this.TMCK(context);
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
        String sql = "select a_dongmoky_id, a_dongmoky_id from a_dongmoky where hoatdong = 1";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        a_dongmoky m = db.a_dongmokies.Single(p => p.a_dongmoky_id == context.Request.Form["id"]);
        m.a_namtaichinh_id = context.Request.Form["tennam"];
        m.a_kytrongnam_id = context.Request.Form["tenky"];
        m.ky_hoatdong = context.Request.Form["ky_hoatdong"];
        m.daxuly = false;
        
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

        a_dongmoky mnu = new a_dongmoky
        {
            a_dongmoky_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            a_namtaichinh_id = context.Request.Form["tennam"],
            a_kytrongnam_id = context.Request.Form["tenky"],
            ky_hoatdong = context.Request.Form["ky_hoatdong"],
            daxuly = false,
            daxuly2 = false,
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.a_dongmokies.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
       LinqDBDataContext db = new LinqDBDataContext(); 
        String id = context.Request.Form["id"];
        
       /* id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");*/

        a_dongmoky dmk = db.a_dongmokies.SingleOrDefault(p => p.a_dongmoky_id == id);

        if (dmk != null)
        {
			if(dmk.daxuly == false & dmk.daxuly2 == false)
			{
				db.a_dongmokies.DeleteOnSubmit(dmk);
			}
			else
			{
				foreach (a_tonghopcongno thcn in db.a_tonghopcongnos.ToList().Where(s=>s.a_kytrongnam_id == dmk.a_kytrongnam_id
					& s.a_namtaichinh_id == dmk.a_namtaichinh_id))
				{
					db.a_tonghopcongnos.DeleteOnSubmit(thcn);
				}

				foreach (a_dongmoky dmk_list in db.a_dongmokies.ToList().Where(s=>s.a_kytrongnam_id == dmk.a_kytrongnam_id
					& s.a_namtaichinh_id == dmk.a_namtaichinh_id))
				{
					db.a_dongmokies.DeleteOnSubmit(dmk_list);
				}
			}
        }
		
       db.SubmitChanges();
    }
    
    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        String sqlCount = "SELECT COUNT(*) AS count FROM a_dongmoky dmk "+
						  "left join a_namtaichinh ntc on dmk.a_namtaichinh_id = ntc.a_namtaichinh_id " +
						  "left join a_kytrongnam ktn on dmk.a_kytrongnam_id = ktn.a_kytrongnam_id " +
						  "WHERE 1=1 " + filter;
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
            sidx = "dmk.ngaytao";
        }

        string strsql = "select * from " +
						"( select dmk.a_dongmoky_id, ntc.ten_namtaichinh as tennam, " +
						"		 ktn.tenky as tenky, dmk.ky_hoatdong, dmk.ngaytao, dmk.nguoitao, dmk.ngaycapnhat, " +
						"		 dmk.nguoicapnhat, dmk.mota, dmk.hoatdong, dmk.daxuly, dmk.daxuly2, " +
						"		 ROW_NUMBER() OVER (ORDER BY dmk.ngaytao " + sord + ") " +
						"		 as RowNum FROM a_dongmoky dmk " +
						"		 left join a_namtaichinh ntc on dmk.a_namtaichinh_id = ntc.a_namtaichinh_id " +
						"		 left join a_kytrongnam ktn on dmk.a_kytrongnam_id = ktn.a_kytrongnam_id " +
						"		 WHERE 1=1 " + filter + ")P where RowNum > @start AND RowNum < @end ";

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
            xml += "<cell><![CDATA[" + row[8] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[9] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[10] + "]]></cell>";
            xml += "<cell><![CDATA[" + row[11] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }

    //nhan
    public void dongMoKy(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        string msg = "";
        try
        {
            string dongmoky_id = context.Request.QueryString["dongmoky_id"];
            var dongmoky = db.a_dongmokies.SingleOrDefault(d => d.a_dongmoky_id.Equals(dongmoky_id));

			
            if (dongmoky.daxuly.Value.Equals(false) | dongmoky.daxuly2.Value.Equals(false))
            {
                bool check_kh = bool.Parse(context.Request.QueryString["khachhang"]);
                bool check_ncc = bool.Parse(context.Request.QueryString["nhacungcap"]);



                // db.CommandTimeout = 900000000;
                var kyketoan = db.a_kytrongnams.Single(k => k.a_kytrongnam_id.Equals(dongmoky.a_kytrongnam_id));
                string ngaybatdau = kyketoan.ngaybatdau.Value.ToString("MM/dd/yyyy");
                string ngayketthuc = kyketoan.ngayketthuc.Value.ToString("MM/dd/yyyy");
				
				if (dongmoky.ky_hoatdong.Equals("MOKY"))
                {
                    if (dongmoky.daxuly.Value.Equals(true) & check_kh.Equals(true))
                    {
                        msg = "Đã mở kỳ này cho khách hàng rồi!";
                    }
                    else if (dongmoky.daxuly2.Value.Equals(true) & check_ncc.Equals(true))
                    {
                        msg = "Đã mở kỳ này cho nhà cung cấp rồi!";
                    }
                    else if (dongmoky.daxuly2.Value.Equals(true) & (check_ncc.Equals(false) & check_kh.Equals(false)))
                    {
                        msg = "Đã mở kỳ này cho nhà cung cấp rồi!";
                    }
                    else if (dongmoky.daxuly.Value.Equals(true) & (check_ncc.Equals(false) & check_kh.Equals(false)))
                    {
                        msg = "Đã mở kỳ này cho khách hàng rồi!";
                    }
                    else if (dongmoky.daxuly2.Value.Equals(true) & (check_ncc.Equals(true) & check_kh.Equals(true)))
                    {
                        msg = "Đã mở kỳ này cho nhà cung cấp rồi!";
                    }
                    else if (dongmoky.daxuly.Value.Equals(true) & (check_ncc.Equals(true) & check_kh.Equals(true)))
                    {
                        msg = "Đã mở kỳ này cho khách hàng rồi!";
                    }
                    else
                    {
                        //mdbc.ExecuteScalarProcedure("p_chuyencongno_mk", "@kymo_id", dongmoky.a_kytrongnam_id, "@username", UserUtils.getUser(context));

                        mokyketoan(dongmoky.a_kytrongnam_id, context, check_kh, check_ncc);
                        if (check_kh == false & check_ncc == false)
                        {
                            dongmoky.daxuly = true;
                            dongmoky.daxuly2 = true;
                        }

                        if (check_kh == true)
                            dongmoky.daxuly = true;

                        if (check_ncc == true)
                            dongmoky.daxuly2 = true;
                        msg = "Xử lý mở kỳ thành công!";
                    }
                }
                else
                {
                    if (dongmoky.daxuly.Value.Equals(true) & check_kh.Equals(true))
                    {
                        msg = "Đã đóng kỳ này cho khách hàng rồi!";
                    }
                    else if (dongmoky.daxuly2.Value.Equals(true) & check_ncc.Equals(true))
                    {
                        msg = "Đã đóng kỳ này cho nhà cung cấp rồi!";
                    }
                    else if (dongmoky.daxuly2.Value.Equals(true) & (check_ncc.Equals(false) & check_kh.Equals(false)))
                    {
                        msg = "Đã đóng kỳ này cho nhà cung cấp rồi!";
                    }
                    else if (dongmoky.daxuly.Value.Equals(true) & (check_ncc.Equals(false) & check_kh.Equals(false)))
                    {
                        msg = "Đã đóng kỳ này cho khách hàng rồi!";
                    }
                    else if (dongmoky.daxuly2.Value.Equals(true) & (check_ncc.Equals(true) & check_kh.Equals(true)))
                    {
                        msg = "Đã đóng kỳ này cho nhà cung cấp rồi!";
                    }
                    else if (dongmoky.daxuly.Value.Equals(true) & (check_ncc.Equals(true) & check_kh.Equals(true)))
                    {
                        msg = "Đã đóng kỳ này cho khách hàng rồi!";
                    }
                    else
                    {
                        dongkyketoan(dongmoky.a_kytrongnam_id, UserUtils.getUser(context), context, check_kh, check_ncc);
                        if (check_kh == false & check_ncc == false)
                        {
                            dongmoky.daxuly = true;
                            dongmoky.daxuly2 = true;
                        }

                        if (check_kh == true)
                            dongmoky.daxuly = true;

                        if (check_ncc == true)
                            dongmoky.daxuly2 = true;
                        msg = "Xử lý đóng kỳ thành công!";
                    }
                }
                db.SubmitChanges();
            }
            else
            {
                msg = "Không thể xử lý lại kỳ kế toán này!";
            }
			
        }
        catch (Exception ex)
        {
           msg = "Lỗi khi thực hiện!" + ex.Message;
        }

        context.Response.Write(msg);
    }
    
    public void mokyketoan(string kymo, HttpContext context, bool check_kh, bool check_ncc)
    {
        a_kytrongnam kmo = db.a_kytrongnams.SingleOrDefault(p => p.a_kytrongnam_id == kymo);
        a_kytrongnam kdong = db.a_kytrongnams.SingleOrDefault(p => p.ngayketthuc.Value == kmo.ngaybatdau.Value.AddDays(-1));
        var doitackinhdoanh = db.md_doitackinhdoanhs.ToList();
        if (check_kh == true & check_ncc == false)
        {
            doitackinhdoanh = db.md_doitackinhdoanhs.Where(s => s.isncc == false).ToList();
        }

        if (check_kh == false & check_ncc == true)
        {
            doitackinhdoanh = db.md_doitackinhdoanhs.Where(s => s.isncc == true).ToList();
        }
        foreach (md_doitackinhdoanh dtkd in doitackinhdoanh)
        {
            a_tonghopcongno thcn = db.a_tonghopcongnos.Where(p => p.a_kytrongnam_id == kdong.a_kytrongnam_id & p.md_doitackinhdoanh_id == dtkd.md_doitackinhdoanh_id).FirstOrDefault();
            a_tonghopcongno thcn_new = new a_tonghopcongno();
            thcn_new.a_tonghopcongno_id = Guid.NewGuid().ToString().Replace("-", "");
            thcn_new.a_kytrongnam_id = kmo.a_kytrongnam_id;
            thcn_new.a_namtaichinh_id = kmo.a_namtaichinh_id;
            thcn_new.ma_dtkd = dtkd.ma_dtkd;
            thcn_new.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
            if (thcn != null)
            {
                //dauky
                thcn_new.nodauky = thcn.nocuoiky;
                thcn_new.nodauky_usd = thcn.nocuoiky_usd;
                thcn_new.nodauky_vnd = thcn.nocuoiky_vnd;

                thcn_new.codauky = thcn.cocuoiky;
                thcn_new.codauky_usd = thcn.cocuoiky_usd;
                thcn_new.codauky_vnd = thcn.cocuoiky_vnd;
                //trongky
                thcn_new.notrongky = 0;
                thcn_new.notrongky_usd = 0;
                thcn_new.notrongky_vnd = 0;

                thcn_new.cotrongky = 0;
                thcn_new.cotrongky_usd = 0;
                thcn_new.cotrongky_vnd = 0;
                //cuoiky
                thcn_new.nocuoiky = thcn.nocuoiky;
                thcn_new.nocuoiky_usd = thcn.nocuoiky_usd;
                thcn_new.nodauky_vnd = thcn.nocuoiky_vnd;

                thcn_new.cocuoiky = thcn.cocuoiky;
                thcn_new.cocuoiky_usd = thcn.cocuoiky_usd;
                thcn_new.cocuoiky_vnd = thcn.cocuoiky_vnd;
            }
            else
            {
                //dauky
                thcn_new.nodauky = 0;
                thcn_new.nodauky_usd = 0;
                thcn_new.nodauky_vnd = 0;

                thcn_new.codauky = 0;
                thcn_new.codauky_usd = 0;
                thcn_new.codauky_vnd = 0;

                //trongky
                thcn_new.notrongky = 0;
                thcn_new.notrongky_usd = 0;
                thcn_new.notrongky_vnd = 0;

                thcn_new.cotrongky = 0;
                thcn_new.cotrongky_usd = 0;
                thcn_new.cotrongky_vnd = 0;
                //cuoiky
                thcn_new.nocuoiky = 0;
                thcn_new.nocuoiky_usd = 0;
                thcn_new.nodauky_vnd = 0;

                thcn_new.cocuoiky = 0;
                thcn_new.cocuoiky_usd = 0;
                thcn_new.cocuoiky_vnd = 0;
            }

            thcn_new.mota = "";
            thcn_new.ngaytao = DateTime.Now;
            thcn_new.ngaycapnhat = DateTime.Now;
            thcn_new.nguoitao = UserUtils.getUser(context);
            thcn_new.nguoicapnhap = UserUtils.getUser(context);
            db.a_tonghopcongnos.InsertOnSubmit(thcn_new);
        }
        db.SubmitChanges(); 
    }
    
    public void TMCK(HttpContext context)
    {
        string msg = "";
        try
        {
            LinqDBDataContext db = new LinqDBDataContext();
            
            a_namtaichinh ntc = db.a_namtaichinhs.Where(s => s.hoatdong == true).OrderBy(s => s.nam).FirstOrDefault();
            a_kytrongnam ktd = db.a_kytrongnams.Where(s => s.a_namtaichinh_id == ntc.a_namtaichinh_id).OrderByDescending(s => s.soky).FirstOrDefault();
            a_kytrongnam ktm = db.a_kytrongnams.Where(s => s.a_namtaichinh_id == ntc.a_namtaichinh_id).OrderBy(s => s.soky).FirstOrDefault();
			mdbc.ExecuteScalarProcedure("p_chuyencongno_mk", "@kymo_id", ktd.a_kytrongnam_id, "@username", UserUtils.getUser(context));
			if (db.a_tonghopcongnos.Count() <= 0)
            {
				
                foreach (md_doitackinhdoanh dtkd in db.md_doitackinhdoanhs.ToList())
                {
                    a_tonghopcongno cn = new a_tonghopcongno();

                    cn.a_tonghopcongno_id = db.ExecuteQuery<string>("select replace(NEWID(),'-','')").Single();
                    cn.nodauky = 0;
                    cn.codauky = decimal.Parse("0");
                    cn.notrongky = 0;
					cn.cotrongky = 0;
                    cn.nocuoiky = cn.nodauky;
                    cn.cocuoiky = cn.codauky;
                    cn.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
                    cn.ma_dtkd = dtkd.ma_dtkd;
                    cn.a_namtaichinh_id = ntc.a_namtaichinh_id;
                    cn.a_kytrongnam_id = ktd.a_kytrongnam_id;
                    cn.mota = "Nhập số dư";

                    cn.ngaytao = DateTime.Now;
                    cn.nguoitao = UserUtils.getUser(context);
                    cn.ngaycapnhat = DateTime.Now;
                    cn.nguoicapnhap = UserUtils.getUser(context);

                    db.a_tonghopcongnos.InsertOnSubmit(cn);
                }
                db.SubmitChanges();
                dongkyketoan(ktd.a_kytrongnam_id, UserUtils.getUser(context), context, true, true);
				
                msg = "Xử lý thành công!";
            }
            else
            {
                msg = "Xử lý thất bại, phải xóa hết dữ liệu trong tổng hợp công nợ!";
            }
        }
        catch (Exception ex)
        {
            msg = "Lỗi khi thực hiện!" + ex.Message;
        }

        context.Response.Write(msg);
    }

    public void dongkyketoan(string kyketoan, string username, HttpContext context,bool check_kh, bool check_ncc)
    {
        //tinhcongnophaitra
           
        decimal nodauky, codauky, notrongky, cotrongky;


        a_kytrongnam ktn = db.a_kytrongnams.SingleOrDefault(p => p.a_kytrongnam_id == kyketoan);
        
        var doitackinhdoanh = db.md_doitackinhdoanhs.ToList();
        if (check_kh == true & check_ncc == false)
        {
            doitackinhdoanh = db.md_doitackinhdoanhs.Where(s=>s.isncc == false).ToList();
        }
        
        if(check_kh == false & check_ncc == true)
        {
            doitackinhdoanh = db.md_doitackinhdoanhs.Where(s=>s.isncc == true).ToList();
        }

        foreach (md_doitackinhdoanh dtkd in doitackinhdoanh)
        {
            a_tonghopcongno thcn = db.a_tonghopcongnos.SingleOrDefault(p => p.a_kytrongnam_id == ktn.a_kytrongnam_id & p.md_doitackinhdoanh_id == dtkd.md_doitackinhdoanh_id);

            //if (thcn != null)
            //{
                try { nodauky = decimal.Parse(thcn.nodauky.ToString()); }
                catch { nodauky = 0; }
                try { codauky = decimal.Parse(thcn.codauky.ToString()); }
                catch { codauky = 0; }

                string kq = "";
                if (dtkd.isncc == true)
                {
                    kq = tinhphatsinhphaitra(ktn.ngaybatdau.Value, ktn.ngayketthuc.Value, dtkd.md_doitackinhdoanh_id);
                }
                else
                {
                    kq = tinhphatsinhphaithu(ktn.ngaybatdau.Value, ktn.ngayketthuc.Value, dtkd.md_doitackinhdoanh_id);
                }

                notrongky = decimal.Parse(kq.Split('#')[0].ToString());
                cotrongky = decimal.Parse(kq.Split('#')[1].ToString());

                thcn.notrongky = notrongky;
                thcn.cotrongky = cotrongky;

                if ((nodauky + notrongky) < (codauky + cotrongky))
                {
                    thcn.cocuoiky = (codauky + cotrongky) - (nodauky + notrongky);
                    thcn.nocuoiky = 0;
                }
                else
                {
                    thcn.nocuoiky = (nodauky + notrongky) - (codauky + cotrongky);
                    thcn.cocuoiky = 0;
                }
            //}
        }
        db.SubmitChanges();
    }


        public string tinhphatsinhphaitra(DateTime nbd, DateTime nkt, string md_doitackinhdoanh_id)
    {
        decimal phaitra, datra, thulai;

     //phaitra
     /*   var result = from p in db.c_packinginvoices
                     from c in db.c_dongpklinvs
                     where p.c_packinginvoice_id == c.c_packinginvoice_id &
                     p.md_trangthai_id.Equals("HIEULUC") & p.ngay_motokhai >= nbd & p.ngay_motokhai <= nkt
                     select new
                     {
                         c_dongnhapxuat_id = c.c_dongnhapxuat_id
                     };

        
         var kq1= from p in db.c_dongnhapxuats
                  join c in result on p.c_dongnhapxuat_id equals c.c_dongnhapxuat_id
                  select new{ dongnhapxuat_ref = p.dongnhapxuat_ref};

         
        var kq2 = from p in db.c_dongnhapxuats
                  join c in kq1 on p.c_dongnhapxuat_id equals c.dongnhapxuat_ref
                  select new{ c_nhapxuat_id = p.c_nhapxuat_id};
        
        var kq3 = from p in db.c_nhapxuats
                  join c in kq2.Distinct() on p.c_nhapxuat_id equals c.c_nhapxuat_id
                  where p.md_doitackinhdoanh_id == md_doitackinhdoanh_id
                  select new{ 
                    grandtotal = p.grandtotal,
                    phicong = p.phicong,
                    phitru = p.phitru
                  };
        
         
        decimal total = 0,pc = 0, pt = 0;
        try { total = decimal.Parse(kq3.Distinct().Sum(p=>p.grandtotal).ToString()); } catch{ total = 0; }
        
        try { pc = decimal.Parse(kq3.Distinct().Sum(p=>p.phicong).ToString()); } catch{ pc = 0; }
        
        try { pt = decimal.Parse(kq3.Distinct().Sum(p=>p.phitru).ToString()); } catch{ pt = 0; }
 
        phaitra = total + pc - pt;

        //datra
        var kqtc= from p in db.c_thuchis
                where p.ispaymentin == false & p.md_doitackinhdoanh_id == md_doitackinhdoanh_id 
                & p.ngay_giaonop >= nbd & p.ngay_giaonop <= nkt & p.md_trangthai_id == "HIEULUC"
                select new {sotien = p.sotien};
        
        try { datra = decimal.Parse(kqtc.Sum(p=>p.sotien).ToString()); } catch{ datra = 0; }

        
        var kqtl= from p in db.c_thuchis
                where p.ispaymentin == true & p.md_doitackinhdoanh_id == md_doitackinhdoanh_id
                & p.ngay_giaonop >= nbd & p.ngay_giaonop <= nkt & p.md_trangthai_id == "HIEULUC"
                select new {sotien = p.sotien};
        
        //thulai
        try { thulai = decimal.Parse(kqtl.Sum(p=>p.sotien).ToString()); } catch{ thulai = 0; }
        
        phaitra += thulai;
        return phaitra + "#" + datra + "#";*/
        string sql = string.Format("select * from [dbo].[f_tinhphatsinhphaitra]('{0}', '{1}', '{2}')", nbd, nkt, md_doitackinhdoanh_id);
        System.Data.DataTable tbl_phaitra = mdbc.GetData(sql);
        return tbl_phaitra.Rows[0][0] + "#" + tbl_phaitra.Rows[0][1] + "#";
    }

    public string tinhphatsinhphaithu(DateTime nbd, DateTime nkt, string md_doitackinhdoanh_id)
    {
        decimal phaithu,dathu,chilai;
        /*var kq1 = from p in db.c_packinginvoices
                  where p.ngay_motokhai >= nbd & p.ngay_motokhai <= nkt &
                  p.md_doitackinhdoanh_id == md_doitackinhdoanh_id & p.md_trangthai_id == "HIEULUC"
                  select new { totalgross = p.totalgross };

        try { phaithu = decimal.Parse(kq1.Sum(p => p.totalgross).ToString()); }
        catch { phaithu = 0; }

        var kq2 = from p in db.c_thuchis
                  where p.ngay_giaonop >= nbd & p.ngay_giaonop <= nkt & p.ispaymentin == true &
                  p.md_doitackinhdoanh_id == md_doitackinhdoanh_id & p.md_trangthai_id == "HIEULUC"
                  select new { sotien = p.sotien };

        try { dathu = decimal.Parse(kq2.Sum(p => p.sotien).ToString()); }
        catch { dathu = 0; }

        var kq3 = from p in db.c_thuchis
                  where p.ngay_giaonop >= nbd & p.ngay_giaonop <= nkt & p.ispaymentin == false &
                  p.md_doitackinhdoanh_id == md_doitackinhdoanh_id & p.md_trangthai_id == "HIEULUC"
                  select new { sotien = p.sotien };

        try { chilai = decimal.Parse(kq3.Sum(p => p.sotien).ToString()); }
        catch { chilai = 0; }

        phaithu += chilai;
        return phaithu + "#" + dathu + "#";*/
        string sql = string.Format("select * from [dbo].[f_tinhphatsinhphaithu]('{0}', '{1}', '{2}')", nbd, nkt, md_doitackinhdoanh_id);
        System.Data.DataTable tbl_phaitra = mdbc.GetData(sql);
        return tbl_phaitra.Rows[0][0] + "#" + tbl_phaitra.Rows[0][1] + "#";
    }
    //end-nhan
    public bool IsReusable
    {
        get { return false; }
    }
}
