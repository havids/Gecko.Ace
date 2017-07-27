using System;
using System.Collections;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;
using Gecko.Security.Util;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 用户。
    /// </summary>
    public sealed class UserSrv
    {
        private UserSrv() { }

        #region public static IList GetUsers(int firstResult, int maxResults)
        /// <summary>
        /// 获取用户信息。
        /// </summary>
        /// <param name="firstResult">第一条记录的位置。</param>
        /// <param name="maxResults">返回的最大记录条数。</param>
        /// <returns>用户信息集合。</returns>
        public static IList GetUsers(int firstResult, int maxResults)
        {
            string hql = "from User u order by u.RegisterDate desc";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetFirstResult(firstResult);
            q.SetMaxResults(maxResults);
            q.SetCacheable(true);
            q.SetCacheRegion("User");
            return q.List();
        }
        #endregion

        #region public static int GetUsersCount()
        /// <summary>
        /// 获取用户总数。
        /// </summary>
        /// <returns>用户总数。</returns>
        public static int GetUsersCount()
        {
            string hql = "select count(*) from User u";
            IQuery q = Db.Session.CreateQuery(hql);
            object o = q.UniqueResult();
            return int.Parse(o.ToString());
        }
        #endregion

        #region public static void DeleteUser(string id)
        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="id">要删除的用户的主键值。</param>
        public static void DeleteUser(string id)
        {
            Db.SessionFactory.EvictQueries("User");

            User u = Db.Session.Load(typeof(User), id) as User;

            Db.TransDelete(u);
        }
        #endregion

        #region public static string InsertUser(UserDTO dto)
        /// <summary>
        /// 新增用户。
        /// </summary>
        /// <param name="dto">待新增用户的信息。</param>
        /// <returns>新增是否成功的标示。（1：成功；-2：登录Id重复。）</returns>
        public static string InsertUser(UserDTO dto)
        {
            //判断登录Id是否重复。
            string hql = "select count(*) from User u where u.LoginId = :loginId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("loginId", dto.LoginId);
            object o = q.UniqueResult();
            if (int.Parse(o.ToString()) > 0) { return "-2"; }

            Db.SessionFactory.EvictQueries("User");

            User u = new User();
            u.LoginId = dto.LoginId;
            u.Password = StringSecurity.StringToSHA1Hash(dto.Password);
            u.Name = dto.Name;
            u.Disabled = dto.Disabled;
            u.IdCard = dto.IdCard;
            u.Sex = dto.Sex;
            u.Birthday = dto.Birthday;
            u.OfficePhone = dto.OfficePhone;
            u.CellPhone = dto.CellPhone;
            u.FamilyPhone = dto.FamilyPhone;
            u.Email = dto.Email;
            u.ZipCode = dto.ZipCode;
            u.Remark = dto.Remark;
            u.Address = dto.Address;
            u.RegisterDate = DateTime.Now;

            Db.TransInsert(u);
            return "1";
        }
        #endregion

        #region public static void UpdateUser(UserDTO dto)
        /// <summary>
        /// 更新用户。
        /// </summary>
        /// <param name="dto">待更新用户的信息。</param>
        public static void UpdateUser(UserDTO dto)
        {
            Db.SessionFactory.EvictQueries("User");

            User u = Db.Session.Load(typeof(User), dto.LoginId) as User;
            u.Name = dto.Name;
            u.Disabled = dto.Disabled;
            u.IdCard = dto.IdCard;
            u.Sex = dto.Sex;
            u.Birthday = dto.Birthday;
            u.OfficePhone = dto.OfficePhone;
            u.CellPhone = dto.CellPhone;
            u.FamilyPhone = dto.FamilyPhone;
            u.Email = dto.Email;
            u.ZipCode = dto.ZipCode;
            u.Remark = dto.Remark;
            u.Address = dto.Address;

            Db.TransUpdate(u);
        }
        #endregion

        #region public static void UpdatePassword(string loginId, string password)
        /// <summary>
        /// 更新用户的密码。
        /// </summary>
        /// <param name="loginId">登录Id。</param>
        /// <param name="password">新的密码。</param>
        public static void UpdatePassword(string loginId, string password)
        {
            Db.SessionFactory.EvictQueries("User");

            User u = Db.Session.Load(typeof(User), loginId) as User;
            u.Password = StringSecurity.StringToSHA1Hash(password);

            Db.TransUpdate(u);
        }
        #endregion

        #region public static User GetUserByLoginIdAndPassword(string loginId, string password)
        /// <summary>
        /// 通过登录ID和密码获取用户。
        /// <remarks>
        /// 前台用户在前台登录时使用。
        /// </remarks>
        /// </summary>
        /// <param name="LoginId">登录ID。</param>
        /// <param name="Password">密码。</param>
        /// <returns>用户。</returns>
        public static User GetUserByLoginIdAndPassword(string loginId, string password)
        {
            string hql = "from User u where u.LoginId = :loginId and u.Password = :Password";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("loginId", loginId);
            q.SetString("Password", StringSecurity.StringToSHA1Hash(password));
            object o = q.UniqueResult();
            return (o == null ? null : o as User);
        }
        #endregion

        #region public static bool UpdatePassword(string loginId, string oldPassword, string newPassword)
        /// <summary>
        /// 更新用户的密码。
        /// </summary>
        /// <remarks>
        /// 前台用户在前台页面调用。
        /// </remarks>
        /// <param name="loginId">登录Id。</param>
        /// <param name="oldPassword">原密码。</param>
        /// <param name="newPassword">新密码。</param>
        /// <returns>true：更新密码成功；false：原密码错误。</returns>
        public static bool UpdatePassword(string loginId, string oldPassword, string newPassword)
        {
            User u = Db.Session.Load(typeof(User), loginId) as User;
            if (u.Password == StringSecurity.StringToSHA1Hash(oldPassword))
            {
                Db.SessionFactory.EvictQueries("User");

                u.Password = StringSecurity.StringToSHA1Hash(newPassword);
                Db.TransUpdate(u);

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region public static User GetUserByLoginId(string LoginId)
        /// <summary>
        /// 通过登录ID获取职员。
        /// </summary>
        /// <param name="LoginId">登录ID。</param>
        /// <returns>职员。</returns>
        public static User GetUserByLoginId(string LoginId)
        {
            string hql = "from User s where s.LoginId = :loginId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("loginId", LoginId);
            object o = q.UniqueResult();
            return (o == null ? null : o as User);
        }
        #endregion

    }
}
