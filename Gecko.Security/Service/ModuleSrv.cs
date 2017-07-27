using System;
using System.Collections;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 模块。
    /// </summary>
    public sealed class ModuleSrv
    {
        private ModuleSrv() { }

        #region public static Module GetModuleByTag(string tag)
        /// <summary>
        /// 根据标示获取模块。
        /// </summary>
        /// <param name="tag">标示。</param>
        /// <returns>模块。</returns>
        public static Module GetModuleByTag(string tag)
        {
            string hql = "from Module m where m.Tag = :tag";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("tag", tag);
            q.SetCacheable(true);
            q.SetCacheRegion("Module");
            object o = q.UniqueResult();
            return (o != null) ? (Module)o : null;
        }
        #endregion

        #region public static IList GetAllDisabledModule()

        /// <summary>
        /// 获取所有有效未禁用的模块列表。
        /// </summary>
        /// <param name="tag">标示。</param>
        /// <returns>模块。</returns>
        public static IList GetAllDisabledModule()
        {
            string hql = "from Module m where m.Disabled = :disabled";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetInt32("disabled", 0);
            q.SetCacheable(true);
            q.SetCacheRegion("Module");
            return q.List();
        }

        #endregion

        #region public static void DeleteModule(string id)
        /// <summary>
        /// 删除模块。
        /// </summary>
        /// <param name="id">要删除的模块的主键值。</param>
        public static void DeleteModule(string id)
        {
            Db.SessionFactory.EvictQueries("Module");

            Module m = Db.Session.Load(typeof(Module), id) as Module;
            m.BreakAwayFromModuleType();

            Db.TransDelete(m);
        }
        #endregion

        #region public static string InsertModule(ModuleDTO dto)
        /// <summary>
        /// 新增模块。
        /// </summary>
        /// <param name="dto">待新增模块的信息。</param>
        /// <returns>新模块的Id。（-2：Tag重复。）</returns>
        public static string InsertModule(ModuleDTO dto)
        {
            Module existingM = GetModuleByTag(dto.Tag);
            if (existingM != null) { return "-2"; }

            Db.SessionFactory.EvictQueries("Module");

            //模块基本信息。
            Module m = new Module();
            m.Id = IdGen.GetNextId(typeof(Module));
            m.Tag = dto.Tag;
            m.Name = dto.Name;
            m.Remark = dto.Remark;
            m.OrderId = dto.OrderId;
            m.ModuleUrl = dto.ModuleUrl;
            m.Disabled = dto.Disabled?1:0;

            //模块分类。
            ModuleType mt = Db.Session.Load(typeof(ModuleType), dto.ModuleTypeId) as ModuleType;
            mt.AddModule(m);

            //获取新增模块权限的主键值。
            string[] Ids = null;
            if (dto.ModuleRights.Count > 0)
                Ids = IdGen.GetNextId(typeof(ModuleRight), dto.ModuleRights.Count);

            //向数据库保存模块和模块权限。
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Db.Session.Save(m);
                for (int i = 0; i < dto.ModuleRights.Count; i++)
                {
                    ModuleRight mr = new ModuleRight();
                    mr.Id = Ids[i];
                    mr.RightTag = dto.ModuleRights[i].ToString();
                    m.AddModuleRight(mr);
                    Db.Session.Save(mr);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            //返回新模块的Id。
            return m.Id;
        }
        #endregion

        #region public static string UpdateModule(ModuleDTO dto)
        /// <summary>
        /// 更新模块。
        /// </summary>
        /// <param name="dto">待更新模块的信息。</param>
        /// <returns>成功标示。（1：成功；-2：Tag重复。）</returns>
        public static string UpdateModule(ModuleDTO dto)
        {
            Module existingM = GetModuleByTag(dto.Tag);
            if (existingM != null && existingM.Id != dto.Id) { return "-2"; }

            Db.SessionFactory.EvictQueries("Module");

            //模块基本信息。
            Module m = Db.Session.Load(typeof(Module), dto.Id) as Module;
            m.Tag = dto.Tag;
            m.Name = dto.Name;
            m.Remark = dto.Remark;
            m.OrderId = dto.OrderId;
            m.ModuleUrl = dto.ModuleUrl;
            m.Disabled = dto.Disabled?1:0;

            //待删除的权限。
            ArrayList ToBeRemoved = new ArrayList();

            foreach (var k in m.ModuleRights)
            {
                string key = k.Key.ToString();
                bool isFind = false;
                for (int i = 0; i < dto.ModuleRights.Count; i++)
                {
                    if (key == dto.ModuleRights[i].ToString())
                    {
                        isFind = true;
                        break;
                    }
                }
                if (!isFind)
                {
                    ToBeRemoved.Add(key);
                }
            }


            //待增加的权限。
            ArrayList ToBeAdded = new ArrayList();
            for (int i = 0; i < dto.ModuleRights.Count; i++)
            {
                //2017-07-20 修改
                if (!m.ModuleRights.ContainsKey(dto.ModuleRights[i]))
                    ToBeAdded.Add(dto.ModuleRights[i].ToString());
            }

            //获取新增模块权限的主键值。
            string[] Ids = null;
            if (ToBeAdded.Count > 0)
                Ids = IdGen.GetNextId(typeof(ModuleRight), ToBeAdded.Count);

            //向数据库保存新的模块信息和模块权限信息。
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                //删除权限。
                foreach (object key in ToBeRemoved)
                {
                    ModuleRight mr = m.ModuleRights[key] as ModuleRight;
                    mr.RemoveAllPermissions();
                    mr.BreakAwayFromModule();
                    Db.Session.Delete(mr);
                }

                //增加权限。
                for (int i = 0; i < ToBeAdded.Count; i++)
                {
                    ModuleRight newMr = new ModuleRight();
                    newMr.Id = Ids[i];
                    newMr.RightTag = ToBeAdded[i].ToString();
                    m.AddModuleRight(newMr);
                    Db.Session.Save(newMr);
                }

                Db.Session.Update(m);
                transaction.Commit();

                return "1";
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion

        #region public static void MoveModule(string id, string newModuleTypeId)
        /// <summary>
        /// 移动模块。
        /// </summary>
        /// <param name="id">要移动的模块的ID。</param>
        /// <param name="newModuleTypeId">新的模块分类的Id。</param>
        public static void MoveModule(string id, string newModuleTypeId)
        {
            Db.SessionFactory.EvictQueries("Module");

            Module m = Db.Session.Load(typeof(Module), id) as Module;
            ModuleType newParent = Db.Session.Load(typeof(ModuleType), newModuleTypeId) as ModuleType;

            m.MoveTo(newParent);

            Db.TransUpdate(m);
        }
        #endregion

    }
}
