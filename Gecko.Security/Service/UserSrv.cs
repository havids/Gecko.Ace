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
    /// �û���
    /// </summary>
    public sealed class UserSrv
    {
        private UserSrv() { }

        #region public static IList GetUsers(int firstResult, int maxResults)
        /// <summary>
        /// ��ȡ�û���Ϣ��
        /// </summary>
        /// <param name="firstResult">��һ����¼��λ�á�</param>
        /// <param name="maxResults">���ص�����¼������</param>
        /// <returns>�û���Ϣ���ϡ�</returns>
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
        /// ��ȡ�û�������
        /// </summary>
        /// <returns>�û�������</returns>
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
        /// ɾ���û���
        /// </summary>
        /// <param name="id">Ҫɾ�����û�������ֵ��</param>
        public static void DeleteUser(string id)
        {
            Db.SessionFactory.EvictQueries("User");

            User u = Db.Session.Load(typeof(User), id) as User;

            Db.TransDelete(u);
        }
        #endregion

        #region public static string InsertUser(UserDTO dto)
        /// <summary>
        /// �����û���
        /// </summary>
        /// <param name="dto">�������û�����Ϣ��</param>
        /// <returns>�����Ƿ�ɹ��ı�ʾ����1���ɹ���-2����¼Id�ظ�����</returns>
        public static string InsertUser(UserDTO dto)
        {
            //�жϵ�¼Id�Ƿ��ظ���
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
        /// �����û���
        /// </summary>
        /// <param name="dto">�������û�����Ϣ��</param>
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
        /// �����û������롣
        /// </summary>
        /// <param name="loginId">��¼Id��</param>
        /// <param name="password">�µ����롣</param>
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
        /// ͨ����¼ID�������ȡ�û���
        /// <remarks>
        /// ǰ̨�û���ǰ̨��¼ʱʹ�á�
        /// </remarks>
        /// </summary>
        /// <param name="LoginId">��¼ID��</param>
        /// <param name="Password">���롣</param>
        /// <returns>�û���</returns>
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
        /// �����û������롣
        /// </summary>
        /// <remarks>
        /// ǰ̨�û���ǰ̨ҳ����á�
        /// </remarks>
        /// <param name="loginId">��¼Id��</param>
        /// <param name="oldPassword">ԭ���롣</param>
        /// <param name="newPassword">�����롣</param>
        /// <returns>true����������ɹ���false��ԭ�������</returns>
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
        /// ͨ����¼ID��ȡְԱ��
        /// </summary>
        /// <param name="LoginId">��¼ID��</param>
        /// <returns>ְԱ��</returns>
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
