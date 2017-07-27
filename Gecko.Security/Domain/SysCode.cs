using System;
using System.Collections;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	代码。
    /// </summary>
    public class SysCode
    {
        private string _id;
        private string _tag;
        private string _name;
        private string _remark;
        private int _order_id;
        private SysCodeType _syscode_type;

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
        /// 代码分类。
        /// </summary>
        public virtual SysCodeType SysCodeTypee
        {
            get { return _syscode_type; }
            set { _syscode_type = value; }
        }

        #endregion

        #region 构造函数

        public SysCode()
        {
            _id = String.Empty;
            _tag = String.Empty;
            _name = String.Empty;
            _remark = String.Empty;
            _order_id = 0;
            _syscode_type = null;
        }

        #endregion

        #region 公共方法


        /// <summary>
        /// 从代码分类脱离。
        /// </summary>
        public virtual void BreakAwayFromSysCodeType()
        {
            SysCodeType s = this.SysCodeTypee;
            this.SysCodeTypee = null;
            s.SysCodes.Remove(this);
        }


        #endregion

    }
}