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
    /// ְԱ��
    /// </summary>
    public sealed class StaffSrv
    {
        private StaffSrv() { }

        #region public static void DeleteStaff(string id)
        /// <summary>
        /// ɾ��ְԱ��
        /// </summary>
        /// <param name="id">Ҫɾ����ְԱ�ĵ�¼ID��</param>
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
        /// ����ְԱ��
        /// </summary>
        /// <param name="dto">������ְԱ����Ϣ��</param>
        /// <returns>�����Ƿ�ɹ��ı�ʾ����1���ɹ���-2����¼Id�ظ���</returns>
        public static string InsertStaff(StaffDTO dto)
        {
            //�жϵ�¼Id�Ƿ��ظ���
            string hql = "select count(*) from Staff s where s.LoginId = :loginId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("loginId", dto.LoginId);
            object o = q.UniqueResult();
            if (int.Parse(o.ToString()) > 0) { return "-2"; }

            //ְԱ������Ϣ��
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

            //����ͨ����������ְԱ�������ڲ��û���
            s.IsInnerUser = 0;

            //�������š�
            Department d = Db.Session.Load(typeof(Department), dto.DepartmentId) as Department;
            d.AddStaff(s);

            Db.TransInsert(s);
            return "1";
        }
        #endregion

        #region public static void UpdateStaff(StaffDTO dto)
        /// <summary>
        /// ����ְԱ��
        /// </summary>
        /// <param name="dto">������ְԱ����Ϣ��</param>
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
        /// �ƶ�ְԱ��
        /// </summary>
        /// <param name="Id">��¼Id��</param>
        /// <param name="newDepartmentId">�µĲ���Id��</param>
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
        /// ����ְԱ��ģ����Ȩ��Ϣ��
        /// </summary>
        /// <param name="staffId">ְԱ��¼ID��</param>
        /// <param name="grant">�϶���Ȩ��Ȩ��ID��</param>
        /// <param name="deny">����Ȩ��Ȩ��ID��</param>
        public static void UpdatePermissions(string staffId, string[] grant, string[] deny)
        {
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Staff s = Db.Session.Load(typeof(Staff), staffId) as Staff;

                //���¿϶�Ȩ�ޡ�
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

                //���¿϶�Ȩ�ޡ�
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
        /// ����ְԱ�����롣
        /// </summary>
        /// <param name="loginId">��¼Id��</param>
        /// <param name="password">�µ����롣</param>
        public static void UpdatePassword(string loginId, string password)
        {
            Staff s = Db.Session.Load(typeof(Staff), loginId) as Staff;
            s.Password = StringSecurity.StringToSHA1Hash(password);

            Db.TransUpdate(s);
        }
        #endregion

        #region public static bool UpdatePassword(string loginId, string oldPassword, string newPassword)
        /// <summary>
        /// ����ְԱ�����롣
        /// </summary>
        /// <param name="loginId">��¼Id��</param>
        /// <param name="oldPassword">ԭ���롣</param>
        /// <param name="newPassword">�����롣</param>
        /// <returns>����true��ʾ��������ɹ���false��ʾԭ�������</returns>
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
        /// ����ְԱ�Ľ�ɫ��
        /// </summary>
        /// <param name="staffId">ְԱ��¼ID��</param>
        /// <param name="roleIds">��ɫ��ID��</param>
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
        /// ͨ����¼ID�������ȡְԱ��
        /// </summary>
        /// <param name="LoginId">��¼ID��</param>
        /// <param name="Password">���롣</param>
        /// <returns>ְԱ��</returns>
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
        /// �����û�������Staff
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

        #region ��ȡ�û���ɫ��Ϣ

        /// <summary>
        /// ��ȡ�û��Ľ�ɫ
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
