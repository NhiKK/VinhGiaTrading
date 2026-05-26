using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Objects
/// </summary>
public static class ExcuteSignalRStatic
{
    public static DataTable getDongGoi(string md_sanpham_id)
    {
        string strsql =
                @"
				select dg.sl_inner, (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner) as dvtinner,
				dg.sl_outer, (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer) as dvtouter,
				dg.l1, dg.w1, dg.h1,
				dg.l2_mix, dg.w2_mix, dg.h2_mix, convert(decimal(18, 4), dg.v2 / dg.sl_outer) as v2, dg.sl_cont_mix, dg.soluonggoi_ctn_20, dg.soluonggoi_ctn, dg.soluonggoi_ctn_40hc
				FROM md_donggoisanpham (nolock) dgsp, md_donggoi (nolock) dg
				WHERE 
				dgsp.md_sanpham_id = '{0}'
				AND dgsp.md_donggoi_id = dg.md_donggoi_id
				AND dgsp.macdinh = 1";

        strsql = string.Format(strsql, md_sanpham_id);
        return mdbc.GetData(strsql);
    }

    public static DataTable getFOB(string md_sanpham_id)
    {
        string strsql =
                @"
				    select top 1 cb.ten_cangbien 
                    from md_cangbien (nolock) cb 
                    where cb.md_cangbien_id = (
	                    select top 1
		                    cxh.md_cangbien_id
	                    FROM 
		                    md_cangxuathang (nolock) cxh
	                    WHERE 
		                    cxh.md_sanpham_id = '{0}'
		                    and cxh.macdinh = 1
                    )
                ";

        strsql = string.Format(strsql, md_sanpham_id);
        return mdbc.GetData(strsql);
    }

    public static DataTable getPrice(string arrMSP)
    {
        string strsql =
                @"    
                    declare @tblBG table (
	                    bgid nvarchar(32)
	                    , tenbg nvarchar(MAX)
	                    , bgban bit
	                    , pbgid nvarchar(32)
	                    , ngayhl datetime
	                    , dongtien nvarchar(32)
                    )
                    insert into @tblBG
                    select bg.md_banggia_id, bg.ten_banggia, bg.banggiaban, pbg.md_phienbangia_id, pbg.ngay_hieuluc, dt.ma_iso
                    from md_banggia (nolock) bg
                    inner join md_phienbangia (nolock) pbg on bg.md_banggia_id = pbg.md_banggia_id
                    inner join md_dongtien (nolock) dt on bg.md_dongtien_id = dt.md_dongtien_id
                    where 
	                    1=1
	                    and bg.banggiaban = 1
	                    AND substring(bg.ten_banggia, 0, 5) = 'FOB-' 

                    declare @tblGia table (
	                    spid nvarchar(32)
	                    , ma_sanpham nvarchar(MAX)
	                    , l_cm decimal(18,1)
	                    , w_cm decimal(18,1)
	                    , h_cm decimal(18,1)
	                    , gia decimal(18,2)
	                    , pbgid nvarchar(32)
                    )
                    insert into @tblGia
                    select 
		                    sp.*
		                    , gsp.gia
		                    ,gsp.md_phienbangia_id
	                    from (
		                    select sp.md_sanpham_id, sp.ma_sanpham, sp.l_cm, sp.w_cm, sp.h_cm
		                    FROM 
			                    md_sanpham (nolock) sp
		                    WHERE 
			                    1=1
			                    AND sp.ma_sanpham in ({0})
	                    ) sp
	                    inner join md_giasanpham (nolock) gsp on gsp.md_sanpham_id = sp.md_sanpham_id
	                    where
		                    1=1
		                    AND gsp.hoatdong = 1 

                    select 
	                    T.* 
                    from (
	                    select 
		                    gia.*
		                    , row_number() over(partition by gia.ma_sanpham order by bg.ngayhl desc) as rn
	                    from 
		                    @tblGia gia
		                    inner join @tblBG bg on bg.pbgid = gia.pbgid
	                )T
                    where T.rn = 1 order by T.ma_sanpham
                ";

        strsql = string.Format(strsql, arrMSP);
        return mdbc.GetData(strsql);
    }

