<%@ WebHandler Language="C#" Class="ChiTietPhaiChiController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class ChiTietPhaiChiController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();
    string md_doitackinhdoanh_id = "";

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        md_doitackinhdoanh_id = context.Request.QueryString["dtkd"];
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
            case "loadPN":
                this.loadPN(context);
                break;
            case "loadInvoice":
                this.loadInvoice(context);
                break;
            case "doitac":
                this.doitac(context);
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

    public void getSelectOption(HttpContext context)
    {
        //String sql = "select c_chitietthuchi_id, c_chitietthuchi_id from c_chitietthuchi where hoatdong = 1";
        //SelectHtmlControl s = new SelectHtmlControl(sql);
        //context.Response.Write(s.ToString());
    }

    public void doitac(HttpContext context)
    {
        /*LinqDBDataContext db = new LinqDBDataContext();
        string md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        string id = context.Request.Form["id"];
        string msg = "";


        foreach (c_chitietthuchi cttc in db.c_chitietthuchis.Where(s => s.c_chitietthuchi_id == id))
        {
            c_thuchi thuchi = db.c_thuchis.Where(s => s.c_thuchi_id == cttc.c_thuchi_id).FirstOrDefault();
            if (thuchi.md_trangthai_id == "HIEULUC")
            {
                msg = "Đã hiệu lực không thể sửa";
                break;
            }
            else
            {
                cttc.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
            }
        }

        if (msg.Length <= 0)
        {
            db.SubmitChanges();
            md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == md_doitackinhdoanh_id).FirstOrDefault();
            msg = "Chọn nhà cung ứng " + dtkd.ten_dtkd + " thành công";
        }
        context.Response.Write(msg);*/
    }

    public void edit(HttpContext context)
    {
        string tiencong = context.Request.Form["sotiencong"];
        string ngaychi = context.Request.Form["ctd_ngaychi"];
        string ltt = context.Request.Form["loaithanhtoan"];
        string lp2 = ltt == "KH" ? context.Request.Form["loaiphieu2"] : "";

        if(tiencong == null || tiencong == "")
            tiencong = "0";
        try {
            string tc = context.Request.Form["istiencoc"];
            bool istiencoc = false;
            if (tc != null)
            {
                if (tc.ToLower().Equals("on")) { istiencoc = true; }
            }
            c_chitietthuchi m = db.c_chitietthuchis.Single(p => p.c_chitietthuchi_id == context.Request.Form["id"]);
            c_thuchi chi = db.c_thuchis.FirstOrDefault(p => p.c_thuchi_id.Equals(m.c_thuchi_id));
            string loai = context.Request.Form["loai"];
            string md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id2"];
            /*if(m.loai == "TM")
				md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id3"];*/
            if (chi.md_trangthai_id.Equals("SOANTHAO"))
            {
                m.tk_no = int.Parse(context.Request.Form["tkno"]);
                m.tk_co = int.Parse(context.Request.Form["tkco"]);
                m.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                m.dtkd_id_ThuChi = chi.md_doitackinhdoanh_id;
                m.dtkd_val_ThuChi = chi.doitacValue;
                m.sotien = decimal.Parse(context.Request.Form["sotien"]);
                m.sotiencong = decimal.Parse(tiencong);
                m.md_bophan_id = context.Request.Form["md_bophan_id"];
                m.ngaychi = DateTime.ParseExact(ngaychi, "dd/MM/yyyy", null);
                m.quydoi = (chi.tygia.Value * decimal.Parse(context.Request.Form["sotien"]));
                m.diengiai = context.Request.Form["diengiai"];
                m.c_donhang_id = context.Request.Form["c_donhang_id"];
                m.c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"];
                m.obj_code = context.Request.Form["c_nhapxuat_id"];
                m.loaiphieu = ltt;
                m.loaiphieu2 = lp2;
                m.thue = int.Parse(context.Request.Form["thue"]);
                if (m.loaiphieu.Equals("IN"))
                {
                    m.c_packinginvoice_id = context.Request.Form["inv"];
                }
                m.isdatcoc = istiencoc;

                m.mota = context.Request.Form["mota"];
                m.hoatdong = true;
                m.ngaycapnhat = DateTime.Now;
                m.nguoicapnhat = UserUtils.getUser(context);

                db.SubmitChanges();
                jqGridHelper.Utils.writeResult(1, "Lưu thành công!");
            }
            else {
                jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi phiếu chi đã hiệu lực.");
            }
        }
        catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }

    public void add(HttpContext context)
    {
        string sphieu_id = "b1b236e7fd8250eca850782b295d528e", sphieu = "", tiento = "UNC";     //CK UNC // table hotro
        string loai = context.Request.Form["loai"];
        if(loai == "TM")
        {
            sphieu_id = "91fb9d0f2fcbc85bdefa91cfa289f0d4"; //TM C
            tiento = "C";
        }

        hotro ht = db.hotros.Where(s => s.c_chitietthuchi_id == sphieu_id).FirstOrDefault();
        md_sochungtu sochungtu = db.md_sochungtus.Single(s => s.tiento == tiento);
        if(ht != null)
        {
            if(ht.ngaymoinhat.Value.Year == DateTime.Now.Year)
            {
                if(ht.ngaymoinhat.Value.Month != DateTime.Now.Month)
                {
                    sochungtu.so_duocgan = 1;
                }
                if(loai == "TM")
                    sphieu = sochungtu.so_duocgan + sochungtu.tiento + "/" + sochungtu.hauto;
                else if(loai == "CK")
                    sphieu = sochungtu.tiento + sochungtu.so_duocgan + "/" + sochungtu.hauto;
                sochungtu.so_duocgan = sochungtu.so_duocgan + sochungtu.buocnhay;
            }
            else
            {
                if(loai == "TM")
                    sphieu = sochungtu.so_duocgan + sochungtu.tiento + "/" + sochungtu.hauto;
                else if(loai == "CK")
                    sphieu = sochungtu.tiento + sochungtu.so_duocgan + "/" + sochungtu.hauto;
                sochungtu.so_duocgan = sochungtu.so_duocgan + sochungtu.buocnhay;
            }
        }
        else
        {
            if(loai == "TM")
                sphieu = sochungtu.so_duocgan + sochungtu.tiento + "/" + sochungtu.hauto;
            else if(loai == "CK")
                sphieu = sochungtu.tiento + sochungtu.so_duocgan + "/" + sochungtu.hauto;
            sochungtu.so_duocgan = sochungtu.so_duocgan + sochungtu.buocnhay;
        }

        string tc = context.Request.Form["istiencoc"];
        string tiencong = context.Request.Form["sotiencong"];
        if(tiencong == null || tiencong == "")
            tiencong = "0";
        bool istiencoc = false;
        if (tc != null)
        {
            if (tc.ToLower().Equals("on")) { istiencoc = true; }
        }
        string c_thuchi_id = context.Request.Form["c_thuchi_id"];
        var thuchi = db.c_thuchis.Single(c => c.c_thuchi_id.Equals(c_thuchi_id));

        string ids = context.Request.Form["ids"].Replace("[", "").Replace("]", "").Replace("\"", "").Trim();
        string values = context.Request.Form["values"].Replace("[", "").Replace("]", "").Replace("\"", "").Trim();
        string md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id2"];
        DateTime ngaychi = DateTime.ParseExact(context.Request.Form["ctd_ngaychi"], "dd/MM/yyyy", null);
        int thue = int.Parse(context.Request.Form["thue"]);
        string[] idsArr = ids.Split(',');
        string[] valuesArr = values.Split(',');
        decimal sum_ = 0;
        for (int i = 0; i < valuesArr.Length; i++)
        {
            if (valuesArr[i].Length >1)
            {
                sum_ += decimal.Parse(valuesArr[i]);
            }
        }

        if (thuchi.md_trangthai_id.Equals("SOANTHAO"))
        {
            var idVal = new string[] { thuchi.md_doitackinhdoanh_id, thuchi.doitacValue };

            if (thuchi.sotien.Value > sum_)
            {
                string ltt = context.Request.Form["loaithanhtoan"];
                string lp2 = ltt == "KH" ? context.Request.Form["loaiphieu2"] : "";
                if (idsArr.Length > 0 && values.Length > 0)
                {
                    for (int i = 0; i < idsArr.Length; i++)
                    {
                        var mnu = new c_chitietthuchi();
                        mnu.c_chitietthuchi_id = Helper.getNewId();
                        mnu.c_thuchi_id = c_thuchi_id;
                        mnu.loaiphieu2 = lp2;
                        mnu.loai = loai;
                        mnu.so_c = sphieu;
                        mnu.tk_no = int.Parse(context.Request.Form["tkno"]);
                        mnu.tk_co = int.Parse(context.Request.Form["tkco"]);
                        mnu.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                        mnu.dtkd_id_ThuChi = idVal[0];
                        mnu.dtkd_val_ThuChi = idVal[1];
                        mnu.sotien = decimal.Parse(valuesArr[i]);
                        mnu.sotiencong = decimal.Parse(tiencong);
                        mnu.quydoi = (thuchi.tygia.Value * decimal.Parse(valuesArr[i]));
                        mnu.diengiai = context.Request.Form["diengiai"];
                        mnu.c_donhang_id = context.Request.Form["c_donhang_id"];
                        mnu.ngaychi = ngaychi;
                        mnu.c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"];
                        mnu.obj_code = idsArr[i];
                        mnu.isdatcoc = istiencoc;
                        mnu.thue = thue;
                        mnu.mota = context.Request.Form["mota"];
                        mnu.hoatdong = true;
                        mnu.ngaytao = DateTime.Now;
                        mnu.ngaycapnhat = DateTime.Now;
                        mnu.nguoitao = UserUtils.getUser(context);
                        mnu.nguoicapnhat = UserUtils.getUser(context);

                        db.c_chitietthuchis.InsertOnSubmit(mnu);
                    }
                }
                else
                {
                    var mnu = new c_chitietthuchi();
                    mnu.c_chitietthuchi_id = Helper.getNewId();
                    mnu.c_thuchi_id = c_thuchi_id;
                    mnu.so_c = sphieu;
                    mnu.loai = loai;
                    mnu.tk_no = int.Parse(context.Request.Form["tkno"]);
                    mnu.tk_co = int.Parse(context.Request.Form["tkco"]);
                    mnu.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                    mnu.dtkd_id_ThuChi = idVal[0];
                    mnu.dtkd_val_ThuChi = idVal[1];
                    mnu.sotien = decimal.Parse(context.Request.Form["sotien"]);
                    mnu.sotiencong = decimal.Parse(tiencong);
                    mnu.quydoi = (thuchi.tygia.Value * decimal.Parse(context.Request.Form["sotien"]));
                    mnu.diengiai = context.Request.Form["diengiai"];
                    mnu.c_donhang_id = context.Request.Form["c_donhang_id"];
                    mnu.ngaychi = ngaychi;
                    mnu.c_packinginvoice_id = context.Request.Form["c_packinginvoice_id"];
                    mnu.obj_code = context.Request.Form["c_nhapxuat_id"];
                    mnu.loaiphieu = ltt;
                    mnu.loaiphieu2 = lp2;
                    mnu.isdatcoc = istiencoc;
                    mnu.thue = thue;
                    mnu.mota = context.Request.Form["mota"];
                    mnu.hoatdong = true;
                    mnu.ngaytao = DateTime.Now;
                    mnu.ngaycapnhat = DateTime.Now;
                    mnu.nguoitao = UserUtils.getUser(context);
                    mnu.nguoicapnhat = UserUtils.getUser(context);

                    db.c_chitietthuchis.InsertOnSubmit(mnu);
                }

                ht = db.hotros.Where(s => s.c_chitietthuchi_id == sphieu_id).FirstOrDefault();
                if(ht == null)
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

                jqGridHelper.Utils.writeResult(1, "Xử lý thành công!");
            }
            else {
                jqGridHelper.Utils.writeResult(0, "Số tiền phân bổ không hợp lệ!");
            }
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể thêm mới khi phiếu chi đã hiệu lực.");
        }
    }

    public void del(HttpContext context)
    {
        var id = context.Request.Form["id"];
        var ct = db.c_chitietthuchis.FirstOrDefault(p => p.c_chitietthuchi_id.Equals(id));
        var chi = db.c_thuchis.FirstOrDefault(p => p.c_thuchi_id.Equals(ct.c_thuchi_id));
        if (chi.md_trangthai_id.Equals("SOANTHAO"))
        {
            db.c_chitietthuchis.DeleteOnSubmit(ct);
            db.SubmitChanges();
            jqGridHelper.Utils.writeResult(1, "Xóa thành công!");
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa khi phiếu chi đã hiệu lực.");
        }
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        String tcId = context.Request.QueryString["tcId"];

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_chitietthuchi cttc left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id =  cttc.md_doitackinhdoanh_id  left join c_thuchi tc on cttc.c_thuchi_id = tc.c_thuchi_id left join c_nhapxuat nx on cttc.obj_code = nx.c_nhapxuat_id left join c_donhang dh on dh.c_donhang_id = nx.c_donhang_id" +
            " WHERE cttc.c_thuchi_id = N'{0}'" + filter;

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
            sidx = "cttc.ngaytao";
            sord = "asc";
        }

        string strsql = "select * from( " +
            " select cttc.c_chitietthuchi_id,cttc.so_c, tc.sophieu, cttc.tk_no, cttc.tk_co, " +
            " cttc.sotien,cttc.sotiencong, cttc.thue, cttc.quydoi, cttc.diengiai, cttc.obj_code, cttc.obj_id, " +
            " cttc.obj_num, cttc.isdatcoc, nx.sophieu as phieunhap, dh.sochungtu, cttc.md_doitackinhdoanh_id," +
            " dtkd.ma_dtkd, dtkd.ten_dtkd, dtkd.daidien, dtkd.diachi, dtkd.so_taikhoan, dtkd.nganhang," +
            " (case when c_packinginvoice_id is null then '' else (select so_pkl from c_packinginvoice where c_packinginvoice_id = cttc.c_packinginvoice_id) end) as so_pkl, " +
            " bp.ten_bophan, cttc.ngaychi,cttc.ngaytao, cttc.nguoitao, cttc.ngaycapnhat, " +
            " cttc.nguoicapnhat, cttc.mota, cttc.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY cttc.ngaytao asc) as RowNum " +
            " FROM c_chitietthuchi cttc " +
            " left join c_thuchi tc on cttc.c_thuchi_id = tc.c_thuchi_id left join c_nhapxuat nx on cttc.obj_code = nx.c_nhapxuat_id left join c_donhang dh on dh.c_donhang_id = nx.c_donhang_id left join md_doitackinhdoanh dtkd on dtkd.md_doitackinhdoanh_id =  cttc.md_doitackinhdoanh_id" +
            " left join md_bophan bp on cttc.md_bophan_id = bp.md_bophan_id" +

            " WHERE cttc.c_thuchi_id = N'{0}'" + filter +
            " )P where RowNum > @start AND RowNum < @end order by RowNum asc";

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
            // ve them cot
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_chitietthuchi_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_c"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_no"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tk_co"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sotien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sotiencong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["thue"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["quydoi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["diengiai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isdatcoc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phieunhap"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_pkl"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_bophan"] + "]]></cell>";
            if(row["ngaychi"].ToString() != null & row["ngaychi"].ToString() != "")
                xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaychi"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            else
                xml += "<cell><![CDATA[]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_doitackinhdoanh_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["daidien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["diachi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_taikhoan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nganhang"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }

    public void loadPN(HttpContext context) {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        //String sqlCount = "select count(*) from c_nhapxuat where md_loaichungtu_id = 'NK' and md_trangthai_id = 'HIEULUC' and md_doitackinhdoanh_id = '"+md_doitackinhdoanh_id+"'";
        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        bool search = bool.Parse(context.Request.QueryString["_search"]);
        string filter = "";
        if (search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }
        String sqlCount = "select count(cnx.c_nhapxuat_id) from c_nhapxuat cnx, c_donhang cdh" +
        " WHERE cnx.c_donhang_id = cdh.c_donhang_id "+
        " and cnx.md_loaichungtu_id = 'NK' and cnx.md_trangthai_id = 'HIEULUC' and cnx.md_doitackinhdoanh_id = '"+md_doitackinhdoanh_id+"'" +
        " and cnx.grandtotal > 0 " + filter +
        " and (cnx.grandtotal - (select coalesce(sum(sotien), 0) from c_chitietthuchi where obj_code = cnx.c_nhapxuat_id)) > 0";
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

        string strsql = "select *, (P.thanhtien - P.datra) as conlaiphaitra from (" +
            " select cnx.c_nhapxuat_id, cnx.sophieu as sochungtu, cdh.sochungtu as sodonhang, cnx.grandtotal as thanhtien,  "+
            " ROW_NUMBER() OVER (ORDER BY cnx.sophieu asc) as RowNum, " +
            " (select coalesce(sum(sotien), 0) from c_chitietthuchi where obj_code = cnx.c_nhapxuat_id) as datra  " +
            " FROM c_nhapxuat cnx , c_donhang cdh" +
            " WHERE cnx.c_donhang_id = cdh.c_donhang_id "+
            " and cnx.md_loaichungtu_id = 'NK' and cnx.md_trangthai_id = 'HIEULUC' and cnx.md_doitackinhdoanh_id = '"+md_doitackinhdoanh_id+"'"+
            " and cnx.grandtotal > 0 " + filter +
            " and (cnx.grandtotal - (select coalesce(sum(sotien), 0) from c_chitietthuchi where obj_code = cnx.c_nhapxuat_id)) > 0" +
            " )P where RowNum > @start AND RowNum < @end order by RowNum";

        //strsql = strsql.Replace("{dk}", filter); 

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_nhapxuat_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sodonhang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["thanhtien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["conlaiphaitra"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);

    }
    public void loadInvoice(HttpContext context){

        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String sqlCount = @"select count(*) from c_packinginvoice
                        where c_packinginvoice_id in (select distinct c_packinginvoice_id from c_dongpklinv where c_donhang_id = '6AAE3F89884A44148CDDCDFF7C370815')";
        int page = int.Parse(context.Request.QueryString["page"]);
        int limit = int.Parse(context.Request.QueryString["rows"]);
        bool search = bool.Parse(context.Request.QueryString["_search"]);
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
        string filter = "";
        if (search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        string strsql = @"select c_packinginvoice_id, so_pkl, totalgross, tiendatcoc, tiendatra  
                        from c_packinginvoice
                        where c_packinginvoice_id in (select distinct c_packinginvoice_id from c_dongpklinv where c_donhang_id = '6AAE3F89884A44148CDDCDFF7C370815')
                        ";

        strsql = strsql.Replace("{dk}", filter);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_packinginvoice_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_pkl"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalgross"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tiendatcoc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tiendatra"] + "]]></cell>";
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
