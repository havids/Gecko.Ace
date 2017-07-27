using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	������ࡣ
    /// </summary>
    public class SysCodeType
    {
        private string _id;
        private string _tag;
        private string _name;
        private string _remark;
        private int _order_id;
        private IList _syscodes;

        #region ����

        /// <summary>
        /// ID��
        /// </summary>
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// ��ʾ��
        /// </summary>
        public virtual string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// ��ע��
        /// </summary>
        public virtual string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// ����ID��
        /// </summary>
        public virtual int OrderId
        {
            get { return _order_id; }
            set { _order_id = value; }
        }

        /// <summary>
        /// �����б�
        /// </summary>
        public virtual IList SysCodes
        {
            get { return _syscodes; }
            set { _syscodes = value; }
        }

        #endregion

        #region ���캯��

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

        #region ��������


        /// <summary>
        /// ����һ�����롣
        /// </summary>
        /// <param name="staff">���롣</param>
        public virtual void AddSysCode(SysCode s)
        {
            this.SysCodes.Add(s);
            s.SysCodeTypee = this;
        }


        #endregion

    }
}
