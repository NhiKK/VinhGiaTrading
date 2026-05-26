using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class NhaCungUngMacDinh
{
    private bool macdinh;
    private String masanpham, madoitac;

    public String Masanpham
    {
        get { return masanpham; }
        set { masanpham = value; }
    }

    public String Madoitac
    {
        get { return madoitac; }
        set { madoitac = value; }
    }

    public bool Macdinh
    {
        get { return macdinh; }
        set { macdinh = value; }
    }
}