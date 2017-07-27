using System;
using System.Collections;
using System.Collections.Specialized;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 角色类型。
    /// </summary>
    public sealed class RoleTypeSrv
    {
        private RoleTypeSrv() { }

        #region public static IList GetAllTopRoleType()
        /// <summary>
        /// 获取所有顶层的角色分类实例。
        /// </summary>
        /// <returns>所有顶层的角色分类实例。</returns>
        public static IList GetAllTopRoleType()
        {
            string hql = "from RoleType r where r.ParentRoleType is null order by r.OrderId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetCacheable(true);
            q.SetCacheRegion("RoleType");
            return q.List();
        }
        #endregion


        #region public static void DeleteRoleType(string id)
        /// <summary>
        /// 删除模块分类。
        /// </summary>
        /// <param name="id">要删除的模块分类的主键值。</param>
        public static void DeleteModuleType(string id)
        {
            Db.SessionFactory.EvictQueries("RoleType");

            RoleType rt = Db.Session.Load(typeof(RoleType), id) as RoleType;
            rt.BreakAwayFromParent();

            Db.TransDelete(rt);
        }
        #endregion

        #region public static string InsertRoleType(RoleTypeDTO dto)
        /// <summary>
        /// 新增角色分类。
        /// </summary>
        /// <param name="dto">待新增角色分类的信息。</param>
        /// <returns>新角色分类的Id。</returns>
        public static string InsertRoleType(RoleTypeDTO dto)
        {
            Db.SessionFactory.EvictQueries("RoleType");

            RoleType rt = new RoleType();
            rt.Id = IdGen.GetNextId(typeof(RoleType));
            rt.Name = dto.Name;
            rt.Remark = dto.Remark;
            rt.OrderId = dto.OrderId;

            RoleType prt = null;
            if (dto.ParentRoleTypeId != null && dto.ParentRoleTypeId.Length > 0)
            {
                prt = Db.Session.Load(typeof(RoleType), dto.ParentRoleTypeId) as RoleType;
                prt.AddSubRoleType(rt);
            }

            Db.TransInsert(rt);

            return rt.Id;
        }
        #endregion

        #region public static void UpdateRoleType(RoleTypeDTO dto)
        /// <summary>
        /// 更新角色分类。
        /// </summary>
        /// <param name="dto">待更新角色分类的信息。</param>
        public static void UpdateRoleType(RoleTypeDTO dto)
        {
            Db.SessionFactory.EvictQueries("RoleType");

            RoleType rt = Db.Session.Load(typeof(RoleType), dto.Id) as RoleType;
            rt.Name = dto.Name;
            rt.Remark = dto.Remark;
            rt.OrderId = dto.OrderId;

            Db.TransUpdate(rt);
        }
        #endregion

        #region public static void MoveRoleType(string Id, string newParentId)
        /// <summary>
        /// 移动角色分类。
        /// </summary>
        /// <param name="Id">角色分类Id。</param>
        /// <param name="newParentModuleTypeId">新的父角色分类Id。</param>
        public static void MoveRoleType(string Id, string newParentId)
        {
            Db.SessionFactory.EvictQueries("RoleType");

            RoleType rt = Db.Session.Load(typeof(RoleType), Id) as RoleType;

            RoleType newParent = null;
            if (newParentId != null && newParentId.Length > 0)
            {
                newParent = Db.Session.Load(typeof(RoleType), newParentId) as RoleType;
            }

            rt.MoveTo(newParent);

            Db.TransUpdate(rt);
        }
        #endregion

    }
}
