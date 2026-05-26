using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System.IO;
using System.Web;

[HubName("mainhub")]
public class MainHub : Hub
{
    public partial class UserLoginMobile
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public string emailcc = "anco@ancopottery.com,info@ancopottery.com";

    public void Login(UserLoginMobile user)
    {
        string manv = user.Username;
        string password = user.Password;
        string json = string.Format(@"[{{ ""user"":""{0}"", ""key"":""{1}"", ""status"":""{2}""}}]", manv, "", "false");
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", json }
        };

        if (manv != "" & manv != null)
        {
            string matkhau = Security.EncodeMd5Hash(password);
            string manvmd5 = Security.EncodeMd5Hash(manv);
            string sql = "select * from nhanvien where manv = @manv and matkhau = @matkhau and hoatdong = 1";
            var dt = mdbc.GetData(sql, "@manv", manv, "@matkhau", matkhau);

            if (dt.Rows.Count != 0)
            {
                json = string.Format(@"[{{ ""user"":""{0}"", ""key"":""{1}"", ""status"":""{2}""}}]", manv, manvmd5, "true");
            }

            response["Success"] = true;
            response["Message"] = json;
        }

        Clients.Caller.Login(response);
    }

    public void SizeInfo(string ma_sanpham)
    {
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };

        try
        {
            var db = new LinqDBDataContext();
            var sp = db.md_sanphams.Where(s => s.ma_sanpham.StartsWith(ma_sanpham) & s.trangthai == "DHD").FirstOrDefault();

            string kt = sp.l_cm + "x" + sp.w_cm + "xH" + sp.h_cm + "cm";
            var json = new Dictionary<string, string>();
            json["dong6"] = ma_sanpham;
            json["dong9"] = ExcuteSignalRStatic.getSizeRectOrRound(db,sp, kt);
            json["dong14"] = "";

            var dtFOB = ExcuteSignalRStatic.getFOB(sp.md_sanpham_id);
            json["dong15"] = dtFOB.Rows.Count > 0 ? dtFOB.Rows[0]["ten_cangbien"].ToString() : "";

            var dt = ExcuteSignalRStatic.getDongGoi(sp.md_sanpham_id);
            foreach (System.Data.DataRow row in dt.Rows)
            {
                string sl_inner = row["sl_inner"].ToString(), dvt_inner = row["dvtinner"].ToString();
                string sl_outer = row["sl_outer"].ToString(), dvt_outer = row["dvtouter"].ToString();
                var dDVT = ExcuteSignalRStatic.getTextFromInner(dvt_inner, dvt_outer, sl_inner, sl_outer);
                json["dong10"] = dDVT.inner;
                json["dong11"] = dDVT.outer;
                json["packingText"] = dDVT.packingText;
                json["sizeText"] = dDVT.sizeText;

                json["dong12"] = "" + row["l2_mix"] + "x" + row["w2_mix"] + "xH" + row["h2_mix"] + "cm";
                json["dong13"] = row["v2"].ToString();

                string slc_20 = row["soluonggoi_ctn_20"].ToString();
                string slc_40 = row["soluonggoi_ctn"].ToString();
                string slc_40HC = row["soluonggoi_ctn_40hc"].ToString();

                if (slc_40 != "" & slc_40 != "0")
                {
                    json["dong14"] += "\\n" + slc_40 + " " + dDVT.quantity + "/40'";
                }

                if (json["dong14"] != "")
                {
                    json["dong14"] = json["dong14"].Substring(2);
                }
            }
            response["Success"] = true;
            response["Message"] = JsonConvert.SerializeObject(json);
        }
        catch (Exception ex) { response["Message"] = ma_sanpham + ":" + ex; }
        Clients.Caller.SizeInfo(response);
    }

    public void ColorInfo(string ma_sanpham)
    {
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };

        try
        {
            var img = ExcuteSignalRStatic.getImage(ma_sanpham, 1);
            response["Success"] = true;
            response["Message"] = img["msLink"];
            response["CL"] = img["cl"];
            response["DT"] = img["dt"];
            response["MS"] = img["ms"];
            response["Input"] = ma_sanpham;
        }
        catch (Exception ex)
        {
            response["Message"] = ma_sanpham + ":" + ex;
        }
        Clients.Caller.ColorInfo(response);
    }

    public void PreviewAll(dynamic arrSP)
    {
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };

        var db = new LinqDBDataContext();

        try
        {
            for(var i=0; i < arrSP.Count; i++)
            {
                var rowSP = arrSP[i];
                string msp = rowSP.ma_sanpham;
                var sp = db.md_sanphams.Where(s => s.ma_sanpham == msp & s.trangthai == "DHD").FirstOrDefault();
                if (sp != null)
                {
                    string kt = sp.l_cm + "x" + sp.w_cm + "xH" + sp.h_cm + "cm";
                    rowSP.kichthuoc = ExcuteSignalRStatic.getSizeRectOrRound(db, sp, kt);
                    var dtFOB = ExcuteSignalRStatic.getFOB(sp.md_sanpham_id);
                    rowSP.fob = dtFOB.Rows.Count > 0 ? dtFOB.Rows[0]["ten_cangbien"].ToString() : "";
                    var dtPrice = ExcuteSignalRStatic.getPrice("'" +sp.ma_sanpham + "'");
                    rowSP.price = dtPrice.Rows.Count > 0 ? dtPrice.Rows[0]["gia"].ToString() : "";
                    var dt = ExcuteSignalRStatic.getDongGoi(sp.md_sanpham_id);
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        string sl_inner = row["sl_inner"].ToString(), dvt_inner = row["dvtinner"].ToString();
                        string sl_outer = row["sl_outer"].ToString(), dvt_outer = row["dvtouter"].ToString();
                        var dDVT = ExcuteSignalRStatic.getTextFromInner(dvt_inner, dvt_outer, sl_inner, sl_outer);
                        rowSP.inner = dDVT.inner;
                        rowSP.master = dDVT.outer;
                        rowSP.packingText = dDVT.packingText;
                        rowSP.sizeText = dDVT.sizeText;

                        rowSP.kichthuocDG = "" + row["l2_mix"] + "x" + row["w2_mix"] + "xH" + row["h2_mix"] + "cm";
                        rowSP.CBM = row["v2"].ToString();

                        string slc_20 = row["soluonggoi_ctn_20"].ToString();
                        string slc_40 = row["soluonggoi_ctn"].ToString();
                        string slc_40HC = row["soluonggoi_ctn_40hc"].ToString();

                        if (slc_40 != "" & slc_40 != "0")
                        {
                            rowSP.slcont += "\\n" + slc_40 + " " + dDVT.quantity + "/40'";
                        }

                        if (rowSP.slcont != "")
                        {
                            string slcont = (rowSP.slcont + "");
                            rowSP.slcont = slcont.Substring(2);
                        }
                    }

                    var img = ExcuteSignalRStatic.getImage(sp.ma_sanpham, 0);
                    rowSP.image = img["msLink"];
                }
            }
            response["Message"] = arrSP;
        }
        catch (Exception ex)
        {
            response["Message"] =  ex;
        }
        Clients.Caller.PreviewAll(response);
    }

    public void ProductInfo(string ma_sanpham)
    {
        var db = new LinqDBDataContext();
        string arr_sanpham = "'" + ma_sanpham + "'";
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };
        try
        {
            ma_sanpham = ma_sanpham.Split(new[] { '\r', '\n' }).FirstOrDefault();
            string unknown = "(Unknown)";
            string dong0 = unknown, dong1 = unknown, dong2 = unknown, dong3 = unknown, dong4 = unknown, dong5 = unknown, dong6 = unknown, dong7 = unknown,
            dong8 = "", dong9 = "", dong10 = unknown, dong11 = unknown, dong12 = unknown, dong13 = unknown, dong14 = "", dong15 = unknown, dong16 = unknown,
            image = "", size = "", sizeSel = "", packingText = "", sizeText = "";
            string json = @"[{{ 
                ""dong0"":""{0}""
                , ""dong1"":""{1}""
                , ""dong2"":""{2}""
                , ""dong3"":""{3}""
                , ""dong4"":""{4}""
                , ""dong5"":""{5}""
                , ""dong6"":""{6}""
                , ""dong7"":""{7}""
                , ""dong8"":""{8}""
                , ""dong9"":""{9}""
                , ""dong10"":""{10}""
                , ""dong11"":""{11}""
                , ""dong12"":""{12}""
                , ""dong13"":""{13}""
                , ""dong14"":""{14}""
                , ""dong15"":""{15}""
                , ""dong16"":""{16}""
                , ""image"":""{17}""
                , ""detai"":""{18}""
                , ""bientau"":""{19}""
                , ""mausac"":""{20}""
                , ""size"":""{21}""
                , ""sizeSel"":""{22}""
                , ""packingText"":""{23}""
                , ""sizeText"":""{24}""
            }}]";

            var sp = db.md_sanphams.Where(s => s.ma_sanpham == ma_sanpham & s.trangthai == "DHD").FirstOrDefault();
            if (sp != null)
            {
                string cl = sp.ma_sanpham.Substring(0, 2);
                string bc = sp.ma_sanpham.Substring(9, 2);

                var plhh = db.md_chungloais.Where(s => s.code_cl == cl).FirstOrDefault();
                if (plhh != null)
                {
                    dong0 = plhh.ta_ngan.ToUpper();
                }
                dong6 = sp.ma_sanpham;
                //dong 7, dong 8, dong 9
                if (bc.Substring(0, 1) != "0" & bc != "S1")
                {
                    //Tim` bo S cua san pham
                    string msp_0009 = sp.ma_sanpham.Substring(0, 9), msp_1103 = sp.ma_sanpham.Substring(11, 3);
                    string ma_bo_cha = db.md_sanphams.Where(s => s.ma_sanpham.Substring(0, 9) == msp_0009
                    & s.ma_sanpham.Substring(11, 3) == msp_1103 & s.ma_sanpham.Substring(9, 2).Contains("S")).Select(s => s.ma_sanpham.Substring(9, 2)).FirstOrDefault();
                    //Lay cac ma cai thuoc bo cua san pham
                    string md_bo_id = db.md_bos.Where(s => s.ma_bo == bc & s.ma_bo_cha == ma_bo_cha).Select(s => s.md_bo_id).FirstOrDefault();
                    foreach (var bct in db.md_bo_chitiets.Where(s => s.md_bo_id == md_bo_id))
                    {
                        string sanpham_add = msp_0009 + bct.md_bo_detail + msp_1103;
                        arr_sanpham += ",'" + sanpham_add + "'";
                    }
                }
				
				var arrSps = arr_sanpham.Split(',');
				for(var i=0; i < arrSps.Length; i++) {
					sizeSel += arrSps[i].Substring(10, 2) + ",";
				}
				
				sizeSel = sizeSel.Length > 0 ? sizeSel.Substring(0, sizeSel.Length - 1) : "";

                var dt = ExcuteSignalRStatic.getPrice(arr_sanpham);
                string kichthuoc_9 = "";
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    var msp2 = row["ma_sanpham"].ToString();
                    var sp2 = db.md_sanphams.Where(s => s.ma_sanpham == msp2).FirstOrDefault();
                    if (msp2 != ma_sanpham)
                    {
                        if (row["l_cm"].ToString() != "0.0" & row["w_cm"].ToString() != "0.0" & row["h_cm"].ToString() != "0.0")
                        {
                            string kt = row["l_cm"] + "x" + row["w_cm"] + "xH" + row["h_cm"];
                            dong9 += ExcuteSignalRStatic.getSizeRectOrRound(db, sp2, kt) + "/ \\n          ";
                        }
                    }
                    else
                    {
                        dong7 = "$" + row["gia"].ToString();
                        if (row["l_cm"].ToString() != "0.0" & row["w_cm"].ToString() != "0.0" & row["h_cm"].ToString() != "0.0")
                        {
                            string kt = row["l_cm"] + "x" + row["w_cm"] + "xH" + row["h_cm"];
                            kichthuoc_9 = ExcuteSignalRStatic.getSizeRectOrRound(db, sp2, kt);
                        }
                    }
                }
                if (dong8 != "")
                {
                    dong8 = "( " + dong8.Substring(0, dong8.Length - 1) + ")";
                }
                if (dong9 != "")
                {
                    dong9 = dong9.Substring(0, dong9.Length - 14) + "cm";
                }
                else
                {
                    if (kichthuoc_9 != "")
                    {
                        dong9 = kichthuoc_9 + "cm";
                    }
                    else
                    {
                        dong9 = "";
                    }
                }

                dt = ExcuteSignalRStatic.getDongGoi(sp.md_sanpham_id);
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string sl_inner = row["sl_inner"].ToString(), dvt_inner = row["dvtinner"].ToString();
                    string sl_outer = row["sl_outer"].ToString(), dvt_outer = row["dvtouter"].ToString();

                    var dDVT = ExcuteSignalRStatic.getTextFromInner(dvt_inner, dvt_outer, sl_inner, sl_outer);
                    dong10 = dDVT.inner;
                    dong11 = dDVT.outer;
                    packingText = dDVT.packingText;
                    sizeText = dDVT.sizeText;

                    dong12 = "L" + row["l2_mix"] + "xW" + row["w2_mix"] + "xH" + row["h2_mix"] + "cm";
                    dong13 = row["v2"].ToString();

                    string slc_20 = row["soluonggoi_ctn_20"].ToString();
                    string slc_40 = row["soluonggoi_ctn"].ToString();
                    string slc_40HC = row["soluonggoi_ctn_40hc"].ToString();

                    if (slc_40 != "" & slc_40 != "0")
                    {
                        dong14 += "\\n" + slc_40 + " " + dDVT.quantity + "/40'";
                    }
                    if (dong14 != "")
                    {
                        dong14 = dong14.Substring(2);
                    }
                }
                //dong 15
                dt = ExcuteSignalRStatic.getFOB(sp.md_sanpham_id);
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    dong15 = row["ten_cangbien"] + "";
                }

                string msp = sp.ma_sanpham;
                image = ExcuteSignalRStatic.getImageProduct(msp);

                string detai_json = "", bientau_json = "", mausac_json = "", detai_chk = "", bientau_chk = "", mausac_chk = "";
                string chungloai = msp.Substring(0, 2);

                string sql_detail = string.Format(@"
					select 
	                    A.detai, 
                        A.bientau, 
                        A.mausac, 
                        dtai.ta_ngan as detai_ta, 
                        A.bientau as bientau_ta, 
                        msac.ta_ngan as mausac_ta
                    from (
	                    select distinct 
	                        substring(sp.ma_sanpham, 0, 7) as todetai, 
	                        substring(sp.ma_sanpham, 10, 2) as bientau,
	                        substring(sp.ma_sanpham, 7, 2) as detai,
	                        substring(sp.ma_sanpham, 13, 2) as mausac
	                    from md_sanpham (nolock) sp
	                    where 1=1
	                        and sp.hoatdong = 1 
	                        and sp.trangthai = 'DHD'
                            and substring(sp.ma_sanpham, 0, 7) = '{0}'
                    ) A
                    left join md_detai (nolock) dtai on dtai.code_cl = '{1}' and dtai.code_dt = A.detai
                    left join md_mausac (nolock) msac on msac.code_cl = '{1}' and msac.code_dt = A.detai and msac.code_mau = A.mausac
                    where 
                        A.todetai = '{0}'
                        order by A.detai, A.bientau, A.mausac
				", msp.Substring(0, 6), chungloai);

                var dt_detail = mdbc.GetData(sql_detail);
                var arrBienTau = new List<string>();
                foreach (System.Data.DataRow row_dt in dt_detail.Rows)
                {
                    if (!detai_chk.Split(',').Contains(row_dt["detai"].ToString()))
                    {
                        detai_chk += row_dt["detai"] + ",";
                        string dtTA = row_dt["detai_ta"].ToString();
                        dtTA = dtTA.Replace(System.Environment.NewLine, " ");
                        dtTA = dtTA.Replace("\n", " ");
                        detai_json += row_dt["detai"] + " : " + dtTA + ",";
                    }

                    string detai_bientau = row_dt["detai"] + " - " + row_dt["bientau"];
                    if (!bientau_chk.Split(',').Contains(detai_bientau))
                    {
                        bientau_chk += detai_bientau + ",";
                        bientau_json += detai_bientau + ",";
                    }

                    var bientau = row_dt["bientau"] + "";
                    var maBT = msp.Substring(0, 9) + bientau;
                    if ((bientau.StartsWith("0") | bientau.StartsWith("F")) & !arrBienTau.Contains(bientau))
                    {
                        var spBT = db.md_sanphams.Where(s => s.ma_sanpham.StartsWith(maBT)).FirstOrDefault();
                        if (spBT != null)
                        {
                            string kt = spBT.l_cm + "x" + spBT.w_cm + "xH" + spBT.h_cm + "cm";
                            string sizeVal = row_dt["bientau"] + ":" + ExcuteSignalRStatic.getSizeRectOrRound(db, spBT, kt);
                            if (!size.Split(',').Contains(sizeVal)) {
                                size += sizeVal + ",";								
							}
							
							// if (!sizeSel.Split(',').Contains(sizeVal)) {
								// if(arr_sanpham.Contains(msp))
									// sizeSel += sizeVal + ",";
							// }
                        }
                    }

                    string detai_bientau_mausac = row_dt["detai"] + " - " + row_dt["bientau"] + " - " + row_dt["mausac"];
                    if (!mausac_chk.Split(',').Contains(detai_bientau_mausac))
                    {
                        mausac_chk += detai_bientau_mausac + ",";
                        string msTA = row_dt["mausac_ta"].ToString();
                        msTA = msTA.Replace(System.Environment.NewLine, " ");
                        msTA = msTA.Replace("\n", " ");
                        mausac_json += detai_bientau_mausac + " : " + msTA + ",";
                    }
                }

                if (detai_json != "")
                {
                    detai_json = detai_json.Substring(0, detai_json.Length - 1);
                }
                if (bientau_json != "")
                {
                    bientau_json = bientau_json.Substring(0, bientau_json.Length - 1);
                }
                if (mausac_json != "")
                {
                    mausac_json = mausac_json.Substring(0, mausac_json.Length - 1);
                }

                string kq_json = string.Format(json, dong0, dong1, dong2, dong3, dong4, dong5, dong6, dong7, dong8, dong9, dong10, dong11, dong12, dong13, dong14,
                dong15, dong16, image, detai_json, bientau_json, mausac_json, size, sizeSel, packingText, sizeText);
                response["Success"] = true;
                response["Message"] = kq_json;
            }
        }
        catch(Exception ex)
        {
            response["Success"] = false;
            response["Message"] = ex + "";
        }
        Clients.Caller.ProductInfo(response);
    }

    public void GetPrice(dynamic _params)
    {
        string ma_sanpham = _params.spnew;
        string spchanged = _params.sp;
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };
        try
        {
            var db = new LinqDBDataContext();
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
            var rs = new Dictionary<string, object>();
            rs["ma_sanpham"] = ma_sanpham;
            rs["spcu"] = spchanged;

            string msp = ma_sanpham, image = "";
            if (!msp.Contains("-F"))
                image = msp.Substring(0, 8) + ".jpg";
            else
                image = msp.Substring(0, 11) + ".jpg";
            image = Public.imgProduct + image;
            rs["image"] = image; 
            rs["gia"] = "(unknown)";
            foreach (System.Data.DataRow row in dt.Rows)
            {
                rs["gia"] = "$" + row["gia"].ToString();
                break;
            }

            response["Success"] = true;
            response["Message"] = JsonConvert.SerializeObject(rs);
        }
        catch (Exception ex)
        {
            response["Message"] = ex.Message;
        }
        Clients.Caller.GetPrice(response);
    }

    public void PartnerInfo(string parrams)
    {
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };
        try
        {
            var db = new LinqDBDataContext();
            var dtkds = from a in db.md_doitackinhdoanhs.Where(s => s.hoatdong == true)
                        join b in db.md_loaidtkds on a.md_loaidtkd_id equals b.md_loaidtkd_id
                        where b.ma_loaidtkd == "KH"
                        orderby a.ma_dtkd
                        select new { a.ma_dtkd, a.ten_dtkd, a.tel, a.email, a.diachi };
            string json = "[";
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
            json += json_detail + "]";

            response["Success"] = true;
            response["Message"] = json;
        }
        catch (Exception ex)
        {
            response["Message"] = ex.Message;
        }
        Clients.Caller.PartnerInfo(response);
    }
	
    public void CreateQO(dynamic parrams)
    {
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };

        string msg = "";
        try
        {
            var db = new LinqDBDataContext();
            var exec = new ExcuteSignalR();
            string username = parrams.username + "";
            string ma_dtkd = parrams.ma_dtkd + "";
            string ten_dtkd = parrams.ten_dtkd + "";
            string email = parrams.email + "";
            string phone = parrams.phone + "";
            string address = parrams.address + "";
            string nguoilienhe = parrams.nguoilienhe + "";
            string json_data = parrams.json_data + "";
            var d = JsonConvert.DeserializeObject<List<Objects.Json_product>>(json_data);

            var nv = db.nhanviens.FirstOrDefault(p => p.manv == username);
            if (nv == null)
            {
                msg = "Not found user \"" + username + "\" in system.";
            }
            else
            {
                var dtkd = exec.createDTKD(db, ma_dtkd, ten_dtkd, email, phone, address, nguoilienhe);

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
                    mota = "",
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
                            exec.add_ctbg(row.ma_sanpham, maxOrder, gia_fob, mnuqr.c_baogiaqr_id, username, dtkd.md_doitackinhdoanh_id, db, "qr", row.soluong);
                        }
                        db.SubmitChanges();
                    }

                    try
                    {
                        var ms = new MemoryStream();
                        // Lấy thông tin smtp
                        md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
                        string path = ExcuteSignalRStatic.mapPathSignalR("~/FileSendMail/");
                        String fileNameXLS = nv.manv + "-Quotation-" + DateTime.Now.ToString("mmhh-dd-MM-yyyy") + ".xls";
                        String logoPath = ExcuteSignalRStatic.mapPathSignalR("~/images/VINHGIA_logo_print.png");
                        String productImagePath = ExcuteSignalRStatic.mapPathSignalR("~/images/products/fullsize/");
                        var rptQO = new Objects.BaoGiaMauReport_QRCode(db, mnu.c_baogia_id, logoPath, productImagePath, "qr");

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
                        msg = parrams.success;

                    }
                    catch (Exception ex)
                    {
                        msg = parrams.success + "\nSend mail error." + ex.Message;
                    }
                    response["Success"] = true;
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex + "";
        }
        response["Message"] = msg;
        response["data_bk"] = parrams.data_bk;
        Clients.Caller.QOPOexcute(response);
    }

    public void CreatePO(dynamic parrams)
    {
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };
        string msg = "";
        try
        {
            var db = new LinqDBDataContext();
            var exec = new ExcuteSignalR();
            string username = parrams.username + "";
            string ma_dtkd = parrams.ma_dtkd + "";
            string ten_dtkd = parrams.ten_dtkd + "";
            string email = parrams.email + "";
            string phone = parrams.phone + "";
            string address = parrams.address + "";
            string nguoilienhe = parrams.nguoilienhe + "";
            string json_data = parrams.json_data + "";
            var d = JsonConvert.DeserializeObject<List<Objects.Json_product>>(json_data);
            var nv = db.nhanviens.FirstOrDefault(p => p.manv == username);
            if (nv == null)
            {
                msg = "Not found user \"" + username + "\" in system.";
            }
            else if (ma_dtkd.Trim() == "")
            {
                msg = "Error: Customer Code is not empty.";
            }
            else if (ten_dtkd.Trim() == "")
            {
                msg = "Error: Customer Name is not empty.";
            }
            else if (json_data == null | json_data == "" | json_data == "[]")
            {
                msg = "Error: Have no product.";
            }
            else
            {
                md_doitackinhdoanh dtkd = exec.createDTKD(db, ma_dtkd, ten_dtkd, email, phone, address, nguoilienhe);
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
                mnu.nguoilap = nv.manv;
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
                mnu.sochungtu = "***/" + sochungtu.Substring(4);

                mnu.mota = "";
                mnu.hoatdong = true;
                mnu.ngaytao = DateTime.Now;
                mnu.ngaycapnhat = DateTime.Now;
                mnu.nguoitao = nv.manv;
                mnu.nguoicapnhat = nv.manv;

                db.c_donhangqrs.InsertOnSubmit(mnu);
                db.SubmitChanges();

                String getOrder = "select max(sothutu) as count from c_dongdonhangqr where c_donhangqr_id = @c_donhang_id";
                object omax = mdbc.ExecuteScalar(getOrder, "@c_donhang_id", c_donhang_id);
                int maxOrder = (omax.ToString().Equals("")) ? 0 : (int)omax;
                foreach (var row in d)
                {
                    decimal gia_fob = 0;
                    try { gia_fob = decimal.Parse(row.gia_fob.Replace("$", "").Replace(" ", "")); } catch { }
                    exec.add_ctdonhang(row.ma_sanpham, row.soluong.ToString(), maxOrder, gia_fob, mnu.c_donhangqr_id, username, dtkd.md_doitackinhdoanh_id, db, "qr");
                }
                db.SubmitChanges();
                response["Success"] = true;
                msg = parrams.success;
            }
        }
        catch (Exception ex)
        {
            msg = ex + "";
        }
        response["Message"] = msg;
        response["data_bk"] = parrams.data_bk;
        Clients.Caller.QOPOexcute(response);
    }

    public void SendMailQO(dynamic parrams)
    {
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };
        string msg = "";
        try
        {
            var db = new LinqDBDataContext();
            var exec = new ExcuteSignalR();
            string username = parrams.username + "";
            string ma_dtkd = parrams.ma_dtkd + "";
            string ten_dtkd = parrams.ten_dtkd + "";
            string email = parrams.email + "";
            string phone = parrams.phone + "";
            string address = parrams.address + "";
            string nguoilienhe = parrams.nguoilienhe + "";
            string json_data = parrams.json_data + "";
            var d = JsonConvert.DeserializeObject<List<Objects.Json_product>>(json_data);

            md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
            nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv == username);
            if (nv == null)
            {
                msg = "Not found user \"" + username + "\" in system.";
            }
            else if (ten_dtkd == "" | ten_dtkd == null)
            {
                msg = "Company Name is not empty.";
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
                    response["Success"] = true;
                    msg = parrams.success;
                }
                catch (Exception ex)
                {
                    msg = ex + "";
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex + "";
        }
        response["Message"] = msg;
        response["data_bk"] = parrams.data_bk;
        Clients.Caller.QOPOexcute(response);
    }

    public void SendMail(dynamic parrams)
    {
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };
        string msg = "";
        string preview = "";
        try
        {
			preview = parrams.preview + "";
            var db = new LinqDBDataContext();
            var exec = new ExcuteSignalR();
            string username = parrams.username + "";
            string ma_dtkd = parrams.ma_dtkd + "";
            string ten_dtkd = parrams.ten_dtkd + "";
            string email = parrams.email + "";
            string phone = parrams.phone + "";
            string address = parrams.address + "";
            string nguoilienhe = parrams.nguoilienhe + "";
            string note = parrams.note + "";
            string json_data = parrams.json_data + "";
            var d = JsonConvert.DeserializeObject<List<Objects.Json_product>>(json_data);

            md_smtp smtp = db.md_smtps.FirstOrDefault(p => p.macdinh.Equals(true));
            nhanvien nv = db.nhanviens.FirstOrDefault(p => p.manv == username);
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
                var ms = new MemoryStream();
                string path = ExcuteSignalRStatic.mapPathSignalR("~/FileSendMail/");
                String fileNameXLS = nv.manv + "-Inquiry-" + DateTime.Now.ToString("mmhh-dd-MM-yyyy") + ".xls";
                String logoPath = ExcuteSignalRStatic.mapPathSignalR("~/images/VINHGIA_logo_print.png");
                String productImagePath = ExcuteSignalRStatic.mapPathSignalR("~/images/products/fullsize/");

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
                if (preview == "true")
                {
                    msg = string.Format("<b style='font-size: 110%'>{0}</b><br><br> {1}", subject, body);
                }
                else
                {
                    var rptQO = new Objects.Inquiry_QRCode(db, d, logoPath, productImagePath, ten_dtkd, address, phone, "");
                    NPOI.HSSF.UserModel.HSSFWorkbook wb = rptQO.CreateWBQuotation();
                    wb.Write(ms);
                    File.WriteAllBytes(path + fileNameXLS, ms.ToArray());
                    ms.Close();

                    mail.Send(nv.email, email.Trim(), emailcc, subject, body, path + fileNameXLS);
                    msg = parrams.success;
                }
                response["Success"] = true;
            }
        }
        catch (Exception ex)
        {
            msg = ex + "";
        
		}
        response["Message"] = msg;
		try {
			response["data_bk"] = parrams.data_bk;
		}
		catch(Exception ex) { }
		
        if (preview == "true")
            Clients.Caller.PreviewEmail(response);
        else
            Clients.Caller.SendEmail(response);
    }

    public void searchProduct(string _params)
    {
        string ma_sanpham = _params;
        var response = new Dictionary<string, object>()
        {
            { "Success", false },
            { "Message", "" }
        };

        string msg = "";
        try
        {
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
                string msp = row["ma_sanpham"].ToString(), 
                    image = ExcuteSignalRStatic.getImageProduct(msp), 
                    spId = row["md_sanpham_id"].ToString();

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
                    if (!detai_chk.Split(',').Contains(row_dt["detai"].ToString()))
                    {
                        detai_chk += row_dt["detai"] + ",";
                        string dtTA = row_dt["detai_ta"].ToString();
                        dtTA = dtTA.Replace(System.Environment.NewLine, " ");
                        dtTA = dtTA.Replace("\n", " ");
                        detai_json += row_dt["detai"] + " : " + dtTA + ",";
                    }

                    string detai_bientau = row_dt["detai"] + " - " + row_dt["bientau"];
                    if (!bientau_chk.Split(',').Contains(detai_bientau))
                    {
                        bientau_chk += detai_bientau + ",";
                        bientau_json += detai_bientau + ",";
                    }

                    string detai_bientau_mausac = row_dt["detai"] + " - " + row_dt["bientau"] + " - " + row_dt["mausac"];
                    if (!mausac_chk.Split(',').Contains(detai_bientau_mausac))
                    {
                        mausac_chk += detai_bientau_mausac + ",";
                        string msTA = row_dt["mausac_ta"].ToString();
                        msTA = msTA.Replace(System.Environment.NewLine, " ");
                        msTA = msTA.Replace("\n", " ");
                        mausac_json += detai_bientau_mausac + " : " + msTA + ",";
                    }
                }
                if (detai_json != "")
                {
                    detai_json = detai_json.Substring(0, detai_json.Length - 1);
                }
                if (bientau_json != "")
                {
                    bientau_json = bientau_json.Substring(0, bientau_json.Length - 1);
                }
                if (mausac_json != "")
                {
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
            if (detail != "")
            {
                detail = detail.Substring(0, detail.Length - 1);
            }
            response["Success"] = true;
            msg = "[" + detail + "]";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        response["Message"] = msg;
        Clients.Caller.searchProduct(response);
    }
}