    public static dynamic getImage(string ma_sanpham, int type)
    {
        var response = new Dictionary<string, object>()
        {
            { "cl", "" },
            { "dt", "" },
            { "ms", "" },
            { "msLink", Public.imgNotFound }
        };

        try
        {
            string msLink = "images/products/mausac/";
            string cl = ma_sanpham.Substring(0, 2);
            string dt = ma_sanpham.Substring(6, 2);
            string ms = ma_sanpham.Substring(12, 2);
            string kd = ma_sanpham.Substring(0, 9);
            string image = mapPathSignalR("~/" + msLink);
            string clDTMS = string.Format(@"{0}-{1}-{2}.jpg", cl, dt, ms);
            //Neu co hinh anh mau sac chinh xac cua bo F
            if (File.Exists(image + ma_sanpham + ".jpg"))
                msLink += ma_sanpham + ".jpg";
            //Neu co hinh anh mau sac chinh xac cua bo S
            else if (File.Exists(image + kd + ms + ".jpg"))
                msLink += kd + ms + ".jpg";
            //Neu khong co hinh anh mau sac chinh xac cua bo F va S
            else
            {
                bool haveImg = false;

                if (type == 0)
                {
                    string kdCP = kd;
                    var rgxMauSac3 = new System.Text.RegularExpressions.Regex("^[0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]-F[0-9]-[0-9][0-9]$");
                    if (rgxMauSac3.IsMatch(ma_sanpham)) {
                        kdCP = ma_sanpham.Substring(0, 12);
                    }
                    
                    for (var i = 0; i < 99; i++)
                    {
                        var msCP = i < 10 ? "0" + i : i.ToString();
                        if (File.Exists(image + kdCP + msCP + ".jpg"))
                        {
                            msLink += kdCP + msCP + ".jpg";
                            haveImg = true;
                            break;
                        }
                    }

                    if (haveImg == false)
                        msLink = Public.imgNotFound;
                }
                else if (File.Exists(image + clDTMS))
                    msLink += clDTMS;
            }

            response["cl"] = cl;
            response["dt"] = dt;
            response["ms"] = ms;
            response["msLink"] = msLink;
        }
        catch { }

        return response;
    }

    public static string getImageProduct(string msp)
    {
        string image = "";
        try
        {
            string msLink = "images/products/mausac/";
            string cl = msp.Substring(0, 2);
            string dt = msp.Substring(6, 2);
            string ms = msp.Substring(12, 2);
            string kd = msp.Substring(0, 9);
            var url = mapPathSignalR("~/" + msLink);
            
            //Neu co hinh anh mau sac cua F
            if (File.Exists(url + msp + ".jpg"))
                image = msLink + msp + ".jpg";
            //Neu co hinh anh mau sac cua S
            else if (File.Exists(url + kd + ms + ".jpg"))
                image = msLink + kd + ms + ".jpg";
            //Neu khong co hinh anh mau sac cua S va F thi lay hinh anh mac dinh cua san pham
            else
            {
                bool haveImg = false;
                string kdCP = kd;
                var rgxMauSac3 = new System.Text.RegularExpressions.Regex("^[0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]-F[0-9]-[0-9][0-9]$");
                if (rgxMauSac3.IsMatch(msp))
                {
                    kdCP = msp.Substring(0, 12);
                }

                for (var i = 0; i < 99; i++)
                {
                    var msCP = i < 10 ? "0" + i : i.ToString();
                    if (File.Exists(url + kdCP + msCP + ".jpg"))
                    {
                        image = msLink + kdCP + msCP + ".jpg";
                        haveImg = true;
                        break;
                    }
                }

                if (haveImg == false)
                {
                    if (rgxMauSac3.IsMatch(msp))
                    {
                        image = msp.Substring(0, 11) + ".jpg";
                        image = Public.imgProduct + image;
                    }
                    else
                    {
                        image = msp.Substring(0, 8) + ".jpg";
                        image = Public.imgProduct + image;
                    }

                    if (!File.Exists(mapPathSignalR("~/" + image)))
                        image = Public.imgNotFound;
                }
            }
        }
        catch
        {
            image = Public.imgNotFound;
        }

        return image;
    }

