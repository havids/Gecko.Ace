using System;
using Gecko.Security.NHHelper;

namespace Gecko.Security.Service
{
    /// <summary>
    /// ���õ�Ӧ���߼����ܡ�
    /// </summary>
    public sealed class CommonSrv
    {
        private CommonSrv() { }
        
        #region public static object LoadObjectById(Type type, object id)
        /// <summary>
        /// ����Id���ز����س־û�����
        /// </summary>
        /// <param name="type">�־û���������͡�</param>
        /// <param name="id">�־û����������ֵ��</param>
        /// <returns>�־û�����</returns>
        public static object LoadObjectById(Type type, object id)
        {
            return Db.Session.Load(type, id);
        }
        #endregion

    }
}
