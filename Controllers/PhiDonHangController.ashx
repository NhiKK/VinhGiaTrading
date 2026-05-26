<%@ WebHandler Language="C#" Class="PhiDonHangController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class PhiDonHangController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "getoption":
                this.getSelectOption(context);
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
        //String sql = "select md_bocai_id, code_bc from md_bocai where hoatdong = 1 order by code_bc desc";
        //SelectHtmlControl s = new SelectHtmlControl(sql);
        //context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        //String h = context.Request.Form["hoatdong"].ToLower();
        //bool hd = false;

        //Tra cho NCC
        string tracho_ncc = context.Request.Form["tracho_ncc"].ToLower();
        string check_chhet = "false";
        string check_ptr = "false";
        string check_ptrCBM = "false";
        string phitang_kh = "false";
        string diengiai_kh = "";
        string ma_dtkd_select = "", md_doitackinhdoanh_id = "";
        string dsncc_check_ptr =  context.Request.Form["dsncc_check_ptr"];
        string dsncc_check_chhet =  context.Request.Form["dsncc_check_chhet"];
        int check = 0;
        if(tracho_ncc == "true") {
            phitang_kh = context.Request.Form["phitang_kh"].ToLower();
            diengiai_kh = context.Request.Form["diengiai_kh"].ToLower();
            check_chhet = context.Request.Form["check_chhet"].ToLower();
            check_ptr = context.Request.Form["check_ptr"].ToLower();
            check_ptrCBM = context.Request.Form["check_ptrCBM"].ToLower();

            if(check_chhet == "true")
                check = 1;
            else if(check_ptr == "true")
                check = 2;
            else if(check_ptrCBM == "true")
                check = 3;

            if(check > 0 & check < 3) {
                md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
                ma_dtkd_select = db.md_doitackinhdoanhs.SingleOrDefault(p => p.md_doitackinhdoanh_id == md_doitackinhdoanh_id).ma_dtkd;
            }
        }

        //if (h.Equals("on") || h.Equals("true"))
        //{ hd = true; }

        String phitang = context.Request.Form["phitang"].ToLower();
        if (phitang.Equals("on") || phitang.Equals("true"))
        {
            phitang = "true";
        }
        else
        {
            phitang = "false";
        }

        String c_donhang_id = context.Request.Form["c_donhang_id"];
        c_donhang dh = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(c_donhang_id));
        //get ty gia moi nhat
        md_tygia tg = db.md_tygias.Where(p => p.md_tygia_id != "").OrderByDescending(c => c.hieuluc_tungay).FirstOrDefault();

        decimal phi_vnd = 0;
        decimal gianhap = get_gianhap(context, ma_dtkd_select);

        c_phidonhang m = db.c_phidonhangs.Single(p => p.c_phidonhang_id == context.Request.Form["id"]);

        var user = UserUtils.getUser(db, context);
        string msg = "";
        if (!user.themPhiSauHL.GetValueOrDefault(false))
        {
            if (dh.md_trangthai_id == "HIEULUC")
                msg = "Không thể chỉnh sửa khi đơn hàng đã hiệu lực.!";
            else if (dh.md_trangthai_id == "KETTHUC")
                msg = "Không thể chỉnh sửa khi đơn hàng đã kết thúc.!";
        }

        if(msg.Length <= 0)
        {
            if(tg != null)
            {
                switch (check)
                {
                    case 1:
                        phi_vnd = decimal.Parse(context.Request.Form["phi"]) * tg.chia_cho.Value ;
                        break;
                    case 2:
                        phi_vnd = decimal.Parse(context.Request.Form["phi"]) * tg.chia_cho.Value * gianhap;
                        break;
                    case 3:
                        phi_vnd = decimal.Parse(context.Request.Form["phi"]) * tg.chia_cho.Value * dh.totalcbm.Value / 100;
                        break;
                    default:
                        phi_vnd = -1;
                        break;
                }
                //Khách hàng
                m.phi = decimal.Parse(context.Request.Form["phi"]);
                m.phitang = bool.Parse(phitang);
                m.mota = context.Request.Form["mota"];
                m.tracho_ncc = bool.Parse(tracho_ncc);
                //Trả cho NCC
                m.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
                m.phitang_kh = bool.Parse(phitang_kh);
                m.diengiai_kh = diengiai_kh;
                m.check_chhet = bool.Parse(check_chhet);
                m.check_ptr = bool.Parse(check_ptr);
                m.check_ptrCBM = bool.Parse(check_ptrCBM);
                m.dsncc_check_ptr = dsncc_check_ptr;
                m.dsncc_check_chhet = dsncc_check_chhet;

                //m.hoatdong = hd;
                m.ngaycapnhat = DateTime.Now;
                m.nguoicapnhat = UserUtils.getUser(context);
                m.phi_vnd = phi_vnd;
                db.SubmitChanges();
            }
            else
            {
                msg = "Chưa có tỷ giá mới nhất.!";
            }
        }

        if (msg.Length > 0)
            jqGridHelper.Utils.writeResult(0, msg);
    }

    public void add(HttpContext context)
    {
        //string h = context.Request.Form["hoatdong"].ToLower();

        //Tra cho NCC
        string tracho_ncc = context.Request.Form["tracho_ncc"].ToLower();
        string check_chhet = "false";
        string check_ptr = "false";
        string check_ptrCBM = "false";
        string phitang_kh = "false";
        string diengiai_kh = "";
        string dsncc_check_ptr = context.Request.Form["dsncc_check_ptr"];
        string dsncc_check_chhet = context.Request.Form["dsncc_check_chhet"];
        string ma_dtkd_select = "", md_doitackinhdoanh_id = "";

        int check = 0;
        if (tracho_ncc == "true")
        {
            phitang_kh = context.Request.Form["phitang_kh"].ToLower();
            diengiai_kh = context.Request.Form["diengiai_kh"].ToLower();
            check_chhet = context.Request.Form["check_chhet"].ToLower();
            check_ptr = context.Request.Form["check_ptr"].ToLower();
            check_ptrCBM = context.Request.Form["check_ptrCBM"].ToLower();

            if (check_chhet == "true")
                check = 1;
            else if (check_ptr == "true")
                check = 2;
            else if (check_ptrCBM == "true")
                check = 3;

            if (check > 0 & check < 3)
            {
                md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
                ma_dtkd_select = db.md_doitackinhdoanhs.SingleOrDefault(p => p.md_doitackinhdoanh_id == md_doitackinhdoanh_id).ma_dtkd;
            }
        }
        //bool hd = false;
        //if (h.Equals("on") || h.Equals("true"))
        //{ hd = true; }

        String phitang = context.Request.Form["phitang"].ToLower();
        if (phitang.Equals("on") || phitang.Equals("true"))
        {
            phitang = "true";
        }
        else
        {
            phitang = "false";
        }

        String c_donhang_id = context.Request.Form["c_donhang_id"];
        c_donhang dh = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(c_donhang_id));
        //get ty gia moi nhat
        md_tygia tg = db.md_tygias.Where(p => p.md_tygia_id != "").OrderByDescending(c => c.hieuluc_tungay).FirstOrDefault();

        decimal phi_vnd = 0;
        //Tinh % theo nha cung ung
        decimal gianhap = get_gianhap(context, ma_dtkd_select);
        // tinh CBM
        System.Data.DataTable dt = mdbc.GetDataFromProcedure("tinhNangLucCungUng", "@donhang_id", c_donhang_id);
        int rowCount = dt.Rows.Count;
        string nangluc = "";
        if (rowCount > 0)
        {
            foreach (System.Data.DataRow row in dt.Rows)
            {
                nangluc += row["ma_dtkd"] + ":" + row[2] + ",";
            }
        }

        decimal cbm = 0;
        string[] nangluc_split = nangluc.Split(',');
        for (int i = 0; i < nangluc_split.Count(); i++)
        {
            string[] cbm_ncc = nangluc_split[i].Split(':');
            if (ma_dtkd_select == cbm_ncc[0])
            {
                try { cbm = decimal.Parse(cbm_ncc[1]); } catch { }
            }
        }

        decimal vnn = 0;
        try { vnn = ((cbm * 100) / dh.totalcbm.Value); } catch { }

        var user = UserUtils.getUser(db, context);
        string msg = "";
        if (!user.themPhiSauHL.GetValueOrDefault(false))
        {
            if (dh.md_trangthai_id == "HIEULUC")
                msg = "Không thể tạo mới khi đơn hàng đã hiệu lực.!";
            else if (dh.md_trangthai_id == "KETTHUC")
                msg = "Không thể tạo mới khi đơn hàng đã kết thúc.!";
        }

        if(msg.Length <= 0)
        {
            if (tg != null)
            {
                switch (check)
                {
                    case 1:
                        phi_vnd = decimal.Parse(context.Request.Form["phi"]) * tg.chia_cho.Value;
                        break;
                    case 2:
                        phi_vnd = decimal.Parse(context.Request.Form["phi"]) * tg.chia_cho.Value * gianhap;
                        break;
                    case 3:
                        phi_vnd = decimal.Parse(context.Request.Form["phi"]) * tg.chia_cho.Value * vnn / 100;
                        break;
                    default:
                        phi_vnd = -1;
                        break;
                }

                c_phidonhang mnu = new c_phidonhang
                {
                    c_phidonhang_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                    c_donhang_id = context.Request.Form["c_donhang_id"],
                    //Khach hang
                    phi = decimal.Parse(context.Request.Form["phi"]),
                    phitang = bool.Parse(phitang),
                    mota = context.Request.Form["mota"],
                    tracho_ncc = bool.Parse(tracho_ncc),
                    sauHL = dh.md_trangthai_id != "SOANTHAO",
                    //Tra cho NCC
                    md_doitackinhdoanh_id = md_doitackinhdoanh_id,
                    phitang_kh = bool.Parse(phitang_kh),
                    diengiai_kh = diengiai_kh,
                    check_chhet = bool.Parse(check_chhet),
                    check_ptr = bool.Parse(check_ptr),
                    check_ptrCBM = bool.Parse(check_ptrCBM),
                    dsncc_check_ptr = dsncc_check_ptr,
                    dsncc_check_chhet = dsncc_check_chhet,
                    hoatdong = true,
                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now,
                    nguoitao = UserUtils.getUser(context),
                    nguoicapnhat = UserUtils.getUser(context),
                    phi_vnd = phi_vnd
                };

                db.c_phidonhangs.InsertOnSubmit(mnu);
                db.SubmitChanges();
            }
            else
            {
                msg = "Chưa có tỷ giá mới nhất.!";
            }
        }

        if (msg.Length > 0)
            jqGridHelper.Utils.writeResult(0, msg);
    }

    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        c_phidonhang pdh = db.c_phidonhangs.FirstOrDefault(p => p.c_phidonhang_id.Equals(id));
        c_donhang dh = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(pdh.c_donhang_id));

        var user = UserUtils.getUser(db, context);
        string msg = "";
        if (!user.themPhiSauHL.GetValueOrDefault(false))
        {
            if (dh.md_trangthai_id == "HIEULUC")
                msg = "Không thể tạo mới khi đơn hàng đã hiệu lực.!";
            else if (dh.md_trangthai_id == "KETTHUC")
                msg = "Không thể tạo mới khi đơn hàng đã kết thúc.!";
        }
        else if(!pdh.sauHL.GetValueOrDefault(false))
        {
            if(new string[] { "HIEULUC", "KETTHUC" }.Contains(dh.md_trangthai_id))
            {
                //msg = "Không được xóa phí được khởi tạo trước khi Hiệu Lực.!";
            }
        }

        if (msg.Length <= 0)
        {
            if (new string[] { "HIEULUC", "KETTHUC" }.Contains(dh.md_trangthai_id))
                pdh.hoatdong = false;
            else
                db.c_phidonhangs.DeleteOnSubmit(pdh);

            db.SubmitChanges();
        }

        if (msg.Length > 0)
            jqGridHelper.Utils.writeResult(0, msg);
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        String poId = context.Request.QueryString["poId"];
        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        string sqlCount = string.Format(@"
            SELECT COUNT(1) AS count 
            FROM c_phidonhang bc 
            left join md_doitackinhdoanh dtkd on bc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
            WHERE 1=1 {0} and bc.c_donhang_id = '{1}'"
            , filter
            , poId
        );

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
            sidx = "dtkd.ma_dtkd";
        }

        string strsql = string.Format(@"
            select * 
            from (
                select bc.*
                , dtkd.ma_dtkd
                , ROW_NUMBER() OVER (ORDER BY {0} {1}) as RowNum 
                FROM c_phidonhang bc 
                left join md_doitackinhdoanh dtkd on bc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
                WHERE 
                    1=1 {2} 
                    and c_donhang_id = '{3}'
            )P where RowNum > @start AND RowNum < @end",
        sidx,
        sord,
        filter,
        poId
        );

        try
        {
            System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            xml += "<rows>";
            xml += "<page>" + page + "</page>";
            xml += "<total>" + total_page + "</total>";
            xml += "<records>" + count + "</records>";
            foreach (System.Data.DataRow row in dt.Rows)
            {
                xml += "<row>";
                xml += "<cell><![CDATA[" + row["c_phidonhang_id"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["c_donhang_id"] + "]]></cell>";

                xml += "<cell><![CDATA[]]></cell>";
                xml += "<cell><![CDATA[" + row["phi"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["phitang"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["tracho_ncc"] + "]]></cell>";

                xml += "<cell><![CDATA[]]></cell>";
                xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["phitang_kh"] + "]]></cell>";

                xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
                xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
                xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
                xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["diengiai_kh"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["check_chhet"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["check_ptrCBM"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["check_ptr"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["dsncc_check_ptr"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["dsncc_check_chhet"] + "]]></cell>";

                xml += "<cell><![CDATA[" + row["phi_vnd"] + "]]></cell>";
                xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
                xml += "</row>";
            }
            xml += "</rows>";
            context.Response.Write(xml);
        }
        catch (Exception ex)
        {
            context.Response.Write(strsql);
        }
    }

    public decimal get_gianhap(HttpContext context, string ma_dtkd_select) {
        //Tinh % theo nha cung ung
        decimal gianhap = 1;
        if (("VINHGIA").Contains(ma_dtkd_select))
        {
            gianhap = gianhap * 82 / 100;
        }
        //Neu doi tac la AN BINH, ANCO1, ANCO2, ANCOHUNG
        else if(ma_dtkd_select == "AN BINH" | ma_dtkd_select == "ANCO1" | ma_dtkd_select == "ANCO2" | ma_dtkd_select == "ANCOHUNG")
        {
            gianhap = gianhap * 80 / 100;
        }
        //Neu doi tac la TITAN
        else if(ma_dtkd_select == "TITAN")
        {
            gianhap = gianhap * 95 / 100;
        }

        return gianhap;
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
