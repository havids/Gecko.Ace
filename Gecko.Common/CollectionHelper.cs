using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Gecko.Common
{
    public static class CollectionHelper
    {
        public static List<string> CreateStringListFromCommaSeparatedString(string input)
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(input))
            {
                return list;
            }

            var array = input.Split(new char[] {'|', ','}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in array)
            {
                list.Add(item.Trim());
            }
            return list;
        }

        public static List<int> CreateIntListFromCommaSeparatedString(string input)
        {
            var list = new List<int>();
            if (string.IsNullOrEmpty(input))
            {
                return list;
            }
            var array = input.Split(new char[] {'|', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in array)
            {
                list.Add(Convert.ToInt32(item.Trim()));
            }
            return list;
        }

        public static string CreateParameters<TKey, TValue>(this IDictionary<TKey, TValue> dict, bool isUrlEncode)
        {
            var hasChanged = false;
            var buffer = new StringBuilder();
            foreach (var key in dict.Keys)
            {
                var value = dict[key];

                buffer.AppendFormat("{0}={1}&", key, isUrlEncode ? HttpUtility.UrlEncode(value.ToString()) : value.ToString());
                hasChanged = true;
            }

            if (hasChanged)
            {
                buffer.Length = buffer.Length - 1;
            }

            return buffer.ToString();
        }
    }
}
