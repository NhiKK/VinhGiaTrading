using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class NhaCungUngMacDinhImport : ImportOriginal
{
    private bool _macdinh;
    private String _md_doitackinhdoanh_id, _md_sanpham_id;

    public String Md_doitackinhdoanh_id
    {
        get { return _md_doitackinhdoanh_id; }
        set { _md_doitackinhdoanh_id = value; }
    }

    public String Md_sanpham_id
    {
        get { return _md_sanpham_id; }
        set { _md_sanpham_id = value; }
    }

    public bool Macdinh
    {
        get { return _macdinh; }
        set { _macdinh = value; }
    }

    public NhaCungUngMacDinhImport(String md_sanpham_id, String md_doitackinhdoanh_id, bool macdinh)
        : this(md_sanpham_id, md_doitackinhdoanh_id, macdinh, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public NhaCungUngMacDinhImport(String md_sanpham_id, String md_doitackinhdoanh_id, bool macdinh, DateTime ngaytao, String nguoitao, DateTime ngaycapnhat, String nguoicapnhat, bool hoatdong)
    {
        this._md_sanpham_id = md_sanpham_id;
        this._md_doitackinhdoanh_id = md_doitackinhdoanh_id;
        this._macdinh = macdinh;
        this.nguoiTao = nguoitao;
        this.ngayTao = ngaytao;
        this.ngayCapNhat = ngaycapnhat;
        this.nguoiCapNhat = nguoicapnhat;
        this.hoatDong = hoatdong;
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Import()
    {
        try
        {
            md_nhacungungmacdinh n = new md_nhacungungmacdinh();
            n.md_nhacungungmacdinh_id = ImportUtils.getNEWID();
            n.md_sanpham_id = _md_sanpham_id;
            n.md_doitackinhdoanh_id = _md_doitackinhdoanh_id;
            n.macdinh = _macdinh;
            n.ngaytao = ngayTao;
            n.nguoitao = nguoiTao;
            n.ngaycapnhat = ngayCapNhat;
            n.nguoicapnhat = nguoiCapNhat;
            n.hoatdong = hoatDong;

            db.md_nhacungungmacdinhs.InsertOnSubmit(n);
            db.SubmitChanges();
        }catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Update()
    {
        try
        {
            md_nhacungungmacdinh n = db.md_nhacungungmacdinhs.Single(p => p.md_sanpham_id.Equals(_md_sanpham_id) && p.md_doitackinhdoanh_id.Equals(_md_doitackinhdoanh_id));
            n.macdinh = _macdinh;
            n.ngaycapnhat = ngayCapNhat;
            n.nguoicapnhat = nguoiCapNhat;
            n.hoatdong = hoatDong;

            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
