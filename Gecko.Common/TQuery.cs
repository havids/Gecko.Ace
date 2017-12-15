using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace Anxin.Common
{
    public class TQuery
    {

        private Dictionary<string, object> _item = new Dictionary<string, object>();
        public Dictionary<string, object> QueryString
        {
            get { return _item; }
        }


        public int Count
        {
            get
            {
                return _item.Count;
            }
        }

        public TQuery() { }

        public TQuery(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string _query = new Regex("^\\?").Replace(query, "");
                string[] tokens = _query.Split('&');

                foreach (string token in tokens)
                {
                    string[] item = token.Split('=');

                    if (item.Length >= 2 && item[0].Length > 0)
                    {
                        this[item[0]] = item[1];
                    }
                }

            }
        }

        public string Query
        {
            get
            {
                if (_item == null || _item.Count == 0) return string.Empty;

                string query = string.Empty;

                foreach (string key in _item.Keys)
                {
                    query += string.Format("&{0}={1}", key, _item[key]);
                }

                query = new Regex("^&").Replace(query, "?");

                return query;

            }
        }

        public string ToSqlQuery()
        {
            if (_item == null || _item.Count == 0) return string.Empty;

            string query = string.Empty;

            foreach (string key in _item.Keys)
            {
                if (key.StartsWith("s_", StringComparison.OrdinalIgnoreCase))
                    query += string.Format(" and {0}={1}", key.Substring(2), _item[key]);
            }

            query = new Regex("^ and").Replace(query, "");

            return query;
        }

        public string ToSqlQuery(string query)
        {
            if ((this._item != null) && (this._item.Count != 0))
            {
                foreach (string str in this._item.Keys)
                {
                    if (this._item[str] == null) continue;

                    if (str.StartsWith("s_", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query + string.Format(" and ({0} like '%{1}%')", str.Substring(2).Trim(), System.Web.HttpUtility.UrlDecode(this._item[str].ToString()).Trim());
                    }

                    if (str.StartsWith("e_", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query + string.Format(" and ({0} = {1})", str.Substring(2).Trim(), this._item[str]);
                    }

                    if (str.StartsWith("l_", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query + string.Format(" and ({0} <= {1})", str.Substring(2).Trim(), this._item[str]);
                    }

                    if (str.StartsWith("g_", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query + string.Format(" and ({0} >= {1})", str.Substring(2).Trim(), this._item[str]);
                    }
                }
                query = new Regex("^ and").Replace(query, "");
            }
            return query;

        }

        public override string ToString()
        {
            return Query;
        }

        public void Add(string key, object value)
        {
            if (value == null) _item.Remove(key);

            _item.Add(key.ToLower(), value);
        }


        public void Remove(string key)
        {
            _item.Remove(key.ToLower());
        }

        public object this[string key]
        {
            get
            {
                if (!_item.ContainsKey(key.ToLower())) return string.Empty;

                return _item[key.ToLower()];
            }
            set
            {
                _item[key.ToLower()] = value;
            }
        }



        public static TQuery PageQuery
        {
            get
            {
                TQuery query = new TQuery(System.Web.HttpContext.Current.Request.Url.Query);
                return query;
            }
        }


        #region Request


        public static string GetSafeString(string key)
        {
            return Utils.GetSafeString(GetString(key, ""));
        }

        public static string GetInput(string key)
        {
            return StringHelper.InputTitle(GetString(key, ""));
        }

        public static string Get(string key)
        {
            return GetString(key, "");
        }

        public static string Get(string key, int maxLength)
        {
            string query = GetSafeString(key);
            if (query.Length > maxLength) return string.Empty;

            return query;
        }

        public static string GetString(string key)
        {
            return GetString(key, "");
        }
        

        public static string GetString(string key, string def)
        {
            string ret = Utils.ParseString(System.Web.HttpContext.Current.Request[key]);
            return (ret == string.Empty) ? def : ret;
        }

        public static string GetNumber(string key)
        {
            return GetNumber(key, 0);
        }

        public static string GetNumber(string key, int def)
        {
            return GetInt(key, def).ToString();
        }

        public static int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        public static int GetInt(string key, int def)
        {
            return Utils.ParseInt(System.Web.HttpContext.Current.Request[key], def);
        }

        public static long GetLong(string key)
        {
            return GetLong(key, 0);
        }
        public static long GetLong(string key, int def)
        {
            return Utils.ParseInt64(System.Web.HttpContext.Current.Request[key], def);
        }

        public static decimal GetDecimal(string key, decimal def)
        {
            return Utils.ParseDecimal(System.Web.HttpContext.Current.Request[key], def);
        }

        public static string GetFomart(string key, string format)
        {
            return GetFomart(key, format, "");
        }

        public static string GetFomart(string key, string format, string def)
        {
            string tstr = Utils.ParseString(System.Web.HttpContext.Current.Request[key]);
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(format);
            if (reg.IsMatch(tstr)) return tstr; else return def;
        }


        #endregion
    }
}
