<%@ WebHandler Language="C#" Class="PhieuHanNgachController" %>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Data.Linq;
using System.Data;
using System.Data.SqlClient;

public class PhieuHanNgachController : IHttpHandler {
    LinqDBDataContext db = new LinqDBDataContext();
    
    public void ProcessRequest(HttpContext context)
    {
        String action, oper;
        action = context.Request.QueryString["action"];
        oper = context.Request.Form["oper"];
        switch (action)
        {
			case "startsendmail":
				this.startSendMail(context);
				break;
			case "sendmail":
				this.sendMail(context);
				break;
            case "TaoHanNgach":
                this.add(context);
                break;
            case "ActiveHanNgach":
                this.ActiveHanNgach(context);
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
	
	public void startSendMail(HttpContext context)
    {
        try
        {
            String c_phieuhanngach_id = context.Request.QueryString["c_phieuhanngach_id"];
            String manv = UserUtils.getUser(context);
            nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
            //Lấy phiếu hạng ngạch
			c_phieuhanngach phn = db.c_phieuhanngaches.FirstOrDefault(p=>p.c_phieuhanngach_id.Equals(c_phieuhanngach_id));
			
			c_donhang dh = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(phn.c_donhang_id));
			nhanvien nguoinhan = db.nhanviens.FirstOrDefault(p=>p.manv.Equals(dh.nguoitao));
		
            String str = "<table style='width:100%'> <tr> <td colspan='2'><input id='rdKcs' checked='checked' value='kcs' name='rdTypeMail' type='radio'/> <label for='rdKcs'>Gửi cho KCS</label></td> </tr> <tr><td>Gửi từ:</td> <td>{0}</td></tr><tr><td>Gửi cho:</td> <td>{1}</td></tr><tr><td>Cc:</td> <td>{2}</td>         </tr></table>";
            context.Response.Write(String.Format(str, nv.email, nguoinhan.email, nv.email_cc));
            //context.Response.Write(String.Format("<div>Gửi từ: {0}</div><div>Gửi đến: {1}</div><div>Cc: {2}</div>", nv.email, doitac.email, nv.email_cc));
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }
	
	
	public void sendMail(HttpContext context)
    {
		string msg = "";
		try
		{
			string c_phieuhanngach_id = context.Request.QueryString["c_phieuhanngach_id"];
			c_phieuhanngach phn = db.c_phieuhanngaches.FirstOrDefault(p=>p.c_phieuhanngach_id.Equals(c_phieuhanngach_id));
			c_donhang dh = db.c_donhangs.FirstOrDefault(p => p.c_donhang_id.Equals(phn.c_donhang_id));
			nhanvien nguoinhan = db.nhanviens.FirstOrDefault(p=>p.manv.Equals(dh.nguoitao));
			
			string manv = UserUtils.getUser(context);
			nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
			md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
			
			if(!string.IsNullOrEmpty(nv.smtp))
				smtp = db.md_smtps.Where(s=>s.md_smtp_id == nv.smtp).FirstOrDefault();
			
			string path = context.Server.MapPath("~/FileSendMail/");
			string fileNamePdf = manv + "Phieu-Han-Ngach-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + ".pdf";
			
			string sql = string.Format(@"SELECT 
								phn.sochungtu, phn.sodathang as so_dsdh, dh.sochungtu as so_donhang,
								sp.ma_sanpham, cthn.ma_sanpham_khach, sl_thaydoi, gianhap, dtkd.ma_dtkd, cthn.ghichu,
                                (select hoten from nhanvien where manv = dh.nguoilap) as nguoilappo,
                                (select soluong from c_dongdonhang where c_dongdonhang_id = cthn.c_dongdonhang_id) as sl_po,
                                (select sl_conlai from c_dongdsdh where c_dongdsdh_id = cthn.c_dongdsdh_id) as sl_conlai,
                                (select hoten from nhanvien where manv = phn.nguoiphutrach) as nguoiphutrachpo,
                                (select hoten from nhanvien where manv = phn.nguoitao) as nguoilapphieu,
								phn.ngaylap as ngaylapphieu
							FROM 
								c_phieuhanngach phn, c_donhang dh, c_chitietphieuhanngach cthn
								, md_sanpham sp, md_doitackinhdoanh dtkd
							WHERE
								phn.c_donhang_id = dh.c_donhang_id
								AND phn.c_phieuhanngach_id = cthn.c_phieuhanngach_id
								AND cthn.md_sanpham_id = sp.md_sanpham_id
								AND cthn.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
								AND phn.c_phieuhanngach_id = '{0}' order by sp.ma_sanpham asc", c_phieuhanngach_id);
			
			bool next = true;
			try
			{
				rptHanNgach report = new rptHanNgach();
				SqlDataAdapter da = new SqlDataAdapter(sql, mdbc.GetConnection);
				DataSet ds = new DataSet();
				da.Fill(ds);
				report.DataSource = ds;
				report.DataAdapter = da;
				report.ExportToPdf(path + fileNamePdf);
			}catch(Exception ex)
			{
				next = false;
				msg = "Không tạo được file PDF " + ex.Message;
			}

			if (next)
			{
				try 
				{
					GoogleMail mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value, smtp.use_ssl.GetValueOrDefault(false));
					mail.Send(nv.email, nguoinhan.email, nv.email_cc, "", "Phiếu hạn ngạch " + phn.sochungtu, string.Format("Phiếu hạn ngạch số: {0}<br/>Số danh sách đặt hàng: {1}<br/>Số đơn hàng: {2}", phn.sochungtu, phn.sodathang, dh.sochungtu), path + fileNamePdf);
					msg = String.Format("Đã gửi mail thành công đến:<br/>{0}<br/>{1}<br/>", nv.email_cc, nguoinhan.email);
				}
				catch (Exception ex) 
				{
					msg = String.Format("Gửi mail lỗi:<br/>SMTP: {0}<br/>Port: {1}<br/>SSL: {2}<br/>Error: {3}", smtp.smtpserver, smtp.port.Value, smtp.use_ssl.GetValueOrDefault(false), ex.Message);
				}
			}
		}catch(Exception e)
		{
			msg = e.Message;
		}
		
        context.Response.Write(msg);
    }

    public void ActiveHanNgach(HttpContext context)
    {
        try
        {
            String id = context.Request.QueryString["id"];
            c_phieuhanngach hn = db.c_phieuhanngaches.FirstOrDefault(p => p.c_phieuhanngach_id.Equals(id));
            if (hn != null)
            {
                bool next = true;
                string msg = "";
				decimal sltd_hn = 0;
                if (hn.md_trangthai_id.Equals("SOANTHAO"))
                {
                    var cthns = from c in db.c_chitietphieuhanngaches where c.c_phieuhanngach_id.Equals(hn.c_phieuhanngach_id) select c;
					
                    foreach (var item in cthns)
                    {
                        c_dongdsdh ddsdh = db.c_dongdsdhs.FirstOrDefault(d => d.c_dongdsdh_id.Equals(item.c_dongdsdh_id));
                        c_dongdonhang ddh = db.c_dongdonhangs.FirstOrDefault(d => d.c_dongdonhang_id.Equals(item.c_dongdonhang_id));
						
                        if (item.sl_thaydoi <= ddsdh.sl_conlai)
                        {
                            ddsdh.sl_conlai = ddsdh.sl_conlai - item.sl_thaydoi;
                            ddh.soluong_conlai = ddh.soluong_conlai - item.sl_thaydoi;
							
							decimal cbm1row = ddh.v2.Value / ddh.soluong.Value;
							sltd_hn += Math.Round(cbm1row * item.sl_thaydoi.Value, 3);
                        }
                        else
                        {
                            md_sanpham sp = db.md_sanphams.FirstOrDefault(p => p.md_sanpham_id.Equals(item.md_sanpham_id));
                            next = false;
                            msg = string.Format("Mã {0} có số lượng thay đổi lớn hơn số lượng còn lại của danh sách đặt hàng: {1} > {2}", sp.ma_sanpham, item.sl_thaydoi, ddsdh.sl_conlai);
                            break;
                        }
                    }
					
                    if (next)
                    {
                        hn.md_trangthai_id = "HIEULUC";
                        db.SubmitChanges();
						foreach(c_kehoachxuathang khxh in db.c_kehoachxuathangs.Where(s=>s.c_danhsachdathang_id == hn.sodathang)) {
							//khxh.cbm = khxh.cbm - sltd_hn;
							khxh.cbm_conlai = khxh.cbm_conlai - sltd_hn;
						}
						db.SubmitChanges();
                        msg = "Đã hiệu lực phiếu hạn ngạch " + hn.sochungtu + " thành công!";
                    }
                    context.Response.Write(msg);
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    public void add(HttpContext context)
    {
        string c_danhsachdathang_id = context.Request.Form["c_danhsachdathang_id"].ToString();
        DateTime ngayLap = DateTime.ParseExact(context.Request.Form["ngaylap"].ToString(), "dd/MM/yyyy", null);
        DateTime ngayTrinh = DateTime.ParseExact(context.Request.Form["ngaytrinh"].ToString(), "dd/MM/yyyy", null);
        DateTime ngayDuyet = DateTime.ParseExact(context.Request.Form["ngayduyet"].ToString(), "dd/MM/yyyy", null);
        string lido = context.Request.Form["lido"].ToString();
        bool phieutang = bool.Parse(context.Request.Form["phieutang"]);

        string jsondata = context.Request.Form["grid_form"].ToString();
        IList<JToken> tokenList = JToken.Parse(jsondata).ToList();
        List<dongHanNgach> dsSanPham = new List<dongHanNgach>();

        foreach (JToken token in tokenList)
        {
            dsSanPham.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dongHanNgach>(token.ToString()));
        }

        string sochungtu = string.Format(@"select tiento + '/'+ CONVERT(nvarchar(32), so_duocgan) + '/' + hauto 
                                            from 
	                                            md_sochungtu sct, md_loaichungtu lct
                                            where 
	                                            sct.md_loaichungtu_id = lct.md_loaichungtu_id
	                                            AND lct.kieu_doituong ='{0}'", phieutang ? "THN" : "GHN");

        c_danhsachdathang dsdh = db.c_danhsachdathangs.FirstOrDefault(p => p.c_danhsachdathang_id.Equals(c_danhsachdathang_id));

        c_phieuhanngach hanngach = new c_phieuhanngach();
        hanngach.c_phieuhanngach_id = ImportUtils.getNEWID();
        hanngach.c_danhsachdathang_id = dsdh.c_danhsachdathang_id;
        hanngach.c_donhang_id = dsdh.c_donhang_id;
        hanngach.sochungtu = (string)mdbc.ExecuteScalar(sochungtu);
        hanngach.diachigiaohang = dsdh.diachigiaohang;
        hanngach.huongdanlamhang = dsdh.huongdanlamhang;
        hanngach.hangiaohang_po = dsdh.hangiaohang_po;
        hanngach.phieutang = phieutang;
        hanngach.sodathang = dsdh.sochungtu;
        hanngach.nguoiphutrach = dsdh.nguoi_phutrach;
        hanngach.nguoidathang = dsdh.nguoi_dathang;
        hanngach.ngaylap = ngayLap;
        hanngach.ngaytrinh = ngayTrinh;
        hanngach.ngayduyet = ngayDuyet;
        hanngach.lido = lido;
        hanngach.md_doitackinhdoanh_id = dsdh.md_doitackinhdoanh_id;
        hanngach.md_trangthai_id = "SOANTHAO";


        hanngach.ngaytao = DateTime.Now;
        hanngach.nguoitao = UserUtils.getUser(context);
        hanngach.ngaycapnhat = DateTime.Now;
        hanngach.nguoicapnhat = UserUtils.getUser(context);
        hanngach.hoatdong = true;

        int stt = 10;
        if (dsSanPham.Count > 0)
        {

            System.Collections.Generic.List<c_chitietphieuhanngach> lstChiTietHanNgach = new System.Collections.Generic.List<c_chitietphieuhanngach>();
            foreach (dongHanNgach item in dsSanPham)
            {
                c_dongdsdh ddsdh = db.c_dongdsdhs.FirstOrDefault(d => d.c_dongdsdh_id.Equals(item.c_dongdsdh_id));
                c_chitietphieuhanngach ctphn = new c_chitietphieuhanngach();
                ctphn.c_chitietphieuhanngach_id = ImportUtils.getNEWID();
                ctphn.c_phieuhanngach_id = hanngach.c_phieuhanngach_id;
                ctphn.sothutu = stt;
                ctphn.ghichu = item.ghichu;
                ctphn.gianhap = ddsdh.gianhap;
                ctphn.md_sanpham_id = item.md_sanpham_id;
                ctphn.ma_sanpham_thaydoi = item.md_sanpham_id;
                ctphn.sl_canlam = item.sl_canlam;
                ctphn.sl_thaydoi = item.sl_thaydoi;
                ctphn.ma_sanpham_khach = item.ma_sanpham_khach;
                ctphn.md_doitackinhdoanh_id = ddsdh.md_doitackinhdoanh_id;
                ctphn.c_dongdsdh_id = ddsdh.c_dongdsdh_id;
                ctphn.c_dongdonhang_id = ddsdh.c_dongdonhang_id;

                ctphn.ngaytao = DateTime.Now;
                ctphn.nguoitao = UserUtils.getUser(context);
                ctphn.ngaycapnhat = DateTime.Now;
                ctphn.nguoicapnhat = UserUtils.getUser(context);
                ctphn.hoatdong = true;
                lstChiTietHanNgach.Add(ctphn);
                stt += 10;
            }
            db.c_phieuhanngaches.InsertOnSubmit(hanngach);
            db.c_chitietphieuhanngaches.InsertAllOnSubmit(lstChiTietHanNgach);
            db.SubmitChanges();

            string updateSoChungTu = string.Format(@"update md_sochungtu set so_duocgan = so_duocgan + buocnhay where md_loaichungtu_id = (select md_loaichungtu_id from md_loaichungtu where kieu_doituong = '{0}')", phieutang ? "THN" : "GHN");
            mdbc.ExcuteNonQuery(updateSoChungTu);
            context.Response.Write("Tạo hạn ngạch thành công!");
        }
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

        String sqlCount = "SELECT COUNT(*) AS count FROM c_phieuhanngach phn, c_donhang dh where phn.c_donhang_id = dh.c_donhang_id {0} {1} ";
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
            sidx = "phn.ngaytao";
        }

        string strsql = "select * from( " +
            " select c_phieuhanngach_id, phn.md_trangthai_id, phn.sochungtu, phn.phieutang, phn.ngaylap, phn.ngaytrinh, phn.ngayduyet, phn.lido, phn.sodathang, dh.sochungtu as so_po, " +
            " phn.ngaytao, phn.nguoitao, phn.ngaycapnhat, phn.nguoicapnhat, phn.mota, phn.hoatdong, " +
            " ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_phieuhanngach phn, c_donhang dh where phn.c_donhang_id = dh.c_donhang_id " +
            " {0} {1})P WHERE RowNum > @start AND RowNum < @end";
        
        strsql = String.Format(strsql, isadmin == true ? "" : "AND dh.nguoitao IN(" + strAccount + ")", filter);
        
        System.Data.DataTable dt = mdbc.GetData(strsql, "@start", start, "@end", end);
        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
        xml += "<rows>";
        xml += "<page>" + page + "</page>";
        xml += "<total>" + total_page + "</total>";
        xml += "<records>" + count + "</records>";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            xml += "<row>";
            xml += "<cell><![CDATA[" + row["c_phieuhanngach_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_trangthai_id"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sochungtu"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["phieutang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaylap"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytrinh"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngayduyet"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["lido"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sodathang"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["so_po"] + "]]></cell>";
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

    public void edit(HttpContext context)
    {
        String id = context.Request.Form["id"];
        DateTime ngaylap = DateTime.ParseExact(context.Request.Form["ngaylap"], "dd/MM/yyyy", null);
        DateTime ngaytrinh = DateTime.ParseExact(context.Request.Form["ngaytrinh"], "dd/MM/yyyy", null);
        DateTime ngayduyet = DateTime.ParseExact(context.Request.Form["ngayduyet"], "dd/MM/yyyy", null);
        string lido = context.Request.Form["lido"];

        c_phieuhanngach hn = db.c_phieuhanngaches.FirstOrDefault(p => p.c_phieuhanngach_id.Equals(id));
        if (hn != null)
        {
            if (hn.md_trangthai_id.Equals("SOANTHAO"))
            {
                hn.ngaylap = ngaylap;
                hn.ngayduyet = ngayduyet;
                hn.ngaytrinh = ngaytrinh;
                hn.lido = lido;

                hn.ngaycapnhat = DateTime.Now;
                hn.nguoicapnhat = UserUtils.getUser(context);
                db.SubmitChanges();
            }
            else
            {
                jqGridHelper.Utils.writeResult(0, "Không thể chỉnh sửa khi phiếu hạn ngạch đã hiệu lực.!");
            }
        }
        else {
            jqGridHelper.Utils.writeResult(0, "Không tìm thấy phiếu hạn ngạch, có thể đã được xóa trước đó.!");
        }
    }

    public void del(HttpContext context)
    {
        String id = context.Request.Form["id"];
        c_phieuhanngach phn = db.c_phieuhanngaches.FirstOrDefault(p=>p.c_phieuhanngach_id.Equals(id));
        if (phn.md_trangthai_id.Equals("SOANTHAO"))
        {
            var cthns = from cthn in db.c_chitietphieuhanngaches where cthn.c_phieuhanngach_id.Equals(phn.c_phieuhanngach_id) select cthn;
            db.c_chitietphieuhanngaches.DeleteAllOnSubmit(cthns);
            db.c_phieuhanngaches.DeleteOnSubmit(phn);
            db.SubmitChanges();
        }
        else {
            jqGridHelper.Utils.writeResult(0, "Không xóa phiếu hạn ngạch khi trạng thái là Hiệu lực!");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}