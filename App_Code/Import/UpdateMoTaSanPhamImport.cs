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

/// <summary>
/// Summary description for UpdateMoTaSanPhamImport
/// </summary>
public class UpdateMoTaSanPhamImport : ImportOriginal
{
    private String md_sanpham_id, mota_tiengviet, mota_tienganh;

    public String Md_sanpham_id
    {
        get { return md_sanpham_id; }
        set { md_sanpham_id = value; }
    }

    public String Mota_tiengviet
    {
        get { return mota_tiengviet; }
        set { mota_tiengviet = value; }
    }
    
    public String Mota_tienganh
    {
        get { return mota_tienganh; }
        set { mota_tienganh = value; }
    }

    public UpdateMoTaSanPhamImport(String md_sanpham_id, String mota_tiengviet, String mota_tienganh)
	{
        this.md_sanpham_id = md_sanpham_id;
        this.mota_tiengviet = mota_tiengviet;
        this.mota_tienganh = mota_tienganh;
	}

    LinqDBDataContext db = new LinqDBDataContext();

    public void Update()
    {
        md_sanpham sp = db.md_sanphams.FirstOrDefault(p => p.md_sanpham_id.Equals(md_sanpham_id));
        sp.mota_tiengviet = mota_tiengviet;
        sp.mota_tienganh = mota_tienganh;
        db.SubmitChanges();
    }
}
