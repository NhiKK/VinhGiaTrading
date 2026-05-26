using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;
using System.Linq;

namespace jqGridHelper
{
    public class Utils
    {
        public static double Round2(double value, int digits)
        {
            if (digits >= 0) return Math.Round(value, digits);

            double n = Math.Pow(10, -digits);
            return Math.Round(value / n, 0) * n;
        }

        public static decimal Round3(decimal d, int decimals)
        {
            if (decimals >= 0) return decimal.Round(d, decimals);

            decimal n = (decimal)Math.Pow(10, -decimals);
            return decimal.Round(d / n, 0) * n;
        }

        public static decimal Round4(decimal d, int decimals)
        {
            if (decimals >= 0) return Math.Round(d, decimals);

            decimal n = (decimal)Math.Pow(10, -decimals);
            return Math.Round(d / n, 0) * n;
        }

        public static decimal Round5(decimal d, int decimals)
        {
            if (decimals >= 0) return decimal.Round(d, decimals);

            decimal n = (decimal)Math.Pow(10, -decimals);
            return decimal.Ceiling(d / n) * n;
        }

        public static void writeResult(int type, string message)
        {
            HttpContext.Current.Response.Clear();
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            xml += "<root>";
            xml += "<type>" + type + "</type>";
            xml += "<message>" + message + "</message>";
            xml += "</root>";
            HttpContext.Current.Response.Write(xml);
        }

        public static void ShowMsg(String Content)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = 500;
            HttpContext.Current.Response.StatusDescription = Content;
            HttpContext.Current.Response.Write(Content);
        }

        public static String ToXml(int page, int totalPage, int count, params object[] values)
        {
            String result = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            result += String.Format(
                        "<rows><page>{0}</page><total>{1}</total><records>{2}</records>"
                        , page, totalPage, count
                      );
            for (int i = 0; i < values.Length; i++)
            {
                result += "<row>";
                result += "<cell><![CDATA[" + values[i] + "]]></cell>";
                result += "</row>";
            }
            result = "</rows>";
            return result;
        }



        public static string taoChungTuPO(string md_doitackinhdoanh_id, int isdonhangmau, string namchungtu)
        {
            LinqDBDataContext db = new LinqDBDataContext();
            string result = "";
            string ctdh = "";
            string ctmau = "";
            string namht = "";
            string namct = "";
            string nam = "";
            string makhachhang = db.md_doitackinhdoanhs.Single(k => k.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id)).ma_dtkd;

            string sql = "";
            if (isdonhangmau == 0)
            {
                sql = @"select top 1 substring(sochungtu, 1, 3) as ctdh, SUBSTRING(sochungtu, 1, 3) as ctmau, datepart(YEAR, GETDATE()), DATEPART(YEAR, ngaylap) 
                    from c_donhang 
                    where donhang_mau = 0 and isdonhangtmp = 0 and year(ngaylap) = " + namchungtu + " and md_doitackinhdoanh_id ='" + md_doitackinhdoanh_id + "' order by SUBSTRING(sochungtu, 1, 3) desc";
            }
            if (isdonhangmau == 1)
            {
                sql = @"select top 1 substring(sochungtu, 1, 3) as ctdh, SUBSTRING(sochungtu, 1, 3) as ctmau, datepart(YEAR, GETDATE()), DATEPART(YEAR, ngaylap) 
                    from c_donhang 
                    where donhang_mau = 1 and isdonhangtmp = 0 and year(ngaylap) = " + namchungtu + " and md_doitackinhdoanh_id ='" + md_doitackinhdoanh_id + "' order by SUBSTRING(sochungtu, 1, 3) desc";
            }
            else if (isdonhangmau == 2)// don hang tmp
            {
                sql = @"select top 1 substring(sochungtu, 1, 3) as ctdh, SUBSTRING(sochungtu, 1, 3) as ctmau, datepart(YEAR, GETDATE()), DATEPART(YEAR, ngaylap) 
                    from c_donhang 
                    where donhang_mau = 0 and isdonhangtmp = 1 and year(ngaylap) = " + namchungtu + " and md_doitackinhdoanh_id ='" + md_doitackinhdoanh_id + "' order by SUBSTRING(sochungtu, 1, 3) desc";
            }

            DataTable data = mdbc.GetData(sql);
            foreach (DataRow item in data.Rows)
            {
                ctdh = item[0].ToString();
                ctmau = item[1].ToString();
                namht = item[2].ToString();
                namct = item[3].ToString();
            }

