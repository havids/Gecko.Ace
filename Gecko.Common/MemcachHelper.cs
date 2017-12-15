using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Configuration;
using System.Collections;

namespace Anxin.Common
{
    public class MemcachHelper
    {
        //实例
        private static MemcachedClient objMC = new MemcachedClient();
        //分布Memcachedf服务IP 端口
        private static string[] serverlist = ConfigurationManager.AppSettings["Memcached.ServerList"].Split(',');
        private static int minuteTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["Memcached.MinuteTimeOut"]);
        private static string poolName = string.IsNullOrEmpty(ConfigurationManager.AppSettings["Memcached.PoolName"])? "MemcacheIOPool" : ConfigurationManager.AppSettings["Memcached.PoolName"];
        private static Hashtable Pools = new Hashtable();

        private static void SetPool() {
            SockIOPool pool = SockIOPool.CheckPools(poolName);
            if (pool == null) {
                pool = SockIOPool.GetInstance(poolName);
            }
                
            pool.SetServers(serverlist);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 1000;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            if (!pool.Initialized) { pool.Initialize(); }
        }
        /// <summary>
        /// 获取memcached值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static Object Get(String key)
        {
            if (key == "MemcachedKeysInfo") { getCachLists();}
            //初始化池
            SetPool();

            objMC.PoolName = poolName;
            objMC.EnableCompression = false;

            return objMC.Get(key);
        }


        public delegate T InsertCacheFun<T>();
        /// <summary>
        /// 获取缓存, 如果缓存不存在就执行代理方法添加缓存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key</param>
        /// <param name="getDataFun">获取数据的代理</param>
        /// <param name="minutes">缓存分钟数</param>
        /// <returns>缓存对象</returns>
        public static T Get<T>(string key, int minutes, InsertCacheFun<T> getDataFun)
        {
            object obj = null;
            if (checkKey(key))
            {
                obj = (T) Get(key);
                if (obj == null)
                {
                    obj = getDataFun();
                    if (minutes > 0 && obj != null)
                        Set(key, obj, minutes);
                }
            }
            return (T)obj;
        }

        /// <summary>
        /// 写入memcached
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool Set(string key, Object value)
        {
            return Set(key, value, DateTime.Now.AddMinutes(minuteTimeOut));
        }

        /// <summary>
        /// 写入memcached
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">缓存分钟数</param>
        /// <returns></returns>
        public static bool Set(string key, Object value,int minutes)
        {
            return Set(key, value, DateTime.Now.AddMinutes(minutes));
        }
        /// <summary>
        /// 写入memcached
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间（如：DateTime.Now.AddMinutes(1)）</param>
        /// <returns></returns>
        public static bool Set(string key, Object value, DateTime expiry)
        {
            if (!checkKey(key))
            {
                return false;
            }
            //初始化池
            SetPool();

            objMC.PoolName = poolName;
            objMC.EnableCompression = false;

            return objMC.Set(key, value, expiry);
        }


        /// <summary>
        /// 删除memcached
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool Delete(string key)
        {
            //初始化池
            SetPool();

            objMC.PoolName = poolName;
            objMC.EnableCompression = false;

            return objMC.Delete(key);
        }

        private static bool checkKey(string key){
            try
            {
                string temp = string.Empty;
                List<string> lists = getCachLists();
                foreach (string s in lists)
                {
                    temp = s;
                    if (s.IndexOf("#num#") > -1)
                    {
                        temp=s.Replace("#num#", key.Split('-')[key.Split('-').Length-1]);
                    }
                    if (temp == key)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch {
                Delete("MemcachedKeysInfo");
                return false;
            }
            
        }

        private static List<string> getCachLists() {
            List<string> lists = new List<string>();
            //初始化池
            SetPool();

            objMC.PoolName = poolName;
            objMC.EnableCompression = false;
            object objO = objMC.Get("MemcachedKeysInfo");
            if (objO != null) {
                return (List<string>)objO;
            }
            string fileContent = string.Empty;
            int i = 3;
            while (fileContent.IndexOf("MemcachedKeysInfo") < 0 && i > 0)
            {
                fileContent = HttpHelper.HttpGetHTML("http://config.anxin.com/html/cache/MemcachedKeysInfo.txt", Encoding.Default);
                i--;
            }
            byte[] array = Encoding.UTF8.GetBytes(fileContent);
            MemoryStream objS = new MemoryStream(array);             //convert stream 2 string      
            StreamReader objSR = new StreamReader(objS,Encoding.UTF8);

            String line;
            while ((line = objSR.ReadLine()) != null)
            {
                if (line.Length>0 && !line.Split('|')[0].StartsWith("//"))
                {
                    lists.Add(line.Split('|')[0]);
                }
            }
            objSR.Dispose();

            objMC.Set("MemcachedKeysInfo", lists, DateTime.Now.AddDays(1));

            return lists;
        }
    }
}
