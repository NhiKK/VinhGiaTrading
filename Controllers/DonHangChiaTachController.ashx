<%@ WebHandler Language="C#" Class="DonHangChiaTachController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data;

public class DonHangChiaTachController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        this.load(context);
    }

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
        
        
        String sqlCount = @"SELECT COUNT(*) AS count
            FROM c_donhang dh
			left join md_doitackinhdoanh dtkd on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			left join md_cangbien cbien on dh.md_cangbien_id = cbien.md_cangbien_id
			left join md_paymentterm pmt on dh.md_paymentterm_id = pmt.md_paymentterm_id
			left join md_dongtien dtien on dh.md_dongtien_id = dtien.md_dongtien_id
			left join md_kichthuoc kt on dh.md_kichthuoc_id = kt.md_kichthuoc_id AND dh.c_donhang_id in( select distinct c_donhang_id from c_dongdonhang where soluong_conlai > 0 ) 
			left join md_trongluong tl on dh.md_trongluong_id = tl.md_trongluong_id
			left join md_nganhang ngh on dh.md_nganhang_id = ngh.md_nganhang_id 
            WHERE 1=1   
            {0}
            {1}
			{2}
			
			and 
			(
				SELECT COUNT(1) AS count
				FROM c_dongdonhang
				WHERE c_donhang_id = dh.c_donhang_id AND soluong_conlai > 0
			) > 0
			"
            ;

        sqlCount = String.Format(sqlCount, isadmin == true ? "" : "AND dh.nguoitao IN(" + strAccount + ")", filter, fStatus);
        
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
            sidx = "dh.c_donhang_id";
        }

        string strsql = @"select * from(
            select dh.c_donhang_id, dh.md_trangthai_id, dh.donhang_mau, dh.sochungtu,
            dtkd.ma_dtkd, dh.ngaylap, dh.nguoilap,
            cbien.ten_cangbien, dh.discount, dh.shipmenttime,
            dh.shipmentdate, pmt.ten_paymentterm, ngh.ma_nganhang,  tl.ten_trongluong,
            dtien.ma_iso as tendongtien, kt.ten_kichthuoc,
            dh.payer, dh.portdischarge, dh.sl_cont, dh.loai_cont,
            dh.amount, dh.totalcbm, dh.totalcbf, dh.dagui_mail, dh.ismakepi,
            (case when md_nguoilienhe_id is null then '' else (select ma_dtkd from md_doitackinhdoanh where md_doitackinhdoanh_id = dh.md_nguoilienhe_id) end) as nguoilienhe,
            dh.hoahong, dh.ngaydieuchinh,
            dh.ngaytao, dh.nguoitao, dh.ngaycapnhat, dh.nguoicapnhat, dh.mota, dh.hoatdong,
            ROW_NUMBER() OVER (ORDER BY "+ sidx + " " + sord + @") as RowNum
			FROM c_donhang dh
			left join md_doitackinhdoanh dtkd on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			left join md_cangbien cbien on dh.md_cangbien_id = cbien.md_cangbien_id
			left join md_paymentterm pmt on dh.md_paymentterm_id = pmt.md_paymentterm_id
			left join md_dongtien dtien on dh.md_dongtien_id = dtien.md_dongtien_id
			left join md_kichthuoc kt on dh.md_kichthuoc_id = kt.md_kichthuoc_id AND dh.c_donhang_id in( select distinct c_donhang_id from c_dongdonhang where soluong_conlai > 0 ) 
			left join md_trongluong tl on dh.md_trongluong_id = tl.md_trongluong_id
			left join md_nganhang ngh on dh.md_nganhang_id = ngh.md_nganhang_id 
             WHERE 1=1   
            {0}
            {1}
			{2}
			
			and 
			(
				SELECT COUNT(1) AS count
				FROM c_dongdonhang
				WHERE c_donhang_id = dh.c_donhang_id AND soluong_conlai > 0
			) > 0
            )P where RowNum > @start AND RowNum < @end";
        strsql = String.Format(strsql, isadmin == true ? "" : "AND dh.nguoitao IN(" + strAccount + ")", filter, fStatus);
        
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_donhang_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["donhang_mau"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaylap"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoilap"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_cangbien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["discount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["shipmenttime"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["shipmentdate"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + ((!row["ngaydieuchinh"].ToString().Equals("")) ?DateTime.Parse(row["ngaydieuchinh"].ToString()).ToString("dd/MM/yyyy"): "") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_paymentterm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_nganhang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_trongluong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tendongtien"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_kichthuoc"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["payer"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hoahong"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoilienhe"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["portdischarge"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sl_cont"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["loai_cont"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["amount"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalcbm"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["totalcbf"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["dagui_mail"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ismakepi"] + "]]></cell>";
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
