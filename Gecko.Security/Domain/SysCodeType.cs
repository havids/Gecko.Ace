using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	代码分类。
    /// </summary>
    public class SysCodeType
    {
        private string _id;
        private string _tag;
        private string _name;
        private string _remark;
        private int _order_id;
        private IList _syscodes;

        #region 属性

        /// <summary>
        /// ID。
        /// </summary>
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 标示。
        /// </summary>
        public virtual string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 备注。
        /// </summary>
        public virtual string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// 排序ID。
        /// </summary>
        public virtual int OrderId
        {
            get { return _order_id; }
            set { _order_id = value; }
        }

        /// <summary>
        /// 代码列表。
        /// </summary>
        public virtual IList SysCodes
        {
            get { return _syscodes; }
            set { _syscodes = value; }
        }

        #endregion

        #region 构造函数

        public SysCodeType()
        {
            _id = String.Empty;
            _tag = String.Empty;
            _name = String.Empty;
            _remark = String.Empty;
            _order_id = 0;
            _syscodes = new List<SysCode>();
        }

        #endregion

        #region 公共方法


        /// <summary>
        /// 增加一个代码。
        /// </summary>
        /// <param name="staff">代码。</param>
        public virtual void AddSysCode(SysCode s)
        {
            this.SysCodes.Add(s);
            s.SysCodeTypee = this;
        }


        #endregion

    }
}
