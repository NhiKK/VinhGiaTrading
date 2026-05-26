using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ExcelLibrary.SpreadSheet;


public class PhanNhomMauImport : ImportOriginal
{
    public void Import(string maNhomMau, string tenNhomMau, string mota)
    {
        md_nhommau o = new md_nhommau();
        o.md_nhommau_id = ImportUtils.getNEWID();
        o.ma_nhommau = maNhomMau;
        o.ten_nhommau = tenNhomMau;
        o.mota = mota;
        o.hoatdong = true;
        o.nguoitao = "admin";
        o.nguoicapnhat = "admin";
        o.ngaytao = DateTime.Now;
        o.ngaycapnhat = DateTime.Now;
        db.md_nhommaus.InsertOnSubmit(o);
        db.SubmitChanges();
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update(string maNhomMau, string tenNhomMau, string mota)
    {
        md_nhommau o = db.md_nhommaus.FirstOrDefault(p => p.ma_nhommau.Equals(maNhomMau));
        if (o != null)
        {
            o.ten_nhommau = tenNhomMau;
            o.mota = mota;
            o.hoatdong = true;
            o.nguoitao = "admin";
            o.nguoicapnhat = "admin";
            o.ngaytao = DateTime.Now;
            o.ngaycapnhat = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
