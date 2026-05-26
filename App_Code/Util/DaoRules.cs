using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class DaoRules
{
    private HttpContext _context;
    LinqDBDataContext db = new LinqDBDataContext();
    public HttpContext Context
    {
        get { return _context; }
        set { _context = value; }
    }

    public DaoRules() { }

	public DaoRules(HttpContext context)
	{
        this._context = context;
	}

    /// <summary>
    /// Lấy phân quyền
    /// </summary>
    /// <returns></returns>
    public phanquyen GetRoles()
    {
        try
        {
            if (this.GetUser().isadmin.Equals(true))
            {
                phanquyen p = new phanquyen();
                p.mapq = ImportUtils.getNEWID();
                p.mavt = this.GetUser().mavt;
                p.mamenu = this.GetMenu().mamenu;
                p.xem = true;
                p.them = true;
                p.sua = true;
                p.xoa = true;
                p.hoatdong = true;

                return p;
            }
            else
            {
                phanquyen pq = db.phanquyens.FirstOrDefault(
                        p => p.mamenu.Equals(this.GetMenu().mamenu) && p.mavt.Equals(this.GetUser().mavt));
                return pq;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Lấy user id trong context
    /// </summary>
    /// <returns></returns>
    public String GetUserId()
    {
        return _context.User.Identity.Name;
    }

    /// <summary>
    /// Lấy user trong context
    /// </summary>
    /// <returns></returns>
    public nhanvien GetUser()
    {
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(this.GetUserId()));
        return nv;
    }

    /// <summary>
    /// Lấy menu thông qua tên trang web
    /// </summary>
    /// <returns></returns>
    public menu GetMenu()
    {
        menu mnu = db.menus.FirstOrDefault(p => p.url.Equals(this.GetFileName()));
        return mnu;
    }

    /// <summary>
    /// Lấy tên trang web
    /// </summary>
    /// <returns></returns>
    public String GetFileName()
    {
        String _url =_context.Request.Url.AbsolutePath;
        int i = _url.LastIndexOf('/');
        String str = _url.Substring(i + 1);
        return str;
    }

    public static bool GetRule(HttpContext context, String rule)
    {
        DaoRules rules = new DaoRules(context);
        bool ret = true;
        switch (rule)
        {
            case "xem":
                ret = rules.GetRoles().xem.Value;
                break;
            case "them":
                ret = rules.GetRoles().them.Value;
                break;
            case "sua":
                ret = rules.GetRoles().sua.Value;
                break;
            case "xoa":
                ret = rules.GetRoles().xoa.Value;
                break;
            case "truyxuat":
                ret = true;
                break;
            default:
                throw new Exception("rule phải thuộc 1 trong tập hợp sau: xem, them, sua, xoa");
        }
        return ret;
    }

    public static bool GetRuleView(HttpContext context)
    {
        return DaoRules.GetRule(context, "xem");
    }

    public static bool GetRuleAdd(HttpContext context)
    {
        return DaoRules.GetRule(context, "them");
    }

    public static bool GetRuleEdit(HttpContext context)
    {
        return DaoRules.GetRule(context, "sua");
    }

    public static bool GetRuleDel(HttpContext context)
    {
        return DaoRules.GetRule(context, "xoa");
    }
}
