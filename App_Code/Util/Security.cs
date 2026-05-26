using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

public static class Security
{
    /// <summary>
    /// Permission For User
    /// </summary>
    /// <param name="manv">Ma Nhan Vien</param>
    /// <param name="url">URL</param>
    /// <returns>SqlDataReader [xem][them][xoa][sua][truyxuat]</returns>
    public static SqlDataReader getPermission(String manv, String url)
    {
        String sql = "select pq.xem as xem, pq.them as them, pq.xoa as xoa, " +
            " pq.sua as sua, pq.truyxuat as truyxuat " +
            " from nhanvien nv, vaitro vt, phanquyen pq, menu mnu " +
            " where nv.mavt = vt.mavt AND pq.mamenu = mnu.mamenu AND pq.mavt = vt.mavt " +
	        " AND nv.manv = @manv " +
	        " AND mnu.url = @url";
        return mdbc.ExecuteReader(sql,"@manv", manv, "@url", url);
    }

    public static string base64Decode(string data)
    {
        if (data == null) return "";
        try
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(data);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception("Error in base64Decode" + e.Message);
        }
    }

    public static string base64Encode(string data)
    {
        if (data == null) return "";
        try
        {
            byte[] encData_byte = new byte[data.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception e)
        {
            throw new Exception("Error in base64Encode" + e.Message);
        }
    }

    // Hash an input string and return the hash as
    // a 32 character hexadecimal string.
    public static string EncodeMd5Hash(string input)
    {
        // Create a new instance of the MD5CryptoServiceProvider object.
        MD5 md5Hasher = MD5.Create();

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    // Verify a hash against a string.
    public static bool verifyMd5Hash(string input, string hash)
    {
        // Hash the input.
        string hashOfInput = EncodeMd5Hash(input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        if (0 == comparer.Compare(hashOfInput, hash))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
