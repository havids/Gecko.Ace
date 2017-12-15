using System;
using System.Text;
using System.IO;
using System.Threading;

namespace Gecko.Common
{
    /// <summary>
    /// ����־�洢�ķ�װ
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

        #region ˽�б���
        private static string LineBreak = new string('-', 100);
        #endregion

        #region ��������

        private static string logPath = "/Log/";

        /// <summary>
        /// �洢·��
        /// </summary>
        public static string LogPath
        {
            get { return logPath + DateTime.Today.ToString("yyyy-MM") + "\\"; }
            set {  }
        }

        /// <summary>
        /// �ļ����ɹ���
        /// </summary>
        public static ST SaveType = ST.Day;

        /// <summary>
        /// ��־�ļ�Ĭ��ǰ׺
        /// </summary>
        public static string DefaultPrefix = "app";
        #endregion

        #region ��������

        /// <summary>
        /// �洢���ݿ��쳣
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="e">�쳣����</param>
        /// <param name="strSQL">SQL���</param>
        /// <param name="param">SQL����õ��Ĳ���</param>
        public static void DBExp(ST st, string prefix, Exception e, string strSQL, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "DB");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("�쳣��" + e.Message + "\r\n");
            sb.Append("SQL��" + strSQL + "\r\n");
            sb.Append("������" + GetParamList(param) + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());

        }

        /// <summary>
        /// �洢���ݿ��쳣
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="e">�쳣����</param>
        /// <param name="strSQL">SQL���</param>
        /// <param name="param">SQL����õ��Ĳ���</param>
        public static void DBExp(ST st, string prefix, string e, string strSQL, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "DB");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("�쳣��" + e + "\r\n");
            sb.Append("SQL��" + strSQL + "\r\n");
            sb.Append("������" + GetParamList(param) + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());

        }
        /// <summary>
        /// �洢Ӧ�ó����쳣
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="e">�쳣����</param>
        /// <param name="param">�洢�ı����б�</param>
        public static void APPExp(ST st, string prefix, Exception e, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "APP");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("�쳣��" + e.ToString() + "\r\n");
            sb.Append("������" + GetParamList(param) + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());

        }



        /// <summary>
        /// ����һ������洢������Ϣ�������ó����ȡ
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="param">�洢�ı����б�</param>
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
        /// �洢Debug��Ϣ
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="Info">�Զ�����Ϣ</param>
        /// <param name="param">�洢�ı����б�</param>
        public static void Debug(ST st, string prefix, string Info, params object[] param)
        {
            string fp = GetFilePath(st, prefix, "DEB");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("��Ϣ��" + Info + "\r\n");
            sb.Append("������" + GetParamList(param) + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }



        /// <summary>
        /// �洢��־��Ϣ
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="Info">�Զ�����Ϣ</param>
        public static void Trace(ST st, string prefix, string Info)
        {
            string fp = GetFilePath(st, prefix, "TRC");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("��Ϣ��" + Info + "\r\n");
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }
        #endregion

        #region ˽�к���

        /// <summary>
        /// �����ļ�����·��
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="type">�ļ���������</param>
        /// <returns>�ļ�����·��</returns>
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
        /// ������object[]���ɵ��ַ���
        /// </summary>
        /// <param name="param">object����</param>
        /// <returns>���Ӻõ��ַ���</returns>
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
        /// ������־����д���ļ�
        /// </summary>
        /// <param name="FilePath">�ļ�·��</param>
        /// <param name="Content">��־����</param>
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

        #region ö��
        /// <summary>
        /// �ļ����ɹ���
        /// </summary>
        public enum ST : int
        {
            /// <summary>
            /// �̶��ļ���
            /// </summary>
            Fixed = 1,
            /// <summary>
            /// ���������µ��ļ�
            /// </summary>
            Day = 2,
            /// <summary>
            /// ��Сʱ�����µ��ļ�
            /// </summary>
            Hour = 3
        }
        #endregion

    }
}
