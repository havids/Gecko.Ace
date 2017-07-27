using System;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;

namespace Gecko.Security.Service
{
    /// <summary>
    /// ģ��Ȩ�ޡ�
    /// </summary>
    public sealed class ModuleRightSrv
    {
        private ModuleRightSrv() { }

        #region public static ModuleRight GetModuleRight(Module module, string rightTag)
        /// <summary>
        /// ����ģ���Ȩ�ޱ�ʾ��ȡģ��Ȩ�ޡ�
        /// </summary>
        /// <param name="module">ģ�顣</param>
        /// <param name="rightTag">Ȩ�ޱ�ʾ��</param>
        /// <returns>ģ��Ȩ�ޡ�</returns>
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
