<%@ WebHandler Language="C#" Class="NhapXuatController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;

public class NhapXuatController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "activenk":
                this.ActiveNhapKho(context);
                break;
            case "deactivenk":
                this.DeactiveNhapKho(context);
                break;
            case "createxk":
                this.CreateXuatKho(context);
                break;
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

    public void CreateXuatKho(HttpContext context)
    {
        String ids = context.Request.QueryString["nkId"];
        string msg = Public.taoPhieuXK(context, ids);
        context.Response.Write(msg);
        //c_nhapxuat m = db.c_nhapxuats.Single(p => p.c_nhapxuat_id == id);
        //if (m.md_trangthai_id.Equals("HIEULUC"))
        //{
        //    // Kiểm tra các đơn hàng đã kết thúc chưa?
        //    String sqlStop = "select count(*) from c_donhang dh where dh.md_trangthai_id ='KETTHUC' AND dh.c_donhang_id IN( select distinct c_donhang_id from c_nhapxuat where c_nhapxuat_id = '" + m.c_nhapxuat_id + "' )";
        //    int countStop = (int)mdbc.ExecuteScalar(sqlStop);
        //    if (countStop > 0)
        //    {
        //        context.Response.Write("Tạo phiếu xuất thất bại! Vì có đơn hàng đã kết thúc!");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            String selSoPhieu = @"select (sct.tiento + '/' + CAST(sct.so_duocgan as nvarchar(32)) + '/' + sct.hauto) as sophieu 
        //                                from md_sochungtu sct, md_loaichungtu lct 
        //                                where sct.md_loaichungtu_id = lct.md_loaichungtu_id 
        //                                AND lct.kieu_doituong = 'XK'";
        //            String sophieu = (String)mdbc.ExecuteScalar(selSoPhieu);

        //            //mdbc.ExcuteNonProcedure("CopyToXK", "@c_nhapxuat_id", id, "@sophieu", sophieu);
        //            context.Response.Write(String.Format("Đã tạo phiếu xuất thành công với số phiếu {0}.!", sophieu));
        //        }
        //        catch (Exception ex)
        //        {
        //            context.Response.Write(ex.Message);
        //        }
        //    }
        //}
        //else {
        //    context.Response.Write(String.Format("Phiếu nhập {0} chưa được hiệu lực.!", m.sophieu));
        //}
    }

    public void ActiveNhapKho(HttpContext context)
    {
        try
        {
            String id = context.Request.QueryString["nkId"];
            c_nhapxuat m = db.c_nhapxuats.Single(p => p.c_nhapxuat_id == id);
            if (m.md_trangthai_id.Equals("HIEULUC"))
            {
                context.Response.Write(String.Format("Hiệu trạng thái của phiếu nhập {0} đã là hiệu lực.!", m.sophieu));
            }
            else
            {
                // kiểm tra số lượng trên danh sách đặt hàng và trên phiếu nhập
                // đếm dòng danh sách đặt hàng có số lượng còn lại bé hơn 0
                // và số lượng nhập thực tế của phiếu nhập lớn hơn số lượng còn lại.
                // nếu kết quả lớn hơn 0 thì không cho hiệu lực vì tồn tại mã hàng đã xuất hết số lượng
                String sqlCount = @"select count(*) from c_dongdsdh ddsdh 
								where ddsdh.c_dongdsdh_id IN(
									select c_dongdsdh_id from c_dongnhapxuat where c_nhapxuat_id = @c_nhapxuat_id AND slthuc_nhapxuat > ddsdh.sl_conlai
								) AND ddsdh.sl_conlai <= 0";
                int count = (int)mdbc.ExecuteScalar(sqlCount, "@c_nhapxuat_id", id);
                if (count > 0)
                {
                    context.Response.Write(String.Format("Không thể hiệu lực vì phiếu nhập kho {0} có chứa mã hàng đã nhập hết số lượng.!", m.sophieu));
                }
                else
                {
                    String update = "update c_dongnhapxuat set hoatdong = 1 where c_nhapxuat_id = @id";
                    mdbc.ExcuteNonQuery(update, "@id", id);

                    mdbc.ExcuteNonProcedure("dbo.p_hieulucnhapxuat", "@nhapxuat_id", id);

                    m.md_trangthai_id = "HIEULUC";
                    db.SubmitChanges();

                    foreach(c_dongnhapxuat dnx in db.c_dongnhapxuats.Where(s=>s.c_nhapxuat_id == id)){
                        c_dongdsdh ddsdh = db.c_dongdsdhs.FirstOrDefault(s=>s.c_dongdsdh_id == dnx.c_dongdsdh_id);
                        c_dongdonhang ddh = db.c_dongdonhangs.FirstOrDefault(d => d.c_dongdonhang_id.Equals(dnx.c_dongdonhang_id));
                        if(ddsdh != null & ddh != null) {
                            c_danhsachdathang dsdh = db.c_danhsachdathangs.FirstOrDefault(s=>s.c_danhsachdathang_id == ddsdh.c_danhsachdathang_id);
                            if(dsdh != null) {
                                decimal cbm1row = ddh.v2.Value / ddh.soluong.Value;
                                decimal sltd_hn = Math.Round(cbm1row * dnx.slthuc_nhapxuat.Value, 3);
                                foreach(c_kehoachxuathang khxh in db.c_kehoachxuathangs.Where(s=>s.c_danhsachdathang_id == dsdh.sochungtu)) {
                                    //khxh.cbm = khxh.cbm - sltd_hn;
                                    khxh.cbm_conlai = khxh.cbm_conlai - sltd_hn;
                                }
                            }
                        }
                    }
                    db.SubmitChanges();

                    context.Response.Write(String.Format("Hiệu lực phiếu nhập kho {0} thành công.!", m.sophieu));
                }
            }
        }catch(Exception ex)
        {
            context.Response.Write("ERROR: " + ex.Message);
        }
    }

    public void DeactiveNhapKho(HttpContext context)
    {
        //try
        {
            String id = context.Request.QueryString["nkId"];
            c_nhapxuat m = db.c_nhapxuats.Single(p => p.c_nhapxuat_id == id);
            int r = 0;

            if (m.md_trangthai_id.Equals("SOANTHAO"))
            {
                context.Response.Write(String.Format("Hiệu trạng thái của phiếu nhập {0} đã là soạn thảo.!", m.sophieu));
            }
            else
            {
                // kiểm tra số lượng trên danh sách đặt hàng và trên phiếu nhập
                // đếm dòng danh sách đặt hàng có số lượng còn lại bé hơn 0
                // và số lượng nhập thực tế của phiếu nhập lớn hơn số lượng còn lại.
                // nếu kết quả lớn hơn 0 thì không cho hiệu lực vì tồn tại mã hàng đã xuất hết số lượng
                String sqlCount = @"select count(*) as count from c_nhapxuat nx
									left join c_dongnhapxuat dnx on dnx.c_nhapxuat_id = nx.c_nhapxuat_id
									where nx.md_loaichungtu_id = 'XK' and dnx.dongnhapxuat_ref in (select c_dongnhapxuat_id from c_dongnhapxuat where c_nhapxuat_id = @c_nhapxuat_id)";
                int count = (int)mdbc.ExecuteScalar(sqlCount, "@c_nhapxuat_id", id);
                if (count > 0)
                {
                    context.Response.Write(String.Format("Không thể thay đổi trạng thái \"Soạn thảo\" vì phiếu nhập kho {0} đã tạo phiếu xuất.!", m.sophieu));
                }
                else
                {
                    String update = "update c_dongnhapxuat set hoatdong = 1 where c_nhapxuat_id = @id";

                    mdbc.ExcuteNonQuery(update, "@id", id);
                    mdbc.ExcuteNonProcedure("dbo.p_soanthaonhapxuat", "@nhapxuat_id", id);

                    m.md_trangthai_id = "SOANTHAO";
                    db.SubmitChanges();

                    foreach(c_dongnhapxuat dnx in db.c_dongnhapxuats.Where(s=>s.c_nhapxuat_id == id)){
                        c_dongdsdh ddsdh = db.c_dongdsdhs.FirstOrDefault(s=>s.c_dongdsdh_id == dnx.c_dongdsdh_id);
                        c_dongdonhang ddh = db.c_dongdonhangs.FirstOrDefault(d => d.c_dongdonhang_id.Equals(dnx.c_dongdonhang_id));
                        if(ddsdh != null & ddh != null) {
                            c_danhsachdathang dsdh = db.c_danhsachdathangs.FirstOrDefault(s=>s.c_danhsachdathang_id == ddsdh.c_danhsachdathang_id);
                            if(dsdh != null) {
                                decimal cbm1row = ddh.v2.Value / ddh.soluong.Value;
                                decimal sltd_hn = Math.Round(cbm1row * dnx.slthuc_nhapxuat.Value, 3);
                                foreach(c_kehoachxuathang khxh in db.c_kehoachxuathangs.Where(s=>s.c_danhsachdathang_id == dsdh.sochungtu)) {
                                    //khxh.cbm = khxh.cbm - sltd_hn;
                                    khxh.cbm_conlai = khxh.cbm_conlai + sltd_hn;
                                }
                            }
                        }
                    }
                    db.SubmitChanges();

                    context.Response.Write(String.Format("Thay đổi trạng thái \"Soạn thảo\" phiếu nhập kho {0} thành công.!", m.sophieu));
                }
            }
        }
        //catch(Exception ex)
        {
            //context.Response.Write("ERROR: " + ex.Message);
        }
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_nhapxuat_id, sophieu from c_nhapxuat where hoatdong = 1 AND md_loaichungtu_id = N'NK' ";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }


    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        c_nhapxuat m = db.c_nhapxuats.Single(p => p.c_nhapxuat_id == context.Request.Form["id"]);
        if (m.md_trangthai_id.Equals("SOANTHAO"))
        {
            // m.c_donhang_id = context.Request.Form["c_donhang_id"];
            m.sophieu = context.Request.Form["sophieu"];
            m.sophieunx = context.Request.Form["sophieunx"];
            m.ngay_giaonhan = DateTime.ParseExact(context.Request.Form["ngay_giaonhan"], "dd/MM/yyyy", null);
            //m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
            m.nguoigiao = context.Request.Form["nguoigiao"];
            m.nguoinhan = context.Request.Form["nguoinhan"];
            m.sophieukhach = context.Request.Form["sophieukhach"];
            m.ngay_phieu = DateTime.ParseExact(context.Request.Form["ngay_phieu"], "dd/MM/yyyy", null);
            // m.md_kho_id = context.Request.Form["md_kho_id"];
            m.soseal = context.Request.Form["soseal"];
            m.socontainer = 1;
            m.container = context.Request.Form["container"];
            m.loaicont = context.Request.Form["loaicont"];
            m.phicong = (context.Request.Form["phicong"] == "" ? 0 : decimal.Parse(context.Request.Form["phicong"]));
            m.phitru = (context.Request.Form["phitru"] == "" ? 0 : decimal.Parse(context.Request.Form["phitru"]));

            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;

            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi trạng thái là hiệu lực!");
        }
    }

    public void add(HttpContext context)
    {
        //String h = context.Request.Form["hoatdong"].ToLower();
        //bool hd = false;
        //if (h.Equals("on") || h.Equals("true"))
        //{ hd = true; }

        //c_nhapxuat mnu = new c_nhapxuat
        //{
        //    c_nhapxuat_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
        //    c_donhang_id = context.Request.Form["c_donhang_id"],
        //    sophieu = context.Request.Form["sophieu"],
        //    sophieunx = context.Request.Form["sophieunx"],
        //    ngay_giaonhan = DateTime.ParseExact(context.Request.Form["ngay_giaonhan"], "dd/MM/yyyy", null),
        //    md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"],
        //    nguoigiao = context.Request.Form["nguoigiao"],
        //    nguoinhan = context.Request.Form["nguoinhan"],
        //    sophieukhach = context.Request.Form["sophieukhach"],
        //    ngay_phieu = DateTime.ParseExact(context.Request.Form["ngay_phieu"], "dd/MM/yyyy", null),
        //    md_kho_id = context.Request.Form["md_kho_id"],
        //    soseal = context.Request.Form["soseal"],
        //    socontainer = context.Request.Form["socontainer"],
        //    loaicont = context.Request.Form["loaicont"],
        //    md_trangthai_id = context.Request.Form["md_trangthai_id"],
        //    md_loaichungtu_id = "NK",

        //    mota = context.Request.Form["mota"],
        //    hoatdong = hd,
        //    ngaytao = DateTime.Now,
        //    ngaycapnhat = DateTime.Now
        //};

        //db.c_nhapxuats.InsertOnSubmit(mnu);
        //db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        try
        {
            String id = context.Request.Form["id"];
            id = id.Insert(0, "'");
            id = id.Insert(id.Length, "'");
            id = id.Replace(",", "','");

            String selActive = "select count(*) from c_nhapxuat where c_nhapxuat_id IN(" + id + ") AND md_trangthai_id = 'HIEULUC'";
            int countActive = (int)mdbc.ExecuteScalar(selActive);
            if (countActive != 0)
            {
                jqGridHelper.Utils.writeResult(0, "Không thể xóa khi phiếu nhập kho đã hiệu lực.!");
            }
            else
            {
                string sql_del = @"delete from c_nhapxuat where c_nhapxuat_id in(" + id + ")";
                string sql_del_tail = @"delete from c_dongnhapxuat where c_nhapxuat_id in (" + id + ")";
                mdbc.ExcuteNonQuery(sql_del_tail);
                mdbc.ExcuteNonQuery(sql_del);
            }
        }
        catch (Exception ex)
        {
            jqGridHelper.Utils.writeResult(0, "Lỗi:" + ex.Message);
        }
    }

    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";
        bool isadmin = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(context))).isadmin.Value;

        //// filter
        String filter = "";
        bool _search = bool.Parse(context.Request.QueryString["_search"]);
        if (_search)
        {
            String _filters = context.Request.QueryString["filters"];
            jqGridHelper.Filter f = jqGridHelper.Filter.CreateFilter(_filters);
            filter = f.ToScript();
        }

        // phân quyền theo nhóm
        String manv = UserUtils.getUser(context);
        String strAccount = "";
        System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
        foreach (String item in lstAccount)
        {
            strAccount += String.Format(", '{0}'", item);
        }
        strAccount = String.Format("'{0}'{1}", manv, strAccount);

        String sqlCount = "SELECT COUNT(*) AS count " +
            " FROM c_nhapxuat nx " +
            " Left join c_donhang dh on nx.c_donhang_id = dh.c_donhang_id " +
            " Left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " Left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE 1 = 1 " +
            " {0} " +
            " AND nx.md_loaichungtu_id = N'NK' " + filter;

        sqlCount = String.Format(sqlCount, isadmin == true ? "" : "AND nx.nguoitao IN(" + strAccount + ") ");

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
            sidx = "nx.c_nhapxuat_id";
        }

        string strsql = "select * from( " +
            " select nx.c_nhapxuat_id, nx.md_trangthai_id, dh.sochungtu, nx.sophieu, nx.sophieunx " +
            "   , nx.ngay_giaonhan, dtkd.ten_dtkd, nx.nguoigiao, nx.nguoinhan " +
            "   , nx.sophieukhach, nx.ngay_phieu, kho.ten_kho " +
            "   , nx.soseal, nx.container, nx.loaicont " +
            "   , nx.md_loaichungtu_id, nx.ngaytao, nx.nguoitao, nx.ngaycapnhat, nx.nguoicapnhat " +
            "   , nx.mota, nx.hoatdong, phicong, phitru, grandtotal, nx.discount " +
            "   , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_nhapxuat nx " +
            " Left join c_donhang dh on nx.c_donhang_id = dh.c_donhang_id " +
            " Left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
            " Left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE 1 = 1 " +
            " {0} " +
            "  AND nx.md_loaichungtu_id = N'NK' " + filter +
            " )P where RowNum > @start AND RowNum < @end";

        strsql = String.Format(strsql, isadmin == true ? "" : "AND nx.nguoitao IN(" + strAccount + ") ");

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
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieunx"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngay_giaonhan"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoigiao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoinhan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieukhach"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngay_phieu"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_kho"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["soseal"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["container"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["loaicont"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phicong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phitru"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["discount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["grandtotal"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_loaichungtu_id"] + "]]></cell>";
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
