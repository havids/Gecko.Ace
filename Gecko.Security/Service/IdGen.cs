using System;
using System.Collections;
using System.Configuration;
using NHibernate;
using Gecko.Security.Domain;
using Gecko.Security.NHHelper;

namespace Gecko.Security.Service
{
    /// <summary>
    /// �̰߳�ȫ�Ļ�ȡָ���־û��������ֵ��
    /// </summary>
    internal sealed class IdGen
    {
        private static readonly Hashtable SyncLocks = new Hashtable();
        private static readonly string MirrorTag = "";

        private IdGen(){}

        static IdGen()
        {
            //Ϊÿ��nhibernateӳ�������һ��objectʵ�������߳�������
            IEnumerator ie = Db.Configuration.ClassMappings.GetEnumerator();
            while(ie.MoveNext())
            {
                SyncLocks.Add((ie.Current as NHibernate.Mapping.RootClass).MappedClass, new object());
            }

            //�����������ʾ��
            //MirrorTag = ConfigurationManager.AppSettings["MirrorTag"];
            //if (MirrorTag.Length > 5) throw new Exception("�����������ʾ�ֻ��Ϊ5λ��");
        }

        #region public static string GetNextId(Type type)
        /// <summary>
        /// ��ȡָ���־û������һ������ֵ��
        /// </summary>
        /// <param name="type">�־û�������͡�</param>
        /// <returns>������</returns>
        public static string GetNextId(Type type)
        {
            //��ȡ��ǰPO�����ݿ��ж�Ӧ�ı�����
            NHibernate.Mapping.PersistentClass pc = Db.Configuration.GetClassMapping(type);
            string tableName = pc.Table.Name;

            int iNextId = 0;
            lock (SyncLocks[type])
            {
                iNextId = InnerGetNextId(tableName, 1);
            }

            return MirrorTag + iNextId.ToString("d10");
        }
        #endregion

        #region public static string[] GetNextId(Type type, int count)
        /// <summary>
        /// ��ȡָ���־û����ָ������������ֵ��
        /// </summary>
        /// <param name="type">�־û�������͡�</param>
        /// <param name="count">Ҫ��ȡ������ֵ�ĸ�����</param>
        /// <returns>�������ϡ�</returns>
        public static string[] GetNextId(Type type, int count)
        {
            if (count > 0)
            {
                //�����������־��
                //string MirrorTag = ConfigurationManager.AppSettings["MirrorTag"];
                string MirrorTag = "";
                if (MirrorTag.Length > 5) return null;

                //��ȡ��ǰPO�����ݿ��ж�Ӧ�ı�����
                NHibernate.Mapping.PersistentClass pc = Db.Configuration.GetClassMapping(type);
                string tableName = pc.Table.Name;

                int iNextId = 0;
                lock (SyncLocks[type])
                {
                    iNextId = InnerGetNextId(tableName, count);
                }

                string[] Ids = new string[count];
                for (int i = 0; i < count; i++)
                {
                    int id = iNextId + i;
                    Ids[i] = MirrorTag + id.ToString("d10");
                }

                return Ids;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region private static string[] InnerGetNextId(Type type, int count)
        /// <summary>
        /// ��ȡָ���־û����ָ������������ֵ��
        /// </summary>
        /// <param name="tableName">���ݿ������</param>
        /// <param name="count">Ҫ��ȡ������ֵ�ĸ�����</param>
        /// <returns>��һ������ֵ��</returns>
        private static int InnerGetNextId(string tableName, int count)
        {
            Sequence Seq = Db.Session.Load(typeof(Sequence), tableName) as Sequence;
            int NextId = Seq.NextId;
            Seq.NextId = Seq.NextId + count;
            Db.Session.Update(Seq);
            Db.Session.Flush();
            Db.Session.Evict(Seq);
            return NextId;
        }
        #endregion

    }
}
