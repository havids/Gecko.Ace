using System;
using Gecko.Security.NHHelper;

namespace Gecko.Security.Service
{
    /// <summary>
    /// 共用的应用逻辑功能。
    /// </summary>
    public sealed class CommonSrv
    {
        private CommonSrv() { }
        
        #region public static object LoadObjectById(Type type, object id)
        /// <summary>
        /// 根据Id加载并返回持久化对象。
        /// </summary>
        /// <param name="type">持久化对象的类型。</param>
        /// <param name="id">持久化对象的主键值。</param>
        /// <returns>持久化对象。</returns>
        public static object LoadObjectById(Type type, object id)
        {
            return Db.Session.Load(type, id);
        }
        #endregion

    }
}
