using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for clsEncryption
/// </summary>
public class clsEncryption
{
	public clsEncryption()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static byte[] Encryption(string PlainText, string key)
    {
        TripleDES des = CreateDES(key);
        ICryptoTransform ct = des.CreateEncryptor();
        byte[] input = Encoding.Unicode.GetBytes(PlainText);
        return ct.TransformFinalBlock(input, 0, input.Length);
    }
    public static string Decryption(string CypherText, string key)
    {
        CypherText = CypherText.Replace(' ', '+');
        byte[] b = Convert.FromBase64String(CypherText);
        TripleDES des = CreateDES(key);
        ICryptoTransform ct = des.CreateDecryptor();
        byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
        return Encoding.Unicode.GetString(output);
    }
    public static TripleDES CreateDES(string key)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
        des.IV = new byte[des.BlockSize / 8];
        return des;
    }
}