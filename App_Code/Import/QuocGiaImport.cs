using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class QuocGiaImport : ImportOriginal
{
    private String maQuocGia, tenQuocGia, khuvuc_id;

    public QuocGiaImport()
    { 
	}

    LinqDBDataContext db = new LinqDBDataContext();

    public void Import(String maQuocGia, String tenQuocGia, String KhuVucId)
    {
        md_quocgia qg = new md_quocgia();
        qg.md_quocgia_id = ImportUtils.getNEWID();
        qg.ma_quocgia = maQuocGia;
        qg.ten_quocgia = tenQuocGia;
		qg.md_khuvuc_id = KhuVucId;
        qg.ngaytao = DateTime.Now;
        qg.nguoitao = "admin";
        qg.ngaycapnhat = DateTime.Now;
        qg.nguoicapnhat = "admin";
        qg.hoatdong = true;
        db.md_quocgias.InsertOnSubmit(qg);
        db.SubmitChanges();
    }

    public void Update(String maQuocGia, String tenQuocGia, String KhuVucId)
    {
        md_quocgia qg = db.md_quocgias.FirstOrDefault(p => p.ma_quocgia.Equals(maQuocGia));
        qg.ten_quocgia = tenQuocGia;
		qg.md_khuvuc_id = KhuVucId;
        qg.ngaycapnhat = DateTime.Now;
        qg.nguoicapnhat = "admin";
        qg.hoatdong = true;
        db.SubmitChanges();
    }
}