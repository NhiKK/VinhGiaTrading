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


public class BienTauImport : ImportOriginal
{
    private String maSanPham;
    private String bienTau;

    public String MaSanPham
    {
        get { return maSanPham; }
        set { maSanPham = value; }
    }

    public String BienTau
    {
        get { return bienTau; }
        set { bienTau = value; }
    }

    public BienTauImport(String md_sanpham_id, String bienTau)
        : this(md_sanpham_id, bienTau, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {
    
    }

    public BienTauImport(String md_sanpham_id, String bienTau
        , DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maSanPham = md_sanpham_id;
        this.bienTau = bienTau;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        md_bientau bt = new md_bientau();
        bt.md_bientau_id = ImportUtils.getNEWID();
        bt.md_sanpham_id = maSanPham;
        bt.ma_sanpham_ref = bienTau;
        bt.ngaytao = ngayTao;
        bt.nguoitao = nguoiTao;
        bt.ngaycapnhat = ngayCapNhat;
        bt.nguoicapnhat = nguoiCapNhat;
        bt.hoatdong = hoatDong;

        db.md_bientaus.InsertOnSubmit(bt);
        db.SubmitChanges();
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_bientau bt = db.md_bientaus.FirstOrDefault(p => p.md_sanpham_id.Equals(maSanPham) && p.ma_sanpham_ref.Equals(bienTau));
        if (bt != null)
        {
            bt.ngaycapnhat = ngayCapNhat;
            bt.nguoicapnhat = nguoiCapNhat;
            bt.hoatdong = hoatDong;
            db.SubmitChanges();
        }
    }
}
