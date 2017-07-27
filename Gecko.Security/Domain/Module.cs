using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	ģ�顣
    /// </summary>
    public class Module
    {
        private string _id;
        private string _tag;
        private string _name;
        private string _module_url;
        private string _remark;
        private int _disabled;
        private int _order_id;
        private ModuleType _module_type;
        private IDictionary<object,ModuleRight> _module_rights;

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
        /// ģ���ַ��
        /// </summary>
        public virtual string ModuleUrl
        {
            get { return _module_url; }
            set { _module_url = value; }
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
        /// �ѽ��á�
        /// </summary>
        public virtual int Disabled
        {
            get { return _disabled; }
            set { _disabled = value; }
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
        /// ģ����ࡣ
        /// </summary>
        public virtual ModuleType ModuleType
        {
            get { return _module_type; }
            set { _module_type = value; }
        }

        /// <summary>
        /// ģ��Ȩ�޼��ϡ�
        /// </summary>
        public virtual IDictionary<object, ModuleRight> ModuleRights
        {
            get { return _module_rights; }
            set { _module_rights = value; }
        }

        #endregion

        #region ���캯��

        public Module()
        {
            _id = String.Empty;
            _tag = String.Empty;
            _name = String.Empty;
            _module_url = String.Empty;
            _remark = String.Empty;
            _disabled = 0;
            _order_id = 0;
            _module_type = null;
            _module_rights = new Dictionary<object, ModuleRight>();
        }

        #endregion

        #region ��������


        /// <summary>
        /// �ƶ�ģ�顣
        /// </summary>
        /// <param name="newParent">�µ�ģ����ࡣ</param>
        public virtual void MoveTo(ModuleType newParent)
        {
            ModuleType oldParent = this.ModuleType;
            oldParent.Modules.Remove(this);

            newParent.Modules.Add(this);
            this.ModuleType = newParent;
        }


        /// <summary>
        /// ����ģ��Ȩ�ޡ�
        /// </summary>
        /// <param name="moduleRight">ģ��Ȩ�ޡ�</param>
        public virtual void AddModuleRight(ModuleRight moduleRight)
        {
            this.ModuleRights.Add(moduleRight.RightTag, moduleRight);
            moduleRight.Module = this;
        }


        /// <summary>
        /// ��ģ��������롣
        /// </summary>
        public virtual void BreakAwayFromModuleType()
        {
            ModuleType mt = this.ModuleType;
            this.ModuleType = null;
            mt.Modules.Remove(this);
        }


        #endregion

    }
}