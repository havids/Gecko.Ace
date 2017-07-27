using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Gecko.Security.Util
{
    /// <summary>
    /// �ַ������ܽ����ࡣ
    /// </summary>
    public sealed class StringSecurity
    {
        private StringSecurity() { }

        #region SHA1 ����

        /// <summary>
        /// ʹ��SHA1�����ַ�����
        /// </summary>
        /// <param name="inputString">�����ַ�����</param>
        /// <returns>���ܺ���ַ�������40���ַ���</returns>
        public static string StringToSHA1Hash(string inputString)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] encryptedBytes = sha1.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }

        #endregion

        #region DES ����/����

        private static byte[] key = ASCIIEncoding.ASCII.GetBytes("caikelun");
        private static byte[] iv = ASCIIEncoding.ASCII.GetBytes("12345678");

        /// <summary>
        /// DES���ܡ�
        /// </summary>
        /// <param name="inputString">�����ַ�����</param>
        /// <returns>���ܺ���ַ�����</returns>
        public static string DESEncrypt(string inputString)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StreamWriter sw = null;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                sw = new StreamWriter(cs);
                sw.Write(inputString);
                sw.Flush();
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            finally
            {
                if (sw != null) sw.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
        }

        /// <summary>
        /// DES���ܡ�
        /// </summary>
        /// <param name="inputString">�����ַ�����</param>
        /// <returns>���ܺ���ַ�����</returns>
        public static string DESDecrypt(string inputString)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StreamReader sr = null;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                ms = new MemoryStream(Convert.FromBase64String(inputString));
                cs = new CryptoStream(ms, des.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            finally
            {
                if (sr != null) sr.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
        }

        #endregion

    }
}
