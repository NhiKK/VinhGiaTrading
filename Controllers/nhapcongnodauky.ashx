<%@ WebHandler Language="C#" Class="nhapcongnodauky" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class nhapcongnodauky : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "NhapDauKy":
                this.nhapdauky(context);
                break;
            default:
                context.Response.Write("Không tìm thấy phương thức mặc định");
                break;
        }
    }

    public void nhapdauky(HttpContext context){
        try {
            LinqDBDataContext db = new LinqDBDataContext();
            string md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
            string a_kytrongnam_id = context.Request.Form["a_kytrongnam_id"];
            // lay ra thong tin dong mo ky ke toan
            a_dongmoky dongmoky = db.a_dongmokies.Single(d=>d.a_dongmoky_id.Equals(a_kytrongnam_id));
            string sodu = context.Request.Form["sodu"];
            string isnoco = context.Request.Form["isnoco"];
            int check = (from i in db.a_tonghopcongnos
                        where i.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id)
                            && i.a_kytrongnam_id.Equals(dongmoky.a_kytrongnam_id)
                        select new { i.a_tonghopcongno_id}).Count();
            if (check > 0)
            {
                var obj = db.a_tonghopcongnos.SingleOrDefault(a => a.a_kytrongnam_id.Equals(dongmoky.a_kytrongnam_id) && a.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id));
                if (isnoco.Equals("1"))
                {
                    obj.nodauky = decimal.Parse(sodu);
                }
                else
                {
                    obj.codauky = decimal.Parse(sodu);
                }
                obj.nocuoiky = obj.nodauky + obj.notrongky - obj.cotrongky;
                obj.cocuoiky = obj.codauky + obj.cotrongky - obj.notrongky;
                
                db.SubmitChanges();
            }
            else { 
                a_tonghopcongno cn = new a_tonghopcongno();
                a_kytrongnam k = db.a_kytrongnams.Single(ky => ky.a_kytrongnam_id.Equals(dongmoky.a_kytrongnam_id));
                cn.a_tonghopcongno_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss"));
                if (isnoco.Equals("1"))
                {
                    cn.nodauky = decimal.Parse(sodu);
                    cn.codauky = 0;
                }
                else
                {
                    cn.nodauky = 0;
                    cn.codauky = decimal.Parse(sodu);
                }
                cn.notrongky = 0; cn.cotrongky = 0;
                cn.nocuoiky = cn.nodauky;
                cn.cocuoiky = cn.codauky;
                cn.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                cn.a_namtaichinh_id = k.a_namtaichinh_id;
                cn.a_kytrongnam_id = k.a_kytrongnam_id;
                cn.mota = "Nhập số dư";
                
                cn.ngaytao = DateTime.Now;
                cn.nguoitao = UserUtils.getUser(context);
                cn.ngaycapnhat = DateTime.Now;
                cn.nguoicapnhap = UserUtils.getUser(context);

                db.a_tonghopcongnos.InsertOnSubmit(cn);
                db.SubmitChanges();
            }
            jqGridHelper.Utils.writeResult(1, "Cập nhập thành công!");
        }catch(Exception e){
            jqGridHelper.Utils.writeResult(0, "Cập nhập thành công!");
        }
    }
    public bool IsReusable
    {
        get { return false; }
    }
}