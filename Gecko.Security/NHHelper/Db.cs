using System.Web;

using NHibernate;

namespace Gecko.Security.NHHelper
{
	/// <summary>
	/// ��ASP.NET������ʹ��NHibernate�İ����ࡣ
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
        /// ��ȡһ�����õ�Sessionʵ����
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
		/// ��ȡ��ǰ��SessionFactoryʵ����
		/// </summary>
		public static ISessionFactory SessionFactory
		{
			get { return Db.sessionFactory; }
		}
		#endregion

		#region public static Configuration Configuration
		/// <summary>
		/// ��ȡ��ǰ��Configurationʵ����
		/// </summary>
        public static NHibernate.Cfg.Configuration Configuration
		{
			get { return Db.configuration; }
		}
		#endregion

		#region public static void CloseSession()
		/// <summary>
		/// �ر�Session��
		/// </summary>
		/// <remarks>
		/// Ӧ����EndRequest�¼�����ʱ�����ã�д��HttpModule�С�
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
		/// ��ָ����ʵ�����뵽���ݿ��С��������Բ����������ڿ��ܰ�������������������
		/// </summary>
		/// <param name="item">�������ʵ����</param>
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
        /// ��ָ����ʵ�����µ����ݿ��С��������Բ����������ڿ��ܰ�������������������
		/// </summary>
		/// <param name="item">�����µ�ʵ����</param>
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
        /// �����ݿ���ɾ��ָ����ʵ�����������Բ����������ڿ��ܰ�������������������
		/// </summary>
		/// <param name="item">��ɾ����ʵ����</param>
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