using System;
using NHibernate;
using System.Collections;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// ���š�
    /// </summary>
    public sealed class DepartmentSrv
    {
        private DepartmentSrv() { }

        #region public static IList GetAllTopDepartment()
        /// <summary>
        /// ��ȡ���ж���Ĳ���ʵ����
        /// </summary>
        /// <returns>���ж���Ĳ���ʵ����</returns>
        public static IList GetAllTopDepartment()
        {
            string hql = "from Department d where d.ParentDepartment is null order by d.OrderId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetCacheable(true);
            q.SetCacheRegion("Department");
            return q.List();
        }
        #endregion


        #region public static string DeleteDepartment(string id)
        /// <summary>
        /// ɾ�����š�
        /// </summary>
        /// <param name="id">Ҫɾ���Ĳ��ŵ�����ֵ��</param>
        /// <returns>ɾ���ɹ���ǡ���1���ɹ���-2�����Ű���ְԱ�����ܱ�ɾ����</returns>
        public static string DeleteDepartment(string id)
        {
            Department d = Db.Session.Load(typeof(Department), id) as Department;
            if (d.HasStaff()) { return "-2"; }

            Db.SessionFactory.EvictQueries("Department");
            d.BreakAwayFromParent();

            Db.TransDelete(d);

            return "1";
        }
        #endregion

        #region public static string InsertDepartment(DepartmentDTO dto)
        /// <summary>
        /// �������š�
        /// </summary>
        /// <param name="dto">���������ŵ���Ϣ��</param>
        /// <returns>�²��ŵ�Id��</returns>
        public static string InsertDepartment(DepartmentDTO dto)
        {
            Db.SessionFactory.EvictQueries("Department");

            Department d = new Department();
            d.Id = IdGen.GetNextId(typeof(Department));
            d.Name = dto.Name;
            d.Phone = dto.Phone;
            d.ExtNumber = dto.ExtNumber;
            d.Fax = dto.Fax;
            d.Remark = dto.Remark;
            d.OrderId = dto.OrderId;

            if (dto.ParentDepartmentId != null && dto.ParentDepartmentId.Length > 0)
            {
                Department pd = Db.Session.Load(typeof(Department), dto.ParentDepartmentId) as Department;
                pd.AddSubDepartment(d);
            }

            Db.TransInsert(d);

            return d.Id;
        }
        #endregion

        #region public static void UpdateDepartment(DepartmentDTO dto)
        /// <summary>
        /// ���²��š�
        /// </summary>
        /// <param name="dto">�����²��ŵ���Ϣ��</param>
        public static void UpdateDepartment(DepartmentDTO dto)
        {
            Db.SessionFactory.EvictQueries("Department");

            Department d = Db.Session.Load(typeof(Department), dto.Id) as Department;
            d.Name = dto.Name;
            d.Phone = dto.Phone;
            d.ExtNumber = dto.ExtNumber;
            d.Fax = dto.Fax;
            d.Remark = dto.Remark;
            d.OrderId = dto.OrderId;

            Db.TransUpdate(d);
        }
        #endregion

        #region public static void MoveDepartment(string id, string newParentId)
        /// <summary>
        /// �ƶ����š�
        /// </summary>
        /// <param name="id">����ID��</param>
        /// <param name="newParentId">�µĲ���Id��</param>
        public static void MoveDepartment(string id, string newParentId)
        {
            Db.SessionFactory.EvictQueries("Department");

            Department d = Db.Session.Load(typeof(Department), id) as Department;

            Department newParent = null;
            if (newParentId != null && newParentId.Length > 0)
            {
                newParent = Db.Session.Load(typeof(Department), newParentId) as Department;
            }

            d.MoveTo(newParent);

            Db.TransUpdate(d);
        }
        #endregion

    }
}
