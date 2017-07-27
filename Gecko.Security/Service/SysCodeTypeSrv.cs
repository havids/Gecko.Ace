using System;
using NHibernate;
using System.Collections;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 代码分类。
    /// </summary>
    public sealed class SysCodeTypeSrv
    {
        private SysCodeTypeSrv() { }

        #region public static IList GetAllSysCodeType()
        /// <summary>
        /// 获取所有系统代码分类实例。
        /// </summary>
        /// <returns>所有系统代码分类实例。</returns>
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
        /// 根据标示获取系统代码分类。
        /// </summary>
        /// <param name="tag">标示。</param>
        /// <returns>系统代码分类。</returns>
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
        /// 删除系统代码分类。
        /// </summary>
        /// <param name="id">要删除的系统代码分类的主键值。</param>
        public static void DeleteSysCodeType(string id)
        {
            Db.SessionFactory.EvictQueries("SysCodeType");

            SysCodeType sct = Db.Session.Load(typeof(SysCodeType), id) as SysCodeType;

            Db.TransDelete(sct);
        }
        #endregion

        #region public static string InsertSysCodeType(SysCodeTypeDTO dto)
        /// <summary>
        /// 新增系统代码分类。
        /// </summary>
        /// <param name="dto">待新增系统代码分类的信息。</param>
        /// <returns>新系统代码分类的Id。（-2：Tag重复。）</returns>
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
        /// 更新系统代码分类。
        /// </summary>
        /// <param name="dto">待更新系统代码分类的信息。</param>
        /// <param name="Id">系统代码分类的Id。</param>
        /// <returns>成功标示。（1：成功；-2：Tag重复。）</returns>
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
