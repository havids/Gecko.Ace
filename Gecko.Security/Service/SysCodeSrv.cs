using System;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 代码。
    /// </summary>
    public sealed class SysCodeSrv
    {
        private SysCodeSrv() { }

        #region public static SysCode GetSysCodeByTag(string tag)
        /// <summary>
        /// 根据标示获取系统代码。
        /// </summary>
        /// <param name="tag">标示。</param>
        /// <returns>系统代码。</returns>
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
        /// 删除系统代码。
        /// </summary>
        /// <param name="id">要删除的系统代码的主键值。</param>
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
        /// 新增系统代码。
        /// </summary>
        /// <param name="dto">待新增系统代码的信息。</param>
        /// <returns>新系统代码的Id。（-2：Tag重复。）</returns>
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
        /// 更新系统代码。
        /// </summary>
        /// <param name="dto">待更新系统代码的信息。</param>
        /// <returns>成功标示。（1：成功；-2：Tag重复。）</returns>
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
