using System;
using System.Collections;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// ��ɫ��
    /// </summary>
    public sealed class RoleSrv
    {
        private RoleSrv() { }

        #region public static void DeleteRole(string id)
        /// <summary>
        /// ɾ����ɫ��
        /// </summary>
        /// <param name="id">Ҫɾ���Ľ�ɫ������ֵ��</param>
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
        /// ������ɫ��
        /// </summary>
        /// <param name="dto">��������ɫ����Ϣ��</param>
        /// <returns>�½�ɫ��Id��</returns>
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
        /// ���½�ɫ��
        /// </summary>
        /// <param name="dto">�����½�ɫ����Ϣ��</param>
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
        /// �ƶ���ɫ��
        /// </summary>
        /// <param name="id">Ҫ�ƶ��Ľ�ɫ��Id��</param>
        /// <param name="newModuleTypeId">�������µĽ�ɫ�����Id��</param>
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
        /// ���½�ɫ��ģ����Ȩ��Ϣ��
        /// </summary>
        /// <param name="roleId">��ɫID��</param>
        /// <param name="grant">�϶���Ȩ��Ȩ��ID��</param>
        /// <param name="deny">����Ȩ��Ȩ��ID��</param>
        public static void UpdatePermissions(string roleId, string[] grant, string[] deny)
        {
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Role r = Db.Session.Load(typeof(Role), roleId) as Role;
                
                //���¿϶�Ȩ�ޡ�
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

                //���·�Ȩ�ޡ�
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
