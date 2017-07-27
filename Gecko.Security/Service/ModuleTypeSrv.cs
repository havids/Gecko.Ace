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
    /// 模块类型。
    /// </summary>
    public sealed class ModuleTypeSrv
    {
        private ModuleTypeSrv() { }

        #region public static IList GetAllTopModuleType()
        /// <summary>
        /// 获取所有顶层的模块分类实例。
        /// </summary>
        /// <returns>所有顶层的模块分类实例。</returns>
        public static IList GetAllTopModuleType()
        {
            //TODO 设置第三方缓存 memcache 
            string hql = "from ModuleType m where m.ParentModuleType is null order by m.OrderId";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetCacheable(true);
            q.SetCacheRegion("ModuleType");
            return q.List();
        }
        ///// <summary>
        ///// 根据pbId 获取顶层模块
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
        /// 删除模块分类。
        /// </summary>
        /// <param name="id">要删除的模块分类的主键值。</param>
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
        /// 新增模块分类。
        /// </summary>
        /// <param name="dto">待新增模块分类的信息。</param>
        /// <returns>新模块分类的Id。</returns>
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
        /// 更新模块分类。
        /// </summary>
        /// <param name="dto">待更新模块分类的信息。</param>
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
        /// 移动模块分类。
        /// </summary>
        /// <param name="Id">模块分类Id。</param>
        /// <param name="newParentModuleTypeId">新的父模块分类Id。</param>
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
