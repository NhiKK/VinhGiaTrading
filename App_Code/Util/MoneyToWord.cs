using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MoneyToWord
/// </summary>
public static class MoneyToWord
{
    public static string ConvertMoneyToText(string value)
    {
        value = value.Replace(",", "").Replace("$", "");
        int decimalCount = 0;
        int Val = value.Length - 1;
        for (int x = 0; x <= Val; x++)
        {
            char Val2 = value[x];
            if (Val2.ToString() == ".")
            {
                decimalCount++;
                if (decimalCount > 1)
                {
                    throw new ArgumentException("Only monetary values are accepted");
                }
            }
            Val2 = value[x];
            char Valtemp = value[x];
            if (!(char.IsDigit(value[x]) | (Val2.ToString() == ".")) & !((x == 0) & (Valtemp.ToString() == "-")))
            {
                throw new ArgumentException("Only monetary values are accepted");
            }
        }
        string returnValue = "";
        string[] parts;
        if (value.Contains("."))
            parts = value.Split(new char[] { '.' });
        else
            parts = (value + ".00").Split(new char[] { '.' });


        parts[1] = new string((parts[1] + "00").Substring(0, 2).ToCharArray());
        bool IsNegative = parts[0].Contains("-");
        if (parts[0].Replace("-", "").Length > 0x12)
        {
            throw new ArgumentException("Maximum value is $999,999,999,999,999,999.99");
        }
        if (IsNegative)
        {
            parts[0] = parts[0].Replace("-", "");
            returnValue = returnValue + "Minus ";
        }
        if (parts[0].Length > 15)
        {
            returnValue = ((((returnValue + HundredsText(parts[0].PadLeft(0x12, '0').Substring(0, 3)) + "Quadrillion ")
                + HundredsText(parts[0].PadLeft(0x12, '0').Substring(3, 3)) + "Trillion ") +
                HundredsText(parts[0].PadLeft(0x12, '0').Substring(6, 3)) + "Billion ") +
                HundredsText(parts[0].PadLeft(0x12, '0').Substring(9, 3)) + "Million ") +
                HundredsText(parts[0].PadLeft(0x12, '0').Substring(12, 3)) + "Thousand ";
        }
        else if (parts[0].Length > 12)
        {
            returnValue = (((returnValue + HundredsText(parts[0].PadLeft(15, '0').Substring(0, 3)) +
                "Trillion ") + HundredsText(parts[0].PadLeft(15, '0').Substring(3, 3)) + "Billion ") +
                HundredsText(parts[0].PadLeft(15, '0').Substring(6, 3)) + "Million ") +
                HundredsText(parts[0].PadLeft(15, '0').Substring(9, 3)) + "Thousand ";
        }
        else if (parts[0].Length > 9)
        {
            returnValue = ((returnValue + HundredsText(parts[0].PadLeft(12, '0').Substring(0, 3)) +
                "Billion ") + HundredsText(parts[0].PadLeft(12, '0').Substring(3, 3)) + "Million ") +
                HundredsText(parts[0].PadLeft(12, '0').Substring(6, 3)) + "Thousand ";
        }
        else if (parts[0].Length > 6)
        {
            returnValue = (returnValue + HundredsText(parts[0].PadLeft(9, '0').Substring(0, 3)) +
                "Million ") + HundredsText(parts[0].PadLeft(9, '0').Substring(3, 3)) + "Thousand ";
        }
        else if (parts[0].Length > 3)
        {
            returnValue = returnValue + HundredsText(parts[0].PadLeft(6, '0').Substring(0, 3)) +
                "Thousand ";
        }
        string hundreds = parts[0].PadLeft(3, '0');
        int tempInt = 0;
        hundreds = hundreds.Substring(hundreds.Length - 3, 3);
        if (int.TryParse(hundreds, out tempInt) == true)
        {
            if (int.Parse(hundreds) < 100)
            {
                //returnValue = returnValue + "and ";
            }
            returnValue = returnValue + HundredsText(hundreds) + "Dollar";
            if (int.Parse(hundreds) != 1)
            {
                returnValue = returnValue + "s";
            }
            if (int.Parse(parts[1]) != 0)
            {
                returnValue = returnValue + " and ";
            }
        }
        if ((parts.Length == 2) && (int.Parse(parts[1]) != 0))
        {
            if (returnValue != "")
            {
                string cent = "Cent";
                if (int.Parse(parts[1]) != 1)
                {
                    cent += "s";
                    //returnValue = returnValue + "s";
                }

                returnValue = returnValue + " " + cent + " " + HundredsText(parts[1].PadLeft(3, '0'));
            }
        }

        if (value == "0")
            returnValue = "Zero " + returnValue;
        return returnValue;
    }


