using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Gecko.Common
{
    public class FileTypeHelper
    {
        /// <summary>
        /// 检测文件真实 扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileExtension CheckTrueFileName(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string bx = " ";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                bx = buffer.ToString();
                buffer = r.ReadByte();
                bx += buffer.ToString();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            r.Close();
            fs.Close();
            int type = 0;
            int.TryParse(bx, out type);
            return (FileExtension)type;
        }



        /// <summary>
        /// 判断文件是否为安全文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool IsSafeFile(string filePath, params FileExtension[] exts)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;
            FileInfo f = new FileInfo(filePath);
            try
            {
                long length = f.Length;
            }
            catch
            {
                return false;
            }
            foreach (FileExtension fe in exts)
            {
                if (string.Compare("." + fe.ToString(), f.Extension, true) == 0 || CheckTrueFileName(f.FullName) == fe)
                {
                    return true;
                }
            }
            //判断不成功则返回false
            return false;
        }
    }

    public enum FileExtension
    {
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        COM = 7790,
        EXE = 7790,
        DLL = 7790,
        RAR = 8297,
        ZIP = 8075,
        XML = 6063,
        HTML = 6033,
        ASPX = 239187,
        CS = 117115,
        JS = 119105,
        TXT = 210187,
        SQL = 255254,
        BAT = 64101,
        BTSEED = 10056,
        RDP = 255254,
        PSD = 5666,
        PDF = 3780,
        CHM = 7384,
        LOG = 70105,
        REG = 8269,
        HLP = 6395,
        DOC = 208207,
        XLS = 208207,
        DOCX = 208207,
        XLSX = 208207,
    }

}
