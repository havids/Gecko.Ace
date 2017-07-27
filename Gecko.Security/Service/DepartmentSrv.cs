using System;
using NHibernate;
using System.Collections;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 部门。
    /// </summary>
    public sealed class DepartmentSrv
    {
        private DepartmentSrv() { }

        #region public static IList GetAllTopDepartment()
        /// <summary>
        /// 获取所有顶层的部门实例。
        /// </summary>
        /// <returns>所有顶层的部门实例。</returns>
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
        /// 删除部门。
        /// </summary>
        /// <param name="id">要删除的部门的主键值。</param>
        /// <returns>删除成功标记。（1：成功；-2：部门包含职员，不能被删除）</returns>
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
        /// 新增部门。
        /// </summary>
        /// <param name="dto">待新增部门的信息。</param>
        /// <returns>新部门的Id。</returns>
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
        /// 更新部门。
        /// </summary>
        /// <param name="dto">待更新部门的信息。</param>
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
        /// 移动部门。
        /// </summary>
        /// <param name="id">部门ID。</param>
        /// <param name="newParentId">新的部门Id。</param>
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
