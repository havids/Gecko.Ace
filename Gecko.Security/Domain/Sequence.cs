using System;
using System.Collections;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	���С�
    /// </summary>
    public class Sequence
    {
        private string _table_name;
        private int _next_id;

        #region ����

        /// <summary>
        /// �����ơ�
        /// </summary>
        public virtual string TableName
        {
            get { return _table_name; }
            set { _table_name = value; }
        }

        /// <summary>
        /// ��һ��IDֵ��
        /// </summary>
        public virtual int NextId
        {
            get { return _next_id; }
            set { _next_id = value; }
        }

        #endregion

        #region ���캯��

        public Sequence()
        {
            _table_name = String.Empty;
            _next_id = 0;
        }

        #endregion

    }
}
