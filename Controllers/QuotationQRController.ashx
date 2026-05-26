<%@ WebHandler Language="C#" Class="QuotationQRController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

public class QuotationQRController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
            case "chuyenThanhQO":
                this.chuyenThanhQO(context);
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

    #region oper
    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        var qo = db.c_baogiaqrs.Single(q=>q.c_baogiaqr_id.Equals(id));
        if (qo.md_trangthai_id.Equals("SOANTHAO"))
        {
            foreach(c_chitietbaogiaqr ctbg in db.c_chitietbaogiaqrs.Where(s=>s.c_baogiaqr_id == id))
            {
                db.c_chitietbaogiaqrs.DeleteOnSubmit(ctbg);
            }
            db.SubmitChanges();

            db.c_baogiaqrs.DeleteOnSubmit(qo);
            db.SubmitChanges();
        }
        else
        {
            jqGridHelper.Utils.writeResult(0, "Không thể xóa báo giá đã hiệu lực!");
        }
    }

    #region load
    public void chuyenThanhQO(HttpContext context)
    {
        string c_baogia_id = context.Request.Form["id"];
        c_baogiaqr bgqr = db.c_baogiaqrs.FirstOrDefault(s => s.c_baogiaqr_id == c_baogia_id);
        Dictionary<string, object> lstMsg = new Dictionary<string, object>();   
        int status = 0;
        string mess = "";
        if (bgqr != null)
        {
			md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.Where(s=>s.md_doitackinhdoanh_id == bgqr.md_doitackinhdoanh_id).FirstOrDefault();
			
			if(string.IsNullOrEmpty(dtkd.ma_dtkd)) {
				mess += "<br>Thiếu mã đối tác.";
			}
			if(string.IsNullOrEmpty(dtkd.ten_dtkd)) {
				mess += "<br>Thiếu tên đối tác.";
			}
			if(string.IsNullOrEmpty(dtkd.tel)) {
				mess += "<br>Thiếu số điện thoại của đối tác.";
			}
			if(string.IsNullOrEmpty(dtkd.fax)) {
				mess += "<br>Thiếu số Fax.";
			}
			if(string.IsNullOrEmpty(dtkd.md_quocgia_id)){
				mess += "<br>Thiếu quốc gia.";
			}
			if(string.IsNullOrEmpty(dtkd.md_khuvuc_id)){
				mess += "<br>Thiếu khu vực.";
			}
			
			if(mess.Length > 0) {
				mess = mess.Substring(4);
			}
			else {
				try
				{
					string bgid = Guid.NewGuid().ToString().Replace("-", "");
					String sochungtu = jqGridHelper.Utils.taoChungTuQO(bgqr.md_doitackinhdoanh_id, 1, DateTime.Now.Year.ToString());
					c_baogia bg = bgqr.Cast<c_baogia>();
					bg.c_baogia_id = bgid;
					bg.sobaogia = sochungtu;
					db.c_baogias.InsertOnSubmit(bg);

					foreach (c_chitietbaogiaqr ctbgqr in db.c_chitietbaogiaqrs.Where(s => s.c_baogiaqr_id == c_baogia_id))
					{
						c_chitietbaogia ctbg = ctbgqr.Cast<c_chitietbaogia>();
						ctbg.c_chitietbaogia_id = Guid.NewGuid().ToString().Replace("-", "");
						ctbg.c_baogia_id = bgid;
						db.c_chitietbaogias.InsertOnSubmit(ctbg);
						db.c_chitietbaogiaqrs.DeleteOnSubmit(ctbgqr);
					}

					db.c_baogiaqrs.DeleteOnSubmit(bgqr);
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
            mess = "Không tìm thấy báo giá, hãy làm mới lại lưới dữ liệu.";
        }

        lstMsg.Add("status", status);
        lstMsg.Add("mess", mess);

        context.Response.Write(JsonConvert.SerializeObject(lstMsg));
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

        // phân quy?n theo nhóm
        String manv = UserUtils.getUser(context);
        String strAccount = "";
        System.Collections.Generic.List<String> lstAccount = LinqUtils.GetUserListInGroup(manv);
        foreach (String item in lstAccount)
        {
            strAccount += String.Format(", '{0}'", item);
        }
        strAccount = String.Format("'{0}'{1}", manv, strAccount);

        String sqlCount = @"SELECT COUNT(1) AS count
            FROM c_baogiaqr baogia with (nolock)
			LEFT JOIN md_doitackinhdoanh dtkd with (nolock) on baogia.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
			LEFT JOIN md_cangbien cb with (nolock) on baogia.md_cangbien_id = cb.md_cangbien_id 
            LEFT JOIN md_dongtien dt with (nolock) on baogia.md_dongtien_id = dt.md_dongtien_id 
            LEFT JOIN md_trongluong tl with (nolock) on baogia.md_trongluong_id = tl.md_trongluong_id
            LEFT JOIN md_kichthuoc kt with (nolock) on baogia.md_kichthuoc_id = kt.md_kichthuoc_id
            WHERE 1=1
            {0}
            {1}";
        sqlCount = String.Format(sqlCount, isadmin == true ? "" : "AND baogia.nguoitao IN(" + strAccount + ")", filter);

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
            sidx = "baogia.ngaytao";
            sord = "desc";
        }


        string strsql = @"select * from( 
            select baogia.c_baogiaqr_id, baogia.md_trangthai_id, baogia.sobaogia, dtkd.ma_dtkd, 
            ngaybaogia, ngayhethan, cb.ten_cangbien, baogia.shipmenttime, baogia.moq_item_color,  tl.ten_trongluong, 
            baogia.totalcbm, baogia.totalcbf, baogia.totalquo, dt.ma_iso, kt.ten_kichthuoc, baogia.isform,  
            baogia.ngaytao, baogia.nguoitao, baogia.ngaycapnhat, baogia.nguoicapnhat, baogia.mota, baogia.hoatdong, 
            baogia.gia_db, baogia.ghepbo, baogia.chkPBGCu, baogia.phienbangiacu,
            ROW_NUMBER() OVER (ORDER BY {2} {3}) as RowNum 
            FROM c_baogiaqr baogia with (nolock) 
			LEFT JOIN md_doitackinhdoanh dtkd with (nolock) on baogia.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
			LEFT JOIN md_cangbien cb with (nolock) on baogia.md_cangbien_id = cb.md_cangbien_id 
            LEFT JOIN md_dongtien dt with (nolock) on baogia.md_dongtien_id = dt.md_dongtien_id 
            LEFT JOIN md_trongluong tl with (nolock) on baogia.md_trongluong_id = tl.md_trongluong_id
            LEFT JOIN md_kichthuoc kt with (nolock) on baogia.md_kichthuoc_id = kt.md_kichthuoc_id 
            WHERE 1=1
            {0}
            {1}
        )P where RowNum > @start AND RowNum < @end";

        strsql = String.Format(strsql, isadmin == true ? "" : "AND baogia.nguoitao IN(" + strAccount + ")", filter, sidx, sord);

        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_baogiaqr_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sobaogia"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaybaogia"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngayhethan"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_cangbien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["shipmenttime"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["moq_item_color"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_trongluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalcbm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalquo"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_iso"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_kichthuoc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["isform"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaycapnhat"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoicapnhat"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["mota"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoatdong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["gia_db"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["chkPBGCu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phienbangiacu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ghepbo"] + "]]></cell>";
            xml += "</row>";
        }
        xml += "</rows>";
        context.Response.Write(xml);
    }
    #endregion

    #endregion

    public bool IsReusable
    {
        get { return false; }
    }
}
