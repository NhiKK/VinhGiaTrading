using System;
using System.Collections.Generic;
using System.Web;
using System.Globalization;

/// <summary>
/// Summary description for CultureInfoUtil
/// </summary>
public static class CultureInfoUtil
{

    public static string currency(decimal value, int countryCode, Int32 currencyDecimalDigits)
    {
        String str = "";
        CultureInfo cul = new CultureInfo(countryCode);
        NumberFormatInfo fmat = cul.NumberFormat;
        fmat.CurrencySymbol = "";
        fmat.CurrencyDecimalDigits = currencyDecimalDigits;
        if (value != 0 && value != null)
        {
            str = value.ToString("c", fmat);
        }
        return str;
    }

    public static string currencyUSD(object value)
    {
        return String.Format("{0:#,0.00}", value);
    }

    public static string currencyUSD(String value)
    {
        return String.Format("{0:#,0.00}", value);
    }

    public static string currencyUSD(decimal value)
    {
        return String.Format("{0:#,0.00}", value);
    }

    public static string currencyVND(object value)
    {
        return currency((decimal)value, 1066, 0);
    }

    public static string currencyVND(String value)
    {
        return currency(decimal.Parse(value), 1066, 0);
    }

    public static string currencyVND(decimal value)
    {
        return currency(value, 1066, 0);
    }

    public static string currencyVND2Digits(decimal value)
    {
        return currency(value, 1066, 2);
    }
}
