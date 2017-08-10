using System;
using System.Collections;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;
using System.Collections.Generic;

namespace Gecko.Security.Service
{
    /// <summary>
    /// ģ�����͡�
    /// </summary>
    public sealed class ModuleTypeSrv
    {
        private ModuleTypeSrv() { }

        #region public static IList GetAllTopModuleType()
        /// <summary>
        /// ��ȡ���ж����ģ�����ʵ����
        /// </summary>
        /// <returns>���ж����ģ�����ʵ����</returns>
        public static IList GetAllTopModuleType()
        {
            string hql = "from ModuleType m where m.ParentModuleType is null order by m.OrderId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetCacheable(true);
            q.SetCacheRegion("ModuleType");
            return q.List();
        }
        ///// <summary>
        ///// ����pbId ��ȡ����ģ��
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        public static IList GetTopModuleType(string id)
        {
            string hql = "from ModuleType m where m.ParentModuleType is null and m.Id=:pbId order by m.OrderId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("pbId", id);
            q.SetCacheable(true);
            q.SetCacheRegion("ModuleType");
            return q.List();
        }
        #endregion


        #region public static void DeleteModuleType(string id)
        /// <summary>
        /// ɾ��ģ����ࡣ
        /// </summary>
        /// <param name="id">Ҫɾ����ģ����������ֵ��</param>
        public static void DeleteModuleType(string id)
        {
            Db.SessionFactory.EvictQueries("ModuleType");

            ModuleType mt = Db.Session.Load(typeof(ModuleType), id) as ModuleType;
            mt.BreakAwayFromParent();

            Db.TransDelete(mt);
        }
        #endregion

        #region public static string InsertModuleType(ModuleTypeDTO dto)
        /// <summary>
        /// ����ģ����ࡣ
        /// </summary>
        /// <param name="dto">������ģ��������Ϣ��</param>
        /// <returns>��ģ������Id��</returns>
        public static string InsertModuleType(ModuleTypeDTO dto)
        {
            Db.SessionFactory.EvictQueries("ModuleType");

            ModuleType mt = new ModuleType();
            mt.Id = IdGen.GetNextId(typeof(ModuleType));
            mt.Name = dto.Name;
            mt.Remark = dto.Remark;
            mt.OrderId = dto.OrderId;

            if (dto.ParentModuleTypeId != null && dto.ParentModuleTypeId.Length > 0)
            {
                ModuleType pmt = Db.Session.Load(typeof(ModuleType), dto.ParentModuleTypeId) as ModuleType;
                pmt.AddSubModuleType(mt);
            }

            Db.TransInsert(mt);

            return mt.Id;
        }
        #endregion

        #region public static void UpdateModuleType(ModuleTypeDTO dto)
        /// <summary>
        /// ����ģ����ࡣ
        /// </summary>
        /// <param name="dto">������ģ��������Ϣ��</param>
        public static void UpdateModuleType(ModuleTypeDTO dto)
        {
            Db.SessionFactory.EvictQueries("ModuleType");

            ModuleType mt = Db.Session.Load(typeof(ModuleType), dto.Id) as ModuleType;
            mt.Name = dto.Name;
            mt.Remark = dto.Remark;
            mt.OrderId = dto.OrderId;

            Db.TransUpdate(mt);
        }
        #endregion

        #region public static void MoveModuleType(string Id, string newParentId)
        /// <summary>
        /// �ƶ�ģ����ࡣ
        /// </summary>
        /// <param name="Id">ģ�����Id��</param>
        /// <param name="newParentModuleTypeId">�µĸ�ģ�����Id��</param>
        public static void MoveModuleType(string Id, string newParentId)
        {
            Db.SessionFactory.EvictQueries("ModuleType");

            ModuleType mt = Db.Session.Load(typeof(ModuleType), Id) as ModuleType;

            ModuleType newParent = null;
            if (newParentId != null && newParentId.Length > 0)
            {
                newParent = Db.Session.Load(typeof(ModuleType), newParentId) as ModuleType;
            }

            mt.MoveTo(newParent);

            Db.TransUpdate(mt);
        }
        #endregion

    }
}
