using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ImportOriginal
{
    protected DateTime ngayTao, ngayCapNhat;
    protected bool hoatDong;
    protected String nguoiTao, nguoiCapNhat;

    public String NguoiTao
    {
        get { return nguoiTao; }
        set { nguoiTao = value; }
    }

    public String NguoiCapNhat
    {
        get { return nguoiCapNhat; }
        set { nguoiCapNhat = value; }
    }
    

    public DateTime NgayTao
    {
        get { return ngayTao; }
        set { ngayTao = value; }
    }

    public DateTime NgayCapNhat
    {
        get { return ngayCapNhat; }
        set { ngayCapNhat = value; }
    }
    

    public bool HoatDong
    {
        get { return hoatDong; }
        set { hoatDong = value; }
    }


}