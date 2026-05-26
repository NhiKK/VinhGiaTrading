<%@ WebHandler Language="C#" Class="DeTaiMainController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using jqGridHelper;

public class DeTaiMainController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
			case "updatestatus":
                this.UpdateStatus(context);
                break;
            case "getoption":
                this.getSelectOption(context);
                break;
            case "HinhAnh":
                this.HinhAnh(context);
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
	
    public void HinhAnh(HttpContext context){
        string msg = "";
        if (context.Request.Files.Count != 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 1; i <= files.Count; i++)
            {
                string fileName = files[i - 1].FileName;
                int mimeI = fileName.LastIndexOf(".");
                string mime = fileName.Substring(mimeI);
                string name = fileName.Substring(0, mimeI);
                if(mime.ToLower() == ".jpg") {
                    try {
                        string[] arrN = name.Split('-');
                        if(arrN.Length == 2) {
                            string cl = arrN[0], dt = arrN[1];
                            int countDT = db.md_detais.Where(p => p.code_cl.Equals(cl) & p.code_dt.Equals(dt)).Count();
                            if(countDT > 0) {
                                files[i - 1].SaveAs(context.Server.MapPath("../images/products/detai/" + files[i - 1].FileName));
                                msg += "<div style='color:blue'>" + fileName + ": upload thành công.</div>";
                            }
                            else {
                               msg += "<div style='color:red'>" + fileName + ": không tồn tại đề tài này</div>"; 
                            }
                        }
                        else {
                            msg += "<div style='color:red'>" + fileName + ": không đúng quy cách đặt tên (CL-DT)</div>";
                        }
                    }
                    catch(Exception ex) {
                        msg += "<div style='color:red'>" + fileName + ":" + ex.Message + "</div>";
                    }
                }
                else {
                    msg += "<div style='color:red'>" + fileName + ": không đúng định dạng jpg</div>";
                }
            }
        }
        else {
            msg += "<div style='color:red'>Hãy chọn hình ảnh</div>";
        }
        context.Response.Write(msg);
    }

	public void UpdateStatus(HttpContext context)
    {
        String msg = "";
        try
        {   
            String id = context.Request.QueryString["md_detai_id"];
            String trangthai = context.Request.QueryString["trangthai"];
            md_detai sp = db.md_detais.FirstOrDefault(p => p.md_detai_id.Equals(id));
			
			//// filter
            String filter = "";
			bool _search = bool.Parse(context.Request.QueryString["_search"]);
			if (_search)
			{
				String _filters = context.Request.QueryString["filters"];
				Filter f = Filter.CreateFilter(_filters);
				filter = f.ToScript();
			}
			
			String sqlGetProductId = @"
			SELECT dt.md_detai_id 
			FROM md_detai dt, md_chungloai cl 
			WHERE 1=1 
			AND dt.md_chungloai_id = cl.md_chungloai_id 
            {1}
			{0}";
			var ttcp = new string[] { "TOACTIVE", "TOACTIVENHD" };
            if (sp != null | ttcp.Contains(trangthai))
			{
				bool next = true;
				switch (trangthai)
				{
					case "HIEULUC":
						msg = "Đã chuyển trạng thái đề tài thành \"Hiệu lực\"!";
						break;
					case "SOANTHAO":
						msg = "Đã chuyển trạng thái đề tài thành \"Soạn thảo\"!";
						break;
                    case "NHD":
						msg = "Đã chuyển trạng thái đề tài thành \"Ngưng hoạt động\"!";
						break;
					case "TOACTIVE":
						msg = "Đã chuyển tất cả đề tài được lọc từ \"soạn thảo\" sang \"Hiệu lực\"!";
						break;
					case "TOACTIVENHD":
						msg = "Đã chuyển tất cả đề tài được lọc từ \"Ngưng hoạt động\" sang \"Hiệu lực\"!";
						break;
					default:
						msg = "Không xác định được trạng thái của đề tài!";
						next = false;
						break;
				}

				if (next)
				{
					if (trangthai.Equals("TOACTIVE"))
					{
						sqlGetProductId = String.Format(sqlGetProductId, filter, "AND dt.trangthai = 'SOANTHAO'");
						String sqlUpdateToHIEULUC = "update md_detai set trangthai = 'HIEULUC' where md_detai_id IN(" + sqlGetProductId + ")";
						mdbc.ExcuteNonQuery(sqlUpdateToHIEULUC);
					}
					else if (trangthai.Equals("TOACTIVENHD"))
					{
						sqlGetProductId = String.Format(sqlGetProductId, filter, "AND dt.trangthai = 'NHD'");
						String sqlUpdateToHIEULUC = "update md_detai set trangthai = 'HIEULUC' where md_detai_id IN(" + sqlGetProductId + ")";
						mdbc.ExcuteNonQuery(sqlUpdateToHIEULUC);
					}
					else
					{	
						sp.trangthai = trangthai;
						db.SubmitChanges();
					}
				}
			}
			else
			{
				msg = "Chưa chọn đề tài hoặc đề tài không tồn tại!";
			}
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        context.Response.Write(msg);
    }

    public void getSelectOption(HttpContext context)
    {
        String sql = "select md_detai_id, (code_cl + '-' + code_dt) from md_detai where hoatdong = 1 order by (code_cl + '-' + code_dt) desc";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String parChungLoai_Id = context.Request.Form["md_chungloai_id"];
        String code_cl = db.md_chungloais.Where(cl => cl.md_chungloai_id.Equals(parChungLoai_Id)).FirstOrDefault().code_cl;

        md_detai m = db.md_detais.Where(p => p.md_detai_id == context.Request.Form["id"]).FirstOrDefault();
        m.md_chungloai_id = parChungLoai_Id;
        m.code_cl = code_cl;
        m.code_dt = context.Request.Form["code_dt"];
        m.tv_ngan = context.Request.Form["tv_ngan"];
        m.ta_ngan = context.Request.Form["ta_ngan"];
        m.tv_dai = context.Request.Form["tv_dai"];
        m.ta_dai = context.Request.Form["ta_dai"];
        m.hinhthucban = context.Request.Form["hinhthucban"];
        m.mota = context.Request.Form["mota"];
        m.hoatdong = hd;
        m.nguoicapnhat = UserUtils.getUser(context);
        m.ngaycapnhat = DateTime.Now;
        
        db.SubmitChanges();
    }

    public void add(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }

        String parChungLoai_Id = context.Request.Form["md_chungloai_id"];
        String code_cl = db.md_chungloais.Single(cl => cl.md_chungloai_id.Equals(parChungLoai_Id)).code_cl;

        md_detai mnu = new md_detai
        {
            md_detai_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
            md_chungloai_id = parChungLoai_Id,
            code_cl = code_cl,
            code_dt = context.Request.Form["code_dt"],
            tv_ngan = context.Request.Form["tv_ngan"],
            ta_ngan = context.Request.Form["ta_ngan"],
            tv_dai = context.Request.Form["tv_dai"],
            ta_dai = context.Request.Form["ta_dai"],
			hinhthucban = context.Request.Form["hinhthucban"],
			trangthai = "SOANTHAO",
            
            mota = context.Request.Form["mota"],
            hoatdong = hd,
            nguoitao = UserUtils.getUser(context),
            nguoicapnhat = UserUtils.getUser(context),
            ngaytao = DateTime.Now,
            ngaycapnhat = DateTime.Now
        };

        db.md_detais.InsertOnSubmit(mnu);
        db.SubmitChanges();
    }


    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        id = id.Insert(0, "'");
        id = id.Insert(id.Length, "'");
        id = id.Replace(",", "','");
        String sql = "delete md_detai where md_detai_id IN (" + id + ")";
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
        
        String sqlCount = "SELECT COUNT(*) AS count " + 
            " FROM md_detai dt, md_chungloai cl " +
            " WHERE dt.md_chungloai_id = cl.md_chungloai_id " +
            " " + filter;
        
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
            sidx = "cl.code_cl, dt.code_dt";
        }
		else {
			sidx = sidx + " " + sord;
		}

        string strsql = "select * from( " +
            " SELECT dt.md_detai_id, dt.trangthai, cl.code_cl, dt.code_dt, dt.tv_ngan, dt.ta_ngan " +
            " , dt.tv_dai, dt.ta_dai " +
            " , dt.hinhthucban, dt.ngaytao, dt.nguoitao, dt.ngaycapnhat, dt.nguoicapnhat, dt.mota, dt.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " ) as RowNum " +
            " FROM md_detai dt, md_chungloai cl " +
            " WHERE dt.md_chungloai_id = cl.md_chungloai_id " +
            " " + filter +
            " )P where RowNum > @start AND RowNum < @end";
        
        
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["md_detai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["trangthai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["code_cl"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["code_dt"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tv_ngan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ta_ngan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["tv_dai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ta_dai"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["hinhthucban"] + "]]></cell>";
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
