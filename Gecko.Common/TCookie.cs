using AiChou.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace Anxin.Common
{
    public class TCookie
    {

        public static string Domain
        {
            get { return Constant.CookieDomain; }
        }
        public static string DefaultName = System.Configuration.ConfigurationManager.AppSettings["cookiedomain"];
        public const string Key_User_ID = "uuid";
        public const string Key_Password = "pwdu";

        public static int maxExpires = 525600;


        public static HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        public static void Set(string name, string key, string val)
        {
            Set(name, key, val, 43200);
        }

        public static void Set(string key, string val, int minute)
        {
            Set(DefaultName, key, val, minute);
        }

        public static void Set(string name, string key, string val, int minute)
        {
            if (string.IsNullOrEmpty(val) || string.IsNullOrEmpty(name))
                return;

            if (Request.Browser.Cookies == false)
                return;

            HttpCookie cookie = HttpContext.Current.Response.Cookies[name];

            if (cookie == null) cookie = new HttpCookie(name);

            cookie.Values[key] = Security.Encode(val);
            if (minute > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(minute);
            }
            cookie.Domain = Domain;

            if (HttpContext.Current.Response.Cookies[name] != null)
            {
                HttpContext.Current.Response.Cookies.Remove(name);
                Request.Cookies.Remove(name);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);

        }



        /// <summary>
        /// Getcokie 
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        public static string Get(string name, string key)
        {
            if (Request == null) return string.Empty;

            HttpCookie cookie = Request.Cookies[name];

            if (cookie == null || cookie[key] == null) return string.Empty;

            return Security.Decode(cookie[key]);
        }

        public static void Remove(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (Request.Browser.Cookies == false) return;
            HttpCookie cookie = Request.Cookies[name];
            if (cookie == null) return;
            cookie.Expires = DateTime.MinValue;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void Set(string key, string val)
        {
            Set(DefaultName, key, val);
        }

        public static void Set(string key, object val)
        {
            Set(key, val.ToString());
        }

        public static string Get(string key)
        {
            return Get(DefaultName, key);
        }



        /// <summary>
        /// 返回原始Cookie值(未进行解密)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookie(string key)
        {

            if (Request == null) return string.Empty;

            HttpCookie cookie = Request.Cookies[key];

            if (cookie == null || cookie.Value == null) return string.Empty;

            return HttpUtility.UrlDecode(cookie.Value);
        }

        public static void SetCookie(string key, string value)
        {
            SetCookie(key, value, maxExpires);
        }

        public static string GetCookieAnxin(string key)
        {
            if (HttpContext.Current.Request == null) return "";

            HttpCookie cookie = Request.Cookies[key];

            if (cookie == null || cookie.Value == null) return "";

            return Security.Decode(cookie.Value);
        }

        public static string GetCookieAnxin2(string key)
        {
            if (HttpContext.Current.Response == null) return "";

            HttpCookie cookie = HttpContext.Current.Response.Cookies[key];

            if (cookie == null || cookie.Value == null) return "";

            return Security.Decode(cookie.Value);
        }

        public static void SetCookieAnxin2(string key, string value, int minute)
        {
            if (minute == -1)
                minute = 7 * 24 * 60;

            HttpCookie cookie = Request.Cookies[key];
            if (cookie == null)
            {
                cookie = new HttpCookie(key, value);
                cookie.Domain = "anxin.com";
                cookie.Expires = DateTime.Now.AddMinutes(minute);
                HttpContext.Current.Response.Cookies.Add(cookie);

                //LOG.Trace(LOG.ST.Day, "wei_ffffff_", key + "--" + value);
            }
            else
            {
                HttpContext.Current.Response.Cookies[key].Value = value;


                //LOG.Trace(LOG.ST.Day, "wei_ffffff_", "已有：" + key + "--" + value);

            }
        }

        public static void SetCookieAnxin(string key, string value, int minute)
        {
            if (Request.Browser.Cookies == false) return;

            if (minute == -1) minute = maxExpires;

            if (string.IsNullOrEmpty(value)) minute = -1;

            HttpCookie cookie = new HttpCookie(key, value);
            cookie.Domain = "anxin.com";

           
            if (minute != 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(minute);
            }
            if (HttpContext.Current.Response.Cookies[key] != null)
            {
                HttpContext.Current.Response.Cookies.Remove(key);
                Request.Cookies.Remove(key);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        /// <summary>
        /// 不加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minute"></param>
        public static void SetCookie(string key, string value, int minute)
        {
            if (Request.Browser.Cookies == false) return;

            if (minute == -1) minute = maxExpires;

            if (string.IsNullOrEmpty(value)) minute = -1;

            HttpCookie cookie = new HttpCookie(key, value);
            if (Domain != string.Empty) cookie.Domain = Domain;

            if (minute != 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(minute);
            }

            if (HttpContext.Current.Response.Cookies[key] != null)
            {
                HttpContext.Current.Response.Cookies.Remove(key);
                Request.Cookies.Remove(key);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }



    }
}
