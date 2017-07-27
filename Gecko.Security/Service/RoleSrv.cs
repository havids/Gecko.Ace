using System;
using System.Collections;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 角色。
    /// </summary>
    public sealed class RoleSrv
    {
        private RoleSrv() { }

        #region public static void DeleteRole(string id)
        /// <summary>
        /// 删除角色。
        /// </summary>
        /// <param name="id">要删除的角色的主键值。</param>
        public static void DeleteRole(string id)
        {
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Role r = Db.Session.Load(typeof(Role), id) as Role;
                r.RemoveAllPermissions();
                r.BreakAwayFromStaff();
                r.BreakAwayFromRoleType();

                Db.Session.Delete(r);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion

        #region public static string InsertRole(RoleDTO dto)
        /// <summary>
        /// 新增角色。
        /// </summary>
        /// <param name="dto">待新增角色的信息。</param>
        /// <returns>新角色的Id。</returns>
        public static string InsertRole(RoleDTO dto)
        {
            Role r = new Role();
            r.Id = IdGen.GetNextId(typeof(Role));
            r.Name = dto.Name;
            r.Remark = dto.Remark;
            r.OrderId = dto.OrderId;

            RoleType rt = Db.Session.Load(typeof(RoleType), dto.RoleTypeId) as RoleType;
            rt.AddRole(r);

            Db.TransInsert(r);
            return r.Id;
        }
        #endregion

        #region public static void UpdateRole(RoleDTO dto)
        /// <summary>
        /// 更新角色。
        /// </summary>
        /// <param name="dto">待更新角色的信息。</param>
        public static void UpdateRole(RoleDTO dto)
        {
            Role r = Db.Session.Load(typeof(Role), dto.Id) as Role;
            r.Name = dto.Name;
            r.Remark = dto.Remark;
            r.OrderId = dto.OrderId;

            Db.TransUpdate(r);
        }
        #endregion

        #region public static void MoveRole(string id, string newRoleTypeId)
        /// <summary>
        /// 移动角色。
        /// </summary>
        /// <param name="id">要移动的角色的Id。</param>
        /// <param name="newModuleTypeId">所属的新的角色分类的Id。</param>
        public static void MoveRole(string id, string newRoleTypeId)
        {
            Role r = Db.Session.Load(typeof(Role), id) as Role;
            RoleType newParent = Db.Session.Load(typeof(RoleType), newRoleTypeId) as RoleType;
            r.MoveTo(newParent);

            Db.TransUpdate(r);
        }
        #endregion

        #region public static void UpdatePermissions(string roleId, string[] grant, string[] deny)
        /// <summary>
        /// 更新角色的模块授权信息。
        /// </summary>
        /// <param name="roleId">角色ID。</param>
        /// <param name="grant">肯定授权的权限ID。</param>
        /// <param name="deny">否定授权的权限ID。</param>
        public static void UpdatePermissions(string roleId, string[] grant, string[] deny)
        {
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Role r = Db.Session.Load(typeof(Role), roleId) as Role;
                
                //更新肯定权限。
                r.ModuleRightsGrant.Clear();
                foreach (ModuleRight mr in r.ModuleRightsGrant)
                {
                    mr.RolesGrant.Remove(r);
                }
                for (int i = 0; i < grant.Length; i++)
                {
                    ModuleRight mr = Db.Session.Load(typeof(ModuleRight), grant[i]) as ModuleRight;
                    r.ModuleRightsGrant.Add(mr);
                    mr.RolesGrant.Add(r);
                }

                //更新否定权限。
                r.ModuleRightsDeny.Clear();
                foreach (ModuleRight mr in r.ModuleRightsDeny)
                {
                    mr.RolesGrant.Remove(r);
                }
                for (int i = 0; i < deny.Length; i++)
                {
                    ModuleRight mr = Db.Session.Load(typeof(ModuleRight), deny[i]) as ModuleRight;
                    r.ModuleRightsDeny.Add(mr);
                    mr.RolesDeny.Add(r);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion

    }
}
