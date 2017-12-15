using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace Anxin.Common
{
    public class IPHelper
    {
        public static long IPToLong(string strIP)
        {
            byte[] ip_bytes = new byte[8];
            string[] strArr = strIP.Split(new char[] { '.' });
            for (int i = 0; i < 4; i++)
            {
                ip_bytes[i] = byte.Parse(strArr[3 - i]);
            }
            return BitConverter.ToInt64(ip_bytes, 0);
        }

        public static long IpToLong2(string ipAddress)
        {
            //将目标IP地址字符串strIPAddress转换为数字  
            string[] arrayIP = ipAddress.Split('.');
            long sip1 = Int64.Parse(arrayIP[0]);
            long sip2 = Int64.Parse(arrayIP[1]);
            long sip3 = Int64.Parse(arrayIP[2]);
            long sip4 = Int64.Parse(arrayIP[3]);

            long r1 = sip1 * 256 * 256 * 256;
            long r2 = sip2 * 256 * 256;
            long r3 = sip3 * 256;
            long r4 = sip4;

            long result = r1 + r2 + r3 + r4;
            return result;
        }

    }


}
