using System;
using System.Collections.Generic;
using System.Text;

namespace Gecko.Common
{
    /// <summary>
    /// 用于对时间类型的工具类
    /// </summary>
    public class TimeHelper
    {
        private static DateTime _timeStamp = new DateTime(1970, 1, 1, 8, 0, 0);
        public readonly static DateTime TIME_DEFAULT = new DateTime(1900, 1, 1);
        public static Dictionary<int, string> MonthChineseNameDic = new Dictionary<int, string>()
        {
            { 1, "一月" },
            { 2, "二月" },
            { 3, "三月" },
            { 4, "四月" },
            { 5, "五月" },
            { 6, "六月" },
            { 7, "七月" },
            { 8, "八月" },
            { 9, "九月" },
            { 10, "十月" },
            { 11, "十一月" },
            { 12, "十二月" },

        };


        /// <summary>
        /// 返回当前时间的时间戳
        /// </summary>
        public static Int64 NowStamp
        {
            get
            {
                return GetTimeStamp(DateTime.Now);
            }
        }

        /// <summary>
        /// 返回当前时间戳的时间类型
        /// </summary>
        /// <param name="stamp">long型的数字时间戳</param>
        /// <returns>时间</returns>
        public static DateTime GetDateTime(Int64 stamp)
        {
            return _timeStamp.AddSeconds(stamp);
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="d">目标时间</param>
        /// <returns>时间戳</returns>
        public static Int64 GetTimeStamp(DateTime d)
        {
            TimeSpan ts = d - _timeStamp;

            return (Int64)ts.TotalSeconds;
        }

        /// <summary>
        /// 返回当前时间的时间戳
        /// </summary>
        public static Int64 NowStampMilliseconds
        {
            get
            {
                return GetTimeStampMilliseconds(DateTime.Now);
            }
        }
        /// <summary>
        /// 返回当前时间戳的时间类型
        /// </summary>
        /// <param name="stamp">long型的数字时间戳</param>
        /// <returns>时间</returns>
        public static DateTime GetDateTimeMilliseconds(Int64 stamp)
        {
            return _timeStamp.AddMilliseconds(stamp);
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="d">目标时间</param>
        /// <returns>时间戳</returns>
        public static Int64 GetTimeStampMilliseconds(DateTime d)
        {
            TimeSpan ts = d - _timeStamp;

            return (Int64)ts.TotalMilliseconds;
        }


        /// <summary>
        /// 据起始时间和年龄返回出生日期
        /// </summary>
        /// <param name="start">时间</param>
        /// <param name="age">年龄</param>
        /// <returns>生日</returns>
        public static DateTime GetBirthDay(DateTime start, int age)
        {
            return DateTime.Parse(start.AddYears(age * -1).ToShortDateString());
        }

        /// <summary>
        /// 返回生日
        /// </summary>
        /// <param name="age">年龄</param>
        /// <returns>出生日期</returns>
        public static DateTime GetBirthDay(int age)
        {
            return GetBirthDay(DateTime.Now, age);
        }

        /// <summary>
        /// 转化Object为DateTime，如果转换失败则返回当前时间
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(object d)
        {
            return ParseDateTime(d, TIME_DEFAULT);
        }

        public static DateTime ParseDateTime(object d,DateTime def)
        {
            if (d != null)
            {
                try
                {
                    return DateTime.Parse(d.ToString());
                }
                catch { }
            }
            return def;
        }

        /// <summary>
        /// 获取生日对应的星座
        /// </summary>
        /// <param name="birth">生日</param>
        /// <returns>星座</returns>
        public static string GetAstrology(DateTime birth)
        {
            int y = birth.Year;
            //白羊座    3月21日-4月19日
            if (birth >= new DateTime(y, 3, 21) && birth <= new DateTime(y, 4, 19)) return "白羊座";
            else if (birth >= new DateTime(y, 4, 20) && birth <= new DateTime(y, 5, 20)) return "金牛座";
            else if (birth >= new DateTime(y, 5, 21) && birth <= new DateTime(y, 6, 21)) return "双子座";
            else if (birth >= new DateTime(y, 6, 22) && birth <= new DateTime(y, 7, 22)) return "巨蟹座";
            else if (birth >= new DateTime(y, 7, 23) && birth <= new DateTime(y, 8, 22)) return "狮子座";
            else if (birth >= new DateTime(y, 8, 23) && birth <= new DateTime(y, 9, 22)) return "处女座";
            else if (birth >= new DateTime(y, 9, 23) && birth <= new DateTime(y, 10, 23)) return "天枰座";
            else if (birth >= new DateTime(y, 10, 24) && birth <= new DateTime(y, 11, 22)) return "天蝎座";
            else if (birth >= new DateTime(y, 11, 23) && birth <= new DateTime(y, 12, 21)) return "射手座";
            else if (birth >= new DateTime(y, 12, 22) && birth <= new DateTime(y, 12, 31)) return "摩羯座";      //跨年
            else if (birth >= new DateTime(y, 1, 1) && birth <= new DateTime(y, 1, 19)) return "摩羯座";
            else if (birth >= new DateTime(y, 1, 20) && birth <= new DateTime(y, 2, 18)) return "水瓶座";
            else if (birth >= new DateTime(y, 2, 19) && birth <= new DateTime(y, 3, 20)) return "双鱼座";
            else
                return "";
        }

        /// <summary>
        /// 获取指定日期那周的周一日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfTheWeek(DateTime date)
        {
            int dateIndex = ((int)date.DayOfWeek + 6) % 7;
            return date.AddDays(-dateIndex);
        }

        /// <summary>
        /// 返回字符串的 yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TimeFromater(string s)
        {
            try
            {
                return Convert.ToDateTime(s).ToString("yyyy-MM-dd HH:mm");
            }
            catch
            {
                return s;
            }
        }

        /// <summary>
        /// 计算方法: thisTime - thatTime, 如果 thisTime < thatTime, 则返回刚刚
        /// </summary>
        /// <param name="thisTime"></param>
        /// <param name="thatTime"></param>
        /// <returns></returns>
        public static string FormatDateDiff(DateTime thisTime, DateTime thatTime)
        {
            var diff = thisTime - thatTime;

            var diffDay = diff.Days;

            if (diffDay > 365)
            {
                return (diffDay / 365).ToString() + "年前";
            }
            else if (diffDay > 30)
            {
                var month = diffDay / 30;

                if (month > 11)
                {
                    return "1年前";
                }
                return month.ToString() + "个月前";
            }
            else if (diffDay > 0) 
            {
                return diffDay.ToString() + "天前";
            }
            else
            {
                var diffHour = diff.Hours;

                if (diffHour > 0)
                {
                    return diffHour.ToString() + "小时前";
                }
                else
                {
                    var diffMin = diff.Minutes;

                    if (diffMin > 0)
                    {
                        return diffMin.ToString() + "分钟前";
                    }
                }
            }

            return "刚刚";
        }

    }
}