    static string[] Tens = new string[] {
            "Ten",
            "Twenty",
            "Thirty",
            "Forty",
            "Fifty",
            "Sixty",
            "Seventy",
            "Eighty",
            "Ninety" };
    static string[] Ones = new string[] {
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Ten",
            "Eleven",
            "Twelve",
            "Thirteen",
            "Fourteen",
            "Fifteen",
            "Sixteen",
            "Seventeen",
            "Eighteen",
            "Nineteen" };



    private static string HundredsText(string value)
    {
        char Val_1;
        char Val_2;

        string returnValue = "";
        bool IsSingleDigit = true;
        char Val = value[0];
        if (int.Parse(Val.ToString()) != 0)
        {
            Val_1 = value[0];
            returnValue = returnValue + Ones[int.Parse(Val_1.ToString()) - 1] + " Hundred ";
            IsSingleDigit = false;
        }
        Val_1 = value[1];
        if (int.Parse(Val_1.ToString()) > 1)
        {
            Val = value[1];
            returnValue = returnValue + Tens[int.Parse(Val.ToString()) - 1] + " ";
            Val_1 = value[2];
            if (int.Parse(Val_1.ToString()) != 0)
            {
                Val = value[2];
                returnValue = returnValue + Ones[int.Parse(Val.ToString()) - 1] + " ";
            }
            return returnValue;
        }
        Val_1 = value[1];
        if (int.Parse(Val_1.ToString()) == 1)
        {
            Val = value[1];
            Val_2 = value[2];
            return (returnValue + Ones[int.Parse(Val.ToString() + Val_2.ToString()) - 1] + " ");
        }
        Val_2 = value[2];
        if (int.Parse(Val_2.ToString()) == 0)
        {
            return returnValue;
        }
        if (!IsSingleDigit)
        {
            returnValue = returnValue + "and ";
        }
        Val_2 = value[2];
        return (returnValue + Ones[int.Parse(Val_2.ToString()) - 1] + " ");
    }

    public static string ConvertMoneyToTextVND(decimal number)
    {
        string s = number.ToString("#");
        string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
        int i, j, donvi, chuc, tram;
        string str = " ";
        bool booAm = false;
        decimal decS = 0;
        //Tung addnew
        try
        {
            decS = Convert.ToDecimal(s.ToString());
        }
        catch
        {
        }
        if (decS < 0)
        {
            decS = -decS;
            s = decS.ToString();
            booAm = true;
        }
        i = s.Length;
        if (i == 0)
            str = so[0] + str;
        else
        {
            j = 0;
            while (i > 0)
            {
                donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                i--;
                if (i > 0)
                    chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    chuc = -1;
                i--;
                if (i > 0)
                    tram = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    tram = -1;
                i--;
                if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                    str = hang[j] + str;
                j++;
                if (j > 3) j = 1;
                if ((donvi == 1) && (chuc > 1))
                    str = "một " + str;
                else
                {
                    if ((donvi == 5) && (chuc > 0))
                        str = "lăm " + str;
                    else if (donvi > 0)
                        str = so[donvi] + " " + str;
                }
                if (chuc < 0)
                    break;
                else
                {
                    if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                    if (chuc == 1) str = "mười " + str;
                    if (chuc > 1) str = so[chuc] + " mươi " + str;
                }
                if (tram < 0) break;
                else
                {
                    if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                }
                str = " " + str;
            }
        }
        if (booAm) str = "Âm " + str;
        return str + "chẵn";
    }

    public static string ConvertMoneyToTextVND(decimal number, string dtkd)
    {
        string textMoney = "";
        if (new string[] { "VINHGIA" }.Contains(dtkd))
        {
            string m = MoneyToWord.ConvertMoneyToText(number.ToString()).Replace("Dollars", "");
            if (number >= (decimal)0.000 & number <= (decimal)0.009)
                m = "Zero " + m;
            int j = m.LastIndexOf("and");

            if (m.Contains("Cents") | m.Contains("Cent"))
            {
                //m = m.Replace("Cents", "").Replace("Cent", "");
                //m = m.Insert(m.Length, "cents");
            }
            else
            {
                m = m.Replace("Cents", "").Replace("Cent", "");
            }
            textMoney = m;
        }
        else
        {
            textMoney = MoneyToWord.ConvertMoneyToTextVND(number);
        }
        return textMoney;
    }
}
