<%@ WebHandler Language="C#" Class="ChiTietThuChiController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ChiTietThuChiController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "add":
                this.add(context);
                break;
            case "edit":
                this.edit(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "getTien":
                this.getTien(context);
                break;
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

    public void getTien(HttpContext context)
    {
        decimal tienconlai = 0;
        string c_packinginvoice_id = context.Request.QueryString["c_packinginvoice_id"];
        var pkl = db.c_packinginvoices.Where(s => s.c_packinginvoice_id == c_packinginvoice_id).FirstOrDefault();
        if (pkl != null)
            tienconlai = pkl.tienconlai.GetValueOrDefault(0);
        context.Response.Write(tienconlai.ToString());
    }

    public void getSelectOption(HttpContext context)
    {
        //String sql = "select c_chitietthuchi_id, c_chitietthuchi_id from c_chitietthuchi where hoatdong = 1";
        //SelectHtmlControl s = new SelectHtmlControl(sql);
        //context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        try
        {
            string tc = context.Request.Form["istiencoc"];
            bool istiencoc = false;
            if (tc != null)
            {
                if (tc.ToLower().Equals("on")) { istiencoc = true; }
            }

            c_chitietthuchi m = db.c_chitietthuchis.Single(p => p.c_chitietthuchi_id == context.Request.Form["id"]);

            var thuchi = db.c_thuchis.Single(c => c.c_thuchi_id.Equals(m.c_thuchi_id));

            if (thuchi.md_trangthai_id.Equals("SOANTHAO"))
            {
                m.tk_no = int.Parse(context.Request.Form["tk_no"]);
                m.tk_co = int.Parse(context.Request.Form["tk_co"]);
                m.sotien = decimal.Parse(context.Request.Form["sotien"]);
                m.quydoi = (thuchi.tygia.Value * decimal.Parse(context.Request.Form["sotien"]));
                m.diengiai = context.Request.Form["diengiai"];
                m.c_donhang_id = context.Request.Form["c_donhang_id"];
                m.c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"];
                m.isdatcoc = istiencoc;

                //m.mota = context.Request.Form["mota"];
                m.hoatdong = true;
                m.ngaycapnhat = DateTime.Now;
                m.nguoicapnhat = UserUtils.getUser(context);
                m.loaiphieu = context.Request.Form["loaithanhtoan"];

                //if (m.sotien.Value > -thuchi.tienconlai)
                //{
                //    jqGridHelper.Utils.writeResult(0, "Số tiền không hợp lệ!");
                //}
                //else
                //{
                db.SubmitChanges();
                jqGridHelper.Utils.writeResult(1, "Lưu thành công!");
                //}
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi phiếu thu đã hiệu lực!");
            }

        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }

    }

    public void add(HttpContext context)
    {
        string sphieu_id = "6075512b0ec3145bc86241ab1d7f510b", sphieu = "", tiento = "T"; // table hotro
        hotro ht = null;
        ht = db.hotros.Where(s => s.c_chitietthuchi_id == sphieu_id).FirstOrDefault();
        md_sochungtu sochungtu = db.md_sochungtus.Single(s => s.tiento == tiento);
        if (ht != null)
        {
            if (ht.ngaymoinhat.Value.Year == DateTime.Now.Year)
            {
                if (ht.ngaymoinhat.Value.Month != DateTime.Now.Month)
                {
                    sochungtu.so_duocgan = 1;
                }
                sphieu = sochungtu.so_duocgan + sochungtu.tiento + "/" + sochungtu.hauto;
                sochungtu.so_duocgan = sochungtu.so_duocgan + sochungtu.buocnhay;
            }
            else
            {
                sphieu = sochungtu.so_duocgan + sochungtu.tiento + "/" + sochungtu.hauto;
                sochungtu.so_duocgan = sochungtu.so_duocgan + sochungtu.buocnhay;
            }
        }
        else
        {
            sphieu = sochungtu.so_duocgan + sochungtu.tiento + "/" + sochungtu.hauto;
            sochungtu.so_duocgan = sochungtu.so_duocgan + sochungtu.buocnhay;
        }
        string tc = context.Request.Form["istiencoc"];

        bool istiencoc = false;
        if (tc != null)
        {
            if (tc.ToLower().Equals("on")) { istiencoc = true; }
        }
        string c_thuchi_id = context.Request.Form["c_thuchi_id"];
        //var thuchi = db.c_thuchis.Fir(c => c.c_thuchi_id.Equals(c_thuchi_id));
        var thuchi = db.c_thuchis.FirstOrDefault(c => c.c_thuchi_id == c_thuchi_id);
        if (thuchi.md_trangthai_id == "SOANTHAO")
        {
            c_chitietthuchi mnu = new c_chitietthuchi
            {
                c_chitietthuchi_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                c_thuchi_id = c_thuchi_id,
                so_c = sphieu,
                tk_no = int.Parse(context.Request.Form["tk_no"]),
                tk_co = int.Parse(context.Request.Form["tk_co"]),
                sotien = decimal.Parse(context.Request.Form["sotien"]),
                quydoi = (thuchi.tygia.Value * decimal.Parse(context.Request.Form["sotien"])),
                diengiai = context.Request.Form["diengiai"],
                c_donhang_id = context.Request.Form["c_donhang_id"],
                c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"],
                isdatcoc = istiencoc,
                loaiphieu = context.Request.Form["loaithanhtoan"],

                //mota = context.Request.Form["mota"],
                hoatdong = true,
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                nguoitao = UserUtils.getUser(context),
                nguoicapnhat = UserUtils.getUser(context)
            };

            //if (mnu.sotien.Value > -thuchi.tienconlai)
            //{
            //    context.Response.Write("Số tiền không hợp lệ!");    
            //}
            //else
            //{
            db.c_chitietthuchis.InsertOnSubmit(mnu);

            ht = db.hotros.Where(s => s.c_chitietthuchi_id == sphieu_id).FirstOrDefault();
            if (ht == null)
            {
                ht = new hotro();
                ht.hotro_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss"));
                ht.c_chitietthuchi_id = sphieu_id;
                ht.ngaymoinhat = DateTime.Now;
                ht.hoatdong = true;
                ht.ngaytao = DateTime.Now;
                ht.ngaycapnhat = DateTime.Now;
                ht.nguoitao = UserUtils.getUser(context);
                ht.nguoicapnhat = UserUtils.getUser(context);
                db.hotros.InsertOnSubmit(ht);
            }
            else
            {
                ht.ngaymoinhat = DateTime.Now;
                ht.hoatdong = true;
                ht.nguoicapnhat = UserUtils.getUser(context);
                ht.ngaycapnhat = DateTime.Now;
            }

            db.SubmitChanges();
            jqGridHelper.Utils.writeResult(1, "Tạo mới thành công!");
            //}
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể thêm chi tiết khi phiếu thu đã hiệu lực!");
        }
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];

        c_chitietthuchi ct = db.c_chitietthuchis.FirstOrDefault(p => p.c_chitietthuchi_id.Equals(id));
        c_thuchi thuchi = db.c_thuchis.FirstOrDefault(p => p.c_thuchi_id.Equals(ct.c_thuchi_id));
        if (thuchi.md_trangthai_id.Equals("SOANTHAO"))
        {
            db.c_chitietthuchis.DeleteOnSubmit(ct);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa chi tiết khi phiếu thu đã hiệu lực!");
        }
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String tcId = context.Request.QueryString["tcId"];

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_chitietthuchi cttc left join c_thuchi tc on cttc.c_thuchi_id = tc.c_thuchi_id left join c_packinginvoice cpk on cttc.c_packinginvoice_id = cpk.c_packinginvoice_id left join c_donhang dh on cttc.c_donhang_id = dh.c_donhang_id" +
            " WHERE cttc.c_thuchi_id = tc.c_thuchi_id " +
            " AND cttc.c_thuchi_id = N'{0}'" + filter;

        if (tcId != null)
        {
            sqlCount = string.Format(sqlCount, tcId);
        }
        else
        {
            sqlCount = string.Format(sqlCount, 0);
        }

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

        if (sidx.Equals("") || sidx == null)
        {
            sidx = "cttc.c_chitietthuchi_id";
        }

        string strsql = "select * from( " +
            " select cttc.c_chitietthuchi_id, tc.sophieu, cttc.so_c, cttc.tk_no, cttc.tk_co, " +
            " cttc.sotien, cttc.quydoi, cttc.diengiai, cttc.obj_code, cttc.obj_id, " +
            " cttc.obj_num, cttc.isdatcoc, " +
            " (case when obj_code is null then '' else (select sophieu from c_nhapxuat where c_nhapxuat_id = cttc.obj_code) end) as phieunhap, " +
            // " (case when c_donhang_id is null then '' else (select sochungtu from c_donhang where c_donhang_id = cttc.c_donhang_id) end) as c_donhang_id, " +
            " dh.sochungtu, " +
            " cpk.so_pkl, " +
            " cttc.ngaytao, cttc.nguoitao, cttc.ngaycapnhat, " +
            " cttc.nguoicapnhat, cttc.mota, cttc.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_chitietthuchi cttc left join c_thuchi tc on cttc.c_thuchi_id = tc.c_thuchi_id left join c_packinginvoice cpk on cttc.c_packinginvoice_id = cpk.c_packinginvoice_id left join c_donhang dh on cttc.c_donhang_id = dh.c_donhang_id" +
            " WHERE cttc.c_thuchi_id = tc.c_thuchi_id " +
            " AND cttc.c_thuchi_id = N'{0}' " + filter +
            " )P where RowNum > @start AND RowNum < @end ";

        if (tcId != null)
        {
            strsql = string.Format(strsql, tcId);
        }
        else
        {
            strsql = string.Format(strsql, 0);
        }

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_chitietthuchi_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_c"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_no"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_co"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sotien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["quydoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["diengiai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_pkl"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isdatcoc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
