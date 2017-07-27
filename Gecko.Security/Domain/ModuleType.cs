using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Security.Domain
{
    /// <summary>
    ///	模块分类。
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
        /// 父模块分类。
        /// </summary>
        public virtual ModuleType ParentModuleType
        {
            get { return _parent_module_type; }
            set { _parent_module_type = value; }
        }

        /// <summary>
        /// 模块列表。
        /// </summary>
        public virtual IList Modules
        {
            get { return _modules; }
            set { _modules = value; }
        }

        /// <summary>
        /// 子模块分类列表。
        /// </summary>
        public virtual IList SubModuleTypes
        {
            get { return _sub_module_types; }
            set { _sub_module_types = value; }
        }

        #endregion

        #region 构造函数

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

        #region 公共方法


        /// <summary>
        /// 移动模块分类。
        /// </summary>
        /// <param name="newParent">新的父模块分类。</param>
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
        /// 从上级模块分类脱离。（使自己成为顶层的模块分类）
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
        /// 增加一个子模块分类。
        /// </summary>
        /// <param name="subDepartment">子模块分类。</param>
        public virtual void AddSubModuleType(ModuleType subModuleType)
        {
            this.SubModuleTypes.Add(subModuleType);
            subModuleType.ParentModuleType = this;
        }


        /// <summary>
        /// 增加模块。
        /// </summary>
        /// <param name="module">模块。</param>
        public virtual void AddModule(Module module)
        {
            this.Modules.Add(module);
            module.ModuleType = this;
        }


        #endregion

    }
}
