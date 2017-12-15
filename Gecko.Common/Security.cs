using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Anxin.Common;

namespace AiChou.Common
{
    public class Security
    {
        static string publickey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        static string privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";

        public static string UnicodeDecode(string s)
        {
            Regex reUnicode = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);
            return reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, null, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
        }


        //public static bool CheckSign(string parms, string sign)
        //{
        //    string _sign = MD5(parms + AiChou.Common.Constant.AiChouSignKey);
        //    return _sign == sign;
        //}

        //public static string GetSign(string parms)
        //{
        //    return MD5(parms + AiChou.Common.Constant.AiChouSignKey);
        //}

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns>f527e75a9cf89ecb23a32fa3e8b2c650</returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.Default.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return BitConverter.ToString(resultArray).Replace("-", "").ToLower();

            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">f527e75a9cf89ecb23a32fa3e8b2c650</param>
        /// <param name="key">16λ</param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            List<string> list = new List<string>();
            for (int i = 0; i < str.Length; i = i + 2)
            {
                list.Add(str.Substring(i, 2));
            }
            string a4 = (string.Join("-", list.ToArray()));
            String[] arr2 = a4.ToUpper().Split('-');
            byte[] toEncryptArray = new byte[arr2.Length];
            for (int i = 0; i < arr2.Length; i++)
                toEncryptArray[i] = Convert.ToByte(arr2[i], 16);