            if (isdonhangmau == 0)//la don hang cua khach
            {

                if (data.Rows.Count == 0)//neu chua co don hang nao cua khach nay
                {
                    result = "001/" + makhachhang + "-VG-" + namchungtu.Substring(2, 2);
                }
                else
                {
                    //nam = (!namht.Equals(namct) ? namht.Substring(2, 2) : namct.Substring(2, 2));
                    result = genericNumberDoc(ctdh) + "/" + makhachhang + "-VG-" + namchungtu.Substring(2, 2);
                }
            }
            else if (isdonhangmau == 1)//la don hang mau
            {
                if (data.Rows.Count == 0)//neu chua co don hang nao cua khach nay
                {
                    result = "001/" + makhachhang + "-VG_SR-" + namchungtu.Substring(2, 2);
                }
                else
                {
                    //nam = (!namht.Equals(namct) ? namht.Substring(2, 2) : namct.Substring(2, 2));
                    result = genericNumberDoc(ctmau) + "/" + makhachhang + "-VG_SR-" + namchungtu.Substring(2, 2);
                }
            }
            else if (isdonhangmau == 2) // la don hang tmp
            {
                if (data.Rows.Count == 0)//neu chua co don hang nao cua khach nay
                {
                    result = "001/" + makhachhang + "-VG_TI-" + namchungtu.Substring(2, 2);
                }
                else
                {
                    //nam = (!namht.Equals(namct) ? namht.Substring(2, 2) : namct.Substring(2, 2));
                    result = genericNumberDoc(ctmau) + "/" + makhachhang + "-VG_TI-" + namchungtu.Substring(2, 2);
                }
            }

