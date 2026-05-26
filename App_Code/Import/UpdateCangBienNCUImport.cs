using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpdateCangBienNCUImport
/// </summary>
public class UpdateCangBienNCUImport : ImportOriginal
{
    private String maSP, maCB, maNCU;

    public String MaSP
    {
        get { return maSP; }
        set { maSP = value; }
    }

    public String MaCB
    {
        get { return maCB; }
        set { maCB = value; }
    }

    public String MaNCU
    {
        get { return maNCU; }
        set { maNCU = value; }
    }

    public UpdateCangBienNCUImport()
    {

    }

    public UpdateCangBienNCUImport(String maSP, String maCB, String maNCU)
        : this(maSP, maCB, maNCU, DateTime.Now, "admin", DateTime.Now, "admin", true)
    {

    }

    public UpdateCangBienNCUImport(String maSP, String maCB, String maNCU, DateTime ngayTao, String nguoiTao, DateTime ngayCapNhat, String nguoiCapNhat, bool hoatDong)
    {
        this.maSP = maSP;
        this.maCB = maCB;
        this.maNCU = maNCU;
        this.ngayTao = ngayTao;
        this.nguoiTao = nguoiTao;
        this.ngayCapNhat = ngayCapNhat;
        this.nguoiCapNhat = nguoiCapNhat;
        this.hoatDong = hoatDong;
    }

    public void Import()
    {
        String update = "update md_sanpham set md_cangbien_id = @md_cangbien_id, nhacungung = @md_doitackinhdoanh_id where ma_sanpham = @ma_sanpham";
        String md_cangbien_id = UpdateCangBienNCUImport.getCangBienId(maCB);
        String md_doitackinhdoanh_id = UpdateCangBienNCUImport.getNhaCungUngId(maNCU);

        mdbc.ExcuteNonQuery(update, "@md_cangbien_id", md_cangbien_id, "@md_doitackinhdoanh_id", md_doitackinhdoanh_id
            , "@ma_sanpham", maSP
            , "@ngaytao", ngayTao
            , "@nguoitao", nguoiTao
            , "@ngaycapnhat", ngayCapNhat
            , "@nguoicapnhat", nguoiCapNhat
            , "@hoatdong", hoatDong);
    }

    public static String getCangBienId(String maCB)
    {
        String get = "select md_cangbien_id from md_cangbien where ma_cangbien = @ma_cangbien";
        return (String)mdbc.ExecuteScalar(get, "@ma_cangbien", maCB);
    }

    public static String getNhaCungUngId(String maNCU)
    {
        String get = "select md_doitackinhdoanh_id from md_doitackinhdoanh where ma_dtkd = @ma_dtkd";
        return (String)mdbc.ExecuteScalar(get, "@ma_dtkd", maNCU);
    }
}
