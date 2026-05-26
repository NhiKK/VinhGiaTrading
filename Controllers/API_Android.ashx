<%@ WebHandler Language="C#" Class="API_Android" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

public class API_Android : IHttpHandler {
    public JavaScriptSerializer js = new JavaScriptSerializer();
    private LinqDBDataContext db = new LinqDBDataContext();
    public string emailcc = "anco@ancopottery.com,info@ancopottery.com";
    //public string emailcc = "ngocnhan@esc.vn";
    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.Params["oper"];
        switch (oper)
        {
            case "load_dbserver":
                this.load_dbserver(context);
                break;
            case "ESC_login":
                this.ESC_login(context);
                break;
            case "ESC_info":
                this.ESC_info(context);
                break;
            case "ESC_getPrice":
                this.ESC_getPrice(context);
                break;
            case "ESC_getspprice":
                this.ESC_getspprice(context);
                break;
            case "ESC_getCustomer":
                this.ESC_getCustomer(context);
                break;
            case "ESC_getDTKD":
                this.ESC_getDTKD(context);
                break;
            case "ESC_createQO":
                this.ESC_createQO(context);
                break;
            case "ESC_createPO":
                this.ESC_createPO(context);
                break;
            case "ESC_sendMail":
                this.ESC_sendMail(context);
                break;
            case "ESC_sendMailQO":
                this.ESC_sendMailQO(context);
                break;
            case "ESC_ghepbo":
                this.ESC_ghepbo(context);
                break;
            case "ESC_getCookie":
                this.ESC_getCookie(context);
                break;
        }
    }

    public void ESC_getCookie(HttpContext context)
    {
        try
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie("superadmin", false);
            var cookie = System.Web.Security.FormsAuthentication.GetAuthCookie("superadmin", false);
            var json = new Dictionary<string, string>();
            json["Name"] = cookie.Name;
            json["Value"] = cookie.Value;
            json["Domain"] = cookie.Domain;
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(json));
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.ToString());
        }
    }

    public void load_dbserver(HttpContext context)
    {
        string a = Math.Round(47.8, 0).ToString();
        context.Response.Write(a);
    }

    public void ESC_getspprice(HttpContext context) {
        string ma_sanpham = context.Request.Params["ma_sanpham"];
        string json = @"{{ ""ma_sanpham"":""{0}"", ""gia_fob"":""{1}"", ""image"":""{2}"", ""detai"":""{3}"", ""bientau"":""{4}"", ""mausac"":""{5}"", ""cbm"":""{6}""}}";

        string strsql =
                @"select top 25 T.* from(
					select sp.md_sanpham_id, sp.ma_sanpham, gsp.gia, dt.ma_iso, pbg.ngay_hieuluc, sp.l_cm, sp.w_cm, sp.h_cm,
					row_number() over(partition by sp.ma_sanpham order by pbg.ngay_hieuluc desc) as rn
					FROM md_sanpham sp
					left join md_giasanpham gsp on gsp.md_sanpham_id = sp.md_sanpham_id
					left join md_phienbangia pbg on gsp.md_phienbangia_id = pbg.md_phienbangia_id 
					left join md_banggia bg on pbg.md_banggia_id = bg.md_banggia_id 
					left join md_dongtien dt on bg.md_dongtien_id = dt.md_dongtien_id 
					 WHERE 1=1 
					 AND gsp.hoatdong = 1 
					 AND bg.banggiaban = 1 
					 AND substring(bg.ten_banggia,0,5) = 'FOB-' 
					 AND sp.ma_sanpham like N'%{0}%' and sp.trangthai = 'DHD' 
				 )T
				where T.rn = 1 order by T.ma_sanpham";

        strsql = string.Format(strsql, ma_sanpham);
        System.Data.DataTable dt = mdbc.GetData(strsql);
        string detail = "";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            string msp = row["ma_sanpham"].ToString(), image = "", spId = row["md_sanpham_id"].ToString();
            if(!msp.Contains("-F")) {
                image = msp.Substring(0, 8) + ".jpg";
            }
            else {
                image = msp.Substring(0, 11) + ".jpg";
            }
            string detai_json = "", bientau_json = "", mausac_json = "", detai_chk = "", bientau_chk = "", mausac_chk = "", v2 = "";
            string chungloai = msp.Substring(0, 2);

            string sql_detail = string.Format(@"
				select A.detai, A.bientau, A.mausac, dtai.ta_ngan as detai_ta, A.bientau as bientau_ta, msac.ta_ngan as mausac_ta from (
					select distinct 
					substring(sp.ma_sanpham, 0, 7) as todetai, 
					substring(sp.ma_sanpham, 10, 2) as bientau,
					substring(sp.ma_sanpham, 7, 2) as detai,
					substring(sp.ma_sanpham, 13, 2) as mausac
					from md_sanpham sp
					where 1=1
					and sp.hoatdong = 1 
					and sp.trangthai = 'DHD'
				) A
				left join md_detai dtai on dtai.code_cl = '{1}' and dtai.code_dt = A.detai
				left join md_mausac msac on msac.code_cl = '{1}' and msac.code_dt = A.detai and msac.code_mau = A.mausac
				where 
				A.todetai = '{0}'
				order by A.detai, A.bientau, A.mausac
			", msp.Substring(0, 6), chungloai);
            System.Data.DataTable dt_detail = mdbc.GetData(sql_detail);
            foreach (System.Data.DataRow row_dt in dt_detail.Rows)
            {
                if(!detai_chk.Split(',').Contains(row_dt["detai"].ToString())) {
                    detai_chk += row_dt["detai"] + ",";
                    string dtTA = row_dt["detai_ta"].ToString();
                    dtTA = dtTA.Replace(System.Environment.NewLine, " ");
                    dtTA = dtTA.Replace("\n", " ");
                    detai_json += row_dt["detai"] + " : " + dtTA + ",";
                }

                string detai_bientau = row_dt["detai"] + " - " + row_dt["bientau"];
                if(!bientau_chk.Split(',').Contains(detai_bientau)) {
                    bientau_chk += detai_bientau + ",";
                    bientau_json += detai_bientau + ",";
                }

                string detai_bientau_mausac = row_dt["detai"] + " - " + row_dt["bientau"] + " - " + row_dt["mausac"];
                if(!mausac_chk.Split(',').Contains(detai_bientau_mausac)) {
                    mausac_chk += detai_bientau_mausac + ",";
                    string msTA = row_dt["mausac_ta"].ToString();
                    msTA = msTA.Replace(System.Environment.NewLine, " ");
                    msTA = msTA.Replace("\n", " ");
                    mausac_json += detai_bientau_mausac + " : " + msTA + ",";
                }
            }
            if(detai_json != "") {
                detai_json = detai_json.Substring(0, detai_json.Length - 1);
            }
            if(bientau_json != "") {
                bientau_json = bientau_json.Substring(0, bientau_json.Length - 1);
            }
            if(mausac_json != "") {
                mausac_json = mausac_json.Substring(0, mausac_json.Length - 1);
            }

            string strsqlDG =
            @"
			select convert(decimal(18,3), dg.v2 / dg.sl_outer) as v2
			FROM md_donggoisanpham dgsp, md_donggoi dg
			WHERE 
			dgsp.md_sanpham_id = '{0}'
			AND dgsp.md_donggoi_id = dg.md_donggoi_id
			AND dgsp.macdinh = 1";

            strsqlDG = string.Format(strsqlDG, spId);
            System.Data.DataTable dtDG = mdbc.GetData(strsqlDG);
            foreach (System.Data.DataRow rowDG in dtDG.Rows)
            {
                v2 = rowDG["v2"].ToString();
            }
            detail += string.Format(json, msp, "$" + row["gia"].ToString(), image, detai_json, bientau_json, mausac_json, v2) + ",";
        }
        if(detail != "") {
            detail = detail.Substring(0, detail.Length - 1);
        }
        string kq_json = "[" + detail + "]";
        context.Response.Write(kq_json);
    }

    public void ESC_login(HttpContext context)
    {
        string manv = context.Request.Form["username"];
        string password = context.Request.Form["password"];
        string json = string.Format(@"[{{ ""user"":""{0}"", ""key"":""{1}"", ""status"":""{2}""}}]", manv, "", "false");
        if (manv != "" & manv != null)
        {
            string matkhau = Security.EncodeMd5Hash(password);
            string manvmd5 = Security.EncodeMd5Hash(manv);
            string sql = "select * from nhanvien where manv = @manv and matkhau = @matkhau and hoatdong = 1";
            DataTable dt = mdbc.GetData(sql, "@manv", manv, "@matkhau", matkhau);

            if (dt.Rows.Count != 0)
            {
                json = string.Format(@"[{{ ""user"":""{0}"", ""key"":""{1}"", ""status"":""{2}""}}]", manv, manvmd5, "true");
            }
        }
        context.Response.Write(json);
    }

    public void ESC_info(HttpContext context)
    {
        string ma_sanpham = context.Request.Params["ma_sanpham"];
        string arr_sanpham = "'" + ma_sanpham + "'";
        try {
            ma_sanpham = ma_sanpham.Split(new [] { '\r', '\n' }).FirstOrDefault();
            string unknown = "(Unknown)";
            string dong0 = unknown, dong1 = unknown, dong2 = unknown, dong3 = unknown , dong4 = unknown , dong5 = unknown , dong6 = unknown, dong7 = unknown,
            dong8 = "", dong9 = "", dong10 = unknown, dong11 = unknown, dong12 = unknown, dong13 = unknown, dong14 = "", dong15 = unknown, dong16 = unknown, image = "", detai_mausac = "";
            string json = @"[{{ ""dong0"":""{0}"", ""dong1"":""{1}"", ""dong2"":""{2}"", ""dong3"":""{3}"", 
			""dong4"":""{4}"", ""dong5"":""{5}"", ""dong6"":""{6}"", ""dong7"":""{7}"", ""dong8"":""{8}"", ""dong9"":""{9}"", 
			""dong10"":""{10}"", ""dong11"":""{11}"", ""dong12"":""{12}"", ""dong13"":""{13}"", ""dong14"":""{14}"",
			""dong15"":""{15}"", ""dong16"":""{16}"", ""image"":""{17}"", ""detai"":""{18}"", ""bientau"":""{19}"", ""mausac"":""{20}""}}]";


            md_sanpham sp = db.md_sanphams.FirstOrDefault(s=>s.ma_sanpham == ma_sanpham & s.trangthai == "DHD");
            if(sp != null) {
                string cl = sp.ma_sanpham.Substring(0, 2);
                string bc = sp.ma_sanpham.Substring(9, 2);

                md_chungloai plhh = db.md_chungloais.FirstOrDefault(s=>s.code_cl == cl);
                if(plhh != null) {
                    dong0 = plhh.ta_ngan.ToUpper();
                }
                dong6 = sp.ma_sanpham;
                //dong 7, dong 8, dong 9
                if(bc.Substring(0, 1) != "0" & bc != "S1") {
                    //Tim` bo S cua san pham
                    string msp_0009 = sp.ma_sanpham.Substring(0, 9), msp_1103 = sp.ma_sanpham.Substring(11, 3);
                    string ma_bo_cha = db.md_sanphams.Where(s=>s.ma_sanpham.Substring(0, 9) == msp_0009
                    & s.ma_sanpham.Substring(11, 3) == msp_1103 & s.ma_sanpham.Substring(9, 2).Contains("S")).Select(s=>s.ma_sanpham.Substring(9, 2)).FirstOrDefault();
                    //Lay cac ma cai thuoc bo cua san pham
                    string md_bo_id = db.md_bos.Where(s=>s.ma_bo == bc & s.ma_bo_cha == ma_bo_cha).Select(s=>s.md_bo_id).FirstOrDefault();
                    foreach(md_bo_chitiet bct in db.md_bo_chitiets.Where(s=>s.md_bo_id == md_bo_id)) {
                        string sanpham_add = msp_0009 + bct.md_bo_detail + msp_1103;
                        arr_sanpham += ",'"+ sanpham_add +"'";
                    }
                }
                string strsql =
                @"select T.* from(
					select sp.ma_sanpham, gsp.gia, dt.ma_iso, pbg.ngay_hieuluc, sp.l_cm, sp.w_cm, sp.h_cm,
					row_number() over(partition by sp.ma_sanpham order by pbg.ngay_hieuluc desc) as rn
					FROM md_sanpham sp, md_banggia bg, md_phienbangia pbg,
					 md_giasanpham gsp, md_dongtien dt 
					 WHERE gsp.md_phienbangia_id = pbg.md_phienbangia_id 
					 AND pbg.md_banggia_id = bg.md_banggia_id 
					 AND bg.md_dongtien_id = dt.md_dongtien_id 
					 AND gsp.hoatdong = 1 
					 AND bg.banggiaban = 1 
					 AND substring(bg.ten_banggia,0,5) = 'FOB-'
					 AND sp.ma_sanpham in ({0}) 
					 AND gsp.md_sanpham_id = sp.md_sanpham_id
				 )T
				where T.rn = 1 order by T.ma_sanpham";

                strsql = string.Format(strsql, arr_sanpham);
                System.Data.DataTable dt = mdbc.GetData(strsql);

                string kichthuoc_9 = "";
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    if(row["ma_sanpham"].ToString() != ma_sanpham) {
                        //dong8 += "$" + row["gia"].ToString() + " / ";
                        if(row["l_cm"].ToString() != "0.0" & row["w_cm"].ToString() != "0.0" & row["h_cm"].ToString() != "0.0") {
                            dong9 += row["l_cm"] + "x" + row["w_cm"] + "xH" + row["h_cm"] + "/ \\n          ";
                        }
                    }
                    else {
                        dong7 = "$" + row["gia"].ToString();
                        if(row["l_cm"].ToString() != "0.0" & row["w_cm"].ToString() != "0.0" & row["h_cm"].ToString() != "0.0") {
                            kichthuoc_9 = row["l_cm"] + "x" + row["w_cm"] + "xH" + row["h_cm"];
                        }
                    }
                }
                if(dong8 != ""){
                    dong8 = "( " + dong8.Substring(0, dong8.Length - 1) + ")";
                }
                if(dong9 != ""){
                    dong9 = dong9.Substring(0, dong9.Length - 14) + "cm";
                }
                else {
                    if(kichthuoc_9 != "") {
                        dong9 = kichthuoc_9 + "cm";
                    }
                    else {
                        dong9 = "";
                    }
                }
                //dong 10, dong 11, dong 12, dong 13, dong 14
                strsql =
                @"
				select dg.sl_inner, (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner) as dvtinner,
				dg.sl_outer, (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer) as dvtouter,
				dg.l1, dg.w1, dg.h1,
				dg.l2_mix, dg.w2_mix, dg.h2_mix, convert(decimal(18,3), dg.v2 / dg.sl_outer) as v2, dg.sl_cont_mix, dg.soluonggoi_ctn_20, dg.soluonggoi_ctn, dg.soluonggoi_ctn_40hc
				FROM md_donggoisanpham dgsp, md_donggoi dg
				WHERE 
				dgsp.md_sanpham_id = '{0}'
				AND dgsp.md_donggoi_id = dg.md_donggoi_id
				AND dgsp.macdinh = 1";

                strsql = string.Format(strsql, sp.md_sanpham_id);
                dt = mdbc.GetData(strsql);
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string sl_inner = row["sl_inner"].ToString(), dvt_inner = row["dvtinner"].ToString();
                    string sl_outer = row["sl_outer"].ToString(), dvt_outer = row["dvtouter"].ToString();
                    if(sl_inner != "0" & dvt_inner != "0" & sl_inner != "" & dvt_inner != "") {
                        dong10 = sl_inner + " " + dvt_inner;
                    }
                    else {
                        dong10 = " ";
                    }

                    if(sl_outer != "0" & dvt_outer != "0" & sl_outer != "" & dvt_outer != "") {
                        dong11 = sl_outer + " " + dvt_outer;
                    }
                    else {
                        dong11 = " ";
                    }

                    dong12 = "L" + row["l2_mix"] + "xW" + row["w2_mix"] + "xH" + row["h2_mix"] + "cm";
                    dong13 = row["v2"].ToString();

                    // if(row["sl_cont_mix"].ToString() != "") {
                    // dong14 = " " + row["sl_cont_mix"] + "/01'";
                    // }
                    string slc_20 = row["soluonggoi_ctn_20"].ToString();
                    string slc_40 = row["soluonggoi_ctn"].ToString();
                    string slc_40HC = row["soluonggoi_ctn_40hc"].ToString();
                    // if(slc_20 != "" & slc_20 != "0") {
                    // dong14 += "\\n" + slc_20 + " pallets/20'";
                    // }
                    if(slc_40 != "" & slc_40 != "0") {
                        dong14 += "\\n" + slc_40 + " pallets/40'";
                    }
                    // if(slc_40HC != "" & slc_40HC != "0") {
                    // dong14 += "\\n" + slc_40HC + " pallets/40'HC";
                    // }
                    if(dong14 != "") {
                        dong14 = dong14.Substring(2);
                    }

                    /*if(dong9 == "") {
                        if(row["l1"].ToString() != "0.0" & row["w1"].ToString() != "0.0" & row["h1"].ToString() != "0.0") {
                            dong9 = row["l1"] + "x" + row["w1"] + "xH" + row["h1"] + "cm";
                        }
                    }*/
                }
                //dong 15
                strsql =
                @"
				select cb.ten_cangbien
				FROM md_cangxuathang cxh, md_cangbien cb
				WHERE 
				cxh.md_sanpham_id = '{0}'
				AND cxh.md_cangbien_id = cb.md_cangbien_id
				AND cxh.macdinh = 1";

                strsql = string.Format(strsql, sp.md_sanpham_id);
                dt = mdbc.GetData(strsql);
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    dong15 = row["ten_cangbien"] + "";
                }

                string msp = sp.ma_sanpham;
                if(!msp.Contains("-F")) {
                    image = msp.Substring(0, 8) + ".jpg";
                }
                else {
                    image = msp.Substring(0, 11) + ".jpg";
                }

                string detai_json = "", bientau_json = "", mausac_json = "", detai_chk = "", bientau_chk = "", mausac_chk = "";
                string chungloai = msp.Substring(0, 2);

                string sql_detail = string.Format(@"
					select A.detai, A.bientau, A.mausac, dtai.ta_ngan as detai_ta, A.bientau as bientau_ta, msac.ta_ngan as mausac_ta from (
						select distinct 
						substring(sp.ma_sanpham, 0, 7) as todetai, 
						substring(sp.ma_sanpham, 10, 2) as bientau,
						substring(sp.ma_sanpham, 7, 2) as detai,
						substring(sp.ma_sanpham, 13, 2) as mausac
						from md_sanpham sp
						where 1=1
						and sp.hoatdong = 1 
						and sp.trangthai = 'DHD'
					) A
					left join md_detai dtai on dtai.code_cl = '{1}' and dtai.code_dt = A.detai
					left join md_mausac msac on msac.code_cl = '{1}' and msac.code_dt = A.detai and msac.code_mau = A.mausac
					where 
					A.todetai = '{0}'
					order by A.detai, A.bientau, A.mausac
				", msp.Substring(0, 6), chungloai);
                System.Data.DataTable dt_detail = mdbc.GetData(sql_detail);
                foreach (System.Data.DataRow row_dt in dt_detail.Rows)
                {
                    if(!detai_chk.Split(',').Contains(row_dt["detai"].ToString())) {
                        detai_chk += row_dt["detai"] + ",";
                        string dtTA = row_dt["detai_ta"].ToString();
                        dtTA = dtTA.Replace(System.Environment.NewLine, " ");
                        dtTA = dtTA.Replace("\n", " ");
                        detai_json += row_dt["detai"] + " : " + dtTA + ",";
                    }

                    string detai_bientau = row_dt["detai"] + " - " + row_dt["bientau"];
                    if(!bientau_chk.Split(',').Contains(detai_bientau)) {
                        bientau_chk += detai_bientau + ",";
                        bientau_json += detai_bientau + ",";
                    }

                    string detai_bientau_mausac = row_dt["detai"] + " - " + row_dt["bientau"] + " - " + row_dt["mausac"];
                    if(!mausac_chk.Split(',').Contains(detai_bientau_mausac)) {
                        mausac_chk += detai_bientau_mausac + ",";
                        string msTA = row_dt["mausac_ta"].ToString();
                        msTA = msTA.Replace(System.Environment.NewLine, " ");
                        msTA = msTA.Replace("\n", " ");
                        mausac_json += detai_bientau_mausac + " : " + msTA + ",";
                    }
                }

                if(detai_json != "") {
                    detai_json = detai_json.Substring(0, detai_json.Length - 1);
                }
                if(bientau_json != "") {
                    bientau_json = bientau_json.Substring(0, bientau_json.Length - 1);
                }
                if(mausac_json != "") {
                    mausac_json = mausac_json.Substring(0, mausac_json.Length - 1);
                }

                string kq_json = string.Format(json, dong0, dong1, dong2, dong3, dong4, dong5, dong6, dong7, dong8, dong9, dong10, dong11, dong12, dong13, dong14,
                dong15, dong16, image, detai_json, bientau_json, mausac_json);
                context.Response.Write(kq_json);
            }
        }
        catch {

        }
    }

    public void ESC_getPrice(HttpContext context) {
        string ma_sanpham = context.Request.Form["ma_sanpham"];
        string gia = "123";
        string strsql =
                @"select T.* from(
					select gsp.gia,
					row_number() over(partition by sp.ma_sanpham order by pbg.ngay_hieuluc desc) as rn
					FROM md_sanpham sp, md_banggia bg, md_phienbangia pbg,
					 md_giasanpham gsp, md_dongtien dt 
					 WHERE gsp.md_phienbangia_id = pbg.md_phienbangia_id 
					 AND pbg.md_banggia_id = bg.md_banggia_id 
					 AND bg.md_dongtien_id = dt.md_dongtien_id 
					 AND gsp.hoatdong = 1 AND bg.banggiaban = 1 
					 AND sp.ma_sanpham = N'{0}' 
					 AND substring(bg.ten_banggia,0,5) = 'FOB-'
					 AND gsp.md_sanpham_id = sp.md_sanpham_id
				 )T
				where T.rn = 1";

        strsql = string.Format(strsql, ma_sanpham);
        System.Data.DataTable dt = mdbc.GetData(strsql);

        foreach (System.Data.DataRow row in dt.Rows)
        {
            gia = "$" + row["gia"].ToString();
            break;
        }

        context.Response.Write(gia);
    }

    public void ESC_getCustomer(HttpContext context)
    {
        string ma_dtkd = context.Request.Form["ma_dtkd"];
        string ten_dtkd = context.Request.Form["ten_dtkd"];
        string gia = "";
        string strsql =
                @"select top 1 dtkd.ma_dtkd, dtkd.ten_dtkd, dtkd.email, dtkd.tel, dtkd.diachi
				FROM md_doitackinhdoanh dtkd
				WHERE 1=1 {0}";
        string whereex = "";
        if (ma_dtkd != "" & ma_dtkd != null)
        {
            whereex += "AND UPPER(dtkd.ma_dtkd) = UPPER(N'" + ma_dtkd + "')";
        }
        if (ten_dtkd != "" & ten_dtkd != null)
        {
            whereex += "AND UPPER(dtkd.ten_dtkd) = UPPER(N'" + ten_dtkd + "')";
        }
        strsql = string.Format(strsql, whereex);
        System.Data.DataTable dt = mdbc.GetData(strsql);

        string json = "[";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            json += "{\"ma_dtkd\":\"" + row["ma_dtkd"] + "\", \"ten_dtkd\":\"" + row["ten_dtkd"] + "\",";
            json += "\"phone\":\"" + row["tel"] + "\", \"email\":\"" + row["email"] + "\", \"address\":\"" + row["diachi"] + "\"}";
            break;
        }
        json += "]";
        context.Response.Write(json);
    }

    public void ESC_getDTKD(HttpContext context)
    {
        var dtkds = from a in db.md_doitackinhdoanhs.Where(s => s.hoatdong == true)
                    join b in db.md_loaidtkds on a.md_loaidtkd_id equals b.md_loaidtkd_id
                    where b.ma_loaidtkd == "KH"
                    orderby a.ma_dtkd
                    select new { a.ma_dtkd, a.ten_dtkd, a.tel, a.email, a.diachi };
        string json = "{\"count\":7,\"next\":null,\"previous\":null,\"results\":[";
        string json_detail = "";
        foreach (var dtkd in dtkds)
        {
            json_detail += "{\"ma_dtkd\":\"" + dtkd.ma_dtkd + "\",";
            json_detail += "\"title\":\"" + dtkd.ten_dtkd + "\",";
            json_detail += "\"phone\":\"" + dtkd.tel + "\",";
            json_detail += "\"email\":\"" + dtkd.email + "\",";
            json_detail += "\"address\":\"" + dtkd.diachi + "\"},";
        }
        if (json_detail != "")
        {
            json_detail = json_detail.Substring(0, json_detail.Length - 1);
        }
        json += json_detail + "]}";
        context.Response.Write(json);
    }

    public void ESC_sendMail(HttpContext context)
    {
        string username = context.Request.Form["username"];
        string ma_dtkd = context.Request.Form["ma_dtkd"];
        string ten_dtkd = context.Request.Form["ten_dtkd"];
        string email = context.Request.Form["email"];
        string phone = context.Request.Form["phone"];
        string address = context.Request.Form["address"];
        string nguoilienhe = context.Request.Form["nguoilienhe"];
        string note = context.Request.Form["note"];
        string json_data = context.Request.Form["json_data"];
        var d = js.Deserialize<List<Json_product>>(json_data);

        md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv == username);
        string msg = "";
        if (nv == null)
        {
            msg = "Not found user \"" + username + "\" in system.";
        }
        else if (ten_dtkd == "" | ten_dtkd == null)
        {
            msg = "Customer Name is not empty.";
        }
        else if (email == "" | email == null)
        {
            msg = "Email is not empty.";
        }
        else if (d.Count() <= 0)
        {
            msg = "Have no products.";
        }
        else
        {
            try
            {
                string html_span = "<span style=\"font-size: 11.0pt; font-family: 'Calibri','sans-serif'\">{0}</span>";
                GoogleMail mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value, smtp.use_ssl.Value);
                string subject = "Request a quote for customer \"" + ten_dtkd + "\"";
                string body = "";
                body += string.Format(html_span, "Dear Anco's Sales Team,");
                body += "<br><br>";

                body += string.Format(html_span, "We are " + ten_dtkd + ". We are interested in your following items:");
                body += "<br><br>";

                foreach (var row in d)
                {
                    body += "<b>" + string.Format(html_span, row.ma_sanpham) + "</b>";
                    body += "<br>";
                }

                body += "<br>";
                body += string.Format(html_span, "Please send us a quotation for the above-mentioned items through our following contact:");
                body += "<br>";
                body += "<br>";

                body += string.Format(html_span, "- Our company's name: " + ten_dtkd);
                body += "<br>";
                body += string.Format(html_span, "- Our address: " + address);
                body += "<br>";
                body += string.Format(html_span, "- Our email's address: " + email);
                body += "<br>";
                body += string.Format(html_span, "- Person contact: " + nguoilienhe);
                body += "<br>";
                body += string.Format(html_span, "- Customer's brief introduction: " + note);
                body += "<br>";
                body += "<br>";

                body += string.Format(html_span, "We look forward to receiving your quotation.");
                body += "<br>";
                body += "<br>";

                body += string.Format(html_span, "Kind Regards,");
                body += "<br>";
                body += string.Format(html_span, nguoilienhe);

                mail.Send(nv.email, email.Trim(), emailcc, subject, body, "");
                msg = "ok#";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        context.Response.Write(msg);
    }

    public void ESC_sendMailQO(HttpContext context)
    {
        string username = context.Request.Form["username"];
        string ma_dtkd = context.Request.Form["ma_dtkd"];
        string ten_dtkd = context.Request.Form["ten_dtkd"];
        string email = context.Request.Form["email"];
        string phone = context.Request.Form["phone"];
        string address = context.Request.Form["address"];
        string nguoilienhe = context.Request.Form["nguoilienhe"];
        string note = context.Request.Form["note"];
        string json_data = context.Request.Form["json_data"];
        var d = js.Deserialize<List<Json_product>>(json_data);

        md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv == username);
        string msg = "";
        if (nv == null)
        {
            msg = "Not found user \"" + username + "\" in system.";
        }
        else if (ten_dtkd == "" | ten_dtkd == null)
        {
            msg = "Customer Name is not empty.";
        }
        else if (email == "" | email == null)
        {
            msg = "Email is not empty.";
        }
        else if (d.Count() <= 0)
        {
            msg = "Have no products.";
        }
        else
        {
            try
            {
                string html_span = "<span style=\"font-size: 11.0pt; font-family: 'Calibri','sans-serif'\">{0}</span>";
                GoogleMail mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value, smtp.use_ssl.Value);
                string subject = "[Anco] Product information (" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ")";
                string body = "";
                body += string.Format(html_span, "Dear " + (nguoilienhe.Trim() != "" ? nguoilienhe : ten_dtkd));
                body += "<br><br>";

                body += string.Format(html_span, "Thank you for your time to visit us.");
                body += "<br><br>";

                body += string.Format(html_span, "Following your visit, we would like to send a quick summary of your selected items as follows:");
                body += "<br><br>";

                body += "<table>";
                body += "<tr><th>Item</th><th style='width:10px'></th><th>Price</th></tr>";

                foreach (var row in d)
                {
                    body += "<tr><td><b>" + string.Format(html_span, row.ma_sanpham) + "</b></td>";
                    body += "<td></td>";
                    body += "<td>" + string.Format(html_span, row.gia_fob) + "</td></tr>";
                }
                body += "</table>";
                body += "<br><br>";

                body += string.Format(html_span, "The detailed photo quotation will be sent to you in couple days.");
                body += "<br><br>";

                body += string.Format(html_span, "Thank you for your kind attention.");
                body += "<br><br>";

                body += string.Format(html_span, "Kind Regards,");
                body += "<br>";
                body += string.Format(html_span, "Anco Company Limited");


                mail.Send(nv.email, email.Trim(), emailcc + "," + nv.email, subject, body, "");
                msg = "ok#";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        context.Response.Write(msg);
    }

    public void ESC_createQO(HttpContext context)
    {
        string username = context.Request.Form["username"];
        string ma_dtkd = context.Request.Form["ma_dtkd"];
        string ten_dtkd = context.Request.Form["ten_dtkd"];
        string email = context.Request.Form["email"];
        string phone = context.Request.Form["phone"];
        string address = context.Request.Form["address"];
        string nguoilienhe = context.Request.Form["nguoilienhe"];
        string json_data = context.Request.Form["json_data"];
        var d = js.Deserialize<List<Json_product>>(json_data);

        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv == username);
        string msg = "";
        if (nv == null)
        {
            msg = "Not found user \"" + username + "\" in system.";
        }
        else
        {
            md_doitackinhdoanh dtkd = createDTKD(context, ma_dtkd, ten_dtkd, email, phone, address, nguoilienhe);

            try
            {
                bool hd = false, gia_db = false;
                int check = 0; // validate form on server
                int qo_mau = 1;

                String sochungtu = jqGridHelper.Utils.taoChungTuQO(dtkd.md_doitackinhdoanh_id, qo_mau, DateTime.Now.Year.ToString());


                string md_trongluong_id = "", md_cangbien_id = "", md_dongtien_id = "", md_kichthuoc_id = "";
                md_trongluong trl = db.md_trongluongs.FirstOrDefault(s => s.ten_trongluong == "kg");
                md_dongtien dtien = db.md_dongtiens.FirstOrDefault(s => s.ma_iso == "USD");
                md_cangbien cb = db.md_cangbiens.FirstOrDefault(s => s.ma_cangbien == "HCMP");
                md_kichthuoc kt = db.md_kichthuocs.FirstOrDefault(s => s.ten_kichthuoc == "cm");
                if (trl != null)
                {
                    md_trongluong_id = trl.md_trongluong_id;
                    md_dongtien_id = dtien.md_dongtien_id;
                    md_kichthuoc_id = kt.md_kichthuoc_id;
                    md_cangbien_id = cb.md_cangbien_id;
                }

                c_baogia mnu = new c_baogia
                {
                    c_baogia_id = Security.EncodeMd5Hash(DateTime.Now.ToString("yyyyMMddddhhmmss")),
                    sobaogia = sochungtu,

                    md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id,
                    shipmenttime = 75,
                    ngaybaogia = DateTime.Now,
                    ngayhethan = DateTime.Now.AddMonths(6),
                    md_trongluong_id = md_trongluong_id,
                    md_cangbien_id = md_cangbien_id,
                    md_dongtien_id = md_dongtien_id,
                    md_kichthuoc_id = md_kichthuoc_id,
                    totalcbm = 0,
                    totalcbf = 0,
                    totalquo = 0,
                    isform = true,
                    moq_item_color = "",
                    md_trangthai_id = "SOANTHAO",

                    nguoitao = username,
                    nguoicapnhat = username,
                    mota = context.Request.Form["mota"],
                    hoatdong = hd,
                    gia_db = gia_db,
                    ngaytao = DateTime.Now,
                    ngaycapnhat = DateTime.Now
                };

                if (json_data == null | json_data == "" | json_data == "[]")
                {
                    check = 1;
                    msg = "Error: Have no product.";
                }
                else if (ma_dtkd.Trim() == "")
                {
                    check = 1;
                    msg = "Error: Customer Code is not empty.";
                }
                else if (ten_dtkd.Trim() == "")
                {
                    check = 1;
                    msg = "Error: Customer Name is not empty.";
                }

                if (check == 0)
                {
                    if (1 == 1)
                    {
                        c_baogiaqr mnuqr = new c_baogiaqr();
                        mnu.CopyPropertiesTo(mnuqr);
                        mnuqr.c_baogiaqr_id = mnu.c_baogia_id;
                        mnuqr.sobaogia = "***/" + sochungtu.Substring(4);
                        db.c_baogiaqrs.InsertOnSubmit(mnuqr);
                        db.SubmitChanges();

                        String getOrder = "select max(sothutu) as count from c_chitietbaogiaqr where c_baogiaqr_id = @c_baogia_id";
                        object omax = mdbc.ExecuteScalar(getOrder, "@c_baogia_id", mnuqr.c_baogiaqr_id);
                        int maxOrder = (omax.ToString().Equals("")) ? 0 : (int)omax;
                        foreach (var row in d)
                        {
                            decimal gia_fob = 0;
                            try { gia_fob = decimal.Parse(row.gia_fob.Replace("$", "").Replace(" ", "")); } catch { }
                            add_ctbg(context, row.ma_sanpham, maxOrder, gia_fob, mnuqr.c_baogiaqr_id, username, dtkd.md_doitackinhdoanh_id, db, "qr", row.soluong);
                        }
                        db.SubmitChanges();
                    }
                    //else
                    //{
                    //    db.c_baogias.InsertOnSubmit(mnu);
                    //    db.SubmitChanges();

                    //    String getOrder = "select max(sothutu) as count from c_chitietbaogia where c_baogia_id = @c_baogia_id";
                    //    object omax = mdbc.ExecuteScalar(getOrder, "@c_baogia_id", mnu.c_baogia_id);
                    //    int maxOrder = (omax.ToString().Equals("")) ? 0 : (int)omax;
                    //    foreach (var row in d)
                    //    {
                    //        decimal gia_fob = 0;
                    //        try { gia_fob = decimal.Parse(row.gia_fob.Replace("$", "").Replace(" ", "")); } catch { }

                    //        add_ctbg(context, row.ma_sanpham, maxOrder, gia_fob, mnu.c_baogia_id, username, dtkd.md_doitackinhdoanh_id, db, "", row.soluong);
                    //    }
                    //    db.SubmitChanges();
                    //}

                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        // Lấy thông tin smtp
                        md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));

                        String path = context.Server.MapPath("~/FileSendMail/");
                        String fileNameXLS = nv.manv + "-Quotation-" + DateTime.Now.ToString("mmhh-dd-MM-yyyy") + ".xls";
                        String logoPath = context.Server.MapPath("~/images/VINHGIA_logo_print.png");
                        String productImagePath = context.Server.MapPath("~/images/products/fullsize/");
                        BaoGiaMauReport_QRCode rptQO;
                        //if (nv.manv == "ancoqr")
                        rptQO = new BaoGiaMauReport_QRCode(mnu.c_baogia_id, logoPath, productImagePath, "qr");
                        //else
                        //    rptQO = new BaoGiaMauReport_QRCode(mnu.c_baogia_id, logoPath, productImagePath);

                        NPOI.HSSF.UserModel.HSSFWorkbook wb = rptQO.CreateWBQuotation();
                        wb.Write(ms);
                        File.WriteAllBytes(path + fileNameXLS, ms.ToArray());
                        ms.Close();
                        md_mailtemplate tmp = db.md_mailtemplates.FirstOrDefault(p => p.use_for.Equals("QRCode") && p.default_mail.Equals(true));
                        GoogleMail mail = new GoogleMail(nv.email, nv.email_pass, smtp.smtpserver, smtp.port.Value, smtp.use_ssl.Value);
                        email = email.Trim();
                        if (email == "")
                        {
                            email = nv.email;
                        }
                        else
                        {
                            emailcc = emailcc + "," + nv.email;
                        }

                        if (nguoilienhe.Trim() != "")
                        {
                            ten_dtkd = nguoilienhe;
                        }
                        mail.Send(nv.email, email, emailcc, "", tmp.subject_mail,
                        tmp.content_mail.Replace("%0D", "<br/>").Replace("[ten_dtkd]", ten_dtkd), path + fileNameXLS);
                        msg = "ok#";
                    }
                    catch (Exception ex)
                    {
                        msg = "ok#";
                        msg += "\nSend mail error." + ex.Message;
                    }

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        context.Response.Write(msg);
    }

    public void ESC_ghepbo(HttpContext context) {

        string type = context.Request.Form["type"];
        string c_baogia_id = context.Request.Form["c_baogia_id"];
        string msg_detail = "";
        string get_kdsp = @"
		select A.*, 
		(select top 1 substring(ma_sanpham, 10, 2) from md_sanpham where substring(ma_sanpham, 0, 9) = A.v1 and substring(ma_sanpham, 10, 1) = 'S') as main	
		from(
			select substring(sp.ma_sanpham, 0, 9) as v1, 
			substring(sp.ma_sanpham, 13, 2) as v2
			from c_chitietbaogia ctbg
			left join c_baogia bg on ctbg.c_baogia_id = bg.c_baogia_id
			left join md_sanpham sp on sp.md_sanpham_id = ctbg.md_sanpham_id
			where bg.c_baogia_id = '"+ c_baogia_id + @"'
		) A
		group by A.v1, A.v2 
		";

        string msg = "";
        c_baogia bg = db.c_baogias.FirstOrDefault(s=>s.c_baogia_id == c_baogia_id);
        if(bg == null) {
            msg = "false#Không tìm thấy báo giá đã chọn.";
        }
        else {
            if(bg.ghepbo == true) {
                msg = "false#Báo giá này đã từng thực hiện ghép bộ.";
            }
            else if(bg.md_trangthai_id == "HIEULUC") {
                msg = "false#Báo giá này đã hiệu lực.";
            }
            else {
                try {
                    System.Data.DataTable dt = mdbc.GetData(get_kdsp);
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        get_kdsp = @"
							select A.* from (
								select ctbg.c_chitietbaogia_id, sp.ma_sanpham, substring(sp.ma_sanpham, 10, 2) as detai
								from c_chitietbaogia ctbg 
								left join c_baogia bg on ctbg.c_baogia_id = bg.c_baogia_id 
								left join md_sanpham sp on sp.md_sanpham_id = ctbg.md_sanpham_id 
								where bg.c_baogia_id = '"+ c_baogia_id + @"' 
								and substring(sp.ma_sanpham, 0, 9) = '" + row["v1"] + @"'  
								and substring(sp.ma_sanpham, 13, 2) = '" + row["v2"] + @"' 
								and substring(sp.ma_sanpham, 10, 1) like '%[0-9]%'
							)A
							order by A.detai asc
						";
                        System.Data.DataTable dt_tail = mdbc.GetData(get_kdsp);
                        string detai_arr = "", id_ctbg_del = "";
                        foreach (System.Data.DataRow row2 in dt_tail.Rows)
                        {
                            string detai = row2["detai"].ToString();
                            detai_arr += detai + ",";
                            id_ctbg_del += row2["c_chitietbaogia_id"].ToString() + ",";
                        }

                        bool gb_tc = false;
                        foreach(md_bo bo in db.md_bos.Where(s=>s.ma_bo_cha == row["main"].ToString()).OrderBy(s=>s.ma_bo)) {
                            var arr_bc = db.md_bo_chitiets.Where(s=>s.md_bo_id == bo.md_bo_id).OrderBy(s=>s.md_bo_detail);
                            string detail_ss = "";
                            foreach(var a in arr_bc) {
                                detail_ss += a.md_bo_detail + ",";
                            }

                            if(detai_arr == detail_ss) {
                                string new_sanpham = row["v1"] + "-" + bo.ma_bo + "-" + row["v2"];
                                md_sanpham pro = db.md_sanphams.FirstOrDefault(s=>s.ma_sanpham == new_sanpham);
                                if(pro != null) {
                                    string tk = UserUtils.getUser(context);
                                    c_chitietbaogia ctbg_chk = db.c_chitietbaogias.FirstOrDefault(s=>s.md_sanpham_id == pro.md_sanpham_id & s.c_baogia_id == c_baogia_id);
                                    if(ctbg_chk == null) {
                                        add_ctbg(context, new_sanpham, 0, -1, c_baogia_id, tk, bg.md_doitackinhdoanh_id, db);
                                    }

                                    if(type == "1") {
                                        string[] arr_ctbg_del = id_ctbg_del.Split(',');
                                        foreach(c_chitietbaogia ctbg_del in db.c_chitietbaogias.Where(s=> arr_ctbg_del.Contains(s.c_chitietbaogia_id))) {
                                            db.c_chitietbaogias.DeleteOnSubmit(ctbg_del);
                                        }
                                    }
                                    msg_detail += "<div style='color:blue'>Ghép bộ thành công: \"" + new_sanpham  + "\"</div>";
                                }
                                else {
                                    msg_detail += "<div style='color:red'>Ghép bộ thất bại: không tìm thấy \"" + new_sanpham  + "\"</div>";
                                }
                                gb_tc = true;
                                break;
                            }
                        }

                        if(gb_tc == false) {
                            msg_detail += "<div style='color:red'>Ghép bộ thất bại: [" + row["v1"] + "] màu [" + row["v2"] + "]</div>";
                        }
                    }
                    db.SubmitChanges();


                    if(bg != null) {
                        bg.ghepbo = true;
                        db.SubmitChanges();
                    }
                    msg = "true#" + msg_detail;
                }
                catch(Exception ex) {
                    msg = "false#" + ex.Message;
                }
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";
        context.Response.Write(msg);
    }

    public void add_ctbg(HttpContext context, string ma_sanpham, int maxOrder, decimal gia_fob, string c_baogia_id,
    string tk, string md_doitackinhdoanh_id, LinqDBDataContext db, params object[] args)
    {
        md_sanpham pro = db.md_sanphams.FirstOrDefault(s => s.ma_sanpham == ma_sanpham);
        if (pro != null)
        {
            if (gia_fob < 0)
            {
                var itemJS = UserUtils.get_giasanpham(context, c_baogia_id, pro.md_sanpham_id, db);
                try { gia_fob = decimal.Parse(itemJS["gia"].ToString()); } catch { gia_fob = 0; }
            }
            String dvt_inner = "", dvt_outer = "", ghichu_vachngan = "", trangthai = "BT", md_donggoi_id = "";

            System.Nullable<decimal> sl_inner = 0, soluonggoi_ctn = 0, sl_outer = 0, l1 = 0, w1 = 0, h1 = 0, l2_mix = 0, w2_mix = 0, h2_mix = 0, v2 = 0, vd = 0, vn = 0, vl = 0;
            md_donggoisanpham dgsp = db.md_donggoisanphams.FirstOrDefault(s => s.md_sanpham_id == pro.md_sanpham_id & s.macdinh == true);
            if (dgsp != null)
            {
                md_donggoi dg_sel = db.md_donggois.FirstOrDefault(s => s.md_donggoi_id == dgsp.md_donggoi_id);
                if (dg_sel != null)
                {
                    dvt_inner = dg_sel.dvt_inner;
                    dvt_outer = dg_sel.dvt_outer;
                    ghichu_vachngan = dg_sel.ghichu_vachngan;

                    sl_inner = dg_sel.sl_inner;
                    soluonggoi_ctn = dg_sel.soluonggoi_ctn;
                    sl_outer = dg_sel.sl_outer;
                    l1 = dg_sel.l1;
                    w1 = dg_sel.w1;
                    h1 = dg_sel.h1;
                    l2_mix = dg_sel.l2_mix;
                    w2_mix = dg_sel.w2_mix;
                    h2_mix = dg_sel.h2_mix;
                    v2 = dg_sel.v2;
                    vd = dg_sel.vd;
                    vn = dg_sel.vn;
                    vl = dg_sel.vl;
                    md_donggoi_id = dg_sel.md_donggoi_id;
                }
            }

            trangthai = UserUtils.get_DQ_DG_ALL(context, dgsp.md_donggoi_id, md_doitackinhdoanh_id, pro.md_sanpham_id, db)[0];
            string object0 = "";
            try { object0 = args[0].ToString(); } catch { }

            string object1 = "1";
            try { object1 = args[1].ToString(); } catch { }

            c_chitietbaogia ctbg = new c_chitietbaogia
            {
                c_chitietbaogia_id = ImportUtils.getNEWID(),
                c_baogia_id = c_baogia_id,
                trangthai = trangthai,
                md_sanpham_id = pro.md_sanpham_id,
                ma_sanpham_khach = "",
                md_cangbien_id = pro.md_cangbien_id,
                mota_tienganh = pro.mota_tienganh,
                mota_tiengviet = pro.mota_tiengviet,
                ghichu_vachngan = "",
                sothutu = maxOrder + 10,
                giafob = gia_fob,
                soluong = int.Parse(object1),
                md_donggoi_id = md_donggoi_id,
                sl_inner = sl_inner,
                l1 = l1,
                w1 = w1,
                h1 = h1,
                sl_outer = sl_outer,
                l2 = l2_mix,
                w2 = w2_mix,
                h2 = h2_mix,
                v2 = v2,
                vd = vd,
                vn = vn,
                vl = vl,
                sl_cont = soluonggoi_ctn,
                ghichu = "",
                docquyen = "",
                mota = "",
                hoatdong = true,
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now,
                nguoitao = tk,
                nguoicapnhat = tk
            };

            if (object0 == "qr")
            {
                c_chitietbaogiaqr ctbgqr = new c_chitietbaogiaqr();
                ctbg.CopyPropertiesTo(ctbgqr);
                ctbgqr.c_chitietbaogiaqr_id = ctbg.c_chitietbaogia_id;
                ctbgqr.c_baogiaqr_id = c_baogia_id;
                db.c_chitietbaogiaqrs.InsertOnSubmit(ctbgqr);
            }
            else
            {
                db.c_chitietbaogias.InsertOnSubmit(ctbg);
            }
        }
    }

    public void ESC_createPO(HttpContext context)
    {
        string username = context.Request.Form["username"];
        string ma_dtkd = context.Request.Form["ma_dtkd"];
        string ten_dtkd = context.Request.Form["ten_dtkd"];
        string email = context.Request.Form["email"];
        string phone = context.Request.Form["phone"];
        string address = context.Request.Form["address"];
        string nguoilienhe = context.Request.Form["nguoilienhe"];
        string json_data = context.Request.Form["json_data"];
        var d = js.Deserialize<List<Json_product>>(json_data);
        int check = 0; // validate form on server
        nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv == username);
        string msg = "";
        if (nv == null)
        {
            msg = "Not found user \"" + username + "\" in system.";
        }
        else if (ma_dtkd.Trim() == "")
        {
            check = 1;
            msg = "Error: Customer Code is not empty.";
        }
        else if (ten_dtkd.Trim() == "")
        {
            check = 1;
            msg = "Error: Customer Name is not empty.";
        }
        else if (json_data == null | json_data == "" | json_data == "[]")
        {
            check = 1;
            msg = "Error: Have no product.";
        }
        else
        {
            try
            {
                md_doitackinhdoanh dtkd = createDTKD(context, ma_dtkd, ten_dtkd, email, phone, address, nguoilienhe);
                string sochungtu = jqGridHelper.Utils.taoChungTuPO(dtkd.md_doitackinhdoanh_id, 1, DateTime.Now.Year.ToString());

                string md_trongluong_id = "", md_cangbien_id = "", md_dongtien_id = "", md_kichthuoc_id = "", md_nganhang_id = "", md_paymentterm_id = "";
                md_trongluong trl = db.md_trongluongs.FirstOrDefault(s => s.ten_trongluong == "kg");
                md_dongtien dtien = db.md_dongtiens.FirstOrDefault(s => s.ma_iso == "USD");
                md_cangbien cb = db.md_cangbiens.FirstOrDefault(s => s.ma_cangbien == "HCMP");
                md_kichthuoc kt = db.md_kichthuocs.FirstOrDefault(s => s.ten_kichthuoc == "cm");
                md_nganhang nh = db.md_nganhangs.FirstOrDefault(s => s.ma_nganhang == "VIETCOMBANK");
                md_paymentterm pmt = db.md_paymentterms.FirstOrDefault(s => s.ten_paymentterm == "CAD AT 60 DAYS");
                if (trl != null)
                {
                    md_trongluong_id = trl.md_trongluong_id;
                    md_dongtien_id = dtien.md_dongtien_id;
                    md_kichthuoc_id = kt.md_kichthuoc_id;
                    md_cangbien_id = cb.md_cangbien_id;
                }

                if (nh != null)
                {
                    md_nganhang_id = nh.md_nganhang_id;
                }

                if (pmt != null)
                {
                    md_paymentterm_id = pmt.md_paymentterm_id;
                }
                string c_donhang_id = ImportUtils.getNEWID();
                c_donhangqr mnu = new c_donhangqr();
                mnu.c_donhangqr_id = c_donhang_id;
                mnu.md_trangthai_id = "SOANTHAO";
                mnu.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
                mnu.c_baogia_id = "";
                mnu.ngaylap = DateTime.Now;
                mnu.nguoilap = UserUtils.getUser(context);
                mnu.md_cangbien_id = md_cangbien_id;
                mnu.discount = 0;
                mnu.shipmentdate = DateTime.Now.AddMonths(6);
                mnu.shipmenttime = DateTime.Now.AddMonths(6);
                mnu.md_paymentterm_id = md_paymentterm_id;
                mnu.md_nganhang_id = md_nganhang_id;
                mnu.md_trongluong_id = md_trongluong_id;
                mnu.md_dongtien_id = md_dongtien_id;
                mnu.md_kichthuoc_id = md_kichthuoc_id;
                mnu.cont20 = 0;
                mnu.cont40 = 0;
                mnu.cont40hc = 0;
                mnu.cont45 = 0;
                mnu.contle = 0;
                mnu.payer = "";
                mnu.portdischarge = "";
                mnu.donhang_mau = true;
                mnu.isdonhangtmp = false;
                mnu.dagui_mail = false;
                mnu.hoahong = 0;
                mnu.md_nguoilienhe_id = "";
                mnu.ismakepi = false;
                mnu.customer_order_no = "";
                mnu.gia_db = false;
                mnu.chkPBGCu = false;
                mnu.phienbangiacu = "";
                mnu.ghichu = "";
                mnu.ngaydieuchinh = DateTime.Now;
                mnu.md_banggia_id = "";
                mnu.sochungtu = "***/" + sochungtu.Substring(4);;

                mnu.mota = "";
                mnu.hoatdong = true;
                mnu.ngaytao = DateTime.Now;
                mnu.ngaycapnhat = DateTime.Now;
                mnu.nguoitao = UserUtils.getUser(context);
                mnu.nguoicapnhat = UserUtils.getUser(context);

                db.c_donhangqrs.InsertOnSubmit(mnu);
                db.SubmitChanges();

                String getOrder = "select max(sothutu) as count from c_dongdonhangqr where c_donhangqr_id = @c_donhang_id";
                object omax = mdbc.ExecuteScalar(getOrder, "@c_donhang_id", c_donhang_id);
                int maxOrder = (omax.ToString().Equals("")) ? 0 : (int)omax;
                foreach (var row in d)
                {
                    decimal gia_fob = 0;
                    try { gia_fob = decimal.Parse(row.gia_fob.Replace("$", "").Replace(" ", "")); } catch { }
                    add_ctdonhang(context, row.ma_sanpham, row.soluong.ToString(), maxOrder, gia_fob, mnu.c_donhangqr_id, username, dtkd.md_doitackinhdoanh_id, db, "qr");
                }
                db.SubmitChanges();
                msg = "ok#";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        context.Response.Write(msg);
    }

    public void add_ctdonhang(HttpContext context, string ma_sanpham, string soluong, int maxOrder, decimal gia_fob, string c_donhang_id,
    string tk, string md_doitackinhdoanh_id, LinqDBDataContext db, params object[] args)
    {
        md_sanpham pro = db.md_sanphams.FirstOrDefault(s => s.ma_sanpham == ma_sanpham);
        if (pro != null)
        {
            md_nhacungungmacdinh ncumd = db.md_nhacungungmacdinhs.FirstOrDefault(s => s.md_sanpham_id == pro.md_sanpham_id & s.macdinh == true);
            if (gia_fob < 0)
            {
                var itemJS = UserUtils.get_giasanpham(context, c_donhang_id, pro.md_sanpham_id, db);
                try { gia_fob = decimal.Parse(itemJS["gia"].ToString()); } catch { gia_fob = 0; }
            }
            String dvt_inner = "", dvt_outer = "", ghichu_vachngan = "", trangthai = "BT", md_donggoi_id = "";
            bool? doigia_donggoi = false;
            System.Nullable<decimal> sl_inner = 0, soluonggoi_ctn = 0, sl_outer = 0, l1 = 0, w1 = 0, h1 = 0, l2_mix = 0, w2_mix = 0, h2_mix = 0, v2 = 0, vd = 0, vn = 0, vl = 0;
            md_donggoisanpham dgsp = db.md_donggoisanphams.FirstOrDefault(s => s.md_sanpham_id == pro.md_sanpham_id & s.macdinh == true);
            if (dgsp != null)
            {
                md_donggoi dg_sel = db.md_donggois.FirstOrDefault(s => s.md_donggoi_id == dgsp.md_donggoi_id);
                if (dg_sel != null)
                {
                    dvt_inner = dg_sel.dvt_inner;
                    dvt_outer = dg_sel.dvt_outer;
                    ghichu_vachngan = dg_sel.ghichu_vachngan;

                    sl_inner = dg_sel.sl_inner;
                    soluonggoi_ctn = dg_sel.soluonggoi_ctn;
                    sl_outer = dg_sel.sl_outer;
                    l1 = dg_sel.l1;
                    w1 = dg_sel.w1;
                    h1 = dg_sel.h1;
                    l2_mix = dg_sel.l2_mix;
                    w2_mix = dg_sel.w2_mix;
                    h2_mix = dg_sel.h2_mix;
                    v2 = dg_sel.v2;
                    vd = dg_sel.vd;
                    vn = dg_sel.vn;
                    vl = dg_sel.vl;
                    md_donggoi_id = dg_sel.md_donggoi_id;
                    doigia_donggoi = dg_sel.doigia_donggoi;
                }
            }

            trangthai = UserUtils.get_DQ_DG_ALL(context, dgsp.md_donggoi_id, md_doitackinhdoanh_id, pro.md_sanpham_id, db)[0];
            c_dongdonhangqr mnu = new c_dongdonhangqr();
            mnu.c_dongdonhangqr_id = Guid.NewGuid().ToString().Replace("-", "");
            mnu.c_donhangqr_id = c_donhang_id;
            mnu.md_sanpham_id = pro.md_sanpham_id;
            mnu.ma_sanpham_khach = "";
            mnu.sothutu = maxOrder + 10;
            mnu.giafob = gia_fob;
            mnu.soluong = decimal.Parse(soluong);
            mnu.soluong_conlai = decimal.Parse(soluong);
            mnu.soluong_daxuat = 0;
            mnu.soluong_dathang = 0;

            mnu.mota_tiengviet = pro.mota_tiengviet;
            mnu.mota_tienganh = pro.mota_tienganh;
            // mnu.nhacungungid = ncu_line;
            mnu.nhacungungid = ncumd.md_doitackinhdoanh_id;

            // Thành phần đóng gói
            mnu.md_donggoi_id = md_donggoi_id;
            mnu.sl_inner = sl_inner;
            mnu.l1 = l1;
            mnu.w1 = w1;
            mnu.h1 = h1;
            mnu.sl_outer = sl_outer;
            mnu.l2 = l2_mix;
            mnu.w2 = w2_mix;
            mnu.h2 = h2_mix;
            mnu.v2 = v2;
            mnu.vd = vd;
            mnu.vn = vn;
            mnu.vl = vl;
            mnu.ghichu_vachngan = ghichu_vachngan;
            mnu.sl_cont = soluonggoi_ctn;
            mnu.ghichu = "";

            //kiem tra hang hoa doc quyen
            int count_dq = (from hdq in db.md_hanghoadocquyens
                            where !hdq.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(pro.md_sanpham_id)
                            select new { hdq.md_hanghoadocquyen_id }).Count();

            int count_dqdt = (from hdq in db.md_hanghoadocquyens
                              where hdq.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id) && hdq.md_sanpham_id.Equals(pro.md_sanpham_id)
                              select new { hdq.md_hanghoadocquyen_id }).Count();

            if (count_dq > 0 && count_dqdt == 0 && doigia_donggoi == true)
            {
                mnu.trangthai = "DGDGDQ";
            }
            else if (count_dq > 0 && count_dqdt == 0)
            {
                mnu.trangthai = "DQ";
            }
            else if (doigia_donggoi == true)
            {
                mnu.trangthai = "DGDG";
            }
            else
            {
                mnu.trangthai = "BT";
            }

            // Thông tin chung
            mnu.mota = "";
            mnu.hoatdong = true;
            mnu.nguoitao = UserUtils.getUser(context);
            mnu.nguoicapnhat = UserUtils.getUser(context);
            mnu.ngaytao = DateTime.Now;
            mnu.ngaycapnhat = DateTime.Now;
            mnu.docquyen = "";
            db.c_dongdonhangqrs.InsertOnSubmit(mnu);
        }
    }

    public md_doitackinhdoanh createDTKD(HttpContext context, string ma_dtkd, string ten_dtkd, string email, string phone, string address, string nguoilienhe)
    {
        md_doitackinhdoanh dtkd = db.md_doitackinhdoanhs.FirstOrDefault(s => s.ma_dtkd == ma_dtkd);
        string md_loaidtkd_id = "";
        md_loaidtkd ldt = db.md_loaidtkds.FirstOrDefault(s => s.ma_loaidtkd == "KH");
        if (ldt != null) { md_loaidtkd_id = ldt.md_loaidtkd_id; }
        if (dtkd == null)
        {
            dtkd = new md_doitackinhdoanh
            {
                md_doitackinhdoanh_id = ImportUtils.getNEWID(),
                ma_dtkd = ma_dtkd,
                ten_dtkd = ten_dtkd,
                email = email,
                tel = phone,
                diachi = address,
                daidien = nguoilienhe,
                md_loaidtkd_id = md_loaidtkd_id,
                isncc = false,
                hoatdong = true,
                ngaytao = DateTime.Now,
                ngaycapnhat = DateTime.Now
            };
            db.md_doitackinhdoanhs.InsertOnSubmit(dtkd);
            db.SubmitChanges();
        }

        return dtkd;
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}

public class Json_product
{
    public string ma_sanpham;
    public string gia_fob;
    public string image;
    public string soluong;
    public string detai_mausac;
}

/// <summary>
/// Summary description for BaoGiaMauReport
/// </summary>
public class BaoGiaMauReport_QRCode
{
    private String c_baogia_id, logoPath, productImagePath, type;

    public String LogoPath
    {
        get { return logoPath; }
        set { logoPath = value; }
    }

    public String ProductImagePath
    {
        get { return productImagePath; }
        set { productImagePath = value; }
    }

    private LinqDBDataContext db = new LinqDBDataContext();
    public HSSFWorkbook CreateWBQuotation()
    {
        string table = "c_baogia", tableDT = "c_chitietbaogia";
        if (type == "qr")
        {
            table = "c_baogiaqr";
            tableDT = "c_chitietbaogiaqr";
        }

        string orderby = UserUtils.get_sapxep_qodetail(null, c_baogia_id, "B.ma_sanpham", "order by B.ma_sanpham asc", db);

        String selectDetails = String.Format(@"
				SELECT *, CASE WHEN  B.sl_inner = '0 0' and B.master like N'%ctn%'
						THEN round(B.n_w_outer + ((B.l2 + B.w2) * (B.w2 + B.h2))/5400,0)

						WHEN B.sl_inner like N'%ctn%' and B.master like N'%ctn%'
						THEN round((B.g_w_inner * (B.sl_outer/ B.inner_)) +  (((B.l2 + B.w2) * (B.w2 + B.h2))/5400),0)

						WHEN B.sl_inner like N'%ctn%' and B.master like N'%pal%'
						THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 20,0)

						WHEN B.sl_inner like N'%ctn%' and B.master like N'%crt%'
						THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 5,0)

						WHEN B.sl_inner like N'%bun%' and B.master like N'%crt%'
						THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 5,0)

						WHEN B.sl_inner like N'%bun%' and B.master like N'%pal%'
						THEN round(B.g_w_inner * (B.sl_outer/ B.inner_) + 20,0)

						WHEN B.sl_inner = '0 0' and B.master like N'%pal%'
						THEN round(B.n_w_outer + 25,0)

						WHEN B.sl_inner = '0 0' and B.master like N'%bun%'
						THEN round(B.n_w_outer + 0.5,0)
						
						WHEN B.sl_inner = '0 0' and B.master like N'%crt%'
						THEN round(B.n_w_outer + 5,0)

						ELSE 0 END as g_w_outer
				FROM(
				SELECT *,CASE WHEN  A.sl_inner like N'%ctn%'  
						THEN A.n_w_inner + ((A.l1+A.w1)*(A.w1+A.h1)/5400)
						WHEN A.sl_inner like N'%bun%'  
						THEN A.n_w_inner + 0.5
						WHEN A.sl_inner like N'%crt%'  
						THEN A.n_w_inner + 3
						ELSE 0 END as g_w_inner
				FROM(
				select 
	            dtkd.ten_dtkd, dtkd.diachi, dtkd.tel, dtkd.fax, bg.ngaybaogia
	            , sp.ma_sanpham as msp, (case when  SUBSTRING(sp.ma_sanpham,10,1) != 'F' then SUBSTRING(sp.ma_sanpham,0,9) else SUBSTRING(sp.ma_sanpham,0,12) end) as pic, sp.ma_sanpham, ctbg.ma_sanpham_khach
	            , (CASE 
						WHEN kt.ten_kichthuoc = 'inch' then dbo.f_taomotainchQoPo(sp.md_sanpham_id) 
						ELSE ctbg.mota_tienganh
					END) as mota_tienganh
				,  (select dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id)) as trongluong
                ,  tl.ten_trongluong, dg.sl_inner as inner_, dg.sl_outer
                , (CAST(dg.sl_inner as nvarchar) + ' ' + (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_inner)) as sl_inner
	            , (CAST(dg.sl_outer as nvarchar) + ' ' + (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dg.dvt_outer)) as master
                , dg.ten_donggoi, ctbg.l1, ctbg.w1, ctbg.h1, ctbg.l2, ctbg.w2, ctbg.h2, ctbg.v2, round(ctbg.v2 * 35.31466672, 2)  as cbf
	            , dg.soluonggoi_ctn as nopack, dg.soluonggoi_ctn_20 as nopack_20, dg.soluonggoi_ctn_40hc as nopack_40hc, ctbg.giafob, ctbg.soluong
	            , sp.l_cm, sp.w_cm, sp.h_cm, sp.l_inch, sp.w_inch, sp.h_inch
	            , bg.shipmenttime, bg.shipmenttime + 15 as shipmenttime2, cb.ten_cangbien
	            , bg.ngayhethan, kt.ten_kichthuoc, bg.moq_item_color, hscode.hscode
				, dg.sl_inner * (SELECT dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id))  as n_w_inner
				, dg.sl_outer * (SELECT dbo.f_convertKgToPounds(sp.trongluong, bg.md_trongluong_id)) as n_w_outer
            from 
	            {2} bg
				left join {3} ctbg ON bg.{2}_id = ctbg.{2}_id
	            left join md_doitackinhdoanh dtkd ON bg.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
				left join md_sanpham sp ON ctbg.md_sanpham_id = sp.md_sanpham_id
				left join md_hscode hscode ON sp.md_hscode_id = hscode.md_hscode_id
	            left join md_donggoi dg ON ctbg.md_donggoi_id = dg.md_donggoi_id
	            left join md_cangbien cb ON sp.md_cangbien_id = cb.md_cangbien_id
                left join md_kichthuoc kt ON bg.md_kichthuoc_id = kt.md_kichthuoc_id
                left join md_trongluong tl ON bg.md_trongluong_id = tl.md_trongluong_id
            where bg.{2}_id = N'{0}')A)B {1}", (c_baogia_id == null ? "" : c_baogia_id), orderby, table, tableDT);


        String selectRefereces = String.Format(@"SELECT 
	                                                  (select SUBSTRING(filter, 1, 2) + '-' + ta_ngan as ta_ngan from md_chungloai cl where cl.code_cl = SUBSTRING(filter, 1, 2)) as ta_ngan, SUBSTRING(filter, 1, 2) as chungloai, SUBSTRING(filter, 4, 2) as codemau, mau , url
                                                   FROM 
	                                                   {1} bg
														inner join md_color_reference color on bg.{1}_id = color.c_baogia_id
                                                   WHERE
                                                       color.hoatdong = 1
                                                       AND bg.{1}_id = N'{0}'", (c_baogia_id == null ? "" : c_baogia_id), table);

        String selectDistinct = String.Format(@"SELECT distinct chungloai
                                                    FROM(
                                                        SELECT 
	                                                        (select ta_ngan from md_chungloai cl where cl.code_cl = SUBSTRING(filter, 1, 2)) as ta_ngan, SUBSTRING(filter, 1, 2) as chungloai, SUBSTRING(filter, 4, 2) as codemau, mau , url
                                                        FROM 
	                                                        {1} bg
															inner join md_color_reference color on bg.{1}_id = color.c_baogia_id
                                                        WHERE
															color.hoatdong = 1
                                                            AND bg.{1}_id = N'{0}'
                                                    ) as tmp", (c_baogia_id == null ? "" : c_baogia_id), table);

        DataTable dtDetails = mdbc.GetData(selectDetails);
        DataTable dtDistinct = mdbc.GetData(selectDistinct);
        DataTable dtReferences = mdbc.GetData(selectRefereces);
        if (dtDetails.Rows.Count != 0)
        {
            HSSFWorkbook hssfworkbook = this.CreateHSSFWorkbook(dtDetails, dtDistinct, dtReferences);
            return hssfworkbook;
        }
        else
        {
            throw new Exception("Báo giá không có dữ liệu!");
        }





    }

    private HSSFWorkbook CreateHSSFWorkbook(DataTable dtDetails, DataTable dtDistinct, DataTable dtReferences)
    {
        string tenkt = dtDetails.Rows[0]["ten_kichthuoc"].ToString().ToLower();
        string tentl = dtDetails.Rows[0]["ten_trongluong"].ToString().ToLower();
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet s1 = hssfworkbook.CreateSheet("Sheet1");
        System.Drawing.Image logo = System.Drawing.Image.FromFile(logoPath);
        MemoryStream mslogo = new MemoryStream();
        logo.Save(mslogo, System.Drawing.Imaging.ImageFormat.Jpeg);

        IDrawing patriarchlogo = s1.CreateDrawingPatriarch();
        HSSFClientAnchor anchorlogo = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 1, 3);
        anchorlogo.AnchorType = AnchorType.DontMoveAndResize;

        int indexlogo = hssfworkbook.AddPicture(mslogo.ToArray(), PictureType.JPEG);
        IPicture signaturePicturelogo = patriarchlogo.CreatePicture(anchorlogo, indexlogo);

        // set A1
        s1.CreateRow(0).CreateCell(0).SetCellValue("VINH GIA COMPANY LIMITED");

        // set A2
        s1.CreateRow(1).CreateCell(0).SetCellValue("Northern Chu Lai Industrial Zone, Nui Thanh Commune, Danang City, Viet Nam");

        // set A3
        s1.CreateRow(2).CreateCell(0).SetCellValue("Tel: (84-235) 3567393   Fax: (84-235) 3567494");

        // set A4
        s1.CreateRow(3).CreateCell(0).SetCellValue("QUOTATION");

        IFont fontBold = hssfworkbook.CreateFont();
        fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        ICellStyle cellBold = hssfworkbook.CreateCellStyle();
        cellBold.SetFont(fontBold);

        ICellStyle cellBoldCenter = hssfworkbook.CreateCellStyle();
        cellBoldCenter.Alignment = HorizontalAlignment.Center;
        cellBoldCenter.VerticalAlignment = VerticalAlignment.Top;
        cellBoldCenter.SetFont(fontBold);

        // Style of Company Name
        ICellStyle cellStyleCompany = hssfworkbook.CreateCellStyle();
        cellStyleCompany.Alignment = HorizontalAlignment.Center;
        cellStyleCompany.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 15));

        // Style of Company Information 
        ICellStyle cellStyleInfomation = hssfworkbook.CreateCellStyle();
        cellStyleInfomation.Alignment = HorizontalAlignment.Center;
        cellStyleInfomation.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 12));

        // Style of Title
        ICellStyle cellStyleTitle = hssfworkbook.CreateCellStyle();
        cellStyleTitle.Alignment = HorizontalAlignment.Center;
        cellStyleTitle.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 18));

        // Style of Company Information Underline
        ICellStyle cellStyleInfomationUnderline = hssfworkbook.CreateCellStyle();
        cellStyleInfomationUnderline.Alignment = HorizontalAlignment.Center;
        cellStyleInfomationUnderline.SetFont(NPOIUtils.CreateFontSize(hssfworkbook, 12, true));



        s1.GetRow(0).GetCell(0).CellStyle = cellStyleCompany;
        s1.GetRow(1).GetCell(0).CellStyle = cellStyleInfomation;
        s1.GetRow(2).GetCell(0).CellStyle = cellStyleInfomationUnderline;
        s1.GetRow(3).GetCell(0).CellStyle = cellStyleTitle;

        s1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 9));
        s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 9));
        s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 9));
        s1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 9));

        s1.CreateRow(4).CreateCell(0).SetCellValue("CUSTOMER NAME:");
        s1.GetRow(4).GetCell(0).CellStyle = cellBold;

        s1.GetRow(4).CreateCell(1).SetCellValue(dtDetails.Rows[0]["ten_dtkd"].ToString());
        s1.GetRow(4).GetCell(1).CellStyle = cellBold;


        s1.CreateRow(5).CreateCell(0).SetCellValue("ADDRESS:");
        s1.GetRow(5).GetCell(0).CellStyle = cellBold;

        s1.GetRow(5).CreateCell(1).SetCellValue(dtDetails.Rows[0]["diachi"].ToString() + " Tel:" + dtDetails.Rows[0]["tel"].ToString() + " Fax:" + dtDetails.Rows[0]["fax"].ToString());
        s1.GetRow(5).GetCell(1).CellStyle = cellBold;

        s1.GetRow(4).CreateCell(8).SetCellValue("Date:");



        s1.GetRow(4).GetCell(8).CellStyle = cellBold;

        s1.GetRow(4).CreateCell(9).SetCellValue(DateTime.Parse(dtDetails.Rows[0]["ngaybaogia"].ToString()).ToString("dd/MMM/yyyy"));
        s1.GetRow(4).GetCell(9).CellStyle = cellBold;

        IRow headerColumn = s1.CreateRow(6);
        IRow headerColumn2 = s1.CreateRow(7);

        headerColumn.HeightInPoints = 40;
        headerColumn2.HeightInPoints = 40;
        ICellStyle hStyle = hssfworkbook.CreateCellStyle();
        hStyle.WrapText = true;
        hStyle.SetFont(fontBold);
        hStyle.Alignment = HorizontalAlignment.Center;
        hStyle.VerticalAlignment = VerticalAlignment.Top;
        hStyle.BorderBottom = hStyle.BorderLeft
            = hStyle.BorderRight
            = hStyle.BorderTop
            = NPOI.SS.UserModel.BorderStyle.Thin;

        for (int i = 0; i <= 9; i++)
        {
            headerColumn.CreateCell(i).CellStyle = hStyle;
            headerColumn2.CreateCell(i).CellStyle = hStyle;
        }

        headerColumn.GetCell(0).SetCellValue("Item No");
        s1.AddMergedRegion(new CellRangeAddress(6, 7, 0, 0));

        headerColumn.GetCell(1).SetCellValue("Picture - Only for shape ref.");
        s1.AddMergedRegion(new CellRangeAddress(6, 7, 1, 1));

        headerColumn.GetCell(2).SetCellValue("Description");
        s1.AddMergedRegion(new CellRangeAddress(6, 7, 2, 2));

        headerColumn.GetCell(3).SetCellValue("Packing");
        s1.AddMergedRegion(new CellRangeAddress(6, 6, 3, 4));

        headerColumn2.GetCell(3).SetCellValue("Inner");
        headerColumn2.GetCell(4).SetCellValue("Master");

        headerColumn.GetCell(5).SetCellValue(String.Format("PACK {0}", tenkt.Equals("cm") ? "CBM" : "CBF"));
        s1.AddMergedRegion(new CellRangeAddress(6, 7, 5, 5));

        headerColumn.GetCell(6).SetCellValue("No. of master packages per 40'' container");
        s1.AddMergedRegion(new CellRangeAddress(6, 7, 6, 6));

        headerColumn.GetCell(7).SetCellValue("FOB PRICE (USD)");
        s1.AddMergedRegion(new CellRangeAddress(6, 7, 7, 7));

        headerColumn.GetCell(8).SetCellValue("Port of loading");
        s1.AddMergedRegion(new CellRangeAddress(6, 7, 8, 8));

        headerColumn.GetCell(9).SetCellValue("Quantity");
        s1.AddMergedRegion(new CellRangeAddress(6, 7, 9, 9));

        HSSFSheet hssfsheet = (HSSFSheet)s1;

        Excel_Format ex_fm = new Excel_Format(hssfworkbook);

        ICellStyle cellStyleCenter = ex_fm.getcell(10, false, true, "LRTB", "C", "T");
        ICellStyle cellStyleLeft = ex_fm.getcell(10, false, true, "LRTB", "L", "T");
        ICellStyle cellStyleNumber2 = ex_fm.cell_decimal_2(10, false, true, "LRTB", "C", "T");
        ICellStyle cellStyleNumber3 = ex_fm.cell_decimal_3(10, false, true, "LRTB", "C", "T");

        ICellStyle border = hssfworkbook.CreateCellStyle();
        border.BorderBottom = border.BorderLeft
            = border.BorderRight
            = border.BorderTop
            = NPOI.SS.UserModel.BorderStyle.Thin;
        border.WrapText = true;
        border.VerticalAlignment = VerticalAlignment.Top;

        s1.SetColumnWidth(0, 5000);
        s1.SetColumnWidth(1, 4000);
        s1.SetColumnWidth(2, 9000);
        s1.SetColumnWidth(3, 3000);
        s1.SetColumnWidth(4, 3000);
        s1.SetColumnWidth(5, 3000);
        s1.SetColumnWidth(6, 3000);
        s1.SetColumnWidth(7, 4000);
        s1.SetColumnWidth(8, 4000);
        s1.SetColumnWidth(9, 4000);

        ICellStyle cellNumBorder = hssfworkbook.CreateCellStyle();
        cellNumBorder.Alignment = HorizontalAlignment.Right;
        cellNumBorder.VerticalAlignment = VerticalAlignment.Top;
        cellNumBorder.DataFormat = HSSFUtils.CellDataFormat.GetDataFormat(hssfworkbook, "#,##0.00");
        cellNumBorder.BorderBottom = cellNumBorder.BorderRight = cellNumBorder.BorderLeft = cellNumBorder.BorderTop = BorderStyle.Thin;

        int detailsCount = dtDetails.Rows.Count;
        for (int i = 0; i < detailsCount; i++)
        {
            try
            {
                String imgPath = System.IO.Path.Combine(productImagePath, dtDetails.Rows[i]["pic"].ToString() + ".jpg");
                System.Drawing.Image image = System.Drawing.Image.FromFile(imgPath);
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                IDrawing patriarch = s1.CreateDrawingPatriarch();
                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 1, 8 + i, 2, 9 + i);
                anchor.AnchorType = AnchorType.MoveDontResize;

                int index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.JPEG);
                IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
            }
            catch
            { }

            s1.CreateRow(8 + i).CreateCell(0).SetCellValue(dtDetails.Rows[i]["ma_sanpham"].ToString());
            s1.GetRow(8 + i).HeightInPoints = 50;
            s1.GetRow(8 + i).CreateCell(1).SetCellValue("");
            s1.GetRow(8 + i).CreateCell(2).SetCellValue(dtDetails.Rows[i]["mota_tienganh"].ToString());

            s1.GetRow(8 + i).CreateCell(3).SetCellValue(dtDetails.Rows[i]["sl_inner"].ToString());
            s1.GetRow(8 + i).CreateCell(4).SetCellValue(dtDetails.Rows[i]["master"].ToString());

            s1.GetRow(8 + i).CreateCell(5).SetCellValue(tenkt.Equals("cm") ? double.Parse(dtDetails.Rows[i]["v2"].ToString()) : double.Parse(dtDetails.Rows[i]["cbf"].ToString()));
            s1.GetRow(8 + i).GetCell(5).SetCellType(CellType.Numeric);

            s1.GetRow(8 + i).CreateCell(6).SetCellValue(double.Parse(dtDetails.Rows[i]["nopack"].ToString()));
            s1.GetRow(8 + i).GetCell(6).SetCellType(CellType.Numeric);

            s1.GetRow(8 + i).CreateCell(7).SetCellValue(double.Parse(dtDetails.Rows[i]["giafob"].ToString()));
            s1.GetRow(8 + i).GetCell(7).SetCellType(CellType.Numeric);

            s1.GetRow(8 + i).CreateCell(8).SetCellValue(dtDetails.Rows[i]["ten_cangbien"].ToString());

            s1.GetRow(8 + i).CreateCell(9).SetCellValue(int.Parse(dtDetails.Rows[i]["soluong"].ToString()));
            s1.GetRow(8 + i).GetCell(9).SetCellType(CellType.Numeric);
        }

        for (int i = 0; i < dtDetails.Rows.Count; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                if (j == 2)
                {
                    s1.GetRow(8 + i).GetCell(j).CellStyle = cellStyleLeft;
                }
                else if (j == 5)
                {
                    s1.GetRow(8 + i).GetCell(j).CellStyle = cellStyleNumber3;
                }
                else if (j == 7)
                {
                    s1.GetRow(8 + i).GetCell(j).CellStyle = cellStyleNumber2;
                }
                else
                {
                    s1.GetRow(8 + i).GetCell(j).CellStyle = cellStyleCenter;
                }
            }
        }

        int totalLine = 0;
        s1.CreateRow(totalLine * 8 + 10 + detailsCount).CreateCell(0).SetCellValue("Minimum order:");
        s1.CreateRow(totalLine * 8 + 11 + detailsCount).CreateCell(0).SetCellValue("Shipment time:");
        s1.CreateRow(totalLine * 8 + 12 + detailsCount).CreateCell(0).SetCellValue("Payment terms:");
        s1.CreateRow(totalLine * 8 + 13 + detailsCount).CreateCell(0).SetCellValue("Validity  until:");

        ICellStyle wrap = hssfworkbook.CreateCellStyle();
        wrap.WrapText = true;

        s1.GetRow(totalLine * 8 + 10 + detailsCount).CreateCell(1).SetCellValue("1x40” container ( a full container)");
        s1.GetRow(totalLine * 8 + 11 + detailsCount).CreateCell(1).SetCellValue(String.Format("{0} - {1} days after receipt of original L/C or 30% deposit", dtDetails.Rows[0]["shipmenttime"].ToString(), dtDetails.Rows[0]["shipmenttime2"].ToString()));
        s1.GetRow(totalLine * 8 + 12 + detailsCount).CreateCell(1).SetCellValue("TTR 30% deposit, 70% after shipment, or L/C at sight ( value over USD 20,000)");
        s1.GetRow(totalLine * 8 + 13 + detailsCount).CreateCell(1).SetCellValue(DateTime.Parse(dtDetails.Rows[0]["ngayhethan"].ToString()).ToString("dd/MMM/yyyy"));

        return hssfworkbook;
    }

    public String C_baogia_id
    {
        get { return c_baogia_id; }
        set { c_baogia_id = value; }
    }

    public BaoGiaMauReport_QRCode(String c_baogia_id, String logoPath, String productImagePath, params object[] args)
    {
        this.c_baogia_id = c_baogia_id;
        this.logoPath = logoPath;
        this.productImagePath = productImagePath;
        if (args.Length > 0)
            this.type = args[0].ToString();
        else
            this.type = "";
    }
}