using System;
using System.Collections;
using System.Configuration;
using NHibernate;
using Gecko.Security.Domain;
using Gecko.Security.NHHelper;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 线程安全的获取指定持久化类的主键值。
    /// </summary>
    internal sealed class IdGen
    {
        private static readonly Hashtable SyncLocks = new Hashtable();
        private static readonly string MirrorTag = "";

        private IdGen(){}

        static IdGen()
        {
            //为每个nhibernate映射类产生一个object实例用于线程锁定。
            IEnumerator ie = Db.Configuration.ClassMappings.GetEnumerator();
            while(ie.MoveNext())
            {
                SyncLocks.Add((ie.Current as NHibernate.Mapping.RootClass).MappedClass, new object());
            }

            //镜像服务器标示。
            //MirrorTag = ConfigurationManager.AppSettings["MirrorTag"];
            //if (MirrorTag.Length > 5) throw new Exception("镜像服务器标示最长只能为5位。");
        }

        #region public static string GetNextId(Type type)
        /// <summary>
        /// 获取指定持久化类的下一个主键值。
        /// </summary>
        /// <param name="type">持久化类的类型。</param>
        /// <returns>主键。</returns>
        public static string GetNextId(Type type)
        {
            //获取当前PO在数据库中对应的表名。
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
        /// 获取指定持久化类的指定个数的主键值。
        /// </summary>
        /// <param name="type">持久化类的类型。</param>
        /// <param name="count">要获取的主键值的个数。</param>
        /// <returns>主键集合。</returns>
        public static string[] GetNextId(Type type, int count)
        {
            if (count > 0)
            {
                //镜像服务器标志。
                //string MirrorTag = ConfigurationManager.AppSettings["MirrorTag"];
                string MirrorTag = "";
                if (MirrorTag.Length > 5) return null;

                //获取当前PO在数据库中对应的表名。
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
        /// 获取指定持久化类的指定个数的主键值。
        /// </summary>
        /// <param name="tableName">数据库表名。</param>
        /// <param name="count">要获取的主键值的个数。</param>
        /// <returns>下一个主键值。</returns>
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
