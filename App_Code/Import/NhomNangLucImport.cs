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


public class NhomNangLucImport : ImportOriginal
{
    private String md_chungloai_id, heHang, maNhom, moTaTiengViet, hsCode, moTa;
	private int tGLH;

    public String HeHang
    {
        get { return heHang; }
        set { heHang = value; }
    }

    public String HsCode
    {
        get { return hsCode; }
        set { hsCode = value; }
    }

    public String MoTaTiengViet
    {
        get { return moTaTiengViet; }
        set { moTaTiengViet = value; }
    }

    public String Md_chungloai_id
    {
        get { return md_chungloai_id; }
        set { md_chungloai_id = value; }
    }

    public String MaNhom
    {
        get { return maNhom; }
        set { maNhom = value; }
    }

	public int TGLH
    {
        get { return tGLH; }
        set { tGLH = value; }
    }
	
	public String MoTa
    {
        get { return moTa; }
        set { moTa = value; }
    }
    
	public NhomNangLucImport()
    {
    
    }

    public NhomNangLucImport(String md_chungloai_id, String heHang, String maNhom, String moTaTiengViet, String hsCode, int tGLH, String moTa)
        : this(md_chungloai_id, heHang, maNhom, moTaTiengViet, hsCode, tGLH, moTa, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public NhomNangLucImport(String md_chungloai_id, String heHang, String maNhom, String moTaTiengViet, String hsCode, int tGLH, String moTa,
	DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.md_chungloai_id = md_chungloai_id;
        this.heHang = heHang;
        this.maNhom = maNhom;
        this.moTaTiengViet = moTaTiengViet;
        this.hsCode = hsCode;
        this.tGLH = tGLH;
        this.moTa = moTa;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        md_nhomnangluc nnl = new md_nhomnangluc();
        nnl.md_nhomnangluc_id = ImportUtils.getNEWID();
        nnl.md_chungloai_id = md_chungloai_id;
        nnl.hehang = heHang;
        nnl.nhom = maNhom;
        nnl.mota_tiengviet = moTaTiengViet;
        nnl.hscode = hsCode;
		nnl.thoigianlamhang = tGLH;
		nnl.mota = moTa;
        nnl.ngaytao = ngayTao;
        nnl.nguoitao = nguoiTao;
        nnl.ngaycapnhat = ngayCapNhat;
        nnl.nguoicapnhat = nguoiCapNhat;
        nnl.hoatdong = hoatDong;

        db.md_nhomnanglucs.InsertOnSubmit(nnl);
        db.SubmitChanges();
    }

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_nhomnangluc o = db.md_nhomnanglucs.FirstOrDefault(p => p.nhom.Equals(maNhom) && p.hehang.Equals(heHang));
        o.md_chungloai_id = md_chungloai_id;
		o.hehang = heHang;
        o.mota_tiengviet = moTaTiengViet;
        o.hscode = hsCode;
		o.thoigianlamhang = tGLH;
		o.mota = moTa;
        o.ngaycapnhat = ngayCapNhat;
        o.nguoicapnhat = nguoiCapNhat;
        db.SubmitChanges();
    }
}
