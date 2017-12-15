using System;
using System.Collections.Generic;
using System.Text;

namespace Gecko.Common
{
    /// <summary>
    /// ���ڶ�ʱ�����͵Ĺ�����
    /// </summary>
    public class TimeHelper
    {
        private static DateTime _timeStamp = new DateTime(1970, 1, 1, 8, 0, 0);
        public readonly static DateTime TIME_DEFAULT = new DateTime(1900, 1, 1);
        public static Dictionary<int, string> MonthChineseNameDic = new Dictionary<int, string>()
        {
            { 1, "һ��" },
            { 2, "����" },
            { 3, "����" },
            { 4, "����" },
            { 5, "����" },
            { 6, "����" },
            { 7, "����" },
            { 8, "����" },
            { 9, "����" },
            { 10, "ʮ��" },
            { 11, "ʮһ��" },
            { 12, "ʮ����" },

        };


        /// <summary>
        /// ���ص�ǰʱ���ʱ���
        /// </summary>
        public static Int64 NowStamp
        {
            get
            {
                return GetTimeStamp(DateTime.Now);
            }
        }

        /// <summary>
        /// ���ص�ǰʱ�����ʱ������
        /// </summary>
        /// <param name="stamp">long�͵�����ʱ���</param>
        /// <returns>ʱ��</returns>
        public static DateTime GetDateTime(Int64 stamp)
        {
            return _timeStamp.AddSeconds(stamp);
        }

        /// <summary>
        /// ��ȡʱ���
        /// </summary>
        /// <param name="d">Ŀ��ʱ��</param>
        /// <returns>ʱ���</returns>
        public static Int64 GetTimeStamp(DateTime d)
        {
            TimeSpan ts = d - _timeStamp;

            return (Int64)ts.TotalSeconds;
        }

        /// <summary>
        /// ���ص�ǰʱ���ʱ���
        /// </summary>
        public static Int64 NowStampMilliseconds
        {
            get
            {
                return GetTimeStampMilliseconds(DateTime.Now);
            }
        }
        /// <summary>
        /// ���ص�ǰʱ�����ʱ������
        /// </summary>
        /// <param name="stamp">long�͵�����ʱ���</param>
        /// <returns>ʱ��</returns>
        public static DateTime GetDateTimeMilliseconds(Int64 stamp)
        {
            return _timeStamp.AddMilliseconds(stamp);
        }

        /// <summary>
        /// ��ȡʱ���
        /// </summary>
        /// <param name="d">Ŀ��ʱ��</param>
        /// <returns>ʱ���</returns>
        public static Int64 GetTimeStampMilliseconds(DateTime d)
        {
            TimeSpan ts = d - _timeStamp;

            return (Int64)ts.TotalMilliseconds;
        }


        /// <summary>
        /// ����ʼʱ������䷵�س�������
        /// </summary>
        /// <param name="start">ʱ��</param>
        /// <param name="age">����</param>
        /// <returns>����</returns>
        public static DateTime GetBirthDay(DateTime start, int age)
        {
            return DateTime.Parse(start.AddYears(age * -1).ToShortDateString());
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="age">����</param>
        /// <returns>��������</returns>
        public static DateTime GetBirthDay(int age)
        {
            return GetBirthDay(DateTime.Now, age);
        }

        /// <summary>
        /// ת��ObjectΪDateTime�����ת��ʧ���򷵻ص�ǰʱ��
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
        /// ��ȡ���ն�Ӧ������
        /// </summary>
        /// <param name="birth">����</param>
        /// <returns>����</returns>
        public static string GetAstrology(DateTime birth)
        {
            int y = birth.Year;
            //������    3��21��-4��19��
            if (birth >= new DateTime(y, 3, 21) && birth <= new DateTime(y, 4, 19)) return "������";
            else if (birth >= new DateTime(y, 4, 20) && birth <= new DateTime(y, 5, 20)) return "��ţ��";
            else if (birth >= new DateTime(y, 5, 21) && birth <= new DateTime(y, 6, 21)) return "˫����";
            else if (birth >= new DateTime(y, 6, 22) && birth <= new DateTime(y, 7, 22)) return "��з��";
            else if (birth >= new DateTime(y, 7, 23) && birth <= new DateTime(y, 8, 22)) return "ʨ����";
            else if (birth >= new DateTime(y, 8, 23) && birth <= new DateTime(y, 9, 22)) return "��Ů��";
            else if (birth >= new DateTime(y, 9, 23) && birth <= new DateTime(y, 10, 23)) return "������";
            else if (birth >= new DateTime(y, 10, 24) && birth <= new DateTime(y, 11, 22)) return "��Ы��";
            else if (birth >= new DateTime(y, 11, 23) && birth <= new DateTime(y, 12, 21)) return "������";
            else if (birth >= new DateTime(y, 12, 22) && birth <= new DateTime(y, 12, 31)) return "Ħ����";      //����
            else if (birth >= new DateTime(y, 1, 1) && birth <= new DateTime(y, 1, 19)) return "Ħ����";
            else if (birth >= new DateTime(y, 1, 20) && birth <= new DateTime(y, 2, 18)) return "ˮƿ��";
            else if (birth >= new DateTime(y, 2, 19) && birth <= new DateTime(y, 3, 20)) return "˫����";
            else
                return "";
        }

        /// <summary>
        /// ��ȡָ���������ܵ���һ����
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfTheWeek(DateTime date)
        {
            int dateIndex = ((int)date.DayOfWeek + 6) % 7;
            return date.AddDays(-dateIndex);
        }

        /// <summary>
        /// �����ַ����� yyyy-MM-dd HH:mm
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
        /// ���㷽��: thisTime - thatTime, ��� thisTime < thatTime, �򷵻ظո�
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
                return (diffDay / 365).ToString() + "��ǰ";
            }
            else if (diffDay > 30)
            {
                var month = diffDay / 30;

                if (month > 11)
                {
                    return "1��ǰ";
                }
                return month.ToString() + "����ǰ";
            }
            else if (diffDay > 0) 
            {
                return diffDay.ToString() + "��ǰ";
            }
            else
            {
                var diffHour = diff.Hours;

                if (diffHour > 0)
                {
                    return diffHour.ToString() + "Сʱǰ";
                }
                else
                {
                    var diffMin = diff.Minutes;

                    if (diffMin > 0)
                    {
                        return diffMin.ToString() + "����ǰ";
                    }
                }
            }

            return "�ո�";
        }

    }
}
