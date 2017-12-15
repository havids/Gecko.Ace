using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.IO;

namespace Anxin.Common
{

    public class CacheHlper
    {
        protected static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;
        protected static int _timeOut = 3; // 默认缓存存活期为1440分钟(24小时)

        private static object syncObj = new object();


        public static System.Web.Caching.Cache WebCache
        {
            get { return webCache; }
        }

        //设置到期相对时间[单位：／分钟] 
        public static int TimeOut
        {
            set { _timeOut = value > 0 ? value : 0; }
            get { return _timeOut; }
        }

        public static void Add(string key, object obj)
        {


            Add(key, obj, TimeOut);
        }

        public static void Add(string key, object obj, int timeOut)
        {
            if (string.IsNullOrEmpty(key) || obj == null) return;

            DateTime expiration = timeOut == 0 ? DateTime.MaxValue : DateTime.Now.AddMinutes(timeOut);

            webCache.Insert(key, obj, null, expiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            webCache.Remove(key);
        }

        public static void Clear(string prefix)
        {
            IDictionaryEnumerator cacheEnum = webCache.GetEnumerator();
            while (cacheEnum.MoveNext())//找出所有的Cache
            {
                string key = cacheEnum.Key.ToString();

                if (key.StartsWith(prefix)) webCache.Remove(key);
            }
        }


        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;

            return webCache.Get(key);
        }

        public static DataSet GetCacheXML(string fileName, string keyName)
        {
            string key = "GetCacheXml_" + keyName;
            DataSet ds = CacheHlper.Get(key) as DataSet;
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            ds = new DataSet();
            ds.ReadXml(fileName);
            CacheHlper.Add(key, ds);
            return ds;
        }

        public static DataSet GetCacheXML(Stream stream, string keyName)
        {
            string key = "GetCacheXml_" + keyName;

            DataSet ds = CacheHlper.Get(key) as DataSet;
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            ds = new DataSet();
            ds.ReadXml(stream);
            CacheHlper.Add(key, ds);
            return ds;
        }
        /// <summary>
        /// 缓存本地txt文件
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetCacheStrByTxt(string keyName, string fileName)
        {

            string chcheKey = "GetCacheStr_" + keyName;
            string result = CacheHlper.Get(chcheKey) as String;
            if (!string.IsNullOrEmpty(result))
                return result;
            string content = Anxin.Common.FileHelper.ReadTextFileString(fileName);
            CacheHlper.Add(chcheKey, content);
            return content;
        }


        public delegate T InsertCacheFun<T>();
        public static T Get<T>(string key, int timeOut, InsertCacheFun<T> getDataFun)
        {
            object obj = Get(key);
            if (obj == null)
            {
                obj = getDataFun();
                if (timeOut > 0) Add(key, obj, timeOut);
            }
            return (T)obj;
        }


    }

}
