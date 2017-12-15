using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.Caching;

namespace Gecko.Common
{
    /// <summary>
    /// 数据缓存
    /// </summary>
    public class CacheHelper
    {
        private CacheHelper() { }

        //>> Based on Factor = 5 default value
        /// <summary>
        /// DayFactor
        /// </summary>
        public static readonly int DayFactor = 86400;
        /// <summary>
        /// HourFactor
        /// </summary>
        public static readonly int HourFactor = 3600;
        /// <summary>
        /// MinuteFactor
        /// </summary>
        public static readonly int MinuteFactor = 60;
        /// <summary>
        /// SecondFactor
        /// </summary>
        public static readonly double SecondFactor = 1;
        /// <summary>
        /// _cache
        /// </summary>
        private static readonly Cache _cache;
        /// <summary>
        /// Factor
        /// </summary>
        private static int Factor = 1;
        /// <summary>
        /// ReSetFactor
        /// </summary>
        /// <param name="cacheFactor"></param>
        public static void ReSetFactor(int cacheFactor)
        {
            Factor = cacheFactor;
        }

        /// <summary>
        /// Static initializer should ensure we only have to look up the current cache
        /// instance once.
        /// </summary>
        static CacheHelper()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                _cache = context.Cache;
            }
            else
            {
                _cache = HttpRuntime.Cache;
            }
        }

        /// <summary>
        /// Removes all items from the Cache
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }

            foreach (string key in al)
            {
                _cache.Remove(key);
            }

        }
        /// <summary>
        /// RemoveByPattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            while (CacheEnum.MoveNext())
            {
                if (regex.IsMatch(CacheEnum.Key.ToString()))
                    _cache.Remove(CacheEnum.Key.ToString());
            }
        }

        /// <summary>
        /// Removes the specified key from the cache
        /// </summary>
        /// <param name="key">key</param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Insert the current "obj" into the cache. 
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        public static void Insert(string key, object obj)
        {
            Insert(key, obj, null, 1000);
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        /// <param name="dep">dep</param>
        public static void Insert(string key, object obj, CacheDependency dep)
        {
            Insert(key, obj, dep, HourFactor * 12);
        }
        /// <summary>
        /// 数据依赖缓存
        /// </summary>
        /// <param name="Key">键值</param>
        /// <param name="obj">对象</param>
        /// <param name="ConnectionStringName">数据库联接字符串</param>
        /// <param name="DependencyTableName">与 SqlCacheDependency 关联的数据库表的名称</param>
        /// <param name="DatabaseEntryName">在应用程序的 Web.config 文件的 caching 的 sqlCacheDependency 的 databases 元素（ASP.NET 设置架构）元素中定义的数据库的名称。 </param>
        public static void Insert(string Key, object obj, string ConnectionString, string DependencyTableName, string DatabaseEntryName)
        {
            SqlCacheDependencyAdmin.EnableNotifications(ConnectionString);
            SqlCacheDependencyAdmin.EnableTableForNotifications(ConnectionString, DependencyTableName);
            CacheHelper.Insert(Key, obj, new System.Web.Caching.SqlCacheDependency(DatabaseEntryName, DependencyTableName));
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        /// <param name="seconds">seconds</param>
        public static void Insert(string key, object obj, int seconds)
        {
            Insert(key, obj, null, seconds);
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        /// <param name="seconds">seconds</param>
        /// <param name="priority">priority</param>
        public static void Insert(string key, object obj, int seconds, CacheItemPriority priority)
        {
            Insert(key, obj, null, seconds, priority);
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        /// <param name="dep">dep</param>
        /// <param name="seconds">seconds</param>
        public static void Insert(string key, object obj, CacheDependency dep, int seconds)
        {
            Insert(key, obj, dep, seconds, CacheItemPriority.Normal);
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        /// <param name="dep">dep</param>
        /// <param name="seconds">seconds</param>
        /// <param name="priority">priority</param>
        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.Now.AddSeconds(Factor * seconds), TimeSpan.Zero, priority, null);
            }

        }
        /// <summary>
        /// MicroInsert
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        /// <param name="secondFactor">secondFactor</param>
        public static void MicroInsert(string key, object obj, int secondFactor)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, null, DateTime.Now.AddSeconds(Factor * secondFactor), TimeSpan.Zero);
            }
        }
        /// <summary>
        /// 在指定时间间隔内如果没有访问将会自动从缓存中清除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="ts">指定时间间隔</param>
        public static void Insert(string key, object obj, TimeSpan ts)
        {
            if (obj != null)
                _cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, ts);
        }
        /// <summary>
        /// Insert an item into the cache for the Maximum allowed time
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }
        /// <summary>
        /// Max
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">obj</param>
        /// <param name="dep">dep</param>
        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return _cache[key];
        }

        public delegate T InsertCacheFun<T>();
        /// <summary>
        /// 获取缓存, 如果缓存不存在就执行代理方法添加缓存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key</param>
        /// <param name="getDataFun">获取数据的代理</param>
        /// <param name="second">缓存秒数</param>
        /// <returns>缓存对象</returns>
        public static T Get<T>(string key, int second, InsertCacheFun<T> getDataFun)
        {
            object obj = _cache[key];
            if (obj == null)
            {
                obj = getDataFun();
                if (second > 0)
                    Insert(key, obj, second);
            }
            return (T)obj;
        }

        /// <summary>
        /// 通过缓存和 http 读取文件内容
        /// </summary>
        /// <param name="fileUrl">文件的url地址</param>
        /// <param name="second">缓存秒数</param>
        /// <returns>文件内容</returns>
        //public static string LoadFileContent(string fileUrl, int second)
        //{
        //    object obj = _cache[fileUrl];

        //    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
        //    {
        //        var htmlContent = PageHelper.GetHtml(fileUrl);

        //        if (string.IsNullOrEmpty(htmlContent) || htmlContent.Contains("操作超时")) 
        //        {
        //            LOG.Trace(LOG.ST.Day, "CacheFileLoad", "File load Error: " + fileUrl + "\r\n" + htmlContent.ToString());
        //            return string.Empty;
        //        }
                
        //        if (second > 0)
        //        {
        //            Insert(fileUrl, htmlContent, second);
        //        }

        //        return htmlContent;
        //    }

        //    return obj.ToString();
        //}

        /// <summary>
        /// SecondFactorCalculate
        /// </summary>
        /// <param name="seconds">seconds</param>
        /// <returns></returns>
        public static int SecondFactorCalculate(int seconds)
        {
            // Insert method below takes integer seconds, so we have to round any fractional values
            return Convert.ToInt32(Math.Round((double)seconds * SecondFactor));
        }

    }
}
