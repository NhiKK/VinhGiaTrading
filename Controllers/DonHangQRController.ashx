<%@ WebHandler Language="C#" Class="DonHangQRController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
public class DonHangQRController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();
    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "chuyenThanhPO":
                this.chuyenThanhPO(context);
                break;
            case "getpoinformation":
                this.getPoInformation(context);
                break;
            default:
                switch (oper)
                {
                    case "del":
                        this.del(context);
                        break;
                    default:
                        this.load(context);
                        break;
                }
                break;
        }
    }

    public void getPoInformation(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        string c_donhang_id = context.Request.QueryString["poId"];
        string msg = "";
        try
        {
            decimal totalQuantity = 0, totalSalary = 0, totalCbm = 0;
            c_donhangqr dh = db.c_donhangqrs.FirstOrDefault(d => d.c_donhangqr_id.Equals(c_donhang_id));
            if (dh != null)
            {
                var ddh = from c in db.c_dongdonhangqrs where c.c_donhangqr_id.Equals(dh.c_donhangqr_id) select c;

                totalQuantity = ddh.Sum(p => p.soluong).GetValueOrDefault(0);
                totalCbm =  ddh.Sum(p => p.v2).GetValueOrDefault(0);
                totalSalary = ddh.ToList().Sum(p => p.soluong.GetValueOrDefault(0) * p.giafob.GetValueOrDefault(0)).DecRound(2);
                decimal discountVal = (totalSalary * dh.discount.GetValueOrDefault(0) / 100).DecRound(2);
                totalSalary = (totalSalary - discountVal);


                msg = string.Format("Đơn hàng:{0}, CBM:{1:#,##0.000}, Amount:{2:#,##0.00}, Quantity:{3:#,##0}", dh.sochungtu, totalCbm, totalSalary, totalQuantity);
            }
            else
            {
                msg = "Đơn hàng này đã bị xóa, hãy thử làm mới dữ liệu.";
            }
        }
        catch (Exception e)
        {
            msg = e.Message;
        }

        context.Response.Write(msg);
    }

    public void chuyenThanhPO(HttpContext context)
    {
        string c_donhang_id = context.Request.Form["id"];
        c_donhangqr bgqr = db.c_donhangqrs.FirstOrDefault(s => s.c_donhangqr_id == c_donhang_id);
        Dictionary<string, object> lstMsg = new Dictionary<string, object>();
        int status = 0;
        string mess = "";
        if (bgqr != null)
        {
            md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.Where(s => s.md_doitackinhdoanh_id == bgqr.md_doitackinhdoanh_id).FirstOrDefault();

            if (string.IsNullOrEmpty(dtkd.ma_dtkd))
            {
                mess += "<br>Thiếu mã đối tác.";
            }
            if (string.IsNullOrEmpty(dtkd.ten_dtkd))
            {
                mess += "<br>Thiếu tên đối tác.";
            }
            if (string.IsNullOrEmpty(dtkd.tel))
            {
                mess += "<br>Thiếu số điện thoại của đối tác.";
            }
            if (string.IsNullOrEmpty(dtkd.fax))
            {
                mess += "<br>Thiếu số Fax.";
            }
            if (string.IsNullOrEmpty(dtkd.md_quocgia_id))
            {
                mess += "<br>Thiếu quốc gia.";
            }
            if (string.IsNullOrEmpty(dtkd.md_khuvuc_id))
            {
                mess += "<br>Thiếu khu vực.";
            }

            if (mess.Length > 0)
            {
                mess = mess.Substring(4);
            }
            else
            {
                try
                {
                    string bgid = Guid.NewGuid().ToString().Replace("-", "");
                    String sochungtu = jqGridHelper.Utils.taoChungTuPO(bgqr.md_doitackinhdoanh_id, 1, DateTime.Now.Year.ToString());
                    c_donhang bg = bgqr.Cast<c_donhang>();
                    bg.c_donhang_id = bgid;
                    bg.sochungtu = sochungtu;
                    db.c_donhangs.InsertOnSubmit(bg);

                    foreach (c_dongdonhangqr ctbgqr in db.c_dongdonhangqrs.Where(s => s.c_donhangqr_id == c_donhang_id))
                    {
                        c_dongdonhang ctbg = ctbgqr.Cast<c_dongdonhang>();
                        ctbg.c_dongdonhang_id = Guid.NewGuid().ToString().Replace("-", "");
                        ctbg.c_donhang_id = bgid;
                        db.c_dongdonhangs.InsertOnSubmit(ctbg);
                        db.c_dongdonhangqrs.DeleteOnSubmit(ctbgqr);
                    }

                    db.c_donhangqrs.DeleteOnSubmit(bgqr);
                    db.SubmitChanges();
                    status = 1;
                    mess = sochungtu;
                }
                catch (Exception ex)
                {
                    mess = ex.Message;
                }
            }
        }
        else
        {
            mess = "Không tìm thấy đơn hàng, hãy làm mới lại lưới dữ liệu.";
        }

        lstMsg.Add("status", status);
        lstMsg.Add("mess", mess);

        context.Response.Write(JsonConvert.SerializeObject(lstMsg));
    }

    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        var po = db.c_donhangqrs.Single(q => q.c_donhangqr_id.Equals(id));
        if (po.md_trangthai_id.Equals("SOANTHAO"))
        {
            foreach (c_dongdonhangqr ctbg in db.c_dongdonhangqrs.Where(s => s.c_donhangqr_id == id))
            {
                db.c_dongdonhangqrs.DeleteOnSubmit(ctbg);
            }
            db.SubmitChanges();

            db.c_donhangqrs.DeleteOnSubmit(po);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa đơn hàng đã hiệu lực!");
        }
    }
    #region load
    public void load(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "UTF-8";

        bool isadmin = db.nhanviens.FirstOrDefault(p => p.manv.Equals(UserUtils.getUser(context))).isadmin.Value;

        String md_trangthai_id = context.Request.QueryString["status"];
        md_trangthai_id = md_trangthai_id == null ? "" : md_trangthai_id.ToUpper();

        String fStatus = "";

        if (md_trangthai_id.Equals("ALL"))
        {
            fStatus = "";
        }
        else
        {
            fStatus = String.Format("AND dh.md_trangthai_id = N'{0}' ", md_trangthai_id);
        }

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


        String sqlCount = @"SELECT COUNT(1) AS count 
            FROM c_donhangqr dh with (nolock)
			left join md_doitackinhdoanh dtkd with (nolock) on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			left join md_cangbien cbien with (nolock) on dh.md_cangbien_id = cbien.md_cangbien_id
            left join md_paymentterm pmt with (nolock) on dh.md_paymentterm_id = pmt.md_paymentterm_id
			left join md_trongluong tl with (nolock) on dh.md_trongluong_id = tl.md_trongluong_id
			left join md_kichthuoc kt with (nolock) on dh.md_kichthuoc_id = kt.md_kichthuoc_id
			left join md_dongtien dtien with (nolock) on dh.md_dongtien_id = dtien.md_dongtien_id
			left join md_nganhang ngh with (nolock) on dh.md_nganhang_id = ngh.md_nganhang_id
            WHERE 1 = 1 {0} " + fStatus + " {1}";

        sqlCount = String.Format(sqlCount, isadmin == true ? "" : "AND dh.nguoitao IN(" + strAccount + ")", filter);

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
            sidx = "dh.c_donhangqr_id";
        }

        string strsql = @"select * from( 
            select dh.c_donhangqr_id, dh.md_trangthai_id, dh.donhang_mau, dh.sochungtu, dh.customer_order_no, 
            dtkd.ma_dtkd, dh.ngaylap, dh.nguoilap, 
            cbien.ten_cangbien, dh.discount, dh.shipmenttime, 
            dh.shipmentdate, pmt.ten_paymentterm, ngh.ma_nganhang,  tl.ten_trongluong, 
            dtien.ma_iso as tendongtien, kt.ten_kichthuoc, 
            dh.payer, dh.portdischarge, 
			(
				dh.amount - (dh.amount * dh.discount)/100 + 
				isnull((select SUM(phi) from c_phidonhang where phitang = 1 and c_donhangqr_id = dh.c_donhangqr_id and isnull(hoatdong, 0) = 1), 0) -
				isnull((select SUM(phi) from c_phidonhang where phitang = 0 and c_donhangqr_id = dh.c_donhangqr_id and isnull(hoatdong, 0) = 1), 0)
			) as amount, 
            dh.totalcbm, dh.totalcbf, dh.dagui_mail, 
            dh.cont20, dh.cont40, dh.cont40hc, dh.contle, dh.cont45, 
            dh.ismakepi, dh.isdonhangtmp, 
            (case when md_nguoilienhe_id is null then '' else (select ma_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = dh.md_nguoilienhe_id) end) as nguoilienhe, 
            dh.hoahong, dh.ngaydieuchinh, 
            dh.ngaytao, dh.nguoitao, dh.ngaycapnhat, dh.nguoicapnhat, dh.mota, dh.hoatdong, 
            dh.gia_db,
            dh.chkPBGCu,
            dh.phienbangiacu,
            dh.ghichu,
            ROW_NUMBER() OVER (ORDER BY {2} {3}) as RowNum 
            FROM c_donhangqr dh with (nolock)
			left join md_doitackinhdoanh dtkd with (nolock) on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			left join md_cangbien cbien with (nolock) on dh.md_cangbien_id = cbien.md_cangbien_id
            left join md_paymentterm pmt with (nolock) on dh.md_paymentterm_id = pmt.md_paymentterm_id
			left join md_trongluong tl with (nolock) on dh.md_trongluong_id = tl.md_trongluong_id
			left join md_kichthuoc kt with (nolock) on dh.md_kichthuoc_id = kt.md_kichthuoc_id
			left join md_dongtien dtien with (nolock) on dh.md_dongtien_id = dtien.md_dongtien_id
			left join md_nganhang ngh with (nolock) on dh.md_nganhang_id = ngh.md_nganhang_id
            WHERE 1 = 1 {0} {4} {1} )P where RowNum > @start AND RowNum < @end";

        strsql = String.Format(strsql,
            isadmin == true ? "" : "AND dh.nguoitao IN(" + strAccount + ")",
            filter, sidx, sord, fStatus);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_donhangqr_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["customer_order_no"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_cangbien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["shipmenttime"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalcbm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["amount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["discount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_paymentterm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_nganhang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoahong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoilienhe"] + "]]></cell>";

            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaylap"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoilap"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["shipmentdate"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + ((!row["ngaydieuchinh"].ToString().Equals("")) ? DateTime.Parse(row["ngaydieuchinh"].ToString()).ToString("dd/MM/yyyy") : "") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["donhang_mau"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isdonhangtmp"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_trongluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tendongtien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_kichthuoc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["payer"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["portdischarge"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalcbf"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cont20"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cont40"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cont40hc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["cont45"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["contle"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["dagui_mail"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ismakepi"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["gia_db"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chkPBGCu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phienbangiacu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghichu"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }
    #endregion

    public bool IsReusable
    {
        get { return false; }
    }
}
