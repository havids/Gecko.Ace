using System;
using System.Text;
using System.IO;
using System.Threading;

namespace Gecko.Common
{
    /// <summary>
    /// 对日志存储的封装
    /// </summary>
    public class LOG
    {
        static LOG()
        {
            try
            {
                if (System.Web.HttpContext.Current != null)
                    logPath = System.Web.HttpContext.Current.Server.MapPath("~/log/");
                else
                    logPath = System.Windows.Forms.Application.StartupPath + "/log/";

                if (System.Configuration.ConfigurationManager.AppSettings["LogPath"] != null)
                {
                    string path = System.Configuration.ConfigurationManager.AppSettings["LogPath"];

                    if (Directory.Exists(path)) logPath = path;
                }
            }
            catch{}

        }

        #region 私有变量
        private static string LineBreak = new string('-', 100);
        #endregion

        #region 公开属性

        private static string logPath = "/Log/";

        /// <summary>
        /// 存储路径
        /// </summary>
        public static string LogPath
        {
            get { return logPath + DateTime.Today.ToString("yyyy-MM") + "\\"; }
            set {  }
        }

        /// <summary>
        /// 文件生成规则
        /// </summary>
        public static ST SaveType = ST.Day;

        /// <summary>
        /// 日志文件默认前缀
        /// </summary>
        public static string DefaultPrefix = "app";
        #endregion

        #region 公开方法

        /// <summary>
        /// 存储数据库异常
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="e">异常对象</param>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="param">SQL语句用到的参数</param>
        public static void DBExp(ST st, string prefix, Exception e, string strSQL, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "DB");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("异常：" + e.Message + "\r\n");
            sb.Append("SQL：" + strSQL + "\r\n");
            sb.Append("参数：" + GetParamList(param) + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());

        }

        /// <summary>
        /// 存储数据库异常
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="e">异常对象</param>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="param">SQL语句用到的参数</param>
        public static void DBExp(ST st, string prefix, string e, string strSQL, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "DB");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("异常：" + e + "\r\n");
            sb.Append("SQL：" + strSQL + "\r\n");
            sb.Append("参数：" + GetParamList(param) + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());

        }
        /// <summary>
        /// 存储应用程序异常
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="e">异常对象</param>
        /// <param name="param">存储的变量列表</param>
        public static void APPExp(ST st, string prefix, Exception e, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "APP");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("异常：" + e.ToString() + "\r\n");
            sb.Append("参数：" + GetParamList(param) + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());

        }



        /// <summary>
        /// 按照一定规则存储出错信息，便于用程序读取
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="param">存储的变量列表</param>
        public static void Error(ST st, string prefix, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "ERR");
            object[] p = new object[param.Length + 1];
            Array.Copy(param, 0, p, 1, param.Length);
            p[0] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string plist = GetParamList(p);
            WriteFile(fp, plist + "\r\n");

        }



        /// <summary>
        /// 存储Debug信息
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="Info">自定义信息</param>
        /// <param name="param">存储的变量列表</param>
        public static void Debug(ST st, string prefix, string Info, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "DEB");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("信息：" + Info + "\r\n");
            sb.Append("参数：" + GetParamList(param) + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }



        /// <summary>
        /// 存储日志信息
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="Info">自定义信息</param>
        public static void Trace(ST st, string prefix, string Info)
        {
            string fp = GetFilePath(st, prefix, "TRC");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("信息：" + Info + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }
        #endregion

        #region 私有函数

        /// <summary>
        /// 返回文件完整路径
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="type">文件内容类型</param>
        /// <returns>文件完整路径</returns>
        private static string GetFilePath(ST st, string prefix, string type)
        {
            string ext = ".log";
            string path = LogPath.Replace("/", "\\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (path.Substring(path.Length - 1) != "\\")
            {
                path += "\\";
            }
            path += prefix + "_" + type;
            switch (st)
            {
                case ST.Fixed:
                    path += ext;
                    break;
                case ST.Day:
                    path += DateTime.Now.ToString("yyyyMMdd") + ext;
                    break;
                case ST.Hour:
                    path += DateTime.Now.ToString("yyyyMMddHH") + ext;
                    break;
            }
            return (path);
        }

        /// <summary>
        /// 返回由object[]连成的字符串
        /// </summary>
        /// <param name="param">object数组</param>
        /// <returns>连接好的字符串</returns>
        private static string GetParamList(object[] param)
        {
            if (param == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (object p in param)
            {
                sb.Append("(" + ((p != null) ? p.ToString() : "null") + ")");
                if (p != param[param.Length - 1])
                {
                    sb.Append(",");
                }
            }
            return (sb.ToString());
        }

        /// <summary>
        /// 负责将日志内容写入文件
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="Content">日志内容</param>
        private static void WriteFile(string FilePath, string Content)
        {
            Type type = typeof(LOG);
            try
            {
                if (!Monitor.TryEnter(type))
                {
                    Monitor.Enter(type);
                    return;
                }
                using (StreamWriter writer = new StreamWriter(FilePath, true, System.Text.Encoding.UTF8))
                {
                    writer.Write(Content);
                    writer.Close();
                }
            }
            catch (System.IO.IOException e)
            {
                throw (e);
            }
            catch { }
            finally
            {
                Monitor.Exit(type);
            }
        }

        #endregion

        #region 枚举
        /// <summary>
        /// 文件生成规则
        /// </summary>
        public enum ST : int
        {
            /// <summary>
            /// 固定文件名
            /// </summary>
            Fixed = 1,
            /// <summary>
            /// 按天生成新的文件
            /// </summary>
            Day = 2,
            /// <summary>
            /// 按小时生成新的文件
            /// </summary>
            Hour = 3
        }
        #endregion

    }
}
