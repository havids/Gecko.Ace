using System;
using System.Collections;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	序列。
    /// </summary>
    public class Sequence
    {
        private string _table_name;
        private int _next_id;

        #region 属性

        /// <summary>
        /// 表名称。
        /// </summary>
        public virtual string TableName
        {
            get { return _table_name; }
            set { _table_name = value; }
        }

        /// <summary>
        /// 下一个ID值。
        /// </summary>
        public virtual int NextId
        {
            get { return _next_id; }
            set { _next_id = value; }
        }

        #endregion

        #region 构造函数

        public Sequence()
        {
            _table_name = String.Empty;
            _next_id = 0;
        }

        #endregion

    }
}