    public static string getImageColor(string color)
    {
        string image = "";
        try
        {
            var indexMimeImg = color.LastIndexOf(".");
            var colorFull = indexMimeImg <= -1 ? color + ".jpg" : color;
            var mimeImg = colorFull.Substring(colorFull.LastIndexOf("."));
            string msLink = Public.imgProduct;
            if (color.Length >= 11)
            {
                msLink = Public.imgColor;
            }

            image = mapPathSignalR("~/" + msLink) + colorFull;

            if (File.Exists(image))
                image = msLink + colorFull;
            else
                image = Public.imgNotFound;
        }
        catch(Exception ex)
        {
            image = "Error:" + ex.Message;
        }

        return image;
    }

    public static dynamic getTextFromInner(string inner, string outer, string slinner, string slouter)
    {
        string packingText = "", sizeText = "", quantity = "";

        if (inner.LastIndexOf("/bun") > -1)
        {
            packingText = "Packing in bundle";
            sizeText = "Pallet size";
        }
        else if (inner.LastIndexOf("/ctn") > -1)
        {
            packingText = "Packing in carton";
            sizeText = "Carton size";
        }
        else if (inner.LastIndexOf("/crt") > -1)
        {
            packingText = "Packing in wooden crate";
            sizeText = "Crate size";
        }

        if (outer.LastIndexOf("/pal") > -1)
        {
            packingText = string.IsNullOrEmpty(packingText) ? "Packing in bundle" : packingText;
            sizeText = "Pallet size";
        }
        else if (outer.LastIndexOf("/ctn") > -1)
        {
            packingText = string.IsNullOrEmpty(packingText) ? "Packing in carton" : packingText;
            sizeText = "Carton size";
        }
        else if (outer.LastIndexOf("/crt") > -1)
        {
            packingText = string.IsNullOrEmpty(packingText) ? "Packing in wooden crate" : packingText;
            sizeText = "Crate size";
        }

        inner = inner.Replace("/bun", "/bundle").Replace("/pal", "/pallet").Replace("/ctn", "/carton").Replace("/crt", "/crate");
        outer = outer.Replace("/bun", "/bundle").Replace("/pal", "/pallet").Replace("/ctn", "/carton").Replace("/crt", "/crate");

        int iq = outer.LastIndexOf("/");
        quantity = outer.Substring(iq + 1) + "s";

        if (slinner != "0" & inner != "0" & slinner != "" & inner != "")
            inner = slinner + " " + inner;
        else
            inner = " ";

        if (slouter != "0" & outer != "0" & slouter != "" & outer != "")
            outer = slouter + " " + outer;
        else
            outer = " ";

        return new {
            packingText,
            sizeText,
            inner,
            outer,
            quantity
        };
    }

    public static string getSizeRectOrRound(LinqDBDataContext db, md_sanpham sp, string kichthuoc)
    {
        var kd = db.md_kieudangs.Where(s => s.md_kieudang_id == sp.md_kieudang_id).Select(s => s.ma_kieudang).FirstOrDefault();

        if(kd == "ROU")
        {
            kichthuoc = "D" + kichthuoc.Split('x')[0] + "x" + kichthuoc.Split('x')[2];
        }

        return kichthuoc;
    }
	
	public static string mapPathSignalR(string path)
    {
        return System.Web.Hosting.HostingEnvironment.MapPath(path);
    }
}