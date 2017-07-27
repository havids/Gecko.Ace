using System;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// ���롣
    /// </summary>
    public sealed class SysCodeSrv
    {
        private SysCodeSrv() { }

        #region public static SysCode GetSysCodeByTag(string tag)
        /// <summary>
        /// ���ݱ�ʾ��ȡϵͳ���롣
        /// </summary>
        /// <param name="tag">��ʾ��</param>
        /// <returns>ϵͳ���롣</returns>
        public static SysCode GetSysCodeByTag(string tag)
        {
            string hql = "from SysCode s where s.Tag = :tag";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("tag", tag);
            q.SetCacheable(true);
            q.SetCacheRegion("SysCode");
            object o = q.UniqueResult();
            return (o != null) ? (SysCode)o : null;
        }
        #endregion


        #region public static void DeleteSysCode(string id)
        /// <summary>
        /// ɾ��ϵͳ���롣
        /// </summary>
        /// <param name="id">Ҫɾ����ϵͳ���������ֵ��</param>
        public static void DeleteSysCode(string id)
        {
            Db.SessionFactory.EvictQueries("SysCode");

            SysCode sc = Db.Session.Load(typeof(SysCode), id) as SysCode;
            sc.BreakAwayFromSysCodeType();

            Db.TransDelete(sc);
        }
        #endregion

        #region public static string InsertSysCode(SysCodeDTO dto)
        /// <summary>
        /// ����ϵͳ���롣
        /// </summary>
        /// <param name="dto">������ϵͳ�������Ϣ��</param>
        /// <returns>��ϵͳ�����Id����-2��Tag�ظ�����</returns>
        public static string InsertSysCode(SysCodeDTO dto)
        {
            SysCode existingSc = GetSysCodeByTag(dto.Tag);
            if (existingSc != null) { return "-2"; }

            Db.SessionFactory.EvictQueries("SysCode");

            SysCode sc = new SysCode();
            sc.Id = IdGen.GetNextId(typeof(SysCode));
            sc.Tag = dto.Tag;
            sc.Name = dto.Name;
            sc.Remark = dto.Remark;
            sc.OrderId = dto.OrderId;

            SysCodeType sct = Db.Session.Load(typeof(SysCodeType), dto.SysCodeTypeId) as SysCodeType;
            sct.AddSysCode(sc);

            Db.TransInsert(sc);
            return sc.Id;
        }
        #endregion

        #region public static string UpdateSysCode(SysCodeDTO dto)
        /// <summary>
        /// ����ϵͳ���롣
        /// </summary>
        /// <param name="dto">������ϵͳ�������Ϣ��</param>
        /// <returns>�ɹ���ʾ����1���ɹ���-2��Tag�ظ�����</returns>
        public static string UpdateSysCode(SysCodeDTO dto)
        {
            SysCode existingSc = GetSysCodeByTag(dto.Tag);
            if (existingSc != null && existingSc.Id != dto.Id) { return "-2"; }

            Db.SessionFactory.EvictQueries("SysCode");

            SysCode sc = Db.Session.Load(typeof(SysCode), dto.Id) as SysCode;
            sc.Tag = dto.Tag;
            sc.Name = dto.Name;
            sc.Remark = dto.Remark;
            sc.OrderId = dto.OrderId;

            Db.TransUpdate(sc);
            return "1";
        }
        #endregion

    }
}
