using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;
using Gecko.Security.Util;
using NHibernate.Criterion;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 职员。
    /// </summary>
    public sealed class StaffSrv
    {
        private StaffSrv() { }

        #region public static void DeleteStaff(string id)
        /// <summary>
        /// 删除职员。
        /// </summary>
        /// <param name="id">要删除的职员的登录ID。</param>
        public static void DeleteStaff(string id)
        {
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Staff s = Db.Session.Load(typeof(Staff), id) as Staff;
                s.RemoveAllPermissions();
                s.BreakAwayFromRoles();
                s.BreakAwayFromDepartment();

                Db.Session.Delete(s);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion

        #region public static string InsertStaff(StaffDTO dto)
        /// <summary>
        /// 新增职员。
        /// </summary>
        /// <param name="dto">待新增职员的信息。</param>
        /// <returns>新增是否成功的标示。（1：成功；-2：登录Id重复）</returns>
        public static string InsertStaff(StaffDTO dto)
        {
            //判断登录Id是否重复。
            string hql = "select count(*) from Staff s where s.LoginId = :loginId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("loginId", dto.LoginId);
            object o = q.UniqueResult();
            if (int.Parse(o.ToString()) > 0) { return "-2"; }

            //职员基本信息。
            Staff s = new Staff();
            s.LoginId = dto.LoginId;
            s.Password = StringSecurity.StringToSHA1Hash(dto.Password);
            s.Name = dto.Name;
            s.Disabled = dto.Disabled;
            s.OrderId = dto.OrderId;
            s.IdCard = dto.IdCard;
            s.Code = dto.Code;
            s.OfficePhone = dto.OfficePhone;
            s.ExtNumber = dto.ExtNumber;
            s.CellPhone = dto.CellPhone;
            s.FamilyPhone = dto.FamilyPhone;
            s.Email = dto.Email;
            s.ZipCode = dto.ZipCode;
            s.Remark = dto.Remark;
            s.Address = dto.Address;
            s.DegreeTag = dto.DegreeTag;
            s.Sex = dto.Sex;
            s.PoliticalAppearanceTag = dto.PoliticalAppearanceTag;
            s.Married = dto.Married;
            s.Birthday = dto.Birthday.Value;
            s.CountryTag = dto.CountryTag;
            s.EntersDay = dto.EntersDay.Value;
            s.NationTag = dto.NationTag;
            s.LeavesDay = dto.LeavesDay.Value;
            s.PositionTag = dto.PositionTag;
            s.TitleTag = dto.TitleTag;

            //所有通过程序加入的职员都不是内部用户。
            s.IsInnerUser = 0;

            //所属部门。
            Department d = Db.Session.Load(typeof(Department), dto.DepartmentId) as Department;
            d.AddStaff(s);

            Db.TransInsert(s);
            return "1";
        }
        #endregion

        #region public static void UpdateStaff(StaffDTO dto)
        /// <summary>
        /// 更新职员。
        /// </summary>
        /// <param name="dto">待更新职员的信息。</param>
        public static void UpdateStaff(StaffDTO dto)
        {
            Staff s = Db.Session.Load(typeof(Staff), dto.LoginId) as Staff;
            s.Name = dto.Name;
            s.Disabled = dto.Disabled;
            s.OrderId = dto.OrderId;
            s.IdCard = dto.IdCard;
            s.Code = dto.Code;
            s.OfficePhone = dto.OfficePhone;
            s.ExtNumber = dto.ExtNumber;
            s.CellPhone = dto.CellPhone;
            s.FamilyPhone = dto.FamilyPhone;
            s.Email = dto.Email;
            s.ZipCode = dto.ZipCode;
            s.Remark = dto.Remark;
            s.Address = dto.Address;
            s.DegreeTag = dto.DegreeTag;
            s.Sex = dto.Sex;
            s.PoliticalAppearanceTag = dto.PoliticalAppearanceTag;
            s.Married = dto.Married;
            s.Birthday = dto.Birthday.Value;
            s.CountryTag = dto.CountryTag;
            s.EntersDay = dto.EntersDay.Value;
            s.NationTag = dto.NationTag;
            s.LeavesDay = dto.LeavesDay.Value;
            s.PositionTag = dto.PositionTag;
            s.TitleTag = dto.TitleTag;

            Db.TransUpdate(s);
        }
        #endregion

        #region public static void MoveStaff(string id, string newDepartmentId)
        /// <summary>
        /// 移动职员。
        /// </summary>
        /// <param name="Id">登录Id。</param>
        /// <param name="newDepartmentId">新的部门Id。</param>
        public static void MoveStaff(string id, string newDepartmentId)
        {
            Staff s = Db.Session.Load(typeof(Staff), id) as Staff;
            Department newDepartment = Db.Session.Load(typeof(Department), newDepartmentId) as Department;
            s.MoveTo(newDepartment);

            Db.TransUpdate(s);
        }
        #endregion

        #region public static void UpdatePermissions(string staffId, string[] grant, string[] deny)
        /// <summary>
        /// 更新职员的模块授权信息。
        /// </summary>
        /// <param name="staffId">职员登录ID。</param>
        /// <param name="grant">肯定授权的权限ID。</param>
        /// <param name="deny">否定授权的权限ID。</param>
        public static void UpdatePermissions(string staffId, string[] grant, string[] deny)
        {
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Staff s = Db.Session.Load(typeof(Staff), staffId) as Staff;

                //更新肯定权限。
                s.ModuleRightsGrant.Clear();
                foreach (ModuleRight mr in s.ModuleRightsGrant)
                {
                    mr.StaffGrant.Remove(s);
                }
                for (int i = 0; i < grant.Length; i++)
                {
                    ModuleRight mr = Db.Session.Load(typeof(ModuleRight), grant[i]) as ModuleRight;
                    s.ModuleRightsGrant.Add(mr);
                    mr.StaffGrant.Add(s);
                }

                //更新肯定权限。
                s.ModuleRightsDeny.Clear();
                foreach (ModuleRight mr in s.ModuleRightsDeny)
                {
                    mr.StaffDeny.Remove(s);
                }
                for (int i = 0; i < deny.Length; i++)
                {
                    ModuleRight mr = Db.Session.Load(typeof(ModuleRight), deny[i]) as ModuleRight;
                    s.ModuleRightsDeny.Add(mr);
                    mr.StaffDeny.Add(s);
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

        #region public static void UpdatePassword(string loginId, string password)
        /// <summary>
        /// 更新职员的密码。
        /// </summary>
        /// <param name="loginId">登录Id。</param>
        /// <param name="password">新的密码。</param>
        public static void UpdatePassword(string loginId, string password)
        {
            Staff s = Db.Session.Load(typeof(Staff), loginId) as Staff;
            s.Password = StringSecurity.StringToSHA1Hash(password);

            Db.TransUpdate(s);
        }
        #endregion

        #region public static bool UpdatePassword(string loginId, string oldPassword, string newPassword)
        /// <summary>
        /// 更新职员的密码。
        /// </summary>
        /// <param name="loginId">登录Id。</param>
        /// <param name="oldPassword">原密码。</param>
        /// <param name="newPassword">新密码。</param>
        /// <returns>返回true表示更新密码成功；false表示原密码错误。</returns>
        public static bool UpdatePassword(string loginId, string oldPassword, string newPassword)
        {

            Staff s = Db.Session.Load(typeof(Staff), loginId) as Staff;

            if (s.Password == StringSecurity.StringToSHA1Hash(oldPassword))
            {
                s.Password = StringSecurity.StringToSHA1Hash(newPassword);
                Db.TransUpdate(s);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region public static void UpdateRoles(string staffId, string[] roleIds)
        /// <summary>
        /// 更新职员的角色。
        /// </summary>
        /// <param name="staffId">职员登录ID。</param>
        /// <param name="roleIds">角色的ID。</param>
        public static void UpdateRoles(string staffId, string[] roleIds)
        {
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Staff s = Db.Session.Load(typeof(Staff), staffId) as Staff;

                s.Roles.Clear();
                foreach (Role r in s.Roles)
                {
                    r.Staff.Remove(s);
                }
                for (int i = 0; i < roleIds.Length; i++)
                {
                    Role r = Db.Session.Load(typeof(Role), roleIds[i]) as Role;
                    s.Roles.Add(r);
                    r.Staff.Add(s);
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

        #region public static Staff GetStaffByLoginIdAndPassword(string LoginId, string Password)
        /// <summary>
        /// 通过登录ID和密码获取职员。
        /// </summary>
        /// <param name="LoginId">登录ID。</param>
        /// <param name="Password">密码。</param>
        /// <returns>职员。</returns>
        public static Staff GetStaffByLoginIdAndPassword(string LoginId, string Password)
        {
            string hql = "from Staff s where s.LoginId = :loginId and s.Password = :Password";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("loginId", LoginId);
            q.SetString("Password", StringSecurity.StringToSHA1Hash(Password));
            object o = q.UniqueResult();
            return (o == null ? null : o as Staff);
        }

        #endregion

        /// <summary>
        /// 根据用户名返回Staff
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Staff GetStaffByUserName(string userName)
        {
            string hql = "from Staff s where s.Name=:userName";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("userName", userName);
            q.SetMaxResults(1);
            object o = q.UniqueResult();
            return (o == null ? null : o as Staff);
        }

        public static Staff GetStaffByLoginId(string loginId)
        {
            string hql = "from Staff s where s.LoginId = :loginId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("loginId", loginId);
            object o = q.UniqueResult();
            return (o == null ? null : o as Staff);
        }

        #region 获取用户角色信息

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="loginid"></param>
        /// <returns></returns>
        public static ISet<Role> GetRolesByLoginID(string loginid)
        {
            var staff = Db.Session.CreateCriteria(typeof(Staff))
                             .Add(Restrictions.Eq("LoginId",loginid))
                             .UniqueResult<Staff>();
            return staff.Roles;
        }

        #endregion
    }
}