            //Byte[] toEncryptArray = Convert.FromBase64String(content);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        /// <summary>
        /// AES256����
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AES256Encrypt(string toEncrypt, string key)
        {
            if (string.IsNullOrEmpty(toEncrypt) || string.IsNullOrEmpty(key))
            {
                return null;
            }
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cryptoTransform = rm.CreateEncryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray);
        }
        /// <summary>
        /// AES256����
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AES256Decrypt(string toDecrypt, string key)
        {
            if (string.IsNullOrEmpty(toDecrypt) || string.IsNullOrEmpty(key))
            {
                return null;
            }
            string toDecryptRaw = string.Empty;
            try
            {
                var toDecryptArray = Convert.FromBase64String(toDecrypt);
                RijndaelManaged rm = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                var cryptoTransform = rm.CreateDecryptor();
                var resultArray = cryptoTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                toDecryptRaw = Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                LOG.Error(LOG.ST.Day, "AES256", ex.StackTrace);
            }

            return toDecryptRaw;
        }

        /// <summary>
        /// RSA����
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA����
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSADecrypt(string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

        public static bool RSAVerify(string str_DataToVerify, string str_SignedData)
        {
            byte[] SignedData = Convert.FromBase64String(str_SignedData);

            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToVerify = ByteConverter.GetBytes(str_DataToVerify);
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(publickey);

                return rsa.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }


        /// <summary>
        /// MD5����2
        /// </summary>
        /// <param name="str">ԭʼ�ַ���</param>
        /// <returns>MD5���</returns>
        public static string MD5(string input)
        {
            return MD5(input, Encoding.Default);
        }

        /// <summary>
        /// MD5����2
        /// </summary>
        /// <param name="str">ԭʼ�ַ���</param>
        /// <returns>MD5���</returns>
        public static string MD5(string input, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(input);
            data = new MD5CryptoServiceProvider().ComputeHash(data);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        //Ĭ����Կ����
        private static byte[] _iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        //Ĭ����Կ
        private static string _key = "Wwinti%($ever";
        private static string _decKey = "6o(^;^)o";

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="input">�����ܵ��ַ���</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ���,ʧ�ܷ���Դ��</returns>
        public static string Encode(string input)
        {
            return Encode(input, _key);
        }

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="input">�����ܵ��ַ���</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���,ʧ�ܷ�Դ��</returns>
        public static string Decode(string input)
        {
            return Decode(input, _key);
        }

        /// <summary>
        /// Ĭ�ϼ򵥼���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DefaultEncode(string str)
        {
            return Common.Security.EncodeDec(str, "!))*^123", "%%&%()&!");
        }

        /// <summary>
        /// Ĭ�ϼ򵥽���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DefaultDecode(string str)
        {
            return Common.Security.DecodeDec(str, "!))*^123", "%%&%()&!");
        }
        /// <summary>
        /// DEC����
        /// </summary>
        /// <param name="input">�����ܵ��ַ���</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ���</returns>
        public static string EncodeDec(string input)
        {
            return EncodeDec(input, _decKey, _decKey);
        }

        /// <summary>
        /// DEC����
        /// </summary>
        /// <param name="input"><�����ܵ��ַ���/param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���</returns>
        public static string DecodeDec(string input)
        {
            return DecodeDec(input, _decKey, _decKey);
        }


        /// <summary>
        /// DEC ���ܹ��� 
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <param name="sIV"></param>
        /// <returns></returns>
        public static string EncodeDec(string pToEncrypt, string sKey, string sIV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider(); //���ַ����ŵ�byte������ 

            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            des.Key = Encoding.ASCII.GetBytes(sKey); //�������ܶ������Կ��ƫ���� 
            des.IV = Encoding.ASCII.GetBytes(sIV); //ԭ��ʹ��ASCIIEncoding.ASCII������GetBytes���� 
            MemoryStream ms = new MemoryStream(); //ʹ�����������������Ӣ���ı� 
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        /// <summary>
        /// DEC ���ܹ��� 
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <param name="sIV"></param>
        /// <returns></returns>
        public static string DecodeDec(string pToDecrypt, string sKey, string sIV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.ASCII.GetBytes(sKey); //�������ܶ������Կ��ƫ��������ֵ��Ҫ�������޸� 
            des.IV = Encoding.ASCII.GetBytes(sIV);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            ms.Close();

            StringBuilder ret = new StringBuilder(); //����StringBuild����CreateDecryptʹ�õ��������󣬱���ѽ��ܺ���ı���������� 

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="input">�����ܵ��ַ���</param>
        /// <param name="key">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ���,ʧ�ܷ���Դ��</returns>
        public static string Encode(string input, string key)
        {
            key = (key + "        ").Substring(0, 8);

            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = _iv;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            return Convert.ToBase64String(mStream.ToArray());

        }

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="input">�����ܵ��ַ���</param>
        /// <param name="key">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���,ʧ�ܷ�Դ��</returns>
        public static string Decode(string input, string key)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            try
            {
                key = (key + "        ").Substring(0, 8);

                byte[] rgbKey = Encoding.UTF8.GetBytes(key);
                byte[] rgbIV = _iv;
                byte[] inputByteArray = Convert.FromBase64String(input);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return input;
            }

        }
        public static string EncryptDES(string source)
        {
            return new Security().EncryptDES(source, "!))*^123", "%%&%()&!");
        }
        public static string DecryptDES(string encode)
        {
            return new Security().DecryptDES(encode, "!))*^123", "%%&%()&!");
        }
        /// <summary>
        /// ���ַ�������DES����
        /// </summary>
        /// <param name="sourceString">�����ܵ��ַ���</param>
        /// <returns>���ܺ��BASE64������ַ���</returns>
        public string EncryptDES(string sourceString, string key, string iv)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIV = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// ��DES���ܺ���ַ������н���
        /// </summary>
        /// <param name="encryptedString">�����ܵ��ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
        public string DecryptDES(string encryptedString, string key, string iv)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIV = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// use sha1 to encrypt string
        /// </summary>
        public static string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }


        public string TripleDesEncrypt(string source, string keystr)
        {
            byte[] data = Encoding.UTF8.GetBytes(source);
            byte[] key = GetByteKey(keystr);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Padding = PaddingMode.PKCS7;

            //Decrypt
            Type t = Type.GetType("System.Security.Cryptography.CryptoAPITransformMode");
            object obj = t.GetField("Encrypt", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).GetValue(t);

            MethodInfo mi = des.GetType().GetMethod("_NewEncryptor", BindingFlags.Instance | BindingFlags.NonPublic);
            ICryptoTransform desCrypt = (ICryptoTransform)mi.Invoke(des, new object[] { key, CipherMode.ECB, null, 0, obj });

            byte[] result = desCrypt.TransformFinalBlock(data, 0, data.Length);

            return BitConverter.ToString(result).Replace("-", "");
        }

        public string TripleDesDecrypt(string source, string keystr)
        {
            byte[] data = Encoding.UTF8.GetBytes(source);
            byte[] key = GetByteKey(keystr);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Padding = PaddingMode.PKCS7;

            //Decrypt
            Type t = Type.GetType("System.Security.Cryptography.CryptoAPITransformMode");
            object obj = t.GetField("Decrypt", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).GetValue(t);

            MethodInfo mi = des.GetType().GetMethod("_NewEncryptor", BindingFlags.Instance | BindingFlags.NonPublic);
            ICryptoTransform desCrypt = (ICryptoTransform)mi.Invoke(des, new object[] { key, CipherMode.ECB, null, 0, obj });

            byte[] result = HexStringToByte(source);

            result = desCrypt.TransformFinalBlock(result, 0, result.Length);
            return Encoding.UTF8.GetString(result);
        }

        private byte[] HexStringToByte(string hex)
        {
            hex = hex.ToUpper();
            int len = (hex.Length / 2);
            byte[] result = new byte[len];
            char[] achar = hex.ToCharArray();
            for (int i = 0; i < len; i++)
            {
                int pos = i * 2;
                result[i] = (byte)(HexToByte(achar[pos]) << 4 | HexToByte(achar[pos + 1]));
            }
            return result;
        }
        private byte HexToByte(char c)
        {
            byte b = (byte)"0123456789ABCDEF".IndexOf(c);
            return b;
        }


        private byte[] GetByteKey(string keyStr)
        {
            byte[] key = new byte[24];
            byte[] temp = Encoding.UTF8.GetBytes(keyStr);

            if (key.Length > temp.Length)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    key[i] = temp[i];
                }

            }
            else
            {
                for (int i = 0; i < key.Length; i++)
                {
                    key[i] = temp[i];
                }
            }
            return key;
        }

        #region BASE64
        /// <summary>
        /// ����base64json��ʽ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static sbyte[] byteToSbyte(byte[] data)
        {
            sbyte[] bSByte = new sbyte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > 127)
                    bSByte[i] = (sbyte)(data[i] - 256);
                else
                    bSByte[i] = (sbyte)data[i];
            }
            return bSByte;
        }
        /// <summary>
        /// ��������Base64����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64EncodeBySbyte(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                sbyte[] encData_sbyte = byteToSbyte(encData_byte);
                byte[] y = (byte[])(object)encData_sbyte;
                string encodedData = Convert.ToBase64String(y);
                return encodedData;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// ��������Base64����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// ��������Base64����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(string data, Encoding encod)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = encod.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// ��������Base64����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Base64EncodeTobyte(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                return encData_byte;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// ��������Base64����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(byte[] data)
        {
            try
            {
                string encodedData = Convert.ToBase64String(data);
                return encodedData;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string FileBase64Encode(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new Exception("�ļ�������");
            }
            byte[] filebytes = File.ReadAllBytes(filepath);
            return Base64Encode(filebytes);
        }

        public static byte[] FileBase64Decode(string encode)
        {
            return Base64Decode(encode, "byte");
        }

        /// <summary>
        /// ��������Base64����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Decode(string data)
        {
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
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// ��������Base64����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Base64Decode(string data, string type = "byte")
        {
            try
            {
                return Convert.FromBase64String(data);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion BASE64 

        /// <summary>
        /// HMACSHA256ǩ��,����16���Ʊ����ַ���
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string HMACSHA256(string key, string source)
        {
            string result = string.Empty;
            using (KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create("HmacSHA256"))
            {
                kha.Key = Encoding.Default.GetBytes(key);
                var bytes = kha.ComputeHash(Encoding.Default.GetBytes(source));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.AppendFormat("{0:x2}", b);
                }
                result = builder.ToString();
            }
            return result;
        }

    }


}