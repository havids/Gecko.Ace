using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	ģ����ࡣ
    /// </summary>
    public class ModuleType
    {
        private string _id;
        private string _name;
        private string _remark;
        private int _order_id;
        private ModuleType _parent_module_type;
        private IList _modules;
        private IList _sub_module_types;

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
        /// ��ģ����ࡣ
        /// </summary>
        public virtual ModuleType ParentModuleType
        {
            get { return _parent_module_type; }
            set { _parent_module_type = value; }
        }

        /// <summary>
        /// ģ���б�
        /// </summary>
        public virtual IList Modules
        {
            get { return _modules; }
            set { _modules = value; }
        }

        /// <summary>
        /// ��ģ������б�
        /// </summary>
        public virtual IList SubModuleTypes
        {
            get { return _sub_module_types; }
            set { _sub_module_types = value; }
        }

        #endregion

        #region ���캯��

        public ModuleType()
        {
            _id = String.Empty;
            _name = String.Empty;
            _remark = String.Empty;
            _order_id = 0;
            _parent_module_type = null;
            _modules = new List<Module>();
            _sub_module_types = new List<ModuleType>();
        }

        #endregion

        #region ��������


        /// <summary>
        /// �ƶ�ģ����ࡣ
        /// </summary>
        /// <param name="newParent">�µĸ�ģ����ࡣ</param>
        public virtual void MoveTo(ModuleType newParent)
        {
            if (this.ParentModuleType != null)
            {
                ModuleType oldParent = this.ParentModuleType;
                oldParent.SubModuleTypes.Remove(this);
            }

            if (newParent != null)
            {
                newParent.SubModuleTypes.Add(this);
                this.ParentModuleType = newParent;
            }
            else
            {
                this.ParentModuleType = null;
            }
        }


        /// <summary>
        /// ���ϼ�ģ��������롣��ʹ�Լ���Ϊ�����ģ����ࣩ
        /// </summary>
        public virtual void BreakAwayFromParent()
        {
            if (this.ParentModuleType != null)
            {
                ModuleType p = this.ParentModuleType;
                this.ParentModuleType = null;
                p.SubModuleTypes.Remove(this);
            }
        }


        /// <summary>
        /// ����һ����ģ����ࡣ
        /// </summary>
        /// <param name="subDepartment">��ģ����ࡣ</param>
        public virtual void AddSubModuleType(ModuleType subModuleType)
        {
            this.SubModuleTypes.Add(subModuleType);
            subModuleType.ParentModuleType = this;
        }


        /// <summary>
        /// ����ģ�顣
        /// </summary>
        /// <param name="module">ģ�顣</param>
        public virtual void AddModule(Module module)
        {
            this.Modules.Add(module);
            module.ModuleType = this;
        }


        #endregion

    }
}
