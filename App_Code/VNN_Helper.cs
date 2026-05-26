using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using System.IO;
using System.Data;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Reflection;
using System.Net;
using System.Text;

/// <summary>
/// Summary description for Extension
/// </summary>
public class VNN_Helper
{
    public LinqDBDataContext db { get; set; }
    public c_donhang dh { get; set; }
    public VNN_Helper() { }

    public bool laUSDHayVND()
    {
        return dh.dongtienISO != "VND";
    }
    public decimal layTongGiaTriDonHang(List<dongDonHangVNN> ddh)
    {
        decimal total = 0;
        if (dh.discount_hehang.GetValueOrDefault(false))
        {
            var items = ddh.Select(s => new { s.code_cl, s.c.discount }).Distinct().ToList();
            foreach (var item in items)
            {
                var totalHH =
                     ddh.Where(s => s.code_cl == item.code_cl)
                     .Sum(s => s.c.soluong.GetValueOrDefault(0) * UserUtils.getGiaHopDongPO(dh, s.c, 2, dh.phanbodiscount));

                if (dh.phanbodiscount.GetValueOrDefault(false))
                    total += totalHH;
                else
                    total += totalHH - decimal.Round(totalHH * (decimal)item.discount.GetValueOrDefault(0) / 100, 2, MidpointRounding.AwayFromZero);
            }
        }
        else
        {
            total = ddh.Sum(p => p.c.soluong.GetValueOrDefault(0) * UserUtils.getGiaHopDongPO(dh, p.c, 2, dh.phanbodiscount));
            if (!dh.phanbodiscount.GetValueOrDefault(false))
                total = total - decimal.Round(total * dh.discount.GetValueOrDefault(0) / 100, 2, MidpointRounding.AwayFromZero);
        }
        return total;
    }

    public string doiTienThanhChu(bool isUSD, decimal total)
    {
        string totalWord = "";
        if (isUSD)
        {
            total = decimal.Round(total, 2, MidpointRounding.AwayFromZero);
            totalWord = MoneyToWord.ConvertMoneyToText(total.ToString());
            if (totalWord.Contains("Dollars"))
            {
                totalWord = "USD " + totalWord.Replace("Dollars", "");
            }
        }
        else
        {
            totalWord = MoneyToWord.ConvertMoneyToTextVND(total);
        }
        return totalWord;
    }

    public class dongDonHangVNN
    {
        public c_dongdonhang c { get; set; }
        public string code_cl { get; set; }
    }
}