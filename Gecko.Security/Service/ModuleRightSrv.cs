using System;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 模块权限。
    /// </summary>
    public sealed class ModuleRightSrv
    {
        private ModuleRightSrv() { }

        #region public static ModuleRight GetModuleRight(Module module, string rightTag)
        /// <summary>
        /// 根据模块和权限标示获取模块权限。
        /// </summary>
        /// <param name="module">模块。</param>
        /// <param name="rightTag">权限标示。</param>
        /// <returns>模块权限。</returns>
        public static ModuleRight GetModuleRight(Module module, string rightTag)
        {
            string hql = "from ModuleRight mr where mr.Module = :module and mr.RightTag = :rightTag";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetEntity("module", module);
            q.SetString("rightTag", rightTag);
            object o = q.UniqueResult();

            return (o == null) ? null : o as ModuleRight;
        }
        #endregion

    }
}
