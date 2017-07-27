using System;
using NHibernate;
using System.Collections;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// ������ࡣ
    /// </summary>
    public sealed class SysCodeTypeSrv
    {
        private SysCodeTypeSrv() { }

        #region public static IList GetAllSysCodeType()
        /// <summary>
        /// ��ȡ����ϵͳ�������ʵ����
        /// </summary>
        /// <returns>����ϵͳ�������ʵ����</returns>
        public static IList GetAllSysCodeType()
        {
            string hql = "from SysCodeType s order by s.OrderId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetCacheable(true);
            q.SetCacheRegion("SysCodeType");
            return q.List();
        }
        #endregion

        #region public static SysCodeType GetSysCodeTypeByTag(string tag)
        /// <summary>
        /// ���ݱ�ʾ��ȡϵͳ������ࡣ
        /// </summary>
        /// <param name="tag">��ʾ��</param>
        /// <returns>ϵͳ������ࡣ</returns>
        public static SysCodeType GetSysCodeTypeByTag(string tag)
        {
            string hql = "from SysCodeType s where s.Tag = :tag";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("tag", tag);
            q.SetCacheable(true);
            q.SetCacheRegion("SysCodeType");
            object o = q.UniqueResult();
            return (o != null) ? (SysCodeType)o : null;
        }
        #endregion


        #region public static void DeleteSysCodeType(string id)
        /// <summary>
        /// ɾ��ϵͳ������ࡣ
        /// </summary>
        /// <param name="id">Ҫɾ����ϵͳ������������ֵ��</param>
        public static void DeleteSysCodeType(string id)
        {
            Db.SessionFactory.EvictQueries("SysCodeType");

            SysCodeType sct = Db.Session.Load(typeof(SysCodeType), id) as SysCodeType;

            Db.TransDelete(sct);
        }
        #endregion

        #region public static string InsertSysCodeType(SysCodeTypeDTO dto)
        /// <summary>
        /// ����ϵͳ������ࡣ
        /// </summary>
        /// <param name="dto">������ϵͳ����������Ϣ��</param>
        /// <returns>��ϵͳ��������Id����-2��Tag�ظ�����</returns>
        public static string InsertSysCodeType(SysCodeTypeDTO dto)
        {
            SysCodeType existingSct = GetSysCodeTypeByTag(dto.Tag);
            if (existingSct != null) { return "-2"; }

            Db.SessionFactory.EvictQueries("SysCodeType");

            SysCodeType sct = new SysCodeType();
            sct.Id = IdGen.GetNextId(typeof(SysCodeType));
            sct.Tag = dto.Tag;
            sct.Name = dto.Name;
            sct.Remark = dto.Remark;
            sct.OrderId = dto.OrderId;

            Db.TransInsert(sct);
            return sct.Id;
        }
        #endregion

        #region public static string UpdateSysCodeType(SysCodeTypeDTO dto)
        /// <summary>
        /// ����ϵͳ������ࡣ
        /// </summary>
        /// <param name="dto">������ϵͳ����������Ϣ��</param>
        /// <param name="Id">ϵͳ��������Id��</param>
        /// <returns>�ɹ���ʾ����1���ɹ���-2��Tag�ظ�����</returns>
        public static string UpdateSysCodeType(SysCodeTypeDTO dto)
        {
            SysCodeType existingSct = GetSysCodeTypeByTag(dto.Tag);
            if (existingSct != null && existingSct.Id != dto.Id) { return "-2"; }

            Db.SessionFactory.EvictQueries("SysCodeType");

            SysCodeType sct = Db.Session.Load(typeof(SysCodeType), dto.Id) as SysCodeType;
            sct.Tag = dto.Tag;
            sct.Name = dto.Name;
            sct.Remark = dto.Remark;
            sct.OrderId = dto.OrderId;

            Db.TransUpdate(sct);
            return "1";
        }
        #endregion

    }
}
