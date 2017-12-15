using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Anxin.Common
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public class FileHelper
    {
        public FileHelper() { }


 


        public static System.Text.Encoding GetType(string path)
        {
            //if (!File.Exists(path)) return System.Text.Encoding.Default;

            FileStream fs = null;
            try 
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                System.Text.Encoding r = GetType(fs);

                return r;
            }
            catch { }
            finally
            {
                fs.Close();
                fs.Dispose();
            }

            return System.Text.Encoding.Default;

        }
        public static void Delete(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public static System.Text.Encoding GetType(FileStream fs)
        {

            BinaryReader r = null;

            byte[] ss = null;

            try
            {
                r = new BinaryReader(fs, System.Text.Encoding.Default);
                ss = r.ReadBytes((int)(fs.Length < 1000 ? fs.Length : 1000));
            }
            catch { }
            finally
            {
                r.Close();
            }



            //编码类型 Coding=编码类型.ASCII;  
            if (ss[0] >= 0xEF)
            {
                if (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)
                {
                    return System.Text.Encoding.UTF8;
                }
                else if (ss[0] == 0xFE && ss[1] == 0xFF)
                {
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (ss[0] == 0xFF && ss[1] == 0xFE)
                {
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    return System.Text.Encoding.Default;
                }
            }
            else
            {
                return GetNoBomType(ss, ss.Length);
            }


        }

        // 针对无BOM的内容做判断,不是分准确--http://www.joymo.cn
        public static System.Text.Encoding GetNoBomType(byte[] buf, int len)
        {
            byte chr;

            for (int i = 0; i < len; i++)
            {
                chr = buf[i];

                if (chr >= 0x80)
                {
                    if ((chr & 0xf0) == 0xe0)
                    {
                        i++;
                        chr = buf[i];
                        if ((chr & 0xc0) == 0x80)
                        {
                            i++;
                            chr = buf[i];
                            if ((chr & 0xc0) == 0x80)
                                return System.Text.Encoding.UTF8;
                            else return System.Text.Encoding.Default;
                        }
                        else
                            return System.Text.Encoding.Default;
                    }
                    else
                        return System.Text.Encoding.Default;
                }
            }
            return System.Text.Encoding.Default;
        }

        public static string ReadTextFileString(string filePath, System.Text.Encoding encode)
        {
            string strResult = string.Empty;

            StreamReader sr = null;

            try
            {
                if (File.Exists(filePath))
                {
                    sr = new StreamReader(filePath, encode);
                    strResult = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                strResult = ex.ToString();
            }

            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }

            return strResult;
        }

        public static string ReadTextFileString(string filePath)
        {
            return ReadTextFileString(filePath, System.Text.Encoding.Default);
        }
        /// <summary>
        /// 写文本文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="strContent">文件内容</param>
        /// <returns></returns>
        public static bool WriteTextFile(string filePath, string strContent, bool append, System.Text.Encoding encode)
        {
            StreamWriter sw = null;
            try
            {
                if (!File.Exists(filePath))
                {
                    FileInfo fi = new FileInfo(filePath);
                    Directory.CreateDirectory(fi.DirectoryName);
                }

                sw = new StreamWriter(filePath, append, encode);

                sw.Write(strContent);


            }
            catch
            {
                return false;
            }

            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

            return true;
        }

        public static bool WriteTextFile(string filePath, string strContent, bool append)
        {
            return WriteTextFile(filePath, strContent, append, System.Text.Encoding.Default);
        }

        public static bool WriteLineTextFile(string fullName, string strContent, System.Text.Encoding encoder)
        {
            StreamWriter sw = null;
            try
            {

                if (!File.Exists(fullName))
                {
                    FileInfo fi = new FileInfo(fullName);
                    Directory.CreateDirectory(fi.DirectoryName);
                }
                sw = new StreamWriter(fullName, true, encoder);
                sw.WriteLine(strContent);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

            return true;
        }
        public static string GetFileName(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            return fi.Name;
        }


        public static bool CreateDirectory(string path)
        {
            string pattern = @"[^\\]+\.[\.\w]+$";

            path = Regex.Replace(path, pattern, "", RegexOptions.IgnoreCase);

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 返回文件名中的扩展名
        /// </summary>
        /// <param name="fileName">文件名或Url或文件路径</param>
        /// <returns>扩展名</returns>
        public static string GetExtention(string fileName)
        {
            string ext = Regex.Replace(fileName, @"(\?|#).*", "");

            ext = Regex.Replace(ext, @".*\.", "");

            return ext.Trim();
        }



        /// <summary>
        /// 计算文件的 MD5 值
        /// </summary>
        /// <param name="fileName">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string MD5File(string fileName)
        {
            return HashFile(fileName, "md5");
        }

        /// <summary>
        /// 计算文件的 sha1 值
        /// </summary>
        /// <param name="fileName">要计算 sha1 值的文件名和路径</param>
        /// <returns>sha1 值16进制字符串</returns>
        public static string SHA1File(string fileName)
        {
            return HashFile(fileName, "sha1");
        }

        /// <summary>
        /// 计算文件的哈希值
        /// </summary>
        /// <param name="fileName">要计算哈希值的文件名和路径</param>
        /// <param name="algName">算法:sha1,md5</param>
        /// <returns>哈希值16进制字符串</returns>
        private static string HashFile(string fileName, string algName)
        {
            if (!System.IO.File.Exists(fileName))
                return string.Empty;

            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] hashBytes = HashData(fs, algName);
            fs.Close();
            return ByteArrayToHexString(hashBytes);
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="stream">要计算哈希值的 Stream</param>
        /// <param name="algName">算法:sha1,md5</param>
        /// <returns>哈希值字节数组</returns>
        private static byte[] HashData(System.IO.Stream stream, string algName)
        {
            System.Security.Cryptography.HashAlgorithm algorithm;
            if (algName == null)
            {
                throw new ArgumentNullException("algName 不能为 null");
            }
            if (string.Compare(algName, "sha1", true) == 0)
            {
                algorithm = System.Security.Cryptography.SHA1.Create();
            }
            else
            {
                if (string.Compare(algName, "md5", true) != 0)
                {
                    throw new Exception("algName 只能使用 sha1 或 md5");
                }
                algorithm = System.Security.Cryptography.MD5.Create();
            }
            return algorithm.ComputeHash(stream);
        }

        /// <summary>
        /// 字节数组转换为16进制表示的字符串
        /// </summary>
        private static string ByteArrayToHexString(byte[] buf)
        {
            return BitConverter.ToString(buf).Replace("-", "");
        }


        public static bool CheckFileSafe(string fileName)
        {
            var array = Anxin.Common.Logic.UploadSafeFileType;
            Regex regex = null;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                regex = new System.Text.RegularExpressions.Regex(array[i, 2], RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                if (regex.IsMatch(fileName))
                    return true;
            }
            return false;
        }

        public static int GetFileType(string fileName)
        {
            var array = Anxin.Common.Logic.UploadSafeFileType;
            Regex regex = null;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                regex = new System.Text.RegularExpressions.Regex(array[i, 2]);
                if (regex.IsMatch(fileName))
                    return Convert.ToInt32(array[i, 0]);
            }
            return 1;
        }


    }
}