            return result.ToUpper();
        }


        public static string taoChungTuQO(string md_doitackinhdoanh_id, int isdonhangmau, string namchungtu)//isdonhangmau default la 0 do QO ko co QO mẩu
        {
            LinqDBDataContext db = new LinqDBDataContext();
            string result = "";
            string ctdh = "";
            string namht = "";
            string namct = "";
            string nam = "";
            string makhachhang = db.md_doitackinhdoanhs.Single(k => k.md_doitackinhdoanh_id.Equals(md_doitackinhdoanh_id)).ma_dtkd;
            string sql = @"select top 1 substring(sobaogia, 1, 3) as ctdh, datepart(YEAR, GETDATE()), DATEPART(YEAR, ngaybaogia) 
                    from c_baogia 
                    where isform = " + isdonhangmau + " and year(ngaybaogia) = " + namchungtu + " and md_doitackinhdoanh_id ='" + md_doitackinhdoanh_id + "' order by substring(sobaogia, 1, 3) desc";
            DataTable data = mdbc.GetData(sql);
            foreach (DataRow item in data.Rows)
            {
                ctdh = item[0].ToString();
                namht = item[1].ToString();
                namct = item[2].ToString();
            }
            if (isdonhangmau == 0)//la don hang cua khach
            {

                if (data.Rows.Count == 0)//neu chua co don hang nao cua khach nay
                {

                    result = "001/" + makhachhang + "-" + namchungtu.Substring(2, 2);
                }
                else
                {
                    //nam = (!namht.Equals(namct) ? namht.Substring(2, 2) : namct.Substring(2, 2));
                    result = genericNumberDoc(ctdh) + "/" + makhachhang + "-" + namct.Substring(2, 2);
                }
            }
            else
            {
                if (data.Rows.Count == 0)//neu chua co don hang nao cua khach nay
                {

                    result = "001/" + makhachhang + "_SR-" + namchungtu.Substring(2, 2);
                }
                else
                {
                    //nam = (!namht.Equals(namct) ? namht.Substring(2, 2) : namct.Substring(2, 2));
                    result = genericNumberDoc(ctdh) + "/" + makhachhang + "_SR" + "-" + namct.Substring(2, 2);
                }
            }


            return result.ToUpper();
        }

        public static string taoChungTuINV(LinqDBDataContext db, string selHangCua)
        {
            string result = "";
            var rsSQL = db.c_packinginvoices.Where(s =>
                    System.Data.Linq.SqlClient.SqlMethods.Like(s.so_inv.Substring(0, 4), "[0-9][0-9][0-9][0-9]") &
                    System.Data.Linq.SqlClient.SqlMethods.Like(s.so_inv.Substring(s.so_inv.Length - 2, 2), "[0-9][0-9]")
                )
                .Where(s => s.hangcua == selHangCua)
                .Select(s => new { sct = s.so_inv.Substring(0, 4), ntc = s.so_inv.Substring(s.so_inv.Length - 2, 2) })
                .OrderByDescending(s => s.ntc).ThenByDescending(s => s.sct).Take(1).FirstOrDefault();

            if (rsSQL == null)
            {
                rsSQL = db.c_packinginvoices.Where(s =>
                    System.Data.Linq.SqlClient.SqlMethods.Like(s.so_inv.Substring(0, 3), "[0-9][0-9][0-9]") &
                    System.Data.Linq.SqlClient.SqlMethods.Like(s.so_inv.Substring(s.so_inv.Length - 2, 2), "[0-9][0-9]")
                )
                .Select(s => new { sct = s.so_inv.Substring(0, 3), ntc = s.so_inv.Substring(s.so_inv.Length - 2, 2) })
                .OrderByDescending(s => s.ntc).ThenByDescending(s => s.sct).Take(1).FirstOrDefault();
            }
            else
            {
                rsSQL = db.c_packinginvoices.Where(s =>
                        System.Data.Linq.SqlClient.SqlMethods.Like(s.so_inv.Substring(0, 4), "[0-9][0-9][0-9][0-9]") &
                        System.Data.Linq.SqlClient.SqlMethods.Like(s.so_inv.Substring(s.so_inv.Length - 2, 2), "[0-9][0-9]")
                    )
                    .Where(s => s.hangcua == selHangCua)
                    .Select(s => new { sct = s.so_inv.Substring(0, 4), ntc = s.so_inv.Substring(s.so_inv.Length - 2, 2) })
                    .OrderByDescending(s => s.ntc).ThenByDescending(s => s.sct).Take(1).FirstOrDefault();

            }

            var tiento = "PKLINV";
            string stt = "0000";
            selHangCua = selHangCua;

            var chungtu = db.md_sochungtus.Where(s => s.tiento.Equals(tiento)).FirstOrDefault();
            if (selHangCua != "VG")
            {
                tiento = "PKLINV_HHH";
                chungtu = db.md_sochungtus.Where(s => s.tiento.Equals(tiento)).FirstOrDefault();
                selHangCua = "HHH";
            }

            if (chungtu == null)
                throw new Exception(string.Format(@"Không tìm thấy chứng từ có tiền tố là ""{0}"".", tiento));
            var lenght = chungtu.so_duocgan.ToString().Length;
            var km = stt.Substring(0, stt.Length - lenght) + chungtu.so_duocgan + "/" + selHangCua + "-" + chungtu.hauto;
            chungtu.so_duocgan = chungtu.so_duocgan + 1;
            result = km;

            //if (rsSQL == null)
            //{
            //    result = "0001/VG-" + chungtu.hauto;
            //}
            //else
            //{
            //    if (rsSQL.ntc == chungtu.hauto)
            //        result = genericNumberDoc(rsSQL.sct, 4) + "/" + selHangCua + "-" + chungtu.hauto;
            //    else
            //        result = "0001/VG-" + chungtu.hauto;
            //}

            return result;
        }

        public static string genericNumberDoc(string doc, int? sdg = null)
        {
            string result = "";
            try
            {
                int sr = int.Parse(doc);
                sr = sr + 1;
                result = sr.ToString();
            }
            catch (Exception ex)
            {
                result = "1";
            }

            while (result.Length < sdg.GetValueOrDefault(3))
            {
                result = "0" + result;
            }
            return result;
        }

        public static string taoChungTuPLHHH(LinqDBDataContext db, string selHangCua)
        {
            string result = "";
            var tiento = "HHH";

            var a = Helper.nhaCungCaps.Contains(selHangCua);
            string stt = "000";

            var chungtu = db.md_sochungtus.Where(s => s.tiento.Equals(tiento)).FirstOrDefault();
            if (selHangCua != "VG")
            {
                tiento = "PLHHH";
                chungtu = db.md_sochungtus.Where(s => s.tiento.Equals(tiento)).FirstOrDefault();
            }

            if (chungtu == null)
                throw new Exception(string.Format(@"Không tìm thấy chứng từ có tiền tố là ""{0}"".", tiento));

            var lenght = chungtu.so_duocgan.ToString().Length;
            var km = stt.Substring(0, stt.Length - lenght) + chungtu.so_duocgan + "/" + chungtu.tiento + "-" + chungtu.hauto;
            chungtu.so_duocgan = chungtu.so_duocgan + 1;
            result = km;

            //if (rsSQL == null)
            //    result = "001/PLHHH-" + DateTime.Now.Year.ToString().Substring(2, 2);
            //else
            //    result = genericNumberDoc(rsSQL.sct, 3) + "/PLHHH-" + DateTime.Now.Year.ToString().Substring(2, 2);

            return result;
        }

    }

}