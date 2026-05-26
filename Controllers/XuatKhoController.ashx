<%@ WebHandler Language="C#" Class="XuatKhoController" %>

using System;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

public class XuatKhoController : IHttpHandler
{
    private LinqDBDataContext db = new LinqDBDataContext();

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
            case "activexk":
                this.ActiveXuatKho(context);
                break;
			case "deactivexk":
                this.DeactiveXuatKho(context);
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
	
	public void startSendMail(HttpContext context)
    {
        try
        {
            String xkId = context.Request.QueryString["xkId"];
            String manv = UserUtils.getUser(context);
            nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
            //Lấy báo giá
            c_nhapxuat nx = db.c_nhapxuats.FirstOrDefault(p => p.c_nhapxuat_id.Equals(xkId));


            String str = "<table style='width:100%'> <tr> <td colspan='2'><input id='rdAccounting' checked='checked' value='toAccounting' name='rdTypeMail' type='radio'/> <label for='rdAccounting'>Gửi cho kế toán</label></td> </tr>                    </table>";
            context.Response.Write(String.Format(str, nv.email, "hongphuong@ancopottery.com", nv.email_cc));
            //context.Response.Write(String.Format("<div>Gửi từ: {0}</div><div>Gửi đến: {1}</div><div>Cc: {2}</div>", nv.email, doitac.email, nv.email_cc));
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    public String getNhapKhoSql(String c_nhapxuat_id)
    {
        String selTotal = String.Format(@"select SUM( dnx.dongia * dnx.slthuc_nhapxuat) as tongtien 
                            from c_nhapxuat nx, c_dongnhapxuat dnx 
                            where nx.c_nhapxuat_id = dnx.c_nhapxuat_id
                            AND nx.c_nhapxuat_id = N'{0}'", c_nhapxuat_id == null ? "" : c_nhapxuat_id);
        decimal total = (decimal)mdbc.ExecuteScalar(selTotal);

        String str = String.Format(@"
            SELECT 
	            nx.sophieu, dtkd.ma_dtkd, nx.ngay_giaonhan as ngay, dh.sochungtu as so_po, kho.ten_kho as giao_kho
                , (select sochungtu from c_danhsachdathang dsdh where dsdh.c_danhsachdathang_id = (select c_danhsachdathang_id from c_dongdsdh ddsdh where ddsdh.c_dongdsdh_id = dnx.c_dongdsdh_id)) as chungtu_dathang
                , sp.ma_sanpham, sp.mota_tiengviet, dvt.ma_edi as dvt, nx.phicong, nx.phitru, nx.discount, nx.totaldiscount, nx.grandtotal as totalgrand
	            , dnx.slthuc_nhapxuat as sl, dnx.dongia as dongia, (dnx.slthuc_nhapxuat * dnx.dongia) as thanhtien
                , N'{0} USD' as tien, (select hoten from nhanvien where manv = nx.nguoitao) as hoten
            FROM 
	            c_nhapxuat nx , c_dongnhapxuat dnx, md_doitackinhdoanh dtkd
                , c_donhang dh, md_sanpham sp, md_donvitinhsanpham dvt, md_kho kho
            WHERE
	            nx.c_nhapxuat_id = dnx.c_nhapxuat_id
	            AND nx.c_donhang_id = dh.c_donhang_id
                AND nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	            AND dnx.md_sanpham_id = sp.md_sanpham_id
	            AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
                AND nx.md_kho_id = kho.md_kho_id 
                AND nx.c_nhapxuat_id = N'{1}' order by sp.ma_sanpham asc", MoneyToWord.ConvertMoneyToTextVND(total), c_nhapxuat_id == null ? "" : c_nhapxuat_id);
        return str;
    }

    public String getXuatKhoSql(String c_nhapxuat_id)
    {
        String selTotal = String.Format(@"select SUM( dnx.dongia * dnx.sl_dathang ) as tongtien 
                            from c_nhapxuat nx, c_dongnhapxuat dnx 
                            where nx.c_nhapxuat_id = dnx.c_nhapxuat_id
                            AND nx.c_nhapxuat_id = N'{0}'", c_nhapxuat_id == null ? "" : c_nhapxuat_id);
        decimal total = (decimal)mdbc.ExecuteScalar(selTotal);

        String str = String.Format(@"
            SELECT 
                nx.sophieu, nx.ngay_giaonhan as ngay, dh.sochungtu, nx.container as so_con, nx.soseal as seal, kho.ten_kho as taikho
                , sp.ma_sanpham, sp.mota_tiengviet, dvt.ma_edi as dvt
                , dnx.slthuc_nhapxuat as soluong, dnx.dongia as dongia, (dnx.slthuc_nhapxuat * dnx.dongia) as thanhtien
                , N'{0} USD' as tien, (select hoten from nhanvien where manv = nx.nguoitao) as hoten
            FROM 
                c_nhapxuat nx , c_dongnhapxuat dnx, md_kho kho
                , md_sanpham sp, md_donvitinhsanpham dvt, c_dongdonhang ddh, c_donhang dh
            WHERE
                nx.c_nhapxuat_id = dnx.c_nhapxuat_id
                AND dnx.md_sanpham_id = sp.md_sanpham_id
                AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
                AND nx.md_kho_id = kho.md_kho_id 
                AND dnx.c_dongdonhang_id = ddh.c_dongdonhang_id
                AND ddh.c_donhang_id = dh.c_donhang_id
                AND nx.c_nhapxuat_id = N'{1}' order by dnx.line asc", MoneyToWord.ConvertMoneyToTextVND(total), c_nhapxuat_id == null ? "" : c_nhapxuat_id);
        return str;
    }
    

    public void sendMail(HttpContext context)
    {
        String manv = UserUtils.getUser(context);
        String sendMailType = context.Request.QueryString["sendMailType"];
        List<string> lstAttachFiles = new List<string>();
        String path = context.Server.MapPath("~/FileSendMail/");
        String xuatKhoPdf = manv + "Xuat-Kho-{0}-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + ".pdf";
        String nhapKhoPdf = manv + "Nhap-Kho-{0}-" + DateTime.Now.ToString("ssmmhh-dd-MM-yyyy") + "-{1}-" + ".pdf";
        
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv.Equals(manv));
        md_mailtemplate tmp = db.md_mailtemplates.FirstOrDefault(p => p.use_for.Equals("KETOAN") && p.default_mail.Equals(true));

        // Lấy thông tin smtp
        md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));

        String xkId = context.Request.QueryString["xkId"];
        c_nhapxuat xk = db.c_nhapxuats.FirstOrDefault(p => p.c_nhapxuat_id.Equals(xkId));

        bool next = true;
        String msg = "";

        if (nv == null)
        {
            next = false;
            msg += "Không tìm thấy mã nhân viên!";
        }

        if (tmp == null)
        {
            next = false;
            msg += "Không tìm thấy Mail Template!";
        }

        if (xk == null)
        {
            next = false;
            msg += "Không tìm thấy Phiếu xuất!";
        }

        try
        {
            rptXuatKho report = new rptXuatKho();
            SqlDataAdapter da = new SqlDataAdapter(getXuatKhoSql(xkId), mdbc.GetConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            report.DataSource = ds;
            report.DataAdapter = da;
            report.ExportToPdf(path + string.Format(xuatKhoPdf, xk.sophieu.Replace("/", "-")));
            lstAttachFiles.Add(path + string.Format(xuatKhoPdf, xk.sophieu.Replace("/", "-")));
        }
        catch (Exception e)
        {
            next = false;
            msg += "Không chiết xuất được phiếu xuất kho!" + e.Message;
        }
		
        try
        {
            string getNhapKhoId = "select c_nhapxuat_id, sophieu from c_nhapxuat where c_nhapxuat_id IN ( select c_nhapxuat_id from c_dongnhapxuat dnx where dnx.c_dongnhapxuat_id in( select dongnhapxuat_ref from c_dongnhapxuat where c_nhapxuat_id = '" + xkId + "' ))";
            DataTable dtNhapKho = mdbc.GetData(getNhapKhoId);
            int i = 1;
            foreach (DataRow item in dtNhapKho.Rows)
            {
                rptNhaCungCap report = new rptNhaCungCap();
                SqlDataAdapter da = new SqlDataAdapter(getNhapKhoSql(item[0].ToString()), mdbc.GetConnection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                report.DataSource = ds;
                report.DataAdapter = da;
                report.ExportToPdf(path + string.Format(nhapKhoPdf, item[1].ToString().Replace("/", "-"), i));
                lstAttachFiles.Add(path + string.Format(nhapKhoPdf, item[1].ToString().Replace("/", "-"), i));
            }
        }
        catch(Exception e)
        {
            next = false;
            msg += "Không chiết xuất được phiếu nhập kho!" + e.Message;;
        }

        if (next)
        {
            try
            {
                string sql = "select sophieu from c_nhapxuat where c_nhapxuat_id IN ( select c_nhapxuat_id from c_dongnhapxuat dnx where dnx.c_dongnhapxuat_id in( select dongnhapxuat_ref from c_dongnhapxuat where c_nhapxuat_id = '" + xkId + "' ))";
                System.Data.DataTable dt = mdbc.GetData(sql);

                string nks = "";
                if (dt.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow item in dt.Rows)
                    {
                        nks += ", " + item["sophieu"].ToString();
                    }

                    nks = nks.Substring(1);
                }

                string files = "";
                if (lstAttachFiles.Count > 0)
                {
                    foreach (string item in lstAttachFiles)
                    {
                        files += "; " + item;
                    }
                    files = files.Substring(1);
                }


                tmp.subject_mail = tmp.subject_mail.Replace("[PHIEUXUAT]", xk.sophieu);
                tmp.subject_mail = tmp.subject_mail.Replace("[SOPO]", xk.sophieunx);

                tmp.content_mail = tmp.content_mail.Replace("[PHIEUXUAT]", xk.sophieu);
                tmp.content_mail = tmp.content_mail.Replace("[SOPO]", xk.sophieunx);
                tmp.content_mail = tmp.content_mail.Replace("[PHIEUNHAP]", nks);

                if (sendMailType == "toAccounting")
                {
                    string resp = "";
                    GoogleMail mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value);
                   // mail.Send(nv.email, "hongphuong@ancopottery.com", nv.email_cc, "", tmp.subject_mail, tmp.content_mail.Replace("%0D", "<br/>"), files);
                    mail.Send(nv.email, "binhpn@ancopottery.com", nv.email_cc, "", tmp.subject_mail, tmp.content_mail.Replace("%0D", "<br/>"), files);
					
				//	resp += String.Format("Đã gửi mail thành công đến:<br/>{0}<br/>{1}<br/>", nv.email_cc, "hongphuong@ancopottery.com");
                    resp += String.Format("Đã gửi mail thành công đến:<br/>{0}<br/>{1}<br/>", nv.email_cc, "binhpn@ancopottery.com");
					context.Response.Write(resp);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }else{
			context.Response.Write(msg);
		}
    }

    public void ActiveXuatKho(HttpContext context)
    {
        String id = context.Request.QueryString["xkId"];
        c_nhapxuat m = db.c_nhapxuats.Single(p => p.c_nhapxuat_id.Equals(id));
        if (m.md_trangthai_id.Equals("HIEULUC"))
        {
            context.Response.Write(String.Format("Hiệu trạng thái của phiếu xuất {0} đã là hiệu lực.!", m.sophieu));
        }
        else
        {
            bool next = true;
            if (m.container == null)
            {
                next = false;
                context.Response.Write(String.Format("Phiếu xuất kho {0} chưa có số container.!", m.sophieu));
            }

            if (m.soseal == null)
            {
                next = false;
                context.Response.Write(String.Format("Phiếu xuất kho {0} chưa có số seal.!", m.sophieu));
            }

            String selNhapKho = "select count(*) as count from c_nhapxuat nx, c_dongnhapxuat dnx where nx.c_nhapxuat_id = dnx.c_nhapxuat_id AND nx.cr_phieuxuat = 1 AND dnx.c_dongnhapxuat_id IN(select distinct dongnhapxuat_ref from c_dongnhapxuat where c_nhapxuat_id = @c_nhapxuat_id)";
            int count_cr_phieuxuat = (int)mdbc.ExecuteScalar(selNhapKho, "@c_nhapxuat_id", m.c_nhapxuat_id);
            if (count_cr_phieuxuat > 0)
            {
                next = false;
                context.Response.Write(String.Format("Phiếu xuất kho {0} có chứa phiếu nhập đã tạo phiếu xuất.!", m.sophieu));
            }

            if (next)
            {
				mdbc.ExcuteNonProcedure("dbo.p_hieulucnhapxuat", "@nhapxuat_id", id);
				
                m.md_trangthai_id = "HIEULUC";
                db.SubmitChanges();
                context.Response.Write(String.Format("Hiệu lực phiếu xuất kho {0} thành công.!", m.sophieu));    
            }
        }
    }
	
	public void DeactiveXuatKho(HttpContext context)
    {
        String id = context.Request.QueryString["xkId"];
        c_nhapxuat m = db.c_nhapxuats.Single(p => p.c_nhapxuat_id.Equals(id));
        if (m.md_trangthai_id.Equals("SOANTHAO"))
        {
            context.Response.Write(String.Format("Hiệu trạng thái của phiếu xuất {0} đã là soạn thảo.!", m.sophieu));
        }
		else if(m.cr_invoice == true)
		{
			context.Response.Write(String.Format("Phiếu xuất {0} đã tạo invoice.!", m.sophieu));
		}
        else
        {
            bool next = true;
            if (next)
            {
				mdbc.ExcuteNonProcedure("dbo.p_soanthaonhapxuat", "@nhapxuat_id", id);
				
                m.md_trangthai_id = "SOANTHAO";
                db.SubmitChanges();
                context.Response.Write(String.Format("Thay đổi trạng thái \"Soạn thảo\" phiếu xuất kho {0} thành công.!", m.sophieu));    
            }
        }
    }
    
    public void getSelectOption(HttpContext context)
    {
        String sql = "select c_nhapxuat_id, sophieu from c_nhapxuat where hoatdong = 1 AND md_loaichungtu_id = N'XK'";
        SelectHtmlControl s = new SelectHtmlControl(sql);
        context.Response.Write(s.ToString());
    }
    
    
    public void edit(HttpContext context)
    {
        String h = context.Request.Form["hoatdong"].ToLower();
		string soseal = context.Request.Form["soseal"];
		string container = context.Request.Form["container"];
		string loaicont = context.Request.Form["loaicont"];
		
		c_nhapxuat m = db.c_nhapxuats.Single(p => p.c_nhapxuat_id == context.Request.Form["id"]);
		
		string sqlCount = @"select count(*) from c_nhapxuat where c_nhapxuat_id != '"+ context.Request.Form["id"] +"' and md_loaichungtu_id = 'XK' and (soseal = '" + context.Request.Form["soseal"] + "' or container = '"+ context.Request.Form["container"] +"') and sophieunx = '" + m.sophieunx +"'";
		int count = (int)mdbc.ExecuteScalar(sqlCount);
		
        bool hd = false;
        if (h.Equals("on") || h.Equals("true"))
        { hd = true; }
		if(count > 0 & loaicont != "LE")
		{
			jqGridHelper.Utils.writeResult(0, "Số Seal hoặc số container số đã tồn tại! Chỉ được phép chọn Cont Lẻ");
		}
        else if (m.md_trangthai_id.Equals("SOANTHAO"))
        {
            //m.c_donhang_id = context.Request.Form["c_donhang_id"];
            m.sophieu = context.Request.Form["sophieu"];
            m.sophieunx = context.Request.Form["sophieunx"];
            m.ngay_giaonhan = DateTime.ParseExact(context.Request.Form["ngay_giaonhan"], "dd/MM/yyyy", null);
            //m.md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
            m.nguoigiao = context.Request.Form["nguoigiao"];
            m.nguoinhan = context.Request.Form["nguoinhan"];
            m.sophieukhach = context.Request.Form["sophieukhach"];
            m.ngay_phieu = DateTime.ParseExact(context.Request.Form["ngay_phieu"], "dd/MM/yyyy", null);
            //m.md_kho_id = context.Request.Form["md_kho_id"];
            m.soseal = context.Request.Form["soseal"];
            m.socontainer = 1;
            m.container = context.Request.Form["container"];
            m.loaicont = context.Request.Form["loaicont"];
            //m.md_trangthai_id = context.Request.Form["md_trangthai_id"];
            //m.md_loaichungtu_id = context.Request.Form["md_loaichungtu_id"];


            m.mota = context.Request.Form["mota"];
            m.hoatdong = hd;
            m.ngaycapnhat = DateTime.Now;
            m.nguoicapnhat = UserUtils.getUser(context);

            db.SubmitChanges();
        }else
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
        //    //md_kho_id = context.Request.Form["md_kho_id"],
        //    soseal = context.Request.Form["soseal"],
        //    socontainer = context.Request.Form["socontainer"],
        //    loaicont = context.Request.Form["loaicont"],
        //    //md_trangthai_id = context.Request.Form["md_trangthai_id"],
        //    //md_loaichungtu_id = context.Request.Form["md_loaichungtu_id"],

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
			" Left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
			" Left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE 1 = 1" +
            " {0} " +
            " AND nx.md_loaichungtu_id = N'XK' " + filter;

        sqlCount = String.Format(sqlCount, isadmin == true ? "" : " AND nx.nguoitao IN(" + strAccount + ") ");
        
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
            " select nx.c_nhapxuat_id, nx.md_trangthai_id " +
            " , nx.sophieunx " +
            " , nx.sophieu " +
            " , nx.ngay_giaonhan, dtkd.ma_dtkd, nx.nguoigiao, nx.nguoinhan " +
            " , nx.sophieukhach, nx.ngay_phieu, kho.ten_kho " +
            " , nx.soseal, nx.container, nx.loaicont " +
            " , nx.md_loaichungtu_id, nx.ngaytao, nx.nguoitao, nx.ngaycapnhat, nx.nguoicapnhat " +
            " , nx.mota, nx.hoatdong " +
            " , ROW_NUMBER() OVER (ORDER BY " + sidx + " " + sord + ") as RowNum " +
            " FROM c_nhapxuat nx " +
			" Left join md_doitackinhdoanh dtkd on nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id " +
			" Left join md_kho kho on nx.md_kho_id = kho.md_kho_id " +
            " WHERE 1 = 1" +
            " {0} " +
            " AND nx.md_loaichungtu_id = N'XK' " + filter +
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
            xml += "<cell><![CDATA[" + row["sophieunx"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieu"] + "]]></cell>";
			xml += "<cell><![CDATA[" + row["soseal"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["container"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["loaicont"] + "]]></cell>";
			xml += "<cell><![CDATA[" + DateTime.Parse(row["ngaytao"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoitao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngay_giaonhan"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ma_dtkd"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoigiao"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["nguoinhan"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["sophieukhach"] + "]]></cell>";
            xml += "<cell><![CDATA[" + DateTime.Parse(row["ngay_phieu"].ToString()).ToString("dd/MM/yyyy") + "]]></cell>";
            xml += "<cell><![CDATA[" + row["ten_kho"] + "]]></cell>";
            xml += "<cell><![CDATA[" + row["md_loaichungtu_id"] + "]]></cell>";            
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