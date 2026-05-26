<%@ WebHandler Language="C#" Class="TongHopDoanhThuChiPhiController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class TongHopDoanhThuChiPhiController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            default:
                switch (oper)
                {
                    case "del":
                        this.del(context);
                        break;
                    case "edit":
                        this.edit(context);
                        break;
                    case "add":
                        this.add(context);
                        break;
                    default:
                        this.load(context);
                        break;
                }
                break;
        }
    }

    public string removeSpace(string a)
    {
        if (string.IsNullOrWhiteSpace(a))
            a = "0";
        return a.Replace(" ", "");
    }

    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        var id = context.Request.Form["id"];
        var xuongId = context.Request.Form["xuongId"];
        var nam = int.Parse(context.Request.Form["nam"]);
        var thang = int.Parse(context.Request.Form["thang"]);

        var noDauNamStr = removeSpace(context.Request.Form["noDauNam"]);
        var noDauNam = string.IsNullOrEmpty(noDauNamStr) ? 0 : decimal.Parse(noDauNamStr);

        var tygiaNamStr = removeSpace(context.Request.Form["tyGiaMua"]);
        var tygiaNam = string.IsNullOrEmpty(tygiaNamStr) ? 23000 : decimal.Parse(tygiaNamStr);

        var chenhLechTyGiaStr = removeSpace(context.Request.Form["chenhLechTyGia"]);
        var chenhLechTyGia = string.IsNullOrEmpty(chenhLechTyGiaStr) ? 0 : decimal.Parse(chenhLechTyGiaStr);

        var lst = (from o in db.rpt_tonghopdoanhthuchiphis
                   where
                        o.nam.Equals(nam)
                        & o.thang.Equals(thang)
                        & o.xuongId.Equals(xuongId)
                        & !o.rpt_tonghopdoanhthuchiphi_id.Equals(id)
                   select new { o.rpt_tonghopdoanhthuchiphi_id });

        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã tồn tại!");
        }
        else
        {
            var dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == xuongId).FirstOrDefault();
            if (dtkd == null)
                jqGridHelper.Utils.writeResult(0, "Không tìm thấy xưởng đã chọn!");
            else if(dtkd.ma_dtkd != "ANCO1" & chenhLechTyGia != 0)
            {
                jqGridHelper.Utils.writeResult(0, "Xưởng ANCO1 mới có chênh lệch tỷ giá!");
            }
            else
            {
                var m = db.rpt_tonghopdoanhthuchiphis.Where(p => p.rpt_tonghopdoanhthuchiphi_id.Equals(id)).FirstOrDefault();
                m.xuongId = xuongId;
                m.xuongValue = dtkd.ma_dtkd;
                m.nam = nam;
                m.thang = thang;
                m.noDauNam = thang == 1 ? noDauNam : 0;
                m.tyGiaMua = thang == 1 ? tygiaNam : 0;
                m.chenhLechTyGia = chenhLechTyGia;
                m.chiPhiHCNS = decimal.Parse(removeSpace(context.Request.Form["chiPhiHCNS"]));
                m.chiPhiXuatKhau = decimal.Parse(removeSpace(context.Request.Form["chiPhiXuatKhau"]));
                m.tienBHXHBHLDNghiViec = decimal.Parse(removeSpace(context.Request.Form["tienBHXHBHLDNghiViec"]));
                m.dienNuocRacCtyHaca = decimal.Parse(removeSpace(context.Request.Form["dienNuocRacCtyHaca"]));
                m.nopTamUng = decimal.Parse(removeSpace(context.Request.Form["nopTamUng"]));
                m.chiTienMatKhac = decimal.Parse(removeSpace(context.Request.Form["chiTienMatKhac"]));
                m.cuocGuiMauKiemDinh = decimal.Parse(removeSpace(context.Request.Form["cuocGuiMauKiemDinh"]));
                m.dsoVTXuatChoBPCtyKhac = decimal.Parse(removeSpace(context.Request.Form["dsoVTXuatChoBPCtyKhac"]));
                m.khieuNai = 0;
                m.luongCN = decimal.Parse(removeSpace(context.Request.Form["luongCN"]));
                m.luongNV = decimal.Parse(removeSpace(context.Request.Form["luongNV"]));
                m.phiCDBHXHCN = decimal.Parse(removeSpace(context.Request.Form["phiCDBHXHCN"]));
                m.phiCDBHXHNV = decimal.Parse(removeSpace(context.Request.Form["phiCDBHXHNV"]));
                m.thuongLeTetNV = decimal.Parse(removeSpace(context.Request.Form["thuongLeTetNV"]));
                m.thuongLeTetCN = decimal.Parse(removeSpace(context.Request.Form["thuongLeTetCN"]));
                m.tamUngLuongCN = decimal.Parse(removeSpace(context.Request.Form["tamUngLuongCN"]));
                m.tienPheLieu = decimal.Parse(removeSpace(context.Request.Form["tienPheLieu"]));
                m.trich20PheLieu = decimal.Parse(removeSpace(context.Request.Form["trich20PheLieu"]));
                m.mota = context.Request.Form["mota"];
                m.hoatdong = hd;
                m.nguoicapnhat = UserUtils.getUser(context);
                m.ngaycapnhat = DateTime.Now;
                db.SubmitChanges();
            }
        }
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        var xuongId = context.Request.Form["xuongId"];
        var nam = int.Parse(context.Request.Form["nam"]);
        var thang = int.Parse(context.Request.Form["thang"]);
        var noDauNamStr = removeSpace(context.Request.Form["noDauNam"]);
        var noDauNam = string.IsNullOrEmpty(noDauNamStr) ? 0 : decimal.Parse(noDauNamStr);

        var tygiaNamStr = removeSpace(context.Request.Form["tyGiaMua"]);
        var tygiaNam = string.IsNullOrEmpty(tygiaNamStr) ? 23000 : decimal.Parse(tygiaNamStr);

        var chenhLechTyGiaStr = removeSpace(context.Request.Form["chenhLechTyGia"]);
        var chenhLechTyGia = string.IsNullOrEmpty(chenhLechTyGiaStr) ? 0 : decimal.Parse(chenhLechTyGiaStr);

        var lst = from o in db.rpt_tonghopdoanhthuchiphis where o.xuongId.Equals(xuongId) & o.nam.Equals(nam) & o.thang.Equals(thang) select o;
        if (lst.Count() != 0)
        {
            jqGridHelper.Utils.writeResult(0, "Đã tồn tại!");
        }
        else
        {
            var dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == xuongId).FirstOrDefault();
            if (dtkd == null)
                jqGridHelper.Utils.writeResult(0, "Không tìm thấy xưởng đã chọn!");
            else if(dtkd.ma_dtkd != "ANCO1" & chenhLechTyGia != 0)
            {
                jqGridHelper.Utils.writeResult(0, "Xưởng ANCO1 mới có chênh lệch tỷ giá!");
            }
            else
            {
                var mnu = new rpt_tonghopdoanhthuchiphi();
                mnu.rpt_tonghopdoanhthuchiphi_id = Helper.getNewId();
                mnu.nam = nam;
                mnu.thang = thang;
                mnu.xuongId = xuongId;
                mnu.xuongValue = dtkd.ma_dtkd;
                mnu.tyGiaMua = tygiaNam;
                mnu.chenhLechTyGia = chenhLechTyGia;
                mnu.noDauNam = thang == 1 ? noDauNam : 0;
                mnu.chiPhiHCNS = decimal.Parse(removeSpace(context.Request.Form["chiPhiHCNS"]));
                mnu.chiPhiXuatKhau = decimal.Parse(removeSpace(context.Request.Form["chiPhiXuatKhau"]));
                mnu.tienBHXHBHLDNghiViec = decimal.Parse(removeSpace(context.Request.Form["tienBHXHBHLDNghiViec"]));
                mnu.dienNuocRacCtyHaca = decimal.Parse(removeSpace(context.Request.Form["dienNuocRacCtyHaca"]));
                mnu.nopTamUng = decimal.Parse(removeSpace(context.Request.Form["nopTamUng"]));
                mnu.chiTienMatKhac = decimal.Parse(removeSpace(context.Request.Form["chiTienMatKhac"]));
                mnu.cuocGuiMauKiemDinh = decimal.Parse(removeSpace(context.Request.Form["cuocGuiMauKiemDinh"]));
                mnu.dsoVTXuatChoBPCtyKhac = decimal.Parse(removeSpace(context.Request.Form["dsoVTXuatChoBPCtyKhac"]));
                mnu.khieuNai = 0;
                mnu.luongCN = decimal.Parse(removeSpace(context.Request.Form["luongCN"]));
                mnu.luongNV = decimal.Parse(removeSpace(context.Request.Form["luongNV"]));
                mnu.phiCDBHXHCN = decimal.Parse(removeSpace(context.Request.Form["phiCDBHXHCN"]));
                mnu.phiCDBHXHNV = decimal.Parse(removeSpace(context.Request.Form["phiCDBHXHNV"]));
                mnu.thuongLeTetNV = decimal.Parse(removeSpace(context.Request.Form["thuongLeTetNV"]));
                mnu.thuongLeTetCN = decimal.Parse(removeSpace(context.Request.Form["thuongLeTetCN"]));
                mnu.tamUngLuongCN = decimal.Parse(removeSpace(context.Request.Form["tamUngLuongCN"]));
                mnu.tienPheLieu = decimal.Parse(removeSpace(context.Request.Form["tienPheLieu"]));
                mnu.trich20PheLieu = decimal.Parse(removeSpace(context.Request.Form["trich20PheLieu"]));
                mnu.mota = context.Request.Form["mota"];
                mnu.hoatdong = hd;
                mnu.nguoitao = UserUtils.getUser(context);
                mnu.nguoicapnhat = UserUtils.getUser(context);
                mnu.ngaytao = DateTime.Now;
                mnu.ngaycapnhat = DateTime.Now;

                db.rpt_tonghopdoanhthuchiphis.InsertOnSubmit(mnu);
                db.SubmitChanges();
            }
        }
    }

    public void del(HttpContext context)
    {
        var id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        var sql = "delete rpt_tonghopdoanhthuchiphi where rpt_tonghopdoanhthuchiphi_id IN (" + id + ")";
        mdbc.ExcuteNonQuery(sql);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        String sqlCount = "SELECT COUNT(1) AS count FROM rpt_tonghopdoanhthuchiphi cl where 1=1 {0} ";
        sqlCount = string.Format(sqlCount, filter);

        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        String sidx = context.Request.QueryString["sidx"];
        String sord = context.Request.QueryString["sord"];
        int total_page;
        int count = (int)mdbc.ExecuteScalar(sqlCount);
        int start, end;

        if (count > 0)
        {
            total_page = (int)Math.Ceiling(1.0 * count / limit);
        }
        else
        {
            total_page = 0;
        }

        if (page > total_page) page = total_page;
        start = limit * page - limit;
        end = (page * limit) + 1;

        var orderBy = sidx + " " + sord;
        if (sidx.Equals("") || sidx == null)
        {
            orderBy = "ncc.ma_dtkd, cl.nam desc, cl.thang desc";
        }

        string strsql = string.Format(@"
            select * from (
                select 
                    cl.rpt_tonghopdoanhthuchiphi_id, ncc.ma_dtkd, cl.nam, cl.thang, cl.noDauNam, cl.tyGiaMua, cl.chenhLechTyGia, cl.dsoVTXuatChoBPCtyKhac, 
                    cl.tienBHXHBHLDNghiViec, cl.dienNuocRacCtyHaca, cl.nopTamUng, cl.tienPheLieu, cl.chiPhiXuatKhau,  
                    cl.chiPhiHCNS, cl.luongNV, cl.luongCN, cl.phiCDBHXHNV, cl.phiCDBHXHCN, cl.thuongLeTetNV, 
                    cl.thuongLeTetCN, cl.tamUngLuongCN, cl.chiTienMatKhac, cl.trich20PheLieu, cl.khieuNai, 
                    cl.cuocGuiMauKiemDinh, cl.ngaytao, cl.nguoitao, cl.ngaycapnhat, cl.nguoicapnhat, cl.mota, cl.hoatdong,
                    ROW_NUMBER() OVER (ORDER BY {0}) as RowNum
                FROM rpt_tonghopdoanhthuchiphi cl 
                left join md_doitackinhdoanh ncc on ncc.md_doitackinhdoanh_id = cl.xuongId
                where 1=1 {1}
            )P 
            where RowNum > @start AND RowNum < @end
            order by P.RowNum
            ",
            orderBy,
            filter
        );
        strsql = string.Format(strsql, filter);
        //throw new ArgumentNullException(strsql);

        var dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (DataRow row in dt.Rows)
        {
            var xmlDT = "";
            foreach (DataColumn col in dt.Columns)
            {
                var colsDate = new string[] { "ngaytao", "ngaycapnhat" };
                var val = row[col.ColumnName] + "";
                if (colsDate.Contains(col.ColumnName))
                    val = DateTime.Parse(val).ToString("dd/MM/yyyy");
                xmlDT += "<cell><![CDATA[" + val + "]]></cell>";
            }
            xml += string.Format(@"<row>{0}</row>", xmlDT);
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
