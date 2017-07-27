using System.Web;

using NHibernate;

namespace Gecko.Security.NHHelper
{
	/// <summary>
	/// 在ASP.NET环境下使用NHibernate的帮助类。
	/// </summary>
	public sealed class Db
	{
        private const string sessionKey = "NHibernate.Db";
        private static readonly NHibernate.Cfg.Configuration configuration = new NHibernate.Cfg.Configuration();
        private static readonly ISessionFactory sessionFactory = configuration.Configure().BuildSessionFactory();

        private Db() { }
        static Db() { }

		#region public static ISession Session

        /// <summary>
        /// 获取一个可用的Session实例。
        /// </summary>
        /// 
        public static ISession Session
        {
            get
            {
                ISession session;
                if (HttpContext.Current.Items.Contains(sessionKey))
                {
                    session = (ISession)HttpContext.Current.Items[sessionKey];
                }
                else
                {
                    session = Db.sessionFactory.OpenSession();
                    HttpContext.Current.Items[sessionKey] = session;
                }
                return session;
            }
        }
		#endregion

		#region public static ISessionFactory SessionFactory
		/// <summary>
		/// 获取当前的SessionFactory实例。
		/// </summary>
		public static ISessionFactory SessionFactory
		{
			get { return Db.sessionFactory; }
		}
		#endregion

		#region public static Configuration Configuration
		/// <summary>
		/// 获取当前的Configuration实例。
		/// </summary>
        public static NHibernate.Cfg.Configuration Configuration
		{
			get { return Db.configuration; }
		}
		#endregion

		#region public static void CloseSession()
		/// <summary>
		/// 关闭Session。
		/// </summary>
		/// <remarks>
		/// 应该在EndRequest事件发生时被调用，写在HttpModule中。
		/// </remarks>
		public static void CloseSession()
		{
			if (HttpContext.Current.Items.Contains(sessionKey))
			{
				ISession session = (ISession)HttpContext.Current.Items[sessionKey];
				session.Close();
				HttpContext.Current.Items.Remove(sessionKey);
			}
		}
		#endregion
        
        #region public static void TransInsert(object item)
        /// <summary>
		/// 将指定的实例插入到数据库中。（事务性操作，适用于可能包含及联处理的情况。）
		/// </summary>
		/// <param name="item">待插入的实例。</param>
        public static void TransInsert(object item)
		{
			ITransaction transaction = Db.Session.BeginTransaction();

			try
			{
				Db.Session.Save(item);
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}
		#endregion

        #region public static void TransUpdate(object item)
        /// <summary>
        /// 将指定的实例更新到数据库中。（事务性操作，适用于可能包含及联处理的情况。）
		/// </summary>
		/// <param name="item">待更新的实例。</param>
        public static void TransUpdate(object item)
		{
			ITransaction transaction = Db.Session.BeginTransaction();

			try
			{
				Db.Session.Update(item);
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}
		#endregion

        #region public static void TransDelete(object item)
        /// <summary>
        /// 在数据库中删除指定的实例。（事务性操作，适用于可能包含及联处理的情况。）
		/// </summary>
		/// <param name="item">待删除的实例。</param>
        public static void TransDelete(object item)
		{
			ITransaction transaction = Db.Session.BeginTransaction();

			try
			{
				Db.Session.Delete(item);
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